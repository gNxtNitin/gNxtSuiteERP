Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLeaveLedger
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
    Private Const ColCard As Short = 2
    Private Const ColName As Short = 3
    Private Const ColType As Short = 4
    Private Const ColOpening As Short = 5
    Private Const ColEntitle As Short = 6
    Private Const ColELPaid As Short = 7
    Private Const ColTakenOn As Short = 8
    Private Const ColDays As Short = 9
    Private Const ColTotAvailed As Short = 10
    Private Const ColLHead As Short = 11

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With sprdLeave
            .MaxCols = ColLHead
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)
            .set_RowHeight(0, ConRowHeight * 5)

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCard, 8)


            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 8)
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 25)

            .Col = ColType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColType, 9)
            .ColsFrozen = ColType

            .Col = ColOpening
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColOpening, 9)

            .Col = ColEntitle
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColEntitle, 9)
            .ColHidden = False

            .Col = ColELPaid
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColELPaid, 9)
            .ColHidden = False

            .Col = ColDays
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColDays, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColTakenOn
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColTakenOn, 9)
            .TypeMaxEditLen = 5000
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColTotAvailed
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .TypeMaxEditLen = 5000
            .set_ColWidth(ColTotAvailed, 9)

            .Col = ColLHead
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColLHead, 9)
        End With

        MainClass.ProtectCell(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols)
        sprdLeave.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle							
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
            .MaxCols = ColLHead
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCard
            .Text = "Card No"

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColType
            .Text = "Leave Type"

            .Col = ColOpening
            .Text = "Opening Leaves From Date"

            .Col = ColEntitle
            .Text = "Leave Entitlement For the Period"

            .Col = ColELPaid
            .Text = "Encash For the Period"

            .Col = ColTotAvailed
            .Text = "Total Leave Availed For the Period"

            .Col = ColTakenOn
            .Text = "Leave Taken On For the Period"

            .Col = ColDays
            .Text = "No. Of Days"

            .Col = ColLHead
            .Text = "Leave in Hand"
        End With
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpCode.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtEmpCode.Enabled = True
            cmdsearch.Enabled = True
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
        cboDept.SelectedIndex = 0

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

        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("SICK")
        cboShow.Items.Add("CASUAL")
        cboShow.Items.Add("EARN")
        cboShow.Items.Add("MATERNITY")
        cboShow.Items.Add("WOPAY")


        cboShow.SelectedIndex = 0

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


        If FillPrintDummyData(sprdLeave, 1, sprdLeave.MaxRows, 0, sprdLeave.MaxCols, PubDBCn) = False Then GoTo ERR1



        '''''Select Record for print...							

        SqlStr = ""

        SqlStr = FetchRecordForLReport(SqlStr)

        mSubTitle = "From : " & txtFrom.Text & " To : " & txtTo.Text

        mTitle = "Leave Ledger"
        Call ShowReport(SqlStr, "LeaveLdgr.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume							
    End Sub
    Private Function FetchRecordForLReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW, FIELD5"

        FetchRecordForLReport = mSqlStr

    End Function
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
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE",  ,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub frmLeaveLedger_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen							
    End Sub
    Private Sub frmLeaveLedger_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection							
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

        txtFrom.Text = "01/01/" & VB6.Format(RunDate, "YYYY")
        txtTo.Text = MainClass.LastDay(Month(RunDate), Year(RunDate)) & "/" & VB6.Format(RunDate, "MM/YYYY")

        FillHeading()
        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

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
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsEmp As ADODB.Recordset
        'Dim mMonth As Integer							
        'Dim mYear As Integer							
        Dim cntRow As Integer
        'Dim mDOJ As Date							
        'Dim mDOL As Date							
        Dim mRow As Integer
        Dim mDeptCode As String = ""
        Dim xEmpCode As String

        MainClass.ClearGrid(sprdLeave)

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If txtEmpCode.Text = "" Then
                MsgInformation("Please select the Employee Code.")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If

        SqlStr = " Select * From PAY_EMPLOYEE_MST" & vbCrLf _
            & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(txtTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') AND EMP_STOP_SALARY='N'"

        If optShowEmp(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtFrom.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " OR" & vbCrLf _
                & " EMP_LEAVE_DATE IS NULL)"
        Else
            SqlStr = SqlStr & vbCrLf & "AND EMP_LEAVE_DATE IS NULL"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And cboDept.Text <> "" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = VB6.Format(MasterNo, "DD/MM/YYYY")
            End If
            SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG<>'C'"
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            With sprdLeave
                .MaxRows = 1
                mRow = 1
                cntRow = 1
                Do While Not RsEmp.EOF
                    .Row = mRow

                    .Col = ColSNO
                    .Text = CStr(cntRow)
                    '                cntRow = cntRow + 2							

                    xEmpCode = IIf(IsDBNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value)
                    .Col = ColCode
                    .Text = xEmpCode

                    .Col = ColCard
                    .Text = xEmpCode

                    .Col = ColName
                    .Text = RsEmp.Fields("EMP_NAME").Value

                    If cboShow.SelectedIndex = 0 Or cboShow.SelectedIndex = 1 Then
                        .Col = ColType
                        .Text = "SICK"

                        Call FillDataInSprd(xEmpCode, SICK, mRow)
                        mRow = mRow + 1
                        .MaxRows = .MaxRows + 1
                    End If

                    If cboShow.SelectedIndex = 0 Or cboShow.SelectedIndex = 2 Then
                        .Row = mRow
                        .Col = ColType
                        .Text = "CASUAL"
                        .Col = ColCode
                        .Text = xEmpCode

                        Call FillDataInSprd(xEmpCode, CASUAL, mRow)
                        mRow = mRow + 1
                        .MaxRows = .MaxRows + 1
                    End If

                    If cboShow.SelectedIndex = 0 Or cboShow.SelectedIndex = 3 Then
                        .Row = mRow
                        .Col = ColType
                        .Text = "EARN"
                        .Col = ColCode
                        .Text = xEmpCode

                        Call FillDataInSprd(xEmpCode, EARN, mRow)
                        mRow = mRow + 1
                        .MaxRows = .MaxRows + 1
                    End If


                    If cboShow.SelectedIndex = 0 Or cboShow.SelectedIndex = 4 Then
                        If GetLeavesAvail(xEmpCode, (txtFrom.Text), (txtTo.Text), MATERNITY) > 0 Then
                            .Row = mRow
                            .Col = ColType
                            .Text = "SP. LEAVE / MATERNITY"
                            .Col = ColCode
                            .Text = xEmpCode

                            Call FillDataInSprd(xEmpCode, MATERNITY, mRow)
                            mRow = mRow + 1
                            .MaxRows = .MaxRows + 1
                        End If
                    End If

                    If cboShow.SelectedIndex = 0 Or cboShow.SelectedIndex = 5 Then
                        If GetLeavesAvail(xEmpCode, (txtFrom.Text), (txtTo.Text), WOPAY) > 0 Then
                            .Row = mRow
                            .Col = ColType
                            .Text = "WOPAY"
                            .Col = ColCode
                            .Text = xEmpCode

                            Call FillDataInSprd(xEmpCode, WOPAY, mRow)
                            mRow = mRow + 1
                            .MaxRows = .MaxRows + 1
                        End If
                    End If

                    '                .Row = mRow							
                    '                .Col = ColType							
                    '                .Text = "CPL"							
                    '                .Col = ColCode							
                    '                .Text = xEmpCode							
                    '							
                    '                Call FillDataInSprd(xEmpCode, CPLEARN, mRow)							
                    '                mRow = mRow + 1							
                    '                .MaxRows = .MaxRows + 1							

                    RsEmp.MoveNext()
                    If Not RsEmp.EOF Then
                        If xEmpCode <> RsEmp.Fields("EMP_CODE").Value Then
                            cntRow = cntRow + 1
                        End If
                    End If
                Loop
                MainClass.ProtectCell(sprdLeave, 0, .MaxRows, 0, .MaxCols)
            End With
        End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillDataInSprd(ByRef mCode As String, ByRef mLeaveType As Integer, ByRef mRow As Integer)

        Dim RsOpLeave As ADODB.Recordset
        Dim RsLeave As ADODB.Recordset
        Dim mOPBal As Double

        Dim mETLeaves As Double

        Dim mLeavesTaken As Double
        Dim mELPaid As Double
        Dim mLeavesTakenDate As String
        Dim mCasualDate As String
        Dim mEarnDate As String

        'Dim mMonth As Integer							
        'Dim mYear As Integer							

        Dim I As Integer
        Dim xFirstRow As Integer
        Dim mELEntitlement As Double
        Dim mCPLEarn As Double
        Dim mMonthStartDate As String
        Dim mOPDate As String

        xFirstRow = mRow
        mLeavesTakenDate = ""
        '    mMonth = Month(txtTo.Text)							
        '    mYear = Year(txtTo.Text)							

        '    If mLeaveType = EARN Then							
        '        mELEntitlement = GETEntitleEarnLeave(PubDBCn, mCode, mLeaveType, txtTo.Text)							
        '    End If							
        '							
        '    If mLeaveType = CPLEARN Then							
        '        mCPLEarn = GETCPL(PubDBCn, mCode, txtTo.Text)							
        '    End If							


        '    SqlStr = " SELECT LEAVECODE,SUM(OPENING) AS OPENING, SUM(TOTENTITLE) AS TOTENTITLE " & vbCrLf _							
        ''            & " FROM PAY_OPLEAVE_TRN  WHERE" & vbCrLf _							
        ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _							
        ''            & " AND PAYYEAR = " & Year(txtTo.Text) & "" & vbCrLf _							
        ''            & " AND EMP_CODE ='" & mCode & "' AND LEAVECODE=" & mLeaveType & " GROUP BY LEAVECODE"							
        '							
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOpLeave, adLockOptimistic							
        '							
        '    If RsOpLeave.EOF = False Then							
        '        Do While Not RsOpLeave.EOF							
        '            If RsOpLeave!LeaveCode = SICK Then							
        '                mOPBal = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING) + IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE)							
        '            ElseIf RsOpLeave!LeaveCode = CASUAL Then							
        '                mOPBal = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING) + IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE)							
        '            ElseIf RsOpLeave!LeaveCode = EARN Then							
        '                mOPBal = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING)							
        '            ElseIf RsOpLeave!LeaveCode = CPLEARN Then							
        '                mOPBal = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING)							
        '            End If							
        '							
        '							
        '            RsOpLeave.MoveNext							
        '        Loop							
        '    End If							
        '							
        '    mOPBal = mOPBal + mELEntitlement + mCPLEarn							
        '							
        '    If mLeaveType = CPLEARN Then							
        '        mLeaveType = CPLAVAIL							
        '    End If							

        If optShow(0).Checked = True Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(txtTo.Text)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ATTN_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND (FIRSTHALF = " & mLeaveType & " OR SECONDHALF = " & mLeaveType & " )" & vbCrLf & " ORDER BY ATTN_DATE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

            If RsLeave.EOF = False Then
                Do While Not RsLeave.EOF
                    If RsLeave.Fields("FIRSTHALF").Value = mLeaveType And RsLeave.Fields("SECONDHALF").Value = mLeaveType Then
                        sprdLeave.Row = mRow

                        sprdLeave.Col = ColCode
                        sprdLeave.Text = mCode

                        mLeavesTakenDate = VB6.Format(RsLeave.Fields("ATTN_DATE").Value, "DD/MM/YYYY")

                        sprdLeave.Col = ColTakenOn
                        sprdLeave.Text = mLeavesTakenDate

                        sprdLeave.Col = ColDays
                        sprdLeave.Text = "1.0"
                        mLeavesTaken = mLeavesTaken + 1
                    ElseIf RsLeave.Fields("FIRSTHALF").Value = mLeaveType Or RsLeave.Fields("SECONDHALF").Value = mLeaveType Then
                        sprdLeave.Row = mRow

                        sprdLeave.Col = ColCode
                        sprdLeave.Text = mCode

                        mLeavesTakenDate = VB6.Format(RsLeave.Fields("ATTN_DATE").Value, "DD/MM/YYYY")

                        sprdLeave.Col = ColTakenOn
                        sprdLeave.Text = CStr(IIf(sprdLeave.Text = "", "", sprdLeave.Text & Chr(13)) + mLeavesTakenDate)

                        sprdLeave.Col = ColDays
                        sprdLeave.Text = CStr(IIf(sprdLeave.Text = "", "", sprdLeave.Text & Chr(13)) + "0.5")
                        mLeavesTaken = mLeavesTaken + 0.5
                    End If

                    RsLeave.MoveNext()

                    If RsLeave.EOF = False Then
                        sprdLeave.MaxRows = sprdLeave.MaxRows + 1
                        mRow = mRow + 1
                    End If
                Loop

                sprdLeave.set_RowHeight(mRow, sprdLeave.get_MaxTextRowHeight(mRow))
            End If
        Else
            mLeavesTaken = GetLeavesAvail(mCode, (txtFrom.Text), (txtTo.Text), mLeaveType)
        End If

        mOPDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtFrom.Text)))
        If Year(CDate(mOPDate)) = Year(CDate(txtFrom.Text)) Then
            mOPBal = GetOpeningLeaves(mCode, mOPDate, mLeaveType, "Y", "Y", mOPDate)
            mETLeaves = GetOpeningLeaves(mCode, (txtTo.Text), mLeaveType, "N", "Y", "") - GetOpeningLeaves(mCode, mOPDate, mLeaveType, "N", "Y", "")
        Else
            mOPBal = GetOpeningLeaves(mCode, (txtFrom.Text), mLeaveType, "Y", "N", "")
            mETLeaves = GetOpeningLeaves(mCode, (txtTo.Text), mLeaveType, "N", "Y", "") ''							
        End If

        If mLeaveType = EARN And RsCompany.Fields("COMPANY_CODE").Value = 11 Then
            mELPaid = GetELPaidDays(mCode, (txtFrom.Text), txtTo.Text)
        End If

        If mLeaveType = EARN Then
            mELPaid = GetPaidEL(mCode, (txtFrom.Text), PubDBCn, (txtFrom.Text), (txtTo.Text))
        End If

        With sprdLeave
            .Row = xFirstRow
            sprdLeave.Col = ColCode
            sprdLeave.Text = mCode

            .Col = ColOpening
            .Text = VB6.Format(mOPBal, "0.0")

            .Col = ColEntitle
            .Text = VB6.Format(mETLeaves, "0.0")

            .Col = ColELPaid
            .Text = VB6.Format(mELPaid, "0.0")

            .Col = ColTotAvailed
            .Text = VB6.Format(mLeavesTaken, "0.0")

            .Col = ColLHead
            .Text = VB6.Format(mOPBal + mETLeaves - mELPaid - mLeavesTaken, "0.0")

        End With
    End Sub
    Private Sub FillDataInSprdold(ByRef mCode As String, ByRef mLeaveType As Integer, ByRef mRow As Integer)

        Dim RsOpLeave As ADODB.Recordset
        Dim RsLeave As ADODB.Recordset
        Dim mOPBal As Double

        Dim mETLeaves As Double
        Dim mLeavesTaken As Double

        Dim mLeavesTakenDate As String
        Dim mCasualDate As String
        Dim mEarnDate As String

        'Dim mMonth As Integer							
        'Dim mYear As Integer							

        Dim I As Integer
        Dim xFirstRow As Integer
        Dim mELEntitlement As Double
        Dim mCPLEarn As Double

        xFirstRow = mRow
        '    mMonth = Month(txtTo.Text)							
        '    mYear = Year(txtTo.Text)							

        If mLeaveType = EARN Then
            mELEntitlement = GETEntitleEarnLeave(PubDBCn, mCode, mLeaveType, (txtTo.Text))
        End If

        If mLeaveType = CPLEARN Then
            mCPLEarn = GETCPL(PubDBCn, mCode, (txtTo.Text))
        End If


        SqlStr = " SELECT LEAVECODE,SUM(OPENING) AS OPENING, SUM(TOTENTITLE) AS TOTENTITLE " & vbCrLf & " FROM PAY_OPLEAVE_TRN  WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR = " & Year(CDate(txtTo.Text)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "' AND LEAVECODE=" & mLeaveType & " GROUP BY LEAVECODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOpLeave.EOF = False Then
            Do While Not RsOpLeave.EOF
                If RsOpLeave.Fields("LeaveCode").Value = SICK Then
                    mOPBal = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value) + IIf(IsDBNull(RsOpLeave.Fields("TOTENTITLE").Value), 0, RsOpLeave.Fields("TOTENTITLE").Value)
                ElseIf RsOpLeave.Fields("LeaveCode").Value = CASUAL Then
                    mOPBal = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value) + IIf(IsDBNull(RsOpLeave.Fields("TOTENTITLE").Value), 0, RsOpLeave.Fields("TOTENTITLE").Value)
                ElseIf RsOpLeave.Fields("LeaveCode").Value = EARN Then
                    mOPBal = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                ElseIf RsOpLeave.Fields("LeaveCode").Value = CPLEARN Then
                    mOPBal = IIf(IsDBNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                End If


                RsOpLeave.MoveNext()
            Loop
        End If

        mOPBal = mOPBal + mELEntitlement + mCPLEarn

        If mLeaveType = CPLEARN Then
            mLeaveType = CPLAVAIL
        End If

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(txtTo.Text)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND (FIRSTHALF = " & mLeaveType & " OR SECONDHALF = " & mLeaveType & " )" & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While Not RsLeave.EOF
                If RsLeave.Fields("FIRSTHALF").Value = mLeaveType And RsLeave.Fields("SECONDHALF").Value = mLeaveType Then
                    sprdLeave.Row = mRow

                    sprdLeave.Col = ColCode
                    sprdLeave.Text = mCode

                    mLeavesTakenDate = VB6.Format(RsLeave.Fields("ATTN_DATE").Value, "DD/MM/YYYY")

                    sprdLeave.Col = ColTakenOn
                    sprdLeave.Text = mLeavesTakenDate

                    sprdLeave.Col = ColDays
                    sprdLeave.Text = "1.0"
                    mLeavesTaken = mLeavesTaken + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = mLeaveType Or RsLeave.Fields("SECONDHALF").Value = mLeaveType Then
                    sprdLeave.Row = mRow

                    sprdLeave.Col = ColCode
                    sprdLeave.Text = mCode

                    mLeavesTakenDate = VB6.Format(RsLeave.Fields("ATTN_DATE").Value, "DD/MM/YYYY")

                    sprdLeave.Col = ColTakenOn
                    sprdLeave.Text = CStr(IIf(sprdLeave.Text = "", "", sprdLeave.Text & Chr(13)) + mLeavesTakenDate)

                    sprdLeave.Col = ColDays
                    sprdLeave.Text = CStr(IIf(sprdLeave.Text = "", "", sprdLeave.Text & Chr(13)) + "0.5")
                    mLeavesTaken = mLeavesTaken + 0.5
                End If

                RsLeave.MoveNext()

                If RsLeave.EOF = False Then
                    sprdLeave.MaxRows = sprdLeave.MaxRows + 1
                    mRow = mRow + 1
                End If
            Loop

            sprdLeave.set_RowHeight(mRow, sprdLeave.get_MaxTextRowHeight(mRow))
        End If

        With sprdLeave
            .Row = xFirstRow
            sprdLeave.Col = ColCode
            sprdLeave.Text = mCode

            .Col = ColOpening
            .Text = VB6.Format(mOPBal, "0.0")

            .Col = ColEntitle
            .Text = VB6.Format(mETLeaves, "0.0")

            .Col = ColTotAvailed
            .Text = VB6.Format(mLeavesTaken, "0.0")

            .Col = ColLHead
            .Text = VB6.Format(mOPBal + mETLeaves - mLeavesTaken, "0.0")

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

