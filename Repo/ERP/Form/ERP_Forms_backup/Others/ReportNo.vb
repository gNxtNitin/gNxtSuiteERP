Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmReportNo
    Inherits System.Windows.Forms.Form
    Dim RsReportNo As ADODB.Recordset

    Private Const ColMenuName As Short = 1
    Private Const ColDOC_NO As Short = 2
    Private Const ColDATE_ORIG As Short = 3
    Private Const ColREV_NO As Short = 4
    Private Const ColDATE_REV As Short = 5
    Private Const ColMenu As Short = 6

    Dim retval As Object
    Dim mnuCnt As Integer
    Private Const ConRowHeight As Short = 13
    Dim mSearchKey As String
    Dim cntSearchRow As Integer
    Private Sub Show1()

        On Error GoTo Errshow1
        Dim SqlStr As String = ""

        SqlStr = "Select * From ATH_REPORT_NO_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        '            & " Where MODULEID=" & Val(LblModuleId.text) & " " & vbCrLf _
        '& " And COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReportNo, ADODB.LockTypeEnum.adLockReadOnly)
        Dim k As Short
        If RsReportNo.EOF = False Then
            RsReportNo.MoveFirst()
            Do While Not RsReportNo.EOF

                For k = 1 To SprdMain.MaxRows
                    SprdMain.Row = k
                    SprdMain.Col = ColMenu
                    If UCase(SprdMain.Text) = UCase(RsReportNo.Fields("MenuHead").Value) Then
                        SprdMain.Col = ColDOC_NO
                        SprdMain.Text = IIf(IsDbNull(RsReportNo.Fields("DOC_NO").Value), "", RsReportNo.Fields("DOC_NO").Value)

                        SprdMain.Col = ColDATE_ORIG
                        SprdMain.Text = IIf(IsDbNull(RsReportNo.Fields("DATE_ORIG").Value), "", RsReportNo.Fields("DATE_ORIG").Value)

                        SprdMain.Col = ColREV_NO
                        SprdMain.Text = IIf(IsDbNull(RsReportNo.Fields("REV_NO").Value), "", RsReportNo.Fields("REV_NO").Value)

                        SprdMain.Col = ColDATE_REV
                        SprdMain.Text = IIf(IsDbNull(RsReportNo.Fields("DATE_REV").Value), "", RsReportNo.Fields("DATE_REV").Value)

                    End If
                Next
                RsReportNo.MoveNext()
            Loop
        End If
        Exit Sub
Errshow1:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)


        On Error GoTo ErrPart

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColMenuName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .set_ColWidth(ColMenuName, 35)
            .BlockMode = True

            .Col = ColDOC_NO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsReportNo.Fields("DOC_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColDOC_NO, 15)

            .Col = ColDATE_ORIG
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColDATE_ORIG, 10)

            .Col = ColREV_NO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsReportNo.Fields("REV_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColREV_NO, 10)

            .Col = ColDATE_REV
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(ColDATE_REV, 10)

            .Col = ColMenu
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .ColHidden = True

            MainClass.ProtectCell(SprdMain, Arow, .MaxRows, ColMenuName, ColMenuName)

            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Clear1()

        MainClass.ClearGrid(SprdMain, ConRowHeight)
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

        FormatSprdMain(-1)
        I = 1

        SqlStr = "SELECT A.*, B.MODULENAME " & vbCrLf _
                & " FROM GEN_ERPMENU_MST A, GEN_MODULE_MST B" & vbCrLf _
                & " WHERE A.MODULEID = B.MODULEID And STATUS='O' AND A.IS_ACTIVE='Y' AND IS_GROUP='Y'"


        SqlStr = SqlStr & vbCrLf & " And B.MODULENAME ='" & MainClass.AllowSingleQuote(cboModuleID.Text) & "'"

        SqlStr = SqlStr & vbCrLf & "ORDER BY A.MODULEID, A.SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = I

                    .Col = ColMenuName
                    .Text = IIf(IsDBNull(RsTemp.Fields("MENUHEADNAME").Value), "", RsTemp.Fields("MENUHEADNAME").Value)

                    .Col = ColMenu
                    .Text = IIf(IsDBNull(RsTemp.Fields("MENUHEADID").Value), "", RsTemp.Fields("MENUHEADID").Value)

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


        'Dim mMnu As System.Windows.Forms.Control
        'Dim I As Integer

        'Dim menuhnd As Integer
        ''Dim hwndCurrentWindow As Integer
        'Dim mMenuTitle As String


        'Dim lpClassName, lpWindowName As String
        ''for msgbox
        'Dim Response, Help, Style, Msg, Title, Ctxt, MyString As Object
        'the window to analyze comes from the option buttons
        'And Is stored in glApplicationName
        '        hwndCurrentWindow = FindWindow(vbNullString, Master.Text)
        'menuhnd = GetMenu(hwndCurrentWindow)


        '        mnuCnt = Master.Controls.Count()
        '        SprdMain.MaxRows = mnuCnt
        '        FormatSprdMain(-1)
        '        For I = 0 To mnuCnt - 1
        '            SprdMain.Row = I
        '            SprdMain.Col = ColMenuName
        '            If CType(Master.Controls(I), Object).Name = "Toolbar1" Or CType(Master.Controls(I), Object).Name = "ImageList1" Or CType(Master.Controls(I), Object).Name = "StatusBar1" Then I = I : GoTo Label1
        '            If CType(Master.Controls(I), Object).Text = "-" Then I = I : GoTo Label1
        '            mMenuTitle = RTrim(LTrim(RemoveSplChar((CType(Master.Controls(I), Object).Text))))
        '            SprdMain.Text = mMenuTitle
        '            If AnalyzeTopLevelMenus(menuhnd, mMenuTitle) = True Then
        '                SprdMain.Row = I
        '                SprdMain.Row2 = I
        '                SprdMain.Col = 1
        '                SprdMain.Col2 = SprdMain.MaxCols
        '                SprdMain.BlockMode = True
        '                SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)
        '                SprdMain.BackColor = System.Drawing.Color.Lime
        '                SprdMain.BlockMode = False

        '                SprdMain.Row = I

        '                SprdMain.Col = ColMenuName
        '                SprdMain.Text = UCase(SprdMain.Text)
        '            Else
        '                SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, False)
        '            End If

        '            SprdMain.Col = ColMenu
        '            SprdMain.Text = CType(Master.Controls(I), Object).Name
        'Label1:
        '        Next
        HideBlankRow()
        Exit Sub
ErrFillMenu:
        MsgBox(Err.Description)
    End Sub
    Private Sub cboModuleID_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboModuleID.SelectedIndexChanged
        If MainClass.ValidateWithMasterTable(cboModuleID.Text, "ModuleName", "ModuleID", "GEN_Module_MST", PubDBCn, MasterNo) = True Then
            LblModuleId.text = MasterNo
        Else
            LblModuleId.text = "-1"
        End If
        Call Clear1()
        Call FillModuleAndMenu()
        Call Show1()
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If SprdMain.MaxRows <= 0 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default : Exit Sub
        End If
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmReportNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        If KeyCode = System.Windows.Forms.Keys.F3 Then
            MainClass.SearchIntoGrid(SprdMain, ColMenuName, mSearchKey, cntSearchRow)
            cntSearchRow = cntSearchRow + 1
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F3 Then
            MainClass.SearchIntoGrid(SprdMain, ColMenuName, mSearchKey, cntSearchRow)
            cntSearchRow = cntSearchRow + 1
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrSave
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        Dim ICnt As Short


        Dim mMenuName As String
        Dim mDOC_NO As String
        Dim mDATE_ORIG As String
        Dim mREV_NO As String
        Dim mDATE_REV As String
        Dim mMenu As String
        ''Dim PvtDBCn As ADODB.Connection

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        If Val(LblModuleId.Text) < 0 Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mSqlStr = " DELETE FROM ATH_REPORT_NO_MST " & vbCrLf _
            & " WHERE MODULEID=" & Val(LblModuleId.Text) & " " & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        PubDBCn.Execute(mSqlStr)


        ''**************************
        For ICnt = 1 To SprdMain.MaxRows
            mMenu = ""

            SprdMain.Row = ICnt

            SprdMain.Col = ColMenuName
            If SprdMain.Text = "" Then GoTo LabelSave

            SprdMain.Col = ColDOC_NO
            mDOC_NO = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColDATE_ORIG
            mDATE_ORIG = VB6.Format(SprdMain.Text, "DD-MMM-YYYY")

            SprdMain.Col = ColREV_NO
            mREV_NO = MainClass.AllowSingleQuote(SprdMain.Text)

            SprdMain.Col = ColDATE_REV
            mDATE_REV = VB6.Format(SprdMain.Text, "DD-MMM-YYYY")

            SprdMain.Col = ColMenu
            mMenu = MainClass.AllowSingleQuote(UCase(SprdMain.Text))

            If mDOC_NO <> "" Or mDATE_ORIG <> "" Or mREV_NO <> "" Or mDATE_REV <> "" Then
                SqlStr = ""
                SqlStr = " INSERT INTO ATH_REPORT_NO_MST (COMPANY_CODE,   " & vbCrLf _
                    & " MODULEID,MenuHead, DOC_NO,DATE_ORIG,REV_NO,DATE_REV) " & vbCrLf _
                    & " Values (" & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(LblModuleId.Text) & ", " & vbCrLf _
                    & " '" & mMenu & "', '" & mDOC_NO & "',TO_DATE('" & mDATE_ORIG & "','DD-MON-YYYY'),'" & mREV_NO & "',TO_DATE('" & mDATE_REV & "','DD-MON-YYYY'))"
                PubDBCn.Execute(SqlStr)
            End If
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

    Private Sub frmReportNo_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim SqlStr As String = ""

        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(6000)
        'Me.Width = VB6.TwipsToPixelsX(7905)

        SqlStr = "Select * From ATH_REPORT_NO_MST WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReportNo, ADODB.LockTypeEnum.adLockReadOnly)

        MainClass.SetControlsColor(Me)
        cboModuleID.Enabled = True
        Call FillModuleID()
    End Sub
    Private Sub FillModuleID()

        On Error GoTo FillERR
        Dim RsModule As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        cboModuleID.Items.Clear()
        SqlStr = "SELECT MODULENAME FROM GEN_Module_MST"    ''  WHERE MODULENAME='" & CurrModuleName & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModule, ADODB.LockTypeEnum.adLockReadOnly)
        With RsModule
            If Not .EOF Then
                Do While Not .EOF
                    cboModuleID.Items.Add(IIf(IsDbNull(RsModule.Fields("MODULENAME").Value), "", RsModule.Fields("MODULENAME").Value))
                    .MoveNext()
                Loop
                cboModuleID.SelectedIndex = 0
            Else
                cboModuleID.SelectedIndex = -1
            End If
        End With
        Exit Sub
FillERR:
        '    Resume
        MsgBox(Err.Description)
    End Sub

    Private Sub frmReportNo_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        RsReportNo.Close()
        RsReportNo = Nothing
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmReportNo_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        CmdSave.Enabled = True
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        CmdSave.Enabled = True
        cntSearchRow = 1
        If eventArgs.Row = 0 And eventArgs.Col = ColMenuName Then
            mSearchKey = ""
            mSearchKey = InputBox("Enter Menu Name :", "Search", mSearchKey)
            MainClass.SearchIntoGrid(SprdMain, ColMenuName, mSearchKey, cntSearchRow)
            cntSearchRow = cntSearchRow + 1
            SprdMain.Focus()
        End If
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mDOC_NO As String
        Dim mDATE_ORIG As String
        Dim mREV_NO As String
        Dim mDATE_REV As String

        InsertDataIntoDummyTable()
        MainClass.ClearCRptFormulas(Report1)
        Report1.Reset()
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MenuReport.RPT"

        SqlStr = "Select Field1,Field2,Field3,Field4,Field5,Field6 " & vbCrLf & " FROM TEMP_PrintDummyData PrintDummyData " & vbCrLf & " WHERE UserId='" & MainClass.AllowSingleQuote(PubUserID) & "' ORDER BY SUBROW"

        Report1.SQLQuery = SqlStr

        With SprdMain
            .Row = 0

            .Col = ColDOC_NO
            mDOC_NO = .Text

            .Col = ColDATE_ORIG
            mDATE_ORIG = .Text

            .Col = ColREV_NO
            mREV_NO = .Text

            .Col = ColDATE_REV
            mDATE_REV = .Text

        End With

        MainClass.AssignCRptFormulas(Report1, "FieldHead1=""" & mDOC_NO & """")
        MainClass.AssignCRptFormulas(Report1, "FieldHead2=""" & mDATE_ORIG & """")
        MainClass.AssignCRptFormulas(Report1, "FieldHead3=""" & mREV_NO & """")
        MainClass.AssignCRptFormulas(Report1, "FieldHead4=""" & mDATE_REV & """")

        SetCrpt(Report1, Mode, 1, "Reports Documents No. Report", Trim(cboModuleID.Text))
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub InsertDataIntoDummyTable()

        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mMenuHead As String = ""
        Dim mMenuName As String
        Dim mDOC_NO As String
        Dim mDATE_ORIG As String
        Dim mREV_NO As String
        Dim mDATE_REV As String

        ''Dim PvtDBCn As ADODB.Connection

        'Set PvtDBCn = New ADODB.Connection
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

                SprdMain.Col = ColDOC_NO
                mDOC_NO = MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Col = ColDATE_ORIG
                mDATE_ORIG = VB6.Format(SprdMain.Text, "DD-MM-YYYY")

                SprdMain.Col = ColREV_NO
                mREV_NO = MainClass.AllowSingleQuote(SprdMain.Text)

                SprdMain.Col = ColDATE_REV
                mDATE_REV = VB6.Format(SprdMain.Text, "DD-MM-YYYY")

                If mMenuName <> "" Then
                    SqlStr = " Insert Into TEMP_PrintDummyData " & vbCrLf _
                        & " (UserID,SubRow,Field1,Field2, " & vbCrLf _
                        & " Field3,Field4,Field5,Field6,Field7 ) Values ( " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " " & I & ",'" & cboModuleID.Text & "','" & mMenuName & "'," & vbCrLf _
                        & " '" & mDOC_NO & "',TO_DATE('" & mDATE_ORIG & "','DD-MON-YYYY'),'" & mREV_NO & "', " & vbCrLf _
                        & " TO_DATE('" & mDATE_REV & "','DD-MON-YYYY'),'" & mMenuHead & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next

        End With
    End Sub
End Class
