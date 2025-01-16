Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpAttn
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Private Const ColDate As Short = 1
    Private Const ColFH As Short = 2
    Private Const ColSH As Short = 3

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click

        If PubSuperUser <> "S" Then
            If CheckSalaryMade((lblEmpCode.Text), VB6.Format(lblDate.Text, "DD/MM/YYYY")) = True Then
                MsgInformation("Salary Made Againt This Month. So Cann't be Modified")
                Exit Sub
            End If
        End If

        Update1()
    End Sub
    Private Sub FormatMain()

        Dim cntCol As Integer
        '    MainClass.ClearGrid sprdHoliday

        Call FillDate()

        With SprdMain
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
                .TypeComboBoxList = "" & Chr(9) & "0 -ABSENT" & Chr(9) & "1 -CASUAL" & Chr(9) & "2 -EARN"
                .TypeComboBoxList = .TypeComboBoxList & "3 -SICK" & Chr(9) & "4 -MATERNITY" & Chr(9) & "5 -CPLEARN" & Chr(9) & "6 -WOPAY"
                .TypeComboBoxList = .TypeComboBoxList & "7 -CPLAVAIL" & Chr(9) & "8 -SUNDAY" & Chr(9) & "9 -HOLIDAY" & Chr(9) & "10 -PRESENT" & Chr(9) & "11 -WFH"
                .TypeComboBoxCurSel = 0
                .set_ColWidth(cntCol, 13)
            Next

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDate, ColDate)
            MainClass.SetSpreadColor(SprdMain, -1)
            '        sprdHoliday.OperationMode = OperationModeSingle
        End With
    End Sub
    Private Sub FillDate()

        Dim cntRow As Integer
        Dim mLastDate As Integer
        Dim mDate As String

        mLastDate = MainClass.LastDay(Month(CDate(lblDate.Text)), Year(CDate(lblDate.Text)))

        With SprdMain
            .MaxRows = mLastDate
            For cntRow = 1 To mLastDate
                .Row = cntRow
                .Col = ColDate
                mDate = VB6.Format(cntRow, "00") & "/" & VB6.Format(lblDate.Text, "MM/YYYY")
                .Text = VB6.Format(mDate, "DD/MM/YYYY")
            Next
        End With
    End Sub
    Private Sub frmEmpAttn_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Show1()
    End Sub
    Private Sub frmEmpAttn_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmEmpAttn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmEmpAttn_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Me.Height = VB6.TwipsToPixelsY(5760)
        'Me.Width = VB6.TwipsToPixelsX(5550)
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mFHalf As Integer
        Dim mSHalf As Integer
        Dim mDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_ATTN_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & lblEmpCode.Text & "'" & vbCrLf & " AND TO_CHAR(ATTN_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblDate.Text, "MMM-YYYY")) & "'"

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                mFHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColSH
                mSHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                If mFHalf <> -1 And mFHalf <> -1 Then
                    SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf & " ATTN_DATE, FIRSTHALF, SECONDHALF, " & vbCrLf & " ADDUSER, ADDDATE) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(lblDate.Text)) & ", " & vbCrLf & " '" & lblEmpCode.Text & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & "  " & mFHalf & ", " & mSHalf & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        Me.hide()
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub Show1()

        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mCode As String
        Dim mRowDate As String
        Dim mAttnDate As String
        Dim mFH As Integer
        Dim mSH As Integer

        Call FormatMain()

        SqlStr = " SELECT ATTN_DATE, FIRSTHALF , SECONDHALF " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE ='" & lblEmpCode.Text & "'" & vbCrLf _
            & " AND TO_CHAR(ATTN_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblDate.Text, "MMM-YYYY")) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            Do While Not RsAttn.EOF
                mAttnDate = VB6.Format(IIf(IsDbNull(RsAttn.Fields("ATTN_DATE").Value), "", RsAttn.Fields("ATTN_DATE").Value), "DD/MM/YYYY")
                mFH = IIf(IsDbNull(RsAttn.Fields("FIRSTHALF").Value), -1, RsAttn.Fields("FIRSTHALF").Value)
                mSH = IIf(IsDbNull(RsAttn.Fields("SECONDHALF").Value), -1, RsAttn.Fields("SECONDHALF").Value)

                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColDate
                    mRowDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")
                    If mAttnDate = mRowDate Then
                        SprdMain.Col = ColFH
                        SprdMain.TypeComboBoxCurSel = mFH + 1

                        SprdMain.Col = ColSH
                        SprdMain.TypeComboBoxCurSel = mSH + 1
                        Exit For
                    End If
                Next
                RsAttn.MoveNext()
            Loop
        End If
    End Sub
    Private Function CheckSalaryMade(ByRef xEmpCode As String, ByRef xSalDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String

        CheckSalaryMade = False
        mCheckDate = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))) & "/" & VB6.Format(xSalDate, "MM/YYYY")

        SqlStr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColFH
        mListIndex = CInt(SprdMain.Value)

        SprdMain.Col = ColSH
        If CDbl(SprdMain.Value) <= 0 Then
            SprdMain.Value = CStr(mListIndex)
        End If

        '    MainClass.SetFocusToCell SprdMain, SprdMain.Row + 1, ColFH
    End Sub
End Class
