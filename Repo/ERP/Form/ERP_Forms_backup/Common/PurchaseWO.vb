Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Imports Microsoft.VisualBasic.Compatibility
Imports System.Drawing.Color
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Printing
Friend Class FrmPurchaseWO
    Inherits System.Windows.Forms.Form
    Dim RsPurchMain As ADODB.Recordset ''Recordset
    Dim RsPurchDetail As ADODB.Recordset ''Recordset
    Dim RsPurchExp As ADODB.Recordset ''Recordset
    Dim RsPurchPrn As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    'Dim SqlStr As String
    Dim mSupplierCode As String
    Dim pRound As Double
    Dim pShowCalc As Boolean
    Private Const mBookType As String = "P"
    'Private Const mBookSubType = "W"
    Dim mBookSubType As String
    Dim pProcessKey As Double
    'Dim mBookSubType As String
    Private Const ConRowHeight As Short = 12
    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String
    Private Const ColGoodsServs As Short = 1
    Private Const ColPONo As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColHSN As Short = 4
    Private Const ColCreditApp As Short = 5
    Private Const ColRCApp As Short = 6
    Private Const ColExempted As Short = 7
    Private Const ColPORate As Short = 8
    Private Const ColUnit As Short = 9
    Private Const ColQty As Short = 10
    Private Const ColRate As Short = 11
    Private Const ColAmount As Short = 12
    Private Const ColGSTableAmount As Short = 13
    Private Const ColCGSTPer As Short = 14
    Private Const ColCGSTAmount As Short = 15
    Private Const ColSGSTPer As Short = 16
    Private Const ColSGSTAmount As Short = 17
    Private Const ColIGSTPer As Short = 18
    Private Const ColIGSTAmount As Short = 19
    Private Const ColAccountPostCode As Short = 20
    Private Const ColRCMkey As Short = 21
    Private Const ColSaleBillPrefix As Short = 22
    Private Const ColSaleBillSeq As Short = 23
    Private Const ColSaleBillNo As Short = 24
    Private Const ColSaleBillDate As Short = 25
    Private Const ColShowPO As Short = 26
    Private Const ColRO As Short = 1
    Private Const ColExpName As Short = 2
    Private Const ColExpPercent As Short = 3
    Private Const ColExpAmt As Short = 4
    Private Const ColExpSTCode As Short = 5
    Private Const ColExpAddDeduct As Short = 6
    Private Const ColExpIdent As Short = 7
    Private Const ColTaxable As Short = 8
    Private Const ColExciseable As Short = 9
    Private Const ColExpCalcOn As Short = 10
    Private Const ColExpDebitAmt As Short = 11

    Private Const ColPayBillNo As Short = 1
    Private Const ColPayBillDate As Short = 2
    Private Const ColPayBillAmount As Short = 3
    Private Const ColPayBalAmount As Short = 4
    Private Const ColPayBalDC As Short = 5
    Private Const ColPayPaymentAmt As Short = 6

    Dim pDnCnNo As String
    Dim mDNCnNO As Integer
    Dim pTempDNCNSeq As Double
    Dim mIsAuthorisedUser As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub FillExpFromPartyExp()
        On Error GoTo ERR1
        Dim RS As ADODB.Recordset
        Dim xAcctCode As String
        Dim xTrnCode As Double
        Dim I As Integer
        Dim mLocal As String
        Dim SqlStr As String
        Dim mRO As String
        If Trim(txtSupplier.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        If Trim(txtSupplier.Text) <> "" Then
            If chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Checked Then
                mLocal = "N"
            Else
                If Trim(txtSupplier.Text) <> "" Then
                    mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
                End If
            End If

            'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = "N"
        End If

        SqlStr = "Select IH.* FROM " & vbCrLf & " FIN_INTERFACE_MST IH " & vbCrLf & " Where IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (IH.Type='P' OR IH.Type='B')  " & vbCrLf & " ORDER BY IH.PrintSequence"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            MainClass.ClearGrid(SprdExp)
            I = 0
            Do While Not RS.EOF
                I = I + 1
                SprdExp.Row = I
                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value
                mRO = IIf(IsDbNull(RS.Fields("RO").Value), "N", RS.Fields("RO").Value)
                SprdExp.Col = ColRO
                SprdExp.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                SprdExp.Col = ColExpPercent
                SprdExp.Text = VB6.Format(0, "0.00")
                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"
                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDbNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))
                SprdExp.Col = ColExpAddDeduct
                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols - 1)
                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)
                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)
                If RS.Fields("Identification").Value = "ST" Then
                    If RS.Fields("STTYPE").Value = mLocal Then
                        SprdExp.RowHidden = False
                    Else
                        SprdExp.RowHidden = True
                    End If
                End If
                RS.MoveNext()
                If RS.EOF = False Then
                    SprdExp.MaxRows = SprdExp.MaxRows + 1
                End If
            Loop
        End If
        FormatSprdExp(-1)
        Call CalcTots()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume
    End Sub
    Private Sub cboGoodsService_Change()
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGoodsService_Click()
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboGSTStatus.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mGSTStatus As String
        Dim mHSNCode As String
        Dim pCreditApplicable As String
        Dim pRCApplicable As String
        Dim pExempted As String
        Dim mGoodServ As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim xAcctCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = Trim(MasterNo)
        Else
            xAcctCode = "-1"
        End If
        If chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Checked Then
            mLocal = "N"
        Else
            mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        End If

        mPartyGSTNo = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = Trim(MasterNo)
        'Else
        '    mLocal = "N"
        'End If
        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = Trim(MasterNo)
        'End If
        For cntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = cntRow
            SprdMain.Col = ColGoodsServs
            mGoodServ = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "G", "S")
            SprdMain.Col = ColHSN
            mHSNCode = Trim(SprdMain.Text)
            If mHSNCode <> "" Then
                If mGoodServ = "S" Then
                    pRCApplicable = "N"
                    pCreditApplicable = "N"
                    pExempted = "N"
                    If GetSACDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "G", pRCApplicable, pCreditApplicable, pExempted) = False Then GoTo ErrPart
                    SprdMain.Row = cntRow
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                        If ADDMode = True Then
                            SprdMain.Col = ColRCApp
                            SprdMain.Value = IIf(pRCApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                            SprdMain.Col = ColCreditApp
                            SprdMain.Value = IIf(pCreditApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                        Else
                        End If
                    Else
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColRCApp
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        SprdMain.Col = ColCreditApp
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If
                Else
                    pRCApplicable = "N"
                    pCreditApplicable = "N"
                    pExempted = "N"
                    If GetHSNDetails(mHSNCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo, pRCApplicable, pCreditApplicable, pExempted) = False Then GoTo ErrPart
                    SprdMain.Row = cntRow
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                        If ADDMode = True Then
                            SprdMain.Col = ColRCApp
                            SprdMain.Value = IIf(pRCApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                            SprdMain.Col = ColCreditApp
                            SprdMain.Value = IIf(pCreditApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                        End If
                    Else
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColRCApp
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        SprdMain.Col = ColCreditApp
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If
                End If
            End If
        Next
        Call CalcTots()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkCreditRC_Click()
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkESI_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkESI.CheckStateChanged
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIRate.Enabled = True
            txtESIDeductOn.Enabled = True
            If Val(txtESIRate.Text) = 0 Then
                SqlStr = "SELECT ESI_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtESIRate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ESI_PER").Value), 0, RsTemp.Fields("ESI_PER").Value), "0.000")
                End If
            End If
        Else
            txtESIRate.Enabled = False
            txtESIDeductOn.Enabled = False
            txtESIRate.Text = CStr(0)
        End If
        txtESIRate.Text = VB6.Format(txtESIRate.Text, "0.000")
        CalcTots()
    End Sub
    Private Sub ChkESIRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkESIRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub chkFinalPost_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFinalPost.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub 'chkSupplyOtherLoc
    Private Sub chkSupplyOtherLoc_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSupplyOtherLoc.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub '
    Private Sub chkFOC_Click()
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkSTDS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSTDS.CheckStateChanged
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSRate.Enabled = True
            txtSTDSDeductOn.Enabled = True
            If Val(txtSTDSRate.Text) = 0 Then
                SqlStr = "SELECT STDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
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
        txtSTDSRate.Text = VB6.Format(txtSTDSRate.Text, "0.000")
        CalcTots()
    End Sub
    Private Sub ChkSTDSRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSTDSRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub chkTDS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTDS.CheckStateChanged
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTdsRate.Enabled = True
            txtTDSDeductOn.Enabled = True
            txtSection.Enabled = True
            If Val(txtTdsRate.Text) = 0 Then
                SqlStr = "SELECT TDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtTdsRate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDS_PER").Value), 0, RsTemp.Fields("TDS_PER").Value), "0.000")
                End If
            End If
        Else
            txtTDSDeductOn.Enabled = False
            txtSection.Enabled = False
            txtTDSRate.Enabled = False
            txtTdsRate.Text = CStr(0)
        End If
        txtTdsRate.Text = VB6.Format(txtTdsRate.Text, "0.000")
        CalcTots()
    End Sub
    Private Sub ChkTDSRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkTDSRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtVNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A" Or mIsAuthorisedUser = True, True, False)
            pShowCalc = True
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim xDCNo As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBookCode As Integer
        Dim mLockBookCode As Integer
        'If PubUserID <> "G0416" Then
        '    Exit Sub
        'End If
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            Exit Sub
        End If
        '
        '    If PubUserID <> "G0416" Then
        '        MsgBox "You Have Not Rights to delete Voucher", vbInformation
        '        Exit Sub
        '    End If
        If PubSuperUser <> "S" Then
            If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Final Bill Post Cann't be Deleted")
                Exit Sub
            End If
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockPurchase), TxtVDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If
        If Trim(txtBillNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Trim(txtVno.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        ''    If LblBookCode.text = ConPurchaseBookCode Then
        ''        If MainClass.GetUserCanModify(txtVDate.Text) = False Then
        ''            MsgBox "You Have Not Rights to delete back Voucher", vbInformation
        ''            Exit Sub
        ''        End If
        ''    End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Deleted.")
            Exit Sub
        End If
        If CheckBillPayment(mSupplierCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub
        If CheckDebitNoteServiceExists(mSupplierCode, (txtBillNo.Text), (txtBillDate.Text)) = True Then Exit Sub
        If Not RsPurchMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                'If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_HDR", (txtBillNo.Text), RsPurchMain, "BillNo") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_HDR", (LblMKey.Text), RsPurchMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_DET", (LblMKey.Text), RsPurchDetail, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_EXP", (LblMKey.Text), RsPurchExp, "MKEY", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "FIN_PURCHASE_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("Delete from FIN_PURCHASE_VNO_MST Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_PURCHASE_TRN WHERE MKey='" & LblMKey.Text & "' AND BookCode=" & ConPurchaseBookCode & "")
                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'")
                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & ConPurchaseBookCode & "'")
                PubDBCn.Execute("DELETE FROM FIN_GST_SEQ_MST " & vbCrLf & " WHERE MKEY= '" & LblMKey.Text & "'" & vbCrLf & " AND COMPANY_CODE = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCODE = '" & LblBookCode.Text & "'" & vbCrLf & " AND BOOKTYPE = '" & mBookType & "'")
                PubDBCn.Execute("Delete from FIN_PURCHASE_EXP Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_PURCHASE_DET Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_PURCHASE_HDR WHERE MKey='" & LblMKey.Text & "' ")
                PubDBCn.CommitTrans()
                RsPurchMain.Requery() ''.Refresh
                RsPurchDetail.Requery() ''.Refresh
                RsPurchExp.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        '    Resume
        PubDBCn.RollbackTrans() ''
        RsPurchMain.Requery() ''.Refresh
        RsPurchDetail.Requery() ''.Refresh
        RsPurchExp.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click
        On Error GoTo ModifyErr
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Modified")
            Exit Sub
        End If
        '    If chkFinalPost.Value = vbChecked Then
        '        MsgInformation "Final Bill Post Cann't be Modified"
        '        Exit Sub
        '    End If
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtVNo.Enabled = IIf(PubSuperUser = "S" Or mIsAuthorisedUser = True, True, False) ''Or PubSuperUser = "A"
            pShowCalc = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPostingHead_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPostingHead.Click
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntRow As Integer
        FraPostingDtl.Visible = Not FraPostingDtl.Visible
        If FraPostingDtl.Visible = True Then
            FraPostingDtl.BringToFront()
            MainClass.ClearGrid(SprdPostingDetail)
            SqlStr = " SELECT ACM.SUPP_CUST_NAME, " & vbCrLf & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC "
            SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE ='" & mBookType & "'" & vbCrLf & " AND BOOKSUBTYPE = '" & mBookSubType & "'" & vbCrLf & " AND TRN.MKEY='" & LblMKey.Text & "'"
            SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            cntRow = 1
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    SprdPostingDetail.Row = cntRow
                    SprdPostingDetail.Col = 1
                    SprdPostingDetail.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    SprdPostingDetail.Col = 2
                    SprdPostingDetail.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Amount").Value), "0.00", RsTemp.Fields("Amount").Value), "0.00")
                    SprdPostingDetail.Col = 3
                    SprdPostingDetail.Text = IIf(IsDbNull(RsTemp.Fields("DC").Value), "", RsTemp.Fields("DC").Value)
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        SprdPostingDetail.MaxRows = cntRow
                    End If
                Loop
            End If
            FraPostingDtl.BringToFront()
            Call FormatSprdPostingDetail(-1)
        End If
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPurchase(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPurchase(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnPurchase(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Dim mVNo As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        Call SelectQryForVoucher(SqlStr)
        mTitle = "Purchase (Contract / Service / Work) Entry"
        mRptFileName = "PurchaseGST.rpt"
        mSubTitle = ""
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, "N")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForVoucher(ByRef mSqlStr As String) As String
        ''SELECT CLAUSE...
        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.SUPP_CUST_NAME "
        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST " ' & vbCrLf |
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf & " AND IH.BOOKTYPE='" & mBookType & "'" & vbCrLf & " AND IH.BOOKSUBTYPE='" & mBookSubType & "'" & vbCrLf & " AND IH.ISFINALPOST='Y'"
        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"
        SelectQryForVoucher = mSqlStr
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call CalcTots()
        'If PubUserID <> "G0416" Then
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        'End If
        pDnCnNo = ""
        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            ''TxtVNo_Validate False
            txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub SprdExp_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdExp.ButtonClicked
        If pShowCalc = True Then '' If FormActive = True Then
            SprdExp.Col = ColExpAmt
            SprdExp.Row = eventArgs.row
            If Val(SprdExp.Text) <> 0 Then
                Call CalcTots()
            End If
        End If
    End Sub
    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        On Error GoTo ErrPart
        Dim mPONo As String
        If eventArgs.col = ColPONo Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColPONo
            mPONo = SprdMain.Text
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Call ReportonPO(Crystal.DestinationConstants.crptToWindow, mPONo)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '    ElseIf Col = ColCreditApp Then
            '        If Left(cboGSTStatus.Text, 1) = "E" Or Left(cboGSTStatus.Text, 1) = "D" Or Left(cboGSTStatus.Text, 1) = "N" Then
            '            SprdMain.Row = Row
            '            SprdMain.Col = ColCreditApp
            '            SprdMain.Value = vbUnchecked
            '        End If
        End If
        Exit Sub
ErrPart:
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim xIName As String
        Dim SqlStr As String
        Dim mSupplierCode As String
        Dim mPONo As Double
        Dim xHSNCode As String
        Dim RsTemp As ADODB.Recordset
        Dim mGoodServ As String
        mSupplierCode = ""
        If Trim(txtSupplier.Text) = "" Then
            MsgBox("Please Select Supplier Name.", MsgBoxStyle.Information)
            Exit Sub
        ElseIf MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            MsgBox("Invalid Supplier Name.", MsgBoxStyle.Information)
            Exit Sub
        End If
        If eventArgs.row = 0 And eventArgs.col = ColPONo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColPONo
                mPONo = Val(.Text)
                SqlStr = GetPOQuery(mPONo, ColPONo)
                If SqlStr <> "" Then
                    With SprdMain
                        .Row = .ActiveRow
                        .Col = ColPONo
                        If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                            .Row = .ActiveRow
                            .Col = ColPONo
                            .Text = AcName
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColPONo)
                        End If
                    End With
                End If
                '            If MainClass.SearchGridMaster(.Text, "PUR_PURCHASE_HDR", "AUTO_KEY_PO", "AMEND_NO", "AMEND_WEF_DATE", , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_CODE='" & mSupplierCode & "' AND PUR_TYPE='W'") = True Then
                '                .Row = .ActiveRow
                '                .Col = ColPONo
                '                .Text = AcName
                '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColPONo
                '            End If
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Row = .ActiveRow
                .Col = ColPONo
                mPONo = Val(.Text)
                .Col = ColItemDesc
                xIName = .Text
                .Text = ""
                SqlStr = GetPOQuery(mPONo, ColItemDesc)
                If SqlStr <> "" Then
                    With SprdMain
                        .Row = .ActiveRow
                        .Col = ColItemDesc
                        If MainClass.SearchGridMasterBySQL2(xIName, SqlStr) = True Then
                            .Row = .ActiveRow
                            .Col = ColItemDesc
                            .Text = AcName
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemDesc)
                        End If
                    End With
                End If
                '            If MainClass.SearchGridMaster(.Text, "PUR_PURCHASE_DET", "WO_DESCRIPTION", "MKEY", "PO_WEF_DATE", , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_CODE='" & mSupplierCode & "' AND PUR_TYPE='W'") = True Then
                '                .Row = .ActiveRow
                '                .Col = ColPONo
                '                .Text = AcName
                '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColAccountPostCode
                '            End If
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColHSN Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColGoodsServs
                mGoodServ = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "G", "S")
                .Col = ColHSN
                '            mHSNCode = Trim(.Text)
                If MainClass.SearchGridMaster(.Text, "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & mGoodServ & "'") = True Then
                    .Row = .ActiveRow
                    .Col = ColHSN
                    .Text = AcName
                    xHSNCode = Trim(.Text)
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHSN)
                End If
            End With
        End If
        '    If Row = 0 And Col = ColAccountPostCode Then
        '        With SprdMain
        '            .Row = .ActiveRow
        '            .Col = ColAccountPostCode
        '            If MainClass.SearchGridMaster(.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE='O'") = True Then
        '                .Row = .ActiveRow
        '                .Col = ColAccountPostCode
        '                .Text = AcName
        ''                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColAccountPostCode
        '            End If
        '        End With
        '    End If
        If eventArgs.row = 0 And eventArgs.col = ColAccountPostCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColAccountPostCode
                If MainClass.SearchGridMaster(.Text, "FIN_INVTYPE_MST", "NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    .Row = .ActiveRow
                    .Col = ColAccountPostCode
                    .Text = AcName
                    '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColInvType
                End If
            End With
        End If
        If eventArgs.col = 0 And eventArgs.row > 0 Then '***ROW DEL. OPTION NOT REQ IN INVOICE
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColPONo
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColPONo)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If
        Call CalcTots()
    End Sub
    Private Function GetPOQuery(ByRef pPONO As Double, ByRef pCol As Integer) As String
        Dim xIName As String
        Dim SqlStr As String
        Dim mSupplierCode As String
        Dim mPONo As String
        Dim mDivisionCode As Integer
        mSupplierCode = ""
        If Trim(txtSupplier.Text) = "" Then
            MsgBox("Please Select Supplier Name.", MsgBoxStyle.Information)
            Exit Function
        ElseIf MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            MsgBox("Invalid Supplier Name.", MsgBoxStyle.Information)
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CInt(Trim(MasterNo))
        End If
        If pCol = ColPONo Then
            SqlStr = "SELECT DISTINCT POMain.AUTO_KEY_PO  As AUTO_KEY_PO , POMain.PUR_ORD_DATE, PODetail.PO_WEF_DATE, PODetail.WO_DESCRIPTION, PODetail.ITEM_QTY, PODetail.ITEM_PRICE, PODetail.GROSS_AMT "
        ElseIf pCol = 100 Then
            SqlStr = "SELECT DISTINCT SAC_CODE,ISGSTAPPLICABLE "
        Else
            SqlStr = "SELECT DISTINCT PODetail.WO_DESCRIPTION, POMain.AUTO_KEY_PO  As AUTO_KEY_PO , POMain.PUR_ORD_DATE "
        End If

        SqlStr = SqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR POMain,PUR_PURCHASE_DET PODetail" & vbCrLf _
            & " WHERE POMain.MKEY=PODetail.MKEY" & vbCrLf _
            & " AND POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUR_TYPE ='W'"

        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & mSupplierCode & "'"
        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        If IsDate(txtBillDate.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='Y' AND POMain.PO_CLOSED='N'"
        End If
        '    If PubGSTApplicable = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE>='" & vb6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "'"
        '    End If
        SqlStr = SqlStr & vbCrLf & "  AND PO_ITEM_STATUS='N' "
        If pCol = ColPONo Then
            If Val(CStr(pPONO)) > 0 Then
                SqlStr = SqlStr & vbCrLf & " AND POMain.AUTO_KEY_PO Like '" & pPONO & "%'"
            End If
        Else
            SqlStr = SqlStr & vbCrLf & " AND POMain.AUTO_KEY_PO = " & pPONO & ""
        End If
        GetPOQuery = SqlStr
    End Function
    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If Trim(txtSupplier.Text) = "" Then
            MsgInformation("Please select the Customer First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        End If

        SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        If MainClass.SearchGridMasterBySQL2((txtBillTo.Text), SqlStr) = True Then
            txtBillTo.Text = AcName
            txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))
            If txtBillTo.Enabled = True Then txtBillTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtBillTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBillTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBillTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtBillTo_DoubleClick(txtBillTo, New System.EventArgs())
    End Sub
    Private Sub txtBillTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String
        Dim mLocal As String
        Dim CntRow As Long
        Dim mGoodServ As String
        Dim mHSNCode As String
        Dim mSACCode As String
        Dim mRateOption As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mPartyGSTNo As String
        Dim pRCApplicable As String
        Dim pCreditApplicable As String
        Dim pExempted As String

        If Trim(txtSupplier.Text) = "" Then GoTo EventExitSub
        If Trim(txtBillTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
            MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Checked Then
            mLocal = "N"
        Else
            mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        End If

        mPartyGSTNo = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        For CntRow = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = CntRow
            SprdMain.Col = ColGoodsServs
            mGoodServ = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "G", "S")
            SprdMain.Col = ColHSN
            mHSNCode = Trim(SprdMain.Text)
            If mHSNCode <> "" Then
                If mGoodServ = "S" Then
                    If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        mSACCode = MasterNo
                    End If

                    If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "GST_RATE_OPT", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        mRateOption = MasterNo
                    Else
                        mRateOption = "N"
                    End If
                    pRCApplicable = "N"
                    pCreditApplicable = "N"
                    pExempted = "N"
                    If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "G", pRCApplicable, pCreditApplicable, pExempted) = False Then GoTo EventExitSub
                    SprdMain.Row = CntRow
                    SprdMain.Col = ColHSN
                    SprdMain.Text = Trim(mSACCode)
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Or VB.Left(cboGSTStatus.Text, 1) = "I" Or VB.Left(cboGSTStatus.Text, 1) = "C" Then
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                        If ADDMode = True Then
                            SprdMain.Col = ColRCApp
                            SprdMain.Value = IIf(pRCApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                            SprdMain.Col = ColCreditApp
                            SprdMain.Value = IIf(pCreditApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                        Else
                        End If
                    Else
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColRCApp
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        SprdMain.Col = ColCreditApp
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If
                    SprdMain.Col = ColExempted
                    SprdMain.Value = IIf(pExempted = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    If mRateOption = "Y" Then
                        '                        MainClass.UnProtectCell SprdMain, 1, SprdMain.Row, 1, SprdMain.MaxCols
                        '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColPORate, ColPORate
                        '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColRCApp, ColExempted
                        '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColAmount, ColGSTableAmount
                        '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColCGSTAmount, ColCGSTAmount
                        '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColSGSTAmount, ColSGSTAmount
                        '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColIGSTAmount, ColIGSTAmount
                        '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColRCMkey, ColShowPO
                        If mLocal = "Y" Then
                            MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                            MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTPer, ColSGSTPer)
                            MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTPer, ColIGSTPer)
                        Else
                            MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTPer, ColIGSTPer)
                            MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                            MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTPer, ColSGSTPer)
                        End If
                    Else
                        MainClass.ProtectCell(SprdMain, 1, SprdMain.Row, ColCGSTPer, ColIGSTPer)
                    End If
                Else
                    If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = True Then
                        mSACCode = MasterNo
                    Else
                        MsgBox("Invalid HSN Code.", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, CntRow, ColHSN)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If
                    pRCApplicable = "N"
                    pCreditApplicable = "N"
                    pExempted = "N"
                    If GetHSNDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo, pRCApplicable, pCreditApplicable, pExempted) = False Then GoTo EventExitSub
                    SprdMain.Row = CntRow
                    SprdMain.Col = ColHSN
                    SprdMain.Text = Trim(mSACCode)
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Or VB.Left(cboGSTStatus.Text, 1) = "I" Or VB.Left(cboGSTStatus.Text, 1) = "C" Then
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                        If ADDMode = True Then
                            SprdMain.Col = ColRCApp
                            SprdMain.Value = IIf(pRCApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                            SprdMain.Col = ColCreditApp
                            SprdMain.Value = IIf(pCreditApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                        End If
                    Else
                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(0, "0.00")
                        SprdMain.Col = ColRCApp
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        SprdMain.Col = ColCreditApp
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If
                    SprdMain.Col = ColExempted
                    SprdMain.Value = IIf(pExempted = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                End If
            End If
        Next
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim RIBBONSGroup As Boolean
        Dim xSoNo As String
        Dim xICode As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        'Dim mServCode As String
        Dim mServDesc As String
        Dim mSACCode As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mLocal As String
        Dim mPONo As Double
        Dim mItemDesc As String
        Dim mInvTypeCode As Double
        Dim mGSTApplicable As String
        Dim mReverseCharge As String
        Dim mPORate As Double
        Dim mUOM As String
        Dim mPartyGSTNo As String
        Dim mHSNCode As String
        Dim mGoodServ As String
        Dim pRCApplicable As String
        Dim pCreditApplicable As String
        Dim pExempted As String
        Dim mRateOption As String
        Dim pActiveRow As Integer
        If eventArgs.newRow = -1 Then Exit Sub
        If Trim(txtSupplier.Text) = "" Then MsgInformation("Please Enter the Supplier First") : Exit Sub

        Dim xAcctCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = Trim(MasterNo)
        Else
            xAcctCode = "-1"
        End If

        If chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Checked Then
            mLocal = "N"
        Else
            mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        End If

        mPartyGSTNo = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        ''21-08-2017
        '    If Left(cboGSTStatus.Text, 1) = "R" Or chkReverserChargeApp.Value = vbChecked Then
        '        mLocal = "Y"
        '    Else
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = Trim(MasterNo)
        'Else
        '    mLocal = "N"
        'End If
        ''    End If
        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = Trim(MasterNo)
        'End If
        SprdMain.Row = SprdMain.ActiveRow
        pActiveRow = SprdMain.ActiveRow
        Select Case eventArgs.col
            Case ColPONo
                SprdMain.Row = pActiveRow
                SprdMain.Col = ColPONo
                If Val(SprdMain.Text) > 0 Then
                    mSqlStr = GetPOQuery(Val(SprdMain.Text), 100)
                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
                    If RsTemp.EOF = False Then
                        mSACCode = IIf(IsDBNull(RsTemp.Fields("SAC_CODE").Value), "", RsTemp.Fields("SAC_CODE").Value)
                        mGSTApplicable = IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "", RsTemp.Fields("ISGSTAPPLICABLE").Value)
                        If mGSTApplicable = "G" Then
                            cboGSTStatus.SelectedIndex = 0
                        ElseIf mGSTApplicable = "R" Then
                            cboGSTStatus.SelectedIndex = 1
                        ElseIf mGSTApplicable = "E" Then
                            cboGSTStatus.SelectedIndex = 2
                        ElseIf mGSTApplicable = "N" Then
                            cboGSTStatus.SelectedIndex = 3
                        ElseIf mGSTApplicable = "I" Then
                            cboGSTStatus.SelectedIndex = 4
                        ElseIf mGSTApplicable = "C" Then
                            cboGSTStatus.SelectedIndex = 5
                        ElseIf mGSTApplicable = "D" Then
                            cboGSTStatus.SelectedIndex = 6
                        End If
                        If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
                            MsgBox("Invalid Service.", MsgBoxStyle.Information)
                            MainClass.SetFocusToCell(SprdMain, pActiveRow, ColPONo)
                            eventArgs.cancel = True
                            Exit Sub
                        Else
                            txtServProvided.Text = IIf(Trim(txtServProvided.Text) = "", MasterNo, txtServProvided.Text)
                        End If
                        SprdMain.Col = ColPONo
                        mPONo = Val(SprdMain.Text)
                        SprdMain.Col = ColItemDesc
                        mItemDesc = Trim(SprdMain.Text)
                        SprdMain.Col = ColAccountPostCode
                        If Trim(SprdMain.Text) = "" Then
                            If GetPODetails(mPONo, mItemDesc, mInvTypeCode, mPORate, mUOM) = False Then GoTo ErrPart
                            SprdMain.Col = ColPORate
                            SprdMain.Text = VB6.Format(mPORate, "0.0000")
                            SprdMain.Col = ColUnit
                            SprdMain.Text = mUOM
                            SprdMain.Col = ColAccountPostCode
                            If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                SprdMain.Text = MasterNo
                            Else
                                SprdMain.Text = ""
                            End If
                        End If
                        '                    If MainClass.ValidateWithMasterTable(mServCode, "CODE", "SERV_CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        '                        mSACCode = MasterNo
                        '                    End If
                        '
                        '                    If GetSACDetails(mServCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal) = False Then GoTo ErrPart
                        '
                        '                    SprdMain.Row = pActiveRow
                        '                    SprdMain.Col =ColHSN
                        '                    SprdMain.Text = Trim(mSACCode)
                        '
                        '                    SprdMain.Col = ColCGSTPer
                        '                    SprdMain.Text = Format(mCGSTPer, "0.00")
                        '
                        '                    SprdMain.Col = ColSGSTPer
                        '                    SprdMain.Text = Format(mSGSTPer, "0.00")
                        '
                        '                    SprdMain.Col = ColIGSTPer
                        '                    SprdMain.Text = Format(mIGSTPer, "0.00")
                    End If
                End If
            Case ColItemDesc
                SprdMain.Row = pActiveRow
                SprdMain.Col = ColPONo
                mPONo = Val(SprdMain.Text)
                SprdMain.Col = ColItemDesc
                mItemDesc = Trim(SprdMain.Text)
                If mItemDesc = "" Then Exit Sub
                If CheckDuplicateItem(mItemDesc) = True Then
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemDesc)
                    Exit Sub
                End If
                If lblPurchaseType.Text = "S" Then
                    '                If Trim(txtServProvided.Text) = "" Then MsgInformation "Please Select The Service First": Exit Sub
                    '
                    '
                    '                If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    '                    MsgBox "Invalid Service.", vbInformation
                    '                    MainClass.SetFocusToCell SprdMain, pActiveRow, ColPONo
                    '                    Cancel = True
                    '                    Exit Sub
                    '                Else
                    '                    mServCode = MasterNo
                    '                End If
                    '
                    '
                    '                If MainClass.ValidateWithMasterTable(mServCode, "CODE", "SAC_CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '                    mSACCode = MasterNo
                    '                End If
                    '
                    '                If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo) = False Then GoTo ErrPart
                    '
                    '                SprdMain.Row = pActiveRow
                    '                SprdMain.Col = ColHSN
                    '                SprdMain.Text = Trim(mSACCode)
                    '
                    '                SprdMain.Col = ColCGSTPer
                    '                SprdMain.Text = Format(mCGSTPer, "0.00")
                    '
                    '                SprdMain.Col = ColSGSTPer
                    '                SprdMain.Text = Format(mSGSTPer, "0.00")
                    '
                    '                SprdMain.Col = ColIGSTPer
                    '                SprdMain.Text = Format(mIGSTPer, "0.00")
                    SprdMain.Col = ColItemDesc
                    mItemDesc = Trim(SprdMain.Text)
                    SprdMain.Col = ColAccountPostCode
                    If Trim(SprdMain.Text) = "" Then
                        SprdMain.Col = ColAccountPostCode
                        If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            SprdMain.Text = MasterNo
                        Else
                            SprdMain.Text = ""
                        End If
                    End If
                Else
                    If mPONo > 0 Then
                        mSqlStr = GetPOQuery(Val(CStr(mPONo)), 100)
                        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
                        If RsTemp.EOF = False Then
                            mSACCode = IIf(IsDBNull(RsTemp.Fields("SAC_CODE").Value), "", RsTemp.Fields("SAC_CODE").Value)
                            mGSTApplicable = IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "", RsTemp.Fields("ISGSTAPPLICABLE").Value)
                            If mGSTApplicable = "G" Then
                                cboGSTStatus.SelectedIndex = 0
                            ElseIf mGSTApplicable = "R" Then
                                cboGSTStatus.SelectedIndex = 1
                            ElseIf mGSTApplicable = "E" Then
                                cboGSTStatus.SelectedIndex = 2
                            ElseIf mGSTApplicable = "N" Then
                                cboGSTStatus.SelectedIndex = 3
                            ElseIf mGSTApplicable = "I" Then
                                cboGSTStatus.SelectedIndex = 4
                            ElseIf mGSTApplicable = "C" Then
                                cboGSTStatus.SelectedIndex = 5
                            ElseIf mGSTApplicable = "D" Then
                                cboGSTStatus.SelectedIndex = 6
                            End If
                            '                        If MainClass.ValidateWithMasterTable(mServCode, "CODE", "NAME", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                            '                            MsgBox "Invalid Service.", vbInformation
                            '                            MainClass.SetFocusToCell SprdMain, pActiveRow, ColPONo
                            '                            Cancel = True
                            '                            Exit Sub
                            '                        Else
                            '                            txtServProvided.Text = IIf(Trim(txtServProvided.Text) = "", MasterNo, txtServProvided.Text)
                            '                        End If
                            If MainClass.ValidateWithMasterTable(txtServProvided.Text, "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mSACCode = MasterNo
                            End If
                            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ErrPart
                            SprdMain.Row = pActiveRow
                            SprdMain.Col = ColHSN
                            SprdMain.Text = Trim(mSACCode)
                            SprdMain.Col = ColCGSTPer
                            SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                            SprdMain.Col = ColSGSTPer
                            SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                            SprdMain.Col = ColIGSTPer
                            SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                            SprdMain.Col = ColPONo
                            mPONo = Val(SprdMain.Text)
                            SprdMain.Col = ColItemDesc
                            mItemDesc = Trim(SprdMain.Text)
                            If GetPODetails(mPONo, mItemDesc, mInvTypeCode, mPORate, mUOM) = False Then GoTo ErrPart
                            SprdMain.Col = ColPORate
                            SprdMain.Text = VB6.Format(mPORate, "0.0000")
                            SprdMain.Col = ColUnit
                            SprdMain.Text = mUOM
                            SprdMain.Col = ColAccountPostCode
                            If Trim(SprdMain.Text) = "" Then
                                SprdMain.Col = ColAccountPostCode
                                If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                                    SprdMain.Text = MasterNo
                                Else
                                    SprdMain.Text = ""
                                End If
                            End If
                        End If
                    End If
                End If
            Case ColHSN
                SprdMain.Row = pActiveRow
                SprdMain.Col = ColGoodsServs
                mGoodServ = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "G", "S")
                SprdMain.Col = ColHSN
                mHSNCode = Trim(SprdMain.Text)
                If mHSNCode <> "" Then
                    If mGoodServ = "S" Then
                        If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                            mSACCode = MasterNo
                        Else
                            MsgBox("Invalid SAC Code.", MsgBoxStyle.Information)
                            MainClass.SetFocusToCell(SprdMain, pActiveRow, ColHSN)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                        If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "GST_RATE_OPT", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                            mRateOption = MasterNo
                        Else
                            mRateOption = "N"
                        End If
                        pRCApplicable = "N"
                        pCreditApplicable = "N"
                        pExempted = "N"
                        If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, "G", pRCApplicable, pCreditApplicable, pExempted) = False Then GoTo ErrPart
                        SprdMain.Row = pActiveRow
                        SprdMain.Col = ColHSN
                        SprdMain.Text = Trim(mSACCode)
                        If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Or VB.Left(cboGSTStatus.Text, 1) = "I" Or VB.Left(cboGSTStatus.Text, 1) = "C" Then
                            SprdMain.Col = ColCGSTPer
                            SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                            SprdMain.Col = ColSGSTPer
                            SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                            SprdMain.Col = ColIGSTPer
                            SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                            If ADDMode = True Then
                                SprdMain.Col = ColRCApp
                                SprdMain.Value = IIf(pRCApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                                SprdMain.Col = ColCreditApp
                                SprdMain.Value = IIf(pCreditApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                            Else
                            End If
                        Else
                            SprdMain.Col = ColCGSTPer
                            SprdMain.Text = VB6.Format(0, "0.00")
                            SprdMain.Col = ColSGSTPer
                            SprdMain.Text = VB6.Format(0, "0.00")
                            SprdMain.Col = ColIGSTPer
                            SprdMain.Text = VB6.Format(0, "0.00")
                            SprdMain.Col = ColRCApp
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                            SprdMain.Col = ColCreditApp
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        End If
                        SprdMain.Col = ColExempted
                        SprdMain.Value = IIf(pExempted = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                        If mRateOption = "Y" Then
                            '                        MainClass.UnProtectCell SprdMain, 1, SprdMain.Row, 1, SprdMain.MaxCols
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColPORate, ColPORate
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColRCApp, ColExempted
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColAmount, ColGSTableAmount
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColCGSTAmount, ColCGSTAmount
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColSGSTAmount, ColSGSTAmount
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColIGSTAmount, ColIGSTAmount
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColRCMkey, ColShowPO
                            If mLocal = "Y" Then
                                MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                                MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTPer, ColSGSTPer)
                                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTPer, ColIGSTPer)
                            Else
                                MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTPer, ColIGSTPer)
                                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                                MainClass.ProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTPer, ColSGSTPer)
                            End If
                        Else
                            MainClass.ProtectCell(SprdMain, 1, SprdMain.Row, ColCGSTPer, ColIGSTPer)
                        End If
                    Else
                        If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = True Then
                            mSACCode = MasterNo
                        Else
                            MsgBox("Invalid HSN Code.", MsgBoxStyle.Information)
                            MainClass.SetFocusToCell(SprdMain, pActiveRow, ColHSN)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                        pRCApplicable = "N"
                        pCreditApplicable = "N"
                        pExempted = "N"
                        If GetHSNDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo, pRCApplicable, pCreditApplicable, pExempted) = False Then GoTo ErrPart
                        SprdMain.Row = pActiveRow
                        SprdMain.Col = ColHSN
                        SprdMain.Text = Trim(mSACCode)
                        If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Or VB.Left(cboGSTStatus.Text, 1) = "I" Or VB.Left(cboGSTStatus.Text, 1) = "C" Then
                            SprdMain.Col = ColCGSTPer
                            SprdMain.Text = VB6.Format(mCGSTPer, "0.00")
                            SprdMain.Col = ColSGSTPer
                            SprdMain.Text = VB6.Format(mSGSTPer, "0.00")
                            SprdMain.Col = ColIGSTPer
                            SprdMain.Text = VB6.Format(mIGSTPer, "0.00")
                            If ADDMode = True Then
                                SprdMain.Col = ColRCApp
                                SprdMain.Value = IIf(pRCApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                                SprdMain.Col = ColCreditApp
                                SprdMain.Value = IIf(pCreditApplicable = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                            End If
                        Else
                            SprdMain.Col = ColCGSTPer
                            SprdMain.Text = VB6.Format(0, "0.00")
                            SprdMain.Col = ColSGSTPer
                            SprdMain.Text = VB6.Format(0, "0.00")
                            SprdMain.Col = ColIGSTPer
                            SprdMain.Text = VB6.Format(0, "0.00")
                            SprdMain.Col = ColRCApp
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                            SprdMain.Col = ColCreditApp
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        End If
                        SprdMain.Col = ColExempted
                        SprdMain.Value = IIf(pExempted = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    End If
                End If
            Case ColQty
                If CheckQty() = True Then
                    SprdMain.Row = pActiveRow
                    SprdMain.Col = ColGoodsServs
                    mGoodServ = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "G", "S")
                    SprdMain.Col = ColHSN
                    mHSNCode = Trim(SprdMain.Text)
                    MainClass.AddBlankSprdRow(SprdMain, ColItemDesc, ConRowHeight * 2)
                    FormatSprdMain((SprdMain.MaxRows))
                    SprdMain.Row = pActiveRow
                    If mGoodServ = "S" Then
                        If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "GST_RATE_OPT", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                            mRateOption = MasterNo
                        Else
                            mRateOption = "N"
                        End If
                        If mRateOption = "Y" Then
                            '                        MainClass.UnProtectCell SprdMain, 1, SprdMain.Row, 1, SprdMain.MaxCols
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColPORate, ColPORate
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColRCApp, ColExempted
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColAmount, ColGSTableAmount
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColCGSTAmount, ColCGSTAmount
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColSGSTAmount, ColSGSTAmount
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColIGSTAmount, ColIGSTAmount
                            '                        MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColRCMkey, ColShowPO
                            '
                            '                        If mLocal = "Y" Then
                            '                            MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColIGSTPer, ColIGSTPer
                            '                        Else
                            '                            MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColCGSTPer, ColCGSTPer
                            '                            MainClass.ProtectCell SprdMain, 1, SprdMain.Row, ColSGSTPer, ColSGSTPer
                            '                        End If
                            If mLocal = "Y" Then
                                MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                                MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColSGSTPer, ColSGSTPer)
                                MainClass.ProtectCell(SprdMain, 1, SprdMain.Row, ColIGSTPer, ColIGSTPer)
                            Else
                                MainClass.UnProtectCell(SprdMain, SprdMain.Row, SprdMain.Row, ColIGSTPer, ColIGSTPer)
                                MainClass.ProtectCell(SprdMain, 1, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                                MainClass.ProtectCell(SprdMain, 1, SprdMain.Row, ColSGSTPer, ColSGSTPer)
                            End If
                        Else
                            MainClass.ProtectCell(SprdMain, 1, SprdMain.Row, ColCGSTPer, ColCGSTPer)
                        End If
                    End If
                End If
            Case ColRate
                Call CheckRate()
            Case ColAccountPostCode
                SprdMain.Row = pActiveRow
                SprdMain.Col = ColAccountPostCode
                If Trim(SprdMain.Text) <> "" Then
                    '                If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE NOT IN ('S','C')") = False Then
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgBox("Invoice Name Does Not Exist In Master", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, pActiveRow, ColAccountPostCode)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                End If
        End Select
        '    If Left(cboGoodsService.Text, 1) = "S" Then
        '        If ValidateHSNCode = False Then
        '            Cancel = True
        '            Exit Sub
        '        End If
        '    End If
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function ValidateHSNCode() As Boolean
        On Error GoTo ErrPart
        Dim mHSNCode As String
        Dim mCreditApp As String
        Dim cntRow As Integer
        Dim mCountCreditApp As Integer
        Dim mCountCreditNotApp As Integer
        Dim mReverseChargeApp As String
        Dim mCountReverseChargeApp As Integer
        Dim mCountReverseChargeNotApp As Integer
        Dim mCount As Integer
        Dim mGSTRegNo As String
        ValidateHSNCode = True
        Exit Function
        '    ValidateHSNCode = False
        '    mCountCreditApp = 0
        '    mCountCreditNotApp = 0
        '    mCountReverseChargeApp = 0
        '    mCountReverseChargeNotApp = 0
        '    mCount = 0
        '    With SprdMain
        '        For cntRow = 1 To .MaxRows
        '            SprdMain.Row = cntRow
        '            SprdMain.Col = ColHSN
        '            mHSNCode = Trim(.Text)
        '
        '            If mHSNCode <> "" Then
        '                If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "GST_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
        '                    MsgBox "Invalid SAC Code.", vbInformation
        '                    ValidateHSNCode = False
        '                    Exit Function
        '                Else
        '                    mCreditApp = Trim(MasterNo)
        '                    mCountCreditApp = mCountCreditApp + IIf(mCreditApp = "Y", 1, 0)
        '                    mCountCreditNotApp = mCountCreditNotApp + IIf(mCreditApp = "N", 1, 0)
        '                    mCount = mCount + 1
        '                End If
        '
        '                If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "REVERSE_CHARGE_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND CODETYPE='S'") = False Then
        '                    MsgBox "Invalid SAC Code.", vbInformation
        '                    ValidateHSNCode = False
        '                    Exit Function
        '                Else
        '                    mReverseChargeApp = Trim(MasterNo)
        '                    mCountReverseChargeApp = mCountReverseChargeApp + IIf(mReverseChargeApp = "Y", 1, 0)
        '                    mCountReverseChargeNotApp = mCountReverseChargeNotApp + IIf(mReverseChargeApp = "N", 1, 0)
        '                End If
        '            End If
        '        Next
        '    End With
        '
        '    If mCount = mCountCreditApp Then
        '        chkGSTCreditApp.Value = vbChecked
        '        ValidateHSNCode = True
        '    ElseIf mCount = mCountCreditNotApp Then
        '        chkGSTCreditApp.Value = vbUnchecked
        '        ValidateHSNCode = True
        '    Else
        '        MsgBox "Please select services either Credit Applicable or Not Applicable Only.", vbInformation
        '        ValidateHSNCode = False
        '        Exit Function
        '    End If
        '
        '    If mCount = mCountReverseChargeApp Then
        '        chkReverserChargeApp.Value = vbChecked
        '        ValidateHSNCode = True
        '    ElseIf mCount = mCountReverseChargeNotApp Then
        '        chkReverserChargeApp.Value = vbUnchecked
        '        ValidateHSNCode = True
        '    Else
        '        MsgBox "Please select services either Reverse Charge Applicable or Not Applicable Only.", vbInformation
        '        ValidateHSNCode = False
        '        Exit Function
        '    End If
        '
        '    If chkReverserChargeApp.Value = vbChecked Then
        '        cboGSTStatus.ListIndex = 1
        '    Else
        '        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mGSTRegNo = MasterNo
        '        End If
        '        If Trim(mGSTRegNo) = "" Then
        '            cboGSTStatus.ListIndex = 1
        '        Else
        '            cboGSTStatus.ListIndex = 0
        '        End If
        '    End If
        '
        '    Call CalcTots
        Exit Function
ErrPart:
        MsgBox(Err.Description)
    End Function
    Private Function GetPODetails(ByRef pPONO As Double, ByRef pItemDesc As String, ByRef pInvType As Double, ByRef pPORate As Double, ByRef pUOM As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mItemCode As String
        GetPODetails = False
        pInvType = -1
        pPORate = 0
        SqlStr = " SELECT ACCOUNT_POSTING_CODE, (NVL(ID.ITEM_PRICE,0) - ROUND((NVL(ID.ITEM_PRICE,0) * ID.ITEM_DIS_PER)/100,4)) AS ITEM_PRICE, ITEM_UOM " & vbCrLf & " FROM  PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N'" & vbCrLf & " AND AUTO_KEY_PO=" & Val(CStr(pPONO)) & "" & vbCrLf & " AND ID.WO_DESCRIPTION='" & MainClass.AllowSingleQuote(pItemDesc) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            pInvType = CDbl(Trim(IIf(IsDbNull(RsTemp.Fields("ACCOUNT_POSTING_CODE").Value), -1, RsTemp.Fields("ACCOUNT_POSTING_CODE").Value)))
            pPORate = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value), "0.0000"))
            pUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value))
        End If
        GetPODetails = True
        Exit Function
ErrPart:
        GetPODetails = False
    End Function
    Private Sub CheckRate()
        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemDesc
            If Trim(.Text) = "" Then Exit Sub
            .Col = ColRate
            If Val(.Text) <= 0 Then
                MsgInformation("Please Enter the Rate.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColRate)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function CheckQty() As Boolean
        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemDesc
            If Trim(.Text) = "" Then Exit Function
            If lblPurchaseType.Text = "W" Then
                .Col = ColUnit
                If Trim(.Text) <> "" Then
                    CheckQty = True
                Else
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColUnit)
                    Exit Function
                End If
            End If
            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    With SprdView
    '        If eventArgs.row = 0 Then Exit Sub
    '        .Row = eventArgs.row
    '        .Col = 1
    '        txtVNoPrefix.Text = .Text
    '        .Col = 2
    '        txtVNo.Text = VB6.Format(.Text, "00000")
    '        .Col = 3
    '        txtVNoSuffix.Text = .Text
    '        .Col = 5
    '        txtVDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
    '        .Col = 6
    '        txtModvatNo.Text = VB6.Format(.Text, "00000")
    '        .Col = 7
    '        txtModvatDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
    '        txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
    '        CmdView_Click(CmdView, New System.EventArgs())
    '    End With
    'End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick


        Dim mBillNoPrefix As String
        Dim mBillNo As String
        Dim mBillNoSuffix As String
        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn
        Dim mVDate As String

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)


        mBillNoPrefix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))
        mBillNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))       ''ultrow.SetCellValue(m_udtColumns.EntryNo, dtRow.Item("EntryNo"))
        mBillNoSuffix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(2))

        mVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(4))

        txtVNoPrefix.Text = mBillNoPrefix

        txtVNo.Text = VB6.Format(mBillNo, "00000")

        txtVNoSuffix.Text = mBillNoSuffix

        txtVDate.Text = VB6.Format(mVDate, "DD/MM/YYYY")
        'txtModvatNo.Text = VB6.Format(.Text, "00000")
        'txtModvatDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
        'ChkCapital.CheckState = IIf(VB.Left(.Text, 1) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


        txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))

        CmdView_Click(CmdView, New System.EventArgs())


    End Sub
    Private Sub SearchAdvanceVNo()
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mDivisionCode As Double
        Dim xSupplierCode As Double
        Dim mVNo As String
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSupplierCode = MasterNo
        End If
        mVNo = ""
        If Val(CStr(Val(txtVno.Text))) > 0 Then
            mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVno.Text), "00000") & Trim(txtVNoSuffix.Text))
        End If
        SqlStr = " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE FROM ("
        SqlStr = SqlStr & vbCrLf & " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE " & vbCrLf & " FROM FIN_ADVANCE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "' AND BOOKTYPE='AP'" & vbCrLf & " AND DIV_CODE = " & mDivisionCode & "" & vbCrLf & " AND VDATE <= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " GROUP BY VNO, VDATE"
        SqlStr = SqlStr & vbCrLf & " UNION "
        SqlStr = SqlStr & vbCrLf & " SELECT ADV_VNO AS VNO, ADV_VDATE AS VDATE, SUM(ADV_ADJUSTED_AMT*-1) AS ADV_ADJUSTED_AMT " & vbCrLf & " FROM FIN_PURCHASE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "'" & vbCrLf & " AND DIV_CODE = " & mDivisionCode & "" & vbCrLf & " AND VDATE <= TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If mVNo <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR || VNO <> " & RsCompany.Fields("FYEAR").Value & " || '" & mVNo & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " GROUP BY ADV_VNO, ADV_VDATE HAVING SUM(ADV_ADJUSTED_AMT) <>0 "
        SqlStr = SqlStr & vbCrLf & ") GROUP BY VNO, VDATE HAVING SUM(NETVALUE)>0"
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtAdvVNo.Text = AcName
            txtAdvVNo_Validating(txtAdvVNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtAdvAdjust_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvAdjust.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvBal_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvBal.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvBal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvBal.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvAdjust_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvAdjust.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvAdjust_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvAdjust.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvCGST.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvCGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvCGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvIGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvSGST.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvSGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvIGST.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvIGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvSGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvVNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvVNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvVNo.DoubleClick
        Call SearchAdvanceVNo()
    End Sub
    Private Sub txtAdvVNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvVNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAdvanceVNo()
    End Sub
    Private Sub txtAdvVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mDivisionCode As Double
        If txtAdvVNo.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE = " & mDivisionCode & " AND BOOKTYPE='AP'"
        If MainClass.ValidateWithMasterTable((txtAdvVNo.Text), "VNO", "VDATE", "FIN_ADVANCE_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
            txtAdvDate.Text = VB6.Format(MasterNo, "DD/MM/YYYY")
        Else
            MsgInformation("No Such Advance Voucher")
            Cancel = True
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
    Private Sub txtExpDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtExpDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtExpDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        '    If CDate(txtExpDate.Text) > CDate(TxtVDate.Text) Then
        '        MsgInformation "Exp Date Cann't be great than Voucher Date."
        '        Cancel = True
        '        Exit Sub
        '    End If
        If CDate(txtExpDate.Text) > CDate(txtBillDate.Text) Then
            MsgInformation("Exp Date could not be Greater than to Bill Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtItemAdvAdjust_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemAdvAdjust.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemAdvAdjust_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemAdvAdjust.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemAdvAdjust_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemAdvAdjust.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtBillDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtBillDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        If CDate(txtBillDate.Text) > CDate(PubCurrDate) Then
            MsgInformation("Bill Date Cann't be great than Current Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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
    Private Sub txtESIAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
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
    Private Sub txtModvatDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModvatDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtModvatDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtModvatDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtModvatDate.Text) = "" Then
            MsgBox("Modvat Date Cann't be Blank", MsgBoxStyle.Information)
            GoTo EventExitSub
        End If
        If Not IsDate(txtModvatDate.Text) Then
            MsgBox("Invalid Modvat Date", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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
    Private Sub SearchProvidedMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'"
        If MainClass.SearchGridMaster((txtServProvided.Text), "GEN_HSN_MST", "HSN_DESC", "HSN_CODE", , , SqlStr) = True Then
            txtServProvided.Text = AcName
            txtServProvided_Validating(txtServProvided, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtPaymentDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentdate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPaymentDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaymentdate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPaymentDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPaymentDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
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
    Private Sub txtServiceTaxAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServiceTaxAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
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
        CalcTots()
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
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mReverseChargeApp As String
        Dim mReverseChargePer As Double
        Dim mServiceTaxOn As Double
        Dim cntRow As Integer
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mLocal As String
        Dim mGSTRegNo As String
        Dim mGSTCreditApp As String
        Dim xAcctCode As String

        If Trim(txtSupplier.Text) = "" Then GoTo EventExitSub
        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub
        mLocal = "N"
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xAcctCode = MasterNo
            End If
            If chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Checked Then
                mLocal = "N"
            Else
                If Trim(txtSupplier.Text) <> "" Then
                    mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
                End If
            End If

            'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = MasterNo
            'End If
        End If
        '    cboGSTStatus.List5Index = -1
        txtProviderPer.Text = "0.00"
        txtRecipientPer.Text = "0.00"
        SqlStr = " SELECT HSN_CODE, HSN_DESC, CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " REVERSE_CHARGE_APP, GST_APP " & vbCrLf & " FROM GEN_HSN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HSN_DESC='" & MainClass.AllowSingleQuote((txtServProvided.Text)) & "' AND CODETYPE='S'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
        Else
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
            GoTo EventExitSub
        End If
        CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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
    Private Sub txtSTDSAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
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
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub
    Private Sub SearchSupplier()
        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C','O','2')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtsupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim xSuppCode As String
        Dim mDivisionCode As Double
        Dim mVNo As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        If txtSupplier.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        mVNo = ""
        If Val(CStr(Val(txtVno.Text))) > 0 Then
            mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVno.Text), "00000") & Trim(txtVNoSuffix.Text))
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtSupplier.Text = UCase(Trim(txtSupplier.Text))
            xSuppCode = MasterNo
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
            GoTo EventExitSub
        End If

        txtBillTo.Text = GetDefaultLocation(xSuppCode)

        txtAdvBal.Text = CStr(GetBalancePaymentAmount(xSuppCode, (txtBillDate.Text), mVNo, (TxtVDate.Text), mDivisionCode, "AP", mBalCGST, mBalSGST, mBalIGST))
        '    txtAdvCGSTBal.Text = Format(mBalCGST, "0.00")
        '    txtAdvSGSTBal.Text = Format(mBalSGST, "0.00")
        '    txtAdvIGSTBal.Text = Format(mBalIGST, "0.00")
        '    txtServProvided_Validate True
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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
    Private Sub txtTDSAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
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
    Private Sub txtTdsRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTdsRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTdsRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTdsRate.Text = VB6.Format(txtTdsRate.Text, "0.000")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(TxtVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(TxtVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk((TxtVDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub txtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMKey As String
        Dim mVNo As String
        Dim SqlStr As String
        If Trim(txtVno.Text) = "" Then GoTo EventExitSub
        txtVno.Text = VB6.Format(Val(txtVno.Text), "00000")
        If MODIFYMode = True And RsPurchMain.EOF = False Then xMKey = RsPurchMain.Fields("mKey").Value
        mVNo = Trim(Trim(txtVNoPrefix.Text) & Trim(txtVno.Text) & Trim(txtVNoSuffix.Text))
        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND VNo='" & MainClass.AllowSingleQuote(mVNo) & "' " & vbCrLf & " AND BookCode=" & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'"
        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            SqlStr = SqlStr & vbCrLf & " AND PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPurchMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_PURCHASE_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMKey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String
        Dim nMkey As String
        Dim mTRNType As String
        Dim mVNoSeq As Double
        Dim mVNo As String
        Dim mSuppCustCode As String
        Dim mModvatSuppCode As String
        Dim mAccountCode As String
        Dim mFREIGHTCHARGES As String
        Dim mEXEMPT_NOTIF_NO As String
        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double
        Dim mTotEDAmount As Double
        Dim mTotEDUAmount As Double
        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mPRINTED As String
        Dim mCancelled As String
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mBookCode As Integer
        Dim mStartingNo As Double
        Dim mSTPERCENT As Double
        Dim mTOTFREIGHT As Double
        Dim mEDPERCENT As Double
        Dim mEDUPERCENT As Double
        Dim mSHECPercent As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mIsGSTRefund As String
        Dim mSRBillNo As String
        Dim mSRBillDate As String
        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mTotDiscount As Double
        Dim mREJECTION As String
        Dim pDueDate As String
        Dim mModvatNo As Integer
        Dim mSERVNo As Integer
        Dim mSTCLAIMNo As Integer
        Dim mCapital As String
        Dim mNarration As String
        Dim mISMODVAT As String
        Dim mIsServClaim As String
        Dim mISSTREFUND As String
        Dim mISCSTREFUND As String
        Dim mFinalPost As String
        Dim mItemType As String
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim mPreviousRJ As Double
        Dim mAlreadyRejQty As Double
        Dim mDNCNQty As Double
        Dim xDebitAmt As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mPDIRItem As Integer
        Dim PDIRAmount As Double
        Dim mDNCNCreated As Boolean
        Dim xExpDiffDN As Boolean
        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mISFOC As String
        Dim mIsSuppBill As String
        Dim mServTax_Repost As String
        Dim mApproved As String
        Dim mItemCode As String
        Dim mModvatType As Integer
        Dim mISFixAssets As String
        Dim mItemDesc As String
        Dim mModvatAmount As Double
        Dim mLocal As String
        Dim mDivisionCode As Double
        Dim mSACCode As String
        Dim RsPostSRTrn As ADODB.Recordset
        Dim xItemValue As Double
        Dim xTOTEXPAMT As Double
        Dim xTotED As Double
        Dim xTotST As Double
        Dim xModvatAmount As Double
        Dim xCESSAmount As Double
        Dim xSHECAmount As Double
        Dim xServiceAmount As Double
        Dim xEDUAmount As Double
        Dim xSHEC As Double
        Dim xSTClaimAmount As Double
        Dim xNETVALUE As Double
        Dim xSurOnVat As Double
        Dim xSurcharge As Double
        Dim mFirstRow As Boolean
        Dim mSubRowNo As Integer
        Dim mGSTNo As Double
        Dim mTotGSTAmount As Double
        Dim mShipTo As String
        Dim mShipToCode As String
        'Dim mSaleBillNoPrefix As String
        'Dim mSaleBillNoSeq As Double
        'Dim mSaleBillNo As String
        'Dim mSaleBillDate As String
        Dim mNewGSTNo As Boolean
        Dim mGoodServ As String
        Dim mGSTCreditApp As String
        'Dim mReverserChargeApp As String
        Dim mDebitAccountDesc As String
        Dim mIsReversalApp As String
        Dim pSectionCode As Long
        Dim mPOSOtherLocation As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mGSTCreditApp = IIf(Val(txtTotCGSTRefund.Text) + Val(txtTotSGSTRefund.Text) + Val(txtTotIGSTRefund.Text) > 0, "Y", "N")
        '    mReverserChargeApp = "N"
        mGoodServ = "S"
        '    With SprdMain
        '        For cntRow = 1 To .MaxRows - 1
        '            .Row = cntRow
        '            .Col = ColCreditApp
        '            If .Value = vbChecked Then
        '                mGSTCreditApp = "Y"
        '            End If
        '            .Col = ColRCApp
        '            If .Value = vbChecked Then
        '                mReverserChargeApp = "Y"
        '            End If
        '        Next
        '    End With
        mNewGSTNo = False
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        '    If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "GST_APP", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mGSTCreditApp = Trim(MasterNo)
        '    Else
        '        mGSTCreditApp = "N"
        '    End If
        mFormRecdCode = -1
        mFormDueCode = -1
        mFinalPost = "Y"
        chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked

        mPOSOtherLocation = IIf(chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        mShipTo = "Y"
        mShipToCode = mSuppCustCode
        mAccountCode = "-1"
        If chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Checked Then
            mLocal = "N"
        Else
            mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        End If

        'If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = Trim(MasterNo)
        'Else
        '    mLocal = "N"
        'End If
        '*********
        mModvatSuppCode = CStr(-1)
        '*************
        mFREIGHTCHARGES = IIf(OptFreight(0).Checked = True, "To Pay", "Paid")
        mEXEMPT_NOTIF_NO = ""
        mBookCode = CInt(LblBookCode.Text)
        mItemValue = Val(lblTotItemValue.Text)
        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)
        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)
        mRO = Val(lblRO.Text)
        mSURAmount = Val(lblSurcharge.Text)
        mTotQty = Val(lblTotQty.Text)
        mLSTCST = ""
        mWITHFORM = ""
        mPRINTED = "N"
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsRegdNo = "N"
        mREJECTION = "N"
        mCapital = "N"
        mISMODVAT = "N"
        mIsGSTRefund = VB.Left(cboGSTStatus.Text, 1)
        mIsServClaim = "N"
        mIsServClaim = "N"
        mISSTREFUND = "N"
        mISCSTREFUND = "N"
        mISTDSDEDUCT = IIf(chkTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISESIDEDUCT = IIf(chkESI.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISSTDSDEDUCT = IIf(ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mServTax_Repost = "N"
        mISFOC = "N" ''IIf(chkFOC.Value = vbChecked, "Y", "N")
        mIsSuppBill = "N"
        mSTType = "0"
        mTotGSTAmount = Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text)
        mGSTNo = 0
        If VB.Left(cboGSTStatus.Text, 1) = "G" And mGSTCreditApp = "Y" Then
            mStartingNo = 1
            If Trim(lblGSTClaimNo.Text) = "" Or Val(lblGSTClaimNo.Text) = 0 Then
                mGSTNo = CDbl(AutoGenSeqGSTNo())
                mNewGSTNo = True
            Else
                mGSTNo = Val(lblGSTClaimNo.Text)
            End If
        End If
        '    mStartingNo = 100001
        If Val(txtVno.Text) > 0 Then
            mVNoSeq = Val(txtVno.Text)
        Else

            mVNoSeq = CDbl(AutoGenSeqBillNoNew("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode))

        End If
        mModvatNo = 0
        txtVNo.Text = IIf(mVNoSeq = -1 Or mVNoSeq = 0, "", VB6.Format(Val(CStr(mVNoSeq)), "00000"))
        lblGSTClaimNo.Text = VB6.Format(Val(CStr(mGSTNo)), "00000")

        If mIsAuthorisedUser = False Then
            If CheckValidVDate(mVNoSeq, mDivisionCode, 100001) = False Then GoTo ErrPart
        End If

        '    txtNarration.Text = GetNarration
        mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "00000") & Trim(txtVNoSuffix.Text))
        SqlStr = ""
        If VB.Left(cboGSTStatus.Text, 1) = "G" And mGSTCreditApp = "Y" Then
            mModvatType = 1
        Else
            mModvatType = 0
        End If
        If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mSACCode = Trim(MasterNo)
        Else
            mSACCode = ""
        End If

        pSectionCode = -1

        If Trim(txtSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSectionCode = MasterNo
            End If
        End If

        ''" & mGSTCreditApp & "', '" & mReverserChargeApp & "'
        '    .Enabled = IIf(.Fields("IS_CREDITAPP").Value = "Y", True, False)
        '            .Enabled = IIf(.Fields("IS_REVERSECHARGEAPP").Value = "Y", True, False)
        '    If Left(cboGSTStatus.Text, 1) = "R" Then
        ''        If ADDMode = True Then
        ''            mSaleBillNoPrefix = "S"
        ''            mSaleBillNoSeq = AutoGenSeqSaleBillNo(lblPurchaseType.text)
        ''            mSaleBillNo = mSaleBillNoPrefix & vb6.Format(mSaleBillNoSeq, "00000000")
        ''            mSaleBillDate = Format(TxtVDate.Text, "DD/MM/YYYY")
        ''        Else
        '            mSaleBillNoPrefix = "S"
        '            mSaleBillNoSeq = Val(lblSaleBillNoSeq.text)
        '            mSaleBillNo = lblSaleBillNo.text
        '            mSaleBillDate = Format(lblSaleBillDate.text, "DD/MM/YYYY")
        ''        End If
        '    Else
        '        mSaleBillNoPrefix = ""
        '        mSaleBillNoSeq = 0
        '        mSaleBillNo = ""
        '        mSaleBillDate = ""
        '    End If
        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_HDR", (LblMKey.Text), RsPurchMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_DET", (LblMKey.Text), RsPurchDetail, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_EXP", (LblMKey.Text), RsPurchExp, "MKEY", "M") = False Then GoTo ErrPart
        End If

        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_PURCHASE_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey

            SqlStr = "INSERT INTO FIN_PURCHASE_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, ROWNO," & vbCrLf & " TRNTYPE, VNOPREFIX, VNOSEQ, VNOSUFFIX, VNO, VDATE, " & vbCrLf & " BILLNO, INVOICE_DATE, AUTO_KEY_MRR, MRRDATE," & vbCrLf & " CUSTREFNO, CUSTREFDATE, SUPP_CUST_CODE, MODVAT_SUPP_CODE, ACCOUNTCODE," & vbCrLf & " ST_38_NO, DUEDAYSFROM, DUEDAYSTO, DESPATCHMODE," & vbCrLf & " DOCSTHROUGH, VEHICLENO, CARRIERS, FREIGHTCHARGES," & vbCrLf & " TARIFFHEADING, EXEMPT_NOTIF_NO," & vbCrLf & " BOOKCODE, BOOKTYPE, BOOKSUBTYPE, " & vbCrLf & " REMARKS, ITEMDESC, ITEMVALUE," & vbCrLf & " TOTSTAMT, TOTCHARGES, " & vbCrLf & " TOTEDAMOUNT, TOTEXPAMT, NETVALUE, TOTQTY,  " & vbCrLf & " STTYPE, STFORMCODE, STFORMNAME, STFORMNO, " & vbCrLf & " STFORMDATE, STDUEFORMCODE, STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE," & vbCrLf & " ISREGDNO, LSTCST, WITHFORM, " & vbCrLf & " CANCELLED, REJECTION,  NARRATION,  " & vbCrLf & " STPERCENT,TOTFREIGHT,EDPERCENT,TOTTAXABLEAMOUNT,  " & vbCrLf & " TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT,TOTRO,  " & vbCrLf & " MODVATNO, MODVATDATE, MODVATPER, MODVATAMOUNT, " & vbCrLf & " STCLAIMNO, STCLAIMDATE, STCLAIMPER, STCLAIMAMOUNT,ISCAPITAL, PAYMENTDATE, " & vbCrLf & " ISMODVAT,ISSTREFUND, ISCSTREFUND, ISFINALPOST,ISTDSDEDUCT,TDSPER,TDSAMOUNT, " & vbCrLf & " ISESIDEDUCT,ESIPER,ESIAMOUNT, ISSTDSDEDUCT,STDSPER,STDSAMOUNT," & vbCrLf & " TDS_DEDUCT_ON, STDS_DEDUCT_ON, ESI_DEDUCT_ON, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE, "
            SqlStr = SqlStr & vbCrLf & " MODVATItemValue, " & vbCrLf & " TOTEDUPERCENT,TOTEDUAMOUNT," & vbCrLf & " CESSABLEAMOUNT,CESSPER,CESSAMOUNT," & vbCrLf & " ISFOC,ISSUPPBILL,"
            SqlStr = SqlStr & vbCrLf & " TOTSERVICEPERCENT,TOTSERVICEAMOUNT, " & vbCrLf & " SERVNO, SERVDATE, " & vbCrLf & " ISSERVCLAIM, " & vbCrLf & " SERVCLAIMPERCENT, SERVICECLAIMAMOUNT, ISSERVTAX_POST,SERV_PROV, "
            SqlStr = SqlStr & vbCrLf & " SHECMODVATPER,SHECMODVATAMOUNT, SHECPERCENT, SHECAMOUNT, " & vbCrLf _
                & " ADEMODVATPER,ADEMODVATAMOUNT, ADEAMOUNT,UPDATE_FROM,MODVAT_TYPE,SUR_VATCLAIMAMOUNT,DIV_CODE," & vbCrLf _
                & " SAC_CODE, SERVICE_ON_AMT, SERV_PROVIDER_PER, " & vbCrLf _
                & " SERV_RECIPIENT_PER,SERVICE_TAX_PER,SERVICE_TAX_AMOUNT,KK_CESS_PER,KK_CESS_AMOUNT, " & vbCrLf _
                & " ISGSTAPPLICABLE, GST_CLAIM_NO, GST_CLAIM_DATE, " & vbCrLf _
                & " TOTALGSTVALUE, TOTCGST_REFUNDAMT, TOTSGST_REFUNDAMT, " & vbCrLf _
                & " TOTIGST_REFUNDAMT, TOTCGST_AMOUNT, TOTSGST_AMOUNT, " & vbCrLf _
                & " TOTIGST_AMOUNT, SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,PURCHASE_TYPE, " & vbCrLf _
                & " ADV_VNO, ADV_VDATE, ADV_ADJUSTED_AMT, " & vbCrLf _
                & " ADV_CGST_AMT, ADV_SGST_AMT, ADV_IGST_AMT,ADV_ITEM_AMT,PURCHASESEQTYPE,BILL_TO_LOC_ID , SHIP_TO_LOC_ID, SECTION_CODE,SUPPLY_OTHER_LOCATION" & vbCrLf & " )"

            SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & mCurRowNo & ", " & vbCrLf & " " & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "', " & vbCrLf & " " & mVNoSeq & ", '" & MainClass.AllowSingleQuote(txtVNoSuffix.Text) & "', '" & MainClass.AllowSingleQuote(mVNo) & "',TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "',TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " -1, TO_DATE('" & VB6.Format(txtExpDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPONo.Text) & "',TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mSuppCustCode & "', '" & mModvatSuppCode & "', '" & mAccountCode & "','', " & vbCrLf & " " & Val(txtCreditDays(0).Text) & ", " & Val(txtCreditDays(1).Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtMode.Text) & "', '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "', '" & mFREIGHTCHARGES & "', " & vbCrLf & " '', '" & mEXEMPT_NOTIF_NO & "', " & vbCrLf & " '" & mBookCode & "', '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & MainClass.AllowSingleQuote(txtItemType.Text) & "', " & mItemValue & ", " & vbCrLf & " " & mTOTSTAMT & ", " & mTOTCHARGES & ", " & mTotEDAmount & ", " & vbCrLf & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf & " '" & mSTType & "'," & mFormRecdCode & ",'','', '', " & vbCrLf & " " & mFormDueCode & ",'','', '', " & vbCrLf & " '" & mIsRegdNo & "', '" & mLSTCST & "', " & vbCrLf & " '" & mWITHFORM & "', " & vbCrLf & " '" & mCancelled & "', '" & mREJECTION & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "',  " & vbCrLf & "" & mSTPERCENT & "," & mTOTFREIGHT & "," & mEDPERCENT & "," & mTOTTAXABLEAMOUNT & "," & vbCrLf & "" & mSURAmount & "," & mTotDiscount & "," & mMSC & "," & mRO & ", "
            SqlStr = SqlStr & vbCrLf & " '" & mModvatNo & "', TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 0,0, " & vbCrLf & " '" & mSTCLAIMNo & "','',0,0, '" & mCapital & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf & " '" & mISMODVAT & "','" & mISSTREFUND & "','" & mISCSTREFUND & "', '" & mFinalPost & "'," & vbCrLf & " '" & mISTDSDEDUCT & "'," & Val(txtTDSRate.Text) & ", " & Val(txtTDSAmount.Text) & ", " & vbCrLf & " '" & mISESIDEDUCT & "'," & Val(txtESIRate.Text) & ", " & Val(txtESIAmount.Text) & ", " & vbCrLf & " '" & mISSTDSDEDUCT & "'," & Val(txtSTDSRate.Text) & ", " & Val(txtSTDSAmount.Text) & ", " & vbCrLf & " '" & Val(txtTDSDeductOn.Text) & "'," & Val(txtSTDSDeductOn.Text) & ", " & Val(txtESIDeductOn.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " '','',0,0,0,0,0,0, " & vbCrLf & " '" & mISFOC & "','" & mIsSuppBill & "',"
            SqlStr = SqlStr & vbCrLf & " 0," & vbCrLf & " 0," & vbCrLf & " '" & mSERVNo & "', ''," & vbCrLf & " '" & mIsServClaim & "', " & vbCrLf & " 0, 0," & vbCrLf & " '" & mServTax_Repost & "','" & MainClass.AllowSingleQuote(txtServProvided.Text) & "',"
            SqlStr = SqlStr & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, 'N','" & mModvatType & "',0," & mDivisionCode & "," & vbCrLf & " '" & mSACCode & "', " & Val(txtServiceOn.Text) & ", " & Val(txtProviderPer.Text) & ", " & Val(txtRecipientPer.Text) & ", " & vbCrLf & " " & Val(txtServiceTaxPer.Text) & "," & Val(txtServiceTaxAmount.Text) & ",0,0, " & vbCrLf & " '" & mIsGSTRefund & "', " & Val(CStr(mGSTNo)) & ", TO_DATE('" & VB6.Format(lblGSTClaimDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(CStr(mTotGSTAmount)) & ", " & Val(txtTotCGSTRefund.Text) & ", " & Val(txtTotSGSTRefund.Text) & ", " & vbCrLf & " " & Val(txtTotIGSTRefund.Text) & ", " & Val(lblTotCGSTAmount.Text) & ", " & Val(lblTotSGSTAmount.Text) & "," & vbCrLf & " " & Val(lblTotIGSTAmount.Text) & ",'" & mShipTo & "', '" & mShipToCode & "', '" & lblPurchaseType.Text & "'," & vbCrLf & " '" & Trim(txtAdvVNo.Text) & "', TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtAdvAdjust.Text) & ", " & vbCrLf _
                & " " & Val(txtAdvCGST.Text) & ", " & Val(txtAdvSGST.Text) & ", " & Val(txtAdvIGST.Text) & ", " & Val(txtItemAdvAdjust.Text) & "," & Val(lblPurchaseSeqType.Text) & ",'" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "', " & IIf(pSectionCode = -1, "NULL", pSectionCode) & ",'" & mPOSOtherLocation & "'" & vbCrLf _
                & " )" ''
        ElseIf MODIFYMode = True Then

            SqlStr = ""
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET " & vbCrLf & " VNOPREFIX = '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "'," & vbCrLf & " VNOSEQ= " & mVNoSeq & ", TRNTYPE=" & Val(mTRNType) & "," & vbCrLf & " VNOSUFFIX= '" & MainClass.AllowSingleQuote(txtVNoSuffix.Text) & "'," & vbCrLf & " VNO= '" & MainClass.AllowSingleQuote(mVNo) & "'," & vbCrLf & " VDATE= TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " BILLNO= '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf & " INVOICE_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " MRRDATE= TO_DATE('" & VB6.Format(txtExpDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CUSTREFNO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf & " CUSTREFDATE= TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " MODVAT_SUPP_CODE= '" & mModvatSuppCode & "'," & vbCrLf & " ACCOUNTCODE= '" & mAccountCode & "'," & vbCrLf & " ST_38_NO= '', "
            SqlStr = SqlStr & vbCrLf & " SECTION_CODE= " & IIf(pSectionCode = -1, "NULL", pSectionCode) & ", DUEDAYSFROM= " & Val(txtCreditDays(0).Text) & "," & vbCrLf & " DUEDAYSTO= " & Val(txtCreditDays(1).Text) & ", " & vbCrLf & " DESPATCHMODE= '" & MainClass.AllowSingleQuote(txtMode.Text) & "', " & vbCrLf & " DOCSTHROUGH= '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "'," & vbCrLf & " VEHICLENO= '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " CARRIERS=  '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "'," & vbCrLf & " FREIGHTCHARGES= '" & mFREIGHTCHARGES & "', " & vbCrLf & " EXEMPT_NOTIF_NO= '" & MainClass.AllowSingleQuote(mEXEMPT_NOTIF_NO) & "',"
            SqlStr = SqlStr & vbCrLf & " BOOKCODE= " & mBookCode & "," & vbCrLf & " BOOKTYPE= '" & mBookType & "'," & vbCrLf & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " ITEMDESC= '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & "," & vbCrLf & " TOTSTAMT= " & mTOTSTAMT & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & "," & vbCrLf & " TOTEDAMOUNT= " & mTotEDAmount & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " NETVALUE= " & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & "," & vbCrLf & " STTYPE= '" & mSTType & "'," & vbCrLf & " REJECTION='" & mREJECTION & "'," & vbCrLf & " STFORMCODE= " & mFormRecdCode & "," & vbCrLf & " STFORMNAME= ''," & vbCrLf & " STFORMNO= ''," & vbCrLf & " STFORMDATE='', " & vbCrLf & " STDUEFORMCODE= " & mFormDueCode & "," & vbCrLf & " STDUEFORMNAME= ''," & vbCrLf & " STDUEFORMNO= ''," & vbCrLf & " STDUEFORMDATE= '',"
            SqlStr = SqlStr & vbCrLf & " STPERCENT=" & mSTPERCENT & "," & vbCrLf & " TOTFREIGHT=" & mTOTFREIGHT & "," & vbCrLf & " EDPERCENT=" & mEDPERCENT & "," & vbCrLf & " TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & "," & vbCrLf & " ISREGDNO= '" & mIsRegdNo & "'," & vbCrLf & " LSTCST= '" & mLSTCST & "', " & vbCrLf & " WITHFORM= '" & mWITHFORM & "'," & vbCrLf & " CANCELLED= '" & mCancelled & "'," & vbCrLf & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "'," & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & mTotDiscount & ", " & vbCrLf & " TOTMSCAMOUNT=" & mMSC & ", TotRO=" & mRO & "," & vbCrLf & " MODVATNO='" & mModvatNo & "', " & vbCrLf & " MODVATDATE=TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODVATPER=0, " & vbCrLf & " MODVATAMOUNT=0, " & vbCrLf & " TOTEDUPERCENT=0, " & vbCrLf & " TOTEDUAMOUNT=0, " & vbCrLf & " CESSABLEAMOUNT=0," & vbCrLf & " CESSPER=0, " & vbCrLf & " CESSAMOUNT=0, " & vbCrLf & " TDS_DEDUCT_ON=" & Val(txtTDSDeductOn.Text) & ", " & vbCrLf & " ISTDSDEDUCT='" & mISTDSDEDUCT & "'," & vbCrLf & " TDSPER=" & Val(txtTDSRate.Text) & ", TDSAMOUNT=" & Val(txtTDSAmount.Text) & ", "
            SqlStr = SqlStr & vbCrLf & " MODVATItemValue=0," & vbCrLf & " ESI_DEDUCT_ON=" & Val(txtESIDeductOn.Text) & ", " & vbCrLf & " ISESIDEDUCT='" & mISESIDEDUCT & "'," & vbCrLf & " ESIPER=" & Val(txtESIRate.Text) & ", " & vbCrLf & " ESIAMOUNT=" & Val(txtESIAmount.Text) & ", " & vbCrLf & " STDS_DEDUCT_ON=" & Val(txtSTDSDeductOn.Text) & ", " & vbCrLf & " ISSTDSDEDUCT='" & mISSTDSDEDUCT & "'," & vbCrLf & " STDSPER=" & Val(txtSTDSRate.Text) & ", " & vbCrLf & " STDSAMOUNT=" & Val(txtSTDSAmount.Text) & ", " & vbCrLf & " STCLAIMNO='" & mSTCLAIMNo & "', " & vbCrLf & " STCLAIMDATE='', " & vbCrLf & " STCLAIMPER=0, " & vbCrLf & " STCLAIMAMOUNT=0, " & vbCrLf & " ISCAPITAL='" & mCapital & "', PAYMENTDATE=TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ISMODVAT='" & mISMODVAT & "',ISSTREFUND='" & mISSTREFUND & "', " & vbCrLf & " ISCSTREFUND='" & mISCSTREFUND & "', ISFINALPOST='" & mFinalPost & "', " & vbCrLf & " ISFOC='" & mISFOC & "',ISSUPPBILL='" & mIsSuppBill & "', "
            SqlStr = SqlStr & vbCrLf & " TOTSERVICEPERCENT=0, " & vbCrLf & " TOTSERVICEAMOUNT=0, " & vbCrLf & " SERV_PROV='" & MainClass.AllowSingleQuote(txtServProvided.Text) & "',"
            SqlStr = SqlStr & vbCrLf & " SHECMODVATPER=0, " & vbCrLf & " SHECMODVATAMOUNT=0, " & vbCrLf & " SHECPERCENT=0," & vbCrLf & " SHECAMOUNT=0," & vbCrLf & " ADEMODVATPER=0, " & vbCrLf & " ADEMODVATAMOUNT=0, " & vbCrLf & " ADEAMOUNT=0," & vbCrLf & " UPDATE_FROM='N',MODVAT_TYPE='" & mModvatType & "',SUR_VATCLAIMAMOUNT= 0,"
            SqlStr = SqlStr & vbCrLf & " SAC_CODE='" & mSACCode & "'," & vbCrLf & " SERVICE_ON_AMT=" & Val(txtServiceOn.Text) & "," & vbCrLf & " SERV_PROVIDER_PER=" & Val(txtProviderPer.Text) & "," & vbCrLf & " SERV_RECIPIENT_PER=" & Val(txtRecipientPer.Text) & "," & vbCrLf & " SERVICE_TAX_PER=" & Val(txtServiceTaxPer.Text) & "," & vbCrLf & " SERVICE_TAX_AMOUNT=" & Val(txtServiceTaxAmount.Text) & "," & vbCrLf & " KK_CESS_PER=0," & vbCrLf & " KK_CESS_AMOUNT=0,"
            SqlStr = SqlStr & vbCrLf & " ISGSTAPPLICABLE='" & mIsGSTRefund & "', " & vbCrLf & " GST_CLAIM_NO=" & Val(CStr(mGSTNo)) & ",  " & vbCrLf & " GST_CLAIM_DATE=TO_DATE('" & VB6.Format(lblGSTClaimDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " GST_CLAIM='" & IIf(chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", lblClaimStatus.Text) & "', " & vbCrLf & " GST_CLAIM_NEW_NO=" & Val(txtModvatNo.Text) & ",  " & vbCrLf & " GST_CLAIM_NEW_DATE=TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TOTALGSTVALUE=" & Val(CStr(mTotGSTAmount)) & ",  " & vbCrLf & " TOTCGST_REFUNDAMT=" & Val(txtTotCGSTRefund.Text) & ",  " & vbCrLf & " TOTSGST_REFUNDAMT=" & Val(txtTotSGSTRefund.Text) & ", " & vbCrLf & " TOTIGST_REFUNDAMT=" & Val(txtTotIGSTRefund.Text) & ",  " & vbCrLf & " TOTCGST_AMOUNT=" & Val(lblTotCGSTAmount.Text) & ",  " & vbCrLf & " TOTSGST_AMOUNT=" & Val(lblTotSGSTAmount.Text) & ", " & vbCrLf & " TOTIGST_AMOUNT=" & Val(lblTotIGSTAmount.Text) & ",  " & vbCrLf & " SHIPPED_TO_SAMEPARTY='" & mShipTo & "',  " & vbCrLf & " SHIPPED_TO_PARTY_CODE='" & mShipToCode & "', PURCHASE_TYPE='" & lblPurchaseType.Text & "',"

            SqlStr = SqlStr & vbCrLf & " ADV_VNO = '" & Trim(txtAdvVNo.Text) & "'," & vbCrLf _
                & " ADV_VDATE = TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " ADV_ADJUSTED_AMT = " & Val(txtAdvAdjust.Text) & ", " & vbCrLf _
                & " ADV_CGST_AMT = " & Val(txtAdvCGST.Text) & ", " & vbCrLf _
                & " ADV_SGST_AMT = " & Val(txtAdvSGST.Text) & ", " & vbCrLf _
                & " ADV_IGST_AMT = " & Val(txtAdvIGST.Text) & ", " & vbCrLf _
                & " ADV_ITEM_AMT = " & Val(txtItemAdvAdjust.Text) & ", SUPPLY_OTHER_LOCATION='" & mPOSOtherLocation & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & ",PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ",BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "' ,SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'" & vbCrLf _
                & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)
        If VB.Left(cboGSTStatus.Text, 1) = "G" And mGSTCreditApp = "Y" And mNewGSTNo = True Then ''chkCancelled.Value = vbUnchecked
            If UpdateGSTSeqMaster(PubDBCn, (LblMKey.Text), LblBookCode.Text, mBookType, mBookSubType, mGSTNo, VB6.Format(lblGSTClaimDate.Text, "DD-MMM-YYYY"), mCapital, "N", "S") = False Then GoTo ErrPart
        End If
        mIsReversalApp = IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N")
        If UpdateDetail1(mNarration, mAccountCode, mVNo, mSuppCustCode, mShipTo, mShipToCode, mDivisionCode, mIsReversalApp) = False Then GoTo ErrPart
        pDueDate = txtPaymentdate.Text
        mIsGSTRefund = IIf(Val(txtTotCGSTRefund.Text) + Val(txtTotSGSTRefund.Text) + Val(txtTotIGSTRefund.Text) > 0, "Y", "N")
        SprdMain.Row = 1
        SprdMain.Col = ColAccountPostCode
        mDebitAccountDesc = Trim(SprdMain.Text)
        mAccountCode = GetDebitNameOfInvType(mDebitAccountDesc, "N")

        If PurchasePostTRNGST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(lblNetAmount.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), False, pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), Val(lblTotExpAmt.Text) + Val(lblTotalGSTTax.Text), mIsGSTRefund, Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), (txtExpDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, mIsReversalApp, 0, 0, 0, 0, "", "", txtBillTo.Text) = False Then GoTo ErrPart
        'If ADDMode = True Then

        If IIf(IsDBNull(RsCompany.Fields("MANNUAL_BILL_ADJUST").Value), "N", RsCompany.Fields("MANNUAL_BILL_ADJUST").Value) = "Y" Then
            If UpdatePaymentDetail1(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(lblNetAmount.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), False, pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), Val(lblTotExpAmt.Text) + Val(lblTotalGSTTax.Text), IIf(mIsGSTRefund = "G", "Y", "N"), Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), (txtExpDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(mIsGSTRefund = "R", "Y", "N"), Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), "", "", txtBillTo.Text) = False Then GoTo ErrPart
        End If


        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Or chkESI.CheckState = System.Windows.Forms.CheckState.Checked Or ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            Dim pJVTMKey As String = ""
            pJVTMKey = lblJVTMKey.Text
            If Val(txtTDSAmount.Text) + Val(txtESIAmount.Text) + Val(txtSTDSAmount.Text) > 0 Then
                If UpdateTDSVoucher(mDivisionCode, pJVTMKey) = False Then GoTo ErrPart
                SqlStr = "UPDATE FIN_PURCHASE_HDR SET JVNO='" & txtJVVNO.Text & "', " & vbCrLf _
                    & " JVT_MKEY='" & pJVTMKey & "'," & vbCrLf _
                    & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKEY='" & LblMKey.Text & "'"
                PubDBCn.Execute(SqlStr)
            End If
        End If
        'End If
        PubDBCn.CommitTrans()
        UpdateMain1 = True
        If ADDMode = True And Trim(txtJVVNO.Text) <> "" Then
            MsgBox("TDS Journal Voucher No. " & txtJVVNO.Text & " Created. ", MsgBoxStyle.Information)
        End If
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsPurchMain.Requery() ''.Refresh
        RsPurchDetail.Requery() ''.Refresh
        If ADDMode = True Then
            txtVNo.Text = ""
        End If
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume
    End Function
    Private Sub CalcAdvTots()
        On Error GoTo ERR1
        Dim mNetAdvanceAmount As Double
        txtItemAdvAdjust.Text = VB6.Format(txtItemAdvAdjust.Text, "0.00")
        mNetAdvanceAmount = Val(txtItemAdvAdjust.Text)
        txtAdvCGST.Text = VB6.Format(txtAdvCGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvCGST.Text)
        txtAdvSGST.Text = VB6.Format(txtAdvSGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvSGST.Text)
        txtAdvIGST.Text = VB6.Format(txtAdvIGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvIGST.Text)
        txtAdvAdjust.Text = VB6.Format(mNetAdvanceAmount, "0.00")
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Function UpdateTDSVoucher(ByRef mDivisionCode As Double, ByRef pJVTMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim SqlStr As String
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
        Dim CurJVMKey As String
        Dim mNarration As String
        Dim pAddMode As Boolean

        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)

        mVType = "JVT"
        If pJVTMKey = "" Then
            mVNo = GenJVVno(mVType)
            mVNoPrefix = GenPrefixVNo(txtVDate.Text)
            mVNoSuffix = ""
            mVnoStr = mVType & mVNoPrefix & mVNo & mVNoSuffix
            txtJVVNO.Text = mVnoStr
            pAddMode = True
        Else
            mVnoStr = txtJVVNO.Text
            pAddMode = False
        End If

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mNarration = txtSupplier.Text & " : TDS DEDUCT ON RS. " & VB6.Format(txtTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtTDSRate.Text, "0.000") & "% AGT BILL NO : " & txtBillNo.Text & " DATE : " & txtBillDate.Text

        mBookCode = CStr(ConJournalBookCode)
        If pAddMode = True Then
            pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurJVMKey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
            pJVTMKey = CurJVMKey
            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf _
                & " Vno, Vdate, BookType,BookSubType, " & vbCrLf _
                & " BookCode, Narration, CANCELLED, " & vbCrLf _
                & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE,IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY ) VALUES ( " & vbCrLf _
                & " '" & CurJVMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & pRowNo & ", " & vbCrLf _
                & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf _
                & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote(mNarration) & "', '" & mCancelled & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'N','N','')"
        Else            ''If MODIFYMode = True Then
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
                & " Vdate=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " Narration='" & MainClass.AllowSingleQuote(mNarration) & "', " & vbCrLf _
                & " CANCELLED='" & mCancelled & "', " & vbCrLf & " BookType='" & mBookType & "', " & vbCrLf _
                & " BookSubType='" & mBookSubType & "', " & vbCrLf _
                & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " Where Mkey='" & pJVTMKey & "'"
        End If
        PubDBCn.Execute(SqlStr)
        If UpdateJVDetail(pJVTMKey, pRowNo, mBookCode, mVType, mVnoStr, (txtVDate.Text), "", PubDBCn, mDivisionCode) = False Then GoTo ErrPart
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If UpdateTDSCreditDetail(pJVTMKey, mVnoStr, mBookType, mBookSubType, pAddMode) = False Then GoTo ErrPart
        End If
        '    txtVno.Text = mVNo
        UpdateTDSVoucher = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateTDSVoucher = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function GenJVVno(ByRef xBookType As String) As String
        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String
        Dim mBookType As String
        Dim mBookSubType As String
        ''    Call GenPrefixVNo
        ''
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        'If ADDMode = True Then
        SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BookType='" & mBookType & "'" & vbCrLf _
            & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
            & " AND VTYPE='" & MainClass.AllowSingleQuote(xBookType) & "'"

        If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

        End If

        GenJVVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")
        'End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume
    End Function
    Private Function UpdateJVDetail(ByRef mMkey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNo As String, ByRef mVDate As String, ByRef pNarration As String, ByRef pDBCn As ADODB.Connection, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrDetail
        Dim I As Integer
        Dim SqlStr As String
        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mChequeNo As String
        Dim mChqDate As String
        Dim mAmount As Double
        Dim mCCCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mExpCode As String
        Dim mIBRNo As String
        Dim mDC As String
        Dim mRemarks As String
        Dim mPRRowNo As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mClearDate As String
        Dim mParticulars As String
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        SqlStr = "Delete From FIN_TEMPBILL_TRN Where UserId='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMPMKEY=" & pProcessKey & ""
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        mRemarks = " agt Bill No(s) " & txtBillNo.Text & " Dt. " & txtBillDate.Text
        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMkey & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BooksubType='" & mBookSubType & "'"
        pDBCn.Execute(SqlStr)
        '    Call InsertTempBill(mAccountCode, mAmount, mRemarks)
        '******SUPPLIER ACCOUNT POSTING
        mAccountName = txtSupplier.Text
        If mAccountName <> "" Then
            mPRRowNo = 1
            mDC = "D"
            mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)
            mAmount = Val(txtTDSAmount.Text) + Val(txtESIAmount.Text) + Val(txtSTDSAmount.Text)
            mParticulars = "Bill No : " & txtBillNo.Text
            If Val(txtTDSAmount.Text) > 0 Then
                mParticulars = mParticulars & " (TDS DEDUCT ON RS. " & VB6.Format(txtTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtTDSRate.Text, "0.000") & "%)"
            End If

            If Val(txtESIAmount.Text) > 0 Then
                mParticulars = mParticulars & " (ESI DEDUCT ON RS. " & VB6.Format(txtESIDeductOn.Text, "0.000") & " @ " & VB6.Format(txtESIRate.Text, "0.000") & "%)"
            End If

            If Val(txtSTDSAmount.Text) > 0 Then
                mParticulars = mParticulars & " (STDS DEDUCT ON RS. " & VB6.Format(txtSTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtSTDSRate.Text, "0.000") & "%)"
            End If

            mChequeNo = ""
            mChqDate = ""
            mCCCode = "-1"
            mDeptCode = "-1"
            mEmpCode = "-1"
            mExpCode = "-1"
            mIBRNo = "-1"
            mClearDate = ""
            I = 1
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & MainClass.AllowSingleQuote(mAccountCode) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "','" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdateSuppPRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, mAmount, mRemarks, mDivisionCode) = False Then GoTo ErrDetail
        End If
        '******TDS ACCOUNT POSTING
        mPRRowNo = 2
        mDC = "C"
        'mAccountCode = IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        mAccountCode = GetTDSAccountCode(txtSection.Text)       ''' IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        If Trim(mAccountCode) = "" Then
            MsgInformation("TDS Head Not Defined.")
            UpdateJVDetail = False
            Exit Function
        End If
        mParticulars = ""
        mParticulars = "Bill No : " & txtBillNo.Text & " (TDS DEDUCT ON RS. " & VB6.Format(txtTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtTDSRate.Text, "0.000") & "%)"
        mAmount = Val(txtTDSAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 2
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & MainClass.AllowSingleQuote(mAccountCode) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtExpDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        '******ESI ACCOUNT POSTING
        mPRRowNo = 3
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("ESICREDITACCOUNT").Value), "-1", RsCompany.Fields("ESICREDITACCOUNT").Value)
        mParticulars = "Bill No : " & txtBillNo.Text & " (ESI DEDUCT ON RS. " & VB6.Format(txtESIDeductOn.Text, "0.000") & " @ " & VB6.Format(txtESIRate.Text, "0.000") & "%)"
        mAmount = Val(txtESIAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 3
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & MainClass.AllowSingleQuote(mAccountCode) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtExpDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        '******STDS ACCOUNT POSTING
        mPRRowNo = 4
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("STDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("STDSCREDITACCOUNT").Value)
        mParticulars = "Bill No : " & txtBillNo.Text & " (STDS DEDUCT ON RS. " & VB6.Format(txtSTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtSTDSRate.Text, "0.000") & "%)"
        mAmount = Val(txtSTDSAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 4
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & MainClass.AllowSingleQuote(mAccountCode) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " To_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtExpDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        UpdateJVDetail = True
        Exit Function
ErrDetail:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateJVDetail = False
        ''Resume
    End Function
    Public Function UpdateSuppPRDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pRowNo As Integer, ByRef pTRNDtlSubRow As Integer, ByRef pAccountCode As String, ByRef pBookCode As String, ByRef pVType As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pTrnDC As String, ByRef pTrnAmount As Double, ByRef pChequeNo As String, ByRef pChqDate As String, ByRef pCostCCode As String, ByRef pDeptCode As String, ByRef pEmpCode As String, ByRef pExpCode As String, ByRef pIBRNo As String, ByRef pClearDate As String, ByRef pLocked As String, ByRef pNarration As String, ByRef xAmount As Double, ByRef xRemarks As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrDetail
        Dim RsTempPRDetail As ADODB.Recordset
        Dim SqlStr As String
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
        Dim mAccountCode As String = ""
        pSubRowNo = 1000 * pRowNo
        pSubRowNo = pSubRowNo + 1
        pTRNType = "T"
        pBillNo = txtBillNo.Text
        pBillDate = txtBillDate.Text
        pBillAmount = Val(lblNetAmount.Text)
        pBillDC = "C"
        pAmount = xAmount
        pDC = "D"
        pRemarks = xRemarks
        pDueDate = txtPaymentdate.Text
        If GetAccountBalancingMethod(pAccountCode, True) = "D" Then
            SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf _
                & " COMPANY_CODE, BILL_TO_LOC_ID, BOOKTYPE, MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf _
                & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf _
                & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE,BILL_COMPANY_CODE ) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & txtBillTo.Text & "', '" & pBookType & "', '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf _
                & " '" & pAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & RsCompany.Fields("COMPANY_CODE").Value & ") "
            pDBCn.Execute(SqlStr)
        End If
        If pTRNType = "N" Then
            pBillType = "B"
        ElseIf pTRNType = "B" Or pTRNType = "O" Or pTRNType = "A" Then
            pBillType = "P"
        Else
            pBillType = pTRNType
        End If
        mAccountCode = IIf(MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, "-1")

        If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, (txtExpDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, txtBillTo.Text) = False Then GoTo ErrDetail
        UpdateSuppPRDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateSuppPRDetail = False
        'Resume
    End Function
    Private Function UpdateTDSCreditDetail(ByRef pMKey As String, ByRef pVNoStr As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pAddMode As Boolean) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String
        Dim mTDSAccountCode As String
        Dim mExempted As String
        Dim mSectionCode As Integer
        Dim mAmountPaid As Double
        Dim mTdsRate As Double
        Dim mPartyName As String
        Dim mPartyCode As String
        Dim xAddMode As Boolean
        SqlStr = ""

        'SqlStr = "DELETE FROM TDS_TRN WHERE MKey= '" & pMKey & "'"
        'PubDBCn.Execute(SqlStr)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked Or chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            UpdateTDSCreditDetail = True
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(pMKey, "MKEY", "MKEY", "TDS_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAddMode = False
        Else
            xAddMode = True
        End If

        mTDSAccountCode = GetTDSAccountCode(txtSection.Text)       'IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        If mTDSAccountCode = "" Then
            ErrorMsg("TDS ACCOUNT Code not Defined into System Pref.", "", MsgBoxStyle.Critical)
            UpdateTDSCreditDetail = False
        End If

        mPartyName = Trim(txtSupplier.Text)
        If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyCode = MasterNo
        Else
            mPartyCode = "-1"
        End If
        'If MainClass.ValidateWithMasterTable(mPartyCode, "SUPP_CUST_CODE", "SECTIONCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mSectionCode = MasterNo
        'Else
        '    mSectionCode = CInt("-1")
        'End If

        mSectionCode = -1

        If Trim(txtSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSectionCode = MasterNo
            End If
        End If

        mAmountPaid = Val(CStr(CDbl(txtTDSDeductOn.Text)))
        mTdsRate = Val(txtTDSRate.Text)
        mExempted = "N"
        If xAddMode = True Then
            SqlStr = "INSERT INTO TDS_TRN ( MKEY, COMPANY_CODE, " & vbCrLf & " FYEAR, ROWNO, SUBROWNO, VNO,VDATE, " & vbCrLf & " BOOKCODE, BOOKTYPE, BOOKSUBTYPE, ACCOUNTCODE, " & vbCrLf & " PARTYCODE,PARTYNAME, AMOUNTPAID, SECTIONCODE, " & vbCrLf & " TDSRATE, ISEXEPTED, EXEPTIONCNO, " & vbCrLf & " TDSAMOUNT, ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM) VALUES ( "
            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(pMKey)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " 1,1,'" & MainClass.AllowSingleQuote(pVNoStr) & "', TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & -1 & ",'" & pBookType & "', '" & pBookSubType & "', " & vbCrLf & " '" & mTDSAccountCode & "', '" & MainClass.AllowSingleQuote(mPartyCode) & "', '" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " " & Val(CStr(mAmountPaid)) & "," & mSectionCode & "," & Val(CStr(mTdsRate)) & ", " & vbCrLf & " '" & mExempted & "','', " & vbCrLf & " " & Val(txtTDSAmount.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N')"
        Else
            SqlStr = " UPDATE TDS_TRN SET " & vbCrLf & " VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ACCOUNTCODE='" & mTDSAccountCode & "', " & vbCrLf & " PARTYCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "', PARTYNAME='" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " VNO='" & MainClass.AllowSingleQuote(pVNoStr) & "', " & vbCrLf & " AMOUNTPAID=" & Val(CStr(mAmountPaid)) & ", " & vbCrLf & " SECTIONCODE=" & mSectionCode & "," & vbCrLf & " TDSRATE=" & Val(CStr(mTdsRate)) & ", " & vbCrLf & " ISEXEPTED='" & mExempted & "', " & vbCrLf & " EXEPTIONCNO='', " & vbCrLf & " TDSAMOUNT=" & Val(txtTDSAmount.Text) & ", UPDATE_FROM='N'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE MKey= '" & pMKey & "'"
        End If
        PubDBCn.Execute(SqlStr)
        UpdateTDSCreditDetail = True
        Exit Function
UpdateError:
        UpdateTDSCreditDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function CheckValidVDate(ByRef pBillNoSeq As Double, ByRef mDivisionCode As Double, ByRef pStartingNo As Double) As Object
        On Error GoTo CheckERR
        Dim SqlStr As String
        Dim mRsCheck1 As ADODB.Recordset
        Dim mRsCheck2 As ADODB.Recordset
        Dim mBackBillDate As String
        Dim mMaxInvStrfNo As Integer
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset
        CheckValidVDate = True

        If CDate(txtVDate.Text) < CDate("01/07/2022") Then
            CheckValidVDate = True
            Exit Function
        End If

        '    mSeparateSeries = IIf(IsNull(RsCompany!SEPARATE_PUR_SERIES), "N", RsCompany!SEPARATE_PUR_SERIES)
        '    If mSeparateSeries = "Y" Then
        '        SqlStr = "SELECT PUR_SERIES " & vbCrLf _
        ''                & " FROM INV_DIVISION_MST " & vbCrLf _
        ''                & " WHERE Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''                & " AND DIV_CODE=" & mDivisionCode & ""
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '        If RsTemp.EOF = False Then
        '            If mDivisionCode > 1 Then
        '                pStartingNo = pStartingNo + IIf(IsNull(RsTemp!PUR_SERIES), 0, RsTemp!PUR_SERIES)
        '            End If
        '        End If
        '    End If
        If Val(txtVNo.Text) = pStartingNo Then Exit Function
        SqlStr = "SELECT MAX(VDATE)" & vbCrLf _
            & " FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND VNOSEQ<" & Val(CStr(pBillNoSeq)) & ""
        If lblPurchaseType.Text = "W" Or lblPurchaseType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE IN ('W','R')"
        Else
            SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE='" & lblPurchaseType.Text & "'"
        End If
        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            SqlStr = SqlStr & vbCrLf & " AND PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ""
        End If
        '        If mSeparateSeries = "Y" Then
        '            SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
        '        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If
        SqlStr = "SELECT MIN(VDATE)" & " FROM FIN_PURCHASE_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND VNOSEQ>" & Val(CStr(pBillNoSeq)) & ""
        If lblPurchaseType.Text = "W" Or lblPurchaseType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE IN ('W','R')"
        Else
            SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE='" & lblPurchaseType.Text & "'"
        End If
        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            SqlStr = SqlStr & vbCrLf & " AND PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ""
        End If
        '    If mSeparateSeries = "Y" Then
        '        SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
        '    End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtVDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Voucher Date Is Greater Than The Voucher Date Of Next Voucher No.")
                CheckValidVDate = False
            ElseIf CDate(txtVDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Voucher Date Is Less Than The Voucher Date Of Previous Voucher No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtVDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Voucher Date Is Greater Than The Voucher Date Of Next Voucher No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtVDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Voucher Date Is Less Than The Voucher Date Of Previous Voucher No.")
                CheckValidVDate = False
            End If
        End If
        Exit Function
CheckERR:
        '    Resume
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqBillNo(ByRef mFieldName As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingNo As Integer, ByRef mDivisionCode As Double) As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsPurchMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Integer
        Dim SqlStr As String
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset
        Dim mMAxNo As Double

        SqlStr = ""
        If lblPurchaseType.Text = "W" Or lblPurchaseType.Text = "R" Then
            pStartingNo = 90001
        Else
            pStartingNo = 95001
        End If
        '    mSeparateSeries = IIf(IsNull(RsCompany!SEPARATE_PUR_SERIES), "N", RsCompany!SEPARATE_PUR_SERIES)
        '
        '    If mFieldName = "VNOSEQ" And mSeparateSeries = "Y" Then
        '        SqlStr = "SELECT PUR_SERIES " & vbCrLf _
        ''                & " FROM INV_DIVISION_MST " & vbCrLf _
        ''                & " WHERE Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''                & " AND DIV_CODE=" & mDivisionCode & ""
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '        If RsTemp.EOF = False Then
        '            If mDivisionCode > 1 Then
        '                pStartingNo = pStartingNo + IIf(IsNull(RsTemp!PUR_SERIES), 1, RsTemp!PUR_SERIES)
        '                pStartingNo = IIf(pStartingNo = 0, 1, pStartingNo)
        '            End If
        '        End If
        '    End If
        SqlStr = ""
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookType='" & mBookType & "'"
        If mFieldName = "VNOSEQ" Then
            If lblPurchaseType.Text = "W" Or lblPurchaseType.Text = "R" Then
                SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE IN ('W','R')"
            Else
                SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE='" & lblPurchaseType.Text & "'"
            End If
            '        If mSeparateSeries = "Y" Then
            '            SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
            '        End If
        End If
        If mFieldName = "GST_CLAIM_NO" Then
            SqlStr = SqlStr & vbCrLf & " AND MODVAT_TYPE =1 AND ISGSTAPPLICABLE ='Y'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchMainGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMAxNo = -1 Then
                    mNewSeqBillNo = pStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = pStartingNo
                End If
            Else
                mNewSeqBillNo = pStartingNo
            End If
        End With
        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqBillNoNew(ByRef mFieldName As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingNo As Double, ByRef mDivisionCode As Double) As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsPurchMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset
        Dim xFyear As Integer
        SqlStr = ""
        xFyear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))

        pStartingNo = 1
        pStartingNo = CDbl(xFyear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(lblPurchaseSeqType.Text) & VB6.Format(pStartingNo, "00000"))

        SqlStr = ""
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND BookType='" & mBookType & "'"

        If mFieldName = "VNOSEQ" Then
            SqlStr = SqlStr & vbCrLf & "AND PURCHASESEQTYPE='" & lblPurchaseSeqType.Text & "'"
        End If

        If mFieldName = "GST_CLAIM_NO" Then
            SqlStr = SqlStr & vbCrLf & " AND MODVAT_TYPE =1 AND ISGSTAPPLICABLE ='Y'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchMainGen
            If .EOF = False Then
                If IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = pStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = pStartingNo
                End If
            Else
                mNewSeqBillNo = pStartingNo
            End If
        End With
        AutoGenSeqBillNoNew = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDetail1(ByRef xNarration As String, ByRef pAccountCode As String, ByRef pVNo As String, ByRef pSuppCustCode As String, ByRef mShipTo As String, ByRef mShipToCode As String, ByRef pDivCode As Double, ByRef mIsReversalApp As String) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double
        Dim mGSTableAmount As Double
        Dim mExicseableAmt As Double
        Dim mCessableAmt As Double
        Dim mSTableAmt As Double
        Dim mShortageQty As Double
        Dim mRejectQty As Double
        Dim mCESSAmt As Double
        Dim mServiceAmt As Double
        Dim mTotExicseableAmt As Double
        Dim mTotSTableAmt As Double
        Dim mPONo As String
        Dim mTotCessableAmt As Double
        Dim mSHECAmt As Double
        Dim mEDRate As Double
        Dim xIsCancelled As Boolean
        Dim xIsFOC As Boolean
        Dim xIsModvat As String
        Dim xISSTRefund As String
        Dim xISCSTRefund As String
        Dim mIsJobWork As String
        Dim mIsSaleReturn As String
        Dim mApprovedQty As Double
        Dim mOtherAmount As Double
        Dim mHSNCode As String
        Dim pInvType As String
        Dim mInvTypeCode As Double
        Dim mDebitAccountDesc As String
        Dim mDebitAccountCode As String
        Dim mPODate As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mPOS As String
        Dim mState As String
        Dim xSuppCustCode As String
        Dim mRCSaleBillMKey As String
        Dim mSaleBillPrefix As String
        Dim mSaleBillSeq As String
        Dim mSaleBillNo As String
        Dim mSaleBillDate As String
        Dim mGSTCreditApp As String
        Dim mReverseChargeApp As String
        Dim mGoodServ As String
        Dim mExempted As String
        xIsCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        xIsFOC = False
        xIsModvat = "N" ''IIf(chkModvat.Value = vbChecked, "Y", "N")
        xISSTRefund = "N"
        xISCSTRefund = "N"
        mTotExicseableAmt = 0
        mTotSTableAmt = 0
        mTotCessableAmt = 0
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "'")
        '    PubDBCn.Execute "DELETE FROM FIN_PURCHASE_TRN WHERE MKEY='" & LblMKey.text & "'"
        PubDBCn.Execute("Delete From FIN_PURCHASE_DET Where Mkey='" & LblMKey.Text & "'")
        mPOS = ""
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                xNarration = xNarration & IIf(xNarration = "", "", ", ") & mItemDesc

                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColGoodsServs
                mGoodServ = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "G", "S")
                .Col = ColCreditApp
                mGSTCreditApp = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                .Col = ColRCApp
                mReverseChargeApp = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                If mIsReversalApp = "N" Then
                    mIsReversalApp = IIf(mReverseChargeApp = "Y", "Y", mIsReversalApp)
                End If
                .Col = ColExempted
                mExempted = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                '            If Left(cboGoodsService.Text, 1) = "S" Then
                '                If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "GST_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                '                    mGSTCreditApp = IIf(Trim(MasterNo) = "Y", "Y", "N")
                '                Else
                '                    mGSTCreditApp = "N"
                '                End If
                '
                '                If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "REVERSE_CHARGE_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                '                    mReverseChargeApp = IIf(Trim(MasterNo) = "Y", "Y", "N")
                '                Else
                '                    mReverseChargeApp = "N"
                '                End If
                '            Else
                '                mGSTCreditApp = "Y"
                '                mReverseChargeApp = "N"
                '            End If
                .Col = ColQty
                mQty = Val(.Text)
                .Col = ColUnit
                If lblPurchaseType.Text = "S" Then
                    mUnit = "NOS"
                Else
                    mUnit = MainClass.AllowSingleQuote(.Text)
                End If
                .Col = ColRate
                mRate = Val(.Text)
                mEDRate = 0
                .Col = ColAmount
                mAmount = Val(.Text)
                .Col = ColGSTableAmount
                mGSTableAmount = Val(.Text)
                .Col = ColPONo
                mPONo = Trim(.Text)
                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)
                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)
                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)
                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)
                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)
                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)
                .Col = ColAccountPostCode
                mDebitAccountDesc = Trim(.Text)
                mDebitAccountCode = GetDebitNameOfInvType(mDebitAccountDesc, "N")
                If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                    If mDebitAccountCode = "-1" Or mDebitAccountCode = "" Then MsgBox("Account Code not Defined For Item Code : " & mItemDesc) : GoTo UpdateDetail1
                End If
                '            If MainClass.ValidateWithMasterTable(mDebitAccountDesc, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                '                MsgBox "Invoice Type Does Not Exist In Master", vbInformation
                '                GoTo UpdateDetail1
                '            End If
                If MainClass.ValidateWithMasterTable(mDebitAccountDesc, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mInvTypeCode = MasterNo
                Else
                    MsgBox("Invoice Type Does Not Exist In Master", MsgBoxStyle.Information)
                    GoTo UpdateDetail1
                End If
                .Col = ColRCMkey
                mRCSaleBillMKey = Trim(.Text)
                SprdMain.Col = ColSaleBillPrefix
                mSaleBillPrefix = .Text
                SprdMain.Col = ColSaleBillSeq
                mSaleBillSeq = .Text
                SprdMain.Col = ColSaleBillNo
                mSaleBillNo = .Text
                SprdMain.Col = ColSaleBillDate
                mSaleBillDate = VB6.Format(.Text, "DD/MM/YYYY")
                '            If MainClass.ValidateWithMasterTable(mDebitAccountDesc, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mDebitAccountCode = MasterNo
                '            Else
                '                MsgBox "Account Does Not Exist In Master", vbInformation
                '                GoTo UpdateDetail1
                '            End If
                SqlStr = ""
                mItemCode = "-1"
                If mItemDesc <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO FIN_PURCHASE_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , ITEM_DESC, CUSTOMER_PART_NO, HSNCODE, ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT, " & vbCrLf & " ITEM_ED, ITEM_ST, ITEM_CESS, SHORTAGE_QTY,REJECTED_QTY," & vbCrLf & " CUST_REF_NO, CUST_REF_DATE, COMPANY_CODE,ITEM_SHEC, " & vbCrLf & " PUR_ACCOUNT_CODE,ITEM_ED_PER,ITEM_TRNTYPE, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT,GSTABLE_AMT, " & vbCrLf & " RCSALEBILLMKEY, SALEBILLNOPREFIX, SALEBILLNOSEQ, SALEBILL_NO, SALEBILLDATE, " & vbCrLf & " GOODS_SERVICE, GST_CREDITAPP, GST_RCAPP, GST_EXEMPTED) "
                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & " '" & mItemCode & "','" & MainClass.AllowSingleQuote(mItemDesc) & "','" & mPartNo & "','" & mHSNCode & "', " & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & vbCrLf & " " & mExicseableAmt & ", " & mSTableAmt & ", " & vbCrLf & " " & mCESSAmt & "," & mShortageQty & "," & mRejectQty & ", " & vbCrLf & " '" & mPONo & "',TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mSHECAmt)) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mDebitAccountCode) & "'," & Val(CStr(mEDRate)) & "," & mInvTypeCode & ", " & vbCrLf & " " & mCGSTPer & "," & mSGSTPer & "," & mIGSTPer & "," & vbCrLf & " " & mCGSTAmount & "," & mSGSTAmount & "," & mIGSTAmount & "," & mGSTableAmount & "," & vbCrLf & " '" & mRCSaleBillMKey & "','" & mSaleBillPrefix & "', '" & mSaleBillSeq & "', '" & mSaleBillNo & "', TO_DATE('" & VB6.Format(mSaleBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mGoodServ & "',  '" & mGSTCreditApp & "', '" & mReverseChargeApp & "', '" & mExempted & "'" & vbCrLf & " ) "
                    PubDBCn.Execute(SqlStr)
                    mApprovedQty = mQty - mShortageQty - mRejectQty
                    '
                    '                If chkFinalPost.Value = vbChecked And mApprovedQty > 0 Then
                    '                    If FinancePurchaseTRN(LblMKey.text, xIsCancelled, xIsFOC, -1, "", _
                    ''                        Trim(txtBillNo.Text), Format(txtBillDate.Text, "DD-MMM-YYYY"), Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text), Format(txtVDate.Text, "DD-MMM-YYYY"), _
                    ''                        LblBookCode.text, xIsModvat, xISSTRefund, xISCSTRefund, I, mItemCode, mUnit, mApprovedQty, mRate, Val(mExicseableAmt), _
                    ''                        mCESSAmt, mSHECAmt, mSTableAmt, mOtherAmount) = False Then GoTo UpdateDetail1
                    '                End If
                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If (VB.Left(cboGSTStatus.Text, 1) = "G" And mReverseChargeApp = "N") And mGSTCreditApp = "Y" Then
                            If UpdateGSTTRN(PubDBCn, (LblMKey.Text), LblBookCode.Text, mBookType, mBookSubType, pVNo, VB6.Format(TxtVDate.Text, "DD-MMM-YYYY"), Trim(txtBillNo.Text), VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), "", "", pSuppCustCode, pAccountCode, mShipTo, mShipToCode, I, mItemCode, mQty, mUnit, mRate, mQty * mRate, mGSTableAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mItemDesc), mPOS, "N", "", mGoodServ, mIsReversalApp, "C", VB6.Format(TxtVDate.Text, "DD-MMM-YYYY"), "N") = False Then GoTo UpdateDetail1
                            '                    ElseIf Left(cboGSTStatus.Text, 1) = "R" Then
                            '                        xSuppCustCode = IIf(IsNull(RsCompany!COMPANY_ACCTCODE), -1, RsCompany!COMPANY_ACCTCODE)
                            '                        If UpdateGSTTRN(PubDBCn, LblMKey.text, LblBookCode, mBookType, mBookSubType, _
                            ''                                        pVNo, Format(TxtVDate.Text, "DD-MMM-YYYY"), Trim(pSaleBillNo), Format(pSaleBillDate, "DD-MMM-YYYY"), "", "", _
                            ''                                        xSuppCustCode, pAccountCode, mShipTo, mShipToCode, _
                            ''                                        I, mItemCode, mQty, mUnit, mRate, _
                            ''                                        mQty * mRate, mGSTableAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, _
                            ''                                        mCGSTAmount, mSGSTAmount, mIGSTAmount, _
                            ''                                        mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mItemDesc), mPOS, "N", _
                            ''                                        "", "S", "Y", "D", pSaleBillDate, "N" _
                            ''                                        ) = False Then GoTo UpdateDetail1:
                        End If
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        UpdateDetail1 = UpdatePurchaseExp1
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdatePurchaseExp1() As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim SqlStr As String
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mDebitAmount As String
        PubDBCn.Execute("Delete From FIN_PURCHASE_EXP Where Mkey='" & LblMKey.Text & "'")
        With SprdExp
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColExpName
                If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mExpCode = MasterNo
                Else
                    mExpCode = -1
                End If
                .Col = ColExpPercent
                mPercent = Val(.Text)
                .Col = ColExpAmt
                mExpAmount = Val(.Text)
                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    mExpAmount = mExpAmount * -1
                End If
                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)
                .Col = ColExpDebitAmt
                mDebitAmount = CStr(Val(.Text))
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                SqlStr = ""
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_PURCHASE_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DebitAmount) " & vbCrLf & " Values ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "'," & mDebitAmount & ")"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdatePurchaseExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdatePurchaseExp1 = False
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xPORate As Double
        Dim xVolDiscRate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xVolDiscRateDN As Double
        Dim xRateDiffCN As Double
        Dim mExciseDutyAmt As Double
        Dim mServiceTaxAmt As Double
        Dim mEDUAmt As Double
        Dim mSHECAmt As Double
        Dim mSalesTaxAmount As Double
        Dim mWithInState As String
        Dim xPoNo As String
        Dim mPORateZero As Boolean
        Dim mLockBookCode As Integer
        Dim mAgtPO As Boolean
        Dim mSalesTaxReq As String
        Dim mSectionCode As String
        Dim mPANNo As String
        Dim mRefType As String
        Dim mIsSaleReturn As String
        Dim mItemType As String
        Dim mIsItemCapital As String
        Dim mCapitalInvType As String
        Dim mItemCode As String
        Dim mGoodsServs As String
        Dim mHeadType As String
        Dim mInterUnit As String
        'Dim mAlreadyRejQty As Double
        Dim pDebitNoteNo As String
        Dim pDebitNoteDate As String
        Dim mItemClassification As String
        Dim mAcctPostName As String
        Dim xSuppCode As String
        Dim xGSTRegd As String
        Dim xGSTNo As String
        Dim mActServiceCode As String
        Dim mActChargeApp As String
        Dim mPOServiceCode As String
        Dim mPOChargeApp As String
        Dim mGSTCreditApp As String
        Dim mHSNCode As String
        Dim pMaxDate As String
        Dim mCreditAppinMST As String
        Dim mCreditApp As String
        Dim mAmount As Double
        Dim pErrorMsg As String
        mAgtPO = False
        FieldsVarification = True
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        '    If PubUserID <> "G0416" Then
        '    If CheckVoucherDateLock(txtVDate.Text, pMaxDate) = True Then
        '         MsgInformation "Working Company has been locked till date : " & pMaxDate & vbCrLf _
        ''                    & "So Unable to Save or Delete. Contact your system administrator."
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '    End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            mLockBookCode = CInt(ConLockPurchase)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVno.Text), (TxtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
                If ValidateBookLocking(PubDBCn, mLockBookCode, txtExpDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPurchMain.EOF = True Then Exit Function
        If MainClass.GetUserCanModify(TxtVDate.Text) = False Then
            MsgBox("You Have Not Rights to Add or Modify back Voucher", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If CDate(VB6.Format(txtVDate.Text, "DD/MM/YYYY")) > CDate(VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
            MsgInformation("Voucher Date is Greater Than Current Date. Cannot Save")
            txtVDate.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If PubUserID <> "G0416" Then
            If chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("GST Claim is Taken, So that cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If
            '        If CheckVoucherDateLock(txtVDate.Text, pMaxDate) = True Then
            '             MsgInformation "Working Company has been locked till date : " & pMaxDate & vbCrLf _
            ''                        & "So Unable to Save or Delete. Contact your system administrator."
            '            FieldsVarification = False
            '            Exit Function
            '        End If
        End If
        If MODIFYMode = True And txtVno.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboDivision.Text) = "" Then
            MsgInformation("Division is Blank. Cannot Save")
            If cboDivision.Enabled = True Then cboDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Division Name. Cannot Save")
            If cboDivision.Enabled = True Then cboDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If TxtVDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            TxtVDate.Focus()
            Exit Function
        ElseIf FYChk((TxtVDate.Text)) = False Then
            FieldsVarification = False
            If TxtVDate.Enabled = True Then TxtVDate.Focus()
            Exit Function
        End If
        If txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            FieldsVarification = False
            Exit Function
        End If
        If txtBillDate.Text = "" Then
            MsgBox("BillDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtBillDate.Text) Then
            MsgBox("Invalid Bill Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        End If
        If CDate(TxtVDate.Text) < CDate(txtBillDate.Text) Then
            MsgBox("VDate Can Not be Less Than BillDate.")
            FieldsVarification = False
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            Exit Function
        End If
        If txtExpDate.Text = "" Then
            MsgBox("Expense Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtExpDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtExpDate.Text) Then
            MsgBox("Invalid Expense Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtExpDate.Focus()
            Exit Function
        End If
        'If ADDMode = True Then
        If CDate(txtExpDate.Text) > CDate(txtBillDate.Text) Then
            MsgBox("Expense Date could not be greater than Bill Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtExpDate.Focus()
            Exit Function
        End If
        'End If
        If Trim(lblSaleBillNo.Text) <> "" Then
            MsgBox("Reverse Charge Sale Bill is Generated agt Bill No. " & lblSaleBillNo.Text & ", So Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSupplier.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            xSuppCode = MasterNo
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSuppCode) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                If txtBillTo.Enabled = True Then txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If

        Dim mJVTVDate As String

        If Trim(txtJVVNO.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(lblJVTMKey.Text, "MKEY", "VDATE", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mJVTVDate = MasterNo
                If CDate(mJVTVDate) <> CDate(txtVDate.Text) Then
                    MsgBox("Cann't be Change Voucher Date. JVT Voucher has been Made.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xGSTRegd = MasterNo
        End If
        If xGSTRegd = "Y" Then
            xGSTNo = GetPartyBusinessDetail(Trim(xSuppCode), Trim(txtBillTo.Text), "GST_RGN_NO")
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    xGSTNo = Trim(MasterNo)
            'End If
            If Trim(xGSTNo) = "" Then
                MsgBox("Invalid GST no, Please Check.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        If cboGSTStatus.SelectedIndex = -1 Then
            MsgBox("Please select GST Status", MsgBoxStyle.Information)
            If cboGSTStatus.Enabled = True Then cboGSTStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If xGSTRegd = "Y" And VB.Left(cboGSTStatus.Text, 1) = "R" Then
            MsgBox("Supplier is registered,So Cann't be Selected Reverse Charge.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If xGSTRegd = "N" And VB.Left(cboGSTStatus.Text, 1) = "G" Then
            MsgBox("Supplier is not registered, So Cann't be select the GST Registered.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If xGSTRegd = "E" And VB.Left(cboGSTStatus.Text, 1) <> "E" Then
            MsgBox("GST Exempted Supplier, please select the GST Exempted.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If xGSTRegd = "C" And VB.Left(cboGSTStatus.Text, 1) <> "C" Then
            MsgBox("Composit Supplier, please select the GST Composit.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        ElseIf xGSTRegd <> "C" And VB.Left(cboGSTStatus.Text, 1) = "C" Then
            MsgBox("Not Composit Supplier, please unselect the Composit.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If xGSTRegd = "Y" Then
            If VB.Left(cboGSTStatus.Text, 1) = "D" Then
                MsgBox("Supplier is Registered, So Cann't be use Daily Exemption.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "D" Then
            If Val(lblNetAmount.Text) > 5000 Then
                MsgBox("Amount Cann't be Greater than 5000.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            mAmount = GetTodatExemptionAmount()
            If Val(CStr(mAmount)) + Val(lblNetAmount.Text) > 5000 Then
                MsgBox("Today Already used the Exemption Amount. ( Rs." & mAmount & ")", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        ''28082017
        '    If chkCreditRC.Value = vbChecked Then
        '        MsgBox "Final Credit on Reverse Charge is Done, So that cann't be Modify.", vbInformation
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        If ValidateBillNo((txtBillNo.Text), pErrorMsg) = False Then
            MsgInformation(pErrorMsg)
            FieldsVarification = False
            Exit Function
        End If
        If DuplicateBillNo(xSuppCode, (LblMKey.Text)) = True Then
            MsgBox("Duplicate Bill for this Supplier", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        mWithInState = "Y"
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    xSuppCode = MasterNo
        'End If
        If Trim(txtSupplier.Text) <> "" Then
            mWithInState = GetPartyBusinessDetail(Trim(xSuppCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        End If
        'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = IIf(IsDbNull(MasterNo), "Y", MasterNo)
        'End If
        If txtPaymentDate.Text = "" Then
            MsgBox("Payment Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPaymentDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtPaymentDate.Text) Then
            MsgBox("Invalid Payment Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPaymentDate.Focus()
            Exit Function
        End If
        '    If Left(cboGoodsService.Text, 1) = "S" Then
        '        If ValidateHSNCode = False Then
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '        mGSTCreditApp = IIf(chkGSTCreditApp.Value = vbChecked, "Y", "N")
        '    Else
        '        If chkGSTCreditApp.Value = vbUnchecked Then
        '            If MsgQuestion("You Select the GST Credit Not Applicable, Are you want to continue...") = vbNo Then
        '                FieldsVarification = False
        '                Exit Function
        '            End If
        '        End If
        '        mGSTCreditApp = IIf(chkGSTCreditApp.Value = vbChecked, "Y", "N")
        '    End If
        '    If Left(cboGSTStatus.Text, 1) = "E" Or Left(cboGSTStatus.Text, 1) = "N" Then
        '        If chkGSTCreditApp.Value = vbChecked Then
        '            MsgBox "GST Credit should be Not Applicable, if Exempted", vbInformation
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        '    If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "GST_APP", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mGSTCreditApp = Trim(MasterNo)
        '    Else
        '        mGSTCreditApp = "N"
        '    End If
        '
        '    If Left(cboGSTStatus.Text, 1) = "E" And (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) <> 0 Then
        '        If MsgQuestion("You have not Check in GST Credit Applicable. You Want to Continue ...") = vbNo Then
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        '
        '    If Left(cboGSTStatus.Text, 1) = "G" And mGSTCreditApp = "Y" And (Val(txtTotCGSTRefund.Text) + Val(txtTotSGSTRefund.Text) + Val(txtTotIGSTRefund.Text)) = 0 Then
        '        MsgBox "GST Amount Cann't be Zero.", vbInformation
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If mGSTCreditApp = "Y" Then
        '        If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) <> (Val(txtTotCGSTRefund.Text) + Val(txtTotSGSTRefund.Text) + Val(txtTotIGSTRefund.Text)) Then
        '            If MsgQuestion("GST Amount And Refund Amount Not Match. You Want to Continue ...") = vbNo Then
        '                FieldsVarification = False
        '                Exit Function
        '            End If
        '        End If
        '    End If
        If Val(txtTdsRate.Text) > 100 Then
            MsgBox("TDS RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtESIRate.Text) > 100 Then
            MsgBox("ESI RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtSTDSRate.Text) > 100 Then
            MsgBox("STDS RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        mWithInState = "Y"
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    xSuppCode = MasterNo
        'End If
        If Trim(txtSupplier.Text) <> "" Then
            mWithInState = GetPartyBusinessDetail(Trim(xSuppCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        End If
        'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mWithInState = IIf(IsDbNull(MasterNo), "Y", MasterNo)
        'End If
        '    If Trim(txtItemType.Text) = "" Then
        '        MsgBox "Item Type is Blank", vbInformation
        '        FieldsVarification = False
        '        txtItemType.SetFocus
        '        Exit Function
        '    End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            If VB.Left(cboGSTStatus.Text, 1) <> "E" Then
                MsgBox("Cann't be Cancelled.(First You Deleted GST Claim.)", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        '    If Trim(cboGoodsService.Text) = "" Then
        '            MsgBox "Goods / Service Cann't be Blank", vbInformation
        '            If cboGoodsService.Enabled Then cboGoodsService.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '    End If
        '    If Left(cboGoodsService.Text, 1) = "S" Then
        '        If Trim(txtServProvided.Text) = "" Then
        '            MsgBox "Service Provided Cann't be Blank", vbInformation
        '            If txtServProvided.Enabled Then txtServProvided.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        Else
        '            If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '                MsgBox "Service Provided is not defined in Master, So cann't be Saved.", vbInformation
        '                FieldsVarification = False
        '                Exit Function
        '            Else
        '                mActServiceCode = MasterNo
        '            End If
        '        End If
        '    End If
        mActChargeApp = VB.Left(cboGSTStatus.Text, 1)
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColGoodsServs
                mGoodsServs = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "G", "S")
                .Col = ColCreditApp
                mCreditApp = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                .Col = ColItemDesc
                mItemCode = Trim(.Text)
                .Col = ColPORate
                xPORate = Val(.Text)
                .Col = ColPONo
                xPoNo = CStr(Val(.Text))
                .Col = ColRate
                xRate = Val(.Text)
                SprdMain.Row = cntRow
                SprdMain.Col = ColAccountPostCode
                mAcctPostName = UCase(SprdMain.Text)
                If VB.Left(cboGSTStatus.Text, 1) = "I" And mCreditApp = "Y" Then
                    MsgInformation("Please uncheck the Credit App for ineligible credit.")
                    '                MainClass.SetFocusToCell SprdMain, cntRow, ColAccountPostCode
                    FieldsVarification = False
                    Exit Function
                End If
                If mAcctPostName = "" Then
                    MsgInformation("Account Post Name Cann't be Blank.")
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColAccountPostCode)
                    FieldsVarification = False
                    Exit Function
                Else
                    '                    If MainClass.ValidateWithMasterTable(mAcctPostName, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = False Then
                    If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("Invalid Account Post Name.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColAccountPostCode)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
                If VB.Left(cboGSTStatus.Text, 1) = "E" Or VB.Left(cboGSTStatus.Text, 1) = "D" Or VB.Left(cboGSTStatus.Text, 1) = "N" Then
                    If mCreditApp = "Y" Then
                        MsgInformation("You Selected the GST Status Exempted / Non GST / Daily Exemption, So Than please untick from GST Credit.")
                        FieldsVarification = False
                        Exit Function
                    End If
                Else
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColHSN
                    mHSNCode = Trim(UCase(SprdMain.Text))
                    If Trim(UCase(SprdMain.Text)) = "" Then
                        MsgInformation("HSN/SAC Cann't be Blank.")
                        '                        MainClass.SetFocusToCell SprdMain, I, ColAcctPostName
                        FieldsVarification = False
                        Exit Function
                    End If
                    If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & mGoodsServs & "'") = False Then
                        MsgInformation("Invalid HSN/SAC Code.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColHSN)
                        FieldsVarification = False
                        Exit Function
                    End If
                    If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "GST_APP", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & mGoodsServs & "'") = True Then
                        mCreditAppinMST = MasterNo
                    End If
                    If mCreditAppinMST = "N" And mCreditApp = "Y" Then
                        MsgInformation("Please Check Credit Applicable, Credit is not Applicable.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColHSN)
                        FieldsVarification = False
                        Exit Function
                    ElseIf mCreditAppinMST = "Y" And mCreditApp = "N" Then
                        If MsgQuestion("For HSN/SAC " & mHSNCode & " Credit is Applicable, but you selected not. Want to Continue..") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
                mPOServiceCode = CStr(-1)
                '            If lblPurchaseType.text = "W" Then
                '                .Col = ColPONo
                '                If Trim(.Text) <> "" Then
                '                    If MainClass.ValidateWithMasterTable(Val(.Text), "AUTO_KEY_PO", "SERVICE_CODE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                '                        mPOServiceCode = MasterNo
                '                    End If
                '                End If
                '
                '                If mActServiceCode <> mPOServiceCode Then
                '                    MsgInformation "Service is not Match with PO."
                '                    FieldsVarification = False
                '                    Exit Function
                '                End If
                '                    .Col = ColPONo
                '                    mPOChargeApp = "N"
                '                    If Trim(.Text) <> "" Then
                '                        If MainClass.ValidateWithMasterTable(Val(.Text), "AUTO_KEY_PO", "ISGSTAPPLICABLE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                '                            mPOChargeApp = MasterNo
                '                        End If
                '                    End If
                '
                '                    If mActChargeApp <> mPOChargeApp Then
                '                        MsgInformation "GST Charge Not Match With PO."
                '                        FieldsVarification = False
                '                        Exit Function
                '                    End If
                '            End If
            Next
        End With
        'If ADDMode = True Then
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtTDSAmount.Text) = 0 Then
            MsgBox("Please Check TDS Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked And Trim(txtSection.Text) = "" Then
            MsgBox("Please Check TDS Section.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Please Check TDS Section.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtESIAmount.Text) = 0 Then
            MsgBox("Please Check ESI Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtSTDSAmount.Text) = 0 Then
            MsgBox("Please Check STDS Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        'End If
        'If ADDMode = True Then
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPANNo = MasterNo
            Else
                mPANNo = ""
            End If
            If Trim(mPANNo) = "" Then
                MsgBox("PAN NO is not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SECTIONCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mSectionCode = MasterNo
            'Else
            '    mSectionCode = ""
            'End If
            'If Trim(mSectionCode) = "" Or Trim(mSectionCode) = "-1" Then
            '    MsgBox("TDS Section not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
            '    FieldsVarification = False
            '    Exit Function
            'End If
        End If
        'End If
        If Val(txtAdvBal.Text) > 0 And Val(txtAdvAdjust.Text) = 0 Then
            If MsgQuestion("Party has advance Payment, Want to adjust with this voucher.") = CStr(MsgBoxResult.Yes) Then
                FieldsVarification = False
                Exit Function
            End If
        End If
        If Val(txtAdvBal.Text) > 0 Then
            If Val(txtAdvBal.Text) < Val(txtAdvAdjust.Text) Then
                MsgBox("Advance Balance is Less than Advnace Adjusted, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        Dim mPaymentAmount As Double = 0
        Dim mBalanceAdjPayment As Double = 0
        Dim mBillAdjNo As String

        With SprdPaymentDetail
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColPayBillNo
                mBillAdjNo = Trim(.Text)
                If mBillAdjNo <> "" Then
                    .Col = ColPayPaymentAmt
                    mPaymentAmount = mPaymentAmount + Val(.Text)

                    .Col = ColPayBalAmount
                    mBalanceAdjPayment = Val(.Text)

                    .Col = ColPayBalDC
                    mBalanceAdjPayment = mBalanceAdjPayment * IIf(Mid(.Text, 1, 1) = "D", 1, -1)

                    .Col = ColPayPaymentAmt
                    If mBalanceAdjPayment < Val(.Text) Then
                        MsgBox("There is no Balance Amount for Adjust. Bill No : " & mBillAdjNo, MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If

                End If
            Next
        End With

        'Private Const ColPayBalAmount As Short = 4
        'Private Const ColPayBalDC As Short = 5
        'Private Const ColPayPaymentAmt As Short = 6

        If mPaymentAmount > Val(lblNetAmount.Text) Then
            MsgBox("Payment Cann't be greater than Bill Amount", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If


        If lblPurchaseType.Text = "W" Then
            If MainClass.ValidDataInGrid(SprdMain, ColPONo, "N", "PO No Is Blank.") = False Then FieldsVarification = False : Exit Function
        End If
        If MainClass.ValidDataInGrid(SprdMain, ColItemDesc, "S", "ItemCode is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        If VB.Left(cboGSTStatus.Text, 1) <> "N" Then
            If MainClass.ValidDataInGrid(SprdMain, ColHSN, "S", "HSN / SAC Code is Blank.") = False Then FieldsVarification = False : Exit Function
        End If
        Exit Function
err_Renamed:
        FieldsVarification = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function GetTodatExemptionAmount() As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        GetTodatExemptionAmount = 0
        SqlStr = " Select SUM(NETVALUE) AS NETVALUE " & vbCrLf & " FROM FIN_PURCHASE_HDR  " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ISGSTAPPLICABLE = 'D'"
        SqlStr = SqlStr & vbCrLf & " AND VDate=TO_DATE('" & VB6.Format(TxtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If Trim(LblMKey.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND MKEY <> '" & Trim(LblMKey.Text) & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetTodatExemptionAmount = IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
        End If
        Exit Function
ErrPart:
        GetTodatExemptionAmount = 0
    End Function
    Private Function CheckVoucherDateLock(ByRef pVDate As String, ByRef pMaxDate As String) As Boolean
        On Error GoTo CheckERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mLastBillDate As String
        mLastBillDate = RsCompany.Fields("START_DATE").Value
        CheckVoucherDateLock = False
        pMaxDate = ""
        SqlStr = "SELECT MAX(VDATE) AS VDATE" & vbCrLf & " FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mLastBillDate = IIf(IsDbNull(RsTemp.Fields("VDATE").Value), mLastBillDate, RsTemp.Fields("VDATE").Value)
        End If
        pMaxDate = mLastBillDate
        If CDate(mLastBillDate) > CDate(pVDate) Then
            CheckVoucherDateLock = True
        End If
        Exit Function
CheckERR:
        CheckVoucherDateLock = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function DuplicateBillNo(ByRef pSuppCode As String, ByRef pMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillFyear As Integer
        Dim mCount As Integer
        Dim mAcctBillFYear As Integer
        Dim mBOOKType As String
        Dim mFYEAR As Long

        DuplicateBillNo = False

        mCount = 0 ''AND FYEAR=" & RsCompany.fields("FYEAR").value & "

        SqlStr = "SELECT BILLNO, BILLDATE,BOOKTYPE, FYEAR  " & vbCrLf _
            & " FROM FIN_POSTED_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BILLTYPE='B'" & vbCrLf & " AND ACCOUNTCODE='" & pSuppCode & "'" & vbCrLf _
            & " AND BILLNO='" & Trim(txtBillNo.Text) & "'"
        If ADDMode = True Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND MKEY<>'" & pMKey & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        mAcctBillFYear = GetCurrentFYNo(PubDBCn, (txtBillDate.Text))
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mBillNo = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                mBOOKType = IIf(IsDBNull(RsTemp.Fields("BOOKTYPE").Value), "", RsTemp.Fields("BOOKTYPE").Value)
                mFYEAR = IIf(IsDBNull(RsTemp.Fields("FYEAR").Value), "", RsTemp.Fields("FYEAR").Value)
                If CDate(txtBillDate.Text) = CDate(mBillDate) And mBOOKType <> "O" Then
                    DuplicateBillNo = True
                    Exit Function
                ElseIf CDate(txtBillDate.Text) = CDate(mBillDate) And mBOOKType = "O" And mFYEAR <= RsCompany.Fields("FYEAR").Value Then
                    DuplicateBillNo = True
                    Exit Function
                Else
                    mBillFyear = GetCurrentFYNo(PubDBCn, mBillDate)

                    If mBOOKType = "O" Then
                        If mFYEAR <= RsCompany.Fields("FYEAR").Value Then
                            If mAcctBillFYear = mBillFyear Then
                                mCount = mCount + 1
                            End If
                        End If
                    Else
                        If mAcctBillFYear = mBillFyear Then
                            mCount = mCount + 1
                        End If
                    End If

                End If
                RsTemp.MoveNext()
            Loop
        End If
        If mCount > 0 Then
            DuplicateBillNo = True
            Exit Function
        End If
        Exit Function
ErrPart:
        DuplicateBillNo = False
    End Function
    Private Function CheckDuplicateItem(ByRef mItemDesc As String) As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        If mItemDesc = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemDesc
                If UCase(Trim(.Text)) = UCase(Trim(mItemDesc)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Description")
                        '                    MainClass.SetFocusToCell SprdMain, .ActiveRow, ColItemDesc
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmPurchaseWO_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = ""
        SqlStr = "Select * from FIN_PURCHASE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_PURCHASE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_PURCHASE_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchExp, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)
        cboGSTStatus.Items.Clear()
        cboGSTStatus.Items.Add("GST Input")
        cboGSTStatus.Items.Add("Reverse Charge")
        cboGSTStatus.Items.Add("Exempt")
        cboGSTStatus.Items.Add("Non GST")
        cboGSTStatus.Items.Add("Ineligible")
        cboGSTStatus.Items.Add("Composit")

        mBookSubType = lblPurchaseType.Text
        cboGSTStatus.SelectedIndex = -1
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""
        SqlStr = ""

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)

        SqlStr = "SELECT " & vbCrLf _
            & " VNOPREFIX, TO_CHAR(VNOSEQ),VNOSUFFIX, " & vbCrLf _
            & " VNO,VDATE, "

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(DECODE(GST_CLAIM_NO,-1,'',GST_CLAIM_NO),'00000') AS GST_CLAIM_NO,GST_CLAIM_DATE, "

        SqlStr = SqlStr & vbCrLf _
            & " BILLNO, INVOICE_DATE  AS BILLDATE, " & vbCrLf _
            & " A.SUPP_CUST_NAME AS SUPPLIER, B.SUPP_CUST_NAME AS CREDIT_ACCOUNT, " & vbCrLf _
            & " ITEMDESC, TARIFFHEADING AS TARIFF,ITEMVALUE,"

        SqlStr = SqlStr & vbCrLf _
            & "TOTCGST_REFUNDAMT AS CGSTAMT,TOTSGST_REFUNDAMT AS SGSTAMT,TOTIGST_REFUNDAMT AS IGSTAMT, NETVALUE, DECODE(ISFINALPOST,'Y','YES','NO') AS FINAL_POST "

        SqlStr = SqlStr & vbCrLf _
            & " FROM " & vbCrLf _
            & " FIN_PURCHASE_HDR, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE FIN_PURCHASE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And FIN_PURCHASE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.COMPANY_CODE=B.COMPANY_CODE(+) " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.ACCOUNTCODE=B.SUPP_CUST_CODE(+) " 'AND BOOKSUBTYPE='W'

        SqlStr = SqlStr & vbCrLf _
            & " AND PURCHASE_TYPE = '" & lblPurchaseType.Text & "'"

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY VDATE,VNO"


        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")

        MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        oledbAdapter.Dispose()
        oledbCnn.Close()

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        'FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Sub
    Private Sub CreateGridHeader(pShowType As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header


            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Bill No Prefix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Bill Seq No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Bill No Suffix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "VNo"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "VDate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Claim No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Claim Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Account Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Item Desc"

            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Tariff Heading"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Item Value"
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "CGST Refund Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "SGST Refund Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Header.Caption = "IGST Refund Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Header.Caption = "Net Value"
            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Header.Caption = "Final Post"



            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
            Next

            For inti = 13 To 17
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Style = UltraWinGrid.ColumnStyle.Double
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellAppearance.TextHAlign = HAlign.Right
            Next

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Hidden = True
            'UltraGrid1.DisplayLayout.Bands(0).Columns(15).Hidden = True

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 250

            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Width = 90



            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    'Private Sub FormatSprdView()
    '    Dim cntCol As Integer
    '    With SprdView
    '        .Row = -1
    '        .set_RowHeight(0, 600)
    '        .set_ColWidth(0, 600)
    '        .set_ColWidth(1, 0)
    '        .set_ColWidth(2, 0)
    '        .set_ColWidth(3, 0)
    '        .set_ColWidth(4, 1200)
    '        .set_ColWidth(5, 1200)
    '        .set_ColWidth(6, 1200)
    '        .set_ColWidth(7, 1200)
    '        .set_ColWidth(8, 1300)
    '        .set_ColWidth(9, 1200)
    '        .set_ColWidth(10, 1300)
    '        .set_ColWidth(11, 1200)
    '        .set_ColWidth(12, 1200)
    '        .set_ColWidth(13, 2000)
    '        .set_ColWidth(14, 2000)
    '        .set_ColWidth(15, 1200)
    '        .set_ColWidth(16, 1200)
    '        .set_ColWidth(17, 1200)
    '        .set_ColWidth(18, 1200)
    '        .set_ColWidth(19, 1200)
    '        .set_ColWidth(20, 1200)
    '        .set_ColWidth(21, 800)
    '        .set_ColWidth(22, 800)
    '        For cntCol = 17 To 20
    '            .Col = cntCol
    '            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
    '        Next
    '        .ColsFrozen = 8
    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        SprdView.set_RowHeight(-1, 300)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub
    Private Sub FormatSprdExp(ByRef Arow As Integer)
        On Error GoTo ERR1
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)
            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColExpName, 18)

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.999
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .set_ColWidth(ColExpPercent, 5)

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 8)
            .TypeEditMultiLine = False

            .Col = ColExpSTCode
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMin = CDbl("-9999999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True
            .Col = ColExpAddDeduct 'ExpFlag (For Add or Deduct) Hidden Column
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColExpIdent
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColTaxable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColExciseable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True
            SprdExp.Col = ColExpCalcOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .ColHidden = True
            .Col = ColExpDebitAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpDebitAmt, 8)
            .TypeEditMultiLine = False
            .Col = ColRO
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRO, 2)
            '.Value = vbUnchecked

            MainClass.UnProtectCell(SprdExp, 1, .MaxRows, 1, ColExpDebitAmt)
            If ADDMode = True Then
                'MainClass.UnProtectCell(SprdExp, 1, .MaxRows, ColExpDebitAmt, ColExpDebitAmt)
            Else
                MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpDebitAmt, ColExpDebitAmt)
            End If
            MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpName, ColExpName)
        End With
        MainClass.SetSpreadColor(SprdExp, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        Dim cntCol As Integer
        pShowCalc = False
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 2)
            .Col = ColGoodsServs
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColGoodsServs, 4)
            '        .Value = vbUnchecked
            For cntCol = ColCreditApp To ColExempted
                .Col = cntCol
                .CellType = SS_CELL_TYPE_CHECKBOX
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .set_ColWidth(cntCol, 4)
            Next

            .Col = ColHSN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn) ''
            .set_ColWidth(ColHSN, 7)

            .Col = ColAccountPostCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
            .set_ColWidth(ColAccountPostCode, 12)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsPurchDetail.Fields("Item_Desc").DefinedSize ''
            If lblPurchaseType.Text = "S" Then
                .set_ColWidth(ColItemDesc, 15)
            Else
                .set_ColWidth(ColItemDesc, 15)
            End If
            .ColsFrozen = ColItemDesc
            .Col = ColPORate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPORate, 8)
            If lblPurchaseType.Text = "S" Then
                .ColHidden = True
            End If
            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsPurchDetail.Fields("ITEM_UOM").DefinedSize ''RCSALEBILLMKEY
            .set_ColWidth(ColUnit, 4)
            If lblPurchaseType.Text = "S" Then
                .ColHidden = True
            End If

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 8)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 8)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColAmount, 9)

            .Col = ColGSTableAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColGSTableAmount, 9)

            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColCGSTPer, 4)

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColCGSTAmount, 7)

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColSGSTPer, 4)

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColSGSTAmount, 7)

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColIGSTPer, 4)

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColIGSTAmount, 7)

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPurchDetail.Fields("CUST_REF_NO").DefinedSize ''
            .set_ColWidth(ColPONo, 9)
            If lblPurchaseType.Text = "S" Then
                .ColHidden = True
            End If

            .Col = ColRCMkey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsPurchDetail.Fields("RCSALEBILLMKEY").DefinedSize ''
            .set_ColWidth(ColRCMkey, 15)
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

            .Col = ColShowPO
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Show"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColShowPO, 4)
        End With
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPORate, ColPORate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColExempted, ColExempted) ''ColRCApp previously 07/06/2018
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColGSTableAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRCMkey, ColShowPO)
        pShowCalc = True
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPurchDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub FormatSprdPostingDetail(ByRef Arow As Integer)
        On Error GoTo ERR1
        With SprdPostingDetail
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(1, 30)
            .Col = 2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(2, 12)
            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(3, 5)
        End With
        MainClass.ProtectCell(SprdPostingDetail, 1, SprdPostingDetail.MaxRows, 1, 3)
        MainClass.SetSpreadColor(SprdPostingDetail, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPurchMain
            txtVno.Maxlength = .Fields("Vno").DefinedSize ''
            txtVNoPrefix.Maxlength = .Fields("VNoPrefix").DefinedSize ''
            txtVNoSuffix.Maxlength = .Fields("VNoSuffix").DefinedSize ''
            TxtVDate.Maxlength = 10
            txtModvatNo.Maxlength = .Fields("GST_CLAIM_NEW_NO").DefinedSize
            txtModvatDate.Maxlength = 10
            txtTotCGSTRefund.Maxlength = .Fields("TOTCGST_REFUNDAMT").Precision
            txtTotSGSTRefund.Maxlength = .Fields("TOTSGST_REFUNDAMT").Precision
            txtTotIGSTRefund.Maxlength = .Fields("TOTIGST_REFUNDAMT").Precision
            txtBillNo.Maxlength = .Fields("BillNo").Precision ''
            txtBillDate.Maxlength = 10
            txtExpDate.Maxlength = 10
            txtSupplier.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtCreditDays(0).Maxlength = .Fields("DUEDAYSFROM").Precision ''
            txtCreditDays(1).Maxlength = .Fields("DUEDAYSTO").Precision ''
            txtItemType.Maxlength = .Fields("ItemDesc").DefinedSize ''
            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize ''
            txtNarration.Maxlength = .Fields("NARRATION").DefinedSize ''
            txtCarriers.Maxlength = .Fields("CARRIERS").DefinedSize ''
            txtVehicle.Maxlength = .Fields("VehicleNo").DefinedSize ''
            txtDocsThru.Maxlength = .Fields("DocsThrough").DefinedSize ''
            txtMode.Maxlength = .Fields("DespatchMode").DefinedSize ''
            txtTdsRate.Maxlength = .Fields("TDSPer").Precision ''
            txtTDSAmount.Maxlength = .Fields("TDSAMOUNT").Precision ''
            txtESIRate.Maxlength = .Fields("ESIPER").Precision ''
            txtESIAmount.Maxlength = .Fields("ESIAMOUNT").Precision ''
            txtSTDSRate.Maxlength = .Fields("STDSPER").Precision ''
            txtSTDSAmount.Maxlength = .Fields("STDSAMOUNT").Precision ''
            txtJVVNO.Maxlength = .Fields("JVNO").DefinedSize ''
            txtServProvided.Maxlength = .Fields("SERV_PROV").DefinedSize ''
            txtServiceOn.Maxlength = .Fields("SERVICE_ON_AMT").Precision
            txtProviderPer.Maxlength = .Fields("SERV_PROVIDER_PER").Precision
            txtRecipientPer.Maxlength = .Fields("SERV_RECIPIENT_PER").Precision
            txtServiceTaxPer.Maxlength = .Fields("SERVICE_TAX_PER").Precision
            txtServiceTaxAmount.Maxlength = .Fields("SERVICE_TAX_AMOUNT").Precision
            txtAdvVNo.Maxlength = .Fields("ADV_VNO").DefinedSize
            txtAdvDate.Maxlength = .Fields("ADV_VDATE").DefinedSize
            txtItemAdvAdjust.Maxlength = .Fields("ADV_ITEM_AMT").Precision
            txtAdvAdjust.Maxlength = .Fields("ADV_ADJUSTED_AMT").Precision
            txtAdvCGST.Maxlength = .Fields("ADV_CGST_AMT").Precision
            txtAdvSGST.Maxlength = .Fields("ADV_SGST_AMT").Precision
            txtAdvIGST.MaxLength = .Fields("ADV_IGST_AMT").Precision
            txtBillTo.MaxLength = .Fields("BILL_TO_LOC_ID").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim mCustRefNo As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mSACCode As String
        Dim mGSTStatus As String
        Dim mVNo As String
        Dim mGoodServ As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        Dim pSectionCode As Long

        Clear1()
        With RsPurchMain
            If Not .EOF Then
                LblMKey.Text = .Fields("MKey").Value
                lblPMKey.Text = ""
                txtVNoPrefix.Text = IIf(IsDbNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value)
                txtVno.Text = VB6.Format(IIf(IsDbNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                txtVNoSuffix.Text = IIf(IsDbNull(.Fields("VNOSUFFIX").Value), "", .Fields("VNOSUFFIX").Value)
                TxtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")
                mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVno.Text), "00000") & Trim(txtVNoSuffix.Text))
                lblPurchaseVNo.Text = VB6.Format(IIf(IsDbNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                lblPurchaseSeqType.Text = IIf(IsDbNull(.Fields("PURCHASESEQTYPE").Value), 0, .Fields("PURCHASESEQTYPE").Value)
                '            lblSaleBillNoSeq.text = Format(IIf(IsNull(.Fields("SALEBILLNOSEQ").Value), "", .Fields("SALEBILLNOSEQ").Value), "00000000")
                '            lblSaleBillNo.text = IIf(IsNull(.Fields("SALEBILL_NO").Value), "", .Fields("SALEBILL_NO").Value)
                '            lblSaleBillDate.text = Format(IIf(IsNull(.Fields("SALEBILLDATE").Value), "", .Fields("SALEBILLDATE").Value), "DD/MM/YYYY")
                mGSTStatus = IIf(IsDbNull(.Fields("ISGSTAPPLICABLE").Value), "", .Fields("ISGSTAPPLICABLE").Value)
                If mGSTStatus = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                ElseIf mGSTStatus = "R" Then
                    cboGSTStatus.SelectedIndex = 1
                ElseIf mGSTStatus = "E" Then
                    cboGSTStatus.SelectedIndex = 2
                ElseIf mGSTStatus = "N" Then
                    cboGSTStatus.SelectedIndex = 3
                ElseIf mGSTStatus = "I" Then
                    cboGSTStatus.SelectedIndex = 4
                ElseIf mGSTStatus = "C" Then
                    cboGSTStatus.SelectedIndex = 5
                Else
                    cboGSTStatus.SelectedIndex = 6
                End If
                cboGSTStatus.Enabled = IIf(chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                '            mGoodServ = IIf(IsNull(!GOODS_SERV), "", !GOODS_SERV)
                '            If mGoodServ = "G" Then
                '                cboGoodsService.ListIndex = 0
                '            Else
                '                cboGoodsService.ListIndex = 1
                '            End If
                '            cboGoodsService.Enabled = False
                '            chkGSTCreditApp.Enabled = False
                '
                '            chkCreditRC.Value = IIf(.Fields("GST_RC_CLAIM").Value = "Y", vbChecked, vbUnchecked)
                '            If chkCreditRC.Value = vbChecked Then
                '                txtModvatNo.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_RC_NO").Value), "", .Fields("GST_CLAIM_RC_NO").Value), "00000")
                '                txtModvatDate.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_RC_DATE").Value), "", .Fields("GST_CLAIM_RC_DATE").Value), "DD/MM/YYYY")
                '            Else
                '                txtModvatNo.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value), "00000")
                '                txtModvatDate.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                '            End If
                lblGSTClaimNo.Text = IIf(IsDbNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value)
                lblGSTClaimDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                txtModvatNo.Text = IIf(IsDbNull(.Fields("GST_CLAIM_NEW_NO").Value), "", .Fields("GST_CLAIM_NEW_NO").Value)
                txtModvatDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_NEW_DATE").Value), "", .Fields("GST_CLAIM_NEW_DATE").Value), "DD/MM/YYYY")
                chkGSTClaim.CheckState = IIf(.Fields("GST_CLAIM").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                lblClaimStatus.Text = IIf(IsDbNull(.Fields("GST_CLAIM").Value), "N", .Fields("GST_CLAIM").Value)
                txtModvatNo.Enabled = False
                txtModvatDate.Enabled = False
                txtTotCGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_REFUNDAMT").Value), "", .Fields("TOTCGST_REFUNDAMT").Value), "0.00")
                txtTotSGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_REFUNDAMT").Value), "", .Fields("TOTSGST_REFUNDAMT").Value), "0.00")
                txtTotIGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_REFUNDAMT").Value), "", .Fields("TOTIGST_REFUNDAMT").Value), "0.00")
                txtPONo.Text = IIf(IsDbNull(.Fields("CUSTREFNO").Value), "", .Fields("CUSTREFNO").Value)
                txtPODate.Text = IIf(IsDbNull(.Fields("CUSTREFDATE").Value), "", .Fields("CUSTREFDATE").Value)
                txtBillNo.Text = IIf(IsDbNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                txtExpDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value), "DD/MM/YYYY")
                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If
                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                txtSupplier.Enabled = False
                txtBillTo.Enabled = False
                txtBillNo.Enabled = IIf(PubSuperUser = "S", True, False)

                txtExpDate.Enabled = False
                '            If MainClass.ValidateWithMasterTable(.Fields("MODVAT_SUPP_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                txtModvatSupp.Text = MasterNo
                '            End If
                txtCreditDays(0).Text = IIf(IsDbNull(.Fields("DUEDAYSFROM").Value), "", .Fields("DUEDAYSFROM").Value)
                txtCreditDays(1).Text = IIf(IsDbNull(.Fields("DUEDAYSTO").Value), "", .Fields("DUEDAYSTO").Value)
                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)
                lblTotQty.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                lblTotItemValue.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                lblTotCGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_AMOUNT").Value), "", .Fields("TOTCGST_AMOUNT").Value), "0.00")
                lblTotSGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_AMOUNT").Value), "", .Fields("TOTSGST_AMOUNT").Value), "0.00")
                lblTotIGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_AMOUNT").Value), "", .Fields("TOTIGST_AMOUNT").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                txtItemType.Text = IIf(IsDbNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtNarration.Text = IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtCarriers.Text = IIf(IsDbNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)
                txtVehicle.Text = IIf(IsDbNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                txtDocsThru.Text = IIf(IsDbNull(.Fields("DOCSTHROUGH").Value), "", .Fields("DOCSTHROUGH").Value)
                txtMode.Text = IIf(IsDbNull(.Fields("DESPATCHMODE").Value), "", .Fields("DESPATCHMODE").Value)
                txtPaymentDate.Text = IIf(IsDbNull(.Fields("PAYMENTDATE").Value), "", .Fields("PAYMENTDATE").Value)
                chkTDS.CheckState = IIf(.Fields("ISTDSDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkTDS.Enabled = IIf(.Fields("ISTDSDEDUCT").Value = "Y", False, True)
                txtTDSRate.Text = VB6.Format(IIf(IsDbNull(.Fields("TDSPer").Value), "", .Fields("TDSPer").Value), "0.000")
                txtTDSAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TDSAMOUNT").Value), "", .Fields("TDSAMOUNT").Value), "0.00")
                chkESI.CheckState = IIf(.Fields("ISESIDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkESI.Enabled = IIf(.Fields("ISESIDEDUCT").Value = "Y", False, True)
                txtESIRate.Text = VB6.Format(IIf(IsDbNull(.Fields("ESIPer").Value), "", .Fields("ESIPer").Value), "0.000")
                txtESIAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("ESIAMOUNT").Value), "", .Fields("ESIAMOUNT").Value), "0.00")
                ChkSTDS.CheckState = IIf(.Fields("ISSTDSDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                ChkSTDS.Enabled = IIf(.Fields("ISSTDSDEDUCT").Value = "Y", False, True)
                txtSTDSRate.Text = VB6.Format(IIf(IsDbNull(.Fields("STDSPer").Value), "", .Fields("STDSPer").Value), "0.000")
                txtSTDSAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("STDSAMOUNT").Value), "", .Fields("STDSAMOUNT").Value), "0.00")
                txtTDSDeductOn.Text = VB6.Format(IIf(IsDbNull(.Fields("TDS_DEDUCT_ON").Value), "", .Fields("TDS_DEDUCT_ON").Value), "0.00")
                txtSTDSDeductOn.Text = VB6.Format(IIf(IsDbNull(.Fields("STDS_DEDUCT_ON").Value), "", .Fields("STDS_DEDUCT_ON").Value), "0.00")
                txtESIDeductOn.Text = VB6.Format(IIf(IsDbNull(.Fields("ESI_DEDUCT_ON").Value), "", .Fields("ESI_DEDUCT_ON").Value), "0.00")
                txtJVVNO.Text = IIf(IsDBNull(.Fields("JVNO").Value), "", .Fields("JVNO").Value)
                lblJVTMKey.Text = IIf(IsDBNull(.Fields("JVT_MKEY").Value), "", .Fields("JVT_MKEY").Value)
                If lblJVTMKey.Text <> "" Then
                    If MainClass.ValidateWithMasterTable((lblJVTMKey.Text), "MKEY", "VNO", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtJVVNO.Text = Trim(MasterNo)
                    End If
                End If

                pSectionCode = IIf(IsDBNull(.Fields("SECTION_CODE").Value), -1, .Fields("SECTION_CODE").Value)

                If pSectionCode > 0 Then
                    If MainClass.ValidateWithMasterTable(pSectionCode, "CODE", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtSection.Text = MasterNo
                    End If
                End If

                OptFreight(0).Checked = True
                OptFreight(1).Checked = False
                chkFinalPost.CheckState = IIf(.Fields("ISFINALPOST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkSupplyOtherLoc.CheckState = IIf(.Fields("SUPPLY_OTHER_LOCATION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                '            chkFOC.Value = IIf(.Fields("ISFOC").Value = "Y", vbChecked, vbUnchecked)
                '            chkFOC.Enabled = IIf(.Fields("ISFOC").Value = "Y", True, False)
                '            txtBalAmount.Text = GetBillBalanceAmt(.Fields("SUPP_CUST_CODE").Value, txtBillNo.Text)
                mSACCode = IIf(IsDbNull(.Fields("SAC_CODE").Value), "", .Fields("SAC_CODE").Value)
                If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    txtServProvided.Text = Trim(MasterNo)
                Else
                    txtServProvided.Text = ""
                End If
                '            chkGSTCreditApp.Value = IIf(.Fields("IS_CREDITAPP").Value = "Y", vbChecked, vbUnchecked)
                '            chkReverserChargeApp.Value = IIf(.Fields("IS_REVERSECHARGEAPP").Value = "Y", vbChecked, vbUnchecked)
                '            If Left(cboGoodsService.Text, 1) = "S" Then
                '
                ''                If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "GST_APP", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                ''                    chkGSTCreditApp.Value = IIf(Trim(MasterNo) = "Y", vbChecked, vbUnchecked)
                ''                Else
                ''                    chkGSTCreditApp.Value = vbUnchecked
                ''                End If
                ''
                ''                If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "REVERSE_CHARGE_APP", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                ''                    chkReverserChargeApp.Value = IIf(Trim(MasterNo) = "Y", vbChecked, vbUnchecked)
                ''                Else
                ''                    chkReverserChargeApp.Value = vbUnchecked
                ''                End If
                '            Else
                '                chkGSTCreditApp.Value = vbChecked
                '                chkReverserChargeApp.Value = vbUnchecked
                '            End If
                txtServiceOn.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVICE_ON_AMT").Value), 0, .Fields("SERVICE_ON_AMT").Value), "0.00")
                txtProviderPer.Text = VB6.Format(IIf(IsDbNull(.Fields("SERV_PROVIDER_PER").Value), 0, .Fields("SERV_PROVIDER_PER").Value), "0.00")
                txtRecipientPer.Text = VB6.Format(IIf(IsDbNull(.Fields("SERV_RECIPIENT_PER").Value), 0, .Fields("SERV_RECIPIENT_PER").Value), "0.00")
                txtServiceTaxPer.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVICE_TAX_PER").Value), 0, .Fields("SERVICE_TAX_PER").Value), "0.00")
                txtServiceTaxAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVICE_TAX_AMOUNT").Value), 0, .Fields("SERVICE_TAX_AMOUNT").Value), "0.00")
                mAddUser = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")
                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)
                chkCancelled.Enabled = False
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False
                txtAdvVNo.Text = IIf(IsDbNull(.Fields("ADV_VNO").Value), "", .Fields("ADV_VNO").Value)
                txtAdvDate.Text = IIf(IsDbNull(.Fields("ADV_VDATE").Value), "", .Fields("ADV_VDATE").Value)
                txtAdvBal.Text = CStr(GetBalancePaymentAmount((.Fields("SUPP_CUST_CODE").Value), txtBillDate.Text, mVNo, (TxtVDate.Text), mDivisionCode, "AP", mBalCGST, mBalSGST, mBalIGST))
                '    txtAdvCGSTBal.Text = Format(mBalCGST, "0.00")
                '    txtAdvSGSTBal.Text = Format(mBalSGST, "0.00")
                '    txtAdvIGSTBal.Text = Format(mBalIGST, "0.00")
                txtAdvBal.Text = VB6.Format(txtAdvBal.Text, "0.00")
                txtItemAdvAdjust.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_ITEM_AMT").Value), 0, .Fields("ADV_ITEM_AMT").Value), "0.00")
                txtAdvAdjust.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_ADJUSTED_AMT").Value), 0, .Fields("ADV_ADJUSTED_AMT").Value), "0.00")
                txtAdvCGST.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_CGST_AMT").Value), 0, .Fields("ADV_CGST_AMT").Value), "0.00")
                txtAdvSGST.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_SGST_AMT").Value), 0, .Fields("ADV_SGST_AMT").Value), "0.00")
                txtAdvIGST.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_IGST_AMT").Value), 0, .Fields("ADV_IGST_AMT").Value), "0.00")
                '            cmdResetMRR.Enabled = True
                Call ShowDetail1((LblMKey.Text), mCustRefNo)
                Call ShowPaymentDetail1((LblMKey.Text), mSupplierCode)
                Call ShowExp1((LblMKey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                ''Call CalcTots
            End If
        End With
        txtVno.Enabled = True
        '    chkModvat.Enabled = False
        '    chkSTRefund.Enabled = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        FormatSprdMain(-1)
        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColHSN, ColIGSTAmount
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        '    txtSupplier.Enabled = True
        '    txtBillNo.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowExp1(ByRef mMkey As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String
        Call FillSprdExp()
        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select FIN_PURCHASE_EXP.EXPCODE,FIN_PURCHASE_EXP.EXPPERCENT, " & vbCrLf & " FIN_PURCHASE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From FIN_PURCHASE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_PURCHASE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_PURCHASE_EXP.Mkey='" & mMkey & "'"
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPurchExp.EOF = False Then
            RsPurchExp.MoveFirst()
            With SprdExp
                Do While Not RsPurchExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        .Col = ColExpName
                        If .Text = RsPurchExp.Fields("Name").Value Then Exit For
                    Next I
                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDbNull(RsPurchExp.Fields("ExpPercent").Value), "", RsPurchExp.Fields("ExpPercent").Value)))
                    .Col = ColExpAmt
                    If RsPurchExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDbNull(RsPurchExp.Fields("Amount").Value), "", RsPurchExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDbNull(RsPurchExp.Fields("Amount").Value), "", RsPurchExp.Fields("Amount").Value))))
                    End If
                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDbNull(RsPurchExp.Fields("CODE").Value), 0, RsPurchExp.Fields("CODE").Value)))
                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RsPurchExp.Fields("Add_Ded").Value = "A", "A", "D")
                    .Col = ColExpIdent
                    .Text = IIf(IsDbNull(RsPurchExp.Fields("Identification").Value), "", RsPurchExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If
                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDbNull(RsPurchExp.Fields("Taxable").Value), "N", RsPurchExp.Fields("Taxable").Value)
                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDbNull(RsPurchExp.Fields("Exciseable").Value), "N", RsPurchExp.Fields("Exciseable").Value)
                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDbNull(RsPurchExp.Fields("CalcOn").Value), "", RsPurchExp.Fields("CalcOn").Value)))
                    .Col = ColExpDebitAmt
                    .Text = CStr(Val(IIf(IsDbNull(RsPurchExp.Fields("DebitAmount").Value), "", RsPurchExp.Fields("DebitAmount").Value)))
                    .Col = ColRO
                    .Value = IIf(RsPurchExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    RsPurchExp.MoveNext()
                Loop
            End With
            '    Else
            '        If ADDMode = True Then
            '            Call FillExpFromPartyExp
            '        End If
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowDetail1(ByRef mMkey As String, ByRef mCustRefType As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset
        Dim mShortageQty As Double
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim SqlStr As String
        Dim mBillNo As Double
        Dim mReOffer As Double
        Dim mRejQty As Double
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        'Dim mHSNCode As String
        Dim mAccountCode As String
        Dim mColValue As String
        SqlStr = ""
        SqlStr = " SELECT FIN_PURCHASE_DET.* "
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_DET " & vbCrLf & " Where Mkey='" & mMkey & "'" & vbCrLf & " Order By SubRowNo"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I
                SprdMain.Col = ColGoodsServs
                mColValue = IIf(IsDbNull(.Fields("GOODS_SERVICE").Value), "G", .Fields("GOODS_SERVICE").Value)
                SprdMain.Value = IIf(mColValue = "S", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                SprdMain.Col = ColItemDesc
                mItemDesc = IIf(IsDbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)
                SprdMain.Text = mItemDesc
                SprdMain.Col = ColHSN
                mHSNCode = IIf(IsDbNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value)
                SprdMain.Text = mHSNCode
                SprdMain.Col = ColCreditApp
                mColValue = IIf(IsDbNull(.Fields("GST_CREDITAPP").Value), "N", .Fields("GST_CREDITAPP").Value)
                SprdMain.Value = IIf(mColValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                SprdMain.Col = ColRCApp
                mColValue = IIf(IsDbNull(.Fields("GST_RCAPP").Value), "N", .Fields("GST_RCAPP").Value)
                SprdMain.Value = IIf(mColValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                SprdMain.Col = ColExempted
                mColValue = IIf(IsDbNull(.Fields("GST_EXEMPTED").Value), "N", .Fields("GST_EXEMPTED").Value)
                SprdMain.Value = IIf(mColValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))
                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                '            SprdMain.Col = ColPORate
                '            SprdMain.Text = Val(IIf(IsNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value))
                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))
                SprdMain.Col = ColGSTableAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("GSTABLE_AMT").Value), 0, .Fields("GSTABLE_AMT").Value)))
                SprdMain.Col = ColPONo
                SprdMain.Text = CStr(IIf(IsDbNull(.Fields("CUST_REF_NO").Value), "", .Fields("CUST_REF_NO").Value))
                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))
                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))
                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))
                SprdMain.Col = ColAccountPostCode
                If MainClass.ValidateWithMasterTable(.Fields("ITEM_TRNTYPE").Value, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If
                SprdMain.Col = ColRCMkey
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("RCSALEBILLMKEY").Value), "", .Fields("RCSALEBILLMKEY").Value))
                SprdMain.Col = ColSaleBillPrefix
                SprdMain.Text = IIf(IsDbNull(.Fields("SALEBILLNOPREFIX").Value), "", .Fields("SALEBILLNOPREFIX").Value)
                SprdMain.Col = ColSaleBillSeq
                SprdMain.Text = Str(IIf(IsDbNull(.Fields("SALEBILLNOSEQ").Value), 0, .Fields("SALEBILLNOSEQ").Value))
                SprdMain.Col = ColSaleBillNo
                SprdMain.Text = IIf(IsDbNull(.Fields("SALEBILL_NO").Value), "", .Fields("SALEBILL_NO").Value)
                If Trim(lblSaleBillNo.Text) = "" Then
                    lblSaleBillNo.Text = IIf(SprdMain.Text = "", "", SprdMain.Text)
                Else
                    lblSaleBillNo.Text = IIf(SprdMain.Text = "", lblSaleBillNo.Text, lblSaleBillNo.Text & "," & SprdMain.Text)
                End If
                SprdMain.Col = ColSaleBillDate
                SprdMain.Text = IIf(IsDbNull(.Fields("SALEBILLDATE").Value), "", .Fields("SALEBILLDATE").Value)
                '            mAccountCode = IIf(IsNull(!PUR_ACCOUNT_CODE), "", !PUR_ACCOUNT_CODE)
                '            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                SprdMain.Text = MasterNo
                '            Else
                '                SprdMain.Text = ""
                '            End If
                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            'MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh
            'FormatSprdView()
            'SprdView.Focus()
            UltraGrid1.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mTotQty As Double
        Dim j As Integer
        Dim I As Integer
        Dim mItemDesc As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim mShortage As Double
        Dim mTotItemAmount As Double
        Dim mTotTaxableItemAmount As Double
        Dim pTotOthers As Double
        Dim pTotMSC As Double
        Dim pTotDiscount As Double
        Dim pTotRO As Double
        Dim pTotTCS As Double
        Dim mTotExp As Double
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim pTCSPer As Double
        Dim mGSTableAmount As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim pTotCGSTAmount As Double
        Dim pTotSGSTAmount As Double
        Dim pTotIGSTAmount As Double
        Dim pTotCGSTRefundAmount As Double
        Dim pTotSGSTRefundAmount As Double
        Dim pTotIGSTRefundAmount As Double
        Dim mExpName As String
        Dim mIsTaxable As String
        Dim mOtherTaxableAmount As Double
        Dim mGSTCreditApp As String
        Dim mRCApp As String
        Dim mExempted As String
        Dim mGSTTaxAmount As Double
        Dim mAddDeduct As String = "A"

        If FormActive = False Then Exit Sub

        pRound = 0
        mQty = 0
        mRate = 0
        '    mST = 0
        mItemAmount = 0
        mTotItemAmount = 0
        pTotCGSTRefundAmount = 0
        pTotSGSTRefundAmount = 0
        pTotIGSTRefundAmount = 0
        mTotExp = 0
        mTotQty = 0
        '    If Left(cboGoodsService.Text, 1) = "S" Then
        '        If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "GST_APP", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mGSTCreditApp = Trim(MasterNo)
        '        Else
        '            mGSTCreditApp = "N"
        '        End If
        '    Else
        '        mGSTCreditApp = "Y"
        '    End If
        '    mGSTCreditApp = IIf(chkGSTCreditApp.Value = vbChecked, "Y", "N")
        With SprdExp
            For I = 1 To SprdExp.MaxRows
                .Row = I
                .Col = ColExpName
                mExpName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y' AND TAXABLE='Y'") = True Then
                    mIsTaxable = MasterNo
                Else
                    mIsTaxable = "N"
                End If

                'mAddDeduct
                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "ADD_DED", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y'") = True Then
                    mAddDeduct = MasterNo
                Else
                    mAddDeduct = "A"
                End If

                If mIsTaxable = "Y" Then
                    .Col = ColExpAmt
                    mOtherTaxableAmount = mOtherTaxableAmount + (IIf(mAddDeduct = "A", 1, -1) * CDbl(VB6.Format(.Text, "0.00")))
                End If
            Next
        End With
        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc
                .Col = ColItemDesc
                If .Text = "" Then GoTo DontCalc
                mItemDesc = .Text
                '            .Col = ColRCApp
                '            If .Value = vbChecked Then GoTo DontCalc
                '
                '            .Col = ColCreditApp
                '            If .Value = vbUnchecked Then GoTo DontCalc
                '
                '            .Col = ColExempted
                '            If .Value = vbChecked Then GoTo DontCalc
                .Col = ColQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty
                .Col = ColRate
                mRate = Val(.Text)
                .Text = CStr(mRate)
                .Col = ColCGSTPer
                pCGSTPer = Val(.Text)
                .Col = ColSGSTPer
                pSGSTPer = Val(.Text)
                .Col = ColIGSTPer
                pIGSTPer = Val(.Text)
                .Col = ColAmount
                .Text = VB6.Format(mQty * mRate, "0.00")
                mItemAmount = CDbl(VB6.Format(mQty * mRate, "0.00"))
                mTotItemAmount = mTotItemAmount + CDbl(VB6.Format(mQty * mRate, "0.00"))
DontCalc:
            Next I
        End With
        mTotTaxableItemAmount = mTotItemAmount + mOtherTaxableAmount
        mGSTTaxAmount = 0
        With SprdMain
            '        If Val(mTotItemAmount) = 0 Then GoTo DontCalc1
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc1
                .Col = ColItemDesc
                If .Text = "" Then GoTo DontCalc1
                mItemDesc = .Text
                .Col = ColCreditApp
                mGSTCreditApp = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                .Col = ColRCApp
                mRCApp = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                .Col = ColExempted
                mExempted = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                .Col = ColQty
                mQty = Val(.Text)
                '            mTotQty = mTotQty + mQty
                .Col = ColRate
                mRate = Val(.Text)
                .Col = ColCGSTPer
                pCGSTPer = Val(.Text)
                .Col = ColSGSTPer
                pSGSTPer = Val(.Text)
                .Col = ColIGSTPer
                pIGSTPer = Val(.Text)
                .Col = ColAmount
                mItemAmount = Val(.Text)
                .Col = ColGSTableAmount
                If Val(CStr(mTotItemAmount)) = 0 Then
                    mGSTableAmount = 0
                Else
                    mGSTableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00")) '' Format(Val(.Text), "0.00")
                End If
                .Text = VB6.Format(Val(CStr(mGSTableAmount)), "0.00")
                mCGSTAmount = CDbl(VB6.Format(mGSTableAmount * pCGSTPer * 0.01, "0.00"))
                mSGSTAmount = CDbl(VB6.Format(mGSTableAmount * pSGSTPer * 0.01, "0.00"))
                mIGSTAmount = CDbl(VB6.Format(mGSTableAmount * pIGSTPer * 0.01, "0.00"))
                .Col = ColCGSTAmount
                .Text = VB6.Format(mCGSTAmount, "0.00")
                .Col = ColSGSTAmount
                .Text = VB6.Format(mSGSTAmount, "0.00")
                .Col = ColIGSTAmount
                .Text = VB6.Format(mIGSTAmount, "0.00")
                pTotCGSTAmount = pTotCGSTAmount + mCGSTAmount
                pTotSGSTAmount = pTotSGSTAmount + mSGSTAmount
                pTotIGSTAmount = pTotIGSTAmount + mIGSTAmount
                If mExempted = "N" Then
                    If mRCApp = "N" Then
                        If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Or VB.Left(cboGSTStatus.Text, 1) = "C" Then
                            If mGSTCreditApp = "Y" Then
                                pTotCGSTRefundAmount = pTotCGSTRefundAmount + CDbl(VB6.Format(mGSTableAmount * pCGSTPer * 0.01, "0.00"))
                                pTotSGSTRefundAmount = pTotSGSTRefundAmount + CDbl(VB6.Format(mGSTableAmount * pSGSTPer * 0.01, "0.00"))
                                pTotIGSTRefundAmount = pTotIGSTRefundAmount + CDbl(VB6.Format(mGSTableAmount * pIGSTPer * 0.01, "0.00"))
                            End If
                            mGSTTaxAmount = mGSTTaxAmount + CDbl(VB6.Format(mGSTableAmount * pCGSTPer * 0.01, "0.00"))
                            mGSTTaxAmount = mGSTTaxAmount + CDbl(VB6.Format(mGSTableAmount * pSGSTPer * 0.01, "0.00"))
                            mGSTTaxAmount = mGSTTaxAmount + CDbl(VB6.Format(mGSTableAmount * pIGSTPer * 0.01, "0.00"))
                        End If
                    End If
                End If
DontCalc1:
            Next I
        End With
        pTotDiscount = 0
        pTotRO = 0
        pTotTCS = 0
        mTotExp = 0
        mNetAccessAmt = Val(CStr(mTotItemAmount + mOtherTaxableAmount))
        Call BillExpensesCalcTots_GST(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTotItemAmount, 0, 0, 0, pTotIGSTAmount, pTotSGSTAmount, pTotCGSTAmount, 0, 0, 0, pTotOthers, 0, 0, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, pTCSPer, "PA")
        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotalGSTTax.Text = VB6.Format(mGSTTaxAmount, "#0.00")
        lblNetAmount.Text = VB6.Format(mTotItemAmount + mGSTTaxAmount + mTotExp, "#0.00") ' Format(mTotItemAmount + pTotCGSTRefundAmount + pTotSGSTRefundAmount + pTotIGSTRefundAmount + mTotExp, "#0.00")
        '    If Left(cboGSTStatus.Text, 1) = "R" Then
        '        lblNetAmount.text = Format(mTotItemAmount + mTotExp, "#0.00")
        '    Else
        '        lblNetAmount.text = Format(mTotItemAmount + pTotCGSTAmount + pTotSGSTAmount + pTotIGSTAmount + mTotExp, "#0.00")
        '    End If
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(Val(CStr(mTotItemAmount + mOtherTaxableAmount)), "#0.00")
        lblTotCGSTAmount.Text = VB6.Format(pTotCGSTAmount, "#0.00")
        lblTotSGSTAmount.Text = VB6.Format(pTotSGSTAmount, "#0.00")
        lblTotIGSTAmount.Text = VB6.Format(pTotIGSTAmount, "#0.00")
        '    If Left(cboGSTStatus.Text, 1) = "G" Then
        txtTotCGSTRefund.Text = VB6.Format(pTotCGSTRefundAmount, "#0.00")
        txtTotSGSTRefund.Text = VB6.Format(pTotSGSTRefundAmount, "#0.00")
        txtTotIGSTRefund.Text = VB6.Format(pTotIGSTRefundAmount, "#0.00")
        '    Else
        '        txtTotCGSTRefund.Text = Format(0, "#0.00")
        '        txtTotSGSTRefund.Text = Format(0, "#0.00")
        '        txtTotIGSTRefund.Text = Format(0, "#0.00")
        '    End If
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSDeductOn.Text = VB6.Format(IIf(Val(txtTDSDeductOn.Text) = 0, lblTotItemValue.Text, txtTDSDeductOn.Text), "#0.00")
        Else
            txtTDSDeductOn.Text = VB6.Format(lblTotItemValue.Text, "#0.00")
        End If
        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIDeductOn.Text = VB6.Format(IIf(Val(txtESIDeductOn.Text) = 0, lblTotItemValue.Text, txtESIDeductOn.Text), "#0.00")
        Else
            txtESIDeductOn.Text = VB6.Format(lblTotItemValue.Text, "#0.00")
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSDeductOn.Text = VB6.Format(IIf(Val(txtSTDSDeductOn.Text) = 0, lblTotItemValue.Text, txtSTDSDeductOn.Text), "#0.00")
        Else
            txtSTDSDeductOn.Text = VB6.Format(lblTotItemValue.Text, "#0.00")
        End If


        'If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    If ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
        '        txtTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtTdsRate.Text) * Val(txtTDSDeductOn.Text) / 100, 0), "0.00")
        '    Else
        '        txtTDSAmount.Text = VB6.Format(Val(txtTdsRate.Text) * Val(txtTDSDeductOn.Text) / 100, "0.00")
        '    End If
        'Else
        '    txtTDSAmount.Text = "0.00"
        'End If

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSAmount.Text = VB6.Format(Val(txtTDSRate.Text) * Val(txtTDSDeductOn.Text) / 100, "0.00")

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                txtTDSAmount.Text = IIf(Val(txtTDSAmount.Text) > Int(txtTDSAmount.Text), Int(txtTDSAmount.Text) + 1, Val(txtTDSAmount.Text))
            Else
                If ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtTDSAmount.Text), 0), "0.00")
                    If Val(txtTDSRate.Text) > 0 And Val(txtTDSDeductOn.Text) > 0 And Val(txtTDSAmount.Text) <= 1 Then
                        txtTDSAmount.Text = 1
                    End If
                End If
            End If

        Else
            txtTDSAmount.Text = "0.00"
        End If

        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtESIAmount.Text = VB6.Format(System.Math.Round(Val(txtESIRate.Text) * Val(txtESIDeductOn.Text) / 100, 0), "0.00")
            Else
                txtESIAmount.Text = VB6.Format(Val(txtESIRate.Text) * Val(txtESIDeductOn.Text) / 100, "0.00")
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
        '    Call CheckPORate
        Exit Sub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()
        pShowCalc = False
        LblMKey.Text = ""
        lblPMKey.Text = ""
        mSupplierCode = CStr(-1)
        lblSaleBillNoSeq.Text = ""
        lblSaleBillNo.Text = ""
        lblSaleBillDate.Text = ""
        lblTotalGSTTax.Text = CStr(0)
        lblClaimStatus.Text = ""
        '    chkCreditRC.Value = vbUnchecked
        lblPurchaseVNo.Text = ""
        '    chkGSTCreditApp.Value = vbUnchecked
        '    chkReverserChargeApp.Value = vbUnchecked
        '    mAuthSign = ""
        '    txtMRRNoPrefix.Text = RsCompany!Alias & vb6.Format(RsCompany!FYNO, "00")
        '    txtMRRNo.Text = ""
        '    txtMRRDate.Text = ""
        '    txtBillNoPrefix.Text = RsCompany!Alias & vb6.Format(RsCompany!FYNO, "00")
        '    txtBillNo.Text = ""
        '    txtBillNoSuffix.Text = IIf(LblBookCode.text = "-7", "E", "")
        '    txtBillDate.Text = Format(RunDate, "DD/MM/YYYY")
        '    TxtBillTm.Text = GetServerTime
        '    txtSupplier.Text = ""
        SSTab1.SelectedIndex = 0
        SSTabLevies.SelectedIndex = 0
        txtVno.Text = ""
        txtVNoPrefix.Text = mBookType
        txtVNoSuffix.Text = ""
        If Not IsDate(TxtVDate.Text) Then
            TxtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        End If
        chkCancelled.Enabled = False
        '4-07-2003 Commit on Mukesh Demand....
        ''    cboInvType.ListIndex = -1
        txtBillNo.Text = ""
        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtExpDate.Text = "" ''Format(RunDate, "DD/MM/YYYY")
        txtSupplier.Text = ""
        txtBillTo.Text = ""
        txtBillTo.Enabled = True
        '    txtModvatSupp.Text = ""
        txtCreditDays(0).Text = ""
        txtCreditDays(1).Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtRemarks.Text = ""
        txtNarration.Text = ""
        txtCarriers.Text = ""
        txtVehicle.Text = ""
        txtDocsThru.Text = ""
        txtMode.Text = ""
        OptFreight(0).Checked = True
        OptFreight(1).Checked = False
        lblGSTClaimNo.Text = ""
        lblGSTClaimDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtModvatNo.Text = ""
        txtModvatDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtModvatNo.Enabled = False
        txtModvatDate.Enabled = False
        chkGSTClaim.Enabled = False
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True
        '    cboGoodsService.Enabled = True
        '    cboGoodsService.ListIndex = -1
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblTotQty.Text = "0.00"
        lblTotCGSTAmount.Text = "0.00"
        lblTotSGSTAmount.Text = "0.00"
        lblTotIGSTAmount.Text = "0.00"
        lblTotOtherExp.Text = "0.00"
        txtTotCGSTRefund.Text = "0.00"
        txtTotSGSTRefund.Text = "0.00"
        txtTotIGSTRefund.Text = "0.00"
        txtTotCGSTRefund.Enabled = False
        txtTotSGSTRefund.Enabled = False
        txtTotIGSTRefund.Enabled = False
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        txtPaymentDate.Text = ""
        cboGSTStatus.SelectedIndex = -1
        cboGSTStatus.Enabled = True
        chkFinalPost.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Unchecked
        '    chkFOC.Value = vbUnchecked
        '    chkFOC.Enabled = False
        txtPaymentdate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtTdsRate.Text = "0.000"
        txtTDSAmount.Text = "0.00"
        chkTDS.Enabled = True
        chkESI.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtESIRate.Text = "0.000"
        txtESIAmount.Text = "0.00"
        chkESI.Enabled = True
        ChkSTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSTDSRate.Text = "0.000"
        txtSTDSAmount.Text = "0.00"
        ChkSTDS.Enabled = True
        txtJVVNO.Text = ""
        lblJVTMKey.Text = ""
        txtTDSDeductOn.Text = "0.00"
        txtESIDeductOn.Text = "0.00"
        txtSTDSDeductOn.Text = "0.00"
        txtSection.Text = ""
        ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked
        ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        FraServiceTax.Enabled = IIf(CDbl(LblBookCode.Text) = ConPurchaseBookCode, True, False)
        '    txtBalAmount.Text = "0.00"
        txtServProvided.Text = ""
        txtServiceOn.Text = ""
        txtProviderPer.Text = ""
        txtRecipientPer.Text = ""
        txtServiceTaxPer.Text = ""
        txtServiceTaxAmount.Text = ""
        txtAdvVNo.Text = ""
        txtAdvDate.Text = ""
        txtAdvBal.Text = ""
        txtItemAdvAdjust.Text = ""
        txtAdvAdjust.Text = ""
        txtAdvCGST.Text = ""
        txtAdvSGST.Text = ""
        txtAdvIGST.Text = ""
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        FraPostingDtl.Visible = False

        MainClass.ClearGrid(SprdPaymentDetail)
        Call FormatSprdPaymentDetail(-1, False)


        MainClass.ClearGrid(SprdPostingDetail)
        Call FormatSprdPostingDetail(-1)
        txtSupplier.Enabled = True
        txtBillNo.Enabled = True
        txtExpDate.Enabled = True
        '    If Left(cboGoodsService.Text, 1) = "G" Then
        '        chkGSTCreditApp.Enabled = True
        '    Else
        '        chkGSTCreditApp.Enabled = False
        '    End If
        pProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)
        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""
        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        pShowCalc = True
    End Sub
    Private Sub FillSprdExp()
        On Error GoTo ERR1
        Dim mLocal As String
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim I As Integer
        pShowCalc = False
        Dim xSuppCode As String
        MainClass.ClearGrid(SprdExp)
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xSuppCode = MasterNo
            End If
            If chkSupplyOtherLoc.CheckState = System.Windows.Forms.CheckState.Checked Then
                mLocal = "N"
            Else
                If Trim(txtSupplier.Text) <> "" Then
                    mLocal = GetPartyBusinessDetail(Trim(xSuppCode), Trim(txtBillTo.Text), "WITHIN_STATE")
                End If
            End If

            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = "N"
        End If
        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (Type='P' OR Type='B') "
        If CDate(txtBillDate.Text) >= CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " Order By PrintSequence"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            I = 0
            Do While Not RS.EOF
                I = I + 1
                SprdExp.Row = I
                SprdExp.Col = ColRO
                SprdExp.Value = IIf(RS.Fields("ROUNDOFF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value
                SprdExp.Col = ColExpPercent
                If ADDMode = True Then
                    SprdExp.Text = Str(IIf(IsDbNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                Else
                    SprdExp.Text = ""
                End If
                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"
                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDbNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))
                SprdExp.Col = ColExpAddDeduct
                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)

                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols - 1)

                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)
                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)
                If RS.Fields("Identification").Value = "ST" Then
                    If RS.Fields("STTYPE").Value = mLocal Then
                        SprdExp.RowHidden = False
                    Else
                        SprdExp.RowHidden = True
                    End If
                End If
                RS.MoveNext()
                If RS.EOF = False Then
                    SprdExp.MaxRows = SprdExp.MaxRows + 1
                End If
            Loop
        End If
        pShowCalc = True
        FormatSprdExp(-1)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FrmPurchaseWO_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmPurchaseWO_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub FrmPurchaseWO_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim x As Boolean
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        If InStr(1, XRIGHT, "D", CompareMethod.Text) > 1 Then
            chkCancelled.Enabled = True
        Else
            chkCancelled.Enabled = False
        End If

        mIsAuthorisedUser = False
        If InStr(1, XRIGHT, "S", CompareMethod.Text) > 0 Then
            mIsAuthorisedUser = True
        End If

        txtVNoPrefix.Text = mBookType
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtVno.Enabled = True
        '    txtStClaimNo.Enabled = False
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7755) '8000
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900
        SSTab1.SelectedIndex = 0
        'AdoDCMain.Visible = False
        txtSupplier.Enabled = True
        txtBillDate.Enabled = True
        txtExpDate.Enabled = True
        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        '    cboGoodsService.Clear
        '    cboGoodsService.AddItem "Goods"
        '    cboGoodsService.AddItem "Service"
        '    cboGoodsService.ListIndex = -1
        ' Control displays text tips aligned to pointer with focus
        SprdMain.TextTip = FPSpreadADO.TextTipConstants.TextTipFloatingFocusOnly
        ' Control displays text tips after 250 milliseconds
        SprdMain.TextTipDelay = 250
        ' Text tip displays custom font and colors
        ' Background is yellow, RGB(255, 255, 0)
        ' Foreground is dark blue, RGB(0, 0, 128)
        x = SprdMain.SetTextTipAppearance("Arial", CShort("10"), False, False, &HFFFF, &H800000)

        FormActive = False

        Call FrmPurchaseWO_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub OptFreight_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptFreight.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptFreight.GetIndex(eventSender)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdExp_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdExp.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdExp_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdExp.LeaveCell
        On Error GoTo ErrPart
        Static ESCol As Object
        Static ESRow As Integer
        Static m_Exp As Object
        Static mIDENT As String
        Static m_Amt As Object
        Static m_ExpPercent As Double
        Static m_xp As Object
        Static m_xpn As String
        Static p_DebitAmt As Double
        Static p_Amt As Double
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        ESCol = eventArgs.col
        ESRow = eventArgs.row
        Select Case eventArgs.col
            Case 1 'Exp.Name
                If eventArgs.newRow >= ESRow Or eventArgs.newRow = -1 Then
                    SprdExp.Row = ESRow
                    SprdExp.Col = 1
                    m_Exp = MainClass.AllowSingleQuote(SprdExp.Text)
                    If SprdExp.Text = "" Then Exit Sub
                    If m_Exp <> "" Then Exit Sub
                    SprdExp.Col = ColExpIdent
                    mIDENT = SprdExp.Text
                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND Name= '" & m_Exp & "'"
                    If PubGSTApplicable = True Then
                        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
                    End If
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
                    If RS.EOF = True Then
                        ESCol = 1
                        GoTo ErrPart
                    Else
                        If mIDENT = "ST" Then
                            SprdExp.Col = 2
                            SprdExp.Text = CStr(0)
                        End If
                        If RS.EOF = False Then
                            SprdExp.Row = ESRow
                            SprdExp.Col = 4
                            SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                        End If
                        SprdExp.Col = 1
                        If SprdExp.Text <> "" Then
                            If SprdExp.MaxRows = ESRow Then
                                MainClass.AddBlankSprdRow(SprdExp, ColExpName)
                                FormatSprdExp((SprdExp.MaxRows))
                            End If
                        End If
                    End If
                End If
            Case 2 'Exp. %
                If eventArgs.newRow >= ESRow Or eventArgs.newRow = -1 Then
                    SprdExp.Row = ESRow
                    SprdExp.Col = 1
                    If SprdExp.Text = "" Then Exit Sub
                    '               mExp = SprdExp.Text
                    m_xpn = SprdExp.Text
                    SprdExp.Col = 2
                    SprdExp.Row = ESRow
                    m_ExpPercent = Val(SprdExp.Value)
                    If m_ExpPercent = 0 Then
                        Exit Sub
                    Else
                        SprdExp.Col = ColExpIdent
                        mIDENT = SprdExp.Text
                        If mIDENT = "ST" Or mIDENT = "ED" Or mIDENT = "RO" Then
                            Call CalcTots()
                            Exit Sub
                        End If
                        SprdExp.Row = ESRow
                        SprdExp.Col = 3
                        If MainClass.ValidateWithMasterTable(m_xpn, "Name", "RoundOff", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                            If MasterNo = True Then
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(lblTotItemValue.Text)) / 100, "0")
                            Else
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(lblTotItemValue.Text)) / 100, "0.00")
                            End If
                        End If
                    End If
                Else
                    ESCol = 2
                    ESRow = eventArgs.newRow
                    GoTo ErrPart
                End If
            Case ColExpDebitAmt
                If eventArgs.newRow = -1 Then Exit Sub
                SprdExp.Row = ESRow
                SprdExp.Col = ColExpAmt
                p_Amt = Val(SprdExp.Text)
                SprdExp.Col = ColExpDebitAmt
                p_DebitAmt = Val(SprdExp.Text)
                If p_Amt < p_DebitAmt And p_DebitAmt <> 0 Then
                    MsgBox("Debit Amount Cann't be Greater Than Exp Amount.", MsgBoxStyle.Information)
                    Call MainClass.SetFocusToCell(SprdExp, ESRow, ColExpDebitAmt)
                    '                    Exit Sub
                End If
        End Select
        'Call DistributeExpInMainGrid
        Call CalcTots()
        Exit Sub
ErrPart:
        SprdExp.Col = ESCol
        SprdExp.col2 = ESCol
        SprdExp.Row = ESRow
        SprdExp.Row2 = ESRow
        SprdExp.BlockMode = True
        SprdExp.Action = 0
        SprdExp.BlockMode = False
        SprdExp.Focus()
    End Sub
    Private Sub SprdExp_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdExp.ClickEvent
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        '    If KeyCode = vbKeyF1 And mCol = ColItemCode Then SprdMain_Click ColItemCode, 0
        '    If KeyCode = vbKeyF1 And mCol = ColItemDesc Then SprdMain_Click ColItemDesc, 0
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColAccountPostCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColAccountPostCode, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        '    End With
    End Sub
    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text) 'MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCarriers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriers.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCarriers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriers.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCreditDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditDays.TextChanged
        Dim Index As Short = txtCreditDays.GetIndex(eventSender)
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCreditDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim Index As Short = txtCreditDays.GetIndex(eventSender)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDocsThru_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocsThru.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDocsThru_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDocsThru.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDocsThru.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtMode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtMode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNarration_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNarration.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNarration_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNarration.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtNarration.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
    Private Sub txtItemType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtItemType.Text), "FIN_ITEMTYPE_MST", "NAME", , , , SqlStr) = True Then
            txtItemType.Text = AcName
            If txtItemType.Enabled = True Then txtItemType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtItemType_DoubleClick(txtItemType, New System.EventArgs())
    End Sub
    Private Sub txtVehicle_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtVehicle.Text), "FIN_Vehicle_MST", "NAME", , , , SqlStr) = True Then
            txtVehicle.Text = AcName
            If txtVehicle.Enabled = True Then txtVehicle.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtVehicle_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicle.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtVehicle_DoubleClick(txtVehicle, New System.EventArgs())
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants, ByRef mPONo As String)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)
        '    Call InsertForPO(mPONo)
        SqlStr = ""
        Call SelectQryForPO(SqlStr, mPONo)
        '    SqlStr = FetchRecordForReport(SqlStr)
        mTitle = "PURCHASE ORDER"
        mRptFileName = "PO_View.rpt" ''mRptFileName = "PO.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, "Y")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForPO(ByRef mSqlStr As String, ByRef pPONO As String) As String
        On Error GoTo ErrPart
        Dim mSuppCode As String
        ''SELECT CLAUSE...
        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,"
        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"
        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, FIN_PAYTERM_MST PAYMST, INV_ITEM_MST IMST"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        mSqlStr = mSqlStr & vbCrLf & " AND IMST.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.ITEM_CODE=IMST.ITEM_CODE(+) AND PO_STATUS='Y'"
        If pPONO = "" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
            Else
                mSuppCode = "-1"
            End If
            mSqlStr = mSqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & mSuppCode & "'"
            If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
                mSqlStr = mSqlStr & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
            Else
                mSqlStr = mSqlStr & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
            End If
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(pPONO) & ""
        End If
        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.MKEY, ID.SERIAL_NO"
        SelectQryForPO = mSqlStr
        Exit Function
        SelectQryForPO = ""
ErrPart:
    End Function
    Private Sub InsertForPO(ByRef mPONo As String)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mSqlStr As String
        Dim mSuppCode As String
        Dim mRefType As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = "DELETE FROM Temp_PO NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        mSqlStr = ""
        ''SELECT CLAUSE...
        mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " IH.AUTO_KEY_PO, IH.COMPANY_CODE, " & vbCrLf & " IH.PUR_TYPE, IH.ORDER_TYPE, " & vbCrLf & " IH.PUR_ORD_DATE, IH.SUPP_CUST_CODE, " & vbCrLf & " IH.AMEND_NO, IH.AMEND_DATE, " & vbCrLf & " IH.REMARKS, WO_DESCRIPTION," & vbCrLf & " 'DELIVERY : ' || IH.DELIVERY || ' EXCISE : ' || IH.EXCISE_OTHERS || ' PAYMENT : ' || IH.PAYMENT_CODE || ' DESPATCH MODE : ' || IH.MODE_DESPATCH || ' INSPECTION : ' || IH.INSPECTION || ' PACKING & FORWARDING : ' || IH.PACKING_FORWARDING || ' INSURANCE : ' || IH.INSURANCE || ' OTHER TERMS1 : ' || IH.OTHERS_COND1 || ' OTHER TERMS2 : ' || IH.OTHERS_COND2 , " & vbCrLf & " ID.PO_WEF_DATE, " & vbCrLf
        mSqlStr = mSqlStr & " ID.ITEM_CODE, " & vbCrLf & " ID.ITEM_UOM, ID.ITEM_QTY, " & vbCrLf & " ID.ITEM_PRICE, ID.ITEM_DIS_PER, ID.ITEM_QTY*ID.ITEM_PRICE, " & vbCrLf & " ITEM_SHORT_DESC, SUPP_CUST_NAME "
        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID," & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IMST.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If Trim(mPONo) = "" Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
            Else
                mSuppCode = "-1"
            End If
            mSqlStr = mSqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & mSuppCode & "'"
            mSqlStr = mSqlStr & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(mPONo) & ""
        End If
        mSqlStr = mSqlStr & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.ITEM_CODE=IMST.ITEM_CODE(+) AND PO_STATUS='Y'"
        ''& " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf _
        '
        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_PO,IH.AMEND_NO"
        SqlStr = " INSERT INTO TEMP_PO (" & vbCrLf & " USERID, AUTO_KEY_PO, COMPANY_CODE, " & vbCrLf & " PUR_TYPE, ORDER_TYPE, PUR_ORD_DATE, " & vbCrLf & " SUPP_CUST_CODE, AMEND_NO, AMEND_DATE, " & vbCrLf & " REMARKS, WO_DESCRIPTION," & vbCrLf & " CONDITIONS_CHG, " & vbCrLf & " AMEND_WEF_DATE, " & vbCrLf & " ITEM_CODE, ITEM_UOM, ITEM_QTY, " & vbCrLf & " ITEM_PRICE, ITEM_DIS_PER, GROSS_AMT, ITEM_SHORT_DESC, SUPP_CUST_NAME ) " & mSqlStr
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PO " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY AUTO_KEY_PO,AMEND_NO"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef pIsPO As String)
        'Dim Printer As New Printer
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mStateName As String
        Dim mStateCode As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDbNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        If pIsPO = "Y" Then
            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub
            Report1.SubreportToChange = ""
        Else
            mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.Text) = 0, 0, lblNetAmount.Text)))
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""Rs. Zero""")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""0.00""")
            Else
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & lblNetAmount.Text & """")
            End If
            SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf & " FROM FIN_PURCHASE_EXP, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_PURCHASE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_PURCHASE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY SUBROWNO"
            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub
            '        Report1.SubreportToChange = ""
            SqlStrSub = " SELECT ACM.SUPP_CUST_NAME, " & vbCrLf & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC " & vbCrLf & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE ='" & mBookType & "'" & vbCrLf & " AND BOOKSUBTYPE = '" & mBookSubType & "'" & vbCrLf & " AND TRN.MKEY='" & LblMKey.Text & "'" & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME"
            Report1.SubreportToChange = Report1.GetNthSubreportName(1)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub
            Report1.SubreportToChange = ""
        End If
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
        Report1.ReportFileName = ""
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub txtSection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSection_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSection.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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
            txtSection.Text = Trim(AcName)
            txtSection_Validating(txtSection, New System.ComponentModel.CancelEventArgs(False)) ''_Validate False
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtSection_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSection.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSection()
    End Sub

    Private Sub txtSection_DoubleClick(sender As Object, e As EventArgs) Handles txtSection.DoubleClick
        Call SearchSection()
    End Sub

    Private Sub FrmPurchaseWO_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SSTab1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        'SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdPaymentDetail_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPaymentDetail.Change
        MainClass.SaveStatus(frmAtrn.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdPaymentDetail_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPaymentDetail.ClickEvent

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCompanyCode As Long
        Dim mShortName As String
        Dim mSupplierCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If
        Select Case eventArgs.col
            Case 0
                If eventArgs.row > 0 Then
                    MainClass.DeleteSprdRow(SprdPaymentDetail, eventArgs.row, ColPayBillNo)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    CalcTots()
                End If
            Case ColPayBillNo
                If eventArgs.row = 0 Then
                    SearchBill(mSupplierCode)
                End If
        End Select
        Exit Sub
ERR1:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchBill(ByRef pSupplierCode As String)

        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String

        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        ''mBalAmtStr = "ABS(" & mBillAmtStr & ")-ABS(" & mPayAmtStr & ")"
        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""


        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf _
            & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf _
            & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf _
            & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf _
            & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        SqlStr = " Select BillNo, BillDate, LOCATION_ID," & vbCrLf _
            & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf _
            & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC , " & vbCrLf _
            & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf _
            & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf _
            & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf _
            & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf _
            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, MAX(DUEDATE) AS DUEDATE,COMPANY_CODE  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & pSupplierCode & "'"      '' AND TRNTYPE='B'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'End If
        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""

        SqlStr = SqlStr & vbCrLf & " AND LOCATION_ID='" & txtBillTo.Text & "'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY  BillDate, BillNo,COMPANY_CODE,LOCATION_ID" & vbCrLf _
            & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf _
            & " ORDER BY BillDate, BillNo "

        MainClass.SearchGridMasterBySQL("", SqlStr)

        If AcName <> "" Then
            SprdPaymentDetail.Row = SprdPaymentDetail.ActiveRow
            SprdPaymentDetail.Col = ColPayBillNo
            SprdPaymentDetail.Text = AcName
            SprdPaymentDetail.Col = ColPayBillDate
            SprdPaymentDetail.Text = AcName1
            MainClass.SetFocusToCell(SprdPaymentDetail, SprdPaymentDetail.ActiveRow, ColPayBillNo)
        End If
        Exit Sub

    End Sub
    Private Sub SprdPaymentDetail_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdPaymentDetail.KeyDownEvent

        Dim mPayType As String
        Dim mActiveCol As Integer
        Dim mActiveRow As Integer
        Dim mSupplierCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If

        mActiveCol = SprdPaymentDetail.ActiveCol
        mActiveRow = SprdPaymentDetail.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColPayPaymentAmt Then
                SprdPaymentDetail.Row = SprdPaymentDetail.ActiveRow
                SprdPaymentDetail.Col = ColPayPaymentAmt
                If Val(SprdPaymentDetail.Text) <> 0 Then
                    If SprdPaymentDetail.MaxRows = SprdPaymentDetail.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdPaymentDetail, ColPayBillNo, ConRowHeight)
                        FormatSprdPaymentDetail((SprdPaymentDetail.MaxRows), False)
                        MainClass.SetFocusToCell(SprdPaymentDetail, mActiveRow, ColPayPaymentAmt)
                    End If
                End If

            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If SprdPaymentDetail.ActiveCol = ColPayBillNo Then SearchBill(mSupplierCode)
        End If
        eventArgs.keyCode = 9999
    End Sub
    Private Sub SprdPaymentDetail_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPaymentDetail.LeaveCell

        On Error GoTo ERR1

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        Dim mPayType As String
        Dim mBillNo As String
        Dim mAmount As Double
        Dim mBillDate As String
        Dim mDueDays As Double
        Dim mPayCode As String
        Dim mPONo As String
        Dim mAccountCode As String = ""
        Dim mPrevBillAmount As Double
        Dim mCurrBillAmount As Double
        Dim mPOAmount As Double
        Dim mCompanyCode As Long
        Dim mCurrCompanyCode As Long
        Dim mBillCompanyName As String
        Dim mSupplierCode As String

        If eventArgs.newRow = -1 Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If

        SprdPaymentDetail.Row = eventArgs.row

        SprdPaymentDetail.Col = ColPayBillNo
        mBillNo = SprdPaymentDetail.Text

        SprdPaymentDetail.Col = ColPayBillDate
        mBillDate = SprdPaymentDetail.Text


        mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value   ''GetCompanyCode(mBillNo, mBillDate, lblAccountCode.Text)       ' IIf(Val(SprdPaymentDetail.Text) <= 0, RsCompany.Fields("COMPANY_CODE").Value, Val(SprdPaymentDetail.Text))

        Dim mAccountName As String
        Select Case eventArgs.col

            Case ColPayBillNo

                If DuplicatePayBillNo() = False Then
                    If CheckBillNo(mSupplierCode) = True Then

                    End If
                    SprdPaymentDetail.Row = eventArgs.row

                    SprdPaymentDetail.Col = ColPayBillNo
                    mBillNo = SprdPaymentDetail.Text

                    '-------- FILLING BILL AMT TO AMT COL

                    SprdPaymentDetail.Col = ColPayBalAmount
                    mAmount = Val(SprdPaymentDetail.Text)
                    SprdPaymentDetail.Col = ColPayPaymentAmt
                    If Val(SprdPaymentDetail.Text) = 0 Then
                        SprdPaymentDetail.Text = IIf(Val(lblDiffAmt.Text) >= mAmount, mAmount, Val(lblDiffAmt.Text))
                    End If
                    '                MainClass.SetFocusToCell SprdPaymentDetail, Row, ColPayPaymentAmt
                    '                SprdPaymentDetail.Col = ColPayType
                End If
            Case ColPayBillDate
                SprdPaymentDetail.Row = eventArgs.row

                If DuplicatePayBillNo() = False Then
                    If CheckBillNo(mSupplierCode) = True Then

                    End If
                    If mPayType = "N" Then
                        SprdPaymentDetail.Row = eventArgs.row
                        SprdPaymentDetail.Col = ColPayBillDate
                        mBillDate = SprdPaymentDetail.Text

                        SprdPaymentDetail.Col = ColPayPaymentAmt
                        If Val(SprdPaymentDetail.Text) = 0 Then SprdPaymentDetail.Text = CStr(Val(lblDiffAmt.Text))
                    End If
                End If
            Case ColPayPaymentAmt
                SprdPaymentDetail.Row = eventArgs.row        ''SprdPaymentDetail.ActiveRow
                SprdPaymentDetail.Col = ColPayBillNo
                mBillNo = SprdPaymentDetail.Text
                SprdPaymentDetail.Col = ColPayPaymentAmt

                If CheckPayAmount() = False Then
                    MainClass.SetFocusToCell(SprdPaymentDetail, eventArgs.row, ColPayPaymentAmt)
                    Exit Sub
                End If

            Case ColPayBalDC
                SprdPaymentDetail.Col = ColPayBalDC
                SprdPaymentDetail.Row = eventArgs.row
                If UCase(SprdPaymentDetail.Text) = "DR" Or UCase(SprdPaymentDetail.Text) = "D" Then
                    SprdPaymentDetail.Text = "Dr"
                    Exit Sub
                ElseIf UCase(SprdPaymentDetail.Text) = "CR" Or UCase(SprdPaymentDetail.Text) = "C" Then
                    SprdPaymentDetail.Text = "Cr"
                    Exit Sub
                Else
                    eventArgs.col = ColPayBalDC
                    SprdPaymentDetail.Text = "Dr"
                    Exit Sub
                End If
                '            If Row <> NewRow Then CheckForEqualAmount

        End Select
        CalcTotsPayment()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Function DuplicatePayBillNo() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckBillNo As String
        Dim mBillNo As String
        Dim mFYear As Integer

        With SprdPaymentDetail
            .Row = .ActiveRow
            .Col = ColPayBillNo
            mCheckBillNo = Trim(UCase(.Text))

            .Col = ColPayBillDate
            If Trim(.Text) <> "" Then
                If Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) >= 4 And Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) <= 12 Then
                    mFYear = CInt(VB6.Format(.Text, "YYYY"))
                Else
                    mFYear = CDbl(VB6.Format(.Text, "YYYY")) - 1
                End If
            End If

            mCheckBillNo = mCheckBillNo & ":" & VB6.Format(mFYear, "0000")

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPayBillNo
                mBillNo = Trim(UCase(.Text))

                .Col = ColPayBillDate
                If Trim(.Text) <> "" Then
                    If Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) >= 4 And Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) <= 12 Then
                        mFYear = CInt(VB6.Format(.Text, "YYYY"))
                    Else
                        mFYear = CDbl(VB6.Format(.Text, "YYYY")) - 1
                    End If
                End If
                mBillNo = mBillNo & ":" & VB6.Format(mFYear, "0000")

                If (mBillNo = mCheckBillNo And mCheckBillNo <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicatePayBillNo = True
                    MainClass.SetFocusToCell(SprdPaymentDetail, .ActiveRow, ColPayBillNo, "Duplicate Bill No. : " & Mid(mCheckBillNo, 2))
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function CheckPayAmount() As Boolean
        Dim mDC As String
        Dim mBalance As Double
        Dim mBalanceDC As String
        Dim mOldAmount As Double
        Dim mOldDC As String
        Dim mNetBalance As Double
        Dim mCurrAmount As Double

        With SprdPaymentDetail

            .Col = ColPayBalDC
            mBalanceDC = VB.Left(.Text, 1)

            .Col = ColPayBalAmount
            mBalance = Val(.Text) * IIf(mBalanceDC = "D", 1, -1)

            mNetBalance = mBalance + mOldAmount

            mDC = mBalanceDC

            .Col = ColAmount
            mCurrAmount = Val(.Text) * IIf(mDC = "D", -1, 1)

            If System.Math.Abs(mCurrAmount) > System.Math.Abs(mNetBalance) Then
                ErrorMsg("Amount Exceeds", "", MsgBoxStyle.Critical)
                CheckPayAmount = False
            Else
                CheckPayAmount = True
            End If


        End With
    End Function
    Private Sub CalcTotsPayment()
        On Error GoTo ErrSprdTotal
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mNetAmt As Double
        Dim MTotalAmt As Double
        Dim cntRow As Integer
        Dim mDC As String
        Dim mDrCr As String = ""

        With SprdPaymentDetail
            For cntRow = 1 To .MaxRows - 1 Step 1
                .Row = cntRow


                .Col = ColPayBalDC
                mDC = VB.Left(.Text, 1)

                .Col = ColPayPaymentAmt
                If mDC = "D" Then
                    mDAmt = mDAmt + Val(.Value)
                Else
                    mCAmt = mCAmt + Val(.Value)
                End If

                mNetAmt = System.Math.Abs(mCAmt - mDAmt)

NextRow:
            Next cntRow
        End With


        lblDiffAmt.Text = Val(lblNetAmount.Text) - Val(mNetAmt)

ErrSprdTotal:
    End Sub
    Private Function UpdatePaymentDetail1(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pCurrRowNo As Integer, ByRef pBookCode As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pTRNType As String, ByRef pSupplierCode As String, ByRef pAccountCode As String, ByRef pItemValue As Double, ByRef pNetBillValue As Double, ByRef pCancel As Boolean, ByRef pFOC As Boolean, ByRef pDueDate As String, ByRef pNarration As String, ByRef pRemarks As String,
                    ByRef pExpAmount As Double, ByRef pISGSTRefund As String, ByRef pCGSTRefundAmount As Double, ByRef pSGSTRefundAmount As Double, ByRef pIGSTRefundAmount As Double, ByRef pMRRDate As String, ByRef pAddMode As Boolean, ByRef pAddUser As String, ByRef pAddDate As String, ByRef mDivisionCode As Double, ByRef pReverseCharge As String, ByRef pReverseTaxAmount As Double, ByRef pReverseCGST As Double, ByRef pReverseSGST As Double, ByRef pReverseIGST As Double, ByRef pSaleBillNo As String,
                    ByRef pSaleBillDate As String, ByRef pLoactionID As String) As Boolean



        On Error GoTo UpdatePaymentDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mPayBillNo As String
        Dim mPayBillDate As String
        Dim mPayBillAmount As Double
        Dim mPayBalDC As String
        Dim mPayPaymentAmt As Double
        Dim mTotPayPaymentAmt As Double
        Dim mSubRowNo As Long

        PubDBCn.Execute("Delete From FIN_PURBILLDETAILS_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "'")

        pDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & UCase(pBookType) & "' AND BookCode='" & UCase(pBookCode) & "' AND BILLTYPE='P'")

        mTotPayPaymentAmt = 0
        If SprdPaymentDetail.MaxRows = 1 Then
            UpdatePaymentDetail1 = True
            Exit Function
        End If

        mSubRowNo = 1000

        With SprdPaymentDetail
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColPayBillNo
                mPayBillNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColPayBillDate
                mPayBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColPayBillAmount
                mPayBillAmount = Val(.Text)

                .Col = ColPayBalDC
                mPayBalDC = Mid(.Text, 1, 1)

                .Col = ColPayPaymentAmt
                mPayPaymentAmt = Val(.Text)

                mTotPayPaymentAmt = mTotPayPaymentAmt + mPayPaymentAmt

                SqlStr = ""
                If mPayBillNo <> "" And mPayPaymentAmt > 0 Then
                    SqlStr = " INSERT INTO FIN_PURBILLDETAILS_TRN (COMPANY_CODE, " & vbCrLf _
                        & " MKEY , SUBROWNO, " & vbCrLf _
                        & " ACCOUNTCODE , BILLNO, BILLDATE, BILLAMOUNT, BILLDC, " & vbCrLf _
                        & " AMOUNT , DC, BOOKTYPE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & pMKey & "'," & I & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pSupplierCode) & "','" & mPayBillNo & "',TO_DATE('" & VB6.Format(mPayBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mPayBillAmount & ", " & vbCrLf _
                        & " '" & mPayBalDC & "'," & mPayPaymentAmt & ",'" & mPayBalDC & "','" & UCase(mBookType) & "') "

                    PubDBCn.Execute(SqlStr)

                    'If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then
                    'Else
                    If UpdateTRN(pDBCn, pMKey, pCurrRowNo, mSubRowNo + I, pBookCode, "P", pBookType, pBookSubType, pSupplierCode,
                         pVNo, pVDate, mPayBillNo, mPayBillDate, mPayPaymentAmt, "C", "O", "", "",
                         CStr(-1), CStr(-1), CStr(-1), CStr(-1), pDueDate, "", "P", "", "",
                         pNarration, pRemarks, pMRRDate, pAddMode, pAddUser, pAddDate, mDivisionCode, pLoactionID) = False Then GoTo UpdatePaymentDetail1
                    'End If


                End If
            Next
        End With

        'If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then
        'Else
        If UpdateTRN(pDBCn, pMKey, pCurrRowNo, mSubRowNo + I, pBookCode, "P", pBookType, pBookSubType, pSupplierCode,
                         pVNo, pVDate, pBillNo, pBillDate, mTotPayPaymentAmt, "D", "B", "", "",
                         CStr(-1), CStr(-1), CStr(-1), CStr(-1), pDueDate, "", "P", "", "",
                         pNarration, pRemarks, pMRRDate, pAddMode, pAddUser, pAddDate, mDivisionCode, pLoactionID) = False Then GoTo UpdatePaymentDetail1
        'End If


        UpdatePaymentDetail1 = True
        Exit Function
UpdatePaymentDetail1:
        UpdatePaymentDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub ShowPaymentDetail1(ByRef mMkey As String, ByRef mSupplierCode As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim SqlStr As String = ""
        Dim mBillNo As String
        Dim mBillDate As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_PURBILLDETAILS_TRN " & vbCrLf _
            & " Where Mkey='" & mMkey & "' AND BookType='" & UCase(mBookType) & "'" & vbCrLf _
            & " Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If .EOF = True Then Exit Sub
            FormatSprdPaymentDetail(-1, False)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdPaymentDetail.Row = I
                SprdPaymentDetail.Col = ColPayBillNo
                SprdPaymentDetail.Text = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                mBillNo = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)

                SprdPaymentDetail.Col = ColPayBillDate
                SprdPaymentDetail.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")
                mBillDate = VB6.Format(IIf(IsDBNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")

                SprdPaymentDetail.Col = ColPayBillAmount
                SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("BILLAMOUNT").Value), 0, .Fields("BILLAMOUNT").Value)))

                'SprdPaymentDetail.Col = ColPayBalAmount
                'SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                SprdPaymentDetail.Col = ColPayBalDC
                SprdPaymentDetail.Text = IIf(IsDBNull(.Fields("DC").Value), "D", .Fields("DC").Value)

                SprdPaymentDetail.Col = ColPayPaymentAmt
                SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("AMOUNT").Value), 0, .Fields("AMOUNT").Value)))

                Call GetBalanceAmount(I, (mSupplierCode), mBillNo, mBillDate, "B")

                .MoveNext()
                I = I + 1
                SprdPaymentDetail.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub FormatSprdPaymentDetail(ByRef Arow As Integer, ByRef mFromPopulate As Boolean)

        On Error GoTo ErrPart
        Dim RsTRN As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = "SELECT * FROM FIN_POSTED_TRN WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRN, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdPaymentDetail
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = 0
            .set_ColWidth(0, 3)

            .Col = ColPayBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTRN.Fields("BillNo").DefinedSize ''
            .set_ColWidth(.Col, 12)

            .ColsFrozen = ColPayBillNo


            .Col = ColPayBillDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 8)


            .Col = ColPayBillAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)


            .Col = ColPayBalAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8.25)

            .Col = ColPayBalDC
            .CellType = SS_CELL_TYPE_EDIT
            If mFromPopulate = False Then
                .Text = "Cr"    ''IIf(VB.Left(lblDC.Text, 1) = "C", "Cr", "Dr")
            End If
            .set_ColWidth(.Col, 3)

            .Col = ColPayPaymentAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8.5)

            .Row = Arow
            MainClass.ProtectCell(SprdPaymentDetail, 1, .MaxRows, ColPayBillDate, ColPayBalDC)
            'MainClass.ProtectCell(SprdPaymentDetail, 1, .MaxRows, ColCompanyCode, ColCompanyCode)
            MainClass.SetSpreadColor(SprdPaymentDetail, Arow)


        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub GetBalanceAmount(ByRef pRow As Integer, ByRef pAccountCode As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pPayType As String)

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBalance As Double
        Dim mActBillAmount As Double
        Dim mBillAmount As Double
        Dim mPaymentAmt As Double
        Dim mDueDays As Double
        Dim mBillDate As String
        Dim mPayCode As String
        Dim mBillDC As String
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mCompanyName As String
        Dim mVNO As String


        mVNO = Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text)

        SqlStr = " Select Company_Code,BillNo, BillDate,MAX(EXPDATE) AS DueDate , " & vbCrLf _
            & " SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) AS BillAMT, " & vbCrLf _
            & " SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) AS PayAMT " & vbCrLf _
            & " FROM FIN_POSTED_TRN  "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ACCOUNTCODE = '" & MainClass.AllowSingleQuote(pAccountCode) & "'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        'End If
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(pBillNo) & "'"

        SqlStr = SqlStr & vbCrLf & " AND VNo<>'" & MainClass.AllowSingleQuote(mVNO) & "'"

        ''18-03-2010  ''Check New Bill Also.....
        If pPayType = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND BillDate>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BillDate<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            If pBillDate <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND BillDate=TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY Company_Code,BillNo, BillDate " & vbCrLf & " ORDER BY BillNo, BillDate,ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount))-SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdPaymentDetail
            .Row = pRow
            If RsTemp.EOF = False Then


                mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value

                .Col = ColPayBillDate
                mBillDate = IIf(IsDBNull(RsTemp.Fields("BillDate").Value), "", VB6.Format(RsTemp.Fields("BillDate").Value, "DD/MM/YYYY"))
                .Text = IIf(IsDBNull(RsTemp.Fields("BillDate").Value), "", VB6.Format(RsTemp.Fields("BillDate").Value, "DD/MM/YYYY"))

                .Col = ColPayBillAmount
                mActBillAmount = GetBillAmount(pAccountCode, pBillNo, mBillDate, Val(IIf(IsDBNull(RsTemp.Fields("BILLAMT").Value), 0, RsTemp.Fields("BILLAMT").Value)))
                mBillAmount = Val(IIf(IsDBNull(RsTemp.Fields("BILLAMT").Value), 0, RsTemp.Fields("BILLAMT").Value))
                .Text = Str(System.Math.Abs(mActBillAmount))

                '.Col = ColBillAmountDC
                '.Text = IIf(mActBillAmount >= 0, "Dr", "Cr")
                'mBillDC = IIf(mBillAmount >= 0, "Dr", "Cr")

                .Col = ColPayBalAmount
                'mPaymentAmt = Val(.Text)
                mPaymentAmt = Val(IIf(IsDBNull(RsTemp.Fields("PAYAMT").Value), 0, RsTemp.Fields("PAYAMT").Value))
                mBalance = mBillAmount + mPaymentAmt
                .Text = Str(System.Math.Abs(mBalance))
                '.Text = Str(Abs(mBalance) + Abs(mPRAmount))

                .Col = ColPayBalDC
                If mBalance = 0 Then
                    .Text = mBillDC
                Else
                    .Text = IIf(mBalance > 0, "Dr", "Cr")
                End If

                '********************
                .Row = .MaxRows
                .Row2 = .MaxRows
                .Col = 1
                .Col2 = .MaxCols
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(IIf(UCase(mBillDC) = "CR", &H8000000F, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White))) ''&H80FF80
                .BlockMode = False
                '********************
            End If
        End With
    End Sub
    Private Function GetBillAmount(ByRef xAccountCode As String, ByRef xBillNo As String, ByRef xBillDate As String, ByRef xBillAmount As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheck As Integer
        Dim mBillYear As Integer


        mBillYear = GetCurrentFYNo(PubDBCn, xBillDate)
        If mBillYear = RsCompany.Fields("FYEAR").Value Then
            GetBillAmount = xBillAmount
            Exit Function
        End If

        mCheck = 1

NextSearch:
        GetBillAmount = 0
        SqlStr = " Select SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) AS BillAMT " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " ACCOUNTCODE = '" & MainClass.AllowSingleQuote(xAccountCode) & "'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        'End If
        If mCheck = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE<>'O'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='O'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(xBillNo) & "'"
        SqlStr = SqlStr & vbCrLf & " AND BillDate=TO_DATE('" & VB6.Format(xBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetBillAmount = IIf(IsDBNull(RsTemp.Fields("BillAMT").Value), 0, RsTemp.Fields("BillAMT").Value)
        Else
            If mCheck = 2 Then
                GetBillAmount = 0
            Else
                '            mBillYear = GetCurrentFYNo(PubDBCn, xBillDate)
                If mBillYear = RsCompany.Fields("FYEAR").Value Then
                    GetBillAmount = 0
                Else
                    mCheck = 2
                    GoTo NextSearch
                End If
            End If
        End If
        Exit Function
ErrPart:
        GetBillAmount = 0
    End Function
    Private Function CheckBillNo(ByRef pSupplierCode As String) As Boolean
        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        Dim mPayType As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillAmount As Double
        Dim mDC As String
        Dim mPaymentAmt As Double

        Dim mBalance As Double
        Dim mRow As Integer
        Dim cntRow As Integer
        Dim mOldAmount As Double

        With SprdPaymentDetail
            mRow = .ActiveRow
            .Row = mRow
            .Col = ColPayBillNo
            mBillNo = Trim(.Text)

            If mBillNo = "" Then
                .Row = mRow
                .Col = ColPayBillNo
                .Text = ""

                .Col = ColPayBillDate
                .Text = ""

                .Col = ColPayBillAmount
                .Text = "0.00"

                .Col = ColPayBalAmount
                .Text = "0.00"

                .Col = ColPayPaymentAmt
                .Text = "0.00"

                CheckBillNo = True
                Exit Function
            End If



            .Col = ColPayBillDate
            mBillDate = .Text

            Call GetBalanceAmount(mRow, pSupplierCode, mBillNo, mBillDate, "B")
            'Call PickUpBillPayment("B", mBillNo, mOldAmount, "D")

        End With
        CheckBillNo = True
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdUpdatePayment_Click(sender As Object, e As EventArgs) Handles CmdUpdatePayment.Click
        On Error GoTo ErrPart
        Dim mMannualAdjustment As String
        Dim mSRBillNo As String
        Dim mSRBillDate As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim RsPostSRTrn As ADODB.Recordset
        Dim mRow As Long
        Dim mBalanceAmount As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String

        Dim nMkey As String
        Dim mTRNType As String
        Dim mVNoSeq As Double
        Dim mVNo As String
        Dim mSuppCustCode As String
        Dim mModvatSuppCode As String
        Dim mAccountCode As String
        Dim mFREIGHTCHARGES As String
        Dim mEXEMPT_NOTIF_NO As String
        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double
        Dim mTotEDAmount As Double
        Dim mTotEDUAmount As Double
        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mPRINTED As String
        Dim mCancelled As String
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mBookCode As Integer
        Dim mStartingNo As Double
        Dim mSTPERCENT As Double
        Dim mTOTFREIGHT As Double
        Dim mEDPERCENT As Double
        Dim mEDUPERCENT As Double
        Dim mSHECPercent As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mIsGSTRefund As String
        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mTotDiscount As Double
        Dim mREJECTION As String
        Dim pDueDate As String
        Dim mModvatNo As Integer
        Dim mSERVNo As Integer
        Dim mSTCLAIMNo As Integer
        Dim mCapital As String
        Dim mNarration As String = ""
        Dim mISMODVAT As String
        Dim mIsServClaim As String
        Dim mISSTREFUND As String
        Dim mISCSTREFUND As String
        Dim mFinalPost As String
        Dim mItemType As String
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim mPreviousRJ As Double
        Dim mAlreadyRejQty As Double
        Dim mDNCNQty As Double
        Dim xDebitAmt As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mPDIRItem As Integer
        Dim PDIRAmount As Double
        Dim mDNCNCreated As Boolean
        Dim xExpDiffDN As Boolean
        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mISFOC As String
        Dim mIsSuppBill As String
        Dim mServTax_Repost As String
        Dim mApproved As String
        Dim mItemCode As String
        Dim mModvatType As Integer
        Dim mISFixAssets As String
        Dim mItemDesc As String
        Dim mModvatAmount As Double
        Dim mLocal As String
        Dim mDivisionCode As Double
        Dim xItemValue As Double
        Dim xTOTEXPAMT As Double

        Dim xNETVALUE As Double

        Dim mFirstRow As Boolean
        Dim mSubRowNo As Integer
        Dim mGSTNo As Double
        Dim mTotGSTAmount As Double
        Dim mShipTo As String
        Dim mShipToCode As String = ""
        Dim mNetExpAmount As Double
        Dim mSaleBillNoPrefix As String
        Dim mSaleBillNoSeq As Double
        Dim mSaleBillNo As String
        Dim mSaleBillDate As String
        Dim mNewGSTNo As Boolean

        Dim mSACCode As String
        Dim mItemCGST As Double
        Dim mItemSGST As Double
        Dim mItemIGST As Double
        'Dim mBookType As String
        Dim mAlias As String
        If SprdPaymentDetail.MaxRows <= 1 Then Exit Sub

        If ADDMode = True Or MODIFYMode = True Then Exit Sub

        'If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then
        'Else
        '    Exit Sub
        'End If

        With SprdPaymentDetail
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColPayBillDate
                If Not IsDate(CDate(.Text)) Then
                    MsgInformation("Invalid Bill Date.")
                    Exit Sub
                End If

            Next
        End With


        mMannualAdjustment = IIf(IsDBNull(RsCompany.Fields("MANNUAL_BILL_ADJUST").Value), "N", RsCompany.Fields("MANNUAL_BILL_ADJUST").Value)
        If mMannualAdjustment = "N" Then Exit Sub

        If Trim(txtPONo.Text) = "" Then
            mSRBillNo = txtBillNo.Text
            mSRBillDate = txtBillDate.Text
        Else
            mSRBillNo = txtBillNo.Text
            mSRBillDate = txtBillDate.Text
        End If

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        mNarration = ""

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                mNarration = mNarration & IIf(mNarration = "", "", ", ") & mItemDesc

            Next
        End With

        mNarration = IIf(mNarration = "", "", IIf(mBookSubType = "J", " ( JobWork of :", " ( Cost of :")) & mNarration & IIf(mNarration = "", "", " )")


        SqlStr = "SELECT * FROM FIN_PURCHASE_HDR WHERE MKEY='" & LblMKey.Text & "' AND PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mRow = SprdPaymentDetail.MaxRows
            mBalanceAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value) '' Val(lblNetAmount.Text)
            mTRNType = IIf(IsDBNull(RsTemp.Fields("TRNTYPE").Value), "-1", RsTemp.Fields("TRNTYPE").Value)
            mSuppCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            mAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE").Value), "-1", RsTemp.Fields("ACCOUNTCODE").Value)
            mIsGSTRefund = IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "G", RsTemp.Fields("ISGSTAPPLICABLE").Value)

            'mBookType = IIf(IsDBNull(RsTemp.Fields("BOOOKTYPE").Value), "", RsTemp.Fields("BOOOKTYPE").Value)
            mBookSubType = IIf(IsDBNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)

            If MainClass.ValidateWithMasterTable(mTRNType, "Code", "Alias", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAlias = MasterNo & "-"
            Else
                mAlias = ""
            End If

            If mBookSubType = "W" Then
                'If chkFOC.CheckState = System.Windows.Forms.CheckState.Checked Then
                '    mNarration = "Bill No : " & txtBillNo.Text & " (Cancelled)"
                'Else
                '    mNarration = "Bill No : " & txtBillNo.Text & mNarration
                'End If
            Else
                'If chkFOC.CheckState = System.Windows.Forms.CheckState.Checked Then
                '    mNarration = "Bill No : " & mAlias & txtBillNo.Text & " (Cancelled)"
                'Else
                mNarration = "Bill No : " & mAlias & txtBillNo.Text & mNarration
                'End If
            End If

            mSubRowNo = 0      'IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "G", RsTemp.Fields("ISGSTAPPLICABLE").Value)
            mVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
            pDueDate = txtPaymentdate.Text
            mLocal = "N"
            mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")

            mItemValue = IIf(IsDBNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value) '' Val(lblNetAmount.Text)

            mDivisionCode = IIf(IsDBNull(RsTemp.Fields("DIV_CODE").Value), "G", RsTemp.Fields("DIV_CODE").Value)
            If UpdatePaymentDetail1(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(lblNetAmount.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), False, pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), mNetExpAmount, IIf(mIsGSTRefund = "G", "Y", "N"), Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), (txtExpDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(mIsGSTRefund = "R", "Y", "N"), Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), mSaleBillNo, mSaleBillDate, txtBillTo.Text) = False Then GoTo ErrPart
        End If

        MsgInformation("Payment Saved.")
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

        Dim I As Integer
        'Dim RsTemp As ADODB.Recordset
        'Dim mShortageQty As Double
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mBillNo As Double
        Dim mReOffer As Double
        Dim mRejQty As Double
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        'Dim mHSNCode As String
        Dim mAccountCode As String
        Dim mColValue As String

        mVNO = Trim(txtPopulateVNo.Text)

        Sqlstr = " Select * From FIN_PURCHASE_HDR WHERE " & vbCrLf _
            & " Vno='" & mVNO & "'" & vbCrLf _
            & " AND Booktype='P'" & vbCrLf _
            & " AND BookCode=" & Val(LblBookCode.Text) & "" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ""

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRNTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTRNTemp.EOF = False Then
            mKey = RsTRNTemp.Fields("mKey").Value
            Clear1()

            Sqlstr = ""
            Sqlstr = " SELECT FIN_PURCHASE_DET.* "
            Sqlstr = Sqlstr & vbCrLf _
                & " FROM FIN_PURCHASE_DET " & vbCrLf _
                & " Where Mkey='" & mKey & "'" & vbCrLf _
                & " Order By SubRowNo"

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTempDetail, ADODB.LockTypeEnum.adLockReadOnly)
            With RSTempDetail
                If .EOF = True Then Exit Sub
                FormatSprdMain(-1)
                I = 1
                .MoveFirst()
                Do While Not .EOF
                    SprdMain.Row = I
                    SprdMain.Col = ColGoodsServs
                    mColValue = IIf(IsDBNull(.Fields("GOODS_SERVICE").Value), "G", .Fields("GOODS_SERVICE").Value)
                    SprdMain.Value = IIf(mColValue = "S", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                    SprdMain.Col = ColItemDesc
                    mItemDesc = IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColHSN
                    mHSNCode = IIf(IsDBNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value)
                    SprdMain.Text = mHSNCode

                    SprdMain.Col = ColCreditApp
                    mColValue = IIf(IsDBNull(.Fields("GST_CREDITAPP").Value), "N", .Fields("GST_CREDITAPP").Value)
                    SprdMain.Value = IIf(mColValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    SprdMain.Col = ColRCApp
                    mColValue = IIf(IsDBNull(.Fields("GST_RCAPP").Value), "N", .Fields("GST_RCAPP").Value)
                    SprdMain.Value = IIf(mColValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    SprdMain.Col = ColExempted
                    mColValue = IIf(IsDBNull(.Fields("GST_EXEMPTED").Value), "N", .Fields("GST_EXEMPTED").Value)
                    SprdMain.Value = IIf(mColValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    SprdMain.Col = ColQty
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                    SprdMain.Col = ColUnit
                    SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                    SprdMain.Col = ColRate
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                    SprdMain.Col = ColAmount
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))

                    SprdMain.Col = ColGSTableAmount
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GSTABLE_AMT").Value), 0, .Fields("GSTABLE_AMT").Value)))

                    SprdMain.Col = ColPONo
                    SprdMain.Text = CStr(IIf(IsDBNull(.Fields("CUST_REF_NO").Value), "", .Fields("CUST_REF_NO").Value))

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                    SprdMain.Col = ColAccountPostCode
                    If MainClass.ValidateWithMasterTable(.Fields("ITEM_TRNTYPE").Value, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    Else
                        SprdMain.Text = ""
                    End If

                    .MoveNext()
                    I = I + 1
                    SprdMain.MaxRows = I
                Loop
            End With
        End If

        '    Call CalcAccountBal			
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
        '    Resume			
    End Sub
End Class
