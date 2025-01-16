Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamExpBudgetReport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection	

    Private Const ColAccountCode As Short = 1
    Private Const ColAccountName As Short = 2
    Private Const ColAprilValue As Short = 3
    Private Const ColMayValue As Short = 4
    Private Const ColJuneValue As Short = 5
    Private Const ColJulyValue As Short = 6
    Private Const ColAugustValue As Short = 7
    Private Const ColSeptemberValue As Short = 8
    Private Const ColOctoberValue As Short = 9
    Private Const ColNovemberValue As Short = 10
    Private Const ColDecemberValue As Short = 11
    Private Const ColJanuaryValue As Short = 12
    Private Const ColFebruaryValue As Short = 13
    Private Const ColMarchValue As Short = 14
    Private Const ColTotalValue As Short = 15

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub ChkAllName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkAllName.CheckStateChanged
        txtName.Enabled = IIf(ChkAllName.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(ChkAllName.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnSaleBudget(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnSaleBudget(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnSaleBudget(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Sale Budget Details"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\expbudget.rpt"

        '    If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr	

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr


        SqlStr = FetchRecordForReport()

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume	
    End Sub

    Private Function FillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...	
        On Error GoTo PrintDummyErr
        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For RowNum = prmStartGridRow To prmEndGridRow
            '        FieldCnt = 3	
            '        SetData = "FIELD1,FIELD2"	
            '        GetData = "'" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _	
            ''                & ",'" & MainClass.AllowSingleQuote(txtName.Text) & "'"	

            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol

                GridName.Col = FieldNum
                SetData = SetData & ", " & "FIELD" & FieldCnt
                GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
NextRec:
        Next
        PubDBCn.CommitTrans()
        FillPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyData = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function FetchRecordForReport() As String

        Dim mSqlStr As String

        mSqlStr = " SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster(txtName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtName.Text = AcName
            txtCode.Text = AcName1
        End If

        txtName.Focus()
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

    Private Sub frmParamExpBudgetReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Expense Budget Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamExpBudgetReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        ChkAllName.CheckState = System.Windows.Forms.CheckState.Checked
        txtName.Enabled = False
        cmdSearch.Enabled = False
        Call PrintStatus(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamExpBudgetReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub frmParamExpBudgetReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub txtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        Call cmdSearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String = ""
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Account Does Not Exist In Master.")
            Cancel = True
        Else
            txtCode.Text = MasterNo
        End If

        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        Dim mCol As Integer

        With SprdMain
            .MaxCols = ColTotalValue
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColAccountCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColAccountCode, 8)

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColAccountName, 25)



            .ColsFrozen = ColAccountName

            For mCol = ColAprilValue To ColTotalValue
                .Col = mCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(mCol, 10)
            Next

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")


        '********************************	
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        MakeSQL = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '04' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS APRIL_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '05' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS MAY_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '06' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS JUNE_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '07' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS JULY_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '08' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS AUGUST_VALUE, "

        MakeSQL = MakeSQL & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '09' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS SEPTEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '10' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS OCTOBER_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '11' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS NOVEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '12' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS DECEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '01' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS JANUARY_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '02' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS FEBRUARY_VALUE, " & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.BUDGET_DATE,'MM') = '03' THEN IH.BUDGET_AMOUNT ELSE 0 END) AS MARCH_VALUE, " & vbCrLf & " SUM(CASE WHEN IH.BUDGET_AMOUNT IS NULL THEN 0 ELSE IH.BUDGET_AMOUNT END) AS TOTAL_VALUE "

        MakeSQL = MakeSQL & vbCrLf & " FROM MIS_EXPBUDGET_DET IH, FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "



        If ChkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' "
        End If

        MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME" & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If Trim(txtName.Text) = "" And ChkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("Customer is blank.")
            txtName.Focus()
            FieldsVerification = False
            Exit Function
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
End Class
