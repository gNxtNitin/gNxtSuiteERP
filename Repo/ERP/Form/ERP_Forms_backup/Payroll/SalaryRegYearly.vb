Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalaryRegYearly
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    Private Const ColCard As Short = 1
    Private Const ColName As Short = 2
    Private Const ColSalMonth As Short = 3
    Private Const ColPaymentType As Short = 4
    Private Const ColDept As Short = 5
    Private Const ColDesg As Short = 6
    Private Const ColDOJ As Short = 7
    Private Const ColBankNo As Short = 8
    Private Const ColDays As Short = 9
    Private Const ColBSalary As Short = 10
    Private Const ColPSalary As Short = 11

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer
        Dim SqlStrCond As String

        MainClass.ClearGrid(sprdAttn)

        With sprdAttn
            .MaxCols = ColPSalary

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Col = ColSNo
            .Text = "S. No."

            .Col = ColCard
            .Text = "Emp Card No"

            .Col = ColPaymentType
            .Text = "Payment Type"

            .Col = ColBankNo
            .Text = "Bank A/c No."

            .Col = ColDept
            .Text = "Department"

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColDOJ
            .Text = "DoJ"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColSalMonth
            .Text = "Salary Month "

            .Col = ColDays
            .Text = "Working Days"

            .Col = ColBSalary
            .Text = "Basic Salary"

            .Col = ColPSalary
            .Text = "Payable Salary"

            SqlStr = " SELECT NAME,ADDDEDUCT FROM " & vbCrLf & " PAY_SALARYHEAD_MST WHERE "

            SqlStrCond = SqlStrCond & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

            If optShow(0).Checked = True Then ''chkPerksHead = vbUnchecked
                SqlStrCond = SqlStrCond & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"
            ElseIf optShow(1).Checked = True Then
                SqlStrCond = SqlStrCond & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND PAYMENT_TYPE='M'"
            ElseIf optShow(2).Checked = True Then
                SqlStrCond = SqlStrCond & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND PAYMENT_TYPE='M'"
            End If

            SqlStr = SqlStr & vbCrLf & SqlStrCond
            SqlStr = SqlStr & vbCrLf & " ORDER BY ADDDEDUCT,SEQ "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                .MaxCols = .MaxCols + 2 * (MainClass.GetMaxRecord("PAY_SALARYHEAD_MST", PubDBCn, SqlStrCond)) + IIf(optShow(0).Checked = True, 1, 0) ''IIf(chkPerksHead = vbUnchecked, 1, 0)
                cntCol = 1
                Do While Not RsTemp.EOF
                    .Col = ColPSalary + cntCol
                    .Text = "RATE-" & RsTemp.Fields("Name").Value
                    .ColHidden = True
                    cntCol = cntCol + 1

                    .Col = ColPSalary + cntCol
                    .Text = RsTemp.Fields("Name").Value
                    mAddDeduct = RsTemp.Fields("ADDDEDUCT").Value

                    RsTemp.MoveNext()
                    cntCol = cntCol + 1
                    If Not RsTemp.EOF Then
                        If RsTemp.Fields("ADDDEDUCT").Value <> mAddDeduct Then
                            .Col = ColPSalary + cntCol
                            .Text = "Total Payable"

                            cntCol = cntCol + 1
                        End If
                    End If
                Loop

                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "Total Deduction"
                .ColHidden = IIf(optShow(0).Checked = True, False, True) '' IIf(chkPerksHead = vbUnchecked, False, True)
            End If

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Net Salary"

            FormatSprd(-1)
        End With
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSalary(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForSalary(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim ColStartRow As Integer
        Dim ColEndRow As Integer
        Dim CntRow As Integer
        Dim mBankName As String
        Dim mRptFileName As String

        PubDBCn.Errors.Clear()


        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""


        If FillPrintDummyData(sprdAttn, 0, sprdAttn.MaxRows, ColCard, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mRptFileName = "SalRegYearly.Rpt"

        mTitle = "Salary Register (Yearly)"



        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        'Resume
    End Sub
    Private Function FetchRecordForSalReg(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM TEMP_SALREG_TRN " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW "

        FetchRecordForSalReg = mSqlStr
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1

    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSalary(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        '    RefreshScreen
        If optShow(0).Checked = True Then '' chkPerksHead.Value = vbUnchecked
            RefreshScreen()
        Else
            RefreshScreenPerks()
        End If
    End Sub
    Private Sub frmSalaryRegYearly_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmSalaryRegYearly_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        FillHeading()

        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mAddDeduct As Integer
        Dim mPayableSalary As Double
        Dim mTotPayable As Double
        Dim mTotDeduct As Double
        Dim mNetSalary As Double
        Dim ColPayableAmount As Integer
        Dim ColDeductionAmount As Integer
        Dim mTotalMonth As Integer
        Dim mSalMonth As String
        Dim mSalNextMonth As String

        Dim mIsArrear As String
        Dim mIsNextArrear As String

        mTotalMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtFrom.Text), CDate(txtTo.Text))

        FillHeading()

        For cntCol = ColBSalary To sprdAttn.MaxCols
            sprdAttn.Row = 0
            sprdAttn.Col = cntCol
            If Trim(sprdAttn.Text) = "Total Payable" Then
                ColPayableAmount = cntCol
            End If

            If Trim(sprdAttn.Text) = "Total Deduction" Then
                ColDeductionAmount = cntCol
            End If
        Next


        SqlStr = " SELECT SALTRN.*, EMP.EMP_NAME, EMP.EMP_DOJ, EMP.EMP_FNAME, " & vbCrLf & " ADD_DEDUCT.NAME AS ADDNAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.SALHEADCODE =ADD_DEDUCT.CODE " & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"


        '    SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='N' "

        If optShow(0).Checked = True Then '' chkPerksHead.Value = vbUnchecked
            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND PAYMENT_TYPE='M'"
        End If

        If chkAdvance.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.TYPE NOT IN (" & ConAdvance & ", " & ConImprest & ", " & ConLoan & ") "
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE, SALTRN.ISARREAR "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                CntRow = 1

                Do While Not RsAttn.EOF
                    .MaxRows = CntRow
                    .Row = CntRow

                    mTotPayable = 0
                    mTotDeduct = 0
                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    '                If mCode = "000089" Then MsgBox RsAttn!EMP_CODE
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColSalMonth
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("SAL_DATE").Value), "", RsAttn.Fields("SAL_DATE").Value), "MMMM,YYYY")

                    If RsAttn.Fields("IsArrear").Value = "Y" Then
                        .Text = .Text & " - Arrear"
                    ElseIf RsAttn.Fields("IsArrear").Value = "O" Then
                        .Text = .Text & " - Others"
                    ElseIf RsAttn.Fields("IsArrear").Value = "F" Then
                        .Text = .Text & " - F & F"
                    End If

                    .Col = ColPaymentType
                    .Text = IIf(RsAttn.Fields("PAYMENTMODE").Value = "1", "Cash", "Cheque")

                    .Col = ColBankNo
                    .Text = IIf(IsDbNull(RsAttn.Fields("BANKACCTNO").Value), "", RsAttn.Fields("BANKACCTNO").Value)

                    .Col = ColDept
                    .Text = IIf(IsDbNull(RsAttn.Fields("Department").Value), "", RsAttn.Fields("Department").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDbNull(RsAttn.Fields("DESG_DESC").Value), "", RsAttn.Fields("DESG_DESC").Value)

                    .Col = ColDOJ
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("EMP_DOJ").Value), "", RsAttn.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                    .Col = ColDays
                    .Text = CStr(RsAttn.Fields("WDAYS").Value)

                    .Col = ColBSalary
                    .Text = MainClass.FormatRupees(RsAttn.Fields("BASICSALARY"))

                    .Col = ColPSalary
                    .Text = VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00")
                    mPayableSalary = CDbl(VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00"))
                    mTotPayable = mPayableSalary * IIf(optShow(0).Checked = True, 1, 0) ''IIf(chkPerksHead = vbUnchecked, 1, 0)

                    mSalMonth = IIf(IsDbNull(RsAttn.Fields("SAL_DATE").Value), "", RsAttn.Fields("SAL_DATE").Value)
                    mSalNextMonth = IIf(IsDbNull(RsAttn.Fields("SAL_DATE").Value), "", RsAttn.Fields("SAL_DATE").Value)

                    mIsArrear = IIf(IsDbNull(RsAttn.Fields("IsArrear").Value), "", RsAttn.Fields("IsArrear").Value)
                    mIsNextArrear = IIf(IsDbNull(RsAttn.Fields("IsArrear").Value), "", RsAttn.Fields("IsArrear").Value)

                    Do While mSalMonth = mSalNextMonth And mIsArrear = mIsNextArrear
                        For cntCol = ColBSalary To .MaxCols
                            .Row = 0
                            .Col = cntCol
                            If Trim(UCase(.Text)) = "RATE-" & Trim(UCase(RsAttn.Fields("ADDNAME").Value)) Then
                                .Row = CntRow
                                .Col = cntCol
                                .Text = MainClass.FormatRupees(IIf(IsDBNull(RsAttn.Fields("ACTUALAMOUNT").Value), 0, RsAttn.Fields("ACTUALAMOUNT").Value))

                                cntCol = cntCol + 1

                                .Col = cntCol
                                .Text = MainClass.FormatRupees(IIf(IsDBNull(RsAttn.Fields("PayableAmount").Value), 0, RsAttn.Fields("PayableAmount").Value))

                                If RsAttn.Fields("ADDDEDUCT").Value = ConEarning Or RsAttn.Fields("ADDDEDUCT").Value = ConPerks Then
                                    mTotPayable = mTotPayable + IIf(IsDBNull(RsAttn.Fields("PayableAmount").Value), 0, RsAttn.Fields("PayableAmount").Value)
                                ElseIf RsAttn.Fields("ADDDEDUCT").Value = ConDeduct Then
                                    mTotDeduct = mTotDeduct + IIf(IsDBNull(RsAttn.Fields("PayableAmount").Value), 0, RsAttn.Fields("PayableAmount").Value)
                                End If
                                Exit For
                            End If
                        Next

                        RsAttn.MoveNext()
                        If RsAttn.EOF = True Then
                            Exit Do
                        Else
                            mSalNextMonth = IIf(IsDbNull(RsAttn.Fields("SAL_DATE").Value), "", RsAttn.Fields("SAL_DATE").Value)
                            mIsNextArrear = IIf(IsDbNull(RsAttn.Fields("IsArrear").Value), "", RsAttn.Fields("IsArrear").Value)
                        End If
                    Loop

                    .Row = CntRow
                    If optShow(0).Checked = True Then ''chkPerksHead = vbUnchecked
                        .Col = ColPayableAmount
                        .Text = MainClass.FormatRupees(mTotPayable)

                        .Col = ColDeductionAmount
                        .Text = MainClass.FormatRupees(mTotDeduct)
                    End If

                    .Col = .MaxCols
                    mNetSalary = (mTotPayable - mTotDeduct)
                    .Text = VB6.Format(mNetSalary, "0")
                    CntRow = CntRow + 1
                Loop

                ColTotal(sprdAttn, ColBSalary, .MaxCols)
                .Col = ColName
                .Row = .MaxRows
                .Text = "TOTAL :"
                MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            End With
            Call PrintCommand(True)
        Else
            MsgInformation("Salary Not Processed For This Period ..." & vbNewLine & vbNewLine & "Please Process Salary .")
        End If
        FormatSprd(-1)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub

    Private Sub RefreshScreenPerks()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mAddDeduct As Integer
        Dim mPayableSalary As Double
        Dim mTotPayable As Double
        Dim mTotDeduct As Double
        Dim mNetSalary As Double
        Dim ColPayableAmount As Integer
        Dim ColDeductionAmount As Integer
        Dim mTotalMonth As Integer
        Dim mSalMonth As String
        Dim mSalNextMonth As String

        Dim mIsArrear As String
        Dim mIsNextArrear As String
        Dim mPaidDay As Integer
        Dim mNextPaidDay As Integer

        mTotalMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtFrom.Text), CDate(txtTo.Text))

        FillHeading()

        For cntCol = ColBSalary To sprdAttn.MaxCols
            sprdAttn.Row = 0
            sprdAttn.Col = cntCol
            If Trim(sprdAttn.Text) = "Total Payable" Then
                ColPayableAmount = cntCol
            End If

            If Trim(sprdAttn.Text) = "Total Deduction" Then
                ColDeductionAmount = cntCol
            End If
        Next


        SqlStr = " SELECT SALTRN.*, EMP.EMP_NAME, EMP.EMP_DOJ, EMP.EMP_FNAME, " & vbCrLf & " ADD_DEDUCT.NAME AS ADDNAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ,EMP_DEPT_CODE,EMP_DESG_CODE, EMP_BANK_NO" & vbCrLf & " FROM PAY_PERKS_TRN SALTRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.ADD_DEDUCTCODE =ADD_DEDUCT.CODE " & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & ""

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE IN ('O','S','A','F','Z','V')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE IN ('P')"
        End If

        If chkAdvance.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.TYPE NOT IN (" & ConAdvance & ", " & ConImprest & ", " & ConLoan & ") "
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE, PAID_WEEK, BOOKTYPE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                CntRow = 1

                Do While Not RsAttn.EOF
                    .MaxRows = CntRow
                    .Row = CntRow

                    mTotPayable = 0
                    mTotDeduct = 0
                    mPaidDay = 0


                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    '                If mCode = "000089" Then MsgBox RsAttn!EMP_CODE
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColSalMonth
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("SAL_DATE").Value), "", RsAttn.Fields("SAL_DATE").Value), "MMMM,YYYY") & " - " & IIf(IsDbNull(RsAttn.Fields("PAID_WEEK").Value), "", RsAttn.Fields("PAID_WEEK").Value)

                    If RsAttn.Fields("BOOKTYPE").Value = "A" Then
                        .Text = .Text & " - Arrear"
                        '                ElseIf RsAttn!IsArrear = "O" Then
                        '                    .Text = .Text & " - Others"
                    End If

                    .Col = ColPaymentType
                    .Text = IIf(IsDbNull(RsAttn.Fields("PAYMENT_TYPE").Value), "", IIf(RsAttn.Fields("PAYMENT_TYPE").Value = "1", "Cash", "Cheque"))

                    .Col = ColBankNo
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_BANK_NO").Value), "", RsAttn.Fields("EMP_BANK_NO").Value)

                    .Col = ColDept
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_DESG_CODE").Value), "", RsAttn.Fields("EMP_DESG_CODE").Value)

                    .Col = ColDOJ
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("EMP_DOJ").Value), "", RsAttn.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                    .Col = ColDays
                    .Text = "" ''CStr(RsAttn!WDAYS)

                    .Col = ColBSalary
                    .Text = "" ''MainClass.FormatRupees(RsAttn!BASICSALARY)

                    .Col = ColPSalary
                    .Text = "" ''Format(RsAttn!PAYABLESALARY, "0.00")
                    '                mPayableSalary = Format(RsAttn!PAYABLESALARY, "0.00")
                    '                mTotPayable = mPayableSalary * IIf(chkPerksHead = vbUnchecked, 1, 0)

                    mSalMonth = IIf(IsDbNull(RsAttn.Fields("SAL_DATE").Value), "", RsAttn.Fields("SAL_DATE").Value)
                    mSalNextMonth = IIf(IsDbNull(RsAttn.Fields("SAL_DATE").Value), "", RsAttn.Fields("SAL_DATE").Value)

                    mPaidDay = IIf(IsDbNull(RsAttn.Fields("PAID_WEEK").Value), "", RsAttn.Fields("PAID_WEEK").Value)
                    mNextPaidDay = IIf(IsDbNull(RsAttn.Fields("PAID_WEEK").Value), "", RsAttn.Fields("PAID_WEEK").Value)

                    mIsArrear = IIf(IsDbNull(RsAttn.Fields("BOOKTYPE").Value), "", RsAttn.Fields("BOOKTYPE").Value)
                    mIsNextArrear = IIf(IsDbNull(RsAttn.Fields("BOOKTYPE").Value), "", RsAttn.Fields("BOOKTYPE").Value)

                    Do While mSalMonth = mSalNextMonth And mIsArrear = mIsNextArrear And mPaidDay = mNextPaidDay
                        For cntCol = ColBSalary To .MaxCols
                            .Row = 0
                            .Col = cntCol
                            If Trim(UCase(.Text)) = "RATE-" & Trim(UCase(RsAttn.Fields("ADDNAME").Value)) Then
                                .Row = CntRow
                                .Col = cntCol
                                .Text = MainClass.FormatRupees(RsAttn.Fields("Amount"))

                                cntCol = cntCol + 1

                                .Col = cntCol
                                .Text = MainClass.FormatRupees(RsAttn.Fields("Amount"))

                                If RsAttn.Fields("ADDDEDUCT").Value = ConEarning Or RsAttn.Fields("ADDDEDUCT").Value = ConPerks Then
                                    mTotPayable = mTotPayable + RsAttn.Fields("Amount").Value
                                ElseIf RsAttn.Fields("ADDDEDUCT").Value = ConDeduct Then
                                    mTotDeduct = mTotDeduct + RsAttn.Fields("Amount").Value
                                End If
                                Exit For
                            End If
                        Next

                        RsAttn.MoveNext()
                        If RsAttn.EOF = True Then
                            Exit Do
                        Else
                            mSalNextMonth = IIf(IsDbNull(RsAttn.Fields("SAL_DATE").Value), "", RsAttn.Fields("SAL_DATE").Value)
                            mIsNextArrear = IIf(IsDbNull(RsAttn.Fields("BOOKTYPE").Value), "", RsAttn.Fields("BOOKTYPE").Value)
                            mNextPaidDay = IIf(IsDbNull(RsAttn.Fields("PAID_WEEK").Value), "", RsAttn.Fields("PAID_WEEK").Value)
                        End If
                    Loop

                    .Row = CntRow
                    If optShow(0).Checked = True Then ''chkPerksHead = vbUnchecked Then
                        .Col = ColPayableAmount
                        .Text = MainClass.FormatRupees(mTotPayable)

                        .Col = ColDeductionAmount
                        .Text = MainClass.FormatRupees(mTotDeduct)
                    End If

                    .Col = .MaxCols
                    mNetSalary = (mTotPayable - mTotDeduct)
                    .Text = VB6.Format(mNetSalary, "0")
                    CntRow = CntRow + 1
                Loop

                ColTotal(sprdAttn, ColBSalary, .MaxCols)
                .Col = ColName
                .Row = .MaxRows
                .Text = "TOTAL :"
                MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            End With
            Call PrintCommand(True)
        Else
            MsgInformation("Salary Not Processed For This Period ..." & vbNewLine & vbNewLine & "Please Process Salary .")
        End If
        FormatSprd(-1)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAttn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .set_ColWidth(ColSNo, 5)

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCard, 7)
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 15)
            .ColHidden = True

            .Col = ColSalMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColSalMonth, 15)

            .Col = ColPaymentType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColPaymentType, 7)
            .ColHidden = True

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 9)
            .ColHidden = True

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesg, 9)
            .ColHidden = True

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDOJ, 9)
            .ColHidden = True

            .Col = ColBankNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBankNo, 7)
            .ColHidden = True

            .Col = ColDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDays, 6)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            If optShow(0).Checked = True Then
                .ColsFrozen = ColBSalary
            Else
                .ColsFrozen = ColSalMonth
            End If
            For cntCol = ColBSalary To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)

                If cntCol = ColBSalary Or cntCol = ColPSalary Then
                    .ColHidden = IIf(optShow(0).Checked = True, False, True)
                End If
            Next
        End With

        MainClass.ProtectCell(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxRows)
        sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' OperationModeSingle
        MainClass.SetSpreadColor(sprdAttn, mRow)

        Exit Sub
ERR1:

        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function CheckCalcOnBasic(ByRef mSalHead As String) As Boolean
        On Error GoTo CheckCalcOnBasicErr
        Dim SqlStr As String = ""
        CheckCalcOnBasic = False
        If MainClass.ValidateWithMasterTable(mSalHead, "Name", "CALC_ON", "Add_Deduct", PubDBCn, MasterNo) = True Then
            If MasterNo <> ConCalcVariable Then
                CheckCalcOnBasic = True
            End If
        End If
        Exit Function
CheckCalcOnBasicErr:
        MsgBox(Err.Description)
        CheckCalcOnBasic = False
    End Function

    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        cmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub

    Private Sub frmSalaryRegYearly_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdAttn.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            TxtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            TxtName.Text = AcName
        End If
    End Sub
End Class
