Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamFuelConsumption
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColMachineNo As Short = 1
    Private Const ColMachineDesc As Short = 2
    Private Const ColDate As Short = 3
    Private Const ColFuelType As Short = 4
    Private Const ColFuelConsOn As Short = 5
    Private Const ColFuelCons As Short = 6
    Private Const ColMeterReading As Short = 7
    Private Const ColNet As Short = 8
    Private Const ColTotFuelCons As Short = 9
    Private Const ColFuelRate As Short = 10
    Private Const ColTotAmount As Short = 11
    Private Const ColRemarks As Short = 12
    Private Const ColEmpCode As Short = 13

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboFuelType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFuelType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboFuelType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFuelType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAllMachine_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllMachine.CheckStateChanged
        Call PrintStatus(False)
        If chkAllMachine.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMachineNo.Enabled = False
            cmdSearchMachine.Enabled = False
        Else
            txtMachineNo.Enabled = True
            cmdSearchMachine.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnFuelConsumption(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnFuelConsumption(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnFuelConsumption(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Machines' Fuel Consumption Report"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        If cboFuelType.Text <> "Both" Then
            mSubTitle = mSubTitle & " [ Fuel Type : " & cboFuelType.Text & " ]"
        End If
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\FuelConsumption.rpt"
        SqlStr = MakeSQL
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdSearchMachine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachine.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtMachineNo.Text = AcName1
            lblMachine.text = AcName
        End If
        txtMachineNo.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamFuelConsumption_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Machines' Fuel Consumption Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamFuelConsumption_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        cboFuelType.Items.Clear()
        cboFuelType.Items.Add("Electricity")
        cboFuelType.Items.Add("Diesel")
        cboFuelType.Items.Add("Both")
        cboFuelType.SelectedIndex = 2

        chkAllMachine.CheckState = System.Windows.Forms.CheckState.Checked
        txtMachineNo.Enabled = False
        cmdSearchMachine.Enabled = False
        txtDateFrom.Text = "01/" & VB6.Format(RunDate, "MM/YYYY")
        txtDateTo.Text = VB6.Format(MainClass.LastDay(Month(RunDate), Year(RunDate)), "00") & "/" & VB6.Format(RunDate, "MM/YYYY")
        Call PrintStatus(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamFuelConsumption_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtMachineNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.DoubleClick
        Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Machine Does Not Exist In Master.")
            Cancel = True
        Else
            lblMachine.text = MasterNo
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColEmpCode
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColDate, 8)
            .ColHidden = IIf(optSummary.Checked = True, True, False)

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColMachineNo, 8)

            .Col = ColMachineDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColMachineDesc, 25)

            .Col = ColFuelType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColFuelType, 8)

            .Col = ColFuelConsOn
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColFuelConsOn, 11.5)

            .Col = ColFuelCons
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColFuelCons, 10)

            .Col = ColMeterReading
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColMeterReading, 10)

            .Col = ColNet
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColNet, 8)

            .Col = ColTotFuelCons
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = False
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 0
            .set_ColWidth(ColTotFuelCons, 8)

            .Col = ColFuelRate
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = False
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColFuelRate, 8)

            .Col = ColTotAmount
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            '        .TypeEditLen = 255
            '        .TypeEditMultiLine = False
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColTotAmount, 8)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColRemarks, 20)
            .ColHidden = IIf(optSummary.Checked = True, True, False)

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmpCode, 8)
            .ColHidden = IIf(optSummary.Checked = True, True, False)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim TotCol As Double

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        With SprdMain
            .MaxRows = .MaxRows + 2

            .Col = ColTotFuelCons
            For cntRow = 1 To .MaxRows - 2
                .Row = cntRow
                TotCol = TotCol + IIf(IsNumeric(.Text), .Text, 0)
            Next
            .Row = .MaxRows
            .Text = CStr(TotCol)

            .Col = ColTotAmount
            For cntRow = 1 To .MaxRows - 2
                .Row = cntRow
                TotCol = TotCol + IIf(IsNumeric(.Text), .Text, 0)
            Next
            .Row = .MaxRows
            .Text = CStr(TotCol)

            .Col = ColMachineDesc
            .Row = .MaxRows
            .Text = "TOTAL :"
            MainClass.ProtectCell(SprdMain, 0, .MaxRows, 0, .MaxCols)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)
            .BlockMode = False
        End With

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        'Private Const ColMachineNo = 1
        'Private Const ColMachineDesc = 2
        'Private Const ColDate = 3
        'Private Const ColFuelType = 4
        'Private Const ColFuelConsOn = 5
        'Private Const ColFuelCons = 6
        'Private Const ColMeterReading = 7
        'Private Const ColNet = 8
        'Private Const ColTotFuelCons = 9
        'Private Const ColFuelRate = 10
        'Private Const ColTotAmount = 11
        'Private Const ColRemarks = 12
        'Private Const ColEmpCode = 13

        If optDetail.Checked = True Then
            MakeSQL = " SELECT MAN_FUELCONSUMP_TRN.MACHINE_NO, MAN_MACHINE_MST.MACHINE_DESC, DOC_DATE, " & vbCrLf & " DECODE(MAN_FUELCONSUMP_TRN.FUEL_TYPE,'E','Electricity','D','Diesel') AS FUEL_TYPE, " & vbCrLf & " DECODE(MAN_FUELCONSUMP_TRN.FUEL_CONS_ON,'H','Hour Basis','U','Unit Basis') AS FUEL_CONS_ON, " & vbCrLf & " MAN_FUELCONSUMP_TRN.FUEL_CONS, " & vbCrLf & " CASE WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='H' THEN MAN_FUELCONSUMP_TRN.HOUR_METER_READING " & vbCrLf & " WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='U' THEN MAN_FUELCONSUMP_TRN.UNIT_METER_READING END AS METER_READING, " & vbCrLf & " CASE WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='H' THEN MAN_FUELCONSUMP_TRN.NET_HOURS " & vbCrLf & " WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='U' THEN MAN_FUELCONSUMP_TRN.NET_UNITS END AS NET, " & vbCrLf & " MAN_FUELCONSUMP_TRN.TOT_FUEL_CONSUMED, MAN_FUELCONSUMP_TRN.FUEL_RATE, MAN_FUELCONSUMP_TRN.TOT_AMOUNT, " & vbCrLf & " MAN_FUELCONSUMP_TRN.REMARKS, MAN_FUELCONSUMP_TRN.EMP_CODE "
        Else
            MakeSQL = " SELECT MAN_FUELCONSUMP_TRN.MACHINE_NO, MAN_MACHINE_MST.MACHINE_DESC, '', " & vbCrLf & " DECODE(MAN_FUELCONSUMP_TRN.FUEL_TYPE,'E','Electricity','D','Diesel') AS FUEL_TYPE, " & vbCrLf & " DECODE(MAN_FUELCONSUMP_TRN.FUEL_CONS_ON,'H','Hour Basis','U','Unit Basis') AS FUEL_CONS_ON, " & vbCrLf & " SUM(MAN_FUELCONSUMP_TRN.FUEL_CONS), " & vbCrLf & " MAX(CASE WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='H' THEN MAN_FUELCONSUMP_TRN.HOUR_METER_READING " & vbCrLf & " WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='U' THEN MAN_FUELCONSUMP_TRN.UNIT_METER_READING END) AS METER_READING, " & vbCrLf & " SUM(CASE WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='H' THEN MAN_FUELCONSUMP_TRN.NET_HOURS " & vbCrLf & " WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='U' THEN MAN_FUELCONSUMP_TRN.NET_UNITS END) AS NET, " & vbCrLf & " SUM(MAN_FUELCONSUMP_TRN.TOT_FUEL_CONSUMED), AVG(MAN_FUELCONSUMP_TRN.FUEL_RATE), SUM(MAN_FUELCONSUMP_TRN.TOT_FUEL_CONSUMED*MAN_FUELCONSUMP_TRN.FUEL_RATE), " & vbCrLf & " '', '' "

        End If

        MakeSQL = MakeSQL & vbCrLf & " FROM MAN_FUELCONSUMP_TRN, MAN_MACHINE_MST " & vbCrLf & " WHERE MAN_FUELCONSUMP_TRN.COMPANY_CODE = MAN_MACHINE_MST.COMPANY_CODE " & vbCrLf & " AND MAN_FUELCONSUMP_TRN.MACHINE_NO = MAN_MACHINE_MST.MACHINE_NO " & vbCrLf & " AND MAN_FUELCONSUMP_TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MAN_FUELCONSUMP_TRN.DOC_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND MAN_FUELCONSUMP_TRN.DOC_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAllMachine.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachineNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAN_FUELCONSUMP_TRN.MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        End If

        If cboFuelType.Text <> "Both" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAN_FUELCONSUMP_TRN.FUEL_TYPE='" & VB.Left(cboFuelType.Text, 1) & "'"
        End If

        If optDetail.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY MAN_FUELCONSUMP_TRN.MACHINE_NO, MAN_FUELCONSUMP_TRN.DOC_DATE "
        Else
            MakeSQL = MakeSQL & vbCrLf & " GROUP BY  MAN_FUELCONSUMP_TRN.MACHINE_NO, MAN_MACHINE_MST.MACHINE_DESC, " & vbCrLf & " DECODE(MAN_FUELCONSUMP_TRN.FUEL_TYPE,'E','Electricity','D','Diesel'), " & vbCrLf & " DECODE(MAN_FUELCONSUMP_TRN.FUEL_CONS_ON,'H','Hour Basis','U','Unit Basis')" '', " & vbCrLf |                & " CASE WHEN  " & vbCrLf |                & " MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='H' THEN MAN_FUELCONSUMP_TRN.HOUR_METER_READING " & vbCrLf |                & " WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='U' THEN MAN_FUELCONSUMP_TRN.UNIT_METER_READING END, " & vbCrLf |                & " CASE WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='H' THEN MAN_FUELCONSUMP_TRN.NET_HOURS " & vbCrLf |                & " WHEN MAN_FUELCONSUMP_TRN.FUEL_CONS_ON='U' THEN MAN_FUELCONSUMP_TRN.NET_UNITS END"

            MakeSQL = MakeSQL & vbCrLf & " ORDER BY MAN_FUELCONSUMP_TRN.MACHINE_NO"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If Trim(txtDateFrom.Text) = "" Then
            MsgBox("From Date Is Blank")
            txtDateFrom.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If Trim(txtDateTo.Text) = "" Then
            MsgBox("To Date Is Blank")
            txtDateTo.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If chkAllMachine.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtMachineNo.Text) = "" Then
                MsgInformation("Machine is blank.")
                txtMachineNo.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
End Class
