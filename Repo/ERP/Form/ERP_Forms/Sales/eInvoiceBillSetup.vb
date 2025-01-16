Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmeInvoiceBillSetup
   Inherits System.Windows.Forms.Form

   Dim XRIGHT As String
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim FormActive As Boolean

   Dim mADDMode As Boolean
   Dim RseWaySetup As ADODB.Recordset

   Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdcancel.Click
        Me.Hide()
        Me.Close()
   End Sub
   Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
      If FieldVerification = False Then Exit Sub
      If Update1 = True Then cmdSave.Enabled = False
   End Sub
   Private Sub frmeInvoiceBillSetup_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

      On Error GoTo ERR1
      If FormActive = True Then Exit Sub
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      MainClass.UOpenRecordSet("Select * From GEN_EINVSETUP_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RseWaySetup, ADODB.LockTypeEnum.adLockReadOnly)
      Call SetMaxLength()
      Call Show1()
      FormActive = True
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
      Exit Sub
ERR1:
      MsgBox(Err.Description)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub frmeInvoiceBillSetup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

      Call SetMainFormCordinate(Me)
      XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
      MainClass.RightsToButton(Me, XRIGHT)
      MainClass.SetControlsColor(Me)

      ADDMode = False
      MODIFYMode = False
      If XRIGHT <> "" Then MODIFYMode = True
   End Sub
   Private Sub SetMaxLength()
      txtCDKey.MaxLength = RseWaySetup.Fields("CD_KEY").DefinedSize
      txtEFUserName.MaxLength = RseWaySetup.Fields("EF_USERNAME").DefinedSize
      txtEFPassword.MaxLength = RseWaySetup.Fields("EF_PASSWORD").DefinedSize
      txtEINVUserName.MaxLength = RseWaySetup.Fields("E_INV_USERNAME").DefinedSize
      txtEINVPassword.MaxLength = RseWaySetup.Fields("E_INV_PASSWORD").DefinedSize
      txtGenerateURL.MaxLength = RseWaySetup.Fields("GENERATE_URL").DefinedSize
      txtCancelURL.MaxLength = RseWaySetup.Fields("CANCEL_URL").DefinedSize
      txtGetByIRN.MaxLength = RseWaySetup.Fields("GETBYIRN_URL").DefinedSize
      txteInvoicePrint.MaxLength = RseWaySetup.Fields("PRINT_INV_URL").DefinedSize
      txteWayBillGenerate.MaxLength = RseWaySetup.Fields("GENERATE_EWAY_URL").DefinedSize
   End Sub
   Private Sub Show1()
      On Error GoTo ERR1
      ShowAddress()
      cmdSave.Enabled = False
      Exit Sub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Sub

   Sub ShowAddress()
      On Error GoTo ERR1
      If RseWaySetup.EOF = False Then
         txtCDKey.Text = IIf(IsDBNull(RseWaySetup.Fields("CD_KEY").Value), "", RseWaySetup.Fields("CD_KEY").Value)
         txtEFUserName.Text = IIf(IsDBNull(RseWaySetup.Fields("EF_USERNAME").Value), "", RseWaySetup.Fields("EF_USERNAME").Value)
         txtEFPassword.Text = IIf(IsDBNull(RseWaySetup.Fields("EF_PASSWORD").Value), "", RseWaySetup.Fields("EF_PASSWORD").Value)
         txtEINVUserName.Text = IIf(IsDBNull(RseWaySetup.Fields("E_INV_USERNAME").Value), "", RseWaySetup.Fields("E_INV_USERNAME").Value)
         txtEINVPassword.Text = IIf(IsDBNull(RseWaySetup.Fields("E_INV_PASSWORD").Value), "", RseWaySetup.Fields("E_INV_PASSWORD").Value)
         txtGenerateURL.Text = IIf(IsDBNull(RseWaySetup.Fields("GENERATE_URL").Value), "", RseWaySetup.Fields("GENERATE_URL").Value)
         txtCancelURL.Text = IIf(IsDBNull(RseWaySetup.Fields("CANCEL_URL").Value), "", RseWaySetup.Fields("CANCEL_URL").Value)
         txtGetByIRN.Text = IIf(IsDBNull(RseWaySetup.Fields("GETBYIRN_URL").Value), "", RseWaySetup.Fields("GETBYIRN_URL").Value)

         txteInvoicePrint.Text = IIf(IsDBNull(RseWaySetup.Fields("PRINT_INV_URL").Value), "", RseWaySetup.Fields("PRINT_INV_URL").Value)
         txteWayBillGenerate.Text = IIf(IsDBNull(RseWaySetup.Fields("GENERATE_EWAY_URL").Value), "", RseWaySetup.Fields("GENERATE_EWAY_URL").Value)

      Else
         txtCDKey.Text = ""
         txtEFUserName.Text = ""
         txtEFPassword.Text = ""
         txtEINVUserName.Text = ""
         txtEINVPassword.Text = ""
         txtGenerateURL.Text = ""
         txtCancelURL.Text = ""
         txtGetByIRN.Text = ""
         txteInvoicePrint.Text = ""
         txteWayBillGenerate.Text = ""
      End If

      Exit Sub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Sub
   Private Function Update1() As Boolean

      On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim xCode As Integer


      PubDBCn.Errors.Clear()
      PubDBCn.BeginTrans()

      Sqlstr = ""
      xCode = RsCompany.Fields("Company_Code").Value

      If MainClass.ValidateWithMasterTable(xCode, "Company_Code", "Company_Code", "GEN_EINVSETUP_MST", PubDBCn, MasterNo) = True Then
         mADDMode = False
      Else
         mADDMode = True
      End If

      If mADDMode = True Then
            SqlStr = "INSERT INTO GEN_EINVSETUP_MST ( " & vbCrLf _
                & " COMPANY_CODE, CD_KEY, EF_USERNAME, " & vbCrLf _
                & " EF_PASSWORD, E_INV_USERNAME, E_INV_PASSWORD,  " & vbCrLf _
                & " GENERATE_URL, CANCEL_URL, GETBYIRN_URL, PRINT_INV_URL, GENERATE_EWAY_URL"

            SqlStr = SqlStr & vbCrLf _
                & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " " & xCode & ", '" & MainClass.AllowSingleQuote(txtCDKey.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtEFUserName.Text) & "','" & MainClass.AllowSingleQuote(txtEFPassword.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtEINVUserName.Text) & "','" & MainClass.AllowSingleQuote(txtEINVPassword.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtGenerateURL.Text) & "','" & MainClass.AllowSingleQuote(txtCancelURL.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtGetByIRN.Text) & "','" & MainClass.AllowSingleQuote(txteInvoicePrint.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txteWayBillGenerate.Text) & "')"
        Else
            SqlStr = "UPDATE  GEN_EINVSETUP_MST SET " & vbCrLf _
                & " CD_KEY='" & MainClass.AllowSingleQuote(txtCDKey.Text) & "'," & vbCrLf _
                & " EF_USERNAME='" & MainClass.AllowSingleQuote(txtEFUserName.Text) & "'," & vbCrLf _
                & " EF_PASSWORD='" & MainClass.AllowSingleQuote(txtEFPassword.Text) & "'," & vbCrLf _
                & " E_INV_USERNAME='" & MainClass.AllowSingleQuote(txtEINVUserName.Text) & "'," & vbCrLf _
                & " E_INV_PASSWORD='" & MainClass.AllowSingleQuote(txtEINVPassword.Text) & "'," & vbCrLf _
                & " GENERATE_URL= '" & MainClass.AllowSingleQuote(txtGenerateURL.Text) & "'," & vbCrLf _
                & " CANCEL_URL= '" & MainClass.AllowSingleQuote(txtCancelURL.Text) & "'," & vbCrLf _
                & " GETBYIRN_URL= '" & MainClass.AllowSingleQuote(txtGetByIRN.Text) & "'," & vbCrLf _
                & " PRINT_INV_URL='" & MainClass.AllowSingleQuote(txteInvoicePrint.Text) & "'," & vbCrLf _
                & " GENERATE_EWAY_URL='" & MainClass.AllowSingleQuote(txteWayBillGenerate.Text) & "'"


            SqlStr = Sqlstr & vbCrLf & " WHERE Company_Code=" & xCode & ""

      End If

      PubDBCn.Execute(Sqlstr)

      PubDBCn.CommitTrans()
      Update1 = True
      RseWaySetup.Requery() ''.Refresh

      Exit Function
err_Renamed:
      Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
      Update1 = False
      PubDBCn.RollbackTrans() ''
      RseWaySetup.Requery() ''.Refresh


   End Function
   Private Sub frmeInvoiceBillSetup_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
      FormActive = False
      RseWaySetup = Nothing
   End Sub
   Private Function FieldVerification() As Boolean
      On Error GoTo ERR1
      FieldVerification = True


      '    If Trim(txtGenerateURL.Text) = "" Then
      '        MsgInformation "Generate URL cann't be blank."
      '        txtGenerateURL.SetFocus
      '        FieldVerification = False
      '        Exit Function
      '    End If
      '
      '    If Trim(txtCancelURL.Text) = "" Then
      '        MsgInformation "Cancel URL cann't be blank."
      '        txtCancelURL.SetFocus
      '        FieldVerification = False
      '        Exit Function
      '    End If
      '
      '
      '    If Trim(txtGetByIRN.Text) = "" Then
      '        MsgInformation "Get By ID URL cann't be blank."
      '        txtGetByIRN.SetFocus
      '        FieldVerification = False
      '        Exit Function
      '    End If


      Exit Function
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Function
   Private Sub txtCancelURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCancelURL.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtCDKey_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCDKey.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtEFPassword_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEFPassword.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtEFUserName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEFUserName.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txteInvoicePrint_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteInvoicePrint.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtEINVPassword_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEINVPassword.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtEINVUserName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEINVUserName.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txteWayBillGenerate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txteWayBillGenerate.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtGenerateURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGenerateURL.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
   Private Sub txtGetByIRN_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGetByIRN.TextChanged
      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub
End Class