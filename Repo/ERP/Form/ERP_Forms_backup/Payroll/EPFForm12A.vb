Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEPFForm12A
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColParticulars As Short = 0
    Private Const ColWages As Short = 1
    Private Const ColEmpShare As Short = 2
    Private Const ColEmperShare As Short = 3
    Private Const ColReEmpShare As Short = 4
    Private Const ColReEmperShare As Short = 5
    Private Const ColAdminCharge As Short = 6
    Private Const ColReAdminCharge As Short = 7
    Private Const ColDate As Short = 8
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprd12A)

        With sprd12A
            .MaxCols = ColDate
            .MaxRows = 3

            .Row = 0

            .Col = ColParticulars
            .Text = "Particulars"

            .Col = ColWages
            .Text = "Wages on Which contributions are Payable"

            .Col = ColEmpShare
            .Text = "Recovered from the workers"


            .Col = ColEmperShare
            .Text = "Payable by the Employer"

            .Col = ColReEmpShare
            .Text = "Worker's Share"

            .Col = ColReEmperShare
            .Text = "Employer's Share"

            .Col = ColAdminCharge
            .Text = "Amount of Administrative charges due"

            .Col = ColReAdminCharge
            .Text = "Amount of Administrative charges remitted"

            .Col = ColDate
            .Text = "Date of Remittance"
            .set_RowHeight(0, .get_MaxTextRowHeight(0))

            .Col = ColParticulars

            .Row = 1
            .Text = "E.P.F." & Chr(13) & "A/c No. 01"

            .Row = 2
            .Text = "Pension Fund" & Chr(13) & "A/c No. 10"

            .Row = 3
            .Text = "D.L.I." & Chr(13) & "A/c No. 21"

            MainClass.ProtectCell(sprd12A, 0, .MaxRows, 0, .MaxCols)
            'sprd12A.OperationMode = OperationModeSingle
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

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        ''    mainclass.ClearGrid sprd12A
        ''    mainclass.ClearGrid sprdEmp
        ''    mainclass.ClearGrid sprdEmpDetail
        RefreshScreen()
    End Sub
    Private Sub frmEPFForm12A_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmEPFForm12A_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        lblNewDate.Text = CStr(RunDate)
        txtMonth.Text = MonthName(Month(RunDate))
        TxtYear.Text = CStr(Year(RunDate))

        FormatSprd(-1)
        FillHeading()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub RefreshScreen()

        On Error GoTo ErrPart
        Dim RsSprd12A As ADODB.Recordset
        Dim mNewDate As String

        Call StatutoryRate()

        mNewDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), TxtYear.Text) & "/" & Month(CDate(lblNewDate.Text)) & "/" & TxtYear.Text

        SqlStr = " SELECT COUNT(EMP_CODE) AS EMPCOUNT, " & vbCrLf & " SUM(PFABLEAMT) AS PFABLEAMT1,SUM(PENSIONWAGES) AS PENSIONWAGES1," & vbCrLf & " SUM(PFAMT) AS PFAMT1, " & vbCrLf & " SUM(EPFAMT) AS EPFAMT1,SUM(PENSIONFUND) AS PENSIONFUND1 " & vbCrLf & " FROM PAY_PFESI_TRN PFESITRN " & vbCrLf & " WHERE " & vbCrLf & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf & " SAL_DATE=TO_DATE('" & VB6.Format(mNewDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND PFRATE>0 "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSprd12A, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSprd12A.EOF = False Then
            With sprd12A
                .Row = 1

                .Col = ColWages
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFABLEAMT1").Value), 0, RsSprd12A.Fields("PFABLEAMT1").Value), 0))

                .Col = ColEmpShare
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFAMT1").Value), 0, RsSprd12A.Fields("PFAMT1").Value), 0))

                .Col = ColEmperShare
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("EPFAMT1").Value), 0, RsSprd12A.Fields("EPFAMT1").Value), 0))

                .Col = ColReEmpShare
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFAMT1").Value), 0, RsSprd12A.Fields("PFAMT1").Value), 0))

                .Col = ColReEmperShare
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("EPFAMT1").Value), 0, RsSprd12A.Fields("EPFAMT1").Value), 0))

                .Col = ColAdminCharge
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFABLEAMT1").Value), 0, RsSprd12A.Fields("PFABLEAMT1").Value) * 1.1 / 100, 0))

                .Col = ColReAdminCharge
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFABLEAMT1").Value), 0, RsSprd12A.Fields("PFABLEAMT1").Value) * 1.1 / 100, 0))

                .Col = ColDate
                .Text = ""

                .Row = 2

                .Col = ColWages
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PENSIONWAGES1").Value), 0, RsSprd12A.Fields("PENSIONWAGES1").Value), 0))

                .Col = ColEmpShare
                .Text = "NIL"

                .Col = ColEmperShare
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PENSIONFUND1").Value), 0, RsSprd12A.Fields("PENSIONFUND1").Value), 0))

                .Col = ColReEmpShare
                .Text = "NIL"

                .Col = ColReEmperShare
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PENSIONFUND1").Value), 0, RsSprd12A.Fields("PENSIONFUND1").Value), 0))

                .Col = ColAdminCharge
                .Text = "NIL"

                .Col = ColReAdminCharge
                .Text = "NIL"

                .Col = ColDate
                .Text = ""

                .Row = 3

                .Col = ColWages
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFABLEAMT1").Value), 0, RsSprd12A.Fields("PFABLEAMT1").Value), 0))

                .Col = ColEmpShare
                .Text = "NIL"

                .Col = ColEmperShare
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFABLEAMT1").Value), 0, RsSprd12A.Fields("PFABLEAMT1").Value) * 0.5 / 100, 0))

                .Col = ColReEmpShare
                .Text = "NIL"

                .Col = ColReEmperShare
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFABLEAMT1").Value), 0, RsSprd12A.Fields("PFABLEAMT1").Value) * 0.5 / 100, 0))

                .Col = ColAdminCharge
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFABLEAMT1").Value), 0, RsSprd12A.Fields("PFABLEAMT1").Value) * 0.01 / 100, 0))

                .Col = ColReAdminCharge
                .Text = MainClass.FormatRupees(System.Math.Round(IIf(IsDbNull(RsSprd12A.Fields("PFABLEAMT1").Value), 0, RsSprd12A.Fields("PFABLEAMT1").Value) * 0.01 / 100, 0))

                .Col = ColDate
                .Text = ""

                MainClass.ProtectCell(sprd12A, 0, .MaxRows, 0, .MaxCols)
            End With

            With sprdEmp
                .Col = 2

                .Row = 1
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                .Text = "-"

                .Row = 2
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                .Text = CStr(IIf(IsDbNull(RsSprd12A.Fields("EMPCOUNT").Value), "-", RsSprd12A.Fields("EMPCOUNT").Value))

                .Row = 3
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                .Text = CStr(IIf(IsDbNull(RsSprd12A.Fields("EMPCOUNT").Value), "-", RsSprd12A.Fields("EMPCOUNT").Value))

            End With
        End If
        FillDetail()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillDetail()

        Dim RsEmpCnt As ADODB.Recordset

        Dim mNewDate As String
        Dim mYM As String
        Dim mCurrYM As String
        Dim mPrevYM As String
        Dim CurrSubscriber As Integer
        Dim PrevSubscriber As Integer
        Dim mNewSubscriber As Double
        Dim mLeftSubscriber As Double


        mNewDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), TxtYear.Text) & "/" & Month(CDate(lblNewDate.Text)) & "/" & TxtYear.Text

        mCurrYM = mNewDate
        mPrevYM = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(mNewDate)))
        mYM = "SAL_DATE BETWEEN TO_DATE('" & VB6.Format(mPrevYM, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(mCurrYM, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT EMP_CODE,SAL_DATE " & vbCrLf & " FROM PAY_PFESI_TRN PFESITRN WHERE" & vbCrLf & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf & " " & mYM & " AND PFRATE>0 "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpCnt, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmpCnt.EOF = False Then
            Do While Not RsEmpCnt.EOF
                If VB6.Format(RsEmpCnt.Fields("SAL_DATE").Value, "MMYYYY") = VB6.Format(mCurrYM, "MMYYYY") Then
                    CurrSubscriber = CurrSubscriber + 1
                ElseIf VB6.Format(RsEmpCnt.Fields("SAL_DATE").Value, "MMYYYY") = VB6.Format(mPrevYM, "MMYYYY") Then
                    PrevSubscriber = PrevSubscriber + 1
                End If
                RsEmpCnt.MoveNext()
            Loop
        End If

        With sprdEmpDetail
            .Row = 1
            .Col = 2

            .Text = CStr(PrevSubscriber)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Col = 3
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(PrevSubscriber)
            .Col = 4
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(PrevSubscriber)

            .Row = 2
            .Col = 2
            mNewSubscriber = CalcNewSubscriber(Month(CDate(lblNewDate.Text)), CShort(TxtYear.Text))

            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(mNewSubscriber)
            .Col = 3
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(mNewSubscriber)
            .Col = 4
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(mNewSubscriber)

            .Row = 3
            mLeftSubscriber = CalcLeftSubscriber(Month(CDate(lblNewDate.Text)), CShort(TxtYear.Text))
            .Col = 2
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(mLeftSubscriber)
            .Col = 3
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(mLeftSubscriber)
            .Col = 4
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(mLeftSubscriber)

            .Row = 4
            .Col = 2
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(CurrSubscriber)
            .Col = 3
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(CurrSubscriber)
            .Col = 4
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Text = CStr(CurrSubscriber)

        End With
    End Sub

    Private Sub StatutoryRate()

        Dim RsPFRate As ADODB.Recordset
        Dim mSqlStr As String

        Dim mNewDate As String

        mNewDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), TxtYear.Text) & "/" & Month(CDate(lblNewDate.Text)) & "/" & TxtYear.Text
        mSqlStr = "( SELECT MAX(WEF) " & vbCrLf & " FROM PAY_PFESICeiling_MST WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf & " WEF<=To_Date('" & VB6.Format(mNewDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = " SELECT RATE " & vbCrLf & " FROM PAY_PFESICeiling_MST WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf & " WEF=" & mSqlStr & " AND Code=" & ConPF & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPFRate, ADODB.LockTypeEnum.adLockOptimistic)

        If RsPFRate.EOF = False Then
            txtRate.Text = IIf(IsDbNull(RsPFRate.Fields("Rate").Value), "", RsPFRate.Fields("Rate").Value)
        Else
            txtRate.Text = "12"
        End If
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With sprd12A
            .MaxCols = ColDate
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.7)

            .Col = ColParticulars
            .TypeEditMultiLine = True
            .set_ColWidth(ColParticulars, 11)

            .Col = ColWages
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColWages, 10)

            .Col = ColEmpShare
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColEmpShare, 10)

            .Col = ColEmperShare
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColEmperShare, 10)

            .Col = ColReEmpShare
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColReEmpShare, 10)

            .Col = ColReEmperShare
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColReEmperShare, 10)

            .Col = ColAdminCharge
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColAdminCharge, 10)

            .Col = ColReAdminCharge
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColReAdminCharge, 10)

            .Col = ColDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .set_ColWidth(ColDate, 10)
        End With

        With sprdEmpDetail
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.7)
        End With

        With sprdEmp
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)
        End With

        MainClass.ProtectCell(sprd12A, 1, sprd12A.MaxRows, 1, sprd12A.MaxCols)
        MainClass.ProtectCell(sprdEmp, 1, sprdEmp.MaxRows, 1, sprdEmp.MaxCols)
        MainClass.ProtectCell(sprdEmpDetail, 1, sprdEmpDetail.MaxRows, 1, sprdEmpDetail.MaxCols)

        MainClass.SetSpreadColor(sprd12A, mRow)
        MainClass.SetSpreadColor(sprdEmp, mRow)
        MainClass.SetSpreadColor(sprdEmpDetail, mRow)
        sprdEmp.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsNone
        sprdEmpDetail.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsNone

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    'Private Sub UpDMonth_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.DownClick
    '    SetNewDate(-1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text)))
    'End Sub
    Sub SetNewDate(ByRef prmSpinDirection As Short)
        lblNewDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, prmSpinDirection, CDate(lblNewDate.Text)))
    End Sub
    'Private Sub UpDMonth_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.UpClick
    '    SetNewDate(1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text)))
    'End Sub
    'Private Sub UpDYear_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDYear.DownClick
    '    TxtYear.Text = CStr(CDbl(TxtYear.Text) - 1)
    'End Sub
    'Private Sub UpDYear_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDYear.UpClick
    '    TxtYear.Text = CStr(CDbl(TxtYear.Text) + 1)
    'End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprd12A, 1, sprd12A.MaxRows, ColParticulars, sprd12A.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "CURRENCY PERIOD FROM Ist APRIL, 20" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & " TO 31st MARCH, 20" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00")
        mTitle = "Form 12A (Revised)"
        Call ShowReport(SqlStr, "PFFORM12A.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        'PubDBCn.RollbackTrans
        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        With sprdEmp
            .Col = 2

            .Row = 1
            MainClass.AssignCRptFormulas(Report1, "Contract='" & .Text & "'")

            .Row = 2
            MainClass.AssignCRptFormulas(Report1, "Rest='" & .Text & "'")

            .Row = 3
            MainClass.AssignCRptFormulas(Report1, "Total='" & .Text & "'")

        End With

        With sprdEmpDetail
            .Col = 2

            .Row = 1
            MainClass.AssignCRptFormulas(Report1, "EPF1='" & .Text & "'")

            .Row = 2
            MainClass.AssignCRptFormulas(Report1, "EPF2='" & .Text & "'")

            .Row = 3
            MainClass.AssignCRptFormulas(Report1, "EPF3='" & .Text & "'")

            .Row = 4
            MainClass.AssignCRptFormulas(Report1, "EPF4='" & .Text & "'")

            .Col = 3

            .Row = 1
            MainClass.AssignCRptFormulas(Report1, "PF1='" & .Text & "'")

            .Row = 2
            MainClass.AssignCRptFormulas(Report1, "PF2='" & .Text & "'")

            .Row = 3
            MainClass.AssignCRptFormulas(Report1, "PF3='" & .Text & "'")

            .Row = 4
            MainClass.AssignCRptFormulas(Report1, "PF4='" & .Text & "'")

            .Col = 4

            .Row = 1
            MainClass.AssignCRptFormulas(Report1, "EDL1='" & .Text & "'")

            .Row = 2
            MainClass.AssignCRptFormulas(Report1, "EDL2='" & .Text & "'")

            .Row = 3
            MainClass.AssignCRptFormulas(Report1, "EDL3='" & .Text & "'")

            .Row = 4
            MainClass.AssignCRptFormulas(Report1, "EDL4='" & .Text & "'")
        End With
        MainClass.AssignCRptFormulas(Report1, "MONTH='" & txtMonth.Text & ", " & TxtYear.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "RATE='" & txtRate.Text & "'")
        '    MainClass.AssignCRptFormulas Report1, "BranchPFCode='" & lblBranchPFCode.Caption & "'"
        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Function CalcNewSubscriber(ByRef mMonth As Short, ByRef mYear As Short) As Double

        On Error GoTo refreshErrPart
        Dim mDOJ As Date
        Dim mDOL As Date
        Dim mPFCode As Integer
        Dim RsEmp As ADODB.Recordset = Nothing
        If MainClass.ValidateWithMasterTable(ConPF, "TYPE", "CODE", "PAY_SALARYHEAD_MST ", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPFCode = MasterNo
        Else
            mPFCode = -1
        End If

        mDOJ = CDate(MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear)
        mDOL = CDate("01" & "/" & mMonth & "/" & mYear)

        SqlStr = " Select Count(EMP.EMP_CODE) AS CntEmp" & vbCrLf & " From PAY_EMPLOYEE_MST EMP, PAY_SALARYDEF_MST SALARYDEF WHERE " & vbCrLf & " EMP.COMPANY_CODE=SALARYDEF.COMPANY_CODE AND " & vbCrLf & " EMP.EMP_CODE=SALARYDEF.EMP_CODE AND " & vbCrLf & " SALARYDEF.ADD_DEDUCTCODE=" & mPFCode & " AND " & vbCrLf & " EMP_STOP_SALARY ='N' AND SALARYDEF.AMOUNT>0 AND" & vbCrLf & " (EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY')) AND " & vbCrLf & " EMP.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            CalcNewSubscriber = IIf(IsDbNull(RsEmp.Fields("CntEmp").Value), 0, RsEmp.Fields("CntEmp").Value)
        Else
            CalcNewSubscriber = 0
        End If
        Exit Function
refreshErrPart:
        CalcNewSubscriber = 0
        ''    Resume
    End Function
    Private Function CalcLeftSubscriber(ByRef mMonth As Short, ByRef mYear As Short) As Double

        On Error GoTo refreshErrPart
        Dim mDOJ As Date
        Dim mDOL As Date
        Dim mPFCode As Integer
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mYM As Integer

        If MainClass.ValidateWithMasterTable(ConPF, "TYPE", "CODE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPFCode = MasterNo
        Else
            mPFCode = -1
        End If


        mYM = CInt(VB6.Format(mYear, "0000") & VB6.Format(mMonth, "00"))

        mDOJ = CDate(MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear)
        mDOL = CDate("01" & "/" & mMonth & "/" & mYear)

        SqlStr = " Select Count(EMP.EMP_CODE) AS CntEmp" & vbCrLf & " From PAY_EMPLOYEE_MST EMP, PAY_SALARYDEF_MST SALARYDEF WHERE " & vbCrLf & " EMP.COMPANY_CODE=SALARYDEF.COMPANY_CODE AND " & vbCrLf & " EMP.EMP_CODE=SALARYDEF.EMP_CODE AND " & vbCrLf & " SALARYDEF.ADD_DEDUCTCODE=" & mPFCode & " AND " & vbCrLf & " EMP_STOP_SALARY ='N' AND SALARYDEF.AMOUNT>0 AND" & vbCrLf & " (EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY')) AND " & vbCrLf & " EMP.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        '    SqlStr = " Select Count(EMP.CODE) AS CntEmp" & vbCrLf _
        ''            & " From EMP, SALARYDEF WHERE " & vbCrLf _
        ''            & " EMP.CODE=SALARYDEF.CODE AND " & vbCrLf _
        ''            & " StopSal='N' AND SALARYDEF.ADD_DEDUCTCODE=" & mPFCode & " AND " & vbCrLf _
        ''            & " SALARYDEF.SUBKEY=(SELECT MAX(SUBKEY) FROM SALARYDEF " & vbCrLf _
        ''            & " WHERE SUBKEY<=" & mYM & "  AND " & vbCrLf _
        ''            & " CompanyCode=" & RsCompany!CompanyCode & ") " & vbCrLf _
        ''            & " AND SALARYDEF.AMOUNT>0 AND" & vbCrLf _
        ''            & " (EMP_DOL <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "') " & vbCrLf _
        ''            & " AND EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "')) AND " & vbCrLf _
        ''            & " EMP.CompanyCode=" & RsCompany!CompanyCode & ""
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            CalcLeftSubscriber = IIf(IsDbNull(RsEmp.Fields("CntEmp").Value), 0, RsEmp.Fields("CntEmp").Value)
        Else
            CalcLeftSubscriber = 0
        End If
        Exit Function
refreshErrPart:
        CalcLeftSubscriber = 0
        ''    Resume
    End Function

    Private Sub UpDMonth_ValueChanged(sender As Object, e As EventArgs) Handles UpDMonth.ValueChanged

    End Sub
End Class
