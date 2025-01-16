Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGenRecord
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColOnDate As Short = 1
    Private Const ColOnTime As Short = 2
    Private Const ColOffDate As Short = 3
    Private Const ColOffTime As Short = 4
    Private Const ColTotalTime As Short = 5
    Private Const ColReadingDate As Short = 6
    Private Const ColReadingTime As Short = 7
    Private Const ColRPM As Short = 8
    Private Const ColHoursMtrReading As Short = 9
    Private Const ColUnitMtrReading As Short = 10
    Private Const ColOilPress As Short = 11
    Private Const ColOilTemp As Short = 12
    Private Const ColWaterTemp As Short = 13
    Private Const ColFrequency As Short = 14
    Private Const ColAMPS As Short = 15
    Private Const ColHSPLevel As Short = 16
    Private Const ColLoad As Short = 17
    Private Const ColRHM As Short = 18
    Private Const ColOilTempOut As Short = 19
    Private Const ColVoltage As Short = 20
    Private Const ColKWH As Short = 21
    Private Const ColTemp1 As Short = 22
    Private Const ColTemp2 As Short = 23
    Private Const ColTemp3 As Short = 24
    Private Const ColTemp4 As Short = 25
    Private Const ColTemp5 As Short = 26
    Private Const ColTemp6 As Short = 27
    Private Const ColOilLevel As Short = 28
    Private Const ColRemarks As Short = 29
    Private Const ColDoneBy As Short = 30

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim RsIMTE As ADODB.Recordset

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDateCondition_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDateCondition.SelectedIndexChanged
        If cboDateCondition.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboDateCondition.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboDateCondition.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboDateCondition.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboDateCondition.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGenRec(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGenRec(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnGenRec(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        If lblType.Text = "2" Then
            mTitle = "250 KVA Generators Data Recording"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GenRec250.rpt"
        ElseIf lblType.Text = "8" Then
            mTitle = "800 KVA Generators Data Recording"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GenRec800.rpt"
        ElseIf lblType.Text = "C" Then
            mTitle = "Compressor Data Recording"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\CompRec.rpt"
        End If

        If cboDateCondition.Text = "Between" Then
            mSubTitle = mSubTitle & " [Reading Date Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboDateCondition.Text = "After" Then
            mSubTitle = mSubTitle & " [Reading Date After  " & txtDate1.Text & " ]"
        End If
        If cboDateCondition.Text = "Before" Then
            mSubTitle = mSubTitle & " [Reading Date Before  " & txtDate1.Text & " ]"
        End If
        If cboDateCondition.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Reading Date On  " & txtDate1.Text & " ]"
        End If

        SqlStr = MakeSQL1
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

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Sub frmParamGenRecord_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblType.Text = "2" Then
            Me.Text = "250 KVA Generators Data Recording"
        ElseIf lblType.Text = "8" Then
            Me.Text = "800 KVA Generators Data Recording"
        ElseIf lblType.Text = "C" Then
            Me.Text = "Compressor Data Recording"
        End If
        Call SetSprdHeading()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetSprdHeading()
        On Error GoTo ERR1
        Dim I As Short

        With SprdMain
            If lblType.Text = "2" Then
                For I = ColHSPLevel To ColOilLevel
                    .Col = I
                    .ColHidden = True
                Next
            ElseIf lblType.Text = "8" Then
                .Col = ColAMPS
                .ColHidden = True
                .Col = ColOilLevel
                .ColHidden = True
            ElseIf lblType.Text = "C" Then
                .Col = ColUnitMtrReading
                .ColHidden = True
                .Col = ColRPM
                .ColHidden = True
                For I = ColFrequency To ColTemp6
                    .Col = I
                    .ColHidden = True
                Next
                .Row = 0
                .Col = ColOilPress
                .Text = "Line Pressure"
                .Col = ColOilTemp
                .Text = "Discharge Temp."
                .Col = ColWaterTemp
                .Text = "Sump Pressure"
            End If
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub frmParamGenRecord_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Width = VB6.TwipsToPixelsX(11565)

        Call FillCbo()

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()
        cboDateCondition.Items.Clear()
        cboDateCondition.Items.Add("None")
        cboDateCondition.Items.Add("Between")
        cboDateCondition.Items.Add("After")
        cboDateCondition.Items.Add("Before")
        cboDateCondition.Items.Add("On Date")
        cboDateCondition.SelectedIndex = 0
    End Sub

    Private Sub frmParamGenRecord_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        Dim I As Short

        With SprdMain
            .MaxCols = ColDoneBy
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColOnDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColOnDate, 8)

            .Col = ColOnTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColOnTime, 5)

            .Col = ColOffDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColOffDate, 8)

            .Col = ColOffTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColOffTime, 5)

            .Col = ColTotalTime
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColTotalTime, 5)

            .Col = ColReadingDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColReadingDate, 8)

            .Col = ColReadingTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColReadingTime, 6)

            .ColsFrozen = ColReadingTime

            .Col = ColRPM
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColRPM, 7)

            .Col = ColHoursMtrReading
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColHoursMtrReading, 8)

            .Col = ColUnitMtrReading
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColUnitMtrReading, 8)

            For I = ColOilPress To ColKWH
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .set_ColWidth(I, 7)
            Next

            For I = ColTemp1 To ColTemp6
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .set_ColWidth(I, 9)
            Next

            .Col = ColOilLevel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(ColOilLevel, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 15)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        MakeSQL = " SELECT ON_DATE, TO_CHAR(ON_TIME,'HH24:MI'),OFF_DATE,TO_CHAR(OFF_TIME,'HH24:MI'),TOTAL_TIME,  " & vbCrLf & " READING_DATE,TO_CHAR(READING_TIME,'HH24:MI'),RPM,HRS_MTR_READING,UNIT_MTR_READING,OIL_PRESSURE, " & vbCrLf & " OIL_TEMP_IN,WATER_TEMP,FREQUENCY,AMPS,HSP_LEVEL, " & vbCrLf & " LOAD,RHM,OIL_TEMP_OUT,VOLTAGE,KWH, " & vbCrLf & " CYLINDER1_TEMP,CYLINDER2_TEMP,CYLINDER3_TEMP,CYLINDER4_TEMP,CYLINDER5_TEMP, " & vbCrLf & " CYLINDER6_TEMP,OIL_LEVEL,MAN_GENREC_DET.REMARKS,DONE_BY " & vbCrLf & " FROM MAN_GENREC_HDR,MAN_GENREC_DET " & vbCrLf & " WHERE MAN_GENREC_HDR.AUTO_KEY_GENREC=MAN_GENREC_DET.AUTO_KEY_GENREC " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_TYPE='" & lblType.Text & "'"

        '            & " AND SUBSTR(MAN_GENREC_HDR.AUTO_KEY_GENREC,LENGTH(MAN_GENREC_HDR.AUTO_KEY_GENREC)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
        '
        If Trim(txtMachineNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
        End If

        If cboDateCondition.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND ON_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboDateCondition.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND ON_DATE> TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDateCondition.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND ON_DATE< TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDateCondition.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND ON_DATE= TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY ON_DATE,ON_TIME,SERIAL_NO "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQL1() As String

        On Error GoTo ERR1

        MakeSQL1 = " SELECT IH.*,ID.*,MACHINE_DESC " & vbCrLf & " FROM MAN_GENREC_HDR IH,MAN_GENREC_DET ID,MAN_MACHINE_MST" & vbCrLf & " WHERE IH.AUTO_KEY_GENREC=ID.AUTO_KEY_GENREC " & vbCrLf & " AND IH.COMPANY_CODE=MAN_MACHINE_MST.COMPANY_CODE " & vbCrLf & " AND IH.MACHINE_NO=MAN_MACHINE_MST.MACHINE_NO" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_TYPE='" & lblType.Text & "'"

        '            & " AND SUBSTR(IH.AUTO_KEY_GENREC,LENGTH(IH.AUTO_KEY_GENREC)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
        '
        If Trim(txtMachineNo.Text) <> "" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND IH.MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
        End If

        If cboDateCondition.Text = "Between" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ON_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboDateCondition.Text = "After" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ON_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDateCondition.Text = "Before" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ON_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDateCondition.Text = "On Date" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ON_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL1 = MakeSQL1 & vbCrLf & " ORDER BY ON_DATE,ON_TIME,SERIAL_NO "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If Trim(txtMachineNo.Text) = "" Then
            MsgBox("Please Select Machine")
            txtMachineNo.Focus()
            Exit Function
        End If
        If cboDateCondition.Text = "Between" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
            If Not IsDate(txtDate2.Text) Then
                MsgBox("Date2 is Blank.")
                txtDate2.Focus()
                Exit Function
            End If
        End If
        If cboDateCondition.Text = "After" Or cboDateCondition.Text = "Before" Or cboDateCondition.Text = "On Date" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtMachineNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.DoubleClick
        Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
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

    Public Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Machine Does Not Exist.")
            Cancel = True
        Else
            lblMachine.text = MasterNo
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
