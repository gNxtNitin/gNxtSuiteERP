Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmEncashProcess
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim mLoanDate As String
    Dim mLoanAmount As Double

    Dim mPFRate As Double
    Dim mPFEPFRate As Double
    Dim mPFPensionRate As Double
    Dim mPFCeiling As Double
    Dim mESICeiling As Double
    Dim mESIRate As Double
    Dim ConWorkDay As Double
    Dim mEmplerPFCont As String
    Private Const ConWorkHour As Short = 8

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdOK.Click

        Dim mDate As String
        If FieldVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Visible = True
        mDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & Month(CDate(lblNewDate.Text)) & "/" & Year(CDate(lblNewDate.Text))

        Call CheckPFRates(CDate(VB6.Format(mDate, "dd/mm/yyyy")))
        Call CheckESIRates(CDate(VB6.Format(mDate, "dd/mm/yyyy")))

        Call EncashProcess()

        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CheckESIRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String

        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mESICeiling = IIf(IsDbNull(RsCeiling.Fields("CEILING").Value), 0, RsCeiling.Fields("CEILING").Value)
            mESIRate = IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
        Else
            mESICeiling = 10000
            mESIRate = 1.75
        End If
        Exit Sub
ERR1:
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
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Function FieldVarification() As Boolean
        FieldVarification = True

        If OptParti.Checked = True Then
            If txtCardNo.Text = "" Then
                MsgBox("CardNo. Not Selected.")
                FieldVarification = False
                txtCardNo.Focus()
                Exit Function
            End If
        End If

        If Not IsDate(txtAsOn.Text) Then
            MsgBox("Invalid Date")
            FieldVarification = False
            txtAsOn.Focus()
            Exit Function
        End If

        If Year(CDate(txtAsOn.Text)) <> Val(txtMonth.Text) Then
            MsgBox("Invalid Date of this Year.")
            FieldVarification = False
            txtAsOn.Focus()
            Exit Function
        End If
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""

        SqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtCardNo.Text = AcName1
            TxtName.Text = AcName
            TxtCardNo_Validating(TxtCardNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdUnProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUnProcess.Click

        Dim mCurrentFYNo As Integer
        Dim mDate As String
        If FieldVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Visible = False
        mDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & Month(CDate(lblNewDate.Text)) & "/" & Year(CDate(lblNewDate.Text))
        mCurrentFYNo = GetCurrentFYNo(PubDBCn, mDate)

        If mCurrentFYNo = -1 Then
            Exit Sub
        End If

        Call EncashUnProcess()

        cmdUnProcess.Enabled = False
        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub FrmEncashProcess_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If lblBookType.Text = "E" Then
            Me.Text = "Leave Encashment Process"
            chkDeposit.Enabled = False
            chkDeposit.CheckState = System.Windows.Forms.CheckState.Checked
        ElseIf lblBookType.Text = "P" Then
            Me.Text = "Leave Encashment (Arrear) Process"
        ElseIf lblBookType.Text = "I" Then
            Me.Text = "Leave Encashment Process (For Insurance)"
            chkDeposit.Enabled = False
            chkDeposit.CheckState = System.Windows.Forms.CheckState.Unchecked
        Else
            Me.Text = "CPL Payment Process"
        End If
    End Sub

    Private Sub FrmEncashProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Me.Left = 0
        Me.Top = 0
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(3855)
        Me.Width = VB6.TwipsToPixelsX(5475)

        txtMonth.Enabled = False
        '    TxtYear.Enabled = False

        lblNewDate.Text = CStr(RunDate)
        txtMonth.Text = CStr(Year(RunDate))

        txtAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        OptLeave(0).Checked = True

        If PubATHUSER = True Then
            cmdUnProcess.Enabled = True
        Else
            cmdUnProcess.Enabled = False
        End If

        cboEmpType.Items.Clear()
        cboEmpType.Items.Add("ALL")
        cboEmpType.Items.Add("1 : Staff")
        cboEmpType.Items.Add("2 : Workers")
        cboEmpType.SelectedIndex = 0

        optAll.Checked = True
        HideUnHide(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmEncashProcess_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub TxtCardNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCardNo.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtCardNo.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCardNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtCardNo.Text = "" Then GoTo EventExitSub
        txtCardNo.Text = VB6.Format(txtCardNo.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtCardNo.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("CardNo. Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        Else
            TxtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    'Private Sub UpDMonth_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.DownClick
    '    SetNewDate(-1)
    '    txtMonth.Text = CStr(Year(CDate(lblNewDate.Text)))
    'End Sub
    Sub SetNewDate(ByRef prmSpinDirection As Short)
        lblNewDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, prmSpinDirection, CDate(lblNewDate.Text)))
    End Sub
    'Private Sub UpDMonth_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.UpClick
    '    SetNewDate(1)
    '    txtMonth.Text = CStr(Year(CDate(lblNewDate.Text)))
    'End Sub
    Private Sub HideUnHide(ByRef mCheck As Boolean)
        txtCardNo.Enabled = mCheck
        cmdSearch.Enabled = mCheck
    End Sub

    Private Sub EncashProcess()

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim RsEmployee As ADODB.Recordset
        Dim SqlStr As String = ""
        Static mDOJ As String
        Static mDOL As String
        Dim mMonth As Integer
        Dim mSalDate As String
        Dim mCalcArrear As String
        Dim mAddDays As Double
        Dim mDeptDesc As String
        Dim mDesgDesc As String
        Dim mSalary As Double
        Dim mMonth1 As String
        Dim mPaidSalary As String
        Dim mESIPayable As Double
        Dim mIsStaff As Boolean

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'PBar.Min = 0

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mMonth = Year(CDate(lblNewDate.Text))

        SqlStr = ""

        If Trim(txtAsOn.Text) = "" Then
            mSalDate = "31/12/" & mMonth
            mDOJ = "01/01/" & mMonth
            mDOL = "31/12/" & mMonth
        Else
            mSalDate = VB6.Format(txtAsOn.Text, "DD/MM/YYYY")
            mDOJ = "01/01/" & mMonth
            mDOL = VB6.Format(txtAsOn.Text, "DD/MM/YYYY")
        End If

        ''Check Validation
        SqlStr = "Select COUNT(1) AS CNTREC From PAY_ENCASH_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR>" & mMonth & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

        If lblBookType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(PAID_MONTH,'YYYYMM')='" & VB6.Format(txtAsOn.Text, "YYYYMM") & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C'"

        If Trim(cboEmpType.Text) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpType.Text, 1) & "' "
        End If


        SqlStr = SqlStr & vbCrLf & ")"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSalTRN.EOF = False Then
            If RsSalTRN.Fields("CNTREC").Value > 0 Then
                MsgInformation("You Cann't Process back leave Encashment.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Exit Sub
            End If
        End If

        ''---------------------------------------AND DESG.DESG_CAT<>'D' as per discussion with Abhishek
        SqlStr = " SELECT EMP.* FROM " & vbCrLf & " PAY_EMPLOYEE_MST EMP, PAY_DESG_MST DESG" & vbCrLf & " WHERE EMP.COMPANY_CODE=DESG.COMPANY_CODE" & vbCrLf & " AND TRIM(EMP.EMP_DESG_CODE)=TRIM(DESG.DESG_CODE)" & vbCrLf & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.EMP_STOP_SALARY='N'  " & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " AND EMP_CATG<>'C'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
        End If

        If Trim(cboEmpType.Text) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpType.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & " Order By EMP_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)


        SqlStr = "Select * From PAY_ENCASH_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & mMonth & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

        If lblBookType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(PAID_MONTH,'YYYYMM')='" & VB6.Format(txtAsOn.Text, "YYYYMM") & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C'"

        If Trim(cboEmpType.Text) <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpType.Text, 1) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & ")"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSalTRN.EOF = False Then
            SqlStr = CStr(MsgBox("Leave Encashment Already Processed For This Year ... " & vbNewLine & vbNewLine & "Want To Reprocess ...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2))
            If SqlStr = CStr(MsgBoxResult.Yes) Then
                SqlStr = "DELETE FROM PAY_ENCASH_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & mMonth & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

                If lblBookType.Text = "C" Then
                    SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(PAID_MONTH,'YYYYMM')='" & VB6.Format(txtAsOn.Text, "YYYYMM") & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C'"

                If Trim(cboEmpType.Text) <> "ALL" Then
                    SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpType.Text, 1) & "' "
                End If


                SqlStr = SqlStr & vbCrLf & ")"

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

                mMonth1 = UCase(VB6.Format(mSalDate, "MMM-YYYY"))

                ''TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth1 & "' 'SK 28-03-2006   ''
                SqlStr = "DELETE FROM PAY_PFESI_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & mMonth & "" & vbCrLf & " AND ISARREAR='" & lblBookType.Text & "'"

                If lblBookType.Text = "C" Then
                    SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(txtAsOn.Text, "YYYYMM") & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C'"

                If Trim(cboEmpType.Text) <> "ALL" Then
                    SqlStr = SqlStr & vbCrLf & "AND EMP_CAT_TYPE='" & VB.Left(cboEmpType.Text, 1) & "' "
                End If


                SqlStr = SqlStr & vbCrLf & ")"
                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

            Else
                PBar.Visible = False
                Exit Sub
            End If
        End If


        If RsEmployee.EOF = False Then
            PBar.Visible = True

            'PBar.Min = 0
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
            End If

            'PBar.Max = MainClass.GetMaxRecord("PAY_EMPLOYEE_MST", PubDBCn, SqlStr)
            'PBar.Value = PBar.Min
            Do While Not RsEmployee.EOF

                mIsStaff = True
                If MainClass.ValidateWithMasterTable(RsEmployee.Fields("EMP_CODE"), "EMP_CODE", "EMP_CAT_TYPE ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE =2") = True Then
                    mIsStaff = False
                End If

                mSalary = CDbl(VB6.Format(CalcSal(RsEmployee.Fields("EMP_CODE").Value, RsEmployee.Fields("EMP_DOJ").Value, IIf(IsDbNull(RsEmployee.Fields("EMP_LEAVE_DATE").Value), mDOL, RsEmployee.Fields("EMP_LEAVE_DATE").Value), mSalDate, mIsStaff), "0.00"))

                If lblBookType.Text = "C" Then
                    mESIPayable = CDbl(VB6.Format(CalcESIPayableSal(RsEmployee.Fields("EMP_CODE").Value, RsEmployee.Fields("EMP_DOJ").Value, IIf(IsDbNull(RsEmployee.Fields("EMP_LEAVE_DATE").Value), mDOL, RsEmployee.Fields("EMP_LEAVE_DATE").Value), mSalDate), "0.00"))
                Else
                    mESIPayable = 0
                End If
                If lblBookType.Text = "P" Then
                    mPaidSalary = VB6.Format(GetPaidELSal(RsEmployee.Fields("EMP_CODE").Value, mMonth), "0.00")
                    mSalary = mSalary - CDbl(mPaidSalary)
                End If

                If mSalary > 0 Then
                    If UpdateEncashTrn(RsEmployee.Fields("EMP_CODE").Value, mSalary, mMonth, mSalDate, mESIPayable, mIsStaff) = False Then GoTo ErrPart
                End If

                RsEmployee.MoveNext()
                PBar.Value = PBar.Value + 1
            Loop

            MsgBox("Leave Encashment Process Complete")
        Else
            MsgBox("No Record Found For Processing.")
        End If

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

ErrPart:
        'Resume
        MsgInformation("Leave Encashment Process Not Complete, Try Again.")
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub EncashUnProcess()

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim RsEmployee As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mMonth As Integer

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'PBar.Min = 0

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mMonth = Year(CDate(lblNewDate.Text))

        SqlStr = ""

        ''Check Validation
        SqlStr = "Select COUNT(1) AS CNTREC From PAY_ENCASH_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR>" & mMonth & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

        If lblBookType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(PAID_MONTH,'YYYYMM')='" & VB6.Format(txtAsOn.Text, "YYYYMM") & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C')"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSalTRN.EOF = False Then
            If RsSalTRN.Fields("CNTREC").Value > 0 Then
                MsgInformation("You Cann't UnProcess back leave Encashment.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                PubDBCn.RollbackTrans()
                Exit Sub
            End If
        End If


        SqlStr = "DELETE FROM PAY_ENCASH_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & mMonth & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C')"

        If lblBookType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(PAID_MONTH,'YYYYMM')='" & VB6.Format(txtAsOn.Text, "YYYYMM") & "'"
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
        End If

        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM PAY_PFESI_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & mMonth & "" & vbCrLf & " AND ISARREAR='" & lblBookType.Text & "'" & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C')"

        If lblBookType.Text = "C" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(txtAsOn.Text, "YYYYMM") & "'"
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtCardNo.Text & "'"
        End If

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

ErrPart:
        'Resume
        MsgInformation("Leave Encashment Process Not Complete, Try Again.")
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function GetVarAmount(ByRef xCode As String, ByRef pYear As Integer) As Double

        On Error GoTo ErrPart
        Dim RsCalcAddDays As ADODB.Recordset
        Dim SqlStr As String = ""

        GetVarAmount = 0
        SqlStr = "SELECT SUM(AMOUNT) As AMOUNT1 FROM PAY_MONTHLY_TRN WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & pYear & " " & vbCrLf & " AND EMP_CODE= '" & xCode & "'" & vbCrLf & " AND SAL_FLAG='" & lblBookType.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCalcAddDays, ADODB.LockTypeEnum.adLockOptimistic)

        If RsCalcAddDays.EOF = False Then
            GetVarAmount = IIf(IsDbNull(RsCalcAddDays.Fields("AMOUNT1").Value), 0, RsCalcAddDays.Fields("AMOUNT1").Value)
        End If
        Exit Function
ErrPart:
        GetVarAmount = 0
    End Function
    Private Function UpdateEncashTrn(ByRef mCode As String, ByRef xBSalary As Double, ByRef xYear As Integer, ByRef pRunDate As String, ByRef mESIPayable As Double, ByRef mIsStaff As Boolean) As Boolean

        On Error GoTo UpDateSalTrnErr
        Dim SqlStr As String = ""
        Dim mWDays As Double
        Dim mTotalLeavesBal As Double
        Dim mPaidLeaves As Double
        Dim mDepositLeave As Double
        Dim mPaidDays As Double
        Dim mDeductAmount As Double
        Dim mNetAmount As Double
        'Dim mESIPayableAmount As Double
        Dim mGrossAmount As Double
        Dim mPFAmount As Double
        Dim mPensionFund As Double
        Dim mEmpCont As Double
        Dim mPayablePensionWages As Double
        Dim mNewPFRate As Double
        Dim pBalEL As Double
        Dim pBalCL As Double
        Dim pBalSL As Double
        Dim pBalCPL As Double
        Dim mEmployer_PF As Double
        Dim pAsOn As String
        Dim mVPFAmount As Double
        Dim mVPFRate As Double
        'Dim mESIRate As Double
        Dim mESIAmount As Double
        'Dim mLeaveDate As String
        'Dim mEmpLeave As Boolean


        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "IS_LEAVE_ENCHASE_PAYABLE ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_LEAVE_ENCHASE_PAYABLE ='N'") = True Then
            UpdateEncashTrn = True
            Exit Function
        End If

        '    If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_LEAVE_DATE ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mLeaveDate = MasterNo
        '    End If


        pAsOn = VB6.Format(txtAsOn.Text, "DD/MM/YYYY")
        '    If mCode = "000123" Then MsgBox "OK"
        mWDays = CalcWDays(mCode, pAsOn)
        mTotalLeavesBal = CalcBalLeaves(mCode, pAsOn, PubDBCn, pBalEL, pBalCL, pBalSL, pBalCPL)

        If OptLeave(1).Checked = True Then ''Only EL to be Process
            mTotalLeavesBal = pBalEL
        Else
            If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                mTotalLeavesBal = (pBalEL + IIf(pBalSL > 21, pBalSL - 21, 0)) - pBalCPL
            Else
                mTotalLeavesBal = mTotalLeavesBal '' - pBalCPL
            End If
        End If

        If lblBookType.Text = "E" Or lblBookType.Text = "P" Or lblBookType.Text = "I" Then
            If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                If mIsStaff = True Then
                    mDepositLeave = IIf(IsDbNull(RsCompany.Fields("DEPOSITLEAVE").Value), 0, RsCompany.Fields("DEPOSITLEAVE").Value)
                Else
                    mDepositLeave = IIf(IsDbNull(RsCompany.Fields("DEPOSITLEAVE_WK").Value), 0, RsCompany.Fields("DEPOSITLEAVE_WK").Value)
                End If
            Else
                mDepositLeave = IIf(IsDbNull(RsCompany.Fields("DEPOSITLEAVE").Value), 0, RsCompany.Fields("DEPOSITLEAVE").Value)
            End If
            mDepositLeave = IIf(chkDeposit.CheckState = System.Windows.Forms.CheckState.Unchecked, 0, mDepositLeave)
        Else
            mPaidLeaves = GetCPLPaid(mCode, pAsOn, PubDBCn)
            mDepositLeave = pBalCPL - mPaidLeaves
        End If

        If lblBookType.Text = "E" Or lblBookType.Text = "P" Or lblBookType.Text = "I" Then
            '        If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
            '            If mDepositLeave > pBalEL Then
            '                mPaidLeaves = 0
            '            Else
            '                mPaidLeaves = pBalEL - mDepositLeave
            '            End If
            '
            '            mPaidLeaves = mPaidLeaves + pBalCL + pBalSL
            '            mPaidLeaves = IIf(mPaidLeaves < 0, 0, mPaidLeaves)
            '
            '            mTotalLeavesBal = mTotalLeavesBal - pBalCPL
            ''            mTotalLeavesBal = IIf(mTotalLeavesBal < 0, 0, mTotalLeavesBal)
            '        Else
            If CDbl(PubPAYYEAR) = 2018 And RsCompany.Fields("COMPANY_CODE").Value = 35 And mCode = "002022" Then
                mPaidLeaves = 130

            Else
                mPaidLeaves = mTotalLeavesBal - mDepositLeave
                mPaidLeaves = IIf(mPaidLeaves < 0, 0, mPaidLeaves)
            End If
            '        End If
        Else
            If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                mTotalLeavesBal = pBalCPL
                mPaidLeaves = IIf(mPaidLeaves < 0, 0, mPaidLeaves)
            Else
                mPaidLeaves = 0
                mPaidLeaves = IIf(mPaidLeaves < 0, 0, mPaidLeaves)
            End If
        End If


        mPaidDays = IIf(IsDbNull(RsCompany.Fields("LEAVEPAIDDAYS").Value), 0, RsCompany.Fields("LEAVEPAIDDAYS").Value)
        If lblBookType.Text = "P" Then
            mDeductAmount = 0
        Else
            mDeductAmount = GetVarAmount(mCode, Year(CDate(pAsOn)))
        End If

        mGrossAmount = System.Math.Round(xBSalary * mPaidLeaves / mPaidDays, 0)

        '    If CVDate(pAsOn) < CVDate("01/03/2008") Then
        '        mNewPFRate = GetPFESIRate(mCode, pAsOn, ConPF)
        '        mESIRate = GetPFESIRate(mCode, pAsOn, ConESI)
        '        mPFAmount = mGrossAmount * mPFRate * 0.01
        '        mPFAmount = Round(mPFAmount, 0)
        '
        '        mVPFRate = GetPFESIRate(mCode, pAsOn, ConVPFAllw)
        '        mVPFAmount = mGrossAmount * mVPFRate * 0.01
        '        mVPFAmount = Round(mVPFAmount, 0)
        '    Else
        mESIRate = GetPFESIRate(mCode, pAsOn, ConESI)
        '    End If

        If lblBookType.Text = "C" Then
            '        mESIAmount = mGrossAmount * mESIRate * 0.01
            '        mESIPayableAmount = xBSalary
            mESIAmount = IIf(mESIPayable > mESICeiling, 0, mGrossAmount) * mESIRate / 100
            mESIAmount = System.Math.Round(mESIAmount, 0)
        Else
            mESIAmount = 0
        End If

        If mEmplerPFCont = "B" Then
            mEmployer_PF = mPFAmount
        Else
            mEmployer_PF = IIf(mGrossAmount > mPFCeiling, mPFCeiling, mGrossAmount) * mPFRate / 100
            mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
        End If

        If CDate(txtAsOn.Text) > CDate("31/12/2007") Then
            mNewPFRate = 0
            mPFAmount = 0
            mVPFAmount = 0
            mEmployer_PF = 0
        End If

        '    mEmployer_PF = mPFAmount

        If mNewPFRate <> mPFRate Then
            mPFAmount = mPFAmount * mNewPFRate / mPFRate
        End If
        mPFAmount = System.Math.Round(mPFAmount, 0)


        mNetAmount = mGrossAmount - mDeductAmount - mPFAmount - mVPFAmount - mESIAmount

        SqlStr = " INSERT INTO PAY_ENCASH_TRN ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE, " & vbCrLf & " BASICSALARY, WDAYS, TOT_LEAVES, " & vbCrLf & " PAID_LEAVES, PAID_DAYS, GROSS_AMOUNT,  " & vbCrLf & " DED_AMOUNT, PF_AMOUNT, NET_AMOUNT, BOOKTYPE, VPFAMOUNT, VPFRATE, " & vbCrLf & " ADDUSER, ADDDATE, ESI_AMOUNT, PAID_MONTH ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",  " & xYear & " , '" & mCode & "', " & vbCrLf & " " & xBSalary & ", " & mWDays & ", " & mTotalLeavesBal & ", " & vbCrLf & " " & mPaidLeaves & ", " & mPaidDays & ", " & mGrossAmount & ", " & vbCrLf & " " & mDeductAmount & ", " & mPFAmount & ", " & mNetAmount & ",'" & lblBookType.Text & "', " & vbCrLf & " " & mVPFAmount & ", " & mVPFRate & ", " & vbCrLf & " '',''," & mESIAmount & ",TO_DATE('" & VB6.Format(txtAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        PubDBCn.Execute(SqlStr)

        If lblBookType.Text = "C" Then

        Else
            If VB6.Format(txtAsOn.Text, "DD/MM/YYYY") <> VB6.Format("31/12/" & Val(txtMonth.Text), "DD/MM/YYYY") Then GoTo NextRec
        End If

        'PF ESI Calc....
        If lblBookType.Text = "I" Then
            If mGrossAmount > 0 Then
                If mPFAmount = 0 Then
                    mPensionFund = 0
                    mEmpCont = 0
                    mPayablePensionWages = 0
                    mPensionFund = 0
                Else
                    If mGrossAmount <= mPFCeiling Then
                        mPensionFund = mGrossAmount * mPFPensionRate / 100
                        mPensionFund = System.Math.Round(mPensionFund, 0)
                        mEmpCont = mEmployer_PF - mPensionFund ''mPFAmount
                    Else
                        mPensionFund = mPFCeiling * mPFPensionRate / 100
                        mPensionFund = System.Math.Round(mPensionFund, 0)
                        mEmpCont = mEmployer_PF - mPensionFund ''mPFAmount
                    End If

                    mPayablePensionWages = IIf(mPFCeiling <= mGrossAmount, mPFCeiling, mGrossAmount)
                    mPayablePensionWages = CDbl(VB6.Format(mPayablePensionWages, "0"))

                    mPensionFund = IIf(mPensionFund = 0, 0, System.Math.Round(mPensionFund, 0))
                    mEmpCont = IIf(mEmpCont = 0, 0, System.Math.Round(mEmpCont, 0))
                    mPayablePensionWages = IIf(mPayablePensionWages = 0, 0, System.Math.Round(mPayablePensionWages, 0))
                End If


                SqlStr = " INSERT INTO PAY_PFESI_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BasicSalary, PFABLEAMT, PENSIONWAGES, PFAMT, PFRate ,  " & vbCrLf & " ESIABLEAMT , ESIAMT, ESIRATE, PENSIONFUND, EPFAMT ,  " & vbCrLf & " LEAVEWOP , WDAYS, ISARREAR, VPFAMT, VPFRATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & xYear & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(pRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mGrossAmount & ", " & mGrossAmount & "," & mPayablePensionWages & "," & mPFAmount & "," & mPFRate & ", " & vbCrLf & " " & mGrossAmount & "," & mESIAmount & "," & mESIRate & ", " & vbCrLf & " " & mPensionFund & ", " & mEmpCont & ",0," & vbCrLf & " " & mPaidLeaves & ", " & vbCrLf & " '" & lblBookType.Text & "', " & vbCrLf & " " & mVPFAmount & ", " & mVPFRate & " ) "

                PubDBCn.Execute(SqlStr)
            End If
        End If
NextRec:
        UpdateEncashTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdateEncashTrn = False
    End Function


    Private Function GetPFESIRate(ByRef mCode As String, ByRef pRunDate As String, ByRef pDeductType As Integer) As Double


        On Error GoTo UpDateSalTrnErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetPFESIRate = 0

        SqlStr = " SELECT SALARYDEF.*," & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE =" & pDeductType & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(pRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If Not RsTemp.EOF Then
            GetPFESIRate = IIf(IsDbNull(RsTemp.Fields("PERCENTAGE").Value), 0, RsTemp.Fields("PERCENTAGE").Value)
        End If

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        GetPFESIRate = 0
    End Function
    Private Function CalcSal(ByRef mCode As String, ByRef xDOJ As Date, ByRef xDOL As Date, ByRef xSalDate As String, ByRef mIsStaff As Boolean) As Double

        On Error GoTo ErrPart
        Dim RsEmpSal As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mYM As String
        Dim mEarn As Double
        Dim mDeduct As Double
        Dim mNetPay As Double
        Dim mBasicSalary As Double
        Dim mTotEarn As Double
        Dim mTotDeduct As Double
        Dim TotalPay As Double
        Dim mPeriod As Double

        SqlStr = " SELECT SalaryDef.*,INCLUDEDLEAVEENCASH, ADDDEDUCT,ISSALPART " & vbCrLf & " FROM PAY_SALARYDEF_MST SalaryDef,PAY_SALARYHEAD_MST ADD_DEDUCT " & vbCrLf & " WHERE " & vbCrLf & " SalaryDef.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE " & vbCrLf & " AND SalaryDef.ADD_DEDUCTCode=ADD_DEDUCT.CODE " & vbCrLf & " AND SalaryDef.SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST " & vbCrLf & " WHERE SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_Code='" & mCode & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf & " AND SalaryDef.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SalaryDef.EMP_Code='" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)


        If RsEmpSal.EOF = False Then
            mBasicSalary = RsEmpSal.Fields("BASICSALARY").Value
            Do While Not RsEmpSal.EOF
                If RsEmpSal.Fields("ADDDEDUCT").Value = ConEarning Then
                    If CDate(xSalDate) < CDate("13/09/2009") Then

                    Else
                        If RsEmpSal.Fields("INCLUDEDLEAVEENCASH").Value = "Y" Then
                            If RsEmpSal.Fields("ISSALPART").Value = "Y" Then
                                mBasicSalary = mBasicSalary + RsEmpSal.Fields("Amount").Value
                            Else
                                mEarn = mEarn + RsEmpSal.Fields("Amount").Value
                            End If
                        End If
                    End If
                ElseIf RsEmpSal.Fields("ADDDEDUCT").Value = ConDeduct Then
                    If RsEmpSal.Fields("INCLUDEDLEAVEENCASH").Value = "Y" Then
                        mDeduct = mDeduct + RsEmpSal.Fields("Amount").Value
                    End If
                End If

                RsEmpSal.MoveNext()
                mTotEarn = mTotEarn + mEarn
                mTotDeduct = mTotDeduct + mDeduct

                mEarn = 0
                mDeduct = 0
            Loop
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            CalcSal = mBasicSalary
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
            If mIsStaff = True Then
                CalcSal = mBasicSalary + (mTotEarn - mTotDeduct)
            Else
                CalcSal = mBasicSalary
            End If
        Else
            CalcSal = mBasicSalary + (mTotEarn - mTotDeduct)
        End If


        Exit Function
ErrPart:
        CalcSal = 0
    End Function

    Private Function CalcESIPayableSal(ByRef mCode As String, ByRef xDOJ As Date, ByRef xDOL As Date, ByRef xSalDate As String) As Double

        On Error GoTo ErrPart
        Dim RsEmpSal As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mYM As String
        Dim mEarn As Double
        Dim mDeduct As Double
        Dim mNetPay As Double
        Dim mBasicSalary As Double
        Dim mTotEarn As Double
        Dim mTotDeduct As Double
        Dim TotalPay As Double
        Dim mPeriod As Double

        SqlStr = " SELECT SalaryDef.*,INCLUDEDESI, ADDDEDUCT " & vbCrLf & " FROM PAY_SALARYDEF_MST SalaryDef,PAY_SALARYHEAD_MST ADD_DEDUCT " & vbCrLf & " WHERE " & vbCrLf & " SalaryDef.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE " & vbCrLf & " AND SalaryDef.ADD_DEDUCTCode=ADD_DEDUCT.CODE " & vbCrLf & " AND SalaryDef.SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST " & vbCrLf & " WHERE SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_Code='" & mCode & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf & " AND SalaryDef.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SalaryDef.EMP_Code='" & mCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)


        If RsEmpSal.EOF = False Then
            mBasicSalary = RsEmpSal.Fields("BASICSALARY").Value
            Do While Not RsEmpSal.EOF
                If RsEmpSal.Fields("ADDDEDUCT").Value = ConEarning Then
                    If RsEmpSal.Fields("INCLUDEDESI").Value = "Y" Then
                        mEarn = mEarn + RsEmpSal.Fields("Amount").Value
                    End If
                ElseIf RsEmpSal.Fields("ADDDEDUCT").Value = ConDeduct Then
                    If RsEmpSal.Fields("INCLUDEDESI").Value = "Y" Then
                        mDeduct = mDeduct + RsEmpSal.Fields("Amount").Value
                    End If
                End If

                RsEmpSal.MoveNext()
                mTotEarn = mTotEarn + mEarn
                mTotDeduct = mTotDeduct + mDeduct

                mEarn = 0
                mDeduct = 0
            Loop
        End If
        CalcESIPayableSal = mBasicSalary + (mTotEarn - mTotDeduct)

        Exit Function
ErrPart:
        CalcESIPayableSal = 0
    End Function
    Private Function GetPaidELSal(ByRef mCode As String, ByRef xMonth As Integer) As Double

        On Error GoTo ErrPart
        Dim RsEmpSal As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mYM As String
        Dim mEarn As Double
        Dim mDeduct As Double
        Dim mNetPay As Double
        Dim mBasicSalary As Double
        Dim mTotEarn As Double
        Dim mTotDeduct As Double
        Dim TotalPay As Double
        Dim mPeriod As Double

        SqlStr = " SELECT BASICSALARY " & vbCrLf & " FROM PAY_ENCASH_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_Code='" & mCode & "'" & vbCrLf & " AND BOOKTYPE = 'E'" & vbCrLf & " AND PAYYEAR=" & xMonth & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)


        If RsEmpSal.EOF = False Then
            GetPaidELSal = IIf(IsDbNull(RsEmpSal.Fields("BASICSALARY").Value), 0, RsEmpSal.Fields("BASICSALARY").Value)
        End If

        Exit Function
ErrPart:
        GetPaidELSal = 0
    End Function
    Private Function CalcWDays(ByRef pEmpCode As String, ByRef pRunDate As String) As Double

        On Error GoTo ErrPart
        Dim RsBalEL As ADODB.Recordset
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim xRunDate As String
        Dim mTotalLeaves As Double
        Dim mTotalHoliDays As Double
        Dim mTotalRunningDays As Double
        Dim mDOJ As String
        Dim mDOL As String
        Dim mStartingDate As String
        Dim mEndingDate As String
        Dim SqlStr As String = ""

        CalcWDays = 0

        xRunDate = VB6.Format(pRunDate, "DD/MM/YYYY")

        mStartingDate = "01/01/" & Year(CDate(xRunDate))
        mEndingDate = MainClass.LastDay(Month(CDate(xRunDate)), Year(CDate(xRunDate))) & "/" & VB6.Format(xRunDate, "MM/YYYY")
        '    mEndingDate = "31/12/" & Year(xRunDate)

        SqlStr = " SELECT EMP_DOJ, EMP_LEAVE_DATE,EMP_CATG " & vbCrLf & " FROM PAY_EMPLOYEE_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & pEmpCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            mDOJ = IIf(IsDbNull(RsEmp.Fields("EMP_DOJ").Value), "", RsEmp.Fields("EMP_DOJ").Value)
            mDOL = IIf(IsDbNull(RsEmp.Fields("EMP_LEAVE_DATE").Value), "", RsEmp.Fields("EMP_LEAVE_DATE").Value)
        End If

        If mDOJ = "" Then

        ElseIf CDate(mStartingDate) < CDate(mDOJ) Then
            mStartingDate = mDOJ
        End If

        If mDOL = "" Then

        ElseIf CDate(mEndingDate) > CDate(mDOL) Then
            mEndingDate = mDOL
        End If

        mTotalRunningDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartingDate), CDate(mEndingDate)) + 1

        SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(xRunDate)) & " " & vbCrLf & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsBalEL.EOF = False Then
            Do While Not RsBalEL.EOF
                If RsBalEL.Fields("FIRSTHALF").Value <> -1 Then
                    If RsBalEL.Fields("FIRSTHALF").Value = CPLEARN Or RsBalEL.Fields("FIRSTHALF").Value = CPLAVAIL Or RsBalEL.Fields("FIRSTHALF").Value = PRESENT Then

                    ElseIf RsBalEL.Fields("FIRSTHALF").Value = SUNDAY Or RsBalEL.Fields("FIRSTHALF").Value = HOLIDAY Then
                        mTotalHoliDays = mTotalHoliDays + 0.5
                    Else
                        mFHalf = mFHalf + 0.5
                    End If
                End If

                If RsBalEL.Fields("SECONDHALF").Value <> -1 Then
                    If RsBalEL.Fields("SECONDHALF").Value = CPLEARN Or RsBalEL.Fields("SECONDHALF").Value = CPLAVAIL Or RsBalEL.Fields("FIRSTHALF").Value = PRESENT Then

                    ElseIf RsBalEL.Fields("SECONDHALF").Value = SUNDAY Or RsBalEL.Fields("SECONDHALF").Value = HOLIDAY Then
                        mTotalHoliDays = mTotalHoliDays + 0.5
                    Else
                        mSHalf = mSHalf + 0.5
                    End If
                End If
                RsBalEL.MoveNext()
            Loop
        End If

        mTotalLeaves = mFHalf + mSHalf

        If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
            If Year(CDate(xRunDate)) < 2006 Then
                SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND HOLIDAY_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)

                If RsBalEL.EOF = False Then
                    mTotalHoliDays = IIf(IsDbNull(RsBalEL.Fields("HOLIDAYCNT").Value), 0, RsBalEL.Fields("HOLIDAYCNT").Value)
                End If
            End If
        Else
            If Year(CDate(xRunDate)) <= 2006 Then
                SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND HOLIDAY_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)

                If RsBalEL.EOF = False Then
                    mTotalHoliDays = IIf(IsDbNull(RsBalEL.Fields("HOLIDAYCNT").Value), 0, RsBalEL.Fields("HOLIDAYCNT").Value)
                End If
            End If
        End If

        CalcWDays = mTotalRunningDays - mTotalLeaves - mTotalHoliDays

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
