Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamProdLoss
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColAutoNo As Short = 1
    Private Const ColRefDate As Short = 2
    Private Const ColEmpCode As Short = 3
    Private Const ColEmpName As Short = 4
    Private Const ColEmpBasic As Short = 5
    Private Const ColTimeFrom As Short = 6
    Private Const ColTimeTo As Short = 7
    Private Const ColTotalTime As Short = 8
    Private Const ColBDAmount As Short = 9
    Private Const ColReason As Short = 10
    Private Const ColRemarks As Short = 11

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    'Dim CurrFormWidth As Long	
    'Dim CurrFormHeight As Long	

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpName.Enabled = False
            cmdSearchEmp.Enabled = False
        Else
            txtEmpName.Enabled = True
            cmdSearchEmp.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnDailyTarget(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnDailyTarget(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnDailyTarget(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        mTitle = "Worker's Production Loss Register [From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & "]"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & "[Emp : " & txtEmpName.Text & "] "
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProdLossReg.rpt"

        SqlStr = MakeSQL

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        SearchEMP()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(sprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)

        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamProdLoss_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Worker's Production Loss Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamProdLoss_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        txtEmpName.Enabled = False
        cmdSearchEmp.Enabled = False

        Call FillCombo()

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCombo()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDept.Items.Clear()

        SqlStr = "SELECT DEPT_DESC FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDept.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDept.Items.Add(RS.Fields("DEPT_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDept.SelectedIndex = 0

        cboReason.Items.Clear()
        cboReason.Items.Add("ALL")
        cboReason.Items.Add("1.M/C Breakdown")
        cboReason.Items.Add("2.Die Breakdown")
        cboReason.Items.Add("3.Die Change")
        cboReason.Items.Add("4.Others")
        cboReason.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub frmParamProdLoss_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(sprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamProdLoss_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        sprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        sprdMain.DAutoCellTypes = True
        sprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        sprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mAutoNo As String
        sprdMain.Row = sprdMain.ActiveRow
        sprdMain.Col = ColAutoNo
        mAutoNo = Trim(SprdMain.Text)
        frmProdLoss.MdiParent = Me.MdiParent
        frmProdLoss.Show()
        frmProdLoss.frmProdLoss_Activated(Nothing, New System.EventArgs())
        frmProdLoss.txtNumber.Text = mAutoNo
        frmProdLoss.txtNumber_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.DoubleClick
        SearchEMP()
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEMP()
    End Sub

    Private Sub txtEmpName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtEmpName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_TYPE='W'"

        If MainClass.ValidateWithMasterTable(txtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Emp in Emp Master")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearchEMP()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_TYPE='W'"
        MainClass.SearchGridMaster(txtEmpName.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr)
        If AcName <> "" Then
            txtEmpName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With sprdMain
            .MaxCols = ColRemarks
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColAutoNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .set_ColWidth(ColAutoNo, 6)
            .ColHidden = False

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 8)

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpCode, 5)

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpName, 17)

            .Col = ColEmpBasic
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColEmpBasic, 12)

            .Col = ColBDAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBDAmount, 12)


            .Col = ColTimeFrom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTimeFrom, 6)

            .Col = ColTimeTo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTimeTo, 6)

            .Col = ColTotalTime
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColTotalTime, 6)

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColReason, 12)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 25)

            MainClass.SetSpreadColor(sprdMain, -1)
            MainClass.ProtectCell(sprdMain, 1, .MaxRows, 1, .MaxCols)
            sprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle	
            sprdMain.DAutoCellTypes = True
            sprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            sprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************	
        Call CalcTots()

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mEmpCode As String


        If OptShow(0).Checked = True Then
            MakeSQL = " SELECT TO_CHAR(AUTO_KEY_NO) AS AUTO_KEY_NO,TO_CHAR(IGH.REF_DATE,'DD/MM/YYYY'), " & vbCrLf & " IGH.EMP_CODE, EMP.EMP_NAME, 0, "

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(IGH.TIME_FROM,'HH24:MI'), TO_CHAR(IGH.TIME_TO,'HH24:MI'), " & vbCrLf & " IGH.TOTAL_TIME, 0," & vbCrLf & " DECODE(IGH.REASON,'1','M/C B/D','2','DIE B/D','3','DIE CHANGE','4','OTHERS') AS REASON, REMARKS "

        Else
            MakeSQL = " SELECT '','', " & vbCrLf & " IGH.EMP_CODE, EMP.EMP_NAME, 0, "

            MakeSQL = MakeSQL & vbCrLf & " '', '', " & vbCrLf & " SUM(IGH.TOTAL_TIME), 0," & vbCrLf & " '', '' "
        End If



        ''FROM CLAUSE...	
        MakeSQL = MakeSQL & vbCrLf & " FROM PRD_PROD_LOSS_TRN IGH, " & vbCrLf & " PAY_EMPLOYEE_MST EMP "

        ''WHERE CLAUSE...	
        MakeSQL = MakeSQL & vbCrLf & " WHERE IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IGH.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND IGH.EMP_CODE=EMP.EMP_CODE  AND EMP.EMP_TYPE='W'"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_TYPE='W'") = True Then
                mEmpCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & " AND IGH.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
            End If
        End If

        If cboReason.Text <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.REASON='" & VB.Left(cboReason.Text, 1) & "'"
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQL = MakeSQL & vbCrLf & " AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IGH.REF_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.REF_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If OptShow(0).Checked = True Then
            If OptOrderBy(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY IGH.REF_DATE, IGH.EMP_CODE "
            ElseIf OptOrderBy(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY IGH.EMP_CODE, IGH.REF_DATE "
            End If
        Else
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY IGH.EMP_CODE, EMP.EMP_NAME "
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IGH.EMP_CODE "
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtEmpName.Text) = "" Then
                MsgInformation("Please enter Emp Name")
                txtEmpName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(txtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_TYPE='W'") = False Then
                MsgInformation("Invaild Emp Name")
                txtEmpName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim mLossAmount As String
        Dim mBSalary As Double
        Dim mHours As Double
        Dim cntRow As Integer
        Dim mLastDay As Double
        Dim mEmpCode As String

        mLastDay = 26 '' MainClass.LastDay(Month(txtDateFrom.Text), Year(txtDateFrom.Text))	


        With sprdMain
            For cntRow = 1 To .MaxRows
                mLossAmount = CStr(0)
                mBSalary = 0
                mHours = 0

                .Row = cntRow
                .Col = ColEmpCode
                mEmpCode = Trim(.Text)

                .Col = ColEmpBasic
                mBSalary = CalcBSalary(mEmpCode, "Y", "N", (txtDateFrom.Text))
                .Text = VB6.Format(mBSalary, "0.00")

                .Col = ColTotalTime
                mHours = Val(.Text)

                mLossAmount = CStr(mBSalary * mHours / (mLastDay * 8))

                .Row = cntRow
                .Col = ColBDAmount
                .Text = VB6.Format(mLossAmount, "0.00")

            Next cntRow
        End With
        Exit Sub

ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function CalcBSalary(ByRef mCode As String, ByRef mIsRegularEmp As String, ByRef mISBasicSalary As String, ByRef pSalDate As String) As Double

        On Error GoTo ERR1
        Dim RSSalDef As ADODB.Recordset
        Dim mCheckDate As String
        Dim SqlStr As String = ""
        Dim mTable As String
        Dim mSalaryHeadTable As String
        CalcBSalary = 0

        If mIsRegularEmp = "Y" Then
            mTable = "PAY_SALARYDEF_MST"
            mSalaryHeadTable = "PAY_SALARYHEAD_MST"
        Else
            mTable = "PAY_CONT_SALARYDEF_MST"
            mSalaryHeadTable = "PAY_CONT_SALARYHEAD_MST"
        End If
        mCheckDate = MainClass.LastDay(Month(CDate(pSalDate)), Year(CDate(pSalDate))) & "/" & VB6.Format(pSalDate, "MM/YYYY")
        mCheckDate = VB6.Format(mCheckDate, "DD/MM/YYYY")

        SqlStr = " SELECT BASICSALARY from " & mTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From " & mTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalDef, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalDef.EOF = False Then
            CalcBSalary = IIf(IsDbNull(RSSalDef.Fields("BASICSALARY").Value), 0, RSSalDef.Fields("BASICSALARY").Value)
        End If

        If mISBasicSalary = "N" Then
            SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM " & mTable & " SALDEF, " & mSalaryHeadTable & " SMAST " & vbCrLf & " WHERE SALDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALDEF.COMPANY_CODE=SMAST.COMPANY_CODE " & vbCrLf & " AND SALDEF.ADD_DEDUCTCODE=SMAST.CODE " & vbCrLf & " AND SMAST.ADDDEDUCT=" & ConEarning & " " & vbCrLf & " AND SALDEF.EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALDEF.SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From " & mTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalDef, ADODB.LockTypeEnum.adLockOptimistic)

            If RSSalDef.EOF = False Then
                CalcBSalary = CalcBSalary + IIf(IsDbNull(RSSalDef.Fields("Amount").Value), 0, RSSalDef.Fields("Amount").Value)
            End If
        End If

        CalcBSalary = MainClass.FormatRupees(CalcBSalary)

        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
