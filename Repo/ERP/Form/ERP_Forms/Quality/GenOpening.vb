Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGenOpening
    Inherits System.Windows.Forms.Form
    Dim RsGenRecOpen As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Dim xMenuID As String

    Private Const ColMachineNo As Short = 1
    Private Const ColMachineDesc As Short = 2
    Private Const ColReading As Short = 3
    Private Const ColUnitReading As Short = 4

    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsGenRecOpen, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        lblMkey.Text = ""
        txtDate.Text = ""
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsGenRecOpen, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsGenRecOpen.Fields("MACHINE_NO").DefinedSize

            .Col = ColMachineDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 250

            .Col = ColReading
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.99")
            .TypeFloatMin = CDbl("0.00")
            .TypeEditLen = RsGenRecOpen.Fields("OPEN_READING").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 2

            .Col = ColUnitReading
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.99")
            .TypeFloatMin = CDbl("0.00")
            .TypeEditLen = RsGenRecOpen.Fields("OPEN_UNIT_READING").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 2

            MainClass.SetSpreadColor(SprdMain, Arow)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMachineDesc, ColMachineDesc)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsGenRecOpen, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
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
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Call ViewGrid()
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsGenRecOpen.EOF = False Then RsGenRecOpen.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        ''Resume
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtDate.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsGenRecOpen.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_GENREC_OPEN", (txtDate.Text), RsGenRecOpen) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_GENREC_OPEN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND OPEN_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")
                PubDBCn.CommitTrans()
                RsGenRecOpen.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsGenRecOpen.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmGenOpening_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmGenOpening_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ErrPart
        Dim xMachineDesc As String

        If eventArgs.row = 0 And eventArgs.col = ColMachineNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColMachineNo
                If MainClass.SearchGridMaster(.Text, "MAN_MACHINE_MST", "MACHINE_NO", "MACHINE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'") = True Then
                    .Row = .ActiveRow
                    .Col = ColMachineNo
                    .Text = AcName
                    .Col = ColMachineDesc
                    .Text = AcName1
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColMachineNo)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColMachineDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColMachineDesc
                xMachineDesc = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'") = True Then
                    .Row = .ActiveRow
                    .Col = ColMachineDesc
                    .Text = AcName
                    .Col = ColMachineNo
                    .Text = AcName1
                Else
                    .Row = .ActiveRow
                    .Col = ColMachineDesc
                    .Text = xMachineDesc
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColMachineNo)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColMachineNo
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColMachineNo)
                MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColUnitReading Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColUnitReading
                If SprdMain.MaxRows = SprdMain.ActiveRow Then
                    MainClass.AddBlankSprdRow(SprdMain, ColMachineNo, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
                SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColMachineNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMachineNo, 0))
            If mActiveCol = ColMachineDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMachineDesc, 0))
        End If
        eventArgs.KeyCode = 9999
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColMachineNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMachineNo, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColMachineDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMachineDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub

        With SprdMain
            .Row = .ActiveRow
            Select Case eventArgs.Col
                Case ColMachineNo
                    .col = ColMachineNo
                    If Trim(.Text) = "" Then Exit Sub
                    If MainClass.ValidateWithMasterTable(.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'") = True Then
                        .Col = ColMachineDesc
                        .Text = MasterNo
                        If DuplicateMachine = False Then
                            FormatSprdMain(-1)
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColMachineNo)
                    End If
            End Select
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub

    Private Function DuplicateMachine() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckMachineNo As String
        Dim mMachineNo As String

        DuplicateMachine = False

        With SprdMain
            .Row = .ActiveRow
            .Col = ColMachineNo
            mCheckMachineNo = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMachineNo
                mMachineNo = Trim(UCase(.Text))

                If (mMachineNo = mCheckMachineNo And mCheckMachineNo <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateMachine = True
                    MsgInformation("Duplicate Machine : " & mCheckMachineNo)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColMachineNo)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtDate.Text = SprdView.Text
        txtDate_Validating(txtDate, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Public Sub frmGenOpening_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Machines Opening Meter Reading"

        SqlStr = " Select * From MAN_GENREC_OPEN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGenRecOpen, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume
    End Sub

    Private Sub frmGenOpening_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(7515)
        Me.Width = VB6.TwipsToPixelsX(9780)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmGenOpening_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsGenRecOpen.Close()
        RsGenRecOpen = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Clear1()

        If Not RsGenRecOpen.EOF Then
            IsShowing = True

            lblMkey.Text = IIf(IsDbNull(RsGenRecOpen.Fields("OPEN_DATE").Value), "", RsGenRecOpen.Fields("OPEN_DATE").Value)
            txtDate.Text = IIf(IsDbNull(RsGenRecOpen.Fields("OPEN_DATE").Value), "", RsGenRecOpen.Fields("OPEN_DATE").Value)

            Call ShowReading()
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        MainClass.ButtonStatus(Me, XRIGHT, RsGenRecOpen, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowReading()

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim I As Integer

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM MAN_GENREC_OPEN " & vbCrLf & " WHERE OPEN_DATE=TO_DATE('" & VB6.Format(lblMkey.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColMachineNo
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("MACHINE_NO").Value), "", .Fields("MACHINE_NO").Value))

                MainClass.ValidateWithMasterTable(SprdMain.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                SprdMain.Col = ColMachineDesc
                SprdMain.Text = MasterNo

                SprdMain.Col = ColReading
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OPEN_READING").Value), "", .Fields("OPEN_READING").Value))

                SprdMain.Col = ColUnitReading
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OPEN_UNIT_READING").Value), "", .Fields("OPEN_UNIT_READING").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtDate_Validating(txtDate, New System.ComponentModel.CancelEventArgs(False))
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

        On Error GoTo UpdateERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mDate As String
        Dim mMachineNo As String
        Dim mReading As Double
        Dim mUnitReading As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mDate = txtDate.Text

        PubDBCn.Execute("DELETE FROM MAN_GENREC_OPEN WHERE OPEN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColMachineNo
                mMachineNo = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColReading
                mReading = Val(.Text)

                .Col = ColUnitReading
                mUnitReading = Val(.Text)

                SqlStr = ""
                If mMachineNo <> "" Then
                    SqlStr = " INSERT INTO MAN_GENREC_OPEN ( " & vbCrLf & " COMPANY_CODE,OPEN_DATE,MACHINE_NO,OPEN_READING,OPEN_UNIT_READING,ADDUSER,ADDDATE ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mMachineNo & "'," & mReading & "," & mUnitReading & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateERR:
        PubDBCn.RollbackTrans()
        Update1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtDate.Text) = "" Then
            MsgInformation("As on is empty, So unable to Save")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsGenRecOpen.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT OPEN_DATE AS AS_ON_DATE,MACHINE_NO,OPEN_READING,OPEN_UNIT_READING " & vbCrLf & " FROM MAN_GENREC_OPEN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY OPEN_DATE,MACHINE_NO"
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Machines Opening Meter Reading"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\GenOpen.rpt"

        SqlStr = " SELECT MAN_GENREC_OPEN.*, MAN_MACHINE_MST.MACHINE_DESC " & vbCrLf & " FROM MAN_GENREC_OPEN, MAN_MACHINE_MST " & vbCrLf & " WHERE MAN_GENREC_OPEN.COMPANY_CODE=MAN_MACHINE_MST.COMPANY_CODE " & vbCrLf & " AND MAN_GENREC_OPEN.MACHINE_NO=MAN_MACHINE_MST.MACHINE_NO " & vbCrLf & " AND MAN_GENREC_OPEN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MAN_GENREC_OPEN.OPEN_DATE=TO_DATE('" & VB6.Format(lblMkey.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Date
        Dim mDate As Date
        Dim SqlStr As String

        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        End If
        mDate = CDate(VB6.Format(txtDate.Text, "DD/MM/YYYY"))

        If MODIFYMode = True And RsGenRecOpen.BOF = False Then xMKey = RsGenRecOpen.Fields("OPEN_DATE").Value

        SqlStr = "SELECT * FROM MAN_GENREC_OPEN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND OPEN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGenRecOpen, ADODB.LockTypeEnum.adLockReadOnly)
        If RsGenRecOpen.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Date. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_GENREC_OPEN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND OPEN_DATE=TO_DATE('" & VB6.Format(xMKey, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGenRecOpen, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtDate.Maxlength = RsGenRecOpen.Fields("OPEN_DATE").DefinedSize - 6
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 3)
            .set_ColWidth(2, 500 * 5)
            .set_ColWidth(3, 500 * 5)
            .set_ColWidth(4, 500 * 5)
            .set_ColWidth(5, 500 * 4)
            .set_ColWidth(6, 500 * 4)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 5)
            .set_ColWidth(9, 500 * 1)
            .set_ColWidth(10, 500 * 3)
            .set_ColWidth(11, 500 * 3)
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
End Class
