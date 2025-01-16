Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Friend Class FrmAcctCodeMerging
    Inherits System.Windows.Forms.Form

    'Private PvtDBCN As ADODB.Connection				

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler


        'If PubUserID = "G0416" Then
        '    If UpdateMainTemp1() = False Then
        '        MsgInformation("Record not saved")
        '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '        Exit Sub
        '    Else
        '        lblStatus.Visible = False
        '        cmdSave.Enabled = False
        '    End If
        '    Exit Sub
        'Else
        '    Exit Sub
        'End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        lblStatus.Visible = True

        If UpdateMain1() = False Then
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            lblStatus.Visible = False
            cmdSave.Enabled = False
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume				
    End Sub
    Private Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        FieldVarification = True
        Dim mCustomerHead As String = "N"

        If Trim(txtAcctCodeFrom.Text) = "" Then
            MsgInformation("Account Code is empty. Cannot Merge")
            txtAcctCodeFrom.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtAcctCodeFrom.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Account From Name. Cannot Merge")
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtAcctCodeFrom.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            mCustomerHead = "Y"
        End If

        If mCustomerHead = "Y" Then
            If Trim(txtLocationFrom.Text) = "" Then
                MsgInformation("Account Location is empty. Cannot Merge")
                'txtLocationFrom.Focus()
                FieldVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtAcctCodeFrom.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'") = False Then
                MsgInformation("Invalid Account Code. Cannot Merge")
                txtAcctCodeFrom.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If Trim(txtAcctCodeTo.Text) = "" Then
            MsgInformation("Account Code is empty. Cannot Merge")
            txtAcctCodeTo.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtAcctCodeTo.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Account To Name. Cannot Merge")
            FieldVarification = False
            Exit Function
        End If

        mCustomerHead = "N"
        If MainClass.ValidateWithMasterTable(txtAcctCodeTo.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            mCustomerHead = "Y"
        End If

        If mCustomerHead = "Y" Then
            If Trim(txtLocationTo.Text) = "" Then
                MsgInformation("Account Location is empty. Cannot Merge")
                'txtLocationTo.Focus()
                FieldVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtAcctCodeTo.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & MainClass.AllowSingleQuote(txtLocationTo.Text) & "'") = False Then
                MsgInformation("Invalid Account Code. Cannot Merge")
                txtAcctCodeTo.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub cmdSearchAcctFrom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAcctFrom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtAcctNameFrom.Text, "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", , SqlStr) = True Then
            txtAcctNameFrom.Text = AcName
            txtLocationFrom.Text = AcName2
            txtAcctNameFrom_Validating(txtAcctNameFrom, New System.ComponentModel.CancelEventArgs(False)) 'txtAcctNameFrom_Validate False
            If txtAcctCodeFrom.Enabled = True Then txtAcctCodeFrom.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchAcctTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAcctTo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtAcctNameTo.Text, "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", , SqlStr) = True Then
            txtAcctNameTo.Text = AcName
            txtLocationTo.Text = AcName2
            txtAcctNameTo_Validating(txtAcctNameTo, New System.ComponentModel.CancelEventArgs(False)) 'txtAcctNameTo_Validate False
            If txtAcctCodeTo.Enabled = True Then txtAcctCodeTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub FrmAcctCodeMerging_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Function UpdateMain1() As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mAccountCodeFrom As String
        Dim mAccountCodeTo As String
        Dim RsTemp As ADODB.Recordset
        Dim mConditionStr As String

        mAccountCodeFrom = Trim(txtAcctCodeFrom.Text)
        mAccountCodeTo = Trim(txtAcctCodeTo.Text)



        SqlStr = " SELECT COUNT(*) AS REC_CNT FROM FIN_SUPP_CUST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            If RsTemp.Fields("REC_CNT").Value > 0 Then
                If MsgQuestion("'Supplier/Customer Terms & Item Details' Exists for" & Chr(13) & "                       " & mAccountCodeFrom & "                       " & Chr(13) & "            Do You Want to Delete them ?") = CStr(MsgBoxResult.No) Then
                    MsgInformation("        Can Not Merge Account for which" & Chr(13) & "'Supplier/Customer Terms & Item Details' Exists.")
                    UpdateMain1 = False
                    Exit Function
                End If
            End If
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        'SqlStr = " SELECT COUNT(*) AS REC_CNT FROM GEN_MERGING_TRN " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '    & " AND MERGE_TYPE='A' " & vbCrLf _
        '    & " AND MERGE_FROM='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "' " & vbCrLf _
        '    & " AND MERGE_TO='" & MainClass.AllowSingleQuote(mAccountCodeTo) & "' "


        'SqlStr = SqlStr & vbCrLf & " AND MERGE_UNIT_DATE IS NULL "


        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        'If Not RsTemp.EOF Then
        '    If RsTemp.Fields("REC_CNT").Value > 0 Then
        '        SqlStr = " UPDATE GEN_MERGING_TRN SET " & vbCrLf _
        '            & " MERGE_UNIT_DATE='" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "', " & vbCrLf _
        '            & " MERGE_UNIT_USER='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf _
        '            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '            & " AND MERGE_TYPE='A' " & vbCrLf _
        '            & " AND MERGE_FROM='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "' " & vbCrLf _
        '            & " AND MERGE_TO='" & MainClass.AllowSingleQuote(mAccountCodeTo) & "' " & vbCrLf _
        '            & " AND MERGE_UNIT_DATE IS NULL "
        '    Else
        '        SqlStr = " INSERT INTO GEN_MERGING_TRN " & vbCrLf _
        '            & " (COMPANY_CODE, MERGE_TYPE, MERGE_FROM, MERGE_TO, " & vbCrLf _
        '            & " MERGE_HO_DATE, MERGE_HO_USER, MERGE_UNIT_DATE, MERGE_UNIT_USER) " & vbCrLf _
        '            & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", 'A', " & vbCrLf _
        '            & " '" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "', '" & MainClass.AllowSingleQuote(mAccountCodeTo) & "', " & vbCrLf _
        '            & " NULL, NULL, '" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "', '" & MainClass.AllowSingleQuote(PubUserID) & "') "
        '    End If
        '    PubDBCn.Execute(SqlStr)
        'End If

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            mConditionStr = ""
        Else
            mConditionStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If


        If UpdateAccountCode("DSP_57F4_HDR", "DSP_57F4_DET", "AUTO_KEY_57F4", "AUTO_KEY_57F4", "SUPP_CUST_CODE", True, False, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_CUST_ITEM_MST", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_DELV_SCHLD_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_DESPATCH_HDR", "DSP_DESPATCH_DET", "AUTO_KEY_DESP", "AUTO_KEY_DESP", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_DESPATCH_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("DSP_DI_HDR", "DSP_DI_DET", "AUTO_KEY_DESP", "AUTO_KEY_DESP", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_DI_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


        If UpdateAccountCode("FIN_EXPINV_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_EXPINV_HDR", "", "", "", "BUYER_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


        If UpdateAccountCode("DSP_PACKING_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_PACKING_HDR", "", "", "", "BUYER_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("INV_CUSTOMER_PACKING_MST", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_CUST_SOB_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_CUST_SOB_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart



        If UpdateAccountCode("DSP_OUT57F4_HDR", "DSP_OUT57F4_DET", "AUTO_KEY_OUT57F4", "AUTO_KEY_OUT57F4", "SUPP_CUST_CODE", True, False, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_OUTWARD57F4_TRN", "", "MKEY", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_OW_RECD57F4_HDR", "", "MKEY", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_PACKING_HDR", "", "AUTO_KEY_PACK", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_PAINT57F4_HDR", "", "MKEY", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_PAINT57F4_TRN", "", "MKEY", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("DSP_SALEORDER_HDR", "DSP_SALEORDER_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("DSP_SALEORDER_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart



        If UpdateAccountCode("INV_PO_RELATIONSHIP_HDR", "", "AUTO_KEY_PO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("DSP_TROLLY_IO_HDR", "", "AUTO_KEY_TRLY", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("FIN_DNCN_AMEND", "", "VMKEY", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_EXPINV_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_INVOICE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_INVOICE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE ", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("FIN_SUPP_SALE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("FIN_SUPP_SALE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE ", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PRD_SALERETURN_DET", "", "", "", "BOP_SUPP_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart



        If UpdateAccountCode("FIN_INVRECEIPT_HDR", "", "AUTO_KEY_REFNO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_PARTY_INTERFACE_MST", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_PURCHASE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_PURCHASE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_SUPP_PURCHASE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


        If UpdateAccountCode("FIN_ST_ADV_MST", "", "MKEY", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("FIN_INVOICE_HDR", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_PURCHASE_HDR", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_SUPP_PURCHASE_HDR", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("FIN_VOUCHER_HDR", "", "", "", "BOOKCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_VOUCHER_HDR", "FIN_VOUCHER_DET", "MKEY", "MKEY", "ACCOUNTCODE", False, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_VOUCHER_HDR", "FIN_BILLDETAILS_TRN", "MKEY", "MKEY", "ACCOUNTCODE", False, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_INVTYPE_MST", "", "", "", "ACCOUNTPOSTCODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_DNCN_HDR", "", "", "", "DEBITACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_DNCN_HDR", "", "", "", "CREDITACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_INTERFACE_MST", "", "", "", "SALEPOSTCODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_INTERFACE_MST", "", "", "", "PURCHASEPOSTCODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_POSTED_TRN", "", "", "", "BOOKCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("FIN_POSTED_TRN", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "LOCATION_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("INV_ADJ_RGP_HDR", "", "AUTO_KEY_NO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_DESCRP_HDR", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("INV_GATEENTRY_HDR", "INV_GATEENTRY_DET", "AUTO_KEY_GATE", "AUTO_KEY_GATE", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_GATEENTRY_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, False, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("INV_GATE_HDR", "INV_GATE_DET", "AUTO_KEY_MRR", "AUTO_KEY_MRR", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_GATE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, False, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart



        If UpdateAccountCode("INV_GATEPASS_HDR", "INV_GATEPASS_DET", "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_MISC_GATE_HDR", "", "AUTO_KEY_NO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_REOFFER_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_RGP_REG_TRN", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_RGP_SLIP_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_SAMPLE_INSP_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("INV_SRN_DET", "", "AUTO_KEY_SRN", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        'If UpdateAccountCode("PAY_SALARYHEAD_MST", "", "", "", "ACCOUNTCODEPOST", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("PRD_DEPTOPNGQTY_HDR", "PRD_DEPTOPNGQTY_DET", "OPNG_DATE", "OPNG_DATE", "SUPP_CUST_CODE", False, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PRD_ECN_MST", "", "ECN_NO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PRD_PLNF03_PLAN_HDR", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PRD_PROCESSWIP_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_PRODPLAN_HDR", "PRD_PRODPLAN_DET", "AUTO_KEY_PRODPLAN", "AUTO_KEY_PRODPLAN", "SUPP_CUST_CODE", False, True, True, mConditionStr, "") = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_PRODPLAN_MONTH_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "") = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_SENDBACKFORRWK_DET", "", "AUTO_KEY_SBRWK", "", "SUPP_CUST_CODE", True, False, False, "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = False Then GoTo ErrPart				
        If UpdateAccountCode("PRD_STOCK_TRN", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("PRD_TOOLPROG_HDR", "", "", "", "VENDORCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PRD_BOM_HDR_DUP", "", "", "", "VENDORCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PRD_BOM_HDR_DUP", "", "", "", "CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PRD_BOM_HDR", "", "", "", "VENDORCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PRD_BOM_HDR", "", "", "", "CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("PUR_RM_DWG_RATE_HDR", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


        If UpdateAccountCode("PUR_DELV_SCHLD_HDR", "PUR_DAILY_SCHLD_DET", "AUTO_KEY_DELV", "AUTO_KEY_DELV", "SUPP_CUST_CODE", False, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PUR_DELV_SCHLD_HDR", "", "AUTO_KEY_DELV", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PUR_FORMST38_HDR", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PUR_PURCHASE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PUR_PURCHASE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


        If UpdateAccountCode("PUR_QUOTATION_HDR", "", "AUTO_KEY_QUOT", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("PUR_SUPP_CUST_RES", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("QAL_CUST_COMPLAINT_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_DEVIATION_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_FINAL_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_FLASH_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_INS_INITSAMPART_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_IPT_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_LAYOUT_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_LAYOUT_PLAN_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_MASTER_SAMPLE_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("QAL_OTH_INST_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = False Then GoTo ErrPart				
        If UpdateAccountCode("QAL_PART_TESTREPORT_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_PPKPLANSR_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_PRODAUDIT_PLAN_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_RECEIPT_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("QAL_REJPRODANALY_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If UpdateAccountCode("TDS_TRN", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("TDS_TRN", "", "", "", "PARTYCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("TDS_CHALLAN", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        If UpdateAccountCode("TCS_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


        'If UpdateAccountCode("PRD_RM_GRADE_RATE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_RM_GRADE_RATE_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_SUPP_PRESS_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_SUPP_OPR_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        'If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_COST_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_PART_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_PROCESS1_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        'If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_PROCESS2_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_OPERATION_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
        'If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_EXP_COST_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = " DELETE From FIN_SUPP_CUST_DET WHERE " & vbCrLf _
                 & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE From FIN_SUPP_CUST_HDR WHERE " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE From TDS_MASTER WHERE " & vbCrLf _
                & " ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE From FIN_SUPP_CUST_BUSINESS_MST WHERE " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'" ''& vbCrLf _
            ''& " AND LOCATION_ID='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE From FIN_SUPP_CUST_MST WHERE " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

            PubDBCn.Execute(SqlStr)

        Else
            SqlStr = " DELETE From FIN_SUPP_CUST_DET WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE From FIN_SUPP_CUST_HDR WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE From TDS_MASTER WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE From FIN_SUPP_CUST_BUSINESS_MST WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'" ''& vbCrLf _
            ''& " AND LOCATION_ID='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'"

            PubDBCn.Execute(SqlStr)

            SqlStr = " DELETE From FIN_SUPP_CUST_MST WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

            PubDBCn.Execute(SqlStr)

        End If



        PubDBCn.CommitTrans()
        UpdateMain1 = True

        Exit Function
ErrPart:
        'Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateMain1 = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function UpdateMainTemp1() As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mAccountCodeFrom As String
        Dim mAccountCodeTo As String
        Dim RsTemp As ADODB.Recordset
        Dim RsTempData As ADODB.Recordset
        Dim mConditionStr As String


        SqlStr = " ALTER TABLE PRD_RM_GRADE_RATE_DET MODIFY  Constraint PRD_RM_GRADE_RATE_DET_FK DISABLE"
        PubDBCn.Execute(SqlStr)

        SqlStr = " ALTER TABLE DSP_CUST_SOB_DET MODIFY  Constraint DSP_CUST_SOB_DET_FK1 DISABLE"
        PubDBCn.Execute(SqlStr)


        ''

        'SqlStr = " SELECT * FROM TEMP_NEW_MST" & vbCrLf _
        '    & " WHERE NEW_CODE not in (SELECT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=TEMP_NEW_MST.COMPANY_CODE )"

        SqlStr = " SELECT * FROM TEMP_NEW_MST" & vbCrLf _
            & " WHERE NEW_CODE not in (SELECT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST )"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempData, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTempData.EOF Then
            Do While Not RsTempData.EOF
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                mAccountCodeFrom = RsTempData.Fields("SUPP_CUST_CODE").Value '
                mAccountCodeTo = RsTempData.Fields("NEW_CODE").Value

                SqlStr = " DELETE From TEMP_RR_SUPPLIER_MST"
                PubDBCn.Execute(SqlStr)


                SqlStr = " DELETE From TEMP_RR_SUPPLIER_BUSINESS_MST"
                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From TEMP_RR_SUPPLIER_DET"
                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From TEMP_RR_SUPPLIER_HDR"
                PubDBCn.Execute(SqlStr)

                SqlStr = " INSERT INTO TEMP_RR_SUPPLIER_MST SELECT * From FIN_SUPP_CUST_MST WHERE SUPP_CUST_CODE='" & mAccountCodeFrom & "'"
                PubDBCn.Execute(SqlStr)

                SqlStr = " INSERT INTO TEMP_RR_SUPPLIER_BUSINESS_MST SELECT * From FIN_SUPP_CUST_BUSINESS_MST WHERE SUPP_CUST_CODE='" & mAccountCodeFrom & "'"
                PubDBCn.Execute(SqlStr)

                SqlStr = " INSERT INTO TEMP_RR_SUPPLIER_HDR SELECT * From FIN_SUPP_CUST_HDR WHERE SUPP_CUST_CODE='" & mAccountCodeFrom & "'"
                PubDBCn.Execute(SqlStr)

                SqlStr = " INSERT INTO TEMP_RR_SUPPLIER_DET SELECT * From FIN_SUPP_CUST_DET WHERE SUPP_CUST_CODE='" & mAccountCodeFrom & "'"
                PubDBCn.Execute(SqlStr)

                SqlStr = "UPDATE FIN_SUPP_CUST_MST SET SUPP_CUST_NAME= SUPP_CUST_NAME || '-' || SUPP_CUST_CODE WHERE SUPP_CUST_CODE='" & mAccountCodeFrom & "'"
                PubDBCn.Execute(SqlStr)

                ''

                SqlStr = "UPDATE TEMP_RR_SUPPLIER_MST SET SUPP_CUST_CODE='" & mAccountCodeTo & "'"
                PubDBCn.Execute(SqlStr)

                SqlStr = "UPDATE TEMP_RR_SUPPLIER_BUSINESS_MST SET SUPP_CUST_CODE='" & mAccountCodeTo & "' "
                PubDBCn.Execute(SqlStr)

                SqlStr = "UPDATE TEMP_RR_SUPPLIER_HDR SET SUPP_CUST_CODE='" & mAccountCodeTo & "' "
                PubDBCn.Execute(SqlStr)

                SqlStr = "UPDATE TEMP_RR_SUPPLIER_DET SET SUPP_CUST_CODE='" & mAccountCodeTo & "' "
                PubDBCn.Execute(SqlStr)

                SqlStr = " INSERT INTO FIN_SUPP_CUST_MST SELECT * From TEMP_RR_SUPPLIER_MST "
                PubDBCn.Execute(SqlStr)

                SqlStr = " INSERT INTO FIN_SUPP_CUST_BUSINESS_MST SELECT * From TEMP_RR_SUPPLIER_BUSINESS_MST "
                PubDBCn.Execute(SqlStr)

                SqlStr = " INSERT INTO FIN_SUPP_CUST_HDR SELECT * From TEMP_RR_SUPPLIER_HDR "
                PubDBCn.Execute(SqlStr)

                SqlStr = " INSERT INTO FIN_SUPP_CUST_DET SELECT * From TEMP_RR_SUPPLIER_DET "
                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From TEMP_RR_SUPPLIER_BUSINESS_MST"
                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From TEMP_RR_SUPPLIER_MST"
                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From TEMP_RR_SUPPLIER_DET"
                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From TEMP_RR_SUPPLIER_HDR"
                PubDBCn.Execute(SqlStr)

                mConditionStr = "" ''"COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If UpdateAccountCode("DSP_57F4_HDR", "DSP_57F4_DET", "AUTO_KEY_57F4", "AUTO_KEY_57F4", "SUPP_CUST_CODE", True, False, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_CUST_ITEM_MST", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_DELV_SCHLD_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_DESPATCH_HDR", "DSP_DESPATCH_DET", "AUTO_KEY_DESP", "AUTO_KEY_DESP", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_DESPATCH_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("DSP_DI_HDR", "DSP_DI_DET", "AUTO_KEY_DESP", "AUTO_KEY_DESP", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_DI_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


                If UpdateAccountCode("FIN_EXPINV_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_EXPINV_HDR", "", "", "", "BUYER_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


                If UpdateAccountCode("DSP_PACKING_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_PACKING_HDR", "", "", "", "BUYER_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("INV_CUSTOMER_PACKING_MST", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_CUST_SOB_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_CUST_SOB_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart



                If UpdateAccountCode("DSP_OUT57F4_HDR", "DSP_OUT57F4_DET", "AUTO_KEY_OUT57F4", "AUTO_KEY_OUT57F4", "SUPP_CUST_CODE", True, False, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_OUTWARD57F4_TRN", "", "MKEY", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_OW_RECD57F4_HDR", "", "MKEY", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_PACKING_HDR", "", "AUTO_KEY_PACK", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_PAINT57F4_HDR", "", "MKEY", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_PAINT57F4_TRN", "", "MKEY", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("DSP_SALEORDER_HDR", "DSP_SALEORDER_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("DSP_SALEORDER_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart



                If UpdateAccountCode("INV_PO_RELATIONSHIP_HDR", "", "AUTO_KEY_PO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("DSP_TROLLY_IO_HDR", "", "AUTO_KEY_TRLY", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("FIN_DNCN_AMEND", "", "VMKEY", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_EXPINV_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_INVOICE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_INVOICE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE ", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("FIN_SUPP_SALE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                'If UpdateAccountCode("FIN_SUPP_SALE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE ", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_SALERETURN_DET", "", "", "", "BOP_SUPP_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart



                If UpdateAccountCode("FIN_INVRECEIPT_HDR", "", "AUTO_KEY_REFNO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_PARTY_INTERFACE_MST", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_PURCHASE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_PURCHASE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_SUPP_PURCHASE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


                If UpdateAccountCode("FIN_ST_ADV_MST", "", "MKEY", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("FIN_INVOICE_HDR", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_PURCHASE_HDR", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_SUPP_PURCHASE_HDR", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("FIN_VOUCHER_HDR", "", "", "", "BOOKCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_VOUCHER_HDR", "FIN_VOUCHER_DET", "MKEY", "MKEY", "ACCOUNTCODE", False, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_VOUCHER_HDR", "FIN_BILLDETAILS_TRN", "MKEY", "MKEY", "ACCOUNTCODE", False, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_INVTYPE_MST", "", "", "", "ACCOUNTPOSTCODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_DNCN_HDR", "", "", "", "DEBITACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_DNCN_HDR", "", "", "", "CREDITACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_INTERFACE_MST", "", "", "", "SALEPOSTCODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_INTERFACE_MST", "", "", "", "PURCHASEPOSTCODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_POSTED_TRN", "", "", "", "BOOKCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_POSTED_TRN", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "LOCATION_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("INV_ADJ_RGP_HDR", "", "AUTO_KEY_NO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_DESCRP_HDR", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("INV_GATEENTRY_HDR", "INV_GATEENTRY_DET", "AUTO_KEY_GATE", "AUTO_KEY_GATE", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_GATEENTRY_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, False, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("INV_GATE_HDR", "INV_GATE_DET", "AUTO_KEY_MRR", "AUTO_KEY_MRR", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_GATE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, False, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart



                If UpdateAccountCode("INV_GATEPASS_HDR", "INV_GATEPASS_DET", "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "SUPP_CUST_CODE", False, True, True, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_MISC_GATE_HDR", "", "AUTO_KEY_NO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_REOFFER_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_RGP_REG_TRN", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_RGP_SLIP_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_SAMPLE_INSP_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("INV_SRN_DET", "", "AUTO_KEY_SRN", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                'If UpdateAccountCode("PAY_SALARYHEAD_MST", "", "", "", "ACCOUNTCODEPOST", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("PRD_DEPTOPNGQTY_HDR", "PRD_DEPTOPNGQTY_DET", "OPNG_DATE", "OPNG_DATE", "SUPP_CUST_CODE", False, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_ECN_MST", "", "ECN_NO", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_PLNF03_PLAN_HDR", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_PROCESSWIP_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                'If UpdateAccountCode("PRD_PRODPLAN_HDR", "PRD_PRODPLAN_DET", "AUTO_KEY_PRODPLAN", "AUTO_KEY_PRODPLAN", "SUPP_CUST_CODE", False, True, True, mConditionStr, "") = False Then GoTo ErrPart
                'If UpdateAccountCode("PRD_PRODPLAN_MONTH_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "") = False Then GoTo ErrPart
                'If UpdateAccountCode("PRD_SENDBACKFORRWK_DET", "", "AUTO_KEY_SBRWK", "", "SUPP_CUST_CODE", True, False, False, "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = False Then GoTo ErrPart				
                If UpdateAccountCode("PRD_STOCK_TRN", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("PRD_TOOLPROG_HDR", "", "", "", "VENDORCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_BOM_HDR_DUP", "", "", "", "VENDORCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_BOM_HDR_DUP", "", "", "", "CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_BOM_HDR", "", "", "", "VENDORCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_BOM_HDR", "", "", "", "CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("PUR_DELV_SCHLD_HDR", "PUR_DAILY_SCHLD_DET", "AUTO_KEY_DELV", "AUTO_KEY_DELV", "SUPP_CUST_CODE", False, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PUR_DELV_SCHLD_HDR", "", "AUTO_KEY_DELV", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PUR_FORMST38_HDR", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PUR_PURCHASE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "BILL_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PUR_PURCHASE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "SHIP_TO_LOC_ID", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


                If UpdateAccountCode("PUR_QUOTATION_HDR", "", "AUTO_KEY_QUOT", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PUR_SUPP_CUST_RES", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("QAL_CUST_COMPLAINT_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_DEVIATION_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_FINAL_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_FLASH_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_INS_INITSAMPART_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_IPT_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_LAYOUT_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_LAYOUT_PLAN_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_MASTER_SAMPLE_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                'If UpdateAccountCode("QAL_OTH_INST_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = False Then GoTo ErrPart				
                If UpdateAccountCode("QAL_PART_TESTREPORT_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_PPKPLANSR_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_PRODAUDIT_PLAN_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_RECEIPT_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("QAL_REJPRODANALY_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("TDS_TRN", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("TDS_TRN", "", "", "", "PARTYCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("TDS_CHALLAN", "", "", "", "ACCOUNTCODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("TCS_TRN", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PPC_MODELWISE_MON_SCHD_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_PRO_INVOICE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("FIN_PRO_INVOICE_HDR", "", "", "", "SHIPPED_TO_PARTY_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart


                If UpdateAccountCode("PRD_RM_GRADE_RATE_HDR", "", "", "", "SUPP_CUST_CODE", True, True, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_RM_GRADE_RATE_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_SUPP_PRESS_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_SUPP_OPR_DET", "", "", "", "SUPP_CUST_CODE", True, False, False, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_COST_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_PART_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_PROCESS1_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_PROCESS2_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_OPERATION_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart
                If UpdateAccountCode("PRD_BOP_COST_HDR", "PRD_BOP_EXP_COST_DET", "MKEY", "MKEY", "SUPP_CUST_CODE", False, True, True, mConditionStr, "", mAccountCodeFrom, mAccountCodeTo) = False Then GoTo ErrPart

                SqlStr = " DELETE From FIN_SUPP_CUST_DET WHERE " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

                If mConditionStr <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND " & mConditionStr
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From FIN_SUPP_CUST_HDR WHERE " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

                If mConditionStr <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND " & mConditionStr
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From TDS_MASTER WHERE " & vbCrLf _
                    & "  ACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

                If mConditionStr <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND " & mConditionStr
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From FIN_SUPP_CUST_BUSINESS_MST WHERE " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'" '& vbCrLf _
                '& " AND LOCATION_ID='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'"

                If mConditionStr <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND " & mConditionStr
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From FIN_SUPP_CUST_MST WHERE " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCodeFrom) & "'"

                If mConditionStr <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND " & mConditionStr
                End If

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()

                RsTempData.MoveNext()
            Loop
        End If

        SqlStr = " ALTER TABLE PRD_RM_GRADE_RATE_DET MODIFY  Constraint PRD_RM_GRADE_RATE_DET_FK ENABLE"
        PubDBCn.Execute(SqlStr)

        SqlStr = " ALTER TABLE DSP_CUST_SOB_DET MODIFY  Constraint DSP_CUST_SOB_DET_FK1 ENABLE"
        PubDBCn.Execute(SqlStr)


        UpdateMainTemp1 = True

        Exit Function
ErrPart:
        'Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateMainTemp1 = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function UpdateAccountCode(ByRef mHDRTable As String, ByRef mDETTABLE As String, ByRef mHDRRefKey As String,
                                       ByRef mDETRefKey As String, ByRef mUpdateKey As String, ByRef mMasterTable As Boolean,
                                       ByRef mModField As Boolean, ByRef mACCOUNTCODEIN_MST As Boolean,
                                       ByRef mCondQry As String, ByRef mIsLocationID As String, ByRef pAccountCodeFrom As String, ByRef pAccountCodeTo As String) As Boolean

        Dim MainClass_Renamed As Object

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mTableStr As String

        mTableStr = mHDRTable
        mTableStr = IIf(mTableStr = "", "", mTableStr & " AND ") & mDETTABLE

        lblStatus.Text = "Merging in " & mTableStr & "... "
        System.Windows.Forms.Application.DoEvents()

        If mMasterTable = True Then
            SqlStr = " UPDATE " & mHDRTable & " SET " & vbCrLf _
                & " " & mUpdateKey & "='" & pAccountCodeTo & "'"

            If mIsLocationID <> "" And txtLocationTo.Text <> "" Then
                SqlStr = SqlStr & vbCrLf & " , " & mIsLocationID & "='" & MainClass.AllowSingleQuote(txtLocationTo.Text) & "'"
            End If

            If mModField = True Then
                SqlStr = SqlStr & vbCrLf _
                    & " , MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            SqlStr = SqlStr & vbCrLf & " WHERE " & mUpdateKey & "='" & pAccountCodeFrom & "'"

            If mIsLocationID <> "" And txtLocationTo.Text <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND  " & mIsLocationID & "='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'"
            End If

            If Trim(mCondQry) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND " & mCondQry
            End If

            PubDBCn.Execute(SqlStr)

            '        If mDETTABLE <> "" Then				
            '            SqlStr = " UPDATE " & mDETTABLE & " SET " & vbCrLf _				
            ''                    & " " & mUpdateKey & "='" & pAccountCodeTo & "'" & vbCrLf _				
            ''                    & " WHERE " & mUpdateKey & "='" & pAccountCodeFrom & "'"				
            '				
            '            PubDBCN.Execute SqlStr				
            '        End If				
        Else

            If mACCOUNTCODEIN_MST = True Then
                SqlStr = " UPDATE " & mHDRTable & " SET " & vbCrLf _
                    & " " & mUpdateKey & "='" & pAccountCodeTo & "', "

                If mIsLocationID <> "" And txtLocationTo.Text <> "" Then
                    SqlStr = SqlStr & vbCrLf & " " & mIsLocationID & "='" & MainClass.AllowSingleQuote(txtLocationTo.Text) & "',"
                End If

                SqlStr = SqlStr & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE " & mUpdateKey & "='" & pAccountCodeFrom & "'"


                If mIsLocationID <> "" And txtLocationTo.Text <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND " & mIsLocationID & "='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'"
                End If

                If Trim(mCondQry) <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND " & mCondQry
                End If
            Else
                SqlStr = " UPDATE " & mHDRTable & " SET "

                SqlStr = SqlStr & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE " & mHDRRefKey & " IN " & vbCrLf _
                    & " ( SELECT IH." & mHDRRefKey & "" & vbCrLf _
                    & " FROM " & mHDRTable & " IH, " & mDETTABLE & " ID " & vbCrLf _
                    & " WHERE IH." & mHDRRefKey & "=ID." & mDETRefKey & " " & vbCrLf _
                    & " AND ID." & mUpdateKey & "='" & pAccountCodeFrom & "'"

                If Trim(mCondQry) <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                End If

                If mIsLocationID <> "" And txtLocationTo.Text <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND " & mIsLocationID & "='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & ") "
            End If

            PubDBCn.Execute(SqlStr)

            SqlStr = " UPDATE " & mDETTABLE & " SET " & vbCrLf _
                & " " & mUpdateKey & "='" & pAccountCodeTo & "'"

            If mDETTABLE = "DSP_DESPATCH_DET" Or mDETTABLE = "DSP_DI_DET" Or mDETTABLE = "DSP_SALEORDER_DET" Or mDETTABLE = "INV_GATE_DET" Or mDETTABLE = "INV_GATEENTRY_DET" Or mDETTABLE = "INV_GATEPASS_DET" Then

            Else
                If mIsLocationID <> "" And txtLocationTo.Text <> "" Then
                    SqlStr = SqlStr & vbCrLf & " , " & mIsLocationID & "='" & MainClass.AllowSingleQuote(txtLocationTo.Text) & "'"
                End If
            End If


            SqlStr = SqlStr & vbCrLf _
                & " WHERE " & mUpdateKey & "='" & pAccountCodeFrom & "'" & vbCrLf _
                & " AND " & mDETRefKey & " IN " & vbCrLf & " ( SELECT DISTINCT IH." & mHDRRefKey & "" & vbCrLf _
                & " FROM " & mHDRTable & " IH, " & mDETTABLE & " ID " & vbCrLf _
                & " WHERE IH." & mHDRRefKey & "=ID." & mDETRefKey & " " & vbCrLf _
                & " AND ID." & mUpdateKey & "='" & pAccountCodeFrom & "'"

            If Trim(mCondQry) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            End If

            If mDETTABLE = "DSP_DESPATCH_DET" Or mDETTABLE = "DSP_DI_DET" Or mDETTABLE = "DSP_SALEORDER_DET" Or mDETTABLE = "INV_GATE_DET" Or mDETTABLE = "INV_GATEENTRY_DET" Or mDETTABLE = "INV_GATEPASS_DET" Then

            Else
                If mIsLocationID <> "" And txtLocationTo.Text <> "" Then
                    SqlStr = SqlStr & vbCrLf & " And " & mIsLocationID & "='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'"
                End If
            End If

            SqlStr = SqlStr & vbCrLf & ") "

            PubDBCn.Execute(SqlStr)

        End If

        UpdateAccountCode = True

        Exit Function
ErrPart:
        'Resume				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateAccountCode = False
    End Function

    Public Sub FrmAcctCodeMerging_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume				
    End Sub
    Public Sub FrmAcctCodeMerging_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCN = New ADODB.Connection				
        'PvtDBCN.Open StrConn				

        '    XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)				
        '    MainClass.RightsToButton Me, XRIGHT				


        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(3405) '''8000				
        Me.Width = VB6.TwipsToPixelsX(8040) '''11900				

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub



    Private Sub txtAcctCodeFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcctCodeFrom.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtAcctCodeFrom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcctCodeFrom.DoubleClick
        SearchCodeFrom()
    End Sub

    Private Sub txtAcctCodeFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAcctCodeFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAcctCodeFrom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAcctCodeFrom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAcctCodeFrom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCodeFrom()
    End Sub


    Private Sub txtAcctCodeFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAcctCodeFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsAccountMast As ADODB.Recordset

        If Trim(txtAcctCodeFrom.Text) = "" Then GoTo EventExitSub


        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "Select SUPP_CUST_NAME From FIN_SUPP_CUST_MST " & vbCrLf _
                & " WHERE LTRIM(RTRIM(SUPP_CUST_CODE))='" & MainClass.AllowSingleQuote(txtAcctCodeFrom.Text) & "'"
        Else
            SqlStr = "Select SUPP_CUST_NAME From FIN_SUPP_CUST_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND LTRIM(RTRIM(SUPP_CUST_CODE))='" & MainClass.AllowSingleQuote(txtAcctCodeFrom.Text) & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAccountMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAccountMast.EOF = False Then
            txtAcctNameFrom.Text = IIf(IsDBNull(RsAccountMast.Fields("SUPP_CUST_NAME").Value), "", RsAccountMast.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgBox("Invalid Account Code.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAcctCodeTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcctCodeTo.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtAcctCodeTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcctCodeTo.DoubleClick
        SearchCodeTo()
    End Sub

    Private Sub txtAcctCodeTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAcctCodeTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAcctCodeTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAcctCodeTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAcctCodeTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCodeTo()
    End Sub


    Private Sub txtAcctCodeTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAcctCodeTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsAccountMast As ADODB.Recordset

        If Trim(txtAcctCodeTo.Text) = "" Then GoTo EventExitSub

        SqlStr = "Select SUPP_CUST_NAME From FIN_SUPP_CUST_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(SUPP_CUST_CODE))='" & MainClass.AllowSingleQuote(txtAcctCodeTo.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAccountMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAccountMast.EOF = False Then
            txtAcctNameTo.Text = IIf(IsDBNull(RsAccountMast.Fields("SUPP_CUST_NAME").Value), "", RsAccountMast.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgBox("Invalid Account Code.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtAcctNameFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcctNameFrom.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtAcctNameFrom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcctNameFrom.DoubleClick
        cmdSearchAcctFrom_Click(cmdSearchAcctFrom, New System.EventArgs())
    End Sub

    Private Sub SearchCodeFrom()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtAcctCodeFrom.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
            txtAcctCodeFrom.Text = AcName
            ''txtAcctCodeFrom_Validate False
            txtAcctCodeFrom_Validating(txtAcctCodeFrom, New System.ComponentModel.CancelEventArgs(False))
            If txtAcctCodeFrom.Enabled = True Then txtAcctCodeFrom.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchCodeTo()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtAcctCodeTo.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
            txtAcctCodeTo.Text = AcName
            txtAcctCodeTo_Validating(txtAcctCodeTo, New System.ComponentModel.CancelEventArgs(False)) 'txtAcctCodeTo_Validate False
            If txtAcctCodeTo.Enabled = True Then txtAcctCodeTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtAcctNameFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAcctNameFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAcctNameFrom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAcctNameFrom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAcctNameFrom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchAcctFrom_Click(cmdSearchAcctFrom, New System.EventArgs())
    End Sub


    Private Sub txtAcctNameFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAcctNameFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsAccountMast As ADODB.Recordset

        If Trim(txtAcctNameFrom.Text) = "" Then GoTo EventExitSub
        If Trim(txtLocationFrom.Text) = "" Then GoTo EventExitSub


        SqlStr = "Select SUPP_CUST_CODE From FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(SUPP_CUST_NAME))='" & MainClass.AllowSingleQuote(txtAcctNameFrom.Text) & "'" & vbCrLf _
            & " AND LTRIM(RTRIM(LOCATION_ID))='" & MainClass.AllowSingleQuote(txtLocationFrom.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAccountMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAccountMast.EOF = False Then
            txtAcctCodeFrom.Text = IIf(IsDBNull(RsAccountMast.Fields("SUPP_CUST_CODE").Value), "", RsAccountMast.Fields("SUPP_CUST_CODE").Value)
        Else
            MsgBox("Invalid Account Name.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtAcctNameTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcctNameTo.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtAcctNameTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcctNameTo.DoubleClick
        cmdSearchAcctTo_Click(cmdSearchAcctTo, New System.EventArgs())
    End Sub

    Private Sub txtAcctNameTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAcctNameTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim MainClass_Renamed As Object
        KeyAscii = MainClass.UpperCase(KeyAscii, txtAcctNameTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAcctNameTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAcctNameTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchAcctTo_Click(cmdSearchAcctTo, New System.EventArgs())
    End Sub


    Private Sub txtAcctNameTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAcctNameTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim MainClass_Renamed As Object
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsAccountMast As ADODB.Recordset

        If Trim(txtAcctNameTo.Text) = "" Then GoTo EventExitSub
        If Trim(txtLocationTo.Text) = "" Then GoTo EventExitSub

        ''

        SqlStr = "Select SUPP_CUST_CODE From FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(SUPP_CUST_NAME))='" & MainClass.AllowSingleQuote(txtAcctNameTo.Text) & "'" & vbCrLf _
            & " AND LTRIM(RTRIM(LOCATION_ID))='" & MainClass.AllowSingleQuote(txtLocationTo.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAccountMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAccountMast.EOF = False Then
            txtAcctCodeTo.Text = IIf(IsDBNull(RsAccountMast.Fields("SUPP_CUST_CODE").Value), "", RsAccountMast.Fields("SUPP_CUST_CODE").Value)
        Else
            MsgBox("Invalid Account Name.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class