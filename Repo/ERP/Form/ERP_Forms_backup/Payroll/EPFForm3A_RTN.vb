Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEPFForm3A_Rtn
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColAcctNo As Short = 1
    Private Const ColMonthYear As Short = 2
    Private Const ColExtra1 As Short = 3
    Private Const ColExtra2 As Short = 4
    Private Const ColBlank1 As Short = 5
    Private Const ColTotWages As Short = 6
    Private Const ColPensionWages As Short = 7
    Private Const ColNPC As Short = 8
    Private Const ColEPF_12 As Short = 9
    Private Const ColEPF_3 As Short = 10
    Private Const ColEPF_8 As Short = 11
    Private Const ColRefund_ER As Short = 12
    Private Const ColRemarks As Short = 13
    Private Const ColBlank2 As Short = 14
    Private Const ColBlank3 As Short = 15
    Private Const ColHigherRate_EPF As Short = 16
    Private Const ColHigherRate_EPS As Short = 17
    Private Const ColBlank4 As Short = 18
    Private Const ColContRate As Short = 19
    Private Const ColBlank5 As Short = 20
    Private Const ColSNo As Short = 21
    Private Const ColName As Short = 22
    Private Const ColNetWages As Short = 23
    Private Const ColNetEE As Short = 24
    Private Const ColNetER As Short = 25
    Private Const ColNetPEN As Short = 26
    Private Const ColNetNCP As Short = 27
    Private Const ColNetRefund_ER As Short = 28

    '
    'Private Const ColArrear = 1
    'Private Const ColMonth = 2
    'Private Const ColAcctNo = 3
    '
    'Private Const ColFName = 5
    '
    'Private Const ColHigherRate = 7
    'Private Const ColDate = 8
    '
    '
    ''Private Const ColBlank1 = 13
    ''Private Const ColBlank2 = 14
    'Private Const ColDateLeave = 13


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
            .MaxCols = ColNetRefund_ER

            .Row = 0

            .Col = ColAcctNo
            .Text = "PF NO"

            .Col = ColMonthYear
            .Text = "Month Year"

            .Col = ColExtra1
            .Text = "Extra"

            .Col = ColExtra2
            .Text = "Extra"

            .Col = ColBlank1
            .Text = "Blank"

            .Col = ColTotWages
            .Text = "Wages"

            .Col = ColPensionWages
            .Text = "Pension Wages"

            .Col = ColNPC
            .Text = "NPC Days"

            .Col = ColEPF_12
            .Text = "Contribution EE"

            .Col = ColEPF_3
            .Text = "Contribution ER"

            .Col = ColEPF_8
            .Text = "Contribution Pension"

            .Col = ColRefund_ER
            .Text = "Refund ER"

            .Col = ColRemarks
            .Text = "Remarks"

            .Col = ColBlank2
            .Text = "Blank"

            .Col = ColBlank3
            .Text = "Blank"

            .Col = ColHigherRate_EPF
            .Text = "Higher Wages Contribution EPF"

            .Col = ColHigherRate_EPS
            .Text = "Higher Wages Contribution EPS"

            .Col = ColBlank4
            .Text = "Blank"

            .Col = ColContRate
            .Text = "Contribution Rate"

            .Col = ColBlank5
            .Text = "Blank"

            .Col = ColSNo
            .Text = "Sno"

            .Col = ColName
            .Text = "Name"

            .Col = ColNetWages
            .Text = "Total Wages"

            .Col = ColNetEE
            .Text = "Total EE"

            .Col = ColNetER
            .Text = "Total ER"

            .Col = ColNetPEN
            .Text = "Total Pension"

            .Col = ColNetNCP
            .Text = "Total NCP"

            .Col = ColNetRefund_ER
            .Text = "Total Refund"



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

    Private Sub cmdCD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCD.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim SqlStr As String = ""
        Dim mEST_CODEStr As String
        Dim mDelimited As String
        Dim pFileName As String
        Dim mMainString As String

        Dim mTotMember As Integer
        Dim mYear As String
        Dim mWages As Double
        Dim mContEE As Double
        Dim mContRR As Double
        Dim mContPen As Double
        Dim mNCP As Double
        Dim mAccNo As String

        mDelimited = "#~#"
        pFileName = mLocalPath & "\Form6A.txt"


        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        FileOpen(1, pFileName, OpenMode.Output)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mEST_CODEStr = RsCompany.Fields("PFEST").Value

        mTotMember = 0
        mAccNo = ""

        With Sprd3A
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAcctNo
                If mAccNo <> Trim(.Text) Then
                    mTotMember = mTotMember + 1
                End If
                mAccNo = Trim(.Text)

                mYear = "04" & VB6.Format(txtFrom.Text, "YYYY")
                .Col = ColTotWages
                mWages = mWages + Val(.Text)

                .Col = ColEPF_12
                mContEE = mContEE + Val(.Text)

                .Col = ColEPF_3
                mContRR = mContRR + Val(.Text)

                .Col = ColEPF_8
                mContPen = mContPen + Val(.Text)

                .Col = ColNPC
                mNCP = mNCP + Val(.Text)

            Next
        End With

        mMainString = mEST_CODEStr
        mMainString = mMainString & mDelimited & mTotMember
        mMainString = mMainString & mDelimited & mYear
        mMainString = mMainString & mDelimited & mWages
        mMainString = mMainString & mDelimited & mContEE
        mMainString = mMainString & mDelimited & mContRR
        mMainString = mMainString & mDelimited & mContPen
        mMainString = mMainString & mDelimited & mNCP
        mMainString = mMainString & mDelimited & "0"
        mMainString = mMainString & mDelimited & "0"
        PrintLine(1, TAB(0), mMainString)

        With Sprd3A
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                mMainString = ""
                For cntCol = 1 To .MaxCols
                    .Col = cntCol
                    If cntCol = 1 Then
                        mMainString = Trim(.Text)
                    Else
                        mMainString = mMainString & mDelimited & Trim(.Text)
                    End If
                Next
                PrintLine(1, TAB(0), mMainString)
            Next
        End With


        FileClose(1)

        Shell("ATTRIB +R -A " & pFileName)
        Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    If FoxPvtDBCn.State = adStateClosed Then
        '    End If
        '    Resume
        FileClose(1)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
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

    Private Sub frmEPFForm3A_Rtn_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmEPFForm3A_Rtn_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        'Dim mTotWOPDay As Double
        'Dim mTotPFund As Double
        'Dim mTotEmperEPF As Double
        'Dim mTotEPF As Double
        'Dim mTotWages As Double
        'Dim mPFMonth As String
        'Dim mLeavingDate As String
        Dim mMonthDays As String

        Dim mTotWages As Double
        Dim mTotalEE As Double
        Dim mTotalER As Double
        Dim mTotalPen As Double
        Dim mTotalNCP As Double
        Dim mTotalRefund As Double

        Dim mPFCode As String
        Dim mCurrPFCode As String

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


        mMonthDays = "(last_day(EDATE)-(add_months(last_day(EDATE),-1)))"

        ''DECODE(WDAYS=0,DECODE(TOT_WAGES>6500,6500,TOT_WAGES),6500 * (" & mMonthDays & " - WDAYS) / " & mMonthDays & ")
        SqlStr = " SELECT PFAC_CODE, TO_CHAR(EDATE,'MMYYYY'), '0','0',''," & vbCrLf & " SUM(ROUND(TOT_WAGES,0)), " & vbCrLf & " SUM(CASE WHEN WDAYS=0 THEN CASE WHEN TOT_WAGES>6500 THEN 6500 ELSE TOT_WAGES END ELSE 6500 * (" & mMonthDays & " - WDAYS) / " & mMonthDays & " END) ," & vbCrLf & " SUM(WDAYS), SUM(ROUND(EPF_AMT+VPFAMT,0)), SUM(ROUND(EPF_367,0)), SUM(ROUND(EPF_833,0)), 0, " & vbCrLf & " '', '','','N','N','',12,'',1," & vbCrLf & " EMP_NAME, " & vbCrLf & " 0,0,0,0,0,0"


        SqlStr = SqlStr & vbCrLf & " FROM PAY_CONTSALARY_TRN PFESITRN " & vbCrLf & " WHERE "

        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
        Else
            SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE  IN (" & RsCompany.Fields("COMPANY_CODE").Value & ",4,15,11,25) "
        End If

        SqlStr = SqlStr & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TOT_WAGES>0"

        If optAll(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND PFAC_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND PFESITRN.CONT_NAME='" & MainClass.AllowSingleQuote(Trim(cboEmployee.Text)) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP by  PFAC_CODE, EMP_NAME, TO_CHAR(EDATE,'MMYYYY'),TO_CHAR(EDATE,'YYYYMM')"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by  EMP_NAME, TO_CHAR(EDATE,'YYYYMM')"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by PFAC_CODE, EMP_NAME, TO_CHAR(EDATE,'YYYYMM')"
        End If

        MainClass.AssignDataInSprd8(SqlStr, Sprd3A, StrConn, "Y")

        mPFCode = ""
        With Sprd3A
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAcctNo
                If mPFCode <> Trim(.Text) Then
                    mPFCode = Trim(.Text)
                    mTotWages = 0
                    mTotalEE = 0
                    mTotalER = 0
                    mTotalPen = 0
                    mTotalNCP = 0
                    mTotalRefund = 0
                    Call CalcTotal(mPFCode, mTotWages, mTotalEE, mTotalER, mTotalPen, mTotalNCP, mTotalRefund)
                Else
                    mPFCode = Trim(.Text)
                End If

                .Row = cntRow
                .Col = ColNetWages
                .Text = CStr(mTotWages)

                .Col = ColNetEE
                .Text = CStr(mTotalEE)

                .Col = ColNetER
                .Text = CStr(mTotalER)

                .Col = ColNetPEN
                .Text = CStr(mTotalPen)

                .Col = ColNetNCP
                .Text = CStr(mTotalNCP)

                .Col = ColNetRefund_ER
                .Text = CStr(mTotalRefund)
            Next
        End With

        '    Private Const ColNetWages = 23
        'Private Const ColNetEE = 24
        'Private Const ColNetER = 25
        'Private Const ColNetPEN = 26
        'Private Const ColNetNCP = 27
        'Private Const ColNetRefund_ER = 28

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
    Private Sub CalcTotal(ByRef mPFCode As String, ByRef mTotWages As Double, ByRef mTotalEE As Double, ByRef mTotalER As Double, ByRef mTotalPen As Double, ByRef mTotalNCP As Double, ByRef mTotalRefund As Double)
        Dim cntRow As Integer

        With Sprd3A
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAcctNo
                If mPFCode = Trim(.Text) Then
                    .Col = ColTotWages
                    mTotWages = mTotWages + Val(.Text)

                    .Col = ColEPF_12
                    mTotalEE = mTotalEE + Val(.Text)

                    .Col = ColEPF_3
                    mTotalER = mTotalER + Val(.Text)

                    .Col = ColEPF_8
                    mTotalPen = mTotalPen + Val(.Text)

                    .Col = ColNPC
                    mTotalNCP = mTotalNCP + Val(.Text)

                    .Col = ColRefund_ER
                    mTotalRefund = mTotalRefund + Val(.Text)
                End If
            Next
        End With

    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntRow As Integer

        With Sprd3A
            .MaxCols = ColNetRefund_ER
            .Row = mRow

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 25)

            For cntRow = ColAcctNo To ColBlank1
                .Col = cntRow
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
                .TypeEditMultiLine = False
                .set_ColWidth(cntRow, 12)
            Next

            For cntRow = ColRemarks To ColSNo
                .Col = cntRow
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
                .TypeEditMultiLine = False
                .set_ColWidth(cntRow, 12)
            Next

            For cntRow = ColTotWages To ColRefund_ER
                .Col = cntRow
                .CellType = SS_CELL_TYPE_INTEGER
                '            .TypeFloatDecimalPlaces = 0
                '            .TypeFloatDecimalChar = Asc(".")
                .TypeNumberMax = CDbl("99999999999")
                .TypeNumberMin = CDbl("-99999999999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntRow, 10)
            Next

            For cntRow = ColNetWages To ColNetRefund_ER
                .Col = cntRow
                .CellType = SS_CELL_TYPE_INTEGER
                '            .TypeFloatDecimalPlaces = 0
                '            .TypeFloatDecimalChar = Asc(".")
                .TypeNumberMax = CDbl("99999999999")
                .TypeNumberMin = CDbl("-99999999999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntRow, 10)
            Next

        End With

        MainClass.ProtectCell(Sprd3A, 1, Sprd3A.MaxRows, 1, Sprd3A.MaxCols)
        MainClass.SetSpreadColor(Sprd3A, mRow)
        Sprd3A.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal


        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub frmEPFForm3A_Rtn_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
