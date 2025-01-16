Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEligiblityPFReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColName As Short = 1
    Private Const ColFName As Short = 2
    Private Const ColDept As Short = 3
    Private Const ColDOJ As Short = 4
    Private Const ColPFNo As Short = 5
    Private Const ColDOM As Short = 6
    Private Const ColDOB As Short = 7
    Private Const ColLastBal As Short = 8
    Private Const ColJan As Short = 9
    Private Const ColFeb As Short = 10
    Private Const ColMar As Short = 11
    Private Const ColApr As Short = 12
    Private Const ColMay As Short = 13
    Private Const ColJun As Short = 14
    Private Const ColJul As Short = 15
    Private Const ColAug As Short = 16
    Private Const ColSep As Short = 17
    Private Const ColOct As Short = 18
    Private Const ColNov As Short = 19
    Private Const ColDec As Short = 20
    Private Const ColTotal As Short = 21
    Private Const ColRemarks As Short = 22

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer
        With SprdView
            .MaxCols = ColRemarks
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)
            .set_RowHeight(0, ConRowHeight * 2.5)

            .Col = ColSNO
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColSNO, 4)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 18)

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColFName, 18)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 8)

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDOJ, 8)

            .Col = ColPFNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColPFNo, 9)

            .Col = ColDOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDOM, 8)

            .Col = ColDOB
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDOB, 8)

            .ColsFrozen = ColPFNo

            .Col = ColLastBal
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColLastBal, 4)

            For cntCol = ColJan To ColTotal
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 5)
            Next

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 12)

        End With

        MainClass.ProtectCell(SprdView, 1, SprdView.MaxRows, 1, SprdView.MaxCols)
        SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        MainClass.SetSpreadColor(SprdView, mRow)

        FillHeading()
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()

        With SprdView
            .MaxCols = ColRemarks
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColName
            .Text = "Name and Address" & vbNewLine & "of Employee"

            .Col = ColFName
            .Text = "Father's Name or Name" & vbNewLine & "of husband in case of" & vbNewLine & "married woman"

            .Col = ColDept
            .Text = "Dept."

            .Col = ColDOJ
            .Text = "Date of entry in service"

            .Col = ColPFNo
            .Text = "P.F. A/c No."

            .Col = ColDOM
            .Text = "Date of Member ship"

            .Col = ColDOB
            .Text = "Date of Birth or age"

            .Col = ColLastBal
            .Text = "Last Year Bal."

            .Col = ColJan
            .Text = "Jan."

            .Col = ColFeb
            .Text = "Feb."

            .Col = ColMar
            .Text = "Mar."

            .Col = ColApr
            .Text = "Apr."

            .Col = ColMay
            .Text = "May"

            .Col = ColJun
            .Text = "June"

            .Col = ColJul
            .Text = "July"

            .Col = ColAug
            .Text = "Aug."

            .Col = ColSep
            .Text = "Sep."

            .Col = ColOct
            .Text = "Oct."

            .Col = ColNov
            .Text = "Nov."

            .Col = ColDec
            .Text = "Dec."

            .Col = ColTotal
            .Text = "Total"

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

        Dim mRptFileName As String


        ''Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""
        mTitle = "Eligiblity Register of Employees for Provident Fund"
        mRptFileName = "EligiblityPFReg.Rpt"

        If FillPrintDummyData(SprdView, 1, SprdView.MaxRows, 0, SprdView.MaxCols, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Function FillBankDataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mWDays As String
        Dim mNetPay As String
        Dim mBankAcct As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.BeginTrans()

        '    For RowNum = prmStartGridRow To prmEndGridRow
        '        GridName.Row = RowNum
        '
        '        GridName.Col = ColCode
        '        mEmpCode = MainClass.AllowSingleQuote(GridName.Text)
        '
        '        GridName.Col = ColName
        '        mEmpName = MainClass.AllowSingleQuote(GridName.Text)
        '
        '        GridName.Col = ColAmount
        '        mNetPay = GridName.Text
        '
        '        GridName.Col = ColBankAcct
        '        mBankAcct = MainClass.AllowSingleQuote(GridName.Text)
        '
        '
        '        SqlStr = " INSERT INTO PRINTDUMMYDATA (USERID, SUBROW,FIELD1, " & vbCrLf _
        ''                & " FIELD2, FIELD3, FIELD4, FIELD5) " & vbCrLf _
        ''                & " VALUES (" & vbCrLf _
        ''                & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf _
        ''                & " '" & mEmpCode & "','" & mEmpName & "','" & mWDays & "', " & vbCrLf _
        ''                & " '" & mNetPay & "','" & mBankAcct & "') "
        '        PubDBCn.Execute SqlStr
        '
        '    Next
        PubDBCn.CommitTrans()
        FillBankDataIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillBankDataIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        On Error GoTo ErrPart

        Report1.SQLQuery = mSqlStr

        MainClass.AssignCRptFormulas(Report1, "Name='" & txtName.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "Code='" & txtCode.Text & "'")

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        Me.Text = "Eligiblity Register of Employees' for Provident Fund - " & Year(RunDate)
        RefreshScreen()
    End Sub
    Private Sub frmEligiblityPFReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Me.Text = "Eligiblity Register of Employees' for Provident Fund - " & Year(RunDate)

        txtName.Text = IIf(IsDbNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
        txtCode.Text = IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value)

        'RefreshScreen
    End Sub
    Private Sub frmEligiblityPFReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        OptName.Checked = True
        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub

    Private Sub frmEligiblityPFReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub


    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            PrintCommand(False)
        End If
    End Sub
    Private Sub OptName_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptName.CheckedChanged
        If eventSender.Checked Then
            PrintCommand(False)
        End If
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim SqlStr As String = ""
        Dim RsView As ADODB.Recordset
        Dim mDOJ As String
        Dim mDOL As String
        Dim mDeptCode As String

        MainClass.ClearGrid(SprdView)


        mDOJ = "01/01/" & Year(RunDate)
        mDOL = "31/12/" & Year(RunDate)

        SqlStr = "Select EMP_NAME, EMP_CODE, EMP_ADDR, EMP_CITY, EMP_STATE, EMP_PIN," & vbCrLf & " EMP_FNAME, EMP_DEPT_CODE, EMP_DOJ, EMP_PF_ACNO, EMP_DOB" & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        If optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order By EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order By EMP_NAME"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsView, ADODB.LockTypeEnum.adLockOptimistic)
        If RsView.EOF = False Then
            Do While Not RsView.EOF
                With SprdView
                    .Row = .MaxRows

                    '                .Col = ColSNO
                    '                .Text = .Row

                    .Col = ColName
                    .Text = RsView.Fields("EMP_NAME").Value


                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsView.Fields("EMP_FNAME").Value), "", RsView.Fields("EMP_FNAME").Value)

                    .Col = ColDept
                    mDeptCode = IIf(IsDbNull(RsView.Fields("EMP_DEPT_CODE").Value), "", RsView.Fields("EMP_DEPT_CODE").Value)

                    If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    Else
                        .Text = ""
                    End If

                    .Col = ColDOJ
                    .Text = IIf(IsDbNull(RsView.Fields("EMP_DOJ").Value), "", RsView.Fields("EMP_DOJ").Value)

                    .Col = ColPFNo
                    .Text = IIf(IsDbNull(RsView.Fields("EMP_PF_ACNO").Value), "", RsView.Fields("EMP_PF_ACNO").Value)

                    .Col = ColDOM
                    .Text = IIf(IsDbNull(RsView.Fields("EMP_DOJ").Value), "", RsView.Fields("EMP_DOJ").Value)

                    .Col = ColDOB
                    .Text = IIf(IsDbNull(RsView.Fields("EMP_DOB").Value), "", RsView.Fields("EMP_DOB").Value)

                    If FillAttn(RsView.Fields("EMP_CODE").Value, .Row) = False Then Exit Sub

                    If RsView.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                End With
                RsView.MoveNext()
            Loop
        End If
        FormatSprd(-1)
        PrintCommand(True)
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        cmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub

    Private Function FillAttn(ByRef mCode As String, ByRef mRow As Integer) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsDetail As ADODB.Recordset
        Dim RsOpLeave As ADODB.Recordset
        Dim mPFYear As Integer

        Dim mMonth As Integer
        Dim mTotal As Double
        Dim mOPLeave As Double

        mPFYear = Year(RunDate)

        SqlStr = "Select SUM(OPENING) AS Opening1 From PAY_OPLeave_TRN " & vbCrLf & " WHERE Emp_Code='" & mCode & "' " & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PAYYEAR =" & mPFYear & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOpLeave.EOF = False Then
            mOPLeave = IIf(IsDbNull(RsOpLeave.Fields("OPENING1").Value), 0, RsOpLeave.Fields("OPENING1").Value)
            SprdView.Row = mRow
            SprdView.Col = ColLastBal
            SprdView.Text = VB6.Format(mOPLeave, "0.00")
        End If

        SqlStr = "Select * From PAY_PFESI_TRN PFESITRN " & vbCrLf & " WHERE Emp_Code='" & mCode & "' " & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TO_CHAR(Sal_Date,'YYYY') =" & mPFYear & " AND ISARREAR='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDetail, ADODB.LockTypeEnum.adLockOptimistic)


        mTotal = 0
        If RsDetail.EOF = False Then
            SprdView.Row = mRow
            Do While Not RsDetail.EOF
                mMonth = Month(RsDetail.Fields("SAL_DATE").Value)
                Select Case mMonth
                    Case 1
                        sprdView.Col = ColJan
                    Case 2
                        sprdView.Col = ColFeb
                    Case 3
                        sprdView.Col = ColMar
                    Case 4
                        sprdView.Col = ColApr
                    Case 5
                        sprdView.Col = ColMay
                    Case 6
                        sprdView.Col = ColJun
                    Case 7
                        sprdView.Col = ColJul
                    Case 8
                        sprdView.Col = ColAug
                    Case 9
                        sprdView.Col = ColSep
                    Case 10
                        sprdView.Col = ColOct
                    Case 11
                        sprdView.Col = ColNov
                    Case 12
                        sprdView.Col = ColDec
                End Select

                SprdView.Text = VB6.Format(IIf(IsDbNull(RsDetail.Fields("WDAYS").Value), 0, RsDetail.Fields("WDAYS").Value), "0.00")
                mTotal = mTotal + IIf(IsDbNull(RsDetail.Fields("WDAYS").Value), 0, RsDetail.Fields("WDAYS").Value)
                RsDetail.MoveNext()
            Loop

            SprdView.Col = ColTotal
            SprdView.Text = VB6.Format(mTotal, "0.00")
        End If
        FillAttn = True
        Exit Function
ErrPart:
        FillAttn = False
        MsgInformation(Err.Description)
        'Resume
    End Function

    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
