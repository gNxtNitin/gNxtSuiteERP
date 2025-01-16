Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTaxDeductionReg
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
    Private Const ColDesg As Short = 3
    Private Const ColBankAcct As Short = 4
    Private Const ColPaymentMode As Short = 5
    Private Const ColTaxApr As Short = 6
    Private Const ColTaxMay As Short = 7
    Private Const ColTaxJun As Short = 8
    Private Const ColTaxJul As Short = 9
    Private Const ColTaxAug As Short = 10
    Private Const ColTaxSep As Short = 11
    Private Const ColTaxOct As Short = 12
    Private Const ColTaxNov As Short = 13
    Private Const ColTaxDec As Short = 14
    Private Const ColTaxJan As Short = 15
    Private Const ColTaxFeb As Short = 16
    Private Const ColTaxMar As Short = 17
    Private Const ColTotalPaid As Short = 18
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAddDeduct
            .MaxCols = ColTotalPaid
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 6)
            .TypeMaxEditLen = 5000

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 18)
            .TypeMaxEditLen = 5000

            .ColsFrozen = ColName

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDesg, 18)
            .ColHidden = True

            .Col = ColBankAcct
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankAcct, 10)
            .ColHidden = True

            .Col = ColPaymentMode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColBankAcct, 10)
            .ColHidden = True


            For cntCol = ColTaxApr To ColTotalPaid
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 9)
            Next

        End With

        Call FillHeading()
        MainClass.ProtectCell(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 1, sprdAddDeduct.MaxCols)
        MainClass.SetSpreadColor(sprdAddDeduct, mRow)
        sprdAddDeduct.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()
        With sprdAddDeduct
            .MaxCols = ColTotalPaid
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColBankAcct
            .Text = "Bank Account"

            .Col = ColPaymentMode
            .Text = "Payment Mode"

            .Col = ColTaxApr
            .Text = "April"

            .Col = ColTaxMay
            .Text = "May"

            .Col = ColTaxJun
            .Text = "June"

            .Col = ColTaxJul
            .Text = "July"

            .Col = ColTaxAug
            .Text = "August"

            .Col = ColTaxSep
            .Text = "September"

            .Col = ColTaxOct
            .Text = "October"

            .Col = ColTaxNov
            .Text = "November"

            .Col = ColTaxDec
            .Text = "December"

            .Col = ColTaxJan
            .Text = "January"

            .Col = ColTaxFeb
            .Text = "February"

            .Col = ColTaxMar
            .Text = "March"

            .Col = ColTotalPaid
            .Text = "Total Tax Paid"
        End With
    End Sub
    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        Call PrintCommand(False)
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
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

        Dim mRptFileName As String
        Dim mBankName As String
        Dim mChequeNo As String
        Dim mChequeDate As String
        Dim mChequeAmount As String


        'Insert Data from Grid to PrintDummyData Table...

        Call MainClass.ClearCRptFormulas(Report1)

        mTitle = "Tax Deduction Register"

        mRptFileName = "TaxDedReg.rpt"
        'FillPrintDummyData(sprdAddDeduct, 0, sprdAddDeduct.MaxRows, ColCard, sprdAddDeduct.MaxCols, PubDBCn) = False Then GoTo Err1
        If FillPrintDummyData(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 0, sprdAddDeduct.MaxCols, PubDBCn) = False Then GoTo ERR1

        mSubTitle = "FROM : " & txtFrom.Text & " To " & txtTo.Text



        'Select Record for print...

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
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
    Private Function GetTotalAmount() As Double
        On Error GoTo ErrPart1
        Dim cntRow As Integer
        Dim mAmount As Double
        With sprdAddDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColTotalPaid
                mAmount = mAmount + IIf(IsNumeric(.Text), .Text, 0)
            Next
        End With
        GetTotalAmount = mAmount
        Exit Function

ErrPart1:
        GetTotalAmount = 0
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        MainClass.ClearGrid(sprdAddDeduct)
        RefreshScreen()
        CalcSubTotal()
        FormatSprd(-1)
    End Sub
    Private Sub CalcSubTotal()

        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim arrsal() As Double

        Call MainClass.AddBlankfpSprdRow(sprdAddDeduct, ColName)

        ReDim arrsal(sprdAddDeduct.MaxCols)

        With sprdAddDeduct
            .Row = .MaxRows
            .Col = ColName
            .Font = VB6.FontChangeBold(.Font, True)
            .Text = "GRAND TOTAL"

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                For cntCol = ColTaxApr To ColTotalPaid
                    .Col = cntCol
                    arrsal(cntCol) = arrsal(cntCol) + Val(sprdAddDeduct.Text)
                Next
            Next

            .Row = .MaxRows
            For cntCol = ColTaxApr To ColTotalPaid
                .Col = cntCol
                sprdAddDeduct.Text = CStr(arrsal(cntCol))
                .Font = VB6.FontChangeBold(.Font, True)
            Next


        End With

    End Sub
    Private Sub frmTaxDeductionReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmTaxDeductionReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        OptName.Checked = True
        FillHeading()
        FillDeptCombo()
        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim mDeptCode As String
        Dim mBonusPer As Double

        MainClass.ClearGrid(sprdAddDeduct)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mBonusPer = Val(IIf(IsDbNull(RsCompany.Fields("BonusLimit").Value), 0, RsCompany.Fields("BonusLimit").Value))

        SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, MAX(TRN.DESG_DESC) AS DESG_DESC, " & vbCrLf & " EMP.EMP_BANK_NO, DECODE(EMP.PAYMENTMODE,1,'CASH','CHEQUE') AS PAYMENTMODE, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS April," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS May, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS June, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS July, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS August, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS September, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS October, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS November, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS December, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS January, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS February, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN TRN.PAYABLEAMOUNT ELSE 0 END)) AS March, " & vbCrLf & " TO_CHAR(SUM(TRN.PAYABLEAMOUNT)) AS TaxPaid " & vbCrLf & " From PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST SALHEAD, PAY_SAL_TRN TRN " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE " & vbCrLf & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf & " AND SALHEAD.TYPE=" & ConIncomeTax & "" & vbCrLf & " AND TRN.SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TRN.SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND EMP_STOP_SALARY='N' "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " HAVING Sum (TRN.PAYABLEAMOUNT)>0"

        SqlStr = SqlStr & vbCrLf & " GROUP by EMP.EMP_CODE, EMP.EMP_NAME,EMP.EMP_BANK_NO, EMP.PAYMENTMODE"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_CODE"
        End If

        MainClass.AssignDataInSprd8(SqlStr, sprdAddDeduct, StrConn, "Y")
        Call PrintCommand(True)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        cboDept.Items.Clear()

        SqlStr = "Select DEPT_DESC from PAY_DEPT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function FillDataInSprd(ByRef mCode As Integer, ByRef mRow As Integer, ByRef mEmpCode As String, ByRef mEmpName As String, ByRef mBankAcct As String, ByRef mPaymentMode As String) As Boolean

        'Dim RsEmpSal As ADODB.Recordset
        'Dim mStartYM As Long
        'Dim mEndYM As Long
        'Dim mSalYM As Long
        'Dim mYM As String
        'Dim mEarn As Double
        'Dim mDeduct As Double
        'Dim mNetPay As Double
        'Dim mBasicSalary As Double
        'Dim mTotBasic As Double
        'Dim mBPayable As Double
        'Dim mTotBonus As Double
        'Dim mBonusRate As Double
        'Dim mBonuscalc As Double
        'Dim mDepartment As String
        'Dim mActualBasicSal As Double
        '
        'Dim mPeriod As String
        '
        '
        '    FillDataInSprd = False
        '
        '    mStartYM = Year(txtFrom.Text) & vb6.Format(Month(txtFrom.Text), "00")
        '    mEndYM = Year(txtTo.Text) & vb6.Format(Month(txtTo.Text), "00")
        '
        '
        '    mYM = "YM BETWEEN " & mStartYM & " AND " & mEndYM & ""
        '
        '    SqlStr = " SELECT * " & vbCrLf _
        ''        & " FROM SALTRN WHERE" & vbCrLf _
        ''        & " EMPCODE =" & mCode & " AND  " & vbCrLf _
        ''        & " " & mYM & " And ISARREAR='N' AND " & vbCrLf _
        ''        & " COMPANYCODE =" & RsCompany!CompanyCode & ""
        '
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY YM"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsEmpSal, adLockOptimistic
        '
        '    sprdAddDeduct.Row = mRow
        '
        '    If RsEmpSal.EOF = False Then
        '        sprdAddDeduct.Row = mRow
        '        mYM = RsEmpSal!YM
        '        Do While Not RsEmpSal.EOF
        '            mSalYM = RsEmpSal!YM
        '            mBasicSalary = RsEmpSal!PAYABLESALARY
        '            mPeriod = Mid(MonthName(RsEmpSal!SalMONTH), 1, 3) & ", " & RsEmpSal!SALYEAR
        '            mDepartment = IIf(IsNull(RsEmpSal!Department), "", RsEmpSal!Department)
        '
        '            mActualBasicSal = RsEmpSal!BASICSALARY
        '
        '            RsEmpSal.MoveNext
        '
        '             If chkTypeAll.Value = vbUnchecked Then
        '                If cboType.ListIndex = 0 Then
        '                    If mActualBasicSal > IIf(IsNull(RsCompany!BonusLimit), 3500, RsCompany!BonusLimit) & "" Then
        '                        GoTo NextRecset
        '                    End If
        '                ElseIf cboType.ListIndex = 1 Then
        '                    If mActualBasicSal <= IIf(IsNull(RsCompany!BonusLimit), 3500, RsCompany!BonusLimit) & "" Then
        '                        GoTo NextRecset
        '                    End If
        '                End If
        '            End If
        '
        '            If Not RsEmpSal.EOF Then
        '                If mSalYM = RsEmpSal!YM Then
        '                    GoTo NextRecset
        '                End If
        '            End If
        '            With sprdAddDeduct
        '                FillDataInSprd = True
        '                .Col = ColCode
        '                .Text = mEmpCode
        '
        '                .Col = ColName
        '                .Text = mEmpName
        '
        '                .Col = ColBankAcct
        '                .Text = mBankAcct
        '
        '                 .Col = ColPaymentMode
        '                .Text = mPaymentMode
        '
        '                .Col = ColPeriod
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & mPeriod
        '
        '                .Col = ColBasic
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBasicSalary)
        '                mTotBasic = mTotBasic + mBasicSalary
        '
        '                mBPayable = CalcBonusPayable(mCode, mBasicSalary, mBonuscalc, mBonusRate, mDepartment)
        '
        '                .Col = ColTaxCalc
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBonuscalc)
        '
        '                .Col = ColTaxRate
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBonusRate)
        '
        '                .Col = ColTaxPayable
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) & MainClass.FormatRupees(mBPayable)
        '                mTotBonus = mTotBonus + (mBPayable)
        '
        '                .Col = ColTotalBonus
        '
        '
        '            End With
        'NextRecset:
        '        Loop
        '
        '        If FillDataInSprd = True Then
        '            With sprdAddDeduct
        '                .Col = ColPeriod
        '                .Text = .Text + Chr(13) + Chr(13) & "Total :"
        '
        '                .Col = ColBasic
        '                .Text = .Text + Chr(13) + Chr(13) & MainClass.FormatRupees(mTotBasic)
        '
        '                .Col = ColTaxPayable
        '                .Text = .Text + Chr(13) + Chr(13) & MainClass.FormatRupees(mTotBonus)
        '
        '                .Col = ColTotalBonus
        '                .Text = MainClass.FormatRupees(mTotBonus)
        '            End With
        '        End If
        '        sprdAddDeduct.RowHeight(mRow) = sprdAddDeduct.MaxTextRowHeight(mRow)
        '    End If
    End Function

    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub

    Private Sub txtFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrom.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        ElseIf FYChk((txtFrom.Text)) = False Then
            Cancel = True
        End If
        txtFrom.Text = VB6.Format(txtFrom.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTo.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        ElseIf FYChk((txtTo.Text)) = False Then
            Cancel = True
        End If
        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
