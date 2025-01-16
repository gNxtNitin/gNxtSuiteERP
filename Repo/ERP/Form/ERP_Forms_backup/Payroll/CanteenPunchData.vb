Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCanteenPunchData
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
    Private Const ColPunchTime1 As Short = 4
    Private Const ColPunchTime2 As Short = 5
    Private Const ColPunchTime3 As Short = 6
    Private Const ColPunchTime4 As Short = 7
    Private Const ColPunchTime5 As Short = 8
    Private Const ColPunchTime6 As Short = 9
    Private Const ColPunchTime7 As Short = 10
    Private Const ColPunchTime8 As Short = 11
    Private Const ColPunchTime9 As Short = 12
    Private Const ColPunchTime10 As Short = 13
    Private Const ColPunchTime11 As Short = 14
    Private Const ColPunchTime12 As Short = 15
    Private Const ColPunchTime13 As Short = 16
    Private Const ColPunchTime14 As Short = 17
    Private Const ColPunchTime15 As Short = 18
    Private Const ColPunchTime16 As Short = 19
    Private Const ColPunchTime17 As Short = 20
    Private Const ColPunchTime18 As Short = 21
    Private Const ColPunchTime19 As Short = 22
    Private Const ColPunchTime20 As Short = 23
    Private Const ColPunchTime21 As Short = 24
    Private Const ColPunchTime22 As Short = 25
    Private Const ColPunchTime23 As Short = 26
    Private Const ColPunchTime24 As Short = 27
    Private Const ColPunchTime25 As Short = 28
    Private Const ColPunchTime26 As Short = 29
    Private Const ColPunchTime27 As Short = 30
    Private Const ColPunchTime28 As Short = 31
    Private Const ColPunchTime29 As Short = 32
    Private Const ColPunchTime30 As Short = 33
    Private Const ColPunchTime31 As Short = 34
    Private Const ColEmpType As Short = 35

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub FillHeading()

        Dim cntCol As Integer
        Dim I As Integer
        Dim cellheight As Integer

        MainClass.ClearGrid(sprdAttn)


        With sprdAttn
            .MaxCols = ColEmpType

            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(-1, ConRowHeight * 2)

            .Row = -1

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColCard, 7)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 26)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 6)

            For cntCol = ColPunchTime1 To ColPunchTime31
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .set_ColWidth(cntCol, 5)
                .TypeEditMultiLine = True
            Next

            .Col = ColEmpType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColEmpType, 15)

            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCard
            .Text = "Emp Card No"


            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDept
            .Text = "Dept."

            .Col = ColPunchTime1
            If optShow(0).Checked = True Then
                .Text = "1"
            ElseIf optShow(1).Checked = True Then
                .Text = "Punch Time"
            Else
                .Text = "In Time"
            End If



            .Col = ColPunchTime2
            If optShow(0).Checked = True Then
                .Text = "2"
            ElseIf optShow(1).Checked = True Then
                .Text = "Punch Time"
            Else
                .Text = "Out Time"
            End If
            .ColHidden = IIf(optShow(0).Checked = True Or optShow(2).Checked = True, False, True)

            .Col = ColPunchTime3
            .Text = "3"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime4
            .Text = "4"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime5
            .Text = "5"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime6
            .Text = "6"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime7
            .Text = "7"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime8
            .Text = "8"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime9
            .Text = "9"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime10
            .Text = "10"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime11
            .Text = "11"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime12
            .Text = "12"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime13
            .Text = "13"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime14
            .Text = "14"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime15
            .Text = "15"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime16
            .Text = "16"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime17
            .Text = "17"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime18
            .Text = "18"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime19
            .Text = "19"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime20
            .Text = "20"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime21
            .Text = "21"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime22
            .Text = "22"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime23
            .Text = "23"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime24
            .Text = "24"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime25
            .Text = "25"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime26
            .Text = "26"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime27
            .Text = "27"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime28
            .Text = "28"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime29
            .Text = "29"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime30
            .Text = "30"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPunchTime31
            .Text = "31"
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColEmpType
            .Text = "Employee Type"

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, ColEmpType)
            MainClass.SetSpreadColor(sprdAttn, -1)
            sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        End With



        '        sprdAttn.OperationMode = OperationModeNormal
        '        sprdAttn.DAutoCellTypes = True
        '        sprdAttn.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        '        sprdAttn.GridColor = &HC00000

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
    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCatgeory.Enabled = False
        Else
            cboCatgeory.Enabled = True
        End If
    End Sub

    Private Sub chkContractor_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkContractor.CheckStateChanged
        If chkContractor.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboConName.Enabled = False
        Else
            cboConName.Enabled = True
        End If
    End Sub

    Private Sub chkShow_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShow.CheckStateChanged
        If chkShow.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboShow.Enabled = False
        Else
            cboShow.Enabled = True
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
        PubDBCn.Errors.Clear()

        ''Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "For The Month : " & VB6.Format(lblRunDate.Text, "MMM-YYYY")
        mTitle = "Employee Punching Report"

        If chkCategory.CheckState = System.Windows.Forms.CheckState.UnChecked And cboCatgeory.Text <> "" Then
            mSubTitle = mSubTitle & " - " & cboCatgeory.Text
        End If

        If chkShow.CheckState = System.Windows.Forms.CheckState.UnChecked And cboShow.Text <> "" Then
            mSubTitle = mSubTitle & " - " & cboShow.Text
        End If

        If lblBookType.Text = "C" Then
            If chkContractor.CheckState = System.Windows.Forms.CheckState.UnChecked And cboConName.Text <> "" Then
                mSubTitle = mSubTitle & " - " & cboConName.Text
            End If
        End If

        Call ShowReport(SqlStr, "PunchdataReport.Rpt", Mode, mTitle, mSubTitle)

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

        On Error GoTo ErrPart
        Dim mTable As String

        If lblBookType.Text = "E" Then
            mTable = "PAY_EMPLOYEE_MST"
        Else
            mTable = "PAY_CONT_EMPLOYEE_MST"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtEmpCode.Text) = "" Then
                MsgInformation("Please Select Code")
                txtEmpCode.Focus()
                Exit Sub
            End If

            txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", mTable, PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Employee Code ")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If

        FillHeading()
        RefreshScreen()
        MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, ColEmpType)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub frmCanteenPunchData_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen

        If FormActive = False Then
            FillDeptCombo()
        End If
        If lblBookType.Text = "E" Then
            optShow(2).Enabled = False
            optShow(2).Visible = False
        End If

        FormActive = True
        Me.Text = "Canteen Punch Data"
    End Sub

    Private Sub frmCanteenPunchData_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        '    FillDeptCombo

        If optShow(0).Checked = True Then
            lblRunDate.Text = VB6.Format(RunDate, "MMM-YYYY")
        Else
            lblRunDate.Text = VB6.Format(RunDate, "DD-MMM-YYYY")
        End If



        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCatgeory.Enabled = False
        chkContractor.CheckState = System.Windows.Forms.CheckState.Checked
        cboConName.Enabled = False

        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        chkShow.CheckState = System.Windows.Forms.CheckState.Checked
        cboShow.Enabled = False
        FraShow.Enabled = False

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub frmCanteenPunchData_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)
            SetDate(CDate(lblRunDate.Text))
            If Index = 2 Then
                FraShow.Enabled = True
            Else
                cboShow.Enabled = False
                chkShow.CheckState = System.Windows.Forms.CheckState.Checked
                FraShow.Enabled = False
            End If
        End If
    End Sub

    Private Sub UpDYear_DownClick()


        If optShow(0).Checked = True Then
            lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)), "MMM-YYYY")
        Else
            lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(lblRunDate.Text)), "DD-MMM-YYYY")
        End If
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdAttn, -1)
        '' RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()


        If optShow(0).Checked = True Then
            lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)), "MMM-YYYY")
        Else
            lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(lblRunDate.Text)), "DD-MMM-YYYY")
        End If
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdAttn, -1)
        ''RefreshScreen
    End Sub


    Private Sub SetDate(ByRef xDate As Date)
        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        If optShow(0).Checked = True Then
            Tempdate = "01/" & Month(lblRunDate.Text) & "/" & Year(lblRunDate.Text)
            NewDate = CDate(VB6.Format(Tempdate, "MMM-YYYY"))
            lblRunDate.Text = VB6.Format(NewDate, "MMM-YYYY")
        Else
            Tempdate = VB.Day(lblRunDate.Text) & "/" & Month(lblRunDate.Text) & "/" & Year(lblRunDate.Text)
            NewDate = CDate(VB6.Format(Tempdate, "DD-MMM-YYYY"))
            lblRunDate.Text = VB6.Format(NewDate, "DD-MMM-YYYY")
        End If
        '    lblRunDate.Caption = Format(xDate, "DD-MMM-YYYY")
        '
        '    Daysinmonth = MainClass.LastDay(Format(xDate, "mm"), Format(xDate, "yyyy"))
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCode As String

        Dim cntRow As Integer
        Dim cntCol As Integer

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
        Dim mCellHeight As Integer
        Dim mTable As String
        Dim mContractorName As String
        Dim mAddNewLine As Boolean
        Dim mInTime As String
        Dim mOutTime As String

        If lblBookType.Text = "E" Then
            mTable = "PAY_EMPLOYEE_MST"
        Else
            mTable = "PAY_CONT_EMPLOYEE_MST"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mDOJ = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))
        mDOL = "01" & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))

        SqlStr = " SELECT DISTINCT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf & " EMP.EMP_DEPT_CODE,"

        If lblBookType.Text = "E" Then
            SqlStr = SqlStr & "'EMPLOYEE' AS EMPTYPE"
        Else
            SqlStr = SqlStr & "CONTRACTOR_CODE AS EMPTYPE"
        End If

        SqlStr = SqlStr & vbCrLf & " FROM " & mTable & " EMP " & vbCrLf & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND EMP_DOJ<=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"


        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If lblBookType.Text = "E" Then
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
            End If
        Else
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT='" & VB.Left(cboCatgeory.Text, 1) & "' "
            End If
            If chkContractor.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(cboConName.Text, "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mContCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & "AND CONTRACTOR_CODE='" & MainClass.AllowSingleQuote(Trim(mContCode)) & "' "
                End If
            End If
        End If

        If lblBookType.Text = "E" Then
            SqlStr = SqlStr & vbCrLf & "ORDER BY "
        Else
            SqlStr = SqlStr & vbCrLf & "ORDER BY " 'CONTRACTOR_CODE,
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & "EMP.EMP_NAME"
        ElseIf optCard.Checked = True Then
            SqlStr = SqlStr & "EMP.EMP_CODE"
        ElseIf optDept.Checked = True Then
            SqlStr = SqlStr & "EMP_DEPT_CODE, EMP.EMP_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    mAddNewLine = True
                    .MaxRows = cntRow

                    .Row = cntRow

                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColDept
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

                    mDays = 1
                    If optShow(0).Checked = True Then
                        For cntCol = ColPunchTime1 To ColPunchTime31
                            mDate = mDays & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
                            mDate = VB6.Format(mDate, "DD/MM/YYYY")
                            .Col = cntCol
                            .Text = GetPunchTime(mCode, mDate, mTable)

                            mDays = mDays + 1
                            If mDays > MainClass.LastDay(Month(CDate(mDate)), Year(CDate(mDate))) Then
                                Exit For
                            End If
                            '                .Text = Format(IIf(IsNull(RsAttn!OFFICEPUNCH), "", RsAttn!OFFICEPUNCH), "HH:MM")
                        Next
                    ElseIf optShow(1).Checked = True Then
                        mDate = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")
                        .Col = ColPunchTime1
                        .Text = GetPunchTime(mCode, mDate, mTable)
                    ElseIf optShow(2).Checked = True Then
                        mDate = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")
                        .Col = ColPunchTime1
                        mInTime = GetPunchIOTime(mCode, mDate, mTable, "I")
                        .Text = mInTime

                        .Col = ColPunchTime2
                        mOutTime = GetPunchIOTime(mCode, mDate, mTable, "O")
                        .Text = mOutTime
                        If chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked And cboShow.SelectedIndex <> -1 Then
                            If cboShow.SelectedIndex = 0 Then ''"Present"
                                If Trim(mInTime) <> "" Then
                                    mAddNewLine = True
                                Else
                                    mAddNewLine = False
                                End If
                            ElseIf cboShow.SelectedIndex = 1 Then  ''"Absent"
                                If Trim(mInTime) = "" Then
                                    mAddNewLine = True
                                Else
                                    mAddNewLine = False
                                End If
                            ElseIf cboShow.SelectedIndex = 2 Then  ''"Miss Punch"
                                If (Trim(mInTime) = "" And Trim(mOutTime) <> "") Or (Trim(mInTime) <> "" And Trim(mOutTime) = "") Then
                                    mAddNewLine = True
                                Else
                                    mAddNewLine = False
                                End If
                            End If
                        End If

                    End If

                    .Col = ColEmpType
                    If lblBookType.Text = "E" Then
                        .Text = IIf(IsDbNull(RsAttn.Fields("EMPTYPE").Value), "", RsAttn.Fields("EMPTYPE").Value)
                    Else
                        If MainClass.ValidateWithMasterTable(RsAttn.Fields("EMPTYPE"), "CON_CODE", "CON_NAME", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mContractorName = MasterNo
                        Else
                            mContractorName = ""
                        End If
                        .Text = mContractorName
                    End If

                    '                mCellHeight = sprdAttn.MaxTextCellHeight
                    '                sprdAttn.RowHeight(cntRow) = mCellHeight
                    '
                    If mAddNewLine = True Then
                        cntRow = cntRow + 1
                    Else
                        .Row = cntRow
                        .Col = ColCard
                        .Text = ""
                        .Col = ColName
                        .Text = ""
                        .Col = ColDept
                        .Text = ""
                        .Col = ColPunchTime1
                        .Text = ""
                        .Col = ColPunchTime2
                        .Text = ""
                        .Col = ColEmpType
                        .Text = ""
                    End If
                    RsAttn.MoveNext()
                Loop
            End With
        End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Function GetPunchTime(ByRef mEmpCode As String, ByRef mDate As String, ByRef mTable As String) As String

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim mCanteenData As String

        GetPunchTime = ""

        '    If RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '        mCanteenData = "CANTEEN.TEMPDATA"
        '    Else
        mCanteenData = "CANTEENTEMPDATA"
        '    End If

        SqlStr = " SELECT DISTINCT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf & " EMP.EMP_DEPT_CODE, TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI') AS OFFICEPUNCH " & vbCrLf & " FROM " & mTable & " EMP, " & mCanteenData & " SMST " & vbCrLf & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then
            ''SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=TO_NUMBER(SUBSTR(SMST.CARDNO,1,LENGTH(trim(SMST.CARDNO))-6)) AND TRIM(EMP.EMP_CODE) = TRIM(SUBSTR(SMST.CARDNO,3)) AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
            SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=TO_NUMBER(SUBSTR(SMST.CARDNO,1,LENGTH(trim(SMST.CARDNO))-6))"
            SqlStr = SqlStr & vbCrLf & " AND TRIM(EMP.EMP_CODE) = TRIM(SUBSTR(SMST.CARDNO,LENGTH(trim(SMST.CARDNO))-5)) "
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"


        Else
            SqlStr = SqlStr & vbCrLf & " AND TRIM(EMP.EMP_CODE) = TRIM(SMST.CARDNO) " & vbCrLf & " AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"

        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        SqlStr = SqlStr & vbCrLf & "Order by OFFICEPUNCH"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            Do While Not RsAttn.EOF
                If GetPunchTime = "" Then
                    GetPunchTime = VB6.Format(IIf(IsDbNull(RsAttn.Fields("OFFICEPUNCH").Value), "", RsAttn.Fields("OFFICEPUNCH").Value), "HH:MM")
                Else
                    GetPunchTime = GetPunchTime & " " & VB6.Format(IIf(IsDbNull(RsAttn.Fields("OFFICEPUNCH").Value), "", RsAttn.Fields("OFFICEPUNCH").Value), "HH:MM")
                End If
                RsAttn.MoveNext()
            Loop
        End If
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function GetPunchIOTime(ByRef mEmpCode As String, ByRef mDate As String, ByRef mTable As String, ByRef mIO As String) As String

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim mIsRoundClock As String
        Dim mCanteenData As String

        GetPunchIOTime = ""

        '    If RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '        mCanteenData = "CANTEEN.TEMPDATA"
        '    Else
        mCanteenData = "CANTEENTEMPDATA"
        '    End If

        mIsRoundClock = IIf(GetRoundClock(mEmpCode, mDate, (lblBookType.Text)) = True, "Y", "N")

        If mIsRoundClock = "N" Then
            If mIO = "I" Then
                SqlStr = " SELECT MIN(TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI')) AS OFFICEPUNCH "
            Else
                SqlStr = " SELECT MAX(TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI')) AS OFFICEPUNCH "
            End If
            SqlStr = SqlStr & vbCrLf & " FROM " & mTable & " EMP, " & mCanteenData & " SMST " & vbCrLf & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then
                SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=TO_NUMBER(SUBSTR(SMST.CARDNO,1,LENGTH(trim(SMST.CARDNO))-6)) AND TRIM(EMP.EMP_CODE) = TRIM(SUBSTR(SMST.CARDNO,3)) AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND TRIM(EMP.EMP_CODE) = TRIM(SMST.CARDNO) " & vbCrLf & " AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
            End If
        Else
            If mIO = "I" Then
                SqlStr = " SELECT MAX(TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI')) AS OFFICEPUNCH "

                SqlStr = SqlStr & vbCrLf & " FROM " & mTable & " EMP, " & mCanteenData & " SMST " & vbCrLf & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=TO_NUMBER(SUBSTR(SMST.CARDNO,1,LENGTH(trim(SMST.CARDNO))-6)) AND TRIM(EMP.EMP_CODE) = TRIM(SUBSTR(SMST.CARDNO,3)) AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND TRIM(EMP.EMP_CODE) = TRIM(SMST.CARDNO) " & vbCrLf & " AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
                End If

            Else
                mDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)))
                SqlStr = " SELECT MIN(TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI')) AS OFFICEPUNCH "
                SqlStr = SqlStr & vbCrLf & " FROM " & mTable & " EMP, " & mCanteenData & " SMST " & vbCrLf & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=TO_NUMBER(SUBSTR(SMST.CARDNO,1,LENGTH(trim(SMST.CARDNO))-6)) AND TRIM(EMP.EMP_CODE) = TRIM(SUBSTR(SMST.CARDNO,3)) AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND TRIM(EMP.EMP_CODE) = TRIM(SMST.CARDNO) " & vbCrLf & " AND TO_CHAR(SMST.OFFICEPUNCH,'YYYYMMDD')= '" & VB6.Format(mDate, "YYYYMMDD") & "'"
                End If
            End If

        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        SqlStr = SqlStr & vbCrLf & "Order by OFFICEPUNCH"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            Do While Not RsAttn.EOF
                If GetPunchIOTime = "" Then
                    GetPunchIOTime = VB6.Format(IIf(IsDbNull(RsAttn.Fields("OFFICEPUNCH").Value), "", RsAttn.Fields("OFFICEPUNCH").Value), "HH:MM")
                Else
                    GetPunchIOTime = GetPunchIOTime & " " & VB6.Format(IIf(IsDbNull(RsAttn.Fields("OFFICEPUNCH").Value), "", RsAttn.Fields("OFFICEPUNCH").Value), "HH:MM")
                End If
                RsAttn.MoveNext()
            Loop
        End If
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
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

        If lblBookType.Text = "E" Then
            cboConName.Items.Add("EMPLOYEE")
        Else
            SqlStr = "Select CON_NAME FROM PAY_CONTRACTOR_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' Order by CON_NAME"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

            cboConName.Items.Clear()
            If RsDept.EOF = False Then
                Do While Not RsDept.EOF
                    cboConName.Items.Add(RsDept.Fields("CON_NAME").Value)
                    RsDept.MoveNext()
                Loop
            End If
        End If
        cboConName.SelectedIndex = 0

        cboCatgeory.Items.Clear()
        If lblBookType.Text = "E" Then
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

            '        cboCatgeory.AddItem "General Staff"
            '        cboCatgeory.AddItem "Production Staff"
            '        cboCatgeory.AddItem "Export Staff"
            '        cboCatgeory.AddItem "Regular Worker"
            '        cboCatgeory.AddItem "Staff R & D"
            '    '    cboCategory.AddItem "Contratcor Staff"
            '        cboCatgeory.AddItem "Director"
            '        cboCatgeory.AddItem "Trainee Staff"
        Else
            cboCatgeory.Items.Add("General")
            cboCatgeory.Items.Add("Pc. Rate")
        End If
        cboCatgeory.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("Present")
        cboShow.Items.Add("Absent")
        cboShow.Items.Add("Miss Punch")
        cboShow.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mTable As String

        If lblBookType.Text = "E" Then
            mTable = "PAY_EMPLOYEE_MST"
        Else
            mTable = "PAY_CONT_EMPLOYEE_MST"
        End If

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", mTable, PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Dim mTable As String

        If lblBookType.Text = "E" Then
            mTable = "PAY_EMPLOYEE_MST"
        Else
            mTable = "PAY_CONT_EMPLOYEE_MST"
        End If
        If MainClass.SearchGridMaster((txtEmpCode.Text), mTable, "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub
End Class
