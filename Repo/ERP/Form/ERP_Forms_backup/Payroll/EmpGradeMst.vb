Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpGradeMaster
    Inherits System.Windows.Forms.Form
    Dim RsGrade As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection
    Dim Shw As Boolean
    Dim xCode As String
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Sub ViewGrid()

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

        MainClass.ButtonStatus(Me, XRIGHT, RsGrade, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        txtName.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsGrade, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboCatgeory_Change()

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCatgeory_Click()

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtCode.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsGrade, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If txtCode.Enabled = True Then txtCode.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsGrade.EOF = False Then RsGrade.MoveFirst()
            txtCode.Enabled = True
            Show1()
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsGrade.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsGrade.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        If MainClass.SearchGridMaster((txtName.Text), "PAY_GRADE_MST", "GRADE_DESC", "GRADE_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtName.Text = AcName
            txtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            txtName.Focus()
        End If
    End Sub
    Private Sub SearchCode()
        If MainClass.SearchGridMaster((txtCode.Text), "PAY_GRADE_MST", "GRADE_CODE", "GRADE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
    End Sub
    Private Sub frmEmpGradeMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmEmpGradeMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SqlStr = ""

        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        SqlStr = " SELECT * from PAY_GRADE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GRADE_CODE='" & MainClass.AllowSingleQuote(UCase(SprdView.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGrade, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGrade.EOF = False Then
            Show1()
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call SearchCode()
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchCode()
    End Sub

    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        txtCode.Text = VB6.Format(txtCode.Text, "000")
        If MODIFYMode = True And RsGrade.EOF = False Then xCode = RsGrade.Fields("GRADE_CODE").Value
        SqlStr = ""
        SqlStr = " SELECT * from  PAY_GRADE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GRADE_CODE='" & MainClass.AllowSingleQuote(Trim(txtCode.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGrade, ADODB.LockTypeEnum.adLockReadOnly)
        If RsGrade.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_GRADE_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GRADE_CODE='" & xCode & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGrade, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmEmpGradeMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From PAY_GRADE_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGrade, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()

        Clear1()

        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmEmpGradeMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)
        Me.Height = VB6.TwipsToPixelsY(3090)
        Me.Width = VB6.TwipsToPixelsX(7065)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmEmpGradeMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsGrade = Nothing
        'frmEmpGradeMaster = Nothing
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Shw = True
        If Not RsGrade.EOF Then
            txtName.Text = IIf(IsDbNull(RsGrade.Fields("GRADE_DESC").Value), "", RsGrade.Fields("GRADE_DESC").Value)
            txtCode.Text = IIf(IsDbNull(RsGrade.Fields("GRADE_CODE").Value), "", RsGrade.Fields("GRADE_CODE").Value)

            txtCode.Enabled = False
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        If RsGrade.EOF = False Then
            xCode = RsGrade.Fields("GRADE_CODE").Value
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsGrade, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
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
        Dim pCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        If ADDMode = True Then
            pCode = Trim(txtCode.Text)

            SqlStr = " INSERT INTO PAY_GRADE_MST ( " & vbCrLf & " COMPANY_CODE,GRADE_CODE,GRADE_DESC) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(pCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtName.Text)) & "')"
        Else
            SqlStr = " UPDATE PAY_GRADE_MST SET " & vbCrLf & " GRADE_DESC='" & MainClass.AllowSingleQuote(txtName.Text) & "'" & vbCrLf & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GRADE_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        RsGrade.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        SqlStr = ""
        FieldsVarification = True

        If Trim(txtCode.Text) = "" Then
            MsgInformation("Grade is empty. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Len(Trim(txtCode.Text)) <> 3 Then
            MsgInformation("Grade Length must be 3. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtName.Text) = "" Then
            MsgInformation("Grade Desc is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Grade or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsGrade.EOF = 0 Or RsGrade.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub settextlength()
        On Error GoTo ERR1



        txtName.Maxlength = RsGrade.Fields("GRADE_DESC").DefinedSize
        txtCode.Maxlength = RsGrade.Fields("GRADE_CODE").DefinedSize

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsGrade.EOF = False Then xCode = RsGrade.Fields("GRADE_CODE").Value
        SqlStr = ""
        SqlStr = " SELECT * from  PAY_GRADE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GRADE_DESC='" & MainClass.AllowSingleQuote(Trim(txtName.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGrade, ADODB.LockTypeEnum.adLockReadOnly)
        If RsGrade.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_GRADE_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GRADE_CODE='" & xCode & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGrade, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = "SELECT GRADE_CODE AS CODE, GRADE_DESC AS NAME " & vbCrLf & " FROM PAY_GRADE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY GRADE_CODE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 5)
            .set_ColWidth(2, 43)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""
        If Trim(txtCode.Text) = "" Then Delete1 = False : Exit Function

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_GRADE_MST", (txtName.Text), RsGrade) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_GRADE_MST", "GRADE_CODE", (txtCode.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM PAY_GRADE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GRADE_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsGrade.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsGrade.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Designation Listing"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\EmpGrade.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
End Class
