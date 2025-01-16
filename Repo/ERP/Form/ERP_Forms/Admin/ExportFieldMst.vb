Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmExportFieldMst
    Inherits System.Windows.Forms.Form
    Dim RsBillExp As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Private PvtDBCn As ADODB.Connection	


    Private Const IdLOADINGPORT As String = "LOADINGPORT"
    Private Const IdLTYPE As String = "LTYPE"
    Private Const IdPARTDESCR As String = "PARTDESCR"
    Private Const IdSHIPCONTACT As String = "SHIPCONTACT"
    Private Const IdBUYERCONTACT As String = "BUYERCONTACT"
    Private Const IdFINALDESTINATION As String = "FINALDESTINATION"
    Private Const IdREJREM As String = "REJREM"
    Private Const IdTRANSHIPMENT As String = "TRANSHIPMENT"
    Private Const IdRTYPE As String = "RTYPE"
    Private Const IdCOUNTRYDESTINATION As String = "COUNTRYDESTINATION"
    Private Const IdDISCHARGEPORT As String = "DISCHARGEPORT"
    Private Const IdFOB2 As String = "FOB2"
    Private Const IdDELIVERYTERMS As String = "DELIVERYTERMS"
    Private Const IdGOODSTYPE As String = "GOODSTYPE"
    Private Const IdPARTIALSHIPMENT As String = "PARTIALSHIPMENT"
    Private Const IdPORTNO As String = "PORTNO"
    Private Const IdTERMSOFPAYMENTS As String = "TERMSOFPAYMENTS"
    Private Const IdTPT As String = "TPT"
    Private Const IdLICNO As String = "LICNO"
    Private Const IdSTYPE As String = "STYPE"
    Private Const IdGOODSTYPEHEAD As String = "GOODSTYPEHEAD"

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
        MainClass.ButtonStatus(Me, XRIGHT, RsBillExp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)

        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""
        CboIdentification.Text = ""


        MainClass.ButtonStatus(Me, XRIGHT, RsBillExp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub CboIdentification_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CboIdentification.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboIdentification_TextChanged(sender As Object, e As System.EventArgs) Handles CboIdentification.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsBillExp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
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
        ShowReport(Crystal.DestinationConstants.crptToWindow)
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
        Me.Dispose()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "FIN_EXPORT_FIELD_MST", (txtName.Text), RsBillExp, "FIELD_VALUE", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_EXPORT_FIELD_MST", "FIELD_VALUE", (txtName.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM FIN_EXPORT_FIELD_MST " & vbCrLf _
              & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
              & "AND FIELD_NAME='" & MainClass.AllowSingleQuote(CboIdentification.Text) & "' " & vbCrLf _
              & "AND FIELD_VALUE='" & MainClass.AllowSingleQuote(UCase((txtName.Text))) & "' "

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsBillExp.Requery() '' .Refresh		
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''		
        RsBillExp.Requery() ''.Refresh		
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdDelete_Click(sender As Object, e As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsBillExp.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.		
                If Delete1() = False Then GoTo DelErrPart
                If RsBillExp.EOF = True Then
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

    Private Sub cmdsearch_Click(sender As Object, e As System.EventArgs)
        On Error GoTo SearchError
        Dim SqlStr As String = ""

        ''FIELD_NAME, FIELD_VALUE

        'If MainClass.SearchMaster(txtName.Text, "FIN_EXPORT_FIELD_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y'") = True Then
        If MainClass.SearchGridMaster(txtName.Text, "FIN_EXPORT_FIELD_MST", "NAME", "NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y'") = True Then
            txtName.Text = AcName
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            CboIdentification.Focus()
        End If

        Exit Sub
SearchError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmExportFieldMst_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_EXPORT_FIELD_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        FillIdentification()
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmExportFieldMst_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsBillExp = Nothing
        RsBillExp.Close()
    End Sub

    Private Sub frmExportFieldMst_KeyDown(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmExportFieldMst_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub
    Private Sub SprdView_DblClick(sender As Object, e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        txtName.Text = Trim(SprdView.Text)
        txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(sender As Object, EventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If EventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))

    End Sub

    Private Sub txtName_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtName_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " SELECT FIELD_NAME,FIELD_VALUE "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_EXPORT_FIELD_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY FIELD_NAME,FIELD_VALUE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As System.EventArgs) Handles txtName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtName_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        Dim xFieldValue As String

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsBillExp.EOF = False Then xFieldValue = RsBillExp.Fields("FIELD_VALUE").Value

        SqlStr = "SELECT * FROM FIN_EXPORT_FIELD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FIELD_NAME='" & MainClass.AllowSingleQuote(CboIdentification.Text) & "'" & vbCrLf _
            & " AND FIELD_VALUE='" & MainClass.AllowSingleQuote(UCase((Trim(txtName.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBillExp.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM FIN_EXPORT_FIELD_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND FIELD_VALUE='" & xFieldValue & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBillExp, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub


    Private Sub frmExportFieldMst_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
        'Me.Height = VB6.TwipsToPixelsY(6360)
        ''Me.Width = VB6.TwipsToPixelsX(8220)
        Call frmExportFieldMst_Activated(sender, e)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsBillExp.EOF Then
            txtName.Text = IIf(IsDBNull(RsBillExp.Fields("Name").Value), "", RsBillExp.Fields("Name").Value)

            Select Case RsBillExp.Fields("FIELD_NAME").Value
                Case "LOADINGPORT"
                    CboIdentification.Text = IdLOADINGPORT
                Case "LTYPE"
                    CboIdentification.Text = IdLTYPE
                Case "PARTDESCR"
                    CboIdentification.Text = IdPARTDESCR
                Case "SHIPCONTACT"
                    CboIdentification.Text = IdSHIPCONTACT
                Case "BUYERCONTACT"
                    CboIdentification.Text = IdBUYERCONTACT
                Case "FINALDESTINATION"
                    CboIdentification.Text = IdFINALDESTINATION
                Case "REJREM"
                    CboIdentification.Text = IdREJREM
                Case "TRANSHIPMENT"
                    CboIdentification.Text = IdTRANSHIPMENT
                Case "RTYPE"
                    CboIdentification.Text = IdRTYPE
                Case "COUNTRYDESTINATION"
                    CboIdentification.Text = IdCOUNTRYDESTINATION
                Case "DISCHARGEPORT"
                    CboIdentification.Text = IdDISCHARGEPORT
                Case "FOB2"
                    CboIdentification.Text = IdFOB2
                Case "DELIVERYTERMS"
                    CboIdentification.Text = IdDELIVERYTERMS
                Case "GOODSTYPE"
                    CboIdentification.Text = IdGOODSTYPE
                Case "PARTIALSHIPMENT"
                    CboIdentification.Text = IdPARTIALSHIPMENT
                Case "PORTNO"
                    CboIdentification.Text = IdPORTNO
                Case "TERMSOFPAYMENTS"
                    CboIdentification.Text = IdTERMSOFPAYMENTS
                Case "TPT"
                    CboIdentification.Text = IdTPT
                Case "LICNO"
                    CboIdentification.Text = IdLICNO
                Case "STYPE"
                    CboIdentification.Text = IdSTYPE
                Case "GOODSTYPEHEAD"
                    CboIdentification.Text = IdGOODSTYPEHEAD
            End Select

        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsBillExp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        'Resume		
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(sender As Object, e As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
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

        Dim Identification As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        Select Case CboIdentification.Text
            Case IdLOADINGPORT
                Identification = "LOADINGPORT"
            Case IdLTYPE
                Identification = "LTYPE"
            Case IdPARTDESCR
                Identification = "PARTDESCR"
            Case IdSHIPCONTACT
                Identification = "SHIPCONTACT"
            Case IdBUYERCONTACT
                Identification = "BUYERCONTACT"
            Case IdFINALDESTINATION
                Identification = "FINALDESTINATION"
            Case IdREJREM
                Identification = "REJREM"
            Case IdTRANSHIPMENT
                Identification = "TRANSHIPMENT"
            Case IdRTYPE
                Identification = "RTYPE"
            Case IdCOUNTRYDESTINATION
                Identification = "COUNTRYDESTINATION"
            Case IdDISCHARGEPORT
                Identification = "DISCHARGEPORT"
            Case IdFOB2
                Identification = "FOB2"
            Case IdDELIVERYTERMS
                Identification = "DELIVERYTERMS"
            Case IdGOODSTYPE
                Identification = "GOODSTYPE"
            Case IdPARTIALSHIPMENT
                Identification = "PARTIALSHIPMENT"
            Case IdPORTNO
                Identification = "PORTNO"
            Case IdTERMSOFPAYMENTS
                Identification = "TERMSOFPAYMENTS"
            Case IdTPT
                Identification = "TPT"
            Case IdLICNO
                Identification = "LICNO"
            Case IdSTYPE
                Identification = "STYPE"
            Case IdGOODSTYPEHEAD
                Identification = "GOODSTYPEHEAD"
        End Select

        SqlStr = ""
        If ADDMode = True Then
            SqlStr = "INSERT INTO FIN_EXPORT_FIELD_MST (" & vbCrLf _
            & " COMPANY_CODE, FIELD_NAME, FIELD_VALUE) VALUES ( " & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(Identification) & "', '" & MainClass.AllowSingleQuote(txtName.Text) & "'" & vbCrLf _
            & " ) "
            'Else
            '    SqlStr = " UPDATE FIN_EXPORT_FIELD_MST  SET " & vbCrLf _
            '    & " FIELD_VALUE='" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
            '    & " FIELD_NAME='" & MainClass.AllowSingleQuote(Identification) & "' " & vbCrLf _
            '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            '    & " AND CODE= " & xCode & ""
        End If
UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''		
        RsBillExp.Requery() ''.Refresh		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtName.MaxLength = RsBillExp.Fields("FIELD_VALUE").DefinedSize ''		

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillIdentification()
        On Error GoTo ERR1

        CboIdentification.Items.Add(IdLOADINGPORT)
        CboIdentification.Items.Add(IdLTYPE)
        CboIdentification.Items.Add(IdPARTDESCR)
        CboIdentification.Items.Add(IdSHIPCONTACT)
        CboIdentification.Items.Add(IdBUYERCONTACT)
        CboIdentification.Items.Add(IdFINALDESTINATION)
        CboIdentification.Items.Add(IdREJREM)
        CboIdentification.Items.Add(IdTRANSHIPMENT)
        CboIdentification.Items.Add(IdRTYPE)
        CboIdentification.Items.Add(IdCOUNTRYDESTINATION)
        CboIdentification.Items.Add(IdDISCHARGEPORT)
        CboIdentification.Items.Add(IdFOB2)
        CboIdentification.Items.Add(IdDELIVERYTERMS)
        CboIdentification.Items.Add(IdGOODSTYPE)
        CboIdentification.Items.Add(IdPARTIALSHIPMENT)
        CboIdentification.Items.Add(IdPORTNO)
        CboIdentification.Items.Add(IdTERMSOFPAYMENTS)
        CboIdentification.Items.Add(IdTPT)
        CboIdentification.Items.Add(IdLICNO)
        CboIdentification.Items.Add(IdSTYPE)
        CboIdentification.Items.Add(IdGOODSTYPEHEAD)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
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
        If Trim(CboIdentification.Text) = "" Then
            MsgInformation("Identification is empty. Cannot Save")
            CboIdentification.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsBillExp.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 16)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle		
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String = ""
        On Error GoTo ERR1
        mTitle = ""
        Report1.Reset()
        mTitle = "Export Field Parameter"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Billexp.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub TxtDefaultPer_TextChanged(sender As Object, e As System.EventArgs)
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class