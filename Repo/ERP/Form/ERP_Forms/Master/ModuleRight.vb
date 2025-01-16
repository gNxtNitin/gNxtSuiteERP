Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmModuleRights
   Inherits System.Windows.Forms.Form
   Dim RsUserID As ADODB.Recordset
   Dim RsModules As ADODB.Recordset
   Dim RsModuleRights As ADODB.Recordset
   Dim ColModuleName As Integer
   Dim ColCanWork As Integer
   Dim retval As Object
   Dim mnuCnt As Integer
    Dim SqlStr As String = ""
    Dim MasterNo As Object
    Private Const ConRowHeight As Short = 13
    Private Sub Show1()

        On Error GoTo Errshow1
        Dim k As Short
        'Dim RsFields As OraFields					

        k = 1
        SqlStr = ""
        SqlStr = "Select Module.ModuleName,ModuleRight.Rights " & vbCrLf _
           & " From GEN_Module_MST Module, GEN_ModuleRight_MST ModuleRight " & vbCrLf _
           & " Where ModuleRight.ModuleID=Module.ModuleID " & vbCrLf _
           & " And ModuleRight.UserID='" & UCase(txtUserId.Text) & "'" & vbCrLf _
           & " AND COMPANY_CODE=" & CStr(RsCompany.Fields("COMPANY_CODE").Value) & " AND STATUS='O'" & vbCrLf _
           & " ORDER BY Module.ModuleName "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModuleRights, ADODB.LockTypeEnum.adLockOptimistic)

        '    Set RsFields = rsModuleRights.Fields					

        If RsModuleRights.EOF = False Then
            RsModuleRights.MoveFirst()
            Do While Not RsModuleRights.EOF

                For k = 1 To SprdMain.MaxRows
                    SprdMain.Row = k
                    SprdMain.Col = ColModuleName
                    If Trim(SprdMain.Text) = IIf(IsDBNull(RsModuleRights.Fields("ModuleName").Value), "", RsModuleRights.Fields("ModuleName").Value) Then

                        SprdMain.Col = ColCanWork
                        If IIf(IsDBNull(RsModuleRights.Fields("Rights").Value), "No", RsModuleRights.Fields("Rights").Value) = "No" Then
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        Else
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                        End If
                        Exit For
                    End If
                Next
                RsModuleRights.MoveNext()

            Loop
        End If
        Exit Sub
Errshow1:
        MsgBox(Err.Description)

    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)


        On Error GoTo ErrPart

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColModuleName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 31)

            .Col = ColCanWork
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8.5)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.ProtectCell(SprdMain, Arow, .MaxRows, ColModuleName, ColModuleName)
            MainClass.SetSpreadColor(SprdMain, Arow)
            'MainClass.CellColor(SprdMain, 1, .MaxRows, 1, .MaxCols)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Clear1()

        SprdMain.MaxRows = 1
        SprdMain.Col = 1
        SprdMain.Col2 = SprdMain.MaxCols
        SprdMain.Row = 1
        SprdMain.Row2 = SprdMain.MaxRows
        SprdMain.BlockMode = True
        SprdMain.Action = 3
        SprdMain.BlockMode = False
        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        Call AutoCompleteSearch("ATH_PASSWORD_MST", "USER_ID", "", txtUserId)

    End Sub

    Private Sub HideBlankRow()
        On Error GoTo ErrHideBlankRow
        Static j As Integer
        Dim GridRows As Integer
        Dim SlNo As Integer
        GridRows = SprdMain.MaxRows
        SlNo = 1
        For j = 1 To GridRows
            SprdMain.Row = j
            SprdMain.Col = ColModuleName
            If SprdMain.Text = "" Then
                SprdMain.Row = j
                SprdMain.Row2 = j
                SprdMain.RowHidden = True
                GoTo Label11
            End If
            SprdMain.Col = 0
            SprdMain.Row = j
            SprdMain.Row2 = j
            SprdMain.Text = CStr(SlNo)
            SlNo = SlNo + 1
Label11:
        Next
        Exit Sub
ErrHideBlankRow:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillModule()

        On Error GoTo ErrFillMenu
        Dim mRow As Integer
        Dim SqlStr As String = ""

        mRow = 1
        SqlStr = "Select ModuleID,ModuleName from GEN_Module_MST WHERE STATUS='O' Order By ModuleName"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModules, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsModules.EOF Then
            'SprdMain.MaxRows = RsModules.RecordCount				
            FormatSprdMain(-1)
            Do While Not RsModules.EOF
                SprdMain.Row = mRow
                SprdMain.Col = ColModuleName
                SprdMain.Text = RsModules.Fields("ModuleName").Value
                RsModules.MoveNext()
                mRow = mRow + 1
                SprdMain.MaxRows = mRow
            Loop
            FormatSprdMain(-1)
            HideBlankRow()
        End If
        Exit Sub
ErrFillMenu:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtUserId_DoubleClick(sender As Object, e As System.EventArgs) Handles txtUserId.DoubleClick
        UserIDSearch()
    End Sub

    Private Sub txtUserId_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUserId.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtUserId.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUserId_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtUserId.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then UserIDSearch()
    End Sub

    Private Sub txtUserId_TextChanged(sender As Object, e As System.EventArgs) Handles txtUserId.TextChanged
        CmdSave.Enabled = True
    End Sub

    Private Sub txtUserId_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtUserId.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        LblUserName.Text = ""
        If txtUserId.Text <> "" Then
            If MainClass.ValidateWithMasterTable(txtUserId.Text, "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtUserId.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(txtUserId.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                LblUserName.Text = MasterNo
                'DoEvents()							
            End If
            Clear1()
            FillModule()
            Show1()
        End If
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub CmdClose_Click(sender As Object, e As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdSave_Click(sender As Object, e As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrSave
        Dim ICnt As Short
        Dim ModRight As String
        Dim mModuleID As Double
        Dim strModuleName As String
        Dim mUserType As String

        'Dim PvtDBCn As ADODB.Connection					

        ''Set PvtDBCn = New ADODB.Connection					
        'PvtDBCn.Open StrConn					


        If Trim(txtUserId.Text) = "" Then Exit Sub

        If PubSuperUser = "U" Or PubSuperUser = "G" Then
            MsgInformation("You have not right. Cannot Save")
            Exit Sub
        End If

        mUserType = GetUserPermission("SUPER_USER", "N", Trim(txtUserId.Text), RsCompany.Fields("COMPANY_CODE").Value)

        If PubSuperUser = "A" Or PubSuperUser = "S" Then
        Else
            'If mUserType = "A" Or mUserType = "S" Then
            MsgInformation("You have not right. Cannot Save")
            Exit Sub
            'End If
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Execute("Delete From GEN_ModuleRight_MST Where UserID='" & UCase(txtUserId.Text) & "' AND COMPANY_CODE=" & CStr(RsCompany.Fields("COMPANY_CODE").Value) & "")

        For ICnt = 1 To SprdMain.MaxRows
            ModRight = ""
            strModuleName = ""
            SprdMain.Row = ICnt
            SprdMain.Col = ColModuleName
            If SprdMain.Text = "" Then GoTo LabelSave
            strModuleName = SprdMain.Text
            MainClass.ValidateWithMasterTable(strModuleName, "ModuleName", "ModuleID", "GEN_Module_MST", PubDBCn, MasterNo)
            mModuleID = MasterNo
            SprdMain.Col = ColCanWork
            ModRight = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Yes", "No")
            ''14-06-2006				
            '        ModRight = IIf(UCase(txtUserId.Text) = "SUPER", "Yes", ModRight)				
            SqlStr = ""
            SqlStr = "Insert Into GEN_ModuleRight_MST (UserID,COMPANY_CODE,MODULEID, Rights) Values ('" & txtUserId.Text & "'," & CStr(RsCompany.Fields("COMPANY_CODE").Value) & ", " & mModuleID & ", '" & ModRight & "')"
            PubDBCn.Execute(SqlStr)
LabelSave:
        Next
        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        CmdSave.Enabled = False
        Exit Sub
ErrSave:
        MsgBox(Err.Description)
        'Resume	
    End Sub

    Private Sub frmModuleRights_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub frmModuleRights_FormClosing(sender As Object, EventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = EventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = EventArgs.CloseReason
        RsUserID = Nothing
        RsModules = Nothing
        RsModuleRights = Nothing
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub frmModuleRights_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call SetMainFormCordinate(Me)
        MainClass.SetControlsColor(Me)
        ColModuleName = 1
        ColCanWork = 2
        Clear1()
        lblCompanyName.Text = RsCompany.Fields("Company_Name").Value

    End Sub

    Private Sub OptRights_CheckedChanged(eventSender As Object, e As System.EventArgs) Handles OptRights.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptRights.GetIndex(eventSender)
            Static I, j As Object
            'Static X As Integer
            j = SprdMain.MaxRows
            For I = 1 To j
                SprdMain.Row = I
                SprdMain.Col = ColCanWork
                SprdMain.Text = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            Next I
            CmdSave.Enabled = True
        End If
    End Sub

    Private Sub SprdMain_Change(sender As Object, e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        CmdSave.Enabled = True
    End Sub

    Private Sub SprdMain_ClickEvent(sender As Object, e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        CmdSave.Enabled = True
    End Sub
    Private Sub UserIDSearch()
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtUserId.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr) = True Then
            txtUserId.Text = AcName
            TxtUserID_Validating(txtUserId, New System.ComponentModel.CancelEventArgs(False))
            If SprdMain.Enabled = True Then SprdMain.Focus()
            'End If							
        End If
    End Sub
End Class
