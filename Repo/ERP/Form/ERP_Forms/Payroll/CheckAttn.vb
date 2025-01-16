Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinExplorerBar
Imports Infragistics.Win.UltraWinGrid
Imports System.Data.OleDb
Friend Class frmCheckAttn
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
    Private Const ColDept As Short = 3
    Private Const ColDesg As Short = 4
    Private Const ColTotWDays As Short = 5
    Private Const ColHolidays As Short = 6
    Private Const ColCASUAL As Short = 7
    Private Const ColEARN As Short = 8
    Private Const ColSICK As Short = 9
    Private Const ColOTHERLEAVE As Short = 10
    Private Const ColRH As Short = 11
    Private Const ColTotLeaves As Short = 12
    Private Const ColWOPAY As Short = 13
    Private Const ColABSENT As Short = 14
    Private Const ColTotalABSENT As Short = 15
    Private Const ColABSENTPer As Short = 16
    Private Const ColABSENTPerWithLeave As Short = 17
    Private Const ColTotPresent As Short = 18
    Private Const ColOverTime As Short = 19
    Private Const ColLeaveDate As Short = 20

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub FillHeading()

        Dim cntCol As Integer

        MainClass.ClearGrid(sprdAttn)

        With sprdAttn
            .MaxCols = ColLeaveDate

            .Row = -1
            For cntCol = ColTotWDays To ColOverTime
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 1
            Next

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Col = ColSNO
            .Text = "S. No."
            .set_ColWidth(ColSNO, 5)

            .Col = ColCard
            .Text = "Emp Card No"
            .set_ColWidth(ColCard, 6)


            .Col = ColName
            .Text = "Employees' Name "
            .set_ColWidth(ColName, 24)
            .ColsFrozen = 3

            .Col = ColABSENT
            .Text = "Absent"
            .set_ColWidth(ColABSENT, 6)
            .ColHidden = False

            .Col = ColCASUAL
            .Text = "Casual"
            .set_ColWidth(ColCASUAL, 6)

            .Col = ColEARN
            .Text = "Earn"
            .set_ColWidth(ColEARN, 6)

            .Col = ColSICK
            .Text = "Sick"
            .set_ColWidth(ColSICK, 6)

            .Col = ColOTHERLEAVE
            .Text = "Other Leave"
            .set_ColWidth(ColOTHERLEAVE, 6)

            .Col = ColWOPAY
            .Text = "W/o Pay"
            .set_ColWidth(ColWOPAY, 6)


            .Col = ColTotalABSENT
            .Text = "Total ABSENT"
            .set_ColWidth(ColTotalABSENT, 6)

            .Col = ColABSENTPer
            .Text = "Absent %"
            .set_ColWidth(ColABSENTPer, 6)

            .Col = ColABSENTPerWithLeave
            .Text = "Absent % (Include Leave)"
            .set_ColWidth(ColABSENTPerWithLeave, 6)


            .Col = ColRH
            .Text = "RH"
            .set_ColWidth(ColRH, 6)

            .Col = ColTotLeaves
            .Text = "Total Leaves"
            .set_ColWidth(ColTotLeaves, 6)

            .Col = ColHolidays
            .Text = "Holidays"
            .set_ColWidth(ColHolidays, 6)

            .Col = ColTotWDays
            .Text = "Working Days"
            .set_ColWidth(ColTotWDays, 6)

            .Col = ColTotPresent
            .Text = "Total Present"
            .set_ColWidth(ColTotPresent, 6)

            .Col = ColOverTime
            .Text = "Incentive Hrs"
            .set_ColWidth(ColOverTime, 6)

            .Col = ColLeaveDate
            .Text = "Leave Date"
            .set_ColWidth(ColLeaveDate, 6)

            .Col = ColDept
            .Text = "Department"
            .set_ColWidth(ColDept, 12)

            .Col = ColDesg
            .Text = "Designation"
            .set_ColWidth(ColDesg, 12)

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, ColLeaveDate)
            MainClass.SetSpreadColor(sprdAttn, -1)
            sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
        End With
    End Sub
    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
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

    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String = ""
        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)


        mSubTitle = mSubTitle & "(From : " & VB6.Format(txtFrom.Text, "DD/MM/YYYY") & " To: " & VB6.Format(txtTo.Text, "DD/MM/YYYY") & ") "
        mTitle = "Attendance - Check List"

        If chkCategory.Checked = False Then
            mTitle = mTitle & " - " & cboCategory.Text
        End If
        Call ShowReport(SqlStr, "AttnCheckList.Rpt", Mode, mTitle, mSubTitle)

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

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        FillHeading()
        RefreshScreen()
        cmdPrint.Enabled = True
        CmdPreview.Enabled = True
        MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, ColLeaveDate)
    End Sub


    Private Sub frmCheckAttn_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmCheckAttn_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        FillHeading()
        optCardNo.Checked = True
        FillDeptCombo()

        txtFrom.Text = VB6.Format("01/" & VB6.Format(RunDate, "MM/YYYY"), "dd/mm/yyyy")
        txtTo.Text = VB6.Format(MainClass.LastDay(Month(RunDate), Year(RunDate)) & "/" & VB6.Format(RunDate, "MM/YYYY"), "dd/mm/yyyy")

        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False
        cboDept.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim mCode As String
        Dim mDOJ As String
        Dim mDOL As String
        'Dim mMonth As Integer
        'Dim mYear As Integer
        Dim LastDayofMon As String
        Dim mTotLeave As Double
        Dim cntRow As Integer
        Dim mJDays As Short
        Dim mThisMonAttn As Short
        Dim mLDays As Short
        Dim mDeptCode As String

        Dim mAbsent As Double
        Dim mCasual As Double
        Dim mEarn As Double
        Dim mSick As Double
        Dim mOtherLeave As Double
        Dim mWopay As Double
        Dim mRH As Double
        Dim mHoliday As Double
        Dim mLeaveDate As String
        Dim mDateDiff As Integer
        Dim mOTHours As Double
        Dim mABSENTPer As Double
        Dim mDeptName As String


        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtEmpCode.Text) = "" Then
                MsgInformation("Please Select Operator Code")
                txtEmpCode.Focus()
                Exit Sub
            End If

            txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Employee Code ")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If




        '    mMonth = Format(Month(lblRunDate.Caption), "00")
        '    mYear = Year(lblRunDate.Caption)
        ''DateDiff("d", txtFrom.Text, txtTo.Text) + 1
        mDateDiff = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtFrom.Text), CDate(txtTo.Text)) + 1
        LastDayofMon = VB6.Format(txtTo.Text) '' MainClass.LastDay(mMonth, Year(lblRunDate.Caption)) & "/" & Month(lblRunDate.Caption) & "/" & Year(lblRunDate.Caption)
        mDOJ = VB6.Format(txtTo.Text) '' MainClass.LastDay(mMonth, Year(lblRunDate.Caption)) & "/" & mMonth & "/" & Year(lblRunDate.Caption)
        mDOL = VB6.Format(txtFrom.Text) '' "01" & "/" & mMonth & "/" & Year(lblRunDate.Caption)

        SqlStr = " SELECT EMP_NAME, EMP_CODE, " & vbCrLf _
            & " EMP_DOJ,EMP_LEAVE_DATE, EMP_DEPT_CODE, GETEMPDESG(COMPANY_CODE,EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_STOP_SALARY='N'" & vbCrLf _
            & " AND EMP_DOJ<=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND (EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"

        'If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDeptCode = MasterNo
        '        SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
        '    End If
        'End If

        If cboDept.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboDept.CheckedRows
                If mDeptName <> "" Then
                    mDeptName += "," & "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                Else
                    mDeptName += "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                End If
            Next
        End If

        If mDeptName <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_DEPT_CODE IN (" & mDeptName & ")"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP_CODE"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    .MaxRows = cntRow

                    mTotLeave = 0
                    mAbsent = 0
                    mCasual = 0
                    mEarn = 0
                    mSick = 0
                    mOtherLeave = 0
                    mWopay = 0
                    mRH = 0
                    mHoliday = 0

                    .Row = cntRow
                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColLeaveDate
                    mLeaveDate = IIf(IsDBNull(RsAttn.Fields("EMP_LEAVE_DATE").Value), "", RsAttn.Fields("EMP_LEAVE_DATE").Value)

                    Call CalcLeaves(mCode, LastDayofMon, mAbsent, mCasual, mEarn, mSick, mOtherLeave, mWopay, mRH, mHoliday, mLeaveDate)

                    .Col = ColABSENT
                    .Text = CStr(Val(CStr(mAbsent)))

                    .Col = ColCASUAL
                    .Text = CStr(Val(CStr(mCasual)))

                    .Col = ColEARN
                    .Text = CStr(Val(CStr(mEarn)))

                    .Col = ColSICK
                    .Text = CStr(Val(CStr(mSick)))

                    .Col = ColOTHERLEAVE
                    .Text = CStr(Val(CStr(-mOtherLeave + mRH)))

                    .Col = ColWOPAY
                    .Text = CStr(Val(CStr(mWopay)))

                    .Col = ColRH
                    .Text = CStr(0) 'Val(mRH)

                    .Col = ColTotLeaves
                    mTotLeave = mCasual + mEarn + mSick ''+ mOtherLeave + mRH
                    .Text = CStr(mTotLeave)

                    '                mHoliday = GetMonthHolidays(lblRunDate.Caption, RsAttn!EMP_DOJ)

                    .Col = ColHolidays
                    .Text = CStr(mHoliday)

                    .Col = ColTotPresent
                    If mLeaveDate = "" Then
                        mJDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, IIf(CDate(RsAttn.Fields("EMP_DOJ").Value) < CDate(txtFrom.Text), txtFrom.Text, RsAttn.Fields("EMP_DOJ").Value), CDate(VB6.Format(txtTo.Text, "dd/mm/yyyy"))) + 1
                    Else
                        mJDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, IIf(CDate(RsAttn.Fields("EMP_DOJ").Value) < CDate(txtFrom.Text), txtFrom.Text, RsAttn.Fields("EMP_DOJ").Value), IIf(CDate(mLeaveDate) < CDate(txtTo.Text), mLeaveDate, txtTo.Text)) + 1
                    End If
                    mThisMonAttn = mJDays
                    '                If Not IsNull(RsAttn!EMP_LEAVE_DATE) Then
                    '                    mLDays = DateDiff("d", RsAttn!EMP_LEAVE_DATE, Format(LastDayofMon, "dd/mm/yyyy"))
                    '                End If
                    '                If Format(RsAttn!EMP_DOJ, "mm yyyy") = Format(RsAttn!EMP_LEAVE_DATE, "mm yyyy") Then
                    '                    mThisMonAttn = mJDays - mLDays + 1
                    '                ElseIf Format(RsAttn!EMP_DOJ, "mm yyyy") = Format(LastDayofMon, "mm yyyy") Then
                    '                    mThisMonAttn = mJDays + 1
                    '                ElseIf Format(RsAttn!EMP_LEAVE_DATE, "mm yyyy") = Format(LastDayofMon, "mm yyyy") Then
                    '                    mThisMonAttn = mDateDiff - mLDays
                    '                End If
                    '
                    '                If mDateDiff < mThisMonAttn Then
                    '                    mThisMonAttn = mDateDiff '' MainClass.LastDay(mMonth, mYear)
                    '                End If
                    .Text = CStr(mThisMonAttn - mAbsent - mWopay)

                    .Col = ColTotWDays
                    .Text = CStr(mThisMonAttn - (mTotLeave + mAbsent + mWopay + mHoliday))

                    .Col = ColOverTime
                    mOTHours = GetOverTime(mCode)
                    .Text = CStr(mOTHours)

                    .Col = ColLeaveDate
                    If mLeaveDate <> "" Then
                        If CDate(mLeaveDate) >= CDate(txtFrom.Text) And CDate(mLeaveDate) <= CDate(txtTo.Text) Then
                            .Text = mLeaveDate
                        End If
                    End If

                    .Col = ColTotalABSENT
                    .Text = CStr(mAbsent + mWopay)

                    mABSENTPer = CStr((mAbsent + mWopay) * 100 / mThisMonAttn)

                    .Col = ColABSENTPer
                    .Text = CStr(mABSENTPer)

                    mABSENTPer = CStr((mTotLeave + mAbsent + mWopay) * 100 / mThisMonAttn)

                    .Col = ColABSENTPerWithLeave
                    .Text = CStr(mABSENTPer)

                    .Col = ColDept
                    .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDBNull(RsAttn.Fields("DESG_DESC").Value), "", RsAttn.Fields("DESG_DESC").Value)

                    cntRow = cntRow + 1
                    RsAttn.MoveNext()
                Loop
            End With
        End If

        With sprdAttn
            ColTotal(sprdAttn, ColTotWDays, .MaxCols - 1)
            .Col = ColName
            .Row = .MaxRows
            .Text = "TOTAL :"
            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)  ''System.Drawing.ColorTranslator.FromOle(H8000000F) '' &H8000000B             ''&H80FF80  ''&H8000000F
            .BlockMode = False
        End With

        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub FillDeptCombo()


        Dim RsDept As ADODB.Recordset = Nothing
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)


        SqlStr = "Select DEPT_DESC, DEPT_CODE " & vbCrLf _
            & " FROM PAY_DEPT_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " Order by DEPT_DESC"

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboDept.DataSource = ds
        cboDept.DataMember = ""
        Dim c As UltraGridColumn = Me.cboDept.DisplayLayout.Bands(0).Columns.Add()
        With c
            .Key = "Selected"
            .Header.Caption = String.Empty
            .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
            .DataType = GetType(Boolean)
            .DataType = GetType(Boolean)
            .Header.VisiblePosition = 0
        End With
        cboDept.CheckedListSettings.CheckStateMember = "Selected"
        cboDept.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        ' Set up the control to use a custom list delimiter 
        cboDept.CheckedListSettings.ListSeparator = " , "
        ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        cboDept.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        cboDept.DisplayMember = "DEPT_DESC"
        cboDept.ValueMember = "DEPT_CODE"

        cboDept.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Dept Name"
        cboDept.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Dept Code"
        'cboDepartment.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
        'cboDepartment.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
        'cboDepartment.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"

        cboDept.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboDept.DisplayLayout.Bands(0).Columns(1).Width = 100
        'cboDepartment.DisplayLayout.Bands(0).Columns(2).Width = 350
        'cboDepartment.DisplayLayout.Bands(0).Columns(3).Width = 100
        'cboDepartment.DisplayLayout.Bands(0).Columns(4).Width = 100

        cboDept.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown



        oledbAdapter.Dispose()
        oledbCnn.Close()


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
        '

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function ChechJoinLeaveDate(ByRef mDays As String, ByRef mCode As String) As Boolean

        Dim SqlStr As String = ""
        Dim RsTempJL As ADODB.Recordset = Nothing

        SqlStr = " SELECT " & vbCrLf & " EMP_DOJ,EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempJL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTempJL.EOF = False Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(RsTempJL.Fields("EMP_DOJ").Value, "dd/mm/yyyy")), CDate(VB6.Format(mDays, "dd/mm/yyyy"))) > 0 Then
                ChechJoinLeaveDate = True
            Else
                MsgInformation("Employee Joining Date is Greater then Current Date.")
                ChechJoinLeaveDate = False
                Exit Function
            End If
            If IsDBNull(RsTempJL.Fields("EMP_LEAVE_DATE").Value) Then
                ChechJoinLeaveDate = True
            ElseIf DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(mDays, "dd/mm/yyyy")), CDate(VB6.Format(RsTempJL.Fields("EMP_LEAVE_DATE").Value, "dd/mm/yyyy"))) < 0 Then
                MsgInformation("Employee Leaving Date is Less then Current Date.")
                ChechJoinLeaveDate = False
                Exit Function
            Else
                ChechJoinLeaveDate = True
            End If
        End If
    End Function

    Private Function CalcLeaves(ByRef mCode As String, ByRef mMMYYYY As String, ByRef mAbsent As Double, ByRef mCasual As Double, ByRef mEarn As Double, ByRef mSick As Double, ByRef mCPLEarn As Double, ByRef mWopay As Double, ByRef mCPLAvail As Double, ByRef mHoliday As Double, ByRef mLeaveDate As String) As Boolean

        On Error GoTo ErrFillLeaves
        Dim RsLeaves As ADODB.Recordset = Nothing


        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" ''& vbCrLf |        & " AND TO_CHAR(ATTN_DATE,'MON-YYYY')=TO_CHAR('" & UCase(Format(mMMYYYY, "MMM-YYYY")) & "')"

        SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE >= TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE <= TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If mLeaveDate <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            Do While Not RsLeaves.EOF
                If RsLeaves.Fields("FIRSTHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = SICK Then
                    mSick = mSick + 0.5
                    '            ElseIf RsLeaves!FIRSTHALF = CPLEARN Then
                    '                mCPLEARN = mCPLEARN + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = WOPAY Then
                    mWopay = mWopay + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = CPLAVAIL Then
                    mCPLAvail = mCPLAvail + 0.5
                ElseIf RsLeaves.Fields("FIRSTHALF").Value = SUNDAY Or RsLeaves.Fields("FIRSTHALF").Value = HOLIDAY Then
                    mHoliday = mHoliday + 0.5
                End If

                If RsLeaves.Fields("SECONDHALF").Value = ABSENT Then
                    mAbsent = mAbsent + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = EARN Then
                    mEarn = mEarn + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
                    '            ElseIf RsLeaves!SECONDHALF = CPLEARN Then
                    '                mCPLEARN = mCPLEARN + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = WOPAY Then
                    mWopay = mWopay + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPLAvail = mCPLAvail + 0.5
                ElseIf RsLeaves.Fields("SECONDHALF").Value = SUNDAY Or RsLeaves.Fields("SECONDHALF").Value = HOLIDAY Then
                    mHoliday = mHoliday + 0.5
                End If

                mCPLEarn = IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) * 0.5
                RsLeaves.MoveNext()
            Loop
        End If

        CalcLeaves = True
        Exit Function
ErrFillLeaves:
        CalcLeaves = False
    End Function
    Private Function GetOverTime(ByRef mCode As String) As Double

        On Error GoTo ErrFillLeaves
        Dim RsOT As ADODB.Recordset = Nothing
        Dim mHour As Double
        Dim mMin As Double

        GetOverTime = 0

        SqlStr = " SELECT SUM(OT.OTHOUR+OT.PREV_OTHOUR) AS TOTOTHR,  SUM(OT.OTMIN+OT.PREV_OTMIN) AS TOTOTMI"

        SqlStr = SqlStr & vbCrLf _
            & " FROM PAY_OVERTIME_MST OT " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & mCode & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND OT.OT_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND OT.OT_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOT, ADODB.LockTypeEnum.adLockOptimistic)
        If RsOT.EOF = False Then
            mHour = IIf(IsDBNull(RsOT.Fields("TOTOTHR").Value), 0, RsOT.Fields("TOTOTHR").Value)
            mMin = IIf(IsDBNull(RsOT.Fields("TOTOTMI").Value), "", RsOT.Fields("TOTOTMI").Value)

            'mMin = mMin * 100 / 60

            Dim mTempMin As Double

            mTempMin = (mMin)

            mHour = mHour + Int(mTempMin / 60)
            mMin = (mTempMin Mod 60)

            mMin = mMin / 60 '' Int(mMin / 60) * 100
            'GetTOTOverTime = (mHour) + (mMin * 0.01)

            GetOverTime = (mHour) + mMin  '' (mMin * 0.01) '' CDbl(VB6.Format(GetTOTOverTime(mHour, mMin), "0.00"))
        End If

        Exit Function
ErrFillLeaves:

    End Function
    Private Sub frmCheckAttn_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
            '    ElseIf FYChk(txtFrom.Text) = False Then
            '        Cancel = True
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
            '    ElseIf FYChk(txtTo.Text) = False Then
            Cancel = True
        End If
        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub txtEmpCode_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

End Class
