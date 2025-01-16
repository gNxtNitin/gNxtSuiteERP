Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmGSTReversalEntry
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

    Private Const ConRowHeight As Short = 12


    Private Sub cboReversalRule_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReversalRule.TextChanged

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



                If InsertIntoDelAudit(PubDBCn, "FIN_GSTREVERSAL_TRN", (txtGSTNo.Text), RsTransferTrn, "GSTNo") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "FIN_GSTREVERSAL_TRN", "MKey", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM FIN_GSTREVERSAL_TRN WHERE MKey='" & lblMkey.Text & "' ")

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

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBookCode As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mMKey As String
        Dim mFYear As Integer
        Dim mSupplierCode As String
        Dim pSqlStr As String
        Dim RsTrn As ADODB.Recordset
        Dim pTableName As String
        Dim pFieldName As String


        If Val(txtOGSTNo.Text) <= 0 Then MsgInformation("Invalid GST NO.") : Exit Sub

        If Trim(txtOGSTDate.Text) = "" Then MsgInformation("Invalid GST Date.") : Exit Sub

        If Not IsDate(txtOGSTDate.Text) Then MsgInformation("Invalid GST Date.") : Exit Sub

        SqlStr = "SELECT * FROM FIN_GST_NEWSEQ_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GST_CLAIM_NO=" & Val(txtOGSTNo.Text) & "" & vbCrLf _
            & " AND GST_CLAIM_DATE=TO_DATE('" & VB6.Format(txtOGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND INPUT_REVERSE='I' AND CLAIM_TYPE='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mBookCode = IIf(IsDbNull(RsTemp.Fields("BOOKCODE").Value), -1, RsTemp.Fields("BOOKCODE").Value)
            mBookType = IIf(IsDbNull(RsTemp.Fields("BOOKTYPE").Value), "", RsTemp.Fields("BOOKTYPE").Value)
            mBookSubType = IIf(IsDbNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)
            mMKey = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "-1", RsTemp.Fields("mKey").Value)
            mFYear = IIf(IsDbNull(RsTemp.Fields("FYEAR").Value), 0, RsTemp.Fields("FYEAR").Value)
            txtAlreadyReversalAmt.Text = CStr(GetAlreadyReversalAmt(Val(txtOGSTNo.Text), VB6.Format(txtOGSTDate.Text, "DD-MMM-YYYY")))

            If mBookCode = ConSalesBookCode Then
                pTableName = "FIN_INVOICE_HDR"
                pFieldName = "ACCOUNTCODE"
                pSqlStr = "SELECT BILLNO AS VNO, INVOICE_DATE AS VDATE, BILLNO, INVOICE_DATE, " & vbCrLf & " SUPP_CUST_CODE, TOTTAXABLEAMOUNT, " & vbCrLf & " NETVALUE, TOTCGST_RC_REFUNDAMT AS TOTCGST_REFUNDAMT,TOTSGST_RC_REFUNDAMT AS TOTSGST_REFUNDAMT, TOTIGST_RC_REFUNDAMT AS TOTIGST_REFUNDAMT " & vbCrLf & " FROM FIN_INVOICE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND MKEY='" & mMKey & "'" & vbCrLf & " AND BOOKTYPE='" & mBookType & "'"
            ElseIf mBookCode = ConPurchaseBookCode Then
                pTableName = "FIN_PURCHASE_DET"
                pFieldName = "PUR_ACCOUNT_CODE"
                pSqlStr = "SELECT VNO, VDATE, BILLNO, INVOICE_DATE, " & vbCrLf & " SUPP_CUST_CODE, TOTTAXABLEAMOUNT, " & vbCrLf & " NETVALUE, TOTCGST_REFUNDAMT,TOTSGST_REFUNDAMT, TOTIGST_REFUNDAMT " & vbCrLf & " FROM FIN_PURCHASE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND MKEY='" & mMKey & "'" & vbCrLf & " AND BOOKTYPE='" & mBookType & "'"
            ElseIf mBookCode = ConPurchaseSuppBookCode Then
                pTableName = "FIN_SUPP_PURCHASE_DET"
                pFieldName = "PUR_ACCOUNT_CODE"
                pSqlStr = "SELECT VNO, VDATE, BILLNO, INVOICE_DATE, " & vbCrLf & " SUPP_CUST_CODE, ITEMVALUE AS TOTTAXABLEAMOUNT, " & vbCrLf & " NETVALUE, TOTCGST_REFUNDAMT,TOTSGST_REFUNDAMT, TOTIGST_REFUNDAMT " & vbCrLf & " FROM FIN_SUPP_PURCHASE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND MKEY='" & mMKey & "'" & vbCrLf & " AND BOOKTYPE='" & mBookType & "'"
            ElseIf mBookCode = ConLCBookCode And mBookSubType = "P" Then
                pTableName = "FIN_LCOPEN_DET"
                pFieldName = "ACCOUNTCODE"
                pSqlStr = "SELECT VNO, VDATE, REF_NO AS BILLNO, REF_DATE AS INVOICE_DATE, " & vbCrLf & " SUPP_CUST_CODE, ITEMVALUE AS TOTTAXABLEAMOUNT, " & vbCrLf & " NETVALUE, TOTCGST_CREDITAMT AS TOTCGST_REFUNDAMT,TOTSGST_CREDITAMT AS TOTSGST_REFUNDAMT, TOTIGST_CREDITAMT AS TOTIGST_REFUNDAMT " & vbCrLf & " FROM FIN_LCOPEN_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND MKEY='" & mMKey & "'" & vbCrLf & " AND BOOKTYPE='" & mBookType & mBookSubType & "'"
            ElseIf mBookCode = ConLDBookCode And mBookSubType = "D" Then
                pTableName = "FIN_LCDISC_DET"
                pFieldName = "ACCOUNTCODE"
                pSqlStr = "SELECT VNO, VDATE, REF_NO AS BILLNO, REF_DATE AS INVOICE_DATE, " & vbCrLf & " SUPP_CUST_CODE, ITEMVALUE AS TOTTAXABLEAMOUNT, " & vbCrLf & " NETVALUE, TOTCGST_CREDITAMT AS TOTCGST_REFUNDAMT,TOTSGST_CREDITAMT AS TOTSGST_REFUNDAMT, TOTIGST_CREDITAMT AS TOTIGST_REFUNDAMT " & vbCrLf & " FROM FIN_LCDISC_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND MKEY='" & mMKey & "'" & vbCrLf & " AND BOOKTYPE='" & mBookType & mBookSubType & "'"
            End If

            If pSqlStr <> "" Then
                MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTrn, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTrn.EOF = False Then
                    txtVNo.Text = IIf(IsDbNull(RsTrn.Fields("VNO").Value), "", RsTrn.Fields("VNO").Value)
                    txtVDate.Text = VB6.Format(IIf(IsDbNull(RsTrn.Fields("VDate").Value), "", RsTrn.Fields("VDate").Value), "DD/MM/YYYY")

                    txtBillNo.Text = IIf(IsDbNull(RsTrn.Fields("BILLNO").Value), "", RsTrn.Fields("BILLNO").Value)
                    txtBillDate.Text = VB6.Format(IIf(IsDbNull(RsTrn.Fields("INVOICE_DATE").Value), "", RsTrn.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                    mSupplierCode = IIf(IsDbNull(RsTrn.Fields("SUPP_CUST_CODE").Value), "", RsTrn.Fields("SUPP_CUST_CODE").Value)

                    If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtSupplier.Text = MasterNo
                    End If

                    txtBillAmount.Text = VB6.Format(IIf(IsDbNull(RsTrn.Fields("NETVALUE").Value), 0, RsTrn.Fields("NETVALUE").Value), "0.00")
                    txtTaxableAmount.Text = VB6.Format(IIf(IsDbNull(RsTrn.Fields("TOTTAXABLEAMOUNT").Value), 0, RsTrn.Fields("TOTTAXABLEAMOUNT").Value), "0.00")
                    txtCGSTAmount.Text = VB6.Format(IIf(IsDbNull(RsTrn.Fields("TOTCGST_REFUNDAMT").Value), 0, RsTrn.Fields("TOTCGST_REFUNDAMT").Value), "0.00")
                    txtSGSTAmount.Text = VB6.Format(IIf(IsDbNull(RsTrn.Fields("TOTSGST_REFUNDAMT").Value), 0, RsTrn.Fields("TOTSGST_REFUNDAMT").Value), "0.00")
                    txtIGSTAmount.Text = VB6.Format(IIf(IsDbNull(RsTrn.Fields("TOTIGST_REFUNDAMT").Value), 0, RsTrn.Fields("TOTIGST_REFUNDAMT").Value), "0.00")
                    txtGSTAmount.Text = VB6.Format(Val(txtCGSTAmount.Text) + Val(txtSGSTAmount.Text) + Val(txtIGSTAmount.Text), "0.00")
                End If

                Call FillDebitComboBox(pTableName, pFieldName, mMKey)
                frmOClaimDetail.Enabled = False
            End If
        Else
            MsgInformation("Invalid Claim No & Date. Please check.")
        End If



        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetAlreadyReversalAmt(ByRef pOGSTNo As Integer, ByRef pOGSTDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mReversalAmount As Double

        mReversalAmount = 0
        SqlStr = "SELECT SUM(REVERSAL_GST_AMOUNT) AS REV_AMOUNT FROM FIN_GSTREVERSAL_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND O_GST_CLAIM_NO=" & Val(CStr(pOGSTNo)) & "" & vbCrLf _
            & " AND O_GST_CLAIM_DATE=TO_DATE('" & VB6.Format(pOGSTDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ISFINALPOST='Y' AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mReversalAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("REV_AMOUNT").Value), 0, RsTemp.Fields("REV_AMOUNT").Value), "0.00"))
        End If


        GetAlreadyReversalAmt = mReversalAmount

        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillDebitComboBox(ByRef pTableName As String, ByRef pFieldName As String, ByRef pMKey As String)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        cboDebitAccount.Items.Clear()

        SqlStr = "SELECT DISTINCT CMST.SUPP_CUST_NAME " & vbCrLf & " FROM " & pTableName & " TRN, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.MKEY ='" & pMKey & "'" & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND TRN." & pFieldName & "=CMST.SUPP_CUST_CODE " & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                cboDebitAccount.Items.Add(RsTemp.Fields("SUPP_CUST_NAME").Value)
                RsTemp.MoveNext()
            Loop
        End If

        cboDebitAccount.SelectedIndex = -1

        Exit Sub
ErrPart:
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

        Call CalcTots()

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

        SqlStr = " SELECT * FROM FIN_GSTREVERSAL_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

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
                SqlStr = "SELECT * FROM FIN_GSTREVERSAL_TRN " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMkey) & "' "
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
        Dim nMkey As String
        Dim pVoucherMkey As String
        Dim mReversalRule As String
        Dim mDebitAccountCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mFinalPost = IIf(chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mDebitAccountCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((cboDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDebitAccountCode = MasterNo
        Else
            mDebitAccountCode = CStr(-1)
            MsgBox("Debit Account Code Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If


        mReversalRule = VB.Left(cboReversalRule.Text, 1)

        If Trim(txtGSTNo.Text) = "" Then
            mVNO = CDbl(AutoGenSeqBillNo())
            txtGSTNo.Text = VB6.Format(mVNO, "00000")
        Else
            mVNO = Val(txtGSTNo.Text)
        End If


        SqlStr = ""
        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_GSTREVERSAL_TRN", "ROWNO", PubDBCn)
            nMkey = (RsCompany.Fields("COMPANY_CODE").Value) & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey


            SqlStr = " INSERT INTO FIN_GSTREVERSAL_TRN( " & vbCrLf & " MKEY, ROWNO, COMPANY_CODE, " & vbCrLf & " FYEAR, SUPP_CUST_CODE, ACCOUNT_CODE, REFNO, " & vbCrLf & " REFDATE, O_GST_CLAIM_NO, O_GST_CLAIM_DATE, " & vbCrLf & " VNO, VDATE, " & vbCrLf & " BILLNO, INVOICE_DATE, BILLAMOUNT, " & vbCrLf & " TAXABLEAMOUNT, CGST_AMOUNT, SGST_AMOUNT, " & vbCrLf & " IGST_AMOUNT, TOTALGST_AMOUNT, REVERSAL_RULE, " & vbCrLf & " REVERSAL_CGST_AMOUNT, REVERSAL_SGST_AMOUNT, REVERSAL_IGST_AMOUNT, " & vbCrLf & " INTEREST_AMOUNT, REVERSAL_GST_AMOUNT, TOTAL_REVERSAL_AMOUNT, REMARKS, " & vbCrLf & " JVMKEY, CANCELLED, ISFINALPOST, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE " & vbCrLf & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & nMkey & "'," & mCurRowNo & ", " & RsCompany.Fields("Company_Code").Value & ",  " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote(mDebitAccountCode) & "', " & mVNO & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtOGSTNo.Text) & "', TO_DATE('" & VB6.Format(txtOGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtVNo.Text) & "', TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtBillAmount.Text) & "," & vbCrLf & " " & Val(txtTaxableAmount.Text) & ", " & Val(txtCGSTAmount.Text) & ", " & Val(txtSGSTAmount.Text) & ", " & vbCrLf & " " & Val(txtIGSTAmount.Text) & "," & Val(txtGSTAmount.Text) & ", '" & mReversalRule & "'," & vbCrLf & " " & Val(txtReversalCGSTAmount.Text) & ", " & Val(txtReversalSGSTAmount.Text) & ", " & Val(txtReversalIGSTAmount.Text) & "," & vbCrLf & " " & Val(txtInterestAmount.Text) & ", " & Val(txtNetReversalGSTAmount.Text) & ", " & Val(txtReversalAmount.Text) & ", '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " '" & lblJVMkey.Text & "', '" & mCancelled & "', '" & mFinalPost & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''" & vbCrLf & " )"


        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_GSTREVERSAL_TRN SET " & vbCrLf & " COMPANY_CODE = " & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf & " FYEAR = " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', " & vbCrLf & " ACCOUNT_CODE = '" & MainClass.AllowSingleQuote(mDebitAccountCode) & "', " & vbCrLf & " REFNO = " & mVNO & ", " & vbCrLf _
                & " REFDATE = TO_DATE('" & VB6.Format(txtGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " O_GST_CLAIM_NO='" & MainClass.AllowSingleQuote(txtOGSTNo.Text) & "', " & vbCrLf _
                & " O_GST_CLAIM_DATE=TO_DATE('" & VB6.Format(txtOGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " VNO='" & MainClass.AllowSingleQuote(txtVNo.Text) & "', " & vbCrLf _
                & " VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " BILLNO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf & " INVOICE_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " BILLAMOUNT=" & Val(txtBillAmount.Text) & ", " & vbCrLf & " TAXABLEAMOUNT=" & Val(txtTaxableAmount.Text) & ", " & vbCrLf & " CGST_AMOUNT=" & Val(txtCGSTAmount.Text) & ", " & vbCrLf & " SGST_AMOUNT=" & Val(txtSGSTAmount.Text) & ", " & vbCrLf & " IGST_AMOUNT=" & Val(txtIGSTAmount.Text) & ", " & vbCrLf & " TOTALGST_AMOUNT=" & Val(txtGSTAmount.Text) & ", " & vbCrLf & " REVERSAL_RULE='" & mReversalRule & "', " & vbCrLf & " REVERSAL_CGST_AMOUNT=" & Val(txtReversalCGSTAmount.Text) & ", " & vbCrLf & " REVERSAL_SGST_AMOUNT=" & Val(txtReversalSGSTAmount.Text) & ", " & vbCrLf & " REVERSAL_IGST_AMOUNT=" & Val(txtReversalIGSTAmount.Text) & ", " & vbCrLf & " INTEREST_AMOUNT=" & Val(txtInterestAmount.Text) & ", " & vbCrLf & " REVERSAL_GST_AMOUNT=" & Val(txtNetReversalGSTAmount.Text) & ","

            SqlStr = SqlStr & vbCrLf & " TOTAL_REVERSAL_AMOUNT=" & Val(txtReversalAmount.Text) & ", " & vbCrLf & " REMARKS = '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " CANCELLED = '" & mCancelled & "', " & vbCrLf & " ISFINALPOST= '" & mFinalPost & "', " & vbCrLf & " JVMKEY = '" & lblJVMkey.Text & "', "

            SqlStr = SqlStr & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)

        If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then

            If GenerateVoucher(pVoucherMkey, 1, IIf(ADDMode = True, True, False)) = False Then
                GoTo ErrPart
            End If

            SqlStr = "UPDATE FIN_GSTREVERSAL_TRN SET  JVMKEY='" & pVoucherMkey & "' WHERE MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
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


        I = I + 1
        mPRRowNo = I
        mDC = "D"
        mAmount = Val(txtNetReversalGSTAmount.Text) + Val(txtInterestAmount.Text)

        mAccountName = Trim(cboDebitAccount.Text)

        If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            mAccountCode = "-1"
        Else
            mAccountCode = MasterNo
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid Debit Account Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If


        I = I + 1
        mPRRowNo = I
        mDC = "C"
        mAmount = Val(txtReversalCGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("CGST_REFUNDCODE").Value), "-1", RsCompany.Fields("CGST_REFUNDCODE").Value)

        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            mAccountCode = "-1"
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid CGST Recoverable Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        I = I + 1
        mPRRowNo = I
        mDC = "C"
        mAmount = Val(txtReversalSGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("SGST_REFUNDCODE").Value), "-1", RsCompany.Fields("SGST_REFUNDCODE").Value)

        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            mAccountCode = "-1"
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid CGST Recoverable Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        I = I + 1
        mPRRowNo = I
        mDC = "C"
        mAmount = Val(txtReversalIGSTAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("IGST_REFUNDCODE").Value), "-1", RsCompany.Fields("IGST_REFUNDCODE").Value)

        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            mAccountCode = "-1"
        End If

        If mAccountCode = "-1" Or mAccountCode = "" Then
            MsgInformation("Invalid IGST Recoverable Code, Please contact to Administrator.")
            GenerateJVDetail = False
            Exit Function
        Else
            If GeneratePostingDetail(mMKey, mRowNo, mBookCode, pJVBookType, mVType, mVNO, mVDate, pNarration, mDivCode, pDBCn, mAccountCode, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mDC, mParticulars, mPRRowNo, I, mBookType, mBookSubType, mClearDate, mParticulars) = False Then GoTo ErrDetail
        End If

        '************************** Inserest Posting Payable

        I = I + 1
        mPRRowNo = I
        mDC = "C"
        mAmount = Val(txtInterestAmount.Text)
        mAccountCode = IIf(IsDbNull(RsCompany.Fields("GST_INTEREST_ACCTCODE").Value), "-1", RsCompany.Fields("GST_INTEREST_ACCTCODE").Value)

        If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            mAccountCode = "-1"
        End If

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
        Dim mMAxNo As Double

        SqlStr = ""


        SqlStr = "SELECT Max(REFNO)  FROM FIN_GSTREVERSAL_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransferTrnGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTransferTrnGen
            If .EOF = False Then
                mMAxNo = IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value)
                If mMAxNo = -1 Then
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

        If cboReversalRule.SelectedIndex = -1 Then
            MsgBox("Please Select the Reversal under rule.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If cboDebitAccount.SelectedIndex = -1 Then
            MsgBox("Please Select the Debit Account.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Debit Account Code, Please contact to Administrator.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtNetReversalGSTAmount.Text) <= 0 Then
            MsgBox("Please Enter the Reversal GST Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtReversalAmount.Text) <= 0 Then
            MsgBox("Please Enter the Taxable Reversal Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtTaxableAmount.Text) < Val(txtReversalAmount.Text) Then
            MsgBox("Reversal Taxable Amount Cann't be Greater than Taxable Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtGSTAmount.Text) + Val(txtAlreadyReversalAmt.Text) < Val(txtNetReversalGSTAmount.Text) Then
            MsgBox("Reversal GST Amount Cann't be Greater than Claimed GST Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtCGSTAmount.Text) < Val(txtReversalCGSTAmount.Text) Then
            MsgBox("Reversal CGST Amount Cann't be Greater than Claimed CGST Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtSGSTAmount.Text) < Val(txtReversalSGSTAmount.Text) Then
            MsgBox("Reversal SGST Amount Cann't be Greater than Claimed SGST Amount.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtIGSTAmount.Text) < Val(txtReversalIGSTAmount.Text) Then
            MsgBox("Reversal IGST Amount Cann't be Greater than Claimed IGST Amount.", MsgBoxStyle.Information)
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

        mNetAmount = Val(txtReversalCGSTAmount.Text) + Val(txtReversalSGSTAmount.Text) + Val(txtReversalIGSTAmount.Text)
        txtNetReversalGSTAmount.Text = VB6.Format(mNetAmount, "0.00")

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmGSTReversalEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from FIN_GSTREVERSAL_TRN Where 1<>1"
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

        SqlStr = SqlStr & vbCrLf & " REVERSAL_RULE AS REVERSAL_RULE, "

        SqlStr = SqlStr & vbCrLf & "REVERSAL_CGST_AMOUNT, REVERSAL_SGST_AMOUNT, REVERSAL_IGST_AMOUNT,"

        SqlStr = SqlStr & vbCrLf & " DECODE(CANCELLED,'Y','YES','NO') AS CANCELLED "
        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf & " FIN_GSTREVERSAL_TRN IH, FIN_SUPP_CUST_MST A " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND IH.COMPANY_CODE=A.COMPANY_CODE(+) " & vbCrLf & " AND IH.SUPP_CUST_CODE=A.SUPP_CUST_CODE(+) "

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
        Dim mReversalRule As String
        Dim mStatus As String

        Dim mBookCode As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mMKey As String
        Dim mFYear As Integer
        Dim mSupplierCode As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pTableName As String
        Dim pFieldName As String
        Dim mDebitAccountCode As String

        With RsTransferTrn
            If Not .EOF Then
                lblMkey.Text = .Fields("MKey").Value
                lblJVMkey.Text = IIf(IsDbNull(.Fields("JVMKEY").Value), "", .Fields("JVMKEY").Value)
                txtGSTNo.Text = VB6.Format(IIf(IsDbNull(.Fields("REFNO").Value), "", .Fields("REFNO").Value), "00000")
                txtGSTDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REFDATE").Value), "", .Fields("REFDATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If

                mDebitAccountCode = IIf(IsDbNull(.Fields("ACCOUNT_CODE").Value), "", .Fields("ACCOUNT_CODE").Value)

                mStatus = IIf(IsDbNull(.Fields("ISFINALPOST").Value), "N", .Fields("ISFINALPOST").Value)
                chkFinalPost.CheckState = IIf(mStatus = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mReversalRule = IIf(IsDbNull(.Fields("REVERSAL_RULE").Value), "a", .Fields("REVERSAL_RULE").Value)
                If mReversalRule = "a" Then
                    cboReversalRule.SelectedIndex = 0
                ElseIf mReversalRule = "b" Then
                    cboReversalRule.SelectedIndex = 1
                ElseIf mReversalRule = "c" Then
                    cboReversalRule.SelectedIndex = 2
                ElseIf mReversalRule = "d" Then
                    cboReversalRule.SelectedIndex = 3
                ElseIf mReversalRule = "e" Then
                    cboReversalRule.SelectedIndex = 4
                ElseIf mReversalRule = "f" Then
                    cboReversalRule.SelectedIndex = 5
                Else
                    cboReversalRule.SelectedIndex = 6
                End If

                txtOGSTNo.Text = VB6.Format(IIf(IsDbNull(.Fields("O_GST_CLAIM_NO").Value), "", .Fields("O_GST_CLAIM_NO").Value), "00000")
                txtOGSTDate.Text = VB6.Format(IIf(IsDbNull(.Fields("O_GST_CLAIM_DATE").Value), "", .Fields("O_GST_CLAIM_DATE").Value), "DD/MM/YYYY")

                txtVNo.Text = VB6.Format(IIf(IsDbNull(.Fields("VNO").Value), "", .Fields("VNO").Value), "00000")
                txtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("VDATE").Value), "", .Fields("VDATE").Value), "DD/MM/YYYY")

                txtBillNo.Text = VB6.Format(IIf(IsDbNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value), "00000")
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                txtBillAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("BILLAMOUNT").Value), 0, .Fields("BILLAMOUNT").Value), "0.00")
                txtTaxableAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TAXABLEAMOUNT").Value), 0, .Fields("TAXABLEAMOUNT").Value), "0.00")
                txtCGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("CGST_AMOUNT").Value), 0, .Fields("CGST_AMOUNT").Value), "0.00")
                txtSGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("SGST_AMOUNT").Value), 0, .Fields("SGST_AMOUNT").Value), "0.00")
                txtIGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("IGST_AMOUNT").Value), 0, .Fields("IGST_AMOUNT").Value), "0.00")
                txtGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTALGST_AMOUNT").Value), 0, .Fields("TOTALGST_AMOUNT").Value), "0.00")

                txtReversalCGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("REVERSAL_CGST_AMOUNT").Value), 0, .Fields("REVERSAL_CGST_AMOUNT").Value), "0.00")
                txtReversalSGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("REVERSAL_SGST_AMOUNT").Value), 0, .Fields("REVERSAL_SGST_AMOUNT").Value), "0.00")
                txtReversalIGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("REVERSAL_IGST_AMOUNT").Value), 0, .Fields("REVERSAL_IGST_AMOUNT").Value), "0.00")
                txtInterestAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("INTEREST_AMOUNT").Value), 0, .Fields("INTEREST_AMOUNT").Value), "0.00")
                txtReversalAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTAL_REVERSAL_AMOUNT").Value), 0, .Fields("TOTAL_REVERSAL_AMOUNT").Value), "0.00")
                txtNetReversalGSTAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("REVERSAL_GST_AMOUNT").Value), 0, .Fields("REVERSAL_GST_AMOUNT").Value), "0.00")


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

                SqlStr = "SELECT * FROM FIN_GST_NEWSEQ_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GST_CLAIM_NO=" & Val(txtOGSTNo.Text) & "" & vbCrLf _
                    & " AND GST_CLAIM_DATE=TO_DATE('" & VB6.Format(txtOGSTDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND INPUT_REVERSE='I' AND CLAIM_TYPE='Y'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mBookCode = IIf(IsDbNull(RsTemp.Fields("BOOKCODE").Value), -1, RsTemp.Fields("BOOKCODE").Value)
                    mBookType = IIf(IsDbNull(RsTemp.Fields("BOOKTYPE").Value), "", RsTemp.Fields("BOOKTYPE").Value)
                    mBookSubType = IIf(IsDbNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)
                    mMKey = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "-1", RsTemp.Fields("mKey").Value)
                    mFYear = IIf(IsDbNull(RsTemp.Fields("FYEAR").Value), 0, RsTemp.Fields("FYEAR").Value)

                    If mBookCode = ConSalesBookCode Then
                        pTableName = "FIN_INVOICE_HDR"
                        pFieldName = "ACCOUNTCODE"
                    ElseIf mBookCode = ConPurchaseBookCode Then
                        pTableName = "FIN_PURCHASE_DET"
                        pFieldName = "PUR_ACCOUNT_CODE"
                    ElseIf mBookCode = ConPurchaseSuppBookCode Then
                        pTableName = "FIN_SUPP_PURCHASE_DET"
                        pFieldName = "PUR_ACCOUNT_CODE"
                    ElseIf mBookCode = ConLCBookCode And mBookSubType = "P" Then
                        pTableName = "FIN_LCOPEN_DET"
                        pFieldName = "ACCOUNTCODE"
                    ElseIf mBookCode = ConLDBookCode And mBookSubType = "D" Then
                        pTableName = "FIN_LCDISC_DET"
                        pFieldName = "ACCOUNTCODE"
                    End If

                    Call FillDebitComboBox(pTableName, pFieldName, mMKey)

                    If MainClass.ValidateWithMasterTable(mDebitAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        cboDebitAccount.Text = MasterNo
                    End If


                    frmOClaimDetail.Enabled = False
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
        ''    Resume
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

        txtOGSTNo.Text = ""
        txtOGSTDate.Text = ""
        txtVNo.Text = ""
        txtVDate.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = ""
        txtSupplier.Text = ""
        txtBillAmount.Text = "0.00"
        txtTaxableAmount.Text = "0.00"
        txtCGSTAmount.Text = "0.00"
        txtSGSTAmount.Text = "0.00"
        txtIGSTAmount.Text = "0.00"
        txtGSTAmount.Text = "0.00"
        txtReversalCGSTAmount.Text = "0.00"
        txtReversalSGSTAmount.Text = "0.00"
        txtReversalIGSTAmount.Text = "0.00"
        txtInterestAmount.Text = "0.00"
        txtNetReversalGSTAmount.Text = "0.00"
        txtReversalAmount.Text = "0.00"
        txtRemarks.Text = ""
        txtJVNo.Text = ""
        txtJVDate.Text = ""
        cboReversalRule.SelectedIndex = -1
        cboDebitAccount.SelectedIndex = -1

        chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked
        chkFinalPost.Enabled = False
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCancelled.Enabled = False
        cmdPopulate.Enabled = True

        frmOClaimDetail.Enabled = True

        MainClass.ButtonStatus(Me, XRIGHT, RsTransferTrn, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmGSTReversalEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmGSTReversalEntry_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub FrmGSTReversalEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Height = VB6.TwipsToPixelsY(7695) '8000
        'Me.Width = VB6.TwipsToPixelsX(10140) '11900
        'AdoDCMain.Visible = False


        cboReversalRule.Items.Clear()
        cboReversalRule.Items.Add("a : Rule 37(2)")
        cboReversalRule.Items.Add("b : Rule 42(1)m")
        cboReversalRule.Items.Add("c : Rule 43(1)h")
        cboReversalRule.Items.Add("d : Rule 42(2)a")
        cboReversalRule.Items.Add("e : Rule 42(2)b")
        cboReversalRule.Items.Add("f : Rule 39(1)(j)(ii)")
        cboReversalRule.SelectedIndex = -1


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

    Private Sub txtInterestAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInterestAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInterestAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInterestAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtJVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJVDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtJVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtReversalAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReversalAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReversalAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReversalAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtReversalCGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReversalCGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReversalCGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReversalCGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtReversalCGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReversalCGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReversalSGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReversalSGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReversalIGSTAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReversalIGSTAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReversalIGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReversalIGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReversalIGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReversalIGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtReversalSGSTAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReversalSGSTAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtReversalSGSTAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReversalSGSTAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
