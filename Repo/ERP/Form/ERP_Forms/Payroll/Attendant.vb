Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAttendanceReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim RsEmp As ADODB.Recordset = Nothing
    Dim RsAttn As ADODB.Recordset = Nothing
    Dim cntRow As Integer
    Dim mCode As String
    Dim mWopay As Double
    Dim mLeave As Double
    Dim ecntRow As Integer
    Dim mMonth As Short
    Dim mYear As Short
    Dim mThisMonAttn As Double
    Dim mJDays As Integer
    Dim mLDays As Integer
    Dim LastDateofMon As String
    Dim mCurCol As Integer
    Dim mCurRow As Integer

    Private Const ColEmpCode As Short = 1
    Private Const ColEmpName As Short = 2
    Private Const ColEmpFather As Short = 3
    Private Const ColDesg As Short = 4
    Private Const ColDay1 As Short = 5
    Private Const ColDay2 As Short = 6
    Private Const ColDay3 As Short = 7
    Private Const ColDay4 As Short = 8
    Private Const ColDay5 As Short = 9
    Private Const ColDay6 As Short = 10
    Private Const ColDay7 As Short = 11
    Private Const ColDay8 As Short = 12
    Private Const ColDay9 As Short = 13
    Private Const ColDay10 As Short = 14
    Private Const ColDay11 As Short = 15
    Private Const ColDay12 As Short = 16
    Private Const ColDay13 As Short = 17
    Private Const ColDay14 As Short = 18
    Private Const ColDay15 As Short = 19
    Private Const ColDay16 As Short = 20
    Private Const ColDay17 As Short = 21
    Private Const ColDay18 As Short = 22
    Private Const ColDay19 As Short = 23
    Private Const ColDay20 As Short = 24
    Private Const ColDay21 As Short = 25
    Private Const ColDay22 As Short = 26
    Private Const ColDay23 As Short = 27
    Private Const ColDay24 As Short = 28
    Private Const ColDay25 As Short = 29
    Private Const ColDay26 As Short = 30
    Private Const ColDay27 As Short = 31
    Private Const ColDay28 As Short = 32
    Private Const ColDay29 As Short = 33
    Private Const ColDay30 As Short = 34
    Private Const ColDay31 As Short = 35
    Private Const ColTotPresent As Short = 36
    Private Const ColTotWFH As Short = 37
    Private Const ColHoliday As Short = 38
    Private Const ColABSENT As Short = 39
    Private Const ColCL As Short = 40
    Private Const ColSL As Short = 41
    Private Const ColEL As Short = 42
    Private Const ColWOPAY As Short = 43
    Private Const ColLayOff As Short = 44
    Private Const ColRemarks As Short = 45
    Private Const ColRemarks1 As Short = 46
    Private Const ColAddEmpCode As Short = 47


    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub ANextRow1()

        mJDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, RsEmp.Fields("DOJ").Value, CDate(VB6.Format(LastDateofMon, "dd/mm/yyyy")))
        mThisMonAttn = mJDays
        If Not IsDbNull(RsEmp.Fields("DOL").Value) Then
            mLDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, RsEmp.Fields("DOL").Value, CDate(VB6.Format(LastDateofMon, "dd/mm/yyyy")))
        End If
        If VB6.Format(RsEmp.Fields("DOJ").Value, "mm yyyy") = VB6.Format(RsEmp.Fields("DOL").Value, "mm yyyy") Then
            mThisMonAttn = mJDays - mLDays + 1
        ElseIf VB6.Format(RsEmp.Fields("DOJ").Value, "mm yyyy") = VB6.Format(LastDateofMon, "mm yyyy") Then
            mThisMonAttn = mJDays + 1
        ElseIf VB6.Format(RsEmp.Fields("DOL").Value, "mm yyyy") = VB6.Format(LastDateofMon, "mm yyyy") Then
            mThisMonAttn = MainClass.LastDay(mMonth, mYear) - mLDays
        End If

        If MainClass.LastDay(mMonth, mYear) < mThisMonAttn Then
            mThisMonAttn = MainClass.LastDay(mMonth, mYear)
        End If

        If RsEmp.EOF Then Call ANextRow2()
        If mCode <> RsEmp.Fields("Code").Value Then
            Call ANextRow2()
        End If
    End Sub


    Private Sub ANextRow2()
        sprdAttn.Col = sprdAttn.MaxCols - 2
        sprdAttn.Text = CStr(mLeave)

        sprdAttn.Col = sprdAttn.MaxCols - 1
        sprdAttn.Text = CStr(mWopay)

        sprdAttn.Col = sprdAttn.MaxCols
        sprdAttn.Text = CStr(mThisMonAttn - (mLeave + mWopay))

        mWopay = 0
        mLeave = 0
        cntRow = cntRow + 1

    End Sub

    Private Sub FillHeading(ByVal xDate As Date)

        Dim Daysinmonth As Integer
        Dim cntCol As Integer
        Dim Tempdate As String
        Dim mDay As Integer
        Dim NewDate As Date

        MainClass.ClearGrid(sprdAttn)
        'xDate = lblYear.Text
        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))

        If CDate(NewDate) < CDate("01/08/2012") Then
            If ConAttnDataFromMC = True Then
                optShow(0).Enabled = True
                optShow(1).Enabled = True
                optShow(0).Checked = True
            Else
                optShow(1).Enabled = False
                optShow(0).Checked = True
            End If
        ElseIf CDate(NewDate) < CDate("01/02/2013") And RsCompany.Fields("COMPANY_CODE").Value = 15 Then
            optShow(0).Enabled = True
            optShow(1).Enabled = True
            optShow(0).Checked = True
        Else
            If ConAttnDataFromMC = True Then
                optShow(0).Enabled = False
                optShow(1).Checked = True
            Else
                optShow(1).Enabled = False
                optShow(0).Checked = True
            End If
        End If

        With sprdAttn
            .MaxCols = ColAddEmpCode

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Row = -1
            For cntCol = ColTotPresent To ColLayOff
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 1
            Next

            .Row = 0
            .Col = 0
            .Text = "S. No."
            .set_ColWidth(0, 5)

            .Col = ColEmpCode
            .Text = "Emp Card No"
            .set_ColWidth(ColEmpCode, 7)


            .Col = ColEmpName
            .Text = "Employees' Name "
            .set_ColWidth(ColEmpName, 25)
            .ColsFrozen = 2

            .Col = ColEmpFather
            .Text = "Father's Name"
            .set_ColWidth(ColEmpFather, 7)
            .ColHidden = True

            .Col = ColDesg
            .Text = "Designation"
            .set_ColWidth(ColDesg, 7)
            .ColHidden = True

            mDay = 1
            For cntCol = ColDay1 To ColDay31
                .Col = cntCol
                If mDay <= Daysinmonth Then
                    .Text = VB6.Format(VB.Day(NewDate), "00") & vbNewLine & WeekdayName(Weekday(NewDate, FirstDayOfWeek.Monday), False, FirstDayOfWeek.Monday)
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    NewDate = System.DateTime.FromOADate(NewDate.ToOADate + 1)
                Else
                    .Text = " "
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                End If
                mDay = mDay + 1
            Next

            .Col = ColTotPresent
            .Text = "Total Present"
            .set_ColWidth(ColTotPresent, 7)

            .Col = ColTotWFH
            .Text = "Total WFH"
            .set_ColWidth(ColTotWFH, 7)

            .Col = ColHoliday
            .Text = "Holiday"
            .set_ColWidth(ColHoliday, 7)

            .Col = ColABSENT
            .Text = "Absent"
            .set_ColWidth(ColABSENT, 7)

            .Col = ColCL
            .Text = "CL"
            .set_ColWidth(ColCL, 7)

            .Col = ColSL
            .Text = "SL"
            .set_ColWidth(ColSL, 7)

            .Col = ColEL
            .Text = "EL"
            .set_ColWidth(ColEL, 7)

            .Col = ColRemarks
            .Text = "Remarks"
            .set_ColWidth(ColRemarks, 7)

            .Col = ColRemarks1
            .Text = "Remarks 1"
            .set_ColWidth(ColRemarks1, 7)

            .Col = ColAddEmpCode
            .Text = "Add. Emp Code"
            .set_ColWidth(ColAddEmpCode, 9)

            .Col = ColWOPAY
            .Text = "W/o Pay"
            .set_ColWidth(ColWOPAY, 7)

            .Col = ColLayOff
            .Text = "Lay-Off"
            .set_ColWidth(ColLayOff, 7)

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)
            MainClass.SetSpreadColor(sprdAttn, -1)
        End With
    End Sub


    Private Sub cboCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.SelectedIndexChanged

        MainClass.ClearGrid(sprdAttn, -1)
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged

        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
        MainClass.ClearGrid(sprdAttn, -1)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
        MainClass.ClearGrid(sprdAttn, -1)
        '    Call PrintCommand(False)
    End Sub

    Private Sub cmdAllEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAllEmp.Click
        If sprdAttn.ActiveCol < 3 Or sprdAttn.ActiveCol > sprdAttn.MaxCols - 9 Then Exit Sub

        sprdAttn.Row = sprdAttn.ActiveRow
        sprdAttn.Col = 1
        If Trim(sprdAttn.Text) = "" Then Exit Sub
        frmAttnHead.lblCode.Text = sprdAttn.Text

        frmAttnHead.lblEmpName.Text = "All Employees' Today Attendance"

        sprdAttn.Row = 0
        sprdAttn.Col = sprdAttn.ActiveCol

        frmAttnHead.lblDate.Text = Mid(LTrim(sprdAttn.Text), 1, 2) & " " & lblYear.Text
        frmAttnHead.lblType.Text = CStr(2)
        frmAttnHead.ShowDialog()
        RefreshScreen()
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdEmpAttn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmpAttn.Click

        sprdAttn.Row = sprdAttn.ActiveRow
        sprdAttn.Col = 1
        If Trim(sprdAttn.Text) = "" Then Exit Sub
        frmEmpAttn.lblEmpCode.Text = sprdAttn.Text

        sprdAttn.Col = 2
        frmEmpAttn.lblEmpName.Text = sprdAttn.Text
        frmEmpAttn.lblDate.Text = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")
        frmEmpAttn.ShowDialog()

        RefreshScreen()
    End Sub

    Private Sub cmdLeave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLeave.Click

        Dim mLastDay As Integer

        sprdAttn.Row = sprdAttn.ActiveRow
        sprdAttn.Col = 1
        If Trim(sprdAttn.Text) = "" Then Exit Sub
        frmLeave.lblCode.Text = sprdAttn.Text

        sprdAttn.Col = 2
        frmLeave.lblEmpName.Text = sprdAttn.Text

        sprdAttn.Row = 0
        sprdAttn.Col = sprdAttn.ActiveCol
        frmLeave.lblvwMonth.Text = VB6.Format(lblRunDate.Text, "MMMM , yyyy")
        frmLeave.lblMonth.Text = CStr(Month(CDate(lblRunDate.Text)))

        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        frmLeave.lblDate.Text = VB6.Format(mLastDay, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        frmLeave.lblYear.Text = IIf(Month(CDate(lblRunDate.Text)) < 4, Year(CDate(lblRunDate.Text)) - 1, Year(CDate(lblRunDate.Text)))
        frmLeave.ShowDialog()
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

        'Insert Data from Grid to PrintDummyData Table...

        Call MainClass.ClearCRptFormulas(Report1)

        '    If FillTempPrintDummyData(1, sprdAttn.MaxRows, 0, sprdAttn.MaxCols) = False Then GoTo ERR1

        SqlStr = MainClass.FillPrintDummyDataFromSprd(sprdAttn, 1, sprdAttn.MaxRows, 1, sprdAttn.MaxCols, PubDBCn)
        mTitle = "Attendance Register"
        mSubTitle = "Month of : " & lblYear.Text
        mRptFileName = "AttnReg.Rpt"
        'Select Record for print...

        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Public Function FillTempPrintDummyData(ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""
        Dim mLastDay As Integer
        Dim mGetDataStr As String
        Dim mBlankCol As Integer
        Dim mCheckDate As String
        Dim mHType As String
        Dim mEmpCode As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        mLastDay = Val(MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))))

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            sprdAttn.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                sprdAttn.Col = 1
                mEmpCode = Trim(sprdAttn.Text)
                sprdAttn.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(sprdAttn.Text) & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    If RowNum = 0 Then
                        If FieldNum > 2 And FieldNum <= mLastDay + 2 Then
                            GetData = GetData & ", " & "'" & VB.Left(Trim(sprdAttn.Text), 2) & "'"
                        Else
                            If FieldNum <= 2 Then
                                GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(sprdAttn.Text) & "'"
                            Else
                                If mLastDay < 31 And FieldNum = mLastDay + 3 Then
                                    For mBlankCol = 1 To 31 - mLastDay
                                        GetData = GetData & ", " & "''"
                                        FieldCnt = FieldCnt + 1
                                        SetData = SetData & ", " & "FIELD" & FieldCnt
                                    Next
                                End If

                                GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(sprdAttn.Text) & "'"

                            End If
                        End If
                    Else
                        '                    If FieldNum > 2 And FieldNum <= mLastDay + 2 Then
                        '                        mCheckDate = (FieldNum - 2) & "/" & Month(lblRunDate.Caption) & "/" & Year(lblRunDate.Caption)
                        '                        If ChechJoinLeaveDate(mCheckDate, mEmpCode, "N") = False Then
                        '                            mGetDataStr = ""
                        '                        Else
                        '                            If Trim(sprdAttn.Text) = "" Then
                        '                                If GetIsHolidays(mCheckDate, mHType) = True Then
                        '                                    mGetDataStr = mHType        ''"H, H"
                        '                                Else
                        '                                    mGetDataStr = "P, P"
                        '                                End If
                        '                            ElseIf Len(sprdAttn.Text) = 1 Then
                        '                                mGetDataStr = Trim(sprdAttn.Text) & ", P"
                        '                            ElseIf Len(sprdAttn.Text) = 3 Then
                        '                                mGetDataStr = "P" & Trim(sprdAttn.Text)
                        '                            Else
                        '                                mGetDataStr = Trim(sprdAttn.Text)
                        '                            End If
                        '                        End If
                        '                        GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(mGetDataStr) & "'"
                        '                    Else
                        If FieldNum <= 2 Then
                            mGetDataStr = Trim(sprdAttn.Text)
                            GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(mGetDataStr) & "'"
                        Else
                            If mLastDay < 31 And FieldNum = mLastDay + 3 Then
                                For mBlankCol = 1 To 31 - mLastDay
                                    mGetDataStr = ""
                                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(mGetDataStr) & "'"

                                    FieldCnt = FieldCnt + 1
                                    SetData = SetData & ", " & "FIELD" & FieldCnt
                                Next
                            End If
                            mGetDataStr = Trim(sprdAttn.Text)
                            GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(mGetDataStr) & "'"
                        End If
                        '                    End If

                    End If
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
NextRec:
        Next

        PubDBCn.CommitTrans()
        FillTempPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillTempPrintDummyData = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mHoliday As Double

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        '    mHoliday = GetMonthHolidays(lblRunDate.Caption)
        '    MainClass.AssignCRptFormulas Report1, "Holiday=""" & mHoliday & """"

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtEmpCode.Text) = "" Then
                MsgInformation("Please Select Operator Code")
                TxtEmpCode.Focus()
                Exit Sub
            End If

            TxtEmpCode.Text = VB6.Format(TxtEmpCode.Text, "000000")
            If MainClass.ValidateWithMasterTable((TxtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Employee Code ")
                TxtEmpCode.Focus()
                Exit Sub
            End If
        End If

        FillHeading(CDate(lblRunDate.Text))
        RefreshScreen()
    End Sub


    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((TxtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub frmAttendanceReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
    End Sub

    Private Sub frmAttendanceReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        FillHeading(CDate(lblRunDate.Text))
        'UpDYear.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(lblYear.Height) + 15)
        optCardNo.Checked = True
        FillDeptCombo()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        cboCategory.Enabled = False

        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        TxtEmpCode.Enabled = False
        cmdSearch.Enabled = False


        If ConAttnDataFromMC = True Then
            cmdEmpAttn.Enabled = False
            cmdAllEmp.Enabled = False
            optShow(0).Enabled = False
            optShow(1).Checked = True
        Else
            optShow(1).Enabled = False
            optShow(0).Checked = True
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub frmAttendanceReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    Private Sub sprdAttn_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles sprdAttn.DblClick
        'Dim mDays As String
        'Dim mCode As String

        'If eventArgs.col < 3 Or eventArgs.col > sprdAttn.MaxCols - 8 Then Exit Sub

        'sprdAttn.Row = eventArgs.row
        'sprdAttn.Col = 1
        'If Trim(sprdAttn.Text) = "" Then Exit Sub
        'frmAttnHead.lblCode.Text = sprdAttn.Text
        'mCode = Trim(sprdAttn.Text)

        'sprdAttn.Col = 2
        'frmAttnHead.lblEmpName.Text = sprdAttn.Text

        'sprdAttn.Row = 0
        'sprdAttn.Col = eventArgs.col
        'If Val(Mid(LTrim(sprdAttn.Text), 1, 2)) = 0 Then Exit Sub

        'frmAttnHead.lblDate.Text = Mid(Trim(sprdAttn.Text), 1, 2) & " " & lblYear.Text
        'mDays = Mid(LTrim(sprdAttn.Text), 1, 2) & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))

        'If ChechJoinLeaveDate(mDays, mCode, "Y") = False Then Exit Sub

        'frmAttnHead.lblType.Text = CStr(1)
        'frmAttnHead.ShowDialog()

        'If ConAttnDataFromMC = False Then
        '    RefreshScreen()
        'End If

    End Sub

    Private Sub sprdAttn_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles sprdAttn.KeyUpEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            Call sprdAttn_DblClick(sprdAttn, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(sprdAttn.ActiveCol, sprdAttn.ActiveRow))
        End If
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(TxtEmpCode.Text) = "" Then GoTo EventExitSub
        TxtEmpCode.Text = VB6.Format(TxtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((TxtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtEmpCode.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtEmpCode.Enabled = True
            cmdSearch.Enabled = True
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

        On Error GoTo ErrRefreshScreen
        Dim mDOJ As Date
        Dim mDOL As Date
        Dim mDeptCode As String
        Dim mCheckDate As Date
        Dim I As Integer
        Dim RS As ADODB.Recordset = Nothing
        Dim mLastDay As Integer
        Dim mFirstDateOfMonth As String
        Dim mCL As Double
        Dim mSL As Double
        Dim mEL As Double
        Dim mRemarks As String
        Dim mRemarks1 As String

        Dim mEmpDOJ As String
        Dim mEmpDOL As String
        Dim mHoliday As Double
        Dim mHType As String
        Dim mCurrentDate As String
        Dim mAbsent As Double
        Dim mTotPresent As Double
        Dim mTotWFH As Double
        Dim mLayoffDays As Double
        Dim pLayOffDateStart As String
        Dim pLayOffDateEnd As String
        'Dim mMonthStart As String

        mCurCol = sprdAttn.ActiveCol
        mCurRow = sprdAttn.ActiveRow

        MainClass.ClearGrid(sprdAttn, -1)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        mCurrentDate = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        mMonth = CShort(VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        mYear = Year(CDate(lblRunDate.Text))

        mLastDay = Val(MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))))

        mFirstDateOfMonth = "01/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))

        LastDateofMon = MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))) & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))
        mDOJ = CDate(MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))) & "/" & mMonth & "/" & Year(CDate(lblRunDate.Text)))
        mDOL = CDate("01" & "/" & mMonth & "/" & Year(CDate(lblRunDate.Text)))


        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_FNAME, EMP.EMP_CODE, " & vbCrLf _
            & " EMP.EMP_DOJ,EMP.EMP_LEAVE_DATE, DESG_DESC,ADD_EMP_CODE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_DESG_MST DMST" & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP.COMPANY_CODE=DMST.COMPANY_CODE" & vbCrLf _
            & " AND trim(EMP.EMP_DESG_CODE)=trim(DMST.DESG_CODE)" & vbCrLf _
            & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf _
            & " AND EMP.EMP_DOJ<=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP.EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP.EMP_LEAVE_DATE IS NULL) "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mDeptCode = MasterNo
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(TxtEmpCode.Text) & "'"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)


        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    .MaxRows = cntRow
                    .Row = cntRow

                    .Col = ColEmpCode
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColEmpName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColEmpFather
                    .Text = IIf(IsDbNull(RsAttn.Fields("EMP_FNAME").Value), "", RsAttn.Fields("EMP_FNAME").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDbNull(RsAttn.Fields("DESG_DESC").Value), "", RsAttn.Fields("DESG_DESC").Value)

                    mLeave = 0
                    mWopay = 0
                    mCL = 0
                    mSL = 0
                    mHoliday = 0
                    mAbsent = 0
                    mEL = 0
                    mTotPresent = 0
                    mTotWFH = 0
                    mLayoffDays = 0
                    mRemarks1 = ""
                    mRemarks = ""
                    pLayOffDateStart = ""
                    pLayOffDateEnd = ""
                    For I = 1 To mLastDay
                        mCheckDate = CDate(VB6.Format(I & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY"))


                        .Row = cntRow
                        .Col = I + ColDesg

                        mHType = ""

                        If CDate(mCheckDate) <= CDate(mCurrentDate) Then
                            If ChechJoinLeaveDate(VB6.Format(mCheckDate, "DD/MM/YYYY"), mCode, "N") = False Then
                                .Text = ""

                            Else
                                If GetLayoffDate(VB6.Format(mCheckDate, "DD/MM/YYYY")) = True Then
                                    .Text = "Lay-Off"
                                    mLayoffDays = mLayoffDays + 1
                                Else
                                    '                            If GetIsHolidays(Format(mCheckDate, "DD/MM/YYYY"), mHType) = True Then
                                    '                                .Text = mHType
                                    '                            Else
                                    .Text = GetLeaveType(mCode, VB6.Format(mCheckDate, "DD/MM/YYYY"), mLeave, mAbsent, mEL, mWopay, mCL, mSL, mHoliday, mTotPresent, mTotWFH)
                                    '                            End If
                                End If
                            End If
                        Else
                            .Text = ""
                        End If
                    Next

                    mJDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, RsAttn.Fields("EMP_DOJ").Value, CDate(VB6.Format(LastDateofMon, "dd/mm/yyyy")))
                    mThisMonAttn = mJDays
                    If Not IsDbNull(RsAttn.Fields("EMP_LEAVE_DATE").Value) Then
                        mLDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, RsAttn.Fields("EMP_LEAVE_DATE").Value, CDate(VB6.Format(LastDateofMon, "dd/mm/yyyy")))
                    End If
                    If VB6.Format(RsAttn.Fields("EMP_DOJ").Value, "mm yyyy") = VB6.Format(RsAttn.Fields("EMP_LEAVE_DATE").Value, "mm yyyy") Then
                        mThisMonAttn = mJDays - mLDays + 1
                    ElseIf VB6.Format(RsAttn.Fields("EMP_DOJ").Value, "mm yyyy") = VB6.Format(LastDateofMon, "mm yyyy") Then
                        mThisMonAttn = mJDays + 1
                    ElseIf VB6.Format(RsAttn.Fields("EMP_LEAVE_DATE").Value, "mm yyyy") = VB6.Format(LastDateofMon, "mm yyyy") Then
                        mThisMonAttn = MainClass.LastDay(mMonth, mYear) - mLDays
                    End If

                    If MainClass.LastDay(mMonth, mYear) < mThisMonAttn Then
                        mThisMonAttn = MainClass.LastDay(mMonth, mYear)
                    End If

                    .Col = ColTotPresent
                    .Text = CStr(mTotPresent)

                    .Col = ColTotWFH
                    .Text = CStr(mTotWFH)

                    .Col = ColHoliday
                    .Text = CStr(mHoliday)

                    .Col = ColABSENT
                    .Text = CStr(mAbsent)

                    .Col = ColCL
                    .Text = CStr(mCL)

                    .Col = ColSL
                    .Text = CStr(mSL)

                    .Col = ColEL
                    .Text = CStr(mEL)

                    mEmpDOJ = IIf(IsDbNull(RsAttn.Fields("EMP_DOJ").Value), "", RsAttn.Fields("EMP_DOJ").Value)
                    mEmpDOL = IIf(IsDbNull(RsAttn.Fields("EMP_LEAVE_DATE").Value), "", RsAttn.Fields("EMP_LEAVE_DATE").Value)

                    If VB6.Format(mEmpDOJ, "YYYYMM") = VB6.Format(lblRunDate.Text, "YYYYMM") Then
                        mRemarks1 = VB6.Format(mEmpDOJ, "DD/MM/YYYY") & "(J)"
                    End If

                    If VB6.Format(mEmpDOL, "YYYYMM") = VB6.Format(lblRunDate.Text, "YYYYMM") Then
                        mRemarks1 = mRemarks1 & VB6.Format(mEmpDOJ, "DD/MM/YYYY") & "(L)"
                    End If

                    mRemarks = mRemarks & "(" & mTotPresent + mTotWFH + mHoliday + mCL + mSL + mEL & ")"

                    .Col = ColRemarks
                    .Text = mRemarks

                    .Col = ColRemarks1
                    .Text = mRemarks1

                    .Col = ColAddEmpCode
                    .Text = IIf(IsDBNull(RsAttn.Fields("ADD_EMP_CODE").Value), "", RsAttn.Fields("ADD_EMP_CODE").Value)



                    .Col = ColWOPAY
                    .Text = CStr(mWopay)

                    .Col = ColLayOff
                    .Text = CStr(mLayoffDays)

                    mWopay = 0
                    mLeave = 0
                    mCL = 0
                    mSL = 0
                    mHoliday = 0
                    mLayoffDays = 0
                    mRemarks = ""
                    cntRow = cntRow + 1

                    RsAttn.MoveNext()

                Loop
                MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            End With
        End If
        MainClass.SetFocusToCell(sprdAttn, mCurRow, mCurCol)
        Exit Sub
ErrRefreshScreen:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function GetMarkFromMachine(ByRef mEmpCode As String, ByRef pDate As String, ByRef mFirstHalf As String, ByRef mSecondHalf As String) As String
        On Error GoTo ErrPart

        Dim mEmpShiftIN As String
        Dim mEmpShiftOUT As String
        Dim mEmpShiftBreak As String

        Dim mMarginsMinute As Double
        Dim mEmpInTime As String
        Dim mEmpOutTime As String
        Dim mSLTime As String
        Dim mSLOutTime As String
        Dim mIsRoundClock As String
        Dim mShortLeave As Boolean
        Dim mBreakSLTime As String

        mMarginsMinute = IIf(IsDbNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mIsRoundClock = IIf(GetRoundClock(mEmpCode, pDate, "E") = True, "Y", "N")

        mEmpShiftIN = GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "I", mIsRoundClock, "E")
        mEmpShiftOUT = GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "O", mIsRoundClock, "E")
        '    mEmpShiftBreak = CVDate(Format(DateSerial(Year(mEmpShiftIN), Month(mEmpShiftIN), Day(mEmpShiftIN)) & " " & TimeSerial(Hour(mEmpShiftIN) + 4, Minute(mEmpShiftIN), 0), "DD/MM/YYYY HH:MM"))    ''GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "B", "E")
        If DateDiff(Microsoft.VisualBasic.DateInterval.Hour, CDate(mEmpShiftIN), CDate(mEmpShiftOUT)) <= 9 Then
            mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
            mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))
        Else
            mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 5, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
            '        mEmpShiftBreak = CVDate(Format(DateAdd("n", 30, mEmpShiftBreak), "DD/MM/YYYY HH:MM"))
        End If


        mSLTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 2, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
        mSLOutTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, -2, CDate(mEmpShiftOUT)), "DD/MM/YYYY HH:MM")))
        mBreakSLTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 2, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))
        mShortLeave = False

        'DateSerial(year(mEmpShiftOUT), month(mEmpShiftOUT), day(mEmpShiftOUT))

        If CheckEmpTime(mEmpCode, pDate, mEmpInTime, mEmpOutTime, mIsRoundClock) = False Then GoTo ErrPart

        If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Then
            If mFirstHalf = "" Then
                GetMarkFromMachine = "A"
            Else
                GetMarkFromMachine = mFirstHalf
            End If
        Else
            If mFirstHalf = "" Then
                If CDate(mEmpInTime) <= CDate(mSLTime) Then
                    GetMarkFromMachine = "P"
                Else
                    GetMarkFromMachine = "A"
                End If
                If CDate(mEmpInTime) > CDate(mEmpShiftIN) And CDate(mEmpInTime) <= CDate(mSLTime) Then
                    mShortLeave = True
                End If
            Else
                GetMarkFromMachine = mFirstHalf
            End If
        End If

        If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
            If mSecondHalf = "" Then
                If mEmpInTime = "00:00" Then
                    GetMarkFromMachine = GetMarkFromMachine & "," & "A"
                Else
                    GetMarkFromMachine = GetMarkFromMachine & "," & ""
                End If
            ElseIf mSecondHalf <> "" Then
                GetMarkFromMachine = GetMarkFromMachine & "," & mSecondHalf
            End If
        Else
            If mSecondHalf = "" Then
                If mFirstHalf = "" Then
                    '                If mShortLeave = False Then
                    If CDate(mEmpInTime) <= CDate(mEmpShiftBreak) And CDate(mEmpOutTime) >= CDate(mSLOutTime) Then
                        GetMarkFromMachine = GetMarkFromMachine & "," & "P"
                    Else
                        GetMarkFromMachine = GetMarkFromMachine & "," & "A"
                    End If
                    '                Else
                    '                    If CVDate(mEmpInTime) <= CVDate(mEmpShiftBreak) And CVDate(mEmpOutTime) >= CVDate(mEmpShiftOUT) Then
                    '                        GetMarkFromMachine = GetMarkFromMachine & "," & "P"
                    '                    Else
                    '                        GetMarkFromMachine = GetMarkFromMachine & "," & "A"
                    '                    End If
                    '                End If

                Else
                    If CDate(mEmpInTime) <= CDate(mEmpShiftBreak) And CDate(mEmpOutTime) >= CDate(mSLOutTime) Then
                        GetMarkFromMachine = GetMarkFromMachine & "," & "P"
                    Else
                        If CDate(mEmpInTime) <= CDate(mBreakSLTime) And CDate(mEmpOutTime) >= CDate(mEmpShiftOUT) Then
                            GetMarkFromMachine = GetMarkFromMachine & "," & "P"
                            mShortLeave = True
                        Else
                            GetMarkFromMachine = GetMarkFromMachine & "," & "A"
                        End If
                    End If
                End If
            Else
                GetMarkFromMachine = GetMarkFromMachine & "," & mSecondHalf
            End If
        End If
        Exit Function
ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
        '    Resume
        GetMarkFromMachine = ""
    End Function
    Private Function GetMarkFromMachineOld(ByRef mEmpCode As String, ByRef pDate As String, ByRef mFirstHalf As String, ByRef mSecondHalf As String) As String
        On Error GoTo ErrPart

        Dim mEmpShiftIN As String
        Dim mEmpShiftOUT As String
        Dim mEmpShiftBreak As String

        Dim mMarginsMinute As Double
        Dim mEmpInTime As String
        Dim mEmpOutTime As String

        mMarginsMinute = IIf(IsDbNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mEmpShiftIN = GetShiftTime(mEmpCode, pDate, mMarginsMinute, "I", "E")
        mEmpShiftOUT = GetShiftTime(mEmpCode, pDate, mMarginsMinute, "O", "E")
        mEmpShiftBreak = GetShiftTime(mEmpCode, pDate, mMarginsMinute, "B", "E")

        If CheckEmpTime(mEmpCode, pDate, mEmpInTime, mEmpOutTime, "") = False Then GoTo ErrPart

        If mEmpInTime = "00:00" Then
            If mFirstHalf = "" Then
                GetMarkFromMachineOld = "A"
            Else
                GetMarkFromMachineOld = mFirstHalf
            End If
        Else
            If mFirstHalf = "" Then
                If CDate(mEmpInTime) <= CDate(VB6.Format(TimeSerial(Hour(CDate(mEmpShiftBreak)) - 2, Minute(CDate(mEmpShiftBreak)) - 30, 0), "HH:MM")) Then
                    GetMarkFromMachineOld = "P"
                ElseIf CDate(mEmpInTime) >= CDate("19:30") Then
                    If CDate(VB6.Format(TimeSerial(Hour(CDate(mEmpShiftIN)) + 2, Minute(CDate(mEmpShiftIN)), 0), "HH:MM")) < CDate("00:00") Then
                        If CDate(mEmpInTime) <= CDate(VB6.Format(TimeSerial(Hour(CDate(mEmpShiftIN)) + 2, Minute(CDate(mEmpShiftIN)), 0), "HH:MM")) Then
                            GetMarkFromMachineOld = "P"
                        Else
                            GetMarkFromMachineOld = "A"
                        End If
                    Else
                        If CDate(mEmpInTime) <= CDate("23:59") Then
                            GetMarkFromMachineOld = "P"
                        Else
                            GetMarkFromMachineOld = "A"
                        End If
                    End If
                Else
                    GetMarkFromMachineOld = "A"
                End If
            Else
                GetMarkFromMachineOld = mFirstHalf
            End If
            '        ElseIf CVDate(mEmpInTime) > CVDate(Format(TimeSerial(Hour(mEmpShiftBreak), Minute(mEmpShiftBreak), 0), "HH:MM")) Then
            '            GetMarkFromMachineOld = "A"
            ''        ElseIf CVDate(mEmpInTime) < CVDate(Format(TimeSerial(Hour(mEmpShiftIN) + 4, Minute(mEmpShiftIN) + 30, 0), "HH:MM")) Then
            ''            GetMarkFromMachineOld = "A"
            '        End If
        End If

        If mEmpOutTime = "00:00" Then
            If mSecondHalf = "" Then
                If mEmpInTime = "00:00" Then
                    GetMarkFromMachineOld = GetMarkFromMachineOld & "," & "A"
                Else
                    GetMarkFromMachineOld = GetMarkFromMachineOld & "," & ""
                End If
            ElseIf mSecondHalf <> "" Then
                GetMarkFromMachineOld = GetMarkFromMachineOld & "," & mSecondHalf
            End If
        Else
            If mSecondHalf = "" Then
                If CDate(mEmpInTime) <= CDate(VB6.Format(TimeSerial(Hour(CDate(mEmpShiftBreak)), Minute(CDate(mEmpShiftBreak)), 0), "HH:MM")) And CDate(mEmpOutTime) >= CDate(VB6.Format(TimeSerial(Hour(CDate(mEmpShiftOUT)) - 2, Minute(CDate(mEmpShiftOUT)), 0), "HH:MM")) Then
                    GetMarkFromMachineOld = GetMarkFromMachineOld & "," & "P"
                Else
                    If CDate(mEmpInTime) >= CDate("19:30") Then
                        If CDate(mEmpInTime) <= CDate(VB6.Format(TimeSerial(Hour(CDate(mEmpShiftIN)) + 2, Minute(CDate(mEmpShiftIN)), 0), "HH:MM")) And CDate(mEmpOutTime) >= CDate(VB6.Format(TimeSerial(Hour(CDate(mEmpShiftOUT)) - 2, Minute(CDate(mEmpShiftOUT)), 0), "HH:MM")) Then
                            GetMarkFromMachineOld = GetMarkFromMachineOld & "," & "P"
                        Else

                        End If
                    Else
                        GetMarkFromMachineOld = GetMarkFromMachineOld & "," & "A"
                    End If
                End If
            Else
                GetMarkFromMachineOld = GetMarkFromMachineOld & "," & mSecondHalf
            End If

            ''        GetMarkFromMachineOld = GetMarkFromMachineOld & "," & "P"
            '        If CVDate(mEmpInTime) < CVDate(Format(TimeSerial(Hour(mEmpShiftIN) + 4, Minute(mEmpShiftIN) + 30, 0), "HH:MM")) Then
            '            If CVDate(mEmpOutTime) >= CVDate(mEmpShiftOUT) Then
            '                GetMarkFromMachineOld = GetMarkFromMachineOld & "," & "P"
            ''            ElseIf CVDate(mEmpOutTime) < CVDate(Format(TimeSerial(Hour(mEmpShiftOUT) + 2, Minute(mEmpShiftOUT), 0), "HH:MM")) Then
            ''                GetMarkFromMachineOld = "A"
            ''            ElseIf CVDate(mEmpInTime) < CVDate(Format(TimeSerial(Hour(mEmpShiftIN) + 4, Minute(mEmpShiftIN) + 30, 0), "HH:MM")) Then
            '            Else
            '                GetMarkFromMachineOld = GetMarkFromMachineOld & "," & "A"
            '            End If
            '        Else
            '            GetMarkFromMachineOld = GetMarkFromMachineOld & "," & "A"
            '        End If
        End If

        '    If mEmpInTime = "00:00" Then
        '        If mFirstHalf = "" And mSecondHalf = "" Then
        '            GetMarkFromMachineOld = "A,A"
        '        ElseIf mFirstHalf = "" And mSecondHalf <> "" Then
        '            GetMarkFromMachineOld = "A," & mSecondHalf
        '        ElseIf mFirstHalf <> "" And mSecondHalf = "" Then
        '            GetMarkFromMachineOld = mFirstHalf & ",A"
        '        ElseIf mFirstHalf <> "" And mSecondHalf <> "" Then
        '            GetMarkFromMachineOld = mFirstHalf & "," & mSecondHalf
        '        End If
        '    ElseIf mEmpInTime = "00:00" Then
        '        GetMarkFromMachineOld = "P,P"
        '    ElseIf mEmpInTime = "00:00" Then
        '        GetMarkFromMachineOld = "P,P"
        '    End If

        Exit Function
ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
        '    Resume
        GetMarkFromMachineOld = ""
    End Function
    Private Function CheckEmpTime(ByRef mEmpCode As String, ByRef mMonthDate As String, ByRef mEmpInTime As String, ByRef mEmpOutTime As String, ByRef mIsRound As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEMPODOut As String
        Dim mEmpODIn As String

        mEmpInTime = "00:00"
        mEmpOutTime = "00:00"
        mEMPODOut = "00:00"
        mEmpODIn = "00:00"

        SqlStr = " SELECT IN_TIME, OUT_TIME " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mEmpInTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
            mEmpOutTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:MM")

            mEmpInTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpInTime)), Minute(CDate(mEmpInTime)), 0), "DD/MM/YYYY HH:MM")

            If mIsRound = "Y" Then
                mEmpOutTime = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
            Else
                mEmpOutTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
            End If
        End If

        SqlStr = " SELECT MIN(TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI')) AS TIME_FROM " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M','P')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("TIME_FROM").Value) = False Then
                mEMPODOut = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
                mEMPODOut = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEMPODOut)), Minute(CDate(mEMPODOut)), 0), "DD/MM/YYYY HH:MM")
            End If
        End If

        If mIsRound = "Y" Then
            SqlStr = " SELECT MIN(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M','P')" & vbCrLf & " AND REF_DATE='" & UCase(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)), "DD-MMM-YYYY")) & "'"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDBNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mEmpODIn = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        Else
            SqlStr = " SELECT MAX(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M','P')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDbNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mEmpODIn = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        End If

        If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Then
            mEmpInTime = mEMPODOut
        End If

        If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
            mEmpOutTime = mEmpODIn
        End If

        If VB6.Format(mEMPODOut, "HH:MM") <> "00:00" Then
            If CDate(mEMPODOut) < CDate(mEmpInTime) Then
                mEmpInTime = mEMPODOut
            End If
        End If

        If VB6.Format(mEmpODIn, "HH:MM") <> "00:00" Then
            If CDate(mEmpODIn) > CDate(mEmpOutTime) Then
                mEmpOutTime = mEmpODIn
            End If
        End If

        CheckEmpTime = True
        Exit Function
ErrPart:
        CheckEmpTime = False

    End Function

    Private Function GetLeaveType(ByRef pCode As String, ByRef pCheckDate As String, ByRef mLeave As Double, ByRef mAbsent As Double, ByRef mEL As Double, ByRef mWopay As Double, ByRef mCL As Double, ByRef mSL As Double, ByRef mHoliday As Double, ByRef mTotPresent As Double, ByRef mTotWFH As Double) As String

        On Error GoTo ErrRefreshScreen
        Dim RS As ADODB.Recordset = Nothing
        Dim xFirstHalf As String
        Dim xSecondHalf As String
        Dim mAgtESI As String
        Dim mMarkType1 As String
        Dim mMarkType2 As String

        Dim mFirstPaidLeave As String = "N"
        Dim mSecondPaidLeave As String = "N"

        GetLeaveType = ""
        'optForm1Leave

        SqlStr = " SELECT ATTN_DATE,SECONDHALF, FIRSTHALF,AGT_ESI,EXTRA_LEAVE,EXTRA_LEAVE_2 " & vbCrLf _
            & " FROM  PAY_ATTN_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & pCode & "'" & vbCrLf _
            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        If optShow(0).Checked = True Then
            If RS.EOF = False Then
                If optForm1Leave.Checked = True Then
                    GetLeaveType = mMark(RS.Fields("FIRSTHALF").Value)
                    GetLeaveType = IIf(mMark(RS.Fields("SECONDHALF").Value) = "", GetLeaveType, GetLeaveType & ", ") & mMark(RS.Fields("SECONDHALF").Value)
                Else
                    mFirstPaidLeave = IIf(IsDBNull(RS.Fields("EXTRA_LEAVE").Value), "N", RS.Fields("EXTRA_LEAVE").Value)
                    mSecondPaidLeave = IIf(IsDBNull(RS.Fields("EXTRA_LEAVE_2").Value), "N", RS.Fields("EXTRA_LEAVE_2").Value)

                    GetLeaveType = IIf(mFirstPaidLeave = "N", mMark(RS.Fields("FIRSTHALF").Value), "W")
                    GetLeaveType = IIf(mMark(RS.Fields("SECONDHALF").Value) = "", GetLeaveType, GetLeaveType & ", ") & IIf(mSecondPaidLeave = "N", mMark(RS.Fields("SECONDHALF").Value), "W")
                End If

            Else
                GetLeaveType = ""       ''"P,P"
            End If
        Else
            If RS.EOF = False Then
                '            GetLeaveType = mMark(RS!FIRSTHALF)
                '            GetLeaveType = IIf(mMark(RS!SECONDHALF) = "", GetLeaveType, GetLeaveType & ", ") & mMark(RS!SECONDHALF)
                mAgtESI = IIf(IsDBNull(RS.Fields("AGT_ESI").Value), "N", RS.Fields("AGT_ESI").Value)

                If RS.Fields("FIRSTHALF").Value = HOLIDAY Or RS.Fields("FIRSTHALF").Value = SUNDAY Then
                    GetLeaveType = mMark(RS.Fields("FIRSTHALF").Value) & "," & mMark(RS.Fields("SECONDHALF").Value)
                Else
                    If CDate(pCheckDate) < CDate("01/04/2014") Then
                        GetLeaveType = GetMarkFromMachine(pCode, pCheckDate, IIf(RS.Fields("FIRSTHALF").Value = -1, "", mMark(RS.Fields("FIRSTHALF").Value)), IIf(RS.Fields("SECONDHALF").Value = -1, "", mMark(RS.Fields("SECONDHALF").Value)))
                    Else
                        If mAgtESI = "Y" And (RS.Fields("FIRSTHALF").Value = ABSENT Or RS.Fields("FIRSTHALF").Value = WOPAY Or RS.Fields("SECONDHALF").Value = ABSENT Or RS.Fields("SECONDHALF").Value = WOPAY) Then
                            mMarkType1 = mMark(RS.Fields("FIRSTHALF").Value)
                            mMarkType2 = mMark(RS.Fields("SECONDHALF").Value)

                            GetLeaveType = IIf(RS.Fields("FIRSTHALF").Value = -1, "", IIf(mMarkType1 = "A" Or mMarkType1 = "W", "X", mMarkType1))
                            GetLeaveType = IIf(mMark(RS.Fields("SECONDHALF").Value) = "", GetLeaveType, GetLeaveType & ", ") & IIf(RS.Fields("SECONDHALF").Value = -1, "", IIf(mMarkType2 = "A" Or mMarkType2 = "W", "X", mMarkType2))
                        Else
                            GetLeaveType = IIf(RS.Fields("FIRSTHALF").Value = -1, "", mMark(RS.Fields("FIRSTHALF").Value))
                            GetLeaveType = IIf(mMark(RS.Fields("SECONDHALF").Value) = "", GetLeaveType, GetLeaveType & ", ") & IIf(RS.Fields("SECONDHALF").Value = -1, "", mMark(RS.Fields("SECONDHALF").Value))
                        End If
                    End If
                End If
            Else
                If CDate(pCheckDate) < CDate("01/04/2014") Then
                    GetLeaveType = GetMarkFromMachine(pCode, pCheckDate, "", "")
                Else
                    GetLeaveType = ","
                End If
            End If
        End If
        xFirstHalf = VB.Left(GetLeaveType, 1)
        xFirstHalf = IIf(xFirstHalf = ",", "A", xFirstHalf)

        xSecondHalf = VB.Right(GetLeaveType, 1)
        xSecondHalf = IIf(xSecondHalf = ",", "A", xSecondHalf)

        If xFirstHalf = "A" Then
            mAbsent = mAbsent + 0.5
        ElseIf xFirstHalf = "C" Then
            mCL = mCL + 0.5
            mLeave = mLeave + 0.5
        ElseIf xFirstHalf = "E" Then
            mEL = mEL + 0.5
            mLeave = mLeave + 0.5
        ElseIf xFirstHalf = "S" Then
            mSL = mSL + 0.5
            mLeave = mLeave + 0.5
        ElseIf xFirstHalf = "M" Then
            mSL = mSL + 0.5
            mLeave = mLeave + 0.5
        ElseIf xFirstHalf = "W" Then
            mWopay = mWopay + 0.5
        ElseIf xFirstHalf = "F" Then
            mTotWFH = mTotWFH + 0.5
        ElseIf xFirstHalf = "U" Or xFirstHalf = "H" Then
            mHoliday = mHoliday + 0.5
        ElseIf xFirstHalf = "P" Or xFirstHalf = "L" Then
            mTotPresent = mTotPresent + 0.5
        End If

        If xSecondHalf = "A" Then
            mAbsent = mAbsent + 0.5
        ElseIf xSecondHalf = "C" Then
            mCL = mCL + 0.5
            mLeave = mLeave + 0.5
        ElseIf xSecondHalf = "E" Then
            mEL = mEL + 0.5
            mLeave = mLeave + 0.5
        ElseIf xSecondHalf = "S" Then
            mSL = mSL + 0.5
            mLeave = mLeave + 0.5
        ElseIf xSecondHalf = "M" Then
            mSL = mSL + 0.5
            mLeave = mLeave + 0.5
        ElseIf xSecondHalf = "W" Then
            mWopay = mWopay + 0.5
        ElseIf xSecondHalf = "F" Then
            mTotWFH = mTotWFH + 0.5
        ElseIf xSecondHalf = "U" Or xSecondHalf = "H" Then
            mHoliday = mHoliday + 0.5
        ElseIf xSecondHalf = "P" Or xSecondHalf = "L" Then
            mTotPresent = mTotPresent + 0.5
        End If



        Exit Function
ErrRefreshScreen:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = "Select DEPT_DESC from PAY_DEPT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCategory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCategory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCategory.SelectedIndex = 0

        '    cboCategory.Clear
        '    cboCategory.AddItem "General Staff"
        '    cboCategory.AddItem "Production Staff"
        '    cboCategory.AddItem "Export Staff"
        '    cboCategory.AddItem "Regular Worker"
        '    cboCategory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCategory.AddItem "Director"
        '    cboCategory.AddItem "Trainee Staff"
        '    cboCategory.ListIndex = 0

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub


    Private Function ChechJoinLeaveDate(ByRef mDays As String, ByRef mCode As String, ByRef pMsgFlash As String) As Boolean

        Dim SqlStr As String = ""
        Dim RsTempJL As ADODB.Recordset = Nothing

        SqlStr = " SELECT EMP_DOJ,EMP_LEAVE_DATE FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempJL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTempJL.EOF = False Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(RsTempJL.Fields("EMP_DOJ").Value, "dd/mm/yyyy")), CDate(VB6.Format(mDays, "dd/mm/yyyy"))) + 1 > 0 Then
                ChechJoinLeaveDate = True
            Else
                If pMsgFlash = "Y" Then
                    MsgInformation("Employee Joining Date is Greater then Current Date.")
                End If
                ChechJoinLeaveDate = False
                Exit Function
            End If
            If IsDbNull(RsTempJL.Fields("EMP_LEAVE_DATE").Value) Then
                ChechJoinLeaveDate = True
            ElseIf DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(mDays, "dd/mm/yyyy")), CDate(VB6.Format(RsTempJL.Fields("EMP_LEAVE_DATE").Value, "dd/mm/yyyy"))) < 0 Then
                If pMsgFlash = "Y" Then
                    MsgInformation("Employee Leaving Date is Less then Current Date.")
                End If
                ChechJoinLeaveDate = False
                Exit Function
            Else
                ChechJoinLeaveDate = True
            End If
        End If
    End Function
    Private Sub txtEmpCode_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub
End Class
