Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCheckAttnCont
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColSNo As Short = 0
    Private Const ColBookNo As Short = 1
    Private Const ColCard As Short = 2
    Private Const ColName As Short = 3
    Private Const ColWorkDays As Short = 4
    Private Const ColOTHour As Short = 5
    Private Const ColSundayOTHour As Short = 6

    Private Sub FillHeading(ByRef xDate As Date)


        Dim cntCol As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        MainClass.ClearGrid(sprdAttn)

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        With sprdAttn
            .MaxCols = ColSundayOTHour

            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Row = -1

            .Col = ColBookNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBookNo, 6)

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColCard, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 30)

            For cntCol = ColWorkDays To ColSundayOTHour
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 1
                .set_ColWidth(cntCol, 10)
            Next


            .Row = 0

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColBookNo
            .Text = "Book No"

            .Col = ColCard
            .Text = "Emp Card No"


            .Col = ColName
            .Text = "Employees' Name "


            .Col = ColWorkDays
            .Text = "Working Days"

            .Col = ColOTHour
            .Text = IIf(lblBookType.Text = "G", "OT Hour", "Gross Salary")

            .Col = ColSundayOTHour
            .Text = "Sunday OT Hour"

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, ColSundayOTHour)
            MainClass.SetSpreadColor(sprdAttn, -1)
            sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        End With
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
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
            cboConName.Enabled = False
        Else
            cboConName.Enabled = True
        End If
    End Sub

    Private Sub chkPageNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPageNo.CheckStateChanged
        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPageNo.Enabled = False
        Else
            txtPageNo.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
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


        'Insert Data from Grid to PrintDummyData Table...


        '    If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1
        '
        '
        '
        '    'Select Record for print...
        '
        '    SqlStr = ""
        '
        '    SqlStr = FetchRecordForReport(SqlStr)
        '
        '    mSubTitle = "For the period : " & lblYear.Caption
        '    mTitle = "Attendance - Check List"
        '
        '    If cboConName.Text <> "" Then
        '        mTitle = mTitle & " - " & cboConName.Text
        '    End If
        '    Call ShowReport(SqlStr, "AttnCheckList.Rpt", Mode, mTitle, mSubTitle)

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

        FillHeading(CDate(lblRunDate.Text))
        RefreshScreen()
        cmdPrint.Enabled = True
        CmdPreview.Enabled = True
        MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, ColSundayOTHour)
    End Sub


    Private Sub frmCheckAttnCont_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        Me.Text = "Contractor Attendance - Check List"
        Me.Text = Me.Text & IIf(lblBookType.Text = "G", "(General)", "(P. Rate)")
    End Sub

    Private Sub frmCheckAttnCont_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        FillHeading(CDate(lblRunDate.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        OptName.Checked = True
        FillDeptCombo()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        chkBookNo.CheckState = System.Windows.Forms.CheckState.Checked
        chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked

        cboDept.Enabled = False
        cboConName.Enabled = False
        txtBookNo.Enabled = False
        txtPageNo.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
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

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim mCode As String

        Dim cntRow As Integer

        Dim mDeptCode As String
        Dim mAttnDate As String
        Dim mDeptName As String
        Dim mContCode As String
        Dim mBookNo As String

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mAttnDate = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")

        SqlStr = " SELECT ATTN.BOOKNO, ATTN.PAGENO, EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf & " WDAYS, WHOUR, GSALARY,SUNDAYOTHOUR " & vbCrLf & " FROM PAY_CONT_EMPLOYEE_MST EMP, PAY_CONT_VAR_TRN ATTN " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=ATTN.COMPANY_CODE " & vbCrLf & " AND EMP.EMP_CODE=ATTN.EMP_CODE" & vbCrLf & " AND TO_CHAR(ATTN_MONTH,'MON-YYYY')='" & UCase(VB6.Format(mAttnDate, "MMM-YYYY")) & "'" & vbCrLf & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN.WDAYS>0"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboConName.SelectedIndex <> -1 Then
            If MainClass.ValidateWithMasterTable(cboConName.Text, "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mContCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND CONTRACTOR_CODE='" & MainClass.AllowSingleQuote(Trim(mContCode)) & "' "
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptName = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "' "
            End If
        End If

        If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ATTN.BOOKNO=" & Val(txtBookNo.Text) & ""
        End If

        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND ATTN.PAGENO=" & Val(txtPageNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & "AND EMP_CAT='" & lblBookType.Text & "' "

        '    SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE='206414' "

        '    SqlStr = SqlStr & vbCrLf & "Group by EMP.EMP_NAME, EMP.EMP_CODE "

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by ATTN.BOOKNO, ATTN.PAGENO, EMP.EMP_CODE"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    .MaxRows = cntRow

                    .Row = cntRow
                    .Col = ColBookNo
                    mBookNo = VB6.Format(IIf(IsDbNull(RsAttn.Fields("BOOKNO").Value), 0, RsAttn.Fields("BOOKNO").Value), "0")
                    mBookNo = mBookNo & "." & VB6.Format(IIf(IsDbNull(RsAttn.Fields("PageNo").Value), 0, RsAttn.Fields("PageNo").Value), "0")
                    .Text = mBookNo

                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColWorkDays
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("WDAYS").Value), 0, RsAttn.Fields("WDAYS").Value), "0.00")

                    .Col = ColOTHour
                    If lblBookType.Text = "G" Then
                        .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("WHOUR").Value), 0, RsAttn.Fields("WHOUR").Value), "0.00")
                    Else
                        .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("GSALARY").Value), 0, RsAttn.Fields("GSALARY").Value), "0.00")
                    End If

                    .Col = ColSundayOTHour
                    .Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields("SUNDAYOTHOUR").Value), 0, RsAttn.Fields("SUNDAYOTHOUR").Value), "0.00")

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

        SqlStr = "Select CON_NAME FROM PAY_CONTRACTOR_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' Order by CON_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboConName.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboConName.Items.Add(RsDept.Fields("CON_NAME").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboConName.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
End Class
