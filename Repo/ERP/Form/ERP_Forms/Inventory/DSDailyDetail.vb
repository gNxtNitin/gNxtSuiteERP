Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmDSDailyDetail
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11

    Private Const ColDlvItemCode As Short = 1
    Private Const ColDlvDate As Short = 2
    Private Const ColDlvQty As Short = 3
    Private Const ColDlvActualQty As Short = 4

    Public Sub FormatSprdDlv(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdDlv
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColDlvItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColDlvDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY


            .Col = ColDlvQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColDlvActualQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            MainClass.ProtectCell(SprdDlv, 1, .MaxRows, ColDlvItemCode, ColDlvDate)
            MainClass.ProtectCell(SprdDlv, 1, .MaxRows, ColDlvActualQty, ColDlvActualQty)

        End With
        MainClass.SetSpreadColor(SprdDlv, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmd1Week_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd1Week.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mDlvDate As String

        Dim mWeekDay1 As Integer
        Dim mWeekDay2 As Integer
        Dim mWeekDay3 As Integer
        Dim mWeekDay4 As Integer
        Dim mWeekDay5 As Integer

        mWeekDay1 = 0
        mWeekDay2 = 0
        mWeekDay3 = 0
        mWeekDay4 = 0
        mWeekDay5 = 0

        With SprdDlv
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDlvDate
                mDlvDate = VB6.Format(.Text, "DD/MM/YYYY")

                If VB.Day(CDate(mDlvDate)) < 8 Then
                    If mWeekDay1 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay1 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 15 Then
                    If mWeekDay2 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay2 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 22 Then
                    If mWeekDay3 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay3 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 29 Then
                    If mWeekDay4 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay4 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                Else
                    If mWeekDay5 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay5 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                End If

                .Col = ColDlvQty
                .Text = "0.000"
            Next

            .Col = ColDlvQty
            If mWeekDay1 <> 0 Then
                .Row = mWeekDay1
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay2 <> 0 Then
                .Row = mWeekDay2
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay3 <> 0 Then
                .Row = mWeekDay3
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay4 <> 0 Then
                .Row = mWeekDay4
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay5 <> 0 Then
                .Row = mWeekDay5
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If

        End With

        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function IsHoliday(ByRef pDate As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        IsHoliday = True
        If IsDate(pDate) Then
            SqlStr = " SELECT HOLIDAY_DATE FROM PAY_HOLIDAY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                IsHoliday = True
            Else
                IsHoliday = False
            End If
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmd2Month_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd2Month.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mDlvDate As String

        Dim mWeekDay1 As Integer
        Dim mWeekDay2 As Integer

        mWeekDay1 = 0
        mWeekDay2 = 0

        With SprdDlv
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDlvDate
                mDlvDate = VB6.Format(.Text, "DD/MM/YYYY")

                If VB.Day(CDate(mDlvDate)) < 15 Then
                    If mWeekDay1 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay1 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                Else
                    If mWeekDay2 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay2 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                End If

                .Col = ColDlvQty
                .Text = "0.000"
            Next

            .Col = ColDlvQty
            If mWeekDay1 <> 0 Then
                .Row = mWeekDay1
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay2 <> 0 Then
                .Row = mWeekDay2
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If

        End With

        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmd2Week_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd2Week.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mDlvDate As String

        Dim mWeekDay1 As Integer
        Dim mWeekDay2 As Integer
        Dim mWeekDay3 As Integer
        Dim mWeekDay4 As Integer
        Dim mWeekDay5 As Integer
        Dim mWeekDay6 As Integer
        Dim mWeekDay7 As Integer
        Dim mWeekDay8 As Integer
        Dim mWeekDay9 As Integer
        Dim mWeekDay10 As Integer

        mWeekDay1 = 0
        mWeekDay2 = 0
        mWeekDay3 = 0
        mWeekDay4 = 0
        mWeekDay5 = 0
        mWeekDay6 = 0
        mWeekDay7 = 0
        mWeekDay8 = 0
        mWeekDay9 = 0
        mWeekDay10 = 0

        With SprdDlv
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDlvDate
                mDlvDate = VB6.Format(.Text, "DD/MM/YYYY")

                If VB.Day(CDate(mDlvDate)) < 4 Then
                    If mWeekDay1 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay1 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 8 Then
                    If mWeekDay2 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay2 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 12 Then
                    If mWeekDay3 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay3 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 15 Then
                    If mWeekDay4 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay4 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 18 Then
                    If mWeekDay5 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay5 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 22 Then
                    If mWeekDay6 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay6 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 25 Then
                    If mWeekDay7 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay7 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                ElseIf VB.Day(CDate(mDlvDate)) < 29 Then
                    If mWeekDay8 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay8 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                Else
                    If mWeekDay9 = 0 Then
                        If IsHoliday(mDlvDate) = False Then
                            mWeekDay9 = VB.Day(CDate(mDlvDate))
                        End If
                    End If
                End If

                .Col = ColDlvQty
                .Text = "0.000"
            Next

            .Col = ColDlvQty
            If mWeekDay1 <> 0 Then
                .Row = mWeekDay1
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay2 <> 0 Then
                .Row = mWeekDay2
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay3 <> 0 Then
                .Row = mWeekDay3
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay4 <> 0 Then
                .Row = mWeekDay4
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay5 <> 0 Then
                .Row = mWeekDay5
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If

            If mWeekDay6 <> 0 Then
                .Row = mWeekDay6
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay7 <> 0 Then
                .Row = mWeekDay7
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay8 <> 0 Then
                .Row = mWeekDay8
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If
            If mWeekDay9 <> 0 Then
                .Row = mWeekDay9
                .Text = CStr(Val(txtMonthDeliverySchedule.Text))
            End If

        End With

        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        ConDSDetail = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdDaily_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDaily.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mDlvDate As String

        With SprdDlv
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDlvDate
                mDlvDate = VB6.Format(.Text, "DD/MM/YYYY")


                If IsHoliday(mDlvDate) = False Then
                    .Col = ColDlvQty
                    .Text = CStr(Val(txtMonthDeliverySchedule.Text))
                End If

            Next
        End With

        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        Dim I As Integer
        Dim mActualQty As Double
        Dim mMaxLevelQty As Double

        Call CalcTots()
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then Exit Sub

        With SprdDlv
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDlvQty
                mActualQty = Val(.Text)

                mMaxLevelQty = GetInventoryLevelQty(Trim(LblItemCode.Text), "MAXIMUM_QTY")
                If mMaxLevelQty = 0 Then
                    If MsgQuestion("Max Level Not Define for Item Code : " & LblItemCode.Text & ". Want to process?") = CStr(MsgBoxResult.Yes) Then
                        GoTo UpdateLine
                    Else
                        Exit Sub
                    End If
                End If
                If mActualQty > mMaxLevelQty Then
                    MsgInformation("You Cross the Max Level for Item Code : " & LblItemCode.Text & ". Max Level is : " & mMaxLevelQty)
                    Exit Sub
                End If

            Next
        End With
UpdateLine:

        If InsertIntoTemp_Table = True Then
            ConDSDetail = True
            Me.Hide()
            'Me.Close()
            '' Unload Me
        Else
            ConDSDetail = False
            MsgBox("Can Not Save Daily Delivery Schedule Detail", MsgBoxStyle.Critical)
            cmdOk.Enabled = True
        End If

    End Sub
    Private Sub FrmDSDailyDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormLoaded = True Then Exit Sub
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then
            cmdOk.Enabled = False
        Else
            cmdOk.Enabled = True
        End If

        Call ShowDSDailyDetail()
        FormLoaded = True

    End Sub
    Private Sub FrmDSDailyDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadPart
        FormLoaded = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me)
        Call SetMainFormCordinate(Me)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)


        'Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        'Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        MainClass.ClearGrid(SprdDlv)
        FormatSprdDlv(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LoadPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmDSDailyDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormLoaded = False
        lblSuppCode.Text = ""
        LblItemCode.Text = ""
        LblPODate.Text = ""
        LblPONo.Text = ""
        LblAddMode.Text = ""
        LblModifyMode.Text = ""
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub ShowDSDailyDetail()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mLastDay As Integer
        Dim pSDate As String

        MainClass.ClearGrid(SprdDlv)
        FormatSprdDlv(-1)
        SqlStr = "SELECT * FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND AUTO_KEY_DELV =" & Val(lblPoNo.Text) & "" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(lblItemCode.Text) & "'" & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(LblPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ORDER BY SERIAL_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                I = I + 1

                With SprdDlv
                    .Row = I

                    .Col = ColDlvItemCode
                    .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), LblItemCode.Text, RsTemp.Fields("ITEM_CODE").Value)

                    .Col = ColDlvDate
                    .Text = IIf(IsDbNull(RsTemp.Fields("SERIAL_DATE").Value), "", RsTemp.Fields("SERIAL_DATE").Value)

                    .Col = ColDlvQty
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("PLANNED_QTY").Value), "", RsTemp.Fields("PLANNED_QTY").Value)))

                    .Col = ColDlvActualQty
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ACTUAL_QTY").Value), "", RsTemp.Fields("ACTUAL_QTY").Value)))

                End With

                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    SprdDlv.MaxRows = I + 1
                End If
            Loop
        Else
            With SprdDlv
                mLastDay = MainClass.LastDay(Month(CDate(LblPODate.Text)), Year(CDate(LblPODate.Text)))
                .MaxRows = mLastDay

                For I = 1 To .MaxRows
                    .Row = I


                    .Col = 1
                    .Text = Trim(LblItemCode.Text)

                    pSDate = VB6.Format(I, "00") & "/" & VB6.Format(Month(CDate(LblPODate.Text)), "00") & "/" & VB6.Format(Year(CDate(LblPODate.Text)), "0000")

                    .Col = 2
                    .Text = pSDate

                    .Col = 3
                    .Text = "0.000"

                    .Col = 4
                    .Text = "0.000"
                Next
            End With
        End If
        FormatSprdDlv(-1)
        Call CalcTots()
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset=Nothing
        Dim mQty As Double
        Dim mAcutalQty As Double
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double
        Dim mDate As String

        Dim I As Integer
        Dim j As Integer

        mAcutalQty = 0
        mQty = 0

        With SprdDlv
            j = .MaxRows
            For I = 1 To j
                .Row = I
                '            .Col = ColDlvItemCode
                '            .Text = Trim(lblItemCode.text)

                .Col = ColDlvDate
                mDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColDlvQty
                mQty = mQty + Val(.Text)

                If VB.Day(CDate(mDate)) < 8 Then
                    mWeek1Qty = mWeek1Qty + Val(.Text)
                ElseIf VB.Day(CDate(mDate)) < 15 Then
                    mWeek2Qty = mWeek2Qty + Val(.Text)
                ElseIf VB.Day(CDate(mDate)) < 22 Then
                    mWeek3Qty = mWeek3Qty + Val(.Text)
                ElseIf VB.Day(CDate(mDate)) < 29 Then
                    mWeek4Qty = mWeek4Qty + Val(.Text)
                Else
                    mWeek5Qty = mWeek5Qty + Val(.Text)
                End If

                .Col = ColDlvActualQty
                mAcutalQty = mAcutalQty + Val(.Text)

            Next I
        End With

        lblPlanQty.Text = VB6.Format(mQty, "#0.00")
        lblActual.Text = VB6.Format(mAcutalQty, "#0.00")
        lblWeek1.Text = VB6.Format(mWeek1Qty, "#0.00")
        lblWeek2.Text = VB6.Format(mWeek2Qty, "#0.00")
        lblWeek3.Text = VB6.Format(mWeek3Qty, "#0.00")
        lblWeek4.Text = VB6.Format(mWeek4Qty, "#0.00")
        lblWeek5.Text = VB6.Format(mWeek5Qty, "#0.00")

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function InsertIntoTemp_Table() As Boolean

        On Error GoTo InsertErr
        Dim I As Integer
        Dim mSerialDate As String
        Dim mPlanQty As Double
        Dim mActualQty As Double
        Dim SqlStr As String = ""
        Dim mRefNo As Double


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PUR_DAILY_SCHLD_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND AUTO_KEY_DELV =" & Val(lblPoNo.Text) & "" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(lblItemCode.Text) & "'" & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(LblPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " "

        PubDBCn.Execute(SqlStr)
        mRefNo = Val(LblPONo.Text)

        With SprdDlv
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDlvDate
                mSerialDate = .Text

                .Col = ColDlvQty
                mPlanQty = Val(.Text)

                .Col = ColDlvActualQty
                mActualQty = Val(.Text)

                SqlStr = ""
                If mSerialDate <> "" Then
                    SqlStr = "INSERT INTO TEMP_PUR_DAILY_SCHLD_DET " & " (USERID, AUTO_KEY_DELV, " & vbCrLf & " ITEM_CODE, SERIAL_DATE, PLANNED_QTY, " & vbCrLf & " ACTUAL_QTY, DELV_CNT, SUPP_CUST_CODE, " & vbCrLf & " SCHLD_DATE ) VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & mRefNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblItemCode.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mPlanQty & ", " & mActualQty & ", 0," & vbCrLf & " '" & MainClass.AllowSingleQuote(lblSuppCode.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(LblPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        InsertIntoTemp_Table = True
        Exit Function
InsertErr:
        'Resume
        PubDBCn.RollbackTrans()
        InsertIntoTemp_Table = False
        MsgBox(Err.Description)
    End Function

    Private Sub SprdDlv_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdDlv.KeyPressEvent
        With SprdDlv
            If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then
                SprdDlv_LeaveCell(SprdDlv, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow + 1, False))
                '            SprdDlv_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
            End If

        End With
    End Sub

    Private Sub SprdDlv_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdDlv.KeyUpEvent
        Dim mCol As Short
        Dim mDlvQty As Double

        mCol = SprdDlv.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F3 And mCol = ColDlvQty And SprdDlv.ActiveRow > 1 Then
            SprdDlv.Row = SprdDlv.ActiveRow - 1
            SprdDlv.Col = ColDlvQty
            mDlvQty = Val(SprdDlv.Text)

            SprdDlv.Row = SprdDlv.ActiveRow
            SprdDlv.Col = ColDlvQty
            SprdDlv.Text = CStr(mDlvQty)

        End If
        ''SprdMain_Click ColItemName, 0
    End Sub

    Private Sub SprdDlv_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdDlv.LeaveCell

        On Error GoTo ErrPart
        If eventArgs.NewRow = -1 Then Exit Sub

        '    SprdDlv.Row = SprdDlv.ActiveRow
        '    SprdDlv.Col = ColDlvQty
        '    If Val(SprdDlv.Text) = 0 Then Exit Sub

        Call CalcTots()
        MainClass.SetFocusToCell(SprdDlv, eventArgs.NewRow, eventArgs.NewCol)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub txtMonthDeliverySchedule_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMonthDeliverySchedule.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMonthDeliverySchedule_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMonthDeliverySchedule.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtMonthDeliverySchedule.Text = VB6.Format(txtMonthDeliverySchedule.Text, "0.0000")
        eventArgs.Cancel = Cancel
    End Sub
End Class
