Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPerksVarProcess
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColBSalary As Short = 3

    Private Function CalcBSalary(ByRef mCode As String) As Double

        On Error GoTo ERR1
        Dim RSSalDef As ADODB.Recordset

        CalcBSalary = 0

        SqlStr = " SELECT BASICSALARY from PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) From PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalDef, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalDef.EOF = False Then
            CalcBSalary = MainClass.FormatRupees(IIf(IsDbNull(RSSalDef.Fields("BASICSALARY").Value), 0, RSSalDef.Fields("BASICSALARY").Value))
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CalcVariable(ByRef mCode As String, ByRef mRow As Integer, ByRef mLICAmount As Double, ByRef mBankLoan As Double, ByRef ITAmount As Double) As Boolean

        On Error GoTo ERR1
        Dim RSSalVar As ADODB.Recordset
        Dim cntCol As Integer
        Dim mHeadTitle As String

        CalcVariable = True
        SqlStr = " SELECT TRN.*, " & vbCrLf & " SMST.NAME, " & vbCrLf & " SMST.ADDDEDUCT, SMST.CALC_ON, SMST.TYPE, " & vbCrLf & " SMST.SEQ,DEFAULT_AMT " & vbCrLf & " FROM PAY_PERKS_TRN TRN, PAY_SALARYHEAD_MST SMST " & vbCrLf & " WHERE  TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE=SMST.CODE" & vbCrLf & " AND TRN.EMP_Code='" & mCode & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " ORDER BY SMST.SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalVar, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalVar.EOF = False Then
            Do While Not RSSalVar.EOF
                SprdMain.Row = mRow

                For cntCol = ColBSalary + 1 To SprdMain.MaxCols
                    SprdMain.Col = cntCol
                    SprdMain.Row = 0
                    If Trim(UCase(RSSalVar.Fields("Name").Value)) = Trim(UCase(SprdMain.Text)) Then
                        SprdMain.Row = mRow
                        SprdMain.Text = MainClass.FormatRupees(IIf(IsDbNull(RSSalVar.Fields("Amount").Value), 0, RSSalVar.Fields("Amount").Value))
                        GoTo NextRec
                    End If
                Next
NextRec:
                RSSalVar.MoveNext()
            Loop
        End If
        Exit Function
ERR1:
        CalcVariable = False
    End Function
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(SprdMain)

        With SprdMain
            .MaxCols = ColBSalary

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name "
            .ColsFrozen = ColName

            .Col = ColBSalary
            .Text = "Basic Salary"

            SqlStr = " SELECT NAME,ADDDEDUCT " & vbCrLf & " FROM PAY_SALARYHEAD_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT=" & ConPerks & " AND PAYMENT_TYPE='M'" & vbCrLf & " AND CALC_ON=" & ConCalcVariable & ""

            '        If RsCompany!PRINTOTINPAYSLIP = "N" Then
            '            SqlStr = SqlStr & vbCrLf & " AND TYPE<>" & ConOT & " "
            '        End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY ADDDEDUCT,SEQ "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                .MaxCols = .MaxCols + 1
                cntCol = 1
                Do While Not RsTemp.EOF
                    .Col = ColBSalary + cntCol
                    .Text = RsTemp.Fields("Name").Value
                    cntCol = cntCol + 1
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        .MaxCols = .MaxCols + 1
                    End If
                Loop
            End If
            MainClass.ProtectCell(SprdMain, 0, SprdMain.MaxRows, 0, ColBSalary)
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

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mCode As String
        Dim mSalary As Double

        SqlStr = ""
        PubDBCn.BeginTrans()

        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Col = ColCode
            SprdMain.Row = cntRow
            mCode = SprdMain.Text

            SprdMain.Col = ColBSalary
            If IsNumeric(SprdMain.Text) Then
                mSalary = CDbl(SprdMain.Text)
                If UpdateMonthTrn(mCode, mSalary, cntRow) = False Then GoTo UpdateError
            End If
        Next

        PubDBCn.CommitTrans()

        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        '    Resume
        PubDBCn.Errors.Clear()
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateMonthTrn(ByRef xCode As String, ByRef xSalary As Double, ByRef xRow As Integer) As Boolean


        On Error GoTo UpdateMonthTrnErr
        Dim SqlStr As String = ""
        Dim cntCol As Integer
        Dim xMonth As String
        Dim xTypeCode As Integer
        Dim xLoanType As Integer
        Dim xAmount As Double
        Dim xAddDays As Double
        Dim mMonthDate As String
        Dim RsTemp As ADODB.Recordset = Nothing

        xMonth = UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY"))
        mMonthDate = "01-" & xMonth


        SqlStr = " DELETE " & vbCrLf & " FROM PAY_PERKS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE=" & MainClass.AllowSingleQuote(xCode) & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & VB6.Format(mMonthDate, "YYYYMM") & " AND BOOKTYPE='" & lblBookType.Text & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        SprdMain.Col = ColBSalary
        SprdMain.Row = xRow
        xAddDays = CDbl(IIf(IsNumeric(SprdMain.Text), SprdMain.Text, 0))

        For cntCol = ColBSalary + 1 To SprdMain.MaxCols
            SprdMain.Row = 0
            SprdMain.Col = cntCol

            SqlStr = " SELECT CODE,TYPE FROM PAY_SALARYHEAD_MST" & vbCrLf & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND NAME = '" & MainClass.AllowSingleQuote(Trim(SprdMain.Text)) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                xTypeCode = RsTemp.Fields("CODE").Value

                SprdMain.Row = xRow
                xAmount = CDbl(IIf(IsNumeric(SprdMain.Text), SprdMain.Text, 0))
            Else
                GoTo NextCol
            End If


            If xAmount <> 0 Then
                SqlStr = " INSERT INTO PAY_PERKS_TRN ( " & vbCrLf & " COMPANY_CODE, SAL_DATE, " & vbCrLf & " EMP_CODE, ADD_DEDUCTCODE, AMOUNT,BOOKTYPE,DC,PAYMENT_TYPE,ADDUSER,ADDDATE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & xCode & "', " & xTypeCode & ", " & xAmount & ",'" & lblBookType.Text & "'," & vbCrLf & " 'D', '1','" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                PubDBCn.Execute(SqlStr)
            End If
NextCol:
        Next
        UpdateMonthTrn = True
        Exit Function
UpdateMonthTrnErr:
        ''Resume
        MsgBox(Err.Description)
        UpdateMonthTrn = False
    End Function

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        Dim mYM As Integer

        '    If PubSuperUser = "U" Then
        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        If SalPerksProcess(mYM) = False Then
            MsgBox("You are enable to process. ", MsgBoxStyle.Critical)
            Exit Sub
        End If
        '    End If

        If Update1 = True Then
            CmdSave.Enabled = False
        Else
            CmdSave.Enabled = True
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub


    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call ReportForDeduction(crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForDeduction(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String


        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(SprdMain, 0, SprdMain.MaxRows, ColCode, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        mTitle = "Monthly Perks List "

        Call ShowReport(SqlStr, "MonthlyVar.Rpt", Mode, mTitle, mSubTitle)

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
        '    Call ReportForDeduction(crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        RefreshScreen()
    End Sub
    Private Sub frmPerksVarProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
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
        OptName.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        FormatSprd(-1)
        UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        FillDeptCombo()
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub



    Private Sub UpDYear_DownClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain, -1)
        '' RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(SprdMain, -1)
        ''RefreshScreen
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo ErrRefreshScreen

        Dim RsEmpSal As ADODB.Recordset
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mMonth As Short
        Dim mYear As Short
        Dim mYYMM As Integer
        Dim mDOJ As String
        Dim mDOL As String
        Dim mDept As String
        Dim mBasicSalary As Double

        mMonth = Month(CDate(lblRunDate.Text))
        mYear = Year(CDate(lblRunDate.Text))
        mYYMM = Val(Str(mYear) & VB6.Format(mMonth, "00"))


        mDOJ = MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
        mDOL = "01" & "/" & mMonth & "/" & mYear

        SqlStr = " SELECT EMP_NAME,EMP_CODE,BNKLOAN_DED, LIC_DED, ITAX_DED " & vbCrLf & " FROM PAY_EMPLOYEE_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) " & vbCrLf & " AND EMP_STOP_SALARY='N'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDept)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf & " PAY_FFSETTLE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "')"


        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP_CODE"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmpSal.EOF = False Then
            With SprdMain
                cntRow = 1
                .MaxRows = cntRow
                Do While Not RsEmpSal.EOF
                    .Row = cntRow

                    .Col = ColCode
                    mCode = RsEmpSal.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsEmpSal.Fields("EMP_NAME").Value

                    mBasicSalary = CalcBSalary(mCode)

                    .Col = ColBSalary
                    .Text = VB6.Format(mBasicSalary, "0.00")


                    If CalcVariable(mCode, cntRow, IIf(IsDbNull(RsEmpSal.Fields("LIC_DED").Value), 0, RsEmpSal.Fields("LIC_DED").Value), IIf(IsDbNull(RsEmpSal.Fields("BNKLOAN_DED").Value), 0, RsEmpSal.Fields("BNKLOAN_DED").Value), IIf(IsDbNull(RsEmpSal.Fields("ITAX_DED").Value), 0, RsEmpSal.Fields("ITAX_DED").Value)) = False Then GoTo NextRow

NextRow:
                    cntRow = cntRow + 1
                    RsEmpSal.MoveNext()
                    If RsEmpSal.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop

                ColTotal(SprdMain, ColBSalary + 1, .MaxCols)
                .Col = ColName
                .Row = .MaxRows
                .Text = "TOTAL :"

                FormatSprd(-1)

                MainClass.ProtectCell(SprdMain, .MaxRows, .MaxRows, 0, .MaxCols)
            End With
        End If
        CmdSave.Enabled = True
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing

        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboDept.Items.Clear()
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
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))
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
        With SprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .set_ColWidth(ColSNO, 4)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCode, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 22)

            .Col = ColBSalary
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBSalary, 8)


            For cntCol = ColBSalary + 1 To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 8)
            Next

        End With
        MainClass.ProtectCell(SprdMain, 0, SprdMain.MaxRows, 0, ColBSalary)
        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function CheckVariableType(ByRef mSalHeadName As String, ByRef mVariableType As Integer) As Boolean

        On Error GoTo ErrCheck
        Dim RsCheck As ADODB.Recordset
        CheckVariableType = False

        SqlStr = " SELECT TYPE FROM PAY_SALARYHEAD_MST WHERE " & vbCrLf & " NAME = '" & MainClass.AllowSingleQuote(mSalHeadName) & "' AND " & vbCrLf & " TYPE IN (" & mVariableType & ") AND Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheck, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCheck.EOF = False Then
            CheckVariableType = True
        End If
        Exit Function
ErrCheck:
        CheckVariableType = False
        MsgBox(Err.Description)
    End Function

    Private Function CalcMonthYear(ByRef xSubkey As Integer, ByRef mType As String) As Short
        Dim mMonth As Short
        Dim mYear As Short
        xSubkey = CInt(VB6.Format(xSubkey, "000000"))
        mMonth = CShort(Mid(CStr(xSubkey), 5, 6))
        mYear = CShort(Mid(CStr(xSubkey), 1, 4))
        If mType = "M" Then
            CalcMonthYear = IIf(mMonth > 12, 1, mMonth)
        ElseIf mType = "Y" Then
            CalcMonthYear = IIf(mMonth > 12, mYear + 1, mYear)
        End If
    End Function

    Private Function ReCalcLoanMaster(ByRef xCode As String, ByRef xMkey As String, ByRef xLOANAMOUNT As Double) As Boolean

        On Error GoTo ErrReCalcLoanMaster
        Dim RsReLoan As ADODB.Recordset
        Dim mBalanceAmount As Double

        ReCalcLoanMaster = True

        mBalanceAmount = xLOANAMOUNT
        SqlStr = " SELECT BALANCE_AMOUNT,DEDUCT_DATE FROM PAY_LOAN_MST " & vbCrLf & " WHERE MKEY='" & xMkey & "' AND" & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "'" & vbCrLf & " ORDER BY DEDUCT_DATE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReLoan, ADODB.LockTypeEnum.adLockOptimistic)
        If RsReLoan.EOF = False Then
            Do While Not RsReLoan.EOF
                mBalanceAmount = mBalanceAmount - IIf(IsDbNull(RsReLoan.Fields("Balance_Amount").Value), 0, RsReLoan.Fields("Balance_Amount").Value)
                If mBalanceAmount < 0 Then
                    SqlStr = " DELETE FROM PAY_LOAN_MST " & vbCrLf & " WHERE MKEY='" & xMkey & "' AND " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "' AND " & vbCrLf & " DEDUCT_DATE = TO_DATE('" & VB6.Format(RsReLoan.Fields("DEDUCT_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                    PubDBCn.Execute(SqlStr)
                End If
                RsReLoan.MoveNext()
            Loop
        End If
        Exit Function
ErrReCalcLoanMaster:
        ReCalcLoanMaster = False
    End Function

    Private Function SalPerksProcess(ByRef mYM As Integer) As Boolean

        On Error GoTo ErrSalProcess
        Dim RsMain As ADODB.Recordset
        SalPerksProcess = True
        SqlStr = " SELECT EMP_CODE FROM PAY_PERKS_TRN WHERE " & vbCrLf & " TO_CHAR(SAL_DATE,'YYYYMM') > " & mYM & "" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & lblBookType.Text & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockOptimistic)

        If RsMain.EOF = False Then
            SalPerksProcess = False
        End If
        Exit Function
ErrSalProcess:
        SalPerksProcess = False
    End Function
End Class
