Option Strict Off										
Option Explicit On										
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Friend Class frmAtrn
    Inherits System.Windows.Forms.Form
    Private RsTRNMain As ADODB.Recordset '' ADODB.Recordset										
    Private RsTRNDetail As ADODB.Recordset ''ADODB.Recordset										
    Private XRIGHT As String
    Private pMYMenu As String
    Private ADDMode As Boolean
    Private MODIFYMode As Boolean
    Private FormLoaded As Boolean
    Private ShowSalary As Boolean
    Private CurMKey As String
    Dim mRowNo As Integer
    'Private PvtDBCn As ADODB.Connection										
    Dim PaymentDetailShow As Boolean
    Dim pProcessKey As Double
    Private Const ColPRRowNo As Short = 1
    Private Const ColDC As Short = 2
    Private Const ColAccountName As Short = 3
    Private Const ColParticulars As Short = 4
    Private Const ColChequeNo As Short = 5
    Private Const ColChequeDate As Short = 6
    Private Const ColEmp As Short = 7
    Private Const ColDept As Short = 8
    Private Const ColCC As Short = 9
    Private Const ColExp As Short = 10
    Private Const ColDivisionCode As Short = 11
    Private Const ColIBRNo As Short = 12
    Private Const ColAmount As Short = 13
    Private Const ColSAC As Short = 14
    Private Const ColCGSTPer As Short = 15
    Private Const ColCGSTAmount As Short = 16
    Private Const ColSGSTPer As Short = 17
    Private Const ColSGSTAmount As Short = 18
    Private Const ColIGSTPer As Short = 19
    Private Const ColIGSTAmount As Short = 20
    Private Const ColSaleBillPrefix As Short = 21
    Private Const ColSaleBillSeq As Short = 22
    Private Const ColSaleBillNo As Short = 23
    Private Const ColSaleBillDate As Short = 24
    Private Const ColClearDate As Short = 25
    Private Const ConRowHeight As Short = 22
    Dim mAuthorised As String
    Private xPrevModvatNo As Integer
    Private xPrevSTRefundNo As Integer
    Private xPrevServNo As Integer
    Private xPrevVnoStr As String
    Private xPrevISCapital As String
    Private xPrevISPLA As String
    Private xPrevServTaxClaim As String
    Private xPrevServTaxRefund As String
    Private xPrevSuppBill As String
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Function CheckExpHead(ByRef mAcctName As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        CheckExpHead = False
        SqlStr = "Select BSGROUP.BSGROUP_ACCTTYPE " & vbCrLf & " FROM FIN_SUPP_CUST_MST,FIN_GROUP_MST ACMGROUP, " & vbCrLf & " FIN_BSGROUP_MST BSGROUP WHERE " & vbCrLf & " FIN_SUPP_CUST_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf & " AND BSGROUP.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE" & vbCrLf & " AND FIN_SUPP_CUST_MST.GROUPCODE=GROUP_Code " & vbCrLf & " AND GROUP_BSCodeDr=BSGROUP_Code " & vbCrLf & " AND BSGROUP_ACCTTYPE IN (" & ConIncome & "," & ConExpenses & ")" & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(Trim(mAcctName)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            CheckExpHead = True
        Else
            CheckExpHead = False
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckExpHead = False
    End Function
    Private Sub ChkPLA_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPLA.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkPnL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPnL.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkReverseCharge_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkReverseCharge.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub chkServTaxClaim_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkServTaxClaim.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkServTaxRefund_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkServTaxRefund.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkSTClaim_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSTClaim.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkSTDS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSTDS.CheckStateChanged
        Dim mAccountName As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        SprdMain.Row = 2
        SprdMain.Col = ColAccountName
        mAccountName = Trim(SprdMain.Text)
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSRate.Enabled = True
            txtSTDSDeductOn.Enabled = True
            If Val(txtSTDSRate.Text) = 0 Then
                SqlStr = "SELECT STDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mAccountName) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtSTDSRate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("STDS_PER").Value), 0, RsTemp.Fields("STDS_PER").Value), "0.000")
                End If
            End If
        Else
            txtSTDSRate.Enabled = False
            txtSTDSDeductOn.Enabled = False
            txtSTDSRate.Text = CStr(0)
        End If
        CalcTots()
    End Sub
    Private Sub ChkSTDSRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSTDSRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub chkSuppBill_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSuppBill.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        '    If chkSuppBill.Value = vbChecked Then										
        '        txtModvatNo.Enabled = True										
        '        txtSTRefundNo.Enabled = True										
        ''        txtServiceTaxNo.Enabled = True										
        '        ChkCapital.Enabled = True										
        '    Else										
        '        txtModvatNo.Enabled = False										
        '        txtSTRefundNo.Enabled = False										
        ''        txtServiceTaxNo.Enabled = False										
        '        ChkCapital.Enabled = False										
        '    End If										
    End Sub
    Private Sub chkTDS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTDS.CheckStateChanged
        Dim mAccountName As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        SprdMain.Row = 2
        SprdMain.Col = ColAccountName
        mAccountName = Trim(SprdMain.Text)
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtJVTDSRate.Enabled = True
            txtTDSDeductOn.Enabled = True
            txtTDSSection.Enabled = True
            If Val(txtJVTDSRate.Text) = 0 Then
                SqlStr = "SELECT TDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mAccountName) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtJVTDSRate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDS_PER").Value), 0, RsTemp.Fields("TDS_PER").Value), "0.000")
                End If
            End If
        Else
            txtJVTDSRate.Enabled = False
            txtTDSSection.Enabled = False
            txtTDSDeductOn.Enabled = False
            txtJVTDSRate.Text = CStr(0)
        End If
        txtJVTDSRate.Text = VB6.Format(txtJVTDSRate.Text, "0.000")
        CalcTots()
    End Sub
    Private Sub ChkTDSRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkTDSRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        If cmdAdd.Text = ConCmdAddCaption Then
            Clear1()
            If lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Then
                If CheckPendingPDC() = True Then Exit Sub
            End If
            ADDMode = True
            MODIFYMode = False
            cmdAdd.Text = ConCmdCancelCaption
            SprdMain.Enabled = True
            If txtPartyName.Visible = True Then
                txtPartyName.Focus()
            Else
                MainClass.SetFocusToCell(SprdMain, 1, ColDC)
            End If
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            cmdAdd.Text = ConCmdAddCaption
            Clear1()
            Show1()
        End If
    End Sub
    Private Function CheckPendingPDC() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsPDC As ADODB.Recordset = Nothing
        Dim xBookType As String = ""
        Dim xBookSubType As String = ""
        Dim xAccountCode As String = ""
        Dim mChq As String = ""
        If lblBookType.Text = ConBankReceipt Then
            xBookType = VB.Left(ConPDCReceipt, 1)
            xBookSubType = VB.Right(ConPDCReceipt, 1)
        ElseIf lblBookType.Text = ConBankPayment Then
            xBookType = VB.Left(ConPDCPayment, 1)
            xBookSubType = VB.Right(ConPDCPayment, 1)
        End If
        If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            xAccountCode = IIf(IsDbNull(MasterNo), -1, MasterNo)
        Else
            xAccountCode = CStr(-1)
        End If
        SqlStr = "SELECT 'VNO : ' || FIN_VOUCHER_HDR.VNO || ':' || 'CHQ NO : ' || CHEQUENO AS VNO FROM FIN_VOUCHER_HDR,FIN_VOUCHER_DET" & vbCrLf & " WHERE FIN_VOUCHER_HDR.MKEY=FIN_VOUCHER_DET.MKEY " & vbCrLf & " AND COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & " AND CHQDATE<=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf & " AND BOOKSUBTYPE='" & xBookSubType & "' AND BOOKCODE='" & xAccountCode & "' AND CANCELLED='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPDC, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPDC.EOF = False Then
            Do While Not RsPDC.EOF
                mChq = IIf(mChq = "", "", mChq & vbNewLine) & IIf(IsDbNull(RsPDC.Fields("VNO").Value), "", RsPDC.Fields("VNO").Value)
                RsPDC.MoveNext()
            Loop
            MsgBox("Following PDC are pending for Normalization " & vbNewLine & mChq, MsgBoxStyle.Information)
            CheckPendingPDC = True
        Else
            CheckPendingPDC = False
        End If
        RsPDC.Close()
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        RsPDC.Close()
    End Function
    Private Sub Clear1()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        'If lblBookType.Text = ConBankPayment Then
        '    TxtVDate.Text = CStr(RunDate)
        '    txtExpDate.Text = CStr(RunDate)
        'Else
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            If lblBookType.Text = ConBankPayment Then
                TxtVDate.Text = CStr(RunDate)
                txtExpDate.Text = CStr(RunDate)
            Else
                TxtVDate.Text = IIf(TxtVDate.Text = "", RunDate, TxtVDate.Text)
                txtExpDate.Text = IIf(TxtVDate.Text = "", RunDate, TxtVDate.Text)
            End If
        Else
            'TxtVDate.Text = TxtVDate.Text
            'txtExpDate.Text = txtExpDate.Text
        End If
        'End If
        TxtVDate.Enabled = True
        txtExpDate.Enabled = True

        txtVNo1.Enabled = False
        txtVNo1.Visible = True

        txtTDSSection.Text = ""

        txtVNo1.Text = GenPrefixVNo(TxtVDate.Text)

        If lblBookType.Text = ConCashReceipt Or lblBookType.Text = ConCashPayment Then
            txtPartyName.Text = IIf(txtPartyName.Text = "", GetCashBookName, txtPartyName.Text)
        ElseIf lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Then
            '' txtPartyName.Text = ""
        Else
            txtPartyName.Text = ""
        End If

        If lblBookType.Text = ConJournal Or lblBookType.Text = ConCashReceipt Or lblBookType.Text = ConCashPayment Then
            If Trim(txtVType.Text) = "" Then
                txtVType.Text = GetVType
            Else
                txtVType.Text = GetVType(Trim(txtVType.Text))
            End If
        ElseIf lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Then

        Else
            txtVType.Text = "" ''IIf(Trim(txtVType.Text) = "", GetVType, Trim(txtVType.Text))										
        End If


        txtVno.Text = ""
        txtVno.Enabled = True
        txtVNoSuffix.Text = ""
        lblBookBalAmt.Text = "0.00"
        lblBookBalDC.Text = "Dr"
        txtNarration.Text = ""
        LblDrAmt.Text = ""
        LblCrAmt.Text = ""
        LblNetAmt.Text = ""
        ConPaymentDetail = False
        ConServiceTaxDetail = False
        chkChqDeposit.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkChqDeposit.Enabled = False
        ChkACPayee.CheckState = System.Windows.Forms.CheckState.Checked
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPnL.CheckState = System.Windows.Forms.CheckState.Unchecked
        'If InStr(1, XRIGHT, "M") = 0 Then
        '    chkCancelled.Enabled = False
        'Else
        '    chkCancelled.Enabled = True
        'End If
        chkCancelled.Enabled = IIf(PubUserID = "G0416", True, False)

        txtPopulateVNo.Text = ""
        txtPopulateVNo.Enabled = True

        TxtTDSAccount.Text = ""
        chkExempted.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkISLowerDed.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtAmountPaid.Text = ""
        txtPName.Text = ""
        txtVD.Text = ""
        txtSection.Text = ""
        txtTDSAmount.Text = ""
        txtTdsRate.Text = ""
        txtExempted.Text = ""
        FraTDSFrame.Visible = False
        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""
        chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkReverseCharge.Enabled = True
        txtJVTDSRate.Text = "0.00"
        txtJVTDSAmount.Text = "0.00"
        chkTDS.Enabled = True
        chkESI.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtESIRate.Text = "0.00"
        txtESIAmount.Text = "0.00"
        chkESI.Enabled = True
        ChkSTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSTDSRate.Text = "0.00"
        txtSTDSAmount.Text = "0.00"
        ChkSTDS.Enabled = True
        txtJVVNO.Text = ""
        txtTDSDeductOn.Text = "0.00"
        txtESIDeductOn.Text = "0.00"
        txtSTDSDeductOn.Text = "0.00"
        ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked
        ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        txtImpPartyName.Text = ""
        txtImpMRRNo.Text = ""
        txtImpBillNo.Text = ""
        txtImpBillDate.Text = ""
        txtExpPartyName.Text = ""
        txtExpBillNo.Text = ""
        txtExpBillDate.Text = ""
        txtServProvided.Text = ""
        txtServiceOn.Text = ""
        txtServiceTaxPer.Text = ""
        txtServiceTaxAmount.Text = ""
        txtProviderPer.Text = ""
        txtRecipientPer.Text = ""
        '    chkServiceTaxClaim.Value = vbUnchecked										
        '    txtServTaxPer.Text = ""										
        '    txtCESSPer.Text = ""										
        '    txtServProvided.Text = ""										
        mAuthorised = "N"
        ssTab.SelectedIndex = 0
        CurMKey = ""
        '    lblSR.text = ""										
        xPrevModvatNo = 0
        xPrevSTRefundNo = 0
        xPrevVnoStr = ""
        xPrevISCapital = "N"
        xPrevServNo = 0
        xPrevISPLA = "N"
        xPrevServTaxClaim = "N"
        xPrevServTaxRefund = "N"
        chkSuppBill.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkModvat.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPLA.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkServTaxRefund.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSuppBill.Enabled = True
        chkModvat.Enabled = True
        chkCapital.Enabled = True
        chkPLA.Enabled = True
        chkSTClaim.Enabled = True
        chkServTaxClaim.Enabled = True
        chkServTaxRefund.Enabled = True
        txtModvatNo.Text = ""
        txtModvatNo.Enabled = True
        txtSTRefundNo.Text = ""
        txtSTRefundNo.Enabled = True
        txtServNo.Text = ""
        txtServNo.Enabled = True
        lblAcBalAmt.Text = "0.00"
        lblAcBalAmtDiv.Text = "0.00"
        lblAcBalDC.Text = ""
        lblAcBalDCDiv.Text = ""
        lblReversalMade.Text = "N"
        lblReversalVoucher.Text = "N"
        lblReversalMkey.Text = ""
        lblSaleBillNo.Text = ""
        pProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)
        SqlStr = "Delete from FIN_TEMPBILL_TRN  Where UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" ''BookType='" & Trim(lblBookType.text) & "'"										
        PubDBCn.Execute(SqlStr)
        SqlStr = "Delete from FIN_TEMP_SERVICE_TRN  Where UserID='" & PubUserID & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "Delete from TEMP_PAY_LOAN_MST   Where UserID='" & PubUserID & "'"
        PubDBCn.Execute(SqlStr)
        '    PubDBCn.Execute "Delete from Temp_BillDetail Where UserID='" & UCase(PubUserID) & "' AND BookType='" & UCase(Trim(lblBookType.text)) & "'"										
        SprdMain.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsTRNMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdAuthorised_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAuthorised.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mAccountCode As String
        Dim mLockBookCode As Integer
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtPartyName.Text)) = True Then
            Exit Sub
        End If
        If lblBookType.Text = ConCashReceipt Then
            mLockBookCode = CInt(ConLockCashReceipt)
        ElseIf lblBookType.Text = ConCashPayment Then
            mLockBookCode = CInt(ConLockCashPayment)
        ElseIf lblBookType.Text = ConBankReceipt Then
            mLockBookCode = CInt(ConLockBankReceipt)
        ElseIf lblBookType.Text = ConBankPayment Then
            mLockBookCode = CInt(ConLockBankPayment)
        ElseIf lblBookType.Text = ConPDCReceipt Then
            mLockBookCode = CInt(ConLockPDCReceipt)
        ElseIf lblBookType.Text = ConPDCPayment Then
            mLockBookCode = CInt(ConLockPDCPayment)
        Else
            mLockBookCode = CInt(ConLockJournal)
        End If
        If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
            Exit Sub
        End If
        If RsTRNMain.Fields("AUTHORISED").Value = "Y" Then
            MsgBox("Already Authorised.", MsgBoxStyle.Information)
            cmdAuthorised.Enabled = False
            Exit Sub
        End If
        If MsgQuestion("Want to Authorised Such Voucher. Once Authorised Cann't be Deleted or Modified.") = CStr(MsgBoxResult.No) Then Exit Sub
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " AUTHORISED='Y', " & vbCrLf & " AUTHORISED_CODE='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " AUTHORISED_DATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') "
        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & RsTRNMain.Fields("mKey").Value & "'           "
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        txtVNo_Validating(txtVno, New System.ComponentModel.CancelEventArgs(False))
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Information)
        PubDBCn.RollbackTrans()
    End Sub
    Private Sub cmdBillDetail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillDetail.Click

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cheque has been cancelled, So cann't be print.")
            Exit Sub
        End If

        If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
            Call PrintCheque()
        End If
    End Sub
    Private Sub PrintCheque()
        'Dim Printer As New Printer										
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mAmountInword As String
        Dim mAccountName As String
        Dim mChqDate As String
        Dim ii As Integer
        Dim mChqFormat As String
        'Dim prt As Printer										
        With SprdMain
            'Print Cheque only one for one voucher....										
            ''For ii = 1 To .MaxRows - 1										
            ''        SetCrpt Report1, mMode, 1, mTitle, mSubTitle										
            Call MainClass.ClearCRptFormulas(Report1)
            '        SetCrpt Report1, 1, 1, "Cheque Printing", ""										
            '        Report1.Reset										
            .Row = 1
            .Col = ColAccountName
            mAccountName = InputBox("Please Enter Account Name :", "Account Name", .Text)
            If mAccountName = "" Then Exit Sub
            If Val(LblNetAmt.Text) = 0 Then Exit Sub
            mChqFormat = InputBox("Enter 1 For Other Bank , 2 for Kotak Bank , 3 for ICICI Bank, 4 for Axis Bank, 5 for Canara Bank, 6 for S.B.P., 7 for S.B.I., 8 for Vijaya, 9 for Corporation Bank, 10 for BOB", "Cheque Format", "1")
            MainClass.AssignCRptFormulas(Report1, "AccountName=""" & mAccountName & """")
            ''.Col = ColAmount										
            MainClass.AssignCRptFormulas(Report1, "AMOUNT=""" & VB6.Format(CDbl(LblNetAmt.Text), "0.00") & """")
            mAmountInword = MainClass.RupeesConversion(CDbl(LblNetAmt.Text))
            mAmountInword = mAmountInword & " Only"
            .Col = ColChequeDate
            mChqDate = VB6.Format(.Text, "DD/MM/YYYY")
            MainClass.AssignCRptFormulas(Report1, "ChqDate=""" & mChqDate & """")
            MainClass.AssignCRptFormulas(Report1, "AmountInWords=""" & mAmountInword & """")
            If ChkACPayee.CheckState = System.Windows.Forms.CheckState.Checked Then
                MainClass.AssignCRptFormulas(Report1, "AcPayee=""A/C PAYEE ONLY""")
            Else
                MainClass.AssignCRptFormulas(Report1, "AcPayee=''")
            End If
            If mChqFormat = "10" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_10.RPT"
            ElseIf mChqFormat = "9" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_9.RPT"
            ElseIf mChqFormat = "8" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_8.RPT"
            ElseIf mChqFormat = "7" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_7.RPT"
            ElseIf mChqFormat = "6" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_6.RPT"
            ElseIf mChqFormat = "5" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_5.RPT"
            ElseIf mChqFormat = "4" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_4.RPT"
            ElseIf mChqFormat = "3" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_3.RPT"
            ElseIf mChqFormat = "2" Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ_2.RPT"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PRINTCHQ.RPT"
            End If
            Report1.Destination = 0
            Report1.DiscardSavedData = True
            MainClass.ReportWindow(Report1, "Cheque Printing")
            '        Report1.SQLQuery = ""   ''mSqlStr										
            Report1.WindowShowGroupTree = False
            'If PubUniversalPrinter = "Y" Then										
            '    For Each prt In Printers										
            '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then										
            '            Printer = prt										
            '            Report1.PrinterName = prt.DeviceName										
            '            Report1.PrinterDriver = prt.DriverName										
            '            Report1.PrinterPort = prt.Port										
            '            Exit For										
            '        End If										
            '    Next prt										
            'End If										
            Report1.Action = 1
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume										
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then										
        '        'PvtDBCn.Close										
        '        'Set PvtDBCn = Nothing										
        '    End If										
        If ADDMode = True Or MODIFYMode = True Then
            If MsgQuestion("Are you sure to Exit") = CStr(MsgBoxResult.No) Then
                Exit Sub
            End If
        End If
        RsTRNMain.Close()
        RsTRNMain = Nothing
        RsTRNDetail.Close()
        RsTRNDetail = Nothing
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        Dim SqlStr As String = ""
        On Error GoTo DelErrPart
        Dim ii As Integer
        Dim mVnoStr As String
        Dim mAccountCode As String
        Dim mLockBookCode As Integer
        Dim mIsCapital As String
        Dim pTDSChallanNo As String = ""
        Dim pClaimNo As String = ""
        Dim mChequeNo As String
        Dim VMkey As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRefNo As String
        Dim mReversalVoucher As String = ""

        Dim xBookType As String = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Then
            SqlStr = "SELECT * FROM FIN_ADVANCE_HDR WHERE BANKVOUCHERMKEY='" & CurMKey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mRefNo = RsTemp.Fields("VNO").Value & "-" & VB6.Format(RsTemp.Fields("VDATE").Value, "DD/MM/YYYY")
                MsgInformation("This Voucher is Update against Advance Voucher No : " & mRefNo & ", so cann't be delete.")
                Exit Sub
            End If
        End If

        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtPartyName.Text)) = True Then
            Exit Sub
        End If
        If lblBookType.Text = ConCashReceipt Then
            mLockBookCode = CInt(ConLockCashReceipt)
        ElseIf lblBookType.Text = ConCashPayment Then
            mLockBookCode = CInt(ConLockCashPayment)
        ElseIf lblBookType.Text = ConBankReceipt Then
            mLockBookCode = CInt(ConLockBankReceipt)
        ElseIf lblBookType.Text = ConBankPayment Then
            mLockBookCode = CInt(ConLockBankPayment)
        ElseIf lblBookType.Text = ConPDCReceipt Then
            mLockBookCode = CInt(ConLockPDCReceipt)
        ElseIf lblBookType.Text = ConPDCPayment Then
            mLockBookCode = CInt(ConLockPDCPayment)
        Else
            mLockBookCode = CInt(ConLockJournal)
        End If
        If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
            Exit Sub
        End If
        If lblReversalMade.Text = "Y" Then
            If MainClass.ValidateWithMasterTable(lblReversalMkey.Text, "REVERSAL_MKEY", "VNO", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mReversalVoucher = MasterNo
            End If
            MsgBox("Reversal Voucher " & mReversalVoucher & " made against this Voucher, So cann't be Change.", MsgBoxStyle.Information)
            Exit Sub
        End If
        If lblReversalVoucher.Text = "Y" Then
            mReversalVoucher = ""
            If MainClass.ValidateWithMasterTable(lblReversalMkey.Text, "MKEY", "VNO", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mReversalVoucher = MasterNo
            End If
            MsgBox("This is a Reversal Voucher of " & mReversalVoucher & ", So cann't be Change.", MsgBoxStyle.Information)
            Exit Sub
        End If
        With SprdMain
            For ii = 1 To .MaxRows
                .Row = ii
                .Col = ColAccountName
                mAccountCode = Trim(.Text)
                If ValidateAccountLocking(PubDBCn, TxtVDate.Text, mAccountCode) = True Then
                    Exit Sub
                End If
                '            If CheckBillPayment(mAccountCode, txtBillNo.Text, "B") = True Then Exit Sub										
            Next
        End With
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("You Cann't Delete Cancelled Voucher", MsgBoxStyle.Information)
            Exit Sub
        End If


        If RsTRNMain.Fields("Authorised").Value = "Y" And PubSuperUser <> "S" Then
            MsgBox("You Cann't Delete Authorised Voucher", MsgBoxStyle.Information)
            Exit Sub
        End If
        If GetServiceClaimMade((RsTRNMain.Fields("mKey").Value), pClaimNo) = True Then
            MsgInformation("Service Claim No " & pClaimNo & " Made, so Cann't be Deleted.")
            Exit Sub
        End If
        If MainClass.GetUserCanModify(TxtVDate.Text) = False Then
            MsgBox("You Have Not Rights to delete back Voucher", MsgBoxStyle.Information)
            Exit Sub
        End If
        If Trim(txtVno.Text) = "" Then
            MsgBox("Nothing to Delete.", MsgBoxStyle.Information)
            Exit Sub
        End If
        If GetTDSChallanMade((RsTRNMain.Fields("mKey").Value), pTDSChallanNo) = True Then
            MsgInformation("TDS Challan No" & pTDSChallanNo & " Made, so Cann't be Deleted.")
            Exit Sub
        End If
        If MsgQuestion("Want to Delete the Complete Voucher") = CStr(MsgBoxResult.No) Then Exit Sub
        mVnoStr = Trim(txtVType.Text) & txtVNo1.Text & txtVno.Text & txtVNoSuffix.Text

        If MainClass.ValidateWithMasterTable(RsTRNMain.Fields("mKey").Value, "MKEY", "BOOKTYPE", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xBookType = MasterNo
        Else
            MsgInformation("Voucher Not Found.")
            Exit Sub
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "FIN_VOUCHER_HDR", RsTRNMain.Fields("mKey").Value, RsTRNMain, "MKEY", "D") = False Then GoTo DelErrPart
        If InsertIntoDelAudit(PubDBCn, "FIN_VOUCHER_DET", RsTRNMain.Fields("mKey").Value, RsTRNDetail, "MKEY", "D") = False Then GoTo DelErrPart


        If InsertIntoDeleteTrn(PubDBCn, "FIN_VOUCHER_HDR", "MKEY", RsTRNMain.Fields("mKey").Value) = False Then GoTo DelErrPart



        SqlStr = "DELETE FROM FIN_BILLDETAILS_TRN WHERE Mkey='" & RsTRNMain.Fields("mKey").Value & "' AND BookType='" & xBookType & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM FIN_SERVTAXDETAILS_TRN  WHERE Mkey='" & RsTRNMain.Fields("mKey").Value & "' "
        PubDBCn.Execute(SqlStr)
        SqlStr = "DELETE FROM PAY_LOAN_MST WHERE Mkey='" & RsTRNMain.Fields("mKey").Value & "' "
        PubDBCn.Execute(SqlStr)


        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE Mkey='" & RsTRNMain.Fields("mKey").Value & "'" & vbCrLf _
            & " AND BookType='" & xBookType & "'"

        ''& vbCrLf _
        '& " AND BooksubType='" & VB.Right(lblBookType.Text, 1) & "' "
        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM TDS_TRN WHERE Mkey='" & RsTRNMain.Fields("mKey").Value & "' AND BOOKCODE=-1 "
        PubDBCn.Execute(SqlStr)

        SqlStr = "UPDATE FIN_PURCHASE_HDR SET JVT_MKEY='',SECTION_CODE='', JVNO='',ISTDSDEDUCT='N',ISESIDEDUCT='N',ISSTDSDEDUCT='N'" & vbCrLf _
            & " WHERE JVT_MKEY='" & RsTRNMain.Fields("mKey").Value & "'"

        PubDBCn.Execute(SqlStr)




        SqlStr = " DELETE FROM FIN_SalVoucher_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND mKey='" & RsTRNMain.Fields("mKey").Value & "'"
        If Val(lblELYear.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND EL_YEAR=" & Val(lblELYear.Text) & ""
        End If
        PubDBCn.Execute(SqlStr)
        If VB.Left(lblSR.Text, 1) = "F" Then
            If UpdateFullNFinal("N") = False Then GoTo DelErrPart
        End If
        If VB.Left(lblSR.Text, 1) = "Q" Then
            If UpdateVoucherSalary("N") = False Then GoTo DelErrPart
        End If

        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & RsTRNMain.Fields("mKey").Value & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "Delete From FIN_VOUCHER_HDR Where Mkey='" & RsTRNMain.Fields("mKey").Value & "'"
        PubDBCn.Execute(SqlStr)

        If chkSuppBill.CheckState = System.Windows.Forms.CheckState.Checked Then
            mIsCapital = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
            If Val(txtModvatNo.Text) <> 0 Then
                SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='N',JVNO=''," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MODVATNO=" & txtModvatNo.Text & " AND ISCAPITAL='" & mIsCapital & "'  AND VNO='-1'"
                PubDBCn.Execute(SqlStr)
            End If
            '        If Val(txtServiceTaxNo.Text) <> 0 Then										
            '            SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='N',JVNO=''" & vbCrLf _										
            ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _										
            ''                    & " AND FYEAR=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _										
            ''                    & " AND SERVNO=" & txtServiceTaxNo.Text & "  AND VNO='-1'"										
            '            PubDBCn.Execute SqlStr										
            '        End If										
            If Val(txtSTRefundNo.Text) <> 0 Then
                SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='N',JVNO=''," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STCLAIMNO=" & txtSTRefundNo.Text & "  AND VNO='-1'"
                PubDBCn.Execute(SqlStr)
            End If
        End If
        SqlStr = "UPDATE FIN_SUPP_PURCHASE_HDR SET ISFINALPOST='N',JVNO='-1',JVMKEY='-1'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND JVMKEY='" & RsTRNMain.Fields("mKey").Value & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "UPDATE FIN_SUPP_SALE_HDR SET ISFINALPOST='N',JVNO='-1',JVMKEY='-1'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND JVMKEY='" & RsTRNMain.Fields("mKey").Value & "'"
        PubDBCn.Execute(SqlStr)
        With SprdMain
            For ii = 1 To .MaxRows
                .Row = ii
                .Col = ColChequeNo
                mChequeNo = Trim(.Text)
                VMkey = RsTRNMain.Fields("mKey").Value
                If (lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment) And mChequeNo <> "" Then
                    If UpdateChequeDetail(mChequeNo, VMkey, "O", True) = False Then GoTo DelErrPart
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        RsTRNMain.Requery() ''refresh										
        Clear1()
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''										
        RsTRNMain.Requery() ''RsTRNMain.Refresh										
        RsTRNDetail.Requery() ''.Refresh										
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume										
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click
        Dim mIsAuthorisedUser As String
        If cmdModify.Text = ConcmdmodifyCaption Then
            If RsTRNMain.Fields("CANCELLED").Value = "Y" Then
                MsgBox("You Cann't Modify Cancelled Voucher", MsgBoxStyle.Information)
                Exit Sub
            End If
            mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
            If InStr(1, mIsAuthorisedUser, "S") = 0 Then
                If RsTRNMain.Fields("AUTHORISED").Value = "Y" Then
                    MsgBox("You Cann't Modify Authorised Voucher", MsgBoxStyle.Information)
                    Exit Sub
                End If
            End If
            '        If PubSuperUser = "U" Then										
            '            If Trim(UCase(RsTRNMain!ADDUSER)) = Trim(UCase(PubUserID)) Then										
            '                MsgBox "Same User Cann't be Modify Voucher", vbInformation										
            '                Exit Sub										
            '            End If										
            '        End If										
            ADDMode = False
            MODIFYMode = True
            cmdModify.Text = ConCmdCancelCaption
            SprdMain.Enabled = True
            chkSuppBill.Enabled = True
            '        txtModvatNo.Enabled = False										
            '        txtSTRefundNo.Enabled = False										
            txtVno.Enabled = True '' 1/5/2003   IIf(PubUserID = "ADMIN", True, False)			
            TxtVDate.Enabled = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110, True, False)
        Else
            cmdModify.Text = ConcmdmodifyCaption
            ADDMode = False
            MODIFYMode = False
            SprdMain.Enabled = True
            txtVno.Enabled = True
            TxtVDate.Enabled = True
            ''Show1										
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTRNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        cmdAuthorised.Enabled = IIf(mAuthorised = "Y", False, cmdAuthorised.Enabled)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnTrnVoucher(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnTrnVoucher(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnTrnVoucher(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mBranchCode As Integer
        Dim mCategoryCode As Integer
        Dim mVNo As String = ""
        Dim mBookType As String = ""
        Dim mBookSubType As String = ""
        Dim mBookCode As String = ""
        Dim SqlStr As String = ""
        Dim mMultiLine As Boolean
        Dim mRptFileName As String = ""
        Dim cntRow As Integer
        Dim mNarration As String = ""
        Dim mAccountName As String = ""
        Dim mNarrDetail As String = ""
        Dim mChequeNo As String = ""
        Dim mNarrAcct As String = ""
        Dim mDCType As String = ""
        Dim mBankName As String = ""
        Dim mPartyOpBal As String = ""
        Dim pOpBal As Double
        Dim mAccountCode As String = ""
        Dim xAccountName As String = ""
        Dim mParticularAmount As Double
        Dim pLastPaymentAmount As String = ""
        Dim mLCPayment As Boolean
        mLCPayment = False
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        mVNo = Trim(txtVType.Text) & txtVNo1.Text & Trim(txtVno.Text) & Trim(txtVNoSuffix.Text)
        mBookType = lblBookType.Text
        ''if voucher is not Journal..										
        If Me.lblBookType.Text = ConJournal Then
            mBookCode = CStr(ConJournalBookCode)
        ElseIf Me.lblBookType.Text = ConContra Then
            mBookCode = CStr(ConContraBookCode)
        Else
            MainClass.ValidateWithMasterTable(Me.txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mBookCode = MasterNo
        End If
        mSubTitle = ""
        mMultiLine = False
        cntRow = 0
        ''Check multiple entry...										
        With SprdMain
            .Row = 1
            Do While .Row < .MaxRows
                .Col = ColAccountName
                If .Text <> "" Then
                    cntRow = cntRow + 1
                End If
                If .Row = 1 Then
                    mAccountName = .Text
                    .Col = ColParticulars
                    mNarrDetail = MainClass.AllowVBNewLine(.Text)
                    .Col = ColChequeNo
                    mChequeNo = .Text
                    .Col = ColChequeDate
                    mChequeNo = mChequeNo & IIf(Trim(.Text) = "", "", " Dt. " & .Text)
                    .Col = ColAmount
                    mParticularAmount = Val(.Text)
                End If
                If cntRow > 1 Then
                    mMultiLine = True
                    Exit Do
                ElseIf cntRow = 1 Then
                    If GetAccountBalancingMethod(mAccountName, False) = "S" Then
                        mMultiLine = True
                    End If
                End If
                .Row = .Row + 1
            Loop
        End With
        If mMultiLine = True Then
            '         frmPrintVoucher.OptReceipt.Enabled = False										
            frmPrintVoucher.OptVoucher.Checked = True
        End If
        frmPrintVoucher.ShowDialog()
        If G_PrintLedg = False Then
            Exit Sub
        End If
        If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
            Call ReportOnAdviseVoucher(Mode, mVNo, mBookType, mBookCode)
            Exit Sub
        End If
        If frmPrintVoucher.OptReceiptExcel.Checked = True Then
            If IsDate(TxtVDate.Text) Then
                MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mAccountCode = MasterNo
            End If
            Call SelectQryForAdvise(SqlStr, CurMKey, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode, mAccountCode)
            If CreateExcelFile(SqlStr) = False Then GoTo ERR1
            Exit Sub
        End If
        Call MainClass.ClearCRptFormulas(Report1)
        Select Case lblBookType.Text
            Case ConCashReceipt
                mTitle = "Cash Receipt"
                'If frmPrintVoucher.OptReceipt Then										
                mNarrAcct = mAccountName
                mNarration = "Received with thanks a sum of Rs. " & LblNetAmt.Text
                mNarration = mNarration & " ( " & MainClass.RupeesConversion(LblNetAmt.Text)
                mNarration = mNarration & " ) Cash on account of " & mNarrAcct & ""
                'End If										
            Case ConCashPayment
                mTitle = "Cash Payment"
                mNarrAcct = mAccountName
                mNarration = "Received with thanks a sum of Rs. " & LblNetAmt.Text
                mNarration = mNarration & " ( " & MainClass.RupeesConversion(LblNetAmt.Text)
                mNarration = mNarration & " ) Cash on account of " & mNarrAcct & ". "
                mNarration = VB.Left(mNarration & Trim(txtNarration.Text), 250)
            Case ConBankReceipt
                mTitle = "Bank Receipt"
                If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.OptReceiptWithDue.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                    mNarrAcct = mAccountName
                    mNarration = "Received with thanks a sum of Rs. " & LblNetAmt.Text
                    mNarration = mNarration & " ( " & MainClass.RupeesConversion(LblNetAmt.Text)
                    mNarration = mNarration & " ) By Cheque No. " & mChequeNo & " On account of " & mNarrDetail & " "
                    mNarration = mNarration & " as Per detail given below :"
                End If
                mBankName = "Bank : " & txtPartyName.Text
            Case ConBankPayment
                mTitle = "Bank Payment"
                xAccountName = InputBox("Please Enter the Account Name (If Default Leave Blank):", "Advise Statment", "")
                If Trim(xAccountName) <> "" Then
                    mAccountName = xAccountName
                End If
                If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.OptReceiptWithDue.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                    mNarrAcct = mAccountName
                    mSubTitle = "We have pleasure in enclosing herewith our cheque against "
                    mSubTitle = mSubTitle & " your invoice details given below :"
                End If
                mNarration = "Narration : " & Trim(txtNarration.Text)
                mBankName = "Bank : " & txtPartyName.Text
            Case ConPDCReceipt
                mTitle = "Post Dated Receipt"
                If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.OptReceiptWithDue.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                    mNarrAcct = mAccountName
                    mNarration = "Received with thanks a sum of Rs. " & LblNetAmt.Text
                    mNarration = mNarration & " ( " & MainClass.RupeesConversion(LblNetAmt.Text)
                    mNarration = mNarration & " ) By Cheque No. " & mChequeNo & " On account of " & mNarrDetail & " "
                    mNarration = mNarration & " as Per detail given below :"
                End If
                mBankName = "Bank : " & txtPartyName.Text
            Case ConPDCPayment
                mTitle = "Post Dated Payment"
                mNarration = "Narration : " & Trim(txtNarration.Text)
                mBankName = "Bank : " & txtPartyName.Text
            Case ConJournal
                mTitle = "Journal"
                mNarration = "Narration : " & txtNarration.Text '' mNarrDetail										
            Case ConContra
                mTitle = "Contra"
                mNarration = "Narration : " & txtNarration.Text ''mNarrDetail										
        End Select
        mNarration = VB.Left(mNarration, 254)
        If frmPrintVoucher.OptItemRecevied.Checked = True Then
            Call SelectQryForItem(SqlStr, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode)
            mRptFileName = "ItemRecevied.rpt"
            mTitle = "Details of Items Received"
        ElseIf frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
            If IsDate(TxtVDate.Text) Then
                MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mAccountCode = MasterNo
                pOpBal = GetOpeningBal(mAccountCode, (TxtVDate.Text))
                pLastPaymentAmount = GetLastPaymentNDate(mAccountCode, (TxtVDate.Text), mVNo)
            End If
            mPartyOpBal = VB6.Format(System.Math.Abs(pOpBal), "0.00") & IIf(pOpBal >= 0, "Dr", "Cr")
            Call SelectQryForAdvise(SqlStr, CurMKey, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode, mAccountCode)
            If frmPrintVoucher.OptReceipt.Checked = True Then
                mRptFileName = "ReceiptAdvise.rpt"
                mTitle = mTitle & " Advice"
            Else
                mRptFileName = "HundiAdvise.rpt"
                mTitle = "Hundi Advice"
            End If
        ElseIf frmPrintVoucher.OptReceiptWithDue.Checked = True Then
            If IsDate(TxtVDate.Text) Then
                MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mAccountCode = MasterNo
                pOpBal = GetOpeningBal(mAccountCode, (TxtVDate.Text))
                pLastPaymentAmount = GetLastPaymentNDate(mAccountCode, (TxtVDate.Text), mVNo)
            End If
            mPartyOpBal = VB6.Format(System.Math.Abs(pOpBal), "0.00") & IIf(pOpBal >= 0, "Dr", "Cr")
            Call SelectQryForAdvise(SqlStr, CurMKey, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode, mAccountCode)
            mRptFileName = "ReceiptAdviseWithDue.rpt"
            mTitle = mTitle & " Advice"
        ElseIf frmPrintVoucher.OptBankAdvise.Checked = True Then
            mBankName = InputBox("Enclosed :", "Enclosed Detail", "")
            If IsDate(TxtVDate.Text) Then
                MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mAccountCode = MasterNo
            End If
            Call SelectQryForBankAdvise(SqlStr, CurMKey, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode, mAccountCode)
            mRptFileName = "BankAdvise.rpt"
            mTitle = "Subject  >>>  Invoice Discounting"
            mSubTitle = "Please find below the details of the enclosed certified true copies of "
            mSubTitle = mSubTitle & " invoices, for materials received from M/s " & mAccountName
            mSubTitle = mSubTitle & " against supplies of raw material and consumables."
            mNarration = "We request you to please discount the above-mentioned invoices and issue DD/PO/cheque "
            mNarration = mNarration & "in favour of M/s " & mAccountName
            mNarration = mNarration & " or transfer the above amount under RTGS scheme."
        ElseIf frmPrintVoucher.OptDnCn.Checked = True Then
            Call ReportOnDrCr(Crystal.DestinationConstants.crptToWindow)
            frmPrintVoucher.Close()
            Exit Sub
        ElseIf frmPrintVoucher.OptVoucher.Checked = True Then

            If lblBookType.Text = ConJournal Then
                '"UPDATE FIN_LCDISC_HDR SET BANKVOUCHERMKEY='" & pBankVoucherMkey & "' WHERE MKEY='" & MainClass.AllowSingleQuote(LblMKey.text) & "'"										
                If MainClass.ValidateWithMasterTable(CurMKey, "BANKVOUCHERMKEY", "BANKVOUCHERMKEY", "FIN_LCDISC_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mLCPayment = True
                End If
            End If
            If mLCPayment = True Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then
                Call SelectQryForVoucher(SqlStr, CurMKey, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode)
                mRptFileName = "BankVoucher.rpt"
            Else
                Call SelectQryForTRNVoucher(SqlStr, CurMKey, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode)
                mRptFileName = "TrnVoucher.rpt"
            End If
            mPartyOpBal = CStr(0)
            If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Or mLCPayment = True Then
                If IsDate(TxtVDate.Text) Then
                    MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mAccountCode = MasterNo
                    pOpBal = GetOpeningBal(mAccountCode, (TxtVDate.Text))
                    pLastPaymentAmount = GetLastPaymentNDate(mAccountCode, (TxtVDate.Text), mVNo)
                End If
                mPartyOpBal = VB6.Format(System.Math.Abs(pOpBal), "0.00") & IIf(pOpBal >= 0, "Dr", "Cr")
            End If
        ElseIf frmPrintVoucher.optDNVoucher.Checked = True Then
            If lblBookType.Text = ConJournal Then
                Call SelectQryForVoucher(SqlStr, CurMKey, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode)
                mTitle = "Debit Note"
                mRptFileName = "Voucher_DN.rpt"
            End If
        ElseIf frmPrintVoucher.optLoanPrint.Checked = True Then
            If lblBookType.Text = ConBankPayment Then
                Call SelectQryForLoan(SqlStr, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode)
                mTitle = "Employee Loan Statement"
                mRptFileName = "LoanVoucher.rpt"
            End If
        ElseIf frmPrintVoucher.optRTGSLetter.Checked = True Then
            MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mAccountCode = MasterNo
            SqlStr = "SELECT * FROM FIN_SUPP_CUST_MST CMST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            mRptFileName = "RTGSLETTER1.rpt"
            mTitle = "RTGS LETTER"
        End If
        mTitle = mTitle & IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, " (CANCELLED )", "")

        If ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, mNarration, mBankName, mAccountName, mPartyOpBal, pLastPaymentAmount, mChequeNo, mParticularAmount, mLCPayment) = False Then GoTo ERR1

        If Mode = Crystal.DestinationConstants.crptToPrinter Then
            Select Case lblBookType.Text
                Case ConCashReceipt, ConCashPayment, ConBankReceipt, ConBankPayment
                    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
                    If MainClass.ValidateWithMasterTable(RsTRNMain.Fields("mKey").Value, "MKEY", "Authorised", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, "", SqlStr) = True Then
                        If Trim(MasterNo) = "N" Then
                            SqlStr = "UPDATE FIN_VOUCHER_HDR SET Authorised='Y', " & vbCrLf & " AUTHORISED_CODE='" & MainClass.AllowSingleQuote(PubUserID) & "',AUTHORISED_DATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), UPDATE_FROM='H'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & RsTRNMain.Fields("mKey").Value & "'"
                            PubDBCn.Execute(SqlStr)
                            RsTRNMain.Requery()
                        End If
                    End If
            End Select
        End If
        '										
        '    If lblBookType.text = ConCashReceipt Or lblBookType.text = ConBankReceipt Then										
        frmPrintVoucher.Close()
        '    End If										
        Exit Sub
ERR1:
        If Err.Number <> 0 Then
            MsgInformation(Err.Number & " : " & Err.Description)
        End If
        'Resume										
        frmPrintVoucher.Close()
    End Sub
    Private Sub ReportOnAdviseVoucher(ByRef Mode As Crystal.DestinationConstants, ByRef mVNo As String, ByRef mBookType As String, ByRef mBookCode As String)
        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String
        'Dim mBranchCode As Long										
        'Dim mCategoryCode As Long										
        'Dim mVNo As String										
        'Dim mBookType As String										
        'Dim mBookSubType As String										
        'Dim mBookCode As String										
        Dim SqlStr As String = ""
        'Dim mMultiLine As Boolean										
        Dim mRptFileName As String
        Dim cntRow As Integer
        Dim mNarration As String = ""
        Dim mAccountName As String = ""
        Dim mNarrDetail As String = ""
        Dim mChequeNo As String = ""
        Dim mNarrAcct As String
        Dim mDCType As String
        Dim mBankName As String = ""
        Dim mPartyOpBal As String
        Dim pOpBal As Double
        Dim mAccountCode As String = ""
        Dim xAccountName As String = ""
        Dim mParticularAmount As Double
        Dim pLastPaymentAmount As String = ""
        SqlStr = ""
        Select Case lblBookType.Text
            Case ConCashReceipt
                mTitle = "Cash Receipt"
                'If frmPrintVoucher.OptReceipt Then										
                mNarrAcct = mAccountName
                mNarration = "Received with thanks a sum of Rs. " & LblNetAmt.Text
                mNarration = mNarration & " ( " & MainClass.RupeesConversion(LblNetAmt.Text)
                mNarration = mNarration & " ) Cash on account of " & mNarrAcct & ""
                'End If										
            Case ConCashPayment
                mTitle = "Cash Payment"
                mNarrAcct = mAccountName
                mNarration = "Received with thanks a sum of Rs. " & LblNetAmt.Text
                mNarration = mNarration & " ( " & MainClass.RupeesConversion(LblNetAmt.Text)
                mNarration = mNarration & " ) Cash on account of " & mNarrAcct & ". "
                mNarration = VB.Left(mNarration & Trim(txtNarration.Text), 250)
            Case ConBankReceipt
                mTitle = "Bank Receipt"
                If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.OptReceiptWithDue.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                    mNarrAcct = mAccountName
                    mNarration = "Received with thanks a sum of Rs. " & LblNetAmt.Text
                    mNarration = mNarration & " ( " & MainClass.RupeesConversion(LblNetAmt.Text)
                    mNarration = mNarration & " ) By Cheque No. " & mChequeNo & " On account of " & mNarrDetail & " "
                    mNarration = mNarration & " as Per detail given below :"
                End If
                mBankName = "Bank : " & txtPartyName.Text
            Case ConBankPayment
                mTitle = "Bank Payment"
                xAccountName = InputBox("Please Enter the Account Name (If Default Leave Blank):", "Advise Statment", "")
                If Trim(xAccountName) <> "" Then
                    mAccountName = xAccountName
                End If
                If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.OptReceiptWithDue.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                    mNarrAcct = mAccountName
                    mSubTitle = "We have pleasure in enclosing herewith our cheque against "
                    mSubTitle = mSubTitle & " your invoice details given below :"
                End If
                mNarration = "Narration : " & Trim(txtNarration.Text)
                mBankName = "Bank : " & txtPartyName.Text
            Case ConPDCReceipt
                mTitle = "Post Dated Receipt"
                If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.OptReceiptWithDue.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                    mNarrAcct = mAccountName
                    mNarration = "Received with thanks a sum of Rs. " & LblNetAmt.Text
                    mNarration = mNarration & " ( " & MainClass.RupeesConversion(LblNetAmt.Text)
                    mNarration = mNarration & " ) By Cheque No. " & mChequeNo & " On account of " & mNarrDetail & " "
                    mNarration = mNarration & " as Per detail given below :"
                End If
                mBankName = "Bank : " & txtPartyName.Text
            Case ConPDCPayment
                mTitle = "Post Dated Payment"
                mNarration = "Narration : " & Trim(txtNarration.Text)
                mBankName = "Bank : " & txtPartyName.Text
            Case ConJournal
                mTitle = "Journal"
                mNarration = "Narration : " & txtNarration.Text '' mNarrDetail										
            Case ConContra
                mTitle = "Contra"
                mNarration = "Narration : " & txtNarration.Text ''mNarrDetail										
        End Select
        mNarration = VB.Left(mNarration, 254)
        If frmPrintVoucher.OptReceipt.Checked = True Then
            mRptFileName = "ReceiptAdvise.rpt"
            mTitle = mTitle & " Advice"
        Else
            mRptFileName = "HundiAdvise.rpt"
            mTitle = "Hundi Advice"
        End If
        mTitle = mTitle & IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, " (CANCELLED )", "")
        mSubTitle = ""
        cntRow = 0
        ''Check multiple entry...										
        'Call MainClass.ClearCRptFormulas(Report1)										
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColAccountName
                mAccountName = Trim(.Text)
                If mAccountName <> "" Then
                    If GetAccountBalancingMethod(mAccountName, False) = "D" Then
                        If IsDate(TxtVDate.Text) Then
                            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mAccountCode = MasterNo
                                pOpBal = GetOpeningBal(mAccountCode, (TxtVDate.Text))
                            Else
                                mAccountCode = "-1"
                                pOpBal = 0
                            End If
                            pLastPaymentAmount = GetLastPaymentNDate(mAccountCode, (TxtVDate.Text), mVNo)
                        End If
                        mPartyOpBal = VB6.Format(System.Math.Abs(pOpBal), "0.00") & IIf(pOpBal >= 0, "Dr", "Cr")
                        .Col = ColParticulars
                        mNarrDetail = MainClass.AllowVBNewLine(.Text)
                        .Col = ColChequeNo
                        mChequeNo = .Text
                        .Col = ColChequeDate
                        mChequeNo = mChequeNo & IIf(Trim(.Text) = "", "", " Dt. " & .Text)
                        .Col = ColAmount
                        mParticularAmount = Val(.Text)
                        Report1.Reset()
                        MainClass.ClearCRptFormulas(Report1)
                        Call SelectQryForAdvise(SqlStr, CurMKey, mVNo, CDate(TxtVDate.Text), mBookType, mBookCode, mAccountCode)
                        If ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, mNarration, mBankName, mAccountName, mPartyOpBal, pLastPaymentAmount, mChequeNo, mParticularAmount) = False Then GoTo ERR1
                    End If
                End If
            Next
        End With
        Select Case lblBookType.Text
            Case ConCashReceipt, ConCashPayment, ConBankReceipt, ConBankPayment
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
                If MainClass.ValidateWithMasterTable(RsTRNMain.Fields("mKey").Value, "MKEY", "Authorised", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, "", SqlStr) = True Then
                    If Trim(MasterNo) = "N" Then
                        SqlStr = "UPDATE FIN_VOUCHER_HDR SET Authorised='Y', " & vbCrLf & " AUTHORISED_CODE='" & MainClass.AllowSingleQuote(PubUserID) & "',AUTHORISED_DATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), UPDATE_FROM='H'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & RsTRNMain.Fields("mKey").Value & "'"
                        PubDBCn.Execute(SqlStr)
                        RsTRNMain.Requery()
                    End If
                End If
        End Select
        frmPrintVoucher.Close()
        Exit Sub
ERR1:
        If Err.Number <> 0 Then
            MsgInformation(Err.Number & " : " & Err.Description)
        End If
        'Resume										
        frmPrintVoucher.Close()
    End Sub
    Public Function CreateExcelFile(ByRef pSqlStr As String) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Double
        Dim mHeadingline As Integer
        Dim exlobj As Object
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSupplierCode As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mAmount As Double
        Dim mNetAmount As Double
        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly) '', True										
        If RsTemp.EOF = True Then CreateExcelFile = True : Exit Function
        mHeadingline = 1
        exlobj = CreateObject("excel.application")
        exlobj.Visible = True
        exlobj.Workbooks.Add()
        With exlobj.ActiveSheet
            .Cells(mHeadingline, 1).Value = "Supplier Code"
            .Cells(mHeadingline, 1).Font.Name = "Verdana"
            .Cells(mHeadingline, 1).Font.bold = True
            .Cells(mHeadingline, 2).Value = "BILL NO"
            .Cells(mHeadingline, 2).Font.Name = "Verdana"
            .Cells(mHeadingline, 2).Font.bold = True
            .Cells(mHeadingline, 3).Value = "BILL DATE"
            .Cells(mHeadingline, 3).Font.Name = "Verdana"
            .Cells(mHeadingline, 3).Font.bold = True
            .Cells(mHeadingline, 4).Value = "Amount"
            .Cells(mHeadingline, 4).Font.Name = "Verdana"
            .Cells(mHeadingline, 4).Font.bold = True
            mHeadingline = mHeadingline + 1
            Do While RsTemp.EOF = False
                mSupplierCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillDate = IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value)
                mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00")) * IIf(RsTemp.Fields("DC").Value = "C", -1, 1)
                .Cells(mHeadingline, 1).Value = mSupplierCode
                .Cells(mHeadingline, 2).NumberFormat = ""
                .Cells(mHeadingline, 2).Value = "'" & VB6.Format(mBillNo, "")
                .Cells(mHeadingline, 3).Value = VB6.Format(mBillDate, "dd/mmm/YYYY")
                '           .cells(mHeadingline, 3).NumberFormat = "dd/mm/yyyy"										
                '            .Name = Format(Range("A1"), "mm-dd-yy")										
                '            .Name = Format(Range(" & mHeadingline & " & ":" & 3), "dd-mm-yyyy")										
                ''ActiveSheet.Range("A2").NumberFormat = "mmm-yy"										
                .Cells(mHeadingline, 4).Value = VB6.Format(mAmount, "0.00")
                mNetAmount = mNetAmount + mAmount
                mHeadingline = mHeadingline + 1
                RsTemp.MoveNext()
            Loop
            '        .Cells(mHeadingline, 4).Value = mNetAmount										
            '        .Cells(mHeadingline, 4).Font.Name = "Verdana"										
            '        .Cells(mHeadingline, 4).Font.bold = True:										
        End With
        CreateExcelFile = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        CreateExcelFile = False
    End Function
    Private Sub ReportOnDrCr(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        '    mVNo = txtVNoPrefix.Text & Trim(txtVType.Text) & txtVNo.Text & txtVNoSuffix										
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)
        Call SelectQryForDNCNVoucher(SqlStr)
        '    If lblBookType.text = ConDebitNote Then										
        mTitle = "Debit Note"
        '    If frmPrintVoucher.chkPrintType = vbChecked Then										
        '        mRptFileName = "DrNote.rpt"										
        '    Else										
        '        mRptFileName = "DrNote_Plain.rpt"										
        '    End If										
        mRptFileName = "DrNote_GST.rpt"
        '    Else										
        '        mTitle = "Credit Note"										
        '        mRptFileName = "CrNote.rpt"										
        '    End If										
        If SqlStr = "" Then
            MsgBox("Nothing to Print", MsgBoxStyle.Information)
        Else
            Call ShowDNCNReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowDNCNReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer										
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mReceivedBy As String
        Dim mDrCrNo As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    If lblBookType.text = ConCashPayment Then										
        '        mReceivedBy = "Receiver's Signature"										
        '    Else										
        mReceivedBy = " "
        '    End If										
        MainClass.AssignCRptFormulas(Report1, "ReceivedBy=""" & mReceivedBy & """")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        'For SubReport .........										
        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        Report1.Connect = STRRptConn
        Report1.SubreportToChange = ""
        ''										
        'Dim prt As Printer										
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then										
        '    For Each prt In Printers										
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then										
        '            Printer = prt										
        '            Report1.PrinterName = prt.DeviceName										
        '            Report1.PrinterDriver = prt.DriverName										
        '            Report1.PrinterPort = prt.Port										
        '            Exit For										
        '        End If										
        '    Next prt										
        'End If										
        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function SelectQryForDNCNVoucher(ByRef mSqlStr As String) As String
        Dim mDNCnNO As String
        mDNCnNO = GETDNCNNoInVoucher()

        If Trim(mDNCnNO) = "" Then
            SelectQryForDNCNVoucher = ""
            Exit Function
        End If
        mDNCnNO = "(" & mDNCnNO & ")"
        ''SELECT CLAUSE...										
        mSqlStr = " SELECT " & vbCrLf & " IH.VNOPREFIX, IH.VNOSEQ, IH.VNOSUFFIX," & vbCrLf & " IH.VNO, IH.VDATE, IH.PURVNO, IH.PURVDATE," & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, IH.AUTO_KEY_MRR, IH.MRRDATE," & vbCrLf & " IH.DEBITACCOUNTCODE, IH.CREDITACCOUNTCODE," & vbCrLf & " IH.REMARKS, IH.REASON, IH.NARRATION, IH.DNCNTYPE,"
        mSqlStr = mSqlStr & " ID.SUBROWNO, ID.ITEM_CODE, " & vbCrLf & " ID.ITEM_DESC, " & vbCrLf & " ID.ITEM_QTY, ID.ITEM_UOM, " & vbCrLf & " ID.ITEM_RATE, ID.ITEM_AMT, "
        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"
        ''FROM CLAUSE...										
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST"
        ''WHERE CLAUSE...										
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND " & vbCrLf & " IH.BOOKTYPE='" & VB.Left(ConDebitNote, 1) & "'" & vbCrLf & " AND IH.BOOKSUBTYPE='" & VB.Right(ConDebitNote, 1) & "'" & vbCrLf & " AND IH.APPROVED='Y'" & vbCrLf & " AND IH.MKEY=ID.MKEY(+)" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE"
        mSqlStr = mSqlStr & vbCrLf & " AND IH.VNO||':'||IH.FYEAR IN " & mDNCnNO
        ''IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "										
        '    If lblBookType.text = ConDebitNote Then										
        mSqlStr = mSqlStr & vbCrLf & " AND IH.DEBITACCOUNTCODE=CMST.SUPP_CUST_CODE"
        '    Else										
        '        mSqlStr = mSqlStr & vbCrLf & " AND IH.CREDITACCOUNTCODE=CMST.SUPP_CUST_CODE"										
        '    End If										
        ''ORDER CLAUSE...										
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.VNO,ID.SUBROWNO"
        SelectQryForDNCNVoucher = mSqlStr
    End Function
    Private Function ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef mNarration As String, ByRef mBankName As String, ByRef mAccountName As String, ByRef pPartyOpBal As String, ByRef pLastPaymentAmount As String, ByRef mChqNo As String, ByRef mParticularAmount As Double, Optional ByRef mLCPayment As Boolean = False) As Boolean
        'Dim Printer As New Printer										
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mReceivedBy As String
        Dim mDrCrNo As String
        Dim mVoucherAmount As Double
        Dim mRefNo As String
        Dim mRefDate As String
        Dim mReference As String
        Dim mSubject As String
        Dim mTextBody1 As String
        Dim mTextBody2 As String
        Dim mTextBody3 As String
        Dim mBankAccountNo As String
        Dim mPartyBankName As String = ""
        Dim mPartyBankBranch As String = ""
        Dim mBankBranch As String = ""
        Dim mFavourof As String = ""
        Dim mPartyBankAcctNo As String = ""
        Dim mOurBankName As String = ""
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        If frmPrintVoucher.optRTGSLetter.Checked = True Then
            mRefNo = "Ref. " & IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", RsCompany.Fields("COMPANY_SHORTNAME").Value) & "/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY") & "/"
            MainClass.AssignCRptFormulas(Report1, "RefNo=""" & mRefNo & """")
            MainClass.AssignCRptFormulas(Report1, "RefDate=""" & VB6.Format(TxtVDate.Text, "MMMM DD, YYYY") & """")
            mBankAccountNo = ""
            If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "CUST_BANK_ACCT_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mBankAccountNo = MasterNo
            End If
            If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "CUST_BANK_BANK", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mOurBankName = MasterNo
            End If
            If MainClass.ValidateWithMasterTable(Trim(txtPartyName.Text), "SUPP_CUST_NAME", "BANK_BRANCH_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mBankBranch = MasterNo
            End If
            mVoucherAmount = CDbl(VB6.Format(mParticularAmount, "0.00"))
            mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(mVoucherAmount)) = 0, 0, mVoucherAmount)))
            mReference = "Our C.C. A/c No. " & mBankAccountNo
            If MainClass.ValidateWithMasterTable(Trim(mAccountName), "SUPP_CUST_NAME", "CUST_BANK_ACCT_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyBankAcctNo = MasterNo
            End If
            If MainClass.ValidateWithMasterTable(Trim(mAccountName), "SUPP_CUST_NAME", "CUST_BANK_BANK", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyBankName = MasterNo
            End If
            If MainClass.ValidateWithMasterTable(Trim(mAccountName), "SUPP_CUST_NAME", "BANK_BRANCH_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyBankBranch = MasterNo
            End If
            mSubject = "Request for transfer of Rs. " & VB6.Format(mVoucherAmount, "0.00") & "/- under RTGS / NEFT to " & mPartyBankName & " " & mPartyBankBranch
            mFavourof = "In favour of M/s " & RsCompany.Fields("Company_Name").Value & " C.C. A/c No. " & mPartyBankAcctNo
            mTextBody1 = "It is hereby requested to transfer a sum of Rs. " & VB6.Format(mVoucherAmount, "0.00") & "/- ( Rs." & mAmountInword & ") to the following account with " & mPartyBankName & " " & mPartyBankBranch & " under RTGS / NEFT Scheme : -"
            mTextBody2 = "We are enclosing herewith Cheque No. " & mChqNo & " of " & mOurBankName & " " & mBankBranch & "."
            mTextBody3 = "You are requested to please debit our C.C. Account No. " & mBankAccountNo & " & oblige."
            MainClass.AssignCRptFormulas(Report1, "BankName=""" & mOurBankName & """")
            MainClass.AssignCRptFormulas(Report1, "BranchName=""" & mBankBranch & """")
            MainClass.AssignCRptFormulas(Report1, "Favourof=""" & mFavourof & """")
            MainClass.AssignCRptFormulas(Report1, "Reference=""" & mReference & """")
            MainClass.AssignCRptFormulas(Report1, "Subject=""" & mSubject & """")
            MainClass.AssignCRptFormulas(Report1, "TextBody1=""" & mTextBody1 & """")
            MainClass.AssignCRptFormulas(Report1, "TextBody2=""" & mTextBody2 & """")
            MainClass.AssignCRptFormulas(Report1, "TextBody3=""" & mTextBody3 & """")
        Else
            MainClass.AssignCRptFormulas(Report1, "Narration=""" & mNarration & """")
            MainClass.AssignCRptFormulas(Report1, "BankName=""" & mBankName & """")
            If frmPrintVoucher.optDNVoucher.Checked = True Then
                mVoucherAmount = CDbl(VB6.Format(LblDrAmt.Text, "0.00"))
            Else
                mVoucherAmount = GetVoucherNetAmount()
            End If
            If frmPrintVoucher.OptReceipt.Checked = True Or frmPrintVoucher.OptReceiptWithDue.Checked = True Or frmPrintVoucher.optHundiAdvise.Checked = True Then
                '        mDrCrNo = GETDRCRNo										
                MainClass.AssignCRptFormulas(Report1, "AmountPaid=""" & VB6.Format(mVoucherAmount, "0.00") & """")
                MainClass.AssignCRptFormulas(Report1, "CurrBal=""" & pPartyOpBal & " & " & pLastPaymentAmount & """")
                '        MainClass.AssignCRptFormulas Report1, "DrCrNo=""" & mDrCrNo & """"										
            ElseIf frmPrintVoucher.OptVoucher.Checked = True And (lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Or mLCPayment = True) Then
                MainClass.AssignCRptFormulas(Report1, "CurrBal=""" & pPartyOpBal & " & " & pLastPaymentAmount & """")
            End If
            If lblBookType.Text = ConCashPayment Then
                mReceivedBy = "Receiver's Signature"
            Else
                mReceivedBy = " "
            End If
            MainClass.AssignCRptFormulas(Report1, "ReceivedBy=""" & mReceivedBy & """")
            mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(CStr(mVoucherAmount)) = 0, 0, mVoucherAmount)))
            MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
        End If
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        'Dim prt As Printer										
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then										
        '    For Each prt In Printers										
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then										
        '            Printer = prt										
        '            Report1.PrinterName = prt.DeviceName										
        '            Report1.PrinterDriver = prt.DriverName										
        '            Report1.PrinterPort = prt.Port										
        '            Exit For										
        '        End If										
        '    Next prt										
        'End If										
        Report1.Action = 1
        Report1.Reset()
        ShowReport = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        ShowReport = False
    End Function
    Private Function SelectQryForVoucher(ByRef mSqlStr As String, ByRef pMKey As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String) As String

        mSqlStr = " SELECT TRN.VNO,TRN.VDATE,TRN.BOOKTYPE,TRN.BOOKSUBTYPE, " & vbCrLf _
            & " TRN.ACCOUNTCODE,TRN.BOOKCODE,TRN.NARRATION, " & vbCrLf _
            & " TRN.AMOUNT,TRN.DC,TRN.CHEQUENO,TRN.CHQDATE, " & vbCrLf _
            & " TRN.IBRNO,TRN.CLEARDATE, " & vbCrLf _
            & " A.SUPP_CUST_Name,B.SUPP_CUST_NAME, IDIV.DIV_ALIAS " & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
            & " TRN.COMPANY_CODE=A.COMPANY_CODE AND " & vbCrLf _
            & " TRN.AccountCode=A.SUPP_CUST_CODE AND" & vbCrLf _
            & " TRN.COMPANY_CODE=IDIV.COMPANY_CODE AND " & vbCrLf _
            & " TRN.DIV_CODE=IDIV.DIV_CODE AND" & vbCrLf _
            & " TRN.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf _
            & " TRN.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf _
            & " VNO='" & mVNo & "' AND " & vbCrLf _
            & " MKEY='" & pMKey & "' AND " & vbCrLf _
            & " VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
            & " BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf _
            & " BookSubType='" & Mid(mBookType, 2, 1) & "' "

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
            mSqlStr = mSqlStr & vbCrLf & " AND BookCode='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "' "
        End If

        If frmPrintVoucher.optDNVoucher.Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.DC DESC"
        Else
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.SubRowNo, TRN.ACCOUNTCODE"
        End If

        SelectQryForVoucher = mSqlStr
    End Function

    Private Function SelectQryForTRNVoucher(ByRef mSqlStr As String, ByRef pMKey As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String) As String

        mSqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID, FIN_SUPP_CUST_MST A,FIN_SUPP_CUST_MST B, INV_DIVISION_MST IDIV " & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND IH.COMPANY_CODE=A.COMPANY_CODE AND " & vbCrLf _
            & " ID.AccountCode=A.SUPP_CUST_CODE AND" & vbCrLf _
            & " IH.COMPANY_CODE=IDIV.COMPANY_CODE AND " & vbCrLf _
            & " ID.DIV_CODE=IDIV.DIV_CODE AND" & vbCrLf _
            & " IH.COMPANY_CODE=B.COMPANY_CODE(+) AND " & vbCrLf _
            & " IH.BookCode=B.SUPP_CUST_CODE(+) AND " & vbCrLf _
            & " IH.VNO='" & mVNo & "' AND " & vbCrLf _
            & " IH.MKEY='" & pMKey & "' AND " & vbCrLf _
            & " IH.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf _
            & " IH.BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf _
            & " IH.BookSubType='" & Mid(mBookType, 2, 1) & "' "

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If Me.lblBookType.Text <> ConJournal And Me.lblBookType.Text <> ConContra Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.BookCode='" & mBookCode & "' AND " & vbCrLf & " ID.AccountCode<>'" & mBookCode & "' "
        End If

        If frmPrintVoucher.optDNVoucher.Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY ID.DC DESC"
        Else
            mSqlStr = mSqlStr & vbCrLf & " ORDER BY ID.SubRowNo"
        End If

        SelectQryForTRNVoucher = mSqlStr
    End Function

    Private Function SelectQryForLoan(ByRef mSqlStr As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String) As String
        mSqlStr = " SELECT TRN.*, LOAN.*, EMP.* "
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN, PAY_LOAN_MST LOAN, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=LOAN.COMPANY_CODE " & vbCrLf & " AND TRN.MKEY=LOAN.MKEY " & vbCrLf & " AND TRN.EMPCODE=LOAN.EMP_CODE " & vbCrLf & " AND LOAN.COMPANY_CODE=LOAN.COMPANY_CODE" & vbCrLf & " AND LOAN.COMPANY_CODE=EMP.COMPANY_CODE AND LOAN.EMP_CODE=EMP.EMP_CODE"
        mSqlStr = mSqlStr & vbCrLf & " AND VNO='" & mVNo & "' " & vbCrLf & " AND VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND BookType='" & Mid(mBookType, 1, 1) & "' " & vbCrLf & " AND BookSubType='" & Mid(mBookType, 2, 1) & "' "
        '    If Me.lblBookType.text <> ConJournal And Me.lblBookType.text <> ConContra Then										
        '        mSqlStr = mSqlStr & vbCrLf & " AND BookCode='" & mBookCode & "' AND " & vbCrLf _										
        ''                & " AccountCode<>'" & mBookCode & "' "										
        '    End If										
        mSqlStr = mSqlStr & vbCrLf & " ORDER BY TRN.SubRowNo, LOAN.SUBROWNO"
        SelectQryForLoan = mSqlStr
    End Function
    Private Function SelectQryForAdvise(ByRef mSqlStr As String, ByRef pMKey As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String, ByRef mAccountCode As String) As String
        On Error GoTo ErrPart

        mSqlStr = ""

        If InsertTempTable(mVNo, mVDate, mBookType, mAccountCode) = False Then GoTo ErrPart

        mSqlStr = " SELECT TEMP_FIN_PAYMENT.COMPANY_CODE, TEMP_FIN_PAYMENT.FYEAR, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.BILLNO, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.BILLDATE, TEMP_FIN_PAYMENT.BILLAMOUNT, TEMP_FIN_PAYMENT.ADV, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.DNOTE, TEMP_FIN_PAYMENT.CNOTE, TEMP_FIN_PAYMENT.TDS, TEMP_FIN_PAYMENT.PAYMENT, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.BALANCE, TRN.DC, TEMP_FIN_PAYMENT.DCNOTE, ACM.SUPP_CUST_CODE, TEMP_FIN_PAYMENT.ACCOUNTCODE, " & vbCrLf _
            & " ACM.SUPP_CUST_ADDR, ACM.SUPP_CUST_CITY, ACM.SUPP_CUST_STATE, " & vbCrLf _
            & " ACM.SUPP_CUST_PIN,  ACM.SUPP_CUST_PHONE,TRN.CHEQUENO,TRN.CHQDATE,TRN.AMOUNT,TEMP_FIN_PAYMENT.DUEDATE " & vbCrLf _
            & " FROM TEMP_FIN_PAYMENT, FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM " & vbCrLf _
            & " WHERE TEMP_FIN_PAYMENT.UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " AND TRN.COMPANY_CODE=TEMP_FIN_PAYMENT.COMPANY_CODE(+)" & vbCrLf _
            & " AND TRN.FYEAR=TEMP_FIN_PAYMENT.FYEAR(+) " & vbCrLf _
            & " AND TRN.BillNo=TEMP_FIN_PAYMENT.BillNo(+) " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=TEMP_FIN_PAYMENT.ACCOUNTCODE(+) " & vbCrLf _
            & " AND TRN.BillDate=TEMP_FIN_PAYMENT.BillDate(+) " & vbCrLf _
            & " AND TEMP_FIN_PAYMENT.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TEMP_FIN_PAYMENT.ACCOUNTCODE=ACM.SUPP_CUST_CODE"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        mSqlStr = mSqlStr & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "  " & vbCrLf _
            & " AND TRN.BookType='" & Mid(mBookType, 1, 1) & "'" & vbCrLf _
            & " AND TRN.BookSubType='" & Mid(mBookType, 2, 1) & "'" & vbCrLf _
            & " AND TRN.BOOKCODE='" & mBookCode & "'" & vbCrLf _
            & " AND TRN.AccountCode<>'" & mBookCode & "'" & vbCrLf _
            & " AND TRN.VNO='" & mVNo & "'" & vbCrLf _
            & " AND TRN.MKEY='" & pMKey & "'" & vbCrLf _
            & " AND TRN.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        mSqlStr = mSqlStr & vbCrLf & " ORDER BY TEMP_FIN_PAYMENT.BILLDATE,TEMP_FIN_PAYMENT.BILLNO"
        SelectQryForAdvise = mSqlStr
        Exit Function
ErrPart:
        SelectQryForAdvise = ""
    End Function
    Private Function SelectQryForBankAdvise(ByRef mSqlStr As String, ByRef pMKey As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String, ByRef mAccountCode As String) As String
        On Error GoTo ErrPart
        If InsertTempTable(mVNo, mVDate, mBookType, mAccountCode) = False Then GoTo ErrPart

        mSqlStr = " SELECT TEMP_FIN_PAYMENT.COMPANY_CODE, TEMP_FIN_PAYMENT.FYEAR, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.BILLNO, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.BILLDATE, TEMP_FIN_PAYMENT.BILLAMOUNT, TEMP_FIN_PAYMENT.ADV, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.DNOTE, TEMP_FIN_PAYMENT.CNOTE, TEMP_FIN_PAYMENT.TDS, TEMP_FIN_PAYMENT.PAYMENT, " & vbCrLf _
            & " TEMP_FIN_PAYMENT.BALANCE, TEMP_FIN_PAYMENT.DC, TEMP_FIN_PAYMENT.DCNOTE, ACM.SUPP_CUST_CODE, TEMP_FIN_PAYMENT.ACCOUNTCODE, " & vbCrLf _
            & " ACM.SUPP_CUST_ADDR, ACM.SUPP_CUST_CITY, ACM.SUPP_CUST_STATE, " & vbCrLf _
            & " ACM.SUPP_CUST_PIN,  ACM.SUPP_CUST_PHONE,TRN.CHEQUENO,TRN.CHQDATE,TRN.AMOUNT, PH.AUTO_KEY_MRR " & vbCrLf _
            & " FROM TEMP_FIN_PAYMENT , FIN_POSTED_TRN TRN, FIN_PURCHASE_HDR PH, FIN_SUPP_CUST_MST ACM " & vbCrLf _
            & " WHERE TEMP_FIN_PAYMENT.UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " AND TRN.COMPANY_CODE=TEMP_FIN_PAYMENT.COMPANY_CODE(+)" & vbCrLf _
            & " AND TRN.FYEAR=TEMP_FIN_PAYMENT.FYEAR(+) " & vbCrLf _
            & " AND TRN.BillNo=TEMP_FIN_PAYMENT.BillNo(+) " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=TEMP_FIN_PAYMENT.ACCOUNTCODE(+) " & vbCrLf _
            & " AND TRN.BillDate=TEMP_FIN_PAYMENT.BillDate(+) " & vbCrLf _
            & " AND TEMP_FIN_PAYMENT.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TEMP_FIN_PAYMENT.ACCOUNTCODE=ACM.SUPP_CUST_CODE"

        mSqlStr = mSqlStr & vbCrLf _
            & " AND TRN.COMPANY_CODE=PH.COMPANY_CODE " & vbCrLf _
            & " AND TRN.FYEAR=PH.FYEAR" & vbCrLf _
            & " AND TRN.ACCOUNTCODE=PH.SUPP_CUST_CODE" & vbCrLf _
            & " AND TRN.BILLNO=PH.BILLNO"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        mSqlStr = mSqlStr & vbCrLf _
            & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "  " & vbCrLf _
            & " AND TRN.BookType='" & Mid(mBookType, 1, 1) & "'" & vbCrLf _
            & " AND TRN.BookSubType='" & Mid(mBookType, 2, 1) & "'" & vbCrLf _
            & " AND TRN.BOOKCODE='" & mBookCode & "'" & vbCrLf _
            & " AND TRN.AccountCode<>'" & mBookCode & "'" & vbCrLf _
            & " AND TRN.VNO='" & mVNo & "'" & vbCrLf _
            & " AND TRN.MKEY='" & pMKey & "'" & vbCrLf _
            & " AND TRN.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        mSqlStr = mSqlStr & vbCrLf _
            & " ORDER BY TEMP_FIN_PAYMENT.BILLDATE,TEMP_FIN_PAYMENT.BILLNO"

        SelectQryForBankAdvise = mSqlStr
        Exit Function
ErrPart:
        SelectQryForBankAdvise = ""
    End Function
    Private Function InsertTempTable(ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mAccountCode As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        Dim mDrCrNo As String
        Dim mDueDate As String
        Dim mPurVNo As String
        Dim mPurVDate As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM TEMP_FIN_PAYMENT NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        mDrCrNo = Mid(GETDRCRNo(mAccountCode), 1, 1000)

        If frmPrintVoucher.OptReceiptWithDue.Checked = True Then
            mDueDate = "GETBILLDUEDATE(TRN.COMPANY_CODE, TRN.FYEAR, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE)"
        Else
            mDueDate = "''"
        End If

        If frmPrintVoucher.optHundiAdvise.Checked = True Then
            mPurVNo = "GETVOUCHERNO(TRN.COMPANY_CODE, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE)"
            mPurVDate = "GETVOUCHERDATE(TRN.COMPANY_CODE, TRN.ACCOUNTCODE, TRN.BILLNO, TRN.BILLDATE)"
        Else
            mPurVNo = "''"
            mPurVDate = "''"
        End If

        mSqlStr = "Select '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf _
            & " TRN.COMPANY_CODE,  TRN.FYEAR, TRN.ACCOUNTCODE, " & vbCrLf _
            & " BillNo,  BillDate, " & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)) ," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount))," & vbCrLf _
            & " CASE WHEN SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount) >=0 THEn 'DR' ELSE 'CR' END, " & vbCrLf _
            & " '" & mDrCrNo & "'," & mDueDate & ", " & mPurVNo & ", " & mPurVDate & " " & vbCrLf _
            & " FROM FIN_POSTED_TRN TRN " & vbCrLf _
            & " Where FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND AccountCode='" & mAccountCode & "'"

        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        mSqlStr = mSqlStr & vbCrLf _
            & " GROUP BY BillNo, BillDate,COMPANY_CODE,FYEAR,ACCOUNTCODE " & vbCrLf _
            & " ORDER BY COMPANY_CODE,FYEAR , BillNo, BillDate"

        SqlStr = "INSERT INTO TEMP_FIN_PAYMENT (" & vbCrLf & " USERID, COMPANY_CODE, FYEAR, ACCOUNTCODE," & vbCrLf _
            & " BillNo, BillDate, BILLAMOUNT," & vbCrLf & " ADV, DNOTE, CNOTE, TDS, " & vbCrLf _
            & " PAYMENT,BALANCE, DC,DCNOTE,DUEDATE,VNO, VDATE ) " & vbCrLf & mSqlStr

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        InsertTempTable = True
        Exit Function
ErrPart:
        InsertTempTable = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function SelectQryForItem(ByRef mSqlStr As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String) As String
        On Error GoTo ErrPart
        Dim pSqlStr As String
        Dim cntRow As Integer
        Dim pSupplierName As String
        Dim pSupplierCode As String
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBillNo As String
        Dim mBillDate As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        pSqlStr = "DELETE FROM TEMP_FIN_ITEMRECD WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(pSqlStr)
        With SprdMain
            For cntRow = 1 To .MaxRows
                pSupplierCode = ""
                .Row = cntRow
                .Col = ColAccountName
                pSupplierName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(pSupplierName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pSupplierCode = MasterNo
                Else
                    pSupplierCode = ""
                End If
                If pSupplierCode <> "" Then
                    xSqlStr = " SELECT BILLNO, BILLDATE FROM  FIN_POSTED_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.VNO='" & mVNo & "'" & vbCrLf & " AND TRN.BookType='" & Mid(mBookType, 1, 1) & "'" & vbCrLf & " AND TRN.BookSubType='" & Mid(mBookType, 2, 1) & "'" & vbCrLf & " AND TRN.BOOKCODE='" & mBookCode & "'" & vbCrLf & " AND TRN.AccountCode='" & pSupplierCode & "'" & vbCrLf & " AND TRN.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        Do While RsTemp.EOF = False
                            mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                            mBillDate = IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value)
                            pSqlStr = "INSERT INTO TEMP_FIN_ITEMRECD " & vbCrLf & " (UserId, BILLNO, INVOICE_DATE, DUEDATE, " & vbCrLf & " ITEM_QTY, ITEM_RATE, ITEM_DESC, SUPP_CUST_NAME) "
                            pSqlStr = pSqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.PAYMENTDATE, ID.ITEM_QTY, " & vbCrLf & " ID.ITEM_RATE, ID.ITEM_CODE || '-' || ID.ITEM_DESC, " & vbCrLf & " ACM.SUPP_CUST_NAME "
                            If VB.Right(mBookType, 1) = "P" Or CDbl(mBookCode) = ConJournalBookCode Then
                                pSqlStr = pSqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST ACM"
                            Else
                                pSqlStr = pSqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST ACM"
                            End If
                            pSqlStr = pSqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                            pSqlStr = pSqlStr & vbCrLf & " AND IH.Company_Code =ACM.Company_Code " & vbCrLf & " AND IH.SUPP_CUST_CODE =ACM.SUPP_CUST_CODE " & vbCrLf & " AND IH.MKey=ID.MKey " & vbCrLf & " AND IH.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(pSupplierCode) & "'"
                            pSqlStr = pSqlStr & vbCrLf & " AND IH.BILLNO ='" & MainClass.AllowSingleQuote(mBillNo) & "'" & vbCrLf & " AND IH.INVOICE_DATE =TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                            PubDBCn.Execute(pSqlStr)
                            RsTemp.MoveNext()
                        Loop
                    End If
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        mSqlStr = "SELECT " & vbCrLf & " UserId, BILLNO, INVOICE_DATE, DUEDATE, " & vbCrLf & " ITEM_QTY, ITEM_RATE, ITEM_DESC, SUPP_CUST_NAME " & vbCrLf & " FROM TEMP_FIN_ITEMRECD" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " ORDER BY ITEM_DESC,INVOICE_DATE,BILLNO"
        SelectQryForItem = mSqlStr
        Exit Function
ErrPart:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Function
    Private Function SelectQryForItem1(ByRef mSqlStr As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String) As String
        On Error GoTo ErrPart
        Dim pSqlStr As String
        Dim cntRow As Integer
        Dim pSupplierName As String
        Dim pSupplierCode As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        pSqlStr = "DELETE FROM TEMP_FIN_ITEMRECD WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(pSqlStr)
        With SprdMain
            For cntRow = 1 To .MaxRows
                pSupplierCode = ""
                .Row = cntRow
                .Col = ColAccountName
                pSupplierName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(pSupplierName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pSupplierCode = MasterNo
                Else
                    pSupplierCode = ""
                End If
                If pSupplierCode <> "" Then
                    pSqlStr = "INSERT INTO TEMP_FIN_ITEMRECD " & vbCrLf & " (UserId, BILLNO, INVOICE_DATE, DUEDATE, " & vbCrLf & " ITEM_QTY, ITEM_RATE, ITEM_DESC, SUPP_CUST_NAME) "
                    pSqlStr = pSqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.PAYMENTDATE, ID.ITEM_QTY, " & vbCrLf & " ID.ITEM_RATE, ID.ITEM_CODE || '-' || ID.ITEM_DESC, " & vbCrLf & " ACM.SUPP_CUST_NAME "
                    If VB.Right(mBookType, 1) = "P" Or CDbl(mBookCode) = ConJournalBookCode Then
                        pSqlStr = pSqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST ACM, FIN_POSTED_TRN TRN"
                    Else
                        pSqlStr = pSqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_MST ACM,FIN_POSTED_TRN TRN"
                    End If
                    pSqlStr = pSqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                    '                    pSqlStr = pSqlStr & vbCrLf & " AND IH.FYEAR IN (" & RsCompany.fields("FYEAR").value - 1 & "," & RsCompany.fields("FYEAR").value & ")"										
                    pSqlStr = pSqlStr & vbCrLf & " AND IH.Company_Code =ACM.Company_Code " & vbCrLf & " AND IH.SUPP_CUST_CODE =ACM.SUPP_CUST_CODE " & vbCrLf & " AND IH.MKey=ID.MKey " & vbCrLf & " AND IH.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(pSupplierCode) & "'"
                    pSqlStr = pSqlStr & vbCrLf & " AND IH.BILLNO || IH.INVOICE_DATE IN ( "
                    pSqlStr = pSqlStr & vbCrLf & " SELECT BILLNO||BILLDATE FROM  FIN_POSTED_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
                    pSqlStr = pSqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.VNO='" & mVNo & "'" & vbCrLf & " AND TRN.BookType='" & Mid(mBookType, 1, 1) & "'" & vbCrLf & " AND TRN.BookSubType='" & Mid(mBookType, 2, 1) & "'" & vbCrLf & " AND TRN.BOOKCODE='" & mBookCode & "'" & vbCrLf & " AND TRN.AccountCode='" & pSupplierCode & "'" & vbCrLf & " AND TRN.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                    PubDBCn.Execute(pSqlStr)
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        mSqlStr = "SELECT " & vbCrLf & " UserId, BILLNO, INVOICE_DATE, DUEDATE, " & vbCrLf & " ITEM_QTY, ITEM_RATE, ITEM_DESC, SUPP_CUST_NAME " & vbCrLf & " FROM TEMP_FIN_ITEMRECD" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " ORDER BY ITEM_DESC,INVOICE_DATE,BILLNO"
        SelectQryForItem1 = mSqlStr
        Exit Function
ErrPart:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Function
    Private Function SelectQryForItemOld(ByRef mSqlStr As String, ByRef mVNo As String, ByRef mVDate As Date, ByRef mBookType As String, ByRef mBookCode As String) As String
        On Error GoTo ErrPart
        Dim pSqlStr As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        pSqlStr = "DELETE FROM TEMP_FIN_ITEMRECD WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(pSqlStr)
        pSqlStr = "INSERT INTO TEMP_FIN_ITEMRECD " & vbCrLf & " (UserId, BILLNO, INVOICE_DATE, DUEDATE, " & vbCrLf & " ITEM_QTY, ITEM_RATE, ITEM_DESC, SUPP_CUST_NAME) "
        pSqlStr = pSqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " PURCHMAIN.BILLNO, " & vbCrLf & " PURCHMAIN.INVOICE_DATE, " & vbCrLf & " TRN.DUEDATE, " & vbCrLf & " PURCHDETAIL.ITEM_QTY, " & vbCrLf & " PURCHDETAIL.ITEM_RATE, " & vbCrLf & " PURCHDETAIL.ITEM_DESC, " & vbCrLf & " ACM.SUPP_CUST_NAME "
        pSqlStr = pSqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN ,FIN_SUPP_CUST_MST ACM, " & vbCrLf & " FIN_PURCHASE_HDR PURCHMAIN, FIN_PURCHASE_DET PURCHDETAIL"
        pSqlStr = pSqlStr & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " TRN.COMPANY_CODE=PURCHMAIN.COMPANY_CODE AND " & vbCrLf & " TRN.FYEAR=PURCHMAIN.FYEAR AND " & vbCrLf & " TRN.AccountCode=PURCHMAIN.SUPP_CUST_CODE AND " & vbCrLf & " TRN.AccountCode =ACM.SUPP_CUST_CODE AND TRN.Company_Code =ACM.Company_Code AND " & vbCrLf & " TRN.BILLNO=PURCHMAIN.BILLNO AND " & vbCrLf & " PURCHMAIN.MKey=PURCHDETAIL.MKey AND"
        pSqlStr = pSqlStr & vbCrLf & " TRN.VNO='" & mVNo & "' AND " & vbCrLf & " TRN.VDate=TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf & " TRN.BookType='" & Mid(mBookType, 1, 1) & "' AND " & vbCrLf & " TRN.BookSubType='" & Mid(mBookType, 2, 1) & "' AND " & vbCrLf & " TRN.BOOKCODE='" & mBookCode & "' AND " & vbCrLf & " TRN.AccountCode<>'" & mBookCode & "' "
        pSqlStr = pSqlStr & vbCrLf & " AND TRN.BILLNO IN ( " & vbCrLf & " SELECT BILLNO FROM FIN_POSTED_TRN" & vbCrLf & " WHERE FIN_POSTED_TRN.AccountCode=Acm.SUPP_CUST_Code AND  FIN_POSTED_TRN.Company_Code=Acm.Company_Code" & vbCrLf & " AND FIN_POSTED_TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FIN_POSTED_TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND FIN_POSTED_TRN.BILLNO=PURCHMAIN.BILLNO " & vbCrLf & " AND FIN_POSTED_TRN.BILLDATE=PURCHMAIN.INVOICE_DATE " & vbCrLf & " GROUP BY FIN_POSTED_TRN.BILLNO" & vbCrLf & " HAVING " & vbCrLf & " SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount) + SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)=0 )"
        PubDBCn.Execute(pSqlStr)
        PubDBCn.CommitTrans()
        mSqlStr = "SELECT " & vbCrLf & " UserId, BILLNO, INVOICE_DATE, DUEDATE, " & vbCrLf & " ITEM_QTY, ITEM_RATE, ITEM_DESC, SUPP_CUST_NAME " & vbCrLf & " FROM TEMP_FIN_ITEMRECD" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " ORDER BY ITEM_DESC,INVOICE_DATE,BILLNO"
        SelectQryForItemOld = mSqlStr
        Exit Function
ErrPart:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo SaveErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mVNo As String
        If PubUserID = "A00001" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If FieldsVerification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            If chkChqDeposit.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                txtVNo_Validating(txtVno, New System.ComponentModel.CancelEventArgs(False))
            Else
                Clear1()
                SqlStr = " Select VNO From FIN_VOUCHER_HDR WHERE " & vbCrLf & " MKEY='" & CurMKey & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    mVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                    MsgBox("PDC is Normalization New Voucher No Is " & mVNo, MsgBoxStyle.Information)
                End If
            End If
        Else
            MsgInformation("Record not Saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
SaveErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ErrPart
        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow
            .Col = ColPRRowNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColDC
            .CellType = SS_CELL_TYPE_EDIT
            '        If FormLoaded = False Then										
            If lblBookType.Text = ConCashReceipt Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCReceipt Then
                .Text = "Cr"
            ElseIf lblBookType.Text = ConCashPayment Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
                .Text = "Dr"
            End If
            '        End If										
            .set_ColWidth(ColDC, 2.4)

            .Col = ColAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .set_ColWidth(ColAccountName, 35)


            .ColsFrozen = ColAccountName

            .Col = ColParticulars
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsTRNDetail.Fields("PARTICULARS").DefinedSize ''										
            .set_ColWidth(ColParticulars, 30)

            .Col = ColChequeNo
            .TypeEditLen = RsTRNDetail.Fields("ChequeNo").DefinedSize ''										
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColChequeNo, 8)
            If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConContra Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = ColChequeDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColChequeDate, 8)
            If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConContra Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .Col = ColExp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColExp, 5)
            .Col = ColCC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCC, 5)
            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColDept, 4)
            .Col = ColDivisionCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharSet = FPSpreadADO.TypeEditCharSetConstants.TypeEditCharSetNumeric
            .set_ColWidth(ColDivisionCode, 4)
            .Col = ColEmp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmp, 5)
            .Col = ColIBRNo
            .TypeEditLen = RsTRNDetail.Fields("IBRNo").DefinedSize ''										
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 12)

            .Col = ColSAC
            .TypeEditLen = RsTRNDetail.Fields("SAC").DefinedSize ''										
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False

            .ColHidden = True

            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = True

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = True

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = True

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = True

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = True

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = True

            .Col = ColSaleBillPrefix
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True
            .Col = ColSaleBillSeq
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True
            .Col = ColSaleBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True
            .Col = ColSaleBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True

            .Col = ColClearDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 10
            .ColHidden = True

        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSaleBillPrefix, ColClearDate)
        Exit Sub
ErrPart:
        'Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdMainGST(ByRef Arow As Integer)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount)
        Exit Sub
ErrPart:
        'Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchName()
    End Sub
    Private Sub cmdServProvided_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdServProvided.Click
        Call SearchProvidedMaster()
    End Sub
    Private Sub cmdTDSHide_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTDSHide.Click
        FraTDSFrame.Visible = Not FraTDSFrame.Visible
        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDC)
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh										
            SprdView.Refresh()
            FormatSprdView()
            SprdView.Focus()
            fraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            fraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTRNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub frmAtrn_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmAtrn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        If ADDMode = True Or MODIFYMode = True Then
            If KeyAscii = System.Windows.Forms.Keys.Escape Then CmdClose_Click(cmdClose, New System.EventArgs())
        End If
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmAtrn_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection										
        'PvtDBCn.Open StrConn			


        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Call SetMainFormCordinate(Me)
        ADDMode = False
        MODIFYMode = False
        FormLoaded = False
        ShowSalary = False
        mAuthorised = "N"
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pMYMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        lblBookBalAmt.Text = "0.00"
        lblBookBalDC.Text = ""
        TxtVDate.Text = IIf(TxtVDate.Text = "", RunDate, TxtVDate.Text)
        ChkACPayee.CheckState = System.Windows.Forms.CheckState.Checked
        If lblBookType.Text = ConJournal Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
            ssTab.Visible = True
        Else
            ssTab.Visible = False
        End If
        ssTab.SelectedIndex = 0
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MainClass.SetControlsColor(Me)
        Call frmAtrn_Activated(eventSender, eventArgs)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume										
    End Sub
    Public Sub frmAtrn_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SprdMain.Refresh()
        If FormLoaded = True Then Exit Sub
        FormLoaded = True
        SqlStr = "Select * From FIN_VOUCHER_HDR Where 1=2 "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNMain, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = "Select * From FIN_VOUCHER_DET Where 1=2 "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNDetail, ADODB.LockTypeEnum.adLockReadOnly)
        FormatSprdMain(-1)
        AssignGrid(False)
        InitialiseTRN()
        SetTextLengths()
        '    CalcAccountBal										
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        Dim RsTDSDetail As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        SqlStr = "SELECT * FROM TDS_TRN WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)
        txtVno.MaxLength = RsTRNMain.Fields("VNoSeq").Precision '' .Precision     ''										
        txtVNoSuffix.MaxLength = RsTRNMain.Fields("VNOSUFFIX").DefinedSize ''										
        txtVNo1.MaxLength = RsTRNMain.Fields("VNOPREFIX").DefinedSize ''										
        txtVType.MaxLength = RsTRNMain.Fields("VTYPE").DefinedSize ''										
        txtPartyName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn)
        txtNarration.MaxLength = RsTRNMain.Fields("NARRATION").DefinedSize ''										
        TxtTDSAccount.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtAmountPaid.MaxLength = RsTDSDetail.Fields("AMOUNTPAID").DefinedSize ''										
        txtAmountPaid.MaxLength = RsTDSDetail.Fields("AMOUNTPAID").DefinedSize ''										
        txtPName.MaxLength = RsTDSDetail.Fields("PARTYNAME").DefinedSize ''										
        txtVD.MaxLength = 10
        txtSection.MaxLength = MainClass.SetMaxLength("Name", "TDS_Section_MST", PubDBCn)
        txtTDSAmount.MaxLength = RsTDSDetail.Fields("TDSAMOUNT").Precision ''										
        txtTdsRate.MaxLength = RsTDSDetail.Fields("TDSRATE").Precision ''										
        txtExempted.MaxLength = RsTDSDetail.Fields("EXEPTIONCNO").DefinedSize ''										
        txtJVTDSRate.MaxLength = RsTRNMain.Fields("TDSPer").Precision ''										
        txtJVTDSAmount.MaxLength = RsTRNMain.Fields("TDSAMOUNT").Precision ''										
        txtESIRate.MaxLength = RsTRNMain.Fields("ESIPER").Precision ''										
        txtESIAmount.MaxLength = RsTRNMain.Fields("ESIAMOUNT").Precision ''										
        txtSTDSRate.MaxLength = RsTRNMain.Fields("STDSPER").Precision ''										
        txtSTDSAmount.MaxLength = RsTRNMain.Fields("STDSAMOUNT").Precision ''										
        txtJVVNO.MaxLength = RsTRNMain.Fields("JVNO").DefinedSize ''										
        txtImpPartyName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn)
        txtImpMRRNo.MaxLength = RsTRNMain.Fields("IMP_MRR_NO").Precision
        txtImpBillNo.MaxLength = RsTRNMain.Fields("IMP_BILL_NO").DefinedSize
        txtImpBillDate.MaxLength = 10
        txtExpPartyName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn)
        txtExpBillNo.MaxLength = RsTRNMain.Fields("EXP_BILL_NO").Precision
        txtExpBillDate.MaxLength = 10
        txtServProvided.MaxLength = MainClass.SetMaxLength("NAME", "FIN_SERVPROV_MST", PubDBCn)
        txtServiceOn.MaxLength = RsTRNMain.Fields("SERVICE_ON_AMT").Precision
        txtServiceTaxPer.MaxLength = RsTRNMain.Fields("SERVICE_TAX_PER").Precision
        txtServiceTaxAmount.MaxLength = RsTRNMain.Fields("SERVICE_TAX_AMOUNT").Precision
        txtProviderPer.MaxLength = RsTRNMain.Fields("SERV_PROVIDER_PER").Precision
        txtRecipientPer.MaxLength = RsTRNMain.Fields("SERV_RECIPIENT_PER").Precision
        '    txtServTaxPer.MaxLength = RsTRNMain.Fields("SERVICE_PER").Precision										
        '    txtCESSPer.MaxLength = RsTRNMain.Fields("CESS_PER").Precision										
        '    txtServProvided.MaxLength = RsTRNMain.Fields("SERV_PROV").DefinedSize										
        Exit Sub
ERR1:
        '    Resume										
        ErrorMsg(Err.Description)
    End Sub
    Private Sub InitialiseTRN()
        Dim SqlStr As String = ""
        Select Case lblBookType.Text
            Case ConCashReceipt
                lblAccount.Text = "Cash A/c"
                Me.Text = "Cash Receipt"
                chkPnL.Visible = False
            Case ConCashPayment
                lblAccount.Text = "Cash A/c"
                Me.Text = "Cash Payment"
                chkPnL.Visible = False
            Case ConBankReceipt
                lblAccount.Text = "Bank"
                Me.Text = "Bank Receipt"
                chkPnL.Visible = False
            Case ConBankPayment
                lblAccount.Text = "Bank"
                Me.Text = "Bank Payment"
                chkPnL.Visible = False
            Case ConPDCReceipt
                lblAccount.Text = "PDC"
                Me.Text = "PDC Receipt"
                chkPnL.Visible = False
            Case ConPDCPayment
                lblAccount.Text = "PDC"
                Me.Text = "PDC Payment"
                chkPnL.Visible = False
            Case ConContra
                lblAccount.Text = "Contra"
                Me.Text = "Contra"
                lblAccount.Visible = False
                txtPartyName.Visible = False
                cmdsearch.Visible = False
                chkPnL.Visible = False
            Case ConJournal
                lblAccount.Text = "Type"
                Me.Text = "Journal"
                lblAccount.Visible = False
                txtPartyName.Visible = False
                cmdsearch.Visible = False
                chkPnL.Visible = True
        End Select
        If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
            cmdBillDetail.Enabled = True
            cmdBillDetail.Text = "Cheque"
            ChkACPayee.Visible = True
        Else
            ChkACPayee.Visible = False
            cmdBillDetail.Enabled = False
        End If
        If lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then
            chkChqDeposit.Visible = True
        Else
            chkChqDeposit.Visible = False
        End If
        If lblBookType.Text = ConJournal Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
            ssTab.Visible = True
        Else
            ssTab.Visible = False
        End If
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        SqlStr = "Select TO_CHAR(VDATE,'DD/MM/YYYY') as VDate, " & vbCrLf & " VNOPREFIX as VNoPrefix, VTYPE AS VType, " & vbCrLf & " To_CHAR(VnoSeq) as VNoSeq, DECODE(CANCELLED,'Y','<<CANCELLED>>',Vno) as VNo, " & vbCrLf & " VNOSUFFIX as VNoSuffix, FIN_SUPP_CUST_MST.SUPP_CUST_NAME AS  Account_Name, "
        Select Case lblBookType.Text
            Case ConBankPayment, ConBankReceipt, ConPDCPayment, ConPDCReceipt
                SqlStr = SqlStr & vbCrLf & " B.SUPP_CUST_Name as BankName, " & vbCrLf & " FIN_VOUCHER_DET.Amount  as Amount " & vbCrLf & " From FIN_VOUCHER_HDR,FIN_VOUCHER_DET , FIN_SUPP_CUST_MST , FIN_SUPP_CUST_MST B  " & vbCrLf & " WHERE " & vbCrLf & " FIN_VOUCHER_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FIN_VOUCHER_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND FIN_VOUCHER_HDR.BookType='" & VB.Left(lblBookType.Text, 1) & "' " & vbCrLf & " AND FIN_VOUCHER_HDR.BookSubType='" & VB.Right(lblBookType.Text, 1) & "' " & vbCrLf & " AND FIN_VOUCHER_HDR.Mkey=FIN_VOUCHER_DET.Mkey " & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_VOUCHER_HDR.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_CODE=FIN_VOUCHER_DET.AccountCode " & vbCrLf & " AND FIN_VOUCHER_HDR.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND FIN_VOUCHER_HDR.BookCode=B.SUPP_CUST_CODE "
            Case Else
                SqlStr = SqlStr & vbCrLf & " '' as BankName, FIN_VOUCHER_DET.Amount  as Amount " & vbCrLf & " FROM FIN_VOUCHER_HDR,FIN_VOUCHER_DET, FIN_SUPP_CUST_MST  " & vbCrLf & " WHERE " & vbCrLf & " FIN_VOUCHER_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FIN_VOUCHER_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND FIN_VOUCHER_HDR.BookType='" & VB.Left(lblBookType.Text, 1) & "' " & vbCrLf & " AND FIN_VOUCHER_HDR.BookSubType='" & VB.Right(lblBookType.Text, 1) & "' " & vbCrLf & " AND FIN_VOUCHER_HDR.Mkey=FIN_VOUCHER_DET.Mkey " & vbCrLf & " AND FIN_SUPP_CUST_MST.COMPANY_CODE=FIN_VOUCHER_HDR.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_CODE=FIN_VOUCHER_DET.AccountCode"
        End Select
        SqlStr = SqlStr & vbCrLf & " ORDER BY TO_DATE(VDATE,'DD/MM/YYYY'), VNO,FIN_VOUCHER_DET.SUBROWNO"
        FormatSprdView()
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Exit Sub
ERR1:
        'Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1100)
            .set_ColWidth(2, 0)
            .set_ColWidth(3, 0)
            .set_ColWidth(4, 0)
            .set_ColWidth(5, 1300)
            .set_ColWidth(6, 0)
            If VB.Left(lblBookType.Text, 1) = "B" Or VB.Left(lblBookType.Text, 1) = "F" Then
                .set_ColWidth(7, 2500)
                .set_ColWidth(8, 2350)
                .set_ColWidth(9, 1200)
            Else
                .set_ColWidth(7, 4850)
                .set_ColWidth(8, 0)
                .set_ColWidth(9, 1200)
            End If
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 450)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle										
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub frmAtrn_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then										
        '        'PvtDBCn.Close										
        '        'Set PvtDBCn = Nothing										
        '    End If										
        'Cancel = 0										
        If ADDMode = True Or MODIFYMode = True Then
            If MsgQuestion("Are you sure to Exit") = CStr(MsgBoxResult.No) Then
                'Cancel = 1										
                Exit Sub
            End If
        End If
        RsTRNMain.Close()
        RsTRNMain = Nothing
        RsTRNDetail.Close()
        RsTRNDetail = Nothing
        '    Unload frmPaymentDetail										
        '    Unload frmViewOuts										
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        On Error GoTo ERR1
        Dim mClearDate As String

        Select Case eventArgs.col
            Case 0
                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColClearDate
                mClearDate = SprdMain.Text

                If IsDate(mClearDate) Or mClearDate <> "" Then
                    MsgInformation("Cheque Already Reconciled , So cann't be Delete or add new Row.")
                    Exit Sub
                End If
                If eventArgs.row > 0 And SprdMain.Enabled = True Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColAccountName)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    CalcTots()
                End If
            Case ColAccountName, ColCC, ColDept, ColEmp, ColExp, ColDivisionCode, ColSAC
                If eventArgs.row = 0 Then NameSearch(eventArgs.col, (SprdMain.ActiveRow))
            Case ColChequeNo
                If eventArgs.row = 0 Then ChequeSearch(eventArgs.col, (SprdMain.ActiveRow))
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub NameSearch(ByRef Col As Integer, ByRef Row As Integer)
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mString As String = ""
        Dim mTableName As String = ""
        Dim mFieldName1 As String = ""
        Dim mFieldName2 As String = ""
        Dim mDeptCode As String = ""
        SprdMain.Row = Row
        SprdMain.Col = Col
        mString = SprdMain.Text
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Select Case Col
            Case ColAccountName
                SqlStr = SqlStr & " AND STATUS='O'"
                '            If lblBookType.text = ConContra Then										
                '                SqlStr = SqlStr & " AND SUPP_CUST_TYPE IN ('1','2')"										
                '            Else										
                '                SqlStr = SqlStr & " AND SUPP_CUST_TYPE NOT IN ('2','1')"										
                '            End If										
                mTableName = "FIN_SUPP_CUST_MST"
                mFieldName1 = "SUPP_CUST_NAME"
                mFieldName2 = "SUPP_CUST_CODE"
            Case ColExp
                If ADDMode = True Then
                    SqlStr = SqlStr & " AND STATUS='O'"
                End If
                mTableName = "CST_CENTER_MST"
                mFieldName1 = "COST_CENTER_CODE"
                mFieldName2 = "COST_CENTER_DESC"
            Case ColCC
                mTableName = "FIN_CCENTER_HDR"
                mFieldName1 = "CC_CODE"
                mFieldName2 = "CC_DESC"
            Case ColDept
                mTableName = "PAY_DEPT_MST"
                mFieldName1 = "DEPT_CODE"
                mFieldName2 = "DEPT_DESC"
            Case ColDivisionCode
                mTableName = "INV_DIVISION_MST"
                mFieldName1 = "DIV_CODE"
                mFieldName2 = "DIV_DESC"
            Case ColEmp
                mTableName = "PAY_EMPLOYEE_MST"
                mFieldName1 = "EMP_CODE"
                mFieldName2 = "EMP_NAME"
            Case ColSAC
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'"
                mTableName = "GEN_HSN_MST"
                mFieldName1 = "HSN_CODE"
                mFieldName2 = "HSN_DESC"
        End Select
        If Col = ColAccountName Then
            MainClass.SearchGridMaster(mString, mTableName, mFieldName1, mFieldName2, , , SqlStr)
            If AcName <> "" Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = Col
                SprdMain.Text = AcName
            End If
        ElseIf Col = ColCC Then
            SprdMain.Row = Row
            SprdMain.Col = ColDept
            mDeptCode = SprdMain.Text
            SqlStr = " SELECT IH.CC_DESC,IH.CC_CODE, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE"
            If Trim(mDeptCode) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"
            End If
            MainClass.SearchGridMasterBySQL2("", SqlStr)
            SprdMain.Row = Row
            SprdMain.Col = Col
            If AcName <> "" Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = Col
                SprdMain.Text = AcName1
            End If
        Else
            MainClass.SearchGridMaster("", mTableName, mFieldName2, mFieldName1, , , SqlStr)
            If AcName <> "" Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = Col
                SprdMain.Text = AcName1
            End If
        End If
        SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(Col, SprdMain.ActiveRow, Col, SprdMain.ActiveRow, False))
        SprdMain.Refresh()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ChequeSearch(ByRef Col As Integer, ByRef Row As Integer)
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mString As String
        Dim mBankCode As String

        SprdMain.Row = Row
        SprdMain.Col = Col
        mString = SprdMain.Text
        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master.")
            Exit Sub
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BANKCODE='" & mBankCode & "'"
        If CurMKey = "" Then
            SqlStr = SqlStr & " AND CHEQUE_STATUS='O'"
        Else
            SqlStr = SqlStr & " AND (CHEQUE_STATUS='O' OR VMKEY='" & CurMKey & "')"
        End If
        MainClass.SearchGridMaster("", "FIN_CHEQUE_MST", "CHEQUE_NO", "", , , SqlStr)
        If AcName <> "" Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = Col
            SprdMain.Text = AcName
        End If
        SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(Col, SprdMain.ActiveRow, Col, SprdMain.ActiveRow, False))
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        On Error GoTo ERR1
        If SprdMain.ActiveRow <= 0 Then Exit Sub
        Select Case SprdMain.ActiveCol
            Case ColAccountName, ColCC, ColDept, ColEmp, ColExp, ColDivisionCode, ColSAC
                If eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then NameSearch((SprdMain.ActiveCol), (SprdMain.ActiveRow))
            Case ColAmount
                If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColAccountName, ConRowHeight)
                        '                    FormatSprdMain -1										
                        FormatSprdMainGST(-1)
                    End If
                End If
        End Select
        eventArgs.keyCode = 9999
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ERR1
        Dim mAmount As Double
        Dim pAccountName As String
        Dim mAccountCode As String
        Dim mOPBal As Double
        Dim mOPBalDiv As Double
        Dim mEmpCode As String
        Dim mDiv As Integer
        Dim mSACCode As String
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mRateOption As String
        If eventArgs.newRow = -1 Then Exit Sub
        Select Case eventArgs.col
            Case ColDC
                SprdMain.Col = ColDC
                SprdMain.Row = eventArgs.row
                If UCase(SprdMain.Text) = "DR" Or UCase(SprdMain.Text) = "D" Then
                    SprdMain.Text = "Dr"
                    Exit Sub
                ElseIf UCase(SprdMain.Text) = "CR" Or UCase(SprdMain.Text) = "C" Then
                    SprdMain.Text = "Cr"
                    Exit Sub
                Else
                    eventArgs.col = ColDC
                    SprdMain.Text = "Dr"
                    Exit Sub
                End If
            Case ColAccountName
                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColDivisionCode
                mDiv = Val(SprdMain.Text)
                SprdMain.Col = ColAccountName
                pAccountName = Trim(SprdMain.Text)
                If pAccountName = "" Then Exit Sub
                If CheckAccountName(pAccountName, eventArgs.col, eventArgs.row) = True Then
                    If eventArgs.row = 1 Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColDC
                        If GetHeadType(pAccountName) = "T" And UCase(SprdMain.Text) = "CR" Then
                            MainClass.SetFocusToCell(SprdMain, 1, ColAccountName, "TDS A/c Cann't be Select into First Row.")
                            Exit Sub
                        End If
                    End If
                    If lblBookType.Text = ConPDCPayment Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Then
                        SprdMain.Col = ColChequeDate
                        If Trim(SprdMain.Text) = "" Then
                            SprdMain.Text = TxtVDate.Text
                        End If
                    End If
                End If
                Call FillPRRowNo((SprdMain.ActiveRow))
                MainClass.ValidateWithMasterTable(pAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mAccountCode = MasterNo
                '            If IsDate(TxtVDate.Text) Then										
                mOPBal = GetOpeningBal(mAccountCode, VB6.Format(RunDate, "DD/MM/YYYY"))
                '            End If										
                lblAcBalAmt.Text = VB6.Format(System.Math.Abs(mOPBal), "0.00")
                lblAcBalDC.Text = IIf(mOPBal >= 0, "Dr", "Cr")
                If GetHeadType(pAccountName) = "L" Then
                    SprdMain.Col = ColEmp
                    SprdMain.Row = eventArgs.row
                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "EMP_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    End If
                    mEmpCode = Trim(SprdMain.Text)
                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Col = ColDept
                        SprdMain.Text = IIf(Trim(SprdMain.Text) = "", Trim(MasterNo), SprdMain.Text)
                    End If
                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "COST_CENTER_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Col = ColCC
                        SprdMain.Text = IIf(Trim(SprdMain.Text) = "", Trim(MasterNo), SprdMain.Text)
                    End If
                End If
            Case ColChequeNo
            Case ColChequeDate
            Case ColSAC
                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColSAC
                mSACCode = Trim(SprdMain.Text)
                If mSACCode = "" Then
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(0, "0.00")
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(0, "0.00")
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(0, "0.00")
                    SprdMain.Col = ColCGSTAmount
                    SprdMain.Text = VB6.Format(0, "0.00")
                    SprdMain.Col = ColSGSTAmount
                    SprdMain.Text = VB6.Format(0, "0.00")
                    SprdMain.Col = ColIGSTAmount
                    SprdMain.Text = VB6.Format(0, "0.00")
                Else
                    If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
                        MsgInformation("Invalid SAC.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColSAC)
                        Exit Sub
                    End If
                    If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "GST_RATE_OPT", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        mRateOption = MasterNo
                    Else
                        mRateOption = "N"
                    End If
                    If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, "Y", "", "G") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColSAC)
                        Exit Sub
                    End If
                    SprdMain.Col = ColAmount
                    mAmount = Val(SprdMain.Text)
                    If mRateOption = "Y" Then
                        MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                        MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTPer, ColSGSTPer)
                        '                    MainClass.ProtectCell SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTPer, ColIGSTPer										
                    Else
                        MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColIGSTPer)
                    End If
                    SprdMain.Row = eventArgs.row
                    If mRateOption = "Y" Then
                        SprdMain.Col = ColCGSTPer
                        mCGSTPer = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                        SprdMain.Col = ColSGSTPer
                        mSGSTPer = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                        SprdMain.Col = ColIGSTPer
                        mIGSTPer = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                    Else
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                    End If
                    mCGSTAmount = mAmount * mCGSTPer * 0.01
                    mSGSTAmount = mAmount * mSGSTPer * 0.01
                    mIGSTAmount = mAmount * mIGSTPer * 0.01
                    SprdMain.Col = ColCGSTAmount
                    SprdMain.Text = VB6.Format(mCGSTAmount, "0.00")
                    SprdMain.Col = ColSGSTAmount
                    SprdMain.Text = VB6.Format(mSGSTAmount, "0.00")
                    SprdMain.Col = ColIGSTAmount
                    SprdMain.Text = VB6.Format(mIGSTAmount, "0.00")
                End If
            Case ColExp
                If CheckMst(ColExp, "CST_CENTER_MST", "COST_CENTER_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColDivisionCode
                If CheckMst(ColDivisionCode, "INV_DIVISION_MST", "DIV_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColDivisionCode
                mDiv = Val(SprdMain.Text)
                SprdMain.Col = ColAccountName
                pAccountName = Trim(SprdMain.Text)
                If pAccountName <> "" Then
                    If MainClass.ValidateWithMasterTable(pAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                        mOPBalDiv = GetOpeningBal(mAccountCode, VB6.Format(RunDate, "DD/MM/YYYY"), "", mDiv)
                        lblAcBalAmtDiv.Text = VB6.Format(System.Math.Abs(mOPBalDiv), "0.00")
                        lblAcBalDCDiv.Text = IIf(mOPBalDiv >= 0, "Dr", "Cr")
                    End If
                End If
            Case ColCC
                If CheckMst(ColCC, "FIN_CCENTER_HDR", "CC_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColDept
                If CheckMst(ColDept, "PAY_DEPT_MST", "DEPT_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColEmp
                If CheckMst(ColEmp, "PAY_EMPLOYEE_MST", "EMP_CODE") = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColCGSTPer, ColSGSTPer, ColIGSTPer
                SprdMain.Row = eventArgs.row
                '            SprdMain.Col = ColSAC										
                '            mSACCode = Trim(SprdMain.Text)										
                '										
                '										
                '            If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then										
                '                MsgInformation "Invalid SAC."										
                '										
                '                MainClass.SetFocusToCell SprdMain, Row, ColSAC										
                '                Exit Sub										
                '            End If										
                '            If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "GST_RATE_OPT", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then										
                '                mRateOption = MasterNo										
                '            Else										
                '                mRateOption = "N"										
                '            End If										
                '            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, "Y", "") = False Then										
                '                MainClass.SetFocusToCell SprdMain, Row, ColSAC										
                '                Exit Sub										
                '            End If										
                SprdMain.Col = ColAmount
                mAmount = Val(SprdMain.Text)
                '            If mRateOption = "Y" Then										
                '                MainClass.UnProtectCell SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColCGSTPer										
                '                MainClass.UnProtectCell SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTPer, ColSGSTPer										
                '                MainClass.ProtectCell SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTPer, ColIGSTPer										
                '            Else										
                '                MainClass.ProtectCell SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColIGSTPer										
                '            End If										
                SprdMain.Col = ColCGSTPer
                mCGSTPer = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                SprdMain.Col = ColSGSTPer
                mSGSTPer = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                SprdMain.Col = ColIGSTPer
                mIGSTPer = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
                mCGSTAmount = mAmount * mCGSTPer * 0.01
                mSGSTAmount = mAmount * mSGSTPer * 0.01
                mIGSTAmount = mAmount * mIGSTPer * 0.01
                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = VB6.Format(mCGSTAmount, "0.00")
                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = VB6.Format(mSGSTAmount, "0.00")
                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = VB6.Format(mIGSTAmount, "0.00")
            Case ColAmount
                '            SprdMain.Col = ColAmount										
                '            SprdMain.Row = Row										
                '            If Val(SprdMain.Text) = 0 Then										
                '                MainClass.SetFocusToCell SprdMain, Row, ColAmount										
                '                Exit Sub										
                '            End If										
                Call PayDetailForm((SprdMain.ActiveRow))
        End Select
        '    FormatSprdMain -1										
        FormatSprdMainGST(-1)
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColSAC
        mSACCode = Trim(SprdMain.Text)
        If mSACCode <> "" Then
            If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "GST_RATE_OPT", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                mRateOption = MasterNo
            Else
                mRateOption = "N"
            End If
            If mRateOption = "Y" Then
                MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTPer, ColSGSTPer)
                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTPer, ColIGSTPer)
                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTAmount, ColCGSTAmount)
                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTAmount, ColSGSTAmount)
                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTAmount, ColIGSTAmount)
            Else
                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColIGSTPer)
            End If
        End If
        CalcTots()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume										
    End Sub
    Private Sub CalcTots()
        On Error GoTo ErrSprdTotal
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mNetAmt As Double
        Dim MTotalAmt As Double
        Dim cntRow As Integer
        Dim mPartyAmt As Double
        mPartyAmt = 0
        If FormLoaded = False Then Exit Sub
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow
            SprdMain.Col = ColDC
            If VB.Left(SprdMain.Text, 1) = "D" Then
                SprdMain.Col = ColAmount
                mDAmt = mDAmt + Val(SprdMain.Value)
                If chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    SprdMain.Col = ColCGSTAmount
                    mDAmt = mDAmt + Val(SprdMain.Value)
                    SprdMain.Col = ColSGSTAmount
                    mDAmt = mDAmt + Val(SprdMain.Value)
                    SprdMain.Col = ColIGSTAmount
                    mDAmt = mDAmt + Val(SprdMain.Value)
                End If
            Else
                SprdMain.Col = ColAmount
                mCAmt = mCAmt + Val(SprdMain.Value)
                If mPartyAmt = 0 Then
                    mPartyAmt = Val(SprdMain.Value)
                End If
            End If
            mNetAmt = System.Math.Abs(mCAmt - mDAmt)
NextRow:
        Next cntRow
        LblDrAmt.Text = VB6.Format(mDAmt, "##,##,##,##0.00")
        LblCrAmt.Text = VB6.Format(mCAmt, "##,##,##,##0.00")
        LblNetAmt.Text = VB6.Format(mNetAmt, "##,##,##,##0.00")
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSDeductOn.Text = VB6.Format(IIf(Val(txtTDSDeductOn.Text) = 0, mPartyAmt, txtTDSDeductOn.Text), "#0.00")
        Else
            txtTDSDeductOn.Text = VB6.Format(mPartyAmt, "#0.00")
        End If
        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIDeductOn.Text = VB6.Format(IIf(Val(txtESIDeductOn.Text) = 0, mPartyAmt, txtESIDeductOn.Text), "#0.00")
        Else
            txtESIDeductOn.Text = VB6.Format(mPartyAmt, "#0.00")
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSDeductOn.Text = VB6.Format(IIf(Val(txtSTDSDeductOn.Text) = 0, mPartyAmt, txtSTDSDeductOn.Text), "#0.00")
        Else
            txtSTDSDeductOn.Text = VB6.Format(mPartyAmt, "#0.00")
        End If
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtJVTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtJVTDSRate.Text) * Val(txtTDSDeductOn.Text) / 100, 0), "0.00")
            Else
                txtJVTDSAmount.Text = VB6.Format(Val(txtJVTDSRate.Text) * Val(txtTDSDeductOn.Text) / 100, "0.00")
            End If
        Else
            txtJVTDSAmount.Text = "0.00"
        End If
        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Val(txtESIRate.Text) <> 0 Then
                If ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtESIAmount.Text = VB6.Format(System.Math.Round(Val(txtESIRate.Text) * Val(txtESIDeductOn.Text) / 100, 0), "0.00")
                Else
                    txtESIAmount.Text = VB6.Format(Val(txtESIRate.Text) * Val(txtESIDeductOn.Text) / 100, "0.00")
                End If
            End If
        Else
            txtESIAmount.Text = "0.00"
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtSTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtSTDSRate.Text) * Val(txtSTDSDeductOn.Text) / 100, 0), "0.00")
            Else
                txtSTDSAmount.Text = VB6.Format(Val(txtSTDSRate.Text) * Val(txtSTDSDeductOn.Text) / 100, "0.00")
            End If
        Else
            txtSTDSAmount.Text = "0.00"
        End If
        txtServiceTaxAmount.Text = VB6.Format(System.Math.Round(Val(txtServiceOn.Text) * Val(txtServiceTaxPer.Text) * 0.01, 0), "0.00")
        Exit Sub
ErrSprdTotal:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume										
    End Sub
    Private Function CheckMst(ByRef mCol As Integer, ByRef TabName As String, Optional ByRef pCode As String = "") As Boolean
        On Error GoTo ERR1
        Dim mAcctName As String
        Dim mSqlStr As String
        Dim mEmpCode As String
        CheckMst = False
        With SprdMain
            .Row = .ActiveRow
            .Col = ColAccountName
            If Trim(.Text) = "" Then
                CheckMst = True
                Exit Function
            Else
                mAcctName = Trim(.Text)
                ''Validate only if acct is income/expenses ...										
                If CheckExpHead(mAcctName) = True Then
                    .Col = mCol
                    If (UCase(TabName) = UCase("CST_CENTER_MST") Or UCase(TabName) = UCase("FIN_CCENTER_HDR")) And Trim(.Text) = "" Then
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, mCol, "This Field is must.")
                        Exit Function
                    End If
                End If
                .Col = mCol
                If (UCase(TabName) = UCase("INV_DIVISION_MST")) And Trim(.Text) = "" Then
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, mCol, "This Field is must.")
                    Exit Function
                End If
            End If
            .Col = mCol
            If Trim(.Text) <> "" Then
                mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                If UCase(TabName) = UCase("CST_CENTER_MST") And ADDMode = True Then
                    mSqlStr = mSqlStr & " AND STATUS='O'"
                End If
                If MainClass.ValidateWithMasterTable(.Text, pCode, pCode, TabName, PubDBCn, MasterNo, , mSqlStr) = False Then
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, .ActiveCol, "Invalid Alias.")
                    Exit Function
                End If
                If UCase(TabName) = "PAY_EMPLOYEE_MST" Then
                    mEmpCode = Trim(.Text)
                    If MainClass.ValidateWithMasterTable(mEmpCode, pCode, "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                        .Col = ColDept
                        .Text = IIf(Trim(.Text) = "", Trim(MasterNo), .Text)
                    End If
                    If MainClass.ValidateWithMasterTable(mEmpCode, pCode, "COST_CENTER_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , mSqlStr) = True Then
                        .Col = ColCC
                        .Text = IIf(Trim(.Text) = "", Trim(MasterNo), .Text)
                    End If
                End If
            End If
        End With
        CheckMst = True
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillPRRowNo(ByRef mRow As Integer)
        Dim cntRow As Integer
        Dim mMaxRowNo As Integer
        With SprdMain
            .Row = mRow
            .Col = ColPRRowNo
            If Trim(.Text) <> "" Then Exit Sub
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPRRowNo
                If Val(.Text) > mMaxRowNo Then
                    mMaxRowNo = Val(.Text)
                End If
            Next
            .Row = mRow
            .Col = ColPRRowNo
            .Text = CStr(mMaxRowNo + 1)
        End With
    End Sub
    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If SprdMain.ActiveCol = ColAmount Then
            Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(SprdMain.ActiveCol, SprdMain.ActiveRow, ColDC, SprdMain.ActiveCol + 1, False))
            Call PayDetailForm((SprdMain.ActiveRow))
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub PayDetailForm(ByRef mActiveRow As Integer)
        ConPaymentDetail = False
        ConServiceTaxDetail = False
        If ShowDetailForm() = "S" Then 'When Account is bill by bill										
            If SprdMain.MaxRows = mActiveRow Then
                MainClass.AddBlankSprdRow(SprdMain, ColAccountName, ConRowHeight)
                '                FormatSprdMain -1										
                FormatSprdMainGST(-1)
            End If
        Else
            If ConPaymentDetail = True Then
                SprdMain.Row = mActiveRow
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(frmPaymentDetail.LblNetAmt.Text))
                If SprdMain.MaxRows = mActiveRow Then
                    MainClass.AddBlankSprdRow(SprdMain, ColAccountName, ConRowHeight)
                    '                    FormatSprdMain -1										
                    FormatSprdMainGST(-1)
                End If
            End If
            frmPaymentDetail.Close()
            frmLoanMaster.Close()
            frmServiceTaxDetail.Close()
        End If
    End Sub
    Private Function ShowDetailForm() As String
        Dim mAccountName As String = ""
        Dim mAmount As Double
        Dim mDC As String = ""
        Dim mNarration As String = ""
        Dim mEmpCode As String = ""
        Dim mCostCName As String = ""
        Dim mPRRowNo As Integer
        Dim mCostCode As String = ""
        Dim mAccountCode As String = ""
        Dim mHeadType As String = ""
        Dim mPartyName As String = ""
        Dim mCurrRow As Integer
        Dim cntRow As Integer
        Dim mSectionCode As Double
        Dim mBillAmount As Double
        Dim mDivisionCode As Double
        ShowDetailForm = "S"
        With SprdMain
            .Row = .ActiveRow
            .Col = ColPRRowNo
            mPRRowNo = Val(.Text)
            .Col = ColAccountName
            mAccountName = SprdMain.Text
            .Col = ColDC
            mDC = .Text
            .Col = ColCC
            If Trim(.Text) <> "" Then
                If MainClass.ValidateWithMasterTable(.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCostCName = MasterNo
                Else
                    Exit Function
                End If
                ''MainClass.ValidateWithMasterTable .Text, "COST_CENTER_CODE", "COST_CENTER_DESC", "CST_CENTER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""										
                mCostCode = Trim(.Text) ''MasterNo										
            End If
            .Col = ColDivisionCode
            If Val(.Text) <> 0 Then
                If MainClass.ValidateWithMasterTable(.Text, "DIV_CODE", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = Val(MasterNo)
                Else
                    Exit Function
                End If
            End If
            .Col = ColAmount
            mAmount = Val(.Text)
        End With
        ''01/01/2003										
        '    If Val(SprdMain.Text) <> 0 Then										
        If GetAccountBalancingMethod(mAccountName, False) = "D" Then
            ShowDetailForm = "D"
            With frmPaymentDetail
                .lblAccountName.Text = mAccountName
                .lblAmount.Text = CStr(mAmount)
                .lblADDMode.Text = CStr(ADDMode)
                .lblTempProcessKey.Text = CStr(pProcessKey)
                .lblModifyMode.Text = CStr(MODIFYMode)
                .lblDC.Text = mDC
                .lblVDate.Text = TxtVDate.Text
                .lblNarration.Text = mNarration
                .lblBookType.Text = lblBookType.Text
                .lblCostCName.Text = mCostCName
                .lblCostCCode.Text = mCostCode
                .lblTrnRowNo.Text = CStr(mPRRowNo)
                .lblDivisionCode.Text = CStr(mDivisionCode)
                .txtDefaultCompanyName.Text = RsCompany.Fields("COMPANY_NAME").Value
                .lblMkey.Text = CurMKey

                .lblVoucherAmount.Text = mAmount
                .lblVoucherDC.Text = mDC

                .cmdPopulate.Enabled = True
                If ADDMode = True Then
                    .cmdAppendDetail.Enabled = False
                Else
                    .cmdAppendDetail.Enabled = True
                End If
                .ShowDialog()
                If ADDMode = True Or MODIFYMode = True Then cmdSave.Enabled = True
            End With
        Else
            mHeadType = GetHeadType(mAccountName)
        End If
        If mHeadType = "T" Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColDC
            If UCase(SprdMain.Text) = "DR" Then Exit Function
            SprdMain.Col = ColAccountName
            TxtTDSAccount.Text = SprdMain.Text
            SprdMain.Col = ColAmount
            txtTDSAmount.Text = SprdMain.Text
            txtVD.Text = TxtVDate.Text
            txtAmountPaid.Text = IIf(Val(txtAmountPaid.Text) = 0, Val(CStr(CDbl(LblDrAmt.Text))), Val(txtAmountPaid.Text))
            If Val(txtAmountPaid.Text) = Val(txtTDSAmount.Text) Then
                txtAmountPaid.Text = ""
            End If
            SprdMain.Row = 1
            SprdMain.Col = ColAccountName
            txtPName.Text = SprdMain.Text
            If Trim(txtSection.Text) = "" Then
                If MainClass.ValidateWithMasterTable((txtPName.Text), "SUPP_CUST_NAME", "SECTIONCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSectionCode = MasterNo
                    If MainClass.ValidateWithMasterTable(mSectionCode, "CODE", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtSection.Text = MasterNo
                    End If
                End If
            End If
            FraTDSFrame.Visible = True
            TxtTDSAccount.Focus()
        ElseIf mHeadType = "S" Then
            mCurrRow = SprdMain.ActiveRow
            If lblBookType.Text = ConBankPayment Then
                mPartyName = Trim(txtPartyName.Text)
                SprdMain.Row = 1
                SprdMain.Col = ColAmount
                mBillAmount = Val(SprdMain.Text)
            Else
                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColDC
                    If UCase(SprdMain.Text) = "CR" Then
                        SprdMain.Col = ColAccountName
                        mPartyName = Trim(SprdMain.Text)
                        If GetAccountBalancingMethod(mPartyName, False) = "D" Then
                            Exit For
                        End If
                    End If
                Next
            End If
            If mPartyName = "" Then Exit Function
            SprdMain.Row = mCurrRow ''SprdMain.ActiveRow										
            SprdMain.Col = ColDC
            If UCase(SprdMain.Text) = "CR" Then Exit Function
            With frmServiceTaxDetail
                .lblAccountName.Text = mPartyName
                .lblAmount.Text = CStr(mAmount)
                .lblBillAmount.Text = CStr(mBillAmount)
                .lblADDMode.Text = CStr(ADDMode)
                .lblModifyMode.Text = CStr(MODIFYMode)
                .lblDC.Text = mDC
                .lblVDate.Text = TxtVDate.Text
                .lblBookType.Text = lblBookType.Text
                .lblTrnRowNo.Text = CStr(mPRRowNo)
                .ShowDialog()
                If ADDMode = True Or MODIFYMode = True Then cmdSave.Enabled = True
            End With
        ElseIf mHeadType = "L" Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColDC
            If UCase(SprdMain.Text) = "CR" Then Exit Function
            With frmLoanMaster
                If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then Exit Function
                mAccountCode = MasterNo
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "EMP_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then Exit Function
                mEmpCode = MasterNo
                If Trim(mEmpCode) = "" Then Exit Function
                .txtEmpNo.Text = mEmpCode
                .txtLoanAmt.Text = CStr(mAmount)
                .txtLoanDate.Text = TxtVDate.Text
                .lblADDMode.Text = CStr(ADDMode)
                .lblModifyMode.Text = CStr(MODIFYMode)
                .ShowDialog()
            End With
            '        ElseIf mHeadType = "1" Then										
            '            mCurrRow = SprdMain.ActiveRow										
            '            For cntRow = 1 To SprdMain.MaxRows										
            '                SprdMain.Row = cntRow										
            '                SprdMain.Col = ColDC										
            '                If UCase(SprdMain.Text) = "DR" Then										
            '                    SprdMain.Col = ColAccountName										
            '                    mPartyName = Trim(SprdMain.Text)										
            '                    If GetAccountBalancingMethod(mPartyName, False) = "D" Then										
            '                        Exit For										
            '                    End If										
            '                End If										
            '            Next										
            '										
            '            If mPartyName = "" Then Exit Function										
            '            SprdMain.Row = mCurrRow         ''SprdMain.ActiveRow										
            '            SprdMain.Col = ColDC										
            '            If UCase(SprdMain.Text) = "CR" Then Exit Function										
            '										
            '            With frmFreightDetail										
            '                .lblAccountName.text = mPartyName										
            '                .lblAmount.text = mAmount										
            '                .lblADDMode.text = ADDMode										
            '                .lblModifyMode.text = MODIFYMode										
            '                .lblDC.text = mDC										
            '                .lblVDate.text = TxtVDate.Text										
            '                .lblBookType.text = lblBookType.text										
            '                .lblTrnRowNo.text = mPRRowNo										
            '                .Show 1										
            '                If ADDMode = True Or MODIFYMode = True Then cmdSave.Enabled = True										
            '            End With										
        End If
        '    End If										
    End Function
    Private Sub ShowTDSDetail()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mPartyCode As String = ""


        Dim RsTDSDetail As ADODB.Recordset = Nothing
        SqlStr = "SELECT * FROM TDS_TRN WHERE Mkey='" & CurMKey & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSDetail, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTDSDetail.EOF = False Then
            With RsTDSDetail
                If MainClass.ValidateWithMasterTable(.Fields("ACCOUNTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtTDSAccount.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                Else
                    TxtTDSAccount.Text = ""
                End If
                chkExempted.CheckState = IIf(.Fields("ISEXEPTED").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkISLowerDed.CheckState = IIf(.Fields("ISLOWERDED").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                txtAmountPaid.Text = VB6.Format(IIf(IsDBNull(.Fields("AmountPaid").Value), "", .Fields("AmountPaid").Value), "0.00")

                mPartyCode = If(IsDBNull(.Fields("PARTYCODE").Value), "", .Fields("PARTYCODE").Value)

                If mPartyCode = "" Then
                    txtPName.Text = ""
                Else

                    If MainClass.ValidateWithMasterTable(.Fields("PARTYCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtPName.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                    Else
                        txtPName.Text = ""
                    End If
                End If
                '            txtPName.Text = IIf(IsNull(.Fields("PARTYNAME").Value), "", IIf(.Fields("PARTYNAME").Value = "-1", "", .Fields("PARTYNAME").Value))										
                txtVD.Text = IIf(IsDBNull(.Fields("VDate").Value), "", .Fields("VDate").Value)

                If IsDBNull(.Fields("SECTIONCODE").Value) Then
                    txtSection.Text = ""
                Else
                    If MainClass.ValidateWithMasterTable(.Fields("SECTIONCODE").Value, "Code", "Name", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtSection.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                    Else
                        txtSection.Text = ""
                    End If
                End If
                txtTDSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TDSAMOUNT").Value), "", .Fields("TDSAMOUNT").Value), "0.00")
                txtTdsRate.Text = VB6.Format(IIf(IsDBNull(.Fields("TDSRATE").Value), "", .Fields("TDSRATE").Value), "0.000")
                txtExempted.Text = IIf(IsDBNull(.Fields("EXEPTIONCNO").Value), "", .Fields("EXEPTIONCNO").Value)
            End With
            RsTDSDetail.Close()
            RsTDSDetail = Nothing
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        RsTDSDetail.Close()
        RsTDSDetail = Nothing
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        TxtVDate.Text = SprdView.Text
        SprdView.Col = 2
        txtVNo1.Text = SprdView.Text
        SprdView.Col = 3
        txtVType.Text = SprdView.Text
        SprdView.Col = 6
        txtVNoSuffix.Text = SprdView.Text
        SprdView.Col = 4
        txtVno.Text = VB6.Format(SprdView.Text, "00000")
        txtVNo_Validating(txtVno, New System.ComponentModel.CancelEventArgs(False))
        If SprdMain.Enabled = True Then SprdMain.Focus()
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub txtAmountPaid_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmountPaid.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAmountPaid_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmountPaid.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtESIDeductOn.Text = VB6.Format(txtESIDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtESIRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtESIRate.Text = VB6.Format(txtESIRate.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtExempted_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExempted.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtExempted_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExempted.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtExempted.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtExpBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtExpBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtExpBillNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpBillNo.DoubleClick
        SearchExpBillNo()
    End Sub
    Private Sub txtExpBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExpBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtExpBillNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtExpBillNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchExpBillNo()
    End Sub
    Private Sub txtExpBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If Trim(txtExpBillNo.Text) = "" Then
            '        txtExpPartyName.Text = ""										
            txtExpBillDate.Text = ""
            GoTo EventExitSub
        End If
        SqlStr = " SELECT CMST.SUPP_CUST_NAME, IH.AUTO_KEY_EXPINV, " & vbCrLf & " IH.BILLNO, IH.EXPINV_DATE " & vbCrLf & " FROM FIN_EXPINV_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.AUTO_KEY_EXPINV=" & Val(txtExpBillNo.Text) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            txtExpPartyName.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            '        txtExpBillNo.Text = IIf(IsNull(RsTemp!BILL_NO), "", RsTemp!BILL_NO)										
            txtExpBillDate.Text = IIf(IsDBNull(RsTemp.Fields("EXPINV_DATE").Value), "", RsTemp.Fields("EXPINV_DATE").Value)
        Else
            ssTab.SelectedIndex = 3
            MsgInformation("Invalid Export Bill No.")
            '        txtExpPartyName.Text = ""										
            txtExpBillDate.Text = ""
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtExpDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtExpDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExpDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtExpDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtExpDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtExpDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        If lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then GoTo EventExitSub
        If FYChk((txtExpDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtExpPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpPartyName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtExpPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpPartyName.DoubleClick
        Call SearchExpPartyName()
    End Sub
    Private Sub txtExpPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExpPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtExpPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtExpPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtExpPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchExpPartyName()
    End Sub
    Private Sub txtExpPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If Trim(txtExpPartyName.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If MainClass.ValidateWithMasterTable((txtExpPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ssTab.SelectedIndex = 3
            MsgBox("Invaild Party Name. ", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtImpBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtImpBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtImpBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtImpBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtImpBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtImpBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtImpBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtImpMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtImpMRRNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtImpMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtImpMRRNo.DoubleClick
        Call SearchImpMRRNo()
    End Sub
    Private Sub txtImpMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtImpMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtImpMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtImpMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchImpMRRNo()
    End Sub
    Private Sub SearchImpMRRNo()
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        Dim mImpPartyCode As String
        SqlStr = "COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If Trim(txtImpPartyName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtImpPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mImpPartyCode = MasterNo
                SqlStr = SqlStr & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(mImpPartyCode) & "'"
            End If
        End If
        MainClass.SearchGridMaster(txtImpMRRNo.Text, "INV_GATE_HDR", "AUTO_KEY_MRR", "BILL_NO", "BILL_DATE", , SqlStr)
        If AcName <> "" Then
            txtImpMRRNo.Text = AcName
            txtImpMRRNo_Validating(txtImpMRRNo, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtImpMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtImpMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If Val(txtImpMRRNo.Text) = 0 Then
            '        txtImpPartyName.Text = ""										
            txtImpBillNo.Text = ""
            txtImpBillDate.Text = ""
            GoTo EventExitSub
        End If
        SqlStr = " SELECT CMST.SUPP_CUST_NAME, IH.AUTO_KEY_MRR, " & vbCrLf & " IH.BILL_NO, IH.BILL_DATE " & vbCrLf & " FROM INV_GATE_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(txtImpMRRNo.Text) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            txtImpPartyName.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            txtImpBillNo.Text = IIf(IsDBNull(RsTemp.Fields("BILL_NO").Value), "", RsTemp.Fields("BILL_NO").Value)
            txtImpBillDate.Text = IIf(IsDBNull(RsTemp.Fields("BILL_DATE").Value), "", RsTemp.Fields("BILL_DATE").Value)
        Else
            ssTab.SelectedIndex = 2
            MsgInformation("Invalid MRR No.")
            '        txtImpPartyName.Text = ""										
            txtImpBillNo.Text = ""
            txtImpBillDate.Text = ""
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtImpPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtImpPartyName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtImpPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtImpPartyName.DoubleClick
        Call SearchImpPartyName()
    End Sub
    Private Sub txtImpPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtImpPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtImpPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtImpPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtImpPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchImpPartyName()
    End Sub
    Private Sub txtImpPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtImpPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If Trim(txtImpPartyName.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If MainClass.ValidateWithMasterTable((txtImpPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ssTab.SelectedIndex = 2
            MsgBox("Invaild Party Name. ", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtJVTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJVTDSAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtJVTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtJVTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtJVTDSRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJVTDSRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtJVTDSRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtJVTDSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtJVTDSRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJVTDSRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtJVTDSRate.Text = VB6.Format(txtJVTDSRate.Text, "0.000")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtJVVNO_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJVVNO.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtModvatNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModvatNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtModvatNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModvatNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNarration_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNarration.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        Call SearchName()
    End Sub
    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchName()
    End Sub
    Private Sub SearchName()
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Select Case lblBookType.Text
            Case ConCashPayment, ConCashReceipt
                SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '1'"
            Case ConBankPayment, ConBankReceipt, ConPDCPayment, ConPDCReceipt
                SqlStr = SqlStr & " AND SUPP_CUST_TYPE = '2'"
            Case Else
                SqlStr = SqlStr & " AND 1=2"
        End Select
        SqlStr = SqlStr & " AND STATUS='O'"

        If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCPayment Or lblBookType.Text = ConPDCReceipt Then
            SqlStr = SqlStr & " AND SUPP_CUST_NAME IN (SELECT VNAME FROM FIN_VOUCHERTYPE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FOR_HO='O')"
        End If
        'MainClass.SearchMaster(txtPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)

        MainClass.SearchGridMaster((txtPartyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_Name", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtPartyName.Text = AcName
            txtPartyName_Validating(txtPartyName.Text, New System.ComponentModel.CancelEventArgs(True))
            If ADDMode = True Then txtVType.Focus() Else SprdMain.Focus()
        End If
        Exit Sub
SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetCashBookName() As String
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        Dim TempRs As ADODB.Recordset = Nothing
        GetCashBookName = ""

        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_TYPE = '1' AND STATUS='O'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, TempRs, ADODB.LockTypeEnum.adLockReadOnly)
        If TempRs.EOF = False Then
            GetCashBookName = IIf(IsDBNull(TempRs.Fields("SUPP_CUST_NAME").Value), "", TempRs.Fields("SUPP_CUST_NAME").Value)
        End If
        Exit Function
SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CheckAccountName(ByRef pAccountName As String, ByRef col2 As Integer, ByRef Row2 As Integer) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing '' ADODB.Recordset										
        CheckAccountName = False
        If pAccountName = "" Then
            Exit Function
        End If
        SqlStr = " SELECT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(Trim(pAccountName)) & "'"
        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND STATUS='O' "
        End If
        '    If lblBookType.text = ConContra Then										
        '        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE IN ('1','2')"										
        '    Else										
        '        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_TYPE NOT IN ('1','2')"										
        '    End If										
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            MainClass.SetFocusToCell(SprdMain, Row2, col2, "Invalid Account.")
            Exit Function
        End If
        SprdMain.Col = ColAccountName
        SprdMain.Row = Row2
        If CheckforAccountsType((SprdMain.Text)) = False Then
            MainClass.SetFocusToCell(SprdMain, Row2, col2, "This Account is not allowed here")
            Exit Function
        End If
        CheckAccountName = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        CheckAccountName = True
        RS.Close()
        RS = Nothing
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckAccountName = False
        RS.Close()
        RS = Nothing
    End Function
    Private Function CheckforAccountsType(ByRef mAccountName As String) As Boolean
        Dim mAccountCode As String = ""
        Dim mBookCode As String = ""
        CheckforAccountsType = True
        If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If MainClass.ValidateWithMasterTable((txtPartyName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, mBookCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If mAccountCode = mBookCode Then
                    CheckforAccountsType = False
                End If
            End If
        End If
    End Function
    Private Function CheckDivisionWiseDRCRMatch(ByRef mDRCRBal As Double, ByRef xDivName As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mDRCRBal As Double										
        Dim cntRow As Integer
        Dim mDivisionCode As Double
        Dim mCheckDivisionCode As Double
        Dim mAccountName As String
        Dim mDC As String
        Dim mSuppCustDC As String = ""
        Dim mSuppCustAmount As Double
        Dim mPRowNo As Integer
        Dim mGSTAmount As Double
        CheckDivisionWiseDRCRMatch = False
        SqlStr = "SELECT DIV_CODE,DIV_DESC FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DIV_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mDivisionCode = IIf(IsDBNull(RsTemp.Fields("DIV_CODE").Value), -1, RsTemp.Fields("DIV_CODE").Value)
                xDivName = IIf(IsDBNull(RsTemp.Fields("DIV_DESC").Value), "", RsTemp.Fields("DIV_DESC").Value)
                mDRCRBal = 0
                With SprdMain
                    For cntRow = 1 To .MaxRows - 1
                        .Row = cntRow
                        .Col = ColAccountName
                        mAccountName = Trim(.Text)
                        .Col = ColPRRowNo
                        mPRowNo = Val(.Text)
                        If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            If GetAccountBalancingMethod(mAccountName, False) = "D" Then
                                If GetBillDetailAmount(mPRowNo, mAccountName, mDivisionCode, mSuppCustDC, mSuppCustAmount) = True Then
                                    mDRCRBal = CDbl(VB6.Format(mDRCRBal + Val(CStr((mSuppCustAmount) * IIf(mSuppCustDC = "D", 1, -1))), "0.00"))
                                End If
                            Else
                                .Col = ColDivisionCode
                                mCheckDivisionCode = Val(.Text)
                                If mDivisionCode = mCheckDivisionCode Then
                                    .Col = ColDC
                                    mDC = UCase(Trim(.Text))
                                    .Col = ColAmount
                                    mDRCRBal = CDbl(VB6.Format(mDRCRBal + Val(CStr(Val(.Text) * IIf(mDC = "DR", 1, -1))), "0.00"))
                                    If chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                                        .Col = ColCGSTAmount
                                        mDRCRBal = CDbl(VB6.Format(mDRCRBal + Val(CStr(Val(.Text) * IIf(mDC = "DR", 1, -1))), "0.00"))
                                        .Col = ColSGSTAmount
                                        mDRCRBal = CDbl(VB6.Format(mDRCRBal + Val(CStr(Val(.Text) * IIf(mDC = "DR", 1, -1))), "0.00"))
                                        .Col = ColIGSTAmount
                                        mDRCRBal = CDbl(VB6.Format(mDRCRBal + Val(CStr(Val(.Text) * IIf(mDC = "DR", 1, -1))), "0.00"))
                                    End If
                                End If
                            End If
                        End If
                    Next
                End With
                If mDRCRBal <> 0 Then
                    CheckDivisionWiseDRCRMatch = False
                    Exit Function
                End If
                RsTemp.MoveNext()
            Loop
        End If
        CheckDivisionWiseDRCRMatch = True
        Exit Function
ErrPart:
        'Resume										
        MsgInformation(Err.Description)
        CheckDivisionWiseDRCRMatch = False
    End Function
    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsCheckName As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mOPBal As Double
        If Trim(txtPartyName.Text) = "" Then GoTo EventExitSub
        SqlStr = " Select SUPP_CUST_NAME,SUPP_CUST_CODE,STATUS FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(Trim(txtPartyName.Text)) & "'"
        Select Case lblBookType.Text
            Case ConBankPayment, ConBankReceipt, ConPDCPayment, ConPDCReceipt
                SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_TYPE='2'"
            Case ConCashPayment, ConCashReceipt
                SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_TYPE='1'"
        End Select
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheckName, ADODB.LockTypeEnum.adLockReadOnly)
        If RsCheckName.EOF = True Then
            MsgBox("Invaild Account Name. ", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
            '    ElseIf RsCheckName.Fields("Status").Value = "C" Then										
            '        MsgBox "Account is closed. ", vbCritical										
            '        Cancel = True										
            '        Exit Sub										
        End If
        If lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Then
            If PubUserID = "G0416" Then
            Else
                If CheckPendingPDC() = True Then
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If
        End If
        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "VNAME", "VTYPE", "FIN_VOUCHERTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'") = True Then
            txtVType.Text = MasterNo
        End If

        If IsDate(TxtVDate.Text) Then
            mOPBal = GetOpeningBal((RsCheckName.Fields("SUPP_CUST_CODE").Value), (TxtVDate.Text))
        End If
        lblBookBalAmt.Text = VB6.Format(System.Math.Abs(mOPBal), "0.00")
        lblBookBalDC.Text = IIf(mOPBal >= 0, "Dr", "Cr")
        RsCheckName.Close()
        RsCheckName = Nothing
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtProviderPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProviderPer.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtProviderPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProviderPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRecipientPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecipientPer.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRecipientPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRecipientPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSection_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.DoubleClick
        Call SearchTDSSection()
    End Sub
    Private Sub SearchTDSSection()
        On Error GoTo SearchErr
        'If MainClass.SearchMaster((txtSection.Text), "TDS_Section_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        If MainClass.SearchGridMaster((txtSection.Text), "TDS_Section_MST", "NAME", "CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSection.Text = AcName
            txtSection.Focus()
        End If
        Exit Sub
SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSection_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSection.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTDSSection()
    End Sub
    Private Sub txtSection_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSection.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSection.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtSection.Text), "NAME", "NAME", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invaild TDS Section")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtServiceOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServiceOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServiceOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServiceOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServiceOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServiceOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtServiceTaxAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServiceTaxAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServiceTaxAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServiceTaxAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServiceTaxPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServiceTaxPer.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServiceTaxPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServiceTaxPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServiceTaxPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServiceTaxPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Val(txtServiceTaxPer.Text) = 0 Then GoTo EventExitSub
        If Val(txtServiceTaxPer.Text) > 100 Then
            MsgInformation("Service Tax Cann't be Greater Than 100%")
            Cancel = True
            GoTo EventExitSub
        End If
        CalcTots()
        GoTo EventExitSub
ErrPart:
        GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtServProvided_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServProvided_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.DoubleClick
        SearchProvidedMaster()
    End Sub
    Private Sub txtServProvided_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServProvided.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtServProvided.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServProvided_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtServProvided.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchProvidedMaster()
    End Sub
    Private Sub txtServProvided_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServProvided.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mReverseChargeApp As String
        Dim mReverseChargePer As String
        Dim mServiceTaxOn As Double
        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub
        txtProviderPer.Text = "0.00"
        txtRecipientPer.Text = "0.00"
        SprdMain.Row = 1
        SprdMain.Col = ColAmount
        mServiceTaxOn = Val(SprdMain.Text)
        SqlStr = " SELECT CODE, NAME, REVERSE_CHARGE_APP, REVERSE_CHARGE_PER, SERVICE_TAX_PER,SERVICE_TAX_ON_PER" & vbCrLf & " FROM FIN_SERVPROV_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND NAME='" & MainClass.AllowSingleQuote((txtServProvided.Text)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mReverseChargeApp = IIf(IsDBNull(RsTemp.Fields("REVERSE_CHARGE_APP").Value), "N", RsTemp.Fields("REVERSE_CHARGE_APP").Value)
            If ADDMode = True Then
                txtServiceTaxPer.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SERVICE_TAX_PER").Value), 0, RsTemp.Fields("SERVICE_TAX_PER").Value), "0.00")
                txtServiceOn.Text = VB6.Format(mServiceTaxOn * IIf(IsDBNull(RsTemp.Fields("SERVICE_TAX_ON_PER").Value), 100, RsTemp.Fields("SERVICE_TAX_ON_PER").Value) * 0.01, "0.00")
            End If
            If mReverseChargeApp = "Y" Then
                mReverseChargePer = IIf(IsDBNull(RsTemp.Fields("REVERSE_CHARGE_PER").Value), 0, RsTemp.Fields("REVERSE_CHARGE_PER").Value)
                txtProviderPer.Text = VB6.Format(100 - CDbl(mReverseChargePer), "0.00")
                txtRecipientPer.Text = VB6.Format(mReverseChargePer, "0.00")
            Else
                txtProviderPer.Text = "100.00"
                txtRecipientPer.Text = "0.00"
            End If
            CalcTots()
        Else
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchProvidedMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster((txtServProvided.Text), "FIN_SERVPROV_MST", "NAME", , , , SqlStr) = True Then
            txtServProvided.Text = AcName
            txtServProvided_Validating(txtServProvided, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtSTDSDeductOn.Text = VB6.Format(txtSTDSDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSTDSRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtSTDSRate.Text = VB6.Format(txtSTDSRate.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSTRefundNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTRefundNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTRefundNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTRefundNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtTDSAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtTDSAccount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtTDSAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtTDSAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtTDSAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTDSDeductOn.Text = VB6.Format(txtTDSDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTdsRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTdsRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTdsRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTdsRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVD_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVD.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtVDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtVDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtVDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(TxtVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtVNo1.Text = GenPrefixVNo(TxtVDate.Text)
        If lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then GoTo EventExitSub
        If FYChk((TxtVDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVno.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVno.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVno.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrTxtVno
        If lblSR.Text = "" Then
            If Val(txtVno.Text) = 0 Then GoTo EventExitSub
        End If
        txtVno.Text = VB6.Format(txtVno.Text, "00000")
        CheckVouchExistance()
        GoTo EventExitSub
ErrTxtVno:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub CheckVouchExistance()
        On Error GoTo ERR1
        Dim mBookCode As String
        Dim mVDate As String
        Dim mVNo As String
        Dim SqlStr As String = ""
        '    If MainClass.ValidateWithMasterTable(txtPartyName.Text, "Name", "Code", "FIN_SUPP_CUST_MST", PubDBCn, mBookCode) = False Then										
        '        ErrorMsg "Please Select Book First"										
        '        txtPartyName.SetFocus										
        '    End If										
        mVNo = txtVType.Text & txtVNo1.Text & txtVno.Text & txtVNoSuffix.Text
        mVDate = TxtVDate.Text

        If MODIFYMode = True And RsTRNMain.EOF = False Then CurMKey = RsTRNMain.Fields("MKey").Value
        SqlStr = " Select * From FIN_VOUCHER_HDR WHERE " & vbCrLf _
            & " Vno='" & mVNo & "'" & vbCrLf _
            & " AND Booktype='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        '    If lblBookType = ConBankPayment Or lblBookType = ConBankReceipt Or lblBookType = ConJournal Or lblBookType = ConContra Or lblBookType.text = ConPDCPayment Or lblBookType.text = ConPDCReceipt Then										
        '        SqlStr = SqlStr & " AND BookCode=" & mBookCode & ""										
        '    End If										
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTRNMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            '        Clear1										
            Show1()
            If MODIFYMode = True Then SprdMain.Enabled = True
        Else
            If lblSR.Text <> "" And ShowSalary = False Then
                If VB.Left(lblSR.Text, 1) = "E" Or VB.Left(lblSR.Text, 1) = "C" Then
                    Call FillGridFromTMEnCash(CStr(Year(CDate(TxtVDate.Text))), VB.Left(lblSR.Text, 1), Mid(lblSR.Text, 2, 1), CDbl(Mid(lblSR.Text, 3)))
                ElseIf VB.Left(lblSR.Text, 1) = "P" Or VB.Left(lblSR.Text, 1) = "V" Then
                    Call FillGridFromTMPerks((TxtVDate.Text), VB.Left(lblSR.Text, 1), Mid(lblSR.Text, 2, 1), CDbl(Mid(lblSR.Text, 3)))
                ElseIf VB.Left(lblSR.Text, 1) = "F" Then
                    Call FillGridFromTMFullFinal(Year(CDate(TxtVDate.Text)) & VB6.Format(Month(CDate(TxtVDate.Text)), "00"), VB.Left(lblSR.Text, 1), Mid(lblSR.Text, 2, 1), CDbl(Mid(lblSR.Text, 3)), Trim(lblEmpCode.Text))
                ElseIf VB.Left(lblSR.Text, 1) = "Q" Then  ''Voucher Payment										
                    Call FillGridFromVoucherSal(Year(CDate(TxtVDate.Text)) & VB6.Format(Month(CDate(TxtVDate.Text)), "00"), VB.Left(lblSR.Text, 1), Mid(lblSR.Text, 2, 1), CDbl(Mid(lblSR.Text, 3)), Trim(lblEmpCode.Text))
                ElseIf VB.Left(lblSR.Text, 1) = "L" Then  ''LTA Annual										
                    Call FillGridFromTMLTA((TxtVDate.Text), Trim(lblEmpCode.Text), VB.Left(lblSR.Text, 1), Mid(lblSR.Text, 2, 1), CDbl(Mid(lblSR.Text, 3)))
                ElseIf VB.Left(lblSR.Text, 1) = "T" Then  ''LTA Arrear										
                    '                Call FillGridFromTMLTAArrear(txtVDate.Text, Trim(lblEmpCode.text), Left(lblSR.text, 1), Right(lblSR.text, 1))										

                Else
                    Call FillGridFromTMSal(Year(CDate(TxtVDate.Text)) & VB6.Format(Month(CDate(TxtVDate.Text)), "00"), IIf(VB.Left(lblSR.Text, 1) = "A" Or VB.Left(lblSR.Text, 1) = "X", "Y", "N"), VB.Left(lblSR.Text, 1), Mid(lblSR.Text, 2, 1), CDbl(Mid(lblSR.Text, 3)))
                End If
                Call CalcTots()
                ShowSalary = True
            End If
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("VNo Does Not Exist, Click Add To Add", MsgBoxStyle.Information)
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From FIN_VOUCHER_HDR Where mkey='" & CurMKey & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        '    Call CalcAccountBal										
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
        ' Resume										
    End Sub
    Private Sub FillGridFromTMSal(ByRef pYM As String, ByRef pArrear As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mDivisionCode As Double)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTMSal As ADODB.Recordset = Nothing
        Dim mAmount As Double
        Dim mEmpCode As String = ""
        Dim mDeptCode As String
        Dim mNarration As String = ""
        Dim mDivisionDesc As String = ""
        Dim mEmpCategoryName As String
        If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionDesc = Trim(MasterNo)
        End If
        SqlStr = " Select ACM.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, SUM(AMOUNT) AS AMOUNT, DC,PARTICULARS" & vbCrLf _
            & " FROM FIN_TMSal_TRN TRN, FIN_SUPP_CUST_MST ACM" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf _
            & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND YM='" & pYM & "' AND ISARREAR='" & pArrear & "' AND DIV_CODE=" & mDivisionCode & "" & vbCrLf _
            & " AND BookType='" & pBookType & "' AND BookSubType='" & pBookSubType & "'" & vbCrLf _
            & " GROUP BY ACM.SUPP_CUST_CODE,ACM.SUPP_CUST_NAME,DC,PARTICULARS ORDER BY DC DESC,ACM.SUPP_CUST_NAME"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            With SprdMain
                Do While Not RsTMSal.EOF
                    mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("Amount").Value), 0, RsTMSal.Fields("Amount").Value), "0.00"))
                    If mAmount <> 0 Then
                        .Row = .MaxRows
                        .Col = ColPRRowNo
                        .Text = CStr(.Row)
                        .Col = ColDC
                        .Text = IIf(IsDBNull(RsTMSal.Fields("DC").Value), -1, RsTMSal.Fields("DC").Value)
                        .Col = ColAccountName
                        .Text = IIf(IsDBNull(RsTMSal.Fields("SUPP_CUST_NAME").Value), "", RsTMSal.Fields("SUPP_CUST_NAME").Value)
                        .Col = ColParticulars
                        .Text = IIf(IsDBNull(RsTMSal.Fields("PARTICULARS").Value), "", RsTMSal.Fields("PARTICULARS").Value)

                        If CheckExpHead(.Text) = True Then
                            SprdMain.Col = ColCC
                            If Trim(SprdMain.Text) = "" Then
                                SprdMain.Text = "001"
                            End If
                            SprdMain.Col = ColExp
                            If Trim(SprdMain.Text) = "" Then
                                SprdMain.Text = "001"
                            End If
                        End If

                        .Col = ColEmp
                        If MainClass.ValidateWithMasterTable(RsTMSal.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "EMP_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mEmpCode = Trim(MasterNo)
                            .Text = mEmpCode
                        End If

                        If mEmpCode <> "" Then
                            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mDeptCode = Trim(MasterNo)
                                .Col = ColDept
                                .Text = mDeptCode
                            End If
                        End If
                        .Col = ColDivisionCode
                        .Text = CStr(mDivisionCode)
                        .Col = ColAmount
                        .Text = CStr(mAmount)
                        .MaxRows = .MaxRows + 1
                    End If
                    RsTMSal.MoveNext()
                Loop
            End With
            If pBookType = "A" Then
                mNarration = "Arrear to"
            ElseIf pBookType = "S" Then
                mNarration = "Salary to"
            ElseIf pBookType = "O" Then
                mNarration = "Production Incentive to"
            ElseIf pBookType = "XO" Then
                mNarration = "Production Incentive Arrear to"
            End If
            mEmpCategoryName = GetEmployeeCategoryName(pBookSubType)
            mNarration = mNarration & mEmpCategoryName
            '        If pBookSubType = "G" Then										
            '            mNarration = mNarration & " General Staff"										
            '        ElseIf pBookSubType = "P" Then										
            '            mNarration = mNarration & " Production Staff"										
            '        ElseIf pBookSubType = "R" Then										
            '            mNarration = mNarration & " Workers Staff"										
            '        ElseIf pBookSubType = "E" Then										
            '            mNarration = mNarration & " Export Staff"										
            '        ElseIf pBookSubType = "S" Then										
            '            mNarration = mNarration & " R & D Staff"										
            '        ElseIf pBookSubType = "D" Then										
            '            mNarration = mNarration & " Director"										
            '        ElseIf pBookSubType = "T" Then										
            '            mNarration = mNarration & " Trainee Staff"										
            '        End If										
            mNarration = mNarration & "(" & mDivisionDesc & ")"
            mNarration = mNarration & " for the M/o " & MonthName(Month(CDate(TxtVDate.Text))) & ", " & Year(CDate(TxtVDate.Text))
            txtNarration.Text = UCase(mNarration)
        End If
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
    End Sub
    Private Sub FillGridFromTMEnCash(ByRef pYM As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mDivisionCode As Double)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTMSal As ADODB.Recordset = Nothing
        Dim mAmount As Double
        Dim mPFAccountCode As String
        Dim mAccountCode As String = ""
        Dim mDrAccountCode As String
        Dim mCrAccountCode As String
        Dim mPFAccountName As String = ""
        Dim mAccountName As String = ""
        Dim mEmpCode As String
        Dim mDeptCode As String
        Dim mNarration As String
        Dim mNarration1 As String
        Dim mNarration2 As String
        Dim mNarration3 As String
        Dim mNarration4 As String
        Dim mGrossAmount As Double
        Dim mPFAmount As Double
        Dim mNetAmount As Double
        Dim mDedAmount As Double
        Dim mVPFAmount As Double
        Dim cntRow As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSALType As String
        'Dim mEmpCode As String										
        Dim mDivisionDesc As String = ""
        'Dim mESIAmount As String										
        Dim mESIAccountCode As String
        Dim mESIAccountName As String = ""
        Dim mESIAmount As Double
        Dim mESIPayableCode As String = ""
        Dim mESIPayableName As String = ""
        Dim mNarration5 As String
        If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionDesc = Trim(MasterNo)
        End If
        SqlStr = " Select SUM(GROSS_AMOUNT) AS GAMOUNT, SUM(PF_AMOUNT) AS PFAMOUNT, " & vbCrLf & " SUM(NET_AMOUNT) AS NETAMT, SUM(DED_AMOUNT) AS DEDAMOUNT, SUM(VPFAMOUNT) AS VPFAMOUNT,SUM(ESI_AMOUNT) AS ESI_AMOUNT" & vbCrLf & " FROM PAY_ENCASH_TRN TRN, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.PAYYEAR=" & pYM & "" & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE AND BOOKTYPE='" & pBookType & "'" & vbCrLf & " AND EMP_CATG='" & pBookSubType & "' AND DIV_CODE=" & mDivisionCode & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            mGrossAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("GAMOUNT").Value), 0, RsTMSal.Fields("GAMOUNT").Value), "0.00"))
            mPFAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("PFAMOUNT").Value), 0, RsTMSal.Fields("PFAMOUNT").Value), "0.00"))
            mESIAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("ESI_AMOUNT").Value), 0, RsTMSal.Fields("ESI_AMOUNT").Value), "0.00"))
            mNetAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("NETAMT").Value), 0, RsTMSal.Fields("NETAMT").Value), "0.00"))
            mDedAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("DEDAMOUNT").Value), 0, RsTMSal.Fields("DEDAMOUNT").Value), "0.00"))
            mVPFAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("VPFAMOUNT").Value), 0, RsTMSal.Fields("VPFAMOUNT").Value), "0.00"))
            mESIAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("ESI_AMOUNT").Value), 0, RsTMSal.Fields("ESI_AMOUNT").Value), "0.00"))
            mPFAccountCode = GetCategoryAcctCode(pBookSubType, "P")
            mESIAccountCode = GetCategoryAcctCode(pBookSubType, "E")
            mDrAccountCode = GetCategoryAcctCode(pBookSubType, "ED")
            mCrAccountCode = GetCategoryAcctCode(pBookSubType, "EC")
            If MainClass.ValidateWithMasterTable(ConESI, "TYPE", "ACCOUNTCODEPOST", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mESIPayableCode = Trim(MasterNo)
            End If
            If MainClass.ValidateWithMasterTable(mESIPayableCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mESIPayableName = Trim(MasterNo)
            End If
            If MainClass.ValidateWithMasterTable(mPFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPFAccountName = Trim(MasterNo)
            End If
            If MainClass.ValidateWithMasterTable(mESIAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mESIAccountName = Trim(MasterNo)
            End If
            mNarration1 = "Admin Charges on PF : " & VB6.Format(RsCompany.Fields("PFADMINPER").Value + RsCompany.Fields("PFADMINPER_22").Value, "0.00") & " of Basic Salary"
            mNarration2 = "Contribution to EDLI : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00") & " of Basic Salary"
            mNarration3 = "Employer Contribution : Equal to Employee Contribution"
            mNarration4 = "Employer Contribution : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00") & " of ESI Deduction"
            mNarration5 = "ESI Deduction"
            With SprdMain
                cntRow = 1
                .MaxRows = cntRow
                If mESIAmount > 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Cr"
                    .Col = ColAccountName
                    .Text = mESIPayableName
                    .Col = ColParticulars
                    .Text = mNarration5
                    .Col = ColAmount
                    .Text = VB6.Format(mESIAmount, "0.00") '' Format(mGrossAmount * Format(IIf(IsNull(RsCompany!PFADMINPER), 0, RsCompany!PFADMINPER), "0.00") / 100, "0.00")										
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Cr"
                    .Col = ColAccountName
                    .Text = mESIPayableName
                    .Col = ColParticulars
                    .Text = mNarration4
                    .Col = ColAmount
                    .Text = VB6.Format(System.Math.Round(mESIAmount * 100 / 1.75, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00") '' Format(mGrossAmount * Format(IIf(IsNull(RsCompany!PFADMINPER), 0, RsCompany!PFADMINPER), "0.00") / 100, "0.00")										
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Dr"
                    .Col = ColAccountName
                    .Text = mESIAccountName
                    .Col = ColParticulars
                    .Text = mNarration4
                    .Col = ColAmount
                    .Text = VB6.Format(System.Math.Round(mESIAmount * 100 / 1.75, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00") '' Format(mGrossAmount * Format(IIf(IsNull(RsCompany!PFADMINPER), 0, RsCompany!PFADMINPER), "0.00") / 100, "0.00")										
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value) <> 0 And mPFAmount > 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Dr"
                    .Col = ColAccountName
                    .Text = mPFAccountName
                    .Col = ColParticulars
                    .Text = mNarration1
                    .Col = ColAmount
                    .Text = VB6.Format(mGrossAmount * CDbl(VB6.Format(RsCompany.Fields("PFADMINPER").Value + RsCompany.Fields("PFADMINPER_22").Value, "0.00")) / 100, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value) <> 0 And mPFAmount > 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Dr"
                    .Col = ColAccountName
                    .Text = mPFAccountName
                    .Col = ColParticulars
                    .Text = mNarration2
                    .Col = ColAmount
                    .Text = VB6.Format(mGrossAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00")) / 100, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If mPFAmount <> 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Dr"
                    .Col = ColAccountName
                    .Text = mPFAccountName
                    .Col = ColParticulars
                    .Text = mNarration3
                    .Col = ColAmount
                    .Text = VB6.Format(mPFAmount, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If mGrossAmount <> 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Dr"
                    If MainClass.ValidateWithMasterTable(mDrAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountName = Trim(MasterNo)
                    End If
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = ""
                    .Col = ColAmount
                    .Text = VB6.Format(mGrossAmount, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If mNetAmount <> 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Cr"
                    If MainClass.ValidateWithMasterTable(mCrAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountName = Trim(MasterNo)
                    End If
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = ""
                    .Col = ColAmount
                    .Text = VB6.Format(mNetAmount, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value) <> 0 And mPFAmount > 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Cr"
                    .Col = ColAccountName
                    .Text = mPFAccountName
                    .Col = ColParticulars
                    .Text = mNarration1
                    .Col = ColAmount
                    .Text = VB6.Format(mGrossAmount * CDbl(VB6.Format(RsCompany.Fields("PFADMINPER").Value + RsCompany.Fields("PFADMINPER_22").Value, "0.00")) / 100, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value) <> 0 And mPFAmount > 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Cr"
                    .Col = ColAccountName
                    .Text = mPFAccountName
                    .Col = ColParticulars
                    .Text = mNarration2
                    .Col = ColAmount
                    .Text = VB6.Format(mGrossAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00")) / 100, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If mPFAmount <> 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Cr"
                    .Col = ColAccountName
                    .Text = mPFAccountName
                    .Col = ColParticulars
                    .Text = mNarration3
                    .Col = ColAmount
                    .Text = VB6.Format(mPFAmount, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If mPFAmount <> 0 Then
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Cr"
                    .Col = ColAccountName
                    .Text = mPFAccountName
                    .Col = ColParticulars
                    .Text = ""
                    .Col = ColAmount
                    .Text = VB6.Format(mPFAmount, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                End If
                .MaxRows = cntRow
                If mVPFAmount <> 0 And mPFAmount > 0 Then
                    SqlStr = " SELECT ACCOUNTCODEPOST " & vbCrLf & " FROM PAY_SALARYHEAD_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TYPE=" & ConVPFAllw & ""
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODEPOST").Value), "-1", RsTemp.Fields("ACCOUNTCODEPOST").Value)
                    End If
                    If mAccountCode <> "-1" Then
                        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountName = Trim(MasterNo)
                        End If
                        .Row = cntRow
                        .Col = 1
                        .Col = ColPRRowNo
                        .Text = CStr(cntRow)
                        .Col = ColDC
                        .Text = "Cr"
                        .Col = ColAccountName
                        .Text = mAccountName
                        .Col = ColParticulars
                        .Text = ""
                        .Col = ColAmount
                        .Text = VB6.Format(mVPFAmount, "0.00")
                        .Col = ColDivisionCode
                        .Text = CStr(mDivisionCode)
                        cntRow = cntRow + 1
                    End If
                End If
                .MaxRows = cntRow
                If mDedAmount <> 0 Then
                    SqlStr = "SELECT EMP_CODE, AMOUNT, ADD_DEDUCTCODE, ACCOUNTCODEPOST, TYPE " & vbCrLf & " FROM PAY_MONTHLY_TRN TRN, PAY_SALARYHEAD_MST MST " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.PAYYEAR=" & pYM & " " & vbCrLf & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE=MST.CODE" & vbCrLf & " AND SAL_FLAG='E' AND AMOUNT<>0"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        Do While RsTemp.EOF = False
                            mSALType = IIf(IsDBNull(RsTemp.Fields("Type").Value), "-1", RsTemp.Fields("Type").Value)
                            mEmpCode = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "-1", RsTemp.Fields("EMP_CODE").Value)
                            Select Case mSALType
                                Case CStr(ConAdvance)
                                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='L'") = True Then
                                        mAccountName = Trim(MasterNo)
                                    End If
                                Case CStr(ConLoan)
                                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "ADV_ACCOUNT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                        mAccountCode = Trim(MasterNo)
                                    End If
                                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                        mAccountName = Trim(MasterNo)
                                    End If
                                Case CStr(ConImprest)
                                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='I'") = True Then
                                        mAccountName = Trim(MasterNo)
                                    End If
                                Case Else
                                    mAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODEPOST").Value), "-1", RsTemp.Fields("ACCOUNTCODEPOST").Value)
                                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                        mAccountName = Trim(MasterNo)
                                    End If
                            End Select
                            .Row = cntRow
                            .Col = 1
                            .Col = ColPRRowNo
                            .Text = CStr(cntRow)
                            .Col = ColDC
                            .Text = "Cr"
                            .Col = ColAccountName
                            .Text = mAccountName
                            .Col = ColParticulars
                            .Text = ""
                            .Col = ColAmount
                            .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), "-1", RsTemp.Fields("Amount").Value), "0.00")
                            .Col = ColDivisionCode
                            .Text = CStr(mDivisionCode)
                            mAccountName = ""
                            mAccountCode = ""
                            cntRow = cntRow + 1
                            .MaxRows = cntRow
                            RsTemp.MoveNext()
                        Loop
                    Else
                        '                    cntRow = cntRow + 1										
                        '                    .MaxRows = cntRow										
                    End If
                Else
                    '                cntRow = cntRow + 1										
                End If
            End With
            mNarration = "Leave Encash of "
            mNarration = mNarration & GetEmployeeCategoryName(pBookSubType)
            '        If pBookSubType = "G" Then										
            '            mNarration = mNarration & " General Staff "										
            '        ElseIf pBookSubType = "P" Then										
            '            mNarration = mNarration & " Production Staff"										
            '        ElseIf pBookSubType = "R" Then										
            '            mNarration = mNarration & " Workers Staff"										
            '        ElseIf pBookSubType = "E" Then										
            '            mNarration = mNarration & " Export Staff"										
            '        ElseIf pBookSubType = "S" Then										
            '            mNarration = mNarration & " R & D Staff"										
            '        ElseIf pBookSubType = "D" Then										
            '            mNarration = mNarration & " Director"										
            '        ElseIf pBookSubType = "T" Then										
            '            mNarration = mNarration & " Trainee Staff"										
            '        End If										
            mNarration = mNarration & " (" & mDivisionDesc & ")"
            mNarration = mNarration & " for the Year " & Year(CDate(TxtVDate.Text))
            txtNarration.Text = UCase(mNarration)
        End If
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
    End Sub
    Private Sub FillGridFromTMPerks(ByRef pSalDate As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mDivisionCode As Double)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTMSal As ADODB.Recordset = Nothing
        Dim mAmount As Double
        Dim mAccountCode As String
        Dim mSalaryHeadCode As String
        Dim mNarration As String
        Dim cntRow As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAccountName As String = ""
        Dim mDivisionDesc As String = ""
        Dim mEmpCode As String

        If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionDesc = Trim(MasterNo)
        End If
        cntRow = 1
        SqlStr = " Select SUM(AMOUNT) AS AMOUNT, ADD_DEDUCTCODE" & vbCrLf & " FROM PAY_PERKS_TRN  TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')='" & VB6.Format(pSalDate, "YYYYMM") & "' AND TRN.BOOKTYPE='" & pBookType & "' AND TRN.DIV_CODE=" & mDivisionCode & ""
        If pBookSubType = "X" Then
            SqlStr = SqlStr & vbCrLf & " AND PAID_WEEK=1"
        ElseIf pBookSubType = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND PAID_WEEK=2"
        Else
            SqlStr = SqlStr & vbCrLf & " AND PAID_WEEK=3"
        End If
        SqlStr = SqlStr & vbCrLf & " GROUP BY ADD_DEDUCTCODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            Do While RsTMSal.EOF = False
                mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("Amount").Value), 0, RsTMSal.Fields("Amount").Value), "0.00"))
                mSalaryHeadCode = IIf(IsDBNull(RsTMSal.Fields("ADD_DEDUCTCODE").Value), "-1", RsTMSal.Fields("ADD_DEDUCTCODE").Value)
                If MainClass.ValidateWithMasterTable(mSalaryHeadCode, "CODE", "ACCOUNTCODEPOST", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = Trim(MasterNo)
                Else
                    mAccountCode = "-1"
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                Else
                    mAccountName = ""
                End If
                mNarration = " for the Month " & VB6.Format(pSalDate, "MMMM-YYYY") & "(" & mDivisionDesc & ")"
                With SprdMain
                    .MaxRows = cntRow
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Dr"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = mNarration
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
                RsTMSal.MoveNext()
            Loop
        End If

        SqlStr = " Select SUM(AMOUNT) AS AMOUNT, EMP_CODE" & vbCrLf _
            & " FROM PAY_PERKS_TRN  TRN" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')='" & VB6.Format(pSalDate, "YYYYMM") & "'  AND TRN.BOOKTYPE='" & pBookType & "'"

        If pBookSubType = "X" Then
            SqlStr = SqlStr & vbCrLf & " AND PAID_WEEK=1"
        ElseIf pBookSubType = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND PAID_WEEK=2"
        Else
            SqlStr = SqlStr & vbCrLf & " AND PAID_WEEK=3"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY EMP_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value), "0.00"))
                'mAccountCode = GetCategoryAcctCode("G", "SC") ''IIf(IsNull(RsCompany!POSTSTAFFCACCOUNTCODE), -1, RsCompany!POSTSTAFFCACCOUNTCODE)										
                'If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    mAccountName = Trim(MasterNo)
                'End If

                mEmpCode = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)

                mAccountCode = GetEmpSalaryAcctCode(mEmpCode)

                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If

                mNarration = " for the Month " & VB6.Format(pSalDate, "MMMM-YYYY") & "(" & mDivisionDesc & ")"
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "Cr"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = mNarration
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    .Col = ColDivisionCode
                    .Text = CStr(mDivisionCode)
                    mAccountName = ""
                    mAccountCode = ""
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
                RsTemp.MoveNext()
            Loop
        End If
        mNarration = " for the Month " & VB6.Format(pSalDate, "MMMM-YYYY") & "(" & mDivisionDesc & ")"
        txtNarration.Text = UCase(mNarration)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
    End Sub
    Private Sub FillGridFromTMLTA(ByRef pSalDate As String, ByRef pEmpCode As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mDivisionCode As Double)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTMSal As ADODB.Recordset = Nothing
        Dim mAmount As Double
        Dim mAccountCode As String
        Dim mEmpName As String
        Dim mNarration As String = ""
        Dim cntRow As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAccountName As String
        cntRow = 1
        mAmount = 0
        SqlStr = " Select NET_LTA_AMOUNT AS AMOUNT,FROM_DATE,TO_DATE" & vbCrLf & " FROM PAY_LTA_HDR  TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & lblELYear.Text & "" '& vbCrLf |            & " AND TO_CHAR(TRN.TO_DATE,'YYYYMM')='" & vb6.Format(pSalDate, "YYYYMM") & "'"										
        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            mAmount = CDbl(VB6.Format(System.Math.Round(IIf(IsDBNull(RsTMSal.Fields("Amount").Value), 0, RsTMSal.Fields("Amount").Value), 0), "0.00"))
            If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpName = Trim(MasterNo)
            Else
                mEmpName = ""
            End If
            mNarration = "LTA of " & mEmpName & " for the Period From : " & VB6.Format(IIf(IsDBNull(RsTMSal.Fields("FROM_DATE").Value), "", RsTMSal.Fields("FROM_DATE").Value), "DD/MM/YYYY")
            mNarration = mNarration & " To : " & VB6.Format(IIf(IsDBNull(RsTMSal.Fields("TO_DATE").Value), "", RsTMSal.Fields("TO_DATE").Value), "DD/MM/YYYY")
        End If
        mAccountCode = ""
        mAccountCode = GetCategoryAcctCode(pBookSubType, "LD")
        '    If pBookSubType = "G" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFLTC_DEBITCODE), -1, RsCompany!POSTSTAFFLTC_DEBITCODE)										
        '    ElseIf pBookSubType = "P" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPRODLTC_DEBITCODE), -1, RsCompany!POSTPRODLTC_DEBITCODE)										
        '    ElseIf pBookSubType = "E" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTEXPORTLTC_DEBITCODE), -1, RsCompany!POSTEXPORTLTC_DEBITCODE)										
        '    ElseIf pBookSubType = "R" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTWLTC_DEBITCODE), -1, RsCompany!POSTWLTC_DEBITCODE)										
        '    ElseIf pBookSubType = "S" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTRNDLTC_DEBITCODE), -1, RsCompany!POSTRNDLTC_DEBITCODE)										
        '    ElseIf pBookSubType = "D" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTORLTC_DEBITCODE), -1, RsCompany!POSTDIRECTORLTC_DEBITCODE)										
        '    ElseIf pBookSubType = "T" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTTRNLTC_DEBITCODE), -1, RsCompany!POSTTRNLTC_DEBITCODE)										
        '    End If										
        If mAccountCode = "" Or Trim(mAccountCode) = "-1" Then MsgInformation("Please Defined LTA Debit Head in System Preference") : Exit Sub
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        Else
            mAccountName = ""
        End If
        With SprdMain
            .MaxRows = cntRow
            .Row = cntRow
            .Col = 1
            .Col = ColPRRowNo
            .Text = CStr(cntRow)
            .Col = ColDC
            .Text = "Dr"
            .Col = ColAccountName
            .Text = mAccountName
            .Col = ColEmp
            .Text = pEmpCode
            If CheckMst(ColEmp, "PAY_EMPLOYEE_MST", "EMP_CODE") = False Then GoTo ErrPart
            '        .Col = ColDept										
            '        .Text = pEMPCode										
            '										
            '        .Col = ColCC										
            '        .Text = pEMPCode										
            .Col = ColDivisionCode
            .Text = CStr(mDivisionCode)
            .Col = ColParticulars
            .Text = mNarration
            .Col = ColAmount
            .Text = VB6.Format(mAmount, "0.00")
            cntRow = cntRow + 1
            .MaxRows = cntRow
        End With
        '    mAccountCode = ""										
        '										
        '    If pBookSubType = "G" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFLTC_CREDITCODE), -1, RsCompany!POSTSTAFFLTC_CREDITCODE)										
        '    ElseIf pBookSubType = "P" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPRODLTC_CREDITCODE), -1, RsCompany!POSTPRODLTC_CREDITCODE)										
        '    ElseIf pBookSubType = "E" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTEXPORTLTC_CREDITCODE), -1, RsCompany!POSTEXPORTLTC_CREDITCODE)										
        '    ElseIf pBookSubType = "R" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTWLTC_CREDITCODE), -1, RsCompany!POSTWLTC_CREDITCODE)										
        '    ElseIf pBookSubType = "S" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTRNDLTC_CREDITCODE), -1, RsCompany!POSTRNDLTC_CREDITCODE)										
        '    ElseIf pBookSubType = "D" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTORLTC_CREDITCODE), -1, RsCompany!POSTDIRECTORLTC_CREDITCODE)										
        '    ElseIf pBookSubType = "T" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTTRNLTC_CREDITCODE), -1, RsCompany!POSTTRNLTC_CREDITCODE)										
        '    End If										
        '										
        '    If mAccountCode = "" Or Trim(mAccountCode) = "-1" Then MsgInformation "Please Defined LTA Credit Head in System Preference": Exit Sub										
        '										
        '    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then										
        '        mAccountName = Trim(MasterNo)										
        '    End If										
        '										
        '    With SprdMain										
        '        .Row = cntRow										
        '        .Col = 1										
        '        .Col = ColPRRowNo										
        '        .Text = cntRow										
        '										
        '        .Col = ColDC										
        '        .Text = "Cr"										
        '										
        '        .Col = ColAccountName										
        '        .Text = mAccountName										
        '										
        '        .Col = ColEmp										
        '        .Text = pEMPCode										
        '										
        '        If CheckMst(ColEmp, "PAY_EMPLOYEE_MST", "EMP_CODE") = False Then GoTo ErrPart										
        '										
        '        .Col = ColParticulars										
        '        .Text = mNarration										
        '										
        '        .Col = ColAmount										
        '        .Text = Format(mAmount, "0.00")										
        '										
        '        mAccountName = ""										
        '        mAccountCode = ""										
        '        cntRow = cntRow + 1										
        '        .MaxRows = cntRow										
        '    End With										
        txtNarration.Text = UCase(mNarration)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
    End Sub
    Private Sub FillGridFromTMFullFinal(ByRef pSalDate As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mDivisionCode As Double, ByRef pEmpCode As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTMSal As ADODB.Recordset = Nothing
        Dim mAmount As Double
        Dim mAccountCode As String
        Dim mSalaryHeadCode As String
        Dim mNarration As String = ""
        Dim cntRow As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAccountName As String = ""
        Dim mType As Integer
        Dim mVPFAmount As Double
        Dim mPFAmount As Double
        Dim mESIAmount As Double
        Dim mEmpName As String
        Dim pNarration As String
        Dim pDeptCode As String
        Dim mPFAccountCode As String = ""
        Dim mNarration1 As String
        Dim mNarration2 As String
        Dim mNarration3 As String
        Dim mPFAccountName As String = ""
        Dim mESIAccountCode As String = ""
        Dim mWFAccountCode As String = ""
        Dim mWFAmount As Double
        Dim mCCCode As String
        Dim mExpCode As String
        Dim mSalaryPayableAmount As Double
        Dim mPensionAmount As Double
        mExpCode = "001"
        mSalaryPayableAmount = 0
        cntRow = 1
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpName = MasterNo
        Else
            mEmpName = "-1"
        End If
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pDeptCode = MasterNo
        Else
            pDeptCode = ""
        End If
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "COST_CENTER_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCCCode = MasterNo
        Else
            mCCCode = "001"
        End If
        pNarration = " FULL & FINAL SETTLEMENT OF " & mEmpName & " (" & pEmpCode & ")"
        txtNarration.Text = pNarration
        mPFAmount = 0
        mESIAmount = 0
        SqlStr = " Select * " & vbCrLf & " FROM PAY_FFSETTLE_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.EMP_CODE='" & VB6.Format(pEmpCode, "000000") & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            'GROSS SALARY & Arrear ....										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("GROSS_SALARY").Value), 0, RsTemp.Fields("GROSS_SALARY").Value), "0.00"))
            mAmount = System.Math.Round(mAmount, 0)
            If mAmount <> 0 Then
                mAccountCode = GetCategoryAcctCode(pBookSubType, "SD")
                '            If pBookSubType = "G" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFDACCOUNTCODE), -1, RsCompany!POSTSTAFFDACCOUNTCODE)										
                '            ElseIf pBookSubType = "P" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTPRODDACCOUNTCODE), -1, RsCompany!POSTPRODDACCOUNTCODE)										
                '            ElseIf pBookSubType = "E" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTEXPDACCOUNTCODE), -1, RsCompany!POSTEXPDACCOUNTCODE)										
                '            ElseIf pBookSubType = "R" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTWORKERDACCOUNTCODE), -1, RsCompany!POSTWORKERDACCOUNTCODE)										
                '            ElseIf pBookSubType = "S" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTRNDDACCOUNTCODE), -1, RsCompany!POSTRNDDACCOUNTCODE)										
                '            ElseIf pBookSubType = "D" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTOR_DEBITCODE), -1, RsCompany!POSTDIRECTOR_DEBITCODE)										
                '            ElseIf pBookSubType = "T" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!PostTRNDAccountCode), -1, RsCompany!PostTRNDAccountCode)										
                '            End If										
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                Call InsertIntoGrid(cntRow, "DR", mAccountName, "GROSS SALARY", pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
            End If
            ''Salary Arrear Amount										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ARREAR_SAL").Value), 0, RsTemp.Fields("ARREAR_SAL").Value), "0.00"))
            Call InsertIntoGrid(cntRow, "DR", mAccountName, "ARREAR", pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
            ''Other Amount..										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("OTHERS_AMOUNT").Value), 0, RsTemp.Fields("OTHERS_AMOUNT").Value), "0.00"))
            Call InsertIntoGrid(cntRow, "DR", mAccountName, "OTHER DEDUCTION", pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
            mAccountName = ""
            mAccountCode = ""
            ''Notice Pay / Ex-Gratia..										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("NOTICE_AMOUNT").Value), 0, RsTemp.Fields("NOTICE_AMOUNT").Value), "0.00"))
            If mAmount < 0 Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_NOTICEPAY").Value), -1, RsCompany.Fields("POST_NOTICEPAY").Value)
            Else
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_EXGRATIA").Value), -1, RsCompany.Fields("POST_EXGRATIA").Value)
            End If
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = Trim(MasterNo)
            End If
            Call InsertIntoGrid(cntRow, "DR", mAccountName, IIf(mAmount < 0, "NOTICE PAY", "EX-GRATIA"), pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
            mAccountName = ""
            mAccountCode = ""
            'Incentive..										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("INC_AMT_FORMON").Value), 0, RsTemp.Fields("INC_AMT_FORMON").Value), "0.00"))
            mAmount = mAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("INC_AMT_PREMON").Value), 0, RsTemp.Fields("INC_AMT_PREMON").Value), "0.00"))
            mAmount = mAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ARREAR_INC").Value), 0, RsTemp.Fields("ARREAR_INC").Value), "0.00"))
            If mAmount <> 0 Then
                mAccountCode = GetCategoryAcctCode(pBookSubType, "ID")
                '            If pBookSubType = "G" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFINC_DEBITCODE), -1, RsCompany!POSTSTAFFINC_DEBITCODE)										
                '            ElseIf pBookSubType = "P" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTPRODINC_DEBITCODE), -1, RsCompany!POSTPRODINC_DEBITCODE)										
                '            ElseIf pBookSubType = "E" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTEXPORTINC_DEBITCODE), -1, RsCompany!POSTEXPORTINC_DEBITCODE)										
                '            ElseIf pBookSubType = "R" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTWINC_DEBITCODE), -1, RsCompany!POSTWINC_DEBITCODE)										
                '            ElseIf pBookSubType = "S" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTRNDINC_DEBITCODE), -1, RsCompany!POSTRNDINC_DEBITCODE)										
                '            ElseIf pBookSubType = "D" Then										
                '                mAccountCode = "-1"     ''IIf(IsNull(RsCompany!POSTDIRECTOR_DEBITCODE), -1, RsCompany!POSTDIRECTOR_DEBITCODE)										
                '            ElseIf pBookSubType = "T" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTTRNINC_DEBITCODE), -1, RsCompany!POSTTRNINC_DEBITCODE)										
                '            End If										
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                Call InsertIntoGrid(cntRow, "DR", mAccountName, "INCENTIVE AMOUNT", pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                mAccountName = ""
                mAccountCode = ""
            End If
            ''Leave Encash										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("EL_AMOUNT").Value), 0, RsTemp.Fields("EL_AMOUNT").Value), "0.00"))
            If mAmount <> 0 Then
                mAccountCode = GetCategoryAcctCode(pBookSubType, "ED")
                '            If pBookSubType = "G" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_EL_S_DCODE), -1, RsCompany!POST_EL_S_DCODE)										
                '            ElseIf pBookSubType = "P" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_EL_P_DCODE), -1, RsCompany!POST_EL_P_DCODE)										
                '            ElseIf pBookSubType = "E" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_EL_E_DCODE), -1, RsCompany!POST_EL_E_DCODE)										
                '            ElseIf pBookSubType = "R" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_EL_W_DCODE), -1, RsCompany!POST_EL_W_DCODE)										
                '            ElseIf pBookSubType = "S" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_EL_R_DCODE), -1, RsCompany!POST_EL_R_DCODE)										
                '            ElseIf pBookSubType = "D" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_EL_D_DCODE), -1, RsCompany!POST_EL_D_DCODE)										
                '            ElseIf pBookSubType = "T" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_EL_T_DCODE), -1, RsCompany!POST_EL_T_DCODE)										
                '            End If										
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                Call InsertIntoGrid(cntRow, "DR", mAccountName, "LEAVE ENCASHMENT @DAYS " & VB6.Format(IIf(IsDBNull(RsTemp.Fields("EL_DAYS").Value), 0, RsTemp.Fields("EL_DAYS").Value), "0.0"), pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                mAccountName = ""
                mAccountCode = ""
            End If
            ''L.T.A.										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("LTC_AMOUNT").Value), 0, RsTemp.Fields("LTC_AMOUNT").Value), "0.00"))
            If mAmount <> 0 Then
                mAccountCode = GetCategoryAcctCode(pBookSubType, "LD")
                '            If pBookSubType = "G" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFLTC_DEBITCODE), -1, RsCompany!POSTSTAFFLTC_DEBITCODE)										
                '            ElseIf pBookSubType = "P" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTPRODLTC_DEBITCODE), -1, RsCompany!POSTPRODLTC_DEBITCODE)										
                '            ElseIf pBookSubType = "E" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTEXPORTLTC_DEBITCODE), -1, RsCompany!POSTEXPORTLTC_DEBITCODE)										
                '            ElseIf pBookSubType = "R" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTWLTC_DEBITCODE), -1, RsCompany!POSTWLTC_DEBITCODE)										
                '            ElseIf pBookSubType = "S" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTRNDLTC_DEBITCODE), -1, RsCompany!POSTRNDLTC_DEBITCODE)										
                '            ElseIf pBookSubType = "D" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTORLTC_DEBITCODE), -1, RsCompany!POSTDIRECTORLTC_DEBITCODE)										
                '            ElseIf pBookSubType = "T" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTTRNLTC_DEBITCODE), -1, RsCompany!POSTTRNLTC_DEBITCODE)										
                '            End If										
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                Call InsertIntoGrid(cntRow, "DR", mAccountName, "LTA @MONTH " & VB6.Format(IIf(IsDBNull(RsTemp.Fields("LTC_MONTH").Value), 0, RsTemp.Fields("LTC_MONTH").Value), "0.0"), pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                mAccountName = ""
                mAccountCode = ""
            End If
            ''BONUS PREVIOUS YEAR										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("BONUS_FORYEAR").Value), 0, RsTemp.Fields("BONUS_FORYEAR").Value), "0.00"))
            If mAmount <> 0 Then
                mAccountCode = GetCategoryAcctCode(pBookSubType, "BC")
                '            If pBookSubType = "G" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFBONUS_CREDITCODE), -1, RsCompany!POSTSTAFFBONUS_CREDITCODE)										
                '            ElseIf pBookSubType = "P" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTPRODBONUS_CREDITCODE), -1, RsCompany!POSTPRODBONUS_CREDITCODE)										
                '            ElseIf pBookSubType = "E" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTEXPORTBONUS_CREDITCODE), -1, RsCompany!POSTEXPORTBONUS_CREDITCODE)										
                '            ElseIf pBookSubType = "R" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTWBONUS_CREDITCODE), -1, RsCompany!POSTWBONUS_CREDITCODE)										
                '            ElseIf pBookSubType = "S" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTRNDBONUS_CREDITCODE), -1, RsCompany!POSTRNDBONUS_CREDITCODE)										
                '            ElseIf pBookSubType = "D" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTORBONUS_CREDITCODE), -1, RsCompany!POSTDIRECTORBONUS_CREDITCODE)										
                '            ElseIf pBookSubType = "T" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTTRNBONUS_CREDITCODE), -1, RsCompany!POSTTRNBONUS_CREDITCODE)										
                '            End If										
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                Call InsertIntoGrid(cntRow, "DR", mAccountName, "PREVIOUS YEAR BONUS @" & VB6.Format(IIf(IsDBNull(RsTemp.Fields("BONUS_PER_FORYEAR").Value), 0, RsTemp.Fields("BONUS_PER_FORYEAR").Value), "0.0") & "%", pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                mAccountName = ""
                mAccountCode = ""
            End If
            ''BONUS CURRENT YEAR										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("BONUS_CURRYEAR").Value), 0, RsTemp.Fields("BONUS_CURRYEAR").Value), "0.00"))
            If mAmount <> 0 Then
                mAccountCode = GetCategoryAcctCode(pBookSubType, "BD")
                '            If pBookSubType = "G" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFBONUS_DEBITCODE), -1, RsCompany!POSTSTAFFBONUS_DEBITCODE)										
                '            ElseIf pBookSubType = "P" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTPRODBONUS_DEBITCODE), -1, RsCompany!POSTPRODBONUS_DEBITCODE)										
                '            ElseIf pBookSubType = "E" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTEXPORTBONUS_DEBITCODE), -1, RsCompany!POSTEXPORTBONUS_DEBITCODE)										
                '            ElseIf pBookSubType = "R" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTWBONUS_DEBITCODE), -1, RsCompany!POSTWBONUS_DEBITCODE)										
                '            ElseIf pBookSubType = "S" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTRNDBONUS_DEBITCODE), -1, RsCompany!POSTRNDBONUS_DEBITCODE)										
                '            ElseIf pBookSubType = "D" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTORBONUS_DEBITCODE), -1, RsCompany!POSTDIRECTORBONUS_DEBITCODE)										
                '            ElseIf pBookSubType = "T" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POSTTRNBONUS_DEBITCODE), -1, RsCompany!POSTTRNBONUS_DEBITCODE)										
                '            End If										
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                Call InsertIntoGrid(cntRow, "DR", mAccountName, "CURRENT YEAR BONUS @" & VB6.Format(IIf(IsDBNull(RsTemp.Fields("BONUS_PER_CURRYEAR").Value), 0, RsTemp.Fields("BONUS_PER_CURRYEAR").Value), "0.0") & "%", pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                mAccountName = ""
                mAccountCode = ""
            End If
            ''Gratuity..										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("GRATUITY_AMOUNT").Value), 0, RsTemp.Fields("GRATUITY_AMOUNT").Value), "0.00"))
            If mAmount <> 0 Then
                mAccountCode = GetCategoryAcctCode(pBookSubType, "GD")
                '            If pBookSubType = "G" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_GRATUITY_S_DCODE), -1, RsCompany!POST_GRATUITY_S_DCODE)										
                '            ElseIf pBookSubType = "P" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_GRATUITY_P_DCODE), -1, RsCompany!POST_GRATUITY_P_DCODE)										
                '            ElseIf pBookSubType = "E" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_GRATUITY_E_DCODE), -1, RsCompany!POST_GRATUITY_E_DCODE)										
                '            ElseIf pBookSubType = "R" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_GRATUITY_W_DCODE), -1, RsCompany!POST_GRATUITY_W_DCODE)										
                '            ElseIf pBookSubType = "S" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_GRATUITY_R_DCODE), -1, RsCompany!POST_GRATUITY_R_DCODE)										
                '            ElseIf pBookSubType = "D" Then										
                '                mAccountCode = IIf(IsNull(RsCompany!POST_GRATUITY_D_DCODE), -1, RsCompany!POST_GRATUITY_D_DCODE)										
                '            End If										
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                Call InsertIntoGrid(cntRow, "DR", mAccountName, "GRATUITY @MONTHS " & VB6.Format(IIf(IsDBNull(RsTemp.Fields("GRATUITY_MONTH").Value), 0, RsTemp.Fields("GRATUITY_MONTH").Value), "0.0") & "%", pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                mAccountName = ""
                mAccountCode = ""
            End If
            ''Salary Payable										
            mSalaryPayableAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("NET_SALARY").Value), 0, RsTemp.Fields("NET_SALARY").Value), "0.00"))
        End If
        SqlStr = " Select SUM(PAYABLEAMOUNT) AS PAYABLEAMOUNT, SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_FFSETTLE_DET ID, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE ID.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND ID.SALHEADCODE=SMST.CODE" & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND SMST.ADDDEDUCT=" & ConDeduct & ""
        SqlStr = SqlStr & vbCrLf & " AND PAYABLEAMOUNT<>0"
        SqlStr = SqlStr & vbCrLf & " GROUP BY SALHEADCODE,TYPE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            Do While RsTMSal.EOF = False
                mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("PayableAmount").Value), 0, RsTMSal.Fields("PayableAmount").Value), "0.00"))
                If mAmount <> 0 Then
                    mSalaryHeadCode = IIf(IsDBNull(RsTMSal.Fields("SALHEADCODE").Value), "-1", RsTMSal.Fields("SALHEADCODE").Value)
                    mType = IIf(IsDBNull(RsTMSal.Fields("Type").Value), "-1", RsTMSal.Fields("Type").Value)
                    If MainClass.ValidateWithMasterTable(mSalaryHeadCode, "CODE", "ACCOUNTCODEPOST", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = Trim(MasterNo)
                    Else
                        mAccountCode = "-1"
                    End If
                    If mType = ConPF Then
                        mPFAmount = mAmount
                        mPFAccountCode = mAccountCode
                    ElseIf mType = ConESI Then
                        mESIAmount = mAmount
                        mESIAccountCode = mAccountCode
                    ElseIf mType = ConVPFAllw Then
                        mVPFAmount = mAmount
                    ElseIf mType = ConWelfare Then
                        mWFAmount = mAmount
                        mWFAccountCode = mAccountCode
                    End If
                    mAccountName = ""
                    mAccountCode = ""
                End If
                RsTMSal.MoveNext()
            Loop
        End If
        mAccountCode = GetCategoryAcctCode(pBookSubType, "P")
        '    If pBookSubType = "G" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPFCONTCR), -1, RsCompany!POSTPFCONTCR)										
        '    ElseIf pBookSubType = "P" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPFPRODCONTCR), -1, RsCompany!POSTPFPRODCONTCR)										
        '    ElseIf pBookSubType = "E" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPFEXPORTCONTCR), -1, RsCompany!POSTPFEXPORTCONTCR)										
        '    ElseIf pBookSubType = "R" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPFWCONTCR), -1, RsCompany!POSTPFWCONTCR)										
        '    ElseIf pBookSubType = "S" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPFRNDCONTCR), -1, RsCompany!POSTPFRNDCONTCR)										
        '    ElseIf pBookSubType = "D" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPFDIRECTORACCOUNTCODE), -1, RsCompany!POSTPFDIRECTORACCOUNTCODE)										
        '    ElseIf pBookSubType = "T" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!PostTRNPFContCr), -1, RsCompany!PostTRNPFContCr)										
        '    End If										
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        End If
        mNarration1 = "Admin Charges on PF : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value), "0.00") & " of Basic Salary" ''+ IIf(IsNull(RsCompany!PFADMINPER_22), 0, RsCompany!PFADMINPER_22)										
        mNarration2 = "Contribution to EDLI : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value) + IIf(IsDBNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value), "0.00") & " of Basic Salary"
        mNarration3 = "Employer Contribution : Equal to Employee Contribution"
        mAmount = CDbl(VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value), "0.00")) / 100, "0.00")) '' + IIf(IsNull(RsCompany!PFADMINPER_22), 0, RsCompany!PFADMINPER_22)										
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mPensionAmount = GetPensionWages(Val(pSalDate), "F", mDivisionCode)
        mAmount = CDbl(VB6.Format(System.Math.Round(mPensionAmount, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value) + IIf(IsDBNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration2, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mAmount = mPFAmount
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration3, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mNarration1 = "Employer Contribution : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00") & " of ESI Deduction"
        mAccountCode = GetCategoryAcctCode(pBookSubType, "E")
        '    If pBookSubType = "G" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTESICONTCR), -1, RsCompany!POSTESICONTCR)										
        '    ElseIf pBookSubType = "P" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTESIPRODCONTCR), -1, RsCompany!POSTESIPRODCONTCR)										
        '    ElseIf pBookSubType = "E" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTESIEXPORTCONTCR), -1, RsCompany!POSTESIEXPORTCONTCR)										
        '    ElseIf pBookSubType = "R" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTESIWCONTCR), -1, RsCompany!POSTESIWCONTCR)										
        '    ElseIf pBookSubType = "S" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!POSTESIRNDCONTCR), -1, RsCompany!POSTESIRNDCONTCR)										
        '    ElseIf pBookSubType = "D" Then										
        '        mAccountCode = "-1"										
        '    ElseIf pBookSubType = "T" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!PostTRNESIContCr), -1, RsCompany!PostTRNESIContCr)										
        '    End If										
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        Else
            mAccountName = ""
        End If
        mAmount = CDbl(VB6.Format(System.Math.Round(mESIAmount * 100 / 1.75, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mNarration1 = "Employer Contribution:Double of Employee deduction"
        mAccountCode = GetCategoryAcctCode(pBookSubType, "W")
        '    If pBookSubType = "G" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!WELFARE_GS), -1, RsCompany!WELFARE_GS)										
        '    ElseIf pBookSubType = "P" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!WELFARE_PS), -1, RsCompany!WELFARE_PS)										
        '    ElseIf pBookSubType = "E" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!WELFARE_ES), -1, RsCompany!WELFARE_ES)										
        '    ElseIf pBookSubType = "R" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!WELFARE_WS), -1, RsCompany!WELFARE_WS)										
        '    ElseIf pBookSubType = "S" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!WELFARE_RS), -1, RsCompany!WELFARE_RS)										
        '    ElseIf pBookSubType = "D" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!WELFARE_D), -1, RsCompany!WELFARE_D)										
        '    ElseIf pBookSubType = "T" Then										
        '        mAccountCode = IIf(IsNull(RsCompany!Welfare_TRN), -1, RsCompany!Welfare_TRN)										
        '    End If										
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        Else
            mAccountName = ""
        End If
        mAmount = CDbl(VB6.Format(mWFAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        SqlStr = " Select SUM(PAYABLEAMOUNT) AS PAYABLEAMOUNT, SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_FFSETTLE_DET ID, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE ID.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND ID.SALHEADCODE=SMST.CODE" & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND SMST.ADDDEDUCT=" & ConDeduct & ""
        SqlStr = SqlStr & vbCrLf & " AND PAYABLEAMOUNT<>0"
        SqlStr = SqlStr & vbCrLf & " GROUP BY SALHEADCODE,TYPE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            Do While RsTMSal.EOF = False
                mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("PayableAmount").Value), 0, RsTMSal.Fields("PayableAmount").Value), "0.00"))
                If mAmount <> 0 Then
                    mSalaryHeadCode = IIf(IsDBNull(RsTMSal.Fields("SALHEADCODE").Value), "-1", RsTMSal.Fields("SALHEADCODE").Value)
                    mType = IIf(IsDBNull(RsTMSal.Fields("Type").Value), "-1", RsTMSal.Fields("Type").Value)
                    If mType = ConAdvance Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='L'") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    ElseIf mType = ConImprest Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='I'") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    ElseIf mType = ConLoan Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "ADV_ACCOUNT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    Else
                        If MainClass.ValidateWithMasterTable(mSalaryHeadCode, "CODE", "ACCOUNTCODEPOST", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountCode = Trim(MasterNo)
                        Else
                            mAccountCode = "-1"
                        End If
                    End If
                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountName = Trim(MasterNo)
                    Else
                        mAccountName = ""
                    End If
                    '            mNarration = " for the Month " & vb6.Format(pSalDate, "MMMM-YYYY")										
                    Call InsertIntoGrid(cntRow, "CR", mAccountName, mNarration, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                    If mType = ConPF Then
                        mNarration1 = "Admin Charges on PF : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value), "0.00") & " of Basic Salary"
                        mNarration2 = "Contribution to EDLI : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value) + IIf(IsDBNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value), "0.00") & " of Basic Salary"
                        mNarration3 = "Employer Contribution : Equal to Employee Contribution"
                        If MainClass.ValidateWithMasterTable(mPFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mPFAccountName = Trim(MasterNo)
                        End If
                        mAmount = CDbl(VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value), "0.00")) / 100, "0.00"))
                        Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                        mAmount = CDbl(VB6.Format(System.Math.Round(mPensionAmount, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value + IIf(IsDBNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value)), "0.00")) / 100, "0.00"))
                        Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration2, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                        mAmount = mPFAmount
                        Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration3, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                    ElseIf mType = ConESI Then
                        mNarration1 = "Employer Contribution : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00") & " of ESI Deduction"
                        If MainClass.ValidateWithMasterTable(mESIAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountName = Trim(MasterNo)
                        Else
                            mAccountName = ""
                        End If
                        mAmount = CDbl(VB6.Format(System.Math.Round(mESIAmount * 100 / 1.75, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00"))
                        Call InsertIntoGrid(cntRow, "CR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                    ElseIf mType = ConWelfare Then
                        mNarration1 = "Employer Contribution:Double of Employee deduction"
                        If MainClass.ValidateWithMasterTable(mWFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountName = Trim(MasterNo)
                        Else
                            mAccountName = ""
                        End If
                        mAmount = CDbl(VB6.Format(mWFAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value), "0.00")) / 100, "0.00"))
                        Call InsertIntoGrid(cntRow, "CR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                    End If
                    mAccountName = ""
                    mAccountCode = ""
                End If
                RsTMSal.MoveNext()
            Loop
        End If
        '    mNarration1 = "Admin Charges on PF : " & vb6.Format(IIf(IsNull(RsCompany!PFADMINPER), 0, RsCompany!PFADMINPER), "0.00") & " of Basic Salary"										
        '    mNarration2 = "Contribution to EDLI : " & vb6.Format(IIf(IsNull(RsCompany!PFEDLIPER), 0, RsCompany!PFEDLIPER), "0.00") & " of Basic Salary"										
        '    mNarration3 = "Employer Contribution : Equal to Employee Contribution"										
        '										
        '    If MainClass.ValidateWithMasterTable(mPFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then										
        '        mPFAccountName = Trim(MasterNo)										
        '    End If										
        '										
        '    mAmount = Format(Round(mPFAmount * 100 / 12, 0) * Format(IIf(IsNull(RsCompany!PFADMINPER), 0, RsCompany!PFADMINPER), "0.00") / 100, "0.00")										
        '    Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount)										
        '										
        '    mAmount = Format(Round(mPFAmount * 100 / 12, 0) * Format(IIf(IsNull(RsCompany!PFEDLIPER), 0, RsCompany!PFEDLIPER), "0.00") / 100, "0.00")										
        '    Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration2, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount)										
        '										
        '    mAmount = mPFAmount										
        '    Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration3, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount)										
        '    mNarration1 = "Employer Contribution : " & vb6.Format(IIf(IsNull(RsCompany!EMPLOYERESIPER), 0, RsCompany!EMPLOYERESIPER), "0.00") & " of ESI Deduction"										
        '										
        '    If MainClass.ValidateWithMasterTable(mESIAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then										
        '        mAccountName = Trim(MasterNo)										
        '    Else										
        '        mAccountName = ""										
        '    End If										
        '										
        '    mAmount = Format(Round(mESIAmount * 100 / 1.75, 0) * Format(IIf(IsNull(RsCompany!EMPLOYERESIPER), 0, RsCompany!EMPLOYERESIPER), "0.00") / 100, "0.00")										
        '    Call InsertIntoGrid(cntRow, "CR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount)										
        '    mNarration1 = "Employer Contribution:Double of Employee deduction"										
        '										
        '    If MainClass.ValidateWithMasterTable(mWFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then										
        '        mAccountName = Trim(MasterNo)										
        '    Else										
        '        mAccountName = ""										
        '    End If										
        '										
        '    mAmount = Format(mWFAmount * Format(IIf(IsNull(RsCompany!WELFAREPER), 0, RsCompany!WELFAREPER), "0.00") / 100, "0.00")										
        '    Call InsertIntoGrid(cntRow, "CR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount)										
        If mSalaryPayableAmount <> 0 Then
            mAccountCode = GetCategoryAcctCode(pBookSubType, "SC")
            '        If pBookSubType = "G" Then										
            '            mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFCACCOUNTCODE), -1, RsCompany!POSTSTAFFCACCOUNTCODE)										
            '        ElseIf pBookSubType = "P" Then										
            '            mAccountCode = IIf(IsNull(RsCompany!POSTPRODCACCOUNTCODE), -1, RsCompany!POSTPRODCACCOUNTCODE)										
            '        ElseIf pBookSubType = "E" Then										
            '            mAccountCode = IIf(IsNull(RsCompany!POSTEXPCACCOUNTCODE), -1, RsCompany!POSTEXPCACCOUNTCODE)										
            '        ElseIf pBookSubType = "R" Then										
            '            mAccountCode = IIf(IsNull(RsCompany!POSTWORKERCACCOUNTCODE), -1, RsCompany!POSTWORKERCACCOUNTCODE)										
            '        ElseIf pBookSubType = "S" Then										
            '            mAccountCode = IIf(IsNull(RsCompany!POSTRNDCACCOUNTCODE), -1, RsCompany!POSTRNDCACCOUNTCODE)										
            '        ElseIf pBookSubType = "D" Then										
            '            mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTOR_CREDITCODE), -1, RsCompany!POSTDIRECTOR_CREDITCODE)										
            '        ElseIf pBookSubType = "T" Then										
            '            mAccountCode = IIf(IsNull(RsCompany!PostTRNCAccountCode), -1, RsCompany!PostTRNCAccountCode)										
            '        End If										
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = Trim(MasterNo)
            End If
            Call InsertIntoGrid(cntRow, "CR", mAccountName, pNarration, pEmpCode, pDeptCode, mCCCode, mExpCode, mSalaryPayableAmount, mDivisionCode)
            mAccountName = ""
            mAccountCode = ""
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
    End Sub
    Private Function GetPensionWages(ByRef pYM As Integer, ByRef mArrear As String, ByRef mDivisionCode As Double) As Double
        On Error GoTo UpdateAccountPostingHeadErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetPensionWages = 0
        SqlStr = ""
        SqlStr = " SELECT  SUM(PENSIONWAGES) As AMOUNT " & vbCrLf & " FROM PAY_PFESI_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND ISARREAR='" & mArrear & "'" & vbCrLf & " AND EMP_CODE = '" & lblEmpCode.Text & "'"
        '    SqlStr = SqlStr & vbCrLf _										
        ''        & " SELECT DISTINCT EMP_CODE " & vbCrLf _										
        ''        & " FROM PAY_SAL_TRN " & vbCrLf _										
        ''        & " WHERE " & vbCrLf _										
        ''        & " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _										
        ''        & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf _										
        ''        & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf _										
        ''        & " AND CATEGORY='" & mCategory & "'" & vbCrLf _										
        ''        & " AND ISARREAR='" & mArrear & "'" & vbCrLf _										
        ''        & " AND DIV_CODE=" & mDivisionCode & ")"										
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetPensionWages = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If
        Exit Function
UpdateAccountPostingHeadErr:
        GetPensionWages = 0
        MsgInformation(Err.Description)
        '    Resume										
    End Function
    Private Sub FillGridFromTMFullFinalOld(ByRef pSalDate As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pEmpCode As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTMSal As ADODB.Recordset = Nothing
        Dim mAmount As Double
        Dim mAccountCode As String = ""
        Dim mSalaryHeadCode As String
        Dim mNarration As String = ""
        Dim cntRow As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAccountName As String = ""
        Dim mType As Integer
        Dim mVPFAmount As Double
        Dim mPFAmount As Double
        Dim mESIAmount As Double
        Dim mEmpName As String
        Dim pNarration As String
        Dim pDeptCode As String
        Dim mPFAccountCode As String = ""
        Dim mNarration1 As String
        Dim mNarration2 As String
        Dim mNarration3 As String
        Dim mPFAccountName As String = ""
        Dim mESIAccountCode As String = ""
        Dim mWFAccountCode As String = ""
        Dim mWFAmount As Double
        Dim mCCCode As String
        Dim mExpCode As String
        Dim pDivisionCode As Double
        mExpCode = "001"
        cntRow = 1
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpName = MasterNo
        Else
            mEmpName = "-1"
        End If
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pDeptCode = MasterNo
        Else
            pDeptCode = ""
        End If
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "DIV_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pDivisionCode = Val(MasterNo)
        Else
            pDivisionCode = -1
        End If
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "COST_CENTER_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCCCode = MasterNo
        Else
            mCCCode = "001"
        End If
        pNarration = " FULL & FINAL SETTLEMENT OF " & mEmpName & " (" & pEmpCode & ")"
        txtNarration.Text = pNarration
        mPFAmount = 0
        mESIAmount = 0
        SqlStr = " Select * " & vbCrLf & " FROM PAY_FFSETTLE_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.EMP_CODE='" & VB6.Format(pEmpCode, "000000") & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            'GROSS SALARY & Arrear ....										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("GROSS_SALARY").Value), 0, RsTemp.Fields("GROSS_SALARY").Value), "0.00"))
            mAmount = System.Math.Round(mAmount, 0)
            If mAmount <> 0 Then
                If pBookSubType = "G" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTSTAFFDACCOUNTCODE").Value), -1, RsCompany.Fields("POSTSTAFFDACCOUNTCODE").Value)
                ElseIf pBookSubType = "P" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPRODDACCOUNTCODE").Value), -1, RsCompany.Fields("POSTPRODDACCOUNTCODE").Value)
                ElseIf pBookSubType = "E" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTEXPDACCOUNTCODE").Value), -1, RsCompany.Fields("POSTEXPDACCOUNTCODE").Value)
                ElseIf pBookSubType = "R" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTWORKERDACCOUNTCODE").Value), -1, RsCompany.Fields("POSTWORKERDACCOUNTCODE").Value)
                ElseIf pBookSubType = "S" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTRNDDACCOUNTCODE").Value), -1, RsCompany.Fields("POSTRNDDACCOUNTCODE").Value)
                ElseIf pBookSubType = "D" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTDIRECTOR_DEBITCODE").Value), -1, RsCompany.Fields("POSTDIRECTOR_DEBITCODE").Value)
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "F&F : GROSS SALARY"
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    '            mAccountName = ""										
                    '            mAccountCode = ""										
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''Salary Arrear Amount										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ARREAR_SAL").Value), 0, RsTemp.Fields("ARREAR_SAL").Value), "0.00"))
            If mAmount <> 0 Then
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "F&F : ARREAR"
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    '            mAccountName = ""										
                    '            mAccountCode = ""										
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''Other Amount..										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("OTHERS_AMOUNT").Value), 0, RsTemp.Fields("OTHERS_AMOUNT").Value), "0.00"))
            If mAmount <> 0 Then
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "F&F : OTHER DEDUCTION"
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    '            mAccountName = ""										
                    '            mAccountCode = ""										
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''Notice Pay / Ex-Gratia..										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("NOTICE_AMOUNT").Value), 0, RsTemp.Fields("NOTICE_AMOUNT").Value), "0.00"))
            If mAmount <> 0 Then
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = IIf(mAmount < 0, "CR", "DR")
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = IIf(mAmount < 0, "F&F : NOTICE PAY", "F&F : EX-GRATIA")
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(System.Math.Abs(mAmount), "0.00")
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            mAccountName = ""
            mAccountCode = ""
            'Incentive..										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("INC_AMT_FORMON").Value), 0, RsTemp.Fields("INC_AMT_FORMON").Value), "0.00"))
            mAmount = mAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("INC_AMT_PREMON").Value), 0, RsTemp.Fields("INC_AMT_PREMON").Value), "0.00"))
            mAmount = mAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ARREAR_INC").Value), 0, RsTemp.Fields("ARREAR_INC").Value), "0.00"))
            If mAmount <> 0 Then
                If pBookSubType = "G" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTSTAFFINC_DEBITCODE").Value), -1, RsCompany.Fields("POSTSTAFFINC_DEBITCODE").Value)
                ElseIf pBookSubType = "P" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPRODINC_DEBITCODE").Value), -1, RsCompany.Fields("POSTPRODINC_DEBITCODE").Value)
                ElseIf pBookSubType = "E" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTEXPORTINC_DEBITCODE").Value), -1, RsCompany.Fields("POSTEXPORTINC_DEBITCODE").Value)
                ElseIf pBookSubType = "R" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTWINC_DEBITCODE").Value), -1, RsCompany.Fields("POSTWINC_DEBITCODE").Value)
                ElseIf pBookSubType = "S" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTRNDINC_DEBITCODE").Value), -1, RsCompany.Fields("POSTRNDINC_DEBITCODE").Value)
                ElseIf pBookSubType = "D" Then
                    mAccountCode = "-1" ''IIf(IsNull(RsCompany!POSTDIRECTOR_DEBITCODE), -1, RsCompany!POSTDIRECTOR_DEBITCODE)										
                ElseIf pBookSubType = "T" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTTRNINC_DEBITCODE").Value), -1, RsCompany.Fields("POSTTRNINC_DEBITCODE").Value)
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "F&F : INCENTIVE AMOUNT"
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    mAccountName = ""
                    mAccountCode = ""
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''Leave Encash										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("EL_AMOUNT").Value), 0, RsTemp.Fields("EL_AMOUNT").Value), "0.00"))
            If mAmount <> 0 Then
                If pBookSubType = "G" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_EL_S_DCODE").Value), -1, RsCompany.Fields("POST_EL_S_DCODE").Value)
                ElseIf pBookSubType = "P" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_EL_P_DCODE").Value), -1, RsCompany.Fields("POST_EL_P_DCODE").Value)
                ElseIf pBookSubType = "E" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_EL_E_DCODE").Value), -1, RsCompany.Fields("POST_EL_E_DCODE").Value)
                ElseIf pBookSubType = "R" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_EL_W_DCODE").Value), -1, RsCompany.Fields("POST_EL_W_DCODE").Value)
                ElseIf pBookSubType = "S" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_EL_R_DCODE").Value), -1, RsCompany.Fields("POST_EL_R_DCODE").Value)
                ElseIf pBookSubType = "D" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_EL_D_DCODE").Value), -1, RsCompany.Fields("POST_EL_D_DCODE").Value)
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "LEAVE ENCASHMENT @DAYS " & VB6.Format(IIf(IsDBNull(RsTemp.Fields("EL_DAYS").Value), 0, RsTemp.Fields("EL_DAYS").Value), "0.0")
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    mAccountName = ""
                    mAccountCode = ""
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''L.T.A.										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("LTC_AMOUNT").Value), 0, RsTemp.Fields("LTC_AMOUNT").Value), "0.00"))
            If mAmount <> 0 Then
                If pBookSubType = "G" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTSTAFFLTC_DEBITCODE").Value), -1, RsCompany.Fields("POSTSTAFFLTC_DEBITCODE").Value)
                ElseIf pBookSubType = "P" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPRODLTC_DEBITCODE").Value), -1, RsCompany.Fields("POSTPRODLTC_DEBITCODE").Value)
                ElseIf pBookSubType = "E" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTEXPORTLTC_DEBITCODE").Value), -1, RsCompany.Fields("POSTEXPORTLTC_DEBITCODE").Value)
                ElseIf pBookSubType = "R" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTWLTC_DEBITCODE").Value), -1, RsCompany.Fields("POSTWLTC_DEBITCODE").Value)
                ElseIf pBookSubType = "S" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTRNDLTC_DEBITCODE").Value), -1, RsCompany.Fields("POSTRNDLTC_DEBITCODE").Value)
                ElseIf pBookSubType = "D" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTDIRECTORLTC_DEBITCODE").Value), -1, RsCompany.Fields("POSTDIRECTORLTC_DEBITCODE").Value)
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "LTA @MONTH " & VB6.Format(IIf(IsDBNull(RsTemp.Fields("LTC_MONTH").Value), 0, RsTemp.Fields("LTC_MONTH").Value), "0.0")
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    mAccountName = ""
                    mAccountCode = ""
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''BONUS PREVIOUS YEAR										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("BONUS_FORYEAR").Value), 0, RsTemp.Fields("BONUS_FORYEAR").Value), "0.00"))
            If mAmount <> 0 Then
                If pBookSubType = "G" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTSTAFFBONUS_CREDITCODE").Value), -1, RsCompany.Fields("POSTSTAFFBONUS_CREDITCODE").Value)
                ElseIf pBookSubType = "P" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPRODBONUS_CREDITCODE").Value), -1, RsCompany.Fields("POSTPRODBONUS_CREDITCODE").Value)
                ElseIf pBookSubType = "E" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTEXPORTBONUS_CREDITCODE").Value), -1, RsCompany.Fields("POSTEXPORTBONUS_CREDITCODE").Value)
                ElseIf pBookSubType = "R" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTWBONUS_CREDITCODE").Value), -1, RsCompany.Fields("POSTWBONUS_CREDITCODE").Value)
                ElseIf pBookSubType = "S" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTRNDBONUS_CREDITCODE").Value), -1, RsCompany.Fields("POSTRNDBONUS_CREDITCODE").Value)
                ElseIf pBookSubType = "D" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTDIRECTORBONUS_CREDITCODE").Value), -1, RsCompany.Fields("POSTDIRECTORBONUS_CREDITCODE").Value)
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "PREVIOUS YEAR BONUS @" & VB6.Format(IIf(IsDBNull(RsTemp.Fields("BONUS_PER_FORYEAR").Value), 0, RsTemp.Fields("BONUS_PER_FORYEAR").Value), "0.0") & "%"
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    mAccountName = ""
                    mAccountCode = ""
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''BONUS CURRENT YEAR										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("BONUS_CURRYEAR").Value), 0, RsTemp.Fields("BONUS_CURRYEAR").Value), "0.00"))
            If mAmount <> 0 Then
                If pBookSubType = "G" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTSTAFFBONUS_DEBITCODE").Value), -1, RsCompany.Fields("POSTSTAFFBONUS_DEBITCODE").Value)
                ElseIf pBookSubType = "P" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPRODBONUS_DEBITCODE").Value), -1, RsCompany.Fields("POSTPRODBONUS_DEBITCODE").Value)
                ElseIf pBookSubType = "E" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTEXPORTBONUS_DEBITCODE").Value), -1, RsCompany.Fields("POSTEXPORTBONUS_DEBITCODE").Value)
                ElseIf pBookSubType = "R" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTWBONUS_DEBITCODE").Value), -1, RsCompany.Fields("POSTWBONUS_DEBITCODE").Value)
                ElseIf pBookSubType = "S" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTRNDBONUS_DEBITCODE").Value), -1, RsCompany.Fields("POSTRNDBONUS_DEBITCODE").Value)
                ElseIf pBookSubType = "D" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTDIRECTORBONUS_DEBITCODE").Value), -1, RsCompany.Fields("POSTDIRECTORBONUS_DEBITCODE").Value)
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "CURRENT YEAR BONUS @" & VB6.Format(IIf(IsDBNull(RsTemp.Fields("BONUS_PER_CURRYEAR").Value), 0, RsTemp.Fields("BONUS_PER_CURRYEAR").Value), "0.0") & "%"
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    mAccountName = ""
                    mAccountCode = ""
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''Gratuity..										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("GRATUITY_AMOUNT").Value), 0, RsTemp.Fields("GRATUITY_AMOUNT").Value), "0.00"))
            If mAmount <> 0 Then
                If pBookSubType = "G" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_GRATUITY_S_DCODE").Value), -1, RsCompany.Fields("POST_GRATUITY_S_DCODE").Value)
                ElseIf pBookSubType = "P" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_GRATUITY_P_DCODE").Value), -1, RsCompany.Fields("POST_GRATUITY_P_DCODE").Value)
                ElseIf pBookSubType = "E" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_GRATUITY_E_DCODE").Value), -1, RsCompany.Fields("POST_GRATUITY_E_DCODE").Value)
                ElseIf pBookSubType = "R" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_GRATUITY_W_DCODE").Value), -1, RsCompany.Fields("POST_GRATUITY_W_DCODE").Value)
                ElseIf pBookSubType = "S" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_GRATUITY_R_DCODE").Value), -1, RsCompany.Fields("POST_GRATUITY_R_DCODE").Value)
                ElseIf pBookSubType = "D" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POST_GRATUITY_D_DCODE").Value), -1, RsCompany.Fields("POST_GRATUITY_D_DCODE").Value)
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "DR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = "GRATUITY @MONTHS " & VB6.Format(IIf(IsDBNull(RsTemp.Fields("GRATUITY_MONTH").Value), 0, RsTemp.Fields("GRATUITY_MONTH").Value), "0.0") & "%"
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(System.Math.Abs(mAmount), "0.00")
                    mAccountName = ""
                    mAccountCode = ""
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
            ''Salary Payable										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("NET_SALARY").Value), 0, RsTemp.Fields("NET_SALARY").Value), "0.00"))
            If mAmount <> 0 Then
                If pBookSubType = "G" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTSTAFFCACCOUNTCODE").Value), -1, RsCompany.Fields("POSTSTAFFCACCOUNTCODE").Value)
                ElseIf pBookSubType = "P" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPRODCACCOUNTCODE").Value), -1, RsCompany.Fields("POSTPRODCACCOUNTCODE").Value)
                ElseIf pBookSubType = "E" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTEXPCACCOUNTCODE").Value), -1, RsCompany.Fields("POSTEXPCACCOUNTCODE").Value)
                ElseIf pBookSubType = "R" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTWORKERCACCOUNTCODE").Value), -1, RsCompany.Fields("POSTWORKERCACCOUNTCODE").Value)
                ElseIf pBookSubType = "S" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTRNDCACCOUNTCODE").Value), -1, RsCompany.Fields("POSTRNDCACCOUNTCODE").Value)
                ElseIf pBookSubType = "D" Then
                    mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTDIRECTOR_CREDITCODE").Value), -1, RsCompany.Fields("POSTDIRECTOR_CREDITCODE").Value)
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                With SprdMain
                    .Row = cntRow
                    .Col = 1
                    .Col = ColPRRowNo
                    .Text = CStr(cntRow)
                    .Col = ColDC
                    .Text = "CR"
                    .Col = ColAccountName
                    .Text = mAccountName
                    .Col = ColParticulars
                    .Text = pNarration
                    .Col = ColEmp
                    .Text = pEmpCode
                    .Col = ColDept
                    .Text = pDeptCode
                    .Col = ColDivisionCode
                    .Text = CStr(pDivisionCode)
                    .Col = ColCC
                    .Text = mCCCode
                    .Col = ColExp
                    .Text = mExpCode
                    .Col = ColAmount
                    .Text = VB6.Format(mAmount, "0.00")
                    mAccountName = ""
                    mAccountCode = ""
                    cntRow = cntRow + 1
                    .MaxRows = cntRow
                End With
            End If
        End If
        SqlStr = " Select SUM(PAYABLEAMOUNT) AS PAYABLEAMOUNT, SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_FFSETTLE_DET ID, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE ID.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND ID.SALHEADCODE=SMST.CODE" & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND SMST.ADDDEDUCT=" & ConDeduct & ""
        SqlStr = SqlStr & vbCrLf & " AND PAYABLEAMOUNT<>0"
        SqlStr = SqlStr & vbCrLf & " GROUP BY SALHEADCODE,TYPE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            Do While RsTMSal.EOF = False
                mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("PayableAmount").Value), 0, RsTMSal.Fields("PayableAmount").Value), "0.00"))
                If mAmount <> 0 Then
                    mSalaryHeadCode = IIf(IsDBNull(RsTMSal.Fields("SALHEADCODE").Value), "-1", RsTMSal.Fields("SALHEADCODE").Value)
                    mType = IIf(IsDBNull(RsTMSal.Fields("Type").Value), "-1", RsTMSal.Fields("Type").Value)
                    If mType = ConPF Then
                        mPFAmount = mAmount
                    ElseIf mType = ConESI Then
                        mESIAmount = mAmount
                    ElseIf mType = ConVPFAllw Then
                        mVPFAmount = mAmount
                    ElseIf mType = ConWelfare Then
                        mWFAmount = mAmount
                    End If
                    If mType = ConAdvance Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='L'") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    ElseIf mType = ConImprest Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='I'") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    ElseIf mType = ConLoan Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "ADV_ACCOUNT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    Else
                        If MainClass.ValidateWithMasterTable(mSalaryHeadCode, "CODE", "ACCOUNTCODEPOST", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountCode = Trim(MasterNo)
                        Else
                            mAccountCode = "-1"
                        End If
                        If mType = ConPF Then
                            mPFAccountCode = mAccountCode
                        ElseIf mType = ConESI Then
                            mESIAccountCode = mAccountCode
                        ElseIf mType = ConWelfare Then
                            mWFAccountCode = mAccountCode
                        End If
                    End If
                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountName = Trim(MasterNo)
                    Else
                        mAccountName = ""
                    End If
                    '            mNarration = " for the Month " & vb6.Format(pSalDate, "MMMM-YYYY")										
                    With SprdMain
                        .MaxRows = cntRow
                        .Row = cntRow
                        .Col = 1
                        .Col = ColPRRowNo
                        .Text = CStr(cntRow)
                        .Col = ColDC
                        .Text = "CR"
                        .Col = ColAccountName
                        .Text = mAccountName
                        .Col = ColParticulars
                        .Text = mNarration
                        .Col = ColEmp
                        .Text = pEmpCode
                        .Col = ColDept
                        .Text = pDeptCode
                        .Col = ColDivisionCode
                        .Text = CStr(pDivisionCode)
                        .Col = ColCC
                        .Text = mCCCode
                        .Col = ColExp
                        .Text = mExpCode
                        .Col = ColAmount
                        .Text = VB6.Format(mAmount, "0.00")
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End With
                End If
                RsTMSal.MoveNext()
            Loop
        End If
        If pBookSubType = "G" Then
            mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPFCONTCR").Value), -1, RsCompany.Fields("POSTPFCONTCR").Value)
        ElseIf pBookSubType = "P" Then
            mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPFPRODCONTCR").Value), -1, RsCompany.Fields("POSTPFPRODCONTCR").Value)
        ElseIf pBookSubType = "E" Then
            mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPFEXPORTCONTCR").Value), -1, RsCompany.Fields("POSTPFEXPORTCONTCR").Value)
        ElseIf pBookSubType = "R" Then
            mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPFWCONTCR").Value), -1, RsCompany.Fields("POSTPFWCONTCR").Value)
        ElseIf pBookSubType = "S" Then
            mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPFRNDCONTCR").Value), -1, RsCompany.Fields("POSTPFRNDCONTCR").Value)
        ElseIf pBookSubType = "D" Then
            mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTPFDIRECTORACCOUNTCODE").Value), -1, RsCompany.Fields("POSTPFDIRECTORACCOUNTCODE").Value)
        End If
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        End If
        mNarration1 = "Admin Charges on PF : " & VB6.Format(RsCompany.Fields("PFADMINPER").Value + RsCompany.Fields("PFADMINPER_22").Value, "0.00") & " of Basic Salary"
        mNarration2 = "Contribution to EDLI : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00") & " of Basic Salary"
        mNarration3 = "Employer Contribution : Equal to Employee Contribution"
        With SprdMain
            If IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value) <> 0 Then
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "Dr"
                .Col = ColAccountName
                .Text = mAccountName
                .Col = ColParticulars
                .Text = mNarration1
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value) + IIf(IsDBNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value), "0.00")) / 100, "0.00")
                cntRow = cntRow + 1
            End If
            .MaxRows = cntRow
            If IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value) <> 0 Then
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "Dr"
                .Col = ColAccountName
                .Text = mAccountName
                .Col = ColParticulars
                .Text = mNarration2
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00")) / 100, "0.00")
                cntRow = cntRow + 1
            End If
            .MaxRows = cntRow
            If mPFAmount <> 0 Then
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "Dr"
                .Col = ColAccountName
                .Text = mAccountName
                .Col = ColParticulars
                .Text = mNarration3
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(mPFAmount, "0.00")
                cntRow = cntRow + 1
            End If
            If MainClass.ValidateWithMasterTable(mPFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPFAccountName = Trim(MasterNo)
            End If
            .MaxRows = cntRow
            If IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value) <> 0 Then
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "Cr"
                .Col = ColAccountName
                .Text = mPFAccountName
                .Col = ColParticulars
                .Text = mNarration1
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(RsCompany.Fields("PFADMINPER").Value + RsCompany.Fields("PFADMINPER_22").Value, "0.00")) / 100, "0.00")
                cntRow = cntRow + 1
            End If
            .MaxRows = cntRow
            If IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value) <> 0 Then
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "Cr"
                .Col = ColAccountName
                .Text = mPFAccountName
                .Col = ColParticulars
                .Text = mNarration2
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00")) / 100, "0.00")
                cntRow = cntRow + 1
            End If
            .MaxRows = cntRow
            If mPFAmount <> 0 Then
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "Cr"
                .Col = ColAccountName
                .Text = mPFAccountName
                .Col = ColParticulars
                .Text = mNarration3
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColCC
                .Text = mCCCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(mPFAmount, "0.00")
                cntRow = cntRow + 1
            End If
            mNarration1 = "Employer Contribution : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00") & " of ESI Deduction"
            If pBookSubType = "G" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTESICONTCR").Value), -1, RsCompany.Fields("POSTESICONTCR").Value)
            ElseIf pBookSubType = "P" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTESIPRODCONTCR").Value), -1, RsCompany.Fields("POSTESIPRODCONTCR").Value)
            ElseIf pBookSubType = "E" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTESIEXPORTCONTCR").Value), -1, RsCompany.Fields("POSTESIEXPORTCONTCR").Value)
            ElseIf pBookSubType = "R" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTESIWCONTCR").Value), -1, RsCompany.Fields("POSTESIWCONTCR").Value)
            ElseIf pBookSubType = "S" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("POSTESIRNDCONTCR").Value), -1, RsCompany.Fields("POSTESIRNDCONTCR").Value)
            ElseIf pBookSubType = "D" Then
                mAccountCode = "-1"
            End If
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = Trim(MasterNo)
            End If
            .MaxRows = cntRow
            If mESIAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) <> 0 Then
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "DR"
                .Col = ColAccountName
                .Text = mAccountName
                .Col = ColParticulars
                .Text = mNarration1
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(System.Math.Round(mESIAmount * 100 / 1.75, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00")
                cntRow = cntRow + 1
                .MaxRows = cntRow
                If MainClass.ValidateWithMasterTable(mESIAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "CR"
                .Col = ColAccountName
                .Text = mAccountName
                .Col = ColParticulars
                .Text = mNarration1
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(System.Math.Round(mESIAmount * 100 / 1.75, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00")
                cntRow = cntRow + 1
                .MaxRows = cntRow
            End If
            mNarration1 = "Employer Contribution:Double of Employee deduction"
            If pBookSubType = "G" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("WELFARE_GS").Value), -1, RsCompany.Fields("WELFARE_GS").Value)
            ElseIf pBookSubType = "P" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("WELFARE_PS").Value), -1, RsCompany.Fields("WELFARE_PS").Value)
            ElseIf pBookSubType = "E" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("WELFARE_ES").Value), -1, RsCompany.Fields("WELFARE_ES").Value)
            ElseIf pBookSubType = "R" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("WELFARE_WS").Value), -1, RsCompany.Fields("WELFARE_WS").Value)
            ElseIf pBookSubType = "S" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("WELFARE_RS").Value), -1, RsCompany.Fields("WELFARE_RS").Value)
            ElseIf pBookSubType = "D" Then
                mAccountCode = IIf(IsDBNull(RsCompany.Fields("WELFARE_D").Value), -1, RsCompany.Fields("WELFARE_D").Value)
            End If
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = Trim(MasterNo)
            End If
            .MaxRows = cntRow
            If mWFAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value), "0.00")) <> 0 Then
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "DR"
                .Col = ColAccountName
                .Text = mAccountName
                .Col = ColParticulars
                .Text = mNarration1
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(mWFAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value), "0.00")) / 100, "0.00")
                cntRow = cntRow + 1
                .MaxRows = cntRow
                If MainClass.ValidateWithMasterTable(mWFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = Trim(MasterNo)
                End If
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = "CR"
                .Col = ColAccountName
                .Text = mAccountName
                .Col = ColParticulars
                .Text = mNarration1
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColCC
                .Text = mCCCode
                .Col = ColExp
                .Text = mExpCode
                .Col = ColAmount
                .Text = VB6.Format(mWFAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value), "0.00")) / 100, "0.00")
                cntRow = cntRow + 1
                .MaxRows = cntRow
            End If
        End With
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
    End Sub
    Private Sub FillGridFromVoucherSal(ByRef pYM As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mDivisionCode As Double, ByRef pEmpCode As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTMSal As ADODB.Recordset = Nothing
        Dim mAmount As Double
        Dim mAccountCode As String
        Dim mSalaryHeadCode As String
        Dim mNarration As String = ""
        Dim cntRow As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAccountName As String = ""
        Dim mType As Integer
        Dim mVPFAmount As Double
        Dim mPFAmount As Double
        Dim mESIAmount As Double
        Dim mEmpName As String
        Dim pNarration As String
        Dim pDeptCode As String
        Dim mPFAccountCode As String = ""
        Dim mNarration1 As String
        Dim mNarration2 As String
        Dim mNarration3 As String
        Dim mPFAccountName As String = ""
        Dim mESIAccountCode As String = ""
        Dim mWFAccountCode As String = ""
        Dim mWFAmount As Double
        Dim mCCCode As String
        Dim mExpCode As String
        Dim mBasicSalary As Double
        Dim mWDays As Double
        mExpCode = "001"
        cntRow = 1
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpName = MasterNo
        Else
            mEmpName = "-1"
        End If
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pDeptCode = MasterNo
        Else
            pDeptCode = ""
        End If
        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "COST_CENTER_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCCCode = MasterNo
        Else
            mCCCode = "001"
        End If
        pNarration = "Voucher Payment For the Month of " & "" & mEmpName & " (" & pEmpCode & ")"
        txtNarration.Text = pNarration
        mPFAmount = 0
        mESIAmount = 0
        SqlStr = " Select DISTINCT PAYABLESALARY, WDAYS " & vbCrLf & " FROM PAY_SALVOUCHER_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "' AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mBasicSalary = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("PAYABLESALARY").Value), 0, RsTemp.Fields("PAYABLESALARY").Value), "0.00"))
            mWDays = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("WDAYS").Value), 0, RsTemp.Fields("WDAYS").Value), "0.00"))
        End If
        SqlStr = " Select SUM(PAYABLEAMOUNT) AS PAYABLEAMOUNT, SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_SALVOUCHER_TRN ID, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE ID.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND ID.SALHEADCODE=SMST.CODE" & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "' AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND SMST.ADDDEDUCT=" & ConDeduct & ""
        SqlStr = SqlStr & vbCrLf & " AND PAYABLEAMOUNT<>0"
        SqlStr = SqlStr & vbCrLf & " GROUP BY SALHEADCODE,TYPE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            Do While RsTMSal.EOF = False
                mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("PayableAmount").Value), 0, RsTMSal.Fields("PayableAmount").Value), "0.00"))
                mType = IIf(IsDBNull(RsTMSal.Fields("Type").Value), "-1", RsTMSal.Fields("Type").Value)
                mSalaryHeadCode = IIf(IsDBNull(RsTMSal.Fields("SALHEADCODE").Value), "-1", RsTMSal.Fields("SALHEADCODE").Value)
                If MainClass.ValidateWithMasterTable(mSalaryHeadCode, "CODE", "ACCOUNTCODEPOST", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = Trim(MasterNo)
                Else
                    mAccountCode = "-1"
                End If
                If mType = ConPF Then
                    mPFAmount = mAmount
                    mPFAccountCode = mAccountCode
                ElseIf mType = ConESI Then
                    mESIAmount = mAmount
                    mESIAccountCode = mAccountCode
                ElseIf mType = ConVPFAllw Then
                    mVPFAmount = mAmount
                ElseIf mType = ConWelfare Then
                    mWFAmount = mAmount
                    mWFAccountCode = mAccountCode
                End If
                RsTMSal.MoveNext()
            Loop
        End If
        SqlStr = " Select SUM(PAYABLEAMOUNT) AS PAYABLEAMOUNT " & vbCrLf & " FROM PAY_SALVOUCHER_TRN ID, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE ID.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND ID.SALHEADCODE=SMST.CODE" & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "' AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND SMST.ADDDEDUCT=" & ConEarning & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            'GROSS SALARY & Arrear ....										
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("PayableAmount").Value), 0, RsTemp.Fields("PayableAmount").Value), "0.00"))
        End If
        mAmount = System.Math.Round(mAmount + mBasicSalary, 0)
        If mAmount <> 0 Then
            mAccountCode = GetCategoryAcctCode(pBookSubType, "SD")
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = Trim(MasterNo)
            End If
            Call InsertIntoGrid(cntRow, "DR", mAccountName, "GROSS SALARY", pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        End If
        mAccountCode = GetCategoryAcctCode(pBookSubType, "P")
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        End If
        mNarration1 = "Admin Charges on PF : " & VB6.Format(RsCompany.Fields("PFADMINPER").Value + RsCompany.Fields("PFADMINPER_22").Value, "0.00") & " of Basic Salary"
        mNarration2 = "Contribution to EDLI : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00") & " of Basic Salary"
        mNarration3 = "Employer Contribution : Equal to Employee Contribution"
        mAmount = CDbl(VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value) + IIf(IsDBNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mAmount = CDbl(VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration2, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration3, pEmpCode, pDeptCode, mCCCode, mExpCode, mPFAmount, mDivisionCode)
        mNarration1 = "Employer Contribution : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00") & " of ESI Deduction"
        mAccountCode = GetCategoryAcctCode(pBookSubType, "E")
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        End If
        mAmount = CDbl(VB6.Format(System.Math.Round(mESIAmount * 100 / 1.75, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mNarration1 = "Employer Contribution:Double of Employee deduction"
        mAccountCode = GetCategoryAcctCode(pBookSubType, "W")
        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        End If
        mAmount = CDbl(VB6.Format(mWFAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "DR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        SqlStr = " Select SUM(PAYABLEAMOUNT) AS PAYABLEAMOUNT, SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_SALVOUCHER_TRN ID, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE ID.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND ID.SALHEADCODE=SMST.CODE" & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "' AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND SMST.ADDDEDUCT=" & ConDeduct & ""
        SqlStr = SqlStr & vbCrLf & " AND PAYABLEAMOUNT<>0"
        SqlStr = SqlStr & vbCrLf & " GROUP BY SALHEADCODE,TYPE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTMSal, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTMSal.EOF = False Then
            Do While RsTMSal.EOF = False
                mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTMSal.Fields("PayableAmount").Value), 0, RsTMSal.Fields("PayableAmount").Value), "0.00"))
                If mAmount <> 0 Then
                    mSalaryHeadCode = IIf(IsDBNull(RsTMSal.Fields("SALHEADCODE").Value), "-1", RsTMSal.Fields("SALHEADCODE").Value)
                    mType = IIf(IsDBNull(RsTMSal.Fields("Type").Value), "-1", RsTMSal.Fields("Type").Value)
                    If mType = ConAdvance Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='L'") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    ElseIf mType = ConImprest Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='I'") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    ElseIf mType = ConLoan Then
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "ADV_ACCOUNT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountCode = MasterNo
                        Else
                            mAccountCode = "-1"
                        End If
                    Else
                        If MainClass.ValidateWithMasterTable(mSalaryHeadCode, "CODE", "ACCOUNTCODEPOST", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAccountCode = Trim(MasterNo)
                        Else
                            mAccountCode = "-1"
                        End If
                    End If
                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountName = Trim(MasterNo)
                    Else
                        mAccountName = ""
                    End If
                    '            mNarration = " for the Month " & vb6.Format(pSalDate, "MMMM-YYYY")										
                    Call InsertIntoGrid(cntRow, "CR", mAccountName, mNarration, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
                End If
                RsTMSal.MoveNext()
            Loop
        End If
        mNarration1 = "Admin Charges on PF : " & VB6.Format(RsCompany.Fields("PFADMINPER").Value + RsCompany.Fields("PFADMINPER_22").Value, "0.00") & " of Basic Salary"
        mNarration2 = "Contribution to EDLI : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00") & " of Basic Salary"
        mNarration3 = "Employer Contribution : Equal to Employee Contribution"
        If MainClass.ValidateWithMasterTable(mPFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPFAccountName = Trim(MasterNo)
        End If
        mAmount = CDbl(VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0, RsCompany.Fields("PFADMINPER").Value) + IIf(IsDBNull(RsCompany.Fields("PFADMINPER_22").Value), 0, RsCompany.Fields("PFADMINPER_22").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mAmount = CDbl(VB6.Format(System.Math.Round(mPFAmount * 100 / 12, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration2, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mAmount = CDbl(VB6.Format(mPFAmount, "0.00"))
        Call InsertIntoGrid(cntRow, "CR", mPFAccountName, mNarration3, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mNarration1 = "Employer Contribution : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00") & " of ESI Deduction"
        If MainClass.ValidateWithMasterTable(mESIAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        End If
        mAmount = CDbl(VB6.Format(System.Math.Round(mESIAmount * 100 / 1.75, 0) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "CR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        mNarration1 = "Employer Contribution:Double of Employee deduction"
        If MainClass.ValidateWithMasterTable(mWFAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountName = Trim(MasterNo)
        End If
        mAmount = CDbl(VB6.Format(mWFAmount * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value), "0.00")) / 100, "0.00"))
        Call InsertIntoGrid(cntRow, "CR", mAccountName, mNarration1, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
        ''Salary Payable										
        SqlStr = " Select SUM(PAYABLEAMOUNT * DECODE(ADDDEDUCT," & ConDeduct & ",-1,1)) AS PAYABLEAMOUNT " & vbCrLf & " FROM PAY_SALVOUCHER_TRN ID, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE ID.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND ID.SALHEADCODE=SMST.CODE" & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "' AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        mAmount = 0
        If RsTemp.EOF = False Then
            mAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("PayableAmount").Value), 0, RsTemp.Fields("PayableAmount").Value), "0.00"))
        End If
        mAmount = System.Math.Round(mAmount + mBasicSalary, 0)
        If mAmount <> 0 Then
            mAccountCode = GetCategoryAcctCode(pBookSubType, "SC")
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = Trim(MasterNo)
            End If
            Call InsertIntoGrid(cntRow, "CR", mAccountName, pNarration, pEmpCode, pDeptCode, mCCCode, mExpCode, mAmount, mDivisionCode)
            mAccountName = ""
            mAccountCode = ""
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
        If RsTMSal.State = ADODB.ObjectStateEnum.adStateOpen Then
            RsTMSal.Close()
            RsTMSal = Nothing
        End If
    End Sub
    Private Sub InsertIntoGrid(ByRef cntRow As Integer, ByRef pDC As String, ByRef pAccountName As String, ByRef pParticulars As String, ByRef pEmpCode As String, ByRef pDeptCode As String, ByRef pCCCode As String, ByRef pExpCode As String, ByRef pAmount As Double, ByRef pDivisionCode As Double)
        On Error GoTo ErrPart
        Dim pRevDC As String
        pRevDC = IIf(UCase(pDC) = "DR", "CR", "DR")
        If pAmount <> 0 Then
            With SprdMain
                .Row = cntRow
                .Col = 1
                .Col = ColPRRowNo
                .Text = CStr(cntRow)
                .Col = ColDC
                .Text = IIf(pAmount < 0, pRevDC, pDC)
                .Col = ColAccountName
                .Text = pAccountName
                .Col = ColParticulars
                .Text = pParticulars
                .Col = ColEmp
                .Text = pEmpCode
                .Col = ColDivisionCode
                .Text = CStr(pDivisionCode)
                .Col = ColDept
                .Text = pDeptCode
                .Col = ColCC
                .Text = pCCCode
                .Col = ColExp
                .Text = pExpCode
                .Col = ColAmount
                .Text = VB6.Format(System.Math.Abs(pAmount), "0.00")
                cntRow = cntRow + 1
                .MaxRows = cntRow
            End With
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim PKey As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing '' ADODB.Recordset										
        Dim mOPBal As Double
        Dim mPartyCode As String
        Dim mServiceCode As Double
        Dim mValue As String
        Dim pSectionCode As Long

        Clear1()
        If RsTRNMain.EOF = True Then Exit Sub
        If lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then
            TxtVDate.Enabled = True
            chkChqDeposit.Enabled = True
        Else
            TxtVDate.Enabled = True
        End If
        CurMKey = RsTRNMain.Fields("mKey").Value
        mRowNo = RsTRNMain.Fields("RowNo").Value
        txtVNo1.Text = IIf(IsDBNull(RsTRNMain.Fields("VNoPrefix").Value), "", RsTRNMain.Fields("VNoPrefix").Value)
        txtVType.Text = IIf(IsDBNull(RsTRNMain.Fields("VTYPE").Value), "", RsTRNMain.Fields("VTYPE").Value)
        txtVNoSuffix.Text = IIf(IsDBNull(RsTRNMain.Fields("VNOSUFFIX").Value), "", RsTRNMain.Fields("VNOSUFFIX").Value)
        txtVno.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("VNOSeq").Value), "", RsTRNMain.Fields("VNOSeq").Value), "00000")
        TxtVDate.Text = IIf(IsDBNull(RsTRNMain.Fields("VDate").Value), "", RsTRNMain.Fields("VDate").Value)
        txtExpDate.Text = IIf(IsDBNull(RsTRNMain.Fields("EXPDate").Value), "", RsTRNMain.Fields("EXPDate").Value)
        If MainClass.ValidateWithMasterTable((RsTRNMain.Fields("BOOKCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPartyName.Text = MasterNo
            If IsDate(TxtVDate.Text) Then
                mOPBal = GetOpeningBal((RsTRNMain.Fields("BOOKCODE").Value), (TxtVDate.Text))
            End If
            lblBookBalAmt.Text = VB6.Format(System.Math.Abs(mOPBal), "0.00")
            lblBookBalDC.Text = IIf(mOPBal >= 0, "Dr", "Cr")
        Else
            txtPartyName.Text = ""
        End If
        txtNarration.Text = IIf(IsDBNull(RsTRNMain.Fields("NARRATION").Value), "", RsTRNMain.Fields("NARRATION").Value)
        chkCancelled.CheckState = IIf(RsTRNMain.Fields("CANCELLED").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
        chkPnL.CheckState = IIf(RsTRNMain.Fields("PL_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
        'If InStr(1, XRIGHT, "M") = 0 Then
        '    chkCancelled.Enabled = False
        'Else
        '    chkCancelled.Enabled = IIf(RsTRNMain.Fields("CANCELLED").Value = "N", True, False)
        'End If
        chkCancelled.Enabled = IIf(RsTRNMain.Fields("CANCELLED").Value = "N", IIf(PubUserID = "G0416", True, False), False)      '

        mValue = IIf(IsDBNull(RsTRNMain.Fields("ISTDSDEDUCT").Value), "N", RsTRNMain.Fields("ISTDSDEDUCT").Value)

        chkTDS.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkTDS.Enabled = False

        mValue = IIf(IsDBNull(RsTRNMain.Fields("REVERSE_CHARGE_APP").Value), "N", RsTRNMain.Fields("REVERSE_CHARGE_APP").Value)
        chkReverseCharge.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        '    chkReverseCharge.Enabled = False										
        txtJVTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("TDSPer").Value), "", RsTRNMain.Fields("TDSPer").Value), "0.000")
        txtJVTDSAmount.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("TDSAMOUNT").Value), "", RsTRNMain.Fields("TDSAMOUNT").Value), "0.00")

        mValue = IIf(IsDBNull(RsTRNMain.Fields("ISESIDEDUCT").Value), "N", RsTRNMain.Fields("ISESIDEDUCT").Value)
        chkESI.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkESI.Enabled = False
        txtESIRate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("ESIPer").Value), "", RsTRNMain.Fields("ESIPer").Value), "0.000")
        txtESIAmount.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("ESIAMOUNT").Value), "", RsTRNMain.Fields("ESIAMOUNT").Value), "0.00")

        mValue = IIf(IsDBNull(RsTRNMain.Fields("ISSTDSDEDUCT").Value), "N", RsTRNMain.Fields("ISSTDSDEDUCT").Value)
        ChkSTDS.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkSTDS.Enabled = False

        txtSTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("STDSPer").Value), "", RsTRNMain.Fields("STDSPer").Value), "0.000")
        txtSTDSAmount.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("STDSAMOUNT").Value), "", RsTRNMain.Fields("STDSAMOUNT").Value), "0.00")
        txtJVVNO.Text = IIf(IsDBNull(RsTRNMain.Fields("JVNO").Value), "", RsTRNMain.Fields("JVNO").Value)
        chkSuppBill.CheckState = IIf(RsTRNMain.Fields("ISSUPPBill").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkCapital.CheckState = IIf(RsTRNMain.Fields("ISCAPITAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkModvat.CheckState = IIf(RsTRNMain.Fields("ISMODVAT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPLA.CheckState = IIf(RsTRNMain.Fields("ISPLA").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkSTClaim.CheckState = IIf(RsTRNMain.Fields("ISSTCLAIM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkServTaxClaim.CheckState = IIf(RsTRNMain.Fields("ISSERVTAXCLAIM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkServTaxRefund.CheckState = IIf(RsTRNMain.Fields("ISSERVTAXREFUND").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        txtModvatNo.Text = IIf(IsDBNull(RsTRNMain.Fields("MODVATNO").Value), "", RsTRNMain.Fields("MODVATNO").Value)
        txtSTRefundNo.Text = IIf(IsDBNull(RsTRNMain.Fields("STREFUNDNO").Value), "", RsTRNMain.Fields("STREFUNDNO").Value)
        txtServNo.Text = IIf(IsDBNull(RsTRNMain.Fields("SERVNO").Value), "", RsTRNMain.Fields("SERVNO").Value)
        xPrevModvatNo = IIf(IsDBNull(RsTRNMain.Fields("MODVATNO").Value), 0, RsTRNMain.Fields("MODVATNO").Value)
        xPrevSTRefundNo = IIf(IsDBNull(RsTRNMain.Fields("STREFUNDNO").Value), 0, RsTRNMain.Fields("STREFUNDNO").Value)
        xPrevVnoStr = IIf(IsDBNull(RsTRNMain.Fields("JVNO").Value), "", RsTRNMain.Fields("JVNO").Value)
        xPrevISCapital = IIf(IsDBNull(RsTRNMain.Fields("ISCAPITAL").Value), "N", RsTRNMain.Fields("ISCAPITAL").Value)
        xPrevSuppBill = IIf(IsDBNull(RsTRNMain.Fields("ISSUPPBill").Value), "N", RsTRNMain.Fields("ISSUPPBill").Value)
        xPrevISPLA = IIf(IsDBNull(RsTRNMain.Fields("ISPLA").Value), "N", RsTRNMain.Fields("ISPLA").Value)
        xPrevServNo = IIf(IsDBNull(RsTRNMain.Fields("SERVNO").Value), 0, RsTRNMain.Fields("SERVNO").Value)
        xPrevServTaxClaim = IIf(IsDBNull(RsTRNMain.Fields("ISSERVTAXCLAIM").Value), "N", RsTRNMain.Fields("ISSERVTAXCLAIM").Value)
        xPrevServTaxRefund = IIf(IsDBNull(RsTRNMain.Fields("ISSERVTAXREFUND").Value), "N", RsTRNMain.Fields("ISSERVTAXREFUND").Value)
        mPartyCode = IIf(IsDBNull(RsTRNMain.Fields("IMP_SUPP_CUST_CODE").Value), "", RsTRNMain.Fields("IMP_SUPP_CUST_CODE").Value)
        If MainClass.ValidateWithMasterTable(mPartyCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtImpPartyName.Text = MasterNo
        Else
            txtImpPartyName.Text = ""
        End If
        txtImpMRRNo.Text = IIf(IsDBNull(RsTRNMain.Fields("IMP_MRR_NO").Value), "", RsTRNMain.Fields("IMP_MRR_NO").Value)
        txtImpBillNo.Text = IIf(IsDBNull(RsTRNMain.Fields("IMP_BILL_NO").Value), "", RsTRNMain.Fields("IMP_BILL_NO").Value)
        txtImpBillDate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("IMP_BILL_DATE").Value), "", RsTRNMain.Fields("IMP_BILL_DATE").Value), "DD/MM/YYYY")
        mPartyCode = IIf(IsDBNull(RsTRNMain.Fields("EXP_SUPP_CUST_CODE").Value), "", RsTRNMain.Fields("EXP_SUPP_CUST_CODE").Value)
        If MainClass.ValidateWithMasterTable(mPartyCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtExpPartyName.Text = MasterNo
        Else
            txtExpPartyName.Text = ""
        End If
        txtExpBillNo.Text = IIf(IsDBNull(RsTRNMain.Fields("EXP_BILL_NO").Value), "", RsTRNMain.Fields("EXP_BILL_NO").Value)
        txtExpBillDate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("EXP_BILL_DATE").Value), "", RsTRNMain.Fields("EXP_BILL_DATE").Value), "DD/MM/YYYY")
        mServiceCode = IIf(IsDBNull(RsTRNMain.Fields("SERVICE_CODE").Value), -1, RsTRNMain.Fields("SERVICE_CODE").Value)
        If MainClass.ValidateWithMasterTable(mServiceCode, "CODE", "NAME", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtServProvided.Text = Trim(MasterNo)
        Else
            txtServProvided.Text = ""
        End If
        txtServiceOn.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("SERVICE_ON_AMT").Value), 0, RsTRNMain.Fields("SERVICE_ON_AMT").Value), "0.00")
        txtServiceTaxPer.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("SERVICE_TAX_PER").Value), 0, RsTRNMain.Fields("SERVICE_TAX_PER").Value), "0.00")
        txtServiceTaxAmount.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("SERVICE_TAX_AMOUNT").Value), 0, RsTRNMain.Fields("SERVICE_TAX_AMOUNT").Value), "0.00")
        txtProviderPer.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("SERV_PROVIDER_PER").Value), 0, RsTRNMain.Fields("SERV_PROVIDER_PER").Value), "0.00")
        txtRecipientPer.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("SERV_RECIPIENT_PER").Value), 0, RsTRNMain.Fields("SERV_RECIPIENT_PER").Value), "0.00")
        mAuthorised = IIf(IsDBNull(RsTRNMain.Fields("Authorised").Value), "N", RsTRNMain.Fields("Authorised").Value)
        lblReversalMade.Text = IIf(IsDBNull(RsTRNMain.Fields("IS_REVERSAL_MADE").Value), "", RsTRNMain.Fields("IS_REVERSAL_MADE").Value)
        lblReversalVoucher.Text = IIf(IsDBNull(RsTRNMain.Fields("IS_REVERSAL_VOUCHER").Value), "", RsTRNMain.Fields("IS_REVERSAL_VOUCHER").Value)
        lblReversalMkey.Text = IIf(IsDBNull(RsTRNMain.Fields("REVERSAL_MKEY").Value), "", RsTRNMain.Fields("REVERSAL_MKEY").Value)
        lblAddUser.Text = IIf(IsDBNull(RsTRNMain.Fields("ADDUSER").Value), "", RsTRNMain.Fields("ADDUSER").Value)
        lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("ADDDATE").Value), "", RsTRNMain.Fields("ADDDATE").Value), "DD/MM/YYYY")
        lblModUser.Text = IIf(IsDBNull(RsTRNMain.Fields("MODUSER").Value), "", RsTRNMain.Fields("MODUSER").Value)
        lblModDate.Text = VB6.Format(IIf(IsDBNull(RsTRNMain.Fields("MODDATE").Value), "", RsTRNMain.Fields("MODDATE").Value), "DD/MM/YYYY")
        '    txtModvatNo.Enabled = False										
        '    txtSTRefundNo.Enabled = False										
        '										
        '    chkSuppBill.Enabled = False										
        '    chkCapital.Enabled = False				

        pSectionCode = IIf(IsDBNull(RsTRNMain.Fields("SECTION_CODE").Value), -1, RsTRNMain.Fields("SECTION_CODE").Value)

        If pSectionCode > 0 Then
            If MainClass.ValidateWithMasterTable(pSectionCode, "CODE", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtTDSSection.Text = MasterNo
            End If
        End If


        ShowDetail()
        ShowTDSDetail()
        CopyToTempPRDetail()
        CopyToTempLoanDetail()
        '    CopyToTempBillDetail CurMKey										
        CalcTots()
        SprdMain.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsTRNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        cmdAuthorised.Enabled = IIf(mAuthorised = "Y", False, cmdAuthorised.Enabled)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume										
    End Sub
    Private Sub CopyToTempPRDetail()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "Insert Into FIN_TEMPBILL_TRN  ( " & vbCrLf _
            & " UserId, TRNDTLSUBROWNO, SUBROWNO, " & vbCrLf _
            & " ACCOUNTCODE, BILLNO, BILLDATE, " & vbCrLf _
            & " BILLAMOUNT, BILLDC, TRNTYPE, " & vbCrLf _
            & " Amount, DC, BOOKTYPE, REMARKS,  " & vbCrLf _
            & " OldAmount, OldDC, OldBillNo, " & vbCrLf _
            & " OldPayType,DUEDATE, " & vbCrLf _
            & " STTYPE, STFORMNAME, STFORMNO, " & vbCrLf _
            & " STFORMDATE, STFORMAMT, STDUEFORMNAME, " & vbCrLf _
            & " STDUEFORMNO, STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf _
            & " ISREGDNO, STFORMCODE, STDUEFORMCODE,TAXABLE_AMOUNT,PONO,DIV_CODE,REF_NO,TEMPMKEY,BILL_TO_LOC_ID,BILL_COMPANY_CODE, TDS_AMOUNT, INTEREST_AMOUNT " & vbCrLf _
            & " )"

        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "' , " & vbCrLf _
            & " TRNDTLSUBROWNO, SUBROWNO, " & vbCrLf _
            & " ACCOUNTCODE, BILLNO, BILLDATE, " & vbCrLf _
            & " BILLAMOUNT, BILLDC,TRNTYPE,Amount,DC, " & vbCrLf _
            & " '" & Trim(UCase(lblBookType.Text)) & "', " & vbCrLf _
            & " REMARKS, AMOUNT, DC, BILLNO, TRNTYPE,DUEDATE, " & vbCrLf _
            & " STTYPE, STFORMNAME, STFORMNO, " & vbCrLf _
            & " STFORMDATE, STFORMAMT, STDUEFORMNAME, " & vbCrLf _
            & " STDUEFORMNO, STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf _
            & " ISREGDNO, STFORMCODE, STDUEFORMCODE,TAXABLE_AMOUNT,PONO,DIV_CODE,REF_NO," & pProcessKey & ",BILL_TO_LOC_ID, BILL_COMPANY_CODE, TDS_AMOUNT, INTEREST_AMOUNT" & vbCrLf _
            & " FROM FIN_BILLDETAILS_TRN Where MKey='" & CurMKey & "'"
        PubDBCn.Execute(SqlStr)


        SqlStr = "INSERT INTO FIN_TEMP_SERVICE_TRN  ( " & vbCrLf _
            & " USERID, SUBROWNO, ACCOUNTCODE, " & vbCrLf _
            & " RO, BILLNO, BILLDATE, BILLAMOUNT, " & vbCrLf _
            & " TAX_ON, SERVICETAX_AMT, CESS_AMT, " & vbCrLf _
            & " SERV_PROV, ISSERVICECLAIM, SERVNO, " & vbCrLf _
            & " SERVDATE, SERVICE_PER, CESS_PER, " & vbCrLf _
            & " SHE_CESS_PER,SHE_CESS_AMT, SERVICE_PER_PROV, SERVICE_PER_REC, " & vbCrLf _
            & " SERVICETAX_REC_AMT, CESS_REC_AMT, SHE_CESS_REC_AMT, " & vbCrLf _
            & " SWACHH_CESS_PER, SWACHH_CESS_AMOUNT, SWACHH_CESS_AMOUNT_REC, " & vbCrLf _
            & " KK_CESS_PER, KK_CESS_AMOUNT, KK_CESS_AMOUNT_REC)"

        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "' , " & vbCrLf & " SUBROWNO, ACCOUNTCODE, " & vbCrLf & " RO, BILLNO, BILLDATE, BILLAMOUNT, " & vbCrLf & " TAX_ON, SERVICETAX_AMT, CESS_AMT, " & vbCrLf & " SERV_PROV, ISSERVICECLAIM, SERVNO, " & vbCrLf & " SERVDATE, SERVICE_PER, CESS_PER,SHE_CESS_PER,SHE_CESS_AMT,SERVICE_PER_PROV, SERVICE_PER_REC, " & vbCrLf & " SERVICETAX_REC_AMT, CESS_REC_AMT, SHE_CESS_REC_AMT, " & vbCrLf & " SWACHH_CESS_PER, SWACHH_CESS_AMOUNT, SWACHH_CESS_AMOUNT_REC, " & vbCrLf & " KK_CESS_PER, KK_CESS_AMOUNT, KK_CESS_AMOUNT_REC " & vbCrLf & " FROM FIN_SERVTAXDETAILS_TRN Where MKey='" & CurMKey & "'"
        PubDBCn.Execute(SqlStr)
        Exit Sub
ERR1:
        '    Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CopyToTempLoanDetail()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        SqlStr = "INSERT INTO TEMP_PAY_LOAN_MST  ( " & vbCrLf & " USERID, EMP_CODE, SUBROWNO, " & vbCrLf & " LOANTYPE, LOANAMOUNT, LOANDATE, " & vbCrLf & " INSTALMENTAMT, INTERESTCALC, INTERESTRATE, " & vbCrLf & " DEDUCT_DATE, STARTINGMONTH, STARTINGYEAR, " & vbCrLf & " OPPRINCIPALAMT, INTERESTAMT, PRINCIPALAMT, " & vbCrLf & " DEDUCT_AMOUNT, BALANCE_AMOUNT, PAID_AMOUNT )"
        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "' , EMP_CODE, SUBROWNO, " & vbCrLf & " LOANTYPE, LOANAMOUNT, LOANDATE, " & vbCrLf & " INSTALMENTAMT, INTERESTCALC, INTERESTRATE, " & vbCrLf & " DEDUCT_DATE, STARTINGMONTH, STARTINGYEAR, " & vbCrLf & " OPPRINCIPALAMT, INTERESTAMT, PRINCIPALAMT, " & vbCrLf & " DEDUCT_AMOUNT, BALANCE_AMOUNT, PAID_AMOUNT " & vbCrLf & " FROM PAY_LOAN_MST  Where MKey='" & CurMKey & "'"
        PubDBCn.Execute(SqlStr)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowDetail()
        On Error GoTo ShowErr
        Dim SqlStr As String = ""
        Dim mAccountCode As String
        Dim mNewAccountCode As String
        Dim mValue As String

        ', FIN_SUPP_CUST_MST.SUPP_CUST_NAME AS ACCOUNTNAME										
        '',FIN_SUPP_CUST_MST										
        SqlStr = "SELECT FIN_VOUCHER_DET.*" & vbCrLf & " FROM FIN_VOUCHER_DET WHERE MKEY= '" & CurMKey & "' Order By SubRowNo" '& vbCrLf |            & " AND FIN_VOUCHER_DET.ACCOUNTCODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE AND " & vbCrLf |            & " "										
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNDetail, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTRNDetail.EOF = True Then Exit Sub
        Do While RsTRNDetail.EOF = False
            SprdMain.Row = SprdMain.MaxRows
            SprdMain.Col = ColPRRowNo
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("PRRowNo").Value), 0, RsTRNDetail.Fields("PRRowNo").Value))
            SprdMain.Col = ColDC
            SprdMain.Text = RsTRNDetail.Fields("DC").Value + "r"
            mNewAccountCode = GetDummyAccountCode((RsTRNDetail.Fields("ACCOUNTCODE").Value), CurMKey)
            SprdMain.Col = ColAccountName
            If MainClass.ValidateWithMasterTable(mNewAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SprdMain.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If
            '        SprdMain.Text = IIf(IsNull(RsTRNDetail.Fields("AccountName").Value), "", RsTRNDetail.Fields("AccountName").Value)										
            SprdMain.Col = ColParticulars
            SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("PARTICULARS").Value), "", RsTRNDetail.Fields("PARTICULARS").Value)
            SprdMain.Col = ColChequeNo
            SprdMain.Text = IIf(Not IsDBNull(RsTRNDetail.Fields("ChequeNo").Value), RsTRNDetail.Fields("ChequeNo").Value, "")
            SprdMain.Col = ColChequeDate
            SprdMain.Text = VB6.Format(IIf(Not IsDBNull(RsTRNDetail.Fields("CHQDATE").Value), RsTRNDetail.Fields("CHQDATE").Value, ""), "DD/MM/YYYY")
            SprdMain.Col = ColCC
            mValue = IIf(IsDBNull(RsTRNDetail.Fields("COSTCCODE").Value), "", RsTRNDetail.Fields("COSTCCODE").Value)
            If Trim(mValue) <> "-1" Then
                '            If MainClass.ValidateWithMasterTable(RsTRNDetail.Fields("CostCCode").Value, "COST_CENTER_CODE", "Alias", "CST_CENTER_MST", PubDBCn, MasterNo) = True Then										
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)										
                '            End If										
                SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("COSTCCODE").Value), "", RsTRNDetail.Fields("COSTCCODE").Value)
            Else
                SprdMain.Text = ""
            End If
            SprdMain.Col = ColExp
            mValue = IIf(IsDBNull(RsTRNDetail.Fields("EXP_CODE").Value), "", RsTRNDetail.Fields("EXP_CODE").Value)
            If Trim(mValue) <> "-1" Then
                SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("EXP_CODE").Value), "", RsTRNDetail.Fields("EXP_CODE").Value)
            Else
                SprdMain.Text = ""
            End If
            SprdMain.Col = ColDept
            mValue = IIf(IsDBNull(RsTRNDetail.Fields("DeptCode").Value), "", RsTRNDetail.Fields("DeptCode").Value)
            If Trim(mValue) <> "-1" Then
                '            If MainClass.ValidateWithMasterTable(RsTRNDetail.Fields("DeptCode").Value, "Code", "Alias", "Dept", PubDBCn, MasterNo) = True Then										
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)										
                '            End If										
                SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("DeptCode").Value), "", RsTRNDetail.Fields("DeptCode").Value)
            Else
                SprdMain.Text = ""
            End If
            SprdMain.Col = ColDivisionCode
            If RsTRNDetail.Fields("DIV_CODE").Value <> -1 Then
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTRNDetail.Fields("DIV_CODE").Value), "", RsTRNDetail.Fields("DIV_CODE").Value)))
            Else
                SprdMain.Text = ""
            End If
            SprdMain.Col = ColEmp
            mValue = IIf(IsDBNull(RsTRNDetail.Fields("EMPCODE").Value), "", RsTRNDetail.Fields("EMPCODE").Value)
            If Trim(mValue) <> "-1" Then
                '            If MainClass.ValidateWithMasterTable(RsTRNDetail.Fields("EMPCODE").Value, "Code", "Alias", "Emp", PubDBCn, MasterNo) = True Then										
                '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)										
                '            End If										
                SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("EMPCODE").Value), "", RsTRNDetail.Fields("EMPCODE").Value)
            Else
                SprdMain.Text = ""
            End If
            SprdMain.Col = ColIBRNo
            SprdMain.Text = IIf(Not IsDBNull(RsTRNDetail.Fields("IBRNo").Value), RsTRNDetail.Fields("IBRNo").Value, "")
            SprdMain.Col = ColAmount
            SprdMain.Text = Str(RsTRNDetail.Fields("Amount").Value)
            SprdMain.Col = ColSAC
            SprdMain.Text = IIf(Not IsDBNull(RsTRNDetail.Fields("SAC").Value), RsTRNDetail.Fields("SAC").Value, "")
            SprdMain.Col = ColCGSTPer
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("CGST_PER").Value), 0, RsTRNDetail.Fields("CGST_PER").Value))
            SprdMain.Col = ColCGSTAmount
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("CGST_AMOUNT").Value), 0, RsTRNDetail.Fields("CGST_AMOUNT").Value))
            SprdMain.Col = ColSGSTPer
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("SGST_PER").Value), 0, RsTRNDetail.Fields("SGST_PER").Value))
            SprdMain.Col = ColSGSTAmount
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("SGST_AMOUNT").Value), 0, RsTRNDetail.Fields("SGST_AMOUNT").Value))
            SprdMain.Col = ColIGSTPer
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("IGST_PER").Value), 0, RsTRNDetail.Fields("IGST_PER").Value))
            SprdMain.Col = ColIGSTAmount
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("IGST_AMOUNT").Value), 0, RsTRNDetail.Fields("IGST_AMOUNT").Value))
            SprdMain.Col = ColSaleBillPrefix
            SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("SALEBILLNOPREFIX").Value), "", RsTRNDetail.Fields("SALEBILLNOPREFIX").Value)
            SprdMain.Col = ColSaleBillSeq
            SprdMain.Text = Str(IIf(IsDBNull(RsTRNDetail.Fields("SALEBILLNOSEQ").Value), 0, RsTRNDetail.Fields("SALEBILLNOSEQ").Value))
            SprdMain.Col = ColSaleBillNo
            SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("SALEBILL_NO").Value), "", RsTRNDetail.Fields("SALEBILL_NO").Value)
            SprdMain.Col = ColSaleBillDate
            SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("SALEBILLDATE").Value), "", RsTRNDetail.Fields("SALEBILLDATE").Value)

            SprdMain.Col = ColClearDate
            SprdMain.Text = IIf(IsDBNull(RsTRNDetail.Fields("ClearDate").Value), "", RsTRNDetail.Fields("ClearDate").Value)

            SprdMain.Col = ColSaleBillNo
            If lblSaleBillNo.Text = "" Then
                lblSaleBillNo.Text = IIf(SprdMain.Text = "", "", SprdMain.Text)
            Else
                lblSaleBillNo.Text = IIf(SprdMain.Text = "", lblSaleBillNo.Text, lblSaleBillNo.Text & "," & SprdMain.Text)
            End If
            SprdMain.MaxRows = SprdMain.MaxRows + 1
            RsTRNDetail.MoveNext()
        Loop
        '    FormatSprdMain -1										
        FormatSprdMainGST(-1)
        Exit Sub
ShowErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume										
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mAcctCode As String
        Dim mPRRowNo As Integer
        Dim mAmount As Double
        Dim mDC As String = ""
        Dim ii As Integer
        Dim mEmpCode As String
        Dim mLockBookCode As Integer
        Dim mIsTDSAccount As Boolean
        Dim pTDSChallanNo As String = ""
        Dim pVNo As String = ""
        Dim mServiceClaimCode As String
        Dim mISServiceClaim As Boolean
        Dim pClaimNo As String = ""
        Dim mServiceTaxHeadCount As Integer
        Dim mPartyName As String = ""
        Dim mChequeNo As String
        Dim mIsAuthorisedUser As String
        Dim mPANNo As String
        Dim mHeadType As String = ""
        Dim mDRCRBal As Double
        Dim xDivName As String
        Dim mServiceGL As String
        Dim xUnlockVType As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRefNo As String
        Dim mSACCode As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        'Dim mAmount As Double										
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mCheckSAC As Integer
        Dim mReversalVoucher As String = ""
        Dim mReversalMkey As String = ""
        Dim mBankCode As String = ""
        Dim mLenderBankCode As String
        mIsTDSAccount = False
        '    mISServiceClaim = False										
        FieldsVerification = False
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            FieldsVerification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtPartyName.Text)) = True Then
            FieldsVerification = False
            Exit Function
        End If
        xUnlockVType = ""
        If lblBookType.Text = ConCashReceipt Then
            mLockBookCode = CInt(ConLockCashReceipt)
            xUnlockVType = "R"
        ElseIf lblBookType.Text = ConCashPayment Then
            mLockBookCode = CInt(ConLockCashPayment)
            xUnlockVType = "P"
        ElseIf lblBookType.Text = ConBankReceipt Then
            mLockBookCode = CInt(ConLockBankReceipt)
            xUnlockVType = "R"
        ElseIf lblBookType.Text = ConBankPayment Then
            mLockBookCode = CInt(ConLockBankPayment)
            xUnlockVType = "P"
        ElseIf lblBookType.Text = ConPDCReceipt Then
            mLockBookCode = CInt(ConLockPDCReceipt)
            xUnlockVType = "R"
        ElseIf lblBookType.Text = ConPDCPayment Then
            mLockBookCode = CInt(ConLockPDCPayment)
            xUnlockVType = "P"
        Else
            mLockBookCode = CInt(ConLockJournal)
            xUnlockVType = ""
        End If
        If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(MainClass.AllowSingleQuote(Trim(txtVType.Text)) & Trim(txtVNo1.Text)) & Trim(txtVno.Text) & MainClass.AllowSingleQuote(Trim(txtVNoSuffix.Text)), (TxtVDate.Text), xUnlockVType) = False Then
            If ValidateBookLocking(PubDBCn, mLockBookCode, (TxtVDate.Text)) = True Then
                FieldsVerification = False
                Exit Function
            End If
            If ValidateBookLocking(PubDBCn, mLockBookCode, (txtExpDate.Text)) = True Then
                FieldsVerification = False
                Exit Function
            End If
        End If
        mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, pMYMenu, PubDBCn)
        If MODIFYMode = True Then
            If InStr(1, mIsAuthorisedUser, "S") = 0 Then
                '        If PubUserID <> "G0416" Then										
                If RsTRNMain.Fields("AUTHORISED").Value = "Y" Then
                    MsgBox("You Cann't Modify Authorised Voucher", MsgBoxStyle.Information)
                    FieldsVerification = False
                    Exit Function
                End If
            End If
            If MainClass.ValidateWithMasterTable(RsTRNMain.Fields("mKey").Value, "JVMKEY", "VNO", "FIN_SUPP_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If MsgQuestion("Purchase Supplementary Invoice (" & MasterNo & ") Had Entered For this Voucher. You Want to Save This Voucher ...") = CStr(MsgBoxResult.No) Then
                    FieldsVerification = False
                    Exit Function
                End If
            End If
        End If
        If lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then
        Else
            If InStr(1, mIsAuthorisedUser, "S") = 0 Then
                If CheckLastestVDate(CDate(TxtVDate.Text), (txtVType.Text)) = False Then ''If CheckBackDateEntry(TxtVDate.Text) = True Then										
                    MsgBox("You Cann't Add/Modify back date Voucher", MsgBoxStyle.Information)
                    FieldsVerification = False
                    Exit Function
                End If
            Else
                '        If MainClass.GetUserCanModify(TxtVDate.Text) = False Then										
                '            MsgBox "You Have Not Rights to change back Voucher", vbInformation										
                '            FieldsVerification = False										
                '            Exit Function										
                '        End If										
            End If

            If CDate(VB6.Format(TxtVDate.Text, "DD/MM/YYYY")) > CDate(VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
                MsgInformation("Voucher Date is Greater Than Current Date. Cannot Save")
                TxtVDate.Focus()
                FieldsVerification = False
                Exit Function
            End If

        End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MsgQuestion("Want to Cancelled the Complete Voucher") = CStr(MsgBoxResult.No) Then
                FieldsVerification = False
                Exit Function
            End If
        End If
        If lblReversalMade.Text = "Y" Then
            If MainClass.ValidateWithMasterTable(lblReversalMkey.Text, "REVERSAL_MKEY", "VNO", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mReversalVoucher = MasterNo
            End If
            MsgBox("Reversal Voucher " & mReversalVoucher & " made against this Voucher, So cann't be Change.", MsgBoxStyle.Information)
            FieldsVerification = False
            Exit Function
        End If
        If lblReversalVoucher.Text = "Y" Then
            mReversalVoucher = ""
            If MainClass.ValidateWithMasterTable(lblReversalMkey.Text, "MKEY", "VNO", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mReversalVoucher = MasterNo
            End If
            MsgBox("This is a Reversal Voucher of " & mReversalVoucher & ", So cann't be Change.", MsgBoxStyle.Information)
            FieldsVerification = False
            Exit Function
        End If
        With SprdMain
            For ii = 1 To .MaxRows
                .Row = ii
                .Col = ColAccountName
                If Trim(.Text) = "" Then Exit For
                If ValidateAccountLocking(PubDBCn, TxtVDate.Text, .Text) = True Then
                    FieldsVerification = False
                    Exit Function
                End If
            Next
        End With

        If MainClass.ValidDataInGrid(SprdMain, ColAmount, "N", "Please check. Either Amount is Missing all the rows are marked for deletion") = False Then Exit Function

        FieldsVerification = False
        If (lblBookType.Text <> ConJournal And lblBookType.Text <> ConContra) And txtPartyName.Text = "" Then
            MsgInformation("Account Name missing")
            txtPartyName.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If Trim(txtPartyName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_Name", "STATUS", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If MasterNo = "C" Then
                    MsgInformation("Account is closed. So that you Cann't Save. ")
                    FieldsVerification = False
                    Exit Function
                End If
            End If
        End If
        If Trim(txtVType.Text) = "" Then
            MsgInformation("Voucher Type is Blank")
            txtVType.Focus()
            FieldsVerification = False
            Exit Function
        End If
        '    If ADDMode = True Then										
        'If CheckVType() = False Then
        '    MsgInformation("Either Voucher Type is not valid or Not in your series.")
        '    If txtVType.Enabled = True Then txtVType.Focus()
        '    FieldsVerification = False
        '    Exit Function
        'End If
        '    End If										
        If Trim(txtVno.Text) <> "" Then
            If Val(txtVno.Text) = 0 Then
                MsgInformation("Invalid Voucher No. Cann't be Saved.")
                txtVno.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        If MODIFYMode = True And Trim(txtVno.Text) = "" Then
            MsgInformation("Voucher No. is Blank")
            txtVno.Focus()
            FieldsVerification = False
            Exit Function
        End If
        If lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConPDCPayment Then GoTo NextLine
        If FYChk(TxtVDate.Text) = False Then
            '        MsgInformation "Date is not in the Current Financial Year"										
            If TxtVDate.Enabled = True Then TxtVDate.Focus()
            Exit Function
        End If
        If FYChk(txtExpDate.Text) = False Then
            '        MsgInformation "Date is not in the Current Financial Year"										
            If txtExpDate.Enabled = True Then txtExpDate.Focus()
            Exit Function
        End If
        If Trim(lblSaleBillNo.Text) <> "" Then
            MsgBox("Reverse Charge Sale Bill is Generated agt Bill No. " & lblSaleBillNo.Text & ", So Cann't be Save.", MsgBoxStyle.Information)
            FieldsVerification = False
            Exit Function
        End If
NextLine:
        If lblBookType.Text = ConJournal Or lblBookType.Text = ConContra Then
            If Val(LblNetAmt.Text) <> 0 Then
                MsgInformation("Dr./Cr. Mismatch, Voucher Not Saved")
                Exit Function
            End If
            ''Division wise check										
            '        mDRCRBal = 0										
            '        xDivName = ""										
            '        If CheckDivisionWiseDRCRMatch(mDRCRBal, xDivName) = False Then										
            '            MsgInformation "Division Wise Dr./Cr. Mismatch. Amount Diff is " & mDRCRBal & " in Division : " & xDivName & ". Voucher Not Saved"										
            '            Exit Function										
            '        End If										
        End If
        If Val(txtJVTDSRate.Text) > 100 Then
            MsgBox("TDS RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVerification = False
            Exit Function
        End If
        If Val(txtESIRate.Text) > 100 Then
            MsgBox("ESI RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVerification = False
            Exit Function
        End If
        If Val(txtSTDSRate.Text) > 100 Then
            MsgBox("STDS RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVerification = False
            Exit Function
        End If
        If ADDMode = True Then
            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtJVTDSAmount.Text) = 0 Then
                MsgBox("TDS Amount is Zero. Please Check", MsgBoxStyle.Information)
                FieldsVerification = False
                Exit Function
            End If

            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked And Trim(txtTDSSection.Text) = "" Then
                MsgBox("Please Check TDS Section.", MsgBoxStyle.Information)
                FieldsVerification = False
                Exit Function
            End If

            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
                If MainClass.ValidateWithMasterTable(txtTDSSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Please Check TDS Section.", MsgBoxStyle.Information)
                    FieldsVerification = False
                    Exit Function
                End If
            End If

            If chkESI.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtESIAmount.Text) = 0 Then
                MsgBox("ESI Amount is Zero. Please Check", MsgBoxStyle.Information)
                FieldsVerification = False
                Exit Function
            End If
            If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtSTDSAmount.Text) = 0 Then
                MsgBox("STDS Amount is Zero. Please Check", MsgBoxStyle.Information)
                FieldsVerification = False
                Exit Function
            End If
        End If
        If MODIFYMode = True Then
            If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConBankReceipt Then
                SqlStr = "SELECT * FROM FIN_ADVANCE_HDR WHERE BANKVOUCHERMKEY='" & CurMKey & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    mRefNo = RsTemp.Fields("VNO").Value & "-" & VB6.Format(RsTemp.Fields("VDATE").Value, "DD/MM/YYYY")
                    MsgInformation("This Voucher is Update against Advance Voucher No : " & mRefNo & ", so cann't be modify.")
                    FieldsVerification = False
                    Exit Function
                End If
            End If
        End If
        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = cntRow
            SprdMain.Col = ColDC
            If UCase(SprdMain.Text) = "CR" Then
                SprdMain.Col = ColAccountName
                mPartyName = Trim(SprdMain.Text)
                If GetAccountBalancingMethod(mPartyName, False) = "D" Then
                    Exit For
                End If
            End If
        Next
        If lblBookType.Text = ConBankPayment Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mBankCode = MasterNo
            End If
        End If
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow
            SprdMain.Col = ColPRRowNo
            mPRRowNo = Val(SprdMain.Text)
            SprdMain.Col = ColAmount
            mAmount = Val(SprdMain.Text)
            SprdMain.Col = ColDC
            mDC = SprdMain.Text
            SprdMain.Col = ColChequeNo
            mChequeNo = SprdMain.Text
            If (lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment) And mChequeNo <> "" Then
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And Len(mChequeNo) > 6 Then

                Else
                    If GetChequeStatus(mChequeNo) = False Then
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColChequeNo)
                        Exit Function
                    End If
                End If

            End If
            SprdMain.Col = ColAccountName
            If MainClass.ValidateWithMasterTable(SprdMain.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAcctCode = MasterNo
                If lblBookType.Text = ConBankPayment And ADDMode = True Then
                    If MainClass.ValidateWithMasterTable(mAcctCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_BANK='Y'") = True Then
                        MsgBox("Payment Lock for Such Customer/Supplier, So cann't be saved", MsgBoxStyle.Information)
                        FieldsVerification = False
                        Exit Function
                    End If
                    mLenderBankCode = ""
                    If MainClass.ValidateWithMasterTable(mAcctCode, "SUPP_CUST_CODE", "LENDER_BANK_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLenderBankCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    If Trim(mLenderBankCode) <> "" Then
                        If mLenderBankCode <> mBankCode Then
                            If MsgQuestion("Lender Bank is not match with payment bank. Are you want to continue...") = CStr(MsgBoxResult.No) Then
                                FieldsVerification = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
                If CheckExpHead((SprdMain.Text)) = True Then
                    '                    If CDate(TxtVDate.Text) >= CDate(PubGSTApplicableDate) Then
                    '                        SprdMain.Col = ColDC
                    '                        If VB.Left(Trim(SprdMain.Text), 1) = "D" Then
                    '                            SprdMain.Col = ColSAC
                    '                            If Trim(SprdMain.Text) = "" Then
                    '                                If MsgQuestion("SAC Code is Missing. Are you want to continue...") = CStr(MsgBoxResult.Yes) Then
                    '                                    GoTo NextRow
                    '                                Else
                    '                                    MainClass.SetFocusToCell(SprdMain, cntRow, ColSAC)
                    '                                    Exit Function
                    '                                End If
                    '                            End If
                    '                            If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
                    '                                MsgInformation("Invalid SAC.")
                    '                                MainClass.SetFocusToCell(SprdMain, cntRow, ColSAC)
                    '                                Exit Function
                    '                            Else
                    '                                mSACCode = Trim(SprdMain.Text)
                    '                            End If
                    '                            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, "Y", "") = False Then GoTo ERR1
                    '                            SprdMain.Col = ColAmount
                    '                            mAmount = Val(SprdMain.Text)
                    '                            SprdMain.Col = ColCGSTPer
                    '                            SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                    '                            SprdMain.Col = ColSGSTPer
                    '                            SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                    '                            SprdMain.Col = ColIGSTPer
                    '                            SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                    '                            mCGSTAmount = mAmount * mCGSTPer * 0.01
                    '                            mSGSTAmount = mAmount * mSGSTPer * 0.01
                    '                            mIGSTAmount = mAmount * mIGSTPer * 0.01
                    '                            SprdMain.Col = ColCGSTAmount
                    '                            SprdMain.Text = VB6.Format(mCGSTAmount, "0.00")
                    '                            SprdMain.Col = ColSGSTAmount
                    '                            SprdMain.Text = VB6.Format(mSGSTAmount, "0.00")
                    '                            SprdMain.Col = ColIGSTAmount
                    '                            SprdMain.Text = VB6.Format(mIGSTAmount, "0.00")
                    '                            If chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    '                                If MsgQuestion("You not selected the Reverse Charge. Are you want to continue...") = CStr(MsgBoxResult.No) Then
                    '                                    FieldsVerification = False
                    '                                    Exit Function
                    '                                End If
                    '                            End If
                    'NextRow:
                    '                        End If
                    '                    End If
                    SprdMain.Col = ColCC
                    If Trim(SprdMain.Text) = "" Then
                        MsgInformation("Please Check Cost Centre is Missing.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColCC)
                        Exit Function
                    End If
                    SprdMain.Col = ColExp
                    If Trim(SprdMain.Text) = "" Then
                        MsgInformation("Please Check Expenses Centre is Missing.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColExp)
                        Exit Function
                    End If
                End If
                '            mServiceClaimCode = IIf(IsNull(RsCompany!SRVTAXACCOUNT), "", RsCompany!SRVTAXACCOUNT)										
                '            If Trim(mAcctCode) = Trim(mServiceClaimCode) And UCase(Left(mDC, 1)) = "D" Then										
                '                mISServiceClaim = True										
                '            End If										
            Else
                MsgInformation("Invaild Account Name.")
                MainClass.SetFocusToCell(SprdMain, cntRow, ColAccountName)
                Exit Function
            End If
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SprdMain.Col = ColAccountName
                If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "SUPP_CUST_NAME", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mHeadType = MasterNo
                Else
                    mHeadType = ""
                End If
                If GetAccountBalancingMethod(Trim(SprdMain.Text), False) = "D" Then
                    If CDate(TxtVDate.Text) >= CDate(PubGSTApplicableDate) Then
                        If CheckAdvanceExists(mAcctCode, mPRRowNo) = True Then
                            MsgInformation("New Bills & Advance Cann't be Enter Here.")
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColAmount)
                            FieldsVerification = False
                            Exit Function
                        End If
                    End If
                    If PayDetailExists(mAcctCode, mPRRowNo, mAmount, mDC) = False Then
                        MsgInformation("Payment Detail missing")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColAmount)
                        FieldsVerification = False
                        Exit Function
                    End If
                ElseIf GetHeadType(Trim(SprdMain.Text)) = "T" And VB.Left(mDC, 1) = "C" Then
                    mIsTDSAccount = True
                    If Trim(TxtTDSAccount.Text) = "" Then
                        MsgInformation("TDS Account Name Missing.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColAmount)
                        Exit Function
                    End If
                    If MainClass.ValidateWithMasterTable(TxtTDSAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HeadType IN ('T','1')") = False Then
                        MsgInformation("InValid TDS Account Name.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColAmount)
                        Exit Function
                    End If
                    If Trim(txtSection.Text) = "" Then
                        MsgInformation("TDS Section Name Missing.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColAmount)
                        Exit Function
                    End If
                    SprdMain.Row = 1
                    SprdMain.Col = ColAccountName
                    If UCase(Trim(txtPName.Text)) <> UCase(Trim(SprdMain.Text)) Then
                        MsgInformation("TDS Party Name Not macth with Debit Account Name")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColAmount)
                        Exit Function
                    End If
                    If Val(txtTDSAmount.Text) = 0 Then
                        MsgInformation("TDS Amount Cann't be Zero.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColAmount)
                        Exit Function
                    End If
                    If mHeadType = "T" Then ''Only TDS not Salary TDS  '06-aug-2010  ''lblBookType.text = ConJournal And CHECK IN BANK ALSO										
                        If MainClass.ValidateWithMasterTable(txtPName.Text, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mPANNo = MasterNo
                        Else
                            mPANNo = ""
                        End If
                        If Trim(mPANNo) = "" Then
                            MsgBox("PAN NO is not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
                            FieldsVerification = False
                            Exit Function
                        End If
                    End If
                ElseIf GetHeadType(Trim(SprdMain.Text)) = "S" And VB.Left(mDC, 1) = "D" Then
                    If Trim(mPartyName) <> "" Then
                        If ServiceTaxDetailExists(mAmount) = False Then
                            MsgInformation("Service Tax Detail missing")
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColAmount)
                            Exit Function
                        End If
                    End If
                    mServiceTaxHeadCount = mServiceTaxHeadCount + 1
                    '            ElseIf GetHeadType(Trim(SprdMain.Text)) = "L" Then										
                    '                If MainClass.ValidateWithMasterTable(mAcctCode, "ADV_ACCOUNT_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then										
                    '                    mEmpCode = MasterNo										
                    '                    If LoanDetailExists(mEmpCode, mAmount) = False Then										
                    '                        MsgInformation "Loan Detail Missing"										
                    '                        MainClass.SetFocusToCell SprdMain, cntRow, ColAmount										
                    '                        Exit Function										
                    '                    End If										
                    '                End If										
                End If
            End If
        Next
        mCheckSAC = 0
        If chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Checked Then
            For cntRow = 1 To SprdMain.MaxRows - 1
                SprdMain.Row = cntRow
                SprdMain.Col = ColSAC
                If Trim(SprdMain.Text) <> "" Then
                    mCheckSAC = mCheckSAC + 1
                    Exit For
                End If
            Next
            If mCheckSAC = 0 Then
                MsgInformation("SAC Code not Defined, so that please unselect the Reverse Charge.")
                Exit Function
            End If
        End If
        '    If ADDMode = True Or chkChqDeposit.Value = vbChecked Then										
        If chkChqDeposit.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CDate(VB6.Format(TxtVDate.Text, "DD/MM/YYYY")) <= CDate(VB6.Format(RunDate, "DD/MM/YYYY")) Then
                TxtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
            Else
                MsgInformation("Voucher Date is Greater Than Current Date. Cannot Save")
                TxtVDate.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        '										
        '        If Left(lblBookType.text, 1) <> Left(ConPDCPayment, 1) Then										
        '            If CheckLastestVDate(TxtVDate.Text, txtVType.Text) = False Then										
        '                MsgInformation "This Voucher Date is Locked. Please Enter the Current Date."										
        '                TxtVDate.SetFocus										
        '                Exit Function										
        '            End If										
        '        End If										
        '    End If										
        '										
        '    If Trim(txtVno.Text) <> "" Then										
        '        If Left(lblBookType.text, 1) <> Left(ConPDCPayment, 1) Then										
        '            If CheckLastestVNo(txtVno.Text, TxtVDate.Text, txtVType.Text) = False Then										
        '                MsgInformation "Voucher No. is not in Current Date Sequence."										
        '                txtVno.SetFocus										
        '                Exit Function										
        '            End If										
        '        End If										
        '    End If										
        If lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
            SprdMain.Row = 1
            SprdMain.Col = ColAccountName
            If CheckValidPartyPanNo(UCase(Trim(SprdMain.Text))) = False Then
                MsgInformation("Invalid Party PANNo, so Cann't be Saved")
                FieldsVerification = False
                Exit Function
            End If
        End If
        If lblBookType.Text = ConJournal Then
            If MODIFYMode = True Then
                If GetHeadType(Trim(SprdMain.Text)) = "T" And VB.Left(mDC, 1) = "C" Then
                    mIsTDSAccount = True
                End If
                If mIsTDSAccount = True Then
                    If GetTDSChallanMade((RsTRNMain.Fields("mKey").Value), pTDSChallanNo) = True Then
                        MsgInformation("TDS Challan No " & pTDSChallanNo & " Made, so Cann't be " & IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked, "Modified.", "Cancelled."))
                        FieldsVerification = False
                        Exit Function
                    End If
                End If
            End If
            If GetImpMRRNo(Val(txtImpMRRNo.Text), pVNo) = True Then
                MsgInformation("MRR No already Entered in " & pVNo & " Made, so Cann't be entered.")
                FieldsVerification = False
                Exit Function
            End If
            If GetExpVNo(Val(txtExpBillNo.Text), pVNo) = True Then
                MsgInformation("Export Invoice already Entered in " & pVNo & " Made, so Cann't be entered.")
                FieldsVerification = False
                Exit Function
            End If
            If mServiceTaxHeadCount > 1 Then
                MsgInformation("Please Check Service Tax Head. Only one Time you can Select Service Tax Head.")
                FieldsVerification = False
                Exit Function
            End If
            If MODIFYMode = True Then
                If PubSuperUser = "U" Then
                    If GetServiceClaimMade((RsTRNMain.Fields("mKey").Value), pClaimNo) = True Then
                        MsgInformation("Service Claim No " & pClaimNo & " Made, so Cann't be Modified")
                        FieldsVerification = False
                        Exit Function
                    End If
                End If
            End If
            If Val(txtModvatNo.Text) <> 0 And chkModvat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                chkModvat.CheckState = System.Windows.Forms.CheckState.Checked
            End If
            If Val(txtSTRefundNo.Text) <> 0 And chkSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                chkSTClaim.CheckState = System.Windows.Forms.CheckState.Checked
            End If
            If Val(txtServNo.Text) <> 0 And chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Checked
            End If
            If chkPLA.CheckState = System.Windows.Forms.CheckState.Checked Then
                If chkModvat.CheckState = System.Windows.Forms.CheckState.Unchecked And chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    MsgInformation("Please Select Modvat or Service Tax")
                    ssTab.SelectedIndex = 1
                    chkPLA.Focus()
                    FieldsVerification = False
                    Exit Function
                End If
            End If
            If chkModvat.CheckState = System.Windows.Forms.CheckState.Checked Then
                If Val(txtModvatNo.Text) = 0 Then
                    MsgInformation("Please Entered Modvat No.")
                    ssTab.SelectedIndex = 1
                    txtModvatNo.Focus()
                    FieldsVerification = False
                    Exit Function
                End If
            End If
            If chkCapital.CheckState = System.Windows.Forms.CheckState.Checked Then
                chkModvat.CheckState = System.Windows.Forms.CheckState.Checked
            End If
            If chkSTClaim.CheckState = System.Windows.Forms.CheckState.Checked Then
                If Val(txtSTRefundNo.Text) = 0 Then
                    MsgInformation("Please Entered ST Refund No.")
                    ssTab.SelectedIndex = 1
                    txtSTRefundNo.Focus()
                    FieldsVerification = False
                    Exit Function
                End If
            End If
            If chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Checked Then
                If Val(txtServNo.Text) = 0 Then
                    MsgInformation("Please Entered Service Tax Refund No.")
                    ssTab.SelectedIndex = 1
                    txtServNo.Focus()
                    FieldsVerification = False
                    Exit Function
                End If
            End If
        End If
        If lblBookType.Text = ConJournal Then
            If ADDMode = True Then
                If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
                    If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPANNo = MasterNo
                    Else
                        mPANNo = ""
                    End If
                    If Trim(mPANNo) = "" Then
                        MsgBox("PAN NO is not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
                        FieldsVerification = False
                        Exit Function
                    End If
                End If
            End If
        End If
        If ADDMode = True Then
            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Or chkESI.CheckState = System.Windows.Forms.CheckState.Checked Or ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
                If CheckAdvanceTdsDeduct() = True Then
                    If MsgQuestion("TDS Amount is deduct in Advance. Are you want to process") = CStr(MsgBoxResult.No) Then
                        FieldsVerification = False
                        Exit Function
                    End If
                End If
            End If
        End If
        If lblBookType.Text = ConJournal Then
            For ii = 1 To SprdMain.MaxRows - 1
                mHeadType = ""
                SprdMain.Row = ii
                SprdMain.Col = ColAccountName
                mServiceGL = Trim(SprdMain.Text)
                If MainClass.ValidateWithMasterTable(Trim(mServiceGL), "SUPP_CUST_NAME", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mHeadType = Trim(MasterNo)
                    If mHeadType = "4" Then
                        Exit For
                    End If
                End If
            Next
            If mHeadType = "4" Then
                If Trim(txtServProvided.Text) = "" Then
                    MsgBox("Please Select The Service., So cann't be Saved.", MsgBoxStyle.Information)
                    FieldsVerification = False
                    Exit Function
                End If
                If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Service Provided is not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
                    FieldsVerification = False
                    Exit Function
                End If
                If Val(txtServiceTaxPer.Text) = 0 Then
                    MsgBox("Please Enter the Service Tax Per, So cann't be Saved.", MsgBoxStyle.Information)
                    FieldsVerification = False
                    Exit Function
                End If
                If Val(txtProviderPer.Text) + Val(txtRecipientPer.Text) <> 100 Then
                    MsgBox("Provider & Recipient Service Percent is not Equal to 100, So cann't be Saved.", MsgBoxStyle.Information)
                    FieldsVerification = False
                    Exit Function
                End If
            End If
        End If
        If MainClass.ValidDataInGrid(SprdMain, ColDivisionCode, "S", "Division Is Blank.") = False Then FieldsVerification = False : Exit Function
        FieldsVerification = True
        Exit Function
ERR1:
        '    Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FieldsVerification = False
    End Function
    Private Function CheckAdvanceTdsDeduct() As Boolean
        On Error GoTo ERR1
        Dim mAcctCode As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow
            SprdMain.Col = ColAccountName
            If MainClass.ValidateWithMasterTable((SprdMain.Text), "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAcctCode = MasterNo
            End If
            SqlStr = "SELECT DISTINCT BILLDATE" & vbCrLf & " FROM FIN_POSTED_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR>=" & RsCompany.Fields("FYEAR").Value - 1 & " AND FYEAR<=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ACCOUNTCODE='" & mAcctCode & "'" & vbCrLf & " AND TRNTYPE IN ('A','O') AND DC='D'" & vbCrLf & " AND MKEY IN (" & vbCrLf & " SELECT MKEY FROM FIN_POSTED_TRN A, FIN_SUPP_CUST_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf & " AND A.ACCOUNTCODE=B.SUPP_CUST_CODE" & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.FYEAR>=" & RsCompany.Fields("FYEAR").Value - 1 & " AND A.FYEAR<=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND B.HEADTYPE='T' AND A.DC='C'" & vbCrLf & " )"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                CheckAdvanceTdsDeduct = True
                Exit Function
            End If
        Next
        Exit Function
ERR1:
        '    Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckAdvanceTdsDeduct = False
    End Function
    Private Function GetExpVNo(ByRef pExpBillNo As Double, ByRef pVNo As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetExpVNo = False
        SqlStr = " SELECT VNO " & vbCrLf & "  FROM FIN_VOUCHER_HDR IH" & vbCrLf & "  WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & "  AND IH.EXP_BILL_NO=" & Val(CStr(pExpBillNo)) & ""
        If MODIFYMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND MKEY<>" & RsTRNMain.Fields("MKEY").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            pVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
            GetExpVNo = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetExpVNo = False
    End Function
    Private Function CheckLastestVDate(ByRef mVDate As Date, ByRef mVType As String) As Boolean
        On Error GoTo CheckLastestVDateErr
        Dim SqlStr As String = ""
        Dim RsCheck As ADODB.Recordset = Nothing '' ADODB.Recordset										
        Dim mBookSubType As String = ""
        Dim mBookType As String = ""

        If chkChqDeposit.CheckState = System.Windows.Forms.CheckState.Checked Then
            Call GetNewBook(mBookType, mBookSubType, mVType)
        Else
            mBookType = VB.Left(lblBookType.Text, 1)
            mBookSubType = VB.Right(lblBookType.Text, 1)
        End If

        CheckLastestVDate = True
        SqlStr = "SELECT VDATE FROM FIN_VOUCHER_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf _
            & " BookType='" & mBookType & "' AND " & vbCrLf _
            & " BookSubType='" & mBookSubType & "' AND " & vbCrLf _
            & " VTYPE='" & MainClass.AllowSingleQuote(Trim(mVType)) & "' AND " & vbCrLf _
            & " VDATE>TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheck, ADODB.LockTypeEnum.adLockReadOnly)
        If RsCheck.EOF = False Then
            CheckLastestVDate = False
        End If
        Exit Function
CheckLastestVDateErr:
        CheckLastestVDate = False
    End Function
    Private Function CheckLastestVNo(ByRef mVNo As Integer, ByRef mVDate As Date, ByRef mVType As String) As Boolean
        On Error GoTo CheckLastestVNoErr
        Dim SqlStr As String = ""
        Dim RsCheckPVNo As ADODB.Recordset = Nothing '' ADODB.Recordset										
        Dim RsCheckLVNo As ADODB.Recordset = Nothing ''ADODB.Recordset										
        Dim mBookSubType As String
        Dim mBookType As String
        CheckLastestVNo = True
        If chkChqDeposit.CheckState = System.Windows.Forms.CheckState.Checked Then
            Exit Function
        Else
            mBookType = VB.Left(lblBookType.Text, 1)
            mBookSubType = VB.Right(lblBookType.Text, 1)
        End If
        ''Checl Previous VNO...										
        SqlStr = "SELECT Max(VNOSeq) AS VNoSeq FROM FIN_VOUCHER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " BookType='" & mBookType & "' AND " & vbCrLf & " BookSubType='" & mBookSubType & "' AND " & vbCrLf & " VTYPE='" & MainClass.AllowSingleQuote(Trim(mVType)) & "' AND " & vbCrLf & " VDATE<TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheckPVNo, ADODB.LockTypeEnum.adLockReadOnly)

        ''Checl Later VNO...										
        SqlStr = "SELECT Max(VNOSeq) AS VNoSeq FROM FIN_VOUCHER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " BookType='" & mBookType & "' AND " & vbCrLf & " BookSubType='" & mBookSubType & "' AND " & vbCrLf & " VTYPE='" & MainClass.AllowSingleQuote(Trim(mVType)) & "' AND " & vbCrLf & " VDATE>TO_DATE('" & VB6.Format(mVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheckLVNo, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCheckPVNo.EOF = False Then
            If Val(IIf(IsDBNull(RsCheckPVNo.Fields("VNoSeq").Value), 1, RsCheckPVNo.Fields("VNoSeq").Value)) > Val(CStr(mVNo)) Then
                CheckLastestVNo = False
            End If
        End If

        If RsCheckLVNo.EOF = False Then
            If Val(IIf(IsDBNull(RsCheckPVNo.Fields("VNoSeq").Value), 1, RsCheckPVNo.Fields("VNoSeq").Value)) < Val(CStr(mVNo)) Then
                CheckLastestVNo = False
            End If
        End If
        Exit Function
CheckLastestVNoErr:
        CheckLastestVNo = False
    End Function
    Private Function PayDetailExists(ByRef nAccountCode As String, ByRef mTRNRowNo As Integer, ByRef mAmount As Double, ByRef mDC As String) As Boolean
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset										
        SqlStr = " SELECT AccountCode, SUM(AMOUNT * DECODE(DC,'D',1,-1)) AS Amount FROM  FIN_TEMPBILL_TRN  " & vbCrLf & " WHERE AccountCode='" & nAccountCode & "' " & vbCrLf & " AND BOOKTYPE = '" & lblBookType.Text & "'" & vbCrLf & " AND TRNDTLSUBROWNO=" & mTRNRowNo & " " & vbCrLf & " AND USERID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " GROUP BY AccountCode"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If Val(RsTemp.Fields("Amount").Value) = mAmount * IIf(VB.Left(mDC, 1) = "D", 1, -1) Then
                PayDetailExists = True
            Else
                PayDetailExists = False
            End If
        Else
            PayDetailExists = False
        End If
    End Function
    Private Function CheckAdvanceExists(ByRef nAccountCode As String, ByRef mTRNRowNo As Integer) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset	

        CheckAdvanceExists = False
        Exit Function

        If lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCReceipt Or lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
        Else
            CheckAdvanceExists = False
            Exit Function
        End If
        SqlStr = " SELECT DISTINCT TRNTYPE FROM  FIN_TEMPBILL_TRN  " & vbCrLf & " WHERE AccountCode='" & nAccountCode & "' " & vbCrLf & " AND BOOKTYPE = '" & lblBookType.Text & "'" & vbCrLf & " AND TRNDTLSUBROWNO=" & mTRNRowNo & " " & vbCrLf & " AND USERID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " AND TRNTYPE IN ('A','N')"
        If lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConPDCReceipt Then
            SqlStr = SqlStr & vbCrLf & " AND DC='C'"
        ElseIf lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment Then
            SqlStr = SqlStr & vbCrLf & " AND DC='D'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            CheckAdvanceExists = True
        Else
            CheckAdvanceExists = False
        End If
        Exit Function
ErrPart:
        CheckAdvanceExists = True
    End Function
    Private Function ServiceTaxDetailExists(ByRef mAmount As Double) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset										
        Dim mServiceTaxAmount As Double
        SqlStr = " SELECT SUM(SERVICETAX_AMT) AS AMOUNT " & vbCrLf & " FROM  FIN_TEMP_SERVICE_TRN  " & vbCrLf & " WHERE USERID='" & PubUserID & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mServiceTaxAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
            If mServiceTaxAmount = mAmount Then
                ServiceTaxDetailExists = True
            Else
                ServiceTaxDetailExists = False
            End If
        Else
            ServiceTaxDetailExists = False
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function LoanDetailExists(ByRef nEmpCode As String, ByRef mAmount As Double) As Boolean
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset										
        SqlStr = " SELECT Emp_Code,LoanAmount AS Amount " & vbCrLf & " FROM TEMP_PAY_LOAN_MST   " & vbCrLf & " WHERE Emp_Code='" & nEmpCode & "' " & vbCrLf & " AND USERID='" & PubUserID & "'" & vbCrLf & " ORDER BY Emp_Code"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If Val(RsTemp.Fields("Amount").Value) = mAmount Then
                LoanDetailExists = True
            Else
                LoanDetailExists = False
            End If
        Else
            LoanDetailExists = False
        End If
    End Function
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim mBookCode As String = ""
        Dim SqlStr As String = ""
        Dim mAccountCode As String
        Dim mDrCr As String
        Dim mVAmount As Double
        Dim mVnoStr As String
        Dim mVType As String = ""
        Dim mVNoPrefix As String
        Dim mVNoSuffix As String
        Dim mBookType As String = ""
        Dim mBookSubType As String = ""
        Dim mVNo As String
        Dim mCancelled As String
        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mIsSuppBill As String
        Dim mIsCapital As String
        Dim mExpPartyCode As String = ""
        Dim mImpPartyCode As String = ""
        Dim mExpDate As String
        Dim mISMODVAT As String
        Dim mIsPLA As String
        Dim mIsSTClaim As String
        Dim mIsServtaxClaim As String
        Dim mIsServTaxRefund As String
        Dim mNoOfEMI As String
        Dim mTotalNoOfEMI As Integer
        Dim I As Integer
        Dim mVDate As String
        Dim mPLFlag As String
        Dim cntRow As Integer
        Dim xSqlStr As String
        Dim RSDiv As ADODB.Recordset = Nothing
        Dim mDC As String = ""
        Dim mDivCode As Double
        Dim mChkDivCode As Double
        Dim mSubRowNo As Integer
        Dim mSuppCustName As String
        Dim mSuppCustAmount As Double
        Dim mPRowNo As Integer
        Dim mServiceCode As Double
        Dim mReverseChargeApp As String
        Dim mLocCode As String
        Dim pSectionCode As Long
        Dim mClearDate As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        txtNarration.Text = Trim(Replace(txtNarration.Text, vbCrLf, ""))
        If chkChqDeposit.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtVno.Text = ""
            Call GetNewBook(mBookType, mBookSubType, mVType)
        Else
            mBookType = VB.Left(lblBookType.Text, 1)
            mBookSubType = VB.Right(lblBookType.Text, 1)
            mVType = MainClass.AllowSingleQuote(Trim(txtVType.Text))
        End If
        If Trim(txtExpDate.Text) = "" Or Not IsDate(txtExpDate.Text) Then
            txtExpDate.Text = VB6.Format(TxtVDate.Text, "DD/MM/YYYY")
            mExpDate = VB6.Format(txtExpDate.Text, "DD/MM/YYYY")
        Else
            mExpDate = VB6.Format(txtExpDate.Text, "DD/MM/YYYY")
        End If
        If txtVno.Text = "" Then
            mVNo = GenVno()
        Else
            mVNo = txtVno.Text
        End If
        mVNoPrefix = MainClass.AllowSingleQuote(Trim(txtVNo1.Text))
        mVNoSuffix = MainClass.AllowSingleQuote(Trim(txtVNoSuffix.Text))
        mVnoStr = mVType & mVNoPrefix & mVNo & mVNoSuffix
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPLFlag = IIf(chkPnL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsSuppBill = IIf(chkSuppBill.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsCapital = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        ''mISMODVAT & "','" & mIsPLA & "','" & mIsSTClaim & "','" & mIsServtaxClaim & "','" & mIsServTaxRefund & "'										
        mISMODVAT = IIf(chkModvat.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsPLA = IIf(chkPLA.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsSTClaim = IIf(chkSTClaim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsServtaxClaim = IIf(chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsServTaxRefund = IIf(chkServTaxRefund.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        Select Case lblBookType.Text
            Case ConJournal
                mBookCode = CStr(ConJournalBookCode)
            Case ConContra
                mBookCode = CStr(ConContraBookCode)
            Case Else
                If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mBookCode = MasterNo
                End If
        End Select
        If MainClass.ValidateWithMasterTable(txtExpPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mExpPartyCode = MasterNo
        End If
        If MainClass.ValidateWithMasterTable(txtImpPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mImpPartyCode = MasterNo
        End If
        mISTDSDEDUCT = IIf(chkTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISESIDEDUCT = IIf(chkESI.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISSTDSDEDUCT = IIf(ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mReverseChargeApp = IIf(chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mServiceCode = Val(MasterNo)
        Else
            mServiceCode = -1
        End If

        pSectionCode = -1

        If Trim(txtTDSSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtTDSSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSectionCode = MasterNo
            End If
        End If ''


        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_VOUCHER_HDR", CurMKey, RsTRNMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_VOUCHER_DET", CurMKey, RsTRNDetail, "MKEY", "M") = False Then GoTo ErrPart
        End If

        If ADDMode = True Then
            mRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurMKey = RsCompany.Fields("COMPANY_CODE").Value & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(mRowNo)
            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf _
                & " Mkey, COMPANY_CODE, " & vbCrLf _
                & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf _
                & " Vno, Vdate, BookType,BookSubType, " & vbCrLf _
                & " BookCode, Narration, CANCELLED, " & vbCrLf _
                & " ISTDSDEDUCT,TDSPER,TDSAMOUNT, " & vbCrLf _
                & " ISESIDEDUCT,ESIPER,ESIAMOUNT, ISSTDSDEDUCT,STDSPER,STDSAMOUNT," & vbCrLf _
                & " ISSUPPBILL,MODVATNO ,STREFUNDNO, ISCAPITAL," & vbCrLf _
                & " IMP_SUPP_CUST_CODE, IMP_MRR_NO, " & vbCrLf _
                & " IMP_BILL_NO, IMP_BILL_DATE,  " & vbCrLf & " EXP_SUPP_CUST_CODE, EXP_BILL_NO,  " & vbCrLf _
                & " EXP_BILL_DATE, AUTHORISED, " & vbCrLf _
                & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE, " & vbCrLf _
                & " ISMODVAT, ISPLA, ISSTCLAIM, ISSERVTAXCLAIM, ISSERVTAXREFUND, SERVNO, PL_FLAG, " & vbCrLf _
                & " SERVICE_CODE, SERVICE_ON_AMT, SERVICE_TAX_PER, " & vbCrLf _
                & " SERVICE_TAX_AMOUNT, SERV_PROVIDER_PER, SERV_RECIPIENT_PER,REVERSE_CHARGE_APP, " & vbCrLf _
                & " IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY, SECTION_CODE) VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " '" & CurMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & mRowNo & ", " & vbCrLf _
                & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf _
                & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf _
                & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote(txtNarration.Text.Replace(vbCrLf, "")) & "', '" & mCancelled & "', " & vbCrLf _
                & " '" & mISTDSDEDUCT & "'," & Val(txtJVTDSRate.Text) & ", " & Val(txtJVTDSAmount.Text) & ", " & vbCrLf _
                & " '" & mISESIDEDUCT & "'," & Val(txtESIRate.Text) & ", " & Val(txtESIAmount.Text) & ", " & vbCrLf _
                & " '" & mISSTDSDEDUCT & "'," & Val(txtSTDSRate.Text) & ", " & Val(txtSTDSAmount.Text) & ", " & vbCrLf _
                & " '" & mIsSuppBill & "'," & Val(txtModvatNo.Text) & ", " & Val(txtSTRefundNo.Text) & ", " & vbCrLf _
                & " '" & mIsCapital & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mImpPartyCode) & "', " & IIf(Val(txtImpMRRNo.Text) = 0 Or Trim(txtImpMRRNo.Text) = "", "Null", Val(txtImpMRRNo.Text)) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtImpBillNo.Text) & "', TO_DATE('" & VB6.Format(txtImpBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mExpPartyCode) & "', " & IIf(Val(txtExpBillNo.Text) = 0 Or Trim(txtExpBillNo.Text) = "", "Null", Val(txtExpBillNo.Text)) & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtExpBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'N', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','H', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & mISMODVAT & "','" & mIsPLA & "','" & mIsSTClaim & "','" & mIsServtaxClaim & "'," & vbCrLf _
                & "'" & mIsServTaxRefund & "'," & Val(txtServNo.Text) & ",'" & mPLFlag & "', " & vbCrLf _
                & " " & IIf(mServiceCode = -1, "NULL", mServiceCode) & ", " & Val(txtServiceOn.Text) & ", " & Val(txtServiceTaxPer.Text) & ", " & vbCrLf _
                & " " & Val(txtServiceTaxAmount.Text) & ", " & Val(txtProviderPer.Text) & ", " & Val(txtRecipientPer.Text) & "," & vbCrLf _
                & " '" & mReverseChargeApp & "', '" & lblReversalMade.Text & "','" & lblReversalVoucher.Text & "','" & lblReversalMkey.Text & "'," & IIf(pSectionCode = -1, "NULL", pSectionCode) & ")"

        ElseIf MODIFYMode = True Then
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET SECTION_CODE=" & IIf(pSectionCode = -1, "NULL", pSectionCode) & "," & vbCrLf _
                & " Vdate=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " VType= '" & mVType & "'," & vbCrLf & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf _
                & " VnoSeq=" & Val(mVNo) & ", " & vbCrLf & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf _
                & " Vno='" & mVnoStr & "', " & vbCrLf _
                & " BookCode='" & mBookCode & "', " & vbCrLf _
                & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text.Replace(vbCrLf, "")) & "', " & vbCrLf _
                & " CANCELLED='" & mCancelled & "', " & vbCrLf _
                & " BookType='" & mBookType & "', " & vbCrLf _
                & " BookSubType='" & mBookSubType & "', " & vbCrLf _
                & " UPDATE_FROM='H'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ISTDSDEDUCT='" & mISTDSDEDUCT & "'," & vbCrLf _
                & " TDSPER=" & Val(txtJVTDSRate.Text) & ", " & vbCrLf _
                & " TDSAMOUNT=" & Val(txtJVTDSAmount.Text) & ", " & vbCrLf _
                & " REVERSE_CHARGE_APP='" & mReverseChargeApp & "', "

            SqlStr = SqlStr & vbCrLf _
                & " ISESIDEDUCT='" & mISESIDEDUCT & "'," & vbCrLf _
                & " ESIPER=" & Val(txtESIRate.Text) & ", " & vbCrLf _
                & " ESIAMOUNT=" & Val(txtESIAmount.Text) & ", " & vbCrLf _
                & " ISSTDSDEDUCT='" & mISSTDSDEDUCT & "'," & vbCrLf _
                & " STDSPER=" & Val(txtSTDSRate.Text) & ", " & vbCrLf _
                & " STDSAMOUNT=" & Val(txtSTDSAmount.Text) & ", " & vbCrLf _
                & " ISSUPPBILL='" & mIsSuppBill & "', ISCAPITAL='" & mIsCapital & "'," & vbCrLf & " MODVATNO=" & Val(txtModvatNo.Text) & ",  " & vbCrLf _
                & " STREFUNDNO=" & Val(txtSTRefundNo.Text) & ", " & vbCrLf _
                & " IMP_SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mImpPartyCode) & "'," & vbCrLf _
                & " IMP_MRR_NO=" & IIf(Val(txtImpMRRNo.Text) = 0 Or Trim(txtImpMRRNo.Text) = "", "Null", Val(txtImpMRRNo.Text)) & "," & vbCrLf _
                & " IMP_BILL_NO='" & MainClass.AllowSingleQuote(txtImpBillNo.Text) & "'," & vbCrLf _
                & " IMP_BILL_DATE=TO_DATE('" & VB6.Format(txtImpBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " EXP_SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mExpPartyCode) & "'," & vbCrLf _
                & " EXP_BILL_NO=" & IIf(Val(txtExpBillNo.Text) = 0 Or Trim(txtExpBillNo.Text) = "", "Null", Val(txtExpBillNo.Text)) & "," & vbCrLf _
                & " EXP_BILL_DATE=TO_DATE('" & VB6.Format(txtExpBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " EXPDATE=TO_DATE('" & VB6.Format(mExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), PL_FLAG='" & mPLFlag & "',"

            SqlStr = SqlStr & vbCrLf _
                & " ISMODVAT='" & mISMODVAT & "'," & vbCrLf _
                & " ISPLA='" & mIsPLA & "'," & vbCrLf _
                & " ISSTCLAIM='" & mIsSTClaim & "'," & vbCrLf _
                & " ISSERVTAXCLAIM='" & mIsServtaxClaim & "'," & vbCrLf _
                & " ISSERVTAXREFUND='" & mIsServTaxRefund & "'," & vbCrLf _
                & " SERVNO=" & Val(txtServNo.Text) & ","

            SqlStr = SqlStr & vbCrLf _
                & " SERVICE_CODE=" & IIf(mServiceCode = -1, "NULL", mServiceCode) & "," & vbCrLf _
                & " SERVICE_ON_AMT=" & Val(txtServiceOn.Text) & "," & vbCrLf _
                & " SERVICE_TAX_PER=" & Val(txtServiceTaxPer.Text) & "," & vbCrLf _
                & " SERVICE_TAX_AMOUNT=" & Val(txtServiceTaxAmount.Text) & "," & vbCrLf _
                & " SERV_PROVIDER_PER=" & Val(txtProviderPer.Text) & "," & vbCrLf _
                & " SERV_RECIPIENT_PER=" & Val(txtRecipientPer.Text) & ", " & vbCrLf _
                & " IS_REVERSAL_MADE='" & lblReversalMade.Text & "', IS_REVERSAL_VOUCHER='" & lblReversalVoucher.Text & "', REVERSAL_MKEY='" & lblReversalMkey.Text & "'"

            SqlStr = SqlStr & vbCrLf _
                & " Where Mkey='" & CurMKey & "'"
        End If
        PubDBCn.Execute(SqlStr)

        If UpdateDetail(CurMKey, mRowNo, mBookCode, mVType, mVnoStr, (TxtVDate.Text), (txtNarration.Text), PubDBCn) = False Then GoTo ErrPart
        If lblBookType.Text = ConJournal Or lblBookType.Text = ConContra Then
        Else
            xSqlStr = "SELECT DIV_CODE FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSDiv, ADODB.LockTypeEnum.adLockReadOnly)
            If RSDiv.EOF = False Then
                mSubRowNo = -1
                Do While RSDiv.EOF = False
                    mVAmount = 0
                    mDivCode = IIf(IsDBNull(RSDiv.Fields("DIV_CODE").Value), -1, RSDiv.Fields("DIV_CODE").Value)
                    For cntRow = 1 To SprdMain.MaxRows - 1
                        SprdMain.Row = cntRow
                        SprdMain.Col = ColClearDate
                        mClearDate = SprdMain.Text

                        SprdMain.Col = ColAccountName
                        mSuppCustName = MainClass.AllowSingleQuote(SprdMain.Text)
                        SprdMain.Col = ColPRRowNo
                        mPRowNo = Val(SprdMain.Text)
                        If GetAccountBalancingMethod(mSuppCustName, False) = "D" Then
                            If GetBillDetailAmount(mPRowNo, mSuppCustName, mDivCode, mDC, mSuppCustAmount) = True Then
                                mVAmount = mVAmount + (mSuppCustAmount * IIf(UCase(mDC) = "D", 1, -1))
                                mDC = IIf(mDC = "D", "CR", "DR") ''Book Code Update										
                            Else
                                mDC = "DR"
                            End If
                        Else
                            SprdMain.Col = ColDivisionCode
                            mChkDivCode = Val(SprdMain.Text)
                            If mDivCode = mChkDivCode Then
                                SprdMain.Col = ColDC
                                mDC = Trim(SprdMain.Text)
                                SprdMain.Col = ColAmount
                                mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "DR", 1, -1))
                                If chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                                    SprdMain.Col = ColCGSTAmount
                                    mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "DR", 1, -1))
                                    SprdMain.Col = ColSGSTAmount
                                    mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "DR", 1, -1))
                                    SprdMain.Col = ColIGSTAmount
                                    mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "DR", 1, -1))
                                End If
                            End If
                        End If
                    Next
                    mDrCr = IIf(mVAmount > 0, "C", "D")
                    mVAmount = Val(CStr(System.Math.Abs(mVAmount)))
                    If mCancelled = "Y" Then
                        mVAmount = 0
                    End If
                    If mVAmount <> 0 Then
                        mLocCode = GetDefaultLocation(mBookCode)
                        If UpdateTRN(PubDBCn, CurMKey, mRowNo, mSubRowNo, mBookCode, mVType, mBookType, mBookSubType, mBookCode, mVnoStr, (TxtVDate.Text), mVnoStr,
                                     (TxtVDate.Text), mVAmount, mDrCr, "P", "", "", CStr(-1), CStr(-1), CStr(-1), CStr(-1), "", "", "P", mClearDate, "", (txtNarration.Text), "",
                                     mExpDate, ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivCode, mLocCode,
                                     IIf(chkPLA.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrPart
                        mSubRowNo = mSubRowNo - 1
                    End If
                    RSDiv.MoveNext()
                Loop
            Else
                mVAmount = Val(CStr(CDbl(LblNetAmt.Text)))
                mDrCr = IIf(Val(CStr(CDbl(LblDrAmt.Text))) >= Val(CStr(CDbl(LblCrAmt.Text))), "C", "D")
                If mCancelled = "Y" Then
                    mVAmount = 0
                End If
                mLocCode = GetDefaultLocation(mBookCode)
                If UpdateTRN(PubDBCn, CurMKey, mRowNo, -1, mBookCode, mVType, mBookType, mBookSubType, mBookCode, mVnoStr, (TxtVDate.Text), mVnoStr, (TxtVDate.Text), mVAmount, mDrCr, "P", "", "", CStr(-1), CStr(-1), CStr(-1), CStr(-1), "", "", "P", "", "", (txtNarration.Text), "", mExpDate, ADDMode, (lblAddUser.Text), (lblAddDate.Text), 1, mLocCode, IIf(chkPLA.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrPart
            End If
        End If
        If lblSR.Text <> "" Then 'Update Salary  for the current Month..										
            If UpdateSalVoucher(PubDBCn, CurMKey, VB.Left(lblSR.Text, 1), Mid(lblSR.Text, 2, 1), mVNo, CDbl(Mid(lblSR.Text, 3))) = False Then GoTo ErrPart
            If VB.Left(lblSR.Text, 1) = "F" Then
                If UpdateFullNFinal(IIf(mCancelled = "Y", "N", "Y")) = False Then GoTo ErrPart
            ElseIf VB.Left(lblSR.Text, 1) = "L" Then
                If UpdateLTA(IIf(mCancelled = "Y", "N", "Y")) = False Then GoTo ErrPart
            ElseIf VB.Left(lblSR.Text, 1) = "Q" Then
                If UpdateVoucherSalary(IIf(mCancelled = "Y", "N", "Y"), mVnoStr) = False Then GoTo ErrPart
            End If
        End If
        '    If mCancelled = "N" Then										
        If mBookType <> VB.Left(ConPDCPayment, 1) Then
            If UpdateTDSDetail(PubDBCn, CurMKey, mVnoStr, mBookType, mBookSubType, mCancelled) = False Then GoTo ErrPart
        End If
        '    End If										
        '    If chkSuppBill.Value = vbChecked Or chkCapital.Value = vbChecked Or Val(txtModvatNo.Text) <> 0 Or Val(txtSTRefundNo.Text) <> 0 Then										
        If lblBookType.Text = ConJournal Then
            If ClearJVNo(mVnoStr) = False Then GoTo ErrPart
            If UpDateSuppBill(mVnoStr) = False Then GoTo ErrPart
        End If
        '    PubDBCn.CommitTrans										
        txtVno.Text = mVNo
        '    Update1 = True										
        If ADDMode = True Then
            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Or chkESI.CheckState = System.Windows.Forms.CheckState.Checked Or ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
                If UpdateTDSVoucher() = False Then
                    GoTo ErrPart ''Exit Function										
                End If
                SqlStr = "UPDATE FIN_VOUCHER_HDR SET JVNO='" & txtJVVNO.Text & "' WHERE MKEY='" & CurMKey & "'"
                PubDBCn.Execute(SqlStr)
            End If
        End If
        If Trim(txtJVVNO.Text) <> "" And ADDMode = True Then
            MsgBox("TDS Journal Voucher No. " & txtJVVNO.Text & " Created. ", MsgBoxStyle.Information)
        End If
        If lblBookType.Text = ConPDCPayment And ADDMode = True Then
            If MsgQuestion("Are you Entering EMI PDC Cheque.") = CStr(MsgBoxResult.Yes) Then
                mNoOfEMI = InputBox("Please Enter total No of EMI", "Total EMI")
                mTotalNoOfEMI = Val(mNoOfEMI)
                If Val(mNoOfEMI) > 0 Then
                    mVDate = VB6.Format(TxtVDate.Text, "DD/MM/YYYY")
                    For I = 1 To CInt(mNoOfEMI)
                        mVDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(mVDate)))
                        If UpdateEMIVoucher(mBookType, mBookSubType, mVType, mVDate, mBookCode, mExpPartyCode, mImpPartyCode, I, mTotalNoOfEMI) = False Then GoTo ErrPart
                    Next
                End If
            End If
        End If
        PubDBCn.CommitTrans()
        txtVno.Text = mVNo
        Update1 = True
        Exit Function
ErrPart:
        '    Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''										
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateEMIVoucher(ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVType As String, ByRef pVDate As String, ByRef pBookCode As String, ByRef pExpPartyCode As String, ByRef pImpPartyCode As String, ByRef pNoOfEMI As Integer, ByRef mTotalNoOfEMI As Integer) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDrCr As String
        Dim mVAmount As Double
        '										
        Dim mVnoStr As String
        Dim mVNoPrefix As String
        Dim mVNoSuffix As String
        Dim mVNo As String
        Dim mCancelled As String
        '										
        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mIsSuppBill As String
        Dim mIsCapital As String
        Dim mExpDate As String
        '										
        Dim mISMODVAT As String
        Dim mIsPLA As String
        Dim mIsSTClaim As String
        Dim mIsServtaxClaim As String
        Dim mIsServTaxRefund As String
        Dim pNarration As String
        Dim cntRow As Integer
        Dim xSqlStr As String
        Dim RSDiv As ADODB.Recordset = Nothing
        Dim mDC As String
        Dim mDivCode As Double
        Dim mChkDivCode As Double
        Dim mSubRowNo As Integer
        Dim mLocCode As String
        mVNo = GenEMIVno(pBookType, pBookSubType, pVType)
        mExpDate = pVDate
        mVNoPrefix = MainClass.AllowSingleQuote(Trim(txtVNo1.Text))
        mVNoSuffix = MainClass.AllowSingleQuote(Trim(txtVNoSuffix.Text))
        mVnoStr = pVType & mVNoPrefix & mVNo & mVNoSuffix
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsSuppBill = IIf(chkSuppBill.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsCapital = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISMODVAT = IIf(chkModvat.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsPLA = IIf(chkPLA.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsSTClaim = IIf(chkSTClaim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsServtaxClaim = IIf(chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsServTaxRefund = IIf(chkServTaxRefund.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISTDSDEDUCT = IIf(chkTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISESIDEDUCT = IIf(chkESI.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISSTDSDEDUCT = IIf(ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        Dim pCurMKey As String
        Dim pRowNo As Integer
        pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
        pCurMKey = RsCompany.Fields("COMPANY_CODE").Value & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
        pNarration = "INSTALLATION NO. : " & pNoOfEMI + 1 & "/" & mTotalNoOfEMI
        SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " ISTDSDEDUCT,TDSPER,TDSAMOUNT, " & vbCrLf & " ISESIDEDUCT,ESIPER,ESIAMOUNT, ISSTDSDEDUCT,STDSPER,STDSAMOUNT," & vbCrLf & " ISSUPPBILL,MODVATNO ,STREFUNDNO, ISCAPITAL," & vbCrLf & " IMP_SUPP_CUST_CODE, IMP_MRR_NO, " & vbCrLf & " IMP_BILL_NO, IMP_BILL_DATE,  " & vbCrLf & " EXP_SUPP_CUST_CODE, EXP_BILL_NO,  " & vbCrLf & " EXP_BILL_DATE, AUTHORISED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE, " & vbCrLf & " ISMODVAT, ISPLA, ISSTCLAIM, ISSERVTAXCLAIM, ISSERVTAXREFUND, SERVNO,IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY " & vbCrLf & " ) VALUES ( "
        SqlStr = SqlStr & vbCrLf & " '" & pCurMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & pRowNo & ", " & vbCrLf & " '" & pVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf & " TO_DATE('" & VB6.Format(pVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & pBookType & "', '" & pBookSubType & "', " & vbCrLf & " '" & pBookCode & "', '" & MainClass.AllowSingleQuote(pNarration) & "', '" & mCancelled & "', " & vbCrLf & " '" & mISTDSDEDUCT & "'," & Val(txtJVTDSRate.Text) & ", " & Val(txtJVTDSAmount.Text) & ", " & vbCrLf & " '" & mISESIDEDUCT & "'," & Val(txtESIRate.Text) & ", " & Val(txtESIAmount.Text) & ", " & vbCrLf & " '" & mISSTDSDEDUCT & "'," & Val(txtSTDSRate.Text) & ", " & Val(txtSTDSAmount.Text) & ", " & vbCrLf & " '" & mIsSuppBill & "',0, 0, " & vbCrLf & " '" & mIsCapital & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(pImpPartyCode) & "', ''," & vbCrLf & " '', ''," & vbCrLf & " '', ''," & vbCrLf & " '', 'N', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','H', " & vbCrLf & " TO_DATE('" & VB6.Format(mExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mISMODVAT & "','" & mIsPLA & "','" & mIsSTClaim & "','" & mIsServtaxClaim & "','" & mIsServTaxRefund & "'," & Val(txtServNo.Text) & ",'N','N','')"
        PubDBCn.Execute(SqlStr)
        If UpdateEMIDetail(pCurMKey, pRowNo, pBookCode, pVType, mVnoStr, pVDate, pNarration, pNoOfEMI, mTotalNoOfEMI, PubDBCn) = False Then GoTo ErrPart
        If lblBookType.Text = ConJournal Or lblBookType.Text = ConContra Then
        Else
            xSqlStr = "SELECT DIV_CODE FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSDiv, ADODB.LockTypeEnum.adLockReadOnly)
            If RSDiv.EOF = False Then
                Do While RSDiv.EOF
                    mVAmount = 0
                    mDivCode = IIf(IsDBNull(RSDiv.Fields("DIV_CODE").Value), -1, RSDiv.Fields("DIV_CODE").Value)
                    mSubRowNo = -1
                    For cntRow = 1 To SprdMain.MaxRows - 1
                        SprdMain.Row = cntRow
                        SprdMain.Col = ColDivisionCode
                        mChkDivCode = Val(SprdMain.Text)
                        If mDivCode = mChkDivCode Then
                            SprdMain.Col = ColDC
                            mDC = Trim(SprdMain.Text)
                            SprdMain.Col = ColAmount
                            mVAmount = mVAmount + (Val(SprdMain.Text) * IIf(UCase(mDC) = "DR", 1, -1))
                        End If
                    Next
                    mDrCr = IIf(mVAmount < 0, "C", "D")
                    mVAmount = Val(CStr(System.Math.Abs(mVAmount)))
                    If mCancelled = "Y" Then
                        mVAmount = 0
                    End If
                    If mVAmount <> 0 Then
                        mLocCode = GetDefaultLocation(pBookCode)
                        If UpdateTRN(PubDBCn, pCurMKey, pRowNo, mSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pBookCode, mVnoStr, pVDate, mVnoStr, pVDate, mVAmount, mDrCr, "P", "", "", CStr(-1), CStr(-1), CStr(-1), CStr(-1), "", "", "P", "", "", pNarration, "", mExpDate, ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivCode, mLocCode) = False Then GoTo ErrPart
                        mSubRowNo = mSubRowNo - 1
                    End If
                    RSDiv.MoveNext()
                Loop
            Else
                mVAmount = Val(CStr(CDbl(LblNetAmt.Text)))
                mDrCr = IIf(Val(CStr(CDbl(LblDrAmt.Text))) >= Val(CStr(CDbl(LblCrAmt.Text))), "C", "D")
                If mCancelled = "Y" Then
                    mVAmount = 0
                End If
                mLocCode = GetDefaultLocation(pBookCode)
                If UpdateTRN(PubDBCn, pCurMKey, pRowNo, -1, pBookCode, pVType, pBookType, pBookSubType, pBookCode, mVnoStr, pVDate, mVnoStr, pVDate, mVAmount, mDrCr, "P", "", "", CStr(-1), CStr(-1), CStr(-1), CStr(-1), "", "", "P", "", "", pNarration, "", mExpDate, ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivCode, mLocCode) = False Then GoTo ErrPart
            End If
        End If
        UpdateEMIVoucher = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateEMIVoucher = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateVoucherSalary(ByRef pAcPosting As String, Optional ByRef xVNo As String = "") As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = " UPDATE PAY_SALVOUCHER_TRN SET APP_STATUS = '" & pAcPosting & "', "
        If pAcPosting = "N" Then
            SqlStr = SqlStr & " VNO='', VDATE=''"
        Else
            SqlStr = SqlStr & " VNO='" & xVNo & "', VDATE=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(lblEmpCode.Text, "000000") & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(TxtVDate.Text, "YYYYMM") & "'"
        PubDBCn.Execute(SqlStr)
        UpdateVoucherSalary = True
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        UpdateVoucherSalary = False
    End Function
    Private Function GetBillDetailAmount(ByRef mPRowNo As Integer, ByRef mSuppCustName As String, ByRef mDivCode As Double, ByRef mDC As String, ByRef mSuppCustAmount As Double) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xAcctCode As String
        If MainClass.ValidateWithMasterTable(mSuppCustName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = CStr(-1)
        End If
        mDC = "D"
        mSuppCustAmount = 0
        GetBillDetailAmount = False
        SqlStr = "SELECT SUM(AMOUNT * DECODE(DC,'D',1,-1)) AS AMOUNT FROM FIN_TEMPBILL_TRN  Where UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'" & vbCrLf & " AND TRNDTLSUBROWNO=" & Val(CStr(mPRowNo)) & "" & vbCrLf & " AND BOOKTYPE='" & UCase(Trim(lblBookType.Text)) & "' AND DIV_CODE= " & mDivCode & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            mSuppCustAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
            mDC = IIf(mSuppCustAmount >= 0, "D", "C")
            mSuppCustAmount = System.Math.Abs(mSuppCustAmount)
        End If
        GetBillDetailAmount = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        '    Resume										
    End Function
    Private Function UpdateSalVoucher(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mVNo As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrPart
        Dim mYM As Integer
        Dim SqlStr As String = ""
        Dim mBankCode As String
        Dim pVNo As String
        SqlStr = " DELETE FROM FIN_SalVoucher_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND mKey=" & pMKey & " "
        If Val(lblELYear.Text) <> 0 Then
            SqlStr = SqlStr & " AND EL_YEAR = " & Val(lblELYear.Text) & ""
            mYM = Val(lblELYear.Text)
        Else
            mYM = CInt(VB6.Format(TxtVDate.Text, "YYYYMM"))
        End If
        pDBCn.Execute(SqlStr)
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            UpdateSalVoucher = True
            Exit Function
        End If
        SqlStr = ""
        mBankCode = CStr(Val(lblEmpCode.Text))
        mBankCode = IIf(CDbl(mBankCode) > 0, mBankCode, "-1")
        pVNo = txtVType.Text & VB6.Format(Val(mVNo), "00000") & txtVNoSuffix.Text
        SqlStr = " INSERT INTO FIN_SalVoucher_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, MKEY, " & vbCrLf & " YM, VDATE, VNOPREFIX, " & vbCrLf & " VNOSEQ, VNOSUFFIX, VNO, " & vbCrLf & " BANKCODE, BOOKTYPE, BOOKSUBTYPE,DIV_CODE,EL_YEAR " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", '" & CurMKey & "'," & vbCrLf & " " & mYM & ", TO_Date('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & txtVType.Text & "', " & vbCrLf & " " & Val(mVNo) & ", '" & txtVNoSuffix.Text & "', " & vbCrLf & " '" & pVNo & "', " & mBankCode & ", " & vbCrLf & " '" & pBookType & "','" & pBookSubType & "','" & mDivisionCode & "'," & Val(lblELYear.Text) & ")"
        pDBCn.Execute(SqlStr)
        UpdateSalVoucher = True
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        UpdateSalVoucher = False
    End Function
    Private Function UpdateFullNFinal(ByRef pAcPosting As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = " UPDATE PAY_FFSETTLE_HDR SET AC_POSTING='" & pAcPosting & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(lblEmpCode.Text, "000000") & "'"
        PubDBCn.Execute(SqlStr)
        UpdateFullNFinal = True
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        UpdateFullNFinal = False
    End Function
    Private Function UpdateLTA(ByRef pAcPosting As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mChqNo As String
        Dim mChqDate As String
        SprdMain.Row = 1
        SprdMain.Col = ColChequeNo
        mChqNo = Trim(SprdMain.Text)
        SprdMain.Col = ColChequeDate
        mChqDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")
        SqlStr = " UPDATE PAY_LTA_HDR SET AC_POSTING='" & pAcPosting & "'," & vbCrLf & " CHQ_NO='" & mChqNo & "', CHQ_DATE=TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), BANK_NAME='" & MainClass.AllowSingleQuote((txtPartyName.Text)) & "'" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & lblELYear.Text & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(lblEmpCode.Text, "000000") & "'"
        PubDBCn.Execute(SqlStr)
        UpdateLTA = True
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        UpdateLTA = False
    End Function
    Private Function UpdateTDSVoucher() As Boolean
        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim SqlStr As String = ""
        Dim mAccountCode As String
        Dim mDrCr As String
        Dim mVAmount As Double
        Dim mVnoStr As String
        Dim mVType As String
        Dim mVNoPrefix As String
        Dim mVNoSuffix As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVNo As String
        Dim mCancelled As String
        Dim pRowNo As Integer
        Dim CurJVMKey As String = ""
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)

        mVType = "JVT"
        mVNo = GenJVVno(mVType)
        mVNoPrefix = Trim(txtVNo1.Text)
        mVNoSuffix = ""
        mVnoStr = mVType & mVNoPrefix & mVNo & mVNoSuffix
        txtJVVNO.Text = mVnoStr
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBookCode = CStr(ConJournalBookCode)
        If ADDMode = True Then
            pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurJVMKey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM, EXPDATE,IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY) VALUES ( " & vbCrLf & " '" & CurJVMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & pRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf & " TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote("") & "', '" & mCancelled & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','H', " & vbCrLf & " TO_DATE('" & VB6.Format(txtExpDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'N','N','')"
        ElseIf MODIFYMode = True Then
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf & " Vdate=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EXPDATE=TO_DATE('" & VB6.Format(txtExpDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " VType= '" & mVType & "'," & vbCrLf & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf & " VnoSeq=" & Val(mVNo) & ", " & vbCrLf & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf & " Vno='" & mVnoStr & "', " & vbCrLf & " BookCode='" & mBookCode & "', " & vbCrLf & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf & " CANCELLED='" & mCancelled & "', " & vbCrLf & " BookType='" & mBookType & "', " & vbCrLf & " BookSubType='" & mBookSubType & "', " & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " Where Mkey='" & CurJVMKey & "'"
        End If
        PubDBCn.Execute(SqlStr)
        If UpdateJVDetail(CurJVMKey, pRowNo, mBookCode, mVType, mVnoStr, (TxtVDate.Text), "", PubDBCn) = False Then GoTo ErrPart
        '    If mCancelled = "N" Then										
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If UpdateTDSCreditDetail(CurJVMKey, mVnoStr, mBookType, mBookSubType) = False Then GoTo ErrPart
        End If
        '    End If										
        UpdateTDSVoucher = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateTDSVoucher = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateJVDetail(ByRef mMkey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNo As String, ByRef mVDate As String, ByRef pNarration As String, ByRef pDBCn As ADODB.Connection) As Boolean
        On Error GoTo ErrDetail
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mAccountName As String = ""
        Dim mAccountCode As String = ""
        Dim mChequeNo As String
        Dim mChqDate As String
        Dim mAmount As Double
        Dim mCCCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mExpCode As String
        Dim mIBRNo As String
        Dim mDC As String
        Dim mRemarks As String = ""
        Dim mPRRowNo As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mClearDate As String
        Dim mParticulars As String
        Dim cntRow As Integer
        Dim mDivisionCode As Double

        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        SqlStr = "Delete From FIN_TEMPBILL_TRN Where UserId='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMPMKEY=" & pProcessKey & ""
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        '    mRemarks = " agt Bill No(s) " & txtBillNo.Text & " Dt. " & txtBillDate.Text										
        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf _
            & " MKEY ='" & mMkey & "' " & vbCrLf _
            & " AND BookType='" & mBookType & "'" & vbCrLf _
            & " AND BooksubType='" & mBookSubType & "'"
        pDBCn.Execute(SqlStr)
        '    Call InsertTempBill(mAccountCode, mAmount, mRemarks)										
        '******SUPPLIER ACCOUNT POSTING										
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDC
                If (VB.Left(UCase(.Text), 1) = "C" And ConJournal = lblBookType.Text) Or ConBankPayment = lblBookType.Text Or ConPDCPayment = lblBookType.Text Then
                    .Col = ColAccountName
                    mAccountName = Trim(.Text)
                    .Col = ColDivisionCode
                    mDivisionCode = Val(.Text)
                    Exit For
                End If
            Next
        End With

        If Val(mDivisionCode) <= 0 Then
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColDC
                    .Col = ColDivisionCode
                    mDivisionCode = Val(.Text)
                    Exit For
                Next
            End With
        End If

        If mAccountName <> "" Then
            mPRRowNo = cntRow
            mDC = "D"
            mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)
            mAmount = Val(txtJVTDSAmount.Text) + Val(txtESIAmount.Text) + Val(txtSTDSAmount.Text)
            mParticulars = ""
            If Val(txtJVTDSRate.Text) <> 0 Then
                mParticulars = "TDS DEDUCT ON RS. " & txtTDSDeductOn.Text & " @ " & txtJVTDSRate.Text
            End If
            If Val(txtESIRate.Text) <> 0 Then
                mParticulars = mParticulars & ", " & "ESI DEDUCT ON RS. " & txtESIDeductOn.Text & " @  " & txtESIRate.Text
            End If
            If Val(txtSTDSRate.Text) <> 0 Then
                mParticulars = mParticulars & ", " & "STDS DEDUCT ON RS. " & txtSTDSDeductOn.Text & " @  " & txtSTDSRate.Text
            End If
            mChequeNo = ""
            mChqDate = ""
            mCCCode = "-1"
            mDeptCode = "-1"
            mEmpCode = "-1"
            mExpCode = "-1"
            mIBRNo = "-1"
            mClearDate = ""
            I = cntRow
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & ")"
            PubDBCn.Execute(SqlStr)


            If UpdateSuppPRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, mAmount, mRemarks, mDivisionCode) = False Then GoTo ErrDetail
        End If
        '******TDS ACCOUNT POSTING										
        mPRRowNo = cntRow + 1
        mDC = "C"
        mAccountCode = GetTDSAccountCode(txtTDSSection.Text)       'IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        If Trim(mAccountCode) = "" Then
            MsgInformation("TDS Head Not Defined.")
            UpdateJVDetail = False
            Exit Function
        End If
        mParticulars = "TDS DEDUCT ON RS. " & txtTDSDeductOn.Text & " @ " & txtJVTDSRate.Text
        mAmount = Val(txtJVTDSAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = cntRow + 1
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivisionCode, pProcessKey, IIf(chkPnL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrDetail
        End If
        '******ESI ACCOUNT POSTING										
        mPRRowNo = cntRow + 2
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("ESICREDITACCOUNT").Value), "-1", RsCompany.Fields("ESICREDITACCOUNT").Value)
        mParticulars = "ESI DEDUCT ON RS. " & txtESIDeductOn.Text & " @  " & txtESIRate.Text
        mAmount = Val(txtESIAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = cntRow + 2
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode, EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivisionCode, pProcessKey, IIf(chkPnL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrDetail
        End If
        '******STDS ACCOUNT POSTING										
        mPRRowNo = cntRow + 3
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("STDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("STDSCREDITACCOUNT").Value)
        mParticulars = "STDS DEDUCT ON RS. " & txtSTDSDeductOn.Text & " @  " & txtSTDSRate.Text
        mAmount = Val(txtSTDSAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = cntRow + 3
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivisionCode, pProcessKey, IIf(chkPnL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrDetail
        End If
        UpdateJVDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateJVDetail = False
        ''Resume										
    End Function
    Public Function UpdateSuppPRDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pRowNo As Integer, ByRef pTRNDtlSubRow As Integer, ByRef pAccountCode As String, ByRef pBookCode As String, ByRef pVType As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pTrnDC As String, ByRef pTrnAmount As Double, ByRef pChequeNo As String, ByRef pChqDate As String, ByRef pCostCCode As String, ByRef pDeptCode As String, ByRef pEmpCode As String, ByRef pExpCode As String, ByRef pIBRNo As String, ByRef pClearDate As String, ByRef pLocked As String, ByRef pNarration As String, ByRef xAmount As Double, ByRef xRemarks As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrDetail
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim RsCntPRDetail As ADODB.Recordset = Nothing
        Dim mCountBill As Integer
        Dim SqlStr As String = ""
        Dim pTRNType As String
        Dim pBillNo As String
        Dim pBillDate As String
        Dim pBillAmount As Double
        Dim pBillDC As String
        Dim pAmount As Double
        Dim pDC As String
        Dim pBillType As String
        Dim pSubRowNo As Integer
        Dim pRemarks As String
        Dim pDueDate As String
        Dim mRowCount As Integer
        Dim mTDSAmount As Integer
        Dim pSTTYPE As String
        Dim pSTFORMNAME As String
        Dim pSTFORMNO As String
        Dim pSTFORMDATE As String
        Dim pSTFORMAMT As Double
        Dim pSTDUEFORMNAME As String
        Dim pSTDUEFORMNO As String
        Dim pSTDUEFORMDATE As String
        Dim pSTDUEFORMAMT As Double
        Dim pISREGDNO As String
        Dim pSTFORMCODE As Integer
        Dim pSTDUEFORMCODE As Integer
        Dim pTaxableAmount As Double
        Dim pPONO As String
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mBillCompanyCode As Long

        SqlStr = " Select COUNT(1) CntCount From FIN_BILLDETAILS_TRN  " & vbCrLf _
            & " WHERE MKEY='" & CurMKey & "'" & vbCrLf _
            & " AND TRNDtlSubRowNo=" & pTRNDtlSubRow & "" & vbCrLf _
            & " AND " & vbCrLf _
            & " AccountCode='" & pAccountCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCntPRDetail, ADODB.LockTypeEnum.adLockReadOnly)
        If RsCntPRDetail.EOF = False Then
            mCountBill = IIf(IsDBNull(RsCntPRDetail.Fields("CntCount").Value), 0, RsCntPRDetail.Fields("CntCount").Value)
        End If
        pSubRowNo = 1000 * pRowNo
        SqlStr = " Select * From FIN_BILLDETAILS_TRN  " & vbCrLf _
            & " WHERE MKEY='" & CurMKey & "'" & vbCrLf _
            & " AND TRNDtlSubRowNo=" & pTRNDtlSubRow & "" & vbCrLf _
            & " AND " & vbCrLf _
            & " AccountCode='" & pAccountCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)
        mRowCount = 0
        If RsTempPRDetail.EOF = False Then
            '        pSubRowNo = 0										
            Do While RsTempPRDetail.EOF = False
                pSubRowNo = pSubRowNo + 1
                mRowCount = mRowCount + 1
                pTRNType = "T" 'IIf(IsNull(RsTempPRDetail!TRNTYPE), "B", RsTempPRDetail!TRNTYPE)										
                pBillNo = IIf(IsDBNull(RsTempPRDetail.Fields("BILLNO").Value), "", RsTempPRDetail.Fields("BILLNO").Value)
                pBillDate = IIf(IsDBNull(RsTempPRDetail.Fields("BILLDATE").Value), "", RsTempPRDetail.Fields("BILLDATE").Value)
                pBillAmount = IIf(IsDBNull(RsTempPRDetail.Fields("Amount").Value), 0, RsTempPRDetail.Fields("Amount").Value)
                pTaxableAmount = IIf(IsDBNull(RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value), 0, RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value)
                pPONO = IIf(IsDBNull(RsTempPRDetail.Fields("PONO").Value), "", RsTempPRDetail.Fields("PONO").Value)
                mDivCode = IIf(IsDBNull(RsTempPRDetail.Fields("DIV_CODE").Value), "", RsTempPRDetail.Fields("DIV_CODE").Value)
                pBillDC = "C"
                If mCountBill = 1 Then
                    pAmount = pTrnAmount ''Round(IIf(IsNull(RsTempPRDetail!Amount), 0, RsTempPRDetail!Amount) * Val(txtJVTDSRate.Text) * 0.01, 0)										
                Else
                    If mCountBill = mRowCount Then
                        pAmount = pTrnAmount - mTDSAmount
                    Else
                        pAmount = System.Math.Round((pBillAmount * CDbl(txtJVTDSRate.Text) * 0.01) + (pBillAmount * CDbl(txtESIRate.Text) * 0.01) + (pBillAmount * CDbl(txtSTDSRate.Text) * 0.01), 0)
                    End If
                End If
                mTDSAmount = mTDSAmount + pAmount
                pSTTYPE = "0" ' IIf(IsNull(RsTempPRDetail!STTYPE), "", RsTempPRDetail!STTYPE)										
                pSTFORMNAME = "" 'IIf(IsNull(RsTempPRDetail!STFORMNAME), "", RsTempPRDetail!STFORMNAME)										
                pSTFORMNO = "" 'IIf(IsNull(RsTempPRDetail!STFORMNO), "", RsTempPRDetail!STFORMNO)										
                pSTFORMDATE = "" 'IIf(IsNull(RsTempPRDetail!STFORMDATE), "", RsTempPRDetail!STFORMDATE)										
                pSTFORMAMT = 0 'IIf(IsNull(RsTempPRDetail!STFORMAMT), 0, RsTempPRDetail!STFORMAMT)										
                pSTDUEFORMNAME = "" ' IIf(IsNull(RsTempPRDetail!STDUEFORMNAME), "", RsTempPRDetail!STDUEFORMNAME)										
                pSTDUEFORMNO = "" '  IIf(IsNull(RsTempPRDetail!STDUEFORMNO), "", RsTempPRDetail!STDUEFORMNO)										
                pSTDUEFORMDATE = "" 'IIf(IsNull(RsTempPRDetail!STDUEFORMDATE), "", RsTempPRDetail!STDUEFORMDATE)										
                pSTDUEFORMAMT = 0 '   IIf(IsNull(RsTempPRDetail!STDUEFORMAMT), 0, RsTempPRDetail!STDUEFORMAMT)										
                pISREGDNO = "N" 'IIf(IsNull(RsTempPRDetail!ISREGDNO), "", RsTempPRDetail!ISREGDNO)										
                pSTFORMCODE = CInt("-1") 'IIf(IsNull(RsTempPRDetail!STFORMCODE), Null, RsTempPRDetail!STFORMCODE)										
                pSTDUEFORMCODE = CInt("-1") ' IIf(IsNull(RsTempPRDetail!STDUEFORMCODE), Null, RsTempPRDetail!STDUEFORMCODE)	
                mLocCode = IIf(IsDBNull(RsTempPRDetail.Fields("BILL_TO_LOC_ID").Value), "", RsTempPRDetail.Fields("BILL_TO_LOC_ID").Value)
                pDC = "D"
                pRemarks = xRemarks
                pDueDate = IIf(IsDBNull(RsTempPRDetail.Fields("DUEDATE").Value), "", RsTempPRDetail.Fields("DUEDATE").Value)
                mBillCompanyCode = IIf(IsDBNull(RsTempPRDetail.Fields("BILL_COMPANY_CODE").Value), RsCompany.Fields("COMPANY_CODE").Value, RsTempPRDetail.Fields("BILL_COMPANY_CODE").Value) ''

                SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf _
                    & " MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf _
                    & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf _
                    & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE, " & vbCrLf _
                    & " STTYPE, STFORMNAME, STFORMNO, " & vbCrLf _
                    & " STFORMDATE, STFORMAMT, STDUEFORMNAME, " & vbCrLf _
                    & " STDUEFORMNO, STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf _
                    & " ISREGDNO, STFORMCODE, STDUEFORMCODE,TAXABLE_AMOUNT,PONO,DIV_CODE,REF_NO,BILL_TO_LOC_ID, COMPANY_CODE, BOOKTYPE,BILL_COMPANY_CODE " & vbCrLf _
                    & " ) VALUES ( " & vbCrLf _
                    & " '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf _
                    & " '" & pAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & pSTTYPE & "', '" & MainClass.AllowSingleQuote(pSTFORMNAME) & "', '" & MainClass.AllowSingleQuote(pSTFORMNO) & "', " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(pSTFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTFORMAMT)) & ", '" & MainClass.AllowSingleQuote(pSTDUEFORMNAME) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(pSTDUEFORMNO) & "', TO_DATE('" & VB6.Format(pSTDUEFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTDUEFORMAMT)) & ", " & vbCrLf _
                    & " '" & pISREGDNO & "', " & Val(CStr(pSTFORMCODE)) & ", " & Val(CStr(pSTDUEFORMCODE)) & ", " & Val(CStr(pTaxableAmount)) & ", '" & pPONO & "'," & mDivCode & ",'', " & vbCrLf _
                    & " '" & mLocCode & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & VB.Left(lblBookType.Text, 1) & "'," & Val(mBillCompanyCode) & ") "

                pDBCn.Execute(SqlStr)

                If pTRNType = "N" Then
                    pBillType = "B"
                ElseIf pTRNType = "B" Or pTRNType = "O" Or pTRNType = "A" Then
                    pBillType = "P"
                Else
                    pBillType = pTRNType
                End If
                If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivCode, mLocCode, IIf(chkPLA.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrDetail
                RsTempPRDetail.MoveNext()
            Loop
        Else
            pTRNType = "T"
            pBillNo = pVType & pVNo
            pBillDate = pVDate
            pBillAmount = pTrnAmount
            pBillDC = "C"
            pAmount = xAmount
            pDC = "D"
            pRemarks = ""
            pBillType = "T"
            pDueDate = pBillDate

            mLocCode = GetDefaultLocation(pAccountCode)
            If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, "", VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivisionCode, mLocCode, IIf(chkPLA.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrDetail
        End If
        UpdateSuppPRDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateSuppPRDetail = False
        'Resume										
    End Function
    Private Function GenJVVno(ByRef xBookType As String) As String
        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String = ""
        Dim mBookType As String
        Dim mBookSubType As String
        ''    Call GenPrefixVNo										
        ''										
        GenJVVno = ""
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        If ADDMode = True Then
            SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND BookType='" & mBookType & "'" & vbCrLf _
                & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
                & " AND VTYPE='" & MainClass.AllowSingleQuote(xBookType) & "'"

            If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
                SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
            ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

            End If

            GenJVVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume										
    End Function
    Private Function UpdateTDSCreditDetail(ByRef pMKey As String, ByRef pVNoStr As String, ByRef pBookType As String, ByRef pBookSubType As String) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""
        Dim mTDSAccountCode As String
        Dim mExempted As String
        Dim mSectionCode As Integer
        Dim mAmountPaid As Double
        Dim mTdsRate As Double
        Dim mPartyName As String = ""
        Dim cntRow As Integer
        Dim mPartyCode As String = ""
        SqlStr = ""
        SqlStr = "DELETE FROM TDS_TRN WHERE MKey= '" & pMKey & "'"
        PubDBCn.Execute(SqlStr)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked Or chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            UpdateTDSCreditDetail = True
            Exit Function
        End If
        mTDSAccountCode = GetTDSAccountCode(txtTDSSection.Text)       ' IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        If mTDSAccountCode = "" Then
            MsgInformation("TDS ACCOUNT Code not Defined into System Pref.")
            UpdateTDSCreditDetail = False
        End If
        mSectionCode = -1
        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = cntRow
            SprdMain.Col = ColDC
            If VB.Left(UCase(Trim(SprdMain.Text)), 1) = "C" Then
                SprdMain.Col = ColAccountName
                mPartyName = Trim(SprdMain.Text)
                'If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    mPartyCode = MasterNo
                'End If
                If MainClass.ValidateWithMasterTable(txtTDSSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSectionCode = IIf(IsDBNull(MasterNo), -1, MasterNo)
                End If
                If mSectionCode = CDbl("-1") Then
                    MsgBox("TDS Section not defined of Party : " & mPartyName & ", so cann't be saved.")
                    UpdateTDSCreditDetail = False
                    Exit Function
                End If
                Exit For
            End If
        Next
        '    SprdMain.Row = 1										
        '    SprdMain.Col = ColAmount										
        mAmountPaid = Val(CStr(CDbl(txtTDSDeductOn.Text)))
        mTdsRate = Val(txtJVTDSRate.Text)
        mExempted = "N"
        If ADDMode = True Then
            SqlStr = "INSERT INTO TDS_TRN ( MKEY, COMPANY_CODE, " & vbCrLf & " FYEAR, ROWNO, SUBROWNO, VNO,VDATE, " & vbCrLf & " BOOKCODE, BOOKTYPE, BOOKSUBTYPE, ACCOUNTCODE, " & vbCrLf & " PARTYCODE, PARTYNAME, AMOUNTPAID, SECTIONCODE, " & vbCrLf & " TDSRATE, ISEXEPTED, EXEPTIONCNO, " & vbCrLf & " TDSAMOUNT, ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM) VALUES ( "
            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(pMKey)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " 1,1,'" & MainClass.AllowSingleQuote(pVNoStr) & "', TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & -1 & ",'" & pBookType & "', '" & pBookSubType & "', " & vbCrLf & " '" & mTDSAccountCode & "', '" & MainClass.AllowSingleQuote(mPartyCode) & "', '" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " " & Val(CStr(mAmountPaid)) & "," & mSectionCode & "," & Val(CStr(mTdsRate)) & ", " & vbCrLf & " '" & mExempted & "','', " & vbCrLf & " " & Val(txtJVTDSAmount.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','H')"
        Else
            SqlStr = " UPDATE TDS_TRN SET " & vbCrLf & " VDATE=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ACCOUNTCODE='" & mTDSAccountCode & "', " & vbCrLf & " PARTYCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "', PARTYNAME='" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " VNO='" & MainClass.AllowSingleQuote(pVNoStr) & "', " & vbCrLf & " AMOUNTPAID=" & Val(CStr(mAmountPaid)) & ", " & vbCrLf & " SECTIONCODE=" & mSectionCode & "," & vbCrLf & " TDSRATE=" & Val(CStr(mTdsRate)) & ", " & vbCrLf & " ISEXEPTED='" & mExempted & "', " & vbCrLf & " EXEPTIONCNO='', " & vbCrLf & " TDSAMOUNT=" & Val(txtJVTDSAmount.Text) & ",UPDATE_FROM='H', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE MKey= '" & pMKey & "'"
        End If
        PubDBCn.Execute(SqlStr)
        UpdateTDSCreditDetail = True
        Exit Function
UpdateError:
        UpdateTDSCreditDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume										
    End Function
    Private Function UpdateTDSDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pVNoStr As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pCancelled As String) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""
        Dim mAccountCode As String
        Dim mExempted As String
        Dim mSectionCode As Integer
        Dim mAmountPaid As Double
        Dim mTdsRate As Double
        Dim mPartyName As String
        Dim cntRow As Integer
        Dim pIsTDSHead As Boolean
        Dim mPartyCode As String
        Dim mLowerDec As String
        SqlStr = ""
        pIsTDSHead = False
        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = cntRow
            SprdMain.Col = ColAccountName
            If GetHeadType((SprdMain.Text)) = "T" Then
                pIsTDSHead = True
            End If
        Next
        If pCancelled = "Y" Or pIsTDSHead = False Then
            SqlStr = " DELETE FROM TDS_TRN " & vbCrLf & " WHERE MKey= '" & pMKey & "'" & vbCrLf & " AND BookType='" & pBookType & "' AND BookSubType='" & pBookSubType & "'"
            PubDBCn.Execute(SqlStr)
            UpdateTDSDetail = True
            Exit Function
        End If
        If TxtTDSAccount.Text = "" Then
            UpdateTDSDetail = True
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable((TxtTDSAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HeadType IN ('T','1')") = True Then
            mAccountCode = MasterNo
        Else
            UpdateTDSDetail = True
            Exit Function
        End If
        mPartyName = Trim(txtPName.Text)
        If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyCode = MasterNo
        Else
            mPartyCode = "-1"
        End If
        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSectionCode = MasterNo
        Else
            mSectionCode = -1
        End If
        If Val(txtAmountPaid.Text) = 0 Then
            mAmountPaid = Val(CStr(CDbl(LblDrAmt.Text)))
        Else
            mAmountPaid = Val(txtAmountPaid.Text)
        End If
        If Val(txtTdsRate.Text) = 0 Then
            mTdsRate = Val(txtTDSAmount.Text) * 100 / mAmountPaid
        Else
            mTdsRate = Val(txtTdsRate.Text)
        End If
        mExempted = IIf(chkExempted.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mLowerDec = IIf(chkISLowerDed.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        '    If ADDMode = True Then										
        If GetTransInTDS(pMKey, pBookType, pBookSubType) = False Then
            SqlStr = "INSERT INTO TDS_TRN ( MKEY, COMPANY_CODE, " & vbCrLf & " FYEAR, ROWNO, SUBROWNO, VNO,VDATE, " & vbCrLf & " BOOKCODE, BOOKTYPE, BOOKSUBTYPE, ACCOUNTCODE, " & vbCrLf & " PARTYCODE, PARTYNAME, AMOUNTPAID, SECTIONCODE, " & vbCrLf & " TDSRATE, ISEXEPTED, EXEPTIONCNO, ISLOWERDED, " & vbCrLf & " TDSAMOUNT, ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM) VALUES ( "
            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(pMKey)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mRowNo & ",1,'" & MainClass.AllowSingleQuote(pVNoStr) & "', TO_DATE('" & VB6.Format(txtVD.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & -1 & ",'" & pBookType & "', '" & pBookSubType & "', " & vbCrLf & " '" & mAccountCode & "', '" & MainClass.AllowSingleQuote(mPartyCode) & "', '" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " " & Val(CStr(mAmountPaid)) & "," & mSectionCode & "," & Val(CStr(mTdsRate)) & ", " & vbCrLf & " '" & mExempted & "','" & MainClass.AllowSingleQuote(txtExempted.Text) & "', '" & mLowerDec & "', " & vbCrLf & " " & Val(txtTDSAmount.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','H')"
        Else
            SqlStr = " UPDATE TDS_TRN SET BookType='" & pBookType & "', BookSubType='" & pBookSubType & "'," & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & ",VDATE=TO_DATE('" & VB6.Format(txtVD.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ACCOUNTCODE='" & mAccountCode & "', " & vbCrLf & " PARTYCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "', PARTYNAME='" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " VNO='" & MainClass.AllowSingleQuote(pVNoStr) & "', " & vbCrLf & " AMOUNTPAID=" & Val(CStr(mAmountPaid)) & ", " & vbCrLf & " SECTIONCODE=" & mSectionCode & "," & vbCrLf & " TDSRATE=" & Val(CStr(mTdsRate)) & ", " & vbCrLf & " ISEXEPTED='" & mExempted & "', " & vbCrLf & " ISLOWERDED='" & mLowerDec & "', " & vbCrLf & " EXEPTIONCNO='" & MainClass.AllowSingleQuote(txtExempted.Text) & "', " & vbCrLf & " TDSAMOUNT=" & Val(txtTDSAmount.Text) & ", UPDATE_FROM='H'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE MKey= '" & pMKey & "' AND " & vbCrLf & " BookType='" & VB.Left(lblBookType.Text, 1) & "' AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "'"
        End If
        pDBCn.Execute(SqlStr)
        UpdateTDSDetail = True
        Exit Function
UpdateError:
        UpdateTDSDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume										
    End Function
    Private Function GenVno() As String
        On Error GoTo ERR1
        Dim mVNo1 As String = ""
        Dim SqlStr2 As String = ""
        Dim SqlStr As String = ""
        Dim mBookType As String = ""
        Dim mBookSubType As String = ""
        Dim mVType As String = ""
        GenVno = ""
        txtVNo1.Text = GenPrefixVNo(TxtVDate.Text)
        If chkChqDeposit.CheckState = System.Windows.Forms.CheckState.Checked Then
            Call GetNewBook(mBookType, mBookSubType, mVType)
        Else
            mBookType = VB.Left(lblBookType.Text, 1)
            mBookSubType = VB.Right(lblBookType.Text, 1)
            mVType = Trim(txtVType.Text)
        End If
        If ADDMode = True Or txtVno.Text = "" Then
            SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND BookType='" & mBookType & "'" & vbCrLf _
                & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
                & " AND " & vbCrLf _
                & " VTYPE='" & MainClass.AllowSingleQuote(mVType) & "'"

            If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
                SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
            ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

            End If

            GenVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume										
    End Function
    Private Function GenEMIVno(ByRef mBookType As String, ByRef mBookSubType As String, ByRef mVType As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        GenEMIVno = ""
        If ADDMode = True Then
            SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND BookType='" & mBookType & "'" & vbCrLf _
                & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
                & " AND VTYPE='" & MainClass.AllowSingleQuote(mVType) & "'"

            If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
                SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
                SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
            ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

            End If

            GenEMIVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume										
    End Function
    Private Function UpdateDetail(ByRef mMkey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNo As String, ByRef mVDate As String, ByRef pNarration As String, ByRef pDBCn As ADODB.Connection) As Boolean
        On Error GoTo ErrDetail
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mAccountName As String
        Dim mAccountCode As String = ""
        Dim mChequeNo As String
        Dim mChqDate As String
        Dim mAmount As Double
        Dim mCCCode As String = ""
        Dim mExpCode As String = ""
        Dim mDeptCode As String = ""
        Dim mDivisionCode As Double
        Dim mEmpCode As String = ""
        Dim mIBRNo As String
        Dim mDC As String
        Dim mRemarks As String
        Dim mPRRowNo As Integer
        Dim mSubRowNo As Integer
        Dim mBookType As String = ""
        Dim mBookSubType As String = ""
        Dim mClearDate As String
        Dim mParticulars As String
        Dim VMkey As String
        Dim mIsFixedAssets As String
        Dim mSameVNo As Boolean
        Dim pSqlStr As String
        Dim RSDiv As ADODB.Recordset = Nothing
        Dim pDivCode As Double
        Dim mSAC As String
        Dim mCGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTPer As Double
        Dim mSGSTAmount As Double
        Dim mIGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mNetAmount As Double
        Dim mSaleBillPrefix As String
        Dim mSaleBillSeq As String
        Dim mSaleBillNo As String
        Dim mSaleBillDate As String
        If chkChqDeposit.CheckState = System.Windows.Forms.CheckState.Checked Then
            Call GetNewBook(mBookType, mBookSubType, mVType)
        Else
            mBookType = VB.Left(lblBookType.Text, 1)
            mBookSubType = VB.Right(lblBookType.Text, 1)
        End If
        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        '    Sqlstr = " DELETE FROM AST_ASSET_TRN " & vbCrLf _										
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _										
        ''            & " AND VMKEY='" & MainClass.AllowSingleQuote(mMkey) & "' " & vbCrLf _										
        ''            & " AND BOOKTYPE='" & vb.Left(lblBookType.text, 1) & "' "										
        '										
        '    PubDBCn.Execute Sqlstr										
        SqlStr = "Delete From FIN_SERVTAXDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From PAY_LOAN_MST Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMkey & "' " & vbCrLf & " AND BookType='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BooksubType='" & VB.Right(lblBookType.Text, 1) & "'"
        pDBCn.Execute(SqlStr)
        If (lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment) Then
            SqlStr = "UPDATE FIN_CHEQUE_MST SET " & vbCrLf & " CHEQUE_STATUS='O'," & vbCrLf & " VMKEY=''," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND VMKEY='" & MainClass.AllowSingleQuote(Trim(mMkey)) & "'"
            PubDBCn.Execute(SqlStr)
        End If
        mSameVNo = False
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColAccountName
                mAccountName = Trim(.Text)
                .Col = 0
                If mAccountName <> "" Then
                    .Col = ColPRRowNo
                    mPRRowNo = Val(.Text)
                    mSubRowNo = mPRRowNo
                    .Col = ColDC
                    mDC = UCase(VB.Left(.Text, 1))
                    .Col = ColAccountName
                    mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)
                    If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "ISFIXASSETS", "FIN_INVTYPE_MST", pDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                        mIsFixedAssets = MasterNo
                    Else
                        mIsFixedAssets = "N"
                    End If
                    .Col = ColParticulars
                    mParticulars = IIf(Trim(.Text) = "", pNarration, Trim(.Text))
                    .Col = ColAmount
                    mAmount = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(.Text))
                    .Col = ColChequeNo
                    mChequeNo = Trim(.Text)
                    .Col = ColChequeDate
                    mChqDate = IIf(mChequeNo = "", mVDate, Trim(.Text))
                    .Col = ColCC
                    mCCCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "CC_CODE", "CC_CODE", "FIN_CCENTER_HDR", pDBCn, mCCCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mCCCode, -1)
                    .Col = ColExp
                    mExpCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "COST_CENTER_CODE", "COST_CENTER_CODE", "CST_CENTER_MST", pDBCn, mExpCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mExpCode, -1)
                    .Col = ColDept
                    mDeptCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", pDBCn, mDeptCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mDeptCode, -1)
                    .Col = ColDivisionCode
                    mDivisionCode = IIf(MainClass.ValidateWithMasterTable(Val(SprdMain.Text), "DIV_CODE", "DIV_CODE", "INV_DIVISION_MST", pDBCn, mDivisionCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mDivisionCode, -1)
                    .Col = ColEmp
                    mEmpCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", pDBCn, mEmpCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mEmpCode, -1)
                    .Col = ColIBRNo
                    mIBRNo = .Text
                    .Col = ColSAC
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", pDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        mSAC = .Text
                    Else
                        mSAC = ""
                    End If
                    .Col = ColCGSTPer
                    mCGSTPer = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(.Text))
                    .Col = ColCGSTAmount
                    mCGSTAmount = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(.Text))
                    .Col = ColSGSTPer
                    mSGSTPer = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(.Text))
                    .Col = ColSGSTAmount
                    mSGSTAmount = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(.Text))
                    .Col = ColIGSTPer
                    mIGSTPer = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(.Text))
                    .Col = ColIGSTAmount
                    mIGSTAmount = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(.Text))
                    SprdMain.Col = ColSaleBillPrefix
                    mSaleBillPrefix = .Text
                    SprdMain.Col = ColSaleBillSeq
                    mSaleBillSeq = .Text
                    SprdMain.Col = ColSaleBillNo
                    mSaleBillNo = .Text
                    SprdMain.Col = ColSaleBillDate
                    mSaleBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                    .Col = ColClearDate
                    mClearDate = .Text

                    SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf _
                        & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf _
                        & " ChequeNo,ChqDate,CostCCode, " & vbCrLf _
                        & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate, " & vbCrLf _
                        & " PARTICULARS,DIV_CODE, " & vbCrLf _
                        & " SAC, CGST_PER, CGST_AMOUNT, " & vbCrLf _
                        & " SGST_PER, SGST_AMOUNT, IGST_PER, IGST_AMOUNT," & vbCrLf _
                        & " SALEBILLNOPREFIX, SALEBILLNOSEQ, SALEBILL_NO, SALEBILLDATE" & vbCrLf _
                        & ")" & vbCrLf _
                        & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & ",  " & vbCrLf _
                        & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf _
                        & " " & mSubRowNo & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & "," & vbCrLf _
                        & " '" & mSAC & "', " & mCGSTPer & ", " & mCGSTAmount & ", " & vbCrLf _
                        & " " & mSGSTPer & ", " & mSGSTAmount & "," & vbCrLf _
                        & " " & mIGSTPer & ", " & mIGSTAmount & ", " & vbCrLf _
                        & " '" & mSaleBillPrefix & "', '" & mSaleBillSeq & "', '" & mSaleBillNo & "', TO_DATE('" & VB6.Format(mSaleBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " )"

                    PubDBCn.Execute(SqlStr)
                    If chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mNetAmount = mAmount
                    Else
                        mNetAmount = mAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount
                    End If
                    'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
                    '    If UpdatePRDetailAutoJV(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mNetAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, (txtNarration.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), VB.Left(lblBookType.Text, 1), VB.Right(lblBookType.Text, 1), VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivisionCode, pProcessKey, IIf(chkPnL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), IIf(chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mSAC, mCGSTPer, mCGSTAmount, mSGSTPer, mSGSTAmount, mIGSTPer, mIGSTAmount) = False Then GoTo ErrDetail
                    'Else
                    If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mNetAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, (txtNarration.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), VB.Left(lblBookType.Text, 1), VB.Right(lblBookType.Text, 1), VB6.Format(txtExpDate.Text, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivisionCode, pProcessKey, IIf(chkPnL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), IIf(chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mSAC, mCGSTPer, mCGSTAmount, mSGSTPer, mSGSTAmount, mIGSTPer, mIGSTAmount) = False Then GoTo ErrDetail
                    'End If
                    If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        If MasterNo = "L" Then
                            If UpdateLoanDetail(pDBCn, mMkey, mEmpCode, IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrDetail
                        ElseIf MasterNo = "S" Then
                            If UpdateServiceTaxDetail(pDBCn, mMkey, IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")) = False Then GoTo ErrDetail
                        End If
                    End If
                    If (lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment) And mChequeNo <> "" Then
                        VMkey = mMkey
                        '                    If chkCancelled.Value = vbUnchecked Then										
                        If UpdateChequeDetail(mChequeNo, VMkey, "C", mSameVNo) = False Then GoTo ErrDetail
                        '                    Else										
                        '                        If UpdateChequeDetail(mChequeNo, VMkey, "O") = False Then GoTo ErrDetail										
                        '                    End If										
                    End If
                    '                If lblBookType.text = ConJournal Or lblBookType.text = ConCashBook Then										
                    ''                    If mIsFixedAssets = "Y" And chkCancelled.Value = vbUnchecked Then										
                    ''                        If UpdateFixedAsset(mMkey, mVNo, mVDate, mAccountCode, mAmount, mDC, mParticulars) = False Then GoTo ErrDetail										
                    ''                     End If										
                    '                End If										
                End If
                mSameVNo = True
            Next I
        End With
        UpdateDetail = True
        Exit Function
ErrDetail:
        'Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDetail = False
        'Resume										
    End Function

    '    Public Function UpdatePRDetailAutoJV(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pRowNo As Integer, ByRef pTRNDtlSubRow As Integer, ByRef pAccountCode As String, ByRef pBookCode As String, ByRef pVType As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pTrnDC As String, ByRef pTrnAmount As Double, ByRef pChequeNo As String, ByRef pChqDate As String, ByRef pCostCCode As String, ByRef pDeptCode As String, ByRef pEmpCode As String, ByRef pExpCode As String, ByRef pIBRNo As String, ByRef pClearDate As String, ByRef pLocked As String, ByRef pNarration As String, ByRef xRemarks As String, ByRef pCancelled As String, ByRef pOldBookType As String, ByRef pOldBookSubType As String, ByRef pExpDate As String, ByRef pAddMode As Boolean, ByRef pAddUser As String, ByRef pAddDate As String, ByRef mDivisionCode As Double, ByRef pTempProcessKey As Double, Optional ByRef pPLFlag As String = "", Optional ByRef pReverseCharge As String = "", Optional ByRef pSAC As String = "", Optional ByRef pCGSTPer As Double = 0, Optional ByRef pCGSTAmount As Double = 0, Optional ByRef pSGSTPer As Double = 0, Optional ByRef pSGSTAmount As Double = 0, Optional ByRef pIGSTPer As Double = 0, Optional ByRef pIGSTAmount As Double = 0) As Boolean
    '        On Error GoTo ErrDetail
    '        Dim RsTempPRDetail As ADODB.Recordset = Nothing
    '        Dim RsTempPRBrn As ADODB.Recordset = Nothing
    '        Dim SqlStr As String = ""
    '        Dim pTRNType As String
    '        Dim pBillNo As String
    '        Dim pBillDate As String
    '        Dim pBillAmount As Double
    '        Dim pBillDC As String
    '        Dim pAmount As Double
    '        Dim pDC As String
    '        Dim pBillType As String
    '        Dim pSubRowNo As Integer
    '        Dim pRemarks As String
    '        Dim pDueDate As String
    '        Dim pSTTYPE As String
    '        Dim pSTFORMNAME As String
    '        Dim pSTFORMNO As String
    '        Dim pSTFORMDATE As String
    '        Dim pSTFORMAMT As Double
    '        Dim pSTDUEFORMNAME As String
    '        Dim pSTDUEFORMNO As String
    '        Dim pSTDUEFORMDATE As String
    '        Dim pSTDUEFORMAMT As Double
    '        Dim pISREGDNO As String
    '        Dim pSTFORMCODE As Integer
    '        Dim pSTDUEFORMCODE As Integer
    '        Dim pTaxableAmount As Double
    '        Dim pPONO As String
    '        Dim xDivCode As Double
    '        Dim mRefNo As String
    '        Dim mLocCode As String
    '        Dim mBillCompanyCode As Long
    '        Dim mBillCompanyCode As String
    '        pSubRowNo = 1000 * pRowNo
    '        If pCancelled = "Y" Then
    '            pTrnAmount = 0
    '            GoTo SummariedPart
    '        End If
    '        If GetAccountBalancingMethod(pAccountCode, True) = "S" Then
    '            GoTo SummariedPart
    '        End If

    '        'CODED AS ON [04-02-2022] FOR AUTO JV BY RSS
    '        If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
    '            'CODE FOR OTHER COMPANY POSTING
    '            SqlStr = " SELECT TB.BILL_TO_LOC_ID, TRN.COMPANY_CODE,PRN.COMP_AC_CODE,SUM(TB.AMOUNT) AS AMOUNT " & vbCrLf _
    '                    & " From FIN_TEMPBILL_TRN TB,FIN_POSTED_TRN TRN, FIN_PRINT_MST PRN " & vbCrLf _
    '                    & " WHERE TB.BILLNO=TRN.BILLNO AND PRN.COMPANY_CODE=TRN.COMPANY_CODE AND TRN.TRNTYPE='B' " & vbCrLf _
    '                    & " AND TRN.COMPANY_CODE<>" & RsCompany.Fields("COMPANY_CODE").Value & " AND TB.UserID='" & PubUserID & "'" & vbCrLf _
    '                    & " AND TB.TEMPMKEY=" & pTempProcessKey & "" & " AND TB.TRNDtlSubRowNo=" & pTRNDtlSubRow & "" & vbCrLf _
    '                    & " AND TB.AccountCode='" & pAccountCode & "'" & " AND TB.BookType='" & pOldBookType & pOldBookSubType & "'" & vbCrLf _
    '                    & " GROUP BY TRN.COMPANY_CODE,PRN.COMP_AC_CODE,TB.BILL_TO_LOC_ID"
    '            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)
    '            If RsTempPRDetail.EOF = False Then
    '                Do While RsTempPRDetail.EOF = False
    '                    pSubRowNo = pSubRowNo + 1
    '                    pTRNType = "O"
    '                    pBillNo = pVNo
    '                    pBillDate = pVDate
    '                    pBillAmount = RsTempPRDetail.Fields("Amount").Value
    '                    pBillDC = pTrnDC
    '                    pAmount = RsTempPRDetail.Fields("Amount").Value
    '                    pDC = pTrnDC
    '                    pRemarks = xRemarks
    '                    pBillType = "P"
    '                    pDueDate = pBillDate
    '                    pAccountCode = RsTempPRDetail.Fields("COMP_AC_CODE").Value
    '                    mLocCode = RsTempPRDetail.Fields("TB.BILL_TO_LOC_ID").Value
    '                    If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, pExpDate, pAddMode, pAddUser, pAddDate, xDivCode, mLocCode, pPLFlag) = False Then GoTo ErrDetail
    '                    RsTempPRDetail.MoveNext()
    '                Loop
    '            End If

    '            'CODE FOR SAME COMPANY POSTING
    '            SqlStr = " Select TB.* From FIN_TEMPBILL_TRN TB,FIN_POSTED_TRN TRN" & vbCrLf _
    '                    & " WHERE TB.BILLNO=TRN.BILLNO AND TRN.TRNTYPE='B' " & vbCrLf _
    '                    & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
    '                    & " AND TB.UserID='" & PubUserID & "' AND TB.TEMPMKEY=" & pTempProcessKey & "" & vbCrLf _
    '                    & " AND TB.TRNDtlSubRowNo=" & pTRNDtlSubRow & "" & vbCrLf _
    '                    & " AND TB.AccountCode='" & pAccountCode & "' AND TB.BookType='" & pOldBookType & pOldBookSubType & "'"
    '            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)
    '            If RsTempPRDetail.EOF = False Then
    '                Do While RsTempPRDetail.EOF = False
    '                    pSubRowNo = pSubRowNo + 1
    '                    pTRNType = RsTempPRDetail.Fields("TrnType").Value
    '                    pBillNo = RsTempPRDetail.Fields("BillNo").Value
    '                    pBillDate = IIf(IsDBNull(RsTempPRDetail.Fields("BillDate").Value), pVDate, RsTempPRDetail.Fields("BillDate").Value)
    '                    pBillAmount = RsTempPRDetail.Fields("BillAmount").Value
    '                    pBillDC = RsTempPRDetail.Fields("BillDC").Value
    '                    pAmount = RsTempPRDetail.Fields("Amount").Value
    '                    pDC = RsTempPRDetail.Fields("DC").Value
    '                    pRemarks = IIf(IsDBNull(RsTempPRDetail.Fields("REMARKS").Value), "", RsTempPRDetail.Fields("REMARKS").Value)
    '                    pDueDate = IIf(IsDBNull(RsTempPRDetail.Fields("DueDate").Value), "", RsTempPRDetail.Fields("DueDate").Value)
    '                    pSTFORMNAME = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMNAME").Value), "", RsTempPRDetail.Fields("STFORMNAME").Value)
    '                    pSTFORMNO = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMNO").Value), "", RsTempPRDetail.Fields("STFORMNO").Value)
    '                    pSTFORMDATE = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMDATE").Value), "", RsTempPRDetail.Fields("STFORMDATE").Value)
    '                    pSTFORMAMT = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMAMT").Value), 0, RsTempPRDetail.Fields("STFORMAMT").Value)
    '                    pSTDUEFORMNAME = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMNAME").Value), "", RsTempPRDetail.Fields("STDUEFORMNAME").Value)
    '                    pSTDUEFORMNO = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMNO").Value), "", RsTempPRDetail.Fields("STDUEFORMNO").Value)
    '                    pSTDUEFORMDATE = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMDATE").Value), "", RsTempPRDetail.Fields("STDUEFORMDATE").Value)
    '                    pSTDUEFORMAMT = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMAMT").Value), 0, RsTempPRDetail.Fields("STDUEFORMAMT").Value)
    '                    pSTFORMCODE = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMCODE").Value), -1, RsTempPRDetail.Fields("STFORMCODE").Value)
    '                    pSTDUEFORMCODE = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMCODE").Value), -1, RsTempPRDetail.Fields("STDUEFORMCODE").Value)
    '                    pSTTYPE = IIf(IsDBNull(RsTempPRDetail.Fields("STTYPE").Value), "", RsTempPRDetail.Fields("STTYPE").Value)
    '                    pISREGDNO = IIf(IsDBNull(RsTempPRDetail.Fields("ISREGDNO").Value), "", RsTempPRDetail.Fields("ISREGDNO").Value)
    '                    pTaxableAmount = IIf(IsDBNull(RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value), 0, RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value)
    '                    pPONO = IIf(IsDBNull(RsTempPRDetail.Fields("PONO").Value), "", RsTempPRDetail.Fields("PONO").Value)
    '                    xDivCode = IIf(IsDBNull(RsTempPRDetail.Fields("DIV_CODE").Value), "", RsTempPRDetail.Fields("DIV_CODE").Value)
    '                    mRefNo = IIf(IsDBNull(RsTempPRDetail.Fields("REF_NO").Value), "", RsTempPRDetail.Fields("REF_NO").Value)
    '                    mLocCode = RsTempPRDetail.Fields("TB.BILL_TO_LOC_ID").Value
    '                    mBillCompanyCode = IIf(IsDBNull(RsTempPRDetail.Fields("BILL_COMPANY_CODE").Value), 0, RsTempPRDetail.Fields("BILL_COMPANY_CODE").Value)
    '                    mBillCompanyCode = IIf(mBillCompanyCode <= 0, RsCompany.Fields("COMPANY_CODE").Value, mBillCompanyCode)

    '                    SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf _
    '                        & " MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf _
    '                        & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf _
    '                        & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE, " & vbCrLf _
    '                        & " STFORMNAME, STFORMNO, " & vbCrLf _
    '                        & " STFORMDATE, STFORMAMT, " & vbCrLf _
    '                        & " STDUEFORMNAME, STDUEFORMNO, " & vbCrLf _
    '                        & " STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf _
    '                        & " STFORMCODE, STDUEFORMCODE, " & vbCrLf _
    '                        & " STTYPE, ISREGDNO, TAXABLE_AMOUNT,PONO,DIV_CODE,REF_NO,BILL_TO_LOC_ID,COMPANY_CODE, BOOKTYPE,BILL_COMPANY_CODE " & vbCrLf _
    '                        & " ) VALUES ( "
    '                    SqlStr = SqlStr & vbCrLf _
    '                        & " '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf _
    '                        & " '" & pAccountCode & "'," & vbCrLf _
    '                        & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf _
    '                        & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf _
    '                        & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
    '                        & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf _
    '                        & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
    '                        & " '" & MainClass.AllowSingleQuote(pSTFORMNAME) & "', '" & MainClass.AllowSingleQuote(pSTFORMNO) & "', " & vbCrLf _
    '                        & " TO_DATE('" & VB6.Format(pSTFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTFORMAMT)) & ", " & vbCrLf _
    '                        & " '" & MainClass.AllowSingleQuote(pSTDUEFORMNAME) & "', '" & MainClass.AllowSingleQuote(pSTDUEFORMNO) & "', " & vbCrLf _
    '                        & " TO_DATE('" & VB6.Format(pSTDUEFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTDUEFORMAMT)) & ", " & vbCrLf _
    '                        & " " & Val(CStr(pSTFORMCODE)) & ", " & Val(CStr(pSTDUEFORMCODE)) & ", " & vbCrLf _
    '                        & " '" & pSTTYPE & "', '" & pISREGDNO & "', " & Val(CStr(pTaxableAmount)) & ", '" & pPONO & "', " & Val(CStr(xDivCode)) & ",'" & MainClass.AllowSingleQuote(mRefNo) & "','" & mLocCode & "'," & vbCrLf _
    '                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & VB.Left(lblBookType.Text, 1) & "'," & mBillCompanyCode & " ) "
    '                    pDBCn.Execute(SqlStr)
    '                    If pTRNType = "N" Then
    '                        pBillType = "B"
    '                    ElseIf pTRNType = "B" Or pTRNType = "O" Or pTRNType = "A" Then
    '                        pBillType = "P"
    '                    Else
    '                        pBillType = pTRNType
    '                    End If
    '                    If (pTRNType = "O" Or pTRNType = "A") And mRefNo <> "" Then
    '                        pBillNo = IIf(pBillNo = "ON ACCOUNT", "ON AC", pBillNo) & "-" & mRefNo
    '                    End If
    '                    pRemarks = IIf(Trim(pRemarks) = "", xRemarks, pRemarks & vbNewLine & xRemarks)
    '                    If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, pExpDate, pAddMode, pAddUser, pAddDate, xDivCode, mLocCode, pPLFlag) = False Then GoTo ErrDetail
    '                    RsTempPRDetail.MoveNext()
    '                Loop
    '            End If

    '            'HERE CODE FOR AUTO JV
    '            SqlStr = " Select * From FIN_TEMPBILL_TRN" & vbCrLf _
    '                & " WHERE UserID='" & PubUserID & "' AND TEMPMKEY=" & pTempProcessKey & "" & vbCrLf _
    '                & " AND TRNDtlSubRowNo=" & pTRNDtlSubRow & "" & vbCrLf _
    '                & " AND AccountCode='" & pAccountCode & "'" & vbCrLf _
    '                & " AND BookType='" & pOldBookType & pOldBookSubType & "'"

    '            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)
    '            If RsTempPRDetail.EOF = False Then
    '                'HERE CODE TO BE WRITTEN FOR AUTO JV
    '                'RsTempPRBrn
    '                SqlStr = " SELECT TRN.COMPANY_CODE,SUM(TB.AMOUNT) AS AMOUNT " & vbCrLf _
    '                        & " From FIN_TEMPBILL_TRN TB,FIN_POSTED_TRN TRN" & vbCrLf _
    '                        & " WHERE TB.BILLNO=TRN.BILLNO AND TRN.TRNTYPE='B' " & vbCrLf _
    '                        & " AND TRN.COMPANY_CODE<>" & RsCompany.Fields("COMPANY_CODE").Value & " AND TB.UserID='" & PubUserID & "'" & vbCrLf _
    '                        & " AND TB.TEMPMKEY=" & pTempProcessKey & "" & " AND TB.TRNDtlSubRowNo=" & pTRNDtlSubRow & "" & vbCrLf _
    '                        & " AND TB.AccountCode='" & pAccountCode & "'" & " AND TB.BookType='" & pOldBookType & pOldBookSubType & "'" & vbCrLf _
    '                        & " GROUP BY TRN.COMPANY_CODE"
    '                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRBrn, ADODB.LockTypeEnum.adLockReadOnly)
    '                If RsTempPRBrn.EOF = False Then
    '                    Do While RsTempPRBrn.EOF = False

    '                        'pSubRowNo = pSubRowNo + 1
    '                        'pTRNType = "O"
    '                        'pBillNo = pVNo
    '                        'pBillDate = pVDate
    '                        'pBillAmount = RsTempPRDetail.Fields("Amount").Value
    '                        'pBillDC = pTrnDC
    '                        'pAmount = RsTempPRDetail.Fields("Amount").Value
    '                        'pDC = pTrnDC
    '                        'pRemarks = xRemarks
    '                        'pBillType = "P"
    '                        'pDueDate = pBillDate
    '                        'pAccountCode = RsTempPRDetail.Fields("COMP_AC_CODE").Value
    '                        'If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, pExpDate, pAddMode, pAddUser, pAddDate, xDivCode, pPLFlag) = False Then GoTo ErrDetail
    '                        RsTempPRBrn.MoveNext()
    '                    Loop
    '                End If
    '            Else
    '                pTRNType = "O"
    '                pBillNo = pVNo
    '                pBillDate = pVDate
    '                pBillAmount = pTrnAmount
    '                pBillDC = pTrnDC
    '                pAmount = pTrnAmount
    '                pDC = pTrnDC
    '                pRemarks = xRemarks
    '                pBillType = "P"
    '                pDueDate = pBillDate
    '                mLocCode = GetDefaultLocation(pAccountCode)
    '                If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, "", pExpDate, pAddMode, pAddUser, pAddDate, mDivisionCode, mLocCode, pPLFlag, pReverseCharge, pSAC, pCGSTPer, pCGSTAmount, pSGSTPer, pSGSTAmount, pIGSTPer, pIGSTAmount) = False Then GoTo ErrDetail
    '            End If
    '        Else
    '            GoTo PrDetailPost
    '        End If
    'PrDetailPost:
    '        'END OF CODED AS ON [04-02-2022] FOR AUTO JV BY RSS

    '        SqlStr = " Select * From FIN_TEMPBILL_TRN" & vbCrLf _
    '            & " WHERE UserID='" & PubUserID & "' AND TEMPMKEY=" & pTempProcessKey & "" & vbCrLf _
    '            & " AND TRNDtlSubRowNo=" & pTRNDtlSubRow & "" & vbCrLf _
    '            & " AND AccountCode='" & pAccountCode & "'" & vbCrLf _
    '            & " AND BookType='" & pOldBookType & pOldBookSubType & "'"
    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)
    '        If RsTempPRDetail.EOF = False Then
    '            Do While RsTempPRDetail.EOF = False
    '                pSubRowNo = pSubRowNo + 1
    '                pTRNType = RsTempPRDetail.Fields("TrnType").Value
    '                pBillNo = RsTempPRDetail.Fields("BillNo").Value
    '                pBillDate = IIf(IsDBNull(RsTempPRDetail.Fields("BillDate").Value), pVDate, RsTempPRDetail.Fields("BillDate").Value)
    '                pBillAmount = RsTempPRDetail.Fields("BillAmount").Value
    '                pBillDC = RsTempPRDetail.Fields("BillDC").Value
    '                pAmount = RsTempPRDetail.Fields("Amount").Value
    '                pDC = RsTempPRDetail.Fields("DC").Value
    '                pRemarks = IIf(IsDBNull(RsTempPRDetail.Fields("REMARKS").Value), "", RsTempPRDetail.Fields("REMARKS").Value)
    '                pDueDate = IIf(IsDBNull(RsTempPRDetail.Fields("DueDate").Value), "", RsTempPRDetail.Fields("DueDate").Value)
    '                pSTFORMNAME = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMNAME").Value), "", RsTempPRDetail.Fields("STFORMNAME").Value)
    '                pSTFORMNO = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMNO").Value), "", RsTempPRDetail.Fields("STFORMNO").Value)
    '                pSTFORMDATE = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMDATE").Value), "", RsTempPRDetail.Fields("STFORMDATE").Value)
    '                pSTFORMAMT = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMAMT").Value), 0, RsTempPRDetail.Fields("STFORMAMT").Value)
    '                pSTDUEFORMNAME = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMNAME").Value), "", RsTempPRDetail.Fields("STDUEFORMNAME").Value)
    '                pSTDUEFORMNO = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMNO").Value), "", RsTempPRDetail.Fields("STDUEFORMNO").Value)
    '                pSTDUEFORMDATE = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMDATE").Value), "", RsTempPRDetail.Fields("STDUEFORMDATE").Value)
    '                pSTDUEFORMAMT = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMAMT").Value), 0, RsTempPRDetail.Fields("STDUEFORMAMT").Value)
    '                pSTFORMCODE = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMCODE").Value), -1, RsTempPRDetail.Fields("STFORMCODE").Value)
    '                pSTDUEFORMCODE = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMCODE").Value), -1, RsTempPRDetail.Fields("STDUEFORMCODE").Value)
    '                pSTTYPE = IIf(IsDBNull(RsTempPRDetail.Fields("STTYPE").Value), "", RsTempPRDetail.Fields("STTYPE").Value)
    '                pISREGDNO = IIf(IsDBNull(RsTempPRDetail.Fields("ISREGDNO").Value), "", RsTempPRDetail.Fields("ISREGDNO").Value)
    '                pTaxableAmount = IIf(IsDBNull(RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value), 0, RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value)
    '                pPONO = IIf(IsDBNull(RsTempPRDetail.Fields("PONO").Value), "", RsTempPRDetail.Fields("PONO").Value)
    '                xDivCode = IIf(IsDBNull(RsTempPRDetail.Fields("DIV_CODE").Value), "", RsTempPRDetail.Fields("DIV_CODE").Value)
    '                mRefNo = IIf(IsDBNull(RsTempPRDetail.Fields("REF_NO").Value), "", RsTempPRDetail.Fields("REF_NO").Value)
    '                mLocCode = RsTempPRDetail.Fields("TB.BILL_TO_LOC_ID").Value

    '                mBillCompanyCode = IIf(IsDBNull(RsTempPRDetail.Fields("BILL_COMPANY_CODE").Value), RsCompany.Fields("COMPANY_CODE").Value, RsTempPRDetail.Fields("BILL_COMPANY_CODE").Value)
    '                '

    '                SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf _
    '                    & " MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf _
    '                    & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf _
    '                    & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE, " & vbCrLf _
    '                    & " STFORMNAME, STFORMNO, " & vbCrLf & " STFORMDATE, STFORMAMT, " & vbCrLf _
    '                    & " STDUEFORMNAME, STDUEFORMNO, " & vbCrLf _
    '                    & " STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf _
    '                    & " STFORMCODE, STDUEFORMCODE, " & vbCrLf _
    '                    & " STTYPE, ISREGDNO, TAXABLE_AMOUNT,PONO,DIV_CODE,REF_NO,BILL_TO_LOC_ID, COMPANY_CODE, BOOKTYPE,BILL_COMPANY_CODE " & vbCrLf _
    '                    & " ) VALUES ( "

    '                SqlStr = SqlStr & vbCrLf _
    '                    & " '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf _
    '                    & " '" & pAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf _
    '                    & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf _
    '                    & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
    '                    & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf _
    '                    & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
    '                    & " '" & MainClass.AllowSingleQuote(pSTFORMNAME) & "', '" & MainClass.AllowSingleQuote(pSTFORMNO) & "', " & vbCrLf _
    '                    & " TO_DATE('" & VB6.Format(pSTFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTFORMAMT)) & ", " & vbCrLf _
    '                    & " '" & MainClass.AllowSingleQuote(pSTDUEFORMNAME) & "', '" & MainClass.AllowSingleQuote(pSTDUEFORMNO) & "', " & vbCrLf _
    '                    & " TO_DATE('" & VB6.Format(pSTDUEFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTDUEFORMAMT)) & ", " & vbCrLf _
    '                    & " " & Val(CStr(pSTFORMCODE)) & ", " & Val(CStr(pSTDUEFORMCODE)) & ", " & vbCrLf _
    '                    & " '" & pSTTYPE & "', '" & pISREGDNO & "', " & Val(CStr(pTaxableAmount)) & ", '" & pPONO & "', " & Val(CStr(xDivCode)) & ",'" & MainClass.AllowSingleQuote(mRefNo) & "','" & mLocCode & "'," & vbCrLf _
    '                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & VB.Left(lblBookType.Text, 1) & "'," & mBillCompanyCode & ") "

    '                pDBCn.Execute(SqlStr)
    '                If pTRNType = "N" Then
    '                    pBillType = "B"
    '                ElseIf pTRNType = "B" Or pTRNType = "O" Or pTRNType = "A" Then
    '                    pBillType = "P"
    '                Else
    '                    pBillType = pTRNType
    '                End If
    '                If (pTRNType = "O" Or pTRNType = "A") And mRefNo <> "" Then
    '                    pBillNo = IIf(pBillNo = "ON ACCOUNT", "ON AC", pBillNo) & "-" & mRefNo
    '                End If
    '                pRemarks = IIf(Trim(pRemarks) = "", xRemarks, pRemarks & vbNewLine & xRemarks)
    '                If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, pExpDate, pAddMode, pAddUser, pAddDate, xDivCode, mLocCode, pPLFlag) = False Then GoTo ErrDetail
    '                RsTempPRDetail.MoveNext()
    '            Loop
    '        Else
    'SummariedPart:
    '            pTRNType = "O"
    '            pBillNo = pVNo
    '            pBillDate = pVDate
    '            pBillAmount = pTrnAmount
    '            pBillDC = pTrnDC
    '            pAmount = pTrnAmount
    '            pDC = pTrnDC
    '            pRemarks = xRemarks
    '            pBillType = "P"
    '            pDueDate = pBillDate
    '            mLocCode = GetDefaultLocation(pAccountCode)
    '            If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, "", pExpDate, pAddMode, pAddUser, pAddDate, mDivisionCode, mLocCode, pPLFlag, pReverseCharge, pSAC, pCGSTPer, pCGSTAmount, pSGSTPer, pSGSTAmount, pIGSTPer, pIGSTAmount) = False Then GoTo ErrDetail
    '        End If
    '        UpdatePRDetailAutoJV = True
    '        Exit Function
    'ErrDetail:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '        UpdatePRDetailAutoJV = False
    '        'Resume
    '    End Function

    Private Function UpdateAutoJvMain() As Boolean
        On Error GoTo ErrPart
        Dim tBookCode As String = ""
        Dim SqlStr As String = ""
        Dim tDrCr As String
        Dim tVAmount As Double
        Dim tVnoStr As String
        Dim tVType As String = ""
        Dim tVNoPrefix As String
        Dim tVNoSuffix As String
        Dim tBookType As String = ""
        Dim tBookSubType As String = ""
        Dim tVNo As String
        Dim tCancelled As String
        Dim tISTDSDEDUCT As String
        Dim tISESIDEDUCT As String
        Dim tISSTDSDEDUCT As String
        Dim tIsSuppBill As String
        Dim tIsCapital As String
        Dim tExpPartyCode As String = ""
        Dim tImpPartyCode As String = ""
        Dim tExpDate As String
        Dim tISMODVAT As String
        Dim tIsPLA As String
        Dim tIsSTClaim As String
        Dim tIsServtaxClaim As String
        Dim tIsServTaxRefund As String
        Dim tNoOfEMI As String
        Dim tTotalNoOfEMI As Integer
        Dim I As Integer
        Dim tVDate As String
        Dim tPLFlag As String
        Dim cntRow As Integer
        Dim xSqlStr As String
        Dim RSDiv As ADODB.Recordset = Nothing
        Dim tDC As String = ""
        Dim tDivCode As Double
        Dim tServiceCode As Double
        Dim tReverseChargeApp As String

        'txtNarration.Text = Trim(Replace(txtNarration.Text, vbCrLf, ""))
        'SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BookSubType='" & mBookSubType & "'" & vbCrLf & " AND " & vbCrLf & " VTYPE='" & MainClass.AllowSingleQuote(mVType) & "'"
        'tVNo = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")

        ''tVNo = GenVno()
        'tVNoPrefix = MainClass.AllowSingleQuote(Trim(txtVNo1.Text))
        'tVNoSuffix = ""
        'tVnoStr = tVNoPrefix & tVType & tVNo & tVNoSuffix
        'tCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tPLFlag = IIf(chkPnL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tIsSuppBill = IIf(chkSuppBill.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tIsCapital = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        ' ''tISMODVAT & "','" & tIsPLA & "','" & tIsSTClaim & "','" & tIsServtaxClaim & "','" & tIsServTaxRefund & "'										
        'tISMODVAT = IIf(chkModvat.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tIsPLA = IIf(chkPLA.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tIsSTClaim = IIf(chkSTClaim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tIsServtaxClaim = IIf(chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tIsServTaxRefund = IIf(chkServTaxRefund.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'Select Case lblBookType.Text
        '    Case ConJournal
        '        tBookCode = CStr(ConJournalBookCode)
        '    Case ConContra
        '        tBookCode = CStr(ConContraBookCode)
        '    Case Else
        '        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            tBookCode = MasterNo
        '        End If
        'End Select
        'If MainClass.ValidateWithMasterTable(txtExpPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    tExpPartyCode = MasterNo
        'End If
        'If MainClass.ValidateWithMasterTable(txtImpPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    tImpPartyCode = MasterNo
        'End If
        'tISTDSDEDUCT = IIf(chkTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tISESIDEDUCT = IIf(chkESI.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tISSTDSDEDUCT = IIf(ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'tReverseChargeApp = IIf(chkReverseCharge.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        'If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    tServiceCode = Val(MasterNo)
        'Else
        '    tServiceCode = -1
        'End If
        'If ADDMode = True Then
        '    mRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
        '    CurMKey = RsCompany.Fields("COMPANY_CODE").Value & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(mRowNo)
        '    SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " ISTDSDEDUCT,TDSPER,TDSAMOUNT, " & vbCrLf & " ISESIDEDUCT,ESIPER,ESIAMOUNT, ISSTDSDEDUCT,STDSPER,STDSAMOUNT," & vbCrLf & " ISSUPPBILL,MODVATNO ,STREFUNDNO, ISCAPITAL," & vbCrLf & " IMP_SUPP_CUST_CODE, IMP_MRR_NO, " & vbCrLf & " IMP_BILL_NO, IMP_BILL_DATE,  " & vbCrLf & " EXP_SUPP_CUST_CODE, EXP_BILL_NO,  " & vbCrLf & " EXP_BILL_DATE, AUTHORISED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE, " & vbCrLf & " ISMODVAT, ISPLA, ISSTCLAIM, ISSERVTAXCLAIM, ISSERVTAXREFUND, SERVNO, PL_FLAG, " & vbCrLf & " SERVICE_CODE, SERVICE_ON_AMT, SERVICE_TAX_PER, " & vbCrLf & " SERVICE_TAX_AMOUNT, SERV_PROVIDER_PER, SERV_RECIPIENT_PER,REVERSE_CHARGE_APP, " & vbCrLf & " IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY) VALUES ( "
        '    SqlStr = SqlStr & vbCrLf & " '" & CurMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mRowNo & ", " & vbCrLf & " '" & tVType & "', '" & tVNoPrefix & "', " & vbCrLf & " " & Val(tVNo) & ", '" & tVNoSuffix & "', '" & tVnoStr & "', " & vbCrLf & " TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & tBookType & "', '" & tBookSubType & "', " & vbCrLf & " '" & tBookCode & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "', '" & tCancelled & "', " & vbCrLf & " '" & tISTDSDEDUCT & "'," & Val(txtJVTDSRate.Text) & ", " & Val(txtJVTDSAmount.Text) & ", " & vbCrLf & " '" & tISESIDEDUCT & "'," & Val(txtESIRate.Text) & ", " & Val(txtESIAmount.Text) & ", " & vbCrLf & " '" & tISSTDSDEDUCT & "'," & Val(txtSTDSRate.Text) & ", " & Val(txtSTDSAmount.Text) & ", " & vbCrLf & " '" & tIsSuppBill & "'," & Val(txtModvatNo.Text) & ", " & Val(txtSTRefundNo.Text) & ", " & vbCrLf & " '" & tIsCapital & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(tImpPartyCode) & "', " & IIf(Val(txtImpMRRNo.Text) = 0 Or Trim(txtImpMRRNo.Text) = "", "Null", Val(txtImpMRRNo.Text)) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtImpBillNo.Text) & "', TO_DATE('" & VB6.Format(txtImpBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(tExpPartyCode) & "', " & IIf(Val(txtExpBillNo.Text) = 0 Or Trim(txtExpBillNo.Text) = "", "Null", Val(txtExpBillNo.Text)) & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtExpBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'N', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','H', " & vbCrLf & " TO_DATE('" & VB6.Format(tExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & tISMODVAT & "','" & tIsPLA & "','" & tIsSTClaim & "','" & tIsServtaxClaim & "'," & vbCrLf & "'" & tIsServTaxRefund & "'," & Val(txtServNo.Text) & ",'" & tPLFlag & "', " & vbCrLf & " " & IIf(tServiceCode = -1, "NULL", tServiceCode) & ", " & Val(txtServiceOn.Text) & ", " & Val(txtServiceTaxPer.Text) & ", " & vbCrLf & " " & Val(txtServiceTaxAmount.Text) & ", " & Val(txtProviderPer.Text) & ", " & Val(txtRecipientPer.Text) & "," & vbCrLf & " '" & tReverseChargeApp & "', '" & lblReversalMade.Text & "','" & lblReversalVoucher.Text & "','" & lblReversalMkey.Text & "')"
        '    'ElseIf MODIFYMode = True Then
        '    '    SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf & " Vdate=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " VType= '" & tVType & "'," & vbCrLf & " VnoPrefix='" & tVNoPrefix & "', " & vbCrLf & " VnoSeq=" & Val(tVNo) & ", " & vbCrLf & " VnoSuffix='" & tVNoSuffix & "', " & vbCrLf & " Vno='" & tVnoStr & "', " & vbCrLf & " BookCode='" & tBookCode & "', " & vbCrLf & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf & " CANCELLED='" & tCancelled & "', " & vbCrLf & " BookType='" & tBookType & "', " & vbCrLf & " BookSubType='" & tBookSubType & "', " & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " ISTDSDEDUCT='" & tISTDSDEDUCT & "'," & vbCrLf & " TDSPER=" & Val(txtJVTDSRate.Text) & ", " & vbCrLf & " TDSAMOUNT=" & Val(txtJVTDSAmount.Text) & ", " & vbCrLf & " REVERSE_CHARGE_APP='" & tReverseChargeApp & "', "
        '    '    SqlStr = SqlStr & vbCrLf & " ISESIDEDUCT='" & tISESIDEDUCT & "'," & vbCrLf & " ESIPER=" & Val(txtESIRate.Text) & ", " & vbCrLf & " ESIAMOUNT=" & Val(txtESIAmount.Text) & ", " & vbCrLf & " ISSTDSDEDUCT='" & tISSTDSDEDUCT & "'," & vbCrLf & " STDSPER=" & Val(txtSTDSRate.Text) & ", " & vbCrLf & " STDSAMOUNT=" & Val(txtSTDSAmount.Text) & ", " & vbCrLf & " ISSUPPBILL='" & tIsSuppBill & "', ISCAPITAL='" & tIsCapital & "'," & vbCrLf & " MODVATNO=" & Val(txtModvatNo.Text) & ",  " & vbCrLf & " STREFUNDNO=" & Val(txtSTRefundNo.Text) & ", " & vbCrLf & " IMP_SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(tImpPartyCode) & "'," & vbCrLf & " IMP_MRR_NO=" & IIf(Val(txtImpMRRNo.Text) = 0 Or Trim(txtImpMRRNo.Text) = "", "Null", Val(txtImpMRRNo.Text)) & "," & vbCrLf & " IMP_BILL_NO='" & MainClass.AllowSingleQuote(txtImpBillNo.Text) & "'," & vbCrLf & " IMP_BILL_DATE=TO_DATE('" & VB6.Format(txtImpBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " EXP_SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(tExpPartyCode) & "'," & vbCrLf & " EXP_BILL_NO=" & IIf(Val(txtExpBillNo.Text) = 0 Or Trim(txtExpBillNo.Text) = "", "Null", Val(txtExpBillNo.Text)) & "," & vbCrLf & " EXP_BILL_DATE=TO_DATE('" & VB6.Format(txtExpBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " EXPDATE=TO_DATE('" & VB6.Format(tExpDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), PL_FLAG='" & tPLFlag & "',"
        '    '    SqlStr = SqlStr & vbCrLf & " ISMODVAT='" & tISMODVAT & "'," & vbCrLf & " ISPLA='" & tIsPLA & "'," & vbCrLf & " ISSTCLAIM='" & tIsSTClaim & "'," & vbCrLf & " ISSERVTAXCLAIM='" & tIsServtaxClaim & "'," & vbCrLf & " ISSERVTAXREFUND='" & tIsServTaxRefund & "'," & vbCrLf & " SERVNO=" & Val(txtServNo.Text) & ","
        '    '    SqlStr = SqlStr & vbCrLf & " SERVICE_CODE=" & IIf(tServiceCode = -1, "NULL", tServiceCode) & "," & vbCrLf & " SERVICE_ON_AMT=" & Val(txtServiceOn.Text) & "," & vbCrLf & " SERVICE_TAX_PER=" & Val(txtServiceTaxPer.Text) & "," & vbCrLf & " SERVICE_TAX_AMOUNT=" & Val(txtServiceTaxAmount.Text) & "," & vbCrLf & " SERV_PROVIDER_PER=" & Val(txtProviderPer.Text) & "," & vbCrLf & " SERV_RECIPIENT_PER=" & Val(txtRecipientPer.Text) & ", " & vbCrLf & " IS_REVERSAL_MADE='" & lblReversalMade.Text & "', IS_REVERSAL_VOUCHER='" & lblReversalVoucher.Text & "', REVERSAL_MKEY='" & lblReversalMkey.Text & "'"
        '    '    SqlStr = SqlStr & vbCrLf & " Where Mkey='" & CurMKey & "'"
        'End If
        'PubDBCn.Execute(SqlStr)
        ''If UpdateDetail(CurMKey, mRowNo, tBookCode, tVType, tVnoStr, (TxtVDate.Text), (txtNarration.Text), PubDBCn) = False Then GoTo ErrPart
        ''If lblBookType.Text = ConJournal Or lblBookType.Text = ConContra Then
        'If lblBookType.Text = ConJournal Then
        '    If ClearJVNo(tVnoStr) = False Then GoTo ErrPart
        '    If UpDateSuppBill(tVnoStr) = False Then GoTo ErrPart
        'End If
        UpdateAutoJvMain = True
        Exit Function
ErrPart:
        '    Resume										
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateAutoJvMain = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function UpdateEMIDetail(ByRef mMkey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNo As String, ByRef mVDate As String, ByRef pNarration As String, ByRef pNoOfEMI As Integer, ByRef mTotalNoOfEMI As Integer, ByRef pDBCn As ADODB.Connection) As Boolean
        On Error GoTo ErrDetail
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mAccountName As String = ""
        Dim mAccountCode As String = ""
        Dim mChequeNo As String = ""
        Dim mChqDate As String = ""
        Dim mAmount As Double
        Dim mCCCode As String = ""
        Dim mExpCode As String = ""
        Dim mDeptCode As String = ""
        Dim mEmpCode As String = ""
        Dim mIBRNo As String = ""
        Dim mDC As String
        Dim mRemarks As String
        Dim mPRRowNo As Integer
        Dim mSubRowNo As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mClearDate As String
        Dim mParticulars As String
        Dim VMkey As String
        Dim mIsFixedAssets As String
        Dim mSameVNo As Boolean
        Dim mDivisionCode As Double
        mBookType = VB.Left(lblBookType.Text, 1)
        mBookSubType = VB.Right(lblBookType.Text, 1)
        mSameVNo = False
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColAccountName
                mAccountName = Trim(.Text)
                .Col = 0
                If mAccountName <> "" Then
                    .Col = ColPRRowNo
                    mPRRowNo = Val(.Text)
                    mSubRowNo = mPRRowNo
                    .Col = ColDC
                    mDC = UCase(VB.Left(.Text, 1))
                    .Col = ColAccountName
                    mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)
                    If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "ISFIXASSETS", "FIN_INVTYPE_MST", pDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                        mIsFixedAssets = MasterNo
                    Else
                        mIsFixedAssets = "N"
                    End If
                    .Col = ColParticulars
                    mParticulars = "INSTALLATION NO. : " & pNoOfEMI + 1 & "/" & mTotalNoOfEMI '' IIf(Trim(.Text) = "", pNarration, Trim(.Text))										
                    .Col = ColAmount
                    mAmount = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, 0, Val(.Text))
                    .Col = ColChequeNo
                    mChequeNo = IIf(Trim(.Text) = "", "", Val(.Text) + pNoOfEMI)
                    .Col = ColChequeDate
                    mChqDate = IIf(mChequeNo = "", mVDate, mVDate)
                    .Col = ColCC
                    mCCCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "CC_CODE", "CC_CODE", "FIN_CCENTER_HDR", pDBCn, mCCCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mCCCode, -1)
                    .Col = ColExp
                    mExpCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "COST_CENTER_CODE", "COST_CENTER_CODE", "CST_CENTER_MST", pDBCn, mExpCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mExpCode, -1)
                    .Col = ColDept
                    mDeptCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", pDBCn, mDeptCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mDeptCode, -1)
                    .Col = ColDivisionCode
                    mDivisionCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "DIV_CODE", "DIV_CODE", "INV_DIVISION_MST", pDBCn, mDivisionCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mDivisionCode, -1)
                    .Col = ColEmp
                    mEmpCode = IIf(MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", pDBCn, mEmpCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mEmpCode, -1)
                    .Col = ColIBRNo
                    mIBRNo = .Text

                    .Col = ColClearDate
                    mClearDate = .Text

                    SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & ",  " & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & mSubRowNo & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
                    PubDBCn.Execute(SqlStr)
                    If UpdateEMIPRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, mParticulars, IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), VB.Left(lblBookType.Text, 1), VB.Right(lblBookType.Text, 1), VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, (lblAddUser.Text), (lblAddDate.Text), mDivisionCode) = False Then GoTo ErrDetail
                    If (lblBookType.Text = ConBankPayment Or lblBookType.Text = ConPDCPayment) And mChequeNo <> "" Then
                        VMkey = mMkey
                        If UpdateChequeDetail(mChequeNo, VMkey, "C", mSameVNo) = False Then GoTo ErrDetail
                    End If
                End If
                mSameVNo = True
            Next I
        End With
        UpdateEMIDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateEMIDetail = False
        '    Resume										
    End Function
    Private Function UpdateEMIPRDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pRowNo As Integer, ByRef pTRNDtlSubRow As Integer, ByRef pAccountCode As String, ByRef pBookCode As String, ByRef pVType As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pTrnDC As String, ByRef pTrnAmount As Double, ByRef pChequeNo As String, ByRef pChqDate As String, ByRef pCostCCode As String, ByRef pDeptCode As String, ByRef pEmpCode As String, ByRef pExpCode As String, ByRef pIBRNo As String, ByRef pClearDate As String, ByRef pLocked As String, ByRef pNarration As String, ByRef xRemarks As String, ByRef pCancelled As String, ByRef pOldBookType As String, ByRef pOldBookSubType As String, ByRef pExpDate As String, ByRef pAddMode As Boolean, ByRef pAddUser As String, ByRef pAddDate As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrDetail
        Dim RsTempPRDetail As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim pTRNType As String
        Dim pBillNo As String
        Dim pBillDate As String
        Dim pBillAmount As Double
        Dim pBillDC As String
        Dim pAmount As Double
        Dim pDC As String
        Dim pBillType As String
        Dim pSubRowNo As Integer
        Dim pRemarks As String
        Dim pDueDate As String
        Dim pSTTYPE As String
        Dim pSTFORMNAME As String
        Dim pSTFORMNO As String
        Dim pSTFORMDATE As String
        Dim pSTFORMAMT As Double
        Dim pSTDUEFORMNAME As String
        Dim pSTDUEFORMNO As String
        Dim pSTDUEFORMDATE As String
        Dim pSTDUEFORMAMT As Double
        Dim pISREGDNO As String
        Dim pSTFORMCODE As Integer
        Dim pSTDUEFORMCODE As Integer
        Dim pTaxableAmount As Double
        Dim pPONO As String
        Dim mDivCode As Double
        Dim mLocCode As String

        pSubRowNo = 1000 * pRowNo
        If pCancelled = "Y" Then
            pTrnAmount = 0
        End If
        If GetAccountBalancingMethod(pAccountCode, True) = "S" Then
            GoTo SummariedPart
        End If
        SqlStr = " Select * From FIN_TEMPBILL_TRN  " & vbCrLf & " WHERE UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " AND TRNDtlSubRowNo=" & pTRNDtlSubRow & "" & vbCrLf & " AND AccountCode='" & pAccountCode & "'" & vbCrLf & " AND BookType='" & pOldBookType & pOldBookSubType & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPRDetail, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTempPRDetail.EOF = False Then
            '        pSubRowNo = 0										
            Do While RsTempPRDetail.EOF = False
                pSubRowNo = pSubRowNo + 1
                pTRNType = RsTempPRDetail.Fields("TrnType").Value
                pBillNo = RsTempPRDetail.Fields("BillNo").Value
                pBillDate = pVDate '' IIf(IsNull(RsTempPRDetail.Fields("BillDate").Value), pVDate, RsTempPRDetail.Fields("BillDate").Value)										
                pBillAmount = RsTempPRDetail.Fields("BillAmount").Value
                pBillDC = RsTempPRDetail.Fields("BillDC").Value
                pAmount = RsTempPRDetail.Fields("Amount").Value
                pDC = RsTempPRDetail.Fields("DC").Value
                pRemarks = IIf(IsDBNull(RsTempPRDetail.Fields("REMARKS").Value), "", RsTempPRDetail.Fields("REMARKS").Value)
                pDueDate = pVDate '' IIf(IsNull(RsTempPRDetail.Fields("DueDate").Value), "", RsTempPRDetail.Fields("DueDate").Value)										
                pSTFORMNAME = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMNAME").Value), "", RsTempPRDetail.Fields("STFORMNAME").Value)
                pSTFORMNO = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMNO").Value), "", RsTempPRDetail.Fields("STFORMNO").Value)
                pSTFORMDATE = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMDATE").Value), "", RsTempPRDetail.Fields("STFORMDATE").Value)
                pSTFORMAMT = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMAMT").Value), 0, RsTempPRDetail.Fields("STFORMAMT").Value)
                pSTDUEFORMNAME = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMNAME").Value), "", RsTempPRDetail.Fields("STDUEFORMNAME").Value)
                pSTDUEFORMNO = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMNO").Value), "", RsTempPRDetail.Fields("STDUEFORMNO").Value)
                pSTDUEFORMDATE = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMDATE").Value), "", RsTempPRDetail.Fields("STDUEFORMDATE").Value)
                pSTDUEFORMAMT = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMAMT").Value), 0, RsTempPRDetail.Fields("STDUEFORMAMT").Value)
                pSTFORMCODE = IIf(IsDBNull(RsTempPRDetail.Fields("STFORMCODE").Value), -1, RsTempPRDetail.Fields("STFORMCODE").Value)
                pSTDUEFORMCODE = IIf(IsDBNull(RsTempPRDetail.Fields("STDUEFORMCODE").Value), -1, RsTempPRDetail.Fields("STDUEFORMCODE").Value)
                pSTTYPE = IIf(IsDBNull(RsTempPRDetail.Fields("STTYPE").Value), "", RsTempPRDetail.Fields("STTYPE").Value)
                pISREGDNO = IIf(IsDBNull(RsTempPRDetail.Fields("ISREGDNO").Value), "", RsTempPRDetail.Fields("ISREGDNO").Value)
                pTaxableAmount = IIf(IsDBNull(RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value), 0, RsTempPRDetail.Fields("TAXABLE_AMOUNT").Value)
                pPONO = IIf(IsDBNull(RsTempPRDetail.Fields("PONO").Value), "", RsTempPRDetail.Fields("PONO").Value)
                mDivCode = IIf(IsDBNull(RsTempPRDetail.Fields("DIV_CODE").Value), 1, RsTempPRDetail.Fields("DIV_CODE").Value)
                mLocCode = IIf(IsDBNull(RsTempPRDetail.Fields("BILL_TO_LOC_ID").Value), 1, RsTempPRDetail.Fields("BILL_TO_LOC_ID").Value)
                SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf _
                    & " MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf _
                    & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf _
                    & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE, " & vbCrLf _
                    & " STFORMNAME, STFORMNO, " & vbCrLf & " STFORMDATE, STFORMAMT, " & vbCrLf _
                    & " STDUEFORMNAME, STDUEFORMNO, " & vbCrLf _
                    & " STDUEFORMDATE, STDUEFORMAMT, " & vbCrLf _
                    & " STFORMCODE, STDUEFORMCODE, " & vbCrLf _
                    & " STTYPE, ISREGDNO, TAXABLE_AMOUNT,PONO,DIV_CODE,REF_NO,BILL_TO_LOC_ID,COMPANY_CODE, BOOKTYPE,BILL_COMPANY_CODE " & vbCrLf _
                    & " ) VALUES ( "
                SqlStr = SqlStr & vbCrLf _
                    & " '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf _
                    & " '" & pAccountCode & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(pSTFORMNAME) & "', '" & MainClass.AllowSingleQuote(pSTFORMNO) & "', " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(pSTFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTFORMAMT)) & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(pSTDUEFORMNAME) & "', '" & MainClass.AllowSingleQuote(pSTDUEFORMNO) & "', " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(pSTDUEFORMDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(pSTDUEFORMAMT)) & ", " & vbCrLf _
                    & " " & Val(CStr(pSTFORMCODE)) & ", " & Val(CStr(pSTDUEFORMCODE)) & ", " & vbCrLf _
                    & " '" & pSTTYPE & "', '" & pISREGDNO & "', " & Val(CStr(pTaxableAmount)) & ", '" & pPONO & "'," & mDivCode & ",''," & vbCrLf _
                    & " '" & mLocCode & "', " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & VB.Left(lblBookType.Text, 1) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ") "
                pDBCn.Execute(SqlStr)
                If pTRNType = "N" Then
                    pBillType = "B"
                ElseIf pTRNType = "B" Or pTRNType = "O" Or pTRNType = "A" Then
                    pBillType = "P"
                Else
                    pBillType = pTRNType
                End If
                pRemarks = IIf(Trim(pRemarks) = "", xRemarks, pRemarks & vbNewLine & xRemarks)
                If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, pExpDate, pAddMode, pAddUser, pAddDate, mDivCode, mLocCode) = False Then GoTo ErrDetail
                RsTempPRDetail.MoveNext()
            Loop
        Else
SummariedPart:
            pTRNType = "O"
            pBillNo = pVNo
            pBillDate = pVDate
            pBillAmount = pTrnAmount
            pBillDC = pTrnDC
            pAmount = pTrnAmount
            pDC = pTrnDC
            pRemarks = xRemarks
            pBillType = "P"
            pDueDate = pBillDate
            mLocCode = GetDefaultLocation(pAccountCode)
            If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, "", pExpDate, pAddMode, pAddUser, pAddDate, mDivisionCode, mLocCode) = False Then GoTo ErrDetail
        End If
        UpdateEMIPRDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateEMIPRDetail = False
        '    Resume										
    End Function
    Private Function UpdateFixedAsset(ByRef mMkey As String, ByRef mVNo As String, ByRef mVDate As String, ByRef xAccountCode As String, ByRef mAmount As Double, ByRef mDC As String, ByRef pNarration As String) As Boolean
        On Error GoTo ErrDetail
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAccountName As String
        Dim mPRRowNo As Integer
        Dim mSuppCode As String = ""
        Dim mBalAmount As Double
        Dim mBillAmount As Double
        Dim mBillDC As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mTRNType As String
        mBalAmount = mAmount * IIf(mDC = "D", 1, -1)
        If MainClass.ValidateWithMasterTable(xAccountCode, "ACCOUNTPOSTCODE", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
            mTRNType = MasterNo
        Else
            mTRNType = "-1"
        End If
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColAccountName
                mAccountName = Trim(.Text)
                .Col = 0
                If mAccountName <> "" Then
                    .Col = ColPRRowNo
                    mPRRowNo = Val(.Text)
                    .Col = ColDC
                    mDC = UCase(VB.Left(.Text, 1))
                    mSuppCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, mSuppCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True, mSuppCode, -1)
                    SqlStr = "SELECT BILLNO, BILLDATE,BILLAMOUNT,BILLDC, AMOUNT, DC" & vbCrLf & " FROM FIN_TEMPBILL_TRN " & vbCrLf & " WHERE " & vbCrLf & " UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " AND ACCOUNTCODE='" & mSuppCode & "'" & vbCrLf & " AND TRNDtlSubRowNo=" & mPRRowNo & ""
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        Do While RsTemp.EOF = False
                            mBillDC = IIf(IsDBNull(RsTemp.Fields("DC").Value), "D", RsTemp.Fields("DC").Value)
                            mBillAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * IIf(mBillDC = "D", -1, 1)
                            mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), 0, RsTemp.Fields("BILLNO").Value)
                            mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), 0, RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                            mBalAmount = mBalAmount - mBillAmount
                            If UpdateAssetTRN(mMkey, RsCompany.Fields("FYEAR").Value, mTRNType, "N", "N", 0, VB6.Format(mVDate, "DD-MMM-YYYY"), Trim(mBillNo), VB6.Format(mBillDate, "DD-MMM-YYYY"), Trim(mVNo), VB6.Format(mVDate, "DD-MMM-YYYY"), VB.Left(lblBookType.Text, 1), Trim(mAccountName), Trim(pNarration), mBillAmount, Val(CStr(mBillAmount)), 0, 0, 0, "Y", "", 0, 0, 0) = False Then GoTo ErrDetail
                            RsTemp.MoveNext()
                        Loop
                    End If
                End If
            Next I
            If mBalAmount <> 0 Then
                If UpdateAssetTRN(mMkey, RsCompany.Fields("FYEAR").Value, mTRNType, "N", "N", 0, VB6.Format(mVDate, "DD-MMM-YYYY"), Trim(mVNo), VB6.Format(mVDate, "DD-MMM-YYYY"), Trim(mVNo), VB6.Format(mVDate, "DD-MMM-YYYY"), VB.Left(lblBookType.Text, 1), "", Trim(pNarration), mBalAmount, Val(CStr(mBalAmount)), 0, 0, 0, "Y", "", 0, 0, 0) = False Then GoTo ErrDetail
            End If
        End With
        UpdateFixedAsset = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateFixedAsset = False
        'Resume										
    End Function
    Private Sub GetNewBook(ByRef mNewBookType As String, ByRef mNewBookSubType As String, ByRef mNewVType As String)
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        If lblBookType.Text = ConPDCPayment Then
            mNewBookType = VB.Left(ConBankPayment, 1)
            mNewBookSubType = VB.Right(ConBankPayment, 1)
        ElseIf lblBookType.Text = ConPDCReceipt Then
            mNewBookType = VB.Left(ConBankReceipt, 1)
            mNewBookSubType = VB.Right(ConBankReceipt, 1)
        Else
            mNewBookType = VB.Left(lblBookType.Text, 1)
            mNewBookSubType = VB.Right(lblBookType.Text, 1)
        End If
        SqlStr = "SELECT VTYPE FROM FIN_VOUCHERTYPE_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & ConBankBook & "'" & vbCrLf _
            & " AND VNAME='" & MainClass.AllowSingleQuote(Trim(txtPartyName.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            mNewVType = IIf(IsDBNull(RS.Fields("VTYPE").Value), "", RS.Fields("VTYPE").Value)
        End If
    End Sub
    Private Function GetChequeStatus(ByRef mChequeNo As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mBankCode As String
        Dim mChequeStatus As String
        Dim mVMkey As String
        If Trim(TxtVDate.Text) = "" Then GetChequeStatus = True : Exit Function
        If IsDate(TxtVDate.Text) Then
            If CDate(TxtVDate.Text) < CDate("01/07/2008") Then
                GetChequeStatus = True : Exit Function
            End If
        End If
        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master.")
            GetChequeStatus = False
            Exit Function
        End If
        SqlStr = "SELECT CHEQUE_STATUS,VMKEY FROM FIN_CHEQUE_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BANKCODE='" & mBankCode & "'" & vbCrLf _
            & " AND CHEQUE_NO='" & MainClass.AllowSingleQuote(Trim(mChequeNo)) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            mChequeStatus = IIf(IsDBNull(RS.Fields("CHEQUE_STATUS").Value), "C", RS.Fields("CHEQUE_STATUS").Value)
            mVMkey = IIf(IsDBNull(RS.Fields("VMkey").Value), "", RS.Fields("VMkey").Value)
            If mChequeStatus = "O" Then
                GetChequeStatus = True
            Else
                If mVMkey = Trim(CurMKey) Then
                    GetChequeStatus = True
                Else
                    MsgBox("Cheque No for such Bank Already Issue.")
                    GetChequeStatus = False
                End If
            End If
        Else
            MsgBox("No Cheque Allocated for such Bank.")
            GetChequeStatus = False
        End If
        Exit Function
ErrPart:
        GetChequeStatus = False
    End Function
    Private Function UpdateChequeDetail(ByRef mChequeNo As String, ByRef VMkey As String, ByRef mChqStatus As String, ByRef mSameVNo As Boolean) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mBankCode As String
        Dim pVMkey As String
        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = True Then
            mBankCode = MasterNo
        Else
            MsgBox("Bank Does Not Exist In Master.")
            UpdateChequeDetail = False
            Exit Function
        End If
        pVMkey = IIf(mChqStatus = "C", VMkey, "")
        If VMkey <> "" And mSameVNo = False Then
            SqlStr = "UPDATE FIN_CHEQUE_MST SET " & vbCrLf & " CHEQUE_STATUS='O'," & vbCrLf & " VMKEY=''," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BANKCODE='" & mBankCode & "'" & vbCrLf & " AND VMKEY='" & MainClass.AllowSingleQuote(Trim(VMkey)) & "'"
            PubDBCn.Execute(SqlStr)
        End If
        SqlStr = "UPDATE FIN_CHEQUE_MST SET " & vbCrLf & " CHEQUE_STATUS='" & mChqStatus & "'," & vbCrLf & " VMKEY='" & pVMkey & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BANKCODE='" & mBankCode & "'" & vbCrLf & " AND CHEQUE_NO='" & MainClass.AllowSingleQuote(Trim(mChequeNo)) & "'"
        PubDBCn.Execute(SqlStr)
        UpdateChequeDetail = True
        Exit Function
ErrPart:
        UpdateChequeDetail = False
    End Function
    Private Sub txtVNo1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo1.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVNo1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVNoSuffix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNoSuffix.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNoSuffix_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNoSuffix.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVNoSuffix.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVNoSuffix_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNoSuffix.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call txtVNo_Validating(txtVno, New System.ComponentModel.CancelEventArgs(False))
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.DoubleClick
        SearchVType()
    End Sub
    Private Function CheckVType() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        If ConOnlineData = True Then
            CheckVType = True
            Exit Function
        End If
        CheckVType = False
        SqlStr = "SELECT VTYPE,VNAME FROM FIN_VOUCHERTYPE_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & "" & vbCrLf _
            & " AND VTYPE='" & MainClass.AllowSingleQuote(Trim(txtVType.Text)) & "'" & vbCrLf & " ORDER BY VTYPE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            CheckVType = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckVType = False
    End Function
    Private Sub txtVType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function GetVType(Optional ByRef mChkVtype As String = "") As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        SqlStr = "SELECT VTYPE,VNAME FROM FIN_VOUCHERTYPE_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '    If lblBookType.text = ConPDCPayment Or lblBookType.text = ConPDCReceipt Then										
        '        SqlStr = SqlStr & " AND BOOKTYPE='" & vb.Left(ConBankPayment, 1) & "'"										
        '    Else										
        SqlStr = SqlStr & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"
        '    End If										
        If mChkVtype <> "" Then
            SqlStr = SqlStr & " AND VTYPE='" & MainClass.AllowSingleQuote(Trim(mChkVtype)) & "'"
        End If

        SqlStr = SqlStr & "ORDER BY VTYPE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            GetVType = ""
        Else
            GetVType = IIf(IsDBNull(RS.Fields("VTYPE").Value), "", RS.Fields("VTYPE").Value)
            If VB.Left(lblBookType.Text, 1) = "C" Or VB.Left(lblBookType.Text, 1) = "B" Or VB.Left(lblBookType.Text, 1) = "F" Then
                txtPartyName.Text = IIf(IsDBNull(RS.Fields("VNAME").Value), "", RS.Fields("VNAME").Value)
            End If
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetVType = ""
    End Function
    Private Sub txtVType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVType()
    End Sub
    Private Sub SearchVType()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' AND FOR_HO='O'"
        '    If ConOnlineData = False Then										
        'SqlStr = SqlStr & vbCrLf & " AND FOR_HO='" & PubHO & "'"
        '    End If										
        If MainClass.SearchGridMaster((txtVType.Text), "FIN_VOUCHERTYPE_MST", "VTYPE", "VNAME", , , SqlStr) = True Then
            txtVType.Text = AcName
            '        txtTariff_Validate False										
            If txtVType.Enabled = True Then txtVType.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtVType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If GetVType(Trim(txtVType.Text)) = "" Then
            Cancel = True
            MsgInformation("Invalid Voucher Type")
            GoTo EventExitSub
        Else
            txtVType.Text = GetVType(Trim(txtVType.Text))
        End If
        If lblBookType.Text = ConBankReceipt Or lblBookType.Text = ConBankPayment Then
            If CheckPendingPDC() = True Then
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function GETDRCRNo(ByRef lAccountCode As String) As String
        On Error GoTo ErrPart
        Dim pCrNo As String = ""
        Dim pDrNo As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mVNo As String
        Dim mBookCode As String
        'Dim lAccountCode As String										
        Dim pVType As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mDrVno As String
        mVNo = txtVType.Text & txtVNo1.Text & txtVno.Text & txtVNoSuffix.Text
        If Me.lblBookType.Text = ConJournal Then
            mBookCode = CStr(ConJournalBookCode)
        ElseIf Me.lblBookType.Text = ConContra Then
            mBookCode = CStr(ConContraBookCode)
        Else
            MainClass.ValidateWithMasterTable(Me.txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mBookCode = MasterNo
        End If
        '        SprdMain.Row = 1										
        '        SprdMain.Col = ColAccountName										
        '        MainClass.ValidateWithMasterTable SprdMain.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""										
        '        lAccountCode = MasterNo										
        ''TRN.FYEAR=" & RsCompany.fields("FYEAR").value & "										
        pSqlStr = " SELECT BILLNO, BILLDATE FROM FIN_POSTED_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND  FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND VNO='" & mVNo & "' AND " & vbCrLf & " VDate=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf & " BookType='" & Mid(lblBookType.Text, 1, 1) & "' AND " & vbCrLf & " BookSubType='" & Mid(lblBookType.Text, 2, 1) & "' AND " & vbCrLf & " BOOKCODE='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "' ORDER BY BILLDATE,BILLNO"
        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                SqlStr = " SELECT DISTINCT TRN.VNO,VTYPE " & vbCrLf & " FROM FIN_POSTED_TRN TRN " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.BookType ='" & ConDebitNoteBook & "'" & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(lAccountCode) & "' " & vbCrLf & " AND TRN.BILLNO='" & Trim(mBillNo) & "'" & vbCrLf & " AND TRN.BILLDATE =TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
                If RS.EOF = False Then
                    Do While RS.EOF = False
                        mDrVno = IIf(IsDBNull(RS.Fields("VNO").Value), "", Mid(RS.Fields("VNO").Value, 3))
                        If InStr(1, pDrNo, mDrVno) = 0 Then
                            pDrNo = IIf(pDrNo = "", "", pDrNo & ", ") & mDrVno
                        End If
                        RS.MoveNext()
                    Loop
                End If
                SqlStr = " SELECT DISTINCT TRN.VNO,VTYPE " & vbCrLf & " FROM FIN_POSTED_TRN TRN " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.BookType ='" & ConCreditNoteBook & "'" & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(lAccountCode) & "' " & vbCrLf & " AND TRN.BILLNO='" & Trim(mBillNo) & "'" & vbCrLf & " AND TRN.BILLDATE =TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
                If RS.EOF = False Then
                    Do While RS.EOF = False
                        mDrVno = IIf(IsDBNull(RS.Fields("VNO").Value), "", Mid(RS.Fields("VNO").Value, 3))
                        If InStr(1, pCrNo, mDrVno) = 0 Then
                            pCrNo = IIf(pCrNo = "", "", pCrNo & ", ") & mDrVno
                        End If
                        RS.MoveNext()
                    Loop
                End If
                RsTemp.MoveNext()
            Loop
        End If
        GETDRCRNo = "DR" & pDrNo
        If pCrNo <> "" Then
            GETDRCRNo = GETDRCRNo & ", CR" & pCrNo
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GETDRCRNo = ""
    End Function
    Private Function GETDNCNNoInVoucher() As String
        On Error GoTo ErrPart
        Dim pDrCrNo As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mVNo As String
        Dim mBookCode As String
        Dim lAccountCode As String
        Dim pVType As String
        mVNo = txtVType.Text & txtVNo1.Text & txtVno.Text & txtVNoSuffix.Text
        If Me.lblBookType.Text = ConJournal Then
            mBookCode = CStr(ConJournalBookCode)
        ElseIf Me.lblBookType.Text = ConContra Then
            mBookCode = CStr(ConContraBookCode)
        Else
            MainClass.ValidateWithMasterTable(Me.txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mBookCode = MasterNo
        End If
        SprdMain.Row = 1
        SprdMain.Col = ColAccountName
        MainClass.ValidateWithMasterTable(SprdMain.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        lAccountCode = MasterNo
        ''TRN.FYEAR=" & RsCompany.fields("FYEAR").value & " AND										
        SqlStr = " SELECT DISTINCT TRN.VNO||':'||TRN.FYEAR AS VNO" & vbCrLf & " FROM FIN_POSTED_TRN TRN " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND (TRN.BookType='" & ConDebitNoteBook & "' " & vbCrLf & " OR TRN.BookType='" & ConCreditNoteBook & "') AND " & vbCrLf & " AccountCode='" & lAccountCode & "' AND TRN.BILLNO||TRN.BILLDATE IN (SELECT BILLNO||BILLDATE FROM FIN_POSTED_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND  " & vbCrLf & " VNO='" & mVNo & "' AND " & vbCrLf & " VDate=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND " & vbCrLf & " BookType='" & Mid(lblBookType.Text, 1, 1) & "' AND " & vbCrLf & " BookSubType='" & Mid(lblBookType.Text, 2, 1) & "' AND " & vbCrLf & " BOOKCODE='" & mBookCode & "' AND " & vbCrLf & " AccountCode<>'" & mBookCode & "') "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While RS.EOF = False
                pDrCrNo = IIf(pDrCrNo = "", "", pDrCrNo & ", ") & IIf(IsDBNull(RS.Fields("VNO").Value), "", "'" & RS.Fields("VNO").Value & "'")
                RS.MoveNext()
            Loop
        End If
        GETDNCNNoInVoucher = pDrCrNo
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GETDNCNNoInVoucher = ""
    End Function
    Private Function GetTransInTDS(ByRef xMKey As String, ByRef xBookType As String, ByRef xBookSubType As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        GetTransInTDS = False
        SqlStr = " SELECT *  FROM TDS_TRN " & vbCrLf & " WHERE MKey= '" & xMKey & "'" & vbCrLf & " AND BookType='" & VB.Left(lblBookType.Text, 1) & "' AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            GetTransInTDS = True
        End If
        Exit Function
ErrPart:
        GetTransInTDS = True
    End Function
    Private Function UpDateSuppBill(ByRef xVnoStr As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mFinalPost As String
        Dim mISCST As String
        Dim mPartyName As String
        Dim cntRow As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPrevJVNo As String
        Dim pVNoStr As String
        Dim xModvatNo As Integer
        Dim xSTRefundNo As Integer
        Dim xServNo As Integer
        Dim xIsModvat As String
        Dim xIsPLA As String
        Dim xIsSTClaim As String
        Dim pISCapital As String
        Dim xIsServTaxClaim As String
        Dim xIsServTaxRefund As String
        xModvatNo = Val(txtModvatNo.Text)
        xSTRefundNo = Val(txtSTRefundNo.Text)
        xServNo = Val(txtServNo.Text)
        xIsModvat = IIf(chkModvat.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        xIsPLA = IIf(chkPLA.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        xIsSTClaim = IIf(chkSTClaim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        pISCapital = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        xIsServTaxClaim = IIf(chkServTaxClaim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        xIsServTaxRefund = IIf(chkServTaxRefund.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        With SprdMain
            mISCST = "N"
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAccountName
                mPartyName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND WITHIN_STATE='N' AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                    mISCST = "Y"
                    Exit For
                End If
            Next
        End With
        pVNoStr = xVnoStr
        mFinalPost = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "N", "Y")
        xVnoStr = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "", xVnoStr)
        If xModvatNo <> 0 Then
            SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MODVATNO=" & xModvatNo & " AND ISCAPITAL='" & pISCapital & "'" & vbCrLf & " AND ISMODVAT='Y' AND ISPLA='" & xIsPLA & "' AND VNO='-1'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Not Such Modvat Entry.")
                UpDateSuppBill = False
                Exit Function
            End If
            SqlStr = " SELECT JVNO FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MODVATNO=" & xModvatNo & " AND ISCAPITAL='" & pISCapital & "'" & vbCrLf & " AND ISMODVAT='Y' AND ISPLA='" & xIsPLA & "' AND VNO='-1'" & vbCrLf & " AND JVNO<>'" & pVNoStr & "' AND JVNO IS NOT NULL"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mPrevJVNo = IIf(IsDBNull(RsTemp.Fields("JVNO").Value), "", RsTemp.Fields("JVNO").Value)
                MsgInformation("Modvat Entry Already Save Agt. JVno " & mPrevJVNo)
                UpDateSuppBill = False
                Exit Function
            End If
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='" & mFinalPost & "',JVNO='" & xVnoStr & "'," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MODVATNO=" & xModvatNo & " AND ISCAPITAL='" & pISCapital & "'" & vbCrLf & " AND ISMODVAT='Y' AND ISPLA='" & xIsPLA & "' AND VNO='-1'"
            PubDBCn.Execute(SqlStr)
        End If
        If xServNo <> 0 Then
            SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SERVNO=" & xServNo & " AND SERVICE_REFUND='" & xIsServTaxRefund & "'" & vbCrLf & " AND ISSERVCLAIM='Y' AND ISPLA='" & xIsPLA & "' AND VNO='-1'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Not Such Modvat Entry.")
                UpDateSuppBill = False
                Exit Function
            End If
            SqlStr = " SELECT JVNO FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SERVNO=" & xServNo & " AND SERVICE_REFUND='" & xIsServTaxRefund & "'" & vbCrLf & " AND ISSERVCLAIM='Y' AND ISPLA='" & xIsPLA & "' AND VNO='-1'" & vbCrLf & " AND JVNO<>'" & pVNoStr & "' AND JVNO IS NOT NULL"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mPrevJVNo = IIf(IsDBNull(RsTemp.Fields("JVNO").Value), "", RsTemp.Fields("JVNO").Value)
                MsgInformation("Modvat Entry Already Save Agt. JVno " & mPrevJVNo)
                UpDateSuppBill = False
                Exit Function
            End If
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='" & mFinalPost & "',JVNO='" & xVnoStr & "'," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SERVNO=" & xServNo & " AND SERVICE_REFUND='" & xIsServTaxRefund & "'" & vbCrLf & " AND ISSERVCLAIM='Y' AND ISPLA='" & xIsPLA & "' AND VNO='-1'"
            PubDBCn.Execute(SqlStr)
        End If
        If xSTRefundNo <> 0 Then
            SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STCLAIMNO=" & xSTRefundNo & " AND VNO='-1' "
            If mISCST = "N" Then
                SqlStr = SqlStr & " AND ISSTREFUND='Y'"
            Else
                SqlStr = SqlStr & " AND ISCSTREFUND='Y'"
            End If
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Not Such Claim Entry.")
                UpDateSuppBill = False
                Exit Function
            End If
            SqlStr = " SELECT JVNO FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STCLAIMNO=" & xSTRefundNo & " AND VNO='-1' " & vbCrLf & " AND JVNO<>'" & pVNoStr & "' AND JVNO IS NOT NULL"
            If mISCST = "N" Then
                SqlStr = SqlStr & " AND ISSTREFUND='Y'"
            Else
                SqlStr = SqlStr & " AND ISCSTREFUND='Y'"
            End If
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mPrevJVNo = IIf(IsDBNull(RsTemp.Fields("JVNO").Value), "", RsTemp.Fields("JVNO").Value)
                MsgInformation("Claim Entry Already Save Agt. JVno " & mPrevJVNo)
                UpDateSuppBill = False
                Exit Function
            End If
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='" & mFinalPost & "',JVNO='" & xVnoStr & "'," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STCLAIMNO=" & xSTRefundNo & " AND VNO='-1' "
            If mISCST = "N" Then
                SqlStr = SqlStr & " AND ISSTREFUND='Y'"
            Else
                SqlStr = SqlStr & " AND ISCSTREFUND='Y'"
            End If
            PubDBCn.Execute(SqlStr)
        End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = "UPDATE FIN_SUPP_PURCHASE_HDR SET ISFINALPOST='N'," & vbCrLf & " JVNO='-1'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND JVMKEY='" & CurMKey & "'"
            PubDBCn.Execute(SqlStr)
        End If
        UpDateSuppBill = True
        Exit Function
ErrPart:
        UpDateSuppBill = False
    End Function
    Private Function ClearJVNo(ByRef mVnoStr As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        If xPrevModvatNo <> 0 Then
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='N',JVNO=''," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MODVATNO=" & xPrevModvatNo & " AND ISCAPITAL='" & xPrevISCapital & "' AND JVNO='" & mVnoStr & "'" & vbCrLf & " AND ISMODVAT='Y' AND ISPLA='" & xPrevISPLA & "' AND VNO='-1'"
            PubDBCn.Execute(SqlStr)
        End If
        If xPrevSTRefundNo <> 0 Then
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='N',JVNO=''," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ISSTREFUND='Y'" & vbCrLf & " AND STCLAIMNO=" & xPrevSTRefundNo & "  AND JVNO='" & mVnoStr & "' AND VNO='-1' "
            PubDBCn.Execute(SqlStr)
        End If
        If xPrevServNo <> 0 Then
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET ISFINALPOST='N',JVNO=''," & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ISSERVCLAIM='Y'" & vbCrLf & " AND SERVNO=" & xPrevServNo & "  AND ISPLA='" & xPrevISPLA & "' AND SERVICE_REFUND='" & xPrevServTaxRefund & "' AND JVNO='" & mVnoStr & "' AND VNO='-1' "
            PubDBCn.Execute(SqlStr)
        End If
        ClearJVNo = True
        Exit Function
ErrPart:
        ClearJVNo = False
    End Function
    Private Function GetVoucherNetAmount() As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If lblBookType.Text = ConJournal Or lblBookType.Text = ConContra Then
            GetVoucherNetAmount = CDbl(IIf(IsNumeric(LblDrAmt.Text), LblDrAmt.Text, 0))
        Else
            GetVoucherNetAmount = CDbl(IIf(IsNumeric(LblNetAmt.Text), LblNetAmt.Text, 0))
        End If
        Exit Function
        SqlStr = "  SELECT SUM(AMOUNT) as AMOUNT FROM FIN_POSTED_TRN " & vbCrLf & " WHERE MKEY='" & CurMKey & "'" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND BOOKSUBTYPE='" & VB.Right(lblBookType.Text, 1) & "' AND SUBROWNO=-1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetVoucherNetAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If
    End Function
    Private Sub SearchExpPartyName()
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') AND STATUS='O'"
        'MainClass.SearchMaster(txtExpPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)

        MainClass.SearchGridMaster((txtExpPartyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_Name", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtExpPartyName.Text = AcName
            txtExpPartyName_Validating(txtExpPartyName, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchImpPartyName()
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') AND STATUS='O'"
        'MainClass.SearchMaster(txtImpPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr)
        MainClass.SearchGridMaster((txtImpPartyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtImpPartyName.Text = AcName
            txtImpPartyName_Validating(txtImpPartyName.Text, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchExpBillNo()
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        Dim mExpPartyCode As String
        SqlStr = "COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If Trim(txtExpPartyName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtExpPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mExpPartyCode = MasterNo
                SqlStr = SqlStr & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(mExpPartyCode) & "'"
            End If
        End If
        MainClass.SearchGridMaster(txtExpBillNo.Text, "FIN_EXPINV_HDR", "AUTO_KEY_EXPINV", "BILLNO", "EXPINV_DATE", , SqlStr)
        If AcName <> "" Then
            txtExpBillNo.Text = AcName
            txtExpBillNo_Validating(txtExpBillNo, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
SearchErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetTDSChallanMade(ByRef pMKey As String, ByRef pChallanNo As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetTDSChallanMade = False
        SqlStr = " SELECT CHALLAN.REFNO " & vbCrLf & "  FROM TDS_TRN TRN, TDS_CHALLAN CHALLAN" & vbCrLf & "  WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & "  AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & "  AND TRN.COMPANY_CODE=CHALLAN.COMPANY_CODE" & vbCrLf & "  AND TRN.FYEAR=CHALLAN.FYEAR" & vbCrLf & "  AND TRN.CHALLANMKEY=CHALLAN.MKEY" & vbCrLf & "  AND TRN.MKEY='" & pMKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            pChallanNo = IIf(IsDbNull(RsTemp.Fields("REFNO").Value), "", RsTemp.Fields("REFNO").Value)
            GetTDSChallanMade = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetTDSChallanMade = False
    End Function
    Private Function GetServiceClaimMade(ByRef pMKey As String, ByRef pClaimNo As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetServiceClaimMade = False
        SqlStr = " SELECT SERVNO " & vbCrLf & "  FROM FIN_SERVTAXDETAILS_TRN TRN" & vbCrLf & "  WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & "  AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ISSERVICECLAIM='Y'" & vbCrLf & "  AND TRN.MKEY='" & pMKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            pClaimNo = IIf(IsDbNull(RsTemp.Fields("SERVNO").Value), "", RsTemp.Fields("SERVNO").Value)
            GetServiceClaimMade = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetServiceClaimMade = False
    End Function
    Private Function GetImpMRRNo(ByRef pMRRNo As Double, ByRef pVNo As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetImpMRRNo = False
        SqlStr = " SELECT VNO " & vbCrLf & "  FROM FIN_VOUCHER_HDR IH" & vbCrLf & "  WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & "  AND IH.IMP_MRR_NO=" & Val(CStr(pMRRNo)) & ""
        If MODIFYMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND MKEY<>" & RsTRNMain.Fields("MKEY").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            pVNo = IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
            GetImpMRRNo = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetImpMRRNo = False
    End Function
    Private Function CheckValidPartyPanNo(ByRef pPartyName As String) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPANNo As String
        Dim xAccountCode As String
        Dim xSuppCustType As String
        If MainClass.ValidateWithMasterTable(pPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAccountCode = MasterNo
        Else
            CheckValidPartyPanNo = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(xAccountCode, "SUPP_CUST_CODE", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If Trim(MasterNo) = "N" Then
                CheckValidPartyPanNo = True
                Exit Function
            End If
        End If
        If MainClass.ValidateWithMasterTable(xAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_TYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCustType = MasterNo
            If xSuppCustType = "C" Or xSuppCustType = "S" Then
            Else
                CheckValidPartyPanNo = True
                Exit Function
            End If
        Else
            CheckValidPartyPanNo = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(xAccountCode, "SUPP_CUST_CODE", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPANNo = MasterNo
            If Trim(mPANNo) <> "" Then
                If CheckPANValidation(mPANNo) = True Then
                    CheckValidPartyPanNo = True
                    Exit Function
                End If
            End If
        Else
            CheckValidPartyPanNo = False
            Exit Function
        End If
        mSqlStr = " Select A.BILLNO, A.BILLDATE " & vbCrLf & " FROM FIN_TEMPBILL_TRN A, FIN_POSTED_TRN B, TDS_TRN C  " & vbCrLf & " WHERE B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND B.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND A.UserID='" & PubUserID & "' AND TEMPMKEY=" & pProcessKey & "" & vbCrLf & " AND A.AccountCode='" & xAccountCode & "'" & vbCrLf & " AND A.BookType='" & lblBookType.Text & "'" & vbCrLf & " AND A.BILLNO=B.BILLNO" & vbCrLf & " AND A.BILLDATE=B.BILLDATE" & vbCrLf & " AND A.ACCOUNTCODE=B.ACCOUNTCODE" & vbCrLf & " AND B.COMPANY_CODE=C.COMPANY_CODE AND B.MKEY=C.MKEY" & vbCrLf & " AND B.BOOKTYPE=C.BOOKTYPE AND B.BOOKSUBTYPE=C.BOOKSUBTYPE"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = True Then
            CheckValidPartyPanNo = True
            Exit Function
        Else
            CheckValidPartyPanNo = False
            Exit Function
        End If
        Exit Function
ErrPart:
        CheckValidPartyPanNo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ChkACPayee_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkACPayee.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkCapital_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCapital.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkChqDeposit_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkChqDeposit.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkESI_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkESI.CheckStateChanged
        Dim mAccountName As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        SprdMain.Row = 2
        SprdMain.Col = ColAccountName
        mAccountName = Trim(SprdMain.Text)
        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIRate.Enabled = True
            txtESIDeductOn.Enabled = True
            If Val(txtESIRate.Text) = 0 Then
                SqlStr = "SELECT ESI_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mAccountName) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtESIRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ESI_PER").Value), 0, RsTemp.Fields("ESI_PER").Value), "0.000")
                End If
            End If
        Else
            txtESIRate.Enabled = False
            txtESIDeductOn.Enabled = False
            txtESIRate.Text = CStr(0)
        End If
        CalcTots()
    End Sub
    Private Sub ChkESIRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkESIRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub chkExempted_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExempted.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    'Private Sub chkServiceTaxClaim_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkServiceTaxClaim.CheckStateChanged										
    '    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)										
    'End Sub										
    Private Sub chkISLowerDed_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkISLowerDed.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkModvat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkModvat.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

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
                    Call PopulateFromXLSFile(strFilePath)
                End If
            End If

        Catch ex As Exception

        End Try
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:

    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""

        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String


        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""

        Dim mDC As String
        Dim mAccountName As String
        Dim mParticulars As String
        Dim mChqNo As String
        Dim mChqDate As String
        Dim mEmp As String
        Dim mDept As String
        Dim mCC As String
        Dim mEXP As String

        Dim mDivisionCode As Double
        Dim mAmount As Double

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

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

        For Each dtRow In dt.Rows
            mDC = Trim(IIf(IsDBNull(dtRow.Item(0)), "", dtRow.Item(0)))      ''Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))
            mAccountName = Trim(IIf(IsDBNull(dtRow.Item(1)), "", dtRow.Item(1)))
            mParticulars = Trim(IIf(IsDBNull(dtRow.Item(2)), "", dtRow.Item(2)))
            mChqNo = Trim(IIf(IsDBNull(dtRow.Item(3)), "", dtRow.Item(2)))
            mChqDate = Trim(IIf(IsDBNull(dtRow.Item(4)), "", dtRow.Item(4)))
            mChqDate = VB6.Format(mChqDate, "DD/MM/YYYY")
            mEmp = Trim(IIf(IsDBNull(dtRow.Item(5)), "", dtRow.Item(5)))
            mDept = Trim(IIf(IsDBNull(dtRow.Item(6)), "", dtRow.Item(6)))
            mCC = Trim(IIf(IsDBNull(dtRow.Item(7)), "", dtRow.Item(7)))
            mEXP = Trim(IIf(IsDBNull(dtRow.Item(8)), "", dtRow.Item(8)))
            mDivisionCode = Val(IIf(IsDBNull(dtRow.Item(9)), 0, dtRow.Item(9)))
            mAmount = Val(IIf(IsDBNull(dtRow.Item(10)), 0, dtRow.Item(10)))

            'OpenLocalConnection()

            'xSqlStr = " Select ITEM_SHORT_DESC, ISSUE_UOM, CUSTOMER_PART_NO, HSN_CODE " & vbCrLf _
            '        & " FROM INV_ITEM_MST " & vbCrLf _
            '        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            '        & " And LTRIM(RTRIM(ITEM_CODE)) ='" & MainClass.AllowSingleQuote(mItemCode) & "'"

            'MainClass.UOpenRecordSet(xSqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            'If RsTemp.EOF = False Then
            '    mItemDesc = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
            '    mUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
            '    mPartno = Trim(IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value))
            '    mHSNCode = Trim(IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value))
            'Else
            '    GoTo NextRecord
            'End If

            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColDC
            SprdMain.Text = mDC

            SprdMain.Col = ColAccountName
            SprdMain.Text = mAccountName

            SprdMain.Col = ColParticulars
            SprdMain.Text = mParticulars

            SprdMain.Col = ColChequeNo
            SprdMain.Text = mChqNo

            SprdMain.Col = ColChequeDate
            SprdMain.Text = VB6.Format(mChqDate, "DD/MM/YYYY")

            SprdMain.Col = ColEmp
            SprdMain.Text = mEmp

            SprdMain.Col = ColDept
            SprdMain.Text = mDept

            SprdMain.Col = ColCC
            SprdMain.Text = mCC

            SprdMain.Col = ColExp
            SprdMain.Text = mEXP

            SprdMain.Col = ColDivisionCode
            SprdMain.Text = Val(mDivisionCode)

            SprdMain.Col = ColAmount
            SprdMain.Text = VB6.Format(mAmount, "0.000")

            SprdMain.MaxRows = SprdMain.MaxRows + 1

            RsTemp.Close()
            RsTemp = Nothing

            'CloseLocalConnection()
NextRecord:

        Next

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Sub txtTDSSection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSSection.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSSection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSSection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtTDSSection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSSection_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSSection.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtTDSSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtTDSSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Section Code.")
                eventArgs.Cancel = True
            End If
        End If
    End Sub

    Private Sub SearchSection()
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = " SELECT TDSSECTION.NAME  AS NAME" & vbCrLf _
                & " From TDS_Section_MST TDSSECTION " & vbCrLf _
                & " Where TDSSECTION.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchBySQL(SqlStr, "NAME") = True Then
            txtTDSSection.Text = Trim(AcName)
            txtTDSSection_Validating(txtTDSSection, New System.ComponentModel.CancelEventArgs(False)) ''_Validate False
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtTDSSection_KeyUp(sender As Object, e As KeyEventArgs) Handles txtTDSSection.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSection()
    End Sub

    Private Sub txtTDSSection_DoubleClick(sender As Object, e As EventArgs) Handles txtTDSSection.DoubleClick
        Call SearchSection()
    End Sub

    Private Sub frmAtrn_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        fraGridView.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraTrans.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtPopulateVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPopulateVNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPopulateVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPopulateVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPopulateVNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPopulateVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPopulateVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrTxtVno

        If ADDMode = True Then
            CopyVouchExistance()
        End If
        GoTo EventExitSub
ErrTxtVno:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub CopyVouchExistance()
        On Error GoTo ERR1
        Dim mBookCode As String
        Dim mVDate As String
        Dim mVNO As String
        Dim Sqlstr As String
        Dim RsTRNTemp As ADODB.Recordset
        Dim RSTempDetail As ADODB.Recordset
        Dim mKey As String

        mVNO = Trim(txtPopulateVNo.Text)

        Sqlstr = " Select * From FIN_VOUCHER_HDR WHERE " & vbCrLf _
            & " Vno='" & mVNO & "'" & vbCrLf _
            & " AND Booktype='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND BookSubType='" & VB.Right(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTRNTemp.EOF = False Then
            mKey = RsTRNTemp.Fields("mKey").Value
            Clear1()
            Sqlstr = "SELECT FIN_VOUCHER_DET.*" & vbCrLf & " FROM FIN_VOUCHER_DET WHERE MKEY= '" & mKey & "' Order By SubRowNo"
            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTempDetail, ADODB.LockTypeEnum.adLockReadOnly)

            If RSTempDetail.EOF = True Then Exit Sub

            Do While RSTempDetail.EOF = False

                SprdMain.Row = SprdMain.MaxRows

                SprdMain.Col = ColPRRowNo
                SprdMain.Text = Str(IIf(IsDBNull(RSTempDetail.Fields("PRRowNo").Value), 0, RSTempDetail.Fields("PRRowNo").Value))

                SprdMain.Col = ColDC
                SprdMain.Text = RSTempDetail.Fields("DC").Value + "r"


                SprdMain.Col = ColAccountName
                If MainClass.ValidateWithMasterTable(RSTempDetail.Fields("ACCOUNTCODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If

                '        SprdMain.Text = IIf(IsNull(RsTempDetail.Fields("AccountName").Value), "", RsTempDetail.Fields("AccountName").Value)			

                SprdMain.Col = ColParticulars
                SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("PARTICULARS").Value), "", RSTempDetail.Fields("PARTICULARS").Value)

                SprdMain.Col = ColChequeNo
                SprdMain.Text = IIf(Not IsDBNull(RSTempDetail.Fields("ChequeNo").Value), RSTempDetail.Fields("ChequeNo").Value, "")

                SprdMain.Col = ColChequeDate
                SprdMain.Text = VB6.Format(IIf(Not IsDBNull(RSTempDetail.Fields("CHQDATE").Value), RSTempDetail.Fields("CHQDATE").Value, ""), "DD/MM/YYYY")

                SprdMain.Col = ColCC
                If RSTempDetail.Fields("COSTCCODE").Value <> -1 Then
                    '            If MainClass.ValidateWithMasterTable(RsTempDetail.Fields("CostCCode").Value, "COST_CENTER_CODE", "Alias", "CST_CENTER_MST", PubDBCn, MasterNo) = True Then			
                    '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                    '            End If			
                    SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("COSTCCODE").Value), "", RSTempDetail.Fields("COSTCCODE").Value)
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColExp
                If RSTempDetail.Fields("EXP_CODE").Value <> -1 Then
                    SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("EXP_CODE").Value), "", RSTempDetail.Fields("EXP_CODE").Value)
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColDivisionCode
                If RSTempDetail.Fields("DIV_CODE").Value <> -1 Then
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RSTempDetail.Fields("DIV_CODE").Value), "", RSTempDetail.Fields("DIV_CODE").Value)))
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColDept
                If RSTempDetail.Fields("DeptCode").Value <> -1 Then
                    '            If MainClass.ValidateWithMasterTable(RsTempDetail.Fields("DeptCode").Value, "Code", "Alias", "Dept", PubDBCn, MasterNo) = True Then			
                    '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                    '            End If			
                    SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("DeptCode").Value), "", RSTempDetail.Fields("DeptCode").Value)
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColEmp
                If RSTempDetail.Fields("EMPCODE").Value <> -1 Then
                    '            If MainClass.ValidateWithMasterTable(RsTempDetail.Fields("EMPCODE").Value, "Code", "Alias", "Emp", PubDBCn, MasterNo) = True Then			
                    '                SprdMain.Text = IIf(IsNull(MasterNo), "", MasterNo)			
                    '            End If			
                    SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("EMPCODE").Value), "", RSTempDetail.Fields("EMPCODE").Value)
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColIBRNo
                SprdMain.Text = IIf(Not IsDBNull(RSTempDetail.Fields("IBRNo").Value), RSTempDetail.Fields("IBRNo").Value, "")

                SprdMain.Col = ColAmount
                SprdMain.Text = "0"

                SprdMain.Col = ColClearDate
                SprdMain.Text = IIf(IsDBNull(RSTempDetail.Fields("ClearDate").Value), "", RSTempDetail.Fields("ClearDate").Value)

                SprdMain.MaxRows = SprdMain.MaxRows + 1
                RSTempDetail.MoveNext()
            Loop

        End If

        '    Call CalcAccountBal			
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
        '    Resume			
    End Sub

End Class
