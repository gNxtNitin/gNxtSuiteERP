Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System
'Imports Microsoft.Office.Interop

Friend Class frmSalaryReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCard As Short = 1
    Private Const ColNewCard As Short = 2
    Private Const ColName As Short = 3
    Private Const ColFName As Short = 4
    Private Const ColPaymentType As Short = 5
    Private Const ColDept As Short = 6
    Private Const ColDesg As Short = 7
    Private Const ColDOJ As Short = 8
    Private Const ColBankNo As Short = 9
    Private Const ColBankName As Short = 10
    Private Const ColBankIFSC As Short = 11
    Private Const ColDays As Short = 12
    'Private Const ColOTHours As Short = 13
    Private Const ColBSalary As Short = 13
    Private Const ColPSalary As Short = 14

    Dim mOtherEarningVar As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer
        Dim SqlStrCond1 As String
        Dim SqlStrCond2 As String
        Dim mRecordCount As Integer

        Dim mSalaryHeadName As String
        Dim mSalHeadType As Integer


        mOtherEarningVar = False
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

            .Col = ColNewCard
            .Text = "Emp New Card No"

            .Col = ColPaymentType
            .Text = "Payment Type"

            .Col = ColBankNo
            .Text = "Bank A/c No."

            .Col = ColBankName
            .Text = "Bank Name"

            .Col = ColBankIFSC
            .Text = "Bank IFSC Code"

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
            .ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)

            '.Col = ColOTHours
            '.Text = "INC Hours"
            '.ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)

            .Col = ColBSalary
            .Text = "Basic Salary"
            .ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)

            .Col = ColPSalary
            .Text = "Payable Salary"
            .ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)


            SqlStr = " SELECT NAME,ADDDEDUCT,SEQ FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"
            Else
                SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND PAYMENT_TYPE='M'"
                If lblIsArrear.Text = "P" Then
                    SqlStr = SqlStr & vbCrLf & " AND CALC_ON <> " & ConCalcVariable & ""
                Else
                    SqlStr = SqlStr & vbCrLf & " AND CALC_ON = " & ConCalcVariable & ""
                End If
            End If

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
                .MaxCols = .MaxCols + (IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, 2, 1) * mRecordCount) + IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, 1, 1)
                .MaxCols = .MaxCols + IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, 1, 0) '' iNCENTIVE cOL
                cntCol = 1
                mOtherEarningVar = False

                Do While Not RsTemp.EOF

                    mSalaryHeadName = RsTemp.Fields("Name").Value

                    If MainClass.ValidateWithMasterTable(UCase(Trim(mSalaryHeadName)), "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mSalHeadType = Val(MasterNo)
                    Else
                        mSalHeadType = 0
                    End If

                    'If Val(CStr(mSalHeadType)) = ConOtherEarningVar And RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(lblRunDate.Text) >= CDate("01/11/2015") Then
                    '    mOtherEarningVar = True
                    'Else
                    If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        .Col = ColPSalary + cntCol
                        .Text = "RATE-" & RsTemp.Fields("Name").Value
                        .ColHidden = True
                        cntCol = cntCol + 1
                    End If

                    .Col = ColPSalary + cntCol
                    .Text = RsTemp.Fields("Name").Value
                    mAddDeduct = RsTemp.Fields("ADDDEDUCT").Value
                    'End If

                    RsTemp.MoveNext()
                    'If Val(CStr(mSalHeadType)) = ConOtherEarningVar And RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(lblRunDate.Text) >= CDate("01/11/2015") Then
                    'Else
                    cntCol = cntCol + 1
                    'End If

                    If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If Not RsTemp.EOF Then
                            If RsTemp.Fields("ADDDEDUCT").Value <> mAddDeduct Then
                                .Col = ColPSalary + cntCol ''01012023
                                .Text = "INCENTIVE"
                                cntCol = cntCol + 1

                                .Col = ColPSalary + cntCol
                                .Text = "Total Payable"

                                cntCol = cntCol + 1

                                ''Add Other Allow..  ''02/12/2015
                                If mOtherEarningVar = True Then
                                    .Col = ColPSalary + cntCol
                                    .Text = "RATE-" & mSalaryHeadName
                                    .ColHidden = True
                                    cntCol = cntCol + 1

                                    .Col = ColPSalary + cntCol
                                    .Text = mSalaryHeadName
                                    cntCol = cntCol + 1
                                End If
                            End If
                        End If
                    Else

                        .Col = ColPSalary + cntCol
                        .Text = "Total Payable"
                    End If
                Loop

                If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    .MaxCols = .MaxCols + 1
                    .Col = .MaxCols
                    .Text = "Total Deduction"
                    .ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)
                Else
                    .MaxCols = .MaxCols + 1
                    .Col = .MaxCols
                    .Text = "Total Deduction"
                End If
            End If

            .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Net Salary"

            If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then    ''And cboShowSalary.SelectedIndex = 1
                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "Net Salary (Bank Transfer)"

                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "Incentive"

                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "Incentive"
            End If

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

    Private Sub cmdAccountPost_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAccountPost.Click

        Dim mVNo As String
        Dim mVDate As String
        Dim mBankCode As Integer
        Dim mYM As Integer
        Dim mBType As String
        Dim mBSType As String
        Dim mm As New frmAtrn
        Dim mVType As String
        Dim mVSeqNo As Integer
        Dim mVNoSuffix As String
        Dim mDivisionCode As Double

        '    myMenu = "mnuJournal"

        If lblShowType.Text = "D" Then
            Exit Sub
        End If

        mm.lblBookType.Text = ConJournal

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Please Select Division First.")
            Exit Sub
        Else
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            Else
                MsgBox("Invaild Division.")
                Exit Sub
            End If
        End If

        mm.txtVDate.Text = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(Month(CDate(lblRunDate.Text)), "00") & "/" & Year(CDate(lblRunDate.Text))
        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        mm.lblYM.Text = CStr(mYM)
        If lblIsArrear.Text = "P" Then
            mBType = "P"
        ElseIf lblIsArrear.Text = "V" Then
            mBType = "V"
        ElseIf lblIsArrear.Text = "Y" Then
            mBType = "A"
        Else
            mBType = "S"
        End If

        If lblIsArrear.Text = "P" Or lblIsArrear.Text = "V" Then
            If Val(cboMonthTerm.Text) = 1 Then
                mBSType = "X"
            ElseIf Val(cboMonthTerm.Text) = 2 Then
                mBSType = "Y"
            Else
                mBSType = "Z"
            End If
        Else
            mBSType = VB.Left(cboCategory.Text, 1)
        End If

        mm.lblSR.Text = mBType & mBSType & mDivisionCode
        mm.MdiParent = Me.MdiParent
        mm.Show()
        If CheckSalVoucher(mYM, mVNo, mVDate, mVType, mVSeqNo, mVNoSuffix, mBankCode, mBType, mBSType, mDivisionCode) = True Then

            mm.frmAtrn_Activated(Nothing, New System.EventArgs())
            mm.txtVDate.Text = VB6.Format(mVDate, "dd/mm/yyyy")
            mm.txtVType.Text = mVType
            mm.txtVNo.Text = VB6.Format(mVSeqNo, "00000")
            mm.txtVNoSuffix.Text = mVNoSuffix

            mm.txtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
            mm.CmdAdd.Enabled = False
        Else
            mm.frmAtrn_Activated(Nothing, New System.EventArgs())
            mm.txtVDate.Text = VB6.Format(MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(Month(CDate(lblRunDate.Text)), "00") & "/" & Year(CDate(lblRunDate.Text)), "dd/mm/yyyy")
            mm.txtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdLeave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLeave.Click

        If lblShowType.Text = "D" Then
            Exit Sub
        End If

        sprdAttn.Row = sprdAttn.ActiveRow
        sprdAttn.Col = 1
        If Trim(sprdAttn.Text) = "" Then Exit Sub
        frmLeave.lblCode.Text = sprdAttn.Text

        sprdAttn.Col = ColName
        frmLeave.lblEmpName.Text = sprdAttn.Text

        sprdAttn.Row = 0
        sprdAttn.Col = sprdAttn.ActiveCol
        frmLeave.lblvwMonth.Text = VB6.Format(lblRunDate.Text, "MMMM , yyyy")
        frmLeave.lblMonth.Text = CStr(Month(CDate(lblRunDate.Text)))
        frmLeave.lblYear.Text = IIf(Month(CDate(lblRunDate.Text)) < 4, Year(CDate(lblRunDate.Text)) - 1, Year(CDate(lblRunDate.Text)))
        frmLeave.ShowDialog()
    End Sub

    Private Sub cmdPayslipeMail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPaySlipeMail.Click
        Dim mAuthorisation As String

        If lblShowType.Text = "D" Then
            Exit Sub
        End If
        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        If mAuthorisation = "N" Then
            MsgBox("You have no Right to Mail eSlip. ", MsgBoxStyle.Critical)
            Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PayslipeMail()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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

        'If lblShowType.Text = "D" Then
        '    Exit Sub
        'End If

        PubDBCn.Errors.Clear()

        frmPrintSalReg.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        If lblIsArrear.Text = "Y" Then
            mSubTitle = "For the Month Paid: " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        Else
            mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        End If

        If lblIsArrear.Text = "P" Or lblIsArrear.Text = "V" Then
            mSubTitle = mSubTitle & IIf(Val(cboMonthTerm.Text) = 1, " (1st Half)", IIf(Val(cboMonthTerm.Text) = 2, " (@nd Half)", " (3rd Half)"))
        End If

        mSubTitle = mSubTitle & IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Department : " & cboDept.Text, " ")

        If cboCostCenter.SelectedIndex <> 0 Then
            mSubTitle = mSubTitle & IIf(chkCostC.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Cost Center : " & cboCostCenter.Text, " ")
        End If


        mSubTitle = mSubTitle & IIf(chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Division : " & cboDivision.Text, " ")


        If frmPrintSalReg.optPaySlip.Checked = True Or frmPrintSalReg.OptIncentive.Checked = True Then
            If frmPrintSalReg.optAll(0).Checked Then
                ColStartRow = 1
                ColEndRow = sprdAttn.MaxRows - 2
            Else
                For cntRow = 1 To sprdAttn.MaxRows
                    sprdAttn.Row = cntRow
                    sprdAttn.Col = ColCard
                    If UCase(Trim(sprdAttn.Text)) = UCase(Trim(frmPrintSalReg.TxtEmpCode.Text)) Then
                        ColStartRow = cntRow
                        ColEndRow = cntRow
                        Exit For
                    End If
                Next
            End If
            If ColEndRow = 0 Then
                MsgBox("Such Employee Salary is not Updated...", MsgBoxStyle.Information)
                Exit Sub
            End If

            If frmPrintSalReg.optPaySlip.Checked = True Then
                If FillPaySlipIntoPrintDummy(sprdAttn, ColStartRow, ColEndRow) = False Then GoTo ERR1
            Else
                If FillIncentiveSlipIntoPrintDummy(sprdAttn, ColStartRow, ColEndRow) = False Then GoTo ERR1
            End If


            SqlStr = ""
            SqlStr = FetchRecordForPaySlip(SqlStr)

            If frmPrintSalReg.OptIncentive.Checked = True Then
                mRptFileName = "PaySlip.Rpt"
                If lblIsArrear.Text = "Y" Then
                    mTitle = "INCENTIVE ARREAR SLIP " & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                Else
                    mTitle = "INCENTIVE SLIP " & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                End If
            Else
                If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mRptFileName = "PayPerks.Rpt" ''"PerksSlip.Rpt"
                    mTitle = "" & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                Else
                    mRptFileName = "PaySlip.Rpt"
                    If lblIsArrear.Text = "Y" Then
                        mTitle = "ARREAR SLIP " & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                    Else
                        mTitle = "PAY SLIP " & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                    End If
                End If
            End If
            mSubTitle = "" ''"[ Rule 26(2) ]"

        ElseIf frmPrintSalReg.OptSalReg.Checked = True Then
            If FillSalRegIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 2) = False Then GoTo ERR1
            '       If FillPrintDummyData(sprdAttn, 0, sprdAttn.MaxRows, ColCard, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1

            SqlStr = ""
            SqlStr = FetchRecordForSalReg(SqlStr)
            If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Checked Then
                mRptFileName = "PerksReg.Rpt"
                mTitle = "Perks Register" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
            Else
                mRptFileName = "SalReg.Rpt"
                If lblIsArrear.Text = "Y" Then
                    mTitle = "Arrear Register" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
                Else
                    mTitle = "Salary Register" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
                End If
            End If

        ElseIf frmPrintSalReg.optCashSheet.Checked = True Then
            mBankName = ""
            '        If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, "CASH", "") = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, ColCard, ColName, ColDays, ColPaymentType, (sprdAttn.MaxCols), ColBankNo, "CASH", mBankName, ColBankIFSC) = False Then GoTo ERR1
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr)
            mRptFileName = "SalCashSheet.Rpt"

            If lblIsArrear.Text = "Y" Then
                mTitle = "Arrear Sheet (Cash)" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
            Else
                mTitle = "Salary Sheet (Cash)" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
            End If
        ElseIf frmPrintSalReg.optInaam.Checked = True Then
            mCheckCol = 0
            For cntCol = 1 To sprdAttn.MaxCols
                sprdAttn.Row = 0
                sprdAttn.Col = cntCol
                If MainClass.ValidateWithMasterTable(UCase(Trim(sprdAttn.Text)), "NAME", "CODE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE=" & ConINAAM & "") = True Then
                    mCheckCol = cntCol
                    Exit For
                End If
            Next
            If mCheckCol > 0 Then
                If FillInaamIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, ColCard, ColName, mCheckCol, (sprdAttn.MaxCols)) = False Then GoTo ERR1
                SqlStr = ""
                SqlStr = FetchRecordForReport(SqlStr)
                mRptFileName = "InaamSheet.Rpt"

                If lblIsArrear.Text = "Y" Then
                    mTitle = "Inaam Sheet (Arrear)" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
                Else
                    mTitle = "Inaam Sheet " & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
                End If
            End If
        ElseIf frmPrintSalReg.OptSalSheet.Checked = True Then
            If frmPrintSalReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintSalReg.txtBankName.Text
            End If
            '        If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, "CHEQUE", mBankName) = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, ColCard, ColName, ColDays, ColPaymentType, (sprdAttn.MaxCols - 2), ColBankNo, "CHEQUE", mBankName, ColBankIFSC) = False Then GoTo ERR1

            SqlStr = ""
            SqlStr = FetchRecordForBankReport(SqlStr)
            mRptFileName = "BankSheet.Rpt"

            ''InputBox("Please Enter Bank Name. :", "Bank Name")

            If mBankName = "" Then
                mTitle = "BANK ANNEXURES"
            Else
                mTitle = "BANK ANNEXURES OF " & mBankName
            End If

            If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Checked Then
                mSubTitle = "Perks For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
            Else
                If lblIsArrear.Text = "Y" Then
                    mSubTitle = "Arrear For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                Else
                    mSubTitle = "Salary For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                End If
            End If

        ElseIf frmPrintSalReg.optBankTxt.Checked = True Then
            If frmPrintSalReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintSalReg.txtBankName.Text
            End If

            If lblIsArrear.Text = "P" Then
                pNarr = "BY PERKS " & UCase(lblYear.Text)
            Else
                'If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                '    pNarr = IIf(lblIsArrear.Text = "N", "Salary For the Month :", "BY ARREAR OF ") & UCase(lblYear.Text)
                'Else
                pNarr = IIf(lblIsArrear.Text = "N", "BY SALARY OF ", "BY ARREAR OF ") & UCase(lblYear.Text)
                'End If

            End If
            If CreateTxtFileForBank(sprdAttn, ColCard, ColName, ColPaymentType, ColBankNo, (sprdAttn.MaxCols), mBankName, pNarr, sprdAttn.MaxRows - 2) = False Then GoTo ERR1

            frmPrintSalReg.Close()
            Exit Sub
        ElseIf frmPrintSalReg.OptDeductionList.Checked = True Then

            If Trim(frmPrintSalReg.txtDeductionName.Text) <> "" Then
                For cntCol = 1 To sprdAttn.MaxCols
                    sprdAttn.Row = 0
                    sprdAttn.Col = cntCol
                    If UCase(Trim(frmPrintSalReg.txtDeductionName.Text)) = UCase(Trim(sprdAttn.Text)) Then
                        mCheckCol = cntCol
                        Exit For
                    End If
                Next
                If FillDed_DataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows - 1, mCheckCol) = False Then GoTo ERR1

                SqlStr = ""

                If MainClass.ValidateWithMasterTable(frmPrintSalReg.txtDeductionName.Text, "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE=" & ConLoan & "") = True Then
                    mRptFileName = "SalBankDeductSheet.Rpt"
                    If cboOrder1.SelectedIndex = 0 Then
                        SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD4,FIELD2")
                    Else
                        SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD4,FIELD1")
                    End If
                ElseIf frmPrintSalReg.txtDeductionName.Text = "LIC" Then
                    mRptFileName = "SalLICDeductSheet.Rpt"
                    If cboOrder1.SelectedIndex = 0 Then
                        SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD6,FIELD2")
                    Else
                        SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD6,FIELD1")
                    End If
                Else
                    mRptFileName = "SalDeductSheet.Rpt"
                    SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
                End If

                mTitle = "DEDUCTION LIST (" & frmPrintSalReg.txtDeductionName.Text & ")"

                If lblIsArrear.Text = "Y" Then
                    mTitle = mTitle & "Arrear"
                End If

                If lblIsArrear.Text = "Y" Then
                    mSubTitle = "For the Month Paid: " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                Else
                    mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                End If
            Else
                MsgInformation("Please Select Deduction Head Name")
                frmPrintSalReg.Close()
                Exit Sub
            End If
        End If

        If lblShowType.Text = "D" Then
            mTitle = mTitle & " (Checking Purpose)"
        End If

        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
        frmPrintSalReg.Close()
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
    Private Sub PayslipeMail()

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


        frmPrintSalReg.OptSalReg.Enabled = False
        frmPrintSalReg.optCashSheet.Enabled = False
        frmPrintSalReg.OptSalSheet.Enabled = False
        frmPrintSalReg.optBankTxt.Enabled = False
        frmPrintSalReg.optInaam.Enabled = False
        frmPrintSalReg.fraBankName.Enabled = False
        frmPrintSalReg.OptDeductionList.Enabled = False

        frmPrintSalReg.optPaySlip.Enabled = True
        frmPrintSalReg.optPaySlip.Checked = True

        frmPrintSalReg.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If


        'Insert Data from Grid to PrintDummyData Table...

        If lblIsArrear.Text = "Y" Then
            mSubTitle = "For the Month Paid: " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        Else
            mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        End If

        If lblIsArrear.Text = "P" Or lblIsArrear.Text = "V" Then
            mSubTitle = mSubTitle & IIf(Val(cboMonthTerm.Text) = 1, " (1st Half)", IIf(Val(cboMonthTerm.Text) = 2, " (@nd Half)", " (3rd Half)"))
        End If

        mSubTitle = mSubTitle & IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Department : " & cboDept.Text, " ")

        If cboCostCenter.SelectedIndex <> 0 Then
            mSubTitle = mSubTitle & IIf(chkCostC.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Cost Center : " & cboCostCenter.Text, " ")
        End If


        mSubTitle = mSubTitle & IIf(chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Division : " & cboDivision.Text, " ")


        If frmPrintSalReg.optAll(0).Checked Then
            ColStartRow = 1
            ColEndRow = sprdAttn.MaxRows - 2
        Else
            For cntRow = 1 To sprdAttn.MaxRows
                sprdAttn.Row = cntRow
                sprdAttn.Col = ColCard
                If UCase(Trim(sprdAttn.Text)) = UCase(Trim(frmPrintSalReg.TxtEmpCode.Text)) Then
                    ColStartRow = cntRow
                    ColEndRow = cntRow
                    Exit For
                End If
            Next
        End If
        If ColEndRow = 0 Then
            MsgBox("Such Employee Salary is not Updated...", MsgBoxStyle.Information)
            Exit Sub
        End If
        If FillPaySlipIntoPrintDummy(sprdAttn, ColStartRow, ColEndRow) = False Then GoTo ERR1



        SqlStr = ""
        SqlStr = FetchRecordForPaySlip(SqlStr)


        If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Checked Then
            mRptFileName = "ePerksSlip.Rpt"
            mTitle = "" & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        Else
            mRptFileName = "PaySlip.Rpt"
            If lblIsArrear.Text = "Y" Then
                mTitle = "ARREAR SLIP " & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
            Else
                mTitle = "PAY SLIP " & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
            End If
        End If
        mSubTitle = "" ''"[ Rule 26(2) ]"


        Call ShoweMailReport(SqlStr, mRptFileName, mTitle, mSubTitle)

        frmPrintSalReg.Close()
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

    Private Function FetchRecordForPaySlip(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM TEMP_PAYSLIP_TRN " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        mSqlStr = mSqlStr & " ORDER BY "

        If cboOrder1.SelectedIndex = 0 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_NAME, EMP_CODE,"
        ElseIf cboOrder1.SelectedIndex = 1 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_CODE, "
        ElseIf cboOrder1.SelectedIndex = 2 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_CATG, "
        ElseIf cboOrder1.SelectedIndex = 3 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_DEPT_DESC, "
        End If

        If cboOrder2.SelectedIndex = 1 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_NAME, EMP_CODE,"
        ElseIf cboOrder2.SelectedIndex = 2 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_CODE, "
        ElseIf cboOrder2.SelectedIndex = 3 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_CATG, "
        ElseIf cboOrder2.SelectedIndex = 4 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_DEPT_DESC, "
        End If

        If cboOrder3.SelectedIndex = 1 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_NAME, EMP_CODE,"
        ElseIf cboOrder3.SelectedIndex = 2 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_CODE, "
        ElseIf cboOrder3.SelectedIndex = 3 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_CATG, "
        ElseIf cboOrder3.SelectedIndex = 4 Then
            mSqlStr = mSqlStr & vbCrLf & " EMP_DEPT_DESC, "
        End If

        mSqlStr = mSqlStr & vbCrLf & " SUBROW"

        '    If OptDN.Value = True Then
        '        mSqlStr = mSqlStr & " ORDER BY EMP_DEPT_DESC, EMP_NAME, EMP_CODE, SUBROW "
        '    ElseIf OptDC.Value = True Then
        '        mSqlStr = mSqlStr & " ORDER BY EMP_DEPT_DESC, EMP_CODE, EMP_NAME, SUBROW "
        '    Else
        '        mSqlStr = mSqlStr & " ORDER BY EMP_NAME, EMP_CODE, SUBROW "
        '    End If

        FetchRecordForPaySlip = mSqlStr
    End Function

    Private Function FetchRecordForSalReg(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM TEMP_SALREG_TRN " & vbCrLf _
            & " WHERE  " & vbCrLf _
            & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
            & " ORDER BY SUBROW "

        FetchRecordForSalReg = mSqlStr
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        On Error GoTo ErrPart
        Dim RS As New ADODB.Recordset


        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShoweMailReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mTitle As String, ByRef mSubTitle As String)

        On Error GoTo ErrPart
        Dim crapp As New CRAXDRT.Application
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim objRpt As CRAXDRT.Report
        Dim fPath As String

        Dim mCompanyCode As String
        Dim mEmpCode As String
        Dim SqlStr As String = ""
        Dim mOKCount As Integer
        Dim mNotOKCount As Integer
        Dim empMailId As String
        Dim mMonthName As String

        mNotOKCount = 0
        mOKCount = 0
        mRPTName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        mSqlStr = " SELECT DISTINCT EMP_CODE FROM TEMP_PAYSLIP_TRN WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' ORDER BY EMP_CODE"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mEmpCode = IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                SqlStr = " SELECT * FROM TEMP_PAYSLIP_TRN" & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " ORDER BY SUBROW"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

                If RS.EOF = False Then
                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        empMailId = MasterNo
                    End If
                    empMailId = Trim(empMailId)
                    objRpt = crapp.OpenReport(mRPTName)

                    Call Connect_Report_To_Database(objRpt, RS, SqlStr)
                    With objRpt
                        Call ClearCRpt8Formulas(objRpt)
                        .DiscardSavedData()
                        .Database.SetDataSource(RS)
                        SetCrpteMail(objRpt, 1, mTitle, mSubTitle)
                        .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint
                    End With

                    mCompanyCode = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
                    mMonthName = VB6.Format(lblYear.Text, "MMMYYYY")
                    fPath = mLocalPath & "\ePaySlip" & mMonthName & mCompanyCode & mEmpCode & ".pdf"

                    With objRpt
                        .ExportOptions.FormatType = CRAXDDRT.CRExportFormatType.crEFTPortableDocFormat
                        .ExportOptions.DestinationType = CRAXDDRT.CRExportDestinationType.crEDTDiskFile
                        .ExportOptions.DiskFileName = fPath
                        '    .ExportOptions.PDFExportAllPages = True
                        .Export(False)
                    End With

                    '                Set objRpt = crapp.CanClose
                    objRpt = Nothing

                    If empMailId = "" Or fPath = "" Then
                        mNotOKCount = mNotOKCount + 1
                    Else
                        If SendeMail(fPath, empMailId) = False Then GoTo ErrPart
                        mOKCount = mOKCount + 1
                    End If
                End If
                RsTemp.MoveNext()
            Loop

            MsgInformation("Total " & mOKCount & " mail send & Total " & mNotOKCount & " mail not send.")
        End If
        Exit Sub
ErrPart:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function SendeMail(ByRef mAttachmentFile As String, ByRef mTo As String) As Boolean
        On Error GoTo ErrPart

        Dim mCC As String
        Dim mFrom As String
        Dim mSubject As String


        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String

        SendeMail = False

        ' *****************************************************************************
        ' This is where all of the Components Properties are set / Methods called
        ' *****************************************************************************

        mFrom = GetEMailID("PAY_MAIL_TO")
        mCC = GetEMailID("PAY_MAIL_TO")


        mSubject = "Auto Generated Salary Slip for the month of " & VB6.Format(lblRunDate.Text, "MMMM , YYYY")


        mBodyText = "<html><body><br />" & "<b></b>" & mSubject & "<br />" & "<br />" & "<br />" & "Your Faithfully<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"


        If Trim(mTo) <> "" Then
            If SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText) = False Then GoTo ErrPart
        End If

        SendeMail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        SendeMail = False
        '    Resume
    End Function
    'Private Sub Connect_Report_To_Database(Report1 As CRAXDRT.Report, mRs As ADODB.Recordset)
    'On Error GoTo ErrPart
    'Dim I As Integer
    '
    'Dim tables As CRAXDRT.DatabaseTables
    '13/12/2016 Dim csprop As CRAXDRT.ConnectionProperties
    '13/12/2016 Dim cs As CRAXDRT.ConnectionProperty
    'Dim tablecount As Integer
    'Dim CRXDatabase As CRAXDRT.Database
    '
    ''Dim crtable As CRAXDRT.DatabaseTable
    '
    '
    ''  Report1.Database.Tables.Item(1).SetLogOnInfo "HEMA", "SERVER", "HEMAERP", "JUN2011"
    '  Report1.Database.Tables.Item(1).SetDataSource RS, 3
    'CRXDatabase.SetDataSource mRs, 3, 1
    'CRXDatabase.LogOnServer "crdb_odbc.dll", "HEMA", "HEMA", "HEMAERP", "JUN2011"
    '
    'Exit Sub
    '
    'Set tables = Report1.Database.tables
    '
    '
    'tablecount = tables.Count
    '
    'For I = 1 To tablecount
    ''    MsgBox tables.Item(I).Name
    '    ''13/12/2016 Set csprop = tables.Item(tablecount).ConnectionProperties
    '    ''13/12/2016 .Item("Data Source") = DBConSERVICENAME        '' "MYERP"
    ''    csprop.Item("SERVICE NAME") = "MYERP"
    '    ''13/12/2016 csprop.Item("User ID") = DBConUID           ''"TAXATION"
    '    ''13/12/2016 csprop.Item("Password") = DBConPWD          ''"TAX"
    'Next
    'Exit Sub
    'ErrPart:
    '    ErrorMsg err.Description, err.Number, vbCritical
    'End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSalary(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        SetDate(CDate(lblRunDate.Text))

        chkPerksHead.CheckState = IIf(lblIsArrear.Text = "P", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            RefreshScreen()
        Else
            RefreshScreenPerks()
        End If

        cmdReprocessDays.Enabled = True
    End Sub

    Private Sub cmdWelFare_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdWelFare.Click

        On Error GoTo ErrPart
        Dim exlobj As Object
        Dim pFileName As String
        Dim mLineCount As Integer
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mRelation As String
        Dim mFName As String
        Dim mWorkAadharNo As String
        Dim mESINo As String
        Dim mPFNo As String
        Dim mGender As String
        Dim mMobileNo As String
        Dim mDOB As String
        Dim mGrossWage As Double
        Dim mDOJ As String
        Dim mDOR As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mSalHeadName As String
        Dim mCheckCol As Integer
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor



        If MainClass.ValidateWithMasterTable(ConWelfare, "TYPE", "NAME", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE=" & ConWelfare & "") = True Then
            mSalHeadName = Trim(MasterNo)
        Else
            mSalHeadName = ""
        End If

        '    pFileName = mLocalPath & "\Welfare.txt"

        exlobj = CreateObject("excel.application")
        exlobj.Visible = True
        exlobj.Workbooks.Add()

        mLineCount = 1

        With exlobj.ActiveSheet
            .Cells(mLineCount, 1).Value = "Name"
            .Cells(mLineCount, 2).Value = "Relation (Father / Husband)"
            .Cells(mLineCount, 3).Value = "Relation -Person - Name"
            .Cells(mLineCount, 4).Value = "Workers-Adhar-number(Please add 'A' before Aadhaar number)"
            .Cells(mLineCount, 5).Value = "ESI-No."
            .Cells(mLineCount, 6).Value = "EPF-No."
            .Cells(mLineCount, 7).Value = "Gender (Male Or Female)"
            .Cells(mLineCount, 8).Value = "Mobile"
            .Cells(mLineCount, 9).Value = "DOB(Format: dd-mm-yyyy)"
            .Cells(mLineCount, 10).Value = "Gross -Wage"
            .Cells(mLineCount, 11).Value = "Date-Of-Joining(Format: dd-mm-yyyy)"
            .Cells(mLineCount, 12).Value = "Date-Of-Relieving(Format: dd-mm-yyyy)"

            '        .Cells(mLineCount, 6).Font.Name = "Verdana"
            '        .Cells(mLineCount, 6).Font.bold = True:

        End With

        With sprdAttn
            For cntRow = 1 To .MaxRows - 2

                .Row = cntRow
                .Col = ColCard
                mEmpCode = Trim(.Text)

                For cntCol = 1 To sprdAttn.MaxCols
                    sprdAttn.Row = 0
                    sprdAttn.Col = cntCol
                    If UCase(mSalHeadName) = UCase(Trim(sprdAttn.Text)) Then
                        mCheckCol = cntCol
                        Exit For
                    End If
                Next

                .Row = cntRow
                .Col = mCheckCol
                mGrossWage = CDbl(VB6.Format(Val(.Text), "0.00"))

                mSqlStr = "SELECT EMP_CODE, EMP_NAME, EMP_MOBILE_NO, EMP_SEX, " & vbCrLf & " EMP_DOB, EMP_DOJ, EMP_PF_ACNO, EMP_LEAVE_DATE," & vbCrLf & " EMP_LEAVE_REASON, EMP_FNAME, EMP_ESI_NO" & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                mEmpName = ""
                mRelation = ""
                mFName = ""
                mWorkAadharNo = ""
                mESINo = ""
                mPFNo = ""
                mGender = ""
                mGender = ""
                mMobileNo = ""
                mDOB = ""
                mDOJ = ""
                mDOR = ""

                If RsTemp.EOF = False Then
                    mEmpName = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
                    mRelation = "FATHER"
                    mFName = IIf(IsDbNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value)
                    mWorkAadharNo = ""
                    mESINo = IIf(IsDbNull(RsTemp.Fields("EMP_ESI_NO").Value), "", RsTemp.Fields("EMP_ESI_NO").Value)
                    mPFNo = IIf(IsDbNull(RsTemp.Fields("EMP_PF_ACNO").Value), "", RsTemp.Fields("EMP_PF_ACNO").Value)
                    mGender = IIf(IsDbNull(RsTemp.Fields("EMP_SEX").Value), "M", RsTemp.Fields("EMP_SEX").Value)
                    mGender = IIf(mGender = "M", "Male", "Female")
                    mMobileNo = IIf(IsDbNull(RsTemp.Fields("EMP_MOBILE_NO").Value), "", RsTemp.Fields("EMP_MOBILE_NO").Value)
                    mDOB = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOB").Value), "", RsTemp.Fields("EMP_DOB").Value), "DD-MM-YYYY")
                    mDOJ = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "DD-MM-YYYY")
                    mDOR = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value), "DD-MM-YYYY")
                End If

                With exlobj.ActiveSheet
                    mLineCount = mLineCount + 1
                    .Cells(mLineCount, 1).Value = mEmpName
                    .Cells(mLineCount, 2).Value = mRelation
                    .Cells(mLineCount, 3).Value = mFName
                    .Cells(mLineCount, 4).Value = mWorkAadharNo
                    .Cells(mLineCount, 5).Value = mESINo
                    .Cells(mLineCount, 6).Value = mPFNo
                    .Cells(mLineCount, 7).Value = mGender
                    .Cells(mLineCount, 8).Value = mMobileNo
                    .Cells(mLineCount, 9).Value = "'" & mDOB
                    .Cells(mLineCount, 10).Value = mGrossWage
                    .Cells(mLineCount, 11).Value = "'" & mDOJ
                    .Cells(mLineCount, 12).Value = "'" & mDOR

                    '        .Cells(mLineCount, 6).Font.Name = "Verdana"
                    '        .Cells(mLineCount, 6).Font.bold = True:

                End With
            Next
        End With

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmSalaryReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
        cmdReprocessDays.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "Y", IIf(lblShowType.Text = "D", True, False), False)
    End Sub
    Private Sub frmSalaryReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


        Label9.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "Y", True, False)
        cboShowSalary.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "Y", True, False)
        cmdReprocessDays.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "Y", IIf(lblShowType.Text = "D", True, False), False)

        '    OptCC.Value = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub frmSalaryReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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


    'Private Sub UpDYear_DownClick()
    '    lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
    '    SetDate(CDate(lblRunDate.Text))
    '    Call PrintCommand(False)
    '    'RefreshScreen
    'End Sub
    'Private Sub UpDYear_UpClick()
    '    lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
    '    SetDate(CDate(lblRunDate.Text))
    '    Call PrintCommand(False)
    '    'RefreshScreen
    'End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mAddDeduct As Integer
        Dim mPayableSalary As Double
        Dim mTotOtherPayable As Double
        Dim mTotPayable As Double
        Dim mESIAmt As Double
        Dim mTotDeduct As Double
        Dim mNetSalary As Double
        Dim ColPayableAmount As Integer
        Dim ColDeductionAmount As Integer
        Dim mArrearStr As String
        Dim mBankAcctNo As String
        Dim mBankName As String
        Dim mBankIFSC As String
        Dim mCostCCode As String
        Dim mDivisionCode As Double
        Dim mTable As String
        Dim mNetBankSalary As Double
        Dim mNetCashSalary As Double

        Dim mSalaryHeadName As String
        Dim mSalHeadType As Integer
        Dim mIncentive As Double
        Dim mIncentiveActual As Double
        Dim mValue As Double

        If lblShowType.Text = "D" Then
            mTable = IIf(cboShowSalary.SelectedIndex = 0, "PAY_DUMMYSAL_TRN", "PAY_DUMMYACTUAL_SAL_TRN")
        Else
            mTable = IIf(cboShowSalary.SelectedIndex = 0, "PAY_SAL_TRN", "PAY_ACTUAL_SAL_TRN")
        End If

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

        If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = " SELECT SALTRN.*, EMP.EMP_NAME, DECODE(EMP.EMP_GROUP_DOJ,NULL,EMP.EMP_DOJ,EMP.EMP_GROUP_DOJ) AS EMP_DOJ, EMP.EMP_FNAME, EMP.ADD_EMP_CODE, " & vbCrLf _
                & " ADD_DEDUCT.NAME AS ADDNAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ, ADD_DEDUCT.CALC_ON,EMP_BANK_NAME,EMPBANK_IFSC " & vbCrLf _
                & " FROM " & mTable _
                & " SALTRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'" & vbCrLf _
                & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf _
                & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf _
                & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf _
                & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf _
                & " AND SALTRN.SALHEADCODE =ADD_DEDUCT.CODE "

            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"
        Else

            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & ""
        End If

        'SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='AUX430'"

        SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='" & lblIsArrear.Text & "'"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND SALTRN.DEPARTMENT='" & MainClass.AllowSingleQuote(Trim(cboDept.Text)) & "' "
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
                SqlStr = SqlStr & vbCrLf & "AND SALTRN.DIV_CODE=" & mDivisionCode & ""
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND SALTRN.CATEGORY='" & VB.Left(cboCategory.Text, 1) & "' "
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
            SqlStr = SqlStr & vbCrLf & " SALTRN.CATEGORY, "
        ElseIf cboOrder1.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.DEPARTMENT, "
        ElseIf cboOrder1.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.BANKACCTNO, "
        End If

        If cboOrder2.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_NAME, EMP.EMP_CODE,"
        ElseIf cboOrder2.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CODE, "
        ElseIf cboOrder2.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.CATEGORY, "
        ElseIf cboOrder2.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.DEPARTMENT, "
        ElseIf cboOrder2.SelectedIndex = 5 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.BANKACCTNO, "
        End If

        If cboOrder3.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_NAME, EMP.EMP_CODE,"
        ElseIf cboOrder3.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CODE, "
        ElseIf cboOrder3.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.CATEGORY, "
        ElseIf cboOrder3.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.DEPARTMENT, "
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
                    .Text = Trim(mCode)

                    .Col = ColNewCard
                    .Text = IIf(IsDBNull(RsAttn.Fields("ADD_EMP_CODE").Value), "", RsAttn.Fields("ADD_EMP_CODE").Value)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_FNAME").Value), "", RsAttn.Fields("EMP_FNAME").Value)

                    .Col = ColPaymentType
                    .Text = IIf(RsAttn.Fields("PAYMENTMODE").Value = "1", "Cash", "Cheque")

                    mBankAcctNo = IIf(IsDbNull(RsAttn.Fields("BANKACCTNO").Value), "", RsAttn.Fields("BANKACCTNO").Value)
                    mBankName = IIf(IsDbNull(RsAttn.Fields("EMP_BANK_NAME").Value), "", RsAttn.Fields("EMP_BANK_NAME").Value)
                    mBankIFSC = IIf(IsDbNull(RsAttn.Fields("EMPBANK_IFSC").Value), "", RsAttn.Fields("EMPBANK_IFSC").Value)

                    If lblIsArrear.Text = "Y" Then
                        mArrearStr = GetEMPWEFDate(mCode, (lblRunDate.Text))
                        mBankAcctNo = mBankAcctNo & New String(" ", 20 - Len(mBankAcctNo)) & vbNewLine & mArrearStr
                    End If

                    .Col = ColBankNo
                    .Text = mBankAcctNo

                    .Col = ColBankName
                    .Text = mBankName

                    .Col = ColBankIFSC
                    .Text = mBankIFSC

                    .Col = ColDept
                    .Text = IIf(IsDbNull(RsAttn.Fields("Department").Value), "", RsAttn.Fields("Department").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDbNull(RsAttn.Fields("DESG_DESC").Value), "", RsAttn.Fields("DESG_DESC").Value)

                    .Col = ColDOJ
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("EMP_DOJ").Value), "", RsAttn.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                    .Col = ColDays
                    .Text = CStr(RsAttn.Fields("WDAYS").Value)

                    '.Col = ColOTHours
                    '.Text = ""

                    .Col = ColBSalary
                    .Text = MainClass.FormatRupees(IIf(IsDBNull(RsAttn.Fields("BASICSALARY").Value), 0, RsAttn.Fields("BASICSALARY").Value))

                    .Col = ColPSalary
                    .Text = VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00")
                    mPayableSalary = CDbl(VB6.Format(RsAttn.Fields("PAYABLESALARY").Value, "0.00"))
                    mTotPayable = mPayableSalary * IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, 1, 0)


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
                                    .Text = MainClass.FormatRupees(RsAttn.Fields("ACTUALAMOUNT").Value)
                                End If

                                mSalaryHeadName = Trim(UCase(RsAttn.Fields("ADDNAME").Value))

                                If MainClass.ValidateWithMasterTable(UCase(Trim(mSalaryHeadName)), "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                    mSalHeadType = Val(MasterNo)
                                Else
                                    mSalHeadType = 0
                                End If

                                cntCol = cntCol + 1

                                .Col = cntCol
                                If mSalHeadType = ConESI And cboShowSalary.SelectedIndex = 0 Then
                                    mIncentive = GetBankSalary(mCode, lblRunDate.Text, "O", IIf(lblShowType.Text = "D", "Y", "N"), "ESI")
                                Else
                                    mIncentive = 0
                                End If

                                If mSalHeadType = ConAttendanceAllw Then
                                    mValue = 0
                                Else
                                    mValue = RsAttn.Fields("PayableAmount").Value
                                End If

                                If mSalHeadType = ConESI Then
                                    mESIAmt = mValue + mIncentive

                                    If mESIAmt > Int(mESIAmt) Then
                                        mESIAmt = Int(mESIAmt) + 1
                                    Else
                                        mESIAmt = System.Math.Round(mESIAmt, 0)
                                    End If
                                    .Text = MainClass.FormatRupees(mESIAmt)
                                Else
                                    .Text = MainClass.FormatRupees(mValue + mIncentive)
                                End If


                                If RsAttn.Fields("ADDDEDUCT").Value = ConEarning Or RsAttn.Fields("ADDDEDUCT").Value = ConPerks Then
                                    If mSalHeadType = ConAttendanceAllw Then
                                    Else
                                        mTotPayable = mTotPayable + RsAttn.Fields("PayableAmount").Value
                                    End If

                                ElseIf RsAttn.Fields("ADDDEDUCT").Value = ConDeduct Then
                                    If mSalHeadType = ConESI Then
                                        If cboShowSalary.SelectedIndex = 0 Then
                                            mIncentive = GetBankSalary(mCode, lblRunDate.Text, "O", IIf(lblShowType.Text = "D", "Y", "N"), "ESI")

                                            mESIAmt = RsAttn.Fields("PayableAmount").Value + mIncentive

                                            If mESIAmt > Int(mESIAmt) Then
                                                mESIAmt = Int(mESIAmt) + 1
                                            Else
                                                mESIAmt = System.Math.Round(mESIAmt, 0)
                                            End If

                                            mTotDeduct = mTotDeduct + mESIAmt
                                        Else
                                            mTotDeduct = mTotDeduct + RsAttn.Fields("PayableAmount").Value
                                        End If
                                    Else
                                        mTotDeduct = mTotDeduct + RsAttn.Fields("PayableAmount").Value
                                    End If


                                End If
                                Exit For
                            End If
                        Next
                        RsAttn.MoveNext()
                        If RsAttn.EOF = True Then Exit Do
                    Loop

                    .Row = cntRow
                    If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then 'If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked And cboShowSalary.SelectedIndex = 1 Then
                        .Col = ColPayableAmount - 1
                        If cboShowSalary.SelectedIndex = 0 And lblShowType.Text <> "D" Then
                            mIncentive = GetBankSalary(mCode, lblRunDate.Text, "O", IIf(lblShowType.Text = "D", "Y", "N"), "")
                            'mIncentive = mIncentive - GetIncentive_Adj(mCode, lblRunDate.Text)
                            mIncentive = mIncentive + GetAttnAwardAmount(ConAttendanceAllw, mCode, lblRunDate.Text)
                            mIncentive = mIncentive - GetAttnAwardAmount_Adj(-1, mCode, lblRunDate.Text, "S")
                            mIncentive = mIncentive - GetAttnAwardAmount_Adj(-1, mCode, lblRunDate.Text, "I")
                        Else
                            mIncentive = 0
                        End If

                        .Text = MainClass.FormatRupees(mIncentive)

                        mTotPayable = mTotPayable + mIncentive ''Sandeep 

                        .Col = ColPayableAmount
                        .Text = MainClass.FormatRupees(mTotPayable)         ''Sandeep mTotPayable + mIncentive)

                        .Col = ColDeductionAmount
                        .Text = MainClass.FormatRupees(mTotDeduct)
                    End If


                    .Col = .MaxCols - 3

                    If cboShowSalary.SelectedIndex = 0 Then
                        If lblShowType.Text = "D" Then
                            mNetBankSalary = GetBankSalary(mCode, lblRunDate.Text, "S", IIf(lblShowType.Text = "D", "Y", "N"), "")
                            mNetBankSalary = mNetBankSalary - mTotDeduct
                        Else
                            mNetBankSalary = (mTotPayable + mTotOtherPayable - mTotDeduct)
                        End If
                    Else
                        mNetBankSalary = (mTotPayable + mTotOtherPayable - mTotDeduct)
                    End If


                    .Text = VB6.Format(mNetBankSalary, "0")
                    .ColHidden = IIf(cboShowSalary.SelectedIndex = 0, False, True)


                    .Col = .MaxCols - 2
                    mNetSalary = (mTotPayable + mTotOtherPayable - mTotDeduct)
                    .Text = VB6.Format(mNetSalary, "0")
                    .ColHidden = IIf(cboShowSalary.SelectedIndex = 0, True, False)

                    .Col = .MaxCols - 1
                    'If cboShowSalary.SelectedIndex = 0 Then
                    '    mIncentive = 0
                    'Else
                    mIncentiveActual = GetBankSalary(mCode, lblRunDate.Text, "O", IIf(lblShowType.Text = "D", "Y", "N"), "")
                    mIncentiveActual = mIncentiveActual + GetAttnAwardAmount(ConAttendanceAllw, mCode, lblRunDate.Text)
                    'End If

                    .Text = VB6.Format(mIncentiveActual, "0")
                    .ColHidden = IIf(cboShowSalary.SelectedIndex = 0, True, False)

                    .Col = .MaxCols
                    If cboShowSalary.SelectedIndex = 0 Then
                        mNetCashSalary = IIf(Math.Abs(mNetBankSalary - mNetSalary) <= 1, 0, mNetBankSalary - mNetSalary)
                    Else
                        mNetCashSalary = 0
                    End If

                    .Text = VB6.Format(mNetCashSalary, "0")
                    .ColHidden = IIf(cboShowSalary.SelectedIndex = 0, IIf(lblShowType.Text = "D", False, True), True)


                    If (mTotPayable + mTotDeduct + mNetSalary) = 0 And lblIsArrear.Text = "Y" Then
                        .Row = cntRow
                        For cntCol = 1 To .MaxCols
                            .Col = cntCol
                            .Text = ""
                        Next
                    Else
                        cntRow = cntRow + 1
                    End If
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
    Private Sub RefreshScreenPerks()

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
        Dim mPaidDate As String
        Dim mDivisionCode As Double
        Dim mBankName As String
        Dim mBankIFSC As String

        Call FillHeading()

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
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

        ''& " AND EMP.COMPANY_CODE =DESG.COMPANY_CODE" & vbCrLf _
        '& " AND EMP.EMP_DESG_CODE =DESG.DESG_CODE " & vbCrLf _
        '
        mPaidDate = "01/" & VB6.Format(lblRunDate.Text, "MMM-YYYY")

        SqlStr = " SELECT SALTRN.*, EMP.ADD_EMP_CODE, EMP.EMP_NAME, DECODE(EMP.EMP_GROUP_DOJ,NULL,EMP.EMP_DOJ,EMP.EMP_GROUP_DOJ) AS EMP_DOJ, EMP.EMP_FNAME, " & vbCrLf & " ADD_DEDUCT.NAME AS ADDNAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ,EMP_BANK_NO,DEPT_DESC," & vbCrLf & " GETEMPDESG ('" & RsCompany.Fields("COMPANY_CODE").Value & "',EMP.EMP_CODE,TO_DATE('" & VB6.Format(mPaidDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC,EMP_BANK_NAME,EMPBANK_IFSC " & vbCrLf & " FROM PAY_PERKS_TRN SALTRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT, PAY_DEPT_MST DEPT " & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND EMP.COMPANY_CODE =DEPT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE =DEPT.DEPT_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.ADD_DEDUCTCODE =ADD_DEDUCT.CODE "

        SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND ADD_DEDUCT.PAYMENT_TYPE='M'"


        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"
        End If

        If lblIsArrear.Text = "P" Then
            SqlStr = SqlStr & vbCrLf & " AND CALC_ON <> " & ConCalcVariable & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND CALC_ON = " & ConCalcVariable & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND SALTRN.BOOKTYPE='" & lblIsArrear.Text & "'"

        SqlStr = SqlStr & vbCrLf & " AND SALTRN.PAID_WEEK='" & Val(cboMonthTerm.Text) & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(Trim(cboDept.Text)) & "' "
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
                SqlStr = SqlStr & vbCrLf & "AND SALTRN.DIV_CODE=" & mDivisionCode & ""
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY "

        If cboOrder1.SelectedIndex = 0 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_NAME, EMP.EMP_CODE,"
        ElseIf cboOrder1.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CODE, "
        ElseIf cboOrder1.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CATG, "
        ElseIf cboOrder1.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " DEPT.DEPT_DESC, "
        ElseIf cboOrder1.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.BANKACCTNO, "
        End If

        If cboOrder2.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_NAME, EMP.EMP_CODE,"
        ElseIf cboOrder2.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CODE, "
        ElseIf cboOrder2.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CATG, "
        ElseIf cboOrder2.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " DEPT.DEPT_DESC, "
        ElseIf cboOrder2.SelectedIndex = 5 Then
            SqlStr = SqlStr & vbCrLf & " SALTRN.BANKACCTNO, "
        End If

        If cboOrder3.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_NAME, EMP.EMP_CODE,"
        ElseIf cboOrder3.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CODE, "
        ElseIf cboOrder3.SelectedIndex = 3 Then
            SqlStr = SqlStr & vbCrLf & " EMP.EMP_CATG, "
        ElseIf cboOrder3.SelectedIndex = 4 Then
            SqlStr = SqlStr & vbCrLf & " DEPT.DEPT_DESC, "
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
                    '                If mCode = "000089" Then MsgBox RsAttn!EMP_CODE
                    .Text = CStr(mCode)

                    .Col = ColNewCard
                    .Text = IIf(IsDBNull(RsAttn.Fields("ADD_EMP_CODE").Value), "", RsAttn.Fields("ADD_EMP_CODE").Value)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_FNAME").Value), "", RsAttn.Fields("EMP_FNAME").Value)

                    .Col = ColPaymentType
                    .Text = IIf(RsAttn.Fields("PAYMENT_TYPE").Value = "2", "Cash", "Cheque")

                    mBankAcctNo = IIf(IsDbNull(RsAttn.Fields("EMP_BANK_NO").Value), "", RsAttn.Fields("EMP_BANK_NO").Value)
                    mBankName = IIf(IsDbNull(RsAttn.Fields("EMP_BANK_NAME").Value), "", RsAttn.Fields("EMP_BANK_NAME").Value)
                    mBankIFSC = IIf(IsDbNull(RsAttn.Fields("EMPBANK_IFSC").Value), "", RsAttn.Fields("EMPBANK_IFSC").Value)

                    If lblIsArrear.Text = "Y" Then
                        mArrearStr = GetEMPWEFDate(mCode, (lblRunDate.Text))
                        mBankAcctNo = mBankAcctNo & New String(" ", 15 - Len(mBankAcctNo)) & vbNewLine & mArrearStr
                    End If

                    .Col = ColBankNo
                    .Text = mBankAcctNo

                    .Col = ColBankName
                    .Text = mBankName

                    .Col = ColBankIFSC
                    .Text = mBankIFSC

                    .Col = ColDept
                    .Text = IIf(IsDbNull(RsAttn.Fields("DEPT_DESC").Value), "", RsAttn.Fields("DEPT_DESC").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDbNull(RsAttn.Fields("DESG_DESC").Value), "", RsAttn.Fields("DESG_DESC").Value)

                    .Col = ColDOJ
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("EMP_DOJ").Value), "", RsAttn.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                    .Col = ColDays
                    .Text = CStr(0) ''CStr(RsAttn!WDAYS)

                    '.Col = ColOTHours
                    '.Text = CStr(0) ''CStr(RsAttn!WDAYS)

                    .Col = ColBSalary
                    .Text = CStr(0) ''MainClass.FormatRupees(RsAttn!BASICSALARY)

                    .Col = ColPSalary
                    .Text = CStr(0) ''Format(RsAttn!PAYABLESALARY, "0.00")
                    mPayableSalary = 0 ''Format(RsAttn!PAYABLESALARY, "0.00")
                    mTotPayable = mPayableSalary * IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, 1, 0)


                    Do While mCode = RsAttn.Fields("EMP_CODE").Value
                        For cntCol = ColBSalary To .MaxCols
                            .Row = 0
                            .Col = cntCol
                            If Trim(UCase(.Text)) = Trim(UCase(RsAttn.Fields("ADDNAME").Value)) Then
                                .Row = cntRow

                                .Col = cntCol
                                .Text = MainClass.FormatRupees(RsAttn.Fields("Amount"))

                                mTotPayable = mTotPayable + RsAttn.Fields("Amount").Value
                                Exit For
                            End If
                        Next
                        RsAttn.MoveNext()
                        If RsAttn.EOF = True Then Exit Do
                    Loop

                    .Row = cntRow

                    If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        .Col = ColPayableAmount
                        .Text = MainClass.FormatRupees(mTotPayable)

                        .Col = ColDeductionAmount
                        .Text = MainClass.FormatRupees(mTotDeduct)
                    Else
                        .Col = ColPayableAmount
                        .Text = MainClass.FormatRupees(mTotPayable)
                    End If

                    .Col = .MaxCols
                    mNetSalary = (mTotPayable - mTotDeduct)
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
    Private Function CreateTxtFileForBankOLD(ByRef pBankName As String) As Boolean
        On Error GoTo ErrPart
        Dim mLineCount As Integer
        Dim pPageNo As Integer
        Dim cntRow As Double
        Dim pFileName As String
        Dim mAmount As String
        Dim mEmpName As String
        Dim mCardNo As String
        Dim mCheckBankName As String

        mLineCount = 1
        pFileName = mLocalPath & "\BankList.txt"
        ''Shell "ATTRIB +A -R " & pFileName

        Call ShellAndContinue("ATTRIB +A -R " & pFileName)

        With sprdAttn
            If .MaxRows >= 1 Then

                FileOpen(1, pFileName, OpenMode.Output)
                For cntRow = 1 To .MaxRows - 2
                    If mLineCount = 1 Then
                        pPageNo = pPageNo + 1
                    End If

                    .Row = cntRow
                    .Col = ColCard
                    mCardNo = Trim(.Text)

                    If MainClass.ValidateWithMasterTable(mCardNo, "EMP_CODE", "EMP_BANK_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCheckBankName = MasterNo
                    Else
                        mCheckBankName = ""
                    End If

                    If Trim(UCase(pBankName)) = Trim(UCase(mCheckBankName)) Or pBankName = "" Then

                        .Col = ColPaymentType
                        If UCase(.Text) = "CHEQUE" Then
                            .Col = .MaxCols
                            If Val(.Text) > 0 Then
                                .Col = ColBankNo
                                If lblIsArrear.Text = "N" Then
                                    Print(1, TAB(0), Trim(.Text))
                                Else
                                    Print(1, TAB(0), VB.Left(Trim(.Text), 15))
                                End If

                                .Col = ColName
                                mEmpName = VB.Left(Trim(.Text), 60)
                                Print(1, TAB(17), mEmpName)

                                .Col = .MaxCols
                                mAmount = New String(" ", 18 - Len(Trim(.Text))) & Trim(.Text)
                                Print(1, TAB(76), mAmount)

                                If lblIsArrear.Text = "N" Then
                                    Print(1, TAB(94), "BY SALARY OF " & UCase(lblYear.Text))
                                Else
                                    Print(1, TAB(94), "BY ARREAR OF " & UCase(lblYear.Text))
                                End If

                                PrintLine(1, TAB(124), "C")

                                mLineCount = mLineCount + 1
                                If mLineCount = 60 Then
                                    mLineCount = 1
                                End If
                            End If
                        End If
                    End If
                Next
                FileClose(1)
            End If
        End With

        Shell("ATTRIB +R -A " & pFileName)
        Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)

        CreateTxtFileForBankOLD = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        CreateTxtFileForBankOLD = False
        ''Resume
        FileClose(1)
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAttn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .set_ColWidth(ColSNO, 5)

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColCard, 7)

            .Col = ColNewCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColNewCard, 7)


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

            .Col = ColBankName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBankName, 10)
            .ColHidden = True

            .Col = ColBankIFSC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBankIFSC, 10)
            .ColHidden = True

            .Col = ColDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDays, 6)

            '.Col = ColOTHours
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatMax = CDbl("9999999.99")
            '.TypeFloatMin = CDbl("-9999999.99")
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '.set_ColWidth(ColOTHours, 6)


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

        cboMonthTerm.Items.Clear()
        cboMonthTerm.Items.Add("1")
        cboMonthTerm.Items.Add("2")
        cboMonthTerm.Items.Add("3")
        cboMonthTerm.SelectedIndex = 0

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

        cboShowSalary.Items.Clear()
        cboShowSalary.Items.Add("Form 1 Salary")
        cboShowSalary.Items.Add("Paid Salary")

        cboShowSalary.SelectedIndex = 0

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


        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))
    End Sub
    Private Function FillDataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef mPaymentType As String, ByRef pBankName As String) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mWDays As String
        Dim mNetPay As String
        Dim mBankAcct As String
        Dim mCheckBankName As String
        Dim mNewEmpCode As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.BeginTrans()
        lblNetPay.Text = CStr(0)
        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum
            GridName.Col = ColPaymentType
            If UCase(GridName.Text) = UCase(mPaymentType) Then
                GridName.Col = ColCard
                mEmpCode = MainClass.AllowSingleQuote(GridName.Text)

                GridName.Col = ColNewCard
                mNewEmpCode = MainClass.AllowSingleQuote(GridName.Text)

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_BANK_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCheckBankName = MasterNo
                Else
                    mCheckBankName = ""
                End If

                If Trim(UCase(pBankName)) = Trim(UCase(mCheckBankName)) Or pBankName = "" Then

                    GridName.Col = ColName
                    mEmpName = MainClass.AllowSingleQuote(GridName.Text)

                    GridName.Col = ColDays
                    mWDays = GridName.Text

                    GridName.Col = GridName.MaxCols
                    mNetPay = GridName.Text

                    lblNetPay.Text = CStr(Val(lblNetPay.Text) + CDbl(mNetPay))

                    GridName.Col = ColBankNo
                    If lblIsArrear.Text = "N" Then
                        mBankAcct = MainClass.AllowSingleQuote(Trim(GridName.Text))
                    Else
                        mBankAcct = MainClass.AllowSingleQuote(VB.Left(Trim(GridName.Text), 15))
                    End If

                    SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "','" & mWDays & "', " & vbCrLf & " '" & mNetPay & "','" & mBankAcct & "') "
                    PubDBCn.Execute(SqlStr)
                End If
            End If
        Next
        PubDBCn.CommitTrans()
        FillDataIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillDataIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FillDed_DataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef pCheckCol As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mDeductAmount As String
        Dim mBankCode As String
        Dim mBankName As String
        Dim mImpCode As String
        Dim mImpName As String
        Dim mLICNo As String
        Dim mLOANACNO As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.BeginTrans()
        lblNetPay.Text = CStr(0)
        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum

            GridName.Col = ColCard
            mEmpCode = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColName
            mEmpName = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = pCheckCol
            mDeductAmount = VB6.Format(GridName.Text, "0.00")

            mBankCode = ""
            mImpCode = ""
            mBankName = ""
            mImpName = ""
            mLICNo = ""

            If Val(mDeductAmount) <> 0 Then
                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "ADV_ACCOUNT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mBankCode = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "IMPREST_ACCOUNT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mImpCode = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mBankCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mBankName = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mImpCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mImpName = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_LICNO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mLICNo = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_LOANNO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mLOANACNO = MasterNo
                End If

                SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW,FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, FIELD5, FIELD6,FIELD7) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "','" & mDeductAmount & "', '" & mBankName & "', '" & mImpName & "', '" & mLICNo & "','" & mLOANACNO & "') "
                PubDBCn.Execute(SqlStr)
            End If
        Next
        PubDBCn.CommitTrans()
        FillDed_DataIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillDed_DataIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FillPaySlipIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer) As Boolean

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
        Dim mEmpFName As String
        Dim mDOJ As String
        Dim mDepartment As String
        Dim mDesignation As String
        Dim mPFNo As String
        Dim mUIDNo As String
        Dim mBankAcct As String
        Dim mActualDays As Integer
        Dim mWDays As Double
        Dim mPaymentType As String

        Dim mBSalary As Double
        Dim mPSalary As Double
        Dim mLeaves As String
        Dim mRemarks As String

        Dim mGrossDeduct As Double
        Dim mGrossPay As Double
        Dim mNetPay As Double
        Dim mGrossEarn As Double
        Dim mActualGrossEarn As Double
        Dim mNetPayInWord As String
        Dim mCategory As String
        Dim mESINo As String
        Dim mDOB As String
        Dim mHoliDays As Double
        Dim mLeaveDate As String
        Dim mSalHeadType As Integer
        Dim ColInaam As Integer
        Dim ColOthThanInaam As Integer
        Dim mInaamAmount As Double
        Dim mOthThanInaamAmount As Double

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PAYSLIP_TRN WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        PubDBCn.Execute(SqlStr)

        GridName.Row = 0

        For ColNum = 0 To GridName.MaxCols
            GridName.Col = ColNum

            If MainClass.ValidateWithMasterTable(UCase(Trim(GridName.Text)), "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSalHeadType = Val(MasterNo)
            Else
                mSalHeadType = 0
            End If
            If Val(CStr(mSalHeadType)) = ConINAAM Then
                ColInaam = ColNum
            End If

            If Val(CStr(mSalHeadType)) = ConOtherEarningVar Then
                ColOthThanInaam = ColNum
            End If

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

            GridName.Col = ColCard
            mEmpCode = GridName.Text

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCategory = MasterNo
            End If

            mLeaves = GetBalLeave(mEmpCode)

            GridName.Col = ColName
            mEmpName = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColFName
            mEmpFName = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColDays
            mWDays = CDbl(GridName.Text)

            GridName.Col = ColDept
            mDepartment = GridName.Text

            GridName.Col = ColDesg
            mDesignation = GridName.Text

            GridName.Col = ColPaymentType
            mPaymentType = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColBankNo
            If lblIsArrear.Text = "N" Then
                mBankAcct = MainClass.AllowSingleQuote(GridName.Text)
            Else
                mBankAcct = MainClass.AllowSingleQuote(VB.Left(Trim(GridName.Text), 15))
            End If

            GridName.Col = ColDOJ
            mDOJ = GridName.Text

            GridName.Col = ColBSalary
            mBSalary = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            GridName.Col = ColPSalary
            mPSalary = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_PF_ACNO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPFNo = MasterNo
            Else
                mPFNo = ""
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_ESI_NO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mESINo = MasterNo
            Else
                mESINo = ""
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "UID_NO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mUIDNo = MasterNo
            Else
                mUIDNo = ""
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DOB", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDOB = Trim(MasterNo)
            Else
                mDOB = ""
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLeaveDate = Trim(MasterNo)
            Else
                mLeaveDate = ""
            End If

            mHoliDays = GetHolidays(mEmpCode, mLeaveDate)

            Colcnt = 1
            GridName.Col = ColBSalary
            mActualGrossEarn = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
            mInaamAmount = 0
            mOthThanInaamAmount = 0

            GridName.Col = ColPSalary + 1
            Do While GridName.Col < GridName.MaxCols

                If GridName.Col < ColTotPayable Then
                    If GridName.Col = ColInaam - 1 Or GridName.Col = ColInaam Then
                        mEmpEarnData(Colcnt).mRate = 0
                        mEmpEarnData(Colcnt).mPayable = 0
                        mEmpEarnData(Colcnt).mTitle = ""
                        If GridName.Col = ColInaam Then
                            mInaamAmount = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            Colcnt = Colcnt + 1
                        End If
                    ElseIf GridName.Col = ColOthThanInaam - 1 Or GridName.Col = ColOthThanInaam Then
                        mEmpEarnData(Colcnt).mRate = 0
                        mEmpEarnData(Colcnt).mPayable = 0
                        mEmpEarnData(Colcnt).mTitle = ""
                        If GridName.Col = ColOthThanInaam Then
                            mOthThanInaamAmount = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            Colcnt = Colcnt + 1
                        End If
                    Else
                        If VB.Left(arrsal(GridName.Col), 4) = "RATE" Then
                            mEmpEarnData(Colcnt).mRate = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            mActualGrossEarn = mActualGrossEarn + CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                        Else
                            mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                            Colcnt = Colcnt + 1
                        End If
                    End If
                ElseIf GridName.Col = ColTotPayable Then
                    mGrossEarn = MainClass.FormatRupees(CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))) - mInaamAmount - mOthThanInaamAmount
                    MaxColcnt = Colcnt
                    Colcnt = 1
                ElseIf GridName.Col > ColTotPayable And GridName.Col < ColTotDeduction Then

                    If VB.Left(arrsal(GridName.Col), 4) = "RATE" Then
                        mEmpDeductData(Colcnt).mRate = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                    Else
                        mEmpDeductData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                        mEmpDeductData(Colcnt).mTitle = arrsal(GridName.Col)
                        Colcnt = Colcnt + 1
                    End If
                ElseIf GridName.Col = ColTotDeduction Then
                    mGrossDeduct = MainClass.FormatRupees(CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0)))
                End If

                GridName.Col = GridName.Col + 1
            Loop

            If MaxColcnt < Colcnt Then
                MaxColcnt = Colcnt
            End If

            mGrossPay = MainClass.FormatRupees(CDbl(mGrossEarn) - CDbl(mGrossDeduct) + CDbl(mInaamAmount) + CDbl(mOthThanInaamAmount))
            mNetPay = MainClass.FormatRupees(System.Math.Round(CDbl(mGrossPay), 0))
            '        mRoundOff = MainClass.FormatRupees(Abs(CDbl(mNetPay) - CDbl(mGrossPay)))
            mNetPayInWord = MainClass.RupeesConversion(CDbl(mNetPay))

            For Colcnt = 1 To MaxColcnt
                SqlStr = " INSERT INTO TEMP_PAYSLIP_TRN ( " & vbCrLf _
                    & " USERID, COMPANY_CODE, SUBROW , " & vbCrLf _
                    & " EMP_CODE, EMP_NAME, EMP_FNAME, " & vbCrLf _
                    & " EMP_DEPT_DESC, EMP_DESG_DESC, EMP_DOJ, " & vbCrLf _
                    & " EMP_PF_ACNO, EMP_BANK_NO, ACTUAL_DAYS," & vbCrLf _
                    & " PAYABLE_DAYS, BASIC_SALARY, PAYABLE_BASIC_SALARY,"

                SqlStr = SqlStr & vbCrLf _
                    & " EARN_TITLE,EARN_RATE,EARN_PAYABLE," & vbCrLf _
                    & " DEDUCT_TITLE, DEDUCT_RATE, DEDUCT_PAYABLE," & vbCrLf _
                    & " LEAVES, REMARKS, " & vbCrLf _
                    & " GROSS_SALARY, GROSS_PAYABLE, " & vbCrLf _
                    & " GROSS_DEDUCT, NET_SALARY, EMP_CATG, EMP_ESI_NO, " & vbCrLf _
                    & " EMP_DOB,HOLIDAYS,INAAM,OTHTHAN_INAAM,UID_NO " & vbCrLf _
                    & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Colcnt & ", " & vbCrLf _
                    & " '" & mEmpCode & "','" & mEmpName & "', '" & mEmpFName & "', " & vbCrLf _
                    & " '" & mDepartment & "', '" & mDesignation & "',TO_DATE('" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & mPFNo & "','" & mBankAcct & "', " & Val(mActualDays) & "," & vbCrLf _
                    & " " & Val(mWDays) & ", " & mBSalary & ", " & mPSalary & ", "


                SqlStr = SqlStr & vbCrLf _
                    & " '" & mEmpEarnData(Colcnt).mTitle & "'," & mEmpEarnData(Colcnt).mRate & "," & mEmpEarnData(Colcnt).mPayable & "," & vbCrLf _
                    & " '" & mEmpDeductData(Colcnt).mTitle & "'," & mEmpDeductData(Colcnt).mRate & "," & mEmpDeductData(Colcnt).mPayable & "," & vbCrLf _
                    & " '" & mLeaves & "','" & mNetPayInWord & "', " & vbCrLf & " " & mActualGrossEarn & "," & mGrossEarn & ", " & vbCrLf _
                    & " " & mGrossDeduct & ", " & mNetPay & ", '" & mCategory & "','" & mESINo & "'," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mDOB, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mHoliDays & "," & mInaamAmount & "," & mOthThanInaamAmount & "," & Val(mUIDNo) & " )"


                PubDBCn.Execute(SqlStr)
            Next
        Next
        PubDBCn.CommitTrans()
        FillPaySlipIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        'Resume
        FillPaySlipIntoPrintDummy = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function FillIncentiveSlipIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer) As Boolean

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
        Dim mEmpFName As String
        Dim mDOJ As String
        Dim mDepartment As String
        Dim mDesignation As String
        Dim mPFNo As String
        Dim mUIDNo As String
        Dim mBankAcct As String
        Dim mActualDays As Integer
        Dim mWDays As Double
        Dim mPaymentType As String

        Dim mBSalary As Double
        Dim mPSalary As Double
        Dim mLeaves As String
        Dim mRemarks As String

        Dim mGrossDeduct As Double
        Dim mGrossPay As Double
        Dim mNetPay As Double
        Dim mGrossEarn As Double
        Dim mActualGrossEarn As Double
        Dim mNetPayInWord As String
        Dim mCategory As String
        Dim mESINo As String
        Dim mDOB As String
        Dim mHoliDays As Double
        Dim mLeaveDate As String
        Dim mSalHeadType As Integer
        Dim ColInaam As Integer
        Dim ColOthThanInaam As Integer
        Dim mInaamAmount As Double
        Dim mOthThanInaamAmount As Double

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PAYSLIP_TRN WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        PubDBCn.Execute(SqlStr)

        GridName.Row = 0

        For ColNum = 0 To GridName.MaxCols
            GridName.Col = ColNum

            If MainClass.ValidateWithMasterTable(UCase(Trim(GridName.Text)), "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSalHeadType = Val(MasterNo)
            Else
                mSalHeadType = 0
            End If
            If Val(CStr(mSalHeadType)) = ConINAAM Then
                ColInaam = ColNum
            End If

            If Val(CStr(mSalHeadType)) = ConOtherEarningVar Then
                ColOthThanInaam = ColNum
            End If

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

            GridName.Col = ColCard
            mEmpCode = GridName.Text

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCategory = MasterNo
            End If

            mLeaves = "" ''GetBalLeave(mEmpCode)

            GridName.Col = ColName
            mEmpName = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColFName
            mEmpFName = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColDays
            mWDays = CDbl(GridName.Text)

            GridName.Col = ColDept
            mDepartment = GridName.Text

            GridName.Col = ColDesg
            mDesignation = GridName.Text

            GridName.Col = ColPaymentType
            mPaymentType = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColBankNo
            If lblIsArrear.Text = "N" Then
                mBankAcct = MainClass.AllowSingleQuote(GridName.Text)
            Else
                mBankAcct = MainClass.AllowSingleQuote(VB.Left(Trim(GridName.Text), 15))
            End If

            GridName.Col = ColDOJ
            mDOJ = GridName.Text

            GridName.Col = ColBSalary
            mBSalary = 0 '' CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            GridName.Col = ColPSalary
            mPSalary = 0 '' CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_PF_ACNO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPFNo = MasterNo
            Else
                mPFNo = ""
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_ESI_NO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mESINo = MasterNo
            Else
                mESINo = ""
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "UID_NO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mUIDNo = MasterNo
            Else
                mUIDNo = ""
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DOB", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDOB = Trim(MasterNo)
            Else
                mDOB = ""
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLeaveDate = Trim(MasterNo)
            Else
                mLeaveDate = ""
            End If

            mHoliDays = GetHolidays(mEmpCode, mLeaveDate)

            Colcnt = 1
            GridName.Col = ColBSalary
            mActualGrossEarn = 0 '' CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
            mInaamAmount = 0
            mOthThanInaamAmount = 0

            GridName.Col = ColPSalary + 1
            Do While GridName.Col < GridName.MaxCols

                If GridName.Col < ColTotPayable Then
                    If GridName.Col = ColInaam - 1 Or GridName.Col = ColInaam Then
                        mEmpEarnData(Colcnt).mRate = 0
                        mEmpEarnData(Colcnt).mPayable = 0
                        mEmpEarnData(Colcnt).mTitle = ""
                        If GridName.Col = ColInaam Then
                            mInaamAmount = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            Colcnt = Colcnt + 1
                        End If
                    ElseIf GridName.Col = ColOthThanInaam - 1 Or GridName.Col = ColOthThanInaam Then
                        mEmpEarnData(Colcnt).mRate = 0
                        mEmpEarnData(Colcnt).mPayable = 0
                        mEmpEarnData(Colcnt).mTitle = ""
                        If GridName.Col = ColOthThanInaam Then
                            mOthThanInaamAmount = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            Colcnt = Colcnt + 1
                        End If
                    Else
                        If VB.Left(arrsal(GridName.Col), 4) = "RATE" Then
                            mEmpEarnData(Colcnt).mRate = 0 ''CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            mActualGrossEarn = mActualGrossEarn + 0 ''CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                        Else
                            mEmpEarnData(Colcnt).mPayable = 0 '' CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                            Colcnt = Colcnt + 1
                        End If
                    End If
                ElseIf GridName.Col = ColTotPayable Then
                    mGrossEarn = 0 ''MainClass.FormatRupees(CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))) - mInaamAmount - mOthThanInaamAmount
                    MaxColcnt = Colcnt
                    '                ColCnt = 1
                ElseIf GridName.Col > ColTotPayable And GridName.Col < ColTotDeduction Then

                    If VB.Left(arrsal(GridName.Col), 4) = "RATE" Then
                            mEmpDeductData(Colcnt).mRate = 0 ''CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                        Else
                            mEmpDeductData(Colcnt).mPayable = 0 '' CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            mEmpDeductData(Colcnt).mTitle = "" 'arrsal(GridName.Col)
                            Colcnt = Colcnt + 1
                        End If

                ElseIf GridName.Col = ColTotDeduction Then
                    mGrossDeduct = 0 ''MainClass.FormatRupees(CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0)))
                End If

                GridName.Col = GridName.Col + 1
            Loop

            If MaxColcnt < Colcnt Then
                MaxColcnt = Colcnt
            End If

            mGrossPay = MainClass.FormatRupees(CDbl(mGrossEarn) - CDbl(mGrossDeduct) + CDbl(mInaamAmount) + CDbl(mOthThanInaamAmount))

            mNetPay = MainClass.FormatRupees(System.Math.Round(CDbl(mGrossPay), 0))
            '        mRoundOff = MainClass.FormatRupees(Abs(CDbl(mNetPay) - CDbl(mGrossPay)))
            mNetPayInWord = MainClass.RupeesConversion(CDbl(mNetPay))

            For Colcnt = 1 To MaxColcnt
                SqlStr = " INSERT INTO TEMP_PAYSLIP_TRN ( " & vbCrLf & " USERID, COMPANY_CODE, SUBROW , " & vbCrLf & " EMP_CODE, EMP_NAME, EMP_FNAME, " & vbCrLf & " EMP_DEPT_DESC, EMP_DESG_DESC, EMP_DOJ, " & vbCrLf & " EMP_PF_ACNO, EMP_BANK_NO, ACTUAL_DAYS," & vbCrLf & " PAYABLE_DAYS, BASIC_SALARY, PAYABLE_BASIC_SALARY,"

                SqlStr = SqlStr & vbCrLf & " EARN_TITLE,EARN_RATE,EARN_PAYABLE," & vbCrLf & " DEDUCT_TITLE, DEDUCT_RATE, DEDUCT_PAYABLE," & vbCrLf & " LEAVES, REMARKS, " & vbCrLf & " GROSS_SALARY, GROSS_PAYABLE, " & vbCrLf & " GROSS_DEDUCT, NET_SALARY, EMP_CATG, EMP_ESI_NO, " & vbCrLf & " EMP_DOB,HOLIDAYS,INAAM,OTHTHAN_INAAM,UID_NO " & vbCrLf & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Colcnt & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "', '" & mEmpFName & "', " & vbCrLf & " '" & mDepartment & "', '" & mDesignation & "','" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "', " & vbCrLf & " '" & mPFNo & "','" & mBankAcct & "', " & Val(CStr(mActualDays)) & "," & vbCrLf & " " & Val(CStr(mWDays)) & ", " & mBSalary & ", " & mPSalary & ", "


                SqlStr = SqlStr & vbCrLf & " '" & mEmpEarnData(Colcnt).mTitle & "'," & mEmpEarnData(Colcnt).mRate & "," & mEmpEarnData(Colcnt).mPayable & "," & vbCrLf & " '" & mEmpDeductData(Colcnt).mTitle & "'," & mEmpDeductData(Colcnt).mRate & "," & mEmpDeductData(Colcnt).mPayable & "," & vbCrLf & " '" & mLeaves & "','" & mNetPayInWord & "', " & vbCrLf & " " & mActualGrossEarn & "," & mGrossEarn & ", " & vbCrLf & " " & mGrossDeduct & ", " & mNetPay & ", '" & mCategory & "','" & mESINo & "'," & vbCrLf & " '" & VB6.Format(mDOB, "DD-MMM-YYYY") & "', " & mHoliDays & "," & mInaamAmount & "," & mOthThanInaamAmount & "," & Val(mUIDNo) & " )"


                PubDBCn.Execute(SqlStr)
            Next
        Next
        PubDBCn.CommitTrans()
        FillIncentiveSlipIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        'Resume
        FillIncentiveSlipIntoPrintDummy = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
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
        Dim xADDCol As Integer

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

        If mOtherEarningVar = True Then
            xADDCol = 2
        Else
            xADDCol = 0
        End If

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

            GridName.Col = ColCard
            mEmpCode = GridName.Text

            GridName.Col = ColName
            mEmpName = MainClass.AllowSingleQuote(GridName.Text)

            GridName.Col = ColDays
            mWDays = CDbl(GridName.Text)

            GridName.Col = ColDept
            mDepartment = GridName.Text

            GridName.Col = ColDesg
            mDesignation = GridName.Text

            GridName.Col = ColBankNo
            mBankAcct = MainClass.AllowSingleQuote(GridName.Text)

            mEmpDesc = mEmpName & vbNewLine & mDesignation & vbNewLine & mDepartment & vbNewLine & mBankAcct

            GridName.Col = ColBSalary
            mBSalary = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            GridName.Col = ColPSalary
            mPSalary = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mEmpEarnData(0).mRate = mBSalary
                mEmpEarnData(0).mPayable = mPSalary
                mEmpEarnData(0).mTitle = "BASIC SALARY"
                mEmpEarnData(0).mHeadingDesc = "Rates Payables"
            End If

            Colcnt = 1
            GridName.Col = ColPSalary + 1
            Do While GridName.Col < GridName.MaxCols
                If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    If GridName.Col <= ColTotPayable + xADDCol Then
                        If GridName.Col < ColTotPayable Then
                            If VB.Left(arrsal(GridName.Col), 4) = "RATE" Then
                                mEmpEarnData(Colcnt).mRate = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            Else
                                mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                                mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                                mEmpEarnData(Colcnt).mHeadingDesc = "Rates Payables"
                                Colcnt = Colcnt + 1
                            End If
                        End If
                        If GridName.Col > ColTotPayable Then
                            If VB.Left(arrsal(GridName.Col), 4) = "RATE" Then
                                mEmpEarnData(Colcnt).mRate = 0 ''CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            Else
                                mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                                mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                                mEmpEarnData(Colcnt).mHeadingDesc = "Others Payables"
                                Colcnt = Colcnt + 1
                            End If
                        End If
                    ElseIf GridName.Col > ColTotPayable + xADDCol And GridName.Col < ColTotDeduction Then
                        If VB.Left(arrsal(GridName.Col), 4) = "RATE" Then
                            mEmpEarnData(Colcnt).mRate = 0
                        Else
                            mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0)) * -1
                            mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                            mEmpEarnData(Colcnt).mHeadingDesc = "Deductions"
                            Colcnt = Colcnt + 1
                        End If
                    End If
                Else
                    mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                    mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                    mEmpEarnData(Colcnt).mHeadingDesc = "Rates Payables"
                    Colcnt = Colcnt + 1
                End If
                GridName.Col = GridName.Col + 1
            Loop
            '        GridName.Col = GridName.MaxCols
            '        mEmpEarnData(Colcnt).mRate = 0
            '        mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
            '        mEmpEarnData(Colcnt).mTitle = "NET SALARY"
            '        mEmpEarnData(Colcnt).mHeadingDesc = " "

            MaxColcnt = Colcnt - 1

            Colcnt = 1

            If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                I = 0
            Else
                I = 1
            End If
            For Colcnt = I To MaxColcnt
                SqlStr = " INSERT INTO TEMP_SALREG_TRN ( " & vbCrLf _
                    & " USERID, COMPANY_CODE, SUBROW , " & vbCrLf _
                    & " EMP_CODE, EMP_DESC, " & vbCrLf _
                    & " PAYABLE_DAYS, BASIC_SALARY, PAYABLE_BASIC_SALARY,"

                SqlStr = SqlStr & vbCrLf _
                    & " ROW_SEQ, ROW_EARN_DEDUCT, ROW_TITLE,ROW_RATE,ROW_PAYABLE " & vbCrLf _
                    & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RowNum & ", " & vbCrLf _
                    & " '" & mEmpCode & "','" & mEmpDesc & "', " & vbCrLf _
                    & " " & Val(CStr(mWDays)) & ", " & mBSalary & ", " & mPSalary & ", "


                SqlStr = SqlStr & vbCrLf _
                    & " " & Colcnt & ", '" & mEmpEarnData(Colcnt).mHeadingDesc & "', '" & mEmpEarnData(Colcnt).mTitle & "'," & mEmpEarnData(Colcnt).mRate & "," & mEmpEarnData(Colcnt).mPayable & "" & vbCrLf _
                    & " )"


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
    Private Function GetBalLeave(ByRef mCode As String) As String

        On Error GoTo ErrBalLeave
        Dim SqlStr As String = ""
        Dim RsOpLeave As ADODB.Recordset
        Dim RsLeave As ADODB.Recordset
        Dim mOpCasual As Double
        Dim mOpEarn As Double
        Dim mOpSick As Double

        Dim mEarn As Double
        Dim mCasual As Double
        Dim mSick As Double

        Dim mMonEarn As Double
        Dim mMonCasual As Double
        Dim mMonSick As Double

        Dim pBalEL As Double
        Dim pBalCL As Double
        Dim pBalSL As Double
        Dim pBalCPL As Double

        Dim mCPLEarn As Double
        Dim mCPLAvail As Double
        Dim pRunDate As String
        Dim mTotalLeavesBal As Double

        pRunDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mTotalLeavesBal = CalcBalLeaves(mCode, pRunDate, PubDBCn, pBalEL, pBalCL, pBalSL, pBalCPL)


        SqlStr = " SELECT LEAVECODE, SUM(OPENING + TOTENTITLE) As OPENING " & vbCrLf & " FROM PAY_OPLEAVE_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(lblRunDate.Text)) & " " & vbCrLf & " AND EMP_CODE ='" & mCode & "'" & vbCrLf & " GROUP BY LEAVECODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOpLeave.EOF = False Then
            Do While Not RsOpLeave.EOF
                If RsOpLeave.Fields("LeaveCode").Value = CASUAL Then
                    mOpCasual = IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                ElseIf RsOpLeave.Fields("LeaveCode").Value = EARN Then
                    mOpEarn = IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                ElseIf RsOpLeave.Fields("LeaveCode").Value = SICK Then
                    mOpSick = IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                End If

                RsOpLeave.MoveNext()
            Loop
        End If

        SqlStr = " SELECT FIRSTHALF, SECONDHALF,ATTN_DATE " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(lblRunDate.Text)) & " " & vbCrLf & " AND EMP_CODE ='" & mCode & "' AND TO_CHAR(ATTN_DATE,'YYYYMM')<='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While Not RsLeave.EOF
                If RsLeave.Fields("FIRSTHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Then
                    mSick = mSick + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Then
                    mCPLEarn = mCPLEarn + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Then
                    mCPLAvail = mCPLAvail + 0.5
                End If

                If RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mCPLEarn = mCPLEarn + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mCPLAvail = mCPLAvail + 0.5
                End If

                If VB6.Format(RsLeave.Fields("ATTN_DATE").Value, "MM-YYYY") = VB6.Format(lblRunDate.Text, "MM-YYYY") Then
                    If RsLeave.Fields("FIRSTHALF").Value = CASUAL Then
                        mMonCasual = mMonCasual + 0.5
                    ElseIf RsLeave.Fields("FIRSTHALF").Value = EARN Then
                        mMonEarn = mMonEarn + 0.5
                    ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Then
                        mMonSick = mMonSick + 0.5
                    End If

                    If RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                        mMonCasual = mMonCasual + 0.5
                    ElseIf RsLeave.Fields("SECONDHALF").Value = EARN Then
                        mMonEarn = mMonEarn + 0.5
                    ElseIf RsLeave.Fields("SECONDHALF").Value = SICK Then
                        mMonSick = mMonSick + 0.5
                    End If
                End If
                RsLeave.MoveNext()
            Loop
        End If
        '    mBalCL = mOpCasual - mCasual
        '    mBalEL = mOpEarn - mEarn
        '    mBalSL = mOpSick - mSick

        '    If RsCompany.Fields("COMPANY_CODE").Value = 15 Then
        '        GetBalLeave = "EL: " & mMonEarn & "/" & mEarn & "  CL: " & mMonCasual & "/" & mCasual & "  SL: " & mMonSick & "/" & mSick & "  CPL BALANCE : " & mCPLEarn - mCPLAvail
        '    Else
        'If RsCompany.Fields("COMPANY_CODE").Value = 16 Or RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 35 Then
        GetBalLeave = "EL: " & mMonEarn & "/" & mEarn & "/" & pBalEL & "   CL: " & mMonCasual & "/" & mCasual & "/" & pBalCL & "   SL: " & mMonSick & "/" & mSick & "/" & pBalSL & ""
        GetBalLeave = "Leaves Availed (This Month/YTD/Balance) : " & GetBalLeave
        'Else
        '    GetBalLeave = "EL: " & mMonEarn & "/" & mEarn & "     CL: " & mMonCasual & "/" & mCasual & "     SL: " & mMonSick & "/" & mSick
        '    GetBalLeave = "Leaves Availed (This Month/YTD) : " & GetBalLeave
        'End If
        '    End If


        '

        '', pBalCL, pBalSL
        Exit Function
ErrBalLeave:
        '    Resume
        MsgBox(Err.Description)
        GetBalLeave = ""
    End Function

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
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
        cmdAccountPost.Enabled = mPrintEnable
        cmdPaySlipeMail.Enabled = mPrintEnable
        cmdExport.Enabled = mPrintEnable
    End Sub

    Private Function GetHolidays(ByRef pEmpCode As String, ByRef mLeaveDate As String) As Double

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFirstDateOfMonth As String
        Dim LastDateofMon As String

        GetHolidays = 0
        mFirstDateOfMonth = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

        If Trim(mLeaveDate) = "" Then
            LastDateofMon = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        Else
            LastDateofMon = mLeaveDate
        End If

        SqlStr = " SELECT ATTN_DATE, SECONDHALF, FIRSTHALF " & vbCrLf & " FROM  PAY_ATTN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mFirstDateOfMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(LastDateofMon, "DD-MMM-YYYY") & "','DD-MON-YYYY') ORDER BY ATTN_DATE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                If RsTemp.Fields("FIRSTHALF").Value = SUNDAY Or RsTemp.Fields("FIRSTHALF").Value = HOLIDAY Then
                    GetHolidays = GetHolidays + 0.5
                End If

                If RsTemp.Fields("SECONDHALF").Value = SUNDAY Or RsTemp.Fields("SECONDHALF").Value = HOLIDAY Then
                    GetHolidays = GetHolidays + 0.5
                End If
                RsTemp.MoveNext()
            Loop
        End If

    End Function

    Private Sub cboShowSalary_TextChanged(sender As Object, e As EventArgs) Handles cboShowSalary.TextChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cboShowSalary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboShowSalary.SelectedIndexChanged
        Call PrintCommand(False)
    End Sub

    Private Sub cmdReprocessDays_Click(sender As Object, e As EventArgs) Handles cmdReprocessDays.Click
        On Error GoTo ErrPart
        Dim CntRow As Long
        Dim mAmount As Double
        Dim mGrossSalary As Double
        Dim mMonthDays As Long
        Dim mDeductedDays As Double
        Dim mActualDeductedDays As Double
        Dim mEmpCode As String
        Dim mSalaryMonth As String
        Dim mSalaryStartMonth As String

        Dim mSqlstr As String
        Dim RsTemp As ADODB.Recordset
        Dim mAttnFlag As Long
        Dim mBalanceAttn As Double
        Dim mAttnDate As String
        Dim mEmpDOJ As String
        Dim mDOL As String
        Dim mTotalWop_Absent, mWOP, mAbsent As Double
        Dim pSalaryType As String
        Dim mWDays As Double
        Dim mAttnAwardAmount As String
        Dim mDiffAfterAttnAward As Double
        Dim mNewAttanAwardAmount As Double
        Dim mCount As Long
        Dim mIncentiveActual As Double
        Dim mExcessAmount As Double
        Dim mSalaryAdjwithInc As Boolean

        Dim mSalaryType As String

        If cboShowSalary.SelectedIndex = 1 Then
            MsgInformation("Process will be run only under Form 1 Salary, Please select the Form 1 Salary.")
            cmdReprocessDays.Enabled = False
            Exit Sub
        End If

        mMonthDays = MainClass.LastDay(Month(lblYear.Text), Year(lblYear.Text))
        mSalaryMonth = VB6.Format(mMonthDays & "/" & VB6.Format(lblYear.Text, "MM/YYYY"), "DD/MM/YYYY")

        mSalaryStartMonth = VB6.Format("01/" & VB6.Format(lblYear.Text, "MM/YYYY"), "DD/MM/YYYY")

        mSqlstr = " SELECT COUNT(1) AS CNTROW FROM PAY_ATTN_MST  " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mSalaryStartMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mSalaryMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY') AND (EXTRA_LEAVE='Y' OR EXTRA_LEAVE_2='Y')"


        MainClass.UOpenRecordSet(mSqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        mCount = 0

        If RsTemp.EOF = False Then
            mCount = IIf(IsDBNull(RsTemp.Fields("CNTROW").Value), 0, RsTemp.Fields("CNTROW").Value)
        End If

        If mCount > 0 Then
            MsgInformation("Process Already Done.")
            cmdReprocessDays.Enabled = False
            Exit Sub
        End If


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        With sprdAttn
            For CntRow = 1 To .MaxRows - 2
                .Row = CntRow
                .Col = ColCard
                mEmpCode = Trim(.Text)

                mSalaryType = "G"
                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_RATE_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSalaryType = MasterNo
                Else
                    mSalaryType = "G"
                End If

                .Col = ColDOJ
                mEmpDOJ = Trim(.Text)


                mSqlstr = " DELETE FROM PAY_OVERTIME_ADJ_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                        & " AND OT_DATE>=TO_DATE('" & VB6.Format(mSalaryStartMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                        & " AND OT_DATE<=TO_DATE('" & VB6.Format(mSalaryMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(mSqlstr)


                mSqlstr = " UPDATE PAY_ATTN_MST SET EXTRA_LEAVE='N', EXTRA_LEAVE_2='N' " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                        & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mSalaryStartMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                        & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mSalaryMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(mSqlstr)

                .Row = CntRow
                .Col = .MaxCols - 1
                mIncentiveActual = Val(.Text)

                .Col = .MaxCols
                mAmount = Val(.Text)
                mExcessAmount = mAmount

                mGrossSalary = 0
                mDeductedDays = 0

                mSalaryAdjwithInc = False

                If mAmount < 0 Then
                    mAttnAwardAmount = 0 '' GetAttnAwardAmount(ConAttendanceAllw, mEmpCode, mSalaryMonth)
                    'mDiffAfterAttnAward = mAttnAwardAmount - mAmount
                    'mAmount = mAmount + mAttnAwardAmount
                    'If mAmount + mAttnAwardAmount < 0 Then
                    mGrossSalary = GetGrossSalary(mEmpCode, mSalaryMonth)
                    mGrossSalary = VB6.Format(mGrossSalary / mMonthDays, "0.0")

                    'If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '    mDOL = MasterNo
                    'Else
                    '    mDOL = ""
                    'End If

                    'mWDays = CalcAttn(mEmpCode, mEmpDOJ, mDOL, mSalaryMonth, mTotalWop_Absent, , , mWOP, mAbsent, pSalaryType)

                    mActualDeductedDays = 0
                    mNewAttanAwardAmount = 0
                    mActualDeductedDays = VB6.Format((mAmount + mAttnAwardAmount) * -1 / mGrossSalary, "0.00")
                    mDeductedDays = Int(mActualDeductedDays) + IIf(mActualDeductedDays - Int(mActualDeductedDays) > 0.5, 1, 0.5)

                    mExcessAmount = mDeductedDays * mGrossSalary
                    If mIncentiveActual = 0 Then
                        mIncentiveActual = -1 * IIf(mExcessAmount > Math.Abs(mAmount), mExcessAmount - Math.Abs(mAmount), 0)
                        mSalaryAdjwithInc = IIf(mExcessAmount > Math.Abs(mAmount), True, False)
                    ElseIf mActualDeductedDays <> mDeductedDays Then
                        mIncentiveActual = mIncentiveActual - (mDeductedDays - mActualDeductedDays) * mGrossSalary
                        mSalaryAdjwithInc = IIf(mSalaryType = "P", False, True)
                    Else
                        mIncentiveActual = mIncentiveActual - (mExcessAmount + mAmount)
                    End If


                    'mDeductedDays = mDeductedDays - (mMonthDays - mWDays)
                    'Else
                    '    mNewAttanAwardAmount = mAttnAwardAmount + mAmount
                    'End If

                    'Not Required
                    'If UpdateAttnAward(ConAttendanceAllw, mEmpCode, mSalaryMonth, mAmount * -1, "I") = False Then GoTo ErrPart
                Else
                    mAttnAwardAmount = GetAttnAwardAmount(ConAttendanceAllw, mEmpCode, mSalaryMonth)
                    If mIncentiveActual = 0 Then
                        mIncentiveActual = 0
                    Else
                        mIncentiveActual = mIncentiveActual - mExcessAmount
                    End If

                    If mAmount < mAttnAwardAmount Then
                        mNewAttanAwardAmount = mAttnAwardAmount - mAmount
                        If UpdateAttnAward(ConAttendanceAllw, mEmpCode, mSalaryMonth, mNewAttanAwardAmount, "S") = False Then GoTo ErrPart
                    End If
                End If

                ''mExcessAmount = mIncentiveActual    ''IIf(mIncentiveActual > (mExcessAmount * -1), mIncentiveActual - (mExcessAmount * -1), mIncentiveActual)

                If mIncentiveActual > 0 Or mSalaryAdjwithInc = True Then
                    mSqlstr = " INSERT INTO PAY_OVERTIME_ADJ_MST (COMPANY_CODE,PAYYEAR,EMP_CODE,OT_DATE,OT_AMOUNT, ADDUSER,ADDDATE) VALUES (  " & vbCrLf _
                                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf _
                                & " '" & mEmpCode & "', TO_DATE('" & VB6.Format(mSalaryMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                                & " " & mIncentiveActual & ", '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    PubDBCn.Execute(mSqlstr)
                End If
                If mDeductedDays > 0 Then


                    mSqlstr = "SELECT * From PAY_ATTN_MST" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                            & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mSalaryStartMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mSalaryMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " AND (FIRSTHALF=" & PRESENT & " OR SECONDHALF=" & PRESENT & ")" & vbCrLf _
                            & " ORDER BY ATTN_DATE DESC"


                    MainClass.UOpenRecordSet(mSqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)



                    If RsTemp.EOF = False Then
                        mBalanceAttn = mDeductedDays
                        Do While RsTemp.EOF = False
                            mAttnDate = IIf(IsDBNull(RsTemp.Fields("ATTN_DATE").Value), "", RsTemp.Fields("ATTN_DATE").Value)

                            If mBalanceAttn = 0 Then Exit Do
                            mAttnFlag = IIf(IsDBNull(RsTemp.Fields("FIRSTHALF").Value), 0, RsTemp.Fields("FIRSTHALF").Value)

                            If mAttnFlag = PRESENT Then

                                mSqlstr = "UPDATE PAY_ATTN_MST SET EXTRA_LEAVE='Y' " & vbCrLf _
                                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                        & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                                        & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

                                PubDBCn.Execute(mSqlstr)
                                mBalanceAttn = mBalanceAttn - 0.5
                            End If

                            'mBalanceAttn = mBalanceAttn - 0.5

                            If mBalanceAttn = 0 Then Exit Do
                            mAttnFlag = IIf(IsDBNull(RsTemp.Fields("SECONDHALF").Value), 0, RsTemp.Fields("SECONDHALF").Value)

                            If mAttnFlag = PRESENT Then

                                mSqlstr = "UPDATE PAY_ATTN_MST SET EXTRA_LEAVE_2='Y' " & vbCrLf _
                                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                        & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf _
                                        & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mAttnDate, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

                                PubDBCn.Execute(mSqlstr)
                                mBalanceAttn = mBalanceAttn - 0.5
                            End If

                            'mBalanceAttn = mBalanceAttn - 0.5

                            RsTemp.MoveNext()
                        Loop
                    End If

                End If
            Next
        End With

        'Call SalaryProcess

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation("Leave Updated")

        Exit Sub
ErrPart:

        PubDBCn.RollbackTrans()
    End Sub

    Private Function UpdateAttnAward(ByRef ConAttendanceAllw As Integer, ByRef mEmpCode As String, ByRef mSalaryMonth As String,
                                     ByRef mNewAttanAwardAmount As Double, pType As String) As Boolean

        On Error GoTo UpdateAttnAwardErr
        Dim SqlStr As String = ""
        Dim cntCol As Integer
        Dim xMonth As String
        Dim xTypeCode As Integer
        Dim xLoanType As Integer
        Dim mMonthDate As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If pType = "S" Then
            SqlStr = " SELECT CODE,TYPE FROM PAY_SALARYHEAD_MST" & vbCrLf _
                    & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TYPE = " & Val(ConAttendanceAllw) & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                xTypeCode = RsTemp.Fields("Code").Value
                xLoanType = RsTemp.Fields("Type").Value
            Else
                MsgInformation("Attd Ward not found.")
                UpdateAttnAward = False
                Exit Function
            End If
        Else
            xTypeCode = -1
        End If

        SqlStr = "DELETE FROM PAY_MONTHLY_AA_TRN" & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND PAYYEAR = " & Year(CDate(lblRunDate.Text)) & " " & vbCrLf _
                & " AND EMP_CODE= '" & mEmpCode & "' " & vbCrLf _
                & " AND TO_CHAR(SAL_MONTH,'MMYYYY')=TO_CHAR('" & VB6.Format(mSalaryMonth, "MMYYYY") & "') " & vbCrLf _
                & " AND ADD_DEDUCTCODE=" & xTypeCode & " And SAL_FLAG='" & pType & "' "

        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO PAY_MONTHLY_AA_TRN ( " & vbCrLf _
                & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                & " EMP_CODE, BASICSALARY, " & vbCrLf _
                & " SAL_MONTH, ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, ADDDAYS,SAL_FLAG) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(lblRunDate.Text)) & ", " & vbCrLf _
                & " '" & mEmpCode & "', 0, " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mSalaryMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & xTypeCode & ", 0, " & vbCrLf _
                & " " & mNewAttanAwardAmount & ",0,'" & pType & "') "

        PubDBCn.Execute(SqlStr)


        'SqlStr = " UPDATE PAY_MONTHLY_TRN SET AMOUNT=" & mNewAttanAwardAmount & " " & vbCrLf _
        '        & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '        & " AND PAYYEAR = " & Year(CDate(lblRunDate.Text)) & " " & vbCrLf _
        '        & " AND EMP_CODE= '" & mEmpCode & "' " & vbCrLf _
        '        & " AND TO_CHAR(SAL_MONTH,'MMYYYY')=TO_CHAR('" & VB6.Format(mSalaryMonth, "MMYYYY") & "') " & vbCrLf _
        '        & " AND ADD_DEDUCTCODE=" & xTypeCode & " And SAL_FLAG='S' "

        'PubDBCn.Execute(SqlStr)

        'SqlStr = " UPDATE PAY_DUMMYSAL_TRN SET AMOUNT = " & mNewAttanAwardAmount & " " & vbCrLf _
        '        & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '        & " AND PAYYEAR = " & Year(CDate(lblRunDate.Text)) & " " & vbCrLf _
        '        & " AND EMP_CODE = '" & mEmpCode & "' " & vbCrLf _
        '        & " AND TO_CHAR(SAL_DATE,'MMYYYY')=TO_CHAR('" & VB6.Format(mSalaryMonth, "MMYYYY") & "') " & vbCrLf _
        '        & " AND SALHEADCODE=" & xTypeCode & " And ISARREAR='Y' "


        'PubDBCn.Execute(SqlStr)

        'SqlStr = " UPDATE PAY_DUMMYACTUAL_SAL_TRN SET AMOUNT = " & mNewAttanAwardAmount & " " & vbCrLf _
        '        & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '        & " AND PAYYEAR = " & Year(CDate(lblRunDate.Text)) & " " & vbCrLf _
        '        & " AND EMP_CODE = '" & mEmpCode & "' " & vbCrLf _
        '        & " AND TO_CHAR(SAL_DATE,'MMYYYY')=TO_CHAR('" & VB6.Format(mSalaryMonth, "MMYYYY") & "') " & vbCrLf _
        '        & " AND SALHEADCODE=" & xTypeCode & " And ISARREAR='Y' "


        'PubDBCn.Execute(SqlStr)

        UpdateAttnAward = True
        Exit Function
UpdateAttnAwardErr:
        ''Resume
        MsgBox(Err.Description)
        UpdateAttnAward = False
    End Function
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        txtEmpCode.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearch.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        'Try
        '    Dim oxl As Excel.Application
        '    Dim owbs As Excel.Workbooks
        '    Dim owb As Excel.Workbook
        '    Dim osheets As Excel.Worksheets
        '    Dim osheet As Excel.Worksheet
        '    Dim CntRow As Long
        '    Dim cntCol As Long
        '    Dim mExcelCol As Long
        '    Dim mEPFSalary As Double
        '    Dim mFilename As String = ""
        '    Dim mAttachmentFile As String
        '    Dim mDate As String
        '    Dim mHeadingline As Long
        '    Dim pCardNo As String
        '    Dim mColHeadingName As String
        '    Dim mColSalHeadName As String
        '    Dim mPFAmount As Double
        '    Dim mLastEarnCol As Long
        '    Dim mTotalPayableCol As Long
        '    Dim mTotalDeductCol As Long
        '    Dim mNetTotalCol As Long
        '    Dim mRateGrossSalary As Double
        '    Dim mAddDeduct As String
        '    Dim mText As String = ""

        '    For cntCol = 1 To sprdAttn.MaxCols
        '        sprdAttn.Row = 0
        '        sprdAttn.Col = cntCol
        '        mText = Trim(sprdAttn.Text)

        '        If mText = "Total Payable" Then
        '            mTotalPayableCol = cntCol
        '        End If

        '        If mText = "Total Deduction" Then
        '            mTotalDeductCol = cntCol
        '        End If

        '        If mText = "Net Salary" Then
        '            mNetTotalCol = cntCol
        '        End If
        '    Next

        '    mFilename = PubReportFolderPath & "SalaryRegister.xlsx"  ''(.xlsx) ''mPubBarCodePath &
        '    mAttachmentFile = mPubBarCodePath & "\" & "SalaryRegister" & VB6.Format(Now(), "DDMMYYYYhhmm") & ".xlsx"

        '    oxl = CreateObject("Excel.Application")
        '    'oxl.DisplayAlerts = True
        '    oxl.Visible = True
        '    owb = oxl.Workbooks.Open(mFilename)


        '    With oxl.ActiveSheet
        '        mHeadingline = 1
        '        .Cells(mHeadingline, 1).Value = RsCompany.Fields("COMPANY_NAME").Value

        '        mHeadingline = 2
        '        .Cells(mHeadingline, 1).Value = "Salary Register for the month " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))

        '        mHeadingline = 3
        '        sprdAttn.Row = 0
        '        .Cells(mHeadingline, 1).Value = "S.No"

        '        sprdAttn.Col = ColCard
        '        .Cells(mHeadingline, 2).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColName
        '        .Cells(mHeadingline, 3).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColDept
        '        .Cells(mHeadingline, 4).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColDesg
        '        .Cells(mHeadingline, 5).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColDOJ
        '        .Cells(mHeadingline, 6).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColBankNo
        '        .Cells(mHeadingline, 7).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColBankName
        '        .Cells(mHeadingline, 8).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColBankIFSC
        '        .Cells(mHeadingline, 9).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColDays
        '        .Cells(mHeadingline, 10).Value = Trim(sprdAttn.Text)

        '        sprdAttn.Col = ColBSalary
        '        .Cells(mHeadingline, 11).Value = "Rate - " & Trim(sprdAttn.Text)

        '        mExcelCol = 12
        '        For cntCol = ColPSalary + 1 To sprdAttn.MaxCols - 2 Step 2
        '            sprdAttn.Col = cntCol
        '            mColHeadingName = Trim(sprdAttn.Text)
        '            mColSalHeadName = Mid(mColHeadingName, Len("Rate -"), Len(mColHeadingName))

        '            If MainClass.ValidateWithMasterTable(UCase(Trim(mColSalHeadName)), "NAME", "ADDDEDUCT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                mAddDeduct = Val(MasterNo)
        '            Else
        '                mAddDeduct = 0
        '            End If
        '            If mAddDeduct = 1 Then
        '                .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '                mExcelCol = mExcelCol + 1
        '            End If
        '        Next

        '        .Cells(mHeadingline, mExcelCol).Value = "Rate - Gross Salary"
        '        mExcelCol = mExcelCol + 1

        '        sprdAttn.Col = ColPSalary
        '        .Cells(mHeadingline, mExcelCol).Value = "Basic Salary" '' Trim(sprdAttn.Text)
        '        mExcelCol = mExcelCol + 1

        '        For cntCol = ColPSalary + 2 To mTotalPayableCol - 1       ''sprdAttn.MaxCols - 2 Step 2
        '            sprdAttn.Col = cntCol
        '            mColSalHeadName = Trim(sprdAttn.Text)

        '            If MainClass.ValidateWithMasterTable(UCase(Trim(mColSalHeadName)), "NAME", "ADDDEDUCT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                mAddDeduct = Val(MasterNo)
        '            Else
        '                mAddDeduct = 0
        '            End If
        '            If mAddDeduct = 1 Then
        '                .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '                mExcelCol = mExcelCol + 1
        '            End If
        '            mLastEarnCol = cntCol
        '        Next

        '        For cntCol = mLastEarnCol To mTotalPayableCol
        '            sprdAttn.Col = cntCol
        '            mColSalHeadName = Trim(sprdAttn.Text)

        '            .Cells(mHeadingline, mExcelCol).Value = IIf(cntCol = mTotalPayableCol, "Gross Salary", Trim(sprdAttn.Text))
        '            mExcelCol = mExcelCol + 1
        '        Next

        '        .Cells(mHeadingline, mExcelCol).Value = "EPF Salary"
        '        mExcelCol = mExcelCol + 1

        '        For cntCol = mTotalPayableCol + 1 To mTotalDeductCol - 1
        '            sprdAttn.Col = cntCol
        '            mColSalHeadName = Trim(sprdAttn.Text)

        '            If MainClass.ValidateWithMasterTable(UCase(Trim(mColSalHeadName)), "NAME", "ADDDEDUCT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                mAddDeduct = Val(MasterNo)
        '            Else
        '                mAddDeduct = 0
        '            End If
        '            If mAddDeduct = 2 Then
        '                .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '                mExcelCol = mExcelCol + 1
        '            End If
        '            mLastEarnCol = cntCol
        '        Next

        '        sprdAttn.Col = mTotalDeductCol
        '        .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '        mExcelCol = mExcelCol + 1

        '        sprdAttn.Col = mNetTotalCol
        '        .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '        mExcelCol = mExcelCol + 1


        '        mHeadingline = mHeadingline + 1
        '    End With

        '    'mHeadingline = mHeadingline + 1

        '    For CntRow = 1 To sprdAttn.MaxRows
        '        With oxl.ActiveSheet
        '            sprdAttn.Row = CntRow

        '            sprdAttn.Col = ColCard
        '            pCardNo = Trim(sprdAttn.Text)

        '            .Cells(mHeadingline, 1).Value = IIf(pCardNo = "", "", CntRow)

        '            sprdAttn.Col = ColCard
        '            .Cells(mHeadingline, 2).Value = "'" & Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColName
        '            .Cells(mHeadingline, 3).Value = Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColDept
        '            .Cells(mHeadingline, 4).Value = Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColDesg
        '            .Cells(mHeadingline, 5).Value = Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColDOJ
        '            .Cells(mHeadingline, 6).Value = Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColBankNo
        '            .Cells(mHeadingline, 7).Value = "'" & Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColBankName
        '            .Cells(mHeadingline, 8).Value = Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColBankIFSC
        '            .Cells(mHeadingline, 9).Value = Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColDays
        '            .Cells(mHeadingline, 10).Value = Trim(sprdAttn.Text)

        '            sprdAttn.Col = ColBSalary
        '            .Cells(mHeadingline, 11).Value = Trim(sprdAttn.Text)
        '            mRateGrossSalary = Val(sprdAttn.Text)

        '            mExcelCol = 12
        '            For cntCol = ColPSalary + 1 To sprdAttn.MaxCols - 2 Step 2
        '                sprdAttn.Row = 0
        '                sprdAttn.Col = cntCol
        '                mColHeadingName = Trim(sprdAttn.Text)
        '                mColSalHeadName = Mid(mColHeadingName, Len("Rate -"), Len(mColHeadingName))

        '                If MainClass.ValidateWithMasterTable(UCase(Trim(mColSalHeadName)), "NAME", "ADDDEDUCT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAddDeduct = Val(MasterNo)
        '                Else
        '                    mAddDeduct = 0
        '                End If
        '                If mAddDeduct = 1 Then
        '                    sprdAttn.Row = CntRow
        '                    .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '                    mRateGrossSalary = mRateGrossSalary + Val(sprdAttn.Text)
        '                    mExcelCol = mExcelCol + 1
        '                End If
        '            Next

        '            .Cells(mHeadingline, mExcelCol).Value = mRateGrossSalary
        '            mExcelCol = mExcelCol + 1

        '            sprdAttn.Row = CntRow
        '            sprdAttn.Col = ColPSalary
        '            .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '            mExcelCol = mExcelCol + 1

        '            For cntCol = ColPSalary + 2 To mTotalPayableCol - 1       ''sprdAttn.MaxCols - 2 Step 2
        '                sprdAttn.Row = 0
        '                sprdAttn.Col = cntCol
        '                mColSalHeadName = Trim(sprdAttn.Text)

        '                If MainClass.ValidateWithMasterTable(UCase(Trim(mColSalHeadName)), "NAME", "ADDDEDUCT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAddDeduct = Val(MasterNo)
        '                Else
        '                    mAddDeduct = 0
        '                End If
        '                If mAddDeduct = 1 Then
        '                    sprdAttn.Row = CntRow
        '                    .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '                    mExcelCol = mExcelCol + 1
        '                End If
        '                mLastEarnCol = cntCol
        '            Next

        '            For cntCol = mLastEarnCol To mTotalPayableCol
        '                sprdAttn.Row = CntRow
        '                sprdAttn.Col = cntCol
        '                mColSalHeadName = Trim(sprdAttn.Text)

        '                .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '                mExcelCol = mExcelCol + 1
        '            Next

        '            mPFAmount = 0
        '            For cntCol = mTotalPayableCol + 1 To mTotalDeductCol - 1
        '                sprdAttn.Row = 0
        '                sprdAttn.Col = cntCol
        '                mColSalHeadName = Trim(sprdAttn.Text)

        '                If MainClass.ValidateWithMasterTable(UCase(Trim(mColSalHeadName)), "NAME", "ADDDEDUCT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE=" & ConPF & "") = True Then
        '                    sprdAttn.Row = CntRow
        '                    mPFAmount = Val(sprdAttn.Text)
        '                End If
        '            Next

        '            mEPFSalary = VB6.Format(mPFAmount * 100 / 12, "0")
        '            .Cells(mHeadingline, mExcelCol).Value = mEPFSalary
        '            mExcelCol = mExcelCol + 1

        '            For cntCol = mTotalPayableCol + 1 To mTotalDeductCol - 1
        '                sprdAttn.Row = 0
        '                sprdAttn.Col = cntCol
        '                mColSalHeadName = Trim(sprdAttn.Text)

        '                If MainClass.ValidateWithMasterTable(UCase(Trim(mColSalHeadName)), "NAME", "ADDDEDUCT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAddDeduct = Val(MasterNo)
        '                Else
        '                    mAddDeduct = 0
        '                End If
        '                If mAddDeduct = 2 Then
        '                    sprdAttn.Row = CntRow
        '                    .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '                    mExcelCol = mExcelCol + 1
        '                End If
        '                mLastEarnCol = cntCol
        '            Next

        '            sprdAttn.Row = CntRow
        '            sprdAttn.Col = mTotalDeductCol
        '            .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '            mExcelCol = mExcelCol + 1

        '            sprdAttn.Col = mNetTotalCol
        '            .Cells(mHeadingline, mExcelCol).Value = Trim(sprdAttn.Text)
        '            mExcelCol = mExcelCol + 1

        '            mHeadingline = mHeadingline + 1
        '        End With
        '    Next

        '    With oxl
        '        .ScreenUpdating = False
        '        .DisplayAlerts = False
        '    End With

        '    Dim mColHeader As String
        '    Dim mEndCol As String

        '    mEndCol = ConvertToLetter(mExcelCol - 1)
        '    mColHeader = "A3" & ":" & mEndCol & mHeadingline
        '    oxl.ActiveSheet.Cells.Range("" & mColHeader & "").Borders(1).LineStyle = 1
        '    oxl.ActiveSheet.Cells.Range("" & mColHeader & "").Borders(3).LineStyle = 1
        '    oxl.ActiveSheet.Cells.Range("" & mColHeader & "").BorderAround(LineStyle:=1, Weight:=3, ColorIndex:=1)

        '    oxl.ActiveWorkbook.SaveAs(mAttachmentFile)
        '    oxl.Quit()
        '    MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name Is " & mAttachmentFile)
        '    'Dim mFilename As String = ""

        '    'mFilename = mPubBarCodePath & "\" & "SalaryRegister.xls"  ''(.xlsx)

        '    'If sprdAttn.ExportToExcel(mFilename, "SalaryRegister", "") = True Then
        '    '    '                If sprdAttn.ExportExcelBook(mFilename, "") = True Then
        '    '    MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name Is " & mFilename)
        '    'End If
        'Catch ex As Exception

        'End Try
    End Sub
End Class
