Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalaryRegSummary
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColDept As Short = 1
    Private Const ColDays As Short = 2
    Private Const ColBSalary As Short = 3
    Private Const ColPSalary As Short = 4

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer
        Dim SqlStrCond1 As String
        Dim SqlStrCond2 As String
        Dim mRecordCount As Integer

        MainClass.ClearGrid(sprdAttn)

        With sprdAttn
            .MaxCols = ColPSalary

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Col = ColSNO
            .Text = "S. No."

            .Col = ColDept
            .Text = IIf(OptGroup(0).Checked = True, "Department", "Category")

            .Col = ColDays
            .Text = "Nos of Employee's"
            .ColHidden = False

            .Col = ColBSalary
            .Text = "Basic Salary"
            .ColHidden = False

            .Col = ColPSalary
            .Text = "Payable Salary"
            .ColHidden = False


            SqlStr = " SELECT NAME,ADDDEDUCT,SEQ FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"


            '        SqlStrCond = SqlStrCond & vbCrLf & " AND (STATUS='O' OR CLOSED_DATE>='" & VB6.Format(lblRunDate.Caption, "DD-MMM-YYYY") & "')"

            SqlStrCond1 = " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            SqlStrCond2 = " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & SqlStrCond1 & vbCrLf & " UNION " & vbCrLf & SqlStr & vbCrLf & SqlStrCond2

            SqlStr = SqlStr & vbCrLf & " ORDER BY ADDDEDUCT,SEQ "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    mRecordCount = mRecordCount + 1
                    RsTemp.MoveNext()
                Loop
                RsTemp.MoveFirst()
            End If

            If RsTemp.EOF = False Then
                '            .MaxCols = .MaxCols + (IIf(chkPerksHead.Value = vbUnchecked, 2, 1) * mRecordCount) + IIf(chkPerksHead.Value = vbUnchecked, 1, 0)
                .MaxCols = .MaxCols + (1 * mRecordCount) + 1
                cntCol = 1
                Do While Not RsTemp.EOF
                    '                If chkPerksHead.Value = vbUnchecked Then
                    '                    .Col = ColPSalary + cntCol
                    '                    .Text = "RATE-" & RsTemp!Name
                    '                    .ColHidden = True
                    '                    cntCol = cntCol + 1
                    '                End If

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
                .ColHidden = False

            End If

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Net Salary"

            FormatSprd(-1)
        End With
    End Sub

    Private Sub cboCostCenter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCostCenter.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboCostCenter_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCostCenter.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub


    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        Call PrintCommand(False)
    End Sub
    Private Sub cboCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.TextChanged
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
    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub chkCostC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCostC.CheckStateChanged
        If chkCostC.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCostCenter.Enabled = False
        Else
            cboCostCenter.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub chkDiv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDiv.CheckStateChanged
        If chkDiv.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
        End If
        Call PrintCommand(False)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
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
        Dim cntRow As Integer
        Dim mBankName As String
        Dim mRptFileName As String
        Dim cntCol As Integer
        Dim mCheckCol As Integer
        Dim pNarr As String

        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        If lblIsArrear.Text = "Y" Then
            mSubTitle = "For the Month Paid: " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        Else
            mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        End If

        mSubTitle = mSubTitle & IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Department : " & cboDept.Text, " ")

        mSubTitle = mSubTitle & IIf(chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Division : " & cboDivision.Text, " ")

        If cboCostCenter.SelectedIndex <> 0 Then
            mSubTitle = mSubTitle & IIf(chkCostC.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Cost Center : " & cboCostCenter.Text, " ")
        End If
        If FillSalRegIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 2) = False Then GoTo ERR1
        '       If FillPrintDummyData(sprdAttn, 0, sprdAttn.MaxRows, ColCard, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = ""
        SqlStr = FetchRecordForSalReg(SqlStr)

        mRptFileName = "SalRegSummary.Rpt"
        If lblIsArrear.Text = "Y" Then
            mTitle = "Arrear Register Summarised" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
        Else
            mTitle = "Salary Register Summarised" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
        End If



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
    Private Function FetchRecordForBankReport(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM TEMP_PrintDummyData PrintDummyData" & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW" ''FIELD7,FIELD1"     ''FIELD6,FIELD65


        FetchRecordForBankReport = mSqlStr

    End Function

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
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub
    Private Sub frmSalaryRegSummary_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmSalaryRegSummary_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        lblRunDate.Text = CStr(RunDate)
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        FillDeptCombo()
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        chkCostC.CheckState = System.Windows.Forms.CheckState.Checked
        chkDiv.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        cboCategory.Enabled = False
        cboCostCenter.Enabled = False
        cboDivision.Enabled = False
        '    OptCC.Value = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub frmSalaryRegSummary_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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


    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        Call PrintCommand(False)
        'RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        Call PrintCommand(False)
        'RefreshScreen
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mDept As String
        Dim mAddDeduct As Integer
        Dim mPayableSalary As Double
        Dim mTotPayable As Double
        Dim mTotDeduct As Double
        Dim mNetSalary As Double
        Dim ColPayableAmount As Integer
        Dim ColDeductionAmount As Integer
        Dim mArrearStr As String
        Dim mBankAcctNo As String
        Dim mCostCCode As String
        Dim mDivisionCode As Double

        Call FillHeading()

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                MsgInformation("Please select the Division Name.")
                cboDivision.Focus()
                Exit Sub
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        If chkCostC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboCostCenter.Text = "" Then
                MsgInformation("Please select the Cost Center.")
                cboCostCenter.Focus()
                Exit Sub
            End If
        End If

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

        If OptGroup(0).Checked = True Then
            SqlStr = " SELECT SALTRN.DEPARTMENT,"
        Else
            SqlStr = " SELECT CATEGORY_DESC AS DEPARTMENT,"
        End If

        SqlStr = SqlStr & vbCrLf & " COUNT(SALTRN.EMP_CODE) AS WDAYS, SUM(SALTRN.BASICSALARY) AS BASICSALARY, SUM(SALTRN.PayableAmount) AS PayableAmount," & vbCrLf & " SUM(SALTRN.PAYABLESALARY) AS PAYABLESALARY,SUM(SALTRN.ACTUALAMOUNT) AS ACTUALAMOUNT, ADD_DEDUCT.NAME AS ADDNAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_EMPLOYEE_MST EMP,PAY_CATEGORY_MST CMST, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =CMST.COMPANY_CODE" & vbCrLf & " AND SALTRN.CATEGORY =CMST.CATEGORY_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.SALHEADCODE =ADD_DEDUCT.CODE "

        SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"


        SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='" & lblIsArrear.Text & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND SALTRN.DEPARTMENT='" & MainClass.AllowSingleQuote(Trim(cboDept.Text)) & "' "
        End If

        If chkDiv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND EMP.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If chkCostC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboCostCenter.SelectedIndex = 0 Then
                If MainClass.ValidateWithMasterTable("R & D", "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCostCCode = Trim(MasterNo)
                    SqlStr = SqlStr & vbCrLf & "AND EMP.COST_CENTER_CODE<>'" & MainClass.AllowSingleQuote(Trim(mCostCCode)) & "' "
                End If
            Else
                If MainClass.ValidateWithMasterTable(cboCostCenter.Text, "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCostCCode = Trim(MasterNo)
                    SqlStr = SqlStr & vbCrLf & "AND EMP.COST_CENTER_CODE='" & MainClass.AllowSingleQuote(Trim(mCostCCode)) & "' "
                End If
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND SALTRN.CATEGORY='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If OptGroup(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "GROUP BY SALTRN.DEPARTMENT,ADD_DEDUCT.NAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ"
            SqlStr = SqlStr & vbCrLf & "ORDER BY SALTRN.DEPARTMENT,ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ"
        Else
            SqlStr = SqlStr & vbCrLf & "GROUP BY SALTRN.CATEGORY,ADD_DEDUCT.NAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ"
            SqlStr = SqlStr & vbCrLf & "ORDER BY SALTRN.CATEGORY,ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1

                Do While Not RsAttn.EOF
                    .MaxRows = cntRow
                    .Row = cntRow

                    mTotPayable = 0
                    mTotDeduct = 0

                    .Col = ColDept
                    If OptGroup(0).Checked = True Then
                        .Text = IIf(IsDbNull(RsAttn.Fields("Department").Value), "", RsAttn.Fields("Department").Value)
                        mDept = IIf(IsDbNull(RsAttn.Fields("Department").Value), "", RsAttn.Fields("Department").Value)
                    Else
                        .Text = IIf(IsDbNull(RsAttn.Fields("Department").Value), "", RsAttn.Fields("Department").Value)
                        mDept = IIf(IsDbNull(RsAttn.Fields("Department").Value), "", RsAttn.Fields("Department").Value)
                    End If
                    .Col = ColDays
                    .Text = CStr(RsAttn.Fields("WDAYS").Value)

                    .Col = ColBSalary
                    .Text = MainClass.FormatRupees(RsAttn.Fields("BASICSALARY").Value)

                    .Col = ColPSalary
                    .Text = VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00")
                    mPayableSalary = CDbl(VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00"))
                    mTotPayable = mPayableSalary * 1


                    Do While mDept = RsAttn.Fields("Department").Value
                        For cntCol = ColBSalary To .MaxCols
                            .Row = 0
                            .Col = cntCol
                            '                        If Trim(UCase(.Text)) = "RATE-" & Trim(UCase(RsAttn!ADDNAME)) Then
                            If Trim(UCase(.Text)) = Trim(UCase(RsAttn.Fields("ADDNAME").Value)) Then
                                .Row = cntRow
                                '                            .Col = cntCol
                                '                            .Text = MainClass.FormatRupees(RsAttn!ACTUALAMOUNT)
                                '
                                '                            cntCol = cntCol + 1

                                .Col = cntCol
                                .Text = MainClass.FormatRupees(RsAttn.Fields("PayableAmount").Value)

                                If RsAttn.Fields("ADDDEDUCT").Value = ConEarning Or RsAttn.Fields("ADDDEDUCT").Value = ConPerks Then
                                    mTotPayable = mTotPayable + RsAttn.Fields("PayableAmount").Value
                                ElseIf RsAttn.Fields("ADDDEDUCT").Value = ConDeduct Then
                                    mTotDeduct = mTotDeduct + RsAttn.Fields("PayableAmount").Value
                                End If
                                Exit For
                            End If
                        Next
                        RsAttn.MoveNext()
                        If RsAttn.EOF = True Then Exit Do
                    Loop

                    .Row = cntRow


                    .Col = ColPayableAmount
                    .Text = MainClass.FormatRupees(mTotPayable)

                    .Col = ColDeductionAmount
                    .Text = MainClass.FormatRupees(mTotDeduct)


                    .Col = .MaxCols
                    mNetSalary = (mTotPayable - mTotDeduct)
                    .Text = VB6.Format(mNetSalary, "0")
                    cntRow = cntRow + 1
                Loop

                ColTotal(sprdAttn, ColDays, .MaxCols)
                .Col = ColDept
                .Row = .MaxRows
                .Text = "TOTAL :"
                MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

                .Row = .MaxRows
                .Row2 = .MaxRows
                .Col = 1
                .col2 = .MaxCols
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '' &H8000000B             ''&H80FF80
                .BlockMode = False

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

            .set_ColWidth(ColSNO, 5)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 20)

            .Col = ColDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDays, 6)

            .ColsFrozen = ColBSalary
            For cntCol = ColBSalary To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
            Next
        End With

        MainClass.ProtectCell(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxRows)
        sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        sprdAttn.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(sprdAttn, mRow)

        Exit Sub
ERR1:

        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim RS As ADODB.Recordset = Nothing

        cboDept.Items.Clear()


        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST WHERE COMPANy_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        cboCostCenter.Items.Clear()
        SqlStr = "Select CC_DESC FROM FIN_CCENTER_HDR WHERE COMPANy_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY CC_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            cboCostCenter.Items.Add("Except R & D")
            Do While Not RsDept.EOF
                cboCostCenter.Items.Add(RsDept.Fields("CC_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

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
ERR1:
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
    Private Function FillSalRegIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr


        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim ColTotPayable As Integer
        Dim ColTotDeduction As Integer
        Dim ColNum As Integer

        Dim Colcnt As Integer
        Dim MaxColcnt As Integer
        Dim arrsal() As String

        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mEmpDesc As String
        Dim mDOJ As String
        Dim mDepartment As String
        Dim mDesignation As String
        Dim mPFNo As String
        Dim mBankAcct As String
        Dim mActualDays As Integer
        Dim mWDays As Double
        Dim mPaymentType As String

        Dim mBSalary As Double
        Dim mPSalary As Double
        Dim I As Integer


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_SALREG_TRN WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        PubDBCn.Execute(SqlStr)

        GridName.Row = 0

        For ColNum = 0 To GridName.MaxCols
            GridName.Col = ColNum
            If UCase(Trim(GridName.Text)) = UCase(Trim("Total Payable")) Then
                ColTotPayable = ColNum
            End If
            If UCase(Trim(GridName.Text)) = UCase(Trim("Total Deduction")) Then
                ColTotDeduction = ColNum
                Exit For
            End If
        Next

        ReDim arrsal(GridName.MaxCols)
        ReDim mEmpEarnData(GridName.MaxCols)
        ReDim mEmpDeductData(GridName.MaxCols)

        For ColNum = ColPSalary + 1 To GridName.MaxCols - 1
            GridName.Col = ColNum
            arrsal(ColNum) = GridName.Text
        Next

        mActualDays = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum

            GridName.Col = ColDays
            mWDays = CDbl(GridName.Text)

            GridName.Col = ColDept
            mDepartment = GridName.Text

            GridName.Col = ColBSalary
            mBSalary = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            GridName.Col = ColPSalary
            mPSalary = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))


            mEmpEarnData(0).mRate = mBSalary
            mEmpEarnData(0).mPayable = mPSalary
            mEmpEarnData(0).mTitle = "BASIC SALARY"
            mEmpEarnData(0).mHeadingDesc = "Rates Payables"


            Colcnt = 1
            GridName.Col = ColPSalary + 1
            Do While GridName.Col < GridName.MaxCols
                '            If chkPerksHead.Value = vbUnchecked Then
                If GridName.Col < ColTotPayable Then
                    mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                    mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                    mEmpEarnData(Colcnt).mHeadingDesc = "Rates Payables"
                    Colcnt = Colcnt + 1
                ElseIf GridName.Col > ColTotPayable And GridName.Col < ColTotDeduction Then
                    mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0)) * -1
                    mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                    mEmpEarnData(Colcnt).mHeadingDesc = "Deductions"
                    Colcnt = Colcnt + 1
                End If
                '            Else
                '                mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                '                mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                '                mEmpEarnData(Colcnt).mHeadingDesc = "Rates Payables"
                '                Colcnt = Colcnt + 1
                '            End If
                GridName.Col = GridName.Col + 1
            Loop
            '        GridName.Col = GridName.MaxCols
            '        mEmpEarnData(Colcnt).mRate = 0
            '        mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
            '        mEmpEarnData(Colcnt).mTitle = "NET SALARY"
            '        mEmpEarnData(Colcnt).mHeadingDesc = " "

            MaxColcnt = Colcnt - 1

            Colcnt = 1

            '        If chkPerksHead.Value = vbUnchecked Then
            I = 0
            '        Else
            '            I = 1
            '        End If
            For Colcnt = I To MaxColcnt
                SqlStr = " INSERT INTO TEMP_SALREG_TRN ( " & vbCrLf & " USERID, COMPANY_CODE, SUBROW , " & vbCrLf & " EMP_CODE, EMP_DESC, " & vbCrLf & " PAYABLE_DAYS, BASIC_SALARY, PAYABLE_BASIC_SALARY,"

                SqlStr = SqlStr & vbCrLf & " ROW_SEQ, ROW_EARN_DEDUCT, ROW_TITLE,ROW_RATE,ROW_PAYABLE " & vbCrLf & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RowNum & ", " & vbCrLf & " '-1','" & mDepartment & "', " & vbCrLf & " " & Val(CStr(mWDays)) & ", " & mBSalary & ", " & mPSalary & ", "


                SqlStr = SqlStr & vbCrLf & " " & Colcnt & ", '" & mEmpEarnData(Colcnt).mHeadingDesc & "', '" & mEmpEarnData(Colcnt).mTitle & "'," & mEmpEarnData(Colcnt).mRate & "," & mEmpEarnData(Colcnt).mPayable & "" & vbCrLf & " )"


                PubDBCn.Execute(SqlStr)
            Next
        Next

        PubDBCn.CommitTrans()
        FillSalRegIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        'Resume
        FillSalRegIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub
End Class
