Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLoanMaster
    Inherits System.Windows.Forms.Form
    Private XRIGHT As String
    Private ADDMode As Boolean
    Private MODIFYMode As Boolean
    Private FormLoaded As Boolean
    'Private PvtDBCn As ADODB.Connection

    Private Const ColMonth As Short = 1
    Private Const ColYear As Short = 2
    Private Const ColOPPrincipal As Short = 3
    Private Const ColInterest As Short = 4
    Private Const ColPrincipal As Short = 5
    Private Const ColMonthlyInstal As Short = 6
    Private Const ColBalAmt As Short = 7
    Private Const ColPaidAmt As Short = 8
    Private Const ColChk As Short = 9

    Private Const ConRowHeight As Short = 14
    Private Sub FillHeading()
        With SprdMain
            .Row = 0

            .Col = ColMonth
            .Text = "Instalment Month"

            .Col = ColYear
            .Text = "Instalment Year"

            .Col = ColOPPrincipal
            .Text = "Opening Principal"

            .Col = ColInterest
            .Text = "Interest"

            .Col = ColPrincipal
            .Text = "Principal Amount"

            .Col = ColMonthlyInstal
            .Text = "Instalment Amount"

            .Col = ColBalAmt
            .Text = "Loan Balance"

            .Col = ColPaidAmt
            .Text = "Paid Amount"

            .Col = ColChk

        End With
    End Sub
    Private Sub cboLoanType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLoanType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboLoanType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLoanType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        frmAtrn.lblLoanDetail.Text = "False"
        Me.Hide()
        Me.Dispose()
        FormLoaded = False
        frmAtrn.Refresh()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        '    txtAmtPerPeriod_Validate True
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        UpdateTempLoanDetail()
        Me.Hide()
        Me.Dispose()
        FormLoaded = False
        frmAtrn.Refresh()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim xAmount As Decimal

        FieldsVarification = True

        If CheckSalary() = True Then
            MsgInformation("Cann't Modify. Salary Made Agt. This Month.")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            TxtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Card No is empty. Cannot Save")
            txtEmpNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboLoanType.Text = "" Or cboLoanType.SelectedIndex = -1 Then
            MsgInformation("Loan Type is empty. Cannot Save")
            cboLoanType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        xAmount = IIf(IsNumeric(txtLoanAmt.Text), txtLoanAmt.Text, 0)
        If xAmount = 0 Then
            MsgInformation("Please enter vaild Loan Amount.")
            txtLoanAmt.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtLoanDate.Text) Then
            MsgInformation("Please Enter the Loan Date.")
            txtLoanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Val(txtAmtPerPeriod.Text) = 0 Then
            MsgInformation("Please Enter the Instalment Amount.")
            txtAmtPerPeriod.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If xAmount < Val(txtAmtPerPeriod.Text) Then
            MsgInformation("Instalment Amount is not greater than Loan Amount")
            txtAmtPerPeriod.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If optInterest(1).Checked = True Then
            If Val(txtRate.Text) <= 0 Then
                MsgInformation("Please enter the Rate of Interest.")
                txtRate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If txtLoanDate.Text <> "" Then
            If Val(Year(CDate(txtLoanDate.Text)) & VB6.Format(Month(CDate(txtLoanDate.Text)), "00")) > Val(VB6.Format(txtInstYear.Text, "0000") & VB6.Format(txtInstMonth.Text, "00")) Then
                MsgInformation("Loan Date Cann't be exceed than Instalment Date")
                FieldsVarification = False
                Exit Function
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        '    Resume
    End Function
    Private Function CheckSalary() As Boolean

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mMonth As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mMonth = "01" & VB6.Format(txtInstMonth.Text, "00") & VB6.Format(txtInstYear.Text, "0000")
        mMonth = VB6.Format(mMonth, "MMM-YYYY")

        SqlStr = "Select * From PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & txtEmpNo.Text & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSalTRN.EOF = False Then
            CheckSalary = True
        Else
            CheckSalary = False
        End If
        Exit Function

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckSalary = True
    End Function
    Private Function UpdateTempLoanDetail() As Object
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTempPRDetail As ADODB.Recordset
        Dim cntRow As Short
        Dim mMon As Integer
        Dim xAddDeductCode As Integer
        Dim xTypeCode As Integer
        Dim xMonth As Integer
        Dim xYear As Integer
        Dim xAmount As Double
        Dim xLOANAMOUNT As Double
        Dim xLOANDATE As String
        Dim mInterestCalc As String
        Dim TotLoanAmt As Double
        Dim mRate As Double
        Dim mDeductDate As String
        Dim mLoanType As String
        Dim mBalAmount As Double
        Dim mPaidAmount As Double

        Dim xOPPrincipal As Double
        Dim xInterest As Double
        Dim xPrincipal As Double
        Dim xMonthlyInstal As Double
        Dim xBalAmt As Double
        Dim xPaidAmt As Double


        xLOANAMOUNT = CDbl(txtLoanAmt.Text)

        mLoanType = VB.Left(Trim(cboLoanType.Text), 1)

        If xLOANAMOUNT = 0 Then Exit Function
        xLOANDATE = VB6.Format(txtLoanDate.Text, "DD-MMM-YYYY")

        '    If MainClass.ValidateWithMasterTable(cboLoanType, "NAME", "CODE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        xAddDeductCode = MasterNo
        '    End If

        xAddDeductCode = CInt("-1")

        If optInterest(1).Checked = True Then
            mInterestCalc = "Y"
            mRate = Val(txtRate.Text)
        Else
            mInterestCalc = "N"
            mRate = 0
        End If

        SqlStr = "DELETE TEMP_PAY_LOAN_MST  " & vbCrLf & " WHERE UserID='" & PubUserID & "'" & vbCrLf & " AND Emp_Code='" & Trim(txtEmpNo.Text) & "'"
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For cntRow = 1 To .MaxRows
                If TotLoanAmt >= xLOANAMOUNT Then Exit Function
                .Row = cntRow

                .Col = ColMonth
                For mMon = 1 To 12
                    If MonthName(mMon) = .Text Then
                        GoTo nextstep1
                    End If
                Next
nextstep1:
                xMonth = mMon

                .Col = ColYear
                xYear = Val(.Text)

                mDeductDate = "01/" & VB6.Format(xMonth, "00") & "/" & VB6.Format(xYear, "0000")

                .Col = ColOPPrincipal
                xOPPrincipal = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColInterest
                xInterest = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPrincipal
                xPrincipal = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColMonthlyInstal
                xMonthlyInstal = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColBalAmt
                xBalAmt = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPaidAmt
                xPaidAmt = IIf(IsNumeric(.Text), .Text, 0)


                '            .Col = ColAmt
                '            xAmount = IIf(IsNumeric(.Text), .Text, 0)
                '            TotLoanAmt = TotLoanAmt + xAmount
                '
                '            .Col = ColBalAmt
                '            mBalAmount = IIf(IsNumeric(.Text), .Text, 0)
                '
                '            .Col = ColPaidAmt
                '            mPaidAmount = IIf(IsNumeric(.Text), .Text, 0)
                '
                '
                '            If TotLoanAmt >= xLOANAMOUNT Then
                '                xAmount = xAmount - (TotLoanAmt - xLOANAMOUNT)
                '            End If

                SqlStr = " INSERT INTO TEMP_PAY_LOAN_MST ( " & vbCrLf & " USERID, EMP_CODE, SUBROWNO, " & vbCrLf & " LOANTYPE, LOANAMOUNT, LOANDATE," & vbCrLf & " INSTALMENTAMT, INTERESTCALC, INTERESTRATE," & vbCrLf & " DEDUCT_DATE, STARTINGMONTH, STARTINGYEAR, " & vbCrLf & " OPPRINCIPALAMT, INTERESTAMT, PRINCIPALAMT, " & vbCrLf & " DEDUCT_AMOUNT, BALANCE_AMOUNT, PAID_AMOUNT " & vbCrLf & " ) VALUES "

                SqlStr = SqlStr & vbCrLf & " ('" & PubUserID & "', '" & txtEmpNo.Text & "', " & cntRow & ", " & vbCrLf & " " & mLoanType & ", " & xLOANAMOUNT & ", TO_DATE('" & VB6.Format(xLOANDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(txtAmtPerPeriod.Text) & ", '" & mInterestCalc & "', " & Val(CStr(mRate)) & "," & vbCrLf & " TO_DATE('" & VB6.Format(mDeductDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & xMonth & ", " & xYear & ", " & vbCrLf & " " & xOPPrincipal & ", " & xInterest & ", " & xPrincipal & ", " & vbCrLf & " " & xMonthlyInstal & ", " & xBalAmt & ", " & xPaidAmt & ")"


                '            SqlStr = SqlStr & vbCrLf _
                ''                    & " ('" & PubUserID & "', '" & txtEmpNo.Text & "', " & cntRow & ", " & vbCrLf _
                ''                    & " " & xAddDeductCode & "," & mLoanType & ", " & vbCrLf _
                ''                    & " " & xLOANAMOUNT & ",TO_DATE('" & VB6.Format(xLOANDATE, "DD-MMM-YYYY") & "'), " & vbCrLf _
                ''                    & " TO_DATE('" & VB6.Format(mDeductDate, "DD-MMM-YYYY") & "'), " & vbCrLf _
                ''                    & xMonth & "," & xYear & "," & xAmount & ", " & vbCrLf _
                ''                    & " " & txtInstMonth.Text & ", " & txtInstYear.Text & " ," & vbCrLf _
                ''                    & " " & Val(txtAmtPerPeriod.Text) & ",'" & mInterestCalc & "', " & vbCrLf _
                ''                    & " " & Val(mRate) & ", " & Val(mBalAmount) & ", " & Val(mPaidAmount) & ") "

                PubDBCn.Execute(SqlStr)
NextEarnRow:
            Next
        End With

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Function
    Private Sub frmLoanMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        If FormLoaded = False Then

            Clear1()
            FormatSprdMain(-1)

            txtInstMonth.Text = VB6.Format(RunDate, "mm")
            txtInstYear.Text = VB6.Format(RunDate, "yyyy")
            FillCombo()
            Show1()

            FormLoaded = True
        End If
    End Sub

    Private Sub Clear1()

        txtAmtPerPeriod.Text = ""
        txtLoanAmt.Enabled = False
        txtLoanDate.Enabled = False
        'UpDInstMonth.Enabled = True
        'UpDInstYear.Enabled = True
        txtAmtPerPeriod.Enabled = True
        txtRate.Text = ""
        optInterest(0).Checked = True
        txtRate.Enabled = False
        cboLoanType.Enabled = True

        MainClass.ClearGrid(SprdMain, -1)
    End Sub
    Private Sub FillCombo()
        Dim RsCombo As ADODB.Recordset
        Dim SqlStr As String = ""

        cboLoanType.Items.Clear()
        '    SqlStr = " SELECT NAME FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND (TYPE= " & ConLoan & " OR TYPE= " & ConAdvance & ") ORDER BY NAME "
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsCombo, adLockOptimistic
        '
        '    If RsCombo.EOF = False Then
        '        Do While Not RsCombo.EOF
        '            cboLoanType.AddItem RsCombo!Name
        '            RsCombo.MoveNext
        '        Loop
        '    End If

        cboLoanType.Items.Add("1.ADVANCE AGT SALARY")
        cboLoanType.Items.Add("2.CAR LOAN")
        cboLoanType.Items.Add("3.HOUSING LOAN")
        cboLoanType.Items.Add("4.EDUCATION LOAN")
        cboLoanType.Items.Add("5.PERSONAL LOAN")

    End Sub

    Private Sub frmLoanMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetChildFormCordinate(Me)
        ADDMode = False
        MODIFYMode = False
        FormLoaded = False

        XRIGHT = "AMD"
        FormatSprdMain(-1)
        MainClass.SetControlsColor(Me)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1

        Dim RsTempLoan As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mLoanDate As String
        Dim mLoanAmt As Double
        Dim mCode As String
        Dim mMonth As String
        Dim mYear As String
        Dim mMon As Integer
        Dim xLoanMonth As Short
        Dim xLoanYear As Short
        Dim mPaidAmount As Double
        Dim mLoanType As Double

        MainClass.ClearGrid(SprdMain, ConRowHeight)

        SqlStr = "SELECT * From TEMP_PAY_LOAN_MST " & vbCrLf & " WHERE UserID='" & PubUserID & "'" & vbCrLf & " AND EMP_CODE = '" & txtEmpNo.Text & "'" & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempLoan, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempLoan.EOF = True Then Exit Sub
        FormatSprdMain(-1)

        With SprdMain
            If RsTempLoan.EOF = False Then
                .Row = .MaxRows
                txtInstMonth.Text = IIf(IsDBNull(RsTempLoan.Fields("StartingMonth").Value), "", RsTempLoan.Fields("StartingMonth").Value)
                txtInstYear.Text = IIf(IsDBNull(RsTempLoan.Fields("StartingYear").Value), "", RsTempLoan.Fields("StartingYear").Value)
                xLoanMonth = Month(RsTempLoan.Fields("LoanDate").Value)
                xLoanYear = Year(RsTempLoan.Fields("LoanDate").Value)
                txtAmtPerPeriod.Text = VB6.Format(IIf(IsDBNull(RsTempLoan.Fields("InstalmentAmt").Value), 0, RsTempLoan.Fields("InstalmentAmt").Value), "0.00")


                mLoanType = IIf(IsDBNull(RsTempLoan.Fields("LoanType").Value), "-1", RsTempLoan.Fields("LoanType").Value)
                cboLoanType.SelectedIndex = IIf(mLoanType > 5, 0, mLoanType - 1)

                If RsTempLoan.Fields("InterestCalc").Value = "Y" Then
                    optInterest(1).Checked = True
                Else
                    optInterest(0).Checked = True
                End If
                txtRate.Text = VB6.Format(IIf(IsDBNull(RsTempLoan.Fields("InterestRate").Value), 0, RsTempLoan.Fields("InterestRate").Value), "0.00")
                MainClass.ClearGrid(SprdMain, ConRowHeight)

                Do While Not RsTempLoan.EOF
                    .Row = .MaxRows

                    .Col = ColMonth
                    .Text = MonthName(Month(RsTempLoan.Fields("DEDUCT_DATE").Value))

                    .Col = ColYear
                    .Text = CStr(Year(RsTempLoan.Fields("DEDUCT_DATE").Value))

                    .Col = ColOPPrincipal
                    .Text = VB6.Format(IIf(IsDBNull(RsTempLoan.Fields("OPPrincipalAmt").Value), 0, RsTempLoan.Fields("OPPrincipalAmt").Value), "0.00")

                    .Col = ColInterest
                    .Text = VB6.Format(IIf(IsDBNull(RsTempLoan.Fields("InterestAmt").Value), 0, RsTempLoan.Fields("InterestAmt").Value), "0.00")

                    .Col = ColPrincipal
                    .Text = VB6.Format(IIf(IsDBNull(RsTempLoan.Fields("PrincipalAmt").Value), 0, RsTempLoan.Fields("PrincipalAmt").Value), "0.00")

                    .Col = ColMonthlyInstal
                    .Text = VB6.Format(IIf(IsDBNull(RsTempLoan.Fields("Deduct_Amount").Value), 0, RsTempLoan.Fields("Deduct_Amount").Value), "0.00")

                    .Col = ColBalAmt
                    .Text = VB6.Format(IIf(IsDBNull(RsTempLoan.Fields("Balance_Amount").Value), 0, RsTempLoan.Fields("Balance_Amount").Value), "0.00")

                    .Col = ColPaidAmt
                    .Text = VB6.Format(IIf(IsDBNull(RsTempLoan.Fields("Paid_Amount").Value), 0, RsTempLoan.Fields("Paid_Amount").Value), "0.00")

                    RsTempLoan.MoveNext()
                    If Not RsTempLoan.EOF Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop

                If mPaidAmount = 0 Then
                    txtAmtPerPeriod.Enabled = True
                    cboLoanType.Enabled = True
                Else
                    txtAmtPerPeriod.Enabled = False
                    cboLoanType.Enabled = False
                End If
                '            If Val(txtLoanAmt.Text) <> mLoanAmt Then
                '                .Row = .MaxRows
                '                .Col = ColCode
                '                mCode = .Text
                '
                '                .Col = ColMonth
                '                mMonth = .Text
                '
                '                .Col = ColYear
                '                mYear = .Text
                '
                '                .MaxRows = .MaxRows + 1
                '                .Row = .MaxRows
                '
                '                .Col = ColCode
                '                .Text = mCode
                '
                '                .Col = ColMonth
                '                mMon = 1
                '                Do While MonthName(mMon) <> mMonth
                '                    mMon = mMon + 1
                '                Loop
                '                .Text = MonthName(IIf(mMon = 12, 1, mMon + 1))
                '
                '                .Col = ColYear
                '                .Text = IIf(mMon = 12, mYear + 1, mYear)
                '
                '                .Col = ColAmt
                '                .Text = CStr(txtLoanAmt.Text - mLoanAmt)
                '            End If
            End If
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMonth, ColChk)
        End With
        Exit Sub
ERR1:
        Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub FormatSprdMain(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdMain

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)


            .Col = ColMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColMonth, 8)

            .Col = ColYear
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColYear, 7)

            For I = ColOPPrincipal To ColPaidAmt
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 8.5)
            Next

            .Col = ColChk
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColChk, 2)
            .ColHidden = True
        End With

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMonth, ColChk)

        MainClass.SetSpreadColor(SprdMain, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub optInterest_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optInterest.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optInterest.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            txtRate.Enabled = IIf(Index = 0, False, True)
            Call FillSprdMain()
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(frmAtrn.cmdSave, lblADDMode.Text, lblModifyMode.Text)
    End Sub



    Private Sub txtAmtPerPeriod_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmtPerPeriod.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmtPerPeriod_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmtPerPeriod.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAmtPerPeriod_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmtPerPeriod.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtAmtPerPeriod.Text = "" Then GoTo EventExitSub
        If Val(txtLoanAmt.Text) = 0 Then
            MsgInformation("Please enter the Loan Amount.")
            Cancel = True
            GoTo EventExitSub
        End If

        txtInstMonth_Validating(txtInstMonth, New System.ComponentModel.CancelEventArgs(True))
        txtInstYear_Validating(txtInstYear, New System.ComponentModel.CancelEventArgs(True))
        FillSprdMain()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtInstMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInstMonth.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInstMonth_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInstMonth.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInstMonth_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInstMonth.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtLoanAmt.Text) > 0 Then
            If Val(txtInstMonth.Text) <= 0 Then
                MsgInformation("Please Enter the Valid Month")
                Cancel = True
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInstYear_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInstYear.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtLoanAmt.Text) > 0 Then
            If Val(txtInstYear.Text) <= 0 Then
                MsgInformation("Please Enter the Valid Year")
                Cancel = True
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInstYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInstYear.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInstYear_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInstYear.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLoanAmt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanAmt.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLoanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtLoanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLoanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtLoanDate.Text = "" Then GoTo EventExitSub
        If Not IsDate(txtLoanDate.Text) Then
            MsgInformation("Please enter valid Loan date.")
            txtLoanDate.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call FillSprdMain()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub UpDInstMonth_Change()

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub UpDInstMonth_DownClick()

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If Val(txtInstMonth.Text) <= 12 And Val(txtInstMonth.Text) > 1 Then
            txtInstMonth.Text = VB6.Format(Val(txtInstMonth.Text) - 1, "00")
        End If
        FillSprdMain()
    End Sub

    Private Sub FillSprdMain()
        Dim cntRow As Object
        Dim mPeriod As Short
        Dim mMonth As Object
        Dim mYear As Integer
        Dim mAmount As Decimal
        Dim mOPPrincipal As Double
        Dim mInterest As Double
        Dim mPrinical As Double
        Dim mMonthlyInst As Double
        Dim mBalance As Double
        Dim mInterestRate As Double

        If Val(txtAmtPerPeriod.Text) = 0 Then Exit Sub

        mMonth = Val(txtInstMonth.Text)
        mYear = Val(txtInstYear.Text)
        mOPPrincipal = Val(txtLoanAmt.Text)
        mBalance = Val(txtLoanAmt.Text)
        mMonthlyInst = Val(txtAmtPerPeriod.Text)

        mInterestRate = IIf(optInterest(0).Checked = True, 0, Val(txtRate.Text))

        cntRow = 1

        Do While mBalance > 0
            With SprdMain
                .MaxRows = cntRow
                .Row = cntRow
                .Col = ColMonth
                .Text = MonthName(mMonth)

                .Col = ColYear
                .Text = CStr(mYear)

                .Col = ColOPPrincipal
                .Text = CStr(Val(CStr(mOPPrincipal)))

                .Col = ColInterest
                mInterest = System.Math.Round(mOPPrincipal * mInterestRate * 0.01 / 12, 0)
                .Text = CStr(Val(CStr(mInterest)))

                If mOPPrincipal <= mMonthlyInst Then
                    mMonthlyInst = mOPPrincipal + mInterest
                End If

                mPrinical = mMonthlyInst - mInterest
                .Col = ColPrincipal
                .Text = CStr(Val(CStr(mPrinical)))

                .Col = ColMonthlyInstal
                .Text = CStr(Val(CStr(mMonthlyInst)))

                mBalance = mOPPrincipal - mPrinical
                mOPPrincipal = mBalance
                .Col = ColBalAmt
                .Text = CStr(Val(CStr(mBalance)))

                .Col = ColPaidAmt
                .Col = ColChk

                mMonth = mMonth + 1
                If mMonth = 13 Then
                    mMonth = 1
                    mYear = mYear + 1
                End If

            End With
            cntRow = cntRow + 1
        Loop

        '    mPeriod = Int(Val(txtLoanAmt.Text) / Val(txtAmtPerPeriod.Text))

        '    With SprdMain
        '        .MaxRows = mPeriod
        '        For cntRow = 1 To mPeriod
        '            .Row = cntRow
        '
        '            .Col = ColMonth
        '            .Text = MonthName(mMonth)
        '
        '            .Col = ColYear
        '            .Text = CStr(mYear)
        '
        '            .Col = ColBalAmt
        '            .Text = CStr(txtAmtPerPeriod.Text)
        '
        '            .Col = ColMonthlyInstal
        '            .Text = CStr(txtAmtPerPeriod.Text)
        '
        '            mMonth = mMonth + 1
        '            If mMonth = 13 Then
        '                mMonth = 1
        '                mYear = mYear + 1
        '            End If
        '            mAmount = mAmount + txtAmtPerPeriod.Text
        '        Next
        '
        '        If mAmount < txtLoanAmt.Text Then
        '            .MaxRows = .MaxRows + 1
        '            .Row = .MaxRows
        '
        '            .Col = ColMonth
        '            .Text = MonthName(mMonth)
        '
        '            .Col = ColYear
        '            .Text = CStr(mYear)
        '
        '            .Col = ColMonthlyInstal
        '            .Text = CStr(txtLoanAmt.Text - mAmount)
        '
        '            .Col = ColBalAmt
        '            .Text = CStr(txtLoanAmt.Text - mAmount)
        '
        '        End If
        '
        '        If .MaxRows < 3 Then
        '            .ColWidth(ColYear) = 11.5
        '        Else
        '            .ColWidth(ColYear) = 11.5
        '        End If
        FormatSprdMain(-1)
        '    End With
    End Sub

    Private Sub UpDInstMonth_UpClick()
        If Val(txtInstMonth.Text) < 12 And Val(txtInstMonth.Text) >= 1 Then
            txtInstMonth.Text = VB6.Format(Val(txtInstMonth.Text) + 1, "00")
        End If
        FillSprdMain()
    End Sub

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If MainClass.SearchGridMaster((txtEmpNo.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmpNo.Text = AcName1
            TxtEmpNo_Validating(txtEmpNo, New System.ComponentModel.CancelEventArgs(False))
            If txtEmpNo.Enabled = True Then txtEmpNo.Focus()
        End If

        Exit Sub
    End Sub
    Private Sub TxtEmpNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mEmpCode As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(txtEmpNo.Text) = "" Then GoTo EventExitSub
        mEmpCode = Trim(txtEmpNo.Text)


        SqlStr = ""
        SqlStr = "SELECT * FROM  PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpNo.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = True Then
            MsgBox("Name Does Not Exist In Master", MsgBoxStyle.Information)
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
