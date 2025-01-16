Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalary_ArrearReg
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
    Private Const ColName As Short = 2
    Private Const ColFName As Short = 3
    Private Const ColPaymentType As Short = 4
    Private Const ColDept As Short = 5
    Private Const ColDesg As Short = 6
    Private Const ColDOJ As Short = 7
    Private Const ColBankNo As Short = 8
    Private Const ColSalType As Short = 9
    Private Const ColDays As Short = 10
    Private Const ColBSalary As Short = 11
    Private Const ColPSalary As Short = 12
    Private Const ColBankIFSC As Short = 13
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

            .Col = ColSalType
            .Text = "Salary Type"

            .Col = ColDays
            .Text = "Working Days"
            .ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)

            .Col = ColBSalary
            .Text = "Basic Salary"
            .ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)

            .Col = ColPSalary
            .Text = "Payable Salary"
            .ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)


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
                .MaxCols = .MaxCols + (IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, 2, 1) * mRecordCount) + IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, 1, 0)
                cntCol = 1
                Do While Not RsTemp.EOF
                    If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        .Col = ColPSalary + cntCol
                        .Text = "RATE-" & RsTemp.Fields("Name").Value
                        .ColHidden = True
                        cntCol = cntCol + 1
                    End If

                    .Col = ColPSalary + cntCol
                    .Text = RsTemp.Fields("Name").Value
                    mAddDeduct = RsTemp.Fields("ADDDEDUCT").Value

                    RsTemp.MoveNext()
                    cntCol = cntCol + 1
                    If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If Not RsTemp.EOF Then
                            If RsTemp.Fields("ADDDEDUCT").Value <> mAddDeduct Then
                                .Col = ColPSalary + cntCol
                                .Text = "Total Payable"

                                cntCol = cntCol + 1
                            End If
                        End If
                    End If
                Loop

                If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    .MaxCols = .MaxCols + 1
                    .Col = .MaxCols
                    .Text = "Total Deduction"
                    .ColHidden = IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)
                End If
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

        Exit Sub
        mDivisionCode = 1
        '    myMenu = "mnuJournal"
        mm.lblBookType.Text = ConJournal

        mm.txtVDate.Text = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(Month(CDate(lblRunDate.Text)), "00") & "/" & Year(CDate(lblRunDate.Text))
        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        mm.lblYM.Text = CStr(mYM)
        '    If lblIsArrear.Caption = "P" Then
        '        mBType = "P"
        '    ElseIf lblIsArrear.Caption = "V" Then
        '        mBType = "V"
        '    ElseIf lblIsArrear.Caption = "Y" Then
        '        mBType = "A"
        '    Else
        mBType = "S"
        '    End If

        '    If lblIsArrear.Caption = "P" Or lblIsArrear.Caption = "V" Then
        '        If Val(cboMonthTerm.Text) = 1 Then
        '            mBSType = "X"
        '        ElseIf Val(cboMonthTerm.Text) = 2 Then
        '            mBSType = "Y"
        '        Else
        '            mBSType = "Z"
        '        End If
        '    Else
        mBSType = VB.Left(cboCategory.Text, 1)
        '    End If

        mm.MdiParent = Me.MdiParent

        mm.lblSR.Text = mBType & mBSType & mDivisionCode

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
    Private Sub cmdPFESIPosting_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPFESIPosting.Click
        'Dim mVNo As String
        'Dim mVDate As String
        'Dim mBankCode As Long
        'Dim mYM As Long
        'Dim mBType As String
        'Dim mBSType As String
        '
        '   '' myMenu = "mnuBankPayment"
        '    myMenu = "mnuSalReg"
        '
        '    frmAtrn.txtVDate.Text = MainClass.LastDay(Month(lblRunDate), Year(lblRunDate)) & "/" & vb6.Format(Month(lblRunDate), "00") & "/" & Year(lblRunDate)
        '    mYM = Format(Year(lblRunDate), "0000") & vb6.Format(Month(lblRunDate), "00")
        '    frmAtrn.lblYM.Caption = mYM
        '    If lblIsArrear.Caption = "Y" Then
        '        mBType = "P"
        '        mBSType = "A"
        '        frmAtrn.lblSR.Caption = "PA"
        '    Else
        '        mBType = "P"
        '        mBSType = "R"
        '        frmAtrn.lblSR.Caption = "PR"
        '    End If
        '
        '    frmAtrn.lblBookType.Caption = ConJournal
        '    frmAtrn.Show
        '    If CheckSalVoucher(mYM, mVNo, mVDate, mBankCode, mBType, mBSType) = True Then
        '
        '        frmAtrn.Form_Activate
        '        frmAtrn.txtVDate = Format(mVDate, "dd/mm/yyyy")
        '        frmAtrn.txtVNo1 = Format(Month(mVDate), "00")
        '        frmAtrn.txtVNo = Mid(mVNo, 3)
        '        'If mainclass.ValidateWithMasterTable(mBankCode, "Code", "Name", "ACM", PubDBCn, MasterNo) = True Then
        '        '    frmAtrn.CboBookName = Trim(MasterNo)
        '        'End If
        '
        '        frmAtrn.txtVno_LostFocus
        '        frmAtrn.CmdAdd.Enabled = False
        '    End If
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

        frmPrintSalReg.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        '    If lblIsArrear.Caption = "Y" Then
        '        mSubTitle = "For the Month Paid: " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '    Else
        mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        '    End If

        '    If lblIsArrear.Caption = "P" Or lblIsArrear.Caption = "V" Then
        '        mSubTitle = mSubTitle & IIf(Val(cboMonthTerm.Text) = 1, " (1st Half)", " (2nd Half)")
        '    End If
        '
        mSubTitle = mSubTitle & IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Department : " & cboDept.Text, " ")

        If cboCostCenter.SelectedIndex <> 0 Then
            mSubTitle = mSubTitle & IIf(chkCostC.CheckState = System.Windows.Forms.CheckState.Unchecked, " AND Cost Center : " & cboCostCenter.Text, " ")
        End If

        If frmPrintSalReg.optPaySlip.Checked = True Then
            '        If frmPrintSalReg.OptAll(0) Then
            ColStartRow = 1
            ColEndRow = sprdAttn.MaxRows - 2
            '        Else
            '            For cntRow = 1 To sprdAttn.MaxRows
            '                sprdAttn.Row = cntRow
            '                sprdAttn.Col = ColSalType
            '                If Left(sprdAttn.Text, 1) = "S" Then
            '                    sprdAttn.Col = ColCard
            '                    If UCase(Trim(sprdAttn.Text)) = UCase(Trim(frmPrintSalReg.txtEmpCode)) Then
            '                        ColStartRow = cntRow
            '                        ColEndRow = cntRow
            '                        Exit For
            '                    End If
            '                End If
            '            Next
            '        End If
            If ColEndRow = 0 Then
                MsgBox("Such Employee Salary is not Updated...", MsgBoxStyle.Information)
                Exit Sub
            End If
            If FillPaySlipIntoPrintDummy(sprdAttn, ColStartRow, ColEndRow) = False Then GoTo ERR1



            SqlStr = ""
            SqlStr = FetchRecordForPaySlip(SqlStr)


            If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Checked Then
                mRptFileName = "PerksSlip_New.Rpt"
                mTitle = "" & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
            Else
                mRptFileName = IIf(RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12, "PaySlipKJ.Rpt", "PaySlip_New.Rpt")
                '            If lblIsArrear.Caption = "Y" Then
                '                mTitle = "ARREAR SLIP " & "For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
                '            Else
                mTitle = "PAY SLIP " & "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                '            End If
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
                mRptFileName = "SalReg_New.Rpt"
                '            If lblIsArrear.Caption = "Y" Then
                '                mTitle = "Arrear Register" & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
                '            Else
                mTitle = "Salary Register" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
                '            End If
            End If

        ElseIf frmPrintSalReg.optCashSheet.Checked = True Then
            mBankName = ""
            '        If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, "CASH", "") = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, ColCard, ColName, ColDays, ColPaymentType, (sprdAttn.MaxCols), ColBankNo, "CASH", mBankName, ColBankIFSC) = False Then GoTo ERR1
            SqlStr = ""
            SqlStr = FetchRecordForReport(SqlStr)
            mRptFileName = "SalCashSheet_New.Rpt"

            '        If lblIsArrear.Caption = "Y" Then
            '            mTitle = "Arrear Sheet (Cash)" & IIf(chkCategory.Value = vbUnchecked, " - " & cboCategory.Text, "")
            '        Else
            mTitle = "Salary Sheet (Cash)" & IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, " - " & cboCategory.Text, "")
            '        End If

        ElseIf frmPrintSalReg.OptSalSheet.Checked = True Then
            If frmPrintSalReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintSalReg.txtBankName.Text
            End If
            '        If FillDataIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, "CHEQUE", mBankName) = False Then GoTo ERR1
            If FillBankSheetIntoPrintDummy(sprdAttn, 1, sprdAttn.MaxRows, ColCard, ColName, ColDays, ColPaymentType, (sprdAttn.MaxCols), ColBankNo, "CHEQUE", mBankName, ColBankIFSC) = False Then GoTo ERR1

            SqlStr = ""
            SqlStr = FetchRecordForBankReport(SqlStr)
            mRptFileName = "BankSheet_New.Rpt"

            ''InputBox("Please Enter Bank Name. :", "Bank Name")

            If mBankName = "" Then
                mTitle = "BANK ANNEXURES"
            Else
                mTitle = "BANK ANNEXURES OF " & mBankName
            End If

            If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Checked Then
                mSubTitle = "Perks For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
            Else
                '            If lblIsArrear.Caption = "Y" Then
                '                mSubTitle = "Arrear For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
                '            Else
                mSubTitle = "Salary For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                '            End If
            End If

        ElseIf frmPrintSalReg.optBankTxt.Checked = True Then
            If frmPrintSalReg.optAllBank(0).Checked = True Then
                mBankName = ""
            Else
                mBankName = frmPrintSalReg.txtBankName.Text
            End If

            '        If lblIsArrear.Caption = "P" Then
            '            pNarr = "BY PERKS " & UCase(lblYear.Caption)
            '        Else
            pNarr = "BY SALARY OF " ''IIf(lblIsArrear.Caption = "N", "BY SALARY OF ", "BY ARREAR OF ") & UCase(lblYear.Caption)
            '        End If
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

                '            If lblIsArrear.Caption = "Y" Then
                '                mTitle = mTitle & "Arrear"
                '            End If
                '
                '            If lblIsArrear.Caption = "Y" Then
                '                mSubTitle = "For the Month Paid: " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
                '            Else
                mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
                '            End If
            Else
                MsgInformation("Please Select Deduction Head Name")
                frmPrintSalReg.Close()
                Exit Sub
            End If
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
        '    If chkPerksHead.Value = vbUnchecked Then
        RefreshScreen()
        '    Else
        '        RefreshScreenPerks
        '    End If
    End Sub
    Private Sub frmSalary_ArrearReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmSalary_ArrearReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        cboDept.Enabled = False
        cboCategory.Enabled = False
        cboCostCenter.Enabled = False
        '    OptCC.Value = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub frmSalary_ArrearReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
        Dim mSALType As String

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
            SqlStr = " SELECT SALTRN.*, EMP.EMP_NAME, EMP.EMP_DOJ, EMP.EMP_FNAME, " & vbCrLf & " ADD_DEDUCT.NAME AS ADDNAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.SALHEADCODE =ADD_DEDUCT.CODE "

            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"
        Else

            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & ""
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='" & lblIsArrear.Caption & "'"

        SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR IN ('N','Y')"

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

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND SALTRN.CATEGORY='" & VB.Left(cboCategory.Text, 1) & "' "
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

        SqlStr = SqlStr & vbCrLf & " SALTRN.ISARREAR, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ"

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

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_FNAME").Value), "", RsAttn.Fields("EMP_FNAME").Value)

                    .Col = ColPaymentType
                    .Text = IIf(RsAttn.Fields("PAYMENTMODE").Value = "1", "Cash", "Cheque")

                    mSALType = IIf(IsDbNull(RsAttn.Fields("IsArrear").Value), "S", RsAttn.Fields("IsArrear").Value)

                    .Col = ColSalType
                    .Text = IIf(mSALType = "N", "SALARY", "ARREAR")

                    mBankAcctNo = IIf(IsDbNull(RsAttn.Fields("BANKACCTNO").Value), "", RsAttn.Fields("BANKACCTNO").Value)


                    '                If mSALType = "Y" Then
                    '                    mArrearStr = GetEMPWEFDate(mCode, lblRunDate.Caption)
                    '                    mBankAcctNo = mBankAcctNo & String(15 - Len(mBankAcctNo), " ") & vbNewLine & mArrearStr
                    '                End If

                    .Col = ColBankNo
                    .Text = mBankAcctNo

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
                    mTotPayable = mPayableSalary * IIf(chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked, 1, 0)


                    Do While mCode = RsAttn.Fields("EMP_CODE").Value And mSALType = RsAttn.Fields("IsArrear").Value
                        For cntCol = ColBSalary To .MaxCols
                            .Row = 0
                            .Col = cntCol
                            If Trim(UCase(.Text)) = "RATE-" & Trim(UCase(RsAttn.Fields("ADDNAME").Value)) Then
                                .Row = cntRow
                                .Col = cntCol
                                .Text = MainClass.FormatRupees(RsAttn.Fields("ACTUALAMOUNT"))

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

                    If chkPerksHead.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        .Col = ColPayableAmount
                        .Text = MainClass.FormatRupees(mTotPayable)

                        .Col = ColDeductionAmount
                        .Text = MainClass.FormatRupees(mTotDeduct)
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

        Exit Sub
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

        SqlStr = " SELECT SALTRN.*, EMP.EMP_NAME, EMP.EMP_DOJ, EMP.EMP_FNAME, " & vbCrLf & " ADD_DEDUCT.NAME AS ADDNAME, ADD_DEDUCT.ADDDEDUCT,ADD_DEDUCT.SEQ,EMP_BANK_NO,DEPT_DESC,DESG_DESC " & vbCrLf & " FROM PAY_PERKS_TRN SALTRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT, PAY_DEPT_MST DEPT, PAY_DESG_MST DESG" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND EMP.COMPANY_CODE =DEPT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE =DEPT.DEPT_CODE " & vbCrLf & " AND EMP.COMPANY_CODE =DESG.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DESG_CODE =DESG.DESG_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.ADD_DEDUCTCODE =ADD_DEDUCT.CODE "

        SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT = " & ConPerks & " AND ADD_DEDUCT.PAYMENT_TYPE='M'"

        '    If lblIsArrear.Caption = "P" Then
        '        SqlStr = SqlStr & vbCrLf & " AND CALC_ON <> " & ConCalcVariable & ""
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND CALC_ON = " & ConCalcVariable & ""
        '    End If

        '    SqlStr = SqlStr & vbCrLf & " AND SALTRN.BOOKTYPE='" & lblIsArrear.Caption & "'"

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

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_FNAME").Value), "", RsAttn.Fields("EMP_FNAME").Value)

                    .Col = ColPaymentType
                    .Text = IIf(RsAttn.Fields("PAYMENT_TYPE").Value = "2", "Cash", "Cheque")

                    mBankAcctNo = IIf(IsDbNull(RsAttn.Fields("EMP_BANK_NO").Value), "", RsAttn.Fields("EMP_BANK_NO").Value)


                    '                If lblIsArrear.Caption = "Y" Then
                    '                    mArrearStr = GetEMPWEFDate(mCode, lblRunDate.Caption)
                    '                    mBankAcctNo = mBankAcctNo & String(15 - Len(mBankAcctNo), " ") & vbNewLine & mArrearStr
                    '                End If

                    .Col = ColBankNo
                    .Text = mBankAcctNo

                    .Col = ColDept
                    .Text = IIf(IsDbNull(RsAttn.Fields("DEPT_DESC").Value), "", RsAttn.Fields("DEPT_DESC").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDbNull(RsAttn.Fields("DESG_DESC").Value), "", RsAttn.Fields("DESG_DESC").Value)

                    .Col = ColDOJ
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("EMP_DOJ").Value), "", RsAttn.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                    .Col = ColDays
                    .Text = CStr(0) ''CStr(RsAttn!WDAYS)

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

            .Col = ColSalType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColSalType, 7)
            .ColHidden = False

            .Col = ColCard
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColName
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColFName
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColPaymentType
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColDept
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColDesg
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColDOJ
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColBankNo
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            '        .Col = ColSalType
            '        .ColMerge = MergeAlways

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
                    '                If lblIsArrear.Caption = "N" Then
                    mBankAcct = MainClass.AllowSingleQuote(Trim(GridName.Text))
                    '                Else
                    '                    mBankAcct = MainClass.AllowSingleQuote(Left(Trim(GridName.Text), 15))
                    '                End If

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
        Dim mInaamAmount As Double

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



            If frmPrintSalReg.optAll(1).Checked = True Then
                If UCase(Trim(frmPrintSalReg.TxtEmpCode.Text)) <> UCase(Trim(mEmpCode)) Then
                    GoTo NextRecord
                End If
            End If
            ''ColSalType

            GridName.Col = ColSalType
            If VB.Left(GridName.Text, 1) = "A" Then
                If UpdateArrearInPaySlip(GridName, RowNum, RowNum) = False Then GoTo PrintDummyErr
                GoTo NextRecord
            End If

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
            '        If lblIsArrear.Caption = "N" Then
            mBankAcct = MainClass.AllowSingleQuote(GridName.Text)
            '        Else
            '            mBankAcct = MainClass.AllowSingleQuote(Left(Trim(GridName.Text), 15))
            '        End If

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
                    mGrossEarn = MainClass.FormatRupees(CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))) - mInaamAmount
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

            mGrossPay = MainClass.FormatRupees(CDbl(mGrossEarn) - CDbl(mGrossDeduct) + CDbl(mInaamAmount))
            mNetPay = MainClass.FormatRupees(System.Math.Round(CDbl(mGrossPay), 0))
            '        mRoundOff = MainClass.FormatRupees(Abs(CDbl(mNetPay) - CDbl(mGrossPay)))
            mNetPayInWord = MainClass.RupeesConversion(CDbl(mNetPay))

            For Colcnt = 1 To MaxColcnt
                SqlStr = " INSERT INTO TEMP_PAYSLIP_TRN ( " & vbCrLf & " USERID, COMPANY_CODE, SUBROW , " & vbCrLf & " EMP_CODE, EMP_NAME, EMP_FNAME, " & vbCrLf & " EMP_DEPT_DESC, EMP_DESG_DESC, EMP_DOJ, " & vbCrLf & " EMP_PF_ACNO, EMP_BANK_NO, ACTUAL_DAYS," & vbCrLf & " PAYABLE_DAYS, BASIC_SALARY, PAYABLE_BASIC_SALARY,"

                SqlStr = SqlStr & vbCrLf & " EARN_TITLE,EARN_RATE,EARN_PAYABLE," & vbCrLf & " DEDUCT_TITLE, DEDUCT_RATE, DEDUCT_PAYABLE," & vbCrLf & " LEAVES, REMARKS, " & vbCrLf & " GROSS_SALARY, GROSS_PAYABLE, " & vbCrLf & " GROSS_DEDUCT, NET_SALARY, EMP_CATG,EMP_ESI_NO,EMP_DOB,HOLIDAYS,INAAM " & vbCrLf & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Colcnt & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "', '" & mEmpFName & "', " & vbCrLf & " '" & mDepartment & "', '" & mDesignation & "','" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "', " & vbCrLf & " '" & mPFNo & "','" & mBankAcct & "', " & Val(CStr(mActualDays)) & "," & vbCrLf & " " & Val(CStr(mWDays)) & ", " & mBSalary & ", " & mPSalary & ", "


                SqlStr = SqlStr & vbCrLf & " '" & mEmpEarnData(Colcnt).mTitle & "'," & mEmpEarnData(Colcnt).mRate & "," & mEmpEarnData(Colcnt).mPayable & "," & vbCrLf & " '" & mEmpDeductData(Colcnt).mTitle & "'," & mEmpDeductData(Colcnt).mRate & "," & mEmpDeductData(Colcnt).mPayable & "," & vbCrLf & " '" & mLeaves & "','" & mNetPayInWord & "', " & vbCrLf & " " & mActualGrossEarn & "," & mGrossEarn & ", " & vbCrLf & " " & mGrossDeduct & ", " & mNetPay & ", '" & mCategory & "','" & mESINo & "'," & vbCrLf & " '" & VB6.Format(mDOB, "DD-MMM-YYYY") & "', " & mHoliDays & "," & mInaamAmount & " )"


                PubDBCn.Execute(SqlStr)
            Next
NextRecord:
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

    Private Function UpdateArrearInPaySlip(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr


        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim ColTotPayable As Integer
        Dim ColTotDeduction As Integer
        Dim ColNum As Integer

        Dim Colcnt As Integer
        Dim MaxColcnt As Integer
        Dim xArrSal() As String

        Dim mEmpCode As String
        Dim mActualDays As Integer
        Dim mWDays As Double
        Dim mBSalary As Double
        Dim mPSalary As Double
        Dim mGrossDeduct As Double
        Dim mGrossPay As Double
        Dim mNetPay As Double
        Dim mGrossEarn As Double
        Dim mActualGrossEarn As Double
        Dim mNetPayInWord As String
        Dim mSalHeadType As Integer
        Dim ColInaam As Integer
        Dim mInaamAmount As Double


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
            If UCase(Trim(GridName.Text)) = UCase(Trim("Total Payable")) Then
                ColTotPayable = ColNum
            End If
            If UCase(Trim(GridName.Text)) = UCase(Trim("Total Deduction")) Then
                ColTotDeduction = ColNum
                Exit For
            End If

        Next

        ReDim xArrSal(GridName.MaxCols)
        ReDim mEmpArrearEarnData(GridName.MaxCols)
        ReDim mEmpArrearDeductData(GridName.MaxCols)

        For ColNum = ColPSalary + 1 To GridName.MaxCols - 1
            GridName.Col = ColNum
            xArrSal(ColNum) = GridName.Text
        Next

        mActualDays = 0 ''MainClass.LastDay(Month(lblRunDate.Caption), Year(lblRunDate.Caption))

        For RowNum = prmStartGridRow To prmEndGridRow
            GridName.Row = RowNum

            GridName.Col = ColCard
            mEmpCode = GridName.Text

            ''ColSalType

            GridName.Col = ColDays
            mWDays = CDbl(GridName.Text)

            GridName.Col = ColSalType
            If VB.Left(GridName.Text, 1) = "S" Then GoTo NextRecord


            GridName.Col = ColBSalary
            mBSalary = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            GridName.Col = ColPSalary
            mPSalary = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))

            Colcnt = 1
            GridName.Col = ColBSalary
            mActualGrossEarn = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
            mInaamAmount = 0

            GridName.Col = ColPSalary + 1
            Do While GridName.Col < GridName.MaxCols

                If GridName.Col < ColTotPayable Then
                    If GridName.Col = ColInaam - 1 Or GridName.Col = ColInaam Then
                        mEmpArrearEarnData(Colcnt).mRate = 0
                        mEmpArrearEarnData(Colcnt).mPayable = 0
                        mEmpArrearEarnData(Colcnt).mTitle = ""
                        If GridName.Col = ColInaam Then
                            mInaamAmount = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            Colcnt = Colcnt + 1
                        End If
                    Else
                        If VB.Left(xArrSal(GridName.Col), 4) = "RATE" Then
                            mEmpArrearEarnData(Colcnt).mRate = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            mActualGrossEarn = mActualGrossEarn + CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                        Else
                            mEmpArrearEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            mEmpArrearEarnData(Colcnt).mTitle = xArrSal(GridName.Col)
                            Colcnt = Colcnt + 1
                        End If
                    End If
                ElseIf GridName.Col = ColTotPayable Then
                    mGrossEarn = MainClass.FormatRupees(CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))) - mInaamAmount
                    MaxColcnt = Colcnt
                    Colcnt = 1
                ElseIf GridName.Col > ColTotPayable And GridName.Col < ColTotDeduction Then
                    If VB.Left(xArrSal(GridName.Col), 4) = "RATE" Then
                        mEmpArrearDeductData(Colcnt).mRate = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                    Else
                        mEmpArrearDeductData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                        mEmpArrearDeductData(Colcnt).mTitle = xArrSal(GridName.Col)
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

            mGrossPay = MainClass.FormatRupees(CDbl(mGrossEarn) - CDbl(mGrossDeduct) + CDbl(mInaamAmount))
            mNetPay = MainClass.FormatRupees(System.Math.Round(CDbl(mGrossPay), 0))
            '        mRoundOff = MainClass.FormatRupees(Abs(CDbl(mNetPay) - CDbl(mGrossPay)))
            mNetPayInWord = MainClass.RupeesConversion(CDbl(mNetPay))

            For Colcnt = 1 To MaxColcnt
                SqlStr = " UPDATE TEMP_PAYSLIP_TRN SET " & vbCrLf & " ARREAR_DAYS = " & Val(CStr(mWDays)) & ", " & vbCrLf & " ARREAR_SALARY = " & mBSalary & ", " & vbCrLf & " PAYABLE_ARREAR_SALARY = " & mPSalary & ", " & vbCrLf & " ARREAR_EARN_RATE = " & mEmpArrearEarnData(Colcnt).mRate & ", " & vbCrLf & " ARREAR_EARN_PAYABLE = " & mEmpArrearEarnData(Colcnt).mPayable & ", " & vbCrLf & " ARREAR_DEDUCT_RATE = " & mEmpArrearDeductData(Colcnt).mRate & ", " & vbCrLf & " ARREAR_DEDUCT_PAYABLE = " & mEmpArrearDeductData(Colcnt).mPayable & ", " & vbCrLf & " ARREAR_GROSS_SALARY = " & mActualGrossEarn & ", " & vbCrLf & " ARREAR_GROSS_PAYABLE = " & mGrossEarn & ", " & vbCrLf & " ARREAR_GROSS_DEDUCT = " & mGrossDeduct & ", " & vbCrLf & " ARREAR_NET_SALARY = " & mNetPay & "," & vbCrLf & " ARREAR_INAAM = " & mInaamAmount & "" & vbCrLf & " WHERE USERID = '" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBROW= " & Colcnt & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "'" ''& vbCrLf |                    & " AND EARN_TITLE='" & mEmpArrearEarnData(Colcnt).mTitle & "'" & vbCrLf |                    & " AND DEDUCT_TITLE='" & mEmpArrearDeductData(Colcnt).mTitle & "'"

                PubDBCn.Execute(SqlStr)
            Next
NextRecord:
        Next

        UpdateArrearInPaySlip = True
        Exit Function
PrintDummyErr:
        'Resume
        UpdateArrearInPaySlip = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

            GridName.Col = ColSalType
            If VB.Left(Trim(GridName.Text), 1) = "S" Then
                mEmpDesc = " " & mEmpName & vbNewLine & " " & mDesignation & vbNewLine & " " & mDepartment & vbNewLine & " " & mBankAcct
            Else
                mEmpDesc = GetEMPWEFDate(Trim(mEmpCode), (lblRunDate.Text)) ''mBankAcct
            End If

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
                    If GridName.Col < ColTotPayable Then
                        If VB.Left(arrsal(GridName.Col), 4) = "RATE" Then
                            mEmpEarnData(Colcnt).mRate = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                        Else
                            mEmpEarnData(Colcnt).mPayable = CDbl(IIf(IsNumeric(GridName.Text), GridName.Text, 0))
                            mEmpEarnData(Colcnt).mTitle = arrsal(GridName.Col)
                            mEmpEarnData(Colcnt).mHeadingDesc = "Rates Payables"
                            Colcnt = Colcnt + 1
                        End If
                    ElseIf GridName.Col > ColTotPayable And GridName.Col < ColTotDeduction Then
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
                SqlStr = " INSERT INTO TEMP_SALREG_TRN ( " & vbCrLf & " USERID, COMPANY_CODE, SUBROW , " & vbCrLf & " EMP_CODE, EMP_DESC, " & vbCrLf & " PAYABLE_DAYS, BASIC_SALARY, PAYABLE_BASIC_SALARY,"

                SqlStr = SqlStr & vbCrLf & " ROW_SEQ, ROW_EARN_DEDUCT, ROW_TITLE,ROW_RATE,ROW_PAYABLE " & vbCrLf & " ) VALUES ( "

                SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RowNum & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpDesc & "', " & vbCrLf & " " & Val(CStr(mWDays)) & ", " & mBSalary & ", " & mPSalary & ", "


                SqlStr = SqlStr & vbCrLf & " " & Colcnt & ", '" & mEmpEarnData(Colcnt).mHeadingDesc & "', '" & mEmpEarnData(Colcnt).mTitle & "'," & mEmpEarnData(Colcnt).mRate & "," & mEmpEarnData(Colcnt).mPayable & "" & vbCrLf & " )"


                PubDBCn.Execute(SqlStr)
            Next
        Next

        PubDBCn.CommitTrans()
        FillSalRegIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        Resume
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

        Dim mBalCL As Double
        Dim mBalEL As Double
        Dim mBalSL As Double


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
                End If

                If RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
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
        GetBalLeave = "EL: " & mMonEarn & "/" & mEarn & "     CL: " & mMonCasual & "/" & mCasual & "     SL: " & mMonSick & "/" & mSick
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
        cmdPFESIPosting.Enabled = mPrintEnable
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
End Class
