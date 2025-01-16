Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmOpeningPerks
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
    Private Const ColDesg As Short = 3
    Private Const mBookType As String = "O"
    Private Const mDC As String = "C"
    Private Function CalcVariable(ByRef mCode As String, ByRef mRow As Integer) As Boolean

        On Error GoTo ERR1
        Dim RSSalVar As ADODB.Recordset
        Dim cntCol As Integer
        Dim mHeadTitle As String

        CalcVariable = True
        SqlStr = " SELECT TRN.*, " & vbCrLf & " ADD_DEDUCT.NAME, " & vbCrLf & " ADD_DEDUCT.ADDDEDUCT, ADD_DEDUCT.CALC_ON, ADD_DEDUCT.TYPE, " & vbCrLf & " ADD_DEDUCT.SEQ,DEFAULT_AMT " & vbCrLf & " FROM PAY_PERKS_TRN TRN, PAY_SALARYHEAD_MST ADD_DEDUCT " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND TRN.EMP_Code='" & mCode & "' AND BOOKTYPE='" & mBookType & "'" & vbCrLf & " ORDER BY ADD_DEDUCT.SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalVar, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalVar.EOF = False Then
            Do While Not RSSalVar.EOF
                sprdMain.Col = ColDesg
                sprdMain.Row = mRow
                sprdMain.Text = ""

                For cntCol = ColDesg + 1 To sprdMain.MaxCols
                    sprdMain.Col = cntCol
                    sprdMain.Row = 0
                    If Trim(UCase(RSSalVar.Fields("Name").Value)) = Trim(UCase(sprdMain.Text)) Then
                        sprdMain.Row = mRow
                        sprdMain.Text = MainClass.FormatRupees(IIf(IsDbNull(RSSalVar.Fields("Amount").Value), 0, RSSalVar.Fields("Amount").Value))
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

        MainClass.ClearGrid(sprdMain)

        With sprdMain
            .MaxCols = ColDesg

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name "
            .ColsFrozen = ColName

            .Col = ColDesg
            .Text = "Designation"

            SqlStr = " SELECT NAME,ADDDEDUCT " & vbCrLf & " FROM PAY_SALARYHEAD_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT=" & ConPerks & " AND PAYMENT_TYPE='M'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ADDDEDUCT,SEQ "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                .MaxCols = .MaxCols + 1
                cntCol = 1
                Do While Not RsTemp.EOF
                    .Col = ColDesg + cntCol
                    .Text = RsTemp.Fields("Name").Value
                    cntCol = cntCol + 1
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        .MaxCols = .MaxCols + 1
                    End If
                Loop
            End If
            MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, 0, ColDesg)
        End With
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mCode As String
        Dim SqlStr As String = ""

        SqlStr = ""
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM PAY_PERKS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BOOKTYPE='" & mBookType & "'"

        PubDBCn.Execute(SqlStr)

        For cntRow = 1 To sprdMain.MaxRows
            sprdMain.Col = ColCode
            sprdMain.Row = cntRow
            mCode = sprdMain.Text

            If mCode <> "" Then
                If UpdateMonthTrn(mCode, cntRow) = False Then GoTo UpdateError
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
    Private Function UpdateMonthTrn(ByRef xCode As String, ByRef xRow As Integer) As Boolean


        On Error GoTo UpdateMonthTrnErr
        Dim SqlStr As String = ""
        Dim cntCol As Integer
        Dim xMonth As String
        Dim xTypeCode As Integer
        Dim xAmount As Double
        Dim mMonthDate As String
        Dim RsTemp As ADODB.Recordset = Nothing

        xMonth = UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY"))
        mMonthDate = "01-" & xMonth

        SqlStr = ""

        For cntCol = ColDesg + 1 To sprdMain.MaxCols
            sprdMain.Row = 0
            sprdMain.Col = cntCol

            SqlStr = " SELECT CODE,TYPE FROM PAY_SALARYHEAD_MST" & vbCrLf & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND NAME = '" & MainClass.AllowSingleQuote(Trim(sprdMain.Text)) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                xTypeCode = RsTemp.Fields("Code").Value

                sprdMain.Row = xRow
                xAmount = CDbl(IIf(IsNumeric(sprdMain.Text), sprdMain.Text, 0))
            Else
                GoTo NextCol
            End If

            If Val(CStr(xAmount)) <> 0 Then
                SqlStr = " INSERT INTO PAY_PERKS_TRN ( " & vbCrLf & " COMPANY_CODE, SAL_DATE, " & vbCrLf & " EMP_CODE, ADD_DEDUCTCODE, AMOUNT,BOOKTYPE,DC, ADDUSER, ADDDATE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & mMonthDate & "'), " & vbCrLf & " '" & xCode & "', " & xTypeCode & ", " & xAmount & ",'" & mBookType & "','" & mDC & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

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

        Exit Sub

        '    If PubSuperUser = "U" Then
        '        mYM = Format(Year(lblRunDate.Caption), "0000") & vb6.Format(Month(lblRunDate.Caption), "00")
        '        If SalProcess(mYM) = False Then
        '            MsgBox "You are enable to process. ", vbCritical
        '            Exit Sub
        '        End If
        '    End If

        If Update1 = True Then
            Call cmdRefresh_Click(cmdRefresh, New System.EventArgs())
            cmdSave.Enabled = False
        Else
            cmdSave.Enabled = True
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
        Call ReportForDeduction(Crystal.DestinationConstants.crptToWindow)
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


        If FillPrintDummyData(sprdMain, 0, sprdMain.MaxRows, ColCode, sprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        mTitle = "Deduction List "
        If lblSalType.Text = "O" Then
            mTitle = mTitle & "(Over Time)"
        ElseIf lblSalType.Text = "E" Then
            mTitle = mTitle & "(Encashment)"
        ElseIf lblSalType.Text = "C" Then
            mTitle = mTitle & "(CPL)"
        End If

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
        Call ReportForDeduction(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        Dim mOPDate As String

        mOPDate = GetOpeningDate()
        If IsDate(mOPDate) Then
            lblRunDate.Text = mOPDate
            UpDYear.Enabled = False
        End If

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdMain)

        RefreshScreen()
    End Sub
    Private Function GetOpeningDate() As String

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        GetOpeningDate = ""
        mSqlStr = "SELECT MAX(SAl_DATE) AS SAl_DATE FROM PAY_PERKS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & mBookType & "'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetOpeningDate = IIf(IsDbNull(RsTemp.Fields("SAL_DATE").Value), "", RsTemp.Fields("SAL_DATE").Value)
        End If
        Exit Function
ErrPart:
        GetOpeningDate = ""
    End Function
    Private Sub frmOpeningPerks_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        Me.Text = "Opening Perks Payable"

    End Sub
    Private Sub frmOpeningPerks_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim mOPDate As String

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

        mOPDate = GetOpeningDate()

        If IsDate(mOPDate) Then
            lblRunDate.Text = mOPDate
            UpDYear.Enabled = False
        Else
            lblRunDate.Text = CStr(RunDate)
        End If

        OptName.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        FormatSprd(-1)
        UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

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
        MainClass.ClearGrid(sprdMain, -1)
        '' RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdMain, -1)
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

        SqlStr = " SELECT EMP_NAME, EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) " ''& vbCrLf |            & " AND EMP_STOP_SALARY='N'"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (" & vbCrLf & " SELECT DISTINCT EMP_CODE FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMOUNT>0" & vbCrLf & " AND ADD_DEDUCTCODE IN (" & vbCrLf & " SELECT CODE " & vbCrLf & " FROM PAY_SALARYHEAD_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT=" & ConPerks & " AND PAYMENT_TYPE='M'))"


        '    SqlStr = SqlStr & vbCrLf _
        ''                & " AND EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf _
        ''                & " PAY_FFSETTLE_HDR " & vbCrLf _
        ''                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "')"


        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP_CODE"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmpSal.EOF = False Then
            With sprdMain
                cntRow = 1
                .MaxRows = cntRow
                Do While Not RsEmpSal.EOF
                    .Row = cntRow

                    .Col = ColCode
                    mCode = RsEmpSal.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsEmpSal.Fields("EMP_NAME").Value

                    If CalcVariable(mCode, cntRow) = False Then GoTo NextRow

NextRow:
                    cntRow = cntRow + 1
                    RsEmpSal.MoveNext()
                    If RsEmpSal.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop

                ColTotal(sprdMain, ColDesg + 1, .MaxCols)
                .Col = ColName
                .Row = .MaxRows
                .Text = "TOTAL :"

                FormatSprd(-1)

                MainClass.ProtectCell(sprdMain, .MaxRows, .MaxRows, 0, .MaxCols)
            End With
        End If
        cmdSave.Enabled = True
        Exit Sub

ErrRefreshScreen:
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
        With sprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)
            .set_RowHeight(0, ConRowHeight * 3)


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

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDesg, 8)
            .ColHidden = True

            For cntCol = ColDesg + 1 To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 12)
            Next

        End With
        MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, 0, ColDesg)
        MainClass.SetSpreadColor(sprdMain, mRow)
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


    Private Sub GetMaxDeductDate(ByRef xCode As String, ByRef xMKey As String, ByRef xDate As String, ByRef xSubRow As Integer)

        On Error GoTo ErrPart
        Dim RsBalLoan As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT Max(DEDUCT_DATE) AS DDate,Max(SUBROWNO) AS SUBROWNO" & vbCrLf & " FROM PAY_LOAN_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "' AND " & vbCrLf & " MKEY=" & xMKey & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalLoan, ADODB.LockTypeEnum.adLockOptimistic)

        If RsBalLoan.EOF = False Then
            xDate = IIf(IsDbNull(RsBalLoan.Fields("DDate").Value), "", RsBalLoan.Fields("DDate").Value)
            xSubRow = IIf(IsDbNull(RsBalLoan.Fields("SUBROWNO").Value), "-1", RsBalLoan.Fields("SUBROWNO").Value)
        End If
        Exit Sub
ErrPart:
        xDate = ""
        xSubRow = -1
    End Sub
End Class
