Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLeaveSummary
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection					

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColDept As Short = 3
    Private Const ColWDays As Short = 4
    Private Const ColOpening As Short = 5
    Private Const ColEntitle As Short = 6
    Private Const ColCASUAL As Short = 7
    Private Const ColEARN As Short = 8
    Private Const ColSICK As Short = 9
    Private Const ColMATERNITY As Short = 10
    Private Const ColABSENT As Short = 11
    Private Const ColWOPAY As Short = 12
    Private Const ColCPLEarn As Short = 13
    Private Const ColCPLAVAIL As Short = 14
    Private Const ColWFH As Short = 15
    Private Const ColCashPaid As Short = 16
    Private Const ColTotAvailed As Short = 17
    Private Const ColTotBalance As Short = 18


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdLeave
            .MaxCols = ColTotBalance
            .Row = mRow
            '        .RowHeight(mRow) = ConRowHeight	

            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 20)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 15)

            For cntCol = ColWDays To ColTotBalance
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditMultiLine = True
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 6)
            Next

            .ColsFrozen = ColName
        End With


        MainClass.ProtectCell(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols)
        sprdLeave.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle					
        MainClass.SetSpreadColor(sprdLeave, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdLeave)

        With sprdLeave
            .MaxCols = ColTotBalance
            .Row = 0


            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDept
            .Text = "Employees' Dept "

            .Col = ColWDays
            .Text = "W.Days"

            .Col = ColOpening
            .Text = "Opening"

            .Col = ColEntitle
            .Text = "Entitled Leaves"

            .Col = ColCASUAL
            .Text = "Casual"

            .Col = ColEARN
            .Text = "Earn"

            .Col = ColSICK
            .Text = "Sick"

            .Col = ColMATERNITY
            .Text = "Maternity"

            .Col = ColABSENT
            .Text = "Un Approved"

            .Col = ColWOPAY
            .Text = "Approved"

            .Col = ColCPLEarn
            .Text = "CPL Earn"

            .Col = ColCPLAVAIL
            .Text = "CPL Avail"

            .Col = ColWFH
            .Text = "W.F.H."

            .Col = ColCashPaid
            .Text = "Cash Paid"

            .Col = ColTotAvailed
            .Text = "Total Availed"

            .Col = ColTotBalance
            .Text = "Total Balance"

        End With
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
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
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()


        '''''Insert Data from Grid to PrintDummyData Table...					


        If FillPrintDummyData(sprdLeave, 1, sprdLeave.MaxRows, ColCode, sprdLeave.MaxCols, PubDBCn) = False Then GoTo ERR1



        '''''Select Record for print...					

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "From : " & txtFrom.Text & " To : " & txtTo.Text
        mTitle = "Leave Summary"
        Call ShowReport(SqlStr, "LeaveSummary.Rpt", Mode, mTitle, mSubTitle)

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
        RefreshScreen()
        Call FormatSprd(-1)
    End Sub
    Private Sub frmLeaveSummary_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen					
    End Sub
    Private Sub frmLeaveSummary_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        txtFrom.Text = "01/01/" & Year(CDate(RunDate))
        txtFrom.Enabled = False

        txtTo.Text = CStr(RunDate)
        OptName.Checked = True

        FillHeading()

        FillDeptCombo()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        cboCategory.Enabled = False

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub frmLeaveSummary_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdLeave.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1					
        '    MainClass.SetSpreadColor SprdOption, -1					
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsEmp As ADODB.Recordset
        Dim mMonth As Short
        Dim mYear As Short
        Dim cntRow As Integer
        Dim mDOJ As Date
        Dim mDOL As Date
        Dim mDeptCode As String
        Dim xDeptCode As String
        Dim mDeptName As String

        MainClass.ClearGrid(sprdLeave)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If
        '					
        '    mMonth = Month(lblRunDate.Caption)					
        '    mYear = Year(lblRunDate.Caption)					
        '					
        '    mDOJ = MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear					
        '    mDOL = "01" & "/" & mMonth & "/" & mYear					

        SqlStr = " Select * From PAY_EMPLOYEE_MST" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND EMP_STOP_SALARY='N' AND " & vbCrLf _
            & " EMP_DOJ <=TO_DATE('" & VB6.Format(txtTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') AND EMP_STOP_SALARY='N'"

        If OptShow(0).Checked = True Then ''All					
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " OR" & vbCrLf & " EMP_LEAVE_DATE IS NULL)"

        ElseIf OptShow(1).Checked = True Then  ''Present					
            SqlStr = SqlStr & vbCrLf & " AND EMP_LEAVE_DATE IS NULL"

        Else ''Left					
            SqlStr = SqlStr & vbCrLf & " AND EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_LEAVE_DATE <=TO_DATE('" & VB6.Format(txtTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME"
        ElseIf OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP_DEPT_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            With sprdLeave
                Do While Not RsEmp.EOF
                    .Row = .MaxRows
                    '                .RowHeight(.Row) = ConRowHeight * 3					

                    .Col = ColCode
                    .Text = IIf(IsDBNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value)

                    .Col = ColName
                    .Text = RsEmp.Fields("EMP_NAME").Value

                    xDeptCode = IIf(IsDBNull(RsEmp.Fields("EMP_DEPT_CODE").Value), "", RsEmp.Fields("EMP_DEPT_CODE").Value)
                    'mDeptName = ""

                    'If MainClass.ValidateWithMasterTable(xDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '    mDeptName = MasterNo
                    'End If

                    .Col = ColDept
                    .Text = xDeptCode

                    Call FillDataInSprd(RsEmp.Fields("EMP_CODE").Value, .Row)
                    RsEmp.MoveNext()
                    If Not RsEmp.EOF Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
                MainClass.ProtectCell(sprdLeave, 0, .MaxRows, 0, .MaxCols)
            End With
        End If
        CmdPreview.Enabled = True
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume					
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset
        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST WHERE COMPANy_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
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
    Private Sub FillDataInSprd(ByRef mCode As String, ByRef mRow As Integer)

        Dim RsOpLeave As ADODB.Recordset
        Dim RsLeave As ADODB.Recordset
        Dim RsEmp As ADODB.Recordset

        Dim mOpSick As Double
        Dim mOpCasual As Double
        Dim mOpEarn As Double
        Dim mOpCPL As Double
        Dim mSick As Double
        Dim mCasual As Double
        Dim mEarn As Double
        Dim mCPL As Double
        Dim mWFH As Double
        Dim mCashPaid As Double
        Dim mPrevSick As Double
        Dim mPrevCasual As Double
        Dim mPrevEarn As Double
        Dim mPrevCPL As Double
        Dim mTotAvailed As Double
        Dim criteriaMonth As String
        'Dim mMonth As Integer					
        'Dim mYear As Integer					
        Dim I As Integer
        Dim mELEntitlement As Double

        Dim mWopay As Double
        Dim mMaternity As Double
        Dim mAbsent As Double
        Dim mHoliday As Double
        Dim mDOJ As String
        Dim mDOL As String
        Dim mCategory As String
        Dim mTotalRunningDays As Double
        Dim mStartingDate As String
        Dim mEndingDate As String
        Dim mOPDate As Date
        '    mMonth = Month(lblRunDate.Caption)					
        '    mYear = Year(lblRunDate.Caption)					

        mStartingDate = txtFrom.Text '' "01/01/" & Year(lblRunDate.Caption)					
        mEndingDate = txtTo.Text '' lblRunDate.Caption					
        SqlStr = " SELECT EMP_DOJ, EMP_LEAVE_DATE,EMP_CATG " & vbCrLf & " FROM PAY_EMPLOYEE_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            mDOJ = IIf(IsDBNull(RsEmp.Fields("EMP_DOJ").Value), "", RsEmp.Fields("EMP_DOJ").Value)
            mDOL = IIf(IsDBNull(RsEmp.Fields("EMP_LEAVE_DATE").Value), "", RsEmp.Fields("EMP_LEAVE_DATE").Value)
            mCategory = IIf(IsDBNull(RsEmp.Fields("EMP_CATG").Value), "G", RsEmp.Fields("EMP_CATG").Value)
        End If

        If mDOJ = "" Then

        ElseIf CDate(txtFrom.Text) < CDate(mDOJ) Then
            mStartingDate = mDOJ
        End If

        If mDOL = "" Then

        ElseIf CDate(txtTo.Text) > CDate(mDOL) Then
            mEndingDate = mDOL
        End If

        mTotalRunningDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartingDate), CDate(mEndingDate)) + 1

        mOPDate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFrom.Text))

        mOpEarn = mOpEarn + GetOpeningLeaves(mCode, (txtFrom.Text), SICK, "Y", "N", "")
        mOpEarn = mOpEarn + GetOpeningLeaves(mCode, (txtFrom.Text), CASUAL, "Y", "N", "")
        mOpEarn = mOpEarn + GetOpeningLeaves(mCode, (txtFrom.Text), EARN, "Y", "N", "")

        mELEntitlement = mELEntitlement + GetOpeningLeaves(mCode, (txtTo.Text), SICK, "N", "Y", "") '					
        mELEntitlement = mELEntitlement + GetOpeningLeaves(mCode, (txtTo.Text), CASUAL, "N", "Y", "") '					
        mELEntitlement = mELEntitlement + GetOpeningLeaves(mCode, (txtTo.Text), EARN, "N", "Y", "") '					


        '    mELEntitlement = GETEntitleEarnLeave(PubDBCn, mCode, EARN, mEndingDate)					
        '					
        '    SqlStr = " SELECT LEAVECODE,SUM(OPENING) AS OPENING, SUM(TOTENTITLE) AS TOTENTITLE " & vbCrLf _					
        ''        & " FROM PAY_OPLEAVE_TRN  WHERE" & vbCrLf _					
        ''        & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _					
        ''        & " AND PAYYEAR = " & Year(mEndingDate) & "" & vbCrLf _					
        ''        & " AND EMP_CODE ='" & mCode & "' GROUP BY LEAVECODE"					
        '					
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOpLeave, adLockOptimistic					
        '					
        '    If RsOpLeave.EOF = False Then					
        '        Do While Not RsOpLeave.EOF					
        ''            If RsOpLeave!LeaveCode = SICK Then					
        ''                mOpSick = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING) ''+ IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE)					
        ''            ElseIf RsOpLeave!LeaveCode = CASUAL Then					
        ''                mOpCasual = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING) '' + IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE)					
        ''            ElseIf RsOpLeave!LeaveCode = EARN Then					
        ''                mOpEarn = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING)					
        ''            End If					
        '            '					
        '					
        '            RsOpLeave.MoveNext					
        '        Loop					
        '    End If					

        '    mOPDate = DateAdd("d", -1, txtFrom.Text)					
        '    If Year(mOPDate) = Year(txtFrom.Text) Then					
        '        mOPBal = GetOpeningLeaves(mCode, mOPDate, mLeaveType, "Y", "Y", mOPDate)					
        '        mETLeaves = GetOpeningLeaves(mCode, txtTo.Text, mLeaveType, "N", "Y", "") - GetOpeningLeaves(mCode, mOPDate, mLeaveType, "N", "Y", "")					
        '    Else					
        '        mOPBal = GetOpeningLeaves(mCode, txtFrom.Text, mLeaveType, "Y", "N", "")					
        '        mETLeaves = GetOpeningLeaves(mCode, txtTo.Text, mLeaveType, "N", "Y", "") ''					
        '    End If					

        '    mOpEarn = mOpEarn + mELEntitlement					

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE" & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAYYEAR=" & Year(CDate(mEndingDate)) & "" & vbCrLf _
            & " AND EMP_CODE ='" & mCode & "'" & vbCrLf _
            & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While Not RsLeave.EOF

                If RsLeave.Fields("FIRSTHALF").Value = SICK Then
                    mSick = mSick + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                    mHoliday = mHoliday + 0.5
                    '            ElseIf RsLeave!FIRSTHALF = CPLEARN Then					
                    '                mOpCPL = mOpCPL + 0.5					
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CPLAVAIL Then
                    mCPL = mCPL + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = WFH Then
                    mWFH = mWFH + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = MATERNITY Then
                    mMaternity = mMaternity + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = WOPAY Then
                    mWopay = mWopay + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SUNDAY Or RsLeave.Fields("FIRSTHALF").Value = HOLIDAY Then
                    If GetHolidayAgtWorking(RsLeave.Fields("ATTN_DATE").Value) = "N" Then
                        mHoliday = mHoliday + 0.5
                    End If
                End If

                If RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                    mHoliday = mHoliday + 0.5
                    '            ElseIf RsLeave!SECONDHALF = CPLEARN Then					
                    '                mOpCPL = mOpCPL + 0.5					
                ElseIf RsLeave.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPL = mCPL + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = WFH Then
                    mWFH = mWFH + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = MATERNITY Then
                    mMaternity = mMaternity + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = WOPAY Then
                    mWopay = mWopay + 0.5
                    mHoliday = mHoliday + 0.5
                ElseIf RsLeave.Fields("SECONDHALF").Value = SUNDAY Or RsLeave.Fields("SECONDHALF").Value = HOLIDAY Then
                    If GetHolidayAgtWorking(RsLeave.Fields("ATTN_DATE").Value) = "N" Then
                        mHoliday = mHoliday + 0.5
                    End If
                End If
                mOpCPL = IIf(IsDBNull(RsLeave.Fields("CPL_EARN").Value), 0, RsLeave.Fields("CPL_EARN").Value) * 0.5
                RsLeave.MoveNext()
            Loop
            mTotAvailed = mSick + mCasual + mEarn '''+ mOpCPL - mCPL					
        End If


        mCashPaid = GetPaidEL(mCode, (txtFrom.Text), PubDBCn, "", (txtTo.Text))


        mTotAvailed = mSick + mCasual + mEarn + mCashPaid

        With sprdLeave
            .Row = mRow

            .Col = ColWDays
            .Text = CStr(mTotalRunningDays - mHoliday)

            .Col = ColOpening
            .Text = CStr(mOpSick + mOpCasual + mOpEarn)

            .Col = ColEntitle
            .Text = CStr(mELEntitlement)

            .Col = ColCASUAL
            .Text = CStr(mCasual)

            .Col = ColEARN
            .Text = CStr(mEarn)

            .Col = ColSICK
            .Text = CStr(mSick)

            .Col = ColMATERNITY
            .Text = CStr(mMaternity)

            .Col = ColABSENT
            .Text = CStr(mAbsent)

            .Col = ColWOPAY
            .Text = CStr(mWopay)

            .Col = ColCPLEarn
            .Text = CStr(mOpCPL)

            .Col = ColCPLAVAIL
            .Text = CStr(mCPL)

            .Col = ColWFH
            .Text = CStr(mWFH)

            .Col = ColCashPaid
            .Text = CStr(mCashPaid)

            .Col = ColTotAvailed
            .Text = CStr(mTotAvailed)

            .Col = ColTotBalance
            .Text = CStr(mOpSick + mOpCasual + mOpEarn + mELEntitlement - mTotAvailed)

        End With
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtFrom.Text) = "" Or Trim(txtFrom.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtFrom.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        '    If Year(txtFrom.Text) <> PubPAYYEAR Then					
        '        MsgBox "Invalid Current Calender Year Date", vbInformation					
        '        Cancel = True					
        '        Exit Sub					
        '    End If					
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtTo.Text) = "" Or Trim(txtTo.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtTo.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        '    If Year(txtTo.Text) <> PubPAYYEAR Then					
        '        MsgBox "Invalid Current Calender Year Date", vbInformation					
        '        Cancel = True					
        '        Exit Sub					
        '    End If					
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
