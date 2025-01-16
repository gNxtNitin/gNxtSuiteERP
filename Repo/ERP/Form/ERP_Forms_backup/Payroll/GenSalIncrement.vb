Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmGenSalIncrement
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim Shw As Boolean
    Dim xCode As String

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColCode As Short = 1
    Private Const ColDesc As Short = 2
    Private Const ColPer As Short = 3
    Private Const ColAmt As Short = 4


    Private Sub Clear1()

        txtWEF.Text = ""

        cboAppMon.Text = MonthName(Month(RunDate))
        cboAppYear.Text = CStr(Year(RunDate))

        cboArrearMonth.Text = MonthName(Month(RunDate))
        cboArrearYear.Text = CStr(Year(RunDate))

        '    MainClass.ClearGrid sprdEarn, -1
        '    MainClass.ClearGrid sprdDeduct, -1
        lblAppDate.Text = ""

        cboAppMon.Enabled = True
        cboAppYear.Enabled = True

        cboArrearMonth.Enabled = True
        cboArrearYear.Enabled = True

        fraSalMY.Enabled = False
        FillSalarySprd()

    End Sub
    Private Sub cboAppMon_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAppMon.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboAppMon_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAppMon.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If cboArrearMonth.Text = "" Then cboArrearMonth.Text = cboAppMon.Text
    End Sub

    Private Sub cboAppMon_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboAppMon.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        lblAppDate.Text = "01/" & MonthValue((cboAppMon.Text)) & "/" & cboAppYear.Text
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboAppYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAppYear.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboAppYear_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAppYear.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If cboArrearYear.Text = "" Then cboArrearYear.Text = cboAppYear.Text
    End Sub

    Private Sub cboAppYear_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboAppYear.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        lblAppDate.Text = "01/" & MonthValue((cboAppMon.Text)) & "/" & cboAppYear.Text

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboArrearMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboArrearMonth.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboArrearMonth_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboArrearMonth.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cboArrearYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboArrearYear.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboArrearYear_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboArrearYear.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cboYear_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboYear.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        cboDept.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        cboCategory.Enabled = IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub


    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If Trim(lblAppDate.Text) = "" Then MsgExclamation("Nothing to delete") : Exit Sub

        If CheckSalaryMade(VB6.Format(lblAppDate.Text, "DD/MM/YYYY")) = True Then
            MsgInformation("Salary Made Against This Month So Cann't be deleted.")
            Exit Sub
        End If

        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
            If Delete1 = False Then GoTo DelErrPart
            Clear1()
            CmdSave.Enabled = True
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub frmGenSalIncrement_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub optBasicSalary_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBasicSalary.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBasicSalary.GetIndex(eventSender)
            If Index = 0 Then
                txtBasicSalary.Enabled = False
            Else
                txtBasicSalary.Enabled = True
            End If
        End If
    End Sub

    Private Sub sprdDeduct_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdDeduct.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdDeduct_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdDeduct.LeaveCell
        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdDeduct.Row = eventArgs.row

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub sprdDeduct_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdDeduct.Leave
        With sprdDeduct
            sprdDeduct_LeaveCell(sprdDeduct, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub sprdEarn_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdEarn.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboMonth_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMonth.SelectedIndexChanged
        '    MainClass.SaveStatus Me, ADDMode, MODIFYMode
        '    If Trim(TxtName.Text) = "" Then Exit Sub
        '
        '    If ADDMode = True Then Exit Sub

    End Sub
    Private Sub frmGenSalIncrement_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        SqlStr = "Select * From PAY_SalaryDef_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        cboMonth.Text = MonthName(Month(RunDate))
        cboYear.Text = CStr(Year(RunDate))
        cboAppMon.Text = MonthName(Month(RunDate))
        cboAppYear.Text = CStr(Year(RunDate))

        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Resume
    End Sub
    Private Sub frmGenSalIncrement_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Height = VB6.TwipsToPixelsY(6780)
        Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = 0
        Me.Top = 0

        Call FillComboMst()
        FormatSprd(-1)

        FillMonthYearCombo()
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        cboCategory.Enabled = False
        cboDept.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub FillMonthYearCombo()
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntMon As Integer
        Dim cntYear As Integer

        cboMonth.Items.Clear()
        cboAppMon.Items.Clear()
        cboArrearMonth.Items.Clear()
        For cntMon = 1 To 12
            cboMonth.Items.Add(MonthName(cntMon))
            cboAppMon.Items.Add(MonthName(cntMon))
            cboArrearMonth.Items.Add(MonthName(cntMon))
        Next

        cboYear.Items.Clear()
        cboAppYear.Items.Clear()
        cboArrearYear.Items.Clear()
        For cntYear = 1970 To 2200
            cboYear.Items.Add(CStr(cntYear))
            cboAppYear.Items.Add(CStr(cntYear))
            cboArrearYear.Items.Add(CStr(cntYear))
        Next
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmGenSalIncrement_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsEmp = Nothing
        'Set PvtDBCn = Nothing
    End Sub



    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If


        If Update1 = True Then
            CmdSave.Enabled = False
        Else
            MsgInformation("Record not saved")
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mEmpCode As String
        Dim RsEmp As ADODB.Recordset = Nothing

        Dim mCode As Integer
        Dim mArrearCalc As String = ""
        Dim mAppDate As Date
        Dim mWEF As Date
        Dim mArrearDate As Date
        Dim mArrMon As Integer
        Dim mDeptCode As String

        SqlStr = ""


        mAppDate = CDate("01/" & MonthValue((cboAppMon.Text)) & "/" & Val(cboAppYear.Text))
        mWEF = CDate("01/" & MonthValue((cboMonth.Text)) & "/" & Val(cboYear.Text))
        mArrearDate = CDate("01/" & MonthValue((cboArrearMonth.Text)) & "/" & Val(cboArrearYear.Text))
        mArrMon = DateDiff(Microsoft.VisualBasic.DateInterval.Month, mWEF, mAppDate)

        SqlStr = " SELECT * FROM " & vbCrLf & " PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_STOP_SALARY='N' AND " & vbCrLf & " EMP_DOJ <=TO_DATE('" & VB6.Format(mWEF, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mWEF, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & "Order by EMP_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsEmp.EOF = False Then
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            Do While RsEmp.EOF = False
                mEmpCode = IIf(IsDbNull(RsEmp.Fields("EMP_CODE").Value), "", RsEmp.Fields("EMP_CODE").Value)

                If mEmpCode <> "" Then

                    If UpdateEmpSalary(mEmpCode, CStr(mWEF), CStr(mAppDate), CStr(mArrearDate), mArrMon, mArrearCalc) = False Then GoTo UpdateError

                End If
                RsEmp.MoveNext()
            Loop
            PubDBCn.CommitTrans()
        End If

        If mArrMon > 0 Then
            MsgInformation(mArrMon & " Month " & " Arrear Also Calculated.")
        End If

        ' FillMonthYearCombo
        RsEmp.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        '    Resume
        PubDBCn.RollbackTrans()
        RsEmp.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If

        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim xAmount As Decimal
        Dim mAppDate As Date
        Dim mWEF As Date
        Dim mArrearDate As Date

        FieldsVarification = True

        If cboAppMon.Text = "" Then
            MsgInformation("Applicable Month can not be blank.")
            cboAppMon.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboAppYear.Text = "" Then
            MsgInformation("Applicable Year can not be blank.")
            cboAppYear.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboMonth.Text = "" Then
            MsgInformation("WEF Month can not be blank.")
            cboMonth.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboYear.Text = "" Then
            MsgInformation("WEF Year can not be blank.")
            cboYear.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboArrearMonth.Text = "" Then
            MsgInformation("Arrear Month can not be blank.")
            cboArrearMonth.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboArrearYear.Text = "" Then
            MsgInformation("Arrear Year can not be blank.")
            cboArrearYear.Focus()
            FieldsVarification = False
            Exit Function
        End If


        mAppDate = CDate("01/" & MonthValue((cboAppMon.Text)) & "/" & Val(cboAppYear.Text))
        mWEF = CDate("01/" & MonthValue((cboMonth.Text)) & "/" & Val(cboYear.Text))
        mArrearDate = CDate("01/" & MonthValue((cboArrearMonth.Text)) & "/" & Val(cboArrearYear.Text))

        If mWEF > mAppDate Then
            MsgInformation("Applicable Date Cann't be Less Than WEF Date.")
            cboAppMon.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If mWEF = mAppDate Then
            cboArrearMonth.Text = cboAppMon.Text
            cboArrearYear.Text = cboAppYear.Text
        Else
            If mAppDate > mArrearDate Then
                MsgInformation("Arrear Date Cann't be Less Than Applicable Date.")
                cboAppMon.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        lblAppDate.Text = "01/" & MonthValue((cboAppMon.Text)) & "/" & cboAppYear.Text

        If PubSuperUser <> "S" Then
            If CheckSalaryMade(VB6.Format(lblAppDate.Text, "DD/MM/YYYY")) = True Then
                MsgInformation("Salary Made Againt This Month. So Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If

        '    If ADDMode = False And MODIFYMode = False Then
        '        MsgInformation "Click Add Mode Or Modify to add a new Account or modify an existing item"
        '        FieldsVarification = False
        '    End If
        '    If MODIFYMode = True And (RsEmp.EOF = True Or RsEmp.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Function Delete1() As Boolean

        On Error GoTo DeleteErr
        Dim mDeptCode As String
        Dim mRecordDelete As Integer

        SqlStr = ""

        '     If MainClass.ValidateWithMasterTable(txtEmpNo.Text, "EMP_CODE", "EMP_CODE", "PAY_SAL_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        MsgBox "Salary Exists Against This Employee."
        '        Delete1 = False
        '        Exit Function
        '    End If

        SqlStr = "Delete from PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARY_EFF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN ( " & vbCrLf & " SELECT EMP_CODE FROM " & vbCrLf & " PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_STOP_SALARY='N' AND " & vbCrLf & " EMP_DOJ <=TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & ")"

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        PubDBCn.Execute(SqlStr) '', mRecordDelete
        '    MsgInformation " Total " & mRecordDelete & " deleted."
        PubDBCn.CommitTrans()
        RsEmp.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsEmp.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub FillComboMst()

        Dim RsDept As ADODB.Recordset = Nothing
        cboDept.Items.Clear()
        MainClass.FillCombo(cboDept, "PAY_DEPT_MST", "DEPT_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        cboDept.SelectedIndex = 0

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCategory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCategory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCategory.SelectedIndex = 0

        '    cboCategory.Clear
        '    cboCategory.AddItem "General Staff"
        '    cboCategory.AddItem "Production Staff"
        '    cboCategory.AddItem "Export Staff"
        '    cboCategory.AddItem "Regular Worker"
        '    cboCategory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCategory.AddItem "Director"
        '    cboCategory.AddItem "Trainee Staff"
        '    cboCategory.ListIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillSalarySprd()

        Dim RsADD As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        MainClass.ClearGrid(sprdEarn, -1)
        MainClass.ClearGrid(sprdDeduct, -1)

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            With RsADD
                Do While Not .EOF
                    If .Fields("ADDDEDUCT").Value = ConEarning Then
                        With sprdEarn
                            .Row = .MaxRows
                            .Col = ColCode
                            .Text = CStr(RsADD.Fields("CODE").Value)

                            .Col = ColDesc
                            .Text = RsADD.Fields("Name").Value

                            .Col = ColPer
                            .Text = "0.00"
                        End With
                    ElseIf .Fields("ADDDEDUCT").Value = ConDeduct Then
                        With sprdDeduct
                            .Row = .MaxRows

                            .Col = ColCode
                            .Text = CStr(RsADD.Fields("CODE").Value)

                            .Col = ColDesc
                            .Text = RsADD.Fields("Name").Value

                            .Col = ColPer
                            .Text = "0.00"
                        End With
                    End If
                    .MoveNext()
                    If Not .EOF Then
                        If .Fields("ADDDEDUCT").Value = ConEarning Then
                            sprdEarn.Col = 1
                            sprdEarn.Row = sprdEarn.MaxRows
                            If Trim(sprdEarn.Text) <> "" Then
                                sprdEarn.MaxRows = sprdEarn.MaxRows + 1
                                If sprdEarn.MaxRows > 3 Then
                                    sprdEarn.set_ColWidth(ColDesc, 14)
                                End If
                            End If
                        ElseIf .Fields("ADDDEDUCT").Value = ConDeduct Then
                            sprdDeduct.Col = 1
                            sprdDeduct.Row = sprdDeduct.MaxRows
                            If Trim(sprdDeduct.Text) <> "" Then
                                sprdDeduct.MaxRows = sprdDeduct.MaxRows + 1
                                If sprdDeduct.MaxRows > 3 Then
                                    sprdDeduct.set_ColWidth(ColDesc, 14)
                                End If
                            End If
                        End If
                    End If
                Loop
            End With
        End If

        '    Call FormatSprd(-1)

        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdEarn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.25)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 17)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 6)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 8)

        End With

        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColPer)
        MainClass.SetSpreadColor(sprdEarn, mRow)

        With sprdDeduct

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.25)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 17)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 6)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 8)

        End With

        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColPer)
        MainClass.SetSpreadColor(sprdDeduct, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Function UpdateEmpSalary(ByRef xCode As String, ByRef xWEF As String, ByRef xAppDate As String, ByRef xArrearDate As String, ByRef xTotArrearMonth As Integer, ByRef xArrearCalc As String) As Boolean


        On Error GoTo UpdateEmpSalaryErr
        Dim RsADD As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xTypeCode As Integer
        Dim cntRow As Integer
        Dim xPrevAmount As Double
        Dim xPrevPer As Double
        Dim xPreSalary As Double
        Dim xEmpDesgCode As String = ""

        Dim xIncease_Amount As Double
        Dim xIncease_Per As Double

        Dim xSalary As Double
        Dim xAmount As Double
        Dim xPer As Double
        Dim xLastWEF As String
        Dim BasicESISalary As Double
        Dim BasicPFSalary As Double
        Dim mType As Integer
        Dim mRounding As Double
        Dim mRound As String

        If Trim(xCode) = "" Then
            UpdateEmpSalary = True
            Exit Function
        End If

        SqlStr = " DELETE FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND TO_CHAR(SALARY_EFF_DATE,'MONYYYY')='" & UCase(VB6.Format(xWEF, "MMMYYYY")) & "'"


        PubDBCn.Execute(SqlStr)

        SqlStr = " SELECT MAX(SALARY_EFF_DATE) AS SALARY_EFF_DATE From PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            xLastWEF = VB6.Format(IIf(IsDbNull(RsADD.Fields("SALARY_EFF_DATE").Value), 0, RsADD.Fields("SALARY_EFF_DATE").Value), "DD/MM/YYYY")
        Else
            xLastWEF = xWEF
        End If
        RsADD.Close()

        SqlStr = ""

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextEarnRow
                xTypeCode = Val(.Text)

                xPrevAmount = 0
                xPrevPer = 0
                xPreSalary = 0
                xIncease_Amount = 0
                xIncease_Per = 0
                xSalary = 0
                xAmount = 0
                xPer = 0

                SqlStr = " SELECT * from PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & xTypeCode & " AND  " & vbCrLf & " SALARY_EFF_DATE=TO_DATE('" & VB6.Format(xLastWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)


                .Col = ColPer
                xIncease_Per = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xIncease_Amount = IIf(IsNumeric(.Text), .Text, 0)

                If RsADD.EOF = False Then
                    xPreSalary = IIf(IsDbNull(RsADD.Fields("BASICSALARY").Value), 0, RsADD.Fields("BASICSALARY").Value)
                    xPrevAmount = IIf(IsDbNull(RsADD.Fields("Amount").Value), 0, RsADD.Fields("Amount").Value)
                    xPrevPer = IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), 0, RsADD.Fields("PERCENTAGE").Value)
                    xEmpDesgCode = IIf(IsDbNull(RsADD.Fields("EMP_DESG_CODE").Value), "", RsADD.Fields("EMP_DESG_CODE").Value)
                End If

                If optBasicSalary(0).Checked = True Then
                    xSalary = xPreSalary
                ElseIf optBasicSalary(0).Checked = True Then
                    xSalary = xPreSalary + (xPreSalary * Val(txtBasicSalary.Text) * 0.01)
                Else
                    xSalary = xPreSalary + Val(txtBasicSalary.Text)
                End If

                If xIncease_Per <> 0 Then
                    xPer = xPrevPer + xIncease_Per
                    xAmount = CDbl(CStr(xPer * Val(CStr(xSalary)) / 100))
                ElseIf Val(CStr(xIncease_Amount)) <> 0 Then
                    xAmount = xPrevAmount + xIncease_Amount
                    xPer = 0
                Else
                    xPer = xPrevPer
                    If xPrevPer <> 0 Then
                        xAmount = CDbl(CStr(xPer * Val(CStr(xSalary)) / 100))
                    Else
                        xAmount = xPrevAmount
                    End If
                End If

                If MainClass.ValidateWithMasterTable(xTypeCode, "Code", "IncludedPF", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        .Col = ColAmt
                        BasicPFSalary = BasicPFSalary + xAmount
                    End If
                End If

                If MainClass.ValidateWithMasterTable(xTypeCode, "Code", "IncludedESI", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        .Col = ColAmt
                        BasicESISalary = BasicESISalary + xAmount
                    End If
                End If

                If MainClass.ValidateWithMasterTable(xTypeCode, "Code", "ROUNDING", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mRounding = MasterNo
                End If

                If mRounding = CDbl("0.05") Then
                    xAmount = PaiseRound(xAmount, 0.05)
                Else
                    mRound = Replace(CStr(mRounding), "1", "0")
                    xAmount = CDbl(VB6.Format(xAmount, mRound))
                End If

                If xAmount <> 0 Then
                    SqlStr = " Insert Into PAY_SalaryDef_MST (COMPANY_CODE, FYEAR, " & vbCrLf & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE,ADDDAYS_IN, " & vbCrLf & " ADDUSER, ADDDATE " & vbCrLf & " ) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xPreSalary & "," & vbCrLf & " " & xPrevAmount & ",TO_DATE('" & VB6.Format(xArrearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & xTotArrearMonth & ", '" & xArrearCalc & "','" & xEmpDesgCode & "'," & vbCrLf & " 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " )"

                    PubDBCn.Execute(SqlStr)
                End If
NextEarnRow:
            Next
        End With

        BasicPFSalary = BasicPFSalary + xSalary
        BasicESISalary = BasicESISalary + xSalary

        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextDeductRow
                xTypeCode = Val(.Text)

                If MainClass.ValidateWithMasterTable(xTypeCode, "Code", "Type", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mType = MasterNo
                End If

                xPrevAmount = 0
                xPrevPer = 0
                xPreSalary = 0
                xIncease_Amount = 0
                xIncease_Per = 0
                xSalary = 0
                xAmount = 0
                xPer = 0

                SqlStr = " SELECT * from PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & xTypeCode & " AND  " & vbCrLf & " SALARY_EFF_DATE=TO_DATE('" & VB6.Format(xLastWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)


                .Col = ColPer
                xIncease_Per = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xIncease_Amount = IIf(IsNumeric(.Text), .Text, 0)

                If RsADD.EOF = False Then
                    xPreSalary = IIf(IsDbNull(RsADD.Fields("BASICSALARY").Value), 0, RsADD.Fields("BASICSALARY").Value)
                    xPrevAmount = IIf(IsDbNull(RsADD.Fields("Amount").Value), 0, RsADD.Fields("Amount").Value)
                    xPrevPer = IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), 0, RsADD.Fields("PERCENTAGE").Value)
                    xEmpDesgCode = IIf(IsDbNull(RsADD.Fields("EMP_DESG_CODE").Value), "", RsADD.Fields("EMP_DESG_CODE").Value)
                End If

                If optBasicSalary(0).Checked = True Then
                    xSalary = xPreSalary
                ElseIf optBasicSalary(0).Checked = True Then
                    xSalary = xPreSalary + (xPreSalary * Val(txtBasicSalary.Text) * 0.01)
                Else
                    xSalary = xPreSalary + Val(txtBasicSalary.Text)
                End If

                If xIncease_Per <> 0 Then
                    xPer = xPrevPer + xIncease_Per
                    xAmount = CDbl(CStr(xPer * Val(CStr(xSalary)) / 100))
                ElseIf Val(CStr(xIncease_Amount)) <> 0 Then
                    xAmount = xPrevAmount + xIncease_Amount
                    xPer = 0
                Else
                    xPer = xPrevPer
                    If xPrevPer <> 0 Then
                        If mType = ConPF Then
                            xAmount = CDbl(CStr(xPer * Val(CStr(BasicPFSalary)) / 100))
                        ElseIf mType = ConESI Then
                            xAmount = CDbl(CStr(xPer * Val(CStr(BasicESISalary)) / 100))
                        Else
                            xAmount = CDbl(CStr(xPer * Val(CStr(xSalary)) / 100))
                        End If
                    Else
                        xAmount = xPrevAmount
                    End If
                End If

                If MainClass.ValidateWithMasterTable(xTypeCode, "Code", "ROUNDING", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mRounding = MasterNo
                End If

                If mRounding = CDbl("0.05") Then
                    xAmount = PaiseRound(xAmount, 0.05)
                Else
                    mRound = Replace(CStr(mRounding), "1", "0")
                    xAmount = CDbl(VB6.Format(xAmount, mRound))
                End If

                If xAmount <> 0 Then
                    SqlStr = " Insert Into PAY_SalaryDef_MST (COMPANY_CODE, FYEAR, " & vbCrLf & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR, EMP_DESG_CODE,ADDDAYS_IN, " & vbCrLf & " ADDUSER, ADDDATE " & vbCrLf & " ) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xPreSalary & "," & vbCrLf & " " & xPrevAmount & ",TO_DATE('" & VB6.Format(xArrearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & xTotArrearMonth & ", '" & xArrearCalc & "','" & xEmpDesgCode & "'," & vbCrLf & " 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " )"

                    PubDBCn.Execute(SqlStr)
                End If
NextDeductRow:
            Next
        End With

        UpdateEmpSalary = True
        Exit Function
UpdateEmpSalaryErr:
        'Resume
        UpdateEmpSalary = False
        MsgInformation(Err.Description)
    End Function


    Public Sub DataChanged()

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub SearchWEF()
        'Dim mProdCode As String
        SqlStr = ""

        '    If MainClass.SearchMaster("", "SalaryDef", "SubKey", "CODE=" & Val(lblcode) & "") = True Then
        '        lblWEF.Caption = AcName
        '        txtWEF.Text = MonthName(Mid(lblWEF, 5, 2)) & ", " & Mid(lblWEF, 1, 4)
        '    End If
        Exit Sub
    End Sub

    Private Sub txtBasicSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBasicSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xWEF As String
        Dim SqlStr As String = ""

        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        txtWEF.Text = "01/" & VB6.Format(txtWEF.Text, "MM/YYYY")


        xWEF = VB6.Format(txtWEF.Text, "MMMYYYY")

        cboMonth.Text = VB6.Format(txtWEF.Text, "MMMM")
        cboYear.Text = VB6.Format(txtWEF.Text, "YYYY")

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Function CheckSalaryMade(ByRef xSalDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String

        CheckSalaryMade = False
        mCheckDate = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))) & "/" & VB6.Format(xSalDate, "MM/YYYY")

        SqlStr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSalaryMade = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
