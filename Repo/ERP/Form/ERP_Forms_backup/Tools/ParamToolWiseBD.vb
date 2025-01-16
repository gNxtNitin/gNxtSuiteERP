Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamToolWiseBD
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection			

    Private Const ColMachineNo As Short = 1
    Private Const ColMachineDesc As Short = 2
    Private Const ColBreakDownCode As Short = 3
    Private Const ColBreakDownDesc As Short = 4
    Private Const ColBreakDownType As Short = 5
    Private Const ColOccuranceNo As Short = 6
    Private Const ColDownTime As Short = 7

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboProbType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboProbType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMachine.Enabled = False
            cmdSearch.Enabled = False
        Else
            txtMachine.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllProd_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllProd.CheckStateChanged
        Call PrintStatus(False)
        If chkAllProd.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtProduct.Enabled = False
            cmdSearchProd.Enabled = False
        Else
            txtProduct.Enabled = True
            cmdSearchProd.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMachineBD(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMachineBD(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnMachineBD(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Tool Wise Break Down Details"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        If Trim(txtMachine.Text) <> "" Then
            mSubTitle = mSubTitle & " [ Machine : " & txtMachine.Text & " ]"
        End If
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ToolWiseBD.rpt"
        If InsertIntoTemp() = False Then GoTo ReportErr
        SqlStr = MakeSQL()
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TOOL_STATUS='O' "
        If MainClass.SearchGridMaster("", "TOL_TOOLINFO_MST", "TOOL_NO", "TOOL_NO", , , SqlStr) = True Then
            txtMachine.Text = AcName
            lblCode.Text = AcName1
        End If
        txtMachine.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchProd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProd.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TOOL_STATUS='O' "

        SqlStr = " SELECT INV.ITEM_SHORT_DESC, TOL.TOOL_NO, TOL.TOOL_ITEM_CODE " & vbCrLf & " FROM TOL_TOOLINFO_MST TOL, INV_ITEM_MST INV" & vbCrLf & " WHERE TOL.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TOL.COMPANY_CODE=INV.COMPANY_CODE" & vbCrLf & " AND TOL.TOOL_ITEM_CODE=INV.ITEM_CODE AND TOOL_STATUS='O'"

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtProduct.Text = AcName
        End If

        txtProduct.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4			
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamToolWiseBD_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Tools Break Down Details"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormatSprdMain(-1)

        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamToolWiseBD_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtMachine.Enabled = False
        cmdSearch.Enabled = False
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Call PrintStatus(True)

        cboProbType.Items.Clear()
        '    cboProbType.AddItem "All"			
        '    cboProbType.AddItem "Mechanical"			
        '    cboProbType.AddItem "Electrical"			
        '    cboProbType.AddItem "Hydraulic"			
        MainClass.FillCombo(cboProbType, "TOL_BDPROBLEMS_MST", "PROB_DESC", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        cboProbType.SelectedIndex = 0



        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        '    Resume			
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamToolWiseBD_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamToolWiseBD_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub txtmachine_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtmachine_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachine.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtmachine_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachine.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachine.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtmachine_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachine.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtmachine_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachine.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachine.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TOOL_STATUS='O' "
        If MainClass.ValidateWithMasterTable(txtMachine.Text, "TOOL_NO", "TOOL_NO", "TOL_TOOLINFO_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Machine Does Not Exist In Master.")
            Cancel = True
        Else
            lblCode.Text = MasterNo
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
            .MaxCols = ColDownTime
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

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
            .set_ColWidth(ColMachineDesc, 27)

            .Col = ColBreakDownCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColBreakDownCode, 8)

            .Col = ColBreakDownDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColBreakDownDesc, 27)

            .Col = ColBreakDownType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColBreakDownType, 8)

            .Col = ColOccuranceNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColOccuranceNo, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .Col = ColDownTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColDownTime, 8)
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            .Row = 0
            .Col = ColMachineNo
            .Text = "Tool No"

            .Col = ColMachineDesc
            .Text = "Tool Desc"


        End With
    End Sub

    Private Function Show1() As Boolean
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If InsertIntoTemp() = False Then GoTo LedgError
        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************			
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function InsertIntoTemp() As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_BREAKDOWN NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_BREAKDOWN (" & vbCrLf & " USERID, COMPANY_CODE, MACHINE_NO, SLIP_DATE, " & vbCrLf & " SLIP_TIME , FROM_DEPT_CODE, COMPLETION_DATE, COMP_TIME, " & vbCrLf & " SUSPECTED_REASON, PROBLEM_FACED, DEPU_EMP_CODE,DEPU_REMARKS,DOWNTIME) "
        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TOL.COMPANY_CODE,TRIM(TOL.TOOL_NO), TOL.SLIP_DATE, " & vbCrLf & " (CASE WHEN TOL.SLIP_DATE IS NULL THEN NULL " & vbCrLf & " Else " & vbCrLf & " TO_DATE(TO_CHAR(TOL.SLIP_DATE,'DD/MON/RRRR')||TO_CHAR(TOL.BRK_DWN_TIME,'HH24:MI'),'DD/MM/RRRR HH24:MI') " & vbCrLf & " END) SLIP_TIME, " & vbCrLf & " TRIM(TOL.FROM_DEPT_CODE), "
        SqlStr = SqlStr & vbCrLf & "  TOL.COMPLETION_DATE, " & vbCrLf & " (CASE WHEN TOL.COMPLETION_DATE IS NULL THEN NULL " & vbCrLf & " Else " & vbCrLf & " TO_DATE(TO_CHAR(TOL.COMPLETION_DATE,'DD/MON/RRRR')||TO_CHAR(TOL.COMPLETION_TIME,'HH24:MI'),'DD/MM/RRRR HH24:MI') " & vbCrLf & " END) COMP_TIME, " & vbCrLf & " TOL.SUSPECTED_REASON , " & vbCrLf & " TRIM(TOL.PROBLEM_FACED),TOL.DEPU_EMP_CODE, TOL.DEPU_REMARKS, " & vbCrLf & " ROUND(ABS(( " & vbCrLf & " (CASE WHEN TOL.COMPLETION_DATE IS NULL THEN NULL " & vbCrLf & " Else " & vbCrLf & " TO_DATE(TO_CHAR(TOL.COMPLETION_DATE,'DD/MON/RRRR')||TO_CHAR(TOL.COMPLETION_TIME,'HH24:MI'),'DD/MM/RRRR HH24:MI') " & vbCrLf & " END) " & vbCrLf & " - " & vbCrLf & " (CASE WHEN TOL.SLIP_DATE IS NULL THEN NULL " & vbCrLf & " Else " & vbCrLf & " TO_DATE(TO_CHAR(TOL.SLIP_DATE,'DD/MON/RRRR')||TO_CHAR(TOL.BRK_DWN_TIME,'HH24:MI'),'DD/MM/RRRR HH24:MI') " & vbCrLf & " END) " & vbCrLf & " ) *24*60)) AS DOWN_TIME"

        SqlStr = SqlStr & vbCrLf _
            & " FROM TOL_BREAKDOWN_HDR TOL, TOL_TOOLINFO_MST TMST, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE TOL.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TOL.COMPANY_CODE=TMST.COMPANY_CODE" & vbCrLf _
            & " AND TOL.TOOL_NO=TMST.TOOL_NO" & vbCrLf _
            & " AND TMST.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND TMST.TOOL_ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND TOL.SLIP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND TOL.SLIP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachine.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TOL.TOOL_NO='" & MainClass.AllowSingleQuote(lblCode.Text) & "'"
        End If

        If chkAllProd.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtProduct.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(txtProduct.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TOL.TOOL_NO, TOL.PROBLEM_FACED "

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()

        InsertIntoTemp = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        InsertIntoTemp = False
        PubDBCn.RollbackTrans()
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1


        MakeSQL = " SELECT IH.MACHINE_NO, IMST.ITEM_SHORT_DESC, PROB_CODE, PROB_DESC, " & vbCrLf _
            & " PROB_TYPE, " & vbCrLf _
            & " TO_CHAR(COUNT(SLIP_DATE)) AS OCC_NO, " & vbCrLf _
            & " TO_CHAR(SUM(DOWNTIME)) AS DWN_TIME " & vbCrLf _
            & " FROM TEMP_BREAKDOWN IH, TOL_BDPROBLEMS_MST BDMST, TOL_TOOLINFO_MST MACMST, INV_ITEM_MST IMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND IH.COMPANY_CODE=BDMST.COMPANY_CODE (+) " & vbCrLf _
            & " AND IH.PROBLEM_FACED=BDMST.PROB_CODE (+) " & vbCrLf _
            & " AND IH.COMPANY_CODE=MACMST.COMPANY_CODE AND LTRIM(RTRIM(IH.MACHINE_NO))=LTRIM(RTRIM(MACMST.TOOL_NO))  " & vbCrLf _
            & " AND MACMST.COMPANY_CODE=IMST.COMPANY_CODE AND MACMST.ITEM_CODE=IMST.ITEM_CODE   "

        If cboProbType.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND BDMST.PROB_DESC='" & MainClass.AllowSingleQuote(cboProbType.Text) & "' "
        End If

        MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.MACHINE_NO,IMST.ITEM_SHORT_DESC,PROB_CODE,PROB_DESC,PROB_TYPE " & vbCrLf & " ORDER BY IH.MACHINE_NO,IMST.ITEM_SHORT_DESC"

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
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtMachine.Text) = "" Then
                MsgInformation("Machine is blank.")
                txtMachine.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtProduct_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProduct.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtProduct_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProduct.DoubleClick
        Call cmdSearchProd_Click(cmdSearchProd, New System.EventArgs())
    End Sub


    Private Sub txtProduct_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProduct.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtProduct.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtProduct_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProduct.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProd_Click(cmdSearchProd, New System.EventArgs())
    End Sub


    Private Sub txtProduct_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProduct.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR

        If Trim(txtProduct.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtProduct.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Component Does Not Exist In Master.")
            Cancel = True
        End If

        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
