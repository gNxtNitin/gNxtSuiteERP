Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMachineHis
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColCompanyCode As Short = 1
    Private Const ColMachineNo As Short = 2
    Private Const ColAutoKey As Short = 3
    Private Const ColDate As Short = 4
    Private Const ColType As Short = 5
    Private Const ColReason As Short = 6
    Private Const ColActionTaken As Short = 7
    Private Const ColDoneBy As Short = 8
    Private Const ColRemarks As Short = 9
    Private Const ColItemCode As Short = 10
    Private Const ColItemDesc As Short = 11
    Private Const ColUom As Short = 12
    Private Const ColQty As Short = 13
    Private Const ColRate As Short = 14
    Private Const ColAmount As Short = 15
    Private Const ColStatus As Short = 16

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim RsMachineHis As ADODB.Recordset

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
        ReportOnMachineHis(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMachineHis(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnMachineHis(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Machine History Card"
        If cboDateCondition.Text = "Between" Then
            mSubTitle = mSubTitle & " [Date Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboDateCondition.Text = "After" Then
            mSubTitle = mSubTitle & " [Date After  " & txtDate1.Text & " ]"
        End If
        If cboDateCondition.Text = "Before" Then
            mSubTitle = mSubTitle & " [Date Before  " & txtDate1.Text & " ]"
        End If
        If cboDateCondition.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Date On  " & txtDate1.Text & " ]"
        End If

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 3, SprdMain.MaxCols) = False Then GoTo ReportErr

        SqlStr = FetchRecordForReport()

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MachineHis.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Function FillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer) As Boolean

        ''' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 7
            SetData = "FIELD1,FIELD2,FIELD3,FIELD4,FIELD5,FIELD6"
            GetData = "'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblDescription.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblSpec.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblMake.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblLocation.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblInstDate.Text) & "'" & vbCrLf
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

    Private Sub cmdSearchMachineNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineNo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", "", "", SqlStr) = True Then
            txtMachineNo.Text = AcName1
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

    Public Sub frmParamMachineHis_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Machine History Card"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMachineHis_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

    Private Sub frmParamMachineHis_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth), 11592.4, 763)
        CurrFormWidth = mReFormWidth
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMachineHis_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColAutoKey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColActionTaken
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDoneBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            '        .ColHidden = True

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            '        .ColHidden = True

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            '        .ColHidden = True

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            '        .ColHidden = True

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False

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

        MakeSQL = " SELECT * FROM (" & vbCrLf & " SELECT TO_CHAR(MAN_MACHINE_PM_HDR.COMPANY_CODE) AS COMPANY_CODE, MACHINE_NO, TO_CHAR(MAN_MACHINE_PM_HDR.AUTO_KEY_PM) AS AUTO_KEY, PM_DATE AS MAINT_DATE, " & vbCrLf & " 'P/M' AS TYPE, 'PREV. MAINT. - ' || CHECK_TYPE AS REASON, REMARKS AS ACTION_TAKEN, EMP_NAME, '' AS REMARKS, " & vbCrLf & " MAN_MACHINE_PM_ITEM.ITEM_CODE, INV_ITEM_MST.ITEM_SHORT_DESC, MAN_MACHINE_PM_ITEM.ITEM_UOM, TO_CHAR(MAN_MACHINE_PM_ITEM.ITEM_QTY), " & vbCrLf & " TO_CHAR(MAN_MACHINE_PM_ITEM.ITEM_RATE),TO_CHAR(MAN_MACHINE_PM_ITEM.ITEM_AMOUNT),'" & lblStatus.Text & "' AS MC_STATUS " & vbCrLf & " From MAN_MACHINE_PM_HDR, MAN_MACHINE_PM_ITEM, PAY_EMPLOYEE_MST, INV_ITEM_MST " & vbCrLf & " WHERE MAN_MACHINE_PM_HDR.AUTO_KEY_PM=MAN_MACHINE_PM_ITEM.AUTO_KEY_PM (+) " & vbCrLf & " AND MAN_MACHINE_PM_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND MAN_MACHINE_PM_HDR.DONE_BY=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND SUBSTR(MAN_MACHINE_PM_ITEM.AUTO_KEY_PM,LENGTH(MAN_MACHINE_PM_ITEM.AUTO_KEY_PM)-1,2)=INV_ITEM_MST.COMPANY_CODE (+) " & vbCrLf & " AND MAN_MACHINE_PM_ITEM.ITEM_CODE=INV_ITEM_MST.ITEM_CODE (+) " & vbCrLf & " Union " & vbCrLf & " SELECT TO_CHAR(MAN_MACHINE_SCHD_HDR.COMPANY_CODE) AS COMPANY_CODE, MACHINE_NO, TO_CHAR(MAN_MACHINE_SCHD_HDR.AUTO_KEY_SCHD) AS AUTO_KEY, " & vbCrLf & " MONTH_LAST_DATE(SCHD_MONTH, SCHD_YEAR) AS MAINT_DATE, 'P/M' AS TYPE, " & vbCrLf & " 'PREV. MAINT. NOT ACHIEVED DUE TO ' || " & vbCrLf & " DECODE(NOT_ACH_REASON,'A','SPARES/MATT N/A','B','MAN N/A','C','MACHINE N/A','') AS REASON, " & vbCrLf & " 'NEXT DUE : ' || TO_CHAR(NEXT_DUE,'DD/MM/YYYY') AS ACTION_TAKEN, EMP_NAME, '' AS REMARKS, " & vbCrLf & " NULL AS ITEM_CODE, NULL AS ITEM_SHORT_DESC, NULL AS ITEM_UOM, NULL AS ITEM_QTY, " & vbCrLf & " NULL AS ITEM_RATE,NULL AS ITEM_AMOUNT,'" & lblStatus.Text & "' AS MC_STATUS " & vbCrLf & " From MAN_MACHINE_SCHD_HDR, MAN_MACHINE_SCHD_DET, PAY_EMPLOYEE_MST " & vbCrLf & " Where MAN_MACHINE_SCHD_HDR.AUTO_KEY_SCHD = MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD (+) " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.PREP_BY=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND NEXT_DUE IS NOT NULL "
        MakeSQL = MakeSQL & vbCrLf & " Union " & vbCrLf & " SELECT TO_CHAR(MAN_BREAKDOWN_HDR.COMPANY_CODE)AS COMPANY_CODE, MAN_BREAKDOWN_HDR.MACHINE_NO, TO_CHAR(MAN_BREAKDOWN_HDR.AUTO_KEY_BDSLIP) AS AUTO_KEY, " & vbCrLf & " MAN_BREAKDOWN_HDR.SLIP_DATE AS MAINT_DATE, 'B/D' AS TYPE, MAN_BREAKDOWN_HDR.SUSPECTED_REASON ||' / ' || MAN_BDPROBLEMS_MST.PROB_DESC AS REASON, DEPU_REMARKS AS ACTION_TAKEN, EMP_NAME, COMPLETION_REMARKS AS REMARKS, " & vbCrLf & " MAN_BREAKDOWN_DET.ITEM_CODE, INV_ITEM_MST.ITEM_SHORT_DESC, MAN_BREAKDOWN_DET.ITEM_UOM, TO_CHAR(MAN_BREAKDOWN_DET.ITEM_QTY), " & vbCrLf & " TO_CHAR(MAN_BREAKDOWN_DET.ITEM_RATE),TO_CHAR(MAN_BREAKDOWN_DET.ITEM_AMOUNT),'" & lblStatus.Text & "' AS MC_STATUS " & vbCrLf & " From MAN_BREAKDOWN_HDR, MAN_BREAKDOWN_DET, PAY_EMPLOYEE_MST, MAN_BDPROBLEMS_MST, INV_ITEM_MST " & vbCrLf & " Where MAN_BREAKDOWN_HDR.AUTO_KEY_BDSLIP = MAN_BREAKDOWN_DET.AUTO_KEY_BDSLIP (+) " & vbCrLf & " AND MAN_BREAKDOWN_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND MAN_BREAKDOWN_HDR.DEPU_EMP_CODE=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND MAN_BREAKDOWN_HDR.COMPANY_CODE=MAN_BDPROBLEMS_MST.COMPANY_CODE (+) " & vbCrLf & " AND MAN_BREAKDOWN_HDR.PROBLEM_FACED=MAN_BDPROBLEMS_MST.PROB_CODE (+) " & vbCrLf & " AND SUBSTR(MAN_BREAKDOWN_DET.AUTO_KEY_BDSLIP,LENGTH(MAN_BREAKDOWN_DET.AUTO_KEY_BDSLIP)-1,2)=INV_ITEM_MST.COMPANY_CODE (+) " & vbCrLf & " AND MAN_BREAKDOWN_DET.ITEM_CODE=INV_ITEM_MST.ITEM_CODE (+) " & vbCrLf & " ) MACHINE_HIS " & vbCrLf & " WHERE COMPANY_CODE='" & RsCompany.Fields("COMPANY_CODE").Value & "' "

        If Trim(txtMachineNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
        End If

        If cboDateCondition.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboDateCondition.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDateCondition.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDateCondition.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY MAINT_DATE "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If Trim(txtMachineNo.Text) = "" Then
            MsgBox("Please Select Machine No.")
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
        Call cmdSearchMachineNo_Click(cmdSearchMachineNo, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachineNo_Click(cmdSearchMachineNo, New System.EventArgs())
    End Sub

    Public Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                    & " FROM MAN_MACHINE_MST" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND STATUS='O' " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineHis, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMachineHis.EOF = False Then
            ShowMachine()
        Else
            MsgBox("Such Number Does Not Exist", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub ShowMachine()
        On Error GoTo ShowErrPart
        If Not RsMachineHis.EOF Then
            lblDescription.Text = IIf(IsDbNull(RsMachineHis.Fields("MACHINE_DESC").Value), "", RsMachineHis.Fields("MACHINE_DESC").Value)
            lblSpec.Text = IIf(IsDbNull(RsMachineHis.Fields("MACHINE_SPEC").Value), "", RsMachineHis.Fields("MACHINE_SPEC").Value)
            lblMake.Text = IIf(IsDbNull(RsMachineHis.Fields("MAKE").Value), "", RsMachineHis.Fields("MAKE").Value)
            lblLocation.Text = IIf(IsDbNull(RsMachineHis.Fields("LOCATION").Value), "", RsMachineHis.Fields("LOCATION").Value)
            lblInstDate.Text = IIf(IsDbNull(RsMachineHis.Fields("MACHINE_INST_DATE").Value), "", RsMachineHis.Fields("MACHINE_INST_DATE").Value)

            If RsMachineHis.Fields("Status").Value = "O" Then
                lblStatus.Text = "OPEN/ACTIVE"
            ElseIf RsMachineHis.Fields("Status").Value = "T" Then
                lblStatus.Text = "TRANSFER SALE"
            ElseIf RsMachineHis.Fields("Status").Value = "S" Then
                lblStatus.Text = "SCRAP SALE"
            ElseIf RsMachineHis.Fields("Status").Value = "C" Then
                lblStatus.Text = "CLOSE/INACTIVE"
            End If

        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
End Class
