Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPFForm5
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    Private Const ColAccountNo As Short = 1
    Private Const ColCode As Short = 2
    Private Const ColName As Short = 3
    Private Const ColFName As Short = 4
    Private Const ColAge As Short = 5
    Private Const ColSex As Short = 6
    Private Const ColDOM As Short = 7
    Private Const ColPeriod As Short = 8
    Private Const ColRemarks As Short = 9
    Private Const ColDOB As Short = 10


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With sprdMain
            .MaxCols = ColDOB
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)
            .set_RowHeight(0, ConRowHeight * 2.5)

            .Col = ColAccountNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColAccountNo, 10)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 21)


            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColFName, 20)
            .ColsFrozen = ColFName

            .Col = ColAge
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColAge, 4)

            .Col = ColSex
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColSex, 6)

            .Col = ColDOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColDOM, 8)

            .Col = ColPeriod
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColPeriod, 8)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 8)

            .Col = ColDOB
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDOB, 8)

        End With

        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, 1, sprdMain.MaxCols)
        sprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        MainClass.SetSpreadColor(sprdMain, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdMain)

        With sprdMain
            .MaxCols = ColDOB
            .Row = 0

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColAccountNo
            .Text = "Accont No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Name of the Employees" & vbNewLine & "(in block letters)"

            .Col = ColFName
            .Text = "Father's Name or husband's Name in case of married women"

            .Col = ColAge
            .Text = "Age"

            .Col = ColSex
            .Text = "Sex"

            .Col = ColDOM
            .Text = "Date of eligibility for membership"

            .Col = ColPeriod
            .Text = "Total period of previous service (excluding period of breaks on the date of joining the fund)"

            .Col = ColRemarks
            .Text = "Remarks"

            .Col = ColDOB
            .Text = "Date of Birth"
        End With
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
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
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdMain, 1, sprdMain.MaxRows, 0, sprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "[Paragraph 36 (2) (b)]"
        mTitle = "Employees Provident Fund Scheme, 1952"

        Call ShowReport(SqlStr, "PFForm5.Rpt", Mode, mTitle, mSubTitle)

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

        Dim mRemarks As String

        mRemarks = "Return of Employee Qualifying for membership of the Employee s Provident Fund"
        mRemarks = mRemarks & " for the first time during the month of " & lblYear.Text
        mRemarks = mRemarks & " (to be sent to the Commissioner with Form 2) "

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        MainClass.AssignCRptFormulas(Report1, "Remarks='" & mRemarks & "'")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub
    Private Sub frmPFForm5_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmPFForm5_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
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
        lblRunDate.Text = CStr(RunDate)
        OptName.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub frmPFForm5_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub UpDYear_DownClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub
    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mMonth As Short
        Dim mYear As Short
        Dim CntRow As Integer
        Dim mDOJ As Date
        Dim mDOL As Date
        Dim mRow As Integer
        Dim mPFCode As Integer
        Dim mAge As String
        Dim mDOB As Date

        MainClass.ClearGrid(sprdMain)

        If MainClass.ValidateWithMasterTable(ConPF, "TYPE", "CODE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPFCode = MasterNo
        Else
            mPFCode = -1
        End If

        mMonth = Month(CDate(lblRunDate.Text))
        mYear = Year(CDate(lblRunDate.Text))

        mDOJ = CDate(MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear)
        mDOL = CDate("01" & "/" & mMonth & "/" & mYear)

        ''AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "DD-MMM-YYYY") & "') OR EMP_LEAVE_DATE IS NULL)

        SqlStr = " Select MAX(SALARYDEF.SALARY_EFF_DATE), EMP.EMP_CODE, EMP.EMP_NAME, EMP.EMP_DOB, EMP.EMP_FNAME," & vbCrLf & " EMP.EMP_SEX, EMP.EMP_DOJ, EMP.EMP_PF_ACNO, EMP.EMP_DOB " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_SALARYDEF_MST SALARYDEF " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=SALARYDEF.COMPANY_CODE " & vbCrLf & " AND EMP.EMP_CODE=SALARYDEF.EMP_CODE " & vbCrLf & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=" & mPFCode & "" & vbCrLf & " AND EMP_STOP_SALARY='N' AND SALARYDEF.AMOUNT>0 " & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND EMP_DOJ >=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " GROUP BY " & vbCrLf & " EMP.EMP_CODE, EMP.EMP_NAME, EMP.EMP_DOB, EMP.EMP_FNAME, " & vbCrLf & " EMP.EMP_SEX, EMP.EMP_DOJ, EMP.EMP_PF_ACNO, EMP.EMP_DOB "

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            With sprdMain
                CntRow = 1
                Do While Not RsEmp.EOF
                    .Col = ColSNo
                    .Row = CntRow

                    .Text = CStr(CntRow)
                    CntRow = CntRow + 1

                    .Col = ColAccountNo
                    .Text = CStr(IIf(IsDbNull(RsEmp.Fields("EMP_PF_ACNO").Value), "", RsEmp.Fields("EMP_PF_ACNO").Value))

                    .Col = ColCode
                    .Text = IIf(IsDbNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value)

                    .Col = ColName
                    .Text = RsEmp.Fields("EMP_NAME").Value

                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsEmp.Fields("EMP_FNAME").Value), "", RsEmp.Fields("EMP_FNAME").Value)

                    .Col = ColAge
                    If Not IsDbNull(RsEmp.Fields("EMP_DOB").Value) Then
                        mDOB = IIf(IsDbNull(RsEmp.Fields("EMP_DOB").Value), "", RsEmp.Fields("EMP_DOB").Value)
                        mAge = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Month, mDOB, mDOJ) / 12) ''DateDiff("yyyy", mDOB, mDOJ)
                    Else
                        mAge = ""
                    End If

                    .Text = CStr(mAge)

                    .Col = ColSex
                    .Text = IIf(IsDbNull(RsEmp.Fields("EMP_SEX").Value), "MALE", IIf(RsEmp.Fields("EMP_SEX").Value = "M", "MALE", "FEMALE"))

                    .Col = ColDOM
                    .Text = IIf(IsDbNull(RsEmp.Fields("EMP_DOJ").Value), "", RsEmp.Fields("EMP_DOJ").Value)

                    .Col = ColDOB
                    .Text = IIf(IsDbNull(RsEmp.Fields("EMP_DOB").Value), "", RsEmp.Fields("EMP_DOB").Value)

                    If RsEmp.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If

                    RsEmp.MoveNext()
                Loop
                MainClass.ProtectCell(sprdMain, 0, .MaxRows, 0, .MaxCols)
            End With
        End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(lblYear.Text, "mm"), VB6.Format(lblYear.Text, "yyyy"))
    End Sub
End Class
