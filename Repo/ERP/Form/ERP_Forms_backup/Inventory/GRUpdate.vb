Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmGRUpdate
    Inherits System.Windows.Forms.Form
    Dim RsSaleGRMain As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    Dim SqlStr As String = ""
    Private Const ConRowHeight As Short = 12
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If lblType.Text = "S" Then
            If UpdateMain1 = True Then
                Me.Close()
            Else
                MsgInformation("Record not saved")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        Else
            If UpdateGMain1 = True Then
                Me.Close()
            Else
                MsgInformation("Record not saved")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If

        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FrmGRUpdate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
    End Sub


    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim CntBillNo As Integer
        Dim mBillNo As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mGRNo As String
        Dim mGRDate As String
        Dim pGRDate As String
        Dim mTransBillDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        'For CntBillNo = Val(txtBillNo.Text) To Val(txtBillNoTo.Text)
        '    mBillNo = txtBillNoPrefix.Text & VB6.Format(CntBillNo, "00000")

        SqlStr = "SELECT GRNO,GRDATE FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMKey.Text & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mGRNo = IIf(IsDbNull(RsTemp.Fields("GRNO").Value), "", RsTemp.Fields("GRNO").Value)
                mGRDate = IIf(IsDbNull(RsTemp.Fields("GRDATE").Value), "", RsTemp.Fields("GRDATE").Value)
            'If Trim(mGRNo) <> "" Then
            '    If MsgQuestion("On Bill No. " & mBillNo & ", GR Already Entered. GR No is :" & mGRNo & ". You Want to Continue ...") = CStr(MsgBoxResult.No) Then
            '        GoTo NextBill
            '    End If
            'End If
        End If
            pGRDate = IIf(TxtGRDate.Text = "__/__/____", "", TxtGRDate.Text)
            mTransBillDate = IIf(txtTransporterBillDate.Text = "__/__/____", "", txtTransporterBillDate.Text)

            SqlStr = ""
            SqlStr = "UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " GRNO= '" & MainClass.AllowSingleQuote(TxtGRNo.Text) & "', " & vbCrLf & " GRDATE= TO_DATE('" & VB6.Format(pGRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " VEHICLENO= '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " CARRIERS=  '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " TRANSPORTERBILLNO= '" & MainClass.AllowSingleQuote(txtTransporterBillNo.Text) & "', " & vbCrLf & " TRANSPORTERBILLDATE= TO_DATE('" & VB6.Format(mTransBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " GRAMOUNT=" & Val(txtGRAmount.Text) & ", UPDATE_FROM='N'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMKey.Text & "'"

            PubDBCn.Execute(SqlStr)
NextBill:
        'Next CntBillNo


        UpdateMain1 = True

        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function

    Private Function UpdateGMain1() As Boolean

        On Error GoTo ErrPart
        Dim CntBillNo As Double
        Dim mBillNo As Double
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mGRNo As String
        Dim mGRDate As String
        Dim pGRDate As String
        Dim mTransBillDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        For CntBillNo = CInt(Mid(CStr(Val(txtBillNo.Text)), 1, Len(txtBillNo.Text) - 6)) To CInt(Mid(CStr(Val(txtBillNoTo.Text)), 1, Len(txtBillNoTo.Text) - 6)) ''Val(txtBillNoTo.Text)
            mBillNo = CDbl(CntBillNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))

            SqlStr = "SELECT GRNO,GRDATE FROM INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO=" & mBillNo & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mGRNo = IIf(IsDbNull(RsTemp.Fields("GRNO").Value), "", RsTemp.Fields("GRNO").Value)
                mGRDate = IIf(IsDbNull(RsTemp.Fields("GRDATE").Value), "", RsTemp.Fields("GRDATE").Value)
                If Trim(mGRNo) <> "" Then
                    If MsgQuestion("On Bill No. " & mBillNo & ", GR Already Entered. GR No is :" & mGRNo & ". You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                        GoTo NextBill
                    End If
                End If
            End If
            pGRDate = IIf(TxtGRDate.Text = "__/__/____", "", TxtGRDate.Text)
            mTransBillDate = IIf(txtTransporterBillDate.Text = "__/__/____", "", txtTransporterBillDate.Text)

            SqlStr = ""
            SqlStr = "UPDATE INV_GATEPASS_HDR SET " & vbCrLf & " GRNO= '" & MainClass.AllowSingleQuote(TxtGRNo.Text) & "', " & vbCrLf & " GRDATE= TO_DATE('" & VB6.Format(pGRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " VEHICLE_NO= '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " CARRIERS=  '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " TRANSPORTERBILLNO= '" & MainClass.AllowSingleQuote(txtTransporterBillNo.Text) & "', " & vbCrLf & " TRANSPORTERBILLDATE= TO_DATE('" & VB6.Format(mTransBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " GRAMOUNT=" & Val(txtGRAmount.Text) & "," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO=" & mBillNo & ""

            PubDBCn.Execute(SqlStr)
NextBill:
        Next CntBillNo


        UpdateGMain1 = True

        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateGMain1 = False
        PubDBCn.RollbackTrans() ''
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True

        If txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            txtBillNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If txtBillNoTo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            txtBillNoTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtGRNo.Text) = "" Then
            MsgBox("GR No is blank.", MsgBoxStyle.Critical)
            FieldsVarification = False
            TxtGRNo.Focus()
            Exit Function
        End If

        If IsDate(TxtGRDate.Text) = False Then         ''If Trim(TxtGRDate.Text) = "__/__/____" Then
            MsgBox("GR Date is blank.", MsgBoxStyle.Critical)
            FieldsVarification = False
            TxtGRDate.Focus()
            Exit Function
        End If

        'If TxtGRDate.Text <> "__/__/____" Then
        '    If FYChk((TxtGRDate.Text)) = False Then
        '        FieldsVarification = False
        '        TxtGRDate.Focus()
        '        Exit Function
        '    End If
        'End If


        If IsDate(txtTransporterBillDate.Text) = False Then         ''If Trim(txtTransporterBillDate.Text) = "__/__/____" Then
            MsgBox("Transporter Bill Date is blank.", MsgBoxStyle.Critical)
            FieldsVarification = False
            txtTransporterBillDate.Focus()
            Exit Function
        End If

        If CDate(txtTransporterBillDate.Text) < CDate(TxtGRDate.Text) Then
            MsgBox("Transporter Bill Date cann't be less than GR Date.", MsgBoxStyle.Critical)
            FieldsVarification = False
            txtTransporterBillDate.Focus()
            Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Public Sub FrmGRUpdate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from FIN_INVOICE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleGRMain, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()
        Call Clear1()
        If lblType.Text = "S" Then
            Call Show1()
        Else
            Call Show_G1()
        End If

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsSaleGRMain
            txtBillNoPrefix.Maxlength = .Fields("BillNoPrefix").DefinedSize ''
            txtBillNo.Maxlength = .Fields("AUTO_KEY_INVOICE").Precision ''

            txtBillNoPrefixTo.Maxlength = .Fields("BillNoPrefix").DefinedSize ''
            txtBillNoTo.Maxlength = .Fields("AUTO_KEY_INVOICE").Precision

            txtBillNoSuffix.MaxLength = .Fields("BillNoSuffix").DefinedSize ''
            txtBillNoSuffixTo.MaxLength = .Fields("BillNoSuffix").DefinedSize ''

            TxtGRNo.Maxlength = .Fields("GRNo").DefinedSize ''
            TxtGRDate.MaxLength = 10
            txtCarriers.Maxlength = .Fields("CARRIERS").DefinedSize ''
            txtVehicle.Maxlength = .Fields("VehicleNo").DefinedSize ''
            txtTransporterBillNo.Maxlength = .Fields("TRANSPORTERBILLNO").DefinedSize ''
            txtTransporterBillDate.MaxLength = 10
            txtGRAmount.Maxlength = .Fields("GRAMOUNT").Precision
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mBillNo As String

        mBillNo = Trim(txtBillNoPrefix.Text) & Trim(txtBillNo.Text) & Trim(txtBillNoSuffix.Text)

        SqlStr = "SELECT * FROM FIN_INVOICE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR= '" & RsCompany.Fields("FYEAR").Value & "' AND MKEY='" & lblMKey.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleGRMain, ADODB.LockTypeEnum.adLockReadOnly)

        With RsSaleGRMain
            If Not .EOF Then

                txtBillNoPrefix.Text = IIf(IsDbNull(.Fields("BILLNOPREFIX").Value), "", .Fields("BILLNOPREFIX").Value)
                txtBillNo.Text = VB6.Format(IIf(IsDbNull(.Fields("BILLNOSEQ").Value), "", .Fields("BILLNOSEQ").Value), "00000")
                txtBillNoSuffix.Text = IIf(IsDBNull(.Fields("BILLNOSUFFIX").Value), "", .Fields("BILLNOSUFFIX").Value)

                TxtGRNo.Text = IIf(IsDbNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                TxtGRDate.Text = IIf(IsDbNull(.Fields("GRDATE").Value), "__/__/____", .Fields("GRDATE").Value)
                txtCarriers.Text = IIf(IsDbNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)
                txtVehicle.Text = IIf(IsDbNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                txtTransporterBillNo.Text = IIf(IsDbNull(.Fields("TRANSPORTERBILLNO").Value), "", .Fields("TRANSPORTERBILLNO").Value)
                txtTransporterBillDate.Text = IIf(IsDbNull(.Fields("TRANSPORTERBILLDATE").Value), "__/__/____", .Fields("TRANSPORTERBILLDATE").Value)
                txtGRAmount.Text = IIf(IsDbNull(.Fields("GRAMOUNT").Value), "", .Fields("GRAMOUNT").Value)
            End If
        End With
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub Show_G1()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mBillNo As Double

        mBillNo = Val(txtBillNo.Text)

        SqlStr = "SELECT * FROM INV_GATEPASS_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO ='" & mBillNo & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleGRMain, ADODB.LockTypeEnum.adLockReadOnly)

        With RsSaleGRMain
            If Not .EOF Then

                txtBillNoPrefix.Text = ""
                txtBillNoSuffix.Text = ""
                txtBillNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_PASSNO").Value), "", .Fields("AUTO_KEY_PASSNO").Value)

                TxtGRNo.Text = IIf(IsDbNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                TxtGRDate.Text = IIf(IsDbNull(.Fields("GRDATE").Value), .Fields("GATEPASS_DATE").Value, .Fields("GRDATE").Value)
                txtCarriers.Text = IIf(IsDbNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)
                txtVehicle.Text = IIf(IsDbNull(.Fields("VEHICLE_NO").Value), "", .Fields("VEHICLE_NO").Value)
                txtTransporterBillNo.Text = IIf(IsDbNull(.Fields("TRANSPORTERBILLNO").Value), "", .Fields("TRANSPORTERBILLNO").Value)
                txtTransporterBillDate.Text = IIf(IsDbNull(.Fields("TRANSPORTERBILLDATE").Value), "__/__/____", .Fields("TRANSPORTERBILLDATE").Value)
                txtGRAmount.Text = IIf(IsDbNull(.Fields("GRAMOUNT").Value), "", .Fields("GRAMOUNT").Value)
            End If
        End With
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub Clear1()
        '    LblMKey.text = ""
        '
        If lblType.Text = "S" Then
            txtBillNoPrefix.Text = ""
            txtBillNoSuffix.Text = ""
            '    txtBillNo.Text = ""

            txtBillNoPrefixTo.Text = ""
            txtBillNoSuffixTo.Text = ""
            '    txtBillNoTo.Text = ""
        Else
            txtBillNoPrefix.Text = ""
            txtBillNoPrefixTo.Text = ""
            txtBillNoSuffix.Text = ""
            txtBillNoSuffixTo.Text = ""
        End If

        TxtGRNo.Text = ""
        TxtGRDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtCarriers.Text = ""
        txtVehicle.Text = ""
        txtGRAmount.Text = ""
    End Sub

    Private Sub FrmGRUpdate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub FrmGRUpdate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn


        MainClass.SetControlsColor(Me)
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        'Me.Height = VB6.TwipsToPixelsY(4665)
        ''Me.Width = VB6.TwipsToPixelsX(5910)

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBillNoTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNoTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCarriers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriers.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGRAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGRAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtGRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtGRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If TxtGRDate.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(TxtGRDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtGRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtGRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtGRNo.Text) '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTransporterBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTransporterBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtTransporterBillDate.Text = "__/__/____" Then GoTo EventExitSub
        If IsDate(txtTransporterBillDate.Text) = False Then
            ErrorMsg("Invalid Date", "", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVehicle_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtVehicle.Text), "FIN_Vehicle_MST", "NAME", , , , SqlStr) = True Then
            txtVehicle.Text = AcName
            If txtVehicle.Enabled = True Then txtVehicle.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtVehicle_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicle.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtVehicle_DoubleClick(txtVehicle, New System.EventArgs())
    End Sub
    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
