Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpLeaveEntry
    Inherits System.Windows.Forms.Form
    Dim RsEmpLeave As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Private Const ColDate As Short = 1
    Private Const ColDay As Short = 2
    Private Const ColFH As Short = 3
    Private Const ColSH As Short = 4
    Private Const ColForm1HL As Short = 5
    Private Const ColForm1SL As Short = 6
    Private Const ColCPLEarn As Short = 7
    Private Const ColCPLFH As Short = 8
    Private Const ColCPLSH As Short = 9
    Private Const ColAgtESI As Short = 10
    Private Const ColAgtLate As Short = 11


    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Dim xEmpCode As String

    Dim pCPLDays As Integer
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub FormatMain()

        Dim cntCol As Integer
        '    MainClass.ClearGrid sprdHoliday

        Call FillDate()

        With sprdMain
            .MaxCols = ColAgtLate

            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = -1

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 10)

            .Col = ColDay
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDay, 10)

            .Col = ColCPLFH
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColCPLFH, 10)

            .Col = ColCPLSH
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColCPLSH, 10)

            For cntCol = ColFH To ColSH
                .Col = cntCol
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "" & Chr(9) & "0 -UNAPPROVED LEAVE" & Chr(9) & "1 -CASUAL" & Chr(9) & "2 -EARN"
                .TypeComboBoxList = .TypeComboBoxList & "3 -SICK" & Chr(9) & "4 -MATERNITY / SP. LEAVE" & Chr(9) & "5 -CPLEARN" & Chr(9) & "6 -APPROVED LEAVE"
                .TypeComboBoxList = .TypeComboBoxList & "7 -CPLAVAIL" & Chr(9) & "8 -SUNDAY" & Chr(9) & "9 -HOLIDAY" & Chr(9) & "10 -PRESENT" & Chr(9) & "11 -WFH"
                .TypeComboBoxCurSel = 0
                .set_ColWidth(cntCol, 16)
            Next


            .Col = ColForm1HL
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAgtESI, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)

            .Col = ColForm1SL
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAgtESI, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)

            .Col = ColCPLEarn
            .CellType = SS_CELL_TYPE_COMBOBOX
            .TypeComboBoxList = "" & Chr(9) & "1-Half Day" & Chr(9) & "2-One Full Day" & Chr(9) & "3-One Full & Half Day" & Chr(9) & "4-Two Full Day"
            .TypeComboBoxCurSel = 0
            .set_ColWidth(cntCol, 10)

            .Col = ColAgtESI
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAgtESI, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            .Col = ColAgtLate
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAgtLate, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            If lblCategory.Text = "E" Then
                MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDate, ColForm1SL)
                MainClass.ProtectCell(sprdMain, 1, .MaxRows, ColCPLFH, ColAgtLate)
            Else
                MainClass.ProtectCell(sprdMain, 1, .MaxRows, ColDate, ColDay)
            End If
            MainClass.SetSpreadColor(sprdMain, -1)
            '        sprdHoliday.OperationMode = OperationModeSingle
        End With
    End Sub
    Private Sub FillDate()

        Dim cntRow As Integer
        Dim mLastDate As Integer
        Dim mDate As String

        mLastDate = MainClass.LastDay(Month(CDate(txtRefDate.Text)), Year(CDate(txtRefDate.Text)))

        With sprdMain
            .MaxRows = mLastDate
            For cntRow = 1 To mLastDate
                .Row = cntRow
                .Col = ColDate
                mDate = VB6.Format(cntRow, "00") & "/" & VB6.Format(txtRefDate.Text, "MM/YYYY")
                .Text = VB6.Format(mDate, "DD/MM/YYYY")

                .Col = ColDay
                .Text = WeekDayName(WeekDay(CDate(mDate), FirstDayOfWeek.System))
            Next
        End With
    End Sub
    Private Sub frmEmpLeaveEntry_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmEmpLeaveEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
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
        Dim mCPLFH As String
        Dim mCPLSH As String
        Dim mCheckNoRecord As Boolean
        Dim mInsertData As Boolean
        Dim mCPLEarn As Integer
        Dim mAgtESI As String
        Dim mForm1HL As String
        Dim mForm1SL As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If lblCategory.Text <> "E" Then
            SqlStr = "DELETE FROM PAY_ATTN_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & txtEmpCode.Text & "'" & vbCrLf _
                & " AND TO_CHAR(ATTN_DATE,'YYYYMM')='" & VB6.Format(txtRefDate.Text, "YYYYMM") & "'"

            PubDBCn.Execute(SqlStr)
        End If

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                mFHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColSH
                mSHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColCPLEarn
                mCPLEarn = IIf(.Text = "", 0, VB.Left(.Text, 1))

                .Col = ColForm1HL
                mForm1HL = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColForm1SL
                mForm1SL = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")


                .Col = ColAgtESI
                mAgtESI = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColAgtLate
                mAgtLate = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColCPLFH
                If mFHalf = CPLAVAIL Then
                    mCPLFH = VB6.Format(.Text, "DD/MM/YYYY")
                Else
                    mCPLFH = ""
                End If

                .Col = ColCPLSH
                If mSHalf = CPLAVAIL Then
                    mCPLSH = VB6.Format(.Text, "DD/MM/YYYY")
                Else
                    mCPLSH = ""
                End If

                If lblCategory.Text = "E" Then
                    If CheckAttnData(Trim(TxtEmpCode.Text), mDate) = False Then
                        SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf _
                            & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                            & " ATTN_DATE, FIRSTHALF, SECONDHALF, AGT_LATE," & vbCrLf _
                            & " ADDUSER, ADDDATE,CPL_AGT_DATE_FH,CPL_AGT_DATE_SH,CPL_EARN,AGT_ESI,EXTRA_LEAVE,EXTRA_LEAVE_2) VALUES (" & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(txtRefDate.Text)) & ", " & vbCrLf _
                            & " '" & txtEmpCode.Text & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & "  " & mFHalf & ", " & mSHalf & ", '" & mAgtLate & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mCPLFH, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mCPLSH, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mCPLEarn & ",'" & mAgtESI & "','" & mForm1HL & "', '" & mForm1SL & "')"
                    Else
                        SqlStr = "UPDATE PAY_ATTN_MST SET CPL_EARN=" & mCPLEarn & " " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND EMP_CODE='" & txtEmpCode.Text & "'" & vbCrLf _
                            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If
                    PubDBCn.Execute(SqlStr)
                Else
                    If mFHalf <> -1 Or mSHalf <> -1 Or mCPLEarn > 0 Then
                        SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                            & " ATTN_DATE, FIRSTHALF, SECONDHALF, AGT_LATE," & vbCrLf _
                            & " ADDUSER, ADDDATE,CPL_AGT_DATE_FH,CPL_AGT_DATE_SH,CPL_EARN,AGT_ESI,EXTRA_LEAVE,EXTRA_LEAVE_2) VALUES (" & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(txtRefDate.Text)) & ", " & vbCrLf _
                            & " '" & txtEmpCode.Text & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & "  " & mFHalf & ", " & mSHalf & ", '" & mAgtLate & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mCPLFH, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mCPLSH, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mCPLEarn & ",'" & mAgtESI & "','" & mForm1HL & "', '" & mForm1SL & "')"

                        PubDBCn.Execute(SqlStr)
                    End If
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        Update1 = True
        '    Unload Me
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        If Err.Number = -2147467259 Then
            MsgInformation("Please select CPL Earn Leave in CPL EARN Col.")
        Else
            MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        End If
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ERR1
        Select Case eventArgs.Col
            Case ColCPLFH
                If eventArgs.Row = 0 Then
                    SearchEarnDate(ColCPLFH)
                End If
            Case ColCPLSH
                If eventArgs.Row = 0 Then
                    SearchEarnDate(ColCPLSH)
                End If
        End Select

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Exit Sub
ERR1:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub SearchEarnDate(ByRef pCol As Integer)

        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mFromDate As String
        Dim mToDate As String


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 105 Then
            mToDate = "01/01/" & VB6.Format(txtRefDate.Text, "YYYY")
            mFromDate = "31/12/" & VB6.Format(txtRefDate.Text, "YYYY")
        Else
            mToDate = "01/" & VB6.Format(txtRefDate.Text, "MM/YYYY")
            mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, pCPLDays, CDate(mToDate)))
        End If

        SqlStr = " SELECT ATTN_DATE, SUM(CPL_EARN) AS BALANCE_HALF" & vbCrLf _
            & " FROM vw_PAY_CPL_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf _
            & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ATTN_DATE<TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " GROUP BY ATTN_DATE HAVING SUM(CPL_EARN)>0"



        '    SqlStr = SqlStr & vbCrLf & " UNION "
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''                & " SELECT ATTN_DATE " & vbCrLf _
        ''                & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'" & vbCrLf _
        ''                & " AND SECONDHALF = " & CPLEARN & "" & vbCrLf _
        ''                & " AND ATTN_DATE>='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                & " AND ATTN_DATE<'" & VB6.Format(mToDate, "DD-MMM-YYYY") & "'"

        '    SqlStr = SqlStr & vbCrLf & " MINUS  ("
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " SELECT CPL_AGT_DATE_FH ATTN_DATE, 1 AS HALF_CNT " & vbCrLf _
        ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'" & vbCrLf _
        ''            & " AND FIRSTHALF= " & CPLAVAIL & "" & vbCrLf _
        ''            & " AND CPL_AGT_DATE_FH>='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "'"
        '
        '    SqlStr = SqlStr & vbCrLf & " UNION "
        '
        '    SqlStr = SqlStr & vbCrLf _
        ''            & " SELECT CPL_AGT_DATE_SH ATTN_DATE,1 AS HALF_CNT" & vbCrLf _
        ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'" & vbCrLf _
        ''            & " AND SECONDHALF= " & CPLAVAIL & "" & vbCrLf _
        ''            & " AND CPL_AGT_DATE_SH>='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "')"

        '    SqlStr = SqlStr & vbCrLf _
        ''             & " ORDER BY 1"

        MainClass.SearchGridMasterBySQL2("", SqlStr, , , "D")

        If AcName <> "" Then
            sprdMain.Row = sprdMain.ActiveRow
            sprdMain.Col = pCol
            sprdMain.Text = AcName
            MainClass.SetFocusToCell(sprdMain, sprdMain.ActiveRow, pCol)
        End If
        Exit Sub

    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mListIndex As Integer

        Dim cntRow As Integer
        Dim mCurrentDate As String

        Dim mFHalf As Integer
        Dim mSHalf As Integer
        Dim mCPLEarn As Double
        Dim mCPLAvail As Double

        Dim mCPLFH As String
        Dim mCPLSH As String

        If eventArgs.NewRow = -1 Then Exit Sub

        sprdMain.Row = sprdMain.ActiveRow
        If eventArgs.col = ColFH Then
            SprdMain.Col = ColFH
            mListIndex = CInt(SprdMain.Value)

            If mListIndex < 12 Then
                SprdMain.Col = ColSH
                If CDbl(SprdMain.Value) <= 0 Then
                    SprdMain.Value = CStr(mListIndex)
                End If
            End If
        End If

        mCPLEarn = 0
        mCPLAvail = 0


        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mCurrentDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                mFHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColSH
                mSHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColCPLFH
                mCPLFH = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColCPLSH
                mCPLSH = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColCPLEarn
                mCPLEarn = mCPLEarn + (IIf(.Text = "", 0, VB.Left(.Text, 1)) * 0.5)

                If mFHalf = CPLAVAIL Then
                    If VB6.Format(mCurrentDate, "YYYYMM") = VB6.Format(mCPLFH, "YYYYMM") Then
                        mCPLEarn = mCPLEarn - 0.5
                    End If
                End If

                If mSHalf = CPLAVAIL Then
                    If VB6.Format(mCurrentDate, "YYYYMM") = VB6.Format(mCPLSH, "YYYYMM") Then
                        mCPLEarn = mCPLEarn - 0.5
                    End If
                End If
            Next
        End With

        With sprdMain
            .Row = sprdMain.ActiveRow
            .Col = ColDate
            mCurrentDate = VB6.Format(.Text, "DD/MM/YYYY")

            .Col = ColFH
            mFHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

            .Col = ColSH
            mSHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

            Select Case eventArgs.col
                Case ColCPLFH
                    .Col = ColCPLFH
                    mCPLFH = VB6.Format(.Text, "DD/MM/YYYY")

                    If mFHalf = CPLAVAIL Then
                        If Trim(mCPLFH) = "" Then
                            Exit Sub
                        ElseIf Not IsDate(mCPLFH) Then
                            MsgInformation("Please enter the CPL Avail Agt. Date [" & mCurrentDate & "] .")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLFH)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                        If CDate(mCPLFH) < CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Day, pCPLDays, CDate(mCurrentDate))) Then
                            MsgInformation("CPL Earn Date cann't be more than " & System.Math.Abs(pCPLDays) & " days [" & mCurrentDate & "] .")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLFH)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                        If VB6.Format(mCurrentDate, "YYYYMMDD") <= VB6.Format(mCPLFH, "YYYYMMDD") Then
                            If mCPLEarn < 0 Then
                                MsgInformation("CPL Cann't be Avail in Advance, So Cann't be Save [" & mCurrentDate & "] .")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLFH)
                                eventArgs.cancel = True
                                Exit Sub
                            End If
                        End If
                        If VB6.Format(mCurrentDate, "YYYYMM") = VB6.Format(mCPLFH, "YYYYMM") Then
                            If mCPLEarn < 0 Then
                                MsgInformation("No Balance Earn Leave, So Cann't be Save [" & mCurrentDate & "] .")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLFH)
                                eventArgs.cancel = True
                                Exit Sub
                            Else
                                If CheckCurrentCPLEarnBalance(Trim(txtEmpCode.Text), mCurrentDate, mCPLFH) < 0 Then
                                    MsgInformation("No Balance CPL on such Date [" & mCurrentDate & "] .")
                                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLFH)
                                    eventArgs.cancel = True
                                    Exit Sub
                                End If
                            End If
                        Else
                            mCPLAvail = GetEarnAvail(mCPLFH)
                            If CheckCPLEarnBalance(Trim(txtEmpCode.Text), mCurrentDate, mCPLFH, mCPLAvail) = False Then
                                MsgInformation("No Balance CPL on such Date [" & mCurrentDate & "] .")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLFH)
                                eventArgs.cancel = True
                                Exit Sub
                            End If
                        End If
                    End If
                Case ColCPLSH
                    .Col = ColCPLSH
                    mCPLSH = VB6.Format(.Text, "DD/MM/YYYY")

                    If mSHalf = CPLAVAIL Then
                        If Trim(mCPLSH) = "" Then
                            Exit Sub
                        ElseIf Not IsDate(mCPLSH) Then
                            MsgInformation("Please enter the CPL Avail Agt. Date [" & mCurrentDate & "] .")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLSH)
                            eventArgs.cancel = True
                            Exit Sub
                        End If

                        If CDate(mCPLSH) < CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Day, pCPLDays, CDate(mCurrentDate))) Then
                            MsgInformation("CPL Earn Date cann't be more than " & System.Math.Abs(pCPLDays) & " days [" & mCurrentDate & "] .")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLSH)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                        If VB6.Format(mCurrentDate, "YYYYMMDD") <= VB6.Format(mCPLSH, "YYYYMMDD") Then
                            If mCPLEarn < 0 Then
                                MsgInformation("CPL Cann't be Avail in Advance, So Cann't be Save [" & mCurrentDate & "] .")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLSH)
                                eventArgs.cancel = True
                                Exit Sub
                            End If
                        End If
                        If VB6.Format(mCurrentDate, "YYYYMM") = VB6.Format(mCPLSH, "YYYYMM") Then
                            If mCPLEarn < 0 Then
                                MsgInformation("No Balance CPL, So Cann't be Save [" & mCurrentDate & "] .")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLSH)
                                eventArgs.cancel = True
                                Exit Sub
                            Else
                                If CheckCurrentCPLEarnBalance(Trim(txtEmpCode.Text), mCurrentDate, mCPLSH) < 0 Then
                                    MsgInformation("No Balance CPL on such Date [" & mCurrentDate & "] .")
                                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLSH)
                                    eventArgs.cancel = True
                                    Exit Sub
                                End If

                            End If
                        Else
                            mCPLAvail = GetEarnAvail(mCPLSH)
                            If CheckCPLEarnBalance(Trim(txtEmpCode.Text), mCurrentDate, mCPLSH, mCPLAvail) = False Then
                                MsgInformation("No Balance CPL on such Date [" & mCurrentDate & "] .")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCPLSH)
                                eventArgs.cancel = True
                                Exit Sub
                            End If
                        End If
                    End If
            End Select
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    MainClass.SetFocusToCell SprdMain, SprdMain.Row + 1, ColFH
        'Resume
    End Sub

    Public Function CheckCurrentCPLEarnBalance(ByRef mCode As String, ByRef pDate As String, ByRef pEarnDate As String) As Double
        On Error GoTo ErrFillLeaves
        Dim cntRow As Integer
        Dim mCheckDate As String
        Dim mCPLEarn As Double
        Dim mFHalf As Double
        Dim mSHalf As Double

        Dim mCPLFH As String
        Dim mCPLSH As String

        CheckCurrentCPLEarnBalance = 0
        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColDate
                mCheckDate = VB6.Format(.Text, "DD/MM/YYYY")

                If CDate(mCheckDate) <= CDate(pDate) Then
                    If VB6.Format(pEarnDate, "YYYYMMDD") = VB6.Format(mCheckDate, "YYYYMMDD") Then
                        .Col = ColCPLEarn
                        CheckCurrentCPLEarnBalance = IIf(.Text = "", 0, VB.Left(.Text, 1)) * 0.5
                    End If

                    .Col = ColFH
                    mFHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                    .Col = ColSH
                    mSHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                    .Col = ColCPLFH
                    mCPLFH = VB6.Format(.Text, "DD/MM/YYYY")

                    .Col = ColCPLSH
                    mCPLSH = VB6.Format(.Text, "DD/MM/YYYY")

                    If mFHalf = CPLAVAIL Then
                        If VB6.Format(pEarnDate, "YYYYMMDD") = VB6.Format(mCPLFH, "YYYYMMDD") Then
                            CheckCurrentCPLEarnBalance = CheckCurrentCPLEarnBalance - 0.5
                        End If
                    End If

                    If mSHalf = CPLAVAIL Then
                        If VB6.Format(pEarnDate, "YYYYMMDD") = VB6.Format(mCPLSH, "YYYYMMDD") Then
                            CheckCurrentCPLEarnBalance = CheckCurrentCPLEarnBalance - 0.5
                        End If
                    End If
                End If
            Next
        End With

        Exit Function
ErrFillLeaves:
        CheckCurrentCPLEarnBalance = 0
    End Function

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsEmpLeave, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        TxtEmpCode.Text = ""
        TxtEmpName.Text = ""
        txtDept.Text = ""
        txtPlace.Text = ""

        If RsCompany.Fields("COMPANY_CODE").Value = 12 And lblCategory.Text = "W" Then
            pCPLDays = -180
        Else
            pCPLDays = -120
        End If


        MainClass.ClearGrid(sprdMain)
        Call FormatMain()
        MainClass.ButtonStatus(Me, XRIGHT, RsEmpLeave, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsEmpLeave, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim SqlStr As String = ""

        SqlStr = ""
        If lblCategory.Text = "W" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2'"
        ElseIf lblCategory.Text = "S" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1'"
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If MainClass.SearchGridMaster((TxtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            TxtEmpCode.Text = AcName1
            TxtEmpName.Text = AcName
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsEmpLeave.EOF = False Then RsEmpLeave.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        '    If txtTDSName.Text = "" Then MsgExclamation "Nothing to delete": Exit Sub
        If Not RsEmpLeave.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsEmpLeave.EOF = True Then
                    Clear1()
                Else
                    Clear1()
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick


        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = 1
        txtRefDate.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        SprdView.Col = 2
        TxtEmpCode.Text = VB6.Format(SprdView.Text, "000000")

        txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Public Sub frmEmpLeaveEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub

        If lblCategory.Text = "W" Then
            Me.Text = "Leave Entry" & " - Workers"
        ElseIf lblCategory.Text = "S" Then
            Me.Text = "Leave Entry" & " - Staff"
        Else
            Me.Text = "CPL Earn Entry"
        End If

        SqlStr = "SELECT * FROM PAY_ATTN_MST WHERE  1<>1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpLeave, ADODB.LockTypeEnum.adLockReadOnly)
        Clear1()

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()

        Show1()

        If RsEmpLeave.EOF = True Then
            If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        End If
        Call FormatMain()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmEmpLeaveEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7485)
        'Me.Width = VB6.TwipsToPixelsX(11565)
        '    Me.Caption = "Leave Entry"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmEmpLeaveEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsEmpLeave = Nothing
    End Sub
    Private Sub Show1()

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
        Dim mAgtLate As String
        Dim mCPLFH As String
        Dim mCPLSH As String
        Dim mCPLEarn As Integer
        Dim mAgtESI As String
        Dim mForm1FH As String
        Dim mForm1SH As String

        If Not RsEmpLeave.EOF Then

            txtRefDate.Text = "01/" & VB6.Format(IIf(IsDbNull(RsEmpLeave.Fields("ATTN_DATE").Value), "", RsEmpLeave.Fields("ATTN_DATE").Value), "MM/YYYY")
            Call FillDate()
            TxtEmpCode.Text = IIf(IsDbNull(RsEmpLeave.Fields("EMP_CODE").Value), "", RsEmpLeave.Fields("EMP_CODE").Value)
            mEmpCode = IIf(IsDbNull(RsEmpLeave.Fields("EMP_CODE").Value), "", RsEmpLeave.Fields("EMP_CODE").Value)
            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                TxtEmpName.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtDept.Text = MasterNo
            End If

            '        txtPlace.Text = IIf(IsNull(RsEmpLeave!PLACE_VISIT), "", RsEmpLeave!PLACE_VISIT)

            Do While Not RsEmpLeave.EOF
                mAttnDate = VB6.Format(IIf(IsDbNull(RsEmpLeave.Fields("ATTN_DATE").Value), "", RsEmpLeave.Fields("ATTN_DATE").Value), "DD/MM/YYYY")
                mFH = IIf(IsDbNull(RsEmpLeave.Fields("FIRSTHALF").Value), -1, RsEmpLeave.Fields("FIRSTHALF").Value)
                mSH = IIf(IsDbNull(RsEmpLeave.Fields("SECONDHALF").Value), -1, RsEmpLeave.Fields("SECONDHALF").Value)
                mAgtLate = IIf(IsDbNull(RsEmpLeave.Fields("AGT_LATE").Value), "N", RsEmpLeave.Fields("AGT_LATE").Value)
                mAgtESI = IIf(IsDbNull(RsEmpLeave.Fields("AGT_ESI").Value), "N", RsEmpLeave.Fields("AGT_ESI").Value)

                mCPLFH = VB6.Format(IIf(IsDbNull(RsEmpLeave.Fields("CPL_AGT_DATE_FH").Value), "", RsEmpLeave.Fields("CPL_AGT_DATE_FH").Value), "DD/MM/YYYY")
                mCPLSH = VB6.Format(IIf(IsDbNull(RsEmpLeave.Fields("CPL_AGT_DATE_SH").Value), "", RsEmpLeave.Fields("CPL_AGT_DATE_SH").Value), "DD/MM/YYYY")

                mCPLEarn = IIf(IsDbNull(RsEmpLeave.Fields("CPL_EARN").Value), 0, RsEmpLeave.Fields("CPL_EARN").Value)

                mForm1FH = IIf(IsDBNull(RsEmpLeave.Fields("EXTRA_LEAVE").Value), "N", RsEmpLeave.Fields("EXTRA_LEAVE").Value)
                mForm1SH = IIf(IsDBNull(RsEmpLeave.Fields("EXTRA_LEAVE_2").Value), "N", RsEmpLeave.Fields("EXTRA_LEAVE_2").Value)

                For cntRow = 1 To sprdMain.MaxRows
                    sprdMain.Row = cntRow
                    sprdMain.Col = ColDate
                    mRowDate = VB6.Format(sprdMain.Text, "DD/MM/YYYY")
                    If mAttnDate = mRowDate Then
                        sprdMain.Col = ColFH
                        sprdMain.TypeComboBoxCurSel = mFH + 1

                        sprdMain.Col = ColSH
                        sprdMain.TypeComboBoxCurSel = mSH + 1

                        SprdMain.Col = ColForm1HL
                        SprdMain.Value = IIf(mForm1FH = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                        SprdMain.Col = ColForm1SL
                        SprdMain.Value = IIf(mForm1SH = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                        SprdMain.Col = ColAgtESI
                        sprdMain.Value = IIf(mAgtESI = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                        sprdMain.Col = ColAgtLate
                        sprdMain.Value = IIf(mAgtLate = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                        sprdMain.Col = ColCPLFH
                        sprdMain.Text = VB6.Format(mCPLFH, "DD/MM/YYYY")

                        sprdMain.Col = ColCPLSH
                        sprdMain.Text = VB6.Format(mCPLSH, "DD/MM/YYYY")

                        sprdMain.Col = ColCPLEarn
                        sprdMain.TypeComboBoxCurSel = mCPLEarn

                        Exit For
                    End If
                Next
                RsEmpLeave.MoveNext()
            Loop
            RsEmpLeave.MoveFirst()
            Call FillLeaves((TxtEmpCode.Text))
        End If

        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsEmpLeave, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillLeaves(ByRef mCode As String)

        On Error GoTo ErrPart
        Dim RsOpLeave As ADODB.Recordset
        Dim RsLeave As ADODB.Recordset
        Dim mOpSick As Double
        Dim mOpCasual As Double
        Dim mOpEL As Double

        Dim mSick As Double
        Dim mCasual As Double
        Dim mEL As Double
        Dim mCPL As Double
        Dim mCPL_A As Double
        Dim mDOJ As String

        Dim mMonth As Short
        Dim mYear As Short

        Dim I As Integer
        Dim mMonField As Object
        Dim mon As String
        Dim mPeriod As Double

        Dim mStartDate As String
        Dim mYearDays As Integer
        Dim xSalDate As String
        Dim mMonthStartDate As String

        Dim mCPLEarn As Double
        Dim mCPLAvail As Double
        Dim mBalance As Double
        Dim mCPLFrom As String
        Dim mRefDate As String

        Dim mOpML As Double
        Dim mML As Double
        Dim mCashPaid As Double
        Dim mPaidFrom As String

        mCPLEarn = 0
        mCPLAvail = 0
        mBalance = 0
        '
        '    If Trim(txtRefDate.Text) = "" Then Exit Sub
        '
        '    If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDOJ = MasterNo
        '    End If
        '    xSalDate = MainClass.LastDay(Month(txtRefDate.Text), Year(txtRefDate.Text)) & "/" & vb6.Format(txtRefDate.Text, "MM/YYYY")
        '
        '    mOpEL = GETEntitleEarnLeave(PubDBCn, mCode, EARN, xSalDate)
        ''    mCPL = GETCPL(PubDBCn, mCode, xSalDate)

        mRefDate = MainClass.LastDay(Month(CDate(txtRefDate.Text)), Year(CDate(txtRefDate.Text))) & "/" & VB6.Format(txtRefDate.Text, "MM/YYYY")
        mOpSick = GetOpeningLeaves(mCode, mRefDate, SICK, "Y", "Y", "")
        mOpCasual = GetOpeningLeaves(mCode, mRefDate, CASUAL, "Y", "Y", "")
        mOpML = GetOpeningLeaves(mCode, mRefDate, MATERNITY, "Y", "Y", "")

        mOpEL = GetOpeningLeaves(mCode, mRefDate, EARN, "Y", "Y", "")
        mCPL = GetOpeningLeaves(mCode, mRefDate, CPLEARN, "Y", "Y", "")


        mSick = GetLeavesAvail(mCode, "", mRefDate, SICK)
        mCasual = GetLeavesAvail(mCode, "", mRefDate, CASUAL)
        mEL = GetLeavesAvail(mCode, "", mRefDate, EARN)
        mCPL = GetLeavesAvail(mCode, "", mRefDate, CPLEARN)
        mML = GetLeavesAvail(mCode, "", mRefDate, MATERNITY)

        mCPL_A = GetLeavesAvail(mCode, "", mRefDate, CPLAVAIL)


        '    If Year(xSalDate) = Year(mDOJ) Then
        '        mStartDate = mDOJ
        '    Else
        '        mStartDate = "01/01/" & vb6.Format(xSalDate, "YYYY")
        '    End If
        '
        '    mYearDays = DateDiff("d", CDate("01/01/" & vb6.Format(xSalDate, "YYYY")), CDate("31/12/" & vb6.Format(xSalDate, "YYYY"))) + 1
        '
        '    mPeriod = DateDiff("d", CDate(mStartDate), CDate(xSalDate)) + 1
        '    mPeriod = Format(mPeriod / mYearDays, "0.00000")
        '
        ''    mPeriod = Round(Month(lblDate.Caption) / 12, 2)
        '
        '    SqlStr = " SELECT NVL(OPENING,0) AS OPENING, NVL(TOTENTITLE,0) AS  TOTENTITLE, LEAVECODE " & vbCrLf _
        ''        & " FROM PAY_OPLEAVE_TRN " & vbCrLf _
        ''        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''        & " AND PAYYEAR =" & Year(txtRefDate.Text) & "" & vbCrLf _
        ''        & " AND EMP_CODE ='" & mCode & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOpLeave, adLockOptimistic
        '
        '    If RsOpLeave.EOF = False Then
        '        Do While Not RsOpLeave.EOF
        '            If RsOpLeave!LeaveCode = SICK Then
        '                mOpSick = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING)
        '                mOpSick = mOpSick + IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE) * mPeriod        ''(GetLeaveEntitle(Val(RsOpLeave!LeaveCode)) * mPeriod)
        '                mOpSick = Round(mOpSick * 2, 0) / 2
        '            ElseIf RsOpLeave!LeaveCode = CASUAL Then
        '                mOpCasual = IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING)
        '                mOpCasual = mOpCasual + IIf(IsNull(RsOpLeave!TOTENTITLE), 0, RsOpLeave!TOTENTITLE) * mPeriod
        '                mOpCasual = Round(mOpCasual * 2, 0) / 2
        '            ElseIf RsOpLeave!LeaveCode = EARN Then
        '                mOpEL = mOpEL + IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING)
        '            ElseIf RsOpLeave!LeaveCode = CPLEARN Then
        '                mCPL = mCPL + IIf(IsNull(RsOpLeave!OPENING), 0, RsOpLeave!OPENING)
        '            End If
        '
        '            RsOpLeave.MoveNext
        '        Loop
        '    End If

        '    SqlStr = " SELECT * " & vbCrLf _
        ''        & " FROM PAY_ATTN_MST " & vbCrLf _
        ''        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''        & " AND PAYYEAR =" & Year(txtRefDate.Text) & "" & vbCrLf _
        ''        & " AND EMP_CODE ='" & mCode & "'"
        '
        '    SqlStr = SqlStr & vbCrLf & "AND ATTN_DATE<='" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "'"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsLeave, adLockOptimistic
        '
        '    If RsLeave.EOF = False Then
        '        Do While Not RsLeave.EOF
        '            If RsLeave!FIRSTHALF = SICK And RsLeave!SECONDHALF = SICK Then
        '                mSick = mSick + 1
        '            ElseIf RsLeave!FIRSTHALF = SICK Or RsLeave!SECONDHALF = SICK Then
        '                mSick = mSick + 0.5
        '            End If
        '
        '            If RsLeave!FIRSTHALF = CASUAL And RsLeave!SECONDHALF = CASUAL Then
        '                mCasual = mCasual + 1
        '            ElseIf RsLeave!FIRSTHALF = CASUAL Or RsLeave!SECONDHALF = CASUAL Then
        '                mCasual = mCasual + 0.5
        '            End If
        '
        '            If RsLeave!FIRSTHALF = EARN And RsLeave!SECONDHALF = EARN Then
        '                mEL = mEL + 1
        '            ElseIf RsLeave!FIRSTHALF = EARN Or RsLeave!SECONDHALF = EARN Then
        '                mEL = mEL + 0.5
        '            End If
        '
        '            If RsLeave!FIRSTHALF = CPLEARN And RsLeave!SECONDHALF = CPLEARN Then
        '                mCPL = mCPL + 1
        '            ElseIf RsLeave!FIRSTHALF = CPLEARN Or RsLeave!SECONDHALF = CPLEARN Then
        '                mCPL = mCPL + 0.5
        '            End If
        '
        '            If RsCompany.Fields("COMPANY_CODE").Value = 15 And CDate(RsLeave!ATTN_DATE) >= CDate("01/09/2012") Then
        ''                mCPLFrom = DateAdd("d", -120, Format(txtRefDate.Text, "DD/MM/YYYY"))
        ''                If GetOpeningCPL(mCode, mCPLFrom, mCPLEarn, mCPLAvail, mBalance) = False Then GoTo ErrPart
        '                If Format(RsLeave!ATTN_DATE, "YYYYMM") = Format(xSalDate, "YYYYMM") Then
        ''                    If RsLeave!FIRSTHALF = CPLAVAIL And RsLeave!SECONDHALF = CPLAVAIL Then
        ''                        mCPL_A = mCPL_A + 1
        ''                    ElseIf RsLeave!FIRSTHALF = CPLAVAIL Or RsLeave!SECONDHALF = CPLAVAIL Then
        ''                        mCPL_A = mCPL_A + 0.5
        ''                    End If
        ''                End If
        '            Else
        '                If RsLeave!FIRSTHALF = CPLAVAIL And RsLeave!SECONDHALF = CPLAVAIL Then
        '                    mCPL_A = mCPL_A + 1
        '                ElseIf RsLeave!FIRSTHALF = CPLAVAIL Or RsLeave!SECONDHALF = CPLAVAIL Then
        '                    mCPL_A = mCPL_A + 0.5
        '                End If
        '            End If
        '            RsLeave.MoveNext
        '        Loop
        '    End If


        mPaidFrom = MainClass.LastDay(Month(CDate(txtRefDate.Text)), Year(CDate(txtRefDate.Text))) & "/" & VB6.Format(txtRefDate.Text, "MM/YYYY")

        mCashPaid = GetPaidEL(mCode, mPaidFrom, PubDBCn, "", mPaidFrom)

        lblBalSL.Text = VB6.Format(mOpSick - mSick, "0.0")
        lblBalCL.Text = VB6.Format(mOpCasual - mCasual, "0.0")
        lblBalEL.Text = VB6.Format(mOpEL - mEL - mCashPaid, "0.0")
        lblBalML.Text = VB6.Format(mOpML - mML, "0.0")



        lblBalCPL.Text = VB6.Format(mCPL - mCPL_A, "0.0")


        mCPL_A = 0

        lblAvlSL.Text = VB6.Format(mSick, "0.0")
        lblAvlCL.Text = VB6.Format(mCasual, "0.0")
        lblAvlEL.Text = VB6.Format(mEL, "0.0")
        lblAvlCPL.Text = VB6.Format(mCPL_A, "0.0")
        lblAvlML.Text = VB6.Format(mML, "0.0")
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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
        Dim cntRow As Integer
        Dim mCurrentDate As String
        Dim mOpCPL As Double
        Dim mMonthStartDate As String
        Dim mFHalf As Integer
        Dim mSHalf As Integer
        Dim mCPLEarn As Double
        Dim mTotCPLEarn As Double
        Dim mCPLAvail As Double
        Dim mCurrCPLAvail As Double

        Dim mBalCPLEarn As Double
        Dim mCPLFH As String
        Dim mCPLSH As String

        Dim mCheckCPLEarn As Double
        Dim mTotalCPLAvail As Double
        Dim mGetCPLAvailDate As String
        Dim mWorkingHours As Double
        Dim mShortLeave As Integer
        Dim mIsRoundClock As String
        Dim mAttnCheck As Boolean
        Dim mSalaryCheck As Boolean
        Dim mLastDay As String
        Dim mMLAvail As Double
        Dim mRefDate As String
        Dim mAgtESI As String
        Dim mUnderESI As String

        Dim mCLBalance As Double
        Dim mSLBalance As Double
        Dim mELBalance As Double

        Dim mCLApplied As Double
        Dim mSLApplied As Double
        Dim mELApplied As Double

        Dim mIsHoliday As Boolean
        Dim RsTemp As ADODB.Recordset = Nothing

        If MainClass.ValidateWithMasterTable((TxtEmpCode.Text), "EMP_CODE", "EMP_ESI_FLAG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mUnderESI = MasterNo
        End If

        mAttnCheck = False   '' True

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If

        If TxtEmpCode.Text = "" Then
            MsgInformation("Please Entered Emp Code.")
            TxtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If txtRefDate.Text = "" Then
            MsgInformation("Please Entered Ref Date.")
            txtRefDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If PubUserID = "G0416" Then
            mSalaryCheck = True
        ElseIf PubSuperUser = "S" Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtRefDate.Text), CDate(PubCurrDate)) <= 1 Then
                mSalaryCheck = True
            Else
                mSalaryCheck = False
            End If
        Else
            mSalaryCheck = False
        End If


        '    If mSalaryCheck = False Then
        If lblCategory.Text <> "E" Then
            If CheckSalaryMade((TxtEmpCode.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY")) = True Then
                MsgInformation("Salary Made Againt This Month. So Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If
        '    End If

        If PubSuperUser = "S" Then
        Else
            SqlStr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf _
            & " AND IsArrear='F'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                MsgInformation("F & F Made, So Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If


        mCPLEarn = 0
        mCPLAvail = 0
        mCurrCPLAvail = 0
        mTotCPLEarn = 0
        mShortLeave = 0

        mMLAvail = 0
        mRefDate = VB6.Format(txtRefDate.Text, "DD/MM/YYYY") ' DateAdd("d", -1, txtRefDate.Text)
        mMLAvail = GetOpeningLeaves((TxtEmpCode.Text), mRefDate, MATERNITY, "Y", "Y", "")
        mCLBalance = Val(CStr(1))


        Dim mEmpESPApp As Boolean

        mEmpESPApp = GetEmployeeESIApp(txtEmpCode.Text, mRefDate)
        mSLBalance = GetOpeningLeaves((TxtEmpCode.Text), mRefDate, SICK, "Y", "Y", "")
        mCLBalance = GetOpeningLeaves((TxtEmpCode.Text), mRefDate, CASUAL, "Y", "Y", "")
        mELBalance = GetOpeningLeaves((txtEmpCode.Text), mRefDate, EARN, "Y", "Y", "")




        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mCurrentDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                If mEmpESPApp = True And Val(VB.Left(.Text, 2)) = 3 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 105 Then
                    MsgInformation("ESI Applicable For this Emp, so cann't be avail Sick Leave.")
                    FieldsVarification = False
                    Exit Function
                End If

                mMLAvail = mMLAvail - IIf(Val(VB.Left(.Text, 2)) = 4, 0.5, 0)


                mCLBalance = mCLBalance - IIf(Val(VB.Left(.Text, 2)) = 1, 0.5, 0)
                mELBalance = mELBalance - IIf(Val(VB.Left(.Text, 2)) = 2, 0.5, 0)
                mSLBalance = mSLBalance - IIf(Val(VB.Left(.Text, 2)) = 3, 0.5, 0)

                mCLApplied = mCLApplied + IIf(Val(VB.Left(.Text, 2)) = 1, 0.5, 0)
                mELApplied = mELApplied + IIf(Val(VB.Left(.Text, 2)) = 2, 0.5, 0)
                mSLApplied = mSLApplied + IIf(Val(VB.Left(.Text, 2)) = 3, 0.5, 0)

                .Col = ColSH
                If mEmpESPApp = True And Val(VB.Left(.Text, 2)) = 3 And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 105 Then
                    MsgInformation("ESI Applicable For this Emp, so cann't be avail Sick Leave.")
                    FieldsVarification = False
                    Exit Function
                End If
                mMLAvail = mMLAvail - IIf(Val(VB.Left(.Text, 2)) = 4, 0.5, 0)

                mCLBalance = mCLBalance - IIf(Val(VB.Left(.Text, 2)) = 1, 0.5, 0)
                mELBalance = mELBalance - IIf(Val(VB.Left(.Text, 2)) = 2, 0.5, 0)
                mSLBalance = mSLBalance - IIf(Val(VB.Left(.Text, 2)) = 3, 0.5, 0)

                mCLApplied = mCLApplied + IIf(Val(VB.Left(.Text, 2)) = 1, 0.5, 0)
                mELApplied = mELApplied + IIf(Val(VB.Left(.Text, 2)) = 2, 0.5, 0)
                mSLApplied = mSLApplied + IIf(Val(VB.Left(.Text, 2)) = 3, 0.5, 0)


            Next
        End With

        If PubUserID = "G0416" Then
        Else
            'If mMLAvail < 0 Then
            '    MsgInformation("No Balance in Maternity / Sp. Leave. So Cann't be Save.")
            '    FieldsVarification = False
            '    Exit Function
            'End If

            If mCLBalance < 0 And mCLApplied > 0 Then
                MsgInformation("No Balance in Casual Leave. So Cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If

            If mELBalance < 0 And mELApplied > 0 Then
                MsgInformation("No Balance in Earn Leave. So Cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If

            If mSLBalance < 0 And mSLApplied > 0 Then
                MsgInformation("No Balance in Sick Leave. So Cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If
        End If


        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mCurrentDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                mFHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColSH
                mSHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColCPLFH
                mCPLFH = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColCPLSH
                mCPLSH = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColAgtESI
                mAgtESI = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                If mAgtESI = "Y" Then
                    If mUnderESI = "N" Then
                        MsgInformation("Please uncheck the AGT ESI, Employee no cover under ESI.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    If mFHalf = ABSENT Or mFHalf = WOPAY Or mSHalf = ABSENT Or mSHalf = WOPAY Then

                    Else
                        MsgInformation("Please uncheck the AGT ESI")
                        FieldsVarification = False
                        Exit Function
                    End If

                End If

                mIsRoundClock = IIf(GetRoundClock(Trim(TxtEmpCode.Text), mCurrentDate, "E") = True, "Y", "N")
                mShortLeave = mShortLeave + IIf(CheckShortLeave(Trim(TxtEmpCode.Text), mCurrentDate) = True, 1, 0)

                If (mFHalf = HOLIDAY Or mFHalf = SUNDAY) And (mSHalf = HOLIDAY Or mSHalf = SUNDAY) Then
                    mIsHoliday = True
                Else
                    mIsHoliday = False
                End If
                mWorkingHours = GetWorkingHours(Trim(TxtEmpCode.Text), mCurrentDate, mIsRoundClock, mIsHoliday)

                If lblCategory.Text <> "E" Then
                    'If RsCompany.Fields("COMPANY_CODE").Value = 6 Then

                    'Else
                    If mWorkingHours = 0 And (mFHalf = PRESENT Or mSHalf = PRESENT) Then
                        If mAttnCheck = False Then
                            If MsgQuestion("Your Working Hours is Zero Hours, on Dated " & mCurrentDate & ", Want to Continue?") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            MsgInformation("Your Working Hours is Zero Hours, on Dated " & mCurrentDate & ", So Cann't be Select Present." & mCurrentDate)
                            FieldsVarification = False
                            Exit Function
                        End If
                    ElseIf mWorkingHours < 3 And (mFHalf = PRESENT Or mSHalf = PRESENT) Then
                        If mAttnCheck = False Then
                            If MsgQuestion("Your Working Hours is Less than 3 Hours, on Dated " & mCurrentDate & ", Want to Continue?") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            MsgInformation("Your Working Hours is Less than 3 Hours, on Dated " & mCurrentDate & ", So Cann't be Select Present." & mCurrentDate)
                            FieldsVarification = False
                            Exit Function
                        End If
                    ElseIf mWorkingHours >= 3 And mWorkingHours < 6 And mFHalf = PRESENT And mSHalf = PRESENT Then
                        If mAttnCheck = False Then
                            If MsgQuestion("Your Working Hours is Less than 6 Hours, on Dated " & mCurrentDate & ", So Cann't be Select both Present. Want to Continue?") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            MsgInformation("Your Working Hours is Less than 6 Hours, on Dated " & mCurrentDate & ", So Cann't be Select both Present." & mCurrentDate)
                            FieldsVarification = False
                            Exit Function
                        End If
                    ElseIf mShortLeave > 3 And mWorkingHours >= 6 And (mFHalf = PRESENT And mSHalf = PRESENT) Then
                        If mAttnCheck = False Then
                            If MsgQuestion("You Already used 3 Short Leave, on Dated " & mCurrentDate & ", So Cann't be Select both Present. Want to Continue?") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            MsgInformation("You Already used 3 Short Leave, on Dated " & mCurrentDate & ", So Cann't be Select both Present." & mCurrentDate)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    'End If
                End If
NextLine1:

                '            If lblCategory.Caption = "E" Then
                .Col = ColCPLEarn
                mCPLEarn = IIf(.Text = "", 0, VB.Left(.Text, 1))
                If lblCategory.Text = "E" Then
                    If mCPLEarn > 0 Then
                        If CheckOverTimeClaim(Trim(TxtEmpCode.Text), mCurrentDate) = True Then
                            MsgInformation("You already Claim Over Time, So cann't be Earn CPL. " & mCurrentDate)
                            FieldsVarification = False
                            Exit Function
                        End If
                        '                    If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Then
                        If (mFHalf = HOLIDAY Or mFHalf = SUNDAY) And (mSHalf = HOLIDAY Or mSHalf = SUNDAY) Then
                            If mWorkingHours <= mCPLEarn * 3 Then
                                MsgInformation("Working hours should be greater than CPL Earn Hours, So Cann't be Save. " & mCurrentDate)
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            If mWorkingHours - 8 <= mCPLEarn * 3 Then
                                MsgInformation("Extra Hours should be Greater than minimum CPL Earn Hours, So Cann't be Save. " & mCurrentDate)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                        '                    End If
                    End If
                End If
                mTotCPLEarn = mTotCPLEarn + (mCPLEarn / 2)
                '            End If

                If CheckLeaveAgtShortLeave(Trim(TxtEmpCode.Text), mCurrentDate) = True Then
                    If mFHalf = -1 And mSHalf = -1 Then
                        MsgInformation("You cann't be unselect Leave Against Short Leave. " & mCurrentDate)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
                If mCPLEarn > 0 Then
                    If mFHalf = CPLAVAIL Or mSHalf = CPLAVAIL Then
                        MsgInformation("You cann't be select both EARN and Avail CPL. " & mCurrentDate)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
                If mFHalf = CPLAVAIL Then
                    If VB6.Format(mCurrentDate, "YYYYMM") = VB6.Format(mCPLFH, "YYYYMM") Then
                        mTotCPLEarn = mTotCPLEarn - 0.5
                    End If
                End If

                If mSHalf = CPLAVAIL Then
                    If VB6.Format(mCurrentDate, "YYYYMM") = VB6.Format(mCPLSH, "YYYYMM") Then
                        mTotCPLEarn = mTotCPLEarn - 0.5
                    End If
                End If
            Next
        End With

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDate
                mCurrentDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColFH
                mFHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColSH
                mSHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColCPLFH
                mCPLFH = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColCPLSH
                mCPLSH = VB6.Format(.Text, "DD/MM/YYYY")

                If mFHalf = CPLAVAIL Then
                    If Trim(mCPLFH) = "" Or Not IsDate(mCPLFH) Then
                        MsgInformation("Please enter the CPL Avail Agt. Date [" & mCurrentDate & "] .")
                        FieldsVarification = False
                        Exit Function
                    End If
                    If CDate(mCPLFH) < CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Day, pCPLDays, CDate(mCurrentDate))) Then
                        MsgInformation("CPL Earn Date cann't be more than " & System.Math.Abs(pCPLDays) & " days [" & mCurrentDate & "] .")
                        FieldsVarification = False
                        Exit Function
                    End If
                    If VB6.Format(mCurrentDate, "YYYYMMDD") <= VB6.Format(mCPLFH, "YYYYMMDD") Then
                        If mCPLEarn < 0 Then
                            MsgInformation("CPL Cann't be Avail in Advance, So Cann't be Save [" & mCurrentDate & "] .")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    If VB6.Format(mCurrentDate, "YYYYMM") = VB6.Format(mCPLFH, "YYYYMM") Then
                        If mCPLEarn < 0 Then
                            MsgInformation("No Balance Earn Leave, So Cann't be Save [" & mCurrentDate & "] .")
                            FieldsVarification = False
                            Exit Function
                        Else
                            If CheckCurrentCPLEarnBalance(Trim(TxtEmpCode.Text), mCurrentDate, mCPLFH) < 0 Then
                                MsgInformation("No Balance CPL on such Date [" & mCurrentDate & "] .")
                                MainClass.SetFocusToCell(sprdMain, cntRow, ColCPLFH)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    Else
                        mCPLAvail = GetEarnAvail(mCPLFH)
                        If CheckCPLEarnBalance(Trim(TxtEmpCode.Text), mCurrentDate, mCPLFH, mCPLAvail) = False Then
                            MsgInformation("No Balance CPL on such Date [" & mCurrentDate & "] .")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If

                If mSHalf = CPLAVAIL Then
                    If Trim(mCPLSH) = "" Or Not IsDate(mCPLSH) Then
                        MsgInformation("Please enter the CPL Avail Agt. Date [" & mCurrentDate & "] .")
                        FieldsVarification = False
                        Exit Function
                    End If

                    If CDate(mCPLSH) < CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Day, pCPLDays, CDate(mCurrentDate))) Then
                        MsgInformation("CPL Earn Date cann't be more than " & System.Math.Abs(pCPLDays) & " days [" & mCurrentDate & "] .")
                        FieldsVarification = False
                        Exit Function
                    End If
                    If VB6.Format(mCurrentDate, "YYYYMMDD") <= VB6.Format(mCPLSH, "YYYYMMDD") Then
                        If mCPLEarn < 0 Then
                            MsgInformation("CPL Cann't be Avail in Advance, So Cann't be Save [" & mCurrentDate & "] .")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    If VB6.Format(mCurrentDate, "YYYYMM") = VB6.Format(mCPLSH, "YYYYMM") Then
                        If mTotCPLEarn < 0 Then
                            MsgInformation("No Balance CPL, So Cann't be Save [" & mCurrentDate & "] .")
                            FieldsVarification = False
                            Exit Function
                        Else
                            If CheckCurrentCPLEarnBalance(Trim(TxtEmpCode.Text), mCurrentDate, mCPLSH) < 0 Then
                                MsgInformation("No Balance CPL on such Date [" & mCurrentDate & "] .")
                                MainClass.SetFocusToCell(sprdMain, cntRow, ColCPLSH)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    Else
                        mCPLAvail = GetEarnAvail(mCPLSH)
                        If CheckCPLEarnBalance(Trim(TxtEmpCode.Text), mCurrentDate, mCPLSH, mCPLAvail) = False Then
                            MsgInformation("No Balance CPL on such Date [" & mCurrentDate & "] .")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If

                mTotalCPLAvail = 0
                .Col = ColCPLEarn
                mCheckCPLEarn = Val(IIf(Trim(VB.Left(.Text, 1)) = "", 0, Trim(VB.Left(.Text, 1)))) * 0.5
                mGetCPLAvailDate = GetCPLAvailDate(Trim(TxtEmpCode.Text), mCurrentDate, mTotalCPLAvail) ''GetCPLAvailDate(mCurrentDate)
                If mCheckCPLEarn = 0 And mTotalCPLAvail > 0 Then
                    MsgInformation("CPL EARN as on Date " & mCurrentDate & " You already Avail in Date " & mGetCPLAvailDate & ", So cann't be change.")
                    FieldsVarification = False
                    Exit Function
                End If
                If mCheckCPLEarn = 0.5 And mTotalCPLAvail > 0.5 Then
                    MsgInformation("CPL EARN as on Date " & mCurrentDate & " You already Avail in Date " & mGetCPLAvailDate & ", So cann't be change.")
                    FieldsVarification = False
                    Exit Function
                End If
            Next
        End With


        '    If RsCompany.Fields("COMPANY_CODE").Value = 15 Then
        '        mMonthStartDate = "01/" & vb6.Format(txtRefDate.Text, "MM/YYYY")
        '        mOpCPL = CalcOPLeaves(Trim(txtEmpCode.Text), mMonthStartDate)
        '        With SprdMain
        '            For cntRow = 1 To .MaxRows
        '                .Row = cntRow
        '                .Col = ColDate
        '                mCurrentDate = Format(.Text, "DD/MM/YYYY")
        '
        '                .Col = ColFH
        '                mFHalf = IIf(.Text = "", -1, Left(.Text, 1))
        '
        '                .Col = ColSH
        '                mSHalf = IIf(.Text = "", -1, Left(.Text, 1))
        '
        '                mCPLAvail = 0
        '                If mFHalf = CPLEARN Then
        '                    mOpCPL = mOpCPL + 0.5
        '                    mCPLEarn = mCPLEarn + 0.5
        '                ElseIf mFHalf = CPLAVAIL Then
        '                    mOpCPL = mOpCPL - 0.5
        '                    mCPLAvail = mCPLAvail + 0.5
        '                End If
        '
        '                If mSHalf = CPLEARN Then
        '                    mOpCPL = mOpCPL + 0.5
        '                    mCPLEarn = mCPLEarn + 0.5
        '                ElseIf mSHalf = CPLAVAIL Then
        '                    mOpCPL = mOpCPL - 0.5
        '                    mCPLAvail = mCPLAvail + 0.5
        '                End If
        '
        '                If mCPLAvail > 0 Then
        '                    If mOpCPL < 0 Then
        '                        MsgInformation "You have Not Enough CPL, so Cann't be avail CPL."
        '                        FieldsVarification = False
        '        '                Exit For
        '                        Exit Function
        '                    Else
        '                        mBalCPLEarn = mCPLEarn + CountCPLLeaves(Trim(txtEmpCode.Text), mCurrentDate, mMonthStartDate)
        '                        If mBalCPLEarn <= 0 Then
        '                            MsgInformation "You have Not Enough CPL Earn with in 120 Days."
        '                            FieldsVarification = False
        '            '                Exit For
        '                            Exit Function
        '                        End If
        '                    End If
        '                End If
        '            Next
        '        End With
        '    End If
        '

        If MODIFYMode = True And RsEmpLeave.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function GetWorkingHours(ByRef mEmpCode As String, ByRef mDate As String, ByRef mIsRound As String, ByRef mIsHoliday As Boolean) As Double

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mHours As Integer
        Dim mMin As Integer
        Dim mTOTHours As String
        Dim mInTime As String
        Dim mOutTime As String
        Dim mWorkingHours As Double
        Dim mEmpInTime As String
        Dim mEmpOutTime As String

        Dim mEmpODFrom As String
        Dim mEmpODTo As String
        Dim mInTrue As Boolean

        Dim mMarginsMinute As Double
        Dim mIsRoundClock As String
        Dim mEmpShiftIN As String
        Dim mEmpShiftOUT As String
        Dim mEmpShiftBreak As String
        'Dim mISHoliday As Boolean

        GetWorkingHours = 0
        mInTrue = False

        mMarginsMinute = IIf(IsDbNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mIsRoundClock = IIf(GetRoundClock(mEmpCode, mDate, "E") = True, "Y", "N")

        mEmpShiftIN = GetShiftTimeNew(mEmpCode, mDate, 0, "I", mIsRoundClock, "E")
        mEmpShiftOUT = GetShiftTimeNew(mEmpCode, mDate, 0, "O", mIsRoundClock, "E")

        mEmpInTime = VB6.Format("00:00", "HH:MM")
        mEmpOutTime = VB6.Format("00:00", "HH:MM")

        SqlStr = " SELECT IN_TIME, OUT_TIME, WORKS_HOURS+OT_HOURS AS WORKS_HOURS FROM PAY_DALIY_ATTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetWorkingHours = IIf(IsDbNull(RsTemp.Fields("WORKS_HOURS").Value), 0, RsTemp.Fields("WORKS_HOURS").Value)
            '        If GetWorkingHours <= 0 Then GoTo NextRecd

            If GetWorkingHours > 3 - 0.08 Then
                GetWorkingHours = GetWorkingHours + 0.08
            End If

            mEmpInTime = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
            mEmpOutTime = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:MM")
            mEmpInTime = VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(CDate(mEmpInTime)), Minute(CDate(mEmpInTime)), 0), "DD/MM/YYYY HH:MM")

            If mIsRound = "Y" Then
                mEmpOutTime = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
            Else
                mEmpOutTime = VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
            End If
            If VB6.Format(mEmpInTime, "HH:MM") = "00:00" And VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then

            Else
                mInTrue = True
                If GetWorkingHours >= 8 Then
                    If lblCategory.Text <> "E" Then ''13/12/2018
                        Exit Function
                    End If
                End If
            End If
        End If

NextRecd:

        SqlStr = " SELECT  MIN(TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI')) AS TIME_FROM, MAX(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(REF_DATE,'YYYYMMDD')='" & VB6.Format(mDate, "YYYYMMDD") & "'" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "' AND MOVE_TYPE IN ('O','M') "

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " HAVING COUNT(1)>0 " ''GROUP BY TOTAL_HRS "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        '    If mInTrue = False And mMoveType <> "P" Then
        '            mWorkingHours = DateDiff("n", mEmpODFrom, mEmpODTo)
        '        Else

        Dim xMissTime As String
        If RsTemp.EOF = False Then

            mEmpODFrom = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "DD/MM/YYYY HH:MM")
            mEmpODTo = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "DD/MM/YYYY HH:MM")

            If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Or Trim(mEmpInTime) = "" Then
                mEmpInTime = mEmpODFrom
            Else
                If CDate(VB6.Format(mEmpInTime, "DD/MM/YYYY HH:MM")) > CDate(VB6.Format(mEmpODFrom, "DD/MM/YYYY HH:MM")) Then
                    xMissTime = mEmpInTime
                    mEmpInTime = mEmpODFrom
                End If
            End If

            If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Or Trim(mEmpOutTime) = "" Then
                If xMissTime = "" Then
                    mEmpOutTime = mEmpODTo
                Else
                    If CDate(VB6.Format(xMissTime, "DD/MM/YYYY HH:MM")) > CDate(VB6.Format(mEmpODTo, "DD/MM/YYYY HH:MM")) Then
                        mEmpOutTime = xMissTime
                    Else
                        mEmpOutTime = mEmpODTo
                    End If
                End If
            Else
                If CDate(VB6.Format(mEmpOutTime, "DD/MM/YYYY HH:MM")) < CDate(VB6.Format(mEmpODTo, "DD/MM/YYYY HH:MM")) Then
                    mEmpOutTime = mEmpODTo
                End If
            End If
        End If

        If lblCategory.Text <> "E" Then
            SqlStr = " SELECT MIN(TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI')) AS TIME_FROM, MAX(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(REF_DATE,'YYYYMMDD')='" & VB6.Format(mDate, "YYYYMMDD") & "'" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "' AND MOVE_TYPE IN ('P') "

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            SqlStr = SqlStr & vbCrLf & " HAVING COUNT(1)>0 " ''GROUP BY TOTAL_HRS "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


            If RsTemp.EOF = False Then

                mEmpODFrom = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "DD/MM/YYYY HH:MM")
                mEmpODTo = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "DD/MM/YYYY HH:MM")

                If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Or Trim(mEmpInTime) = "" Then
                    mEmpInTime = mEmpODTo
                End If

                If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Or Trim(mEmpOutTime) = "" Then
                    mEmpOutTime = mEmpODFrom
                End If
            End If
        End If

        '    mEmpInTime = IIf(CDate(mEmpInTime) <= CDate(mEmpShiftIN), mEmpShiftIN, mEmpInTime)
        '    mEmpOutTime = IIf(CDate(mEmpOutTime) >= CDate(mEmpShiftOUT), mEmpShiftOUT, mEmpOutTime)

        mIsHoliday = GetIsHolidays(VB6.Format(mDate, "DD/MM/YYYY"), "", mEmpCode, "", "Y")

        mWorkingHours = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mEmpInTime), CDate(mEmpOutTime))

        mWorkingHours = IIf(mWorkingHours > 0, mWorkingHours, 0)

        mWorkingHours = mWorkingHours + IIf(mWorkingHours > 0, mMarginsMinute, 0)

        mHours = Int(mWorkingHours / 60)
        mMin = mWorkingHours - (mHours * 60) - IIf(mIsHoliday = True, 0, 30)
        GetWorkingHours = (mHours + (mMin / 60))

        Exit Function
ErrPart:
        '    Resume
        GetWorkingHours = 0
    End Function

    Private Function CheckShortLeave(ByRef mEmpCode As String, ByRef mDate As String) As Boolean

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        CheckShortLeave = False

        SqlStr = " SELECT * FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(REF_DATE,'YYYYMMDD')='" & VB6.Format(mDate, "YYYYMMDD") & "'" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "' AND MOVE_TYPE ='P' AND AGT_LEAVE='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckShortLeave = True
        End If

        Exit Function
ErrPart:
        CheckShortLeave = False
    End Function
    Private Function CheckOverTimeClaim(ByRef mEmpCode As String, ByRef mDate As String) As Boolean

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckOverTimeClaim = False

        SqlStr = "SELECT OTHOUR+OTMIN AS OTHOUR" & vbCrLf & " FROM PAY_OVERTIME_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND OT_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IIf(IsDbNull(RsTemp.Fields("OTHOUR").Value), 0, RsTemp.Fields("OTHOUR").Value) > 0 Then
                CheckOverTimeClaim = True
            End If
            Exit Function
        End If

        Exit Function
ErrPart:
        CheckOverTimeClaim = False
    End Function

    Private Function GetEarnAvail(ByRef pDate As String) As Double
        On Error GoTo ERR1
        Dim cntRow As Integer

        Dim mFHalf As Integer
        Dim mSHalf As Integer

        Dim mCPLFH As String
        Dim mCPLSH As String


        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColFH
                mFHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColSH
                mSHalf = IIf(.Text = "", -1, Val(VB.Left(.Text, 2)))

                .Col = ColCPLFH
                mCPLFH = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColCPLSH
                mCPLSH = VB6.Format(.Text, "DD/MM/YYYY")

                If mFHalf = CPLAVAIL Then
                    If VB6.Format(pDate, "YYYYMMDD") = VB6.Format(mCPLFH, "YYYYMMDD") Then
                        GetEarnAvail = GetEarnAvail + 0.5
                    End If
                End If

                If mSHalf = CPLAVAIL Then
                    If VB6.Format(pDate, "YYYYMMDD") = VB6.Format(mCPLSH, "YYYYMMDD") Then
                        GetEarnAvail = GetEarnAvail + 0.5
                    End If
                End If
            Next
        End With

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CalcOPLeaves(ByRef mCode As String, ByRef mMonthStartDate As String) As Double

        On Error GoTo ErrFillLeaves
        Dim RsLeaves As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mShowOpening As Boolean
        Dim mStartDate As String


        CalcOPLeaves = 0
        mStartDate = VB6.Format("01/01/" & Year(CDate(mMonthStartDate)), "DD/MM/YYYY")
        mShowOpening = True
        '    If RsCompany.Fields("COMPANY_CODE").Value = 15 And Year(mMonthStartDate) > 2012 Then
        '        mShowOpening = False
        '        mStartDate = DateAdd("d", -120, mStartDate) '' "01/09/2012"
        '    End If

        If RsCompany.Fields("COMPANY_CODE").Value = 15 And CDate(mMonthStartDate) >= CDate("01/09/2012") Then
            mShowOpening = False
            mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -120, CDate(mMonthStartDate))) ''"01/09/2012"
            If CDate(mStartDate) < CDate("01/09/2012") Then
                mStartDate = "01/09/2012"
            End If
        End If

        If mShowOpening = True Then
            SqlStr = " SELECT SUM(NVL(OPENING,0)) AS OPENING " & vbCrLf & " FROM PAY_OPLEAVE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR =" & Year(CDate(mMonthStartDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "' AND  LEAVECODE=" & CPLEARN & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)

            If RsLeaves.EOF = False Then
                CalcOPLeaves = IIf(IsDbNull(RsLeaves.Fields("OPENING").Value), 0, RsLeaves.Fields("OPENING").Value)
            End If
        End If

        ''AND PAYYEAR =" & Year(mMonthStartDate) & "

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<'" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            Do While Not RsLeaves.EOF
                CalcOPLeaves = CalcOPLeaves + (IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) * 0.5)
                '            If RsLeaves!FIRSTHALF = CPLEARN Then
                '                CalcOPLeaves = CalcOPLeaves + 0.5
                ''                MsgBox RsLeaves!ATTN_DATE
                '            Else
                If RsLeaves.Fields("FIRSTHALF").Value = CPLAVAIL Then
                    CalcOPLeaves = CalcOPLeaves - 0.5
                End If

                '            If RsLeaves!SECONDHALF = CPLEARN Then
                '                CalcOPLeaves = CalcOPLeaves + 0.5
                '            Else
                If RsLeaves.Fields("SECONDHALF").Value = CPLAVAIL Then
                    CalcOPLeaves = CalcOPLeaves - 0.5
                End If

                RsLeaves.MoveNext()
            Loop
        End If
        Exit Function
ErrFillLeaves:
        CalcOPLeaves = 0
    End Function
    Private Function CountCPLLeaves(ByRef mCode As String, ByRef xDate As String, ByRef mMonthStartDate As String) As Double

        On Error GoTo ErrFillLeaves
        Dim RsLeaves As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mCPLCountFrom As String
        Dim mYearStartDate As String

        mCPLCountFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, pCPLDays, CDate(xDate)))

        CountCPLLeaves = 0

        If RsCompany.Fields("COMPANY_CODE").Value = 15 And Year(CDate(xDate)) = 2012 Then
            If CDate(mCPLCountFrom) < CDate("01/09/2012") Then
                mCPLCountFrom = "01/09/2012"
            End If
        End If

        SqlStr = " SELECT SUM(CPL_EARN) AS CPL_EARN " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "' AND CPL_EARN>0" & vbCrLf & " AND ATTN_DATE>='" & VB6.Format(mCPLCountFrom, "DD-MMM-YYYY") & "'" & vbCrLf & " AND ATTN_DATE<'" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
        If RsLeaves.EOF = False Then
            CountCPLLeaves = IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) * 0.5
        End If

        '    SqlStr = " SELECT COUNT(1) AS CNTCPL " & vbCrLf _
        ''            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP_CODE='" & mCode & "' AND SECONDHALF = " & CPLEARN & "" & vbCrLf _
        ''            & " AND ATTN_DATE>='" & VB6.Format(mCPLCountFrom, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''            & " AND ATTN_DATE<'" & VB6.Format(mMonthStartDate, "DD-MMM-YYYY") & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsLeaves, adLockOptimistic
        '    If RsLeaves.EOF = False Then
        '        CountCPLLeaves = CountCPLLeaves + IIf(IsNull(RsLeaves!CNTCPL), 0, RsLeaves!CNTCPL)
        '    End If

        '    CountCPLLeaves = CountCPLLeaves / 2

        Exit Function
ErrFillLeaves:
        CountCPLLeaves = 0
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtRefDate.MaxLength = 10
        TxtEmpCode.Maxlength = RsEmpLeave.Fields("EMP_CODE").DefinedSize
        TxtEmpName.Maxlength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)
        txtDept.Maxlength = MainClass.SetMaxLength("EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn)
        '    txtPlace.MaxLength = RsEmpLeave.Fields("PLACE_VISIT").DefinedSize

        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '' Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = " SELECT TRN.ATTN_DATE, TRN.EMP_CODE, EMP.EMP_NAME, FIRSTHALF, SECONDHALF,AGT_LATE" & vbCrLf & " FROM PAY_ATTN_MST TRN, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE TRN.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE = EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE = EMP.EMP_CODE" & vbCrLf & " AND TRN.PAYYEAR = " & PubPAYYEAR & " "

        If lblCategory.Text = "W" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='2'"
        ElseIf lblCategory.Text = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='1'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND CPL_EARN>0"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.ATTN_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 8)
            .set_ColWidth(3, 25)
            .set_ColWidth(4, 6)
            .set_ColWidth(5, 6)
            '        .ColWidth(6) = 8
            '        .ColWidth(7) = 8

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Dim SqlStr As String = ""

        SqlStr = ""
        '     If IsFieldExist = True Then Delete1 = False: Exit Function

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_ATTN_MST", (txtRefDate.Text), RsEmpLeave) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_ATTN_MST", "EMP_CODE || ':' || TO_CHAR(ATTN_DATE,'YYYYMM')", TxtEmpCode.Text & ":" & VB6.Format(txtRefDate.Text, "YYYYMM")) = False Then GoTo DeleteErr

        SqlStr = " DELETE " & vbCrLf & " FROM PAY_ATTN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE=" & MainClass.AllowSingleQuote((TxtEmpCode.Text)) & "" & vbCrLf & " AND TO_CHAR(ATTN_DATE,'YYYYMM')=" & VB6.Format(txtRefDate.Text, "YYYYMM") & ""

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsEmpLeave.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsEmpLeave.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String

        Exit Sub
        Report1.Reset()
        mTitle = "EMPLOYEE MOVEMENT SLIP"
        mSubTitle = "From : " & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY") & " TO : " & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PAYMOVEMENT.rpt"
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub



    Private Sub TxtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Public Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub


        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")

        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "' "

        If lblCategory.Text = "W" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='2'"
        ElseIf lblCategory.Text = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='1'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        If RS.EOF = False Then
            txtEmpCode.Text = RS.Fields("EMP_CODE").Value
            TxtEmpName.Text = IIf(IsDBNull(RS.Fields("EMP_NAME").Value), "", RS.Fields("EMP_NAME").Value)
            txtDept.Text = IIf(IsDBNull(RS.Fields("EMP_DEPT_CODE").Value), "", RS.Fields("EMP_DEPT_CODE").Value)
        Else
            MsgBox("This Employee Code does not exsits in Master.", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtRefDate.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsEmpLeave.EOF = False Then xEmpCode = RsEmpLeave.Fields("EMP_CODE").Value

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & txtEmpCode.Text & "'" & vbCrLf & " AND TO_CHAR(ATTN_DATE,'YYYYMM')='" & VB6.Format(txtRefDate.Text, "YYYYMM") & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpLeave, ADODB.LockTypeEnum.adLockReadOnly)
        If RsEmpLeave.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                MainClass.UOpenRecordSet("Select * From PAY_ATTN_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "' AND TO_CHAR(ATTN_DATE,'YYYYMM')='" & VB6.Format(txtRefDate.Text, "YYYY") & "'", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpLeave, ADODB.LockTypeEnum.adLockReadOnly)
            End If
            Call FillLeaves((txtEmpCode.Text))
        End If


        GoTo EventExitSub
ERR1:

        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtEmpName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
    Private Sub txtAthCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAthCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        If Trim(txtAthCode.Text) = "" Then GoTo EventExitSub

        txtAthCode.Text = VB6.Format(txtAthCode.Text, "000000")

        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtAthCode.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        If RS.EOF = False Then
            txtAthCode.Text = RS.Fields("EMP_CODE").Value
        Else
            MsgBox("This Employee Code does not exsits in Master.", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:

        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAthCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAthCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAthCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAthCode.DoubleClick
        Call cmdAthSearch_Click(cmdAthSearch, New System.EventArgs())
    End Sub
    Private Sub cmdAthSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAthSearch.Click
        Dim SqlStr As String = ""

        SqlStr = ""

        If MainClass.SearchGridMaster((txtAthCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAthCode.Text = AcName1
            txtAthCode_Validating(txtAthCode, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
    End Sub
    Private Sub txtAthCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAthCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAthCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAthCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAthCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdAthSearch_Click(cmdAthSearch, New System.EventArgs())
    End Sub

End Class
