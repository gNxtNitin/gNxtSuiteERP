Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLoanStmt
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColLoanType As Short = 3
    Private Const ColLoanDate As Short = 4
    Private Const ColLoanAmount As Short = 5
    Private Const ColInsttAmount As Short = 6
    Private Const ColBalance As Short = 7

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With sprdLoan
            .MaxCols = ColBalance
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)
            .set_RowHeight(0, ConRowHeight * 2)

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
            .set_ColWidth(ColLoanType, 12)

            .Col = ColLoanAmount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColLoanAmount, 10)

            .Col = ColLoanDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColLoanDate, 10)

            .Col = ColInsttAmount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColInsttAmount, 10)

            .Col = ColBalance
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColBalance, 10)

        End With

        MainClass.ProtectCell(sprdLoan, 1, sprdLoan.MaxRows, 1, sprdLoan.MaxCols)
        sprdLoan.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
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

            .Col = ColLoanDate
            .Text = "Loan Date "

            .Col = ColLoanAmount
            .Text = "Loan Amount"

            .Col = ColInsttAmount
            .Text = "Installment Amount"

            .Col = ColBalance
            .Text = "Balance Amount"
        End With
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
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

        mSubTitle = "AS ON : " & lblYear.Text
        mTitle = "Loan Statement"
        Call ShowReport(SqlStr, "LoanStmt.Rpt", Mode, mTitle, mSubTitle)

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
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub
    Private Sub frmLoanStmt_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmLoanStmt_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7145)
        Me.Width = VB6.TwipsToPixelsX(11355)
        lblRunDate.Text = CStr(RunDate)
        OptName.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        FillCatgCombo()
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False
        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

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
        Dim mPaidAmount As Double
        Dim mBalance As Double
        Dim mLoanAmount As Double
        Dim mKey As String
        Dim mFirstLine As Boolean

        MainClass.ClearGrid(sprdLoan)

        '    If txtEmpCode.Text = "" Then
        '        MsgInformation "Please select the Employee Name."
        '        txtEmpCode.SetFocus
        '        Exit Sub
        '    End If

        SqlStr = " SELECT EMP.EMP_CODE, EMP.EMP_NAME, MKEY, " & vbCrLf & " MAX(LOAN.ADD_DEDUCTCODE) AS ADD_DEDUCTCODE, MAX(LOAN.LOANDATE) AS LOANDATE, " & vbCrLf & " MAX(LOAN.LOANAMOUNT) AS LOANAMOUNT, MAX(LOAN.INSTALMENTAMT) AS INSTALMENTAMT, " & vbCrLf & " SUM(LOAN.BALANCE_AMOUNT) AS BALANCE_AMOUNT, SUM(LOAN.PAID_AMOUNT) AS PAID_AMOUNT " & vbCrLf & " FROM PAY_LOAN_MST Loan,PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " LOAN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND LOAN.EMP_CODE=EMP.EMP_CODE" & vbCrLf & " AND LOAN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LOAN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " And LOAN.LOANDATE <= TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If chkBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " HAVING SUM(LOAN.PAID_AMOUNT)=0 "
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY EMP.EMP_CODE, EMP.EMP_NAME, MKEY"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME,LOANDATE,MKEY"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE,LOANDATE,MKEY"
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
                    mKey = IIf(IsDbNull(RsEmpLoan.Fields("mKey").Value), "", RsEmpLoan.Fields("mKey").Value)

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
                        If MainClass.ValidateWithMasterTable(RsEmpLoan.Fields("ADD_DEDUCTCODE"), "CODE", "NAME", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            .Text = MasterNo
                        End If
                        '                    .Text = CStr(IIf(IsNull(RsEmpLoan!LOANTYPE), "", RsEmpLoan!LOANTYPE))
                    Else
                        .Text = ""
                    End If


                    .Col = ColLoanDate
                    If mFirstLine = True Then
                        .Text = VB6.Format(IIf(IsDbNull(RsEmpLoan.Fields("LoanDate").Value), "", RsEmpLoan.Fields("LoanDate").Value), "MMM-YYYY")
                    Else
                        .Text = ""
                    End If

                    .Col = ColLoanAmount
                    mLoanAmount = IIf(IsDbNull(RsEmpLoan.Fields("LOANAMOUNT").Value), 0, RsEmpLoan.Fields("LOANAMOUNT").Value)
                    If mFirstLine = True Then
                        .Text = VB6.Format(IIf(IsDbNull(RsEmpLoan.Fields("LOANAMOUNT").Value), 0, RsEmpLoan.Fields("LOANAMOUNT").Value), "0.00")
                    Else
                        .Text = ""
                    End If

                    .Col = ColInsttAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsEmpLoan.Fields("InstalmentAmt").Value), 0, RsEmpLoan.Fields("InstalmentAmt").Value), "0.00")

                    .Col = ColBalance
                    mPaidAmount = mPaidAmount + IIf(IsDbNull(RsEmpLoan.Fields("PAID_AMOUNT").Value), 0, RsEmpLoan.Fields("PAID_AMOUNT").Value)
                    mBalance = mLoanAmount - mPaidAmount
                    .Text = VB6.Format(mBalance, "0.00")

                    mFirstLine = False
                    RsEmpLoan.MoveNext()
                    If Not RsEmpLoan.EOF Then
                        If mKey <> IIf(IsDbNull(RsEmpLoan.Fields("mKey").Value), "", RsEmpLoan.Fields("mKey").Value) Then
                            mBalance = 0
                            mPaidAmount = 0
                            mFirstLine = True
                        End If
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
                MainClass.ProtectCell(sprdLoan, 0, .MaxRows, 0, .MaxCols)
            End With
        End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillCatgCombo()

        On Error GoTo ErrPart
        Dim RsDept As ADODB.Recordset = Nothing

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCategory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCategory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCategory.SelectedIndex = 0

        '    cboCategory.Clear
        '    cboCategory.AddItem "General Staff"
        '    cboCategory.AddItem "Production Staff"
        '    cboCategory.AddItem "Export Staff"
        '    cboCategory.AddItem "Regular Worker"
        '    cboCategory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCategory.AddItem "Director"
        '    cboCategory.AddItem "Trainee Staff"
        '    cboCategory.ListIndex = 0
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(lblYear.Text, "mm"), VB6.Format(lblYear.Text, "yyyy"))
    End Sub
End Class
