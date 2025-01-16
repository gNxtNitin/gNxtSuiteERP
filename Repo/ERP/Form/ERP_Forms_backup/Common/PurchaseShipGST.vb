Option Strict Off							
Option Explicit On							
Imports VB = Microsoft.VisualBasic							
Imports Microsoft.VisualBasic.Compatibility							
Friend Class FrmPurchaseShipGST							
Inherits System.Windows.Forms.Form							
Dim RsPurchMain As ADODB.Recordset ''Recordset							
Dim RsPurchDetail As ADODB.Recordset ''Recordset							
Dim RsPurchExp As ADODB.Recordset ''Recordset							
Dim RsPurchPrn As ADODB.Recordset ''Recordset							
    'Private PvtDBCn As ADODB.Connection							
    Dim pProcessKey As Double
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
    ''Private Const mBookSubType = "C"							
    Dim mBookSubType As String
    Private Const ConRowHeight As Short = 12
    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String
    Private Const ColMRRNo As Short = 1
    Private Const ColItemCode As Short = 2
    Private Const ColPartNo As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColHSN As Short = 5
    Private Const ColAcceptedQty As Short = 6
    Private Const ColShortageQty As Short = 7
    Private Const ColRejectedQty As Short = 8
    Private Const ColPORate As Short = 9
    Private Const ColVolDiscRate As Short = 10
    Private Const ColUnit As Short = 11
    Private Const ColQty As Short = 12
    Private Const ColRate As Short = 13
    Private Const ColAmount As Short = 14
    Private Const ColTaxableAmount As Short = 15
    Private Const ColCGSTPer As Short = 16
    Private Const ColCGSTAmount As Short = 17
    Private Const ColSGSTPer As Short = 18
    Private Const ColSGSTAmount As Short = 19
    Private Const ColIGSTPer As Short = 20
    Private Const ColIGSTAmount As Short = 21
    Private Const ColInvType As Short = 22
    Private Const ColPONo As Short = 23
    Private Const ColShowPO As Short = 24
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
    Dim pDnCnNo As String
    Dim mDNCnNO As Integer
    Dim pTempDNCNSeq As Double
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboInvType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim pMKey As String
        If Trim(cboInvType.Text) = "" Then GoTo EventExitSub
        txtDebitAccount.Text = GetDebitNameOfInvType(Trim(cboInvType.Text), "Y")
        If ADDMode = True Then
            Call FillExpFromPartyExp()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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
        Exit Sub
        If Trim(txtSupplier.Text) = "" Then Exit Sub
        If Trim(cboInvType.Text) = "" Then Exit Sub
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = IIf(MasterNo = "Y", "L", "C")
            Else
                mLocal = ""
            End If
        Else
            mLocal = ""
        End If
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
            xTrnCode = MasterNo
        Else
            xTrnCode = CDbl("-1")
        End If
        SqlStr = "Select IH.*, ID.PERCENT,ID.RO FROM " & vbCrLf & " FIN_INTERFACE_MST IH, FIN_PARTY_INTERFACE_MST ID  " & vbCrLf & " Where IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE(+) " & vbCrLf & " AND IH.CODE=ID.EXPCODE(+) " & vbCrLf & " AND ID.SUPP_CUST_CODE='" & xAcctCode & "'" & vbCrLf & " AND ID.TRNTYPE='" & xTrnCode & "'" & vbCrLf & " AND (IH.Type='P' OR IH.Type='B')  " & vbCrLf & " AND ID.CATEGORY='P' ORDER BY IH.PrintSequence"
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
                SprdExp.Value = IIf(mRO = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                SprdExp.Col = ColExpPercent
                SprdExp.Text = Str(IIf(IsDbNull(RS.Fields("PERCENT").Value), 0, Str(RS.Fields("PERCENT").Value)))
                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"
                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDbNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))
                SprdExp.Col = ColExpAddDeduct
                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDbNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)
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
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkCapital_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkCapital.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub
    Private Sub chkCreditRC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCreditRC.CheckStateChanged
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
    End Sub
    Private Sub chkFOC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFOC.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkRejection_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRejection.CheckStateChanged
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
    Private Sub CheckPORate()
        Dim mCntRow As Integer
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mITEM_CODE As String
        Dim mTaxableRate As Double
        Dim mPONo As Double
        Dim mGetREfType As String
        Dim mExchangeRate As Double
        Dim pMRRNo As Double
        With SprdMain
            For mCntRow = 1 To .MaxRows - 1
                .Row = mCntRow
                .Col = ColMRRNo
                pMRRNo = Val(.Text)
                mGetREfType = GetMrrRefNo(Val(CStr(pMRRNo)))
                .Col = ColPONo
                mPONo = Val(.Text)
                .Col = ColItemCode
                mITEM_CODE = Trim(.Text)
                If mGetREfType = "I" Or mGetREfType = "2" Then
                    If MainClass.ValidateWithMasterTable(pMRRNo, "AUTO_KEY_MRR", "REF_AUTO_KEY_NO", "INV_GATE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPONo = MasterNo
                    Else
                        mPONo = CDbl("-1")
                    End If
                    SqlStr = "SELECT GetSALEITEMPRICE(" & Val(CStr(mPONo)) & ",'','','" & mITEM_CODE & "') AS PORATE, 0 as VOL_DISCRATE  FROM DUAL"
                ElseIf mGetREfType = "P" Then
                    SqlStr = "SELECT GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(CStr(mPONo)) & ",'" & mITEM_CODE & "') AS PORATE, " & vbCrLf & " GetVOL_DISC_ITEM(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(CStr(mPONo)) & ", '" & mITEM_CODE & "') AS VOL_DISCRATE FROM DUAL"
                ElseIf mGetREfType = "R" Then
                    SqlStr = "SELECT GetITEMJWRate(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))," & Val(CStr(mPONo)) & "," & Val(CStr(pMRRNo)) & ",'" & mITEM_CODE & "'," & mCntRow & ") AS PORATE, 0 as VOL_DISCRATE  FROM DUAL"
                Else
                    SqlStr = "SELECT 0 AS PORATE, 0 as VOL_DISCRATE  FROM DUAL"
                End If
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "PORATEEDITABLE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        If MasterNo = "N" Then
                            '                        If mGetREfType = "P" Then							
                            '                            mExchangeRate = GetExchangeRate(mPONo)							
                            '                        Else							
                            '                            mExchangeRate = 1							
                            '                        End If							
                            .Col = ColPORate
                            .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("PORATE").Value), 0, RsTemp.Fields("PORATE").Value))) ''* mExchangeRate							
                            .Col = ColVolDiscRate
                            .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("VOL_DISCRATE").Value), 0, RsTemp.Fields("VOL_DISCRATE").Value))) ''* mExchangeRate							
                        End If
                    End If
                End If
            Next
        End With
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
            If Val(txtTdsRate.Text) = 0 Then
                SqlStr = "SELECT TDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtTdsRate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDS_PER").Value), 0, RsTemp.Fields("TDS_PER").Value), "0.000")
                End If
            End If
        Else
            txtTDSDeductOn.Enabled = False
            txtTdsRate.Enabled = False
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
            txtVno.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            txtModvatNo.Enabled = False
            txtServNo.Enabled = False
            txtMRRNo.Enabled = True
            CmdSearchMRR.Enabled = True
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If Val(LblBookCode.Text) = ConModvatBookCode Then
                    cboInvType.Enabled = False
                Else
                    cboInvType.Enabled = True
                End If
            Else
                cboInvType.Enabled = False
            End If
            If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
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
        Dim cntRow As Integer
        Dim mMRRNO As Double
        Exit Sub
        '    If CheckVoucherDateLock(txtVDate.Text, pMaxDate) = True Then							
        '         MsgInformation "Working Company Been Locked till Date : " & pMaxDate & vbCrLf _							
        ''                    & "So Unable to Save or Delete. Contact your system administrator."							
        '        FieldsVarification = False							
        '        Exit Function							
        '    End If							
        If chkCreditRC.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Final Credit is Done, So that cann't be Deleted.", MsgBoxStyle.Information)
            Exit Sub
        End If
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            Exit Sub
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            mLockBookCode = CInt(ConLockModvat)
            If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
                Exit Sub
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "Y" Then
            mLockBookCode = CInt(ConLockPurchase)
            If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
                Exit Sub
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "N" Then
            mLockBookCode = CInt(ConLockModvat)
            If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
                Exit Sub
            End If
            mLockBookCode = CInt(ConLockPurchase)
            If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
                Exit Sub
            End If
        End If
        If ValidateAccountLocking(PubDBCn, TxtVDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If
        If Trim(txtBillNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If MainClass.GetUserCanModify((TxtVDate.Text)) = False Then
                MsgBox("You Have Not Rights to delete back Voucher", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Final Bill Post Cann't be Deleted")
                Exit Sub
            End If
        End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Deleted.")
            Exit Sub
        End If
        If CheckBillPayment(mSupplierCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub
        If CheckDebitNoteExsits(Val(txtMRRNo.Text)) = True Then Exit Sub
        If Not RsPurchMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.							
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_HDR", (LblMKey.Text), RsPurchMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_DET", (LblMKey.Text), RsPurchDetail, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_EXP", (LblMKey.Text), RsPurchExp, "MKEY", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "FIN_PURCHASE_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("Delete from FIN_PURCHASE_VNO_MST Where Mkey='" & LblMKey.Text & "'")
                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColMRRNo
                    mMRRNO = Val(SprdMain.Text)
                    If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                        SqlStr = "UPDATE INV_GATE_HDR SET GST_STATUS='N',"
                    Else
                        SqlStr = "UPDATE INV_GATE_HDR SET MRR_FINAL_FLAG='N',"
                    End If
                    SqlStr = SqlStr & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf
                    SqlStr = SqlStr & vbCrLf & " WHERE AUTO_KEY_MRR=" & Val(CStr(mMRRNO)) & " " & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
                    PubDBCn.Execute(SqlStr)
                Next
                If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                    PubDBCn.Execute("DELETE FROM FIN_PURCHASE_TRN WHERE MKey='" & LblMKey.Text & "' AND BookCode=" & ConPurchaseBookCode & "")
                    PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'")
                    PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & ConPurchaseBookCode & "'")
                    PubDBCn.Execute("Delete from FIN_PURCHASE_EXP Where Mkey='" & LblMKey.Text & "'")
                    PubDBCn.Execute("Delete from FIN_PURCHASE_DET Where Mkey='" & LblMKey.Text & "'")
                    PubDBCn.Execute("Delete from FIN_PURCHASE_VNO_MST Where Mkey='" & LblMKey.Text & "'")
                    PubDBCn.Execute("DELETE FROM FIN_PURCHASE_HDR WHERE MKey='" & LblMKey.Text & "' ")
                    PubDBCn.Execute("DELETE FROM FIN_GST_SEQ_MST " & vbCrLf & " WHERE MKEY= '" & LblMKey.Text & "'" & vbCrLf & " AND COMPANY_CODE = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCODE = '" & LblBookCode.Text & "'" & vbCrLf & " AND BOOKTYPE = '" & mBookType & "'")
                ElseIf CDbl(LblBookCode.Text) = ConModvatBookCode Then
                    '                If chkGSTRefund.Value = vbChecked And chkFinalPost.Value = vbUnchecked Then							
                    '                    PubDBCn.Execute "Delete From FIN_GST_POST_TRN Where Mkey='" & lblMKey.text & "' AND BookType='" & UCase(mBookType) & "'"							
                    '                    PubDBCn.Execute "Delete from FIN_PURCHASE_EXP Where Mkey='" & lblMKey.text & "'"							
                    '                    PubDBCn.Execute "Delete from FIN_PURCHASE_DET Where Mkey='" & lblMKey.text & "'"							
                    '                    PubDBCn.Execute "Delete from FIN_PURCHASE_TRN Where Mkey='" & lblMKey.text & "'"							
                    '                    PubDBCn.Execute "Delete from FIN_PURCHASE_VNO_MST Where Mkey='" & lblMKey.text & "'"							
                    '                    PubDBCn.Execute "DELETE FROM FIN_PURCHASE_HDR WHERE MKey='" & lblMKey.text & "' "							
                    '							
                    '                    PubDBCn.Execute "DELETE FROM FIN_GST_SEQ_MST " & vbCrLf _							
                    ''                        & " WHERE MKEY= '" & lblMKey.text & "'" & vbCrLf _							
                    ''                        & " AND COMPANY_CODE = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _							
                    ''                        & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _							
                    ''                        & " AND BOOKCODE = '" & LblBookCode.text & "'" & vbCrLf _							
                    ''                        & " AND BOOKTYPE = '" & mBookType & "'"							
                    '							
                    '                End If							
                End If
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
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Final Bill Post Cann't be Modified")
                Exit Sub
            End If
        End If
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtMRRNo.Enabled = False
            CmdSearchMRR.Enabled = False
            txtMRRDate.Enabled = False
            txtVNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            txtModvatNo.Enabled = False
            txtServNo.Enabled = False
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
                    SprdPostingDetail.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    SprdPostingDetail.Col = 2
                    SprdPostingDetail.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), "0.00", RsTemp.Fields("Amount").Value), "0.00")
                    SprdPostingDetail.Col = 3
                    SprdPostingDetail.Text = IIf(IsDBNull(RsTemp.Fields("DC").Value), "", RsTemp.Fields("DC").Value)
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
        mTitle = "Purchase Voucher"
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
        If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
            txtDebitAccount.Text = GetDebitNameOfInvType(Trim(cboInvType.Text), "Y")
        End If
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        Call CalcTots()
        pDnCnNo = ""
        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            ''TxtVNo_Validate False							
            If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                '            TxtMODVATNo_Validate False							
            ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
            End If
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
    Private Sub CmdSearchMRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchMRR.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_FINAL_FLAG='N' " & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)>=2016" '& vbCrLf |            & " AND MRR_FINAL_FLAG='N'"							
        If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE='R'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE<>'R'"
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            SqlStr = SqlStr & vbCrLf & " AND GST_STATUS='N'"
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            SqlStr = SqlStr & vbCrLf & " AND SEND_AC_FLAG='Y'"
        End If
        If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_GATE_HDR", "AUTO_KEY_MRR", "MRR_DATE", , , SqlStr) = True Then
            txtMRRNo.Text = AcName
            txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
            If cboInvType.Enabled = True Then cboInvType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        Dim mPONo As String
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColPONo
        mPONo = SprdMain.Text
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPO(Crystal.DestinationConstants.crptToWindow, mPONo)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim xIName As String
        Dim SqlStr As String
        If eventArgs.row = 0 And eventArgs.col = ColInvType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColInvType
                If MainClass.SearchGridMaster(.Text, "FIN_INVTYPE_MST", "NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    .Row = .ActiveRow
                    .Col = ColInvType
                    .Text = AcName
                    '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColInvType							
                End If
            End With
        End If
        Exit Sub
        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                '            If mainclass.SearchMaster(.Text, "vwITEM", "ITEMCODE", SqlStr) = True Then							
                '                .Row = .ActiveRow							
                '                .Col = ColItemCode							
                '                .Text = AcName							
                '            End If							
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If
        'If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
        '    With SprdMain
        '        .Row = .ActiveRow
        '        .Col = ColItemDesc
        '        xIName = .Text
        '        .Text = ""
        '        '            If mainclass.SearchMaster(.Text, "vwITEM", "Name", SqlStr) = True Then							
        '        '                .Row = .ActiveRow							
        '        '                .Col = ColItemDesc							
        '        '                .Text = AcName							
        '        '            Else							
        '        '                .Row = .ActiveRow							
        '        '                .Col = ColItemDesc							
        '        '                .Text = xIName							
        '        '            End If							
        '        MainClass.ValidateWithMasterTable(.Text, "Name", "ItemCode", "Item", PubDBCn, MasterNo)
        '        .Row = .ActiveRow
        '        .Col = ColItemCode
        '        .Text = MasterNo
        '        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
        '    End With
        'End If
        '    If Col = 0 And Row > 0 Then    '***ROW DEL. OPTION NOT REQ IN INVOICE							
        '        SprdMain.Row = Row							
        '        SprdMain.Col = ColSONo							
        '        If Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then							
        '            mainclass.DeleteSprdRow SprdMain, Row, ColSONo							
        '            mainclass.SaveStatus Me, ADDMode, MODIFYMode							
        '            FormatSprdMain Row							
        ''            Call DistributeExpInMainGrid							
        ''            Call CalcTots							
        '        End If							
        '    End If							
        Call CalcTots()
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim RIBBONSGroup As Boolean
        Dim xSoNo As String
        Dim xICode As String
        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain.Row = SprdMain.ActiveRow
        Select Case eventArgs.col
            Case ColQty
                If CheckQty() = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColRate
                Call CheckRate()
            Case ColInvType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColInvType
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgBox("Invoice Name Does Not Exist In Master", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColInvType)
                        eventArgs.cancel = True
                    End If
                End If
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckRate()
        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
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
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function
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
    Private Sub SprdMain_TextTipFetch(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles SprdMain.TextTipFetch
        If eventArgs.row = 0 Then Exit Sub
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColItemDesc
        eventArgs.tipText = SprdMain.Text
        eventArgs.showTip = True
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            If eventArgs.row = 0 Then Exit Sub
            .Row = eventArgs.row
            .Col = 1
            If Trim(.Text) = "" Then
                cboInvType.SelectedIndex = -1
            Else
                cboInvType.Text = Trim(.Text)
            End If
            .Col = 2
            txtVNoPrefix.Text = .Text
            .Col = 3
            txtVNo.Text = VB6.Format(.Text, "00000")
            .Col = 4
            txtVNoSuffix.Text = .Text
            .Col = 6
            txtVDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
            .Col = 7
            txtModvatNo.Text = VB6.Format(.Text, "00000")
            .Col = 8
            txtModvatDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
            .Col = 21
            ChkCapital.CheckState = IIf(VB.Left(.Text, 1) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                '            TxtMODVATNo_Validate False							
            ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
            End If
            CmdView_Click(CmdView, New System.EventArgs())
        End With
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
        If Val(CStr(Val(txtVNo.Text))) > 0 Then
            mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVNo.Text), "00000") & Trim(txtVNoSuffix.Text))
        End If
        SqlStr = " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE FROM ("
        SqlStr = SqlStr & vbCrLf & " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE " & vbCrLf & " FROM FIN_ADVANCE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "' AND BOOKTYPE='AP'" & vbCrLf & " AND DIV_CODE = " & mDivisionCode & "" & vbCrLf & " AND VDATE <= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " GROUP BY VNO, VDATE"
        SqlStr = SqlStr & vbCrLf & " UNION "
        SqlStr = SqlStr & vbCrLf & " SELECT ADV_VNO AS VNO, ADV_VDATE AS VDATE, SUM(ADV_ADJUSTED_AMT*-1) AS ADV_ADJUSTED_AMT " & vbCrLf & " FROM FIN_PURCHASE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "'" & vbCrLf & " AND DIV_CODE = " & mDivisionCode & "" & vbCrLf & " AND VDATE <= TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If mVNo <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR || VNO <> " & RsCompany.Fields("FYEAR").Value & " || '" & mVNo & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " GROUP BY ADV_VNO, ADV_VDATE HAVING SUM(ADV_ADJUSTED_AMT)<>0"
        SqlStr = SqlStr & vbCrLf & ") GROUP BY VNO, VDATE HAVING SUM(NETVALUE)>0"
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtAdvVNo.Text = AcName
            txtAdvVNo_Validating(txtAdvVNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
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
        If Trim(txtPaymentdate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPaymentdate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtServDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtServDate.Text) = "" Then
            MsgBox("Service Tax Claim Date Cann't be Blank", MsgBoxStyle.Information)
            GoTo EventExitSub
        End If
        If Not IsDate(txtServDate.Text) Then
            MsgBox("Invalid Modvat Date", MsgBoxStyle.Information)
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
    Private Sub txtServNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMKey As String
        Dim mSERVNo As String
        Dim mCapital As String
        Dim SqlStr As String
        If Val(txtServNo.Text) = 0 Then GoTo EventExitSub
        txtServNo.Text = VB6.Format(Val(txtServNo.Text), "00000")
        mCapital = IIf(ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If MODIFYMode = True And RsPurchMain.EOF = False Then xMKey = RsPurchMain.Fields("mKey").Value
        mSERVNo = Trim(txtServNo.Text)
        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND SERVNO='" & MainClass.AllowSingleQuote(mSERVNo) & "' AND ISPLA='N' AND ISSERVTAX_POST='N'"
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
        Dim mLocal As String
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim cntRow As Integer
        Dim mPartyGSTNo As String
        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub
        mLocal = "N"
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = Trim(MasterNo)
            End If
        End If
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        txtProviderPer.Text = "0.00"
        txtRecipientPer.Text = "0.00"
        SqlStr = " SELECT HSN_CODE, HSN_DESC, CGST_PER, SGST_PER, IGST_PER" & vbCrLf & " REVERSE_CHARGE_APP, GST_APP" & vbCrLf & " FROM GEN_HSN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HSN_DESC='" & MainClass.AllowSingleQuote((txtServProvided.Text)) & "' AND CODETYPE='S'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mReverseChargeApp = IIf(IsDBNull(RsTemp.Fields("REVERSE_CHARGE_APP").Value), "N", RsTemp.Fields("REVERSE_CHARGE_APP").Value)
            mHSNCode = IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value)
            pCGSTPer = 0
            pSGSTPer = 0
            pIGSTPer = 0
            If lblPurchaseType.Text <> "G" Then
                If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ERR1
                For cntRow = 1 To SprdMain.MaxRows - 1
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColHSN
                    SprdMain.Text = mHSNCode
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")
                Next
                CalcTots()
            End If
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
    Private Sub txtTariff_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.DoubleClick
        SearchTariff()
    End Sub
    Private Sub txtTariff_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTariff.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    End Sub
    Private Sub txtTariff_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTariff.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        If Trim(txtTariff.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable((txtTariff.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Tariff.", "", MsgBoxStyle.Critical)
            Cancel = True
        Else
            txtItemType.Text = MasterNo
        End If
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
        txtTDSRate.Text = VB6.Format(txtTDSRate.Text, "0.000")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk((txtVDate.Text)) = False Then
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
        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub
        txtVNo.Text = VB6.Format(Val(txtVNo.Text), "00000")
        If MODIFYMode = True And RsPurchMain.EOF = False Then xMKey = RsPurchMain.Fields("mKey").Value
        mVNo = Trim(Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text) & Trim(txtVNoSuffix.Text))
        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND VNo='" & MainClass.AllowSingleQuote(mVNo) & "' " & vbCrLf & " AND BookCode=" & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' "
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
        Dim mStartingNo As Integer
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
        Dim mServiceCode As String
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
        Dim mNetExpAmount As Double
        Dim mSaleBillNoPrefix As String
        Dim mSaleBillNoSeq As Double
        Dim mSaleBillNo As String
        Dim mSaleBillDate As String
        Dim mNewGSTNo As Boolean
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mSACCode As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mNewGSTNo = False
        '    txtMRRDate.Text = txtVDate.Text							
        If ADDMode = True And chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked And chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked And CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            pTempDNCNSeq = MainClass.AutoGenRowNo("TEMP_FIN_DNCN_DET", "RowNo", PubDBCn)
            If UpdateTempDNCNTable(pTempDNCNSeq) = False Then GoTo ErrPart
        End If
        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        mFormRecdCode = -1
        mFormDueCode = -1
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mTRNType = MasterNo
            Else
                mTRNType = CStr(-1)
            End If
            '        Left(cboGSTStatus.Text, 1)="G"							
        Else
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTRNType = MasterNo
                Else
                    mTRNType = CStr(-1)
                    MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
                    GoTo ErrPart
                End If
            Else
                mTRNType = CStr(-1)
            End If
        End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            mFinalPost = "Y"
            chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked
        Else
            mFinalPost = "N"
            chkFinalPost.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If
        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLocal = Trim(MasterNo)
        Else
            mLocal = "N"
        End If
        '*********							
        mModvatSuppCode = CStr(-1)
        '*************							
        '    If LblBookCode.text = ConModvatBookCode Or LblBookCode.text = ConServiceClaimBookCode Or LblBookCode.text = ConSTClaimBookCode Then							
        If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
            If Trim(txtDebitAccount.Text) = "" Then
                mAccountCode = "-1"
            Else
                If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                Else
                    mAccountCode = "-1"
                    MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
                    GoTo ErrPart
                End If
            End If
        Else
            mAccountCode = "-1"
        End If
        mFREIGHTCHARGES = IIf(OptFreight(0).Checked = True, "To Pay", "Paid")
        mEXEMPT_NOTIF_NO = ""
        mBookCode = CInt(LblBookCode.Text)
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            mBookSubType = "E"
        Else
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mBookSubType = MasterNo
                Else
                    mBookSubType = CStr(-1)
                End If
            Else
                If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mBookSubType = "R"
                Else
                    mBookSubType = "E"
                End If
            End If
        End If
        mItemValue = Val(lblTotItemValue.Text)
        mTOTSTAMT = 0
        mTOTCHARGES = Val(lblTotCharges.Text)
        mTotEDAmount = 0
        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)
        mSTPERCENT = Val(lblSTPercentage.Text)
        mTOTFREIGHT = Val(lblTotFreight.Text)
        mEDPERCENT = Val(lblEDPercentage.Text)
        mEDUPERCENT = Val(lblEDUPercent.Text)
        mSHECPercent = 0
        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)
        mRO = Val(lblRO.Text)
        mTotDiscount = Val(lblDiscount.Text)
        mSURAmount = Val(lblSurcharge.Text)
        mMSC = Val(lblMSC.Text)
        mTotQty = Val(lblTotQty.Text)
        mLSTCST = ""
        mWITHFORM = ""
        mPRINTED = "N"
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsRegdNo = "N"
        mREJECTION = IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCapital = IIf(ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
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
        mISFOC = IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsSuppBill = "N"
        mSTType = "0"
        mTotGSTAmount = Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text)
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                mStartingNo = 1
                If Trim(lblGSTClaimNo.Text) = "" Or Val(lblGSTClaimNo.Text) = 0 Then
                    mGSTNo = CDbl(AutoGenSeqGSTNo())
                    mNewGSTNo = True
                Else
                    mGSTNo = Val(lblGSTClaimNo.Text)
                End If
            End If
        End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "Y" Then
            mGSTNo = Val(lblGSTClaimNo.Text)
            If Trim(txtVNo.Text) = "" Then
                If RsCompany.Fields("FYEAR").Value >= 2018 Then
                    mVNoSeq = CDbl(AutoGenSeqBillNoNew("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode))
                Else
                    mVNoSeq = CDbl(AutoGenSeqBillNo("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode))
                End If
                '            mVNoSeq = AutoGenSeqBillNo("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode)							
            Else
                mVNoSeq = Val(txtVNo.Text)
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "N" Then
            If VB.Left(cboGSTStatus.Text, 1) = "G" And (Val(txtTotCGSTRefund.Text) + Val(txtTotSGSTRefund.Text) + Val(txtTotIGSTRefund.Text) > 0) Then
                mStartingNo = 1
                If Trim(lblGSTClaimNo.Text) = "" Or Val(lblGSTClaimNo.Text) = 0 Then
                    mGSTNo = CDbl(AutoGenSeqGSTNo())
                    mNewGSTNo = True
                Else
                    mGSTNo = Val(lblGSTClaimNo.Text)
                End If
            End If
            If Trim(txtVNo.Text) = "" Then
                If RsCompany.Fields("FYEAR").Value >= 2018 Then
                    mVNoSeq = CDbl(AutoGenSeqBillNoNew("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode))
                Else
                    mVNoSeq = CDbl(AutoGenSeqBillNo("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode))
                End If
            Else
                mVNoSeq = Val(txtVNo.Text)
            End If
        End If
        mModvatNo = 0
        txtVNo.Text = IIf(mVNoSeq = -1 Or mVNoSeq = 0, "", VB6.Format(Val(CStr(mVNoSeq)), "00000"))
        lblGSTClaimNo.Text = VB6.Format(Val(CStr(mGSTNo)), "00000")
        txtServNo.Text = VB6.Format(Val(CStr(mSERVNo)), "00000")
        '    If CheckValidVDate(mVNoSeq) = False Then GoTo ErrPart:							
        txtNarration.Text = GetNarration()

        If Trim(txtVNo.Text) = "" Then
            MsgInformation("Please Check Voucher No.")
            GoTo ErrPart
        End If
        If mVNoSeq = -1 Or mVNoSeq = 0 Then
            mVNo = "-1"
        Else
            mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "00000") & Trim(txtVNoSuffix.Text))
        End If
        SqlStr = ""
        If VB.Left(cboGSTStatus.Text, 1) = "G" Then
            mModvatType = 1
        Else
            mModvatType = 0
        End If
        If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mSACCode = Trim(MasterNo)
        Else
            mSACCode = ""
        End If
        mShipTo = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If mShipTo = "Y" Then
            mShipToCode = mSuppCustCode
        Else
            If MainClass.ValidateWithMasterTable(txtShippedTo.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShipToCode = MasterNo
            End If
        End If
        If VB.Left(cboGSTStatus.Text, 1) = "R" Then
            '        If ADDMode = True Then							
            '            mSaleBillNoPrefix = "S"							
            '            mSaleBillNoSeq = AutoGenSeqSaleBillNo(lblPurchaseType.text)							
            '            mSaleBillNo = mSaleBillNoPrefix & vb6.Format(mSaleBillNoSeq, "00000000")							
            '            mSaleBillDate = Format(TxtVDate.Text, "DD/MM/YYYY")							
            '        Else							
            mSaleBillNoPrefix = "S"
            mSaleBillNoSeq = Val(lblSaleBillNoSeq.Text)
            mSaleBillNo = lblSaleBillNo.Text
            mSaleBillDate = VB6.Format(lblSaleBillDate.Text, "DD/MM/YYYY")
            '        End If							
        Else
            mSaleBillNoPrefix = ""
            mSaleBillNoSeq = 0
            mSaleBillNo = ""
            mSaleBillDate = ""
        End If
        mServiceCode = CStr(-1)

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
            SqlStr = SqlStr & vbCrLf & " SHECMODVATPER,SHECMODVATAMOUNT, SHECPERCENT, SHECAMOUNT, " & vbCrLf & " ADEMODVATPER,ADEMODVATAMOUNT, ADEAMOUNT,UPDATE_FROM,MODVAT_TYPE,SUR_VATCLAIMAMOUNT,DIV_CODE," & vbCrLf & " SAC_CODE, SERVICE_ON_AMT, SERV_PROVIDER_PER, " & vbCrLf & " SERV_RECIPIENT_PER,SERVICE_TAX_PER,SERVICE_TAX_AMOUNT,KK_CESS_PER,KK_CESS_AMOUNT, " & vbCrLf & " ISGSTAPPLICABLE, GST_CLAIM_NO, GST_CLAIM_DATE, " & vbCrLf & " TOTALGSTVALUE, TOTCGST_REFUNDAMT, TOTSGST_REFUNDAMT, " & vbCrLf & " TOTIGST_REFUNDAMT, TOTCGST_AMOUNT, TOTSGST_AMOUNT, " & vbCrLf & " TOTIGST_AMOUNT, SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE, " & vbCrLf & " PURCHASE_TYPE, " & vbCrLf & " ADV_VNO, ADV_VDATE, ADV_ADJUSTED_AMT, " & vbCrLf & " ADV_CGST_AMT, ADV_SGST_AMT, ADV_IGST_AMT,ADV_ITEM_AMT,PURCHASESEQTYPE " & vbCrLf & " )"
            SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & mCurRowNo & ", " & vbCrLf & " " & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "', " & vbCrLf & " " & mVNoSeq & ", '" & MainClass.AllowSingleQuote(txtVNoSuffix.Text) & "', '" & MainClass.AllowSingleQuote(mVNo) & "',TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "',TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(txtMRRNo.Text) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPONo.Text) & "',TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mSuppCustCode & "', '" & mModvatSuppCode & "', '" & mAccountCode & "','', " & vbCrLf & " " & Val(txtCreditDays(0).Text) & ", " & Val(txtCreditDays(1).Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtMode.Text) & "', '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "', '" & mFREIGHTCHARGES & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtTariff.Text) & "', '" & mEXEMPT_NOTIF_NO & "', " & vbCrLf & " '" & mBookCode & "', '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & MainClass.AllowSingleQuote(txtItemType.Text) & "', " & mItemValue & ", " & vbCrLf & " " & mTOTSTAMT & ", " & mTOTCHARGES & ", " & mTotEDAmount & ", " & vbCrLf & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf & " '" & mSTType & "'," & mFormRecdCode & ",'','', '', " & vbCrLf & " " & mFormDueCode & ",'','', '', " & vbCrLf & " '" & mIsRegdNo & "', '" & mLSTCST & "', " & vbCrLf & " '" & mWITHFORM & "', " & vbCrLf & " '" & mCancelled & "', '" & mREJECTION & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "',  " & vbCrLf & "" & mSTPERCENT & "," & mTOTFREIGHT & "," & mEDPERCENT & "," & mTOTTAXABLEAMOUNT & "," & vbCrLf & "" & mSURAmount & "," & mTotDiscount & "," & mMSC & "," & mRO & ", "
            SqlStr = SqlStr & vbCrLf & " '" & mModvatNo & "', TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 0,0, " & vbCrLf & " '" & mSTCLAIMNo & "','',0,0, '" & mCapital & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf & " '" & mISMODVAT & "','" & mISSTREFUND & "','" & mISCSTREFUND & "', '" & mFinalPost & "'," & vbCrLf & " '" & mISTDSDEDUCT & "'," & Val(txtTDSRate.Text) & ", " & Val(txtTDSAmount.Text) & ", " & vbCrLf & " '" & mISESIDEDUCT & "'," & Val(txtESIRate.Text) & ", " & Val(txtESIAmount.Text) & ", " & vbCrLf & " '" & mISSTDSDEDUCT & "'," & Val(txtSTDSRate.Text) & ", " & Val(txtSTDSAmount.Text) & ", " & vbCrLf & " '" & Val(txtTDSDeductOn.Text) & "'," & Val(txtSTDSDeductOn.Text) & ", " & Val(txtESIDeductOn.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " '','',0," & Val(lblEDUPercent.Text) & ",0," & Val(lblCESSableAmount.Text) & ",0,0, " & vbCrLf & " '" & mISFOC & "','" & mIsSuppBill & "',"
            SqlStr = SqlStr & vbCrLf & " " & Val(lblServicePercentage.Text) & "," & vbCrLf & " 0," & vbCrLf & " '" & mSERVNo & "', TO_DATE('" & VB6.Format(txtServDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mIsServClaim & "', " & vbCrLf & " 0, 0," & vbCrLf & " '" & mServTax_Repost & "','" & MainClass.AllowSingleQuote(txtServProvided.Text) & "',"
            SqlStr = SqlStr & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, 'N','" & mModvatType & "',0," & mDivisionCode & "," & vbCrLf & " '" & mSACCode & "', " & Val(txtServiceOn.Text) & ", " & Val(txtProviderPer.Text) & ", " & Val(txtRecipientPer.Text) & ", " & vbCrLf & " " & Val(txtServiceTaxPer.Text) & "," & Val(txtServiceTaxAmount.Text) & ",0,0, " & vbCrLf & " '" & mIsGSTRefund & "', " & Val(CStr(mGSTNo)) & ", TO_DATE('" & VB6.Format(lblGSTClaimDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(CStr(mTotGSTAmount)) & ", " & Val(txtTotCGSTRefund.Text) & ", " & Val(txtTotSGSTRefund.Text) & ", " & vbCrLf & " " & Val(txtTotIGSTRefund.Text) & ", " & Val(lblTotCGSTAmount.Text) & ", " & Val(lblTotSGSTAmount.Text) & "," & vbCrLf & " " & Val(lblTotIGSTAmount.Text) & ",'" & mShipTo & "', '" & mShipToCode & "', " & vbCrLf & " '" & lblPurchaseType.Text & "'," & vbCrLf & " '" & Trim(txtAdvVNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtAdvAdjust.Text) & ", " & vbCrLf & " " & Val(txtAdvCGST.Text) & ", " & Val(txtAdvSGST.Text) & ", " & Val(txtAdvIGST.Text) & ", " & Val(txtItemAdvAdjust.Text) & "," & Val(lblPurchaseSeqType.Text) & " " & vbCrLf & " )"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET " & vbCrLf & " VNOPREFIX = '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "'," & vbCrLf & " VNOSEQ= " & mVNoSeq & ", TRNTYPE=" & Val(mTRNType) & "," & vbCrLf & " VNOSUFFIX= '" & MainClass.AllowSingleQuote(txtVNoSuffix.Text) & "'," & vbCrLf & " VNO= '" & MainClass.AllowSingleQuote(mVNo) & "'," & vbCrLf & " VDATE= TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " BILLNO= '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf & " INVOICE_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " AUTO_KEY_MRR= " & Val(txtMRRNo.Text) & "," & vbCrLf & " MRRDATE= TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CUSTREFNO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf & " CUSTREFDATE= TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " MODVAT_SUPP_CODE= '" & mModvatSuppCode & "'," & vbCrLf & " ACCOUNTCODE= '" & mAccountCode & "'," & vbCrLf & " ST_38_NO= '', "
            SqlStr = SqlStr & vbCrLf & " DUEDAYSFROM= " & Val(txtCreditDays(0).Text) & "," & vbCrLf & " DUEDAYSTO= " & Val(txtCreditDays(1).Text) & ", " & vbCrLf & " DESPATCHMODE= '" & MainClass.AllowSingleQuote(txtMode.Text) & "', " & vbCrLf & " DOCSTHROUGH= '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "'," & vbCrLf & " VEHICLENO= '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " CARRIERS=  '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "'," & vbCrLf & " FREIGHTCHARGES= '" & mFREIGHTCHARGES & "', " & vbCrLf & " TARIFFHEADING= '" & MainClass.AllowSingleQuote(txtTariff.Text) & "'," & vbCrLf & " EXEMPT_NOTIF_NO= '" & MainClass.AllowSingleQuote(mEXEMPT_NOTIF_NO) & "',"
            SqlStr = SqlStr & vbCrLf & " BOOKCODE= " & mBookCode & "," & vbCrLf & " BOOKTYPE= '" & mBookType & "'," & vbCrLf & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " ITEMDESC= '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & "," & vbCrLf & " TOTSTAMT= " & mTOTSTAMT & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & "," & vbCrLf & " TOTEDAMOUNT= " & mTotEDAmount & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " NETVALUE= " & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & "," & vbCrLf & " STTYPE= '" & mSTType & "'," & vbCrLf & " REJECTION='" & mREJECTION & "'," & vbCrLf & " STFORMCODE= " & mFormRecdCode & "," & vbCrLf & " STFORMNAME= ''," & vbCrLf & " STFORMNO= ''," & vbCrLf & " STFORMDATE='', " & vbCrLf & " STDUEFORMCODE= " & mFormDueCode & "," & vbCrLf & " STDUEFORMNAME= ''," & vbCrLf & " STDUEFORMNO= ''," & vbCrLf & " STDUEFORMDATE= '',"
            SqlStr = SqlStr & vbCrLf & " STPERCENT=" & mSTPERCENT & "," & vbCrLf & " TOTFREIGHT=" & mTOTFREIGHT & "," & vbCrLf & " EDPERCENT=" & mEDPERCENT & "," & vbCrLf & " TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & "," & vbCrLf & " ISREGDNO= '" & mIsRegdNo & "'," & vbCrLf & " LSTCST= '" & mLSTCST & "', " & vbCrLf & " WITHFORM= '" & mWITHFORM & "'," & vbCrLf & " CANCELLED= '" & mCancelled & "'," & vbCrLf & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "'," & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & mTotDiscount & ", " & vbCrLf & " TOTMSCAMOUNT=" & mMSC & ", TotRO=" & mRO & "," & vbCrLf & " MODVATNO='" & mModvatNo & "', " & vbCrLf & " MODVATDATE=TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODVATPER=0, " & vbCrLf & " MODVATAMOUNT=0, " & vbCrLf & " TOTEDUPERCENT=" & Val(lblEDUPercent.Text) & ", " & vbCrLf & " TOTEDUAMOUNT=0, " & vbCrLf & " CESSABLEAMOUNT=" & Val(lblCESSableAmount.Text) & "," & vbCrLf & " CESSPER=0, " & vbCrLf & " CESSAMOUNT=0, " & vbCrLf & " TDS_DEDUCT_ON=" & Val(txtTDSDeductOn.Text) & ", " & vbCrLf & " ISTDSDEDUCT='" & mISTDSDEDUCT & "'," & vbCrLf & " TDSPER=" & Val(txtTDSRate.Text) & ", TDSAMOUNT=" & Val(txtTDSAmount.Text) & ", "
            SqlStr = SqlStr & vbCrLf & " MODVATItemValue=0," & vbCrLf & " ESI_DEDUCT_ON=" & Val(txtESIDeductOn.Text) & ", " & vbCrLf & " ISESIDEDUCT='" & mISESIDEDUCT & "'," & vbCrLf & " ESIPER=" & Val(txtESIRate.Text) & ", " & vbCrLf & " ESIAMOUNT=" & Val(txtESIAmount.Text) & ", " & vbCrLf & " STDS_DEDUCT_ON=" & Val(txtSTDSDeductOn.Text) & ", " & vbCrLf & " ISSTDSDEDUCT='" & mISSTDSDEDUCT & "'," & vbCrLf & " STDSPER=" & Val(txtSTDSRate.Text) & ", " & vbCrLf & " STDSAMOUNT=" & Val(txtSTDSAmount.Text) & ", " & vbCrLf & " STCLAIMNO='" & mSTCLAIMNo & "', " & vbCrLf & " STCLAIMDATE='', " & vbCrLf & " STCLAIMPER=0, " & vbCrLf & " STCLAIMAMOUNT=0, " & vbCrLf & " ISCAPITAL='" & mCapital & "', PAYMENTDATE=TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ISMODVAT='" & mISMODVAT & "',ISSTREFUND='" & mISSTREFUND & "', " & vbCrLf & " ISCSTREFUND='" & mISCSTREFUND & "', ISFINALPOST='" & mFinalPost & "', " & vbCrLf & " ISFOC='" & mISFOC & "',ISSUPPBILL='" & mIsSuppBill & "', "
            SqlStr = SqlStr & vbCrLf & " TOTSERVICEPERCENT=" & Val(lblServicePercentage.Text) & ", " & vbCrLf & " TOTSERVICEAMOUNT=0, " & vbCrLf & " SERVNO='" & mSERVNo & "', " & vbCrLf & " SERVDATE=TO_DATE('" & VB6.Format(txtServDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ISSERVCLAIM='" & mIsServClaim & "', " & vbCrLf & " SERVCLAIMPERCENT=0, " & vbCrLf & " SERVICECLAIMAMOUNT=0, " & vbCrLf & " ISSERVTAX_POST='" & mServTax_Repost & "'," & vbCrLf & " SERV_PROV='" & MainClass.AllowSingleQuote(txtServProvided.Text) & "',"
            SqlStr = SqlStr & vbCrLf & " SHECMODVATPER=0, " & vbCrLf & " SHECMODVATAMOUNT=0, " & vbCrLf & " SHECPERCENT=0," & vbCrLf & " SHECAMOUNT=0," & vbCrLf & " ADEMODVATPER=0, " & vbCrLf & " ADEMODVATAMOUNT=0, " & vbCrLf & " ADEAMOUNT=0," & vbCrLf & " UPDATE_FROM='N',MODVAT_TYPE='" & mModvatType & "',SUR_VATCLAIMAMOUNT= 0,"
            SqlStr = SqlStr & vbCrLf & " SAC_CODE='" & mSACCode & "'," & vbCrLf & " SERVICE_ON_AMT=" & Val(txtServiceOn.Text) & "," & vbCrLf & " SERV_PROVIDER_PER=" & Val(txtProviderPer.Text) & "," & vbCrLf & " SERV_RECIPIENT_PER=" & Val(txtRecipientPer.Text) & "," & vbCrLf & " SERVICE_TAX_PER=" & Val(txtServiceTaxPer.Text) & "," & vbCrLf & " SERVICE_TAX_AMOUNT=" & Val(txtServiceTaxAmount.Text) & "," & vbCrLf & " KK_CESS_PER=0," & vbCrLf & " KK_CESS_AMOUNT=0,"
            SqlStr = SqlStr & vbCrLf & " ISGSTAPPLICABLE='" & mIsGSTRefund & "', " & vbCrLf & " GST_CLAIM_NO=" & Val(CStr(mGSTNo)) & ",  " & vbCrLf & " GST_CLAIM_DATE=TO_DATE('" & VB6.Format(lblGSTClaimDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " GST_CLAIM='" & IIf(chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", lblClaimStatus.Text) & "', " & vbCrLf & " GST_CLAIM_NEW_NO=" & Val(txtModvatNo.Text) & ",  " & vbCrLf & " GST_CLAIM_NEW_DATE=TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TOTALGSTVALUE=" & Val(CStr(mTotGSTAmount)) & ",  " & vbCrLf & " TOTCGST_REFUNDAMT=" & Val(txtTotCGSTRefund.Text) & ",  " & vbCrLf & " TOTSGST_REFUNDAMT=" & Val(txtTotSGSTRefund.Text) & ", " & vbCrLf & " TOTIGST_REFUNDAMT=" & Val(txtTotIGSTRefund.Text) & ",  " & vbCrLf & " TOTCGST_AMOUNT=" & Val(lblTotCGSTAmount.Text) & ",  " & vbCrLf & " TOTSGST_AMOUNT=" & Val(lblTotSGSTAmount.Text) & ", " & vbCrLf & " TOTIGST_AMOUNT=" & Val(lblTotIGSTAmount.Text) & ",  " & vbCrLf & " SHIPPED_TO_SAMEPARTY='" & mShipTo & "',  " & vbCrLf & " SHIPPED_TO_PARTY_CODE='" & mShipToCode & "', " & vbCrLf
            SqlStr = SqlStr & vbCrLf & " PURCHASE_TYPE= '" & lblPurchaseType.Text & "'," & vbCrLf & " ADV_VNO = '" & Trim(txtAdvVNo.Text) & "'," & vbCrLf & " ADV_VDATE = TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ADV_ADJUSTED_AMT = " & Val(txtAdvAdjust.Text) & ", " & vbCrLf & " ADV_CGST_AMT = " & Val(txtAdvCGST.Text) & ", " & vbCrLf & " ADV_SGST_AMT = " & Val(txtAdvSGST.Text) & ", " & vbCrLf & " ADV_IGST_AMT = " & Val(txtAdvIGST.Text) & ", " & vbCrLf & " ADV_ITEM_AMT = " & Val(txtItemAdvAdjust.Text) & ", PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ", "
            SqlStr = SqlStr & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & " " & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If
        PubDBCn.Execute(SqlStr)
        If FinancePVNOMST((LblMKey.Text), Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text), VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), IIf(chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), -1) = False Then GoTo ErrPart
        If UpdateDetail1(mNarration, mAccountCode, mVNo, mSuppCustCode, mShipTo, mShipToCode, mDivisionCode, mSaleBillNo, mSaleBillDate) = False Then GoTo ErrPart
        If UpdateMRRMain() = False Then GoTo ErrPart
        If VB.Left(cboGSTStatus.Text, 1) = "G" And mNewGSTNo = True And (Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text) > 0) Then ''chkCancelled.Value = vbUnchecked							
            If UpdateGSTSeqMaster(PubDBCn, (LblMKey.Text), LblBookCode.Text, mBookType, mBookSubType, mGSTNo, VB6.Format(lblGSTClaimDate.Text, "DD-MMM-YYYY"), mCapital, "N", "G") = False Then GoTo ErrPart
        End If
        If DeletePrevious() = False Then GoTo ErrPart
        pDueDate = txtPaymentdate.Text
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode And chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            ''tobecheck							
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ITEMTYPE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemType = MasterNo
                End If
            Else
                SprdMain.Row = 1
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)
                mAccountCode = GetItemAccountCode(mItemCode)
                If mAccountCode = "-1" Then MsgBox("Account Code not Defined For Item Code : " & mItemCode) : GoTo ErrPart
                mItemType = GetItemType(mItemCode)
            End If
            mDNCnNO = 0
            mDNCNCreated = False
            xExpDiffDN = False
            If ADDMode = True And chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConDebitNote, 1), "S", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConDebitNote, 1), "R", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConDebitNote, 1), "P", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConCreditNote, 1), "P", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                With SprdExp
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = ColExpDebitAmt
                        xDebitAmt = Val(.Text)
                        If xDebitAmt <> 0 Then
                            xExpDiffDN = True
                        End If
                    Next
                    If xExpDiffDN = True Then
                        mApproved = IIf(IsDBNull(RsCompany.Fields("RATE_Diff_DN_APP").Value), "N", RsCompany.Fields("RATE_Diff_DN_APP").Value)
                        If mApproved = "Y" Then
                            If MsgQuestion("Are You Want to Approved Debit Note For Rate Diff.") = CStr(MsgBoxResult.No) Then
                                mApproved = "N"
                            Else
                                mApproved = "Y"
                            End If
                        End If
                        If mDNCNCreated = True Then
                            mDNCnNO = mDNCnNO + 1
                        Else
                            mDNCnNO = 0
                        End If
                        mDNCNCreated = True
                        If UpdateDnCnMain(mVNo, (txtVDate.Text), Trim(txtBillNo.Text), Trim(txtBillDate.Text), Trim(txtMRRNo.Text), (txtMRRDate.Text), (txtPONo.Text), (txtPODate.Text), Val(txtCreditDays(0).Text), Val(txtCreditDays(1).Text), Trim(txtItemType.Text), "P", mCancelled, ConDebitNoteBookCode, VB.Left(ConDebitNote, 1), VB.Right(ConDebitNote, 1), mSuppCustCode, mAccountCode, (txtPaymentdate.Text), mApproved, mDNCnNO, xExpDiffDN, mDivisionCode, cntRow) = False Then GoTo ErrPart
                    End If
                End With
                mPDIRItem = GetPDIRItem(Val(txtMRRNo.Text))
                If mPDIRItem > 0 Then
                    PDIRAmount = mPDIRItem * Val(IIf(IsDBNull(RsCompany.Fields("PDIR_AMOUNT").Value), 0, RsCompany.Fields("PDIR_AMOUNT").Value))
                    If PDIRAmount > 0 Then
                        If IsDBNull(RsCompany.Fields("PDIR_CreditAcct").Value) Then
                            MsgBox("PDIR Credit Account Missing, Please Call Administrator....")
                            GoTo ErrPart
                        End If
                        If mDNCNCreated = True Then
                            mDNCnNO = mDNCnNO + 1
                        Else
                            mDNCnNO = 0
                        End If
                        mDNCNCreated = True
                        If UpdateDnCnMain(mVNo, (txtVDate.Text), Trim(txtBillNo.Text), Trim(txtBillDate.Text), Trim(txtMRRNo.Text), (txtMRRDate.Text), (txtPONo.Text), (txtPODate.Text), Val(txtCreditDays(0).Text), Val(txtCreditDays(1).Text), Trim(txtItemType.Text), "O", mCancelled, ConDebitNoteBookCode, VB.Left(ConDebitNote, 1), VB.Right(ConDebitNote, 1), mSuppCustCode, RsCompany.Fields("PDIR_CreditAcct").Value, (txtPaymentdate.Text), "Y", mDNCnNO, False, mDivisionCode, cntRow, PDIRAmount) = False Then GoTo ErrPart
                    End If
                End If
            End If
        End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If mBookSubType = "R" Then
                If Trim(txtPONo.Text) = "" Then
                    mSRBillNo = txtBillNo.Text
                    mSRBillDate = txtBillDate.Text
                Else
                    mSRBillNo = txtBillNo.Text
                    mSRBillDate = txtBillDate.Text
                End If
                SqlStr = "SELECT  CUST_REF_NO, CUST_REF_DATE, " & vbCrLf & " SUM(ITEM_AMT) AS ITEM_AMT " & vbCrLf & " FROM FIN_PURCHASE_DET " & vbCrLf & " WHERE MKEY = '" & (LblMKey.Text) & "'" & vbCrLf & " GROUP BY CUST_REF_NO, CUST_REF_DATE"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPostSRTrn, ADODB.LockTypeEnum.adLockReadOnly)
                If RsPostSRTrn.EOF = False Then
                    mFirstRow = True
                    mSubRowNo = 0
                    Do While RsPostSRTrn.EOF = False
                        mSubRowNo = mSubRowNo + 1
                        mSRBillNo = IIf(IsDBNull(RsPostSRTrn.Fields("CUST_REF_NO").Value), "", RsPostSRTrn.Fields("CUST_REF_NO").Value)
                        mSRBillDate = IIf(IsDBNull(RsPostSRTrn.Fields("CUST_REF_DATE").Value), "", RsPostSRTrn.Fields("CUST_REF_DATE").Value)
                        xItemValue = IIf(IsDBNull(RsPostSRTrn.Fields("ITEM_AMT").Value), 0, RsPostSRTrn.Fields("ITEM_AMT").Value)
                        xTOTEXPAMT = 0
                        xTotED = 0
                        xTotST = 0
                        xModvatAmount = 0
                        xCESSAmount = 0
                        xSHECAmount = 0
                        xServiceAmount = 0
                        xEDUAmount = 0
                        xSHECAmount = 0
                        xSTClaimAmount = 0
                        xNETVALUE = 0
                        xSurOnVat = 0
                        xSurcharge = 0
                        If mItemValue <> 0 Then
                            xTOTEXPAMT = Val(lblTotExpAmt.Text) * xItemValue / mItemValue
                            xTotED = 0
                            xTotST = 0
                            xModvatAmount = 0
                            xCESSAmount = 0
                            xSHECAmount = 0
                            xServiceAmount = 0
                            xEDUAmount = 0
                            xSHEC = 0
                            xSTClaimAmount = 0
                            xSurOnVat = 0
                            xSurcharge = 0
                            xNETVALUE = Val(CStr(mNETVALUE)) * xItemValue / mItemValue
                        End If
                        mSRBillNo = IIf(mSRBillNo = "", txtBillNo.Text, mSRBillNo)
                        mSRBillDate = IIf(mSRBillDate = "", txtBillDate.Text, mSRBillDate)
                        Dim mLocationID As String = GetDefaultLocation(mSuppCustCode)
                        If SaleReturnPostTRNGST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mSubRowNo, mVNo, (txtVDate.Text), mSRBillNo, mSRBillDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(xNETVALUE)), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, True, False), VB.Left(mNarration, 254), (txtRemarks.Text), Val(CStr(xTOTEXPAMT)), Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), IIf(mIsGSTRefund = "G", "Y", "N"), (txtBillNo.Text), (txtBillDate.Text), (txtMRRDate.Text), Val(CStr(xItemValue)), ADDMode, mAddUser, mAddDate, mLocal, mDivisionCode, mFirstRow, mLocationID, 2) = False Then GoTo ErrPart
                        RsPostSRTrn.MoveNext()
                        mFirstRow = False
                    Loop
                End If
            Else
                If mCompanyGSTNo = mPartyGSTNo Then
                    mNetExpAmount = Val(lblTotExpAmt.Text)
                Else
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                        mNetExpAmount = Val(lblTotExpAmt.Text) + Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text)
                    Else
                        mNetExpAmount = Val(lblTotExpAmt.Text)
                    End If
                End If
                Dim mLocationID As String = GetDefaultLocation(mSuppCustCode)
                If PurchasePostTRNGST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(lblNetAmount.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), mNetExpAmount, IIf(mIsGSTRefund = "G", "Y", "N"), Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(mIsGSTRefund = "R", "Y", "N"), Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), mSaleBillNo, mSaleBillDate, mLocationID) = False Then GoTo ErrPart
            End If
            If ADDMode = True Then
                If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Or chkESI.CheckState = System.Windows.Forms.CheckState.Checked Or ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
                    If Val(txtTDSAmount.Text) + Val(txtESIAmount.Text) + Val(txtSTDSAmount.Text) > 0 Then
                        If UpdateTDSVoucher(mDivisionCode) = False Then GoTo ErrPart
                        SqlStr = "UPDATE FIN_PURCHASE_HDR SET JVNO='" & txtJVVNO.Text & "', " & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKEY='" & LblMKey.Text & "'"
                        PubDBCn.Execute(SqlStr)
                    End If
                End If
            End If
            If MODIFYMode = True And chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = "UPDATE FIN_DNCN_HDR SET PURVNO='" & Trim(mVNo) & "', PURVDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND DNCNFROM IN ('P','M')"
                PubDBCn.Execute(SqlStr)
                SqlStr = " UPDATE FIN_DNCN_DET SET PURMKEY='" & LblMKey.Text & "', PURVNO='" & Trim(mVNo) & "', PURVDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_REF_NO=" & Val(txtMRRNo.Text) & " AND MKEY IN (" & vbCrLf & " SELECT MKEY FROM FIN_DNCN_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DNCNFROM IN ('P','M'))"
                PubDBCn.Execute(SqlStr)
                SqlStr = "UPDATE FIN_DNCN_HDR SET " & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DNCNFROM IN ('P','M') AND MKEY IN (" & vbCrLf & " SELECT MKEY FROM FIN_DNCN_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_REF_NO=" & Val(txtMRRNo.Text) & ")"
                PubDBCn.Execute(SqlStr)
            End If
        End If
        PubDBCn.CommitTrans()
        UpdateMain1 = True
        If Trim(pDnCnNo) <> "" Then
            MsgBox(pDnCnNo & " Created. ", MsgBoxStyle.Information)
        End If
        If ADDMode = True And Trim(txtJVVNO.Text) <> "" Then
            MsgBox("TDS Journal Voucher No. " & txtJVVNO.Text & " Created. ", MsgBoxStyle.Information)
        End If
        Exit Function
ErrPart:
        '    Resume							
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
    Private Function UpdateTempDNCNTable(ByRef pTempDNCNSeq As Double) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mSqlStr As String
        Dim mBookType As String
        Dim mSubBookType As String
        Dim mSubRowNo As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemQty As Double
        Dim mItemUOM As String
        Dim mItemRate As Double
        Dim mItemAmount As Double
        Dim mItemED As Double
        Dim mItemST As Double
        Dim mMRRRefNo As Double
        Dim mMRRDate As String
        Dim mSuppRefNo As String
        Dim mSuppRefDate As String
        Dim mSuppPoNo As String
        Dim mPORate As Double
        Dim mMrrRefType As String
        Dim mExpName As String
        Dim mEDAmount As Double
        Dim mEDPer As Double
        Dim mSTAmount As Double
        Dim mSTPer As Double
        Dim mAssessableValue As Double
        Dim mTaxableValue As Double
        Dim mAccountCode As String
        Dim I As Integer
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        PubDBCn.Execute("DELETE FROM TEMP_FIN_DNCN_DET Where AUTO_GEN_REFNO=" & pTempDNCNSeq & "")
        mEDAmount = 0
        mSTAmount = 0
        mAssessableValue = Val(lblTotItemValue.Text)
        mTaxableValue = Val(lblTotItemValue.Text)
        For I = 1 To SprdExp.MaxRows
            SprdExp.Row = I
            SprdExp.Col = ColExpName
            mExpName = Trim(SprdExp.Text)
            If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EXCISEABLE='Y'") = True Then
                SprdExp.Row = I
                SprdExp.Col = ColExpAmt
                mAssessableValue = mAssessableValue + Val(SprdExp.Text)
            End If
            If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TAXABLE='Y'") = True Then
                SprdExp.Row = I
                SprdExp.Col = ColExpAmt
                mTaxableValue = mTaxableValue + Val(SprdExp.Text)
            End If
            If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION='ED'") = True Then
                SprdExp.Row = I
                SprdExp.Col = ColExpAmt
                mEDAmount = mEDAmount + Val(SprdExp.Text)
            End If
            If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION='ST'") = True Then
                SprdExp.Row = I
                SprdExp.Col = ColExpAmt
                mSTAmount = mSTAmount + Val(SprdExp.Text)
            End If
        Next
        If Val(CStr(mAssessableValue)) <> 0 Then
            mEDPer = CDbl(VB6.Format(mEDAmount * 100 / Val(CStr(mAssessableValue)), "0.00"))
        End If
        If Val(CStr(mTaxableValue)) <> 0 Then
            mSTPer = CDbl(VB6.Format(mSTAmount * 100 / Val(CStr(mTaxableValue)), "0.00"))
        End If
        mSubRowNo = 0
        mMRRRefNo = Val(txtMRRNo.Text)
        mMRRDate = VB6.Format(txtMRRDate.Text, "DD/MM/YYYY")
        mSuppRefNo = Trim(txtBillNo.Text)
        mSuppRefDate = VB6.Format(txtBillDate.Text, "DD/MM/YYYY")
        mMrrRefType = GetMrrRefNo(Val(txtMRRNo.Text))
        With SprdMain
            mBookType = VB.Left(ConDebitNote, 1)
            mSubBookType = "S"
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColShortageQty
                mItemQty = Val(.Text)
                .Col = ColUnit
                mItemUOM = Trim(.Text)
                .Col = ColRate
                mItemRate = Val(.Text)
                mItemAmount = CDbl(VB6.Format(mItemQty * mItemRate, "0.0000"))
                .Col = ColPONo
                mSuppPoNo = Trim(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                    .Col = ColCGSTPer
                    mCGSTPer = Val(.Text)
                    .Col = ColSGSTPer
                    mSGSTPer = Val(.Text)
                    .Col = ColIGSTPer
                    mIGSTPer = Val(.Text)
                    mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 0)
                    mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 0)
                    mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 0)
                Else
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                End If
                If mItemQty > 0 Then
                    .Col = ColInvType
                    If Trim(.Text) = "" Then
                        mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                    Else
                        mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                    End If
                    mItemED = mItemAmount * mEDPer * 0.01
                    mItemST = mItemAmount * mSTPer * 0.01
                    mSubRowNo = mSubRowNo + 1
                    mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf & " ( " & vbCrLf & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf & " MRR_REF_TYPE, ACCOUNT_POST_CODE, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf & " ) VALUES ("
                    mSqlStr = mSqlStr & vbCrLf & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf & " " & mMRRRefNo & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '', '', ''," & vbCrLf & " '', '', " & mPORate & ", " & vbCrLf & " '" & mMrrRefType & "','" & mAccountCode & "', " & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(mSqlStr)
                End If
            Next
        End With
        With SprdMain
            mBookType = VB.Left(ConDebitNote, 1)
            mSubBookType = "R"
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColRejectedQty
                mItemQty = Val(.Text)
                .Col = ColUnit
                mItemUOM = Trim(.Text)
                .Col = ColRate
                mItemRate = Val(.Text)
                mItemAmount = CDbl(VB6.Format(mItemQty * mItemRate, "0.0000"))
                .Col = ColPONo
                mSuppPoNo = Trim(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                    .Col = ColCGSTPer
                    mCGSTPer = Val(.Text)
                    .Col = ColSGSTPer
                    mSGSTPer = Val(.Text)
                    .Col = ColIGSTPer
                    mIGSTPer = Val(.Text)
                    mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 0)
                    mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 0)
                    mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 0)
                Else
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                End If
                If mItemQty > 0 Then
                    .Col = ColInvType
                    If Trim(.Text) = "" Then
                        mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                    Else
                        mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                    End If
                    mItemED = mItemAmount * mEDPer * 0.01
                    mItemST = mItemAmount * mSTPer * 0.01
                    mSubRowNo = mSubRowNo + 1
                    mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf & " ( " & vbCrLf & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf & " MRR_REF_TYPE, ACCOUNT_POST_CODE," & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf & " ) VALUES ("
                    mSqlStr = mSqlStr & vbCrLf & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf & " " & mMRRRefNo & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '', '', ''," & vbCrLf & " '', '', " & mPORate & ", " & vbCrLf & " '" & mMrrRefType & "','" & mAccountCode & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(mSqlStr)
                End If
            Next
        End With
        With SprdMain
            mSubBookType = "P"
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColQty
                mItemQty = Val(.Text)
                .Col = ColShortageQty
                mItemQty = mItemQty - Val(.Text)
                .Col = ColRejectedQty
                mItemQty = mItemQty - Val(.Text)
                .Col = ColUnit
                mItemUOM = Trim(.Text)
                .Col = ColRate
                mItemRate = Val(.Text)
                .Col = ColPONo
                mSuppPoNo = Trim(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                    .Col = ColCGSTPer
                    mCGSTPer = Val(.Text)
                    .Col = ColSGSTPer
                    mSGSTPer = Val(.Text)
                    .Col = ColIGSTPer
                    mIGSTPer = Val(.Text)
                Else
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                End If
                If mPORate - mItemRate <> 0 Then
                    mItemRate = mPORate - mItemRate
                    If mItemRate > 0 Then
                        mBookType = VB.Left(ConCreditNote, 1)
                    Else
                        mBookType = VB.Left(ConDebitNote, 1)
                        mItemRate = mItemRate * -1
                    End If
                    mItemAmount = CDbl(VB6.Format(mItemQty * mItemRate, "0.0000"))
                    .Col = ColInvType
                    If Trim(.Text) = "" Then
                        mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                    Else
                        mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                    End If
                    mItemED = mItemAmount * mEDPer * 0.01
                    mItemST = mItemAmount * mSTPer * 0.01
                    mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 0)
                    mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 0)
                    mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 0)
                    mSubRowNo = mSubRowNo + 1
                    mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf & " ( " & vbCrLf & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf & " MRR_REF_TYPE, ACCOUNT_POST_CODE, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf & " ) VALUES ("
                    mSqlStr = mSqlStr & vbCrLf & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf & " " & mMRRRefNo & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '', '', ''," & vbCrLf & " '', '', " & mPORate & ", " & vbCrLf & " '" & mMrrRefType & "','" & mAccountCode & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(mSqlStr)
                End If
            Next
        End With
        With SprdMain
            mBookType = VB.Left(ConDebitNote, 1)
            mSubBookType = "V"
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColQty
                mItemQty = Val(.Text)
                .Col = ColShortageQty
                mItemQty = mItemQty - Val(.Text)
                .Col = ColRejectedQty
                mItemQty = mItemQty - Val(.Text)
                .Col = ColUnit
                mItemUOM = Trim(.Text)
                .Col = ColVolDiscRate
                mItemRate = Val(.Text)
                .Col = ColPONo
                mSuppPoNo = Trim(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                    .Col = ColCGSTPer
                    mCGSTPer = Val(.Text)
                    .Col = ColSGSTPer
                    mSGSTPer = Val(.Text)
                    .Col = ColIGSTPer
                    mIGSTPer = Val(.Text)
                Else
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                End If
                If (mItemQty * mItemRate) > 0 Then
                    mItemAmount = CDbl(VB6.Format(mItemQty * mItemRate, "0.0000"))
                    .Col = ColInvType
                    If Trim(.Text) = "" Then
                        mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                    Else
                        mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                    End If
                    mItemED = mItemAmount * mEDPer * 0.01
                    mItemST = mItemAmount * mSTPer * 0.01
                    '							
                    mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 0)
                    mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 0)
                    mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 0)
                    mSubRowNo = mSubRowNo + 1
                    mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf & " ( " & vbCrLf & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf & " MRR_REF_TYPE, ACCOUNT_POST_CODE, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf & " ) VALUES ("
                    mSqlStr = mSqlStr & vbCrLf & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf & " " & mMRRRefNo & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '', '', ''," & vbCrLf & " '', '', " & mPORate & ", " & vbCrLf & " '" & mMrrRefType & "','" & mAccountCode & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(mSqlStr)
                End If
            Next
        End With
        UpdateTempDNCNTable = True
        Exit Function
ErrPart:
        UpdateTempDNCNTable = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Function IsDebitNoteDeduct(ByRef pTempDNCNSeq As Double, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mVNo As String, ByRef mSuppCustCode As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mApproved As String
        Dim mAccountCode As String
        Dim mDNCnNO As Integer
        Dim mMsg As String
        IsDebitNoteDeduct = False
        mApproved = IIf(IsDBNull(RsCompany.Fields("Shortage_DN_APP").Value), "N", RsCompany.Fields("Shortage_DN_APP").Value)
        If pBookSubType = "S" Then
            mApproved = IIf(IsDBNull(RsCompany.Fields("Shortage_DN_APP").Value), "N", RsCompany.Fields("Shortage_DN_APP").Value)
            mMsg = "Are You Want to Approved Debit Note For Shortage."
        ElseIf pBookSubType = "R" Then
            mApproved = IIf(IsDBNull(RsCompany.Fields("Rejection_DN_APP").Value), "N", RsCompany.Fields("Rejection_DN_APP").Value)
            mMsg = "Are You Want to Approved Debit Note For Rejection."
            If RsCompany.Fields("REJECTION_DN").Value = "N" Then
                IsDebitNoteDeduct = True
                Exit Function
            End If
        ElseIf pBookSubType = "P" Then
            If pBookType = VB.Left(ConDebitNote, 1) Then
                mApproved = IIf(IsDBNull(RsCompany.Fields("RATE_Diff_DN_APP").Value), "N", RsCompany.Fields("RATE_Diff_DN_APP").Value)
                mMsg = "Are You Want to Approved Debit Note For PO Rate Diff."
            Else
                If RsCompany.Fields("RATE_Diff_CN").Value = "N" Then
                    IsDebitNoteDeduct = True
                    Exit Function
                End If
                mApproved = IIf(IsDBNull(RsCompany.Fields("RATE_Diff_CN_APP").Value), "N", RsCompany.Fields("RATE_Diff_CN_APP").Value)
                mMsg = "Are You Want to Approved Credit Note For PO Rate Diff."
            End If
        ElseIf pBookSubType = "V" Then
            mApproved = "Y"
            mMsg = "Are You Want to Approved Debit Note For Volume Discount."
        End If
        mSqlStr = "SELECT ACCOUNT_POST_CODE FROM TEMP_FIN_DNCN_DET " & vbCrLf & " WHERE AUTO_GEN_REFNO=" & pTempDNCNSeq & " " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DNCN_BOOKTYPE='" & pBookType & "'" & vbCrLf & " AND DNCN_BOOKSUBTYPE='" & pBookSubType & "'" & vbCrLf & " GROUP BY ACCOUNT_POST_CODE"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If mApproved = "N" Then
                If pBookType = VB.Left(ConCreditNote, 1) Then
                    mApproved = "N"
                Else
                    If MsgQuestion(mMsg) = CStr(MsgBoxResult.No) Then
                        mApproved = "N"
                    Else
                        mApproved = "Y"
                    End If
                End If
            End If
            Do While RsTemp.EOF = False
                mAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNT_POST_CODE").Value), "", RsTemp.Fields("ACCOUNT_POST_CODE").Value)
                If UpdateNewDnCnMain(mVNo, (txtVDate.Text), Trim(txtBillNo.Text), Trim(txtBillDate.Text), Trim(txtMRRNo.Text), (txtMRRDate.Text), (txtPONo.Text), (txtPODate.Text), Val(txtCreditDays(0).Text), Val(txtCreditDays(1).Text), Trim(txtItemType.Text), pBookSubType, "N", IIf(pBookType = VB.Left(ConDebitNote, 1), ConDebitNoteBookCode, ConCreditNoteBookCode), VB.Left(IIf(pBookType = VB.Left(ConDebitNote, 1), ConDebitNote, ConCreditNote), 1), VB.Right(IIf(pBookType = VB.Left(ConDebitNote, 1), ConDebitNote, ConCreditNote), 1), mSuppCustCode, mAccountCode, (txtPaymentdate.Text), mApproved, pTempDNCNSeq, False, mDivisionCode, mAccountCode) = False Then GoTo ErrPart
                RsTemp.MoveNext()
            Loop
        End If
        IsDebitNoteDeduct = True
        Exit Function
ErrPart:
        IsDebitNoteDeduct = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Function UpdateNewDnCnMain(ByRef xPURVNO As String, ByRef xPURVDate As String, ByRef xBillNo As String, ByRef xBillDate As String, ByRef xMRRNo As String, ByRef xMRRDate As String, ByRef xPoNo As String, ByRef xPODate As String, ByRef xCreditDays1 As Integer, ByRef xCreditDays2 As Integer, ByRef xItemDesc As String, ByRef xDnCnType As String, ByRef xCancelled As String, ByRef xBookCode As Integer, ByRef xBookType As String, ByRef xBookSubType As String, ByRef xDebitAccountCode As String, ByRef xCreditAccountCode As String, ByRef xPayDate As String, ByRef xApproved As String, ByRef pTempDNCNSeq As Double, ByRef pExpDiffDN As Boolean, ByRef mDivisionCode As Double, ByRef xAccountCode As String, Optional ByRef xAmount As Double = 0) As Boolean
        On Error GoTo ErrPart
        Dim xMKey As String
        Dim xCurRowNo As Integer
        Dim SqlStr As String
        Dim xVNoPrefix As String
        Dim xVTYPE As String
        Dim xVNoSeq As Double
        Dim xVNoSuffix As String
        Dim xVNo As String
        Dim xVDate As String
        Dim xItemValue As Double
        Dim xSTPERCENT As Double
        Dim xTOTSTAMT As Double
        Dim xTOTFREIGHT As Double
        Dim xTOTCHARGES As Double
        Dim xEDPERCENT As Double
        Dim xTotEDAmount As Double
        Dim xSURAmount As Double
        Dim xTotDiscount As Double
        Dim xMSC As Double
        Dim xRO As Double
        Dim xTOTEXPAMT As Double
        Dim xTOTTAXABLEAMOUNT As Double
        Dim xNETVALUE As Double
        Dim xTotQty As Double
        Dim xNarration As String
        Dim xReason As String
        Dim nBookCode As String
        Dim RsTemp As ADODB.Recordset
        Dim mPDIRItem As String
        Dim mExpAmount As Double
        Dim mDNFROM As String
        'Dim RsTemp As ADODB.Recordset							
        Dim xCessAmt As Double
        Dim xSHECessAmt As Double
        Dim xTotServiceAmt As Double
        Dim xSTClaimNo As String
        Dim xSTClaimPer As Double
        Dim xSTClaimAmount As Double
        Dim xSURVATClaimAmount As Double
        Dim xSTClaimDate As String
        Dim xISSTRefund As String
        Dim xTOTSURCHARGEAMT As Double
        Dim xTOTVATCLAIMAMT As Double
        Dim xISCSTRefund As String
        Dim pInsertRow As Boolean
        Dim xIsGST As String
        Dim xCGSTRefundAMT As Double
        Dim xSGSTRefundAMT As Double
        Dim xIGSTRefundAMT As Double
        Dim pSuppCustCode As String
        Dim pAccountCode As String
        Dim mSubRow As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        Dim mItemDesc As String
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim pDNSeqType As Integer

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If


        If xCancelled = "Y" Then UpdateNewDnCnMain = True : Exit Function
        If xDnCnType = "R" Then
            xVTYPE = IIf(xBookCode = ConDebitNoteBookCode, "DR", "CR")
            mDNFROM = "M"
        Else
            xVTYPE = IIf(xBookCode = ConDebitNoteBookCode, "DN", "CN")
            mDNFROM = "P"
        End If
        xSTClaimNo = ""
        xSTClaimPer = 0
        xSTClaimAmount = 0
        xSTClaimDate = ""
        xSURVATClaimAmount = 0
        xISSTRefund = "N"
        xISCSTRefund = "N"
        xIsGST = VB.Left(cboGSTStatus.Text, 1)
        xItemValue = 0
        xSTPERCENT = 0
        xTOTSTAMT = 0
        xTOTFREIGHT = 0
        xTOTCHARGES = 0
        xEDPERCENT = 0
        xTotEDAmount = 0
        xSURAmount = 0
        xTotDiscount = 0
        xMSC = 0
        xRO = 0
        xTOTEXPAMT = 0
        xTOTTAXABLEAMOUNT = 0
        xNETVALUE = 0
        xTotQty = 0
        xNarration = ""
        xTOTSURCHARGEAMT = 0
        xTOTVATCLAIMAMT = 0
        If xDnCnType = "R" Then
            xReason = "REJECTION"
            pDNSeqType = 4
        ElseIf xDnCnType = "S" Then
            xReason = "SHORTAGE"
            pDNSeqType = 1
        ElseIf xDnCnType = "P" Then
            xReason = "RATE DIFF"
            pDNSeqType = 2
        ElseIf xDnCnType = "V" Then
            xReason = "VOLUME DISCOUNT"
            pDNSeqType = 5
        ElseIf xDnCnType = "D" Then
            xReason = "DISCOUNT"
            pDNSeqType = 6
        ElseIf xDnCnType = "A" Then
            xReason = "AMENDED PO RATE DIFF"
            pDNSeqType = 3
        ElseIf xDnCnType = "O" Then
            xNETVALUE = xAmount
            xReason = "PDIR NOT RECEVIED."
            xNarration = "PDIR NOT RECEVIED."
            pDNSeqType = 7
            SqlStr = "SELECT ITEM_CODE " & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " WHERE " & vbCrLf & " AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND PDIR_FLAG='N'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    mPDIRItem = IIf(mPDIRItem = "", "", mPDIRItem & ",") & IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    RsTemp.MoveNext()
                Loop
                xNarration = xNarration & " AGT. ITEM CODE  : " & mPDIRItem & " ( Rs. 200/- each)"
            End If
        End If
        xVNoSeq = CDbl(AutoGenDNCNNo("VNOSEQ", xBookCode, xVTYPE, pDNSeqType))
        ''xVNoSeq = xDNCNNO + AutoGenDNCNNo("VNOSEQ", xBookCode, xVTYPE)							
        xVNoPrefix = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")
        xVNoSuffix = ""
        xVNo = Trim(xVTYPE) & Trim(xVNoPrefix) & VB6.Format(Val(CStr(xVNoSeq)), "00000") & Trim(xVNoSuffix)
        xVDate = txtVDate.Text
        SqlStr = ""
        If ADDMode = True Then
            If xDnCnType <> "O" Then
                If UpdateNewDNCNDetail1(xBookType, xDnCnType, xMKey, xVTYPE, xPURVNO, xPURVDate, xAccountCode, "Y", pInsertRow) = False Then GoTo ErrPart
            End If
            If pInsertRow = False Then
                UpdateNewDnCnMain = True
                Exit Function
            End If
            xCurRowNo = MainClass.AutoGenRowNo("FIN_DNCN_HDR", "RowNo", PubDBCn)
            xMKey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & xCurRowNo
            SqlStr = "INSERT INTO FIN_DNCN_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, ROWNO, " & vbCrLf & " VNOPREFIX, VTYPE,VNOSEQ, VNOSUFFIX, " & vbCrLf & " VNO, VDATE, BILLNO, INVOICE_DATE, " & vbCrLf & " DEBITACCOUNTCODE, CREDITACCOUNTCODE, DUEDAYSFROM, DUEDAYSTO, " & vbCrLf & " BOOKCODE, BookType, BOOKSUBTYPE, REMARKS,  " & vbCrLf & " ITEMDESC, REASON, ITEMVALUE, STPERCENT,  " & vbCrLf & " TOTSTAMT, TOTFREIGHT, TOTCHARGES, EDPERCENT,  " & vbCrLf & " TOTEDAMOUNT, TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, " & vbCrLf & " TOTRO, TOTEXPAMT, TOTTAXABLEAMOUNT, NETVALUE, " & vbCrLf & " TOTQTY, CANCELLED, NARRATION, DNCNTYPE, APPROVED, PAYDATE, DNCNFROM, " & vbCrLf & " PURVNO, PURVDATE, AUTO_KEY_MRR, MRRDATE, " & vbCrLf & " CUSTREFNO, CUSTREFDATE, MODVATNO, MODVATDATE, " & vbCrLf & " MODVATPER, MODVATAMOUNT, STCLAIMNO, STCLAIMPER, " & vbCrLf & " STCLAIMAMOUNT, STCLAIMDATE, ISMODVAT, ISSTREFUND, " & vbCrLf & " ISDESPATCHED, SALEINVOICENO, SALEINVOICEDATE, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,ISCSTREFUND, " & vbCrLf & " UPDATE_FROM, SUR_VATCLAIMAMOUNT,DIV_CODE, " & vbCrLf & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT," & vbCrLf & " ISGSTREFUND , GST_NO, GST_DATE, CGST_REFUNDAMOUNT, " & vbCrLf & " SGST_REFUNDAMOUNT, IGST_REFUNDAMOUNT,DNCNSEQTYPE) "
            SqlStr = SqlStr & vbCrLf & " VALUES('" & xMKey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & xCurRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(xVNoPrefix) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(xVTYPE) & "', " & vbCrLf & " " & xVNoSeq & ", '" & MainClass.AllowSingleQuote(xVNoSuffix) & "', '" & MainClass.AllowSingleQuote(xVNo) & "',TO_DATE('" & VB6.Format(xVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(xBillNo) & "', TO_DATE('" & VB6.Format(xBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & xDebitAccountCode & "','" & xCreditAccountCode & "', " & Val(CStr(xCreditDays1)) & ", " & Val(CStr(xCreditDays2)) & ", " & vbCrLf & " '" & xBookCode & "', '" & xBookType & "', '" & xBookSubType & "', ''," & vbCrLf & " '" & MainClass.AllowSingleQuote(xItemDesc) & "', '" & MainClass.AllowSingleQuote(xReason) & "', " & vbCrLf & " " & xItemValue & ", " & xSTPERCENT & ", " & xTOTSTAMT & ", " & xTOTFREIGHT & ", " & xTOTCHARGES & "," & vbCrLf & " " & xEDPERCENT & ", " & xTotEDAmount & ", " & xSURAmount & ", " & xTotDiscount & "," & xMSC & ", " & vbCrLf & " " & xRO & ", " & xTOTEXPAMT & ", " & xTOTTAXABLEAMOUNT & ", " & xNETVALUE & ", " & vbCrLf & " " & xTotQty & ", '" & xCancelled & "', '" & MainClass.AllowSingleQuote(xNarration) & "', " & vbCrLf & " '" & xDnCnType & "', '" & xApproved & "', TO_DATE('" & VB6.Format(xPayDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mDNFROM & "', " & vbCrLf & " '" & xPURVNO & "', TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & xMRRNo & "', TO_DATE('" & VB6.Format(xMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '', '', '', '', " & vbCrLf & " 100, 0, '" & xSTClaimNo & "', " & xSTClaimPer & ", " & vbCrLf & " " & xSTClaimAmount & ", TO_DATE('" & VB6.Format(xSTClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'N', '" & xISSTRefund & "'," & vbCrLf & " 'N','',''," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & vbCrLf & " '" & xISCSTRefund & "','N'," & Val(CStr(xSURVATClaimAmount)) & "," & mDivisionCode & ",0,0,0," & vbCrLf & " '" & xIsGST & "', '' , '',0,0,0," & pDNSeqType & ")"
        End If
        PubDBCn.Execute(SqlStr)
        UpdateNewDnCnMain = True
        '    If pExpDiffDN = True Then							
        '        If UpdateDNCNExp1(xMkey) = False Then GoTo ErrPart							
        '    End If							
        If xDnCnType = "O" Then
            If UpdateDNCNPDIRExp1(xMKey, xAmount) = False Then GoTo ErrPart
        Else
            If UpdateNewDNCNDetail1(xBookType, xDnCnType, xMKey, xVTYPE, xPURVNO, xPURVDate, xAccountCode, "N", pInsertRow) = False Then GoTo ErrPart
        End If
        If (xDnCnType = "P" Or xDnCnType = "R" Or xDnCnType = "S" Or xDnCnType = "V") Then 'And pExpDiffDN = False							
            If UpdateDNCNRateDiffExp1(xDnCnType, xMKey, pExpDiffDN) = False Then GoTo ErrPart
        End If
        If xApproved = "Y" Then
            If UpdateDNCNHDRAPP(xMKey, xDnCnType) = False Then GoTo ErrPart
            nBookCode = CStr(xBookCode)
            SqlStr = "SELECT NETVALUE, TOTEXPAMT, TOTEDAMOUNT,TOTSTAMT, " & vbCrLf & " TOTSURCHARGEAMT,SUR_VATCLAIMAMOUNT, " & vbCrLf & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT, " & vbCrLf & " CGST_REFUNDAMOUNT, SGST_REFUNDAMOUNT, IGST_REFUNDAMOUNT" & vbCrLf & " FROM FIN_DNCN_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & xMKey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                xAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mExpAmount = IIf(IsDBNull(RsTemp.Fields("TOTEXPAMT").Value), 0, RsTemp.Fields("TOTEXPAMT").Value)
                xTotEDAmount = IIf(IsDBNull(RsTemp.Fields("TOTEDAMOUNT").Value), 0, RsTemp.Fields("TOTEDAMOUNT").Value)
                xTOTSTAMT = IIf(IsDBNull(RsTemp.Fields("TOTSTAMT").Value), 0, RsTemp.Fields("TOTSTAMT").Value)
                xTOTSURCHARGEAMT = IIf(IsDBNull(RsTemp.Fields("TOTSURCHARGEAMT").Value), 0, RsTemp.Fields("TOTSURCHARGEAMT").Value)
                xTOTVATCLAIMAMT = IIf(IsDBNull(RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value), 0, RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value)
                '            xCGSTAMT = IIf(IsNull(RsTemp!NETCGST_AMOUNT), 0, RsTemp!NETCGST_AMOUNT)							
                '            xSGSTAMT = IIf(IsNull(RsTemp!NETSGST_AMOUNT), 0, RsTemp!NETSGST_AMOUNT)							
                '            xIGSTAMT = IIf(IsNull(RsTemp!NETIGST_AMOUNT), 0, RsTemp!NETIGST_AMOUNT)							
                xCGSTRefundAMT = IIf(IsDBNull(RsTemp.Fields("CGST_REFUNDAMOUNT").Value), 0, RsTemp.Fields("CGST_REFUNDAMOUNT").Value)
                xSGSTRefundAMT = IIf(IsDBNull(RsTemp.Fields("SGST_REFUNDAMOUNT").Value), 0, RsTemp.Fields("SGST_REFUNDAMOUNT").Value)
                xIGSTRefundAMT = IIf(IsDBNull(RsTemp.Fields("IGST_REFUNDAMOUNT").Value), 0, RsTemp.Fields("IGST_REFUNDAMOUNT").Value)
                If Trim(mCompanyGSTNo) = Trim(mPartyGSTNo) Then
                    xAmount = xAmount - xCGSTRefundAMT - xSGSTRefundAMT - xIGSTRefundAMT
                    xCGSTRefundAMT = 0
                    xSGSTRefundAMT = 0
                    xIGSTRefundAMT = 0
                End If
            End If
            pSuppCustCode = IIf(CDbl(nBookCode) = ConDebitNoteBookCode, xDebitAccountCode, xCreditAccountCode)
            pAccountCode = IIf(CDbl(nBookCode) = ConDebitNoteBookCode, xCreditAccountCode, xDebitAccountCode)

            Dim mLocationID As String = GetDefaultLocation(pSuppCustCode)
            If DNCNPostTRNGST(PubDBCn, xMKey, xCurRowNo, nBookCode, xBookType, xBookSubType, xVTYPE, xVNo, xVDate, xBillNo, xBillDate, xDebitAccountCode, xCreditAccountCode, Val(CStr(xAmount)), IIf(xCancelled = "Y", True, False), xPayDate, "", xReason, mExpAmount, ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(xIsGST = "G", IIf(Trim(mCompanyGSTNo) = Trim(mPartyGSTNo), "N", "Y"), "N"), xCGSTRefundAMT, xSGSTRefundAMT, xIGSTRefundAMT, xDnCnType, mLocationID) = False Then GoTo ErrPart

            SqlStr = " SELECT SUBROWNO, ITEM_CODE,ITEM_QTY,ITEM_UOM,HSNCODE, ITEM_RATE, ITEM_AMT, " & vbCrLf & " CGST_PER,SGST_PER,IGST_PER, " & vbCrLf & " CGST_AMOUNT,SGST_AMOUNT,IGST_AMOUNT " & vbCrLf & " FROM FIN_DNCN_DET" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY='" & xMKey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mSubRow = IIf(IsDBNull(RsTemp.Fields("SUBROWNO").Value), 0, RsTemp.Fields("SUBROWNO").Value)
                    mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo
                    Else
                        mItemDesc = ""
                    End If
                    mHSNCode = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)
                    mQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                    mUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                    mRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                    mAmount = IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value)
                    mCGSTPer = IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value)
                    mSGSTPer = IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value)
                    mIGSTPer = IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value)
                    mCGSTAmount = IIf(IsDBNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value)
                    mSGSTAmount = IIf(IsDBNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value)
                    mIGSTAmount = IIf(IsDBNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value)
                    If Trim(mCompanyGSTNo) = Trim(mPartyGSTNo) Then
                    Else
                        If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                            If UpdateGSTTRN(PubDBCn, xMKey, nBookCode, xBookType, xBookSubType, xVNo, VB6.Format(xVDate, "DD-MMM-YYYY"), Trim(xBillNo), VB6.Format(xBillDate, "DD-MMM-YYYY"), "", "", pSuppCustCode, pAccountCode, "Y", pSuppCustCode, mSubRow, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", xDnCnType, IIf(lblPurchaseType.Text = "G", "G", "S"), IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N"), IIf(xBookCode = ConDebitNoteBookCode, "D", "C"), (lblGSTClaimDate.Text), "N") = False Then GoTo ErrPart
                        End If
                    End If
                    RsTemp.MoveNext()
                Loop
            End If
        End If
        pDnCnNo = IIf(pDnCnNo = "", "", pDnCnNo & ", ") & xVNo
        Exit Function
ErrPart:
        UpdateNewDnCnMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Function UpdateNewDNCNDetail1(ByRef pBookType As String, ByRef pDnCnType As String, ByRef xKey As String, ByRef pVType As String, ByRef xPURVNO As String, ByRef xPURVDate As String, ByRef pAccountCode As String, ByRef mOnlyCheck As String, ByRef pInsertRow As Boolean) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mPORate As Double
        Dim mAmount As Double
        Dim mPONo As String
        Dim mMrrRefType As String
        Dim mFactor As Double
        Dim pItemEDAmount As Double
        Dim pItemSTAmount As Double
        Dim mEDPer As Double
        Dim mEDAmount As Double
        Dim mItemValue As Double
        Dim mExpCode As Double
        Dim mExpName As String
        Dim mEDPerNos As Double
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        pInsertRow = False
        PubDBCn.Execute("Delete From FIN_DNCN_DET Where Mkey='" & xKey & "'")
        SqlStr = ""
        SqlStr = "SELECT ITEM_CODE, HSNCODE, ITEM_DESC, SUM(ITEM_QTY) AS ITEM_QTY," & vbCrLf & " ITEM_UOM, ITEM_RATE, SUM(ITEM_ED) AS ITEM_ED, SUM(ITEM_ST) AS ITEM_ST," & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, SUPP_REF_DATE, " & vbCrLf & " REF_PO_NO, PO_RATE, MRR_REF_TYPE," & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT" & vbCrLf & " FROM TEMP_FIN_DNCN_DET " & vbCrLf & " WHERE AUTO_GEN_REFNO=" & pTempDNCNSeq & " " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DNCN_BOOKTYPE='" & pBookType & "'" & vbCrLf & " AND DNCN_BOOKSUBTYPE='" & pDnCnType & "'" & vbCrLf & " AND ACCOUNT_POST_CODE='" & pAccountCode & "'" & vbCrLf & " GROUP BY ITEM_CODE, HSNCODE, ITEM_DESC, " & vbCrLf & " ITEM_UOM, ITEM_RATE," & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, SUPP_REF_DATE, " & vbCrLf & " REF_PO_NO, PO_RATE, MRR_REF_TYPE,CGST_PER, SGST_PER, IGST_PER"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        I = 1
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mHSNCode = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)
                mItemDesc = IIf(IsDBNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)
                mUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                mPONo = IIf(IsDBNull(RsTemp.Fields("REF_PO_NO").Value), "", RsTemp.Fields("REF_PO_NO").Value)
                mQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                mRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                mPORate = IIf(IsDBNull(RsTemp.Fields("PO_RATE").Value), 0, RsTemp.Fields("PO_RATE").Value)
                mMrrRefType = IIf(IsDBNull(RsTemp.Fields("MRR_REF_TYPE").Value), "", RsTemp.Fields("MRR_REF_TYPE").Value)
                mEDPerNos = 0
                mCGSTPer = IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value)
                mSGSTPer = IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value)
                mIGSTPer = IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value)
                mCGSTAmount = System.Math.Round(IIf(IsDBNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value), 0)
                mSGSTAmount = System.Math.Round(IIf(IsDBNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value), 0)
                mIGSTAmount = System.Math.Round(IIf(IsDBNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value), 0)
                If mQty <> 0 Then
                    mEDPerNos = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_ED").Value), 0, RsTemp.Fields("ITEM_ED").Value) / mQty, "0.00"))
                End If
                If pDnCnType = "R" Then
                    SqlStr = "SELECT DECODE(ISSUE_UOM,'" & mUnit & "',1,UOM_FACTOR) AS UOM_FACTOR,ISSUE_UOM FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
                    mFactor = 1
                    If RsMisc.EOF = False Then
                        mUnit = IIf(IsDBNull(RsMisc.Fields("ISSUE_UOM").Value), "", RsMisc.Fields("ISSUE_UOM").Value)
                        mFactor = IIf(IsDBNull(RsMisc.Fields("UOM_FACTOR").Value), 1, RsMisc.Fields("UOM_FACTOR").Value)
                    End If
                    mQty = mQty * mFactor
                    mQty = mQty - GetDebitQty(Val(txtMRRNo.Text), mItemCode, pDnCnType)
                    mQty = IIf(mQty < 0, 0, mQty)
                    '                If pDnCnType = "R" And chkModvat.Value = vbUnchecked Then							
                    '                    mRate = Format(mRate + mEDPerNos, "0.0000")							
                    '                End If							
                    mRate = mRate / mFactor
                ElseIf pDnCnType = "S" Then
                    mQty = mQty - GetDebitQty(Val(txtMRRNo.Text), mItemCode, pDnCnType)
                ElseIf pDnCnType = "P" Then
                    If mPORate <> 0 Then
                        If pBookType = VB.Left(ConDebitNote, 1) Then
                            mRate = System.Math.Abs(mRate) '' Abs(IIf(mRate - mPORate <= 0, 0, mRate - mPORate))							
                        ElseIf pBookType = VB.Left(ConCreditNote, 1) Then
                            mRate = System.Math.Abs(mRate) '' Abs(IIf(mPORate - mRate <= 0, 0, mPORate - mRate))							
                        Else
                            mRate = 0
                        End If
                    Else
                        mRate = 0
                    End If
                End If
                mAmount = mQty * mRate
                mAmount = CDbl(VB6.Format(mAmount, "0.00"))
                pItemEDAmount = 0
                pItemSTAmount = 0
                If mItemCode <> "" And mAmount <> 0 Then
                    I = I + 1
                    If mOnlyCheck = "N" Then
                        SqlStr = " INSERT INTO FIN_DNCN_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , HSNCODE, ITEM_DESC, ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT," & vbCrLf & " MRR_REF_NO,MRR_REF_DATE,SUPP_REF_NO," & vbCrLf & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, " & vbCrLf & " PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, " & vbCrLf & " PO_RATE, MRR_REF_TYPE,ITEM_ED, ITEM_ST, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT" & vbCrLf & " ) "
                        SqlStr = SqlStr & vbCrLf & " VALUES ('" & xKey & "'," & I & ", " & vbCrLf & " '" & mItemCode & "','" & mHSNCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "'," & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & "," & vbCrLf & " " & Val(txtMRRNo.Text) & ",TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mPONo & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & LblMKey.Text & "'," & vbCrLf & " '" & xPURVNO & "',TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & xPURVNO & "',TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & mPORate & ", '" & mMrrRefType & "'," & Val(CStr(pItemEDAmount)) & ", " & Val(CStr(pItemSTAmount)) & "," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & " " & vbCrLf & " ) "
                        PubDBCn.Execute(SqlStr)
                    End If
                    pInsertRow = True
                End If
                RsTemp.MoveNext()
            Loop
        End If
        UpdateNewDNCNDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateNewDNCNDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Function GetDebitQty(ByRef pMRRNo As Double, ByRef pItemCode As String, ByRef pDnCnType As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        If pDnCnType = "R" Then
            SqlStr = "SELECT SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY * DECODE(IH.BOOKSUBTYPE,'D',1,-1)) AS QTY "
        Else
            SqlStr = "SELECT SUM(ID.ITEM_QTY * DECODE(IH.BOOKSUBTYPE,'D',1,-1)) AS QTY "
        End If
        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.DNCNTYPE='" & pDnCnType & "' AND CANCELLED='N'  AND APPROVED='Y'" ''('M','R','S')							
        If pDnCnType = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM IN ('M')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM IN ('P')"
        End If
        '    If LblBookCode.text = ConCreditNoteBookCode Then							
        '        SqlStr = SqlStr & vbCrLf & " AND IH.ISDESPATCHED='Y'"							
        '    End If							
        If Trim(LblMKey.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY<>'" & LblMKey.Text & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetDebitQty = IIf(IsDBNull(RsTemp.Fields("QTY").Value), 0, RsTemp.Fields("QTY").Value)
        End If
        Exit Function
ErrPart:
        GetDebitQty = 0
    End Function
    Private Function UpdateTDSVoucher(ByRef mDivisionCode As Double) As Boolean
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
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        mVType = "JVT"

        mVNo = GenJVVno(mVType)
        mVNoPrefix = GenPrefixVNo(txtVDate.Text)
        mVNoSuffix = ""
        mVnoStr = mVType & mVNoPrefix & mVNo & mVNoSuffix
        txtJVVNO.Text = mVnoStr
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBookCode = CStr(ConJournalBookCode)
        If ADDMode = True Then
            pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurJVMKey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE,IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY ) VALUES ( " & vbCrLf & " '" & CurJVMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & pRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote("") & "', '" & mCancelled & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N'," & vbCrLf & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'N','N','')"
        ElseIf MODIFYMode = True Then
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf & " Vdate=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EXPDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " VType= '" & mVType & "'," & vbCrLf & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf & " VnoSeq=" & Val(mVNo) & ", " & vbCrLf & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf & " Vno='" & mVnoStr & "', " & vbCrLf & " BookCode='" & mBookCode & "', " & vbCrLf & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf & " CANCELLED='" & mCancelled & "', " & vbCrLf & " BookType='" & mBookType & "', " & vbCrLf & " BookSubType='" & mBookSubType & "', " & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " Where Mkey='" & CurJVMKey & "'"
        End If
        PubDBCn.Execute(SqlStr)
        If UpdateJVDetail(CurJVMKey, pRowNo, mBookCode, mVType, mVnoStr, (txtVDate.Text), "", PubDBCn, mDivisionCode) = False Then GoTo ErrPart
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If UpdateTDSCreditDetail(CurJVMKey, mVnoStr, mBookType, mBookSubType) = False Then GoTo ErrPart
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
        If ADDMode = True Then
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
        End If
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
            mParticulars = ""
            mChequeNo = ""
            mChqDate = ""
            mCCCode = "-1"
            mDeptCode = "-1"
            mEmpCode = "-1"
            mExpCode = "-1"
            mIBRNo = "-1"
            mClearDate = ""
            I = 1
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', " & mAccountCode & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "','" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdateSuppPRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, mAmount, mRemarks, mDivisionCode) = False Then GoTo ErrDetail
        End If
        '******TDS ACCOUNT POSTING							
        mPRRowNo = 2
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        mParticulars = ""
        mParticulars = "TDS DEDUCT ON RS. " & VB6.Format(txtTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtTDSRate.Text, "0.000") & "%"
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
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', " & mAccountCode & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_CHAR('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        '******ESI ACCOUNT POSTING							
        mPRRowNo = 3
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("ESICREDITACCOUNT").Value), "-1", RsCompany.Fields("ESICREDITACCOUNT").Value)
        mParticulars = "ESI DEDUCT ON RS. " & VB6.Format(txtESIDeductOn.Text, "0.000") & " @ " & VB6.Format(txtESIRate.Text, "0.000") & "%"
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
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', " & mAccountCode & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        '******STDS ACCOUNT POSTING							
        mPRRowNo = 4
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("STDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("STDSCREDITACCOUNT").Value)
        mParticulars = "STDS DEDUCT ON RS. " & VB6.Format(txtSTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtSTDSRate.Text, "0.000") & "%"
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
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', " & mAccountCode & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
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
        Dim mLocationID As String
        Dim mAccountCode As String = "-1"
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
            SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf & " MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE ) VALUES ( " & vbCrLf & " '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf & " " & pAccountCode & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "
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

        mLocationID = GetDefaultLocation(mAccountCode)

        If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, mLocationID) = False Then GoTo ErrDetail
        UpdateSuppPRDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateSuppPRDetail = False
        'Resume							
    End Function
    Private Function UpdateTDSCreditDetail(ByRef pMKey As String, ByRef pVNoStr As String, ByRef pBookType As String, ByRef pBookSubType As String) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String
        Dim mTDSAccountCode As String
        Dim mExempted As String
        Dim mSectionCode As Integer
        Dim mAmountPaid As Double
        Dim mTdsRate As Double
        Dim mPartyName As String
        Dim mPartyCode As String
        SqlStr = ""
        SqlStr = "DELETE FROM TDS_TRN WHERE MKey= '" & pMKey & "'"
        PubDBCn.Execute(SqlStr)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked Or chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            UpdateTDSCreditDetail = True
            Exit Function
        End If
        mTDSAccountCode = IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "", RsCompany.Fields("TDSCREDITACCOUNT").Value)
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
        If MainClass.ValidateWithMasterTable(mPartyCode, "SUPP_CUST_CODE", "SECTIONCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSectionCode = MasterNo
        Else
            mSectionCode = CInt("-1")
        End If
        mAmountPaid = Val(CStr(CDbl(txtTDSDeductOn.Text)))
        mTdsRate = Val(txtTDSRate.Text)
        mExempted = "N"
        If ADDMode = True Then
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
    Private Function UpdateDnCnMain(ByRef xPURVNO As String, ByRef xPURVDate As String, ByRef xBillNo As String, ByRef xBillDate As String, ByRef xMRRNo As String, ByRef xMRRDate As String, ByRef xPoNo As String, ByRef xPODate As String, ByRef xCreditDays1 As Integer, ByRef xCreditDays2 As Integer, ByRef xItemDesc As String, ByRef xDnCnType As String, ByRef xCancelled As String, ByRef xBookCode As Integer, ByRef xBookType As String, ByRef xBookSubType As String, ByRef xDebitAccountCode As String, ByRef xCreditAccountCode As String, ByRef xPayDate As String, ByRef xApproved As String, ByRef xDNCNNO As Integer, ByRef pExpDiffDN As Boolean, ByRef mDivisionCode As Double, ByRef cntRow As Integer, Optional ByRef xAmount As Double = 0) As Boolean
        On Error GoTo ErrPart
        Dim xMKey As String
        Dim xCurRowNo As Integer
        Dim SqlStr As String
        Dim xVNoPrefix As String
        Dim xVTYPE As String
        Dim xVNoSeq As Double
        Dim xVNoSuffix As String
        Dim xVNo As String
        Dim xVDate As String
        Dim xItemValue As Double
        Dim xSTPERCENT As Double
        Dim xTOTSTAMT As Double
        Dim xTOTFREIGHT As Double
        Dim xTOTCHARGES As Double
        Dim xEDPERCENT As Double
        Dim xTotEDAmount As Double
        Dim xSURAmount As Double
        Dim xTotDiscount As Double
        Dim xMSC As Double
        Dim xRO As Double
        Dim xTOTEXPAMT As Double
        Dim xTOTTAXABLEAMOUNT As Double
        Dim xNETVALUE As Double
        Dim xTotQty As Double
        Dim xNarration As String
        Dim xReason As String
        Dim nBookCode As String
        Dim RsTemp As ADODB.Recordset
        Dim mPDIRItem As String
        Dim mExpAmount As Double
        Dim mDNFROM As String
        'Dim RsTemp As ADODB.Recordset							
        Dim xCessAmt As Double
        Dim xSHECessAmt As Double
        Dim xTotServiceAmt As Double
        Dim xSTClaimNo As String
        Dim xSTClaimPer As Double
        Dim xSTClaimAmount As Double
        Dim xSTClaimDate As String
        Dim xISSTRefund As String
        Dim xSURVATClaimAmount As Double
        Dim xTOTSURCHARGEAMT As Double
        Dim xTOTVATCLAIMAMT As Double
        Dim pSuppCustCode As String
        Dim pAccountCode As String
        Dim mSubRow As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mHSNCode As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim pDNSeqType As Integer
        If xCancelled = "Y" Then UpdateDnCnMain = True : Exit Function
        If xDnCnType = "R" Then
            xVTYPE = IIf(xBookCode = ConDebitNoteBookCode, "DR", "CR")
            mDNFROM = "M"
        Else
            xVTYPE = IIf(xBookCode = ConDebitNoteBookCode, "DN", "CN")
            mDNFROM = "P"
        End If
        xSTClaimNo = ""
        xSTClaimPer = 0
        xSTClaimAmount = 0
        xSTClaimDate = ""
        xISSTRefund = "N"
        xSURVATClaimAmount = CDbl("N")
        xItemValue = 0
        xSTPERCENT = 0
        xTOTSTAMT = 0
        xTOTFREIGHT = 0
        xTOTCHARGES = 0
        xEDPERCENT = 0
        xTotEDAmount = 0
        xSURAmount = 0
        xTotDiscount = 0
        xMSC = 0
        xRO = 0
        xTOTEXPAMT = 0
        xTOTTAXABLEAMOUNT = 0
        xNETVALUE = 0
        xTotQty = 0
        xNarration = ""
        xTOTSURCHARGEAMT = 0
        xTOTVATCLAIMAMT = 0
        If xDnCnType = "R" Then
            xReason = "REJECTION"
            pDNSeqType = 4
        ElseIf xDnCnType = "S" Then
            xReason = "SHORTAGE"
            pDNSeqType = 1
        ElseIf xDnCnType = "P" Then
            xReason = "RATE DIFF"
            pDNSeqType = 2
        ElseIf xDnCnType = "V" Then
            xReason = "VOLUME DISCOUNT"
            pDNSeqType = 5
        ElseIf xDnCnType = "O" Then
            xNETVALUE = xAmount
            xReason = "PDIR NOT RECEVIED."
            xNarration = "PDIR NOT RECEVIED."
            pDNSeqType = 7
            SqlStr = "SELECT ITEM_CODE " & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " WHERE " & vbCrLf & " AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND PDIR_FLAG='N'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    mPDIRItem = IIf(mPDIRItem = "", "", mPDIRItem & ",") & IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    RsTemp.MoveNext()
                Loop
                xNarration = xNarration & " AGT. ITEM CODE  : " & mPDIRItem & " ( Rs. 200/- each)"
            End If
        End If
        xVNoSeq = CDbl(AutoGenDNCNNo("VNOSEQ", xBookCode, xVTYPE, pDNSeqType))
        ''xVNoSeq = xDNCNNO + AutoGenDNCNNo("VNOSEQ", xBookCode, xVTYPE)							
        xVNoPrefix = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")
        xVNoSuffix = ""
        xVNo = Trim(xVTYPE) & Trim(xVNoPrefix) & VB6.Format(Val(CStr(xVNoSeq)), "00000") & Trim(xVNoSuffix)
        xVDate = txtVDate.Text
        SqlStr = ""
        If ADDMode = True Then
            xCurRowNo = MainClass.AutoGenRowNo("FIN_DNCN_HDR", "RowNo", PubDBCn)
            xMKey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & xCurRowNo
            SqlStr = "INSERT INTO FIN_DNCN_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, ROWNO, " & vbCrLf & " VNOPREFIX, VTYPE,VNOSEQ, VNOSUFFIX, " & vbCrLf & " VNO, VDATE, BILLNO, INVOICE_DATE, " & vbCrLf & " DEBITACCOUNTCODE, CREDITACCOUNTCODE, DUEDAYSFROM, DUEDAYSTO, " & vbCrLf & " BOOKCODE, BookType, BOOKSUBTYPE, REMARKS,  " & vbCrLf & " ITEMDESC, REASON, ITEMVALUE, STPERCENT,  " & vbCrLf & " TOTSTAMT, TOTFREIGHT, TOTCHARGES, EDPERCENT,  " & vbCrLf & " TOTEDAMOUNT, TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, " & vbCrLf & " TOTRO, TOTEXPAMT, TOTTAXABLEAMOUNT, NETVALUE, " & vbCrLf & " TOTQTY, CANCELLED, NARRATION, DNCNTYPE, APPROVED, PAYDATE, DNCNFROM, " & vbCrLf & " PURVNO, PURVDATE, AUTO_KEY_MRR, MRRDATE, " & vbCrLf & " CUSTREFNO, CUSTREFDATE, MODVATNO, MODVATDATE, " & vbCrLf & " MODVATPER, MODVATAMOUNT, STCLAIMNO, STCLAIMPER, " & vbCrLf & " STCLAIMAMOUNT, STCLAIMDATE, ISMODVAT, ISSTREFUND, " & vbCrLf & " ISDESPATCHED, SALEINVOICENO, SALEINVOICEDATE, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM,SUR_VATCLAIMAMOUNT, DIV_CODE,ISGSTREFUND, DNCNSEQTYPE) "
            SqlStr = SqlStr & vbCrLf & " VALUES('" & xMKey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & xCurRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(xVNoPrefix) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(xVTYPE) & "', " & vbCrLf & " " & xVNoSeq & ", '" & MainClass.AllowSingleQuote(xVNoSuffix) & "', '" & MainClass.AllowSingleQuote(xVNo) & "',TO_DATE('" & VB6.Format(xVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(xBillNo) & "', TO_DATE('" & VB6.Format(xBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & xDebitAccountCode & "','" & xCreditAccountCode & "', " & Val(CStr(xCreditDays1)) & ", " & Val(CStr(xCreditDays2)) & ", " & vbCrLf & " '" & xBookCode & "', '" & xBookType & "', '" & xBookSubType & "', ''," & vbCrLf & " '" & MainClass.AllowSingleQuote(xItemDesc) & "', '" & MainClass.AllowSingleQuote(xReason) & "', " & vbCrLf & " " & xItemValue & ", " & xSTPERCENT & ", " & xTOTSTAMT & ", " & xTOTFREIGHT & ", " & xTOTCHARGES & "," & vbCrLf & " " & xEDPERCENT & ", " & xTotEDAmount & ", " & xSURAmount & ", " & xTotDiscount & "," & xMSC & ", " & vbCrLf & " " & xRO & ", " & xTOTEXPAMT & ", " & xTOTTAXABLEAMOUNT & ", " & xNETVALUE & ", " & vbCrLf & " " & xTotQty & ", '" & xCancelled & "', '" & MainClass.AllowSingleQuote(xNarration) & "', " & vbCrLf & " '" & xDnCnType & "', '" & xApproved & "', TO_DATE('" & VB6.Format(xPayDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mDNFROM & "', " & vbCrLf & " '" & xPURVNO & "', TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & xMRRNo & "', TO_DATE('" & VB6.Format(xMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '', '', '', '', " & vbCrLf & " 100, 0, '" & xSTClaimNo & "', " & xSTClaimPer & ", " & vbCrLf & " " & xSTClaimAmount & ", TO_DATE('" & VB6.Format(xSTClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'N', '" & xISSTRefund & "'," & vbCrLf & " 'N','',''," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N'," & Val(CStr(xSURVATClaimAmount)) & "," & mDivisionCode & "," & vbCrLf & "'" & IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N") & "'), " & pDNSeqType & ""
        End If
        PubDBCn.Execute(SqlStr)
        UpdateDnCnMain = True
        If xDnCnType = "O" Then
            If UpdateDNCNPDIRExp1(xMKey, xAmount) = False Then GoTo ErrPart
        Else
            If UpdateDNCNDetail1(xDnCnType, xMKey, xVTYPE, xPURVNO, xPURVDate, cntRow) = False Then GoTo ErrPart
        End If
        If (xDnCnType = "P" Or xDnCnType = "R" Or xDnCnType = "S" Or xDnCnType = "V") Then 'And pExpDiffDN = False							
            If UpdateDNCNRateDiffExp1(xDnCnType, xMKey, pExpDiffDN) = False Then GoTo ErrPart
        End If
        If xApproved = "Y" Then
            If UpdateDNCNHDRAPP(xMKey, xDnCnType) = False Then GoTo ErrPart
            nBookCode = CStr(xBookCode)
            SqlStr = "SELECT NETVALUE, TOTEXPAMT, TOTEDAMOUNT,TOTSTAMT,TOTSURCHARGEAMT,SUR_VATCLAIMAMOUNT " & vbCrLf & " FROM FIN_DNCN_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & xMKey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                xAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mExpAmount = IIf(IsDBNull(RsTemp.Fields("TOTEXPAMT").Value), 0, RsTemp.Fields("TOTEXPAMT").Value)
                xTotEDAmount = IIf(IsDBNull(RsTemp.Fields("TOTEDAMOUNT").Value), 0, RsTemp.Fields("TOTEDAMOUNT").Value)
                xTOTSTAMT = IIf(IsDBNull(RsTemp.Fields("TOTSTAMT").Value), 0, RsTemp.Fields("TOTSTAMT").Value)
                xTOTSURCHARGEAMT = IIf(IsDBNull(RsTemp.Fields("TOTSURCHARGEAMT").Value), 0, RsTemp.Fields("TOTSURCHARGEAMT").Value)
                xTOTVATCLAIMAMT = IIf(IsDBNull(RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value), 0, RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value)
            End If
            SqlStr = "Select IX.AMOUNT,IDENTIFICATION " & vbCrLf & " From FIN_DNCN_EXP IX,FIN_INTERFACE_MST IMST" & vbCrLf & " Where " & vbCrLf & " IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND " & vbCrLf & " IX.ExpCode=IMST.Code " & vbCrLf & " AND IX.Mkey='" & xMKey & "'" & vbCrLf & " AND IDENTIFICATION IN ('EDU','SER','SHC')"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    If RsTemp.Fields("Identification").Value = "EDU" Then
                        xCessAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                    End If
                    If RsTemp.Fields("Identification").Value = "SER" Then
                        xTotServiceAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                    End If
                    If RsTemp.Fields("Identification").Value = "SHC" Then
                        xSHECessAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                    End If
                    RsTemp.MoveNext()
                Loop
            End If
            pSuppCustCode = IIf(CDbl(nBookCode) = ConDebitNoteBookCode, xDebitAccountCode, xCreditAccountCode)
            pAccountCode = IIf(CDbl(nBookCode) = ConDebitNoteBookCode, xCreditAccountCode, xDebitAccountCode)
            Dim mLocationID As String = GetDefaultLocation(pSuppCustCode)
            If DNCNPostTRNGST(PubDBCn, xMKey, xCurRowNo, nBookCode, xBookType, xBookSubType, xVTYPE, xVNo, xVDate, xBillNo, xBillDate, xDebitAccountCode, xCreditAccountCode, Val(CStr(xAmount)), IIf(xCancelled = "Y", True, False), xPayDate, "", xReason, Val(CStr(mExpAmount)), ADDMode, mAddUser, mAddDate, mDivisionCode, "N", 0, 0, 0, xDnCnType, mLocationID) = False Then GoTo ErrPart

            SqlStr = " SELECT SUBROWNO, ITEM_CODE, HSNCODE, ITEM_QTY,ITEM_UOM, ITEM_RATE, ITEM_AMT, " & vbCrLf & " CGST_PER,SGST_PER,IGST_PER, " & vbCrLf & " CGST_AMOUNT,SGST_AMOUNT,IGST_AMOUNT " & vbCrLf & " FROM FIN_DNCN_DET" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & xMKey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mSubRow = IIf(IsDBNull(RsTemp.Fields("SUBROWNO").Value), 0, RsTemp.Fields("SUBROWNO").Value)
                    mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "INV_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo
                    Else
                        mItemDesc = ""
                    End If
                    mHSNCode = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)
                    mQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                    mUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                    mRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                    mAmount = IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value)
                    mCGSTPer = IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value)
                    mSGSTPer = IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value)
                    mIGSTPer = IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value)
                    mCGSTAmount = IIf(IsDBNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value)
                    mSGSTAmount = IIf(IsDBNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value)
                    mIGSTAmount = IIf(IsDBNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value)
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                        If UpdateGSTTRN(PubDBCn, xMKey, nBookCode, xBookType, xBookSubType, xVNo, VB6.Format(xVDate, "DD-MMM-YYYY"), Trim(xBillNo), VB6.Format(xBillDate, "DD-MMM-YYYY"), "", "", pSuppCustCode, pAccountCode, "Y", pSuppCustCode, mSubRow, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", xDnCnType, IIf(lblPurchaseType.Text = "G", "G", "S"), IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N"), IIf(xBookCode = ConDebitNoteBookCode, "D", "C"), (lblGSTClaimDate.Text), "N") = False Then GoTo ErrPart
                    End If
                    RsTemp.MoveNext()
                Loop
            End If
        End If
        pDnCnNo = IIf(pDnCnNo = "", "", pDnCnNo & ", ") & xVNo
        Exit Function
ErrPart:
        UpdateDnCnMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Function UpdateDNCNExp1(ByRef xKey As String) As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim SqlStr As String
        PubDBCn.Execute("Delete From FIN_DNCN_EXP Where Mkey='" & xKey & "'")
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
                .Col = ColExpDebitAmt
                mExpAmount = Val(.Text)
                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    '                mExpAmount = mExpAmount * -1							
                End If
                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                SqlStr = ""
                If mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "'," & I & ", " & vbCrLf & " " & mExpCode & ",0," & mExpAmount & ", " & vbCrLf & " " & mExpAmount & ",'N')"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdateDNCNExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDNCNExp1 = False
    End Function
    Private Function UpdateDNCNRateDiffExp1(ByRef pDnCnType As String, ByRef xKey As String, ByRef pExpDiffDN As Boolean) As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset
        Dim mItemValue As Double
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim SqlStr As String
        Dim mSql As String
        Dim mIdentification As String
        Dim mTaxableAmount As Double
        Dim mCESSableAmount As Double
        Dim mTaxAmount As Double
        PubDBCn.Execute("Delete From FIN_DNCN_EXP Where Mkey='" & xKey & "'")
        SqlStr = "SELECT SUM(ITEM_AMT) AS ITEM_AMT FROM FIN_DNCN_DET Where Mkey='" & xKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mItemValue = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value), "0.00"))
        End If
        mTaxableAmount = mItemValue
        With SprdExp
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColExpName
                mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                If MainClass.ValidateWithMasterTable(.Text, "Name", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                    mIdentification = MasterNo
                End If
                If pDnCnType = "P" Or pDnCnType = "V" Then
                    mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STTYPE='C'"
                    If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                        mExpCode = MasterNo
                    Else
                        mExpCode = -1
                    End If
                ElseIf pDnCnType = "S" Then
                    If mIdentification = "ST" Then
                        mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND  STTYPE='C'"
                        If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                            mExpCode = MasterNo
                        Else
                            mExpCode = -1
                        End If
                    Else
                        mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                        If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                            mExpCode = MasterNo
                        Else
                            mExpCode = -1
                        End If
                    End If
                Else
                    mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='S' OR Type='B')"
                    If mIdentification = "ED" Then
                    Else
                        If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                            mExpCode = MasterNo
                        Else
                            mExpCode = -1
                        End If
                    End If
                End If
                .Col = ColExpPercent
                mPercent = Val(.Text)
                .Col = ColExpAmt
                If Val(.Text) <> 0 And mPercent = 0 Then
                    If Val(lblTotItemValue.Text) = 0 Then
                        mExpAmount = 0
                    Else
                        mExpAmount = mItemValue * Val(.Text) / Val(lblTotItemValue.Text)
                    End If
                    Select Case mIdentification
                        Case "ED"
                            mCESSableAmount = mExpAmount
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount) 'mTaxableAmount + mExpAmount							
                        Case "EDU"
                            mExpAmount = mCESSableAmount * mPercent / 100
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount) 'mTaxableAmount + mExpAmount							
                        Case "SHC"
                            mExpAmount = mCESSableAmount * mPercent / 100
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount)
                        Case "SER"
                            mCESSableAmount = mExpAmount
                            mTaxableAmount = mItemValue + mExpAmount
                        Case "ST"
                            mExpAmount = mTaxableAmount * mPercent / 100
                            mTaxAmount = mTaxableAmount * mPercent / 100
                        Case "SUR"
                            mExpAmount = mTaxAmount * mPercent / 100
                        Case Else
                            mExpAmount = mTaxableAmount * mPercent / 100
                    End Select
                ElseIf Val(.Text) <> 0 And mPercent <> 0 Then
                    '                mExpAmount = mItemValue * mPercent / 100							
                    Select Case mIdentification
                        Case "ED"
                            mExpAmount = mItemValue * mPercent / 100
                            mCESSableAmount = mExpAmount
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount) 'mTaxableAmount + mExpAmount							
                        Case "EDU"
                            mExpAmount = mCESSableAmount * mPercent / 100
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount) 'mTaxableAmount + mExpAmount							
                        Case "SHC"
                            mExpAmount = mCESSableAmount * mPercent / 100
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount)
                        Case "SER"
                            mExpAmount = mItemValue * mPercent / 100
                            mCESSableAmount = mExpAmount
                            mTaxableAmount = mItemValue + mExpAmount
                        Case "ST"
                            mExpAmount = mTaxableAmount * mPercent / 100
                            mTaxAmount = mTaxableAmount * mPercent / 100
                        Case "SUR"
                            mExpAmount = mTaxAmount * mPercent / 100
                        Case Else
                            mExpAmount = mTaxableAmount * mPercent / 100
                    End Select
                End If
                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    '                mExpAmount = mExpAmount * -1							
                End If
                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                If pExpDiffDN = True Then
                    mPercent = 0
                    .Col = ColExpDebitAmt
                    If Val(.Text) <> 0 Then
                        mExpAmount = mExpAmount + Val(.Text)
                        .Col = ColExpName
                        If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mExpCode = MasterNo
                        Else
                            mExpCode = -1
                        End If
                    End If
                End If
                If mRO = "Y" Then
                    mExpAmount = System.Math.Round(mExpAmount, 0)
                End If
                SqlStr = ""
                If mExpAmount <> 0 And mExpCode <> -1 Then
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mItemValue & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                End If
                mExpAmount = 0
            Next I
        End With
        UpdateDNCNRateDiffExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDNCNRateDiffExp1 = False
    End Function
    Private Function UpdateDNCNPDIRExp1(ByRef xKey As String, ByRef xAmount As Double) As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim SqlStr As String
        PubDBCn.Execute("Delete From FIN_DNCN_EXP Where Mkey='" & xKey & "'")
        If IsDBNull(RsCompany.Fields("PDIR_ACCOUNT").Value) Then
            MsgBox("PDIR Account Missing, Please Call Administrator....")
            UpdateDNCNPDIRExp1 = False
        End If
        mExpCode = IIf(IsDBNull(RsCompany.Fields("PDIR_ACCOUNT").Value), "-1", RsCompany.Fields("PDIR_ACCOUNT").Value)
        mPercent = 0
        mExpAmount = xAmount
        mCalcOn = xAmount
        mRO = "N"
        SqlStr = ""
        If mCalcOn <> 0 Or mExpAmount <> 0 Then
            SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "',1, " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "')"
            PubDBCn.Execute(SqlStr)
        End If
        UpdateDNCNPDIRExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDNCNPDIRExp1 = False
    End Function
    Private Function CheckValidVDate(ByRef pBillNoSeq As Integer) As Object
        On Error GoTo CheckERR
        Dim SqlStr As String
        Dim mRsCheck1 As ADODB.Recordset
        Dim mRsCheck2 As ADODB.Recordset
        Dim mBackBillDate As String
        Dim mMaxInvStrfNo As Integer
        CheckValidVDate = True
        If txtBillNo.Text = "000001" Then Exit Function
        SqlStr = "SELECT MAX(INVOICE_DATE)" & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND BookSubType='" & mBookSubType & "' " & vbCrLf & " AND BillNoSeq<" & Val(CStr(pBillNoSeq)) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If
        SqlStr = "SELECT MIN(INVOICE_DATE)" & " FROM FIN_INVOICE_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' " & vbCrLf & " AND BookSubType='" & mBookSubType & "' " & vbCrLf & " AND BillNoSeq>" & Val(CStr(pBillNoSeq)) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Bill Date Is Greater Than The BillDate Of Next InvoiceNo.")
                CheckValidVDate = False
            ElseIf CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Bill Date Is Less Than The BillDate Of Previous InvoiceNo.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Bill Date Is Greater Than The BillDate Of Next InvoiceNo.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Bill Date Is Less Than The BillDate Of Previous InvoiceNo.")
                CheckValidVDate = False
            End If
        End If
        Exit Function
CheckERR:
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
            mLastBillDate = IIf(IsDBNull(RsTemp.Fields("VDATE").Value), mLastBillDate, RsTemp.Fields("VDATE").Value)
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
    Private Function AutoGenSeqBillNo(ByRef mFieldName As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingNo As Integer, ByRef mDivisionCode As Double) As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsPurchMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Integer
        Dim SqlStr As String
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset
        Dim mMAxNo As Double

        SqlStr = ""
        If lblPurchaseType.Text = "G" Then
            If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
                pStartingNo = 50001
            Else
                pStartingNo = 1
            End If
        ElseIf lblPurchaseType.Text = "J" Then
            pStartingNo = 70001
        ElseIf lblPurchaseType.Text = "W" Or lblPurchaseType.Text = "R" Then
            pStartingNo = 90001
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
        '							
        '    End If							
        '							
        SqlStr = ""
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookType='" & mBookType & "'"
        If mFieldName = "VNOSEQ" Then
            '        SqlStr = SqlStr & vbCrLf & " AND BOOKSUBTYPE <> 'W'"							
            '							
            '        SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE='" & lblPurchaseType.text & "'"							
            If lblPurchaseType.Text = "G" Then
                If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = SqlStr & vbCrLf & "AND BOOKSUBTYPE='R'"
                Else
                    SqlStr = SqlStr & vbCrLf & "AND BOOKSUBTYPE<>'R'"
                End If
                SqlStr = SqlStr & vbCrLf & " AND (FIN_PURCHASE_HDR.PURCHASE_TYPE= 'G' OR FIN_PURCHASE_HDR.PURCHASE_TYPE= '' OR FIN_PURCHASE_HDR.PURCHASE_TYPE IS NULL)"
            ElseIf lblPurchaseType.Text = "W" Or lblPurchaseType.Text = "R" Then
                SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE IN ('W','R')"
            Else
                SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE='" & lblPurchaseType.Text & "'"
            End If
            '(FIN_PURCHASE_HDR.PURCHASE_TYPE= 'G' OR FIN_PURCHASE_HDR.PURCHASE_TYPE= '' OR FIN_PURCHASE_HDR.PURCHASE_TYPE IS NULL)							
            '        If mSeparateSeries = "Y" Then							
            '            SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""							
            '        End If							
        ElseIf mFieldName = "GST_CLAIM_NO" Then
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
        Dim mMaxNo As Double
        SqlStr = ""
        xFyear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))
        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            pStartingNo = CDbl(xFyear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(lblPurchaseSeqType.Text) & VB6.Format(pStartingNo, "00000"))
        Else
            pStartingNo = (Val(lblPurchaseSeqType.Text) * 100000) + 1
        End If
        SqlStr = ""
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookType='" & mBookType & "'"
        If mFieldName = "VNOSEQ" Then
            SqlStr = SqlStr & vbCrLf & "AND PURCHASESEQTYPE='" & lblPurchaseSeqType.Text & "'"
        ElseIf mFieldName = "GST_CLAIM_NO" Then
            SqlStr = SqlStr & vbCrLf & " AND MODVAT_TYPE =1 AND ISGSTAPPLICABLE ='Y'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchMainGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMaxNo <= 0 Then
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
    Private Function AutoGenDNCNNo(ByRef mFieldName As String, ByRef pBookCode As Integer, ByRef pVType As String, ByRef pDNSeqType As Integer) As String
        On Error GoTo AutoGenNoErr
        Dim RsGen As ADODB.Recordset
        Dim mNewDNCNNo As Double
        Dim SqlStr As String
        Dim mStartingNo As Double
        Dim xFyear As Integer
        Dim mMaxNo As Double
        SqlStr = ""
        xFyear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))
        mStartingNo = 1
        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            mStartingNo = CDbl(xFyear & Val(CStr(pDNSeqType)) & VB6.Format(mStartingNo, "00000"))
        End If
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_DNCN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookCode='" & pBookCode & "' AND VType='" & pVType & "'"
        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            SqlStr = SqlStr & vbCrLf & " AND DNCNSEQTYPE=" & Val(CStr(pDNSeqType)) & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMaxNo <= 0 Then
                    mNewDNCNNo = mStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewDNCNNo = .Fields(0).Value + 1
                Else
                    mNewDNCNNo = mStartingNo
                End If
            Else
                mNewDNCNNo = mStartingNo
            End If
        End With
        AutoGenDNCNNo = CStr(mNewDNCNNo)
        Exit Function
AutoGenNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMRRMain() As Boolean
        On Error GoTo UpdateDCErr
        Dim xMRRNo As Double
        Dim SqlStr As String
        Dim cntRow As Integer
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColMRRNo
                xMRRNo = Val(.Text)
                SqlStr = ""
                SqlStr = "UPDATE INV_GATE_HDR SET "
                If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        SqlStr = SqlStr & vbCrLf & " GST_STATUS='Y',"
                    Else
                        SqlStr = SqlStr & vbCrLf & " GST_STATUS='N',"
                    End If
                ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        SqlStr = SqlStr & vbCrLf & " MRR_FINAL_FLAG='Y',"
                    Else
                        SqlStr = SqlStr & vbCrLf & " MRR_FINAL_FLAG='N',"
                    End If
                End If
                SqlStr = SqlStr & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') "
                SqlStr = SqlStr & vbCrLf & " WHERE AUTO_KEY_MRR=" & Val(CStr(xMRRNo)) & " " & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
                PubDBCn.Execute(SqlStr)
            Next
        End With
        UpdateMRRMain = True
        Exit Function
UpdateDCErr:
        UpdateMRRMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function DeletePrevious() As Boolean
        On Error GoTo UpdateDCErr
        Dim SqlStr As String
        DeletePrevious = True
        If Trim(lblPMKey.Text) = "" Then Exit Function
        SqlStr = "DELETE FROM FIN_PURCHASE_EXP WHERE MKEY='" & lblPMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "DELETE FROM FIN_PURCHASE_DET WHERE MKEY='" & lblPMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        '    Sqlstr = "DELETE FROM FIN_PURCHASE_TRN WHERE MKEY='" & lblPMKey.text & "'"							
        '    PubDBCn.Execute Sqlstr							
        SqlStr = "Delete From FIN_GST_POST_TRN Where Mkey='" & lblPMKey.Text & "' AND BookType='" & UCase(mBookType) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "DELETE FROM FIN_PURCHASE_VNO_MST WHERE MKEY='" & lblPMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "DELETE FROM FIN_PURCHASE_HDR WHERE MKEY='" & lblPMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        Exit Function
UpdateDCErr:
        DeletePrevious = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDetail1(ByRef xNarration As String, ByRef pAccountCode As String, ByRef pVNo As String, ByRef pSuppCustCode As String, ByRef pShipToSameParty As String, ByRef pShipToSuppCustCode As String, ByRef pDivCode As Double, ByRef pSaleBillNo As String, ByRef pSaleBillDate As String) As Boolean
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
        Dim mRefType As String
        Dim xSuppCustCode As String
        Dim mGSTableAmount As Double
        Dim mItemAdvCGST As Double
        Dim mItemAdvSGST As Double
        Dim mItemAdvIGST As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mMRRNO As Double
        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        xIsCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        xIsFOC = IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        xIsModvat = "N" ''IIf(chkModvat.Value = vbChecked, "Y", "N")							
        xISSTRefund = "N"
        xISCSTRefund = "N"
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION ", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION ='J'") = True Then
            mIsJobWork = "Y"
        Else
            mIsJobWork = "N"
        End If
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "ISSALERETURN ", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSALERETURN ='Y' AND CATEGORY='P'") = True Then
            mIsSaleReturn = "Y"
        Else
            mIsSaleReturn = "N"
        End If
        mTotExicseableAmt = 0
        mTotSTableAmt = 0
        mTotCessableAmt = 0
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "'")
        PubDBCn.Execute("DELETE FROM FIN_PURCHASE_TRN WHERE MKEY='" & LblMKey.Text & "'")
        PubDBCn.Execute("Delete From FIN_PURCHASE_DET Where Mkey='" & LblMKey.Text & "'")
        mPOS = ""
        If pShipToSameParty = "N" Then
            If MainClass.ValidateWithMasterTable(pShipToSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mState = MasterNo
                If MainClass.ValidateWithMasterTable(mState, "NAME", "STATE_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mPOS = MasterNo
                End If
            End If
        End If
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColMRRNo
                mMRRNO = Val(.Text)
                If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mRefType = MasterNo
                End If
                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                xNarration = xNarration & IIf(xNarration = "", "", ", ") & mItemDesc
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColQty
                mQty = Val(.Text)
                .Col = ColShortageQty
                mShortageQty = Val(.Text)
                .Col = ColRejectedQty
                mRejectQty = Val(.Text)
                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)
                .Col = ColRate
                mRate = Val(.Text)
                mEDRate = 0
                .Col = ColAmount
                mAmount = Val(.Text)
                .Col = ColTaxableAmount
                mGSTableAmount = Val(.Text)
                .Col = ColPONo
                mPONo = Trim(.Text)
                If mIsSaleReturn = "Y" Then
                    mPODate = GetSaleInvoiceDate(I, Val(CStr(mMRRNO)), mPONo, mItemCode, PubDBCn)
                    If mPODate = "" Then
                        MsgInformation("Sale Invoice No is not Valid of Item Code : " & mItemCode)
                        UpdateDetail1 = False
                        Exit Function
                    End If
                End If
                If mTotExicseableAmt = 0 Then
                    mExicseableAmt = 0
                    mCessableAmt = 0
                Else
                    mExicseableAmt = 0 ' Format((Val(lblTotED.text) * mAmount) / mTotExicseableAmt, "0.00")							
                    mCessableAmt = 0 ' mExicseableAmt							
                End If
                If Val(lblTotItemValue.Text) = 0 Then
                    mServiceAmt = 0
                Else
                    mServiceAmt = 0 '  Format((Val(lblServiceAmount.text) * mAmount) / Val(lblTotItemValue.text), "0.00")							
                    mCessableAmt = 0 '  mCessableAmt + mServiceAmt							
                End If
                If mTotCessableAmt = 0 Then
                    mCESSAmt = 0
                Else
                    mCESSAmt = 0 'Format((Val(lblEDUAmount.text) * mCessableAmt) / mTotCessableAmt, "0.00")							
                End If
                If mTotCessableAmt = 0 Then
                    mSHECAmt = 0
                Else
                    mSHECAmt = 0 ' Format((Val(lblSHEC.text) * mCessableAmt) / mTotCessableAmt, "0.00")							
                End If
                If mTotSTableAmt = 0 Then
                    mSTableAmt = 0
                Else
                    mSTableAmt = 0 '  Format((Val(lblTotST.text) * (mAmount + mExicseableAmt + mCESSAmt)) / mTotSTableAmt, "0.00")							
                End If
                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)
                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)
                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)
                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)
                If Val(txtTotCGSTRefund.Text) = 0 Then
                    mItemAdvCGST = 0
                Else
                    mItemAdvCGST = mCGSTAmount * Val(txtAdvCGST.Text) / Val(txtTotCGSTRefund.Text)
                End If
                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)
                If Val(txtTotSGSTRefund.Text) = 0 Then
                    mItemAdvSGST = 0
                Else
                    mItemAdvSGST = mSGSTAmount * Val(txtAdvSGST.Text) / Val(txtTotSGSTRefund.Text)
                End If
                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)
                If Val(txtTotIGSTRefund.Text) = 0 Then
                    mItemAdvIGST = 0
                Else
                    mItemAdvIGST = mIGSTAmount * Val(txtAdvIGST.Text) / Val(txtTotIGSTRefund.Text)
                End If
                If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                    .Col = ColInvType
                    pInvType = Trim(.Text)
                    If pInvType = "" Then
                        pInvType = Trim(cboInvType.Text)
                        mDebitAccountCode = pAccountCode
                    Else
                        mDebitAccountCode = GetDebitNameOfInvType(pInvType, "N")
                        If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                            If mDebitAccountCode = "-1" Then MsgBox("Account Code not Defined For Item Code : " & mItemCode) : GoTo UpdateDetail1
                        End If
                    End If
                    If MainClass.ValidateWithMasterTable(pInvType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mInvTypeCode = MasterNo
                    Else
                        MsgBox("Invoice Type Does Not Exist In Master", MsgBoxStyle.Information)
                        GoTo UpdateDetail1
                    End If
                End If
                SqlStr = ""
                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO FIN_PURCHASE_DET ( " & vbCrLf & " MKEY , SUBROWNO, MRRNO, " & vbCrLf & " ITEM_CODE , ITEM_DESC, CUSTOMER_PART_NO, HSNCODE, ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT, " & vbCrLf & " ITEM_ED, ITEM_ST, ITEM_CESS, SHORTAGE_QTY,REJECTED_QTY," & vbCrLf & " CUST_REF_NO, CUST_REF_DATE, COMPANY_CODE,ITEM_SHEC, " & vbCrLf & " PUR_ACCOUNT_CODE,ITEM_ED_PER,ITEM_TRNTYPE, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT,GSTABLE_AMT ) "
                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "'," & I & ", " & mMRRNO & "," & vbCrLf & " '" & mItemCode & "','" & MainClass.AllowSingleQuote(mItemDesc) & "','" & mPartNo & "','" & mHSNCode & "', " & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & vbCrLf & " " & mExicseableAmt & ", " & mSTableAmt & ", " & vbCrLf & " " & mCESSAmt & "," & mShortageQty & "," & mRejectQty & ", " & vbCrLf & " '" & mPONo & "',TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mSHECAmt)) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mDebitAccountCode) & "'," & Val(CStr(mEDRate)) & ", " & mInvTypeCode & ", " & vbCrLf & " " & mCGSTPer & "," & mSGSTPer & "," & mIGSTPer & "," & vbCrLf & " " & mCGSTAmount & "," & mSGSTAmount & "," & mIGSTAmount & "," & mGSTableAmount & ") "
                    PubDBCn.Execute(SqlStr)
                    mApprovedQty = mQty - mShortageQty - mRejectQty
                    If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked And mApprovedQty > 0 And mIsJobWork = "N" And mIsSaleReturn = "N" Then
                        If FinancePurchaseTRN((LblMKey.Text), xIsCancelled, xIsFOC, Val(CStr(mMRRNO)), VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY"), Trim(txtBillNo.Text), VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text), VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), CInt(LblBookCode.Text), xIsModvat, xISSTRefund, xISCSTRefund, I, mItemCode, mUnit, mApprovedQty, mRate, Val(CStr(mExicseableAmt)), mCESSAmt, mSHECAmt, mSTableAmt, mOtherAmount) = False Then GoTo UpdateDetail1
                    End If
                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If VB.Left(cboGSTStatus.Text, 1) = "G" And Trim(mPartyGSTNo) <> Trim(mCompanyGSTNo) Then
                            If UpdateGSTTRN(PubDBCn, (LblMKey.Text), LblBookCode.Text, mBookType, mBookSubType, pVNo, VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), Trim(txtBillNo.Text), VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), "", "", pSuppCustCode, pAccountCode, pShipToSameParty, pShipToSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mQty * mRate, mGSTableAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mItemDesc), mPOS, "N", mRefType, IIf(lblPurchaseType.Text = "G", "G", "S"), IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N"), "C", (lblGSTClaimDate.Text), "N") = False Then GoTo UpdateDetail1
                            '                    ElseIf Left(cboGSTStatus.Text, 1) = "R" Then							
                            '                        xSuppCustCode = IIf(IsNull(RsCompany!COMPANY_ACCTCODE), -1, RsCompany!COMPANY_ACCTCODE)							
                            '                        If UpdateGSTTRN(PubDBCn, LblMKey.text, LblBookCode, mBookType, mBookSubType, _							
                            ''                                        pVNo, Format(TxtVDate.Text, "DD-MMM-YYYY"), Trim(pSaleBillNo), Format(pSaleBillDate, "DD-MMM-YYYY"), "", "", _							
                            ''                                        xSuppCustCode, pAccountCode, pShipToSameParty, pShipToSuppCustCode, _							
                            ''                                        I, mItemCode, mQty, mUnit, mRate, _							
                            ''                                        mQty * mRate, mGSTableAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, _							
                            ''                                        mCGSTAmount, mSGSTAmount, mIGSTAmount, _							
                            ''                                        mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mItemDesc), mPOS, "N", _							
                            ''                                        mRefType, IIf(lblPurchaseType.text = "G", "G", "S"), "Y", "D", pSaleBillDate, "N" _							
                            ''                                        ) = False Then GoTo UpdateDetail1:							
                        End If
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        UpdateDetail1 = UpdatePurchaseExp1()
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Function GetNarration() As String
        On Error GoTo UpdateDetail1
        Dim I As Integer
        Dim mItemDesc As String
        Dim xNarration As String
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                xNarration = xNarration & IIf(xNarration = "", "", ", ") & mItemDesc
            Next
        End With
        xNarration = IIf(xNarration = "", "", IIf(mBookSubType = "J", " ( JobWork of :", " ( Cost of :")) & xNarration & IIf(xNarration = "", "", " )")
        GetNarration = VB.Left(xNarration, 250)
        Exit Function
UpdateDetail1:
        GetNarration = ""
    End Function
    Private Function GetBillBalanceAmt(ByRef pSuppCode As String, ByRef pBillNo As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mSql As String
        mSql = " Sum(AMOUNT*DECODE(DC,'D',1,-1))"
        SqlStr = "SELECT " & vbCrLf & "" & mSql & " AS AMOUNT" & vbCrLf & " FROM FIN_POSTED_TRN "
        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND  ACCOUNTCODE='" & MainClass.AllowSingleQuote(Trim(pSuppCode)) & "'" & vbCrLf & "AND BillNo='" & MainClass.AllowSingleQuote(UCase(Trim(pBillNo))) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetBillBalanceAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        Else
            GetBillBalanceAmt = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetBillBalanceAmt = 0
    End Function
    Private Function UpdateDNCNDetail1(ByRef pDnCnType As String, ByRef xKey As String, ByRef pVType As String, ByRef xPURVNO As String, ByRef xPURVDate As String, ByRef cntRow As Integer) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mPORate As Double
        Dim mAmount As Double
        Dim mPONo As String
        Dim mMrrRefType As String
        Dim pItemEDAmount As Double
        Dim pItemSTAmount As Double
        Dim mFactor As Double
        Dim mEDPer As Double
        Dim mEDAmount As Double
        Dim mItemValue As Double
        Dim mExpCode As Double
        Dim mExpName As String
        Dim mHSNCode As String
        Dim mMRRNO As String
        mEDPer = 0
        If pDnCnType = "R" Then
            mEDAmount = 0
            mItemValue = Val(lblTotItemValue.Text)
            For I = 1 To SprdExp.MaxRows
                SprdExp.Row = I
                SprdExp.Col = ColExpName
                mExpName = Trim(SprdExp.Text)
                If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION='ED'") = True Then
                    SprdExp.Row = I
                    SprdExp.Col = ColExpAmt
                    mEDAmount = mEDAmount + Val(SprdExp.Text)
                End If
            Next
            If mItemValue <> 0 Then
                mEDPer = CDbl(VB6.Format(mEDAmount * 100 / mItemValue, "0.00"))
            End If
        End If
        PubDBCn.Execute("Delete From FIN_DNCN_DET Where Mkey='" & xKey & "'")
        With SprdMain
            For I = cntRow To cntRow '' .MaxRows - 1							
                .Row = I
                .Col = ColMRRNo
                mMRRNO = CStr(Val(.Text))
                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)
                .Col = ColHSN
                mHSNCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColPONo
                mPONo = MainClass.AllowSingleQuote(.Text)
                If pDnCnType = "R" Then
                    SqlStr = "SELECT DECODE(ISSUE_UOM,'" & mUnit & "',1,UOM_FACTOR) AS UOM_FACTOR,ISSUE_UOM FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
                    mFactor = 1
                    If RsMisc.EOF = False Then
                        mUnit = IIf(IsDBNull(RsMisc.Fields("ISSUE_UOM").Value), "", RsMisc.Fields("ISSUE_UOM").Value)
                        mFactor = IIf(IsDBNull(RsMisc.Fields("UOM_FACTOR").Value), "", RsMisc.Fields("UOM_FACTOR").Value)
                    End If
                    .Col = ColRejectedQty
                    mQty = Val(.Text) * mFactor
                    mQty = mQty - GetDebitQty(Val(mMRRNO), mItemCode, pDnCnType)
                    .Col = ColRate
                    mRate = Val(.Text) / mFactor
                    If pDnCnType = "R" Then
                        mRate = CDbl(VB6.Format(mRate + (mRate * mEDPer * 0.01), "0.0000"))
                    End If
                    .Col = ColPORate
                    mPORate = Val(.Text) / mFactor
                ElseIf pDnCnType = "S" Then
                    .Col = ColShortageQty
                    mQty = Val(.Text)
                    mQty = mQty - GetDebitQty(Val(mMRRNO), mItemCode, pDnCnType)
                    .Col = ColRate
                    mRate = Val(.Text)
                ElseIf pDnCnType = "V" Then
                    .Col = ColQty
                    mQty = Val(.Text)
                    .Col = ColRate
                    mRate = Val(.Text)
                ElseIf pDnCnType = "P" Then
                    .Col = ColQty
                    mQty = Val(.Text)
                    .Col = ColRejectedQty
                    mQty = mQty - Val(.Text)
                    .Col = ColShortageQty
                    mQty = mQty - Val(.Text)
                    .Col = ColPORate
                    mPORate = Val(.Text)
                    .Col = ColRate
                    mRate = Val(.Text)
                    If mPORate <> 0 Then
                        If pVType = "DN" Then
                            mRate = IIf(mRate - mPORate <= 0, 0, mRate - mPORate)
                        Else
                            mRate = IIf(mPORate - mRate <= 0, 0, mPORate - mRate)
                        End If
                    Else
                        mRate = 0
                    End If
                End If
                mAmount = mQty * mRate
                mAmount = CDbl(VB6.Format(mAmount, "0.00"))
                If Val(lblTotItemValue.Text) = 0 Then
                    pItemEDAmount = 0
                    pItemSTAmount = 0
                Else
                    pItemEDAmount = 0 '  Val(lblTotED.text) * mAmount / Val(lblTotItemValue.text)							
                    pItemSTAmount = 0 '  Val(lblTotST.text) * mAmount / Val(lblTotItemValue.text)							
                End If
                mMrrRefType = GetMrrRefNo(Val(mMRRNO))
                SqlStr = ""
                If mItemCode <> "" And mAmount <> 0 Then
                    SqlStr = " INSERT INTO FIN_DNCN_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , ITEM_DESC, HSNCODE, ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT," & vbCrLf & " MRR_REF_NO,MRR_REF_DATE,SUPP_REF_NO," & vbCrLf & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, " & vbCrLf & " PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, " & vbCrLf & " PO_RATE, MRR_REF_TYPE,ITEM_ED, ITEM_ST " & vbCrLf & " ) "
                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & xKey & "'," & I & ", " & vbCrLf & " '" & mItemCode & "','" & MainClass.AllowSingleQuote(mItemDesc) & "','" & mHSNCode & "', " & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & "," & vbCrLf & " " & Val(mMRRNO) & ",TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mPONo & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & LblMKey.Text & "'," & vbCrLf & " '" & xPURVNO & "',TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & xPURVNO & "',TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & mPORate & ", '" & mMrrRefType & "', " & Val(CStr(pItemEDAmount)) & ", " & Val(CStr(pItemSTAmount)) & "" & vbCrLf & " ) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDNCNDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDNCNDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Function
    Private Function UpdateDNCNHDRAPP(ByRef xKey As String, ByRef pDnCnType As String) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim xItemValue As Double
        Dim xSTPERCENT As Double
        Dim xTOTSTAMT As Double
        Dim xTOTFREIGHT As Double
        Dim xTOTCHARGES As Double
        Dim xEDPERCENT As Double
        Dim xTotEDAmount As Double
        Dim xSURAmount As Double
        Dim xTotDiscount As Double
        Dim xMSC As Double
        Dim xRO As Double
        Dim xTOTEXPAMT As Double
        Dim xTOTTAXABLEAMOUNT As Double
        Dim xNETVALUE As Double
        Dim xTotQty As Double
        Dim xVatSurAmt As Double
        Dim xCGSTPer As Double
        Dim xSGSTPer As Double
        Dim xIGSTPer As Double
        Dim xCGSTAmount As Double
        Dim xSGSTAmount As Double
        Dim xIGSTAmount As Double
        Dim xCGSTRefundAmount As Double
        Dim xSGSTRefundAmount As Double
        Dim xIGSTRefundAmount As Double
        SqlStr = "SELECT SUM(ITEM_AMT) AS ITEM_AMT FROM FIN_DNCN_DET Where Mkey='" & xKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            xItemValue = CDbl(VB6.Format(IIf(IsDBNull(RsMisc.Fields("ITEM_AMT").Value), 0, RsMisc.Fields("ITEM_AMT").Value), "0.00"))
        End If
        Call CalcTotsDNCN(xKey, pDnCnType, xItemValue, xTOTFREIGHT, xTOTCHARGES, xTotDiscount, xMSC, xRO, xTOTEXPAMT, xNETVALUE, xTotQty, xCGSTPer, xSGSTPer, xIGSTPer, xCGSTAmount, xSGSTAmount, xIGSTAmount, xCGSTRefundAmount, xSGSTRefundAmount, xIGSTRefundAmount)
        SqlStr = "UPDATE FIN_DNCN_HDR SET " & vbCrLf & " ITEMVALUE=" & xItemValue & ", " & vbCrLf & " STPERCENT=" & xSTPERCENT & ",  " & vbCrLf & " TOTSTAMT=" & xTOTSTAMT & ", " & vbCrLf & " TOTFREIGHT=" & xTOTFREIGHT & ", " & vbCrLf & " TOTCHARGES=" & xTOTCHARGES & ", " & vbCrLf & " EDPERCENT=" & xEDPERCENT & ",  " & vbCrLf & " TOTEDAMOUNT=" & xTotEDAmount & ", " & vbCrLf & " TOTSURCHARGEAMT=" & xSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & xTotDiscount & ", " & vbCrLf & " TOTMSCAMOUNT=" & xMSC & ", " & vbCrLf & " TOTRO=" & xRO & ", " & vbCrLf & " TOTEXPAMT=" & xTOTEXPAMT & ", " & vbCrLf & " TOTTAXABLEAMOUNT=" & xTOTTAXABLEAMOUNT & ", " & vbCrLf & " SUR_VATCLAIMAMOUNT=" & xVatSurAmt & ", " & vbCrLf & " NETVALUE=" & xNETVALUE & ", UPDATE_FROM='N'," & vbCrLf & " TOTQTY=" & xTotQty & "," & vbCrLf & " NETCGST_AMOUNT=" & xCGSTAmount & ", " & vbCrLf & " NETSGST_AMOUNT=" & xSGSTAmount & ", " & vbCrLf & " NETIGST_AMOUNT=" & xIGSTAmount & ", " & vbCrLf & " CGST_REFUNDAMOUNT=" & xCGSTRefundAmount & ", " & vbCrLf & " SGST_REFUNDAMOUNT=" & xSGSTRefundAmount & ", " & vbCrLf & " IGST_REFUNDAMOUNT=" & xIGSTRefundAmount & " " & vbCrLf & " Where Mkey='" & xKey & "'"
        PubDBCn.Execute(SqlStr)
        UpdateDNCNHDRAPP = True
        Exit Function
UpdateDetail1:
        UpdateDNCNHDRAPP = False
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
        Dim mHeadType As String
        Dim mInterUnit As String
        'Dim mAlreadyRejQty As Double							
        Dim pDebitNoteNo As String
        Dim pDebitNoteDate As String
        Dim mItemClassification As String
        Dim mAcctPostName As String
        Dim xSuppCode As String
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim pMaxDate As String
        Dim mGSTClass As String
        Dim mItemExempted As Boolean
        Dim mPurpose As String
        Dim mShippFromSameBillFrom As String
        Dim mMRRNO As Double
        Dim mMRRDate As String
        Dim pErrorMsg As String
        mAgtPO = False
        FieldsVarification = True
        If ValidateBranchLocking((TxtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            mLockBookCode = CInt(ConLockModvat)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVno.Text), (TxtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "Y" Then
            mLockBookCode = CInt(ConLockPurchase)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVno.Text), (TxtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "N" Then
            mLockBookCode = CInt(ConLockModvat)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVno.Text), (TxtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            mLockBookCode = CInt(ConLockPurchase)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVno.Text), (TxtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, TxtVDate.Text) = True Then
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
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If MainClass.GetUserCanModify(TxtVDate.Text) = False Then
                MsgBox("You Have Not Rights to Add or Modify back Voucher", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
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
        '    If txtMRRNo.Text = "" Then							
        '        MsgBox "DCNo is Blank", vbInformation							
        '        FieldsVarification = False							
        '        txtMRRNo.SetFocus							
        '        Exit Function							
        '    End If							
        '    If txtMRRDate.Text = "" Then							
        '        MsgBox "DCDate is Blank", vbInformation							
        '        FieldsVarification = False							
        '        txtMRRDate.SetFocus							
        '        Exit Function							
        ''    ElseIf FYChk(txtMRRDate.Text) = False Then							
        ''        FieldsVarification = False							
        ''        txtMRRDate.SetFocus							
        ''        Exit Function							
        '    End If							
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
        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Bill from & Ship from is Same, please save in Purchase (GST - Goods Order) Form. ", MsgBoxStyle.Information)
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
        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        '							
        '    If Trim(txtModvatSupp.Text) = "" Then							
        '        txtModvatSupp.Text = txtSupplier.Text							
        ''        MsgBox "Modvat Supplier Cannot Be Blank", vbInformation							
        ''       ' txtSupplier.SetFocus							
        ''        FieldsVarification = False							
        ''        Exit Function							
        '    End If							
        '							
        '     If MainClass.ValidateWithMasterTable(txtModvatSupp.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then							
        '        MsgBox "Modvat Supplier Does Not Exist In Master", vbInformation							
        '        'txtSupplier.SetFocus							
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
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If Trim(txtTariff.Text) = "" Then
                MsgBox("Tariff Heading Cannot Be Blank", MsgBoxStyle.Information)
                txtTariff.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CheckCRStockType(mItemType) = False Then
                MsgBox("Please Check Stock Type in MRR. Stock Type should be 'CR' For Prduction or 'ST' for BOP / RM.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        mItemType = CheckItemType()
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
                    cboInvType.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
                If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ISST_REQ", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSalesTaxReq = MasterNo
                End If
            End If
            mWithInState = "Y"
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInState = IIf(IsDbNull(MasterNo), "Y", MasterNo)
            End If
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Debit Account Does Not Exist In Master", MsgBoxStyle.Information)
                    'txtDebitAccount.SetFocus							
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            '        If Trim(txtItemType.Text) = "" Then							
            '            MsgBox "Item Type is Blank", vbInformation							
            '            FieldsVarification = False							
            '            txtItemType.SetFocus							
            '            Exit Function							
            '        End If							
            If (lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R") And mRefType <> "R" Then
                MsgBox("MRR not Made Agt. RGP, So cann't be Save.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If (lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R") And mRefType = "R" Then
            Else
                If mRefType = "R" Then
                    MsgBox("MRR Made Agt. RGP, So cann't be Save.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            '        If lblPurchaseType.text = "R" And mRefType <> "R" Then							
            '            MsgBox "MRR not Made Agt. RGP, So cann't be Save.", vbInformation							
            '            FieldsVarification = False							
            '            Exit Function							
            '        End If							
            '							
            '							
            '        If lblPurchaseType.text <> "R" And mRefType = "R" Then							
            '            MsgBox "MRR Made Agt. RGP, So cann't be Save.", vbInformation							
            '            FieldsVarification = False							
            '            Exit Function							
            '        End If							
            If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ISSALERETURN", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mIsSaleReturn = MasterNo
            End If
            If mItemType = "B" Or mItemType = "R" Then
                If mIsSaleReturn = "Y" Then
                    MsgBox("Invaild Invoice Type. BOP/RM Item Cann't be in Sales Return.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                If mIsSaleReturn = "Y" Then
                    If mRefType = "I" Or mRefType = "2" Then
                        If chkRejection.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                            MsgBox("Please Select Agt D3 Check.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    Else
                        MsgBox("Invaild Invoice Type. MRR Not made is Sales Return.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
                If mIsSaleReturn = "N" And (mRefType = "I" Or mRefType = "2") Then
                    MsgBox("Invaild Invoice Type. MRR Not made is Sales Return.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If
        If Trim(lblSaleBillNo.Text) <> "" Then
            MsgBox("Reverse Charge Sale Bill is Generated, So Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
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
        If chkCreditRC.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Final Credit on Reverse Charge is Done, So that cann't be Modify.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode And VB.Left(cboGSTStatus.Text, 1) <> "G" Then
            MsgBox("Please check GST Check.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        '    If Trim(mPartyGSTNo) = Trim(mCompanyGSTNo) Then							
        ''        If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) > 0 Then							
        ''            MsgBox "GST Amount Should be Zero.", vbInformation							
        ''            FieldsVarification = False							
        ''            Exit Function							
        ''        End If							
        '    Else							
        '        If Left(cboGSTStatus.Text, 1) = "G" Or Left(cboGSTStatus.Text, 1) = "R" Then							
        '            If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) = 0 Then							
        '                MsgBox "GST Amount Cann't be Zero.", vbInformation							
        '                FieldsVarification = False							
        '                Exit Function							
        '            End If							
        '        Else							
        '            If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) > 0 Then							
        '                MsgBox "GST Amount Should not be Zero.", vbInformation							
        '                FieldsVarification = False							
        '                Exit Function							
        '            End If							
        '        End If							
        '    End If							
        '							
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
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            mWithInState = "Y"
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInState = IIf(IsDbNull(MasterNo), "Y", MasterNo)
            End If
            '        If Trim(txtItemType.Text) = "" Then							
            '            MsgBox "Item Type is Blank", vbInformation							
            '            FieldsVarification = False							
            '            txtItemType.SetFocus							
            '            Exit Function							
            '        End If							
        End If
        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        '    If RsCompany!ISEOU = "Y" And Val(txtModvatAmount.Text) <> 0 Then							
        '        If MainClass.ValidDataInGrid(SprdMain, ColEDRate, "N", "Please Check Excise Duty Percentage.") = False Then FieldsVarification = False: Exit Function							
        '    End If							
        Dim mGSTRegd As String
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGSTRegd = IIf(IsDbNull(MasterNo), "N", MasterNo)
        End If
        If cboGSTStatus.SelectedIndex = -1 Then
            MsgBox("Please select GST Status", MsgBoxStyle.Information)
            If cboGSTStatus.Enabled = True Then cboGSTStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If VB.Left(cboGSTStatus.Text, 1) = "N" Then
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    mGSTClass = "0"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "GST_ITEMCLASS", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mGSTClass = MasterNo
                    End If
                    If mGSTClass <> "1" Then
                        MsgInformation("Item is not a Non-GST Item, So that cann't be Save.")
                        FieldsVarification = False
                        Exit Function
                    End If
                Next
            End With
        Else
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    mGSTClass = "2"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "GST_ITEMCLASS", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mGSTClass = MasterNo
                    End If
                    If mGSTClass = "2" Then
                        mItemExempted = True
                    Else
                        mItemExempted = False
                        Exit For
                    End If
                Next
            End With
            If mItemExempted = False Then
                If mGSTRegd = "Y" And VB.Left(cboGSTStatus.Text, 1) <> "G" Then
                    MsgBox("Supplier is registered, please select the GST Refund.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus							
                    FieldsVarification = False
                    Exit Function
                End If
                If mGSTRegd = "N" And VB.Left(cboGSTStatus.Text, 1) <> "R" Then
                    MsgBox("Supplier is not registered, please select the Reverse Charge.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus							
                    FieldsVarification = False
                    Exit Function
                End If
                If mGSTRegd = "E" And VB.Left(cboGSTStatus.Text, 1) <> "E" Then
                    MsgBox("GST Exempted Supplier, please select the GST Exempted.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus							
                    FieldsVarification = False
                    Exit Function
                End If
                If Trim(mPartyGSTNo) = Trim(mCompanyGSTNo) Then
                    '        If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) > 0 Then							
                    '            MsgBox "GST Amount Should be Zero.", vbInformation							
                    '            FieldsVarification = False							
                    '            Exit Function							
                    '        End If							
                Else
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Then
                        If (Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text)) = 0 Then
                            MsgBox("GST Amount Cann't be Zero.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    Else
                        If (Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text)) > 0 Then
                            MsgBox("GST Amount Should not be Zero.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            End If
        End If
        '    If LblBookCode.text = ConPurchaseBookCode And chkGSTRefund.Value = vbUnchecked And (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) <> 0 Then							
        '        If MsgQuestion("You have not Check in GST. You Want to Continue ...") = vbNo Then							
        '            FieldsVarification = False							
        '            Exit Function							
        '        End If							
        '    End If							
        '    If LblBookCode.text = ConPurchaseBookCode Then							
        '        If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) <> (Val(txtTotCGSTRefund.Text) + Val(txtTotSGSTRefund.Text) + Val(txtTotIGSTRefund.Text)) Then							
        '            If MsgQuestion("GST Amount And Refund Amount Not Match. You Want to Continue ...") = vbNo Then							
        '                FieldsVarification = False							
        '                Exit Function							
        '            End If							
        '        End If							
        '    End If							
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode And chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mPORateZero = False
            mCapitalInvType = "N"
            If MainClass.ValidateWithMasterTable(Trim(cboInvType.Text), "NAME", "ISFIXASSETS", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCapitalInvType = Trim(IIf(IsDbNull(MasterNo), "N", MasterNo))
            End If
            If chkCapital.CheckState = System.Windows.Forms.CheckState.Checked And mCapitalInvType = "N" Then
                If MsgQuestion("Invoice Type is not Capital but checked in Capital. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            ElseIf chkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked And mCapitalInvType = "Y" Then
                If MsgQuestion("Invoice Type is Capital but not checked in Capital. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColMRRNo
                    mMRRNO = CDbl(Trim(.Text))
                    If MainClass.ValidateWithMasterTable(Trim(CStr(mMRRNO)), "AUTO_KEY_MRR", "MRR_DATE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mMRRDate = Trim(IIf(IsDbNull(MasterNo), "", MasterNo))
                    End If
                    If mMRRDate <> "" Then
                        If CDate(TxtVDate.Text) < CDate(mMRRDate) Then
                            MsgBox("VDate Can Not be Less Than MRRDate.")
                            FieldsVarification = False
                            TxtVDate.Focus()
                            Exit Function
                        End If
                    End If
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    mItemClassification = ""
                    If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemClassification = Trim(IIf(IsDbNull(MasterNo), "", MasterNo))
                    End If
                    If mItemClassification = "3" Then
                        .Col = ColPORate
                        xPORate = Val(.Text)
                        .Col = ColRate
                        xRate = Val(.Text)
                        If xPORate <> xRate Then
                            MsgBox("Diesel Rate in Not Match with PO please Check. Cann't be Save.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    mIsItemCapital = GetProductionType(mItemCode)
                    mIsItemCapital = IIf(mIsItemCapital = "A", "Y", "N")
                    If chkCapital.CheckState = System.Windows.Forms.CheckState.Checked And mIsItemCapital = "N" Then
                        If MsgQuestion("Item Category is not Capital of Item Code [" & mItemCode & "]. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    ElseIf chkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked And mIsItemCapital = "Y" Then
                        If MsgQuestion("Item Category is Capital of Item Code [" & mItemCode & "]. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    .Col = ColShortageQty
                    xShortageQty = xShortageQty + Val(.Text)
                    .Col = ColRejectedQty
                    xRejectedQty = xRejectedQty + Val(.Text)
                    .Col = ColPORate
                    xPORate = Val(.Text)
                    .Col = ColVolDiscRate
                    xVolDiscRate = Val(.Text)
                    .Col = ColPONo
                    xPoNo = CStr(Val(.Text))
                    .Col = ColRate
                    xRate = Val(.Text)
                    If ADDMode = True Then
                        If Val(xPoNo) > 0 And xPORate <> 0 Then
                            If xPORate - xRate < 0 Then
                                xRateDiffDN = xRateDiffDN + 1
                            ElseIf xPORate - xRate > 0 Then
                                xRateDiffCN = xRateDiffCN + 1
                            End If
                        End If
                        If Val(xPoNo) > 0 And xVolDiscRate > 0 Then
                            xVolDiscRateDN = xVolDiscRateDN + 1
                        End If
                        If Val(xPoNo) > 0 And xPORate = 0 Then
                            mPORateZero = True
                        End If
                    End If
                    If mAgtPO = True Then
                        If CheckAmount(Val(xPoNo)) = False Then
                            '                        MsgBox "Purchase Amount Cann't be Greater Than PO Amount", vbInformation							
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColRate)
                            Exit Function
                        End If
                    End If
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColInvType
                    mAcctPostName = IIf(Trim(UCase(SprdMain.Text)) = "", Trim(cboInvType.Text), Trim(UCase(SprdMain.Text)))
                    If mAcctPostName = "" Then
                        MsgInformation("Account Post Name Cann't be Blank.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColInvType)
                        FieldsVarification = False
                        Exit Function
                    Else
                        If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = False Then
                            MsgInformation("Invaild Account Post Name.")
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColInvType)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    If VB.Left(cboGSTStatus.Text, 1) = "E" Or VB.Left(cboGSTStatus.Text, 1) = "N" Then
                    Else
                        SprdMain.Row = cntRow
                        SprdMain.Col = ColHSN
                        If Trim(UCase(SprdMain.Text)) = "" Then
                            MsgInformation("HSN Cann't be Blank.")
                            '                        MainClass.SetFocusToCell SprdMain, I, ColAcctPostName							
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    If mRefType = "R" And xRate > 0.0001 Then
                        mPurpose = ""
                        If GetValidRGPPurpose(Val(xPoNo), mPurpose) = False Then
                            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mInterUnit = IIf(IsDbNull(MasterNo), "Y", MasterNo)
                            End If
                            If mInterUnit = "Y" Then
                                If MsgQuestion("RGP Purpose is FOC OR Trail. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                                    FieldsVarification = False
                                    MainClass.SetFocusToCell(SprdMain, cntRow, ColRate)
                                    Exit Function
                                End If
                            Else
                                MsgBox("RGP Purpose is FOC OR Trail, So Can't be post in Account. RGP NO : " & xPoNo)
                                FieldsVarification = False
                                MainClass.SetFocusToCell(SprdMain, cntRow, ColRate)
                                Exit Function
                            End If
                        Else
                            If lblPurchaseType.Text = "J" And mPurpose <> "B" Then
                                MsgBox("RGP Purpose is not Jobwork, Please check RGP NO : " & xPoNo)
                                FieldsVarification = False
                                Exit Function
                            ElseIf lblPurchaseType.Text = "W" And mPurpose = "B" Then
                                MsgBox("RGP Purpose is not Work Order, Please check RGP NO : " & xPoNo)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If
                Next
            End With
            If mPORateZero = True Then
                If MsgQuestion("Purchase Order rate is Zero. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If RsCompany.Fields("REJECTION_DN").Value = "Y" Then
                If xRejectedQty > 0 And ADDMode = True Then
                    If MsgQuestion("Debit Note Will be Generate of Rejection Qty. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                        FieldsVarification = False
                        Exit Function
                    Else
                        If CheckDebitNote(pDebitNoteNo, pDebitNoteDate, "R", mMRRNO) = True Then
                            If MsgQuestion("Debit Note Already Deduct for this party for such bill. Debit Note No : " & pDebitNoteNo & " - " & pDebitNoteDate & ". You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
            End If
            If xShortageQty > 0 And ADDMode = True Then
                If MsgQuestion("Debit Note Will be Generate of Shortage Qty. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                Else
                    If CheckDebitNote(pDebitNoteNo, pDebitNoteDate, "S", mMRRNO) = True Then
                        If MsgQuestion("Debit Note Already Deduct for this party for such bill. Debit Note No : " & pDebitNoteNo & " - " & pDebitNoteDate & ". You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            End If
            If xRateDiffDN >= 1 And ADDMode = True Then
                If MsgQuestion("Debit Note Will be Generate of Rate Diff. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If xVolDiscRateDN >= 1 And ADDMode = True Then
                If MsgQuestion("Debit Note Will be Generate of Volume Discount. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If xRateDiffCN >= 1 And ADDMode = True Then
                If MsgQuestion("Credit Note Will be Generate of Rate Diff. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If ADDMode = True Then
                If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtTDSAmount.Text) = 0 Then
                    MsgBox("Please Check TDS Amount.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
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
            End If
        End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode And chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            If VB.Left(cboGSTStatus.Text, 1) <> "E" Then
                MsgBox("Cann't be Cancelled.(First You Deleted GST Claim.)", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
            If Trim(txtServProvided.Text) = "" Then
                MsgBox("Service Provided Cann't be Blank", MsgBoxStyle.Information)
                If txtServProvided.Enabled Then txtServProvided.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If Trim(txtServProvided.Text) <> "" Then
                MsgBox("You Select Service Provided for Goods.", MsgBoxStyle.Information)
                If txtServProvided.Enabled Then txtServProvided.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If MainClass.ValidateWithMasterTable(Trim(txtDebitAccount.Text), "SUPP_CUST_NAME", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mHeadType = Trim(MasterNo)
        End If
        If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then ''If mHeadType = "4" Then							
            If Trim(txtServProvided.Text) = "" Then
                MsgBox("Please Select The Service., So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
                MsgBox("Service Provided is not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            '        If Val(txtServiceOn.Text) = 0 Then							
            '            MsgBox "Please Enter the Service Tax On, So cann't be Saved.", vbInformation							
            '            FieldsVarification = False							
            '            Exit Function							
            '        End If							
            '							
            '        If Val(txtServiceTaxPer.Text) = 0 Then							
            '            MsgBox "Please Enter the Service Tax Per, So cann't be Saved.", vbInformation							
            '            FieldsVarification = False							
            '            Exit Function							
            '        End If							
            '							
            '        If Val(txtProviderPer.Text) + Val(txtRecipientPer.Text) <> 100 Then							
            '            MsgBox "Provider & Recipient Service Percent is not Equal to 100, So cann't be Saved.", vbInformation							
            '            FieldsVarification = False							
            '            Exit Function							
            '        End If							
        End If
        If ADDMode = True Then
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
        End If
        If mBookSubType = "R" Then
            If Val(txtAdvAdjust.Text) > 0 Then
                MsgBox("Advance Balance cann't adjust with Sales Return.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
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
        Exit Function
err_Renamed:
        FieldsVarification = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
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
        DuplicateBillNo = False
        mCount = 0
        SqlStr = "SELECT BILLNO, BILLDATE  " & vbCrLf & " FROM FIN_POSTED_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BILLTYPE='B'" & vbCrLf & " AND ACCOUNTCODE='" & pSuppCode & "'" & vbCrLf & " AND BILLNO='" & Trim(txtBillNo.Text) & "'"
        If ADDMode = True Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND MKEY<>'" & pMKey & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        mAcctBillFYear = GetCurrentFYNo(PubDBCn, (txtBillDate.Text))
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mBillNo = IIf(IsDbNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                If CDate(txtBillDate.Text) = CDate(mBillDate) Then
                    DuplicateBillNo = True
                    Exit Function
                Else
                    mBillFyear = GetCurrentFYNo(PubDBCn, mBillDate)
                    If mAcctBillFYear = mBillFyear Then
                        mCount = mCount + 1
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
    Private Function CheckDebitNote(ByRef pDebitNoteNo As String, ByRef pDebitNoteDate As String, ByRef pDnCnType As String, ByRef pMRRNo As Double) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mSql As String
        CheckDebitNote = False
        SqlStr = "SELECT VNO, VDATE " & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID"
        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.MRR_REF_NO=" & Val(CStr(pMRRNo)) & "" & vbCrLf & " AND IH.DNCNTYPE='" & pDnCnType & "' AND IH.APPROVED='Y' AND IH.CANCELLED='N'"
        If pDnCnType = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM IN ('M')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM IN ('P')"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                pDebitNoteNo = IIf(pDebitNoteNo = "", "", pDebitNoteNo & ",") & IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                pDebitNoteDate = IIf(pDebitNoteDate = "", "", pDebitNoteDate & ",") & IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
                RsTemp.MoveNext()
            Loop
            CheckDebitNote = True
        Else
            pDebitNoteNo = ""
            pDebitNoteDate = ""
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckDebitNote = False
    End Function
    Private Function CheckAmount(ByRef pPONO As Double) As Boolean
        On Error GoTo ErrPart1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mTotalAmount As Double
        Dim mPurchaseAmount As Double
        Dim mIsProjectPO As Boolean
        Dim cntRow As Integer
        Dim mPOAmount As Double
        mIsProjectPO = False
        If MainClass.ValidateWithMasterTable(pPONO, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If MasterNo = "R" Then
                mIsProjectPO = True
            End If
        End If
        If mIsProjectPO = False Then CheckAmount = True : Exit Function
        mTotalAmount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPONo
                If Val(.Text) = pPONO Then
                    .Col = ColAmount
                    mTotalAmount = mTotalAmount + Val(.Text)
                End If
            Next
        End With
        SqlStr = "SELECT SUM(ID.ITEM_AMT) AS AMOUNT" & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.ISFINALPOST='Y'" & vbCrLf & " AND ID.CUST_REF_NO='" & pPONO & "'"
        '    If Val(txtMRRNo.Text) <> 0 Then							
        '        SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""							
        '    End If							
        If LblMKey.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY<>'" & LblMKey.Text & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mPurchaseAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If
        mPurchaseAmount = mPurchaseAmount + mTotalAmount
        SqlStr = "SELECT SUM(ID.GROSS_AMT) AS AMOUNT" & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.AUTO_KEY_PO=" & pPONO & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mPOAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If
        If mPOAmount < mPurchaseAmount Then
            MsgInformation("Purchase Amount (Rs." & mPurchaseAmount & ") Cann't be exceed Than PO Amount (Rs." & mPOAmount & ").")
            '        MainClass.SetFocusToCell SprdMain, Row, ColRate							
            CheckAmount = False
        Else
            CheckAmount = True
        End If
        Exit Function
ErrPart1:
        CheckAmount = False
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmPurchaseShipGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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
        If Val(LblBookCode.Text) = ConModvatBookCode Then
            cboInvType.Enabled = False
            cboInvType.Visible = False
            lblInvType.Visible = False
            txtTotCGSTRefund.Enabled = True
            txtTotSGSTRefund.Enabled = True
            txtTotIGSTRefund.Enabled = True
            chkRejection.Enabled = True
        Else
            cboInvType.Enabled = True
            cboInvType.Visible = True
            lblInvType.Visible = True
            txtTotCGSTRefund.Enabled = False
            txtTotSGSTRefund.Enabled = False
            txtTotIGSTRefund.Enabled = False
            chkRejection.Enabled = False
        End If
        If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "I" And RsCompany.Fields("FYEAR").Value >= 2007 Then
            cboInvType.Enabled = False
            '        cboInvType.Visible = True							
        End If
        cboGSTStatus.Items.Clear()
        cboGSTStatus.Items.Add("GST Input")
        cboGSTStatus.Items.Add("Reverse Charge")
        cboGSTStatus.Items.Add("Exempt")
        cboGSTStatus.Items.Add("Non-GST")
        cboGSTStatus.SelectedIndex = -1
        FillCboSaleType()
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
        Dim SqlStr As String
        SqlStr = ""
        MainClass.ClearGrid(SprdView)
        SqlStr = "SELECT FIN_INVTYPE_MST.NAME AS INVOICE_TYPE," & vbCrLf & " VNOPREFIX, TO_CHAR(VNOSEQ),VNOSUFFIX, " & vbCrLf & " VNO,VDATE, "
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            SqlStr = SqlStr & vbCrLf & " VNO AS VNO,VDATE, "
        ElseIf CDbl(LblBookCode.Text) = ConModvatBookCode Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(DECODE(GST_CLAIM_NO,-1,'',GST_CLAIM_NO),'00000') AS GST_CLAIM_NO,GST_CLAIM_DATE, "
        End If
        SqlStr = SqlStr & vbCrLf & " BILLNO, INVOICE_DATE  AS BILLDATE, " & vbCrLf & " AUTO_KEY_MRR AS MRRNO, MRRDATE, " & vbCrLf & " A.SUPP_CUST_NAME AS SUPPLIER, B.SUPP_CUST_NAME AS CREDIT_ACCOUNT, " & vbCrLf & " ITEMDESC, TARIFFHEADING AS TARIFF,ITEMVALUE,"
        SqlStr = SqlStr & vbCrLf & "TOTCGST_REFUNDAMT AS CGSTAMT,TOTSGST_REFUNDAMT AS SGSTAMT,TOTIGST_REFUNDAMT AS IGSTAMT, NETVALUE,DECODE(ISCAPITAL,'Y','YES','NO') AS ISCAPITAL,DECODE(REJECTION,'Y','YES','NO') AS AGTD3 "
        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_PURCHASE_HDR, FIN_INVTYPE_MST, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE FIN_PURCHASE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And FIN_PURCHASE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND FIN_PURCHASE_HDR.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE(+) " & vbCrLf & " AND FIN_PURCHASE_HDR.TRNTYPE=FIN_INVTYPE_MST.CODE(+) " & vbCrLf & " AND FIN_PURCHASE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf & " AND FIN_PURCHASE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE " & vbCrLf & " AND FIN_PURCHASE_HDR.COMPANY_CODE=B.COMPANY_CODE(+) " & vbCrLf & " AND FIN_PURCHASE_HDR.ACCOUNTCODE=B.SUPP_CUST_CODE(+) AND FIN_PURCHASE_HDR.PURCHASE_TYPE= '" & lblPurchaseType.Text & "'"
        SqlStr = SqlStr & vbCrLf & " AND SHIPPED_TO_SAMEPARTY='N' AND PURCHASESEQTYPE='" & Val(lblPurchaseSeqType.Text) & "'"
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='Y'" '' AND AUTO_KEY_MRR<>-1"							
            SqlStr = SqlStr & vbCrLf & " Order by GST_CLAIM_DATE,GST_CLAIM_NO"
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            SqlStr = SqlStr & vbCrLf & " AND ISFINALPOST='Y'" '' AND AUTO_KEY_MRR<>-1"          ''AND TRNTYPE<>-1							
            SqlStr = SqlStr & vbCrLf & " AND VDATE >= TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " Order by FIN_PURCHASE_HDR.VDATE, FIN_PURCHASE_HDR.VNO"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume							
    End Sub
    Private Sub FormatSprdView()
        Dim cntCol As Integer
        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 600)
            .set_ColWidth(1, 0)
            .set_ColWidth(2, 0)
            .set_ColWidth(3, 0)
            .set_ColWidth(4, 0)
            .set_ColWidth(5, 0)
            .set_ColWidth(6, 0)
            .set_ColWidth(7, 1200)
            .set_ColWidth(8, 1300)
            .set_ColWidth(9, 1200)
            .set_ColWidth(10, 1300)
            .set_ColWidth(11, 1200)
            .set_ColWidth(12, 1200)
            .set_ColWidth(13, 2000)
            .set_ColWidth(14, 2000)
            .set_ColWidth(15, 1200)
            .set_ColWidth(16, 1200)
            .set_ColWidth(17, 1200)
            .set_ColWidth(18, 1200)
            .set_ColWidth(19, 1200)
            .set_ColWidth(20, 1200)
            .set_ColWidth(21, 800)
            .set_ColWidth(22, 800)
            For cntCol = 17 To 20
                .Col = cntCol
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            Next
            .ColsFrozen = 8
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdExp(ByRef Arow As Integer)
        On Error GoTo ERR1
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)
            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.999
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
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
                '            MainClass.UnProtectCell SprdExp, 1, .MaxRows, ColExpDebitAmt, ColExpDebitAmt							
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
        pShowCalc = False
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPurchDetail.Fields("MRRNO").DefinedSize ''							
            .set_ColWidth(ColMRRNo, 6)
            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPurchDetail.Fields("ITEM_CODE").DefinedSize ''							
            .set_ColWidth(ColItemCode, 8)
            .Col = ColHSN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn) ''							
            .set_ColWidth(ColHSN, 6)
            .Col = ColInvType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
            .set_ColWidth(ColInvType, 25)
            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPurchDetail.Fields("CUSTOMER_PART_NO").DefinedSize
            .ColHidden = True
            .ColsFrozen = ColItemDesc
            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsPurchDetail.Fields("Item_Desc").DefinedSize ''							
            .set_ColWidth(ColItemDesc, 15)
            .Col = ColAcceptedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True
            .Col = ColShortageQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColShortageQty, 7)
            .Col = ColRejectedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRejectedQty, 7)
            .Col = ColPORate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPORate, 7)
            .Col = ColVolDiscRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColVolDiscRate, 6)
            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsPurchDetail.Fields("ITEM_UOM").DefinedSize ''							
            .set_ColWidth(ColUnit, 4)
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
            .set_ColWidth(ColAmount, 8)
            .Col = ColTaxableAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColTaxableAmount, 8)
            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColCGSTPer, 5)
            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColCGSTAmount, 8)
            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColSGSTPer, 5)
            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColSGSTAmount, 8)
            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColIGSTPer, 5)
            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColIGSTAmount, 8)
            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPurchDetail.Fields("CUST_REF_NO").DefinedSize ''							
            .set_ColWidth(ColPONo, 9)
            .Col = ColShowPO
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False							
            .TypeButtonText = "Show"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColShowPO, 5)
        End With
        '    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "PORATEEDITABLE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then							
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColUnit)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColTaxableAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPONo, ColPONo)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMRRNo, ColMRRNo)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty)
        '    End If							
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
            txtMRRNo.Maxlength = .Fields("AUTO_KEY_MRR").Precision ''							
            txtMRRDate.Maxlength = 10
            txtBillNo.Maxlength = .Fields("BillNo").Precision ''							
            txtBillDate.Maxlength = 10
            txtSupplier.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            '        txtModvatSupp.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)							
            txtDebitAccount.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtShippedTo.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtCreditDays(0).Maxlength = .Fields("DUEDAYSFROM").Precision ''							
            txtCreditDays(1).Maxlength = .Fields("DUEDAYSTO").Precision ''							
            txtTariff.Maxlength = .Fields("TARIFFHEADING").DefinedSize ''							
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
            txtAdvIGST.Maxlength = .Fields("ADV_IGST_AMT").Precision
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        'Dim mCustRefNo As String							
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mSACCode As String
        Dim mGSTStatus As String
        Dim mVNo As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        Clear1()
        With RsPurchMain
            If Not .EOF Then
                LblMKey.Text = .Fields("MKey").Value
                lblPMKey.Text = ""
                txtVNoPrefix.Text = IIf(IsDbNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value)
                txtVno.Text = VB6.Format(IIf(IsDbNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                txtVNoSuffix.Text = IIf(IsDbNull(.Fields("VNOSUFFIX").Value), "", .Fields("VNOSUFFIX").Value)
                TxtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")
                lblGSTClaimNo.Text = IIf(IsDbNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value)
                lblGSTClaimDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                txtModvatNo.Text = IIf(IsDbNull(.Fields("GST_CLAIM_NEW_NO").Value), "", .Fields("GST_CLAIM_NEW_NO").Value)
                txtModvatDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_NEW_DATE").Value), "", .Fields("GST_CLAIM_NEW_DATE").Value), "DD/MM/YYYY")
                chkGSTClaim.CheckState = IIf(.Fields("GST_CLAIM").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                lblClaimStatus.Text = IIf(IsDbNull(.Fields("GST_CLAIM").Value), "N", .Fields("GST_CLAIM").Value)
                chkCreditRC.CheckState = IIf(.Fields("GST_RC_CLAIM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                '            If chkCreditRC.Value = vbChecked Then							
                '                txtModvatNo.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_RC_NO").Value), "", .Fields("GST_CLAIM_RC_NO").Value), "00000")							
                '                txtModvatDate.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_RC_DATE").Value), "", .Fields("GST_CLAIM_RC_DATE").Value), "DD/MM/YYYY")							
                '            Else							
                '                txtModvatNo.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value), "00000")							
                '                txtModvatDate.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")							
                '            End If							
                '							
                mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVno.Text), "00000") & Trim(txtVNoSuffix.Text))
                '            lblSaleBillNoSeq.text = Format(IIf(IsNull(.Fields("SALEBILLNOSEQ").Value), "", .Fields("SALEBILLNOSEQ").Value), "00000000")							
                '            lblSaleBillNo.text = IIf(IsNull(.Fields("SALEBILL_NO").Value), "", .Fields("SALEBILL_NO").Value)							
                '            lblSaleBillDate.text = Format(IIf(IsNull(.Fields("SALEBILLDATE").Value), "", .Fields("SALEBILLDATE").Value), "DD/MM/YYYY")							
                lblPurchaseVNo.Text = VB6.Format(IIf(IsDbNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                lblPurchaseSeqType.Text = IIf(IsDbNull(.Fields("PURCHASESEQTYPE").Value), 0, .Fields("PURCHASESEQTYPE").Value)
                mGSTStatus = IIf(IsDbNull(.Fields("ISGSTAPPLICABLE").Value), "E", .Fields("ISGSTAPPLICABLE").Value) ''IIf(.Fields("ISGSTAPPLICABLE").Value = "Y", vbChecked, vbUnchecked)							
                If mGSTStatus = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                ElseIf mGSTStatus = "R" Then
                    cboGSTStatus.SelectedIndex = 1
                ElseIf mGSTStatus = "E" Then
                    cboGSTStatus.SelectedIndex = 2
                Else
                    cboGSTStatus.SelectedIndex = 3
                End If
                cboGSTStatus.Enabled = False
                txtTotCGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_REFUNDAMT").Value), "", .Fields("TOTCGST_REFUNDAMT").Value), "0.00")
                txtTotSGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_REFUNDAMT").Value), "", .Fields("TOTSGST_REFUNDAMT").Value), "0.00")
                txtTotIGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_REFUNDAMT").Value), "", .Fields("TOTIGST_REFUNDAMT").Value), "0.00")
                txtServNo.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVNo").Value), "", .Fields("SERVNo").Value), "00000")
                txtServDate.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVDate").Value), "", .Fields("SERVDate").Value), "DD/MM/YYYY")
                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = IIf(IsDbNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value)
                txtPONo.Text = IIf(IsDbNull(.Fields("CUSTREFNO").Value), "", .Fields("CUSTREFNO").Value)
                txtPODate.Text = IIf(IsDbNull(.Fields("CUSTREFDATE").Value), "", .Fields("CUSTREFDATE").Value)
                If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                    If MainClass.ValidateWithMasterTable((.Fields("TRNTYPE").Value), "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        cboInvType.Text = MasterNo
                    End If
                    If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                        mBookSubType = MasterNo
                    Else
                        mBookSubType = CStr(-1)
                    End If
                Else
                    mBookSubType = IIf(IsDbNull(.Fields("BOOKSUBTYPE").Value), "", .Fields("BOOKSUBTYPE").Value)
                End If
                txtBillNo.Text = IIf(IsDbNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If
                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtShippedTo.Text = txtSupplier.Text
                Else
                    If MainClass.ValidateWithMasterTable((.Fields("SHIPPED_TO_PARTY_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtShippedTo.Text = MasterNo
                    End If
                End If
                '            If MainClass.ValidateWithMasterTable(.Fields("MODVAT_SUPP_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then							
                '                txtModvatSupp.Text = MasterNo							
                '            End If							
                If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                    If MainClass.ValidateWithMasterTable((.Fields("ACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtDebitAccount.Text = MasterNo
                    End If
                Else
                    txtDebitAccount.Text = ""
                End If
                txtCreditDays(0).Text = IIf(IsDbNull(.Fields("DUEDAYSFROM").Value), "", .Fields("DUEDAYSFROM").Value)
                txtCreditDays(1).Text = IIf(IsDbNull(.Fields("DUEDAYSTO").Value), "", .Fields("DUEDAYSTO").Value)
                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                '            If LblBookCode.text = ConPurchaseBookCode Then							
                '                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)							
                '            Else							
                '                chkCancelled.Enabled = False							
                '            End If							
                chkRejection.CheckState = IIf(.Fields("REJECTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCapital.CheckState = IIf(.Fields("ISCAPITAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                lblTotQty.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                lblTotItemValue.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                lblTotCGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_AMOUNT").Value), "", .Fields("TOTCGST_AMOUNT").Value), "0.00")
                lblTotSGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_AMOUNT").Value), "", .Fields("TOTSGST_AMOUNT").Value), "0.00")
                lblTotIGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_AMOUNT").Value), "", .Fields("TOTIGST_AMOUNT").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                txtTariff.Text = IIf(IsDbNull(.Fields("TARIFFHEADING").Value), "", .Fields("TARIFFHEADING").Value)
                txtItemType.Text = IIf(IsDbNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtNarration.Text = IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtCarriers.Text = IIf(IsDbNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)
                txtVehicle.Text = IIf(IsDbNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                txtDocsThru.Text = IIf(IsDbNull(.Fields("DOCSTHROUGH").Value), "", .Fields("DOCSTHROUGH").Value)
                txtMode.Text = IIf(IsDbNull(.Fields("DESPATCHMODE").Value), "", .Fields("DESPATCHMODE").Value)
                txtPaymentDate.Text = IIf(IsDbNull(.Fields("PAYMENTDATE").Value), "", .Fields("PAYMENTDATE").Value)
                chkTDS.CheckState = IIf(.Fields("ISTDSDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkTDS.Enabled = False
                txtTdsRate.Text = VB6.Format(IIf(IsDbNull(.Fields("TDSPer").Value), "", .Fields("TDSPer").Value), "0.000")
                txtTDSAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TDSAMOUNT").Value), "", .Fields("TDSAMOUNT").Value), "0.00")
                chkESI.CheckState = IIf(.Fields("ISESIDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkESI.Enabled = False
                txtESIRate.Text = VB6.Format(IIf(IsDbNull(.Fields("ESIPer").Value), "", .Fields("ESIPer").Value), "0.000")
                txtESIAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("ESIAMOUNT").Value), "", .Fields("ESIAMOUNT").Value), "0.00")
                ChkSTDS.CheckState = IIf(.Fields("ISSTDSDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                ChkSTDS.Enabled = False
                txtSTDSRate.Text = VB6.Format(IIf(IsDbNull(.Fields("STDSPer").Value), "", .Fields("STDSPer").Value), "0.000")
                txtSTDSAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("STDSAMOUNT").Value), "", .Fields("STDSAMOUNT").Value), "0.00")
                txtTDSDeductOn.Text = VB6.Format(IIf(IsDbNull(.Fields("TDS_DEDUCT_ON").Value), "", .Fields("TDS_DEDUCT_ON").Value), "0.00")
                txtSTDSDeductOn.Text = VB6.Format(IIf(IsDbNull(.Fields("STDS_DEDUCT_ON").Value), "", .Fields("STDS_DEDUCT_ON").Value), "0.00")
                txtESIDeductOn.Text = VB6.Format(IIf(IsDbNull(.Fields("ESI_DEDUCT_ON").Value), "", .Fields("ESI_DEDUCT_ON").Value), "0.00")
                txtJVVNO.Text = IIf(IsDbNull(.Fields("JVNO").Value), "", .Fields("JVNO").Value)
                OptFreight(0).Checked = True
                OptFreight(1).Checked = False
                chkFinalPost.CheckState = IIf(.Fields("ISFINALPOST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkFOC.CheckState = IIf(.Fields("ISFOC").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkFOC.Enabled = IIf(.Fields("ISFOC").Value = "Y", True, False)
                '            If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then							
                '                mCustRefNo = MasterNo							
                '            Else							
                '                mCustRefNo = "-1"							
                '            End If							
                '            txtBalAmount.Text = GetBillBalanceAmt(.Fields("SUPP_CUST_CODE").Value, txtBillNo.Text)							
                mSACCode = IIf(IsDbNull(.Fields("SAC_CODE").Value), "", .Fields("SAC_CODE").Value)
                If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    txtServProvided.Text = Trim(MasterNo)
                Else
                    txtServProvided.Text = ""
                End If
                txtServiceOn.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVICE_ON_AMT").Value), 0, .Fields("SERVICE_ON_AMT").Value), "0.00")
                txtProviderPer.Text = VB6.Format(IIf(IsDbNull(.Fields("SERV_PROVIDER_PER").Value), 0, .Fields("SERV_PROVIDER_PER").Value), "0.00")
                txtRecipientPer.Text = VB6.Format(IIf(IsDbNull(.Fields("SERV_RECIPIENT_PER").Value), 0, .Fields("SERV_RECIPIENT_PER").Value), "0.00")
                txtServiceTaxPer.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVICE_TAX_PER").Value), 0, .Fields("SERVICE_TAX_PER").Value), "0.00")
                txtServiceTaxAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVICE_TAX_AMOUNT").Value), 0, .Fields("SERVICE_TAX_AMOUNT").Value), "0.00")
                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False
                txtAdvVNo.Text = IIf(IsDbNull(.Fields("ADV_VNO").Value), "", .Fields("ADV_VNO").Value)
                txtAdvDate.Text = IIf(IsDbNull(.Fields("ADV_VDATE").Value), "", .Fields("ADV_VDATE").Value)
                txtAdvBal.Text = CStr(GetBalancePaymentAmount((.Fields("SUPP_CUST_CODE").Value), txtBillDate.Text, mVNo, (TxtVDate.Text), mDivisionCode, "AP", mBalCGST, mBalSGST, mBalIGST))
                txtAdvBal.Text = VB6.Format(txtAdvBal.Text, "0.00")
                '            txtAdvCGSTBal.Text = Format(mBalCGST, "0.00")							
                '            txtAdvSGSTBal.Text = Format(mBalSGST, "0.00")							
                '            txtAdvIGSTBal.Text = Format(mBalIGST, "0.00")							
                txtItemAdvAdjust.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_ITEM_AMT").Value), 0, .Fields("ADV_ITEM_AMT").Value), "0.00")
                txtAdvAdjust.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_ADJUSTED_AMT").Value), 0, .Fields("ADV_ADJUSTED_AMT").Value), "0.00")
                txtAdvCGST.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_CGST_AMT").Value), 0, .Fields("ADV_CGST_AMT").Value), "0.00")
                txtAdvSGST.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_SGST_AMT").Value), 0, .Fields("ADV_SGST_AMT").Value), "0.00")
                txtAdvIGST.Text = VB6.Format(IIf(IsDbNull(.Fields("ADV_IGST_AMT").Value), 0, .Fields("ADV_IGST_AMT").Value), "0.00")
                chkRejection.Enabled = False
                chkCapital.Enabled = False
                mAddUser = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")
                '            cmdResetMRR.Enabled = True							
                Call ShowDetail1((LblMKey.Text))
                Call ShowExp1((LblMKey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                ''Call CalcTots							
            End If
        End With
        txtVno.Enabled = True
        '    chkModvat.Enabled = False							
        '    chkSTRefund.Enabled = False							
        chkRejection.Enabled = False
        chkCapital.Enabled = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        FormatSprdMain(-1)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRate, ColRate)
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtMRRNo.Enabled = False
        CmdSearchMRR.Enabled = False
        txtMRRDate.Enabled = False
        If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
            If Val(LblBookCode.Text) = ConModvatBookCode Or Val(LblBookCode.Text) = ConSTClaimBookCode Or Val(LblBookCode.Text) = ConCSTClaimBookCode Or Val(LblBookCode.Text) = ConServiceClaimBookCode Then
                cboInvType.Enabled = False
            Else
                cboInvType.Enabled = MainClass.GetUserCanModify(TxtVDate.Text) ''IIf(PubUserLevel = 1 Or PubUserLevel = 2, True, False)							
            End If
        Else
            cboInvType.Enabled = False
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Function ShowFromExcise1(ByRef mRsDC As ADODB.Recordset) As Boolean
        On Error GoTo ERR1
        'Dim mCustRefNo As String							
        Dim mFormCode As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mGSTStatus As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        With mRsDC
            If Not .EOF Then
                '            txtVNoPrefix.Text = IIf(IsNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value)							
                '							
                '            If .Fields("VNOSEQ").Value = -1 Then							
                '							
                '            Else							
                '                txtVNo.Text = Format(IIf(IsNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")							
                '            End If							
                '            txtVNoSuffix.Text = IIf(IsNull(.Fields("VNOSUFFIX").Value), "", .Fields("VNOSUFFIX").Value)							
                '            txtVDate.Text = Format(IIf(IsNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")							
                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = IIf(IsDbNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value)
                If .Fields("ISFINALPOST").Value = "Y" Then
                    MsgInformation("Account Entry (P" & VB6.Format(IIf(IsDbNull(.Fields("VNOSEQ").Value) Or .Fields("VNOSEQ").Value = "-1", "", .Fields("VNOSEQ").Value), "00000") & ") Already made Against This MRR")
                    ShowFromExcise1 = False
                    Exit Function
                End If
                If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "MRR_FINAL_FLAG", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        MsgInformation("Please Check This MRR Made FOC")
                        ShowFromExcise1 = False
                        Exit Function
                    End If
                End If
                If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                    If .Fields("ISGSTAPPLICABLE").Value = "Y" Then
                        MsgInformation("GST Entry (" & VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_NO").Value) Or .Fields("GST_CLAIM_NO").Value = "-1", "", .Fields("GST_CLAIM_NO").Value), "00000") & ") Already made Against This MRR")
                        ShowFromExcise1 = False
                        Exit Function
                    End If
                End If
                lblPMKey.Text = .Fields("MKey").Value
                LblMKey.Text = ""
                lblPurchaseVNo.Text = VB6.Format(IIf(IsDbNull(.Fields("VNOSEQ").Value) Or .Fields("VNOSEQ").Value = "-1", "", .Fields("VNOSEQ").Value), "00000")
                mGSTStatus = IIf(.Fields("ISGSTAPPLICABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If mGSTStatus = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                ElseIf mGSTStatus = "R" Then
                    cboGSTStatus.SelectedIndex = 1
                ElseIf mGSTStatus = "E" Then
                    cboGSTStatus.SelectedIndex = 2
                Else
                    cboGSTStatus.SelectedIndex = 3
                End If
                cboGSTStatus.Enabled = False
                chkCreditRC.CheckState = IIf(.Fields("GST_RC_CLAIM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If chkCreditRC.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtModvatNo.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_RC_NO").Value), "", .Fields("GST_CLAIM_RC_NO").Value), "00000")
                    txtModvatDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_RC_DATE").Value), "", .Fields("GST_CLAIM_RC_DATE").Value), "DD/MM/YYYY")
                Else
                    txtModvatNo.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value), "00000")
                    txtModvatDate.Text = VB6.Format(IIf(IsDbNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                End If
                If Trim(txtModvatNo.Text) <> "" Then
                    chkCapital.Enabled = False
                End If
                txtTotCGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTCGST_REFUNDAMT").Value), "", .Fields("TOTCGST_REFUNDAMT").Value), "0.00")
                txtTotSGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSGST_REFUNDAMT").Value), "", .Fields("TOTSGST_REFUNDAMT").Value), "0.00")
                txtTotIGSTRefund.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTIGST_REFUNDAMT").Value), "", .Fields("TOTIGST_REFUNDAMT").Value), "0.00")
                lblTotCGSTAmount.Text = IIf(IsDbNull(.Fields("TOTCGST_AMOUNT").Value), "", .Fields("TOTCGST_AMOUNT").Value)
                lblTotSGSTAmount.Text = IIf(IsDbNull(.Fields("TOTSGST_AMOUNT").Value), "", .Fields("TOTSGST_AMOUNT").Value)
                lblTotIGSTAmount.Text = IIf(IsDbNull(.Fields("TOTIGST_AMOUNT").Value), "", .Fields("TOTIGST_AMOUNT").Value)
                txtServNo.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVNo").Value), "", .Fields("SERVNo").Value), "00000")
                txtServDate.Text = VB6.Format(IIf(IsDbNull(.Fields("SERVDate").Value), "", .Fields("SERVDate").Value), "DD/MM/YYYY")
                lblServicePercentage.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTSERVICEPERCENT").Value), 0, .Fields("TOTSERVICEPERCENT").Value), "0.00")
                txtPONo.Text = IIf(IsDbNull(.Fields("CUSTREFNO").Value), "", .Fields("CUSTREFNO").Value)
                txtPODate.Text = IIf(IsDbNull(.Fields("CUSTREFDATE").Value), "", .Fields("CUSTREFDATE").Value)
                txtBillNo.Text = IIf(IsDbNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                mSupplierCode = IIf(IsDbNull(.Fields("SUPP_CUST_CODE").Value), -1, .Fields("SUPP_CUST_CODE").Value) 'DEEPAK 10_09_2004							
                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If
                If MainClass.ValidateWithMasterTable((.Fields("ACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDebitAccount.Text = MasterNo
                End If
                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If MainClass.ValidateWithMasterTable((.Fields("SHIPPED_TO_PARTY_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtShippedTo.Text = MasterNo
                End If
                txtCreditDays(0).Text = IIf(IsDbNull(.Fields("DUEDAYSFROM").Value), "", .Fields("DUEDAYSFROM").Value)
                txtCreditDays(1).Text = IIf(IsDbNull(.Fields("DUEDAYSTO").Value), "", .Fields("DUEDAYSTO").Value)
                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                '            If LblBookCode.text = ConPurchaseBookCode Then							
                '                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)							
                '            Else							
                '                chkCancelled.Enabled = False							
                '            End If							
                chkRejection.CheckState = IIf(.Fields("REJECTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCapital.CheckState = IIf(.Fields("ISCAPITAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                lblTotQty.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                lblTotItemValue.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                txtTariff.Text = IIf(IsDbNull(.Fields("TARIFFHEADING").Value), "", .Fields("TARIFFHEADING").Value)
                txtItemType.Text = IIf(IsDbNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtNarration.Text = IIf(IsDbNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtCarriers.Text = IIf(IsDbNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)
                txtVehicle.Text = IIf(IsDbNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                txtDocsThru.Text = IIf(IsDbNull(.Fields("DOCSTHROUGH").Value), "", .Fields("DOCSTHROUGH").Value)
                txtMode.Text = IIf(IsDbNull(.Fields("DESPATCHMODE").Value), "", .Fields("DESPATCHMODE").Value)
                OptFreight(0).Checked = True
                OptFreight(1).Checked = False
                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False
                chkFinalPost.CheckState = IIf(.Fields("ISFINALPOST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                '							
                '            If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then							
                '                mCustRefNo = MasterNo							
                '            Else							
                '                mCustRefNo = "-1"							
                '            End If							
                txtAdvBal.Text = CStr(GetBalancePaymentAmount(mSupplierCode, txtBillDate.Text, "", "", mDivisionCode, "AP", mBalCGST, mBalSGST, mBalIGST))
                '            txtAdvCGSTBal.Text = Format(mBalCGST, "0.00")							
                '            txtAdvSGSTBal.Text = Format(mBalSGST, "0.00")							
                '            txtAdvIGSTBal.Text = Format(mBalIGST, "0.00")							
                Call ShowDetail1((.Fields("mKey").Value))
                Call ShowExp1((.Fields("mKey").Value))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                ''Call CalcTots							
            End If
        End With
        ShowFromExcise1 = True
        FormatSprdMain(-1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Function
ERR1:
        ShowFromExcise1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume							
    End Function
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
    Private Sub ShowDetail1(ByRef mMkey As String)
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
        Dim mMRRNO As Double
        SqlStr = ""
        SqlStr = " SELECT FIN_PURCHASE_DET.*, "
        '    If mCustRefType = "I" Or mCustRefType = "2" Then							
        '        If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "REF_PO_NO", "INV_GATE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then							
        '            mBillNo = MasterNo							
        '        Else							
        '            mBillNo = -1							
        '        End If							
        '        mBillNo = IIf(Val(mBillNo) = 0, -1, mBillNo)							
        '							
        '        SqlStr = SqlStr & " GetSALEITEMPRICE(" & mBillNo & ",CUST_REF_NO, '" & mSupplierCode & "',ITEM_CODE) AS PORATE, "							
        '        SqlStr = SqlStr & " 0 AS VOL_DISCRATE "							
        '    ElseIf mCustRefType = "P" Then							
        SqlStr = SqlStr & " GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CUST_REF_NO, ITEM_CODE) AS PORATE, "
        SqlStr = SqlStr & " GetVOL_DISC_ITEM(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CUST_REF_NO, ITEM_CODE) AS VOL_DISCRATE "
        '    ElseIf mCustRefType = "R" Then							
        '        SqlStr = SqlStr & " GetITEMJWRate(" & RsCompany.fields("COMPANY_CODE").value & ",1,TO_DATE('" & vb6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "'),CUST_REF_NO, " & Val(txtMRRNo.Text) & ", ITEM_CODE,SUBROWNO) AS PORATE, "							
        '        SqlStr = SqlStr & " 0 AS VOL_DISCRATE "							
        '    Else							
        '        SqlStr = SqlStr & " 0 AS PORATE, "							
        '        SqlStr = SqlStr & " 0 AS VOL_DISCRATE "							
        '    End If							
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_DET " & vbCrLf & " Where Mkey='" & mMkey & "'" & vbCrLf & " Order By SubRowNo"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I
                SprdMain.Col = ColMRRNo
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("MRRNO").Value), "", .Fields("MRRNO").Value)))
                mMRRNO = Val(IIf(IsDbNull(.Fields("MRRNO").Value), "", .Fields("MRRNO").Value))
                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode
                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc ''IIf(IsNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)							
                SprdMain.Col = ColPartNo
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mPartNo = MasterNo
                SprdMain.Text = mPartNo ''IIf(IsNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)							
                If lblPurchaseType.Text = "G" Then
                    mHSNCode = GetHSNCode(mItemCode)
                Else
                    mHSNCode = GetSACCode((txtServProvided.Text))
                End If
                SprdMain.Col = ColHSN
                SprdMain.Text = mHSNCode
                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))
                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Col = ColPORate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value)))
                SprdMain.Col = ColVolDiscRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("VOL_DISCRATE").Value), 0, .Fields("VOL_DISCRATE").Value)))
                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))
                SprdMain.Col = ColTaxableAmount
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("GSTABLE_AMT").Value), 0, .Fields("GSTABLE_AMT").Value)))
                SprdMain.Col = ColPONo
                SprdMain.Text = CStr(IIf(IsDbNull(.Fields("CUST_REF_NO").Value), "", .Fields("CUST_REF_NO").Value))
                '            mHSNCode = IIf(IsNull(!HSNCODE), "", !HSNCODE)							
                '							
                '            If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer) = False Then GoTo ERR1							
                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))
                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))
                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))
                '            SprdMain.Col = ColEDRate							
                '            SprdMain.Text = CStr(IIf(IsNull(.Fields("ITEM_ED_PER").Value), "", .Fields("ITEM_ED_PER").Value))							
                '            If xRefType = "I" Then							
                '                If Trim(txtPONo.Text) <> "" Then							
                '                    .Text = "S" & vb6.Format(Mid(.Text, 1, Len(.Text) - 6), "00000")							
                '                End If							
                '            End If							
                SprdMain.Col = ColInvType
                If IsDbNull(.Fields("ITEM_TRNTYPE").Value) = True Then
                    SprdMain.Text = Trim(cboInvType.Text)
                Else
                    If MainClass.ValidateWithMasterTable(.Fields("ITEM_TRNTYPE").Value, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    Else
                        SprdMain.Text = Trim(cboInvType.Text)
                    End If
                End If
                SqlStr = ""
                SqlStr = " SELECT RECEIVED_QTY,SHORTAGE_QTY,REJECTED_QTY,ITEM_RATE, " & vbCrLf & " GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",AUTO_KEY_MRR,MRR_DATE,SUPP_CUST_CODE,ITEM_CODE) AS REOFFER " & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " Where AUTO_KEY_MRR=" & Val(CStr(mMRRNO)) & "" & vbCrLf & " AND ITEM_CODE='" & RsPurchDetail.Fields("ITEM_CODE").Value & "' and SERIAL_NO=" & Val(CStr(I)) & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    SprdMain.Col = ColAcceptedQty
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("RECEIVED_QTY").Value), 0, RsTemp.Fields("RECEIVED_QTY").Value)))
                    SprdMain.Col = ColShortageQty
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("SHORTAGE_QTY").Value), 0, RsTemp.Fields("SHORTAGE_QTY").Value)))
                    mShortageQty = Val(SprdMain.Text)
                    SprdMain.Col = ColRejectedQty
                    mReOffer = IIf(IsDbNull(RsTemp.Fields("REOFFER").Value), 0, RsTemp.Fields("REOFFER").Value)
                    mRejQty = IIf(IsDbNull(RsTemp.Fields("REJECTED_QTY").Value), 0, RsTemp.Fields("REJECTED_QTY").Value)
                    SprdMain.Text = CStr(Val(CStr(mRejQty))) ''Val(mRejQty - mReOffer)							
                    '                SprdMain.Col = ColPORate							
                    '                SprdMain.Text = Val(IIf(IsNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value))							
                End If
                '            SprdMain.Col = ColModvatableAmount							
                '            SprdMain.Text = (IIf(IsNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)) * mShortageQty							
                '							
                '            SprdMain.Col = ColSTRefundableAmount							
                '            SprdMain.Text = (IIf(IsNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)) * mShortageQty							
                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume							
    End Sub
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh							
            FormatSprdView()
            SprdView.Focus()
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
        Dim mItemCode As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim mShortage As Double
        Dim mTotItemAmount As Double
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
        Dim mTotTaxableItemAmount As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        pRound = 0
        mQty = 0
        mRate = 0
        '    mST = 0							
        mItemAmount = 0
        mTotItemAmount = 0
        mOtherTaxableAmount = 0
        mTotExp = 0
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
                If mIsTaxable = "Y" Then
                    .Col = ColExpAmt
                    mOtherTaxableAmount = mOtherTaxableAmount + CDbl(VB6.Format(.Text, "0.00"))
                End If
            Next
        End With
        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc
                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc
                mItemCode = .Text
                .Col = ColQty
                mQty = Val(.Text)
                .Col = ColShortageQty
                If Val(.Text) >= 0 Then
                    mShortage = mQty - Val(.Text)
                Else
                    mShortage = mQty
                End If
                mTotQty = mTotQty + mQty
                .Col = ColRate
                mRate = Val(.Text)
                .Text = CStr(mRate)
                .Col = ColAmount
                .Text = VB6.Format(mQty * mRate, "0.00")
                mTotItemAmount = mTotItemAmount + CDbl(VB6.Format(mQty * mRate, "0.00"))
DontCalc:
            Next I
        End With
        mTotTaxableItemAmount = mTotItemAmount + mOtherTaxableAmount
        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc1
                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc1
                mItemCode = .Text
                .Col = ColQty
                mQty = Val(.Text)
                .Col = ColShortageQty
                If Val(.Text) >= 0 Then
                    mShortage = mQty - Val(.Text)
                Else
                    mShortage = mQty
                End If
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
                mItemAmount = CDbl(VB6.Format(mQty * mRate, "0.00"))
                .Col = ColTaxableAmount
                mGSTableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00")) '' Format(Val(.Text), "0.00")							
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
                If mCompanyGSTNo = mPartyGSTNo Then
                    pTotCGSTRefundAmount = 0
                    pTotSGSTRefundAmount = 0
                    pTotIGSTRefundAmount = 0
                Else
                    pTotCGSTRefundAmount = pTotCGSTRefundAmount + mCGSTAmount ''Format(mQty * mRate * pCGSTPer * 0.01, "0.00")							
                    pTotSGSTRefundAmount = pTotSGSTRefundAmount + mSGSTAmount ''Format(mQty * mRate * pSGSTPer * 0.01, "0.00")							
                    pTotIGSTRefundAmount = pTotIGSTRefundAmount + mIGSTAmount ''Format(mQty * mRate * pIGSTPer * 0.01, "0.00")							
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
        If mCompanyGSTNo = mPartyGSTNo Then
            lblNetAmount.Text = VB6.Format(mTotItemAmount + mTotExp, "#0.00")
        Else
            If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                lblNetAmount.Text = VB6.Format(mTotItemAmount + pTotCGSTAmount + pTotSGSTAmount + pTotIGSTAmount + mTotExp, "#0.00")
            Else
                lblNetAmount.Text = VB6.Format(mTotItemAmount + mTotExp, "#0.00")
            End If
        End If
        lblTotTaxableAmt.Text = VB6.Format(Val(CStr(mTotItemAmount + mOtherTaxableAmount)), "#0.00")
        lblTotFreight.Text = CStr(0) ''Format(pTotOthers, "#0.00")							
        lblTotCharges.Text = VB6.Format(pTotOthers, "#0.00") ''Format(mRO, "#0.00")							
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")
        lblTotCGSTAmount.Text = VB6.Format(pTotCGSTAmount, "#0.00")
        lblTotSGSTAmount.Text = VB6.Format(pTotSGSTAmount, "#0.00")
        lblTotIGSTAmount.Text = VB6.Format(pTotIGSTAmount, "#0.00")
        If VB.Left(cboGSTStatus.Text, 1) = "G" Then 'If chkFinalPost.Value = vbUnchecked Then							
            txtTotCGSTRefund.Text = VB6.Format(pTotCGSTRefundAmount, "#0.00")
            txtTotSGSTRefund.Text = VB6.Format(pTotSGSTRefundAmount, "#0.00")
            txtTotIGSTRefund.Text = VB6.Format(pTotIGSTRefundAmount, "#0.00")
        End If
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSDeductOn.Text = VB6.Format(IIf(Val(txtTDSDeductOn.Text) = 0, lblNetAmount.Text, txtTDSDeductOn.Text), "#0.00")
        Else
            txtTDSDeductOn.Text = VB6.Format(lblNetAmount.Text, "#0.00")
        End If
        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIDeductOn.Text = VB6.Format(IIf(Val(txtESIDeductOn.Text) = 0, lblNetAmount.Text, txtESIDeductOn.Text), "#0.00")
        Else
            txtESIDeductOn.Text = VB6.Format(lblNetAmount.Text, "#0.00")
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSDeductOn.Text = VB6.Format(IIf(Val(txtSTDSDeductOn.Text) = 0, lblNetAmount.Text, txtSTDSDeductOn.Text), "#0.00")
        Else
            txtSTDSDeductOn.Text = VB6.Format(lblNetAmount.Text, "#0.00")
        End If
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtTdsRate.Text) * Val(txtTDSDeductOn.Text) / 100, 0), "0.00")
            Else
                txtTDSAmount.Text = VB6.Format(Val(txtTdsRate.Text) * Val(txtTDSDeductOn.Text) / 100, "0.00")
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
        Call CheckPORate()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Sub
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
    Private Sub CalcTotsDNCN(ByRef pMKey As String, ByRef pDnCnType As String, ByRef xItemValue As Double, ByRef xTOTFREIGHT As Double, ByRef xTOTCHARGES As Double, ByRef xTotDiscount As Double, ByRef xMSC As Double, ByRef xRO As Double, ByRef xTOTEXPAMT As Double, ByRef xNETVALUE As Double, ByRef xTotQty As Double, ByRef xCGSTPer As Double, ByRef xSGSTPer As Double, ByRef xIGSTPer As Double, ByRef xCGSTAmount As Double, ByRef xSGSTAmount As Double, ByRef xIGSTAmount As Double, ByRef xCGSTRefundAmount As Double, ByRef xSGSTRefundAmount As Double, ByRef xIGSTRefundAmount As Double)
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mDiscount As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mTotQty As Double
        Dim mTotItemAmount As Double
        Dim mTotExp As Double
        Dim mTotDiscount As Double
        Dim j As Integer
        Dim I As Integer
        Dim mST As Decimal
        Dim mItemCode As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim mOTRCharges As Double
        Dim mRO As Double
        Dim mExp As Double
        Dim mRoType As String
        Dim mExpAddDeduct As String
        Dim mMSC As Double
        Dim mExpCode As Integer
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mExpAmount As Double
        pRound = 0
        mQty = 0
        mRate = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mItemValue = 0
        mTotExp = 0
        SqlStr = "SELECT * FROM FIN_DNCN_DET WHERE MKEY='" & pMKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            Do While Not RsMisc.EOF
                mItemCode = IIf(IsDbNull(RsMisc.Fields("ITEM_CODE").Value), "", RsMisc.Fields("ITEM_CODE").Value)
                If mItemCode = "" Then GoTo DontCalc
                mQty = IIf(IsDbNull(RsMisc.Fields("ITEM_QTY").Value), "", RsMisc.Fields("ITEM_QTY").Value)
                mTotQty = mTotQty + mQty
                mRate = IIf(IsDbNull(RsMisc.Fields("ITEM_RATE").Value), "", RsMisc.Fields("ITEM_RATE").Value)
                mItemAmount = IIf(IsDbNull(RsMisc.Fields("ITEM_AMT").Value), "", RsMisc.Fields("ITEM_AMT").Value)
                mTotItemAmount = mTotItemAmount + mItemAmount
                mItemValue = CDbl(VB6.Format(mItemAmount, "0.00"))
                mCGSTAmount = mCGSTAmount + IIf(IsDbNull(RsMisc.Fields("CGST_AMOUNT").Value), 0, RsMisc.Fields("CGST_AMOUNT").Value)
                mSGSTAmount = mSGSTAmount + IIf(IsDbNull(RsMisc.Fields("SGST_AMOUNT").Value), 0, RsMisc.Fields("SGST_AMOUNT").Value)
                mIGSTAmount = mIGSTAmount + IIf(IsDbNull(RsMisc.Fields("IGST_AMOUNT").Value), 0, RsMisc.Fields("IGST_AMOUNT").Value)
DontCalc:
                RsMisc.MoveNext()
            Loop
        End If
        mNetAccessAmt = Val(CStr(mTotItemAmount))
        SqlStr = "SELECT EXP.MKEY ,EXP.SUBROWNO, EXP.EXPCODE, EXP.EXPPERCENT, " & vbCrLf & " EXP.AMOUNT, EXP.CALCON, EXP.RO,  " & vbCrLf & " IMST.IDENTIFICATION,ADD_DED,EXCISEABLE,TAXABLE,CESSABLE " & vbCrLf & " FROM FIN_DNCN_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf & " WHERE EXP.MKEY='" & pMKey & "'" & vbCrLf & " AND IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=IMST.CODE" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            Do While Not RsMisc.EOF
                mRoType = IIf(IsDbNull(RsMisc.Fields("RO").Value), "N", RsMisc.Fields("RO").Value)
                xStr = IIf(IsDbNull(RsMisc.Fields("Identification").Value), "", RsMisc.Fields("Identification").Value)
                mExpPercent = IIf(IsDbNull(RsMisc.Fields("EXPPERCENT").Value), "0", RsMisc.Fields("EXPPERCENT").Value)
                mExpAddDeduct = IIf(IsDbNull(RsMisc.Fields("ADD_DED").Value), "A", RsMisc.Fields("ADD_DED").Value)
                mExpCode = IIf(IsDbNull(RsMisc.Fields("EXPCODE").Value), "-1", RsMisc.Fields("EXPCODE").Value)
                mExpAmount = IIf(IsDbNull(RsMisc.Fields("Amount").Value), "0", RsMisc.Fields("Amount").Value)
                Select Case xStr
                    Case "DOB"
                        '                    If mExpPercent <> 0 Then							
                        mDiscount = mExpAmount
                        If mRoType = "Y" Then
                            mDiscount = System.Math.Round(Val(CStr(mExpAmount)), 0)
                        End If
                        '                    End If							
                        mTotDiscount = mTotDiscount + (mDiscount * IIf(mExpAddDeduct = "D", -1, 1))
                        mNetAccessAmt = Val(CStr(mNetAccessAmt)) - Val(CStr(mDiscount))
                        mExp = mDiscount
                    Case "MSC"
                        mMSC = mMSC + (Val(CStr(mExpAmount)) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(CStr(mExpAmount))
                    Case "OTR", "FRO", "TOL"
                        mOTRCharges = mOTRCharges + (Val(CStr(mExpAmount)) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(CStr(mExpAmount))
                    Case "RO"
                        mRO = mRO + (Val(CStr(mExpAmount)) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(CStr(mExpAmount))
                End Select
                If xStr = "RO" Then
                    mTotExp = mTotExp + mExp
                Else
                    mTotExp = mTotExp + IIf(mExpAddDeduct = "D", -mExp, mExp)
                End If
                mExp = 0
DontCalc1:
                RsMisc.MoveNext()
            Loop
        End If
        xItemValue = CDbl(VB6.Format(mTotItemAmount, "#0.00"))
        xCGSTAmount = CDbl(VB6.Format(mCGSTAmount, "#0.00")) ' mCGSTAmount							
        xSGSTAmount = CDbl(VB6.Format(mSGSTAmount, "#0.00")) ' mSGSTAmount							
        xIGSTAmount = CDbl(VB6.Format(mIGSTAmount, "#0.00")) ' mIGSTAmount							
        '    If pDnCnType = "R" Then							
        xCGSTRefundAmount = CDbl(VB6.Format(mCGSTAmount, "#0.00"))
        xSGSTRefundAmount = CDbl(VB6.Format(mSGSTAmount, "#0.00"))
        xIGSTRefundAmount = CDbl(VB6.Format(mIGSTAmount, "#0.00"))
        '    End If							
        xNETVALUE = CDbl(VB6.Format(System.Math.Abs(mTotExp + xCGSTAmount + xSGSTAmount + xIGSTAmount + mTotItemAmount), "#0.00"))
        xTOTFREIGHT = CDbl(VB6.Format(mOTRCharges, "#0.00"))
        xTOTCHARGES = 0 ''Format(mRO, "#0.00")							
        xTOTEXPAMT = CDbl(VB6.Format(mTotExp, "#0.00"))
        xRO = CDbl(VB6.Format(mRO, "#0.00"))
        xTotDiscount = CDbl(VB6.Format(mTotDiscount, "#0.00"))
        xMSC = CDbl(VB6.Format(mMSC, "#0.00"))
        xTotQty = CDbl(VB6.Format(mTotQty, "#0.00"))
        Exit Sub
ERR1:
        ''Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume							
    End Sub
    Private Sub Clear1()
        pShowCalc = False
        LblMKey.Text = ""
        lblPMKey.Text = ""
        mSupplierCode = CStr(-1)
        lblPurchaseVNo.Text = ""
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
        txtBillNo.Enabled = True
        txtBillDate.Enabled = True
        lblClaimStatus.Text = ""
        SSTab1.SelectedIndex = 0
        SSTabLevies.SelectedIndex = 0
        txtVno.Text = ""
        txtVNoPrefix.Text = mBookType
        lblSaleBillNoSeq.Text = ""
        lblSaleBillNo.Text = ""
        lblSaleBillDate.Text = ""
        txtVNoSuffix.Text = ""
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If Not IsDate(TxtVDate.Text) Then
                TxtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
            End If
            '        chkCancelled.Enabled = True							
        Else
            TxtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
            '        chkCancelled.Enabled = False							
        End If
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""
        '4-07-2003 Commit on Mukesh Demand....							
        cboInvType.SelectedIndex = -1
        txtBillNo.Text = ""
        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtSupplier.Text = ""
        '    txtModvatSupp.Text = ""							
        txtShippedTo.Text = ""
        txtShippedTo.Enabled = False
        chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkShipTo.Enabled = False
        txtDebitAccount.Text = ""
        txtCreditDays(0).Text = ""
        txtCreditDays(1).Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCancelled.Enabled = False
        chkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboGSTStatus.SelectedIndex = -1
        cboGSTStatus.Enabled = True
        txtTariff.Text = ""
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
        cboDivision.Enabled = False
        txtServNo.Text = ""
        txtServDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkRejection.CheckState = System.Windows.Forms.CheckState.Unchecked
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblTotQty.Text = "0.00"
        lblTotCGSTAmount.Text = "0.00"
        lblTotSGSTAmount.Text = "0.00"
        lblTotIGSTAmount.Text = "0.00"
        lblTotOtherExp.Text = "0.00"
        txtTotCGSTRefund.Text = "0.00"
        txtTotSGSTRefund.Text = "0.00"
        txtTotIGSTRefund.Text = "0.00"
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotFreight.Text = VB6.Format(0, "#0.00")
        lblTotCharges.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")
        lblDiscount.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        lblMSC.Text = VB6.Format(0, "#0.00")
        txtPaymentDate.Text = ""
        chkCreditRC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFinalPost.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFOC.Enabled = False
        txtPaymentDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
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
        txtTDSDeductOn.Text = "0.00"
        txtESIDeductOn.Text = "0.00"
        txtSTDSDeductOn.Text = "0.00"
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
        MainClass.ClearGrid(SprdPostingDetail)
        Call FormatSprdPostingDetail(-1)
        pProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)
        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""
        chkRejection.Enabled = True
        chkCapital.Enabled = True
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
        MainClass.ClearGrid(SprdExp)
        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = IIf(MasterNo = "Y", "L", "C")
            Else
                mLocal = ""
            End If
        Else
            mLocal = ""
        End If
        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (Type='P' OR Type='B') "
        If PubGSTApplicable = True Then
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
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)
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
    Private Sub FrmPurchaseShipGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmPurchaseShipGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub FrmPurchaseShipGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim x As Boolean
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection							
        ''PvtDBCn.Open StrConn							
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        '    If InStr(1, XRIGHT, "D", vbTextCompare) > 1 Then							
        '        chkCancelled.Enabled = True							
        '    Else							
        '        chkCancelled.Enabled = False							
        '    End If							
        txtVNoPrefix.Text = mBookType
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtVno.Enabled = True
        txtModvatNo.Enabled = False
        '    txtStClaimNo.Enabled = False							
        txtServNo.Enabled = False
        txtMRRNo.Enabled = True
        CmdSearchMRR.Enabled = True
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7755) '8000							
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900							
        SSTab1.SelectedIndex = 0
        'AdoDCMain.Visible = False
        txtSupplier.Enabled = False
        txtBillDate.Enabled = False
        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        ' Control displays text tips aligned to pointer with focus							
        SprdMain.TextTip = FPSpreadADO.TextTipConstants.TextTipFloatingFocusOnly
        ' Control displays text tips after 250 milliseconds							
        SprdMain.TextTipDelay = 250
        ' Text tip displays custom font and colors							
        ' Background is yellow, RGB(255, 255, 0)							
        ' Foreground is dark blue, RGB(0, 0, 128)							
        x = SprdMain.SetTextTipAppearance("Arial", CShort("10"), False, False, &HFFFF, &H800000)

        FormActive = False

        Call FrmPurchaseShipGST_Activated(eventSender, eventArgs)
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
        Static ESCol As Integer
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
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColInvType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColInvType, 0))
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
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text) '' .SetNumericField(KeyAscii)							
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
    Private Sub txtDebitAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebitAccount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDebitAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebitAccount.DoubleClick
        On Error GoTo ErrPart
        If MainClass.SearchGridMaster((txtDebitAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDebitAccount.Text = AcName
            'txtMRRNo_Validate False							
            txtDebitAccount.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDebitAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDebitAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDebitAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDebitAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDebitAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDebitAccount_DoubleClick(txtDebitAccount, New System.EventArgs())
    End Sub
    Private Sub txtDebitAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDebitAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDebitAccount.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            Cancel = True
            ErrorMsg("Please Enter the Valid Credit Account.", "", MsgBoxStyle.Critical)
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMRRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.DoubleClick
        CmdSearchMRR_Click(CmdSearchMRR, New System.EventArgs())
    End Sub
    Private Sub txtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then CmdSearchMRR_Click(CmdSearchMRR, New System.EventArgs())
    End Sub
    Private Sub txtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mDataFill As Boolean
        Dim pMsg As String
        Dim mBillNo As String
        Dim mBillDate As String
        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub
        If txtMRRNo.Enabled = False Then GoTo EventExitSub
        If Trim(txtBillNo.Text) = "" Then
            MsgInformation("Please Enter the Bill No First.")
            GoTo EventExitSub
        End If
        If Trim(txtBillDate.Text) = "" Then
            MsgInformation("Please Enter the Bill Date First.")
            GoTo EventExitSub
        End If
        mBillNo = Trim(txtBillNo.Text)
        mBillDate = VB6.Format(txtBillDate.Text, "DD/MM/YYYY")
        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mDataFill = False
        If SprdMain.MaxRows > 1 Then
            If MsgQuestion("Data Already Popolated in Detail, Want to Add More MRR in same Bill.") = CStr(MsgBoxResult.No) Then
                Cancel = True
                GoTo EventExitSub
            End If
            If ValidateDetailData(CDbl(Trim(txtMRRNo.Text)), pMsg) = False Then
                MsgBox(pMsg, MsgBoxStyle.Critical)
                Cancel = True
                GoTo EventExitSub
            End If
            mDataFill = True
        End If
        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND CANCELLED='N'" '' AND ISFINALPOST='N'"							
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                If SendMrrToAccount = False Then
                    MsgBox("MRR not Send By Store.", MsgBoxStyle.Critical)
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If
            If mDataFill = False Then
                Clear1()
            End If
            If ShowFromExcise1(RsTemp) = False Then
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND SHIPPED_TO_SAMEPARTY='N'"
            If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE='R'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE<>'R'"
            End If
            If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                SqlStr = SqlStr & vbCrLf & " AND GST_STATUS='N'"
            ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                SqlStr = SqlStr & vbCrLf & " AND SEND_AC_FLAG='Y'"
            End If
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                If mDataFill = False Then
                    Clear1()
                End If
                txtBillNo.Text = Trim(mBillNo)
                txtBillDate.Text = VB6.Format(mBillDate, "DD/MM/YYYY")
                '            If RsTemp.Fields("MRR_FINAL_FLAG").value = "Y" Then							
                '                ErrorMsg "Please Enter Vaild MRR No.", "", vbCritical							
                '                Cancel = True							
                '            End If							
                If ShowFromMRRMain(RsTemp, mDataFill) = False Then
                    Cancel = True
                    GoTo EventExitSub
                End If
            Else
                ErrorMsg("Either InValid MRR No. OR Not Send to Account.", "", MsgBoxStyle.Critical)
                Cancel = True
            End If
        End If
        '    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "PORATEEDITABLE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then							
        '        If MasterNo = "Y" Then							
        '            MainClass.UnProtectCell SprdMain, 1, SprdMain.MaxRows, ColPORate, ColPORate							
        '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColRejectedQty							
        '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColUnit, ColUnit							
        '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColAmount, ColAmount							
        '        End If							
        '    End If							
        '    If ADDMode = True Then							
        '        Call FillExpFromPartyExp							
        '    End If							
        FormatSprdMain(-1)
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function ValidateDetailData(ByRef pMRRNo As Double, ByRef pMsg As String) As Boolean
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mDataFill As Boolean
        Dim cntRow As Integer
        Dim mSupplierCode As String
        Dim mShippedFrom As String
        pMsg = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColMRRNo
                If Val(.Text) = pMRRNo Then
                    pMsg = "MRR Already Populated."
                    ValidateDetailData = False
                    Exit Function
                End If
            Next
        End With
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If
        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mShippedFrom = MasterNo
        End If
        SqlStr = "SELECT * " & vbCrLf & " FROM INV_GATE_HDR " & vbCrLf & " WHERE AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShippedFrom) & "'" & vbCrLf & " AND SHIPPED_TO_PARTY_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf & " AND SHIPPED_TO_SAMEPARTY='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            ValidateDetailData = True
        Else
            pMsg = "Supplier Ship From and Supplier Name not Match with MRR."
            ValidateDetailData = False
            Exit Function
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
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
    Private Sub txtTariff_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTariff_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTariff.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function ShowFromMRRMain(ByRef mRsDC As ADODB.Recordset, ByRef mDataFill As Boolean) As Boolean
        On Error GoTo ErrPart
        Dim mFormCode As Integer
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mShipTo As String
        Dim mShipToCode As String
        Dim mGSTType As String
        Dim pServName As String
        Dim mMrrRefType As String
        Dim mIsGSTReg As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        '    txtMRRNo.Text = IIf(IsNull(mRsDC.Fields("AUTO_KEY_MRR").Value), 0, mRsDC.Fields("AUTO_KEY_MRR").Value)							
        If mRsDC.Fields("MRR_FINAL_FLAG").Value = "Y" Then
            MsgInformation("Account Entry Already made Against This MRR")
            ShowFromMRRMain = False
            Exit Function
        End If
        mMrrRefType = mRsDC.Fields("REF_TYPE").Value
        txtMRRDate.Text = IIf(IsDbNull(mRsDC.Fields("MRR_DATE").Value), "", mRsDC.Fields("MRR_DATE").Value)
        mShipTo = "N"
        mShipToCode = ""
        mGSTType = "E"
        If GetShipToFromPO((mRsDC.Fields("AUTO_KEY_MRR").Value), mMrrRefType, mGSTType, mShipTo, mShipToCode, pServName) = False Then GoTo ErrPart
        If mMrrRefType = "I" Or mMrrRefType = "1" Or mMrrRefType = "2" Or mMrrRefType = "3" Then
            mShipTo = "Y"
            mShipToCode = mSupplierCode
            cboGSTStatus.SelectedIndex = 0
        ElseIf mMrrRefType = "R" Then
            If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mIsGSTReg = MasterNo
            End If
            cboGSTStatus.SelectedIndex = IIf(mIsGSTReg = "Y", 0, 1)
        Else
            If mGSTType = "G" Then
                cboGSTStatus.SelectedIndex = 0
            ElseIf mGSTType = "R" Then
                cboGSTStatus.SelectedIndex = 1
            ElseIf mGSTType = "E" Then
                cboGSTStatus.SelectedIndex = 2
            Else
                cboGSTStatus.SelectedIndex = 3
            End If
        End If
        cboGSTStatus.Enabled = False
        chkShipTo.CheckState = IIf(mShipTo = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        txtServProvided.Text = pServName
        If mShipTo = "Y" Then
            mShipToCode = mSupplierCode
            If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplier.Text = MasterNo
                mSupplierCode = Trim(mRsDC.Fields("SUPP_CUST_CODE").Value)
            End If
            If MainClass.ValidateWithMasterTable(mShipToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtShippedTo.Text = MasterNo
            End If
        Else
            If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtShippedTo.Text = MasterNo
            End If
            If MainClass.ValidateWithMasterTable(mShipToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSupplier.Text = MasterNo
                mSupplierCode = mShipToCode
            End If
        End If
        If mDataFill = False Then
            txtVehicle.Text = IIf(IsDbNull(mRsDC.Fields("VEHICLE").Value), "", mRsDC.Fields("VEHICLE").Value)
            txtMode.Text = IIf(IsDbNull(mRsDC.Fields("TRANSPORT_MODE").Value), "", mRsDC.Fields("TRANSPORT_MODE").Value)
            txtRemarks.Text = IIf(IsDbNull(mRsDC.Fields("REMARKS").Value), "", mRsDC.Fields("REMARKS").Value)
            mDivisionCode = IIf(IsDbNull(mRsDC.Fields("DIV_CODE").Value), -1, mRsDC.Fields("DIV_CODE").Value)
            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionDesc = Trim(MasterNo)
                cboDivision.Text = mDivisionDesc
            End If
            cboDivision.Enabled = False
            Call FillCreditDays(mSupplierCode)
            txtAdvBal.Text = CStr(GetBalancePaymentAmount(mSupplierCode, txtBillDate.Text, "", "", mDivisionCode, "AP", mBalCGST, mBalSGST, mBalIGST))
        End If
        '    txtAdvCGSTBal.Text = Format(mBalCGST, "0.00")							
        '    txtAdvSGSTBal.Text = Format(mBalSGST, "0.00")							
        '    txtAdvIGSTBal.Text = Format(mBalIGST, "0.00")							
        If ShowFromMRRDetail((mRsDC.Fields("AUTO_KEY_MRR").Value), mSupplierCode, (mRsDC.Fields("REF_TYPE").Value), mDataFill) = False Then GoTo ErrPart
        Call FillSprdExp()
        CalcTots()
        ShowFromMRRMain = True
        Exit Function
ErrPart:
        '    Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromMRRMain = False
    End Function
    Private Function ResetMRRMain(ByRef mRsDC As ADODB.Recordset) As Boolean
        On Error GoTo ErrPart
        txtMRRDate.Text = IIf(IsDbNull(mRsDC.Fields("MRR_DATE").Value), "", mRsDC.Fields("MRR_DATE").Value)
        If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSupplier.Text = MasterNo
            mSupplierCode = Trim(mRsDC.Fields("SUPP_CUST_CODE").Value)
        End If
        txtBillNo.Text = IIf(IsDbNull(mRsDC.Fields("BILL_NO").Value), "", mRsDC.Fields("BILL_NO").Value)
        txtBillDate.Text = IIf(IsDbNull(mRsDC.Fields("BILL_DATE").Value), "", mRsDC.Fields("BILL_DATE").Value)
        txtMode.Text = IIf(IsDbNull(mRsDC.Fields("TRANSPORT_MODE").Value), "", mRsDC.Fields("TRANSPORT_MODE").Value)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        '    If ShowFromMRRDetail(mRsDC.Fields("AUTO_KEY_MRR").Value, mSupplierCode, mRsDC.Fields("REF_TYPE").Value) = False Then GoTo ErrPart							
        Call FillSprdExp()
        CalcTots()
        ResetMRRMain = True
        Exit Function
ErrPart:
        '    Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ResetMRRMain = False
    End Function
    Private Function ShowFromMRRDetail(ByRef mDCNo As String, ByRef pCustomerCode As String, ByRef xRefType As String, ByRef mDataFill As Boolean) As Boolean
        On Error GoTo ErrPart
        Dim RsDc As ADODB.Recordset
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mRGPItemCode As String
        Dim mRate As Double
        Dim mQty As Double
        Dim mTariff As String
        Dim mTariffDesc As String
        Dim RejectQty As Double
        Dim ReOfferQty As Double
        Dim mPONo As Double
        Dim mExchangeRate As Double
        Dim mRateExp As Double
        Dim mWorkOrderNo As Double
        Dim mOurAutoSaleKey As Double
        Dim mOurSaleInvoiceNo As String
        Dim mOurSaleInvoiceDate As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mHSNCode As String
        Dim mLocal As String
        Dim mInvTypeCode As Double
        Dim mPartyGSTNo As String
        mLocal = "N"
        If Trim(pCustomerCode) <> "" Then
            If MainClass.ValidateWithMasterTable(pCustomerCode, "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLocal = Trim(MasterNo)
            End If
        End If
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable(pCustomerCode, "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        SqlStr = "SELECT INV_GATE_DET.*, "
        SqlStr = SqlStr & vbCrLf & "GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",AUTO_KEY_MRR,MRR_DATE,SUPP_CUST_CODE,ITEM_CODE) AS REOFFER , "
        If xRefType = "I" Or xRefType = "2" Then
            SqlStr = SqlStr & vbCrLf & " GetSALEITEMPRICE(REF_PO_NO,'','" & pCustomerCode & "',ITEM_CODE) AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        ElseIf xRefType = "P" Then
            SqlStr = SqlStr & vbCrLf & " GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_PO_NO,ITEM_CODE) AS PORATE, "
            SqlStr = SqlStr & " GetVOL_DISC_ITEM(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_PO_NO, ITEM_CODE) AS VOL_DISCRATE "
        ElseIf xRefType = "R" Then
            SqlStr = SqlStr & vbCrLf & " GetITEMJWRate(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_PO_NO,AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO) AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        Else
            SqlStr = SqlStr & vbCrLf & " 0 AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        End If
        SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " WHERE AUTO_KEY_MRR=" & Val(mDCNo) & "" & vbCrLf & " ORDER BY SERIAL_NO "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDc, ADODB.LockTypeEnum.adLockReadOnly)
        With SprdMain
            cntRow = IIf(mDataFill = False, 1, SprdMain.MaxRows)
            If RsDc.EOF = False Then
                Do While Not RsDc.EOF
                    .Row = cntRow
                    .Col = ColMRRNo
                    .Text = mDCNo
                    .Col = ColItemCode
                    .Text = IIf(IsDbNull(RsDc.Fields("ITEM_CODE").Value), "", RsDc.Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDbNull(RsDc.Fields("ITEM_CODE").Value), "", RsDc.Fields("ITEM_CODE").Value)
                    mRGPItemCode = IIf(IsDbNull(RsDc.Fields("RGP_ITEM_CODE").Value), "", RsDc.Fields("RGP_ITEM_CODE").Value)
                    .Col = ColItemDesc
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If
                    .Col = ColPartNo
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If
                    .Col = ColAcceptedQty
                    .Text = CStr(Val(IIf(IsDbNull(RsDc.Fields("RECEIVED_QTY").Value), "", RsDc.Fields("RECEIVED_QTY").Value)))
                    .Col = ColShortageQty
                    .Text = CStr(Val(IIf(IsDbNull(RsDc.Fields("SHORTAGE_QTY").Value), "", RsDc.Fields("SHORTAGE_QTY").Value)))
                    .Col = ColRejectedQty
                    ReOfferQty = IIf(IsDbNull(RsDc.Fields("REOFFER").Value), "", RsDc.Fields("REOFFER").Value)
                    RejectQty = IIf(IsDbNull(RsDc.Fields("REJECTED_QTY").Value), "", RsDc.Fields("REJECTED_QTY").Value)
                    .Text = CStr(Val(CStr(RejectQty))) ' Val(RejectQty - ReOfferQty)							
                    If xRefType = "P" Then
                        mPONo = IIf(IsDbNull(RsDc.Fields("REF_PO_NO").Value), "", RsDc.Fields("REF_PO_NO").Value)
                        mExchangeRate = GetExchangeRate(mPONo)
                    Else
                        mExchangeRate = 1
                    End If
                    mRateExp = 0
                    .Col = ColPORate
                    .Text = CStr(Val(IIf(IsDbNull(RsDc.Fields("PORATE").Value), "", RsDc.Fields("PORATE").Value)) + mRateExp) ''* mExchangeRate							
                    .Col = ColVolDiscRate
                    .Text = CStr(Val(IIf(IsDbNull(RsDc.Fields("VOL_DISCRATE").Value), "", RsDc.Fields("VOL_DISCRATE").Value)))
                    .Col = ColUnit
                    .Text = IIf(IsDbNull(RsDc.Fields("ITEM_UOM").Value), "", RsDc.Fields("ITEM_UOM").Value)
                    .Col = ColQty
                    mQty = IIf(IsDbNull(RsDc.Fields("BILL_QTY").Value), "", RsDc.Fields("BILL_QTY").Value)
                    .Text = CStr(mQty)
                    .Col = ColRate
                    mRate = IIf(IsDBNull(RsDc.Fields("ITEM_RATE").Value), "", RsDc.Fields("ITEM_RATE").Value) ''* mExchangeRate							
                    .Text = CStr(mRate)
                    .Col = ColAmount
                    .Text = VB6.Format(mQty * mRate, "0.00")
                    If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
                        mHSNCode = GetSACCode((txtServProvided.Text))
                        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ErrPart
                    Else
                        mHSNCode = GetHSNCode(mItemCode)
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ErrPart
                    End If
                    SprdMain.Col = ColHSN
                    SprdMain.Text = mHSNCode
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")
                    .Col = ColPONo
                    .Text = CStr(IIf(IsDbNull(RsDc.Fields("REF_PO_NO").Value), "", RsDc.Fields("REF_PO_NO").Value))
                    mOurAutoSaleKey = IIf(IsDbNull(RsDc.Fields("REF_PO_NO").Value), -1, RsDc.Fields("REF_PO_NO").Value)
                    If xRefType = "I" Or xRefType = "2" Then
                        '                    If Trim(.Text) <> "" Then							
                        '                        .Text = "S" & vb6.Format(Mid(.Text, 1, Len(.Text) - 6), "00000")							
                        '                    End If							
                        mOurSaleInvoiceNo = ""
                        If MainClass.ValidateWithMasterTable(mOurAutoSaleKey, "AUTO_KEY_INVOICE", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mOurSaleInvoiceNo = MasterNo
                        End If
                        .Text = mOurSaleInvoiceNo
                    ElseIf xRefType = "R" Then
                        mWorkOrderNo = -1
                        If MainClass.ValidateWithMasterTable(mOurAutoSaleKey, "AUTO_KEY_PASSNO", "AUTO_KEY_WO", "INV_GATEPASS_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mRGPItemCode) & "'") = True Then
                            mWorkOrderNo = MasterNo
                        End If
                    End If
                    If xRefType = "P" Then
                        mInvTypeCode = GetPOInvType(mOurAutoSaleKey, mItemCode)
                        SprdMain.Col = ColInvType
                        If mInvTypeCode = -1 Then
                            SprdMain.Text = Trim(cboInvType.Text)
                        Else
                            If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                SprdMain.Text = MasterNo
                                If cboInvType.SelectedIndex = -1 Then cboInvType.Text = SprdMain.Text
                            Else
                                SprdMain.Text = Trim(cboInvType.Text)
                            End If
                        End If
                    ElseIf xRefType = "R" Then
                        mInvTypeCode = GetPOInvType(mWorkOrderNo, mItemCode)
                        SprdMain.Col = ColInvType
                        If mInvTypeCode = -1 Then
                            SprdMain.Text = Trim(cboInvType.Text)
                        Else
                            If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                SprdMain.Text = MasterNo
                                If cboInvType.SelectedIndex = -1 Then cboInvType.Text = SprdMain.Text
                            Else
                                SprdMain.Text = Trim(cboInvType.Text)
                            End If
                        End If
                    End If
                    '                txtPODate.Text = IIf(IsNull(mRsDC.Fields("REF_DATE").Value), "", mRsDC.Fields("REF_DATE").Value)							
                    If Trim(txtTariff.Text) = "" Then
                        If GetTariffHeading(mItemCode, mTariff, mTariffDesc) = True Then
                            txtTariff.Text = mTariff
                            txtItemType.Text = mTariffDesc
                        End If
                    End If
                    RsDc.MoveNext()
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1
                Loop
            End If
        End With
        FormatSprdMain(-1)
        ShowFromMRRDetail = True
        Exit Function
ErrPart:
        '    Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromMRRDetail = False
    End Function
    Private Sub FillCboSaleType()
        On Error GoTo ErrPart
        Dim RsSaleType As ADODB.Recordset
        Dim SqlStr As String
        cboInvType.Items.Clear()
        'mm.lblPurchaseType.text="J"							
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='P' "
        If lblPurchaseType.Text = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION ='J'"
        ElseIf lblPurchaseType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION ='W'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION NOT IN ('W','J')"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY NAME "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleType, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSaleType.EOF = False Then
            Do While Not RsSaleType.EOF
                cboInvType.Items.Add(RsSaleType.Fields("NAME").Value)
                RsSaleType.MoveNext()
            Loop
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Sub FillCreditDays(ByRef mSupplierCode As String)
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mPayDate As String
        Dim mPayDay As Integer
        Dim mPayDay2 As Integer
        If Trim(txtPONo.Text) = "" Or Val(txtPONo.Text) = 0 Then
            SqlStr = "SELECT FROM_DAYS, TO_DAYS FROM " & vbCrLf & " FIN_SUPP_CUST_HDR A, FIN_PAYTERM_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.PAYMENT_CODE=B.PAY_TERM_CODE AND SUPP_CUST_CODE='" & mSupplierCode & "'"
        Else
            SqlStr = "SELECT FROM_DAYS, TO_DAYS FROM " & vbCrLf & " PUR_PURCHASE_HDR A, FIN_PAYTERM_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.PAYMENT_CODE=B.PAY_TERM_CODE AND AUTO_KEY_PO='" & txtPONo.Text & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            txtCreditDays(0).Text = IIf(IsDbNull(RsTemp.Fields("FROM_DAYS").Value), 0, RsTemp.Fields("FROM_DAYS").Value)
            txtCreditDays(1).Text = IIf(IsDbNull(RsTemp.Fields("TO_DAYS").Value), 0, RsTemp.Fields("TO_DAYS").Value)
        Else
            SqlStr = "SELECT FROM_DAYS, TO_DAYS FROM " & vbCrLf & " FIN_SUPP_CUST_HDR A, FIN_PAYTERM_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.PAYMENT_CODE=B.PAY_TERM_CODE AND SUPP_CUST_CODE='" & mSupplierCode & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
            If RsTemp.EOF = False Then
                txtCreditDays(0).Text = IIf(IsDbNull(RsTemp.Fields("FROM_DAYS").Value), 0, RsTemp.Fields("FROM_DAYS").Value)
                txtCreditDays(1).Text = IIf(IsDbNull(RsTemp.Fields("TO_DAYS").Value), 0, RsTemp.Fields("TO_DAYS").Value)
            End If
        End If
        ''Temp.. Comment.. (paydate from po terms....)							
        '    If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "PAIDDAY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then							
        '        mPayDay = Val(IIf(IsNull(MasterNo), 0, MasterNo))							
        '    Else							
        '        mPayDay = 0							
        '    End If							
        '    If mPayDay = 0 Then							
        If IsDate(txtBillDate.Text) = True Then
            txtPaymentDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, Val(txtCreditDays(0).Text), CDate(txtBillDate.Text)))
        End If
        '    Else							
        '        mPayDate = DateAdd("D", Val(txtCreditDays(0).Text), CDate(txtBillDate.Text))							
        '        If mPayDay >= Day(mPayDate) Then							
        '            txtPaymentdate.Text = Format(mPayDay, "00") & "/" & vb6.Format(Month(mPayDate), "00") & "/" & vb6.Format(Year(mPayDate), "0000")							
        '        Else							
        '            If Val(txtCreditDays(0).Text) = Val(txtCreditDays(1).Text) Then							
        '                mPayDate = DateAdd("M", 1, mPayDate)							
        '                txtPaymentdate.Text = Format(mPayDay, "00") & "/" & vb6.Format(Month(mPayDate), "00") & "/" & vb6.Format(Year(mPayDate), "0000")							
        '            Else							
        '                mPayDate = Format(mPayDay, "00") & "/" & vb6.Format(Month(mPayDate), "00") & "/" & vb6.Format(Year(mPayDate), "0000")							
        '                mPayDate = DateAdd("D", Val(txtCreditDays(1).Text) - Val(txtCreditDays(0).Text), mPayDate)							
        '                    txtPaymentdate.Text = mPayDate      ''Format(mPayDay2, "00") & "/" & vb6.Format(Month(mPayDate), "00") & "/" & vb6.Format(Year(mPayDate), "0000")							
        '            End If							
        '        End If							
        '    End If							
        Exit Sub
ErrPart:
        'Resume							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Sub InsertTempBill(ByRef mAccountCode As String, ByRef mAmount As Double, ByRef mRemarks As String)
        On Error GoTo ErrPart
        Dim SqlStr As String
        '    PubDBCn.BeginTrans							
        SqlStr = "Insert Into FIN_TEMPBILL_TRN  ( " & vbCrLf & " UserId, TRNDTLSUBROWNO, SUBROWNO, " & vbCrLf & " ACCOUNTCODE, BILLNO, BILLDATE, " & vbCrLf & " BILLAMOUNT, BILLDC, TRNTYPE, " & vbCrLf & " Amount, DC, BOOKTYPE, REMARKS,  " & vbCrLf & " OldAmount, OldDC, OldBillNo, OldPayType,DUEDATE,TEMPMKEY ) VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "' , 1, 1, " & vbCrLf & " '" & mAccountCode & "','" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD/MMM/YYYY") & "')," & vbCrLf & " " & Val(lblNetAmount.Text) & ", 'C', 'B', " & vbCrLf & " " & mAmount & ", 'D', '" & ConJournal & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRemarks) & "', '','','',''," & vbCrLf & " TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD/MMM/YYYY") & "')," & pProcessKey & ")"
        PubDBCn.Execute(SqlStr)
        '    PubDBCn.CommitTrans							
        Exit Sub
ErrPart:
        '    PubDBCn.RollbackTrans							
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchTariff()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtTariff.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr) = True Then
            txtTariff.Text = AcName
            txtItemType.Text = AcName1
            '        txtTariff_Validate False							
            If txtTariff.Enabled = True Then txtTariff.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetPDIRItem(ByRef xMRRNo As Double) As Integer
        On Error GoTo ErrPart1
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        mSqlStr = "SELECT COUNT(1) AS CNTPDIR FROM INV_GATE_DET " & vbCrLf & " WHERE AUTO_KEY_MRR=" & xMRRNo & " AND PDIR_FLAG='N'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetPDIRItem = IIf(IsDbNull(RsTemp.Fields("CNTPDIR").Value), 0, RsTemp.Fields("CNTPDIR").Value)
        End If
        Exit Function
ErrPart1:
        GetPDIRItem = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetShipToFromPO(ByRef xMRRNo As Double, ByRef mMrrRefType As String, ByRef mGSTType As String, ByRef mShipTo As String, ByRef mShipToCode As String, ByRef pServName As String) As Boolean
        On Error GoTo ErrPart1
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pSACCode As String
        If mMrrRefType = "P" Then
            pServName = ""
            mSqlStr = "SELECT ISGSTAPPLICABLE,IH.SHIPPED_TO_SAMEPARTY, IH.SHIPPED_TO_PARTY_CODE " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, PUR_PURCHASE_HDR PH " & vbCrLf & " WHERE IH.AUTO_KEY_MRR = ID.AUTO_KEY_MRR" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=" & xMRRNo & " " & vbCrLf & " AND IH.COMPANY_CODE=PH.COMPANY_CODE" & vbCrLf & " AND ID.REF_PO_NO=PH.AUTO_KEY_PO " & vbCrLf & " AND PH.PO_STATUS='Y' AND ID.REF_TYPE='P'" & vbCrLf & " AND MKEY IN ( " & vbCrLf & " SELECT MAX(MKEY) FROM INV_GATE_DET PD, PUR_PURCHASE_HDR PO " & vbCrLf & " WHERE PO.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PD.AUTO_KEY_MRR=" & xMRRNo & " " & vbCrLf & " AND PD.COMPANY_CODE=PO.COMPANY_CODE" & vbCrLf & " AND PD.REF_PO_NO=PO.AUTO_KEY_PO " & vbCrLf & " AND PO.PO_STATUS='Y'" & vbCrLf & " AND PO.AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mGSTType = IIf(IsDbNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "E", RsTemp.Fields("ISGSTAPPLICABLE").Value)
                mShipTo = IIf(IsDbNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "N", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
                mShipToCode = IIf(IsDbNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                '            pServCode = IIf(IsNull(RsTemp!SERVICE_CODE), "", RsTemp!SERVICE_CODE)							
                '							
                '            If pServCode <> "" Then							
                '                If MainClass.ValidateWithMasterTable(pServCode, "CODE", "NAME", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then							
                '                    pServName = Trim(MasterNo)							
                '                End If							
                '            End If							
            End If
        ElseIf mMrrRefType = "R" Then
            pServName = ""
            mSqlStr = "SELECT GST_APP AS ISGSTAPPLICABLE,'Y' AS SHIPPED_TO_SAMEPARTY, PH.SUPP_CUST_CODE AS SHIPPED_TO_PARTY_CODE, SAC_CODE " & vbCrLf & " FROM INV_GATE_DET ID, INV_GATEPASS_HDR PH, INV_GATEPASS_DET PD " & vbCrLf & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.AUTO_KEY_MRR=" & xMRRNo & " " & vbCrLf & " AND ID.COMPANY_CODE=PD.COMPANY_CODE" & vbCrLf & " AND ID.REF_PO_NO=PD.AUTO_KEY_PASSNO " & vbCrLf & " AND ID.RGP_ITEM_CODE=PD.ITEM_CODE AND PH.AUTO_KEY_PASSNO=PD.AUTO_KEY_PASSNO" & vbCrLf & " AND PD.AUTO_KEY_WO IN ( " & vbCrLf & " SELECT MAX(AUTO_KEY_PO) FROM PUR_PURCHASE_HDR PO " & vbCrLf & " WHERE PO.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PO.AUTO_KEY_PO=PD.AUTO_KEY_WO AND PO.PO_STATUS='Y'" & vbCrLf & " AND PO.AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mGSTType = IIf(IsDbNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "E", RsTemp.Fields("ISGSTAPPLICABLE").Value)
                mShipTo = IIf(IsDbNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "N", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
                mShipToCode = IIf(IsDbNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                pSACCode = IIf(IsDbNull(RsTemp.Fields("SAC_CODE").Value), "", RsTemp.Fields("SAC_CODE").Value)
                If pSACCode <> "" Then
                    If MainClass.ValidateWithMasterTable(pSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        pServName = Trim(MasterNo)
                    End If
                End If
            End If
        End If
        GetShipToFromPO = True
        Exit Function
ErrPart1:
        '    Resume							
        GetShipToFromPO = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
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
    Private Sub InsertForPO(ByRef mPONo As String, ByRef pMRRNo As Double)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mSqlStr As String
        Dim mSuppCode As String
        Dim mRefType As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If MainClass.ValidateWithMasterTable(pMRRNo, "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mRefType = MasterNo
        End If
        If mRefType <> "P" Then
            mPONo = ""
        End If
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
            Report1.SubreportToChange = ""
        End If
        Report1.Action = 1
        Report1.ReportFileName = ""
        Report1.Reset()
        Exit Sub
ErrPart:
        '    Resume							
        MsgBox(Err.Description)
    End Sub
    Private Function SendMrrToAccount() As Boolean
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        SqlStr = " SELECT * FROM INV_GATE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND SEND_AC_FLAG='Y'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            SendMrrToAccount = True
        Else
            SendMrrToAccount = False
        End If
        Exit Function
ErrPart:
        SendMrrToAccount = False
    End Function
    Private Function CheckCRStockType(ByRef mItemType As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mItemCode As String
        Dim mStockType As String
        CheckCRStockType = True
        SqlStr = " SELECT ITEM_CODE, STOCK_TYPE " & vbCrLf & " FROM  INV_GATE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
        '            & vbCrLf _							
        ''            & " AND STOCK_TYPE<>'CR'"							
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        Do While RsTemp.EOF = False
            If RsTemp.EOF = True Then
                mItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mItemType = GetProductionType(mItemCode)
                mStockType = IIf(IsDbNull(RsTemp.Fields("STOCK_TYPE").Value), "", RsTemp.Fields("STOCK_TYPE").Value)
                If mItemType = "R" Or mItemType = "B" Then
                    If mStockType <> "ST" Then
                        CheckCRStockType = False
                    End If
                Else
                    If mStockType <> "CR" Then
                        CheckCRStockType = False
                    End If
                End If
            End If
            RsTemp.MoveNext()
        Loop
        Exit Function
ErrPart:
        CheckCRStockType = False
    End Function
    Private Function CheckItemType() As String
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mItemCode As String
        CheckItemType = ""
        SqlStr = " SELECT ITEM_CODE, STOCK_TYPE " & vbCrLf & " FROM  INV_GATE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        '    Do While RsTemp.EOF = False							
        If RsTemp.EOF = False Then
            mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
            CheckItemType = GetProductionType(mItemCode)
        End If
        '        RsTemp.MoveNext							
        '    Loop							
        Exit Function
ErrPart:
        CheckItemType = ""
    End Function
    Private Function GetPOInvType(ByRef pPONO As Double, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mItemCode As String
        GetPOInvType = -1
        SqlStr = " SELECT ACCOUNT_POSTING_CODE " & vbCrLf & " FROM  PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N'" & vbCrLf & " AND AUTO_KEY_PO=" & Val(CStr(pPONO)) & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetPOInvType = CDbl(Trim(IIf(IsDbNull(RsTemp.Fields("ACCOUNT_POSTING_CODE").Value), -1, RsTemp.Fields("ACCOUNT_POSTING_CODE").Value)))
        End If
        Exit Function
ErrPart:
        GetPOInvType = -1
    End Function
    Private Function GetInvoiceExp(ByRef pPONO As Double, ByRef pItemRate As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mTotalExpAmount As Double
        Dim mADDDeduct As String
        Dim mItemValue As Double
        Dim mNETVALUE As Double
        SqlStr = "SELECT IH.ITEMVALUE, IH.NETVALUE, IE.AMOUNT, IMST.IDENTIFICATION, IMST.ADD_DED " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_EXP IE, FIN_INTERFACE_MST IMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=IE.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND IE.EXPCODE=IMST.CODE " & vbCrLf & " AND IH.AUTO_KEY_INVOICE =" & pPONO & "" & vbCrLf & " AND IH.REF_DESP_TYPE<>'U'" & vbCrLf & " ORDER BY IMST.PRINTSEQUENCE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mItemValue = IIf(IsDbNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value)
            mNETVALUE = IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            Do While RsTemp.EOF
                If RsTemp.Fields("Identification").Value = "ST" Then GoTo NextCalc
                mADDDeduct = IIf(IsDbNull(RsTemp.Fields("ADD_DED").Value), 0, RsTemp.Fields("ADD_DED").Value)
                mTotalExpAmount = mTotalExpAmount + (IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * IIf(mADDDeduct = "D", -1, 1))
                RsTemp.MoveNext()
            Loop
        End If
NextCalc:
        If mItemValue = 0 Then
            GetInvoiceExp = 0
        Else
            GetInvoiceExp = CDbl(VB6.Format(mTotalExpAmount * pItemRate / mItemValue, "0.0000"))
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetInvoiceExp = 0
    End Function
    Private Function GetPreviousRJQty(ByRef pcntRow As Integer, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim cntRow As Short
        Dim mItemCode As String
        Dim mRejectedQty As Double
        mRejectedQty = 0
        If pcntRow - 1 <= 0 Then GetPreviousRJQty = 0 : Exit Function
        With SprdMain
            For cntRow = 1 To pcntRow - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                If Trim(mItemCode) = Trim(pItemCode) Then
                    .Col = ColRejectedQty
                    mRejectedQty = mRejectedQty + Val(.Text)
                End If
            Next
        End With
        GetPreviousRJQty = mRejectedQty
        Exit Function
ErrPart:
        GetPreviousRJQty = 0
        '    Resume							
    End Function
End Class							
