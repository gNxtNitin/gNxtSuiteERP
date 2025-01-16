Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmWelfareReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    Private Const ColCard As Short = 1
    Private Const ColName As Short = 2
    Private Const ColFName As Short = 3
    Private Const ColDesg As Short = 4
    Private Const ColTokenNo As Short = 5
    Private Const ColDOJ As Short = 6
    Private Const ColDOL As Short = 7
    Private Const ColJan As Short = 8
    Private Const ColFeb As Short = 9
    Private Const ColMar As Short = 10
    Private Const ColApr As Short = 11
    Private Const ColMay As Short = 12
    Private Const ColJun As Short = 13
    Private Const ColJul As Short = 14
    Private Const ColAug As Short = 15
    Private Const ColSep As Short = 16
    Private Const ColOct As Short = 17
    Private Const ColNov As Short = 18
    Private Const ColDec As Short = 19
    Private Const ColEmpShare As Short = 20
    Private Const ColEmperShare As Short = 21
    Private Const ColGrossAmount As Short = 22
    Private Const ColRemarks As Short = 23

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub FillHeading()
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer

        With sprdAttn
            .MaxCols = ColRemarks

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Col = ColSNo
            .Text = "S. No."

            .Col = ColCard
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name"

            .Col = ColFName
            .Text = "Father's / Husband's Name of Worker / Employee"

            .Col = ColDesg
            .Text = "Designation of Worker / Employee"

            .Col = ColTokenNo
            .Text = "Token No. (if any)"

            .Col = ColDOJ
            .Text = "Date of Joining"

            .Col = ColDOL
            .Text = "Date of Relieving"

            .Col = ColJan
            .Text = "Jan"

            .Col = ColFeb
            .Text = "Feb"

            .Col = ColMar
            .Text = "Mar"

            .Col = ColApr
            .Text = "Apr"

            .Col = ColMay
            .Text = "May"

            .Col = ColJun
            .Text = "Jun"

            .Col = ColJul
            .Text = "Jul"

            .Col = ColAug
            .Text = "Aug"

            .Col = ColSep
            .Text = "Sep"

            .Col = ColOct
            .Text = "Oct"

            .Col = ColNov
            .Text = "Nov"

            .Col = ColDec
            .Text = "Dec"

            .Col = ColEmpShare
            .Text = "Total Employee's Share"

            .Col = ColEmperShare
            .Text = "Total Employer's Share"

            .Col = ColGrossAmount
            .Text = "Gross Amount"

            .Col = ColRemarks
            .Text = "Remarks"

            FormatSprd(-1)
        End With
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
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
        Dim CntRow As Integer
        Dim mBankName As String
        Dim mRptFileName As String

        PubDBCn.Errors.Clear()


        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows - 1, ColCard, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mRptFileName = "WelfareReg.Rpt"

        mTitle = "Proforma for Contribution Details"

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

        Dim mCompanyName As String
        Dim mCompanyAdd As String
        Dim mPrincipleName As String
        Dim mPrincipleAddress As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        If lblEmployee.Text = "Y" Then
            mCompanyName = RsCompany.Fields("Company_Name").Value
            mCompanyAdd = RsCompany.Fields("COMPANY_ADDR").Value & ",  " & RsCompany.Fields("COMPANY_CITY").Value & " , " & RsCompany.Fields("COMPANY_STATE").Value & " - " & RsCompany.Fields("COMPANY_PIN").Value & ""
            mPrincipleName = ""
            mPrincipleAddress = ""
        Else
            mCompanyName = Trim(cboContractor.Text)

            SqlStr = "Select CON_ADDRESS FROM PAY_CONTRACTOR_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CON_NAME"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            mCompanyAdd = IIf(IsDbNull(RsTemp.Fields("CON_ADDRESS").Value), "", RsTemp.Fields("CON_ADDRESS").Value)
            mPrincipleName = RsCompany.Fields("Company_Name").Value
            mPrincipleAddress = RsCompany.Fields("COMPANY_ADDR").Value & ",  " & RsCompany.Fields("COMPANY_CITY").Value & " , " & RsCompany.Fields("COMPANY_STATE").Value & " - " & RsCompany.Fields("COMPANY_PIN").Value & ""
        End If

        MainClass.AssignCRptFormulas(Report1, "EstablishmentCompany=""" & mCompanyName & """")
        MainClass.AssignCRptFormulas(Report1, "EstablishmentAddress=""" & mCompanyAdd & """")
        MainClass.AssignCRptFormulas(Report1, "PrincipleCompany=""" & mPrincipleName & """")
        MainClass.AssignCRptFormulas(Report1, "PrincipleAddress=""" & mPrincipleAddress & """")

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

        If lblEmployee.Text = "Y" Then
            RefreshScreen()
        Else
            RefreshScreencont()
        End If

        FillHeading()
        FormatSprd(-1)
        Call CalcTotals()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmWelfareReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
        If FormActive = True Then Exit Sub
        FraSelection.Visible = IIf(lblEmployee.Text = "Y", False, True)
        cboContractor.Visible = IIf(lblEmployee.Text = "Y", False, True)
        FormActive = True
    End Sub
    Private Sub frmWelfareReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        FormActive = False
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        FillHeading()
        FillCombo()

        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub FillCombo()

        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "Select CON_NAME FROM PAY_CONTRACTOR_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' Order by CON_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        cboContractor.Items.Clear()
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                cboContractor.Items.Add(RsTemp.Fields("CON_NAME").Value)
                RsTemp.MoveNext()
            Loop
        End If
        cboContractor.SelectedIndex = 0


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String


        SqlStr = " SELECT SALTRN.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME, " & vbCrLf & " GETEMPDESG(EMP.COMPANY_CODE, EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC, " & vbCrLf & " '', EMP_DOJ, EMP_LEAVE_DATE,"

        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' THEN  PAYABLEAMOUNT*3 END) AS JAN_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' THEN  PAYABLEAMOUNT*3 END) AS FEB_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN  PAYABLEAMOUNT*3 END) AS MAR_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' THEN  PAYABLEAMOUNT*3 END) AS APR_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' THEN  PAYABLEAMOUNT*3 END) AS MAY_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' THEN  PAYABLEAMOUNT*3 END) AS JUN_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' THEN  PAYABLEAMOUNT*3 END) AS JUL_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' THEN  PAYABLEAMOUNT*3 END) AS AUG_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' THEN  PAYABLEAMOUNT*3 END) AS SEP_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' THEN  PAYABLEAMOUNT*3 END) AS OCT_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' THEN  PAYABLEAMOUNT*3 END) AS NOV_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' THEN  PAYABLEAMOUNT*3 END) AS DEC_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(PAYABLEAMOUNT) AS EMP_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(PAYABLEAMOUNT*2) AS EMPER_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(PAYABLEAMOUNT*3) AS TOT_SAL,"

        SqlStr = SqlStr & vbCrLf & "'' " & vbCrLf
        SqlStr = SqlStr & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.SALHEADCODE =ADD_DEDUCT.CODE "

        '    SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='N' "

        '    SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_STOP_SALARY='N' "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConWelfare & ""

        SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY SALTRN.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME, EMP_DOJ,EMP_LEAVE_DATE,"
        SqlStr = SqlStr & vbCrLf & " GETEMPDESG(EMP.COMPANY_CODE, EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        SqlStr = SqlStr & vbCrLf & " ORDER BY SALTRN.EMP_CODE, EMP.EMP_NAME "

        MainClass.AssignDataInSprd8(SqlStr, sprdAttn, StrConn, "Y")

        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub RefreshScreencont()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mContCode As String
        Dim mContName As String


        SqlStr = " SELECT SALTRN.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME, " & vbCrLf & " GETEMPDESG(EMP.COMPANY_CODE, EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC, " & vbCrLf & " '', EMP_DOJ, EMP_LEAVE_DATE,"

        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JAN' THEN  PAYABLEAMOUNT*3 END) AS JAN_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='FEB' THEN  PAYABLEAMOUNT*3 END) AS FEB_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAR' THEN  PAYABLEAMOUNT*3 END) AS MAR_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='APR' THEN  PAYABLEAMOUNT*3 END) AS APR_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='MAY' THEN  PAYABLEAMOUNT*3 END) AS MAY_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUN' THEN  PAYABLEAMOUNT*3 END) AS JUN_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='JUL' THEN  PAYABLEAMOUNT*3 END) AS JUL_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='AUG' THEN  PAYABLEAMOUNT*3 END) AS AUG_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='SEP' THEN  PAYABLEAMOUNT*3 END) AS SEP_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='OCT' THEN  PAYABLEAMOUNT*3 END) AS OCT_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='NOV' THEN  PAYABLEAMOUNT*3 END) AS NOV_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(SAL_DATE,'MON')='DEC' THEN  PAYABLEAMOUNT*3 END) AS DEC_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(PAYABLEAMOUNT) AS EMP_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(PAYABLEAMOUNT*2) AS EMPER_SAL,"
        SqlStr = SqlStr & vbCrLf & " SUM(PAYABLEAMOUNT*3) AS TOT_SAL,"

        SqlStr = SqlStr & vbCrLf & "'' " & vbCrLf
        SqlStr = SqlStr & vbCrLf & " FROM PAY_CONT_SAL_TRN SALTRN, PAY_CONT_EMPLOYEE_MST EMP, PAY_CONT_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND SALTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.SALHEADCODE =ADD_DEDUCT.CODE "

        '    SqlStr = SqlStr & vbCrLf & " AND SALTRN.ISARREAR='N' "

        '    SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_STOP_SALARY='N' "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConWelfare & ""


        If MainClass.ValidateWithMasterTable(cboContractor.Text, "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mContCode = MasterNo
            SqlStr = SqlStr & vbCrLf & "AND EMP.CONTRACTOR_CODE='" & MainClass.AllowSingleQuote(mContCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY SALTRN.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME, EMP_DOJ,EMP_LEAVE_DATE,"
        SqlStr = SqlStr & vbCrLf & " GETEMPDESG(EMP.COMPANY_CODE, EMP.EMP_CODE,TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
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

            .set_ColWidth(ColSNo, 5)

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCard, 7)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 15)
            .ColsFrozen = ColName

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColFName, 15)

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesg, 12)

            .Col = ColTokenNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColTokenNo, 8)

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDOJ, 12)

            .Col = ColDOL
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDOL, 12)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColRemarks, 12)

            For cntCol = ColJan To ColGrossAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_INTEGER
                '            .TypeFloatDecimalChar = Asc(".")
                .TypeNumberMax = CDbl("9999999.99")
                .TypeNumberMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 7)
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
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub
    Private Sub CalcTotals()
        Dim CntRow As Integer
        Dim cntCol As Integer
        Dim mTotCol As Double

        With sprdAttn
            .MaxRows = .MaxRows + 1
            .Row = .MaxRows
            .Col = ColName
            .Text = "TOTAL :"

            For cntCol = ColJan To ColGrossAmount
                mTotCol = 0
                For CntRow = 1 To .MaxRows - 1
                    .Row = CntRow
                    .Col = cntCol
                    mTotCol = mTotCol + Val(.Text)
                Next

                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mTotCol, "0.00")
            Next

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '' &H8000000B             ''&H80FF80
            .BlockMode = False
        End With

        '         ColTotal sprdAttn, ColBSalary, .MaxCols
        '            .Col = ColName
        '            .Row = .MaxRows
        '            .Text = "TOTAL :"
        '            MainClass.ProtectCell sprdAttn, 0, .MaxRows, 0, .MaxCols



    End Sub

    Private Sub frmWelfareReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
End Class
