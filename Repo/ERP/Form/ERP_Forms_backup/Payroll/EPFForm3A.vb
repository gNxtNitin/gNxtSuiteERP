Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEPFForm3A
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    'Private Const ColCodeNo = 1
    'Private Const ColAcctNo = 2
    'Private Const ColName = 3
    'Private Const ColFName = 4
    'Private Const ColContRate = 5
    'Private Const ColHigherRate = 6
    'Private Const ColMonth = 7
    'Private Const ColWages = 8
    'Private Const ColEPF = 9
    'Private Const ColEmperEPF = 10
    'Private Const ColPFund = 11
    'Private Const ColRefund = 12
    'Private Const ColWOPDays = 13
    'Private Const ColRemarks = 14
    'Private Const ColSum1 = 15
    'Private Const ColSum2 = 16

    Private Const ColArrear As Short = 1
    Private Const ColMonth As Short = 2
    Private Const ColAcctNo As Short = 3
    Private Const ColName As Short = 4
    Private Const ColFName As Short = 5
    Private Const ColContRate As Short = 6
    Private Const ColHigherRate As Short = 7
    Private Const ColDate As Short = 8
    Private Const ColTotWages As Short = 9
    Private Const ColEPF_12 As Short = 10
    Private Const ColEPF_3 As Short = 11
    Private Const ColEPF_8 As Short = 12
    'Private Const ColBlank1 = 13
    'Private Const ColBlank2 = 14
    Private Const ColDateLeave As Short = 13
    Private Const ColWDays As Short = 14

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Function CheckLeaveDate(ByRef mEmpCode As Integer, ByRef mDOL As String, ByRef mSalDate As String, ByRef mREASON As String) As String
        On Error GoTo ErrCheckLeaveDate
        Dim mleavingYM As Integer
        Dim mYM As Integer

        mleavingYM = Val(Year(CDate(VB6.Format(mDOL, "dd/mm/yyyy"))) & Month(CDate(VB6.Format(mDOL, "dd/mm/yyyy"))))
        mYM = Val(Year(CDate(VB6.Format(mSalDate, "dd/mm/yyyy"))) & Month(CDate(VB6.Format(mSalDate, "dd/mm/yyyy"))))

        If mleavingYM = mYM Then
            CheckLeaveDate = "Date of Leaving Service, IF Any, " & VB6.Format(mDOL, "dd/mm/yyyy")
            CheckLeaveDate = CheckLeaveDate & Chr(13) & Chr(13) & "Reasons for Leaving Service, If any " & mREASON
        End If
        Exit Function
ErrCheckLeaveDate:
        MsgBox(Err.Description)
    End Function

    Private Sub FillHeading()

        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(Sprd3A)

        With Sprd3A
            .MaxCols = ColWDays

            .Row = 0

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColAcctNo
            .Text = "Account Number"


            .Col = ColName
            .Text = "Name of Person " & vbNewLine & "(In Block Letters)"

            .Col = ColFName
            .Text = "Father's / Husband's Name"

            .Col = ColContRate
            .Text = "Statutory rate of Contribution"

            .Col = ColHigherRate
            .Text = "Voluntary higher rate of employee's Contribution if any"

            .Col = ColMonth
            .Text = "Month"

            .Col = ColTotWages
            .Text = "Amount of Wages"

            .Col = ColEPF_12
            .Text = "Worker's Share EPF"

            .Col = ColEPF_3
            .Text = "EPF difference between 12% & 8.33 % (if any)"

            .Col = ColEPF_8
            .Text = "Pension Fund Contribution 8.33%"

            '        .Col = ColRefund
            '        .Text = "Refund of Advance"

            '        .Col = ColBlank1
            '        .Text = ""
            '
            '        .Col = ColBlank2
            '        .Text = ""

            .Col = ColDateLeave
            .Text = "Date of Leaving"

            .Col = ColWDays
            .Text = "No, of days / period of non-contributing service if any"

            .set_RowHeight(0, .get_MaxTextRowHeight(0))

            MainClass.ProtectCell(Sprd3A, 0, .MaxRows, 0, .MaxCols)
            Sprd3A.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle ''OperationModeSingle
        End With
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboEmployee.Enabled = False
        Else
            cboEmployee.Enabled = True
        End If
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

        If FillPrintDummyData(Sprd3A, 1, Sprd3A.MaxRows, ColMonth, Sprd3A.MaxCols, PubDBCn) = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)


        mSubTitle = "From " & MonthName(Month(CDate(txtFrom.Text))) & ", " & Year(CDate(txtFrom.Text)) & " To : " & MonthName(Month(CDate(txtTo.Text))) & ", " & Year(CDate(txtTo.Text))
        mTitle = "Form 3A (Revised)"
        Call ShowReport(SqlStr, "PFFORM3A.Rpt", Mode, mTitle, mSubTitle)

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
        FillHeading()
        RefreshScreen()
        FormatSprd(-1)
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_PF_ACNO", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub frmEPFForm3A_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmEPFForm3A_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        optCardNo.Checked = True
        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")

        optAll(0).Checked = True
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        Call FillContCombo()
        cboEmployee.Enabled = False

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub FillContCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = "Select DISTINCT CONT_NAME from PAY_CONTSALARY_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CONT_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboEmployee.Items.Add(RsDept.Fields("CONT_NAME").Value)
                RsDept.MoveNext()
            Loop
            cboEmployee.SelectedIndex = 0
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub RefreshScreenOld()
        'Dim RsAttn As ADODB.Recordset = Nothing
        'Dim cntRow As Long
        'Dim cntCol As Long
        'Dim PrevCode As String
        'Dim mTotWOPDay As Double
        'Dim mTotPFund As Double
        'Dim mTotEmperEPF As Double
        'Dim mTotEPF As Double
        'Dim mTotWages As Double
        'Dim mPFMonth As String
        'Dim mLeavingDate As String
        '
        '    MainClass.ClearGrid Sprd3A
        '
        '    If OptAll(1).Value = True Then
        '        If txtEmpCode.Text = "" Then
        '            MsgInformation "Please select the Employee Code."
        '            txtEmpCode.SetFocus
        '            Exit Sub
        '        End If
        '    End If
        '
        '    SqlStr = " SELECT PFESITRN.EMP_CODE,PFABLEAMT, PFAMT, PFRATE, " & vbCrLf _
        ''        & " EPFAMT, PENSIONFUND, LEAVEWOP, " & vbCrLf _
        ''        & " EMP.EMP_NAME, EMP.EMP_FNAME, EMP.EMP_LEAVE_DATE, EMP.EMP_LEAVE_REASON, " & vbCrLf _
        ''        & " EMP.EMP_PF_ACNO, SAL_DATE " & vbCrLf _
        ''        & " FROM PAY_PFESI_TRN PFESITRN, PAY_EMPLOYEE_MST EMP " & vbCrLf _
        ''        & " WHERE" & vbCrLf _
        ''        & " PFESITRN.COMPANY_CODE =EMP.COMPANY_CODE " & vbCrLf _
        ''        & " AND PFESITRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf _
        ''        & " AND PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''        & " AND SAL_DATE BETWEEN '" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "' " & vbCrLf _
        ''        & " AND '" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''        & " AND PFAMT>0"
        '
        '    If OptAll(1).Value = True Then
        '        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        '    End If
        '
        '    If OptName.Value = True Then
        '        SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_CODE, EMP.EMP_NAME, SAL_DATE"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_PF_ACNO, EMP.EMP_NAME, EMP.EMP_CODE, SAL_DATE"
        '    End If
        '
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsAttn, adLockOptimistic
        '
        '    If RsAttn.EOF = False Then
        '        With Sprd3A
        '            cntRow = 1
        '            Do While Not RsAttn.EOF
        '                .Row = cntRow
        '
        '                If PrevCode <> RsAttn!EMP_CODE Then
        '                    .Col = ColCodeNo
        '                    .Text = CStr(RsAttn!EMP_CODE)
        '                    PrevCode = RsAttn!EMP_CODE
        '
        '                    .Col = ColAcctNo
        '                    .Text = IIf(IsNull(RsAttn!EMP_PF_ACNO), "", RsAttn!EMP_PF_ACNO)
        '
        '                    .Col = ColName
        '                    .Text = RsAttn!EMP_NAME
        '
        '                    .Col = ColFName
        '                    .Text = IIf(IsNull(RsAttn!EMP_FNAME), "", RsAttn!EMP_FNAME)
        '
        '                    .Col = ColContRate
        '                    .Text = CStr(IIf(IsNull(RsAttn!PFRATE), "", RsAttn!PFRATE)) & "%"
        '
        '                    .Col = ColHigherRate
        '                    .Text = ""
        '                 End If
        '                .Col = ColMonth
        '                If Month(RsAttn!SAL_DATE) = 3 Then
        '                    mPFMonth = "March Paid in April"
        '                ElseIf Month(RsAttn!SAL_DATE) = 2 Then
        '                    mPFMonth = "Feb. Paid in March"
        '                Else
        '                    mPFMonth = MonthName(Month(RsAttn!SAL_DATE))
        '                End If
        '
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + mPFMonth
        '                .RowHeight(cntRow) = .MaxTextRowHeight(cntRow)
        '
        '                .Col = ColWages
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + MainClass.FormatRupees(Round(RsAttn!PFABLEAMT, 0))
        '                mTotWages = mTotWages + Round(RsAttn!PFABLEAMT, 0)
        '
        '                .Col = ColEPF
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + MainClass.FormatRupees(Round(RsAttn!PFAMT, 0))
        '                mTotEPF = mTotEPF + Round(RsAttn!PFAMT, 0)
        '
        '                .Col = ColEmperEPF
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + MainClass.FormatRupees(Round(RsAttn!EPFAMT, 0))
        '                mTotEmperEPF = mTotEmperEPF + Round(RsAttn!EPFAMT, 0)
        '
        '                .Col = ColPFund
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + MainClass.FormatRupees(Round(RsAttn!PENSIONFUND, 0))
        '                mTotPFund = mTotPFund + Round(RsAttn!PENSIONFUND, 0)
        '
        '                .Col = ColWOPDays
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + CStr(IIf(IsNull(RsAttn!LEAVEWOP) Or RsAttn!LEAVEWOP = 0, "-", RsAttn!LEAVEWOP))
        '                mTotWOPDay = mTotWOPDay + IIf(IsNull(RsAttn!LEAVEWOP), 0, RsAttn!LEAVEWOP)
        '
        '                .Col = ColRemarks
        '                If Trim(.Text) = "" Then
        '                    If Not IsNull(RsAttn!EMP_LEAVE_DATE) Then
        '                        mLeavingDate = CheckLeaveDate(RsAttn!EMP_CODE, RsAttn!EMP_LEAVE_DATE, RsAttn!SAL_DATE, IIf(IsNull(RsAttn!EMP_LEAVE_REASON), "", RsAttn!EMP_LEAVE_REASON))
        '                        .Text = mLeavingDate
        '                    End If
        '                End If
        '                RsAttn.MoveNext
        '                If Not RsAttn.EOF Then
        '                    If PrevCode <> RsAttn!EMP_CODE Then
        '                        Call RowSubTotal(cntRow, mTotWages, mTotEPF, mTotEmperEPF, mTotPFund, mTotWOPDay)
        '                        cntRow = cntRow + 1
        '                        .MaxRows = cntRow
        '                        mTotWages = 0
        '                        mTotEPF = 0
        '                        mTotEmperEPF = 0
        '                        mTotPFund = 0
        '                        mTotWOPDay = 0
        '                    End If
        '                Else
        '                    Call RowSubTotal(cntRow, mTotWages, mTotEPF, mTotEmperEPF, mTotPFund, mTotWOPDay)
        '                End If
        '            Loop
        '            MainClass.ProtectCell Sprd3A, 0, .MaxRows, 0, .MaxCols
        '
        '        End With
        '    End If
    End Sub
    Private Sub RefreshScreen()

        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim PrevCode As String
        Dim mTotWOPDay As Double
        Dim mTotPFund As Double
        Dim mTotEmperEPF As Double
        Dim mTotEPF As Double
        Dim mTotWages As Double
        Dim mPFMonth As String
        Dim mLeavingDate As String

        MainClass.ClearGrid(Sprd3A)

        If optAll(1).Checked = True Then
            If txtEmpCode.Text = "" Then
                MsgInformation("Please select the Employee Code.")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboEmployee.Text = "" Then
                MsgInformation("Please select the Employer Name.")
                cboEmployee.Focus()
                Exit Sub
            End If
        End If
        '' last_day('02-FEB-2008')-(add_months(last_day('09-FEB-2008'),-1))
        ''
        ''TO_CHAR(EPF_AMT-EPF_367)
        SqlStr = " SELECT '', MONTH_DESC, PFAC_CODE AS EMP_PF_ACNO, " & vbCrLf & " EMP_NAME, EMP_FNAME, 12 AS PFRATE, VPFRATE, EDATE,  " & vbCrLf & " TOT_WAGES AS PFABLEAMT, EPF_AMT+VPFAMT AS PFAMT,  " & vbCrLf & " EPF_367 AS EPFAMT,  " & vbCrLf & " EPF_833 AS PENSIONFUND,  " & vbCrLf & " LEAVEDATE AS EMP_LEAVE_REASON, WDAYS" & vbCrLf & " FROM PAY_CONTSALARY_TRN PFESITRN " & vbCrLf & " WHERE "

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
        Else
            SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE  IN (" & RsCompany.Fields("COMPANY_CODE").Value & ",4,15,11) "
        End If

        SqlStr = SqlStr & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TOT_WAGES>0"

        If optAll(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND PFAC_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND PFESITRN.CONT_NAME='" & MainClass.AllowSingleQuote(Trim(cboEmployee.Text)) & "' "
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by  EMP_NAME, EDATE"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by PFAC_CODE, EMP_NAME, EDATE"
        End If

        MainClass.AssignDataInSprd8(SqlStr, Sprd3A, StrConn, "Y")


        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsAttn, adLockOptimistic
        '
        '    If RsAttn.EOF = False Then
        '        With sprd3A
        '            cntRow = 1
        '            Do While Not RsAttn.EOF
        '                .Row = cntRow
        '
        '                If PrevCode <> RsAttn!EMP_PF_ACNO Then
        '                    .Col = ColCodeNo
        '                    .Text = CStr(RsAttn!EMP_PF_ACNO)
        '                    PrevCode = RsAttn!EMP_PF_ACNO
        '
        '                    .Col = ColAcctNo
        '                    .Text = IIf(IsNull(RsAttn!EMP_PF_ACNO), "", RsAttn!EMP_PF_ACNO)
        '
        '                    .Col = ColName
        '                    .Text = RsAttn!EMP_NAME
        '
        '                    .Col = ColFName
        '                    .Text = IIf(IsNull(RsAttn!EMP_FNAME), "", RsAttn!EMP_FNAME)
        '
        '                    .Col = ColContRate
        '                    .Text = CStr(IIf(IsNull(RsAttn!PFRATE), "", RsAttn!PFRATE)) & "%"
        '
        '                    .Col = ColHigherRate
        '                    .Text = ""
        '                 End If
        '                .Col = ColMonth
        '                If Month(RsAttn!SAL_DATE) = 3 Then
        '                    mPFMonth = "March Paid in April"
        '                ElseIf Month(RsAttn!SAL_DATE) = 2 Then
        '                    mPFMonth = "Feb. Paid in March"
        '                Else
        '                    mPFMonth = MonthName(Month(RsAttn!SAL_DATE))
        '                End If
        '
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + mPFMonth
        '                .RowHeight(cntRow) = .MaxTextRowHeight(cntRow)
        '
        '                .Col = ColWages
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + MainClass.FormatRupees(Round(RsAttn!PFABLEAMT, 0))
        '                mTotWages = mTotWages + Round(RsAttn!PFABLEAMT, 0)
        '
        '                .Col = ColEPF
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + MainClass.FormatRupees(Round(RsAttn!PFAMT, 0))
        '                mTotEPF = mTotEPF + Round(RsAttn!PFAMT, 0)
        '
        '                .Col = ColEmperEPF
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + MainClass.FormatRupees(Round(RsAttn!EPFAMT, 0))
        '                mTotEmperEPF = mTotEmperEPF + Round(RsAttn!EPFAMT, 0)
        '
        '                .Col = ColPFund
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + MainClass.FormatRupees(Round(RsAttn!PENSIONFUND, 0))
        '                mTotPFund = mTotPFund + Round(RsAttn!PENSIONFUND, 0)
        '
        '                .Col = ColWOPDays
        '                .Text = IIf(.Text = "", "", .Text + Chr(13)) + CStr(IIf(IsNull(RsAttn!LEAVEWOP) Or RsAttn!LEAVEWOP = 0, "-", RsAttn!LEAVEWOP))
        '                mTotWOPDay = mTotWOPDay + IIf(IsNull(RsAttn!LEAVEWOP), 0, RsAttn!LEAVEWOP)
        '
        '                .Col = ColRemarks
        '                If Trim(.Text) = "" Then
        '                    If Not IsNull(RsAttn!EMP_LEAVE_DATE) Then
        '                        mLeavingDate = CheckLeaveDate(RsAttn!EMP_PF_ACNO, RsAttn!EMP_LEAVE_DATE, RsAttn!SAL_DATE, IIf(IsNull(RsAttn!EMP_LEAVE_REASON), "", RsAttn!EMP_LEAVE_REASON))
        '                        .Text = mLeavingDate
        '                    End If
        '                End If
        '                RsAttn.MoveNext
        '                If Not RsAttn.EOF Then
        '                    If PrevCode <> RsAttn!EMP_PF_ACNO Then
        '                        Call RowSubTotal(cntRow, mTotWages, mTotEPF, mTotEmperEPF, mTotPFund, mTotWOPDay)
        '                        cntRow = cntRow + 1
        '                        .MaxRows = cntRow
        '                        mTotWages = 0
        '                        mTotEPF = 0
        '                        mTotEmperEPF = 0
        '                        mTotPFund = 0
        '                        mTotWOPDay = 0
        '                    End If
        '                Else
        '                    Call RowSubTotal(cntRow, mTotWages, mTotEPF, mTotEmperEPF, mTotPFund, mTotWOPDay)
        '                End If
        '            Loop
        '            MainClass.ProtectCell sprd3A, 0, .MaxRows, 0, .MaxCols
        '
        '        End With
        '    End If
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With Sprd3A
            .MaxCols = ColWDays
            .Row = mRow

            .Col = ColMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColMonth, 17)

            .Col = ColArrear
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColArrear, 7)
            .ColHidden = True

            .Col = ColAcctNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColAcctNo, 11.5)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 20)

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColFName, 20)
            .ColHidden = True

            .Col = ColContRate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColContRate, 20)
            .ColHidden = True

            .Col = ColHigherRate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColHigherRate, 10)
            .ColHidden = True

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)
            .ColHidden = True

            .Col = ColTotWages
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTotWages, 9)

            .Col = ColEPF_12
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEPF_12, 9)

            .Col = ColEPF_3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEPF_3, 9)

            .Col = ColEPF_8
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEPF_8, 9)

            '        .Col = ColBlank1
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .TypeEditMultiLine = False
            '        .ColWidth(ColBlank1) = 10
            '        .ColHidden = True
            '
            '        .Col = ColBlank2
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .TypeEditMultiLine = False
            '        .ColWidth(ColBlank2) = 10
            '        .ColHidden = True

            .Col = ColDateLeave
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)
            .ColHidden = False

            .Col = ColWDays
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColWDays, 10)
            .ColHidden = False

        End With

        MainClass.ProtectCell(Sprd3A, 1, Sprd3A.MaxRows, 1, Sprd3A.MaxCols)
        MainClass.SetSpreadColor(Sprd3A, mRow)
        Sprd3A.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal


        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub frmEPFForm3A_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        Sprd3A.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAll.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAll.GetIndex(eventSender)
            If optAll(0).Checked = True Then
                txtEmpCode.Enabled = False
                cmdsearch.Enabled = False
            ElseIf optAll(1).Checked = True Then
                txtEmpCode.Enabled = True
                cmdsearch.Enabled = True
            End If
        End If
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_PF_ACNO", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtFrom.Text = VB6.Format(txtFrom.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Sub RowSubTotal(ByRef mRow As Integer, ByRef mTotWages As Double, ByRef mTotEPF As Double, ByRef mTotEmperEPF As Double, ByRef mTotPFund As Double, ByRef mTotWOPDay As Double)
        '
        '    With Sprd3A
        '        .Row = mRow
        '        .Col = ColMonth
        '
        '        .Text = IIf(.Text = "", "", .Text + Chr(13) + Chr(13)) + "Total :"
        '        .RowHeight(mRow) = .MaxTextRowHeight(mRow)
        '
        '        .Col = ColWages
        '        .Text = IIf(.Text = "", "", .Text + Chr(13) + Chr(13)) + MainClass.FormatRupees(mTotWages)
        '
        '        .Col = ColEPF
        '        .Text = IIf(.Text = "", "", .Text + Chr(13) + Chr(13)) + MainClass.FormatRupees(mTotEPF)
        '
        '        .Col = ColEmperEPF
        '        .Text = IIf(.Text = "", "", .Text + Chr(13) + Chr(13)) + MainClass.FormatRupees(mTotEmperEPF)
        '
        '        .Col = ColPFund
        '        .Text = IIf(.Text = "", "", .Text + Chr(13) + Chr(13)) + MainClass.FormatRupees(mTotPFund)
        '
        '        .Col = ColWOPDays
        '        .Text = IIf(.Text = "", "", .Text + Chr(13) + Chr(13)) + CStr(IIf(mTotWOPDay = 0, "-", mTotWOPDay))
        '
        '        .Col = ColSum1
        '        .Text = MainClass.FormatRupees(mTotEPF + mTotEmperEPF)
        '
        '        .Col = ColSum2
        '        .Text = MainClass.FormatRupees(mTotPFund)
        '    End With
    End Sub
End Class
