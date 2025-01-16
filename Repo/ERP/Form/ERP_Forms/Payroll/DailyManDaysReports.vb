Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDailyManDaysReports
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColDesc As Short = 1
    Private Const ColDays1 As Short = 2
    Private Const ColDays2 As Short = 3
    Private Const ColDays3 As Short = 4
    Private Const ColDays4 As Short = 5
    Private Const ColDays As Short = 6
    Private Const ColOTHours As Short = 7

    Dim mCurrRow As Integer
    Dim mSearchKey As String

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FillHeading()

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCnt As Integer
        Dim mDivisionCode As Integer
        Dim mDeptCode As String
        Dim mDesgCode As String

        MainClass.ClearGrid(sprdMain)

        With sprdMain
            If optBoth.Checked = True Then
                .MaxCols = ColDesc
            Else
                .MaxCols = ColOTHours
            End If

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 4)

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColDesc
            If OptName.Checked = True Then
                .Text = "Department"
            ElseIf optCardNo.Checked = True Then
                If lblType.Text = "C" Then
                    .Text = "Contractor's Name"
                Else
                    .Text = "Category Desc"
                End If
            Else
                .Text = "Department Desc"
            End If

            If optBoth.Checked = True Then
                mCnt = 1
AgainFill:
                If lblType.Text = "C" Then
                    SqlStr = " SELECT DISTINCT CMST.CON_NAME AS REF_DESC"
                Else
                    SqlStr = " SELECT DISTINCT CMST.CATEGORY_DESC AS REF_DESC"
                End If

                If lblType.Text = "C" Then
                    SqlStr = SqlStr & vbCrLf & " FROM PAY_CONT_DALIY_ATTN_TRN TRN, PAY_CONT_EMPLOYEE_MST EMP, PAY_CONTRACTOR_MST CMST "
                Else
                    SqlStr = SqlStr & vbCrLf & " FROM PAY_DALIY_ATTN_TRN TRN, PAY_EMPLOYEE_MST EMP, PAY_CATEGORY_MST CMST"
                End If
                SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE"

                If lblType.Text = "C" Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND EMP.CONTRACTOR_CODE=CMST.CON_CODE"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CATG=CMST.CATEGORY_CODE"
                End If

                If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionCode = CInt(Trim(MasterNo))
                        SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & mDivisionCode & ""
                    End If
                End If

                If lblType.Text = "C" Then
                    If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboConName.SelectedIndex <> -1 Then
                        SqlStr = SqlStr & vbCrLf & "AND CMST.CON_NAME='" & MainClass.AllowSingleQuote(Trim(cboConName.Text)) & "' "
                    End If
                Else
                    If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboConName.SelectedIndex <> -1 Then
                        SqlStr = SqlStr & vbCrLf & "AND CMST.CATEGORY_DESC='" & MainClass.AllowSingleQuote(Trim(cboConName.Text)) & "' "
                    End If
                End If

                If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDeptCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
                    End If
                End If

                If chkDesgAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    If MainClass.ValidateWithMasterTable(cboDesg.Text, "DESG_DESC", "DESG_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDesgCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DESG_CODE='" & MainClass.AllowSingleQuote(Trim(mDesgCode)) & "' "
                    End If
                End If

                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(TRN.IN_TIME,'HH24:MI')<>'00:00'"
                SqlStr = SqlStr & vbCrLf & " AND TRN.ATTN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.ATTN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



                If lblType.Text = "C" Then
                    SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.CON_NAME "
                Else
                    SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.CATEGORY_DESC "
                End If


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then

                    Do While RsTemp.EOF = False
                        .Row = 0

                        .MaxCols = .MaxCols + 1
                        .Col = .MaxCols
                        .Text = RsTemp.Fields("REF_DESC").Value & IIf(mCnt = 1, " - SHIFT A", " - SHIFT B") & "(Present - More Than 3 Month)"

                        .MaxCols = .MaxCols + 1
                        .Col = .MaxCols
                        .Text = RsTemp.Fields("REF_DESC").Value & IIf(mCnt = 1, " - SHIFT A", " - SHIFT B") & "(Present - Between 2-3 Month)"

                        .MaxCols = .MaxCols + 1
                        .Col = .MaxCols
                        .Text = RsTemp.Fields("REF_DESC").Value & IIf(mCnt = 1, " - SHIFT A", " - SHIFT B") & "(Present - Between 1-2 Month)"

                        .MaxCols = .MaxCols + 1
                        .Col = .MaxCols
                        .Text = RsTemp.Fields("REF_DESC").Value & IIf(mCnt = 1, " - SHIFT A", " - SHIFT B") & "(Present - Within 1 Month)"

                        .MaxCols = .MaxCols + 1
                        .Col = .MaxCols
                        .Text = RsTemp.Fields("REF_DESC").Value & IIf(mCnt = 1, " - SHIFT A", " - SHIFT B") & "(Present)"


                        '
                        '                    .Row = -1
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .TypeFloatDecimalChar = Asc(".")
                        '                    .TypeFloatDecimalPlaces = 0
                        '                    .TypeFloatMax = "9999999.99"
                        '                    .TypeFloatMin = "-9999999.99"
                        '                    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                        '                    .ColWidth(.MaxCols) = 10
                        '
                        '
                        RsTemp.MoveNext()
                    Loop

                    mCnt = mCnt + 1
                    If mCnt = 2 Then GoTo AgainFill

                End If

                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "OVER ALL (Present - More Than 3 Month)"

                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "OVER ALL (Present - Between 2-3 Month)"

                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "OVER ALL (Present - Between 1-2 Month)"

                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "OVER ALL (Present - With in 1 Month)"

                .MaxCols = .MaxCols + 1
                .Col = .MaxCols
                .Text = "OVER ALL (Present)"

            Else
                .Col = ColDays1
                .Text = "Present (More Than 3 Month)"

                .Col = ColDays2
                .Text = "Present (Between 2-3 Month)"

                .Col = ColDays3
                .Text = "Present (Between 1-2 Month)"

                .Col = ColDays4
                .Text = "Present (With in 1 Month)"

                .Col = ColDays
                .Text = "Present"

                .Col = ColOTHours
                .Text = "OT Hours"
            End If
        End With

    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        If chkDivision.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
        End If
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboConName.Enabled = False
        Else
            cboConName.Enabled = True
        End If
    End Sub

    Private Sub chkDesgAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDesgAll.CheckStateChanged
        If chkDesgAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDesg.Enabled = False
        Else
            cboDesg.Enabled = True
        End If
    End Sub

    Private Sub chkDivision_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDivision.CheckStateChanged
        If chkDivision.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDivision.Enabled = False
        Else
            cboDivision.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForDeduction(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForDeduction(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String


        '    PubDBCn.Errors.Clear
        '
        '
        '    'Insert Data from Grid to PrintDummyData Table...
        '
        '
        '    If FillPrintDummyData(sprdMain, 0, sprdMain.MaxRows, ColCode, sprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1
        '
        '
        '    'Select Record for print...
        '
        '    SqlStr = ""
        '
        '    SqlStr = FetchRecordForReport(SqlStr)
        '
        '    mSubTitle = "For the Month : " & MonthName(Month(lblRunDate.Caption)) & ", " & Year(lblRunDate.Caption)
        '    mTitle = "Deduction List "
        '    If lblSalType.Caption = "O" Then
        '        mTitle = mTitle & "(Over Time)"
        '    ElseIf lblSalType.Caption = "E" Then
        '         mTitle = mTitle & "(Encashment)"
        '    ElseIf lblSalType.Caption = "C" Then
        '        mTitle = mTitle & "(CPL)"
        '    End If
        '
        '    Call ShowReport(SqlStr, "MonthlyVar.Rpt", Mode, mTitle, mSubTitle)

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
        Call ReportForDeduction(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click



        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If


        If chkDesgAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDesg.Text = "" Then
                MsgInformation("Please select the Desgnation Name.")
                cboDesg.Focus()
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

        FillHeading()
        FormatSprd(-1)

        If optBoth.Checked = True Then
            RefreshScreenTabular()
        Else
            RefreshScreen()
        End If
        FormatSprd(-1)
    End Sub
    Private Sub frmDailyManDaysReports_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        If FormActive = True Then Exit Sub
        Me.Text = "Daily Man Days Reports"

        If lblType.Text = "C" Then
            optCardNo.Text = "Contractor"
        Else
            optCardNo.Text = "Category"
        End If

        FillHeading()
        FormatSprd(-1)

        FillDeptCombo()
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboConName.Enabled = False

        FormActive = True
    End Sub

    Private Sub frmDailyManDaysReports_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        optCardNo.Checked = True

        cboDesg.Enabled = False
        chkDesgAll.CheckState = System.Windows.Forms.CheckState.Checked

        chkDivision.CheckState = System.Windows.Forms.CheckState.Checked
        cboDivision.Enabled = False

        FillHeading()
        FormatSprd(-1)



        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo ErrRefreshScreen

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mSqlStr As String
        Dim mDeptCode As String
        Dim mDivisionCode As Double
        Dim mDesgCode As String

        If OptName.Checked = True Then
            SqlStr = " SELECT DEPT.DEPT_DESC AS REF_DESC, "
        Else
            If lblType.Text = "C" Then
                SqlStr = " SELECT CMST.CON_NAME AS REF_DESC, "
            Else
                SqlStr = " SELECT CMST.CATEGORY_DESC AS REF_DESC, "
            End If
        End If

        '    SELECT Months_between(To_date(EMP_DOJ, 'YYYYMMDD'),
        '       To_date('20120101', 'YYYYMMDD'))
        '       num_months,
        '       ( To_date('20120325', 'YYYYMMDD') - To_date('20120101', 'YYYYMMDD') )
        '       diff_in_days
        'FROM   dual;
        '
        '

        SqlStr = SqlStr & vbCrLf & " COUNT(CASE WHEN MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) >3 THEN EMP.EMP_CODE END) AS CNT_MONTH3, " & vbCrLf & " COUNT(CASE WHEN MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) <=3 AND MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) >2 THEN EMP.EMP_CODE END) AS CNT_MONTH2, " & vbCrLf & " COUNT(CASE WHEN MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) <=2 AND MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) >1 THEN EMP.EMP_CODE END) AS CNT_MONTH1, " & vbCrLf & " COUNT(CASE WHEN MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) <=1 THEN EMP.EMP_CODE END) AS CNT_MONTH0, "

        SqlStr = SqlStr & vbCrLf & " COUNT(1) AS CNT_PRESENT, " & vbCrLf & " SUM(OT_HOURS) AS OT_HOURS"

        If lblType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " FROM PAY_CONT_DALIY_ATTN_TRN TRN, PAY_CONT_EMPLOYEE_MST EMP, PAY_CONTRACTOR_MST CMST, PAY_DEPT_MST DEPT "
        Else
            SqlStr = SqlStr & vbCrLf & " FROM PAY_DALIY_ATTN_TRN TRN, PAY_EMPLOYEE_MST EMP, PAY_CATEGORY_MST CMST, PAY_DEPT_MST DEPT"
        End If
        SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE"

        If lblType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND EMP.CONTRACTOR_CODE=CMST.CON_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CATG=CMST.CATEGORY_CODE"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE"

        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
                SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & mDivisionCode & ""
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.ATTN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.ATTN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkDesgAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDesg.Text, "DESG_DESC", "DESG_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDesgCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DESG_CODE='" & MainClass.AllowSingleQuote(Trim(mDesgCode)) & "' "
            End If
        End If

        If lblType.Text = "C" Then
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboConName.SelectedIndex <> -1 Then
                SqlStr = SqlStr & vbCrLf & "AND CMST.CON_NAME='" & MainClass.AllowSingleQuote(Trim(cboConName.Text)) & "' "
            End If
        Else
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboConName.SelectedIndex <> -1 Then
                SqlStr = SqlStr & vbCrLf & "AND CMST.CATEGORY_DESC='" & MainClass.AllowSingleQuote(Trim(cboConName.Text)) & "' "
            End If
        End If



        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(TRN.IN_TIME,'HH24:MI') <>'00:00'" ''TOT_HOURS>0"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY DEPT.DEPT_DESC "
        Else
            If lblType.Text = "C" Then
                SqlStr = SqlStr & vbCrLf & " GROUP BY CMST.CON_NAME "
            Else
                SqlStr = SqlStr & vbCrLf & " GROUP BY CMST.CATEGORY_DESC "
            End If
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
    End Sub
    Private Sub RefreshScreenTabular()

        On Error GoTo ErrRefreshScreen

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mSqlStr As String
        Dim mDeptCode As String
        Dim mDivisionCode As Double
        Dim i As String
        Dim mCategoryDesc As String
        Dim mManPowerCount As Double
        Dim mShiftType As String
        Dim mDeptName As String
        Dim mDivisionName As String
        Dim mDesgCode As String
        Dim mCount1 As Double
        Dim mCount2 As Double
        Dim mCount3 As Double
        Dim mCount4 As Double
        Dim mCount As Double

        Dim mTotCount1 As Double
        Dim mTotCount2 As Double
        Dim mTotCount3 As Double
        Dim mTotCount4 As Double
        Dim mTotCount As Double

        If lblType.Text = "C" Then
            SqlStr = " SELECT DISTINCT DEPT.DEPT_DESC, EMP_DEPT_CODE, EMP.DIV_CODE, '' AS EMP_DESG_CODE"
        Else
            SqlStr = " SELECT DISTINCT DEPT.DEPT_DESC, EMP_DEPT_CODE, EMP.DIV_CODE, '' AS EMP_DESG_CODE "
        End If

        If lblType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " FROM PAY_CONT_DALIY_ATTN_TRN TRN, PAY_CONT_EMPLOYEE_MST EMP, PAY_CONTRACTOR_MST CMST, PAY_DEPT_MST DEPT "
        Else
            SqlStr = SqlStr & vbCrLf & " FROM PAY_DALIY_ATTN_TRN TRN, PAY_EMPLOYEE_MST EMP, PAY_CATEGORY_MST CMST, PAY_DEPT_MST DEPT"
        End If
        SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE"

        If lblType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND EMP.CONTRACTOR_CODE=CMST.CON_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CATG=CMST.CATEGORY_CODE"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE"


        If chkDivision.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
                SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & mDivisionCode & ""
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.ATTN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.ATTN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkDesgAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDesg.Text, "DESG_DESC", "DESG_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDesgCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DESG_CODE='" & MainClass.AllowSingleQuote(Trim(mDesgCode)) & "' "
            End If
        End If

        If lblType.Text = "C" Then
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboConName.SelectedIndex <> -1 Then
                SqlStr = SqlStr & vbCrLf & "AND CMST.CON_NAME='" & MainClass.AllowSingleQuote(Trim(cboConName.Text)) & "' "
            End If
        Else
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboConName.SelectedIndex <> -1 Then
                SqlStr = SqlStr & vbCrLf & "AND CMST.CATEGORY_DESC='" & MainClass.AllowSingleQuote(Trim(cboConName.Text)) & "' "
            End If
        End If

        '    Sqlstr = Sqlstr & vbCrLf & " AND TOT_HOURS>0"

        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(TRN.IN_TIME,'HH24:MI')<>'00:00'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP.EMP_DEPT_CODE"



        '    MainClass.AssignDataInSprd Sqlstr, AData1, StrConn, "Y"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = CStr(1)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mDeptCode = IIf(IsDBNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)
                mDeptName = IIf(IsDBNull(RsTemp.Fields("DEPT_DESC").Value), "", RsTemp.Fields("DEPT_DESC").Value)
                '            mCategoryDesc = Trim(IIf(IsNull(RsTemp!CAT_DESC), "", RsTemp!CAT_DESC))
                mDivisionCode = IIf(IsDBNull(RsTemp.Fields("DIV_CODE").Value), "", RsTemp.Fields("DIV_CODE").Value)
                mDesgCode = IIf(IsDBNull(RsTemp.Fields("EMP_DESG_CODE").Value), "", RsTemp.Fields("EMP_DESG_CODE").Value)

                SprdMain.Row = CInt(i)
                SprdMain.Col = ColDesc

                If chkDivision.CheckState = System.Windows.Forms.CheckState.Checked Then
                    If MainClass.ValidateWithMasterTable(Trim(CStr(mDivisionCode)), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionName = Trim(MasterNo)
                    End If
                    SprdMain.Text = Trim(mDeptName) & " - " & mDivisionName
                Else
                    SprdMain.Text = Trim(mDeptName)
                End If

                mTotCount1 = 0
                mTotCount2 = 0
                mTotCount3 = 0
                mTotCount4 = 0
                mTotCount = 0


                For cntCol = ColDesc + 1 To SprdMain.MaxCols Step 5
                    SprdMain.Row = 0
                    SprdMain.Col = cntCol

                    mCategoryDesc = Trim(SprdMain.Text)

                    If InStr(1, mCategoryDesc, "OVER ALL") > 0 Then
                        SprdMain.Row = CInt(i)
                        SprdMain.Col = cntCol
                        SprdMain.Text = CStr(mTotCount1)

                        SprdMain.Col = cntCol + 1
                        SprdMain.Text = CStr(mTotCount2)

                        SprdMain.Col = cntCol + 2
                        SprdMain.Text = CStr(mTotCount3)

                        SprdMain.Col = cntCol + 3
                        SprdMain.Text = CStr(mTotCount4)

                        SprdMain.Col = cntCol + 4
                        SprdMain.Text = CStr(mTotCount)

                    Else
                        If InStr(1, mCategoryDesc, " - SHIFT A") > 0 Then
                            mShiftType = "A"
                        ElseIf InStr(1, mCategoryDesc, " - SHIFT B") > 0 Then
                            mShiftType = "B"
                        End If

                        mCategoryDesc = Trim(Mid(mCategoryDesc, 1, InStr(1, mCategoryDesc, " - SHIFT")))

                        If GetShiftWiseManPower(mDeptCode, mCategoryDesc, mShiftType, mDivisionCode, mDesgCode, mCount1, mCount2, mCount3, mCount4, mCount) = False Then GoTo ErrRefreshScreen

                        SprdMain.Row = CInt(i)

                        '                SprdMain.Col = ColDesc
                        '
                        ''                If MainClass.ValidateWithMasterTable(mDeptDesc, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        ''                    mDeptName = MasterNo
                        ''                End If
                        '
                        '                If chkDivision.Value = vbChecked Then
                        '                    If MainClass.ValidateWithMasterTable(Trim(mDivisionCode), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        '                        mDivisionName = Trim(MasterNo)
                        '                    End If
                        '                    SprdMain.Text = Trim(mDeptName) & " - " & mDivisionName
                        '                Else
                        '                    SprdMain.Text = Trim(mDeptName)
                        '                End If



                        SprdMain.Col = cntCol
                        SprdMain.Text = CStr(mCount1)

                        SprdMain.Col = cntCol + 1
                        SprdMain.Text = CStr(mCount2)

                        SprdMain.Col = cntCol + 2
                        SprdMain.Text = CStr(mCount3)

                        SprdMain.Col = cntCol + 3
                        SprdMain.Text = CStr(mCount4)

                        SprdMain.Col = cntCol + 4
                        SprdMain.Text = CStr(mCount)

                        mTotCount1 = mTotCount1 + mCount1
                        mTotCount2 = mTotCount2 + mCount2
                        mTotCount3 = mTotCount3 + mCount3
                        mTotCount4 = mTotCount4 + mCount4
                        mTotCount = mTotCount + mCount

                    End If

                Next

                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    i = CStr(CDbl(i) + 1)
                    SprdMain.MaxRows = CInt(i)
                End If
            Loop
        End If
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
    End Sub


    Private Function GetShiftWiseManPower(ByRef mDeptCode As String, ByRef mCategory As String, ByRef ShiftType As String, ByRef mDivisionCode As Double, ByRef mDesgCode As String, ByRef mCount1 As Double, ByRef mCount2 As Double, ByRef mCount3 As Double, ByRef mCount4 As Double, ByRef mCount As Double) As Boolean

        On Error GoTo ErrRefreshScreen

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSqlStr As String


        GetShiftWiseManPower = False
        mCount1 = 0
        mCount2 = 0
        mCount3 = 0
        mCount4 = 0
        mCount = 0

        SqlStr = " SELECT " & vbCrLf & " COUNT(CASE WHEN MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) >3 THEN EMP.EMP_CODE END) AS CNT_MONTH3, " & vbCrLf & " COUNT(CASE WHEN MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) <=3 AND MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) >2 THEN EMP.EMP_CODE END) AS CNT_MONTH2, " & vbCrLf & " COUNT(CASE WHEN MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) <=2 AND MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) >1 THEN EMP.EMP_CODE END) AS CNT_MONTH1, " & vbCrLf & " COUNT(CASE WHEN MONTHS_BETWEEN(TO_DATE(TRN.ATTN_DATE),TO_DATE(EMP.EMP_DOJ)) <=1 THEN EMP.EMP_CODE END) AS CNT_MONTH0, " & vbCrLf & " COUNT(1) AS  CNT_MONTH"



        If lblType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " FROM PAY_CONT_DALIY_ATTN_TRN TRN, PAY_CONT_EMPLOYEE_MST EMP, PAY_CONTRACTOR_MST CMST "
        Else
            SqlStr = SqlStr & vbCrLf & " FROM PAY_DALIY_ATTN_TRN TRN, PAY_EMPLOYEE_MST EMP, PAY_CATEGORY_MST CMST"
        End If
        SqlStr = SqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE"

        If lblType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND EMP.CONTRACTOR_CODE=CMST.CON_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CATG=CMST.CATEGORY_CODE"
        End If

        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & mDeptCode & "'"

        If mDesgCode <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DESG_CODE='" & mDesgCode & "'"
        End If

        If lblType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.CON_NAME='" & mCategory & "'"
        Else
            SqlStr = SqlStr & vbCrLf & "AND CMST.CATEGORY_DESC='" & mCategory & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND EMP.DIV_CODE=" & mDivisionCode & ""


        SqlStr = SqlStr & vbCrLf & " AND TRN.ATTN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.ATTN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If ShiftType = "A" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(TRN.IN_TIME,'HH24:MI')<='10:00' AND TO_CHAR(TRN.IN_TIME,'HH24:MI')<>'00:00'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(TRN.IN_TIME,'HH24:MI')>'10:00'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCount1 = IIf(IsDbNull(RsTemp.Fields("CNT_MONTH3").Value), 0, RsTemp.Fields("CNT_MONTH3").Value)
            mCount2 = IIf(IsDbNull(RsTemp.Fields("CNT_MONTH2").Value), 0, RsTemp.Fields("CNT_MONTH2").Value)
            mCount3 = IIf(IsDbNull(RsTemp.Fields("CNT_MONTH1").Value), 0, RsTemp.Fields("CNT_MONTH1").Value)
            mCount4 = IIf(IsDbNull(RsTemp.Fields("CNT_MONTH0").Value), 0, RsTemp.Fields("CNT_MONTH0").Value)
            mCount = IIf(IsDbNull(RsTemp.Fields("CNT_MONTH").Value), 0, RsTemp.Fields("CNT_MONTH").Value)
        End If

        GetShiftWiseManPower = True
        Exit Function

ErrRefreshScreen:
        GetShiftWiseManPower = False
        MsgInformation(Err.Description)
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDept.EOF = False Then
            Do While RsDept.EOF = False
                cboDivision.Items.Add(RsDept.Fields("DIV_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboDept.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        If lblType.Text = "C" Then
            SqlStr = "Select CON_NAME FROM PAY_CONTRACTOR_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' Order by CON_NAME"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

            cboConName.Items.Clear()
            If RsDept.EOF = False Then
                Do While Not RsDept.EOF
                    cboConName.Items.Add(RsDept.Fields("CON_NAME").Value)
                    RsDept.MoveNext()
                Loop
            End If
            cboConName.SelectedIndex = 0
        Else
            SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

            cboConName.Items.Clear()
            If RsDept.EOF = False Then
                Do While Not RsDept.EOF
                    cboConName.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                    RsDept.MoveNext()
                Loop
            End If
            cboConName.SelectedIndex = 0

        End If


        cboDesg.Items.Clear()

        SqlStr = "Select DESG_DESC FROM PAY_DESG_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DESG_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDept.EOF = False Then
            Do While RsDept.EOF = False
                cboDesg.Items.Add(RsDept.Fields("DESG_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        cboDesg.SelectedIndex = 0

        Exit Sub

ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function FillDataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef mPaymentType As String) As Boolean
        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        FillDataIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillDataIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        Dim cntCol As Integer

        On Error GoTo ERR1

        With sprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .set_ColWidth(ColSNO, 4)

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 25)

            If optBoth.Checked = True Then
                For cntCol = ColDesc + 1 To .MaxCols
                    .Col = cntCol
                    .CellType = SS_CELL_TYPE_FLOAT
                    .TypeFloatDecimalChar = Asc(".")
                    .TypeFloatDecimalPlaces = 0
                    .TypeFloatMax = CDbl("9999999.99")
                    .TypeFloatMin = CDbl("-9999999.99")
                    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                    .set_ColWidth(cntCol, 12)
                Next
            Else

                For cntCol = ColDays1 To ColDays
                    .Col = cntCol
                    .CellType = SS_CELL_TYPE_FLOAT
                    .TypeFloatDecimalChar = Asc(".")
                    .TypeFloatDecimalPlaces = 1
                    .TypeFloatMax = CDbl("9999999.99")
                    .TypeFloatMin = CDbl("-9999999.99")
                    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                    .set_ColWidth(cntCol, 12)
                Next

                .Col = ColOTHours
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(ColOTHours, 7)
            End If


            MainClass.SetSpreadColor(sprdMain, -1)
            MainClass.ProtectCell(sprdMain, 1, .MaxRows, 1, .MaxCols)
            sprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''OperationModeSingle
            sprdMain.DAutoCellTypes = True
            sprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            sprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Function SalProcess(ByRef mYM As Integer) As Boolean

        On Error GoTo ErrSalProcess
        Dim RsMain As ADODB.Recordset
        SalProcess = True
        SqlStr = " SELECT EMP_CODE FROM PAY_CONT_SAL_TRN WHERE " & vbCrLf & " TO_CHAR(SAL_DATE,'YYYYMM') > " & mYM & "" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISARREAR='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockOptimistic)

        If RsMain.EOF = False Then
            SalProcess = False
        End If
        Exit Function
ErrSalProcess:
        SalProcess = False
    End Function

    Private Sub frmDailyManDaysReports_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(sprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub optBoth_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBoth.CheckedChanged
        If eventSender.Checked Then
            FillHeading()
            FormatSprd(-1)
        End If
    End Sub


    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            FillHeading()
            FormatSprd(-1)
        End If
    End Sub


    Private Sub OptName_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptName.CheckedChanged
        If eventSender.Checked Then
            FillHeading()
            FormatSprd(-1)
        End If
    End Sub
End Class
