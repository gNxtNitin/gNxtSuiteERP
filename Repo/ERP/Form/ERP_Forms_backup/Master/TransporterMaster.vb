Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTransporterMaster
    Inherits System.Windows.Forms.Form
    Dim RsTransporter As ADODB.Recordset
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
        MainClass.ButtonStatus(Me, XRIGHT, RsTransporter, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""
        txtTransporterId.Text = ""

        Call AutoCompleteSearch("FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "", txtName)

        MainClass.ButtonStatus(Me, XRIGHT, RsTransporter, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub txtTransporterId_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransporterId.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransporterId.Text, "N")
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTransporterId_TextChanged(sender As Object, e As System.EventArgs) Handles txtTransporterId.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTransporter, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
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
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "FIN_TRANSPORTER_MST", (txtName.Text), RsTransporter, "TRANSPORTER_NAME") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", (txtName.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM FIN_TRANSPORTER_MST " & vbCrLf _
            & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & "AND TRANSPORTER_NAME='" & MainClass.AllowSingleQuote(UCase((txtName.Text))) & "'"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsTransporter.Requery() ''.Refresh			
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''			
        RsTransporter.Requery() ''.Refresh			
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdDelete_Click(sender As Object, e As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsTransporter.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.			
                If Delete1() = False Then GoTo DelErrPart
                If RsTransporter.EOF = True Then
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

    Private Sub frmTransporterMaster_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_TRANSPORTER_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransporter, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmTransporterMaster_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsTransporter = Nothing
        RsTransporter.Close()
    End Sub

    Private Sub frmTransporterMaster_KeyDown(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmTransporterMaster_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
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
        txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(sender As Object, EventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If EventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtName_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text, "N", "Y")
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
        If MODIFYMode = True And RsTransporter.EOF = False Then xCode = RsTransporter.Fields("TRANSPORTER_CODE").Value

        SqlStr = "SELECT * FROM FIN_TRANSPORTER_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRANSPORTER_NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtName.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransporter, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTransporter.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Item Type Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM FIN_TRANSPORTER_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TRANSPORTER_CODE=" & xCode & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransporter, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub frmTransporterMaster_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
        'Me.Height = VB6.TwipsToPixelsY(3660)
        ''Me.Width = VB6.TwipsToPixelsX(8265)

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mVehicleOwner As String

        If Not RsTransporter.EOF Then

            txtName.Text = IIf(IsDBNull(RsTransporter.Fields("TRANSPORTER_NAME").Value), "", RsTransporter.Fields("TRANSPORTER_NAME").Value)
            txtTransporterId.Text = IIf(IsDBNull(RsTransporter.Fields("TRANSPORTER_ID").Value), "", RsTransporter.Fields("TRANSPORTER_ID").Value)

            xCode = RsTransporter.Fields("TRANSPORTER_CODE").Value
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsTransporter, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        Resume
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

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        If ADDMode = True Then
            mCode = MainClass.AutoGenRowNo("FIN_TRANSPORTER_MST", "Code", PubDBCn)
            SqlStr = "INSERT INTO FIN_TRANSPORTER_MST (" & vbCrLf _
                & " COMPANY_CODE, TRANSPORTER_CODE, TRANSPORTER_NAME, TRANSPORTER_ID, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE" & vbCrLf _
                & " ) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCode & ", '" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtTransporterId.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"

        Else
            SqlStr = " UPDATE FIN_TRANSPORTER_MST  SET " & vbCrLf _
                & " TRANSPORTER_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
                & " TRANSPORTER_ID='" & MainClass.AllowSingleQuote(txtTransporterId.Text) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND TRANSPORTER_CODE= " & xCode & ""
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''			
        RsTransporter.Requery() ''.Refresh			
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtName.MaxLength = RsTransporter.Fields("TRANSPORTER_NAME").DefinedSize
        txtTransporterId.MaxLength = RsTransporter.Fields("TRANSPORTER_ID").DefinedSize

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

        If Trim(txtTransporterId.Text) = "" Then
            MsgInformation("Transport ID is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsTransporter.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 40)
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
        Dim mTitle As String = ""
        On Error GoTo ERR1
        mTitle = ""
        Report1.Reset()
        mTitle = "Vehicle Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\TransportMst.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)

        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.ReportFileName = ""

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & "TransportMst.rpt"
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

        MsgBox("Print Complete!", MsgBoxStyle.OkOnly, "Operation Completed")

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT TRANSPORTER_NAME, TRANSPORTER_ID" & vbCrLf & " FROM FIN_TRANSPORTER_MST" & vbCrLf & " WHERE FIN_TRANSPORTER_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub

End Class
