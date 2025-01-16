Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMonthlyESI
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim RsAttn As ADODB.Recordset = Nothing


    Dim FormActive As Boolean

    Private Const ColSNO As Short = 0
    Private Const ColCard As Short = 1
    Private Const ColName As Short = 2
    Private Const ColDesg As Short = 3
    Private Const ColESINo As Short = 4
    Private Const ColWorkedDays As Short = 5
    Private Const ColGSalary As Short = 6
    Private Const ColESIAmount As Short = 7
    Private Const ColEmperESIAmount As Short = 8
    Private Const ColTotESIAmount As Short = 9

    Private Const ConRowHeight As Short = 12

    Private Sub FillHeading(ByRef xDate As Date)

        Dim Tempdate As String
        Dim cntCol As Integer
        Dim NewDate As Date

        MainClass.ClearGrid(SprdView)

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Call FormatSprd(-1)

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
        Dim mSubTitle As String
        Dim mRptFileName As String
        Dim mBankName As String


        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...
        If MainClass.FillPrintDummyDataFromSprd(SprdView, 1, SprdView.MaxRows - 1, ColCard, ColTotESIAmount, PubDBCn) = False Then GoTo ERR1





        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            mTitle = "Employee State Insurance Contribution List"
        Else
            mTitle = "Employee State Insurance Contribution List - " & cboCategory.Text
        End If

        If optShow(0).Checked = True Then
            mSubTitle = "Salary"
        ElseIf optShow(1).Checked = True Then
            mSubTitle = "Arrear"
        ElseIf optShow(2).Checked = True Then
            mSubTitle = "Over Time"
        ElseIf optShow(3).Checked = True Then
            mSubTitle = "Over Time (Arrear)"
        ElseIf optShow(4).Checked = True Then
            mSubTitle = "Full & Final"
        End If

        mSubTitle = mSubTitle & " ( For the period : " & lblYear.Text & " )"


        mRptFileName = "ESIList.Rpt"


        'Select Record for print...

        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

        frmPrintOTReg.Close()

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

        Dim SqlStr As String = ""
        FillHeading(CDate(lblRunDate.Text))

        MainClass.ClearGrid(SprdView)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboCategory.Text = "" Then
                MsgInformation("Please select the Category Name.")
                cboCategory.Focus()
                Exit Sub
            End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, "Y")

        DisplayTotals()
        '    FormatSprd -1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub frmMonthlyESI_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmMonthlyESI_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        lblRunDate.Text = CStr(RunDate)
        FillHeading(CDate(lblRunDate.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        OptName.Checked = True
        FillDeptCombo()
        optShow(0).Checked = True
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub






    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        FillHeading(CDate(lblRunDate.Text))
        'RefreshScreen
    End Sub

    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        FillHeading(CDate(lblRunDate.Text))
        'RefreshScreen
    End Sub

    Private Function MakeSQL() As String

        On Error GoTo ErrRefreshScreen
        Dim mDeptCode As String
        Dim mMonth As Double
        Dim mESIRate As Double

        If CDate(lblRunDate.Text) >= CDate("01/07/2019") Then
            mESIRate = 0.0325 '' "ESI @3.75%"
        Else
            mESIRate = 0.0475
        End If

        mMonth = CDbl(VB6.Format(lblRunDate.Text, "YYYYMM"))
        'TO_CHAR(SUM(CEIL(PFESI.ESIABLEAMT * .0475) + PFESI.ESIAMT),'999999.99')
        If optShow(7).Checked = True Then
            MakeSQL = " SELECT  EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf & " EMP.EMP_DESG_CODE, EMP.EMP_ESI_NO, TO_CHAR(SUM(CASE WHEN ISARREAR = 'N' OR ISARREAR='F' OR ISARREAR='V' THEN PFESI.WDAYS ELSE 0 END),'999.9'), " & vbCrLf & " SUM(PFESI.ESIABLEAMT) AS ESIABLEAMT , SUM(PFESI.ESIAMT) AS ESIAMT, TO_CHAR(SUM((PFESI.ESIABLEAMT * " & mESIRate & ")),'999999.99') AS ESIABLEAMT," & vbCrLf & " TO_CHAR(SUM((PFESI.ESIABLEAMT * " & mESIRate & ") + PFESI.ESIAMT),'999999.99')" & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_PFESI_TRN PFESI " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf & " AND EMP.COMPANY_CODE=PFESI.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CODE=PFESI.EMP_CODE"

            If RsCompany.Fields("COMPANY_CODE").Value = 16 Or RsCompany.Fields("COMPANY_CODE").Value = 15 Then
                MakeSQL = MakeSQL & vbCrLf & " AND TO_CHAR(SAL_DATE ,'YYYYMM') =" & mMonth & ""
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND TO_CHAR(CASE WHEN ISARREAR='O' OR ISARREAR='X' THEN ADD_MONTHS(SAL_DATE , 1) ELSE SAL_DATE END,'YYYYMM') =" & mMonth & ""
            End If
            MakeSQL = MakeSQL & vbCrLf & " AND PFESI.ESIAMT<>0"
        Else
            MakeSQL = " SELECT  EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf & " EMP.EMP_DESG_CODE, EMP.EMP_ESI_NO, TO_CHAR(PFESI.WDAYS,'999.9'), " & vbCrLf & " PFESI.ESIABLEAMT, PFESI.ESIAMT, TO_CHAR((PFESI.ESIABLEAMT * " & mESIRate & "),'999999.99')," & vbCrLf & " TO_CHAR((PFESI.ESIABLEAMT * " & mESIRate & ") + PFESI.ESIAMT,'999999.99')" & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_PFESI_TRN PFESI " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf & " AND EMP.COMPANY_CODE=PFESI.COMPANY_CODE" & vbCrLf & " AND EMP.EMP_CODE=PFESI.EMP_CODE" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "' AND PFESI.ESIAMT<>0"
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR ='N'"
        ElseIf optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR ='Y'"
        ElseIf optShow(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR='O'"
        ElseIf optShow(3).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR ='X'"
        ElseIf optShow(4).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR ='F'"
        ElseIf optShow(5).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR ='V'"
        ElseIf optShow(6).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR  IN ('C','E')"
        ElseIf optShow(7).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND ISARREAR  IN ('N','Y','O','X','F','V','C','E')"
        End If

        If optShow(7).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " GROUP BY  EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf & " EMP.EMP_DESG_CODE, EMP.EMP_ESI_NO"
        End If

        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_CODE"
        ElseIf OptESI.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by TO_NUMBER(EMP.EMP_ESI_NO)"
        End If

        Exit Function
ErrRefreshScreen:
        'Resume
        MsgBox(Err.Description)
    End Function
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

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdView
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.6)
            .MaxCols = ColTotESIAmount

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCard, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 23)

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesg, 15)
            .ColHidden = True

            .Col = ColESINo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColESINo, 10)

            .Col = ColWorkedDays
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColWorkedDays, 7)

            .Col = ColGSalary
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColGSalary, 9)

            .Col = ColESIAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColESIAmount, 9)

            .Col = ColEmperESIAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEmperESIAmount, 9)

            .Col = ColTotESIAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTotESIAmount, 9)



        End With

        MainClass.ProtectCell(SprdView, 1, SprdView.MaxRows, ColCard, ColTotESIAmount)
        MainClass.SetSpreadColor(SprdView, mRow)

        With SprdView
            .Row = 0
            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCard
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name "
            .ColsFrozen = ColName

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColESINo
            .Text = "E.S.I. No."

            .Col = ColWorkedDays
            .Text = "Days"

            .Col = ColGSalary
            .Text = "Gross Salary"

            .Col = ColESIAmount
            .Text = "Employee Contribution"

            .Col = ColEmperESIAmount
            .Text = "Employer Contribution"

            .Col = ColTotESIAmount
            .Text = "Total"

            MainClass.ProtectCell(SprdView, 0, .MaxRows, 0, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdView.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub DisplayTotals()

        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mTotESI As Double
        Dim mTotDays As Double
        Dim mTotGSalary As Double
        Dim mTotEmplerESI As Double
        Dim mGrandTotESI As Double


        With SprdView
            Call MainClass.AddBlankfpSprdRow(SprdView, ColCard)
            .Row = .MaxRows

            .Col = ColCard
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '&H80FF80
            .BlockMode = False

            '        Call CalcRowTotal(SprdView, ColDebit, 1, ColDebit, .MaxRows - 1, .MaxRows, ColDebit)
            '        Call CalcRowTotal(SprdView, ColCredit, 1, ColCredit, .MaxRows - 1, .MaxRows, ColCredit)

            FormatSprd(-1)

            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColESIAmount
                mTotESI = mTotESI + Val(.Text)

                .Col = ColEmperESIAmount
                mTotEmplerESI = mTotEmplerESI + Val(.Text)

                .Col = ColTotESIAmount
                mGrandTotESI = mGrandTotESI + Val(.Text)

                .Col = ColWorkedDays
                mTotDays = mTotDays + Val(.Text)

                .Col = ColGSalary
                mTotGSalary = mTotGSalary + Val(.Text)


            Next
            .Row = .MaxRows

            .Col = ColESIAmount
            .Text = VB6.Format(mTotESI, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColWorkedDays
            .Text = VB6.Format(mTotDays, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColGSalary
            .Text = VB6.Format(mTotGSalary, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColEmperESIAmount
            .Text = VB6.Format(mTotEmplerESI, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColTotESIAmount
            .Text = VB6.Format(mGrandTotESI, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .set_RowHeight(.Row, 1.25 * ConRowHeight)
            '        .RowsFrozen = .MaxRows
        End With

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
End Class
