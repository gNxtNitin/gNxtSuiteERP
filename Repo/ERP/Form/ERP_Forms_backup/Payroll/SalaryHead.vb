Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalaryHead
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim Shw As Boolean
    Dim xCode As Integer
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If

        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        TxtName.Text = ""
        txtPercentage.Text = ""
        txtSeq.Text = ""
        cboType.SelectedIndex = -1
        cboRound.SelectedIndex = 2
        chkESI.CheckState = System.Windows.Forms.CheckState.Unchecked
        ChkPF.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBasicSalPart.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkLeaveEncash.CheckState = System.Windows.Forms.CheckState.Unchecked
        OptAdd_Ded(0).Checked = True
        OptCalc(0).Checked = True
        OptPaymentType(0).Checked = True
        cboDC.SelectedIndex = -1
        txtDebit.Text = ""
        txtDefaultAmount.Text = ""

        OptStatus(0).Checked = True
        txtClosedDate.Text = "__/__/____"

        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboDC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDC.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboRound_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRound.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkBasicSalPart_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBasicSalPart.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkESI_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkESI.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkLeaveEncash_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkLeaveEncash.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub ChkPF_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPF.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdDSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDSearch.Click
        Dim mSqlStr As String
        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'"

        If MainClass.SearchMaster((txtDebit.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", mSqlStr) = True Then
            txtDebit.Text = AcName
        End If

        Exit Sub
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'ShowReport crptToPrinter
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            TxtName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsEmp.EOF = False Then RsEmp.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsEmp.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsEmp.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim mProdCode As String
        SqlStr = ""

        If MainClass.SearchMaster((TxtName.Text), "PAY_SALARYHEAD_MST", "Name", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtName.Text = AcName
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub

    End Sub
    Private Sub frmSalaryHead_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub OptAdd_Ded_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAdd_Ded.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptAdd_Ded.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptCalc_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptCalc.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptCalc.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptPaymentType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptPaymentType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptPaymentType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        TxtName.Text = SprdView.Text
        txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtClosedDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtClosedDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDebit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDebit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebit.DoubleClick
        cmdDSearch_Click(cmdDSearch, New System.EventArgs())
    End Sub

    Private Sub txtDebit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDebit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDebit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDebit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDebit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdDSearch_Click(cmdDSearch, New System.EventArgs())
        End If
    End Sub

    Private Sub txtDebit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDebit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String = ""
        Dim RsACM As ADODB.Recordset

        If Trim(UCase(txtDebit.Text)) = "" Then GoTo EventExitSub

        SqlStr = " SELECT SUPP_CUST_NAME from FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE='O'" & vbCrLf & " AND UPPER(trim(SUPP_CUST_NAME)) = '" & MainClass.AllowSingleQuote(Trim(UCase(txtDebit.Text))) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If RsACM.EOF = False Then
            txtDebit.Text = IIf(IsDbNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invaild Account Name.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDefaultAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDefaultAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDefaultAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDefaultAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmSalaryHead_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From PAY_SALARYHEAD_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
        SqlStr = "Select Name  from PAY_SALARYHEAD_MST Order by Name"
        AssignGrid(False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        settextlength()
        cboRound.Items.Add("1")
        cboRound.Items.Add("0.1")
        cboRound.Items.Add("0.01")
        cboRound.Items.Add("0.05")


        FillCboType()
        Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmSalaryHead_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Height = VB6.TwipsToPixelsY(6225)
        Me.Width = VB6.TwipsToPixelsX(5805)
        Me.Left = 0
        Me.Top = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub frmSalaryHead_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsEmp = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mCategoryName As String
        Dim mClosedDate As String
        Dim mAccountPOstCode As String

        Shw = True
        If Not RsEmp.EOF Then
            TxtName.Text = IIf(IsDbNull(RsEmp.Fields("Name").Value), "", RsEmp.Fields("Name").Value)
            txtSeq.Text = IIf(IsDbNull(RsEmp.Fields("SEQ").Value), "", RsEmp.Fields("SEQ").Value)
            txtPercentage.Text = IIf(IsDbNull(RsEmp.Fields("PERCENTAGE").Value), "", RsEmp.Fields("PERCENTAGE").Value)
            txtDefaultAmount.Text = IIf(IsDbNull(RsEmp.Fields("DEFAULT_AMT").Value), "", RsEmp.Fields("DEFAULT_AMT").Value)

            If RsEmp.Fields("ROUNDING").Value <> "" Then
                cboRound.Text = RsEmp.Fields("ROUNDING").Value
            End If
            '        cboRound.Text = IIf(RsEmp!Rounding <> "", RsEmp!Rounding)

            If RsEmp.Fields("PAYMENT_TYPE").Value = "M" Then
                OptPaymentType(0).Checked = True
            Else
                OptPaymentType(1).Checked = True
            End If

            If RsEmp.Fields("ADDDEDUCT").Value = ConEarning Then
                OptAdd_Ded(0).Checked = True
            ElseIf RsEmp.Fields("ADDDEDUCT").Value = ConDeduct Then
                OptAdd_Ded(1).Checked = True
            ElseIf RsEmp.Fields("ADDDEDUCT").Value = ConPerks Then
                OptAdd_Ded(2).Checked = True
            End If

            If RsEmp.Fields("CALC_ON").Value = ConCalcBSalary Then
                OptCalc(0).Checked = True
            ElseIf RsEmp.Fields("CALC_ON").Value = ConCalcFixed Then
                OptCalc(1).Checked = True
            ElseIf RsEmp.Fields("CALC_ON").Value = ConCalcVariable Then
                OptCalc(2).Checked = True
            End If

            If RsEmp.Fields("Type").Value = ConPF Then
                cboType.SelectedIndex = 0
            ElseIf RsEmp.Fields("Type").Value = ConESI Then
                cboType.SelectedIndex = 1
            ElseIf RsEmp.Fields("Type").Value = ConConveyance Then
                cboType.SelectedIndex = 2
            ElseIf RsEmp.Fields("Type").Value = ConHRA Then
                cboType.SelectedIndex = 3
            ElseIf RsEmp.Fields("Type").Value = ConAdvance Then
                cboType.SelectedIndex = 4
            ElseIf RsEmp.Fields("Type").Value = ConLoan Then
                cboType.SelectedIndex = 5
            ElseIf RsEmp.Fields("Type").Value = ConIncomeTax Then
                cboType.SelectedIndex = 6
            ElseIf RsEmp.Fields("Type").Value = ConImprest Then
                cboType.SelectedIndex = 7
            ElseIf RsEmp.Fields("Type").Value = ConOT Then
                cboType.SelectedIndex = 8
            ElseIf RsEmp.Fields("Type").Value = ConOthers Then
                cboType.SelectedIndex = 9
            ElseIf RsEmp.Fields("Type").Value = ConTDS Then
                cboType.SelectedIndex = 10
            ElseIf RsEmp.Fields("Type").Value = ConChildrenAllw Then
                cboType.SelectedIndex = 11
            ElseIf RsEmp.Fields("Type").Value = ConLIC Then
                cboType.SelectedIndex = 12
            ElseIf RsEmp.Fields("Type").Value = ConDA Then
                cboType.SelectedIndex = 13
            ElseIf RsEmp.Fields("Type").Value = ConVDA Then
                cboType.SelectedIndex = 14
            ElseIf RsEmp.Fields("Type").Value = ConIncentiveAllw Then
                cboType.SelectedIndex = 15
            ElseIf RsEmp.Fields("Type").Value = ConAttendanceAllw Then
                cboType.SelectedIndex = 16
            ElseIf RsEmp.Fields("Type").Value = ConTourAllw Then
                cboType.SelectedIndex = 17
            ElseIf RsEmp.Fields("Type").Value = ConMedicalAllw Then
                cboType.SelectedIndex = 18
            ElseIf RsEmp.Fields("Type").Value = ConMilkAllw Then
                cboType.SelectedIndex = 19
            ElseIf RsEmp.Fields("Type").Value = ConAwardAllw Then
                cboType.SelectedIndex = 20
            ElseIf RsEmp.Fields("Type").Value = ConGiftAllw Then
                cboType.SelectedIndex = 21
            ElseIf RsEmp.Fields("Type").Value = ConWashAllw Then
                cboType.SelectedIndex = 22
            ElseIf RsEmp.Fields("Type").Value = ConVPFAllw Then
                cboType.SelectedIndex = 23
            ElseIf RsEmp.Fields("Type").Value = ConWelfare Then
                cboType.SelectedIndex = 24
            ElseIf RsEmp.Fields("Type").Value = ConCCAAllw Then
                cboType.SelectedIndex = 25
            ElseIf RsEmp.Fields("Type").Value = ConSpecialAllw Then
                cboType.SelectedIndex = 26
            ElseIf RsEmp.Fields("Type").Value = ConTransportAllw Then
                cboType.SelectedIndex = 27
            ElseIf RsEmp.Fields("Type").Value = ConExGratiaAllw Then
                cboType.SelectedIndex = 28
            ElseIf RsEmp.Fields("Type").Value = ConLTA Then
                cboType.SelectedIndex = 29
            ElseIf RsEmp.Fields("Type").Value = ConINAAM Then
                cboType.SelectedIndex = 30
            ElseIf RsEmp.Fields("Type").Value = ConBonus Then
                cboType.SelectedIndex = 31
            ElseIf RsEmp.Fields("Type").Value = ConOtherEarningVar Then
                cboType.SelectedIndex = 32
            ElseIf RsEmp.Fields("Type").Value = ConEmployerPF Then
                cboType.SelectedIndex = 33
            ElseIf RsEmp.Fields("Type").Value = ConCanteen Then
                cboType.SelectedIndex = 34
            ElseIf RsEmp.Fields("Type").Value = ConMedicalReimbursement Then
                cboType.SelectedIndex = 35
            ElseIf RsEmp.Fields("Type").Value = ConEmployerESI Then
                cboType.SelectedIndex = 36
            ElseIf RsEmp.Fields("Type").Value = ConProfessionalTax Then
                cboType.SelectedIndex = 37
            End If


            If RsEmp.Fields("INCLUDEDPF").Value = "Y" Then
                ChkPF.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                ChkPF.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            chkBasicSalPart.CheckState = IIf(RsEmp.Fields("ISSALPART").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            If RsEmp.Fields("INCLUDEDESI").Value = "Y" Then
                chkESI.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                chkESI.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
            If RsEmp.Fields("INCLUDEDLEAVEENCASH").Value = "Y" Then
                chkLeaveEncash.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                chkLeaveEncash.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            mAccountPOstCode = IIf(IsDBNull(RsEmp.Fields("ACCOUNTCODEPOST").Value), "-1", RsEmp.Fields("ACCOUNTCODEPOST").Value)

            If MainClass.ValidateWithMasterTable(mAccountPOstCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtDebit.Text = MasterNo
            Else
                txtDebit.Text = ""
            End If

            'If Trim(RsEmp.Fields("DC").Value) <> "" Then
            If Not IsDbNull(RsEmp.Fields("DC").Value) Then
                    cboDC.Text = RsEmp.Fields("DC").Value
                End If
            'End If

            optStatus(0).Checked = IIf(RsEmp.Fields("Status").Value = "O", True, False)
            OptStatus(1).Checked = IIf(RsEmp.Fields("Status").Value = "C", True, False)

            mClosedDate = IIf(IsDbNull(RsEmp.Fields("CLOSED_DATE").Value), "", RsEmp.Fields("CLOSED_DATE").Value)
            If IsDate(mClosedDate) Then
                txtClosedDate.Text = VB6.Format(mClosedDate, "DD/MM/YYYY")
            Else
                txtClosedDate.Text = "__/__/____"
            End If

            'cboDC.ListIndex = -1
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        If RsEmp.EOF = False Then
            xCode = RsEmp.Fields("Code").Value
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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
        Dim mCode As Integer
        Dim mAddDeduct, mCalcOn As Object
        Dim mType As Integer
        Dim mIncudedPF As String
        Dim mIncudedESI As String
        Dim mIncudedLeaveEncash As String
        Dim mDAccountCode As Integer
        Dim mDC As String
        Dim mISSALPART As String
        Dim mStatus As String
        Dim mClosedDate As String
        Dim mPaymentType As String

        If OptAdd_Ded(2).Checked = True Then
            If OptPaymentType(0).Checked = True Then
                mPaymentType = "M"
            Else
                mPaymentType = "Y"
            End If
        Else
            mPaymentType = "M"
        End If

        If OptAdd_Ded(0).Checked = True Then
            mAddDeduct = ConEarning
        ElseIf OptAdd_Ded(1).Checked = True Then
            mAddDeduct = ConDeduct
        ElseIf OptAdd_Ded(2).Checked = True Then
            mAddDeduct = ConPerks
        End If

        If OptCalc(0).Checked = True Then
            mCalcOn = ConCalcBSalary
        ElseIf OptCalc(1).Checked = True Then
            mCalcOn = ConCalcFixed
        ElseIf OptCalc(2).Checked = True Then
            mCalcOn = ConCalcVariable
        End If

        If cboType.SelectedIndex = 0 Then
            mType = ConPF
        ElseIf cboType.SelectedIndex = 1 Then
            mType = ConESI
        ElseIf cboType.SelectedIndex = 2 Then
            mType = ConConveyance
        ElseIf cboType.SelectedIndex = 3 Then
            mType = ConHRA
        ElseIf cboType.SelectedIndex = 4 Then
            mType = ConAdvance
        ElseIf cboType.SelectedIndex = 5 Then
            mType = ConLoan
        ElseIf cboType.SelectedIndex = 6 Then
            mType = ConIncomeTax
        ElseIf cboType.SelectedIndex = 7 Then
            mType = ConImprest
        ElseIf cboType.SelectedIndex = 8 Then
            mType = ConOT
        ElseIf cboType.SelectedIndex = 9 Then
            mType = ConOthers
        ElseIf cboType.SelectedIndex = 10 Then
            mType = ConTDS
        ElseIf cboType.SelectedIndex = 11 Then
            mType = ConChildrenAllw
        ElseIf cboType.SelectedIndex = 12 Then
            mType = ConLIC
        ElseIf cboType.SelectedIndex = 13 Then
            mType = ConDA
        ElseIf cboType.SelectedIndex = 14 Then
            mType = ConVDA
        ElseIf cboType.SelectedIndex = 15 Then
            mType = ConIncentiveAllw
        ElseIf cboType.SelectedIndex = 16 Then
            mType = ConAttendanceAllw
        ElseIf cboType.SelectedIndex = 17 Then
            mType = ConTourAllw
        ElseIf cboType.SelectedIndex = 18 Then
            mType = ConMedicalAllw
        ElseIf cboType.SelectedIndex = 19 Then
            mType = ConMilkAllw
        ElseIf cboType.SelectedIndex = 20 Then
            mType = ConAwardAllw
        ElseIf cboType.SelectedIndex = 21 Then
            mType = ConGiftAllw
        ElseIf cboType.SelectedIndex = 22 Then
            mType = ConWashAllw
        ElseIf cboType.SelectedIndex = 23 Then
            mType = ConVPFAllw
        ElseIf cboType.SelectedIndex = 24 Then
            mType = ConWelfare
        ElseIf cboType.SelectedIndex = 25 Then
            mType = ConCCAAllw
        ElseIf cboType.SelectedIndex = 26 Then
            mType = ConSpecialAllw
        ElseIf cboType.SelectedIndex = 27 Then
            mType = ConTransportAllw
        ElseIf cboType.SelectedIndex = 28 Then
            mType = ConExGratiaAllw
        ElseIf cboType.SelectedIndex = 29 Then
            mType = ConLTA
        ElseIf cboType.SelectedIndex = 30 Then
            mType = ConINAAM
        ElseIf cboType.SelectedIndex = 31 Then
            mType = ConBonus
        ElseIf cboType.SelectedIndex = 32 Then
            mType = ConOtherEarningVar
        ElseIf cboType.SelectedIndex = 33 Then
            mType = ConEmployerPF
        ElseIf cboType.SelectedIndex = 34 Then
            mType = ConCanteen
        ElseIf cboType.SelectedIndex = 35 Then
            mType = ConMedicalReimbursement
        ElseIf cboType.SelectedIndex = 36 Then
            mType = ConEmployerESI
        ElseIf cboType.SelectedIndex = 37 Then
            mType = ConProfessionalTax
        End If


        If ChkPF.CheckState = System.Windows.Forms.CheckState.Checked Then
            mIncudedPF = "Y"
        Else
            mIncudedPF = "N"
        End If

        mISSALPART = IIf(chkBasicSalPart.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            mIncudedESI = "Y"
        Else
            mIncudedESI = "N"
        End If
        If chkLeaveEncash.CheckState = System.Windows.Forms.CheckState.Checked Then
            mIncudedLeaveEncash = "Y"
        Else
            mIncudedLeaveEncash = "N"
        End If

        If MainClass.ValidateWithMasterTable((txtDebit.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDAccountCode = MasterNo
        Else
            mDAccountCode = -1
        End If

        mDC = cboDC.Text

        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")

        '    mClosedDate = IIf(mStatus = "O", "", txtClosedDate.Text)
        mClosedDate = VB6.Format(txtClosedDate.Text, "DD-MMM-YYYY")


        SqlStr = ""
        If ADDMode = True Then
            mCode = MainClass.AutoGenRowNo("SALARYHEAD", "CODE", PubDBCn)
            SqlStr = " INSERT INTO PAY_SALARYHEAD_MST ( " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " CODE, NAME, ADDDEDUCT, CALC_ON, " & vbCrLf & " TYPE, SEQ,Percentage,Rounding,INCLUDEDPF, " & vbCrLf & " INCLUDEDESI,INCLUDEDLEAVEENCASH,AccountCodePost,DC, ISSALPART, " & vbCrLf & " STATUS, CLOSED_DATE, DEFAULT_AMT, PAYMENT_TYPE," & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE ) " & vbCrLf & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & mCode & ",'" & MainClass.AllowSingleQuote(Trim(txtName.Text)) & "', " & vbCrLf & " " & mAddDeduct & "," & mCalcOn & "," & mType & "," & Val(txtSeq.Text) & ", " & vbCrLf & " " & Val(txtPercentage.Text) & ", '" & cboRound.Text & "', " & vbCrLf & " '" & mIncudedPF & "','" & mIncudedESI & "','" & mIncudedLeaveEncash & "', " & vbCrLf & " " & mDAccountCode & ",'" & mDC & "', '" & mISSALPART & "', " & vbCrLf & " '" & mStatus & "', TO_DATE('" & VB6.Format(mClosedDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtDefaultAmount.Text) & ", '" & mPaymentType & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        Else

            SqlStr = "UPDATE  PAY_SALARYHEAD_MST SET " & vbCrLf & " NAME='" & MainClass.AllowSingleQuote(Trim(txtName.Text)) & "', " & vbCrLf & " ADDDEDUCT = '" & mAddDeduct & "', SEQ=" & Val(txtSeq.Text) & ", " & vbCrLf & " CALC_ON =" & mCalcOn & " , TYPE='" & mType & "', " & vbCrLf & " Percentage=" & Val(txtPercentage.Text) & ", " & vbCrLf & " DEFAULT_AMT=" & Val(txtDefaultAmount.Text) & ", " & vbCrLf & " Rounding='" & cboRound.Text & "', " & vbCrLf & " INCLUDEDPF='" & mIncudedPF & "', " & vbCrLf & " INCLUDEDESI='" & mIncudedESI & "', " & vbCrLf & " INCLUDEDLEAVEENCASH='" & mIncudedLeaveEncash & "', " & vbCrLf & " AccountCodePost=" & mDAccountCode & "," & vbCrLf & " DC='" & mDC & "', PAYMENT_TYPE='" & mPaymentType & "', " & vbCrLf & " ISSALPART='" & mISSALPART & "', " & vbCrLf & " STATUS='" & mStatus & "', " & vbCrLf & " CLOSED_DATE=TO_DATE('" & VB6.Format(mClosedDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & xCode & ""

        End If
UpdatePart:
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsEmp.Requery()
        Update1 = True
        Exit Function
UpdateError:
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        'Resume

        Update1 = False
        PubDBCn.RollbackTrans()
        RsEmp.Requery()

        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim mStatus As String
        Dim mClosedDate As String

        FieldsVarification = True
        If Trim(TxtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            TxtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboType.Text) = "" Then
            MsgInformation("Please select the Type.")
            cboType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboRound.Text) = "" Then
            MsgInformation("Please select the Rounding Value")
            cboRound.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDebit.Text) <> "" Then
            If Trim(cboDC.Text) = "" Then
                MsgInformation("Please select the Posting Type.")
                cboDC.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")
        mClosedDate = VB6.Format(txtClosedDate.Text, "DD/MM/YYYY")

        If mStatus = "C" Then
            If Not IsDate(mClosedDate) Then
                MsgInformation("Please enter Vaild Closed Date.")
                txtClosedDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If Not IsDate(mClosedDate) Then
                MsgInformation("Please enter Vaild Open Date.")
                txtClosedDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsEmp.EOF = True Or RsEmp.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
    End Function
    Private Sub settextlength()
        On Error GoTo ERR1
        TxtName.Maxlength = RsEmp.Fields("Name").DefinedSize
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = " SELECT NAME, " & vbCrLf & " CASE WHEN Adddeduct=" & ConEarning & " THEN 'Earn' WHEN Adddeduct=" & ConPerks & " THEN 'Perks' ELSE 'Deduct' END AS PAY_SALARYHEAD_MST, "


        SqlStr = SqlStr & vbCrLf & " CASE WHEN Calc_On=" & ConCalcBSalary & " THEN 'Basic' " & vbCrLf & " WHEN Calc_On=" & ConCalcFixed & " THEN 'Fixed' " & vbCrLf & " ELSE 'Variable' END AS Calc_On, " & vbCrLf & " CASE WHEN TYPE=" & ConBasicSalary & " THEN 'Basic' " & vbCrLf & " WHEN TYPE=" & ConPF & " THEN 'PF' " & vbCrLf & " WHEN TYPE=" & ConDA & " THEN 'DA' " & vbCrLf & " WHEN TYPE=" & ConVDA & " THEN 'VDA' " & vbCrLf & " WHEN TYPE=" & ConESI & " THEN 'ESI' " & vbCrLf & " WHEN TYPE=" & ConConveyance & " THEN 'Conveyance' " & vbCrLf & " WHEN TYPE=" & ConHRA & " THEN 'HRA' " & vbCrLf & " WHEN TYPE=" & ConAdvance & " THEN 'Advance' " & vbCrLf & " WHEN TYPE=" & ConLoan & " THEN 'Loan' " & vbCrLf & " WHEN TYPE=" & ConIncomeTax & " THEN 'Income Tax' " & vbCrLf & " WHEN TYPE=" & ConImprest & " THEN 'Imprest' " & vbCrLf & " WHEN TYPE=" & ConOT & " THEN 'OT' " & vbCrLf & " WHEN TYPE=" & ConTDS & " THEN 'TDS' " & vbCrLf & " WHEN TYPE=" & ConChildrenAllw & " THEN 'Chld. Allw.' " & vbCrLf & " WHEN TYPE=" & ConLIC & " THEN 'LIC' " & vbCrLf & " WHEN TYPE=" & ConIncentiveAllw & " THEN 'Incentive Allow.' " & vbCrLf & " WHEN TYPE=" & ConAttendanceAllw & " THEN 'Attendance Allow' " & vbCrLf & " WHEN TYPE=" & ConTourAllw & " THEN 'Tour Allow' " & vbCrLf & " WHEN TYPE=" & ConMedicalAllw & " THEN 'Medical Allow' "


        SqlStr = SqlStr & vbCrLf & " WHEN TYPE=" & ConMilkAllw & " THEN 'Milk Allow' " & vbCrLf & " WHEN TYPE=" & ConAwardAllw & " THEN 'Award Allow' " & vbCrLf & " WHEN TYPE=" & ConGiftAllw & " THEN 'Gift Allow' " & vbCrLf & " WHEN TYPE=" & ConWashAllw & " THEN 'Washing Allow' " & vbCrLf & " WHEN TYPE=" & ConVPFAllw & " THEN 'VPF' " & vbCrLf & " WHEN TYPE=" & ConWelfare & " THEN 'WelFare' " & vbCrLf & " WHEN TYPE=" & ConLTA & " THEN 'LTA' "

        SqlStr = SqlStr & vbCrLf & " WHEN TYPE=" & ConCCAAllw & " THEN 'C.C.A. Allow' "
        SqlStr = SqlStr & vbCrLf & " WHEN TYPE=" & ConExGratiaAllw & " THEN 'Ex-Gratia' "
        SqlStr = SqlStr & vbCrLf & " WHEN TYPE=" & ConTransportAllw & " THEN 'Transport Allow' "
        SqlStr = SqlStr & vbCrLf & " WHEN TYPE=" & ConSpecialAllw & " THEN 'Special Allow' "
        SqlStr = SqlStr & vbCrLf & " WHEN TYPE=" & ConINAAM & " THEN 'INAAM' "
        SqlStr = SqlStr & vbCrLf & " WHEN TYPE=" & ConBonus & " THEN 'BONUS' "
        SqlStr = SqlStr & vbCrLf & " ELSE 'Others' END AS TYPE, "

        SqlStr = SqlStr & vbCrLf & " PERCENTAGE,SEQ,Rounding,INCLUDEDPF, INCLUDEDESI, INCLUDEDLEAVEENCASH,  " & vbCrLf & " DECODE(ISSALPART,'Y','Yes','No') AS SAL_PART, DECODE(STATUS,'O','Open','Closed') AS STATUS " & vbCrLf & " FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY Adddeduct,SEQ,NAME"


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 16)
            .set_ColWidth(2, 8)
            .set_ColWidth(3, 8)
            .set_ColWidth(4, 8)
            .set_ColWidth(5, 8)
            .set_ColWidth(6, 8)
            .set_ColWidth(7, 8)
            .set_ColWidth(8, 8)
            .set_ColWidth(9, 8)
            .set_ColWidth(10, 6)
            .set_ColWidth(11, 6)
            .set_ColWidth(12, 6)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean

        On Error GoTo DeleteErr
        Dim mEmpCode As Integer
        SqlStr = ""
        MainClass.ValidateWithMasterTable(TxtName.Text, "Name", "Code", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo)
        mEmpCode = MasterNo
        If MainClass.ValidateWithMasterTable(mEmpCode, "ADD_DEDUCTCODE", "ADD_DEDUCTCODE", "PAY_SALARYDEF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            MsgBox("Salary Exists Against " & TxtName.Text)
            Delete1 = False
            Exit Function
        ElseIf MainClass.ValidateWithMasterTable(mEmpCode, "SALHEADCODE", "SALHEADCODE", "PAY_SAL_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            MsgBox("Salary Exists Against " & TxtName.Text)
            Delete1 = False
            Exit Function
        End If

        SqlStr = " Delete from PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND  Code=" & xCode & ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        PubDBCn.Execute(SqlStr)
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
    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        If Trim(TxtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsEmp.EOF = False Then xCode = RsEmp.Fields("Code").Value
        SqlStr = ""

        SqlStr = "SELECT * from  PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND Upper(Name)='" & MainClass.AllowSingleQuote(Trim(UCase(TxtName.Text))) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsEmp.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & xCode & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPercentage_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPercentage.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSeq_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSeq.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub FillCboType()
        cboType.Items.Clear()
        'cboType.AddItem "Basic Salary"
        cboType.Items.Add("PF")
        cboType.Items.Add("ESI")
        cboType.Items.Add("Conveyance")
        cboType.Items.Add("HRA")
        cboType.Items.Add("Advance")
        cboType.Items.Add("Loan")
        cboType.Items.Add("Income Tax")
        cboType.Items.Add("Imprest")
        cboType.Items.Add("Over Time")
        cboType.Items.Add("Others")
        cboType.Items.Add("TDS")
        cboType.Items.Add("Children Allow.")
        cboType.Items.Add("LIC")
        cboType.Items.Add("DA")

        cboType.Items.Add("VDA")
        cboType.Items.Add("Incentive Allow.")
        cboType.Items.Add("Attendance Allow")
        cboType.Items.Add("Tour Allow")
        cboType.Items.Add("Medical Allow")
        cboType.Items.Add("Milk Allow")
        cboType.Items.Add("Award Allow")
        cboType.Items.Add("Gift Allow")
        cboType.Items.Add("Washing Allow")
        cboType.Items.Add("VPF")
        cboType.Items.Add("Welfare")

        cboType.Items.Add("C.C.A. Allow")
        cboType.Items.Add("Special Allow")
        cboType.Items.Add("Transport Allow")
        cboType.Items.Add("Ex-Gratia")
        cboType.Items.Add("LTA")
        cboType.Items.Add("Inaam")
        cboType.Items.Add("Bonus")
        cboType.Items.Add("Other (Variable)")
        cboType.Items.Add("Employer PF")
        cboType.Items.Add("Canteen")
        cboType.Items.Add("Medical Reimbursement")
        cboType.Items.Add("Employer ESI")
        cboType.Items.Add("Professional Tax")

        cboDC.Items.Clear()
        cboDC.Items.Add("Dr")
        cboDC.Items.Add("Cr")
        cboDC.SelectedIndex = 0
    End Sub
End Class
