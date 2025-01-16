Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmESIForm7
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    Private Const ColAcctNo As Short = 1
    Private Const ColName As Short = 2
    Private Const ColDispName As Short = 3
    Private Const ColOccupation As Short = 4
    Private Const ColDept As Short = 5
    Private Const ColDOJ As Short = 6
    Private Const ColWDays_1 As Short = 7
    Private Const ColWages_1 As Short = 8
    Private Const ColEmpCont_1 As Short = 9
    Private Const ColWDays_2 As Short = 10
    Private Const ColWages_2 As Short = 11
    Private Const ColEmpCont_2 As Short = 12
    Private Const ColWDays_3 As Short = 13
    Private Const ColWages_3 As Short = 14
    Private Const ColEmpCont_3 As Short = 15
    Private Const ColWDays_4 As Short = 16
    Private Const ColWages_4 As Short = 17
    Private Const ColEmpCont_4 As Short = 18
    Private Const ColWDays_5 As Short = 19
    Private Const ColWages_5 As Short = 20
    Private Const ColEmpCont_5 As Short = 21
    Private Const ColWDays_6 As Short = 22
    Private Const ColWages_6 As Short = 23
    Private Const ColEmpCont_6 As Short = 24
    Private Const ColWDays As Short = 25
    Private Const ColWages As Short = 26
    Private Const ColEmpCont As Short = 27
    Private Const ColDWages As Short = 28
    Private Const ColRemarks As Short = 29

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        With SprdMain
            .MaxCols = ColRemarks

            .Row = 0

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColAcctNo
            .Text = "Insurance Number"

            .Col = ColName
            .Text = "Name of the Insured Person"

            .Col = ColDispName
            .Text = "Name of the Dispensary to which attached"

            .Col = ColOccupation
            .Text = "Occupation"

            .Col = ColDept
            .Text = "Department/Shift if any"

            .Col = ColDOJ
            .Text = "if appointted during the contribution period, Date of appointment."

            .Col = ColWDays_1
            .Text = "No. of days for which wages paid / payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "APR", "OCT") & " )"

            .Col = ColWages_1
            .Text = "Total Amount of wages paid/payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "APR", "OCT") & " )"

            .Col = ColEmpCont_1
            .Text = "Employee's Share of Contribution" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "APR", "OCT") & " )"

            .Col = ColWDays_2
            .Text = "No. of days for which wages paid / payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "MAY", "NOV") & " )"

            .Col = ColWages_2
            .Text = "Total Amount of wages paid/payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "MAY", "NOV") & " )"

            .Col = ColEmpCont_2
            .Text = "Employee's Share of Contribution" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "MAY", "NOV") & " )"

            .Col = ColWDays_3
            .Text = "No. of days for which wages paid / payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "JUN", "DEC") & " )"

            .Col = ColWages_3
            .Text = "Total Amount of wages paid/payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "JUN", "DEC") & " )"

            .Col = ColEmpCont_3
            .Text = "Employee's Share of Contribution" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "JUN", "DEC") & " )"

            .Col = ColWDays_4
            .Text = "No. of days for which wages paid / payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "JUL", "JAN") & " )"

            .Col = ColWages_4
            .Text = "Total Amount of wages paid/payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "JUL", "JAN") & " )"

            .Col = ColEmpCont_4
            .Text = "Employee's Share of Contribution" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "JUL", "JAN") & " )"

            .Col = ColWDays_5
            .Text = "No. of days for which wages paid / payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "AUG", "FEB") & " )"

            .Col = ColWages_5
            .Text = "Total Amount of wages paid/payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "AUG", "FEB") & " )"

            .Col = ColEmpCont_5
            .Text = "Employee's Share of Contribution" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "AUG", "FEB") & " )"

            .Col = ColWDays_6
            .Text = "No. of days for which wages paid / payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "SEP", "MAR") & " )"

            .Col = ColWages_6
            .Text = "Total Amount of wages paid/payable" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "SEP", "MAR") & " )"

            .Col = ColEmpCont_6
            .Text = "Employee's Share of Contribution" & vbNewLine & vbNewLine & "( " & IIf(VB.Left(cboPeriod.Text, 1) = "1", "SEP", "MAR") & " )"

            .Col = ColWDays
            .Text = "Total No. of days in Contribution period for which wages paid/payable"

            .Col = ColWages
            .Text = "Total amount of wages paid/payable in the contribution period"

            .Col = ColEmpCont
            .Text = "Total Employees' Contribution in the period"

            .Col = ColDWages
            .Text = "Daily wages"

            .Col = ColRemarks
            .Text = "Remarks"

            .set_RowHeight(0, .get_MaxTextRowHeight(0))

            MainClass.ProtectCell(SprdMain, 0, .MaxRows, 0, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        End With
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboEmployee.Enabled = False
        Else
            cboEmployee.Enabled = True
        End If
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
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        RefreshScreen()
        Call SprdTotal()
        FormatSprd(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdTotal()
        Dim cntRow As Integer
        Dim mTotWDays_1 As Double
        Dim mTotWages_1 As Double
        Dim mTotEmpCont_1 As Double
        Dim mTotWDays_2 As Double
        Dim mTotWages_2 As Double
        Dim mTotEmpCont_2 As Double
        Dim mTotWDays_3 As Double
        Dim mTotWages_3 As Double
        Dim mTotEmpCont_3 As Double
        Dim mTotWDays_4 As Double
        Dim mTotWages_4 As Double
        Dim mTotEmpCont_4 As Double
        Dim mTotWDays_5 As Double
        Dim mTotWages_5 As Double
        Dim mTotEmpCont_5 As Double
        Dim mTotWDays_6 As Double
        Dim mTotWages_6 As Double
        Dim mTotEmpCont_6 As Double
        Dim mTotWDays As Double
        Dim mTotWages As Double
        Dim mTotEmpCont As Double
        Dim mTotDWages As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColWDays_1
                mTotWDays_1 = mTotWDays_1 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWages_1
                mTotWages_1 = mTotWages_1 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColEmpCont_1
                mTotEmpCont_1 = mTotEmpCont_1 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWDays_2
                mTotWDays_2 = mTotWDays_2 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWages_2
                mTotWages_2 = mTotWages_2 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColEmpCont_2
                mTotEmpCont_2 = mTotEmpCont_2 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWDays_3
                mTotWDays_3 = mTotWDays_3 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWages_3
                mTotWages_3 = mTotWages_3 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColEmpCont_3
                mTotEmpCont_3 = mTotEmpCont_3 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWDays_4
                mTotWDays_4 = mTotWDays_4 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWages_4
                mTotWages_4 = mTotWages_4 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColEmpCont_4
                mTotEmpCont_4 = mTotEmpCont_4 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWDays_5
                mTotWDays_5 = mTotWDays_5 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWages_5
                mTotWages_5 = mTotWages_5 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColEmpCont_5
                mTotEmpCont_5 = mTotEmpCont_5 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWDays_6
                mTotWDays_6 = mTotWDays_6 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWages_6
                mTotWages_6 = mTotWages_6 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColEmpCont_6
                mTotEmpCont_6 = mTotEmpCont_6 + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWDays
                mTotWDays = mTotWDays + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColWages
                mTotWages = mTotWages + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColEmpCont
                mTotEmpCont = mTotEmpCont + IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColDWages
                mTotDWages = mTotDWages + IIf(IsNumeric(.Text), .Text, 0)
            Next

            .MaxRows = .MaxRows + 2

            .Row = .MaxRows
            .Col = ColWDays_1
            .Text = VB6.Format(mTotWDays_1, "0.00")

            .Col = ColWages_1
            .Text = VB6.Format(mTotWages_1, "0.00")

            .Col = ColEmpCont_1
            .Text = VB6.Format(mTotEmpCont_1, "0.00")

            .Col = ColWDays_2
            .Text = VB6.Format(mTotWDays_2, "0.00")

            .Col = ColWages_2
            .Text = VB6.Format(mTotWages_2, "0.00")

            .Col = ColEmpCont_2
            .Text = VB6.Format(mTotEmpCont_2, "0.00")

            .Col = ColWDays_3
            .Text = VB6.Format(mTotWDays_3, "0.00")

            .Col = ColWages_3
            .Text = VB6.Format(mTotWages_3, "0.00")

            .Col = ColEmpCont_3
            .Text = VB6.Format(mTotEmpCont_3, "0.00")

            .Col = ColWDays_4
            .Text = VB6.Format(mTotWDays_4, "0.00")

            .Col = ColWages_4
            .Text = VB6.Format(mTotWages_4, "0.00")

            .Col = ColEmpCont_4
            .Text = VB6.Format(mTotEmpCont_4, "0.00")

            .Col = ColWDays_5
            .Text = VB6.Format(mTotWDays_5, "0.00")

            .Col = ColWages_5
            .Text = VB6.Format(mTotWages_5, "0.00")

            .Col = ColEmpCont_5
            .Text = VB6.Format(mTotEmpCont_5, "0.00")

            .Col = ColWDays_6
            .Text = VB6.Format(mTotWDays_6, "0.00")

            .Col = ColWages_6
            .Text = VB6.Format(mTotWages_6, "0.00")

            .Col = ColEmpCont_6
            .Text = VB6.Format(mTotEmpCont_6, "0.00")

            .Col = ColWDays
            .Text = VB6.Format(mTotWDays, "0.00")

            .Col = ColWages
            .Text = VB6.Format(mTotWages, "0.00")

            .Col = ColEmpCont
            .Text = VB6.Format(mTotEmpCont, "0.00")

            .Col = ColDWages
            .Text = VB6.Format(mTotDWages, "0.00")


            .Row = .MaxRows
            .set_RowHeight(.MaxRows, 20)
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .Font = VB6.FontChangeBold(.Font, True)
            .BlockMode = False
        End With

    End Sub
    Private Sub frmESIForm7_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmESIForm7_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        optCardNo.Checked = True

        FillDeptCombo()
        FormatSprd(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmESIForm7_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frasprd.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub RefreshScreen()

        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mDeptCode As String
        Dim mDateFrom As String
        Dim mDateTo As String

        MainClass.ClearGrid(SprdMain)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboEmployee.Text = "" Then
                MsgInformation("Please select the Cont. Name.")
                cboEmployee.Focus()
                Exit Sub
            End If
        End If

        If VB.Left(cboPeriod.Text, 1) = "1" Then
            mDateFrom = "01/04/" & Year(RsCompany.Fields("START_DATE").Value)
            mDateTo = "30/09/" & Year(RsCompany.Fields("START_DATE").Value)
        ElseIf VB.Left(cboPeriod.Text, 1) = "2" Then
            mDateFrom = "01/10/" & Year(RsCompany.Fields("START_DATE").Value)
            mDateTo = "31/03/" & Year(RsCompany.Fields("END_DATE").Value)
        Else
            mDateFrom = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
            mDateTo = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
        End If

        SqlStr = " SELECT " & vbCrLf & " ESIAC_CODE_NUM, EMP_NAME, " & vbCrLf & " DISPENSARY, DESG_DESC,  " & vbCrLf & " DEPT_DESC, CASE WHEN DOJ>=TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND DOJ<=TO_DATE('" & VB6.Format(mDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN DOJ END,  "

        If VB.Left(cboPeriod.Text, 1) = "1" Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='APR' THEN WDAYS ELSE 0 END)) AS APR , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='APR' THEN TOT_WAGES ELSE 0 END)) AS APR, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='APR' THEN ESI_AMT ELSE 0 END)) AS APR, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='MAY' THEN WDAYS ELSE 0 END)) AS MAY , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='MAY' THEN TOT_WAGES ELSE 0 END)) AS MAY, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='MAY' THEN ESI_AMT ELSE 0 END)) AS MAY ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JUN' THEN WDAYS ELSE 0 END)) AS JUN , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JUN' THEN TOT_WAGES ELSE 0 END)) AS JUN ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JUN' THEN ESI_AMT ELSE 0 END)) AS JUN ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JUL' THEN WDAYS ELSE 0 END)) AS JUL , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JUL' THEN TOT_WAGES ELSE 0 END)) AS JUL ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JUL' THEN ESI_AMT ELSE 0 END)) AS JUL ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='AUG' THEN WDAYS ELSE 0 END)) AS AUG , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='AUG' THEN TOT_WAGES ELSE 0 END)) AS AUG ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='AUG' THEN ESI_AMT ELSE 0 END)) AS AUG ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='SEP' THEN WDAYS ELSE 0 END)) AS SEP , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='SEP' THEN TOT_WAGES ELSE 0 END)) AS SEP ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='SEP' THEN ESI_AMT ELSE 0 END)) AS SEP ,"

        ElseIf VB.Left(cboPeriod.Text, 1) = "2" Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='OCT' THEN WDAYS ELSE 0 END)) AS OCT , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='OCT' THEN TOT_WAGES ELSE 0 END)) AS OCT, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='OCT' THEN ESI_AMT ELSE 0 END)) AS OCT, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='NOV' THEN WDAYS ELSE 0 END)) AS NOV , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='NOV' THEN TOT_WAGES ELSE 0 END)) AS NOV, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='NOV' THEN ESI_AMT ELSE 0 END)) AS NOV ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='DEC' THEN WDAYS ELSE 0 END)) AS DEC , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='DEC' THEN TOT_WAGES ELSE 0 END)) AS DEC ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='DEC' THEN ESI_AMT ELSE 0 END)) AS DEC ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JAN' THEN WDAYS ELSE 0 END)) AS JAN , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JAN' THEN TOT_WAGES ELSE 0 END)) AS JAN ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='JAN' THEN ESI_AMT ELSE 0 END)) AS JAN ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='FEB' THEN WDAYS ELSE 0 END)) AS FEB , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='FEB' THEN TOT_WAGES ELSE 0 END)) AS FEB ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='FEB' THEN ESI_AMT ELSE 0 END)) AS FEB ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='MAR' THEN WDAYS ELSE 0 END)) AS MAR , " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='MAR' THEN TOT_WAGES ELSE 0 END)) AS MAR ," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(EDATE,'MON')='MAR' THEN ESI_AMT ELSE 0 END)) AS MAR ,"

        End If


        SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(WDAYS)), " & vbCrLf & " TO_CHAR(SUM(TOT_WAGES)),TO_CHAR(SUM(ESI_AMT)), " & vbCrLf & " TO_CHAR(CASE WHEN SUM(WDAYS)=0 then 0 ELSE SUM(TOT_WAGES)/SUM(WDAYS) END), ''  "

        ''

        SqlStr = SqlStr & vbCrLf & " FROM PAY_CONTESI_TRN WHERE "


        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked And RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            SqlStr = SqlStr & vbCrLf & " COMPANY_CODE IN (" & RsCompany.Fields("COMPANY_CODE").Value & ",15) "
        Else
            SqlStr = SqlStr & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If


        SqlStr = SqlStr & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TO_DATE('" & VB6.Format(mDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And cboEmployee.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND CONT_NAME='" & MainClass.AllowSingleQuote(cboEmployee.Text) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " ESIAC_CODE_NUM, EMP_NAME, " & vbCrLf & " DISPENSARY, DESG_DESC,  " & vbCrLf & " DEPT_DESC, DOJ "

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ESI_AMT)>0"

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP_NAME,ESIAC_CODE_NUM "
        Else
            SqlStr = SqlStr & vbCrLf & " Order by ESIAC_CODE_NUM,EMP_NAME "
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = "Select DISTINCT CONT_NAME from PAY_CONTESI_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CONT_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboEmployee.Items.Add(RsDept.Fields("CONT_NAME").Value)
                RsDept.MoveNext()
            Loop
            cboEmployee.SelectedIndex = 0
        End If


        cboPeriod.Items.Clear()
        cboPeriod.Items.Add("1st Half")
        cboPeriod.Items.Add("2nd Half")
        '    cboPeriod.AddItem "Both"
        cboPeriod.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColRemarks
            .Row = mRow
            .set_RowHeight(-1, 15)
            .Col = ColAcctNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColAcctNo, 10)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 18)
            .ColsFrozen = ColName

            .Col = ColDispName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
            .set_ColWidth(ColWDays, 8)

            .Col = ColOccupation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
            .set_ColWidth(ColOccupation, 8)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
            .set_ColWidth(ColDept, 8)

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
            .set_ColWidth(ColDOJ, 10)

            For cntCol = ColWDays_1 To ColDWages
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 10)
            Next

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 8)
        End With

        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols
        '    MainClass.SetSpreadColor SprdMain, mRow

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 0, SprdMain.MaxRows)
        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(SprdMain, mRow)

        FillHeading()

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub





    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mDateFrom As String
        Dim mDateTo As String

        PubDBCn.Errors.Clear()

        'clear all formulas
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "REGISTER OF EMPLOYEES"

        If VB.Left(cboPeriod.Text, 1) = "1" Then
            mDateFrom = "01/04/" & Year(RsCompany.Fields("START_DATE").Value)
            mDateTo = "30/09/" & Year(RsCompany.Fields("START_DATE").Value)
        ElseIf VB.Left(cboPeriod.Text, 1) = "2" Then
            mDateFrom = "01/10/" & Year(RsCompany.Fields("START_DATE").Value)
            mDateTo = "31/03/" & Year(RsCompany.Fields("END_DATE").Value)
        Else
            mDateFrom = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
            mDateTo = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
        End If

        mSubTitle = "Contribution Period From " & mDateFrom & " To " & mDateTo

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows - 2, ColAcctNo, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)



        Call ShowReport(SqlStr, "ESIFORM7.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mMonth1 As String
        Dim mMonth2 As String
        Dim mMonth3 As String
        Dim mMonth4 As String
        Dim mMonth5 As String
        Dim mMonth6 As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies

        If VB.Left(cboPeriod.Text, 1) = "1" Then
            mMonth1 = "APR"
            mMonth2 = "MAY"
            mMonth3 = "JUN"
            mMonth4 = "JUL"
            mMonth5 = "AUG"
            mMonth6 = "SEP"
        ElseIf VB.Left(cboPeriod.Text, 1) = "2" Then
            mMonth1 = "OCT"
            mMonth2 = "NOV"
            mMonth3 = "DEC"
            mMonth4 = "JAN"
            mMonth5 = "FEB"
            mMonth6 = "MAR"
        End If

        MainClass.AssignCRptFormulas(Report1, "MONTH1=""" & "MONTH : " & mMonth1 & """")
        MainClass.AssignCRptFormulas(Report1, "MONTH2=""" & "MONTH : " & mMonth2 & """")
        MainClass.AssignCRptFormulas(Report1, "MONTH3=""" & "MONTH : " & mMonth3 & """")
        MainClass.AssignCRptFormulas(Report1, "MONTH4=""" & "MONTH : " & mMonth4 & """")
        MainClass.AssignCRptFormulas(Report1, "MONTH5=""" & "MONTH : " & mMonth5 & """")
        MainClass.AssignCRptFormulas(Report1, "MONTH6=""" & "MONTH : " & mMonth6 & """")

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
