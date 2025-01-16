Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSaleBudgetMst
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection	

    Private Const ColCustomerCode As Short = 1
    Private Const ColCustomerName As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemName As Short = 4
    Private Const ColItemUOM As Short = 5
    Private Const ColAprilQty As Short = 6
    Private Const ColAprilRate As Short = 7
    Private Const ColAprilValue As Short = 8
    Private Const ColMayQty As Short = 9
    Private Const ColMayRate As Short = 10
    Private Const ColMayValue As Short = 11
    Private Const ColJuneQty As Short = 12
    Private Const ColJuneRate As Short = 13
    Private Const ColJuneValue As Short = 14
    Private Const ColJulyQty As Short = 15
    Private Const ColJulyRate As Short = 16
    Private Const ColJulyValue As Short = 17
    Private Const ColAugustQty As Short = 18
    Private Const ColAugustRate As Short = 19
    Private Const ColAugustValue As Short = 20
    Private Const ColSeptemberQty As Short = 21
    Private Const ColSeptemberRate As Short = 22
    Private Const ColSeptemberValue As Short = 23
    Private Const ColOctoberQty As Short = 24
    Private Const ColOctoberRate As Short = 25
    Private Const ColOctoberValue As Short = 26
    Private Const ColNovemberQty As Short = 27
    Private Const ColNovemberRate As Short = 28
    Private Const ColNovemberValue As Short = 29
    Private Const ColDecemberQty As Short = 30
    Private Const ColDecemberRate As Short = 31
    Private Const ColDecemberValue As Short = 32
    Private Const ColJanuaryQty As Short = 33
    Private Const ColJanuaryRate As Short = 34
    Private Const ColJanuaryValue As Short = 35
    Private Const ColFebruaryQty As Short = 36
    Private Const ColFebruaryRate As Short = 37
    Private Const ColFebruaryValue As Short = 38
    Private Const ColMarchQty As Short = 39
    Private Const ColMarchRate As Short = 40
    Private Const ColMarchValue As Short = 41
    Private Const ColTotalValue As Short = 42

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
        cmdsearch.Enabled = IIf(ChkAllName.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
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
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SaleBudgetMst.rpt"

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

    Private Sub frmParamSaleBudgetMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Sale Budget Details"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamSaleBudgetMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        cboType.Items.Clear()
        cboType.Items.Add("All")
        cboType.Items.Add("Sale")
        cboType.Items.Add("Jobwork")
        cboType.SelectedIndex = 0

        ChkAllName.CheckState = System.Windows.Forms.CheckState.Checked
        txtName.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamSaleBudgetMst_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub frmParamSaleBudgetMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
            MsgBox("Customer Does Not Exist In Master.")
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

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerCode, 8)

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColCustomerName, 25)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemName, 25)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemUOM, 7)

            .ColsFrozen = ColItemUOM

            For mCol = ColAprilQty To ColMarchValue
                .Col = mCol
                .CellType = SS_CELL_TYPE_FLOAT
                If (mCol Mod 3) = 1 Then
                    .TypeFloatDecimalPlaces = 3
                ElseIf (mCol Mod 3) = 2 Then
                    .TypeFloatDecimalPlaces = 4
                ElseIf (mCol Mod 3) = 0 Then
                    .TypeFloatDecimalPlaces = 2
                End If
                .TypeFloatDecimalChar = Asc(".")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(mCol, 9)
            Next

            .Col = ColTotalValue
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(mCol, 10)

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

        MakeSQL = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'APRIL' THEN ID.QTY ELSE 0 END) AS APRIL_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'APRIL' THEN ID.RATE ELSE 0 END) AS APRIL_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'APRIL' THEN ID.VALUE ELSE 0 END) AS APRIL_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MAY' THEN ID.QTY ELSE 0 END) AS MAY_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MAY' THEN ID.RATE ELSE 0 END) AS MAY_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MAY' THEN ID.VALUE ELSE 0 END) AS MAY_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JUNE' THEN ID.QTY ELSE 0 END) AS JUNE_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JUNE' THEN ID.RATE ELSE 0 END) AS JUNE_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JUNE' THEN ID.VALUE ELSE 0 END) AS JUNE_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JULY' THEN ID.QTY ELSE 0 END) AS JULY_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JULY' THEN ID.RATE ELSE 0 END) AS JULY_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JULY' THEN ID.VALUE ELSE 0 END) AS JULY_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'AUGUST' THEN ID.QTY ELSE 0 END) AS AUGUST_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'AUGUST' THEN ID.RATE ELSE 0 END) AS AUGUST_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'AUGUST' THEN ID.VALUE ELSE 0 END) AS AUGUST_VALUE, "

        MakeSQL = MakeSQL & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'SEPTEMBER' THEN ID.QTY ELSE 0 END) AS SEPTEMBER_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'SEPTEMBER' THEN ID.RATE ELSE 0 END) AS SEPTEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'SEPTEMBER' THEN ID.VALUE ELSE 0 END) AS SEPTEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'OCTOBER' THEN ID.QTY ELSE 0 END) AS OCTOBER_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'OCTOBER' THEN ID.RATE ELSE 0 END) AS OCTOBER_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'OCTOBER' THEN ID.VALUE ELSE 0 END) AS OCTOBER_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'NOVEMBER' THEN ID.QTY ELSE 0 END) AS NOVEMBER_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'NOVEMBER' THEN ID.RATE ELSE 0 END) AS NOVEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'NOVEMBER' THEN ID.VALUE ELSE 0 END) AS NOVEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'DECEMBER' THEN ID.QTY ELSE 0 END) AS DECEMBER_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'DECEMBER' THEN ID.RATE ELSE 0 END) AS DECEMBER_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'DECEMBER' THEN ID.VALUE ELSE 0 END) AS DECEMBER_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JANUARY' THEN ID.QTY ELSE 0 END) AS JANUARY_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JANUARY' THEN ID.RATE ELSE 0 END) AS JANUARY_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'JANUARY' THEN ID.VALUE ELSE 0 END) AS JANUARY_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'FEBRUARY' THEN ID.QTY ELSE 0 END) AS FEBRUARY_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'FEBRUARY' THEN ID.RATE ELSE 0 END) AS FEBRUARY_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'FEBRUARY' THEN ID.VALUE ELSE 0 END) AS FEBRUARY_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MARCH' THEN ID.QTY ELSE 0 END) AS MARCH_QTY, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MARCH' THEN ID.RATE ELSE 0 END) AS MARCH_RATE, " & vbCrLf & " SUM(CASE WHEN ID.MONTH_NAME = 'MARCH' THEN ID.VALUE ELSE 0 END) AS MARCH_VALUE, " & vbCrLf & " SUM(CASE WHEN ID.VALUE IS NULL THEN 0 ELSE ID.VALUE END) AS TOTAL_VALUE "

        MakeSQL = MakeSQL & vbCrLf & " FROM MIS_SALEBUDGET_DET IH, MIS_SALEBUDGET_TRN ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMAT " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_NO,LENGTH(IH.AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.AUTO_KEY_NO=ID.AUTO_KEY_NO (+) " & vbCrLf & " AND IH.SERIAL_NO=ID.SERIAL_NO (+) " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "

        MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE (+) " & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE (+) "

        MakeSQL = MakeSQL & vbCrLf & " AND INVMST.COMPANY_CODE=GMAT.COMPANY_CODE (+) " & vbCrLf & " AND INVMST.CATEGORY_CODE=GMAT.GEN_CODE(+) AND GMAT.GEN_TYPE='C'"

        If ChkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' "
        End If

        If cboType.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " AND GMAT.STOCKTYPE='FG' "
        ElseIf cboType.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND GMAT.STOCKTYPE='CS' "
        End If


        If optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "HAVING (SUM(ID.QTY) IS NOT NULL OR SUM(ID.QTY)>0)"
        End If

        MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM " & vbCrLf & " ORDER BY IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME,IH.ITEM_CODE,INVMST.ITEM_SHORT_DESC,IH.ITEM_UOM "

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
