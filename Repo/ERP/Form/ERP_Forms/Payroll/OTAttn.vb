Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmOTAttn
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim RsAttn As ADODB.Recordset = Nothing
    Dim cntRow As Integer
    Dim cntCol As Integer
    Dim ecntRow As Integer
    Dim mCode As String
    Dim mWopay As Double
    Dim mLeave As Double
    Dim mDOJ As String
    Dim mDOL As String
    Dim mMonth As Short
    Dim mYear As Short
    Dim mThisMonAttn As Double
    Dim mJDays As Integer
    Dim mLDays As Integer
    Dim LastDayofMon As String
    Dim mOTRate As Double
    Dim mTOTOverTime As Double
    Dim mCurCol As Integer
    Dim mCurRow As Integer
    Dim mESIApp As Boolean
    Dim mBasicSalary As Double
    Dim mDate As Date
    Dim mESIRate As Double
    Dim RsEmp As ADODB.Recordset = Nothing
    Dim mOTAmount As Double
    Dim mIncentiveAmount As Double
    Dim mTotal As Double
    Dim mESIAmount As Double
    Dim mNetAmount As Double

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ConWorkDay As Short = 26
    Private Const ConWorkHour As Short = 8

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim FileDBCn As ADODB.Connection
    Private Sub FillHeading(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim cntCol As Integer
        Dim Tempdate As String

        Dim NewDate As Date

        MainClass.ClearGrid(sprdAttn)

        Tempdate = "01/" & Month(lblYear.Text) & "/" & Year(lblYear.Text)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        'lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(lblYear.Text, "mm"), VB6.Format(lblYear.Text, "yyyy"))
        With sprdAttn
            .MaxCols = 4

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Col = 0
            .Text = "S. No."
            .set_ColWidth(0, 5)

            .Col = 1
            .Text = "Emp Card No"
            .set_ColWidth(1, 7)

            .Col = 2
            .Text = "Employees' Name "
            .set_ColWidth(2, 25)
            .ColsFrozen = 2

            .Col = 3
            .Text = "Previous Month OT"
            .set_ColWidth(3, 10)

            .MaxCols = .MaxCols + Daysinmonth
            Do While cntCol <= Daysinmonth
                .Col = cntCol + 4
                .Text = VB6.Format(VB.Day(NewDate), "00") & vbNewLine & WeekDayName(WeekDay(NewDate, FirstDayOfWeek.Monday), False, FirstDayOfWeek.Monday)
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                NewDate = System.Date.FromOADate(NewDate.ToOADate + 1)
                cntCol = cntCol + 1
            Loop

            '        .MaxCols = .MaxCols + 1
            .Col = .MaxCols
            .Text = "Total"
            .set_ColWidth(.MaxCols, 8)

            .Row = -1
            For cntCol = 3 To .MaxCols
                .Col = cntCol
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            Next

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)
            MainClass.SetSpreadColor(sprdAttn, -1)
        End With
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFile.Click

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String
        Dim cntRow As Integer
        Dim mCol As Integer

        Dim cntField As Integer
        Dim mLastMonthDay As Integer
        Dim mDate As String
        Dim mFieldValue As Double
        Dim mHours As Integer
        Dim mMin As Integer

        Dim mPrevFieldValue As Double
        Dim mPrevHours As Integer
        Dim mPrevMin As Integer

        Dim strFilePath As String
        Dim strXLSFile As String

        '    MainClass.ClearGrid SprdMain
        '    FormatSprdMain -1




        mLastMonthDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            Exit Sub
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        strXLSFile = strFilePath
        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    mEmpCode = Trim(IIf(IsDbNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))
                    mEmpCode = VB6.Format(mEmpCode, "000000")

                    If mEmpCode <> "" Then
                        If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then


                            For cntCol = 1 To mLastMonthDay
                                cntField = cntCol + 2
                                mDate = VB6.Format(cntCol, "00") & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
                                mFieldValue = Val(IIf(IsDbNull(RsFile.Fields(cntField).Value), 0, RsFile.Fields(cntField).Value))
                                mHours = Int(mFieldValue)
                                mMin = (mFieldValue - Int(mFieldValue)) * 100

                                If cntCol = 1 Then
                                    mPrevFieldValue = Val(IIf(IsDbNull(RsFile.Fields(2).Value), 0, RsFile.Fields(2).Value))
                                    mPrevHours = Int(mPrevFieldValue)
                                    mPrevMin = (mPrevFieldValue - Int(mPrevFieldValue)) * 100
                                Else
                                    mPrevHours = 0
                                    mPrevMin = 0
                                End If
                                SqlStr = "DELETE FROM PAY_OVERTIME_MST  WHERE " & vbCrLf _
                                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                    & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                                    & " AND OT_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                                PubDBCn.Execute(SqlStr)

                                If mFieldValue + mPrevFieldValue > 0 Then
                                    SqlStr = "INSERT INTO PAY_OVERTIME_MST ( " & vbCrLf _
                                        & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf & " OT_DATE, OTHOUR, OTMIN, OTTYPE," & vbCrLf _
                                        & " PREV_OTHOUR,  PREV_OTMIN, " & vbCrLf & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
                                        & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Year(CDate(lblRunDate.Text)) & ", " & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(mEmpCode) & "', " & vbCrLf _
                                        & " TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                        & "  " & mHours & ", " & mMin & ", '0'," & vbCrLf & " " & mPrevHours & ", " & mPrevMin & ", " & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                                    PubDBCn.Execute(SqlStr)
                                End If
                            Next
                        End If
                    End If
                    RsFile.MoveNext()
                Loop
            End If
        End If

        PubDBCn.CommitTrans()

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        RefreshScreen()

        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub

    Private Sub cmdOTSlab_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOTSlab.Click

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mCol As Integer

        Dim cntField As Integer
        Dim mLastMonthDay As Integer
        Dim mDate As String
        Dim mFieldValue As Double
        Dim mOTFactor As Double
        Dim mHours As Integer
        Dim mMin As Integer

        Dim mPrevFieldValue As Double
        Dim mPrevHours As Integer
        Dim mPrevMin As Integer
        Dim mFromDate As String
        Dim mToDate As String
        Dim mOTMin As Long
        Dim mOTMInFactor As Long

        mOTMInFactor = IIf(IsDBNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)

        If lblCategory.Text = "W" Then
            If MsgQuestion("Want to Process Regular Workers Over Time. Once Process all data will be Updated.") = CStr(MsgBoxResult.No) Then Exit Sub
        Else
            If MsgQuestion("Want to Process Staff Over Time. Once Process all data will be Updated.") = CStr(MsgBoxResult.No) Then Exit Sub
        End If

        mLastMonthDay = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        mFromDate = "01/" & VB6.Format(lblRunDate.Text, "MM/YYYY")
        mToDate = mLastMonthDay & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " SELECT TRN.* " & vbCrLf _
            & " FROM PAY_DALIY_ATTN_TRN TRN, PAY_EMPLOYEE_MST EMP, PAY_DESG_MST DMST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TRN.COMPANY_CODE = EMP.COMPANY_CODE" & vbCrLf _
            & " AND TRN.EMP_CODE = EMP.EMP_CODE" & vbCrLf _
            & " AND EMP.COMPANY_CODE=DMST.COMPANY_CODE" & vbCrLf _
            & " AND EMP.EMP_DESG_CODE=DMST.DESG_CODE" & vbCrLf _
            & " AND EMP.OVERTIME_APP<>'0'" & vbCrLf _
            & " AND OT_HOURS>0" & vbCrLf _
            & " AND TRN.ATTN_DATE >=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TRN.ATTN_DATE <=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If lblCategory.Text = "W" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='2' "
        Else
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='1' "
        End If

        'SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE IN ('AUX074','AUX053','AUX553') "

        '
        '    SqlStr = SqlStr & " AND TRN.EMP_CODE='005007' AND TRN.ATTN_DATE='01-FEB-2020'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAttn.EOF = False Then
            Do While Not RsAttn.EOF
                mEmpCode = Trim(IIf(IsDbNull(RsAttn.Fields("EMP_CODE").Value), "", RsAttn.Fields("EMP_CODE").Value))
                mEmpCode = VB6.Format(mEmpCode, "000000")
                mDate = VB6.Format(IIf(IsDbNull(RsAttn.Fields("ATTN_DATE").Value), "", RsAttn.Fields("ATTN_DATE").Value), "DD/MM/YYYY")
                If CheckCPLAvail(mEmpCode, mDate) = True Then

                    mFieldValue = 0
                    mHours = 0
                    mMin = 0
                Else
                    mFieldValue = Val(IIf(IsDBNull(RsAttn.Fields("OT_HOURS").Value), 0, RsAttn.Fields("OT_HOURS").Value))

                    mOTMin = (Int(mFieldValue) * 60) + Int((mFieldValue - Int(mFieldValue)) * 60)

                    If mOTMin < 40 Then
                        mOTMin = 0
                    Else
                        If mOTMin < 60 Then
                            mOTMin = 40
                        Else
                            If mOTMInFactor > 0 Then
                                'mOTMin = Int(mOTMin / mOTMInFactor) * mOTMInFactor
                                mOTMin = If(mOTMin >= (Int(mOTMin / mOTMInFactor) * mOTMInFactor) + (mOTMInFactor / 2), (Int(mOTMin / mOTMInFactor) * mOTMInFactor) + mOTMInFactor, Int(mOTMin / mOTMInFactor) * mOTMInFactor)
                                ''=IF(K15>=(L15*30)+15,(L15*30)+30,L15*30)
                            End If
                        End If
                    End If
                    'If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_OT_RATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '    mOTFactor = MasterNo
                    'End If

                    mFieldValue = mOTMin

                    'If mOTFactor <= 1 Then
                    '    mFieldValue = mOTMin * mOTFactor
                    'ElseIf mOTFactor = 2 Then
                    '    mFieldValue = mOTMin * 0.5
                    'End If
                    'Else
                    '    mFieldValue = mFieldValue * 0.5
                    'End If

                    mHours = Int(mFieldValue / 60)
                    mMin = mFieldValue - (mHours * 60)
                    mPrevHours = 0
                    mPrevMin = 0
                End If


                SqlStr = "DELETE FROM PAY_OVERTIME_MST  WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND OT_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)

                If mFieldValue > 0 And (mHours + mMin) > 0 Then
                    SqlStr = "INSERT INTO PAY_OVERTIME_MST ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf & " OT_DATE, OTHOUR, OTMIN, OTTYPE," & vbCrLf & " PREV_OTHOUR,  PREV_OTMIN, " & vbCrLf & " ADDUSER, ADDDATE) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Year(CDate(lblRunDate.Text)) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mEmpCode) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & "  " & mHours & ", " & mMin & ", '0'," & vbCrLf & " " & mPrevHours & ", " & mPrevMin & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    PubDBCn.Execute(SqlStr)
                End If

                RsAttn.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()

        RefreshScreen()

        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
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
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim ColStartRow As Integer
        Dim ColEndRow As Integer
        Dim cntRow As Integer
        Dim mBankName As String
        Dim mRptFileName As String

        PubDBCn.Errors.Clear()


        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, 1, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mRptFileName = "OTEntry.Rpt"

        mTitle = "Over Time Entry (For the Month : " & VB6.Format(lblYear.Text, "MMM-YYYY") & ")"


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
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1

    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FillHeading(CDate(lblRunDate.Text))
        RefreshScreen()
        cmdPrint.Enabled = True
        CmdPreview.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub frmOTAttn_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        ' RefreshScreen
        If FormActive = True Then Exit Sub
        If lblCategory.Text = "W" Then
            Me.Text = "Over Time - Entry (Workers)"
        Else
            Me.Text = "Over Time - Entry (Staff)"
        End If

        FillDeptCombo()

        FormActive = True
    End Sub

    Private Sub frmOTAttn_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        OptName.Checked = True
        '    FillDeptCombo
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub


    Private Sub frmOTAttn_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
        Dim mDays As String
        Dim mCode As String

        If eventArgs.col < 3 Or eventArgs.col > sprdAttn.MaxCols - 1 Then Exit Sub

        sprdAttn.Row = eventArgs.row
        sprdAttn.Col = 1
        If Trim(sprdAttn.Text) = "" Then Exit Sub
        frmOverTimeHead.lblCode.Text = sprdAttn.Text
        mCode = sprdAttn.Text

        sprdAttn.Col = 2
        frmOverTimeHead.lblEmpName.Text = sprdAttn.Text

        sprdAttn.Row = 0
        sprdAttn.Col = eventArgs.col
        If Val(VB.Left(sprdAttn.Text, 2)) = 0 Then Exit Sub
        frmOverTimeHead.lblDate.Text = Mid(Trim(sprdAttn.Text), 1, 2) & " " & lblYear.Text
        mDays = Mid(LTrim(sprdAttn.Text), 1, 2) & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))

        If ChechJoinLeaveDate(mDays, mCode) = False Then Exit Sub

        frmOverTimeHead.lblType.Text = "1"
        frmOverTimeHead.ShowDialog()
        RefreshScreen()
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
        Dim mDeptCode As String
        Dim RsOT As ADODB.Recordset
        Dim mTotOTHour As Double
        Dim mTotOTMIN As Double
        Dim mEmpCat As String

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

        mMonth = CShort(VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        mYear = Year(CDate(lblRunDate.Text))

        LastDayofMon = MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))) & "/" & Month(CDate(lblRunDate.Text)) & "/" & Year(CDate(lblRunDate.Text))
        mDOJ = MainClass.LastDay(mMonth, Year(CDate(lblRunDate.Text))) & "/" & mMonth & "/" & Year(CDate(lblRunDate.Text))
        mDOL = "01" & "/" & mMonth & "/" & Year(CDate(lblRunDate.Text))

        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf & " EMP.EMP_BANK_NO, EMP.PAYMENTMODE, " & vbCrLf & " EMP.EMP_DOJ,EMP.EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf & " AND EMP.EMP_DOJ<=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND (EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If lblCategory.Text = "W" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='2' "
        Else
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='1' "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            '            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        'SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE IN ('AUX074','AUX053','AUX553') "

        ''


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
                    mEmpCat = GetEmployeeCategory(RsAttn.Fields("EMP_CODE").Value, mDOJ)

                    If mEmpCat = "S" Then
                        .MaxRows = cntRow
                        .Row = cntRow

                        .Col = 1
                        mCode = RsAttn.Fields("EMP_CODE").Value
                        .Text = CStr(mCode)

                        .Col = 2
                        .Text = RsAttn.Fields("EMP_NAME").Value


                        SqlStr = " SELECT OT.OT_DATE, OT.OTHOUR , OT.OTMIN, OT.PREV_OTHOUR, OT.PREV_OTMIN " & vbCrLf & " FROM PAY_OVERTIME_MST OT " & vbCrLf & " WHERE " & vbCrLf & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND OT.EMP_CODE='" & mCode & "' " & vbCrLf & " AND TO_CHAR(OT.OT_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " ORDER BY OT.OT_DATE"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOT, ADODB.LockTypeEnum.adLockOptimistic)

                        mTotOTHour = 0
                        mTotOTMIN = 0

                        If RsOT.EOF = False Then
                            Do While Not RsOT.EOF

                                If VB.Day(RsOT.Fields("OT_DATE").Value) = 1 Then
                                    .Col = 3
                                    .Text = CStr(IIf(IsDbNull(RsOT.Fields("PREV_OTHOUR").Value), 0, RsOT.Fields("PREV_OTHOUR").Value))
                                    .Text = CStr(IIf(IsDbNull(RsOT.Fields("PREV_OTMIN").Value), "", .Text & ".") & RsOT.Fields("PREV_OTMIN").Value)

                                    mTotOTHour = mTotOTHour + IIf(IsDbNull(RsOT.Fields("PREV_OTHOUR").Value), 0, RsOT.Fields("PREV_OTHOUR").Value)
                                    mTotOTMIN = mTotOTMIN + IIf(IsDbNull(RsOT.Fields("PREV_OTMIN").Value), 0, RsOT.Fields("PREV_OTMIN").Value)
                                End If
                                .Col = VB.Day(RsOT.Fields("OT_DATE").Value) + 3

                                .Text = CStr(IIf(IsDbNull(RsOT.Fields("OTHOUR").Value), 0, RsOT.Fields("OTHOUR").Value))
                                .Text = CStr(IIf(IsDbNull(RsOT.Fields("OTMIN").Value), "", .Text & ".") & RsOT.Fields("OTMIN").Value)

                                mTotOTHour = mTotOTHour + IIf(IsDbNull(RsOT.Fields("OTHOUR").Value), 0, RsOT.Fields("OTHOUR").Value)
                                mTotOTMIN = mTotOTMIN + IIf(IsDbNull(RsOT.Fields("OTMIN").Value), 0, RsOT.Fields("OTMIN").Value)

                                RsOT.MoveNext()
                            Loop
                        End If

                        .Col = .MaxCols
                        .Text = VB6.Format(GetTOTOverTime(mTotOTHour, mTotOTMIN), "0.00")

                        cntRow = cntRow + 1
                    End If
                    RsAttn.MoveNext()
                Loop

                MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            End With
        End If
        MainClass.SetFocusToCell(sprdAttn, mCurRow, mCurCol)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrRefreshScreen:
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If

        cboCategory.Items.Clear()
        '    If lblCategory.Caption = "W" Then
        '        cboCategory.AddItem "Regular Worker"
        '    Else

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

        '    cboCategory.AddItem "General Staff"
        '    cboCategory.AddItem "Production Staff"
        '    cboCategory.AddItem "Export Staff"
        '    cboCategory.AddItem "Regular Worker"
        '    cboCategory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCategory.AddItem "Director"
        '    cboCategory.AddItem "Trainee Staff"
        ''    End If
        '    cboCategory.ListIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function ChechJoinLeaveDate(ByRef mDays As String, ByRef mCode As String) As Boolean

        Dim SqlStr As String = ""
        Dim RsTempJL As ADODB.Recordset = Nothing

        SqlStr = " SELECT EMP_DOJ,EMP_LEAVE_DATE FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE EMP_CODE = '" & mCode & "' And " & vbCrLf & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempJL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTempJL.EOF = False Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(VB6.Format(RsTempJL.Fields("EMP_DOJ").Value, "dd/mm/yyyy")), CDate(VB6.Format(mDays, "dd/mm/yyyy"))) >= 0 Then
                ChechJoinLeaveDate = True
            Else
                MsgInformation("Employee Joining Date is Greater then Current Date.")
                ChechJoinLeaveDate = False
                Exit Function
            End If
            If IsDbNull(RsTempJL.Fields("EMP_LEAVE_DATE").Value) Then
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

    Private Function GetEmployeeCategory(ByRef pEmpCode As String, ByRef pDate As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT DESG_CAT " & vbCrLf & " FROM PAY_SALARYDEF_MST EMST, PAY_DESG_MST DMST" & vbCrLf & " WHERE EMST.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMST.COMPANY_CODE = DMST.COMPANY_CODE" & vbCrLf & " AND trim(EMST.EMP_DESG_CODE) = trim(DMST.DESG_CODE)" & vbCrLf & " AND EMST.EMP_CODE = '" & pEmpCode & "'" & vbCrLf & " AND EMST.SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & pEmpCode & "'" & vbCrLf & " AND SALARY_APP_DATE<=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetEmployeeCategory = IIf(IsDbNull(RsTemp.Fields("DESG_CAT").Value), "S", RsTemp.Fields("DESG_CAT").Value)
        Else
            GetEmployeeCategory = "S"
        End If
        Exit Function
ErrPart:
        GetEmployeeCategory = "S"
    End Function
End Class
