Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.PowerPacks
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLeave
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection

    Private Const ColCode As Short = 1
    Private Const ColType As Short = 2
    Private Const ColOpening As Short = 3
    Private Const ColEntitle As Short = 4
    Private Const ColAvailed As Short = 5
    Private Const ColClosing As Short = 6

    Private Const ConRowHeight As Short = 12
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub frmLeave_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Show1()
    End Sub
    Private Sub frmLeave_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmLeave_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Height = VB6.TwipsToPixelsY(5055)
        Me.Width = VB6.TwipsToPixelsX(5430)
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        FormatSprd(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim RsLeave1 As ADODB.Recordset
        Dim RsLeave2 As ADODB.Recordset
        Dim RsLeave3 As ADODB.Recordset
        Dim cntRow As Integer
        Dim mCode As Integer
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim mOpening As Double
        Dim mEntitlement As Double
        Dim mYear As Short
        Dim mMonField As String
        Dim I As Integer
        Dim mPeriod As Double
        Dim mDOJ As String
        Dim xMonth As Integer

        Dim mStartDate As String
        Dim mYearDays As Integer
        Dim xSalDate As String
        Dim mFromDate As Double

        If MainClass.ValidateWithMasterTable((lblCode.Text), "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOJ = MasterNo
        End If

        '    If Year(lblvwMonth.Caption) = Year(mDOJ) Then
        '        xMonth = Val(lblMonth) - Month(mDOJ)
        '        mPeriod = Round(Val(xMonth) / (12 - Month(mDOJ)), 2)
        '    Else
        '        mPeriod = Round(Val(lblMonth) / 12, 2)
        '    End If


        xSalDate = VB6.Format(lblDate.Text, "DD/MM/YYYY")
        If Year(CDate(xSalDate)) = Year(CDate(mDOJ)) Then
            mStartDate = mDOJ
        Else
            mStartDate = "01/01/" & VB6.Format(xSalDate, "YYYY")
        End If

        mYearDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate("01/01/" & VB6.Format(xSalDate, "YYYY")), CDate("31/12/" & VB6.Format(xSalDate, "YYYY"))) + 1

        mPeriod = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(xSalDate)) + 1
        mPeriod = CDbl(VB6.Format(mPeriod / mYearDays, "0.00000"))

        With SprdLeave1
            For cntRow = 1 To .MaxRows
                mOpening = 0
                mEntitlement = 0
                mFHalf = 0
                mSHalf = 0

                .Row = cntRow
                .Col = 1
                mCode = Val(.Text)

                SqlStr = " SELECT NVL(OPENING,0) AS OPENING , NVL(TOTENTITLE,0) AS TOTENTITLE " & vbCrLf & " FROM PAY_OPLEAVE_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(lblvwMonth.Text)) & " " & vbCrLf & " AND EMP_CODE ='" & lblCode.Text & "'" & vbCrLf & " AND LEAVECODE =" & mCode & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave1, ADODB.LockTypeEnum.adLockOptimistic)

                If RsLeave1.EOF = False Then
                    mOpening = RsLeave1.Fields("OPENING").Value
                    If mCode = EARN Then
                        mEntitlement = GETEntitleEarnLeave(PubDBCn, (lblCode.Text), mCode, (lblDate.Text))
                    Else
                        '                    If Year(xSalDate) = Year(mDOJ) Then
                        mEntitlement = GetLeaveEntitle(mCode, Trim(lblCode.Text), (lblvwMonth.Text)) * mPeriod
                        '                    Else
                        '                        mEntitlement = RsLeave1!TOTENTITLE * mPeriod
                        '                    End If
                    End If
                Else
                    If mCode = EARN Then
                        mEntitlement = GETEntitleEarnLeave(PubDBCn, (lblCode.Text), mCode, (lblDate.Text))
                    End If
                End If

                mEntitlement = System.Math.Round(mEntitlement * 2, 0)
                mEntitlement = mEntitlement / 2

                '            mEntitlement = PaiseRound(mEntitlement, 0.5)
                '            mEntitlement = format(mEntitlement, 0)

                SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(lblvwMonth.Text)) & " " & vbCrLf & " AND EMP_CODE ='" & lblCode.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave2, ADODB.LockTypeEnum.adLockOptimistic)

                If RsLeave2.EOF = False Then
                    Do While Not RsLeave2.EOF
                        If RsLeave2.Fields("FIRSTHALF").Value = mCode Then
                            mFHalf = mFHalf + 0.5
                        End If

                        If RsLeave2.Fields("SECONDHALF").Value = mCode Then
                            mSHalf = mSHalf + 0.5
                        End If
                        RsLeave2.MoveNext()
                    Loop
                End If
                .Col = 3
                .Text = CStr(mOpening)

                .Col = 4
                .Text = CStr(mEntitlement)

                .Col = 5
                .Text = CStr(mFHalf + mSHalf)

                .Col = 6
                .Text = CStr((mOpening + mEntitlement) - (mFHalf + mSHalf))

            Next
        End With

        cntRow = 0
        With sprdLeave2
            For cntRow = 1 To .MaxRows
                mFHalf = 0
                mSHalf = 0

                .Row = cntRow
                .Col = 1
                mCode = Val(.Text)

                SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(lblvwMonth.Text)) & " " & vbCrLf & " AND EMP_CODE ='" & lblCode.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave3, ADODB.LockTypeEnum.adLockOptimistic)
                mFHalf = 0
                mSHalf = 0
                If RsLeave3.EOF = False Then
                    Do While Not RsLeave3.EOF
                        If RsLeave3.Fields("FIRSTHALF").Value = mCode Then
                            mFHalf = mFHalf + 0.5
                        End If

                        If RsLeave3.Fields("SECONDHALF").Value = mCode Then
                            mSHalf = mSHalf + 0.5
                        End If
                        RsLeave3.MoveNext()
                    Loop
                End If
                .Col = 3
                .Text = CStr(CStr(mFHalf + mSHalf))
            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With SprdLeave1
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColType, 13)

            .Col = ColOpening
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOpening, 7)

            .Col = ColEntitle
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColEntitle, 7.4)

            .Col = ColAvailed
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAvailed, 7)

            .Col = ColClosing
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColClosing, 7)
        End With

        With sprdLeave2
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColType, 34)

            .Col = ColOpening
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOpening, 7)
        End With

        FillOpLeave()
        MainClass.SetSpreadColor(SprdLeave1, mRow)
        MainClass.SetSpreadColor(sprdLeave2, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
        Resume
    End Sub
    Private Sub FillOpLeave()


        With SprdLeave1
            .MaxRows = 3
            .MaxCols = 6

            .Row = 1
            .set_RowHeight(1, ConRowHeight)
            .Col = ColCode
            .Text = CStr(EARN)

            .Col = ColType
            .Text = "EARN"

            .Row = 2
            .set_RowHeight(2, ConRowHeight)
            .Col = ColCode
            .Text = CStr(SICK)

            .Col = ColType
            .Text = "SICK"

            .Row = 3
            .set_RowHeight(3, ConRowHeight)
            .Col = ColCode
            .Text = CStr(CASUAL)

            .Col = ColType
            .Text = "CASUAL"
        End With

        With sprdLeave2
            .MaxRows = 4 '5
            .MaxCols = 3

            .Row = 1
            .set_RowHeight(1, ConRowHeight)
            .Col = ColCode
            .Text = CStr(ABSENT)

            .Col = ColType
            .Text = "ABSENT"

            .Row = 2
            .set_RowHeight(2, ConRowHeight)
            .Col = ColCode
            .Text = CStr(WOPAY)

            .Col = ColType
            .Text = "WOPAY"

            .Row = 3
            .set_RowHeight(3, ConRowHeight)
            .Col = ColCode
            .Text = CStr(CPLEARN)

            .Col = ColType
            .Text = "CPLEARN"

            .Row = 4
            .set_RowHeight(4, ConRowHeight)
            .Col = ColCode
            .Text = CStr(CPLAVAIL)

            .Col = ColType
            .Text = "CPLAVAIL"

            '            .Row = 5
            '            .RowHeight(5) = ConRowHeight
            '            .Col = ColCode
            '            .Text = CStr(MATERNITY)
            '
            '            .Col = ColType
            '            .Text = "MATERNITY"

        End With

        MainClass.ProtectCell(SprdLeave1, 1, SprdLeave1.MaxRows, 1, SprdLeave1.MaxCols)
        MainClass.ProtectCell(sprdLeave2, 1, sprdLeave2.MaxRows, 1, sprdLeave2.MaxCols)

    End Sub

    Private Function OpeningLeave(ByRef xMonth As Short, ByRef xOpening As Double, ByRef xTOTENTITLE As Double) As Double

        OpeningLeave = xOpening + xTOTENTITLE

        '    Select Case xMonth
        '
        '        Case MonthConstants.mvwApril
        '            OpeningLeave = xOpening
        '        Case MonthConstants.mvwMay
        '            OpeningLeave = xOpening + (xMon4)
        '        Case MonthConstants.mvwJune
        '            OpeningLeave = xOpening + (xMon4 + xMon5)
        '        Case MonthConstants.mvwJuly
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6)
        '        Case MonthConstants.mvwAugust
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6 + xMon7)
        '        Case MonthConstants.mvwSeptember
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6 + xMon7 + xMon8)
        '        Case MonthConstants.mvwOctober
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6 + xMon7 + xMon8 + xMon9)
        '        Case MonthConstants.mvwNovember
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6 + xMon7 + xMon8 + xMon9 + xMon10)
        '        Case MonthConstants.mvwDecember
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6 + xMon7 + xMon8 + xMon9 + xMon10 + xMon11)
        '        Case MonthConstants.mvwJanuary
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6 + xMon7 + xMon8 + xMon9 + xMon10 + xMon11 + xMon12)
        '        Case MonthConstants.mvwFebruary
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6 + xMon7 + xMon8 + xMon9 + xMon10 + xMon11 + xMon12 + xMon1)
        '        Case MonthConstants.mvwMarch
        '            OpeningLeave = xOpening + (xMon4 + xMon5 + xMon6 + xMon7 + xMon8 + xMon9 + xMon10 + xMon11 + xMon12 + xMon1 + xMon2)
        '    End Select

    End Function
End Class
