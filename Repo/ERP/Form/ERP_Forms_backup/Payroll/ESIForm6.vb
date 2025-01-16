Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmESIForm6
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    Private Const ColCodeNo As Short = 1
    Private Const ColAcctNo As Short = 2
    Private Const ColName As Short = 3
    Private Const ColWDays As Short = 4
    Private Const ColWages As Short = 5
    Private Const ColEmpCont As Short = 6
    Private Const ColDWages As Short = 7
    Private Const ColWorking As Short = 8
    Private Const ColDispensary As Short = 9
    Private Const ColRemarks As Short = 10
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    '    Private Sub FillHeading()

    '        Dim RsTemp As ADODB.Recordset = Nothing
    '        Dim cntCol As Integer
    '        Dim mAddDeduct As Integer

    '        With SprdMain
    '            .MaxCols = ColRemarks

    '            .Row = 0

    '            .Col = ColSNo
    '            .Text = "S. No."

    '            .Col = ColCodeNo
    '            .Text = "Code No"

    '            .Col = ColAcctNo
    '            .Text = "Insurance Number"


    '            .Col = ColName
    '            .Text = "Name of Insured Person"

    '            .Col = ColWDays
    '            .Text = "No. of days for which wages paid"

    '            .Col = ColWages
    '            .Text = "Total amount of wages paid"

    '            .Col = ColEmpCont
    '            .Text = "Employees' Contribution Deduction"

    '            .Col = ColDWages
    '            .Text = "Average Daily wages"

    '            .Col = ColWorking
    '            .Text = "Whether still continues working and drawing wages within the insurable wage ceiling"

    '            .Col = ColDispensary
    '            .Text = "Name of Dispensary"

    '            .Col = ColRemarks
    '            .Text = "Remarks"

    '            .set_RowHeight(0, .get_MaxTextRowHeight(0))

    '            MainClass.ProtectCell(SprdMain, 0, .MaxRows, 0, .MaxCols)
    '            '        sprdMain.OperationMode = OperationModeSingle
    '            '        sprdMain.SetOddEvenRowColor &HC0FFFF, vbBlack, &HFFFFC0, vbBlack
    '        End With
    '    End Sub

    '    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
    '        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
    '            cboEmployee.Enabled = False
    '        Else
    '            cboEmployee.Enabled = True
    '        End If
    '    End Sub

    '    Private Sub chkSalType_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSalType.CheckStateChanged
    '        If chkSalType.CheckState = System.Windows.Forms.CheckState.Checked Then
    '            cboSalType.Enabled = False
    '        Else
    '            cboSalType.Enabled = True
    '        End If
    '    End Sub


    '    Private Sub CmdChallan_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdChallan.Click
    '        frmESIChallan.ShowDialog()
    '        FillFlxGridChallan()
    '    End Sub
    '    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
    '        Me.hide()
    '    End Sub
    '    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '        frmPrintESIForm6.ShowDialog()

    '        If G_PrintLedg = False Then
    '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '            Exit Sub
    '        End If

    '        If frmPrintESIForm6.OptFront.Checked = True Then
    '            Call ReportForFrontPrint(Crystal.DestinationConstants.crptToWindow)
    '        ElseIf frmPrintESIForm6.OptBack.Checked = True Then
    '            Call ReportForBackPrint(Crystal.DestinationConstants.crptToWindow)
    '        End If
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '        frmPrintESIForm6.ShowDialog()

    '        If G_PrintLedg = False Then
    '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '            Exit Sub
    '        End If

    '        If frmPrintESIForm6.OptFront.Checked = True Then
    '            Call ReportForFrontPrint(Crystal.DestinationConstants.crptToPrinter)
    '        ElseIf frmPrintESIForm6.OptBack.Checked = True Then
    '            Call ReportForBackPrint(Crystal.DestinationConstants.crptToPrinter)
    '        End If
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '    End Sub
    '    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

    '        On Error GoTo ErrPart

    '        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If cboEmployee.Text = "" Then
    '                MsgInformation("Please select the Department Name.")
    '                cboEmployee.Focus()
    '                Exit Sub
    '            End If
    '        End If

    '        If chkSalType.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If cboSalType.Text = "" Then
    '                MsgInformation("Please select the Salary Type.")
    '                cboSalType.Focus()
    '                Exit Sub
    '            End If
    '        End If

    '        MainClass.ClearGrid(SprdMain)

    '        If Show1 = False Then GoTo ErrPart
    '        FormatSprd(-1)
    '        FillFlxGridChallan()
    '        Exit Sub
    'ErrPart:
    '        'Resume
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Sub
    '    Private Function Show1() As Boolean

    '        On Error GoTo LedgError

    '        Show1 = False
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

    '        SqlStr = MakeSQL
    '        MainClass.AssignDataInSprd8(SqlStr, AData1, StrConn, "Y")

    '        With SprdMain
    '            ColTotal(SprdMain, ColWDays, ColDWages)
    '            .Col = ColName
    '            .Row = .MaxRows
    '            .Text = "TOTAL :"

    '            MainClass.ProtectCell(SprdMain, 0, .MaxRows, 0, .MaxCols)
    '        End With

    '        '********************************
    '        Show1 = True
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    '        Exit Function
    'LedgError:
    '        Show1 = False
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Function
    '    Private Sub cmdVwChallan_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdVwChallan.Click
    '        fraListChallan.Visible = Not fraListChallan.Visible
    '        If fraListChallan.Visible = True Then
    '            FillFlxGridChallan()
    '            cmdVwChallan.Text = "Clear Challan"
    '            fraListChallan.BringToFront()
    '        Else
    '            cmdVwChallan.Text = "View Challan"
    '            Frasprd.BringToFront()
    '        End If
    '    End Sub
    '    Private Sub frmESIForm6_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
    '        'RefreshScreen
    '    End Sub
    '    Private Sub frmESIForm6_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

    '        On Error GoTo ErrPart

    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '        'Set PvtDBCn = New Connection
    '        'PvtDBCn.Open StrConn
    '        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
    '        MainClass.RightsToButton(Me, XRIGHT)
    '        MainClass.SetControlsColor(Me)
    '        ADDMode = False
    '        MODIFYMode = False

    '        CurrFormHeight = 7245
    '        CurrFormWidth = 11355

    '        Me.Top = 0
    '        Me.Left = 0
    '        Me.Height = VB6.TwipsToPixelsY(7245)
    '        Me.Width = VB6.TwipsToPixelsX(11355)
    '        optCardNo.Checked = True

    '        If Month(RunDate) <= MSComCtl2.MonthConstants.mvwSeptember Then
    '            txtFrom.Text = RsCompany.Fields("START_DATE").Value
    '            txtTo.Text = CStr(System.Date.FromOADate(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 6, RsCompany.Fields("START_DATE").Value).ToOADate - 1))
    '        Else
    '            txtFrom.Text = CStr(System.Date.FromOADate(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -6, RsCompany.Fields("END_DATE").Value).ToOADate + 1))
    '            txtTo.Text = RsCompany.Fields("END_DATE").Value
    '        End If
    '        FillDeptCombo()
    '        FormatSprd(-1)

    '        FillflxGrid()
    '        FillFlxGridChallan()
    '        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '        Exit Sub
    'ErrPart:
    '        MsgBox(Err.Description)
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '        '    Resume
    '    End Sub
    '    Private Sub frmESIForm6_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
    '        On Error GoTo ErrPart
    '        Dim mReFormWidth As Integer

    '        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

    '        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

    '        Frasprd.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
    '        CurrFormWidth = mReFormWidth

    '        '    MainClass.SetSpreadColor SprdMain, -1
    '        '    MainClass.SetSpreadColor SprdOption, -1
    '        Exit Sub
    'ErrPart:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Sub
    '    Private Function MakeSQL() As String

    '        Dim RsAttn As ADODB.Recordset = Nothing
    '        Dim cntRow As Integer
    '        Dim cntCol As Integer
    '        Dim mContName As String


    '        'TO_CHAR(CASE WHEN SUM(CASE WHEN SALTYPE='S' THEN WDAYS ELSE 0 END) =0 THEN 0 ELSE SUM(CASE WHEN SALTYPE='S' THEN TOT_WAGES ELSE 0 END)/SUM(CASE WHEN SALTYPE='S' THEN WDAYS ELSE 0 END) END)

    '        SqlStr = " SELECT " & vbCrLf & " '',ESIAC_CODE_NUM, EMP_NAME, TO_CHAR(SUM(CASE WHEN SALTYPE='S' THEN WDAYS ELSE 0 END)), " & vbCrLf & " TO_CHAR(SUM(TOT_WAGES)),TO_CHAR(SUM(ESI_AMT)), " & vbCrLf & " TO_CHAR(CASE WHEN SUM(CASE WHEN SALTYPE='S' THEN WDAYS ELSE 0 END) =0 THEN 0 ELSE SUM(TOT_WAGES)/SUM(CASE WHEN SALTYPE='S' THEN WDAYS ELSE 0 END) END)," & vbCrLf & " CASE WHEN LEAVEDATE IS NULL OR LEAVEDATE='' THEN 'YES' ELSE 'NO' END, " & vbCrLf & " CASE WHEN DISPENSARY IS NULL OR DISPENSARY='' THEN 'GURGAON-I' ELSE DISPENSARY END AS DISPENSARY,  " & vbCrLf & " CASE WHEN DOJ>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND DOJ<= '" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "' THEN TO_CHAR(DOJ,'DD/MM/YY') || DECODE(DOJ,NULL,'','-(A) ') END || TO_CHAR(LEAVEDATE,'DD/MM/YY') || DECODE(LEAVEDATE,NULL,'','-(L)') As LDATE" & vbCrLf & " FROM PAY_CONTESI_TRN PFESITRN " & vbCrLf & " WHERE "


    '        If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Checked And RsCompany.Fields("COMPANY_CODE").Value = 1 Then
    '            SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE IN (" & RsCompany.Fields("COMPANY_CODE").Value & ",15,11) "
    '        Else
    '            SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
    '        End If

    '        SqlStr = SqlStr & vbCrLf & " AND EDATE BETWEEN '" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND '" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "'"


    '        ''TO_CHAR(SUM(CASE WHEN SALTYPE='S' THEN TOT_WAGES ELSE 0 END)/SUM(CASE WHEN SALTYPE='S' THEN WDAYS ELSE 0 END))
    '        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            SqlStr = SqlStr & vbCrLf & "AND PFESITRN.CONT_NAME='" & MainClass.AllowSingleQuote(Trim(cboEmployee.Text)) & "' "
    '        End If

    '        If chkSalType.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            SqlStr = SqlStr & vbCrLf & "AND PFESITRN.SALTYPE='" & VB.Left(cboSalType.Text, 1) & "' "
    '        End If
    '        SqlStr = SqlStr & vbCrLf & " GROUP BY EMP_NAME, ESIAC_CODE_NUM,LEAVEDATE,DOJ,DISPENSARY"

    '        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ESI_AMT)>0"

    '        If OptName.Checked = True Then
    '            SqlStr = SqlStr & vbCrLf & " Order by EMP_NAME"
    '        Else
    '            SqlStr = SqlStr & vbCrLf & " Order by ESIAC_CODE_NUM" ''TO_NUMBER(ESIAC_CODE) "
    '        End If

    '        MakeSQL = SqlStr

    '    End Function
    '    Private Sub FillDeptCombo()

    '        Dim RsDept As ADODB.Recordset = Nothing
    '        SqlStr = "Select DISTINCT CONT_NAME from PAY_CONTESI_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CONT_NAME"
    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

    '        If RsDept.EOF = False Then
    '            Do While Not RsDept.EOF
    '                cboEmployee.Items.Add(RsDept.Fields("CONT_NAME").Value)
    '                RsDept.MoveNext()
    '            Loop
    '            cboEmployee.SelectedIndex = 0
    '        End If


    '        cboSalType.Items.Clear()
    '        cboSalType.Items.Add("Salary")
    '        cboSalType.Items.Add("Arrear")
    '        cboSalType.Items.Add("OT")
    '        cboSalType.Items.Add("VT")
    '        cboSalType.Items.Add("F&F")
    '        cboSalType.SelectedIndex = 0
    '        Exit Sub
    'ERR1:
    '        MsgInformation(Err.Description)
    '    End Sub
    '    Private Sub FormatSprd(ByRef mRow As Integer)

    '        On Error GoTo ERR1

    '        With SprdMain
    '            .MaxCols = ColRemarks

    '            .Row = mRow
    '            .Col = ColCodeNo
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColCodeNo, 5)
    '            .ColHidden = True

    '            .Col = ColAcctNo
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColAcctNo, 9)

    '            .Col = ColName
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColName, 18)
    '            .ColsFrozen = ColName

    '            .Col = ColWDays
    '            .CellType = SS_CELL_TYPE_FLOAT
    '            .TypeFloatDecimalChar = Asc(".")
    '            .TypeFloatMax = CDbl("999999999.99")
    '            .TypeFloatMin = CDbl("-999999999.99")
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
    '            .TypeFloatDecimalPlaces = 1
    '            .set_ColWidth(ColWDays, 8)

    '            .Col = ColWages
    '            .CellType = SS_CELL_TYPE_FLOAT
    '            .TypeFloatDecimalChar = Asc(".")
    '            .TypeFloatMax = CDbl("999999999.99")
    '            .TypeFloatMin = CDbl("-999999999.99")
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
    '            .TypeFloatDecimalPlaces = 2
    '            .set_ColWidth(ColWages, 9)

    '            .Col = ColEmpCont
    '            .CellType = SS_CELL_TYPE_FLOAT
    '            .TypeFloatDecimalChar = Asc(".")
    '            .TypeFloatMax = CDbl("999999999.99")
    '            .TypeFloatMin = CDbl("-999999999.99")
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
    '            .TypeFloatDecimalPlaces = 2
    '            .set_ColWidth(ColEmpCont, 9)

    '            .Col = ColDWages
    '            .CellType = SS_CELL_TYPE_FLOAT
    '            .TypeFloatDecimalChar = Asc(".")
    '            .TypeFloatMax = CDbl("999999999.99")
    '            .TypeFloatMin = CDbl("-999999999.99")
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
    '            .TypeFloatDecimalPlaces = 2
    '            .set_ColWidth(ColDWages, 8)


    '            .Col = ColWorking
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '            .TypeEditMultiLine = True
    '            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
    '            .set_ColWidth(ColWorking, 15)

    '            .Col = ColDispensary
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColDispensary, 8)

    '            .Col = ColRemarks
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColRemarks, 10)
    '        End With

    '        '    MainClass.ProtectCell sprdMain, 1, sprdMain.MaxRows, 1, sprdMain.MaxCols
    '        '    MainClass.SetSpreadColor sprdMain, mRow

    '        FillHeading()

    '        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 0, SprdMain.MaxRows)
    '        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
    '        SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
    '        MainClass.SetSpreadColor(SprdMain, mRow)



    '        Exit Sub
    'ERR1:
    '        If Err.Number = -2147418113 Then Resume Next
    '        MsgBox(Err.Description, MsgBoxStyle.Information)
    '    End Sub

    '    Private Sub txtFrom_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrom.Leave
    '        'FillFlxGridChallan
    '    End Sub

    '    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
    '        Dim Cancel As Boolean = eventArgs.Cancel
    '        If Not IsDate(txtFrom.Text) Then
    '            MsgInformation("Please enter the vaild date.")
    '            Cancel = True
    '            GoTo EventExitSub
    '        End If
    '        txtFrom.Text = VB6.Format(txtFrom.Text, "dd/mm/yyyy")
    'EventExitSub:
    '        eventArgs.Cancel = Cancel
    '    End Sub


    '    Private Sub txtTo_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTo.Leave
    '        'FillFlxGridChallan
    '    End Sub

    '    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
    '        Dim Cancel As Boolean = eventArgs.Cancel
    '        If Not IsDate(txtTo.Text) Then
    '            MsgInformation("Please enter the vaild date.")
    '            Cancel = True
    '            GoTo EventExitSub
    '        End If
    '        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
    'EventExitSub:
    '        eventArgs.Cancel = Cancel
    '    End Sub
    '    Private Sub FillflxGrid()
    '        With flxGridChallan
    '            .Cols = 4 + 2
    '            .Rows = 1

    '            .Row = 0
    '            .set_RowHeight(0, 500)
    '            .WordWrap = True

    '            .Col = 0
    '            .Text = "S. No."
    '            .set_ColWidth(0, 400)

    '            .Col = 1
    '            .Text = "Period"
    '            .set_ColWidth(1, 900)

    '            .Col = 2
    '            .Text = "Challan Date"
    '            .set_ColWidth(3, 1150)

    '            .Col = 3
    '            .Text = "Challan Amount"
    '            .set_ColWidth(3, 1200)

    '            .Col = 4
    '            .Text = "Emper Share"
    '            .set_ColWidth(4, 1)

    '            .Col = 5
    '            .Text = "Emp Share"
    '            .set_ColWidth(5, 1)

    '        End With
    '    End Sub
    '    Private Sub FillFlxGridChallan()

    '        Dim RSChallan As ADODB.Recordset
    '        Dim mYMFrom As Integer
    '        Dim mYMTo As Integer
    '        Dim cntRow As Integer

    '        flxGridChallan.Rows = 2

    '        mYMFrom = CInt(Year(CDate(txtFrom.Text)) & VB6.Format(Month(CDate(txtFrom.Text)), "00"))
    '        mYMTo = CInt(Year(CDate(txtTo.Text)) & VB6.Format(Month(CDate(txtTo.Text)), "00"))

    '        SqlStr = " SELECT * FROM PAY_ESICHALLAN_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf & " YM BETWEEN " & mYMFrom & " AND " & vbCrLf & " " & mYMTo & " ORDER BY YM"

    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSChallan, ADODB.LockTypeEnum.adLockOptimistic)

    '        If RSChallan.EOF = False Then
    '            cntRow = 1

    '            With flxGridChallan
    '                Do While Not RSChallan.EOF
    '                    .Row = cntRow
    '                    .Col = 0
    '                    .Text = CStr(cntRow)

    '                    .Col = 1
    '                    .Text = Mid(MonthName(RSChallan.Fields("CHALLANMONTH").Value), 1, 3) & ", " & RSChallan.Fields("CHALLANYEAR").Value

    '                    .Col = 2
    '                    .Text = IIf(IsDate(RSChallan.Fields("CHALLANDATE").Value), RSChallan.Fields("CHALLANDATE").Value, "")

    '                    .Col = 3
    '                    .Text = VB6.Format(IIf(IsDbNull(RSChallan.Fields("TotalAmount").Value), "", RSChallan.Fields("TotalAmount").Value), "0.00")

    '                    .Col = 4
    '                    .Text = VB6.Format(IIf(IsDbNull(RSChallan.Fields("EMPERSHARE").Value), "", RSChallan.Fields("EMPERSHARE").Value), "0.00")

    '                    .Col = 5
    '                    .Text = VB6.Format(IIf(IsDbNull(RSChallan.Fields("EMPSHARE").Value), "", RSChallan.Fields("EMPSHARE").Value), "0.00")

    '                    RSChallan.MoveNext()

    '                    If RSChallan.EOF = False Then
    '                        cntRow = cntRow + 1
    '                        flxGridChallan.Rows = cntRow + 1
    '                        '                    flxGridChallan.Rows = cntRow
    '                    End If

    '                Loop
    '            End With
    '            FlxGridColTotal(flxGridChallan, 3, 3)
    '            flxGridChallan.Row = flxGridChallan.Rows - 1
    '            flxGridChallan.Col = 1
    '            flxGridChallan.Text = "TOTAL :"
    '        Else
    '            flxGridChallan.Rows = 7
    '        End If
    '    End Sub
    '    Private Sub ReportForBackPrint(ByRef Mode As Crystal.DestinationConstants)


    '        On Error GoTo ERR1
    '        Dim index1 As Integer
    '        Dim SqlStr As String=""=""
    '        Dim mTitle As String
    '        Dim mSubTitle As String
    '        PubDBCn.Errors.Clear()


    '        'Insert Data from Grid to PrintDummyData Table...


    '        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows - 2, ColCodeNo, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


    '        'Select Record for print...

    '        SqlStr = ""

    '        SqlStr = FetchRecordForReport(SqlStr)

    '        mSubTitle = ""
    '        mTitle = ""
    '        'clear all formulas
    '        MainClass.ClearCRptFormulas(Report1)
    '        Call ShowReport(SqlStr, "ESIFORM6.Rpt", Mode, mTitle, mSubTitle)

    '        Exit Sub
    'ERR1:
    '        MsgInformation(Err.Description)
    '        If Err.Number = 32755 Or Err.Number = 20507 Then
    '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '            Exit Sub
    '        End If

    '        'Resume
    '    End Sub
    '    Private Sub ReportForFrontPrint(ByRef Mode As Crystal.DestinationConstants)


    '        On Error GoTo ERR1
    '        Dim ColRow As Integer
    '        Dim TotalAmt As Double
    '        Dim EmperAmt As Double
    '        Dim EmpAmt As Double
    '        Dim SqlStr As String=""=""
    '        Dim mTitle As String
    '        Dim mSubTitle As String
    '        Dim mYMFrom As Integer
    '        Dim mYMTo As Integer
    '        Dim mChallanAmount As Double
    '        Dim RSChallan As ADODB.Recordset

    '        PubDBCn.Errors.Clear()

    '        'clear all formulas
    '        MainClass.ClearCRptFormulas(Report1)

    '        mYMFrom = CInt(Year(CDate(txtFrom.Text)) & VB6.Format(Month(CDate(txtFrom.Text)), "00"))
    '        mYMTo = CInt(Year(CDate(txtTo.Text)) & VB6.Format(Month(CDate(txtTo.Text)), "00"))

    '        SqlStr = " SELECT CHALLANDATE, SUM(TOTALAMOUNT) As TOTALAMOUNT, " & vbCrLf & " SUM(EMPERSHARE) As EMPERSHARE , SUM(EMPSHARE) As EMPSHARE " & vbCrLf & " FROM PAY_ESICHALLAN_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf & " YM BETWEEN " & mYMFrom & " AND " & mYMTo & "" & vbCrLf & " GROUP BY CHALLANDATE ORDER BY CHALLANDATE"

    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSChallan, ADODB.LockTypeEnum.adLockOptimistic)

    '        With flxGridChallan
    '            ColRow = 1
    '            Do While RSChallan.EOF = False
    '                .Row = ColRow

    '                .Col = 2
    '                SqlStr = MainClass.AllowSingleQuote(Trim(IIf(IsDbNull(RSChallan.Fields("CHALLANDATE").Value), "", RSChallan.Fields("CHALLANDATE").Value)))
    '                MainClass.AssignCRptFormulas(Report1, "CHALLAN" & ColRow & "='" & SqlStr & "'")

    '                .Col = 3
    '                mChallanAmount = IIf(IsDbNull(RSChallan.Fields("TotalAmount").Value), 0, RSChallan.Fields("TotalAmount").Value)
    '                TotalAmt = TotalAmt + CDbl(Val(CStr(mChallanAmount)))
    '                SqlStr = VB6.Format(mChallanAmount, "0.00")
    '                MainClass.AssignCRptFormulas(Report1, "Amount" & ColRow & "='" & SqlStr & "'")

    '                .Col = 4
    '                EmperAmt = EmperAmt + CDbl(Val(IIf(IsDbNull(RSChallan.Fields("EMPERSHARE").Value), 0, RSChallan.Fields("EMPERSHARE").Value)))

    '                .Col = 5
    '                EmpAmt = EmpAmt + CDbl(Val(IIf(IsDbNull(RSChallan.Fields("EMPSHARE").Value), 0, RSChallan.Fields("EMPSHARE").Value)))
    '                RSChallan.MoveNext()
    '                If RSChallan.EOF = False Then
    '                    ColRow = ColRow + 1
    '                End If
    '            Loop
    '        End With

    '        MainClass.AssignCRptFormulas(Report1, "TotalAmt='" & VB6.Format(TotalAmt, "0.00") & "'")

    '        SqlStr = "Period : From "
    '        SqlStr = SqlStr & MonthName(Month(CDate(txtFrom.Text))) & ", " & Year(CDate(txtFrom.Text))
    '        SqlStr = SqlStr & " To " & MonthName(Month(CDate(txtTo.Text))) & ", " & Year(CDate(txtTo.Text))

    '        MainClass.AssignCRptFormulas(Report1, "Period='" & SqlStr & "'")

    '        SqlStr = "Total contribution amounting to Rs. " & VB6.Format(TotalAmt, "0.00")
    '        SqlStr = SqlStr & " comprising of Rs. " & VB6.Format(EmperAmt, "0.00")
    '        SqlStr = SqlStr & " as Employers share and Rs. " & VB6.Format(EmpAmt, "0.00")
    '        SqlStr = SqlStr & " as Employees share (Total of Col. 6 of the Return) paid as under : "
    '        MainClass.AssignCRptFormulas(Report1, "Line1='" & SqlStr & "'")

    '        MainClass.AssignCRptFormulas(Report1, "ESIEST='" & IIf(IsDbNull(RsCompany.Fields("ESIEST").Value), "", RsCompany.Fields("ESIEST").Value) & "'")

    '        mSubTitle = "EMPLOYEES STATE INSURANCE CORPORATION"
    '        mTitle = "FORM - 5"
    '        Call ShowReport(SqlStr, "ESIFORM6B.Rpt", Mode, mTitle, mSubTitle)

    '        Exit Sub
    'ERR1:
    '        MsgInformation(Err.Description)
    '        '    Resume
    '        If Err.Number = 32755 Or Err.Number = 20507 Then
    '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '            Exit Sub
    '        End If

    '        'Resume
    '    End Sub
    '    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
    '        Report1.SQLQuery = mSqlStr
    '        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

    '        ' Report1.CopiesToPrinter = PrintCopies
    '        Report1.WindowShowGroupTree = False
    '        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
    '        Report1.Action = 1
    '    End Sub
End Class
