Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmOTAttnChecklist
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
    Private Const ColEmpGrossSal As Short = 4
    Private Const ColOT_R As Short = 5
    Private Const ColOT_R_Amt As Short = 6
    Private Const ColOT_E As Short = 7
    Private Const ColOT_E_Amt As Short = 8
    Private Const ColOT3 As Short = 9
    Private Const ColPrevMonth As Short = 10
    Private Const ColTotalOT As Short = 11
    Private Const ColOTRate As Short = 12
    Private Const ColTotalAmount As Short = 13
    Private Const ColFoodingAmount As Short = 14
    Private Const ColTotalAmount_Fooding As Short = 15

    'Private Const ConWorkHour As Short = 8    ''WorkHours

    Private Sub FillHeading(ByRef xDate As Date)

        Dim Tempdate As String

        'Dim NewDate As Date
        'Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        'NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        'lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        With sprdAttn
            .MaxCols = ColTotalAmount_Fooding

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

            .Col = ColEmpGrossSal
            .Text = "Gross Salary (Rs)"
            .set_ColWidth(ColEmpGrossSal, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .Col = ColOT_R
            .Text = "Over Time (Regular)"
            .set_ColWidth(ColOT_R, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .Col = ColOT_R_Amt
            .Text = "Over Time Amount (Regular)"
            .set_ColWidth(ColOT_R_Amt, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .Col = ColOT_E
            .Text = "Over Time (Extra)"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColOT_E, 8)
            .ColHidden = False

            .Col = ColOT_E_Amt
            .Text = "Over Time Amount (Extra)"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColOT_E_Amt, 8)
            .ColHidden = False

            .Col = ColOT3
            .Text = "Compulsory Duty"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColOT3, 8)
            .ColHidden = True

            .Col = ColPrevMonth
            .Text = "Previous Month Bal."
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColPrevMonth, 8)

            .Col = ColTotalOT
            .Text = "Total Over Time (Hours)"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColTotalOT, 8)

            .Col = ColOTRate
            .Text = "OT Rate (Rs.)"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColOTRate, 8)

            .Col = ColTotalAmount
            .Text = "Total OT Amount (Rs.)"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColTotalAmount, 10)

            .Col = ColFoodingAmount
            .Text = "Total Fooding Amount (Rs.)"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColFoodingAmount, 10)
            .ColHidden = False

            .Col = ColTotalAmount_Fooding
            .Text = "Total OT Include Fooding Amount (Rs.)"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColTotalAmount_Fooding, 10)
            .ColHidden = False

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

            For cntCol = ColEmpGrossSal To ColTotalAmount_Fooding
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8)
            Next
        End With

        MainClass.ProtectCell(sprdAttn, 1, sprdAttn.MaxRows, ColEmpCode, ColTotalAmount_Fooding)
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

        mTitle = "Over Time Summary"
        mRptFileName = "OTCheckList.Rpt"

        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows - 1, 1, ColTotalAmount_Fooding, PubDBCn) = False Then GoTo ERR1

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


    Private Sub frmOTAttnChecklist_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmOTAttnChecklist_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        pGrossQty = "MAX(GETBasicSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + GETADD_DEDSalaryFROMMST(EMP.COMPANY_CODE,EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"
        'pGrossQty = pGrossQty & "  * 2 / (" & mLastDay & " * 8)"

        SqlStr = " SELECT OT.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC,"

        '            & " SUM(CASE WHEN OT.OTHOUR<=2 THEN OT.OTHOUR ELSE 2 END) AS OTHR,  SUM(CASE WHEN OT.OTHOUR<=2 THEN OT.OTMIN ELSE 0 END) AS OTMI," & vbCrLf _

        SqlStr = SqlStr & vbCrLf _
            & " SUM(OT.OTHOUR) AS OTHR,  SUM(OT.OTMIN) AS OTMI," & vbCrLf _
            & " 0 AS OTABHR,  0 AS OTABMI," & vbCrLf _
            & " SUM(OT.PREV_OTHOUR) AS OTPRHR,  SUM(OT.PREV_OTMIN) AS OTPRMI," & vbCrLf _
            & " SUM(OT.OTHOUR+OT.PREV_OTHOUR) AS TOTOTHR,  SUM(OT.OTMIN+OT.PREV_OTMIN) AS TOTOTMI," & vbCrLf _
            & " SUM(CASE WHEN SM.MAJOR_SHIFT<>'C' AND OT.OTHOUR>=4 AND TO_CHAR(AT.OUT_TIME,'HH24:MI')>='00:55' AND TO_CHAR(AT.OUT_TIME,'DDMMYYYY')<>TO_CHAR(AT.IN_TIME,'DDMMYYYY') THEN 75 ELSE 0 END) AS FOOD_AMT"

        ''OT.OTHOUR<2 THEN 0 WHEN SM.MAJOR_SHIFT<>'C' AND OT.OTHOUR>=2 AND OT.OTHOUR<4 THEN 0 WHEN 

        SqlStr = SqlStr & vbCrLf _
            & " FROM PAY_OVERTIME_MST OT, PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT, PAY_DALIY_ATTN_TRN AT, PAY_SHIFT_TRN ST, PAY_SHIFT_MST SM" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " OT.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf _
            & " AND OT.EMP_CODE=EMP.EMP_CODE " & vbCrLf _
            & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE " & vbCrLf _
            & " AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE " & vbCrLf _
            & " AND OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " AND OT.COMPANY_CODE=AT.COMPANY_CODE(+) AND OT.EMP_CODE=AT.EMP_CODE(+) " & vbCrLf _
            & " AND OT.OT_DATE=AT.ATTN_DATE(+) " & vbCrLf _
            & " AND ST.COMPANY_CODE=SM.COMPANY_CODE(+) " & vbCrLf _
            & " AND ST.SHIFT_CODE=SM.SHIFT_CODE(+) " & vbCrLf _
            & " AND EMP.COMPANY_CODE=ST.COMPANY_CODE(+) " & vbCrLf _
            & " AND EMP.EMP_CODE=ST.EMP_CODE(+) AND OT.OT_DATE=ST.SHIFT_DATE(+) "


        SqlStr = SqlStr & vbCrLf _
            & " AND OT.OT_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND OT.OT_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkOnlySunday.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(OT.OT_DATE,'DY')='SUN'"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & "Group by OT.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order BY EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by OT.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by DEPT.DEPT_DESC"
        End If


        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOT)

        cntRow = 1
        If RsOT.EOF = False Then
            Do While RsOT.EOF = False
                mName = IIf(IsDBNull(RsOT.Fields("EMP_NAME").Value), "", RsOT.Fields("EMP_NAME").Value)
                mEmpCode = IIf(IsDBNull(RsOT.Fields("EMP_CODE").Value), "", RsOT.Fields("EMP_CODE").Value)
                mDeptName = IIf(IsDBNull(RsOT.Fields("DEPT_DESC").Value), "", RsOT.Fields("DEPT_DESC").Value)
                mHour = IIf(IsDBNull(RsOT.Fields("TOTOTHR").Value), 0, RsOT.Fields("TOTOTHR").Value)
                mMin = IIf(IsDBNull(RsOT.Fields("TOTOTMI").Value), "", RsOT.Fields("TOTOTMI").Value)

                mTOTOverTime = CDbl(VB6.Format(GetTOTOverTime(mHour, mMin), "0.00"))

                'If optShow(0).Checked = True Then
                '    mOTRate = 1
                '    mOTAmount = mTOTOverTime * CDbl(VB6.Format(mOTRate, "0.00"))
                'Else
                mOTRate = CDbl(VB6.Format(GetOTRate(mEmpCode, (txtDateFrom.Text), mESIApp, mBasicSalary, mESIRound, False, "", mGrossSalary), "0.00"))
                mOTAmount = mTOTOverTime * CDbl(VB6.Format(mOTRate, "0.00"))

                mOTFactor = 0
                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_OT_RATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOTFactor = MasterNo
                End If

                mOTAmount = mOTAmount * IIf(IsDBNull(mOTFactor) Or Val(CStr(mOTFactor)) = 0, 1, Val(CStr(mOTFactor)))

                'mTOTOverTime = mOTAmount
                'End If

                sprdAttn.Row = cntRow
                sprdAttn.Col = ColEmpName
                sprdAttn.Text = mName

                sprdAttn.Col = ColEmpCode
                sprdAttn.Text = mEmpCode

                sprdAttn.Col = ColEmpDept
                sprdAttn.Text = mDeptName

                sprdAttn.Col = ColEmpGrossSal
                sprdAttn.Text = mGrossSalary


                mHour = IIf(IsDBNull(RsOT.Fields("OTHR").Value), 0, RsOT.Fields("OTHR").Value)
                mMin = IIf(IsDBNull(RsOT.Fields("OTMI").Value), "", RsOT.Fields("OTMI").Value)
                mOverTime = CDbl(VB6.Format(GetTOTOverTime(mHour, mMin), "0.00"))

                sprdAttn.Col = ColOT_R
                sprdAttn.Text = CStr(mOverTime)

                sprdAttn.Col = ColOT_R_Amt
                sprdAttn.Text = CStr(mOverTime * CDbl(VB6.Format(mOTRate, "0.00")))

                mHour = IIf(IsDBNull(RsOT.Fields("OTABHR").Value), 0, RsOT.Fields("OTABHR").Value)
                mMin = IIf(IsDBNull(RsOT.Fields("OTABMI").Value), "", RsOT.Fields("OTABMI").Value)
                mOverTime = CDbl(VB6.Format(GetTOTOverTime(mHour, mMin), "0.00"))

                sprdAttn.Col = ColOT_E
                sprdAttn.Text = CStr(mOverTime)

                sprdAttn.Col = ColOT_E_Amt
                sprdAttn.Text = CStr(mOverTime * CDbl(VB6.Format(mOTRate, "0.00")))

                'mHour = IIf(IsDbNull(RsOT.Fields("OTCMHR").Value), 0, RsOT.Fields("OTCMHR").Value)
                'mMin = IIf(IsDbNull(RsOT.Fields("OTCMMI").Value), "", RsOT.Fields("OTCMMI").Value)
                'mOverTime = mOTRate * mOTFactor * CDbl(VB6.Format(GetTOTOverTime(mHour, mMin), "0.00"))
                'mTOTOverTime1 = mTOTOverTime1 + mOverTime
                'sprdAttn.Col = ColOT3
                'sprdAttn.Text = CStr(mOverTime)

                mHour = IIf(IsDBNull(RsOT.Fields("OTPRHR").Value), 0, RsOT.Fields("OTPRHR").Value)
                mMin = IIf(IsDBNull(RsOT.Fields("OTPRMI").Value), "", RsOT.Fields("OTPRMI").Value)
                mOverTime = CDbl(VB6.Format(GetTOTOverTime(mHour, mMin), "0.00"))
                'mTOTOverTime1 = mTOTOverTime1 + mOverTime
                sprdAttn.Col = ColPrevMonth
                sprdAttn.Text = CStr(mOverTime)

                sprdAttn.Col = ColTotalOT
                sprdAttn.Text = CStr(mTOTOverTime)

                sprdAttn.Col = ColOTRate
                sprdAttn.Text = CStr(mOTRate) ''mOTRate * mOTFactor *

                sprdAttn.Col = ColTotalAmount
                sprdAttn.Text = CStr(mOTAmount)

                sprdAttn.Col = ColFoodingAmount
                sprdAttn.Text = IIf(IsDBNull(RsOT.Fields("FOOD_AMT").Value), "", RsOT.Fields("FOOD_AMT").Value) ''
                mFoodingAmount = IIf(IsDBNull(RsOT.Fields("FOOD_AMT").Value), 0, RsOT.Fields("FOOD_AMT").Value) ''

                sprdAttn.Col = ColTotalAmount_Fooding
                sprdAttn.Text = CStr(mOTAmount + mFoodingAmount)

                mTOTOverTime1 = 0
                RsOT.MoveNext()
                If RsOT.EOF = False Then
                    cntRow = cntRow + 1
                    sprdAttn.MaxRows = cntRow
                End If
            Loop

            'If optShow(1).Checked = True Then
            CalcTots()
            'End If
        End If

        MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, sprdAttn.MaxCols)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrRefreshScreen:
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    '    Private Function GetOTRate(ByRef xCode As String, ByRef xRunDate As String, ByRef mESIApp As Boolean, ByRef mBasicSalary As Double, ByRef mESIRound As Double, ByRef IsArrear As Boolean, ByRef mOverTimeAppType As String, ByRef mGrossSalary As Double) As Double

    '        On Error GoTo ErrPart
    '        Dim SqlStr As String = ""
    '        Dim RsOTRate As ADODB.Recordset
    '        Dim mRound As String
    '        'Dim mGrossSalary As Double
    '        Dim ConWorkDay As Integer
    '        Dim ConWorkHour As Integer

    '        ConWorkHour = 8
    '        If MainClass.ValidateWithMasterTable(xCode, "EMP_CODE", "WORKING_HOURS", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '            ConWorkHour = Val(MasterNo)
    '        End If

    '        ConWorkHour = IIf(ConWorkHour = 0, 8, ConWorkHour)

    '        ConWorkDay = MainClass.LastDay(Month(CDate(txtDateFrom.Text)), Year(CDate(txtDateFrom.Text)))
    '        xRunDate = ConWorkDay & VB6.Format(xRunDate, "/MM/YYYY")

    '        SqlStr = " SELECT "

    '        If IsArrear = True Then
    '            SqlStr = SqlStr & vbCrLf & " (BASICSALARY-PREVIOUS_BASICSALARY) AS BASICSALARY, " & vbCrLf & " (AMOUNT-PREVIOUS_AMOUNT) AS AMOUNT, "
    '        Else
    '            SqlStr = SqlStr & vbCrLf & " BASICSALARY, AMOUNT, "
    '        End If

    '        SqlStr = SqlStr & vbCrLf & " ADD_DEDUCTCODE, ADDDEDUCT,TYPE, ROUNDING, EMP_DESG_CODE"

    '        SqlStr = SqlStr & vbCrLf _
    '            & " FROM PAY_SALARYDEF_MST SD, PAY_SALARYHEAD_MST SH " & vbCrLf _
    '            & " WHERE " & vbCrLf _
    '            & " SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    '            & " AND SD.COMPANY_CODE=SH.COMPANY_CODE" & vbCrLf _
    '            & " AND SD.ADD_DEDUCTCODE=SH.CODE" & vbCrLf _
    '            & " AND SD.EMP_CODE='" & xCode & "'" & vbCrLf _
    '            & " AND SD.SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) From PAY_SalaryDef_MST " & vbCrLf _
    '            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    '            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
    '            & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(xRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

    '        If IsArrear = True Then
    '            SqlStr = SqlStr & vbCrLf & " AND SD.IS_ARREAR='Y' AND TO_CHAR(SD.ARREAR_DATE,'MON-YYYY') ='" & UCase(VB6.Format(xRunDate, "MMM-YYYY")) & "'"
    '        End If

    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOTRate, ADODB.LockTypeEnum.adLockOptimistic)

    '        If RsOTRate.EOF = False Then
    '            ''Manager or Director then exit function dt. 15-09-2006.............
    '            If MainClass.ValidateWithMasterTable(RsOTRate.Fields("EMP_DESG_CODE").Value, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DESG_CAT IN ('D','M')") = True Then
    '                GetOTRate = 0
    '                mBasicSalary = 0
    '                mESIApp = False
    '                Exit Function
    '            End If

    '            mBasicSalary = IIf(IsDBNull(RsOTRate.Fields("BASICSALARY").Value), 0, RsOTRate.Fields("BASICSALARY").Value)
    '            mGrossSalary = mBasicSalary

    '            Do While Not RsOTRate.EOF
    '                If RsOTRate.Fields("ADDDEDUCT").Value = 1 Then

    '                    mGrossSalary = mGrossSalary + IIf(IsDBNull(RsOTRate.Fields("Amount").Value), 0, RsOTRate.Fields("Amount").Value)

    '                Else
    '                    '                mGrossSalary = mGrossSalary - IIf(IsNull(RsOTRate!AMOUNT), 0, RsOTRate!AMOUNT)
    '                    If RsOTRate.Fields("Type").Value = ConESI Then
    '                        '                    mESIRound = RsOTRate!ROUNDING
    '                        mESIRound = IIf(CDate(xRunDate) > CDate("01/12/2004"), "10", RsOTRate.Fields("ROUNDING").Value)
    '                        If RsOTRate.Fields("Amount").Value = 0 Then
    '                            mESIApp = False
    '                        Else
    '                            mESIApp = True
    '                        End If
    '                    End If
    '                End If
    '                RsOTRate.MoveNext()
    '            Loop

    '            GetOTRate = mGrossSalary / (ConWorkDay * ConWorkHour)
    '        Else
    '            GetOTRate = 0
    '            mBasicSalary = 0
    '            mGrossSalary = 0
    '            mESIApp = False
    '        End If


    '        If MainClass.ValidateWithMasterTable(ConOT, "Type", "Rounding", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '            GetOTRate = Int(GetOTRate) + IIf(GetOTRate > Int(GetOTRate), 1, 0)
    '        Else
    '            GetOTRate = CDbl(VB6.Format(GetOTRate, "0.00"))
    '        End If
    '        mBasicSalary = mGrossSalary
    '        Exit Function
    'ErrPart:
    '        GetOTRate = 0
    '        mBasicSalary = 0
    '        mESIApp = False
    '    End Function
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



    Private Function GetTOTOverTimeOld(ByRef xTotOTHOUR As Double, ByRef xTotOTMIN As Double) As Double
        On Error GoTo ErrPart
        Dim mHour As Double
        Dim mTempMin As Double
        Dim mMin As Double
        Dim mFactor As Double

        mHour = xTotOTHOUR
        mTempMin = xTotOTMIN

        mHour = mHour + Int(mTempMin / 60)
        mMin = (mTempMin Mod 60)
        mFactor = IIf(IsDBNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)
        mMin = Int(mMin / mFactor) * mFactor
        GetTOTOverTimeOld = mHour + mMin * 0.01

        Exit Function
ErrPart:
        GetTOTOverTimeOld = 0
    End Function



    Private Function GetTOTOverTime(ByRef xTotOTHOUR As Double, ByRef xTotOTMIN As Double) As Double
        On Error GoTo ErrPart
        Dim mHour As Double
        Dim mTempMin As Double
        Dim mMin As Double
        Dim mFactor As Double

        mHour = xTotOTHOUR
        mTempMin = xTotOTMIN

        mHour = mHour + Int(mTempMin / 60)
        mMin = (mTempMin Mod 60)
        mFactor = IIf(IsDBNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)
        mMin = Int(mMin / mFactor) * mFactor

        If mMin <> 0 Then
            mMin = mMin / 60
        End If

        GetTOTOverTime = mHour + mMin

        Exit Function
ErrPart:
        GetTOTOverTime = 0
    End Function

    Private Sub CalcTots()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mOT1 As Double
        Dim mOT2 As Double
        Dim mOT3 As Double
        Dim mPrevMonth As Double
        Dim mTotal As Double
        Dim mTotalAmt As Double
        Dim mFoodingAmount As Double
        Dim mOT1_Amt As Double
        Dim mOT2_Amt As Double
        Dim mOT_FoodingAmount As Double

        With sprdAttn
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColOT_R
                mOT1 = mOT1 + Val(.Text)

                .Col = ColOT_R_Amt
                mOT1_Amt = mOT1_Amt + Val(.Text)

                .Col = ColOT_E
                mOT2 = mOT2 + Val(.Text)

                .Col = ColOT_E_Amt
                mOT2_Amt = mOT2_Amt + Val(.Text)

                .Col = ColOT3
                mOT3 = mOT3 + Val(.Text)

                .Col = ColPrevMonth
                mPrevMonth = mPrevMonth + Val(.Text)

                .Col = ColTotalOT
                mTotal = mTotal + Val(.Text)

                .Col = ColTotalAmount
                mTotalAmt = mTotalAmt + Val(.Text)

                .Col = ColFoodingAmount
                mFoodingAmount = mFoodingAmount + Val(.Text)

                .Col = ColTotalAmount_Fooding
                mOT_FoodingAmount = mOT_FoodingAmount + Val(.Text)

            Next

            .MaxRows = .MaxRows + 1
            .Row = .MaxRows

            .Col = ColOT_R
            .Text = CStr(mOT1)

            .Col = ColOT_R_Amt
            .Text = CStr(mOT1_Amt)

            .Col = ColOT_E
            .Text = CStr(mOT2)

            .Col = ColOT_E_Amt
            .Text = CStr(mOT2_amt)

            .Col = ColOT3
            .Text = CStr(mOT3)

            .Col = ColPrevMonth
            .Text = CStr(mPrevMonth)

            .Col = ColTotalOT
            .Text = CStr(mTotal)

            .Col = ColTotalAmount
            .Text = CStr(mTotalAmt)

            .Col = ColFoodingAmount
            .Text = CStr(mFoodingAmount)

            .Col = ColTotalAmount_Fooding
            .Text = CStr(mOT_FoodingAmount)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = ColEmpCode
            .Col2 = ColTotalAmount_Fooding
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
