Option Strict Off
Option Explicit On
Imports System.ComponentModel
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmExportInvoice
    Inherits System.Windows.Forms.Form
    Dim RsExpMain As ADODB.Recordset
    Dim RsExpDetail As ADODB.Recordset
    Dim RsExpExp As ADODB.Recordset

    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer

    Dim mCustomerCode As String
    Dim mWithOutOrder As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColPartNo As Short = 3
    Private Const ColUnit As Short = 4

    Private Const ColGlassDescription As Short = 5
    Private Const ColActualWidth As Short = 6
    Private Const ColActualHeight As Short = 7
    Private Const ColSize As Short = 8

    Private Const ColChargeableWidth As Short = 9
    Private Const ColChargeableHeight As Short = 10
    Private Const ColGlassArea As Short = 11

    Private Const ColModelNo As Short = 12
    Private Const ColDrawingNo As Short = 13

    Private Const ColMarks As Short = 14
    Private Const ColPalletNo As Short = 15
    Private Const ColPalletQty As Short = 16
    Private Const ColQty As Short = 17
    Private Const ColRate As Short = 18
    Private Const ColRateINR As Short = 19
    Private Const ColAmount As Short = 20
    Private Const ColAmountINR As Short = 21
    Private Const ColSONo As Short = 22
    Private Const ColSODate As Short = 23
    Private Const ColBuyerPO As Short = 24
    Private Const ColBuyerDate As Short = 25

    Private Const ColOthersName As Short = 1
    Private Const ColOthersDesc As Short = 2
    Private Const ColOthersValue As Short = 3
    Private Sub SprdOther_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdOther.ClickEvent

        Dim SqlStr As String = ""
        Dim mFieldName As String = ""
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If eventArgs.row = 0 And eventArgs.col = ColOthersValue Then
            With SprdOther
                .Row = .ActiveRow
                .Col = ColOthersName
                mFieldName = Trim(.Text)

                If Trim(mFieldName) <> "" Then
                    SqlStr = "SELECT FIELD_VALUE " & vbCrLf _
                            & " FROM FIN_EXPORT_FIELD_MST" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND FIELD_NAME='" & mFieldName & "'"

                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColOthersValue
                        .Text = Trim(AcName)
                    End If
                End If
                MainClass.SetFocusToCell(SprdOther, SprdOther.ActiveRow, ColOthersValue)
            End With
        End If


    End Sub
    Private Sub SprdOther_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdOther.LeaveCell

        On Error GoTo ErrPart
        Dim mName As String = ""
        Dim mValue As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColOthersValue
                SprdOther.Row = SprdOther.ActiveRow

                SprdOther.Col = ColOthersName
                mName = SprdOther.Text

                If mName = "" Then Exit Sub

                SprdOther.Col = ColOthersValue
                mValue = SprdOther.Text

                If mValue = "" Then Exit Sub

                SqlStr = "SELECT FIELD_VALUE " & vbCrLf _
                        & " FROM FIN_EXPORT_FIELD_MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND FIELD_NAME='" & mName & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    If MainClass.ValidateWithMasterTable(mValue, "FIELD_VALUE", "FIELD_VALUE", "FIN_EXPORT_FIELD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIELD_NAME='" & mName & "'") = False Then
                        MsgInformation("Please Select Vaild Value.")
                        MainClass.SetFocusToCell(ColOthersName, eventArgs.row, ColOthersValue)
                    End If
                End If


        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkREXDeclaration_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkREXDeclaration.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkDC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDC.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkExciseInvoice_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExciseInvoice.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdBank_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBank.Click
        If MainClass.SearchGridMaster((txtCreditBank.Text), "FIN_EXP_BANK_MST", "BANK_NAME", "BANK_ALIAS", "BANK_ADCODE", "BANK_ADCODE", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCreditBank.Text = AcName
            txtCreditBank_Validating(txtCreditBank, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub cmdPackNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPackNo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND EXP_INV_MADE='N'"

        SqlStr = " SELECT IH.AUTO_KEY_PACK, IH.PACK_DATE, IH.BUYER_PO, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO" & vbCrLf _
            & " FROM DSP_PACKING_HDR IH, DSP_PACKING_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " And IH.EXP_INV_MADE='N'" & vbCrLf _
            & " AND IH.AUTO_KEY_PACK = ID.AUTO_KEY_PACK " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then         ''(txtPackNo.Text), "DSP_PACKING_HDR", "AUTO_KEY_PACK", "PACK_DATE", , , SqlStr) = True Then
            txtPackNo.Text = AcName
            txtPackNo_Validating(txtPackNo, New System.ComponentModel.CancelEventArgs(False))
            If txtPackNo.Enabled = True Then txtPackNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonInv(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportonInv(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String
        Dim mPrintOption As String = ""


        frmPrintInvoice.OptInvoice.Enabled = True
        frmPrintInvoice.OptInvoice.Visible = True
        frmPrintInvoice.OptInvoice.Text = "Export Invoice"
        frmPrintInvoice.OptInvoiceAnnex.Enabled = True
        frmPrintInvoice.OptInvoiceAnnex.Visible = True
        frmPrintInvoice.OptInvoiceAnnex.Text = "Packing List"
        frmPrintInvoice.optSubsidiaryChallan.Enabled = False
        frmPrintInvoice.optSubsidiaryChallan.Visible = False
        frmPrintInvoice.FraF4.Enabled = False
        frmPrintInvoice.FraF4.Visible = False
        frmPrintInvoice.ShowDialog()

        If G_PrintLedg = False Then
            frmPrintInvoice.Close()
            frmPrintInvoice.Dispose()
            Exit Sub
        Else
            mPrintOption = IIf(frmPrintInvoice.OptInvoice.Checked = True, "E", "P") 'E-Export Invoice , P-Packing List					
            frmPrintInvoice.Close()
            frmPrintInvoice.Dispose()
        End If


        Report1.Reset()
        If mPrintOption = "E" Then
            mTitle = "COMMERCIAL INVOICE"
            mSubTitle = ""
            mRptFileName = "ExportInv.RPT"
        Else
            mTitle = "PACKING LIST"
            mSubTitle = ""
            mRptFileName = "ExportPacking.RPT"
        End If

        SqlStr = MakeSQL(mPrintOption)

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonInv(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SprdMain_LeaveRow(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles SprdMain.LeaveRow
        '    SprdMain.Row = Row
        '    SprdMain.Row2 = Row
        '    SprdMain.Col = 1
        '    SprdMain.col2 = SprdMain.ActiveCol
        '    SprdMain.BlockMode = True
        '    SprdMain.BackColor = &HFFFF80
        '    SprdMain.BlockMode = False
    End Sub

    Private Sub txtAccountNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAccountNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAccountNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAccountNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAccountNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtADCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtADCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAdvLicDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvLicDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAdvLicDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvLicDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtAdvLicDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtAdvLicDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        '    If FYChk(txtPackDate.Text) = False Then
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtAdvLicNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvLicNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAdvLicNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvLicNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAgreement_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAgreement.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAgreement_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAgreement.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAgreement.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBuyerDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBuyerDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBuyerDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtBuyerDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtBuyerDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtBuyerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBuyerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBuyerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBuyerNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBuyerNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBuyerNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCarriage_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriage.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCarriage_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriage.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriage.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtContainerNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContainerNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtContainerNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContainerNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtContainerNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCreditBank_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditBank.DoubleClick
        cmdBank_Click(cmdBank, New System.EventArgs())
    End Sub

    Private Sub txtCreditBank_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCreditBank.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdBank_Click(cmdBank, New System.EventArgs())
        End If
    End Sub

    Private Sub txtCreditBank_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCreditBank.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFurtherBank As String

        SqlStr = "SELECT * FROM FIN_EXP_BANK_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BANK_NAME='" & MainClass.AllowSingleQuote((txtCreditBank.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            txtCreditBankAddress.Text = IIf(IsDBNull(RsTemp.Fields("BANK_ADD").Value), "", RsTemp.Fields("BANK_ADD").Value)
            txtADCode.Text = IIf(IsDBNull(RsTemp.Fields("BANK_ADCODE").Value), "", RsTemp.Fields("BANK_ADCODE").Value)
            txtSwiftCode.Text = IIf(IsDBNull(RsTemp.Fields("BANK_SWIFTCODE").Value), "", RsTemp.Fields("BANK_SWIFTCODE").Value)
            mFurtherBank = "M/s " & RsCompany.Fields("Company_Name").Value
            mFurtherBank = mFurtherBank & " Account " & IIf(IsDBNull(RsTemp.Fields("BANK_AC_NO").Value), "", RsTemp.Fields("BANK_AC_NO").Value)
            mFurtherBank = mFurtherBank & " WITH " & IIf(IsDBNull(RsTemp.Fields("BANK_NAME").Value), "", RsTemp.Fields("BANK_NAME").Value)
            mFurtherBank = mFurtherBank & " " & IIf(IsDBNull(RsTemp.Fields("BANK_ADD").Value), "", RsTemp.Fields("BANK_ADD").Value)
            txtFurtherBank.Text = UCase(mFurtherBank)
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCreditBankAddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditBankAddress.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCreditBankAddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditBankAddress.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCreditBankAddress.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCurrency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCurrency.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCurrency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCurrency.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCurrFactor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCurrFactor.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub

    Private Sub txtCurrFactor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrFactor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCurrFactor_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCurrFactor.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Call CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustomerBank_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerBank.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomerBank_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerBank.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomerBank.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCustomerCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomerCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomerCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCustomerCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomerCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtCustomerCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtCustomerName.Text = MasterNo
            mCustomerCode = txtCustomerCode.Text
        Else
            mCustomerCode = "-1"
            TxtCustomerName.Text = ""
            Cancel = True
        End If

        txtConsigneeAddress.Text = FillAddressDetail(txtCustomerCode.Text, txtShipTo.Text)

        If ADDMode = True Then
            Call FillCustomerDetail()
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FillCustomerDetail()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xBuyerCode As String


        SqlStr = " SELECT BUYERCODE, CARRIAGE, LOADINGPORT, " & vbCrLf & " DISCHARGEPORT, FINALDEST, PAYMENTTERMS,SUPP_CUST_CITY, SUPP_CUST_STATE " & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCustomerCode.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            With RsTemp
                xBuyerCode = IIf(IsDBNull(.Fields("BUYERCODE").Value), "", .Fields("BUYERCODE").Value)

                If MainClass.ValidateWithMasterTable(xBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBuyerName.Text = MasterNo
                End If

                txtCarriage.Text = IIf(IsDBNull(.Fields("CARRIAGE").Value), "", .Fields("CARRIAGE").Value)
                txtLoading.Text = IIf(IsDBNull(.Fields("LOADINGPORT").Value), "", .Fields("LOADINGPORT").Value)
                txtDischarge.Text = IIf(IsDBNull(.Fields("DISCHARGEPORT").Value), "", .Fields("DISCHARGEPORT").Value)
                txtFinalDestination.Text = IIf(IsDBNull(.Fields("FINALDEST").Value), "", .Fields("FINALDEST").Value)
                txtPayments.Text = IIf(IsDBNull(.Fields("PAYMENTTERMS").Value), "", .Fields("PAYMENTTERMS").Value)
                txtDestination.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CITY").Value), "", .Fields("SUPP_CUST_CITY").Value) & ", " & IIf(IsDBNull(.Fields("SUPP_CUST_STATE").Value), "", .Fields("SUPP_CUST_STATE").Value)
            End With
        End If
        Exit Sub

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtCustomerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCustomerName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub txtCustomerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCustomerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCustomerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDestination_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDestination.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDestination_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDestination.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDestination.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDiscAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDiscAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDiscAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDischarge_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDischarge.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDischarge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDischarge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDischarge.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDiscPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDiscPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDiscPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDiscPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDiscPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtExciseBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExciseBillDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExciseBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExciseBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtExciseBillDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtExciseBillDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtExciseBillDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtExciseBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExciseBillNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExciseBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExciseBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExciseBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtFinalDestination_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFinalDestination.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFinalDestination_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFinalDestination.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFinalDestination.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtFlight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFlight.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFlight_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFlight.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFlight.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtFurtherBank_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFurtherBank.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFurtherBank_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFurtherBank.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFurtherBank.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtIECNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIECNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIECNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIECNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIECNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCreditBank_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditBank.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCreditBank_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditBank.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCreditBank.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInvDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtInvDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtInvDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtInvDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtInvNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInvNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInvNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As Double
        Dim SqlStr As String = ""

        If Trim(txtInvNo.Text) = "" Then GoTo EventExitSub



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then
            txtInvNo.Text = VB6.Format(Val(txtInvNo.Text), ConBillFormat)
        Else
            txtInvNo.Text = VB6.Format(Val(txtInvNo.Text), "000000")
        End If

        If MODIFYMode = True And RsExpMain.EOF = False Then xMkey = RsExpMain.Fields("AUTO_KEY_EXPINV").Value

        SqlStr = " SELECT * FROM FIN_EXPINV_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BILLNOSEQ=" & Val(txtInvNo.Text) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsExpMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsExpMain.EOF = False Then
            Clear1()
            'Call FillSprdOther()
            Show1()

        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Export Invoice, Use Generate Invoice Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_EXPINV_HDR " & vbCrLf & " WHERE AUTO_KEY_EXPINV=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsExpMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInvPrefix_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvPrefix.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvPrefix_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvPrefix.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInvPrefix.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtLoading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoading.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLoading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLoading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLoading.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNotifyParty1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNotifyParty1.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNotifyParty1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNotifyParty1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNotifyParty1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNotifyParty2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNotifyParty2.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNotifyParty2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNotifyParty2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNotifyParty2.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNotifyParty3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNotifyParty3.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNotifyParty3_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNotifyParty3.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNotifyParty3.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOrigin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOrigin.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOrigin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOrigin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOrigin.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtOtherAmt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOtherAmt.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOtherAmt_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOtherAmt.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOtherAmt_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOtherAmt.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTots()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPackNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPackNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            'Call FillSprdOther()
            SprdMain.Enabled = True

            Call FillSprdOther()


        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Call FormatSprdMain(-1)


            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim xDCNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If chkDC.CheckState = System.Windows.Forms.CheckState.Checked Or chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Transaction Made Against This Invoice So Cann't be Deleted")
            Exit Sub
        End If

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Invoice Cann't be Deleted.")
            Exit Sub
        End If

        If ValidateBranchLocking((txtInvDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, txtInvDate.Text, (TxtCustomerName.Text), mCustomerCode) = True Then
            Exit Sub
        End If

        If Trim(txtInvNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        '    If CheckBillPayment(mCustomerCode, txtBillNo.Text, "B") = True Then Exit Sub

        If Not RsExpMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "FIN_EXPINV_HDR", (txtInvNo.Text), RsExpMain, "REFNO") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "FIN_EXPINV_HDR", "AUTO_KEY_EXPINV", (LblMkey.Text)) = False Then GoTo DelErrPart



                SqlStr = "UPDATE DSP_PACKING_HDR SET " & vbCrLf & " INVOICE_NO='', " & vbCrLf & " INVOICE_DATE=''," & vbCrLf & " EXP_INV_MADE='N' " & vbCrLf & " WHERE AUTO_KEY_PACK =" & Val(txtPackNo.Text) & ""

                PubDBCn.Execute(SqlStr)
                PubDBCn.Execute("Delete from FIN_EXPORT_PARA_EXP Where AUTO_KEY_EXPINV=" & Val(LblMkey.Text) & "")
                PubDBCn.Execute("Delete from FIN_EXPINV_DET Where AUTO_KEY_EXPINV=" & Val(LblMkey.Text) & "")
                PubDBCn.Execute("Delete from FIN_EXPINV_HDR Where AUTO_KEY_EXPINV=" & Val(LblMkey.Text) & "")

                PubDBCn.CommitTrans()
                RsExpMain.Requery() ''.Refresh
                RsExpDetail.Requery() ''.Refresh
                Clear1()
                'Call FillSprdOther()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsExpMain.Requery() ''.Refresh
        RsExpDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If PubSuperUser <> "S" Then
            If chkDC.CheckState = System.Windows.Forms.CheckState.Checked Or chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Transaction Made Against This Invoice So Cann't be Deleted")
                Exit Sub
            End If
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsExpMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtInvNo.Enabled = True ''IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            'Call FillSprdOther()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Call CalcTots()

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtInvNo_Validating(txtInvNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True And cmdAdd.Visible = True Then cmdAdd.Focus()
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
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""


        Exit Sub
        'If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
        '    With SprdMain
        '        .Row = .ActiveRow
        '        .Col = ColItemCode
        '        SqlStr = GetSearchItem("Y")
        '        If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
        '            .Row = .ActiveRow
        '            .Col = ColItemCode
        '            .Text = Trim(AcName)
        '            .Col = ColItemDesc
        '            .Text = Trim(AcName1)
        '        End If
        '        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
        '    End With
        'End If

        'If eventArgs.row = 0 And eventArgs.col = ColItemDesc And SprdMain.Enabled = True Then
        '    With SprdMain
        '        .Row = .ActiveRow
        '        .Col = ColItemDesc
        '        SqlStr = GetSearchItem("N")
        '        If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
        '            .Row = .ActiveRow
        '            .Col = ColItemDesc
        '            .Text = Trim(AcName)
        '            .Col = ColItemCode
        '            .Text = Trim(AcName1)
        '        End If
        '        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
        '    End With
        'End If

        'Dim DelStatus As Boolean
        'DelStatus = False
        'If eventArgs.col = 0 And eventArgs.row > 0 Then
        '    SprdMain.Row = eventArgs.row
        '    SprdMain.Col = ColItemCode
        '    If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
        '        MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode, DelStatus)
        '        FormatSprdMain(-1)
        '        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        '    End If
        'End If

    End Sub
    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        xSuppCode = IIf(Trim(txtCustomerCode.Text) = "", "-1", Trim(txtCustomerCode.Text))

        If mByCode = "Y" Then
            mSqlStr = "SELECT B.ITEM_CODE,A.ITEM_SHORT_DESC "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,B.ITEM_CODE "
        End If

        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'"
        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String
        Dim mPallet As String
        Dim mCheckRowData As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColPalletNo
                mCheckRowData = xICode & "-" & Trim(SprdMain.Text)

                SprdMain.Col = ColGlassDescription
                mCheckRowData = mCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)


                SprdMain.Col = ColSize
                mCheckRowData = mCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Col = ColModelNo
                mCheckRowData = mCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Col = ColDrawingNo
                mCheckRowData = mCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(mCheckRowData) = False Then
                        If FillGridRow(xICode) = False Then Exit Sub
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If

        End Select

        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetValidItem(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        xSuppCode = IIf(Trim(txtCustomerCode.Text) = "", "-1", Trim(txtCustomerCode.Text))


        mSqlStr = "SELECT B.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND B.ITEM_CODE LIKE '" & pItemCode & "%'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidItem = True
        Else
            MsgInformation("Please Check Item.")
            GetValidItem = False
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function
    Private Function FillGridRow(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select INVMST.* " & vbCrLf & " FROM INV_ITEM_MST INVMST" & vbCrLf & " WHERE " & vbCrLf & " INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)

                SprdMain.Col = ColPartNo
                SprdMain.Text = IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)

                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                FormatSprdMain(-1)

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
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row

            .Col = 1
            txtInvNo.Text = CStr(Val(.Text))

            txtInvNo_Validating(txtInvNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Sub txtPackDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPackDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPackDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPackDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtPackDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtPackDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPackNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPackNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtPackNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPackNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim SqlStr As String = ""
        Dim RsPackMain As ADODB.Recordset = Nothing

        If Trim(txtPackNo.Text) = "" Then GoTo EventExitSub

        If Len(txtPackNo.Text) < 6 Then
            txtPackNo.Text = VB6.Format(Val(txtPackNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        xMkey = Trim(txtPackNo.Text)

        SqlStr = " SELECT * FROM DSP_PACKING_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PACK=" & Val(xMkey) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPackMain.EOF = False Then
            Clear1()
            'Call FillSprdOther()
            Call ShowPackMain(RsPackMain)
        Else
            MsgBox("Invalid Packing List No.", MsgBoxStyle.Information)
            Cancel = True
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
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        Dim mAutoBillNo As Double
        Dim mBillNo As String
        Dim mSuppCustCode As String
        Dim mDCMade As String
        Dim mEXCISE_INV_MADE As String
        Dim mBuyerCode As String = ""
        Dim mCancelled As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        PubDBCn.Execute("Delete From FIN_EXPINV_DET Where AUTO_KEY_EXPINV='" & LblMkey.Text & "'")
        PubDBCn.Execute("Delete From FIN_EXPORT_PARA_EXP Where AUTO_KEY_EXPINV='" & LblMkey.Text & "'")

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBuyerCode = MasterNo
        End If

        mDCMade = IIf(chkDC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mEXCISE_INV_MADE = IIf(chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtInvNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtInvNo.Text)
        End If

        txtInvNo.Text = CStr(Val(CStr(mVNoSeq)))


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then
            mBillNo = txtInvPrefix.Text & VB6.Format(txtInvNo.Text, ConBillFormat)
        Else
            mBillNo = txtInvPrefix.Text & VB6.Format(txtInvNo.Text, "000000")
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Then
            mAutoBillNo = CDbl(Mid(mVNoSeq, 5, 4) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        Else
            mAutoBillNo = CDbl(mVNoSeq & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        End If

        ''Temp. Commit.....
        If CheckValidVDate(mVNoSeq) = False Then GoTo ErrPart

        SqlStr = ""

        If ADDMode = True Then
            LblMkey.Text = CStr(mAutoBillNo)
            SqlStr = "INSERT INTO FIN_EXPINV_HDR( " & vbCrLf _
                & " COMPANY_CODE, FYEAR, " & vbCrLf _
                & " AUTO_KEY_EXPINV, EXPINV_DATE," & vbCrLf _
                & " BILLNOPREFIX, BILLNOSEQ," & vbCrLf _
                & " BILLNO," & vbCrLf _
                & " AUTO_KEY_PACK, PACK_DATE," & vbCrLf _
                & " SUPP_CUST_CODE, REF_NO," & vbCrLf _
                & " BUYER_PO, BUYER_PO_DATE, " & vbCrLf _
                & " EXCISE_INV_NO, EXCISE_INV_DATE," & vbCrLf _
                & " ORIGIN_COUNTRY, DEST_COUNTRY, CARRIAGE," & vbCrLf _
                & " LOADINGPORT, DISCHARGEPORT, FINALDEST," & vbCrLf _
                & " PAYMENTTERMS, FLIGHT_NO, CONTAINERNO," & vbCrLf _
                & " RECIPT_PLACE, REMARKS, " & vbCrLf _
                & " AGREEMENT, CREDITBANK, CREDITBANK_ADD," & vbCrLf _
                & " CUST_BANK, CUST_ACCTNO," & vbCrLf _
                & " SWIFT_CODE, FURTHER_BANK," & vbCrLf _
                & " TOTQTY, NETVALUE,NETVALUE_INR, " & vbCrLf _
                & " CURR_DESC, CON_FACTOR," & vbCrLf _
                & " DC_MADE, EXCISE_INV_MADE, BUYER_CODE," & vbCrLf _
                & " ADV_LIC_NO, ADV_LIC_DATE, AD_CODE," & vbCrLf _
                & " NOTIFY_PARTY_1, NOTIFY_PARTY_2, NOTIFY_PARTY_3, DESC_PER, DISC_AMOUNT,OTHER_AMOUNT," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, CANCELLED,BILL_TO_LOC_ID ,SHIP_TO_LOC_ID,IS_REXDeclaration  )"

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & Val(CStr(mAutoBillNo)) & ", TO_DATE('" & VB6.Format(txtInvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtInvPrefix.Text)) & "'," & Val(CStr(mVNoSeq)) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf _
                & " " & Val(txtPackNo.Text) & " , TO_DATE('" & VB6.Format(txtPackDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote((txtIECNo.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtBuyerNo.Text)) & "', TO_DATE('" & VB6.Format(txtBuyerDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtExciseBillNo.Text)) & "', TO_DATE('" & VB6.Format(txtExciseBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtOrigin.Text)) & "', '" & MainClass.AllowSingleQuote((txtDestination.Text)) & "', '" & MainClass.AllowSingleQuote((txtCarriage.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtLoading.Text)) & "', '" & MainClass.AllowSingleQuote((txtDischarge.Text)) & "', '" & MainClass.AllowSingleQuote((txtFinalDestination.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtPayments.Text)) & "', '" & MainClass.AllowSingleQuote((txtFlight.Text)) & "', '" & MainClass.AllowSingleQuote((txtContainerNo.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtPlace.Text)) & "', '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtAgreement.Text)) & "', '" & MainClass.AllowSingleQuote((txtCreditBank.Text)) & "', '" & MainClass.AllowSingleQuote((txtCreditBankAddress.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCustomerBank.Text) & "', '" & MainClass.AllowSingleQuote(txtAccountNo.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSwiftCode.Text) & "', '" & MainClass.AllowSingleQuote(txtFurtherBank.Text) & "'," & vbCrLf _
                & " " & Val(lblTotQty.Text) & ", " & Val(lblTotAmount.Text) & ", " & Val(lblTotAmount_INR.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtCurrency.Text)) & "', " & Val(txtCurrFactor.Text) & "," & vbCrLf _
                & " '" & mDCMade & "', '" & mEXCISE_INV_MADE & "', '" & MainClass.AllowSingleQuote(mBuyerCode) & "'," & vbCrLf _
                & " " & Val(txtAdvLicNo.Text) & ", TO_DATE('" & VB6.Format(txtAdvLicDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtADCode.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtNotifyParty1.Text)) & "','" & MainClass.AllowSingleQuote((txtNotifyParty2.Text)) & "', '" & MainClass.AllowSingleQuote((txtNotifyParty3.Text)) & "'," & vbCrLf _
                & " " & Val(txtDiscPer.Text) & "," & Val(txtDiscAmount.Text) & ", " & Val(txtOtherAmt.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','N','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "' ,'" & MainClass.AllowSingleQuote(txtShipTo.Text) & "','" & IIf(chkREXDeclaration.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "')"

        ElseIf MODIFYMode = True Then

            SqlStr = ""
            SqlStr = "UPDATE FIN_EXPINV_HDR SET "

            SqlStr = SqlStr & vbCrLf _
                & " AUTO_KEY_EXPINV=" & Val(CStr(mAutoBillNo)) & ", " & vbCrLf _
                & " EXPINV_DATE=TO_DATE('" & VB6.Format(txtInvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " BILLNOPREFIX='" & MainClass.AllowSingleQuote((txtInvPrefix.Text)) & "', " & vbCrLf _
                & " BILLNOSEQ=" & Val(CStr(mVNoSeq)) & ", IS_REXDeclaration='" & IIf(chkREXDeclaration.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " BILLNO='" & MainClass.AllowSingleQuote(mBillNo) & "', BILL_TO_LOC_ID ='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "' ,SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtShipTo.Text) & "',"

            SqlStr = SqlStr & vbCrLf _
                & " AUTO_KEY_PACK =" & Val(txtPackNo.Text) & " ," & vbCrLf _
                & " PACK_DATE=TO_DATE('" & VB6.Format(txtPackDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf _
                & " REF_NO='" & MainClass.AllowSingleQuote((txtIECNo.Text)) & "'," & vbCrLf _
                & " BUYER_PO='" & MainClass.AllowSingleQuote((txtBuyerNo.Text)) & "', " & vbCrLf _
                & " BUYER_PO_DATE=TO_DATE('" & VB6.Format(txtBuyerDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EXCISE_INV_NO='" & MainClass.AllowSingleQuote((txtExciseBillNo.Text)) & "', " & vbCrLf _
                & " EXCISE_INV_DATE=TO_DATE('" & VB6.Format(txtExciseBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " ORIGIN_COUNTRY='" & MainClass.AllowSingleQuote((txtOrigin.Text)) & "', " & vbCrLf _
                & " DEST_COUNTRY='" & MainClass.AllowSingleQuote((txtDestination.Text)) & "', " & vbCrLf _
                & " CARRIAGE='" & MainClass.AllowSingleQuote((txtCarriage.Text)) & "'," & vbCrLf _
                & " LOADINGPORT='" & MainClass.AllowSingleQuote((txtLoading.Text)) & "', " & vbCrLf _
                & " DISCHARGEPORT='" & MainClass.AllowSingleQuote((txtDischarge.Text)) & "', " & vbCrLf _
                & " FINALDEST='" & MainClass.AllowSingleQuote((txtFinalDestination.Text)) & "'," & vbCrLf _
                & " PAYMENTTERMS='" & MainClass.AllowSingleQuote((txtPayments.Text)) & "', " & vbCrLf _
                & " FLIGHT_NO='" & MainClass.AllowSingleQuote((txtFlight.Text)) & "', " & vbCrLf _
                & " CONTAINERNO='" & MainClass.AllowSingleQuote((txtContainerNo.Text)) & "'," & vbCrLf _
                & " RECIPT_PLACE='" & MainClass.AllowSingleQuote((txtPlace.Text)) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', "


            SqlStr = SqlStr & vbCrLf _
                & " AGREEMENT='" & MainClass.AllowSingleQuote(txtAgreement.Text) & "', " & vbCrLf _
                & " CREDITBANK='" & MainClass.AllowSingleQuote(txtCreditBank.Text) & "'," & vbCrLf _
                & " CREDITBANK_ADD='" & MainClass.AllowSingleQuote(txtCreditBankAddress.Text) & "'," & vbCrLf _
                & " CUST_BANK='" & MainClass.AllowSingleQuote(txtCustomerBank.Text) & "', " & vbCrLf _
                & " CUST_ACCTNO='" & MainClass.AllowSingleQuote(txtAccountNo.Text) & "', " & vbCrLf _
                & " SWIFT_CODE='" & MainClass.AllowSingleQuote(txtSwiftCode.Text) & "', " & vbCrLf _
                & " FURTHER_BANK='" & MainClass.AllowSingleQuote(txtFurtherBank.Text) & "', " & vbCrLf _
                & " TOTQTY=" & Val(lblTotQty.Text) & ", " & vbCrLf _
                & " NETVALUE=" & Val(lblTotAmount.Text) & ", " & vbCrLf _
                & " NETVALUE_INR=" & Val(lblTotAmount_INR.Text) & ", " & vbCrLf _
                & " CURR_DESC='" & MainClass.AllowSingleQuote(txtCurrency.Text) & "', " & vbCrLf _
                & " CON_FACTOR=" & Val(txtCurrFactor.Text) & ","


            SqlStr = SqlStr & vbCrLf & " DC_MADE='" & mDCMade & "', " & vbCrLf & " EXCISE_INV_MADE='" & mEXCISE_INV_MADE & "', " & vbCrLf _
                & " BUYER_CODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "', " & vbCrLf _
                & " AD_CODE='" & MainClass.AllowSingleQuote(txtADCode.Text) & "'," & vbCrLf _
                & " ADV_LIC_NO=" & Val(txtAdvLicNo.Text) & ", " & vbCrLf _
                & " ADV_LIC_DATE=TO_DATE('" & VB6.Format(txtAdvLicDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " NOTIFY_PARTY_1='" & MainClass.AllowSingleQuote(txtNotifyParty1.Text) & "', " & vbCrLf _
                & " NOTIFY_PARTY_2='" & MainClass.AllowSingleQuote(txtNotifyParty2.Text) & "', " & vbCrLf _
                & " NOTIFY_PARTY_3='" & MainClass.AllowSingleQuote(txtNotifyParty3.Text) & "' , " & vbCrLf _
                & " DESC_PER=" & Val(txtDiscPer.Text) & ", " & vbCrLf _
                & " DISC_AMOUNT=" & Val(txtDiscAmount.Text) & ", " & vbCrLf _
                & " OTHER_AMOUNT=" & Val(txtOtherAmt.Text) & ", " & vbCrLf _
                & " CANCELLED='" & mCancelled & "',"

            SqlStr = SqlStr & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

            SqlStr = SqlStr & vbCrLf _
                & " WHERE AUTO_KEY_EXPINV ='" & MainClass.AllowSingleQuote(LblMkey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mAutoBillNo) = False Then GoTo ErrPart

        SqlStr = "UPDATE DSP_PACKING_HDR SET " & vbCrLf _
            & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "', SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtShipTo.Text) & "'," & vbCrLf _
            & " INVOICE_NO='" & MainClass.AllowSingleQuote(mBillNo) & "', " & vbCrLf _
            & " INVOICE_DATE=TO_DATE('" & VB6.Format(txtInvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
            & " EXP_INV_MADE='Y' " & vbCrLf _
            & " WHERE AUTO_KEY_PACK =" & Val(txtPackNo.Text) & ""

        PubDBCn.Execute(SqlStr)



        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsExpMain.Requery() ''.Refresh
        RsExpDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        'If Err.Number = -2147217900 Then
        '    ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        'Else
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'End If
        ''Resume
    End Function

    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim j As Integer
        Dim mItemCode As String
        Dim mRate As Double
        Dim mRateINR As Double
        Dim mQty As Double
        Dim mAmount As Double
        Dim mAmountINR As Double
        Dim mTotQty As Double
        Dim mTotAmount As Double
        Dim mTotAmountINR As Double

        Dim mNetAmount As Double
        Dim mNetAmountINR As Double
        Dim mDiscountPer As Double
        Dim mDiscountAmount As Double

        Dim mOtherAmount As Double
        Dim mOtherAmountINR As Double

        mQty = 0
        mAmount = 0
        mAmountINR = 0
        mTotQty = 0
        mTotAmount = 0
        mTotAmountINR = 0

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If Trim(mItemCode) <> "" Then

                    .Col = ColQty
                    mQty = Val(.Text)

                    .Col = ColRate
                    mRate = Val(.Text)

                    SprdMain.Col = ColRateINR
                    If Val(txtCurrFactor.Text) = 0 Then
                        mRateINR = 0
                    Else
                        mRateINR = CDbl(VB6.Format(mRate * Val(txtCurrFactor.Text), "0.0000"))
                    End If
                    .Text = CStr(mRateINR)

                    .Col = ColAmount
                    mAmount = CDbl(VB6.Format(mQty * mRate, "0.00"))
                    .Text = VB6.Format(mAmount, "0.00")

                    .Col = ColAmountINR
                    mAmountINR = CDbl(VB6.Format(mQty * mRateINR, "0.00"))
                    .Text = VB6.Format(mAmountINR, "0.0000")

                    mTotQty = mTotQty + mQty
                    mTotAmount = mTotAmount + mAmount
                    mTotAmountINR = mTotAmountINR + mAmountINR

                    mQty = 0
                    mAmount = 0
                    mAmountINR = 0
                End If
            Next I
        End With

        mOtherAmount = Val(txtOtherAmt.Text)
        mOtherAmountINR = mOtherAmount * Val(txtCurrFactor.Text)

        mTotAmount = mTotAmount + mOtherAmount
        mTotAmountINR = mTotAmountINR + mOtherAmountINR

        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")
        mDiscountPer = Val(txtDiscPer.Text)
        mDiscountAmount = mTotAmount * mDiscountPer * 0.01

        mNetAmount = mTotAmount - (mTotAmount * Val(CStr(mDiscountPer)) * 0.01)
        mNetAmountINR = mTotAmountINR - (mTotAmountINR * Val(CStr(mDiscountPer)) * 0.01)

        txtDiscAmount.Text = VB6.Format(mDiscountAmount, "#0.00")
        lblTotAmount.Text = VB6.Format(mNetAmount, "#0.00")
        lblTotAmount_INR.Text = VB6.Format(mNetAmountINR, "#0.00")

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''    Resume
    End Sub

    Private Function CheckValidVDate(ByRef pDNNoSeq As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer
        CheckValidVDate = True

        If Val(txtInvNo.Text) = 1 Then Exit Function

        SqlStr = "SELECT MAX(EXPINV_DATE)" & vbCrLf & " FROM FIN_EXPINV_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BILLNOSEQ<" & Val(CStr(pDNNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(EXPINV_DATE)" & " FROM FIN_EXPINV_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BILLNOSEQ>" & Val(CStr(pDNNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtInvDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Invoice Date Is Greater Than The Invoice Date Of Next Invoice No.")
                CheckValidVDate = False
            ElseIf CDate(txtInvDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Invoice Date Is Less Than The Invoice Date Of Previous Invoice No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtInvDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Invoice Date Is Greater Than The Invoice Date Of Next Invoice No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtInvDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Invoice Date Is Less Than The Invoice Date Of Previous Invoice No.")
                CheckValidVDate = False
            End If
        End If

        Exit Function
CheckERR:
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsExpMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "SELECT Max(BILLNOSEQ)  " & vbCrLf & " FROM FIN_EXPINV_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsExpMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsExpMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqNo = .Fields(0).Value
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqNo = CStr(mNewSeqNo)
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateDetail1(ByRef pVnoseq As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mSubRowNo As Integer
        Dim mItemCode As String
        Dim mUnit As String
        Dim mPalletNo As Double
        Dim mPalletQty As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mRateINR As Double
        Dim mMarks As String
        Dim mSoNo As Double
        Dim mSODate As String
        Dim mBuyerPO As String
        Dim mBuyerDATE As String

        Dim mSize As String
        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim mGlassDescription As String

        Dim mActualHeight As Double
        Dim mActualWidth As Double

        Dim mChargeableHeight As Double
        Dim mChargeableWidth As Double
        Dim mGlassArea As Double

        mSubRowNo = 0

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColMarks
                mMarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColPalletNo
                mPalletNo = Val(.Text)

                .Col = ColPalletQty
                mPalletQty = Val(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColRateINR
                mRateINR = Val(.Text)

                .Col = ColSONo
                mSoNo = Val(.Text)

                .Col = ColSODate
                mSODate = Trim(.Text)

                .Col = ColBuyerPO
                mBuyerPO = Trim(.Text)

                .Col = ColBuyerDate
                mBuyerDATE = Trim(.Text)

                .Col = ColGlassDescription
                mGlassDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColActualHeight
                mActualHeight = Val(.Text)

                .Col = ColActualWidth
                mActualWidth = Val(.Text)

                .Col = ColSize
                mSize = MainClass.AllowSingleQuote(.Text)

                .Col = ColModelNo
                mModelNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColDrawingNo
                mDrawingNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColChargeableHeight
                mChargeableHeight = Val(.Text)

                .Col = ColChargeableWidth
                mChargeableWidth = Val(.Text)

                .Col = ColGlassArea
                mGlassArea = Val(.Text)

                SqlStr = ""

                If mItemCode <> "" And mQty > 0 Then
                    mSubRowNo = mSubRowNo + 1
                    SqlStr = " INSERT INTO FIN_EXPINV_DET ( " & vbCrLf _
                        & " COMPANY_CODE, AUTO_KEY_EXPINV, " & vbCrLf _
                        & " SERIAL_NO, ITEM_CODE," & vbCrLf _
                        & " ITEM_UOM, MARKS, " & vbCrLf _
                        & " PALLETNO, PALLETQTY, " & vbCrLf _
                        & " PACKED_QTY, " & vbCrLf _
                        & " RATE, RATE_INR," & vbCrLf _
                        & " AUTO_KEY_SO,SO_DATE,CUST_PO_NO,CUST_PO_DATE, " & vbCrLf _
                        & " GLASS_DESC, ACTUAL_HEIGHT, ACTUAL_WIDTH," & vbCrLf _
                        & " ITEM_SIZE, ITEM_MODEL, ITEM_DRAWINGNO, CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, GLASS_AREA)" & vbCrLf

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(pVnoseq)) & ", " & vbCrLf _
                        & " " & mSubRowNo & ", '" & mItemCode & "', " & vbCrLf _
                        & " '" & mUnit & "', '" & mMarks & "'," & vbCrLf _
                        & " " & mPalletNo & ", " & mPalletQty & "," & vbCrLf _
                        & " " & mQty & ", " & vbCrLf _
                        & " " & mRate & ", " & mRateINR & "," & vbCrLf _
                        & " " & Val(CStr(mSoNo)) & ", TO_DATE('" & VB6.Format(mSODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mBuyerPO) & "', TO_DATE('" & VB6.Format(mBuyerDATE, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & mGlassDescription & "', " & mActualHeight & ", " & mActualWidth & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSize) & "', '" & MainClass.AllowSingleQuote(mModelNo) & "', '" & MainClass.AllowSingleQuote(mDrawingNo) & "'," & vbCrLf _
                        & " " & mChargeableHeight & ", " & mChargeableWidth & ", " & mGlassArea & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        Dim mOthersValue As String = ""
        Dim mOthersName As String = ""
        With SprdOther
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColOthersName
                mOthersName = MainClass.AllowSingleQuote(.Text)

                .Col = ColOthersValue
                mOthersValue = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mOthersName) <> "" Then
                    SqlStr = " INSERT INTO FIN_EXPORT_PARA_EXP ( " & vbCrLf _
                    & " AUTO_KEY_EXPINV, SERAIL_NO, FIELD_NAME, FIELD_VALUE ) " & vbCrLf _
                    & " VALUES ( " & Val(pVnoseq) & ", " & I & ", '" & mOthersName & "', '" & mOthersValue & "' )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub FillSprdOther()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim RS As ADODB.Recordset = Nothing

        MainClass.ClearGrid(SprdOther)
        FormatSprdOther(-1)
        I = 1
        SqlStr = "Select COUNT(1) AS CNTROW From FIN_EXPORT_PARA_MST"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            I = If(IsDBNull(RS.Fields("CNTROW").Value), 1, RS.Fields("CNTROW").Value)
        End If
        RS.Close()
        SprdOther.VisibleCols = 3
        SprdOther.VisibleRows = I
        SprdOther.MaxRows = I

        FormatSprdOther(-1)

        SqlStr = "Select * From FIN_EXPORT_PARA_MST ORDER BY SERAIL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            I = 0
            Do While Not RS.EOF
                I = I + 1
                SprdOther.Row = I

                SprdOther.Col = ColOthersName
                SprdOther.Text = If(IsDBNull(RS.Fields("FIELD_NAME").Value), "", RS.Fields("FIELD_NAME").Value)

                SprdOther.Col = ColOthersDesc
                SprdOther.Text = If(IsDBNull(RS.Fields("FIELD_CAPTION").Value), "", RS.Fields("FIELD_CAPTION").Value)

                RS.MoveNext()
                'If RS.EOF = False Then
                '    SprdOther.MaxRows = SprdOther.MaxRows + 1
                'End If
            Loop
        End If

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsExpExp.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdOther(ByRef Arow As Integer)

        On Error GoTo ERR1


        With SprdOther
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 2)


            .Col = ColOthersName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("FIELD_NAME", "FIN_EXPORT_PARA_MST", PubDBCn)
            .set_ColWidth(ColOthersName, 6)
            .ColHidden = True

            .Col = ColOthersDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("FIELD_CAPTION", "FIN_EXPORT_PARA_MST", PubDBCn)
            .ColsFrozen = ColOthersDesc
            .set_ColWidth(ColOthersDesc, 30)
            .ColHidden = False

            .Col = ColOthersValue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsExpExp.Fields("FIELD_VALUE").DefinedSize
            .set_ColWidth(ColOthersValue, 40)
            .ColHidden = False

        End With

        MainClass.ProtectCell(SprdOther, 1, SprdOther.MaxRows, ColOthersName, ColOthersDesc)

        MainClass.SetSpreadColor(SprdOther, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsExpExp.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mExciseDutyAmt As Double
        Dim mSalesTaxAmount As Double
        Dim mWithInState As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCancelled As String

        FieldsVarification = True
        If ValidateBranchLocking((txtPackDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtPackDate.Text, (TxtCustomerName.Text), mCustomerCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsExpMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtPackNo.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtPackDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPackDate.Focus()
            Exit Function
        ElseIf FYChk((txtPackDate.Text)) = False Then
            FieldsVarification = False
            If txtPackDate.Enabled = True Then txtPackDate.Focus()
            Exit Function
        End If

        If Trim(TxtCustomerName.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' TxtCustomerName.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtCurrFactor.Text) = 0 Then
            MsgBox("Currenct Factor Cannot Be Zero", MsgBoxStyle.Information)
            txtCurrFactor.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True And PubSuperUser <> "S" Then
            If chkDC.CheckState = System.Windows.Forms.CheckState.Checked Or chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Transaction Made Against This Invoice So Cann't be Changed.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = True And chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("You cann't be cancelled Invoice, when you ADD New Entry.")
            FieldsVarification = False
            Exit Function
        Else
            If chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Checked Then

                SqlStr = " SELECT CANCELLED" & vbCrLf & " FROM  FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCustomerCode.Text)) & "'" & vbCrLf & " AND BILLNO='" & "S" & txtExciseBillNo.Text & "' AND INVOICE_DATE=TO_DATE('" & VB6.Format(txtExciseBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                mCancelled = "N"
                If RsTemp.EOF = False Then
                    mCancelled = IIf(IsDBNull(RsTemp.Fields("Cancelled").Value), "N", RsTemp.Fields("Cancelled").Value)
                End If

                If mCancelled = "N" Then
                    MsgInformation("Excise Invoice Made Against This Invoice. Please Cancelled Excise Invoice First.")
                    FieldsVarification = False
                    Exit Function
                End If

            End If

        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColRateINR, "N", "Please Check Rate.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmExportInvoice_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Export Invoice"

        SqlStr = ""
        SqlStr = "Select * from FIN_EXPINV_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsExpMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_EXPINV_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsExpDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from FIN_EXPORT_PARA_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsExpExp, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mCustomerCode = CStr(-1)


        cmdAdd.Visible = True
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

        MainClass.ClearGrid(SprdView)

        SqlStr = "Select BILLNOSEQ AS REF_NO, BILLNO, " & vbCrLf _
            & " TO_CHAR(DC.PACK_DATE,'DD/MM/YYYY') AS REF_DATE, " & vbCrLf _
            & " AC.SUPP_CUST_NAME AS CustomerName, TOTQTY, NETVALUE,NETVALUE_INR,CURR_DESC,CON_FACTOR " & vbCrLf _
            & " FROM FIN_EXPINV_HDR DC,FIN_SUPP_CUST_MST AC " & vbCrLf _
            & " WHERE " & vbCrLf & " DC.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DC.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND DC.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf _
            & " AND DC.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf _
            & " Order by BILLNO DESC"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Sub
    Private Function MakeSQL(pType As String) As Object
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = ""

        If pType = "E" Then
            MakeSQL = "SELECT IH.*, ID.*, CMST.*, INVMST.*, BMST.*, GMST.* " '& vbCrLf |
            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH, FIN_EXPINV_DET ID, " & vbCrLf _
                & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST "

            MakeSQL = MakeSQL & vbCrLf _
                & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_EXPINV=ID.AUTO_KEY_EXPINV " & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.SHIP_TO_LOC_ID=BMST.LOCATION_ID " & vbCrLf _
                & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                & " AND IH.AUTO_KEY_EXPINV=" & Val(LblMkey.Text) & ""

            MakeSQL = MakeSQL & vbCrLf & " ORDER BY ID.SERIAL_NO"
        Else
            MakeSQL = "SELECT IH.*, ID.*, CMST.*, INVMST.*, BMST.*, GMST.* " '& vbCrLf |
            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_EXPINV_HDR IH, FIN_EXPINV_DET ID, DSP_PACKING_DET PD," & vbCrLf _
                & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BMST, INV_ITEM_MST INVMST, GEN_COMPANY_MST GMST "

            MakeSQL = MakeSQL & vbCrLf _
                & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_EXPINV=ID.AUTO_KEY_EXPINV " & vbCrLf _
                & " AND IH.COMPANY_CODE=PD.COMPANY_CODE " & vbCrLf _
                & " AND IH.AUTO_KEY_PACK=PD.AUTO_KEY_PACK " & vbCrLf _
                & " AND ID.SERIAL_NO=PD.SERIAL_NO " & vbCrLf _
                & " AND ID.ITEM_CODE=PD.ITEM_CODE " & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE " & vbCrLf _
                & " AND IH.SHIP_TO_LOC_ID=BMST.LOCATION_ID " & vbCrLf _
                & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                & " AND IH.AUTO_KEY_EXPINV=" & Val(LblMkey.Text) & ""

            MakeSQL = MakeSQL & vbCrLf & " ORDER BY ID.SERIAL_NO"
        End If
        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
    End Function
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)

            .set_ColWidth(1, 1000)
            .ColHidden = True

            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 3500)
            .set_ColWidth(5, 1200)
            .set_ColWidth(6, 1500)
            .set_ColWidth(7, 1500)
            .set_ColWidth(8, 1200)
            .set_ColWidth(9, 1200)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsExpDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .ColsFrozen = ColItemDesc
            .set_ColWidth(ColItemDesc, 20)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColPartNo, 12)
            .ColHidden = True

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsExpDetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

            .Col = ColMarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsExpDetail.Fields("MARKS").DefinedSize ''
            .set_ColWidth(ColMarks, 4)
            .ColHidden = True

            For I = ColPalletNo To ColPalletQty
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 0
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 7)
            Next

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(I, 9)

            For I = ColRate To ColAmountINR
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.9999")
                .TypeFloatMin = CDbl("-999999999.9999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 10)
            Next

            .Col = ColSONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeEditLen = RsExpDetail.Fields("AUTO_KEY_SO").Precision
            .set_ColWidth(ColSONo, 7)
            .ColHidden = True

            .Col = ColSODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''
            .set_ColWidth(ColSODate, 6)
            .ColHidden = True

            .Col = ColBuyerPO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsExpDetail.Fields("CUST_PO_NO").DefinedSize ''
            .set_ColWidth(ColBuyerPO, 6)
            .ColHidden = True

            .Col = ColBuyerDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''
            .set_ColWidth(ColBuyerDate, 6)
            .ColHidden = True

            .Col = ColSize
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsExpDetail.Fields("ITEM_SIZE").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColModelNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsExpDetail.Fields("ITEM_MODEL").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColDrawingNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsExpDetail.Fields("ITEM_DRAWINGNO").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 12)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)



            .Col = ColGlassDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsExpDetail.Fields("GLASS_DESC").DefinedSize ''				
            .set_ColWidth(.Col, 20)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            For cntCol = ColActualWidth To ColActualHeight
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

            For cntCol = ColChargeableWidth To ColGlassArea
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next
        End With


        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColAmountINR)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRateINR, ColRateINR)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColAmountINR)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSONo, ColBuyerDate)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsExpDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsExpMain
            txtInvNo.MaxLength = .Fields("BILLNOSEQ").Precision
            txtInvDate.MaxLength = 10

            txtPackNo.MaxLength = .Fields("AUTO_KEY_PACK").Precision
            txtPackDate.MaxLength = 10
            TxtCustomerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtCustomerCode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtBuyerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtExciseBillNo.MaxLength = .Fields("EXCISE_INV_NO").DefinedSize
            txtExciseBillDate.MaxLength = 10
            txtBuyerNo.MaxLength = .Fields("BUYER_PO").DefinedSize
            txtBuyerDate.MaxLength = 10
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
            txtIECNo.MaxLength = .Fields("REF_NO").DefinedSize
            txtOrigin.MaxLength = .Fields("ORIGIN_COUNTRY").DefinedSize
            txtDestination.MaxLength = .Fields("DEST_COUNTRY").DefinedSize
            txtCarriage.MaxLength = .Fields("CARRIAGE").DefinedSize
            txtPlace.MaxLength = .Fields("RECIPT_PLACE").DefinedSize
            txtFlight.MaxLength = .Fields("FLIGHT_NO").DefinedSize
            txtLoading.MaxLength = .Fields("LOADINGPORT").DefinedSize
            txtDischarge.MaxLength = .Fields("DISCHARGEPORT").DefinedSize
            txtFinalDestination.MaxLength = .Fields("FINALDEST").DefinedSize
            txtPayments.MaxLength = .Fields("PAYMENTTERMS").DefinedSize
            txtContainerNo.MaxLength = .Fields("CONTAINERNO").DefinedSize

            txtInvPrefix.MaxLength = .Fields("BILLNOPREFIX").DefinedSize
            txtCurrFactor.MaxLength = .Fields("CON_FACTOR").Precision
            txtCurrency.MaxLength = .Fields("CURR_DESC").DefinedSize
            txtAgreement.MaxLength = .Fields("AGREEMENT").DefinedSize

            txtCreditBank.MaxLength = .Fields("CREDITBANK").DefinedSize
            txtCreditBankAddress.MaxLength = .Fields("CREDITBANK_ADD").DefinedSize
            txtCustomerBank.MaxLength = .Fields("CUST_BANK").DefinedSize
            txtAccountNo.MaxLength = .Fields("CUST_ACCTNO").DefinedSize
            txtSwiftCode.MaxLength = .Fields("SWIFT_CODE").DefinedSize
            txtADCode.MaxLength = .Fields("AD_CODE").DefinedSize
            txtFurtherBank.MaxLength = .Fields("FURTHER_BANK").DefinedSize

            txtAdvLicNo.Text = CStr(.Fields("ADV_LIC_NO").DefinedSize)
            txtAdvLicDate.Text = CStr(10)

            txtNotifyParty1.MaxLength = .Fields("NOTIFY_PARTY_1").DefinedSize
            txtNotifyParty2.MaxLength = .Fields("NOTIFY_PARTY_2").DefinedSize
            txtNotifyParty3.MaxLength = .Fields("NOTIFY_PARTY_3").DefinedSize

            txtOtherAmt.MaxLength = .Fields("OTHER_AMOUNT").Precision
            '        txtDiscPer.Text = Format(IIf(IsNull(!DESC_PER), "", !DESC_PER), "0.000")
            '        txtDiscAmount.Text = Format(IIf(IsNull(!DISC_AMOUNT), "", !DISC_AMOUNT), "0.000")

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mBuyerCode As String

        With RsExpMain
            If Not .EOF Then
                LblMkey.Text = .Fields("AUTO_KEY_EXPINV").Value
                txtInvPrefix.Text = IIf(IsDBNull(.Fields("BILLNOPREFIX").Value), "", .Fields("BILLNOPREFIX").Value)

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then
                    txtInvNo.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value), ConBillFormat)
                Else
                    txtInvNo.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value), "000000")
                End If

                txtInvDate.Text = VB6.Format(IIf(IsDBNull(.Fields("EXPINV_DATE").Value), "", .Fields("EXPINV_DATE").Value), "DD/MM/YYYY")

                txtPackNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_PACK").Value), "", .Fields("AUTO_KEY_PACK").Value)
                txtPackDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PACK_DATE").Value), "", .Fields("PACK_DATE").Value), "DD/MM/YYYY")

                mCustomerCode = .Fields("SUPP_CUST_CODE").Value
                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtCustomerName.Text = MasterNo
                End If


                txtCustomerCode.Text = Trim(mCustomerCode)



                mBuyerCode = IIf(IsDBNull(.Fields("BUYER_CODE").Value), "", .Fields("BUYER_CODE").Value)
                If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBuyerName.Text = MasterNo
                Else
                    If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "BUYERCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mBuyerCode = MasterNo
                        If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            txtBuyerName.Text = MasterNo
                        End If
                    End If
                End If



                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                txtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)


                txtExciseBillNo.Text = IIf(IsDBNull(.Fields("EXCISE_INV_NO").Value), "", .Fields("EXCISE_INV_NO").Value)
                txtExciseBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("EXCISE_INV_DATE").Value), "", .Fields("EXCISE_INV_DATE").Value), "DD/MM/YYYY")
                txtBuyerNo.Text = IIf(IsDBNull(.Fields("BUYER_PO").Value), "", .Fields("BUYER_PO").Value)
                txtBuyerDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BUYER_PO_DATE").Value), "", .Fields("BUYER_PO_DATE").Value), "DD/MM/YYYY")
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtIECNo.Text = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtOrigin.Text = IIf(IsDBNull(.Fields("ORIGIN_COUNTRY").Value), "", .Fields("ORIGIN_COUNTRY").Value)
                txtDestination.Text = IIf(IsDBNull(.Fields("DEST_COUNTRY").Value), "", .Fields("DEST_COUNTRY").Value)
                txtCarriage.Text = IIf(IsDBNull(.Fields("CARRIAGE").Value), "", .Fields("CARRIAGE").Value)
                txtPlace.Text = IIf(IsDBNull(.Fields("RECIPT_PLACE").Value), "", .Fields("RECIPT_PLACE").Value)
                txtFlight.Text = IIf(IsDBNull(.Fields("FLIGHT_NO").Value), "", .Fields("FLIGHT_NO").Value)
                txtLoading.Text = IIf(IsDBNull(.Fields("LOADINGPORT").Value), "", .Fields("LOADINGPORT").Value)
                txtDischarge.Text = IIf(IsDBNull(.Fields("DISCHARGEPORT").Value), "", .Fields("DISCHARGEPORT").Value)
                txtFinalDestination.Text = IIf(IsDBNull(.Fields("FINALDEST").Value), "", .Fields("FINALDEST").Value)
                txtPayments.Text = IIf(IsDBNull(.Fields("PAYMENTTERMS").Value), "", .Fields("PAYMENTTERMS").Value)
                txtContainerNo.Text = IIf(IsDBNull(.Fields("CONTAINERNO").Value), "", .Fields("CONTAINERNO").Value)

                txtCurrFactor.Text = IIf(IsDBNull(.Fields("CON_FACTOR").Value), "", .Fields("CON_FACTOR").Value)
                txtCurrency.Text = IIf(IsDBNull(.Fields("CURR_DESC").Value), "", .Fields("CURR_DESC").Value)
                txtAgreement.Text = IIf(IsDBNull(.Fields("AGREEMENT").Value), "", .Fields("AGREEMENT").Value)
                txtCreditBank.Text = IIf(IsDBNull(.Fields("CREDITBANK").Value), "", .Fields("CREDITBANK").Value)
                txtCreditBankAddress.Text = IIf(IsDBNull(.Fields("CREDITBANK_ADD").Value), "", .Fields("CREDITBANK_ADD").Value)

                txtCustomerBank.Text = IIf(IsDBNull(.Fields("CUST_BANK").Value), "", .Fields("CUST_BANK").Value)
                txtAccountNo.Text = IIf(IsDBNull(.Fields("CUST_ACCTNO").Value), "", .Fields("CUST_ACCTNO").Value)
                txtSwiftCode.Text = IIf(IsDBNull(.Fields("SWIFT_CODE").Value), "", .Fields("SWIFT_CODE").Value)
                txtADCode.Text = IIf(IsDBNull(.Fields("AD_CODE").Value), "", .Fields("AD_CODE").Value)
                txtFurtherBank.Text = IIf(IsDBNull(.Fields("FURTHER_BANK").Value), "", .Fields("FURTHER_BANK").Value)

                lblTotQty.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTQTY").Value), "", .Fields("TOTQTY").Value), "0.000")
                txtDiscPer.Text = VB6.Format(IIf(IsDBNull(.Fields("DESC_PER").Value), "", .Fields("DESC_PER").Value), "0.00")
                txtDiscAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("DISC_AMOUNT").Value), "", .Fields("DISC_AMOUNT").Value), "0.00")

                txtOtherAmt.Text = VB6.Format(IIf(IsDBNull(.Fields("OTHER_AMOUNT").Value), "", .Fields("OTHER_AMOUNT").Value), "0.00")

                lblTotAmount_INR.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE_INR").Value), "", .Fields("NETVALUE_INR").Value), "0.00")
                lblTotAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE").Value), "", .Fields("NETVALUE").Value), "0.00")

                chkDC.CheckState = IIf(.Fields("DC_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkExciseInvoice.CheckState = IIf(.Fields("Excise_INV_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.CheckState = IIf(.Fields("Cancelled").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.Enabled = IIf(.Fields("Cancelled").Value = "Y", False, True)

                chkREXDeclaration.CheckState = IIf(.Fields("IS_REXDeclaration").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                txtAdvLicNo.Text = IIf(IsDBNull(.Fields("ADV_LIC_NO").Value), "", .Fields("ADV_LIC_NO").Value)
                txtAdvLicDate.Text = IIf(IsDBNull(.Fields("ADV_LIC_DATE").Value), "", .Fields("ADV_LIC_DATE").Value)

                txtNotifyParty1.Text = IIf(IsDBNull(.Fields("NOTIFY_PARTY_1").Value), "", .Fields("NOTIFY_PARTY_1").Value)
                txtNotifyParty2.Text = IIf(IsDBNull(.Fields("NOTIFY_PARTY_2").Value), "", .Fields("NOTIFY_PARTY_2").Value)
                txtNotifyParty3.Text = IIf(IsDBNull(.Fields("NOTIFY_PARTY_3").Value), "", .Fields("NOTIFY_PARTY_3").Value)


                txtConsigneeAddress.Text = FillAddressDetail(txtCustomerCode.Text, txtShipTo.Text)
                txtBuyerAddress.Text = FillAddressDetail(mBuyerCode, txtBillTo.Text)


                Call ShowDetail1((LblMkey.Text))
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsExpMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        '    SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtPackNo.Enabled = False
        cmdPackNo.Enabled = False
        SSTInfo.SelectedIndex = 0
        Me.Cursor = System.Windows.Forms.Cursors.Default

        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowPackMain(ByRef pRsPackMain As ADODB.Recordset)
        On Error GoTo ERR1
        Dim mBuyerCode As String

        With pRsPackMain
            If Not .EOF Then

                If .Fields("EXp_INV_MADE").Value = "Y" Then
                    MsgInformation("Invoice already made against Such Packing Note.")
                    Exit Sub
                End If
                LblMkey.Text = "-1"

                txtPackNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_PACK").Value), "", .Fields("AUTO_KEY_PACK").Value)
                txtPackDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PACK_DATE").Value), "", .Fields("PACK_DATE").Value), "DD/MM/YYYY")

                mCustomerCode = .Fields("SUPP_CUST_CODE").Value
                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtCustomerName.Text = MasterNo
                End If


                txtCustomerCode.Text = Trim(mCustomerCode)



                mBuyerCode = IIf(IsDBNull(.Fields("BUYER_CODE").Value), "", .Fields("BUYER_CODE").Value)
                If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBuyerName.Text = MasterNo
                Else
                    If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "BUYERCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mBuyerCode = MasterNo
                        If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            txtBuyerName.Text = MasterNo
                        End If
                    End If
                End If

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                txtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)



                If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "CURRENCYNAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCurrency.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable((txtCurrency.Text), "CURR_DESC", "CON_FACTOR", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCurrFactor.Text = VB6.Format(MasterNo, "0.00")
                End If


                txtBuyerNo.Text = IIf(IsDBNull(.Fields("BUYER_PO").Value), "", .Fields("BUYER_PO").Value)
                txtBuyerDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BUYER_PO_DATE").Value), "", .Fields("BUYER_PO_DATE").Value), "DD/MM/YYYY")
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtIECNo.Text = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtOrigin.Text = IIf(IsDBNull(.Fields("ORIGIN_COUNTRY").Value), "", .Fields("ORIGIN_COUNTRY").Value)
                txtDestination.Text = IIf(IsDBNull(.Fields("DEST_COUNTRY").Value), "", .Fields("DEST_COUNTRY").Value)
                txtCarriage.Text = IIf(IsDBNull(.Fields("CARRIAGE").Value), "", .Fields("CARRIAGE").Value)
                txtPlace.Text = IIf(IsDBNull(.Fields("RECIPT_PLACE").Value), "", .Fields("RECIPT_PLACE").Value)
                txtFlight.Text = IIf(IsDBNull(.Fields("FLIGHT_NO").Value), "", .Fields("FLIGHT_NO").Value)
                txtLoading.Text = IIf(IsDBNull(.Fields("LOADINGPORT").Value), "", .Fields("LOADINGPORT").Value)
                txtDischarge.Text = IIf(IsDBNull(.Fields("DISCHARGEPORT").Value), "", .Fields("DISCHARGEPORT").Value)
                txtFinalDestination.Text = IIf(IsDBNull(.Fields("FINALDEST").Value), "", .Fields("FINALDEST").Value)
                txtPayments.Text = IIf(IsDBNull(.Fields("PAYMENTTERMS").Value), "", .Fields("PAYMENTTERMS").Value)
                txtContainerNo.Text = IIf(IsDBNull(.Fields("CONTAINERNO").Value), "", .Fields("CONTAINERNO").Value)

                txtConsigneeAddress.Text = FillAddressDetail(txtCustomerCode.Text, txtShipTo.Text)
                txtBuyerAddress.Text = FillAddressDetail(mBuyerCode, txtBillTo.Text)

                '            txtAdvLicNo.Text = IIf(IsNull(!ADV_LIC_NO), "", !ADV_LIC_NO)
                '            txtAdvLicDate.Text = IIf(IsNull(!ADV_LIC_DATE), "", !ADV_LIC_DATE)

                Call ShowPackDetail(Val(txtPackNo.Text))
            End If
        End With
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Function FillAddressDetail(ByVal pCode As String, ByVal mLocation As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xBuyerCode As String
        Dim xBuyerAddress As String

        If pCode = "" Then Exit Function
        If mLocation = "" Then Exit Function
        FillAddressDetail = ""

        SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE" & vbCrLf _
                    & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCode) & "'" & vbCrLf _
                    & " AND LOCATION_ID='" & MainClass.AllowSingleQuote(mLocation) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            With RsTemp

                xBuyerAddress = IIf(IsDBNull(.Fields("SUPP_CUST_ADDR").Value), "", .Fields("SUPP_CUST_ADDR").Value)
                xBuyerAddress = xBuyerAddress & ", " & IIf(IsDBNull(.Fields("SUPP_CUST_CITY").Value), "", .Fields("SUPP_CUST_CITY").Value)
                xBuyerAddress = xBuyerAddress & ", " & IIf(IsDBNull(.Fields("SUPP_CUST_STATE").Value), "", .Fields("SUPP_CUST_STATE").Value)

                FillAddressDetail = xBuyerAddress
            End With
        End If
        Exit Function

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ShowDetail1(ByRef mMKey As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String

        SSTInfo.SelectedIndex = 0
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM FIN_EXPINV_DET " & vbCrLf & " Where AUTO_KEY_EXPINV=" & Val(mMKey) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsExpDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsExpDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = Trim(mItemDesc)

                SprdMain.Col = ColPartNo
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mPartNo = MasterNo
                SprdMain.Text = Trim(mPartNo)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColMarks
                SprdMain.Text = IIf(IsDBNull(.Fields("MARKS").Value), "", .Fields("MARKS").Value)

                SprdMain.Col = ColPalletNo
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PalletNo").Value), 0, .Fields("PalletNo").Value), "0.00")

                SprdMain.Col = ColPalletQty
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PALLETQTY").Value), 0, .Fields("PALLETQTY").Value), "0.00")

                SprdMain.Col = ColQty
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PACKED_QTY").Value), 0, .Fields("PACKED_QTY").Value), "0.000")

                SprdMain.Col = ColRate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("Rate").Value), 0, .Fields("Rate").Value), "0.0000")

                SprdMain.Col = ColRateINR
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("RATE_INR").Value), 0, .Fields("RATE_INR").Value), "0.0000")

                SprdMain.Col = ColSONo
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("AUTO_KEY_SO").Value), 0, .Fields("AUTO_KEY_SO").Value)))
                '            mSoNo = Val(IIf(IsNull(!AUTO_KEY_SO), 0, !AUTO_KEY_SO))

                SprdMain.Col = ColSODate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SO_DATE").Value), "", .Fields("SO_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColBuyerPO
                SprdMain.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)

                SprdMain.Col = ColBuyerDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColSize
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SIZE").Value), "", .Fields("ITEM_SIZE").Value))

                SprdMain.Col = ColModelNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value))
                'mModel = Trim(IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value))

                SprdMain.Col = ColDrawingNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value))

                SprdMain.Col = ColGlassDescription
                SprdMain.Text = IIf(IsDBNull(.Fields("GLASS_DESC").Value), "", .Fields("GLASS_DESC").Value)

                SprdMain.Col = ColActualHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)))
                'mHeight = Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value))

                SprdMain.Col = ColActualWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)))
                'mWidth = Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value))

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))

                SprdMain.Col = ColGlassArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_AREA").Value), 0, .Fields("GLASS_AREA").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Call CalcTots()

        FormatSprdMain(-1)

        SSTInfo.SelectedIndex = 2
        FillSprdOther()


        SqlStr = " SELECT *  FROM FIN_EXPORT_PARA_EXP Where AUTO_KEY_EXPINV=" & Val(mMKey) & " Order By SERAIL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsExpExp, ADODB.LockTypeEnum.adLockReadOnly)

        Dim mOthersName As String
        Dim mOthersValue As String
        Dim RsTemp As ADODB.Recordset = Nothing

        With SprdOther
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColOthersName
                mOthersName = Trim(.Text)

                SqlStr = " SELECT *  FROM FIN_EXPORT_PARA_EXP Where AUTO_KEY_EXPINV=" & Val(mMKey) & " AND FIELD_NAME='" & MainClass.AllowSingleQuote(mOthersName) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                mOthersValue = ""
                If RsTemp.EOF = False Then
                    mOthersValue = If(IsDBNull(RsTemp.Fields("FIELD_VALUE").Value), "", RsTemp.Fields("FIELD_VALUE").Value)
                End If

                .Col = ColOthersValue
                .Text = Trim(mOthersValue)
                FormatSprdOther(I)
            Next
        End With

        FormatSprdOther(-1)



        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ShowPackDetail(ByRef mMKey As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim RsPackDetail As ADODB.Recordset = Nothing
        Dim RsPackexp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mRateINR As Double
        Dim mRate As Double
        Dim mSoNo As Double
        Dim mWidth As Double
        Dim mHeight As Double
        Dim mModel As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM DSP_PACKING_DET " & vbCrLf _
            & " Where AUTO_KEY_PACK=" & Val(CStr(mMKey)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPackDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = Trim(mItemDesc)

                SprdMain.Col = ColPartNo
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mPartNo = MasterNo
                SprdMain.Text = Trim(mPartNo)

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColMarks
                SprdMain.Text = IIf(IsDBNull(.Fields("MARKS").Value), "", .Fields("MARKS").Value)

                SprdMain.Col = ColPalletNo
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PalletNo").Value), 0, .Fields("PalletNo").Value), "0.00")

                SprdMain.Col = ColPalletQty
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("NO_OF_PACKETS").Value), 0, .Fields("NO_OF_PACKETS").Value), "0.00")

                SprdMain.Col = ColQty
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("PACKED_QTY").Value), 0, .Fields("PACKED_QTY").Value), "0.000")

                SprdMain.Col = ColSONo
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("AUTO_KEY_SO").Value), 0, .Fields("AUTO_KEY_SO").Value)))
                mSoNo = IIf(IsDBNull(.Fields("AUTO_KEY_SO").Value), 0, .Fields("AUTO_KEY_SO").Value)

                SprdMain.Col = ColSODate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("SO_DATE").Value), "", .Fields("SO_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColBuyerPO
                SprdMain.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)

                SprdMain.Col = ColBuyerDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColSize
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SIZE").Value), "", .Fields("ITEM_SIZE").Value))

                SprdMain.Col = ColModelNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value))
                mModel = Trim(IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value))

                SprdMain.Col = ColDrawingNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value))

                SprdMain.Col = ColGlassDescription
                SprdMain.Text = IIf(IsDBNull(.Fields("GLASS_DESC").Value), "", .Fields("GLASS_DESC").Value)

                SprdMain.Col = ColActualHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)))
                mHeight = Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value))

                SprdMain.Col = ColActualWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)))
                mWidth = Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value))

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))

                SprdMain.Col = ColGlassArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_AREA").Value), 0, .Fields("GLASS_AREA").Value)))

                SprdMain.Col = ColRate
                mRate = GetSORate(mItemCode, mSoNo, mHeight, mWidth, mModel)
                SprdMain.Text = VB6.Format(mRate, "0.0000")

                SprdMain.Col = ColRateINR
                If Val(txtCurrFactor.Text) = 0 Then
                    mRateINR = 0
                Else
                    mRateINR = mRate * Val(txtCurrFactor.Text)
                End If
                SprdMain.Text = VB6.Format(mRateINR, "0.0000")

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With


        SqlStr = " SELECT *  FROM FIN_PACKING_PARA_EXP Where AUTO_KEY_PACK=" & Val(mMKey) & " Order By SERAIL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPackexp, ADODB.LockTypeEnum.adLockReadOnly)

        Dim mOthersName As String
        Dim mOthersValue As String
        Dim RsTemp As ADODB.Recordset = Nothing

        With SprdOther
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColOthersName
                mOthersName = Trim(.Text)

                SqlStr = " SELECT *  FROM FIN_PACKING_PARA_EXP Where AUTO_KEY_PACK=" & Val(mMKey) & " AND FIELD_NAME='" & MainClass.AllowSingleQuote(mOthersName) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                mOthersValue = ""
                If RsTemp.EOF = False Then
                    mOthersValue = If(IsDBNull(RsTemp.Fields("FIELD_VALUE").Value), "", RsTemp.Fields("FIELD_VALUE").Value)
                End If

                .Col = ColOthersValue
                .Text = mOthersValue

            Next
        End With

        Call CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Function GetSORate(ByRef pItemCode As String, ByVal mSoNo As Double, ByRef mHeight As Double, ByRef mWidth As Double, ByRef mModelNo As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCustomerCode As String

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xCustomerCode = MasterNo
        Else
            xCustomerCode = "-1"
        End If

        SqlStr = "SELECT ITEM_PRICE" & vbCrLf _
                & " FROM  DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.MKEY=ID.MKEY AND IH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                & " AND IH.AUTO_KEY_SO=" & Val(mSoNo) & " AND SO_APPROVED='Y'" & vbCrLf _
                & " AND IH.MKEY = ("

        SqlStr = SqlStr & "SELECT MAX(SIH.MKEY) FROM  DSP_SALEORDER_HDR SIH, DSP_SALEORDER_DET SID" & vbCrLf _
                & " WHERE SIH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SIH.MKEY=SID.MKEY AND SIH.SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
                & " AND SID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
                & " AND SIH.AUTO_KEY_SO=" & Val(mSoNo) & " AND SO_APPROVED='Y'" & vbCrLf _
                & " AND SID.AMEND_WEF <=TO_DATE('" & VB6.Format(txtInvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If mModelNo = "" Then
                SqlStr = SqlStr & " And ACTUAL_HEIGHT=" & mHeight & " And ACTUAL_WIDTH=" & mWidth & ""
            Else
                SqlStr = SqlStr & " And ITEM_MODEL='" & MainClass.AllowSingleQuote(mModelNo) & "'"
            End If
        End If

        SqlStr = SqlStr & ")"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If mModelNo <> "" Then
                SqlStr = SqlStr & " And ITEM_MODEL='" & MainClass.AllowSingleQuote(mModelNo) & "'"
            End If

            If Val(mHeight) > 0 Then
                SqlStr = SqlStr & " And ACTUAL_HEIGHT=" & mHeight & ""
            End If

            If Val(mWidth) > 0 Then
                SqlStr = SqlStr & " And ACTUAL_WIDTH=" & mWidth & ""
            End If

        End If

        '        If Val(lblPoNo.text) <> "-1" And Val(lblPoNo.text) <> "0" Then
        '            SqlStr = "SELECT ITEM_PRICE FROM DSP_SALEORDER_DET " & vbCrLf _
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                    & " And SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf _
        ''                    & " AND ITEM_CODE='" & pItemCode & "'" & vbCrLf _
        ''                    & " AND AUTO_KEY_SO=" & Val(lblPoNo.text) & ""
        '        Else
        'SqlStr = "SELECT ITEM_RATE_F AS ITEM_PRICE FROM FIN_SUPP_CUST_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xCustomerCode & "'" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "'"
        '        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSORate = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value))
        Else
            GetSORate = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSORate = 0
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdataItem.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsExpMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        LblMkey.Text = ""

        mCustomerCode = CStr(-1)
        txtPackNo.Text = ""
        txtPackDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        TxtCustomerName.Text = ""
        txtCustomerCode.Text = ""
        txtBuyerName.Text = ""
        txtInvNo.Text = ""
        txtInvDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtExciseBillNo.Text = ""
        txtExciseBillDate.Text = ""
        txtBuyerNo.Text = ""
        txtBuyerDate.Text = ""
        txtRemarks.Text = ""
        txtIECNo.Text = IIf(IsDBNull(RsCompany.Fields("IEC_NO").Value), "", RsCompany.Fields("IEC_NO").Value)
        txtIECNo.Enabled = False
        txtOrigin.Text = ""
        txtDestination.Text = ""
        txtCarriage.Text = ""
        txtPlace.Text = ""
        txtFlight.Text = ""
        txtLoading.Text = ""
        txtDischarge.Text = ""
        txtFinalDestination.Text = ""
        txtPayments.Text = ""
        txtContainerNo.Text = ""

        txtInvPrefix.Text = GetDocumentPrefix("E", "I", "") '' "KJFPL/" ''IIf(IsDbNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "EXP/", RsCompany.Fields("COMPANY_SHORTNAME").Value & "/")
        txtCurrFactor.Text = "0.00"
        txtCurrency.Text = ""
        lblTotQty.Text = "0.000"
        lblTotAmount_INR.Text = "0.000"
        lblTotAmount.Text = "0.000"
        txtAgreement.Text = "" ''UCase("(Agency Commission payable to M/s X Y Europe, France is 3% of FOB value as per agreement dated 14.01.2003 entered with M/s X Y Europe, France)")
        txtCreditBank.Text = "" ''IIf(IsNull(RsCompany!CREDITBANK), "", RsCompany!CREDITBANK)     ''UCase("Canara Bank, Foreign Deptt (Main), Bhagwan Dass Road, New Delhi")
        txtCreditBankAddress.Text = "" ''IIf(IsNull(RsCompany!CREDITBANK_ADD), "", RsCompany!CREDITBANK_ADD)
        txtCustomerBank.Text = ""
        txtAccountNo.Text = ""
        txtSwiftCode.Text = ""
        txtADCode.Text = "" ''IIf(IsNull(RsCompany!AD_CODE), "", RsCompany!AD_CODE)
        txtFurtherBank.Text = "" '' IIf(IsNull(RsCompany!FURTHER_BANK), "", RsCompany!FURTHER_BANK)  ''"CANARA BANK, LAJPAT NAGAR (MAIN) BRANCH, NEW DELHI, INDIA - A/c HEMA ENGINEERING INDUSTRIES LTD. OCC A/C 2995"

        chkDC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkExciseInvoice.CheckState = System.Windows.Forms.CheckState.Unchecked
        SSTInfo.SelectedIndex = 0

        txtBuyerName.Enabled = False
        TxtCustomerName.Enabled = True
        txtCustomerCode.Enabled = False

        txtInvNo.Enabled = True
        txtInvDate.Enabled = True
        txtExciseBillNo.Enabled = False
        txtExciseBillDate.Enabled = False

        txtAdvLicDate.Text = ""
        txtAdvLicNo.Text = ""

        txtNotifyParty1.Text = ""
        txtNotifyParty2.Text = ""
        txtNotifyParty3.Text = ""
        txtDiscPer.Text = "0.00"
        txtDiscAmount.Text = "0.00"
        txtOtherAmt.Text = "0.00"

        txtAdvLicDate.Enabled = True
        txtAdvLicNo.Enabled = True

        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCancelled.Enabled = True

        chkREXDeclaration.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtPackNo.Enabled = True
        cmdPackNo.Enabled = True
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        Call FillSprdOther()
        Call FormatSprdOther(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsExpMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FrmExportInvoice_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub FrmExportInvoice_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmExportInvoice_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Private Sub FrmExportInvoice_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7860) '8000
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900

        'AdataItem.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mCompanyCity As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBuyerCode As String
        Dim mFormulaStr As String
        Dim pSqlStr As String
        Dim mMajorCurr As String
        Dim mMinorCurr As String
        Dim mCOMPANYTYPE As String
        Dim mCntRow As Long
        Dim mFormulaName As String
        Dim mFormulaValue As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle,,, "Y")

        mCompanyCity = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        mCompanyCity = mCompanyCity & "-" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        mCompanyCity = mCompanyCity & " (" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & ") INDIA"


        MainClass.AssignCRptFormulas(Report1, "CompanyAddress=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyCity=""" & mCompanyCity & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyEmail=""" & "Email : " & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & """")

        mCOMPANYTYPE = IIf(RsCompany.Fields("ISEOU").Value = "Y", "100% E.O.U.", "")
        MainClass.AssignCRptFormulas(Report1, "COMPANYTYPE=""" & mCOMPANYTYPE & """")

        pSqlStr = "Select * From FIN_EXPORT_PARA_EXP WHERE AUTO_KEY_EXPINV=" & Val(LblMkey.Text) & " ORDER BY SERAIL_NO"
        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mFormulaName = "p" & IIf(IsDBNull(RsTemp.Fields("FIELD_NAME").Value), "", RsTemp.Fields("FIELD_NAME").Value)
                mFormulaValue = IIf(IsDBNull(RsTemp.Fields("FIELD_VALUE").Value), "", RsTemp.Fields("FIELD_VALUE").Value)
                mFormulaValue = mFormulaValue.Replace(vbCrLf, "")
                MainClass.AssignCRptFormulas(Report1, "" & mFormulaName & "=""" & mFormulaValue & """")
                RsTemp.MoveNext()
            Loop
        End If

        If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBuyerCode = MasterNo
            If mBuyerCode = "" Then
                mBuyerCode = txtCustomerCode.Text
            End If

            pSqlStr = " SELECT A.SUPP_CUST_NAME, B.SUPP_CUST_ADDR, " & vbCrLf _
                & " B.SUPP_CUST_CITY, B.COUNTRY, B.SUPP_CUST_PIN, " & vbCrLf _
                & " A.SUPP_CUST_PHONE, A.SUPP_CUST_FAXNO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_BUSINESS_MST B" & vbCrLf _
                & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "'" & vbCrLf _
                & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
                & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
                & " AND B.LOCATION_ID='" & Trim(txtBillTo.Text) & "'"


            MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


            If RsTemp.EOF = False Then
                mFormulaStr = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                MainClass.AssignCRptFormulas(Report1, "BuyerName=""" & mFormulaStr & """")

                mFormulaStr = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                mFormulaStr = mFormulaStr.Replace(vbCrLf, "")
                MainClass.AssignCRptFormulas(Report1, "BuyerAddress=""" & mFormulaStr & """")

                mFormulaStr = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                mFormulaStr = mFormulaStr.Replace(vbCrLf, "")
                MainClass.AssignCRptFormulas(Report1, "BuyerCity=""" & mFormulaStr & """")

                mFormulaStr = IIf(IsDBNull(RsTemp.Fields("COUNTRY").Value), "", RsTemp.Fields("COUNTRY").Value)
                MainClass.AssignCRptFormulas(Report1, "BuyerCountry=""" & mFormulaStr & """")

                mFormulaStr = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PHONE").Value), "", "Phone No.:" & RsTemp.Fields("SUPP_CUST_PHONE").Value)
                mFormulaStr = mFormulaStr & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_FAXNO").Value), "", "Fax No.:" & RsTemp.Fields("SUPP_CUST_FAXNO").Value)
                MainClass.AssignCRptFormulas(Report1, "BuyerPhone=""" & mFormulaStr & """")
            End If
        End If

        pSqlStr = " SELECT CURR_DESC, MINOR_CURR " & vbCrLf & " FROM FIN_CURRENCY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CURR_DESC='" & MainClass.AllowSingleQuote(txtCurrency.Text) & "'"
        MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mMajorCurr = IIf(IsDBNull(RsTemp.Fields("CURR_DESC").Value), "", RsTemp.Fields("CURR_DESC").Value)
            mMinorCurr = IIf(IsDBNull(RsTemp.Fields("MINOR_CURR").Value), "", RsTemp.Fields("MINOR_CURR").Value)
        End If

        mAmountInword = MainClass.RupeesIntoForigenCurr(Val(lblTotAmount.Text), mMajorCurr, mMinorCurr)

        mAmountInword = "(in words) " & UCase(mAmountInword)
        MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")

        Report1.ReportFileName = PubReportFolderPath & mRptFileName


        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtCustomerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCustomerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(TxtCustomerName.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((TxtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCustomerCode = MasterNo
            txtCustomerCode.Text = mCustomerCode
        Else
            mCustomerCode = "-1"
            Cancel = True
        End If

        txtConsigneeAddress.Text = FillAddressDetail(txtCustomerCode.Text, txtShipTo.Text)

        If ADDMode = True Then
            Call FillCustomerDetail()
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function CheckDuplicateItem(ByRef mCheckRowData As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mCheckItem As String
        Dim mItemRept As Integer
        Dim xCheckRowData

        If mCheckRowData = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                xCheckRowData = UCase(.Text)

                '.Col = ColItemCode
                'xCheckRowData = UCase(.Text)

                .Col = ColPalletNo
                xCheckRowData = xCheckRowData & "-" & UCase(.Text)

                SprdMain.Col = ColGlassDescription
                xCheckRowData = xCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                'SprdMain.Col = ColActualHeight
                'mCheckRowData = mCheckRowData & Val(SprdMain.Text)

                'SprdMain.Col = ColActualWidth
                'mCheckRowData = mCheckRowData & Val(SprdMain.Text)


                SprdMain.Col = ColSize
                xCheckRowData = xCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Col = ColModelNo
                xCheckRowData = xCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Col = ColDrawingNo
                xCheckRowData = xCheckRowData & MainClass.AllowSingleQuote(SprdMain.Text)

                If UCase(mCheckItem) = UCase(mCheckRowData) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)

    End Function

    Private Sub txtPayments_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPayments.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPayments_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPayments.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPayments.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPlace_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlace.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlace_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPlace.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPlace.Text)
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


    Private Sub txtSwiftCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSwiftCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSwiftCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSwiftCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSwiftCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtADCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtADCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtADCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdOther_Change(sender As Object, e As _DSpreadEvents_ChangeEvent) Handles SprdOther.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdOther_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdOther.KeyUpEvent
        Dim mCol As Short
        mCol = SprdOther.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOthersValue Then SprdOther_ClickEvent(SprdOther, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOthersValue, SprdOther.ActiveRow))

        SprdOther.Refresh()
    End Sub

    Private Sub cmdsearchConsinee_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchConsinee.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & ""        '' AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((TxtCustomerName.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR || SUPP_CUST_CITY", SqlStr) = True Then
            TxtCustomerName.Text = AcName
            txtShipTo.Text = AcName2
            txtCustomerName_Validating(TxtCustomerName, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCustomerName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCustomerName.DoubleClick
        cmdsearchConsinee_Click(cmdsearchConsinee, New System.EventArgs())
    End Sub

    Private Sub txtCustomerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtCustomerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearchConsinee_Click(cmdsearchConsinee, New System.EventArgs())
    End Sub
    Private Sub txtBuyerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBuyerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtBuyerName.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((txtBuyerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Buyer Name")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCurrency_KeyUp(sender As Object, EventArgs As KeyEventArgs) Handles txtCurrency.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCurrency()
    End Sub
    Private Sub SearchCurrency()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtCurrency.Text), "FIN_CURRENCY_MST", "CURR_DESC", "", "", "", SqlStr) = True Then
            txtCurrency.Text = AcName
            txtCurrency_Validating(txtCurrency, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCurrency_Validating(sender As Object, EventArgs As CancelEventArgs) Handles txtCurrency.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtCurrency.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtCurrency.Text, "CURR_DESC", "CURR_DESC", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Currency.", vbInformation)
            Cancel = True
            Exit Sub
        End If
        Call CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLoading_DoubleClick(sender As Object, e As EventArgs) Handles txtLoading.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIELD_NAME='LOADINGPORT'"


        '-FIELD_NAME
        '--FIELD_VALUE
        '--DISCHARGEPORT
        '--FINALDESTINATION
        '--

        If MainClass.SearchGridMaster((txtLoading.Text), "FIN_EXPORT_FIELD_MST", "FIELD_VALUE", "", "", "", SqlStr) = True Then
            txtLoading.Text = AcName
            txtLoading_Validating(txtLoading, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtLoading_Validating(sender As Object, EventArgs As CancelEventArgs) Handles txtLoading.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtLoading.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIELD_NAME='LOADINGPORT'"

        If MainClass.ValidateWithMasterTable(txtLoading.Text, "FIELD_VALUE", "FIELD_VALUE", "FIN_EXPORT_FIELD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Loading.", vbInformation)
            Cancel = True
            Exit Sub
        End If
        'Call CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDischarge_DoubleClick(sender As Object, e As EventArgs) Handles txtDischarge.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIELD_NAME='DISCHARGEPORT'"


        '-FIELD_NAME
        '--FIELD_VALUE
        '--
        '--FINALDESTINATION
        '--

        If MainClass.SearchGridMaster((txtDischarge.Text), "FIN_EXPORT_FIELD_MST", "FIELD_VALUE", "", "", "", SqlStr) = True Then
            txtDischarge.Text = AcName
            txtDischarge_Validating(txtDischarge, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDischarge_Validating(sender As Object, EventArgs As CancelEventArgs) Handles txtDischarge.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtDischarge.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIELD_NAME='DISCHARGEPORT'"

        If MainClass.ValidateWithMasterTable(txtDischarge.Text, "FIELD_VALUE", "FIELD_VALUE", "FIN_EXPORT_FIELD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Discharge.", vbInformation)
            Cancel = True
            Exit Sub
        End If
        Call CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtFinalDestination_DoubleClick(sender As Object, e As EventArgs) Handles txtFinalDestination.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIELD_NAME='FINALDESTINATION'"

        If MainClass.SearchGridMaster((txtFinalDestination.Text), "FIN_EXPORT_FIELD_MST", "FIELD_VALUE", "", "", "", SqlStr) = True Then
            txtFinalDestination.Text = AcName
            txtFinalDestination_Validating(txtFinalDestination, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtFinalDestination_Validating(sender As Object, EventArgs As CancelEventArgs) Handles txtFinalDestination.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtFinalDestination.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FIELD_NAME='FINALDESTINATION'"

        If MainClass.ValidateWithMasterTable(txtFinalDestination.Text, "FIELD_VALUE", "FIELD_VALUE", "FIN_EXPORT_FIELD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Final Destination.", vbInformation)
            Cancel = True
            Exit Sub
        End If
        ''Call CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLoading_KeyUp(sender As Object, e As KeyEventArgs) Handles txtLoading.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            txtLoading_DoubleClick(txtLoading, New System.EventArgs())
        End If
    End Sub
    Private Sub txtFinalDestination_KeyUp(sender As Object, e As KeyEventArgs) Handles txtFinalDestination.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            txtFinalDestination_DoubleClick(txtFinalDestination, New System.EventArgs())
        End If
    End Sub
    Private Sub txtDischarge_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDischarge.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            txtDischarge_DoubleClick(txtDischarge, New System.EventArgs())
        End If
    End Sub
End Class
