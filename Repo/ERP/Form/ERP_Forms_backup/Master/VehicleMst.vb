Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmVehicleMst
    Inherits System.Windows.Forms.Form
    Dim RsVehicle As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
   'Private PvtDBCn As ADODB.Connection		

   Dim xCode As Integer
   Dim FormActive As Boolean
   Dim Shw As Boolean
   Dim MasterNo As Object
    Dim SqlStr As String = ""
    Private Sub ViewGrid()

        On Error GoTo ErrorPart
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh			
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsVehicle, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""

        txtCapacity.Text = ""
        txtVehicle_Height.Text = ""
        txtVehicle_Width.Text = ""
        txtVehicle_Len.Text = ""
        cboVehicleOwner.SelectedIndex = -1


        cboTransportName.Items.Clear()
        MainClass.FillCombo(cboTransportName, "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        cboVehicleType.Items.Clear()
        MainClass.FillCombo(cboVehicleType, "FIN_VEHICLETYPE_MST", "NAME", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        cboTransportName.SelectedIndex = -1
        cboVehicleType.SelectedIndex = -1

        Call AutoCompleteSearch("FIN_VEHICLE_MST", "NAME", "", txtName)

        MainClass.ButtonStatus(Me, XRIGHT, RsVehicle, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboTransportName_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cboTransportName.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboTransportName.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub
    Private Sub cboTransportName_TextChanged(sender As Object, e As System.EventArgs) Handles cboTransportName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVehicleOwner_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboVehicleOwner.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVehicleOwner_TextChanged(sender As Object, e As System.EventArgs) Handles cboVehicleOwner.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVehicleType_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cboVehicleType.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboVehicleType.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub cboVehicleType_TextChanged(sender As Object, e As System.EventArgs) Handles cboVehicleType.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsVehicle, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(sender As Object, e As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReportOld(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(sender As Object, e As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub CmdAdd_Click(sender As Object, e As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo ERR1
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdClose_Click(sender As Object, e As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "FIN_VEHICLE_MST", (txtName.Text), RsVehicle, "NAME") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_VEHICLE_MST", "NAME", (txtName.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM FIN_VEHICLE_MST " & vbCrLf _
                & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & "AND Name='" & MainClass.AllowSingleQuote(UCase((txtName.Text))) & "'"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsVehicle.Requery() ''.Refresh			
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''			
        RsVehicle.Requery() ''.Refresh			
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdDelete_Click(sender As Object, e As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsVehicle.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.			
                If Delete1() = False Then GoTo DelErrPart
                If RsVehicle.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        ErrorMsg("Record Not Deleted", "DELETE", MsgBoxStyle.Critical)
    End Sub

    Private Sub frmVehicleMst_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_VEHICLE_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVehicle, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then CmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmVehicleMst_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsVehicle = Nothing
        RsVehicle.Close()
    End Sub

    Private Sub frmVehicleMst_KeyDown(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmVehicleMst_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")

        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(sender As Object, e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtName.Text = Trim(SprdView.Text)
        TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(sender As Object, eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If EventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtCapacity_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCapacity.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCapacity_TextChanged(sender As Object, e As System.EventArgs) Handles txtCapacity.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtName_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text, "N")
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtName_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As System.EventArgs) Handles txtName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtName_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsVehicle.EOF = False Then xCode = RsVehicle.Fields("CODE").Value

        SqlStr = "SELECT * FROM FIN_VEHICLE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtName.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVehicle, ADODB.LockTypeEnum.adLockReadOnly)

        If RsVehicle.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Item Type Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM FIN_VEHICLE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & xCode & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVehicle, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub

ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub frmVehicleMst_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection			
        ''PvtDBCn.Open StrConn			

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5220)
        ''Me.Width = VB6.TwipsToPixelsX(8265)

        cboVehicleOwner.Items.Clear()
        cboVehicleOwner.Items.Add("1. COMPANY VEHICLE")
        cboVehicleOwner.Items.Add("2. TRANSPOTER")
        cboVehicleOwner.Items.Add("3. PARTY VEHICLE")
        cboVehicleOwner.SelectedIndex = -1

        cboTransportName.Items.Clear()
        cboVehicleType.Items.Clear()

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mVehicleOwner As String

        If Not RsVehicle.EOF Then

            txtName.Text = IIf(IsDbNull(RsVehicle.Fields("Name").Value), "", RsVehicle.Fields("Name").Value)
            cboTransportName.Text = IIf(IsDbNull(RsVehicle.Fields("TRANSPORTER_NAME").Value), "", RsVehicle.Fields("TRANSPORTER_NAME").Value)


            cboVehicleType.Text = IIf(IsDbNull(RsVehicle.Fields("VEHICLE_TYPE").Value), "", RsVehicle.Fields("VEHICLE_TYPE").Value)
            txtCapacity.Text = IIf(IsDbNull(RsVehicle.Fields("VEHICLE_CAPACITY").Value), "", RsVehicle.Fields("VEHICLE_CAPACITY").Value)
            txtVehicle_Height.Text = IIf(IsDbNull(RsVehicle.Fields("VEHICLE_HEIGHT").Value), "", RsVehicle.Fields("VEHICLE_HEIGHT").Value)
            txtVehicle_Width.Text = IIf(IsDbNull(RsVehicle.Fields("VEHICLE_WIDTH").Value), "", RsVehicle.Fields("VEHICLE_WIDTH").Value)
            txtVehicle_Len.Text = IIf(IsDbNull(RsVehicle.Fields("VEHICLE_LEN").Value), "", RsVehicle.Fields("VEHICLE_LEN").Value)

            mVehicleOwner = IIf(IsDbNull(RsVehicle.Fields("VEHICLE_OWNER").Value), "", RsVehicle.Fields("VEHICLE_OWNER").Value)
            If mVehicleOwner = "1" Then
                cboVehicleOwner.SelectedIndex = 0
            ElseIf mVehicleOwner = "2" Then
                cboVehicleOwner.SelectedIndex = 1
            ElseIf mVehicleOwner = "3" Then
                cboVehicleOwner.SelectedIndex = 2
            End If


            xCode = RsVehicle.Fields("Code").Value
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsVehicle, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume			
    End Sub

    Private Sub CmdSave_Click(sender As Object, e As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mCode As Integer
        Dim mSalesPostCode As String
        Dim Identification As String
        Dim mSalesTaxCode As Integer
        Dim mStatus As String '***			
        Dim mWef As Date '**			
        Dim mVehicleOwner As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mVehicleOwner = VB.Left(cboVehicleOwner.Text, 1)

        SqlStr = ""
        If ADDMode = True Then
            mCode = MainClass.AutoGenRowNo("FIN_VEHICLE_MST", "Code", PubDBCn)
            SqlStr = "INSERT INTO FIN_VEHICLE_MST (" & vbCrLf & " COMPANY_CODE, CODE, NAME,TRANSPORTER_NAME, " & vbCrLf & " VEHICLE_TYPE, VEHICLE_CAPACITY, " & vbCrLf & " VEHICLE_HEIGHT, VEHICLE_WIDTH, VEHICLE_LEN,VEHICLE_OWNER," & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE" & vbCrLf & " ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCode & ", '" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(cboTransportName.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(cboVehicleType.Text) & "', " & Val(txtCapacity.Text) & ", " & vbCrLf & " " & Val(txtVehicle_Height.Text) & ", " & Val(txtVehicle_Width.Text) & ", " & Val(txtVehicle_Len.Text) & ",'" & mVehicleOwner & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"

        Else
            SqlStr = " UPDATE FIN_VEHICLE_MST  SET " & vbCrLf & " NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf & " TRANSPORTER_NAME='" & MainClass.AllowSingleQuote(cboTransportName.Text) & "'," & vbCrLf & " VEHICLE_TYPE='" & MainClass.AllowSingleQuote(cboVehicleType.Text) & "'," & vbCrLf & " VEHICLE_CAPACITY=" & Val(txtCapacity.Text) & "," & vbCrLf & " VEHICLE_HEIGHT=" & Val(txtVehicle_Height.Text) & "," & vbCrLf & " VEHICLE_WIDTH=" & Val(txtVehicle_Width.Text) & ",VEHICLE_OWNER='" & mVehicleOwner & "'," & vbCrLf & " VEHICLE_LEN=" & Val(txtVehicle_Len.Text) & "," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE= " & xCode & ""
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''			
        RsVehicle.Requery() ''.Refresh			
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtName.Maxlength = RsVehicle.Fields("Name").DefinedSize ''			
        '    txtTransporterName.MaxLength = RsVehicle.Fields("TRANSPORTER_NAME").DefinedSize			

        '    txtVehicleType.MaxLength = RsVehicle.Fields("VEHICLE_TYPE").DefinedSize			
        txtCapacity.Maxlength = RsVehicle.Fields("VEHICLE_CAPACITY").Precision
        txtVehicle_Height.Maxlength = RsVehicle.Fields("VEHICLE_HEIGHT").Precision
        txtVehicle_Width.Maxlength = RsVehicle.Fields("VEHICLE_WIDTH").Precision
        txtVehicle_Len.Maxlength = RsVehicle.Fields("VEHICLE_LEN").Precision

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtName.Text) = "" Then
            MsgInformation(" Name is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboTransportName.Text) = "" Then
            MsgInformation("Transporter Name is empty. Cannot Save")
            cboTransportName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtCapacity.Text) > 999 Then
            MsgInformation("Capacity is Maximum 999 Ton. Cannot Save")
            txtCapacity.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtVehicle_Len.Text) > 999 Then
            MsgInformation("Please Check Lenght Size. Cannot Save")
            txtVehicle_Len.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtVehicle_Width.Text) > 999 Then
            MsgInformation("Please Check Width Size. Cannot Save")
            txtVehicle_Width.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtVehicle_Height.Text) > 999 Then
            MsgInformation("Please Check Height Size. Cannot Save")
            txtVehicle_Height.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboVehicleOwner.Text) = "" Then
            MsgBox("Please Enter Vehicle Owner.", MsgBoxStyle.Information)
            cboVehicleOwner.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsVehicle.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT NAME, TRANSPORTER_NAME, VEHICLE_TYPE, VEHICLE_CAPACITY, VEHICLE_HEIGHT, VEHICLE_WIDTH, VEHICLE_LEN, DECODE(VEHICLE_OWNER,1,'COMPANY VEHICLE',DECODE(VEHICLE_OWNER,2,'TRANSPORTER','THIRD PARTY')) AS VEHICLE_OWNER" & vbCrLf & " FROM FIN_VEHICLE_MST" & vbCrLf & " WHERE FIN_VEHICLE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 20)
            .set_ColWidth(2, 12)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub ShowReportOld(ByRef Mode As Crystal.DestinationConstants)
        'On Error GoTo ErrPart			
        'Dim crapp As New CRAXDRT.Application			
        'Dim RsTemp As New ADODB.Recordset			
        'Dim RS As New ADODB.Recordset			
        '			
        'Dim objRpt As CRAXDRT.Report			
        'Dim fPath As String			
        'Dim mRPTName As String = ""			
        'Dim SqlStr As String = ""=""			
        '			
        'Dim mTitle As String = ""			
        'Dim mSubTitle As String = ""			
        '			
        '    mTitle = ""			
        '    mTitle = "Vehicle Master"			
        '			
        '    mRPTName = App.path & "\Reports\" & "VehicleMst.rpt"   ''    Report1.ReportFileName = App.path & " \ reports \ VehicleMst.rpt" ''VehicleMst			
        '			
        '    SqlStr = " SELECT * FROM FIN_VEHICLE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""			
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RS, adLockReadOnly			
        '			
        '    If RS.EOF = False Then			
        '			
        '                Set objRpt = crapp.OpenReport(mRPTName)			
        '			
        '                Call Connect_Report_To_Database(objRpt, RS, SqlStr)			
        '                With objRpt			
        '                    Call ClearCRpt8Formulas(objRpt)			
        '                    .DiscardSavedData			
        '                    .Database.SetDataSource RS			
        '                    SetCrpteMail objRpt, 1, mTitle, mSubTitle			
        '                    .VerifyOnEveryPrint = True  '' blnVerifyOnEveryPrint			
        '                End With			
        '			
        '                fPath = mLocalPath & "\VehicleMst" & ".pdf"			
        '			
        '                With objRpt			
        '                    .ExportOptions.FormatType = crEFTPortableDocFormat			
        '                    .ExportOptions.DestinationType = crEDTDiskFile			
        '                    .ExportOptions.DiskFileName = fPath			
        '                '    .ExportOptions.PDFExportAllPages = True			
        '                    .Export False			
        '                End With			
        '			
        ''                Set objRpt = crapp.CanClose			
        '                Set objRpt = Nothing			
        '			
        '			
        '            RsTemp.MoveNext			
        '    End If			
        '			
        Exit Sub
ErrPart:
        'Resume			
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Dim mTitle As String = ""			
        'On Error GoTo ERR1			
        '     mTitle = ""			
        '    Report1.Reset			
        '    mTitle = "Vehicle Master"			
        '    Report1.ReportFileName = App.path & "\reports\VehicleMst.rpt"			
        '    SetCrpt Report1, Mode, 1, mTitle			
        '			
        '    Report1.WindowShowGroupTree = False			
        '			
        '    Report1.Action = 1			
        '    Report1.ReportFileName = ""			
        '			
        'Exit Sub			
        'ERR1:			
        '    MsgInformation err.Description			
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & "VehicleMst.rpt"
        ' Name from label on sample form			

        ' Discard saved data?			
        '    If MsgBox("Do you wish to discard any saved data?", vbYesNo + vbQuestion, "Discard Saved Data?") = vbYes Then			
        Report1.DiscardSavedData = 1
        '    End If			

        ' Display progress dialog?			
        If MsgBox("Do you want to see the progress dialog?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Display Progress Dialog?") = MsgBoxResult.Yes Then
            Report1.ProgressDialog = True
        Else
            Report1.ProgressDialog = False
        End If

        Report1.Destination = Mode

        ' Display Windows printer selection dialog			
        Report1.PrinterSelect()

        ' Print			
        Report1.Action = 1

        MsgBox("Print Complete!", MsgBoxStyle.OKOnly, "Operation Completed")

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtVehicle_Height_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle_Height.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVehicle_Height_TextChanged(sender As Object, e As System.EventArgs) Handles txtVehicle_Height.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVehicle_Len_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle_Len.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVehicle_Len_TextChanged(sender As Object, e As System.EventArgs) Handles txtVehicle_Len.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVehicle_Width_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle_Width.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVehicle_Width_TextChanged(sender As Object, e As System.EventArgs) Handles txtVehicle_Width.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class

