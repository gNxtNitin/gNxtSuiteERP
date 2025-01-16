Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalVoucher
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

    Dim mPFRate As Double
    Dim mPFEPFRate As Double
    Dim mPFPensionRate As Double
    Dim mPFCeiling As Double
    Dim mESICeiling As Double
    Dim mESIRate As Double

    Private Const ColCode As Short = 1
    Private Const ColDesc As Short = 2
    Private Const ColActAmt As Short = 3
    Private Const ColPer As Short = 4
    Private Const ColAmt As Short = 5
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

        txtWEF.Text = ""
        txtBSalary.Text = ""
        txtGSalary.Text = ""
        txtDeduction.Text = ""
        txtNetSalary.Text = ""
        txtAtcBasic.Text = ""
        txtPerks.Text = ""
        '    MainClass.ClearGrid sprdEarn, -1
        '    MainClass.ClearGrid sprdDeduct, -1


        txtVNo.Text = ""
        txtVDate.Text = ""
        txtRemarks.Text = ""
        txtPaidDays.Text = CStr(0)
        txtSuspendPer.Text = CStr(0)
        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkApproved.Enabled = True

        cbodesignation.SelectedIndex = -1
        cboSalType.SelectedIndex = 0
        cboSalType.Enabled = True
        cmdAccountPosting.Enabled = False
        FillSalarySprd()
        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub cbodesignation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cbodesignation_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboSalType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSalType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboSalType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSalType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkApproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproved.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAccountPosting_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAccountPosting.Click
        Dim mVNo As String
        Dim mVDate As String
        Dim mBankCode As Integer
        Dim mYM As Integer
        Dim mBType As String
        Dim mBSType As String
        Dim mm As New frmAtrn
        Dim mVType As String
        Dim mVSeqNo As Integer
        Dim mVNoSuffix As String
        Dim mCategory As String
        Dim mDivisionCode As Double

        '    myMenu = "mnuJournal"
        mm.lblBookType.Text = ConJournal

        If Trim(txtEmpNo.Text) = "" Then Exit Sub
        If Trim(txtWEF.Text) = "" Then Exit Sub
        mm.txtVDate.Text = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        mYM = CInt(VB6.Format(Year(CDate(txtWEF.Text)), "0000") & VB6.Format(Month(CDate(txtWEF.Text)), "00"))
        mm.lblYM.Text = CStr(mYM)
        mBType = "Q"

        If MainClass.ValidateWithMasterTable((txtEmpNo.Text), "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBSType = MasterNo
        End If
        If MainClass.ValidateWithMasterTable((txtEmpNo.Text), "EMP_CODE", "DIV_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        End If
        mm.MdiParent = Me.MdiParent
        mm.lblSR.Text = mBType & mBSType & mDivisionCode

        mm.Show()
        If CheckSalVoucher(mYM, mVNo, mVDate, mVType, mVSeqNo, mVNoSuffix, Val(txtEmpNo.Text), mBType, mBSType, mDivisionCode) = True Then

            mm.frmAtrn_Activated(Nothing, New System.EventArgs())
            mm.txtVDate.Text = VB6.Format(mVDate, "dd/mm/yyyy")
            mm.txtVType.Text = mVType
            mm.txtVNo.Text = VB6.Format(mVSeqNo, "00000")
            mm.txtVNoSuffix.Text = mVNoSuffix
            mm.lblEmpCode.Text = Trim(txtEmpNo.Text)
            mm.txtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
            mm.CmdAdd.Enabled = False
        Else
            mm.frmAtrn_Activated(Nothing, New System.EventArgs())
            mm.txtVDate.Text = VB6.Format(txtWEF.Text, "DD/MM/YYYY") ''Format(MainClass.LastDay(Month(lblRunDate), Year(lblRunDate)) & "/" & vb6.Format(Month(lblRunDate), "00") & "/" & Year(lblRunDate), "dd/mm/yyyy")
            mm.lblEmpCode.Text = Trim(txtEmpNo.Text)
            mm.txtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click


        '    If CheckSalaryMade(txtEmpNo.Text, Format(lblAppDate.Caption, "DD/MM/YYYY")) = True Then
        '        MsgInformation " Salary Made Againt This Increment. So Cann't be Modified"
        '        Exit Sub
        '    End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            If RsEmp.Fields("APP_STATUS").Value = "Y" Then
                MsgInformation("Approved Voucher Cann't be Modify")
                Exit Sub
            Else
                ADDMode = False
                MODIFYMode = True
                MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            End If
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

        SqlStr = "SELECT " & vbCrLf & " DISTINCT TO_CHAR(SAL_DATE,'MONYYYY') AS WEF" & vbCrLf & " FROM PAY_SALVoucher_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpNo.Text) & "'"

        If MainClass.SearchGridMasterBySQL("WEF", SqlStr) = True Then
            txtWEF.Text = "01/" & MonthValue(Trim(Mid(AcName, 1, Len(AcName) - 4)), True) & "/" & VB.Right(LTrim(AcName), 4)
            txtWEF.Text = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
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
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAcPosting As String

        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub

        SqlStr = " SELECT APP_STATUS FROM PAY_SALVOUCHER_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(txtEmpNo.Text, "000000") & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(txtWEF.Text, "YYYYMM") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            mAcPosting = IIf(IsDbNull(RsTemp.Fields("APP_STATUS").Value), "N", RsTemp.Fields("APP_STATUS").Value)

            If mAcPosting = "Y" Then
                MsgInformation("Account Posting Done, so Cann't be Deleted.")
                Exit Sub
            End If
        End If

        If Not RsEmp.EOF Then
            If RsEmp.Fields("APP_STATUS").Value = "Y" Then
                MsgInformation("Approved Voucher Cann't be Deleted")
                Exit Sub
            Else
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
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim mProdCode As String
        SqlStr = ""

        If MainClass.SearchGridMaster((txtEmpNo.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpNo.Text = AcName1
            TxtName.Text = AcName
            TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub

    End Sub
    Private Sub frmSalVoucher_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub



    Private Sub sprdDeduct_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdDeduct.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdDeduct_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdDeduct.LeaveCell
        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdDeduct.Row = eventArgs.row

        CalcPFESI()
        CalcGrossSalary(("Y"))
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
    Private Sub sprdEarn_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdEarn.LeaveCell
        On Error GoTo ErrPart
        Dim xPer As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        CalcEarn()
        CalcPFESI()
        CalcPerks()
        CalcGrossSalary(("Y"))
        Exit Sub
ErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub sprdEarn_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdEarn.Leave
        With sprdEarn
            sprdEarn_LeaveCell(sprdEarn, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub sprdPerks_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdPerks.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub sprdPerks_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdPerks.LeaveCell
        On Error GoTo ErrPart
        Dim xPer As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        CalcPerks()
        CalcGrossSalary(("Y"))
        Exit Sub
ErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub


    Private Sub sprdPerks_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdPerks.Leave
        With sprdPerks
            sprdPerks_LeaveCell(sprdPerks, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        Dim xMonth As Short
        Dim xYear As Short

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtEmpNo.Text = SprdView.Text

        TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(True))

        SprdView.Col = 4
        SprdView.Row = SprdView.ActiveRow
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")


        txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(True))

        '    Call ShowSalary(txtEmpNo.Text, Format(txtWEF.Text, "MMMYYYY"))
        If Val(txtBSalary.Text) <> 0 Then
            CalcGrossSalary(("Y"))
        End If
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
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
        CalcPerks()
        CalcPFESI()
        CalcGrossSalary(("Y"))

EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmSalVoucher_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        SqlStr = "Select * From PAY_SalVOUCHER_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        settextlength()

        Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmSalVoucher_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        Call FillComboMst()
        FormatSprd(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmSalVoucher_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsEmp = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer
        Dim mEmpDesg As String
        Dim mAppStatus As String

        Call FillSalarySprd()

        If RsEmp.EOF = False Then
            txtBSalary.Text = VB6.Format(RsEmp.Fields("BASICSALARY").Value, "0.00")
            txtWEF.Text = VB6.Format(RsEmp.Fields("SAL_DATE").Value, "DD/MM/YYYY")

            txtPaidDays.Text = IIf(IsDbNull(RsEmp.Fields("PAIDDAYS").Value), 0, RsEmp.Fields("PAIDDAYS").Value)
            txtSuspendPer.Text = IIf(IsDbNull(RsEmp.Fields("SUSPEND_PER").Value), 0, RsEmp.Fields("SUSPEND_PER").Value)

            txtVNo.Text = IIf(IsDbNull(RsEmp.Fields("VNO").Value), "", RsEmp.Fields("VNO").Value)
            txtVNo.Text = VB6.Format(txtVNo.Text, "00000")
            txtVDate.Text = VB6.Format(IIf(IsDbNull(RsEmp.Fields("VDATE").Value), "", RsEmp.Fields("VDATE").Value), "DD/MM/YYYY")
            txtRemarks.Text = IIf(IsDbNull(RsEmp.Fields("Remarks").Value), "", RsEmp.Fields("Remarks").Value)
            mAppStatus = IIf(IsDbNull(RsEmp.Fields("APP_STATUS").Value), "N", RsEmp.Fields("APP_STATUS").Value)
            chkApproved.CheckState = IIf(mAppStatus = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkApproved.Enabled = IIf(mAppStatus = "Y", False, True)


            Call ShowAtcSalary((txtEmpNo.Text), (txtWEF.Text))

            If MainClass.ValidateWithMasterTable(RsEmp.Fields("EMP_CODE").Value, "EMP_CODE", "EMP_DESG_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpDesg = IIf(IsDBNull(MasterNo), "-1", MasterNo)
                If MainClass.ValidateWithMasterTable(mEmpDesg, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDesg.Text = MasterNo
                End If
            End If

            If MainClass.ValidateWithMasterTable(RsEmp.Fields("DESG_DESC").Value, "DESG_DESC", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                cbodesignation.Text = MasterNo
            End If

            If RsEmp.Fields("SAL_TYPE").Value = "S" Then
                cboSalType.SelectedIndex = 0
            ElseIf RsEmp.Fields("SAL_TYPE").Value = "O" Then
                cboSalType.SelectedIndex = 1
            ElseIf RsEmp.Fields("SAL_TYPE").Value = "A" Then
                cboSalType.SelectedIndex = 2
            ElseIf RsEmp.Fields("SAL_TYPE").Value = "B" Then
                cboSalType.SelectedIndex = 3
            End If

            With sprdEarn
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = 1
                    mTypeCode = Val(.Text)

                    RsEmp.MoveFirst()

                    Do While RsEmp.EOF = False
                        If mTypeCode = RsEmp.Fields("SALHEADCODE").Value Then
                            Exit Do
                        End If
                        RsEmp.MoveNext()
                    Loop

                    If RsEmp.EOF = False Then
                        .Col = ColPer
                        .Text = CStr(IIf(IsDbNull(RsEmp.Fields("PERCENTAGE").Value), "", RsEmp.Fields("PERCENTAGE").Value))

                        .Col = ColAmt
                        .Text = CStr(IIf(IsDbNull(RsEmp.Fields("PayableAmount").Value), "", RsEmp.Fields("PayableAmount").Value))
                    Else
                        .Col = ColPer
                        .Text = "0.00"

                        .Col = ColAmt
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
                        If mTypeCode = RsEmp.Fields("SALHEADCODE").Value Then
                            Exit Do
                        End If
                        RsEmp.MoveNext()
                    Loop

                    If RsEmp.EOF = False Then
                        .Col = ColPer
                        .Text = CStr(IIf(IsDbNull(RsEmp.Fields("PERCENTAGE").Value), "", RsEmp.Fields("PERCENTAGE").Value))

                        .Col = ColAmt
                        .Text = CStr(IIf(IsDbNull(RsEmp.Fields("PayableAmount").Value), "", RsEmp.Fields("PayableAmount").Value))
                    Else
                        .Col = ColPer
                        .Text = "0.00"

                        .Col = ColAmt
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
                        If mTypeCode = RsEmp.Fields("SALHEADCODE").Value Then
                            Exit Do
                        End If
                        RsEmp.MoveNext()
                    Loop

                    If RsEmp.EOF = False Then
                        .Col = ColPer
                        .Text = CStr(IIf(IsDbNull(RsEmp.Fields("PERCENTAGE").Value), "", RsEmp.Fields("PERCENTAGE").Value))

                        .Col = ColAmt
                        .Text = CStr(IIf(IsDbNull(RsEmp.Fields("PayableAmount").Value), "", RsEmp.Fields("PayableAmount").Value))
                    Else
                        .Col = ColPer
                        .Text = "0.00"

                        .Col = ColAmt
                        .Text = "0.00"
                    End If
                Next
            End With

            RsEmp.MoveFirst()
            ADDMode = False
            MODIFYMode = False
            cmdAccountPosting.Enabled = True
        End If

        FormatSprd(-1)
        cboSalType.Enabled = False
        txtBSalary.Enabled = True

        CalcEarn()
        CalcPerks()
        CalcPFESI()
        CalcGrossSalary(("Y"))

        MainClass.UnProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColAmt)
        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)

        MainClass.UnProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColAmt)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)

        MainClass.UnProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColAmt)
        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)

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

        Call CheckPFRates(CDate(VB6.Format(txtWEF.Text, "dd/mm/yyyy")))
        Call CheckESIRates(CDate(VB6.Format(txtWEF.Text, "dd/mm/yyyy")))

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
        Dim mCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mSalHeadCode As String
        Dim mAmount As Double
        Dim mActAmount As Double
        Dim mPerCent As Double
        Dim mDepartment As String
        Dim mCategory As String
        Dim mPaymentMode As String
        Dim mBankAcctNo As String
        Dim cntRow As Integer
        Dim xWEF As String
        Dim mSalHeadType As String

        Dim mPFAmt As Double
        Dim mPensionFund As Double
        Dim mRounding As Double
        Dim mEmpCont As Double
        Dim mPFRounding As Double
        Dim mESIAmt As Double
        Dim mESIRounding As Double
        Dim mPayablePensionWages As Double
        Dim mPayableESISalary As Double
        Dim mPayablePFSalary As Double
        Dim mSalTrnType As String

        Dim mVDate As String
        Dim mRemarks As String
        Dim mAppStatus As String
        Dim mVNo As Double
        Dim mVPFAmt As Double
        Dim mVPFRate As Double
        Dim mOPDate As String

        Dim mPrevPensionFund As Double
        Dim pPensionDiff As Double

        Dim mEmpContOn As String
        Dim mTempPFCeiling As Double
        Dim mEmployer_PF As Double
        Dim mPensionConst As Double


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()



        xWEF = UCase(VB6.Format(txtWEF.Text, "MMMYYYY"))

        mCode = Trim(txtEmpNo.Text)
        mSalTrnType = VB.Left(cboSalType.Text, 1)

        If Val(txtVNo.Text) = 0 Then
            mVNo = CDbl(GenVno)
        Else
            mVNo = Val(txtVNo.Text)
        End If

        txtVNo.Text = VB6.Format(mVNo, "00000")

        mVDate = VB6.Format(txtVDate.Text, "DD-MMM-YYYY")
        mRemarks = Trim(txtRemarks.Text)
        mAppStatus = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If MainClass.ValidateWithMasterTable((txtEmpNo.Text), "EMP_CODE", "EMP_CONT", "PAY_SALARYDEF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpContOn = IIf(IsDbNull(MasterNo), "B", MasterNo)
        End If

        mTempPFCeiling = CDbl(VB6.Format(mPFCeiling * Val(txtPaidDays.Text) / MainClass.LastDay(Month(CDate(txtWEF.Text)), Year(CDate(txtWEF.Text))), "0.00"))

        mTempPFCeiling = IIf(mTempPFCeiling < mPFCeiling, mTempPFCeiling, mPFCeiling)
        mTempPFCeiling = System.Math.Round(mTempPFCeiling, 0)


        SqlStr = ""

        SqlStr = "DELETE FROM PAY_PERKS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "' AND BOOKTYPE ='V'"
        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_SALVOUCHER_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "'" & vbCrLf & " AND ISARREAR='V'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_PFESI_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "'" & vbCrLf & " AND ISARREAR='V'"

        PubDBCn.Execute(SqlStr)



        SqlStr = ""

        SqlStr = " SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If MainClass.ValidateWithMasterTable(RsTemp.Fields("EMP_DEPT_CODE").value, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDepartment = MasterNo
            Else
                mDepartment = "-1"
            End If

            mCategory = IIf(IsDbNull(RsTemp.Fields("EMP_CATG").Value), "-1", RsTemp.Fields("EMP_CATG").Value)
            mPaymentMode = IIf(IsDbNull(RsTemp.Fields("PAYMENTMODE").Value), "-1", RsTemp.Fields("PAYMENTMODE").Value)
            mBankAcctNo = IIf(IsDbNull(RsTemp.Fields("EMP_BANK_NO").Value), "", RsTemp.Fields("EMP_BANK_NO").Value)
        End If

        SqlStr = ""

        mPayablePFSalary = Val(txtBSalary.Text)
        mPayableESISalary = Val(txtBSalary.Text)
        With sprdEarn
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColCode
                mSalHeadCode = Trim(.Text)

                .Col = ColPer
                mPerCent = Val(.Text)

                .Col = ColActAmt
                mActAmount = Val(.Text)

                .Col = ColAmt
                mAmount = Val(.Text)

                If MainClass.ValidateWithMasterTable(mSalHeadCode, "CODE", "INCLUDEDPF", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        mPayablePFSalary = mPayablePFSalary + Val(CStr(mAmount))
                    End If
                End If

                If MainClass.ValidateWithMasterTable(mSalHeadCode, "CODE", "INCLUDEDESI", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        mPayableESISalary = mPayableESISalary + Val(CStr(mAmount))
                    End If
                End If


                If mAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_SALVOUCHER_TRN (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, PERCENTAGE, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,DESG_DESC, SAL_TYPE, " & vbCrLf & " VNO, VDATE, REMARKS, APP_STATUS, PAIDDAYS, SUSPEND_PER, ADDUSER, ADDDATE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & Trim(txtEmpNo.Text) & "',TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtBSalary.Text) & ", " & Val(txtBSalary.Text) & ", " & Val(CStr(mPerCent)) & ", " & vbCrLf & " 0, " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','" & Trim(cbodesignation.Text) & "','" & mSalTrnType & "', " & vbCrLf & " '" & mVNo & "','" & mVDate & "','" & mRemarks & "', '" & mAppStatus & "'," & Val(txtPaidDays.Text) & "," & Val(txtSuspendPer.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    PubDBCn.Execute(SqlStr)

                    SqlStr = " INSERT INTO PAY_SAL_TRN (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtAtcBasic.Text) & ", " & Val(txtBSalary.Text) & ", " & vbCrLf & " " & Val(txtPaidDays.Text) & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','V','" & Trim(cbodesignation.Text) & "')"

                    PubDBCn.Execute(SqlStr)

                End If
            Next
        End With

        With sprdDeduct
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColCode
                mSalHeadCode = Trim(.Text)

                .Col = ColPer
                mPerCent = Val(.Text)

                .Col = ColAmt
                mAmount = Val(.Text)

                If MainClass.ValidateWithMasterTable(mSalHeadCode, "CODE", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSalHeadType = MasterNo
                End If

                If CDbl(mSalHeadType) = ConPF Then
                    mPFAmt = mAmount
                    If mPFAmt = 0 Then
                        mPensionFund = 0
                        mEmpCont = 0
                        mPFRounding = CDbl("0.00")
                        mRounding = CDbl("0.00")
                        mPayablePensionWages = 0
                    Else
                        mPensionFund = IIf(mPayablePFSalary < mTempPFCeiling, mPayablePFSalary, mTempPFCeiling) * mPFPensionRate / 100
                        mRounding = CDbl("0.00")
                        mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                        mEmpCont = mPFAmt - mPensionFund
                        mPFRounding = CDbl("0.00")

                        mPayablePensionWages = IIf(mTempPFCeiling <= mPayablePFSalary, mTempPFCeiling, mPayablePFSalary)
                        mPayablePensionWages = CDbl(VB6.Format(mPayablePensionWages, "0"))
                    End If
                ElseIf CDbl(mSalHeadType) = ConESI Then
                    mESIAmt = mAmount
                    mRounding = CDbl("0.00")
                    mESIAmt = CDbl(VB6.Format(mESIAmt, CStr(mRounding)))
                ElseIf CDbl(mSalHeadType) = ConVPFAllw Then
                    mVPFAmt = mAmount
                    mRounding = CDbl("0.00")
                    mVPFAmt = CDbl(VB6.Format(mVPFAmt, CStr(mRounding)))
                    mVPFRate = mPerCent
                End If

                If mAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_SALVOUCHER_TRN (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, PERCENTAGE, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,DESG_DESC,SAL_TYPE, " & vbCrLf & " VNO,VDATE,REMARKS,APP_STATUS,PAIDDAYS, SUSPEND_PER, ADDUSER, ADDDATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & Trim(txtEmpNo.Text) & "',TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtBSalary.Text) & ", " & Val(txtBSalary.Text) & ", " & Val(CStr(mPerCent)) & ", " & vbCrLf & " 0, " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','" & Trim(cbodesignation.Text) & "','" & mSalTrnType & "'," & vbCrLf & " '" & mVNo & "','" & mVDate & "','" & mRemarks & "', '" & mAppStatus & "'," & Val(txtPaidDays.Text) & "," & Val(txtSuspendPer.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                    PubDBCn.Execute(SqlStr)

                    SqlStr = " INSERT INTO PAY_SAL_TRN (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtBSalary.Text) & ", " & Val(txtBSalary.Text) & ", " & vbCrLf & " " & Val(txtPaidDays.Text) & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','V','" & Trim(cbodesignation.Text) & "')"

                    PubDBCn.Execute(SqlStr)

                End If
            Next
        End With

        With sprdPerks
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColCode
                mSalHeadCode = Trim(.Text)

                .Col = ColPer
                mPerCent = Val(.Text)

                .Col = ColAmt
                mAmount = Val(.Text)

                If mAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_SALVOUCHER_TRN (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, PERCENTAGE, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,DESG_DESC,SAL_TYPE, " & vbCrLf & " VNO, VDATE, REMARKS,APP_STATUS,PAIDDAYS, SUSPEND_PER, ADDUSER, ADDDATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & Trim(txtEmpNo.Text) & "',TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtBSalary.Text) & ", " & Val(txtBSalary.Text) & ", " & Val(CStr(mPerCent)) & ", " & vbCrLf & " 0, " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mActAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','" & Trim(cbodesignation.Text) & "','" & mSalTrnType & "'," & vbCrLf & " '" & mVNo & "','" & mVDate & "','" & mRemarks & "', '" & mAppStatus & "'," & Val(txtPaidDays.Text) & ", " & Val(txtSuspendPer.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


                    PubDBCn.Execute(SqlStr)

                    SqlStr = " INSERT INTO PAY_SAL_TRN (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtBSalary.Text) & ", " & Val(txtBSalary.Text) & ", " & vbCrLf & " " & Val(txtPaidDays.Text) & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mActAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','V','" & Trim(cbodesignation.Text) & "')"

                    PubDBCn.Execute(SqlStr)

                End If
            Next
        End With

        ''PF ESI TRN

        If mPFAmt <> 0 Then
            mPrevPensionFund = GetPensionFund(mCode, VB6.Format(txtWEF.Text, "DD-MMM-YYYY"))

            mPensionConst = System.Math.Round(mPFCeiling * 8.33 * 0.01, 0)

            If mPrevPensionFund <> 0 Then
                If mPrevPensionFund >= mPensionConst Then
                    mEmpCont = mEmpCont + mPensionFund
                    mPensionFund = 0
                Else
                    pPensionDiff = mPensionConst - mPrevPensionFund

                    If pPensionDiff < mPensionFund Then
                        mEmpCont = mEmpCont + (mPensionFund - pPensionDiff)
                        mPensionFund = pPensionDiff
                    End If
                End If
            End If
        End If

        SqlStr = " INSERT INTO PAY_PFESI_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BasicSalary, PFABLEAMT, PENSIONWAGES, PFAMT, PFRate ,  " & vbCrLf & " ESIABLEAMT , ESIAMT, ESIRATE, PENSIONFUND, EPFAMT ,  " & vbCrLf & " LEAVEWOP , WDAYS, ISARREAR, VPFAMT, VPFRATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtBSalary.Text) & ", " & Val(txtBSalary.Text) & "," & mPayablePensionWages & "," & mPFAmt & "," & mPFRate & ", " & vbCrLf & " " & mPayableESISalary & "," & mESIAmt & "," & mESIRate & ", " & vbCrLf & " " & mPensionFund & ", " & mEmpCont & ",0," & vbCrLf & " " & Val(txtPaidDays.Text) & ", " & vbCrLf & " 'V'," & mVPFAmt & ", " & mVPFRate & ") "

        PubDBCn.Execute(SqlStr)


        If VB.Left(cboSalType.Text, 1) = "S" Or VB.Left(cboSalType.Text, 1) = "B" Then
            mOPDate = GetOpeningPerksDate()
            If VB6.Format(mOPDate, "YYYYMM") <= VB6.Format(txtWEF.Text, "YYYYMM") Then
                If UpdatePerksTrn(mCode, (txtWEF.Text), Val(txtPaidDays.Text)) = False Then GoTo UpdateError
                '        If UpdatePerksArrearTrn(mCode, txtWEF.Text) = False Then GoTo UpDateSalTrnErr
            End If
        End If

        PubDBCn.CommitTrans()
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
    Private Function UpdatePerksTrn(ByRef mCode As String, ByRef mSalDate As String, ByRef mWDays As Double) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsVar As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim mSalHeadCode As Integer
        Dim mAmount As Double

        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf _
            & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf _
            & " ADD_DEDUCT.ROUNDING AS ROUNDING " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND TO_CHAR(SALARY_APP_DATE,'YYYYMM') <= '" & VB6.Format(mSalDate, "YYYYMM") & "') "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mSalHeadCode = RsTemp.Fields("ADD_DEDUCTCODE").Value
                mAmount = RsTemp.Fields("Amount").Value
                mAmount = System.Math.Round(mAmount * System.Math.Round(mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), 0), 0)

                If mAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_PERKS_TRN ( " & vbCrLf & " COMPANY_CODE, SAL_DATE, " & vbCrLf & " EMP_CODE, ADD_DEDUCTCODE, AMOUNT,BOOKTYPE,DC,PAYMENT_TYPE,ADDUSER,ADDDATE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCode & "', " & mSalHeadCode & ", " & mAmount & ",'V'," & vbCrLf & " 'C', '','" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                    PubDBCn.Execute(SqlStr)
                End If
                RsTemp.MoveNext()
            Loop
        End If
NextRec:
        UpdatePerksTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdatePerksTrn = False
    End Function

    Private Function GenVno() As String

        On Error GoTo ERR1
        Dim SqlStr As String = ""


        SqlStr = " SELECT MAX(VNO) " & vbCrLf & " FROM PAY_SALVOUCHER_TRN SALVOUCHER " & vbCrLf & " WHERE SALVOUCHER.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALVOUCHER.SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALVOUCHER.SAL_DATE<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        GenVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")

        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume
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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAcPosting As String

        FieldsVarification = True
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

        If Not IsNumeric(txtBSalary.Text) Then
            MsgInformation("Invaild Basic Salary.")
            txtBSalary.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtGSalary.Text) + Val(txtDeduction.Text) + Val(txtPerks.Text) = 0 Then
            MsgInformation("Nothing to Save.")
            txtBSalary.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboSalType.Text) = "" Then
            MsgInformation("Select Salary Type.")
            cboSalType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        CalcEarn()
        CalcPFESI()
        CalcPerks()
        CalcGrossSalary(("Y"))

        If MODIFYMode = True Then
            SqlStr = " SELECT APP_STATUS FROM PAY_SALVOUCHER_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(txtEmpNo.Text, "000000") & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(txtWEF.Text, "YYYYMM") & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
            If RsTemp.EOF = False Then
                mAcPosting = IIf(IsDbNull(RsTemp.Fields("APP_STATUS").Value), "N", RsTemp.Fields("APP_STATUS").Value)

                If mAcPosting = "Y" Then
                    MsgInformation("Account Posting Done, so Cann't be modify.")
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
        'Resume
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1
        TxtName.Maxlength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)

        txtEmpNo.Maxlength = RsEmp.Fields("EMP_CODE").DefinedSize
        txtBSalary.Maxlength = RsEmp.Fields("BASICSALARY").Precision

        txtVNo.Maxlength = RsEmp.Fields("VNO").DefinedSize
        txtVDate.Maxlength = 10
        txtRemarks.Maxlength = RsEmp.Fields("REMARKS").DefinedSize
        txtPaidDays.Maxlength = RsEmp.Fields("PAIDDAYS").Precision
        txtSuspendPer.Maxlength = RsEmp.Fields("SUSPEND_PER").Precision

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        MainClass.ClearGrid(SprdView)
        SqlStr = " SELECT DISTINCT EMP.EMP_CODE,EMP.EMP_NAME AS NAME, SALVOUCHER.BASICSALARY , " & vbCrLf & " TO_CHAR(SALVOUCHER.SAL_DATE,'DD/MM/YYYY') AS SAL_DATE, VNO, VDATE, DECODE(APP_STATUS,'Y','YES','NO') AS STATUS,DECODE(SAL_TYPE,'S','SALARY',DECODE(SAL_TYPE,'O','OTHERS',DECODE(SAL_TYPE,'A','SUSPEND','ARREAR'))) AS SALARY_TYPE" & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP, PAY_SALVoucher_TRN SALVOUCHER " & vbCrLf & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP.COMPANY_CODE=SALVOUCHER.COMPANY_CODE  " & vbCrLf & " AND EMP.EMP_CODE=SALVOUCHER.EMP_CODE  "

        SqlStr = SqlStr & vbCrLf & " AND SALVOUCHER.SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALVOUCHER.SAL_DATE<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY TO_CHAR(SALVOUCHER.SAL_DATE,'DD/MM/YYYY'),EMP.EMP_NAME"


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 576 * 0)
            .set_ColWidth(1, 576 * 2)
            .set_ColWidth(2, 576 * 4)
            .set_ColWidth(3, 576 * 2)
            .set_ColWidth(4, 576 * 2)
            .set_ColWidth(5, 576 * 2)
            .set_ColWidth(6, 576 * 2)
            .set_ColWidth(7, 576 * 2)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Dim xWEF As String

        SqlStr = ""

        '     If MainClass.ValidateWithMasterTable(txtEmpNo.Text, "EMP_CODE", "EMP_CODE", "PAY_SAL_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        MsgBox "Salary Exists Against This Employee."
        '        Delete1 = False
        '        Exit Function
        '    End If

        xWEF = VB6.Format(txtWEF.Text, "MMMYYYY")

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_PERKS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "' AND BOOKTYPE ='V'"
        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtEmpNo.Text & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = "Delete from PAY_SalVoucher_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpNo.Text) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "'" & vbCrLf & " AND ISARREAR='V'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_PFESI_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "'" & vbCrLf & " AND ISARREAR='V'"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsEmp.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        '    Resume
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
        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = ""

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

        cboSalType.Items.Clear()
        cboSalType.Items.Add("Salary")
        cboSalType.Items.Add("Others")
        cboSalType.Items.Add("A. Suspend Allowance")
        cboSalType.Items.Add("B. Arrear")
        cboSalType.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillSalarySprd()

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        Dim mSalDate As String

        MainClass.ClearGrid(sprdEarn, -1)
        MainClass.ClearGrid(sprdDeduct, -1)
        MainClass.ClearGrid(sprdPerks, -1)

        If Trim(txtWEF.Text) = "" Then
            mSalDate = VB6.Format(RunDate, "DD/MM/YYYY")
        Else
            mSalDate = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        End If

        '
        '    SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        ''            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & " OR CALC_ON=" & ConCalcVariable & ") " & vbCrLf _
        ''            & " AND TYPE <> " & ConOT & " "


        mSqlStr = " SELECT " & vbCrLf & " COMPANY_CODE, CODE , " & vbCrLf & " NAME ,ADDDEDUCT,CALC_ON, " & vbCrLf & " TYPE ,PERCENTAGE, SEQ, " & vbCrLf & " ROUNDING ,INCLUDEDPF, INCLUDEDESI, " & vbCrLf & " INCLUDEDLEAVEENCASH,ACCOUNTCODEPOST, " & vbCrLf & " DC ,ISSALPART,STATUS , " & vbCrLf & " CLOSED_DATE , DEFAULT_AMT " & vbCrLf & " FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & "," & ConCalcVariable & ")" & vbCrLf & " AND TYPE <> " & ConOT & " "

        SqlStr = mSqlStr & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & mSqlStr & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

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
                            End If
                        ElseIf .Fields("ADDDEDUCT").Value = ConDeduct Then
                            sprdDeduct.Col = 1
                            sprdDeduct.Row = sprdDeduct.MaxRows
                            If Trim(sprdDeduct.Text) <> "" Then
                                sprdDeduct.MaxRows = sprdDeduct.MaxRows + 1
                            End If
                        ElseIf .Fields("ADDDEDUCT").Value = ConPerks Then
                            sprdPerks.Col = 1
                            sprdPerks.Row = sprdPerks.MaxRows
                            If Trim(sprdPerks.Text) <> "" Then
                                sprdPerks.MaxRows = sprdPerks.MaxRows + 1
                            End If
                        End If
                    End If
                Loop
            End With
        End If

        Call FormatSprd(-1)

        '    MainClass.ProtectCell sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc
        '    MainClass.ProtectCell sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc
        '    MainClass.ProtectCell sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdEarn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight) ''* 1.25

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 13)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 4)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 7)

            .Col = ColActAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColActAmt, 7)
            .ColHidden = True

        End With

        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
        MainClass.SetSpreadColor(sprdEarn, mRow)

        With sprdDeduct

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight) ''* 1.25

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 13)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 4)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 7)

            .Col = ColActAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColActAmt, 7)
            .ColHidden = True

        End With

        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
        MainClass.SetSpreadColor(sprdDeduct, mRow)

        With sprdPerks

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight) ''* 1.25

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 13)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 4)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 7)

            .Col = ColActAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColActAmt, 7)
            .ColHidden = True

        End With

        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
        MainClass.SetSpreadColor(sprdPerks, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub CalcGrossSalary(ByRef mIsReset As String)

        Dim mSalary As Double
        Dim mEarn As Double
        Dim mDeduct As Double
        Dim mPerks As Double
        Dim cntRow As Integer

        If mIsReset = "N" Then Exit Sub

        mSalary = Val(txtBSalary.Text)

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mEarn = mEarn + Val(.Text)
            Next
        End With

        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mDeduct = mDeduct + Val(.Text)
            Next
        End With

        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mPerks = mPerks + Val(.Text)
            Next
        End With

        txtGSalary.Text = MainClass.FormatRupees(Val(CStr(mSalary)) + Val(CStr(mEarn)))
        txtDeduction.Text = MainClass.FormatRupees(Val(CStr(mDeduct)))
        txtPerks.Text = MainClass.FormatRupees(Val(CStr(mPerks)))
        txtNetSalary.Text = MainClass.FormatRupees(Val(CStr(mSalary)) + Val(CStr(mEarn)) + Val(CStr(mPerks)) - Val(CStr(mDeduct)))
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
                End If
            Next
        End With
    End Sub

    Private Function CalcBasicPFSalary(ByRef mType As Integer) As Double
        Dim cntRow As Integer
        Dim mCode As Integer

        CalcBasicPFSalary = IIf(IsNumeric(txtBSalary.Text), txtBSalary.Text, 0)
        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mCode = CInt(.Text)
                If mType = ConPF Then
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
    End Function

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
        Else
            mPFCeiling = 6500
            mPFRate = 12
            mPFEPFRate = 3.67
            mPFPensionRate = 8.33
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Public Sub CalcPFESI()
        On Error GoTo ERR1
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        Dim mRounding As String
        Dim mRound As String
        Dim mAmount As Double

        For mcntRow = 1 To sprdDeduct.MaxRows
            sprdDeduct.Row = mcntRow

            sprdDeduct.Col = ColCode
            If sprdDeduct.Text = "" Then Exit Sub
            mCode = CInt(sprdDeduct.Text)
            If MainClass.ValidateWithMasterTable(mCode, "Code", "Type", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mType = MasterNo
            End If
            sprdDeduct.Col = ColPer
            xPer = IIf(IsNumeric(sprdDeduct.Text), sprdDeduct.Text, 0)

            sprdDeduct.Col = ColAmt
            If xPer <> 0 Then
                sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
            End If

            If mType = ConESI Then
                mRounding = CStr(10)
            Else
                If MainClass.ValidateWithMasterTable(mCode, "Code", "ROUNDING", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mRounding = MasterNo
                End If
            End If

            If mRounding = "0.05" Then
                sprdDeduct.Text = CStr(PaiseRound(Val(sprdDeduct.Text), 0.05))
            ElseIf mRounding = "10" Then
                mAmount = Val(sprdDeduct.Text)
                mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                sprdDeduct.Text = CStr(mAmount)
            Else
                mRound = Replace(mRounding, "1", "0")
                sprdDeduct.Text = VB6.Format(Val(sprdDeduct.Text), mRound)
            End If

        Next
        Exit Sub
ERR1:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
            mESICeiling = 6500
            mESIRate = 1.75
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Public Sub CalcEarn()
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        Dim xActAmount As Double

        For mcntRow = 1 To sprdEarn.MaxRows
            sprdEarn.Row = mcntRow

            sprdEarn.Col = ColPer
            xPer = IIf(IsNumeric(sprdEarn.Text), sprdEarn.Text, 0)

            If xPer <> 0 Then
                sprdEarn.Col = ColAmt
                sprdEarn.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
            Else
                sprdEarn.Col = ColActAmt
                xActAmount = IIf(IsNumeric(sprdEarn.Text), sprdEarn.Text, 0)

                sprdEarn.Col = ColAmt
                If VB.Left(cboSalType.Text, 1) = "S" Then
                    If Val(txtAtcBasic.Text) > 0 Then
                        If ADDMode = True Then
                            sprdEarn.Text = CStr(xActAmount * Val(txtBSalary.Text) / Val(txtAtcBasic.Text))
                        End If
                    End If
                End If
            End If
        Next
    End Sub
    Public Sub CalcPerks()
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        Dim xActAmount As Double

        '    If cboSalType.ListIndex = 2 Then
        '        For mCntRow = 1 To sprdPerks.MaxRows
        '            sprdPerks.Row = mCntRow
        '
        '            sprdPerks.Col = ColPer
        '            sprdPerks.Text = 0
        '
        '            sprdPerks.Col = ColAmt
        '            sprdPerks.Text = 0
        '
        '        Next
        '    Else
        For mcntRow = 1 To sprdPerks.MaxRows
            sprdPerks.Row = mcntRow

            sprdPerks.Col = ColPer
            xPer = IIf(IsNumeric(sprdPerks.Text), sprdPerks.Text, 0)

            sprdPerks.Col = ColAmt
            If xPer <> 0 Then
                sprdPerks.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
            Else
                sprdPerks.Col = ColActAmt
                xActAmount = IIf(IsNumeric(sprdPerks.Text), sprdPerks.Text, 0)

                '                 sprdPerks.Col = ColAmt
                '                 If Val(txtAtcBasic.Text) > 0 Then
                '                    sprdPerks.Text = CStr(xActAmount * Val(txtBSalary.Text) / Val(txtAtcBasic))
                '                End If
            End If
        Next
        '    End If
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


    Private Sub txtPaidDays_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaidDays.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mLastDay As Integer

        If Val(txtPaidDays.Text) = 0 Then GoTo EventExitSub
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub
        If cboSalType.SelectedIndex = 1 Then GoTo EventExitSub

        mLastDay = MainClass.LastDay(Month(CDate(txtWEF.Text)), Year(CDate(txtWEF.Text)))

        If cboSalType.SelectedIndex = 2 Then
            txtBSalary.Text = VB6.Format(Val(txtAtcBasic.Text) * Val(txtPaidDays.Text) * Val(txtSuspendPer.Text) * 0.01 / mLastDay, "0.00")
            txtBSalary.Text = CStr(System.Math.Round(CDbl(txtBSalary.Text), 0))
        ElseIf cboSalType.SelectedIndex = 3 Then
            '        txtBSalary.Text = Format(Val(txtAtcBasic) * Val(txtPaidDays) / mLastDay, "0.00")
            '        txtBSalary.Text = Round(txtBSalary.Text, 0)
        Else
            txtBSalary.Text = VB6.Format(Val(txtAtcBasic.Text) * Val(txtPaidDays.Text) / mLastDay, "0.00")
            txtBSalary.Text = CStr(System.Math.Round(CDbl(txtBSalary.Text), 0))
        End If

        CalcEarn()
        CalcPFESI()
        CalcPerks()
        '    CalcOthers
        Call CalcGrossSalary("Y") ''IIf(ADDMode = True, "Y", "N"))

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSuspendPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuspendPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuspendPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSuspendPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSuspendPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuspendPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mLastDay As Integer

        If Val(txtSuspendPer.Text) > 100 Or Val(txtSuspendPer.Text) < 0 Then
            MsgInformation("Percent cann't be Greater than 100 or Less than 0")
            Cancel = True
            GoTo EventExitSub
        End If

        If Val(txtPaidDays.Text) = 0 Then GoTo EventExitSub
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub
        If cboSalType.SelectedIndex = 1 Then GoTo EventExitSub

        mLastDay = MainClass.LastDay(Month(CDate(txtWEF.Text)), Year(CDate(txtWEF.Text)))

        If cboSalType.SelectedIndex = 2 Then
            txtBSalary.Text = VB6.Format(Val(txtAtcBasic.Text) * Val(txtPaidDays.Text) * Val(txtSuspendPer.Text) * 0.01 / mLastDay, "0.00")
            txtBSalary.Text = CStr(System.Math.Round(CDbl(txtBSalary.Text), 0))
        ElseIf cboSalType.SelectedIndex = 3 Then
            '        txtBSalary.Text = Format(Val(txtAtcBasic) * Val(txtPaidDays) / mLastDay, "0.00")
            '        txtBSalary.Text = Round(txtBSalary.Text, 0)
        Else
            txtBSalary.Text = VB6.Format(Val(txtAtcBasic.Text) * Val(txtPaidDays.Text) / mLastDay, "0.00")
            txtBSalary.Text = CStr(System.Math.Round(CDbl(txtBSalary.Text), 0))
        End If
        CalcEarn()
        CalcPFESI()
        CalcPerks()
        '    CalcOthers
        Call CalcGrossSalary("Y") ''IIf(ADDMode = True, "Y", "N"))

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVDate.Text) = "" Then GoTo EventExitSub


        If Not IsDate(txtVDate.Text) Then
            MsgInformation("Invalid Voucher Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtVNo.Text = VB6.Format(txtVNo.Text, "00000")
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

        '    txtWEF.Text = "01/" & vb6.Format(txtWEF.Text, "MM/YYYY")

        txtWEF.Text = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        xWEF = VB6.Format(txtWEF.Text, "MMMYYYY")

        If MODIFYMode = True And RsEmp.EOF = False Then xCode = RsEmp.Fields("EMP_CODE").Value

        SqlStr = " SELECT * FROM PAY_SALVoucher_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & txtEmpNo.Text & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsEmp.EOF = False Then
            Clear1()
            Call Show1()
            'If txtWEF.Enabled = True Then txtWEF.Focus()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Month, Use add Button to Generate Voucher.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_SALVoucher_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "' AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xWEF) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)
                GoTo EventExitSub
            End If
            Call ShowAtcSalary((txtEmpNo.Text), (txtWEF.Text))
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub ShowAtcSalary(ByRef xCode As String, ByRef xWEF As String)

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        SqlStr = " SELECT * FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND TO_CHAR(SALARY_EFF_DATE,'YYYYMM')<= '" & VB6.Format(xWEF, "YYYYMM") & "') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = True Then Exit Sub
        txtAtcBasic.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")

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
                    .Col = ColActAmt
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))
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
                    .Col = ColActAmt
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))
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
                    .Col = ColActAmt
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))
                End If
            Next
        End With
    End Sub

    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim ColStartRow As Integer
        Dim ColEndRow As Integer
        Dim cntRow As Integer
        Dim mBankName As String
        Dim mRptFileName As String
        Dim cntCol As Integer
        Dim mCheckCol As Integer

        PubDBCn.Errors.Clear()
        Call MainClass.ClearCRptFormulas(Report1)

        frmPrintSalVoucher.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        If frmPrintSalVoucher.OptSalSlip.Checked = True Then

            If FillPaySlipIntoPrintDummy() = False Then GoTo ERR1
            SqlStr = ""
            SqlStr = FetchRecordForPaySlip(SqlStr)
            mRptFileName = "PaySlip.Rpt"
            If cboSalType.SelectedIndex = 2 Then
                mTitle = "Suspension Allowance : For the Month : " & MonthName(Month(CDate(txtWEF.Text))) & ", " & Year(CDate(txtWEF.Text))
                mSubTitle = "Suspension Allowance (@ " & Val(txtSuspendPer.Text) & " % of rate of Pay)"
            ElseIf cboSalType.SelectedIndex = 3 Then
                mTitle = "Arrear For the Month : " & MonthName(Month(CDate(txtWEF.Text))) & ", " & Year(CDate(txtWEF.Text))
                mSubTitle = ""
            Else
                mTitle = "For the Month : " & MonthName(Month(CDate(txtWEF.Text))) & ", " & Year(CDate(txtWEF.Text))
                mSubTitle = ""
            End If
        Else
            If FillPayPerksSlipIntoPrintDummy() = False Then GoTo ERR1
            SqlStr = ""
            SqlStr = FetchRecordForPaySlip(SqlStr)
            mRptFileName = "PayPerks.Rpt"
            mTitle = IIf(cboSalType.SelectedIndex = 3, "Arrear ", "") & " Perks For the Month : " & MonthName(Month(CDate(txtWEF.Text))) & ", " & Year(CDate(txtWEF.Text))
            mSubTitle = ""
        End If
        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
        frmPrintSalVoucher.Close()
        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        frmPrintSalVoucher.Close()
    End Sub

    Private Function FillPaySlipIntoPrintDummy() As Boolean

        On Error GoTo PrintDummyErr


        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim ColTotPayable As Long
        'Dim ColTotDeduction As Long
        Dim ColNum As Integer

        Dim Colcnt As Integer
        Dim MaxColcnt As Integer
        Dim arrsal() As String

        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mEmpFName As String
        Dim mDOJ As String
        Dim mDepartment As String
        Dim mDesignation As String
        Dim mPFNo As String
        Dim mBankAcct As String
        Dim mActualDays As Integer
        Dim mWDays As Double
        Dim mPaymentType As String

        Dim mBSalary As Double
        Dim mPSalary As Double
        Dim mLeaves As String
        Dim mRemarks As String

        Dim mGrossDeduct As Double
        Dim mGrossPay As Double
        Dim mNetPay As Double
        Dim mGrossEarn As Double
        Dim mNetPayInWord As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PAYSLIP_TRN WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        PubDBCn.Execute(SqlStr)


        mActualDays = 0


        '    ReDim arrsal(sprdEarn.MaxCols)

        MaxColcnt = sprdEarn.MaxRows + sprdDeduct.MaxRows
        ReDim mEmpEarnData(MaxColcnt)
        ReDim mEmpDeductData(MaxColcnt)

        mEmpCode = MainClass.AllowSingleQuote(txtEmpNo.Text)
        mLeaves = CStr(0)
        mWDays = Val(txtPaidDays.Text)
        mPaymentType = MainClass.AllowSingleQuote(cboSalType.Text)
        mBSalary = CDbl(IIf(IsNumeric(txtAtcBasic.Text), txtAtcBasic.Text, 0))
        mPSalary = CDbl(IIf(IsNumeric(txtBSalary.Text), txtBSalary.Text, 0))

        SqlStr = "  SELECT EMP_NAME, EMP_FNAME, EMP_DEPT_CODE, " & vbCrLf & " EMP_DESG_CODE, EMP_DOJ, EMP_PF_ACNO, EMP_BANK_NO" & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mEmpName = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
            mEmpFName = IIf(IsDbNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value)
            mDepartment = IIf(IsDbNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDepartment, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDepartment = MasterNo
            End If

            mDesignation = IIf(IsDbNull(RsTemp.Fields("EMP_DESG_CODE").Value), "", RsTemp.Fields("EMP_DESG_CODE").Value)
            If MainClass.ValidateWithMasterTable(mDesignation, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDesignation = MasterNo
            End If

            mBankAcct = IIf(IsDbNull(RsTemp.Fields("EMP_BANK_NO").Value), "", RsTemp.Fields("EMP_BANK_NO").Value)
            mDOJ = IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value)
            mPFNo = IIf(IsDbNull(RsTemp.Fields("EMP_PF_ACNO").Value), "", RsTemp.Fields("EMP_PF_ACNO").Value)
        End If

        With sprdEarn
            For RowNum = 1 To .MaxRows
                .Row = RowNum
                .Col = ColDesc
                mEmpEarnData(RowNum).mTitle = .Text

                .Col = ColActAmt
                mEmpEarnData(RowNum).mRate = CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColAmt
                mEmpEarnData(RowNum).mPayable = CDbl(IIf(IsNumeric(.Text), .Text, 0))

                mGrossEarn = MainClass.FormatRupees(CDbl(IIf(IsNumeric(txtGSalary.Text), txtGSalary.Text, 0)))
            Next
        End With

        With sprdDeduct
            For RowNum = 1 To .MaxRows
                .Row = RowNum
                .Col = ColDesc
                mEmpDeductData(RowNum).mTitle = .Text

                .Col = ColActAmt
                mEmpDeductData(RowNum).mRate = CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColAmt
                mEmpDeductData(RowNum).mPayable = CDbl(IIf(IsNumeric(.Text), .Text, 0))

                mGrossDeduct = MainClass.FormatRupees(CDbl(IIf(IsNumeric(txtDeduction.Text), txtDeduction.Text, 0)))
            Next
        End With


        mGrossPay = MainClass.FormatRupees(CDbl(mGrossEarn) - CDbl(mGrossDeduct))
        mNetPay = MainClass.FormatRupees(System.Math.Round(CDbl(mGrossPay), 0))
        mNetPayInWord = MainClass.RupeesConversion(CDbl(mNetPay))

        For Colcnt = 1 To MaxColcnt
            SqlStr = " INSERT INTO TEMP_PAYSLIP_TRN ( " & vbCrLf & " USERID, COMPANY_CODE, SUBROW , " & vbCrLf & " EMP_CODE, EMP_NAME, EMP_FNAME, " & vbCrLf & " EMP_DEPT_DESC, EMP_DESG_DESC, EMP_DOJ, " & vbCrLf & " EMP_PF_ACNO, EMP_BANK_NO, ACTUAL_DAYS," & vbCrLf & " PAYABLE_DAYS, BASIC_SALARY, PAYABLE_BASIC_SALARY,"

            SqlStr = SqlStr & vbCrLf & " EARN_TITLE,EARN_RATE,EARN_PAYABLE," & vbCrLf & " DEDUCT_TITLE, DEDUCT_RATE, DEDUCT_PAYABLE," & vbCrLf & " LEAVES, REMARKS, " & vbCrLf & " GROSS_SALARY, GROSS_PAYABLE, " & vbCrLf & " GROSS_DEDUCT, NET_SALARY " & vbCrLf & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Colcnt & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "', '" & mEmpFName & "', " & vbCrLf & " '" & mDepartment & "', '" & mDesignation & "','" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "', " & vbCrLf & " '" & mPFNo & "','" & mBankAcct & "', " & Val(CStr(mActualDays)) & "," & vbCrLf & " " & Val(CStr(mWDays)) & ", " & mBSalary & ", " & mPSalary & ", "


            SqlStr = SqlStr & vbCrLf & " '" & mEmpEarnData(Colcnt).mTitle & "'," & mEmpEarnData(Colcnt).mRate & "," & mEmpEarnData(Colcnt).mPayable & "," & vbCrLf & " '" & mEmpDeductData(Colcnt).mTitle & "'," & mEmpDeductData(Colcnt).mRate & "," & mEmpDeductData(Colcnt).mPayable & "," & vbCrLf & " '" & mLeaves & "','" & mNetPayInWord & "', " & vbCrLf & " 0," & mGrossEarn & ", " & vbCrLf & " " & mGrossDeduct & ", " & mNetPay & " )"


            PubDBCn.Execute(SqlStr)
        Next

        PubDBCn.CommitTrans()
        FillPaySlipIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        'Resume
        FillPaySlipIntoPrintDummy = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function FillPayPerksSlipIntoPrintDummy() As Boolean

        On Error GoTo PrintDummyErr


        Dim RowNum As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim ColTotPayable As Long
        'Dim ColTotDeduction As Long
        Dim ColNum As Integer

        Dim Colcnt As Integer
        Dim MaxColcnt As Integer
        Dim arrsal() As String

        Dim mEmpCode As String
        Dim mEmpName As String
        Dim mEmpFName As String
        Dim mDOJ As String
        Dim mDepartment As String
        Dim mDesignation As String
        Dim mPFNo As String
        Dim mBankAcct As String
        Dim mActualDays As Integer
        Dim mWDays As Double
        Dim mPaymentType As String

        Dim mBSalary As Double
        Dim mPSalary As Double
        Dim mLeaves As String
        Dim mRemarks As String

        Dim mGrossDeduct As Double
        Dim mGrossPay As Double
        Dim mNetPay As Double
        Dim mGrossEarn As Double
        Dim mNetPayInWord As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PAYSLIP_TRN WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        PubDBCn.Execute(SqlStr)


        mActualDays = 0


        '    ReDim arrsal(sprdEarn.MaxCols)

        MaxColcnt = sprdPerks.MaxRows ''sprdEarn.MaxRows + sprdDeduct.MaxRows
        ReDim mEmpEarnData(MaxColcnt)
        '    ReDim mEmpDeductData(MaxColcnt)

        mEmpCode = MainClass.AllowSingleQuote(txtEmpNo.Text)
        mLeaves = CStr(0)
        mWDays = Val(txtPaidDays.Text)
        mPaymentType = MainClass.AllowSingleQuote(cboSalType.Text)
        mBSalary = CDbl(IIf(IsNumeric(txtAtcBasic.Text), txtAtcBasic.Text, 0))
        mPSalary = CDbl(IIf(IsNumeric(txtBSalary.Text), txtBSalary.Text, 0))

        SqlStr = "  SELECT EMP_NAME, EMP_FNAME, EMP_DEPT_CODE, " & vbCrLf & " EMP_DESG_CODE, EMP_DOJ, EMP_PF_ACNO, EMP_BANK_NO" & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mEmpName = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
            mEmpFName = IIf(IsDbNull(RsTemp.Fields("EMP_FNAME").Value), "", RsTemp.Fields("EMP_FNAME").Value)
            mDepartment = IIf(IsDbNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDepartment, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDepartment = MasterNo
            End If

            mDesignation = IIf(IsDbNull(RsTemp.Fields("EMP_DESG_CODE").Value), "", RsTemp.Fields("EMP_DESG_CODE").Value)
            If MainClass.ValidateWithMasterTable(mDesignation, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDesignation = MasterNo
            End If

            mBankAcct = IIf(IsDbNull(RsTemp.Fields("EMP_BANK_NO").Value), "", RsTemp.Fields("EMP_BANK_NO").Value)
            mDOJ = IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value)
            mPFNo = IIf(IsDbNull(RsTemp.Fields("EMP_PF_ACNO").Value), "", RsTemp.Fields("EMP_PF_ACNO").Value)
        End If

        With sprdPerks
            For RowNum = 1 To .MaxRows
                .Row = RowNum
                .Col = ColDesc
                mEmpEarnData(RowNum).mTitle = .Text

                .Col = ColActAmt
                mEmpEarnData(RowNum).mRate = CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColAmt
                mEmpEarnData(RowNum).mPayable = CDbl(IIf(IsNumeric(.Text), .Text, 0))

                mGrossEarn = mGrossEarn + CDbl(IIf(IsNumeric(.Text), .Text, 0))
            Next
        End With


        mGrossPay = MainClass.FormatRupees(CDbl(mGrossEarn))
        mNetPay = MainClass.FormatRupees(System.Math.Round(CDbl(mGrossPay), 0))
        mNetPayInWord = MainClass.RupeesConversion(CDbl(mNetPay))

        For Colcnt = 1 To MaxColcnt
            SqlStr = " INSERT INTO TEMP_PAYSLIP_TRN ( " & vbCrLf & " USERID, COMPANY_CODE, SUBROW , " & vbCrLf & " EMP_CODE, EMP_NAME, EMP_FNAME, " & vbCrLf & " EMP_DEPT_DESC, EMP_DESG_DESC, EMP_DOJ, " & vbCrLf & " EMP_PF_ACNO, EMP_BANK_NO, ACTUAL_DAYS," & vbCrLf & " PAYABLE_DAYS, BASIC_SALARY, PAYABLE_BASIC_SALARY,"

            SqlStr = SqlStr & vbCrLf & " EARN_TITLE,EARN_RATE,EARN_PAYABLE," & vbCrLf & " DEDUCT_TITLE, DEDUCT_RATE, DEDUCT_PAYABLE," & vbCrLf & " LEAVES, REMARKS, " & vbCrLf & " GROSS_SALARY, GROSS_PAYABLE, " & vbCrLf & " GROSS_DEDUCT, NET_SALARY " & vbCrLf & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Colcnt & ", " & vbCrLf & " '" & mEmpCode & "','" & mEmpName & "', '" & mEmpFName & "', " & vbCrLf & " '" & mDepartment & "', '" & mDesignation & "','" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "', " & vbCrLf & " '" & mPFNo & "','" & mBankAcct & "', " & Val(CStr(mActualDays)) & "," & vbCrLf & " " & Val(CStr(mWDays)) & ", " & mBSalary & ", " & mPSalary & ", "


            SqlStr = SqlStr & vbCrLf & " '" & mEmpEarnData(Colcnt).mTitle & "'," & mEmpEarnData(Colcnt).mRate & "," & mEmpEarnData(Colcnt).mPayable & "," & vbCrLf & " '',0,0," & vbCrLf & " '" & mLeaves & "','" & mNetPayInWord & "', " & vbCrLf & " 0," & mGrossEarn & ", " & vbCrLf & " " & mGrossDeduct & ", " & mNetPay & " )"


            PubDBCn.Execute(SqlStr)
        Next

        PubDBCn.CommitTrans()
        FillPayPerksSlipIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        'Resume
        FillPayPerksSlipIntoPrintDummy = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Private Function FetchRecordForPaySlip(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM TEMP_PAYSLIP_TRN " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"


        mSqlStr = mSqlStr & " ORDER BY EMP_NAME, EMP_CODE, SUBROW "
        FetchRecordForPaySlip = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mRemarks As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        If Trim(txtRemarks.Text) = "" Then
            mRemarks = ""
        Else
            mRemarks = "Remarks : " & Trim(txtRemarks.Text)
        End If

        MainClass.AssignCRptFormulas(Report1, "Remarks='" & mRemarks & "'")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
