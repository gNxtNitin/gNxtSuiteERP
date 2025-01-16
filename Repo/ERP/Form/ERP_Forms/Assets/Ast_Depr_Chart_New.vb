Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAstDeprChartMstNew
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection						

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColLifeYear As Short = 3
    Private Const ColLifeDays As Short = 4

    Private Sub FillHeading()
        Dim RsTemp As ADODB.Recordset
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdMain)

        With sprdMain
            .MaxCols = ColLifeDays

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Col = ColCode
            .Text = "Code"

            .Col = ColName
            .Text = "Name "

            .Col = ColLifeYear
            .Text = "Usefull Year "

            .Col = ColLifeDays
            .Text = "Usefull Days "

            .ColsFrozen = ColName

            SqlStr = " SELECT MODE_CODE " & vbCrLf & " FROM AST_DEPRECIATION_MODE_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            SqlStr = SqlStr & vbCrLf & " ORDER BY MODE_CODE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                .MaxCols = .MaxCols + 1
                cntCol = 1
                Do While Not RsTemp.EOF
                    .Col = ColLifeDays + cntCol
                    .Text = RsTemp.Fields("MODE_CODE").Value
                    cntCol = cntCol + 1
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        .MaxCols = .MaxCols + 1
                    End If
                Loop
            End If
            MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, 0, ColName)
        End With
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mModeCode As String
        Dim mModePer As Double
        Dim mYear As Integer

        Dim mLifeYear As Double
        Dim mLifeDays As Double

        SqlStr = ""
        mYear = Val(lblYear.Text)

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM AST_DEPRECIATION_NEW_MST WHERE" & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & mYear & ""

        PubDBCn.Execute(SqlStr)

        With sprdMain
            For cntRow = 1 To sprdMain.MaxRows
                .Col = ColCode
                .Row = cntRow
                mCode = Trim(.Text)

                .Col = ColLifeYear
                mLifeYear = Val(.Text)

                .Col = ColLifeDays
                mLifeDays = Val(.Text)

                If Trim(mCode) <> "" Then
                    For cntCol = ColLifeDays + 1 To sprdMain.MaxCols
                        .Row = 0
                        .Col = cntCol
                        mModeCode = Trim(.Text)

                        .Row = cntRow
                        .Col = cntCol
                        mModePer = Val(.Text)

                        SqlStr = "INSERT INTO AST_DEPRECIATION_NEW_MST (" & vbCrLf _
                            & " COMPANY_CODE, FYEAR, GROUP_CODE, ASSETS_LIFE_YEAR, ASSETS_LIFE_DAYS," & vbCrLf _
                            & " DEPR_RATE, MODE_CODE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES (" & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mYear & ", '" & mCode & "', " & mLifeYear & ", " & mLifeDays & "," & vbCrLf _
                            & " " & mModePer & ", '" & mModeCode & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


                        PubDBCn.Execute(SqlStr)
                    Next
                End If
            Next
        End With

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
        Call ReportForChart(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForChart(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String


        PubDBCn.Errors.Clear()

        If MainClass.FillPrintDummyDataFromSprd(sprdMain, 0, sprdMain.MaxRows, ColCode, sprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        '''''Select Record for print...						

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mSubTitle = "For the Year : " & lblRunDate.Text
        mTitle = "Asset Depreciation Chart"

        Call ShowReport(SqlStr, "AstDeprChart.Rpt", Mode, mTitle, mSubTitle)

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
        Call ReportForChart(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        Dim mDate As String

        mDate = lblYear.Value '' VB6.Format(lblYear.Text, "DD/MM/YYYY")
        SetDate(CDate(mDate))
        MainClass.ClearGrid(sprdMain)

        RefreshScreen()
    End Sub
    Private Sub frmAstDeprChartMstNew_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'Me.Text = "Asset Depreciation New Chart Master"
    End Sub
    Private Sub frmAstDeprChartMstNew_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        'lblRunDate.Text = CStr(RunDate)
        'SetDate(CDate(lblRunDate.Text))

        lblRunDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        ''lblYear.Text = VB6.Format(RunDate, "YYYY")
        SetDate(CDate(RunDate))
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

        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mYear As Short

        mYear = Year(CDate(lblRunDate.Text))

        SqlStr = " SELECT CODE, NAME " & vbCrLf & " FROM FIN_INVTYPE_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' " & vbCrLf & " AND ISFIXASSETS='Y' "

        SqlStr = SqlStr & vbCrLf & "Order by CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            With sprdMain
                cntRow = 1
                .MaxRows = cntRow
                Do While Not RsTemp.EOF
                    .Row = cntRow

                    .Col = ColCode
                    mCode = RsTemp.Fields("CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsTemp.Fields("Name").Value


                    If ShowDet1(mCode, cntRow, mYear) = False Then GoTo NextRow

NextRow:
                    cntRow = cntRow + 1
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop

                FormatSprd(-1)
            End With
        End If
        cmdSave.Enabled = True
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
    End Sub
    Private Function ShowDet1(ByRef mCode As String, ByRef cntRow As Integer, ByRef mYear As Short) As Boolean
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim cntCol As Integer
        Dim pModeCode As String


        With sprdMain
            For cntCol = ColLifeDays + 1 To .MaxCols
                .Row = 0
                .Col = cntCol
                pModeCode = Trim(.Text)

                SqlStr = " SELECT DEPR_RATE,ASSETS_LIFE_YEAR, ASSETS_LIFE_DAYS " & vbCrLf _
                    & " FROM AST_DEPRECIATION_NEW_MST WHERE " & vbCrLf _
                    & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND FYEAR=" & mYear & " " & vbCrLf _
                    & " AND GROUP_CODE='" & mCode & "' " & vbCrLf _
                    & " AND MODE_CODE='" & pModeCode & "' "


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                If RsTemp.EOF = False Then
                    .Row = cntRow
                    .Col = ColLifeYear
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ASSETS_LIFE_YEAR").Value), 0, RsTemp.Fields("ASSETS_LIFE_YEAR").Value), "0.00")

                    .Col = ColLifeDays
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ASSETS_LIFE_DAYS").Value), 0, RsTemp.Fields("ASSETS_LIFE_DAYS").Value), "0.00")

                    .Col = cntCol
                    .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DEPR_RATE").Value), 0, RsTemp.Fields("DEPR_RATE").Value), "0.00")
                Else
                    .Row = cntRow
                    .Col = cntCol
                    .Text = "0.00"
                End If
            Next
        End With
        ShowDet1 = True
        Exit Function

ErrPart:
        MsgInformation(Err.Description)
        ShowDet1 = False
    End Function
    Private Sub SetDate(ByRef xDate As Date)
        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Value = NewDate      ''CStr(Year(NewDate)) ''VB6.Format(lblRunDate.Text, "YYYY")

        ''lblYear.Text = CStr(Year(NewDate))

        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)
        Dim cntCol As Integer

        On Error GoTo ERR1
        With sprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)


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
            .set_ColWidth(ColName, 35)

            .Col = ColLifeYear
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColLifeYear, 6)

            .Col = ColLifeDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColLifeDays, 6)

            For cntCol = ColLifeDays + 1 To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 6)
            Next

        End With
        MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, 0, ColName)
        MainClass.SetSpreadColor(sprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub lblYear_Click(sender As Object, e As EventArgs) Handles lblYear.Click
        lblRunDate.Text = lblYear.Value
    End Sub
End Class
