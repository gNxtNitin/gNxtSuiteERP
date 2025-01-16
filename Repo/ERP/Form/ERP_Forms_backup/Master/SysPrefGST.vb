Option Strict Off
Option Explicit On
Friend Class frmSysPrefGST
    Inherits System.Windows.Forms.Form

    Dim XRIGHT As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim PostTaxWiseSale As Byte
    Dim PostSaleAcCode As Integer

    Private Const ConRowHeight As Short = 12

    Private Const ColFromAccountName As Short = 1
    Private Const ColToAccountName As Short = 2
   Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdcancel.Click
        Me.Hide()
        Me.Close()
      'frmSysPrefGST = Nothing
   End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo err_Renamed
        Dim xCode As Integer
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If FieldVerification() = False Then Exit Sub

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCode = RsTemp.Fields("COMPANY_CODE").Value
                If Update1(xCode) = False Then GoTo err_Renamed
                cmdSave.Enabled = False
                RsTemp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        RsCompany.Requery() ''.Refresh		

        Exit Sub
err_Renamed:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''		
        RsCompany.Requery() ''.Refresh		



    End Sub
    Private Sub frmSysPrefGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
      '		
      Call SetMainFormCordinate(Me)

      SSTab1.SelectedIndex = 0
      ''Set PvtDBCn = New ADODB.Connection		
      ''PvtDBCn.Open StrConn		
      XRIGHT = MainClass.STRMenuRight(PubUserID, 1, myMenu, PubDBCn)
      MainClass.RightsToButton(Me, XRIGHT)
      SetMaxLength()
      MainClass.SetControlsColor(Me)
      Me.Left = 0
      Me.Top = 0


      ADDMode = False
      MODIFYMode = False
      If XRIGHT <> "" Then MODIFYMode = True
      Show1()
   End Sub
   Private Sub SetMaxLength()
      '		

      Dim mAccountLength As Integer


      mAccountLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

      txtPurCGST.Maxlength = mAccountLength
      txtPurSGST.Maxlength = mAccountLength
      txtPurIGST.Maxlength = mAccountLength
      txtSaleCGST.Maxlength = mAccountLength
      txtSaleSGST.Maxlength = mAccountLength
      txtSaleIGST.Maxlength = mAccountLength
      txtSaleRetCGST.Maxlength = mAccountLength
      txtSaleRetSGST.Maxlength = mAccountLength
      txtSaleRetIGST.Maxlength = mAccountLength
      txtCRCGST.Maxlength = mAccountLength
      txtCRSGST.Maxlength = mAccountLength
      txtCRIGST.Maxlength = mAccountLength
      txtDRCGST.Maxlength = mAccountLength
      txtDRSGST.Maxlength = mAccountLength
      txtDRIGST.Maxlength = mAccountLength

      txtRejDRCGST.Maxlength = mAccountLength
      txtRejDRSGST.Maxlength = mAccountLength
      txtRejDRIGST.Maxlength = mAccountLength


      txtAdvPayCGST.Maxlength = mAccountLength
      txtAdvPaySGST.Maxlength = mAccountLength
      txtAdvPayIGST.Maxlength = mAccountLength

      txtAdvReceiptCGST.Maxlength = mAccountLength
      txtAdvReceiptSGST.Maxlength = mAccountLength
      txtAdvReceiptIGST.Maxlength = mAccountLength

      txtPurRCCGST.Maxlength = mAccountLength
      txtPurRCSGST.Maxlength = mAccountLength
      txtPurRCIGST.Maxlength = mAccountLength
      txtSaleRCCGST.Maxlength = mAccountLength
      txtSaleRCSGST.Maxlength = mAccountLength
      txtSaleRCIGST.Maxlength = mAccountLength

      txtCompanyAcctName.Maxlength = mAccountLength
      txteCCashLedger.Maxlength = mAccountLength
      txteSCashLedger.Maxlength = mAccountLength
      txteICashLedger.Maxlength = mAccountLength

      txteRCCCashLedger.Maxlength = mAccountLength
      txteRCSCashLedger.Maxlength = mAccountLength
      txteRCICashLedger.Maxlength = mAccountLength

      txtGSTLatePayment.Maxlength = mAccountLength
      txtGSTInterestPayment.Maxlength = mAccountLength
      txtInvPrefix.Maxlength = MainClass.SetMaxLength("INVOICE_PREFIX", "FIN_PRINT_MST", PubDBCn)

   End Sub
   Private Sub Show1()
      On Error GoTo ERR1

      ShowDetail1()

      CmdSave.Enabled = False
      Exit Sub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Sub
   Sub ShowDetail1()
      On Error GoTo ERR1

      Dim mPurCGST As String
      Dim mPurSGST As String
      Dim mPurIGST As String
      Dim mSaleCGST As String
      Dim mSaleSGST As String
      Dim mSaleIGST As String
      Dim mSaleRetCGST As String
      Dim mSaleRetSGST As String
      Dim mSaleRetIGST As String
      Dim mCRCGST As String
      Dim mCRSGST As String
      Dim mCRIGST As String
      Dim mDRCGST As String
      Dim mDRSGST As String
      Dim mDRIGST As String
      Dim mGSTSeparate As String
      Dim mCompanyAcctName As String
      Dim mCompanyAcctCode As String
      Dim mAdvPayCGST As String
      Dim mAdvPaySGST As String
      Dim mAdvPayIGST As String
      Dim mAdvReceiptCGST As String
      Dim mAdvReceiptSGST As String
      Dim mAdvReceiptIGST As String
      Dim mPurRCCGST As String
      Dim mPurRCSGST As String
      Dim mPurRCIGST As String
      Dim mSaleRCCGST As String
      Dim mSaleRCSGST As String
      Dim mSaleRCIGST As String
      Dim meCashLedgerCode As String
      Dim mGSTLatePayment As String
      Dim mGSTInterestPayment As String
      Dim mRejDRCGST As String
      Dim mRejDRSGST As String
      Dim mRejDRIGST As String

        mPurCGST = IIf(IsDBNull(RsCompany.Fields("CGST_REFUNDCODE").Value), "", RsCompany.Fields("CGST_REFUNDCODE").Value)
        If MainClass.ValidateWithMasterTable(mPurCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPurCGST.Text = MasterNo
        Else
            txtPurCGST.Text = ""
        End If

        mPurSGST = IIf(IsDBNull(RsCompany.Fields("SGST_REFUNDCODE").Value), "", RsCompany.Fields("SGST_REFUNDCODE").Value)
        If MainClass.ValidateWithMasterTable(mPurSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPurSGST.Text = MasterNo
        Else
            txtPurSGST.Text = ""
        End If

        mPurIGST = IIf(IsDBNull(RsCompany.Fields("IGST_REFUNDCODE").Value), "", RsCompany.Fields("IGST_REFUNDCODE").Value)
        If MainClass.ValidateWithMasterTable(mPurIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPurIGST.Text = MasterNo
        Else
            txtPurIGST.Text = ""
        End If

        mSaleCGST = IIf(IsDBNull(RsCompany.Fields("CGST_SALECODE").Value), "", RsCompany.Fields("CGST_SALECODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleCGST.Text = MasterNo
        Else
            txtSaleCGST.Text = ""
        End If

        mSaleSGST = IIf(IsDBNull(RsCompany.Fields("SGST_SALECODE").Value), "", RsCompany.Fields("SGST_SALECODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleSGST.Text = MasterNo
        Else
            txtSaleSGST.Text = ""
        End If

        mSaleIGST = IIf(IsDBNull(RsCompany.Fields("IGST_SALECODE").Value), "", RsCompany.Fields("IGST_SALECODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleIGST.Text = MasterNo
        Else
            txtSaleIGST.Text = ""
        End If

        mCRCGST = IIf(IsDBNull(RsCompany.Fields("CGST_CR_RETURNCODE").Value), "", RsCompany.Fields("CGST_CR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mCRCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCRCGST.Text = MasterNo
        Else
            txtCRCGST.Text = ""
        End If

        mCompanyAcctCode = IIf(IsDBNull(RsCompany.Fields("COMPANY_ACCTCODE").Value), "", RsCompany.Fields("COMPANY_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(mCompanyAcctCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCompanyAcctName.Text = MasterNo
        Else
            txtCompanyAcctName.Text = ""
        End If

        meCashLedgerCode = IIf(IsDBNull(RsCompany.Fields("E_CGSTLEDGER_ACCTCODE").Value), "", RsCompany.Fields("E_CGSTLEDGER_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(meCashLedgerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txteCCashLedger.Text = MasterNo
        Else
            txteCCashLedger.Text = ""
        End If

        meCashLedgerCode = IIf(IsDBNull(RsCompany.Fields("E_SGSTLEDGER_ACCTCODE").Value), "", RsCompany.Fields("E_SGSTLEDGER_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(meCashLedgerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txteSCashLedger.Text = MasterNo
        Else
            txteSCashLedger.Text = ""
        End If

        meCashLedgerCode = IIf(IsDBNull(RsCompany.Fields("E_IGSTLEDGER_ACCTCODE").Value), "", RsCompany.Fields("E_IGSTLEDGER_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(meCashLedgerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txteICashLedger.Text = MasterNo
        Else
            txteICashLedger.Text = ""
        End If

        meCashLedgerCode = IIf(IsDBNull(RsCompany.Fields("E_RCCGSTLEDGER_ACCTCODE").Value), "", RsCompany.Fields("E_RCCGSTLEDGER_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(meCashLedgerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txteRCCCashLedger.Text = MasterNo
        Else
            txteRCCCashLedger.Text = ""
        End If

        meCashLedgerCode = IIf(IsDBNull(RsCompany.Fields("E_RCSGSTLEDGER_ACCTCODE").Value), "", RsCompany.Fields("E_RCSGSTLEDGER_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(meCashLedgerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txteRCSCashLedger.Text = MasterNo
        Else
            txteRCSCashLedger.Text = ""
        End If

        meCashLedgerCode = IIf(IsDBNull(RsCompany.Fields("E_RCIGSTLEDGER_ACCTCODE").Value), "", RsCompany.Fields("E_RCIGSTLEDGER_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(meCashLedgerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txteRCICashLedger.Text = MasterNo
        Else
            txteRCICashLedger.Text = ""
        End If



        '    txtGSTLatePayment
        'txtGSTInterestPayment

        mGSTLatePayment = IIf(IsDBNull(RsCompany.Fields("GST_LATE_ACCTCODE").Value), "", RsCompany.Fields("GST_LATE_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(mGSTLatePayment, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtGSTLatePayment.Text = MasterNo
        Else
            txtGSTLatePayment.Text = ""
        End If

        mGSTInterestPayment = IIf(IsDBNull(RsCompany.Fields("GST_INTEREST_ACCTCODE").Value), "", RsCompany.Fields("GST_INTEREST_ACCTCODE").Value)
        If MainClass.ValidateWithMasterTable(mGSTInterestPayment, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtGSTInterestPayment.Text = MasterNo
        Else
            txtGSTInterestPayment.Text = ""
        End If


        mCRSGST = IIf(IsDBNull(RsCompany.Fields("SGST_CR_RETURNCODE").Value), "", RsCompany.Fields("SGST_CR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mCRSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCRSGST.Text = MasterNo
        Else
            txtCRSGST.Text = ""
        End If

        mCRIGST = IIf(IsDBNull(RsCompany.Fields("IGST_CR_RETURNCODE").Value), "", RsCompany.Fields("IGST_CR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mCRIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCRIGST.Text = MasterNo
        Else
            txtCRIGST.Text = ""
        End If

        mSaleRetCGST = IIf(IsDBNull(RsCompany.Fields("CGST_SALE_RETURNCODE").Value), "", RsCompany.Fields("CGST_SALE_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleRetCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleRetCGST.Text = MasterNo
        Else
            txtSaleRetCGST.Text = ""
        End If

        mSaleRetSGST = IIf(IsDBNull(RsCompany.Fields("SGST_SALE_RETURNCODE").Value), "", RsCompany.Fields("SGST_SALE_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleRetSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleRetSGST.Text = MasterNo
        Else
            txtSaleRetSGST.Text = ""
        End If

        mSaleRetIGST = IIf(IsDBNull(RsCompany.Fields("IGST_SALE_RETURNCODE").Value), "", RsCompany.Fields("IGST_SALE_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleRetIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleRetIGST.Text = MasterNo
        Else
            txtSaleRetIGST.Text = ""
        End If

        mDRCGST = IIf(IsDBNull(RsCompany.Fields("CGST_DR_RETURNCODE").Value), "", RsCompany.Fields("CGST_DR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mDRCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDRCGST.Text = MasterNo
        Else
            txtDRCGST.Text = ""
        End If

        mDRSGST = IIf(IsDBNull(RsCompany.Fields("SGST_DR_RETURNCODE").Value), "", RsCompany.Fields("SGST_DR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mDRSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDRSGST.Text = MasterNo
        Else
            txtDRSGST.Text = ""
        End If

        mDRIGST = IIf(IsDBNull(RsCompany.Fields("IGST_DR_RETURNCODE").Value), "", RsCompany.Fields("IGST_DR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mDRIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDRIGST.Text = MasterNo
        Else
            txtDRIGST.Text = ""
        End If

        mAdvPayCGST = IIf(IsDBNull(RsCompany.Fields("CGST_ADV_PAYMENTCODE").Value), "", RsCompany.Fields("CGST_ADV_PAYMENTCODE").Value)
        If MainClass.ValidateWithMasterTable(mAdvPayCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAdvPayCGST.Text = MasterNo
        Else
            txtAdvPayCGST.Text = ""
        End If

        mAdvPaySGST = IIf(IsDBNull(RsCompany.Fields("SGST_ADV_PAYMENTCODE").Value), "", RsCompany.Fields("SGST_ADV_PAYMENTCODE").Value)
        If MainClass.ValidateWithMasterTable(mAdvPaySGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAdvPaySGST.Text = MasterNo
        Else
            txtAdvPaySGST.Text = ""
        End If

        mAdvPayIGST = IIf(IsDBNull(RsCompany.Fields("IGST_ADV_PAYMENTCODE").Value), "", RsCompany.Fields("IGST_ADV_PAYMENTCODE").Value)
        If MainClass.ValidateWithMasterTable(mAdvPayIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAdvPayIGST.Text = MasterNo
        Else
            txtAdvPayIGST.Text = ""
        End If

        mAdvReceiptCGST = IIf(IsDBNull(RsCompany.Fields("CGST_ADV_RECEIPTCODE").Value), "", RsCompany.Fields("CGST_ADV_RECEIPTCODE").Value)
        If MainClass.ValidateWithMasterTable(mAdvReceiptCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAdvReceiptCGST.Text = MasterNo
        Else
            txtAdvReceiptCGST.Text = ""
        End If

        mAdvReceiptSGST = IIf(IsDBNull(RsCompany.Fields("SGST_ADV_RECEIPTCODE").Value), "", RsCompany.Fields("SGST_ADV_RECEIPTCODE").Value)
        If MainClass.ValidateWithMasterTable(mAdvReceiptSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAdvReceiptSGST.Text = MasterNo
        Else
            txtAdvReceiptSGST.Text = ""
        End If

        mAdvReceiptIGST = IIf(IsDBNull(RsCompany.Fields("IGST_ADV_RECEIPTCODE").Value), "", RsCompany.Fields("IGST_ADV_RECEIPTCODE").Value)
        If MainClass.ValidateWithMasterTable(mAdvReceiptIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAdvReceiptIGST.Text = MasterNo
        Else
            txtAdvReceiptIGST.Text = ""
        End If

        txtInvPrefix.Text = IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value)
        txtInvPrefix.Enabled = IIf(Trim(txtInvPrefix.Text) = "", True, False)
        mGSTSeparate = IIf(IsDBNull(RsCompany.Fields("GST_SEPARATE").Value), "N", RsCompany.Fields("GST_SEPARATE").Value)

        If mGSTSeparate = "Y" Then
            optPurEntry(0).Checked = True
        Else
            optPurEntry(1).Checked = True
        End If

        mPurRCCGST = IIf(IsDBNull(RsCompany.Fields("CGST_RC_REFUNDCODE").Value), "", RsCompany.Fields("CGST_RC_REFUNDCODE").Value)
        If MainClass.ValidateWithMasterTable(mPurRCCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPurRCCGST.Text = MasterNo
        Else
            txtPurRCCGST.Text = ""
        End If

        mPurRCSGST = IIf(IsDBNull(RsCompany.Fields("SGST_RC_REFUNDCODE").Value), "", RsCompany.Fields("SGST_RC_REFUNDCODE").Value)
        If MainClass.ValidateWithMasterTable(mPurRCSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPurRCSGST.Text = MasterNo
        Else
            txtPurRCSGST.Text = ""
        End If

        mPurRCIGST = IIf(IsDBNull(RsCompany.Fields("IGST_RC_REFUNDCODE").Value), "", RsCompany.Fields("IGST_RC_REFUNDCODE").Value)
        If MainClass.ValidateWithMasterTable(mPurRCIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtPurRCIGST.Text = MasterNo
        Else
            txtPurRCIGST.Text = ""
        End If

        mSaleRCCGST = IIf(IsDBNull(RsCompany.Fields("CGST_RC_SALECODE").Value), "", RsCompany.Fields("CGST_RC_SALECODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleRCCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleRCCGST.Text = MasterNo
        Else
            txtSaleRCCGST.Text = ""
        End If

        mSaleRCSGST = IIf(IsDBNull(RsCompany.Fields("SGST_RC_SALECODE").Value), "", RsCompany.Fields("SGST_RC_SALECODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleRCSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleRCSGST.Text = MasterNo
        Else
            txtSaleRCSGST.Text = ""
        End If

        mSaleRCIGST = IIf(IsDBNull(RsCompany.Fields("IGST_RC_SALECODE").Value), "", RsCompany.Fields("IGST_RC_SALECODE").Value)
        If MainClass.ValidateWithMasterTable(mSaleRCIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSaleRCIGST.Text = MasterNo
        Else
            txtSaleRCIGST.Text = ""
        End If

        mRejDRCGST = IIf(IsDBNull(RsCompany.Fields("CGST_REJDR_RETURNCODE").Value), "", RsCompany.Fields("CGST_REJDR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mRejDRCGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtRejDRCGST.Text = MasterNo
        Else
            txtRejDRCGST.Text = ""
        End If

        mRejDRSGST = IIf(IsDBNull(RsCompany.Fields("SGST_REJDR_RETURNCODE").Value), "", RsCompany.Fields("SGST_REJDR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mRejDRSGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtRejDRSGST.Text = MasterNo
        Else
            txtRejDRSGST.Text = ""
        End If

        mRejDRIGST = IIf(IsDBNull(RsCompany.Fields("IGST_REJDR_RETURNCODE").Value), "", RsCompany.Fields("IGST_REJDR_RETURNCODE").Value)
        If MainClass.ValidateWithMasterTable(mRejDRIGST, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtRejDRIGST.Text = MasterNo
        Else
            txtRejDRIGST.Text = ""
        End If
        If PubUserID = "G0416" Then

      Else
         txtPurCGST.Enabled = IIf(Trim(txtPurCGST.Text) = "", True, False)
         txtPurSGST.Enabled = IIf(Trim(txtPurSGST.Text) = "", True, False)
         txtPurIGST.Enabled = IIf(Trim(txtPurIGST.Text) = "", True, False)
         txtSaleCGST.Enabled = IIf(Trim(txtSaleCGST.Text) = "", True, False)
         txtSaleSGST.Enabled = IIf(Trim(txtSaleSGST.Text) = "", True, False)
         txtSaleIGST.Enabled = IIf(Trim(txtSaleIGST.Text) = "", True, False)
         txtSaleRetCGST.Enabled = IIf(Trim(txtSaleRetCGST.Text) = "", True, False)
         txtSaleRetSGST.Enabled = IIf(Trim(txtSaleRetSGST.Text) = "", True, False)
         txtSaleRetIGST.Enabled = IIf(Trim(txtSaleRetIGST.Text) = "", True, False)
         txtDRCGST.Enabled = IIf(Trim(txtDRCGST.Text) = "", True, False)
         txtDRSGST.Enabled = IIf(Trim(txtDRSGST.Text) = "", True, False)
         txtDRIGST.Enabled = IIf(Trim(txtDRIGST.Text) = "", True, False)
         txtPurRCCGST.Enabled = IIf(Trim(txtPurRCCGST.Text) = "", True, False)
         txtPurRCSGST.Enabled = IIf(Trim(txtPurRCSGST.Text) = "", True, False)
         txtPurRCIGST.Enabled = IIf(Trim(txtPurRCIGST.Text) = "", True, False)
         txtSaleRCCGST.Enabled = IIf(Trim(txtSaleRCCGST.Text) = "", True, False)
         txtSaleRCSGST.Enabled = IIf(Trim(txtSaleRCSGST.Text) = "", True, False)
         txtSaleRCIGST.Enabled = IIf(Trim(txtSaleRCIGST.Text) = "", True, False)
         txtAdvPayCGST.Enabled = IIf(Trim(txtAdvPayCGST.Text) = "", True, False)
         txtAdvPaySGST.Enabled = IIf(Trim(txtAdvPaySGST.Text) = "", True, False)
         txtAdvPayIGST.Enabled = IIf(Trim(txtAdvPayIGST.Text) = "", True, False)
         txtAdvReceiptCGST.Enabled = IIf(Trim(txtAdvReceiptCGST.Text) = "", True, False)
         txtAdvReceiptSGST.Enabled = IIf(Trim(txtAdvReceiptSGST.Text) = "", True, False)
         txtAdvReceiptIGST.Enabled = IIf(Trim(txtAdvReceiptIGST.Text) = "", True, False)
         txtCompanyAcctName.Enabled = IIf(Trim(txtCompanyAcctName.Text) = "", True, False)

         txteCCashLedger.Enabled = IIf(Trim(txteCCashLedger.Text) = "", True, False)
         txteSCashLedger.Enabled = IIf(Trim(txteSCashLedger.Text) = "", True, False)
         txteICashLedger.Enabled = IIf(Trim(txteICashLedger.Text) = "", True, False)

         txteRCCCashLedger.Enabled = IIf(Trim(txteRCCCashLedger.Text) = "", True, False)
         txteRCSCashLedger.Enabled = IIf(Trim(txteRCSCashLedger.Text) = "", True, False)
         txteRCICashLedger.Enabled = IIf(Trim(txteRCICashLedger.Text) = "", True, False)

         txtGSTLatePayment.Enabled = IIf(Trim(txtGSTLatePayment.Text) = "", True, False)
         txtGSTInterestPayment.Enabled = IIf(Trim(txtGSTInterestPayment.Text) = "", True, False)

         txtRejDRCGST.Enabled = IIf(Trim(txtRejDRCGST.Text) = "", True, False)
         txtRejDRSGST.Enabled = IIf(Trim(txtRejDRSGST.Text) = "", True, False)
         txtRejDRIGST.Enabled = IIf(Trim(txtRejDRIGST.Text) = "", True, False)

      End If

      Exit Sub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
      '    Resume		
   End Sub

    Private Function Update1(xCode As Integer) As Boolean
        '		
        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim mPurCGST As String = ""
        Dim mPurSGST As String = ""
        Dim mPurIGST As String = ""
        Dim mSaleCGST As String = ""
        Dim mSaleSGST As String = ""
        Dim mSaleIGST As String = ""
        Dim mSaleRetCGST As String = ""
        Dim mSaleRetSGST As String = ""
        Dim mSaleRetIGST As String = ""
        Dim mCRCGST As String = ""
        Dim mCRSGST As String = ""
        Dim mCRIGST As String = ""
        Dim mDRCGST As String = ""
        Dim mDRSGST As String = ""
        Dim mDRIGST As String = ""

        Dim mAddMode As Boolean
        Dim mGSTSeparate As String = ""
        Dim mCompanyAcctName As String = ""
        Dim mCompanyAcctCode As String = ""

        Dim mAdvPayCGST As String = ""
        Dim mAdvPaySGST As String = ""
        Dim mAdvPayIGST As String = ""
        Dim mAdvReceiptCGST As String = ""
        Dim mAdvReceiptSGST As String = ""
        Dim mAdvReceiptIGST As String = ""

        Dim mPurRCCGST As String = ""
        Dim mPurRCSGST As String = ""
        Dim mPurRCIGST As String = ""
        Dim mSaleRCCGST As String = ""
        Dim mSaleRCSGST As String = ""
        Dim mSaleRCIGST As String = ""
        Dim meCashLedger As String = ""
        Dim meSCashLedger As String = ""
        Dim meICashLedger As String = ""

        Dim meRCCashLedger As String = ""
        Dim meRCSCashLedger As String = ""
        Dim meRCICashLedger As String = ""

        Dim mGSTLatePayment As String = ""
        Dim mGSTInterestPayment As String = ""
        Dim mRejDRCGST As String = ""
        Dim mRejDRSGST As String = ""
        Dim mRejDRIGST As String = ""

        'PubDBCn.Errors.Clear()
        'PubDBCn.BeginTrans()

        SqlStr = ""

        If MainClass.ValidateWithMasterTable(xCode, "Company_Code", "Company_Code", "FIN_PRINT_MST", PubDBCn, MasterNo) = True Then
            mAddMode = False
        Else
            mAddMode = True
        End If

        If MainClass.ValidateWithMasterTable(txtPurCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mPurCGST = MasterNo
        Else
            mPurCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtPurSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mPurSGST = MasterNo
        Else
            mPurSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtPurIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mPurIGST = MasterNo
        Else
            mPurIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleCGST = MasterNo
        Else
            mSaleCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleSGST = MasterNo
        Else
            mSaleSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleIGST = MasterNo
        Else
            mSaleIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleRetCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleRetCGST = MasterNo
        Else
            mSaleRetCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleRetSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleRetSGST = MasterNo
        Else
            mSaleRetSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleRetIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleRetIGST = MasterNo
        Else
            mSaleRetIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtCRCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mCRCGST = MasterNo
        Else
            mCRCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtCompanyAcctName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mCompanyAcctCode = MasterNo
        Else
            mCompanyAcctCode = ""
        End If

        If MainClass.ValidateWithMasterTable(txteCCashLedger.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            meCashLedger = MasterNo
        Else
            meCashLedger = ""
        End If

        If MainClass.ValidateWithMasterTable(txteSCashLedger.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            meSCashLedger = MasterNo
        Else
            meSCashLedger = ""
        End If

        If MainClass.ValidateWithMasterTable(txteICashLedger.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            meICashLedger = MasterNo
        Else
            meICashLedger = ""
        End If

        If MainClass.ValidateWithMasterTable(txteRCCCashLedger.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            meRCCashLedger = MasterNo
        Else
            meRCCashLedger = ""
        End If

        If MainClass.ValidateWithMasterTable(txteRCSCashLedger.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            meRCSCashLedger = MasterNo
        Else
            meRCSCashLedger = ""
        End If

        If MainClass.ValidateWithMasterTable(txteRCICashLedger.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            meRCICashLedger = MasterNo
        Else
            meRCICashLedger = ""
        End If


        If MainClass.ValidateWithMasterTable(txtGSTLatePayment.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mGSTLatePayment = MasterNo
        Else
            mGSTLatePayment = ""
        End If

        If MainClass.ValidateWithMasterTable(txtGSTInterestPayment.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mGSTInterestPayment = MasterNo
        Else
            mGSTInterestPayment = ""
        End If


        If MainClass.ValidateWithMasterTable(txtCRSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mCRSGST = MasterNo
        Else
            mCRSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtCRIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mCRIGST = MasterNo
        Else
            mCRIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtDRCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mDRCGST = MasterNo
        Else
            mDRCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtDRSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mDRSGST = MasterNo
        Else
            mDRSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtDRIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mDRIGST = MasterNo
        Else
            mDRIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtAdvPayCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mAdvPayCGST = MasterNo
        Else
            mAdvPayCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtAdvPaySGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mAdvPaySGST = MasterNo
        Else
            mAdvPaySGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtAdvPayIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mAdvPayIGST = MasterNo
        Else
            mAdvPayIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtAdvReceiptCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mAdvReceiptCGST = MasterNo
        Else
            mAdvReceiptCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtAdvReceiptSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mAdvReceiptSGST = MasterNo
        Else
            mAdvReceiptSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtAdvReceiptIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mAdvReceiptIGST = MasterNo
        Else
            mAdvReceiptIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtPurRCCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mPurRCCGST = MasterNo
        Else
            mPurRCCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtPurRCSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mPurRCSGST = MasterNo
        Else
            mPurRCSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtPurRCIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mPurRCIGST = MasterNo
        Else
            mPurRCIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleRCCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleRCCGST = MasterNo
        Else
            mSaleRCCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleRCSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleRCSGST = MasterNo
        Else
            mSaleRCSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSaleRCIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mSaleRCIGST = MasterNo
        Else
            mSaleRCIGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtRejDRCGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mRejDRCGST = MasterNo
        Else
            mRejDRCGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtRejDRSGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mRejDRSGST = MasterNo
        Else
            mRejDRSGST = ""
        End If

        If MainClass.ValidateWithMasterTable(txtRejDRIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCode & "") = True Then
            mRejDRIGST = MasterNo
        Else
            mRejDRIGST = ""
        End If

        mGSTSeparate = IIf(optPurEntry(0).Checked = True, "Y", "N")

        ''INVOICE_PREFIX = '" & Trim(txtInvPrefix.Text) & "',

        If mAddMode = True Then
            SqlStr = "INSERT INTO FIN_PRINT_MST ( " & vbCrLf _
                & " COMPANY_CODE, " & vbCrLf _
                & " CGST_REFUNDCODE, SGST_REFUNDCODE, IGST_REFUNDCODE, " & vbCrLf _
                & " CGST_SALECODE, SGST_SALECODE, IGST_SALECODE," & vbCrLf _
                & " CGST_CR_RETURNCODE, SGST_CR_RETURNCODE, IGST_CR_RETURNCODE," & vbCrLf _
                & " CGST_SALE_RETURNCODE, SGST_SALE_RETURNCODE, IGST_SALE_RETURNCODE," & vbCrLf _
                & " CGST_DR_RETURNCODE, SGST_DR_RETURNCODE, IGST_DR_RETURNCODE, " & vbCrLf _
                & " GST_SEPARATE, INVOICE_PREFIX, COMPANY_ACCTCODE, " & vbCrLf _
                & " CGST_ADV_PAYMENTCODE, SGST_ADV_PAYMENTCODE, IGST_ADV_PAYMENTCODE, " & vbCrLf _
                & " CGST_ADV_RECEIPTCODE, SGST_ADV_RECEIPTCODE, IGST_ADV_RECEIPTCODE, " & vbCrLf _
                & " CGST_RC_REFUNDCODE, SGST_RC_REFUNDCODE, IGST_RC_REFUNDCODE, " & vbCrLf _
                & " CGST_RC_SALECODE, SGST_RC_SALECODE, IGST_RC_SALECODE"

            SqlStr = SqlStr & " ) VALUES ( "


            SqlStr = SqlStr & vbCrLf _
                & " " & xCode & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mPurCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mPurSGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mPurIGST)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mSaleCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mSaleSGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mSaleIGST)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mCRCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mCRSGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mCRIGST)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mSaleRetCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mSaleRetSGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mSaleRetIGST)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mDRCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mDRSGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mDRIGST)) & "', " & vbCrLf _
                & " '" & mGSTSeparate & "', '" & Trim(txtInvPrefix.Text) & "', '" & MainClass.AllowSingleQuote(Trim(txtCompanyAcctName.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mAdvPayCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mAdvPaySGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mAdvPayIGST)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mAdvReceiptCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mAdvReceiptSGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mAdvReceiptIGST)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mPurRCCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mPurRCSGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mPurRCIGST)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(Trim(mSaleRCCGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mSaleRCSGST)) & "', '" & MainClass.AllowSingleQuote(Trim(mSaleRCIGST)) & "' "

            SqlStr = SqlStr & " )"

        Else
            SqlStr = "UPDATE  FIN_PRINT_MST SET " & vbCrLf _
                & " CGST_REFUNDCODE = '" & MainClass.AllowSingleQuote(Trim(mPurCGST)) & "'," & vbCrLf _
                & " SGST_REFUNDCODE = '" & MainClass.AllowSingleQuote(Trim(mPurSGST)) & "', " & vbCrLf _
                & " IGST_REFUNDCODE = '" & MainClass.AllowSingleQuote(Trim(mPurIGST)) & "'," & vbCrLf _
                & " CGST_SALECODE = '" & MainClass.AllowSingleQuote(Trim(mSaleCGST)) & "', " & vbCrLf _
                & " SGST_SALECODE = '" & MainClass.AllowSingleQuote(Trim(mSaleSGST)) & "', " & vbCrLf _
                & " IGST_SALECODE = '" & MainClass.AllowSingleQuote(Trim(mSaleIGST)) & "'," & vbCrLf _
                & " CGST_CR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mCRCGST)) & "', " & vbCrLf _
                & " SGST_CR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mCRSGST)) & "'," & vbCrLf _
                & " IGST_CR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mCRIGST)) & "', " & vbCrLf _
                & " CGST_SALE_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mSaleRetCGST)) & "', " & vbCrLf _
                & " SGST_SALE_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mSaleRetSGST)) & "'," & vbCrLf _
                & " IGST_SALE_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mSaleRetIGST)) & "', " & vbCrLf _
                & " CGST_DR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mDRCGST)) & "'," & vbCrLf _
                & " SGST_DR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mDRSGST)) & "', " & vbCrLf _
                & " IGST_DR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mDRIGST)) & "', " & vbCrLf _
                & " GST_SEPARATE = '" & mGSTSeparate & "',  " & vbCrLf _
                & " CGST_ADV_PAYMENTCODE = '" & MainClass.AllowSingleQuote(Trim(mAdvPayCGST)) & "', " & vbCrLf _
                & " SGST_ADV_PAYMENTCODE = '" & MainClass.AllowSingleQuote(Trim(mAdvPaySGST)) & "', " & vbCrLf _
                & " IGST_ADV_PAYMENTCODE = '" & MainClass.AllowSingleQuote(Trim(mAdvPayIGST)) & "'," & vbCrLf _
                & " CGST_ADV_RECEIPTCODE = '" & MainClass.AllowSingleQuote(Trim(mAdvReceiptCGST)) & "', " & vbCrLf _
                & " SGST_ADV_RECEIPTCODE = '" & MainClass.AllowSingleQuote(Trim(mAdvReceiptSGST)) & "', " & vbCrLf _
                & " IGST_ADV_RECEIPTCODE = '" & MainClass.AllowSingleQuote(Trim(mAdvReceiptIGST)) & "', "

            SqlStr = SqlStr & vbCrLf _
                & " CGST_RC_REFUNDCODE = '" & MainClass.AllowSingleQuote(Trim(mPurRCCGST)) & "', " & vbCrLf _
                & " SGST_RC_REFUNDCODE = '" & MainClass.AllowSingleQuote(Trim(mPurRCSGST)) & "'," & vbCrLf _
                & " IGST_RC_REFUNDCODE = '" & MainClass.AllowSingleQuote(Trim(mPurRCIGST)) & "'," & vbCrLf _
                & " CGST_RC_SALECODE = '" & MainClass.AllowSingleQuote(Trim(mSaleRCCGST)) & "'," & vbCrLf _
                & " SGST_RC_SALECODE = '" & MainClass.AllowSingleQuote(Trim(mSaleRCSGST)) & "'," & vbCrLf _
                & " IGST_RC_SALECODE = '" & MainClass.AllowSingleQuote(Trim(mSaleRCIGST)) & "'," & vbCrLf _
                & " E_CGSTLEDGER_ACCTCODE = '" & MainClass.AllowSingleQuote(Trim(meCashLedger)) & "'," & vbCrLf _
                & " E_SGSTLEDGER_ACCTCODE = '" & MainClass.AllowSingleQuote(Trim(meSCashLedger)) & "'," & vbCrLf _
                & " E_IGSTLEDGER_ACCTCODE = '" & MainClass.AllowSingleQuote(Trim(meICashLedger)) & "'," & vbCrLf _
                & " GST_LATE_ACCTCODE = '" & MainClass.AllowSingleQuote(Trim(mGSTLatePayment)) & "'," & vbCrLf _
                & " GST_INTEREST_ACCTCODE = '" & MainClass.AllowSingleQuote(Trim(mGSTInterestPayment)) & "'," & vbCrLf _
                & " E_RCCGSTLEDGER_ACCTCODE = '" & MainClass.AllowSingleQuote(Trim(meRCCashLedger)) & "'," & vbCrLf _
                & " E_RCSGSTLEDGER_ACCTCODE = '" & MainClass.AllowSingleQuote(Trim(meRCSCashLedger)) & "'," & vbCrLf _
                & " E_RCIGSTLEDGER_ACCTCODE = '" & MainClass.AllowSingleQuote(Trim(meRCICashLedger)) & "'," & vbCrLf

            SqlStr = SqlStr & vbCrLf _
                & " CGST_REJDR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mRejDRCGST)) & "', " & vbCrLf _
                & " SGST_REJDR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mRejDRSGST)) & "'," & vbCrLf _
                & " IGST_REJDR_RETURNCODE = '" & MainClass.AllowSingleQuote(Trim(mRejDRIGST)) & "'"


            SqlStr = SqlStr & vbCrLf _
                & " WHERE Company_Code=" & xCode & ""



        End If

        PubDBCn.Execute(SqlStr)

        If xCode = RsCompany.Fields("COMPANY_CODE").Value Then

            SqlStr = "UPDATE  FIN_PRINT_MST SET " & vbCrLf _
                        & " COMPANY_ACCTCODE='" & MainClass.AllowSingleQuote(Trim(mCompanyAcctCode)) & "' " & vbCrLf _
                        & " WHERE Company_Code=" & xCode & ""

            PubDBCn.Execute(SqlStr)

        End If

        'PubDBCn.CommitTrans()
        Update1 = True
        RsCompany.Requery() ''.Refresh		

        Exit Function
err_Renamed:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Update1 = False
        'PubDBCn.RollbackTrans() ''		
        RsCompany.Requery() ''.Refresh		


    End Function
    Private Sub frmSysPrefGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        frmSysPref = Nothing
    End Sub
    Private Sub SearchACM(ByRef pTabName As String, ByRef pFldName As String, ByRef pText As System.Windows.Forms.TextBox, ByRef pCondSTR As String)
    End Sub

    Private Function FieldVerification() As Boolean
        On Error GoTo ERR1
        FieldVerification = True


        Exit Function
ERR1:
        '    Resume		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub SearchAccount(ByRef mTextBox As System.Windows.Forms.TextBox)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('O','C')"
        If MainClass.SearchGridMaster(mTextBox.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", , , , SqlStr) = True Then
            mTextBox.Text = AcName
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



   Private Sub optPurEntry_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPurEntry.CheckedChanged
      If eventSender.Checked Then
         Dim Index As Short = optPurEntry.GetIndex(eventSender)
         '		
         MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
      End If
   End Sub

   Private Sub txtAdvPayCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvPayCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtAdvPayCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvPayCGST.DoubleClick
      Call SearchAccount(txtAdvPayCGST)
   End Sub
   Private Sub txtAdvPayCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvPayCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtAdvPayCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtAdvPayCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvPayCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtAdvPayCGST)
   End Sub
   Private Sub txtAdvPayCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvPayCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtAdvPayCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtAdvPaySGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvPaySGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtAdvPaySGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvPaySGST.DoubleClick
      Call SearchAccount(txtAdvPaySGST)
   End Sub
   Private Sub txtAdvPaySGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvPaySGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtAdvPaySGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtAdvPaySGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvPaySGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtAdvPaySGST)
   End Sub
   Private Sub txtAdvPaySGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvPaySGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtAdvPaySGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtAdvPayIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvPayIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtAdvPayIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvPayIGST.DoubleClick
      Call SearchAccount(txtAdvPayIGST)
   End Sub
   Private Sub txtAdvPayIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvPayIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtAdvPayIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtAdvPayIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvPayIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtAdvPayIGST)
   End Sub
   Private Sub txtAdvPayIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvPayIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtAdvPayIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtAdvReceiptCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvReceiptCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtAdvReceiptCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvReceiptCGST.DoubleClick
      Call SearchAccount(txtAdvReceiptCGST)
   End Sub
   Private Sub txtAdvReceiptCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvReceiptCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtAdvReceiptCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtAdvReceiptCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvReceiptCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtAdvReceiptCGST)
   End Sub
   Private Sub txtAdvReceiptCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvReceiptCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtAdvReceiptCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtAdvReceiptSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvReceiptSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtAdvReceiptSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvReceiptSGST.DoubleClick
      Call SearchAccount(txtAdvReceiptSGST)
   End Sub
   Private Sub txtAdvReceiptSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvReceiptSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtAdvReceiptSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtAdvReceiptSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvReceiptSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtAdvReceiptSGST)
   End Sub
   Private Sub txtAdvReceiptSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvReceiptSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtAdvReceiptSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtAdvReceiptIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvReceiptIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtAdvReceiptIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvReceiptIGST.DoubleClick
      Call SearchAccount(txtAdvReceiptIGST)
   End Sub
   Private Sub txtAdvReceiptIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvReceiptIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtAdvReceiptIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtAdvReceiptIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvReceiptIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtAdvReceiptIGST)
   End Sub
   Private Sub txtAdvReceiptIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvReceiptIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtAdvReceiptIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txtCompanyAcctName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompanyAcctName.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtCompanyAcctName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompanyAcctName.DoubleClick
      Call SearchAccount(txtCompanyAcctName)
   End Sub

   Private Sub txtCompanyAcctName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompanyAcctName.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtCompanyAcctName.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txtCompanyAcctName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCompanyAcctName.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtCompanyAcctName)
   End Sub

   Private Sub txtCompanyAcctName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCompanyAcctName.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtCompanyAcctName.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txteRCCCashLedger_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteRCCCashLedger.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txteRCCCashLedger_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteRCCCashLedger.DoubleClick
      Call SearchAccount(txteRCCCashLedger)
   End Sub

   Private Sub txteRCCCashLedger_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txteRCCCashLedger.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txteRCCCashLedger.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txteRCCCashLedger_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txteRCCCashLedger.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txteRCCCashLedger)
   End Sub

   Private Sub txteRCCCashLedger_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txteRCCCashLedger.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txteRCCCashLedger.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txteRCICashLedger_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteRCICashLedger.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txteRCICashLedger_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteRCICashLedger.DoubleClick
      Call SearchAccount(txteRCICashLedger)
   End Sub

   Private Sub txteRCICashLedger_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txteRCICashLedger.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txteRCICashLedger.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txteRCICashLedger_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txteRCICashLedger.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txteRCICashLedger)
   End Sub

   Private Sub txteRCICashLedger_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txteRCICashLedger.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txteRCICashLedger.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txteRCSCashLedger_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteRCSCashLedger.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txteRCSCashLedger_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteRCSCashLedger.DoubleClick
      Call SearchAccount(txteRCSCashLedger)
   End Sub

   Private Sub txteRCSCashLedger_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txteRCSCashLedger.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txteRCSCashLedger.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txteRCSCashLedger_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txteRCSCashLedger.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txteRCSCashLedger)
   End Sub

   Private Sub txteRCSCashLedger_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txteRCSCashLedger.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txteRCSCashLedger.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txteSCashLedger_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteSCashLedger.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txteSCashLedger_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteSCashLedger.DoubleClick
      Call SearchAccount(txteSCashLedger)
   End Sub

   Private Sub txteSCashLedger_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txteSCashLedger.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txteSCashLedger.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txteSCashLedger_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txteSCashLedger.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txteSCashLedger)
   End Sub

   Private Sub txteSCashLedger_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txteSCashLedger.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txteSCashLedger.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txteICashLedger_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteICashLedger.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txteICashLedger_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteICashLedger.DoubleClick
      Call SearchAccount(txteICashLedger)
   End Sub

   Private Sub txteICashLedger_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txteICashLedger.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txteICashLedger.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txteICashLedger_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txteICashLedger.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txteICashLedger)
   End Sub

   Private Sub txteICashLedger_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txteICashLedger.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txteICashLedger.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txteCCashLedger_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteCCashLedger.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txteCCashLedger_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteCCashLedger.DoubleClick
      Call SearchAccount(txteCCashLedger)
   End Sub

   Private Sub txteCCashLedger_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txteCCashLedger.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txteCCashLedger.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txteCCashLedger_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txteCCashLedger.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txteCCashLedger)
   End Sub

   Private Sub txteCCashLedger_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txteCCashLedger.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txteCCashLedger.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txtGSTInterestPayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTInterestPayment.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtGSTInterestPayment_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTInterestPayment.DoubleClick
      Call SearchAccount(txtGSTInterestPayment)
   End Sub

   Private Sub txtGSTInterestPayment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTInterestPayment.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtGSTInterestPayment.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txtGSTInterestPayment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGSTInterestPayment.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtGSTInterestPayment)
   End Sub

   Private Sub txtGSTInterestPayment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGSTInterestPayment.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtGSTInterestPayment.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txtGSTLatePayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTLatePayment.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtGSTLatePayment_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTLatePayment.DoubleClick
      Call SearchAccount(txtGSTLatePayment)
   End Sub

   Private Sub txtGSTLatePayment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTLatePayment.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtGSTLatePayment.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txtGSTLatePayment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGSTLatePayment.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtGSTLatePayment)
   End Sub

   Private Sub txtGSTLatePayment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGSTLatePayment.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtGSTLatePayment.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtInvPrefix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvPrefix.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtInvPrefix_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvPrefix.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtInvPrefix.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub txtPurCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtPurCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurCGST.DoubleClick
      Call SearchAccount(txtPurCGST)
   End Sub
   Private Sub txtPurCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtPurCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtPurCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtPurCGST)
   End Sub
   Private Sub txtPurCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtPurCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txtPurRCCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurRCCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtPurRCCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurRCCGST.DoubleClick
      Call SearchAccount(txtPurRCCGST)
   End Sub
   Private Sub txtPurRCCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurRCCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtPurRCCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtPurRCCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurRCCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtPurRCCGST)
   End Sub
   Private Sub txtPurRCCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurRCCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtPurRCCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtPurRCSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurRCSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtPurRCSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurRCSGST.DoubleClick
      Call SearchAccount(txtPurRCSGST)
   End Sub
   Private Sub txtPurRCSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurRCSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtPurRCSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtPurRCSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurRCSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtPurRCSGST)
   End Sub
   Private Sub txtPurRCSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurRCSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtPurRCSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtPurRCIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurRCIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtPurRCIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurRCIGST.DoubleClick
      Call SearchAccount(txtPurRCIGST)
   End Sub
   Private Sub txtPurRCIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurRCIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtPurRCIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtPurRCIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurRCIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtPurRCIGST)
   End Sub
   Private Sub txtPurRCIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurRCIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtPurRCIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txtRejDRCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejDRCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtRejDRCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejDRCGST.DoubleClick
      Call SearchAccount(txtRejDRCGST)
   End Sub
   Private Sub txtRejDRCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRejDRCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtRejDRCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtRejDRCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRejDRCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtRejDRCGST)
   End Sub
   Private Sub txtRejDRCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRejDRCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtRejDRCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtRejDRSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejDRSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtRejDRSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejDRSGST.DoubleClick
      Call SearchAccount(txtRejDRSGST)
   End Sub
   Private Sub txtRejDRSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRejDRSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtRejDRSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtRejDRSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRejDRSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtRejDRSGST)
   End Sub
   Private Sub txtRejDRSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRejDRSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtRejDRSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub



   Private Sub txtRejDRIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejDRIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtRejDRIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejDRIGST.DoubleClick
      Call SearchAccount(txtRejDRIGST)
   End Sub
   Private Sub txtRejDRIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRejDRIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtRejDRIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtRejDRIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRejDRIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtRejDRIGST)
   End Sub
   Private Sub txtRejDRIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRejDRIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtRejDRIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub





   Private Sub txtSaleRCIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRCIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleRCIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRCIGST.DoubleClick
      Call SearchAccount(txtSaleRCIGST)
   End Sub
   Private Sub txtSaleRCIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleRCIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleRCIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleRCIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleRCIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleRCIGST)
   End Sub
   Private Sub txtSaleRCIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleRCIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleRCIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtSaleRCSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRCSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleRCSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRCSGST.DoubleClick
      Call SearchAccount(txtSaleRCSGST)
   End Sub
   Private Sub txtSaleRCSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleRCSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleRCSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleRCSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleRCSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleRCSGST)
   End Sub
   Private Sub txtSaleRCSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleRCSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleRCSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtSaleRCCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRCCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleRCCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRCCGST.DoubleClick
      Call SearchAccount(txtSaleRCCGST)
   End Sub
   Private Sub txtSaleRCCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleRCCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleRCCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleRCCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleRCCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleRCCGST)
   End Sub
   Private Sub txtSaleRCCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleRCCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleRCCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub

   Private Sub txtPurSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtPurSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurSGST.DoubleClick
      Call SearchAccount(txtPurSGST)
   End Sub
   Private Sub txtPurSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtPurSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtPurSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtPurSGST)
   End Sub
   Private Sub txtPurSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtPurSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtPurIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtPurIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurIGST.DoubleClick
      Call SearchAccount(txtPurIGST)
   End Sub
   Private Sub txtPurIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtPurIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtPurIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtPurIGST)
   End Sub
   Private Sub txtPurIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtPurIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtSaleCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleCGST.DoubleClick
      Call SearchAccount(txtSaleCGST)
   End Sub
   Private Sub txtSaleCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleCGST)
   End Sub
   Private Sub txtSaleCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtSaleSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleSGST.DoubleClick
      Call SearchAccount(txtSaleSGST)
   End Sub
   Private Sub txtSaleSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleSGST)
   End Sub
   Private Sub txtSaleSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtSaleIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleIGST.DoubleClick
      Call SearchAccount(txtSaleIGST)
   End Sub
   Private Sub txtSaleIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleIGST)
   End Sub
   Private Sub txtSaleIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtSaleRetCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRetCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleRetCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRetCGST.DoubleClick
      Call SearchAccount(txtSaleRetCGST)
   End Sub
   Private Sub txtSaleRetCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleRetCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleRetCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleRetCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleRetCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleRetCGST)
   End Sub
   Private Sub txtSaleRetCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleRetCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleRetCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtSaleRetSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRetSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleRetSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRetSGST.DoubleClick
      Call SearchAccount(txtSaleRetSGST)
   End Sub
   Private Sub txtSaleRetSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleRetSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleRetSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleRetSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleRetSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleRetSGST)
   End Sub
   Private Sub txtSaleRetSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleRetSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleRetSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtSaleRetIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRetIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtSaleRetIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleRetIGST.DoubleClick
      Call SearchAccount(txtSaleRetIGST)
   End Sub
   Private Sub txtSaleRetIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleRetIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleRetIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtSaleRetIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSaleRetIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSaleRetIGST)
   End Sub
   Private Sub txtSaleRetIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleRetIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtSaleRetIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtCRCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCRCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtCRCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCRCGST.DoubleClick
      Call SearchAccount(txtCRCGST)
   End Sub
   Private Sub txtCRCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCRCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtCRCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtCRCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCRCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtCRCGST)
   End Sub
   Private Sub txtCRCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCRCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtCRCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtCRSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCRSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtCRSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCRSGST.DoubleClick
      Call SearchAccount(txtCRSGST)
   End Sub
   Private Sub txtCRSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCRSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtCRSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtCRSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCRSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtCRSGST)
   End Sub
   Private Sub txtCRSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCRSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtCRSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtCRIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCRIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtCRIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCRIGST.DoubleClick
      Call SearchAccount(txtCRIGST)
   End Sub
   Private Sub txtCRIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCRIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtCRIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtCRIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCRIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtCRIGST)
   End Sub
   Private Sub txtCRIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCRIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtCRIGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtDRCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDRCGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtDRCGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDRCGST.DoubleClick
      Call SearchAccount(txtDRCGST)
   End Sub
   Private Sub txtDRCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDRCGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtDRCGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtDRCGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDRCGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtDRCGST)
   End Sub
   Private Sub txtDRCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDRCGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtDRCGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtDRSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDRSGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtDRSGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDRSGST.DoubleClick
      Call SearchAccount(txtDRSGST)
   End Sub
   Private Sub txtDRSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDRSGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtDRSGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtDRSGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDRSGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtDRSGST)
   End Sub
   Private Sub txtDRSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDRSGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtDRSGST.Text) = "" Then GoTo EventExitSub

      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
   Private Sub txtDRIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDRIGST.TextChanged
      '		
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtDRIGST_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDRIGST.DoubleClick
      Call SearchAccount(txtDRIGST)
   End Sub
   Private Sub txtDRIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDRIGST.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      '		
      KeyAscii = MainClass.UpperCase(KeyAscii, txtDRIGST.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub txtDRIGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDRIGST.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtDRIGST)
   End Sub
   Private Sub txtDRIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDRIGST.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ErrPart
      If Trim(txtDRIGST.Text) = "" Then GoTo EventExitSub

      If MainClass.ValidateWithMasterTable(txtDRIGST.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
         eventArgs.Cancel = True
      End If
      GoTo EventExitSub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
End Class
