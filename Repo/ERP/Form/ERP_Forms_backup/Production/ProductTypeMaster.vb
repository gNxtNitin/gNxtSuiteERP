Option Strict Off
Option Explicit On
Friend Class frmProductTypeMaster
   Inherits System.Windows.Forms.Form
   Dim RsProdType As ADODB.Recordset
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   'Private PvtDBCn As ADODB.Connection	

   Dim xProdDesc As String
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
            Fragridview.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            Fragridview.SendToBack()
        End If

        MainClass.ButtonStatus(Me, XRIGHT, RsProdType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtDesc.Text = ""
        cboUOM.SelectedIndex = -1
        txtDesc.Enabled = True

        Call AutoCompleteSearch("INV_PRODUCTTYPE_MST", "PRODTYPE_DESC", "", txtDesc)
        MainClass.ButtonStatus(Me, XRIGHT, RsProdType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProdType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtDesc.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo ERR1
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtDesc.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "INV_PRODUCTTYPE_MST", (txtDesc.Text), RsProdType, "PRODTYPE_DESC") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "INV_PRODUCTTYPE_MST", "PRODTYPE_DESC", (txtDesc.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM INV_PRODUCTTYPE_MST " & vbCrLf _
                  & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                  & " AND PRODTYPE_DESC='" & MainClass.AllowSingleQuote(UCase((txtDesc.Text))) & "'"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsProdType.Requery() ''.Refresh		
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''		
        RsProdType.Requery() ''.Refresh		
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtDesc.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsProdType.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                If Delete1 = False Then GoTo DelErrPart
                If RsProdType.EOF = True Then
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
    Private Sub frmProductTypeMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmProductTypeMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtDesc.Text = Trim(SprdView.Text)
        txtDesc_Validating(txtDesc, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesc.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtDesc.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsProdType.EOF = False Then xProdDesc = RsProdType.Fields("PRODTYPE_DESC").Value

        SqlStr = "SELECT * FROM INV_PRODUCTTYPE_MST " & vbCrLf _
                  & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                  & " AND PRODTYPE_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdType, ADODB.LockTypeEnum.adLockReadOnly)

        If RsProdType.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Product Type Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM INV_PRODUCTTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODTYPE_DESC='" & xProdDesc & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdType, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmProductTypeMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From INV_PRODUCTTYPE_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdType, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmProductTypeMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        cboUOM.Items.Clear()
        cboUOM.Items.Add("NOS")
        cboUOM.Items.Add("DM²")
        cboUOM.SelectedIndex = -1

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmProductTypeMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsProdType = Nothing
        RsProdType.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mUOM As String = ""

        If Not RsProdType.EOF Then

            txtDesc.Text = IIf(IsDbNull(RsProdType.Fields("PRODTYPE_DESC").Value), "", RsProdType.Fields("PRODTYPE_DESC").Value)
            mUOM = IIf(IsDbNull(RsProdType.Fields("UOM").Value), "", RsProdType.Fields("UOM").Value)

            If mUOM = "NOS" Then
                cboUOM.SelectedIndex = 0
            Else
                cboUOM.SelectedIndex = 1
            End If

            xProdDesc = RsProdType.Fields("PRODTYPE_DESC").Value
        End If
        txtDesc.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsProdType, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume		
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtDesc_Validating(txtDesc, New System.ComponentModel.CancelEventArgs(False))
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

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        If ADDMode = True Then
            '        mCode = MainClass.AutoGenRowNo("FIN_TARRIF_MST", "Code", PubDBCn)		
            SqlStr = "INSERT INTO INV_PRODUCTTYPE_MST (" & vbCrLf _
                    & " COMPANY_CODE, PRODTYPE_DESC, UOM) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDesc.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(cboUOM.Text) & "')"
        Else
            SqlStr = " UPDATE INV_PRODUCTTYPE_MST  SET " & vbCrLf _
                    & " UOM ='" & MainClass.AllowSingleQuote(cboUOM.Text) & "' " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND PRODTYPE_DESC= '" & xProdDesc & "'"
        End If

        'If ADDMode = True Then		
        '	'        mCode = MainClass.AutoGenRowNo("FIN_TARRIF_MST", "Code", PubDBCn)	
        'Else		
        '	SqlStr = " UPDATE INV_PRODUCTTYPE_MST  SET " & vbCrLf & " UOM ='" & MainClass.AllowSingleQuote(cboUOM.Text) & "' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODTYPE_DESC= '" & xProdDesc & "'"	
        'End If		
UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''		
        RsProdType.Requery() ''.Refresh		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtDesc.Maxlength = RsProdType.Fields("PRODTYPE_DESC").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True

        If Trim(txtDesc.Text) = "" Then
            MsgInformation("Description is empty. Cannot Save")
            txtDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboUOM.Text) = "" Then
            MsgInformation("UOM is empty. Cannot Save")
            cboUOM.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsProdType.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub txtDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = "" 

        SqlStr = ""

        SqlStr = " SELECT PRODTYPE_DESC, UOM " & vbCrLf & " FROM INV_PRODUCTTYPE_MST" & vbCrLf & " WHERE INV_PRODUCTTYPE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & "ORDER BY PRODTYPE_DESC"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
   Private Sub FormatSprdView()

      With SprdView
         .Row = -1
         .set_RowHeight(0, 12)
         .set_ColWidth(0, 5)
         .set_ColWidth(1, 50)
         .set_ColWidth(2, 8)
         .ColsFrozen = 1
         MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
         MainClass.SetSpreadColor(SprdView, -1)
         .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle		
         MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
      End With
   End Sub
   Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String = ""
      On Error GoTo ERR1
      mTitle = ""
      Report1.Reset()
      mTitle = "Product Type Master"
      Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ProdTypeMst.rpt"
      SetCrpt(Report1, Mode, 1, mTitle)
      Report1.WindowShowGroupTree = False
      Report1.Action = 1
      Exit Sub
ERR1:
      MsgInformation(Err.Description)
   End Sub
End Class
