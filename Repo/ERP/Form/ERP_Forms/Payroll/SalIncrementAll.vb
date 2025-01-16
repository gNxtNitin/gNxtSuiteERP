Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalIncrementAll
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

    Private Const ColEmpCode As Short = 1
    Private Const ColEmpName As Short = 2
    Private Const ColFName As Short = 3
    Private Const ColDOJ As Short = 4
    Private Const ColLastInc As Short = 5
    Private Const ColDept As Short = 6
    Private Const ColDesg As Short = 7
    Private Const ColOldBSalary As Short = 8
    Private Const ColOldPaidBSalary As Short = 9


    Dim ColNewBSalary As Integer
    Dim ColNewPaidBSalary As Integer

    Dim ColOldGrossSalary As Integer
    Dim ColOldPaidGrossSalary As Integer

    Dim ColNewGrossSalary As Integer
    Dim ColNewPaidGrossSalary As Integer

    Dim ColUpdated As Integer
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mPFRate As Double
    Dim mPFEPFRate As Double
    Dim mPFPensionRate As Double
    Dim mPFCeiling As Double
    Dim mESICeiling As Double
    Dim mESIRate As Double
    Dim mEmplerPFCont As String
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

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub


    Private Sub chkContractor_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkContractor.CheckStateChanged
        If chkContractor.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboConName.Enabled = False
        Else
            cboConName.Enabled = True
        End If
    End Sub


    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptName As String
        Dim mDOJ As String
        Dim cntRow As Integer
        Dim mEmpCode As String
        Dim mDeptCode As String

        Dim mDesgCode As String
        Dim mDesgName As String
        Dim mLastIncrement As String

        If Trim(txtWEF.Text) = "" Then
            MsgBox("Please Enter W.E.F. Date")
            txtWEF.Focus()
            Exit Sub
        End If

        If Not IsDate(txtWEF.Text) Then
            MsgBox("Invalid W.E.F. Date")
            txtWEF.Focus()
            Exit Sub
        End If
        MainClass.ClearGrid(sprdMain)

        '        mDOJ = MainClass.LastDay(Month(txtWEF.Text), Year(txtWEF.Text)) & "/" & vb6.Format(txtWEF.Text, "MM/YYYY")

        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, EMP.EMP_DEPT_CODE, " & vbCrLf & " EMP.EMP_FNAME, EMP.EMP_DESG_CODE, EMP.SHIFT_CODE, EMP.EMP_CATG, 'EMPLOYEE' AS CON_NAME,EMP.EMP_DOJ " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""


        SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_LEAVE_DATE IS NULL "

        ''& " AND EMP.EMP_DOJ >=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "') " & vbCrLf _
        '
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(cboDept.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptName = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptName)) & "' "
            End If
        End If

        '        If chkContractor.Value = vbUnchecked And Trim(cboConName.Text) <> "" Then
        '            SqlStr = SqlStr & vbCrLf & " AND CMST.CON_NAME='" & MainClass.AllowSingleQuote(Trim(cboConName.Text)) & "' "
        '        End If

        '        If chkCategory.Value = vbUnchecked And Trim(cboCategory.Text) <> "" Then
        'SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CATG ='" & VB.Left(cboCategory.Text, 1) & "' "
        '        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP.EMP_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        With sprdMain
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    .Row = cntRow
                    .Col = ColEmpCode
                    mEmpCode = IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                    .Text = mEmpCode

                    .Col = ColEmpName
                    .Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)

                    .Col = ColFName
                    .Text = IIf(IsDbNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value)

                    '.Col = ColContractorName
                    '.Text = IIf(IsDbNull(RsTemp.Fields("CON_NAME").Value), "", RsTemp.Fields("CON_NAME").Value)

                    .Col = ColDOJ
                    mDOJ = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")

                    .Col = ColLastInc
                    mLastIncrement = GetEmpLastIncrement(RsCompany.Fields("COMPANY_CODE").Value, mEmpCode, "SALARY_EFF_DATE", txtWEF.Text)
                    mLastIncrement = IIf(mLastIncrement = "", mDOJ, mLastIncrement)
                    .Text = mLastIncrement

                    mDeptCode = IIf(IsDbNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDeptName = MasterNo
                    Else
                        mDeptName = ""
                    End If

                    .Col = ColDept
                    .Text = mDeptName

                    mDesgCode = IIf(IsDbNull(RsTemp.Fields("EMP_DESG_CODE").Value), "", RsTemp.Fields("EMP_DESG_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mDesgCode, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDesgName = MasterNo
                    Else
                        mDesgName = ""
                    End If

                    .Col = ColDesg
                    .Text = mDesgName
                    If FillOldSalaryPart(mEmpCode, mLastIncrement, cntRow) = False Then GoTo ErrPart
                    If FillNewSalaryPart(mEmpCode, (txtWEF.Text), cntRow) = False Then GoTo ErrPart
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop
            End If
        End With
        cmdSave.Enabled = True
        Call FormatSprd(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        Dim cntRow As Integer

        With sprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColOldBSalary
                If Val(.Text) = Val(txtOldSalary.Text) Then
                    .Col = ColNewBSalary
                    .Text = VB6.Format(txtNewSalary.Text)

                    .Col = ColUpdated
                    .Value = CStr(System.Windows.Forms.CheckState.Checked)
                End If
            Next
        End With
        CalcTots()
    End Sub

    Private Sub frmSalIncrementAll_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmSalIncrementAll_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        FraMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(sprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub optContCeiling_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContCeiling.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub cboMonth_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMonth.SelectedIndexChanged
        '    MainClass.SaveStatus Me, ADDMode, MODIFYMode
        '    If Trim(TxtName.Text) = "" Then Exit Sub
        '
        '    If ADDMode = True Then Exit Sub

    End Sub
    Private Sub frmSalIncrementAll_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        SqlStr = "Select * From PAY_SALARYDEF_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        cboMonth.Text = MonthName(Month(RunDate))
        cboYear.Text = CStr(Year(RunDate))
        cboAppMon.Text = MonthName(Month(RunDate))
        cboAppYear.Text = CStr(Year(RunDate))
        FormActive = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Resume
    End Sub
    Private Sub frmSalIncrementAll_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = 0
        Me.Top = 0

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Call FillComboMst()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkContractor.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        cboCategory.Enabled = True
        cboConName.Enabled = False

        FormatSprd(-1)

        FillMonthYearCombo()
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
    Private Sub frmSalIncrementAll_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsEmp = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Function GetPreviousSalary(ByRef xCode As String, ByRef xWEF As String, ByRef xSalHeadCode As Integer, ByRef xPerviousPer As Double) As Double

        On Error GoTo ErrPart
        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        xPerviousPer = 0
        SqlStr = " SELECT BASICSALARY, AMOUNT, PERCENTAGE FROM PAY_SALARYDEF_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'"

        If xSalHeadCode <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCTCODE=" & xSalHeadCode & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            If xSalHeadCode = -1 Then
                GetPreviousSalary = IIf(IsDbNull(RsADD.Fields("BASICSALARY").Value), 0, RsADD.Fields("BASICSALARY").Value)
            Else
                GetPreviousSalary = IIf(IsDbNull(RsADD.Fields("Amount").Value), 0, RsADD.Fields("Amount").Value)
                xPerviousPer = IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), 0, RsADD.Fields("PERCENTAGE").Value)
            End If
        End If

        Exit Function
ErrPart:
        GetPreviousSalary = 0
    End Function
    Private Function GetPreviousPaidSalary(ByRef xCode As String, ByRef xWEF As String, ByRef xSalHeadCode As Integer, ByRef xPerviousPer As Double) As Double

        On Error GoTo ErrPart
        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        xPerviousPer = 0
        SqlStr = " SELECT FORM1_BASICSALARY BASICSALARY, FORM1_AMOUNT AMOUNT, PERCENTAGE FROM PAY_SALARYDEF_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'"

        If xSalHeadCode <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCTCODE=" & xSalHeadCode & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            If xSalHeadCode = -1 Then
                GetPreviousPaidSalary = IIf(IsDBNull(RsADD.Fields("BASICSALARY").Value), 0, RsADD.Fields("BASICSALARY").Value)
            Else
                GetPreviousPaidSalary = IIf(IsDBNull(RsADD.Fields("Amount").Value), 0, RsADD.Fields("Amount").Value)
                'xPerviousPer = IIf(IsDBNull(RsADD.Fields("PERCENTAGE").Value), 0, RsADD.Fields("PERCENTAGE").Value)
            End If
        End If

        Exit Function
ErrPart:
        GetPreviousPaidSalary = 0
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Call CheckPFRates(CDate(VB6.Format(txtWEF.Text, "dd/mm/yyyy")))

        If Update1 = True Then
            cmdSave.Enabled = False
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckPFRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = ""
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & ConPF & "" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConPF & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mPFCeiling = IIf(IsDbNull(RsCeiling.Fields("CEILING").Value), 0, RsCeiling.Fields("CEILING").Value)
            mPFRate = IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
            mPFEPFRate = IIf(IsDbNull(RsCeiling.Fields("EPF").Value), 0, RsCeiling.Fields("EPF").Value)
            mPFPensionRate = IIf(IsDbNull(RsCeiling.Fields("PFUND").Value), 0, RsCeiling.Fields("PFUND").Value)
            mEmplerPFCont = IIf(IsDbNull(RsCeiling.Fields("EMPER_CONT").Value), "B", RsCeiling.Fields("EMPER_CONT").Value)
        Else
            mPFCeiling = 6500
            mPFRate = 12
            mPFEPFRate = 3.67
            mPFPensionRate = 8.33
            mEmplerPFCont = "B"
        End If

        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & ConESI & "" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mESICeiling = IIf(IsDbNull(RsCeiling.Fields("CEILING").Value), 0, RsCeiling.Fields("CEILING").Value)
            mESIRate = IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
        Else
            mESICeiling = 1000
            mESIRate = 1.75
        End If

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mEmpCode As String
        Dim mEmpDesgCode As String

        Dim mArrearCalc As String
        Dim mAppDate As Date
        Dim mWef As Date
        Dim mDOJ As Date
        Dim mArrearDate As Date
        Dim mArrMon As Integer
        Dim mLastInc As String
        Dim cntRow As Integer
        Dim mNewBasicSalary As Double
        Dim mPreBSalary As Double
        Dim mNewPaidBasicSalary As Double
        Dim mPrePaidBSalary As Double
        SqlStr = ""
        PubDBCn.BeginTrans()

        With sprdMain
            For cntRow = 1 To .MaxRows
                '            If CntRow = 300 Then
                '                MsgBox "OK"
                '                MsgBox "OK"
                '            End If
                .Row = cntRow
                .Col = ColUpdated
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    .Col = ColEmpCode
                    mEmpCode = Trim(.Text)

                    .Col = ColLastInc
                    mLastInc = Trim(.Text)

                    .Col = ColDesg
                    If MainClass.ValidateWithMasterTable(.Text, "DESG_DESC", "DESG_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mEmpDesgCode = MasterNo
                    End If

                    .Col = ColOldBSalary
                    mPreBSalary = Val(.Text)

                    .Col = ColOldPaidBSalary
                    mPrePaidBSalary = Val(.Text)

                    .Col = ColNewBSalary
                    mNewBasicSalary = Val(.Text)

                    .Col = ColNewPaidBSalary
                    mNewPaidBasicSalary = Val(.Text)

                    mAppDate = CDate("01/" & MonthValue((cboAppMon.Text)) & "/" & Val(cboAppYear.Text))
                    mWef = CDate("01/" & VB6.Format(txtWEF.Text, "MM/YYYY"))
                    If CDate(mWef) < CDate(mDOJ) Then
                        mWef = CDate(mDOJ)
                    End If

                    mArrearDate = CDate("01/" & MonthValue((cboArrearMonth.Text)) & "/" & Val(cboArrearYear.Text))

                    mArrMon = DateDiff(Microsoft.VisualBasic.DateInterval.Month, mWef, mAppDate)

                    If mArrMon > 0 Then
                        mArrearCalc = "Y"
                    Else
                        mArrearCalc = "N"
                    End If

                    If CDate(mWef) <> CDate(mLastInc) Then
                        If UpdateSalaryDef(mEmpCode, CStr(mWef), mNewBasicSalary, mPreBSalary, mNewPaidBasicSalary, mPrePaidBSalary, CStr(mAppDate),
                                           CStr(mArrearDate), mArrMon, mArrearCalc, mEmpDesgCode, cntRow) = False Then GoTo UpdateError
                    End If
                End If
            Next
        End With


        PubDBCn.CommitTrans()
        '    If mArrMon + Val(txtAddDays.Text) > 0 Then
        '        MsgInformation mArrMon & " Month " & Val(txtAddDays.Text) & " Days Arrear Also Calculated."
        '    End If

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
        Dim mWef As Date
        Dim mArrearDate As Date
        Dim mESIAmount As Double
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

        If Trim(txtWEF.Text) = "" Then
            MsgInformation("WEF can not be blank.")
            txtWEF.Focus()
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
        mWef = CDate("01/" & VB6.Format(txtWEF.Text, "MM/YYYY"))
        mArrearDate = CDate("01/" & MonthValue((cboArrearMonth.Text)) & "/" & Val(cboArrearYear.Text))

        If mWef > mAppDate Then
            MsgInformation("Applicable Date Cann't be Less Than WEF Date.")
            cboAppMon.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If mWef = mAppDate Then
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

        '    If PubSuperUser = "U" Then
        '        If CheckSalaryMade(txtEmpNo.Text, Format(lblAppDate.Caption, "DD/MM/YYYY")) = True Then
        '            MsgInformation "Salary Made Againt This Month. So Cann't be Modified"
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        '

        '    mESICeiling = CheckESICeiling(mWEF)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Sub FillComboMst()

        Dim RsDept As ADODB.Recordset = Nothing
        cboDept.Items.Clear()


        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST WHERE COMPANy_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboDept.SelectedIndex = 0

        'SqlStr = "Select CON_NAME FROM PAY_CONTRACTOR_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' Order by CON_NAME"
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        'cboConName.Items.Clear()
        'If RsDept.EOF = False Then
        '    Do While Not RsDept.EOF
        '        cboConName.Items.Add(RsDept.Fields("CON_NAME").Value)
        '        RsDept.MoveNext()
        '    Loop
        'End If
        'cboConName.SelectedIndex = 0

        cboCategory.Items.Clear()
        ''AND CATEGORY_TYPE='W'

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCategory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCategory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCategory.SelectedIndex = 0

        ''    cboCategory.AddItem "General Staff"
        ''    cboCategory.AddItem "Production Staff"
        ''    cboCategory.AddItem "Export Staff"
        '    cboCategory.AddItem "Regular Worker"
        '    cboCategory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Director"
        ''    cboCategory.AddItem "Trainee Staff"
        '    cboCategory.ListIndex = 0


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillSalarySprd()

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mCurrentCol As Integer

        '    MainClass.ClearGrid sprdMain, -1


        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND ADDDEDUCT=" & ConEarning & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)
        mCurrentCol = ColOldPaidBSalary + 1
        '    sprdMain.MaxCols = mCurrentCol

        If RsADD.EOF = False Then
            With sprdMain
                Do While Not RsADD.EOF
                    .Row = 0
                    .Col = mCurrentCol
                    .Text = "OLD " & RsADD.Fields("Name").Value
                    mCurrentCol = mCurrentCol + 1

                    .Col = mCurrentCol
                    .Text = "OLD PAID " & RsADD.Fields("Name").Value
                    mCurrentCol = mCurrentCol + 1
                    .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
                    '                .MaxCols = mCurrentCol
                    RsADD.MoveNext()
                Loop
            End With
        End If

        ColOldGrossSalary = mCurrentCol
        sprdMain.Row = 0
        sprdMain.Col = ColOldGrossSalary
        sprdMain.Text = "OLD " & "GROSS SALARY"
        mCurrentCol = mCurrentCol + 1

        ColOldPaidGrossSalary = mCurrentCol
        SprdMain.Col = ColOldPaidGrossSalary
        SprdMain.Text = "OLD PAID " & "GROSS SALARY"
        SprdMain.ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
        mCurrentCol = mCurrentCol + 1

        '    sprdMain.MaxCols = mCurrentCol

        ColNewBSalary = mCurrentCol
        sprdMain.Row = 0
        sprdMain.Col = ColNewBSalary
        sprdMain.Text = "NEW " & "BASIC SALARY"
        mCurrentCol = mCurrentCol + 1

        ColNewPaidBSalary = mCurrentCol
        SprdMain.Col = ColNewPaidBSalary
        SprdMain.ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
        SprdMain.Text = "NEW PAID " & "BASIC SALARY"

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND ADDDEDUCT=" & ConEarning & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)
        mCurrentCol = mCurrentCol + 1
        '    sprdMain.MaxCols = mCurrentCol
        If RsADD.EOF = False Then
            With sprdMain
                Do While Not RsADD.EOF
                    .Row = 0
                    .Col = mCurrentCol
                    .Text = "NEW " & RsADD.Fields("Name").Value
                    mCurrentCol = mCurrentCol + 1

                    .Col = mCurrentCol
                    .Text = "NEW PAID " & RsADD.Fields("Name").Value
                    .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
                    mCurrentCol = mCurrentCol + 1

                    '                .MaxCols = mCurrentCol
                    RsADD.MoveNext()
                Loop
            End With
        End If

        ColNewGrossSalary = mCurrentCol
        sprdMain.Row = 0
        sprdMain.Col = ColNewGrossSalary
        SprdMain.Text = "NEW " & "GROSS SALARY"
        mCurrentCol = mCurrentCol + 1

        ColNewPaidGrossSalary = mCurrentCol
        SprdMain.Col = ColNewPaidGrossSalary
        SprdMain.ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
        SprdMain.Text = "NEW PAID " & "GROSS SALARY"

        mCurrentCol = mCurrentCol + 1
        '    sprdMain.MaxCols = mCurrentCol

        ColUpdated = mCurrentCol
        sprdMain.Row = 0
        sprdMain.Col = ColUpdated
        sprdMain.Text = "Update"
    End Sub
    Private Function GetMaxCols() As Double

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND ADDDEDUCT=" & ConEarning & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)
        If RsADD.EOF = False Then
            With sprdMain
                Do While Not RsADD.EOF
                    GetMaxCols = GetMaxCols + 4
                    RsADD.MoveNext()
                Loop
            End With
        End If

        GetMaxCols = ColOldPaidBSalary + GetMaxCols + 7
    End Function

    Private Function FillOldSalaryPart(ByRef mEmpCode As Object, ByRef mLastIncrement As Object, ByRef cntRow As Object) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mBasicSalary As Double
        Dim mSalAmount As Double
        Dim mSalHeadName As String
        Dim mCheckSalHeadName As String
        Dim cntCol As Integer
        Dim mGrossAmount As Double
        Dim mPaidBasicSalary As Double
        Dim mPaidGrossAmount As Double

        SqlStr = "SELECT A.BASICSALARY, A.AMOUNT, FORM1_BASICSALARY, FORM1_AMOUNT," & vbCrLf _
            & " B.NAME" & vbCrLf _
            & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.EMP_CODE='" & mEmpCode & "'" & vbCrLf _
            & " AND A.SALARY_EFF_DATE=TO_DATE('" & VB6.Format(mLastIncrement, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mGrossAmount = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mBasicSalary = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value), "0.00"))
                mSalHeadName = UCase(Trim("OLD " & IIf(IsDbNull(RsTemp.Fields("Name").Value), 0, RsTemp.Fields("Name").Value)))
                mSalAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00"))
                For cntCol = ColOldPaidBSalary + 1 To ColOldGrossSalary - 1
                    SprdMain.Row = 0
                    SprdMain.Col = cntCol
                    mCheckSalHeadName = UCase(Trim(SprdMain.Text))
                    If mSalHeadName = mCheckSalHeadName Then
                        SprdMain.Row = cntRow
                        SprdMain.Col = cntCol
                        SprdMain.Text = CStr(mSalAmount)
                        mGrossAmount = mGrossAmount + mSalAmount
                        Exit For
                    End If
                Next

                mPaidBasicSalary = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("FORM1_BASICSALARY").Value), 0, RsTemp.Fields("FORM1_BASICSALARY").Value), "0.00"))
                mSalHeadName = UCase(Trim("OLD PAID " & IIf(IsDBNull(RsTemp.Fields("Name").Value), 0, RsTemp.Fields("Name").Value)))
                mSalAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("FORM1_AMOUNT").Value), 0, RsTemp.Fields("FORM1_AMOUNT").Value), "0.00"))
                For cntCol = ColOldPaidBSalary + 1 To ColOldGrossSalary - 1
                    SprdMain.Row = 0
                    SprdMain.Col = cntCol
                    mCheckSalHeadName = UCase(Trim(SprdMain.Text))
                    If mSalHeadName = mCheckSalHeadName Then
                        SprdMain.Row = cntRow
                        SprdMain.Col = cntCol
                        SprdMain.Text = CStr(mSalAmount)
                        mPaidGrossAmount = mPaidGrossAmount + mSalAmount
                        Exit For
                    End If
                Next


                RsTemp.MoveNext()
            Loop
        End If

        sprdMain.Row = cntRow
        sprdMain.Col = ColOldBSalary
        sprdMain.Text = CStr(mBasicSalary)

        SprdMain.Col = ColOldPaidBSalary
        SprdMain.Text = CStr(mPaidBasicSalary)

        mGrossAmount = mGrossAmount + mBasicSalary
        mPaidGrossAmount = mPaidGrossAmount + mPaidBasicSalary

        SprdMain.Row = cntRow
        sprdMain.Col = ColOldGrossSalary
        sprdMain.Text = VB6.Format(mGrossAmount, "0.00")

        SprdMain.Col = ColOldPaidGrossSalary
        SprdMain.Text = VB6.Format(mPaidGrossAmount, "0.00")

        FillOldSalaryPart = True
        Exit Function
ErrPart:
        FillOldSalaryPart = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function FillNewSalaryPart(ByRef mEmpCode As Object, ByRef mLastIncrement As Object, ByRef cntRow As Object) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mBasicSalary As Double
        Dim mSalAmount As Double
        Dim mSalHeadName As String
        Dim mCheckSalHeadName As String
        Dim cntCol As Integer
        Dim mGrossAmount As Double
        Dim mPaidBasicSalary As Double
        Dim mPaidGrossAmount As Double

        SqlStr = "SELECT A.BASICSALARY, A.AMOUNT, FORM1_BASICSALARY, FORM1_AMOUNT, " & vbCrLf _
            & " B.NAME" & vbCrLf _
            & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.EMP_CODE='" & mEmpCode & "'" & vbCrLf _
            & " AND A.SALARY_EFF_DATE=TO_DATE('" & VB6.Format(mLastIncrement, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mGrossAmount = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mBasicSalary = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value), "0.00"))
                mSalHeadName = UCase(Trim("NEW " & IIf(IsDbNull(RsTemp.Fields("Name").Value), 0, RsTemp.Fields("Name").Value)))
                mSalAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00"))
                For cntCol = ColNewBSalary + 1 To ColNewGrossSalary - 1
                    sprdMain.Row = 0
                    sprdMain.Col = cntCol
                    mCheckSalHeadName = UCase(Trim(sprdMain.Text))
                    If mSalHeadName = mCheckSalHeadName Then
                        sprdMain.Row = cntRow
                        sprdMain.Col = cntCol
                        sprdMain.Text = CStr(mSalAmount)
                        mGrossAmount = mGrossAmount + mSalAmount
                        Exit For
                    End If
                Next

                mPaidBasicSalary = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("FORM1_BASICSALARY").Value), 0, RsTemp.Fields("FORM1_BASICSALARY").Value), "0.00"))
                mSalHeadName = UCase(Trim("NEW PAID " & IIf(IsDBNull(RsTemp.Fields("Name").Value), 0, RsTemp.Fields("Name").Value)))
                mSalAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("FORM1_AMOUNT").Value), 0, RsTemp.Fields("FORM1_AMOUNT").Value), "0.00"))
                For cntCol = ColNewBSalary + 1 To ColNewGrossSalary - 1
                    SprdMain.Row = 0
                    SprdMain.Col = cntCol
                    mCheckSalHeadName = UCase(Trim(SprdMain.Text))
                    If mSalHeadName = mCheckSalHeadName Then
                        SprdMain.Row = cntRow
                        SprdMain.Col = cntCol
                        SprdMain.Text = CStr(mSalAmount)
                        mPaidGrossAmount = mPaidGrossAmount + mSalAmount
                        Exit For
                    End If
                Next

                RsTemp.MoveNext()
            Loop
        End If

        sprdMain.Row = cntRow
        sprdMain.Col = ColNewBSalary
        sprdMain.Text = CStr(mBasicSalary)

        SprdMain.Col = ColNewPaidBSalary
        SprdMain.Text = CStr(mPaidBasicSalary)

        mGrossAmount = mGrossAmount + mBasicSalary
        mPaidGrossAmount = mPaidGrossAmount + mPaidBasicSalary

        SprdMain.Row = cntRow
        sprdMain.Col = ColNewGrossSalary
        sprdMain.Text = VB6.Format(mGrossAmount, "0.00")

        SprdMain.Col = ColNewPaidGrossSalary
        SprdMain.Text = VB6.Format(mPaidGrossAmount, "0.00")

        FillNewSalaryPart = True
        Exit Function
ErrPart:
        FillNewSalaryPart = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight * 1.5)
            .MaxCols = GetMaxCols()
            Call FillSalarySprd()
            .Row = mRow
            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColEmpCode, 8)

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColEmpName, 17)

            .ColsFrozen = ColEmpName

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColFName, 17)

            '.Col = ColContractorName
            '.CellType = SS_CELL_TYPE_EDIT
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '.set_ColWidth(ColContractorName, 17)

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDOJ, 10)

            .Col = ColLastInc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColLastInc, 10)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 10)

            .Col = ColDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesg, 10)


            For cntCol = ColOldBSalary To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColOldPaidBSalary
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)

            .Col = ColUpdated
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColUpdated, 8)
            .Value = CStr(ColUpdated)
        End With

        MainClass.SetSpreadColor(sprdMain, -1)
        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColEmpCode, ColOldGrossSalary)
        MainClass.ProtectCell(sprdMain, 1, sprdMain.MaxRows, ColNewGrossSalary, ColNewGrossSalary)

        sprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
        sprdMain.DAutoCellTypes = True
        sprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        sprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Function UpdateSalaryDef(ByRef xCode As String, ByRef xWEF As String,
                                     ByRef xSalary As Double, ByRef xPreSalary As Double,
                                      ByRef xPaidSalary As Double, ByRef xPrePaidSalary As Double,
                                     ByRef xAppDate As String, ByRef xArrearDate As String, ByRef xTotArrearMonth As Integer, ByRef xArrearCalc As String, ByRef xEmpDesgCode As String, ByRef mRow As Integer) As Boolean


        On Error GoTo UpdateSalaryDefErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xTypeCode As Integer
        Dim cntCol As Integer
        Dim xAmount As Double
        Dim xPer As Double
        Dim xPrevAmt As Double
        Dim EmpPFCont As String
        Dim mCheckSalHeadName As String
        Dim mSalHeadName As String
        Dim xPayableESIAmt As Double
        Dim mNextIncDueDate As String
        Dim xPreviousPer As Double
        Dim xWelfareSalaryAmt As Double
        Dim xPaidAmount As Double
        Dim xPrevPaidAmt As Double

        If Trim(xCode) = "" Then
            UpdateSalaryDef = True
            Exit Function
        End If

        mNextIncDueDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, CDate(xWEF)))

        xPreSalary = GetPreviousSalary(xCode, xWEF, -1, 0)
        xPayableESIAmt = 0
        xWelfareSalaryAmt = 0

        SqlStr = " DELETE FROM PAY_SALARYDEF_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND TO_CHAR(SALARY_EFF_DATE,'MONYYYY')='" & UCase(VB6.Format(xWEF, "MMMYYYY")) & "'"


        PubDBCn.Execute(SqlStr)

        ''
        EmpPFCont = IIf(optContBasic.Checked = True, "B", "C")

        If MainClass.ValidateWithMasterTable(xCode, "EMP_CODE", "EMP_CONT", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            EmpPFCont = MasterNo
        End If

        SqlStr = ""
        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND ADDDEDUCT=" & ConEarning & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xTypeCode = IIf(IsDBNull(RsTemp.Fields("Code").Value), -1, RsTemp.Fields("Code").Value)
                mSalHeadName = UCase(Trim("NEW " & IIf(IsDBNull(RsTemp.Fields("Name").Value), "", RsTemp.Fields("Name").Value)))
                xAmount = CDbl("0.00")
                xPer = CDbl("0.00")

                For cntCol = ColNewBSalary + 1 To ColNewGrossSalary - 1
                    SprdMain.Row = 0
                    SprdMain.Col = cntCol
                    mCheckSalHeadName = UCase(Trim(SprdMain.Text))
                    If mCheckSalHeadName = mSalHeadName Then
                        SprdMain.Row = mRow
                        SprdMain.Col = cntCol
                        xAmount = Val(SprdMain.Text)

                        xPer = CDbl("0.00")
                        If RsTemp.Fields("INCLUDEDESI").Value = "Y" Then
                            xPayableESIAmt = xPayableESIAmt + xAmount
                        End If
                        xWelfareSalaryAmt = xWelfareSalaryAmt + xAmount
                        Exit For
                    End If
                Next

                mSalHeadName = UCase(Trim("NEW PAID " & IIf(IsDBNull(RsTemp.Fields("Name").Value), "", RsTemp.Fields("Name").Value)))
                xPaidAmount = CDbl("0.00")

                For cntCol = ColNewBSalary + 1 To ColNewGrossSalary - 1
                    SprdMain.Row = 0
                    SprdMain.Col = cntCol
                    mCheckSalHeadName = UCase(Trim(SprdMain.Text))
                    If mCheckSalHeadName = mSalHeadName Then
                        SprdMain.Row = mRow
                        SprdMain.Col = cntCol
                        xPaidAmount = Val(SprdMain.Text)

                        'xPer = CDbl("0.00")
                        'If RsTemp.Fields("INCLUDEDESI").Value = "Y" Then
                        '    xPayableESIAmt = xPayableESIAmt + xAmount
                        'End If
                        'xWelfareSalaryAmt = xWelfareSalaryAmt + xAmount
                        Exit For
                    End If
                Next

                xPrevAmt = GetPreviousSalary(xCode, xWEF, xTypeCode, 0)
                xPrevPaidAmt = GetPreviousPaidSalary(xCode, xWEF, xTypeCode, 0)

                '',FORM1_BASICSALARY, FORM1_AMOUNT

                SqlStr = " Insert Into PAY_SALARYDEF_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                    & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf _
                    & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf _
                    & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf _
                    & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE,ADDDAYS_IN, " & vbCrLf _
                    & " EMP_CONT, ADDUSER, ADDDATE,NEXT_INC_DATE, " & vbCrLf _
                    & " FORM1_BASICSALARY, FORM1_AMOUNT, PREVIOUS_FORM1_BASICSALARY, PREVIOUS_FORM1_AMOUNT " & vbCrLf & " ) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xPreSalary & "," & vbCrLf _
                    & " " & xPrevAmt & ",TO_DATE('" & VB6.Format(xArrearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xTotArrearMonth & ", '" & xArrearCalc & "','" & xEmpDesgCode & "'," & vbCrLf _
                    & " 0, '" & EmpPFCont & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mNextIncDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & xPaidSalary & ", " & xPaidAmount & ",  " & xPrePaidSalary & "," & xPrevPaidAmt & ")"

                PubDBCn.Execute(SqlStr)

                RsTemp.MoveNext()
            Loop
        End If



        ''Deduction.........

        SqlStr = ""
        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And (CALC_ON=" & ConCalcBSalary & " Or CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " And ADDDEDUCT=" & ConDeduct & " And TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xTypeCode = IIf(IsDBNull(RsTemp.Fields("Code").Value), -1, RsTemp.Fields("Code").Value)

                If RsTemp.Fields("Type").Value = ConPF Then
                    xPrevAmt = GetPreviousSalary(xCode, xWEF, xTypeCode, 0)
                    If xPrevAmt = 0 Then
                        xAmount = 0
                        xPer = 0
                    Else
                        xAmount = xSalary * mPFRate / 100
                        xPer = mPFRate
                    End If

                ElseIf RsTemp.Fields("Type").Value = ConESI Then
                    xPrevAmt = GetPreviousSalary(xCode, xWEF, xTypeCode, 0)
                    If xPrevAmt = 0 Then
                        xAmount = 0
                        xPer = 0
                    Else
                        If mESICeiling >= (xPayableESIAmt + xSalary) Then
                            xAmount = (xPayableESIAmt + xSalary) * mESIRate / 100
                            xPer = mESIRate
                        Else
                            xAmount = 0
                            xPer = 0
                        End If
                    End If

                ElseIf RsTemp.Fields("Type").Value = ConWelfare Then
                    xWelfareSalaryAmt = xWelfareSalaryAmt + xSalary
                    xAmount = GetWelfareAmount(xWEF, xWelfareSalaryAmt)
                    xPer = 0
                Else
                    xAmount = CDbl("0.00")
                    xPer = CDbl("0.00")
                End If

                xPrevAmt = GetPreviousSalary(xCode, xWEF, xTypeCode, 0)

                SqlStr = " Insert Into PAY_SALARYDEF_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                    & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf _
                    & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf _
                    & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf _
                    & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE,ADDDAYS_IN, " & vbCrLf _
                    & " EMP_CONT, ADDUSER, ADDDATE ,NEXT_INC_DATE, " & vbCrLf _
                    & "  FORM1_BASICSALARY, FORM1_AMOUNT, PREVIOUS_FORM1_BASICSALARY, PREVIOUS_FORM1_AMOUNT" & vbCrLf _
                    & " ) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xPreSalary & "," & vbCrLf _
                    & " " & xPrevAmt & ",TO_DATE('" & VB6.Format(xArrearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xTotArrearMonth & ", '" & xArrearCalc & "','" & xEmpDesgCode & "'," & vbCrLf _
                    & " 0, '" & EmpPFCont & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mNextIncDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & xPaidSalary & ", " & xAmount & ", " & xPrePaidSalary & ", " & xPrevAmt & " )"

                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If



        ''Perks.........

        SqlStr = ""
        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And (CALC_ON=" & ConCalcBSalary & " Or CALC_ON =" & ConCalcFixed & ") " & vbCrLf & " And ADDDEDUCT=" & ConPerks & " And TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xPreviousPer = 0
                xTypeCode = IIf(IsDBNull(RsTemp.Fields("Code").Value), -1, RsTemp.Fields("Code").Value)
                xPrevAmt = GetPreviousSalary(xCode, xWEF, xTypeCode, xPreviousPer)
                xPer = xPreviousPer
                xAmount = CDbl(VB6.Format(xSalary * xPer / 100, "0.00"))

                SqlStr = " Insert Into PAY_SALARYDEF_MST (COMPANY_CODE, FYEAR, " & vbCrLf & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE,ADDDAYS_IN, " & vbCrLf & " EMP_CONT, ADDUSER, ADDDATE,NEXT_INC_DATE " & vbCrLf & " ) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xPreSalary & "," & vbCrLf & " " & xPrevAmt & ",TO_DATE('" & VB6.Format(xArrearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & xTotArrearMonth & ", '" & xArrearCalc & "','" & xEmpDesgCode & "'," & vbCrLf & " 0, '" & EmpPFCont & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " TO_DATE('" & VB6.Format(mNextIncDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                PubDBCn.Execute(SqlStr)

                RsTemp.MoveNext()
            Loop
        End If

        UpdateSalaryDef = True
        Exit Function
UpdateSalaryDefErr:
        'Resume
        UpdateSalaryDef = False
        MsgInformation(Err.Description)
    End Function


    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        sprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        sprdMain.DAutoCellTypes = True
        sprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        sprdMain.TypeEditLen = 1000
    End Sub


    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ERR1
        Dim mGrossAmount As Double
        Dim cntCol As Integer

        If eventArgs.NewRow = -1 Then Exit Sub

        With sprdMain
            '        If Col >= ColNewBSalary And Col < ColNewGrossSalary Then
            For cntCol = ColNewBSalary To ColNewGrossSalary - 1
                .Row = eventArgs.Row
                .Col = cntCol
                mGrossAmount = mGrossAmount + Val(.Text)
            Next

            .Row = eventArgs.Row
            .Col = ColNewGrossSalary
            .Text = VB6.Format(mGrossAmount, "0.00")
            '        End If
        End With


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mGrossAmount As Double
        Dim cntCol As Integer
        Dim cntRow As Integer

        With sprdMain
            For cntRow = 1 To .MaxRows
                mGrossAmount = 0
                For cntCol = ColNewBSalary To ColNewPaidGrossSalary - 1
                    .Row = cntRow
                    .Col = cntCol
                    mGrossAmount = mGrossAmount + Val(.Text)
                Next

                .Row = cntRow
                .Col = ColNewGrossSalary
                .Text = VB6.Format(mGrossAmount, "0.00")
            Next
        End With


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtNewSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNewSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOldSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOldSalary.KeyPress
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

        '    If Trim(txtEmpNo.Text) = "" Then
        '        MsgInformation "Employee Name Blank."
        '        txtEmpNo.SetFocus
        '        Exit Sub
        '    End If
        '
        '    If Not IsDate(txtWEF.Text) Then
        '        MsgInformation "Invalid Date."
        '        Cancel = True
        '        Exit Sub
        '    End If
        '
        '    txtWEF.Text = "01/" & vb6.Format(txtWEF.Text, "MM/YYYY")
        '
        '
        '    xWEF = Format(txtWEF.Text, "MMMYYYY")
        '
        '    cboMonth.Text = Format(txtWEF.Text, "MMMM")
        '    cboYear.Text = Format(txtWEF.Text, "YYYY")
        '
        '    If MODIFYMode = True And RsEmp.EOF = False Then xCode = RsEmp.Fields("EMP_CODE").Value
        '
        '    SqlStr = " SELECT * FROM PAY_SALARYDEF_MST " & vbCrLf _
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                    & " AND EMP_CODE='" & txtEmpNo.Text & "'" & vbCrLf _
        ''                    & " AND TO_CHAR(SALARY_EFF_DATE,'MONYYYY')='" & UCase(xWEF) & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsEmp, adLockReadOnly
        '
        '    If RsEmp.EOF = False Then
        '        Clear1
        '        Call Show1
        '        If txtWEF.Enabled = True Then txtWEF.SetFocus
        '    Else
        '        If ADDMode = False And MODIFYMode = False Then
        '            MsgBox "No Such Month, Use add Button to Generate New Increment.", vbInformation
        '            Cancel = True
        '            Exit Sub
        '        ElseIf MODIFYMode = True Then
        '            SqlStr = "SELECT * FROM PAY_SALARYDEF_MST " & vbCrLf _
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                    & " AND EMP_CODE='" & xCode & "'"
        '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsEmp, adLockReadOnly
        '            Exit Sub
        '        End If
        '        Call ShowPreviousSalary(txtEmpNo.Text, txtWEF.Text)
        '    End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Function CheckSalaryMade(ByRef xEmpCode As String, ByRef xSalDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String

        CheckSalaryMade = False
        mCheckDate = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))) & "/" & VB6.Format(xSalDate, "MM/YYYY")

        SqlStr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSalaryMade = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdMain, 1, sprdMain.MaxRows, 1, sprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = ""
        mTitle = "New Wages As On " & VB6.Format(txtWEF.Text, "DD-MMMM-YYYY")

        Call ShowReport(SqlStr, "IncrementAll.rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim mEmpName As String
        Dim mEmpDegn As String
        Dim mWef As String
        Dim mNextWEF As String
        Dim mNextWEFStr As String
        Dim mBasic As String
        Dim mGrossAmount As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        If IsDate(txtWEF.Text) Then
            mNextWEFStr = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, CDate(txtWEF.Text)))
        End If

        '    MainClass.AssignCRptFormulas Report1, "mEmpName='" & MainClass.AllowSingleQuote(TxtName.Text) & "'"
        '    MainClass.AssignCRptFormulas Report1, "mEmpDegn='" & MainClass.AllowSingleQuote(lblDesg.Caption) & "'"
        '    MainClass.AssignCRptFormulas Report1, "mWEF='" & MainClass.AllowSingleQuote(txtWEF.Text) & "'"
        '    MainClass.AssignCRptFormulas Report1, "mNextWEF='" & MainClass.AllowSingleQuote(mNextWEFStr) & "'"
        '    MainClass.AssignCRptFormulas Report1, "mBasic='" & txtBSalary.Text & "'"
        '    MainClass.AssignCRptFormulas Report1, "mGrossAmount='" & txtGSalary.Text & "'"

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
