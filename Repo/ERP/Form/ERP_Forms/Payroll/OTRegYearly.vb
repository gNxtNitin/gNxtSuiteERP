Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmOTRegYearly
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
    Private Const ColOTMonth As Short = 3
    Private Const ColPaymentType As Short = 4
    Private Const ColDept As Short = 5
    Private Const ColDesg As Short = 6
    Private Const ColDOJ As Short = 7
    Private Const ColBankNo As Short = 8
    Private Const ColBSalary As Short = 9
    Private Const ColOT As Short = 10
    Private Const ColRate As Short = 11
    Private Const ColAmount As Short = 12
    Private Const ColESIC As Short = 13
    Private Const ColAdvance As Short = 14
    Private Const ColNetAmount As Short = 15

    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdAttn)

        With sprdAttn
            .MaxCols = ColNetAmount

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCard
            .Text = "Emp Card No"

            .Col = ColPaymentType
            .Text = "Payment Type"

            .Col = ColBankNo
            .Text = "Bank A/c No."

            .Col = ColDept
            .Text = "Department"

            .Col = ColDesg
            .Text = "Designation"

            .Col = ColDOJ
            .Text = "DoJ"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColOTMonth
            .Text = "OT Month "

            .Col = ColBSalary
            .Text = "Gross Salary"
            .Col = ColOT
            .Text = "OT"
            .Col = ColRate
            .Text = "Rate"
            .Col = ColAmount
            .Text = "Amount"
            .Col = ColESIC
            .Text = "ESI "
            .Col = ColAdvance
            .Text = "Advance"
            .Col = ColNetAmount
            .Text = "Net Over Time"

            FormatSprd(-1)
        End With
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSalary(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForSalary(ByRef Mode As Crystal.DestinationConstants)


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


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, ColCard, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mRptFileName = "OTRegYearly.Rpt"

        mTitle = "OT Register (Yearly)"


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
    Private Function FetchRecordForSalReg(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM TEMP_SALREG_TRN " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW "

        FetchRecordForSalReg = mSqlStr
    End Function

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
        Call ReportForSalary(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        RefreshScreen()
    End Sub
    Private Sub frmOTRegYearly_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmOTRegYearly_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Width = VB6.TwipsToPixelsX(11355)

        FillHeading()

        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsOT As ADODB.Recordset
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mAddDeduct As Integer
        Dim mPayableSalary As Double
        Dim mTotPayable As Double
        Dim mTotDeduct As Double
        Dim mNetSalary As Double
        Dim mTotalMonth As Integer
        Dim mSalMonth As String
        Dim mSalNextMonth As String

        Dim mIsArrear As String
        Dim mIsNextArrear As String

        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Please Select Emp. Code")
            Exit Sub
        End If

        mTotalMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtFrom.Text), CDate(txtTo.Text))

        MainClass.ClearGrid(sprdAttn)

        SqlStr = " SELECT OTTRN.*, EMP_DEPT_CODE,EMP.EMP_NAME, EMP.EMP_DOJ, EMP.EMP_FNAME, EMP.PAYMENTMODE,EMP_BANK_NO,EMP_BANK_NO, EMP_DESG_CODE " & vbCrLf & " FROM PAY_MONTHLY_OT_TRN OTTRN, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " OTTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'" & vbCrLf & " AND OTTRN.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND OTTRN.EMP_CODE =EMP.EMP_CODE " & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"


        SqlStr = SqlStr & vbCrLf & " AND OT_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND OT_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        SqlStr = SqlStr & vbCrLf & " ORDER BY OT_DATE, OTTRN.IS_ARREAR "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOT, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOT.EOF = False Then
            With sprdAttn
                cntRow = 1

                Do While Not RsOT.EOF
                    .MaxRows = cntRow
                    .Row = cntRow

                    mTotPayable = 0
                    mTotDeduct = 0
                    .Col = ColCard
                    mCode = RsOT.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsOT.Fields("EMP_NAME").Value


                    .Col = ColOTMonth
                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("OT_DATE").Value), "", RsOT.Fields("OT_DATE").Value), "MMMM,YYYY")

                    If RsOT.Fields("Is_Arrear").Value = "Y" Then
                        .Text = .Text & " - Arrear"
                    ElseIf RsOT.Fields("Is_Arrear").Value = "O" Then
                        .Text = .Text & " - Others"
                    End If

                    .Col = ColPaymentType
                    .Text = IIf(RsOT.Fields("PAYMENTMODE").Value = "1", "Cash", "Cheque")

                    .Col = ColDept
                    .Text = IIf(IsDbNull(RsOT.Fields("EMP_DEPT_CODE").Value), "", RsOT.Fields("EMP_DEPT_CODE").Value)

                    .Col = ColDesg
                    .Text = IIf(IsDbNull(RsOT.Fields("EMP_DESG_CODE").Value), "", RsOT.Fields("EMP_DESG_CODE").Value)

                    .Col = ColDOJ
                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("EMP_DOJ").Value), "", RsOT.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                    .Col = ColBankNo
                    .Text = IIf(IsDbNull(RsOT.Fields("EMP_BANK_NO").Value), "", RsOT.Fields("EMP_BANK_NO").Value)


                    .Col = ColBSalary
                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("BASICSALARY").Value), 0, RsOT.Fields("BASICSALARY").Value), "0.00")

                    .Col = ColOT
                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("OT_HOUR").Value), 0, RsOT.Fields("OT_HOUR").Value), "0.00")

                    .Col = ColRate
                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("Rate").Value), 0, RsOT.Fields("Rate").Value), "0.00")

                    .Col = ColAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("OT_AMOUNT").Value), 0, RsOT.Fields("OT_AMOUNT").Value), "0.00")

                    .Col = ColESIC
                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("ESIC_AMOUNT").Value), 0, RsOT.Fields("ESIC_AMOUNT").Value), "0.00")

                    .Col = ColAdvance
                    If chkAdvance.CheckState = System.Windows.Forms.CheckState.Checked Then
                        .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("ADV_AMOUNT").Value), 0, RsOT.Fields("ADV_AMOUNT").Value), "0.00")
                    Else
                        .Text = "0.00"
                    End If

                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("ADV_AMOUNT").Value), 0, RsOT.Fields("ADV_AMOUNT").Value), "0.00")

                    .Col = ColNetAmount
                    .Text = VB6.Format(IIf(IsDbNull(RsOT.Fields("NET_AMOUNT").Value), 0, RsOT.Fields("NET_AMOUNT").Value), "0.00")


                    cntRow = cntRow + 1

                    RsOT.MoveNext()
                Loop

                ColTotal(sprdAttn, ColBSalary, .MaxCols)
                .Col = ColName
                .Row = .MaxRows
                .Text = "TOTAL :"
                MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            End With
            Call PrintCommand(True)
        Else
            MsgInformation("Over Time Not Processed For This Period ...")
        End If
        FormatSprd(-1)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAttn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .set_ColWidth(ColSNO, 5)

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCard, 7)
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 15)
            .ColHidden = True

            .Col = ColOTMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColOTMonth, 15)

            .Col = ColPaymentType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColPaymentType, 7)
            .ColHidden = True

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 9)
            .ColHidden = True

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesg, 9)
            .ColHidden = True

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDOJ, 9)
            .ColHidden = True

            .Col = ColBankNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBankNo, 7)
            .ColHidden = True


            .ColsFrozen = ColBSalary
            For cntCol = ColBSalary To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
            Next
        End With

        MainClass.ProtectCell(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxRows)
        sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        MainClass.SetSpreadColor(sprdAttn, mRow)

        Exit Sub
ERR1:

        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function CheckCalcOnBasic(ByRef mSalHead As String) As Boolean
        On Error GoTo CheckCalcOnBasicErr
        Dim SqlStr As String = ""
        CheckCalcOnBasic = False
        If MainClass.ValidateWithMasterTable(mSalHead, "Name", "CALC_ON", "Add_Deduct", PubDBCn, MasterNo) = True Then
            If MasterNo <> ConCalcVariable Then
                CheckCalcOnBasic = True
            End If
        End If
        Exit Function
CheckCalcOnBasicErr:
        MsgBox(Err.Description)
        CheckCalcOnBasic = False
    End Function

    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub
End Class
