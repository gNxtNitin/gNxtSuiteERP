Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data.OleDb
Imports ADODB

Imports System.Data
Imports System.IO
Imports System.Configuration

Imports System.Drawing.Color
Imports System.ComponentModel

Friend Class FrmAttnProcessFromMachine
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection

    Dim ConWorkDay As Double
    Private Const ConWorkHour As Short = 8
    Dim mCurrentFYNo As Integer

    Private Sub cboCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        '    If chkCategory.Value = vbChecked Then
        '        cboCategory.Enabled = False
        '    Else
        '        cboCategory.Enabled = True
        '    End If
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
            cboContractor.Enabled = False
        Else
            cboContractor.Enabled = True
        End If
    End Sub

    Private Sub chkDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDept.CheckStateChanged
        If chkDept.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkShift_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShift.CheckStateChanged
        If chkShift.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboShift.Enabled = False
        Else
            cboShift.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdOK.Click

        Dim CntDay As Integer
        Dim mDate As String

        Dim xCategoryDesc As String
        Dim xDeptDesc As String
        Dim xShift As String
        Dim xEmpCode As String
        Dim xContractor As String
        Dim xContractorCode As Integer
        Dim mAttnTable As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCount As Double

        If FieldVarification() = False Then Exit Sub

        mDate = MainClass.LastDay(Month(CDate(txtAttnDateFrom.Text)), Year(CDate(txtAttnDateFrom.Text))) & "/" & Month(CDate(txtAttnDateFrom.Text)) & "/" & Year(CDate(txtAttnDateFrom.Text))
        mCurrentFYNo = GetCurrentFYNo(PubDBCn, mDate)


        If mCurrentFYNo = -1 Then
            Exit Sub
        End If



        If optProcess(0).Checked = True Then
            mAttnTable = "PAY_DALIY_ATTN_TRN"
        Else
            mAttnTable = "PAY_CONT_DALIY_ATTN_TRN"
        End If

        SqlStr = " SELECT COUNT(1) AS CNTCOUNT FROM " & mAttnTable & " " & vbCrLf _
            & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND  ATTN_DATE >= TO_DATE('" & VB6.Format(txtAttnDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND  ATTN_DATE <= TO_DATE('" & VB6.Format(txtAttnDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            mCount = IIf(IsDBNull(RsTemp.Fields("CNTCOUNT").Value), 0, RsTemp.Fields("CNTCOUNT").Value)
            If mCount > 0 Then
                If MsgQuestion("Attn Already Process, Are youn want to continue...") = vbNo Then
                    Exit Sub
                End If
            End If
        End If





        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Visible = True

        xCategoryDesc = Trim(IIf(chkCategory.CheckState = System.Windows.Forms.CheckState.Checked, "", cboCategory.Text))
        xDeptDesc = Trim(IIf(chkDept.CheckState = System.Windows.Forms.CheckState.Checked, "", cboDept.Text))
        xShift = Trim(IIf(chkShift.CheckState = System.Windows.Forms.CheckState.Checked, "", cboShift.Text))
        xEmpCode = Trim(IIf(OptParti.Checked = True, TxtCardNo.Text, ""))
        xContractorCode = 0
        If optProcess(1).Checked = True Then
            xContractor = Trim(IIf(chkContractor.CheckState = System.Windows.Forms.CheckState.Checked, "", cboContractor.Text))
            If MainClass.ValidateWithMasterTable(xContractor, "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xContractorCode = MasterNo
            End If
        End If

        For CntDay = VB.Day(CDate(txtAttnDateFrom.Text)) To VB.Day(CDate(txtAttnDateTo.Text))

            mDate = VB6.Format(CntDay & "/" & VB6.Format(txtAttnDateFrom.Text, "MM/YYYY"), "DD/MM/YYYY")

            If optFrom(0).Checked = True Then
                If optProcess(0).Checked = True Then
                    If ValidateShiftEnter(mDate, IIf(optProcess(0).Checked = True, "E", "C"), xCategoryDesc, xDeptDesc, xShift, xEmpCode, xContractorCode) = False Then
                        PBar.Visible = False
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If
                End If
                If AttnProcess(mDate) = False Then
                    MsgBox("Attendance Process Not Complete.")
                    PBar.Visible = False
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If
            ElseIf optFrom(2).Checked = True Then

                If optProcess(0).Checked = True Then
                    If ValidateShiftEnter(mDate, IIf(optProcess(0).Checked = True, "E", "C"), xCategoryDesc, xDeptDesc, xShift, xEmpCode, xContractorCode) = False Then
                        PBar.Visible = False
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If

                    If AttnProcessFromShift(mDate) = False Then
                        MsgBox("Attendance Process Not Complete.")
                        PBar.Visible = False
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If
                End If
            Else
                If optProcess(0).Checked = True Then
                    If ValidateShiftEnter(mDate, IIf(optProcess(0).Checked = True, "E", "C"), xCategoryDesc, xDeptDesc, xShift, xEmpCode, xContractorCode) = False Then
                        PBar.Visible = False
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If

                    '                If AttnProcessMannual(mDate) = False Then
                    '                    MsgBox "Attendance Process Not Complete."
                    '                    PBar.Visible = False
                    '                    Screen.MousePointer = 0
                    '                    Exit Sub
                    '                End If
                Else
                    If AttnProcess(mDate) = False Then
                        MsgBox("Attendance Process Not Complete.")
                        PBar.Visible = False
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Sub
                    End If
                End If
            End If
        Next

        MsgBox("Attendance Process Complete")

        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FieldVarification() As Boolean
        FieldVarification = True

        If OptParti.Checked = True Then
            If TxtCardNo.Text = "" Then
                MsgBox("CardNo. Not Selected.")
                FieldVarification = False
                TxtCardNo.Focus()
                Exit Function
            End If
        End If

        If optProcess(0).Checked = True Then
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(cboCategory.Text) = "" Then
                MsgBox("Please Select Category.")
                FieldVarification = False
                cboCategory.Focus()
                Exit Function
            End If

            If chkDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(cboDept.Text) = "" Then
                MsgBox("Please Select Department.")
                FieldVarification = False
                cboDept.Focus()
                Exit Function
            End If

            If chkShift.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(cboShift.Text) = "" Then
                MsgBox("Please Select Shift.")
                FieldVarification = False
                cboShift.Focus()
                Exit Function
            End If
        Else
            If chkDept.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(cboDept.Text) = "" Then
                MsgBox("Please Select Department.")
                FieldVarification = False
                cboDept.Focus()
                Exit Function
            End If

            If chkContractor.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(cboContractor.Text) = "" Then
                MsgBox("Please Select Contractor.")
                FieldVarification = False
                cboContractor.Focus()
                Exit Function
            End If


        End If

        If Trim(txtAttnDateFrom.Text) = "" Or Trim(txtAttnDateFrom.Text) = "__/__/____" Then
            MsgBox("Please Enter Date.")
            FieldVarification = False
            txtAttnDateFrom.Focus()
            Exit Function
        End If

        If Not IsDate(txtAttnDateFrom.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            FieldVarification = False
            txtAttnDateFrom.Focus()
            Exit Function
        End If


        If Trim(txtAttnDateTo.Text) = "" Or Trim(txtAttnDateTo.Text) = "__/__/____" Then
            MsgBox("Please Enter Date.")
            FieldVarification = False
            txtAttnDateTo.Focus()
            Exit Function
        End If

        If Not IsDate(txtAttnDateTo.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            FieldVarification = False
            txtAttnDateTo.Focus()
            Exit Function
        End If

        If VB6.Format(txtAttnDateFrom.Text, "YYYYMM") <> VB6.Format(txtAttnDateTo.Text, "YYYYMM") Then
            MsgBox("Please Select Same Month.")
            FieldVarification = False
            txtAttnDateFrom.Focus()
            Exit Function
        End If

    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        Dim mTableName As String

        If optProcess(0).Checked = True Then
            mTableName = "PAY_EMPLOYEE_MST"
        Else
            mTableName = "PAY_CONT_EMPLOYEE_MST"
        End If

        If MainClass.SearchGridMaster("", mTableName, "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            TxtCardNo.Text = AcName1
            TxtName.Text = AcName
            TxtCardNo_Validating(TxtCardNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub FrmAttnProcessFromMachine_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsDept As ADODB.Recordset = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Me.Left = VB6.TwipsToPixelsX(25)
        Me.Top = VB6.TwipsToPixelsY(25)
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(5070)
        Me.Width = VB6.TwipsToPixelsX(5475)

        SqlStr = "SELECT CON_NAME FROM PAY_CONTRACTOR_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY CON_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        cboContractor.Items.Clear()

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboContractor.Items.Add(RsDept.Fields("CON_NAME").Value)
                RsDept.MoveNext()
            Loop
            cboContractor.SelectedIndex = 0
        End If


        SqlStr = "SELECT DEPT_DESC FROM PAY_DEPT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        cboDept.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboDept.SelectedIndex = 0

        SqlStr = "SELECT SHIFT_CODE FROM PAY_SHIFT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY SHIFT_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        cboShift.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboShift.Items.Add(RsDept.Fields("SHIFT_CODE").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboShift.SelectedIndex = 0

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

        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False

        chkShift.CheckState = System.Windows.Forms.CheckState.Checked
        cboShift.Enabled = False

        chkDept.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False

        chkContractor.CheckState = System.Windows.Forms.CheckState.Checked
        cboContractor.Enabled = False

        txtAttnDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtAttnDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        OptAll.Checked = True
        HideUnHide(False)

        If CurrModuleName = mContPayrollModule Then
            optProcess(1).Checked = True
            optProcess(0).Enabled = False
            optFrom(2).Enabled = False
            optFrom(1).Enabled = True
        Else
            optProcess(0).Checked = True
            optProcess(1).Enabled = False
            optFrom(1).Enabled = False
            optFrom(2).Enabled = True
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        '    Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmAttnProcessFromMachine_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '    'PvtDBCn.Close
        '    'Set PvtDBCn = Nothing
    End Sub

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAll.CheckedChanged
        If eventSender.Checked Then
            HideUnHide(False)
        End If
    End Sub

    Private Sub OptParti_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptParti.CheckedChanged
        If eventSender.Checked Then
            HideUnHide(True)
        End If
    End Sub

    Private Sub optProcess_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optProcess.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optProcess.GetIndex(eventSender)

            '    If Index = 0 Then
            '        optFrom(1).Enabled = False
            '        optFrom(0).Enabled = True
            '        optFrom(2).Enabled = True
            '        optFrom(0).Value = True
            '    Else
            '        optFrom(1).Enabled = True
            '        optFrom(0).Enabled = False
            '        optFrom(2).Enabled = False
            '        optFrom(1).Value = True
            '    End If
        End If
    End Sub

    Private Sub txtAttnDateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAttnDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtAttnDateFrom.Text) = "" Or Trim(txtAttnDateFrom.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtAttnDateFrom.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtAttnDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAttnDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtAttnDateTo.Text) = "" Or Trim(txtAttnDateTo.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtAttnDateTo.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub TxtCardNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCardNo.DoubleClick
        cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtCardNo.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCardNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mTableName As String

        If optProcess(0).Checked = True Then
            mTableName = "PAY_EMPLOYEE_MST"
        Else
            mTableName = "PAY_CONT_EMPLOYEE_MST"
        End If
        If TxtCardNo.Text = "" Then GoTo EventExitSub
        ''TxtCardNo.Text = VB6.Format(TxtCardNo.Text, "000000")
        TxtCardNo.Text = TxtCardNo.Text
        If MainClass.ValidateWithMasterTable((TxtCardNo.Text), "EMP_CODE", "EMP_NAME", mTableName, PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("CardNo. Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        Else
            TxtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub HideUnHide(ByRef mCheck As Boolean)
        TxtCardNo.Enabled = mCheck
        cmdSearch.Enabled = mCheck
    End Sub
    Private Function AttnProcess(ByRef xDate As String) As Boolean

        On Error GoTo ErrPart


        Dim GateConnStr As String
        Dim GateDBCn As ADODB.Connection
        Dim RsCheckRecd As ADODB.Recordset
        Dim RsGateOut As ADODB.Recordset
        Dim RsGate As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mCode As String

        Dim mTOTHours As Double
        Dim mWorksHours As Double
        Dim mOTHours As Double
        Dim mSundayOTHours As Double
        Dim pInDateTimeStr As String
        Dim pOutDateTimeStr As String
        Dim mEMPOTRate As Double
        Dim mOPApplicable As Double

        Dim pInDateTime As Date
        Dim pOutDateTime As Date

        Dim mDate As String
        Dim mDate2 As String
        'Dim RsSalTRN As ADODB.Recordset = Nothing
        'Dim RsEmployee As ADODB.Recordset
        'Dim SqlStr As String=""=""
        Static mDOJ As String
        Static mDOL As String
        Dim RsEmployee As ADODB.Recordset
        Dim mPassword As String

        Dim pInTimeExists As String
        Dim pOutTimeExists As String
        Dim mEmpTable As String
        Dim mAttnTable As String

        Dim mShiftInTime As String
        Dim mShiftOutTime As String
        Dim mShiftDateInTime As String
        Dim mShiftNextDateInTime As String
        Dim mMarginsMinute As Double
        Dim mIsRoundClock As Boolean
        Dim mIsPrevRoundClock As Boolean
        Dim mGetTime As Date
        Dim mPunchOption As String
        Dim mDeptCode As String
        Dim mFMark As String
        Dim mSMark As String

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMannualChange As Boolean

        Dim mTOTHoursDate As Date
        Dim mWorksHoursDate As Date
        Dim mOTHoursDate As Date
        Dim mSundayOTHoursDate As Date
        Dim mIsHoliday As Boolean
        Dim mHolidayType As String
        Dim mShiftMargin As String

        Dim pFHalf As String
        Dim pSHalf As String
        Dim mEmpShiftBreak As String

        Dim pFHalfPresent As Integer
        Dim pSHalfPresent As Integer

        Dim mPresentMirginIn As String
        Dim mPresentMirginOut As String
        Dim mContractorCode As Integer

        Dim xShiftTime As String
        Dim xShiftOutTime As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Minimum = 0

        '    mMarginsMinute = -30



        If optProcess(0).Checked = True Then
            mEmpTable = "PAY_EMPLOYEE_MST"
            mAttnTable = "PAY_DALIY_ATTN_TRN"
        Else
            mEmpTable = "PAY_CONT_EMPLOYEE_MST"
            mAttnTable = "PAY_CONT_DALIY_ATTN_TRN"
        End If


        mPassword = "FDMSAMHO"
        GateConnStr = StrConn ''"DSN=" & DBConDSN & ""
        mDate = VB6.Format(xDate, "YYYYMMDD")
        mDate2 = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(xDate)), "YYYYMMDD")

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        GateDBCn = New ADODB.Connection
        GateDBCn.Open(GateConnStr)



        mDOJ = MainClass.LastDay(Month(CDate(xDate)), Year(CDate(xDate))) & "/" & VB6.Format(xDate, "MM/YYYY")
        mDOL = VB6.Format(xDate, "MM/YYYY")


        SqlStr = " SELECT * FROM " & vbCrLf _
            & " " & mEmpTable & " " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"

        If optProcess(0).Checked = True Then

            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
            End If

            If chkDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(Trim(cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & Trim(mDeptCode) & "' "
                End If
            End If

            If chkShift.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & "AND EMP_CODE IN ( " & vbCrLf & " SELECT EMP_CODE FROM PAY_SHIFT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "')"

            End If
            SqlStr = SqlStr & vbCrLf & " AND PUNCH_OPT <> 'M'"
            SqlStr = SqlStr & vbCrLf & " AND (PUNCH_STOP_DATE >=TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR PUNCH_STOP_DATE IS NULL)"
        Else
            If chkDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(Trim(cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & Trim(mDeptCode) & "' "
                End If
            End If

            If chkContractor.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(Trim(cboContractor.Text), "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mContractorCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & "AND CONTRACTOR_CODE=" & Val(CStr(mContractorCode)) & " "
                End If
            End If
            If optFrom(1).Checked = True Then
                SqlStr = SqlStr & vbCrLf & "AND EMP_CODE IN ( " & vbCrLf & " SELECT EMP_CODE FROM PAY_CONT_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " )"
            End If
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & Trim(TxtCardNo.Text) & "'"
        End If


        SqlStr = SqlStr & vbCrLf & " Order By EMP_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmployee.EOF = False Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PBar.Maximum = MainClass.GetMaxRecord(mEmpTable, PubDBCn, SqlStr)
            PBar.Value = PBar.Minimum

            Do While RsEmployee.EOF = False
                mCode = IIf(IsDBNull(RsEmployee.Fields("EMP_CODE").Value), "", RsEmployee.Fields("EMP_CODE").Value)
                '            mMannualChange = False
                '
                '            If chkReProcessMannual.Value = vbUnchecked Then
                '                SqlStr = " SELECT REF_TYPE FROM " & mAttnTable & " " & vbCrLf _
                ''                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                ''                        & " AND ATTN_DATE='" & VB6.Format(xDate, "DD-MMM-YYYY") & "'" & vbCrLf _
                ''                        & " AND EMP_CODE='" & mCode & "' AND REF_TYPE='M'"
                '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
                '
                '                mMannualChange = False
                '                If RsTemp.EOF = False Then
                '                    mMannualChange = True
                '                End If
                '
                '                If mMannualChange = True Then
                '                    GoTo NextRec
                '                End If
                '            End If

                If optProcess(0).Checked = True Then
                    mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 5, "I", "E")
                    mShiftMargin = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), -120, "I", "E")

                    mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 5, "O", "E")
                    mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "E")
                    mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "E")

                    xShiftTime = VB6.Format(xDate & " " & mShiftInTime, "DD-MMM-YYYY HH:MM")

                    If mIsRoundClock = False Then
                        xShiftOutTime = VB6.Format(xDate & " " & mShiftOutTime, "DD-MMM-YYYY HH:MM")
                    Else
                        xShiftOutTime = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(xDate)) & " " & mShiftOutTime, "DD-MMM-YYYY HH:MM")
                    End If
                    mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(xShiftTime)), "DD/MM/YYYY HH:MM")))
                    mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))
                Else
                    mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "C")
                    mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "C")
                    mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 0, "I", "C")
                    mShiftMargin = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), -120, "I", "C")
                    mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 0, "O", "C")
                End If

                mPresentMirginIn = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 120, "I", "E")
                mPresentMirginOut = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 120, "O", "E")

                pInDateTime = CDate("00:00:00")
                pOutDateTime = CDate("00:00:00")
                pInTimeExists = "00:00"
                pOutTimeExists = "00:00"
                mTOTHours = 0
                mWorksHours = 0
                mOTHours = 0
                pInDateTimeStr = ""

                pFHalf = ""
                pSHalf = ""

                mGetTime = GetIOTime(mCode, VB6.Format(xDate, "DD/MM/YYYY"), "I", mShiftMargin, mShiftOutTime, mIsRoundClock, mIsPrevRoundClock, pInDateTimeStr, GateDBCn)

                pInDateTime = CDate(VB6.Format(mGetTime, "HH:MM:SS"))
                pInDateTimeStr = VB6.Format(mGetTime, "DD/MM/YYYY HH:MM:SS")

                If pInDateTime = CDate("00:00:00") Then
                    mGetTime = CDate("00:00:00")
                    pOutDateTimeStr = "01/01/1900 00:00:00"
                Else
                    mGetTime = GetIOTime(mCode, VB6.Format(xDate, "DD/MM/YYYY"), "O", mShiftMargin, mShiftOutTime, mIsRoundClock, mIsPrevRoundClock, pInDateTimeStr, GateDBCn)
                    pOutDateTimeStr = VB6.Format(mGetTime, "DD/MM/YYYY HH:MM:SS")
                End If
                If pInDateTimeStr = pOutDateTimeStr Then
                    pOutDateTimeStr = "01/01/1900 00:00:00"
                    pOutDateTime = CDate("00:00:00")
                Else
                    pOutDateTime = CDate(VB6.Format(mGetTime, "HH:MM:SS"))
                    If DateDiff(Microsoft.VisualBasic.DateInterval.Second, CDate(pInDateTimeStr), CDate(pOutDateTimeStr)) < 300 Then ''300Sec
                        pOutDateTimeStr = "01/01/1900 00:00:00"
                        pOutDateTime = CDate("00:00:00")
                    End If
                End If


                If mCode <> "" Then
                    If pOutDateTime <> CDate("00:00:00") Then

                        If GetTotatHours(pInDateTime, pOutDateTime, pInDateTimeStr, pOutDateTimeStr, mTOTHoursDate, mWorksHoursDate, mOTHoursDate, mSundayOTHoursDate, CDate(mShiftInTime), CDate(mShiftOutTime), VB6.Format(xDate, "DD-MMM-YYYY"), mCode) = False Then GoTo ErrPart

                        If optProcess(0).Checked = True Then
                            '                        If mIsRoundClock = True Then
                            If CDate(pInDateTimeStr) <= CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 120, CDate(xShiftTime))) And CDate(pOutDateTimeStr) >= CDate(mEmpShiftBreak) Then
                                If GetIsHolidays(VB6.Format(xDate, "DD/MM/YYYY"), "", mCode, "Y", IIf(optProcess(1).Checked = True, "N", "Y")) = True Then
                                    pFHalf = ""
                                Else
                                    pFHalf = "P"
                                End If
                            End If


                            If CDate(pInDateTimeStr) <= CDate(mEmpShiftBreak) And CDate(pOutDateTimeStr) >= CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, -120, CDate(xShiftOutTime))) Then
                                If GetIsHolidays(VB6.Format(xDate, "DD/MM/YYYY"), "", mCode, "Y", IIf(optProcess(1).Checked = True, "N", "Y")) = True Then
                                    pSHalf = ""
                                Else
                                    pSHalf = "P"
                                End If
                            Else
                                mTOTHours = Val(VB.Left(VB6.Format(mTOTHoursDate, "HH:MM"), 2)) + (CDbl(VB.Right(VB6.Format(mTOTHoursDate, "HH:MM"), 2)) / 60)
                                If mTOTHours >= 8 Then
                                    pSHalf = "P"
                                End If
                            End If
                            '                        Else
                            '                            If CVDate(pInDateTime) <= CVDate(mPresentMirginIn) And CVDate(pOutDateTime) >= CVDate(mEmpShiftBreak) Then
                            '                                If GetIsHolidays(Format(xDate, "DD/MM/YYYY"), "", mCode, "Y", IIf(optProcess(1).Value = True, "N", "Y")) = True Then
                            '                                    pFHalf = ""
                            '                                Else
                            '                                    pFHalf = "P"
                            '                                End If
                            '                            End If
                            '
                            '                            If CVDate(pInDateTime) <= CVDate(mEmpShiftBreak) And CVDate(pOutDateTime) >= CVDate(mPresentMirginOut) Then
                            '                                If GetIsHolidays(Format(xDate, "DD/MM/YYYY"), "", mCode, "Y", IIf(optProcess(1).Value = True, "N", "Y")) = True Then
                            '                                    pFHalf = ""
                            '                                Else
                            '                                    pSHalf = "P"
                            '                                End If
                            '                            End If
                            '                        End If
                        End If

                        mTOTHours = Val(VB.Left(VB6.Format(mTOTHoursDate, "HH:MM"), 2)) + (CDbl(VB.Right(VB6.Format(mTOTHoursDate, "HH:MM"), 2)) / 60)
                        mWorksHours = Val(VB.Left(VB6.Format(mWorksHoursDate, "HH:MM"), 2)) + (CDbl(VB.Right(VB6.Format(mWorksHoursDate, "HH:MM"), 2)) / 60)
                        mOTHours = Val(VB.Left(VB6.Format(mOTHoursDate, "HH:MM"), 2)) + (CDbl(VB.Right(VB6.Format(mOTHoursDate, "HH:MM"), 2)) / 60)
                        mSundayOTHours = Val(VB.Left(VB6.Format(mSundayOTHoursDate, "HH:MM"), 2)) + (CDbl(VB.Right(VB6.Format(mSundayOTHoursDate, "HH:MM"), 2)) / 60)


                        If optProcess(0).Checked = True Then 'Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10
                            mHolidayType = ""

                            mIsHoliday = GetIsHolidays(VB6.Format(xDate, "DD/MM/YYYY"), mHolidayType, mCode, "", "Y")


                            If mIsHoliday = False Then
                                mOTHours = mOTHours
                            Else
                                mOTHours = mWorksHours + mOTHours
                            End If

                            If mIsHoliday = False Then
                                mWorksHours = mWorksHours
                            Else
                                mWorksHours = 0
                            End If
                        Else
                            mHolidayType = ""

                            mIsHoliday = GetIsHolidays(VB6.Format(xDate, "DD/MM/YYYY"), mHolidayType, mCode, "", "N")


                            If mIsHoliday = False Then
                                mOTHours = mOTHours
                            Else
                                mOTHours = mWorksHours + mOTHours
                            End If

                            If mIsHoliday = False Then
                                mWorksHours = mWorksHours
                            Else
                                mWorksHours = 0
                            End If
                        End If
                    End If

                    '                If chkReProcess.Value = vbUnchecked Then
                    '                    SqlStr = " SELECT TO_CHAR(IN_TIME,'DD-MON-YYYY HH24:MI') AS IN_TIME, TO_CHAR(OUT_TIME,'DD-MON-YYYY  HH24:MI') AS OUT_TIME FROM " & mAttnTable & " " & vbCrLf _
                    ''                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    ''                            & " AND ATTN_DATE='" & VB6.Format(xDate, "DD-MMM-YYYY") & "'" & vbCrLf _
                    ''                            & " AND EMP_CODE='" & mCode & "'"
                    '                    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsCheckRecd, adLockReadOnly
                    '
                    '                    If RsCheckRecd.EOF = False Then
                    '                        pInTimeExists = IIf(IsNull(RsCheckRecd!IN_TIME), "", RsCheckRecd!IN_TIME)
                    '                        pOutTimeExists = IIf(IsNull(RsCheckRecd!OUT_TIME), "", RsCheckRecd!OUT_TIME)
                    '                        If pInTimeExists = pOutTimeExists Then
                    '                            pOutTimeExists = "00:00"
                    '                        End If
                    '                        If Format(pOutTimeExists, "HH:MM:SS") = "00:00:00" Then
                    '                            pOutTimeExists = "00:00"
                    '                        End If
                    '                    Else
                    '                        pInTimeExists = "00:00"
                    '                        pOutTimeExists = "00:00"
                    '                    End If
                    '                Else
                    '                    pInTimeExists = "00:00"
                    '                    pOutTimeExists = "00:00"
                    '                End If

                    SqlStr = " DELETE FROM " & mAttnTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

                    PubDBCn.Execute(SqlStr)
                    '                If pInTimeExists <> "00:00" Then
                    If VB6.Format(pInTimeExists, "HH:MM") = "00:00" Then
                        pInDateTimeStr = VB6.Format(pInDateTimeStr, "DD-MMM-YYYY HH:MM")
                    Else
                        pInDateTimeStr = pInTimeExists
                    End If

                    If pOutTimeExists = "00:00" Then
                        pOutDateTimeStr = VB6.Format(pOutDateTimeStr, "DD-MMM-YYYY HH:MM")
                    Else
                        pOutDateTimeStr = pOutTimeExists
                    End If

                    If GetEmployeeExists(mCode, xDate, mEmpTable) = True Then
                        SqlStr = " INSERT INTO " & mAttnTable & " ( " & vbCrLf _
                            & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                            & " EMP_CODE, ATTN_DATE, " & vbCrLf _
                            & " IN_TIME, OUT_TIME, TOT_HOURS," & vbCrLf _
                            & " WORKS_HOURS, OT_HOURS, SUNDAY_OTHOURS, " & vbCrLf _
                            & " ADDUSER, ADDDATE, " & vbCrLf _
                            & " IN_TIME_O, OUT_TIME_O, REF_TYPE," & vbCrLf _
                            & " ATTN_MARK_F, ATTN_MARK_S " & vbCrLf _
                            & " ) VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(xDate)) & ", " & vbCrLf _
                            & " '" & mCode & "', TO_DATE('" & VB6.Format(xDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & pInDateTimeStr & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & pOutDateTimeStr & "','DD-MON-YYYY HH24:MI'), " & mTOTHours & ", " & vbCrLf _
                            & " " & mWorksHours & ", " & mOTHours & ", " & mSundayOTHours & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " TO_DATE('" & pInDateTimeStr & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & pOutDateTimeStr & "','DD-MON-YYYY HH24:MI'), 'O', " & vbCrLf & " '" & pFHalf & "', '" & pSHalf & "'" & vbCrLf & " ) "

                        PubDBCn.Execute(SqlStr)

                        If optProcess(0).Checked = True Then
                            If pFHalf = "P" Or pSHalf = "P" Then
                                If UpdateEmpPresent(mCode, xDate, pFHalf, pSHalf, PubDBCn) = False Then GoTo ErrPart
                            End If
                        End If

                        mEMPOTRate = IIf(IsDBNull(RsEmployee.Fields("EMP_OT_RATE").Value), 0, RsEmployee.Fields("EMP_OT_RATE").Value)
                        mOPApplicable = IIf(IsDBNull(RsEmployee.Fields("OVERTIME_APP").Value), 0, RsEmployee.Fields("OVERTIME_APP").Value)

                        If mEMPOTRate > 0 And mOPApplicable > 0 Then
                            If UpdateOverTime(mCode, xDate, PubDBCn) = False Then GoTo ErrPart
                        End If
                    End If


                    '                End If
                End If
NextRec:
                PBar.Value = PBar.Value + 1
                RsEmployee.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        AttnProcess = True
        Exit Function

ErrPart:
        'Resume
        '    MsgInformation "Attendance Process Not Complete, Try Again."
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        AttnProcess = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateOverTime(ByRef pCode As String, ByRef pDate As String, ByRef pPubDBCn As ADODB.Connection) As Boolean

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mCol As Integer

        Dim cntField As Integer
        'Dim mLastMonthDay As Integer
        'Dim mDate As String
        Dim mFieldValue As Double
        Dim mOTFactor As Double
        Dim mHours As Integer
        Dim mMin As Integer

        Dim mPrevFieldValue As Double
        Dim mPrevHours As Integer
        Dim mPrevMin As Integer
        Dim mFromDate As String
        Dim mToDate As String
        Dim mOTMin As Long
        Dim mOTMInFactor As Long
        Dim mOTStartHour As Double

        mOTMInFactor = IIf(IsDBNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)
        'PubDBCn.Errors.Clear()
        'PubDBCn.BeginTrans()
        UpdateOverTime = False

        SqlStr = " SELECT TRN.* " & vbCrLf _
            & " FROM PAY_DALIY_ATTN_TRN TRN, PAY_EMPLOYEE_MST EMP, PAY_DESG_MST DMST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TRN.COMPANY_CODE = EMP.COMPANY_CODE" & vbCrLf _
            & " AND TRN.EMP_CODE = EMP.EMP_CODE" & vbCrLf _
            & " AND EMP.COMPANY_CODE=DMST.COMPANY_CODE" & vbCrLf _
            & " AND EMP.EMP_DESG_CODE=DMST.DESG_CODE" & vbCrLf _
            & " AND EMP.OVERTIME_APP<>'0'" & vbCrLf _
            & " AND OT_HOURS>0 AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(pCode) & "'" & vbCrLf _
            & " AND TRN.ATTN_DATE =TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, pPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAttn.EOF = False Then
            'mEmpCode = Trim(IIf(IsDBNull(RsAttn.Fields("EMP_CODE").Value), "", RsAttn.Fields("EMP_CODE").Value))
            'mEmpCode = VB6.Format(mEmpCode, "000000")
            'mDate = VB6.Format(IIf(IsDBNull(RsAttn.Fields("ATTN_DATE").Value), "", RsAttn.Fields("ATTN_DATE").Value), "DD/MM/YYYY")
            If CheckCPLAvail(pCode, pDate) = True Then

                mFieldValue = 0
                mHours = 0
                mMin = 0
            Else
                mFieldValue = Val(IIf(IsDBNull(RsAttn.Fields("OT_HOURS").Value), 0, RsAttn.Fields("OT_HOURS").Value))

                mOTMin = (Int(mFieldValue) * 60) + Int((mFieldValue - Int(mFieldValue)) * 60)
                mOTStartHour = IIf(IsDBNull(RsCompany.Fields("OT_START_MIN").Value), 0, RsCompany.Fields("OT_START_MIN").Value) / 60

                If mOTMin < mOTStartHour Then
                    mOTMin = 0
                Else

                    If mOTMInFactor > 0 Then
                        mOTMin = If(mOTMin >= (Int(mOTMin / mOTMInFactor) * mOTMInFactor) + (mOTMInFactor / 2), (Int(mOTMin / mOTMInFactor) * mOTMInFactor) + mOTMInFactor, Int(mOTMin / mOTMInFactor) * mOTMInFactor)
                        ''=IF(K15>=(L15*30)+15,(L15*30)+30,L15*30)
                    End If
                End If
                'If MainClass.ValidateWithMasterTable(pCode, "EMP_CODE", "EMP_OT_RATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    mOTFactor = MasterNo
                'End If

                mFieldValue = mOTMin

                'If mOTFactor <= 1 Then
                '    mFieldValue = mOTMin * mOTFactor
                'ElseIf mOTFactor = 2 Then
                '    mFieldValue = mOTMin * 0.5
                'End If

                mHours = Int(mFieldValue / 60)
                mMin = mFieldValue - (mHours * 60)
                mPrevHours = 0
                mPrevMin = 0
            End If


            SqlStr = "DELETE FROM PAY_OVERTIME_MST  WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pCode) & "'" & vbCrLf _
                    & " AND OT_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            pPubDBCn.Execute(SqlStr)

            If mFieldValue > 0 And (mHours + mMin) > 0 Then
                SqlStr = "INSERT INTO PAY_OVERTIME_MST ( " & vbCrLf _
                        & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                        & " OT_DATE, OTHOUR, OTMIN, OTTYPE," & vbCrLf _
                        & " PREV_OTHOUR,  PREV_OTMIN, " & vbCrLf _
                        & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Year(CDate(pDate)) & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pCode) & "', " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & "  " & mHours & ", " & mMin & ", '0'," & vbCrLf & " " & mPrevHours & ", " & mPrevMin & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                pPubDBCn.Execute(SqlStr)
            End If
        End If

        'PubDBCn.CommitTrans()
        UpdateOverTime = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateOverTime = False
        'PubDBCn.RollbackTrans()
    End Function
    Private Function AttnProcessExcel(ByRef strXLSFile As String) As Boolean

        On Error GoTo ErrPart


        Dim GateConnStr As String
        Dim GateDBCn As ADODB.Connection
        Dim RsCheckRecd As ADODB.Recordset
        Dim RsGateOut As ADODB.Recordset
        Dim RsGate As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim pSqlStr As String = ""

        Dim mCode As String

        Dim mTOTHours As Double
        Dim mWorksHours As Double
        Dim mOTHoursStr As String
        Dim mOTHours As Double
        Dim mSundayOTHours As Double
        Dim pInDateTimeStr As String
        Dim pOutDateTimeStr As String

        Dim pInDateTime As String
        Dim pOutDateTime As String


        Dim mDate As String
        Dim mDate2 As String
        'Dim RsSalTRN As ADODB.Recordset = Nothing
        'Dim RsEmployee As ADODB.Recordset
        'Dim SqlStr As String=""=""
        Static mDOJ As String
        Static mDOL As String
        Dim RsEmployee As ADODB.Recordset
        Dim mPassword As String

        Dim pInTimeExists As String
        Dim pOutTimeExists As String
        Dim mEmpTable As String
        Dim mAttnTable As String

        Dim mShiftInTime As String
        Dim mShiftOutTime As String
        Dim mShiftDateInTime As String
        Dim mShiftNextDateInTime As String
        Dim mMarginsMinute As Double
        Dim mIsRoundClock As Boolean
        Dim mIsPrevRoundClock As Boolean
        Dim mGetTime As Date
        Dim mPunchOption As String
        Dim mDeptCode As String
        Dim mFMark As String
        Dim mSMark As String

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMannualChange As Boolean

        Dim mTOTHoursDate As Date
        Dim mWorksHoursDate As Date
        Dim mOTHoursDate As Date
        Dim mSundayOTHoursDate As Date
        Dim mIsHoliday As Boolean
        Dim mHolidayType As String
        Dim mShiftMargin As String
        Dim pStatus As String
        Dim pFHalf As String
        Dim pSHalf As String

        Dim pFHalfMark As Short
        Dim pSHalfMark As Short

        Dim mEmpShiftBreak As String

        Dim pFHalfPresent As Integer
        Dim pSHalfPresent As Integer

        Dim mPresentMirginIn As String
        Dim mPresentMirginOut As String
        Dim mContractorCode As Integer

        Dim xShiftTime As String
        Dim xShiftOutTime As String
        Dim xDate As String
        Dim mHours As Long
        Dim mMin As Long
        Dim pShift As String
        Dim mWeekDay As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Minimum = 0

        '    mMarginsMinute = -30

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If optProcess(0).Checked = True Then
            mEmpTable = "PAY_EMPLOYEE_MST"
            mAttnTable = "PAY_DALIY_ATTN_TRN"
        Else
            mEmpTable = "PAY_CONT_EMPLOYEE_MST"
            mAttnTable = "PAY_CONT_DALIY_ATTN_TRN"
        End If

        Dim ErrorFile As System.IO.StreamWriter

        Dim FileName As String = Path.GetFileName(strXLSFile)
        Dim Extension As String = Path.GetExtension(strXLSFile)


        Dim conStr As String = ""
        Select Case UCase(Extension)
            Case ".XLS"
                'Excel 97-03 
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                Exit Select
            Case ".XLSX"
                'Excel 07 
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                Exit Select
        End Select

        conStr = String.Format(conStr, strXLSFile)    ''isHDR='Yes'

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()

        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"      '' ORDER BY 4 DESC
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''DATEOFFICE:Emp_Code:Day:Shift:IN1:Lunch Out:Lunch In:OUT2:Shift Late:Excess Lunch:Shift Early:Hours Worked:Over Time:Status

        If dt.Rows.Count >= 1 Then
            PBar.Maximum = dt.Rows.Count
            PBar.Value = 0
            For Each dtRow In dt.Rows

                xDate = IIf(IsDBNull(dtRow.item(0)), "", dtRow.item(0))
                If Not IsDate(xDate) Then GoTo NextRecd

                mCode = IIf(IsDBNull(dtRow.item(1)), "", dtRow.item(1))
                mCode = VB6.Format(mCode, "000000")

                mDOJ = MainClass.LastDay(Month(CDate(xDate)), Year(CDate(xDate))) & "/" & VB6.Format(xDate, "MM/YYYY")
                mDOL = VB6.Format(xDate, "MM/YYYY")

                pShift = IIf(IsDBNull(dtRow.item(3)), "", dtRow.item(3))
                pInDateTime = IIf(IsDBNull(dtRow.item(4)), "", dtRow.item(4))
                pOutDateTime = IIf(IsDBNull(dtRow.item(7)), "", dtRow.item(7))


                If Not IsDate(pInDateTime) Then
                    pInDateTimeStr = ""
                Else
                    pInDateTime = VB6.Format(pInDateTime, "HH:mm")
                    pInDateTimeStr = VB6.Format(xDate & " " & pInDateTime, "DD/MMM/YYYY HH:mm")
                End If

                If Not IsDate(pOutDateTime) Then
                    pOutDateTimeStr = ""
                Else
                    pOutDateTime = VB6.Format(pOutDateTime, "HH:mm")
                    pOutDateTimeStr = VB6.Format(xDate & " " & pOutDateTime, "DD/MMM/YYYY HH:mm")
                End If

                mTOTHours = Val(IIf(IsDBNull(dtRow.item(11)), 0, dtRow.item(11)))

                mOTHoursStr = Trim(IIf(IsDBNull(dtRow.item(12)), 0, dtRow.item(12)))
                mOTHours = Val(Mid(mOTHoursStr, 1, 2)) + (Val(Mid(mOTHoursStr, 4, 2)) / 60)

                mHours = Val(Mid(mOTHoursStr, 1, 2))
                mMin = Val(Mid(mOTHoursStr, 4, 2))
                mWorksHours = IIf(mOTHours = 0, mTOTHours, 8)
                mSundayOTHours = 0
                pStatus = IIf(IsDBNull(dtRow.item(13)), "", dtRow.item(13))

                If GetEmployeeExists(mCode, xDate, mEmpTable) = True Then
                    If pInDateTimeStr = "" Or pOutDateTimeStr = "" Then
                        If pShift = "OFF" Then
                            mWeekDay = DateTime.Parse(xDate).DayOfWeek
                            If UCase(mWeekDay) = "0" Then
                                pFHalfMark = SUNDAY
                                pSHalfMark = SUNDAY
                            Else
                                pFHalfMark = HOLIDAY
                                pSHalfMark = HOLIDAY
                            End If
                        Else
                            If pStatus = "P" Then
                                pFHalfMark = PRESENT
                                pSHalfMark = PRESENT
                            ElseIf pStatus = "HLF" Then
                                pFHalfMark = PRESENT
                                pSHalfMark = -1
                            ElseIf pStatus = "A" Then
                                pFHalfMark = ABSENT
                                pSHalfMark = ABSENT
                            ElseIf pStatus = "WO" Then
                                pFHalfMark = WOPAY
                                pSHalfMark = WOPAY
                            Else
                                pFHalfMark = -1
                                pSHalfMark = -1
                            End If
                        End If
                        If UpdateEmpAttendance(mCode, xDate, pFHalfMark, pSHalfMark, PubDBCn) = False Then GoTo ErrPart
                        GoTo NextRecd
                    End If

                    pFHalf = ""
                    pSHalf = ""

                    SqlStr = " INSERT INTO " & mAttnTable & " ( " & vbCrLf _
                            & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                            & " EMP_CODE, ATTN_DATE, " & vbCrLf _
                            & " IN_TIME, OUT_TIME, TOT_HOURS," & vbCrLf _
                            & " WORKS_HOURS, OT_HOURS, SUNDAY_OTHOURS, " & vbCrLf _
                            & " ADDUSER, ADDDATE, " & vbCrLf _
                            & " IN_TIME_O, OUT_TIME_O, REF_TYPE," & vbCrLf _
                            & " ATTN_MARK_F, ATTN_MARK_S " & vbCrLf _
                            & " ) VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(xDate)) & ", " & vbCrLf _
                            & " '" & mCode & "', TO_DATE('" & VB6.Format(xDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & pInDateTimeStr & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & pOutDateTimeStr & "','DD-MON-YYYY HH24:MI'), " & mTOTHours & ", " & vbCrLf _
                            & " " & mWorksHours & ", " & mOTHours & ", " & mSundayOTHours & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " TO_DATE('" & pInDateTimeStr & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & pOutDateTimeStr & "','DD-MON-YYYY HH24:MI'), 'O', " & vbCrLf _
                            & " '" & pFHalf & "', '" & pSHalf & "'" & vbCrLf _
                            & " ) "

                    PubDBCn.Execute(SqlStr)

                    'If optProcess(0).Checked = True Then
                    If pShift = "OFF" Then
                        mWeekDay = DateTime.Parse(xDate).DayOfWeek
                        If UCase(0) = "0" Then
                            pFHalfMark = SUNDAY
                            pSHalfMark = SUNDAY
                        Else
                            pFHalfMark = HOLIDAY
                            pSHalfMark = HOLIDAY
                        End If
                    Else
                        If pStatus = "P" Then
                            pFHalfMark = PRESENT
                            pSHalfMark = PRESENT
                        ElseIf pStatus = "HLF" Then
                            pFHalfMark = PRESENT
                            pSHalfMark = -1
                        ElseIf pStatus = "A" Then
                            pFHalfMark = ABSENT
                            pSHalfMark = ABSENT
                        ElseIf pStatus = "WO" Then
                            pFHalfMark = WOPAY
                            pSHalfMark = WOPAY
                        Else
                            pFHalfMark = -1
                            pSHalfMark = -1
                        End If
                    End If
                    'If pFHalf = "P" Or pSHalf = "P" Then
                    If UpdateEmpAttendance(mCode, xDate, pFHalfMark, pSHalfMark, PubDBCn) = False Then GoTo ErrPart

                    pSqlStr = "DELETE FROM PAY_OVERTIME_MST  WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf _
                        & " AND OT_DATE=TO_DATE('" & VB6.Format(xDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    PubDBCn.Execute(pSqlStr)

                    If Val(mOTHours) > 0 Then
                        pSqlStr = "INSERT INTO PAY_OVERTIME_MST ( " & vbCrLf _
                            & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf _
                            & " OT_DATE, OTHOUR, OTMIN, OTTYPE," & vbCrLf _
                            & " PREV_OTHOUR,  PREV_OTMIN, " & vbCrLf _
                            & " ADDUSER, ADDDATE) VALUES (" & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Year(CDate(xDate)) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(mCode) & "', " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(xDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & "  " & mHours & ", " & mMin & ", '0'," & vbCrLf _
                            & " 0, 0, " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                        PubDBCn.Execute(pSqlStr)
                    End If
                End If

                PBar.Value = PBar.Value + 1
NextRecd:

            Next
        End If

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        AttnProcessExcel = True
        Exit Function

ErrPart:
        'Resume
        MsgInformation(Err.Description)
        'Resume Next
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        AttnProcessExcel = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function AttnProcessFromShift(ByRef xDate As String) As Boolean

        On Error GoTo ErrPart


        Dim GateConnStr As String
        Dim GateDBCn As ADODB.Connection
        Dim RsCheckRecd As ADODB.Recordset
        Dim RsGateOut As ADODB.Recordset
        Dim RsGate As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mCode As String

        Dim mTOTHours As Double
        Dim mWorksHours As Double
        Dim mOTHours As Double

        Dim pInDateTimeStr As String
        Dim pOutDateTimeStr As String

        Dim pInDateTime As Date
        Dim pOutDateTime As Date

        Static mDOJ As String
        Static mDOL As String
        Dim RsEmployee As ADODB.Recordset
        Dim mPassword As String

        Dim pInTimeExists As String
        Dim pOutTimeExists As String
        Dim mEmpTable As String
        Dim mAttnTable As String

        Dim mShiftInTime As String
        Dim mShiftOutTime As String
        Dim mShiftDateInTime As String
        Dim mShiftNextDateInTime As String
        Dim mMarginsMinute As Double
        Dim mIsRoundClock As Boolean
        Dim mIsPrevRoundClock As Boolean
        Dim mGetTime As Date
        Dim mPunchOption As String

        Dim mEmpShiftBreak As String
        'Dim mSLTime As String
        'Dim mSLOutTime As String
        'Dim mIsRoundClock As String
        'Dim mShortLeave As Boolean
        'Dim mFirstIsOD As Boolean
        'Dim mSecondIsOD As Boolean
        Dim mISFirstLeave As Boolean
        Dim mISSecondLeave As Boolean
        'Dim mCPLFirstEarn As Boolean
        'Dim mCPLFirstAvail As Boolean
        'Dim mCPLSecondEarn As Boolean
        'Dim mCPLSecondAvail As Boolean
        'Dim mISFirstShortLeave As Boolean
        'Dim mISSecondShortLeave As Boolean
        'Dim mFromTime As String
        'Dim mTOTime As String
        Dim mDeptCode As String

        Dim mFMark As String
        Dim mSMark As String

        Dim pFHalf As String
        Dim pSHalf As String
        Dim pFHalfPresent As Integer
        Dim pSHalfPresent As Integer

        Dim mFromDate As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Minimum = 0

        If optProcess(0).Checked = True Then
            mEmpTable = "PAY_EMPLOYEE_MST"
            mAttnTable = "PAY_DALIY_ATTN_TRN"
        Else
            mEmpTable = "PAY_CONT_EMPLOYEE_MST"
            mAttnTable = "PAY_CONT_DALIY_ATTN_TRN"
        End If


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mDOJ = MainClass.LastDay(Month(CDate(xDate)), Year(CDate(xDate))) & "/" & VB6.Format(xDate, "MM/YYYY")
        mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -2, CDate(mDOJ)))

        mDOL = VB6.Format(xDate, "MM/YYYY")


        SqlStr = " SELECT * FROM " & vbCrLf & " " & mEmpTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        If optProcess(0).Checked = True Then
            If chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & "AND EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
            End If

            If chkDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(Trim(cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptCode = MasterNo
                    SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & Trim(mDeptCode) & "' "
                End If
            End If

            If chkShift.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & "AND EMP_CODE IN ( " & vbCrLf & " SELECT EMP_CODE FROM PAY_SHIFT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "')"

            End If


            If (RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 28 Or RsCompany.Fields("COMPANY_CODE").Value = 33) And CDate(txtAttnDateFrom.Text) >= CDate(mFromDate) And PubSuperUser = "S" Then

            Else
                SqlStr = SqlStr & vbCrLf & " AND PUNCH_OPT = 'M'"
            End If
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If


        SqlStr = SqlStr & vbCrLf & " Order By EMP_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmployee.EOF = False Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If



            PBar.Maximum = MainClass.GetMaxRecord(mEmpTable, PubDBCn, SqlStr)
            PBar.Value = PBar.Minimum

            Do While RsEmployee.EOF = False
                mCode = IIf(IsDBNull(RsEmployee.Fields("EMP_CODE").Value), "", RsEmployee.Fields("EMP_CODE").Value)

                If GetIsHolidays(VB6.Format(xDate, "DD-MMM-YYYY"), "", mCode, "", IIf(optProcess(1).Checked = True, "N", "Y")) = True Then
                    GoTo NextRecd
                End If

                If optProcess(0).Checked = True Then
                    mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 0, "I", "E")
                    mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 0, "O", "E")
                    mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "E")
                    '                If GetIsHolidays(DateAdd("D", -1, Format(xDate, "DD-MMM-YYYY")), "", mCode, "", "N") = True Then
                    '                    mIsPrevRoundClock = False
                    '                Else
                    mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "E")
                    '                End If
                Else
                    mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 0, "I", "C")
                    mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 0, "O", "C")
                    mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "C")
                    '                If GetIsHolidays(DateAdd("D", -1, Format(xDate, "DD-MMM-YYYY")), "", mCode, "", "N") = True Then
                    '                    mIsPrevRoundClock = False
                    '                Else
                    mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "C")
                    '                End If
                End If

                Dim xTempTime As String = VB6.Format(xDate, "DD/MM/YYYY") & " " & mShiftInTime

                mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(xTempTime)), "DD/MM/YYYY HH:MM")))
                mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))

                mISFirstLeave = CheckOtherData(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "L", "I", mShiftInTime, mEmpShiftBreak, "", "")
                mISSecondLeave = CheckOtherData(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "L", "O", mShiftInTime, mEmpShiftBreak, "", "")


                pInDateTime = CDate("00:00:00")
                pOutDateTime = CDate("00:00:00")
                pInTimeExists = "00:00"
                pOutTimeExists = "00:00"
                mTOTHours = 0
                mWorksHours = 0
                mOTHours = 0

                If mISFirstLeave = True And mISSecondLeave = True Then

                ElseIf mISFirstLeave = True And mISSecondLeave = False Then
                    pInDateTime = CDate(VB6.Format(mEmpShiftBreak, "HH:MM:SS"))
                    pInDateTimeStr = VB6.Format(xDate, "DD/MM/YYYY") & " " & pInDateTime

                    If mIsRoundClock = False Then
                        pOutDateTime = CDate(VB6.Format(mShiftOutTime, "HH:MM:SS"))
                        pOutDateTimeStr = VB6.Format(xDate, "DD/MM/YYYY") & " " & pOutDateTime
                    Else
                        pOutDateTime = CDate(VB6.Format(mShiftOutTime, "HH:MM:SS"))
                        pOutDateTimeStr = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(xDate)), "DD/MM/YYYY") & " " & pOutDateTime
                    End If
                    If pInDateTimeStr = pOutDateTimeStr Then
                        '                        pOutDateTimeStr = ""
                        pOutDateTimeStr = "01/01/1900 00:00:00"
                        pOutDateTime = CDate("00:00:00")
                    Else
                        pOutDateTime = CDate(VB6.Format(mShiftOutTime, "HH:MM:SS"))
                    End If

                ElseIf mISFirstLeave = False And mISSecondLeave = True Then
                    pInDateTime = CDate(VB6.Format(mShiftInTime, "HH:MM:SS"))
                    pInDateTimeStr = VB6.Format(xDate, "DD/MM/YYYY") & " " & pInDateTime

                    If mIsRoundClock = False Then
                        pOutDateTime = CDate(VB6.Format(mEmpShiftBreak, "HH:MM:SS"))
                        pOutDateTimeStr = VB6.Format(xDate, "DD/MM/YYYY") & " " & pOutDateTime
                    Else
                        pOutDateTime = CDate(VB6.Format(mEmpShiftBreak, "HH:MM:SS"))
                        pOutDateTimeStr = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(xDate)), "DD/MM/YYYY") & " " & pOutDateTime
                    End If
                    If pInDateTimeStr = pOutDateTimeStr Then
                        '                        pOutDateTimeStr = ""
                        pOutDateTimeStr = "01/01/1900 00:00:00"
                        pOutDateTime = CDate("00:00:00")
                    Else
                        pOutDateTime = CDate(VB6.Format(mShiftOutTime, "HH:MM:SS"))
                    End If
                Else
                    pInDateTime = CDate(VB6.Format(mShiftInTime, "HH:MM:SS"))
                    pInDateTimeStr = VB6.Format(xDate, "DD/MM/YYYY") & " " & pInDateTime

                    If mIsRoundClock = False Then
                        pOutDateTime = CDate(VB6.Format(mShiftOutTime, "HH:MM:SS"))
                        pOutDateTimeStr = VB6.Format(xDate, "DD/MM/YYYY") & " " & pOutDateTime
                    Else
                        pOutDateTime = CDate(VB6.Format(mShiftOutTime, "HH:MM:SS"))
                        pOutDateTimeStr = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(xDate)), "DD/MM/YYYY") & " " & pOutDateTime
                    End If
                    If pInDateTimeStr = pOutDateTimeStr Then
                        '                        pOutDateTimeStr = ""
                        pOutDateTimeStr = "01/01/1900 00:00:00"
                        pOutDateTime = CDate("00:00:00")
                    Else
                        pOutDateTime = CDate(VB6.Format(mShiftOutTime, "HH:MM:SS"))
                    End If
                End If

                If mCode <> "" Then
                    If pOutDateTime <> CDate("00:00:00") Then
                        Call CalcTotatHours(pInDateTime, pOutDateTime, mTOTHours, mWorksHours, mOTHours)
                    End If

                    '                If chkReProcess.Value = vbUnchecked Then
                    '                    SqlStr = " SELECT TO_CHAR(IN_TIME,'DD-MON-YYYY HH24:MI') AS IN_TIME, TO_CHAR(OUT_TIME,'DD-MON-YYYY  HH24:MI') AS OUT_TIME FROM " & mAttnTable & " " & vbCrLf _
                    ''                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    ''                            & " AND ATTN_DATE='" & VB6.Format(xDate, "DD-MMM-YYYY") & "'" & vbCrLf _
                    ''                            & " AND EMP_CODE='" & mCode & "'"
                    '                    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsCheckRecd, adLockReadOnly
                    '
                    '                    If RsCheckRecd.EOF = False Then
                    '                        pInTimeExists = IIf(IsNull(RsCheckRecd!IN_TIME), "", RsCheckRecd!IN_TIME)
                    '                        pOutTimeExists = IIf(IsNull(RsCheckRecd!OUT_TIME), "", RsCheckRecd!OUT_TIME)
                    '                        If pInTimeExists = pOutTimeExists Then
                    '                            pOutTimeExists = "00:00"
                    '                        End If
                    '                        If Format(pOutTimeExists, "HH:MM:SS") = "00:00:00" Then
                    '                            pOutTimeExists = "00:00"
                    '                        End If
                    '                    Else
                    '                        pInTimeExists = "00:00"
                    '                        pOutTimeExists = "00:00"
                    '                    End If
                    '                Else
                    '                    pInTimeExists = "00:00"
                    '                    pOutTimeExists = "00:00"
                    '                End If
                    SqlStr = " DELETE FROM " & mAttnTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

                    PubDBCn.Execute(SqlStr)
                    '                If pInTimeExists <> "00:00" Then
                    If VB6.Format(pInTimeExists, "HH:MM") = "00:00" Then
                        pInDateTimeStr = VB6.Format(pInDateTimeStr, "DD-MMM-YYYY HH:MM")
                    Else
                        pInDateTimeStr = pInTimeExists
                    End If

                    If pOutTimeExists = "00:00" Then
                        pOutDateTimeStr = VB6.Format(pOutDateTimeStr, "DD-MMM-YYYY HH:MM")
                    Else
                        pOutDateTimeStr = pOutTimeExists
                    End If
                    mFMark = ""
                    mSMark = ""

                    SqlStr = " INSERT INTO " & mAttnTable & " ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, " & vbCrLf & " EMP_CODE, ATTN_DATE, " & vbCrLf & " IN_TIME, OUT_TIME, TOT_HOURS," & vbCrLf & " WORKS_HOURS, OT_HOURS," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " IN_TIME_O, OUT_TIME_O, REF_TYPE," & vbCrLf & " ATTN_MARK_F, ATTN_MARK_S " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(xDate)) & ", " & vbCrLf & " '" & mCode & "', TO_DATE('" & VB6.Format(xDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TO_DATE('" & pInDateTimeStr & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & pOutDateTimeStr & "','DD-MON-YYYY HH24:MI'), " & mTOTHours & ", " & vbCrLf & " " & mWorksHours & ", " & mOTHours & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '', '', 'M', " & vbCrLf & " '" & mFMark & "', '" & mSMark & "'" & vbCrLf & " ) "


                    PubDBCn.Execute(SqlStr)

                    If mISFirstLeave = False Or mISSecondLeave = False Then
                        pFHalf = IIf(mISFirstLeave = False, "P", "")
                        pSHalf = IIf(mISSecondLeave = False, "P", "")
                        If UpdateEmpPresent(mCode, xDate, pFHalf, pSHalf, PubDBCn) = False Then GoTo ErrPart
                    End If
                    '                End If
                End If
NextRecd:
                PBar.Value = PBar.Value + 1
                RsEmployee.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        AttnProcessFromShift = True
        Exit Function

ErrPart:
        'Resume
        '    MsgInformation "Attendance Process Not Complete, Try Again."
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        AttnProcessFromShift = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function



    Private Function GetIOTime(ByRef mCode As String, ByRef mAttnDate As String, ByRef mIO As String, ByRef mShiftInTime As String, ByRef mShiftOutTime As String, ByRef pRoundClock As Boolean, ByRef pPrevRoundClock As Boolean, ByRef pInDateTimeStr As Object, ByRef pGateDbCn As ADODB.Connection) As Date

        On Error GoTo ErrPart
        Dim mField As String
        Dim mNextDate As String
        Dim SqlStr As String = ""
        Dim RsGate As ADODB.Recordset
        Dim mMINTime As String
        Dim mMAXTime As String
        Dim mPrevINTime As String
        Dim mPrevDate As String
        Dim RsPrevGate As ADODB.Recordset
        Dim mShiftTime As String
        Dim mDate As String
        Dim mDate2 As String
        Dim mShiftDateInTime As String
        Dim mShiftNextDateInTime As String
        Dim mNextDayShift As String
        Dim mNightShift As Boolean
        Dim mPreviousAbsent As Boolean

        Dim mNewCode As String
        Dim mShowHoliday As String
        Dim mFieldName As String
        Dim mTableName As String
        Dim mPunchOption As String

        mPreviousAbsent = False
        mNightShift = False

        '    mShowHoliday = ""

        ''25-03-2014 sandeep
        '    If CurrModuleName = mContPayrollModule Then   'RsCompany.Fields("COMPANY_CODE").Value = 12 And
        '        mShowHoliday = "N"
        '    End If
        '
        '    If RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Then    '
        mShowHoliday = "N"
        '    End If

        mPunchOption = "P"
        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "PUNCH_OPT", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPunchOption = MasterNo
        End If
        '


        If GetIsHolidays(mAttnDate, "", mCode, mShowHoliday, IIf(optProcess(1).Checked = True, "N", "Y")) = True Then
            GetIOTime = CDate("00:00:00")
            Exit Function
        End If

        If GetIsHolidays(CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mAttnDate))), "", mCode, mShowHoliday, IIf(optProcess(1).Checked = True, "N", "Y")) = True Then
            pPrevRoundClock = False
        End If

        If CheckLeave(mCode, mAttnDate, mIO) = True Then
            GetIOTime = CDate("00:00:00")
            Exit Function
        End If

        If CheckLeave(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mAttnDate))), mIO) = True Then
            pPrevRoundClock = False
            '    Else
            '        If CheckPreviousAttn() = False Then
            '            pPrevRoundClock = False
            '            mPreviousAbsent = True
            '        End If
        End If

        '    If CVDate(mShiftInTime) >= CVDate("16:00") Then  ''23-10-2013
        If pRoundClock = True Then
            mNightShift = True
            pRoundClock = True
            mDate = VB6.Format(mAttnDate, "YYYYMMDD")
            mDate2 = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mAttnDate)), "YYYYMMDD")

            '        If optProcess(1).Value = True Then
            '            mShiftInTime = "16:00"
            '            mNextDayShift = "15:59"
            '        Else
            mNextDayShift = GetShiftTime(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD-MMM-YYYY")))), 0, "I", IIf(optProcess(0).Checked = True, "E", "C"))
            If CDate(mShiftOutTime) = CDate(mNextDayShift) Then
                mNextDayShift = GetShiftTime(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD-MMM-YYYY")))), 1, "I", IIf(optProcess(0).Checked = True, "E", "C"))
            Else
                mNextDayShift = GetShiftTime(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD-MMM-YYYY")))), -60, "I", IIf(optProcess(0).Checked = True, "E", "C"))
            End If
            '        End If
        End If

        If optProcess(1).Checked = True Then
            If RsCompany.Fields("PUNCH_FROM").Value = "G" Then
                mTableName = IIf(IsDBNull(RsCompany.Fields("PUNCH_GATE_TABLE").Value), "TEMPDATA", RsCompany.Fields("PUNCH_GATE_TABLE").Value) '"TEMPDATA_CORP"
            Else
                mTableName = IIf(IsDBNull(RsCompany.Fields("PUNCH_DEPT_TABLE").Value), "FACEPUNCHTEMPDATA", RsCompany.Fields("PUNCH_DEPT_TABLE").Value)
            End If
        Else
            mTableName = "TEMPDATA"
        End If


        ''
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            mNewCode = Replace(Replace(Replace(mCode, "G-", "7"), "B-", "2"), "K-", "11")
        Else
            mNewCode = mCode
        End If


        If optProcess(0).Checked = True Then
            If mIO = "I" Then
                mFieldName = "TIME_FROM"
            Else
                mFieldName = "TIME_TO"
            End If

            SqlStr = " SELECT TO_CHAR(" & mFieldName & ",'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME " & vbCrLf _
                & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf _
                & " AND REF_DATE = TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND MOVE_TYPE='M'"



            MainClass.UOpenRecordSet(SqlStr, pGateDbCn, ADODB.CursorTypeEnum.adOpenStatic, RsGate, ADODB.LockTypeEnum.adLockReadOnly)

            If RsGate.EOF = False Then
                GetIOTime = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("ATTN_TIME").Value), "00:00:00", RsGate.Fields("ATTN_TIME").Value), "DD/MM/YYYY HH:MM:SS"))
                Exit Function
            End If
        Else
            If mIO = "I" Then
                mFieldName = "TIME_FROM"
            Else
                mFieldName = "TIME_TO"
            End If

            SqlStr = " SELECT TO_CHAR(" & mFieldName & ",'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME " & vbCrLf _
                & " FROM PAY_CONT_MOVEMENT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'" & vbCrLf _
                & " AND REF_DATE = TO_DATE('" & VB6.Format(mAttnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


            MainClass.UOpenRecordSet(SqlStr, pGateDbCn, ADODB.CursorTypeEnum.adOpenStatic, RsGate, ADODB.LockTypeEnum.adLockReadOnly)

            If RsGate.EOF = False Then
                GetIOTime = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("ATTN_TIME").Value), "00:00:00", RsGate.Fields("ATTN_TIME").Value), "DD/MM/YYYY HH:MM:SS"))
                Exit Function
            End If
        End If

        If mNightShift = True Then
            mShiftDateInTime = mDate & " " & mShiftInTime
            mShiftNextDateInTime = mDate2 & " " & IIf(mNextDayShift = "00:00", mShiftInTime, mNextDayShift)

            If mIO = "I" Then


                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                        & " WHERE TRIM(CARDNO)='" & Trim(mNewCode) & "'" & vbCrLf _
                        & " AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI') >= '" & mShiftDateInTime & "' AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI')<'" & mShiftNextDateInTime & "'"
                Else
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM vwTempData " & vbCrLf _
                        & " WHERE TRIM(CARDNO)='" & Trim(mNewCode) & "'" & vbCrLf _
                         & " AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI') >= '" & mShiftDateInTime & "' AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI')<'" & mShiftNextDateInTime & "'"

                End If

                '            SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            Else

                If mPunchOption = "P" Then
                    'SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                    '        & " WHERE TRIM(CARDNO)='" & Trim(mNewCode) & "'" & vbCrLf _
                    '        & " AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI') >= '" & mShiftDateInTime & "' AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI')<'" & mShiftNextDateInTime & "'"

                    SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                            & " WHERE TRIM(CARDNO)='" & Trim(mNewCode) & "'" & vbCrLf _
                            & " AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI') >= '" & mShiftDateInTime & "' AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI')<'" & mShiftNextDateInTime & "'"

                Else

                    SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS')  AS ATTN_TIME FROM vwTempData " & vbCrLf _
                        & " WHERE TRIM(CARDNO)='" & Trim(mNewCode) & "'" & vbCrLf _
                       & " AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI') >= '" & mShiftDateInTime & "' AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD HH24:MI')<'" & mShiftNextDateInTime & "'"
                End If


                '            SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            End If
        ElseIf pRoundClock = False And pPrevRoundClock = False Then
            If mIO = "I" Then

                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                    & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'"
                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
                Else
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS')  AS ATTN_TIME FROM vwTempData " & vbCrLf _
                    & " WHERE REFDATE = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'"
                    SqlStr = SqlStr & vbCrLf & "HAVING MIN(OFFICEPUNCH) IS NOT NULL "
                End If

            Else
                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                        & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'"

                    SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH)> MIN(OFFICEPUNCH) "
                Else
                    SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM vwTempData " & vbCrLf _
                       & " WHERE REFDATE = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'"

                    SqlStr = SqlStr & vbCrLf & "HAVING MAX(OFFICEPUNCH)> MIN(OFFICEPUNCH) "
                End If

            End If
        ElseIf pRoundClock = False And pPrevRoundClock = True Then
            If mIO = "I" Then
                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                            & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf _
                            & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'" & vbCrLf _
                            & " AND TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI') > (" & vbCrLf _
                            & " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') FROM TEMPDATA " & vbCrLf _
                            & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf _
                            & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "')"

                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
                Else
                    SqlStr = " SELECT TO_CHAR(MIN(CDATE),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM TEMP_EPUNCH " & vbCrLf _
                        & " WHERE TO_CHAR(CDATE,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMPID)='" & Trim(mNewCode) & "'" & vbCrLf & " AND TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI') > (" & vbCrLf & " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "')"

                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(CDATE) IS NOT NULL "
                End If

            Else
                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                    & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'" & vbCrLf & " AND TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI') > (" & vbCrLf & " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "')"
                    SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH)> MIN(OFFICEPUNCH) "
                Else
                    SqlStr = " SELECT TO_CHAR(MAX(CDATE),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM TEMP_EPUNCH " & vbCrLf _
                   & " WHERE TO_CHAR(CDATE,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMPID)='" & Trim(mNewCode) & "'" & vbCrLf & " AND TO_CHAR(OFFICEPUNCH,'DD-MON-YYYY HH24:MI') > (" & vbCrLf & " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "')"
                    SqlStr = SqlStr & vbCrLf & " HAVING MAX(CDATE)> MIN(CDATE) "
                End If

            End If
        ElseIf pRoundClock = True And pPrevRoundClock = False Then
            If mIO = "I" Then
                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                        & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'"
                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
                Else
                    SqlStr = " SELECT TO_CHAR(MIN(CDATE),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM TEMP_EPUNCH " & vbCrLf _
                        & " WHERE TO_CHAR(CDATE,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMPID)='" & Trim(mNewCode) & "'"
                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(CDATE) IS NOT NULL "
                End If

            Else

                mNextDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD/MM/YYYY"))))

                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                            & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf _
                            & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'"
                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
                Else
                    SqlStr = " SELECT TO_CHAR(MIN(CDATE),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM TEMP_EPUNCH " & vbCrLf _
                            & " WHERE TO_CHAR(CDATE,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf _
                            & " AND TRIM(EMPID)='" & Trim(mNewCode) & "'"
                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(CDATE) IS NOT NULL "
                End If

            End If
        ElseIf pRoundClock = True And pPrevRoundClock = True Then
            If mIO = "I" Then

                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                             & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf _
                             & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "')"
                    '                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "

                    SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "

                    '                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) > MIN(OFFICEPUNCH) OR MAX(OFFICEPUNCH) IS NOT NULL"
                Else
                    SqlStr = " SELECT TO_CHAR(MAX(CDATE),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM TEMP_EPUNCH " & vbCrLf _
                             & " WHERE TO_CHAR(CDATE,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMPID)='" & Trim(mNewCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "')"
                    '                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "

                    SqlStr = SqlStr & vbCrLf & " HAVING MAX(CDATE) IS NOT NULL "
                End If

            Else
                mNextDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD/MM/YYYY"))))

                If mPunchOption = "P" Then
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM " & mTableName & " " & vbCrLf _
                    & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mNewCode) & "'"

                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
                Else
                    SqlStr = " SELECT TO_CHAR(MIN(CDATE),'DD-MON-YYYY HH24:MI:SS') AS ATTN_TIME FROM TEMP_EPUNCH" & vbCrLf _
                                        & " WHERE TO_CHAR(CDATE,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMPID)='" & Trim(mNewCode) & "'"

                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(CDATE) IS NOT NULL "
                End If

            End If
        End If



        MainClass.UOpenRecordSet(SqlStr, pGateDbCn, ADODB.CursorTypeEnum.adOpenStatic, RsGate, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGate.EOF = False Then
            GetIOTime = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("ATTN_TIME").Value), "00:00:00", RsGate.Fields("ATTN_TIME").Value), "DD/MM/YYYY HH:MM:SS"))
        Else
            GetIOTime = CDate("00:00:00")
        End If

        Exit Function

ErrPart:
        'Resume
        '    MsgInformation "Attendance Process Not Complete, Try Again."
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function GetIOTimeOld24082012(ByRef mCode As String, ByRef mAttnDate As String, ByRef mIO As String, ByRef mShiftInTime As Object, ByRef mShiftOutTime As Object, ByRef pRoundClock As Boolean, ByRef pPrevRoundClock As Boolean, ByRef pInDateTimeStr As Object, ByRef pGateDbCn As ADODB.Connection) As Date

        On Error GoTo ErrPart
        Dim mField As String
        Dim mNextDate As String
        Dim SqlStr As String = ""
        Dim RsGate As ADODB.Recordset
        Dim mMINTime As String
        Dim mMAXTime As String
        Dim mPrevINTime As String
        Dim mPrevDate As String
        Dim RsPrevGate As ADODB.Recordset
        Dim mShiftTime As String
        Dim mDate As String
        Dim mDate2 As String

        If GetIsHolidays(mAttnDate, "", mCode, "", IIf(optProcess(1).Checked = True, "N", "Y")) = True Then
            GetIOTimeOld24082012 = CDate("00:00:00")
            Exit Function
        End If

        If GetIsHolidays(CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mAttnDate))), "", mCode, "", IIf(optProcess(1).Checked = True, "N", "Y")) = True Then
            pPrevRoundClock = False
        End If

        If CheckLeave(mCode, mAttnDate, mIO) = True Then
            GetIOTimeOld24082012 = CDate("00:00:00")
            Exit Function
        End If

        If CheckLeave(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mAttnDate))), mIO) = True Then
            pPrevRoundClock = False
        End If

        If CDate(mShiftInTime) >= CDate("16:00") Then
            pRoundClock = True
            mDate = VB6.Format(mAttnDate, "YYYYMMDD")
            mDate2 = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mAttnDate)), "YYYYMMDD")
        End If

        If pRoundClock = False And pPrevRoundClock = False Then
            If mIO = "I" Then
                SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            Else

                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH)> MIN(OFFICEPUNCH) "

            End If
        ElseIf pRoundClock = False And pPrevRoundClock = True Then
            If mIO = "I" Then
                SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            Else

                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"

                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH)> MIN(OFFICEPUNCH) "

            End If
        ElseIf pRoundClock = True And pPrevRoundClock = False Then
            If mIO = "I" Then
                SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            Else

                mNextDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD/MM/YYYY"))))

                SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"
                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "

            End If
        ElseIf pRoundClock = True And pPrevRoundClock = True Then
            If mIO = "I" Then

                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"

                '                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "

                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "

                '                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) > MIN(OFFICEPUNCH) OR MAX(OFFICEPUNCH) IS NOT NULL"

            Else
                mNextDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD/MM/YYYY"))))
                SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            End If
        End If



        MainClass.UOpenRecordSet(SqlStr, pGateDbCn, ADODB.CursorTypeEnum.adOpenStatic, RsGate, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGate.EOF = False Then
            GetIOTimeOld24082012 = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("ATTN_TIME").Value), "00:00:00", RsGate.Fields("ATTN_TIME").Value), "DD/MM/YYYY HH:MM:SS"))
        Else
            GetIOTimeOld24082012 = CDate("00:00:00")
        End If

        Exit Function

ErrPart:
        'Resume
        '    MsgInformation "Attendance Process Not Complete, Try Again."
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Private Function CheckLeave(ByRef pEmpCode As String, ByRef pDate As String, ByRef mIO As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""


        '    If mIO = "I" Then
        SqlStr = " SELECT FIRSTHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND FIRSTHALF NOT IN (-1," & CPLAVAIL & "," & SUNDAY & "," & HOLIDAY & "," & PRESENT & ") AND SECONDHALF NOT IN (-1," & CPLAVAIL & "," & SUNDAY & "," & HOLIDAY & "," & PRESENT & ")"
        '    Else
        '        SqlStr = " SELECT SECONDHALF " & vbCrLf _
        ''                & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
        ''                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf _
        ''                & " AND ATTN_DATE='" & VB6.Format(pDate, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                & " AND SECONDHALF NOT IN (-1," & CPLAVAIL & ")"
        '    End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckLeave = True
        Else
            CheckLeave = False
        End If
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Function
    Private Function CheckOtherData(ByRef pEmpCode As String, ByRef pDate As String, ByRef pCheckType As String, ByRef pHalf As String, ByRef mOutTime As String, ByRef mShiftBreakeTime As String, ByRef pFromDate As String, ByRef pToDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pField As String
        Dim SqlStr As String = ""

        pFromDate = ""
        pToDate = ""

        If pCheckType = "L" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " NOT IN (-1," & CPLEARN & "," & CPLAVAIL & "," & PRESENT & ")"
        ElseIf pCheckType = "CE" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & " = " & CPLEARN & ""
        ElseIf pCheckType = "CA" Then
            If pHalf = "I" Then
                pField = "FIRSTHALF"
            Else
                pField = "SECONDHALF"
            End If

            SqlStr = " SELECT " & pField & " " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & pField & "= " & CPLAVAIL & ""
        ElseIf pCheckType = "O" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='O'" & vbCrLf & " AND REF_DATE='" & UCase(VB6.Format(pDate, "DD-MMM-YYYY")) & "'"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 28 Or RsCompany.Fields("COMPANY_CODE").Value = 33 Then
            '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If

            If pHalf = "I" Then
                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'"
            Else
                If mOutTime = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_FROM,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')"
                End If
            End If
        ElseIf pCheckType = "M" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='M'" & vbCrLf & " AND REF_DATE='" & UCase(VB6.Format(pDate, "DD-MMM-YYYY")) & "'"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 28 Or RsCompany.Fields("COMPANY_CODE").Value = 33 Then
            '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If

            If pHalf = "I" Then
                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'"
            Else
                If mOutTime = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_FROM,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')"
                End If
            End If
        ElseIf pCheckType = "P" Then
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE='P' AND AGT_LEAVE='N'" & vbCrLf & " AND REF_DATE='" & UCase(VB6.Format(pDate, "DD-MMM-YYYY")) & "'"

            'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 28 Or RsCompany.Fields("COMPANY_CODE").Value = 33 Then
            '    SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            'End If

            If pHalf = "I" Then
                SqlStr = SqlStr & vbCrLf & "AND TO_CHAR(TIME_FROM,'HH24:MI')<='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "'"
            Else
                If mOutTime = "00:00" Then

                Else
                    SqlStr = SqlStr & vbCrLf & "AND (TO_CHAR(TIME_FROM,'HH24:MI')>='" & VB6.Format(mShiftBreakeTime, "HH:MM") & "')"
                End If
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If pCheckType = "L" Or pCheckType = "CA" Or pCheckType = "CE" Then

            Else
                pFromDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_FROM").Value), "", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
                pToDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TIME_TO").Value), "", RsTemp.Fields("TIME_TO").Value), "HH:MM")
            End If

            CheckOtherData = True
        Else
            CheckOtherData = False
        End If
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Function
    Private Function GetIOTimeOld(ByRef mCode As String, ByRef mAttnDate As String, ByRef mIO As String, ByRef mShiftInTime As Object, ByRef mShiftOutTime As Object, ByRef pRoundClock As Boolean, ByRef pPrevRoundClock As Boolean, ByRef pInDateTimeStr As Object, ByRef pGateDbCn As ADODB.Connection) As Date

        On Error GoTo ErrPart
        Dim mField As String
        Dim mNextDate As String
        Dim SqlStr As String = ""
        Dim RsGate As ADODB.Recordset
        Dim mMINTime As String
        Dim mMAXTime As String
        Dim mPrevINTime As String
        Dim mPrevDate As String
        Dim RsPrevGate As ADODB.Recordset
        Dim mShiftTime As String

        If pRoundClock = False And pPrevRoundClock = False Then
            If mIO = "I" Then
                SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            Else

                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH)> MIN(OFFICEPUNCH) OR MAX(OFFICEPUNCH) IS NOT NULL "

            End If
        ElseIf pRoundClock = False And pPrevRoundClock = True Then
            If mIO = "I" Then
                SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            Else

                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"

                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH)> MIN(OFFICEPUNCH) OR MAX(OFFICEPUNCH) IS NOT NULL "


                '            SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) > MIN(OFFICEPUNCH) OR MAX(OFFICEPUNCH) IS NOT NULL"

                '            SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "

            End If
        Else
            If mIO = "I" Then
                mPrevDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(mAttnDate, "DD/MM/YYYY"))))
                '                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf _
                ''                        & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mPrevDate, "YYYYMMDD") & "'" & vbCrLf _
                ''                        & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf _
                ''                        & " AND OFFICEPUNCH > (" & vbCrLf _
                ''                        & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf _
                ''                        & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mPrevDate, "YYYYMMDD") & "'" & vbCrLf _
                ''                        & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"
                '                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) > MIN(OFFICEPUNCH) OR MAX(OFFICEPUNCH) IS NOT NULL"

                SqlStr = " SELECT COUNT(1) AS CNT, TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS IN_TIME, TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS OUT_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mPrevDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                MainClass.UOpenRecordSet(SqlStr, pGateDbCn, ADODB.CursorTypeEnum.adOpenStatic, RsPrevGate, ADODB.LockTypeEnum.adLockReadOnly)
                mPrevINTime = "00:00:00"
                If RsPrevGate.EOF = False Then
                    mMINTime = VB6.Format(IIf(IsDBNull(RsPrevGate.Fields("IN_TIME").Value), "00:00:00", RsPrevGate.Fields("IN_TIME").Value), "DD/MM/YYYY HH:MM:SS")
                    mMAXTime = VB6.Format(IIf(IsDBNull(RsPrevGate.Fields("OUT_TIME").Value), "00:00:00", RsPrevGate.Fields("OUT_TIME").Value), "DD/MM/YYYY HH:MM:SS")
                    mShiftTime = GetShiftTime(mCode, VB6.Format(mPrevDate, "DD-MMM-YYYY"), -60, "I", "E")
                    If mShiftTime <= VB6.Format(mMINTime, "HH:MM") Then
                        mPrevINTime = mMINTime
                    ElseIf mShiftTime <= VB6.Format(mMAXTime, "HH:MM") Then
                        mPrevINTime = mMAXTime
                    Else
                        mPrevINTime = "00:00:00"
                    End If
                End If

                If VB6.Format(mPrevINTime, "HH:MM:SS") = "00:00:00" Then
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
                Else
                    SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"
                    '                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "

                    SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) > MIN(OFFICEPUNCH) OR MAX(OFFICEPUNCH) IS NOT NULL"
                End If
            Else

                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"
                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) > MIN(OFFICEPUNCH) OR MAX(OFFICEPUNCH) IS NOT NULL"

                MainClass.UOpenRecordSet(SqlStr, pGateDbCn, ADODB.CursorTypeEnum.adOpenStatic, RsPrevGate, ADODB.LockTypeEnum.adLockReadOnly)
                mPrevINTime = "00:00:00"
                If RsPrevGate.EOF = False Then
                    mPrevINTime = VB6.Format(IIf(IsDBNull(RsPrevGate.Fields("ATTN_TIME").Value), "00:00:00", RsPrevGate.Fields("ATTN_TIME").Value), "DD/MM/YYYY HH:MM:SS")
                End If


                mNextDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD/MM/YYYY"))))
                If mPrevINTime = "00:00:00" Then
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH < (" & vbCrLf & " SELECT MAX(OFFICEPUNCH) FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "')"

                    SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL"

                Else
                    SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM TEMPDATA " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(CARDNO)='" & Trim(mCode) & "'"

                    SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
                End If
            End If
        End If



        MainClass.UOpenRecordSet(SqlStr, pGateDbCn, ADODB.CursorTypeEnum.adOpenStatic, RsGate, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGate.EOF = False Then
            GetIOTimeOld = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("ATTN_TIME").Value), "00:00:00", RsGate.Fields("ATTN_TIME").Value), "DD/MM/YYYY HH:MM:SS"))
        Else
            GetIOTimeOld = CDate("00:00:00")
        End If

        Exit Function

ErrPart:
        'Resume
        '    MsgInformation "Attendance Process Not Complete, Try Again."
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function GetMannualIOTime(ByRef mCode As String, ByRef mAttnDate As String, ByRef mIO As String, ByRef pRoundClock As Boolean, ByRef pPrevRoundClock As Boolean, ByRef pGateDbCn As ADODB.Connection) As Date

        On Error GoTo ErrPart
        Dim mField As String
        Dim mNextDate As String
        Dim SqlStr As String = ""
        Dim RsGate As ADODB.Recordset
        Dim mNewCode As String
        Dim mTableName As String

        'SqlStr = " SELECT TO_CHAR(IN_TIME,'HH24:MI') AS INTIME, TO_CHAR(OUT_TIME,'HH24:MI') AS OUTTIME FROM PAY_CONT_IO_TRN " & vbCrLf _
        ''                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND  TO_CHAR(ATTN_DATE,'YYYYMMDD') = '" & mDate & "'" & vbCrLf _
        ''                        & " AND TRIM(EMP_CODE)='" & Trim(mCode) & "'"
        '

        If optProcess(1).Checked = True Then
            If RsCompany.Fields("PUNCH_FROM").Value = "G" Then
                mTableName = IIf(IsDBNull(RsCompany.Fields("PUNCH_GATE_TABLE").Value), "TEMPDATA", RsCompany.Fields("PUNCH_GATE_TABLE").Value) '"TEMPDATA_CORP"
            Else
                mTableName = IIf(IsDBNull(RsCompany.Fields("PUNCH_DEPT_TABLE").Value), "FACEPUNCHTEMPDATA", RsCompany.Fields("PUNCH_DEPT_TABLE").Value)
            End If
        Else
            If RsCompany.Fields("COMPANY_CODE").Value = 35 Then
                mTableName = "TEMPDATA_CORP"
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 1 Then
                mTableName = IIf(IsDBNull(RsCompany.Fields("PUNCH_GATE_TABLE").Value), "TEMPDATA", RsCompany.Fields("PUNCH_GATE_TABLE").Value)
            Else
                mTableName = "TEMPDATA"
            End If
        End If

        '    If RsCompany.Fields("COMPANY_CODE").Value = 35 Then
        '        mTableName = "TEMPDATA_CORP"
        '    Else
        '        mTableName = "TEMPDATA"
        '    End If

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 28 Or RsCompany.Fields("COMPANY_CODE").Value = 33 Then
            mNewCode = RsCompany.Fields("COMPANY_CODE").Value & mCode
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 35 Or RsCompany.Fields("COMPANY_CODE").Value = 41 Or RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            mNewCode = CStr(Val(mCode))
        Else
            mNewCode = mCode
        End If

        If pRoundClock = False And pPrevRoundClock = False Then
            If mIO = "I" Then
                SqlStr = " SELECT TO_CHAR(MIN(IN_TIME),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM PAY_CONT_IO_TRN " & vbCrLf & " WHERE TO_CHAR(ATTN_DATE,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMP_CODE)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(IN_TIME) IS NOT NULL "
            Else

                SqlStr = " SELECT TO_CHAR(MAX(OUT_TIME),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM PAY_CONT_IO_TRN " & vbCrLf & " WHERE TO_CHAR(ATTN_DATE,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMP_CODE)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OUT_TIME) IS NOT NULL "

            End If
        ElseIf pRoundClock = False And pPrevRoundClock = True Then
            If mIO = "I" Then
                SqlStr = " SELECT TO_CHAR(MIN(IN_TIME),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM PAY_CONT_IO_TRN " & vbCrLf & " WHERE TO_CHAR(ATTN_DATE,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMP_CODE)='" & Trim(mCode) & "'" & vbCrLf & " AND OFFICEPUNCH > (" & vbCrLf & " SELECT MIN(OFFICEPUNCH) FROM " & mTableName & " " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMP_CODE)='" & Trim(mNewCode) & "')"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            Else

                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM PAY_CONT_IO_TRN " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMP_CODE)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "

            End If
        Else
            If mIO = "I" Then
                SqlStr = " SELECT TO_CHAR(MAX(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM PAY_CONT_IO_TRN " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mAttnDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMP_CODE)='" & Trim(mCode) & "'"
                SqlStr = SqlStr & vbCrLf & " HAVING MAX(OFFICEPUNCH) IS NOT NULL "
            Else
                mNextDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(VB6.Format(mAttnDate, "DD/MM/YYYY"))))
                SqlStr = " SELECT TO_CHAR(MIN(OFFICEPUNCH),'DD-MON-YYYY HH24:MI') AS ATTN_TIME FROM PAY_CONT_IO_TRN " & vbCrLf & " WHERE TO_CHAR(OFFICEPUNCH,'YYYYMMDD') = '" & VB6.Format(mNextDate, "YYYYMMDD") & "'" & vbCrLf & " AND TRIM(EMP_CODE)='" & Trim(mCode) & "'"

                SqlStr = SqlStr & vbCrLf & " HAVING MIN(OFFICEPUNCH) IS NOT NULL "
            End If
        End If



        MainClass.UOpenRecordSet(SqlStr, pGateDbCn, ADODB.CursorTypeEnum.adOpenStatic, RsGate, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGate.EOF = False Then
            GetMannualIOTime = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("ATTN_TIME").Value), "", RsGate.Fields("ATTN_TIME").Value), "DD/MM/YYYY HH:MM:SS"))
        Else
            GetMannualIOTime = CDate("00:00:00")
        End If

        Exit Function

ErrPart:
        'Resume
        '    MsgInformation "Attendance Process Not Complete, Try Again."
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function AttnProcessMannual(ByRef xDate As String) As Boolean

        On Error GoTo ErrPart

        '
        'Dim GateConnStr As String
        'Dim GateDBCn As ADODB.Connection
        Dim RsCheckRecd As ADODB.Recordset
        'Dim RsGateOut As ADODB.Recordset
        Dim RsGate As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mCode As String

        Dim mTOTHours As Double
        Dim mWorksHours As Double
        Dim mOTHours As Double

        Dim pInDateTimeStr As String
        Dim pOutDateTimeStr As String

        Dim pInDateTime As Date
        Dim pOutDateTime As Date

        Dim mDate As String
        Dim mDate2 As String
        'Dim RsSalTRN As ADODB.Recordset = Nothing
        'Dim RsEmployee As ADODB.Recordset
        'Dim SqlStr As String=""=""
        Static mDOJ As String
        Static mDOL As String
        Dim RsEmployee As ADODB.Recordset
        Dim mPassword As String

        Dim pInTimeExists As String
        Dim pOutTimeExists As String
        Dim mEmpTable As String
        Dim mAttnTable As String

        Dim mShiftInTime As String
        Dim mShiftOutTime As String
        Dim mShiftDateInTime As String
        Dim mShiftNextDateInTime As String
        Dim mMarginsMinute As Double

        Dim mIsRoundClock As Boolean
        Dim mIsPrevRoundClock As Boolean
        Dim mGetTime As Date
        Dim mFMark As String
        Dim mSMark As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Minimum = 0

        '    mMarginsMinute = -30

        If optProcess(0).Checked = True Then
            AttnProcessMannual = True
            Exit Function
            '        mEmpTable = "PAY_EMPLOYEE_MST"
            '        mAttnTable = "PAY_DALIY_ATTN_TRN"
        Else
            mEmpTable = "PAY_CONT_EMPLOYEE_MST"
            mAttnTable = "PAY_CONT_DALIY_ATTN_TRN"
        End If


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mDate = VB6.Format(xDate, "YYYYMMDD")
        mDate2 = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(xDate)), "YYYYMMDD")

        mDOJ = MainClass.LastDay(Month(CDate(xDate)), Year(CDate(xDate))) & "/" & VB6.Format(xDate, "MM/YYYY")
        mDOL = "01" & "/" & VB6.Format(xDate, "MM/YYYY")


        SqlStr = " SELECT * FROM " & vbCrLf & " " & mEmpTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "


        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If


        SqlStr = SqlStr & vbCrLf & " Order By EMP_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmployee.EOF = False Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PBar.Maximum = MainClass.GetMaxRecord(mEmpTable, PubDBCn, SqlStr)
            PBar.Value = PBar.Minimum

            Do While RsEmployee.EOF = False
                mCode = IIf(IsDBNull(RsEmployee.Fields("EMP_CODE").Value), "", RsEmployee.Fields("EMP_CODE").Value)

                If optProcess(0).Checked = True Then
                    mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), -60, "I", "E")
                    mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), -360, "O", "E")
                    mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "E")
                    '                If GetIsHolidays(DateAdd("D", -1, Format(xDate, "DD-MMM-YYYY")), "", mCode, "", "N") = True Then
                    '                    mIsPrevRoundClock = False
                    '                Else
                    mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "E")
                    '                End If
                Else
                    mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), -60, "I", "C")
                    mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), -360, "O", "C")
                    mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "C")
                    '                If GetIsHolidays(DateAdd("D", -1, Format(xDate, "DD-MMM-YYYY")), "", mCode, "", "N") = True Then
                    '                    mIsPrevRoundClock = False
                    '                Else
                    mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "C")
                    '                End If
                End If
                pInDateTime = CDate("00:00:00")
                pOutDateTime = CDate("00:00:00")
                pInTimeExists = "00:00"
                pOutTimeExists = "00:00"
                mTOTHours = 0
                mWorksHours = 0
                mOTHours = 0

                SqlStr = " SELECT TO_CHAR(IN_TIME,'HH24:MI') AS INTIME, TO_CHAR(OUT_TIME,'HH24:MI') AS OUTTIME FROM PAY_CONT_IO_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND  TO_CHAR(ATTN_DATE,'YYYYMMDD') = '" & mDate & "'" & vbCrLf & " AND TRIM(EMP_CODE)='" & Trim(mCode) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGate, ADODB.LockTypeEnum.adLockReadOnly)

                If RsGate.EOF = False Then
                    pInDateTime = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("INTIME").Value), "", RsGate.Fields("INTIME").Value), "HH:MM:SS"))
                    pInDateTimeStr = VB6.Format(IIf(IsDBNull(RsGate.Fields("INTIME").Value), "", RsGate.Fields("INTIME").Value), "YYYYMMDD HH:MM:SS")
                    pOutDateTimeStr = VB6.Format(IIf(IsDBNull(RsGate.Fields("OUTTIME").Value), "", RsGate.Fields("OUTTIME").Value), "YYYYMMDD HH:MM:SS")
                    If pInDateTimeStr <> pOutDateTimeStr Then
                        pOutDateTime = CDate(VB6.Format(IIf(IsDBNull(RsGate.Fields("OUTTIME").Value), "00:00:00", RsGate.Fields("OUTTIME").Value), "HH:MM:SS"))
                    End If
                End If

                '            mGetTime = GetMannualIOTime(mCode, Format(xDate, "DD/MM/YYYY"), "I", mIsRoundClock, mIsPrevRoundClock, GateDBCn)
                '            pInDateTime = Format(mGetTime, "HH:MM:SS")
                '            pInDateTimeStr = Format(mGetTime, "DD/MM/YYYY HH:MM:SS")
                '
                '            mGetTime = GetMannualIOTime(mCode, Format(xDate, "DD/MM/YYYY"), "O", mIsRoundClock, mIsPrevRoundClock, GateDBCn)
                '            pOutDateTimeStr = Format(mGetTime, "DD/MM/YYYY HH:MM:SS")
                '            If pInDateTimeStr = pOutDateTimeStr Then
                '                pOutDateTimeStr = "00:00:00"
                '                pOutDateTime = "00:00:00"
                '            Else
                '                pOutDateTime = Format(mGetTime, "HH:MM:SS")
                '            End If

                If mCode <> "" Then

                    '                If chkReProcess.Value = vbUnchecked Then
                    '                    SqlStr = " SELECT TO_CHAR(IN_TIME,'HH24:MI') AS IN_TIME, TO_CHAR(OUT_TIME,'HH24:MI') AS OUT_TIME FROM " & mAttnTable & " " & vbCrLf _
                    ''                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    ''                            & " AND ATTN_DATE='" & VB6.Format(xDate, "DD-MMM-YYYY") & "'" & vbCrLf _
                    ''                            & " AND EMP_CODE='" & mCode & "'"
                    '                    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsCheckRecd, adLockReadOnly
                    '
                    '                    If RsCheckRecd.EOF = False Then
                    '                        pInTimeExists = IIf(IsNull(RsCheckRecd!IN_TIME), "", RsCheckRecd!IN_TIME)
                    '                        pOutTimeExists = IIf(IsNull(RsCheckRecd!OUT_TIME), "", RsCheckRecd!OUT_TIME)
                    '                        If pInTimeExists = pOutTimeExists Then
                    '                            pOutTimeExists = "00:00"
                    '                        End If
                    '                    Else
                    '                        pInTimeExists = "00:00"
                    '                        pOutTimeExists = "00:00"
                    '                    End If
                    '                Else
                    '                    pInTimeExists = "00:00"
                    '                    pOutTimeExists = "00:00"
                    '                End If
                    SqlStr = " DELETE FROM " & mAttnTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

                    PubDBCn.Execute(SqlStr)
                    '                If pInTimeExists <> "00:00" Then
                    If pInTimeExists = "00:00" Then
                        pInDateTimeStr = VB6.Format(pInDateTime, "HH:MM")
                    Else
                        pInDateTimeStr = pInTimeExists
                    End If

                    If pOutTimeExists = "00:00" Then
                        pOutDateTimeStr = VB6.Format(pOutDateTime, "HH:MM")
                    Else
                        pOutDateTimeStr = pOutTimeExists
                    End If

                    pOutDateTime = CDate(pOutDateTimeStr)
                    pInDateTime = CDate(pInDateTimeStr)

                    If pOutDateTime <> CDate("00:00:00") Then
                        Call CalcTotatHours(pInDateTime, pOutDateTime, mTOTHours, mWorksHours, mOTHours)
                    End If

                    mFMark = ""
                    mSMark = ""

                    SqlStr = " INSERT INTO " & mAttnTable & " ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, " & vbCrLf & " EMP_CODE, ATTN_DATE, " & vbCrLf & " IN_TIME, OUT_TIME, TOT_HOURS," & vbCrLf & " WORKS_HOURS, OT_HOURS," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " IN_TIME_O, OUT_TIME_O, REF_TYPE," & vbCrLf & " ATTN_MARK_F, ATTN_MARK_S " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(xDate)) & ", " & vbCrLf & " '" & mCode & "', '" & VB6.Format(xDate, "DD-MMM-YYYY") & "', " & vbCrLf & " TO_DATE('" & pInDateTimeStr & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & pOutDateTimeStr & "','DD-MON-YYYY HH24:MI'), " & mTOTHours & ", " & vbCrLf & " " & mWorksHours & ", " & mOTHours & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '', '', 'M', " & vbCrLf & " '" & mFMark & "', '" & mSMark & "'" & vbCrLf & " ) "

                    PubDBCn.Execute(SqlStr)
                    '                End If
                End If
                PBar.Value = PBar.Value + 1
                RsEmployee.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        AttnProcessMannual = True
        Exit Function

ErrPart:
        'Resume
        '    MsgInformation "Attendance Process Not Complete, Try Again."
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        AttnProcessMannual = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function CalcTotatHours(ByRef mInDateTime As Date, ByRef mOutDateTime As Date, ByRef mTotDateTimeNum As Double, ByRef mWorkHoursNum As Double, ByRef mOTHoursNum As Double) As Object
        On Error GoTo ERR1
        Dim mBalHours As Date
        Dim mOTFactor As Double
        Dim mHour As Short
        Dim mMin As Short
        Dim mTotDateTime As Date
        Dim mWorkHours As Date
        Dim mOTHours As Date
        Dim TotMin As Double

        'If MainClass.ValidateWithMasterTable(xEmpCode, "EMP_CODE", "OVERTIME_APP", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND OVERTIME_APP='0'") = True Then
        '    mOTApp = False
        'Else
        '    mOTApp = True
        'End If

        If optProcess(0).Checked = True Then
            mOTFactor = IIf(IsDBNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)
        Else
            mOTFactor = IIf(IsDBNull(RsCompany.Fields("CONT_OTFACTOR").Value), 0, RsCompany.Fields("CONT_OTFACTOR").Value)
        End If

        If CDate(mInDateTime) <= CDate(mOutDateTime) Then
            TotMin = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, mInDateTime, mOutDateTime) ''mOutDateTime - mInDateTime
            mHour = Int(TotMin / 60)
            mMin = TotMin - (mHour * 60)
            mTotDateTime = TimeSerial(mHour, mMin, 0)
        Else
            mTotDateTime = CDate(VB6.Format(System.DateTime.FromOADate(System.DateTime.FromOADate(24 - mInDateTime.ToOADate).ToOADate + mOutDateTime.ToOADate), "HH:mm")) ' mInDateTime - (mOutDateTime + 12)
        End If

        If CDate(VB6.Format(mTotDateTime, "hh:mm")) >= CDate(VB6.Format("8:30", "hh:mm")) Then
            mWorkHours = CDate("8:00")
            mBalHours = System.DateTime.FromOADate(CDate(VB6.Format(mTotDateTime, "hh:mm")).ToOADate - CDate(VB6.Format("8:30", "hh:mm")).ToOADate)
        Else
            If CDate(VB6.Format(mTotDateTime, "hh:mm")) < CDate(VB6.Format("8:30", "hh:mm")) And CDate(VB6.Format(mTotDateTime, "hh:mm")) >= CDate(VB6.Format("8:00", "hh:mm")) Then
                mWorkHours = CDate("8:00")
            Else
                mWorkHours = mTotDateTime
            End If
            mBalHours = CDate(VB6.Format("0", "hh:mm"))
        End If

        If CDate(VB6.Format(mBalHours, "hh:mm")) <= System.DateTime.FromOADate(0) Then
            mOTHours = CDate(VB6.Format("0:00", "hh:mm"))
        Else
            mHour = Hour(mBalHours)
            mMin = Int(Minute(mBalHours) / mOTFactor) * mOTFactor
            mBalHours = TimeSerial(mHour, mMin, 0)
        End If

        mOTHours = CDate(VB6.Format(mBalHours, "hh:mm"))



        mTotDateTimeNum = Val(VB.Left(CStr(mTotDateTime), 2)) + System.Math.Round(CDbl(Mid(CStr(mTotDateTime), 4, 2)) / 60, 2)
        mWorkHoursNum = Val(VB.Left(CStr(mWorkHours), 2)) + (CDbl(Mid(CStr(mWorkHours), 4, 2)) / 60)
        mOTHoursNum = Val(VB.Left(CStr(mOTHours), 2)) + (CDbl(Mid(CStr(mOTHours), 4, 2)) / 60)


        Exit Function
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdPopFromFile_Click(sender As Object, e As EventArgs) Handles CmdPopFromFile.Click
        Try
            Dim strFilePath As String = ""
            Dim intflag As Integer
            CommonDialogOpen.FileName = ""

            intflag = CommonDialogOpen.ShowDialog()

            If intflag = 1 Then
                If CommonDialogOpen.FileName <> "" Then
                    strFilePath = CommonDialogOpen.FileName
                    'strfilename = CommonDialogOpen.SafeFileName
                    Call AttnProcessExcel(strFilePath)
                End If
            End If

        Catch ex As Exception

        End Try
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub

    Private Sub TxtCardNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCardNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCardNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
