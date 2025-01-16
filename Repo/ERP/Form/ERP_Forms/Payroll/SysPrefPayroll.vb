Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSysPrefPayroll
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim XRIGHT As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Private Sub chkOTin_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOTin.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdcancel.Click
        Me.hide()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        If FieldVerification = False Then Exit Sub
        If Update1 = True Then CmdSave.Enabled = False
    End Sub
    Private Sub frmSysPrefPayroll_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(6795)
        Me.Width = VB6.TwipsToPixelsX(10350)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        SetMaxLength()
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        If XRIGHT <> "" Then MODIFYMode = True
        Show1()
    End Sub
    Sub SetMaxLength()
        txtOTStartMin.MaxLength = 2
        txtOTMin.MaxLength = 2
        txtContOTMin.MaxLength = 2
        txtPFAdminPer.MaxLength = 5
        txtPFAdminPer_22.MaxLength = 5
        txtPFEDLIPer.MaxLength = 5
        txtEmployerESIPer.MaxLength = 5
        txtWelfarePer.MaxLength = 5
        txtLateEntryMin.MaxLength = 2
        txtSortLeaveMin.MaxLength = 3

        txtLateEntryDays.MaxLength = 2
        txtSortLeaveDays.MaxLength = 2
    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        ShowOT()
        CmdSave.Enabled = False
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowOT()
        On Error GoTo ERR1
        Dim mSqlStr As String
        Dim mWeeklyOffType As String
        Dim mBonusType As String
        Dim mPunchingFrom As String

        chkOTin.CheckState = IIf(RsCompany.Fields("PRINTOTINPAYSLIP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        txtOTStartMin.Text = IIf(IsDBNull(RsCompany.Fields("OT_START_MIN").Value), 0, RsCompany.Fields("OT_START_MIN").Value)
        txtOTMin.Text = IIf(IsDBNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)
        txtContOTMin.Text = IIf(IsDbNull(RsCompany.Fields("CONT_OTFACTOR").Value), 0, RsCompany.Fields("CONT_OTFACTOR").Value)
        TxtBonusLimit.Text = IIf(IsDbNull(RsCompany.Fields("BonusLimit").Value), 0, RsCompany.Fields("BonusLimit").Value)

        txtBonusCeilingAmt.Text = IIf(IsDbNull(RsCompany.Fields("BONUS_CEIL_AMT").Value), 0, RsCompany.Fields("BONUS_CEIL_AMT").Value)

        mBonusType = IIf(IsDbNull(RsCompany.Fields("BONUS_TYPE").Value), "C", RsCompany.Fields("BONUS_TYPE").Value)

        If mBonusType = "B" Then
            optBonus(0).Checked = True
        Else
            optBonus(1).Checked = True
        End If

        txtLateEntryMin.Text = IIf(IsDbNull(RsCompany.Fields("LATE_ENTRY").Value), 0, RsCompany.Fields("LATE_ENTRY").Value)
        txtSortLeaveMin.Text = IIf(IsDbNull(RsCompany.Fields("SHORT_LEAVE").Value), 0, RsCompany.Fields("SHORT_LEAVE").Value)

        txtLateEntryDays.Text = IIf(IsDbNull(RsCompany.Fields("LATE_ENTRY_DAYS").Value), 0, RsCompany.Fields("LATE_ENTRY_DAYS").Value)
        txtSortLeaveDays.Text = IIf(IsDbNull(RsCompany.Fields("SHORT_LEAVE_DAYS").Value), 0, RsCompany.Fields("SHORT_LEAVE_DAYS").Value)

        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If IsDbNull(RsCompany.Fields("POST_NOTICEPAY").Value) Then
            txtNoticePay.Text = ""
        ElseIf MainClass.ValidateWithMasterTable(RsCompany.Fields("POST_NOTICEPAY").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            txtNoticePay.Text = MasterNo
        Else
            txtNoticePay.Text = ""
        End If

        If IsDbNull(RsCompany.Fields("POST_EXGRATIA").Value) Then
            txtExGratia.Text = ""
        ElseIf MainClass.ValidateWithMasterTable(RsCompany.Fields("POST_EXGRATIA").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            txtExGratia.Text = MasterNo
        Else
            txtExGratia.Text = ""
        End If

        txtPFAdminPer.Text = IIf(IsDbNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value)
        txtPFAdminPer_22.Text = IIf(IsDbNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value)
        txtPFEDLIPer.Text = IIf(IsDbNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value)
        txtEmployerESIPer.Text = IIf(IsDbNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value)
        txtWelfarePer.Text = IIf(IsDbNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value)

        txtPaidDays.Text = IIf(IsDbNull(RsCompany.Fields("LEAVEPAIDDAYS").Value), 0, RsCompany.Fields("LEAVEPAIDDAYS").Value)
        txtDepositLeave.Text = IIf(IsDbNull(RsCompany.Fields("DEPOSITLEAVE").Value), 0, RsCompany.Fields("DEPOSITLEAVE").Value)
        txtDepositLeave_Wk.Text = IIf(IsDbNull(RsCompany.Fields("DEPOSITLEAVE_WK").Value), 0, RsCompany.Fields("DEPOSITLEAVE_WK").Value)

        txtStaffELPerDay.Text = IIf(IsDbNull(RsCompany.Fields("STAFF_EL_PER_DAYS").Value), 0, RsCompany.Fields("STAFF_EL_PER_DAYS").Value)
        txtWorkerELPerDay.Text = IIf(IsDbNull(RsCompany.Fields("WORKER_EL_PER_DAYS").Value), 0, RsCompany.Fields("WORKER_EL_PER_DAYS").Value)

        mWeeklyOffType = IIf(IsDbNull(RsCompany.Fields("WEEKLYOFF_TYPE").Value), "C", RsCompany.Fields("WEEKLYOFF_TYPE").Value)

        If mWeeklyOffType = "C" Then
            optWeeklyOff(0).Checked = True
        Else
            optWeeklyOff(1).Checked = True
        End If

        mPunchingFrom = IIf(IsDbNull(RsCompany.Fields("PUNCH_FROM").Value), "G", RsCompany.Fields("PUNCH_FROM").Value)

        If mPunchingFrom = "G" Then
            optPunchingFrom(0).Checked = True
            optPunchingFrom(0).Enabled = True
            optPunchingFrom(1).Enabled = True
        Else
            optPunchingFrom(1).Checked = True
            optPunchingFrom(0).Enabled = False
            optPunchingFrom(1).Enabled = False
        End If

        txtTableName_Gate.Text = IIf(IsDbNull(RsCompany.Fields("PUNCH_GATE_TABLE").Value), "", RsCompany.Fields("PUNCH_GATE_TABLE").Value)
        txtTableName_Dept.Text = IIf(IsDbNull(RsCompany.Fields("PUNCH_DEPT_TABLE").Value), "", RsCompany.Fields("PUNCH_DEPT_TABLE").Value)

        txtTableName_Gate.Enabled = IIf(Trim(txtTableName_Gate.Text) = "", True, False)
        txtTableName_Dept.Enabled = IIf(Trim(txtTableName_Dept.Text) = "", True, False)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo err_Renamed
        Dim SqlStr As String = ""

        Dim mCheckOTinPaySlip As String
        Dim mOTFactor As Double
        Dim mBonusLimit As Double

        Dim mSqlStr As String

        Dim mPFAdminPer As Double
        Dim mPFAdminPer22 As Double
        Dim mPFEDLIPer As Double
        Dim mEmployerESIPer As Double
        Dim mWelfarePer As Double
        Dim mContOTFactor As Double
        Dim mLateEntry As Integer
        Dim mShortLeave As Integer
        Dim mNoticePay As String
        Dim mExGratia As String
        Dim mWeeklyOffType As String
        Dim mBonusType As String

        Dim mLateEntryDays As Double
        Dim mShortLeaveDays As Double

        Dim mPunchingFrom As String
        Dim mTableName_Gate As String
        Dim mTableName_Dept As String
        Dim mOTStartMin As Integer

        PubDBCn.BeginTrans()

        SqlStr = ""


        mCheckOTinPaySlip = IIf(chkOTin.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mOTFactor = Val(txtOTMin.Text)
        mOTStartMin = Val(txtOTStartMin.Text)
        mContOTFactor = Val(txtContOTMin.Text)
        mBonusLimit = Val(TxtBonusLimit.Text)
        mLateEntry = Val(txtLateEntryMin.Text)
        mShortLeave = Val(txtSortLeaveMin.Text)

        mLateEntryDays = Val(txtLateEntryDays.Text)
        mShortLeaveDays = Val(txtSortLeaveDays.Text)

        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""



        If MainClass.ValidateWithMasterTable((txtNoticePay.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mNoticePay = MasterNo
        Else
            mNoticePay = "-1"
        End If

        If MainClass.ValidateWithMasterTable((txtExGratia.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
            mExGratia = MasterNo
        Else
            mExGratia = "-1"
        End If

        mPFAdminPer = Val(txtPFAdminPer.Text)
        mPFAdminPer22 = Val(txtPFAdminPer_22.Text)
        mPFEDLIPer = Val(txtPFEDLIPer.Text)
        mEmployerESIPer = Val(txtEmployerESIPer.Text)
        mWelfarePer = Val(txtWelfarePer.Text)

        mWeeklyOffType = IIf(optWeeklyOff(0).Checked = True, "C", "S")
        mBonusType = IIf(optBonus(0).Checked = True, "B", "C")

        mPunchingFrom = IIf(optPunchingFrom(0).Checked = True, "G", "D")
        mTableName_Gate = Trim(txtTableName_Gate.Text)
        mTableName_Dept = Trim(txtTableName_Dept.Text)



        SqlStr = "UPDATE  FIN_PRINT_MST SET " & vbCrLf _
            & " PrintOTinPaySlip='" & Trim(mCheckOTinPaySlip) & "'," & vbCrLf _
            & " OTFactor=" & mOTFactor & ", " & vbCrLf _
            & " OT_START_MIN=" & mOTStartMin & ", " & vbCrLf _
            & " BonusLimit=" & mBonusLimit & ", " & vbCrLf _
            & " BONUS_CEIL_AMT=" & Val(txtBonusCeilingAmt.Text) & ", BONUS_TYPE='" & mBonusType & "', " & vbCrLf _
            & " PFADMINPER=" & mPFAdminPer & "," & vbCrLf _
            & " PFADMINPER_22=" & mPFAdminPer22 & "," & vbCrLf _
            & " PFEDLIPER=" & mPFEDLIPer & "," & vbCrLf _
            & " EMPLOYERESIPER=" & mEmployerESIPer & "," & vbCrLf _
            & " WELFAREPER=" & mWelfarePer & "," & vbCrLf _
            & " CONT_OTFACTOR=" & mContOTFactor & "," & vbCrLf _
            & " LeavePaidDays =" & Val(txtPaidDays.Text) & ", " & vbCrLf _
            & " DepositLeave =" & Val(txtDepositLeave.Text) & ", " & vbCrLf _
            & " DepositLeave_WK =" & Val(txtDepositLeave_Wk.Text) & ", " & vbCrLf _
            & " STAFF_EL_PER_DAYS =" & Val(txtStaffELPerDay.Text) & ", " & vbCrLf _
            & " WORKER_EL_PER_DAYS =" & Val(txtWorkerELPerDay.Text) & ", " & vbCrLf _
            & " LATE_ENTRY = " & mLateEntry & ", " & vbCrLf _
            & " SHORT_LEAVE = " & mShortLeave & ", " & vbCrLf _
            & " LATE_ENTRY_DAYS = " & mLateEntryDays & ", " & vbCrLf _
            & " SHORT_LEAVE_DAYS= " & mShortLeaveDays & ", " & vbCrLf _
            & " POST_NOTICEPAY='" & Trim(mNoticePay) & "'," & vbCrLf _
            & " POST_EXGRATIA='" & Trim(mExGratia) & "'," & vbCrLf _
            & " PUNCH_FROM='" & mPunchingFrom & "'," & vbCrLf _
            & " PUNCH_GATE_TABLE='" & Trim(mTableName_Gate) & "'," & vbCrLf _
            & " PUNCH_DEPT_TABLE='" & Trim(mTableName_Dept) & "',"

        SqlStr = SqlStr & vbCrLf _
            & " UPDATE_FROM='H', WEEKLYOFF_TYPE ='" & mWeeklyOffType & "'" & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        RsCompany.Requery()
        Exit Function
err_Renamed:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsCompany.Requery()
        MsgInformation(Err.Description)

    End Function
    Private Sub frmSysPrefPayroll_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Function FieldVerification() As Boolean
        On Error GoTo ERR1
        FieldVerification = True
        If Trim(txtOTMin.Text) = "" Then
            MsgInformation("OT Calculation Factor Cann't to blank.")
            txtOTMin.Focus()
            FieldVerification = False
            Exit Function
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub optBonus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBonus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBonus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optPunchingFrom_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPunchingFrom.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPunchingFrom.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optWeeklyOff_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optWeeklyOff.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optWeeklyOff.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub TxtBonusLimit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBonusLimit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtBonusLimit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtBonusLimit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBonusCeilingAmt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusCeilingAmt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBonusCeilingAmt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusCeilingAmt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtContOTMin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContOTMin.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtContOTMin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContOTMin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDepositLeave_Wk_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepositLeave_Wk.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDepositLeave_Wk_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDepositLeave_Wk.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDepositLeave_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepositLeave.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDepositLeave_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDepositLeave.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExGratia_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExGratia.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExGratia_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExGratia.DoubleClick
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchMaster((txtExGratia.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtExGratia.Text = AcName
        End If

        Exit Sub

    End Sub

    Private Sub txtExGratia_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExGratia.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExGratia.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExGratia_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtExGratia.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If KeyCode = System.Windows.Forms.Keys.F1 Then

            If MainClass.SearchMaster((txtExGratia.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
                txtExGratia.Text = AcName
            End If

            Exit Sub
        End If
    End Sub

    Private Sub txtExGratia_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExGratia.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtExGratia.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtExGratia.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtExGratia.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLateEntryDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLateEntryDays.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLateEntryDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLateEntryDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLateEntryMin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLateEntryMin.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLateEntryMin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLateEntryMin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNoticePay_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNoticePay.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNoticePay_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNoticePay.DoubleClick
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchMaster((txtNoticePay.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtNoticePay.Text = AcName
        End If

        Exit Sub

    End Sub

    Private Sub txtNoticePay_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNoticePay.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNoticePay.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNoticePay_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNoticePay.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If KeyCode = System.Windows.Forms.Keys.F1 Then

            If MainClass.SearchMaster((txtNoticePay.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
                txtNoticePay.Text = AcName
            End If

            Exit Sub
        End If
    End Sub

    Private Sub txtNoticePay_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNoticePay.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtNoticePay.Text)) = "" Then GoTo EventExitSub
        SqlStr = "Select SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtNoticePay.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtNoticePay.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPFAdminPer_22_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPFAdminPer_22.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPFAdminPer_22_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPFAdminPer_22.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSortLeaveDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSortLeaveDays.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSortLeaveDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSortLeaveDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSortLeaveMin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSortLeaveMin.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSortLeaveMin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSortLeaveMin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtEmployerESIPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmployerESIPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmployerESIPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmployerESIPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtOTMin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOTMin.TextChanged, txtOTStartMin.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOTMin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOTMin.KeyPress, txtOTStartMin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaidDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDays.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPFAdminPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPFAdminPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPFEDLIPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPFEDLIPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPFEDLIPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPFEDLIPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub txtStaffELPerDay_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStaffELPerDay.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStaffELPerDay_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStaffELPerDay.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTableName_Dept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTableName_Dept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTableName_Dept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTableName_Dept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNoticePay.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTableName_Gate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTableName_Gate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTableName_Gate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTableName_Gate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNoticePay.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtWelfarePer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWelfarePer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWelfarePer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWelfarePer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtWorkerELPerDay_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWorkerELPerDay.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWorkerELPerDay_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkerELPerDay.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
