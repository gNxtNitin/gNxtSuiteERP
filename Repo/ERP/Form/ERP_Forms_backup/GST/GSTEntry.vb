Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmGSTEntry
    Inherits System.Windows.Forms.Form
    Dim RsTransferTrn As ADODB.Recordset ''Recordset


    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""
    Dim mSupplierCode As String
    Dim pRound As Double

    Private Const mBookType As String = "P"
    ''Private Const mBookSubType = "C"

    Dim mBookSubType As String
    Private Const ConRowHeight As Short = 12


    Private Sub cboCGSTDC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCGSTDC.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub

    Private Sub cboCGSTDC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCGSTDC.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub


    Private Sub cboIGSTDC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboIGSTDC.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub

    Private Sub cboIGSTDC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboIGSTDC.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub


    Private Sub cboRCCGSTDC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRCCGSTDC.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub


    Private Sub cboRCCGSTDC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRCCGSTDC.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub


    Private Sub cboRCIGSTDC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRCIGSTDC.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub

    Private Sub cboRCIGSTDC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRCIGSTDC.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub


    Private Sub cboRCSGSTDC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRCSGSTDC.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub


    Private Sub cboRCSGSTDC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRCSGSTDC.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub


    Private Sub cboSGSTDC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSGSTDC.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub


    Private Sub cboSGSTDC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSGSTDC.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub

    Private Sub cboTransferType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTransferType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboTransferType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTransferType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkFinalPost_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFinalPost.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtGSTNo.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim xDCNo As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBookCode As Integer
        Dim mLockBookCode As Integer
        Dim xServMkey As String
        Dim mJVMKEY As String

        If chkFinalPost.Enabled = False And chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Account Posting is Done, So can't be delete")
            Exit Sub
        End If


        If ValidateBranchLocking((txtGSTDate.Text)) = True Then
            Exit Sub
        End If
        mLockBookCode = CInt(ConLockModvat)

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtGSTDate.Text) = True Then
            Exit Sub
        End If

        If ValidateAccountLocking(PubDBCn, txtGSTDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If

        If Trim(txtGSTNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsTransferTrn.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User choose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()



                If InsertIntoDelAudit(PubDBCn, "FIN_GSTTRANSFER_TRN", (txtGSTNo.Text), RsTransferTrn, "GSTNo") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "FIN_GSTTRANSFER_TRN", "MKey", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM FIN_GSTTRANSFER_TRN WHERE MKey='" & lblMkey.Text & "' ")

                PubDBCn.CommitTrans()
                RsTransferTrn.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsTransferTrn.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Cancelled Bill Cann't be Modified")
                Exit Sub
            End If

            If PubUserID <> "G0416" Then
                If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                    MsgInformation("Final Bill Post Cann't be Modified")
                    Exit Sub
                End If
            End If

            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTransferTrn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            '        txtGSTNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        'Call PrintExcise("V", lblMKey.Caption)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        'Call PrintExcise("P", lblMKey.Caption)
    End Sub


    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtGSTNo_Validating(txtGSTNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        Dim mCapital As String
        Dim mPLA As String
        Dim mRePost As String
        With SprdView
            .Row = eventArgs.Row

            .Col = 1
            txtGSTNo.Text = VB6.Format(.Text, "00000")

            .Col = 2
            txtGSTDate.Text = VB6.Format(.Text, "DD/MM/YYYY")


            txtGSTNo_Validating(txtGSTNo, New System.ComponentModel.CancelEventArgs(False))

            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub


    Private Sub txtCGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtIGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtIGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtGSTDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGSTNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGSTNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtGSTNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGSTNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""

        If Val(txtGSTNo.Text) = 0 Then GoTo EventExitSub


        txtGSTNo.Text = VB6.Format(Val(txtGSTNo.Text), "00000")

        If MODIFYMode = True And RsTransferTrn.EOF = False Then xMkey = RsTransferTrn.Fields("mKey").Value

        SqlStr = " SELECT * FROM FIN_GSTTRANSFER_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND REFNO=" & Val(txtGSTNo.Text) & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransferTrn, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTransferTrn.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_GSTTRANSFER_TRN " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMkey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransferTrn, ADODB.LockTypeEnum.adLockReadOnly)
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

        Dim mVNO As Double
        Dim mSuppCustCode As String
        Dim mFinalPost As String
        Dim mCancelled As String
        Dim mTransferType As String
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim nMkey As String
        Dim pVoucherMkey As String
        Dim mRCCGSTAmount As Double
        Dim mRCSGSTAmount As Double
        Dim mRCIGSTAmount As Double

        Dim mCGSTDC As String
        Dim mSGSTDC As String
        Dim mIGSTDC As String
        Dim mRCCGSTDC As String
        Dim mRCSGSTDC As String
        Dim mRCIGSTDC As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mFinalPost = IIf(chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mTransferType = VB.Left(cboTransferType.Text, 1)

        mCGSTAmount = CDbl(VB6.Format(txtCGSTAmount.Text, "0.00"))
        mSGSTAmount = CDbl(VB6.Format(txtSGSTAmount.Text, "0.00"))
        mIGSTAmount = CDbl(VB6.Format(txtIGSTAmount.Text, "0.00"))

        mCGSTDC = VB.Left(cboCGSTDC.Text, 1)
        mSGSTDC = VB.Left(cboSGSTDC.Text, 1)
        mIGSTDC = VB.Left(cboIGSTDC.Text, 1)

        mRCCGSTAmount = CDbl(VB6.Format(txtRCCGSTAmount.Text, "0.00"))
        mRCSGSTAmount = CDbl(VB6.Format(txtRCSGSTAmount.Text, "0.00"))
        mRCIGSTAmount = CDbl(VB6.Format(txtRCIGSTAmount.Text, "0.00"))

        mRCCGSTDC = VB.Left(cboRCCGSTDC.Text, 1)
        mRCSGSTDC = VB.Left(cboRCSGSTDC.Text, 1)
        mRCIGSTDC = VB.Left(cboRCIGSTDC.Text, 1)


        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        If Trim(txtGSTNo.Text) = "" Then
            mVNO = CDbl(AutoGenSeqBillNo())
            txtGSTNo.Text = VB6.Format(mVNO, "00000")
        Else
            mVNO = Val(txtGSTNo.Text)
        End If


        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_GSTTRANSFER_TRN", "ROWNO", PubDBCn)
            nMkey = (RsCompany.Fields("COMPANY_CODE").Value) & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey


            SqlStr = "INSERT INTO FIN_GSTTRANSFER_TRN( " & vbCrLf & " MKEY, ROWNO, COMPANY_CODE, " & vbCrLf & " FYEAR, SUPP_CUST_CODE, REFNO, " & vbCrLf & " REFDATE, TRANSFERTYPE, " & vbCrLf & " TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT, " & vbCrLf & " TOTRCCGST_AMOUNT, TOTRCSGST_AMOUNT, TOTRCIGST_AMOUNT, " & vbCrLf & " CGST_DC, SGST_DC, IGST_DC, " & vbCrLf & " RCCGST_DC, RCSGST_DC, RCIGST_DC, " & vbCrLf & " REMARKS, JVMKEY, CANCELLED, ISFINALPOST, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE )"


            SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & mCurRowNo & ", " & RsCompany.Fields("Company_Code").Value & ",  " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', " & mVNO & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mTransferType & "', " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & "," & mIGSTAmount & ", " & vbCrLf & " " & mRCCGSTAmount & ", " & mRCSGSTAmount & "," & mRCIGSTAmount & ", " & vbCrLf & " '" & mCGSTDC & "', '" & mSGSTDC & "', '" & mIGSTDC & "'," & vbCrLf & " '" & mRCCGSTDC & "', '" & mRCSGSTDC & "', '" & mRCIGSTDC & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & lblJVMkey.Text & "', '" & mCancelled & "', '" & mFinalPost & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','' " & vbCrLf & " )"



        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_GSTTRANSFER_TRN SET " & vbCrLf & " COMPANY_CODE = " & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf & " FYEAR = " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', " & vbCrLf & " REFNO = " & mVNO & ", " & vbCrLf _
                & " REFDATE = TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TRANSFERTYPE = '" & mTransferType & "', " & vbCrLf & " TOTCGST_AMOUNT = " & mCGSTAmount & ", " & vbCrLf & " TOTSGST_AMOUNT = " & mSGSTAmount & ", " & vbCrLf & " TOTIGST_AMOUNT = " & mIGSTAmount & ", " & vbCrLf & " TOTRCCGST_AMOUNT = " & mRCCGSTAmount & ", " & vbCrLf & " TOTRCSGST_AMOUNT = " & mRCSGSTAmount & ", " & vbCrLf & " TOTRCIGST_AMOUNT = " & mRCIGSTAmount & ", " & vbCrLf & " CGST_DC = '" & mCGSTDC & "', " & vbCrLf & " SGST_DC = '" & mSGSTDC & "', " & vbCrLf & " IGST_DC = '" & mIGSTDC & "', " & vbCrLf & " RCCGST_DC = '" & mRCCGSTDC & "', " & vbCrLf & " RCSGST_DC = '" & mRCSGSTDC & "', " & vbCrLf & " RCIGST_DC = '" & mRCIGSTDC & "', " & vbCrLf & " REMARKS = '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " CANCELLED = '" & mCancelled & "', " & vbCrLf & " ISFINALPOST= '" & mFinalPost & "', " & vbCrLf & " JVMKEY = '" & lblJVMkey.Text & "', "


            SqlStr = SqlStr & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"


        End If

        PubDBCn.Execute(SqlStr)

        If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then

            If GenerateVoucher(pVoucherMkey, 1, IIf(ADDMode = True, True, False)) = False Then
                GoTo ErrPart
            End If

            SqlStr = "UPDATE FIN_GSTTRANSFER_TRN SET  JVMKEY='" & pVoucherMkey & "' WHERE MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
            PubDBCn.Execute(SqlStr)

        End If

        UpdateMain1 = True

        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsTransferTrn.Requery() ''.Refresh

        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        If ADDMode = True Then
            txtGSTNo.Text = ""
        End If
        'Resume
    End Function
    Private Function GenerateVoucher(ByRef pVoucherMkey As String, ByRef mDivCode As Double, ByRef pAddMode As Boolean) As Boolean

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
        Dim mVNO As Integer
        Dim mCancelled As String
        Dim pRowNo As Integer
        Dim CurJVMKey As String



        mVNoPrefix = ""
        mVNoSuffix = ""
        mVType = ConJournal
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        mVNO = 0

        If pAddMode = True Then
            mVnoStr = GenJVVno(mBookType, mBookSubType, mVType, mVNO)
        Else
            mVnoStr = VB6.Format(txtJVNo.Text)
            mVNO = CInt(VB6.Format(lblVNOSeq.Text))
        End If

        mVnoStr = mVNoPrefix & mVType & VB6.Format(mVNO, "00000") & mVNoSuffix
        txtJVNo.Text = mVnoStr

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mBookCode = CStr(ConJournalBookCode)


        If pAddMode = True Then
            pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurJVMKey = (VB6.Format(RsCompany.Fields("COMPANY_CODE").Value)) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
            pVoucherMkey = CurJVMKey

            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf & " Mkey, COMPANY_CODE, " & vbCrLf & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf & " Vno, Vdate, BookType,BookSubType, " & vbCrLf & " BookCode, Narration, CANCELLED, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM, EXPDATE) VALUES ( " & vbCrLf & " '" & CurJVMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & pRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(CStr(mVNO)) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & mCancelled & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','H', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        Else
            CurJVMKey = lblJVMkey.Text
            pVoucherMkey = CurJVMKey
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
                & " Vdate=TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EXPDATE=TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " VType= '" & mVType & "'," & vbCrLf & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf & " VnoSeq=" & Val(CStr(mVNO)) & ", " & vbCrLf & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf & " Vno='" & mVnoStr & "', " & vbCrLf & " BookCode='" & mBookCode & "', " & vbCrLf & " Narration='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " CANCELLED='" & mCancelled & "', " & vbCrLf & " BookType='" & mBookType & "', " & vbCrLf & " BookSubType='" & mBookSubType & "', " & vbCrLf & " UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " Where Mkey='" & CurJVMKey & "'"
        End If

        PubDBCn.Execute(SqlStr)

        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        End If

        If GenerateJVDetail(CurJVMKey, pRowNo, mBookCode, ConJournal, mVType, mVnoStr, (txtGSTDate.Text), (txtRemarks.Text), mDivCode, PubDBCn, mAccountCode) = False Then GoTo ErrPart



        GenerateVoucher = True
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GenerateVoucher = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function GenerateJVDetail(ByRef mMKey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef pJVBookType As String, ByRef mVType As String, ByRef mVNO As String, ByRef mVDate As String, ByRef pNarration As String, ByRef mDivCode As Double, ByRef pDBCn As ADODB.Connection, ByRef pSupplierCode As String) As Boolean

        On Error GoTo ErrDetail

        Dim I As Integer
        Dim SqlStr As String = ""
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
        'Dim cntRow As Long
        Dim mCreditApplicable As String


        mBookType = VB.Left(pJVBookType, 1)
        mBookSubType = VB.Right(pJVBookType, 1)

        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMKey & "'"
        pDBCn.Execute(SqlStr)

        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMKey & "'"
        pDBCn.Execute(SqlStr)


        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMKey & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BooksubType='" & mBookSubType & "'"
        pDBCn.Execute(SqlStr)


        mChequeNo = ""
        mChqDate = ""
        mCCCode = "001"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "001"
        mIBRNo = "-1"
        mClearDate = ""
        mParticulars = pNarration
        '    cntRow = 1
        mPRRowNo = 1
        I = 0

        '************************** Posting Payable

        I = I + 1
        mPRRowNo = I
        mDC = VB.Left(cboCGSTDC.Text, 1)
        mAmount = Val(txtCGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_CGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_CGSTLEDGER_ACCTCODE").Value)

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid CGST Payable Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If


        I = I + 1
        mPRRowNo = I
        mDC = VB.Left(cboSGSTDC.Text, 1)
        mAmount = Val(txtSGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_SGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_SGSTLEDGER_ACCTCODE").Value)

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid SGST Payable Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        I = I + 1
        mPRRowNo = I
        mDC = VB.Left(cboIGSTDC.Text, 1)
        mAmount = Val(txtIGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_IGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_IGSTLEDGER_ACCTCODE").Value)

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid IGST Payable Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Reverse Charge Posting Payable

        I = I + 1
        mPRRowNo = I
        mDC = VB.Left(cboRCCGSTDC.Text, 1)
        mAmount = Val(txtRCCGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_RCCGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_RCCGSTLEDGER_ACCTCODE").Value)

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid Reverse Charge CGST Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If


        I = I + 1
        mPRRowNo = I
        mDC = VB.Left(cboRCSGSTDC.Text, 1)
        mAmount = Val(txtRCSGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_RCSGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_RCSGSTLEDGER_ACCTCODE").Value)

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid Reverse Charge SGST Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        I = I + 1
        mPRRowNo = I
        mDC = VB.Left(cboRCIGSTDC.Text, 1)
        mAmount = Val(txtRCIGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("E_RCIGSTLEDGER_ACCTCODE").Value), "-1", RsCompany.Fields("E_RCIGSTLEDGER_ACCTCODE").Value)

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid Reverse Charge IGST Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Account Posting Payable

        I = I + 1
        mPRRowNo = I
        mDC = IIf(VB.Left(lblNetDC.Text, 1) = "D", "C", "D")

        mAmount = Val(txtNetTransferAmount.Text)

        mAccountCode = pSupplierCode

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid Reverse Charge CGST Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        GenerateJVDetail = True
        Exit Function
ErrDetail:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GenerateJVDetail = False
        ''Resume
    End Function

    Private Function GeneratePostingDetail(ByRef mMKey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef pBankBookType As String, ByRef mVType As String, ByRef mVNO As String, ByRef mVDate As String, ByRef pNarration As String, ByRef mDivCode As Double, ByRef pDBCn As ADODB.Connection, ByRef mAccountCode As String, ByRef mAmount As Double, ByRef mChequeNo As String, ByRef mChqDate As String, ByRef mCCCode As String, ByRef mDeptCode As String, ByRef mEmpCode As String, ByRef mExpCode As String, ByRef mIBRNo As String, ByRef mDC As String, ByRef mRemarks As String, ByRef mPRRowNo As Integer, ByRef I As Integer, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mClearDate As String, ByRef mParticulars As String) As Boolean

        On Error GoTo ErrDetail

        ''mAccountCode

        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMKey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivCode & " )"

            PubDBCn.Execute(SqlStr)


            If UpdatePRDetail(pDBCn, mMKey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNO, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, VB6.Format(mVDate, "DD/MM/YYYY"), ADDMode, PubUserID, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mDivCode, -1, "N") = False Then GoTo ErrDetail
        End If



        GeneratePostingDetail = True
        Exit Function
ErrDetail:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GeneratePostingDetail = False
        ''Resume
    End Function


    Private Function GenJVVno(ByRef mBookType As String, ByRef mBookSubType As String, ByRef mVType As String, ByRef mVNoSeq As Integer) As String

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        If ADDMode = True Or txtJVNo.Text = "" Then
            SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BookSubType='" & mBookSubType & "'" & vbCrLf & " AND VTYPE='" & MainClass.AllowSingleQuote(mVType) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

            If RS.EOF = False Then
                If Not IsDbNull(RS.Fields(0).Value) Then
                    mVNoSeq = Val(RS.Fields(0).Value) + 1
                Else
                    mVNoSeq = 1
                End If
            Else
                mVNoSeq = 1
            End If

            GenJVVno = mVType & VB6.Format(mVNoSeq, "00000")
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume
    End Function

    Private Function AutoGenSeqBillNo() As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsTransferTrnGen As ADODB.Recordset
        Dim mNewSeqBillNo As Double
        Dim mSeqNo As Double

        SqlStr = ""


        SqlStr = "SELECT Max(REFNO)  FROM FIN_GSTTRANSFER_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransferTrnGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTransferTrnGen
            If .EOF = False Then
                mSeqNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mSeqNo = -1 Then
                    mNewSeqBillNo = 1
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = 1
                End If
            Else
                mNewSeqBillNo = 1
            End If
        End With
        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mLockBookCode As Integer
        Dim mPLA As String

        FieldsVarification = True
        If ValidateBranchLocking((txtGSTDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        mLockBookCode = CInt(ConLockModvat)


        If ValidateBookLocking(PubDBCn, mLockBookCode, txtGSTDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateAccountLocking(PubDBCn, txtGSTDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsTransferTrn.EOF = True Then Exit Function

        If MODIFYMode = True And txtGSTNo.Text = "" Then
            MsgInformation("Modvat No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtGSTDate.Text = "" Then
            MsgBox("GST Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtGSTDate.Focus()
            Exit Function
        ElseIf FYChk((txtGSTDate.Text)) = False Then
            FieldsVarification = False
            If txtGSTDate.Enabled = True Then txtGSTDate.Focus()
            Exit Function
        End If


        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
                'txtSupplier.SetFocus
                FieldsVarification = False
                Exit Function
            Else
                mSupplierCode = MasterNo
            End If
        End If

        If cboTransferType.SelectedIndex = -1 Then
            MsgBox("Please Select the Transfer Type.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If cboCGSTDC.SelectedIndex = -1 And Val(txtCGSTAmount.Text) > 0 Then
            MsgBox("Please Select the CGST Debit / Credit.", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboCGSTDC.Enabled = True Then cboCGSTDC.Focus()
            Exit Function
        End If

        If cboSGSTDC.SelectedIndex = -1 And Val(txtSGSTAmount.Text) > 0 Then
            MsgBox("Please Select the CGST Debit / Credit.", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboSGSTDC.Enabled = True Then cboSGSTDC.Focus()
            Exit Function
        End If

        If cboIGSTDC.SelectedIndex = -1 And Val(txtIGSTAmount.Text) > 0 Then
            MsgBox("Please Select the CGST Debit / Credit.", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboIGSTDC.Enabled = True Then cboIGSTDC.Focus()
            Exit Function
        End If

        If cboRCCGSTDC.SelectedIndex = -1 And Val(txtRCCGSTAmount.Text) > 0 Then
            MsgBox("Please Select the CGST Debit / Credit.", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboRCCGSTDC.Enabled = True Then cboRCCGSTDC.Focus()
            Exit Function
        End If

        If cboRCSGSTDC.SelectedIndex = -1 And Val(txtRCSGSTAmount.Text) > 0 Then
            MsgBox("Please Select the CGST Debit / Credit.", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboRCSGSTDC.Enabled = True Then cboRCSGSTDC.Focus()
            Exit Function
        End If

        If cboRCIGSTDC.SelectedIndex = -1 And Val(txtRCIGSTAmount.Text) > 0 Then
            MsgBox("Please Select the CGST Debit / Credit.", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboRCIGSTDC.Enabled = True Then cboRCIGSTDC.Focus()
            Exit Function
        End If

        If Val(txtCGSTAmount.Text) < 0 Or Val(txtSGSTAmount.Text) < 0 Or Val(txtIGSTAmount.Text) < 0 Or Val(txtRCCGSTAmount.Text) < 0 Or Val(txtRCSGSTAmount.Text) < 0 Or Val(txtRCIGSTAmount.Text) < 0 Then
            MsgBox("Value Should be Greater than Zero.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If


        If Val(txtCGSTAmount.Text) + Val(txtSGSTAmount.Text) + Val(txtIGSTAmount.Text) + Val(txtRCCGSTAmount.Text) + Val(txtRCSGSTAmount.Text) + Val(txtRCIGSTAmount.Text) = 0 Then
            MsgBox("Nothing to Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        Call CalcTots()

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mNetAmount As Double


        mNetAmount = 0

        mNetAmount = mNetAmount + Val(txtCGSTAmount.Text) * IIf(VB.Left(cboCGSTDC.Text, 1) = "D", 1, -1)
        mNetAmount = mNetAmount + (Val(txtSGSTAmount.Text) * IIf(VB.Left(cboSGSTDC.Text, 1) = "D", 1, -1))
        mNetAmount = mNetAmount + (Val(txtIGSTAmount.Text) * IIf(VB.Left(cboIGSTDC.Text, 1) = "D", 1, -1))

        mNetAmount = mNetAmount + Val(txtRCCGSTAmount.Text) * IIf(VB.Left(cboRCCGSTDC.Text, 1) = "D", 1, -1)
        mNetAmount = mNetAmount + (Val(txtRCSGSTAmount.Text) * IIf(VB.Left(cboRCSGSTDC.Text, 1) = "D", 1, -1))
        mNetAmount = mNetAmount + (Val(txtRCIGSTAmount.Text) * IIf(VB.Left(cboRCIGSTDC.Text, 1) = "D", 1, -1))


        txtNetTransferAmount.Text = VB6.Format(System.Math.Abs(mNetAmount), "#0.00")
        lblNetDC.Text = IIf(mNetAmount >= 0, "Dr", "Cr")

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmGSTEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from FIN_GSTTRANSFER_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransferTrn, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Clear1()
        mSupplierCode = CStr(-1)

        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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
        SqlStr = ""


        SqlStr = "SELECT TO_CHAR(REFNO,'00000') AS NO," & vbCrLf & " REFDATE AS M_DATE, A.SUPP_CUST_NAME AS SUPPLIER, "

        SqlStr = SqlStr & vbCrLf & " DECODE(TRANSFERTYPE,'I','IN','OUT') AS TRANSFERTYPE, "

        SqlStr = SqlStr & vbCrLf & "TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT,"

        SqlStr = SqlStr & vbCrLf & " DECODE(CANCELLED,'Y','YES','NO') AS CANCELLED "
        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_GSTTRANSFER_TRN IH, FIN_SUPP_CUST_MST A " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.COMPANY_CODE=A.COMPANY_CODE(+) " & vbCrLf & " AND IH.SUPP_CUST_CODE=A.SUPP_CUST_CODE(+) "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)

            .set_ColWidth(1, 800)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 800)
            .set_ColWidth(4, 600)
            .set_ColWidth(5, 900)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 1500)
            .set_ColWidth(8, 600)
            .set_ColWidth(9, 1200)
            .set_ColWidth(10, 1100)

            .ColsFrozen = 1
            .Col = 9
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .Col = 10
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight



            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsTransferTrn

            txtGSTNo.Maxlength = .Fields("REFNO").DefinedSize ''
            txtGSTDate.Maxlength = 10


            txtSupplier.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize ''

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mTransferType As String
        Dim mType As String
        Dim mStatus As String

        With RsTransferTrn
            If Not .EOF Then
                lblMkey.Text = .Fields("MKey").Value
                lblJVMkey.Text = IIf(IsDbNull(.Fields("JVMKEY").Value), "", .Fields("JVMKEY").Value)
                txtGSTNo.Text = VB6.Format(IIf(IsDbNull(.Fields("REFNO").Value), "", .Fields("REFNO").Value), "00000")
                txtGSTDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REFDATE").Value), "", .Fields("REFDATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If


                mStatus = IIf(IsDbNull(.Fields("ISFINALPOST").Value), "N", .Fields("ISFINALPOST").Value)

                chkFinalPost.CheckState = IIf(mStatus = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkFinalPost.Enabled = IIf(mStatus = "Y", False, True)

                mTransferType = IIf(IsDbNull(.Fields("TRANSFERTYPE").Value), "I", .Fields("TRANSFERTYPE").Value)
                cboTransferType.SelectedIndex = IIf(mTransferType = "I", 0, 1)

                txtCGSTAmount.Text = IIf(IsDbNull(.Fields("TOTCGST_AMOUNT").Value), 0, System.Math.Abs(.Fields("TOTCGST_AMOUNT").Value))
                txtSGSTAmount.Text = IIf(IsDbNull(.Fields("TOTSGST_AMOUNT").Value), 0, System.Math.Abs(.Fields("TOTSGST_AMOUNT").Value))
                txtIGSTAmount.Text = IIf(IsDbNull(.Fields("TOTIGST_AMOUNT").Value), 0, System.Math.Abs(.Fields("TOTIGST_AMOUNT").Value))

                mType = IIf(IsDbNull(.Fields("CGST_DC").Value), "D", .Fields("CGST_DC").Value)
                cboCGSTDC.SelectedIndex = IIf(mType = "D", 0, 1)

                mType = IIf(IsDbNull(.Fields("SGST_DC").Value), "D", .Fields("SGST_DC").Value)
                cboSGSTDC.SelectedIndex = IIf(mType = "D", 0, 1)

                mType = IIf(IsDbNull(.Fields("IGST_DC").Value), "D", .Fields("IGST_DC").Value)
                cboIGSTDC.SelectedIndex = IIf(mType = "D", 0, 1)


                txtRCCGSTAmount.Text = IIf(IsDbNull(.Fields("TOTRCCGST_AMOUNT").Value), 0, System.Math.Abs(.Fields("TOTRCCGST_AMOUNT").Value))
                txtRCSGSTAmount.Text = IIf(IsDbNull(.Fields("TOTRCSGST_AMOUNT").Value), 0, System.Math.Abs(.Fields("TOTRCSGST_AMOUNT").Value))
                txtRCIGSTAmount.Text = IIf(IsDbNull(.Fields("TOTRCIGST_AMOUNT").Value), 0, System.Math.Abs(.Fields("TOTRCIGST_AMOUNT").Value))

                mType = IIf(IsDbNull(.Fields("RCCGST_DC").Value), "D", .Fields("RCCGST_DC").Value)
                cboRCCGSTDC.SelectedIndex = IIf(mType = "D", 0, 1)

                mType = IIf(IsDbNull(.Fields("RCSGST_DC").Value), "D", .Fields("RCSGST_DC").Value)
                cboRCSGSTDC.SelectedIndex = IIf(mType = "D", 0, 1)

                mType = IIf(IsDbNull(.Fields("RCIGST_DC").Value), "D", .Fields("RCIGST_DC").Value)
                cboRCIGSTDC.SelectedIndex = IIf(mType = "D", 0, 1)

                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)



                SqlStr = "SELECT VNO, VDATE,VNOSeq FROM FIN_VOUCHER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & Trim(lblJVMkey.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

                If RsMisc.EOF = False Then
                    txtJVNo.Text = IIf(IsDbNull(RsMisc.Fields("VNO").Value), "", RsMisc.Fields("VNO").Value)
                    txtJVDate.Text = VB6.Format(IIf(IsDbNull(RsMisc.Fields("VDATE").Value), "", RsMisc.Fields("VDATE").Value), "DD/MM/YYYY")
                    lblVNOSeq.Text = IIf(IsDbNull(RsMisc.Fields("VNOSeq").Value), 0, RsMisc.Fields("VNOSeq").Value)
                End If

            End If
        End With

        Call CalcTots()
        txtGSTNo.Enabled = True
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsTransferTrn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
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
        MainClass.ButtonStatus(Me, XRIGHT, RsTransferTrn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""
        mSupplierCode = CStr(-1)

        lblJVMkey.Text = ""
        lblVNOSeq.Text = CStr(0)
        txtGSTNo.Text = ""
        txtGSTDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtSupplier.Text = ""

        cboTransferType.SelectedIndex = -1

        cboCGSTDC.SelectedIndex = 0
        cboSGSTDC.SelectedIndex = 0
        cboIGSTDC.SelectedIndex = 0
        cboRCCGSTDC.SelectedIndex = 0
        cboRCSGSTDC.SelectedIndex = 0
        cboRCIGSTDC.SelectedIndex = 0

        txtCGSTAmount.Text = "0.00"
        txtSGSTAmount.Text = "0.00"
        txtIGSTAmount.Text = "0.00"


        txtRCCGSTAmount.Text = "0.00"
        txtRCSGSTAmount.Text = "0.00"
        txtRCIGSTAmount.Text = "0.00"

        txtNetTransferAmount.Text = "0.00"
        txtNetTransferAmount.Enabled = False

        lblNetDC.Text = ""

        txtRemarks.Text = ""

        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFinalPost.Enabled = IIf(RsCompany.Fields("FYEAR").Value >= 2018, False, True)
        chkFinalPost.CheckState = IIf(RsCompany.Fields("FYEAR").Value >= 2018, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        txtJVNo.Text = ""
        txtJVDate.Text = ""
        MainClass.ButtonStatus(Me, XRIGHT, RsTransferTrn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmGSTEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmGSTEntry_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub FrmGSTEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        If InStr(1, XRIGHT, "D", CompareMethod.Text) > 1 Then
            chkCancelled.Enabled = True
        Else
            chkCancelled.Enabled = False
        End If

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(5340) '8000
        'Me.Width = VB6.TwipsToPixelsX(10140) '11900
        'AdoDCMain.Visible = False



        cboTransferType.Items.Clear()
        cboTransferType.Items.Add("IN")
        cboTransferType.Items.Add("OUT")
        cboTransferType.SelectedIndex = -1

        cboCGSTDC.Items.Clear()
        cboCGSTDC.Items.Add("Dr")
        cboCGSTDC.Items.Add("Cr")
        cboCGSTDC.SelectedIndex = 0

        cboSGSTDC.Items.Clear()
        cboSGSTDC.Items.Add("Dr")
        cboSGSTDC.Items.Add("Cr")
        cboSGSTDC.SelectedIndex = 0

        cboIGSTDC.Items.Clear()
        cboIGSTDC.Items.Add("Dr")
        cboIGSTDC.Items.Add("Cr")
        cboIGSTDC.SelectedIndex = 0

        cboRCCGSTDC.Items.Clear()
        cboRCCGSTDC.Items.Add("Dr")
        cboRCCGSTDC.Items.Add("Cr")
        cboRCCGSTDC.SelectedIndex = 0

        cboRCSGSTDC.Items.Clear()
        cboRCSGSTDC.Items.Add("Dr")
        cboRCSGSTDC.Items.Add("Cr")
        cboRCSGSTDC.SelectedIndex = 0

        cboRCIGSTDC.Items.Clear()
        cboRCIGSTDC.Items.Add("Dr")
        cboRCIGSTDC.Items.Add("Cr")
        cboRCIGSTDC.SelectedIndex = 0


        txtSupplier.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtIGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNetTransferAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetTransferAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetTransferAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetTransferAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRCCGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRCCGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRCCGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRCCGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRCCGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRCCGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRCIGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRCIGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRCIGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRCIGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRCIGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRCIGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRCSGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRCSGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRCSGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRCSGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRCSGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRCSGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C','2')"

        If MainClass.SearchGridMaster((txtSupplier.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSupplier.Text = AcName
            txtsupplier_Validating(txtsupplier, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
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

    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtSupplier_DoubleClick(txtSupplier, New System.EventArgs())
    End Sub

    Private Sub txtsupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtSupplier.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C','2')"

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Name Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
