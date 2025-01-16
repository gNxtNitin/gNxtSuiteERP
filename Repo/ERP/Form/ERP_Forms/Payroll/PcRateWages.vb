Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPcRateWages
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection					

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColFName As Short = 3
    Private Const ColDepartment As Short = 4
    Private Const ColAmount As Short = 5
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim FileDBCn As ADODB.Connection

    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdMain)

        With sprdMain
            .MaxCols = ColAmount

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColCode
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColFName
            .Text = "Employees' Father Name "

            .Col = ColAmount
            .Text = "Amount"

            .Col = ColDepartment
            .Text = "Department"

            MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColName, ColFName)
        End With
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mCode As String
        Dim mAmount As Double

        Dim xMonth As String
        Dim mMonthDate As String
        Dim mFYEAR As Integer


        SqlStr = ""
        xMonth = UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY"))
        mMonthDate = "01-" & xMonth
        mFYEAR = GetCurrentFYNo(PubDBCn, VB6.Format(mMonthDate, "DD/MM/YYYY"))

        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM PAY_PCRATE_WAGES_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(SAL_Month,'MON-YYYY')='" & xMonth & "'"

        PubDBCn.Execute(SqlStr)

        For cntRow = 1 To sprdMain.MaxRows

            sprdMain.Row = cntRow
            sprdMain.Col = ColCode
            mCode = sprdMain.Text


            sprdMain.Col = ColAmount
            mAmount = CDbl(VB6.Format(Val(sprdMain.Text), "0.00"))

            If mAmount > 0 And mCode <> "" Then

                SqlStr = ""

                SqlStr = "INSERT INTO PAY_PCRATE_WAGES_TRN ( " & vbCrLf _
                    & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                    & " EMP_CODE, SAL_MONTH, AMOUNT) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mFYEAR & ", " & vbCrLf _
                    & " '" & mCode & "', TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mAmount & ") "

                PubDBCn.Execute(SqlStr)

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
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        Dim mYM As Integer

        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        If SalProcess(mYM) = True Then
            MsgBox("You are enable to process. ", MsgBoxStyle.Critical)
            Exit Sub
        End If


        If Update1() = True Then
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
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        PubDBCn.Errors.Clear()


        '''''Insert Data from Grid to PrintDummyData Table...					


        If MainClass.FillPrintDummyDataFromSprd(sprdMain, 1, sprdMain.MaxRows - 1, ColCode, sprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1



        '''''Select Record for print...					

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "")

        mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        mTitle = "Pc Rate Register"

        Call ShowReport(SqlStr, "PcRateWages.Rpt", Mode, mTitle, mSubTitle)

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
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        '    If chkContractor.Value = Unchecked Then					
        '        mCompanyName = cboConName.Text & " C/o " & RsCompany!COMPANY_NAME					
        '        mCompanyName = UCase(mCompanyName)					
        '    End If					

        'MainClass.AssignCRptFormulas(Report1, "CompanyName=""" & mCompanyName & """")
        'MainClass.AssignCRptFormulas(Report1, "CompanyAddress=")
        'MainClass.AssignCRptFormulas(Report1, "CompanyBotLine1=")
        'MainClass.AssignCRptFormulas(Report1, "CompanyBotLine2=")

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
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        MainClass.ClearGrid(sprdMain)

        RefreshScreen()
        PrintStatus(True)
    End Sub
    Private Sub frmPcRateWages_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'Me.Text = "Others Wages"
    End Sub
    Private Sub frmPcRateWages_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        'Me.Top = 0
        'Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        lblRunDate.Text = CStr(RunDate)

        FillHeading()
        FormatSprd(-1)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume					
    End Sub

    Private Sub frmPcRateWages_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mIsEmpReg As String
        Dim mRefDate As String
        Dim mLastDay As Integer

        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        mRefDate = VB6.Format(mLastDay & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

        If eventArgs.row = 0 And eventArgs.col = ColCode Then
            With sprdMain
                .Row = .ActiveRow

                .Col = ColCode

                SqlStr = " SELECT EMP_NAME, EMP_CODE, EMP_FNAME " & vbCrLf _
                    & " FROM PAY_EMPLOYEE_MST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_RATE_TYPE = 'P'"

                SqlStr = SqlStr & vbCrLf _
                    & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


                SqlStr = SqlStr & vbCrLf & " ORDER BY 1"
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColCode
                    .Text = Trim(AcName1)

                    .Col = ColName
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(sprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColCode, .ActiveRow, ColCode, .ActiveRow, False))
            End With
        End If


        If eventArgs.col = 0 And eventArgs.row > 0 Then
            sprdMain.Row = eventArgs.row
            sprdMain.Col = ColCode
            If eventArgs.row < sprdMain.MaxRows Then 'And (ADDMode = True Or MODIFYMode = True)					
                MainClass.DeleteSprdRow(sprdMain, eventArgs.row, ColCode)
                '            MainClass.SaveStatus Me, ADDMode, MODIFYMode					
                FormatSprd(eventArgs.row)
            End If
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles sprdMain.KeyUpEvent
        Dim mCol As Short
        Dim mRow As Short

        mCol = sprdMain.ActiveCol
        mRow = sprdMain.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColCode Then SprdMain_ClickEvent(sprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColCode, 0))

        sprdMain.Refresh()
    End Sub


    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.newRow = -1 Then Exit Sub
        sprdMain.Row = sprdMain.ActiveRow
        sprdMain.Col = ColCode

        If Trim(sprdMain.Text) = "" Then Exit Sub
        sprdMain.Text = VB6.Format(sprdMain.Text, "000000")
        Select Case eventArgs.col
            Case ColCode
                sprdMain.Row = sprdMain.ActiveRow

                sprdMain.Col = ColCode
                If DuplicateItem() = False Then
                    sprdMain.Row = sprdMain.ActiveRow
                    sprdMain.Col = ColCode
                    If FillGridPart(Trim(sprdMain.Text), (sprdMain.ActiveRow)) = False Then
                        MainClass.SetFocusToCell(sprdMain, sprdMain.ActiveRow, ColCode)
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        MainClass.AddBlankSprdRow(sprdMain, ColCode, ConRowHeight)
                        FormatSprd((sprdMain.MaxRows))
                    End If
                Else
                    MainClass.SetFocusToCell(sprdMain, sprdMain.ActiveRow, ColCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
                '        Case ColAmount					
                '            Call CheckAmount					
        End Select
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function FillGridPart(ByRef pOperatorCode As String, ByRef pRow As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDeptCode As String
        Dim mDeptDesc As String

        FillGridPart = False
        If Trim(pOperatorCode) = "" Then Exit Function

        SqlStr = "SELECT EMP_CODE, EMP_NAME, EMP_FNAME, EMP_DEPT_CODE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pOperatorCode) & "' AND EMP_RATE_TYPE='P'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            With sprdMain
                .Row = pRow '.ActiveRow					

                .Col = ColCode
                .Text = Trim(IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value))

                .Col = ColName
                .Text = IIf(IsDBNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)

                .Col = ColFName
                .Text = IIf(IsDBNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value)

                mDeptCode = IIf(IsDBNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)
                mDeptDesc = ""
                If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptDesc = MasterNo
                End If

                .Col = ColDepartment
                .Text = mDeptDesc

                FillGridPart = True
            End With
        Else
            MsgInformation("Invalid Employee Code. Please check.")
        End If
        '    FillGridPart = True					
        Exit Function
ERR1:
        FillGridPart = False
        MsgInformation(Err.Description)
    End Function

    Private Function DuplicateItem() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckOperatorCode As String
        Dim mOperatorCode As String

        With sprdMain
            .Row = .ActiveRow
            .Col = ColCode
            mCheckOperatorCode = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mOperatorCode = Trim(UCase(.Text))

                If mCheckOperatorCode = mOperatorCode Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Operator : " & mCheckOperatorCode)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Sub RefreshScreen()

        On Error GoTo ErrRefreshScreen

        Dim RsEmpSal As ADODB.Recordset
        Dim cntRow As Integer
        Dim mCode As String

        SqlStr = " SELECT EMP_CODE, AMOUNT " & vbCrLf _
            & " FROM PAY_PCRATE_WAGES_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(SAL_MONTH,'YYYYMM')='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "'"

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

                    .Col = ColAmount
                    .Text = VB6.Format(IIf(IsDBNull(RsEmpSal.Fields("Amount").Value), 0, RsEmpSal.Fields("Amount").Value), "0.00")


                    If FillGridPart(Trim(mCode), cntRow) = False Then GoTo NextRow
NextRow:
                    cntRow = cntRow + 1
                    RsEmpSal.MoveNext()
                    .MaxRows = .MaxRows + 1
                Loop

                FormatSprd(-1)

                MainClass.ProtectCell(sprdMain, .MaxRows, .MaxRows, ColName, ColFName)
            End With
        End If
        cmdSave.Enabled = True
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
        '    Resume					
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        Dim cntCol As Integer

        On Error GoTo ERR1
        With sprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .set_ColWidth(ColSNo, 4)


            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCode, 10)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 30)

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColFName, 30)
            .ColHidden = True

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmount, 12)

            .Col = ColDepartment
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDepartment, 20)



        End With
        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColName, ColDepartment)
        MainClass.SetSpreadColor(sprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function SalProcess(ByRef mYM As Integer) As Boolean

        On Error GoTo ErrSalProcess
        Dim RsMain As ADODB.Recordset
        SalProcess = False

        SqlStr = " SELECT EMP_CODE FROM PAY_CONT_VAR_TRN WHERE " & vbCrLf _
            & " TO_CHAR(ATTN_MONTH,'YYYYMM') >= " & mYM & "" & vbCrLf _
            & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockOptimistic)

        If RsMain.EOF = False Then
            SalProcess = True
            Exit Function
        End If

        Exit Function
ErrSalProcess:
        SalProcess = False
    End Function

    Private Sub cmdPopulate_Click(sender As Object, e As EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrRefreshScreen

        Dim RsEmpSal As ADODB.Recordset
        Dim cntRow As Integer
        Dim mCode As String

        MainClass.ClearGrid(sprdMain)

        'SqlStr = " SELECT EMP_CODE, AMOUNT " & vbCrLf _
        '    & " FROM PAY_PCRATE_WAGES_TRN WHERE " & vbCrLf _
        '    & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND TO_CHAR(SAL_MONTH,'YYYYMM')='" & VB6.Format(lblRunDate.Text, "YYYYMM") & "'"

        SqlStr = " SELECT  EMP_CODE, 0 AMOUNT " & vbCrLf _
                    & " FROM PAY_EMPLOYEE_MST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_RATE_TYPE = 'P'"

        SqlStr = SqlStr & vbCrLf _
                    & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        SqlStr = SqlStr & vbCrLf & " ORDER BY 1"

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

                    .Col = ColAmount
                    .Text = VB6.Format(IIf(IsDBNull(RsEmpSal.Fields("Amount").Value), 0, RsEmpSal.Fields("Amount").Value), "0.00")


                    If FillGridPart(Trim(mCode), cntRow) = False Then GoTo NextRow
NextRow:
                    cntRow = cntRow + 1
                    RsEmpSal.MoveNext()
                    .MaxRows = .MaxRows + 1
                Loop

                FormatSprd(-1)

                MainClass.ProtectCell(sprdMain, .MaxRows, .MaxRows, ColName, ColFName)
            End With
        End If
        cmdSave.Enabled = True
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
        '    Resume					
    End Sub
End Class
