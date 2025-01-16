Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalaryRegYearlyNew
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
    Private Const ColAprDays As Short = 3
    Private Const ColAprSalary As Short = 4
    Private Const ColMayDays As Short = 5
    Private Const ColMaySalary As Short = 6
    Private Const ColJunDays As Short = 7
    Private Const ColJunSalary As Short = 8
    Private Const ColJulDays As Short = 9
    Private Const ColJulSalary As Short = 10
    Private Const ColAugDays As Short = 11
    Private Const ColAugSalary As Short = 12
    Private Const ColSepDays As Short = 13
    Private Const ColSepSalary As Short = 14
    Private Const ColOctDays As Short = 15
    Private Const ColOctSalary As Short = 16
    Private Const ColNovDays As Short = 17
    Private Const ColNovSalary As Short = 18
    Private Const ColDecDays As Short = 19
    Private Const ColDecSalary As Short = 20
    Private Const ColJanDays As Short = 21
    Private Const ColJanSalary As Short = 22
    Private Const ColFebDays As Short = 23
    Private Const ColFebSalary As Short = 24
    Private Const ColMarDays As Short = 25
    Private Const ColMarSalary As Short = 26
    Private Const ColTotDays As Short = 27
    Private Const ColTotSalary As Short = 28

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub FillHeading()
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer

        With sprdAttn
            .MaxCols = ColTotSalary

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCard
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColAprDays
            .Text = "Apr Days"

            .Col = ColAprSalary
            .Text = "Apr Salary"

            .Col = ColMayDays
            .Text = "May Days"

            .Col = ColMaySalary
            .Text = "May Salary"

            .Col = ColJunDays
            .Text = "Jun Days"

            .Col = ColJunSalary
            .Text = "Jun Salary"

            .Col = ColJulDays
            .Text = "Jul Days"

            .Col = ColJulSalary
            .Text = "Jul Salary"

            .Col = ColAugDays
            .Text = "Aug Days"

            .Col = ColAugSalary
            .Text = "Aug Salary"

            .Col = ColSepDays
            .Text = "Sep Days"

            .Col = ColSepSalary
            .Text = "Sep Salary"

            .Col = ColOctDays
            .Text = "Oct Days"

            .Col = ColOctSalary
            .Text = "Oct Salary"

            .Col = ColNovDays
            .Text = "Nov Days"

            .Col = ColNovSalary
            .Text = "Nov Salary"

            .Col = ColDecDays
            .Text = "Dec Days"

            .Col = ColDecSalary
            .Text = "Dec Salary"

            .Col = ColJanDays
            .Text = "Jan Days"

            .Col = ColJanSalary
            .Text = "Jan Salary"

            .Col = ColFebDays
            .Text = "Feb Days"

            .Col = ColFebSalary
            .Text = "Feb Salary"

            .Col = ColMarDays
            .Text = "Mar Days"

            .Col = ColMarSalary
            .Text = "Mar Salary"

            .Col = ColTotDays
            .Text = "Total Days"

            .Col = ColTotSalary
            .Text = "Total Salary"

            FormatSprd(-1)
        End With
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        txtEmpCode.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
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

        PubDBCn.Errors.Clear()


        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, ColCard, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mRptFileName = "SalRegYearlyNew.Rpt"

        mTitle = "Salary Register (Yearly)"

        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

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
        Call ReportForSalary(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(sprdAttn)
        RefreshScreen()
        FillHeading()
        FormatSprd(-1)
        Call CalcTotals()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmSalaryRegYearlyNew_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmSalaryRegYearlyNew_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String

        SqlStr = " SELECT SALTRN.EMP_CODE, EMP.EMP_NAME, "

        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' THEN WDAYS END) AS APR_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS APR_SAL,"

        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' THEN WDAYS END) AS MAY_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS MAY_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' THEN WDAYS END) AS JUN_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS JUN_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' THEN WDAYS END) AS JUL_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS JUL_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' THEN WDAYS END) AS AUG_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS AUG_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' THEN WDAYS END) AS SEP_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS SEP_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' THEN WDAYS END) AS OCT_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS OCT_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' THEN WDAYS END) AS NOV_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS NOV_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' THEN WDAYS END) AS DEC_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS DEC_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' THEN WDAYS END) AS JAN_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS JAN_SAL,"


        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' THEN WDAYS END) AS FEB_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS FEB_SAL,"

        SqlStr = SqlStr & vbCrLf & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN WDAYS END) AS MAR_DAYS, " & vbCrLf & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN PAYABLESALARY END) + " & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS MAR_SAL,"

        SqlStr = SqlStr & vbCrLf & " '0.00','0.00' " & vbCrLf
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN WDAYS END) AS MAR_DAYS, " & vbCrLf _
        ''            & " ROUND(MAX(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN PAYABLESALARY END) + " & vbCrLf _
        ''            & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN  PAYABLEAMOUNT*DECODE(ADDDEDUCT,1,1,-1) END),0) AS MAR_SAL"

        SqlStr = SqlStr & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.SALHEADCODE =ADD_DEDUCT.CODE " & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"
        End If

        If optSalaryType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='N' "
        Else
            SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='Y' "
        End If

        If optSalary(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"
        ElseIf optSalary(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & ")"
        ElseIf optSalary(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY SALTRN.EMP_CODE, EMP.EMP_NAME "
        SqlStr = SqlStr & vbCrLf & " ORDER BY SALTRN.EMP_CODE, EMP.EMP_NAME "

        MainClass.AssignDataInSprd8(SqlStr, sprdAttn, StrConn, "Y")

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
            .ColsFrozen = ColName

            For cntCol = ColAprDays To ColTotDays Step 2
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 6)
            Next

            For cntCol = ColAprSalary To ColTotSalary Step 2
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
            Next
        End With

        '    MainClass.ProtectCell sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxRows
        '    sprdAttn.OperationMode = OperationModeSingle
        '    MainClass.SetSpreadColor sprdAttn, mRow
        '
        MainClass.ProtectCell(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxRows)
        sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        sprdAttn.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(sprdAttn, mRow)

        Exit Sub
ERR1:

        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        cmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub

    Private Sub frmSalaryRegYearlyNew_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            TxtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            TxtName.Text = AcName
        End If
    End Sub
    Private Sub CalcTotals()
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mTotDays As Double
        Dim mTotSalary As Double

        With sprdAttn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                mTotDays = 0
                mTotSalary = 0
                For cntCol = ColAprDays To ColMarDays Step 2
                    .Col = cntCol
                    mTotDays = mTotDays + Val(.Text)
                Next
                For cntCol = ColAprSalary To ColMarSalary Step 2
                    .Col = cntCol
                    mTotSalary = mTotSalary + Val(.Text)
                Next

                .Row = cntRow
                .Col = ColTotDays
                .Text = VB6.Format(mTotDays, "0.00")

                .Col = ColTotSalary
                .Text = VB6.Format(mTotSalary, "0.00")

            Next
        End With
    End Sub
End Class
