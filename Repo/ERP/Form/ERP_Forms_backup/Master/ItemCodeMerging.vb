Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmItemCodeMerging
    Inherits System.Windows.Forms.Form

    'Private PvtDBCN As ADODB.Connection						

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

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


        If Trim(txtItemCodeFrom.Text) = "" Then
            MsgInformation("Item Code is empty. Cannot Merge")
            txtItemCodeFrom.Focus()
            FieldVarification = False
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable(txtItemCodeFrom.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Item Code. Cannot Merge")
            txtItemCodeFrom.Focus()
            FieldVarification = False
            Exit Function
        End If


        If Trim(txtItemCodeTo.Text) = "" Then
            MsgInformation("Item Code is empty. Cannot Merge")
            txtItemCodeTo.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtItemCodeTo.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Item Code. Cannot Merge")
            txtItemCodeTo.Focus()
            FieldVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub cmdSearchItemFrom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItemFrom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtItemNameFrom.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemNameFrom.Text = AcName
            ''   txtItemNameFrom_Validate False
            txtItemNameFrom_Validating(txtItemNameFrom, New System.ComponentModel.CancelEventArgs(False))
            If txtItemCodeFrom.Enabled = True Then txtItemCodeFrom.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchItemTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItemTo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtItemNameTo.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemNameTo.Text = AcName
            ''txtItemNameTo_Validate False
            txtItemNameTo_Validating(txtItemNameTo, New System.ComponentModel.CancelEventArgs(False))
            If txtItemCodeTo.Enabled = True Then txtItemCodeTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub FrmItemCodeMerging_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mItemCodeFrom As String
        Dim mItemCodeTo As String
        Dim mAcctCode As String
        Dim mSqlStr As String
        Dim mInvStockTable As String
        Dim I As Integer

        Dim xSqlStr As String
        Dim RsTemp1 As ADODB.Recordset

        mItemCodeFrom = Trim(txtItemCodeFrom.Text)
        mItemCodeTo = Trim(txtItemCodeTo.Text)

        SqlStr = " SELECT COMPANY_CODE,SUPP_CUST_CODE,COUNT(1) AS REC_CNT FROM FIN_SUPP_CUST_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE IN ('" & MainClass.AllowSingleQuote(mItemCodeFrom) & "','" & MainClass.AllowSingleQuote(mItemCodeTo) & "')" & vbCrLf _
            & " HAVING COUNT(1)>1 " & vbCrLf _
            & " GROUP BY COMPANY_CODE,SUPP_CUST_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            Do While Not RsTemp.EOF
                mAcctCode = IIf(mAcctCode = "", "", mAcctCode & vbNewLine) & RsTemp.Fields("SUPP_CUST_CODE").Value
                RsTemp.MoveNext()
            Loop
            MsgInformation("Both Item found in Supplier/Customer Terms & Item Details" & vbNewLine & "For Account Code : " & mAcctCode)
            UpdateMain1 = False
            Exit Function
        End If


        SqlStr = " SELECT COMPANY_CODE,PRODUCT_CODE,WEF,COUNT(1) AS REC_CNT FROM PRD_BOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE IN ('" & MainClass.AllowSingleQuote(mItemCodeFrom) & "','" & MainClass.AllowSingleQuote(mItemCodeTo) & "')" & vbCrLf & " HAVING COUNT(1)>1 " & vbCrLf & " GROUP BY COMPANY_CODE,PRODUCT_CODE,WEF"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            MsgInformation("Both Item found in B.O.M. ")
            UpdateMain1 = False
            Exit Function
        End If

        SqlStr = " SELECT COMPANY_CODE,PRODUCT_CODE,WEF,COUNT(1) AS REC_CNT FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE IN ('" & MainClass.AllowSingleQuote(mItemCodeFrom) & "','" & MainClass.AllowSingleQuote(mItemCodeTo) & "')" & vbCrLf & " HAVING COUNT(1)>1 " & vbCrLf & " GROUP BY COMPANY_CODE,PRODUCT_CODE,WEF"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            MsgInformation("Both Item found in B.O.M. ")
            UpdateMain1 = False
            Exit Function
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " SELECT COUNT(*) AS REC_CNT FROM GEN_MERGING_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MERGE_TYPE='I' " & vbCrLf & " AND MERGE_FROM='" & MainClass.AllowSingleQuote(mItemCodeFrom) & "' " & vbCrLf & " AND MERGE_TO='" & MainClass.AllowSingleQuote(mItemCodeTo) & "' "

        'If PubHO = "Y" Then
        SqlStr = SqlStr & vbCrLf & " AND MERGE_HO_DATE IS NULL "
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND MERGE_UNIT_DATE IS NULL "
        'End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            If RsTemp.Fields("REC_CNT").Value > 0 Then

                SqlStr = " UPDATE GEN_MERGING_TRN SET " & vbCrLf & " MERGE_HO_DATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MERGE_HO_USER='" & MainClass.AllowSingleQuote(PubUserID) & "' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MERGE_TYPE='I' " & vbCrLf & " AND MERGE_FROM='" & MainClass.AllowSingleQuote(mItemCodeFrom) & "' " & vbCrLf & " AND MERGE_TO='" & MainClass.AllowSingleQuote(mItemCodeTo) & "' " & vbCrLf & " AND MERGE_HO_DATE IS NULL "

            Else

                SqlStr = " INSERT INTO GEN_MERGING_TRN " & vbCrLf & " (COMPANY_CODE, MERGE_TYPE, MERGE_FROM, MERGE_TO, " & vbCrLf & " MERGE_HO_DATE, MERGE_HO_USER, MERGE_UNIT_DATE, MERGE_UNIT_USER) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", 'I', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCodeFrom) & "', '" & MainClass.AllowSingleQuote(mItemCodeTo) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(PubUserID) & "', NULL, NULL )"

            End If
            PubDBCn.Execute(SqlStr)
        End If

        If UpdateItemCode("", "DSP_57F4_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "DSP_57F4_DET", "", "", "REF_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_CONSUMPTION_HDR", "DSP_CONSUMPTION_DET", "ITEM_CODE", "CON_ITEM_CODE", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "DSP_CONSUMPTION_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "DSP_CONSUMPTION_DET", "", "", "CON_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "DSP_CUST_ITEM_MST", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_DELV_SCHLD_HDR", "DSP_DELV_SCHLD_DET", "AUTO_KEY_DELV", "AUTO_KEY_DELV", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_DESPATCH_HDR", "DSP_DESPATCH_DET", "AUTO_KEY_DESP", "AUTO_KEY_DESP", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "DSP_OUT57F4_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "DSP_OUT57F4_DET", "", "", "REF_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "DSP_OUTWARD57F4_TRN", "", "", "OUTWARD_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "DSP_OUTWARD57F4_TRN", "", "", "INWARD_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_OW_RECD57F4_HDR", "DSP_OW_RECD57F4_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_OW_RECD57F4_HDR", "DSP_OW_RECD57F4_DET", "MKEY", "MKEY", "REF_ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_PACKING_HDR", "DSP_PACKING_DET", "AUTO_KEY_PACK", "AUTO_KEY_PACK", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_PAINT57F4_HDR", "DSP_PAINT57F4_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_PAINT57F4_HDR", "DSP_PAINT57F4_TRN", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, False, False, True) = False Then GoTo ErrPart
        If UpdateItemCode("DSP_PAINT57F4_HDR", "DSP_PAINT57F4_TRN", "MKEY", "MKEY", "SUB_ITEM_CODE", mItemCodeFrom, mItemCodeTo, False, False, True) = False Then GoTo ErrPart
        '    If UpdateItemCode("DSP_SALEORDER_HDR", "DSP_SALEORDER_DET", "AUTO_KEY_SO", "AUTO_KEY_SO", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart						
        If UpdateItemCode("DSP_SALEORDER_HDR", "DSP_SALEORDER_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("", "FIN_DNCN_AMEND", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("FIN_DNCN_HDR", "FIN_DNCN_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("FIN_EXPINV_HDR", "FIN_EXPINV_DET", "AUTO_KEY_EXPINV", "AUTO_KEY_EXPINV", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "FIN_BARCODE_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("FIN_INVOICE_HDR", "FIN_INVOICE_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("FIN_PURCHASE_HDR", "FIN_PURCHASE_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("FIN_SUPP_PURCHASE_HDR", "FIN_SUPP_PURCHASE_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("FIN_INVOICE_HDR", "FIN_RGDAILYMANU_HDR", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "FIN_RGOP_MST", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("FIN_SUPP_CUST_HDR", "FIN_SUPP_CUST_DET", "SUPP_CUST_CODE", "SUPP_CUST_CODE", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("INV_ADJ_RGP_HDR", "INV_ADJ_RGP_DET", "AUTO_KEY_NO", "AUTO_KEY_NO", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("INV_ADJ_RGP_HDR", "INV_ADJ_RGP_DET", "AUTO_KEY_NO", "AUTO_KEY_NO", "RGP_ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("", "INV_PO_RELATIONSHIP_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart

        If UpdateItemCode("", "INV_ITEM_RELATIONSHIP_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_ITEM_RELATIONSHIP_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_ITEM_RELATIONSHIP_DET", "", "", "REF_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart

        If UpdateItemCode("", "INV_BILLOFMATERIAL_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_CONSUMPTION_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_FEEDBACK_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("INV_GATEENTRY_HDR", "INV_GATEENTRY_DET", "AUTO_KEY_GATE", "AUTO_KEY_GATE", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("INV_GATEENTRY_HDR", "INV_GATEENTRY_DET", "AUTO_KEY_GATE", "AUTO_KEY_GATE", "RGP_ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("INV_GATE_HDR", "INV_GATE_DET", "AUTO_KEY_MRR", "AUTO_KEY_MRR", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("INV_GATE_HDR", "INV_GATE_DET", "AUTO_KEY_MRR", "AUTO_KEY_MRR", "RGP_ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("INV_GATEPASS_HDR", "INV_GATEPASS_DET", "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_GATEPASS_HDR", "", "", "INWARD_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("INV_ISSUE_HDR", "INV_ISSUE_DET", "AUTO_KEY_ISS", "AUTO_KEY_ISS", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_ITEM_MST", "", "", "SEMI_FIN_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_ITEM_MST", "", "", "PACK_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_ITEM_MST", "", "", "SCRAP_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_OPENING_BAL", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_PAINT_STOCK_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("INV_REOFFER_HDR", "INV_REOFFER_DET", "AUTO_KEY_REF", "AUTO_KEY_REF", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_RGP_REG_TRN", "", "", "OUTWARD_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_RGP_REG_TRN", "", "", "INWARD_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("INV_RGP_SLIP_HDR", "INV_RGP_SLIP_DET", "AUTO_KEY_RGPSLIP", "AUTO_KEY_RGPSLIP", "FROM_ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_RGP_SLIP_HDR", "", "", "INWARD_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_SAMPLE_INSP_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("INV_SCRAP_CONV_HDR", "INV_SCRAP_CONV_DET", "AUTO_KEY_SCRAP", "AUTO_KEY_SCRAP", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "INV_SCRAP_CONV_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("INV_SRN_HDR", "INV_SRN_DET", "AUTO_KEY_SRN", "AUTO_KEY_SRN", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("DSP_LOADING_HDR", "DSP_LOADING_DET", "AUTO_KEY_LOAD", "AUTO_KEY_LOAD", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("FIN_CT1_HDR", "FIN_CT1_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("", "PRD_FGBREAKUP_HDR", "", "", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_FGBREAKUP_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart


        If UpdateItemCode("", "INV_RGP_APP_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart



        If UpdateItemCode("", "INV_STOCK_REC_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "FIN_PURCHASE_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart


        If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
            xSqlStr = "SELECT TNAME FROM TAB WHERE TNAME LIKE 'INV_STOCK_REC_TRN%'"
            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp1.EOF = False Then
                Do While Not RsTemp1.EOF
                    mInvStockTable = RsTemp1.Fields("TNAME").Value
                    If UpdateItemCode("", mInvStockTable, "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True) = False Then GoTo ErrPart
                    RsTemp1.MoveNext()
                Loop
            End If
        End If

        If UpdateItemCode("INV_SUB_ISSUE_HDR", "INV_SUB_ISSUE_DET", "AUTO_KEY_ISS", "AUTO_KEY_ISS", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("MAN_BREAKDOWN_HDR", "MAN_BREAKDOWN_DET", "AUTO_KEY_BDSLIP", "AUTO_KEY_BDSLIP", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        '    If UpdateItemCode("", "MAN_CO2COSUMP_TRN", "", "", "ITEM_CODE_LIQ", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart						
        '    If UpdateItemCode("", "MAN_CO2COSUMP_TRN", "", "", "ITEM_CODE_CYL", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart						
        If UpdateItemCode("", "MAN_MACHINE_MST", "", "", "MACHINE_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart

        If UpdateItemCode("PRD_COST_MST", "PRD_COST_CONS_DET", "DEPT_CODE", "DEPT_CODE", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PRD_COST_MST", "PRD_COST_MAIN_DET", "DEPT_CODE", "DEPT_CODE", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PRD_COST_MST", "PRD_COST_MAINCONS_DET", "DEPT_CODE", "DEPT_CODE", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PRD_ISSREC_HDR", "PRD_ISSREC_DET", "AUTO_KEY_ISSREC", "AUTO_KEY_ISSREC", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_ITEMDEVP_MST", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_ITEMDEVP_MST", "", "", "SEMI_FIN_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_ITEMDEVP_MST", "", "", "PACK_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_ITEMDEVP_MST", "", "", "SCRAP_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        '    If UpdateItemCode("PRD_PMEMO_HDR", "PRD_PMEMO_DET", "AUTO_KEY_PMO", "AUTO_KEY_PMO", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart						

        If UpdateItemCode("PRD_PMEMODEPT_HDR", "PRD_PMEMODEPT_DET", "AUTO_KEY_REF", "AUTO_KEY_REF", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PRD_ISSREC_HDR", "PRD_ISSREC_DET", "AUTO_KEY_ISSREC", "AUTO_KEY_ISSREC", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PRD_SENDBACKFORRWK_HDR", "PRD_SENDBACKFORRWK_DET", "AUTO_KEY_SBRWK", "AUTO_KEY_SBRWK", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("INV_ADJ_HDR", "INV_ADJ_DET", "AUTO_KEY_ADJ", "AUTO_KEY_ADJ", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PRD_PMEMO_HDR", "PRD_PMEMO_DET", "AUTO_KEY_PMO", "AUTO_KEY_PMO", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("", "PRD_BOM_HDR", "", "", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_BOM_DET", "", "", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_BOM_DET", "", "", "RM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart

        If UpdateItemCode("", "PRD_NEWBOM_HDR", "", "", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_NEWBOM_DET", "", "", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_NEWBOM_DET", "", "", "RM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_BOM_ALTER_DET", "", "", "MAINITEM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_BOM_ALTER_DET", "", "", "ALTER_RM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart

        If UpdateItemCode("", "PRD_OUTBOM_HDR", "", "", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_OUTBOM_DET", "", "", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_OUTBOM_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_OUTBOM_ALTER_DET", "", "", "PRODUCT_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_OUTBOM_ALTER_DET", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("", "PRD_OUTBOM_ALTER_DET", "", "", "ALTER_ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart

        ''************						
        mSqlStr = "ALTER TABLE PUR_DAILY_SCHLD_DET MODIFY CONSTRAINT PUR_DAILY_SCHLD_DET_FK DISABLE"
        PubDBCn.Execute(mSqlStr)

        If UpdateItemCode("PUR_DELV_SCHLD_HDR", "PUR_DELV_SCHLD_DET", "AUTO_KEY_DELV", "AUTO_KEY_DELV", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PUR_DELV_SCHLD_HDR", "PUR_DAILY_SCHLD_DET", "AUTO_KEY_DELV", "AUTO_KEY_DELV", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        mSqlStr = "ALTER TABLE PUR_DAILY_SCHLD_DET MODIFY CONSTRAINT PUR_DAILY_SCHLD_DET_FK ENABLE"
        PubDBCn.Execute(mSqlStr)

        ''************						

        If UpdateItemCode("PUR_INDENT_HDR", "PUR_INDENT_DET", "AUTO_KEY_INDENT", "AUTO_KEY_INDENT", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PUR_PURCHASE_HDR", "PUR_POCONS_IND_TRN", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PUR_PURCHASE_HDR", "PUR_PURCHASE_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("", "QAL_DEVIATION_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "QAL_FINAL_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "QAL_FLASH_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "QAL_INSPECTION_STD_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        '    If UpdateItemCode("", "QAL_INSTRUMENT_MST", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart						
        If UpdateItemCode("", "QAL_LAYOUT_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "QAL_MASTER_SAMPLE_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        '    If UpdateItemCode("", "QAL_OTH_INST_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart						
        If UpdateItemCode("", "QAL_PART_TESTREPORT_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "QAL_PPKPLANSR_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "QAL_PROCESS_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "QAL_RECEIPT_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart
        If UpdateItemCode("", "QAL_REJPRODANALY_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart

        If UpdateItemCode("", "TOL_TOOLINFO_MST", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, True) = False Then GoTo ErrPart

        If UpdateItemCode("", "FIN_RGDAILYMANU_HDR", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart
        If UpdateItemCode("FIN_MANU_INT_HDR", "FIN_MANU_INT_DET", "AUTO_KEY_REF", "AUTO_KEY_REF", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("INV_PHY_HDR", "INV_PHY_DET", "AUTO_KEY_PHY", "AUTO_KEY_PHY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("PRD_CONSUMPTION_HDR", "PRD_CONSUMPTION_DET", "AUTO_KEY_CONS", "AUTO_KEY_CONS", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart

        If UpdateItemCode("FIN_CT_HDR", "FIN_CT_DET", "MKEY", "MKEY", "ITEM_CODE", mItemCodeFrom, mItemCodeTo) = False Then GoTo ErrPart
        If UpdateItemCode("", "FIN_CT_TRN", "", "", "ITEM_CODE", mItemCodeFrom, mItemCodeTo, True, False) = False Then GoTo ErrPart

        SqlStr = " DELETE From INV_ITEM_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCodeFrom) & "'"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        UpdateMain1 = True

        Exit Function
ErrPart:
        'Resume						
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateMain1 = False
        PubDBCn.RollbackTrans()
    End Function

    Private Function UpdateItemCode(ByRef mHDRTable As String, ByRef mDETTABLE As String, ByRef mHDRRefKey As String, ByRef mDETRefKey As String, ByRef mUpdateKey As String, ByRef pItemCodeFrom As String, ByRef pItemCodeTo As String, Optional ByRef mMasterTable As Boolean = False, Optional ByRef mModField As Boolean = False, Optional ByRef mIsTRNTable As Boolean = False) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String

        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim cntField As Integer
        Dim mCompanyField As Boolean
        Dim mTableStr As String

        mTableStr = mHDRTable
        mTableStr = IIf(mTableStr = "", "", mTableStr & " AND ") & mDETTABLE

        lblStatus.Text = "Merging in " & mTableStr & "... "
        System.Windows.Forms.Application.DoEvents()

        If mMasterTable = True Then
            SqlStr = " UPDATE " & mDETTABLE & " SET " & vbCrLf & " " & mUpdateKey & "='" & pItemCodeTo & "'"

            If mModField = True Then
                SqlStr = SqlStr & vbCrLf & " , MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            SqlStr = SqlStr & vbCrLf & " WHERE " & mUpdateKey & "='" & pItemCodeFrom & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            PubDBCn.Execute(SqlStr)

        Else
            If mIsTRNTable = False Then
                mCompanyField = False
                pSqlStr = "SELECT * FROM " & mDETTABLE & " WHERE 1=2"
                MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                For cntField = 0 To RsTemp.Fields.Count - 1
                    If RsTemp.Fields(cntField).Name = "COMPANY_CODE" Then
                        mCompanyField = True
                        Exit For
                    End If
                Next

                SqlStr = " UPDATE " & mHDRTable & " SET "

                'If UCase(mHDRTable) = "FIN_INVOICE_HDR" Or UCase(mHDRTable) = "FIN_PURCHASE_HDR" Or UCase(mHDRTable) = "FIN_DNCN_HDR" Or UCase(mHDRTable) = "INV_GATE_HDR" Or UCase(mHDRTable) = "TDS_TRN" Or UCase(mHDRTable) = "TCS_TRN" Then
                '    SqlStr = SqlStr & vbCrLf & " UPDATE_FROM='" & PubRun_IN & "',"
                'End If

                SqlStr = SqlStr & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE " & mHDRRefKey & " IN " & vbCrLf & " ( SELECT IH." & mHDRRefKey & "" & vbCrLf & " FROM " & mHDRTable & " IH, " & mDETTABLE & " ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH." & mHDRRefKey & "=ID." & mDETRefKey & " " & vbCrLf & " AND ID." & mUpdateKey & "='" & pItemCodeFrom & "') "

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE " & mDETTABLE & " SET " & vbCrLf & " " & mUpdateKey & "='" & pItemCodeTo & "'" & vbCrLf & " WHERE " & vbCrLf
                SqlStr = SqlStr & vbCrLf & " " & mUpdateKey & "='" & pItemCodeFrom & "'" & vbCrLf & " AND " & mDETRefKey & " IN " & vbCrLf & " ( SELECT IH." & mHDRRefKey & "" & vbCrLf & " FROM " & mHDRTable & " IH, " & mDETTABLE & " ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH." & mHDRRefKey & "=ID." & mDETRefKey & " " & vbCrLf & " AND ID." & mUpdateKey & "='" & pItemCodeFrom & "') "

                '            If mDETTABLE = "PRD_COST_MAINCONS_DET" Or mDETTABLE = "FIN_SUPP_CUST_DET" Or mDETTABLE = "DSP_CONSUMPTION_DET" Or mDETTABLE = "PRD_COST_CONS_DET" Or mDETTABLE = "PRD_COST_MAIN_DET" Then						
                If mCompanyField = True Then
                    SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                End If

                PubDBCn.Execute(SqlStr)
            Else
                SqlStr = " UPDATE " & mDETTABLE & " SET " & vbCrLf & " " & mUpdateKey & "='" & pItemCodeTo & "'" & vbCrLf & " WHERE " & mUpdateKey & "='" & pItemCodeFrom & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                PubDBCn.Execute(SqlStr)
            End If
        End If

        UpdateItemCode = True

        Exit Function
ErrPart:
        'Resume						
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateItemCode = False
    End Function

    Public Sub FrmItemCodeMerging_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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
    Public Sub FrmItemCodeMerging_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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



    Private Sub txtItemCodeFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCodeFrom.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtItemCodeFrom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCodeFrom.DoubleClick
        SearchCodeFrom()
    End Sub

    Private Sub txtItemCodeFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCodeFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCodeFrom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemCodeFrom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCodeFrom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCodeFrom()
    End Sub
    Private Sub txtItemCodeFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCodeFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsItemMast As ADODB.Recordset

        If Trim(txtItemCodeFrom.Text) = "" Then GoTo EventExitSub

        SqlStr = "Select ITEM_SHORT_DESC From INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(txtItemCodeFrom.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemMast.EOF = False Then
            txtItemNameFrom.Text = IIf(IsDBNull(RsItemMast.Fields("Item_Short_Desc").Value), "", RsItemMast.Fields("Item_Short_Desc").Value)
        Else
            MsgBox("Invalid Item Code.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCodeTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCodeTo.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtItemCodeTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCodeTo.DoubleClick
        SearchCodeTo()
    End Sub

    Private Sub txtItemCodeTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCodeTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCodeTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemCodeTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCodeTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCodeTo()
    End Sub


    Private Sub txtItemCodeTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCodeTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsItemMast As ADODB.Recordset

        If Trim(txtItemCodeTo.Text) = "" Then GoTo EventExitSub

        SqlStr = "Select ITEM_SHORT_DESC From INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(txtItemCodeTo.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemMast.EOF = False Then
            txtItemNameTo.Text = IIf(IsDBNull(RsItemMast.Fields("Item_Short_Desc").Value), "", RsItemMast.Fields("Item_Short_Desc").Value)
        Else
            MsgBox("Invalid Item Code.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtItemNameFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemNameFrom.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtItemNameFrom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemNameFrom.DoubleClick
        cmdSearchItemFrom_Click(cmdSearchItemFrom, New System.EventArgs())
    End Sub

    Private Sub SearchCodeFrom()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtItemCodeFrom.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
            txtItemCodeFrom.Text = AcName
            '' txtItemCodeFrom_Validate False
            txtItemCodeFrom_Validating(txtItemCodeFrom, New System.ComponentModel.CancelEventArgs(False))
            If txtItemCodeFrom.Enabled = True Then txtItemCodeFrom.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchCodeTo()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtItemCodeTo.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
            txtItemCodeTo.Text = AcName
            'txtItemCodeTo_Validate False
            txtItemCodeTo_Validating(txtItemCodeTo, New System.ComponentModel.CancelEventArgs(False))
            If txtItemCodeTo.Enabled = True Then txtItemCodeTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtItemNameFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemNameFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemNameFrom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemNameFrom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemNameFrom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchItemFrom_Click(cmdSearchItemFrom, New System.EventArgs())
    End Sub


    Private Sub txtItemNameFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemNameFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsItemMast As ADODB.Recordset

        If Trim(txtItemNameFrom.Text) = "" Then GoTo EventExitSub

        SqlStr = "Select ITEM_CODE From INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(txtItemNameFrom.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemMast.EOF = False Then
            txtItemCodeFrom.Text = IIf(IsDBNull(RsItemMast.Fields("ITEM_CODE").Value), "", RsItemMast.Fields("ITEM_CODE").Value)
        Else
            MsgBox("Invalid Item Name.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtItemNameTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemNameTo.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtItemNameTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemNameTo.DoubleClick
        cmdSearchItemTo_Click(cmdSearchItemTo, New System.EventArgs())
    End Sub

    Private Sub txtItemNameTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemNameTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemNameTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemNameTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemNameTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchItemTo_Click(cmdSearchItemTo, New System.EventArgs())
    End Sub


    Private Sub txtItemNameTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemNameTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsItemMast As ADODB.Recordset

        If Trim(txtItemNameTo.Text) = "" Then GoTo EventExitSub

        SqlStr = "Select ITEM_CODE From INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(txtItemNameTo.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemMast.EOF = False Then
            txtItemCodeTo.Text = IIf(IsDBNull(RsItemMast.Fields("ITEM_CODE").Value), "", RsItemMast.Fields("ITEM_CODE").Value)
        Else
            MsgBox("Invalid Item Name.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class


