Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamDAReport
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim RsAttn As ADODB.Recordset = Nothing
    Dim cntRow As Integer
    Dim cntCol As Integer

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColEmpCode As Short = 1
    Private Const ColEmpName As Short = 2
    Private Const ColEmpDept As Short = 3
    Private Const ColDate As Short = 4
    Private Const ColDARate As Short = 5
    Private Const ColTotalDA As Short = 6


    Private Sub FillHeading(ByRef xDate As Date)

        Dim Tempdate As String


        With sprdAttn
            .MaxCols = ColTotalDA

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)





            .Col = 0
            .Text = "S. No."
            .set_ColWidth(0, 5)

            .Col = ColEmpCode
            .Text = "Emp Card No"
            .set_ColWidth(ColEmpCode, 10)

            .Col = ColEmpName
            .Text = "Employees' Name "
            .set_ColWidth(ColEmpName, 35)
            .ColsFrozen = 2

            .Col = ColEmpDept
            .Text = "Employees' Dept "
            .set_ColWidth(ColEmpDept, 25)

            .Col = ColDate
            .Text = "Date"
            .set_ColWidth(ColDate, 10)

            .Col = ColDARate
            .Text = "DA Rate (Rs)"
            .set_ColWidth(ColDARate, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .Col = ColTotalDA
            .Text = "Total DA (Rs.))"
            .set_ColWidth(ColTotalDA, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)
        End With
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAttn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColEmpCode, 10)

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColEmpName, 30)

            .Col = ColEmpDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColEmpDept, 25)

            .Col = ColDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDate, 10)

            For cntCol = ColDARate To ColTotalDA
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8)
            Next
        End With



        MainClass.ProtectCell(sprdAttn, 1, sprdAttn.MaxRows, ColEmpCode, ColTotalDA)
        MainClass.SetSpreadColor(sprdAttn, mRow)
        sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        '    sprdAttn.OperationMode = OperationModeSingle
        '    sprdAttn.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack
        sprdAttn.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
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

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
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
        Dim mSubTitle As String
        Dim mRptFileName As String
        Dim mHeading As String
        Dim mChequeNo As String
        Dim mChequeDate As String
        Dim mChequeAmount As String

        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        mTitle = "DA Report"
        mRptFileName = "DAReport.Rpt"

        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows - 1, 1, ColTotalDA, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        'mSubTitle = "For the period : " & lblYear.Text

        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")


        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

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
        FillHeading(CDate(txtDateFrom.Text))
        RefreshScreen()
        cmdPrint.Enabled = True
        CmdPreview.Enabled = True
        FillHeading(CDate(txtDateFrom.Text))
        FormatSprd(-1)
    End Sub


    Private Sub frmParamDAReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmParamDAReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'lblRunDate.Text = CStr(RunDate)

        txtDateFrom.Text = VB6.Format("01/" & VB6.Format(RunDate, "MM/YYYY"), "DD/MM/YYYY")        '' VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FillHeading(CDate(txtDateFrom.Text))
        FormatSprd(-1)
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        OptName.Checked = True
        FillDeptCombo()
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    'Private Sub UpDYear_DownClick()
    '    lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
    '    FillHeading(CDate(lblRunDate.Text))
    '    'RefreshScreen
    'End Sub

    'Private Sub UpDYear_UpClick()
    '    lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
    '    FillHeading(CDate(lblRunDate.Text))
    '    'RefreshScreen
    'End Sub

    Private Sub RefreshScreen()

        On Error GoTo ErrRefreshScreen
        Dim mDeptCode As String
        Dim RsOT As ADODB.Recordset
        Dim pGrossQty As String
        Dim mLastDay As Integer
        Dim mName As String
        Dim mEmpCode As String
        Dim mDeptName As String
        Dim mHour As Double
        Dim mMin As Double
        Dim mOverTime As Double
        Dim mTOTOverTime As Double
        Dim mOTRate As Double
        Dim mOTAmount As Double
        Dim mFoodingAmount As Double
        Dim mESIApp As Boolean
        Dim mBasicSalary As Double
        Dim mGrossSalary As Double
        Dim mESIRound As Double
        Dim mOTFactor As Double
        Dim mTOTOverTime1 As Double
        Dim mOverTimeAppType As String
        'Dim mOverTimePaid As Double

        MainClass.ClearGrid(sprdAttn, -1)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Sub
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Sub
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus

        mLastDay = MainClass.LastDay(Month(CDate(txtDateFrom.Text)), Year(CDate(txtDateFrom.Text)))

        SqlStr = " SELECT DISTINCT EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC, TO_CHAR(IH.CDATE,'DD/MM/YYYY') CDATE," & vbCrLf _
            & " EMP.DA_AMOUNT, EMP.DA_AMOUNT"


        SqlStr = SqlStr & vbCrLf _
            & " FROM TEMP_EPUNCH IH, PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.EMPID=EMP.EMP_CODE AND UPPER(IH.DAA)='YES'" & vbCrLf _
            & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE " & vbCrLf _
            & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE " & vbCrLf _
            & " AND EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '','DD-MON-YYYY')

        SqlStr = SqlStr & vbCrLf _
            & " AND TO_CHAR(IH.CDATE,'DDMMYYYY')>='" & VB6.Format(txtDateFrom.Text, "DDMMYYYY") & "'" & vbCrLf _
            & " AND TO_CHAR(IH.CDATE,'DDMMYYYY')<='" & VB6.Format(txtDateTo.Text, "DDMMYYYY") & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If


        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by DEPT.DEPT_DESC,EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by DEPT.DEPT_DESC,EMP.EMP_CODE"
        End If


        MainClass.AssignDataInSprd8(SqlStr, sprdAttn, StrConn, "Y")
        Call CalcTots()
        'MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, sprdAttn.MaxCols)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrRefreshScreen:
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC "
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

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub CalcTots()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mTotalDA As Double


        With sprdAttn
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColTotalDA
                mTotalDA = mTotalDA + Val(.Text)

            Next

            .MaxRows = .MaxRows + 1
            .Row = .MaxRows

            .Col = ColTotalDA
            .Text = CStr(mTotalDA)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = ColEmpCode
            .Col2 = mTotalDA
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .BlockMode = False

            .Col = ColEmpName
            .Row = .MaxRows
            .Text = "TOTAL :"
            '        .BackColor = &HC0FFFF

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

        End With

        Exit Sub
ErrPart:

    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        cmdPrint.Enabled = False
        CmdPreview.Enabled = False
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
