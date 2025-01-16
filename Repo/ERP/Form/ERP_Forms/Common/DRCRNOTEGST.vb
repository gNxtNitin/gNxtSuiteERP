Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel

'Imports QRCoder
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
Imports System.Drawing.Color
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Printing


Friend Class FrmDrCrNoteGST
    Inherits System.Windows.Forms.Form
    Dim RsDNCNMain As ADODB.Recordset ''Recordset
    Dim RsDNCNDetail As ADODB.Recordset ''Recordset
    Dim RsDNCNExp As ADODB.Recordset ''Recordset
    ''Private PvtDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String
    Dim pRound As Double
    Dim mBookType As String
    Dim mBookSubType As String
    Dim mFillData As Boolean
    Dim pShowCalc As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColPURMkey As Short = 1
    Private Const ColPurNO As Short = 2
    Private Const ColPurDATE As Short = 3
    Private Const ColMRRNo As Short = 4
    Private Const ColMRRDate As Short = 5
    Private Const ColBillNo As Short = 6
    Private Const ColBillDate As Short = 7
    Private Const ColRefNo As Short = 8
    Private Const ColRefDate As Short = 9
    Private Const ColItemCode As Short = 10
    Private Const ColPartNo As Short = 11
    Private Const ColItemDesc As Short = 12
    Private Const ColHSNCode As Short = 13
    Private Const ColBillQty As Short = 14
    Private Const ColUnit As Short = 15
    Private Const ColQty As Short = 16
    Private Const ColPORate As Short = 17
    Private Const ColBillRate As Short = 18
    Private Const ColRate As Short = 19
    Private Const ColAmount As Short = 20
    Private Const ColCGSTPer As Short = 21
    Private Const ColSGSTPer As Short = 22
    Private Const ColIGSTPer As Short = 23
    Private Const ColCGSTAmount As Short = 24
    Private Const ColSGSTAmount As Short = 25
    Private Const ColIGSTAmount As Short = 26
    Private Const ColPONo As Short = 27

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
    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

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
    Private Sub cboGSTStatus_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboGSTStatus.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkAproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAproved.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtVNo.Enabled = False
            pShowCalc = True
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdBarCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBarCode.Click
        FraRefund.Visible = Not FraRefund.Visible
        FraRefund.Enabled = FraRefund.Visible
    End Sub
    Private Sub cmdBillNoSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillNoSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mTableName As String
        Dim mFieldName1 As String
        Dim mFieldName2 As String
        Dim mFieldName3 As String
        Dim mFieldName4 As String
        Dim mFieldName5 As String
        Dim mAccountCode As String
        Dim SqlStrDr As String
        Dim SqlStrCr As String
        Dim mSearchQuery As String

        'optPopulate(0).Value = True
        If VB.Left(cboPopulateFrom.Text, 1) = "P" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ISFINALPOST='Y' AND VNO<>'-1'"
            mTableName = "FIN_PURCHASE_HDR"
            mFieldName1 = "VNO"
            mFieldName2 = "VDATE"
            mFieldName3 = "BILLNo"
            mFieldName4 = "BILL_TO_LOC_ID"
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            End If
            If mAccountCode <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & mAccountCode & "'"
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "M" Or VB.Left(cboPopulateFrom.Text, 1) = "S" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
            mTableName = "INV_GATE_HDR"
            mFieldName1 = "AUTO_KEY_MRR"
            mFieldName2 = "BILL_No"
            mFieldName3 = "BILL_Date"
            mFieldName4 = "BILL_TO_LOC_ID"
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            End If
            If mAccountCode <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & mAccountCode & "'"
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "D" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND APPROVED='Y' AND CANCELLED='N' "
            mTableName = "FIN_DNCN_HDR"
            mFieldName1 = "VNO"
            mFieldName2 = "VDATE"
            mFieldName3 = "BILLNo"
            mFieldName4 = "BILL_TO_LOC_ID"
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
                If mAccountCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND CREDITACCOUNTCODE='" & mAccountCode & "'"
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
                If mAccountCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND DEBITACCOUNTCODE='" & mAccountCode & "'"
                End If
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "R" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND APPROVED='Y' AND CANCELLED='N'  AND DNCNTYPE='R' AND AUTO_KEY_MRR>0"            ''AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " 
            mTableName = "FIN_DNCN_HDR"
            mFieldName1 = "VNO"
            mFieldName2 = "VDATE"
            mFieldName3 = "BILLNo"
            mFieldName4 = "BILL_TO_LOC_ID"
            mFieldName5 = "AUTO_KEY_MRR"
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
                If mAccountCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND CREDITACCOUNTCODE='" & mAccountCode & "'"
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
                If mAccountCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND DEBITACCOUNTCODE='" & mAccountCode & "'"
                End If
            End If
        Else
            SearchBillNo()
            Exit Sub
        End If
        If MainClass.SearchGridMaster((txtPurVNo.Text), mTableName, mFieldName1, mFieldName2, mFieldName3, mFieldName4, SqlStr,,, mFieldName5) = True Then
            txtPurVNo.Text = AcName
            'TxtName_Validate False
            If VB.Left(cboPopulateFrom.Text, 1) = "R" Then
                txtPurVNo.Text = AcName
                txtPurVDate.Text = AcName1
                txtBillNo.Text = AcName2
                txtBillTo.Text = AcName3
                txtMRRNo.Text = AcName4
            End If
            If txtPurVNo.Enabled = True Then txtPurVNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim xDCNo As String
        Dim RsTemp As ADODB.Recordset
        Dim mBookCode As Integer
        Dim mAccountCode As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim cntRow As Integer
        Dim mLockBookCode As Integer
        If ValidateBranchLocking((txtVDate.Text)) = True Then
            Exit Sub
        End If
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mLockBookCode = CInt(ConLockDebitNote)
        Else
            mLockBookCode = CInt(ConLockCreditNote)
        End If
        If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtVDate.Text, (txtDebitAccount.Text)) = True Then
            Exit Sub
        End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Voucher Cann't be Deleted.")
            Exit Sub
        End If
        If chkAproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Approved Voucher Cann't be Deleted.")
            Exit Sub
        End If
        If Trim(txtVNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If chkAproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                    For cntRow = 1 To SprdMain.MaxRows
                        SprdMain.Row = cntRow
                        SprdMain.Col = ColBillNo
                        mBillNo = Trim(SprdMain.Text)
                        SprdMain.Col = ColBillDate
                        mBillDate = Trim(SprdMain.Text)
                        If mBillNo <> "" Then
                            If CheckBillPayment(mAccountCode, mBillNo, "D", mBillDate) = True Then Exit Sub
                        End If
                    Next
                End If
            Else
                If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                    For cntRow = 1 To SprdMain.MaxRows
                        SprdMain.Row = cntRow
                        SprdMain.Col = ColBillNo
                        mBillNo = Trim(SprdMain.Text)
                        SprdMain.Col = ColBillDate
                        mBillDate = Trim(SprdMain.Text)
                        If mBillNo <> "" Then
                            If CheckBillPayment(mAccountCode, mBillNo, "C", mBillDate) = True Then Exit Sub
                        End If
                    Next
                End If
            End If
        End If
        If Not RsDNCNMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "FIN_DNCN_HDR", (LblMKey.Text), RsDNCNMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_DNCN_DET", (LblMKey.Text), RsDNCNDetail, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_DNCN_EXP", (LblMKey.Text), RsDNCNExp, "MKEY", "D") = False Then GoTo DelErrPart


                If InsertIntoDeleteTrn(PubDBCn, "FIN_DNCN_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & LblBookCode.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'")
                PubDBCn.Execute("Delete from FIN_DNCN_EXP Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_DNCN_DET Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_DNCN_HDR Where Mkey='" & LblMKey.Text & "' AND BOOKCODE=" & Val(LblBookCode.Text) & "")
                PubDBCn.CommitTrans()
                RsDNCNMain.Requery() ''.Refresh
                RsDNCNDetail.Requery() ''.Refresh
                RsDNCNExp.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsDNCNMain.Requery() ''.Refresh
        RsDNCNDetail.Requery() ''.Refresh
        RsDNCNExp.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click
        On Error GoTo ModifyErr
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Voucher Cann't be Modified")
            Exit Sub
        End If
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDNCNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            '        txtVNo.Enabled = False
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
    Private Sub MRRSearch()
        'On Error GoTo ErrPart
        'Dim SqlStr  As String
        '
        ''    SqlStr = " SELECT INV_GATE_HDR.AUTO_KEY_MRR AS MRRNO, To_CHAR(INV_GATE_HDR.MRR_DATE,'DD/MM/YYYY') AS MRRDATE FROM INV_GATE_HDR, INV_GATE_DET " & vbCrLf _
        '            & " WHERE " & vbCrLf _
        '            & " INV_GATE_HDR.AUTO_KEY_MRR=INV_GATE_DET.AUTO_KEY_MRR" & vbCrLf _
        '            & " AND INV_GATE_HDR.Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        '            & " AND RECEIVED_QTY>0 AND REJ_RTN_STATUS='N'"
        '        ''& " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        '
        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND REJECTED_QTY>0 AND REJ_RTN_STATUS='N'"
        '
        ''    If MainClass.SearchGridMasterBySQL(txtMRRNo.Text, SqlStr) = True Then
        '    If MainClass.SearchGridMaster(txtMRRNo.Text, "INV_GATE_DET", "AUTO_KEY_MRR", "MRR_DATE", , , SqlStr) = True Then
        '        txtMRRNo.Text = AcName
        '        'TxtName_Validate False
        '        If txtMRRNo.Enabled = True Then txtMRRNo.SetFocus
        '    End If
        'Exit Sub
        'ErrPart:
        '    ErrorMsg err.Description, err.Number, vbCritical
    End Sub
    Private Sub cmdMRRNoSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMRRNoSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSqlStr As String
        Dim mAccountCode As String
        Dim mSqlQry As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If VB.Left(cboPopulateFrom.Text, 1) = "M" Then
            SqlStr = SqlStr & vbCrLf & " AND REJECTED_QTY>0 AND REJ_RTN_STATUS='N'"
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND MRR_FINAL_FLAG='N'"
        End If
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            If Trim(txtDebitAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                End If
            End If
        Else
            If Trim(txtCreditAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                End If
            End If
        End If
        If mAccountCode <> "" Then
            If VB.Left(cboPopulateFrom.Text, 1) = "R" Then
                SqlStr = SqlStr & vbCrLf & "AND INV_REOFFER_HDR.SUPP_CUST_CODE='" & mAccountCode & "'"
            Else
                SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & mAccountCode & "'"
            End If
        End If
        SqlStr = SqlStr & vbCrLf & " AND MRR_DATE<=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If VB.Left(cboPopulateFrom.Text, 1) = "M" Then
            If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_GATE_DET", "AUTO_KEY_MRR", "MRR_DATE", , , SqlStr) = True Then
                txtMRRNo.Text = AcName
                txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
                If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "S" Then
            If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_GATE_HDR", "AUTO_KEY_MRR", "MRR_DATE", "BILL_NO", "BILL_DATE", SqlStr) = True Then
                txtMRRNo.Text = AcName
                txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
                If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "R" Then
            SqlStr = SqlStr & "AND INV_REOFFER_HDR.IS_POSTED='Y'"
            If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_REOFFER_HDR", "AUTO_KEY_MRR", "AUTO_KEY_REF", "BILL_NO", "BILL_DATE", SqlStr) = True Then
                txtMRRNo.Text = AcName
                txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
                If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "J" Then
            SqlStr = SqlStr & " AND REF_TYPE='1' AND MRR_FINAL_FLAG='N'"
            mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_MRR IN (" & vbCrLf & " SELECT AUTO_KEY_MRR FROM INV_GATE_HDR WHERE " & vbCrLf & SqlStr & ")"
            If MainClass.SearchGridMaster((txtMRRNo.Text), "DSP_PAINT57F4_HDR", "AUTO_KEY_MRR", "MRR_DATE", "PARTY_F4NO", "BILL_NO", mSqlStr) = True Then
                txtMRRNo.Text = AcName
                txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
                If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPopulate_Click()
        'On Error GoTo ERR1
        'Dim RsTemp As ADODB.Recordset
        'Dim xMKey As String
        'Dim mVNo As String
        'Dim mAccountCode As String
        'Dim mRefNo As String
        'Dim mRefDate As String
        'Dim mRefType As String
        '
        'Dim mBillNo As String
        'Dim mBillType As String
        '
        'Dim mBillDate As String
        'Dim mDivisionCode As Double
        'Dim mDivisionName As String
        '
        '    If Trim(txtPurVNo.Text) = "" Then Exit Sub
        '
        '    If cboDivision.Text = "" Then
        '        If cboDivision.Enabled = True Then cboDivision.SetFocus
        '        MsgInformation "Please Select Division."
        '        Exit Sub
        '    End If
        '
        '    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDivisionCode = Trim(MasterNo)
        '    End If
        '
        '    mDivisionName = Trim(cboDivision.Text)
        '
        '    mRefNo = Trim(txtPurVNo.Text)
        '    mRefDate = Trim(txtPurVDate.Text)
        '
        '
        '    txtPurVNo.Text = ""
        '    txtPurVDate.Text = ""
        '    mRefType = Left(cboPopulateFrom.Text, 1)
        '
        ''    If mFillData = False Then
        '    Clear1
        ''    End If
        '
        '    cboDivision.Text = mDivisionName
        '
        '
        '    If DuplicateDatainGrid(mRefNo) = True Then
        '        MsgBox "Duplicate Ref No."
        '        Exit Sub
        '    End If
        '
        '    If mRefType = "P" Then
        '        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf _
        ''                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        ''                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
        ''                & " AND VNo='" & MainClass.AllowSingleQuote(mRefNo) & "' " & vbCrLf _
        ''                & " AND ISFINALPOST='Y' AND CANCELLED='N'"
        '
        '        If LblBookCode.text = ConDebitNoteBookCode Then
        '            If Trim(txtDebitAccount.Text) <> "" Then
        '                If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAccountCode = MasterNo
        '                End If
        '            End If
        '        Else
        '            If Trim(txtCreditAccount.Text) <> "" Then
        '                If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAccountCode = MasterNo
        '                End If
        '            End If
        '        End If
        '
        '        If mAccountCode <> "" Then
        '            SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & mAccountCode & "'"
        '        End If
        '
        '        SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '        If RsTemp.EOF = False Then
        '            Call ShowFromPurchase(RsTemp)
        '        Else
        '            MsgBox "Please Enter Valid Purchase Voucher No.", vbInformation
        '            If txtPurVNo.Enabled Then txtPurVNo.SetFocus
        '        End If
        '    ElseIf mRefType = "D" Then
        '        SqlStr = " SELECT * FROM FIN_DNCN_HDR " & vbCrLf _
        ''                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        ''                & " AND VNo='" & MainClass.AllowSingleQuote(mRefNo) & "'" & vbCrLf _
        ''                & " AND APPROVED='Y'" & vbCrLf _
        ''                & " AND CANCELLED='N'"
        '
        '        ''FYEAR=" & RsCompany.Fields("FYEAR").Value & "
        '
        '        If Trim(mRefDate) <> "" Then
        '            SqlStr = SqlStr & vbCrLf & "AND VDATE='" & vb6.Format(mRefDate, "DD-MMM-YYYY") & "'"
        '        Else
        '            SqlStr = SqlStr & vbCrLf & "AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        '        End If
        '
        '        If LblBookCode.text = ConDebitNoteBookCode Then
        '            If Trim(txtDebitAccount.Text) <> "" Then
        '                If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAccountCode = MasterNo
        '                End If
        '            End If
        '            If mAccountCode <> "" Then
        '                SqlStr = SqlStr & vbCrLf & "AND CREDITACCOUNTCODE='" & mAccountCode & "'"
        '            End If
        '        Else
        '            If Trim(txtCreditAccount.Text) <> "" Then
        '                If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAccountCode = MasterNo
        '                End If
        '            End If
        '            If mAccountCode <> "" Then
        '                SqlStr = SqlStr & vbCrLf & "AND DEBITACCOUNTCODE='" & mAccountCode & "'"
        '            End If
        '        End If
        '
        '        SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '        If RsTemp.EOF = False Then
        '            Call ShowFromDNCN(RsTemp)
        '        Else
        '            MsgBox "Please Enter Valid DN/CN Voucher No.", vbInformation
        '            If txtPurVNo.Enabled Then txtPurVNo.SetFocus
        '        End If
        '    ElseIf mRefType = "T" Then
        '        If LblBookCode.text = ConDebitNoteBookCode Then
        '            If Trim(txtDebitAccount.Text) = "" Then
        '                MsgBox "Please Select Debit Account First.", vbInformation
        '                Exit Sub
        '            Else
        '                If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAccountCode = MasterNo
        '                Else
        '                    mAccountCode = "-1"
        '                    MsgBox "Debit Account Does Not Exist In Master", vbInformation
        '                    Exit Sub
        '                End If
        '
        '            End If
        '        Else
        '            If Trim(txtCreditAccount.Text) = "" Then
        '                MsgBox "Please Select Credit Account First.", vbInformation
        '                Exit Sub
        '            Else
        '                If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                    mAccountCode = MasterNo
        '                Else
        '                    mAccountCode = "-1"
        '                    MsgBox "Credit Account Does Not Exist In Master", vbInformation
        '                    Exit Sub
        '                End If
        '
        '            End If
        '        End If
        '
        '
        '        SqlStr = " SELECT * FROM FIN_POSTED_TRN " & vbCrLf _
        ''                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        ''                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
        ''                & " AND BillNo='" & MainClass.AllowSingleQuote(mBillNo) & "' AND BillDate='" & vb6.Format(mBillDate, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'" & vbCrLf _
        ''                & " AND BILLTYPE='B'"
        '
        '        SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '        If RsTemp.EOF = False Then
        '            If ADDMode = True Then
        '                Clear1
        '                cboDivision.Text = mDivisionName
        '                Call ShowFromTRN(RsTemp)
        '            Else
        ''                txtBillNo.Text = Trim(mBillNo)
        ''                txtBillDate.Text = Trim(mBillDate)
        ''
        ''                txtPurVNo.Text = Trim(mRefNo)
        ''                txtPurVDate.Text = Trim(mRefDate)
        ''
        '            End If
        '        Else
        '            MsgBox "Please Enter Valid Bill No.", vbInformation
        '    '        Cancel = True
        '        End If
        '    End If
        '
        '    cmdVNoSearch.Enabled = True
        '    txtPurVNo.Enabled = True
        '    cboPopulateFrom.Enabled = True
        '
        '    Exit Sub
        'ERR1:
        '    ErrorMsg err.Description, err.Number, vbCritical
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
            Call FormatSprdPostingDetail(-1)
        End If
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
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDrCr(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDrCr(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnDrCr(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Dim mVNo As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        mVNo = Trim(txtVType.Text) & txtVNoPrefix.Text & txtVNo.Text & txtVNoSuffix.Text
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)
        Call SelectQryForVoucher(SqlStr)
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mTitle = "Debit Note"
            If lblDCType.Text = "R" Then
                mRptFileName = "DrNote_GST_Rej.rpt"
            Else
                mRptFileName = "DrNote_GST.rpt"
            End If
        Else
            mTitle = "Credit Note"
            If lblDCType.Text = "R" Then
                mRptFileName = "CrNote_GST_Rej.rpt"
            Else
                mRptFileName = "CrNote_GST.rpt"
            End If
        End If
        mTitle = mTitle
        mSubTitle = " (" & IIf(txtReason.Text = "", "OTHERS", txtReason.Text) & ")"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mStateName As String
        Dim mStateCode As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & txtBillTo.Text & "'") = True Then
                mStateName = MasterNo
                mStateCode = GetStateCode(mStateName)
            End If
        Else
            If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & txtBillTo.Text & "'") = True Then
                mStateName = MasterNo
                mStateCode = GetStateCode(mStateName)
            End If
        End If
        MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
        mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.Text) = 0, 0, lblNetAmount.Text)))
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MainClass.AssignCRptFormulas(Report1, "AmountInWord=""Rs. Zero""")
            MainClass.AssignCRptFormulas(Report1, "NetAmount=""0.00""")
        Else
            MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
            MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & lblNetAmount.Text & """")
        End If
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        ''"DrNoteKJSub"
        SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf & " FROM FIN_DNCN_EXP, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_DNCN_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_DNCN_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IDENTIFICATION NOT IN ('CGS','SGS','IGS')"
        If CDate(txtVDate.Text) >= CDate(PubGSTApplicableDate) Then 'Change on 29/010/2017 before If CDate(txtVDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"
        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStrSub
        Report1.SubreportToChange = ""
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
        Report1.ReportFileName = ""
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub
    Private Function SelectQryForVoucher(ByRef mSqlStr As String) As String
        ''SELECT CLAUSE...
        mSqlStr = " SELECT " & vbCrLf _
            & " IH.VNOPREFIX, IH.VNOSEQ, IH.VNOSUFFIX," & vbCrLf _
            & " IH.VNO, IH.VDATE, IH.PURVNO, IH.PURVDATE," & vbCrLf _
            & " IH.BILLNO, IH.INVOICE_DATE, IH.AUTO_KEY_MRR, IH.MRRDATE," & vbCrLf _
            & " IH.DEBITACCOUNTCODE, IH.CREDITACCOUNTCODE," & vbCrLf _
            & " IH.REMARKS, IH.REASON, IH.NARRATION, IH.DNCNTYPE,"

        mSqlStr = mSqlStr & " ID.SUBROWNO, ID.ITEM_CODE, " & vbCrLf _
            & " ID.ITEM_DESC, " & vbCrLf _
            & " ID.ITEM_QTY, ID.ITEM_UOM, " & vbCrLf _
            & " ID.ITEM_RATE, ID.ITEM_AMT, " & vbCrLf _
            & " ID.SUPP_REF_NO, ID.SUPP_REF_DATE," & vbCrLf _
            & " ID.REF_PO_NO "

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
            & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
            & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
            & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
            & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf _
            & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf _
            & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf _
            & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf _
            & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf _
            & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf _
            & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST "

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf _
            & " AND IH.BOOKTYPE='" & mBookType & "'" & vbCrLf _
            & " AND IH.BOOKSUBTYPE='" & mBookSubType & "'" & vbCrLf _
            & " AND IH.APPROVED='Y'" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY(+)"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE"
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.DEBITACCOUNTCODE=CMST.SUPP_CUST_CODE"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.CREDITACCOUNTCODE=CMST.SUPP_CUST_CODE"
        End If

        mSqlStr = mSqlStr & vbCrLf _
            & " AND CMST.COMPANY_CODE=BMST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=BMST.LOCATION_ID"

        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"
        SelectQryForVoucher = mSqlStr
    End Function
    Private Sub cmdResetMRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdResetMRR.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mAccountCode As String
        Dim cntRow As Integer
        Dim mMRRNO As Double
        Dim RsTemp As ADODB.Recordset
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mItemCode As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            If Trim(txtDebitAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                End If
            End If
        Else
            If Trim(txtCreditAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountCode = MasterNo
                End If
            End If
        End If
        '    SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf _
        ''                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        ''                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
        ''                & " AND VNo='" & MainClass.AllowSingleQuote(txtPurVNo.Text) & "' AND VDATE='" & vb6.Format(txtPurVDate.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                & " AND ISFINALPOST='Y' AND CANCELLED='N'"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '    If RsTemp.EOF = False Then
        '        txtBillNo.Text = RsTemp.Fields("BILLNO").Value
        '        txtBillDate.Text = Format(RsTemp.Fields("INVOICE_DATE").Value, "DD/MM/YYYY")
        '    End If
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColMRRNo
                mMRRNO = Val(.Text)
                SqlStr = " SELECT * FROM INV_GATE_HDR WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(CStr(mMRRNO)) & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    mBillNo = RsTemp.Fields("BILL_NO").Value
                    mBillDate = RsTemp.Fields("BILL_DATE").Value
                    .Col = ColBillNo
                    .Text = Trim(mBillNo)
                    .Col = ColBillDate
                    .Text = VB6.Format(mBillDate, "DD/MM/YYYY")
                End If
            Next
        End With
        If lblDCType.Text = "S" Then
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColMRRNo
                    mMRRNO = Val(.Text)
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    SqlStr = " SELECT ID.CGST_PER, ID.SGST_PER, ID.IGST_PER " & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID " & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(CStr(mMRRNO)) & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' "
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mCGSTPer = IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value)
                        mSGSTPer = IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value)
                        mIGSTPer = IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value)
                        .Col = ColCGSTPer
                        .Text = VB6.Format(mCGSTPer, "0.00")
                        .Col = ColSGSTPer
                        .Text = VB6.Format(mSGSTPer, "0.00")
                        .Col = ColIGSTPer
                        .Text = VB6.Format(mIGSTPer, "0.00")
                    End If
                Next
            End With
            CalcTots()
        End If
        '    SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf _
        ''                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        ''                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
        ''                & " AND VNo='" & MainClass.AllowSingleQuote(txtPurVNo.Text) & "' AND VDATE='" & vb6.Format(txtPurVDate.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                & " AND ISFINALPOST='Y' AND CANCELLED='N'"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '    If RsTemp.EOF = False Then
        '        txtBillNo.Text = RsTemp.Fields("BILLNO").Value
        '        txtBillDate.Text = Format(RsTemp.Fields("INVOICE_DATE").Value, "DD/MM/YYYY")
        '    End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If PubUserID <> "G0416" Then
            If FieldsVarification() = False Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If
        Call CalcTots()
        If UpdateMain1() = True Then
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
    Private Sub cmdVNoSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdVNoSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mTableName As String
        Dim mFieldName1 As String
        Dim mFieldName2 As String
        Dim mFieldName3 As String
        Dim mAccountCode As String
        Dim SqlStrDr As String
        Dim SqlStrCr As String
        Dim mDivisionCode As Double
        Dim mSqlStr As String
        If Trim(cboDivision.Text) = "" Then
            MsgInformation("Please select Division First")
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Sub
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        If VB.Left(cboPopulateFrom.Text, 1) = "P" Then

            mTableName = "FIN_PURCHASE_HDR"
            mFieldName1 = "VNO"
            mFieldName2 = "TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE"
            mFieldName3 = "BILLNo"

            SqlStr = " SELECT IH.VNO, IH.VDATE, CMST.SUPP_CUST_NAME, BILLNo, INVOICE_DATE" & vbCrLf _
                & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR IN (" & RsCompany.Fields("FYEAR").Value & "," & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf _
                & " AND ISFINALPOST='Y' AND VNO<>'-1'"

            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            End If
            If mAccountCode <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & mAccountCode & "'"
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""
            'If MainClass.SearchGridMaster((txtPurVNo.Text), mTableName, mFieldName1, mFieldName2, mFieldName3, , SqlStr) = True Then
            If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                txtPurVNo.Text = AcName
                txtPurVDate.Text = AcName1
                'TxtName_Validate False
                If txtPurVNo.Enabled = True Then txtPurVNo.Focus()
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "M" Or VB.Left(cboPopulateFrom.Text, 1) = "S" Or VB.Left(cboPopulateFrom.Text, 1) = "R" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If VB.Left(cboPopulateFrom.Text, 1) = "M" Then
                SqlStr = SqlStr & vbCrLf & " AND REJECTED_QTY>0 AND REJ_RTN_STATUS='N'"
            ElseIf VB.Left(cboPopulateFrom.Text, 1) = "R" Then
                SqlStr = SqlStr & vbCrLf & " AND MRR_FINAL_FLAG='N'"
            End If
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            End If
            If mAccountCode <> "" Then
                If VB.Left(cboPopulateFrom.Text, 1) = "R" Then
                    SqlStr = SqlStr & vbCrLf & "AND INV_REOFFER_HDR.SUPP_CUST_CODE='" & mAccountCode & "'"
                Else
                    SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & mAccountCode & "'"
                End If
            End If
            SqlStr = SqlStr & vbCrLf & " AND MRR_DATE<=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            If VB.Left(cboPopulateFrom.Text, 1) = "M" Then
                If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_GATE_DET", "AUTO_KEY_MRR", "MRR_DATE", , , SqlStr) = True Then
                    txtMRRNo.Text = AcName
                    txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
                    If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
                End If
            ElseIf VB.Left(cboPopulateFrom.Text, 1) = "S" Then
                If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_GATE_HDR", "AUTO_KEY_MRR", "MRR_DATE", "BILL_NO", "BILL_DATE", SqlStr) = True Then
                    txtMRRNo.Text = AcName
                    txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
                    If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
                End If
            ElseIf VB.Left(cboPopulateFrom.Text, 1) = "R" Then
                SqlStr = SqlStr & "AND INV_REOFFER_HDR.IS_POSTED='Y'"
                If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_REOFFER_HDR", "AUTO_KEY_MRR", "AUTO_KEY_REF", "BILL_NO", "BILL_DATE", SqlStr) = True Then
                    txtMRRNo.Text = AcName
                    txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
                    If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
                End If
            ElseIf VB.Left(cboPopulateFrom.Text, 1) = "J" Then
                SqlStr = SqlStr & " AND REF_TYPE='1' AND MRR_FINAL_FLAG='N'"
                mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_MRR IN (" & vbCrLf & " SELECT AUTO_KEY_MRR FROM INV_GATE_HDR WHERE " & vbCrLf & SqlStr & ")"
                If MainClass.SearchGridMaster((txtMRRNo.Text), "DSP_PAINT57F4_HDR", "AUTO_KEY_MRR", "MRR_DATE", "PARTY_F4NO", "BILL_NO", mSqlStr) = True Then
                    txtMRRNo.Text = AcName
                    txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
                    If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
                End If
            End If
            '
            '
            '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            ''            & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
            '
            '        mTableName = "INV_GATE_HDR"
            '        mFieldName1 = "AUTO_KEY_MRR"
            '        mFieldName2 = "BILL_NO"
            '        mFieldName3 = "BILL_DATE"
            '        If LblBookCode.text = ConDebitNoteBookCode Then
            '            If Trim(txtDebitAccount.Text) <> "" Then
            '                If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '                    mAccountCode = MasterNo
            '                End If
            '            End If
            '        Else
            '            If Trim(txtCreditAccount.Text) <> "" Then
            '                If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '                    mAccountCode = MasterNo
            '                End If
            '            End If
            '        End If
            '
            '        If mAccountCode <> "" Then
            '            SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & mAccountCode & "'"
            '        End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "D" Or (VB.Left(cboPopulateFrom.Text, 1) = "O" And CDbl(LblBookCode.Text) = ConCreditNoteBookCode And lblDCType.Text = "R") Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND APPROVED='Y' AND CANCELLED='N' AND DNCNTYPE='" & lblDCType.Text & "'"
            ''AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " ''FYEAR NOT REQUIRED..
            mTableName = "FIN_DNCN_HDR"
            mFieldName1 = "VNO"
            mFieldName2 = "VDATE"
            mFieldName3 = "BILLNo"
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
                If mAccountCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND CREDITACCOUNTCODE='" & mAccountCode & "'"
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
                If mAccountCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND DEBITACCOUNTCODE='" & mAccountCode & "'"
                End If
            End If
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
            If MainClass.SearchGridMaster((txtPurVNo.Text), mTableName, mFieldName1, mFieldName2, mFieldName3, , SqlStr) = True Then
                txtPurVNo.Text = AcName
                txtPurVDate.Text = AcName1
                'TxtName_Validate False
                If txtPurVNo.Enabled = True Then txtPurVNo.Focus()
            End If
        Else
            SearchBillNo()
            Exit Sub
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FrmDrCrNoteGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
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
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim xIName As String
        Dim SqlStr As String

        Dim mTableName As String
        Dim mFieldName1 As String
        Dim mFieldName2 As String
        Dim mFieldName3 As String
        Dim mAccountCode As String
        Dim mDivisionCode As Long

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(cboDivision.Text) = "" Then
            MsgInformation("Please select Division First")
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Sub
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        '    If OptDCType(2).Value = True Then
        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                ''If MainClass.SearchMaster(.Text, "INV_ITEM_MST", "ITEMCODE", sqlstr) = True Then
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "HSN_CODE", "CUSTOMER_PART_NO", SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                xIName = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "HSN_CODE", "CUSTOMER_PART_NO", SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = AcName
                End If
                .Row = .ActiveRow
                .Col = ColItemDesc
                MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr)
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = MasterNo
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        'If eventArgs.row = 0 And eventArgs.col = ColBillNo Then
        '    With SprdMain


        '        If VB.Left(cboPopulateFrom.Text, 1) = "P" Then

        '            mTableName = "FIN_PURCHASE_HDR"
        '            mFieldName1 = "VNO"
        '            mFieldName2 = "TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE"
        '            mFieldName3 = "BILLNo"

        '            SqlStr = " SELECT IH.VNO, IH.VDATE, CMST.SUPP_CUST_NAME, BILLNo, INVOICE_DATE" & vbCrLf _
        '                & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
        '                & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
        '                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        '                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '                & " AND FYEAR IN (" & RsCompany.Fields("FYEAR").Value & "," & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf _
        '                & " AND ISFINALPOST='Y' AND VNO<>'-1'"

        '            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
        '                If Trim(txtDebitAccount.Text) <> "" Then
        '                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                        mAccountCode = MasterNo
        '                    End If
        '                End If
        '            Else
        '                If Trim(txtCreditAccount.Text) <> "" Then
        '                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '                        mAccountCode = MasterNo
        '                    End If
        '                End If
        '            End If
        '            If mAccountCode <> "" Then
        '                SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & mAccountCode & "'"
        '            End If
        '            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""
        '            'If MainClass.SearchGridMaster((txtPurVNo.Text), mTableName, mFieldName1, mFieldName2, mFieldName3, , SqlStr) = True Then
        '            If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
        '                .Row = .ActiveRow
        '                .Col = ColBillNo
        '                .Text = AcName

        '                .Col = ColBillDate
        '                .Text = AcName1
        '                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBillNo)
        '            End If
        '        End If
        '    End With
        'End If


        '    End If
        If eventArgs.col = 0 And eventArgs.row > 0 Then '***ROW DEL. OPTION NOT REQ IN INVOICE
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If
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
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If FillGridRow(xICode) = False Then Exit Sub
                    FormatSprdMain(eventArgs.row)
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColQty)
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If
            Case ColQty
                If CheckQty() = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColRate
                Call CheckRate()
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function FillGridRow(ByRef mItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim RsPurMisc As ADODB.Recordset

        Dim SqlStr As String
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mLocal As String = ""
        Dim mAccountName As String
        Dim mSuppCustCode As String
        Dim mRefType As String
        Dim mRGPNo As Double
        Dim mPVNo As String
        Dim mPVNoMKey As String

        If mItemCode = "" Then Exit Function

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        mLocal = "N"
        mPartyGSTNo = ""
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mAccountName = Trim(txtDebitAccount.Text)
        Else
            mAccountName = Trim(txtCreditAccount.Text)
        End If
        If Trim(mAccountName) <> "" Then
            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCustCode = Trim(MasterNo)
            End If
        End If

        mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        If Val(txtMRRNo.Text) > 0 Then
            If MainClass.ValidateWithMasterTable(Val(txtMRRNo.Text), "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mRefType = Trim(MasterNo)
            End If
        End If

        SqlStr = ""
        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,CUSTOMER_PART_NO,PURCHASE_UOM, HSN_CODE" & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(mItemCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc
                SprdMain.Col = ColPartNo
                SprdMain.Text = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

                SprdMain.Col = ColHSNCode
                'If mRefType = "R" Then
                '    SprdMain.Col = ColPONo
                '    mRGPNo = SprdMain.Text

                '    If mRGPNo <= 0 Then
                '        If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "REF_PO_NO", "INV_GATE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & Trim(mItemCode) & "'") = True Then
                '            mRGPNo = CDbl(Trim(MasterNo))
                '        End If
                '    End If
                '    If MainClass.ValidateWithMasterTable(mRGPNo, "AUTO_KEY_PASSNO", "SAC_CODE", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '        mHSNCode = CDbl(Trim(MasterNo))
                '    End If
                '    If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                '        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ERR1
                '    Else
                '        pCGSTPer = 0
                '        pSGSTPer = 0
                '        pIGSTPer = 0
                '    End If

                'Else
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColPurNO
                mPVNo = Trim(SprdMain.Text)
                mPVNoMKey = ""
                mHSNCode = ""
                If mPVNo <> "" Then
                    If MainClass.ValidateWithMasterTable(mPVNo, "VNO", "MKEY", "FIN_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPVNoMKey = CDbl(Trim(MasterNo))
                    End If

                    'If MainClass.ValidateWithMasterTable(mPVNoMKey, "MKEY", "HSNCODE", "FIN_PURCHASE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & Trim(mItemCode) & "'") = True Then
                    '    mHSNCode = CDbl(Trim(MasterNo))
                    'End If

                    SqlStr = ""

                    SqlStr = " Select * " & vbCrLf _
                        & " FROM FIN_PURCHASE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND MKEY='" & mPVNoMKey & "' AND ITEM_CODE='" & Trim(mItemCode) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurMisc, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsPurMisc.EOF = False Then
                        SprdMain.Col = ColHSNCode
                        SprdMain.Text = IIf(IsDBNull(RsPurMisc.Fields("HSNCODE").Value), "", RsPurMisc.Fields("HSNCODE").Value)


                        pCGSTPer = CStr(Val(IIf(IsDBNull(RsPurMisc.Fields("CGST_PER").Value), 0, RsPurMisc.Fields("CGST_PER").Value)))
                        pSGSTPer = CStr(Val(IIf(IsDBNull(RsPurMisc.Fields("SGST_PER").Value), 0, RsPurMisc.Fields("SGST_PER").Value)))
                        pIGSTPer = CStr(Val(IIf(IsDBNull(RsPurMisc.Fields("IGST_PER").Value), 0, RsPurMisc.Fields("IGST_PER").Value)))
                    End If
                Else
                    SprdMain.Col = ColHSNCode
                    If mHSNCode = "" Then
                        SprdMain.Text = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)
                        mHSNCode = Trim(SprdMain.Text)
                    Else
                        SprdMain.Text = mHSNCode
                    End If

                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, Mid(cboGSTStatus.Text, 1, 1), mPartyGSTNo) = False Then GoTo ERR1
                    Else
                        pCGSTPer = 0
                        pSGSTPer = 0
                        pIGSTPer = 0
                    End If
                End If


                'End If

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")


            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If
        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub CheckRate()
        On Error GoTo ERR1
        Exit Sub 'without rate can't be saved
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
        CheckQty = True
        Exit Function
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
    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    With SprdView
    '        .Row = eventArgs.row
    '        .Col = 1
    '        SprdView.Col = 2
    '        txtVType.Text = SprdView.Text
    '        .Col = 3
    '        txtVNoPrefix.Text = .Text
    '        .Col = 4
    '        txtVNo.Text = VB6.Format(.Text, "00000")
    '        .Col = 5
    '        txtVNoSuffix.Text = .Text
    '        .Col = 7
    '        txtVDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
    '        txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
    '        CmdView_Click(CmdView, New System.EventArgs())
    '    End With
    'End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mVType As String = ""
        Dim VNoPrefix As String = ""
        Dim mVNo As String = ""
        Dim mVNoSuffix As String = ""
        Dim mVDate As String = ""
        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn


        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)

        mVType = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))
        VNoPrefix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(2))
        mVNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(3))       ''ultrow.SetCellValue(m_udtColumns.EntryNo, dtRow.Item("EntryNo"))
        mVNoSuffix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(4))

        mVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(6))



        txtVType.Text = mVType
        txtVNoPrefix.Text = VNoPrefix
        txtVNo.Text = VB6.Format(mVNo, "00000")
        txtVNoSuffix.Text = mVNoSuffix
        txtVDate.Text = VB6.Format(mVDate, "DD/MM/YYYY")

        txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())



    End Sub
    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.DoubleClick
        cmdBillNoSearch_Click(cmdBillNoSearch, New System.EventArgs())
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
    Private Sub txtDebitAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDebitAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDebitAccount_DoubleClick(txtDebitAccount, New System.EventArgs())
    End Sub
    Private Sub txtDebitAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDebitAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mAccountName As String
        Dim mLocal As String
        Dim mAcctCode As String
        If Trim(txtDebitAccount.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            Cancel = True
            ErrorMsg("Please Enter the Valid Debit Account.", "", MsgBoxStyle.Critical)
        End If
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mAccountName = Trim(txtDebitAccount.Text)
        Else
            mAccountName = Trim(txtCreditAccount.Text)
        End If
        If Trim(mAccountName) <> "" Then
            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAcctCode = Trim(MasterNo)
            End If

            mLocal = GetPartyBusinessDetail(Trim(mAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            mLocal = IIf(MasterNo = "Y", "L", "C")
            'If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim xMKey As String
        Dim mVNo As String
        Dim mAccountCode As String
        Dim mMRRNO As Double
        Dim mDivisionCode As Double
        Dim mDivisionName As String
        Dim mPopulateFrom As String
        If Val(txtMRRNo.Text) = 0 Then GoTo EventExitSub
        '    If Trim(cboPopulateFrom.Text) = "" Then Exit Sub
        If Trim(cboDivision.Text) <> "" Then
            If VB.Left(cboPopulateFrom.Text, 1) = "R" Then
                If SprdMain.MaxRows = 1 Then
                    cboDivision.SelectedIndex = -1
                End If
            End If
        End If
        If Trim(cboDivision.Text) = "" Then
            If VB.Left(cboPopulateFrom.Text, 1) = "R" Or VB.Left(cboPopulateFrom.Text, 1) = "M" Or VB.Left(cboPopulateFrom.Text, 1) = "S" Then
                If MainClass.ValidateWithMasterTable(Val(txtMRRNo.Text), "AUTO_KEY_MRR", "DIV_CODE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = CDbl(Trim(MasterNo))
                    If MainClass.ValidateWithMasterTable(Trim(CStr(mDivisionCode)), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionName = Trim(MasterNo)
                        cboDivision.Text = mDivisionName
                    End If
                End If
            End If
            If Trim(cboDivision.Text) = "" Then
                MsgBox("Division Name is Blank", MsgBoxStyle.Information)
                If cboDivision.Enabled = True Then cboDivision.Focus()
                GoTo EventExitSub
            End If
        End If
        mDivisionName = Trim(cboDivision.Text)
        mPopulateFrom = Trim(cboPopulateFrom.Text)
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        mMRRNO = Val(txtMRRNo.Text)
        '    txtMRRNo.Text = ""
        If DuplicateDatainGrid(Str(mMRRNO)) = True Then
            MsgBox("Duplicate MRR No.")
            GoTo EventExitSub
        End If
        SqlStr = " SELECT INV_GATE_HDR.*, INV_GATE_DET.REF_PO_NO FROM INV_GATE_HDR, INV_GATE_DET " & vbCrLf & " WHERE " & vbCrLf & " INV_GATE_HDR.AUTO_KEY_MRR=INV_GATE_DET.AUTO_KEY_MRR"
        SqlStr = SqlStr & vbCrLf & " AND INV_GATE_HDR.Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND INV_GATE_HDR.AUTO_KEY_MRR=" & Val(CStr(mMRRNO)) & ""
        If VB.Left(cboPopulateFrom.Text, 1) = "M" Then
            SqlStr = SqlStr & vbCrLf & " AND RECEIVED_QTY>0 AND REJ_RTN_STATUS='N'"
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND RECEIVED_QTY>0"
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "R" Then
            If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "AUTO_KEY_MRR", "INV_REOFFER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_POSTED='Y'") = False Then
                MsgBox("Please Enter Valid MRR No.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            Else
                '            SqlStr = SqlStr & vbCrLf & " AND REJ_RTN_STATUS='N'"
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND INV_GATE_HDR.REF_TYPE='1'"
        End If
        SqlStr = SqlStr & vbCrLf & " AND INV_GATE_HDR.DIV_CODE=" & mDivisionCode & ""
        SqlStr = SqlStr & vbCrLf & " AND INV_GATE_DET.MRR_DATE<=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If mFillData = False Then
                Clear1()
            End If
            cboDivision.Text = mDivisionName
            cboPopulateFrom.Text = mPopulateFrom
            Call ShowFromMRR(RsTemp)
        Else
            MsgBox("Please Enter Valid MRR No.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub ShowFromMRR(ByRef mRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mPartyName As String
        Dim pSupplierCode As String
        Dim mISSTREFUND As String
        Dim mPurNo As String
        Dim mPurDate As String
        Dim mISGST As String
        Dim xMRRNo As String
        Dim mSqlStr As String
        Dim RsTempPur As ADODB.Recordset
        Dim xPoNo As String
        Dim RsTempPO As ADODB.Recordset
        Dim xMRRDate As String
        Dim mMRRType As String

        With mRsTemp
            If Not .EOF Then
                SqlStr = ""
                mSqlStr = ""
                xMRRNo = .Fields("AUTO_KEY_MRR").Value
                xMRRDate = .Fields("MRR_DATE").Value
                mMRRType = .Fields("REF_TYPE").Value

                mSqlStr = "SELECT VNO, VDATE, ISGSTAPPLICABLE, ACCOUNTCODE " & vbCrLf _
                    & " FROM FIN_PURCHASE_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_MRR=" & xMRRNo & ""

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPur, ADODB.LockTypeEnum.adLockReadOnly)
                mPurNo = ""
                mPurDate = ""
                mISGST = "G"
                If RsTempPur.EOF = False Then
                    mPurNo = IIf(IsDBNull(RsTempPur.Fields("VNO").Value), "", RsTempPur.Fields("VNO").Value)
                    mPurDate = VB6.Format(IIf(IsDBNull(RsTempPur.Fields("VDATE").Value), "", RsTempPur.Fields("VDATE").Value), "DD/MM/YYYY")
                    mISGST = IIf(IsDBNull(RsTempPur.Fields("ISGSTAPPLICABLE").Value), "N", RsTempPur.Fields("ISGSTAPPLICABLE").Value)
                    mAccountCode = IIf(IsDBNull(RsTempPur.Fields("ACCOUNTCODE").Value), "", RsTempPur.Fields("ACCOUNTCODE").Value)
                Else
                    xPoNo = .Fields("REF_PO_NO").Value
                    mSqlStr = " SELECT ISGSTAPPLICABLE" & vbCrLf & " FROM PUR_PURCHASE_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_PO=" & xPoNo & "" & vbCrLf & " AND IH.PO_STATUS='Y' " & vbCrLf & " AND IH.AMEND_NO = ( " & vbCrLf & " SELECT MAX(AMEND_NO) " & vbCrLf & " FROM PUR_PURCHASE_HDR PH, PUR_PURCHASE_DET PD" & vbCrLf & " WHERE PH.MKEY=PD.MKEY" & vbCrLf & " AND PH.COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf & " AND PH.AUTO_KEY_PO=IH.AUTO_KEY_PO" & vbCrLf & " AND PH.PO_STATUS=IH.PO_STATUS" & vbCrLf & " AND PD.PO_WEF_DATE <=TO_DATE('" & VB6.Format(xMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPO, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTempPO.EOF = False Then
                        mISGST = IIf(IsDBNull(RsTempPO.Fields("ISGSTAPPLICABLE").Value), "E", RsTempPO.Fields("ISGSTAPPLICABLE").Value)
                    End If
                    mAccountCode = "-1"
                End If
                If mISGST = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                    '            ElseIf mISGST = "R" Then
                    '                cboGSTStatus.ListIndex = 1
                    '            ElseIf mISGST = "E" Then
                    '                cboGSTStatus.ListIndex = 2
                    '            ElseIf mISGST = "N" Then
                    '                cboGSTStatus.ListIndex = 3
                    '            ElseIf mISGST = "I" Then
                    '                cboGSTStatus.ListIndex = 4
                    '            ElseIf mISGST = "C" Then
                    '                cboGSTStatus.ListIndex = 5
                ElseIf mISGST = "I" Then
                    cboGSTStatus.SelectedIndex = 2
                Else 'If mISGST = "W" Then
                    cboGSTStatus.SelectedIndex = 1 '6
                End If
                cboGSTStatus.Enabled = True ' False
                '            LblMKey.text = ""
                txtPurVNo.Text = mPurNo
                txtPurVDate.Text = mPurDate
                txtMRRNo.Text = .Fields("AUTO_KEY_MRR").Value
                txtMRRDate.Text = .Fields("MRR_DATE").Value
                txtBillNo.Text = .Fields("BILL_NO").Value
                txtBillDate.Text = .Fields("BILL_DATE").Value
                '            txtPONo.Text = IIf(IsNull(.Fields("REF_AUTO_KEY_NO").Value), "", .Fields("REF_AUTO_KEY_NO").Value)
                '            txtPODate.Text = IIf(IsNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)
                lblPayDate.Text = txtMRRDate.Text
                If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPartyName = MasterNo
                    pSupplierCode = .Fields("SUPP_CUST_CODE").Value
                End If
                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = MasterNo
                End If
                If mFillData = False Then
                    If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                        txtDebitAccount.Text = mPartyName
                        txtCreditAccount.Text = mAccountName
                        '                txtCreditAccount.Enabled = True
                        '                txtDebitAccount.Enabled = False
                    Else
                        txtCreditAccount.Text = mPartyName
                        txtDebitAccount.Text = mAccountName
                        '                txtDebitAccount.Enabled = True
                        '                txtCreditAccount.Enabled = False
                    End If
                    '                If MainClass.ValidateWithMasterTable(.Fields("AUTO_KEY_MRR").Value, "AUTO_KEY_MRR", "ISSTREFUND", "FIN_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '                    mISSTREFUND = Trim(MasterNo)
                    '                End If
                    '                chkSTRefund.Value = IIf(mISSTREFUND = "Y", vbChecked, vbUnchecked)
                End If

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                lblTotQty.Text = "0.00" 'Format(IIf(IsNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                Call ShowMRRDetail1(pSupplierCode, mMRRType, mISGST, mPurNo, mPurDate, (.Fields("AUTO_KEY_MRR").Value), (.Fields("MRR_DATE").Value), IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value), VB6.Format(IIf(IsDBNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY"))
                cmdMRRNoSearch.Enabled = False
                txtMRRNo.Enabled = False
                cboPopulateFrom.Enabled = False
                '            Call ShowPurExp1(.Fields("AUTO_KEY_MRR").Value)
                mFillData = True
            End If
        End With
        Call CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowMRRDetail1(ByRef pSupplierCode As String, ByRef mMRRType As String, ByRef mISGST As String, ByRef mPurNo As String, ByRef mPurDate As String, ByRef mMRRNO As Double, ByRef mMRRDate As String, ByRef mBillNo As String, ByRef mBillDate As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset
        Dim mQty As Double
        Dim mRate As Double
        Dim mPRate As Double
        Dim mFactor As Double
        Dim mItemCode As String
        Dim mPONo As String
        Dim mItemUOM As String
        Dim mPORate As Double
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mHSNCode As String
        Dim mRGPNo As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mPVNoMKey As String
        Dim pItemCode As String
        Dim pSNO As Long

        SqlStr = " SELECT INV_GATE_DET.*,INV_ITEM_MST.ITEM_SHORT_DESC, DECODE(ISSUE_UOM,ITEM_UOM,1,UOM_FACTOR) AS UOM_FACTOR,ISSUE_UOM,"

        SqlStr = SqlStr & vbCrLf _
            & " GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),INV_GATE_DET.REF_PO_NO, INV_GATE_DET.ITEM_CODE) AS PORATE "

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_GATE_DET, INV_ITEM_MST" & vbCrLf _
            & " Where INV_GATE_DET.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf _
            & " AND INV_GATE_DET.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INV_GATE_DET.ITEM_CODE=INV_ITEM_MST.ITEM_CODE" & vbCrLf & " AND AUTO_KEY_MRR=" & mMRRNO & ""

        If VB.Left(cboPopulateFrom.Text, 1) = "M" Then
            SqlStr = SqlStr & vbCrLf & " AND REJECTED_QTY>0  "
        End If
        SqlStr = SqlStr & vbCrLf & " Order By SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            If mFillData = False Then
                I = 1
            Else
                I = SprdMain.MaxRows
            End If
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I
                mPONo = IIf(IsDBNull(.Fields("REF_PO_NO").Value), "", .Fields("REF_PO_NO").Value)

                If VB.Left(cboPopulateFrom.Text, 1) = "R" Then
                    pItemCode = .Fields("ITEM_CODE").Value
                    If ValidateReoffer(mMRRNO, pItemCode) = False Then GoTo NextRow
                End If

                SprdMain.Col = ColPurNO
                SprdMain.Text = mPurNo
                SprdMain.Col = ColPurDATE
                SprdMain.Text = VB6.Format(mPurDate, "DD/MM/YYYY")
                SprdMain.Col = ColMRRNo
                SprdMain.Text = Str(mMRRNO)
                SprdMain.Col = ColMRRDate
                SprdMain.Text = VB6.Format(mMRRDate, "DD/MM/YYYY")
                SprdMain.Col = ColBillNo
                SprdMain.Text = Trim(mBillNo)
                SprdMain.Col = ColBillDate
                SprdMain.Text = VB6.Format(mBillDate, "DD/MM/YYYY")
                SprdMain.Col = ColRefNo
                SprdMain.Text = Trim(mPurNo)
                SprdMain.Col = ColRefDate
                SprdMain.Text = VB6.Format(mPurDate, "DD/MM/YYYY")
                mFactor = Val(IIf(IsDBNull(.Fields("UOM_FACTOR").Value), "", .Fields("UOM_FACTOR").Value))
                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                mLocal = GetPartyBusinessDetail(Trim(pSupplierCode), Trim(txtBillTo.Text), "WITHIN_STATE")
                mPartyGSTNo = GetPartyBusinessDetail(Trim(pSupplierCode), Trim(txtBillTo.Text), "GST_RGN_NO")

                If mMRRType = "R" Then
                    mRGPNo = IIf(IsDBNull(.Fields("REF_PO_NO").Value), -1, .Fields("REF_PO_NO").Value)
                    If MainClass.ValidateWithMasterTable(mRGPNo, "AUTO_KEY_PASSNO", "SAC_CODE", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mHSNCode = CDbl(Trim(MasterNo))
                    End If
                    If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ERR1
                Else
                    mPVNoMKey = ""
                    mHSNCode = ""
                    If mPurNo <> "" Then
                        If MainClass.ValidateWithMasterTable(mPurNo, "VNO", "MKEY", "FIN_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mPVNoMKey = CDbl(Trim(MasterNo))
                        End If

                        If MainClass.ValidateWithMasterTable(mPVNoMKey, "MKEY", "HSNCODE", "FIN_PURCHASE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & Trim(mItemCode) & "'") = True Then
                            mHSNCode = CDbl(Trim(MasterNo))
                        End If
                    End If

                    If mHSNCode = "" Then
                        mHSNCode = GetHSNCode(mItemCode)

                        SprdMain.Col = ColHSNCode
                        SprdMain.Text = mHSNCode
                        If GetPODetails(mPONo, mMRRDate, mItemCode, pCGSTPer, pSGSTPer, pIGSTPer) = False Then GoTo ERR1
                    Else
                        SprdMain.Col = ColHSNCode
                        SprdMain.Text = mHSNCode
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, Mid(cboGSTStatus.Text, 1, 1), mPartyGSTNo) = False Then GoTo ERR1
                    End If
                End If

                'SprdMain.Col = ColHSNCode
                'SprdMain.Text = mHSNCode

                SprdMain.Col = ColBillQty
                SprdMain.Text = CDbl(VB6.Format(Val(IIf(IsDBNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)), "0.000"))

                SprdMain.Col = ColQty
                If VB.Left(cboPopulateFrom.Text, 1) = "J" Then
                    SprdMain.Text = CStr(CDbl(VB6.Format(IIf(IsDBNull(.Fields("APPROVED_QTY").Value), 0, .Fields("APPROVED_QTY").Value), "0.000")) * mFactor)
                Else
                    SprdMain.Text = CStr(CDbl(VB6.Format(IIf(IsDBNull(.Fields("REJECTED_QTY").Value), 0, .Fields("REJECTED_QTY").Value), "0.000")) * mFactor)
                End If
                mQty = Val(SprdMain.Text)
                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                mItemUOM = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)



                SprdMain.Col = ColPORate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PORATE").Value), "", .Fields("PORATE").Value)) / mFactor) ''Val(IIf(IsNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value))

                pSNO = Val(IIf(IsDBNull(.Fields("SERIAL_NO").Value), 0, .Fields("SERIAL_NO").Value))

                mPRate = GetPurchaseRate(mMRRNO, mItemCode, pSNO) / mFactor



                SprdMain.Col = ColBillRate
                SprdMain.Text = VB6.Format(mPRate, "0.0000") ''Format(IIf(IsNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value), "0.00")



                SprdMain.Col = ColRate
                SprdMain.Text = VB6.Format(mPRate, "0.0000") ''Format(IIf(IsNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value), "0.00")
                mRate = Val(SprdMain.Text)
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(mQty * mRate)
                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")
                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")
                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")
                SprdMain.Col = ColPONo
                SprdMain.Text = mPONo
                I = I + 1
                SprdMain.MaxRows = I
NextRow:
                .MoveNext()
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Function GetPODetails(ByRef mPONo As String, ByRef xMRRDate As String, ByRef mItemCode As String, ByRef pCGSTPer As Double, ByRef pSGSTPer As Double, ByRef pIGSTPer As Double) As Boolean
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        'Dim SqlStr As String
        SqlStr = ""
        GetPODetails = True
        pCGSTPer = 0
        pSGSTPer = 0
        pIGSTPer = 0
        SqlStr = "SELECT ITEM_DIS_PER, CGST_PER, SGST_PER, IGST_PER" & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH,  PUR_PURCHASE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.AUTO_KEY_PO=" & mPONo & "" & vbCrLf _
            & " AND IH.PO_STATUS='Y' " & vbCrLf _
            & " AND ID.ITEM_CODE='" & mItemCode & "' " & vbCrLf _
            & " AND IH.AMEND_NO = ( " & vbCrLf & " SELECT MAX(AMEND_NO) " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR PH " & vbCrLf _
            & " WHERE PH.COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf _
            & " AND PH.AUTO_KEY_PO=IH.AUTO_KEY_PO" & vbCrLf _
            & " AND PH.PO_STATUS=IH.PO_STATUS" & vbCrLf _
            & " AND PH.AMEND_WEF_DATE <=TO_DATE('" & VB6.Format(xMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = True Then Exit Function
            pCGSTPer = IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)
            pSGSTPer = IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)
            pIGSTPer = IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)
        End With
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPODetails = False
        ''Resume
    End Function
    Private Function GetPurchaseRate(ByRef mMRRNO As Double, ByRef mItemCode As String, ByRef mRowNo As Double) As Double
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        SqlStr = ""
        GetPurchaseRate = 0
        SqlStr = " SELECT ID.ITEM_RATE" & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID" & vbCrLf _
            & " Where IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=" & mMRRNO & " AND ISFINALPOST='Y' AND CANCELLED='N'"
        If mRowNo <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND SUBROWNO=" & mRowNo & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = True Then Exit Function
            GetPurchaseRate = IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)
        End With
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function ValidateReoffer(ByRef mMRRNO As Double, ByRef mItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset
        Dim mQty As Double
        Dim mRate As Double
        Dim mCNCreated As String
        SqlStr = ""
        SqlStr = " SELECT ITEM_CODE, MRR_FINAL_FLAG  FROM INV_REOFFER_HDR IH, INV_REOFFER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND AUTO_KEY_MRR=" & mMRRNO & ""
        SqlStr = SqlStr & vbCrLf & " AND LOT_ACC_RWK>0  "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCNCreated = IIf(IsDBNull(RsTemp.Fields("MRR_FINAL_FLAG").Value), "N", RsTemp.Fields("MRR_FINAL_FLAG").Value)
            If mCNCreated = "Y" Then
                If MsgQuestion("Already Credit Note Generated. Are you want to continue..") = CStr(MsgBoxResult.No) Then
                    ValidateReoffer = False
                    Exit Function
                End If
            End If
            ValidateReoffer = True
        Else
            ValidateReoffer = False
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ValidateReoffer = False
        ''Resume
    End Function
    Private Sub txtPartyDNDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyDNDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPartyDNNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyDNNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPartyDNNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyDNNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyDNNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPurVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurVDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPurVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurVNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPurVNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurVNo.DoubleClick
        cmdVNoSearch_Click(cmdVNoSearch, New System.EventArgs())
    End Sub
    Private Sub txtPurVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPurVNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPurVNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurVNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdVNoSearch_Click(cmdVNoSearch, New System.EventArgs())
    End Sub
    Private Sub txtPurVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim xMKey As String
        Dim mVNo As String
        Dim mAccountCode As String
        Dim mRefNo As String
        Dim mRefDate As String
        Dim mRefType As String
        Dim mBillNo As String
        Dim mBillType As String
        Dim mBillDate As String
        Dim mDivisionCode As Double
        Dim mDivisionName As String
        Dim mPopulateFrom As String
        If Trim(txtPurVNo.Text) = "" Then GoTo EventExitSub
        If Trim(cboPopulateFrom.Text) = "" Then GoTo EventExitSub
        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            GoTo EventExitSub
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        mDivisionName = Trim(cboDivision.Text)
        mPopulateFrom = Trim(cboPopulateFrom.Text)
        mRefNo = Trim(txtPurVNo.Text)
        mRefDate = Trim(txtPurVDate.Text)
        txtPurVNo.Text = ""
        txtPurVDate.Text = ""
        mRefType = VB.Left(cboPopulateFrom.Text, 1)
        If mFillData = False Then
            Clear1()
        End If
        cboDivision.Text = mDivisionName
        cboPopulateFrom.Text = mPopulateFrom
        If DuplicateDatainGrid(mRefNo) = True Then
            MsgBox("Duplicate Ref No.")
            GoTo EventExitSub
        End If
        ''AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "
        If mRefType = "P" Then

            SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND VNo='" & MainClass.AllowSingleQuote(mRefNo) & "' " & vbCrLf _
                & " AND VDATE=TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND ISFINALPOST='Y' AND CANCELLED='N'"

            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
            End If
            If mAccountCode <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND SUPP_CUST_CODE='" & mAccountCode & "'"
            End If
            SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Call ShowFromPurchase(RsTemp)
            Else
                MsgBox("Please Enter Valid Purchase Voucher No.", MsgBoxStyle.Information)
                Cancel = True
                If txtPurVNo.Enabled Then txtPurVNo.Focus()
            End If
        ElseIf mRefType = "D" Or (VB.Left(cboPopulateFrom.Text, 1) = "O" And CDbl(LblBookCode.Text) = ConCreditNoteBookCode And lblDCType.Text = "R") Then
            SqlStr = " SELECT * FROM FIN_DNCN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND VNo='" & MainClass.AllowSingleQuote(mRefNo) & "'" & vbCrLf & " AND APPROVED='Y'" & vbCrLf & " AND CANCELLED='N' AND DNCNTYPE='" & lblDCType.Text & "'"
            ''FYEAR=" & RsCompany.Fields("FYEAR").Value & "
            If Trim(mRefDate) <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND VDATE=TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & "AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
            End If
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
                If mAccountCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & "AND CREDITACCOUNTCODE='" & mAccountCode & "'"
                End If
            Else
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    End If
                End If
                If mAccountCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & "AND DEBITACCOUNTCODE='" & mAccountCode & "'"
                End If
            End If
            SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Call ShowFromDNCN(RsTemp)
            Else
                MsgBox("Please Enter Valid DN/CN Voucher No.", MsgBoxStyle.Information)
                Cancel = True
                If txtPurVNo.Enabled Then txtPurVNo.Focus()
            End If
        ElseIf mRefType = "T" Then
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) = "" Then
                    MsgBox("Please Select Debit Account First.", MsgBoxStyle.Information)
                    Cancel = True
                Else
                    If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    Else
                        mAccountCode = "-1"
                        MsgBox("Debit Account Does Not Exist In Master", MsgBoxStyle.Information)
                        Cancel = True
                    End If
                End If
            Else
                If Trim(txtCreditAccount.Text) = "" Then
                    MsgBox("Please Select Credit Account First.", MsgBoxStyle.Information)
                    Cancel = True
                Else
                    If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = MasterNo
                    Else
                        mAccountCode = "-1"
                        MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
                        Cancel = True
                    End If
                End If
            End If
            SqlStr = " SELECT * FROM FIN_POSTED_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(mBillNo) & "' AND BillDate=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'" & vbCrLf & " AND BILLTYPE='B'"
            SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & mDivisionCode & ""
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                If ADDMode = True Then
                    Clear1()
                    cboDivision.Text = mDivisionName
                    Call ShowFromTRN(RsTemp)
                Else
                    '                txtBillNo.Text = Trim(mBillNo)
                    '                txtBillDate.Text = Trim(mBillDate)
                    '
                    '                txtPurVNo.Text = Trim(mRefNo)
                    '                txtPurVDate.Text = Trim(mRefDate)
                    '
                End If
            Else
                MsgBox("Please Enter Valid Bill No.", MsgBoxStyle.Information)
                '        Cancel = True
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRecdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecdDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
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
        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub
        txtVNo.Text = VB6.Format(Val(txtVNo.Text), "00000")
        If MODIFYMode = True And RsDNCNMain.EOF = False Then xMKey = RsDNCNMain.Fields("mKey").Value
        mVNo = Trim(txtVType.Text) & Trim(Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text) & Trim(txtVNoSuffix.Text))


        SqlStr = " SELECT * FROM FIN_DNCN_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND VNo='" & MainClass.AllowSingleQuote(mVNo) & "' " & vbCrLf _
            & " AND BookCode=" & Val(LblBookCode.Text) & " " & vbCrLf _
            & " --AND BookType='" & mBookType & "' "

        SqlStr = SqlStr & " AND DNCNTYPE = '" & lblDCType.Text & "'"
        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            SqlStr = SqlStr & vbCrLf & " AND DNCNSEQTYPE=" & Val(lblDNCNSeqType.Text) & ""
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNCNMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDNCNMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_DNCN_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMKey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNCNMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mVNo As String
        Dim mDebitAccountCode As String
        Dim mCreditAccountCode As String
        Dim mFREIGHTCHARGES As String
        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double
        Dim mTotEDAmount As Double
        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mCancelled As String
        Dim mBookCode As Integer
        Dim mSTPERCENT As Double
        Dim mTOTFREIGHT As Double
        Dim mEDPERCENT As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mTotDiscount As Double
        Dim mDnCnType As String
        Dim mModvatNo As Integer
        Dim mModvatDate As String
        Dim mModvatPer As Double
        Dim mModvatAmount As Double
        Dim mISMODVAT As String
        Dim mSTRefundNo As Integer
        Dim mSTRefundDate As String
        Dim mSTRefundPer As Double
        Dim mSTRefundAmount As Double
        Dim mISSTREFUND As String
        Dim mISCSTREFUND As String
        Dim pDueDate As String
        Dim mDNFROM As String
        Dim mApproved As String
        Dim pBillNo As String
        Dim pBillDate As String
        Dim xCreditDays1 As Object
        Dim xCreditDays2 As Integer
        Dim mDivisionCode As Double
        Dim mISGST As String
        Dim mCGSTRefundAmount As Double
        Dim mSGSTRefundAmount As Double
        Dim mIGSTRefundAmount As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim xAccountName As String
        Dim mDNCNIssue As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mBillNo = ""
        mBillDate = ""
        mDnCnType = lblDCType.Text
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            xAccountName = Trim(txtDebitAccount.Text)
        Else
            xAccountName = Trim(txtCreditAccount.Text)
        End If
        If MainClass.ValidateWithMasterTable(xAccountName, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDebitAccountCode = MasterNo
        Else
            mDebitAccountCode = CStr(-1)
            MsgBox("Debit Account Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCreditAccountCode = MasterNo
        Else
            mCreditAccountCode = CStr(-1)
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If
        mBookCode = CInt(LblBookCode.Text)
        mItemValue = Val(lblTotItemValue.Text)
        mTOTSTAMT = 0 ' Val(lblTotST.text)
        mTOTCHARGES = Val(lblTotCharges.Text)
        mTotEDAmount = 0 '  Val(lblTotED.text)
        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)
        mSTPERCENT = Val(lblSTPercentage.Text)
        mTOTFREIGHT = Val(lblTotFreight.Text)
        mEDPERCENT = Val(lblEDPercentage.Text)
        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)
        mRO = Val(lblRO.Text)
        mTotDiscount = Val(lblDiscount.Text)
        mSURAmount = Val(lblSurcharge.Text)
        mMSC = Val(lblMSC.Text)
        mTotQty = Val(lblTotQty.Text)
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mApproved = IIf(chkAproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If Trim(txtVNo.Text) = "" Then
            mVNoSeq = CDbl(AutoGenSeqVNo("VNOSEQ", CInt(LblBookCode.Text)))
        Else
            mVNoSeq = Val(txtVNo.Text)
        End If
        txtVNo.Text = IIf(mVNoSeq = -1, "", VB6.Format(Val(CStr(mVNoSeq)), "00000"))
        '    If CheckValidVDate(mVNoSeq) = False Then GoTo ErrPart:
        mModvatNo = CInt("-1")
        mModvatDate = ""
        mModvatPer = 0
        mModvatAmount = 0
        mISMODVAT = "N"
        If VB.Left(cboGSTStatus.Text, 1) = "G" Then
            mCGSTRefundAmount = Val(txtCGSTRefundAmount.Text)
            mSGSTRefundAmount = Val(txtSGSTRefundAmount.Text)
            mIGSTRefundAmount = Val(txtIGSTRefundAmount.Text)
        Else
            mCGSTRefundAmount = 0
            mSGSTRefundAmount = 0
            mIGSTRefundAmount = 0
        End If
        mISGST = VB.Left(cboGSTStatus.Text, 1)
        mSTRefundNo = CInt("-1")
        mSTRefundDate = ""
        mSTRefundPer = 0
        mSTRefundAmount = 0
        mISSTREFUND = "N"
        mSTRefundNo = CInt("-1")
        mSTRefundDate = ""
        mSTRefundPer = 0
        mSTRefundAmount = 0
        mISCSTREFUND = "N"
        mVNo = Trim(txtVType.Text) & Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "00000") & Trim(txtVNoSuffix.Text))
        mDNFROM = VB.Left(cboPopulateFrom.Text, 1)
        If VB.Left(cboPopulateFrom.Text, 1) = "P" Then
            mBillNo = ""
            mBillDate = ""
            If Trim(txtPurVNo.Text) <> "" Then
                If GetBillNo(pBillNo, pBillDate, mDNFROM) = True Then
                    mBillNo = Trim(pBillNo)
                    mBillDate = Trim(pBillDate)
                End If
            End If
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "D" Then
            mBillNo = ""
            mBillDate = ""
            If Trim(txtPurVNo.Text) <> "" Then
                If GetBillNo(pBillNo, pBillDate, mDNFROM) = True Then
                    mBillNo = Trim(pBillNo)
                    mBillDate = Trim(pBillDate)
                End If
            End If
        Else
            mBillNo = Trim(txtBillNo.Text)
            mBillDate = VB6.Format(Trim(txtBillDate.Text), "DD/MM/YYYY")
        End If
        SqlStr = ""
        'If lblDCType.Text = "S" And chkAproved.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    txtPartyDNNo.Text = "" ' IIf(Trim(txtPartyDNNo.Text) = "", mVNo, txtPartyDNNo.Text)
        '    txtPartyDNDate.Text = "" ' IIf(Trim(txtPartyDNDate.Text) = "", txtVDate.Text, txtPartyDNDate.Text)
        '    txtRecdDate.Text = "" ' IIf(Trim(txtRecdDate.Text) = "", txtVDate.Text, txtRecdDate.Text)
        '    mDNCNIssue = "Y"
        'Else
        If chkAproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            mDNCNIssue = IIf(Trim(txtRecdDate.Text) = "", "N", "Y")
        Else
            mDNCNIssue = "N"
        End If
        'End If

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_DNCN_HDR", (LblMKey.Text), RsDNCNMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_DNCN_DET", (LblMKey.Text), RsDNCNDetail, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_DNCN_EXP", (LblMKey.Text), RsDNCNExp, "MKEY", "M") = False Then GoTo ErrPart
        End If

        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_DNCN_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey
            SqlStr = "INSERT INTO FIN_DNCN_HDR( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, ROWNO, " & vbCrLf & " VNOPREFIX, VTYPE, VNOSEQ, VNOSUFFIX, " & vbCrLf & " VNO, VDATE, " & vbCrLf & " BILLNO,INVOICE_DATE, " & vbCrLf & " DEBITACCOUNTCODE, CREDITACCOUNTCODE, DUEDAYSFROM, DUEDAYSTO, " & vbCrLf & " BOOKCODE, BookType, BOOKSUBTYPE, REMARKS,  " & vbCrLf & " ITEMDESC, REASON, ITEMVALUE, STPERCENT,  " & vbCrLf & " TOTSTAMT, TOTFREIGHT, TOTCHARGES, EDPERCENT,  " & vbCrLf & " TOTEDAMOUNT, TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, " & vbCrLf & " TOTRO, TOTEXPAMT, TOTTAXABLEAMOUNT, NETVALUE, " & vbCrLf & " TOTQTY, CANCELLED, NARRATION, DNCNTYPE, APPROVED, " & vbCrLf & " MODVATNO, MODVATDATE, MODVATPER, MODVATAMOUNT, " & vbCrLf & " STCLAIMNO, STCLAIMDATE, STCLAIMPER, STCLAIMAMOUNT, " & vbCrLf & " ISMODVAT, ISSTREFUND,ISCSTREFUND, PAYDATE,DNCNFROM, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM,DIV_CODE, " & vbCrLf & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT, " & vbCrLf & " ISGSTREFUND, GST_NO, GST_DATE, " & vbCrLf & " CGST_REFUNDAMOUNT, SGST_REFUNDAMOUNT, IGST_REFUNDAMOUNT," & vbCrLf _
                & " PURVNO, PURVDATE, AUTO_KEY_MRR, MRRDATE, PARTY_DNCN_NO, PARTY_DNCN_DATE,PARTY_DNCN_RECDDATE,ISDNCN_ISSUE,DNCNSEQTYPE,BILL_TO_LOC_ID) "
            SqlStr = SqlStr & vbCrLf & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & mCurRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "', '" & MainClass.AllowSingleQuote(txtVType.Text) & "'," & vbCrLf & " " & mVNoSeq & ", '" & MainClass.AllowSingleQuote(txtVNoSuffix.Text) & "', '" & MainClass.AllowSingleQuote(mVNo) & "',TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(mBillNo) & "',TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " '" & mDebitAccountCode & "','" & mCreditAccountCode & "', " & Val(xCreditDays1) & ", " & Val(CStr(xCreditDays2)) & ", " & vbCrLf & " '" & mBookCode & "', '" & mBookType & "', '" & mBookSubType & "', ''," & vbCrLf & " '', '" & MainClass.AllowSingleQuote(txtReason.Text) & "', " & vbCrLf & " " & mItemValue & ", " & mSTPERCENT & ", " & mTOTSTAMT & ", " & mTOTFREIGHT & ", " & mTOTCHARGES & "," & vbCrLf & " " & mEDPERCENT & ", " & mTotEDAmount & ", " & mSURAmount & ", " & mTotDiscount & "," & mMSC & ", " & vbCrLf & " " & mRO & ", " & mTOTEXPAMT & ", " & mTOTTAXABLEAMOUNT & ", " & mNETVALUE & ", " & vbCrLf & " " & mTotQty & ", '" & mCancelled & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf & " '" & mDnCnType & "', '" & mApproved & "',  " & vbCrLf & " " & mModvatNo & ",TO_DATE('" & VB6.Format(mModvatDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mModvatPer & "," & mModvatAmount & "," & vbCrLf & " " & mSTRefundNo & ", TO_DATE('" & VB6.Format(mSTRefundDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mSTRefundPer & "," & mSTRefundAmount & "," & vbCrLf & " '" & mISMODVAT & "','" & mISSTREFUND & "','" & mISCSTREFUND & "', TO_DATE('" & VB6.Format(lblPayDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mDNFROM & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N'," & mDivisionCode & "," & vbCrLf & " " & Val(lblTotCGSTAmount.Text) & ", " & Val(lblTotSGSTAmount.Text) & ", " & Val(lblTotIGSTAmount.Text) & ", " & vbCrLf & " '" & mISGST & "', '', '', " & vbCrLf & " " & Val(CStr(mCGSTRefundAmount)) & ", " & Val(CStr(mSGSTRefundAmount)) & ", " & Val(CStr(mIGSTRefundAmount)) & "," & vbCrLf _
                & " '" & (txtPurVNo.Text) & "', TO_DATE('" & VB6.Format(txtPurVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & (txtMRRNo.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtPartyDNNo.Text) & "', TO_DATE('" & VB6.Format(txtPartyDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtRecdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mDNCNIssue & "'," & Val(lblDNCNSeqType.Text) & ",'" & MainClass.AllowSingleQuote(txtBillTo.Text) & "')"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_DNCN_HDR SET " & vbCrLf & " VNOPREFIX='" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "',  " & vbCrLf & " VNOSEQ=" & mVNoSeq & ", VTYPE='" & MainClass.AllowSingleQuote(txtVType.Text) & "', DNCNSEQTYPE=" & Val(lblDNCNSeqType.Text) & "," & vbCrLf & " VNOSUFFIX='" & MainClass.AllowSingleQuote(txtVNoSuffix.Text) & "', " & vbCrLf & " VNO='" & MainClass.AllowSingleQuote(mVNo) & "', " & vbCrLf & " VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " BILLNO='" & MainClass.AllowSingleQuote(mBillNo) & "',  " & vbCrLf & " INVOICE_DATE=TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "
            SqlStr = SqlStr & vbCrLf & " DEBITACCOUNTCODE='" & mDebitAccountCode & "', " & vbCrLf & " CREDITACCOUNTCODE='" & mCreditAccountCode & "', " & vbCrLf & " DUEDAYSFROM=" & Val(xCreditDays1) & ",  " & vbCrLf & " DUEDAYSTO=" & Val(CStr(xCreditDays2)) & ", " & vbCrLf & " BOOKCODE='" & mBookCode & "',   " & vbCrLf & " BookType='" & mBookType & "',   " & vbCrLf & " BOOKSUBTYPE='" & mBookSubType & "',  " & vbCrLf & " REMARKS='',  " & vbCrLf & " ITEMDESC='',  " & vbCrLf & " REASON='" & MainClass.AllowSingleQuote(txtReason.Text) & "',  " & vbCrLf & " ITEMVALUE=" & mItemValue & ",  " & vbCrLf & " STPERCENT=" & mSTPERCENT & ",  " & vbCrLf & " TOTSTAMT=" & mTOTSTAMT & ",  " & vbCrLf & " TOTFREIGHT=" & mTOTFREIGHT & ", " & vbCrLf & " TOTCHARGES=" & mTOTCHARGES & ", " & vbCrLf & " EDPERCENT=" & mEDPERCENT & ",  " & vbCrLf & " TOTEDAMOUNT=" & mTotEDAmount & ", " & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & mTotDiscount & "," & vbCrLf & " TOTMSCAMOUNT=" & mMSC & ", UPDATE_FROM='N',"
            SqlStr = SqlStr & vbCrLf & " MODVATNO=" & mModvatNo & ", " & vbCrLf & " MODVATDATE=TO_DATE('" & VB6.Format(mModvatDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODVATPER=" & mModvatPer & ", " & vbCrLf & " MODVATAMOUNT=" & mModvatAmount & ", " & vbCrLf & " STCLAIMNO=" & mSTRefundNo & ", " & vbCrLf & " STCLAIMPER=" & mSTRefundPer & ", " & vbCrLf & " STCLAIMAMOUNT=" & mSTRefundAmount & ", " & vbCrLf & " STCLAIMDATE=TO_DATE('" & VB6.Format(mSTRefundDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ISMODVAT='" & mISMODVAT & "', " & vbCrLf & " ISSTREFUND='" & mISSTREFUND & "', " & vbCrLf & " ISCSTREFUND='" & mISCSTREFUND & "', " & vbCrLf & " NETCGST_AMOUNT = " & Val(lblTotCGSTAmount.Text) & ", " & vbCrLf & " NETSGST_AMOUNT = " & Val(lblTotSGSTAmount.Text) & ", " & vbCrLf & " NETIGST_AMOUNT = " & Val(lblTotIGSTAmount.Text) & ", " & vbCrLf & " ISGSTREFUND = '" & mISGST & "',  " & vbCrLf & " GST_NO = '',  " & vbCrLf & " GST_DATE = '', " & vbCrLf & " CGST_REFUNDAMOUNT = " & Val(CStr(mCGSTRefundAmount)) & ", " & vbCrLf & " SGST_REFUNDAMOUNT = " & Val(CStr(mSGSTRefundAmount)) & ", " & vbCrLf & " IGST_REFUNDAMOUNT = " & Val(CStr(mIGSTRefundAmount)) & ", " & vbCrLf
            SqlStr = SqlStr & vbCrLf & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "', TOTRO=" & mRO & ", AUTO_KEY_MRR='" & (txtMRRNo.Text) & "', MRRDATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TOTEXPAMT=" & mTOTEXPAMT & ", " & vbCrLf & " TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & ", " & vbCrLf & " NETVALUE=" & mNETVALUE & ", " & vbCrLf & " TOTQTY=" & mTotQty & "," & vbCrLf & " CANCELLED='" & mCancelled & "', " & vbCrLf & " NARRATION='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf _
                & " PARTY_DNCN_NO='" & MainClass.AllowSingleQuote(txtPartyDNNo.Text) & "', " & vbCrLf _
                & " PARTY_DNCN_DATE=TO_DATE('" & VB6.Format(txtPartyDNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " PARTY_DNCN_RECDDATE=TO_DATE('" & VB6.Format(txtRecdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " DNCNTYPE='" & mDnCnType & "', ISDNCN_ISSUE='" & mDNCNIssue & "'," & vbCrLf & " APPROVED='" & mApproved & "', DNCNFROM='" & mDNFROM & "', PURVNO='" & (txtPurVNo).Text & "' , PURVDATE=TO_DATE('" & VB6.Format(txtPurVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf & " PAYDATE=TO_DATE('" & VB6.Format(lblPayDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & " " & vbCrLf _
                & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If
        PubDBCn.Execute(SqlStr)
        UpdateMain1 = True
        If UpdateDetail1((txtNarration.Text), mVNo, IIf(CDbl(LblBookCode.Text) = ConDebitNoteBookCode, mDebitAccountCode, mCreditAccountCode), IIf(CDbl(LblBookCode.Text) = ConDebitNoteBookCode, mCreditAccountCode, mDebitAccountCode), mDivisionCode) = False Then GoTo ErrPart

        pDueDate = lblPayDate.Text
        If chkAproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            If VB.Left(cboPopulateFrom.Text, 1) = "D" Then
                mDNCNIssue = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "N", "Y")
                SqlStr = "UPDATE FIN_DNCN_HDR SET ISDNCN_ISSUE='" & mDNCNIssue & "', " & vbCrLf _
                    & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND VNO='" & Trim(txtPurVNo.Text) & "'" & vbCrLf _
                    & " AND VDATE=TO_DATE('" & VB6.Format(txtPurVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                PubDBCn.Execute(SqlStr)
            End If

            If DNCNPostTRNGST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, Trim(txtVType.Text), mVNo, (txtVDate.Text), mBillNo, mBillDate, mDebitAccountCode, mCreditAccountCode, Val(CStr(mNETVALUE)), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, "", (txtNarration.Text), Val(lblTotExpAmt.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(VB.Left(cboGSTStatus.Text, 1) = "G", IIf(Trim(mCompanyGSTNo) = Trim(mPartyGSTNo), "N", "Y"), IIf(VB.Left(cboGSTStatus.Text, 1) = "I", "I", "N")), Val(txtCGSTRefundAmount.Text), Val(txtSGSTRefundAmount.Text), Val(txtIGSTRefundAmount.Text), mDnCnType, txtBillTo.Text) = False Then GoTo ErrPart
        End If
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsDNCNMain.Requery() ''.Refresh
        RsDNCNDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Voucher No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function
    Private Function CheckValidVDate(ByRef pBillNoSeq As Integer) As Object
        'On Error GoTo CheckERR
        'Dim SqlStr As String
        'Dim mRsCheck1 As ADODB.Recordset
        'Dim mRsCheck2 As ADODB.Recordset
        'Dim mBackBillDate As String
        'Dim mMaxInvStrfNo As Long
        '    CheckValidVDate = True
        '
        '    If txtBillNo.Text = "000001" Then Exit Function
        '
        '    SqlStr = "SELECT MAX(INVOICE_DATE)" & vbCrLf _
        ''        & " FROM FIN_INVOICE_HDR " & vbCrLf _
        ''        & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        ''        & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        ''        & " AND BOOKCode = " & Val(LblBookCode.text) & " " & vbCrLf _
        ''        & " AND BookType='" & mBookType & "' " & vbCrLf _
        ''        & " AND BookSubType='" & mBookSubType & "' " & vbCrLf _
        ''        & " AND BillNoSeq<" & Val(pBillNoSeq) & ""
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, mRsCheck2, adLockReadOnly
        '
        '    If mRsCheck2.EOF = False Then
        '        mBackBillDate = IIf(IsNull(mRsCheck2.Fields(0)), mBackBillDate, mRsCheck2.Fields(0))
        '    End If
        '
        '    SqlStr = "SELECT MIN(INVOICE_DATE)" _
        ''        & " FROM FIN_INVOICE_HDR " _
        ''        & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        ''        & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        ''        & " AND BOOKCode = " & Val(LblBookCode.text) & " " & vbCrLf _
        ''        & " AND BookType='" & mBookType & "' " & vbCrLf _
        ''        & " AND BookSubType='" & mBookSubType & "' " & vbCrLf _
        ''        & " AND BillNoSeq>" & Val(pBillNoSeq) & ""
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, mRsCheck1, adLockReadOnly
        '
        '    If mRsCheck1.EOF = False And Not IsNull(mRsCheck1.Fields(0)) And mRsCheck2.EOF = False And Not IsNull(mRsCheck2.Fields(0)) Then
        '        If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0)) Then
        '            MsgBox "Bill Date Is Greater Than The BillDate Of Next InvoiceNo.": CheckValidVDate = False
        '        ElseIf CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0)) Then
        '            MsgBox "Bill Date Is Less Than The BillDate Of Previous InvoiceNo.": CheckValidVDate = False
        '        End If
        '    ElseIf mRsCheck1.EOF = False And Not IsNull(mRsCheck1.Fields(0)) Then
        '        If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0)) Then
        '            MsgBox "Bill Date Is Greater Than The BillDate Of Next InvoiceNo.": CheckValidVDate = False
        '        End If
        '    ElseIf mRsCheck2.EOF = False And Not IsNull(mRsCheck2.Fields(0)) Then
        '        If CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0)) Then
        '            MsgBox "Bill Date Is Less Than The BillDate Of Previous InvoiceNo.": CheckValidVDate = False
        '        End If
        '    End If
        Exit Function
CheckERR:
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqVNo(ByRef mFieldName As String, ByRef pBookCode As Integer) As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsGen As ADODB.Recordset
        Dim mNewSeqBillNo As Integer
        Dim mStartingNo As Double
        Dim xFYear As Integer
        SqlStr = ""
        xFYear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))
        mStartingNo = 1
        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            mStartingNo = CDbl(xFYear & Val(lblDNCNSeqType.Text) & VB6.Format(mStartingNo, "00000"))
        End If
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_DNCN_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BookCode='" & pBookCode & "' AND VTYPE='" & txtVType.Text & "'"
        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            SqlStr = SqlStr & vbCrLf & " AND DNCNSEQTYPE = " & Val(lblDNCNSeqType.Text) & ""
        End If
        ''temp
        ''    SqlStr = SqlStr & "AND " & mFieldName & "<718"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGen
            If .EOF = False Then
                If IIf(IsDBNull(.Fields(0).Value), -1, .Fields(0).Value) = -1 Then
                    mNewSeqBillNo = mStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = mStartingNo
                End If
            Else
                mNewSeqBillNo = mStartingNo
            End If
        End With
        AutoGenSeqVNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDetail1(ByRef xNarration As String, ByRef pVNo As String, ByRef pSuppCustCode As String, ByRef mDebitAccountCode As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset
        Dim I As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double
        Dim mExicseableAmt As Double
        Dim mSTableAmt As Double
        Dim mMRRNO As Double
        Dim mMRRDate As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mPONo As String
        Dim mTotExicseableAmt As Double
        Dim mTotSTableAmt As Double
        Dim mPurMkey As String
        Dim mPurNo As String
        Dim mPurDate As String
        Dim mRefNo As String
        Dim mRefDate As String
        Dim mPORate As Double
        Dim mMrrRefType As String
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        mTotExicseableAmt = GetExicseAbleAmt()
        mTotSTableAmt = GetSTAbleAmt()
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & mBookType & "' AND BOOKCODE='" & LblBookCode.Text & "'")
        PubDBCn.Execute("Delete From FIN_DNCN_DET Where Mkey='" & LblMKey.Text & "'")
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColPURMkey
                mPurMkey = CStr(Val(Trim(.Text)))
                .Col = ColPurNO
                mPurNo = Trim(.Text)
                .Col = ColPurDATE
                mPurDate = VB6.Format(.Text, "DD-MMM-YYYY")
                .Col = ColMRRNo
                mMRRNO = Val(Trim(.Text))
                .Col = ColMRRDate
                mMRRDate = VB6.Format(.Text, "DD-MMM-YYYY")
                .Col = ColBillNo
                mBillNo = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = ColBillDate
                mBillDate = VB6.Format(.Text, "DD-MMM-YYYY")
                If Trim(mBillNo) = "" Then
                    mBillNo = txtBillNo.Text
                    mBillDate = txtBillDate.Text
                End If
                .Col = ColRefNo
                mRefNo = Trim(.Text)
                .Col = ColRefDate
                mRefDate = VB6.Format(.Text, "DD-MMM-YYYY")
                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                xNarration = xNarration & IIf(xNarration = "", "", ", ") & mItemDesc
                .Col = ColHSNCode
                mHSNCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColQty
                mQty = Val(.Text)
                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)
                .Col = ColPONo
                mPONo = MainClass.AllowSingleQuote(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                .Col = ColRate
                mRate = Val(.Text)
                .Col = ColAmount
                mAmount = Val(.Text)
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
                If mTotExicseableAmt = 0 Then
                    mExicseableAmt = CDbl(VB6.Format(0, "0.00"))
                Else
                    mExicseableAmt = 0 ' Format((Val(lblTotED.text) * mAmount) / mTotExicseableAmt, "0.00")
                End If
                If mTotSTableAmt = 0 Then
                    mSTableAmt = CDbl("0.00")
                Else
                    mSTableAmt = 0 ' Format((Val(lblTotST.text) * (mAmount + mExicseableAmt)) / mTotSTableAmt, "0.00")
                End If
                mMrrRefType = GetMrrRefNo(mMRRNO)
                SqlStr = ""
                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO FIN_DNCN_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , ITEM_DESC, HSNCODE, ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT, " & vbCrLf & " ITEM_ED, ITEM_ST, " & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf & " SUPP_REF_DATE,REF_PO_NO,COMPANY_CODE, " & vbCrLf & " PURMKEY, " & vbCrLf & " PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, " & vbCrLf & " PO_RATE, MRR_REF_TYPE, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT " & vbCrLf & " ) "
                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & " '" & mItemCode & "','" & mItemDesc & "','" & mHSNCode & "', " & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & vbCrLf & " " & mExicseableAmt & ", " & vbCrLf & " " & mSTableAmt & "," & vbCrLf & " " & mMRRNO & ",TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mBillNo & "',TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mPONo & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & mPurMkey & "'," & vbCrLf & " '" & mPurNo & "',TO_DATE('" & VB6.Format(mPurDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mRefNo & "',TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & mPORate & ", '" & mMrrRefType & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & " " & vbCrLf & " ) "
                    PubDBCn.Execute(SqlStr)
                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If chkAproved.CheckState = System.Windows.Forms.CheckState.Checked And VB.Left(cboGSTStatus.Text, 1) = "G" Then
                            If UpdateGSTTRN(PubDBCn, (LblMKey.Text), (LblBookCode.Text), mBookType, mBookSubType, pVNo, VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), mBillNo, VB6.Format(mBillDate, "DD-MMM-YYYY"), "", "", pSuppCustCode, mDebitAccountCode, "Y", pSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", (lblDCType.Text), "G", IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N"), IIf(CDbl(LblBookCode.Text) = ConDebitNoteBookCode, "D", "C"), VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), "N") = False Then GoTo UpdateDetail1
                        End If
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        UpdateDetail1 = UpdateExp1()
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function UpdateExp1() As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        PubDBCn.Execute("Delete From FIN_DNCN_EXP Where Mkey='" & LblMKey.Text & "'")
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
                    '                mExpAmount = mExpAmount * -1
                End If
                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                SqlStr = ""
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdateExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateExp1 = False
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mLockBookCode As Integer
        Dim mAccountName As String
        'Dim mLocal As String
        Dim mDivisionCode As Double
        Dim mMRRNO As Double
        Dim mDNCNQty As Double
        Dim mItemCode As String
        Dim mAccountCode As String
        Dim mVNo As String
        Dim mMRRQty As Double
        Dim mRejectedQty As Double
        Dim mReOfferQty As Double
        Dim mItemUOM As String
        Dim mBalDrCrQty As Double
        Dim mGSTRefund_MRR As String
        Dim mGSTRefund As String
        Dim mDespatchQty As Double
        Dim RsTemp As ADODB.Recordset
        'Dim mISMODVAT As String
        Dim pISGSTRegd As String
        FieldsVarification = True
        If ValidateBranchLocking((txtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mLockBookCode = CInt(ConLockDebitNote)
        Else
            mLockBookCode = CInt(ConLockCreditNote)
        End If
        If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVType.Text)) & Trim(txtVNo.Text), (txtVDate.Text), "") = False Then
            If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                FieldsVarification = False
                Exit Function
            End If
        End If
        If ValidateAccountLocking(PubDBCn, txtVDate.Text, (txtDebitAccount.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If

        If CDate(VB6.Format(txtVDate.Text, "DD/MM/YYYY")) > CDate(VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
            MsgInformation("Voucher Date is Greater Than Current Date. Cannot Save")
            txtVDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True And RsDNCNMain.EOF = True Then Exit Function
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mAccountName = Trim(txtDebitAccount.Text)
        Else
            mAccountName = Trim(txtCreditAccount.Text)
        End If
        If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = Trim(MasterNo)
        Else
            mAccountCode = ""
        End If
        '    If Trim(mAccountName) <> "" Then
        '        If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mLocal = IIf(MasterNo = "Y", "L", "C")
        '        Else
        '            mLocal = ""
        '        End If
        '    Else
        '        mLocal = ""
        '    End If
        '
        '    If mLocal = "L" And chkCSTRefund.Value = vbChecked Then
        '        MsgInformation "Please Check Refund"
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If mLocal = "C" And chkSTRefund.Value = vbChecked Then
        '        MsgInformation "Please Check Refund"
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        Else
            MsgBox("Invalid Division Name.", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If
        '    If CDate(txtBillDate.Text) < CDate(txtMRRDate.Text) Then
        '        MsgBox "Bill Date Can Not be Less Than DCDate."
        '        FieldsVarification = False
        '        txtBillDate.SetFocus
        '        Exit Function
        '    End If
        '    If ADDMode = True And Trim(txtPurVNo.Text) = "" Then
        '        If OptDCType(0).Value = True Then
        '            MsgBox "Purchase Voucher No Cannot Be Blank", vbInformation
        '        ElseIf OptDCType(1).Value = True Then
        '            MsgBox "Dn/Cn No Cannot Be Blank", vbInformation
        '        Else
        '            MsgBox "Bill No Cannot Be Blank", vbInformation
        '        End If
        '
        '        txtPurVNo.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '
        '    End If
        '
        '    If OptDCType(2).Value = True Then
        '        If Trim(txtBillNo.Text) = "" Then
        '            MsgBox "Bill No. Can Not be Blank."
        '            FieldsVarification = False
        '            If txtBillNo.Enabled = True Then txtBillNo.SetFocus
        '            Exit Function
        '        End If
        '
        '        If Not IsDate(txtBillDate.Text) Then
        '            MsgBox "Invalid Bill Date ."
        '            FieldsVarification = False
        '            If txtBillDate.Enabled = True Then txtBillDate.SetFocus
        '            Exit Function
        '        End If
        '
        '        If Trim(txtBillDate.Text) = "" Then
        '            MsgBox "Bill Date Can Not be Blank."
        '            FieldsVarification = False
        '            If txtBillDate.Enabled = True Then txtBillDate.SetFocus
        '            Exit Function
        '        End If
        '    End If
        If Trim(txtDebitAccount.Text) = "" Then
            MsgBox("Debit Account Cannot Be Blank", MsgBoxStyle.Information)
            txtDebitAccount.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Debit Account Does Not Exist In Master", MsgBoxStyle.Information)
            txtDebitAccount.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCreditAccount.Text) = "" Then
            MsgBox("Credit Account Cannot Be Blank", MsgBoxStyle.Information)
            txtCreditAccount.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
            txtCreditAccount.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCreditAccount.Text) = Trim(txtDebitAccount.Text) Then
            MsgBox("Debit Account Cann't be Same as Credit Account.", MsgBoxStyle.Information)
            txtDebitAccount.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtVType.Text) = "" Then
            MsgBox("Voucher Type Cannot Be Blank", MsgBoxStyle.Information)
            txtVType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        pISGSTRegd = "N"
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pISGSTRegd = MasterNo
            End If
        Else
            If MainClass.ValidateWithMasterTable(txtCreditAccount.Text, "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pISGSTRegd = MasterNo
            End If
        End If
        If cboGSTStatus.SelectedIndex = -1 Then
            MsgBox("Please select GST Status", MsgBoxStyle.Information)
            If cboGSTStatus.Enabled = True Then cboGSTStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If
        'If CDbl(LblBookCode.Text) = ConCreditNoteBookCode And lblDCType.Text = "P" And VB.Left(cboPopulateFrom.Text, 1) = "P" Then
        '    MsgBox("Please select agt Debit Note, Credit Note cann't made agt Purchase. ", MsgBoxStyle.Information)
        '    If cboPopulateFrom.Enabled = True Then cboPopulateFrom.Focus()
        '    FieldsVarification = False
        '    Exit Function
        'End If
        If VB.Left(cboGSTStatus.Text, 1) = "W" Or VB.Left(cboGSTStatus.Text, 1) = "I" Or VB.Left(cboGSTStatus.Text, 1) = "N" Then
        Else
            If pISGSTRegd = "Y" And VB.Left(cboGSTStatus.Text, 1) <> "G" Then
                MsgBox("Supplier is registered, please select the GST Refund.", MsgBoxStyle.Information)
                ' txtSupplier.SetFocus
                FieldsVarification = False
                Exit Function
            End If
            If pISGSTRegd = "N" And VB.Left(cboGSTStatus.Text, 1) <> "R" Then
                MsgBox("Supplier is not registered, please select the Reverse Charge.", MsgBoxStyle.Information)
                ' txtSupplier.SetFocus
                FieldsVarification = False
                Exit Function
            End If
            If pISGSTRegd = "E" And VB.Left(cboGSTStatus.Text, 1) <> "E" Then
                MsgBox("GST Exempted Supplier, please select the GST Exempted.", MsgBoxStyle.Information)
                ' txtSupplier.SetFocus
                FieldsVarification = False
                Exit Function
            End If
            If pISGSTRegd = "C" And VB.Left(cboGSTStatus.Text, 1) <> "C" Then
                MsgBox("GST Composit Supplier, please select the Composit.", MsgBoxStyle.Information)
                ' txtSupplier.SetFocus
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            Dim xAcctCode As String = ""

            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                If Trim(txtDebitAccount.Text) = "" Then
                    MsgBox("Please select Debit Account.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If

                If Trim(txtDebitAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xAcctCode = MasterNo
                    End If
                End If
            Else
                If Trim(txtCreditAccount.Text) = "" Then
                    MsgBox("Please select Credit Account.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If
                If Trim(txtCreditAccount.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xAcctCode = MasterNo
                    End If
                End If
            End If

            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If

        If Trim(cboPopulateFrom.Text) = "" Then
            MsgBox("Populate From is Blank.", MsgBoxStyle.Information)
            If cboPopulateFrom.Enabled = True Then cboPopulateFrom.Focus()
            FieldsVarification = False
        End If

        If lblDCType.Text = "R" And RsCompany.Fields("StockBalCheck").Value = "Y" Then
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                    mGSTRefund = VB.Left(cboGSTStatus.Text, 1)
                    With SprdMain
                        For mRow = 1 To .MaxRows - 1
                            .Row = mRow
                            .Col = ColMRRNo
                            mMRRNO = Val(.Text)
                            .Col = ColQty
                            mDNCNQty = Val(.Text)
                            .Col = ColItemCode
                            mItemCode = Trim(.Text)

                            If Val(mMRRNO) <= 0 Then
                                If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "AUTO_KEY_MRR", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'") = False Then
                                    MsgInformation("Invalid Supplier for MRR No. " & mMRRNO)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If
                            mMRRQty = GetMRRQty(mMRRNO, mItemCode)
                            mRejectedQty = GetRejectQty(mMRRNO, mItemCode)
                            mReOfferQty = GetReofferQty(mMRRNO, mItemCode)
                            mBalDrCrQty = GetDebitQty(mMRRNO, mItemCode, mVNo)
                            mRejectedQty = mRejectedQty - mReOfferQty
                            If VB.Left(cboPopulateFrom.Text, 1) = "M" Then
                                If mDNCNQty > mRejectedQty - mBalDrCrQty Then
                                    MsgInformation("Balance RJ Qty " & mRejectedQty - mBalDrCrQty & " is less than Debit Note Qty " & mDNCNQty & ". Already Deduct Voucher No : " & mVNo & ". Cann't be Save.")
                                    Call MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If
                            If mMRRQty - mBalDrCrQty < mDNCNQty Then
                                If mVNo = "" Then
                                    MsgInformation("Balance RJ Qty " & mMRRQty - mBalDrCrQty & " is less than Debit Note Qty " & mDNCNQty & ". Cann't be Save.")
                                Else
                                    MsgInformation("Balance RJ Qty " & mMRRQty - mBalDrCrQty & " is less than Debit Note Qty " & mDNCNQty & ". VNO No : " & mVNo & ". Cann't be Save.")
                                End If
                                Call MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                                FieldsVarification = False
                                Exit Function
                            End If
                            If mMRRQty < mDNCNQty Then
                                MsgInformation("MRR Qty " & mMRRQty & " is less than Debit Note Qty " & mDNCNQty & ". Cann't be Save.")
                                Call MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                                FieldsVarification = False
                                Exit Function
                            End If
                            If mDNCNQty > mRejectedQty Then
                                If PubSuperUser = "S" Then GoTo NextTemp
                                '                        If mBalDrCrQty = 0 Then
                                '                            MsgInformation "Debit Note Qty Cann't be Greater Than MRR Qty. (MRR Qty " & vb6.Format(mRejectedQty, "0.000") & ")"
                                '                        Else
                                MsgInformation("Debit Note already Entered. No Balance Qty to Send. Balance Qty : " & mRejectedQty)
                                '                        End If
                                Call MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                                FieldsVarification = False
                                Exit Function
                            End If
NextTemp:
                            mGSTRefund_MRR = "N"
                            If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "ISGSTAPPLICABLE", "FIN_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mGSTRefund_MRR = Trim(MasterNo)
                            End If
                            If mGSTRefund_MRR = "G" Then
                                If mGSTRefund_MRR <> mGSTRefund Then
                                    If PubSuperUser = "S" Then
                                        If MsgQuestion("Please check GST Refund Check. GST Refund not taken for this Bill. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                                            Call MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                                            FieldsVarification = False
                                            Exit Function
                                        End If
                                    Else
                                        MsgInformation("GST Refund Not Match with GST Refund Register.")
                                        FieldsVarification = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    End With
                Else
                    With SprdMain
                        For mRow = 1 To .MaxRows - 1
                            .Row = mRow
                            .Col = ColMRRNo
                            mMRRNO = Val(.Text)
                            .Col = ColQty
                            mDNCNQty = Val(.Text)
                            .Col = ColItemCode
                            mItemCode = Trim(.Text)
                            .Col = ColUnit
                            mItemUOM = Trim(.Text)
                            If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "AUTO_KEY_MRR", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'") = False Then
                                MsgInformation("Invalid Supplier for MRR No. " & mMRRNO)
                                FieldsVarification = False
                                Exit Function
                            End If
                            mRejectedQty = GetRejectQty(mMRRNO, mItemCode)
                            mBalDrCrQty = GetDebitQty(mMRRNO, mItemCode, mVNo)
                            mDespatchQty = GetDespatchQty(mItemCode, mItemUOM, mMRRNO)
                            mReOfferQty = GetReofferQty(mMRRNO, mItemCode)
                            If mBalDrCrQty - mDespatchQty >= mDNCNQty Then
                                If VB.Left(cboPopulateFrom.Text, 1) = "R" Then
                                    If mDNCNQty > mReOfferQty Then
                                        MsgInformation("Credit Note Qty Cann't be Greater Than Reoffer's Qty. (Reoffer Qty " & VB6.Format(mReOfferQty, "0.000") & ")")
                                        Call MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                                        FieldsVarification = False
                                        Exit Function
                                    End If
                                End If
                            Else
                                MsgInformation("Credit Note Qty Cann't be Greater Than MRR Qty. (Debit Note Qty " & VB6.Format(mBalDrCrQty, "0.000") & ")")
                                FieldsVarification = False
                                Exit Function
                            End If
                        Next
                    End With
                End If
            End If
        End If
        ''    If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False: Exit Function
        ''    If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function
        If Val(lblNetAmount.Text) = 0 Then
            MsgBox("Nothing to save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        '    If ADDMode = True Then
        If CheckVType() = False Then
            MsgInformation("Either Voucher Type is not valid or Not in your series.")
            If txtVType.Enabled = True Then txtVType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        '    End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            With SprdMain
                For mRow = 1 To .MaxRows
                    .Row = mRow
                    .Col = ColMRRNo
                    mMRRNO = Val(.Text)
                    If Val(CStr(mMRRNO)) > 0 Then
                        If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "AUTO_KEY_MRR", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & "") = False Then
                            MsgInformation("MRR No : " & mMRRNO & " is invalid for such Division. ")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                Next
            End With
        End If
        mSTTaxcount = 0
        With SprdExp
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColExpIdent
                If Trim(.Text) = "ED" Then
                    .Col = ColExpAmt
                    If Val(.Text) > 0 Then
                        mSTTaxcount = mSTTaxcount + 1
                        If mSTTaxcount > 1 Then Exit For
                    End If
                End If
            Next
        End With
        If mSTTaxcount > 1 Then
            MsgBox("Please Check Excise Duty Expenses.", MsgBoxStyle.Information)
            FieldsVarification = False
            Call MainClass.SetFocusToCell(SprdExp, mRow, ColExpAmt)
            Exit Function
        End If
        mSTTaxcount = 0
        With SprdExp
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColExpIdent
                If Trim(.Text) = "ST" Then
                    .Col = ColExpAmt
                    If Val(.Text) > 0 Then
                        mSTTaxcount = mSTTaxcount + 1
                        If mSTTaxcount > 1 Then Exit For
                    End If
                End If
            Next
        End With
        If mSTTaxcount > 1 Then
            MsgBox("Please Check Sales Tax Expenses.", MsgBoxStyle.Information)
            FieldsVarification = False
            Call MainClass.SetFocusToCell(SprdExp, mRow, ColExpAmt)
            Exit Function
        End If
        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''    Resume
    End Function
    Private Function GetDespatchQty(ByRef pItemCode As String, ByRef pUOM As String, ByRef mMRRNO As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        SqlStr = "SELECT SUM( ID.PACKED_QTY/DECODE(INVMST.ISSUE_UOM,'" & pUOM & "',1,INVMST.UOM_FACTOR)) AS QTY " & vbCrLf & " FROM " & vbCrLf & " DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.MRR_REF_NO=" & Val(CStr(mMRRNO)) & "" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.DESP_TYPE IN ('Q','L')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetDespatchQty = IIf(IsDBNull(RsTemp.Fields("QTY").Value), 0, RsTemp.Fields("QTY").Value)
        End If
        Exit Function
ErrPart:
        GetDespatchQty = 0
    End Function
    Private Function GetMRRQty(ByRef pMRRNo As Double, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mIsReoffer As Boolean
        Dim mItemUOM As String
        Dim mIssueUOM As String
        Dim mUOMFactor As Double
        GetMRRQty = 0
        SqlStr = "SELECT SUM(RECEIVED_QTY) AS RECEIVED_QTY, ITEM_UOM" & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_UOM"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mUOMFactor = 1
                mItemUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mIssueUOM = Trim(MasterNo)
                    If mItemUOM <> mIssueUOM Then
                        If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mUOMFactor = Val(MasterNo)
                        End If
                    End If
                End If
                GetMRRQty = GetMRRQty + (IIf(IsDBNull(RsTemp.Fields("RECEIVED_QTY").Value), 0, RsTemp.Fields("RECEIVED_QTY").Value) * mUOMFactor)
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetMRRQty = 0
    End Function
    Private Function GetRejectQty(ByRef pMRRNo As Double, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mIsReoffer As Boolean
        Dim mItemUOM As String
        Dim mIssueUOM As String
        Dim mUOMFactor As Double
        SqlStr = "SELECT "
        If VB.Left(cboPopulateFrom.Text, 1) = "M" Or VB.Left(cboPopulateFrom.Text, 1) = "R" Then ' ''OptDCType(0).Value = True Or OptDCType(2).Value = True Then
            SqlStr = SqlStr & " SUM(REJECTED_QTY) AS REJ_QTY, ITEM_UOM"
            mIsReoffer = False
        ElseIf VB.Left(cboPopulateFrom.Text, 1) = "S" Then  'OptDCType(1).Value = True Then
            SqlStr = SqlStr & " SUM(APPROVED_QTY+LOT_ACC_SEG+REJECTED_QTY) AS REJ_QTY, ITEM_UOM"
            mIsReoffer = False
            '    ElseIf OptDCType(2).Value = True Then
            '        SqlStr = SqlStr & " SUM(LOT_ACC_RWK) AS REJ_QTY, ITEM_UOM"
            '        mIsReoffer = True
        Else
            GetRejectQty = 0
            Exit Function
        End If
        If mIsReoffer = False Then
            SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " FROM INV_REOFFER_HDR IH, INV_REOFFER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND IH.IS_POSTED='Y'"
        End If
        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_UOM"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mUOMFactor = 1
                mItemUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mIssueUOM = Trim(MasterNo)
                    If mItemUOM <> mIssueUOM Then
                        If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mUOMFactor = Val(MasterNo)
                        End If
                    End If
                End If
                GetRejectQty = GetRejectQty + (IIf(IsDBNull(RsTemp.Fields("REJ_QTY").Value), 0, RsTemp.Fields("REJ_QTY").Value) * mUOMFactor)
                RsTemp.MoveNext()
            Loop
        End If
        GetRejectQty = GetRejectQty ''* mUOMFactor
        Exit Function
ErrPart:
        GetRejectQty = 0
    End Function
    Private Function GetReofferQty(ByRef pMRRNo As Double, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mIsReoffer As Boolean
        Dim mItemUOM As String
        Dim mIssueUOM As String
        Dim mUOMFactor As Double
        If VB.Left(cboPopulateFrom.Text, 1) = "M" Or VB.Left(cboPopulateFrom.Text, 1) = "R" Then 'If OptDCType(0).Value = True Or OptDCType(2).Value = True Then
            SqlStr = SqlStr & " SELECT SUM(LOT_ACC_RWK) AS ACCT_QTY, ITEM_UOM"
            SqlStr = SqlStr & vbCrLf & " FROM INV_REOFFER_HDR IH, INV_REOFFER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND IH.IS_POSTED='Y'"
            SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_UOM"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mUOMFactor = 1
                    mItemUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                    If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mIssueUOM = Trim(MasterNo)
                        If mItemUOM <> mIssueUOM Then
                            If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mUOMFactor = Val(MasterNo)
                            End If
                        End If
                    End If
                    GetReofferQty = GetReofferQty + (IIf(IsDBNull(RsTemp.Fields("ACCT_QTY").Value), 0, RsTemp.Fields("ACCT_QTY").Value) * mUOMFactor)
                    RsTemp.MoveNext()
                Loop
            End If
        End If
        Exit Function
ErrPart:
        GetReofferQty = 0
    End Function
    Private Function GetDebitQty(ByRef pMRRNo As Double, ByRef pItemCode As String, ByRef mVNo As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        mVNo = ""
        GetDebitQty = 0
        SqlStr = "SELECT IH.VNO, SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY * DECODE(IH.BOOKSUBTYPE,'D',1,-1)) AS QTY " & vbCrLf & " FROM " & vbCrLf & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.DNCNTYPE='R' AND CANCELLED='N' AND IH.DNCNFROM IN ('M','R','S') " ''AND APPROVED='Y'
        '    If LblBookCode.text = ConCreditNoteBookCode Then
        '        SqlStr = SqlStr & vbCrLf & " AND IH.ISDESPATCHED='Y'"
        '    End If
        If Trim(LblMKey.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY<>'" & LblMKey.Text & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.VNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mVNo = IIf(mVNo = "", "", mVNo & ", ") & IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                GetDebitQty = GetDebitQty + IIf(IsDBNull(RsTemp.Fields("QTY").Value), 0, RsTemp.Fields("QTY").Value)
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetDebitQty = 0
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmDrCrNoteGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SqlStr = ""
        SqlStr = "Select * from FIN_DNCN_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNCNMain, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_DNCN_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNCNDetail, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_DNCN_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNCNExp, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        Call SetTextLengths()
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mBookType = VB.Left(ConDebitNote, 1)
            mBookSubType = VB.Right(ConDebitNote, 1)
        Else
            mBookType = VB.Left(ConCreditNote, 1)
            mBookSubType = VB.Right(ConCreditNote, 1)
        End If
        If lblDCType.Text = "S" Or lblDCType.Text = "P" Or lblDCType.Text = "A" Or lblDCType.Text = "V" Then
            cboPopulateFrom.Items.Add("P - Purchase")
            cboPopulateFrom.Items.Add("D - Debit / Credit Note")
        ElseIf lblDCType.Text = "D" Or lblDCType.Text = "O" Then
            cboPopulateFrom.Items.Add("D - Debit / Credit Note")
            cboPopulateFrom.Items.Add("P - Purchase")
            cboPopulateFrom.Items.Add("N - Others")
        ElseIf lblDCType.Text = "R" Then
            If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                cboPopulateFrom.Items.Add("M - QC Rejection")
                cboPopulateFrom.Items.Add("S - Line Rejection")
            Else
                cboPopulateFrom.Items.Add("R - Re-offer")
            End If
            cboPopulateFrom.Items.Add("O - Others")
        End If
        cboGSTStatus.Items.Clear()
        cboGSTStatus.Items.Add("GST Input")
        '    cboGSTStatus.AddItem "Reverse Charge"
        '    cboGSTStatus.AddItem "Exempt"
        '    cboGSTStatus.AddItem "Non-GST"

        '    cboGSTStatus.AddItem "Composit"
        cboGSTStatus.Items.Add("Without GST")
        cboGSTStatus.Items.Add("Ineligible")  'cboGSTStatus.AddItem "Ineligible"


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            If lblDCType.Text = "P" Then
                cboGSTStatus.SelectedIndex = 1
            End If
        End If

        '    If lblDCType = "S" Then
        '        txtPurVNo.Enabled = True
        '        cmdVNoSearch.Enabled = True
        '    ElseIf lblDCType = "S" Then
        '        txtPurVNo.Enabled = True
        '        cmdVNoSearch.Enabled = True
        '    ElseIf lblDCType = "S" Then
        '        txtBillNo.Enabled = True
        '        cmdBillNoSearch.Enabled = True
        '    End If
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
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)


        SqlStr = ""
        SqlStr = "SELECT DECODE(APPROVED,'Y','YES','NO') AS APPROVED," & vbCrLf _
            & " VTYPE, VNOPREFIX, TO_CHAR(VNOSEQ),VNOSUFFIX, VNO, VDATE, " & vbCrLf _
            & " CASE WHEN DNCNTYPE='P' THEN 'PO RATE DIFF.'  " & vbCrLf _
            & " WHEN DNCNTYPE='A' THEN 'AMEND PO RATE DIFF.'  " & vbCrLf _
            & " WHEN DNCNTYPE='S' THEN 'SHORTAGE'  " & vbCrLf _
            & " WHEN DNCNTYPE='R' THEN 'REJECTION'  " & vbCrLf _
            & " WHEN DNCNTYPE='D' THEN 'DISCOUNT'  " & vbCrLf _
            & " WHEN DNCNTYPE='V' THEN 'VOLUME DISCOUNT'  " & vbCrLf _
            & " WHEN DNCNTYPE='O' THEN 'OTHERS' END AS REASON, " & vbCrLf _
            & " A.SUPP_CUST_NAME AS DEBITACCOUNT, B.SUPP_CUST_NAME AS CREDITACCOUNT, " & vbCrLf _
            & " PURVNO, PURVDATE, BILLNO, INVOICE_DATE  AS BILLDATE, " & vbCrLf _
            & " AUTO_KEY_MRR AS MRRNO, MRRDATE, " & vbCrLf _
            & " NETVALUE FROM " & vbCrLf _
            & " FIN_DNCN_HDR DN, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE DN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And DN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND DN.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
            & " AND DN.DEBITACCOUNTCODE=A.SUPP_CUST_CODE " & vbCrLf _
            & " AND DN.COMPANY_CODE=B.COMPANY_CODE(+) " & vbCrLf _
            & " AND DN.CREDITACCOUNTCODE=B.SUPP_CUST_CODE(+) " & vbCrLf _
            & " AND BOOKCODE=" & Val(LblBookCode.Text) & " AND DNCNTYPE='" & lblDCType.Text & "'"

        If RsCompany.Fields("FYEAR").Value >= 2020 Then
            SqlStr = SqlStr & vbCrLf & " AND DNCNSEQTYPE=" & Val(lblDNCNSeqType.Text) & ""
        End If
        SqlStr = SqlStr & vbCrLf & " Order by VDATE,VNO"

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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Approved"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "VType"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "VNo Prefix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "VNo Seq"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "VNo Suffix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "VNo"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "VDate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Reason"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Debit Account"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Credit Account"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Purchase VNo"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Purchase VDate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "MRR No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "MRR Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Header.Caption = "Net Value"


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
            Next

            For inti = 16 To 16
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Style = UltraWinGrid.ColumnStyle.Double
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellAppearance.TextHAlign = HAlign.Right
            Next

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Hidden = True


            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 250

            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Width = 100



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
    '    With SprdView
    '        .Row = -1
    '        .set_RowHeight(0, 600)
    '        .set_ColWidth(0, 600)
    '        .set_ColWidth(1, 1000)
    '        .set_ColWidth(2, 0)
    '        .set_ColWidth(3, 0)
    '        .set_ColWidth(4, 0)
    '        .set_ColWidth(5, 0)
    '        .set_ColWidth(6, 1200)
    '        .set_ColWidth(7, 1200)
    '        .set_ColWidth(8, 1200)
    '        .set_ColWidth(9, 2500)
    '        .set_ColWidth(10, 2500)
    '        .set_ColWidth(11, 1200)
    '        .set_ColWidth(12, 1200)
    '        .set_ColWidth(13, 1200)
    '        .set_ColWidth(14, 1200)
    '        .set_ColWidth(15, 1200)
    '        .set_ColWidth(16, 1200)
    '        .set_ColWidth(17, 1200)
    '        .Col = 17
    '        .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        SprdView.set_RowHeight(-1, 300)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub
    Private Sub FormatSprdExp(ByRef Arow As Integer)
        On Error GoTo ERR1
        pShowCalc = False
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)
            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColExpName, 27)

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .set_ColWidth(ColExpPercent, 6)

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 9)
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

            .Col = ColRO
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRO, 4)
            '.Value = vbUnchecked
            MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpName, ColExpName)
        End With
        MainClass.SetSpreadColor(SprdExp, Arow)
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        Dim I As Integer
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .Col = ColPURMkey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = 20 ''RsDNCNDetail.Fields("MRR_REF_No").DefinedSize           ''
            .set_ColWidth(ColPURMkey, 8)
            .ColHidden = True
            .Col = ColPurNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = 20 ''RsDNCNDetail.Fields("MRR_REF_No").DefinedSize           ''
            .set_ColWidth(ColPurNO, 8)
            .ColHidden = True
            .Col = ColPurDATE
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''
            .set_ColWidth(ColPurDATE, 8)
            .ColHidden = True
            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = 20 ''RsDNCNDetail.Fields("MRR_REF_No").DefinedSize           ''
            .set_ColWidth(ColMRRNo, 8)
            .ColHidden = True
            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''
            .set_ColWidth(ColMRRDate, 8)
            .ColHidden = True
            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 20 ''RsDNCNDetail.Fields("SUPP_REF_NO").DefinedSize           ''
            .set_ColWidth(ColBillNo, 10)
            .ColHidden = False
            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''
            .set_ColWidth(ColBillDate, 8)
            .ColHidden = True
            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 20 ''RsDNCNDetail.Fields("SUPP_REF_NO").DefinedSize           ''
            .set_ColWidth(ColRefNo, 10)
            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''
            .set_ColWidth(ColRefDate, 8)
            .ColHidden = True
            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsDNCNDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)
            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .ColHidden = True
            .ColsFrozen = ColItemDesc
            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 22)
            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsDNCNDetail.Fields("HSNCODE").DefinedSize
            .set_ColWidth(ColHSNCode, 5)
            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsDNCNDetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

            .Col = ColBillQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBillQty, 8)


            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 8)
            .Col = ColPORate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPORate, 8)
            .Col = ColBillRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBillRate, 8)
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
            For I = ColCGSTPer To ColIGSTPer
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99.99")
                .TypeFloatMin = CDbl("-99.99")
                .set_ColWidth(I, 5)
            Next
            I = 0
            For I = ColCGSTAmount To ColIGSTAmount
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .set_ColWidth(I, 9)
            Next
            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsDNCNDetail.Fields("REF_PO_NO").DefinedSize ''
            .set_ColWidth(ColPONo, 10)
        End With
        If lblDCType.Text = "S" Or lblDCType.Text = "R" Then
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPURMkey, ColRefDate)
            'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMRRNo, ColRefDate)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColUnit)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColPONo)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColUnit)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColPONo)
        End If
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPURMkey, ColRefDate)
        'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMRRNo, ColRefDate)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPORate, ColBillRate)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsDNCNDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsDNCNMain
            txtVNo.MaxLength = .Fields("Vno").DefinedSize ''
            txtVType.MaxLength = .Fields("VTYPE").DefinedSize
            txtVNoPrefix.MaxLength = .Fields("VNoPrefix").DefinedSize ''
            txtVNoSuffix.MaxLength = .Fields("VNoSuffix").DefinedSize ''
            txtVDate.MaxLength = 10
            txtPartyDNNo.MaxLength = .Fields("PARTY_DNCN_NO").DefinedSize ''
            txtPartyDNDate.MaxLength = 10
            txtRecdDate.MaxLength = 10
            '        txtPurVNo.MaxLength = .Fields("PURVNO").DefinedSize           ''
            '        txtPurVDate.MaxLength = 10
            '        txtPONo.MaxLength = .Fields("CUSTREFNO").DefinedSize           ''
            '        txtPODate.MaxLength = 10
            txtDebitAccount.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtCreditAccount.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtReason.MaxLength = .Fields("REASON").DefinedSize ''
            txtNarration.MaxLength = .Fields("NARRATION").DefinedSize ''
            '        txtModvatNo.MaxLength = .Fields("MODVATNO").DefinedSize           ''
            '        txtModvatDate.MaxLength = 10
            '        txtModvatPer.MaxLength = .Fields("MODVATPER").DefinedSize           ''
            '        txtModvatAmount.MaxLength = .Fields("MODVATAMOUNT").DefinedSize           ''
            txtCGSTRefundAmount.MaxLength = .Fields("CGST_REFUNDAMOUNT").DefinedSize ''
            txtSGSTRefundAmount.MaxLength = .Fields("SGST_REFUNDAMOUNT").DefinedSize
            txtIGSTRefundAmount.MaxLength = .Fields("IGST_REFUNDAMOUNT").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim mAccountName As String
        Dim mLocal As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim pDNCNFrom As String
        Dim mGSTStatus As String
        With RsDNCNMain
            If Not .EOF Then
                LblMKey.Text = .Fields("MKey").Value
                txtVNoPrefix.Text = VB6.Format(IIf(IsDBNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value), "00")
                txtVType.Text = IIf(IsDBNull(.Fields("VTYPE").Value), "", .Fields("VTYPE").Value)
                txtVNo.Text = VB6.Format(IIf(IsDBNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                txtVNoSuffix.Text = IIf(IsDBNull(.Fields("VNOSUFFIX").Value), "", .Fields("VNOSUFFIX").Value)
                txtVDate.Text = VB6.Format(IIf(IsDBNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")
                txtPartyDNNo.Text = IIf(IsDBNull(.Fields("PARTY_DNCN_NO").Value), "", .Fields("PARTY_DNCN_NO").Value)
                txtPartyDNDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PARTY_DNCN_DATE").Value), "", .Fields("PARTY_DNCN_DATE").Value), "DD/MM/YYYY")
                txtRecdDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PARTY_DNCN_RECDDATE").Value), "", .Fields("PARTY_DNCN_RECDDATE").Value), "DD/MM/YYYY")
                txtPurVNo.Text = IIf(IsDBNull(.Fields("PURVNO").Value), "", .Fields("PURVNO").Value)
                txtPurVDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PURVDATE").Value), "", .Fields("PURVDATE").Value), "DD/MM/YYYY")
                txtBillNo.Text = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                txtMRRNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value), "DD/MM/YYYY")
                ''lblPayDate.text = Format(IIf(IsNull(.Fields("PAYDATE").Value), "", .Fields("PAYDATE").Value), "DD/MM/YYYY")
                lblPayDate.Text = GetPayDate((txtPurVNo.Text), (txtPurVDate.Text))
                If MainClass.ValidateWithMasterTable((.Fields("DEBITACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDebitAccount.Text = MasterNo
                End If
                If MainClass.ValidateWithMasterTable((.Fields("CREDITACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCreditAccount.Text = MasterNo
                End If
                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)

                chkAproved.CheckState = IIf(.Fields("APPROVED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkAproved.Enabled = IIf(PubSuperUser = "S", True, IIf(.Fields("APPROVED").Value = "Y", False, True))


                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                If chkAproved.CheckState = System.Windows.Forms.CheckState.Checked Then
                    chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, IIf(PubSuperUser = "S", True, False))
                Else
                    chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)
                End If



                lblTotQty.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                lblTotItemValue.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                '            lblTotED.text = 0 ' Format(IIf(IsNull(.Fields("TOTEDAMOUNT").Value), 0, .Fields("TOTEDAMOUNT").Value), "0.00")
                '            lblTotST.text = 0 '  Format(IIf(IsNull(.Fields("TOTSTAMT").Value), 0, .Fields("TOTSTAMT").Value), "0.00")
                lblTotCGSTAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETCGST_AMOUNT").Value), 0, .Fields("NETCGST_AMOUNT").Value), "0.00")
                lblTotSGSTAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETSGST_AMOUNT").Value), 0, .Fields("NETSGST_AMOUNT").Value), "0.00")
                lblTotIGSTAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETIGST_AMOUNT").Value), 0, .Fields("NETIGST_AMOUNT").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                txtReason.Text = IIf(IsDBNull(.Fields("REASON").Value), "", .Fields("REASON").Value)
                txtNarration.Text = IIf(IsDBNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                lblDNCNSeqType.Text = IIf(IsDBNull(.Fields("DNCNSEQTYPE").Value), 0, .Fields("DNCNSEQTYPE").Value)
                pDNCNFrom = IIf(IsDBNull(.Fields("DNCNFROM").Value), "", .Fields("DNCNFROM").Value)
                If lblDCType.Text = "S" Or lblDCType.Text = "P" Or lblDCType.Text = "A" Or lblDCType.Text = "V" Then
                    If pDNCNFrom = "P" Then
                        cboPopulateFrom.Text = "P - Purchase"
                    ElseIf pDNCNFrom = "D" Then
                        cboPopulateFrom.Text = "D - Debit / Credit Note"
                    End If
                ElseIf lblDCType.Text = "D" Or lblDCType.Text = "O" Then
                    If pDNCNFrom = "P" Then
                        cboPopulateFrom.Text = "P - Purchase"
                    ElseIf pDNCNFrom = "D" Then
                        cboPopulateFrom.Text = "D - Debit / Credit Note"
                    ElseIf pDNCNFrom = "N" Then
                        cboPopulateFrom.Text = "N - Others"
                    End If
                ElseIf lblDCType.Text = "R" Then
                    If pDNCNFrom = "M" Then
                        cboPopulateFrom.Text = "M - QC Rejection"
                    ElseIf pDNCNFrom = "S" Then
                        cboPopulateFrom.Text = "S - Line Rejection"
                    ElseIf pDNCNFrom = "R" Then
                        cboPopulateFrom.Text = "R - Re-offer"
                    ElseIf pDNCNFrom = "O" Then
                        cboPopulateFrom.Text = "O - Others"
                    End If
                End If
                txtCGSTRefundAmount.Text = IIf(IsDBNull(.Fields("CGST_REFUNDAMOUNT").Value), "", .Fields("CGST_REFUNDAMOUNT").Value)
                txtSGSTRefundAmount.Text = IIf(IsDBNull(.Fields("SGST_REFUNDAMOUNT").Value), "", .Fields("SGST_REFUNDAMOUNT").Value)
                txtIGSTRefundAmount.Text = IIf(IsDBNull(.Fields("IGST_REFUNDAMOUNT").Value), "", .Fields("IGST_REFUNDAMOUNT").Value)
                mGSTStatus = IIf(IsDBNull(.Fields("ISGSTREFUND").Value), "E", .Fields("ISGSTREFUND").Value) ''IIf(.Fields("ISGSTREFUND").Value = "E", vbChecked, vbUnchecked)
                If mGSTStatus = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                    '            ElseIf mGSTStatus = "R" Then
                    '                cboGSTStatus.ListIndex = 1
                    '            ElseIf mGSTStatus = "E" Then
                    '                cboGSTStatus.ListIndex = 2
                    '            ElseIf mGSTStatus = "N" Then
                    '                cboGSTStatus.ListIndex = 3
                    '            ElseIf mGSTStatus = "I" Then
                    '                cboGSTStatus.ListIndex = 4
                    '            ElseIf mGSTStatus = "C" Then
                    '                cboGSTStatus.ListIndex = 5
                ElseIf mGSTStatus = "W" Then
                    cboGSTStatus.SelectedIndex = 1 '' 6
                Else
                    cboGSTStatus.SelectedIndex = 2 '' 6
                End If
                '

                If chkAproved.CheckState = System.Windows.Forms.CheckState.Checked Then
                    cboGSTStatus.Enabled = IIf(PubSuperUser = "S", True, False)
                Else
                    cboGSTStatus.Enabled = True
                End If


                If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                    mAccountName = Trim(txtDebitAccount.Text)
                Else
                    mAccountName = Trim(txtCreditAccount.Text)
                End If
                '            If Trim(mAccountName) <> "" Then
                '                If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    mLocal = IIf(MasterNo = "Y", "L", "C")
                '                Else
                '                    mLocal = ""
                '                End If
                '            Else
                '                mLocal = ""
                '            End If
                '
                '            chkSTRefund.Visible = IIf(mLocal = "L", True, False)
                '            chkCSTRefund.Visible = IIf(mLocal = "C", True, False)
                '
                '            chkSTRefund.Value = IIf(.Fields("ISSTREFUND").Value = "Y", vbChecked, vbUnchecked)
                '            chkCSTRefund.Value = IIf(.Fields("ISCSTREFUND").Value = "Y", vbChecked, vbUnchecked)
                '
                '            If mLocal = "C" Then
                '                chkSTRefund.Value = IIf(chkSTRefund.Value = vbChecked, vbUnchecked, chkSTRefund.Value)
                '            End If
                '
                '            If mLocal = "L" Then
                '                chkCSTRefund.Value = IIf(chkCSTRefund.Value = vbChecked, vbUnchecked, chkCSTRefund.Value)
                '            End If
                LblMKey.Text = IIf(IsDBNull(.Fields("MKEY").Value), "", .Fields("MKEY").Value)
                mAddUser = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")
                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False
                Call ShowDetail1((LblMKey.Text))
                Call ShowExp1((LblMKey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                ''Call CalcTots
                cmdVNoSearch.Enabled = False
                txtPurVNo.Enabled = False
                cmdMRRNoSearch.Enabled = False
                txtMRRNo.Enabled = False
                cboPopulateFrom.Enabled = False
                '            cmdPopulate.Enabled = False
                txtDebitAccount.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            End If
        End With
        txtVNo.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsDNCNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        FormatSprdMain(-1)
        '    OptDCType(0).Enabled = False
        '    OptDCType(1).Enabled = False
        '    OptDCType(2).Enabled = False
        SprdMain.Enabled = True
        SprdExp.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowFromPurchase(ByRef mRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        Dim mAccountName As String
        Dim mPartyName As String
        Dim xMRRNo As Double
        Dim xMRRDate As String
        Dim xBillNo As String
        Dim xBillDate As String
        Dim mPurVNo As String
        Dim mPurVDate As String
        Dim mISGST As String
        Dim mGSTNo As String
        Dim mGSTDate As String
        Dim mCGSTRefundAmount As Double
        Dim mSGSTRefundAmount As Double
        Dim mIGSTRefundAmount As Double
        Dim mPurchaseType As String
        With mRsTemp
            If Not .EOF Then
                LblMKey.Text = ""
                mPurVNo = IIf(IsDBNull(.Fields("VNO").Value), "", .Fields("VNO").Value)
                mPurVDate = VB6.Format(IIf(IsDBNull(.Fields("VDATE").Value), "", .Fields("VDATE").Value), "DD/MM/YYYY")
                xMRRNo = IIf(IsDBNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                xMRRDate = IIf(IsDBNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value)
                xBillNo = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                xBillDate = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                lblPayDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PAYMENTDATE").Value), "", .Fields("PAYMENTDATE").Value), "DD/MM/YYYY")
                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPartyName = MasterNo
                End If
                If MainClass.ValidateWithMasterTable((.Fields("ACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = MasterNo
                End If

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)

                mPurchaseType = IIf(IsDBNull(.Fields("PURCHASE_TYPE").Value), "", .Fields("PURCHASE_TYPE").Value)
                If CDate(xBillDate) < CDate(PubGSTApplicableDate) Then
                    cboGSTStatus.SelectedIndex = 1
                Else
                    If mPurchaseType = "G" Or mPurchaseType = "J" Or mPurchaseType = "R" Then
                        mISGST = IIf(IsDBNull(.Fields("ISGSTAPPLICABLE").Value), "W", .Fields("ISGSTAPPLICABLE").Value)
                        If mISGST = "G" Then
                            cboGSTStatus.SelectedIndex = 0
                            '                    ElseIf mISGST = "R" Then
                            '                        cboGSTStatus.ListIndex = 1
                            '                    ElseIf mISGST = "E" Then
                            '                        cboGSTStatus.ListIndex = 2
                            '                    ElseIf mISGST = "N" Then
                            '                        cboGSTStatus.ListIndex = 3
                            '                    ElseIf mISGST = "I" Then
                            '                        cboGSTStatus.ListIndex = 4
                            '                    ElseIf mISGST = "C" Then
                            '                        cboGSTStatus.ListIndex = 5
                            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                                If lblDCType.Text = "P" Then
                                    cboGSTStatus.SelectedIndex = 1
                                End If
                            End If
                        ElseIf mISGST = "I" Then
                            cboGSTStatus.SelectedIndex = 2 ''6
                        Else ''If mISGST = "W" Then
                            cboGSTStatus.SelectedIndex = 1 ''6
                        End If
                    End If
                End If
                txtBillNo.Text = xBillNo
                txtBillDate.Text = VB6.Format(xBillDate, "DD/MM/YYYY")
                txtPurVNo.Text = mPurVNo
                txtPurVDate.Text = VB6.Format(mPurVDate, "DD/MM/YYYY")
                If mFillData = False Then
                    If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                        txtDebitAccount.Text = mPartyName
                        txtCreditAccount.Text = mAccountName
                        txtDebitAccount.Enabled = False
                    Else
                        txtCreditAccount.Text = mPartyName
                        txtDebitAccount.Text = mAccountName
                        txtCreditAccount.Enabled = False
                    End If
                End If
                Call ShowPurDetail1((.Fields("MKEY").Value), (.Fields("FYEAR").Value), mPurVNo, mPurVDate, xMRRNo, xMRRDate, xBillNo, xBillDate, (.Fields("SUPP_CUST_CODE").Value), mPurchaseType)
                cmdVNoSearch.Enabled = True
                txtPurVNo.Enabled = True
                cboPopulateFrom.Enabled = False
                '            Call ShowPurExp1(.Fields("MKEY").Value)
                mFillData = True
            End If
        End With
        Call CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowFromTRN(ByRef mRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        Dim mAccountName As String
        Dim mPartyName As String
        Dim xMRRNo As Double
        Dim xMRRDate As String
        Dim xBillNo As String
        Dim xBillDate As String
        With mRsTemp
            If Not .EOF Then
                LblMKey.Text = ""
                txtBillNo.Text = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")
                txtPurVNo.Text = "" ''IIf(IsNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtPurVDate.Text = "" ''Format(IIf(IsNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")
                lblPayDate.Text = VB6.Format(IIf(IsDBNull(.Fields("DUEDATE").Value), "", .Fields("DUEDATE").Value), "DD/MM/YYYY")
                If MainClass.ValidateWithMasterTable((.Fields("ACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPartyName = MasterNo
                End If
                If mFillData = False Then
                    If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
                        txtDebitAccount.Text = mPartyName
                        txtDebitAccount.Enabled = False
                    Else
                        txtCreditAccount.Text = mPartyName
                        txtCreditAccount.Enabled = False
                    End If
                End If
                mFillData = True
            End If
        End With
        Call CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowFromDNCN(ByRef mRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        Dim mAccountName As String
        Dim mPartyName As String
        Dim xMRRNo As Double
        Dim xMRRDate As String
        Dim xBillNo As String
        Dim xBillDate As String
        Dim xVNo As String
        Dim xVDate As String
        Dim mISGST As String
        With mRsTemp
            If Not .EOF Then
                LblMKey.Text = ""
                xVNo = IIf(IsDBNull(.Fields("VNO").Value), "", .Fields("VNO").Value)
                xVDate = VB6.Format(IIf(IsDBNull(.Fields("VDATE").Value), "", .Fields("VDATE").Value), "DD/MM/YYYY")
                '            xMRRNo = IIf(IsNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                '            xMRRDate = IIf(IsNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value)
                xBillNo = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                xBillDate = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                txtBillNo.Text = xBillNo
                txtBillDate.Text = VB6.Format(xBillDate, "DD/MM/YYYY")
                txtPurVNo.Text = xVNo
                txtPurVDate.Text = VB6.Format(xVDate, "DD/MM/YYYY")
                mISGST = IIf(IsDBNull(.Fields("ISGSTREFUND").Value), "W", .Fields("ISGSTREFUND").Value)
                If mISGST = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                    '            ElseIf mISGST = "R" Then
                    '                cboGSTStatus.ListIndex = 1
                    '            ElseIf mISGST = "E" Then
                    '                cboGSTStatus.ListIndex = 2
                    '            ElseIf mISGST = "N" Then
                    '                cboGSTStatus.ListIndex = 3
                    '            ElseIf mISGST = "I" Then
                    '                cboGSTStatus.ListIndex = 4
                    '            ElseIf mISGST = "C" Then
                    '                cboGSTStatus.ListIndex = 5
                ElseIf mISGST = "I" Then
                    cboGSTStatus.SelectedIndex = 2 '6
                Else 'If mISGST = "W" Then
                    cboGSTStatus.SelectedIndex = 1 '6
                End If
                lblPayDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PAYDATE").Value), "", .Fields("PAYDATE").Value), "DD/MM/YYYY")
                If MainClass.ValidateWithMasterTable((.Fields("DEBITACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPartyName = MasterNo
                End If
                If MainClass.ValidateWithMasterTable((.Fields("CREDITACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAccountName = MasterNo
                End If
                If mFillData = False Then
                    If .Fields("BOOKCODE").Value = ConDebitNoteBookCode Then
                        txtDebitAccount.Text = IIf(CDbl(LblBookCode.Text) = ConDebitNoteBookCode, mPartyName, mAccountName)
                        txtCreditAccount.Text = IIf(CDbl(LblBookCode.Text) = ConDebitNoteBookCode, mAccountName, mPartyName)
                    Else
                        txtDebitAccount.Text = IIf(CDbl(LblBookCode.Text) = ConCreditNoteBookCode, mPartyName, mAccountName)
                        txtCreditAccount.Text = IIf(CDbl(LblBookCode.Text) = ConCreditNoteBookCode, mAccountName, mPartyName)
                    End If
                End If
                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                Call ShowDNCNDetail1((.Fields("MKEY").Value), xVNo, xVDate, xMRRNo, xMRRDate, xBillNo, xBillDate)
                cmdVNoSearch.Enabled = True
                txtPurVNo.Enabled = True
                cboPopulateFrom.Enabled = False
                '            Call ShowDNCNExp1(.Fields("MKEY").Value)
                mFillData = True
            End If
        End With
        Call CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowExp1(ByRef mMkey As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Call FillSprdExp()
        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select FIN_DNCN_EXP.EXPCODE,FIN_DNCN_EXP.EXPPERCENT, " & vbCrLf & " FIN_DNCN_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO " & vbCrLf & " From FIN_DNCN_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_DNCN_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_DNCN_EXP.Mkey='" & mMkey & "'" & vbCrLf & " "
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNCNExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDNCNExp.EOF = False Then
            RsDNCNExp.MoveFirst()
            With SprdExp
                Do While Not RsDNCNExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        '
                        '                    .Col = ColExpIdent
                        '                    pExpId = Trim(.Text)
                        '
                        '                    If pExpId = "ST" Then
                        '
                        '                    End If
                        .Col = ColExpName
                        If .Text = RsDNCNExp.Fields("Name").Value Then Exit For
                    Next I
                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("ExpPercent").Value), "", RsDNCNExp.Fields("ExpPercent").Value)))
                    .Col = ColExpAmt
                    If RsDNCNExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("Amount").Value), "", RsDNCNExp.Fields("Amount").Value)))
                    Else
                        ''.Text = Abs(Val(IIf(IsNull(RsDNCNExp.Fields("Amount").Value), "", RsDNCNExp.Fields("Amount").Value)))
                        .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("Amount").Value), "", RsDNCNExp.Fields("Amount").Value)))
                    End If
                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("CODE").Value), 0, RsDNCNExp.Fields("CODE").Value)))
                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RsDNCNExp.Fields("Add_Ded").Value = "A", "A", "D")
                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsDNCNExp.Fields("Identification").Value), "", RsDNCNExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If
                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsDNCNExp.Fields("Taxable").Value), "N", RsDNCNExp.Fields("Taxable").Value)
                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsDNCNExp.Fields("Exciseable").Value), "N", RsDNCNExp.Fields("Exciseable").Value)
                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("CalcOn").Value), "", RsDNCNExp.Fields("CalcOn").Value)))
                    .Col = ColRO
                    .Value = IIf(RsDNCNExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    RsDNCNExp.MoveNext()
                Loop
            End With
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowPurExp1(ByRef mMkey As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim mEDAmountFill As Boolean
        Exit Sub
        Call FillSprdExp()
        pShowCalc = False
        mEDAmountFill = False
        SqlStr = ""
        SqlStr = "Select FIN_PURCHASE_EXP.EXPCODE,FIN_PURCHASE_EXP.EXPPERCENT, " & vbCrLf & " FIN_PURCHASE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO " & vbCrLf & " From FIN_PURCHASE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_PURCHASE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_PURCHASE_EXP.Mkey='" & mMkey & "'" & vbCrLf & " "
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNCNExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDNCNExp.EOF = False Then
            RsDNCNExp.MoveFirst()
            With SprdExp
                Do While Not RsDNCNExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        '
                        '                    .Col = ColExpIdent
                        '                    pExpId = Trim(.Text)
                        '
                        '                    If pExpId = "ST" Then
                        '
                        '                    End If
                        .Col = ColExpName
                        If .Text = RsDNCNExp.Fields("Name").Value Then Exit For
                        If RsDNCNExp.Fields("Identification").Value = "ED" And mEDAmountFill = False Then
                            If MainClass.ValidateWithMasterTable(Trim(.Text), "NAME", "Identification", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND Identification='ED'") = True Then
                                mEDAmountFill = True
                                Exit For
                            End If
                        End If
                    Next I
                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("ExpPercent").Value), "", RsDNCNExp.Fields("ExpPercent").Value)))
                    .Col = ColExpAmt
                    If RsDNCNExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("Amount").Value), "", RsDNCNExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsDNCNExp.Fields("Amount").Value), "", RsDNCNExp.Fields("Amount").Value))))
                    End If
                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("CODE").Value), 0, RsDNCNExp.Fields("CODE").Value)))
                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RsDNCNExp.Fields("Add_Ded").Value = "A", "A", "D")
                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsDNCNExp.Fields("Identification").Value), "", RsDNCNExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If
                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsDNCNExp.Fields("Taxable").Value), "N", RsDNCNExp.Fields("Taxable").Value)
                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsDNCNExp.Fields("Exciseable").Value), "N", RsDNCNExp.Fields("Exciseable").Value)
                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("CalcOn").Value), "", RsDNCNExp.Fields("CalcOn").Value)))
                    .Col = ColRO
                    .Value = IIf(RsDNCNExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    RsDNCNExp.MoveNext()
                Loop
            End With
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowDNCNExp1(ByRef mMkey As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Call FillSprdExp()
        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select FIN_DNCN_EXP.EXPCODE,FIN_DNCN_EXP.EXPPERCENT, " & vbCrLf & " FIN_DNCN_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO " & vbCrLf & " From FIN_DNCN_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_DNCN_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_DNCN_EXP.Mkey='" & mMkey & "'" & vbCrLf & " "
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNCNExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDNCNExp.EOF = False Then
            RsDNCNExp.MoveFirst()
            With SprdExp
                Do While Not RsDNCNExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        '
                        '                    .Col = ColExpIdent
                        '                    pExpId = Trim(.Text)
                        '
                        '                    If pExpId = "ST" Then
                        '
                        '                    End If
                        .Col = ColExpName
                        If .Text = RsDNCNExp.Fields("Name").Value Then Exit For
                    Next I
                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("ExpPercent").Value), "", RsDNCNExp.Fields("ExpPercent").Value)))
                    .Col = ColExpAmt
                    If RsDNCNExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("Amount").Value), "", RsDNCNExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsDNCNExp.Fields("Amount").Value), "", RsDNCNExp.Fields("Amount").Value))))
                    End If
                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("CODE").Value), 0, RsDNCNExp.Fields("CODE").Value)))
                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RsDNCNExp.Fields("Add_Ded").Value = "A", "A", "D")
                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsDNCNExp.Fields("Identification").Value), "", RsDNCNExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If
                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsDNCNExp.Fields("Taxable").Value), "N", RsDNCNExp.Fields("Taxable").Value)
                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsDNCNExp.Fields("Exciseable").Value), "N", RsDNCNExp.Fields("Exciseable").Value)
                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsDNCNExp.Fields("CalcOn").Value), "", RsDNCNExp.Fields("CalcOn").Value)))
                    .Col = ColRO
                    .Value = IIf(RsDNCNExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    RsDNCNExp.MoveNext()
                Loop
            End With
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
        Dim RsPOPrice As ADODB.Recordset
        Dim pMRRDate As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPONo As String
        Dim mPurNo As String
        Dim mPurDate As String
        Dim mBillDate As String
        SqlStr = ""
        SqlStr = " SELECT ID.* " & vbCrLf & " FROM FIN_DNCN_DET ID " & vbCrLf & " Where ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND Mkey='" & mMkey & "' Order By SubRowNo"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = True Then Exit Sub
            SprdMain.MaxRows = MainClass.GetMaxRecord("FIN_DNCN_DET", PubDBCn, "Mkey='" & mMkey & "'")
            SprdMain.MaxRows = SprdMain.MaxRows + 1
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I
                SprdMain.Col = ColPURMkey
                SprdMain.Text = IIf(IsDBNull(.Fields("PURMKEY").Value), "-1", Trim(.Fields("PURMKEY").Value))
                SprdMain.Col = ColPurNO
                mPurNo = IIf(IsDBNull(RsTemp.Fields("PURVNO").Value), "-1", RsTemp.Fields("PURVNO").Value)

                SprdMain.Text = mPurNo

                SprdMain.Col = ColPurDATE
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PURVDATE").Value), "", RsTemp.Fields("PURVDATE").Value), "DD/MM/YYYY")
                mPurDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PURVDATE").Value), "", RsTemp.Fields("PURVDATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColMRRNo
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("MRR_REF_NO").Value), "-1", Trim(Str(RsTemp.Fields("MRR_REF_NO").Value)))

                SprdMain.Col = ColMRRDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("MRR_REF_DATE").Value), "", RsTemp.Fields("MRR_REF_DATE").Value), "DD/MM/YYYY")
                pMRRDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("MRR_REF_DATE").Value), "", RsTemp.Fields("MRR_REF_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColBillNo
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_REF_NO").Value), "", RsTemp.Fields("SUPP_REF_NO").Value)

                SprdMain.Col = ColBillDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SUPP_REF_DATE").Value), "", RsTemp.Fields("SUPP_REF_DATE").Value), "DD/MM/YYYY")
                mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SUPP_REF_DATE").Value), "", RsTemp.Fields("SUPP_REF_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColRefNo
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("DNCN_REF_NO").Value), "", RsTemp.Fields("DNCN_REF_NO").Value)

                SprdMain.Col = ColRefDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DNCN_REF_DATE").Value), "", RsTemp.Fields("DNCN_REF_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                SprdMain.Col = ColHSNCode
                SprdMain.Text = IIf(IsDBNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value)

                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = IIf(IsDBNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)
                End If
                SprdMain.Text = mItemDesc ''

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColPONo
                SprdMain.Text = CStr(IIf(IsDBNull(RsTemp.Fields("REF_PO_NO").Value), "", RsTemp.Fields("REF_PO_NO").Value))

                mPONo = IIf(IsDBNull(RsTemp.Fields("REF_PO_NO").Value), "", RsTemp.Fields("REF_PO_NO").Value)
                SprdMain.Col = ColPORate
                SqlStr = "SELECT GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(mPONo) & ",'" & mItemCode & "') AS PORate " & vbCrLf & " FROM DUAL"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOPrice, ADODB.LockTypeEnum.adLockReadOnly)
                If RsPOPrice.EOF = False Then
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPOPrice.Fields("PORATE").Value), "", RsPOPrice.Fields("PORATE").Value)))
                End If
                SprdMain.Col = ColBillRate
                SprdMain.Text = CStr(GetBillRate(mPurNo, mPurDate, Trim(.Fields("ITEM_CODE").Value)))
                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))
                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))
                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))
                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))
                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_AMOUNT").Value), 0, .Fields("CGST_AMOUNT").Value)))
                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_AMOUNT").Value), 0, .Fields("SGST_AMOUNT").Value)))
                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_AMOUNT").Value), 0, .Fields("IGST_AMOUNT").Value)))
                .MoveNext()
                I = I + 1
                '            SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowPurDetail1(ByRef mMkey As String, ByRef mFYear As String, ByRef mPurVNo As String, ByRef mPurVDate As String, ByRef mMRRNO As Double, ByRef mMRRDate As String, ByRef mBillNo As String, ByRef mBillDate As String, ByRef mPartyCode As String, ByRef pPurchaseType As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset
        Dim mPORate As Double
        Dim mBillRate As Double
        Dim mRateDiff As Double
        Dim mQty As Double
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
        Dim mSupplierName As String
        Dim mAcctCode As String

        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mSupplierName = Trim(txtDebitAccount.Text)
        Else
            mSupplierName = Trim(txtCreditAccount.Text)
        End If

        If MainClass.ValidateWithMasterTable(mSupplierName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAcctCode = Trim(MasterNo)
        End If

        mLocal = GetPartyBusinessDetail(Trim(mAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        'If MainClass.ValidateWithMasterTable(mSupplierName, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = Trim(MasterNo)
        'Else
        '    mLocal = "N"
        'End If
        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(mSupplierName, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = Trim(MasterNo)
        'End If
        ''mReOfferQty = GetReofferQty(RsPurDetail!AUTO_KEY_MRR, mItemCode)


        '' mISGST = IIf(IsDBNull(.Fields("ISGSTAPPLICABLE").Value), "W", .Fields("ISGSTAPPLICABLE").Value)

        SqlStr = ""
        SqlStr = " SELECT IH.ISGSTAPPLICABLE, ID.*, GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & "," & mMRRNO & ",TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mPartyCode & "',ITEM_CODE) AS REOFFER_QTY," & vbCrLf _
            & " GetITEMPRICE_NEW(" & Val(mFYear) & ", " & RsCompany.Fields("FYEAR").Value & ", TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CUST_REF_NO,ITEM_CODE) AS PORate " & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID" & vbCrLf _
            & " Where IH.Mkey=ID.Mkey AND IH.Mkey='" & mMkey & "'" & vbCrLf _
            & " Order By ID.SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            If mFillData = False Then
                I = 1
            Else
                I = SprdMain.MaxRows
            End If
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I
                SprdMain.Col = ColPURMkey
                SprdMain.Text = mMkey
                SprdMain.Col = ColPurNO
                SprdMain.Text = mPurVNo
                SprdMain.Col = ColPurDATE
                SprdMain.Text = VB6.Format(mPurVDate, "DD/MM/YYYY")
                SprdMain.Col = ColMRRNo
                SprdMain.Text = Str(mMRRNO)
                SprdMain.Col = ColMRRDate
                SprdMain.Text = VB6.Format(mMRRDate, "DD/MM/YYYY")
                SprdMain.Col = ColBillNo
                SprdMain.Text = Trim(mBillNo)
                SprdMain.Col = ColBillDate
                SprdMain.Text = VB6.Format(mBillDate, "DD/MM/YYYY")
                SprdMain.Col = ColRefNo
                SprdMain.Text = Trim(mPurVNo)
                SprdMain.Col = ColRefDate
                SprdMain.Text = VB6.Format(mPurVDate, "DD/MM/YYYY")
                SprdMain.Col = ColItemCode
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)
                SprdMain.Col = ColHSNCode
                SprdMain.Text = IIf(IsDBNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value)

                SprdMain.Col = ColBillQty
                SprdMain.Text = CDbl(VB6.Format(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)), "0.000"))

                SprdMain.Col = ColQty
                If lblDCType.Text = "S" Then
                    mQty = CDbl(VB6.Format(Val(IIf(IsDBNull(.Fields("SHORTAGE_QTY").Value), 0, .Fields("SHORTAGE_QTY").Value)), "0.000"))
                ElseIf lblDCType.Text = "R" Then
                    mQty = CDbl(VB6.Format(Val(IIf(IsDBNull(.Fields("REJECTED_QTY").Value), 0, .Fields("REJECTED_QTY").Value)), "0.000"))
                ElseIf lblDCType.Text = "P" Or lblDCType.Text = "A" Or lblDCType.Text = "V" Then
                    mQty = CDbl(VB6.Format(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)), "0.000"))
                Else
                    mQty = CDbl(VB6.Format(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)), "0.000"))
                End If
                SprdMain.Text = VB6.Format(mQty, "0.000")
                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Col = ColPONo
                SprdMain.Text = IIf(IsDBNull(.Fields("CUST_REF_NO").Value), "", .Fields("CUST_REF_NO").Value)
                SprdMain.Col = ColPORate
                '            SprdMain.Text = GetPurchaseOrderRate(Val(txtPONo.Text), txtMRRDate.Text, Trim(.Fields("ITEM_CODE").Value))
                mPORate = Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value))
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value)))
                SprdMain.Col = ColBillRate
                mBillRate = GetBillRate(mPurVNo, mPurVDate, Trim(.Fields("ITEM_CODE").Value))
                SprdMain.Text = VB6.Format(mBillRate, "0.0000")
                SprdMain.Col = ColRate
                If lblDCType.Text = "S" Then
                    mRateDiff = CDbl(VB6.Format(System.Math.Abs(mBillRate), "0.0000"))
                ElseIf lblDCType.Text = "R" Then
                    mRateDiff = CDbl(VB6.Format(System.Math.Abs(mBillRate), "0.0000"))
                ElseIf lblDCType.Text = "P" Or lblDCType.Text = "A" Or lblDCType.Text = "V" Then
                    mRateDiff = CDbl(VB6.Format(System.Math.Abs(mBillRate - mPORate), "0.0000"))
                Else
                    mRateDiff = CDbl(VB6.Format(System.Math.Abs(mBillRate), "0.0000"))
                End If
                SprdMain.Text = CStr(mRateDiff) 'Val(IIf(IsNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value))
                SprdMain.Col = ColAmount
                SprdMain.Text = VB6.Format(mQty * mRateDiff, "0.0000") ' Val(IIf(IsNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value))
                If pPurchaseType = "S" Or pPurchaseType = "W" Then
                    '                pPurchaseType
                    '                GST_CREDITAPP
                    '                GST_RCAPP
                    '                GST_EXEMPTED
                    If .Fields("ISGSTAPPLICABLE").Value = "I" Then
                        cboGSTStatus.SelectedIndex = 2
                    ElseIf .Fields("GST_CREDITAPP").Value = "Y" Then
                        cboGSTStatus.SelectedIndex = 0
                    Else
                        cboGSTStatus.SelectedIndex = 1
                    End If
                    '                If .Fields("GST_RCAPP").Value = "Y" Then
                    '                    cboGSTStatus.ListIndex = 6
                    '                ElseIf .Fields("GST_EXEMPTED").Value = "Y" Then
                    '                    cboGSTStatus.ListIndex = 2
                    '                ElseIf .Fields("GST_CREDITAPP").Value = "Y" Then
                    '                    cboGSTStatus.ListIndex = 0
                    '                End If
                    '
                    '                If mISGST = "G" Then
                    '                    cboGSTStatus.ListIndex = 0
                    '                ElseIf mISGST = "R" Then
                    '                    cboGSTStatus.ListIndex = 1
                    '                Else
                    '                    cboGSTStatus.ListIndex = 2
                    '                End If
                End If
                If cboGSTStatus.SelectedIndex = 1 Then
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(0, "0.00")
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(0, "0.00")
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(0, "0.00")
                Else

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                End If
                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function DuplicateDatainGrid(ByRef mCheckRefNo As String) As Boolean
        Dim cntRow As Integer
        'Dim mCheckRefNo As String
        Dim mRefNo As String
        DuplicateDatainGrid = False
        If Trim(mCheckRefNo) = "" Then Exit Function
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColRefNo
                mRefNo = Trim(.Text)
                If mCheckRefNo = mRefNo Then
                    DuplicateDatainGrid = True
                    Exit For
                End If
            Next
        End With
    End Function
    Private Sub ShowDNCNDetail1(ByRef mMkey As String, ByRef mRefNo As String, ByRef mRefDate As String, ByRef mMRRNO As Double, ByRef mMRRDate As String, ByRef mBillNo As String, ByRef mBillDate As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset
        Dim mPurNo As String
        Dim mPurDate As String
        SqlStr = ""
        SqlStr = " SELECT FIN_DNCN_DET.*, " & vbCrLf & " GetITEMPRICE_NEW(1,1,SUPP_REF_DATE,DECODE(SUBSTR(REF_PO_NO,1,1),'S',-1,REF_PO_NO),ITEM_CODE) AS PORate " & vbCrLf & " FROM FIN_DNCN_DET " & vbCrLf & " Where Mkey='" & mMkey & "'" & vbCrLf & " Order By SubRowNo"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = False Then
                FormatSprdMain(-1)
                If mFillData = False Then
                    I = 1
                Else
                    I = SprdMain.MaxRows
                End If
                '            I = 1
                .MoveFirst()
                Do While Not .EOF
                    SprdMain.Row = I
                    SprdMain.Col = ColPURMkey
                    SprdMain.Text = IIf(IsDBNull(.Fields("PURMKEY").Value), "-1", .Fields("PURMKEY").Value)
                    SprdMain.Col = ColPurNO
                    SprdMain.Text = IIf(IsDBNull(.Fields("PURVNO").Value), "-1", .Fields("PURVNO").Value)
                    mPurNo = Trim(SprdMain.Text)
                    SprdMain.Col = ColPurDATE
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PURVDATE").Value), "", .Fields("PURVDATE").Value), "DD/MM/YYYY")
                    SprdMain.Col = ColMRRNo
                    SprdMain.Text = Str(IIf(IsDBNull(.Fields("MRR_REF_NO").Value), "-1", .Fields("MRR_REF_NO").Value)) ''Str(mMRRNO)
                    mPurDate = Trim(SprdMain.Text)
                    SprdMain.Col = ColMRRDate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("MRR_REF_DATE").Value), "", .Fields("MRR_REF_DATE").Value), "DD/MM/YYYY") ''Format(mMRRDATE, "DD/MM/YYYY")
                    SprdMain.Col = ColBillNo
                    SprdMain.Text = IIf(IsDBNull(.Fields("SUPP_REF_NO").Value), "", .Fields("SUPP_REF_NO").Value)
                    SprdMain.Col = ColBillDate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SUPP_REF_DATE").Value), "", .Fields("SUPP_REF_DATE").Value), "DD/MM/YYYY")
                    SprdMain.Col = ColRefNo
                    SprdMain.Text = Trim(mRefNo)
                    SprdMain.Col = ColRefDate
                    SprdMain.Text = VB6.Format(mRefDate, "DD/MM/YYYY")
                    SprdMain.Col = ColItemCode
                    SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)
                    SprdMain.Col = ColHSNCode
                    SprdMain.Text = IIf(IsDBNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value)

                    SprdMain.Col = ColBillQty
                    SprdMain.Text = CDbl(VB6.Format(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)), "0.000"))


                    SprdMain.Col = ColQty
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))
                    SprdMain.Col = ColUnit
                    SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                    SprdMain.Col = ColPONo
                    SprdMain.Text = IIf(IsDBNull(.Fields("REF_PO_NO").Value), "", .Fields("REF_PO_NO").Value)
                    SprdMain.Col = ColPORate
                    '            SprdMain.Text = GetPurchaseOrderRate(Val(txtPONo.Text), txtMRRDate.Text, Trim(.Fields("ITEM_CODE").Value))
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value)))
                    SprdMain.Col = ColBillRate
                    SprdMain.Text = CStr(GetBillRate(mPurNo, mPurDate, Trim(.Fields("ITEM_CODE").Value)))
                    SprdMain.Col = ColRate
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))
                    SprdMain.Col = ColAmount
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))
                    .MoveNext()
                    I = I + 1
                    SprdMain.MaxRows = I
                Loop
            Else
                txtBillNo.Text = Trim(mBillNo)
                txtBillDate.Text = VB6.Format(mBillDate, "DD/MM/YYYY")
            End If
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        AdoDCMain.Refresh

            UltraGrid1.Focus()

            'FormatSprdView()
            'SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsDNCNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
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
        Dim pCGSTAmount As Double
        Dim pSGSTAmount As Double
        Dim pIGSTAmount As Double
        Dim pNetCGSTAmount As Double
        Dim pNetSGSTAmount As Double
        Dim pNetIGSTAmount As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim xAccountName As String
        Dim xAcctCode As String
        Dim mRefType As String
        Dim mRGPNo As Double
        Dim mHSNCode As String
        Dim mLocal As String = "N"
        Dim mGSTCatType As String
        Dim mExpName As String
        Dim mIsTaxable As String
        Dim mOtherTaxableAmount As Double
        Dim mGSTableAmount As Double

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            xAccountName = Trim(txtDebitAccount.Text)
        Else
            xAccountName = Trim(txtCreditAccount.Text)
        End If
        If MainClass.ValidateWithMasterTable(xAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        End If

        mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")

        mPartyGSTNo = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        If Val(txtMRRNo.Text) > 0 Then
            If MainClass.ValidateWithMasterTable(Val(txtMRRNo.Text), "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mRefType = Trim(MasterNo)
            End If
        End If

        'If MainClass.ValidateWithMasterTable(xAccountName, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If
        pRound = 0
        mQty = 0
        mRate = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mItemValue = 0
        pCGSTAmount = 0
        pSGSTAmount = 0
        pIGSTAmount = 0
        mOtherTaxableAmount = 0

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

        mTotItemAmount = 0

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0

                .Col = ColItemCode
                mItemCode = .Text
                .Col = ColQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty
                .Col = ColRate
                mRate = Val(.Text)
                .Text = CStr(mRate)
                .Col = ColAmount
                .Text = CStr(mQty * mRate)
                mItemAmount = CDbl(VB6.Format(mQty * mRate, "0.00")) ''(mQty * mRate) '- mDiscount
                mTotItemAmount = mTotItemAmount + mItemAmount
            Next I
        End With

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc
                .Col = ColItemCode
                'If .Text = "" Then GoTo DontCalc
                mItemCode = .Text

                .Col = ColHSNCode
                If .Text = "" Then GoTo DontCalc

                .Col = ColQty
                mQty = Val(.Text)
                'mTotQty = mTotQty + mQty
                .Col = ColRate
                mRate = Val(.Text)
                .Text = CStr(mRate)
                .Col = ColAmount
                .Text = CStr(mQty * mRate)
                mItemAmount = CDbl(VB6.Format(mQty * mRate, "0.00")) ''(mQty * mRate) '- mDiscount

                If mTotItemAmount > 0 Then
                    mGSTableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00"))
                End If

                'mTotItemAmount = mTotItemAmount + mItemAmount
                mItemValue = CDbl(VB6.Format(mQty * mRate, "0.00"))
                If VB.Left(cboGSTStatus.Text, 1) = "W" Then
                    pCGSTPer = 0
                    pSGSTPer = 0
                    pIGSTPer = 0
                Else


                    SprdMain.Col = ColHSNCode
                    If mRefType = "R" Then
                        'SprdMain.Col = ColPONo
                        'mRGPNo = SprdMain.Text

                        'If mRGPNo <= 0 Then
                        '    If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "REF_PO_NO", "INV_GATE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & Trim(mItemCode) & "'") = True Then
                        '        mRGPNo = CDbl(Trim(MasterNo))
                        '    End If
                        'End If
                        'If MainClass.ValidateWithMasterTable(mRGPNo, "AUTO_KEY_PASSNO", "SAC_CODE", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        '    mHSNCode = CDbl(Trim(MasterNo))
                        'End If

                        mHSNCode = SprdMain.Text
                        mHSNCode = Trim(SprdMain.Text)

                        If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                            If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ERR1
                        Else
                            pCGSTPer = 0
                            pSGSTPer = 0
                            pIGSTPer = 0
                        End If

                    Else
                        mHSNCode = SprdMain.Text

                        mHSNCode = Trim(SprdMain.Text)

                        If ADDMode = True Then
                            mGSTCatType = "G"
                            If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "CODETYPE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mGSTCatType = Trim(MasterNo)
                            End If

                            If mGSTCatType = "G" Then
                                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, Mid(cboGSTStatus.Text, 1, 1), mPartyGSTNo) = False Then GoTo ERR1
                            Else
                                If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ERR1
                            End If

                            .Col = ColCGSTPer
                            .Text = Val(pCGSTPer)

                            .Col = ColSGSTPer
                            .Text = Val(pSGSTPer)

                            .Col = ColIGSTPer
                            .Text = Val(pIGSTPer)
                        End If
                    End If


                    .Col = ColCGSTPer
                    pCGSTPer = Val(.Text)
                    .Col = ColSGSTPer
                    pSGSTPer = Val(.Text)
                    .Col = ColIGSTPer
                    pIGSTPer = Val(.Text)
                End If


                pCGSTAmount = CDbl(VB6.Format(System.Math.Round(mGSTableAmount * pCGSTPer * 0.01, 2), "0.00"))
                pSGSTAmount = CDbl(VB6.Format(System.Math.Round(mGSTableAmount * pSGSTPer * 0.01, 2), "0.00"))
                pIGSTAmount = CDbl(VB6.Format(System.Math.Round(mGSTableAmount * pIGSTPer * 0.01, 2), "0.00"))
                pNetCGSTAmount = pNetCGSTAmount + pCGSTAmount
                pNetSGSTAmount = pNetSGSTAmount + pSGSTAmount
                pNetIGSTAmount = pNetIGSTAmount + pIGSTAmount

                .Col = ColCGSTAmount
                .Text = VB6.Format(pCGSTAmount, "0.00")
                .Col = ColSGSTAmount
                .Text = VB6.Format(pSGSTAmount, "0.00")
                .Col = ColIGSTAmount
                .Text = VB6.Format(pIGSTAmount, "0.00")
DontCalc:
            Next I
        End With
        mNetAccessAmt = Val(CStr(mTotItemAmount))
        '    mExciseableAmount = Val(mTotItemAmount)
        '    mTaxableAmount = Val(mTotItemAmount)
        '    Call BillExpensesCalcTots(SprdExp, txtBillDate.Text, False, mNetAccessAmt, mExciseableAmount, mTaxableAmount, _
        ''                                mCEDCessAble, mADDCessAble, mCESSableAmount, mTotItemAmount, _
        ''                                pTotExciseDuty, pTotEduCess, pTotSHECess, pTotADE, pTotExportExp, pTotOthers, _
        ''                                pTotSalesTax, pTotSurcharge, pTotCustomDuty, pTotAddCess, pTotCustomDutyExport, pTotCustomDutyCess, _
        ''                                pTotMSC, pTotDiscount, pTotServiceTax, pTotRO, pTotTCS, mTotExp, pEDPer, pSTPer, pServPer, pCessPer, pSHECPer, pTCSPer, "D", mNetAccessAmt, pTotKKCAmount)
        Call BillExpensesCalcTots_GST(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTotItemAmount, 0, 0, 0, pNetIGSTAmount, pNetSGSTAmount, pNetCGSTAmount, 0, 0, 0, pTotOthers, 0, 0, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, 0, "D")
        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        lblTotCGSTAmount.Text = VB6.Format(pNetCGSTAmount, "#0.00")
        lblTotSGSTAmount.Text = VB6.Format(pNetSGSTAmount, "#0.00")
        lblTotIGSTAmount.Text = VB6.Format(pNetIGSTAmount, "#0.00")
        lblTotFreight.Text = VB6.Format(pTotOthers, "#0.00")
        lblTotCharges.Text = CStr(0) ''Format(mRO, "#0.00")
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")
        txtCGSTRefundAmount.Text = CStr(0)
        txtSGSTRefundAmount.Text = CStr(0)
        txtIGSTRefundAmount.Text = CStr(0)
        If Trim(mCompanyGSTNo) = Trim(mPartyGSTNo) Then
            lblNetAmount.Text = VB6.Format(System.Math.Abs(mTotExp + mTotItemAmount), "#0.00")
        Else
            If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                lblNetAmount.Text = VB6.Format(System.Math.Abs(mTotExp + pNetCGSTAmount + pNetSGSTAmount + pNetIGSTAmount + mTotItemAmount), "#0.00")
                If VB.Left(cboGSTStatus.Text, 1) = "I" Then
                    txtCGSTRefundAmount.Text = CStr(0)
                    txtSGSTRefundAmount.Text = CStr(0)
                    txtIGSTRefundAmount.Text = CStr(0)
                Else
                    txtCGSTRefundAmount.Text = CStr(Val(lblTotCGSTAmount.Text))
                    txtSGSTRefundAmount.Text = CStr(Val(lblTotSGSTAmount.Text))
                    txtIGSTRefundAmount.Text = CStr(Val(lblTotIGSTAmount.Text))
                End If
            Else
                lblNetAmount.Text = VB6.Format(System.Math.Abs(mTotExp + mTotItemAmount), "#0.00")
                txtCGSTRefundAmount.Text = CStr(0)
                txtSGSTRefundAmount.Text = CStr(0)
                txtIGSTRefundAmount.Text = CStr(0)
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function GetCessAbleAmt() As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mExpName As String
        Dim pCessAbleAmt As Double
        Dim mExpAddDeduct As String
        With SprdExp
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColExpName
                mExpName = Trim(.Text)
                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)
                .Col = ColExpAmt
                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "CESSABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        pCessAbleAmt = pCessAbleAmt + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                    End If
                End If
            Next
        End With
        GetCessAbleAmt = pCessAbleAmt
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetCessAbleAmt = 0
    End Function
    Private Sub Clear1()
        LblMKey.Text = ""
        pShowCalc = False
        '    If LblBookCode.text = ConDebitNoteBookCode Then
        '        txtVNoPrefix.Text = "D"
        '    Else
        '        txtVNoPrefix.Text = "C"
        '    End If
        chkCancelled.Enabled = False
        txtVNoPrefix.Text = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")
        txtVType.Text = IIf(Trim(txtVType.Text) = "", GetVType, Trim(txtVType.Text))
        txtVNo.Text = ""
        txtVNoSuffix.Text = ""
        txtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtPartyDNNo.Text = ""
        txtPartyDNDate.Text = ""
        txtRecdDate.Text = ""
        txtPurVNo.Text = ""
        txtPurVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtMRRNo.Text = ""
        txtMRRDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        lblPurVNO.Text = "Pur. VNo :"
        txtDebitAccount.Text = ""
        txtCreditAccount.Text = ""
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAproved.Enabled = True
        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True
        cboPopulateFrom.Enabled = True
        cmdBillNoSearch.Enabled = False
        cboPopulateFrom.SelectedIndex = -1
        lblTotQty.Text = "0.00"
        lblTotItemValue.Text = "0.00"
        '    lblTotED.text = "0.00"
        '    lblTotST.text = "0.00"
        lblTotCGSTAmount.Text = "0.00"
        lblTotSGSTAmount.Text = "0.00"
        lblTotIGSTAmount.Text = "0.00"
        lblNetAmount.Text = "0.00"
        txtReason.Text = ""
        txtNarration.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = ""
        If lblDCType.Text = "S" Then
            txtReason.Text = "SHORTAGE"
        ElseIf lblDCType.Text = "P" Then
            txtReason.Text = "PO RATE DIFF."
        ElseIf lblDCType.Text = "A" Then
            txtReason.Text = "AMEND PO RATE DIFF."
        ElseIf lblDCType.Text = "D" Then
            txtReason.Text = "DISCOUNT"
        ElseIf lblDCType.Text = "V" Then
            txtReason.Text = "VOLUME DISCOUNT"
        ElseIf lblDCType.Text = "O" Then
            txtReason.Text = " "
        ElseIf lblDCType.Text = "R" Then
            txtReason.Text = "REJECTION"
        End If
        cboPopulateFrom.Enabled = True
        If lblDCType.Text = "R" Then
            txtMRRDate.Enabled = False
            txtMRRNo.Enabled = True
            cmdMRRNoSearch.Enabled = True
            txtPurVDate.Enabled = True
            txtPurVNo.Enabled = True
            cmdVNoSearch.Enabled = True
        Else
            txtPurVDate.Enabled = False
            txtPurVNo.Enabled = True
            cmdVNoSearch.Enabled = True
            txtMRRDate.Enabled = False
            txtMRRNo.Enabled = False
            cmdMRRNoSearch.Enabled = False
        End If
        txtBillNo.Enabled = True ' False
        txtBillDate.Enabled = True ' False
        cmdBillNoSearch.Enabled = True ' False
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        '    lblTotST.text = Format(0, "#0.00")
        '    lblTotED.text = Format(0, "#0.00")
        lblTotCGSTAmount.Text = VB6.Format(0, "#0.00")
        lblTotSGSTAmount.Text = VB6.Format(0, "#0.00")
        lblTotIGSTAmount.Text = VB6.Format(0, "#0.00")
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotFreight.Text = VB6.Format(0, "#0.00")
        lblTotCharges.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")
        lblDiscount.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        lblMSC.Text = VB6.Format(0, "#0.00")
        txtDebitAccount.Enabled = True
        txtCGSTRefundAmount.Text = ""
        txtSGSTRefundAmount.Text = ""
        txtIGSTRefundAmount.Text = ""


        cboGSTStatus.SelectedIndex = -1

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
            If lblDCType.Text = "P" Then
                cboGSTStatus.SelectedIndex = 1
            End If
        End If

        cboGSTStatus.Enabled = True
        '    chkGST.Enabled = IIf(lblDCType.text = "R", True, False)
        '    cmdPopulate.Enabled = True
        lblPayDate.Text = ""
        mFillData = False

        txtBillTo.Text = ""
        txtBillTo.Enabled = True    ' IIf(lblDCType.Text = "O", True, False)
        cmdBillToSearch.Enabled = True    '  IIf(lblDCType.Text = "O", True, False)

        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        FraPostingDtl.Visible = False
        MainClass.ClearGrid(SprdPostingDetail)
        Call FormatSprdPostingDetail(-1)
        pShowCalc = True
        MainClass.ButtonStatus(Me, XRIGHT, RsDNCNMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FillSprdExp()
        On Error GoTo ERR1
        Dim mLocal As String
        Dim mAccountName As String
        Dim RS As ADODB.Recordset
        Dim I As Integer
        Dim mAcctCode As String
        pShowCalc = False
        MainClass.ClearGrid(SprdExp)
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mAccountName = Trim(txtDebitAccount.Text)
        Else
            mAccountName = Trim(txtCreditAccount.Text)
        End If
        If Trim(mAccountName) <> "" Then
            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAcctCode = Trim(MasterNo)
            End If

            mLocal = GetPartyBusinessDetail(Trim(mAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")

            '    If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLocal = IIf(MasterNo = "Y", "L", "C")
            '    Else
            '        mLocal = ""
            '    End If
            'Else
            '    mLocal = ""
        End If
        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        '    If OptDCType(3).Value = True Then
        '        sqlstr = sqlstr & vbCrLf & " AND (Type='S' OR Type='B') Order By PrintSequence"
        '    Else
        SqlStr = SqlStr & vbCrLf & " AND (Type='P' OR Type='B') Order By PrintSequence"
        '    End If
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
                    SprdExp.Text = Str(IIf(IsDBNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                Else
                    SprdExp.Text = ""
                End If
                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"
                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDBNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))
                SprdExp.Col = ColExpAddDeduct
                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)
                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)
                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)
                If mLocal <> "" Then
                    If RS.Fields("Identification").Value = "ST" Then
                        If RS.Fields("STTYPE").Value = mLocal Then
                            SprdExp.RowHidden = False
                        Else
                            SprdExp.RowHidden = True
                        End If
                    End If
                End If
                RS.MoveNext()
                If RS.EOF = False Then
                    SprdExp.MaxRows = SprdExp.MaxRows + 1
                End If
            Loop
        End If
        pShowCalc = False
        FormatSprdExp(-1)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        Resume
    End Sub
    Private Sub FrmDrCrNoteGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmDrCrNoteGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub
    Private Sub FrmDrCrNoteGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
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
        '
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245) '8000
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900
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
        'AdoDCMain.Visible = False
        FormActive = False
        Call FrmDrCrNoteGST_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
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
                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " and Name= '" & m_Exp & "'"
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
        End Select
        'Call DistributeExpInMainGrid
        Call CalcTots()
        Exit Sub
ErrPart:
        SprdExp.Col = ESCol
        SprdExp.Col2 = ESCol
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
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub txtCreditAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditAccount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCreditAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditAccount.DoubleClick
        On Error GoTo ErrPart
        If MainClass.SearchGridMaster((txtCreditAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCreditAccount.Text = AcName
            'txtMRRNo_Validate False
            txtCreditAccount.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtCreditAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCreditAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCreditAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCreditAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCreditAccount_DoubleClick(txtCreditAccount, New System.EventArgs())
    End Sub
    Private Sub txtCreditAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCreditAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mAccountName As String
        Dim mLocal As String
        Dim mAcctCode As String

        If Trim(txtCreditAccount.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            Cancel = True
            ErrorMsg("Please Enter the Valid Credit Account.", "", MsgBoxStyle.Critical)
        End If
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mAccountName = Trim(txtDebitAccount.Text)
        Else
            mAccountName = Trim(txtCreditAccount.Text)
        End If
        If Trim(mAccountName) <> "" Then
            If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAcctCode = Trim(MasterNo)
            End If
            mLocal = GetPartyBusinessDetail(Trim(mAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            mLocal = IIf(MasterNo = "Y", "L", "C")
            'If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDebitAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebitAccount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDebitAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDebitAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDebitAccount.Text)
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
    Private Function GetExicseAbleAmt() As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mExpName As String
        Dim pExicseAbleAmt As Double
        Dim mExpAddDeduct As String
        With SprdExp
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColExpName
                mExpName = Trim(.Text)
                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)
                .Col = ColExpAmt
                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "EXCISEABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        pExicseAbleAmt = pExicseAbleAmt + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                    End If
                End If
            Next
        End With
        GetExicseAbleAmt = pExicseAbleAmt + Val(lblTotItemValue.Text)
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetExicseAbleAmt = 0
    End Function
    Private Function GetSTAbleAmt() As Double
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mExpName As String
        Dim pSTAbleAmt As Double
        Dim mExpAddDeduct As String
        With SprdExp
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColExpName
                mExpName = Trim(.Text)
                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)
                .Col = ColExpAmt
                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        pSTAbleAmt = pSTAbleAmt + (Val(.Text) * IIf(mExpAddDeduct = "D", -1, 1))
                    End If
                End If
            Next
        End With
        GetSTAbleAmt = pSTAbleAmt + Val(lblTotItemValue.Text)
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSTAbleAmt = 0
    End Function
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
    Private Sub txtVNoPrefix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNoPrefix.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function GetPayDate(ByRef pVNo As String, ByRef pVDate As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        GetPayDate = ""
        SqlStr = "SELECT PAYMENTDATE FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND VNO='" & pVNo & "'" & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(pVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetPayDate = IIf(IsDBNull(RsTemp.Fields("PAYMENTDATE").Value), "", RsTemp.Fields("PAYMENTDATE").Value)
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub txtVNoSuffix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNoSuffix.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function GetVType(Optional ByRef mChkVtype As String = "") As String
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        SqlStr = "SELECT VTYPE FROM FIN_VOUCHERTYPE_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & mBookType & "'"
        If mChkVtype <> "" Then
            SqlStr = SqlStr & " AND VTYPE='" & MainClass.AllowSingleQuote(Trim(mChkVtype)) & "'"
        End If
        If mBookType = "E" Then
            If lblDCType.Text = "R" Then
                SqlStr = SqlStr & " AND VTYPE='DR'"
            ElseIf lblDCType.Text = "O" Or lblDCType.Text = "D" Then
                SqlStr = SqlStr & " AND VTYPE='DA'"
            Else
                SqlStr = SqlStr & " AND VTYPE NOT IN ('DR','DA')"
            End If
        Else
            If lblDCType.Text = "R" Then
                SqlStr = SqlStr & " AND VTYPE='CR'"
            ElseIf lblDCType.Text = "O" Or lblDCType.Text = "D" Then
                SqlStr = SqlStr & " AND VTYPE='CA'"
            Else
                SqlStr = SqlStr & " AND VTYPE NOT IN ('CR','CA')"
            End If
        End If
        SqlStr = SqlStr & "ORDER BY FOR_HO DESC, VTYPE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            GetVType = ""
        Else
            GetVType = IIf(IsDBNull(RS.Fields("VTYPE").Value), "", RS.Fields("VTYPE").Value)
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetVType = ""
    End Function
    Private Function CheckVType() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        If ConOnlineData = True Then
            CheckVType = True
            Exit Function
        End If
        CheckVType = False
        SqlStr = "SELECT VTYPE,VNAME FROM FIN_VOUCHERTYPE_MST  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BOOKTYPE='" & mBookType & "'" & vbCrLf & " AND VTYPE='" & MainClass.AllowSingleQuote(Trim(txtVType.Text)) & "'" & vbCrLf & " ORDER BY VTYPE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            CheckVType = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckVType = False
    End Function
    Private Function GetBillNo(ByRef pBillNo As String, ByRef pBillDate As String, ByRef mDNFROM As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim mTableName As String
        pBillNo = ""
        pBillDate = ""
        GetBillNo = False
        If mDNFROM = "P" Then
            mTableName = "FIN_PURCHASE_HDR"
        Else
            mTableName = "FIN_DNCN_HDR"
        End If
        SqlStr = "SELECT BILLNO, INVOICE_DATE FROM " & mTableName & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND VNO='" & txtPurVNo.Text & "'" & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(txtPurVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            pBillNo = IIf(IsDBNull(RS.Fields("BILLNO").Value), "", RS.Fields("BILLNO").Value)
            pBillDate = IIf(IsDBNull(RS.Fields("INVOICE_DATE").Value), "", RS.Fields("INVOICE_DATE").Value)
            GetBillNo = True
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetBillNo = False
    End Function
    Private Sub txtVType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVType.DoubleClick
        SearchVType()
    End Sub
    Private Sub txtVType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVType()
    End Sub
    Private Sub TxtVType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If GetVType(Trim(txtVType.Text)) = "" Then
            Cancel = True
            ErrorMsg("Invalid Voucher Type", "", MsgBoxStyle.Information)
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SearchVType()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & mBookType & "'"
        If MainClass.SearchGridMaster((txtVType.Text), "FIN_VOUCHERTYPE_MST", "VTYPE", "VNAME", , , SqlStr) = True Then
            txtVType.Text = AcName
            '        txtTariff_Validate False
            If txtVType.Enabled = True Then txtVType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchBillNo()
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mAccountCode As String
        Dim mAccountName As String
        Dim mDivisionCode As Double
        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            mAccountName = txtDebitAccount.Text
        Else
            mAccountName = txtCreditAccount.Text
        End If
        If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            MsgBox("Please Enter Valid Account", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        SqlStr = SqlStr & vbCrLf & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "' AND BILLTYPE ='B' AND DIV_CODE=" & mDivisionCode & ""
        If MainClass.SearchGridMaster(txtBillNo.Text, "FIN_POSTED_TRN", "BILLNO", "BILLDATE", "LOCATION_ID", , SqlStr) = True Then
            txtBillNo.Text = AcName
            txtBillDate.Text = AcName1
            txtBillTo.Text = AcName2
            If txtBillNo.Enabled = True Then txtBillNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtBillTo_Validating(sender As Object, e As CancelEventArgs) Handles txtBillTo.Validating
        Dim Cancel As Boolean = e.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            If Trim(txtDebitAccount.Text) = "" Then GoTo EventExitSub
            If Trim(txtDebitAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xAcctCode = MasterNo
                End If
            End If
        Else
            If Trim(txtCreditAccount.Text) = "" Then GoTo EventExitSub
            If Trim(txtCreditAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xAcctCode = MasterNo
                End If
            End If
        End If

        If Trim(txtBillTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
            MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        e.Cancel = Cancel
    End Sub

    Private Sub cmdBillToSearch_Click(sender As Object, e As EventArgs) Handles cmdBillToSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If CDbl(LblBookCode.Text) = ConDebitNoteBookCode Then
            If Trim(txtDebitAccount.Text) = "" Then
                MsgInformation("Please select the Customer First")
                Exit Sub
            End If
            If Trim(txtDebitAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xAcctCode = MasterNo
                End If
            End If
        Else
            If Trim(txtCreditAccount.Text) = "" Then
                MsgInformation("Please select the Customer First")
                Exit Sub
            End If
            If Trim(txtCreditAccount.Text) <> "" Then
                If MainClass.ValidateWithMasterTable((txtCreditAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xAcctCode = MasterNo
                End If
            End If
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

    Private Sub FrmDrCrNoteGST_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))

        Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        'SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame3.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
