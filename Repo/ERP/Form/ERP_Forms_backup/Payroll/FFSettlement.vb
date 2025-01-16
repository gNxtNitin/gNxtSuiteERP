Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmFFSettlement
    Inherits System.Windows.Forms.Form
    Dim RsFFMain As ADODB.Recordset
    Dim RsFFDetail As ADODB.Recordset

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
    Dim mEmplerPFCont As String
    Private Const ColCode As Short = 1
    Private Const ColDesc As Short = 2
    Private Const ColPer As Short = 3
    Private Const ColActualAmt As Short = 4
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
        MainClass.ButtonStatus(Me, XRIGHT, RsFFMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

    End Sub
    Private Sub Clear1()

        txtEmpNo.Text = ""
        TxtName.Text = ""
        txtFName.Text = ""
        txtDOJ.Text = ""
        txtDOL.Text = ""
        txtLTCFrom.Text = ""

        txtAtcBasic.Text = ""
        txtBSalary.Text = ""
        txtPaidDays.Text = ""
        txtIncHoursForMon.Text = ""
        txtIncAmtForMon.Text = ""
        txtIncHoursPreMon.Text = ""
        txtIncAmtPreMon.Text = ""
        txtSalArrear.Text = ""
        txtIncArrear.Text = ""
        txtLTCMonth.Text = ""
        txtLTCAmt.Text = ""
        txtBonusForYear.Text = ""
        txtBonusCurrYear.Text = ""
        txtGratuityMon.Text = ""
        txtGratuityAmt.Text = ""
        txtNoticeMon.Text = ""
        txtNoticeamt.Text = ""
        txtOthers.Text = ""
        txtGSalary.Text = ""
        txtDeduction.Text = ""
        txtTotOthers.Text = ""
        txtNetSalary.Text = ""
        txtBonusPerForYear.Text = ""
        chkMannualPerBonus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkMannualPerBonus.Enabled = False

        If RsCompany.Fields("COMPANY_CODE").Value = 16 And RsCompany.Fields("FYEAR").Value = 2014 Then
            chkMannualPerBonus.Enabled = True
            chkMannualPerBonus.Visible = True
            lblMannual.Visible = True
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            chkBonusPaid.CheckState = System.Windows.Forms.CheckState.Unchecked
            chkBonusPaid.Enabled = True
            chkBonusPaid.Visible = True
        Else
            chkBonusPaid.CheckState = System.Windows.Forms.CheckState.Checked
            chkBonusPaid.Enabled = False
            chkBonusPaid.Visible = False
        End If


        txtBonusPerCurrYear.Text = ""
        txtReason.Text = "RESIGN"
        txtChqNo.Text = ""
        txtBankName.Text = ""
        txtRemarks.Text = ""

        txtELDays.Text = ""
        txtELAmount.Text = ""

        txtExGratiaMonth.Text = ""
        txtExGratiaAmount.Text = ""
        txtCompMonth.Text = ""
        txtCompAmount.Text = ""
        chkSuspension.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSuspension.Text = ""
        chkTransfer.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkAccountPosting.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCPL.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCalcPFonEL.Enabled = True
        chkAccountPosting.Enabled = True
        lblActGross.Text = "0.00"
        lblBasicEL.Text = "0.00"

        SSTab1.SelectedIndex = 0
        cmdAccountPosting.Enabled = False
        cmdAccountPosting.Visible = True
        cbodesignation.SelectedIndex = -1

        txtDOL.Enabled = True
        txtPaidDays.Enabled = True

        FillSalarySprd()

        MainClass.ButtonStatus(Me, XRIGHT, RsFFMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub cbodesignation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cbodesignation_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAccountPosting_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAccountPosting.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkBonusPaid_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBonusPaid.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCalcPFonEL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCalcPFonEL.CheckStateChanged

        Dim mPaidDays As Double

        If FormActive = True Then Exit Sub

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Val(txtELAmount.Text) > 0 Then
                mPaidDays = IIf(IsDbNull(RsCompany.Fields("LEAVEPAIDDAYS").Value), 0, RsCompany.Fields("LEAVEPAIDDAYS").Value)
                If RsCompany.Fields("COMPANY_CODE").Value = 5 And CDate(txtDOL.Text) < CDate("01/01/2010") Then
                    lblBasicEL.Text = CStr(PaiseRound(CDbl(VB6.Format(txtELDays.Text, "0.00")) * Val(txtAtcBasic.Text) / mPaidDays, CDbl("0.50")))
                Else
                    lblBasicEL.Text = CStr(Val(txtELAmount.Text))
                End If
            End If
        Else
            lblBasicEL.Text = CStr(0)
        End If

        CalcEarn()
        CalcPFESI()
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))

    End Sub


    Private Sub chkMannualPerBonus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMannualPerBonus.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkMannualPerBonus.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtBonusForYear.Enabled = True
        Else
            txtBonusForYear.Enabled = False
        End If
    End Sub

    Private Sub chkSuspension_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSuspension.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call txtPaidDays_Validating(txtPaidDays, New System.ComponentModel.CancelEventArgs(True))
    End Sub

    Private Sub chkSuspension_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles chkSuspension.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub chkTransfer_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTransfer.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkTransfer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles chkTransfer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
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
        If Trim(txtDOL.Text) = "" Then Exit Sub
        mm.txtVDate.Text = VB6.Format(txtDOL.Text, "DD/MM/YYYY")
        mYM = CInt(VB6.Format(Year(CDate(txtDOL.Text)), "0000") & VB6.Format(Month(CDate(txtDOL.Text)), "00"))
        mm.lblYM.Text = CStr(mYM)
        mBType = "F"

        If MainClass.ValidateWithMasterTable((txtEmpNo.Text), "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBSType = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtEmpNo.Text), "EMP_CODE", "DIV_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        End If

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
            mm.txtVDate.Text = VB6.Format(txtDOL.Text, "DD/MM/YYYY") ''Format(MainClass.LastDay(Month(lblRunDate), Year(lblRunDate)) & "/" & vb6.Format(Month(lblRunDate), "00") & "/" & Year(lblRunDate), "dd/mm/yyyy")
            mm.lblEmpCode.Text = Trim(txtEmpNo.Text)
            mm.txtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
        End If
    End Sub

    Private Sub cmdLeave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLeave.Click

        If Trim(txtEmpNo.Text) = "" Then Exit Sub
        If Trim(txtDOL.Text) = "" Then Exit Sub

        frmLeave.lblCode.Text = Trim(txtEmpNo.Text)
        frmLeave.lblEmpName.Text = Trim(TxtName.Text)
        frmLeave.lblvwMonth.Text = VB6.Format(txtDOL.Text, "MMMM , yyyy")
        frmLeave.lblMonth.Text = CStr(Month(CDate(txtDOL.Text)))
        frmLeave.lblDate.Text = VB6.Format(txtDOL.Text, "DD/MM/YYYY")
        frmLeave.lblYear.Text = IIf(Month(CDate(txtDOL.Text)) < 4, Year(CDate(txtDOL.Text)) - 1, Year(CDate(txtDOL.Text)))
        frmLeave.ShowDialog()
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click


        If PubUserID = "G0416" Or PubUserID = "000994" Then
            txtPaidDays.Enabled = True
        Else
            If Trim(txtEmpNo.Text) = "000840" Then
                Exit Sub
            End If
        End If

        '    If Val(txtPaidDays.Text) > 0 Then
        '        If CheckSalaryMade(txtEmpNo.Text, Format(txtDOL.Text, "DD/MM/YYYY")) = True Then
        '            MsgInformation " Salary Made Againt This Increment. So Cann't be Modified"
        '            Exit Sub
        '        End If
        '    End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsFFMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Call Show1()
        End If
    End Sub

    Private Sub cmdPolicyPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPolicyPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLetter(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
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
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String


        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        SqlStr = "SELECT * FROM " & vbCrLf _
            & " PAY_FFSETTLE_HDR IH, PAY_FFSETTLE_DET ID, " & vbCrLf _
            & " PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST HMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE(+)" & vbCrLf _
            & " AND IH.EMP_CODE=ID.EMP_CODE(+) " & vbCrLf _
            & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf _
            & " AND IH.EMP_CODE=EMP.EMP_CODE " & vbCrLf _
            & " AND ID.COMPANY_CODE=HMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND ID.SALHEADCODE=HMST.CODE(+) " & vbCrLf _
            & " AND IH.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY SEQ"

        mRptFileName = "FFSettlement.Rpt"

        mTitle = "FULL AND FINAL SETTLEMENT"
        mSubTitle = ""

        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
    End Sub
    Private Sub ReportForLetter(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String

        If Val(txtGratuityAmt.Text) <= 0 Then Exit Sub
        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        SqlStr = "SELECT * FROM " & vbCrLf & " PAY_FFSETTLE_HDR IH, " & vbCrLf & " PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE AND IH.EMP_CODE=EMP.EMP_CODE" & vbCrLf & " AND IH.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.EMP_CODE"

        mRptFileName = "GratuityPolicy.Rpt"

        mTitle = "Master Policy No. GG (CA) - 303015"
        mSubTitle = "Claim of Gratuity paid to " & Trim(TxtName.Text)

        Call ShowLetterReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
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

            '        ShowSalary txtEmpNo.Text, Format(txtDOL.Text, "MMMYYYY")
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

        SqlStr = " SELECT AC_POSTING FROM PAY_FFSETTLE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(txtEmpNo.Text, "000000") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            mAcPosting = IIf(IsDbNull(RsTemp.Fields("AC_POSTING").Value), "N", RsTemp.Fields("AC_POSTING").Value)

            If mAcPosting = "Y" Then
                MsgInformation("Account Posting Done, so Cann't be Deleted.")
                Exit Sub
            End If
        End If

        If Not RsFFMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                Clear1()
                '            If RsFFMain.EOF = True Then
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
        SqlStr = ""

        If MainClass.SearchGridMaster((txtEmpNo.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpNo.Text = AcName1
            TxtName.Text = AcName
            TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub

    End Sub
    Private Sub frmFFSettlement_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub




    Private Sub Reset_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Reset_Renamed.Click
        Call CalcGrossSalary("Y")
        txtDOL.Enabled = False
        txtPaidDays.Enabled = False
    End Sub

    Private Sub sprdDeduct_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdDeduct.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdDeduct_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdDeduct.LeaveCell
        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdDeduct.Row = eventArgs.row

        CalcPFESI()
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
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
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
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

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        Dim xMonth As Short
        Dim xYear As Short

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtEmpNo.Text = SprdView.Text

        TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(True))

        If Val(txtBSalary.Text) <> 0 Then
            Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        End If
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtAtcBasic_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAtcBasic.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAtcBasic_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAtcBasic.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBonusCurrYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusCurrYear.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBonusCurrYear_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusCurrYear.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBonusCurrYear_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBonusCurrYear.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBonusForYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusForYear.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBonusForYear_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusForYear.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBonusForYear_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBonusForYear.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBonusPerCurrYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusPerCurrYear.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBonusPerCurrYear_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusPerCurrYear.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBonusPerCurrYear_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBonusPerCurrYear.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBonusPerForYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusPerForYear.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBonusPerForYear_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusPerForYear.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBonusPerForYear_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBonusPerForYear.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub
    Private Function GetLTCAmount(ByRef mCode As String, ByRef mLTAPaidMonth As Double, ByRef xDesgCode As String) As Double

        On Error GoTo ErrPart
        Dim mStartingDate As String
        Dim mEndingDate As String
        Dim mLastDay As Integer
        Dim CntCurrentMonth As String
        Dim CntToMonth As String
        Dim mTotalWorkingDays As Double
        Dim mAbsent As Double
        Dim mActualLTA As Double
        Dim mPaidLTA As Double

        Dim mMonthCnt As Integer
        Dim mDayCnt As Integer
        Dim I As Integer
        Dim mCat As String
        Dim RsTemp As ADODB.Recordset = Nothing

        mMonthCnt = Fix(mLTAPaidMonth)
        mDayCnt = (mLTAPaidMonth - Fix(mLTAPaidMonth)) * 100
        If mLTAPaidMonth = 0 Then
            GetLTCAmount = 0
            Exit Function
        End If
        '
        If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCat = MasterNo
        End If

        CntCurrentMonth = "01/" & VB6.Format(txtLTCFrom.Text, "MM/YYYY")
        CntToMonth = VB6.Format(txtDOL.Text, "DD/MM/YYYY")

        I = 1
        Do While VB6.Format(CntCurrentMonth, "YYYYMM") <= VB6.Format(CntToMonth, "YYYYMM")
            mAbsent = 0
            mLastDay = MainClass.LastDay(Month(CDate(CntCurrentMonth)), Year(CDate(CntCurrentMonth)))

            If I = 1 Then
                mStartingDate = VB6.Format(txtLTCFrom.Text, "DD/MM/YYYY")
            Else
                mStartingDate = "01/" & VB6.Format(CntCurrentMonth, "MM/YYYY")
            End If

            If VB6.Format(CntCurrentMonth, "YYYYMM") = VB6.Format(CntToMonth, "YYYYMM") Then
                mEndingDate = VB6.Format(CntToMonth, "DD/MM/YYYY")
            Else
                mEndingDate = VB6.Format(mLastDay, "00") & "/" & VB6.Format(CntCurrentMonth, "MM/YYYY")
            End If

            mTotalWorkingDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartingDate), CDate(mEndingDate)) + 1
            '        If mCat = "M" Or mCat = "D" Then
            mAbsent = GetAbsentData(Trim(txtEmpNo.Text), mStartingDate, mEndingDate)
            '        End If

            mActualLTA = GetLTAAmount(Trim(txtEmpNo.Text), mStartingDate)

            mPaidLTA = mActualLTA * (mTotalWorkingDays - mAbsent) / mLastDay
            GetLTCAmount = GetLTCAmount + mPaidLTA

            CntCurrentMonth = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(CntCurrentMonth)))
            I = I + 1

        Loop


        SqlStr = " Select SUM(ID.PAID_AMOUNT) AS PAID_AMOUNT" & vbCrLf & " FROM PAY_LTA_ARREAR_HDR IH, PAY_LTA_ARREAR_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.FYEAR=ID.FYEAR " & vbCrLf & " AND IH.EMP_CODE=ID.EMP_CODE " & vbCrLf & " AND IH.ARREAR_DATE=ID.ARREAR_DATE " & vbCrLf & " AND IH.EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "' AND ID.LTA_MONTH>=TO_DATE('" & VB6.Format(txtLTCFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetLTCAmount = GetLTCAmount - IIf(IsDbNull(RsTemp.Fields("PAID_AMOUNT").Value), 0, RsTemp.Fields("PAID_AMOUNT").Value)
        End If
        Exit Function
ErrPart:
        GetLTCAmount = 0
    End Function
    Private Function GetLTCAmount24032014(ByRef mCode As String, ByRef mLTAPaidMonth As Double, ByRef xDesgCode As String) As Double

        On Error GoTo ErrPart
        Dim mStartingDate As String
        Dim mEndingDate As String
        Dim mLastDay As Integer
        Dim CntCurrentMonth As String
        Dim CntToMonth As String
        Dim mTotalWorkingDays As Double
        Dim mAbsent As Double
        Dim mActualLTA As Double
        Dim mPaidLTA As Double

        Dim mMonthCnt As Integer
        Dim mDayCnt As Integer
        Dim I As Integer
        Dim mCat As String

        mMonthCnt = Fix(mLTAPaidMonth)
        mDayCnt = (mLTAPaidMonth - Fix(mLTAPaidMonth)) * 100
        If mLTAPaidMonth = 0 Then
            GetLTCAmount24032014 = 0
            Exit Function
        End If
        '
        If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCat = MasterNo
        End If

        CntToMonth = VB6.Format(txtDOL.Text, "DD/MM/YYYY")

        '    to be check... ''Sandeep 21-09-2012
        If mLTAPaidMonth = Fix(mLTAPaidMonth) And mMonthCnt > 0 Then
            CntCurrentMonth = "01/" & VB6.Format(txtDOL.Text, "MM/YYYY")
            CntCurrentMonth = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -mMonthCnt + 1, CDate(CntCurrentMonth)))
        Else
            CntCurrentMonth = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -mMonthCnt, CDate(CntToMonth)))
        End If
        If mDayCnt > 0 Then
            CntCurrentMonth = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -mDayCnt + 1, CDate(CntCurrentMonth)))
        End If
        I = 1


        Do While VB6.Format(CntCurrentMonth, "YYYYMM") <= VB6.Format(CntToMonth, "YYYYMM")
            mAbsent = 0
            mLastDay = MainClass.LastDay(Month(CDate(CntCurrentMonth)), Year(CDate(CntCurrentMonth)))

            If I = 1 Then
                mStartingDate = VB6.Format(CntCurrentMonth, "DD/MM/YYYY")
            Else
                mStartingDate = "01/" & VB6.Format(CntCurrentMonth, "MM/YYYY")
            End If

            If VB6.Format(CntCurrentMonth, "YYYYMM") = VB6.Format(CntToMonth, "YYYYMM") Then
                mEndingDate = VB6.Format(CntToMonth, "DD/MM/YYYY")
            Else
                mEndingDate = VB6.Format(mLastDay, "00") & "/" & VB6.Format(CntCurrentMonth, "MM/YYYY")
            End If

            mTotalWorkingDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartingDate), CDate(mEndingDate)) + 1
            '        If mCat = "M" Or mCat = "D" Then
            mAbsent = GetAbsentData(Trim(txtEmpNo.Text), mStartingDate, mEndingDate)
            '        End If

            mActualLTA = GetLTAAmount(Trim(txtEmpNo.Text), mStartingDate)

            mPaidLTA = mActualLTA * (mTotalWorkingDays - mAbsent) / mLastDay
            GetLTCAmount24032014 = GetLTCAmount24032014 + mPaidLTA

            CntCurrentMonth = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(CntCurrentMonth)))
            I = I + 1

        Loop

        Exit Function
ErrPart:
        GetLTCAmount24032014 = 0
    End Function
    Private Function GetLTAAmount(ByRef mCode As String, ByRef mDate As String) As Double


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mFromEmpLeaveDate As String

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String

        GetLTAAmount = 0

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then

            mFromEmpCompany = IIf(IsDbNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDbNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            If CDate(mDate) <= CDate(mFromEmpLeaveDate) Then
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
            End If
        End If

        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & mToEmpCompany & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & mToEmpCompany & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConLTA & "" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & mToEmpCompany & "" & vbCrLf & " AND EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND SALARY_EFF_DATE - ADDDAYS_IN <= TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND ADD_DEDUCT.CODE IN (" & vbCrLf _
        ''            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf _
        ''            & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<='" & VB6.Format(mDate, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " UNION " & vbCrLf _
        ''            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf _
        ''            & " AND STATUS='C' AND CLOSED_DATE>'" & VB6.Format(mDate, "DD-MMM-YYYY") & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetLTAAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        GetLTAAmount = 0
    End Function

    Private Function GetAbsentData(ByRef mCode As String, ByRef mFromDate As String, ByRef mToDate As String) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mFromEmpLeaveDate As String

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String

        GetAbsentData = 0

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then

            mFromEmpCompany = IIf(IsDbNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDbNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            If CDate(mToDate) <= CDate(mFromEmpLeaveDate) Then
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
            End If
        End If

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE" & vbCrLf & " COMPANY_CODE =" & mToEmpCompany & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(mFromDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mToEmpCode & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                If RsTemp.Fields("FIRSTHALF").Value = ABSENT Then
                    GetAbsentData = GetAbsentData + 0.5
                ElseIf RsTemp.Fields("FIRSTHALF").Value = WOPAY Then
                    GetAbsentData = GetAbsentData + 0.5
                End If

                If RsTemp.Fields("SECONDHALF").Value = ABSENT Then
                    GetAbsentData = GetAbsentData + 0.5
                ElseIf RsTemp.Fields("SECONDHALF").Value = WOPAY Then
                    GetAbsentData = GetAbsentData + 0.5
                End If

                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetAbsentData = 0
    End Function
    'Private Function GetLTCAmount(mCode As String, mLTAPaidMonth As Double, xDesgCode As String) As Double
    'On Error GoTo ErrGetLTAAmount
    'Dim RsTemp As ADODB.Recordset = Nothing
    'Dim mFromDate As String
    'Dim mBSalary As Double
    'Dim mCat As String
    'Dim mEmpCat As String
    ''Dim xDesgCode As String
    'Dim mEMPDOJ As String
    'Dim mLTAMonth As Long
    'Dim mBaseOn As String
    'Dim mLTAPer As Double
    'Dim mWLTAPer As Double
    'Dim mLTAAmt As Double
    'Dim mLTAFrom As String
    'Dim mLTATo As String
    'Dim pPayableSalary As Double
    ''Dim mLTAPaidMonth As Double
    'Dim mMonthCount As Double
    'Dim mArrearAmount As Double
    ''Dim mArrearMonth As Double
    'Dim mWEFDate As String
    ''Dim mArrearAmount As Double
    ''Dim mLTCArrear As Double
    'Dim mLTADays As Double
    'Dim mLTALastMonthTo As String
    '
    '    mFromDate = Format(txtDOL.Text, "DD/MM/YYYY")
    '
    '
    '    SqlStr = " SELECT IH.BASICSALARY, IH.EMP_DESG_CODE, IH.TOT_ARR_MONTH, IH.SALARY_EFF_DATE,IH.PERCENTAGE, IH.AMOUNT" & vbCrLf _
    ''            & " FROM PAY_SALARYDEF_MST IH, PAY_SALARYHEAD_MST SMST" & vbCrLf _
    ''            & " WHERE " & vbCrLf _
    ''            & " IH.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf _
    ''            & " AND IH.ADD_DEDUCTCODE=SMST.CODE" & vbCrLf _
    ''            & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND IH.EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''            & " AND TYPE=" & ConLTA & "" & vbCrLf _
    ''            & " AND IH.SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf _
    ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
    ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''            & " AND SALARY_APP_DATE<='" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "')"
    '
    '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
    '
    '    If RsTemp.EOF = False Then
    '       mBSalary = IIf(IsNull(RsTemp!BASICSALARY), 0, RsTemp!BASICSALARY)
    '       xDesgCode = IIf(IsNull(RsTemp!EMP_DESG_CODE), "", RsTemp!EMP_DESG_CODE)
    '       mWEFDate = Format(IIf(IsNull(RsTemp!SALARY_EFF_DATE), "", RsTemp!SALARY_EFF_DATE), "DD/MM/YYYY")
    '
    '        mLTAPer = IIf(IsNull(RsTemp!PERCENTAGE), 0, RsTemp!PERCENTAGE)
    '        mLTAAmt = IIf(IsNull(RsTemp!Amount), 0, RsTemp!Amount)
    '        If mLTAPer = 0 Then
    '            mBaseOn = "A"
    '        End If
    '
    '        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '            mEmpCat = MasterNo
    '        End If
    '
    '        If mEmpCat = "R" Then
    '            If mBaseOn = "A" Then
    ''                GetLTCAmount = mLTAAmt * mLTAPaidMonth        ''IIf(IsNull(RsTemp!LTA_WORK_AMT), 0, RsTemp!LTA_WORK_AMT)
    '                GetLTCAmount = Format(mLTAAmt * Round(mLTAPaidMonth, 0), "0.00")       '' Format(mLTAAmt * Round(mLTAPaidMonth, 0) / 12, "0.00")
    '                If mLTAPaidMonth - Round(mLTAPaidMonth, 0) > 0 Then
    '                    mLTAPaidMonth = Format(mLTAPaidMonth - Round(mLTAPaidMonth, 0), "0.00") * 100
    '                    GetLTCAmount = GetLTCAmount + Format(mLTAAmt * mLTAPaidMonth / MainClass.LastDay(Month(txtDOL.Text), Year(txtDOL.Text)), "0.00")
    '                End If
    '            Else
    '
    '                mLTAMonth = Month(Format(txtDOJ.Text, "DD/MM/YYYY"))
    '
    '                Select Case mLTAMonth
    '                    Case 1, 2, 3
    '                        mLTAFrom = Format("01/" & mLTAMonth & "/" & Year(RsCompany!START_DATE), "DD/MM/YYYY")
    '                    Case 4, 5, 6, 7, 8, 9, 10, 11, 12
    '                        mLTAFrom = Format("01/" & mLTAMonth & "/" & Year(RsCompany!START_DATE) - 1, "DD/MM/YYYY")
    '                End Select
    '                mLTATo = DateAdd("m", mLTAPaidMonth, mLTAFrom) - 1
    ''                    mLTATo = MainClass.LastDay(Month(mLTATo), Year(mLTATo)) & "/" & vb6.Format(mLTATo, "MM/YYYY")
    '
    '                SqlStr = " SELECT DISTINCT SAL_DATE, PAYABLESALARY AS BASICSALARY1" & vbCrLf _
    ''                        & " FROM PAY_SAL_TRN SALTRN" & vbCrLf _
    ''                        & " WHERE " & vbCrLf _
    ''                        & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''                        & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''                        & " AND SAL_DATE>='" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "'" & vbCrLf _
    ''                        & " AND SAL_DATE<='" & VB6.Format(mLTATo, "DD-MMM-YYYY") & "'"
    '
    '
    '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
    '
    '                If RsTemp.EOF = False Then
    '                    pPayableSalary = 0
    '                    Do While RsTemp.EOF = False
    '                        pPayableSalary = pPayableSalary + IIf(IsNull(RsTemp!BASICSALARY1), 0, RsTemp!BASICSALARY1)
    '                        RsTemp.MoveNext
    '                    Loop
    '                End If
    '                GetLTCAmount = pPayableSalary * mLTAPer * 0.01
    '            End If
    '        Else
    '            If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                mCat = MasterNo
    '            End If
    '
    '            If mCat = "M" Or mCat = "D" Or RsCompany.Fields("COMPANY_CODE").Value = 11 Then  ''mBSalary
    '                mLTATo = Format(txtDOL.Text, "DD/MM/YYYY")
    '
    '                mLTAFrom = DateAdd("m", Int(txtLTCMonth) * -1, mLTATo)
    '                mLTADays = Val(txtLTCMonth * 100) - (Int(txtLTCMonth) * 100) - Val(txtPaidDays.Text)
    '                mLTAFrom = DateAdd("d", ((Val(txtLTCMonth * 100) - (Int(txtLTCMonth) * 100)) * -1), mLTAFrom)
    '                mLTADays = Day(mLTAFrom)
    '                mLTAFrom = "01/" & vb6.Format(mLTAFrom, "MM/YYYY")
    '                mLTALastMonthTo = MainClass.LastDay(Month(mLTAFrom), Year(mLTAFrom)) & "/" & vb6.Format(mLTAFrom, "MM/YYYY")
    '
    '                SqlStr = " SELECT DISTINCT SAL_DATE, ISARREAR, PAYABLESALARY AS BASICSALARY1" & vbCrLf _
    ''                        & " FROM PAY_SAL_TRN SALTRN" & vbCrLf _
    ''                        & " WHERE " & vbCrLf _
    ''                        & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''                        & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''                        & " AND SAL_DATE>='" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "'" & vbCrLf _
    ''                        & " AND SAL_DATE<='" & VB6.Format(mLTATo, "DD-MMM-YYYY") & "'"
    '
    '                SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"
    '
    '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
    '
    '                If RsTemp.EOF = False Then
    '                    pPayableSalary = 0
    '                    Do While RsTemp.EOF = False
    '                        If RsTemp!IsArrear = "N" Then
    '                            mMonthCount = mMonthCount + 1
    '                        End If
    '                        pPayableSalary = pPayableSalary + IIf(IsNull(RsTemp!BASICSALARY1), 0, RsTemp!BASICSALARY1)
    '                        RsTemp.MoveNext
    '                    Loop
    '                End If
    '
    '                mEMPDOJ = ""
    '
    '                If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                    mEMPDOJ = MasterNo
    '                End If
    '
    '                If CVDate(mEMPDOJ) < CVDate(mLTAFrom) Then
    '                    SqlStr = " SELECT DISTINCT SAL_DATE, ISARREAR, PAYABLESALARY AS BASICSALARY1" & vbCrLf _
    ''                            & " FROM PAY_SAL_TRN SALTRN" & vbCrLf _
    ''                            & " WHERE " & vbCrLf _
    ''                            & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    ''                            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
    ''                            & " AND SAL_DATE>='" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "'" & vbCrLf _
    ''                            & " AND SAL_DATE<='" & VB6.Format(mLTALastMonthTo, "DD-MMM-YYYY") & "'"
    '
    '                    SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"
    '
    '                    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
    '
    '                    If RsTemp.EOF = False Then
    '                        pPayableSalary = pPayableSalary - (IIf(IsNull(RsTemp!BASICSALARY1), 0, RsTemp!BASICSALARY1) * mLTADays / MainClass.LastDay(Month(mLTAFrom), Year(mLTAFrom)))
    '                    End If
    '                End If
    '
    ''                    If mMonthCount < mLTAPaidMonth Then
    ''                        pPayableSalary = pPayableSalary + Val(txtBSalary.Text)
    ''                    End If
    '
    '                If Val(txtPaidDays.Text) > 0 Then
    '                    pPayableSalary = pPayableSalary + Val(txtBSalary.Text)
    '                End If
    '
    '                mArrearAmount = GetCurrentArrearPayable(mCode, "Y")
    '                pPayableSalary = pPayableSalary + mArrearAmount
    '
    '                GetLTCAmount = pPayableSalary * mLTAPer * 0.01
    '            ElseIf mCat = "S" Then
    '                GetLTCAmount = Format(mLTAAmt * Round(mLTAPaidMonth, 0), "0.00")       '' Format(mLTAAmt * Round(mLTAPaidMonth, 0) / 12, "0.00")
    '                If mLTAPaidMonth - Round(mLTAPaidMonth, 0) > 0 Then
    '                    mLTAPaidMonth = Format(mLTAPaidMonth - Round(mLTAPaidMonth, 0), "0.00") * 100
    '                    GetLTCAmount = GetLTCAmount + Format(mLTAAmt * mLTAPaidMonth / MainClass.LastDay(Month(txtDOL.Text), Year(txtDOL.Text)), "0.00")
    '                End If
    '            End If
    '        End If
    '    Else
    '        GetLTCAmount = 0
    '    End If
    ''GetLTCAmount = 13011
    'Exit Function
    'ErrGetLTAAmount:
    '    GetLTCAmount = 0
    'End Function
    Private Function GetLTCAmountOld(ByRef mCode As String, ByRef mLTAPaidMonth As Double, ByRef xDesgCode As String) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFromDate As String
        Dim mBSalary As Double
        Dim mCat As String
        Dim mEmpCat As String
        'Dim xDesgCode As String
        'Dim mEMPDOJ As String
        Dim mLTAMonth As Integer
        Dim mBaseOn As String
        Dim mLTAPer As Double
        Dim mWLTAPer As Double
        Dim mLTAAmt As Double
        Dim mLTAFrom As String
        Dim mLTATo As String
        Dim pPayableSalary As Double
        'Dim mLTAPaidMonth As Double
        Dim mMonthCount As Double
        Dim mArrearAmount As Double
        'Dim mArrearMonth As Double
        Dim mWEFDate As String
        'Dim mArrearAmount As Double
        'Dim mLTCArrear As Double
        Dim mLTADays As Double
        Dim mLTALastMonthTo As String

        mFromDate = VB6.Format(txtDOL.Text, "DD/MM/YYYY")


        SqlStr = " SELECT BASICSALARY,EMP_DESG_CODE, TOT_ARR_MONTH,SALARY_EFF_DATE" & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE<=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mBSalary = IIf(IsDbNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value)
            xDesgCode = IIf(IsDbNull(RsTemp.Fields("EMP_DESG_CODE").Value), "", RsTemp.Fields("EMP_DESG_CODE").Value)
            '       mArrearMonth = IIf(IsNull(RsTemp!TOT_ARR_MONTH), 0, RsTemp!TOT_ARR_MONTH)
            mWEFDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SALARY_EFF_DATE").Value), "", RsTemp.Fields("SALARY_EFF_DATE").Value), "DD/MM/YYYY")

            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MINLIMIT<=" & Val(CStr(mBSalary)) & " AND MAXLIMIT>=" & Val(CStr(mBSalary)) & " " & vbCrLf & " AND WEF_DATE=(SELECT MAX(WEF_DATE) " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND WEF_DATE<=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mBaseOn = IIf(IsDbNull(RsTemp.Fields("LTA_WORK_BASE_ON").Value), "A", RsTemp.Fields("LTA_WORK_BASE_ON").Value)
                mLTAPer = IIf(IsDbNull(RsTemp.Fields("LTA_PER").Value), 0, RsTemp.Fields("LTA_PER").Value)
                mWLTAPer = IIf(IsDbNull(RsTemp.Fields("LTA_WORK_PER").Value), 0, RsTemp.Fields("LTA_WORK_PER").Value)
                mLTAAmt = IIf(IsDbNull(RsTemp.Fields("LTAAMT").Value), 0, RsTemp.Fields("LTAAMT").Value)
                If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mEmpCat = MasterNo
                End If

                If mEmpCat = "R" Then
                    If mBaseOn = "A" Then
                        GetLTCAmountOld = IIf(IsDbNull(RsTemp.Fields("LTA_WORK_AMT").Value), 0, RsTemp.Fields("LTA_WORK_AMT").Value)
                    Else

                        mLTAMonth = Month(CDate(VB6.Format(txtDOJ.Text, "DD/MM/YYYY")))

                        Select Case mLTAMonth
                            Case 1, 2, 3
                                mLTAFrom = VB6.Format("01/" & mLTAMonth & "/" & Year(RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")
                            Case 4, 5, 6, 7, 8, 9, 10, 11, 12
                                mLTAFrom = VB6.Format("01/" & mLTAMonth & "/" & Year(RsCompany.Fields("START_DATE").Value) - 1, "DD/MM/YYYY")
                        End Select
                        mLTATo = CStr(System.Date.FromOADate(DateAdd(Microsoft.VisualBasic.DateInterval.Month, mLTAPaidMonth, CDate(mLTAFrom)).ToOADate - 1))
                        '                    mLTATo = MainClass.LastDay(Month(mLTATo), Year(mLTATo)) & "/" & vb6.Format(mLTATo, "MM/YYYY")

                        SqlStr = " SELECT DISTINCT SAL_DATE, PAYABLESALARY AS BASICSALARY1" & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mLTATo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                        If RsTemp.EOF = False Then
                            pPayableSalary = 0
                            Do While RsTemp.EOF = False
                                pPayableSalary = pPayableSalary + IIf(IsDbNull(RsTemp.Fields("BASICSALARY1").Value), 0, RsTemp.Fields("BASICSALARY1").Value)
                                RsTemp.MoveNext()
                            Loop
                        End If
                        GetLTCAmountOld = pPayableSalary * mWLTAPer * 0.01
                    End If
                Else
                    If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCat = MasterNo
                    End If

                    If mCat = "M" Or mCat = "D" Then ''mBSalary

                        '                    mLTAMonth = Month(Format(txtDOJ.Text, "DD/MM/YYYY"))
                        '
                        '                    Select Case mLTAMonth
                        '                        Case 1, 2, 3
                        ''                            mLTAFrom = Format("01/" & mLTAMonth & "/" & Year(RsCompany!START_DATE), "DD/MM/YYYY")
                        '                        Case 4, 5, 6, 7, 8, 9, 10, 11, 12
                        ''                            mLTAFrom = Format("01/" & mLTAMonth & "/" & Year(RsCompany!START_DATE) - 1, "DD/MM/YYYY")
                        '                    End Select
                        '
                        '                    mLTATo = DateAdd("m", Round(mLTAPaidMonth, 0), mLTAFrom) - 1
                        ''                    mLTATo = MainClass.LastDay(Month(mLTATo), Year(mLTATo)) & "/" & vb6.Format(mLTATo, "MM/YYYY")

                        mLTATo = VB6.Format(txtDOL.Text, "DD/MM/YYYY")

                        mLTAFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Int(CDbl(txtLTCMonth.Text)) * -1, CDate(mLTATo)))
                        mLTADays = Val(CStr(CDbl(txtLTCMonth.Text) * 100)) - (Int(CDbl(txtLTCMonth.Text)) * 100) - Val(txtPaidDays.Text)
                        mLTAFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, (Val(CStr(CDbl(txtLTCMonth.Text) * 100)) - (Int(CDbl(txtLTCMonth.Text)) * 100)) * -1, CDate(mLTAFrom)))
                        mLTADays = VB.Day(CDate(mLTAFrom))
                        mLTAFrom = "01/" & VB6.Format(mLTAFrom, "MM/YYYY")
                        mLTALastMonthTo = MainClass.LastDay(Month(CDate(mLTAFrom)), Year(CDate(mLTAFrom))) & "/" & VB6.Format(mLTAFrom, "MM/YYYY")

                        SqlStr = " SELECT DISTINCT SAL_DATE, ISARREAR, PAYABLESALARY AS BASICSALARY1" & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mLTATo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                        If RsTemp.EOF = False Then
                            pPayableSalary = 0
                            Do While RsTemp.EOF = False
                                If RsTemp.Fields("IsArrear").Value = "N" Then
                                    mMonthCount = mMonthCount + 1
                                End If
                                pPayableSalary = pPayableSalary + IIf(IsDbNull(RsTemp.Fields("BASICSALARY1").Value), 0, RsTemp.Fields("BASICSALARY1").Value)
                                RsTemp.MoveNext()
                            Loop
                        End If

                        SqlStr = " SELECT DISTINCT SAL_DATE, ISARREAR, PAYABLESALARY AS BASICSALARY1" & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mLTALastMonthTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                        If RsTemp.EOF = False Then
                            pPayableSalary = pPayableSalary - (IIf(IsDbNull(RsTemp.Fields("BASICSALARY1").Value), 0, RsTemp.Fields("BASICSALARY1").Value) * mLTADays / MainClass.LastDay(Month(CDate(mLTAFrom)), Year(CDate(mLTAFrom))))
                        End If

                        '                    If mMonthCount < mLTAPaidMonth Then
                        '                        pPayableSalary = pPayableSalary + Val(txtBSalary.Text)
                        '                    End If

                        If Val(txtPaidDays.Text) > 0 Then
                            pPayableSalary = pPayableSalary + Val(txtBSalary.Text)
                        End If

                        mArrearAmount = GetCurrentArrearBasic(mCode, "Y")
                        pPayableSalary = pPayableSalary + mArrearAmount

                        GetLTCAmountOld = pPayableSalary * mLTAPer * 0.01
                    ElseIf mCat = "S" Then
                        GetLTCAmountOld = CDbl(VB6.Format(mLTAAmt * System.Math.Round(mLTAPaidMonth, 0) / 12, "0.00"))
                        If mLTAPaidMonth - System.Math.Round(mLTAPaidMonth, 0) > 0 Then
                            mLTAPaidMonth = CDbl(VB6.Format(mLTAPaidMonth - System.Math.Round(mLTAPaidMonth, 0), "0.00")) * 100
                            GetLTCAmountOld = GetLTCAmountOld + CDbl(VB6.Format(mLTAAmt * mLTAPaidMonth / 365, "0.00"))
                        End If
                    End If
                End If
            Else
                GetLTCAmountOld = 0
            End If
        Else
            GetLTCAmountOld = 0
        End If


        Exit Function
ErrGetLTAAmount:
        GetLTCAmountOld = 0
    End Function

    Private Function GetLastUnitLTCAmount(ByRef mCode As String, ByRef mLTAPaidMonth As Double, ByRef xDesgCode As String) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFromDate As String
        Dim mBSalary As Double
        Dim mCat As String
        Dim mEmpCat As String
        'Dim xDesgCode As String
        'Dim mEMPDOJ As String
        Dim mLTAMonth As Integer
        Dim mBaseOn As String
        Dim mLTAPer As Double
        Dim mWLTAPer As Double
        Dim mLTAAmt As Double
        Dim mLTAFrom As String
        Dim mLTATo As String
        Dim pPayableSalary As Double
        'Dim mLTAPaidMonth As Double
        Dim mMonthCount As Double
        Dim mArrearAmount As Double
        'Dim mArrearMonth As Double
        Dim mWEFDate As String
        'Dim mArrearAmount As Double
        'Dim mLTCArrear As Double
        Dim mLTADays As Double
        Dim mLTALastMonthTo As String

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim RsEmpTrf As ADODB.Recordset

        mFromDate = VB6.Format(txtDOL.Text, "DD/MM/YYYY")

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpTrf, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmpTrf.EOF = False Then

            mToEmpCompany = IIf(IsDbNull(RsEmpTrf.Fields("FROM_COMPANY_CODE").Value), "", RsEmpTrf.Fields("FROM_COMPANY_CODE").Value)
            mToEmpCode = IIf(IsDbNull(RsEmpTrf.Fields("FROM_EMP_CODE").Value), "", RsEmpTrf.Fields("FROM_EMP_CODE").Value)

            SqlStr = " SELECT BASICSALARY,EMP_DESG_CODE, TOT_ARR_MONTH,SALARY_EFF_DATE" & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & mToEmpCompany & "" & vbCrLf & " AND EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & mToEmpCompany & "" & vbCrLf & " AND EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND SALARY_APP_DATE<=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                mBSalary = IIf(IsDbNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value)
                xDesgCode = IIf(IsDbNull(RsTemp.Fields("EMP_DESG_CODE").Value), "", RsTemp.Fields("EMP_DESG_CODE").Value)
                '       mArrearMonth = IIf(IsNull(RsTemp!TOT_ARR_MONTH), 0, RsTemp!TOT_ARR_MONTH)
                mWEFDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SALARY_EFF_DATE").Value), "", RsTemp.Fields("SALARY_EFF_DATE").Value), "DD/MM/YYYY")

                SqlStr = " SELECT * " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE = " & mToEmpCompany & "" & vbCrLf & " AND MINLIMIT<=" & Val(CStr(mBSalary)) & " AND MAXLIMIT>=" & Val(CStr(mBSalary)) & " " & vbCrLf & " AND WEF_DATE=(SELECT MAX(WEF_DATE) " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE = " & mToEmpCompany & "" & vbCrLf & " AND WEF_DATE<=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                If RsTemp.EOF = False Then
                    mBaseOn = IIf(IsDbNull(RsTemp.Fields("LTA_WORK_BASE_ON").Value), "A", RsTemp.Fields("LTA_WORK_BASE_ON").Value)
                    mLTAPer = IIf(IsDbNull(RsTemp.Fields("LTA_PER").Value), 0, RsTemp.Fields("LTA_PER").Value)
                    mWLTAPer = IIf(IsDbNull(RsTemp.Fields("LTA_WORK_PER").Value), 0, RsTemp.Fields("LTA_WORK_PER").Value)
                    mLTAAmt = IIf(IsDbNull(RsTemp.Fields("LTAAMT").Value), 0, RsTemp.Fields("LTAAMT").Value)
                    If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mEmpCat = MasterNo
                    End If

                    If mEmpCat = "R" Then
                        If mBaseOn = "A" Then
                            GetLastUnitLTCAmount = GetLastUnitLTCAmount + IIf(IsDbNull(RsTemp.Fields("LTA_WORK_AMT").Value), 0, RsTemp.Fields("LTA_WORK_AMT").Value)
                        Else

                            mLTAMonth = Month(CDate(VB6.Format(txtDOJ.Text, "DD/MM/YYYY")))

                            Select Case mLTAMonth
                                Case 1, 2, 3
                                    mLTAFrom = VB6.Format("01/" & mLTAMonth & "/" & Year(RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")
                                Case 4, 5, 6, 7, 8, 9, 10, 11, 12
                                    mLTAFrom = VB6.Format("01/" & mLTAMonth & "/" & Year(RsCompany.Fields("START_DATE").Value) - 1, "DD/MM/YYYY")
                            End Select
                            mLTATo = CStr(System.Date.FromOADate(DateAdd(Microsoft.VisualBasic.DateInterval.Month, mLTAPaidMonth, CDate(mLTAFrom)).ToOADate - 1))
                            '                    mLTATo = MainClass.LastDay(Month(mLTATo), Year(mLTATo)) & "/" & vb6.Format(mLTATo, "MM/YYYY")

                            SqlStr = " SELECT DISTINCT SAL_DATE, PAYABLESALARY AS BASICSALARY1" & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mToEmpCompany & "" & vbCrLf & " AND EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mLTATo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                            If RsTemp.EOF = False Then
                                pPayableSalary = 0
                                Do While RsTemp.EOF = False
                                    pPayableSalary = pPayableSalary + IIf(IsDbNull(RsTemp.Fields("BASICSALARY1").Value), 0, RsTemp.Fields("BASICSALARY1").Value)
                                    RsTemp.MoveNext()
                                Loop
                            End If
                            GetLastUnitLTCAmount = GetLastUnitLTCAmount = pPayableSalary * mWLTAPer * 0.01
                        End If
                    Else
                        If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mCat = MasterNo
                        End If

                        If mCat = "M" Or mCat = "D" Then ''mBSalary

                            mLTATo = VB6.Format(txtDOL.Text, "DD/MM/YYYY")

                            mLTAFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Int(CDbl(txtLTCMonth.Text)) * -1, CDate(mLTATo)))
                            mLTADays = Val(CStr(CDbl(txtLTCMonth.Text) * 100)) - (Int(CDbl(txtLTCMonth.Text)) * 100) - Val(txtPaidDays.Text)
                            mLTAFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, (Val(CStr(CDbl(txtLTCMonth.Text) * 100)) - (Int(CDbl(txtLTCMonth.Text)) * 100)) * -1, CDate(mLTAFrom)))
                            mLTADays = VB.Day(CDate(mLTAFrom))
                            mLTAFrom = "01/" & VB6.Format(mLTAFrom, "MM/YYYY")
                            mLTALastMonthTo = MainClass.LastDay(Month(CDate(mLTAFrom)), Year(CDate(mLTAFrom))) & "/" & VB6.Format(mLTAFrom, "MM/YYYY")

                            SqlStr = " SELECT DISTINCT SAL_DATE, ISARREAR, PAYABLESALARY AS BASICSALARY1" & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mToEmpCompany & "" & vbCrLf & " AND EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mLTATo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                            SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"

                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                            If RsTemp.EOF = False Then
                                pPayableSalary = 0
                                Do While RsTemp.EOF = False
                                    If RsTemp.Fields("IsArrear").Value = "N" Then
                                        mMonthCount = mMonthCount + 1
                                    End If
                                    pPayableSalary = pPayableSalary + IIf(IsDbNull(RsTemp.Fields("BASICSALARY1").Value), 0, RsTemp.Fields("BASICSALARY1").Value)
                                    RsTemp.MoveNext()
                                Loop
                            End If

                            '                        SqlStr = " SELECT DISTINCT SAL_DATE, ISARREAR, PAYABLESALARY AS BASICSALARY1" & vbCrLf _
                            ''                                & " FROM PAY_SAL_TRN SALTRN" & vbCrLf _
                            ''                                & " WHERE " & vbCrLf _
                            ''                                & " SALTRN.Company_Code = " & mToEmpCompany & "" & vbCrLf _
                            ''                                & " AND EMP_CODE = '" & mToEmpCode & "'" & vbCrLf _
                            ''                                & " AND SAL_DATE>='" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "'" & vbCrLf _
                            ''                                & " AND SAL_DATE<='" & VB6.Format(mLTALastMonthTo, "DD-MMM-YYYY") & "'"
                            '
                            '                        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"
                            '
                            '                        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic
                            '
                            '                        If RsTemp.EOF = False Then
                            '                            pPayableSalary = pPayableSalary - (IIf(IsNull(RsTemp!BASICSALARY1), 0, RsTemp!BASICSALARY1) * mLTADays / MainClass.LastDay(Month(mLTAFrom), Year(mLTAFrom)))
                            '                        End If
                            '
                            '
                            '                        If Val(txtPaidDays.Text) > 0 Then
                            '                            pPayableSalary = pPayableSalary + Val(txtBSalary.Text)
                            '                        End If

                            '                        mArrearAmount = GetCurrentArrearBasic(mCode)
                            '                        pPayableSalary = pPayableSalary + mArrearAmount

                            GetLastUnitLTCAmount = GetLastUnitLTCAmount + pPayableSalary * mLTAPer * 0.01
                        ElseIf mCat = "S" Then
                            GetLastUnitLTCAmount = CDbl(VB6.Format(mLTAAmt * System.Math.Round(mLTAPaidMonth, 0) / 12, "0.00"))
                            If mLTAPaidMonth - System.Math.Round(mLTAPaidMonth, 0) > 0 Then
                                mLTAPaidMonth = CDbl(VB6.Format(mLTAPaidMonth - System.Math.Round(mLTAPaidMonth, 0), "0.00")) * 100
                                GetLastUnitLTCAmount = GetLastUnitLTCAmount + CDbl(VB6.Format(mLTAAmt * mLTAPaidMonth / 365, "0.00"))
                            End If
                        End If
                    End If
                Else
                    '                GetLastUnitLTCAmount = 0
                End If
            Else
                '            GetLastUnitLTCAmount = 0
            End If
            GoTo SearchRow
        End If

        Exit Function
ErrGetLTAAmount:
        GetLastUnitLTCAmount = 0
    End Function
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

    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtChqNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCompAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCompAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCompAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCompAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCompMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompMonth.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCompMonth_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompMonth.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCompMonth_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCompMonth.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDOJ_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOJ.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOL_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOL.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mLastDay As Integer
        Dim mLeaveDay As Integer
        Dim mStartDate As String
        Dim mWDays As Double


        If Trim(txtDOL.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOL.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        mStartDate = VB6.Format("01/" & VB6.Format(txtDOL.Text, "MM/YYYY"), "DD/MM/YYYY")

        mLeaveDay = VB.Day(CDate(txtDOL.Text))



        If Val(txtPaidDays.Text) > 0 Then
            mWDays = CalcAttnPresent((txtEmpNo.Text), VB6.Format(mStartDate, "DD/MM/YYYY"), VB6.Format(txtDOL.Text, "DD/MM/YYYY"), VB6.Format(txtDOJ.Text, "DD/MM/YYYY"))
            If Val(txtPaidDays.Text) <> Val(CStr(mWDays)) Then
                MsgInformation("Paid Days is not equal to Present Date.")
                Cancel = True
                GoTo EventExitSub
            End If
            If Val(txtPaidDays.Text) > Val(CStr(mLeaveDay)) Then
                MsgInformation("Paid Days Cann't be greater than Leave Day.")
                Cancel = True
                GoTo EventExitSub
            End If
        End If



        If ADDMode = True Then
            Call ShowAtcSalary((txtEmpNo.Text), (txtDOL.Text))
        End If
        Call txtPaidDays_Validating(txtPaidDays, New System.ComponentModel.CancelEventArgs(True))
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtELAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtELAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtELAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtELAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtELAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtELAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtELDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtELDays.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtELDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtELDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtELDays_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtELDays.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
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

    Private Sub txtExGratiaAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExGratiaAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExGratiaAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExGratiaAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtExGratiaAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExGratiaAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtExGratiaMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExGratiaMonth.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExGratiaMonth_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExGratiaMonth.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtExGratiaMonth_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExGratiaMonth.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtFName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGratuityAmt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGratuityAmt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGratuityAmt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGratuityAmt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtGratuityMon_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGratuityMon.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGratuityMon_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGratuityMon.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtGratuityMon_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGratuityMon.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
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

    Private Sub txtIncAmtForMon_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIncAmtForMon.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIncAmtForMon_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIncAmtForMon.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtIncAmtPreMon_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIncAmtPreMon.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIncAmtPreMon_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIncAmtPreMon.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtIncArrear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIncArrear.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIncArrear_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIncArrear.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtIncArrear_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIncArrear.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIncHoursForMon_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIncHoursForMon.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIncHoursForMon_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIncHoursForMon.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtIncHoursForMon_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIncHoursForMon.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'CalcPFESI
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIncHoursPreMon_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIncHoursPreMon.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIncHoursPreMon_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIncHoursPreMon.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtIncHoursPreMon_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIncHoursPreMon.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLTCAmt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLTCAmt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLTCAmt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLTCAmt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLTCFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLTCFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLTCFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLTCFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtLTCFrom.Text) = "" Then GoTo EventExitSub

        If Trim(txtDOL.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtLTCFrom.Text) Then
            MsgInformation("Invalid LTC From Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If Not IsDate(txtDOL.Text) Then
            MsgInformation("Invaild Date of Leaving.")
            '        Cancel = True
            GoTo EventExitSub
        End If

        If CDate(txtLTCFrom.Text) > CDate(txtDOL.Text) Then
            MsgInformation("LTC Date can't be greater than Leave Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        txtLTCMonth.Text = PeriodinMonth_Days((txtLTCFrom.Text), (txtDOL.Text))

        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLTCMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLTCMonth.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLTCMonth_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLTCMonth.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub txtLTCMonth_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLTCMonth.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
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
    Private Sub frmFFSettlement_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub

        SqlStr = "Select * From PAY_FFSETTLE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFFMain, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = "Select * From PAY_FFSETTLE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFFDetail, ADODB.LockTypeEnum.adLockOptimistic)

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
    Private Sub frmFFSettlement_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
    Private Sub frmFFSettlement_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsFFMain = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer
        'Dim mEmpDesg As String
        Dim pEmpCode As String
        Dim mPaidDays As Double
        Dim RsTemp As ADODB.Recordset = Nothing



        If RsFFMain.EOF = False Then
            pEmpCode = Trim(IIf(IsDbNull(RsFFMain.Fields("EMP_CODE").Value), 0, RsFFMain.Fields("EMP_CODE").Value))
            If PubUserID = "G0416" Then ''Or PubUserID = "000994"
            Else
                If Trim(pEmpCode) = "000840" Then
                    Exit Sub
                End If
            End If


            txtEmpNo.Text = pEmpCode

            SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsEmp.EOF = False Then
                TxtName.Text = IIf(IsDbNull(RsEmp.Fields("EMP_NAME").Value), "", RsEmp.Fields("EMP_NAME").Value)
                txtFName.Text = IIf(IsDbNull(RsEmp.Fields("EMP_FNAME").Value), "", RsEmp.Fields("EMP_FNAME").Value)
                '            txtDOJ.Text = Format(IIf(IsNull(RsEmp!EMP_DOJ), "", RsEmp!EMP_DOJ), "DD/MM/YYYY")


                ''29/03/2010
                '            If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_DESG_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mEmpDesg = IIf(IsNull(MasterNo), "-1", MasterNo)
                '                If MainClass.ValidateWithMasterTable(mEmpDesg, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    lblDesg.Caption = MasterNo
                '                End If
                '            End If
                '
                '            If MainClass.ValidateWithMasterTable(lblDesg.Caption, "DESG_DESC", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                cbodesignation.Text = MasterNo
                '            End If

            End If

            txtAtcBasic.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("BASIC_SALARY").Value), 0, RsFFMain.Fields("BASIC_SALARY").Value), "0.00")
            txtBSalary.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("PAID_BASICSALARY").Value), 0, RsFFMain.Fields("PAID_BASICSALARY").Value), "0.00")
            txtPaidDays.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("PAID_DAYS").Value), 0, RsFFMain.Fields("PAID_DAYS").Value), "0.00")
            txtDOL.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("EMP_LEAVE_DATE").Value), "", RsFFMain.Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")
            txtLTCFrom.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("EMP_LTC_FROM").Value), "", RsFFMain.Fields("EMP_LTC_FROM").Value), "DD/MM/YYYY")


            SqlStr = " SELECT GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & pEmpCode & "',TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC FROM DUAL"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                cbodesignation.Text = IIf(IsDbNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)
                lblDesg.Text = IIf(IsDbNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)
            End If


            Call ShowAtcSalary((txtEmpNo.Text), (txtDOL.Text))
            Call ShowDetail1(pEmpCode)


            txtReason.Text = IIf(IsDbNull(RsFFMain.Fields("LEAVE_REASON").Value), "", RsFFMain.Fields("LEAVE_REASON").Value)

            txtChqNo.Text = IIf(IsDbNull(RsFFMain.Fields("CHQ_NO").Value), "", RsFFMain.Fields("CHQ_NO").Value)
            txtBankName.Text = IIf(IsDbNull(RsFFMain.Fields("BANK_NAME").Value), "", RsFFMain.Fields("BANK_NAME").Value)
            txtRemarks.Text = IIf(IsDbNull(RsFFMain.Fields("Remarks").Value), "", RsFFMain.Fields("Remarks").Value)

            '        txtBSalary.Text = Format(RsFFMain!BASIC_SALARY, "0.00")


            txtIncHoursForMon.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("INC_HOUR_FORMON").Value), 0, RsFFMain.Fields("INC_HOUR_FORMON").Value), "0.00")
            txtIncAmtForMon.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("INC_AMT_FORMON").Value), 0, RsFFMain.Fields("INC_AMT_FORMON").Value), "0.00")
            txtIncHoursPreMon.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("INC_HOUR_PREMON").Value), 0, RsFFMain.Fields("INC_HOUR_PREMON").Value), "0.00")
            txtIncAmtPreMon.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("INC_AMT_PREMON").Value), 0, RsFFMain.Fields("INC_AMT_PREMON").Value), "0.00")
            txtSalArrear.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("ARREAR_SAL").Value), 0, RsFFMain.Fields("ARREAR_SAL").Value), "0.00")
            txtIncArrear.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("ARREAR_INC").Value), 0, RsFFMain.Fields("ARREAR_INC").Value), "0.00")
            txtLTCMonth.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("LTC_MONTH").Value), 0, RsFFMain.Fields("LTC_MONTH").Value), "0.00")
            txtLTCAmt.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("LTC_AMOUNT").Value), 0, RsFFMain.Fields("LTC_AMOUNT").Value), "0.00")
            txtBonusForYear.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("BONUS_FORYEAR").Value), 0, RsFFMain.Fields("BONUS_FORYEAR").Value), "0.00")
            txtBonusCurrYear.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("BONUS_CURRYEAR").Value), 0, RsFFMain.Fields("BONUS_CURRYEAR").Value), "0.00")
            txtGratuityMon.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("GRATUITY_MONTH").Value), 0, RsFFMain.Fields("GRATUITY_MONTH").Value), "0.00")
            txtGratuityAmt.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("GRATUITY_AMOUNT").Value), 0, RsFFMain.Fields("GRATUITY_AMOUNT").Value), "0.00")
            txtNoticeMon.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("NOTICE_MONTH").Value), 0, RsFFMain.Fields("NOTICE_MONTH").Value), "0.00")
            txtNoticeamt.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("NOTICE_AMOUNT").Value), 0, RsFFMain.Fields("NOTICE_AMOUNT").Value), "0.00")
            txtOthers.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("OTHERS_AMOUNT").Value), 0, RsFFMain.Fields("OTHERS_AMOUNT").Value), "0.00")
            txtGSalary.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("GROSS_SALARY").Value), 0, RsFFMain.Fields("GROSS_SALARY").Value), "0.00")
            txtDeduction.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("DEDUCTION").Value), 0, RsFFMain.Fields("DEDUCTION").Value), "0.00")
            txtTotOthers.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("OTHER_TOTAL").Value), 0, RsFFMain.Fields("OTHER_TOTAL").Value), "0.00")
            txtNetSalary.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("NET_SALARY").Value), 0, RsFFMain.Fields("NET_SALARY").Value), "0.00")

            txtBonusPerForYear.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("BONUS_PER_FORYEAR").Value), 0, RsFFMain.Fields("BONUS_PER_FORYEAR").Value), "0.00")
            txtBonusPerCurrYear.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("BONUS_PER_CURRYEAR").Value), 0, RsFFMain.Fields("BONUS_PER_CURRYEAR").Value), "0.00")
            chkMannualPerBonus.CheckState = IIf(RsFFMain.Fields("MANNUAL_BONUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            txtELDays.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("EL_DAYS").Value), 0, RsFFMain.Fields("EL_DAYS").Value), "0.00")
            txtELAmount.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("EL_AMOUNT").Value), 0, RsFFMain.Fields("EL_AMOUNT").Value), "0.00")

            txtExGratiaMonth.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("EXGRATIA_MON").Value), 0, RsFFMain.Fields("EXGRATIA_MON").Value), "0.00")
            txtExGratiaAmount.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("EXGRATIA_AMOUNT").Value), 0, RsFFMain.Fields("EXGRATIA_AMOUNT").Value), "0.00")
            txtCompMonth.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("COMPENSATION_MON").Value), 0, RsFFMain.Fields("COMPENSATION_MON").Value), "0.00")
            txtCompAmount.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("COMPENSATION_AMOUNT").Value), 0, RsFFMain.Fields("COMPENSATION_AMOUNT").Value), "0.00")
            txtSuspension.Text = VB6.Format(IIf(IsDbNull(RsFFMain.Fields("SUSPENSIONPER").Value), 0, RsFFMain.Fields("SUSPENSIONPER").Value), "0.00")

            chkTransfer.CheckState = IIf(RsFFMain.Fields("IS_TRANSFER").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkSuspension.CheckState = IIf(RsFFMain.Fields("IS_SUSPENSION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)



            chkCalcPFonEL.CheckState = IIf(RsFFMain.Fields("PF_ON_EL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkCPL.CheckState = IIf(RsFFMain.Fields("ADD_CPL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked Then
                If Val(txtELAmount.Text) > 0 Then
                    mPaidDays = IIf(IsDbNull(RsCompany.Fields("LEAVEPAIDDAYS").Value), 0, RsCompany.Fields("LEAVEPAIDDAYS").Value)
                    If RsCompany.Fields("COMPANY_CODE").Value = 5 And CDate(txtDOL.Text) < CDate("01/01/2010") Then
                        lblBasicEL.Text = CStr(PaiseRound(CDbl(VB6.Format(txtELDays.Text, "0.00")) * Val(txtAtcBasic.Text) / mPaidDays, CDbl("0.50")))
                    Else
                        lblBasicEL.Text = CStr(Val(txtELAmount.Text))
                    End If
                End If
            Else
                lblBasicEL.Text = CStr(0)
            End If

            If CDate(txtDOL.Text) < CDate("01/03/2008") Then
                chkCalcPFonEL.Enabled = False
            End If

            chkAccountPosting.CheckState = IIf(RsFFMain.Fields("AC_POSTING").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAccountPosting.Enabled = IIf(RsFFMain.Fields("AC_POSTING").Value = "Y", False, True)


            ADDMode = False
            MODIFYMode = False
            cmdAccountPosting.Enabled = True
        End If

        FormatSprd(-1)
        '    txtBSalary.Enabled = True

        CalcEarn()
        CalcPFESI()
        txtDOL.Enabled = False
        txtPaidDays.Enabled = IIf(PubUserID = "G0416", True, False)

        '    Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))

        MainClass.UnProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColAmt)
        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColActualAmt, ColActualAmt)

        MainClass.UnProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColAmt)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColActualAmt, ColActualAmt)

        MainClass.ButtonStatus(Me, XRIGHT, RsFFMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Exit Sub
ERR1:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub ShowDetail1(ByRef pEmpCode As String)

        On Error GoTo ERR1
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim cntRow As Integer



        SqlStr = "SELECT * FROM PAY_FFSETTLE_DET " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFFDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsFFDetail.EOF = False Then
            With sprdEarn
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = 1
                    mTypeCode = Val(.Text)

                    RsFFDetail.MoveFirst()

                    Do While RsFFDetail.EOF = False
                        If mTypeCode = RsFFDetail.Fields("SALHEADCODE").Value Then
                            Exit Do
                        End If
                        RsFFDetail.MoveNext()
                    Loop

                    If RsFFDetail.EOF = False Then
                        .Col = ColPer
                        .Text = CStr(IIf(IsDbNull(RsFFDetail.Fields("PERCENTAGE").Value), "", RsFFDetail.Fields("PERCENTAGE").Value))

                        .Col = ColActualAmt
                        .Text = CStr(IIf(IsDbNull(RsFFDetail.Fields("ACTUALAMOUNT").Value), "", RsFFDetail.Fields("ACTUALAMOUNT").Value))

                        .Col = ColAmt
                        .Text = CStr(IIf(IsDbNull(RsFFDetail.Fields("PayableAmount").Value), "", RsFFDetail.Fields("PayableAmount").Value))
                    Else
                        .Col = ColPer
                        .Text = "0.00"

                        .Col = ColActualAmt
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

                    RsFFDetail.MoveFirst()

                    Do While RsFFDetail.EOF = False
                        If mTypeCode = RsFFDetail.Fields("SALHEADCODE").Value Then
                            Exit Do
                        End If
                        RsFFDetail.MoveNext()
                    Loop

                    If RsFFDetail.EOF = False Then
                        .Col = ColPer
                        .Text = CStr(IIf(IsDbNull(RsFFDetail.Fields("PERCENTAGE").Value), "", RsFFDetail.Fields("PERCENTAGE").Value))

                        .Col = ColActualAmt
                        .Text = CStr(IIf(IsDbNull(RsFFDetail.Fields("ACTUALAMOUNT").Value), "", RsFFDetail.Fields("ACTUALAMOUNT").Value))

                        .Col = ColAmt
                        .Text = CStr(IIf(IsDbNull(RsFFDetail.Fields("PayableAmount").Value), "", RsFFDetail.Fields("PayableAmount").Value))
                    Else
                        .Col = ColPer
                        .Text = "0.00"

                        .Col = ColActualAmt
                        .Text = "0.00"

                        .Col = ColAmt
                        .Text = "0.00"
                    End If
                Next
            End With
        End If
        Exit Sub
ERR1:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If PubUserID = "G0416" Or PubUserID = "000994" Then
        Else
            If Trim(txtEmpNo.Text) = "000840" Then
                Exit Sub
            End If
        End If

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        CalcAddDeduct()
        CalcPFESI()

        Call CheckPFRates(CDate(VB6.Format(txtDOL.Text, "dd/mm/yyyy")))
        Call CheckESIRates(CDate(VB6.Format(txtDOL.Text, "dd/mm/yyyy")))

        If Update1 = True Then
            TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mISACPosting As String
        Dim mISPFCalcOnEL As String
        Dim mIsTransfer As String
        Dim mIsSuspension As String
        Dim mADDCPL As String

        If Trim(txtEmpNo.Text) = "" Then
            Update1 = False
            Exit Function
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()



        mCode = txtEmpNo.Text
        mISACPosting = IIf(chkAccountPosting.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISPFCalcOnEL = IIf(chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mADDCPL = IIf(chkCPL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mIsTransfer = IIf(chkTransfer.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsSuspension = IIf(chkSuspension.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = ""

        If ADDMode = True Then
            SqlStr = " INSERT INTO PAY_FFSETTLE_HDR ( " & vbCrLf & " COMPANY_CODE, EMP_CODE, " & vbCrLf & " EMP_LEAVE_DATE,LEAVE_REASON, BASIC_SALARY, PAID_DAYS, " & vbCrLf & " PAID_BASICSALARY, GROSS_SALARY, DEDUCTION, " & vbCrLf & " OTHER_TOTAL, NET_SALARY, INC_HOUR_FORMON, " & vbCrLf & " INC_AMT_FORMON, INC_HOUR_PREMON, INC_AMT_PREMON, " & vbCrLf & " ARREAR_SAL, ARREAR_INC, LTC_MONTH, " & vbCrLf & " LTC_AMOUNT, BONUS_FORYEAR, BONUS_CURRYEAR, " & vbCrLf & " GRATUITY_MONTH, GRATUITY_AMOUNT, NOTICE_MONTH, " & vbCrLf & " NOTICE_AMOUNT, OTHERS_AMOUNT, BONUS_PER_FORYEAR , " & vbCrLf & " BONUS_PER_CURRYEAR, EL_DAYS, EL_AMOUNT, AC_POSTING, " & vbCrLf & " CHQ_NO, BANK_NAME, REMARKS, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE, PF_ON_EL,EXGRATIA_MON, " & vbCrLf & " EXGRATIA_AMOUNT, COMPENSATION_MON, COMPENSATION_AMOUNT, " & vbCrLf & " SUSPENSIONPER, IS_TRANSFER, IS_SUSPENSION,ADD_CPL,EMP_LTC_FROM,MANNUAL_BONUS) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mCode & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtReason.Text)) & "', " & Val(txtAtcBasic.Text) & ", " & Val(txtPaidDays.Text) & ", " & vbCrLf & " " & Val(txtBSalary.Text) & ", " & Val(txtGSalary.Text) & ", " & Val(txtDeduction.Text) & ", " & vbCrLf & " " & Val(txtTotOthers.Text) & ", " & Val(txtNetSalary.Text) & ", " & Val(txtIncHoursForMon.Text) & ", " & vbCrLf & " " & Val(txtIncAmtForMon.Text) & ", " & Val(txtIncHoursPreMon.Text) & ", " & Val(txtIncAmtPreMon.Text) & ", " & vbCrLf & " " & Val(txtSalArrear.Text) & ", " & Val(txtIncArrear.Text) & ", " & Val(txtLTCMonth.Text) & ", " & vbCrLf & " " & Val(txtLTCAmt.Text) & ", " & Val(txtBonusForYear.Text) & ", " & Val(txtBonusCurrYear.Text) & ", " & vbCrLf & " " & Val(txtGratuityMon.Text) & ", " & Val(txtGratuityAmt.Text) & ", " & Val(txtNoticeMon.Text) & ", " & vbCrLf & " " & Val(txtNoticeamt.Text) & ", " & Val(txtOthers.Text) & ", " & vbCrLf & " " & Val(txtBonusPerForYear.Text) & ", " & Val(txtBonusPerCurrYear.Text) & ", " & vbCrLf & " " & Val(txtELDays.Text) & ", " & Val(txtELAmount.Text) & ", '" & mISACPosting & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "', '" & MainClass.AllowSingleQuote((txtBankName.Text)) & "', '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '', '','" & mISPFCalcOnEL & "', " & Val(txtExGratiaMonth.Text) & "," & vbCrLf & " " & Val(txtExGratiaAmount.Text) & ", " & Val(txtCompMonth.Text) & ", " & Val(txtCompAmount.Text) & "," & vbCrLf & " " & Val(txtSuspension.Text) & ", '" & mIsTransfer & "','" & mIsSuspension & "','" & mADDCPL & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtLTCFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & IIf(chkMannualPerBonus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "')"


        Else
            SqlStr = "UPDATE  PAY_FFSETTLE_HDR SET " & vbCrLf & " EMP_LEAVE_DATE=TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_LTC_FROM=TO_DATE('" & VB6.Format(txtLTCFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " LEAVE_REASON='" & MainClass.AllowSingleQuote((txtReason.Text)) & "', " & vbCrLf & " BASIC_SALARY=" & Val(txtAtcBasic.Text) & ",  " & vbCrLf & " PAID_DAYS=" & Val(txtPaidDays.Text) & ", " & vbCrLf & " PAID_BASICSALARY=" & Val(txtBSalary.Text) & ",  " & vbCrLf & " GROSS_SALARY=" & Val(txtGSalary.Text) & ",  " & vbCrLf & " DEDUCTION=" & Val(txtDeduction.Text) & ", " & vbCrLf & " OTHER_TOTAL=" & Val(txtTotOthers.Text) & ",  " & vbCrLf & " NET_SALARY=" & Val(txtNetSalary.Text) & ",  " & vbCrLf & " INC_HOUR_FORMON=" & Val(txtIncHoursForMon.Text) & ", " & vbCrLf & " INC_AMT_FORMON=" & Val(txtIncAmtForMon.Text) & ",  " & vbCrLf & " INC_HOUR_PREMON=" & Val(txtIncHoursPreMon.Text) & ",  " & vbCrLf & " INC_AMT_PREMON=" & Val(txtIncAmtPreMon.Text) & ", " & vbCrLf & " ARREAR_SAL=" & Val(txtSalArrear.Text) & ",  " & vbCrLf & " ARREAR_INC=" & Val(txtIncArrear.Text) & ",  " & vbCrLf & " LTC_MONTH=" & Val(txtLTCMonth.Text) & ", " & vbCrLf & " LTC_AMOUNT=" & Val(txtLTCAmt.Text) & ",  " & vbCrLf & " BONUS_FORYEAR=" & Val(txtBonusForYear.Text) & ",  " & vbCrLf & " BONUS_CURRYEAR=" & Val(txtBonusCurrYear.Text) & ", " & vbCrLf & " GRATUITY_MONTH=" & Val(txtGratuityMon.Text) & ",  " & vbCrLf & " GRATUITY_AMOUNT=" & Val(txtGratuityAmt.Text) & ",  " & vbCrLf & " NOTICE_MONTH=" & Val(txtNoticeMon.Text) & ", ADD_CPL='" & mADDCPL & "'," & vbCrLf & " MANNUAL_BONUS='" & IIf(chkMannualPerBonus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "',"

            SqlStr = SqlStr & vbCrLf & " EXGRATIA_MON=" & Val(txtExGratiaMonth.Text) & "," & vbCrLf & " EXGRATIA_AMOUNT=" & Val(txtExGratiaAmount.Text) & ", " & vbCrLf & " COMPENSATION_MON=" & Val(txtCompMonth.Text) & "," & vbCrLf & " COMPENSATION_AMOUNT=" & Val(txtCompAmount.Text) & ",  " & vbCrLf & " SUSPENSIONPER=" & Val(txtSuspension.Text) & ",  " & vbCrLf & " IS_TRANSFER='" & mIsTransfer & "', " & vbCrLf & " IS_SUSPENSION='" & mIsSuspension & "', "

            SqlStr = SqlStr & vbCrLf & " CHQ_NO='" & MainClass.AllowSingleQuote(txtChqNo.Text) & "'," & vbCrLf & " BANK_NAME='" & MainClass.AllowSingleQuote(txtBankName.Text) & "', " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " NOTICE_AMOUNT=" & Val(txtNoticeamt.Text) & ",  " & vbCrLf & " OTHERS_AMOUNT=" & Val(txtOthers.Text) & ",  " & vbCrLf & " EL_DAYS=" & Val(txtELDays.Text) & ", " & vbCrLf & " EL_AMOUNT=" & Val(txtELAmount.Text) & ", " & vbCrLf & " AC_POSTING='" & mISACPosting & "'," & vbCrLf & " BONUS_PER_FORYEAR =" & Val(txtBonusPerForYear.Text) & ",  " & vbCrLf & " BONUS_PER_CURRYEAR=" & Val(txtBonusPerCurrYear.Text) & ",  " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " PF_ON_EL='" & mISPFCalcOnEL & "'"

            SqlStr = SqlStr & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mCode) = False Then GoTo UpdateError



        If UpdateEmpInfo(mCode) = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        RsFFMain.Requery()
        Update1 = True
        Exit Function
UpdateError:
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        Update1 = False
        PubDBCn.RollbackTrans()
        RsFFMain.Requery()
        RsFFDetail.Requery()
        PubDBCn.Errors.Clear()
        ''   Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdatePerksTrn(ByRef mCode As String, ByRef mSalDate As String, ByRef mWDays As Double) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsVar As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim mSalHeadCode As Integer
        Dim mAmount As Double

        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mSalHeadCode = RsTemp.Fields("ADD_DEDUCTCODE").Value
                mAmount = RsTemp.Fields("Amount").Value
                mAmount = CDbl(VB6.Format(mAmount * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))

                If mAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_PERKS_TRN ( " & vbCrLf & " COMPANY_CODE, SAL_DATE, " & vbCrLf & " EMP_CODE, ADD_DEDUCTCODE, AMOUNT,BOOKTYPE,DC,PAYMENT_TYPE,ADDUSER,ADDDATE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCode & "', " & mSalHeadCode & ", " & mAmount & ",'F'," & vbCrLf & " 'C', '','" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

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
    Private Function UpdatePerksArrearTrn(ByRef mCode As String, ByRef mSalDate As String) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsVar As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim mSalHeadCode As Integer
        Dim mAmount As Double
        Dim cntMonthFrom As String
        Dim cntMonthTo As String
        Dim mArrearMonth As Double
        Dim mAddDays As Double
        Dim mWDays As Double
        Dim mDiffAmount As Double
        Dim mTotWDays As Double
        Dim mEmpDOJ As String
        Dim mDOL As String
        Dim mLeaveWop As Double
        Dim mTotalMonth As Integer

        'UpdatePerksArrearTrn = True
        'Exit Function

        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT, EMP.EMP_DOJ,EMP.EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE AND SALARYDEF.COMPANY_CODE=EMP.COMPANY_CODE AND SALARYDEF.EMP_CODE=EMP.EMP_CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND TO_CHAR(SALARY_APP_DATE,'YYYYMM') = '" & VB6.Format(mSalDate, "YYYYMM") & "') "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mSalHeadCode = RsTemp.Fields("ADD_DEDUCTCODE").Value
                mWDays = 0
                mAmount = 0

                mDiffAmount = RsTemp.Fields("Amount").Value - RsTemp.Fields("PREVIOUS_AMOUNT").Value
                If mDiffAmount > 0 Then
                    cntMonthFrom = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY")))
                    cntMonthTo = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_APP_DATE").Value, "DD/MM/YYYY")))
                    mArrearMonth = (IIf(IsDbNull(RsTemp.Fields("TOT_ARR_MONTH").Value), 0, RsTemp.Fields("TOT_ARR_MONTH").Value))
                    mAddDays = (IIf(IsDbNull(RsTemp.Fields("ADDDAYS_IN").Value), 0, RsTemp.Fields("ADDDAYS_IN").Value))

                    If Val(CStr(mAddDays)) > 0 Then
                        cntMonthFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1 * mAddDays, CDate(cntMonthFrom)))
                    End If
                    mEmpDOJ = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                    mDOL = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")

                    Do While CDate(cntMonthFrom) < CDate(cntMonthTo)
                        mWDays = CalcAttn(mCode, mEmpDOJ, mDOL, cntMonthFrom, mLeaveWop)
                        mTotWDays = mTotWDays + mWDays
                        mAmount = mAmount + CDbl(VB6.Format(mDiffAmount * mWDays / MainClass.LastDay(Month(CDate(cntMonthFrom)), Year(CDate(cntMonthFrom))), "0.00"))

                        cntMonthFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(cntMonthFrom)))
                        mTotalMonth = mTotalMonth + 1

                    Loop

                    If mAmount <> 0 Then
                        SqlStr = " INSERT INTO PAY_PERKS_TRN ( " & vbCrLf & " COMPANY_CODE, SAL_DATE, " & vbCrLf & " EMP_CODE, ADD_DEDUCTCODE, AMOUNT,BOOKTYPE,DC,PAYMENT_TYPE,ADDUSER,ADDDATE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCode & "', " & mSalHeadCode & ", " & mAmount & ",'Z'," & vbCrLf & " 'C', '','" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                        PubDBCn.Execute(SqlStr)
                    End If
                End If
                RsTemp.MoveNext()
            Loop
        End If
NextRec:
        UpdatePerksArrearTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdatePerksArrearTrn = False
    End Function

    Private Function UpdateDetail1(ByRef pEmpCode As String) As Boolean

        On Error GoTo UpdateError

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSalHeadCode As String
        Dim mAmount As Double
        Dim mPerCent As Double

        Dim cntRow As Integer
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
        Dim mPayablePFELSalary As Double
        Dim mPayablePFArrearSalary As Double

        Dim mActualAmount As Double
        Dim xDOL As String

        Dim mDepartment As String
        Dim mCategory As String
        Dim mPaymentMode As String
        Dim mBankAcctNo As String
        Dim xSalDate As String
        Dim mEmpContOn As String
        Dim mTempPFCeiling As Double
        Dim mEmployer_PF As Double
        Dim mRound As Double
        Dim mArrearBasic As Double
        Dim mDOB As String
        Dim mAge As Double
        Dim mOPDate As String
        Dim mPrevPensionFund As Double
        Dim pPensionDiff As Double
        Dim mPensionConst As Double

        If Val(txtSalArrear.Text) <> 0 Then
            mArrearBasic = GetCurrentArrearPayable(pEmpCode, "Y")
        End If

        xDOL = VB6.Format(txtDOL.Text, "MMMYYYY")
        xSalDate = MainClass.LastDay(Month(CDate(txtDOL.Text)), Year(CDate(txtDOL.Text))) & "/" & VB6.Format(txtDOL.Text, "MM/YYYY")

        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_DOB", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOB = VB6.Format(IIf(IsDbNull(MasterNo), "", MasterNo), "DD/MM/YYYY")
        End If

        If mDOB = "" Then
            mAge = 18
        Else
            mAge = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOB), CDate(xSalDate)) / 12 'DateDiff("YYYY", mDOB, xSalDate)
        End If

        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_CONT", "PAY_SALARYDEF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpContOn = IIf(IsDbNull(MasterNo), "B", MasterNo)
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 16 And CDate(xSalDate) >= CDate("01/05/2014") Then
            mTempPFCeiling = CDbl(VB6.Format(mPFCeiling, "0.00"))
        Else
            mTempPFCeiling = CDbl(VB6.Format(mPFCeiling * Val(txtPaidDays.Text) / MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))), "0.00"))
        End If

        If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked Then
            mTempPFCeiling = mTempPFCeiling + CDbl(VB6.Format(mPFCeiling * Val(txtELDays.Text) / MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))), "0.00"))
        End If

        mTempPFCeiling = IIf(mTempPFCeiling < mPFCeiling, mTempPFCeiling, mPFCeiling)
        mTempPFCeiling = System.Math.Round(mTempPFCeiling, 0)

        SqlStr = " SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If MainClass.ValidateWithMasterTable(RsTemp.Fields("EMP_DEPT_CODE").Value, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDepartment = MasterNo
            Else
                mDepartment = "-1"
            End If

            mCategory = IIf(IsDbNull(RsTemp.Fields("EMP_CATG").Value), "-1", RsTemp.Fields("EMP_CATG").Value)
            mPaymentMode = IIf(IsDbNull(RsTemp.Fields("PAYMENTMODE").Value), "-1", RsTemp.Fields("PAYMENTMODE").Value)
            mBankAcctNo = IIf(IsDbNull(RsTemp.Fields("EMP_BANK_NO").Value), "", RsTemp.Fields("EMP_BANK_NO").Value)
        End If


        SqlStr = ""
        SqlStr = " DELETE FROM PAY_FFSETTLE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND ISARREAR='F'"

        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM PAY_PERKS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND BOOKTYPE IN ('F','Z')"

        PubDBCn.Execute(SqlStr)

        '& " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xDOL) & "'" & vbCrLf _
        '
        SqlStr = " DELETE FROM PAY_PFESI_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND ISARREAR='F'"

        PubDBCn.Execute(SqlStr)

        ''& " AND TO_CHAR(SAL_DATE,'MONYYYY')='" & UCase(xDOL) & "'" & vbCrLf _
        '
        SqlStr = ""

        mPayablePFSalary = Val(txtBSalary.Text)
        mPayablePFArrearSalary = Val(CStr(mArrearBasic))

        If RsCompany.Fields("COMPANY_CODE").Value = 5 And CDate(txtDOL.Text) < CDate("01/01/2010") Then
            mPayablePFELSalary = Val(lblBasicEL.Text)
        Else
            mPayablePFELSalary = Val(txtELAmount.Text)
        End If
        '    mPayablePFSalary = mPayablePFSalary + mPayablePFOtherSalary

        mPayableESISalary = Val(txtBSalary.Text)
        mPayableESISalary = mPayableESISalary + Val(txtIncAmtForMon.Text) + Val(txtIncAmtPreMon.Text) '' Not Applicable + Val(txtIncArrear.Text) + Val(txtSalArrear.Text)


        With sprdEarn
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColCode
                mSalHeadCode = Trim(.Text)

                .Col = ColPer
                mPerCent = Val(.Text)

                .Col = ColActualAmt
                mActualAmount = Val(.Text)

                .Col = ColAmt
                mAmount = Val(.Text)

                If MainClass.ValidateWithMasterTable(mSalHeadCode, "CODE", "INCLUDEDPF", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        mPayablePFSalary = mPayablePFSalary + Val(CStr(mAmount))
                    End If
                End If

                If mEmpContOn = "C" Then
                    mPayablePFSalary = IIf(mPayablePFSalary > mPFCeiling, mPFCeiling, mPayablePFSalary)
                End If

                If MainClass.ValidateWithMasterTable(mSalHeadCode, "CODE", "INCLUDEDESI", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        mPayableESISalary = mPayableESISalary + Val(CStr(mAmount))
                    End If
                End If


                'If mAmount + mActualAmount <> 0 Then
                SqlStr = " INSERT INTO PAY_FFSETTLE_DET (" & vbCrLf _
                    & " COMPANY_CODE, EMP_CODE, BASICSALARY, " & vbCrLf _
                    & " PAYABLESALARY, SAL_DATE, WDAYS, " & vbCrLf _
                    & " SALHEADCODE, PERCENTAGE, PAYABLEAMOUNT, " & vbCrLf _
                    & " ACTUALAMOUNT ) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & Trim(pEmpCode) & "', " & Val(txtAtcBasic.Text) & ", " & vbCrLf _
                    & " " & Val(txtBSalary.Text) & ", TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtPaidDays.Text) & ", " & vbCrLf _
                    & " " & mSalHeadCode & ", " & Val(CStr(mPerCent)) & ", " & mAmount & ", " & vbCrLf _
                    & " " & mActualAmount & " )"

                PubDBCn.Execute(SqlStr)


                SqlStr = " INSERT INTO PAY_SAL_TRN (" & vbCrLf _
                    & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf _
                    & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf _
                    & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf _
                    & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf _
                    & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC ) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                    & " '" & Trim(pEmpCode) & "',TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & Val(txtAtcBasic.Text) & ", " & Val(txtBSalary.Text) & ", " & vbCrLf _
                    & " " & Val(txtPaidDays.Text) & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf _
                    & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf _
                    & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf _
                    & " '" & mBankAcctNo & "','F','" & Trim(cbodesignation.Text) & "')"

                    PubDBCn.Execute(SqlStr)

                'End If
            Next
        End With

        With sprdDeduct
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColCode
                mSalHeadCode = Trim(.Text)

                .Col = ColPer
                mPerCent = Val(.Text)

                .Col = ColActualAmt
                mActualAmount = Val(.Text)

                .Col = ColAmt
                mAmount = Val(.Text)

                If MainClass.ValidateWithMasterTable(mSalHeadCode, "CODE", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSalHeadType = MasterNo
                End If

                If CDbl(mSalHeadType) = ConPF Then
                    mPFAmt = mAmount
                    If mPerCent = 0 Then
                        If mEmplerPFCont = "B" Then
                            mEmployer_PF = mAmount
                            mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                        Else
                            If mPayablePFSalary > mTempPFCeiling And mPayablePFSalary > 0 Then
                                mEmployer_PF = mAmount * mTempPFCeiling / mPayablePFSalary
                            Else
                                mEmployer_PF = mAmount
                            End If
                        End If
                    Else
                        If mEmplerPFCont = "B" Then
                            If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked Then
                                mEmployer_PF = (mPayablePFSalary + mPayablePFArrearSalary + mPayablePFELSalary) * mPerCent / 100
                            Else
                                mEmployer_PF = (mPayablePFSalary + mPayablePFArrearSalary) * mPerCent / 100
                            End If
                            mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                        Else
                            If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked Then
                                mEmployer_PF = IIf(mPayablePFSalary + mPayablePFELSalary + mPayablePFArrearSalary > mTempPFCeiling, mTempPFCeiling, mPayablePFSalary + mPayablePFELSalary + mPayablePFArrearSalary) * mPerCent / 100
                            Else
                                mEmployer_PF = IIf(mPayablePFSalary + mPayablePFArrearSalary > mTempPFCeiling, mTempPFCeiling, mPayablePFSalary + mPayablePFArrearSalary) * mPerCent / 100
                            End If
                            '                        mEmployer_PF = mEmployer_PF + IIf(mPayablePFELSalary > mPFCeiling, mPFCeiling, mPayablePFELSalary) * mPercent / 100
                            '                        mEmployer_PF = mEmployer_PF + IIf(mPayablePFArrearSalary > mPFCeiling, mPFCeiling, mPayablePFArrearSalary) * mPercent / 100
                            mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                        End If
                    End If

                    '                mPensionFund = mPayablePFELSalary * mPFPensionRate / 100
                    '                mPensionFund = mPensionFund + (mPayablePFArrearSalary * mPFPensionRate / 100)

                    If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If mPayablePFSalary + mPayablePFELSalary + mPayablePFArrearSalary <= mTempPFCeiling Then ''mPFCeiling
                            mPensionFund = mPensionFund + ((mPayablePFSalary + mPayablePFELSalary + mPayablePFArrearSalary) * mPFPensionRate / 100)
                            mRounding = CDbl("0.00")
                            If mAge > 58 Then
                                mPensionFund = 0
                            Else
                                mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                            End If
                            mEmpCont = mEmployer_PF - mPensionFund
                        Else
                            mTempPFCeiling = IIf(mTempPFCeiling = 0, mPFCeiling, mTempPFCeiling)
                            mPensionFund = (mTempPFCeiling * mPFPensionRate / 100)
                            mRounding = CDbl("0.00")
                            If mAge > 58 Then
                                mPensionFund = 0
                            Else
                                mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                            End If
                            mEmpCont = mEmployer_PF - mPensionFund
                        End If

                        mPayablePensionWages = IIf(mPFCeiling <= (mPayablePFSalary + mPayablePFArrearSalary + mPayablePFELSalary), mTempPFCeiling, (mPayablePFSalary + mPayablePFArrearSalary + mPayablePFELSalary))
                    Else
                        If mPayablePFSalary + mPayablePFArrearSalary <= mTempPFCeiling Then ''mPFCeiling
                            mPensionFund = mPensionFund + ((mPayablePFSalary + mPayablePFArrearSalary) * mPFPensionRate / 100)
                            mRounding = CDbl("0.00")
                            If mAge > 58 Then
                                mPensionFund = 0
                            Else
                                mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                            End If
                            mEmpCont = mEmployer_PF - mPensionFund
                        Else
                            mTempPFCeiling = IIf(mTempPFCeiling = 0, mPFCeiling, mTempPFCeiling)
                            mPensionFund = (mTempPFCeiling * mPFPensionRate / 100)
                            mRounding = CDbl("0.00")
                            If mAge > 58 Then
                                mPensionFund = 0
                            Else
                                mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                            End If
                            mEmpCont = mEmployer_PF - mPensionFund
                        End If

                        mPayablePensionWages = IIf(mTempPFCeiling <= (mPayablePFSalary + mPayablePFArrearSalary), mTempPFCeiling, (mPayablePFSalary + mPayablePFArrearSalary))
                    End If

                    mRound = CDbl(Replace(CStr(mPFRounding), "1", "0"))
                    mEmpCont = IIf(mEmpCont = 0, 0, VB6.Format(mEmpCont, CStr(mRound)))
                    If mAge > 58 Then
                        mPayablePensionWages = 0
                        mPensionFund = 0
                    Else
                        mPensionFund = IIf(mPensionFund = 0, 0, VB6.Format(mPensionFund, CStr(mRound)))
                        mPayablePensionWages = IIf(mPayablePensionWages = 0, 0, VB6.Format(mPayablePensionWages, CStr(mRound)))
                    End If
                    '                mPensionFund = IIf(mPFAmt < mPFCeiling, mPFAmt, mPFCeiling) * mPFPensionRate / 100
                    '                mRounding = "0.00"
                    '                mPensionFund = Format(mPensionFund, mRounding)
                    '                mEmpCont = mEmployer_PF - mPensionFund
                    '                mPFRounding = "0.00"

                    '                mPayablePensionWages = IIf(mPFCeiling <= mPayablePFSalary, mPFCeiling, mPayablePFSalary)
                    '                mPayablePensionWages = Format(mPayablePensionWages, "0")

                ElseIf CDbl(mSalHeadType) = ConESI Then
                    mESIAmt = mAmount
                    mRounding = CDbl("0.00")
                    mESIAmt = System.Math.Round(mESIAmt, 0) ''Format(mESIAmt, mRounding)
                End If

                If mAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_FFSETTLE_DET (" & vbCrLf & " COMPANY_CODE, EMP_CODE, BASICSALARY, " & vbCrLf & " PAYABLESALARY, SAL_DATE, WDAYS, " & vbCrLf & " SALHEADCODE, PERCENTAGE, PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & Trim(pEmpCode) & "', " & Val(txtAtcBasic.Text) & ", " & vbCrLf & " " & Val(txtBSalary.Text) & ", TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtPaidDays.Text) & ", " & vbCrLf & " " & mSalHeadCode & ", " & Val(CStr(mPerCent)) & ", " & mAmount & ", " & vbCrLf & " " & mActualAmount & " )"

                    PubDBCn.Execute(SqlStr)

                    SqlStr = " INSERT INTO PAY_SAL_TRN (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & Trim(pEmpCode) & "',TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtAtcBasic.Text) & ", " & Val(txtBSalary.Text) & ", " & vbCrLf & " " & Val(txtPaidDays.Text) & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','F','" & Trim(cbodesignation.Text) & "')"

                    PubDBCn.Execute(SqlStr)

                End If
            Next
        End With

        ''PF ESI TRN

        '    mPayablePFSalary = mPayablePFSalary + mPayablePFELSalary + mPayablePFArrearSalary

        If mEmplerPFCont = "B" Then
            If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked Then
                mPayablePFSalary = mPayablePFSalary + mPayablePFELSalary + mPayablePFArrearSalary
            Else
                mPayablePFSalary = mPayablePFSalary + mPayablePFArrearSalary
            End If
        Else
            If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Checked Then
                mPayablePFSalary = IIf(mPayablePFSalary + mPayablePFELSalary + mPayablePFArrearSalary > mTempPFCeiling, mTempPFCeiling, mPayablePFSalary + mPayablePFELSalary + mPayablePFArrearSalary)
            Else
                mPayablePFSalary = IIf(mPayablePFSalary + mPayablePFArrearSalary > mTempPFCeiling, mTempPFCeiling, mPayablePFSalary + mPayablePFArrearSalary)
            End If

            '        mPayablePFSalary = mPayablePFSalary + IIf(mPayablePFELSalary > mPFCeiling, mPFCeiling, mPayablePFELSalary)
            '        mPayablePFSalary = mPayablePFSalary + IIf(mPayablePFArrearSalary > mPFCeiling, mPFCeiling, mPayablePFArrearSalary)
        End If

        If mPFAmt + mESIAmt > 0 Then
            If mPFAmt = 0 Then
                mPayablePFSalary = 0
                mPayablePensionWages = 0
                mPensionFund = 0
                mEmpCont = 0
            Else
                mPrevPensionFund = GetPensionFund(pEmpCode, VB6.Format(txtDOL.Text, "DD-MMM-YYYY"))
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

            If mESIAmt = 0 Then
                mPayableESISalary = 0
            End If

            SqlStr = " INSERT INTO PAY_PFESI_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BasicSalary, PFABLEAMT, PENSIONWAGES, PFAMT, PFRate ,  " & vbCrLf & " ESIABLEAMT , ESIAMT, ESIRATE, PENSIONFUND, EPFAMT ,  " & vbCrLf & " LEAVEWOP , WDAYS, ISARREAR ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & Trim(pEmpCode) & "',TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtAtcBasic.Text) & ", " & mPayablePFSalary & "," & mPayablePensionWages & "," & mPFAmt & "," & mPFRate & ", " & vbCrLf & " " & mPayableESISalary & "," & mESIAmt & "," & mESIRate & ", " & vbCrLf & " " & mPensionFund & ", " & mEmpCont & ",0," & vbCrLf & " " & Val(txtPaidDays.Text) & ", " & vbCrLf & " 'F') "

            PubDBCn.Execute(SqlStr)
        End If


        mOPDate = GetOpeningPerksDate()
        If VB6.Format(mOPDate, "YYYYMM") <= VB6.Format(txtDOL.Text, "YYYYMM") Then
            If UpdatePerksTrn(pEmpCode, VB6.Format(txtDOL.Text, "DD/MM/YYYY"), Val(txtPaidDays.Text)) = False Then GoTo UpdateError
            If UpdatePerksArrearTrn(pEmpCode, VB6.Format(txtDOL.Text, "DD/MM/YYYY")) = False Then GoTo UpdateError
        End If

        UpdateDetail1 = True
        Exit Function
UpdateError:
        UpdateDetail1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
    End Function
    Private Function UpdateEmpInfo(ByRef pEmpCode As String) As Boolean
        On Error GoTo UpdateError

        SqlStr = ""
        SqlStr = " UPDATE PAY_EMPLOYEE_MST SET EMP_LEAVE_DATE=TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " EMP_LEAVE_REASON='" & MainClass.AllowSingleQuote((txtReason.Text)) & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE ATH_PASSWORD_MST SET STATUS='C'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND USER_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

        PubDBCn.Execute(SqlStr)

        UpdateEmpInfo = True
        Exit Function
UpdateError:
        UpdateEmpInfo = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
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
        Dim mDate As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAcPosting As String

        Dim mYM As Integer
        Dim mVNo As String
        Dim mVDate As String
        Dim mVType As String
        Dim mVSeqNo As Integer
        Dim mVNoSuffix As String
        Dim mBankCode As Integer
        Dim mBType As String
        Dim mBSType As String
        Dim mDivisionCode As Double
        Dim mWDays As Double
        Dim mStartDate As String

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

        If Trim(txtReason.Text) = "" Then
            MsgInformation("Leave Reason is empty. Cannot Save")
            txtReason.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsNumeric(txtBSalary.Text) Then
            MsgInformation("Invaild Basic Salary.")
            txtBSalary.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If chkAccountPosting.CheckState = System.Windows.Forms.CheckState.Checked Then
            If chkTransfer.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If Trim(txtChqNo.Text) = "" Then
                    MsgInformation("Please Enter the Full Final's Chq No, Cannot Save")
                    txtChqNo.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
                If Trim(txtBankName.Text) = "" Then
                    MsgInformation("Please Enter the Bank Name of Full Final's Chq No,Cannot Save")
                    txtBankName.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If


        '    If chkAccountPosting.Value = vbChecked Then
        '        If Trim(txtChqNo.Text) = "" Then
        '            MsgInformation "Cheque No is empty. Cannot Save"
        '            txtChqNo.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If

        If Val(txtPaidDays.Text) > 0 Then
            If CheckSalaryMade((txtEmpNo.Text), VB6.Format(txtDOL.Text, "DD/MM/YYYY")) = True Then
                MsgInformation("Salary Made Againt This Month. So cann't be add Paid days.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        mYM = CInt(VB6.Format(Year(CDate(txtDOL.Text)), "0000") & VB6.Format(Month(CDate(txtDOL.Text)), "00"))
        If CheckSalVoucherPost(mYM, mVNo, mVDate, mVType, mVSeqNo, mVNoSuffix, Val(txtEmpNo.Text), "F", mBSType, mDivisionCode) = True Then
            MsgInformation("F & F Posted in Accounts, so you cann't be reprocess Salary. VNo is (" & mVNo & ").")
            FieldsVarification = False
            Exit Function
        End If

        mStartDate = VB6.Format("01/" & VB6.Format(txtDOL.Text, "MM/YYYY"), "DD/MM/YYYY")


        If Val(txtPaidDays.Text) > 0 Then
            mWDays = CalcAttnPresent((txtEmpNo.Text), VB6.Format(mStartDate, "DD/MM/YYYY"), VB6.Format(txtDOL.Text, "DD/MM/YYYY"), VB6.Format(txtDOJ.Text, "DD/MM/YYYY"))
            If Val(txtPaidDays.Text) <> Val(CStr(mWDays)) Then
                MsgInformation("Paid Days is not equal to Present Date.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtPaidDays.Text) > Val(CStr(VB.Day(CDate(txtDOL.Text)))) Then
            MsgInformation("Paid Days Cann't be greater than Leave Day.")
            FieldsVarification = False
            Exit Function
        End If

        CalcEarn()
        CalcPFESI()
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))

        If Val(txtIncHoursForMon.Text) <> 0 Then
            mDate = VB6.Format(txtDOL.Text, "DD/MM/YYYY")
            If CheckOverTime((txtEmpNo.Text), mDate, Val(txtIncHoursForMon.Text)) = False Then
                '            MsgInformation "Current Month Over Time not equal to Actual Over Time. Cannot Save"
                txtIncHoursForMon.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtIncHoursPreMon.Text) <> 0 Then
            mDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(VB6.Format(txtDOL.Text, "DD/MM/YYYY"))))
            If CheckOverTime((txtEmpNo.Text), mDate, Val(txtIncHoursPreMon.Text)) = False Then
                MsgInformation("Previous Month Over Time not equal to Actual Over Time. Cannot Save")
                txtIncHoursPreMon.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True Then
            SqlStr = " SELECT AC_POSTING FROM PAY_FFSETTLE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(txtEmpNo.Text, "000000") & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
            If RsTemp.EOF = False Then
                mAcPosting = IIf(IsDbNull(RsTemp.Fields("AC_POSTING").Value), "N", RsTemp.Fields("AC_POSTING").Value)

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
            Exit Function
        End If
        If MODIFYMode = True And (RsFFMain.EOF = True Or RsFFMain.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Function CheckSalVoucherPost(ByRef mYM As Integer, ByRef mVNo As String, ByRef mVDate As String, ByRef mVType As String, ByRef mVSeqNo As Integer, ByRef mVNoSuffix As String, ByRef mBankCode As Integer, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mDivisionCode As Double, Optional ByRef mELYear As Integer = 0) As Boolean


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mKey As String

        CheckSalVoucherPost = False
        SqlStr = " SELECT * FROM FIN_SalVoucher_TRN  " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND BookType='" & mBookType & "'"

        SqlStr = SqlStr & vbCrLf & " AND BANKCODE=" & Val(CStr(mBankCode)) & " "

        '    SqlStr = SqlStr & vbCrLf & " AND BookSubType='" & mBookSubType & "'"

        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        '    If mBookType = "F" Or mBookType = "L" Then
        '        SqlStr = SqlStr & vbCrLf & " AND BANKCODE=" & Val(mBankCode) & " "
        '        If mELYear <> 0 And mBookType = "L" Then
        '            SqlStr = SqlStr & vbCrLf & " AND EL_YEAR=" & mELYear & ""
        '        End If
        '    ElseIf mBookType = "Q" Then
        '        SqlStr = SqlStr & vbCrLf & " AND BANKCODE=" & Val(mBankCode) & " AND YM=" & mYM & ""
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND YM=" & mYM & ""
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc)
        If RsMisc.EOF = False Then
            mKey = IIf(IsDbNull(RsMisc.Fields("mKey").Value), "", RsMisc.Fields("mKey").Value)
            mBankCode = RsMisc.Fields("BANKCODE").Value

            If mKey <> "" Then
                'FYEAR=" & RsCompany.Fields("FYEAR").Value & "
                SqlStr = " SELECT * FROM FIN_VOUCHER_HDR  " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & mKey & "'" & vbCrLf & " AND CANCELLED='N'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc)
                If RsMisc.EOF = False Then
                    mVType = IIf(IsDbNull(RsMisc.Fields("VTYPE").Value), "", RsMisc.Fields("VTYPE").Value)
                    mVSeqNo = RsMisc.Fields("VNOSEQ").Value
                    mVNoSuffix = IIf(IsDbNull(RsMisc.Fields("VNOSUFFIX").Value), "", RsMisc.Fields("VNOSUFFIX").Value)
                    mVNo = RsMisc.Fields("VNO").Value
                    mVDate = RsMisc.Fields("VDATE").Value
                    CheckSalVoucherPost = True
                End If
            End If
        Else
            CheckSalVoucherPost = False
        End If

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        CheckSalVoucherPost = False
    End Function


    Private Sub settextlength()

        On Error GoTo ERR1
        TxtName.Maxlength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)

        txtEmpNo.Maxlength = RsFFMain.Fields("EMP_CODE").DefinedSize
        txtBSalary.Maxlength = RsFFMain.Fields("BASIC_SALARY").Precision

        txtDOJ.Maxlength = 10
        txtDOL.Maxlength = 10

        txtAtcBasic.Maxlength = RsFFMain.Fields("BASIC_SALARY").Precision
        txtBSalary.Maxlength = RsFFMain.Fields("PAID_BASICSALARY").Precision
        txtPaidDays.Maxlength = RsFFMain.Fields("PAID_DAYS").Precision
        txtIncHoursForMon.Maxlength = RsFFMain.Fields("INC_HOUR_FORMON").Precision
        txtIncAmtForMon.Maxlength = RsFFMain.Fields("INC_AMT_FORMON").Precision
        txtIncHoursPreMon.Maxlength = RsFFMain.Fields("INC_HOUR_PREMON").Precision
        txtIncAmtPreMon.Maxlength = RsFFMain.Fields("INC_AMT_PREMON").Precision
        txtSalArrear.Maxlength = RsFFMain.Fields("ARREAR_SAL").Precision
        txtIncArrear.Maxlength = RsFFMain.Fields("ARREAR_INC").Precision

        txtLTCMonth.Maxlength = RsFFMain.Fields("LTC_MONTH").Precision
        txtLTCAmt.Maxlength = RsFFMain.Fields("LTC_AMOUNT").Precision
        txtBonusForYear.Maxlength = RsFFMain.Fields("BONUS_FORYEAR").Precision
        txtBonusCurrYear.Maxlength = RsFFMain.Fields("BONUS_CURRYEAR").Precision

        txtGratuityMon.Maxlength = RsFFMain.Fields("GRATUITY_MONTH").Precision
        txtGratuityAmt.Maxlength = RsFFMain.Fields("GRATUITY_AMOUNT").Precision
        txtNoticeMon.Maxlength = RsFFMain.Fields("NOTICE_MONTH").Precision
        txtNoticeamt.Maxlength = RsFFMain.Fields("NOTICE_AMOUNT").Precision
        txtOthers.Maxlength = RsFFMain.Fields("OTHERS_AMOUNT").Precision
        txtGSalary.Maxlength = RsFFMain.Fields("GROSS_SALARY").Precision
        txtDeduction.Maxlength = RsFFMain.Fields("DEDUCTION").Precision
        txtTotOthers.Maxlength = RsFFMain.Fields("OTHER_TOTAL").Precision
        txtNetSalary.Maxlength = RsFFMain.Fields("NET_SALARY").Precision

        txtExGratiaMonth.Maxlength = RsFFMain.Fields("EXGRATIA_MON").Precision
        txtExGratiaAmount.Maxlength = RsFFMain.Fields("EXGRATIA_AMOUNT").Precision
        txtCompMonth.Maxlength = RsFFMain.Fields("COMPENSATION_MON").Precision
        txtCompAmount.Maxlength = RsFFMain.Fields("COMPENSATION_AMOUNT").Precision
        txtSuspension.Maxlength = RsFFMain.Fields("SUSPENSIONPER").Precision

        txtBonusPerForYear.Maxlength = RsFFMain.Fields("BONUS_PER_FORYEAR").Precision
        txtBonusPerCurrYear.Maxlength = RsFFMain.Fields("BONUS_PER_CURRYEAR").Precision

        txtELDays.Maxlength = RsFFMain.Fields("EL_DAYS").Precision
        txtELAmount.Maxlength = RsFFMain.Fields("EL_AMOUNT").Precision
        txtReason.Maxlength = RsFFMain.Fields("LEAVE_REASON").DefinedSize

        txtChqNo.Maxlength = RsFFMain.Fields("CHQ_NO").DefinedSize
        txtBankName.Maxlength = RsFFMain.Fields("BANK_NAME").DefinedSize
        txtRemarks.Maxlength = RsFFMain.Fields("REMARKS").DefinedSize

        Exit Sub
ERR1:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        MainClass.ClearGrid(SprdView)
        SqlStr = " SELECT DISTINCT EMP.EMP_CODE, EMP.EMP_NAME AS NAME, IH.EMP_LEAVE_DATE,PAID_DAYS, IH.GROSS_SALARY," & vbCrLf _
            & " IH.NET_SALARY, CHQ_NO, BANK_NAME,DECODE(AC_POSTING,'Y','YES','NO') ACCT_POSTING " & vbCrLf _
            & " FROM PAY_FFSETTLE_HDR IH, PAY_EMPLOYEE_MST EMP" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE  " & vbCrLf _
            & " AND IH.EMP_CODE=EMP.EMP_CODE  "


        SqlStr = SqlStr & " ORDER BY IH.EMP_LEAVE_DATE, EMP.EMP_NAME"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 576 * 0)
            .set_ColWidth(1, 576 * 2)
            .set_ColWidth(2, 576 * 7)
            .set_ColWidth(3, 576 * 3)
            .set_ColWidth(4, 576 * 2)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean

        On Error GoTo DeleteErr

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Nothing to Delete.")
            Exit Function
        End If

        SqlStr = ""


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "Delete from PAY_FFSETTLE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpNo.Text) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_FFSETTLE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'" & vbCrLf & " AND ISARREAR='F'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_PERKS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'" & vbCrLf & " AND BOOKTYPE IN ('Z','F')"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_PFESI_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'" & vbCrLf & " AND ISARREAR='F'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE PAY_EMPLOYEE_MST SET EMP_LEAVE_DATE=''," & vbCrLf & " EMP_LEAVE_REASON='', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE ATH_PASSWORD_MST SET STATUS='O'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND USER_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsFFMain.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsFFMain.Requery()
        MsgBox(Err.Description)
    End Function
    Private Sub TxtEmpNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mEmpCode As String
        Dim mName As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mDesgName As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim RsEmpTrf As ADODB.Recordset
        Dim RsTempEmp As ADODB.Recordset

        Dim xSqlStr As String
        Dim mFromCompanyCode As Integer
        Dim mFromEmpCode As String
        'Dim mToCompanyCode As Long
        'Dim mToEmpCode As String

        If Trim(txtEmpNo.Text) = "" Then GoTo EventExitSub

        txtEmpNo.Text = VB6.Format(txtEmpNo.Text, "000000")
        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = ""

        If RS.EOF = False Then
            Clear1()
            txtEmpNo.Text = RS.Fields("EMP_CODE").Value
            TxtName.Text = IIf(IsDbNull(RS.Fields("EMP_NAME").Value), "", RS.Fields("EMP_NAME").Value)
            txtFName.Text = IIf(IsDbNull(RS.Fields("EMP_FNAME").Value), "", RS.Fields("EMP_FNAME").Value)

            mFromCompanyCode = IIf(IsDbNull(RS.Fields("COMPANY_CODE").Value), "", RS.Fields("COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDbNull(RS.Fields("EMP_CODE").Value), "", RS.Fields("EMP_CODE").Value)
            'SearchRow:   ''Comment on 08/03/2020
            '        xSqlStr = GetEmpTransferSQL(mFromEmpCode, mFromCompanyCode)
            '        MainClass.UOpenRecordSet xSqlStr, PubDBCn, adOpenStatic, RsEmpTrf, adLockOptimistic
            '
            '        If RsEmpTrf.EOF = False Then
            '            mFromCompanyCode = IIf(IsNull(RsEmpTrf!FROM_COMPANY_CODE), "", RsEmpTrf!FROM_COMPANY_CODE)
            '            mFromEmpCode = IIf(IsNull(RsEmpTrf!FROM_EMP_CODE), "", RsEmpTrf!FROM_EMP_CODE)
            '            GoTo SearchRow
            '        End If
            '
            '        xSqlStr = " SELECT EMP_GROUP_DOJ EMP_DOJ,EMP_LEAVE_DATE " & vbCrLf _
            ''            & " FROM PAY_EMPLOYEE_MST" & vbCrLf _
            ''            & " WHERE " & vbCrLf _
            ''            & " COMPANY_CODE = " & mFromCompanyCode & "" & vbCrLf _
            ''            & " AND EMP_CODE = '" & mFromEmpCode & "'"
            '
            '
            '        MainClass.UOpenRecordSet xSqlStr, PubDBCn, adOpenStatic, RsTempEmp, adLockOptimistic
            '
            '        If RsTempEmp.EOF = False Then
            '            txtDOJ.Text = Format(IIf(IsNull(RsTempEmp!EMP_DOJ), "", RsTempEmp!EMP_DOJ), "DD/MM/YYYY")
            '        End If


            txtDOJ.Text = VB6.Format(IIf(IsDbNull(RS.Fields("EMP_GROUP_DOJ").Value), "", RS.Fields("EMP_GROUP_DOJ").Value), "DD/MM/YYYY")
            txtDOL.Text = VB6.Format(IIf(IsDbNull(RS.Fields("EMP_LEAVE_DATE").Value), "", RS.Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")

            '        If MainClass.ValidateWithMasterTable(Trim(RS!EMP_DESG_CODE), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '            mDesgName = MasterNo
            '            cbodesignation.Text = mDesgName
            '        End If
            mEmpCode = RS.Fields("EMP_CODE").Value

            SqlStr = " SELECT GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mEmpCode & "',TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC FROM DUAL"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                cbodesignation.Text = IIf(IsDbNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)
            End If

            If MODIFYMode = True And RsFFMain.EOF = False Then xCode = RsFFMain.Fields("EMP_CODE").Value

            SqlStr = " SELECT * FROM PAY_FFSETTLE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFFMain, ADODB.LockTypeEnum.adLockReadOnly)

            If RsFFMain.EOF = False Then
                '            Clear1
                Call Show1()
            Else
                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such Month, Use add Button to New.", MsgBoxStyle.Information)
                    Cancel = True
                    GoTo EventExitSub
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PAY_FFSETTLE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFFMain, ADODB.LockTypeEnum.adLockReadOnly)
                    GoTo EventExitSub
                End If
                Call ShowAtcSalary((txtEmpNo.Text), (txtDOL.Text))
            End If
            If txtDOL.Enabled = True Then txtDOL.Focus()
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
        Dim mSqlStr As String
        Dim mSalDate As String

        MainClass.ClearGrid(sprdEarn, -1)
        MainClass.ClearGrid(sprdDeduct, -1)

        If Trim(txtDOL.Text) = "" Then
            mSalDate = VB6.Format(RunDate, "DD/MM/YYYY")
        Else
            mSalDate = VB6.Format(txtDOL.Text, "DD/MM/YYYY")
        End If

        mSqlStr = " SELECT " & vbCrLf & " COMPANY_CODE, CODE , " & vbCrLf & " NAME ,ADDDEDUCT,CALC_ON, " & vbCrLf & " TYPE ,PERCENTAGE, SEQ, " & vbCrLf & " ROUNDING ,INCLUDEDPF, INCLUDEDESI, " & vbCrLf & " INCLUDEDLEAVEENCASH,ACCOUNTCODEPOST, " & vbCrLf & " DC ,ISSALPART,STATUS , " & vbCrLf & " CLOSED_DATE , DEFAULT_AMT " & vbCrLf & " From PAY_SALARYHEAD_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & " OR CALC_ON=" & ConCalcVariable & ") " & vbCrLf & " AND TYPE <> " & ConOT & " "

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
            .set_RowHeight(mRow, ConRowHeight * 1.25)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 18)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 4)

            .Col = ColActualAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColActualAmt, 8)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 8)

        End With

        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColActualAmt, ColActualAmt)
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
            .set_ColWidth(ColDesc, 18)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 4)

            .Col = ColActualAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColActualAmt, 8)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 8)

        End With

        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColActualAmt, ColActualAmt)
        MainClass.SetSpreadColor(sprdDeduct, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub CalcGrossSalary(ByRef mIsReset As String)

        Dim mSalary As Double
        Dim mEarn As Double
        Dim mDeduct As Double
        Dim mOthers As Double
        Dim cntRow As Integer
        Dim ConCurrWorkDay As Double
        Dim ConPrevWorkDay As Double
        Dim mOTRate As Double
        Dim mPrevMonth As String
        Dim mActualEarn As Double
        Dim mActualGross As Double
        Dim mCTC As Double
        Dim mWDays As Double
        Dim mTotalLeavesBal As Double
        Dim pBalEL As Double
        Dim pBalCL As Double
        Dim pBalSL As Double
        Dim pBalCPL As Double
        Dim mLastDay As Double
        Dim mPaidDays As Double
        Dim pDesgCode As String
        Dim mCat As String
        Dim mLTCAmount As Double
        Dim mActualBasic As Double
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCurrCompanyDOJ As String
        Dim mIsLeavePayable As String
        Dim mISBonusPayable As String
        Dim mEmpCat As String
        Dim mServiceMonth As Double
        Dim mServiceYear As Integer
        Dim mOTFactor As Double

        If Trim(txtEmpNo.Text) = "" Then Exit Sub
        mSqlStr = "SELECT  GETBasicSalaryFROMMST(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & Trim(txtEmpNo.Text) & "',TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + GETBasicPartFROMMST(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & Trim(txtEmpNo.Text) & "',TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS B_SALARY FROM DUAL"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mActualBasic = IIf(IsDbNull(RsTemp.Fields("B_SALARY").Value), 0, RsTemp.Fields("B_SALARY").Value)
        Else
            mActualBasic = Val(txtAtcBasic.Text)
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtEmpNo.Text), "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCurrCompanyDOJ = MasterNo
        End If


        If Trim(mCurrCompanyDOJ) = "" Then MsgBox("Employee D.O.J. Not Defined") : Exit Sub
        If Trim(txtDOL.Text) = "" Then MsgBox("Employee D.O.L. Not Defined") : Exit Sub

        mLastDay = MainClass.LastDay(Month(CDate(txtDOL.Text)), Year(CDate(txtDOL.Text)))

        mPrevMonth = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(txtDOL.Text)))
        ConPrevWorkDay = MainClass.LastDay(Month(CDate(mPrevMonth)), Year(CDate(mPrevMonth)))
        ConCurrWorkDay = MainClass.LastDay(Month(CDate(txtDOL.Text)), Year(CDate(txtDOL.Text)))

        mSalary = Val(txtBSalary.Text)

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColActualAmt
                mActualEarn = mActualEarn + Val(.Text)

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

        mActualGross = CDbl(VB6.Format(Val(txtAtcBasic.Text) + Val(CStr(mActualEarn)), "0.00"))
        txtGSalary.Text = VB6.Format(Val(CStr(mSalary)) + Val(CStr(mEarn)), "0.00")
        txtDeduction.Text = VB6.Format(Val(CStr(mDeduct)), "0.00")

        mOTFactor = 0
        If MainClass.ValidateWithMasterTable(txtEmpNo.Text, "EMP_CODE", "EMP_OT_RATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mOTFactor = MasterNo
        End If

        If Val(txtIncHoursForMon.Text) <> 0 Then
            mOTRate = CDbl(VB6.Format(Val(CStr(mActualGross)) / (ConCurrWorkDay * 8), "0.00")) * mOTFactor
            txtIncAmtForMon.Text = CStr(System.Math.Round(Val(txtIncHoursForMon.Text) * mOTRate, 0))
        Else
            txtIncAmtForMon.Text = "0.00"
        End If

        If Val(txtIncHoursPreMon.Text) <> 0 Then
            mOTRate = CDbl(VB6.Format(Val(CStr(mActualGross)) / (ConPrevWorkDay * 8), "0.00")) * mOTFactor
            txtIncAmtPreMon.Text = CStr(System.Math.Round(Val(txtIncHoursPreMon.Text) * mOTRate, 0))
        Else
            txtIncAmtPreMon.Text = "0.00"
        End If

        pDesgCode = ""

        If mIsReset = "Y" Then
            txtSalArrear.Text = CStr(GetCurrentArrearPayable(Trim(txtEmpNo.Text), "N"))
            If chkTransfer.CheckState = System.Windows.Forms.CheckState.Checked Then
                mLTCAmount = 0
            Else
                mLTCAmount = System.Math.Round(GetLTCAmount(Trim(txtEmpNo.Text), Val(txtLTCMonth.Text), pDesgCode), 0)
            End If

            '        If DateDiff("m", mCurrCompanyDOJ, txtDOL.Text) < Val(txtLTCMonth.Text) Then
            '            mLTCAmount = mLTCAmount + Round(GetLastUnitLTCAmount(Trim(txtEmpNo.Text), Val(txtLTCMonth.Text), pDesgCode), 0)
            '        End If
            txtLTCAmt.Text = CStr(mLTCAmount)


            If MainClass.ValidateWithMasterTable(Trim(txtEmpNo.Text), "EMP_CODE", "IS_BONUS_PAYABLE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mISBonusPayable = Trim(MasterNo)
            Else
                mISBonusPayable = "N"
            End If

            If mISBonusPayable = "Y" Then
                If chkMannualPerBonus.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    If chkBonusPaid.CheckState = System.Windows.Forms.CheckState.Checked Then
                        txtBonusForYear.Text = CStr(GetBonusAmount(Trim(txtEmpNo.Text), Val(txtBonusPerForYear.Text), "N"))
                    Else
                        txtBonusForYear.Text = "0.00"
                    End If
                End If

                txtBonusCurrYear.Text = CStr(GetBonusAmount(Trim(txtEmpNo.Text), Val(txtBonusPerCurrYear.Text), "Y"))
            End If

            mPaidDays = IIf(IsDbNull(RsCompany.Fields("LEAVEPAIDDAYS").Value), 0, RsCompany.Fields("LEAVEPAIDDAYS").Value)

            If MainClass.ValidateWithMasterTable(pDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCat = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(Trim(txtEmpNo.Text), "EMP_CODE", "IS_LEAVE_ENCHASE_PAYABLE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mIsLeavePayable = Trim(MasterNo)
            Else
                mIsLeavePayable = "N"
            End If

            If MainClass.ValidateWithMasterTable(Trim(txtEmpNo.Text), "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpCat = Trim(MasterNo)
            End If


            '        If mCat = "D"  Then
            If mIsLeavePayable = "N" Then
                txtELDays.Text = "0.00"
                txtELAmount.Text = "0.00"
                lblBasicEL.Text = "0.00"
            Else
                mWDays = CalcWDays(Trim(txtEmpNo.Text), VB6.Format(txtDOL.Text, "DD/MM/YYYY"))
                mTotalLeavesBal = CalcBalLeaves(Trim(txtEmpNo.Text), VB6.Format(txtDOL.Text, "DD/MM/YYYY"), PubDBCn, pBalEL, pBalCL, pBalSL, pBalCPL)

                If chkCPL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    mTotalLeavesBal = CDbl(VB6.Format(mTotalLeavesBal, "0.00"))
                    txtELDays.Text = VB6.Format(mTotalLeavesBal, "0.00")
                Else
                    mTotalLeavesBal = CDbl(VB6.Format(mTotalLeavesBal + pBalCPL, "0.00"))
                    txtELDays.Text = VB6.Format(mTotalLeavesBal, "0.00")
                End If

                If RsCompany.Fields("COMPANY_CODE").Value = 27 Or RsCompany.Fields("COMPANY_CODE").Value = 29 Then
                    mTotalLeavesBal = pBalEL
                    txtELDays.Text = VB6.Format(mTotalLeavesBal, "0.00")
                End If

                If chkTransfer.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtELAmount.Text = "0.00"
                Else
                    If Val(txtELDays.Text) < 0 Then
                        If RsCompany.Fields("COMPANY_CODE").Value = 27 Or RsCompany.Fields("COMPANY_CODE").Value = 29 Then
                            txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * Val(CStr(mActualBasic)) / mPaidDays, CDbl("0.50")))
                        Else
                            mCTC = mActualGross
                            mCTC = mCTC + CalcPerksAllowance(Trim(txtEmpNo.Text), (txtDOL.Text), ConPerks)
                            txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * mCTC / 30, CDbl("0.50")))
                        End If
                    Else
                        If RsCompany.Fields("COMPANY_CODE").Value = 5 Then
                            If CDate(txtDOL.Text) >= CDate("01/01/2010") Then ''23-02-2010 ''Mrs. Shefali Telephonic..
                                txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * mActualGross / mPaidDays, CDbl("0.50")))
                            ElseIf CDate(txtDOL.Text) >= CDate("30/04/2008") And CDate(txtDOL.Text) < CDate("01/01/2010") Then
                                txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * Val(CStr(mActualBasic)) / mPaidDays, CDbl("0.50")))
                            Else
                                txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * mActualGross / mPaidDays, CDbl("0.50")))
                            End If
                        Else
                            If RsCompany.Fields("COMPANY_CODE").Value = 16 And RsCompany.Fields("FYEAR").Value >= 2015 Then
                                txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * Val(CStr(mActualBasic)) / mPaidDays, CDbl("0.50")))
                            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 27 Or RsCompany.Fields("COMPANY_CODE").Value = 29 Then
                                txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * Val(CStr(mActualBasic)) / mPaidDays, CDbl("0.50")))
                            ElseIf CDate(txtDOL.Text) >= CDate("13/09/2009") Then
                                If RsCompany.Fields("COMPANY_CODE").Value = 12 And mEmpCat = "R" Then
                                    txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * Val(CStr(mActualBasic)) / mPaidDays, CDbl("0.50")))
                                Else
                                    txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * mActualGross / mPaidDays, CDbl("0.50")))
                                End If
                            ElseIf CDate(txtDOL.Text) >= CDate("30/04/2008") And CDate(txtDOL.Text) < CDate("13/09/2009") Then
                                txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * Val(CStr(mActualBasic)) / mPaidDays, CDbl("0.50")))
                            Else
                                txtELAmount.Text = CStr(PaiseRound(mTotalLeavesBal * mActualGross / mPaidDays, CDbl("0.50")))
                            End If
                        End If
                    End If
                    If chkCalcPFonEL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        lblBasicEL.Text = CStr(0)
                    Else
                        If RsCompany.Fields("COMPANY_CODE").Value = 5 And CDate(txtDOL.Text) < CDate("01/01/2010") Then
                            lblBasicEL.Text = CStr(PaiseRound(mTotalLeavesBal * Val(CStr(mActualBasic)) / 26, CDbl("0.50")))
                        Else
                            lblBasicEL.Text = txtELAmount.Text
                        End If
                    End If
                End If
            End If
        End If

        If Val(txtNoticeMon.Text) = 0 Then
            txtNoticeamt.Text = VB6.Format(txtNoticeamt.Text, "0.00")
        Else
            If RsCompany.Fields("COMPANY_CODE").Value = 16 And RsCompany.Fields("FYEAR").Value >= 2015 Then
                txtNoticeamt.Text = CStr(System.Math.Round(Val(txtNoticeMon.Text) * Val(CStr(mActualBasic)) / 30))
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 27 Or RsCompany.Fields("COMPANY_CODE").Value = 29 Then
                txtNoticeamt.Text = CStr(System.Math.Round(Val(txtNoticeMon.Text) * Val(CStr(mActualBasic)) / 30))
            Else
                txtNoticeamt.Text = CStr(System.Math.Round(Val(txtNoticeMon.Text) * Val(CStr(mActualGross)) / 30))
            End If
        End If

        If mIsReset = "Y" Then
            '        If Round(DateDiff("d", txtDOJ.Text, txtDOL.Text) / 365, 0) >= 5 Then

            If InStr(1, Trim(UCase(txtReason.Text)), "DEATH") > 0 Then
                '            txtGratuityMon.Text = Round(DateDiff("d", txtDOJ.Text, txtDOL.Text) / 365, 0) * 15
                '            txtGratuityAmt.Text = Round(Val(mActualBasic) * Val(txtGratuityMon.Text) / mPaidDays, 0)
                mServiceMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDOJ.Text), DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(txtDOL.Text))) '' DateDiff("d", txtDOJ.Text, txtDOL.Text) / 365
                mServiceYear = Int(mServiceMonth / 12) + IIf(mServiceMonth Mod 12 < 6, 0, 1) '' Int(mServiceMonth) + IIf((mServiceMonth - Int(mServiceMonth)) * 12 < 6, 0, 1)
                txtGratuityMon.Text = CStr(mServiceYear * 15)
                txtGratuityAmt.Text = CStr(System.Math.Round(Val(CStr(mActualBasic)) * Val(txtGratuityMon.Text) / mPaidDays, 0))
            Else
                If DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDOJ.Text), DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(txtDOL.Text))) >= IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, 60, IIf(CDate(txtDOL.Text) >= CDate("18/12/2015"), 60, 56)) Then
                    '                txtGratuityMon.Text = Round(DateDiff("d", txtDOJ.Text, txtDOL.Text) / 365, 0) * 15
                    '                txtGratuityAmt.Text = Round(Val(mActualBasic) * Val(txtGratuityMon.Text) / mPaidDays, 0)
                    mServiceMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDOJ.Text), DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(txtDOL.Text))) '' DateDiff("d", txtDOJ.Text, txtDOL.Text) / 365
                    mServiceYear = Int(mServiceMonth / 12) + IIf(mServiceMonth Mod 12 < 6, 0, 1) ''Int(mServiceMonth) + IIf((mServiceMonth - Int(mServiceMonth)) * 12 < 6, 0, 1)
                    txtGratuityMon.Text = CStr(mServiceYear * 15)
                    If chkTransfer.CheckState = System.Windows.Forms.CheckState.Checked Then
                        txtGratuityAmt.Text = "0.00"
                    Else
                        txtGratuityAmt.Text = CStr(System.Math.Round(Val(CStr(mActualBasic)) * Val(txtGratuityMon.Text) / mPaidDays, 0))
                    End If
                Else
                    txtGratuityMon.Text = "0.00"
                    txtGratuityAmt.Text = "0.00"
                End If
            End If
        End If

        CalcPFESI()

        mOthers = Val(txtIncAmtForMon.Text) + Val(txtIncAmtPreMon.Text) + Val(txtSalArrear.Text)
        mOthers = mOthers + Val(txtIncArrear.Text) + Val(txtLTCAmt.Text) + Val(txtELAmount.Text)
        mOthers = mOthers + Val(txtBonusForYear.Text) + Val(txtBonusCurrYear.Text)
        mOthers = mOthers + Val(txtGratuityAmt.Text) + Val(txtNoticeamt.Text) + Val(txtOthers.Text)

        mOthers = mOthers + Val(txtExGratiaAmount.Text) + Val(txtCompAmount.Text)

        lblActGross.Text = VB6.Format(Val(CStr(mActualGross)), "0.00")




        cntRow = 1
        mDeduct = 0
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mDeduct = mDeduct + Val(.Text)
            Next
        End With
        txtDeduction.Text = VB6.Format(Val(CStr(mDeduct)), "0.00")

        ''mLastDay)
        txtTotOthers.Text = VB6.Format(Val(CStr(mOthers)), "0.00")
        txtNetSalary.Text = CStr(System.Math.Round(Val(CStr(mSalary)) + Val(CStr(mEarn)) + Val(CStr(mOthers)) - Val(CStr(mDeduct)), 0))
        txtNetSalary.Text = VB6.Format(txtNetSalary.Text, "0.00")
    End Sub
    Private Function CalcPerksAllowance(ByRef mCode As String, ByRef pWEFDate As String, ByRef pADDDeduct As Integer) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf & " WHERE A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf & " AND A.EMP_CODE = '" & mCode & "'"

        '' AND B.NAME='" & MainClass.AllowSingleQuote(mSalHeadName) & "'
        SqlStr = SqlStr & vbCrLf & " AND B.ADDDEDUCT=" & ConPerks & " AND B.ISSALPART='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            CalcPerksAllowance = IIf(IsDbNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value)
        Else
            CalcPerksAllowance = 0
        End If

        CalcPerksAllowance = System.Math.Round(CalcPerksAllowance, 0)

        Exit Function
ErrGetLTAAmount:
        CalcPerksAllowance = 0
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


        mDOJ = VB6.Format(txtDOJ.Text, "DD/MM/YYYY")
        mDOL = VB6.Format(txtDOL.Text, "DD/MM/YYYY")

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
                    If RsBalEL.Fields("FIRSTHALF").Value = CPLEARN Or RsBalEL.Fields("FIRSTHALF").Value = CPLAVAIL Then

                    ElseIf RsBalEL.Fields("FIRSTHALF").Value = SUNDAY Or RsBalEL.Fields("FIRSTHALF").Value = HOLIDAY Then
                        mTotalHoliDays = mTotalHoliDays + 0.5
                    Else
                        mFHalf = mFHalf + 0.5
                    End If
                End If

                If RsBalEL.Fields("SECONDHALF").Value <> -1 Then
                    If RsBalEL.Fields("SECONDHALF").Value = CPLEARN Or RsBalEL.Fields("SECONDHALF").Value = CPLAVAIL Then

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

        '    SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf _
        ''            & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND HOLIDAY_DATE>='" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "' AND HOLIDAY_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "' "
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsBalEL, adLockOptimistic
        '
        '    If RsBalEL.EOF = False Then
        '        mTotalHoliDays = IIf(IsNull(RsBalEL!HOLIDAYCNT), 0, RsBalEL!HOLIDAYCNT)
        '    End If

        CalcWDays = mTotalRunningDays - mTotalLeaves - mTotalHoliDays

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetBonusAmount(ByRef mCode As String, ByRef mBonusPer As Double, ByRef mIsCurrentYear As String) As Double
        On Error GoTo ErrCalcBonus
        Dim mBonusAmount As Double
        Dim RsSal As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFromDate As String
        Dim mToDate As String
        Dim mArrearAmount As Double

        Dim RsEmpTemp As ADODB.Recordset
        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mMonthPayableBonus As Double
        Dim CntMonth As Integer

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            mBonusPer = 1
        ElseIf mBonusPer = 0 Then
            GetBonusAmount = 0
            Exit Function
        End If

        If mIsCurrentYear = "N" Then
            mFromDate = "01/04/" & Year(RsCompany.Fields("START_DATE").Value) - 1
            mToDate = "31/03/" & Year(RsCompany.Fields("START_DATE").Value)
        Else
            mFromDate = "01/04/" & Year(RsCompany.Fields("START_DATE").Value)
            mToDate = VB6.Format(txtDOL.Text, "DD/MM/YYYY")
        End If

        mBonusAmount = GetEmpBonusAmount(mCode, mFromDate, mToDate, VB6.Format(txtDOJ.Text, "DD/MM/YYYY"), VB6.Format(txtDOL.Text, "DD/MM/YYYY"))

        If chkSuspension.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If mIsCurrentYear = "Y" And RsCompany.Fields("BONUS_TYPE").Value = "B" Then
                mMonthPayableBonus = GetCurrentMonthPayableBonus
                mBonusAmount = mBonusAmount + (Val(CStr(mMonthPayableBonus)) * mBonusPer / 100)
            End If

            If mIsCurrentYear = "Y" Then
                mArrearAmount = GetCurrentArrearPayable(mCode, "Y")
                mBonusAmount = mBonusAmount + (mArrearAmount * mBonusPer / 100)
            End If
        End If

        GetBonusAmount = System.Math.Round(mBonusAmount, 0)

        Exit Function
ErrCalcBonus:
        GetBonusAmount = 0
    End Function

    Private Function GetCurrentMonthPayableBonus() As Double
        On Error GoTo ErrCalcBonus
        Dim cntRow As Integer
        Dim mSalCode As Double

        GetCurrentMonthPayableBonus = Val(txtBSalary.Text)

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mSalCode = Val(.Text)

                If MainClass.ValidateWithMasterTable(mSalCode, "CODE", "CODE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSALPART='Y'") = True Then
                    .Col = ColAmt
                    GetCurrentMonthPayableBonus = GetCurrentMonthPayableBonus + Val(.Text)
                End If
            Next
        End With

        Exit Function
ErrCalcBonus:
        GetCurrentMonthPayableBonus = 0
    End Function
    Private Function GetCurrentArrearBasic(ByRef mCode As String, ByRef pSalDate As String) As Double

        On Error GoTo ErrCalcBonus

        Dim RsSal As ADODB.Recordset


        GetCurrentArrearBasic = 0

        SqlStr = "SELECT DISTINCT PAYABLESALARY, ISARREAR FROM PAY_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE = '" & mCode & "'"

        If pSalDate = "" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM') = '" & VB6.Format(txtDOL.Text, "YYYYMM") & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM') = '" & VB6.Format(pSalDate, "YYYYMM") & "'"
        End If
        ''
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSal.EOF = False Then
            Do While RsSal.EOF = False
                If RsSal.Fields("IsArrear").Value = "N" Or RsSal.Fields("IsArrear").Value = "V" Then
                    GetCurrentArrearBasic = GetCurrentArrearBasic + IIf(IsDbNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value)
                End If
                RsSal.MoveNext()
            Loop
        End If
        Exit Function
        '    If pSalDate <> "" Then
        '           SqlStr = " SELECT DISTINCT SALARY_APP_DATE, BASICSALARY ," & vbCrLf _
        ''                & " PREVIOUS_BASICSALARY,TOT_ARR_MONTH,ARREAR_DATE " & vbCrLf _
        ''                & " FROM PAY_SALARYDEF_MST" & vbCrLf _
        ''                & " WHERE Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
        ''                & " AND TO_CHAR(ARREAR_DATE,'YYYYMM') = '" & VB6.Format(txtDOL.Text, "YYYYMM") & "'" & vbCrLf _
        ''                & " AND IS_ARREAR='Y'"
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsSal, adLockOptimistic
        '
        '        If RsSal.EOF = False Then
        '            GetCurrentArrearBasic = ((IIf(IsNull(RsSal!BASICSALARY), 0, RsSal!BASICSALARY) - IIf(IsNull(RsSal!PREVIOUS_BASICSALARY), 0, RsSal!PREVIOUS_BASICSALARY)) * IIf(IsNull(RsSal!TOT_ARR_MONTH), 0, RsSal!TOT_ARR_MONTH))
        '        End If
        '    End If

        Exit Function
ErrCalcBonus:
        GetCurrentArrearBasic = 0
    End Function

    Private Function GetWorkingDays(ByRef mCode As String, ByRef pSalDate As String, ByRef mType As String) As Double

        On Error GoTo ErrCalcBonus

        Dim RsSal As ADODB.Recordset
        'Dim mMonthDay As Long


        GetWorkingDays = 0

        If pSalDate = "" Then
            GetWorkingDays = VB.Day(CDate(VB6.Format(txtDOL.Text, "DD/MM/YYYY")))
        Else
            If mType = "S" Then
                GetWorkingDays = MainClass.LastDay(Month(CDate(pSalDate)), Year(CDate(pSalDate)))
            Else
                GetWorkingDays = VB.Day(CDate(VB6.Format(pSalDate, "DD/MM/YYYY")))
            End If
        End If

        '    If mFrom = "S" Then
        '        SqlStr = "SELECT DISTINCT WDAYS, ISARREAR FROM PAY_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE = '" & mCode & "'"
        '
        '        If pSalDate = "" Then
        '            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM') = '" & VB6.Format(txtDOL.Text, "YYYYMM") & "'"
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM') = '" & VB6.Format(pSalDate, "YYYYMM") & "'"
        '        End If
        '        ''
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsSal, adLockOptimistic
        '        If RsSal.EOF = False Then
        '            Do While RsSal.EOF = False
        '                GetWorkingDays = GetWorkingDays + IIf(IsNull(RsSal!WDAYS), 0, RsSal!WDAYS)
        '                RsSal.MoveNext
        '            Loop
        '            Exit Function
        '        End If
        '    Else
        '
        '    End If

        SqlStr = " SELECT COUNT(FIRSTHALF) AS WLEAVE" & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'" & vbCrLf
        If mType = "A" Then
            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(pSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(ATTN_DATE,'YYYYMM') = '" & VB6.Format(pSalDate, "YYYYMM") & "'"
        Else
            If pSalDate = "" Then
                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(ATTN_DATE,'YYYYMM') = '" & VB6.Format(txtDOL.Text, "YYYYMM") & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(ATTN_DATE,'YYYYMM') = '" & VB6.Format(pSalDate, "YYYYMM") & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND FIRSTHALF IN (" & WOPAY & "," & ABSENT & ")"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSal.EOF = False Then
            GetWorkingDays = GetWorkingDays - IIf(IsDbNull(RsSal.Fields("WLEAVE").Value), 0, RsSal.Fields("WLEAVE").Value)
        End If

        SqlStr = " SELECT COUNT(SECONDHALF) AS WLEAVE" & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & mCode & "'" & vbCrLf
        If mType = "A" Then
            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(pSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(ATTN_DATE,'YYYYMM') = '" & VB6.Format(pSalDate, "YYYYMM") & "'"
        Else
            If pSalDate = "" Then
                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(ATTN_DATE,'YYYYMM') = '" & VB6.Format(txtDOL.Text, "YYYYMM") & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(ATTN_DATE,'YYYYMM') = '" & VB6.Format(pSalDate, "YYYYMM") & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND SECONDHALF IN (" & WOPAY & "," & ABSENT & ")"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSal.EOF = False Then
            GetWorkingDays = GetWorkingDays - IIf(IsDbNull(RsSal.Fields("WLEAVE").Value), 0, RsSal.Fields("WLEAVE").Value)
        End If

        Exit Function
ErrCalcBonus:
        GetWorkingDays = 0
    End Function


    Private Function GetCurrentAmount(ByRef mCode As String, ByRef pSalDate As String, ByRef pSalCode As Double) As Double

        On Error GoTo ErrCalcBonus

        Dim RsSal As ADODB.Recordset


        GetCurrentAmount = 0

        SqlStr = "SELECT SUM(PAYABLEAMOUNT) AS Amount FROM PAY_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE = '" & mCode & "'"

        If pSalDate = "" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM') = '" & VB6.Format(txtDOL.Text, "YYYYMM") & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM') = '" & VB6.Format(pSalDate, "YYYYMM") & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND SALHEADCODE=" & pSalCode & " AND ISARREAR IN ('Y','N') " ','F'

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSal.EOF = False Then
            GetCurrentAmount = IIf(IsDbNull(RsSal.Fields("Amount").Value), 0, RsSal.Fields("Amount").Value)
            Exit Function
        End If
        Exit Function
ErrCalcBonus:
        GetCurrentAmount = 0
    End Function
    Private Function GetCurrentArrearPayable(ByRef mCode As String, ByRef mOnlyBasic As String) As Double

        On Error GoTo ErrCalcBonus
        Dim mCurrentBasicInc As Double
        Dim mCurrentBasicPayable As Double
        Dim mCurrentArrearPayable As Double
        Dim RsSal As ADODB.Recordset
        Dim mDedCode As Double
        Dim mCurrentAmountPayable As Double
        Dim mCurrentIncAmountPayable As Double
        Dim mSalDate As String
        Dim mWEFDate As String
        Dim mCurrentIncPayable As Double
        Dim mWDays As Double
        Dim mLastDays As Double
        Dim mMonthDate As String
        Dim mAddDays As Integer

        GetCurrentArrearPayable = 0


        '    SqlStr = "SELECT * FROM PAY_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE = '" & mCode & "' AND TO_CHAR(SAL_DATE,'YYYYMM') = '" & VB6.Format(txtDOL.Text, "YYYYMM") & "' AND ISARREAR<>'F'"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsSal, adLockOptimistic
        '    If RsSal.EOF = False Then
        '        Exit Function
        '    End If

        If mOnlyBasic = "Y" Then
            SqlStr = " SELECT DISTINCT BASICSALARY,TRN.TOT_ARR_MONTH,SALARY_EFF_DATE  AS WEF_DATE,ADDDAYS_IN"
        Else
            SqlStr = " SELECT TRN.PREVIOUS_BASICSALARY, BASICSALARY,TRN.AMOUNT AS AMOUNT,ADD_DEDUCTCODE,MST.ADDDEDUCT,TRN.TOT_ARR_MONTH,SALARY_EFF_DATE AS WEF_DATE, ADDDAYS_IN,ADD_DEDUCTCODE"
        End If
        SqlStr = SqlStr & vbCrLf & " " & vbCrLf & " FROM PAY_SALARYDEF_MST TRN, PAY_SALARYHEAD_MST MST" & vbCrLf & " WHERE TRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.Company_Code =MST.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE =MST.CODE AND MST.ADDDEDUCT=" & ConEarning & "" & vbCrLf & " AND TRN.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND TO_CHAR(TRN.ARREAR_DATE,'YYYYMM') >= '" & VB6.Format(txtDOL.Text, "YYYYMM") & "'" & vbCrLf & " AND TRN.IS_ARREAR='Y' AND MST.ADDDEDUCT in (1,2) AND MST.CALC_ON=1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

        mSalDate = "01/" & VB6.Format(txtDOL.Text, "MM/YYYY")
        mSalDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mSalDate)))
        If RsSal.EOF = False Then
            mWEFDate = IIf(IsDbNull(RsSal.Fields("WEF_DATE").Value), "", RsSal.Fields("WEF_DATE").Value)
            mCurrentBasicInc = IIf(IsDbNull(RsSal.Fields("BASICSALARY").Value), 0, RsSal.Fields("BASICSALARY").Value)
            mAddDays = IIf(IsDbNull(RsSal.Fields("ADDDAYS_IN").Value), 0, RsSal.Fields("ADDDAYS_IN").Value)
            Do While CDate(mWEFDate) <= CDate(mSalDate)
                mWDays = GetWorkingDays(mCode, mSalDate, "S")
                mLastDays = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate)))
                mCurrentIncPayable = System.Math.Round(mCurrentBasicInc * mWDays / mLastDays, 0)
                mCurrentBasicPayable = System.Math.Round(GetCurrentArrearBasic(mCode, mSalDate), 0)
                GetCurrentArrearPayable = GetCurrentArrearPayable + (mCurrentIncPayable - mCurrentBasicPayable)

                Do While RsSal.EOF = False
                    If mOnlyBasic = "N" Then
                        mDedCode = IIf(IsDbNull(RsSal.Fields("ADD_DEDUCTCODE").Value), -1, RsSal.Fields("ADD_DEDUCTCODE").Value)
                        mCurrentAmountPayable = System.Math.Round(GetCurrentAmount(mCode, mSalDate, mDedCode), 0)
                        mCurrentIncAmountPayable = System.Math.Round(IIf(IsDbNull(RsSal.Fields("Amount").Value), 0, RsSal.Fields("Amount").Value) * mWDays / mLastDays, 0)

                        If RsSal.Fields("ADDDEDUCT").Value = 1 Then
                            GetCurrentArrearPayable = GetCurrentArrearPayable + (mCurrentIncAmountPayable - mCurrentAmountPayable)
                        Else
                            GetCurrentArrearPayable = GetCurrentArrearPayable - (mCurrentIncAmountPayable - mCurrentAmountPayable)
                        End If
                    End If
                    RsSal.MoveNext()
                Loop
                RsSal.MoveFirst()
                '            GetCurrentArrearPayable = GetCurrentArrearPayable + (IIf(IsNull(RsSal!Amount), 0, RsSal!Amount) * (IIf(IsNull(RsSal!TOT_ARR_MONTH), 0, RsSal!TOT_ARR_MONTH) + (IIf(IsNull(RsSal!ADDDAYS_IN), 0, RsSal!ADDDAYS_IN) / 30)))
                mSalDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(mSalDate)))
                mLastDays = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate)))
                mSalDate = mLastDays & "/" & VB6.Format(mSalDate, "MM/YYYY")
            Loop

            If mAddDays > 0 Then
                mMonthDate = VB6.Format(mAddDays & "/" & VB6.Format(mSalDate, "MM/YYYY"), "DD/MM/YYYY")
                mWDays = GetWorkingDays(mCode, mMonthDate, "A") ''mAddDays
                mLastDays = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate)))
                mCurrentIncPayable = System.Math.Round(mCurrentBasicInc * mWDays / mLastDays, 0)
                mCurrentBasicPayable = System.Math.Round(GetCurrentArrearBasic(mCode, mSalDate), 0)
                mCurrentBasicPayable = System.Math.Round(mCurrentBasicPayable * mWDays / mLastDays, 0)
                GetCurrentArrearPayable = GetCurrentArrearPayable + (mCurrentIncPayable - mCurrentBasicPayable)
                Do While RsSal.EOF = False
                    If mOnlyBasic = "N" Then
                        mDedCode = IIf(IsDbNull(RsSal.Fields("ADD_DEDUCTCODE").Value), -1, RsSal.Fields("ADD_DEDUCTCODE").Value)
                        mCurrentAmountPayable = System.Math.Round(GetCurrentAmount(mCode, mSalDate, mDedCode), 0)
                        mCurrentAmountPayable = System.Math.Round(mCurrentAmountPayable * mWDays / mLastDays, 0)
                        mCurrentIncAmountPayable = System.Math.Round(IIf(IsDbNull(RsSal.Fields("Amount").Value), 0, RsSal.Fields("Amount").Value) * mWDays / mLastDays, 0)

                        If RsSal.Fields("ADDDEDUCT").Value = 1 Then
                            GetCurrentArrearPayable = GetCurrentArrearPayable + (mCurrentIncAmountPayable - mCurrentAmountPayable)
                        Else
                            GetCurrentArrearPayable = GetCurrentArrearPayable - (mCurrentIncAmountPayable - mCurrentAmountPayable)
                        End If
                    End If
                    RsSal.MoveNext()
                Loop
            End If
        End If



        '    GetCurrentArrearPayable = GetCurrentArrearBasic(mCode)
        '    If RsSal.EOF = False Then
        '
        '        Do While RsSal.EOF = False
        '            GetCurrentArrearPayable = GetCurrentArrearPayable + (IIf(IsNull(RsSal!Amount), 0, RsSal!Amount) * (IIf(IsNull(RsSal!TOT_ARR_MONTH), 0, RsSal!TOT_ARR_MONTH) + (IIf(IsNull(RsSal!ADDDAYS_IN), 0, RsSal!ADDDAYS_IN) / 30)))
        '            RsSal.MoveNext
        '        Loop
        '    End If
        '
        Exit Function
ErrCalcBonus:
        '    Resume
        GetCurrentArrearPayable = 0
    End Function
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
        Dim mPFCeiling As Double
        CalcBasicPFSalary = IIf(IsNumeric(txtBSalary.Text), txtBSalary.Text, 0) ''+ GetCurrentArrearPayable(mCode, "Y")
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
            If CheckPFCeilingOn(txtDOL.Text) = "C" Then
                mPFCeiling = CheckPFCeiling(txtDOL.Text)
            Else
                mPFCeiling = CalcBasicPFSalary
            End If

            CalcBasicPFSalary = IIf(CalcBasicPFSalary >= mPFCeiling, mPFCeiling, CalcBasicPFSalary)
        End If
    End Function

    Private Sub CheckPFRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = ""
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & ConPF & "" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "

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
    Public Sub CalcPFESI()
        On Error GoTo ERR1
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        Dim mRounding As String
        Dim mRound As String
        Dim PayableAmount As Double
        Dim mArrearBasic As Double

        If Val(txtSalArrear.Text) <> 0 Then
            mArrearBasic = GetCurrentArrearPayable(Trim(txtEmpNo.Text), "Y")
        End If

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
                If mType = ConPF Or mType = ConVPFAllw Then
                    PayableAmount = CalcBasicPFSalary(mType)

                    If RsCompany.Fields("COMPANY_CODE").Value = 5 Then
                        '                    If CVDate(txtDOL.Text) >= CVDate("01/04/2008") Then
                        '                        PayableAmount = PayableAmount + Val(mArrearBasic)
                        '                    Else
                        PayableAmount = PayableAmount + Val(CStr(mArrearBasic)) + Val(lblBasicEL.Text)
                        '                    End If
                    Else
                        '                    If CVDate(txtDOL.Text) >= CVDate("01/04/2008") Then
                        '                        PayableAmount = PayableAmount + Val(mArrearBasic)
                        '                    Else
                        PayableAmount = PayableAmount + Val(CStr(mArrearBasic)) + Val(lblBasicEL.Text) ''Val(txtELAmount.Text)
                        '                    End If
                    End If
                ElseIf mType = ConESI Then
                    PayableAmount = CalcBasicPFSalary(mType)
                    If PayableAmount <= mESICeiling Then
                        PayableAmount = PayableAmount + Val(txtIncAmtForMon.Text) + Val(txtIncAmtPreMon.Text) '' Not Applicable.. + Val(txtIncArrear.Text) + Val(txtSalArrear.Text)
                    Else
                        sprdDeduct.Col = ColPer
                        sprdDeduct.Text = "0.00"

                        PayableAmount = 0
                    End If
                Else
                    PayableAmount = CalcBasicPFSalary(mType)
                End If
                sprdDeduct.Col = ColAmt
                sprdDeduct.Text = CStr(xPer * PayableAmount / 100)
            End If

            If MainClass.ValidateWithMasterTable(mCode, "Code", "ROUNDING", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mRounding = MasterNo
            End If

            If mRounding = "0.05" Then
                sprdDeduct.Text = CStr(PaiseRound(Val(sprdDeduct.Text), 0.05))
            Else
                mRound = Replace(mRounding, "1", "0")
                sprdDeduct.Text = CStr(System.Math.Round(Val(sprdDeduct.Text), CInt(mRound)))
                '            sprdDeduct.Text = Format(Val(sprdDeduct.Text), mRound)
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

        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "dd-mmm-yyyy") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mESICeiling = IIf(IsDbNull(RsCeiling.Fields("CEILING").Value), 0, RsCeiling.Fields("CEILING").Value)
            mESIRate = IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
        Else
            If CDate(mDate) >= CDate("01/07/2019") Then
                mESICeiling = 21000
                mESIRate = 0.75
            Else
                mESICeiling = 7500
                mESIRate = 1.75
            End If
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
        Dim mActualAmt As Double
        Dim mLastDay As Double

        If Trim(txtDOL.Text) = "" Then Exit Sub

        mLastDay = MainClass.LastDay(Month(CDate(txtDOL.Text)), Year(CDate(txtDOL.Text)))

        For mcntRow = 1 To sprdEarn.MaxRows
            sprdEarn.Row = mcntRow

            sprdEarn.Col = ColPer
            xPer = IIf(IsNumeric(sprdEarn.Text), sprdEarn.Text, 0)

            sprdEarn.Col = ColActualAmt
            If xPer = 0 Then

            Else
                sprdEarn.Text = CStr(xPer * Val(txtAtcBasic.Text) / 100)
            End If
            mActualAmt = Val(sprdEarn.Text)

            sprdEarn.Col = ColAmt
            If xPer = 0 Then
                If Val(txtAtcBasic.Text) <> 0 Then
                    sprdEarn.Text = CStr(mActualAmt * Val(txtBSalary.Text) / Val(txtAtcBasic.Text))
                End If
            Else
                sprdEarn.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
            End If
        Next
    End Sub
    Private Sub txtDOL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOL.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub ShowAtcSalary(ByRef xCode As String, ByRef xWEF As String)

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer
        Dim mESIApp As String

        If Trim(txtDOL.Text) = "" Then Exit Sub

        Call CheckPFRates(CDate(VB6.Format(txtDOL.Text, "dd/mm/yyyy")))
        Call CheckESIRates(CDate(VB6.Format(txtDOL.Text, "dd/mm/yyyy")))

        SqlStr = " SELECT * FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

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
                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    If Val(.Text) = 0 Then
                        .Col = ColActualAmt
                        .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))
                    End If
                End If
            Next
        End With


        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                If MainClass.ValidateWithMasterTable(mTypeCode, "CODE", "NAME", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE=" & ConESI & "") = True Then
                    mESIApp = "Y"
                Else
                    mESIApp = "N"
                End If

                RsADD.MoveFirst()

                Do While RsADD.EOF = False
                    If mTypeCode = RsADD.Fields("ADD_DEDUCTCODE").Value Then
                        Exit Do
                    End If
                    RsADD.MoveNext()
                Loop

                If RsADD.EOF = False Then
                    .Col = ColPer
                    If mESIApp = "Y" Then
                        .Text = CStr(mESIRate)
                    Else
                        .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))
                    End If
                End If
            Next
        End With

    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim SqlStrSub As String
        Dim mRemarks As String
        Dim mRemarks1 As String
        Dim mDOJ As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        MainClass.AssignCRptFormulas(Report1, "Desg=""" & Trim(cbodesignation.Text) & """")

        mRemarks = "Received with thanks from M/s. " & RsCompany.Fields("Company_Name").Value
        mRemarks = mRemarks & " the sum of Rs. " & VB6.Format(txtNetSalary.Text, "0.00")
        mRemarks = mRemarks & " ( " & MainClass.RupeesConversion(Val(txtNetSalary.Text)) & ")"
        mRemarks = mRemarks & " in full and final settlement and complete satisfaction "
        mRemarks1 = "of all claim or any claim in connection with any employment with the company. "
        mRemarks1 = mRemarks1 & "I have no claim / demand for reinstatement of reemployment. I agree to withdraw "
        mRemarks1 = mRemarks1 & "all my claim and undertake that i will not raise any claim whatsoever against the management."

        MainClass.AssignCRptFormulas(Report1, "Remarks=""" & mRemarks & """")
        MainClass.AssignCRptFormulas(Report1, "Remarks1=""" & mRemarks1 & """")

        MainClass.AssignCRptFormulas(Report1, "mDOJ=""" & VB6.Format(txtDOJ.Text, "DD/MM/YYYY") & """")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName

        ''"SubReport

        SqlStrSub = "SELECT * FROM " & vbCrLf _
            & " PAY_FFSETTLE_DET , PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE PAY_FFSETTLE_DET.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAY_FFSETTLE_DET.COMPANY_CODE=PAY_SALARYHEAD_MST.COMPANY_CODE" & vbCrLf _
            & " AND PAY_FFSETTLE_DET.SALHEADCODE=PAY_SALARYHEAD_MST.CODE " & vbCrLf _
            & " AND PAY_SALARYHEAD_MST.ADDDEDUCT = 2 " & vbCrLf _
            & " AND PAY_FFSETTLE_DET.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"

        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStrSub

        Report1.SubreportToChange = ""

        Report1.Action = 1
        Report1.Reset()
        Report1.ReportFileName = ""

    End Sub

    Private Sub ShowLetterReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mServicePeriod As String
        Dim mAmountInword As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        '    MainClass.AssignCRptFormulas Report1, "Name=""" & Trim(TxtName.Text) & """"
        '    MainClass.AssignCRptFormulas Report1, "GratuityAmount=""" & vb6.Format(txtGratuityAmt.Text, "0.00") & """"
        MainClass.AssignCRptFormulas(Report1, "mDOJ=""" & VB6.Format(txtDOJ.Text, "DD/MM/YYYY") & """")
        '    MainClass.AssignCRptFormulas Report1, "DOL=""" & vb6.Format(txtDOL.Text, "DD/MM/YYYY") & """"

        If Val(txtGratuityAmt.Text) > 0 Then
            mServicePeriod = CStr(DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDOJ.Text), CDate(txtDOL.Text)))
            mServicePeriod = CStr(CDbl(mServicePeriod) / 12)
        Else
            mServicePeriod = CStr(0)
        End If

        MainClass.AssignCRptFormulas(Report1, "ServicePeriod=""" & mServicePeriod & """")
        MainClass.AssignCRptFormulas(Report1, "BasicSalary=""" & VB6.Format(txtAtcBasic.Text, "0.00") & """")

        mAmountInword = " (Rs. " & MainClass.RupeesConversion(VB6.Format(txtGratuityAmt.Text, "0.00")) & ")"

        MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName


        Report1.Action = 1
        Report1.Reset()
        Report1.ReportFileName = ""

    End Sub
    Private Sub txtNoticeamt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNoticeamt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNoticeamt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNoticeamt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNoticeamt_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNoticeamt.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNoticeMon_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNoticeMon.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNoticeMon_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNoticeMon.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNoticeMon_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNoticeMon.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOthers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthers.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtOthers_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOthers.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
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
        Dim mLeaveDay As Integer
        Dim mStartDate As String
        Dim mWDays As Double

        '    If Val(txtAtcBasic) = 0 Then Exit Sub
        '    If Val(txtPaidDays) = 0 Then Exit Sub

        If Trim(txtDOL.Text) = "" Then GoTo EventExitSub

        mStartDate = VB6.Format("01/" & VB6.Format(txtDOL.Text, "MM/YYYY"), "DD/MM/YYYY")

        mLeaveDay = VB.Day(CDate(txtDOL.Text))


        If Val(txtPaidDays.Text) > 0 Then
            If CheckSalaryMade((txtEmpNo.Text), VB6.Format(txtDOL.Text, "DD/MM/YYYY")) = True Then
                MsgInformation("Salary Made Against This Month So Please check the paid days.")
                GoTo EventExitSub
            End If

            mWDays = CalcAttnPresent((txtEmpNo.Text), VB6.Format(mStartDate, "DD/MM/YYYY"), VB6.Format(txtDOL.Text, "DD/MM/YYYY"), VB6.Format(txtDOJ.Text, "DD/MM/YYYY"))


            If Val(txtPaidDays.Text) <> Val(CStr(mWDays)) Then
                MsgInformation("Paid Days is not equal to Present Date.")
                Cancel = True
                GoTo EventExitSub
            End If
        End If

        If Val(txtPaidDays.Text) > Val(CStr(mLeaveDay)) Then
            MsgInformation("Paid Days Cann't be greater than Leave Day.")
            Cancel = True
            GoTo EventExitSub
        End If


        mLastDay = MainClass.LastDay(Month(CDate(txtDOL.Text)), Year(CDate(txtDOL.Text)))

        txtBSalary.Text = VB6.Format(Val(txtAtcBasic.Text) * IIf(chkSuspension.CheckState = System.Windows.Forms.CheckState.Checked, Val(txtSuspension.Text) * 0.01, 1) * Val(txtPaidDays.Text) / mLastDay, "0.00")
        txtBSalary.Text = CStr(System.Math.Round(CDbl(txtBSalary.Text), 0))
        CalcEarn()
        CalcPFESI()
        '    CalcOthers
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReason.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReason.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtReason.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    Private Sub txtSalArrear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSalArrear.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSalArrear_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSalArrear.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSalArrear_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSalArrear.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSuspension_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuspension.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuspension_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSuspension.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSuspension_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuspension.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call txtPaidDays_Validating(txtPaidDays, New System.ComponentModel.CancelEventArgs(True))
        Call CalcGrossSalary(IIf(ADDMode = True, "Y", "N"))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotOthers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotOthers.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTotOthers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotOthers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub




    Private Function CheckOverTime(ByRef mEmpCode As String, ByRef mDate As String, ByRef mEnterOverTime As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsOTTRN As ADODB.Recordset
        Dim mOTHour As Double
        Dim mOTMin As Double
        Dim mOverTime As Double
        Dim mOTFactor As Double

        CheckOverTime = False

        SqlStr = " SELECT " & vbCrLf & " EMP_CODE " & vbCrLf & " FROM PAY_MONTHLY_OT_TRN OT " & vbCrLf & " WHERE " & vbCrLf & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND OT.EMP_CODE = '" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND TO_CHAR(OT.OT_DATE,'YYYYMM')='" & VB6.Format(mDate, "YYYYMM") & "' AND IS_ARREAR='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOTTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOTTRN.EOF = False Then
            If mEnterOverTime > 0 Then
                CheckOverTime = False
                Exit Function
            End If
        End If

        SqlStr = " SELECT " & vbCrLf & " SUM(OT.OTHOUR+OT.PREV_OTHOUR) AS OTHOUR , SUM(OT.OTMIN+OT.PREV_OTMIN)AS OTMIN " & vbCrLf & " FROM PAY_OVERTIME_MST OT " & vbCrLf & " WHERE " & vbCrLf & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND OT.EMP_CODE = '" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND TO_CHAR(OT.OT_DATE,'YYYYMM')='" & VB6.Format(mDate, "YYYYMM") & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOTTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOTTRN.EOF = False Then
            mOTHour = IIf(IsDbNull(RsOTTRN.Fields("OTHOUR").Value), 0, RsOTTRN.Fields("OTHOUR").Value)
            mOTMin = IIf(IsDbNull(RsOTTRN.Fields("OTMIN").Value), 0, RsOTTRN.Fields("OTMIN").Value)
        End If

        mOTFactor = 0
        If MainClass.ValidateWithMasterTable(txtEmpNo.Text, "EMP_CODE", "EMP_OT_RATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mOTFactor = MasterNo
        End If

        mOverTime = CDbl(VB6.Format(GetTOTOverTime(mOTHour, mOTMin), "0.00")) ''* 2

        If mOTFactor = 0 Then
            mOverTime = 0
        Else
            mOverTime = mOverTime / mOTFactor
        End If

        If mOverTime = mEnterOverTime Then
            CheckOverTime = True
        Else
            MsgInformation("F&F Month Over Time (" & mEnterOverTime & " Hrs) Not match with Actual Over Time (" & mOverTime & " Hrs).")
        End If
        Exit Function

ErrPart:
        CheckOverTime = False
    End Function

    Private Function GetTOTOverTime(ByRef xTotOTHOUR As Double, ByRef xTotOTMIN As Double) As Double
        On Error GoTo ErrPart
        Dim mHour As Double
        Dim mTempMin As Double
        Dim mMin As Double
        Dim mFactor As Double

        mHour = xTotOTHOUR
        mTempMin = xTotOTMIN

        mHour = mHour + Int(mTempMin / 60)
        mMin = (mTempMin Mod 60)
        mFactor = IIf(IsDbNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)
        mMin = Int(mMin / mFactor) * mFactor

        If mMin <> 0 Then
            mMin = mMin / 60
        End If

        GetTOTOverTime = mHour + mMin

        Exit Function
ErrPart:
        GetTOTOverTime = 0
    End Function

    Private Sub cmdEMailAccounts_Click(sender As Object, e As EventArgs) Handles cmdEMailAccounts.Click
        On Error GoTo ErrPart
        If Trim(txtEmpNo.Text) = "" Then Exit Sub

        If MsgQuestion("Are you sure to send the Mail..") = vbNo Then
            Exit Sub
        End If

        If SendMail("A") = False Then GoTo ErrPart
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function SendMail(ByRef pFlag As Object) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String

        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mAttachmentFile As String
        Dim mDateTime As String
        Dim pAccountCode As String
        Dim mSubject As String
        Dim mBodyText As String
        Dim mBodyText1 As String
        Dim mDeptCODE As String = ""
        Dim mDeptName As String = ""

        SendMail = False


        mFrom = ""
        If MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "EMAIL", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mFrom = MasterNo
        Else
            mFrom = ""
        End If

        If MainClass.ValidateWithMasterTable(txtEmpNo.Text, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCODE = MasterNo
            If MainClass.ValidateWithMasterTable(mDeptCODE, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptName = MasterNo
            End If
        End If

        mFrom = If(Len(mFrom) < 5, "", Trim(mFrom))

        If mFrom = "" Then
            mFrom = GetEMailID("MAIL_ACCOUNT")  ''strAccount
        End If

        If pFlag = "A" Then
            mTo = GetEMailID("FNF_MAIL_TO")
        Else
            mTo = GetEMailID("INSURANCE_MAIL_TO")
        End If


        mCC = If(Len(mCC) < 5, "", Trim(mCC))


        mAttachmentFile = ""

        mSubject = ""

        mSubject = "Full & Final Settlement of Employee :" & Trim(txtEmpNo.Text) & " - " & Trim(TxtName.Text) & ""



        mBodyText1 = "Mr/Ms " & Trim(TxtName.Text) & " has been relieved from his/her services towards " & RsCompany.Fields("COMPANY_NAME").Value
        mBodyText1 = mBodyText1 & "  from the closing hours on Dated " & VB6.Format(txtDOL.Text, "DD/MM/YYYY") & "."

        mBodyText = "<html><body><b><color=Blue>Full & Final Settlement</font></b><br />" & mBodyText1 & "<br />" _
            & "<b>Name : </b>" & Trim(TxtName.Text) & "<br />" _
            & "<b>FName : </b>" & Trim(txtFName.Text) & "<br />" _
            & "<b>ID No : </b>" & Trim(txtEmpNo.Text) & "<br />" _
            & "<b>Department : </b>" & Trim(mDeptName) & "<br />" _
            & "<b>Date of Joining : </b>" & VB6.Format(txtDOJ.Text) & "<br />" _
            & "<b>Date of Leaving : </b>" & VB6.Format(txtDOL.Text) & "<br />" _
            & "</body></html>"


        ''mBodyText = "<html><body><b><font size=6, color=Blue>Full & Final Settlement</font></b><br />Employee Name : " & Trim(TxtName.Text) & "<br />" & "<b>Department    : </b>" & Trim(mDeptName) & "<br />" & "<b>Dated         : </b>" & VB6.Format(txtDOL.Text) & "<br />" & "</body></html>"


        '        MS.Nisha Rani has been relieved from his/her services towards Auxein Medical Pvt Ltd Kundli from the closing hours on Dated DD/MM/YY(Date of Leaving).
        'His/ Her Details are Given Below:-
        '1. Name- Nisha Rani
        '2. F'name-
        '3. Address
        '4. Date of Birth
        '5. ID NO.-AUX289
        '6. Date of Joining
        '7. Date of Confirmation
        '8. Date of Leaving 


        If Trim(mTo) <> "" Then
            If SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText) = False Then
                SendMail = True
                Exit Function
            End If
        End If

        SendMail = True

        Exit Function
ErrPart:
        SendMail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdEmailExternal_Click(sender As Object, e As EventArgs) Handles cmdEmailExternal.Click
        On Error GoTo ErrPart

        If Trim(txtEmpNo.Text) = "" Then Exit Sub

        If MsgQuestion("Are you sure to send the Mail..") = vbNo Then
            Exit Sub
        End If
        If SendMail("I") = False Then GoTo ErrPart
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
End Class
