Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinDataSource
Imports Infragistics.Win.UltraWinExplorerBar
Imports Infragistics.Win.UltraWinGrid
Imports System.Data.OleDb

Friend Class frmEmpWiseMonthlyAttn
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColSNO As Short = 0
    Private Const ColCard As Short = 1
    Private Const ColName As Short = 2
    Private Const ColDept As Short = 3
    Private Const ColIO As Short = 4
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

    Private Const ColHoliday As Short = 36
    Private Const ColLeave As Short = 37
    Private Const ColNotPunch As Short = 38
    Private Const ColLC As Short = 39
    Private Const ColSL As Short = 40
    Private Const ColOT As Short = 41
    Private Const ColOD As Short = 42
    Private Const ColPresent As Short = 43
    Private Const ColWorking As Short = 44
    Private Const ColWorking_Hours As Short = 45
    Private Const ColSL_Hours As Short = 46
    Private Const ColABSENT As Short = 47
    Private Const ColReportingTo As Short = 48

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub FillHeading()

        Dim cntCol As Integer
        Dim I As Integer

        'MainClass.ClearGrid(sprdAttn)


        With sprdAttn
            .MaxCols = ColReportingTo

            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Row = -1

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColCard, 7)

            .Col = ColReportingTo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColReportingTo, 7)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 26)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 6)


            .Col = ColIO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColIO, 4)
            .ColsFrozen = ColIO

            For cntCol = ColDay1 To ColDay31
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .set_ColWidth(cntCol, 5.5)
            Next

            For cntCol = ColHoliday To ColABSENT
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeNumberDecPlaces = 1
                .TypeNumberDecimal = CStr(Asc("."))
                .TypeNumberMax = CDbl("9999999.9")
                .TypeNumberMin = CDbl("-9999999.9")

                .set_ColWidth(cntCol, 5.5)
            Next

            For cntCol = ColSL_Hours To ColSL_Hours
                .Col = cntCol
                '.CellType = SS_CELL_TYPE_EDIT
                '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColCard
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

            .Col = ColName
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColDept
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCard
            .Text = "Emp Card No"


            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDept
            .Text = "Dept."

            For cntCol = ColDay1 To ColDay31
                I = I + 1
                .Col = cntCol
                .Text = CStr(I)
            Next

            .Col = ColHoliday
            .Text = "Holiday"

            .Col = ColLeave
            .Text = "Leave"

            .Col = ColNotPunch
            .Text = "Not Punch"

            .Col = ColLC
            .Text = "Late Comers"

            .Col = ColSL
            .Text = "Short Leave"

            .Col = ColOT
            .Text = "Inventive"

            .Col = ColOD
            .Text = "O.D."

            .Col = ColPresent
            .Text = "Present"

            .Col = ColWorking
            .Text = "Working"

            .Col = ColWorking_Hours
            .Text = "Total Working (Hours)"

            .Col = ColSL_Hours
            .Text = "Total Short Leave (Hours)"

            .Col = ColABSENT
            .Text = "Absent / W/o Pay"

            .Col = ColReportingTo
            .Text = "Reporting To"

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, ColReportingTo)
            'sprdAttn.SetOddEvenRowColor(System.Drawing.ColorTranslator.ToOle(PubSpdMainColor), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), System.Drawing.ColorTranslator.ToOle(PubSpdAlterColor), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            'sprdAttn.set
            MainClass.SetSpreadColor(sprdAttn, -1, False)
            sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        End With
    End Sub

    'Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
    '    If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
    '        cboDept.Enabled = False
    '    Else
    '        cboDept.Enabled = True
    '    End If
    'End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpCode.Enabled = False
            cmdSearch.Enabled = False
        Else
            txtEmpCode.Enabled = True
            cmdSearch.Enabled = True
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
            cboCatgeory.Enabled = False
        Else
            cboCatgeory.Enabled = True
        End If
    End Sub

    Private Sub chkPageNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPageNo.CheckStateChanged
        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPageNo.Enabled = False
        Else
            txtPageNo.Enabled = True
        End If
    End Sub

    Private Sub cmdAbsent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAbsent.Click
        On Error GoTo ErrPart

        If PubUserLevel = 1 Then

        Else
            Exit Sub
        End If

        Call UpdateMarkForNotPunch(ABSENT)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdExport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExport.Click


        FraPreview.Visible = True
        FraPreview.BringToFront()
        '    With sprdAttn
        '        .Col = ColPic
        '        .ColHidden = True
        '        .ColWidth(ColDesc) = 27 + 15
        '        .ColWidth(ColSchd) = 4
        '        .ColWidth(ColCurrSubTotal) = 12
        '        .ColWidth(ColCurrTotal) = 12
        '        .ColWidth(ColPrevSubTotal) = 12
        '        .ColWidth(ColPrevTotal) = 12
        '    End With

        '    If UCase(lblType.Caption) = UCase("Balance Sheet") Then
        '        sprdAttn.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Balance Sheet as on " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    ElseIf UCase(lblType.Caption) = UCase("Fund Flow") Then
        '        sprdAttn.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Fund Flow as on " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    Else
        '        sprdAttn.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany!Company_Name & "/fn""Arial""/fz""10""/fb0/rPage #/p/n/fn""Arial""/fz""10""/fb1Profit & Loss A//c as on " & vb6.Format(txtDateTo, "DD/MM/YYYY") & ""
        '    End If
        Call SpreadSheetPreview(sprdAttn, SprdPreview, SprdCommand, VB6.PixelsToTwipsX(ClientRectangle.Width) - 200, VB6.PixelsToTwipsY(ClientRectangle.Height) - 200)

    End Sub

    Private Sub cmdWOPay_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdWOPay.Click
        On Error GoTo ErrPart

        If PubUserLevel = 1 Then

        Else
            Exit Sub
        End If


        Call UpdateMarkForNotPunch(WOPAY)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub UpdateMarkForNotPunch(ByRef pLeaveType As Object)

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mEmpCode As String
        Dim mDate As String
        Dim mDay As Integer
        Dim mIO As String
        Dim SqlStr As String = ""
        Dim mFieldName As String
        Dim mColor As FPSpreadADO.BackColorStyleConstants
        Dim mLastDayofMonth As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSqlStr As String
        Dim mFHalf As Integer
        Dim mFSecond As Integer

        mLastDayofMonth = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        With sprdAttn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCard
                mEmpCode = Trim(.Text)

                .Col = ColIO
                mIO = Trim(.Text)
                If mIO = "I" Then
                    mFieldName = "FIRSTHALF"
                Else
                    mFieldName = "SECONDHALF"
                End If

                For cntCol = ColDay1 To ColDay31
                    mDay = cntCol - 4

                    If mDay <= mLastDayofMonth Then
                        .Col = cntCol
                        mColor = System.Drawing.ColorTranslator.ToOle(.BackColor)
                        If mColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) Then

                            mDate = VB6.Format(mDay & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

                            pSqlStr = " SELECT * FROM PAY_ATTN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                            MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                            If RsTemp.EOF = True Then
                                mFHalf = -1
                                mFSecond = -1
                                If mIO = "I" Then
                                    mFHalf = pLeaveType
                                Else
                                    mFSecond = pLeaveType
                                End If
                                SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf & " ATTN_DATE, FIRSTHALF, SECONDHALF, " & vbCrLf & " AGT_LATE, CPL_AGT_DATE_FH," & vbCrLf & " CPL_AGT_DATE_SH, CPL_EARN, " & vbCrLf & " ADDUSER, ADDDATE " & vbCrLf & ") VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & vbCrLf & " '" & mEmpCode & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mFHalf & ", " & mFSecond & ", " & vbCrLf & " 'N', '', " & vbCrLf & " '', 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                            Else
                                SqlStr = "UPDATE PAY_ATTN_MST SET " & mFieldName & "= " & pLeaveType & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                            End If

                            PubDBCn.Execute(SqlStr)
                        End If
                    End If
                Next
            Next
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdCommand_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdCommand.ButtonClicked
        On Error GoTo ERR1
        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mSubject As String


        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String
        Dim cntRow As Integer
        Dim mEmpCode As String
        Dim mHODEmpCode As String
        Dim cntRow1 As Integer
        Dim StartRow As Integer
        Dim mHODeMail As String
        Dim mText As String
        Dim mTitle As String

        Dim mFilename As String

        SprdCommand.Col = eventArgs.col
        SprdCommand.Row = eventArgs.row

        Dim counter As Short
        If SprdCommand.CellType = FPSpreadADO.CellTypeConstants.CellTypeButton Then
            Select Case eventArgs.col
                Case 2 'Next
                    ShowNextPage(sprdAttn, SprdPreview, SprdCommand, eventArgs.col)

                Case 4 'Previous
                    ShowPreviousPage(sprdAttn, SprdPreview, SprdCommand, eventArgs.col)

                Case 6 'Zoom
                    SprdPreview.ZoomState = 3

                Case 8 'Print
                    PrintSpread() ''cmdPrint_Click

                Case 10 'Export
                    mFilename = "" '' ExportSprdToExcel(CommonDialog1)

                    '                If sprdAttn.ExportToExcelEx(mFilename, "AttnSheet", "a.txt", ExcelSaveFlagNone) = True Then
                    If sprdAttn.ExportToExcel(mFilename, "AttnSheet", "") = True Then
                        '                If sprdAttn.ExportExcelBook(mFilename, "") = True Then
                        MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name is " & mFilename)
                    End If

                Case 12 'eMail
                    If optHODWise.Checked = False Then
                        For cntRow = 1 To sprdAttn.MaxRows - 1 Step 2
                            sprdAttn.Row = cntRow

                            sprdAttn.Col = ColCard
                            mEmpCode = Trim(sprdAttn.Text)

                            sprdAttn.Col = ColName
                            mTitle = Trim(sprdAttn.Text)



                            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mTo = MasterNo
                            End If
                            mTo = Trim(mTo)

                            mFilename = "C:\MonthlyAttnReport_" & mEmpCode & ".htm"
                            If sprdAttn.ExportRangeToHTML(0, cntRow, ColDay31, cntRow + 1, mFilename, False, "") = True Then

                                sprdRemarks.ExportRangeToHTML(1, 1, 10, 1, mFilename, True, "")


                                mFrom = GetEMailID("HRD_MAIL_TO")
                                mCC = GetEMailID("HRD_MAIL_TO")

                                'mSubject = "Auto Generated Attendance Report for the month of " & vb6.Format(lblRunDate.Caption, "MMMM , YYYY")
                                mSubject = mTitle & "'S Attendance Report for the month of " & VB6.Format(lblRunDate.Text, "MMMM , YYYY")

                                mBodyText = "<html><body><br />" & "<b></b>" & mSubject & "<br />" & "<br />" & "<br />" & "Your Faithfully<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"

                                If Trim(mTo) <> "" Then
                                    '                                Call SendMailProcessThroughCDO(mFrom, mTo, mCC, "", strAccount, strPassword, mFilename, mBodyText, mSubject)
                                    If SendMailProcess(mFrom, mTo, mCC, "", mFilename, mSubject, mBodyText) = False Then GoTo ERR1

                                    '                            MsgInformation "e-Mail Successfully Complete." ''& vbCrLf & vbCrLf & "Export File Name is " & mFilename
                                End If
                            End If
                        Next
                    Else
                        For cntRow = 1 To sprdAttn.MaxRows - 1 Step 2
                            sprdAttn.Row = cntRow

                            sprdAttn.Col = ColReportingTo
                            mHODEmpCode = Trim(sprdAttn.Text)

                            If mHODEmpCode = "" Then GoTo NextRow
                            StartRow = cntRow
                            counter = cntRow
                            For cntRow1 = counter To sprdAttn.MaxRows
                                sprdAttn.Row = cntRow1
                                sprdAttn.Col = ColReportingTo
                                If mHODEmpCode <> Trim(sprdAttn.Text) Then
                                    cntRow = cntRow1 - 2
                                    Exit For
                                End If
                                If sprdAttn.MaxRows = cntRow1 Then
                                    cntRow = cntRow1 - 1
                                    Exit For
                                End If
                            Next

                            If MainClass.ValidateWithMasterTable(mHODEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mTitle = MasterNo
                            End If

                            'Attendance Report for the month of " & vb6.Format(lblRunDate.Caption, "MMMM , YYYY")

                            If MainClass.ValidateWithMasterTable(mHODEmpCode, "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mTo = MasterNo
                            End If
                            mTo = Trim(mTo)

                            mFilename = "C:\MonthlyAttnReport_" & mHODEmpCode & ".htm"
                            If sprdAttn.ExportRangeToHTML(0, StartRow, ColDay31, cntRow + 1, mFilename, False, "") = True Then
                                GoTo NextRow

                                mFrom = GetEMailID("HRD_MAIL_TO")
                                mCC = GetEMailID("HRD_MAIL_TO")

                                '                            mSubject = "Auto Generated Attendance Report for the month of " & vb6.Format(lblRunDate.Caption, "MMMM , YYYY")
                                mSubject = "Attendance Report for the month of " & VB6.Format(lblRunDate.Text, "MMMM , YYYY") & " Employee's Reported to : " & mTitle

                                mText = "Please Submit MIS-PUNCH / LEAVE / OD with in 48 Hours to HR."

                                mBodyText = "<html><body><br />" & "<b></b>" & mSubject & "<br />" & "<br />" & "<b></b>" & mText & "<br />" & "<br />" & "Your Faithfully<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"

                                If Trim(mTo) <> "" Then
                                    If SendMailProcess(mFrom, mTo, mCC, "", mFilename, mSubject, mBodyText) = False Then GoTo ERR1

                                    '                            MsgInformation "e-Mail Successfully Complete." ''& vbCrLf & vbCrLf & "Export File Name is " & mFilename
                                End If
                            End If
NextRow:
                        Next
                    End If
                Case 18 'Close
                    FraPreview.Visible = False
                    '                With sprdAttn
                    '                     .Col = ColPic
                    '                    .ColHidden = False
                    '                    .ColWidth(ColDesc) = 30
                    '                    .ColWidth(ColSchd) = 4
                    '                    .ColWidth(ColCurrSubTotal) = 12
                    '                    .ColWidth(ColCurrTotal) = 12
                    '                    .ColWidth(ColPrevSubTotal) = 12
                    '                    .ColWidth(ColPrevSubTotal) = 12
                    '                End With
            End Select
        End If
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            Exit Sub
        End If
        MsgInformation(Err.Description)
    End Sub
    Sub PrintSpread()
        'Set printing options for spreadsheet
        CommonDialog1Print.ShowDialog()
        sprdAttn.PrintBorder = True
        sprdAttn.PrintOrientation = FPSpreadADO.PrintOrientationConstants.PrintOrientationLandscape
        sprdAttn.PrintColHeaders = True
        sprdAttn.PrintRowHeaders = False
        sprdAttn.PrintBorder = True
        sprdAttn.PrintColor = True

        sprdAttn.PrintShadows = True
        sprdAttn.PrintGrid = True
        sprdAttn.PrintUseDataMax = True
        sprdAttn.PrintCenterOnPageH = False
        sprdAttn.PrintCenterOnPageV = False

        '    sprdAttn.

        'Page Range
        'All
        '    If Option1(0).Value = True Then
        sprdAttn.PrintType = FPSpreadADO.PrintTypeConstants.PrintTypeAll

        '    'Selected cells
        '    ElseIf Option1(1).Value = True Then
        '        sprdAttn.Col = sprdAttn.SelBlockCol
        '        sprdAttn.col2 = sprdAttn.SelBlockCol2
        '        sprdAttn.Row = sprdAttn.SelBlockRow
        '        sprdAttn.Row2 = sprdAttn.SelBlockRow2
        '        sprdAttn.PrintType = PrintTypeCellRange
        '
        '    'Current Page
        '    ElseIf Option1(2).Value = True Then
        '        sprdAttn.PrintType = PrintTypeCurrentPage
        '
        '    'Pages
        '    Else
        '        sprdAttn.PrintPageStart = CInt(Text1(0).Text)
        '        sprdAttn.PrintPageEnd = CInt(Text1(1).Text)
        '        sprdAttn.PrintType = PrintTypePageRange
        '    End If

        'Print control
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        sprdAttn.PrintSheet()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

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


        ''Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "For The Month : " & VB6.Format(lblRunDate.Text, "MMMM-YYYY")
        mTitle = "Employee Attendance Report"

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.Text <> "" Then
            mSubTitle = mSubTitle & " - " & cboCatgeory.Text
        End If

        Call ShowReport(SqlStr, "MonthlyAttnCheckList.Rpt", Mode, mTitle, mSubTitle)

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

        On Error GoTo ErrPart

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtEmpCode.Text) = "" Then
                MsgInformation("Please Select Operator Code")
                txtEmpCode.Focus()
                Exit Sub
            End If

            txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Employee Code ")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If


        ''RefreshScreenOld()
        MainClass.ClearGrid(sprdAttn)
        RefreshScreen()


        '    FillGridColor
        MainClass.ProtectCell(sprdAttn, 0, sprdAttn.MaxRows, 0, ColABSENT)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub frmEmpWiseMonthlyAttn_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        Me.Text = "Monthly Attendance Report"
    End Sub

    Private Sub frmEmpWiseMonthlyAttn_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        MainClass.ClearGrid(sprdAttn)
        FillHeading()

        FillDeptCombo()

        lblRunDate.Text = VB6.Format(RunDate, "MMMM-YYYY")

        'chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkBookNo.CheckState = System.Windows.Forms.CheckState.Checked
        chkPageNo.CheckState = System.Windows.Forms.CheckState.Checked

        cboDept.Enabled = True
        txtBookNo.Enabled = False
        txtPageNo.Enabled = False

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCatgeory.Enabled = False

        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        txtEmpCode.Enabled = False
        cmdSearch.Enabled = False

        'lblColor(0).BackStyle = 1
        'lblColor(1).BackStyle = 1
        'lblColor(2).BackStyle = 1
        'lblColor(3).BackStyle = 1
        'lblColor(4).BackStyle = 1
        'lblColor(5).BackStyle = 1
        'lblColor(6).BackStyle = 1
        'lblColor(7).BackStyle = 1
        'lblColor(8).BackStyle = 1
        'lblColor(9).BackStyle = 1

        lblColor(0).BackColor = System.Drawing.Color.FromArgb(255, 128, 128)   '' System.Drawing.ColorTranslator.FromOle(&HFF) ''255, 128, 128
        lblColor(1).BackColor = System.Drawing.Color.FromArgb(128, 255, 128)
        lblColor(2).BackColor = System.Drawing.Color.FromArgb(192, 192, 255)
        lblColor(3).BackColor = System.Drawing.Color.FromArgb(192, 255, 255)
        lblColor(4).BackColor = System.Drawing.Color.FromArgb(255, 255, 128)
        lblColor(5).BackColor = System.Drawing.Color.FromArgb(255, 192, 128)
        lblColor(6).BackColor = System.Drawing.Color.FromArgb(255, 192, 255)
        lblColor(7).BackColor = System.Drawing.Color.FromArgb(192, 192, 255)
        lblColor(8).BackColor = System.Drawing.Color.FromArgb(255, 128, 128)
        lblColor(9).BackColor = System.Drawing.Color.FromArgb(255, 128, 255)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub frmEmpWiseMonthlyAttn_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdAttn.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        SprdPreview.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        FraPreview.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub UpDYear_DownClick()

        lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(lblRunDate.Text)), "MMMM-YYYY")

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdAttn, -1)
        '' RefreshScreen
    End Sub
    Private Sub UpDYear_UpClick()

        lblRunDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(lblRunDate.Text)), "MMMM-YYYY")

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdAttn, -1)
        ''RefreshScreen
    End Sub


    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        '    Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        '    NewDate = Format(Tempdate, "dd/mm/yyyy")
        '    lblRunDate.Caption = NewDate

        lblRunDate.Text = VB6.Format(lblRunDate.Text, "MMMM-YYYY")

        Daysinmonth = MainClass.LastDay(VB6.Format(lblRunDate.Text, "mm"), VB6.Format(lblRunDate.Text, "yyyy"))
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
    Private Sub RefreshScreenOld()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCode As String

        Dim cntRow As Integer

        Dim mDeptCode As String
        Dim mAttnDate As String
        Dim mDeptName As String
        Dim mContCode As String
        Dim mBookNo As String
        Dim mDate As String
        Dim mDOJ As String
        Dim mDOL As String
        Dim mLastDay As Integer
        Dim mDays As Integer
        Dim mRowIN As Integer
        Dim mRowOut As Integer
        Dim mRowOT As Integer
        Dim mHourDataRow As Integer
        Dim mPunchDataRow As Integer
        Dim mPunchData As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mFirstColor As FPSpreadADO.BackColorStyleConstants
        Dim mSecondColor As FPSpreadADO.BackColorStyleConstants

        Dim mHoliday1 As Double
        Dim mLeave1 As Double
        Dim mNotPunch1 As Double
        Dim mLC1 As Double
        Dim mSL1 As Double
        Dim mODuty1 As Double
        Dim mCPLEarnCnt1 As Double
        Dim mCPLAvailCnt1 As Double
        Dim mAbsent1 As Double
        Dim mAbsent2 As Double

        Dim mManual1 As Double
        Dim mManual2 As Double

        Dim mHoliday2 As Double
        Dim mLeave2 As Double
        Dim mNotPunch2 As Double
        Dim mLC2 As Double
        Dim mSL2 As Double
        Dim mODuty2 As Double
        Dim mCPLEarnCnt2 As Double
        Dim mCPLAvailCnt2 As Double
        Dim mCurrentDate As String
        Dim mMonthLastDate As String

        Dim mTotalSLMin As Double
        Dim mSLMin As Double
        Dim mOTHours As Double

        'If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If cboDept.Text = "" Then
        '        MsgInformation("Please select the Department Name.")
        '        cboDept.Focus()
        '        Exit Sub
        '    End If
        'End If

        mDate = VB6.Format(lblRunDate.Text, "YYYYMMDD")
        mDOJ = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        mDOL = "01" & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mMonthLastDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mCurrentDate = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        If VB6.Format(mMonthLastDate, "YYYYMM") > VB6.Format(mCurrentDate, "YYYYMM") Then
            Exit Sub
        End If

        If CDate(mMonthLastDate) > CDate(mCurrentDate) Then
            mLastDay = VB.Day(CDate(mCurrentDate))
        End If

        SqlStr = " SELECT DISTINCT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf _
            & " EMP.EMP_DEPT_CODE, EMP.EMP_HOD_CODE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_SHIFT_TRN SMST " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE = SMST.COMPANY_CODE " & vbCrLf _
            & " AND EMP.EMP_CODE = SMST.EMP_CODE " & vbCrLf & " AND TO_CHAR(SMST.SHIFT_DATE,'YYYYMM')= '" & VB6.Format(mDOJ, "YYYYMM") & "'"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            SqlStr = SqlStr & vbCrLf & " And (EMP.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP.EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        'If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDeptCode = MasterNo
        '        SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
        '    End If
        'End If

        If cboDept.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboDept.CheckedRows
                If mDeptName <> "" Then
                    mDeptName += "," & "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                Else
                    mDeptName += "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                End If
            Next
            'mSuppCustCodeNew = mSuppCustCodeNew + ","
        End If

        If mDeptName <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_DEPT_CODE IN (" & mDeptName & ")"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
        End If

        If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBookNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
        End If

        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPageNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        '    SqlStr = SqlStr & vbCrLf _
        ''                    & " AND ATTN_DATE>='" & UCase(Format(txtFrom.Text, "DD-MMM-YYYY")) & "'" & vbCrLf _
        ''                    & " AND ATTN_DATE<='" & UCase(Format(txtTo.Text, "DD-MMM-YYYY")) & "'"


        '    SqlStr = SqlStr & vbCrLf & "Group by EMP.EMP_NAME, EMP.EMP_CODE "

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCard.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        ElseIf optDept.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_DEPT_CODE, EMP.EMP_CODE"
        ElseIf optHODWise.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_HOD_CODE, EMP_DEPT_CODE, EMP.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by SMST.BOOKNO,SMST.PAGENO"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    .MaxRows = cntRow

                    .Row = cntRow
                    mRowIN = cntRow
                    .Col = ColCard
                    mCode = RsAttn.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColReportingTo
                    .Text = CStr(IIf(IsDBNull(RsAttn.Fields("EMP_HOD_CODE").Value), "", RsAttn.Fields("EMP_HOD_CODE").Value))

                    .Col = ColDept
                    .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

                    .Col = ColIO
                    .Text = "I"

                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                    .Row = cntRow
                    mRowOut = cntRow

                    .Col = ColCard
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColReportingTo
                    .Text = CStr(IIf(IsDBNull(RsAttn.Fields("EMP_HOD_CODE").Value), "", RsAttn.Fields("EMP_HOD_CODE").Value))

                    .Col = ColDept
                    .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

                    .Col = ColIO
                    .Text = "O"

                    'If chkWithWorkingHours.Value = vbChecked Then
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                    .Row = cntRow
                    mRowOT = cntRow

                    .Col = ColCard
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsAttn.Fields("EMP_NAME").Value

                    .Col = ColReportingTo
                    .Text = CStr(IIf(IsDBNull(RsAttn.Fields("EMP_HOD_CODE").Value), "", RsAttn.Fields("EMP_HOD_CODE").Value))

                    .Col = ColDept
                    .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

                    .Col = ColIO
                    .Text = "OT"
                    'End If
                    '
                    '                If chkPunchData.Value = vbChecked Then
                    '                    cntRow = cntRow + 1
                    '                    .MaxRows = cntRow
                    '                    .Row = cntRow
                    '                    mPunchDataRow = cntRow
                    '
                    '                    .Col = ColCard
                    '                    .Text = CStr(mCode)
                    '
                    '                    .Col = ColName
                    '                    .Text = RsAttn!EMP_NAME
                    '
                    '                    .Col = ColReportingTo
                    '                    .Text = CStr(IIf(IsNull(RsAttn!EMP_HOD_CODE), "", RsAttn!EMP_HOD_CODE))
                    '
                    '                    .Col = ColDept
                    '                    .Text = IIf(IsNull(RsAttn!EMP_DEPT_CODE), "", RsAttn!EMP_DEPT_CODE)
                    '
                    '                    .Col = ColIO
                    '                    .Text = "P"
                    '
                    '                End If

                    mHoliday1 = 0
                    mLeave1 = 0
                    mNotPunch1 = 0
                    mLC1 = 0
                    mSL1 = 0
                    mODuty1 = 0
                    mCPLEarnCnt1 = 0
                    mCPLAvailCnt1 = 0
                    mAbsent1 = 0
                    mManual1 = 0

                    mHoliday2 = 0
                    mLeave2 = 0
                    mNotPunch2 = 0
                    mLC2 = 0
                    mSL2 = 0
                    mODuty2 = 0
                    mCPLEarnCnt2 = 0
                    mCPLAvailCnt2 = 0
                    mTotalSLMin = 0
                    mSLMin = 0
                    mAbsent2 = 0
                    mManual2 = 0

                    For mDays = 1 To mLastDay
                        mAttnDate = VB6.Format(mDays & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
                        mInTime = "00:00"
                        mOutTime = "00:00"
                        mSLMin = 0

                        If GetINOUTTime(mCode, mAttnDate, mInTime, mOutTime, mFirstColor, mSecondColor, mSLMin, "O") = False Then GoTo refreshErrPart
                        mTotalSLMin = mTotalSLMin + mSLMin

                        .Row = mRowIN
                        .Col = ColIO + mDays
                        .Text = VB6.Format(mInTime, "HH:MM")

                        .Row = mRowIN
                        .Row2 = mRowIN
                        .Col = ColIO + mDays
                        .Col2 = ColIO + mDays
                        .BlockMode = True
                        .BackColor = System.Drawing.ColorTranslator.FromOle(mFirstColor)
                        .BlockMode = False

                        If System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) = mFirstColor Then
                            mNotPunch1 = mNotPunch1 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor) = mFirstColor Then
                            mODuty1 = mODuty1 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor) = mFirstColor Then
                            mLC1 = mLC1 + 1
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor) = mFirstColor Then
                            mHoliday1 = mHoliday1 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor) = mFirstColor Then
                            mLeave1 = mLeave1 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor) = mFirstColor Then
                            mSL1 = mSL1 + 1
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor) = mFirstColor Then
                            mCPLEarnCnt1 = mCPLEarnCnt1 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor) = mFirstColor Then
                            mCPLAvailCnt1 = mCPLAvailCnt1 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor) = mFirstColor Then
                            mAbsent1 = mAbsent1 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor) = mFirstColor Then
                            mManual1 = mManual1 + 0.5
                        End If


                        .Row = mRowOut
                        .Col = ColIO + mDays
                        .Text = VB6.Format(mOutTime, "HH:MM")

                        .Row = mRowOut
                        .Row2 = mRowOut
                        .Col = ColIO + mDays
                        .Col2 = ColIO + mDays
                        .BlockMode = True
                        .BackColor = System.Drawing.ColorTranslator.FromOle(mSecondColor)
                        .BlockMode = False

                        If System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) = mSecondColor Then
                            mNotPunch2 = mNotPunch2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor) = mSecondColor Then
                            mODuty2 = mODuty2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor) = mSecondColor Then
                            '                        mLC2 = mLC2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor) = mSecondColor Then
                            mHoliday2 = mHoliday2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor) = mSecondColor Then
                            mLeave2 = mLeave2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor) = mSecondColor Then
                            '                        mSL2 = mSL2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor) = mSecondColor Then
                            mCPLEarnCnt2 = mCPLEarnCnt2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor) = mSecondColor Then
                            mCPLAvailCnt2 = mCPLAvailCnt2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor) = mSecondColor Then
                            mAbsent2 = mAbsent2 + 0.5
                        ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor) = mSecondColor Then
                            mManual2 = mManual2 + 0.5
                        End If

                        .Row = mRowOT
                        .Col = ColIO + mDays
                        mOTHours = GetOTHours(mCode, mAttnDate)
                        .Text = mOTHours
                        '.Text = VB6.Format(mOutTime, "HH:MM")

                        .Row = mRowOT
                        .Row2 = mRowOT
                        .Col = ColIO + mDays
                        .Col2 = ColIO + mDays
                        .BlockMode = True
                        .BackColor = System.Drawing.ColorTranslator.FromOle(mSecondColor)
                        .BlockMode = False

                    Next

                    .Row = mRowIN
                    .Col = ColHoliday
                    .Text = VB6.Format(mHoliday1 + mHoliday2, "0.0")

                    .Col = ColLeave
                    .Text = VB6.Format(mLeave1 + mLeave2, "0.0")

                    .Col = ColNotPunch
                    .Text = VB6.Format(mNotPunch1 + mNotPunch2, "0.0")

                    .Col = ColLC
                    .Text = VB6.Format(mLC1 + mLC2, "0.0")

                    .Col = ColSL
                    .Text = VB6.Format(mSL1 + mSL2, "0.0")

                    .Col = ColOD
                    .Text = VB6.Format(mODuty1 + mODuty2, "0.0")

                    .Col = ColPresent
                    .Text = VB6.Format(mCPLEarnCnt1 + mCPLEarnCnt2, "0.0")

                    .Col = ColWorking
                    .Text = VB6.Format(mCPLAvailCnt1 + mCPLAvailCnt2, "0.0")

                    .Col = ColSL_Hours
                    .Text = VB6.Format(Int(mTotalSLMin / 60), "00") & ":" & VB6.Format(mTotalSLMin Mod 60, "00")

                    .Col = ColABSENT
                    .Text = VB6.Format(mAbsent1 + mAbsent2, "0.0")

                    '                .Row = mRowOut
                    '                .Col = ColHoliday
                    '                .Text = Format(mHoliday2, "0.0")
                    '
                    '                .Col = ColLeave
                    '                .Text = Format(mLeave2, "0.0")
                    '
                    '                .Col = ColNotPunch
                    '                .Text = Format(mNotPunch2, "0.0")
                    '
                    '                .Col = ColLC
                    '                .Text = Format(mLC2, "0.0")
                    '
                    '                .Col = ColSL
                    '                .Text = Format(mSL2, "0.0")
                    '
                    '                .Col = ColOD
                    '                .Text = Format(mODuty2, "0.0")
                    '
                    '                .Col = ColPresent
                    '                .Text = Format(mCPLEarnCnt2, "0.0")
                    '
                    '                .Col = ColWorking
                    '                .Text = Format(mCPLAvailCnt2, "0.0")

                    mHoliday1 = 0
                    mLeave1 = 0
                    mNotPunch1 = 0
                    mLC1 = 0
                    mSL1 = 0
                    mODuty1 = 0
                    mCPLEarnCnt1 = 0
                    mCPLAvailCnt1 = 0
                    mAbsent1 = 0
                    mManual1 = 0

                    mHoliday2 = 0
                    mLeave2 = 0
                    mNotPunch2 = 0
                    mLC2 = 0
                    mSL2 = 0
                    mODuty2 = 0
                    mCPLEarnCnt2 = 0
                    mCPLAvailCnt2 = 0
                    mTotalSLMin = 0
                    mSLMin = 0
                    mAbsent2 = 0
                    mManual2 = 0
                    mOTHours = 0

                    cntRow = cntRow + 1
                    RsAttn.MoveNext()
                Loop
            End With
        End If
        CmdPreview.Enabled = True
        cmdPrint.Enabled = True
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function GetSelectWHoursQry(ByVal mDOJ As String, ByVal mDOL As String, ByVal mFromDate As String, ByVal mToDate As String, ByVal AttnType As String) As String

        On Error GoTo refreshErrPart
        Dim mDeptName As String
        Dim mFieldName As String


        '' mFieldName = "FLOOR((ATTN.TOT_HOURS+.05)) + ((((ATTN.TOT_HOURS+.05)-floor((ATTN.TOT_HOURS+.05))))*.60)"   ''"ATTN.WORKS_HOURS"  ''WORKS_HOURS" '"ATTN.TOT_HOURS" 

        mFieldName = "CASE WHEN ATTN.TOT_HOURS>0 THEN (OUT_TIME-IN_TIME)* 1440 ELSE 0 END"

        ''(OUT_TIME-IN_TIME)* 1440

        GetSelectWHoursQry = " SELECT EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC, '" & AttnType & "' AS IO, " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='01' THEN " & mFieldName & " END)) DAY_1," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='02' THEN " & mFieldName & " END)) DAY_2," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='03' THEN " & mFieldName & " END)) DAY_3," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='04' THEN " & mFieldName & " END)) DAY_4," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='05' THEN " & mFieldName & " END)) DAY_5," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='06' THEN " & mFieldName & " END)) DAY_6," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='07' THEN " & mFieldName & " END)) DAY_7," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='08' THEN " & mFieldName & " END)) DAY_8," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='09' THEN " & mFieldName & " END)) DAY_9," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='10' THEN " & mFieldName & " END)) DAY_10," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='11' THEN " & mFieldName & " END)) DAY_11," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='12' THEN " & mFieldName & " END)) DAY_12," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='13' THEN " & mFieldName & " END)) DAY_13," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='14' THEN " & mFieldName & " END)) DAY_14," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='15' THEN " & mFieldName & " END)) DAY_15," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='16' THEN " & mFieldName & " END)) DAY_16," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='17' THEN " & mFieldName & " END)) DAY_17," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='18' THEN " & mFieldName & " END)) DAY_18," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='19' THEN " & mFieldName & " END)) DAY_19," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='20' THEN " & mFieldName & " END)) DAY_20," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='21' THEN " & mFieldName & " END)) DAY_21," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='22' THEN " & mFieldName & " END)) DAY_22," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='23' THEN " & mFieldName & " END)) DAY_23," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='24' THEN " & mFieldName & " END)) DAY_24," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='25' THEN " & mFieldName & " END)) DAY_25," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='26' THEN " & mFieldName & " END)) DAY_26," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='27' THEN " & mFieldName & " END)) DAY_27," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='28' THEN " & mFieldName & " END)) DAY_28," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='29' THEN " & mFieldName & " END)) DAY_29," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='30' THEN " & mFieldName & " END)) DAY_30," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='31' THEN " & mFieldName & " END)) DAY_31,"

        'GetSelectWHoursQry = " SELECT EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC, '" & AttnType & "' AS IO, " & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='01' THEN " & mFieldName & " END),'00.00') DAY_1," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='02' THEN " & mFieldName & " END),'00.00') DAY_2," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='03' THEN " & mFieldName & " END),'00.00') DAY_3," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='04' THEN " & mFieldName & " END),'00.00') DAY_4," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='05' THEN " & mFieldName & " END),'00.00') DAY_5," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='06' THEN " & mFieldName & " END),'00.00') DAY_6," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='07' THEN " & mFieldName & " END),'00.00') DAY_7," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='08' THEN " & mFieldName & " END),'00.00') DAY_8," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='09' THEN " & mFieldName & " END),'00.00') DAY_9," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='10' THEN " & mFieldName & " END),'00.00') DAY_10," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='11' THEN " & mFieldName & " END),'00.00') DAY_11," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='12' THEN " & mFieldName & " END),'00.00') DAY_12," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='13' THEN " & mFieldName & " END),'00.00') DAY_13," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='14' THEN " & mFieldName & " END),'00.00') DAY_14," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='15' THEN " & mFieldName & " END),'00.00') DAY_15," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='16' THEN " & mFieldName & " END),'00.00') DAY_16," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='17' THEN " & mFieldName & " END),'00.00') DAY_17," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='18' THEN " & mFieldName & " END),'00.00') DAY_18," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='19' THEN " & mFieldName & " END),'00.00') DAY_19," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='20' THEN " & mFieldName & " END),'00.00') DAY_20," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='21' THEN " & mFieldName & " END),'00.00') DAY_21," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='22' THEN " & mFieldName & " END),'00.00') DAY_22," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='23' THEN " & mFieldName & " END),'00.00') DAY_23," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='24' THEN " & mFieldName & " END),'00.00') DAY_24," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='25' THEN " & mFieldName & " END),'00.00') DAY_25," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='26' THEN " & mFieldName & " END),'00.00') DAY_26," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='27' THEN " & mFieldName & " END),'00.00') DAY_27," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='28' THEN " & mFieldName & " END),'00.00') DAY_28," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='29' THEN " & mFieldName & " END),'00.00') DAY_29," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='30' THEN " & mFieldName & " END),'00.00') DAY_30," & vbCrLf _
        '        & " TO_CHAR(SUM(CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='31' THEN " & mFieldName & " END),'00.00') DAY_31,"

        GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf _
                & " '' HOLIDAY1," & vbCrLf _
                & " '' LEAVE," & vbCrLf _
                & " '' NOTPUNCH," & vbCrLf _
                & " '' LC, '' SHORT_LEAVE, '' OVER_TIME," & vbCrLf _
                & " '' OD," & vbCrLf _
                & " '' CPLE," & vbCrLf _
                & " '' CPLA," & vbCrLf _
                & " '' WORKING_HOURS," & vbCrLf _
                & " '' SL_HOURS," & vbCrLf _
                & " '' ABSENT1, EMP_HOD_CODE "
        '
        GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_DALIY_ATTN_TRN ATTN, PAY_DEPT_MST DEPT " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =DEPT.COMPANY_CODE" & vbCrLf _
                & " And EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE "

        GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =ATTN.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=ATTN.EMP_CODE(+) "


        GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf _
            & " AND ATTN.ATTN_DATE(+) >=TO_DATE('" & VB6.Format(mFromDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ATTN.ATTN_DATE(+) <=TO_DATE('" & VB6.Format(mToDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "

        GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        'GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & " AND EMP.COMPANY_CODE = SMST.COMPANY_CODE " & vbCrLf _
        '    & " AND EMP.EMP_CODE = SMST.EMP_CODE " & vbCrLf & " AND TO_CHAR(SMST.SHIFT_DATE,'YYYYMM')= '" & VB6.Format(mDOJ, "YYYYMM") & "'"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & " And (EMP.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP.EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & " AND EMP.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If cboDept.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboDept.CheckedRows
                If mDeptName <> "" Then
                    mDeptName += "," & "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                Else
                    mDeptName += "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                End If
            Next
        End If

        If mDeptName <> "" Then
            GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & " AND EMP.EMP_DEPT_CODE IN (" & mDeptName & ")"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
            GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
        End If

        'If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBookNo.Text) <> "" Then
        '    GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & "AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
        'End If

        'If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPageNo.Text) <> "" Then
        '    GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & "AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
        'End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        GetSelectWHoursQry = GetSelectWHoursQry & vbCrLf & " GROUP BY EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC,EMP_HOD_CODE "

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCode As String

        Dim cntRow As Integer

        Dim mDeptCode As String
        Dim mAttnDate As String
        Dim mDeptName As String
        Dim mContCode As String
        Dim mBookNo As String
        Dim mDate As String
        Dim mDOJ As String
        Dim mDOL As String
        Dim mLastDay As Integer
        Dim mDays As Integer
        Dim mRowIN As Integer
        Dim mRowOut As Integer
        Dim mRowOT As Integer
        Dim mHourDataRow As Integer
        Dim mPunchDataRow As Integer
        Dim mPunchData As String

        Dim mInTime As String
        Dim mOutTime As String
        Dim mFirstColor As FPSpreadADO.BackColorStyleConstants
        Dim mSecondColor As FPSpreadADO.BackColorStyleConstants

        Dim mPresent1 As Double
        Dim mPresent2 As Double
        Dim mHoliday1 As Double
        Dim mLeave1 As Double
        Dim mNotPunch1 As Double
        Dim mLC1 As Double
        Dim mSL1 As Double
        Dim mODuty1 As Double
        Dim mCPLEarnCnt1 As Double
        Dim mCPLAvailCnt1 As Double
        Dim mAbsent1 As Double
        Dim mAbsent2 As Double

        Dim mManual1 As Double
        Dim mManual2 As Double

        Dim mHoliday2 As Double
        Dim mLeave2 As Double
        Dim mNotPunch2 As Double
        Dim mLC2 As Double
        Dim mSL2 As Double
        Dim mODuty2 As Double
        Dim mCPLEarnCnt2 As Double
        Dim mCPLAvailCnt2 As Double
        Dim mCurrentDate As String
        Dim mMonthLastDate As String

        Dim mTotalSLMin As Double
        Dim mSLMin As Double
        Dim mOTHours As Double
        Dim mWHours As Double
        Dim mWMintue As Double
        Dim mType As String
        Dim mRemarks As String
        Dim mINTime1 As String
        Dim mLateComer As Double
        Dim mAlterColor As Boolean = False
        mDate = VB6.Format(lblRunDate.Text, "YYYYMMDD")
        mDOJ = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))
        mDOL = "01" & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mMonthLastDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mCurrentDate = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        If VB6.Format(mMonthLastDate, "YYYYMM") > VB6.Format(mCurrentDate, "YYYYMM") Then
            Exit Sub
        End If

        If CDate(mMonthLastDate) > CDate(mCurrentDate) Then
            mLastDay = VB.Day(CDate(mCurrentDate))
        End If

        SqlStr = " SELECT EMP_CODE, EMP_NAME, DEPT_DESC, IO, " & vbCrLf _
                & " MAX(DAY_1) AS DAY_1," & vbCrLf _
                & " MAX(DAY_2) AS DAY_2," & vbCrLf _
                & " MAX(DAY_3) AS DAY_3," & vbCrLf _
                & " MAX(DAY_4) AS DAY_4," & vbCrLf _
                & " MAX(DAY_5) AS DAY_5," & vbCrLf _
                & " MAX(DAY_6) AS DAY_6," & vbCrLf _
                & " MAX(DAY_7) AS DAY_7," & vbCrLf _
                & " MAX(DAY_8) AS DAY_8," & vbCrLf _
                & " MAX(DAY_9) AS DAY_9," & vbCrLf _
                & " MAX(DAY_10) AS DAY_10," & vbCrLf _
                & " MAX(DAY_11) AS DAY_11," & vbCrLf _
                & " MAX(DAY_12) AS DAY_12," & vbCrLf _
                & " MAX(DAY_13) AS DAY_13," & vbCrLf _
                & " MAX(DAY_14) AS DAY_14," & vbCrLf _
                & " MAX(DAY_15) AS DAY_15," & vbCrLf _
                & " MAX(DAY_16) AS DAY_16," & vbCrLf _
                & " MAX(DAY_17) AS DAY_17," & vbCrLf _
                & " MAX(DAY_18) AS DAY_18," & vbCrLf _
                & " MAX(DAY_19) AS DAY_19," & vbCrLf _
                & " MAX(DAY_20) AS DAY_20," & vbCrLf _
                & " MAX(DAY_21) AS DAY_21," & vbCrLf _
                & " MAX(DAY_22) AS DAY_22," & vbCrLf _
                & " MAX(DAY_23) AS DAY_23," & vbCrLf _
                & " MAX(DAY_24) AS DAY_24," & vbCrLf _
                & " MAX(DAY_25) AS DAY_25," & vbCrLf _
                & " MAX(DAY_26) AS DAY_26," & vbCrLf _
                & " MAX(DAY_27) AS DAY_27," & vbCrLf _
                & " MAX(DAY_28) AS DAY_28," & vbCrLf _
                & " MAX(DAY_29) AS DAY_29," & vbCrLf _
                & " MAX(DAY_30) AS DAY_30," & vbCrLf _
                & " MAX(DAY_31) AS DAY_31," & vbCrLf _
                & " HOLIDAY1," & vbCrLf _
                & " LEAVE," & vbCrLf _
                & " NOTPUNCH," & vbCrLf _
                & " LC, SHORT_LEAVE, OVER_TIME," & vbCrLf _
                & " OD," & vbCrLf _
                & " CPLE," & vbCrLf _
                & " CPLA," & vbCrLf _
                & " WORKING_HOURS," & vbCrLf _
                & " SL_HOURS," & vbCrLf _
                & " ABSENT1, EMP_HOD_CODE FROM ("

        SqlStr = SqlStr & vbCrLf & GetSelectQry(mDOJ, mDOL, mDOL, mMonthLastDate, "I")

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & GetSelectQry(mDOJ, mDOL, mDOL, mMonthLastDate, "O")

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & GetSelectOTQry(mDOJ, mDOL, mDOL, mMonthLastDate, "O")

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & GetSelectAttnQry(mDOJ, mDOL, mDOL, mMonthLastDate, "P")

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & GetSelectWHoursQry(mDOJ, mDOL, mDOL, mMonthLastDate, "W")

        SqlStr = SqlStr & vbCrLf & " UNION ALL"

        SqlStr = SqlStr & vbCrLf & GetSelectRemarksQry(mDOJ, mDOL, mDOL, mMonthLastDate, "R")


        'SqlStr = SqlStr & vbCrLf & " UNION ALL"

        'SqlStr = SqlStr & vbCrLf & GetSelectAttnQry(mDOJ, mDOL, mDOL, mMonthLastDate, "PS")

        ''GetSelectQry(ByVal mDOJ As String, ByVal mDOL As String,  ByVal mFromDate As String, ByVal mToDate As String) As String

        SqlStr = SqlStr & vbCrLf & ") GROUP BY EMP_CODE, EMP_NAME, DEPT_DESC, IO, HOLIDAY1," & vbCrLf _
                & " LEAVE," & vbCrLf _
                & " NOTPUNCH," & vbCrLf _
                & " LC, SHORT_LEAVE, OVER_TIME," & vbCrLf _
                & " OD," & vbCrLf _
                & " CPLE," & vbCrLf _
                & " CPLA," & vbCrLf _
                & " WORKING_HOURS," & vbCrLf _
                & " SL_HOURS," & vbCrLf _
                & " ABSENT1, EMP_HOD_CODE"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_NAME"
        ElseIf optCard.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_CODE"
        ElseIf optDept.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by DEPT_DESC, EMP_CODE"
        ElseIf optHODWise.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP_HOD_CODE, DEPT_DESC, EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by BOOKNO,PAGENO"
        End If

        MainClass.AssignDataInSprd8(SqlStr, sprdAttn, StrConn, "Y")

        FillHeading()



        With sprdAttn

            .Row = 1
            .Row2 = .MaxRows
            .Col = ColCard
            .Col2 = ColIO
            .BlockMode = True
            .BackColor = PubSpdMainColor
            .BlockMode = False


            For cntRow = 1 To .MaxRows Step 6
                mRowIN = cntRow
                mRowOut = cntRow + 1
                mRowOT = cntRow + 2
                mHoliday1 = 0
                mLeave1 = 0
                mNotPunch1 = 0
                mLC1 = 0
                mSL1 = 0
                mODuty1 = 0
                mCPLEarnCnt1 = 0
                mCPLAvailCnt1 = 0
                mAbsent1 = 0
                mManual1 = 0

                mHoliday2 = 0
                mLeave2 = 0
                mNotPunch2 = 0
                mLC2 = 0
                mSL2 = 0
                mODuty2 = 0
                mCPLEarnCnt2 = 0
                mCPLAvailCnt2 = 0
                mTotalSLMin = 0
                mSLMin = 0
                mAbsent2 = 0
                mManual2 = 0
                mOTHours = 0
                mWHours = 0
                mPresent1 = 0
                mPresent2 = 0

                'sprdAttn.SetOddEvenRowColor(System.Drawing.ColorTranslator.ToOle(PubSpdMainColor), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), System.Drawing.ColorTranslator.ToOle(PubSpdAlterColor), System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
                .Row = cntRow
                .Col = ColCard
                If mAlterColor = False Then
                    .Row = mRowIN
                    .Row2 = mRowIN + 5
                    .Col = ColCard
                    .Col2 = ColIO
                    .BlockMode = True
                    .BackColor = PubSpdAlterColor
                    .BlockMode = False

                    .Row = mRowIN
                    .Row2 = mRowIN + 5
                    .Col = ColHoliday
                    .Col2 = ColReportingTo
                    .BlockMode = True
                    .BackColor = PubSpdAlterColor
                    .BlockMode = False


                    mAlterColor = True
                Else
                    .Row = mRowIN
                    .Row2 = mRowIN + 5
                    .Col = ColCard
                    .Col2 = ColIO
                    .BlockMode = True
                    .BackColor = PubSpdMainColor
                    .BlockMode = False

                    .Row = mRowIN
                    .Row2 = mRowIN + 5
                    .Col = ColHoliday
                    .Col2 = ColReportingTo
                    .BlockMode = True
                    .BackColor = PubSpdMainColor
                    .BlockMode = False


                    mAlterColor = False
                End If




                For mDays = 1 To mLastDay

                    mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(10).BackColor) '&HFFFFFF
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(10).BackColor) '&HFFFFFF

                    mAttnDate = VB6.Format(mDays & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

                    .Row = mRowIN
                    .Col = ColCard
                    mCode = Trim(.Text)

                    .Col = ColIO + mDays
                    mINTime1 = Trim(.Text)

                    .Row = mRowIN + 3
                    mType = Mid(.Text, 1, 2)

                    .Row = mRowIN + 4
                    mRemarks = Trim(.Text)

                    If mType = "PR" Or mType = "WF" Or mType = "CA" Then
                        mPresent1 = mPresent1 + 0.5
                    End If
                    If mType = "HD" Or mType = "SU" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
                        mHoliday1 = mHoliday1 + 0.5
                    ElseIf mType = "CL" Or mType = "EL" Or mType = "SL" Or mType = "ML" Or mType = "AL" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
                        If mType = "CL" Or mType = "EL" Or mType = "SL" Or mType = "ML" Then
                            mLeave1 = mLeave1 + 0.5
                        Else
                            mAbsent1 = mAbsent1 + 0.5
                        End If
                    ElseIf mType = "CE" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
                    ElseIf mType = "AB" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
                        mAbsent1 = mAbsent1 + 0.5
                    ElseIf mType = "CA" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
                    ElseIf mRemarks = "MANUAL" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
                    ElseIf mRemarks = "OD" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
                        mODuty1 = mODuty1 + 0.5
                    ElseIf mRemarks = "SHORT LEAVE" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
                    ElseIf mLateComer > 0 Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
                    ElseIf mINTime1 = "00:00" Or mINTime1 = "" Then
                        mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
                        mNotPunch1 = mNotPunch1 + 0.5
                    End If


                    .Row = mRowIN
                    .Row2 = mRowIN
                    .Col = ColIO + mDays
                    .Col2 = ColIO + mDays
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(mFirstColor)
                    .BlockMode = False

                    'If System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) = mFirstColor Then
                    '    mNotPunch1 = mNotPunch1 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor) = mFirstColor Then
                    '    mODuty1 = mODuty1 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor) = mFirstColor Then
                    '    mLC1 = mLC1 + 1
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor) = mFirstColor Then
                    '    mHoliday1 = mHoliday1 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor) = mFirstColor Then
                    '    mLeave1 = mLeave1 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor) = mFirstColor Then
                    '    mSL1 = mSL1 + 1
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor) = mFirstColor Then
                    '    mCPLEarnCnt1 = mCPLEarnCnt1 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor) = mFirstColor Then
                    '    mCPLAvailCnt1 = mCPLAvailCnt1 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor) = mFirstColor Then
                    '    mAbsent1 = mAbsent1 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor) = mFirstColor Then
                    '    mManual1 = mManual1 + 0.5
                    'End If


                    .Row = mRowOut
                    .Col = ColIO + mDays
                    mINTime1 = Trim(.Text)

                    .Row = mRowIN + 3
                    mType = Mid(.Text, 4, 2)

                    mRemarks = ""

                    If mType = "PR" Or mType = "WF" Or mType = "CA" Then
                        mPresent2 = mPresent2 + 0.5
                    End If

                    If mType = "HD" Or mType = "SU" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
                        mHoliday2 = mHoliday2 + 0.5
                    ElseIf mType = "CL" Or mType = "EL" Or mType = "SL" Or mType = "ML" Or mType = "AL" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
                        If mType = "CL" Or mType = "EL" Or mType = "SL" Or mType = "ML" Then
                            mLeave2 = mLeave2 + 0.5
                        Else
                            mAbsent2 = mAbsent2 + 0.5
                        End If
                    ElseIf mType = "CE" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
                    ElseIf mType = "AB" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
                        mAbsent2 = mAbsent2 + 0.5
                    ElseIf mType = "CA" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
                    ElseIf mRemarks = "MANUAL" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
                    ElseIf mRemarks = "OD" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
                        mODuty2 = mODuty2 + 0.5
                    ElseIf mRemarks = "SHORT LEAVE" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
                    ElseIf mLateComer > 0 Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
                    ElseIf mINTime1 = "00:00" Or mINTime1 = "" Then
                        mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
                        mNotPunch2 = mNotPunch2 + 0.5
                    End If

                    'If mType = "HD" Or mType = "SU" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
                    'ElseIf mType = "CL" Or mType = "EL" Or mType = "SL" Or mType = "ML" Or mType = "AL" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
                    'ElseIf mType = "CE" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
                    'ElseIf mType = "AB" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
                    'ElseIf mType = "CA" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
                    'ElseIf mRemarks = "MANUAL" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
                    'ElseIf mRemarks = "OD" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
                    'ElseIf mRemarks = "SHORT LEAVE" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
                    'ElseIf mLateComer > 0 Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
                    'ElseIf mINTime1 = "00:00" Or mINTime1 = "" Then
                    '    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
                    'End If

                    .Row = mRowOut
                    .Row2 = mRowOut
                    .Col = ColIO + mDays
                    .Col2 = ColIO + mDays
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(mSecondColor)
                    .BlockMode = False

                    'If System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) = mSecondColor Then
                    '    mNotPunch2 = mNotPunch2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor) = mSecondColor Then
                    '    mODuty2 = mODuty2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor) = mSecondColor Then
                    '    '                        mLC2 = mLC2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor) = mSecondColor Then
                    '    mHoliday2 = mHoliday2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor) = mSecondColor Then
                    '    mLeave2 = mLeave2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor) = mSecondColor Then
                    '    '                        mSL2 = mSL2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor) = mSecondColor Then
                    '    mCPLEarnCnt2 = mCPLEarnCnt2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor) = mSecondColor Then
                    '    mCPLAvailCnt2 = mCPLAvailCnt2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor) = mSecondColor Then
                    '    mAbsent2 = mAbsent2 + 0.5
                    'ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor) = mSecondColor Then
                    '    mManual2 = mManual2 + 0.5
                    'End If

                    .Row = mRowOT
                    .Col = ColIO + mDays
                    mOTHours = mOTHours + Val(.Text)     ''GetOTHours(mCode, mAttnDate)
                    '.Text = mOTHours
                    '.Text = VB6.Format(mOutTime, "HH:MM")

                    .Row = mRowOT + 3
                    .Col = ColIO + mDays
                    mWMintue = Val(.Text) ''(Int(Val(.Text)) * 60) + ((Val(.Text) - Int(Val(.Text))) * 100)
                    .Text = VB6.Format(Int(mWMintue / 60) + ((mWMintue - Int(mWMintue / 60) * 60) / 100), "0.00")

                    mWHours = mWHours + mWMintue     ''GetOTHours(mCode, mAttnDate)

                    .Row = mRowOT
                    .Row2 = mRowOT + 3
                    .Col = ColIO + mDays
                    .Col2 = ColIO + mDays
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(mFirstColor)
                    .BlockMode = False

                Next

                .Row = mRowIN
                .Col = ColHoliday
                .Text = VB6.Format(mHoliday1 + mHoliday2, "0.0")

                .Col = ColLeave
                .Text = VB6.Format(mLeave1 + mLeave2, "0.0")

                .Col = ColNotPunch
                .Text = VB6.Format(mNotPunch1 + mNotPunch2, "0.0")

                .Col = ColLC
                .Text = VB6.Format(mLC1 + mLC2, "0.0")

                .Col = ColSL
                .Text = VB6.Format(mSL1 + mSL2, "0.0")

                .Col = ColOD
                .Text = VB6.Format(mODuty1 + mODuty2, "0.0")

                .Col = ColOT
                .Text = VB6.Format(mOTHours, "0.0")


                .Col = ColPresent
                .Text = VB6.Format(mPresent1 + mPresent2, "0.0")

                .Col = ColWorking
                .Text = VB6.Format(mPresent1 + mPresent2 + mHoliday1 + mHoliday2 + mLeave1 + mLeave2, "0.0")

                '.Col = ColWorking_Hours
                '.Text = VB6.Format(Int(mTotalSLMin / 60), "00") & ":" & VB6.Format(mTotalSLMin Mod 60, "00")


                .Col = ColWorking_Hours
                mWHours = Int(mWHours / 60) + ((mWHours - Int(mWHours / 60) * 60) / 100)

                .Text = VB6.Format(mWHours, "0.00")

                .Col = ColSL_Hours
                .Text = VB6.Format(Int(mTotalSLMin / 60), "00") & ":" & VB6.Format(mTotalSLMin Mod 60, "00")

                .Col = ColABSENT
                .Text = VB6.Format(mAbsent1 + mAbsent2, "0.0")


                mHoliday1 = 0
                mLeave1 = 0
                mNotPunch1 = 0
                mLC1 = 0
                mSL1 = 0
                mODuty1 = 0
                mCPLEarnCnt1 = 0
                mCPLAvailCnt1 = 0
                mAbsent1 = 0
                mManual1 = 0
                mPresent1 = 0
                mPresent2 = 0
                mHoliday2 = 0
                mLeave2 = 0
                mNotPunch2 = 0
                mLC2 = 0
                mSL2 = 0
                mODuty2 = 0
                mCPLEarnCnt2 = 0
                mCPLAvailCnt2 = 0
                mTotalSLMin = 0
                mSLMin = 0
                mAbsent2 = 0
                mManual2 = 0
                mOTHours = 0
                mWHours = 0
            Next
        End With

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        'If RsAttn.EOF = False Then
        '    With sprdAttn
        '        cntRow = 1
        '        Do While Not RsAttn.EOF
        '            .MaxRows = cntRow

        '            .Row = cntRow
        '            mRowIN = cntRow
        '            .Col = ColCard
        '            mCode = RsAttn.Fields("EMP_CODE").Value
        '            .Text = CStr(mCode)

        '            .Col = ColName
        '            .Text = RsAttn.Fields("EMP_NAME").Value

        '            .Col = ColReportingTo
        '            .Text = CStr(IIf(IsDBNull(RsAttn.Fields("EMP_HOD_CODE").Value), "", RsAttn.Fields("EMP_HOD_CODE").Value))

        '            .Col = ColDept
        '            .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

        '            .Col = ColIO
        '            .Text = "I"

        '            cntRow = cntRow + 1
        '            .MaxRows = cntRow
        '            .Row = cntRow
        '            mRowOut = cntRow

        '            .Col = ColCard
        '            .Text = CStr(mCode)

        '            .Col = ColName
        '            .Text = RsAttn.Fields("EMP_NAME").Value

        '            .Col = ColReportingTo
        '            .Text = CStr(IIf(IsDBNull(RsAttn.Fields("EMP_HOD_CODE").Value), "", RsAttn.Fields("EMP_HOD_CODE").Value))

        '            .Col = ColDept
        '            .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

        '            .Col = ColIO
        '            .Text = "O"

        '            'If chkWithWorkingHours.Value = vbChecked Then
        '            cntRow = cntRow + 1
        '            .MaxRows = cntRow
        '            .Row = cntRow
        '            mRowOT = cntRow

        '            .Col = ColCard
        '            .Text = CStr(mCode)

        '            .Col = ColName
        '            .Text = RsAttn.Fields("EMP_NAME").Value

        '            .Col = ColReportingTo
        '            .Text = CStr(IIf(IsDBNull(RsAttn.Fields("EMP_HOD_CODE").Value), "", RsAttn.Fields("EMP_HOD_CODE").Value))

        '            .Col = ColDept
        '            .Text = IIf(IsDBNull(RsAttn.Fields("EMP_DEPT_CODE").Value), "", RsAttn.Fields("EMP_DEPT_CODE").Value)

        '            .Col = ColIO
        '            .Text = "OT"
        '            'End If
        '            '
        '            '                If chkPunchData.Value = vbChecked Then
        '            '                    cntRow = cntRow + 1
        '            '                    .MaxRows = cntRow
        '            '                    .Row = cntRow
        '            '                    mPunchDataRow = cntRow
        '            '
        '            '                    .Col = ColCard
        '            '                    .Text = CStr(mCode)
        '            '
        '            '                    .Col = ColName
        '            '                    .Text = RsAttn!EMP_NAME
        '            '
        '            '                    .Col = ColReportingTo
        '            '                    .Text = CStr(IIf(IsNull(RsAttn!EMP_HOD_CODE), "", RsAttn!EMP_HOD_CODE))
        '            '
        '            '                    .Col = ColDept
        '            '                    .Text = IIf(IsNull(RsAttn!EMP_DEPT_CODE), "", RsAttn!EMP_DEPT_CODE)
        '            '
        '            '                    .Col = ColIO
        '            '                    .Text = "P"
        '            '
        '            '                End If

        '            mHoliday1 = 0
        '            mLeave1 = 0
        '            mNotPunch1 = 0
        '            mLC1 = 0
        '            mSL1 = 0
        '            mODuty1 = 0
        '            mCPLEarnCnt1 = 0
        '            mCPLAvailCnt1 = 0
        '            mAbsent1 = 0
        '            mManual1 = 0

        '            mHoliday2 = 0
        '            mLeave2 = 0
        '            mNotPunch2 = 0
        '            mLC2 = 0
        '            mSL2 = 0
        '            mODuty2 = 0
        '            mCPLEarnCnt2 = 0
        '            mCPLAvailCnt2 = 0
        '            mTotalSLMin = 0
        '            mSLMin = 0
        '            mAbsent2 = 0
        '            mManual2 = 0

        '            For mDays = 1 To mLastDay
        '                mAttnDate = VB6.Format(mDays & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
        '                mInTime = "00:00"
        '                mOutTime = "00:00"
        '                mSLMin = 0

        '                If GetINOUTTime(mCode, mAttnDate, mInTime, mOutTime, mFirstColor, mSecondColor, mSLMin) = False Then GoTo refreshErrPart
        '                mTotalSLMin = mTotalSLMin + mSLMin

        '                .Row = mRowIN
        '                .Col = ColIO + mDays
        '                .Text = VB6.Format(mInTime, "HH:MM")

        '                .Row = mRowIN
        '                .Row2 = mRowIN
        '                .Col = ColIO + mDays
        '                .Col2 = ColIO + mDays
        '                .BlockMode = True
        '                .BackColor = System.Drawing.ColorTranslator.FromOle(mFirstColor)
        '                .BlockMode = False

        '                If System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) = mFirstColor Then
        '                    mNotPunch1 = mNotPunch1 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor) = mFirstColor Then
        '                    mODuty1 = mODuty1 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor) = mFirstColor Then
        '                    mLC1 = mLC1 + 1
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor) = mFirstColor Then
        '                    mHoliday1 = mHoliday1 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor) = mFirstColor Then
        '                    mLeave1 = mLeave1 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor) = mFirstColor Then
        '                    mSL1 = mSL1 + 1
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor) = mFirstColor Then
        '                    mCPLEarnCnt1 = mCPLEarnCnt1 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor) = mFirstColor Then
        '                    mCPLAvailCnt1 = mCPLAvailCnt1 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor) = mFirstColor Then
        '                    mAbsent1 = mAbsent1 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor) = mFirstColor Then
        '                    mManual1 = mManual1 + 0.5
        '                End If


        '                .Row = mRowOut
        '                .Col = ColIO + mDays
        '                .Text = VB6.Format(mOutTime, "HH:MM")

        '                .Row = mRowOut
        '                .Row2 = mRowOut
        '                .Col = ColIO + mDays
        '                .Col2 = ColIO + mDays
        '                .BlockMode = True
        '                .BackColor = System.Drawing.ColorTranslator.FromOle(mSecondColor)
        '                .BlockMode = False

        '                If System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor) = mSecondColor Then
        '                    mNotPunch2 = mNotPunch2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor) = mSecondColor Then
        '                    mODuty2 = mODuty2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor) = mSecondColor Then
        '                    '                        mLC2 = mLC2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor) = mSecondColor Then
        '                    mHoliday2 = mHoliday2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor) = mSecondColor Then
        '                    mLeave2 = mLeave2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor) = mSecondColor Then
        '                    '                        mSL2 = mSL2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor) = mSecondColor Then
        '                    mCPLEarnCnt2 = mCPLEarnCnt2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor) = mSecondColor Then
        '                    mCPLAvailCnt2 = mCPLAvailCnt2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor) = mSecondColor Then
        '                    mAbsent2 = mAbsent2 + 0.5
        '                ElseIf System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor) = mSecondColor Then
        '                    mManual2 = mManual2 + 0.5
        '                End If

        '                .Row = mRowOT
        '                .Col = ColIO + mDays
        '                mOTHours = GetOTHours(mCode, mAttnDate)
        '                .Text = mOTHours
        '                '.Text = VB6.Format(mOutTime, "HH:MM")

        '                .Row = mRowOT
        '                .Row2 = mRowOT
        '                .Col = ColIO + mDays
        '                .Col2 = ColIO + mDays
        '                .BlockMode = True
        '                .BackColor = System.Drawing.ColorTranslator.FromOle(mSecondColor)
        '                .BlockMode = False

        '            Next

        '            .Row = mRowIN
        '            .Col = ColHoliday
        '            .Text = VB6.Format(mHoliday1 + mHoliday2, "0.0")

        '            .Col = ColLeave
        '            .Text = VB6.Format(mLeave1 + mLeave2, "0.0")

        '            .Col = ColNotPunch
        '            .Text = VB6.Format(mNotPunch1 + mNotPunch2, "0.0")

        '            .Col = ColLC
        '            .Text = VB6.Format(mLC1 + mLC2, "0.0")

        '            .Col = ColSL
        '            .Text = VB6.Format(mSL1 + mSL2, "0.0")

        '            .Col = ColOD
        '            .Text = VB6.Format(mODuty1 + mODuty2, "0.0")

        '            .Col = ColPresent
        '            .Text = VB6.Format(mCPLEarnCnt1 + mCPLEarnCnt2, "0.0")

        '            .Col = ColWorking
        '            .Text = VB6.Format(mCPLAvailCnt1 + mCPLAvailCnt2, "0.0")

        '            .Col = ColSL_Hours
        '            .Text = VB6.Format(Int(mTotalSLMin / 60), "00") & ":" & VB6.Format(mTotalSLMin Mod 60, "00")

        '            .Col = ColABSENT
        '            .Text = VB6.Format(mAbsent1 + mAbsent2, "0.0")

        '            '                .Row = mRowOut
        '            '                .Col = ColHoliday
        '            '                .Text = Format(mHoliday2, "0.0")
        '            '
        '            '                .Col = ColLeave
        '            '                .Text = Format(mLeave2, "0.0")
        '            '
        '            '                .Col = ColNotPunch
        '            '                .Text = Format(mNotPunch2, "0.0")
        '            '
        '            '                .Col = ColLC
        '            '                .Text = Format(mLC2, "0.0")
        '            '
        '            '                .Col = ColSL
        '            '                .Text = Format(mSL2, "0.0")
        '            '
        '            '                .Col = ColOD
        '            '                .Text = Format(mODuty2, "0.0")
        '            '
        '            '                .Col = ColPresent
        '            '                .Text = Format(mCPLEarnCnt2, "0.0")
        '            '
        '            '                .Col = ColWorking
        '            '                .Text = Format(mCPLAvailCnt2, "0.0")

        '            mHoliday1 = 0
        '            mLeave1 = 0
        '            mNotPunch1 = 0
        '            mLC1 = 0
        '            mSL1 = 0
        '            mODuty1 = 0
        '            mCPLEarnCnt1 = 0
        '            mCPLAvailCnt1 = 0
        '            mAbsent1 = 0
        '            mManual1 = 0

        '            mHoliday2 = 0
        '            mLeave2 = 0
        '            mNotPunch2 = 0
        '            mLC2 = 0
        '            mSL2 = 0
        '            mODuty2 = 0
        '            mCPLEarnCnt2 = 0
        '            mCPLAvailCnt2 = 0
        '            mTotalSLMin = 0
        '            mSLMin = 0
        '            mAbsent2 = 0
        '            mManual2 = 0
        '            mOTHours = 0

        '            cntRow = cntRow + 1
        '            RsAttn.MoveNext()
        '        Loop
        '    End With
        'End If
        CmdPreview.Enabled = True
        cmdPrint.Enabled = True
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function GetSelectQry(ByVal mDOJ As String, ByVal mDOL As String, ByVal mFromDate As String, ByVal mToDate As String, ByVal AttnType As String) As String

        On Error GoTo refreshErrPart
        Dim mDeptName As String
        Dim mFieldName As String

        If AttnType = "I" Then
            mFieldName = "ATTN.IN_TIME"
        Else
            mFieldName = "ATTN.OUT_TIME"
        End If
        GetSelectQry = " SELECT EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC, '" & AttnType & "' AS IO, " & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='01' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_1," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='02' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_2," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='03' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_3," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='04' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_4," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='05' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_5," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='06' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_6," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='07' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_7," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='08' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_8," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='09' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_9," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='10' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_10," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='11' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_11," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='12' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_12," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='13' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_13," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='14' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_14," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='15' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_15," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='16' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_16," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='17' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_17," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='18' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_18," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='19' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_19," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='20' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_20," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='21' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_21," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='22' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_22," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='23' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_23," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='24' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_24," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='25' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_25," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='26' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_26," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='27' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_27," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='28' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_28," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='29' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_29," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='30' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_30," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='31' THEN TO_CHAR(" & mFieldName & ",'HH24:MI') END DAY_31," & vbCrLf _
                & " '' HOLIDAY1," & vbCrLf _
                & " '' LEAVE," & vbCrLf _
                & " '' NOTPUNCH," & vbCrLf _
                & " '' LC, '' SHORT_LEAVE, '' OVER_TIME," & vbCrLf _
                & " '' OD," & vbCrLf _
                & " '' CPLE," & vbCrLf _
                & " '' CPLA," & vbCrLf _
                & " '' WORKING_HOURS," & vbCrLf _
                & " '' SL_HOURS," & vbCrLf _
                & " '' ABSENT1, EMP_HOD_CODE "
        '
        GetSelectQry = GetSelectQry & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_SHIFT_TRN SMST, PAY_DALIY_ATTN_TRN ATTN, PAY_DEPT_MST DEPT " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        GetSelectQry = GetSelectQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =DEPT.COMPANY_CODE" & vbCrLf _
                & " And EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE "

        GetSelectQry = GetSelectQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =ATTN.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=ATTN.EMP_CODE(+) "


        GetSelectQry = GetSelectQry & vbCrLf _
            & " AND ATTN.ATTN_DATE(+) >=TO_DATE('" & VB6.Format(mFromDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ATTN.ATTN_DATE(+) <=TO_DATE('" & VB6.Format(mToDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "

        GetSelectQry = GetSelectQry & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        GetSelectQry = GetSelectQry & vbCrLf & " AND EMP.COMPANY_CODE = SMST.COMPANY_CODE " & vbCrLf _
            & " AND EMP.EMP_CODE = SMST.EMP_CODE " & vbCrLf & " AND TO_CHAR(SMST.SHIFT_DATE,'YYYYMM')= '" & VB6.Format(mDOJ, "YYYYMM") & "'"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            GetSelectQry = GetSelectQry & vbCrLf & " And (EMP.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP.EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            GetSelectQry = GetSelectQry & vbCrLf & " AND EMP.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            GetSelectQry = GetSelectQry & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If cboDept.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboDept.CheckedRows
                If mDeptName <> "" Then
                    mDeptName += "," & "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                Else
                    mDeptName += "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                End If
            Next
        End If

        If mDeptName <> "" Then
            GetSelectQry = GetSelectQry & vbCrLf & " AND EMP.EMP_DEPT_CODE IN (" & mDeptName & ")"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
            GetSelectQry = GetSelectQry & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
        End If

        If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBookNo.Text) <> "" Then
            GetSelectQry = GetSelectQry & vbCrLf & "AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
        End If

        If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPageNo.Text) <> "" Then
            GetSelectQry = GetSelectQry & vbCrLf & "AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
        End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            GetSelectQry = GetSelectQry & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If




        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function GetSelectOTQry(ByVal mDOJ As String, ByVal mDOL As String, ByVal mFromDate As String, ByVal mToDate As String, ByVal AttnType As String) As String

        On Error GoTo refreshErrPart
        Dim mDeptName As String
        Dim mFieldName As String

        mFieldName = "OT.OTHOUR + ROUND(OT.OTMIN/60,2)"

        GetSelectOTQry = " SELECT EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC, 'OT' AS IO, " & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='01' THEN TO_CHAR(" & mFieldName & ") END DAY_1," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='02' THEN TO_CHAR(" & mFieldName & ") END DAY_2," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='03' THEN TO_CHAR(" & mFieldName & ") END DAY_3," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='04' THEN TO_CHAR(" & mFieldName & ") END DAY_4," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='05' THEN TO_CHAR(" & mFieldName & ") END DAY_5," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='06' THEN TO_CHAR(" & mFieldName & ") END DAY_6," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='07' THEN TO_CHAR(" & mFieldName & ") END DAY_7," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='08' THEN TO_CHAR(" & mFieldName & ") END DAY_8," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='09' THEN TO_CHAR(" & mFieldName & ") END DAY_9," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='10' THEN TO_CHAR(" & mFieldName & ") END DAY_10," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='11' THEN TO_CHAR(" & mFieldName & ") END DAY_11," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='12' THEN TO_CHAR(" & mFieldName & ") END DAY_12," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='13' THEN TO_CHAR(" & mFieldName & ") END DAY_13," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='14' THEN TO_CHAR(" & mFieldName & ") END DAY_14," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='15' THEN TO_CHAR(" & mFieldName & ") END DAY_15," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='16' THEN TO_CHAR(" & mFieldName & ") END DAY_16," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='17' THEN TO_CHAR(" & mFieldName & ") END DAY_17," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='18' THEN TO_CHAR(" & mFieldName & ") END DAY_18," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='19' THEN TO_CHAR(" & mFieldName & ") END DAY_19," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='20' THEN TO_CHAR(" & mFieldName & ") END DAY_20," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='21' THEN TO_CHAR(" & mFieldName & ") END DAY_21," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='22' THEN TO_CHAR(" & mFieldName & ") END DAY_22," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='23' THEN TO_CHAR(" & mFieldName & ") END DAY_23," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='24' THEN TO_CHAR(" & mFieldName & ") END DAY_24," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='25' THEN TO_CHAR(" & mFieldName & ") END DAY_25," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='26' THEN TO_CHAR(" & mFieldName & ") END DAY_26," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='27' THEN TO_CHAR(" & mFieldName & ") END DAY_27," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='28' THEN TO_CHAR(" & mFieldName & ") END DAY_28," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='29' THEN TO_CHAR(" & mFieldName & ") END DAY_29," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='30' THEN TO_CHAR(" & mFieldName & ") END DAY_30," & vbCrLf _
                & " CASE WHEN TO_CHAR(OT.OT_DATE,'DD')='31' THEN TO_CHAR(" & mFieldName & ") END DAY_31," & vbCrLf _
                & " '' HOLIDAY1," & vbCrLf _
                & " '' LEAVE," & vbCrLf _
                & " '' NOTPUNCH," & vbCrLf _
                & " '' LC, '' SHORT_LEAVE, '' OVER_TIME," & vbCrLf _
                & " '' OD," & vbCrLf _
                & " '' CPLE," & vbCrLf _
                & " '' CPLA," & vbCrLf _
                & " '' WORKING_HOURS," & vbCrLf _
                & " '' SL_HOURS," & vbCrLf _
                & " '' ABSENT1, EMP_HOD_CODE "
        '
        GetSelectOTQry = GetSelectOTQry & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_OVERTIME_MST OT, PAY_DEPT_MST DEPT " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        'SqlStr = "SELECT OT.OT_DATE, OT.OTHOUR , OT.OTMIN, OT.PREV_OTHOUR, OT.PREV_OTMIN " & vbCrLf _
        '        & " FROM PAY_OVERTIME_MST OT " & vbCrLf _
        '        & " WHERE " & vbCrLf _
        '        & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '        & " And OT.EMP_CODE='" & pEmpCode & "' " & vbCrLf _
        '        & " AND OT.OT_DATE=TO_DATE('" & VB6.Format(pAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '        & " ORDER BY OT.OT_DATE"

        GetSelectOTQry = GetSelectOTQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =DEPT.COMPANY_CODE" & vbCrLf _
                & " And EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE "

        GetSelectOTQry = GetSelectOTQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =OT.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=OT.EMP_CODE(+) "


        GetSelectOTQry = GetSelectOTQry & vbCrLf _
            & " AND OT.OT_DATE(+) >=TO_DATE('" & VB6.Format(mFromDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND OT.OT_DATE(+) <=TO_DATE('" & VB6.Format(mToDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "

        GetSelectOTQry = GetSelectOTQry & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        'GetSelectOTQry = GetSelectOTQry & vbCrLf & " AND EMP.COMPANY_CODE = SMST.COMPANY_CODE " & vbCrLf _
        '    & " AND EMP.EMP_CODE = SMST.EMP_CODE " & vbCrLf & " AND TO_CHAR(SMST.SHIFT_DATE,'YYYYMM')= '" & VB6.Format(mDOJ, "YYYYMM") & "'"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            GetSelectOTQry = GetSelectOTQry & vbCrLf & " And (EMP.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP.EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            GetSelectOTQry = GetSelectOTQry & vbCrLf & " AND EMP.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            GetSelectOTQry = GetSelectOTQry & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If cboDept.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboDept.CheckedRows
                If mDeptName <> "" Then
                    mDeptName += "," & "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                Else
                    mDeptName += "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                End If
            Next
        End If

        If mDeptName <> "" Then
            GetSelectOTQry = GetSelectOTQry & vbCrLf & " AND EMP.EMP_DEPT_CODE IN (" & mDeptName & ")"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
            GetSelectOTQry = GetSelectOTQry & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
        End If

        'If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBookNo.Text) <> "" Then
        '    GetSelectOTQry = GetSelectOTQry & vbCrLf & "AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
        'End If

        'If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPageNo.Text) <> "" Then
        '    GetSelectOTQry = GetSelectOTQry & vbCrLf & "AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
        'End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            GetSelectOTQry = GetSelectOTQry & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If




        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function GetOTHours(ByRef pEmpCode As String, ByRef pAttnDate As String) As Double

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotOTHour As Double
        Dim mTotOTMIN As Double

        mTotOTHour = 0
        mTotOTMIN = 0
        GetOTHours = 0

        GetOTHours = 0

        SqlStr = "SELECT OT.OT_DATE, OT.OTHOUR , OT.OTMIN, OT.PREV_OTHOUR, OT.PREV_OTMIN " & vbCrLf _
                & " FROM PAY_OVERTIME_MST OT " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And OT.EMP_CODE='" & pEmpCode & "' " & vbCrLf _
                & " AND OT.OT_DATE=TO_DATE('" & VB6.Format(pAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " ORDER BY OT.OT_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            '.Text = CStr(IIf(IsDBNull(RsOT.Fields("OTHOUR").Value), 0, RsOT.Fields("OTHOUR").Value))
            '.Text = CStr(IIf(IsDBNull(RsOT.Fields("OTMIN").Value), "", .Text & ".") & RsOT.Fields("OTMIN").Value)

            mTotOTHour = IIf(IsDBNull(RsTemp.Fields("OTHOUR").Value), 0, RsTemp.Fields("OTHOUR").Value)
            mTotOTMIN = IIf(IsDBNull(RsTemp.Fields("OTMIN").Value), 0, RsTemp.Fields("OTMIN").Value)

            GetOTHours = VB6.Format(GetTOTOverTime(mTotOTHour, mTotOTMIN), "0.00")
        End If

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function GetINOUTTime(ByRef mEmpCode As String, ByRef pDate As String, ByRef mInTime As String, ByRef mOutTime As String,
                                  ByRef mFirstColor As FPSpreadADO.BackColorStyleConstants,
                                  ByRef mSecondColor As FPSpreadADO.BackColorStyleConstants, ByRef mTotalSLMin As Double, ByRef pReportType As String) As Boolean
        On Error GoTo ErrPart

        Dim mEmpShiftIN As String
        Dim mEmpShiftOUT As String
        Dim mEmpShiftBreak As String

        Dim mMarginsMinute As Double
        'Dim mEmpInTime As String
        'Dim mEmpOutTime As String
        Dim mSLTime As String
        Dim mSLOutTime As String
        Dim mIsRoundClock As String
        Dim mShortLeave As Boolean
        Dim mFirstIsOD As Boolean
        Dim mSecondIsOD As Boolean
        Dim mISFirstPresent As Boolean
        Dim mISSecondPresent As Boolean

        Dim mISFirstLeave As Boolean
        Dim mISSecondLeave As Boolean
        Dim mCPLFirstEarn As Boolean
        Dim mCPLFirstAvail As Boolean
        Dim mCPLSecondEarn As Boolean
        Dim mCPLSecondAvail As Boolean
        Dim mISFirstShortLeave As Boolean
        Dim mISSecondShortLeave As Boolean
        Dim mFromTime As String
        Dim mToTime As String

        Dim mFirstAbsent As Boolean
        Dim mSecondAbsent As Boolean
        Dim mFirstManual As Boolean
        Dim mSecondManual As Boolean
        Dim mIsHoliday As Boolean

        GetINOUTTime = False

        mFromTime = ""
        mToTime = ""
        mFirstColor = &HFFFFFF
        mSecondColor = &HFFFFFF

        If CheckLeave(mEmpCode, pDate, "H", "I", mEmpShiftIN, mEmpShiftBreak, "", "") = True Then ''If GetIsHolidays(pDate, "", mEmpCode, "", "Y") = True Then
            mInTime = "HH"      ''"00:00"
            mOutTime = "HH"      '' "00:00"
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
            GetINOUTTime = True
            Exit Function
        End If

        mMarginsMinute = IIf(IsDBNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        mIsRoundClock = IIf(GetRoundClock(mEmpCode, pDate, "E") = True, "Y", "N")

        mEmpShiftIN = GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "I", mIsRoundClock, "E")
        mEmpShiftOUT = GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "O", mIsRoundClock, "E")
        '    mEmpShiftBreak = CVDate(Format(DateSerial(Year(mEmpShiftIN), Month(mEmpShiftIN), Day(mEmpShiftIN)) & " " & TimeSerial(Hour(mEmpShiftIN) + 4, Minute(mEmpShiftIN), 0), "DD/MM/YYYY HH:MM"))    ''GetShiftTimeNew(mEmpCode, pDate, mMarginsMinute, "B", "E")

        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))

        mSLTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 2, CDate(mEmpShiftIN)), "DD/MM/YYYY HH:MM")))
        If mEmpShiftOUT <> "00:00" Then
            mSLOutTime = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, -2, CDate(mEmpShiftOUT)), "DD/MM/YYYY HH:MM")))
        Else
            mSLOutTime = mEmpShiftOUT
        End If

        mIsHoliday = CheckLeave(mEmpCode, pDate, "SU", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
        If mIsHoliday = True Then
            mInTime = "SU"       ''"00:00" SANDEEP
            mOutTime = "SU"      ''"00:00"
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(3).BackColor)
            GetINOUTTime = True
            Exit Function
        End If

        '    mISFirstPresent = CheckLeave(mEmpCode, pDate, "PR", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
        If mISFirstPresent = False Then
            mISFirstLeave = CheckLeave(mEmpCode, pDate, "L", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
            mCPLFirstEarn = CheckLeave(mEmpCode, pDate, "CE", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
            mCPLFirstAvail = CheckLeave(mEmpCode, pDate, "CA", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
            mFirstAbsent = CheckLeave(mEmpCode, pDate, "AB", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
            mFirstManual = CheckLeave(mEmpCode, pDate, "M", "I", mEmpShiftIN, mEmpShiftBreak, "", "")
        End If

        '    mISSecondPresent = CheckLeave(mEmpCode, pDate, "PR", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
        If mISSecondPresent = False Then
            mISSecondLeave = CheckLeave(mEmpCode, pDate, "L", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
            mCPLSecondEarn = CheckLeave(mEmpCode, pDate, "CE", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
            mCPLSecondAvail = CheckLeave(mEmpCode, pDate, "CA", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
            mSecondAbsent = CheckLeave(mEmpCode, pDate, "AB", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
            mSecondManual = CheckLeave(mEmpCode, pDate, "M", "O", mEmpShiftIN, mEmpShiftBreak, "", "")
        End If

        mShortLeave = False
        mFirstIsOD = False
        mSecondIsOD = False

        'DateSerial(year(mEmpShiftOUT), month(mEmpShiftOUT), day(mEmpShiftOUT))

        If pReportType = "O" Then
            If CheckEmpTime(mEmpCode, pDate, mInTime, mOutTime, mIsRoundClock, mFirstIsOD, mSecondIsOD, mEmpShiftBreak) = False Then GoTo ErrPart
        End If

        mTotalSLMin = 0

        mISFirstShortLeave = CheckLeave(mEmpCode, pDate, "P", "I", mInTime, mEmpShiftBreak, mFromTime, mToTime)
        mTotalSLMin = CalcTotalMintue(mFromTime, mToTime)
        mISSecondShortLeave = CheckLeave(mEmpCode, pDate, "P", "O", mInTime, mEmpShiftBreak, mFromTime, mToTime)
        mTotalSLMin = mTotalSLMin + CalcTotalMintue(mFromTime, mToTime)

        If mISFirstPresent = True Then
            mFirstColor = &HFFFFFF
        ElseIf mISFirstLeave = True Then
            mInTime = "LE"
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
        ElseIf mCPLFirstEarn = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
        ElseIf mFirstAbsent = True Then
            mInTime = "AB"
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
        ElseIf mCPLFirstAvail = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
        ElseIf mFirstManual = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
        ElseIf VB6.Format(mInTime, "HH:MM") = "00:00" Then
            If mISFirstShortLeave = True Then
                mInTime = pDate & " " & mToTime
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
            Else
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
            End If
        ElseIf CDate(mInTime) > CDate(mEmpShiftIN) And CDate(mInTime) <= CDate(mSLTime) Then
            If mISFirstShortLeave = True Then
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
            ElseIf mFirstIsOD = True Then
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
            Else
                mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
            End If
        ElseIf CDate(mInTime) > CDate(mSLTime) Then
            '        If mISFirstShortLeave = True Then
            '            mFirstColor = lblColor(5).BackColor
            '        ElseIf mFirstIsOD = True Then
            '            mFirstColor = lblColor(1).BackColor
            '        Else
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
            '        End If
        ElseIf mFirstIsOD = True Then
            mFirstColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
        Else
            mFirstColor = &HFFFFFF
        End If

        If mISSecondPresent = True Then
            mSecondColor = &HFFFFFF
        ElseIf mISSecondLeave = True Then
            mOutTime = "LE"
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(4).BackColor)
        ElseIf mCPLSecondEarn = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(6).BackColor)
        ElseIf mSecondAbsent = True Then
            mOutTime = "AB"
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(8).BackColor)
        ElseIf mCPLSecondAvail = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(7).BackColor)
        ElseIf mSecondManual = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(9).BackColor)
        ElseIf VB6.Format(mOutTime, "HH:MM") = "00:00" Then
            If mISSecondShortLeave = True Then
                mOutTime = pDate & " " & mFromTime
                mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
            Else
                mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
            End If
        ElseIf CDate(mOutTime) < CDate(mEmpShiftOUT) Then
            If CDate(mOutTime) >= CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, -5, CDate(mEmpShiftBreak))) And CDate(mOutTime) >= CDate(mSLOutTime) Then
                If mISSecondShortLeave = True Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(5).BackColor)
                ElseIf mSecondIsOD = True Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
                Else
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(2).BackColor)
                End If
            Else
                If mSecondIsOD = True Then
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
                Else
                    mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
                End If
            End If
        ElseIf CDate(mOutTime) < CDate(mSLOutTime) Then
            '        If mISSecondShortLeave = True Then
            '            mSecondColor = lblColor(5).BackColor
            '        ElseIf mSecondIsOD = True Then
            '            mSecondColor = lblColor(1).BackColor
            '        Else
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(0).BackColor)
            '        End If
        ElseIf mSecondIsOD = True Then
            mSecondColor = System.Drawing.ColorTranslator.ToOle(lblColor(1).BackColor)
        Else
            mSecondColor = &HFFFFFF
        End If
        '
        '
        '
        '    If Format(mEmpInTime, "HH:MM") = "00:00" Then
        '        mFirstColor = lblColor(0).BackColor
        '    Else
        '        If CVDate(mEmpInTime) > CVDate(mEmpShiftIN) And CVDate(mEmpInTime) <= CVDate(mSLTime) Then
        '            mFirstColor = lblColor(2).BackColor
        '        End If
        '    End If
        '
        '    If mEmpOutTime = "00:00" Then
        '        If mSecondHalf = "" Then
        '            If mEmpInTime = "00:00" Then
        '                GetMarkFromMachine = GetMarkFromMachine & "," & "A"
        '            Else
        '                GetMarkFromMachine = GetMarkFromMachine & "," & ""
        '            End If
        '        ElseIf mSecondHalf <> "" Then
        '            GetMarkFromMachine = GetMarkFromMachine & "," & mSecondHalf
        '        End If
        '    Else
        '        If mSecondHalf = "" Then
        '            If mFirstHalf = "" Then
        ''                If mShortLeave = False Then
        '                    If CVDate(mEmpInTime) <= CVDate(mEmpShiftBreak) And CVDate(mEmpOutTime) >= CVDate(mSLOutTime) Then
        '                        GetMarkFromMachine = GetMarkFromMachine & "," & "P"
        '                    Else
        '                        GetMarkFromMachine = GetMarkFromMachine & "," & "A"
        '                    End If
        ''                Else
        ''                    If CVDate(mEmpInTime) <= CVDate(mEmpShiftBreak) And CVDate(mEmpOutTime) >= CVDate(mEmpShiftOUT) Then
        ''                        GetMarkFromMachine = GetMarkFromMachine & "," & "P"
        ''                    Else
        ''                        GetMarkFromMachine = GetMarkFromMachine & "," & "A"
        ''                    End If
        ''                End If
        '
        '            Else
        '                If CVDate(mEmpInTime) <= CVDate(mEmpShiftBreak) And CVDate(mEmpOutTime) >= CVDate(mSLOutTime) Then
        '                    GetMarkFromMachine = GetMarkFromMachine & "," & "P"
        '                Else
        '                     GetMarkFromMachine = GetMarkFromMachine & "," & "A"
        '                End If
        '            End If
        '        Else
        '            GetMarkFromMachine = GetMarkFromMachine & "," & mSecondHalf
        '        End If
        '    End If
        GetINOUTTime = True
        Exit Function
ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
        '    Resume
        GetINOUTTime = False
    End Function
    Private Function CalcTotalMintue(ByRef pFromTime As String, ByRef pToTime As String) As Double
        On Error GoTo ErrPart
        Dim mMin1 As Integer
        Dim mMin2 As Integer


        CalcTotalMintue = 0
        If Trim(pFromTime) = "" Or Trim(pFromTime) = "__:__" Or Trim(pToTime) = "" Or Trim(pToTime) = "__:__" Then Exit Function

        mMin1 = Hour(CDate(pFromTime)) * 60 + Minute(CDate(pFromTime))
        mMin2 = Hour(CDate(pToTime)) * 60 + Minute(CDate(pToTime))

        If mMin1 = 0 Or mMin2 = 0 Then Exit Function

        If CDate(pFromTime) <= CDate(pToTime) Then
            CalcTotalMintue = mMin2 - mMin1
        Else
            mMin2 = mMin2 + (24 * 60)
            CalcTotalMintue = mMin2 - mMin1
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CheckEmpTime(ByRef mEmpCode As String, ByRef mMonthDate As String, ByRef mEmpInTime As String, ByRef mEmpOutTime As String, ByRef mIsRound As String, ByRef mFirstIsOD As Boolean, ByRef mSecondIsOD As Boolean, ByRef mEmpShiftBreak As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEMPODOut As String
        Dim mEmpODIn As String
        Dim mIsODLocal1 As Boolean
        Dim mIsODLocal2 As Boolean

        mEmpInTime = ""
        mEmpOutTime = ""

        mIsODLocal1 = False
        mIsODLocal2 = False
        mFirstIsOD = False
        mSecondIsOD = False

        SqlStr = " SELECT IN_TIME, OUT_TIME " & vbCrLf _
            & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf _
            & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
            & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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
        Else
            mEmpInTime = "00:00"
            mEmpOutTime = "00:00"
        End If
        mEMPODOut = "00:00"
        mEmpODIn = "00:00"

        ''MIN(TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI'))

        SqlStr = " SELECT MIN(TIME_FROM) AS TIME_FROM " & vbCrLf _
            & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
            & " AND MOVE_TYPE IN ('O','M')" & vbCrLf _
            & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
        '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("TIME_FROM").Value) = False Then
                mIsODLocal1 = True
                mEMPODOut = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
                mEMPODOut = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEMPODOut)), Minute(CDate(mEMPODOut)), 0), "DD/MM/YYYY HH:MM")
            End If
        End If

        ''MIN(TO_CHAR(TIME_TO,'DD-MON-RRRR HH24:MI'))

        If mIsRound = "Y" Then
            SqlStr = " SELECT MIN(TIME_TO) AS TIME_TO " & vbCrLf _
                & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                & " AND MOVE_TYPE IN ('O','M')" & vbCrLf _
                & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" '" & UCase(Format(DateAdd("d", 1, mMonthDate), "DD-MMM-YYYY")) & "'"

            If VB6.Format(mEmpInTime, "HH:MM") <> "00:00" Then
                'SqlStr = SqlStr & vbCrLf & " AND TO_DATE(TIME_TO,'DD-MON-YYYY HH24:MI')<=TO_DATE('" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 8, CDate(mEmpInTime)), "DD-MMM-YYYY hh:MM") & "','DD-MON-YYYY HH24:MI')"
                SqlStr = SqlStr & vbCrLf & " AND TIME_TO<=TO_DATE('" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 8, CDate(mEmpInTime)), "DD-MMM-YYYY hh:MM") & "','DD-MON-YYYY HH24:MI')"
            End If

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDBNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        Else  ''MAX(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI'))
            SqlStr = " SELECT MAX(TIME_TO) AS TIME_TO " & vbCrLf _
                & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDBNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        End If

        If VB6.Format(mEmpInTime, "HH:MM") = "00:00" And VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
            If mIsODLocal1 = True Then
                If VB6.Format(mEMPODOut, "HH:MM") <= VB6.Format(mEmpShiftBreak, "HH:MM") And VB6.Format(mEmpODIn, "HH:MM") <= VB6.Format(mEmpShiftBreak, "HH:MM") Then
                    mFirstIsOD = True
                    mEmpInTime = mEMPODOut
                Else
                    If CDate(mEMPODOut) <= CDate(mEmpShiftBreak) Then ''If Format(mEMPODOut, "HH:MM") <= Format(mEmpShiftBreak, "HH:MM") Then
                        mFirstIsOD = True
                        mEmpInTime = mEMPODOut
                    Else
                        mFirstIsOD = False
                    End If
                End If

                If CDate(mEmpODIn) > CDate(mEmpShiftBreak) Then ''If Format(mEmpODIn, "HH:MM") > Format(mEmpShiftBreak, "HH:MM") Then
                    mSecondIsOD = True
                    mEmpOutTime = mEmpODIn
                Else
                    mSecondIsOD = False
                End If
            Else
                mFirstIsOD = False
            End If
        Else
            If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Then
                '        mEmpInTime = mEMPODOut
                mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
                mEmpInTime = IIf(mIsODLocal1 = True, mEMPODOut, mEmpInTime)
            Else
                If VB6.Format(mEMPODOut, "HH:MM") <> "00:00" Then
                    If CDate(mEMPODOut) < CDate(mEmpInTime) Then
                        mEmpInTime = mEMPODOut '26/11/2020
                        mFirstIsOD = True
                    End If
                End If
            End If

            If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
                '        mEmpOutTime = mEmpODIn
                mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
                mEmpOutTime = IIf(mIsODLocal2 = True, mEmpODIn, mEmpOutTime)
            Else
                If VB6.Format(mEmpODIn, "HH:MM") <> "00:00" Then
                    If CDate(mEmpODIn) > CDate(mEmpOutTime) Then
                        mEmpOutTime = mEmpODIn '26/11/2020
                        mSecondIsOD = True
                    End If
                End If
            End If
        End If

        '    If Format(mEmpInTime, "HH:MM") = "00:00" Then
        ''        mEmpInTime = mEMPODOut
        '        mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
        '    End If
        '
        '    If Format(mEmpOutTime, "HH:MM") = "00:00" Then
        ''        mEmpOutTime = mEmpODIn
        '        mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
        '    End If
        '
        '    If Format(mEMPODOut, "HH:MM") <> "00:00" Then
        '        If CVDate(mEMPODOut) < CVDate(mEmpInTime) Then
        ''            mEmpInTime = mEMPODOut
        '            mFirstIsOD = True
        '        End If
        '    End If
        '
        '    If Format(mEmpODIn, "HH:MM") <> "00:00" Then
        '        If CVDate(mEmpODIn) > CVDate(mEmpOutTime) Then
        ''            mEmpOutTime = mEmpODIn
        '            mSecondIsOD = True
        '        End If
        '    End If

        CheckEmpTime = True
        Exit Function
ErrPart:
        '    Resume
        CheckEmpTime = False

    End Function

    Private Sub FillGridColor()

        On Error GoTo ErrPart
        Dim mEmpCode As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mBlackColor As Integer
        Dim mIO As String
        Dim mGateTime As String
        'Dim mGateTime As String
        Dim mShiftTime As String
        Dim mLastDay As Integer
        Dim mDay As Integer
        Dim mDate As String
        Dim mMarginsMinute As Double
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mISLeave As Boolean
        Dim mCPLEarn As Boolean
        Dim mCPLAvail As Boolean
        Dim mIsOD As Boolean
        Dim mISShortLeave As Boolean
        Dim mLateComer As Boolean
        Dim mFromTime As String
        Dim mToTime As String
        Dim mShiftBreakeTime As String
        Dim mHoliday As Double
        Dim mLeave As Double
        Dim mNotPunch As Double
        Dim mLC As Double
        Dim mSL As Double
        Dim mODuty As Double
        Dim mCPLEarnCnt As Double
        Dim mCPLAvailCnt As Double

        mMarginsMinute = IIf(IsDBNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)

        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        With sprdAttn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColIO
                mIO = Trim(.Text)

                .Col = ColCard
                If mEmpCode <> Trim(.Text) Then
                    mEmpCode = Trim(.Text)
                End If
                If mEmpCode = "" Then GoTo NextRecd

                mHoliday = 0
                mLeave = 0
                mNotPunch = 0
                mLC = 0
                mSL = 0
                mODuty = 0
                mCPLEarnCnt = 0
                mCPLAvailCnt = 0

                mDay = 1
                For cntCol = ColDay1 To ColDay31
                    .Row = cntRow
                    .Col = cntCol
                    mGateTime = Trim(.Text)
                    mGateTime = IIf(mGateTime = "", "00:00", mGateTime)


                    If mDay <= mLastDay Then
                        mDate = VB6.Format(mDay & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")
                        mShiftTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, mIO, "E")

                        mISLeave = False
                        mIsOD = False
                        mISShortLeave = False
                        mFromTime = ""
                        mToTime = ""
                        If GetIsHolidays(mDate, "", mEmpCode, "", "Y") = True Then
                            .Row = cntRow
                            .Row2 = cntRow
                            .Col = cntCol
                            .Col2 = cntCol
                            .BlockMode = True
                            .BackColor = lblColor(3).BackColor ''mBlackColor            ''&HFFFF00
                            .BlockMode = False
                            mHoliday = mHoliday + 0.5
                        Else
                            .Col = cntCol
                            mISLeave = CheckLeave(mEmpCode, mDate, "L", mIO, mGateTime, mShiftBreakeTime, "", "")
                            mCPLEarn = CheckLeave(mEmpCode, mDate, "CE", mIO, mGateTime, mShiftBreakeTime, "", "")
                            mCPLAvail = CheckLeave(mEmpCode, mDate, "CA", mIO, mGateTime, mShiftBreakeTime, "", "")
                            If mISLeave = True Or mCPLEarn = True Or mCPLAvail = True Then
                                .Row = cntRow
                                .Row2 = cntRow
                                .Col = cntCol
                                .Col2 = cntCol
                                .BlockMode = True
                                If mISLeave = True Then
                                    .BackColor = lblColor(4).BackColor
                                    mLeave = mLeave + 0.5
                                ElseIf mCPLEarn = True Then
                                    .BackColor = lblColor(6).BackColor
                                    mCPLEarnCnt = mCPLEarnCnt + 0.5
                                Else
                                    .BackColor = lblColor(7).BackColor
                                    mCPLAvailCnt = mCPLAvailCnt + 0.5
                                End If
                                .BlockMode = False
                            Else
                                mShiftBreakeTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, "B", "E")
                                mIsOD = CheckLeave(mEmpCode, mDate, "O", mIO, mGateTime, mShiftBreakeTime, mFromTime, mToTime)
                                If mIsOD = True Then
                                    .Row = cntRow
                                    .Row2 = cntRow
                                    .Col = cntCol
                                    .Col2 = cntCol
                                    .BlockMode = True
                                    .BackColor = lblColor(1).BackColor
                                    .BlockMode = False
                                    mODuty = mODuty + 0.5
                                Else
                                    mShiftBreakeTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, "B", "E")
                                    mISShortLeave = CheckLeave(mEmpCode, mDate, "P", mIO, mGateTime, mShiftBreakeTime, mFromTime, mToTime)
                                    If mISShortLeave = True Then
                                        .Row = cntRow
                                        .Row2 = cntRow
                                        .Col = cntCol
                                        .Col2 = cntCol
                                        .BlockMode = True
                                        .BackColor = lblColor(5).BackColor
                                        .BlockMode = False
                                        mSL = mSL + 0.5
                                    Else
                                        If mIO = "I" Then
                                            mShiftBreakeTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, "B", "E")
                                            If CDate(mGateTime) > CDate(mShiftTime) Then
                                                If CDate(mGateTime) <= CDate(VB6.Format(TimeSerial(Hour(CDate(mShiftBreakeTime)) - 2, Minute(CDate(mShiftBreakeTime)) - 30, 0), "HH:MM")) Then
                                                    .Row = cntRow
                                                    .Row2 = cntRow
                                                    .Col = cntCol
                                                    .Col2 = cntCol
                                                    .BlockMode = True
                                                    .BackColor = lblColor(2).BackColor
                                                    .BlockMode = False
                                                    mLC = mLC + 0.5
                                                Else
                                                    .Row = cntRow
                                                    .Row2 = cntRow
                                                    .Col = cntCol
                                                    .Col2 = cntCol
                                                    .BlockMode = True
                                                    .BackColor = lblColor(0).BackColor
                                                    .BlockMode = False
                                                    mNotPunch = mNotPunch + 0.5
                                                End If
                                            ElseIf mGateTime = "00:00" Then
                                                .Row = cntRow
                                                .Row2 = cntRow
                                                .Col = cntCol
                                                .Col2 = cntCol
                                                .BlockMode = True
                                                .BackColor = lblColor(0).BackColor
                                                .BlockMode = False
                                                mNotPunch = mNotPunch + 0.5
                                            End If
                                        Else
                                            If mGateTime = "00:00" Then
                                                .Row = cntRow
                                                .Row2 = cntRow
                                                .Col = cntCol
                                                .Col2 = cntCol
                                                .BlockMode = True
                                                .BackColor = lblColor(0).BackColor
                                                .BlockMode = False
                                                mNotPunch = mNotPunch + 0.5
                                            ElseIf CDate(mGateTime) < CDate(mShiftTime) Then
                                                If CDate(mGateTime) > CDate(VB6.Format(TimeSerial(Hour(CDate(mShiftBreakeTime)), Minute(CDate(mShiftBreakeTime)), 0), "HH:MM")) And CDate(mGateTime) >= CDate(VB6.Format(TimeSerial(Hour(CDate(mShiftTime)) - 2, Minute(CDate(mShiftTime)), 0), "HH:MM")) Then
                                                    .Row = cntRow
                                                    .Row2 = cntRow
                                                    .Col = cntCol
                                                    .Col2 = cntCol
                                                    .BlockMode = True
                                                    .BackColor = lblColor(2).BackColor
                                                    mLC = mLC + 0.5
                                                    .BlockMode = False
                                                Else
                                                    .Row = cntRow
                                                    .Row2 = cntRow
                                                    .Col = cntCol
                                                    .Col2 = cntCol
                                                    .BlockMode = True
                                                    .BackColor = lblColor(0).BackColor
                                                    .BlockMode = False
                                                    mNotPunch = mNotPunch + 0.5
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                    mDay = mDay + 1
                Next

                .Row = cntRow
                .Col = ColHoliday
                .Text = VB6.Format(mHoliday, "0.0")

                .Col = ColLeave
                .Text = VB6.Format(mLeave, "0.0")

                .Col = ColNotPunch
                .Text = VB6.Format(mNotPunch, "0.0")

                .Col = ColLC
                .Text = VB6.Format(mLC, "0.0")

                .Col = ColSL
                .Text = VB6.Format(mSL, "0.0")

                .Col = ColOD
                .Text = VB6.Format(mODuty, "0.0")

                .Col = ColPresent
                .Text = VB6.Format(mCPLEarnCnt, "0.0")

                .Col = ColWorking
                .Text = VB6.Format(mCPLAvailCnt, "0.0")

                mHoliday = 0
                mLeave = 0
                mNotPunch = 0
                mLC = 0
                mSL = 0
                mODuty = 0
                mCPLEarnCnt = 0
                mCPLAvailCnt = 0

NextRecd:
            Next
        End With
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Function CheckLeave(ByRef pEmpCode As String, ByRef pDate As String, ByRef pCheckType As String, ByRef pHalf As String, ByRef mOutTime As String, ByRef mShiftBreakeTime As String, ByRef pFromDate As String, ByRef pToDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pField As String

        pFromDate = ""
        pToDate = ""

        If pCheckType = "H" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf _
                & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " IN (" & HOLIDAY & ")"

        ElseIf pCheckType = "PR" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " IN (" & PRESENT & ")"
        ElseIf pCheckType = "L" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " NOT IN (-1," & CPLEARN & "," & CPLAVAIL & "," & ABSENT & "," & WOPAY & "," & PRESENT & ")"
        ElseIf pCheckType = "SU" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " IN (" & SUNDAY & "," & HOLIDAY & ")"
        ElseIf pCheckType = "AB" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " IN (" & ABSENT & "," & WOPAY & ")"
        ElseIf pCheckType = "CE" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" ''" & pField & " = " & CPLEARN & ""

            If pHalf = "I" Then
                SqlStr = SqlStr & "  AND CPL_EARN=1"
            Else
                SqlStr = SqlStr & "  AND CPL_EARN=2"
            End If

        ElseIf pCheckType = "CA" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf _
                & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf _
                & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & "= " & CPLAVAIL & ""
        ElseIf pCheckType = "O" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='O'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If
            If pHalf = "I" Then
                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'"
            Else
                If mOutTime = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_TO,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')"
                End If
            End If
        ElseIf pCheckType = "M" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='M'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If
            If pHalf = "I" Then
                '            SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'"
                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "DD-MMM-YYYY HH:MM") & "'"
            Else
                If VB6.Format(mOutTime, "HH:MM") = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_TO,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')"
                End If
            End If
        ElseIf pCheckType = "P" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='P' AND AGT_LEAVE='N'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If

            If pHalf = "I" Then
                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'"
            Else
                If mOutTime = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_TO,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')"
                End If
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If pCheckType = "H" Or pCheckType = "PR" Or pCheckType = "L" Or pCheckType = "CA" Or pCheckType = "CE" Or pCheckType = "AB" Or pCheckType = "SU" Then

            Else
                pFromDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_FROM").Value), "", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
                pToDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "", RsTemp.Fields("TIME_TO").Value), "HH:MM")
            End If

            CheckLeave = True
        Else
            CheckLeave = False
        End If
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Function
    Private Sub FillGridColorOld()

        On Error GoTo ErrPart
        Dim mEmpCode As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mBlackColor As Integer
        Dim mIO As String
        Dim mGateTime As String
        'Dim mGateTime As String
        Dim mShiftTime As String
        Dim mLastDay As Integer
        Dim mDay As Integer
        Dim mDate As String
        Dim mMarginsMinute As Double
        Dim RsTemp As ADODB.Recordset = Nothing

        mMarginsMinute = IIf(IsDBNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)

        mLastDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        With sprdAttn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColIO
                mIO = Trim(.Text)

                .Col = ColCard
                If mEmpCode <> Trim(.Text) Then
                    mEmpCode = Trim(.Text)
                End If
                mDay = 1
                For cntCol = ColDay1 To ColDay31
                    If mDay <= mLastDay Then
                        mDate = mDay & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

                        If GetIsHolidays(mDate, "", mEmpCode, "", "Y") = True Then
                            .Row = cntRow
                            .Row2 = cntRow
                            .Col = cntCol
                            .Col2 = cntCol
                            .BlockMode = True
                            .BackColor = lblColor(3).BackColor ''mBlackColor            ''&HFFFF00
                            .BlockMode = False
                        Else
                            .Col = cntCol
                            mGateTime = Trim(.Text)
                            mGateTime = IIf(mGateTime = "", "00:00", mGateTime)
                            mShiftTime = GetShiftTime(mEmpCode, mDate, mMarginsMinute, mIO, "E")

                            If mIO = "I" Then
                                If mGateTime = "00:00" Then
                                    SqlStr = " SELECT FIRSTHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "' AND AGT_LATE='N'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND FIRSTHALF<>-1"
                                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                                    If RsTemp.EOF = False Then
                                        .Row = cntRow
                                        .Row2 = cntRow
                                        .Col = cntCol
                                        .Col2 = cntCol
                                        .BlockMode = True
                                        .BackColor = lblColor(4).BackColor
                                        .BlockMode = False
                                    Else
                                        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='O'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
                                        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                                            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
                                        End If

                                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
                                        If RsTemp.EOF = False Then
                                            .Row = cntRow
                                            .Row2 = cntRow
                                            .Col = cntCol
                                            .Col2 = cntCol
                                            .BlockMode = True
                                            .BackColor = lblColor(1).BackColor
                                            .BlockMode = False
                                        Else
                                            .Row = cntRow
                                            .Row2 = cntRow
                                            .Col = cntCol
                                            .Col2 = cntCol
                                            .BlockMode = True
                                            .BackColor = lblColor(0).BackColor
                                            .BlockMode = False
                                        End If
                                    End If
                                Else
                                    If CDate(mGateTime) > CDate(mShiftTime) Then
                                        .Row = cntRow
                                        .Row2 = cntRow
                                        .Col = cntCol
                                        .Col2 = cntCol
                                        .BlockMode = True
                                        .BackColor = lblColor(2).BackColor
                                        .BlockMode = False
                                    End If
                                End If
                            Else

                                SqlStr = " SELECT SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mEmpCode & "' AND AGT_LATE='N'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SECONDHALF<>-1"
                                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                                If RsTemp.EOF = False Then
                                    .Row = cntRow
                                    .Row2 = cntRow
                                    .Col = cntCol
                                    .Col2 = cntCol
                                    .BlockMode = True
                                    .BackColor = lblColor(4).BackColor
                                    .BlockMode = False
                                Else
                                    If mGateTime = "00:00" Then
                                        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='O'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
                                        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                                            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
                                        End If

                                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
                                        If RsTemp.EOF = False Then
                                            .Row = cntRow
                                            .Row2 = cntRow
                                            .Col = cntCol
                                            .Col2 = cntCol
                                            .BlockMode = True
                                            .BackColor = lblColor(1).BackColor
                                            .BlockMode = False
                                        Else
                                            .Row = cntRow
                                            .Row2 = cntRow
                                            .Col = cntCol
                                            .Col2 = cntCol
                                            .BlockMode = True
                                            .BackColor = lblColor(0).BackColor
                                            .BlockMode = False
                                        End If
                                    Else
                                        If CDate(mGateTime) < CDate(mShiftTime) Then
                                            .Row = cntRow
                                            .Row2 = cntRow
                                            .Col = cntCol
                                            .Col2 = cntCol
                                            .BlockMode = True
                                            .BackColor = lblColor(2).BackColor
                                            .BlockMode = False
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                    mDay = mDay + 1
                Next
            Next
        End With
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        SqlStr = "Select DEPT_DESC, DEPT_CODE " & vbCrLf _
            & " FROM PAY_DEPT_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " Order by DEPT_DESC"
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        'cboDept.Items.Clear()
        'If RsDept.EOF = False Then
        '    Do While Not RsDept.EOF
        '        cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
        '        RsDept.MoveNext()
        '    Loop
        'End If
        'cboDept.SelectedIndex = 0

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboDept.DataSource = ds
        cboDept.DataMember = ""
        Dim c As UltraGridColumn = Me.cboDept.DisplayLayout.Bands(0).Columns.Add()
        With c
            .Key = "Selected"
            .Header.Caption = String.Empty
            .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
            .DataType = GetType(Boolean)
            .DataType = GetType(Boolean)
            .Header.VisiblePosition = 0
        End With
        cboDept.CheckedListSettings.CheckStateMember = "Selected"
        cboDept.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        ' Set up the control to use a custom list delimiter 
        cboDept.CheckedListSettings.ListSeparator = " , "
        ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        cboDept.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        cboDept.DisplayMember = "DEPT_DESC"
        cboDept.ValueMember = "DEPT_CODE"

        cboDept.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Dept Name"
        cboDept.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Dept Code"
        'cboDepartment.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
        'cboDepartment.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
        'cboDepartment.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"

        cboDept.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboDept.DisplayLayout.Bands(0).Columns(1).Width = 100
        'cboDepartment.DisplayLayout.Bands(0).Columns(2).Width = 350
        'cboDepartment.DisplayLayout.Bands(0).Columns(3).Width = 100
        'cboDepartment.DisplayLayout.Bands(0).Columns(4).Width = 100

        cboDept.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown



        oledbAdapter.Dispose()
        oledbCnn.Close()


        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCatgeory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCatgeory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCatgeory.SelectedIndex = 0

        '    cboCatgeory.Clear
        '    cboCatgeory.AddItem "General Staff"
        '    cboCatgeory.AddItem "Production Staff"
        '    cboCatgeory.AddItem "Export Staff"
        '    cboCatgeory.AddItem "Regular Worker"
        '    cboCatgeory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCatgeory.AddItem "Director"
        '    cboCatgeory.AddItem "Trainee Staff"
        '    cboCatgeory.ListIndex = 0

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("ALL")
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = 0


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub sprdAttn_DblClick(sender As Object, EventArgs As _DSpreadEvents_DblClickEvent) Handles sprdAttn.DblClick
        On Error GoTo ErrPart
        Dim mCode As String
        Dim mDate As String
        Dim mValue As String
        Dim mIO As String
        Dim mName As String

        If EventArgs.row = 0 Or EventArgs.col = 0 Then Exit Sub


        If PubUserLevel = 1 Then

        Else
            Exit Sub
        End If


        sprdAttn.Row = 0
        sprdAttn.Col = EventArgs.col
        mValue = Val(sprdAttn.Text)



        sprdAttn.Row = EventArgs.row
        sprdAttn.Col = ColCard
        mCode = sprdAttn.Text

        sprdAttn.Col = ColName
        mName = sprdAttn.Text

        If Trim(sprdAttn.Text) = "" Then Exit Sub

        sprdAttn.Row = EventArgs.row
        sprdAttn.Col = ColIO
        mIO = Trim(sprdAttn.Text)

        If EventArgs.col = ColCard Then

            frmEmpLeaveEntry.MdiParent = Me.MdiParent
            frmEmpLeaveEntry.Show()
            frmEmpLeaveEntry.frmEmpLeaveEntry_Activated(Nothing, New System.EventArgs())

            frmEmpLeaveEntry.txtEmpCode.Text = mCode

            frmEmpLeaveEntry.txtRefDate.Text = "01" & VB6.Format(lblRunDate.Text, "MM/YYYY")
            frmEmpLeaveEntry.TxtEmpName.Text = mName

            frmEmpLeaveEntry.txtEmpCode_Validating(frmEmpLeaveEntry.txtEmpCode, New System.ComponentModel.CancelEventArgs(False))

            'frmEmpLeaveEntry.ShowDialog()

            frmEmpLeaveEntry.Activate()


        End If

        If mValue <= 0 Then Exit Sub

        If mIO = "I" Or mIO = "O" Then
            frmAttnInOutMark.lblCode.Text = mCode
            frmAttnInOutMark.lblEmpName.Text = mName
            mDate = VB6.Format(VB6.Format(mValue, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

            sprdAttn.Row = EventArgs.row + IIf(mIO = "O", -1, 0)
            sprdAttn.Col = EventArgs.col
            frmAttnInOutMark.txtINTime.Text = sprdAttn.Text

            sprdAttn.Row = EventArgs.row + IIf(mIO = "I", 1, 0)
            sprdAttn.Col = EventArgs.col
            frmAttnInOutMark.txtOUTTime.Text = sprdAttn.Text

            frmAttnInOutMark.lblDate.Text = mDate

            frmAttnInOutMark.ShowDialog()
        ElseIf mIO = "OT" Then
            frmOverTimeHead.lblCode.Text = mCode
            frmOverTimeHead.lblEmpName.Text = mName

            mDate = VB6.Format(VB6.Format(mValue, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY"), "DD/MM/YYYY")

            frmOverTimeHead.lblDate.Text = mDate

            If ChechJoinLeaveDate(mDate, mCode) = False Then Exit Sub

            frmOverTimeHead.lblType.Text = "1"
            frmOverTimeHead.ShowDialog()
        End If


        Exit Sub
ErrPart:

    End Sub
    Private Function ChechJoinLeaveDate(ByRef mDays As String, ByRef mCode As String) As Boolean

        Dim SqlStr As String = ""
        Dim RsTempJL As ADODB.Recordset = Nothing

        SqlStr = " SELECT EMP_DOJ,EMP_LEAVE_DATE FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE EMP_CODE = '" & mCode & "' And " & vbCrLf _
            & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempJL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTempJL.EOF = False Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(RsTempJL.Fields("EMP_DOJ").Value, "dd/mm/yyyy")), CDate(VB6.Format(mDays, "dd/mm/yyyy"))) >= 0 Then
                ChechJoinLeaveDate = True
            Else
                MsgInformation("Employee Joining Date is Greater then Current Date.")
                ChechJoinLeaveDate = False
                Exit Function
            End If
            If IsDBNull(RsTempJL.Fields("EMP_LEAVE_DATE").Value) Then
                ChechJoinLeaveDate = True
            ElseIf DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(mDays, "dd/mm/yyyy")), CDate(VB6.Format(RsTempJL.Fields("EMP_LEAVE_DATE").Value, "dd/mm/yyyy"))) < 0 Then
                MsgInformation("Employee Leaving Date is Less then Current Date.")
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
    Private Function GetSelectAttnQry(ByVal mDOJ As String, ByVal mDOL As String, ByVal mFromDate As String, ByVal mToDate As String, ByVal AttnType As String) As String

        On Error GoTo refreshErrPart
        Dim mDeptName As String
        Dim mFieldName As String

        'If AttnType = "f" Then
        '    mFieldName = "ATTN.FIRSTHALF"
        'Else
        '    mFieldName = "ATTN.SECONDHALF"
        'End If

        'mFieldName = " CASE WHEN SECONDHALF = -1 THEN '' " & vbCrLf _
        '        & " WHEN SECONDHALF = 0 THEN 'UNAPPROVED' " & vbCrLf _
        '        & " WHEN SECONDHALF = 1 THEN 'CASUAL' " & vbCrLf _
        '        & " WHEN SECONDHALF = 2 THEN 'EARN' " & vbCrLf _
        '        & " WHEN SECONDHALF = 3 THEN 'SICK' " & vbCrLf _
        '        & " WHEN SECONDHALF = 4 THEN 'MATERNITY' " & vbCrLf _
        '        & " WHEN SECONDHALF = 5 THEN 'CPLEARN' " & vbCrLf _
        '        & " WHEN SECONDHALF = 6 THEN 'APPROVED' " & vbCrLf _
        '        & " WHEN SECONDHALF = 7 THEN 'CPLAVAIL' " & vbCrLf _
        '        & " WHEN SECONDHALF = 8 THEN 'SUNDAY' " & vbCrLf _
        '        & " WHEN SECONDHALF = 9 THEN 'HOLIDAY' " & vbCrLf _
        '        & " WHEN SECONDHALF = 10 THEN 'PRESENT' " & vbCrLf _
        '        & " WHEN SECONDHALF = 11 THEN 'WFH' " & vbCrLf _
        '        & " ELSE '' " & vbCrLf _
        '        & " END"

        'If AttnType = "PF" Then
        mFieldName = " CASE WHEN FIRSTHALF = -1 THEN '' " & vbCrLf _
                & " WHEN FIRSTHALF = 0 THEN 'AB' " & vbCrLf _
                & " WHEN FIRSTHALF = 1 THEN 'CL' " & vbCrLf _
                & " WHEN FIRSTHALF = 2 THEN 'EL' " & vbCrLf _
                & " WHEN FIRSTHALF = 3 THEN 'SL' " & vbCrLf _
                & " WHEN FIRSTHALF = 4 THEN 'ML' " & vbCrLf _
                & " WHEN FIRSTHALF = 5 THEN 'CE' " & vbCrLf _
                & " WHEN FIRSTHALF = 6 THEN 'AL' " & vbCrLf _
                & " WHEN FIRSTHALF = 7 THEN 'CA' " & vbCrLf _
                & " WHEN FIRSTHALF = 8 THEN 'SU' " & vbCrLf _
                & " WHEN FIRSTHALF = 9 THEN 'HD' " & vbCrLf _
                & " WHEN FIRSTHALF = 10 THEN 'PR' " & vbCrLf _
                & " WHEN FIRSTHALF = 11 THEN 'WF' " & vbCrLf _
                & " ELSE '' " & vbCrLf _
                & " END"
        'Else
        mFieldName = mFieldName & "|| ',' || " & vbCrLf _
                & " CASE WHEN SECONDHALF = -1 THEN '' " & vbCrLf _
                & " WHEN SECONDHALF = 0 THEN 'AB' " & vbCrLf _
                & " WHEN SECONDHALF = 1 THEN 'CL' " & vbCrLf _
                & " WHEN SECONDHALF = 2 THEN 'EL' " & vbCrLf _
                & " WHEN SECONDHALF = 3 THEN 'SL' " & vbCrLf _
                & " WHEN SECONDHALF = 4 THEN 'ML' " & vbCrLf _
                & " WHEN SECONDHALF = 5 THEN 'CE' " & vbCrLf _
                & " WHEN SECONDHALF = 6 THEN 'AL' " & vbCrLf _
                & " WHEN SECONDHALF = 7 THEN 'CA' " & vbCrLf _
                & " WHEN SECONDHALF = 8 THEN 'SU' " & vbCrLf _
                & " WHEN SECONDHALF = 9 THEN 'HD' " & vbCrLf _
                & " WHEN SECONDHALF = 10 THEN 'PR' " & vbCrLf _
                & " WHEN SECONDHALF = 11 THEN 'WF' " & vbCrLf _
                & " ELSE '' " & vbCrLf _
                & " END"
        'End If

        GetSelectAttnQry = " SELECT EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC, '" & AttnType & "' AS IO, " & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='01' THEN " & mFieldName & " END DAY_1," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='02' THEN " & mFieldName & " END DAY_2," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='03' THEN " & mFieldName & " END DAY_3," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='04' THEN " & mFieldName & " END DAY_4," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='05' THEN " & mFieldName & " END DAY_5," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='06' THEN " & mFieldName & " END DAY_6," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='07' THEN " & mFieldName & " END DAY_7," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='08' THEN " & mFieldName & " END DAY_8," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='09' THEN " & mFieldName & " END DAY_9," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='10' THEN " & mFieldName & " END DAY_10," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='11' THEN " & mFieldName & " END DAY_11," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='12' THEN " & mFieldName & " END DAY_12," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='13' THEN " & mFieldName & " END DAY_13," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='14' THEN " & mFieldName & " END DAY_14," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='15' THEN " & mFieldName & " END DAY_15," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='16' THEN " & mFieldName & " END DAY_16," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='17' THEN " & mFieldName & " END DAY_17," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='18' THEN " & mFieldName & " END DAY_18," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='19' THEN " & mFieldName & " END DAY_19," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='20' THEN " & mFieldName & " END DAY_20," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='21' THEN " & mFieldName & " END DAY_21," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='22' THEN " & mFieldName & " END DAY_22," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='23' THEN " & mFieldName & " END DAY_23," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='24' THEN " & mFieldName & " END DAY_24," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='25' THEN " & mFieldName & " END DAY_25," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='26' THEN " & mFieldName & " END DAY_26," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='27' THEN " & mFieldName & " END DAY_27," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='28' THEN " & mFieldName & " END DAY_28," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='29' THEN " & mFieldName & " END DAY_29," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='30' THEN " & mFieldName & " END DAY_30," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.ATTN_DATE,'DD')='31' THEN " & mFieldName & " END DAY_31," & vbCrLf _
                & " '' HOLIDAY1," & vbCrLf _
                & " '' LEAVE," & vbCrLf _
                & " '' NOTPUNCH," & vbCrLf _
                & " '' LC, '' SHORT_LEAVE, '' OVER_TIME," & vbCrLf _
                & " '' OD," & vbCrLf _
                & " '' CPLE," & vbCrLf _
                & " '' CPLA," & vbCrLf _
                & " '' WORKING_HOURS," & vbCrLf _
                & " '' SL_HOURS," & vbCrLf _
                & " '' ABSENT1, EMP_HOD_CODE "
        '
        GetSelectAttnQry = GetSelectAttnQry & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_ATTN_mst ATTN, PAY_DEPT_MST DEPT " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        GetSelectAttnQry = GetSelectAttnQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =DEPT.COMPANY_CODE" & vbCrLf _
                & " And EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE "

        GetSelectAttnQry = GetSelectAttnQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =ATTN.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=ATTN.EMP_CODE(+) "


        GetSelectAttnQry = GetSelectAttnQry & vbCrLf _
            & " AND ATTN.ATTN_DATE(+) >=TO_DATE('" & VB6.Format(mFromDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ATTN.ATTN_DATE(+) <=TO_DATE('" & VB6.Format(mToDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "

        GetSelectAttnQry = GetSelectAttnQry & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        'GetSelectAttnQry = GetSelectAttnQry & vbCrLf & " AND EMP.COMPANY_CODE = SMST.COMPANY_CODE " & vbCrLf _
        '    & " AND EMP.EMP_CODE = SMST.EMP_CODE " & vbCrLf & " AND TO_CHAR(SMST.SHIFT_DATE,'YYYYMM')= '" & VB6.Format(mDOJ, "YYYYMM") & "'"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            GetSelectAttnQry = GetSelectAttnQry & vbCrLf & " And (EMP.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP.EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            GetSelectAttnQry = GetSelectAttnQry & vbCrLf & " AND EMP.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            GetSelectAttnQry = GetSelectAttnQry & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If cboDept.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboDept.CheckedRows
                If mDeptName <> "" Then
                    mDeptName += "," & "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                Else
                    mDeptName += "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                End If
            Next
        End If

        If mDeptName <> "" Then
            GetSelectAttnQry = GetSelectAttnQry & vbCrLf & " AND EMP.EMP_DEPT_CODE IN (" & mDeptName & ")"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
            GetSelectAttnQry = GetSelectAttnQry & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
        End If

        'If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBookNo.Text) <> "" Then
        '    GetSelectAttnQry = GetSelectAttnQry & vbCrLf & "AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
        'End If

        'If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPageNo.Text) <> "" Then
        '    GetSelectAttnQry = GetSelectAttnQry & vbCrLf & "AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
        'End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            GetSelectAttnQry = GetSelectAttnQry & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function GetSelectRemarksQry(ByVal mDOJ As String, ByVal mDOL As String, ByVal mFromDate As String, ByVal mToDate As String, ByVal AttnType As String) As String

        On Error GoTo refreshErrPart
        Dim mDeptName As String
        Dim mFieldName As String


        mFieldName = "LISTAGG(CASE WHEN MOVE_TYPE='O' THEN 'OD' WHEN MOVE_TYPE='P' THEN 'SHORT LEAVE' ELSE 'MANUAL' END, ', ') WITHIN GROUP (ORDER BY ATTN.REF_DATE)"


        'SqlStr = SqlStr & vbCrLf _
        '                & " NVL((SELECT LISTAGG(CASE WHEN MOVE_TYPE='O' THEN 'OD' WHEN MOVE_TYPE='P' THEN 'SHORT LEAVE' ELSE 'MANUAL' END, ', ') WITHIN GROUP (ORDER BY TIME_FROM) FROM PAY_MOVEMENT_TRN WHERE COMPANY_CODE=EMP.COMPANY_CODE AND EMP_CODE=EMP.EMP_CODE AND REF_DATE=DAY AND HR_APPROVAL='Y'),'') AS REMARKS"


        GetSelectRemarksQry = " SELECT EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC, '" & AttnType & "' AS IO, " & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='01' THEN " & mFieldName & " END DAY_1," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='02' THEN " & mFieldName & " END DAY_2," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='03' THEN " & mFieldName & " END DAY_3," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='04' THEN " & mFieldName & " END DAY_4," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='05' THEN " & mFieldName & " END DAY_5," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='06' THEN " & mFieldName & " END DAY_6," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='07' THEN " & mFieldName & " END DAY_7," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='08' THEN " & mFieldName & " END DAY_8," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='09' THEN " & mFieldName & " END DAY_9," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='10' THEN " & mFieldName & " END DAY_10," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='11' THEN " & mFieldName & " END DAY_11," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='12' THEN " & mFieldName & " END DAY_12," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='13' THEN " & mFieldName & " END DAY_13," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='14' THEN " & mFieldName & " END DAY_14," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='15' THEN " & mFieldName & " END DAY_15," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='16' THEN " & mFieldName & " END DAY_16," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='17' THEN " & mFieldName & " END DAY_17," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='18' THEN " & mFieldName & " END DAY_18," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='19' THEN " & mFieldName & " END DAY_19," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='20' THEN " & mFieldName & " END DAY_20," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='21' THEN " & mFieldName & " END DAY_21," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='22' THEN " & mFieldName & " END DAY_22," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='23' THEN " & mFieldName & " END DAY_23," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='24' THEN " & mFieldName & " END DAY_24," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='25' THEN " & mFieldName & " END DAY_25," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='26' THEN " & mFieldName & " END DAY_26," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='27' THEN " & mFieldName & " END DAY_27," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='28' THEN " & mFieldName & " END DAY_28," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='29' THEN " & mFieldName & " END DAY_29," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='30' THEN " & mFieldName & " END DAY_30," & vbCrLf _
                & " CASE WHEN TO_CHAR(ATTN.REF_DATE,'DD')='31' THEN " & mFieldName & " END DAY_31," & vbCrLf _
                & " '' HOLIDAY1," & vbCrLf _
                & " '' LEAVE," & vbCrLf _
                & " '' NOTPUNCH," & vbCrLf _
                & " '' LC, '' SHORT_LEAVE, '' OVER_TIME," & vbCrLf _
                & " '' OD," & vbCrLf _
                & " '' CPLE," & vbCrLf _
                & " '' CPLA," & vbCrLf _
                & " '' WORKING_HOURS," & vbCrLf _
                & " '' SL_HOURS," & vbCrLf _
                & " '' ABSENT1, EMP_HOD_CODE "
        '
        GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_MOVEMENT_TRN ATTN, PAY_DEPT_MST DEPT " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HR_APPROVAL(+)='Y'"

        GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =DEPT.COMPANY_CODE" & vbCrLf _
                & " And EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE "

        GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf _
                & " AND EMP.COMPANY_CODE =ATTN.COMPANY_CODE(+)" & vbCrLf _
                & " And EMP.EMP_CODE=ATTN.EMP_CODE(+) "


        GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf _
            & " AND ATTN.REF_DATE(+) >=TO_DATE('" & VB6.Format(mFromDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND ATTN.REF_DATE(+) <=TO_DATE('" & VB6.Format(mToDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "

        GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        'GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & " AND EMP.COMPANY_CODE = SMST.COMPANY_CODE " & vbCrLf _
        '    & " AND EMP.EMP_CODE = SMST.EMP_CODE " & vbCrLf & " AND TO_CHAR(SMST.SHIFT_DATE,'YYYYMM')= '" & VB6.Format(mDOJ, "YYYYMM") & "'"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & " And (EMP.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP.EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & " AND EMP.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & " AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
        End If

        If cboDept.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboDept.CheckedRows
                If mDeptName <> "" Then
                    mDeptName += "," & "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                Else
                    mDeptName += "'" & r.Cells("DEPT_CODE").Value.ToString() & "'"
                End If
            Next
        End If

        If mDeptName <> "" Then
            GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & " AND EMP.EMP_DEPT_CODE IN (" & mDeptName & ")"
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCatgeory.SelectedIndex <> -1 Then
            GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCatgeory.Text, 1) & "' "
        End If

        'If chkBookNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBookNo.Text) <> "" Then
        '    GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & "AND SMST.BOOKNO=" & Val(txtBookNo.Text) & ""
        'End If

        'If chkPageNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPageNo.Text) <> "" Then
        '    GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & "AND SMST.PAGENO=" & Val(txtPageNo.Text) & ""
        'End If

        If UCase(Trim(cboEmpCatType.Text)) <> "ALL" Then
            GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & "AND EMP.EMP_CAT_TYPE='" & VB.Left(cboEmpCatType.Text, 1) & "' "
        End If

        GetSelectRemarksQry = GetSelectRemarksQry & vbCrLf & " GROUP BY EMP.EMP_CODE, EMP.EMP_NAME, DEPT.DEPT_DESC,ATTN.REF_DATE,EMP_HOD_CODE "
        ''
        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Function
End Class
