Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO

Friend Class frmEmpWiseDailyAttn
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
    Private Const ColDept As Short = 3
    Private Const ColWorkingHours As Short = 4
    Private Const ColShiftINTime As Short = 5
    Private Const ColShiftOutTime As Short = 6
    Private Const ColINTime As Short = 7
    Private Const ColOutTime As Short = 8
    Private Const ColTotalHours As Short = 9
    Private Const ColOTHours As Short = 10
    Private Const ColRoundClock As Short = 11
    Private Const ColAttnFH As Short = 12
    Private Const ColAttnSH As Short = 13
    Private Const ColMachineData1 As Short = 14
    Private Const ColMachineData2 As Short = 15
    Private Const ColMachineData As Short = 16
    Private Const ColMachineNo As Short = 17
    Private Const ColGrossSalary As Short = 18
    Private Const ColDailyAmount As Short = 19
    Private Const ColOTAmount As Short = 20
    Private Const ColFoodingAllow As Short = 21
    Private Const ColLateComer As Short = 22
    Private Const ColRemarks As Short = 23

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub FillHeading()

        Dim cntCol As Integer
        Dim I As Integer




        With sprdAttn
            .MaxCols = ColRemarks

            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Row = -1

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColCard, 8)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 25)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 8)

            .Col = ColWorkingHours
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColWorkingHours, 8)

            For cntCol = ColShiftINTime To ColTotalHours
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .set_ColWidth(cntCol, 6)
            Next

            .Col = ColOTHours
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColOTHours, 6)


            For cntCol = ColAttnFH To ColMachineData
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColMachineNo, 8)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColRemarks, 6)

            For cntCol = ColGrossSalary To ColLateComer
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8)
                .ColHidden = IIf(cntCol = ColLateComer, False, IIf(chkWithRate.CheckState = System.Windows.Forms.CheckState.Checked, False, True))
            Next

            .Col = ColRoundClock
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(ColRoundClock, 6)

            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCard
            .Text = "Emp Card No"


            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDept
            .Text = "Dept."

            .Col = ColWorkingHours
            .Text = "Emp Working Hours"

            .Col = ColShiftINTime
            .Text = "Shift IN Time"

            .Col = ColINTime
            .Text = "IN Time"

            .Col = ColShiftOutTime
            .Text = "Shift OUT Time"

            .Col = ColOutTime
            .Text = "OUT Time"

            .Col = ColTotalHours
            .Text = "Total Hours"

            .Col = ColRoundClock
            .Text = "Round Clock"

            .Col = ColOTHours
            .Text = "OT Hours"

            .Col = ColAttnFH
            .Text = "Attn First Half"

            .Col = ColAttnSH
            .Text = "Attn Second Half"

            .Col = ColMachineData1
            .Text = "Previous Day Machine Data"

            .Col = ColMachineData2
            .Text = "Today Machine Data"

            .Col = ColMachineData
            .Text = "Next Day Machine Data"

            .Col = ColMachineNo
            .Text = "Machine No"

            .Col = ColGrossSalary
            .Text = "Gross Salary"

            .Col = ColDailyAmount
            .Text = "Daily Wages Amount"

            .Col = ColOTAmount
            .Text = "OT Allow"

            .Col = ColFoodingAllow
            .Text = "Fooding Allow"

            .Col = ColLateComer
            .Text = "Late Comer"

            .Col = ColRemarks
            .Text = "Remarks"

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, ColRemarks)
            MainClass.SetSpreadColor(sprdAttn, -1, False)
            sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        End With
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpCode.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtEmpCode.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub
    Private Sub chkBookNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBookNo.CheckStateChanged
        If chkBookNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtBookNo.Enabled = False
        Else
            txtBookNo.Enabled = True
        End If
    End Sub


    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCatgeory.Enabled = False
        Else
            cboCatgeory.Enabled = True
        End If
    End Sub

    Private Sub chkPageNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPageNo.CheckStateChanged
        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPageNo.Enabled = False
        Else
            txtPageNo.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdExport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExport.Click


        FraPreview.Visible = True
        FraPreview.BringToFront()
        '    With sprdAttn
        '        .Col = ColPic
        '        .ColHidden = True
        '        .ColWidth(ColDesc) = 27 + 15
        '        .ColWidth(ColSchd) = 4
        '        .ColWidth(ColCurrSubTotal) = 12
        '        .ColWidth(ColCurrTotal) = 12
        '        .ColWidth(ColPrevSubTotal) = 12
        '        .ColWidth(ColPrevTotal) = 12
        '    End With

        '    If UCase(lblType.Caption) = UCase("Balance Sheet") Then
        '        sprdAttn.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Balance Sheet As On " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    ElseIf UCase(lblType.Caption) = UCase("Fund Flow") Then
        '        sprdAttn.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Fund Flow As On " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    Else
        '        sprdAttn.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Profit & Loss A//c As On " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    End If
        Call SpreadSheetPreview(sprdAttn, SprdPreview, SprdCommand, VB6.PixelsToTwipsX(ClientRectangle.Width) - 200, VB6.PixelsToTwipsY(ClientRectangle.Height) - 200)

    End Sub

    Private Sub SprdCommand_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdCommand.ButtonClicked
        On Error GoTo ERR1
        Dim mFilename As String

        SprdCommand.Col = eventArgs.col
        SprdCommand.Row = eventArgs.row

        If SprdCommand.CellType = FPSpreadADO.CellTypeConstants.CellTypeButton Then
            Select Case eventArgs.col
                Case 2 'Next
                    ShowNextPage(sprdAttn, SprdPreview, SprdCommand, eventArgs.col)

                Case 4 'Previous
                    ShowPreviousPage(sprdAttn, SprdPreview, SprdCommand, eventArgs.col)

                Case 6 'Zoom
                    SprdPreview.ZoomState = 3

                Case 8 'Print
                    cmdPrint_Click(cmdPrint, New System.EventArgs())

                Case 10 'Export
                    mFilename = ""    '' ExportSprdToExcel(CommonDialog1)

                    If sprdAttn.ExportToExcel(mFilename, "AttnSheet", "") = True Then
                        '                If sprdAttn.ExportExcelBook(mFilename, "") = True Then
                        MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name Is " & mFilename)
                    End If

                Case 18 'Close
                    FraPreview.Visible = False
                    '                With sprdAttn
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
        Dim mDNAReport As Boolean
        Dim pRptName As String
        PubDBCn.Errors.Clear()


        ''Insert Data from Grid to PrintDummyData Table...

        frmPrintSalVoucher.OptSalSlip.Text = "Daily Attandance Report"
        frmPrintSalVoucher.optPerks.Text = IIf(chkWithRate.CheckState = System.Windows.Forms.CheckState.Unchecked, "DNA Report", "DNA Report (With Wages)")

        frmPrintSalVoucher.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        If frmPrintSalVoucher.OptSalSlip.Checked = True Then
            mDNAReport = False
        Else
            mDNAReport = True
        End If

        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "For The Day : " & VB6.Format(lblRunDate.Text, "DD-MMM-YYYY")

        If mDNAReport = False Then
            If chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked And cboShow.Text <> "" Then
                mTitle = cboShow.Text & " Report"
            Else
                mTitle = "Employee Daily Attendance Report"
            End If

            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.Text <> "" Then
                mSubTitle = mSubTitle & " - " & cboCatgeory.Text
            End If
            pRptName = "DailyAttnEmpReport.Rpt"
        Else
            SqlStr = " SELECT * " & vbCrLf _
                & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf _
                & " WHERE  " & vbCrLf _
                & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
                & " ORDER BY FIELD4, SUBROW"

            mTitle = "Daily Analysis Report"

            If chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked And cboShow.Text <> "" Then
                mTitle = mTitle & " (" & cboShow.Text & ")"
            End If

            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.Text <> "" Then
                mSubTitle = mSubTitle & " - " & cboCatgeory.Text
            End If

            pRptName = IIf(chkWithRate.CheckState = System.Windows.Forms.CheckState.Checked, "EmpDNAReportWithRate.rpt", "EmpDNAReport.Rpt")
        End If

        Call ShowReport(SqlStr, pRptName, Mode, mTitle, mSubTitle)

        frmPrintSalVoucher.Close()
        Exit Sub
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub chkShow_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShow.CheckStateChanged
        If chkShow.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboShow.Enabled = False
        Else
            cboShow.Enabled = True
        End If
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

        On Error GoTo ErrPart

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtEmpCode.Text) = "" Then
                MsgInformation("Please Select Operator Code")
                txtEmpCode.Focus()
                Exit Sub
            End If

            txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Employee Code ")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If
        MainClass.ClearGrid(sprdAttn)
        FillHeading()
        RefreshScreen()
        '    FillGridColor
        MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, ColRemarks)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub frmEmpWiseDailyAttn_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        'Me.Text = "Daily Attendance Report (New)"
    End Sub

    Private Sub frmEmpWiseDailyAttn_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        MainClass.ClearGrid(sprdAttn)
        FillHeading()

        FillDeptCombo()

        lblRunDate.Text = VB6.Format(RunDate, "DD-MMM-YYYY")

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkBookNo.CheckState = System.Windows.Forms.CheckState.Checked
        chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked

        cboDept.Enabled = False
        txtBookNo.Enabled = False
        txtPageNo.Enabled = False

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCatgeory.Enabled = False

        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        chkShow.CheckState = System.Windows.Forms.CheckState.Checked
        cboShow.Enabled = False

        Frame6.Visible = True
        'lblColor(0).BackStyle = 1
        'lblColor(1).BackStyle = 1
        'lblColor(2).BackStyle = 1
        'lblColor(3).BackStyle = 1
        'lblColor(4).BackStyle = 1
        'lblColor(5).BackStyle = 1
        'lblColor(6).BackStyle = 1
        'lblColor(7).BackStyle = 1
        'lblColor(8).BackStyle = 1
        'lblColor(9).BackStyle = 1

        lblColor(0).BackColor = Color.LightPink  '''' System.Drawing.ColorTranslator.FromOle(&HFF)
        lblColor(1).BackColor = System.Drawing.ColorTranslator.FromOle(&HC000)
        lblColor(2).BackColor = System.Drawing.ColorTranslator.FromOle(&HFF8080)
        lblColor(3).BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFC0)
        lblColor(4).BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF)
        lblColor(5).BackColor = System.Drawing.ColorTranslator.FromOle(&H80FF)
        lblColor(6).BackColor = System.Drawing.ColorTranslator.FromOle(&HFF80FF)
        lblColor(7).BackColor = System.Drawing.ColorTranslator.FromOle(&H80FF80)
        lblColor(8).BackColor = System.Drawing.ColorTranslator.FromOle(&H8080)
        lblColor(9).BackColor = System.Drawing.ColorTranslator.FromOle(&HC000C0)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub frmEmpWiseDailyAttn_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    'Private Sub UpDYear_DownClick()

    '    lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(lblRunDate.Text)), "DD-MMM-YYYY")

    '    SetDate(CDate(lblRunDate.Text))
    '    MainClass.ClearGrid(sprdAttn, -1)
    '    '' RefreshScreen
    'End Sub
    'Private Sub UpDYear_UpClick()

    '    lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(lblRunDate.Text)), "DD-MMM-YYYY")

    '    SetDate(CDate(lblRunDate.Text))
    '    MainClass.ClearGrid(sprdAttn, -1)
    '    ''RefreshScreen
    'End Sub


    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        '    Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        '    NewDate = Format(Tempdate, "dd/mm/yyyy")
        '    lblRunDate.text = NewDate

        lblRunDate.Text = VB6.Format(lblRunDate.Text, "DD-MMM-YYYY")

        Daysinmonth = MainClass.LastDay(VB6.Format(lblRunDate.Text, "mm"), VB6.Format(lblRunDate.Text, "yyyy"))
    End Sub
    Private Sub txtBookNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBookNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPageNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPageNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCode As String

        Dim cntRow As Integer

        Dim mDeptCode As String
        Dim mAttnDate As String
        Dim mDeptName As String
        Dim mContCode As String
        Dim mBookNo As String
        Dim mDate As String
        Dim mDOJ As String
        Dim mDOL As String
        Dim mLastDay As Integer
        Dim mDays As Integer
        Dim mRoundClock As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mFirstColor As FPSpreadADO.BackColorStyleConstants
        Dim mSecondColor As FPSpreadADO.BackColorStyleConstants
        Dim mHour As Double
        Dim mMin As Double
        Dim mTotMin As Double
        Dim mFHalf As String
        Dim mSHalf As String
        Dim mOTHours As Double
        Dim mPrevDate As String
        Dim mNextDate As String
        Dim mType As String

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mDate = VB6.Format(lblRunDate.Text, "DD-MMM-YYYY")
        mPrevDate = DateAdd("d", -1, mDate)
        mNextDate = DateAdd("d", 1, mDate)
        mDOJ = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        mDOL = "01" & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

        SqlStr = "SELECT DISTINCT EMP_CODE, EMP_NAME, DEPT_DESC, WORKING_HOURS, " & vbCrLf _
            & " TO_CHAR(SHIFT_IN_TIME,'HH24:MI'), TO_CHAR(SHIFT_OUT_TIME,'HH24:MI')," & vbCrLf _
            & " TO_CHAR(IN_TIME,'HH24:MI') , TO_CHAR(OUT_TIME,'HH24:MI'),   "

        SqlStr = SqlStr & vbCrLf _
                & " TOT_WHOURS, " & vbCrLf _
                & " OT_HOURS, " & vbCrLf _
                & " DECODE(ROUND_CLOCK,'N','0','1') AS ROUND_CLOCK, "

        SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN FIRSTHALF = -1 THEN '' " & vbCrLf _
                & " WHEN FIRSTHALF = 0 THEN 'UNAPPROVED' " & vbCrLf _
                & " WHEN FIRSTHALF = 1 THEN 'CASUAL' " & vbCrLf _
                & " WHEN FIRSTHALF = 2 THEN 'EARN' " & vbCrLf _
                & " WHEN FIRSTHALF = 3 THEN 'SICK' " & vbCrLf _
                & " WHEN FIRSTHALF = 4 THEN 'MATERNITY' " & vbCrLf _
                & " WHEN FIRSTHALF = 5 THEN 'CPLEARN' " & vbCrLf _
                & " WHEN FIRSTHALF = 6 THEN 'APPROVED' " & vbCrLf _
                & " WHEN FIRSTHALF = 7 THEN 'CPLAVAIL' " & vbCrLf _
                & " WHEN FIRSTHALF = 8 THEN 'SUNDAY' " & vbCrLf _
                & " WHEN FIRSTHALF = 9 THEN 'HOLIDAY' " & vbCrLf _
                & " WHEN FIRSTHALF = 10 THEN 'PRESENT' " & vbCrLf _
                & " WHEN FIRSTHALF = 11 THEN 'WFH' " & vbCrLf _
                & " ELSE '' " & vbCrLf _
                & " END FIRSTHALF,"

        SqlStr = SqlStr & vbCrLf _
                    & " CASE WHEN SECONDHALF = -1 THEN '' " & vbCrLf _
                    & " WHEN SECONDHALF = 0 THEN 'UNAPPROVED' " & vbCrLf _
                    & " WHEN SECONDHALF = 1 THEN 'CASUAL' " & vbCrLf _
                    & " WHEN SECONDHALF = 2 THEN 'EARN' " & vbCrLf _
                    & " WHEN SECONDHALF = 3 THEN 'SICK' " & vbCrLf _
                    & " WHEN SECONDHALF = 4 THEN 'MATERNITY' " & vbCrLf _
                    & " WHEN SECONDHALF = 5 THEN 'CPLEARN' " & vbCrLf _
                    & " WHEN SECONDHALF = 6 THEN 'APPROVED' " & vbCrLf _
                    & " WHEN SECONDHALF = 7 THEN 'CPLAVAIL' " & vbCrLf _
                    & " WHEN SECONDHALF = 8 THEN 'SUNDAY' " & vbCrLf _
                    & " WHEN SECONDHALF = 9 THEN 'HOLIDAY' " & vbCrLf _
                    & " WHEN SECONDHALF = 10 THEN 'PRESENT' " & vbCrLf _
                    & " WHEN SECONDHALF = 11 THEN 'WFH' " & vbCrLf _
                    & " ELSE '' " & vbCrLf _
                    & " END SECONDHALF, "


        SqlStr = SqlStr & vbCrLf _
                & " MACH_PREV_DATE," & vbCrLf _
                & " MACH_TODAY_DATE, " & vbCrLf _
                & " MACH_NEXT_DATE, MACHINE_NO, GROSS_SAL, DAILY_WAGES, OT_AMOUNT, FOOD_ALLOW, LATE_COMER, REMARKS"

        SqlStr = SqlStr & vbCrLf _
                & " FROM ( "

        SqlStr = SqlStr & vbCrLf _
                & " SELECT EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf _
                & " DEPT.DEPT_DESC, EMP.WORKING_HOURS, " & vbCrLf _
                & " NVL(SMST.IN_TIME,'') AS SHIFT_IN_TIME, NVL(SMST.OUT_TIME,'')  AS SHIFT_OUT_TIME," & vbCrLf _
                & " NVL(ATTN.IN_TIME,'') AS IN_TIME, NVL(ATTN.OUT_TIME,'') AS OUT_TIME,"

        SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.OUT_TIME,'HH24:MI')='00:00' OR TO_CHAR(ATTN.IN_TIME,'HH24:MI')='00:00' THEN '00:00' ELSE TO_CHAR(TRUNC((ATTN.OUT_TIME-ATTN.IN_TIME)*24,0),'00') || ':' || MOD((ATTN.OUT_TIME-ATTN.IN_TIME)*24*60,60) END AS TOT_WHOURS, " & vbCrLf _
                & " 0 AS OT_HOURS, " & vbCrLf _
                & " SMST.ROUND_CLOCK, "
        ''TO_CHAR(TOT_WHOURS,'HH24:MI') 
        ''SELECT	TRUNC (run_minutes / 60) || ' Hours, ' ||
        'Mod   (run_minutes,  60) || ' Minutes'		AS run_time

        SqlStr = SqlStr & vbCrLf _
                & " NVL(FIRSTHALF, -1) As FIRSTHALF, NVL(SECONDHALF,-1) SECONDHALF, "


        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
        '    'mNewCode = 
        '    SqlStr = SqlStr & vbCrLf _
        '        & " (SELECT LISTAGG(TO_CHAR(OFFICEPUNCH,'HH24:MI'), ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM TEMPDATA WHERE TRIM(CARDNO)= TRIM(REPLACE(REPLACE(REPLACE(EMP.EMP_CODE, 'G-', '7'), 'B-', '2'),'K-', '11')) AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mPrevDate, "YYYYMMDD") & "') AS MACH_PREV_DATE," & vbCrLf _
        '        & " (SELECT LISTAGG(TO_CHAR(OFFICEPUNCH,'HH24:MI'), ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM TEMPDATA WHERE TRIM(CARDNO)= TRIM(REPLACE(REPLACE(REPLACE(EMP.EMP_CODE, 'G-', '7'), 'B-', '2'),'K-', '11')) AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "')  AS MACH_TODAY_DATE, " & vbCrLf _
        '        & " (SELECT LISTAGG(TO_CHAR(OFFICEPUNCH,'HH24:MI'), ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM TEMPDATA WHERE TRIM(CARDNO)= TRIM(REPLACE(REPLACE(REPLACE(EMP.EMP_CODE, 'G-', '7'), 'B-', '2'),'K-', '11')) AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mNextDate, "YYYYMMDD") & "')  AS MACH_NEXT_DATE,"

        'Else
        '    SqlStr = SqlStr & vbCrLf _
        '        & " (SELECT LISTAGG(TO_CHAR(OFFICEPUNCH,'HH24:MI'), ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(EMP.EMP_CODE) AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mPrevDate, "YYYYMMDD") & "') AS MACH_PREV_DATE," & vbCrLf _
        '        & " (SELECT LISTAGG(TO_CHAR(OFFICEPUNCH,'HH24:MI'), ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(EMP.EMP_CODE) AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "')  AS MACH_TODAY_DATE, " & vbCrLf _
        '        & " (SELECT LISTAGG(TO_CHAR(OFFICEPUNCH,'HH24:MI'), ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(EMP.EMP_CODE) AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mNextDate, "YYYYMMDD") & "')  AS MACH_NEXT_DATE,"

        'End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            'mNewCode = 
            SqlStr = SqlStr & vbCrLf _
                & " (SELECT LISTAGG(REFTIME, ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(REPLACE(REPLACE(REPLACE(EMP.EMP_CODE, 'G-', '7'), 'B-', '2'),'K-', '11')) AND REFDATE= '" & VB6.Format(mPrevDate, "YYYYMMDD") & "') AS MACH_PREV_DATE," & vbCrLf _
                & " (SELECT LISTAGG(REFTIME, ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(REPLACE(REPLACE(REPLACE(EMP.EMP_CODE, 'G-', '7'), 'B-', '2'),'K-', '11')) AND REFDATE= '" & VB6.Format(mDate, "YYYYMMDD") & "')  AS MACH_TODAY_DATE, " & vbCrLf _
                & " (SELECT LISTAGG(REFTIME, ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(REPLACE(REPLACE(REPLACE(EMP.EMP_CODE, 'G-', '7'), 'B-', '2'),'K-', '11')) AND REFDATE= '" & VB6.Format(mNextDate, "YYYYMMDD") & "')  AS MACH_NEXT_DATE,"

        Else
            SqlStr = SqlStr & vbCrLf _
                & " (SELECT LISTAGG(REFTIME, ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(EMP.EMP_CODE) AND REFDATE= '" & VB6.Format(mPrevDate, "YYYYMMDD") & "') AS MACH_PREV_DATE," & vbCrLf _
                & " (SELECT LISTAGG(REFTIME, ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(EMP.EMP_CODE) AND REFDATE= '" & VB6.Format(mDate, "YYYYMMDD") & "')  AS MACH_TODAY_DATE, " & vbCrLf _
                & " (SELECT LISTAGG(REFTIME, ', ') WITHIN GROUP (ORDER BY OFFICEPUNCH) FROM vwTempdata WHERE TRIM(CARDNO)= TRIM(EMP.EMP_CODE) AND REFDATE= '" & VB6.Format(mNextDate, "YYYYMMDD") & "')  AS MACH_NEXT_DATE,"

        End If


        SqlStr = SqlStr & vbCrLf _
                & " (SELECT MAX(NAME) FROM PAY_MACHINE_MST WHERE COMPANY_CODE=EMP.COMPANY_CODE AND CODE=EMP.MACHINE_CODE) AS MACHINE_NO," & vbCrLf _
                & " 0 GROSS_SAL, 0 DAILY_WAGES, 0 OT_AMOUNT," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.OUT_TIME,'HH24:MI')>='00:55' AND SHIFTM.MAJOR_SHIFT<>'C' AND TO_CHAR(ATTN.OUT_TIME,'DDMMYYYY')<>TO_CHAR(ATTN.IN_TIME,'DDMMYYYY') THEN 75 ELSE 0 END AS FOOD_ALLOW," '' 

        SqlStr = SqlStr & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.IN_TIME,'HH24:MI')='00:00' THEN 0 WHEN NVL(FIRSTHALF,-1) IN (-1,10,11)  AND NVL(SMST.IN_TIME + (1/1440*5),'')<NVL(ATTN.IN_TIME,'') THEN ROUND((ATTN.IN_TIME - SMST.IN_TIME) * 1440,0) ELSE 0 END  + CASE WHEN TO_CHAR(ATTN.OUT_TIME,'HH24:MI')='00:00' THEN 0 WHEN NVL(SECONDHALF,-1) IN (-1,10,11)  AND NVL(SMST.OUT_TIME + (-1/1440*5),'')>NVL(ATTN.OUT_TIME,'') THEN ROUND((SMST.OUT_TIME - ATTN.OUT_TIME) * 1440,0) ELSE 0 END AS LATE_COMER,"

        'SqlStr = SqlStr & vbCrLf _
        '         & " AND NVL(FIRSTHALF,-1) IN (-1,10,11) AND ((ATTN.IN_TIME IS NOT NULL OR TO_CHAR(ATTN.IN_TIME,'HH24:MI')='00:00') AND NVL(SMST.IN_TIME + (1/1440*5),'')<NVL(ATTN.IN_TIME,''))"


        SqlStr = SqlStr & vbCrLf _
                & " NVL((SELECT LISTAGG(CASE WHEN MOVE_TYPE='O' THEN 'OD' WHEN MOVE_TYPE='P' THEN 'SHORT LEAVE' ELSE 'MANUAL' END, ', ') WITHIN GROUP (ORDER BY TIME_FROM) FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND REF_DATE=DAY AND HR_APPROVAL='Y'),'') AS REMARKS"


        SqlStr = SqlStr & vbCrLf _
                & " FROM (" & vbCrLf _
                & " Select TRUNC(TO_DATE('" & VB6.Format(mDate, "DD/MM/YYYY") & "','DD/MM/YYYY'), 'MM') + LEVEL - 1 AS DAY" & vbCrLf _
                & " FROM DUAL" & vbCrLf _
                & " CONNECT BY LEVEL <= 32" & vbCrLf _
                & " ) CAL_MST, PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT, PAY_DALIY_ATTN_TRN ATTN, PAY_SHIFT_TRN SMST, PAY_ATTN_MST PMST, PAY_SHIFT_MST SHIFTM" & vbCrLf _
                & " WHERE EXTRACT(Month FROM day) = EXTRACT(Month FROM TO_DATE('" & VB6.Format(mDate, "DD/MM/YYYY") & "','DD/MM/YYYY'))"


        SqlStr = SqlStr & vbCrLf _
            & " AND DAY=TO_DATE('" & VB6.Format(mDate, "DD/MM/YYYY") & "','DD/MM/YYYY')"

        SqlStr = SqlStr & vbCrLf _
                & " AND EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And EMP.COMPANY_CODE=DEPT.COMPANY_CODE " & vbCrLf _
                & " And EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE "

        SqlStr = SqlStr & vbCrLf _
                & " AND EMP.COMPANY_CODE =ATTN.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=ATTN.EMP_CODE(+) " & vbCrLf _
                & " And DAY=ATTN.ATTN_DATE (+)"

        SqlStr = SqlStr & vbCrLf _
                & " AND EMP.COMPANY_CODE =SMST.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=SMST.EMP_CODE(+) " & vbCrLf _
                & " And DAY=SMST.SHIFT_DATE(+) "

        SqlStr = SqlStr & vbCrLf _
                & " AND SMST.COMPANY_CODE =SHIFTM.COMPANY_CODE(+)" & vbCrLf _
                & " And SMST.SHIFT_CODE=SHIFTM.SHIFT_CODE(+) " & vbCrLf

        SqlStr = SqlStr & vbCrLf _
                & " AND EMP.COMPANY_CODE =PMST.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=PMST.EMP_CODE(+) " & vbCrLf _
                & " And DAY=PMST.ATTN_DATE(+) "


        SqlStr = SqlStr & vbCrLf _
            & " And EMP_DOJ <=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "


        If chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboShow.SelectedIndex = 0 Then
                SqlStr = SqlStr & vbCrLf _
                    & " AND (ATTN.IN_TIME IS NOT NULL AND TO_CHAR(ATTN.IN_TIME,'HH24:MI')<>'00:00') AND   (ATTN.OUT_TIME IS NOT NULL AND TO_CHAR(ATTN.OUT_TIME,'HH24:MI')<>'00:00')"
            ElseIf cboShow.SelectedIndex = 1 Then
                SqlStr = SqlStr & vbCrLf _
                   & " AND (ATTN.IN_TIME IS NULL OR TO_CHAR(ATTN.IN_TIME,'HH24:MI')='00:00') AND  (ATTN.OUT_TIME IS NULL OR TO_CHAR(ATTN.OUT_TIME,'HH24:MI')='00:00')" & vbCrLf _
                   & " AND (NVL(FIRSTHALF,-1) IN (-1,0) OR NVL(SECONDHALF,-1) IN (-1,0))"
            ElseIf cboShow.SelectedIndex = 2 Then
                SqlStr = SqlStr & vbCrLf _
                  & " AND (FIRSTHALF IN (1,2,3,4,6) OR SECONDHALF IN (1,2,3,4,6))"
            ElseIf cboShow.SelectedIndex = 3 Or cboShow.SelectedIndex = 7 Then
                SqlStr = SqlStr & vbCrLf _
                 & " AND (NVL(FIRSTHALF,-1) IN (-1,10,11) AND ((ATTN.IN_TIME IS NOT NULL OR TO_CHAR(ATTN.IN_TIME,'HH24:MI')='00:00') AND NVL(SMST.IN_TIME + (1/1440*5),'')<NVL(ATTN.IN_TIME,'')))" & vbCrLf _
                 & " AND EMP.EMP_CODE NOT IN (SELECT EMP_CODE FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND REF_DATE=DAY AND HR_APPROVAL='Y' AND MOVE_TYPE <> 'M' AND TIME_FROM<=NVL(ATTN.IN_TIME,''))"
            ElseIf cboShow.SelectedIndex = 4 Then
                SqlStr = SqlStr & vbCrLf _
                  & " AND NVL((Select MAX(NVL(MOVE_TYPE,'')) FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND MOVE_TYPE='O' AND REF_DATE=DAY AND HR_APPROVAL='Y'),'')='O'"
            ElseIf cboShow.SelectedIndex = 5 Then
                SqlStr = SqlStr & vbCrLf _
                  & " AND (TO_CHAR(ATTN.IN_TIME,'HH24:MI')<>'00:00')  AND (ATTN.OUT_TIME IS NULL OR TO_CHAR(ATTN.OUT_TIME,'HH24:MI')='00:00')  AND (NVL(FIRSTHALF,-1) IN (-1) OR NVL(SECONDHALF,-1) IN (-1))"
            ElseIf cboShow.SelectedIndex = 6 Or cboShow.SelectedIndex = 8 Then
                SqlStr = SqlStr & vbCrLf _
                 & " AND ( NVL(SECONDHALF,-1) IN (-1,10,11) AND NVL(TO_CHAR(ATTN.OUT_TIME,'HH24:MI'),'00:00')<>'00:00' AND NVL(SMST.OUT_TIME + (1/1440*-5),'')>NVL(ATTN.OUT_TIME,''))" & vbCrLf _
                 & " --AND EMP.EMP_CODE NOT IN (SELECT EMP_CODE FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND REF_DATE=DAY AND HR_APPROVAL='Y' AND TIME_FROM>=NVL(ATTN.OUT_TIME,''))"
            End If
        End If

        '' WITHIN GROUP (ORDER BY TIME_FROM) FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND REF_DATE=DAY AND HR_APPROVAL='Y'),'') 

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
        End If

        'If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBookNo.Text) <> "" Then
        '    SqlStr = SqlStr & vbCrLf & "AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
        'End If

        'If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPageNo.Text) <> "" Then
        '    SqlStr = SqlStr & vbCrLf & "AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
        'End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf _
                & " ) "

        '    SqlStr = SqlStr & vbCrLf _
        ''                    & " AND ATTN_DATE>='" & UCase(Format(txtFrom.Text, "DD-MMM-YYYY")) & "'" & vbCrLf _
        ''                    & " AND ATTN_DATE<='" & UCase(Format(txtTo.Text, "DD-MMM-YYYY")) & "'"


        '    SqlStr = SqlStr & vbCrLf & "Group by EMP.EMP_NAME, EMP.EMP_CODE "

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by DEPT_DESC, EMP_NAME"
        ElseIf optCard.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by DEPT_DESC, EMP_CODE"
        ElseIf optDept.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by DEPT_DESC, EMP_CODE"
        Else
            'SqlStr = SqlStr & vbCrLf & "Order by SMST.BOOKNO,SMST.PAGENO"
        End If

        MainClass.AssignDataInSprd8(SqlStr, sprdAttn, StrConn, "Y")

        'Dim mOverTime As Double
        'Dim mTOTOverTime As Double
        Dim mOTRate As Double
        Dim mOTAmount As Double
        Dim mFoodingAmount As Double
        Dim mESIApp As Boolean
        Dim mBasicSalary As Double
        Dim mGrossSalary As Double
        Dim mESIRound As Double
        Dim mOTFactor As Double
        Dim mWorkingHours As Double
        Dim mActWorkingHours As Double
        Dim mRemarks As String

        Dim mINTime1 As String
        Dim mOutTime1 As String
        Dim mLateComer As Double

        With sprdAttn
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColCard
                mCode = Trim(.Text)

                mOTHours = GetOTHours(mCode, VB6.Format(lblRunDate.Text, "DD-MMM-YYYY"))

                .Col = ColOTHours
                .Text = mOTHours

                '.Col = ColFoodingAllow
                '.Text = VB6.Format(IIf(mOTHours >= 16, 75, 0), "0.00")

                'If chkWithRate.CheckState = System.Windows.Forms.CheckState.Checked Then
                mOTRate = CDbl(VB6.Format(GetOTRate(mCode, VB6.Format(lblRunDate.Text, "DD-MMM-YYYY"), mESIApp, mBasicSalary, mESIRound, False, "", mGrossSalary), "0.00"))
                mOTAmount = mOTHours * CDbl(VB6.Format(mOTRate, "0.00"))

                mOTFactor = 0
                If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_OT_RATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOTFactor = MasterNo
                End If

                mOTAmount = mOTAmount * IIf(IsDBNull(mOTFactor) Or Val(CStr(mOTFactor)) = 0, 1, Val(CStr(mOTFactor)))

                .Col = ColWorkingHours
                mActWorkingHours = Val(.Text)

                .Col = ColTotalHours
                mWorkingHours = IIf(mActWorkingHours < Val(.Text), mActWorkingHours, Val(.Text))

                .Col = ColGrossSalary
                .Text = mGrossSalary

                .Col = ColDailyAmount
                .Text = VB6.Format(mGrossSalary * mWorkingHours / (mLastDay * mActWorkingHours), "0.00")

                .Col = ColOTAmount
                .Text = VB6.Format(mOTAmount, "0.00")
                'End If

                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(10).BackColor) '&HFFFFFF
                mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(10).BackColor) '&HFFFFFF

                .Row = cntRow
                .Col = ColAttnFH
                mType = Trim(.Text)

                .Col = ColINTime
                mINTime1 = Trim(.Text)

                .Col = ColOutTime
                mOutTime1 = Trim(.Text)

                .Col = ColRemarks
                mRemarks = Trim(.Text)

                .Col = ColLateComer
                mLateComer = Val(.Text)

                If mType = "HOLIDAY" Or mType = "SUNDAY" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
                ElseIf mType = "CASUAL" Or mType = "EARN" Or mType = "SICK" Or mType = "MATERNITY" Or mType = "APPROVED" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
                ElseIf mType = "CPLEARN" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
                ElseIf mType = "UNAPPROVED" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
                ElseIf mType = "CPLAVAIL" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
                ElseIf mRemarks = "MANUAL" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
                ElseIf mRemarks = "OD" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
                ElseIf mRemarks = "SHORT LEAVE" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
                ElseIf mLateComer > 0 Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
                ElseIf mINTime1 = "00:00" Or mINTime1 = "" Then
                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
                End If


                'SqlStr = SqlStr & vbCrLf _
                '& " CASE WHEN FIRSTHALF = -1 THEN '' " & vbCrLf _
                '& " WHEN FIRSTHALF = 0 THEN '' " & vbCrLf _
                '& " WHEN FIRSTHALF = 1 THEN '' " & vbCrLf _
                '& " WHEN FIRSTHALF = 2 THEN '' " & vbCrLf _
                '& " WHEN FIRSTHALF = 3 THEN '' " & vbCrLf _
                '& " WHEN FIRSTHALF = 4 THEN '' " & vbCrLf _
                '& " WHEN FIRSTHALF = 5 THEN '' " & vbCrLf _
                '& " WHEN FIRSTHALF = 6 THEN '' " & vbCrLf _
                '& " WHEN FIRSTHALF = 7 THEN 'CPLAVAIL' " & vbCrLf _
                '& " WHEN FIRSTHALF = 8 THEN 'SUNDAY' " & vbCrLf _
                '& " WHEN FIRSTHALF = 9 THEN 'HOLIDAY' " & vbCrLf _
                '& " WHEN FIRSTHALF = 10 THEN 'PRESENT' " & vbCrLf _
                '& " WHEN FIRSTHALF = 11 THEN '' " & vbCrLf _
                '& " ELSE '' " & vbCrLf _
                '& " END FIRSTHALF,"

                '

                'ElseIf mFirstManual = True Then
                '    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
                'ElseIf VB6.Format(mInTime, "HH:MM") = "00:00" Then
                '    If mISFirstShortLeave = True Then
                '        mInTime = pDate & " " & mToTime
                '        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
                '    Else
                '        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
                '    End If
                'ElseIf CDate(mInTime) > CDate(mEmpShiftIN) And CDate(mInTime) <= CDate(mSLTime) Then
                '    If mISFirstShortLeave = True Then
                '        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
                '    Else
                '        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
                '    End If
                'ElseIf CDate(mInTime) > CDate(mSLTime) Then
                '    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
                'ElseIf mFirstIsOD = True Then
                '    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
                'Else
                '    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(10).BackColor) '&HFFFFFF
                'End If

                .Col = ColAttnSH
                mType = Trim(.Text)

                If mType = "HOLIDAY" Or mType = "SUNDAY" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
                ElseIf mType = "CASUAL" Or mType = "EARN" Or mType = "SICK" Or mType = "MATERNITY" Or mType = "APPROVED" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
                ElseIf mType = "CPLEARN" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
                ElseIf mType = "UNAPPROVED" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
                ElseIf mType = "CPLAVAIL" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
                ElseIf mRemarks = "MANUAL" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
                ElseIf mRemarks = "OD" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
                ElseIf mRemarks = "SHORT LEAVE" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
                ElseIf mLateComer > 0 Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
                ElseIf mOutTime1 = "00:00" Or mOutTime1 = "" Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
                End If


                .Row = cntRow
                .Row2 = cntRow
                .Col = ColINTime
                .Col2 = ColINTime
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(mFirstColor)
                .BlockMode = False

                .Row = cntRow
                .Row2 = cntRow
                .Col = ColOutTime
                .Col2 = ColOutTime
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(mSecondColor)
                .BlockMode = False

            Next
        End With

        FillHeading()
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        'If RsAttn.EOF = False Then
        '    With sprdAttn
        '        cntRow = 1
        '        Do While Not RsAttn.EOF
        '            .MaxRows = cntRow

        '            .Row = cntRow

        '            mOTHours = 0
        '            mFHalf = ""
        '            mSHalf = ""

        '            .Col = ColCard
        '            mCode = RsAttn.Fields("EMP_CODE").Value
        '            .Text = CStr(mCode)

        '            .Col = ColName
        '            .Text = RsAttn.Fields("EMP_NAME").Value

        '            .Col = ColDept
        '            .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

        '            .Col = ColWorkingHours
        '            .Text = IIf(IsDBNull(RsAttn.Fields("WORKING_HOURS").Value), "", RsAttn.Fields("WORKING_HOURS").Value)

        '            .Col = ColShiftINTime
        '            .Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("IN_TIME").Value), "", RsAttn.Fields("IN_TIME").Value), "HH:MM")

        '            .Col = ColShiftOutTime
        '            .Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("OUT_TIME").Value), "", RsAttn.Fields("OUT_TIME").Value), "HH:MM")

        '            .Col = ColRoundClock
        '            mRoundClock = IIf(IsDBNull(RsAttn.Fields("ROUND_CLOCK").Value), "N", RsAttn.Fields("ROUND_CLOCK").Value)

        '            '.Col = ColOTHours
        '            '.Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("OT_HOURS").Value), "", RsAttn.Fields("OT_HOURS").Value), "HH:MM")

        '            '.Col = ColAttnFH
        '            '.Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("FIRST_HALF").Value), "", RsAttn.Fields("FIRST_HALF").Value), "HH:MM")

        '            '.Col = ColAttnSH
        '            '.Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("SECOND_HALF").Value), "", RsAttn.Fields("SECOND_HALF").Value), "HH:MM")

        '            .Text = IIf(mRoundClock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        '            If GetINOUTTime(mCode, mDate, mInTime, mOutTime, mOTHours, mFirstColor, mSecondColor) = False Then GoTo refreshErrPart

        '            .Row = cntRow
        '            .Col = ColINTime
        '            .Text = VB6.Format(mInTime, "HH:MM")
        '            .Row = cntRow
        '            .Row2 = cntRow
        '            .Col = ColINTime
        '            .Col2 = ColINTime
        '            .BlockMode = True
        '            .BackColor = System.Drawing.ColorTranslator.FromOle(mFirstColor)
        '            .BlockMode = False

        '            .Row = cntRow
        '            .Col = ColOutTime
        '            .Text = VB6.Format(mOutTime, "HH:MM")

        '            .Row = cntRow
        '            .Row2 = cntRow
        '            .Col = ColOutTime
        '            .Col2 = ColOutTime
        '            .BlockMode = True
        '            .BackColor = System.Drawing.ColorTranslator.FromOle(mSecondColor)
        '            .BlockMode = False

        '            .Row = cntRow
        '            .Col = ColTotalHours
        '            If TimeValue(mInTime) = CDate("00:00:00") Or TimeValue(mOutTime) = CDate("00:00:00") Then
        '                .Text = "00:00"
        '            Else
        '                mTotMin = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mInTime), CDate(mOutTime))
        '                mHour = Int(mTotMin / 60)
        '                mMin = mTotMin - (mHour * 60)
        '                .Text = VB6.Format(mHour, "00") & ":" & VB6.Format(mMin, "00") ''Format(TimeValue(mOutTime) - TimeValue(mInTime), "HH:MM")
        '            End If

        '            If GetLeaveMark(mCode, mDate, mFHalf, mSHalf) = False Then GoTo refreshErrPart

        '            .Col = ColOTHours
        '            mOTHours = GetOTHours(mCode, mDate)
        '            .Text = mOTHours

        '            .Col = ColAttnFH
        '            .Text = mFHalf

        '            .Col = ColAttnSH
        '            .Text = mSHalf

        '            .Col = ColMachineData1
        '            .Text = GetPunchTime(mCode, DateAdd("d", -1, mDate), "PAY_EMPLOYEE_MST")

        '            .Col = ColMachineData2
        '            .Text = GetPunchTime(mCode, mDate, "PAY_EMPLOYEE_MST")

        '            .Col = ColMachineData
        '            .Text = GetPunchTime(mCode, DateAdd("d", 1, mDate), "PAY_EMPLOYEE_MST")

        '            cntRow = cntRow + 1
        '            RsAttn.MoveNext()
        '        Loop
        '    End With
        'End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub RefreshScreen004032023()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCode As String

        Dim cntRow As Integer

        Dim mDeptCode As String
        Dim mAttnDate As String
        Dim mDeptName As String
        Dim mContCode As String
        Dim mBookNo As String
        Dim mDate As String
        Dim mDOJ As String
        Dim mDOL As String
        Dim mLastDay As Integer
        Dim mDays As Integer
        Dim mRoundClock As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mFirstColor As FPSpreadADO.BackColorStyleConstants
        Dim mSecondColor As FPSpreadADO.BackColorStyleConstants
        Dim mHour As Double
        Dim mMin As Double
        Dim mTotMin As Double
        Dim mFHalf As String
        Dim mSHalf As String
        Dim mOTHours As Double

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mDate = VB6.Format(lblRunDate.Text, "DD-MMM-YYYY")
        mDOJ = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        mDOL = "01" & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

        SqlStr = " SELECT DISTINCT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf _
            & " EMP.EMP_DEPT_CODE, WORKING_HOURS," & vbCrLf _
            & " GETEMPSHIFTTIME(EMP.COMPANY_CODE, TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), EMP.EMP_CODE,'I') AS IN_TIME, " & vbCrLf _
            & " GETEMPSHIFTTIME(EMP.COMPANY_CODE, TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), EMP.EMP_CODE,'O') AS OUT_TIME, " & vbCrLf _
            & " GETEMPSHIFTROUND(EMP.COMPANY_CODE, TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), EMP.EMP_CODE,'R') AS ROUND_CLOCK " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And EMP_DOJ <=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        'SqlStr = SqlStr & vbCrLf _
        '    & " AND EMP.COMPANY_CODE = SMST.COMPANY_CODE (+) " & vbCrLf _
        '    & " AND EMP.EMP_CODE = SMST.EMP_CODE (+) " & vbCrLf _
        '    & " AND TO_CHAR(SMST.SHIFT_DATE,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'(+)"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
        End If

        'If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBookNo.Text) <> "" Then
        '    SqlStr = SqlStr & vbCrLf & "AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
        'End If

        'If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPageNo.Text) <> "" Then
        '    SqlStr = SqlStr & vbCrLf & "AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
        'End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        '    SqlStr = SqlStr & vbCrLf _
        ''                    & " AND ATTN_DATE>='" & UCase(Format(txtFrom.Text, "DD-MMM-YYYY")) & "'" & vbCrLf _
        ''                    & " AND ATTN_DATE<='" & UCase(Format(txtTo.Text, "DD-MMM-YYYY")) & "'"


        '    SqlStr = SqlStr & vbCrLf & "Group by EMP.EMP_NAME, EMP.EMP_CODE "

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCard.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        ElseIf optDept.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_DEPT_CODE, EMP.EMP_CODE"
        Else
            'SqlStr = SqlStr & vbCrLf & "Order by SMST.BOOKNO,SMST.PAGENO"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    .MaxRows = cntRow

                    .Row = cntRow

                    mOTHours = 0
                    mFHalf = ""
                    mSHalf = ""

                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColDept
                    .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

                    .Col = ColWorkingHours
                    .Text = IIf(IsDBNull(RsAttn.Fields("WORKING_HOURS").Value), "", RsAttn.Fields("WORKING_HOURS").Value)

                    .Col = ColShiftINTime
                    .Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("IN_TIME").Value), "", RsAttn.Fields("IN_TIME").Value), "HH:MM")

                    .Col = ColShiftOutTime
                    .Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("OUT_TIME").Value), "", RsAttn.Fields("OUT_TIME").Value), "HH:MM")

                    .Col = ColRoundClock
                    mRoundClock = IIf(IsDBNull(RsAttn.Fields("ROUND_CLOCK").Value), "N", RsAttn.Fields("ROUND_CLOCK").Value)

                    '.Col = ColOTHours
                    '.Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("OT_HOURS").Value), "", RsAttn.Fields("OT_HOURS").Value), "HH:MM")

                    '.Col = ColAttnFH
                    '.Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("FIRST_HALF").Value), "", RsAttn.Fields("FIRST_HALF").Value), "HH:MM")

                    '.Col = ColAttnSH
                    '.Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("SECOND_HALF").Value), "", RsAttn.Fields("SECOND_HALF").Value), "HH:MM")

                    .Text = IIf(mRoundClock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    If GetINOUTTime(mCode, mDate, mInTime, mOutTime, mOTHours, mFirstColor, mSecondColor) = False Then GoTo refreshErrPart

                    .Row = cntRow
                    .Col = ColINTime
                    .Text = VB6.Format(mInTime, "HH:MM")
                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = ColINTime
                    .Col2 = ColINTime
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(mFirstColor)
                    .BlockMode = False

                    .Row = cntRow
                    .Col = ColOutTime
                    .Text = VB6.Format(mOutTime, "HH:MM")

                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = ColOutTime
                    .Col2 = ColOutTime
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(mSecondColor)
                    .BlockMode = False

                    .Row = cntRow
                    .Col = ColTotalHours
                    If TimeValue(mInTime) = CDate("00:00:00") Or TimeValue(mOutTime) = CDate("00:00:00") Then
                        .Text = "00:00"
                    Else
                        mTotMin = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mInTime), CDate(mOutTime))
                        mHour = Int(mTotMin / 60)
                        mMin = mTotMin - (mHour * 60)
                        .Text = VB6.Format(mHour, "00") & ":" & VB6.Format(mMin, "00") ''Format(TimeValue(mOutTime) - TimeValue(mInTime), "HH:MM")
                    End If

                    If GetLeaveMark(mCode, mDate, mFHalf, mSHalf) = False Then GoTo refreshErrPart

                    .Col = ColOTHours
                    mOTHours = GetOTHours(mCode, mDate)
                    .Text = mOTHours

                    .Col = ColAttnFH
                    .Text = mFHalf

                    .Col = ColAttnSH
                    .Text = mSHalf

                    .Col = ColMachineData1
                    .Text = GetPunchTime(mCode, DateAdd("d", -1, mDate), "PAY_EMPLOYEE_MST")

                    .Col = ColMachineData2
                    .Text = GetPunchTime(mCode, mDate, "PAY_EMPLOYEE_MST")

                    .Col = ColMachineData
                    .Text = GetPunchTime(mCode, DateAdd("d", 1, mDate), "PAY_EMPLOYEE_MST")

                    cntRow = cntRow + 1
                    RsAttn.MoveNext()
                Loop
            End With
        End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function GetOTHours(ByRef pEmpCode As String, ByRef pAttnDate As String) As Double

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotOTHour As Double
        Dim mTotOTMIN As Double

        mTotOTHour = 0
        mTotOTMIN = 0
        GetOTHours = 0

        GetOTHours = 0

        SqlStr = "SELECT OT.OT_DATE, OT.OTHOUR , OT.OTMIN, OT.PREV_OTHOUR, OT.PREV_OTMIN " & vbCrLf _
                & " FROM PAY_OVERTIME_MST OT " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And OT.EMP_CODE='" & pEmpCode & "' " & vbCrLf _
                & " AND OT.OT_DATE=TO_DATE('" & VB6.Format(pAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " ORDER BY OT.OT_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            '.Text = CStr(IIf(IsDBNull(RsOT.Fields("OTHOUR").Value), 0, RsOT.Fields("OTHOUR").Value))
            '.Text = CStr(IIf(IsDBNull(RsOT.Fields("OTMIN").Value), "", .Text & ".") & RsOT.Fields("OTMIN").Value)

            mTotOTHour = IIf(IsDBNull(RsTemp.Fields("OTHOUR").Value), 0, RsTemp.Fields("OTHOUR").Value)
            mTotOTMIN = IIf(IsDBNull(RsTemp.Fields("OTMIN").Value), 0, RsTemp.Fields("OTMIN").Value)

            GetOTHours = VB6.Format(GetTOTOverTime(mTotOTHour, mTotOTMIN), "0.00")
        End If

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function GetLeaveMark(ByRef pEmpCode As String, ByRef pAttnDate As String, ByRef mFHalf As String, ByRef mSHalf As String) As Boolean

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFMark As Integer
        Dim mSMark As Integer

        mFHalf = ""
        mSHalf = ""
        GetLeaveMark = False
        SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mFMark = IIf(IsDBNull(RsTemp.Fields("FIRSTHALF").Value), -1, RsTemp.Fields("FIRSTHALF").Value)
            mSMark = IIf(IsDBNull(RsTemp.Fields("SECONDHALF").Value), -1, RsTemp.Fields("SECONDHALF").Value)

            If mFMark = ABSENT Then
                mFHalf = "ABSENT"
            ElseIf mFMark = CASUAL Then
                mFHalf = "CASUAL"
            ElseIf mFMark = EARN Then
                mFHalf = "EARN"
            ElseIf mFMark = SICK Then
                mFHalf = "SICK"
            ElseIf mFMark = MATERNITY Then
                mFHalf = "MATERNITY"
            ElseIf mFMark = CPLEARN Then
                mFHalf = "CPLEARN"
            ElseIf mFMark = WOPAY Then
                mFHalf = "WOPAY"
            ElseIf mFMark = CPLAVAIL Then
                mFHalf = "CPLAVAIL"
            ElseIf mFMark = SUNDAY Then
                mFHalf = "SUNDAY"
            ElseIf mFMark = HOLIDAY Then
                mFHalf = "HOLIDAY"
            ElseIf mFMark = PRESENT Then
                mFHalf = "PRESENT"
            End If

            If mSMark = ABSENT Then
                mSHalf = "ABSENT"
            ElseIf mSMark = CASUAL Then
                mSHalf = "CASUAL"
            ElseIf mSMark = EARN Then
                mSHalf = "EARN"
            ElseIf mSMark = SICK Then
                mSHalf = "SICK"
            ElseIf mSMark = MATERNITY Then
                mSHalf = "MATERNITY"
            ElseIf mSMark = CPLEARN Then
                mSHalf = "CPLEARN"
            ElseIf mSMark = WOPAY Then
                mSHalf = "WOPAY"
            ElseIf mSMark = CPLAVAIL Then
                mSHalf = "CPLAVAIL"
            ElseIf mSMark = SUNDAY Then
                mSHalf = "SUNDAY"
            ElseIf mSMark = HOLIDAY Then
                mSHalf = "HOLIDAY"
            ElseIf mSMark = PRESENT Then
                mSHalf = "PRESENT"
            End If
        End If
        GetLeaveMark = True
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function GetINOUTTime(ByRef mEmpCode As String, ByRef pDate As String, ByRef mInTime As String, ByRef mOutTime As String, ByRef mOTHours As Double,
                                  ByRef mFirstColor As FPSpreadADO.BackColorStyleConstants, ByRef mSecondColor As FPSpreadADO.BackColorStyleConstants) As Boolean
        On Error GoTo ErrPart

        Dim mEmpShiftIN As String
        Dim mEmpShiftOUT As String
        Dim mEmpShiftBreak As String

        Dim mMarginsMinute As Double
        'Dim mEmpInTime As String
        'Dim mEmpOutTime As String
        Dim mSLTime As String
        Dim mSLOutTime As String
        Dim mIsRoundClock As String
        Dim mShortLeave As Boolean
        Dim mFirstIsOD As Boolean
        Dim mSecondIsOD As Boolean
        Dim mISFirstLeave As Boolean
        Dim mISSecondLeave As Boolean
        Dim mCPLFirstEarn As Boolean
        Dim mCPLFirstAvail As Boolean
        Dim mCPLSecondEarn As Boolean
        Dim mCPLSecondAvail As Boolean
        Dim mISFirstShortLeave As Boolean
        Dim mISSecondShortLeave As Boolean
        Dim mFromTime As String
        Dim mToTime As String
        Dim mFirstAbsent As Boolean
        Dim mSecondAbsent As Boolean
        Dim mFirstManual As Boolean
        Dim mSecondManual As Boolean

        GetINOUTTime = False

        mFromTime = ""
        mToTime = ""

        If GetIsHolidays(pDate, "", mEmpCode, "", "Y") = True Then
            mInTime = "00:00"
            mOutTime = "00:00"
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
            GetINOUTTime = True
            Exit Function
        End If


        mMarginsMinute = IIf(IsDBNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mIsRoundClock = IIf(GetRoundClock(mEmpCode, pDate, "E") = True, "Y", "N")

        mEmpShiftIN = GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "I", mIsRoundClock, "E")
        mEmpShiftOUT = GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "O", mIsRoundClock, "E")

        If mEmpShiftIN = "00:00" Then
            mEmpShiftIN = "09:30"
        End If

        If mEmpShiftOUT = "00:00" Then
            mEmpShiftOUT = "18:00"
        End If

        '    mEmpShiftBreak = CVDate(Format(DateSerial(Year(mEmpShiftIN), Month(mEmpShiftIN), Day(mEmpShiftIN)) & " " & TimeSerial(Hour(mEmpShiftIN) + 4, Minute(mEmpShiftIN), 0), "DD/MM/YYYY HH:MM"))    ''GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "B", "E")
        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))
        mSLTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 2, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
        mSLOutTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, -2, CDate(mEmpShiftOUT)), "DD/MM/YYYY HH:MM")))

        mISFirstLeave = CheckLeave(mEmpCode, pDate, "L", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
        mCPLFirstEarn = CheckLeave(mEmpCode, pDate, "CE", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
        mCPLFirstAvail = CheckLeave(mEmpCode, pDate, "CA", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
        mFirstAbsent = CheckLeave(mEmpCode, pDate, "AB", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
        mFirstManual = CheckLeave(mEmpCode, pDate, "M", "I", mEmpShiftIN, mEmpShiftBreak, "", "")


        mISSecondLeave = CheckLeave(mEmpCode, pDate, "L", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
        mCPLSecondEarn = CheckLeave(mEmpCode, pDate, "CE", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
        mCPLSecondAvail = CheckLeave(mEmpCode, pDate, "CA", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
        mSecondAbsent = CheckLeave(mEmpCode, pDate, "AB", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
        mSecondManual = CheckLeave(mEmpCode, pDate, "M", "O", mEmpShiftIN, mEmpShiftBreak, "", "")


        mShortLeave = False
        mFirstIsOD = False
        mSecondIsOD = False

        'DateSerial(year(mEmpShiftOUT), month(mEmpShiftOUT), day(mEmpShiftOUT))

        If CheckEmpTime(mEmpCode, pDate, mInTime, mOutTime, mOTHours, mIsRoundClock, mFirstIsOD, mSecondIsOD, mEmpShiftBreak) = False Then GoTo ErrPart

        mISFirstShortLeave = CheckLeave(mEmpCode, pDate, "P", "I", mInTime, mEmpShiftBreak, mFromTime, mToTime)
        mISSecondShortLeave = CheckLeave(mEmpCode, pDate, "P", "O", mInTime, mEmpShiftBreak, mFromTime, mToTime)

        If mISFirstLeave = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
        ElseIf mCPLFirstEarn = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
        ElseIf mFirstAbsent = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
        ElseIf mCPLFirstAvail = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
        ElseIf mFirstManual = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
        ElseIf VB6.Format(mInTime, "HH:MM") = "00:00" Then
            If mISFirstShortLeave = True Then
                mInTime = pDate & " " & mToTime
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
            Else
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
            End If
        ElseIf CDate(mInTime) > CDate(mEmpShiftIN) And CDate(mInTime) <= CDate(mSLTime) Then
            If mISFirstShortLeave = True Then
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
            Else
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
            End If
        ElseIf CDate(mInTime) > CDate(mSLTime) Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
        ElseIf mFirstIsOD = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
        Else
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(10).BackColor) '&HFFFFFF
        End If

        If mISSecondLeave = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
        ElseIf mCPLSecondEarn = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
        ElseIf mSecondAbsent = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
        ElseIf mCPLSecondAvail = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
        ElseIf mSecondManual = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
        ElseIf VB6.Format(mOutTime, "HH:MM") = "00:00" Then
            If mISSecondShortLeave = True Then
                mOutTime = pDate & " " & mFromTime
                mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
            Else
                mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
            End If
        ElseIf CDate(mOutTime) >= CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, -5, CDate(mEmpShiftOUT))) Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(10).BackColor)
        ElseIf CDate(mOutTime) >= CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, -5, CDate(mEmpShiftBreak))) And CDate(mOutTime) >= CDate(mSLOutTime) Then  ''If CVDate(DateAdd("n", -5, mEmpShiftBreak)) <= CVDate(mOutTime) And CVDate(mOutTime) >= CVDate(mSLOutTime) Then
            If mISSecondShortLeave = True Then
                mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
            Else
                mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
            End If
        ElseIf CDate(mOutTime) < CDate(mSLOutTime) Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
        ElseIf mSecondIsOD = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
        Else
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(10).BackColor)       '&HFFFFFF
        End If

        GetINOUTTime = True
        Exit Function
ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
        '    Resume
        GetINOUTTime = False
    End Function
    Private Function CheckEmpTime(ByRef mEmpCode As String, ByRef mMonthDate As String, ByRef mEmpInTime As String,
                                   ByRef mEmpOutTime As String, ByRef mOTHours As Double, ByRef mIsRound As String, ByRef mFirstIsOD As Boolean, ByRef mSecondIsOD As Boolean, ByRef mEmpShiftBreak As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEMPODOut As String
        Dim mEmpODIn As String
        Dim mIsODLocal1 As Boolean
        Dim mIsODLocal2 As Boolean

        mEmpInTime = ""
        mEmpOutTime = ""

        mIsODLocal1 = False
        mIsODLocal2 = False
        mFirstIsOD = False
        mSecondIsOD = False

        SqlStr = " SELECT IN_TIME, OUT_TIME, OT_HOURS " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf _
            & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mEmpInTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:mm")
            mEmpOutTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:mm")
            mOTHours = IIf(IsDBNull(RsTemp.Fields("OT_HOURS").Value), 0, RsTemp.Fields("OT_HOURS").Value)

            mEmpInTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpInTime)), Minute(CDate(mEmpInTime)), 0), "DD/MM/YYYY HH:mm")

            If mIsRound = "Y" Then
                mEmpOutTime = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:mm")
            Else
                mEmpOutTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:mm")
            End If
        Else
            mEmpInTime = "00:00"
            mEmpOutTime = "00:00"
            mOTHours = 0
        End If
        mEMPODOut = "00:00"
        mEmpODIn = "00:00"

        SqlStr = " SELECT MIN(TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI')) AS TIME_FROM " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
        '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("TIME_FROM").Value) = False Then
                mIsODLocal1 = True
                mEMPODOut = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "HH:mm")
                mEMPODOut = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEMPODOut)), Minute(CDate(mEMPODOut)), 0), "DD/MM/YYYY HH:mm")
            End If
        End If

        If mIsRound = "Y" Then
            SqlStr = " SELECT MIN(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf _
                & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & UCase(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)), "DD-MMM-YYYY")) & "','DD-MON-YYYY')"

            Dim mCheckTime As String = ""

            mCheckTime = DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 8, CDate(mEmpInTime))

            SqlStr = SqlStr & vbCrLf _
                & " AND TIME_TO <=TO_DATE('" & VB6.Format(mCheckTime, "DD-MMM-YYYY HH:mm") & "','DD-MON-YYYY HH24:MI')"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDBNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:mm")
                    mEmpODIn = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:mm")
                End If
            End If
        Else
            SqlStr = " SELECT MAX(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf _
                & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDBNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:mm")
                    mEmpODIn = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:mm")
                End If
            End If
        End If

        If VB6.Format(mEmpInTime, "HH:mm") = "00:00" And VB6.Format(mEmpOutTime, "HH:mm") = "00:00" Then
            If mIsODLocal1 = True Then
                If VB6.Format(mEMPODOut, "HH:mm") <= VB6.Format(mEmpShiftBreak, "HH:mm") And VB6.Format(mEmpODIn, "HH:mm") <= VB6.Format(mEmpShiftBreak, "HH:mm") Then
                    mFirstIsOD = True
                    mEmpInTime = mEMPODOut
                Else
                    If VB6.Format(mEMPODOut, "HH:mm") <= VB6.Format(mEmpShiftBreak, "HH:mm") Then
                        mFirstIsOD = True
                        mEmpInTime = mEMPODOut
                    Else
                        mFirstIsOD = False
                    End If
                End If

                If VB6.Format(mEmpODIn, "HH:mm") > VB6.Format(mEmpShiftBreak, "HH:mm") Then
                    mSecondIsOD = True
                    mEmpOutTime = mEmpODIn
                Else
                    mSecondIsOD = False
                End If
            Else
                mFirstIsOD = False
            End If
        Else
            If VB6.Format(mEmpInTime, "HH:mm") = "00:00" Then
                mEmpInTime = mEMPODOut
                mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
            Else
                If VB6.Format(mEMPODOut, "HH:mm") <> "00:00" Then
                    If CDate(mEMPODOut) < CDate(mEmpInTime) Then
                        '            mEmpInTime = mEMPODOut
                        mFirstIsOD = True
                    End If
                End If
            End If

            If VB6.Format(mEmpOutTime, "HH:mm") = "00:00" Then
                mEmpOutTime = mEmpODIn
                mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
            Else
                If VB6.Format(mEmpODIn, "HH:mm") <> "00:00" Then
                    If CDate(mEmpODIn) > CDate(mEmpOutTime) Then
                        '            mEmpOutTime = mEmpODIn
                        mSecondIsOD = True
                    End If
                End If
            End If
        End If




        CheckEmpTime = True
        Exit Function
ErrPart:
        CheckEmpTime = False

    End Function
    Private Sub FillGridColor()
        On Error GoTo ErrPart
        Dim mEmpCode As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mBlackColor As Integer
        Dim mIO As String
        Dim mGateTime As String

        Dim mGateINTime As String
        Dim mGateOUTTime As String
        Dim mShiftTime As String
        'Dim mShiftOUTTime As String
        'Dim mLastDay As Long
        'Dim mDay As Long
        Dim mDate As String
        Dim mMarginsMinute As Double
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mISLeave As Boolean
        Dim mCPLEarn As Boolean
        Dim mCPLAvail As Boolean
        Dim mIsOD As Boolean
        Dim mISShortLeave As Boolean
        Dim mLateComer As Boolean
        Dim mFromTime As String
        Dim mToTime As String
        Dim mShiftBreakeTime As String
        Dim I As Integer

        mMarginsMinute = IIf(IsDBNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)

        '    mLastDay = MainClass.LastDay(Month(lblRunDate.Caption), Year(lblRunDate))
        '    mDay = Day(lblRunDate.Caption)

        mDate = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")

        With sprdAttn
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColCard
                If mEmpCode <> Trim(.Text) Then
                    mEmpCode = Trim(.Text)
                End If
                If mEmpCode = "" Then GoTo NextRecd

                .Col = ColINTime
                mGateINTime = Trim(.Text)
                mGateINTime = IIf(mGateINTime = "", "00:00", mGateINTime)

                .Col = ColOutTime
                mGateOUTTime = Trim(.Text)
                mGateOUTTime = IIf(mGateOUTTime = "", "00:00", mGateOUTTime)

                '            .Col = ColShiftINTime
                '            mShiftINTime = Trim(.Text)
                '            mShiftINTime = IIf(mShiftINTime = "", "00:00", mShiftINTime)
                '
                '            .Col = ColShiftOutTime
                '            mShiftOUTTime = Trim(.Text)
                '            mShiftOUTTime = IIf(mShiftOUTTime = "", "00:00", mShiftOUTTime)


                For I = 1 To 2
                    mIO = IIf(I = 1, "I", "O")
                    cntCol = IIf(I = 1, ColINTime, ColOutTime)

                    If I = 1 Then
                        mGateTime = mGateINTime
                    Else
                        mGateTime = mGateOUTTime
                    End If
                    mShiftTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, mIO, "E")

                    mISLeave = False
                    mIsOD = False
                    mISShortLeave = False
                    mFromTime = ""
                    mToTime = ""
                    If GetIsHolidays(mDate, "", mEmpCode, "", "Y") = True Then
                        .Row = cntRow
                        .Row2 = cntRow
                        .Col = cntCol
                        .Col2 = cntCol
                        .BlockMode = True
                        .BackColor = lblColor(3).BackColor ''mBlackColor            ''&HFFFF00
                        .BlockMode = False

                    Else
                        mISLeave = CheckLeave(mEmpCode, mDate, "L", mIO, mGateTime, mShiftBreakeTime, "", "")
                        mCPLEarn = CheckLeave(mEmpCode, mDate, "CE", mIO, mGateTime, mShiftBreakeTime, "", "")
                        mCPLAvail = CheckLeave(mEmpCode, mDate, "CA", mIO, mGateTime, mShiftBreakeTime, "", "")
                        If mISLeave = True Or mCPLEarn = True Or mCPLAvail = True Then
                            .Row = cntRow
                            .Row2 = cntRow
                            .Col = cntCol
                            .Col2 = cntCol
                            .BlockMode = True
                            If mISLeave = True Then
                                .BackColor = lblColor(4).BackColor
                            ElseIf mCPLEarn = True Then
                                .BackColor = lblColor(6).BackColor
                            Else
                                .BackColor = lblColor(7).BackColor
                            End If
                            .BlockMode = False
                        Else
                            mShiftBreakeTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, "B", "E")
                            mIsOD = CheckLeave(mEmpCode, mDate, "O", mIO, mGateTime, mShiftBreakeTime, mFromTime, mToTime)
                            If mIsOD = True Then
                                .Row = cntRow
                                .Row2 = cntRow
                                .Col = cntCol
                                .Col2 = cntCol
                                .BlockMode = True
                                .BackColor = lblColor(1).BackColor
                                .BlockMode = False
                            Else
                                mShiftBreakeTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, "B", "E")
                                mISShortLeave = CheckLeave(mEmpCode, mDate, "P", mIO, mGateTime, mShiftBreakeTime, mFromTime, mToTime)
                                If mISShortLeave = True Then
                                    .Row = cntRow
                                    .Row2 = cntRow
                                    .Col = cntCol
                                    .Col2 = cntCol
                                    .BlockMode = True
                                    .BackColor = lblColor(5).BackColor
                                    .BlockMode = False
                                Else
                                    If mIO = "I" Then
                                        mShiftBreakeTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, "B", "E")
                                        If CDate(mGateTime) > CDate(mShiftTime) Then
                                            If CDate(mGateTime) <= CDate(VB6.Format(TimeSerial(Hour(CDate(mShiftBreakeTime)) - 2, Minute(CDate(mShiftBreakeTime)) - 30, 0), "HH:MM")) Then
                                                .Row = cntRow
                                                .Row2 = cntRow
                                                .Col = cntCol
                                                .Col2 = cntCol
                                                .BlockMode = True
                                                .BackColor = lblColor(2).BackColor
                                                .BlockMode = False
                                            Else
                                                .Row = cntRow
                                                .Row2 = cntRow
                                                .Col = cntCol
                                                .Col2 = cntCol
                                                .BlockMode = True
                                                .BackColor = lblColor(0).BackColor
                                                .BlockMode = False
                                            End If
                                        ElseIf mGateTime = "00:00" Then
                                            .Row = cntRow
                                            .Row2 = cntRow
                                            .Col = cntCol
                                            .Col2 = cntCol
                                            .BlockMode = True
                                            .BackColor = lblColor(0).BackColor
                                            .BlockMode = False
                                        End If
                                    Else
                                        If mGateTime = "00:00" Then
                                            .Row = cntRow
                                            .Row2 = cntRow
                                            .Col = cntCol
                                            .Col2 = cntCol
                                            .BlockMode = True
                                            .BackColor = lblColor(0).BackColor
                                            .BlockMode = False
                                        ElseIf CDate(mGateTime) < CDate(mShiftTime) Then
                                            If CDate(mGateTime) > CDate(VB6.Format(TimeSerial(Hour(CDate(mShiftBreakeTime)), Minute(CDate(mShiftBreakeTime)), 0), "HH:MM")) And CDate(mGateTime) >= CDate(VB6.Format(TimeSerial(Hour(CDate(mShiftTime)) - 2, Minute(CDate(mShiftTime)), 0), "HH:MM")) Then
                                                .Row = cntRow
                                                .Row2 = cntRow
                                                .Col = cntCol
                                                .Col2 = cntCol
                                                .BlockMode = True
                                                .BackColor = lblColor(2).BackColor
                                                .BlockMode = False
                                            Else
                                                .Row = cntRow
                                                .Row2 = cntRow
                                                .Col = cntCol
                                                .Col2 = cntCol
                                                .BlockMode = True
                                                .BackColor = lblColor(0).BackColor
                                                .BlockMode = False
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                Next
NextRecd:
            Next
        End With
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Function CheckLeave(ByRef pEmpCode As String, ByRef pDate As String, ByRef pCheckType As String, ByRef pHalf As String, ByRef mOutTime As String, ByRef mShiftBreakeTime As String, ByRef pFromDate As String, ByRef pToDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pField As String

        pFromDate = ""
        pToDate = ""

        If pCheckType = "L" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " NOT IN (-1," & CPLEARN & "," & CPLAVAIL & "," & ABSENT & "," & WOPAY & "," & PRESENT & ")"
        ElseIf pCheckType = "AB" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " IN (" & ABSENT & "," & WOPAY & ")"
        ElseIf pCheckType = "CE" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " = " & CPLEARN & ""
        ElseIf pCheckType = "CA" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & "= " & CPLAVAIL & ""
        ElseIf pCheckType = "O" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE ='O'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            If pHalf = "I" Then
                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'"
            Else
                If mOutTime = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_TO,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')"
                End If
            End If
        ElseIf pCheckType = "M" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE ='M'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            If pHalf = "I" Then
                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'"
            Else
                If mOutTime = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_TO,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')"
                End If
            End If
        ElseIf pCheckType = "P" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='P' AND AGT_LEAVE='N'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            If pHalf = "I" Then

                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'" ''25-01-2018 now date consider

                '            SqlStr = SqlStr & vbCrLf & "AND TIME_FROM <='" & VB6.Format(mShiftBreakeTime, "DD-MMM-YYYY HH:MM") & "'"
            Else
                If mOutTime = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_TO,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')" ''25-01-2018 now date consider
                    '                SqlStr = SqlStr & vbCrLf & "AND (TIME_TO>='" & VB6.Format(mShiftBreakeTime, "DD-MMM-YYYY HH:MM") & "')"
                End If
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If pCheckType = "L" Or pCheckType = "AB" Or pCheckType = "CA" Or pCheckType = "CE" Then

            Else
                pFromDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_FROM").Value), "", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
                pToDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "", RsTemp.Fields("TIME_TO").Value), "HH:MM")
            End If

            CheckLeave = True
        Else
            CheckLeave = False
        End If
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Function


    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = "Select DEPT_DESC " & vbCrLf & " FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboDept.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboDept.SelectedIndex = 0

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCatgeory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCatgeory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCatgeory.SelectedIndex = 0

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("Present")
        cboShow.Items.Add("Absent")
        cboShow.Items.Add("Leave")
        cboShow.Items.Add("Late Comers")
        cboShow.Items.Add("Out Duty")
        cboShow.Items.Add("Blank Out Time")
        cboShow.Items.Add("Early Going")
        cboShow.Items.Add("Late Comers Summary")
        cboShow.Items.Add("Early Going Summary")
        cboShow.SelectedIndex = 0

        '    cboCatgeory.Clear
        '    cboCatgeory.AddItem "General Staff"
        '    cboCatgeory.AddItem "Production Staff"
        '    cboCatgeory.AddItem "Export Staff"
        '    cboCatgeory.AddItem "Regular Worker"
        '    cboCatgeory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCatgeory.AddItem "Director"
        '    cboCatgeory.AddItem "Trainee Staff"
        '    cboCatgeory.ListIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub sprdAttn_DblClick(sender As Object, EventArgs As _DSpreadEvents_DblClickEvent) Handles sprdAttn.DblClick
        On Error GoTo ErrPart
        Dim mCode As String
        Dim mCategory As String

        If EventArgs.row = 0 Or EventArgs.col = 0 Then Exit Sub

        sprdAttn.Row = EventArgs.row
        sprdAttn.Col = ColCard
        mCode = sprdAttn.Text

        If Trim(sprdAttn.Text) = "" Then Exit Sub

        If EventArgs.col = ColINTime Or EventArgs.col = ColOutTime Then
            frmAttnInOutMark.lblCode.Text = sprdAttn.Text
            mCode = sprdAttn.Text

            sprdAttn.Col = ColINTime
            frmAttnInOutMark.txtINTime.Text = sprdAttn.Text

            sprdAttn.Col = ColOutTime
            frmAttnInOutMark.txtOUTTime.Text = sprdAttn.Text


            frmAttnInOutMark.lblDate.Text = lblRunDate.Text

            frmAttnInOutMark.ShowDialog()
        End If

        If EventArgs.col = ColOTHours Then
            frmOverTimeHead.lblCode.Text = sprdAttn.Text
            mCode = sprdAttn.Text

            sprdAttn.Col = 2
            frmOverTimeHead.lblEmpName.Text = sprdAttn.Text

            frmOverTimeHead.lblDate.Text = lblRunDate.Text
            If ChechJoinLeaveDate(lblRunDate.Text, mCode) = False Then Exit Sub

            frmOverTimeHead.lblType.Text = "1"
            frmOverTimeHead.ShowDialog()
        End If

        If EventArgs.col = ColAttnFH Or EventArgs.col = ColAttnSH Then

            frmEmpLeaveEntry.MdiParent = Me.MdiParent

            If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CAT_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCategory = MasterNo
            Else
                mCategory = "1"
            End If

            frmEmpLeaveEntry.lblCategory.Text = IIf(mCategory = "1", "S", "W")

            frmEmpLeaveEntry.Show()
            frmEmpLeaveEntry.frmEmpLeaveEntry_Activated(Nothing, New System.EventArgs())

            frmEmpLeaveEntry.txtEmpCode.Text = mCode

            frmEmpLeaveEntry.txtRefDate.Text = "01" & VB6.Format(lblRunDate.Text, "MM/YYYY")

            sprdAttn.Col = 2
            frmEmpLeaveEntry.TxtEmpName.Text = sprdAttn.Text

            frmEmpLeaveEntry.txtEmpCode_Validating(frmEmpLeaveEntry.txtEmpCode, New System.ComponentModel.CancelEventArgs(False))

            'frmEmpLeaveEntry.ShowDialog()

            frmEmpLeaveEntry.Activate()


        End If

        Exit Sub
ErrPart:

    End Sub
    Private Function ChechJoinLeaveDate(ByRef mDays As String, ByRef mCode As String) As Boolean

        Dim SqlStr As String = ""
        Dim RsTempJL As ADODB.Recordset = Nothing

        SqlStr = " SELECT EMP_DOJ,EMP_LEAVE_DATE FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE EMP_CODE = '" & mCode & "' And " & vbCrLf _
            & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempJL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTempJL.EOF = False Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(RsTempJL.Fields("EMP_DOJ").Value, "dd/mm/yyyy")), CDate(VB6.Format(mDays, "dd/mm/yyyy"))) >= 0 Then
                ChechJoinLeaveDate = True
            Else
                MsgInformation("Employee Joining Date is Greater then Current Date.")
                ChechJoinLeaveDate = False
                Exit Function
            End If
            If IsDBNull(RsTempJL.Fields("EMP_LEAVE_DATE").Value) Then
                ChechJoinLeaveDate = True
            ElseIf DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(mDays, "dd/mm/yyyy")), CDate(VB6.Format(RsTempJL.Fields("EMP_LEAVE_DATE").Value, "dd/mm/yyyy"))) < 0 Then
                MsgInformation("Employee Leaving Date is Less then Current Date.")
                ChechJoinLeaveDate = False
                Exit Function
            Else
                ChechJoinLeaveDate = True
            End If
        End If
    End Function

    Private Sub txtEmpCode_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub


    Private Sub UpdateMarkForNotPunch(ByRef pLeaveType As Object)

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mEmpCode As String
        Dim mDate As String
        Dim mDay As Integer
        Dim mIO As String
        Dim SqlStr As String = ""
        Dim mFieldName As String
        Dim mColor As FPSpreadADO.BackColorStyleConstants
        Dim mLastDayofMonth As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSqlStr As String
        Dim mFHalf As Integer
        Dim mFSecond As Integer

        mDate = lblRunDate.Text

        With sprdAttn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCard
                mEmpCode = Trim(.Text)

                .Col = ColINTime
                mColor = System.Drawing.ColorTranslator.ToOle(.BackColor)
                If mColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) Then

                    pSqlStr = " SELECT * FROM PAY_ATTN_MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                        & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        mFHalf = pLeaveType
                        SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf _
                            & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                            & " ATTN_DATE, FIRSTHALF, " & vbCrLf _
                            & " AGT_LATE, CPL_AGT_DATE_FH," & vbCrLf _
                            & " CPL_AGT_DATE_SH, CPL_EARN, " & vbCrLf _
                            & " ADDUSER, ADDDATE " & vbCrLf _
                            & ") VALUES (" & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & vbCrLf _
                            & " '" & mEmpCode & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " " & mFHalf & ",  " & vbCrLf _
                            & " 'N', '', " & vbCrLf & " '', 0, " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    Else
                        SqlStr = "UPDATE PAY_ATTN_MST SET FIRSTHALF= " & pLeaveType & "" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If

                    PubDBCn.Execute(SqlStr)
                End If

                .Col = ColOutTime
                mColor = System.Drawing.ColorTranslator.ToOle(.BackColor)
                If mColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) Then

                    pSqlStr = " SELECT * FROM PAY_ATTN_MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                        & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        mFHalf = pLeaveType
                        SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf _
                            & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                            & " ATTN_DATE, SECONDHALF, " & vbCrLf _
                            & " AGT_LATE, CPL_AGT_DATE_FH," & vbCrLf _
                            & " CPL_AGT_DATE_SH, CPL_EARN, " & vbCrLf _
                            & " ADDUSER, ADDDATE " & vbCrLf _
                            & ") VALUES (" & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & vbCrLf _
                            & " '" & mEmpCode & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " " & mFHalf & ",  " & vbCrLf _
                            & " 'N', '', " & vbCrLf & " '', 0, " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    Else
                        SqlStr = "UPDATE PAY_ATTN_MST SET SECONDHALF= " & pLeaveType & "" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If

                    PubDBCn.Execute(SqlStr)
                End If


            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdWOPay_Click_1(sender As Object, e As EventArgs) Handles cmdWOPay.Click
        On Error GoTo ErrPart

        Call UpdateMarkForNotPunch(WOPAY)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdAbsent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAbsent.Click
        On Error GoTo ErrPart

        Call UpdateMarkForNotPunch(ABSENT)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub sprdAttn_Advance(sender As Object, e As _DSpreadEvents_AdvanceEvent) Handles sprdAttn.Advance

    End Sub
End Class
