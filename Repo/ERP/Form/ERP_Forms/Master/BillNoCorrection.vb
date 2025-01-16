Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmBillNoCorrection
   Inherits System.Windows.Forms.Form
    Dim SqlStr As String = ""
    Dim RsModvat As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub FrmBillNoCorrection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmBillNoCorrection_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LErr
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        MainClass.SetControlsColor(Me)
        txtNewBillDate.Text = VB6.Format(RunDate, "dd/MM/yyyy")
        txtMRRDate.Text = VB6.Format(RunDate, "dd/MM/yyyy")
        Me.Text = "Update Item Description & PartNo"

        ''Me.Height = VB6.TwipsToPixelsY(3615)
        ''Me.Width = VB6.TwipsToPixelsX(7230)
        'Me.Top = 0
        'Me.Left = 0

        Exit Sub
LErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtNewBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNewBillDate.TextChanged
        'MainClass.SaveStatus Me, ADDMode, MODIFYMode
    End Sub
    Private Sub txtNewBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNewBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtNewBillDate.Text = "" Then
            MsgBox("Date From Cannot Be Blank", MsgBoxStyle.Critical)
            txtNewBillDate.Focus()
            Cancel = True
        ElseIf txtNewBillDate.Text <> "" Then
            If Not IsDate(txtNewBillDate.Text) Then
                MsgBox("Invalid Date Pl. Check", MsgBoxStyle.Critical)
                txtNewBillDate.Focus()
                Cancel = True
            ElseIf FYChk(CStr(CDate(txtNewBillDate.Text))) = False Then
                '            txtNewBillDate.SetFocus
                '            Cancel = True
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNewBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNewBillNo.TextChanged
        'MainClass.SaveStatus Me, ADDMode, MODIFYMode
    End Sub

    Private Sub txtNewBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNewBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNewBillNo.Text) ''MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMRRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtMRRDate.Text = "" Then
            MsgBox("Date To. Cannot Be Blank", MsgBoxStyle.Critical)
            txtMRRDate.Focus()
            Cancel = True
            GoTo EventExitSub
        ElseIf txtMRRDate.Text <> "" Then
            If Not IsDate(txtMRRDate.Text) Then
                MsgBox("Invalid Date Pl. Check", MsgBoxStyle.Critical)
                txtMRRDate.Focus()
                Cancel = True
            ElseIf FYChk(CStr(CDate(txtMRRDate.Text))) = False Then
                txtMRRDate.Focus()
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        On Error GoTo ErrorPart

        Dim SqlStr As String = ""
        Dim mSupplierCode As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


        If Val(txtMRRNo.Text) = 0 Then
            MsgInformation("MRR No. cann't be blank., Cann't save")
            Exit Sub
        End If

        If Trim(txtBillNo.Text) = "" Then
            MsgInformation("Bill No is Blank, Cann't save")
            Exit Sub
        End If

        mSupplierCode = "-1"
        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If


        If DuplicateBillNo(mSupplierCode) = True Then
            MsgBox("Duplicate Bill No for Such Supplier.", MsgBoxStyle.Information)
            If txtBillNo.Enabled = True Then txtBillNo.Focus()
            Exit Sub
        End If

        If Trim(txtMRRDate.Text) = "" Then
            MsgInformation("Modvat Date Blank, Cann't save")
            Exit Sub
        End If

        If Trim(txtNewBillDate.Text) = "" Then
            MsgInformation("New Bill Date Blank, Cann't save")
            Exit Sub
        End If

        If Not IsDate(txtNewBillDate.Text) Then
            MsgInformation("Invalid Bill Date, Cann't save")
            Exit Sub
            '    ElseIf FYChk(CDate(txtNewBillDate.Text)) = False Then
            '        txtMRRDate.SetFocus
            '        Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " UPDATE INV_GATE_HDR SET BILL_NO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
              & " BILL_DATE=TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
              & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
              & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND UNDER_CHALLAN='N'"
        PubDBCn.Execute(SqlStr)


        SqlStr = " UPDATE INV_GATEENTRY_HDR SET BILL_NO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
                & " BILL_DATE=TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_NO=" & Val(txtMRRNo.Text) & "  AND UNDER_CHALLAN='N'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE INV_RGP_REG_TRN SET BILL_NO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
              & " BILL_DATE=TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO =" & Val(txtMRRNo.Text) & " AND BOOKTYPE ='M'"
        PubDBCn.Execute(SqlStr)


        SqlStr = " UPDATE FIN_PURCHASE_HDR SET BILLNO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
                & " INVOICE_DATE=TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE FIN_POSTED_TRN SET BILLNO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
                & " BILLDATE=TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND BOOKTYPE='P' AND MKEY IN (" & vbCrLf _
                & " SELECT MKEY FROM FIN_PURCHASE_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ")"
        PubDBCn.Execute(SqlStr)


        SqlStr = " UPDATE FIN_BILLDETAILS_TRN SET BILLNO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
                & " BILLDATE=TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE ACCOUNTCODE='" & mSupplierCode & "'" & vbCrLf _
                & " AND BILLNO='" & Trim(txtBillNo.Text) & "'" & vbCrLf _
                & " AND BILLDATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND MKEY IN (SELECT MKEY FROM FIN_VOUCHER_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"
        PubDBCn.Execute(SqlStr)

        SqlStr = " UPDATE FIN_POSTED_TRN SET BILLNO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
                & " BILLDATE=TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE ACCOUNTCODE='" & mSupplierCode & "'" & vbCrLf _
                & " AND BILLNO='" & Trim(txtBillNo.Text) & "'" & vbCrLf _
                & " AND BILLDATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        PubDBCn.Execute(SqlStr)


        SqlStr = " UPDATE FIN_DNCN_HDR SET BILLNO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
                & " INVOICE_DATE =TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND (DEBITACCOUNTCODE='" & mSupplierCode & "' OR CREDITACCOUNTCODE='" & mSupplierCode & "')" & vbCrLf _
                & " AND BILLNO='" & Trim(txtBillNo.Text) & "'" & vbCrLf _
                & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""

        PubDBCn.Execute(SqlStr)


        SqlStr = " UPDATE FIN_DNCN_DET SET SUPP_REF_NO='" & Trim(txtNewBillNo.Text) & "', " & vbCrLf _
                & " SUPP_REF_DATE =TO_DATE('" & VB6.Format(txtNewBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND MRR_REF_NO=" & Val(txtMRRNo.Text) & "" & vbCrLf _
                & " AND SUPP_REF_NO='" & Trim(txtBillNo.Text) & "'" & vbCrLf _
                & " AND SUPP_REF_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        MsgInformation("Updated Successfully.")
        Clear1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub TxtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged
        'MainClass.SaveStatus Me, ADDMode, MODIFYMode
    End Sub

    Private Sub TxtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""


        If Val(txtMRRNo.Text) = 0 Then GoTo EventExitSub

        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = VB6.Format(Val(txtMRRNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If


        SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModvat, ADODB.LockTypeEnum.adLockReadOnly)

        If RsModvat.EOF = False Then
            Clear1()
            Show1()
        Else
            MsgBox("Invalid MRR No.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Clear1()
        lblMKey.Text = ""
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""
        txtSupplierName.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = ""
        txtNewBillNo.Text = ""
        txtNewBillDate.Text = ""

    End Sub
    Private Sub Show1()
        On Error GoTo ERR1

        With RsModvat
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_MRR").Value

                txtMRRNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "dd/MM/yyyy")

                If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplierName.Text = MasterNo
                End If

                txtBillNo.Text = IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "dd/MM/yyyy")

                txtNewBillNo.Text = IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtNewBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "dd/MM/yyyy")
            End If
        End With
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''    Resume
    End Sub
    Private Function DuplicateBillNo(ByRef pSuppCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMRRNO As Double

        If Trim(txtMRRNo.Text) = "" Then
            mMRRNO = -1
        Else
            mMRRNO = Val(txtMRRNo.Text)
        End If

        DuplicateBillNo = False
        SqlStr = "SELECT BILL_NO " & vbCrLf _
            & " FROM INV_GATE_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRIM(SUPP_CUST_CODE)='" & pSuppCode & "'  AND BILL_NO='" & Trim(txtNewBillNo.Text) & "'" & vbCrLf _
            & " AND AUTO_KEY_MRR<>" & mMRRNO & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            DuplicateBillNo = True
        End If

        Exit Function
ErrPart:
        DuplicateBillNo = False
    End Function

   Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged
      'MainClass.SaveStatus Me, ADDMode, MODIFYMode
   End Sub
End Class
