Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmHolidayMst
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColDays As Short = 1
    Private Const ColIsHolidayStaff As Short = 2
    Private Const ColIsHolidayRW As Short = 3
    Private Const ColIsHolidayCW As Short = 4
    Private Const ColType As Short = 5
    Private Const ColAgtWorking As Short = 6
    Private Const ColReason As Short = 7
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim cntCol As Integer

        '    MainClass.ClearGrid sprdHoliday

        With sprdHoliday
            .MaxCols = ColReason

            .set_RowHeight(0, ConRowHeight * 3)
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = -1

            .Col = ColDays
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 10
            .set_ColWidth(ColDays, 8)

            .Col = ColIsHolidayStaff
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColIsHolidayStaff, 8)

            .Col = ColIsHolidayRW
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColIsHolidayRW, 9)

            .Col = ColIsHolidayCW
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColIsHolidayCW, 9)

            .Col = ColAgtWorking
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAgtWorking, 9)

            .Col = ColType
            .CellType = SS_CELL_TYPE_COMBOBOX
            '        If FormActive = False Then
            .TypeComboBoxList = "" & Chr(9) & "SUNDAY" & Chr(9) & "HOLIDAY"
            .TypeComboBoxCurSel = 0
            '        End If
            .set_ColWidth(ColType, 7)

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 100
            .set_ColWidth(ColReason, 15)


            MainClass.ProtectCell(sprdHoliday, 1, .MaxRows, ColDays, ColDays)
            MainClass.SetSpreadColor(sprdHoliday, -1)
            '        sprdHoliday.OperationMode = OperationModeSingle
        End With
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mHoliday As String
        Dim mREASON As String
        Dim mType As String
        Dim mHolidayFlag As String
        Dim mHolidayFlagRW As String
        Dim mHolidayFlagCW As String
        Dim mUpdateCount As Integer
        Dim mAgtWorking As String
        Dim mAppContractor As String
        Dim mSalDate As String
        Dim RsSalTRN As ADODB.Recordset = Nothing

        mSalDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

        SqlStr = "Select COUNT(1) AS CNTREC From PAY_SAL_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.ISARREAR IN ('Y','N')" & vbCrLf & " AND TRN.SAL_DATE>=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSalTRN.EOF = False Then
            If RsSalTRN.Fields("CNTREC").Value > 0 Then
                MsgInformation("Salary Already Process, so you cann't be Change Holiday.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Exit Sub
            End If
        End If

        SqlStr = "Select COUNT(1) AS CNTREC From PAY_CONT_SAL_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.ISARREAR IN ('Y','N')" & vbCrLf & " AND TRN.SAL_DATE>=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSalTRN.EOF = False Then
            If RsSalTRN.Fields("CNTREC").Value > 0 Then
                MsgInformation("Contractor Salary Already Process, so you cann't be Change Holiday.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Exit Sub
            End If
        End If

        With sprdHoliday
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColIsHolidayStaff
                mHolidayFlag = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColIsHolidayRW
                mHolidayFlagRW = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColIsHolidayCW
                mHolidayFlagCW = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")


                If (mHolidayFlag = "Y" Or mHolidayFlagRW = "Y" Or mHolidayFlagCW = "Y") Then
                    .Col = ColType
                    If .TypeComboBoxCurSel = 0 Then
                        MsgInformation("Please Select Holiday Type.")
                        MainClass.SetFocusToCell(sprdHoliday, cntRow, ColType)
                        Exit Sub
                    End If

                    .Col = ColReason
                    If Trim(.Text) = "" Then
                        MsgInformation("Please Enter Holiday Reason.")
                        MainClass.SetFocusToCell(sprdHoliday, cntRow, ColReason)
                        Exit Sub
                    End If
                End If

            Next
        End With

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_HOLIDAY_MST WHERE" & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(HOLIDAY_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'"
        PubDBCn.Execute(SqlStr)

        mUpdateCount = 0
        With sprdHoliday
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColDays
                mHoliday = Trim(.Text)

                .Col = ColIsHolidayStaff
                mHolidayFlag = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColIsHolidayRW
                mHolidayFlagRW = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColIsHolidayCW
                mAppContractor = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColAgtWorking
                mAgtWorking = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColType
                mType = IIf(.TypeComboBoxCurSel = 1, "SD", "HH")



                .Col = ColReason
                mREASON = Trim(.Text)

                If (mHolidayFlag = "Y" Or mHolidayFlagRW = "Y" Or mAppContractor = "Y") Then
                    SqlStr = "INSERT INTO PAY_HOLIDAY_MST ( " & vbCrLf & " COMPANY_CODE, HOLIDAY_DATE, " & vbCrLf & " HOLIDAY_REASON, LEAVE_TYPE, " & vbCrLf & " AGT_WORKING, APP_STAFF, APP_RW, APP_CONTRACTOR) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",TO_DATE('" & VB6.Format(mHoliday, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(mREASON) & "','" & mType & "', " & vbCrLf & " '" & mAgtWorking & "','" & mHolidayFlag & "','" & mHolidayFlagRW & "', '" & mAppContractor & "')"

                    PubDBCn.Execute(SqlStr)

                    '',ADDUser,ADDdate

                    mUpdateCount = mUpdateCount + 1
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Holidays in this month.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdHoliday)
        RefreshScreen()
    End Sub
    Private Sub frmHolidayMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Width = VB6.TwipsToPixelsX(8715)

        lblRunDate.Text = CStr(RunDate)

        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)

        MainClass.ClearGrid(sprdHoliday)
        txtWorkingDays.Text = ""
        txtHolidays.Text = ""

        RefreshScreen()


        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub




    Private Sub UpDYear_DownClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdHoliday)

        txtWorkingDays.Text = ""
        txtHolidays.Text = ""

        RefreshScreen()


    End Sub
    Private Sub UpDYear_UpClick()

        lblRunDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)))
        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdHoliday)

        txtWorkingDays.Text = ""
        txtHolidays.Text = ""

        RefreshScreen()

    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsHoliday As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mDate As String
        Dim mHoliDays As Integer

        Dim Days_in_This_Month As Short

        MainClass.ClearGrid(sprdHoliday)

        mHoliDays = 0

        SqlStr = " SELECT COMPANY_CODE,AGT_WORKING,TO_CHAR(HOLIDAY_DATE,'MM') AS HL_MONTH, " & vbCrLf & " TO_CHAR(HOLIDAY_DATE,'YYYY') AS HL_YEAR, " & vbCrLf & " HOLIDAY_DATE,HOLIDAY_REASON,LEAVE_TYPE, APP_STAFF, APP_RW, APP_CONTRACTOR " & vbCrLf & " FROM PAY_HOLIDAY_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(HOLIDAY_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHoliday, ADODB.LockTypeEnum.adLockOptimistic)

        Days_in_This_Month = (MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))))

        With sprdHoliday
            .MaxRows = Days_in_This_Month
            For cntRow = 1 To Days_in_This_Month
                .Row = cntRow
                .Col = ColDays
                mDate = VB6.Format(cntRow, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
                .Text = mDate

                .Col = ColReason
                If WeekDay(CDate(mDate)) = FirstDayOfWeek.Sunday Then
                    .Text = "SUNDAY"
                End If
            Next

            FillHeading()

            If RsHoliday.EOF = False Then
                Do While Not RsHoliday.EOF
                    For cntRow = 1 To Days_in_This_Month
                        .Row = cntRow
                        .Col = ColDays

                        If CDate(.Text) = CDate(RsHoliday.Fields("HOLIDAY_DATE").Value) Then
                            .Col = ColIsHolidayStaff
                            .Text = IIf(RsHoliday.Fields("APP_STAFF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                            .Col = ColIsHolidayRW
                            .Text = IIf(RsHoliday.Fields("APP_RW").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                            .Col = ColIsHolidayCW
                            .Text = IIf(RsHoliday.Fields("APP_CONTRACTOR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                            .Col = ColType
                            .TypeComboBoxCurSel = IIf(RsHoliday.Fields("LEAVE_TYPE").Value = "SD", 1, 2)

                            .Col = ColReason
                            .Text = RsHoliday.Fields("HOLIDAY_REASON").Value '"IIf(IsNull(RsHoliday!EMP_FNAME), "", RsHoliday!EMP_FNAME)"

                            .Col = ColAgtWorking
                            .Text = IIf(RsHoliday.Fields("AGT_WORKING").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)



                            mHoliDays = mHoliDays + 1
                        End If
                    Next
                    RsHoliday.MoveNext()
                Loop
            End If
        End With

        txtWorkingDays.Text = VB6.Format(Days_in_This_Month - mHoliDays, "0")
        txtHolidays.Text = VB6.Format(mHoliDays, "0")


        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
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
