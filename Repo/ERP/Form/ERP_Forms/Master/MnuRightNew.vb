Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMnuRightsNew
   Inherits System.Windows.Forms.Form
   Dim rsRights As ADODB.Recordset
   Private Const ColUserID As Short = 1
   Private Const ColModuleName As Short = 2
   Private Const ColMenuName As Short = 3
   Private Const ColAdd As Short = 4
   Private Const ColModify As Short = 5
   Private Const ColDelete As Short = 6
   Private Const ColView As Short = 7
   Private Const ColAuthorised As Short = 8
   Private Const ColPrint As Short = 9
   Private Const ColHeadCount As Short = 10
   Private Const ColMenu As Short = 11
   Dim retval As Object
   Dim mnuCnt As Integer
   Private Const ConRowHeight As Short = 13
   Dim mSearchKey As String
   Dim cntSearchRow As Integer
   Dim mFormLoad As Boolean
   Dim mFillData As Boolean


   Private Function GetMenuRights(ByVal pUserID As String, ByVal pModuleID As String, ByVal pMenuId As String) As String

      On Error GoTo Errshow1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetMenuRights = ""
        SqlStr = "Select * From FIN_Rights_MST " & vbCrLf _
                  & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                  & " AND UserID='" & MainClass.AllowSingleQuote(pUserID) & "'" & vbCrLf _
                  & " And MODULEID=" & pModuleID & "" & vbCrLf _
                  & " And MENUHEAD='" & MainClass.AllowSingleQuote(pMenuId) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetMenuRights = RsTemp.Fields("Rights").Value
        End If

        Exit Function
Errshow1:
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ErrPart
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColUserID
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColUserID, 6)

            .Col = ColModuleName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColModuleName, 20)

            .Col = ColMenuName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColMenuName, 45)
            .BlockMode = True

            For cntCol = ColAdd To ColPrint
                .Col = cntCol
                .CellType = SS_CELL_TYPE_CHECKBOX
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColHeadCount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .ColHidden = True

            .Col = ColMenu
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .ColHidden = True

            MainClass.ProtectCell(SprdMain, Arow, .MaxRows, ColUserID, ColMenuName)

            MainClass.SetSpreadColor(SprdMain, Arow)
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

        ChkAllAdd.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAllModify.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAllDelete.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAllView.CheckState = System.Windows.Forms.CheckState.Unchecked

        Call AutoCompleteSearch("ATH_PASSWORD_MST", "USER_ID", "", txtUserId)
        'Call AutoCompleteSearch("GEN_ERPMENU_MST", "MENUHEADNAME", "", txtMenuName)
        'Call AutoCompleteSearch("GEN_MODULE_MST", "MODULENAME", "STATUS='O'", txtModuleName)

        mFillData = False
        FormatSprdMain(-1)
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
            SprdMain.Col = ColMenuName
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

    Private Function RemoveSplChar(ByRef GetSTR As String) As String
        On Error GoTo ErrRemoveSplChar
        Dim FindPos As Short
        RemoveSplChar = GetSTR
        FindPos = 0
        FindPos = InStr(1, GetSTR, "&", CompareMethod.Text)
        If FindPos > 0 Then
            RemoveSplChar = VB.Left(GetSTR, FindPos - 1) & Mid(GetSTR, FindPos + 1)
        End If
        Exit Function
ErrRemoveSplChar:
        MsgBox(Err.Description)
    End Function
    Private Sub FillModuleAndMenu()

        On Error GoTo ErrFillMenu
        Dim mHeadMenuCnt As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mModuleID As Double
        Dim mMenuHeadID As String
        Dim I As Integer

        Dim SqlStrUser As String
        Dim RsUser As ADODB.Recordset = Nothing
        Dim mUserId As String
        Dim mGetMenuRights As String
        Dim mTopHeadId As String

        If chkAllUserID.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtUserId.Text) = "" Then Exit Sub
        End If

        FormatSprdMain(-1)
        I = 1

        SqlStrUser = " SELECT DISTINCT A.USER_ID FROM ATH_PASSWORD_MST A, GEN_MODULERIGHT_MST C" & vbCrLf _
           & " WHERE A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStrUser = SqlStrUser & vbCrLf _
                    & " AND A.COMPANY_CODE =C.COMPANY_CODE AND A.USER_ID=C.USERID And UPPER(RIGHTS)='YES' "


        If chkAllModule.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtModuleName.Text, "MODULENAME", "MODULEID", "GEN_MODULE_MST", PubDBCn, MasterNo, , "") = False Then
                SqlStrUser = SqlStrUser & vbCrLf & " C.MODULEID ='" & MainClass.AllowSingleQuote(MasterNo) & "'"
            End If
        End If

        If chkAllUserID.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStrUser = SqlStrUser & vbCrLf & " AND A.USER_ID='" & MainClass.AllowSingleQuote(txtUserId.Text) & "'"
        End If

        If OptShow(0).Checked = True Then
            SqlStrUser = SqlStrUser & vbCrLf & " AND A.STATUS='O'"
        End If

        SqlStrUser = SqlStrUser & vbCrLf & "ORDER BY A.USER_ID"

        MainClass.UOpenRecordSet(SqlStrUser, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsUser, ADODB.LockTypeEnum.adLockReadOnly)
        If RsUser.EOF = False Then
            Do While RsUser.EOF = False
                mUserId = IIf(IsDBNull(RsUser.Fields("USER_ID").Value), "", RsUser.Fields("USER_ID").Value)

                SqlStr = "SELECT A.*, B.MODULENAME " & vbCrLf _
                    & " FROM GEN_ERPMENU_MST A, GEN_MODULE_MST B, GEN_MODULERIGHT_MST C" & vbCrLf _
                    & " WHERE A.MODULEID = B.MODULEID And STATUS='O' AND A.IS_ACTIVE='Y' AND IS_GROUP='Y'" & vbCrLf _
                    & " AND C.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " And C.MODULEID=A.MODULEID And C.USERID='" & mUserId & "' And UPPER(RIGHTS)='YES' "

                If chkAllModule.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    SqlStr = SqlStr & vbCrLf & " And B.MODULENAME ='" & MainClass.AllowSingleQuote(txtModuleName.Text) & "'"
                End If

                If chkAllMenu.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    SqlStr = SqlStr & vbCrLf & " AND UPPER(MENUHEADNAME)='" & MainClass.AllowSingleQuote(UCase(txtMenuName.Text)) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & "ORDER BY A.MODULEID, A.SUBROWNO"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    With SprdMain
                        Do While RsTemp.EOF = False
                            .Row = I
                            .Col = ColUserID
                            .Text = mUserId

                            .Col = ColModuleName
                            .Text = IIf(IsDBNull(RsTemp.Fields("ModuleName").Value), "", RsTemp.Fields("ModuleName").Value)

                            .Col = ColMenuName
                            .Text = IIf(IsDBNull(RsTemp.Fields("MENUHEADNAME").Value), "", RsTemp.Fields("MENUHEADNAME").Value)

                            .Col = ColMenu
                            .Text = IIf(IsDBNull(RsTemp.Fields("MENUHEADID").Value), "", RsTemp.Fields("MENUHEADID").Value)

                            .Col = ColHeadCount
                            .Text = IIf(IsDBNull(RsTemp.Fields("HEADCOUNT").Value), "", RsTemp.Fields("HEADCOUNT").Value)

                            mGetMenuRights = GetMenuRights(mUserId, RsTemp.Fields("MODULEID").Value, IIf(IsDBNull(RsTemp.Fields("MENUHEADID").Value), "", RsTemp.Fields("MENUHEADID").Value))

                            mTopHeadId = IIf(IsDBNull(RsTemp.Fields("FORMNAME").Value), "", RsTemp.Fields("FORMNAME").Value)

                            If InStr(1, mGetMenuRights, "A", CompareMethod.Text) Then
                                SprdMain.Col = ColAdd
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                            End If
                            If InStr(1, mGetMenuRights, "M", CompareMethod.Text) Then
                                SprdMain.Col = ColModify
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                            End If
                            If InStr(1, mGetMenuRights, "D", CompareMethod.Text) Then
                                SprdMain.Col = ColDelete
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                            End If
                            If InStr(1, mGetMenuRights, "V", CompareMethod.Text) Then
                                SprdMain.Col = ColView
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                            End If
                            If InStr(1, mGetMenuRights, "S", CompareMethod.Text) Then
                                SprdMain.Col = ColAuthorised
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                            End If

                            If InStr(1, mGetMenuRights, "P", CompareMethod.Text) Then
                                SprdMain.Col = ColPrint
                                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                            End If

                            'If RsTemp.Fields("ISTOPMENU").Value = "Y" Then
                            '   .Row = I
                            '   .Row2 = I
                            '   .Col = 1
                            '   .col2 = .MaxCols
                            '   .BlockMode = True
                            '   .Font = VB6.FontChangeBold(.Font, True)
                            '   .BackColor = System.Drawing.Color.Lime
                            '   .BlockMode = False
                            'End If

                            If RsTemp.Fields("ISTOPMENU").Value = "Y" Then
                                .Row = I
                                .Row2 = I
                                .Col = 1
                                .Col2 = .MaxCols
                                .BlockMode = True
                                .FontBold = True
                                .BackColor = System.Drawing.Color.Lime
                                .BlockMode = False
                            ElseIf mTopHeadId = "" Then
                                .Row = I
                                .Row2 = I
                                .Col = 1
                                .Col2 = .MaxCols
                                .BlockMode = True
                                .FontBold = True
                                .BackColor = System.Drawing.Color.LightGreen
                                .BlockMode = False
                            End If

                            RsTemp.MoveNext()
                            If RsTemp.EOF = False Then
                                I = I + 1
                                SprdMain.MaxRows = I
                            End If
                        Loop
                    End With
                End If
                RsUser.MoveNext()
                If RsUser.EOF = False Then
                    I = I + 1
                    SprdMain.MaxRows = I
                End If
            Loop
        End If


        '    HideBlankRow	
        Exit Sub
ErrFillMenu:
        MsgBox(Err.Description)
    End Sub

    Private Sub ChkAllAdd_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkAllAdd.CheckStateChanged
        Dim I As Object
        Dim j As Integer

        j = SprdMain.MaxRows

        For I = 1 To j
            SprdMain.Row = I
            SprdMain.Col = ColAdd
            SprdMain.Value = IIf(ChkAllAdd.CheckState = System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        Next I

        CmdSave.Enabled = True
    End Sub

    Private Sub chkAllDelete_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDelete.CheckStateChanged
        Dim I As Object
        Dim j As Integer

        j = SprdMain.MaxRows

        For I = 1 To j
            SprdMain.Row = I
            SprdMain.Col = ColDelete
            SprdMain.Value = IIf(chkAllDelete.CheckState = System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        Next I

        CmdSave.Enabled = True
    End Sub

    Private Sub chkAllMenu_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllMenu.CheckStateChanged
        txtMenuName.Enabled = IIf(chkAllMenu.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearchMenu.Enabled = IIf(chkAllMenu.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        '    Clear1	
        '    FillModuleAndMenu	
        '    mFillData = True	

    End Sub

    Private Sub chkAllModify_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllModify.CheckStateChanged
        Dim I As Object
        Dim j As Integer

        j = SprdMain.MaxRows

        For I = 1 To j
            SprdMain.Row = I
            SprdMain.Col = ColModify
            SprdMain.Value = IIf(chkAllModify.CheckState = System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        Next I

        CmdSave.Enabled = True
    End Sub

    Private Sub chkAllModule_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllModule.CheckStateChanged
        txtModuleName.Enabled = IIf(chkAllModule.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearchModule.Enabled = IIf(chkAllModule.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        '    Clear1	
        '    FillModuleAndMenu	
        '	
        '    mFillData = True	
    End Sub

    Private Sub chkAllUserID_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllUserID.CheckStateChanged
        txtUserId.Enabled = IIf(chkAllUserID.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        '    Clear1	
        '    FillModuleAndMenu	
        '	
        '    mFillData = True	
    End Sub

    Private Sub chkAllView_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllView.CheckStateChanged
        Dim I As Object
        Dim j As Integer

        j = SprdMain.MaxRows

        For I = 1 To j
            SprdMain.Row = I
            SprdMain.Col = ColView
            SprdMain.Value = IIf(chkAllView.CheckState = System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        Next I

        CmdSave.Enabled = True
    End Sub


    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If SprdMain.MaxRows <= 0 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default : Exit Sub
        End If
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearchMenu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchMenu.Click
        Call txtMenuName_DoubleClick(txtMenuName, New System.EventArgs())
    End Sub

    Private Sub cmdsearchModule_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchModule.Click
        Call txtModuleName_DoubleClick(txtModuleName, New System.EventArgs())
    End Sub

    Private Sub cmpPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmpPopulate.Click
        On Error GoTo ErrPart
        Clear1()
        FillModuleAndMenu()

        mFillData = True
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmMnuRightsNew_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        If KeyCode = System.Windows.Forms.Keys.F3 Then
            MainClass.SearchIntoGrid(SprdMain, ColMenuName, mSearchKey, cntSearchRow)
            cntSearchRow = cntSearchRow + 1
        End If
    End Sub

    Private Sub OptShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptShow.GetIndex(eventSender)
            Clear1()
        End If
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        'Dim I As Long	
        'Dim mValue As String	
        Dim cntRow As Integer
        'Dim cntCol As Integer
        Dim mIsHead As String
        Dim mLevel As Integer
        Dim mSprdValue As String

        CmdSave.Enabled = True
        cntSearchRow = 1
        If eventArgs.row = 0 And eventArgs.col = ColMenuName Then
            mSearchKey = ""
            mSearchKey = InputBox("Enter Menu Name :", "Search", mSearchKey)
            MainClass.SearchIntoGrid(SprdMain, ColMenuName, mSearchKey, cntSearchRow)
            cntSearchRow = cntSearchRow + 1
            SprdMain.Focus()
        End If

        If mFillData = False Then Exit Sub

        'If eventArgs.row <> 0 And eventArgs.col = ColAdd Then
        '    SprdMain.Row = eventArgs.row
        '    SprdMain.Col = ColAdd
        '    mSprdValue = SprdMain.Value

        '    SprdMain.Col = ColHeadCount
        '    mIsHead = VB.Left(SprdMain.Text, 1)
        '    mLevel = Val(Mid(SprdMain.Text, 2))
        '    If mIsHead = "H" Then
        '        If MsgQuestion("Are you want to Mark/UnMark all menu in this head.") = CStr(MsgBoxResult.Yes) Then
        '            For cntRow = eventArgs.row To SprdMain.MaxRows
        '                SprdMain.Row = cntRow
        '                SprdMain.Col = ColHeadCount
        '                If mLevel = Val(SprdMain.Text) Then
        '                    SprdMain.Col = ColAdd
        '                    SprdMain.Value = mSprdValue
        '                End If
        '            Next
        '        End If
        '    End If
        'End If

        'If eventArgs.row <> 0 And eventArgs.col = ColModify Then
        '    SprdMain.Row = eventArgs.row
        '    SprdMain.Col = ColModify
        '    mSprdValue = SprdMain.Value

        '    SprdMain.Col = ColHeadCount
        '    mIsHead = VB.Left(SprdMain.Text, 1)
        '    mLevel = Val(Mid(SprdMain.Text, 2))
        '    If mIsHead = "H" Then
        '        If MsgQuestion("Are you want to Mark/UnMark all menu in this head.") = CStr(MsgBoxResult.Yes) Then
        '            For cntRow = eventArgs.row To SprdMain.MaxRows
        '                SprdMain.Row = cntRow
        '                SprdMain.Col = ColHeadCount
        '                If mLevel = Val(SprdMain.Text) Then
        '                    SprdMain.Col = ColModify
        '                    SprdMain.Value = mSprdValue
        '                End If
        '            Next
        '        End If
        '    End If
        'End If

        'If eventArgs.row <> 0 And eventArgs.col = ColDelete Then
        '    SprdMain.Row = eventArgs.row
        '    SprdMain.Col = ColDelete
        '    mSprdValue = SprdMain.Value

        '    SprdMain.Col = ColHeadCount
        '    mIsHead = VB.Left(SprdMain.Text, 1)
        '    mLevel = Val(Mid(SprdMain.Text, 2))
        '    If mIsHead = "H" Then
        '        If MsgQuestion("Are you want to Mark/UnMark all menu in this head.") = CStr(MsgBoxResult.Yes) Then
        '            For cntRow = eventArgs.row To SprdMain.MaxRows
        '                SprdMain.Row = cntRow
        '                SprdMain.Col = ColHeadCount
        '                If mLevel = Val(SprdMain.Text) Then
        '                    SprdMain.Col = ColDelete
        '                    SprdMain.Value = mSprdValue
        '                End If
        '            Next
        '        End If
        '    End If
        'End If

        'If eventArgs.row <> 0 And eventArgs.col = ColView Then
        '    SprdMain.Row = eventArgs.row
        '    SprdMain.Col = ColView
        '    mSprdValue = SprdMain.Value

        '    SprdMain.Col = ColHeadCount
        '    mIsHead = VB.Left(SprdMain.Text, 1)
        '    mLevel = Val(Mid(SprdMain.Text, 2))
        '    If mIsHead = "H" Then
        '        If MsgQuestion("Are you want to Mark/UnMark all menu in this head.") = CStr(MsgBoxResult.Yes) Then
        '            For cntRow = eventArgs.row To SprdMain.MaxRows
        '                SprdMain.Row = cntRow
        '                SprdMain.Col = ColHeadCount
        '                If mLevel = Val(SprdMain.Text) Then
        '                    SprdMain.Col = ColView
        '                    SprdMain.Value = mSprdValue
        '                End If
        '            Next
        '        End If
        '    End If
        'End If


        'If eventArgs.row <> 0 And eventArgs.col = ColAuthorised Then
        '    SprdMain.Row = eventArgs.row
        '    SprdMain.Col = ColAuthorised
        '    mSprdValue = SprdMain.Value

        '    SprdMain.Col = ColHeadCount
        '    mIsHead = VB.Left(SprdMain.Text, 1)
        '    mLevel = Val(Mid(SprdMain.Text, 2))
        '    If mIsHead = "H" Then
        '        If MsgQuestion("Are you want to Mark/UnMark all menu in this head.") = CStr(MsgBoxResult.Yes) Then
        '            For cntRow = eventArgs.row To SprdMain.MaxRows
        '                SprdMain.Row = cntRow
        '                SprdMain.Col = ColHeadCount
        '                If mLevel = Val(SprdMain.Text) Then
        '                    SprdMain.Col = ColAuthorised
        '                    SprdMain.Value = mSprdValue
        '                End If
        '            Next
        '        End If
        '    End If
        'End If

    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent

        If eventArgs.keyCode = System.Windows.Forms.Keys.F3 Then
            MainClass.SearchIntoGrid(SprdMain, ColMenuName, mSearchKey, cntSearchRow)
            cntSearchRow = cntSearchRow + 1
        End If
    End Sub
    Private Sub txtMenuName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMenuName.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String

        'If MainClass.SearchGridMaster(txtMenuName.Text, "GEN_ERPMENU_MST", "MENUHEADNAME", "", , , "") = True Then
        '    txtMenuName.Text = AcName
        '    If txtMenuName.Enabled = True Then txtMenuName.Focus()
        'End If

        ''SqlStr = "SELECT UPPER(MENUHEADNAME) AS UPPER FROM GEN_ERPMENU_MST WHERE 1=1"
        SqlStr = "SELECT MENUHEADNAME FROM vwGEN_ERPMENU_MST WHERE 1=1"

        ''vwGEN_ERPMENU_MST
        If MainClass.SearchGridMasterBySQL2(txtMenuName.Text, SqlStr) = True Then
            txtMenuName.Text = AcName
            If txtMenuName.Enabled = True Then txtMenuName.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtMenuName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMenuName.KeyPress
        'Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        'KeyAscii = MainClass.UpperCase(KeyAscii, txtMenuName.Text)
        'eventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 0 Then
        '    eventArgs.Handled = True
        'End If
    End Sub

    Private Sub txtMenuName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMenuName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtMenuName_DoubleClick(txtMenuName, New System.EventArgs())
    End Sub

    Private Sub txtMenuName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMenuName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtMenuName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtMenuName.Text, "UPPER(MENUHEADNAME)", "MENUHEADNAME", "GEN_ERPMENU_MST", PubDBCn, MasterNo, , "") = False Then
            MsgInformation("Invalid Menu Name Code")
            Cancel = True
        End If

        '    Clear1	
        '    FillModuleAndMenu	
        '	
        '    mFillData = True	

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtModuleName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModuleName.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String

        If chkAllUserID.Checked = True Then
            If MainClass.SearchGridMaster(txtModuleName.Text, "GEN_MODULE_MST", "MODULENAME", "", , , "STATUS='O'") = True Then
                txtModuleName.Text = AcName
                If txtModuleName.Enabled = True Then txtModuleName.Focus()
            End If
        Else
            SqlStr = "SELECT B.MODULENAME " & vbCrLf _
                    & " FROM GEN_MODULE_MST B, GEN_MODULERIGHT_MST C" & vbCrLf _
                    & " WHERE IS_GROUP='Y' AND STATUS='O'" & vbCrLf _
                    & " AND C.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And C.MODULEID = B.MODULEID And C.USERID ='" & txtUserId.Text & "' And UPPER(RIGHTS)='YES' "

            SqlStr = SqlStr & vbCrLf & "ORDER BY B.MODULENAME"
            If MainClass.SearchGridMasterBySQL2(txtModuleName.Text, SqlStr) = True Then
                txtModuleName.Text = AcName
                If txtModuleName.Enabled = True Then txtModuleName.Focus()
            End If

        End If



        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtModuleName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModuleName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtModuleName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtModuleName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtModuleName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtModuleName_DoubleClick(txtModuleName, New System.EventArgs())
    End Sub

    Private Sub txtModuleName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtModuleName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtModuleName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtModuleName.Text, "MODULENAME", "MODULENAME", "GEN_MODULE_MST", PubDBCn, MasterNo, , "STATUS='O'") = False Then
            MsgInformation("Invalid Module Name Code")
            Cancel = True
        End If

        '    Clear1	
        '    FillModuleAndMenu	
        '	
        '    mFillData = True	

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtUserId_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUserId.TextChanged
        CmdSave.Enabled = True
    End Sub

    Private Sub txtUserId_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUserId.DoubleClick
        UserIDSearch()
    End Sub
    Private Sub txtUserId_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUserId.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtUserId.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtUserID_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtUserId.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then UserIDSearch()
    End Sub
    Private Sub TxtUserID_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUserId.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        LblUserName.Text = ""
        If txtUserId.Text <> "" Then
            If MainClass.ValidateWithMasterTable(txtUserId.Text, "User_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '            txtUserId.Text = IIf(IsNull(MasterNo), "", MasterNo)	
                LblUserName.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            Else
                MsgInformation("User ID is not Defined in the Master.")
                Cancel = True
                Exit Sub
            End If

            '        If MainClass.ValidateWithMasterTable(txtUserId.Text, "User_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then	
            '            LblUserName.text = IIf(IsNull(MasterNo), "", MasterNo)	
            '            DoEvents	
            '        End If	
            '        Clear1	
            '        FillModuleAndMenu	
            '	
            '        mFillData = True	
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrSave
        Static SqlStr As String
        Dim mSqlStr As String = ""
        Dim ICnt As Short
        Dim UserRightSTR As String
        Dim Menu_Renamed As String
        Dim mUserType As String
        Dim mTempUserRightSTR As String
        Dim mUserId As String
        Dim mModuleID As Integer
        Dim mModuleName As String

        'Dim PvtDBCn As ADODB.Connection	

        ''Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        '    If Trim(txtUserID.Text) = "" Then Exit Sub	
        '    MainClass.ValidateWithMasterTable cboBranch, "BranchName", "BranchCode", "Branch", PubDBCn, MasterNo	
        '    mBranchCode = MasterNo	

        If PubSuperUser = "U" Or PubSuperUser = "G" Then
            MsgInformation("You have not right. Cannot Save")
            Exit Sub
        End If

        For ICnt = 1 To SprdMain.MaxRows
            SprdMain.Row = ICnt

            SprdMain.Col = ColMenuName
            If SprdMain.Text = "" Then GoTo Label1

            SprdMain.Col = ColUserID
            If SprdMain.Text = "" Then GoTo Label1
            mUserId = Trim(SprdMain.Text)

            mUserType = GetUserPermission("SUPER_USER", "N", mUserId, RsCompany.Fields("COMPANY_CODE").Value)

            'If PubSuperUser = "A" Then
            '    If mUserType = "A" Or mUserType = "U" Then
            '        MsgInformation("You have not rights to save Admin or Super User. Cannot Save")
            '        Exit Sub
            '    End If
            'End If

            'If PubSuperUser <> "S" Then
            '    SprdMain.Col = ColDelete
            '    If SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            '    MsgInformation("You have not rights to give Delete Right to User. Cannot Save")
            '    Exit Sub
            'End If

            'SprdMain.Col = ColAuthorised
            'If SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked) Then
            '    MsgInformation("You have not rights to give Authorise Right to User. Cannot Save")
            '    Exit Sub
            'End If

            'End If
Label1:
        Next
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        ''**************************	
        For ICnt = 1 To SprdMain.MaxRows
            UserRightSTR = ""
            Menu_Renamed = ""
            SprdMain.Row = ICnt
            SprdMain.Col = ColMenuName
            If SprdMain.Text = "" Then GoTo LabelSave

            SprdMain.Col = ColUserID
            If SprdMain.Text = "" Then GoTo LabelSave
            mUserId = Trim(SprdMain.Text)

            SprdMain.Col = ColModuleName
            If SprdMain.Text = "" Then GoTo LabelSave
            mModuleName = Trim(SprdMain.Text)
            If MainClass.ValidateWithMasterTable(mModuleName, "MODULENAME", "MODULEID", "GEN_MODULE_MST", PubDBCn, MasterNo, , "STATUS='O'") = True Then
                mModuleID = MasterNo
            End If


            SprdMain.Col = ColAdd
            UserRightSTR = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "A", "")

            SprdMain.Col = ColModify
            UserRightSTR = UserRightSTR & IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "M", "")


            SprdMain.Col = ColDelete
            UserRightSTR = UserRightSTR & IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "D", "")


            SprdMain.Col = ColView
            UserRightSTR = UserRightSTR & IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "V", "")


            SprdMain.Col = ColAuthorised
            UserRightSTR = UserRightSTR & IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "S", "")

            SprdMain.Col = ColPrint
            UserRightSTR = UserRightSTR & IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "P", "")


            If PubSuperUser = "G" Then
                If UserRightSTR <> "" Then
                    UserRightSTR = "V"
                End If
            End If

            SprdMain.Col = ColMenu
            Menu_Renamed = UCase(SprdMain.Text)

            ''14-06-2006	
            '        If UCase(txtUserId.Text) = UCase("SUPER") Then UserRightSTR = "AMDV"	
            If Delete1(mUserId, mModuleID, Menu_Renamed) = False Then GoTo LabelSave
            If UserRightSTR <> "" Then
                SqlStr = ""
                SqlStr = " Insert Into FIN_Rights_MST (COMPANY_CODE, UserID, " & vbCrLf & " MODULEID,MenuHead, Rights) Values (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mUserId & "'," & vbCrLf & " " & mModuleID & ",'" & Menu_Renamed & "', '" & UserRightSTR & "')"
                PubDBCn.Execute(SqlStr)
            End If
LabelSave:

        Next

        mFillData = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        mFormLoad = True
        CmdSave.Enabled = False
        Exit Sub
ErrSave:
        '    Resume	
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''	
    End Sub

    Private Function Delete1(ByRef pUserID As String, ByRef pModuleID As Integer, ByRef pMenu As String) As Boolean
        On Error GoTo ErrSave
        Dim mSqlStr As String = ""

        Delete1 = False

        mSqlStr = " Delete From FIN_Rights_MST " & vbCrLf _
                  & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                  & " AND UserID='" & MainClass.AllowSingleQuote(pUserID) & "'" & vbCrLf _
                  & " AND ModuleID=" & pModuleID & " " & vbCrLf _
                  & " AND MENUHEAD='" & MainClass.AllowSingleQuote(pMenu) & "' "

        PubDBCn.Execute(mSqlStr)

        Delete1 = True

        Exit Function
ErrSave:
        Delete1 = False
    End Function
    Private Sub frmMnuRightsNew_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim SqlStr As String = ""
        Call SetMainFormCordinate(Me)

        MainClass.SetControlsColor(Me)

        txtModuleName.Text = ""
        chkAllModule.CheckState = System.Windows.Forms.CheckState.Checked
        txtModuleName.Enabled = False
        cmdsearchModule.Enabled = False

        txtMenuName.Text = ""
        chkAllMenu.CheckState = System.Windows.Forms.CheckState.Checked
        txtMenuName.Enabled = False
        cmdsearchMenu.Enabled = False

        chkAllUserID.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtUserId.Enabled = True

        Clear1()
        OptRights(0).Checked = True
        SqlStr = " Select * From FIN_Rights_MST " & vbCrLf & " Where UserID='" & UCase(txtUserId.Text) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, rsRights, ADODB.LockTypeEnum.adLockOptimistic)
        MainClass.SetControlsColor(Me)
        mFormLoad = True
    End Sub

    Private Sub frmMnuRightsNew_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        ''    rsRights.Close	
        '    Set rsRights = Nothing	
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmMnuRightsNew_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub OptRights_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptRights.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptRights.GetIndex(eventSender)
            Dim I, j As Object
            Dim X As Integer

            j = SprdMain.MaxRows

            For I = 1 To j
                SprdMain.Row = I
                For X = ColAdd To ColView
                    SprdMain.Col = X
                    SprdMain.Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next X
            Next I

            CmdSave.Enabled = True
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        CmdSave.Enabled = True
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent



        CmdSave.Enabled = True
        cntSearchRow = 1
        If eventArgs.row = 0 And eventArgs.col = ColMenuName Then
            mSearchKey = ""
            mSearchKey = InputBox("Enter Menu Name :", "Search", mSearchKey)
            MainClass.SearchIntoGrid(SprdMain, ColMenuName, mSearchKey, cntSearchRow)
            cntSearchRow = cntSearchRow + 1
            SprdMain.Focus()
        End If

    End Sub
    Private Sub UserIDSearch()
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        ''If MainClass.SearchMaster("", "USERS", "USERNAME", SqlStr) = True Then	
        If MainClass.SearchGridMaster(txtUserId.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr) = True Then
            txtUserId.Text = AcName
            TxtUserID_Validating(txtUserId, New System.ComponentModel.CancelEventArgs(False))
            If SprdMain.Enabled = True Then SprdMain.Focus()
        End If
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""

        InsertDataIntoDummyTable()
        Report1.Reset()
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MenuReport.RPT"

        SqlStr = "Select Field1,Field2,Field3,Field4,Field5,Field6 " & vbCrLf & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE UserId='" & MainClass.AllowSingleQuote(PubUserID) & "' ORDER BY SUBROW"

        Report1.SQLQuery = SqlStr

        SetCrpt(Report1, Mode, 1, "Menu Right Report", Trim(txtUserId.Text) & " - " & Trim(LblUserName.Text))
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ReportErr:
        'Resume	
        MsgBox(Err.Description)
    End Sub

    Private Sub InsertDataIntoDummyTable()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mMenuHead As String = ""
        Dim mMenuName As String = ""
        Dim mAdd As String = ""
        Dim mModify As String = ""
        Dim mDelete As String = ""
        Dim mView As String = ""
        Dim mAuthorised As String = ""
        Dim mModuleName As String = ""
        Dim mPrint As String = ""

        'Dim PvtDBCn As ADODB.Connection	

        ''Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        SqlStr = " Delete From TEMP_PrintDummyData NOLOGGING" & vbCrLf & " Where USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColMenuName

                If .BackColor.equals(System.Drawing.Color.Lime) And .Font.Bold = True Then
                    mMenuHead = UCase(.Text)
                    mMenuName = ""
                Else
                    mMenuName = UCase(.Text)
                End If

                .Col = ColModuleName
                mModuleName = Trim(.Text)

                .Col = ColAdd
                mAdd = IIf(.Text = "1", "YES", "NO")

                .Col = ColModify
                mModify = IIf(.Text = "1", "YES", "NO")

                .Col = ColDelete
                mDelete = IIf(.Text = "1", "YES", "NO")

                .Col = ColView
                mView = IIf(.Text = "1", "YES", "NO")

                .Col = ColAuthorised
                mAuthorised = IIf(.Text = "1", "YES", "NO")

                .Col = ColPrint
                mPrint = IIf(.Text = "1", "YES", "NO")

                If mMenuName <> "" Then
                    SqlStr = " Insert Into TEMP_PrintDummyData " & vbCrLf & " (UserID,SubRow,Field1,Field2, " & vbCrLf & " Field3,Field4,Field5,Field6,Field7,Field8,Field9  ) Values ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " " & I & ",'" & mModuleName & "','" & MainClass.AllowSingleQuote(mMenuName) & "'," & vbCrLf & " '" & mAdd & "','" & mModify & "','" & mDelete & "', " & vbCrLf & " '" & mView & "','" & mMenuHead & "', '" & mAuthorised & "','" & mPrint & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next

        End With
        Exit Sub
ErrPart:
        '    Resume	
    End Sub
End Class
