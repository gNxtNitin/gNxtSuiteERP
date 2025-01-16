Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPFForm10
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
    Private Const ColDOL As Short = 5
    Private Const ColReason As Short = 6
    Private Const ColRemarks As Short = 7
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With sprdMain
            .MaxCols = ColRemarks
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)
            .set_RowHeight(0, ConRowHeight * 2.5)

            .Col = ColAccountNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColAccountNo, 12)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 25)


            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColFName, 20)
            .ColsFrozen = ColFName

            .Col = ColDOL
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColDOL, 10)

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColReason, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 8)
        End With

        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, 1, sprdMain.MaxCols)
        sprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        MainClass.SetSpreadColor(sprdMain, mRow)
        FillHeading()

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        With sprdMain
            .MaxCols = ColRemarks
            .Row = 0

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColAccountNo
            .Text = "Accont No."

            .Col = ColCode
            .Text = "Emp No"

            .Col = ColName
            .Text = "Name of the Employees" & vbNewLine & "(in block letters)"

            .Col = ColFName
            .Text = "Father's Name or husband's Name in case of married women"

            .Col = ColDOL
            .Text = "Date of Leaving Service"

            .Col = ColReason
            .Text = "Reason of Leaving Service"

            .Col = ColRemarks
            .Text = "Remarks"
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


        'Select Record for print...

        SqlStr = ""

        SqlStr = MakeSQL

        mSubTitle = "[Paragraph 36 (2) (b)]"
        mTitle = "Employees Provident Fund Scheme, 1952"

        Call ShowReport(SqlStr, "PFForm10.Rpt", Mode, mTitle, mSubTitle)

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

        mRemarks = "Return of member leaving Service"
        mRemarks = mRemarks & " during the month of " & lblYear.Text

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        MainClass.AssignCRptFormulas(Report1, "Remarks='" & mRemarks & "'")
        MainClass.AssignCRptFormulas(Report1, "PFEST='" & IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value) & "'")



        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        SetDate(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub
    Private Sub frmPFForm10_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmPFForm10_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub frmPFForm10_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
        '    RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()
        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        '    RefreshScreen
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart

        MainClass.ClearGrid(sprdMain)

        SqlStr = MakeSQL()

        MainClass.AssignDataInSprd8(SqlStr, sprdMain, StrConn, "Y")
        MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, 0, sprdMain.MaxCols)
        FormatSprd(-1)

        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function MakeSQL() As String

        On Error GoTo refreshErrPart
        Dim mMonth As Short
        Dim mYear As Short
        Dim mDOJ As Date
        Dim mDOL As Date

        mMonth = Month(CDate(lblRunDate.Text))
        mYear = Year(CDate(lblRunDate.Text))

        mDOJ = CDate(MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear)
        mDOL = CDate("01" & "/" & mMonth & "/" & mYear)


        MakeSQL = " Select EMP.EMP_PF_ACNO As PFNo, EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf & " EMP.EMP_FNAME, EMP.EMP_LEAVE_DATE As DOL, EMP.EMP_LEAVE_REASON As Reason, '' As Remarks " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (EMP_LEAVE_DATE <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY')) "

        If OptName.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_NAME"
        Else
            MakeSQL = MakeSQL & vbCrLf & "Order by EMP.EMP_CODE"
        End If

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        'Resume
    End Function
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
