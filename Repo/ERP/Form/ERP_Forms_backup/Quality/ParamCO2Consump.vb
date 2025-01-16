Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamCO2Consump
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColDept As Short = 1
    Private Const ColDate As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColCarbonKG As Short = 5
    Private Const ColItemCodeCYL As Short = 6
    Private Const ColCylDesc As Short = 7
    Private Const ColCylNo As Short = 8
    Private Const ColCylKG As Short = 9
    Private Const ColTotCylKG As Short = 10
    Private Const ColRemarks As Short = 11
    Private Const ColSign As Short = 12

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim RsIMTE As ADODB.Recordset

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub chkAllDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDept.CheckStateChanged
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDept.Enabled = False
            cmdSearchDept.Enabled = False
        Else
            txtDept.Enabled = True
            cmdSearchDept.Enabled = True
        End If
    End Sub

    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemCode.Enabled = False
            cmdSearchItem.Enabled = False
        Else
            txtItemCode.Enabled = True
            cmdSearchItem.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPowerConsump(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPowerConsump(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDept.Text) = "" Then
            MsgBox("Please Select Department")
            txtDept.Focus()
            Exit Function
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItemCode.Text) = "" Then
            MsgBox("Please Select Item Code")
            txtItemCode.Focus()
            Exit Function
        End If


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

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub ReportOnPowerConsump(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        If optSummary.Checked = True Then
            mTitle = "Dept Wise Power Consumption Report (Summary)"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\DieselConsumpSumm.rpt"
        Else
            mTitle = "Dept Wise Power Consumption Report"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\DieselConsumpfill.rpt"
        End If

        mSubTitle = mSubTitle & " [Consumption Date Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"

        '    SqlStr = MakeSQL

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr


        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

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

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDept.text = AcName
        End If
        txtDept.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemCode.Text = AcName1
            lblItemName.text = AcName
        End If
        txtItemCode.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain(-1)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Sub frmParamCO2Consump_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Dept Wise CO2 Consumption Report"

        optDetail.Checked = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamCO2Consump_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        txtDate1.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDate2.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        Call FillCbo()

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()

        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0



        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub frmParamCO2Consump_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth), 11592.4, 763)
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamCO2Consump_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
            .MaxCols = ColSign
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(ColDept, 15)

            .Col = ColDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDate, 8)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemDesc, 20)

            .Col = ColCarbonKG
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColCarbonKG, 8)

            .Col = ColItemCodeCYL
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemCodeCYL, 8)

            .Col = ColCylDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColCylDesc, 20)

            .Col = ColCylNo
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColCylNo, 8)

            .Col = ColCylKG
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColCylKG, 8)

            .Col = ColTotCylKG
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColTotCylKG, 8)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 18)

            .Col = ColSign
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False
            .set_ColWidth(ColSign, 8)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            Call SetHeading()

        End With
    End Sub

    Private Sub SetHeading()
        'Dim cntCol As Long
        'Dim I As Integer
        '
        '    With SprdMain
        '        .Row = 0
        '
        '        .Col = ColDept
        '        .Text = "Department"
        '
        '        .Col = ColType
        '        .Text = "Type"
        '
        '        .Col = ColDate
        '        .ColHidden = IIf(optSummary.Value = True, True, False)
        '        .Text = "Date"
        '
        '        .Col = ColItemCode
        '        If optSummary.Value = True Then
        '            .Text = "Total Hours"
        '        Else
        '            .Text = "Hour/Day"
        '        End If
        '
        '        .Col = ColItemDesc
        '        .ColHidden = IIf(optSummary.Value = True, True, False)
        '        .Text = "Meter Reading"
        '
        '        .Col = ColCarbonKG
        '        .Text = "Total Unit Consumed"
        '
        '        .Col = ColItemCodeCYL
        '        .Text = "Rate/Unit"
        '
        '        .Col = ColCylDesc
        '        .Text = "Total Cost"
        '
        '        .Col = ColRemarks
        '        .ColHidden = IIf(optSummary.Value = True, True, False)
        '        .Text = "Remarks"
        '
        '        .Col = ColSign
        '        .ColHidden = IIf(optSummary.Value = True, True, False)
        '        .Text = "Signature Emp Code"
        '
        '    End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If optSummary.Checked = True Then
            SqlStr = MakeSQLSumm
        Else
            SqlStr = MakeSQL
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQLSumm() As String

        On Error GoTo ERR1
        Dim mDivision As Double

        MakeSQLSumm = " SELECT DEPT.DEPT_DESC, '', " & vbCrLf & " ITEM_CODE_LIQ, IMST1.ITEM_SHORT_DESC, SUM(CARBON_KG) AS CARBON_KG, " & vbCrLf & " ITEM_CODE_CYL, IMST2.ITEM_SHORT_DESC,SUM(CYLINDER_NO), SUM(EACH_CYLINDER_KG), SUM(TOT_CYLINDER_KG)," & vbCrLf & " '', '' " & vbCrLf & " FROM MAN_CO2COSUMP_TRN TRN, INV_ITEM_MST IMST1, INV_ITEM_MST IMST2, PAY_DEPT_MST DEPT" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=IMST1.COMPANY_CODE(+)" & vbCrLf & " AND TRN.ITEM_CODE_LIQ=IMST1.ITEM_CODE(+)" & vbCrLf & " AND TRN.COMPANY_CODE=IMST2.COMPANY_CODE(+)" & vbCrLf & " AND TRN.ITEM_CODE_CYL=IMST2.ITEM_CODE(+)" & vbCrLf & " AND TRN.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND TRN.DEPT_CODE=DEPT.DEPT_CODE"

        If Trim(txtDept.Text) <> "" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' "
        End If

        If Trim(txtItemCode.Text) <> "" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.ITEM_CODE_LIQ='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' "
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = Val(MasterNo)
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.DIV_CODE=" & mDivision & ""
            End If
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " GROUP BY DEPT.DEPT_DESC, ITEM_CODE_LIQ, IMST1.ITEM_SHORT_DESC,ITEM_CODE_CYL, IMST2.ITEM_SHORT_DESC"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDivision As Double

        MakeSQL = " SELECT DEPT.DEPT_DESC, TRN.DOC_DATE, " & vbCrLf & " ITEM_CODE_LIQ, IMST1.ITEM_SHORT_DESC, CARBON_KG, " & vbCrLf & " ITEM_CODE_CYL, IMST2.ITEM_SHORT_DESC,CYLINDER_NO, EACH_CYLINDER_KG, TOT_CYLINDER_KG," & vbCrLf & " REMARKS, SIGN_EMP_CODE " & vbCrLf & " FROM MAN_CO2COSUMP_TRN TRN, INV_ITEM_MST IMST1, INV_ITEM_MST IMST2, PAY_DEPT_MST DEPT" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=IMST1.COMPANY_CODE(+)" & vbCrLf & " AND TRN.ITEM_CODE_LIQ=IMST1.ITEM_CODE(+)" & vbCrLf & " AND TRN.COMPANY_CODE=IMST2.COMPANY_CODE(+)" & vbCrLf & " AND TRN.ITEM_CODE_CYL=IMST2.ITEM_CODE(+)" & vbCrLf & " AND TRN.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND TRN.DEPT_CODE=DEPT.DEPT_CODE"

        If Trim(txtDept.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' "
        End If

        If Trim(txtItemCode.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.ITEM_CODE_LIQ='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' "
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = Val(MasterNo)
                MakeSQL = MakeSQL & vbCrLf & "AND TRN.DIV_CODE=" & mDivision & ""
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND DOC_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY TRN.DEPT_CODE,DOC_DATE "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Public Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Dept Does Not Exist.")
            Cancel = True
        Else
            lblDept.text = MasterNo
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Public Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Item Does Not Exist.")
            Cancel = True
        Else
            lblItemName.text = MasterNo
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
