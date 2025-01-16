Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data.SqlClient   '' System.Data.OleDb				
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAcmRequisition
    Inherits System.Windows.Forms.Form
    Dim RsACMReq As ADODB.Recordset ''ADODB.Recordset				
    'Private PvtDBCn As ADODB.Connection				

    ''Dim RsOpOuts As ADODB.Recordset				

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String
    Dim ResizeForm As New Resizer

    Private Const ConRowHeight As Short = 14
    Dim xMyMenu As String
    Private Sub SetCombo(ByRef ComboName As System.Windows.Forms.ComboBox, ByRef mMasterType As String)
        Dim CntCount As Integer

        For CntCount = 0 To ComboName.Items.Count - 1
            ComboName.SelectedIndex = CntCount
            If mMasterType = VB.Left(ComboName.Text, 1) Then
                Exit Sub
            End If
        Next
        ComboName.SelectedIndex = -1
    End Sub
    Private Sub cboCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboCType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboHeadType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboHeadType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboHeadType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboHeadType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPaymentMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentMode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPaymentMode_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentMode.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboSupplierType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSupplierType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboSupplierType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSupplierType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboTDSCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboTDSCategory.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAuthorised_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAuthorised.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDistt_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDistt.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCountry_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCountry.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInterUnit_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInterUnit.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkLowerDeduction_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkLowerDeduction.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMonthWiseLdgr_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMonthWiseLdgr.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub ChkPoRate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPoRate.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkState_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkState.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopBP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopBP.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopGP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopGP.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopInvoice_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopInvoice.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopMRR_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopMRR.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            CmdAdd.Text = ConCmdCancelCaption
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtName.Focus()
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Sub FillComboBox()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        cboCategory.Items.Clear()
        cboCategory.Items.Add("Customer")
        cboCategory.Items.Add("Supplier")
        cboCategory.Items.Add("Employee")
        cboCategory.Items.Add("1- Cash")
        cboCategory.Items.Add("2- Bank")
        cboCategory.Items.Add("Other")
        cboCategory.Items.Add("Fixed Assets")

        cboHeadType.Items.Clear()
        cboHeadType.Items.Add("None")
        cboHeadType.Items.Add("Loan & Advance Head")
        cboHeadType.Items.Add("TDS Head")
        cboHeadType.Items.Add("Imprest Head")
        cboHeadType.Items.Add("ESI Head")
        cboHeadType.Items.Add("Service Tax Claim")
        cboHeadType.Items.Add("Jobworker - Supporting Manu.")
        cboHeadType.Items.Add("Profit & Loss")
        cboHeadType.Items.Add("1. TDS (Salary) Head")
        cboHeadType.Items.Add("2. Duties")
        cboHeadType.Items.Add("3. Increase & Decrease Stock")
        cboHeadType.Items.Add("4. Service Head")
        '    cboHeadType.AddItem "Gratuity"				

        '    cboHeadType.AddItem "1. Freight Outward"				
        '    cboHeadType.AddItem "2. Freight Inward"				
        cboHeadType.SelectedIndex = 0

        CboTDSCategory.Items.Clear()
        CboTDSCategory.Items.Add("NONE")
        CboTDSCategory.Items.Add("CONTRACTOR")
        CboTDSCategory.Items.Add("PROFESSIONAL")
        CboTDSCategory.SelectedIndex = 0


        cboSupplierType.Items.Clear()
        cboSupplierType.Items.Add("")
        cboSupplierType.Items.Add("CONSIGNMENT AGENT")
        cboSupplierType.Items.Add("MANUFACTURER")
        cboSupplierType.Items.Add("1st-STAGE DEALER")
        cboSupplierType.Items.Add("2nd-STAGE DEALER")
        cboSupplierType.Items.Add("TRADERS")
        cboSupplierType.Items.Add("IMPORTER")
        cboSupplierType.Items.Add("CONTRACTOR")
        cboSupplierType.Items.Add("OTHER")
        cboSupplierType.Items.Add("100% EOU")
        cboSupplierType.Items.Add("CUSTOMER")
        cboSupplierType.Items.Add("CUSTOMER-RM")
        cboSupplierType.SelectedIndex = 0

        cboCType.Items.Clear()
        cboCType.Items.Add("COMPANY")
        cboCType.Items.Add("NON-COMPANY")
        cboCType.SelectedIndex = 0

        '    cboPaymentMode.Clear				
        '    cboPaymentMode.AddItem "1. Cheque"				
        '    cboPaymentMode.AddItem "2. Hundi"				
        '    cboPaymentMode.AddItem "3. LC"				
        '    cboPaymentMode.ListIndex = 0				

        cboPaymentMode.Items.Clear()
        cboPaymentMode.Items.Add("1. Cheque")
        cboPaymentMode.Items.Add("2. Hundi")
        cboPaymentMode.Items.Add("3. LC")
        cboPaymentMode.Items.Add("4. MSME")
        cboPaymentMode.Items.Add("5. PDC")
        cboPaymentMode.Items.Add("6. DISC-YES")
        cboPaymentMode.Items.Add("7. DISC-CASH")
        cboPaymentMode.Items.Add("8. DISC-TCFL")
        cboPaymentMode.Items.Add("9. UGRO")
        cboPaymentMode.Items.Add("A. ONLINE")
        ''cboPaymentMode.AddItem "10. NON MSME"				
        cboPaymentMode.SelectedIndex = 0

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        '    'If PvtDBCn.State = adStateOpen Then				
        '        ''PvtDBCn.Close				
        '        ''Set PvtDBCn = Nothing				
        '    End If				
        RsACMReq.Close()
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String = ""
        Dim RsOpOuts As ADODB.Recordset
        Dim mOPBal As Integer

        If txtName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        If Not RsACMReq.EOF Then
            '         SqlStr = " SELECT COUNT(1) AS CNTROW From FIN_POSTED_TRN WHERE " & vbCrLf _				
            ''                & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _				
            ''                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _				
            ''                & " AND BOOKTYPE='" & vb.Left(ConOpening, 1) & "'" & vbCrLf _				
            ''                & " AND BOOKSUBTYPE='" & Right(ConOpening, 1) & "'" & vbCrLf _				
            ''                & " AND ACCOUNTCODE='" & RsACMReq.Fields("SUPP_CUST_CODE").Value & "'"				
            '				
            '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOpOuts, adLockReadOnly				
            '        If Not RsOpOuts.EOF Then				
            '            mOPBal = IIf(IsNull(RsOpOuts!cntRow), 0, RsOpOuts!cntRow)				
            '        End If				
            '				
            '        If mOPBal > 0 Then				
            '            MsgInformation "First Delete Opening Balance."				
            '            Exit Sub				
            '        End If				

            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_CUST_REQ_MST", (txtName.Text), RsACMReq, "SUPP_CUST_NAME") = False Then GoTo DelErrPart
                '            If InsertIntoDeleteTrn(PubDBCn, "FIN_SUPP_CUST_REQ_MST", "SUPP_CUST_CODE", RsACMReq!SUPP_CUST_CODE) = False Then GoTo DelErrPart:				

                '            SqlStr = " DELETE From FIN_POSTED_TRN WHERE " & vbCrLf _				
                ''                    & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _				
                ''                    & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _				
                ''                    & " AND BOOKTYPE='" & vb.Left(ConOpening, 1) & "'" & vbCrLf _				
                ''                    & " AND BOOKSUBTYPE='" & Right(ConOpening, 1) & "'" & vbCrLf _				
                ''                    & " AND ACCOUNTCODE='" & RsACMReq.Fields("SUPP_CUST_CODE").Value & "'"				
                '            PubDBCn.Execute SqlStr				

                SqlStr = " DELETE From FIN_SUPP_CUST_REQ_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & RsACMReq.Fields("SUPP_CUST_NAME").Value & "'"

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsACMReq.Requery() ''.Refresh				
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        '    Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''				
        RsACMReq.Requery() ''.Refresh				
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsACMReq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then

            SSTInfo.SelectedIndex = 0
            If lblRegularized.Text = "Y" Then
                ADDMode = True
                Clear1()
                If CmdAdd.Enabled = True Then CmdAdd.Focus()
            Else
                ADDMode = False
                MODIFYMode = False
                TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
                If CmdAdd.Enabled = True Then CmdAdd.Focus()
            End If
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If UpdateAcm() = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''				
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function



    Private Function UpdateAcm() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mGroupCodeCr As Integer
        Dim mGroupCode As Integer
        Dim mStatus As String = ""
        Dim mCategory As String = ""
        Dim mBalancingMethod As String = ""
        Dim mHeadType As String = ""
        Dim mPaymentMode As String = ""
        Dim mWithInState As String = ""
        Dim mWithInDistt As String = ""
        Dim mWithInCountry As String = ""
        Dim mTypeofSupplier As String = ""

        Dim mRegdDealer As String = ""
        Dim mPORATEEDITABLE As String = ""
        Dim mCurrencyName As String = ""

        Dim mSectionCode As Integer
        Dim mCTYPE As String = ""

        Dim mPurchaseSTRecd As Integer
        Dim mPurchaseSTDue As Integer
        Dim mSaleSTRecd As Integer
        Dim mSaleSTDue As Integer
        Dim mBuyerCode As String = ""
        Dim mMonthWiseLdgr As String = ""
        Dim mServiceProviderCode As Double
        Dim mInterUnit As String = ""
        Dim mAuthorised As String = ""

        Dim mStopMRR As String = ""
        Dim mStopInvoice As String = ""
        Dim mStopGP As String = ""
        Dim mStopBP As String = ""
        Dim mIsLowerDed As String = ""
        Dim mGSTRegd As String = ""
        Dim mGSTClass As String = ""

        If optGSTRegd(0).Checked = True Then
            mGSTRegd = "Y"
        ElseIf optGSTRegd(1).Checked = True Then
            mGSTRegd = "N"
        ElseIf optGSTRegd(2).Checked = True Then
            mGSTRegd = "E"
        ElseIf optGSTRegd(3).Checked = True Then
            mGSTRegd = "F"
        ElseIf optGSTRegd(4).Checked = True Then
            mGSTRegd = "C"
        End If

        If optGSTClassification(0).Checked = True Then
            mGSTClass = "F"
        Else
            mGSTClass = "R"
        End If

        If MainClass.ValidateWithMasterTable(txtPayment.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblPaymentTerms.Text = MasterNo
        Else
            lblPaymentTerms.Text = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSectionCode = MasterNo
        Else
            mSectionCode = -1
        End If

        If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_REQ_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBuyerCode = MasterNo
        Else
            mBuyerCode = ""
        End If

        If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mServiceProviderCode = MasterNo
        Else
            mServiceProviderCode = -1
        End If


        mCTYPE = VB.Left(cboCType.Text, 1)

        mCurrencyName = IIf(Trim(txtCurrency.Text) = "", "Rs", Trim(txtCurrency.Text))
        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")
        mCategory = VB.Left(cboCategory.Text, 1)
        mHeadType = VB.Left(cboHeadType.Text, 1)
        mPaymentMode = VB.Left(cboPaymentMode.Text, 1)

        mTypeofSupplier = Trim(cboSupplierType.Text)


        If MainClass.ValidateWithMasterTable(txtGroupName.Text, "Group_Name", "Group_Code", "FIN_Group_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGroupCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtGroupNameCr.Text, "Group_Name", "Group_Code", "FIN_Group_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGroupCodeCr = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtPurchaseSTRecd.Text, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPurchaseSTRecd = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtPurchaseSTDue.Text, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPurchaseSTDue = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtSaleSTRecd.Text, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSaleSTRecd = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtSaleSTDue.Text, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSaleSTDue = MasterNo
        End If



        '*********				
        mBalancingMethod = IIf(optBalMethod(0).Checked = True, "S", "D")
        mWithInState = IIf(chkState.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mInterUnit = IIf(chkInterUnit.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAuthorised = IIf(chkAuthorised.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mWithInCountry = IIf(chkCountry.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPORATEEDITABLE = IIf(ChkPoRate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMonthWiseLdgr = IIf(chkMonthWiseLdgr.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsLowerDed = IIf(chkLowerDeduction.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        mStopMRR = IIf(chkStopMRR.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStopInvoice = IIf(chkStopInvoice.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStopGP = IIf(chkStopGP.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStopBP = IIf(chkStopBP.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        mWithInDistt = IIf(chkDistt.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If optRegd(0).Checked = True Then
            mRegdDealer = "Y"
        Else
            mRegdDealer = "N"
        End If

        If ADDMode = True Then
            mAccountCode = MainClass.AllowSingleQuote(txtCode.Text) ''MainClass.AutoGenRowNo("ACM", "Code", PubDBCn)				

            SqlStr = ""
            SqlStr = " INSERT INTO FIN_SUPP_CUST_REQ_MST ( " & vbCrLf _
                & " COMPANY_CODE, SUPP_CUST_CODE, SUPP_CUST_TYPE,  " & vbCrLf _
                & " SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,  " & vbCrLf _
                & " SUPP_CUST_STATE, SUPP_CUST_PIN, SUPP_CUST_PHONE, " & vbCrLf _
                & " SUPP_CUST_FAXNO, SUPP_CUST_MAILID,SUPP_CUST_MOBILE, " & vbCrLf _
                & " CST_NO, LST_NO, PAN_NO,  " & vbCrLf _
                & " EXCISE_DIV, EXCISE_RANGE, CENT_EXC_RGN_NO, " & vbCrLf _
                & " ECC_NO, SUPP_CUST_REMARKS, " & vbCrLf _
                & " WITHIN_STATE, WITHIN_DISTT, WITHIN_COUNTRY," & vbCrLf _
                & " COMMISIONER_RATE, REGD_DEALER, " & vbCrLf _
                & " CONTACT_TELNO, " & vbCrLf _
                & " ACTIVITY, TYPE_OF_SUPPLIER, SRV_REGN_NO, " & vbCrLf _
                & " GROUPCODE, GROUPCODECR, BALANCINGMETHOD, HEADTYPE, " & vbCrLf _
                & " HEAD_PER, TDSCATEGORY, TDS_PER, STDS_PER, ESI_PER, " & vbCrLf _
                & " PAIDDAY,ACCOUNT_CODE,PORATEEDITABLE,DSP_RPT_SEQ, CURRENCYNAME, EMP_CODE, " & vbCrLf _
                & " CTYPE, SECTIONCODE, EXPTIONCNO, STATUS," & vbCrLf _
                & " PUR_STRECD_FORMCODE, PUR_STDUE_FORMCODE, " & vbCrLf _
                & " SALE_STRECD_FORMCODE, SALE_STDUE_FORMCODE, ALIAS_NAME, "


            SqlStr = SqlStr & vbCrLf _
                & " COUNTRY, BUYERCODE, CARRIAGE, " & vbCrLf _
                & " LOADINGPORT, DISCHARGEPORT, FINALDEST, " & vbCrLf _
                & " PAYMENTTERMS, "

            SqlStr = SqlStr & vbCrLf _
                & " PAIDDAY2, PAIDDAY3, PAIDDAY4, VENDOR_CODE, " & vbCrLf _
                & " MONTHWISE_LDGR, SERVPROV_CODE, INTER_UNIT, " & vbCrLf _
                & " PAYMENT_CODE, PAYMENT_DESC, AUTHORISED, "

            SqlStr = SqlStr & vbCrLf _
                & " STOP_MRR, STOP_INVOICE, STOP_RGP, STOP_BANK, WEB_PASSWORD, " & vbCrLf _
                & " LOWER_DED_CERT_NO, IS_LOWER_DED,PAYMENT_MODE, "

            SqlStr = SqlStr & vbCrLf _
                & " CUST_BANK_ACCT_NO, BANK_SWIFT_CODE, BANK_BRANCH_NAME, " & vbCrLf _
                & " BANK_IFSC_CODE, CUST_BANK_BANK, " & vbCrLf _
                & " GST_RGN_NO, GST_REGD, GST_CLASSIFICATION, LOC_DISTANCE, "

            SqlStr = SqlStr & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE) VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " " & RsCompany.Fields("Company_Code").Value & ", '" & MainClass.AllowSingleQuote(txtCode.Text) & "', '" & MainClass.AllowSingleQuote(mCategory) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtName.Text) & "', '" & MainClass.AllowSingleQuote(txtaddress.Text) & "', '" & MainClass.AllowSingleQuote(txtCity.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtState.Text) & "','" & MainClass.AllowSingleQuote(txtPinCode.Text) & "','" & MainClass.AllowSingleQuote(txtPhone.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtFax.Text) & "','" & MainClass.AllowSingleQuote(txtEmail.Text) & "','" & MainClass.AllowSingleQuote(txtMobile.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCstNo.Text) & "','" & MainClass.AllowSingleQuote(txtLSTNo.Text) & "','" & MainClass.AllowSingleQuote(txtPan.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDivision.Text) & "', '" & MainClass.AllowSingleQuote(txtRange.Text) & "','" & MainClass.AllowSingleQuote(txtRegnNo.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtECCNo.Text) & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                & " '" & mWithInState & "', '" & mWithInDistt & "', '" & mWithInCountry & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCommRate.Text) & "', '" & MainClass.AllowSingleQuote(mRegdDealer) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtContact.Text) & "', " & vbCrLf & " " & Val(txtChqFrequency.Text) & ",'" & mTypeofSupplier & "', '" & MainClass.AllowSingleQuote(txtSrvRegnNo.Text) & "'," & vbCrLf & " " & mGroupCode & ", " & mGroupCodeCr & ", '" & mBalancingMethod & "', '" & mHeadType & "'," & vbCrLf & " 0,'" & MainClass.AllowSingleQuote(CboTDSCategory.Text) & "', " & vbCrLf & " " & Val(txtTDSPer.Text) & ", " & Val(txtSTDSPer.Text) & ", " & Val(txtESIPer.Text) & ", " & vbCrLf & " " & Val(txtPaidDay.Text) & ",'" & MainClass.AllowSingleQuote(txtTINNo.Text) & "'," & vbCrLf & " '" & mPORATEEDITABLE & "', '" & MainClass.AllowSingleQuote(txtSeq.Text) & "', '" & Trim(mCurrencyName) & "', '" & Trim(txtEmpCode.Text) & "', " & vbCrLf & " '" & mCTYPE & "', " & mSectionCode & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(txtExptionCNo.Text)) & "', '" & mStatus & "', " & vbCrLf & " " & mPurchaseSTRecd & ", " & mPurchaseSTDue & ", " & vbCrLf & " " & mSaleSTRecd & ", " & mSaleSTDue & ", '" & MainClass.AllowSingleQuote(txtAlias.Text) & "', "


            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCountry.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mBuyerCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtCarriage.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtLoadingPort.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtDischargePort.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtFinalDest.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtExportPaymetTerms.Text) & "', "

            SqlStr = SqlStr & vbCrLf _
                & " " & Val(txtPaidDay2.Text) & ", " & Val(txtPaidDay3.Text) & ", " & Val(txtPaidDay4.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "', '" & mMonthWiseLdgr & "', " & vbCrLf & " " & IIf(mServiceProviderCode = -1, "Null", mServiceProviderCode) & ", '" & mInterUnit & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPayment.Text) & "','" & MainClass.AllowSingleQuote(lblPaymentTerms.Text) & "','" & mAuthorised & "',"

            SqlStr = SqlStr & vbCrLf _
                & " '" & mStopMRR & "', '" & mStopInvoice & "', '" & mStopGP & "', '" & mStopBP & "', '" & MainClass.AllowSingleQuote(txtCode.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtLDCertiNo.Text) & "', '" & mIsLowerDed & "', '" & mPaymentMode & "',"

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBankAccountNo.Text) & "', '" & MainClass.AllowSingleQuote(txtSwitCode.Text) & "', '" & MainClass.AllowSingleQuote(txtBankBranch.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtIFSCCode.Text) & "', '" & MainClass.AllowSingleQuote(txtBankName.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtGSTRegnNo.Text) & "', '" & mGSTRegd & "', " & vbCrLf & " '" & mGSTClass & "'," & Val(txtDistance.Text) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"

        End If


        If MODIFYMode = True Then
            SqlStr = ""

            SqlStr = " UPDATE FIN_SUPP_CUST_REQ_MST SET  SUPP_CUST_CODE= '" & MainClass.AllowSingleQuote(txtCode.Text) & "'," & vbCrLf & " SUPP_CUST_NAME= '" & MainClass.AllowSingleQuote(txtName.Text) & "', " & vbCrLf & " GROUPCODE= " & mGroupCode & " , GROUPCODECR= " & mGroupCodeCr & " , " & vbCrLf & " SUPP_CUST_TYPE= '" & mCategory & "', " & vbCrLf & " INTER_UNIT= '" & mInterUnit & "', " & vbCrLf & " BALANCINGMETHOD='" & mBalancingMethod & "' , " & vbCrLf & " SUPP_CUST_ADDR= '" & MainClass.AllowSingleQuote(txtaddress.Text) & "', " & vbCrLf & " SUPP_CUST_CITY= '" & MainClass.AllowSingleQuote(txtCity.Text) & "', " & vbCrLf & " SUPP_CUST_PIN= '" & MainClass.AllowSingleQuote(txtPinCode.Text) & "', " & vbCrLf & " SUPP_CUST_STATE= '" & MainClass.AllowSingleQuote(txtState.Text) & "' , " & vbCrLf & " SUPP_CUST_PHONE= '" & MainClass.AllowSingleQuote(txtPhone.Text) & "', " & vbCrLf & " SUPP_CUST_FAXNO= '" & MainClass.AllowSingleQuote(txtFax.Text) & "', " & vbCrLf & " SUPP_CUST_MAILID= '" & MainClass.AllowSingleQuote(txtEmail.Text) & "', " & vbCrLf & " SUPP_CUST_MOBILE= '" & MainClass.AllowSingleQuote(txtMobile.Text) & "', " & vbCrLf & " AUTHORISED='" & mAuthorised & "'," & vbCrLf & " LST_NO= '" & MainClass.AllowSingleQuote(txtLSTNo.Text) & "', " & vbCrLf & " CST_NO= '" & MainClass.AllowSingleQuote(txtCstNo.Text) & "', " & vbCrLf & " SECTIONCODE=" & mSectionCode & ", " & vbCrLf & " EXPTIONCNO='" & MainClass.AllowSingleQuote(Trim(txtExptionCNo.Text)) & "', " & vbCrLf & " LOWER_DED_CERT_NO='" & MainClass.AllowSingleQuote(txtLDCertiNo.Text) & "', IS_LOWER_DED='" & mIsLowerDed & "'," & vbCrLf & " ALIAS_NAME='" & MainClass.AllowSingleQuote(txtAlias.Text) & "', " & vbCrLf & " CTYPE='" & mCTYPE & "', STATUS='" & mStatus & "', " & vbCrLf & " VENDOR_CODE='" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "',"

            SqlStr = SqlStr & vbCrLf & " CURRENCYNAME = '" & Trim(mCurrencyName) & "', " & vbCrLf & " PAN_NO= '" & MainClass.AllowSingleQuote(txtPan.Text) & "', " & vbCrLf & " EXCISE_DIV= '" & MainClass.AllowSingleQuote(txtDivision.Text) & "', " & vbCrLf & " EXCISE_RANGE= '" & MainClass.AllowSingleQuote(txtRange.Text) & "', " & vbCrLf & " CENT_EXC_RGN_NO= '" & MainClass.AllowSingleQuote(txtRegnNo.Text) & "', " & vbCrLf & " ECC_NO= '" & MainClass.AllowSingleQuote(txtECCNo.Text) & "', " & vbCrLf & " SUPP_CUST_REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " WITHIN_STATE= '" & MainClass.AllowSingleQuote(mWithInState) & "', " & vbCrLf & " WITHIN_DISTT= '" & MainClass.AllowSingleQuote(mWithInDistt) & "', " & vbCrLf & " WITHIN_COUNTRY= '" & MainClass.AllowSingleQuote(mWithInCountry) & "', " & vbCrLf & " COMMISIONER_RATE= '" & MainClass.AllowSingleQuote(txtCommRate.Text) & "', " & vbCrLf & " REGD_DEALER= '" & MainClass.AllowSingleQuote(mRegdDealer) & "', " & vbCrLf & " CONTACT_TELNO= '" & MainClass.AllowSingleQuote(txtContact.Text) & "', " & vbCrLf & " ACTIVITY= " & Val(txtChqFrequency.Text) & ", TYPE_OF_SUPPLIER= '" & mTypeofSupplier & "', " & vbCrLf & " HEADTYPE= '" & MainClass.AllowSingleQuote(mHeadType) & "', " & vbCrLf & " HEAD_PER= 0, EMP_CODE='" & Trim(txtEmpCode.Text) & "', " & vbCrLf & " TDSCATEGORY= '" & MainClass.AllowSingleQuote(CboTDSCategory.Text) & "', " & vbCrLf & " TDS_PER= " & Val(txtTDSPer.Text) & ", STDS_PER= " & Val(txtSTDSPer.Text) & ", ESI_PER= " & Val(txtESIPer.Text) & ", " & vbCrLf & " PORATEEDITABLE='" & mPORATEEDITABLE & "'," & vbCrLf & " PAIDDAY= " & Val(txtPaidDay.Text) & ",ACCOUNT_CODE='" & MainClass.AllowSingleQuote(txtTINNo.Text) & "', " & vbCrLf & " SRV_REGN_NO='" & MainClass.AllowSingleQuote(txtSrvRegnNo.Text) & "'," & vbCrLf & " DSP_RPT_SEQ='" & MainClass.AllowSingleQuote(txtSeq.Text) & "', "

            SqlStr = SqlStr & vbCrLf & " PUR_STRECD_FORMCODE=" & mPurchaseSTRecd & ", " & vbCrLf & " PUR_STDUE_FORMCODE=" & mPurchaseSTDue & ", " & vbCrLf & " SALE_STRECD_FORMCODE=" & mSaleSTRecd & ", " & vbCrLf & " SALE_STDUE_FORMCODE=" & mSaleSTDue & ", "


            SqlStr = SqlStr & vbCrLf & " COUNTRY='" & MainClass.AllowSingleQuote(txtCountry.Text) & "'," & vbCrLf & " BUYERCODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "', " & vbCrLf & " CARRIAGE='" & MainClass.AllowSingleQuote(txtCarriage.Text) & "', " & vbCrLf & " LOADINGPORT='" & MainClass.AllowSingleQuote(txtLoadingPort.Text) & "'," & vbCrLf & " DISCHARGEPORT='" & MainClass.AllowSingleQuote(txtDischargePort.Text) & "', " & vbCrLf & " FINALDEST='" & MainClass.AllowSingleQuote(txtFinalDest.Text) & "', " & vbCrLf & " PAYMENTTERMS='" & MainClass.AllowSingleQuote(txtExportPaymetTerms.Text) & "', "

            SqlStr = SqlStr & vbCrLf & " PAIDDAY2=" & Val(txtPaidDay2.Text) & ", " & vbCrLf & " PAIDDAY3=" & Val(txtPaidDay3.Text) & ", " & vbCrLf & " PAIDDAY4=" & Val(txtPaidDay4.Text) & ", " & vbCrLf & " MONTHWISE_LDGR='" & mMonthWiseLdgr & "'," & vbCrLf & " SERVPROV_CODE= " & IIf(mServiceProviderCode = -1, "Null", mServiceProviderCode) & ", " & vbCrLf & " PAYMENT_CODE='" & MainClass.AllowSingleQuote(txtPayment.Text) & "', " & vbCrLf & " PAYMENT_DESC='" & MainClass.AllowSingleQuote(lblPaymentTerms.Text) & "', "

            SqlStr = SqlStr & vbCrLf & " STOP_MRR='" & mStopMRR & "', STOP_INVOICE='" & mStopInvoice & "', " & vbCrLf & " PAYMENT_MODE='" & mPaymentMode & "',STOP_RGP='" & mStopGP & "', STOP_BANK='" & mStopBP & "', "

            SqlStr = SqlStr & vbCrLf & " CUST_BANK_ACCT_NO= '" & MainClass.AllowSingleQuote(txtBankAccountNo.Text) & "', " & vbCrLf & " BANK_SWIFT_CODE= '" & MainClass.AllowSingleQuote(txtSwitCode.Text) & "', " & vbCrLf & " BANK_BRANCH_NAME= '" & MainClass.AllowSingleQuote(txtBankBranch.Text) & "', " & vbCrLf & " BANK_IFSC_CODE= '" & MainClass.AllowSingleQuote(txtIFSCCode.Text) & "', " & vbCrLf & " GST_RGN_NO = '" & MainClass.AllowSingleQuote(txtGSTRegnNo.Text) & "', GST_REGD = '" & mGSTRegd & "', " & vbCrLf & " CUST_BANK_BANK= '" & MainClass.AllowSingleQuote(txtBankName.Text) & "', " & vbCrLf & " GST_CLASSIFICATION='" & mGSTClass & "', " & vbCrLf & " LOC_DISTANCE= " & Val(txtDistance.Text) & ","

            SqlStr = SqlStr & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "',MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(txtName.Text) & "'"

        End If


        PubDBCn.Execute(SqlStr)
        If lblRegularized.Text = "Y" Then

            SqlStr = " INSERT INTO FIN_SUPP_CUST_MST ( " & vbCrLf & " COMPANY_CODE, SUPP_CUST_CODE, SUPP_CUST_TYPE, " & vbCrLf & " VENDOR_RATING, SUPP_CUST_NAME, SUPP_CUST_ADDR, " & vbCrLf & " SUPP_CUST_CITY, SUPP_CUST_STATE, SUPP_CUST_PIN, " & vbCrLf & " SUPP_CUST_PHONE, SUPP_CUST_FAXNO, SUPP_CUST_MAILID, " & vbCrLf & " SUPP_CUST_MOBILE, CST_NO, LST_NO, PAN_NO, " & vbCrLf & " EXCISE_DIV, EXCISE_RANGE, CENT_EXC_RGN_NO, " & vbCrLf & " ECC_NO, SUPP_CUST_REMARKS, WITHIN_STATE, " & vbCrLf & " WITHIN_DISTT, COMMISIONER_RATE, REGD_DEALER, " & vbCrLf & " DATE_OF_APPROVAL, CONTACT_TELNO, ACTIVITY, " & vbCrLf & " TYPE_OF_SUPPLIER, ACCOUNT_CODE, GROUPCODE, " & vbCrLf & " BALANCINGMETHOD, HEADTYPE, HEAD_PER, " & vbCrLf & " LOCKDATEFROM, LOCKDATETO, TDSCATEGORY, " & vbCrLf & " TDS_PER, STATUS, PAIDDAY, " & vbCrLf & " PORATEEDITABLE, STDS_PER, ESI_PER, "

            SqlStr = SqlStr & vbCrLf & " WITHIN_COUNTRY, DSP_RPT_SEQ, ADDUSER, " & vbCrLf & " ADDDATE, MODUSER, MODDATE, GROUPCODECR, " & vbCrLf & " CURRENCYNAME, EMP_CODE, SECTIONCODE, " & vbCrLf & " EXPTIONCNO, CTYPE, PUR_STRECD_FORMCODE, " & vbCrLf & " PUR_STDUE_FORMCODE, SALE_STRECD_FORMCODE, " & vbCrLf & " SALE_STDUE_FORMCODE, ALIAS_NAME, SRV_REGN_NO, " & vbCrLf & " COUNTRY, BUYERCODE, CARRIAGE, LOADINGPORT, " & vbCrLf & " DISCHARGEPORT, FINALDEST, PAYMENTTERMS, " & vbCrLf & " PAIDDAY2, PAIDDAY3, PAIDDAY4, VENDOR_CODE, " & vbCrLf & " MONTHWISE_LDGR, SERVPROV_CODE, INTER_UNIT, " & vbCrLf & " MIS_GROUP_CODE, PAYMENT_CODE, PAYMENT_DESC, " & vbCrLf & " AUTHORISED, STOP_MRR, STOP_INVOICE, " & vbCrLf & " STOP_RGP, STOP_BANK, WEB_PASSWORD, " & vbCrLf & " WEB_COUNT, ADHOC_PAY_TERMS, LOWER_DED_CERT_NO, " & vbCrLf & " IS_LOWER_DED, PAYMENT_MODE," & vbCrLf & " CUST_BANK_ACCT_NO, BANK_SWIFT_CODE, BANK_BRANCH_NAME, " & vbCrLf & " BANK_IFSC_CODE, CUST_BANK_BANK, " & vbCrLf & " GST_RGN_NO, GST_REGD, GST_CLASSIFICATION, LOC_DISTANCE)"


            SqlStr = SqlStr & vbCrLf & " SELECT COMPANY_CODE, '" & MainClass.AllowSingleQuote(txtCode.Text) & "', SUPP_CUST_TYPE, " & vbCrLf & " VENDOR_RATING, SUPP_CUST_NAME, SUPP_CUST_ADDR, " & vbCrLf & " SUPP_CUST_CITY, SUPP_CUST_STATE, SUPP_CUST_PIN, " & vbCrLf & " SUPP_CUST_PHONE, SUPP_CUST_FAXNO, SUPP_CUST_MAILID, " & vbCrLf & " SUPP_CUST_MOBILE, CST_NO, LST_NO, PAN_NO, " & vbCrLf & " EXCISE_DIV, EXCISE_RANGE, CENT_EXC_RGN_NO, " & vbCrLf & " ECC_NO, SUPP_CUST_REMARKS, WITHIN_STATE, " & vbCrLf & " WITHIN_DISTT, COMMISIONER_RATE, REGD_DEALER, " & vbCrLf & " DATE_OF_APPROVAL, CONTACT_TELNO, ACTIVITY, " & vbCrLf & " TYPE_OF_SUPPLIER, ACCOUNT_CODE, GROUPCODE, " & vbCrLf & " BALANCINGMETHOD, HEADTYPE, HEAD_PER, " & vbCrLf & " LOCKDATEFROM, LOCKDATETO, TDSCATEGORY, " & vbCrLf & " TDS_PER, STATUS, PAIDDAY, " & vbCrLf & " PORATEEDITABLE, STDS_PER, ESI_PER, "

            SqlStr = SqlStr & vbCrLf & " WITHIN_COUNTRY, DSP_RPT_SEQ, ADDUSER, " & vbCrLf & " ADDDATE, MODUSER, MODDATE, GROUPCODECR, " & vbCrLf & " CURRENCYNAME, EMP_CODE, SECTIONCODE, " & vbCrLf & " EXPTIONCNO, CTYPE, PUR_STRECD_FORMCODE, " & vbCrLf & " PUR_STDUE_FORMCODE, SALE_STRECD_FORMCODE, " & vbCrLf & " SALE_STDUE_FORMCODE, ALIAS_NAME, SRV_REGN_NO, " & vbCrLf & " COUNTRY, BUYERCODE, CARRIAGE, LOADINGPORT, " & vbCrLf & " DISCHARGEPORT, FINALDEST, PAYMENTTERMS, " & vbCrLf & " PAIDDAY2, PAIDDAY3, PAIDDAY4, VENDOR_CODE, " & vbCrLf & " MONTHWISE_LDGR, SERVPROV_CODE, INTER_UNIT, " & vbCrLf & " MIS_GROUP_CODE, PAYMENT_CODE, PAYMENT_DESC, " & vbCrLf & " AUTHORISED, STOP_MRR, STOP_INVOICE, " & vbCrLf & " STOP_RGP, STOP_BANK, WEB_PASSWORD, " & vbCrLf & " WEB_COUNT, ADHOC_PAY_TERMS, LOWER_DED_CERT_NO, " & vbCrLf & " IS_LOWER_DED, PAYMENT_MODE, " & vbCrLf & " CUST_BANK_ACCT_NO, BANK_SWIFT_CODE, BANK_BRANCH_NAME, " & vbCrLf & " BANK_IFSC_CODE, CUST_BANK_BANK, " & vbCrLf & " GST_RGN_NO, GST_REGD, GST_CLASSIFICATION, LOC_DISTANCE "

            SqlStr = SqlStr & vbCrLf & " FROM FIN_SUPP_CUST_REQ_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'" & vbCrLf
            PubDBCn.Execute(SqlStr)

            SqlStr = "DELETE FROM FIN_SUPP_CUST_REQ_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(txtName.Text) & "'"

            PubDBCn.Execute(SqlStr)

        End If
        UpdateAcm = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateAcm = False
        '    Resume				
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh				
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsACMReq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmAcmRequisition_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From FIN_SUPP_CUST_REQ_MST WHERE 1<>1 Order by SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMReq, ADODB.LockTypeEnum.adLockReadOnly)

        If lblRegularized.Text = "Y" Then
            Me.Text = "Account Master Requisition - Regularisation"
        Else
            Me.Text = "Account Master Requisition"
        End If
        '    SqlStr = "Select * From OpOuts Where 1<>1"				
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOpOuts, adLockReadOnly				

        Call AssignGrid(False)

        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT ACM.SUPP_CUST_CODE AS CODE, ACM.SUPP_CUST_NAME AS NAME, " & vbCrLf & " PAIDDAY, ACM.SUPP_CUST_ADDR AS ADDRESS, ACM.SUPP_CUST_CITY AS CITY, " & vbCrLf & " ACM.SUPP_CUST_STATE AS STATE, ACM.SUPP_CUST_PIN as PINCODE, " & vbCrLf & " ACM.SUPP_CUST_PHONE AS PHONE, " & vbCrLf & " ACCOUNT_CODE, DECODE(CST_NO,NULL,' ',CST_NO) AS CST_NO, DECODE(LST_NO,NULL,' ',LST_NO) AS LST_NO, DECODE(PAN_NO,NULL,' ',PAN_NO) AS PAN_NO, " & vbCrLf & " EXCISE_DIV, EXCISE_RANGE, CENT_EXC_RGN_NO, ECC_NO, ACCOUNT_CODE AS TIN_NO," & vbCrLf & " FIN_GROUP_MST.GROUP_NAME" & vbCrLf & " FROM FIN_SUPP_CUST_REQ_MST ACM, FIN_GROUP_MST " & vbCrLf & " WHERE ACM.COMPANY_CODE=FIN_GROUP_MST.COMPANY_CODE(+) " & vbCrLf & " AND ACM.GROUPCODE=FIN_GROUP_MST.GROUP_CODE(+) " & vbCrLf & " AND ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        SqlStr = SqlStr & " ORDER BY ACM.SUPP_CUST_NAME"



        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmAcmRequisition_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmAcmRequisition_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				
        Call SetMainFormCordinate(Me)

        xMyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, xMyMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        FillComboBox()
        SSTInfo.SelectedIndex = 0

        ResizeForm.FindAllControls(Me)

        '    MainClass.Init Me				

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        mAccountCode = CStr(-1)
        txtName.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = IIf(lblRegularized.Text = "Y", True, False)
        txtAlias.Text = ""
        txtVendorCode.Text = ""

        txtPayment.Text = ""
        lblPaymentTerms.Text = ""


        cboCategory.SelectedIndex = 0
        cboCategory.Enabled = True

        cboPaymentMode.SelectedIndex = 0
        cboPaymentMode.Enabled = True


        cboSupplierType.SelectedIndex = 0

        cboHeadType.SelectedIndex = 0
        txtGroupName.Text = ""
        txtGroupNameCr.Text = ""
        txtaddress.Text = ""

        txtCity.Text = ""
        txtPinCode.Text = ""
        txtState.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtMobile.Text = ""

        txtLSTNo.Text = ""
        txtCstNo.Text = ""
        txtPan.Text = ""
        txtEmpCode.Text = ""

        txtDivision.Text = ""
        txtRange.Text = ""
        txtRegnNo.Text = ""
        txtECCNo.Text = ""

        txtCommRate.Text = ""
        optRegd(0).Checked = True
        txtContact.Text = ""
        CboTDSCategory.SelectedIndex = -1
        txtTDSPer.Text = ""
        txtSTDSPer.Text = ""
        txtESIPer.Text = ""
        txtSeq.Text = ""
        txtCurrency.Text = "RS"
        txtChqFrequency.Text = ""

        txtPurchaseSTRecd.Text = ""
        txtPurchaseSTDue.Text = ""
        txtSaleSTRecd.Text = ""
        txtSaleSTDue.Text = ""

        txtCountry.Text = "INDIA"
        txtBuyerName.Text = ""
        txtCarriage.Text = ""
        txtLoadingPort.Text = ""
        txtDischargePort.Text = ""
        txtFinalDest.Text = ""
        txtExportPaymetTerms.Text = ""

        chkState.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkInterUnit.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDistt.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCountry.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAuthorised.CheckState = System.Windows.Forms.CheckState.Unchecked

        If InStr(1, XRIGHT, "S") = 0 Then
            chkAuthorised.Enabled = False
        Else
            chkAuthorised.Enabled = True
        End If

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        optBalMethod(0).Checked = True


        txtRemarks.Text = ""
        OptStatus(0).Checked = True
        If PubSuperUser = "S" Or PubSuperUser = "A" Then
            ChkPoRate.Enabled = True
        Else
            ChkPoRate.Enabled = False
        End If

        ChkPoRate.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkMonthWiseLdgr.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtPaidDay.Text = CStr(3)
        txtPaidDay2.Text = ""
        txtPaidDay3.Text = ""
        txtPaidDay4.Text = ""

        txtTINNo.Text = ""
        txtSrvRegnNo.Text = ""
        SSTInfo.SelectedIndex = 0

        txtServProvided.Text = ""
        txtSection.Text = ""
        txtExptionCNo.Text = ""
        cboCType.SelectedIndex = 0
        txtLDCertiNo.Text = ""
        chkLowerDeduction.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStopMRR.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStopInvoice.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStopGP.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStopBP.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStopMRR.Enabled = True
        chkStopInvoice.Enabled = True
        chkStopGP.Enabled = True
        chkStopBP.Enabled = True

        txtChqFrequency.Enabled = True
        txtPayment.Enabled = True

        txtBankAccountNo.Text = ""
        txtSwitCode.Text = ""
        txtBankBranch.Text = ""
        txtIFSCCode.Text = ""
        txtBankName.Text = ""
        txtDistance.Text = ""

        txtGSTRegnNo.Text = ""
        optGSTRegd(0).Checked = True
        optGSTClassification(0).Checked = True

        txtGSTRegnNo.Enabled = True
        FraGSTClass.Enabled = True
        FraGSTStatus.Enabled = True

        FraStatus.Enabled = True

        Call AutoCompleteSearch("FIN_SUPP_CUST_REQ_MST", "SUPP_CUST_NAME", "", txtName)
        Call AutoCompleteSearch("FIN_SUPP_CUST_REQ_MST", "SUPP_CUST_CODE", "", txtCode)

        Call AutoCompleteSearch("FIN_GROUP_MST", "GROUP_NAME", "GROUP_CATEGORY='G'", txtGroupName)
        Call AutoCompleteSearch("FIN_GROUP_MST", "GROUP_NAME", "GROUP_CATEGORY='G'", txtGroupNameCr)
        Call AutoCompleteSearch("PAY_EMPLOYEE_MST", "EMP_NAME", "", txtEmpCode)


        Call AutoCompleteSearch("FIN_PAYTERM_MST", "PAY_TERM_CODE", "", txtPayment)
        Call AutoCompleteSearch("FIN_CURRENCY_MST", "CURR_DESC", "", txtCurrency)
        Call AutoCompleteSearch("TDS_Section_MST", "NAME", "", txtSection)
        Call AutoCompleteSearch("GEN_STATE_MST", "NAME", "", txtState)

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_TYPE IN ('S','C')", txtBuyerName)

        Call AutoCompleteSearch("FIN_SERVPROV_MST", "NAME", "", txtServProvided)


        MainClass.ButtonStatus(Me, XRIGHT, RsACMReq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume				
    End Sub





    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)

            .set_ColWidth(0, 500)
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 3500)
            .set_ColWidth(3, 500)
            .set_ColWidth(4, 3500)
            .set_ColWidth(5, 1500)
            .set_ColWidth(6, 1500)
            .set_ColWidth(7, 1500)
            .set_ColWidth(8, 1500)

            .set_ColWidth(9, 1500)
            .set_ColWidth(10, 2000)
            .set_ColWidth(11, 2000)
            .set_ColWidth(12, 2000)
            .set_ColWidth(13, 2000)
            .set_ColWidth(14, 2000)
            .set_ColWidth(15, 2000)
            .set_ColWidth(16, 2000)

            .set_ColWidth(17, 2000)
            .set_ColWidth(18, 3500)

            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtName.MaxLength = RsACMReq.Fields("SUPP_CUST_NAME").DefinedSize ''				
        txtCode.MaxLength = RsACMReq.Fields("SUPP_CUST_CODE").DefinedSize ''				
        txtGroupName.MaxLength = MainClass.SetMaxLength("GROUP_NAME", "FIN_GROUP_MST", PubDBCn)
        txtGroupNameCr.MaxLength = MainClass.SetMaxLength("GROUP_NAME", "FIN_GROUP_MST", PubDBCn)
        txtaddress.MaxLength = RsACMReq.Fields("SUPP_CUST_ADDR").DefinedSize ''				
        txtCity.MaxLength = RsACMReq.Fields("SUPP_CUST_CITY").DefinedSize ''				
        txtPinCode.MaxLength = RsACMReq.Fields("SUPP_CUST_PIN").DefinedSize ''				
        txtState.MaxLength = RsACMReq.Fields("SUPP_CUST_STATE").DefinedSize ''				
        txtPhone.MaxLength = RsACMReq.Fields("SUPP_CUST_PHONE").DefinedSize ''				
        txtFax.MaxLength = RsACMReq.Fields("SUPP_CUST_FAXNO").DefinedSize ''				
        txtEmail.MaxLength = RsACMReq.Fields("SUPP_CUST_MAILID").DefinedSize ''				
        txtMobile.MaxLength = RsACMReq.Fields("SUPP_CUST_MOBILE").DefinedSize ''				
        txtAlias.MaxLength = RsACMReq.Fields("ALIAS_NAME").DefinedSize
        txtVendorCode.MaxLength = RsACMReq.Fields("VENDOR_CODE").DefinedSize

        txtLSTNo.MaxLength = RsACMReq.Fields("LST_NO").DefinedSize ''				
        txtCstNo.MaxLength = RsACMReq.Fields("CST_NO").DefinedSize ''				
        txtPan.MaxLength = 10 'RsACMReq.Fields("PAN_NO").DefinedSize           ''				
        txtDivision.MaxLength = RsACMReq.Fields("EXCISE_DIV").DefinedSize ''				
        txtRange.MaxLength = RsACMReq.Fields("EXCISE_RANGE").DefinedSize ''				
        txtRegnNo.MaxLength = RsACMReq.Fields("CENT_EXC_RGN_NO").DefinedSize ''				
        txtECCNo.MaxLength = RsACMReq.Fields("ECC_NO").DefinedSize ''				

        txtCommRate.MaxLength = RsACMReq.Fields("COMMISIONER_RATE").DefinedSize ''				
        txtContact.MaxLength = RsACMReq.Fields("CONTACT_TELNO").DefinedSize ''				
        txtTDSPer.MaxLength = RsACMReq.Fields("TDS_PER").Precision ''				
        txtSTDSPer.MaxLength = RsACMReq.Fields("STDS_PER").Precision
        txtESIPer.MaxLength = RsACMReq.Fields("ESI_PER").Precision
        txtRemarks.MaxLength = RsACMReq.Fields("SUPP_CUST_REMARKS").DefinedSize ''				
        txtTINNo.MaxLength = RsACMReq.Fields("ACCOUNT_CODE").DefinedSize
        txtSrvRegnNo.MaxLength = RsACMReq.Fields("SRV_REGN_NO").DefinedSize
        txtSeq.MaxLength = RsACMReq.Fields("DSP_RPT_SEQ").DefinedSize
        txtCurrency.MaxLength = RsACMReq.Fields("CurrencyName").DefinedSize
        txtEmpCode.MaxLength = RsACMReq.Fields("EMP_CODE").DefinedSize
        txtChqFrequency.MaxLength = 1

        txtPurchaseSTRecd.MaxLength = MainClass.SetMaxLength("NAME", "FIN_STFORM_MST", PubDBCn)
        txtPurchaseSTDue.MaxLength = MainClass.SetMaxLength("NAME", "FIN_STFORM_MST", PubDBCn)
        txtSaleSTRecd.MaxLength = MainClass.SetMaxLength("NAME", "FIN_STFORM_MST", PubDBCn)
        txtSaleSTDue.MaxLength = MainClass.SetMaxLength("NAME", "FIN_STFORM_MST", PubDBCn)

        txtCountry.MaxLength = RsACMReq.Fields("COUNTRY").DefinedSize
        txtExptionCNo.MaxLength = RsACMReq.Fields("EXPTIONCNO").DefinedSize
        txtLDCertiNo.MaxLength = RsACMReq.Fields("LOWER_DED_CERT_NO").DefinedSize
        txtBuyerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_REQ_MST", PubDBCn)
        txtCarriage.MaxLength = RsACMReq.Fields("CARRIAGE").DefinedSize
        txtLoadingPort.MaxLength = RsACMReq.Fields("LOADINGPORT").DefinedSize
        txtDischargePort.MaxLength = RsACMReq.Fields("DISCHARGEPORT").DefinedSize
        txtFinalDest.MaxLength = RsACMReq.Fields("FINALDEST").DefinedSize
        txtExportPaymetTerms.MaxLength = RsACMReq.Fields("PAYMENTTERMS").DefinedSize
        txtServProvided.MaxLength = MainClass.SetMaxLength("NAME", "FIN_SERVPROV_MST", PubDBCn)

        txtPayment.MaxLength = RsACMReq.Fields("PAYMENT_CODE").DefinedSize

        txtBankAccountNo.MaxLength = RsACMReq.Fields("CUST_BANK_ACCT_NO").DefinedSize
        txtSwitCode.MaxLength = RsACMReq.Fields("BANK_SWIFT_CODE").DefinedSize
        txtBankBranch.MaxLength = RsACMReq.Fields("BANK_BRANCH_NAME").DefinedSize
        txtIFSCCode.MaxLength = RsACMReq.Fields("BANK_IFSC_CODE").DefinedSize
        txtBankName.MaxLength = RsACMReq.Fields("CUST_BANK_BANK").DefinedSize
        txtGSTRegnNo.MaxLength = RsACMReq.Fields("GST_RGN_NO").DefinedSize


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub

    Function FieldVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        FieldVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldVarification = False
            Exit Function
        End If
        If txtName.Text = "" Then
            MsgInformation("Account Name is empty. Cannot Save")
            txtName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If lblRegularized.Text = "Y" Then
            If txtCode.Text = "" Then
                MsgInformation("Account Code is empty. Cannot Save")
                If txtCode.Enabled Then txtCode.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Val(txtCode.Text) = 0 Then
                MsgInformation("Invalid Account Code. Cannot Save")
                If txtCode.Enabled Then txtCode.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Len(Trim(txtCode.Text)) <> 5 And ADDMode = True Then
                MsgInformation("Account Code Must be Five Digit. Cannot Save")
                If txtCode.Enabled Then txtCode.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True Then
            If IsDate(lblAddDate.Text) = False Then
                lblAddDate.Text = RsCompany.Fields("Start_Date").Value
            End If
            If MainClass.GetUserCanModifyMaster(lblAddDate.Text, XRIGHT) = False Then
                MsgBox("You Have Not Rights to change back Entry.", vbInformation)
                FieldVarification = False
                Exit Function
            End If
        End If

        If lblRegularized.Text = "Y" Then
            'If PubHO = "Y" Then
            '    If VB.Left(cboCategory.Text, 1) = "C" Then
            '        If Val(txtCode.Text) >= 10001 And Val(txtCode.Text) <= 15000 Then
            '        Else
            '            MsgInformation("Customer Code should be between 10001 and 15000. Cannot Save")
            '            If txtCode.Enabled Then txtCode.Focus()
            '            FieldVarification = False
            '            Exit Function
            '        End If
            '    ElseIf VB.Left(cboCategory.Text, 1) = "S" Then
            '        If Val(txtCode.Text) >= 22001 And Val(txtCode.Text) <= 30000 Then
            '        Else
            '            MsgInformation("Supplier Code should be between 22001 and 30000. Cannot Save")
            '            If txtCode.Enabled Then txtCode.Focus()
            '            FieldVarification = False
            '            Exit Function
            '        End If

            '    ElseIf VB.Left(cboCategory.Text, 1) = "E" Then
            '        If Val(txtCode.Text) >= 31001 And Val(txtCode.Text) <= 35000 Then
            '        Else
            '            MsgInformation("Employee Code should be between 31001 and 35000. Cannot Save")
            '            If txtCode.Enabled Then txtCode.Focus()
            '            FieldVarification = False
            '            Exit Function
            '        End If

            '    Else
            '        If Val(txtCode.Text) >= 16001 And Val(txtCode.Text) <= 22000 Then
            '        Else
            '            MsgInformation("Cash/Bank/Assets/Others Code should be between 16001 and 22000. Cannot Save")
            '            If txtCode.Enabled Then txtCode.Focus()
            '            FieldVarification = False
            '            Exit Function
            '        End If
            '    End If
            'Else
            '    If VB.Left(cboCategory.Text, 1) = "C" Then
            '        If Val(txtCode.Text) >= 50001 And Val(txtCode.Text) <= 65000 Then
            '        Else
            '            MsgInformation("Customer Code should be between 50001 and 65000. Cannot Save")
            '            If txtCode.Enabled Then txtCode.Focus()
            '            FieldVarification = False
            '            Exit Function
            '        End If
            '    ElseIf VB.Left(cboCategory.Text, 1) = "S" Then
            '        If Val(txtCode.Text) >= 72001 And Val(txtCode.Text) <= 80000 Then
            '        Else
            '            MsgInformation("Supplier Code should be between 72001 and 80000. Cannot Save")
            '            If txtCode.Enabled Then txtCode.Focus()
            '            FieldVarification = False
            '            Exit Function
            '        End If

            '    ElseIf VB.Left(cboCategory.Text, 1) = "E" Then
            '        If Val(txtCode.Text) >= 81001 And Val(txtCode.Text) <= 85000 Then
            '        Else
            '            MsgInformation("Employee Code should be between 81001 and 85000. Cannot Save")
            '            If txtCode.Enabled Then txtCode.Focus()
            '            FieldVarification = False
            '            Exit Function
            '        End If

            '    Else
            '        If Val(txtCode.Text) >= 66001 And Val(txtCode.Text) <= 72000 Then
            '        Else
            '            MsgInformation("Cash/Bank/Assets/Others Code should be between 66001 and 72000. Cannot Save")
            '            If txtCode.Enabled Then txtCode.Focus()
            '            FieldVarification = False
            '            Exit Function
            '        End If
            '    End If
            'End If
        End If
        If cboCategory.Text = "" Then
            MsgInformation("Category is must.")
            FieldVarification = False
            cboCategory.Focus()
            Exit Function
        End If

        If Trim(UCase(txtGroupName.Text)) = "" Then
            MsgInformation("Group Can Not Be Blank.")
            txtGroupName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtGSTRegnNo.Text) = "" And optGSTRegd(0).Checked = True Then
            MsgBox("Please enter the GST Regn No.", MsgBoxStyle.Information)
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtGSTRegnNo.Text) <> "" And optGSTRegd(0).Checked = False Then
            MsgBox("Please Click in GST Regn", MsgBoxStyle.Information)
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtGSTRegnNo.Text) <> "" Then
            If CheckGSTValidation(Trim(txtGSTRegnNo.Text), Trim(txtState.Text)) = False Then
                MsgBox("Invalid GST Regn No., so that cann't be save.", MsgBoxStyle.Information)
                FieldVarification = False
                Exit Function
            Else
                txtGSTRegnNo.Text = Trim(txtGSTRegnNo.Text)
            End If
        End If

        If Val(txtDistance.Text) = 0 Then
            MsgInformation("Please Enter party location distance from our Premises.")
            FieldVarification = False
            Exit Function
        End If

        If VB.Left(cboHeadType.Text, 1) = "P" Then
            mSqlStr = "SELECT SUPP_CUST_CODE, SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='P'"
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                MsgInformation("Profit & Loss You Already Define for Account Name :(" & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value) & ")" & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                cboHeadType.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If VB.Left(cboHeadType.Text, 1) = "4" And VB.Left(cboCategory.Text, 1) <> "O" Then
            MsgInformation("You can select Service Head for only Account GL.")
            cboHeadType.Focus()
            FieldVarification = False
            Exit Function
        End If

        '    If Left(cboHeadType.Text, 1) = "G" Then				
        '        mSqlStr = "SELECT SUPP_CUST_CODE, SUPP_CUST_NAME FROM FIN_SUPP_CUST_REQ_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='G' AND SUPP_CUST_CODE<>'" & Trim(txtCode.Text) & "'"				
        '        MainClass.UOpenRecordSet mSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly				
        '        If RsTemp.EOF = False Then				
        '            MsgInformation "Gratuity you already define for Account Name :(" & IIf(IsNull(RsTemp!SUPP_CUST_CODE), "", RsTemp!SUPP_CUST_CODE) & ")" & IIf(IsNull(RsTemp!SUPP_CUST_NAME), "", RsTemp!SUPP_CUST_NAME)				
        '            cboHeadType.SetFocus				
        '            FieldVarification = False				
        '            Exit Function				
        '        End If				
        '    End If				

        If MainClass.ValidateWithMasterTable(txtGroupName.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Group Not Defined")
            txtGroupName.Focus()
            FieldVarification = False
            Exit Function
        End If


        If Trim(UCase(txtGroupNameCr.Text)) = "" Then
            MsgInformation("Group Can Not Be Blank.")
            txtGroupNameCr.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtGroupNameCr.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Group Not Defined")
            txtGroupNameCr.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Val(txtTDSPer.Text) > 100 Then
            FieldVarification = False
            MsgBox("TDS % cann't be greater than 100", MsgBoxStyle.Information)
            txtTDSPer.Focus()
            Exit Function
        End If

        If Val(txtSTDSPer.Text) > 100 Then
            FieldVarification = False
            MsgBox("STDS % cann't be greater than 100", MsgBoxStyle.Information)
            txtSTDSPer.Focus()
            Exit Function
        End If

        If Val(txtESIPer.Text) > 100 Then
            FieldVarification = False
            MsgBox("ESI % cann't be greater than 100", MsgBoxStyle.Information)
            txtESIPer.Focus()
            Exit Function
        End If

        If Val(txtPaidDay.Text) > 31 Then
            FieldVarification = False
            MsgBox("Paid Day cann't be greater than 31", MsgBoxStyle.Information)
            txtPaidDay.Focus()
            Exit Function
        End If

        If Val(txtPaidDay2.Text) > 31 Then
            FieldVarification = False
            MsgBox("Paid Day cann't be greater than 31", MsgBoxStyle.Information)
            txtPaidDay2.Focus()
            Exit Function
        End If

        If Val(txtPaidDay3.Text) > 31 Then
            FieldVarification = False
            MsgBox("Paid Day cann't be greater than 31", MsgBoxStyle.Information)
            txtPaidDay3.Focus()
            Exit Function
        End If

        If Val(txtPaidDay4.Text) > 31 Then
            FieldVarification = False
            MsgBox("Paid Day cann't be greater than 31", MsgBoxStyle.Information)
            txtPaidDay4.Focus()
            Exit Function
        End If

        If VB.Left(cboHeadType.Text, 1) = "L" Or VB.Left(cboHeadType.Text, 1) = "I" Then
            If Trim(txtEmpCode.Text) = "" Then
                MsgInformation("Emp Code Can Not Be Blank.")
                txtEmpCode.Focus()
                FieldVarification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                ErrorMsg("Invalid Emp Code.", , MsgBoxStyle.Information)
                txtEmpCode.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If (VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C") And optBalMethod(0).Checked = True Then
            MsgInformation("Please Check Balancing Method.")
            FieldVarification = False
            Exit Function
        End If

        If (VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C") And Trim(txtPayment.Text) = "" Then
            MsgInformation("Please Check Payment Terms.")
            txtPayment.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtCurrency.Text) = "" Then
            MsgInformation("Please Enter Currency. Help For Press F1 Key.")
            txtCurrency.Focus()
            FieldVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtCurrency.Text, "CURR_DESC", "CURR_DESC", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Please Enter Valid Currency. Help For Press F1 Key.")
                txtCurrency.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If Trim(txtaddress.Text) = "" Then
            MsgInformation("Please Enter Address.")
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtCity.Text) = "" Then
            MsgInformation("Please Enter City.")
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtState.Text) = "" Then
            MsgInformation("Please Enter State.")
            FieldVarification = False
            Exit Function
        End If

        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then
            If MainClass.ValidateWithMasterTable(txtState.Text, "NAME", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid State Name")
                SSTInfo.SelectedIndex = 0
                If txtState.Enabled = True Then txtState.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) = UCase(Trim(txtState.Text)) And chkState.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                MsgInformation("Please Select Within State.")
                chkState.Focus()
                FieldVarification = False
                Exit Function
            End If

            If chkLowerDeduction.CheckState = System.Windows.Forms.CheckState.Checked And Trim(txtLDCertiNo.Text) = "" Then
                MsgInformation("Please Enter the Lower Deduction Certificate No.")
                txtLDCertiNo.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) <> UCase(Trim(txtState.Text)) And chkState.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Not In Within State.")
                chkState.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) <> UCase(Trim(txtState.Text)) And chkDistt.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Not In Within Distt.")
                chkDistt.Focus()
                FieldVarification = False
                Exit Function
            End If

            If chkMonthWiseLdgr.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("You Cann't Select Month wise Ledger For Supplier or Customer.")
                chkMonthWiseLdgr.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If Trim(txtPinCode.Text) = "" Then
            MsgInformation("Please Enter Pin Code.")
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtCountry.Text) = "" Then
            MsgInformation("Please Enter Country Name.")
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtBuyerName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_REQ_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
                MsgInformation("Invalid Buyer.")
                FieldVarification = False
                Exit Function
            End If
        End If

        If (VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C") And chkCountry.CheckState = System.Windows.Forms.CheckState.Checked Then
            If cboSupplierType.Text = "CONTRACTOR" Then
                If Trim(txtPan.Text) = "" Then
                    MsgInformation("PAN NO Cann't be Blank.")
                    FieldVarification = False
                    txtPan.Focus()
                    Exit Function
                End If
            End If
            If Trim(txtPan.Text) <> "" Then
                If CheckPANValidation((txtPan.Text)) = False Then
                    MsgInformation("Invalid PAN No.")
                    FieldVarification = False
                    txtPan.Focus()
                    Exit Function
                End If
            End If
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmAcmRequisition_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        ResizeForm.ResizeAllControls(Me) '    MainClass.FormResize Me				
    End Sub

    Private Sub frmAcmRequisition_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    'If PvtDBCn.State = adStateOpen Then				
        '				
        '        ''PvtDBCn.Close				
        '        ''Set PvtDBCn = Nothing				
        '    End If				
        Me.Hide()
        Me.Close()
        RsACMReq.Close()
        'RsOpOuts.Close				
    End Sub

    Private Sub optBalMethod_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBalMethod.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBalMethod.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optRegd_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optRegd.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optRegd.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub txtAlias_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAlias.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAlias_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAlias.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAlias.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankAccountNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankAccountNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankAccountNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankAccountNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSwitCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSwitCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSwitCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSwitCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankBranch_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankBranch.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankBranch_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankBranch.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGSTRegnNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTRegnNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGSTRegnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTRegnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGSTRegnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtIFSCCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIFSCCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIFSCCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIFSCCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDistance_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDistance.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDistance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDistance.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub optGSTClassification_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optGSTClassification.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optGSTClassification.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optGSTRegd_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optGSTRegd.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optGSTRegd.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub


    Private Sub txtBuyerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBuyerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBuyerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBuyerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBuyerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtBuyerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBuyerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtBuyerName.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_REQ_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Buyer.", vbInformation)
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCarriage_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriage.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCarriage_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriage.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriage.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtChqFrequency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqFrequency.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqFrequency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqFrequency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCountry_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCountry.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCountry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCountry.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCountry.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCurrency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCurrency.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCurrency_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCurrency.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub


    Private Sub txtCurrency_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCurrency.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtCurrency.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtCurrency.Text, "CURR_DESC", "CURR_DESC", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Currency.", vbInformation)
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDischargePort_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDischargePort.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDischargePort_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDischargePort.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDischargePort.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExptionCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExptionCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExptionCNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFinalDest_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFinalDest.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFinalDest_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFinalDest.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFinalDest.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLDCertiNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLDCertiNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLDCertiNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLDCertiNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLDCertiNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLOADINGPORT_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoadingPort.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLOADINGPORT_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLoadingPort.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLoadingPort.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaidDay2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDay2.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDay2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDay2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaidDay3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDay3.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDay3_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDay3.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaidDay4_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDay4.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDay4_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDay4.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExportPaymetTerms_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExportPaymetTerms.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExportPaymetTerms_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExportPaymetTerms.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExportPaymetTerms.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPayment.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPurchaseSTDue_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurchaseSTDue.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtPurchaseSTDue_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurchaseSTDue.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPurchaseSTDue.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPurchaseSTDue_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurchaseSTDue.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtPurchaseSTDue_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurchaseSTDue.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtPurchaseSTDue.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " '& vbCrLf |            & " AND IDENTIFICATION='ST'"				

        If MainClass.ValidateWithMasterTable(txtPurchaseSTDue, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Form Type.", "", vbCritical)
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPurchaseSTRecd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurchaseSTRecd.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPurchaseSTRecd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurchaseSTRecd.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPurchaseSTRecd.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPurchaseSTRecd_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurchaseSTRecd.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtPurchaseSTRecd_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurchaseSTRecd.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtPurchaseSTRecd.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " '& vbCrLf |            & " AND IDENTIFICATION='ST'"				

        If MainClass.ValidateWithMasterTable(txtPurchaseSTRecd, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Form Type.", "", vbCritical)
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSaleSTDue_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleSTDue.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSaleSTDue_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleSTDue.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleSTDue.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSaleSTDue_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleSTDue.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtSaleSTDue.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " '& vbCrLf |            & " AND IDENTIFICATION='ST'"				

        If MainClass.ValidateWithMasterTable(txtSaleSTDue, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Form Type.", "", vbCritical)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSaleSTRecd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleSTRecd.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSaleSTRecd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleSTRecd.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSaleSTRecd.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub





    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        If eventArgs.row < 1 Then Exit Sub

        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        txtName.Text = Trim(SprdView.Text)
        TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub





    Private Sub txtaddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtaddress.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtaddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtaddress.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtaddress.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtCity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCity.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Public Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        '    If MODIFYMode = True And RsACMReq.EOF = False Then mAccountCode = RsACMReq.Fields("SUPP_CUST_CODE").Value				

        SqlStr = "Select * From FIN_SUPP_CUST_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            MsgBox("Code Already Exist In Master. Please Enter Other Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
            '    Else				
            '        If ADDMode = False And MODIFYMode = False Then				
            '            MsgBox "Name Does Not Exist In Master, Click Add To Add In Master", vbInformation				
            '            Cancel = True				
            '        ElseIf MODIFYMode = True Then				
            '            SqlStr = "Select * From FIN_SUPP_CUST_REQ_MST " & vbCrLf _				
            ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _				
            ''                    & " AND SUPP_CUST_CODE=" & mAccountCode & ""				
            '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsACMReq, adLockReadOnly				
            '        End If				
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCommRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCommRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCommRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCommRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCommRate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtContact_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContact.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtContact_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContact.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtContact.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtCSTNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCstNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtCSTNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCstNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCstNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCurrency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCurrency.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDivision_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDivision.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDivision.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtECCNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtECCNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtECCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtECCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtECCNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmail.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '    KeyAscii = MainClass.UpperCase(KeyAscii, txtEmail.Text)				
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFax.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub

        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")

        If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Emp Code.", , vbInformation)
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtESIPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtESIPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtExptionCNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExptionCNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFax_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFax.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFax_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFax.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFax.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtGroupName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGroupName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGroupName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGroupName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGroupName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGroupName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGroupName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtGroupName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGroupName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If Trim(txtGroupName.Text) = "" Then
            ErrorMsg("Group Cann't be Blank.", , MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtGroupName.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Group.", , vbInformation)
            Cancel = True
        End If

        If Trim(txtGroupNameCr.Text) = "" Then
            txtGroupNameCr.Text = Trim(txtGroupName.Text)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtGroupNameCr_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGroupNameCr.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGroupNameCr_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGroupNameCr.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGroupNameCr.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGroupNameCr_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGroupNameCr.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

    End Sub

    Private Sub txtGroupNameCr_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGroupNameCr.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If Trim(txtGroupNameCr.Text) = "" Then
            ErrorMsg("Group Cann't be Blank.", , MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtGroupNameCr.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Group.", , vbInformation)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtLSTNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLSTNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtLSTNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLSTNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLSTNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMobile_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMobile.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMobile_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMobile.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMobile.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

    End Sub

    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mAccountName As String = ""

        If Trim(txtName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            MsgBox("Name Already Exist In Master.", vbInformation)
            Cancel = True
            Exit Sub
        End If

        If MODIFYMode = True And RsACMReq.EOF = False Then mAccountName = RsACMReq.Fields("SUPP_CUST_NAME").Value
        SqlStr = "Select * From FIN_SUPP_CUST_REQ_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMReq, ADODB.LockTypeEnum.adLockReadOnly)

        If RsACMReq.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From FIN_SUPP_CUST_REQ_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & mAccountName & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMReq, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        '    Resume				
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mBalOpt As Integer
        Dim mControlBranchCode As Integer
        Dim mBuyerCode As String
        Dim mServiceProviderCode As Double
        Dim mIsAuthorisedUser As String

        Clear1()
        If Not RsACMReq.EOF Then

            mAccountCode = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_CODE").Value), -1, RsACMReq.Fields("SUPP_CUST_CODE").Value)
            txtName.Text = Trim(IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_NAME").Value), "", RsACMReq.Fields("SUPP_CUST_NAME").Value))
            txtCode.Text = Trim(IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_CODE").Value), "", RsACMReq.Fields("SUPP_CUST_CODE").Value))

            txtEmpCode.Text = Trim(IIf(IsDBNull(RsACMReq.Fields("EMP_CODE").Value), "", RsACMReq.Fields("EMP_CODE").Value))
            txtAlias.Text = Trim(IIf(IsDBNull(RsACMReq.Fields("ALIAS_NAME").Value), "", RsACMReq.Fields("ALIAS_NAME").Value))
            txtVendorCode.Text = Trim(IIf(IsDBNull(RsACMReq.Fields("VENDOR_CODE").Value), "", RsACMReq.Fields("VENDOR_CODE").Value))

            txtCode.Enabled = IIf(lblRegularized.Text = "Y", True, False)

            If IsDBNull(RsACMReq.Fields("GROUPCODE").Value) Then
                txtGroupName.Text = ""
            End If


            If IsDBNull(RsACMReq.Fields("GROUPCODECR").Value) Then
                txtGroupNameCr.Text = ""
            End If

            txtaddress.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_ADDR").Value), "", RsACMReq.Fields("SUPP_CUST_ADDR").Value)
            txtCity.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_CITY").Value), "", RsACMReq.Fields("SUPP_CUST_CITY").Value)
            txtPinCode.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_PIN").Value), "", RsACMReq.Fields("SUPP_CUST_PIN").Value)
            txtState.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_STATE").Value), "", RsACMReq.Fields("SUPP_CUST_STATE").Value)
            txtPhone.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_PHONE").Value), "", RsACMReq.Fields("SUPP_CUST_PHONE").Value)
            txtFax.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_FAXNO").Value), "", RsACMReq.Fields("SUPP_CUST_FAXNO").Value)
            txtEmail.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_MAILID").Value), "", RsACMReq.Fields("SUPP_CUST_MAILID").Value)
            txtMobile.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_MOBILE").Value), "", RsACMReq.Fields("SUPP_CUST_MOBILE").Value)

            txtLSTNo.Text = IIf(IsDBNull(RsACMReq.Fields("LST_NO").Value), "", RsACMReq.Fields("LST_NO").Value)
            txtCstNo.Text = IIf(IsDBNull(RsACMReq.Fields("CST_NO").Value), "", RsACMReq.Fields("CST_NO").Value)
            txtPan.Text = IIf(IsDBNull(RsACMReq.Fields("PAN_NO").Value), "", RsACMReq.Fields("PAN_NO").Value)
            txtDivision.Text = IIf(IsDBNull(RsACMReq.Fields("EXCISE_DIV").Value), "", RsACMReq.Fields("EXCISE_DIV").Value)
            txtRange.Text = IIf(IsDBNull(RsACMReq.Fields("EXCISE_RANGE").Value), "", RsACMReq.Fields("EXCISE_RANGE").Value)
            txtRegnNo.Text = IIf(IsDBNull(RsACMReq.Fields("CENT_EXC_RGN_NO").Value), "", RsACMReq.Fields("CENT_EXC_RGN_NO").Value)
            txtECCNo.Text = IIf(IsDBNull(RsACMReq.Fields("ECC_NO").Value), "", RsACMReq.Fields("ECC_NO").Value)

            txtCommRate.Text = IIf(IsDBNull(RsACMReq.Fields("COMMISIONER_RATE").Value), "", RsACMReq.Fields("COMMISIONER_RATE").Value)

            If RsACMReq.Fields("REGD_DEALER").Value = "Y" Then
                optRegd(0).Checked = True
            Else
                optRegd(1).Checked = True
            End If

            txtContact.Text = IIf(IsDBNull(RsACMReq.Fields("CONTACT_TELNO").Value), "", RsACMReq.Fields("CONTACT_TELNO").Value)
            CboTDSCategory.Text = IIf(IsDBNull(RsACMReq.Fields("TDSCATEGORY").Value), "NONE", RsACMReq.Fields("TDSCATEGORY").Value)

            txtCurrency.Text = IIf(IsDBNull(RsACMReq.Fields("CURRENCYNAME").Value), "Rs.", RsACMReq.Fields("CURRENCYNAME").Value)

            If IsDBNull(RsACMReq.Fields("TYPE_OF_SUPPLIER").Value) Then

            Else
                cboSupplierType.Text = IIf(IsDBNull(RsACMReq.Fields("TYPE_OF_SUPPLIER").Value), "", RsACMReq.Fields("TYPE_OF_SUPPLIER").Value)
            End If

            txtTDSPer.Text = VB6.Format(IIf(IsDBNull(RsACMReq.Fields("TDS_PER").Value), 0, RsACMReq.Fields("TDS_PER").Value), "0.000")
            txtSTDSPer.Text = VB6.Format(IIf(IsDBNull(RsACMReq.Fields("STDS_PER").Value), 0, RsACMReq.Fields("STDS_PER").Value), "0.000")
            txtESIPer.Text = VB6.Format(IIf(IsDBNull(RsACMReq.Fields("ESI_PER").Value), 0, RsACMReq.Fields("ESI_PER").Value), "0.000")

            chkState.CheckState = IIf(RsACMReq.Fields("WITHIN_STATE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkInterUnit.CheckState = IIf(RsACMReq.Fields("INTER_UNIT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkCountry.CheckState = IIf(RsACMReq.Fields("WITHIN_COUNTRY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkAuthorised.CheckState = IIf(RsACMReq.Fields("AUTHORISED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAuthorised.Enabled = IIf(RsACMReq.Fields("AUTHORISED").Value = "Y", False, True)

            ChkPoRate.CheckState = IIf(RsACMReq.Fields("PORATEEDITABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkMonthWiseLdgr.CheckState = IIf(RsACMReq.Fields("MONTHWISE_LDGR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkDistt.CheckState = IIf(RsACMReq.Fields("WITHIN_DISTT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            txtRemarks.Text = IIf(IsDBNull(RsACMReq.Fields("SUPP_CUST_REMARKS").Value), "", RsACMReq.Fields("SUPP_CUST_REMARKS").Value)
            txtTINNo.Text = IIf(IsDBNull(RsACMReq.Fields("ACCOUNT_CODE").Value), "", RsACMReq.Fields("ACCOUNT_CODE").Value)
            txtSrvRegnNo.Text = IIf(IsDBNull(RsACMReq.Fields("SRV_REGN_NO").Value), "", RsACMReq.Fields("SRV_REGN_NO").Value)

            txtSeq.Text = IIf(IsDBNull(RsACMReq.Fields("DSP_RPT_SEQ").Value), "", RsACMReq.Fields("DSP_RPT_SEQ").Value)
            txtChqFrequency.Text = CStr(Val(IIf(IsDBNull(RsACMReq.Fields("ACTIVITY").Value), "", RsACMReq.Fields("ACTIVITY").Value)))

            If RsACMReq.Fields("BALANCINGMETHOD").Value = "S" Then
                optBalMethod(0).Checked = True
            Else
                optBalMethod(1).Checked = True
            End If

            If RsACMReq.Fields("STATUS").Value = "O" Then
                OptStatus(0).Checked = True
                FraStatus.Enabled = True
            Else
                OptStatus(1).Checked = True
                '            FraStatus.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)				
                mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, xMyMenu, PubDBCn)
                If InStr(1, mIsAuthorisedUser, "S") = 0 Then
                    FraStatus.Enabled = False
                Else
                    FraStatus.Enabled = True
                End If
            End If

            chkStopMRR.CheckState = IIf(RsACMReq.Fields("STOP_MRR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkStopInvoice.CheckState = IIf(RsACMReq.Fields("STOP_INVOICE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkStopGP.CheckState = IIf(RsACMReq.Fields("STOP_RGP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkStopBP.CheckState = IIf(RsACMReq.Fields("STOP_BANK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, xMyMenu, PubDBCn)
            If InStr(1, mIsAuthorisedUser, "S") = 0 Then
                chkStopMRR.Enabled = IIf(chkStopMRR.CheckState = System.Windows.Forms.CheckState.Checked, IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False), True)
                chkStopInvoice.Enabled = IIf(chkStopInvoice.CheckState = System.Windows.Forms.CheckState.Checked, IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False), True)
                chkStopGP.Enabled = IIf(chkStopGP.CheckState = System.Windows.Forms.CheckState.Checked, IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False), True)
                chkStopBP.Enabled = IIf(chkStopBP.CheckState = System.Windows.Forms.CheckState.Checked, IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False), True)
            Else
                chkStopMRR.Enabled = True
                chkStopInvoice.Enabled = True
                chkStopGP.Enabled = True
                chkStopBP.Enabled = True
            End If

            txtPaidDay.Text = CStr(Val(IIf(IsDBNull(RsACMReq.Fields("PaidDay").Value), 1, RsACMReq.Fields("PaidDay").Value)))

            txtPaidDay2.Text = CStr(Val(IIf(IsDBNull(RsACMReq.Fields("PaidDay2").Value), 0, RsACMReq.Fields("PaidDay2").Value)))
            txtPaidDay3.Text = CStr(Val(IIf(IsDBNull(RsACMReq.Fields("PaidDay3").Value), 0, RsACMReq.Fields("PaidDay3").Value)))
            txtPaidDay4.Text = CStr(Val(IIf(IsDBNull(RsACMReq.Fields("PaidDay4").Value), 0, RsACMReq.Fields("PaidDay4").Value)))

            Call SetCombo(cboCategory, (RsACMReq.Fields("SUPP_CUST_TYPE").Value))
            Call SetCombo(cboHeadType, IIf(IsDBNull(RsACMReq.Fields("HEADTYPE").Value), "", RsACMReq.Fields("HEADTYPE").Value))

            If RsACMReq.Fields("CType").Value = "C" Then
                cboCType.SelectedIndex = 0
            Else
                cboCType.SelectedIndex = 1
            End If

            '        If RsACMReq!PAYMENT_MODE = "1" Then				
            '            cboPaymentMode.ListIndex = 0				
            '        ElseIf RsACMReq!PAYMENT_MODE = "2" Then				
            '            cboPaymentMode.ListIndex = 1				
            '        Else				
            '            cboPaymentMode.ListIndex = 2				
            '        End If				

            If RsACMReq.Fields("PAYMENT_MODE").Value = "1" Then
                cboPaymentMode.SelectedIndex = 0
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "2" Then
                cboPaymentMode.SelectedIndex = 1
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "3" Then
                cboPaymentMode.SelectedIndex = 2
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "4" Then
                cboPaymentMode.SelectedIndex = 3
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "5" Then
                cboPaymentMode.SelectedIndex = 4
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "6" Then
                cboPaymentMode.SelectedIndex = 5
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "7" Then
                cboPaymentMode.SelectedIndex = 6
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "8" Then
                cboPaymentMode.SelectedIndex = 7
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "9" Then
                cboPaymentMode.SelectedIndex = 8
            ElseIf RsACMReq.Fields("PAYMENT_MODE").Value = "A" Then
                cboPaymentMode.SelectedIndex = 9
            Else
                cboPaymentMode.SelectedIndex = 2
            End If

            If MainClass.ValidateWithMasterTable(RsACMReq.Fields("SECTIONCODE"), "Code", "Name", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSection.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If

            txtExptionCNo.Text = IIf(IsDBNull(RsACMReq.Fields("EXPTIONCNO").Value), "", RsACMReq.Fields("EXPTIONCNO").Value)
            txtLDCertiNo.Text = IIf(IsDBNull(RsACMReq.Fields("LOWER_DED_CERT_NO").Value), "", RsACMReq.Fields("LOWER_DED_CERT_NO").Value)
            chkLowerDeduction.CheckState = IIf(RsACMReq.Fields("IS_LOWER_DED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            If IsDBNull(RsACMReq.Fields("PUR_STRECD_FORMCODE").Value) Then
                txtPurchaseSTRecd.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsACMReq.Fields("PUR_STRECD_FORMCODE").Value, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtPurchaseSTRecd.Text = MasterNo
            End If

            If IsDBNull(RsACMReq.Fields("PUR_STDUE_FORMCODE").Value) Then
                txtPurchaseSTDue.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsACMReq.Fields("PUR_STDUE_FORMCODE").Value, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtPurchaseSTDue.Text = MasterNo
            End If

            If IsDBNull(RsACMReq.Fields("SALE_STRECD_FORMCODE").Value) Then
                txtSaleSTRecd.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsACMReq.Fields("SALE_STRECD_FORMCODE").Value, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSaleSTRecd.Text = MasterNo
            End If

            If IsDBNull(RsACMReq.Fields("SALE_STDUE_FORMCODE").Value) Then
                txtSaleSTDue.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(RsACMReq.Fields("SALE_STDUE_FORMCODE").Value, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSaleSTDue.Text = MasterNo
            End If



            '********09-03-2005				
            txtCountry.Text = IIf(IsDBNull(RsACMReq.Fields("COUNTRY").Value), "", RsACMReq.Fields("COUNTRY").Value)

            mBuyerCode = IIf(IsDBNull(RsACMReq.Fields("BUYERCODE").Value), "", RsACMReq.Fields("BUYERCODE").Value)

            If mBuyerCode = "" Then
                txtBuyerName.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_REQ_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBuyerName.Text = MasterNo
                End If
            End If

            mServiceProviderCode = IIf(IsDBNull(RsACMReq.Fields("SERVPROV_CODE").Value), -1, RsACMReq.Fields("SERVPROV_CODE").Value)

            If mServiceProviderCode = -1 Then
                txtServProvided.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(mServiceProviderCode, "CODE", "NAME", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtServProvided.Text = MasterNo
                End If
            End If

            txtCarriage.Text = IIf(IsDBNull(RsACMReq.Fields("CARRIAGE").Value), "", RsACMReq.Fields("CARRIAGE").Value)
            txtLoadingPort.Text = IIf(IsDBNull(RsACMReq.Fields("LOADINGPORT").Value), "", RsACMReq.Fields("LOADINGPORT").Value)
            txtDischargePort.Text = IIf(IsDBNull(RsACMReq.Fields("DISCHARGEPORT").Value), "", RsACMReq.Fields("DISCHARGEPORT").Value)
            txtFinalDest.Text = IIf(IsDBNull(RsACMReq.Fields("FINALDEST").Value), "", RsACMReq.Fields("FINALDEST").Value)
            txtExportPaymetTerms.Text = IIf(IsDBNull(RsACMReq.Fields("PAYMENTTERMS").Value), "", RsACMReq.Fields("PAYMENTTERMS").Value)

            txtPayment.Text = IIf(IsDBNull(RsACMReq.Fields("PAYMENT_CODE").Value), "", RsACMReq.Fields("PAYMENT_CODE").Value)
            If MainClass.ValidateWithMasterTable(txtPayment.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblPaymentTerms.Text = MasterNo
            End If

            txtBankAccountNo.Text = IIf(IsDBNull(RsACMReq.Fields("CUST_BANK_ACCT_NO").Value), "", RsACMReq.Fields("CUST_BANK_ACCT_NO").Value)
            txtSwitCode.Text = IIf(IsDBNull(RsACMReq.Fields("BANK_SWIFT_CODE").Value), "", RsACMReq.Fields("BANK_SWIFT_CODE").Value)
            txtBankBranch.Text = IIf(IsDBNull(RsACMReq.Fields("BANK_BRANCH_NAME").Value), "", RsACMReq.Fields("BANK_BRANCH_NAME").Value)
            txtIFSCCode.Text = IIf(IsDBNull(RsACMReq.Fields("BANK_IFSC_CODE").Value), "", RsACMReq.Fields("BANK_IFSC_CODE").Value)
            txtBankName.Text = IIf(IsDBNull(RsACMReq.Fields("CUST_BANK_BANK").Value), "", RsACMReq.Fields("CUST_BANK_BANK").Value)

            txtGSTRegnNo.Text = IIf(IsDBNull(RsACMReq.Fields("GST_RGN_NO").Value), "", RsACMReq.Fields("GST_RGN_NO").Value)

            If RsACMReq.Fields("GST_REGD").Value = "Y" Then
                optGSTRegd(0).Checked = True
            ElseIf RsACMReq.Fields("GST_REGD").Value = "N" Then
                optGSTRegd(1).Checked = True
            ElseIf RsACMReq.Fields("GST_REGD").Value = "E" Then
                optGSTRegd(2).Checked = True
            ElseIf RsACMReq.Fields("GST_REGD").Value = "F" Then
                optGSTRegd(3).Checked = True
            ElseIf RsACMReq.Fields("GST_REGD").Value = "C" Then
                optGSTRegd(4).Checked = True
            End If

            If RsACMReq.Fields("GST_CLASSIFICATION").Value = "F" Then
                optGSTClassification(0).Checked = True
            Else
                optGSTClassification(1).Checked = True
            End If

            If Trim(txtGSTRegnNo.Text) <> "" Then
                txtGSTRegnNo.Enabled = False
                FraGSTClass.Enabled = False
                FraGSTStatus.Enabled = False
            End If

            lblAddUser.Text = IIf(IsDBNull(RsACMReq.Fields("ADDUSER").Value), "", RsACMReq.Fields("ADDUSER").Value)
            lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsACMReq.Fields("ADDDATE").Value), "", RsACMReq.Fields("ADDDATE").Value), "dd/MM/yyyy")
            lblModUser.Text = IIf(IsDBNull(RsACMReq.Fields("MODUSER").Value), "", RsACMReq.Fields("MODUSER").Value)
            lblModDate.Text = VB6.Format(IIf(IsDBNull(RsACMReq.Fields("MODDATE").Value), "", RsACMReq.Fields("MODDATE").Value), "dd/MM/yyyy")
            txtChqFrequency.Enabled = False
            txtPayment.Enabled = False

            '        OPBalType				

            'Field Disable...				
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsACMReq, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub

    Private Function GetPayType(ByRef pPayType As Object) As String
        Select Case UCase(pPayType)
            Case "B"
                GetPayType = "BILL"
            Case "N"
                GetPayType = "NEW BILL"
            Case "D"
                GetPayType = "D/N"
            Case "C"
                GetPayType = "C/N"
            Case "O"
                GetPayType = "ON ACCOUNT"
            Case "A"
                GetPayType = "ADVANCE"
            Case Else
                GetPayType = "ON ACCOUNT"
        End Select
    End Function

    Private Sub txtPaidDay_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDay.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDay_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDay.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPan.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPAN_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPan.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPan.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtphone_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPhone.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPhone_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPhone.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtpincode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPinCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtpincode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPinCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPinCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRange_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRange.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRange.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRegnNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegnNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSaleSTRecd_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSaleSTRecd.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtSaleSTRecd.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " '& vbCrLf |            & " AND IDENTIFICATION='ST'"				

        If MainClass.ValidateWithMasterTable(txtSaleSTRecd, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Form Type.", "", vbCritical)
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
    End Sub

    Private Sub txtSection_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSection.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mSectionCode As Integer
        Dim mTdsRate As Double

        If Trim(txtSection.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Secion Name Does Not Exist In Master", vbInformation)
            Cancel = True
            Exit Sub
        Else
            mSectionCode = MasterNo
        End If

        mTdsRate = CalcTDSRate(mSectionCode, VB.Left(cboCType.Text, 1), PubDBCn)

        txtTDSPer.Text = VB6.Format(IIf(Trim(txtTDSPer.Text) = "", mTdsRate, txtTDSPer.Text), "0.000")

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSeq_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSeq.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSeq_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSeq.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSeq.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub txtServProvided_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
    End Sub

    Private Sub txtServProvided_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServProvided.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSrvRegnNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSrvRegnNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSrvRegnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSrvRegnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSrvRegnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtstate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtState.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtState_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtState.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtState.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtState_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtState.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtState_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtState.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtState.Text) = "" Then GoTo EventExitSub

        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then
            If MainClass.ValidateWithMasterTable(txtState.Text, "NAME", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                'SSTInfo.Tab = 0				
                ErrorMsg("Invalid State Name", , vbInformation)
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSTDSPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSTDSPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTDSPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTDSPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTINNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTINNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTINNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTINNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVendorCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVendorCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVendorCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVendorCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVendorCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPayment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPayment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPayment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPayment.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtPayment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPayment.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtPayment.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtPayment.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblPaymentTerms.Text = MasterNo
        Else
            MsgBox("Invalid Payment Code.", vbInformation)
            Cancel = True
            Exit Sub
        End If

        GoTo EventExitSub
ERR1:

        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
