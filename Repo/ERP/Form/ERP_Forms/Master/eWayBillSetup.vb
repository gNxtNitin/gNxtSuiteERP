Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmeWayBillSetup
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
        If Update1 = True Then CmdSave.Enabled = False
    End Sub
    Private Sub frmeWayBillSetup_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.UOpenRecordSet("Select * From GEN_EWAYSETUP_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RseWaySetup, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetMaxLength()
        Call Show1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmeWayBillSetup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Call SetMainFormCordinate(Me)

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        If XRIGHT <> "" Then MODIFYMode = True
    End Sub
    Private Sub SetMaxLength()


        txtUpdateURL.MaxLength = RseWaySetup.Fields("UPDATE_URL").DefinedSize
        txtGenerateURL.MaxLength = RseWaySetup.Fields("GENERATE_URL").DefinedSize
        txtCancelURL.MaxLength = RseWaySetup.Fields("CANCEL_URL").DefinedSize
        txtFatchURL.MaxLength = RseWaySetup.Fields("FATCH_URL").DefinedSize
        txtGetByIDURL.MaxLength = RseWaySetup.Fields("GETBYID_URL").DefinedSize


        txtCDKey.MaxLength = RseWaySetup.Fields("CD_KEY").DefinedSize
        txtEFUserName.MaxLength = RseWaySetup.Fields("EF_USERNAME").DefinedSize
        txtEFPassword.MaxLength = RseWaySetup.Fields("EF_PASSWORD").DefinedSize
        txtEWBUserName.MaxLength = RseWaySetup.Fields("EWB_USERNAME").DefinedSize
        txtEWBPassword.MaxLength = RseWaySetup.Fields("EWB_PASSWORD").DefinedSize
        txtCreateURLWebtel.MaxLength = RseWaySetup.Fields("CREATE_URL_WEBTEL").DefinedSize

        'txtCreateURLWebtel.MaxLength = RseWaySetup.Fields("CREATE_URL_WEBTEL").DefinedSize
        txtConsilidationURLWebtel.MaxLength = RseWaySetup.Fields("CONSOL_URL_WEBTEL").DefinedSize
        txtGetByDistanceURL.MaxLength = RseWaySetup.Fields("DISTANCE_URL").DefinedSize
        txtGetPrintURL.MaxLength = RseWaySetup.Fields("PRINT_URL").DefinedSize

        txtDigitalSignURL.MaxLength = RseWaySetup.Fields("DS_URL").DefinedSize
        txtDigitalSignUID.MaxLength = RseWaySetup.Fields("DS_USERID").DefinedSize
        txtDigitalSignPassword.MaxLength = RseWaySetup.Fields("DS_PASSWORD").DefinedSize

        txtDigitalSignTopLeft.MaxLength = RseWaySetup.Fields("DS_TOPLEFT").Precision
        txtDigitalSignBottomLeft.MaxLength = RseWaySetup.Fields("DS_BOTTOMLEFT").Precision
        txtDigitalSignTopRight.MaxLength = RseWaySetup.Fields("DS_TOPRIGHT").Precision
        txtDigitalSignBottomRight.MaxLength = RseWaySetup.Fields("DS_BOTTOMRIGHT").Precision
        txtDSAuthSign.MaxLength = RseWaySetup.Fields("DS_AUTH_SIGN").DefinedSize

        txtFontSize.MaxLength = RseWaySetup.Fields("DS_FONT_SIZE").Precision
        txtFindAuth.MaxLength = RseWaySetup.Fields("DS_FIND_AUTH").DefinedSize
        txtDSCertificateNo.MaxLength = RseWaySetup.Fields("DS_CERTIFICATE_SNO").DefinedSize

    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        ShowAddress()
        CmdSave.Enabled = False
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Sub ShowAddress()
        On Error GoTo ERR1
        Dim mDSType As String

        If RseWaySetup.EOF = False Then

            txtUpdateURL.Text = IIf(IsDBNull(RseWaySetup.Fields("UPDATE_URL").Value), "", RseWaySetup.Fields("UPDATE_URL").Value)
            txtGenerateURL.Text = IIf(IsDBNull(RseWaySetup.Fields("GENERATE_URL").Value), "", RseWaySetup.Fields("GENERATE_URL").Value)
            txtCancelURL.Text = IIf(IsDBNull(RseWaySetup.Fields("CANCEL_URL").Value), "", RseWaySetup.Fields("CANCEL_URL").Value)
            txtFatchURL.Text = IIf(IsDBNull(RseWaySetup.Fields("FATCH_URL").Value), "", RseWaySetup.Fields("FATCH_URL").Value)
            txtGetByIDURL.Text = IIf(IsDBNull(RseWaySetup.Fields("GETBYID_URL").Value), "", RseWaySetup.Fields("GETBYID_URL").Value)

            txtCDKey.Text = IIf(IsDBNull(RseWaySetup.Fields("CD_KEY").Value), "", RseWaySetup.Fields("CD_KEY").Value)
            txtEFUserName.Text = IIf(IsDBNull(RseWaySetup.Fields("EF_USERNAME").Value), "", RseWaySetup.Fields("EF_USERNAME").Value)
            txtEFPassword.Text = IIf(IsDBNull(RseWaySetup.Fields("EF_PASSWORD").Value), "", RseWaySetup.Fields("EF_PASSWORD").Value)
            txtEWBUserName.Text = IIf(IsDBNull(RseWaySetup.Fields("EWB_USERNAME").Value), "", RseWaySetup.Fields("EWB_USERNAME").Value)
            txtEWBPassword.Text = IIf(IsDBNull(RseWaySetup.Fields("EWB_PASSWORD").Value), "", RseWaySetup.Fields("EWB_PASSWORD").Value)
            txtCreateURLWebtel.Text = IIf(IsDBNull(RseWaySetup.Fields("CREATE_URL_WEBTEL").Value), "", RseWaySetup.Fields("CREATE_URL_WEBTEL").Value)
            txtConsilidationURLWebtel.Text = IIf(IsDBNull(RseWaySetup.Fields("CONSOL_URL_WEBTEL").Value), "", RseWaySetup.Fields("CONSOL_URL_WEBTEL").Value)

            txtGetByDistanceURL.Text = IIf(IsDBNull(RseWaySetup.Fields("DISTANCE_URL").Value), "", RseWaySetup.Fields("DISTANCE_URL").Value)
            txtGetPrintURL.Text = IIf(IsDBNull(RseWaySetup.Fields("PRINT_URL").Value), "", RseWaySetup.Fields("PRINT_URL").Value)

            txtDigitalSignURL.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_URL").Value), "", RseWaySetup.Fields("DS_URL").Value)
            txtDigitalSignUID.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_USERID").Value), "", RseWaySetup.Fields("DS_USERID").Value)
            txtDigitalSignPassword.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_PASSWORD").Value), "", RseWaySetup.Fields("DS_PASSWORD").Value)

            txtDigitalSignTopLeft.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_TOPLEFT").Value), 0, RseWaySetup.Fields("DS_TOPLEFT").Value)
            txtDigitalSignBottomLeft.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_BOTTOMLEFT").Value), 0, RseWaySetup.Fields("DS_BOTTOMLEFT").Value)
            txtDigitalSignTopRight.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_TOPRIGHT").Value), 0, RseWaySetup.Fields("DS_TOPRIGHT").Value)
            txtDigitalSignBottomRight.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_BOTTOMRIGHT").Value), 0, RseWaySetup.Fields("DS_BOTTOMRIGHT").Value)
            txtDSAuthSign.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_AUTH_SIGN").Value), "", RseWaySetup.Fields("DS_AUTH_SIGN").Value)
            txtDSCertificateNo.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_CERTIFICATE_SNO").Value), "", RseWaySetup.Fields("DS_CERTIFICATE_SNO").Value)

            txtFontSize.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_FONT_SIZE").Value), 24, RseWaySetup.Fields("DS_FONT_SIZE").Value)
            txtFindAuth.Text = IIf(IsDBNull(RseWaySetup.Fields("DS_FIND_AUTH").Value), "Authorised Signatory", RseWaySetup.Fields("DS_FIND_AUTH").Value)

            Dim mFindAuthLoc As Long = 0
            mFindAuthLoc = IIf(IsDBNull(RseWaySetup.Fields("DS_FIND_AUTH_LOC").Value), 0, RseWaySetup.Fields("DS_FIND_AUTH_LOC").Value)

            If mFindAuthLoc = 0 Then
                optTop.Checked = True
                optBottom.Checked = False
            Else
                optBottom.Checked = True
                optTop.Checked = False
            End If



            mDSType = IIf(IsDBNull(RseWaySetup.Fields("DS_CERTIFICATE_TYPE").Value), "A", RseWaySetup.Fields("DS_CERTIFICATE_TYPE").Value)

            If mDSType = "A" Then
                optType(0).Checked = True
                optType(1).Checked = False
            Else
                optType(0).Checked = False
                optType(1).Checked = True
            End If

        Else

            txtUpdateURL.Text = ""
            txtGenerateURL.Text = ""
            txtCancelURL.Text = ""
            txtFatchURL.Text = ""
            txtGetByIDURL.Text = ""

            txtCDKey.Text = ""
            txtEFUserName.Text = ""
            txtEFPassword.Text = ""
            txtEWBUserName.Text = ""
            txtEWBPassword.Text = ""
            txtCreateURLWebtel.Text = ""
            txtConsilidationURLWebtel.Text = ""
            txtGetByDistanceURL.Text = ""
            txtGetPrintURL.Text = ""

            txtDigitalSignURL.Text = ""
            txtDigitalSignUID.Text = ""
            txtDigitalSignPassword.Text = ""
            txtDSAuthSign.Text = ""
            txtDigitalSignTopLeft.Text = ""
            txtDigitalSignBottomLeft.Text = ""
            txtDigitalSignTopRight.Text = ""
            txtDigitalSignBottomRight.Text = ""

            txtFontSize.Text = ""
            txtDSCertificateNo.Text = ""



            optTop.Checked = True
            optBottom.Checked = False

            txtFindAuth.Text = "Authorised Signatory"

            optType(0).Checked = True
            optType(1).Checked = False
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim xCode As Integer
        Dim optDsType As String
        Dim mFindAuthLoc As Long = 0


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        xCode = RsCompany.Fields("Company_Code").Value

        If MainClass.ValidateWithMasterTable(xCode, "Company_Code", "Company_Code", "GEN_EWAYSETUP_MST", PubDBCn, MasterNo) = True Then
            mADDMode = False
        Else
            mADDMode = True
        End If

        If optType(0).Checked = True Then
            optDsType = "A"
        Else
            optDsType = "T"
        End If

        If optTop.Checked = True Then
            mFindAuthLoc = 0
        Else
            mFindAuthLoc = 1
        End If



        If mADDMode = True Then
            SqlStr = "INSERT INTO GEN_EWAYSETUP_MST ( " & vbCrLf _
                & " COMPANY_CODE, " & vbCrLf _
                & " UPDATE_URL,  " & vbCrLf _
                & " GENERATE_URL, CANCEL_URL, FATCH_URL, " & vbCrLf _
                & " GETBYID_URL, CD_KEY, EF_USERNAME, " & vbCrLf _
                & " EF_PASSWORD, EWB_USERNAME, EWB_PASSWORD," & vbCrLf _
                & " CREATE_URL_WEBTEL, DISTANCE_URL, PRINT_URL, " & vbCrLf _
                & " DS_URL, DS_USERID, DS_PASSWORD, DS_TOPLEFT, " & vbCrLf _
                & " DS_BOTTOMLEFT, DS_TOPRIGHT, DS_BOTTOMRIGHT, DS_AUTH_SIGN, DS_CERTIFICATE_SNO, " & vbCrLf _
                & " DS_CERTIFICATE_TYPE, CONSOL_URL_WEBTEL,DS_FONT_SIZE, DS_FIND_AUTH, DS_FIND_AUTH_LOC"


            SqlStr = SqlStr & vbCrLf & " ) VALUES ( "
            SqlStr = SqlStr & vbCrLf _
                    & " " & xCode & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtUpdateURL.Text) & "','" & MainClass.AllowSingleQuote(txtGenerateURL.Text) & "','" & MainClass.AllowSingleQuote(txtCancelURL.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtFatchURL.Text) & "', '" & MainClass.AllowSingleQuote(txtGetByIDURL.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCDKey.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtEFUserName.Text) & "','" & MainClass.AllowSingleQuote(txtEFPassword.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtEWBUserName.Text) & "','" & MainClass.AllowSingleQuote(txtEWBPassword.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCreateURLWebtel.Text) & "','" & MainClass.AllowSingleQuote(txtGetByDistanceURL.Text) & "','" & MainClass.AllowSingleQuote(txtGetPrintURL.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDigitalSignURL.Text) & "', '" & MainClass.AllowSingleQuote(txtDigitalSignUID.Text) & "', '" & MainClass.AllowSingleQuote(txtDigitalSignPassword.Text) & "'," & vbCrLf _
                    & " " & Val(txtDigitalSignTopLeft.Text) & ", " & Val(txtDigitalSignBottomLeft.Text) & ", " & Val(txtDigitalSignTopRight.Text) & ", " & Val(txtDigitalSignBottomRight.Text) & ", '" & MainClass.AllowSingleQuote(txtDSAuthSign.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDSCertificateNo.Text) & "','" & optDsType & "','" & MainClass.AllowSingleQuote(txtConsilidationURLWebtel.Text) & "'," & Val(txtFontSize.Text) & ",'" & MainClass.AllowSingleQuote(txtFindAuth.Text) & "', " & mFindAuthLoc & ")"



        Else
            SqlStr = "UPDATE  GEN_EWAYSETUP_MST SET DS_FONT_SIZE=" & Val(txtFontSize.Text) & "," & vbCrLf _
                    & " UPDATE_URL= '" & MainClass.AllowSingleQuote(txtUpdateURL.Text) & "', DS_AUTH_SIGN='" & MainClass.AllowSingleQuote(txtDSAuthSign.Text) & "'," & vbCrLf _
                    & " GENERATE_URL= '" & MainClass.AllowSingleQuote(txtGenerateURL.Text) & "', CONSOL_URL_WEBTEL= '" & MainClass.AllowSingleQuote(txtConsilidationURLWebtel.Text) & "'," & vbCrLf _
                    & " CANCEL_URL= '" & MainClass.AllowSingleQuote(txtCancelURL.Text) & "'," & vbCrLf _
                    & " FATCH_URL= '" & MainClass.AllowSingleQuote(txtFatchURL.Text) & "'," & vbCrLf _
                    & " GETBYID_URL= '" & MainClass.AllowSingleQuote(txtGetByIDURL.Text) & "'," & vbCrLf _
                    & " CD_KEY='" & MainClass.AllowSingleQuote(txtCDKey.Text) & "',DS_FIND_AUTH='" & MainClass.AllowSingleQuote(txtFindAuth.Text) & "', DS_FIND_AUTH_LOC= " & mFindAuthLoc & "," & vbCrLf _
                    & " EF_USERNAME='" & MainClass.AllowSingleQuote(txtEFUserName.Text) & "',EF_PASSWORD='" & MainClass.AllowSingleQuote(txtEFPassword.Text) & "'," & vbCrLf _
                    & " EWB_USERNAME='" & MainClass.AllowSingleQuote(txtEWBUserName.Text) & "',EWB_PASSWORD='" & MainClass.AllowSingleQuote(txtEWBPassword.Text) & "'," & vbCrLf _
                    & " CREATE_URL_WEBTEL='" & MainClass.AllowSingleQuote(txtCreateURLWebtel.Text) & "',DISTANCE_URL='" & MainClass.AllowSingleQuote(txtGetByDistanceURL.Text) & "',PRINT_URL='" & MainClass.AllowSingleQuote(txtGetPrintURL.Text) & "', " & vbCrLf _
                    & " DS_URL='" & MainClass.AllowSingleQuote(txtDigitalSignURL.Text) & "', DS_USERID='" & MainClass.AllowSingleQuote(txtDigitalSignUID.Text) & "', DS_PASSWORD='" & MainClass.AllowSingleQuote(txtDigitalSignPassword.Text) & "', DS_TOPLEFT=" & Val(txtDigitalSignTopLeft.Text) & ", " & vbCrLf _
                    & " DS_CERTIFICATE_SNO='" & MainClass.AllowSingleQuote(txtDSCertificateNo.Text) & "', DS_CERTIFICATE_TYPE='" & optDsType & "' ," & vbCrLf _
                    & " DS_BOTTOMLEFT=" & Val(txtDigitalSignBottomLeft.Text) & ", DS_TOPRIGHT=" & Val(txtDigitalSignTopRight.Text) & ", DS_BOTTOMRIGHT=" & Val(txtDigitalSignBottomRight.Text) & ""

            SqlStr = SqlStr & vbCrLf & " WHERE Company_Code=" & xCode & ""

        End If

        PubDBCn.Execute(SqlStr)

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
    Private Sub frmeWayBillSetup_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        FormActive = False
        RseWaySetup = Nothing
    End Sub
    Private Function FieldVerification() As Boolean
        On Error GoTo ERR1
        FieldVerification = True

        '    If Trim(txtCompanyId.Text) = "" Then
        '        MsgInformation "Company Id cann't be blank."
        '        txtCompanyId.SetFocus
        '        FieldVerification = False
        '        Exit Function
        '    End If
        '
        '    If Trim(txtBranchId.Text) = "" Then
        '        MsgInformation "Branch Id cann't be blank."
        '        txtBranchId.SetFocus
        '        FieldVerification = False
        '        Exit Function
        '    End If
        '
        '    If Trim(txtToken.Text) = "" Then
        '        MsgInformation "Token Id cann't be blank."
        '        txtToken.SetFocus
        '        FieldVerification = False
        '        Exit Function
        '    End If

        '    If Trim(txtUserId.Text) = "" Then
        '        MsgInformation "User Id cann't be blank."
        '        txtUserId.SetFocus
        '        FieldVerification = False
        '        Exit Function
        '    End If
        '
        '    If Trim(txtCreateURL.Text) = "" Then
        '        MsgInformation "Create URL cann't be blank."
        '        txtCreateURL.SetFocus
        '        FieldVerification = False
        '        Exit Function
        '    End If

        'If Trim(txtUpdateURL.Text) = "" Then
        '   MsgInformation("Update URL cann't be blank.")
        '   txtUpdateURL.Focus()
        '   FieldVerification = False
        '   Exit Function
        'End If

        'If Trim(txtGenerateURL.Text) = "" Then
        '   MsgInformation("Generate URL cann't be blank.")
        '   txtGenerateURL.Focus()
        '   FieldVerification = False
        '   Exit Function
        'End If

        'If Trim(txtCancelURL.Text) = "" Then
        '   MsgInformation("Cancel URL cann't be blank.")
        '   txtCancelURL.Focus()
        '   FieldVerification = False
        '   Exit Function
        'End If

        'If Trim(txtFatchURL.Text) = "" Then
        '   MsgInformation("Fatch URL cann't be blank.")
        '   txtFatchURL.Focus()
        '   FieldVerification = False
        '   Exit Function
        'End If

        'If Trim(txtGetByIDURL.Text) = "" Then
        '   MsgInformation("Get By ID URL cann't be blank.")
        '   txtGetByIDURL.Focus()
        '   FieldVerification = False
        '   Exit Function
        'End If


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

    Private Sub txtCreateURLWebtel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreateURLWebtel.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtConsilidationURLWebtel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtConsilidationURLWebtel.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEFPassword_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEFPassword.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEFUserName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEFUserName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEWBPassword_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEWBPassword.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEWBUserName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEWBUserName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtFatchURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFatchURL.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGenerateURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGenerateURL.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGetByDistanceURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGetByDistanceURL.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGetByIDURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGetByIDURL.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGetPrintURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGetPrintURL.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtUpdateURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUpdateURL.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDigitalSignTopLeft_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignTopLeft.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDigitalSignBottomLeft_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignBottomLeft.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDigitalSignTopRight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignTopRight.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDigitalSignBottomRight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignBottomRight.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDigitalSignPassword_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignPassword.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDigitalSignUID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignUID.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDigitalSignURL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignURL.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDigitalSignBottomLeft_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDigitalSignBottomLeft.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)

        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDigitalSignBottomRight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDigitalSignBottomRight.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)

        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDigitalSignTopLeft_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDigitalSignTopLeft.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)

        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDigitalSignTopRight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDigitalSignTopRight.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)

        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDSAuthSign_TextChanged(sender As Object, e As EventArgs) Handles txtDSAuthSign.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSCertificateNo_TextChanged(sender As Object, e As EventArgs) Handles txtDSCertificateNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub optType_Click(sender As Object, e As EventArgs) Handles optType.Click
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtFontSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFontSize.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtFontSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFontSize.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)

        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtFindAuth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFindAuth.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
End Class