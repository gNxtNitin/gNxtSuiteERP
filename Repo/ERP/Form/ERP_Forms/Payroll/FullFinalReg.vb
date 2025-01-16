Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmFullFinal
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 1
    Private Const ColCard As Short = 2
    Private Const ColName As Short = 3
    Private Const ColFName As Short = 4
    Private Const ColPaymentType As Short = 5
    Private Const ColDept As Short = 6
    Private Const ColDesg As Short = 7
    Private Const ColDOJ As Short = 8
    Private Const ColBankNo As Short = 9
    Private Const ColDays As Short = 10
    Private Const ColBSalary As Short = 11
    Private Const ColPSalary As Short = 12

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

            .Col = ColFName
            .Text = "Employees'Father Name "

            .Col = ColDays
            .Text = "Working Days"

            .Col = ColBSalary
            .Text = "Basic Salary"

            .Col = ColPSalary
            .Text = "Payable Salary"


            SqlStr = " SELECT NAME,ADDDEDUCT,SEQ FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            '        If chkPerksHead = vbUnchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"
            '        Else
            '            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND PAYMENT_TYPE='M'"
            '            If lblIsArrear.Caption = "P" Then
            '                SqlStr = SqlStr & vbCrLf & " AND CALC_ON <> " & ConCalcVariable & ""
            '            Else
            '                SqlStr = SqlStr & vbCrLf & " AND CALC_ON = " & ConCalcVariable & ""
            '            End If
            '        End If

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
                '            .MaxCols = .MaxCols + 2 * (MainClass.GetMaxRecord("PAY_SALARYHEAD_MST", PubDBCn, SqlStrCond)) + IIf(chkPerksHead = vbUnchecked, 1, 0)
                .MaxCols = .MaxCols + (2 * mRecordCount) '' (IIf(chkPerksHead.Value = vbUnchecked, 2, 1) * mRecordCount) + IIf(chkPerksHead.Value = vbUnchecked, 1, 0)
                cntCol = 1
                Do While Not RsTemp.EOF
                    '                If chkPerksHead.Value = vbUnchecked Then
                    .Col = ColPSalary + cntCol
                    .Text = "RATE-" & RsTemp.Fields("Name").Value
                    .ColHidden = True
                    cntCol = cntCol + 1
                    '                End If

                    .Col = ColPSalary + cntCol
                    .Text = RsTemp.Fields("Name").Value
                    mAddDeduct = RsTemp.Fields("ADDDEDUCT").Value

                    RsTemp.MoveNext()
                    cntCol = cntCol + 1
                    '                If chkPerksHead.Value = vbUnchecked Then
                    If Not RsTemp.EOF Then
                        If RsTemp.Fields("ADDDEDUCT").Value <> mAddDeduct Then
                            .Col = ColPSalary + cntCol
                            .Text = "Total Payable"

                            cntCol = cntCol + 1
                        End If
                    End If
                    '                End If
                Loop

                '            If chkPerksHead.Value = vbUnchecked Then
                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "Total Deduction"
                '                .ColHidden = IIf(chkPerksHead = vbUnchecked, False, True)
                '            End If
            End If

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Incentive For the Current Month"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Incentive For the Previous Month"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Arrear - Salary"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Arrear - Incentive"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Bonus For the Current Month"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Bonus For the Previous Month"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Gratuity"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Notice Pay"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Ex-Gratia"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Compensation"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "LTC"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Leave"

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Others"

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

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboOrder1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrder1.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboOrder1_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrder1.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub
    Private Sub cboOrder2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrder2.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboOrder2_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrder2.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub
    Private Sub cboOrder3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrder3.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboOrder3_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrder3.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
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

    Private Sub chkDivision_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDivision.CheckStateChanged
        If chkDivision.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
        End If
        Call PrintCommand(False)
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
        'Dim All As Boolean
        'Dim SqlStr As String=""=""
        'Dim mTitle As String
        'Dim mSubTitle As String
        'Dim ColStartRow As Long
        'Dim ColEndRow As Long
        'Dim cntRow As Long
        'Dim mBankName As String
        'Dim mRptFileName As String
        'Dim cntCol As Long
        'Dim mCheckCol As Long
        'Dim pNarr As String
        '
        '    PubDBCn.Errors.Clear
        '
        '    frmPrintSalReg.Show 1
        '
        '    If G_PrintLedg = False Then
        '        Exit Sub
        '    End If
        '
        '    Call MainClass.ClearCRptFormulas(Report1)
        '
        '    'Insert Data from Grid to PrintDummyData Table...
        '
        '
        '    mSubTitle = "Full & Final Register For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '
        '    mSubTitle = mSubTitle & IIf(chkAll.Value = vbUnchecked, " AND Department : " & cboDept, " ")
        '
        '    If cboCostCenter.ListIndex <> 0 Then
        '        mSubTitle = mSubTitle & IIf(chkCostC.Value = vbUnchecked, " AND Cost Center : " & cboCostCenter, " ")
        '    End If
        '
        '
        '    mSubTitle = mSubTitle & IIf(chkDivision.Value = vbUnchecked, " AND Division : " & cboDivision, " ")
        '
        '    If frmPrintSalReg.optPaySlip.Value = True Then
        '        If frmPrintSalReg.optAll(0) Then
        '            ColStartRow = 1
        '            ColEndRow = sprdAttn.MaxRows - 2
        '        Else
        '            For cntRow = 1 To sprdAttn.MaxRows
        '                sprdAttn.Row = cntRow
        '                sprdAttn.Col = ColCard
        '                If UCase(Trim(sprdAttn.Text)) = UCase(Trim(frmPrintSalReg.txtEmpCode)) Then
        '                    ColStartRow = cntRow
        '                    ColEndRow = cntRow
        '                    Exit For
        '                End If
        '            Next
        '        End If
        '        If ColEndRow = 0 Then
        '            MsgBox "Such Employee Salary is not Updated...", vbInformation
        '            Exit Sub
        '        End If
        '        If FillPaySlipIntoPrintDummy(sprdAttn, ColStartRow, ColEndRow) = False Then GoTo ERR1
        '
        '
        '
        '        SqlStr = ""
        '        SqlStr = FetchRecordForPaySlip(SqlStr)
        '
        '
        '        If chkPerksHead.Value = vbChecked Then
        '            mRptFileName = "PerksSlip.Rpt"
        '            mTitle = "" & "For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '        Else
        '            mRptFileName = IIf(RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12, "PaySlipKJ.Rpt", "PaySlip.Rpt")
        '            If lblIsArrear.Caption = "Y" Then
        '                mTitle = "ARREAR SLIP " & "For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '            Else
        '                mTitle = "PAY SLIP " & "For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '            End If
        '        End If
        '        mSubTitle = ""              ''"[ Rule 26(2) ]"
        '
        '    ElseIf frmPrintSalReg.OptSalReg.Value = True Then
        '        If FillSalRegIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 2) = False Then GoTo ERR1
        ''       If FillPrintDummyData(sprdAttn, 0, sprdAttn.MaxRows, ColCard, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1
        '
        '        SqlStr = ""
        '        SqlStr = FetchRecordForSalReg(SqlStr)
        '        If chkPerksHead.Value = vbChecked Then
        '            mRptFileName = "PerksReg.Rpt"
        '            mTitle = "Perks Register" & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
        '        Else
        '            mRptFileName = "SalReg.Rpt"
        '            If lblIsArrear.Caption = "Y" Then
        '                mTitle = "Arrear Register" & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
        '            Else
        '                mTitle = "Salary Register" & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
        '            End If
        '        End If
        '
        '    ElseIf frmPrintSalReg.optCashSheet.Value = True Then
        '        mBankName = ""
        ''        If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, "CASH", "") = False Then GoTo ERR1
        '        If FillBankSheetIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, ColCard, ColName, ColDays, ColPaymentType, sprdAttn.MaxCols, ColBankNo, "CASH", mBankName) = False Then GoTo ERR1
        '        SqlStr = ""
        '        SqlStr = FetchRecordForReport(SqlStr)
        '        mRptFileName = "SalCashSheet.Rpt"
        '
        '        If lblIsArrear.Caption = "Y" Then
        '            mTitle = "Arrear Sheet (Cash)" & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
        '        Else
        '            mTitle = "Salary Sheet (Cash)" & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
        '        End If
        '     ElseIf frmPrintSalReg.optInaam.Value = True Then
        '        mCheckCol = 0
        '        For cntCol = 1 To sprdAttn.MaxCols
        '            sprdAttn.Row = 0
        '            sprdAttn.Col = cntCol
        '            If MainClass.ValidateWithMasterTable(UCase(Trim(sprdAttn.Text)), "NAME", "CODE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE=" & ConINAAM & "") = True Then
        '                mCheckCol = cntCol
        '                Exit For
        '            End If
        '        Next
        '        If mCheckCol > 0 Then
        '            If FillInaamIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, ColCard, ColName, mCheckCol, sprdAttn.MaxCols) = False Then GoTo ERR1
        '            SqlStr = ""
        '            SqlStr = FetchRecordForReport(SqlStr)
        '            mRptFileName = "InaamSheet.Rpt"
        '
        '            If lblIsArrear.Caption = "Y" Then
        '                mTitle = "Inaam Sheet (Arrear)" & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
        '            Else
        '                mTitle = "Inaam Sheet " & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
        '            End If
        '        End If
        '    ElseIf frmPrintSalReg.OptSalSheet.Value = True Then
        '        If frmPrintSalReg.optAllBank(0).Value = True Then
        '            mBankName = ""
        '        Else
        '            mBankName = frmPrintSalReg.txtBankName
        '        End If
        ''        If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, "CHEQUE", mBankName) = False Then GoTo ERR1
        '        If FillBankSheetIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, ColCard, ColName, ColDays, ColPaymentType, sprdAttn.MaxCols, ColBankNo, "CHEQUE", mBankName) = False Then GoTo ERR1
        '
        '        SqlStr = ""
        '        SqlStr = FetchRecordForBankReport(SqlStr)
        '        mRptFileName = "BankSheet.Rpt"
        '
        '                ''InputBox("Please Enter Bank Name. :", "Bank Name")
        '
        '        If mBankName = "" Then
        '            mTitle = "BANK ANNEXURES"
        '        Else
        '            mTitle = "BANK ANNEXURES OF " & mBankName
        '        End If
        '
        '        If chkPerksHead.Value = vbChecked Then
        '            mSubTitle = "Perks For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '        Else
        '            If lblIsArrear.Caption = "Y" Then
        '                mSubTitle = "Arrear For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '            Else
        '                mSubTitle = "Salary For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '            End If
        '        End If
        '
        '    ElseIf frmPrintSalReg.optBankTxt.Value = True Then
        '        If frmPrintSalReg.optAllBank(0).Value = True Then
        '            mBankName = ""
        '        Else
        '            mBankName = frmPrintSalReg.txtBankName
        '        End If
        '
        '        If lblIsArrear.Caption = "P" Then
        '            pNarr = "BY PERKS " & UCase(lblYear.Caption)
        '        Else
        '            pNarr = IIf(lblIsArrear.Caption = "N", "BY SALARY OF ", "BY ARREAR OF ") & UCase(lblYear.Caption)
        '        End If
        '        If CreateTxtFileForBank(sprdAttn, ColCard, ColName, ColPaymentType, ColBankNo, sprdAttn.MaxCols, mBankName, pNarr, sprdAttn.MaxRows - 2) = False Then GoTo ERR1
        '
        '        Unload frmPrintSalReg
        '        Exit Sub
        '    ElseIf frmPrintSalReg.OptDeductionList.Value = True Then
        '
        '        If Trim(frmPrintSalReg.txtDeductionName.Text) <> "" Then
        '            For cntCol = 1 To sprdAttn.MaxCols
        '                sprdAttn.Row = 0
        '                sprdAttn.Col = cntCol
        '                If UCase(Trim(frmPrintSalReg.txtDeductionName.Text)) = UCase(Trim(sprdAttn.Text)) Then
        '                    mCheckCol = cntCol
        '                    Exit For
        '                End If
        '            Next
        '            If FillDed_DataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 1, mCheckCol) = False Then GoTo ERR1
        '
        '            SqlStr = ""
        '
        '            If MainClass.ValidateWithMasterTable(frmPrintSalReg.txtDeductionName.Text, "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE=" & ConLoan & "") = True Then
        '                mRptFileName = "SalBankDeductSheet.Rpt"
        '                If cboOrder1.ListIndex = 0 Then
        '                    SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD4,FIELD2")
        '                Else
        '                    SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD4,FIELD1")
        '                End If
        '            ElseIf frmPrintSalReg.txtDeductionName.Text = "LIC" Then
        '                mRptFileName = "SalLICDeductSheet.Rpt"
        '                If cboOrder1.ListIndex = 0 Then
        '                    SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD6,FIELD2")
        '                Else
        '                    SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD6,FIELD1")
        '                End If
        '            Else
        '                mRptFileName = "SalDeductSheet.Rpt"
        '                SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        '            End If
        '
        '            mTitle = "DEDUCTION LIST (" & frmPrintSalReg.txtDeductionName.Text & ")"
        '
        '            If lblIsArrear.Caption = "Y" Then
        '                mTitle = mTitle & "Arrear"
        '            End If
        '
        '            If lblIsArrear.Caption = "Y" Then
        '                mSubTitle = "For the Month Paid: " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '            Else
        '                mSubTitle = "For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '            End If
        '        Else
        '            MsgInformation "Please Select Deduction Head Name"
        '            Unload frmPrintSalReg
        '            Exit Sub
        '        End If
        '    End If
        '
        '    Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
        '    Unload frmPrintSalReg
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
    Private Sub frmFullFinal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmFullFinal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        chkDivision.CheckState = System.Windows.Forms.CheckState.Checked
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

    Private Sub frmFullFinal_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
        Dim mCode As String
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
        Dim mDeptCode As String

        Dim mIncAmtForMonth As Double
        Dim mIncAmtPreMonth As Double
        Dim mArrearSalary As Double
        Dim mArrearInc As Double
        Dim mBonusCurrYear As Double
        Dim mBonusForYear As Double
        Dim mGratuity As Double
        Dim mNoticeAmt As Double
        Dim mExGratia As Double
        Dim mCompensation As Double
        Dim mLTC As Double
        Dim mELAmount As Double
        Dim mOtherAmount As Double


        Call FillHeading()

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

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDivision.Text = "" Then
                If cboDivision.Enabled = True Then cboDivision.Focus()
                MsgInformation("Please Select Division.")
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




        SqlStr = " SELECT IH.*, " & vbCrLf & " ID.WDAYS, ID.BASICSALARY, ID.PAYABLESALARY, ID.PERCENTAGE, ID.PayableAmount,  ID.ACTUALAMOUNT, " & vbCrLf & " EMP.EMP_NAME, DECODE(EMP.EMP_GROUP_DOJ,NULL,EMP.EMP_DOJ,EMP.EMP_GROUP_DOJ) AS EMP_DOJ, EMP.EMP_FNAME, EMP.PAYMENTMODE, EMP.EMP_BANK_NO," & vbCrLf & " ADD_DEDUCT.NAME AS ADDNAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ, ADD_DEDUCT.CALC_ON " & vbCrLf & " FROM PAY_FFSETTLE_HDR IH, PAY_FFSETTLE_DET ID, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE(+)" & vbCrLf & " AND IH.EMP_CODE=ID.EMP_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND IH.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND ID.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND ID.SALHEADCODE =ADD_DEDUCT.CODE " & vbCrLf & " AND TO_CHAR(IH.EMP_LEAVE_DATE,'MMYYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMYYYY")) & "'"

        SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"

        If PubUserID = "G0416" Or PubUserID = "000994" Then
        Else
            SqlStr = SqlStr & " AND  EMP.EMP_CODE<>'000840'"
        End If

        If optTransfer.Checked = True Then
            SqlStr = SqlStr & " AND  IH.IS_TRANSFER='Y'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='F'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDept.Text), "DESP_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
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

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
                SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & mDivisionCode & ""
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        If UCase(Trim(cboCorporate.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.IS_CORPORATE='" & VB.Left(cboCorporate.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY "

        If cboOrder1.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_NAME, EMP.EMP_CODE,"
        ElseIf cboOrder1.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CODE, "
        ElseIf cboOrder1.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CATG, "
        ElseIf cboOrder1.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_DEPT_CODE, "
            '    ElseIf cboOrder1.ListIndex = 4 Then
            '        SqlStr = SqlStr & vbCrLf & " SALTRN.BANKACCTNO, "
        End If

        If cboOrder2.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_NAME, EMP.EMP_CODE,"
        ElseIf cboOrder2.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CODE, "
        ElseIf cboOrder2.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CATG, "
        ElseIf cboOrder2.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_DEPT_CODE, "
            '    ElseIf cboOrder2.ListIndex = 5 Then
            '        SqlStr = SqlStr & vbCrLf & " SALTRN.BANKACCTNO, "
        End If

        If cboOrder3.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_NAME, EMP.EMP_CODE,"
        ElseIf cboOrder3.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CODE, "
        ElseIf cboOrder3.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_DEPT_CODEY, "
        ElseIf cboOrder3.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_DEPT_CODE, "
        End If

        SqlStr = SqlStr & vbCrLf & " ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1

                Do While Not RsAttn.EOF
                    .MaxRows = cntRow
                    .Row = cntRow

                    mTotPayable = 0
                    mTotDeduct = 0
                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    mPayableSalary = CDbl(VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00"))

                    '                If mCode = "000089" Then MsgBox RsAttn!EMP_CODE
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_FNAME").Value), "", RsAttn.Fields("EMP_FNAME").Value)

                    .Col = ColPaymentType
                    .Text = IIf(RsAttn.Fields("PAYMENTMODE").Value = "1", "Cash", "Cheque")

                    mBankAcctNo = IIf(IsDbNull(RsAttn.Fields("EMP_BANK_NO").Value), "", RsAttn.Fields("EMP_BANK_NO").Value)

                    .Col = ColBankNo
                    .Text = mBankAcctNo
                    '
                    '                .Col = ColDept
                    '                .Text = IIf(IsNull(RsAttn!Department), "", RsAttn!Department)
                    '
                    '                .Col = ColDesg
                    '                .Text = IIf(IsNull(RsAttn!DESG_DESC), "", RsAttn!DESG_DESC)

                    .Col = ColDOJ
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("EMP_DOJ").Value), "", RsAttn.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                    .Col = ColDays
                    .Text = CStr(RsAttn.Fields("WDAYS").Value)

                    .Col = ColBSalary
                    .Text = MainClass.FormatRupees(RsAttn.Fields("BASICSALARY"))

                    .Col = ColPSalary
                    .Text = VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00")
                    mPayableSalary = CDbl(VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00"))
                    mTotPayable = mPayableSalary ''* IIf(chkPerksHead = vbUnchecked, 1, 0)

                    mIncAmtForMonth = IIf(IsDbNull(RsAttn.Fields("INC_AMT_FORMON").Value), 0, RsAttn.Fields("INC_AMT_FORMON").Value)
                    mIncAmtPreMonth = IIf(IsDbNull(RsAttn.Fields("INC_AMT_PREMON").Value), 0, RsAttn.Fields("INC_AMT_PREMON").Value)
                    mArrearSalary = IIf(IsDbNull(RsAttn.Fields("ARREAR_SAL").Value), 0, RsAttn.Fields("ARREAR_SAL").Value)
                    mArrearInc = IIf(IsDbNull(RsAttn.Fields("ARREAR_INC").Value), 0, RsAttn.Fields("ARREAR_INC").Value)
                    mBonusCurrYear = IIf(IsDbNull(RsAttn.Fields("BONUS_CURRYEAR").Value), 0, RsAttn.Fields("BONUS_CURRYEAR").Value)
                    mBonusForYear = IIf(IsDbNull(RsAttn.Fields("BONUS_FORYEAR").Value), 0, RsAttn.Fields("BONUS_FORYEAR").Value)
                    mGratuity = IIf(IsDbNull(RsAttn.Fields("GRATUITY_AMOUNT").Value), 0, RsAttn.Fields("GRATUITY_AMOUNT").Value)
                    mNoticeAmt = IIf(IsDbNull(RsAttn.Fields("NOTICE_AMOUNT").Value), 0, RsAttn.Fields("NOTICE_AMOUNT").Value)
                    mExGratia = IIf(IsDbNull(RsAttn.Fields("EXGRATIA_AMOUNT").Value), 0, RsAttn.Fields("EXGRATIA_AMOUNT").Value)
                    mCompensation = IIf(IsDbNull(RsAttn.Fields("COMPENSATION_AMOUNT").Value), 0, RsAttn.Fields("COMPENSATION_AMOUNT").Value)
                    mLTC = IIf(IsDbNull(RsAttn.Fields("LTC_AMOUNT").Value), 0, RsAttn.Fields("LTC_AMOUNT").Value)
                    mELAmount = IIf(IsDbNull(RsAttn.Fields("EL_AMOUNT").Value), 0, RsAttn.Fields("EL_AMOUNT").Value)
                    mOtherAmount = IIf(IsDbNull(RsAttn.Fields("OTHERS_AMOUNT").Value), 0, RsAttn.Fields("OTHERS_AMOUNT").Value)

                    Do While mCode = RsAttn.Fields("EMP_CODE").Value
                        For cntCol = ColBSalary To .MaxCols
                            .Row = 0
                            .Col = cntCol
                            If Trim(UCase(.Text)) = "RATE-" & Trim(UCase(RsAttn.Fields("ADDNAME").Value)) Then
                                .Row = cntRow
                                .Col = cntCol
                                If RsAttn.Fields("CALC_ON").Value = ConCalcVariable Then
                                    .Text = "0.00"
                                Else
                                    .Text = MainClass.FormatRupees(RsAttn.Fields("ACTUALAMOUNT"))
                                End If

                                cntCol = cntCol + 1

                                .Col = cntCol
                                .Text = MainClass.FormatRupees(RsAttn.Fields("PayableAmount"))

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

                    mNetSalary = (mTotPayable - mTotDeduct)

                    .Col = ColDeductionAmount + 1
                    .Text = CStr(mIncAmtForMonth)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 2
                    .Text = CStr(mIncAmtPreMonth)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 3
                    .Text = CStr(mArrearSalary)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 4
                    .Text = CStr(mArrearInc)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 5
                    .Text = CStr(mBonusCurrYear)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 6
                    .Text = CStr(mBonusForYear)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 7
                    .Text = CStr(mGratuity)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 8
                    .Text = CStr(mNoticeAmt)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 9
                    .Text = CStr(mExGratia)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 10
                    .Text = CStr(mCompensation)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 11
                    .Text = CStr(mLTC)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 12
                    .Text = CStr(mELAmount)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = ColDeductionAmount + 13
                    .Text = CStr(mOtherAmount)
                    mNetSalary = mNetSalary + Val(.Text)

                    .Col = .MaxCols
                    .Text = VB6.Format(mNetSalary, "0")

                    cntRow = cntRow + 1

                Loop


                ColTotal(sprdAttn, ColBSalary, .MaxCols)
                .Col = ColName
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

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCard, 7)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 15)

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColFName, 15)
            .ColHidden = True

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

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        cboDept.Items.Clear()


        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST WHERE COMPANy_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If


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

        cboOrder1.Items.Clear()
        cboOrder1.Items.Add("Name")
        cboOrder1.Items.Add("Card")
        cboOrder1.Items.Add("Category")
        cboOrder1.Items.Add("Department")
        cboOrder1.Items.Add("Bank A/c")
        cboOrder1.SelectedIndex = 2

        cboOrder2.Items.Clear()
        cboOrder2.Items.Add("None")
        cboOrder2.Items.Add("Name")
        cboOrder2.Items.Add("Card")
        cboOrder2.Items.Add("Category")
        cboOrder2.Items.Add("Department")
        cboOrder2.Items.Add("Bank A/c")
        cboOrder2.SelectedIndex = 2

        cboOrder3.Items.Clear()
        cboOrder3.Items.Add("None")
        cboOrder3.Items.Add("Name")
        cboOrder3.Items.Add("Card")
        cboOrder3.Items.Add("Category")
        cboOrder3.Items.Add("Department")
        cboOrder3.Items.Add("Bank A/c")
        cboOrder3.SelectedIndex = 0

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0

        cboCorporate.Items.Clear()
        cboCorporate.Items.Add("ALL")
        cboCorporate.Items.Add("Yes")
        cboCorporate.Items.Add("No")
        cboCorporate.SelectedIndex = 0

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
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub
End Class
