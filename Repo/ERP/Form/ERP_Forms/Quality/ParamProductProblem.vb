Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamProductProblem
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColSlipNo As Short = 1
    Private Const ColProblemDate As Short = 2
    Private Const ColProblemDept As Short = 3
    Private Const ColCustomer As Short = 4
    Private Const ColProduct As Short = 5
    Private Const ColModel As Short = 6
    Private Const ColProblemObserved As Short = 7
    Private Const ColProblemEnteredBy As Short = 8
    Private Const ColActionDate As Short = 9
    Private Const ColActionDept As Short = 10
    Private Const ColRootCause As Short = 11
    Private Const ColActionTaken As Short = 12
    Private Const ColEffectiveness As Short = 13
    Private Const ColActionTakenBy As Short = 14

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboProblemDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboProblemDate.SelectedIndexChanged
        If cboProblemDate.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboProblemDate.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboProblemDate.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboProblemDate.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboProblemDate.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub chkAllCustomer_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCustomer.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCustomer.Enabled = False
            CmdSearchCustomer.Enabled = False
        Else
            txtCustomer.Enabled = True
            CmdSearchCustomer.Enabled = True
        End If
    End Sub

    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItem.Enabled = False
            cmdSearchItem.Enabled = False
        Else
            txtItem.Enabled = True
            cmdSearchItem.Enabled = True
        End If
    End Sub

    Private Sub chkAllDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDept.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDept.Enabled = False
            cmdSearchDept.Enabled = False
        Else
            txtDept.Enabled = True
            cmdSearchDept.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnProductProblem(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnProductProblem(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnProductProblem(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Product Problem Log"
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDept.Text) <> "" Then
            mSubTitle = mSubTitle & " [Department : " & txtDept.Text & " ]"
        End If
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomer.Text) <> "" Then
            mSubTitle = mSubTitle & " [Customer : " & txtCustomer.Text & " ]"
        End If
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItem.Text) <> "" Then
            mSubTitle = mSubTitle & " [Product : " & txtItem.Text & " ]"
        End If
        If cboProblemDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboProblemDate.Text = "After" Then
            mSubTitle = mSubTitle & " [After  " & txtDate1.Text & " ]"
        End If
        If cboProblemDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Before  " & txtDate1.Text & " ]"
        End If
        If cboProblemDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [On  " & txtDate1.Text & " ]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProductProblemLog.rpt"

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

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtDept.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName
        End If
        txtDept.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCustomer.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND SUPP_CUST_TYPE = 'C' "
        If MainClass.SearchGridMaster(txtCustomer.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtCustomer.Text = AcName
        End If
        txtCustomer.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtItem.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItem.Text = AcName
        End If
        txtItem.Focus()
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
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamProductProblem_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Product Problem Log"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True

        FormatSprdMain(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamProductProblem_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllDept_CheckStateChanged(chkAllDept, New System.EventArgs())
        chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllCustomer_CheckStateChanged(chkAllCustomer, New System.EventArgs())
        chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllItem_CheckStateChanged(chkAllItem, New System.EventArgs())
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()
        cboProblemDate.Items.Clear()
        cboProblemDate.Items.Add("None")
        cboProblemDate.Items.Add("Between")
        cboProblemDate.Items.Add("After")
        cboProblemDate.Items.Add("Before")
        cboProblemDate.Items.Add("On Date")
        cboProblemDate.SelectedIndex = 0
    End Sub

    Private Sub frmParamProductProblem_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mSlipNo As Integer
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColSlipNo
        mSlipNo = Val(SprdMain.Text)
        frmProductProblem.MdiParent = Me.MdiParent
        frmProductProblem.Show()
        frmProductProblem.frmProductProblem_Activated(Nothing, New System.EventArgs())
        frmProductProblem.txtNumber.Text = CStr(mSlipNo)
        frmProductProblem.txtNumber_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND SUPP_CUST_TYPE = 'C' "
        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Customer", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtItem_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.DoubleClick
        Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItem_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItem_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItem.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtItem.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtItem.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Not a valid Item")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
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

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Dept", vbInformation)
            Cancel = True
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
            .MaxCols = ColActionTakenBy
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColSlipNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColProblemDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeDateSeparator = Asc("/")

            .Col = ColProblemDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCustomer
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColProduct
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColProblemObserved
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColProblemEnteredBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColActionDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeDateSeparator = Asc("/")

            .Col = ColActionDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRootCause
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColActionTaken
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColEffectiveness
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColActionTakenBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

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

        MakeSQL = " SELECT PROBLEM.AUTO_KEY_PROBLEM, PROBLEM.PROBLEM_DATE, PROBLEM_DEPT.DEPT_DESC, CUSTOMER.SUPP_CUST_NAME, " & vbCrLf & " PRODUCT.ITEM_SHORT_DESC, PRODUCT.ITEM_MODEL, PROBLEM.PROBLEM_DESC, PROBLEM_EMP.EMP_NAME, " & vbCrLf & " PROBLEM.ACTION_DATE, ACTION_DEPT.DEPT_DESC, PROBLEM.ROOT_CAUSE, " & vbCrLf & " PROBLEM.ACTION_TAKEN, PROBLEM.EFFECTIVENESS, ACTION_EMP.EMP_NAME " & vbCrLf & " FROM QAL_PRODUCT_PROBLEM_TRN PROBLEM, PAY_DEPT_MST PROBLEM_DEPT, " & vbCrLf & " FIN_SUPP_CUST_MST CUSTOMER, INV_ITEM_MST PRODUCT, PAY_EMPLOYEE_MST PROBLEM_EMP, " & vbCrLf & " PAY_DEPT_MST ACTION_DEPT, PAY_EMPLOYEE_MST ACTION_EMP " & vbCrLf & " WHERE PROBLEM.COMPANY_CODE=PROBLEM_DEPT.COMPANY_CODE AND PROBLEM.PROBLEM_DEPT_CODE=PROBLEM_DEPT.DEPT_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=CUSTOMER.COMPANY_CODE AND PROBLEM.SUPP_CUST_CODE=CUSTOMER.SUPP_CUST_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=PRODUCT.COMPANY_CODE AND PROBLEM.ITEM_CODE=PRODUCT.ITEM_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=PROBLEM_EMP.COMPANY_CODE AND PROBLEM.PROBLEM_EMP_CODE=PROBLEM_EMP.EMP_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=ACTION_DEPT.COMPANY_CODE AND PROBLEM.PROBLEM_DEPT_CODE=ACTION_DEPT.DEPT_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=ACTION_EMP.COMPANY_CODE AND PROBLEM.PROBLEM_EMP_CODE=ACTION_EMP.EMP_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(PROBLEM.AUTO_KEY_PROBLEM,LENGTH(PROBLEM.AUTO_KEY_PROBLEM)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDept.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PROBLEM_DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"
        End If
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomer.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CUSTOMER.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        End If
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItem.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PRODUCT.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(txtItem.Text) & "'"
        End If

        If cboProblemDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PROBLEM.PROBLEM_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboProblemDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PROBLEM.PROBLEM_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboProblemDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PROBLEM.PROBLEM_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboProblemDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PROBLEM.PROBLEM_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY PROBLEM.PROBLEM_DATE "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDept.Text) = "" Then
            MsgBox("Please Select Dept")
            txtDept.Focus()
            Exit Function
        End If
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomer.Text) = "" Then
            MsgBox("Please Select Customer")
            txtCustomer.Focus()
            Exit Function
        End If
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtItem.Text) = "" Then
            MsgBox("Please Select Item.")
            txtItem.Focus()
            Exit Function
        End If
        If cboProblemDate.Text = "Between" Then
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
        If cboProblemDate.Text = "After" Or cboProblemDate.Text = "Before" Or cboProblemDate.Text = "On Date" Then
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
End Class
