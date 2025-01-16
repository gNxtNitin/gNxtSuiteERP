Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmInvoiceReset
   Inherits System.Windows.Forms.Form
   'Dim PvtDBCn As ADODB.Connection

   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String

   Dim FormActive As Boolean


   Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
   Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
      On Error GoTo ErrPart
      Dim mAuthorisation As String

      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

      If Trim(txtReason.Text) = "" Then
         MsgInformation("Please enter the Reason")
         System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
         Exit Sub
      End If

      If Update1 = False Then GoTo ErrPart
      CmdSave.Enabled = False
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
      Exit Sub
ErrPart:
      ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub frmInvoiceReset_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
      On Error GoTo ERR1

      If FormActive = True Then Exit Sub

      If lblBookType.Text = "S" Then
         Me.Text = "Invoice Printing Reset"
         lblInvoice.Text = "Invoice No"
      Else
         Me.Text = "RGP/NRGP Printing Reset"
         lblInvoice.Text = "Gatepass No"
      End If

        chkReset.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPackingReset.CheckState = System.Windows.Forms.CheckState.Unchecked

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

      FormActive = True
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
      Exit Sub
ERR1:
      MsgBox(Err.Description)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub frmInvoiceReset_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub

   Private Sub frmInvoiceReset_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

      On Error GoTo ErrPart
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      Call SetMainFormCordinate(Me)
      'Me.Top = 0
      'Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(2790)
        ''Me.Width = VB6.TwipsToPixelsX(8640)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mLock As String
        Dim mCustomerName As String
        Dim mCustomerCode As String

        mLock = "N"

        If lblBookType.Text = "S" Then
            SqlStr = " SELECT BILLNO, INVOICE_DATE, PRINTED, SUPP_CUST_CODE,PRINT_PACKING " & vbCrLf _
                     & " FROM FIN_INVOICE_HDR" & vbCrLf _
                     & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                     & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                     & " AND BILLNO='" & MainClass.AllowSingleQuote(txtInvoiceNo.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                txtInvoiceNo.Text = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                txtInvoiceDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "dd/MM/yyyy")

                mCustomerCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mLock = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "", RsTemp.Fields("PRINTED").Value)

                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomerName.Text = MasterNo
                End If

                chkReset.CheckState = IIf(mLock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mLock = IIf(IsDBNull(RsTemp.Fields("PRINT_PACKING").Value), "", RsTemp.Fields("PRINT_PACKING").Value)
                chkPackingReset.CheckState = IIf(mLock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                CmdSave.Enabled = True
            End If
        Else
            SqlStr = " SELECT AUTO_KEY_PASSNO, GATEPASS_DATE, PRINTED, 'N' AS PRINT_PACKING,SUPP_CUST_CODE " & vbCrLf _
                & " FROM INV_GATEPASS_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_PASSNO=" & Val(txtInvoiceNo.Text) & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                txtInvoiceNo.Text = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PASSNO").Value), "", RsTemp.Fields("AUTO_KEY_PASSNO").Value)
                txtInvoiceDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("GATEPASS_DATE").Value), "", RsTemp.Fields("GATEPASS_DATE").Value), "dd/MM/yyyy")

                mCustomerCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mLock = IIf(IsDBNull(RsTemp.Fields("PRINTED").Value), "", RsTemp.Fields("PRINTED").Value)

                If MainClass.ValidateWithMasterTable(mCustomerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustomerName.Text = MasterNo
                End If

                chkReset.CheckState = IIf(mLock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mLock = IIf(IsDBNull(RsTemp.Fields("PRINT_PACKING").Value), "", RsTemp.Fields("PRINT_PACKING").Value)
                chkPackingReset.CheckState = IIf(mLock = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkPackingReset.Enabled = False

                '
                CmdSave.Enabled = True
            End If
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateErr
        Dim SqlStr As String = ""
        Dim mResetValue As String
        Dim mResetPackValue As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mResetValue = IIf(chkReset.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mResetPackValue = IIf(chkPackingReset.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If lblBookType.Text = "S" Then

            SqlStr = " UPDATE FIN_INVOICE_HDR " & vbCrLf _
                & " SET PRINTED ='" & mResetValue & "', PRINT_PACKING='" & mResetPackValue & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE Company_Code= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR= " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND BILLNO= '" & MainClass.AllowSingleQuote(txtInvoiceNo.Text) & "' "


            PubDBCn.Execute(SqlStr)
        Else
            SqlStr = " UPDATE INV_GATEPASS_HDR " & vbCrLf & " SET PRINTED ='" & mResetValue & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE Company_Code= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO= " & Val(txtInvoiceNo.Text) & " "


            PubDBCn.Execute(SqlStr)
        End If

        SqlStr = " INSERT INTO FIN_INVOICE_PRINT_LOG (" & vbCrLf _
            & " COMPANY_CODE, FYEAR, BOOKTYPE, REFNO, REFDATE, REMARKS, ADDUSER, ADDDATE " & vbCrLf _
            & " ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", '" & MainClass.AllowSingleQuote(lblBookType.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtInvoiceNo.Text) & "', TO_DATE('" & VB6.Format(txtInvoiceDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtReason.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " ) "


        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()

        Update1 = True

        Exit Function
UpdateErr:
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        ''Resume
    End Function
    Private Sub frmInvoiceReset_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub


    Private Sub txtInvoiceNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvoiceNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInvoiceNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvoiceNo.DoubleClick
        Call SearchBillNo()
    End Sub
    Private Sub txtInvoiceNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvoiceNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInvoiceNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtInvoiceNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInvoiceNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchBillNo()
    End Sub
    Private Sub txtInvoiceNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvoiceNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If Trim(txtInvoiceNo.Text) = "" Then GoTo EventExitSub

        If lblBookType.Text = "S" Then
            If MainClass.ValidateWithMasterTable(txtInvoiceNo.Text, "BILLNO", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "") = False Then
                MsgBox("Invaild Bill No.")
                Cancel = True
                Exit Sub
            Else
                Call Show1()
            End If
        Else
            If MainClass.ValidateWithMasterTable(txtInvoiceNo.Text, "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invaild Gatepass No.")
                Cancel = True
                Exit Sub
            Else
                Call Show1()
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchBillNo()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If lblBookType.Text = "S" Then
            If MainClass.SearchGridMaster(txtInvoiceNo.Text, "FIN_INVOICE_HDR", "BILLNO", "INVOICE_DATE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "") = True Then
                txtInvoiceNo.Text = AcName
                txtInvoiceNo_Validating(txtInvoiceNo.Text, New System.ComponentModel.CancelEventArgs(False))
                If txtInvoiceNo.Enabled = True Then txtInvoiceNo.Focus()
            End If
        Else
            If MainClass.SearchGridMaster(txtInvoiceNo.Text, "INV_GATEPASS_HDR", "AUTO_KEY_PASSNO", "GATEPASS_DATE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtInvoiceNo.Text = AcName
                txtInvoiceNo_Validating(txtInvoiceNo.Text, New System.ComponentModel.CancelEventArgs(False))
                If txtInvoiceNo.Enabled = True Then txtInvoiceNo.Focus()
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

   Private Sub txtReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReason.TextChanged

      MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReason.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtReason.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
End Class
