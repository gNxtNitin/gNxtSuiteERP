Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProductProblem
    Inherits System.Windows.Forms.Form
    Dim RsProductProblem As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If lblFormType.Text = "A" Then MsgInformation("Cann't Add from Problem Action Action") : Exit Sub

        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsProductProblem.EOF = False Then RsProductProblem.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If lblFormType.Text = "A" Then MsgInformation("Cann't Delete from Problem Corrective Action") : Exit Sub

        If Not RsProductProblem.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_PRODUCT_PROBLEM_TRN", (txtNumber.Text), RsProductProblem) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_PRODUCT_PROBLEM_TRN WHERE AUTO_KEY_PROBLEM=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsProductProblem.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsProductProblem.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProductProblem, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_PRODUCT_PROBLEM_TRN " & vbCrLf _
                            & " (AUTO_KEY_PROBLEM, COMPANY_CODE, " & vbCrLf _
                            & " PROBLEM_DATE, PROBLEM_DEPT_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf _
                            & " PROBLEM_DESC, PROBLEM_EMP_CODE, ACTION_DATE, ACTION_DEPT_CODE, " & vbCrLf _
                            & " ROOT_CAUSE, ACTION_TAKEN, EFFECTIVENESS, ACTION_EMP_CODE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtProblemDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtProblemDeptCode.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "','" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtProblemDesc.Text) & "','" & MainClass.AllowSingleQuote(txtProblemEmpCode.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtActionDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtActionDeptCode.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRootCause.Text) & "','" & MainClass.AllowSingleQuote(txtActionTaken.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtEffectiveness.Text) & "','" & MainClass.AllowSingleQuote(txtActionEmpCode.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_PRODUCT_PROBLEM_TRN SET " & vbCrLf _
                    & " AUTO_KEY_PROBLEM=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " PROBLEM_DATE=TO_DATE('" & vb6.Format(txtProblemDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PROBLEM_DEPT_CODE='" & MainClass.AllowSingleQuote(txtProblemDeptCode.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                    & " PROBLEM_DESC='" & MainClass.AllowSingleQuote(txtProblemDesc.Text) & "', " & vbCrLf _
                    & " PROBLEM_EMP_CODE='" & MainClass.AllowSingleQuote(txtProblemEmpCode.Text) & "', " & vbCrLf _
                    & " ACTION_DATE=TO_DATE('" & vb6.Format(txtActionDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " ACTION_DEPT_CODE='" & MainClass.AllowSingleQuote(txtActionDeptCode.Text) & "', " & vbCrLf _
                    & " ROOT_CAUSE='" & MainClass.AllowSingleQuote(txtRootCause.Text) & "', " & vbCrLf _
                    & " ACTION_TAKEN='" & MainClass.AllowSingleQuote(txtActionTaken.Text) & "', " & vbCrLf _
                    & " EFFECTIVENESS='" & MainClass.AllowSingleQuote(txtEffectiveness.Text) & "', " & vbCrLf _
                    & " ACTION_EMP_CODE='" & MainClass.AllowSingleQuote(txtActionEmpCode.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_PROBLEM =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsProductProblem.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_PROBLEM)  " & vbCrLf & " FROM QAL_PRODUCT_PROBLEM_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROBLEM,LENGTH(AUTO_KEY_PROBLEM)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdSearchActionDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchActionDept.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtActionDeptCode.Text = AcName1
            lblActionDeptName.text = AcName
            txtActionDeptCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchActionEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchActionEmp.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtActionEmpCode.Text = AcName1
            lblActionEmpName.text = AcName
            txtActionEmpCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchProblemDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProblemDept.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtProblemDeptCode.Text = AcName1
            lblProblemDeptName.text = AcName
            txtProblemDeptCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchSuppCust_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSuppCust.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND SUPP_CUST_TYPE = 'C' "
        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSuppCustCode.Text = AcName1
            lblSuppCustName.text = AcName
            txtSuppCustCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROBLEM,LENGTH(AUTO_KEY_PROBLEM)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If lblFormType.Text = "A" Then
            SqlStr = SqlStr & vbCrLf & " AND (ACTION_DATE IS NULL OR ACTION_DATE = '') "
        End If
        If MainClass.SearchGridMaster(txtNumber.Text, "QAL_PRODUCT_PROBLEM_TRN", "AUTO_KEY_PROBLEM", "PROBLEM_DATE", "SUPP_CUST_CODE", "ITEM_CODE", SqlStr) = True Then
            txtNumber.Text = AcName
            Call txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        Dim SqlStr As String
        SqlStr = "SELECT B.ITEM_SHORT_DESC, A.ITEM_CODE, B.ITEM_MODEL " & vbCrLf _
                & " FROM FIN_SUPP_CUST_DET A ,INV_ITEM_MST B  " & vbCrLf _
                & " WHERE A.COMPANY_CODE =B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE =  B.ITEM_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' " & vbCrLf _
                & " ORDER BY A.ITEM_CODE   "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtItemCode.Text = AcName1
            lblItemName.text = AcName
            txtItemCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchProblemEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProblemEmp.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtProblemEmpCode.Text = AcName1
            lblProblemEmpName.text = AcName
            txtProblemEmpCode.Focus()
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsProductProblem, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmProductProblem_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblFormType.Text = "P" Then
            Me.Text = "Product Problem Entry"
        ElseIf lblFormType.Text = "A" Then
            Me.Text = "Problem Corrective Action"
        End If

        SqlStr = "Select * From QAL_PRODUCT_PROBLEM_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProductProblem, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()

        If lblFormType.Text = "P" Then
            fraProblem.Enabled = True
            fraAction.Enabled = False
        ElseIf lblFormType.Text = "A" Then
            fraProblem.Enabled = False
            fraAction.Enabled = True
        End If

        If lblFormType.Text = "P" Then
            If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_PROBLEM AS PROBLEM_NUMBER, TO_CHAR(PROBLEM_DATE,'DD/MM/YYYY') AS PROBLEM_DATE, " & vbCrLf & " PROBLEM_DEPT_CODE, SUPP_CUST_CODE, ITEM_CODE, PROBLEM_DESC, PROBLEM_EMP_CODE, " & vbCrLf & " TO_CHAR(ACTION_DATE,'DD/MM/YYYY') AS ACTION_DATE, ACTION_DEPT_CODE, " & vbCrLf & " ROOT_CAUSE, ACTION_TAKEN, EFFECTIVENESS, ACTION_EMP_CODE " & vbCrLf & " FROM QAL_PRODUCT_PROBLEM_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROBLEM,LENGTH(AUTO_KEY_PROBLEM)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_PROBLEM"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmProductProblem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProductProblem_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(7080)
        Me.Width = VB6.TwipsToPixelsX(9285)
        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtProblemDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtProblemDeptCode.Text = ""
        lblProblemDeptName.Text = ""
        txtSuppCustCode.Text = ""
        lblSuppCustName.Text = ""
        txtItemCode.Text = ""
        lblItemName.Text = ""
        lblItemModel.Text = ""
        txtProblemDesc.Text = ""
        txtProblemEmpCode.Text = ""
        lblProblemEmpName.Text = ""
        txtActionDate.Text = ""
        txtActionDeptCode.Text = ""
        lblActionDeptName.Text = ""
        txtRootCause.Text = ""
        txtActionTaken.Text = ""
        txtEffectiveness.Text = ""
        txtActionEmpCode.Text = ""
        lblActionEmpName.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsProductProblem, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 2)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 2)
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 2)
            .set_ColWidth(6, 500 * 2)
            .set_ColWidth(7, 500 * 2)
            .set_ColWidth(8, 500 * 4)
            .set_ColWidth(9, 500 * 2)
            .set_ColWidth(10, 500 * 2)
            .set_ColWidth(11, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsProductProblem.Fields("AUTO_KEY_PROBLEM").Precision
        txtProblemDate.Maxlength = RsProductProblem.Fields("PROBLEM_DATE").DefinedSize - 6
        txtProblemDeptCode.Maxlength = RsProductProblem.Fields("PROBLEM_DEPT_CODE").DefinedSize
        txtSuppCustCode.Maxlength = RsProductProblem.Fields("SUPP_CUST_CODE").DefinedSize
        txtItemCode.Maxlength = RsProductProblem.Fields("ITEM_CODE").DefinedSize
        txtProblemDesc.Maxlength = RsProductProblem.Fields("PROBLEM_DESC").DefinedSize
        txtProblemEmpCode.Maxlength = RsProductProblem.Fields("PROBLEM_EMP_CODE").DefinedSize
        txtActionDate.Maxlength = RsProductProblem.Fields("ACTION_DATE").DefinedSize - 6
        txtActionDeptCode.Maxlength = RsProductProblem.Fields("ACTION_DEPT_CODE").DefinedSize
        txtRootCause.Maxlength = RsProductProblem.Fields("ROOT_CAUSE").DefinedSize
        txtActionTaken.Maxlength = RsProductProblem.Fields("ACTION_TAKEN").DefinedSize
        txtEffectiveness.Maxlength = RsProductProblem.Fields("EFFECTIVENESS").DefinedSize
        txtActionEmpCode.Maxlength = RsProductProblem.Fields("ACTION_EMP_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsProductProblem.EOF = True Then Exit Function

        If Trim(txtProblemDate.Text) = "" Then
            MsgInformation("Problem Date is empty, So unable to save.")
            txtProblemDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProblemDeptCode.Text) = "" Then
            MsgInformation("Problem Department is empty, So unable to save.")
            txtProblemDeptCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSuppCustCode.Text) = "" Then
            MsgInformation("Customer is empty, So unable to save.")
            txtSuppCustCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Product is empty, So unable to save.")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProblemDesc.Text) = "" Then
            MsgInformation("Problem Observed is empty, So unable to save.")
            txtProblemDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProblemEmpCode.Text) = "" Then
            MsgInformation("Problem Entered By is empty, So unable to save.")
            txtProblemEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If lblFormType.Text = "A" Then
            If Trim(txtActionDate.Text) = "" Then
                MsgInformation("Action Date is empty, So unable to save.")
                txtActionDate.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtActionDeptCode.Text) = "" Then
                MsgInformation("Action Department is empty, So unable to save.")
                txtActionDeptCode.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtActionEmpCode.Text) = "" Then
                MsgInformation("Action Taken By is empty, So unable to save.")
                txtActionEmpCode.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmProductProblem_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsProductProblem.Close()
        RsProductProblem = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Public Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        If CDbl(Trim(txtNumber.Text)) < 6 Then
            txtNumber.Text = Trim(txtNumber.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsProductProblem.BOF = False Then xMKey = RsProductProblem.Fields("AUTO_KEY_PROBLEM").Value

        SqlStr = "SELECT * FROM QAL_PRODUCT_PROBLEM_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROBLEM,LENGTH(AUTO_KEY_PROBLEM)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PROBLEM=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProductProblem, ADODB.LockTypeEnum.adLockReadOnly)
        If RsProductProblem.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_PRODUCT_PROBLEM_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROBLEM,LENGTH(AUTO_KEY_PROBLEM)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PROBLEM=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProductProblem, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProblemDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProblemDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProblemDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProblemDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProblemDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProblemDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProblemDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtProblemDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProblemDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If Trim(txtActionDate.Text) <> "" Then
                If CDate(txtActionDate.Text) < CDate(txtProblemDate.Text) Then
                    MsgBox("Problem date cann't be greater than Action Date.")
                    Cancel = True
                End If
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProblemDeptCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProblemDeptCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProblemDeptCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProblemDeptCode.DoubleClick
        Call cmdSearchProblemDept_Click(cmdSearchProblemDept, New System.EventArgs())
    End Sub

    Private Sub txtProblemDeptCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProblemDeptCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProblemDeptCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProblemDeptCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProblemDeptCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProblemDept_Click(cmdSearchProblemDept, New System.EventArgs())
    End Sub

    Private Sub txtProblemDeptCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProblemDeptCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtProblemDeptCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtProblemDeptCode.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Department Does Not Exist In Master.")
            Cancel = True
        Else
            lblProblemDeptName.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSuppCustCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCustCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppCustCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCustCode.DoubleClick
        Call cmdSearchSuppCust_Click(cmdSearchSuppCust, New System.EventArgs())
    End Sub

    Private Sub txtSuppCustCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSuppCustCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSuppCustCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSuppCustCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSuppCustCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSuppCust_Click(cmdSearchSuppCust, New System.EventArgs())
    End Sub

    Private Sub txtSuppCustCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppCustCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtSuppCustCode.Text) = "" Then
            lblSuppCustName.Text = ""
            txtItemCode.Text = ""
            lblItemName.Text = ""
            lblItemModel.Text = ""
            GoTo EventExitSub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND SUPP_CUST_TYPE = 'C' "
        If MainClass.ValidateWithMasterTable(txtSuppCustCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Customer Does Not Exist In Master.")
            Cancel = True
        Else
            lblSuppCustName.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mRsTemp As ADODB.Recordset
        Dim SqlStr As String
        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        If Trim(txtSuppCustCode.Text) = "" Then
            MsgInformation("Please Enter Customer.")
            txtItemCode.Text = ""
            lblItemName.Text = ""
            lblItemModel.Text = ""
            txtSuppCustCode.Focus()
            GoTo EventExitSub
        End If
        SqlStr = " SELECT A.ITEM_CODE,B.ITEM_SHORT_DESC,B.ITEM_MODEL " & vbCrLf _
                & " FROM FIN_SUPP_CUST_DET A,INV_ITEM_MST B  " & vbCrLf _
                & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE = B.ITEM_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND A.ITEM_CODE ='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' " & vbCrLf _
                & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtItemCode.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                lblItemName.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                lblItemModel.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)
            Else
                MsgBox("Not a valid Customer's Product.")
                lblItemName.Text = ""
                lblItemModel.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProblemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProblemDesc.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProblemDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProblemDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProblemDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProblemEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProblemEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProblemEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProblemEmpCode.DoubleClick
        Call cmdSearchProblemEmp_Click(cmdSearchProblemEmp, New System.EventArgs())
    End Sub

    Private Sub txtProblemEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProblemEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProblemEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProblemEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProblemEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProblemEmp_Click(cmdSearchProblemEmp, New System.EventArgs())
    End Sub

    Private Sub txtProblemEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProblemEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtProblemEmpCode.Text) = "" Then GoTo EventExitSub
        txtProblemEmpCode.Text = VB6.Format(txtProblemEmpCode.Text, "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(txtProblemEmpCode.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblProblemEmpName.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtActionDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtActionDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtActionDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtActionDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtActionDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtActionDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtActionDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtActionDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If CDate(txtActionDate.Text) < CDate(txtProblemDate.Text) Then
                MsgBox("Action date cann't be less than Problem Date.")
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtActionDeptCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionDeptCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtActionDeptCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionDeptCode.DoubleClick
        Call cmdSearchActionDept_Click(cmdSearchActionDept, New System.EventArgs())
    End Sub

    Private Sub txtActionDeptCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtActionDeptCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtActionDeptCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtActionDeptCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtActionDeptCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchActionDept_Click(cmdSearchActionDept, New System.EventArgs())
    End Sub

    Private Sub txtActionDeptCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtActionDeptCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtActionDeptCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtActionDeptCode.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Department Does Not Exist In Master.")
            Cancel = True
        Else
            lblActionDeptName.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRootCause_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRootCause.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRootCause_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRootCause.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRootCause.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtActionTaken_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionTaken.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtActionTaken_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtActionTaken.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtActionTaken.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEffectiveness_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEffectiveness.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEffectiveness_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEffectiveness.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEffectiveness.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtActionEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtActionEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionEmpCode.DoubleClick
        Call cmdSearchActionEmp_Click(cmdSearchActionEmp, New System.EventArgs())
    End Sub

    Private Sub txtActionEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtActionEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtActionEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtActionEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtActionEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchActionEmp_Click(cmdSearchActionEmp, New System.EventArgs())
    End Sub

    Private Sub txtActionEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtActionEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtActionEmpCode.Text) = "" Then GoTo EventExitSub
        txtActionEmpCode.Text = VB6.Format(txtActionEmpCode.Text, "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(txtActionEmpCode.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblActionEmpName.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsProductProblem.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsProductProblem.Fields("AUTO_KEY_PROBLEM").Value), "", RsProductProblem.Fields("AUTO_KEY_PROBLEM").Value)
            txtNumber.Text = IIf(IsDbNull(RsProductProblem.Fields("AUTO_KEY_PROBLEM").Value), "", RsProductProblem.Fields("AUTO_KEY_PROBLEM").Value)
            txtProblemDate.Text = IIf(IsDbNull(RsProductProblem.Fields("PROBLEM_DATE").Value), "", RsProductProblem.Fields("PROBLEM_DATE").Value)
            txtProblemDeptCode.Text = IIf(IsDbNull(RsProductProblem.Fields("PROBLEM_DEPT_CODE").Value), "", RsProductProblem.Fields("PROBLEM_DEPT_CODE").Value)
            txtProblemDeptCode_Validating(txtProblemDeptCode, New System.ComponentModel.CancelEventArgs(False))
            txtSuppCustCode.Text = IIf(IsDbNull(RsProductProblem.Fields("SUPP_CUST_CODE").Value), "", RsProductProblem.Fields("SUPP_CUST_CODE").Value)
            txtSuppCustCode_Validating(txtSuppCustCode, New System.ComponentModel.CancelEventArgs(False))
            txtItemCode.Text = IIf(IsDbNull(RsProductProblem.Fields("ITEM_CODE").Value), "", RsProductProblem.Fields("ITEM_CODE").Value)
            txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False))
            txtProblemDesc.Text = IIf(IsDbNull(RsProductProblem.Fields("PROBLEM_DESC").Value), "", RsProductProblem.Fields("PROBLEM_DESC").Value)
            txtProblemEmpCode.Text = IIf(IsDbNull(RsProductProblem.Fields("PROBLEM_EMP_CODE").Value), "", RsProductProblem.Fields("PROBLEM_EMP_CODE").Value)
            txtProblemEmpCode_Validating(txtProblemEmpCode, New System.ComponentModel.CancelEventArgs(False))
            txtActionDate.Text = IIf(IsDbNull(RsProductProblem.Fields("ACTION_DATE").Value), "", RsProductProblem.Fields("ACTION_DATE").Value)
            txtActionDeptCode.Text = IIf(IsDbNull(RsProductProblem.Fields("ACTION_DEPT_CODE").Value), "", RsProductProblem.Fields("ACTION_DEPT_CODE").Value)
            txtActionDeptCode_Validating(txtActionDeptCode, New System.ComponentModel.CancelEventArgs(False))
            txtRootCause.Text = IIf(IsDbNull(RsProductProblem.Fields("ROOT_CAUSE").Value), "", RsProductProblem.Fields("ROOT_CAUSE").Value)
            txtActionTaken.Text = IIf(IsDbNull(RsProductProblem.Fields("ACTION_TAKEN").Value), "", RsProductProblem.Fields("ACTION_TAKEN").Value)
            txtEffectiveness.Text = IIf(IsDbNull(RsProductProblem.Fields("EFFECTIVENESS").Value), "", RsProductProblem.Fields("EFFECTIVENESS").Value)
            txtActionEmpCode.Text = IIf(IsDbNull(RsProductProblem.Fields("ACTION_EMP_CODE").Value), "", RsProductProblem.Fields("ACTION_EMP_CODE").Value)
            txtActionEmpCode_Validating(txtActionEmpCode, New System.ComponentModel.CancelEventArgs(False))
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsProductProblem, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ReportOnProductProblem(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Product Problem and Corrective Action Taken"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProductProblem.rpt"

        SqlStr = MakeSQL

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Function MakeSQL() As String
        On Error GoTo ERR1

        MakeSQL = " SELECT PROBLEM.PROBLEM_DATE, PROBLEM_DEPT.DEPT_DESC, CUSTOMER.SUPP_CUST_NAME, " & vbCrLf & " PRODUCT.ITEM_SHORT_DESC, PRODUCT.ITEM_MODEL, PROBLEM.PROBLEM_DESC, PROBLEM_EMP.EMP_NAME, " & vbCrLf & " PROBLEM.ACTION_DATE, ACTION_DEPT.DEPT_DESC, PROBLEM.ROOT_CAUSE, " & vbCrLf & " PROBLEM.ACTION_TAKEN, PROBLEM.EFFECTIVENESS, ACTION_EMP.EMP_NAME " & vbCrLf & " FROM QAL_PRODUCT_PROBLEM_TRN PROBLEM, PAY_DEPT_MST PROBLEM_DEPT, " & vbCrLf & " FIN_SUPP_CUST_MST CUSTOMER, INV_ITEM_MST PRODUCT, PAY_EMPLOYEE_MST PROBLEM_EMP, " & vbCrLf & " PAY_DEPT_MST ACTION_DEPT, PAY_EMPLOYEE_MST ACTION_EMP " & vbCrLf & " WHERE PROBLEM.COMPANY_CODE=PROBLEM_DEPT.COMPANY_CODE AND PROBLEM.PROBLEM_DEPT_CODE=PROBLEM_DEPT.DEPT_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=CUSTOMER.COMPANY_CODE AND PROBLEM.SUPP_CUST_CODE=CUSTOMER.SUPP_CUST_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=PRODUCT.COMPANY_CODE AND PROBLEM.ITEM_CODE=PRODUCT.ITEM_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=PROBLEM_EMP.COMPANY_CODE AND PROBLEM.PROBLEM_EMP_CODE=PROBLEM_EMP.EMP_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=ACTION_DEPT.COMPANY_CODE AND PROBLEM.PROBLEM_DEPT_CODE=ACTION_DEPT.DEPT_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=ACTION_EMP.COMPANY_CODE AND PROBLEM.PROBLEM_EMP_CODE=ACTION_EMP.EMP_CODE " & vbCrLf & " AND PROBLEM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(PROBLEM.AUTO_KEY_PROBLEM,LENGTH(PROBLEM.AUTO_KEY_PROBLEM)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND AUTO_KEY_PROBLEM=" & Val(txtNumber.Text) & ""

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProductProblem(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProductProblem(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
