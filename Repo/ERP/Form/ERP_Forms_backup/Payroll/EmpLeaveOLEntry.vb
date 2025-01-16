Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpLeaveOLEntry
    Inherits System.Windows.Forms.Form
    Dim RsEmpLeave As ADODB.Recordset

    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Private Const ColDate As Short = 1
    Private Const ColDay As Short = 2
    Private Const ColFH As Short = 3
    Private Const ColSH As Short = 4

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Dim xEmpCode As String


    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub FormatMain()

        Dim cntCol As Integer
        '    MainClass.ClearGrid sprdHoliday

        Call FillDate()

        With sprdMain
            .MaxCols = ColSH

            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = -1

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)

            For cntCol = ColFH To ColSH
                .Col = cntCol
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "" & Chr(9) & "0-ABSENT" & Chr(9) & "1-CASUAL" & Chr(9) & "2-EARN"
                .TypeComboBoxList = .TypeComboBoxList & "3-SICK" & Chr(9) & "4-MATERNITY" & Chr(9) & "5-CPLEARN" & Chr(9) & "6-WOPAY"
                .TypeComboBoxList = .TypeComboBoxList & "7-CPLAVAIL" & Chr(9) & "8-SUNDAY" & Chr(9) & "9-HOLIDAY"
                .TypeComboBoxCurSel = 0
                .set_ColWidth(cntCol, 13)
            Next


            MainClass.ProtectCell(sprdMain, 1, .MaxRows, ColDate, ColDay)
            MainClass.SetSpreadColor(sprdMain, -1)
            '        sprdHoliday.OperationMode = OperationModeSingle
        End With
    End Sub
    Private Sub FillDate()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mLastDate As Integer
        Dim mDate As String
        Dim mCurrDate As Date

        '    mLastDate = MainClass.LastDay(Month(txtRefDate.Text), Year(txtRefDate.Text))

        If Trim(txtLeaveFrom.Text) = "" Then Exit Sub
        If Trim(txtLeaveTo.Text) = "" Then Exit Sub

        With sprdMain
            .MaxRows = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtLeaveFrom.Text), CDate(txtLeaveTo.Text)) + 1
            cntRow = 1
            mCurrDate = CDate(txtLeaveFrom.Text)
            Do While mCurrDate <= CDate(txtLeaveTo.Text)
                .Row = cntRow
                .Col = ColDate
                .Text = VB6.Format(mCurrDate, "DD/MM/YYYY")

                .Col = ColDay
                .Text = WeekDayName(WeekDay(mCurrDate, FirstDayOfWeek.System))

                mCurrDate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mCurrDate))
                cntRow = cntRow + 1
            Loop
            '        For cntRow = 1 To mLastDate
            '            .Row = cntRow
            '            .Col = ColDate
            '            mDate = Format(cntRow, "00") & "/" & vb6.Format(txtRefDate.Text, "MM/YYYY")
            '            .Text = Format(mDate, "DD/MM/YYYY")
            '
            '            .Col = ColDay
            '            .Text = WeekdayName(Weekday(mDate, vbUseSystemDayOfWeek))
            '        Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmEmpLeaveOLEntry_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmEmpLeaveOLEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mFHalf As Integer
        Dim mSHalf As Integer
        Dim mDate As String
        Dim mAgtLate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = "UPDATE PAY_LEAVE_APP_TRN SET " & vbCrLf _
            & " HR_STATUS='C', " & vbCrLf _
            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_REF=" & Val(txtRefNo.Text) & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM PAY_ATTN_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & txtEmpCode.Text & "'" & vbCrLf _
            & " AND ATTN_DATE >=TO_DATE('" & VB6.Format(txtLeaveFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ATTN_DATE <=TO_DATE('" & VB6.Format(txtLeaveTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                mFHalf = IIf(.Text = "", -1, VB.Left(.Text, 1))

                .Col = ColSH
                mSHalf = IIf(.Text = "", -1, VB.Left(.Text, 1))

                mAgtLate = "N"

                If mFHalf <> -1 Or mSHalf <> -1 Then
                    SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf _
                        & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                        & " ATTN_DATE, FIRSTHALF, SECONDHALF, AGT_LATE," & vbCrLf _
                        & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(txtRefDate.Text)) & ", " & vbCrLf _
                        & " '" & txtEmpCode.Text & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & "  " & mFHalf & ", " & mSHalf & ", '" & mAgtLate & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        Update1 = True
        '    Unload Me
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function CheckSalaryMade(ByRef xEmpCode As String, ByRef xSalDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String

        CheckSalaryMade = False
        mCheckDate = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))) & "/" & VB6.Format(xSalDate, "MM/YYYY")

        SqlStr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'" & vbCrLf _
            & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSalaryMade = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        Dim mListIndex As Integer

        If eventArgs.NewRow = -1 Then Exit Sub

        sprdMain.Row = sprdMain.ActiveRow
        If eventArgs.col = ColFH Then
            SprdMain.Col = ColFH
            mListIndex = Val(SprdMain.Value)

            SprdMain.Col = ColSH
            If Val(SprdMain.Value) <= 0 Then
                SprdMain.Value = CStr(mListIndex)
            End If
        End If

        '    MainClass.SetFocusToCell SprdMain, SprdMain.Row + 1, ColFH
    End Sub

    Private Sub Clear1()


        '    txtRefNo.Text = ""
        txtRefDate.Text = "__/__/____"
        txtEmpCode.Text = ""
        txtEmpName.Text = ""
        txtDept.Text = ""
        txtDesg.Text = ""
        '    txtLeaveFrom.Text = ""
        '    txtLeaveTo.Text = ""
        txtLDays.Text = ""
        txtRecEmpCode.Text = ""
        txtRecEmpName.Text = ""
        txtAppEmpCode.Text = ""
        txtAppEmpName.Text = ""
        txtReason.Text = ""


        MainClass.ClearGrid(sprdMain)
        Call FormatMain()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmEmpLeaveOLEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_LEAVE_APP_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_REF =" & Val(txtRefNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpLeave, ADODB.LockTypeEnum.adLockReadOnly)
        Clear1()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Show1()

        Call FormatMain()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmEmpLeaveOLEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        '    Me.Left = 0
        '    Me.Top = 0
        '    Me.Height = 7485
        '    Me.Width = 8340

        'Call SetChildFormCordinate(Me)


        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        'ADDMode = False
        'MODIFYMode = False
        'FormLoaded = False


        Me.Text = "Leave Entry"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmEmpLeaveOLEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsEmpLeave = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim mDeptCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If Not RsEmpLeave.EOF Then
            txtRefNo.Text = IIf(IsDbNull(RsEmpLeave.Fields("AUTO_KEY_REF").Value), "", RsEmpLeave.Fields("AUTO_KEY_REF").Value)
            txtRefDate.Text = VB6.Format(IIf(IsDbNull(RsEmpLeave.Fields("REF_DATE").Value), "", RsEmpLeave.Fields("REF_DATE").Value), "DD/MM/YYYY")
            txtEmpCode.Text = IIf(IsDbNull(RsEmpLeave.Fields("EMP_CODE").Value), "", RsEmpLeave.Fields("EMP_CODE").Value)
            txtLeaveFrom.Text = VB6.Format(IIf(IsDbNull(RsEmpLeave.Fields("FROM_DATE").Value), "", RsEmpLeave.Fields("FROM_DATE").Value), "DD/MM/YYYY")
            txtLeaveTo.Text = VB6.Format(IIf(IsDbNull(RsEmpLeave.Fields("TO_DATE").Value), "", RsEmpLeave.Fields("TO_DATE").Value), "DD/MM/YYYY")
            txtLDays.Text = IIf(IsDbNull(RsEmpLeave.Fields("LDAYS").Value), "", RsEmpLeave.Fields("LDAYS").Value)
            txtRecEmpCode.Text = IIf(IsDbNull(RsEmpLeave.Fields("REC_EMP_CODE").Value), "", RsEmpLeave.Fields("REC_EMP_CODE").Value)
            txtAppEmpCode.Text = IIf(IsDbNull(RsEmpLeave.Fields("APP_EMP_CODE").Value), "", RsEmpLeave.Fields("APP_EMP_CODE").Value)
            txtReason.Text = IIf(IsDbNull(RsEmpLeave.Fields("REASON").Value), "", RsEmpLeave.Fields("REASON").Value)

            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtEmpName.Text = MasterNo
            Else
                txtEmpName.Text = ""
            End If

            If MainClass.ValidateWithMasterTable((txtRecEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtRecEmpName.Text = MasterNo
            Else
                txtRecEmpName.Text = ""
            End If

            If MainClass.ValidateWithMasterTable((txtAppEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtAppEmpName.Text = MasterNo
            Else
                txtAppEmpName.Text = ""
            End If
            '
            '        If MainClass.ValidateWithMasterTable(txtAppEmpCode.Text, "EMP_CODE", "EMP_EMAILID", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '            lblToeMailID.Caption = MasterNo
            '        Else
            '            lblToeMailID.Caption = ""
            '        End If

            SqlStr = "SELECT EMP_NAME,EMP_EMAILID,EMP_DEPT_CODE, " & vbCrLf & " GETEMPDESG (" & RsCompany.Fields.Item("COMPANY_CODE").Value & ",'" & Trim(txtEmpCode.Text) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & Trim(UCase(txtEmpCode.Text)) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                '            lblFromeMailID.Caption = IIf(IsNull(RsTemp!EMP_EMAILID), "", RsTemp!EMP_EMAILID)
                mDeptCode = IIf(IsDbNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)
                txtDesg.Text = IIf(IsDbNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)

                If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDept.Text = MasterNo
                End If
            End If

            Call FillDate()
            If ShowDetail1 = False Then GoTo ShowErrPart
            Call FillLeaves((txtEmpCode.Text))
        End If

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function ShowDetail1() As Boolean

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mMoveType As String
        Dim cntRow As Integer
        Dim mCode As String
        Dim mRowDate As String
        Dim mAttnDate As String
        Dim mFH As Integer
        Dim mSH As Integer
        Dim RsELeaveDetail As ADODB.Recordset = Nothing

        ShowDetail1 = False
        SqlStr = ""
        SqlStr = " SELECT EMP_CODE,ATTN_DATE, FIRSTHALF , SECONDHALF,AGT_LATE " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE ='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf _
            & " AND ATTN_DATE >=TO_DATE('" & VB6.Format(txtLeaveFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ATTN_DATE <=TO_DATE('" & VB6.Format(txtLeaveTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsELeaveDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsELeaveDetail.EOF = True Then
            SqlStr = " SELECT EMP_CODE,ATTN_DATE, FIRSTHALF , SECONDHALF,'N' AS AGT_LATE " & vbCrLf _
                   & " FROM PAY_REQ_ATTN_MST WHERE " & vbCrLf _
                   & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND AUTO_KEY_REF =" & Val(txtRefNo.Text) & " AND EMP_CODE ='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf _
                   & " AND ATTN_DATE >=TO_DATE('" & VB6.Format(txtLeaveFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                   & " AND ATTN_DATE <=TO_DATE('" & VB6.Format(txtLeaveTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ATTN_DATE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsELeaveDetail, ADODB.LockTypeEnum.adLockReadOnly)
        End If
        If RsELeaveDetail.EOF = False Then
            Do While Not RsELeaveDetail.EOF
                mAttnDate = VB6.Format(IIf(IsDbNull(RsELeaveDetail.Fields("ATTN_DATE").Value), "", RsELeaveDetail.Fields("ATTN_DATE").Value), "DD/MM/YYYY")
                mFH = IIf(IsDbNull(RsELeaveDetail.Fields("FIRSTHALF").Value), -1, RsELeaveDetail.Fields("FIRSTHALF").Value)
                mSH = IIf(IsDbNull(RsELeaveDetail.Fields("SECONDHALF").Value), -1, RsELeaveDetail.Fields("SECONDHALF").Value)

                For cntRow = 1 To sprdMain.MaxRows
                    sprdMain.Row = cntRow
                    sprdMain.Col = ColDate
                    mRowDate = VB6.Format(sprdMain.Text, "DD/MM/YYYY")
                    If mAttnDate = mRowDate Then
                        sprdMain.Col = ColFH
                        sprdMain.TypeComboBoxCurSel = mFH + 1

                        sprdMain.Col = ColSH
                        sprdMain.TypeComboBoxCurSel = mSH + 1

                        Exit For
                    End If
                Next
                RsELeaveDetail.MoveNext()
            Loop
            RsELeaveDetail.MoveFirst()
            Call FillLeaves((txtEmpCode.Text))
        End If
        ShowDetail1 = True
        Exit Function
ShowErrPart:
        MsgBox(Err.Description)
        ShowDetail1 = False
        '    Resume
    End Function
    Private Sub FillLeaves(ByRef mCode As String)

        Dim RsOpLeave As ADODB.Recordset = Nothing
        Dim RsLeave As ADODB.Recordset = Nothing
        Dim mOpSick As Double
        Dim mOpCasual As Double
        Dim mOpEL As Double

        Dim mSick As Double
        Dim mCasual As Double
        Dim mEL As Double
        Dim mCPL As Double
        Dim mCPL_A As Double
        Dim mDOJ As String = ""

        'Dim mMonth As Short
        'Dim mYear As Short

        'Dim I As Integer
        'Dim mMonField As Object
        'Dim mon As String
        Dim mPeriod As Double

        Dim mStartDate As String
        Dim mYearDays As Integer
        Dim xSalDate As String

        If Trim(txtRefDate.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOJ = MasterNo
        End If
        xSalDate = MainClass.LastDay(Month(CDate(txtRefDate.Text)), Year(CDate(txtRefDate.Text))) & "/" & VB6.Format(txtRefDate.Text, "MM/YYYY")

        mOpEL = GETEntitleEarnLeave(PubDBCn, mCode, EARN, xSalDate)
        '    mCPL = GETCPL(PubDBCn, mCode, xSalDate)


        If Year(CDate(xSalDate)) = Year(CDate(mDOJ)) Then
            mStartDate = mDOJ
        Else
            mStartDate = "01/01/" & VB6.Format(xSalDate, "YYYY")
        End If

        mYearDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(xSalDate, "YYYY")), CDate("31/12/" & VB6.Format(xSalDate, "YYYY"))) + 1

        mPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xSalDate)) + 1
        mPeriod = CDbl(VB6.Format(mPeriod / mYearDays, "0.00000"))

        '    mPeriod = Round(Month(lblDate.Caption) / 12, 2)

        SqlStr = " SELECT NVL(OPENING,0) AS OPENING, NVL(TOTENTITLE,0) AS  TOTENTITLE, LEAVECODE " & vbCrLf & " FROM PAY_OPLEAVE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(txtRefDate.Text)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOpLeave.EOF = False Then
            Do While Not RsOpLeave.EOF
                If RsOpLeave.Fields("LeaveCode").Value = SICK Then
                    mOpSick = IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    mOpSick = mOpSick + IIf(IsDbNull(RsOpLeave.Fields("TOTENTITLE").Value), 0, RsOpLeave.Fields("TOTENTITLE").Value) * mPeriod ''(GetLeaveEntitle(Val(RsOpLeave!LeaveCode)) * mPeriod)
                    mOpSick = System.Math.Round(mOpSick * 2, 0) / 2
                ElseIf RsOpLeave.Fields("LeaveCode").Value = CASUAL Then
                    mOpCasual = IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                    mOpCasual = mOpCasual + IIf(IsDbNull(RsOpLeave.Fields("TOTENTITLE").Value), 0, RsOpLeave.Fields("TOTENTITLE").Value) * mPeriod
                    mOpCasual = System.Math.Round(mOpCasual * 2, 0) / 2
                ElseIf RsOpLeave.Fields("LeaveCode").Value = EARN Then
                    mOpEL = mOpEL + IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                ElseIf RsOpLeave.Fields("LeaveCode").Value = CPLEARN Then
                    mCPL = mCPL + IIf(IsDbNull(RsOpLeave.Fields("OPENING").Value), 0, RsOpLeave.Fields("OPENING").Value)
                End If

                RsOpLeave.MoveNext()
            Loop
        End If

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(txtRefDate.Text)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'"

        SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE<=TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLeave.EOF = False Then
            Do While Not RsLeave.EOF
                If RsLeave.Fields("FIRSTHALF").Value = SICK And RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = SICK Or RsLeave.Fields("SECONDHALF").Value = SICK Then
                    mSick = mSick + 0.5
                End If

                If RsLeave.Fields("FIRSTHALF").Value = CASUAL And RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CASUAL Or RsLeave.Fields("SECONDHALF").Value = CASUAL Then
                    mCasual = mCasual + 0.5
                End If

                If RsLeave.Fields("FIRSTHALF").Value = EARN And RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEL = mEL + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = EARN Or RsLeave.Fields("SECONDHALF").Value = EARN Then
                    mEL = mEL + 0.5
                End If

                '            If RsLeave!FIRSTHALF = CPLEARN And RsLeave!SECONDHALF = CPLEARN Then
                '                mCPL = mCPL + 1
                '            ElseIf RsLeave!FIRSTHALF = CPLEARN Or RsLeave!SECONDHALF = CPLEARN Then
                '                mCPL = mCPL + 0.5
                '            End If

                If RsLeave.Fields("FIRSTHALF").Value = CPLAVAIL And RsLeave.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPL_A = mCPL_A + 1
                ElseIf RsLeave.Fields("FIRSTHALF").Value = CPLAVAIL Or RsLeave.Fields("SECONDHALF").Value = CPLAVAIL Then
                    mCPL_A = mCPL_A + 0.5
                End If

                mCPL = mCPL + (IIf(IsDbNull(RsLeave.Fields("CPL_EARN").Value), 0, RsLeave.Fields("CPL_EARN").Value) * 0.5)
                RsLeave.MoveNext()
            Loop
        End If

        lblBalSL.Text = VB6.Format(mOpSick - mSick, "0.0")
        lblBalCL.Text = VB6.Format(mOpCasual - mCasual, "0.0")
        lblBalEL.Text = VB6.Format(mOpEL - mEL, "0.0")
        lblBalCPL.Text = VB6.Format(mCPL - mCPL_A, "0.0")

        lblAvlSL.Text = VB6.Format(mSick, "0.0")
        lblAvlCL.Text = VB6.Format(mCasual, "0.0")
        lblAvlEL.Text = VB6.Format(mEL, "0.0")
        lblAvlCPL.Text = VB6.Format(mCPL_A, "0.0")

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1 = True Then
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim mFHLeave As String
        Dim mSHLeave As String

        FieldsVarification = True

        If txtRefNo.Text = "" Then
            MsgInformation("Please Entered Ref No.")
            If txtRefNo.Enabled = True Then txtRefNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If txtEmpCode.Text = "" Then
            MsgInformation("Please Entered Emp Code.")
            If txtEmpCode.Enabled = True Then txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If txtRecEmpCode.Text = "" Then
            MsgInformation("Please Entered Recommended By.")
            If txtRecEmpCode.Enabled = True Then txtRecEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If txtAppEmpCode.Text = "" Then
            MsgInformation("Please Entered Approved By.")
            If txtAppEmpCode.Enabled = True Then txtAppEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If txtRefDate.Text = "" Then
            MsgInformation("Please Entered Ref Date.")
            If txtRefDate.Enabled = True Then txtRefDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If PubSuperUser = "U" Then
        If CheckSalaryMade((txtEmpCode.Text), VB6.Format(txtLeaveFrom.Text, "DD/MM/YYYY")) = True Then
            MsgInformation("Salary Made Againt This Month. So Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If
        '    End If

        If Val(txtLDays.Text) = 0.5 Then
            sprdMain.Row = 1
            sprdMain.Col = ColFH
            mFHLeave = Trim(sprdMain.Text)

            sprdMain.Col = ColSH
            mSHLeave = Trim(sprdMain.Text)

            If mFHLeave <> "" And mSHLeave <> "" Then
                MsgInformation("Please Select Only Half Day Leave.")
                FieldsVarification = False
                Exit Function
            End If
            If mFHLeave = "" And mSHLeave = "" Then
                MsgInformation("Please Select aleast One Half Leave.")
                FieldsVarification = False
                Exit Function
            End If
        Else
            If ValidLeaveInGrid(ColFH, "Please Check First Half.") = False Then FieldsVarification = False : Exit Function
            If ValidLeaveInGrid(ColSH, "Please Check Second Half.") = False Then FieldsVarification = False : Exit Function
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
    End Function

    Private Function ValidLeaveInGrid(ByRef CheckCol As Integer, Optional ByRef InvalidMsg As String = "") As Boolean

        On Error GoTo ERR1
        Static I As Object
        Static j As Integer
        With sprdMain
            j = .MaxRows
            If j = 0 Then MsgBox(InvalidMsg) : ValidLeaveInGrid = False : Exit Function
            For I = 1 To j
                .Row = I
                .Col = 0

                .Col = CheckCol

                If .Text <> "" Then
                    ValidLeaveInGrid = True
                Else
                    ValidLeaveInGrid = False
                    GoTo DspMsg
                End If
            Next I
        End With
        ValidLeaveInGrid = True
        Exit Function
DspMsg:
        'Resume
        If InvalidMsg = "" Then
            MsgInformation("Not a valid Leave")
            MainClass.SetFocusToCell(sprdMain, I, CheckCol)
        Else
            '    Resume
            MsgInformation(InvalidMsg)
            MainClass.SetFocusToCell(sprdMain, I, CheckCol)
        End If
        'Resume
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "LEAVE APPLICATION"
        mSubTitle = "From : " & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY") & " TO : " & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\LeaveApp.rpt"
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub TxtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtRefDate.Text) = "" Or Trim(txtRefDate.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtRefDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If Year(CDate(txtRefDate.Text)) <> CDbl(PubPAYYEAR) Then
            MsgBox("Invalid Current Calender Year Date", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        Call FillDate()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Public Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PAY_LEAVE_APP_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_REF =" & Val(txtRefNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpLeave, ADODB.LockTypeEnum.adLockReadOnly)
        If RsEmpLeave.EOF = False Then
            Clear1()
            Show1()
        End If

        Call FillLeaves((txtEmpCode.Text))
        GoTo EventExitSub
ERR1:

        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
