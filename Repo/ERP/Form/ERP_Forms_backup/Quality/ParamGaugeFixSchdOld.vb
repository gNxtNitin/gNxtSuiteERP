Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamGaugeFixSchdOld
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColDocNo As Short = 1
    Private Const ColDesc As Short = 2
    Private Const ColTypeNo As Short = 3
    Private Const ColDay1 As Short = 4
    Private Const ColDay2 As Short = 5
    Private Const ColDay3 As Short = 6
    Private Const ColDay4 As Short = 7
    Private Const ColDay5 As Short = 8
    Private Const ColDay6 As Short = 9
    Private Const ColDay7 As Short = 10
    Private Const ColDay8 As Short = 11
    Private Const ColDay9 As Short = 12
    Private Const ColDay10 As Short = 13
    Private Const ColDay11 As Short = 14
    Private Const ColDay12 As Short = 15
    Private Const ColDay13 As Short = 16
    Private Const ColDay14 As Short = 17
    Private Const ColDay15 As Short = 18
    Private Const ColDay16 As Short = 19
    Private Const ColDay17 As Short = 20
    Private Const ColDay18 As Short = 21
    Private Const ColDay19 As Short = 22
    Private Const ColDay20 As Short = 23
    Private Const ColDay21 As Short = 24
    Private Const ColDay22 As Short = 25
    Private Const ColDay23 As Short = 26
    Private Const ColDay24 As Short = 27
    Private Const ColDay25 As Short = 28
    Private Const ColDay26 As Short = 29
    Private Const ColDay27 As Short = 30
    Private Const ColDay28 As Short = 31
    Private Const ColDay29 As Short = 32
    Private Const ColDay30 As Short = 33
    Private Const ColDay31 As Short = 34
    Private Const ColType As Short = 35

    Private Const ClrWhite As Integer = &H80000005
    Private Const ClrBlack As Short = 0

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        cmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMonth.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboMonth_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMonth.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboYear.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboYear_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub chkAllLocation_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllLocation.CheckStateChanged
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtLocation.Enabled = False
            cmdSearchLocation.Enabled = False
        Else
            txtLocation.Enabled = True
            cmdSearchLocation.Enabled = True
        End If
    End Sub

    Private Sub chkWithActual_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkWithActual.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdExport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExport.Click


        FraPreview.Visible = True
        FraPreview.BringToFront()
        '    With SprdMain
        '        .Col = ColPic
        '        .ColHidden = True
        '        .ColWidth(ColDesc) = 27 + 15
        '        .ColWidth(ColSchd) = 4
        '        .ColWidth(ColCurrSubTotal) = 12
        '        .ColWidth(ColCurrTotal) = 12
        '        .ColWidth(ColPrevSubTotal) = 12
        '        .ColWidth(ColPrevTotal) = 12
        '    End With

        '    If UCase(lblType.text) = UCase("Balance Sheet") Then
        '        SprdMain.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Balance Sheet as on " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    ElseIf UCase(lblType.text) = UCase("Fund Flow") Then
        '        SprdMain.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Fund Flow as on " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    Else
        '        SprdMain.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Profit & Loss A//c as on " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    End If
        Call SpreadSheetPreview(SprdMain, SprdPreview, SprdCommand, ClientRectangle.Width - 450, ClientRectangle.Height - 450)
    End Sub

    Private Sub SprdCommand_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdCommand.ButtonClicked
        On Error GoTo ERR1
        Dim mFilename As String

        SprdCommand.Col = eventArgs.col
        SprdCommand.Row = eventArgs.row

        If SprdCommand.CellType = FPSpreadADO.CellTypeConstants.CellTypeButton Then
            Select Case eventArgs.col
                Case 2 'Next
                    ShowNextPage(SprdMain, SprdPreview, SprdCommand, eventArgs.col)

                Case 4 'Previous
                    ShowPreviousPage(SprdMain, SprdPreview, SprdCommand, eventArgs.col)

                Case 6 'Zoom
                    SprdPreview.ZoomState = 3

                Case 8 'Print
                    PrintSpread() ''cmdPrint_Click

                Case 10 'Export
                    'mFilename = ExportSprdToExcel(CommonDialog1)

                    '                If SprdMain.ExportToExcelEx(mFilename, "AttnSheet", "a.txt", ExcelSaveFlagNone) = True Then
                    If SprdMain.ExportToExcel(mFilename, "GaugeFixSchd", "") = True Then
                        '                If SprdMain.ExportExcelBook(mFilename, "") = True Then
                        MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name is " & mFilename)
                    End If

                Case 16 'Close
                    FraPreview.Visible = False
                    '                With SprdMain
                    '                     .Col = ColPic
                    '                    .ColHidden = False
                    '                    .ColWidth(ColDesc) = 30
                    '                    .ColWidth(ColSchd) = 4
                    '                    .ColWidth(ColCurrSubTotal) = 12
                    '                    .ColWidth(ColCurrTotal) = 12
                    '                    .ColWidth(ColPrevSubTotal) = 12
                    '                    .ColWidth(ColPrevSubTotal) = 12
                    '                End With
            End Select
        End If
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            Exit Sub
        End If
        MsgInformation(Err.Description)
    End Sub
    Sub PrintSpread()
        'Set printing options for spreadsheet
        CommonDialog1Print.ShowDialog()
        SprdMain.PrintBorder = True
        SprdMain.PrintOrientation = FPSpreadADO.PrintOrientationConstants.PrintOrientationLandscape
        SprdMain.PrintColHeaders = True
        SprdMain.PrintRowHeaders = False
        SprdMain.PrintBorder = True
        SprdMain.PrintColor = True

        SprdMain.PrintShadows = True
        SprdMain.PrintGrid = True
        SprdMain.PrintUseDataMax = True
        SprdMain.PrintCenterOnPageH = False
        SprdMain.PrintCenterOnPageV = False

        '    SprdMain.

        'Page Range
        'All
        '    If Option1(0).Value = True Then
        SprdMain.PrintType = FPSpreadADO.PrintTypeConstants.PrintTypeAll

        '    'Selected cells
        '    ElseIf Option1(1).Value = True Then
        '        SprdMain.Col = SprdMain.SelBlockCol
        '        SprdMain.col2 = SprdMain.SelBlockCol2
        '        SprdMain.Row = SprdMain.SelBlockRow
        '        SprdMain.Row2 = SprdMain.SelBlockRow2
        '        SprdMain.PrintType = PrintTypeCellRange
        '
        '    'Current Page
        '    ElseIf Option1(2).Value = True Then
        '        SprdMain.PrintType = PrintTypeCurrentPage
        '
        '    'Pages
        '    Else
        '        SprdMain.PrintPageStart = CInt(Text1(0).Text)
        '        SprdMain.PrintPageEnd = CInt(Text1(1).Text)
        '        SprdMain.PrintType = PrintTypePageRange
        '    End If

        'Print control
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SprdMain.PrintSheet()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGaugeFixSchd(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGaugeFixSchd(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnGaugeFixSchd(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        If lblGaugeIMTE.Text = "G" Then
            mTitle = "Gauge Fixture Calibration Schedule"
        Else
            mTitle = "IMTE Calibration Schedule"
        End If

        mSubTitle = "[MONTH & YEAR : " & cboMonth.Text & " " & cboYear.Text & "]"
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) <> "" Then
            mSubTitle = mSubTitle & " [LOCATION : " & Trim(txtLocation.Text) & "]"
        End If

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr

        SqlStr = FetchRecordForReport()

        If optType(0).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GaugeFixSchd.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GaugeFixSchdMonth.rpt"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Function FillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer) As Boolean

        ''' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 2
            GridName.Row = RowNum
            GridName.Col = 1

            SetData = "FIELD1"
            GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
            For FieldNum = prmStartGridCol + 1 To prmEndGridCol
                GridName.Col = FieldNum
                SetData = SetData & ", " & "FIELD" & FieldCnt
                GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
NextRec:
        Next
        PubDBCn.CommitTrans()
        FillPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyData = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function FetchRecordForReport() As String

        Dim mSqlStr As String

        mSqlStr = " SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdSearchLocation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchLocation.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtLocation.Text, IIf(lblGaugeIMTE.text = "G", "QAL_GAUGEFIX_MST", "QAL_IMTE_MST"), "LOCATION", "", "", "", SqlStr) = True Then
            txtLocation.Text = AcName
        End If
        txtLocation.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        FormatSprdMain(-1)
        Call PrintStatus(True)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim I As Integer
        Dim mMaxCol As Integer

        mMaxCol = IIf(optType(0).Checked = True, ColDay31, ColDay12) + 1
        Call FillHeading(mMaxCol)

        With SprdMain
            .MaxCols = mMaxCol

            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.7)
            .Row = -1

            .Col = ColDocNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColTypeNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .ColsFrozen = ColTypeNo

            For I = ColTypeNo + 1 To mMaxCol - 1
                .Col = I ''ColTypeNo + I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .TypeEditLen = 255
                .set_ColWidth(I, IIf(optType(0).Checked = True, 3, 8))
                .TypeEditMultiLine = True
            Next

            .Col = mMaxCol
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .set_ColWidth(mMaxCol, 12)

            .Col = ColDocNo
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColDesc
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColTypeNo
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxRows)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        End With
    End Sub

    Private Sub FillHeading(ByRef pMaxCol As Integer)
        Dim I As Integer
        Dim mMaxCol As Integer
        Dim mMonthSerial As Integer

        With SprdMain
            For I = ColTypeNo + 1 To pMaxCol - 1
                .Row = 0
                If optType(0).Checked = True Then
                    .Col = I
                    .Text = CStr(I - 3)
                Else
                    mMonthSerial = I - 3
                    .Col = IIf(mMonthSerial < 4, I + 9, I - 3)
                    .Text = MonthName(I - 3)
                End If
            Next

            .Col = ColType
            .Text = "Type"

        End With
    End Sub
    Public Sub frmParamGaugeFixSchdOld_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblGaugeIMTE.Text = "G" Then
            Me.Text = "Gauge Fixture Calibration Schedule"
        Else
            Me.Text = "IMTE Calibration Schedule"
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        cboMonth.Text = MonthName(Month(RunDate))
        cboYear.Text = CStr(Year(RunDate))
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamGaugeFixSchdOld_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        SprdMain.Row = 1
        SprdMain.Col = 1
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(11565)

        Call FillCbo()

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()
        Dim I As Short
        cboMonth.Items.Clear()
        For I = 1 To 12
            cboMonth.Items.Add(MonthName(I))
        Next

        cboYear.Items.Clear()
        For I = 2000 To 2020
            cboYear.Items.Add(CStr(I))
        Next
        cboMonth.Enabled = True
    End Sub

    Private Sub frmParamGaugeFixSchdOld_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = IIf(VB6.PixelsToTwipsX(Me.Width) > 10, VB6.PixelsToTwipsX(Me.Width) - 10, VB6.PixelsToTwipsX(Me.Width))

        Frame1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11592.4, 763)
        SprdMain.Width = VB6.TwipsToPixelsX(VB6.FromPixelsUserWidth(Frame1.Width, 11592.4, 763) - 325) ' IIf(mReFormWidth > 300, mReFormWidth - 300, mReFormWidth)

        FraPreview.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11592.4, 763)
        SprdPreview.Width = VB6.TwipsToPixelsX(VB6.FromPixelsUserWidth(FraPreview.Width, 11592.4, 763)) '' IIf(mReFormWidth > 180, mReFormWidth - 180 - 400, mReFormWidth)

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamGaugeFixSchdOld_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mRsSchd As ADODB.Recordset
        Dim mRow As Integer
        Dim mMonthName As String
        Dim mMonthCol As Integer
        Dim mMaxCol As Integer
        Dim mValFrequency As Integer

        Dim mDoc As String
        Dim mDescription As String
        Dim mType As String
        Dim mVDoneDate As String
        Dim mVDone As Boolean
        Dim mMonthStartDate As String
        Dim mMonthEndDate As String

        Dim cntCol As Integer
        Dim I As Integer
        Dim mMonth As Short
        Dim mMonthDate As String
        Dim mDueOn As String
        Dim pMonthDueOn As String

        SprdMain.MaxCols = IIf(optType(0).Checked = True, ColDay31, ColDay12) + 1
        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsSchd, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsSchd.EOF = True Then
            Show1 = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgInformation("No Schedule is available for this Month & Year.")
            Exit Function
        End If

        With SprdMain
            mRow = 1
            Do While Not mRsSchd.EOF
                .MaxRows = mRow
                .Row = mRow

                .Col = ColDocNo
                .Text = IIf(IsDbNull(mRsSchd.Fields("DOCNO").Value), "", CStr(mRsSchd.Fields("DOCNO").Value))
                mDoc = IIf(IsDbNull(mRsSchd.Fields("DOCNO").Value), "", CStr(mRsSchd.Fields("DOCNO").Value))

                .Col = ColDesc
                .Text = IIf(IsDbNull(mRsSchd.Fields("Description").Value), "", mRsSchd.Fields("Description").Value)
                mDescription = IIf(IsDbNull(mRsSchd.Fields("Description").Value), "", mRsSchd.Fields("Description").Value)

                .Col = ColType
                .Text = "PLAN"

                .Col = ColTypeNo
                If lblGaugeIMTE.Text = "G" Then
                    .Text = IIf(IsDbNull(mRsSchd.Fields("TYPENO").Value), "", mRsSchd.Fields("TYPENO").Value)
                    mType = IIf(IsDbNull(mRsSchd.Fields("TYPENO").Value), "", mRsSchd.Fields("TYPENO").Value)
                Else
                    .Text = IIf(IsDbNull(mRsSchd.Fields("E_NO").Value), "", mRsSchd.Fields("E_NO").Value)
                    mType = IIf(IsDbNull(mRsSchd.Fields("E_NO").Value), "", mRsSchd.Fields("E_NO").Value)
                End If

                mValFrequency = IIf(IsDbNull(mRsSchd.Fields("ValFrequency").Value), 1, mRsSchd.Fields("ValFrequency").Value)
                mValFrequency = Int(IIf(mValFrequency = 0, 1, mValFrequency))

                If optType(0).Checked = True Then
                    If lblGaugeIMTE.Text = "G" Then
                        .Col = VB.Day(mRsSchd.Fields("VDUEON").Value) + 3
                    Else
                        .Col = VB.Day(mRsSchd.Fields("CDate").Value) + 3
                    End If
                Else
                    If lblGaugeIMTE.Text = "G" Then
                        mMonthName = Trim(mRsSchd.Fields("VDUEON").Value)
                        mDueOn = Trim(mRsSchd.Fields("VDUEON1").Value)
                    Else
                        mMonthName = Trim(mRsSchd.Fields("CDate").Value)
                        mDueOn = Trim(mRsSchd.Fields("CDate1").Value)
                    End If
                    mMonthCol = MonthValue(mMonthName)
                    mMonthCol = IIf(mMonthCol > 3, mMonthCol - 3, mMonthCol + 3)
                    .Col = mMonthCol + 3
                End If
                I = 1
                If optType(0).Checked = True Then
                    .Text = "Y"
                    .BackColor = System.Drawing.ColorTranslator.FromOle(ClrBlack)
                ElseIf optType(1).Checked = True Then
                    For cntCol = ColDay1 To ColDay12
                        '                    .Row = 0
                        '                    .Col = cntCol

                        If I <= 9 Then
                            mMonth = I + 3
                            mMonthDate = MainClass.LastDay(mMonth, Year(RsCompany.Fields("START_DATE").Value)) & "/" & mMonth & "/" & Year(RsCompany.Fields("START_DATE").Value)
                        Else
                            mMonth = I - 9
                            mMonthDate = MainClass.LastDay(mMonth, Year(RsCompany.Fields("END_DATE").Value)) & "/" & mMonth & "/" & Year(RsCompany.Fields("END_DATE").Value)
                        End If

                        '                    If cntCol <= 12 Then
                        '                        mMonth = cntCol - 3
                        '                        mMonthDate = MainClass.LastDay(mMonth, Year(RsCompany!START_DATE)) & "/" & mMonth & "/" & Year(RsCompany!START_DATE)
                        '                    Else
                        '                        mMonth = cntCol - 12
                        '                        mMonthDate = MainClass.LastDay(mMonth, Year(RsCompany!END_DATE)) & "/" & mMonth & "/" & Year(RsCompany!END_DATE)
                        '                    End If
                        mMonthDate = VB6.Format(mMonthDate, "DD/MM/YYYY")

                        If CheckScheduleMonth(mMonthDate, mDueOn, mValFrequency, pMonthDueOn) = True Then
                            .Row = mRow
                            .Col = cntCol
                            .Text = "Y"
                            .BackColor = System.Drawing.ColorTranslator.FromOle(ClrBlack)
                        End If
                        I = I + 1
                    Next
                End If

                If lblGaugeIMTE.Text = "G" And chkWithActual.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mRow = mRow + 1
                    .MaxRows = mRow
                    .Row = mRow

                    .Col = ColDocNo
                    .Text = mDoc

                    .Col = ColDesc
                    .Text = mDescription

                    .Col = ColTypeNo
                    .Text = mType

                    .Col = ColType
                    .Text = "ACTUAL"

                    mVDoneDate = IIf(IsDbNull(mRsSchd.Fields("VDONEON1").Value), "", mRsSchd.Fields("VDONEON1").Value)
                    mVDone = IIf(mVDoneDate = "", False, True)

                    I = 1
                    '                If optType(1).Value = True Then
                    For cntCol = ColDay1 To IIf(optType(1).Checked = True, ColDay12, ColDay31)

                        If optType(1).Checked = True Then
                            If I <= 9 Then
                                mMonth = I + 3
                                mMonthDate = MainClass.LastDay(mMonth, Year(RsCompany.Fields("START_DATE").Value)) & "/" & mMonth & "/" & Year(RsCompany.Fields("START_DATE").Value)
                            Else
                                mMonth = I - 9
                                mMonthDate = MainClass.LastDay(mMonth, Year(RsCompany.Fields("END_DATE").Value)) & "/" & mMonth & "/" & Year(RsCompany.Fields("END_DATE").Value)
                            End If
                        Else
                            If I <= MainClass.LastDay(MonthValue((cboMonth.Text)), Val(cboYear.Text)) Then
                                mMonthDate = I & "/" & MonthValue((cboMonth.Text)) & "/" & Val(cboYear.Text)
                            End If
                        End If
                        mMonthDate = VB6.Format(mMonthDate, "DD/MM/YYYY")

                        If CheckActual(mDoc, mMonthDate) = True Then
                            .Row = mRow
                            .Col = cntCol
                            .Text = "A"
                            .ForeColor = System.Drawing.Color.Lime
                            .BackColor = System.Drawing.Color.Lime
                        End If
                        I = I + 1
                    Next
                    '                End If

                    '                If optType(0).Value = True Then
                    '                    mMonthStartDate = "01/" & vb6.Format(cboMonth.ListIndex + 1, "00") & "/" & vb6.Format(cboYear.Text, "0000")
                    '                    mMonthEndDate = MainClass.LastDay(Format(cboMonth.ListIndex + 1, "00"), Format(cboYear.Text, "0000")) & "/" & vb6.Format(cboMonth.ListIndex + 1, "00") & "/" & vb6.Format(cboYear.Text, "0000")
                    '
                    '                    If CDate(mVDoneDate) >= CDate(mMonthStartDate) And CDate(mVDoneDate) <= CDate(mMonthEndDate) Then
                    '                        .Col = Day(mRsSchd!VDONEON) + 3
                    '                        mVDone = True
                    '                    Else
                    '                        mVDone = False
                    '                    End If
                    '                Else
                    '
                    ''                    CheckScheduleMonth(mEndDate As String, mDueOn As String, mFrequency As Double, pMonthDueOn)
                    '
                    ''                    If CDate(mVDoneDate) >= CDate(RsCompany!START_DATE) And CDate(mVDoneDate) <= CDate(RsCompany!END_DATE) Then
                    ''                        mMonthName = Trim(mRsSchd!VDONEON)
                    ''                        mMonthCol = MonthValue(mMonthName)
                    ''                        mMonthCol = IIf(mMonthCol > 3, mMonthCol - 3, mMonthCol + 3)
                    ''                        .Col = mMonthCol + 3
                    ''                        mVDone = True
                    ''                    Else
                    ''                        mVDone = False
                    ''                    End If
                    '                End If
                    '
                    '                If mVDone = True Then
                    '                    .Text = "A"
                    '                    .ForeColor = vbGreen
                    '                    .BackColor = vbGreen
                    '                End If

                End If
                mRsSchd.MoveNext()
                mRow = mRow + 1
            Loop
        End With

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        'Resume
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CheckScheduleMonth(ByRef mEndDate As String, ByRef mDueOn As String, ByRef mFrequency As Integer, ByRef pMonthDueOn As Object) As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mFYStartDate As String
        Dim mFYEndDate As String
        Dim mCurrentDueOn As String

        pMonthDueOn = ""
        mCurrentDueOn = mDueOn
        mFYStartDate = IIf(IsDbNull(RsCompany.Fields("START_DATE").Value), "", RsCompany.Fields("START_DATE").Value)
        mFYEndDate = IIf(IsDbNull(RsCompany.Fields("END_DATE").Value), "", RsCompany.Fields("END_DATE").Value)
        CheckScheduleMonth = False

        If VB6.Format(mEndDate, "YYYYMM") = VB6.Format(mCurrentDueOn, "YYYYMM") Then
            pMonthDueOn = mDueOn
            CheckScheduleMonth = True
            Exit Function
        End If

        If VB6.Format(mEndDate, "YYYYMM") < VB6.Format(mCurrentDueOn, "YYYYMM") Then
            mCurrentDueOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1 * mFrequency, CDate(mCurrentDueOn)))
            Do While VB6.Format(mEndDate, "YYYYMM") <= VB6.Format(mCurrentDueOn, "YYYYMM")
                If VB6.Format(mEndDate, "YYYYMM") = VB6.Format(mCurrentDueOn, "YYYYMM") Then
                    pMonthDueOn = mCurrentDueOn
                    CheckScheduleMonth = True
                    Exit Function
                End If
                mCurrentDueOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1 * mFrequency, CDate(mCurrentDueOn)))
            Loop
            pMonthDueOn = ""
            CheckScheduleMonth = False
            Exit Function
        Else
            mCurrentDueOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, mFrequency, CDate(mCurrentDueOn)))
            Do While VB6.Format(mEndDate, "YYYYMM") >= VB6.Format(mCurrentDueOn, "YYYYMM")
                If VB6.Format(mEndDate, "YYYYMM") = VB6.Format(mCurrentDueOn, "YYYYMM") Then
                    pMonthDueOn = mCurrentDueOn
                    CheckScheduleMonth = True
                    Exit Function
                End If
                mCurrentDueOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, mFrequency, CDate(mCurrentDueOn)))
            Loop
            pMonthDueOn = ""
            CheckScheduleMonth = False
            Exit Function
        End If

        Exit Function
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CheckActual(ByRef mDoc As String, ByRef mEndDate As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFromDate As String

        If optType(1).Checked = True Then
            mFromDate = "01/" & VB6.Format(mEndDate, "MM/YYYY")
        Else
            mFromDate = mEndDate
        End If

        CheckActual = False
        SqlStr = " SELECT CALIB_DATE " & vbCrLf & " FROM QAL_GAUGE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DOCNO=" & Val(mDoc) & "" & vbCrLf & " AND CALIB_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND CALIB_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckActual = True
        End If
        Exit Function
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mStartDate As String
        Dim mEndDate As String

        MakeSQL = " SELECT DISTINCT DOCNO, DESCRIPTION, "

        If optType(0).Checked = True Then
            If lblGaugeIMTE.Text = "G" Then
                MakeSQL = MakeSQL & vbCrLf & "TYPENO, VDUEON,VDUEON AS VDUEON1, VALFREQUENCY,TO_CHAR(VDONEON,'MONTH') As VDONEON,VDONEON AS VDONEON1 FROM QAL_GAUGEFIX_MST "
            Else
                MakeSQL = MakeSQL & vbCrLf & "E_NO, CDATE,CDATE AS VDUEON1,VALFREQUENCY FROM QAL_IMTE_MST "
            End If
        Else
            If lblGaugeIMTE.Text = "G" Then
                MakeSQL = MakeSQL & vbCrLf & "TYPENO, TO_CHAR(VDUEON,'MONTH') As VDUEON,VDUEON AS VDUEON1,VALFREQUENCY, TO_CHAR(VDONEON,'MONTH') As VDONEON, VDONEON AS VDONEON1 FROM QAL_GAUGEFIX_MST "
            Else
                MakeSQL = MakeSQL & vbCrLf & "E_NO, TO_CHAR(CDATE,'MONTH') As CDATE,CDATE AS VDUEON1,VALFREQUENCY FROM QAL_IMTE_MST "
            End If
        End If
        MakeSQL = MakeSQL & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "'"
        End If

        If optType(0).Checked = True Then
            If Trim(cboMonth.Text) <> "" And Trim(cboYear.Text) <> "" Then
                If lblGaugeIMTE.Text = "G" Then
                    MakeSQL = MakeSQL & vbCrLf & " AND (TO_CHAR(VDUEON,'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-1*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-2*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-3*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-4*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-5*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-6*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-7*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-8*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-9*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-10*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-11*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " OR TO_CHAR(ADD_MONTHS(VDUEON,-12*VALFREQUENCY),'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "'" & vbCrLf & " )"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " AND TO_CHAR(CDATE,'MMYYYY')='" & VB6.Format(cboMonth.SelectedIndex + 1, "00") & VB6.Format(cboYear.Text, "0000") & "' "
                End If
            End If
        Else
            mStartDate = "01/APR/" & VB6.Format(cboYear.Text, "0000")
            mEndDate = "31/MAR/" & (CDbl(VB6.Format(cboYear.Text, "0000")) + 1)
            If Trim(cboYear.Text) <> "" Then
                If lblGaugeIMTE.Text = "G" Then
                    MakeSQL = MakeSQL & vbCrLf & " AND VDUEON>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND VDUEON<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " AND CDATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND CDATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If
            End If
        End If

        '    MakeSQL = MakeSQL & vbCrLf & " AND DOCNO=1 "

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY DOCNO "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1

        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) = "" Then
            MsgBox("Please Select Location")
            txtLocation.Focus()
            Exit Function
        End If
        If Trim(cboMonth.Text) = "" Then
            MsgBox("Please Select Month")
            cboMonth.Focus()
            Exit Function
        End If
        If Trim(cboYear.Text) = "" Then
            MsgBox("Please Select Year")
            cboYear.Focus()
            Exit Function
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            Call PrintStatus(False)
            If Index = 0 Then
                cboMonth.Enabled = True
            Else
                cboMonth.Enabled = False
            End If
        End If
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtLocation_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.DoubleClick
        Call cmdSearchLocation_Click(cmdSearchLocation, New System.EventArgs())
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLocation_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLocation.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchLocation_Click(cmdSearchLocation, New System.EventArgs())
    End Sub

    Private Sub txtLocation_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLocation.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(txtLocation.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtLocation.Text, "LOCATION", "DOCNO", IIf(lblGaugeIMTE.text = "G", "QAL_GAUGEFIX_MST", "QAL_IMTE_MST"), PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Location")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
