Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalIncrement
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
    Private Const ColDeductOn As Short = 3
    Private Const ColPer As Short = 4
    Private Const ColAmt As Short = 5
    Private Const ColForm1Amt As Short = 6
    Private Const ColPrevAmt As Short = 7
    Private Const ColPrevForm1Amt As Short = 8

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraMain.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraMain.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

    End Sub
    Private Sub Clear1()

        txtPreBSalary.Text = ""
        txtWEF.Text = ""
        txtWEF.Enabled = True ''22.10.2011
        txtBSalary.Text = ""
        txtGSalary.Text = ""
        txtDeduction.Text = ""
        txtNetSalary.Text = ""
        txtCTC.Text = ""

        txtPrevForm1BSalary.Text = ""
        txtForm1BSalary.Text = ""
        txtForm1GSalary.Text = ""
        txtForm1NetSalary.Text = ""
        txtForm1CTC.Text = ""

        txtAddDays.Text = ""
        txtNextIncDate.Text = ""

        cboAppMon.Text = MonthName(Month(RunDate))
        cboAppYear.Text = CStr(Year(RunDate))

        cboArrearMonth.Text = MonthName(Month(RunDate))
        cboArrearYear.Text = CStr(Year(RunDate))

        SSTab1.SelectedIndex = 0
        '    MainClass.ClearGrid sprdEarn, -1
        '    MainClass.ClearGrid sprdDeduct, -1
        'MainClass.ClearGrid sprdPerks, -1

        lblAppDate.Text = ""
        lblDesg.Text = ""

        cboAppMon.Enabled = True
        cboAppYear.Enabled = True

        cboArrearMonth.Enabled = True
        cboArrearYear.Enabled = True

        cbodesignation.SelectedIndex = -1

        optContBasic.Checked = True
        optContCeiling.Checked = False
        optContGross.Checked = False
        optContCeilingGross.Checked = False
        fraSalMY.Enabled = False
        FillSalarySprd()
        SSTab1.SelectedIndex = 0




        Label81.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtForm1BSalary.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        Label17.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtPrevForm1BSalary.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        Label82.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtForm1GSalary.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        Label84.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtForm1NetSalary.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        Label83.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtForm1CTC.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)

        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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


    Private Sub cbodesignation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cbodesignation_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboYear_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboYear.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click


        '    If CheckSalaryMade(txtEmpNo.Text, Format(lblAppDate.Caption, "DD/MM/YYYY")) = True Then
        '        MsgInformation " Salary Made Againt This Increment. So Cann't be Modified"
        '        Exit Sub
        '    End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Call Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdSearchSalary_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSalary.Click


        ''01/' || TO_CHAR(SALARY_EFF_DATE,'MM/YYYY') AS WEF, EMP_CODE

        SqlStr = "SELECT " & vbCrLf _
            & " DISTINCT TO_CHAR(SALARY_EFF_DATE,'YYYY/MM/DD') AS WEF,  TO_CHAR(SALARY_EFF_DATE,'MON-YYYY') MONTH_NAME " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & Trim(txtEmpNo.Text) & "'"

        '    If MainClass.SearchBySQL(SqlStr, "WEF") = True Then
        If MainClass.SearchGridMasterBySQL2("", Sqlstr) = True Then
            '        txtWEF.Text = "01/" & MonthValue(Trim(Mid(AcName, 1, Len(AcName) - 4)), True) & "/" & Right(LTrim(AcName), 4)
            txtWEF.Text = VB6.Format(AcName, "DD/MM/YYYY")
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtPreBSalary.Visible = True
            lblPBasicSal.Visible = True
            txtEmpNo.Focus()
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            '        FillSalarySprd

            '        ShowSalary txtEmpNo.Text, Format(txtWEF.Text, "MMMYYYY")
            Call Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        lblAppDate.Text = "01/" & MonthValue((cboAppMon.Text)) & "/" & cboAppYear.Text

        If CheckSalaryMade((txtEmpNo.Text), VB6.Format(lblAppDate.Text, "DD/MM/YYYY")) = True Then
            MsgInformation("Salary Made Against This Month So Cann't be deleted.")
            Exit Sub
        End If

        If Not RsEmp.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                Clear1()
                '            If RsEmp.EOF = True Then
                '                Clear1
                '            Else
                '                Show1
                '            End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim mProdCode As String
        Sqlstr = ""

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblEmpType.Text = "S" Then
            Sqlstr = Sqlstr & vbCrLf & " AND EMP_CAT_TYPE='1'"
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND EMP_CAT_TYPE='2'"
        End If

        If MainClass.SearchGridMaster((txtEmpNo.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , Sqlstr) = True Then
            txtEmpNo.Text = AcName1
            TxtName.Text = AcName
            TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub

    End Sub
    Private Sub frmSalIncrement_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Dim KeyCode As Short = eventArgs.KeyCode
        'Dim Shift As Short = eventArgs.KeyData \ &H10000

        'MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub optContGross_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContGross.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            If FormActive = False Then Exit Sub
            Call CalcPFESI()
            Call CalcGrossSalary()
        End If
    End Sub


    Private Sub optContBasic_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContBasic.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            If FormActive = False Then Exit Sub
            Call CalcPFESI()
            Call CalcGrossSalary()
        End If
    End Sub

    Private Sub optContCeiling_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContCeiling.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            If FormActive = False Then Exit Sub
            Call CalcPFESI()
            Call CalcGrossSalary()
        End If
    End Sub
    Private Sub optContCeilingGross_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContCeilingGross.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            If FormActive = False Then Exit Sub
            Call CalcPFESI()
            Call CalcGrossSalary()
        End If
    End Sub

    Private Sub sprdDeduct_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdDeduct.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdDeduct_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdDeduct.LeaveCell
        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdDeduct.Row = eventArgs.row

        CalcPFESI()
        CalcGrossSalary()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub sprdDeduct_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdDeduct.Leave
        'With sprdDeduct
        '    sprdDeduct_LeaveCell(sprdDeduct, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub
    Private Sub sprdEarn_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdEarn.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdEarn_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdEarn.LeaveCell
        On Error GoTo ErrPart
        Dim xPer As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        CalcEarn()
        CalcPFESI()
        CalcPerks()
        CalcGrossSalary()
        Exit Sub
ErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub sprdEarn_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdEarn.Leave
        'With sprdEarn
        '    sprdEarn_LeaveCell(sprdEarn, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub
    Private Sub sprdPerks_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdPerks.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdPerks_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdPerks.LeaveCell
        On Error GoTo ErrPart
        Dim xPer As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        CalcPerks()
        CalcGrossSalary()

        Exit Sub
ErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub sprdPerks_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdPerks.Leave
        'With sprdPerks
        '    sprdPerks_LeaveCell(sprdPerks, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        Dim xMonth As Short
        Dim xYear As Short

        Sqlstr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtEmpNo.Text = SprdView.Text

        TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(True))

        SprdView.Col = 6
        SprdView.Row = SprdView.ActiveRow
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")


        txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(True))

        '    Call ShowSalary(txtEmpNo.Text, Format(txtWEF.Text, "MMMYYYY"))
        If Val(txtBSalary.Text) <> 0 Then
            CalcGrossSalary()
        End If
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub


    Private Sub txtAddDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddDays.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAddDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAddDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAddDays_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAddDays.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        '
        '    If Val(txtAddDays) > 31 Then
        '        If RsCompany.Fields("COMPANY_CODE").Value <> 5 And RsCompany.Fields("COMPANY_CODE").Value <> 10 Then
        '            MsgInformation "Additional Days Cann't be Greater Than 31."
        '            Cancel = True
        '        End If
        '    End If

        If Val(txtAddDays.Text) < 0 Then
            MsgInformation("Additional Days Cann't be less Than 0.")
            Cancel = True
        End If

        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBSalary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBSalary_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBSalary.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Val(txtBSalary.Text) = 0 Then GoTo EventExitSub

        CalcEarn()
        CalcPFESI()
        CalcPerks()
        CalcGrossSalary()

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPrevForm1BSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrevForm1BSalary.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPrevForm1BSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrevForm1BSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPrevForm1BSalary_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPrevForm1BSalary.Validating
        '        Dim Cancel As Boolean = eventArgs.Cancel

        '        If Val(txtPrevForm1BSalary.Text) = 0 Then GoTo EventExitSub

        '        CalcEarn()
        '        CalcPFESI()
        '        CalcPerks()
        '        CalcGrossSalary()

        'EventExitSub:
        '        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtForm1BSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtForm1BSalary.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtForm1BSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtForm1BSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtForm1BSalary_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtForm1BSalary.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Val(txtForm1BSalary.Text) = 0 Then GoTo EventExitSub

        CalcEarn()
        CalcPFESI()
        CalcPerks()
        CalcGrossSalary()

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtForm1GSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtForm1GSalary.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtForm1GSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtForm1GSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtForm1NetSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtForm1NetSalary.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtForm1NetSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtForm1NetSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtForm1CTC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtForm1CTC.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtForm1CTC_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtForm1CTC.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCTC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCTC.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCTC_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCTC.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtEmpNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEmpNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtGSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSalary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtdeduction_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeduction.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtdeduction_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeduction.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNetSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetSalary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNetSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub cboMonth_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMonth.SelectedIndexChanged
        '    MainClass.SaveStatus Me, ADDMode, MODIFYMode
        '    If Trim(TxtName.Text) = "" Then Exit Sub
        '
        '    If ADDMode = True Then Exit Sub

    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmSalIncrement_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Sqlstr = ""
        If FormActive = True Then Exit Sub
        Sqlstr = "Select * From PAY_SalaryDef_MST Where 1<>1"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        cboMonth.Text = MonthName(Month(RunDate))
        cboYear.Text = CStr(Year(RunDate))
        cboAppMon.Text = MonthName(Month(RunDate))
        cboAppYear.Text = CStr(Year(RunDate))
        settextlength()
        txtPreBSalary.Visible = False
        lblPBasicSal.Visible = False
        Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Resume
    End Sub
    Private Sub frmSalIncrement_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = 0
        Me.Top = 0

        Call FillComboMst()
        FormatSprd(-1)
        txtPreBSalary.Enabled = False

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
    Private Sub frmSalIncrement_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsEmp = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub ShowPreviousSalary(ByRef xCode As String, ByRef xWEF As String)

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        Sqlstr = " SELECT * FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = True Then Exit Sub
        txtPreBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
        '    txtWEF.Text = Format(RsADD!SALARY_EFF_DATE, "DD/MM/YYYY")

        txtNextIncDate.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 12, CDate(VB6.Format(txtWEF.Text, "DD/MM/YYYY"))), "DD/MM/YYYY")

        If MainClass.ValidateWithMasterTable(Trim(RsADD.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            cbodesignation.Text = MasterNo
            lblDesg.Text = MasterNo
        End If


        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    .Col = ColDeductOn
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))

                    .Col = ColPer
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))
                End If
            Next
        End With


        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    '
                    .Col = ColDeductOn
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))

                    .Col = ColPer
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))
                End If
            Next
        End With

        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then

                    .Col = ColDeductOn
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))


                    .Col = ColPer
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                End If
            Next
        End With

    End Sub

    Private Sub RefreshPreviousSalary(ByRef xCode As String, ByRef xWEF As String)

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        Sqlstr = " SELECT * FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = True Then Exit Sub
        txtPreBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
        '    txtWEF.Text = Format(RsADD!SALARY_EFF_DATE, "DD/MM/YYYY")


        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                End If
            Next
        End With


        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                End If
            Next
        End With

        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                End If
            Next
        End With

    End Sub

    Private Sub ValidatePreviousSalary(ByVal xCode As String, ByVal xWEF As String)

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        SqlStr = " SELECT * FROM PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = True Then Exit Sub
        txtPreBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
        '    If MainClass.ValidateWithMasterTable(Trim(RsADD!EMP_DESG_CODE), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        cbodesignation.Text = MasterNo
        '        lblDesg.Caption = MasterNo
        '    End If
        '

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))
                End If
            Next
        End With


        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))
                End If
            Next
        End With

        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    .Col = ColPrevAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPrevForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))
                End If
            Next
        End With

    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer
        Dim mEmpDesg As String
        Dim EmpPFCont As String

        If RsEmp.EOF = False Then
            txtBSalary.Text = VB6.Format(RsEmp.Fields("BASICSALARY").Value, "0.00")
            txtPreBSalary.Text = VB6.Format(RsEmp.Fields("PREVIOUS_BASICSALARY").Value, "0.00")

            txtForm1BSalary.Text = VB6.Format(RsEmp.Fields("FORM1_BASICSALARY").Value, "0.00")
            txtPrevForm1BSalary.Text = VB6.Format(RsEmp.Fields("PREVIOUS_FORM1_BASICSALARY").Value, "0.00")

            txtWEF.Text = VB6.Format(RsEmp.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY")

            txtNextIncDate.Text = VB6.Format(RsEmp.Fields("NEXT_INC_DATE").Value, "DD/MM/YYYY")

            txtAddDays.Text = IIf(IsDbNull(RsEmp.Fields("ADDDAYS_IN").Value) Or RsEmp.Fields("ADDDAYS_IN").Value = 0, "", RsEmp.Fields("ADDDAYS_IN").Value)

            cboMonth.Text = VB6.Format(RsEmp.Fields("SALARY_EFF_DATE").Value, "MMMM")
            cboYear.Text = VB6.Format(RsEmp.Fields("SALARY_EFF_DATE").Value, "YYYY")

            cboAppMon.Text = VB6.Format(RsEmp.Fields("SALARY_APP_DATE").Value, "MMMM")
            cboAppYear.Text = VB6.Format(RsEmp.Fields("SALARY_APP_DATE").Value, "YYYY")

            lblAppDate.Text = VB6.Format(RsEmp.Fields("SALARY_APP_DATE").Value, "DD/MM/YYYY")
            EmpPFCont = IIf(IsDbNull(RsEmp.Fields("EMP_CONT").Value), "B", RsEmp.Fields("EMP_CONT").Value)
            optContBasic.Checked = IIf(EmpPFCont = "B", True, False)
            optContCeiling.Checked = IIf(EmpPFCont = "C", True, False)
            optContGross.Checked = IIf(EmpPFCont = "G", True, False)
            optContCeilingGross.Checked = IIf(EmpPFCont = "E", True, False)

            If MainClass.ValidateWithMasterTable(RsEmp.Fields("EMP_CODE").Value, "EMP_CODE", "EMP_DESG_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpDesg = IIf(IsDBNull(MasterNo), "-1", MasterNo)
                If MainClass.ValidateWithMasterTable(mEmpDesg, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDesg.Text = MasterNo
                End If
            End If

            If MainClass.ValidateWithMasterTable(Trim(RsEmp.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                cbodesignation.Text = MasterNo
            End If

            If Not IsDbNull(RsEmp.Fields("ARREAR_DATE").Value) Then
                cboArrearMonth.Text = VB6.Format(RsEmp.Fields("ARREAR_DATE").Value, "MMMM")
                cboArrearYear.Text = VB6.Format(RsEmp.Fields("ARREAR_DATE").Value, "YYYY")
            Else
                cboArrearMonth.Text = VB6.Format(RsEmp.Fields("SALARY_APP_DATE").Value, "MMMM")
                cboArrearYear.Text = VB6.Format(RsEmp.Fields("SALARY_APP_DATE").Value, "YYYY")
            End If

            If CheckSalaryMade((txtEmpNo.Text), VB6.Format(lblAppDate.Text, "DD/MM/YYYY")) = True Then
                cboAppMon.Enabled = False
                cboAppYear.Enabled = False

                cboArrearMonth.Enabled = False
                cboArrearYear.Enabled = False
            End If

            With sprdEarn
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = 1
                    mTypeCode = Val(.Text)

                    RsEmp.MoveFirst()

                    Do While RsEmp.EOF = False
                        If mTypeCode = RsEmp.Fields("ADD_DEDUCTCODE").Value Then
                            Exit Do
                        End If
                        RsEmp.MoveNext()
                    Loop

                    If RsEmp.EOF = False Then
                        .Col = ColDeductOn
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("AMOUNT_DEDUCT_ON").Value), "", RsEmp.Fields("AMOUNT_DEDUCT_ON").Value))

                        .Col = ColPer
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PERCENTAGE").Value), "", RsEmp.Fields("PERCENTAGE").Value))

                        .Col = ColAmt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("Amount").Value), "", RsEmp.Fields("Amount").Value))

                        .Col = ColPrevAmt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PREVIOUS_AMOUNT").Value), "", RsEmp.Fields("PREVIOUS_AMOUNT").Value))

                        .Col = ColForm1Amt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("FORM1_AMOUNT").Value), "", RsEmp.Fields("FORM1_AMOUNT").Value))

                        .Col = ColPrevForm1Amt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PREVIOUS_FORM1_AMOUNT").Value), "", RsEmp.Fields("PREVIOUS_FORM1_AMOUNT").Value))

                    Else
                        .Col = ColDeductOn
                        .Text = "0.00"

                        .Col = ColPer
                        .Text = "0.00"

                        .Col = ColAmt
                        .Text = "0.00"

                        .Col = ColPrevAmt
                        .Text = "0.00"

                        .Col = ColForm1Amt
                        .Text = "0.00"

                        .Col = ColPrevForm1Amt
                        .Text = "0.00"

                    End If
                Next
            End With

            cntRow = 1
            With sprdDeduct
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = 1
                    mTypeCode = Val(.Text)

                    RsEmp.MoveFirst()

                    Do While RsEmp.EOF = False
                        If mTypeCode = RsEmp.Fields("ADD_DEDUCTCODE").Value Then
                            Exit Do
                        End If
                        RsEmp.MoveNext()
                    Loop

                    If RsEmp.EOF = False Then
                        .Col = ColDeductOn
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("AMOUNT_DEDUCT_ON").Value), "", RsEmp.Fields("AMOUNT_DEDUCT_ON").Value))

                        .Col = ColPer
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PERCENTAGE").Value), "", RsEmp.Fields("PERCENTAGE").Value))

                        .Col = ColAmt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("Amount").Value), "", RsEmp.Fields("Amount").Value))

                        .Col = ColPrevAmt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PREVIOUS_AMOUNT").Value), "", RsEmp.Fields("PREVIOUS_AMOUNT").Value))

                        .Col = ColForm1Amt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("FORM1_AMOUNT").Value), "", RsEmp.Fields("FORM1_AMOUNT").Value))

                        .Col = ColPrevForm1Amt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PREVIOUS_FORM1_AMOUNT").Value), "", RsEmp.Fields("PREVIOUS_FORM1_AMOUNT").Value))
                    Else

                        .Col = ColDeductOn
                        .Text = "0.00"
                        .Col = ColPer
                        .Text = "0.00"

                        .Col = ColAmt
                        .Text = "0.00"

                        .Col = ColPrevAmt
                        .Text = "0.00"

                        .Col = ColForm1Amt
                        .Text = "0.00"

                        .Col = ColPrevForm1Amt
                        .Text = "0.00"
                    End If
                Next
            End With

            cntRow = 1
            With sprdPerks
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = 1
                    mTypeCode = Val(.Text)

                    RsEmp.MoveFirst()

                    Do While RsEmp.EOF = False
                        If mTypeCode = RsEmp.Fields("ADD_DEDUCTCODE").Value Then
                            Exit Do
                        End If
                        RsEmp.MoveNext()
                    Loop

                    If RsEmp.EOF = False Then
                        .Col = ColDeductOn
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("AMOUNT_DEDUCT_ON").Value), "", RsEmp.Fields("AMOUNT_DEDUCT_ON").Value))

                        .Col = ColPer
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PERCENTAGE").Value), "", RsEmp.Fields("PERCENTAGE").Value))

                        .Col = ColAmt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("Amount").Value), "", RsEmp.Fields("Amount").Value))

                        .Col = ColPrevAmt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PREVIOUS_AMOUNT").Value), "", RsEmp.Fields("PREVIOUS_AMOUNT").Value))

                        .Col = ColForm1Amt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("FORM1_AMOUNT").Value), "", RsEmp.Fields("FORM1_AMOUNT").Value))

                        .Col = ColPrevForm1Amt
                        .Text = CStr(IIf(IsDBNull(RsEmp.Fields("PREVIOUS_FORM1_AMOUNT").Value), "", RsEmp.Fields("PREVIOUS_FORM1_AMOUNT").Value))
                    Else


                        .Col = ColDeductOn
                        .Text = "0.00"

                        .Col = ColPer
                        .Text = "0.00"

                        .Col = ColAmt
                        .Text = "0.00"

                        .Col = ColPrevAmt
                        .Text = "0.00"

                        .Col = ColForm1Amt
                        .Text = "0.00"

                        .Col = ColPrevForm1Amt
                        .Text = "0.00"
                    End If
                Next
            End With

            RsEmp.MoveFirst()
            ADDMode = False
            MODIFYMode = False
        End If

        Call RefreshPreviousSalary((txtEmpNo.Text), (txtWEF.Text))

        FormatSprd(-1)
        SSTab1.SelectedIndex = 0
        txtBSalary.Enabled = True
        txtForm1BSalary.Enabled = True
        txtWEF.Enabled = False ''22-10-2011
        CalcEarn()
        CalcPFESI()
        CalcPerks()
        CalcGrossSalary()

        MainClass.UnProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColPrevForm1Amt)
        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColPrevAmt, ColPrevForm1Amt)

        MainClass.UnProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColPrevForm1Amt)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColPrevAmt, ColPrevForm1Amt)

        MainClass.UnProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColPrevForm1Amt)
        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColPrevAmt, ColPrevForm1Amt)

        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Exit Sub
ERR1:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        CalcAddDeduct()
        CalcPFESI()
        If Update1 = True Then
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mArrearCalc As String
        Dim mAppDate As Date
        Dim mWef As Date
        Dim mArrearDate As Date
        Dim mArrMon As Integer
        Dim mEmpDesgCode As String
        Dim mSqlStr As String

        Sqlstr = ""
        PubDBCn.BeginTrans()



        mAppDate = CDate("01/" & MonthValue((cboAppMon.Text)) & "/" & Val(cboAppYear.Text))
        mWef = CDate("01/" & MonthValue((cboMonth.Text)) & "/" & Val(cboYear.Text))
        mArrearDate = CDate("01/" & MonthValue((cboArrearMonth.Text)) & "/" & Val(cboArrearYear.Text))

        If MainClass.ValidateWithMasterTable((cbodesignation.Text), "DESG_DESC", "DESG_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpDesgCode = MasterNo
        End If

        mArrMon = DateDiff(Microsoft.VisualBasic.DateInterval.Month, mWef, mAppDate)

        If mArrMon + Val(txtAddDays.Text) > 0 Then
            '        If MsgQuestion("Are you want to calculate " & mArrMon & " Month Arrear? ") = vbYes Then
            mArrearCalc = "Y"
            '        Else
            '            mArrearCalc = "N"
            '        End If
        Else
            mArrearCalc = "N"
        End If

        If UpdateSalaryDef((txtEmpNo.Text), CStr(mWef), Val(txtBSalary.Text), Val(txtPreBSalary.Text), Val(txtForm1BSalary.Text), Val(txtPrevForm1BSalary.Text), CStr(mAppDate), CStr(mArrearDate), mArrMon, mArrearCalc, mEmpDesgCode) = False Then GoTo UpdateError


        mSqlStr = "UPDATE PAY_EMPLOYEE_MST SET EMP_DESG_CODE='" & mEmpDesgCode & "' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'"

        PubDBCn.Execute(mSqlStr)

        If MainClass.ValidateWithMasterTable((cbodesignation.Text), "DESG_DESC", "DESG_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DESG_CAT IN ('M','D')") = True Then
            mSqlStr = "UPDATE PAY_EMPLOYEE_MST SET OVERTIME_APP='0' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'"

            PubDBCn.Execute(mSqlStr)
        End If

        PubDBCn.CommitTrans()
        If mArrMon + Val(txtAddDays.Text) > 0 Then
            MsgInformation(mArrMon & " Month " & Val(txtAddDays.Text) & " Days Arrear Also Calculated.")
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
    Private Sub TxtEmpNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtEmpNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim xAmount As Decimal
        Dim mAppDate As Date
        Dim mWef As Date
        Dim mArrearDate As Date
        Dim mESICeiling As Double
        Dim mESIAmount As Double
        Dim mEmpCategory As String

        FieldsVarification = True

        lblAppDate.Text = "01/" & MonthValue((cboAppMon.Text)) & "/" & cboAppYear.Text

        If Trim(TxtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            TxtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Employee Code is empty. Cannot Save")
            txtEmpNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cbodesignation.Text) = "" Then
            MsgInformation("Designation Cann't be Blank")
            If cbodesignation.Enabled = True Then cbodesignation.Focus()
            FieldsVarification = False
            Exit Function
        End If

        mEmpCategory = "S"
        If MainClass.ValidateWithMasterTable(Trim(cbodesignation.Text), "DESG_DESC", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpCategory = MasterNo
        End If

        If lblEmpType.Text = "W" Then

        Else

            If PubPayCorpUser = "N" Then
                MsgInformation("You have not Rights to change Increment of this Employee.")
                FieldsVarification = False
                Exit Function
            End If

        End If

        If Not IsNumeric(txtBSalary.Text) Then
            MsgInformation("Invaild Basic Salary.")
            txtBSalary.Focus()
            FieldsVarification = False
            Exit Function
        End If

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
        mWef = CDate("01/" & MonthValue((cboMonth.Text)) & "/" & Val(cboYear.Text))
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
                If cboAppMon.Enabled = True Then cboAppMon.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtNextIncDate.Text) = "" Then
            MsgInformation("Next Increment Date Cann't be Blank.")
            If txtNextIncDate.Enabled = True Then txtNextIncDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If mWEF >= CVDate(txtNextIncDate.Text) Then
        '        MsgInformation "Next Increment Cann't be less or equal then WEF Date."
        '        If txtNextIncDate.Enabled = True Then txtNextIncDate.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        '    If PubUserID <> "G0416" Then
        If mWef < CDate(GetEmpLastIncrement(RsCompany.Fields("COMPANY_CODE").Value, Trim(txtEmpNo.Text), "SALARY_EFF_DATE")) Then
            MsgInformation("You cann't be update back date Increment. Check WEF Date")
            FieldsVarification = False
            Exit Function
        End If


        If mAppDate < CDate(GetEmpLastIncrement(RsCompany.Fields("COMPANY_CODE").Value, Trim(txtEmpNo.Text), "SALARY_APP_DATE")) Then
            MsgInformation("You cann't be update back date Increment. Check Applicable Date")
            FieldsVarification = False
            Exit Function
        End If
        '    End If
        If PubUserID <> "G0416" Then
            If CheckSalaryMade((txtEmpNo.Text), VB6.Format(lblAppDate.Text, "DD/MM/YYYY")) = True Then
                MsgInformation("Salary already Made Againt This Month. So Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If

        Call ValidatePreviousSalary((txtEmpNo.Text), (txtWEF.Text))
        CalcEarn()
        Call CalcPFESI(mESIAmount)
        CalcPerks()
        CalcGrossSalary()
        mESICeiling = CheckESICeiling(mWef)

        If mESIAmount > 0 And Val(VB6.Format(txtGSalary.Text, "0.00")) > 0 Then
            If Val(VB6.Format(txtGSalary.Text, "0.00")) > CheckESICeiling(CDate(txtWEF.Text)) Then
                If MsgBox("Please Check ESI Amount.Gross Amount is greater than ESI Ceiling ... " & vbNewLine & vbNewLine & "Want To Process ...", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    FieldsVarification = False
                    Exit Function
                End If
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
        '    Resume
    End Function

    Private Sub settextlength()

        On Error GoTo ERR1
        TxtName.Maxlength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)

        txtEmpNo.Maxlength = RsEmp.Fields("EMP_CODE").DefinedSize
        txtBSalary.MaxLength = RsEmp.Fields("BASICSALARY").Precision
        txtForm1BSalary.MaxLength = RsEmp.Fields("FORM1_BASICSALARY").Precision
        txtNextIncDate.Maxlength = 10
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        MainClass.ClearGrid(SprdView)
        SqlStr = " SELECT DISTINCT EMP.EMP_CODE,EMP.EMP_NAME AS NAME, EMP.EMP_DOJ,SALARYDEF.PREVIOUS_BASICSALARY AS Previous_Basic,SALARYDEF.BASICSALARY , " & vbCrLf & " SALARYDEF.SALARY_EFF_DATE AS WEF, SALARY_APP_DATE AS APP_DATE, ADDDAYS_IN AS ADD_DAYS, SALARYDEF.NEXT_INC_DATE AS DUE, DECODE(SALARYDEF.EMP_CONT,'B','BASIC','CEILING') AS PF_CONT " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_SALARYDEF_MST SALARYDEF " & vbCrLf & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.COMPANY_CODE=SALARYDEF.COMPANY_CODE  " & vbCrLf & " AND EMP.EMP_CODE=SALARYDEF.EMP_CODE  "

        If lblEmpType.Text = "S" Then
            Sqlstr = Sqlstr & vbCrLf & " AND EMP_CAT_TYPE='1'"
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND EMP_CAT_TYPE='2'"
        End If

        Sqlstr = Sqlstr & vbCrLf & " ORDER BY SALARY_APP_DATE, SALARYDEF.SALARY_EFF_DATE, EMP.EMP_CODE, EMP.EMP_NAME"


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 576 * 0)
            .set_ColWidth(1, 576 * 1.5)
            .set_ColWidth(2, 576 * 4)
            .set_ColWidth(3, 576 * 2)
            .set_ColWidth(4, 576 * 2)
            .set_ColWidth(5, 576 * 2)
            .set_ColWidth(6, 576 * 2)
            .set_ColWidth(7, 576 * 2)
            .set_ColWidth(8, 576 * 2)
            .set_ColWidth(9, 576 * 2)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        Sqlstr = ""

        '     If MainClass.ValidateWithMasterTable(txtEmpNo.Text, "EMP_CODE", "EMP_CODE", "PAY_SAL_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        MsgBox "Salary Exists Against This Employee."
        '        Delete1 = False
        '        Exit Function
        '    End If

        Sqlstr = "Delete from PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpNo.Text) & "'" & vbCrLf & " AND SALARY_EFF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        PubDBCn.Execute(Sqlstr)
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
    Private Sub TxtEmpNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mName As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mDesgName As String

        If Trim(txtEmpNo.Text) = "" Then GoTo EventExitSub

        txtEmpNo.Text = VB6.Format(txtEmpNo.Text, "000000")
        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "' "

        If lblEmpType.Text = "S" Then
            Sqlstr = Sqlstr & vbCrLf & " AND EMP_CAT_TYPE='1'"
        Else
            Sqlstr = Sqlstr & vbCrLf & " AND EMP_CAT_TYPE='2'"
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        Sqlstr = ""

        If RS.EOF = False Then
            Clear1()
            txtEmpNo.Text = RS.Fields("EMP_CODE").Value
            TxtName.Text = IIf(IsDbNull(RS.Fields("EMP_NAME").Value), "", RS.Fields("EMP_NAME").Value)

            If MainClass.ValidateWithMasterTable(Trim(RS.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDesgName = MasterNo
                cbodesignation.Text = mDesgName
            End If

            xCode = RS.Fields("EMP_CODE").Value
            txtWEF.Focus()
        Else
            MsgBox("This Employee Code does not exsits in Master.", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:

        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FillComboMst()


        cbodesignation.Items.Clear()

        MainClass.FillCombo(cbodesignation, "PAY_DESG_MST", "DESG_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillSalarySprd()

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mDate As String

        MainClass.ClearGrid(sprdEarn, -1)
        MainClass.ClearGrid(sprdDeduct, -1)
        SSTab1.SelectedIndex = 1
        MainClass.ClearGrid(sprdPerks, -1)

        If Trim(txtWEF.Text) = "" Then
            mDate = VB6.Format(RunDate, "DD-MMM-YYYY")
        Else
            mDate = VB6.Format("01/" & Trim(cboAppMon.Text) & "/" & Trim(cboAppYear.Text), "DD-MMM-YYYY")
        End If

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf _
            & " AND CODE IN (" & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & "," & ConPerks & ")" & vbCrLf _
            & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " UNION " & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & "," & ConPerks & ")" & vbCrLf _
            & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = Sqlstr & vbCrLf & "ORDER BY ADDDEDUCT,SEQ "

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            With RsADD
                Do While Not .EOF
                    If .Fields("ADDDEDUCT").Value = ConEarning Then
                        With sprdEarn
                            .Row = .MaxRows
                            .Col = ColCode
                            .Text = CStr(RsADD.Fields("Code").Value)

                            .Col = ColDesc
                            .Text = RsADD.Fields("Name").Value

                            .Col = ColPer
                            .Text = CStr(RsADD.Fields("PERCENTAGE").Value)
                        End With
                    ElseIf .Fields("ADDDEDUCT").Value = ConDeduct Then
                        With sprdDeduct
                            .Row = .MaxRows

                            .Col = ColCode
                            .Text = CStr(RsADD.Fields("Code").Value)

                            .Col = ColDesc
                            .Text = RsADD.Fields("Name").Value

                            .Col = ColPer
                            .Text = CStr(RsADD.Fields("PERCENTAGE").Value)
                        End With
                    ElseIf .Fields("ADDDEDUCT").Value = ConPerks Then
                        With sprdPerks
                            .Row = .MaxRows

                            .Col = ColCode
                            .Text = CStr(RsADD.Fields("Code").Value)

                            .Col = ColDesc
                            .Text = RsADD.Fields("Name").Value

                            .Col = ColPer
                            .Text = CStr(RsADD.Fields("PERCENTAGE").Value)
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
                        ElseIf .Fields("ADDDEDUCT").Value = ConPerks Then
                            sprdPerks.Col = 1
                            sprdPerks.Row = sprdPerks.MaxRows
                            If Trim(sprdPerks.Text) <> "" Then
                                sprdPerks.MaxRows = sprdPerks.MaxRows + 1
                                If sprdPerks.MaxRows > 3 Then
                                    sprdPerks.set_ColWidth(ColDesc, 14)
                                End If
                            End If
                        End If
                    End If
                Loop
            End With
        End If

        SSTab1.SelectedIndex = 0
        '    Call FormatSprd(-1)

        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColPrevAmt, ColPrevForm1Amt)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColPrevAmt, ColPrevForm1Amt)

        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColPrevAmt, ColPrevForm1Amt)

    End Sub
    Private Sub CopyPreviousSalary()
        'xCode As Long, xMonth As Integer, xYear As Integer
        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        xSubkey = CInt(VB6.Format(cboYear.Text, "0000") & VB6.Format(MonthValue((cboMonth.Text)), "00"))

        '    SqlStr = " SELECT * from SalaryDef " & vbCrLf _
        ''                    & " WHERE " & vbCrLf _
        ''                    & " SubKey=(SELECT MAX(SubKey) From SalaryDef " & vbCrLf _
        ''                    & " WHERE SubKey<= " & xSubkey & " AND Code=" & Val(lblcode.Caption) & " AND " & vbCrLf _
        ''                    & " COMPANYCODE=" & RsCompany!CompanyCode & " " & vbCrLf _
        ''                    & " ) AND " & vbCrLf _
        ''                    & " SalaryDef.Code=" & Val(lblcode.Caption) & "" & vbCrLf _
        ''                    & " AND COMPANYCODE=" & RsCompany!CompanyCode & ""

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            txtPreBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
            txtPrevForm1BSalary.Text = VB6.Format(RsADD.Fields("FORM1_BASICSALARY").Value, "0.00")

            '        txtBSalary.Text = Format(RsADD!BASICSALARY, "0.00")
            txtWEF.Text = MonthName(RsADD.Fields("Sal_Month").Value) & ", " & RsADD.Fields("SAL_YEAR").Value
            lblWEF.Text = RsADD.Fields("SubKey").Value
            cboAppMon.Text = MonthName(RsADD.Fields("AppSal_Month").Value)
            cboAppYear.Text = RsADD.Fields("AppSal_Year").Value

            If Val(RsADD.Fields("ArrearMonth").Value) <> 0 Then
                cboArrearMonth.Text = MonthName(RsADD.Fields("ArrearMonth").Value)
            Else
                cboArrearMonth.SelectedIndex = -1
            End If
            If Val(RsADD.Fields("ArrearYear").Value) <> 0 Then
                cboArrearYear.Text = RsADD.Fields("ArrearYear").Value
            Else
                cboArrearYear.SelectedIndex = -1
            End If


            Do While Not RsADD.EOF
                With sprdEarn
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = 1
                        mTypeCode = Val(.Text)
                        If RsADD.Fields("ADD_DEDUCTCODE").Value = mTypeCode Then
                            .Row = cntRow

                            .Col = ColDeductOn
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))


                            .Col = ColPer
                            .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                            '                        .Col = ColAmt
                            '                        .Text = CStr(IIf(IsNull(RsADD!AMOUNT), "", RsADD!AMOUNT))

                            .Col = ColPrevAmt
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                            .Col = ColPrevForm1Amt
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))
                        End If
                    Next
                End With
                With sprdDeduct
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = 1
                        mTypeCode = Val(.Text)
                        If RsADD.Fields("ADD_DEDUCTCODE").Value = mTypeCode Then
                            .Row = cntRow

                            .Col = ColDeductOn
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))

                            .Col = ColPer
                            .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                            .Col = ColAmt
                            .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                            .Col = ColForm1Amt
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                            .Col = ColPrevAmt
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                            .Col = ColPrevForm1Amt
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))
                        End If
                    Next
                End With
                With sprdPerks
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = 1
                        mTypeCode = Val(.Text)
                        If RsADD.Fields("ADD_DEDUCTCODE").Value = mTypeCode Then
                            .Row = cntRow
                            .Col = ColDeductOn
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))


                            .Col = ColPer
                            .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                            .Col = ColAmt
                            .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                            .Col = ColForm1Amt
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                            .Col = ColPrevAmt
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                            .Col = ColPrevForm1Amt
                            .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))
                        End If
                    Next
                End With
                RsADD.MoveNext()
            Loop
        End If

    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdEarn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 17)


            .Col = ColDeductOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDeductOn, 6)
            .ColHidden = True


            .Col = ColDeductOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDeductOn, 6)
            .ColHidden = False

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
            .set_ColWidth(ColAmt, 9)

            .Col = ColPrevAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPrevAmt, 9)

            .Col = ColForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
            .set_ColWidth(ColForm1Amt, 9)

            .Col = ColPrevForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
            .set_ColWidth(ColPrevForm1Amt, 9)

        End With

        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColPer)
        MainClass.SetSpreadColor(sprdEarn, mRow)

        With sprdDeduct

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 17)


            .Col = ColDeductOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDeductOn, 6)
            .ColHidden = False

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
            .set_ColWidth(ColAmt, 9)

            .Col = ColPrevAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPrevAmt, 9)

            .Col = ColForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
            .set_ColWidth(ColForm1Amt, 9)

            .Col = ColPrevForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
            .set_ColWidth(ColPrevForm1Amt, 9)
        End With

        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColPer)
        MainClass.SetSpreadColor(sprdDeduct, mRow)

        ''********
        SSTab1.SelectedIndex = 1
        With sprdPerks

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 17)


            .Col = ColDeductOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDeductOn, 6)
            .ColHidden = True

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
            .set_ColWidth(ColAmt, 9)

            .Col = ColPrevAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPrevAmt, 9)

            .Col = ColForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
            .set_ColWidth(ColForm1Amt, 9)

            .Col = ColPrevForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
            .set_ColWidth(ColPrevForm1Amt, 9)
        End With

        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColPrevAmt, ColPrevForm1Amt)
        MainClass.SetSpreadColor(sprdPerks, mRow)

        SSTab1.SelectedIndex = 0
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Function UpdateSalaryDef(ByRef xCode As String, ByRef xWEF As String, ByRef xSalary As Double, ByRef xPreSalary As Double,
                                     ByRef xForm1Salary As Double, ByRef xPreForm1Salary As Double,
                                     ByRef xAppDate As String, ByRef xArrearDate As String, ByRef xTotArrearMonth As Integer, ByRef xArrearCalc As String, ByRef xEmpDesgCode As String) As Boolean

        On Error GoTo UpdateSalaryDefErr
        Dim SqlStr As String = ""
        Dim xTypeCode As Integer
        Dim cntRow As Integer
        Dim xAmount As Double
        Dim xPer As Double
        Dim xPrevAmt As Double
        Dim EmpPFCont As String
        Dim xDeductOn As Double
        Dim xForm1Amt As Double
        Dim xPrevForm1Amt As Double
        Dim mForm1Salary As Double

        If Trim(xCode) = "" Then
            UpdateSalaryDef = True
            Exit Function
        End If

        mForm1Salary = xForm1Salary

        If RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N" Then
            xForm1Salary = xSalary
            xPreForm1Salary = xPreSalary
        Else
            If Val(xForm1Salary) = 0 Then
                xForm1Salary = xSalary
            End If

            If Val(xPreForm1Salary) = 0 Then
                xPreForm1Salary = xPreSalary
            End If
        End If



        SqlStr = " DELETE FROM PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND TO_CHAR(SALARY_EFF_DATE,'MONYYYY')='" & UCase(VB6.Format(xWEF, "MMMYYYY")) & "'"


        PubDBCn.Execute(SqlStr)

        EmpPFCont = IIf(optContBasic.Checked = True, "B", IIf(optContGross.Checked = True, "G", IIf(optContCeiling.Checked = True, "C", "E")))

        SqlStr = ""

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextEarnRow
                xTypeCode = Val(.Text)

                ''   .Col = ColDeductOn
                ' .Text = CStr(IIf(IsDBNull(RsADD.Fields("").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))

                .Col = ColDeductOn
                xDeductOn = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPrevAmt
                xPrevAmt = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amt = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPrevForm1Amt
                xPrevForm1Amt = IIf(IsNumeric(.Text), .Text, 0)

                If RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N" Then
                    xForm1Amt = xAmount
                    xPrevForm1Amt = xPrevAmt
                Else
                    If mForm1Salary = 0 Then
                        xForm1Amt = xAmount
                        xPrevForm1Amt = xPrevAmt
                    End If
                End If


                SqlStr = " Insert Into PAY_SalaryDef_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                    & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf _
                    & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf _
                    & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf _
                    & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE,ADDDAYS_IN, " & vbCrLf _
                    & " EMP_CONT, ADDUSER, ADDDATE, NEXT_INC_DATE, " & vbCrLf _
                    & " FORM1_BASICSALARY, FORM1_AMOUNT, PREVIOUS_FORM1_BASICSALARY, PREVIOUS_FORM1_AMOUNT,AMOUNT_DEDUCT_ON" & vbCrLf _
                    & " ) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xPreSalary & "," & vbCrLf _
                    & " " & xPrevAmt & ",TO_DATE('" & VB6.Format(xArrearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xTotArrearMonth & ", '" & xArrearCalc & "','" & xEmpDesgCode & "'," & vbCrLf _
                    & " " & Val(txtAddDays.Text) & ", '" & EmpPFCont & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(txtNextIncDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & xForm1Salary & ", " & xForm1Amt & ", " & xPreForm1Salary & ", " & xPrevForm1Amt & "," & xDeductOn & "" & vbCrLf _
                    & " )"

                PubDBCn.Execute(SqlStr)
NextEarnRow:
            Next
        End With

        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextDeductRow
                xTypeCode = Val(.Text)

                .Col = ColDeductOn
                xDeductOn = IIf(IsNumeric(.Text), .Text, 0)


                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPrevAmt
                xPrevAmt = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amt = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPrevForm1Amt
                xPrevForm1Amt = IIf(IsNumeric(.Text), .Text, 0)

                If RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N" Then
                    xForm1Amt = xAmount
                    xPrevForm1Amt = xPrevAmt
                Else
                    If mForm1Salary = 0 Then
                        xForm1Amt = xAmount
                        xPrevForm1Amt = xPrevAmt
                    End If
                End If


                SqlStr = " Insert Into PAY_SalaryDef_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                    & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf _
                    & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf _
                    & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf _
                    & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR, EMP_DESG_CODE, ADDDAYS_IN, " & vbCrLf _
                    & " EMP_CONT, ADDUSER, ADDDATE, NEXT_INC_DATE, " & vbCrLf _
                    & " FORM1_BASICSALARY, FORM1_AMOUNT, PREVIOUS_FORM1_BASICSALARY, PREVIOUS_FORM1_AMOUNT,AMOUNT_DEDUCT_ON" & vbCrLf _
                    & " ) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xPreSalary & "," & vbCrLf _
                    & " " & xPrevAmt & ",TO_DATE('" & VB6.Format(xArrearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xTotArrearMonth & ", '" & xArrearCalc & "','" & xEmpDesgCode & "'," & vbCrLf _
                    & " " & Val(txtAddDays.Text) & ", '" & EmpPFCont & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(txtNextIncDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & xForm1Salary & ", " & xForm1Amt & ", " & xPreForm1Salary & ", " & xPrevForm1Amt & "," & xDeductOn & "" & vbCrLf _
                    & " )"

                PubDBCn.Execute(SqlStr)
NextDeductRow:
            Next
        End With


        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextPerksRow
                xTypeCode = Val(.Text)

                .Col = ColDeductOn
                xDeductOn = IIf(IsNumeric(.Text), .Text, 0)


                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPrevAmt
                xPrevAmt = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amt = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPrevForm1Amt
                xPrevForm1Amt = IIf(IsNumeric(.Text), .Text, 0)

                If RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N" Then
                    xForm1Amt = xAmount
                    xPrevForm1Amt = xPrevAmt
                Else
                    If mForm1Salary = 0 Then
                        xForm1Amt = xAmount
                        xPrevForm1Amt = xPrevAmt
                    End If
                End If

                SqlStr = " Insert Into PAY_SalaryDef_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                    & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf _
                    & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf _
                    & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf _
                    & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR, EMP_DESG_CODE,ADDDAYS_IN, " & vbCrLf _
                    & " EMP_CONT, ADDUSER, ADDDATE,NEXT_INC_DATE, " & vbCrLf _
                    & " FORM1_BASICSALARY, FORM1_AMOUNT, PREVIOUS_FORM1_BASICSALARY, PREVIOUS_FORM1_AMOUNT,AMOUNT_DEDUCT_ON" & vbCrLf _
                    & " ) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xPreSalary & "," & vbCrLf _
                    & " " & xPrevAmt & ",TO_DATE('" & VB6.Format(xArrearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xTotArrearMonth & ", '" & xArrearCalc & "','" & xEmpDesgCode & "'," & vbCrLf _
                    & " " & Val(txtAddDays.Text) & ", '" & EmpPFCont & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(txtNextIncDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & xForm1Salary & ", " & xForm1Amt & ", " & xPreForm1Salary & ", " & xPrevForm1Amt & "," & xDeductOn & "" & vbCrLf _
                    & " )"

                PubDBCn.Execute(SqlStr)
NextPerksRow:
            Next
        End With

        UpdateSalaryDef = True
        Exit Function
UpdateSalaryDefErr:
        'Resume
        UpdateSalaryDef = False
        MsgInformation(Err.Description)
    End Function
    Private Sub CalcGrossSalary()

        Dim mSalary As Double
        Dim mForm1Salary As Double
        Dim mEarn As Double
        Dim mDeduct As Double
        Dim cntRow As Integer
        Dim mPerks As Double

        Dim mForm1Earn As Double
        Dim mForm1Deduct As Double
        Dim mForm1Perks As Double

        mSalary = Val(txtBSalary.Text)
        mForm1Salary = Val(txtForm1BSalary.Text)

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mEarn = mEarn + Val(.Text)

                .Col = ColForm1Amt
                mForm1Earn = mForm1Earn + Val(.Text)

            Next
        End With

        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mDeduct = mDeduct + Val(.Text)

                .Col = ColForm1Amt
                mForm1Deduct = mForm1Deduct + Val(.Text)

            Next
        End With

        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mPerks = mPerks + Val(.Text)

                .Col = ColForm1Amt
                mForm1Perks = mForm1Perks + Val(.Text)
            Next
        End With

        txtGSalary.Text = MainClass.FormatRupees(Val(CStr(mSalary)) + Val(CStr(mEarn)))
        txtDeduction.Text = MainClass.FormatRupees(Val(CStr(mDeduct)))
        txtNetSalary.Text = MainClass.FormatRupees(Val(CStr(mSalary)) + Val(CStr(mEarn)) - Val(CStr(mDeduct)))
        txtCTC.Text = MainClass.FormatRupees(Val(CStr(mSalary)) + Val(CStr(mEarn)) + Val(CStr(mPerks)))

        txtForm1GSalary.Text = MainClass.FormatRupees(Val(CStr(mForm1Salary)) + Val(CStr(mForm1Earn)))
        'txtDeduction.Text = MainClass.FormatRupees(Val(CStr(mDeduct)))
        txtForm1NetSalary.Text = MainClass.FormatRupees(Val(CStr(mForm1Salary)) + Val(CStr(mForm1Earn)) - Val(CStr(mDeduct)))
        txtForm1CTC.Text = MainClass.FormatRupees(Val(CStr(mForm1Salary)) + Val(CStr(mForm1Earn)) + Val(CStr(mForm1Perks)))


    End Sub
    Private Sub CalcAddDeduct()
        Dim cntRow As Integer
        Dim xPer As Double

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPer
                xPer = Val(.Text)
                If xPer <> 0 Then
                    .Col = ColAmt
                    .Text = CStr(Val(txtBSalary.Text) * Val(CStr(xPer)) / 100)

                    .Col = ColForm1Amt
                    .Text = CStr(Val(txtForm1BSalary.Text) * Val(CStr(xPer)) / 100)
                End If
            Next
        End With
    End Sub
    Public Sub DataChanged()

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Function CalcBasicPFSalary(ByRef mType As Integer) As Double
        Dim cntRow As Integer
        Dim mCode As Integer
        Dim mPFCeiling As String

        CalcBasicPFSalary = IIf(IsNumeric(txtBSalary.Text), txtBSalary.Text, 0)
        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mCode = CInt(.Text)
                If mType = ConPF Or mType = ConVPFAllw Or mType = ConEmployerPF Then
                    If MainClass.ValidateWithMasterTable(mCode, "Code", "IncludedPF", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        If MasterNo = "Y" Then
                            .Col = ColAmt
                            CalcBasicPFSalary = CalcBasicPFSalary + IIf(IsNumeric(.Text), .Text, 0)
                        End If
                    End If
                ElseIf mType = ConESI Then
                    If MainClass.ValidateWithMasterTable(mCode, "Code", "IncludedESI", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        If MasterNo = "Y" Then
                            .Col = ColAmt
                            CalcBasicPFSalary = CalcBasicPFSalary + IIf(IsNumeric(.Text), .Text, 0)
                        End If
                    End If
                End If
            Next
        End With
        If mType = ConPF Or mType = ConVPFAllw Or mType = ConEmployerPF Then
            If CheckPFCeilingOn(txtWEF.Text) = "C" Then
                mPFCeiling = CheckPFCeiling(txtWEF.Text)
            Else
                mPFCeiling = CalcBasicPFSalary
            End If

            CalcBasicPFSalary = IIf(CalcBasicPFSalary >= mPFCeiling, mPFCeiling, CalcBasicPFSalary)
        End If

    End Function

    Private Function CalcBasicSalaryPart() As Double
        Dim cntRow As Integer
        Dim mCode As Integer

        CalcBasicSalaryPart = IIf(IsNumeric(txtBSalary.Text), txtBSalary.Text, 0)
        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mCode = CInt(.Text)

                If MainClass.ValidateWithMasterTable(mCode, "Code", "ISSALPART", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        .Col = ColAmt
                        CalcBasicSalaryPart = CalcBasicSalaryPart + IIf(IsNumeric(.Text), .Text, 0)
                    End If
                End If
            Next
        End With
    End Function

    Public Sub CalcPFESI(Optional ByRef mESIAmount As Double = 0)
        On Error GoTo ERR1
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        Dim mRounding As String
        Dim mRound As String
        Dim mBasicSal As Double
        Dim mPFCeiling As Double
        Dim mPFAmount As Double
        Dim mDeductOn As Double

        mESIAmount = 0
        For mcntRow = 1 To sprdDeduct.MaxRows
            sprdDeduct.Row = mcntRow

            sprdDeduct.Col = ColCode
            If sprdDeduct.Text = "" Then Exit Sub
            mCode = CInt(sprdDeduct.Text)
            If MainClass.ValidateWithMasterTable(mCode, "Code", "Type", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mType = MasterNo
            End If

            sprdDeduct.Col = ColDeductOn
            If mType = ConPF Then
                If optContBasic.Checked = True Then
                    sprdDeduct.Text = CStr(Val(txtBSalary.Text))
                ElseIf optContGross.Checked = True Then
                    sprdDeduct.Text = CStr(CalcBasicPFSalary(mType))
                ElseIf optContCeilingGross.Checked = True Then
                    'sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)

                    mBasicSal = CalcBasicPFSalary(mType)    ''CalcBasicPFSalary(mType)
                    mPFCeiling = CheckPFCeiling(CDate(txtWEF.Text))
                    mBasicSal = IIf(mBasicSal > mPFCeiling, mPFCeiling, mBasicSal)

                    sprdDeduct.Text = CStr(mBasicSal)

                Else
                    If Trim(txtWEF.Text) <> "" Then
                        mBasicSal = Val(txtBSalary.Text)    ''CalcBasicPFSalary(mType)
                        mPFCeiling = CheckPFCeiling(CDate(txtWEF.Text))
                        mBasicSal = IIf(mBasicSal > mPFCeiling, mPFCeiling, mBasicSal)

                        sprdDeduct.Text = CStr(mBasicSal)
                    End If
                End If
                mDeductOn = Val(sprdDeduct.Text)
            Else
                sprdDeduct.Text = CStr(CalcBasicPFSalary(mType))
                mDeductOn = Val(sprdDeduct.Text)
            End If



            sprdDeduct.Col = ColPer
            xPer = IIf(IsNumeric(sprdDeduct.Text), sprdDeduct.Text, 0)

            sprdDeduct.Col = ColAmt
            If xPer <> 0 Then
                If mType = ConPF Then
                    'If optContBasic.Checked = True Then
                    '    sprdDeduct.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                    'ElseIf optContGross.Checked = True Then
                    '    sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
                    'ElseIf optContCeilingGross.Checked = True Then
                    '    'sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)

                    '    mBasicSal = CalcBasicPFSalary(mType)    ''CalcBasicPFSalary(mType)
                    '    mPFCeiling = CheckPFCeiling(CDate(txtWEF.Text))
                    '    mBasicSal = IIf(mBasicSal > mPFCeiling, mPFCeiling, mBasicSal)

                    '    sprdDeduct.Text = CStr(xPer * mBasicSal / 100)

                    'Else
                    '    If Trim(txtWEF.Text) <> "" Then
                    '        mBasicSal = Val(txtBSalary.Text)    ''CalcBasicPFSalary(mType)
                    '        mPFCeiling = CheckPFCeiling(CDate(txtWEF.Text))
                    '        mBasicSal = IIf(mBasicSal > mPFCeiling, mPFCeiling, mBasicSal)

                    '        sprdDeduct.Text = CStr(xPer * mBasicSal / 100)
                    '    End If
                    'End If

                    mPFAmount = mDeductOn * xPer / 100
                    sprdDeduct.Text = CStr(mPFAmount)

                    sprdDeduct.Col = ColForm1Amt
                    sprdDeduct.Text = mPFAmount
                Else
                    sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
                End If
            End If


            If MainClass.ValidateWithMasterTable(mCode, "Code", "ROUNDING", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mRounding = MasterNo
            End If

            If mRounding = "0.05" Then
                sprdDeduct.Text = CStr(PaiseRound(Val(sprdDeduct.Text), 0.05))
            Else
                mRound = Replace(mRounding, "1", "0")
                sprdDeduct.Text = VB6.Format(Val(sprdDeduct.Text), mRound)
            End If

            If mType = ConESI Then
                mESIAmount = CDbl(sprdDeduct.Text)
            End If

        Next
        Exit Sub
ERR1:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Sub CalcEarn()
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        For mcntRow = 1 To sprdEarn.MaxRows
            sprdEarn.Row = mcntRow

            sprdEarn.Col = ColPer
            xPer = IIf(IsNumeric(sprdEarn.Text), sprdEarn.Text, 0)

            sprdEarn.Col = ColAmt
            If xPer <> 0 Then
                sprdEarn.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
            End If
        Next
    End Sub
    Public Sub CalcPerks()
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        Dim mBasicSalary As Double


        For mcntRow = 1 To sprdPerks.MaxRows
            sprdPerks.Row = mcntRow

            sprdPerks.Col = ColCode
            mCode = Val(sprdPerks.Text)

            If MainClass.ValidateWithMasterTable(mCode, "CODE", "NAME", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And TYPE=" & ConLTA & "") = True Then
                mBasicSalary = Val(txtBSalary.Text)
            Else
                mBasicSalary = CalcBasicSalaryPart()
            End If

            If MainClass.ValidateWithMasterTable(mCode, "Code", "Type", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mType = MasterNo
            End If

            ''
            sprdPerks.Col = ColPer
            xPer = IIf(IsNumeric(sprdPerks.Text), sprdPerks.Text, 0)

            sprdPerks.Col = ColAmt
            If xPer <> 0 Then
                If mType = ConEmployerPF Then
                    sprdPerks.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
                Else
                    sprdPerks.Text = CStr(xPer * Val(CStr(mBasicSalary)) / 100)
                End If

            End If
        Next
    End Sub
    Private Sub SearchWEF()
        Dim mProdCode As String
        SqlStr = ""

        '    If MainClass.SearchMaster("", "SalaryDef", "SubKey", "CODE=" & Val(lblcode) & "") = True Then
        '        lblWEF.Caption = AcName
        '        txtWEF.Text = MonthName(Mid(lblWEF, 5, 2)) & ", " & Mid(lblWEF, 1, 4)
        '    End If
        Exit Sub
    End Sub

    Private Sub txtNextIncDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNextIncDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNextIncDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNextIncDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xWEF As String
        Dim SqlStr As String = ""

        If Trim(txtNextIncDate.Text) = "" Then GoTo EventExitSub

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Employee Name Blank.")
            txtEmpNo.Focus()
            GoTo EventExitSub
        End If

        If Not IsDate(txtNextIncDate.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPreBSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreBSalary.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.DoubleClick
        Call cmdSearchSalary_Click(cmdSearchSalary, New System.EventArgs())
    End Sub

    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            Call cmdSearchSalary_Click(cmdSearchSalary, New System.EventArgs())
        End If
    End Sub

    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xWEF As String
        Dim SqlStr As String = ""
        Dim xPrevWEF As String

        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Employee Name Blank.")
            txtEmpNo.Focus()
            GoTo EventExitSub
        End If

        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
            GoTo EventExitSub
        End If


        txtWEF.Text = "01/" & VB6.Format(txtWEF.Text, "MM/YYYY")

        xPrevWEF = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        xWEF = VB6.Format(txtWEF.Text, "MMMYYYY")

        cboMonth.Text = VB6.Format(txtWEF.Text, "MMMM")
        cboYear.Text = VB6.Format(txtWEF.Text, "YYYY")

        If MODIFYMode = True And RsEmp.EOF = False Then xCode = RsEmp.Fields("EMP_CODE").Value

        SqlStr = " SELECT * FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " And EMP_CODE='" & txtEmpNo.Text & "'" & vbCrLf & " AND TO_CHAR(SALARY_EFF_DATE,'MONYYYY')='" & UCase(xWEF) & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsEmp.EOF = False Then

            Clear1()
            txtWEF.Text = VB6.Format(xPrevWEF, "DD/MM/YYYY")
            FillSalarySprd()
            Call Show1()
            If txtWEF.Enabled = True Then txtWEF.Focus()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Month, Use add Button to Generate New Increment.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            ElseIf MODIFYMode = True Then
                Sqlstr = "SELECT * FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'"
                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)
                GoTo EventExitSub
            End If
            Call ShowPreviousSalary((txtEmpNo.Text), (txtWEF.Text))
        End If
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

        Sqlstr = " SELECT * FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

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

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        Sqlstr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(Sqlstr)

        If FillDummyDataForPrint(sprdEarn, 1, sprdEarn.MaxRows, 0, sprdEarn.MaxCols, PubDBCn, 0, "E") = False Then GoTo ERR1
        If FillDummyDataForPrint(sprdPerks, 1, sprdPerks.MaxRows, 0, sprdPerks.MaxCols, PubDBCn, 1000, "P") = False Then GoTo ERR1

        PubDBCn.CommitTrans()

        'Select Record for print...

        Sqlstr = ""

        Sqlstr = FetchRecordForReport(Sqlstr)

        mSubTitle = ""
        mTitle = ""

        Call ShowReport(Sqlstr, "Increment.rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        PubDBCn.RollbackTrans()
        'Resume
    End Sub

    Public Function FillDummyDataForPrint(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer, ByRef mPvtDBCn As ADODB.Connection, ByRef mStartRowNo As Double, ByRef mType As String) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Double
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            Sqlstr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, FIELD58, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum + mStartRowNo & ", '" & mType & "'," & vbCrLf & " " & GetData & ") "
            mPvtDBCn.Execute(Sqlstr)
NextRec:
        Next
        FillDummyDataForPrint = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillDummyDataForPrint = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
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

        MainClass.AssignCRptFormulas(Report1, "mEmpName='" & MainClass.AllowSingleQuote(TxtName.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mEmpDegn='" & MainClass.AllowSingleQuote(lblDesg.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mWEF='" & MainClass.AllowSingleQuote(txtWEF.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mNextWEF='" & MainClass.AllowSingleQuote(mNextWEFStr) & "'")
        MainClass.AssignCRptFormulas(Report1, "mBasic='" & txtBSalary.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "mGrossAmount='" & txtGSalary.Text & "'")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
