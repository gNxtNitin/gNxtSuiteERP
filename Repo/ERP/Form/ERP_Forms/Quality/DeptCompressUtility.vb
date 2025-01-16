Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDeptCompressUtility
    Inherits System.Windows.Forms.Form
    Dim RsUtility As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Dim xMenuID As String

    Private Const ColDeptCode As Short = 1
    Private Const ColDeptDesc As Short = 2
    Private Const ColRatio As Short = 3

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
        MainClass.ButtonStatus(Me, XRIGHT, RsUtility, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()


        txtDate.Text = ""
        txtMachineNo.Text = ""
        txtMachineNo.Enabled = True
        txtDate.Enabled = True
        cmdSearchMachine.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsUtility, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsUtility.Fields("DEPT_CODE").DefinedSize

            .Col = ColDeptDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 250

            .Col = ColRatio
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999999999.99")
            .TypeFloatMin = CDbl("0.00")
            .TypeEditLen = RsUtility.Fields("DEPT_RATIO").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 2

            MainClass.SetSpreadColor(SprdMain, Arow)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDeptDesc, ColDeptDesc)
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
            MainClass.ButtonStatus(Me, XRIGHT, RsUtility, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            If RsUtility.EOF = False Then RsUtility.MoveFirst()
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
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtDate.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsUtility.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_DEPT_UTILITY_MST", (txtDate.Text), RsUtility) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_DEPT_UTILITY_MST " & vbCrLf _
                                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' AND WEF_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")

                PubDBCn.CommitTrans()
                RsUtility.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsUtility.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmDeptCompressUtility_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmDeptCompressUtility_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
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
        Dim xDeptDesc As String

        If eventArgs.row = 0 And eventArgs.col = ColDeptCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptCode
                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCode
                    .Text = AcName
                    .Col = ColDeptDesc
                    .Text = AcName1
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDeptCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColDeptDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptDesc
                xDeptDesc = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptDesc
                    .Text = AcName
                    .Col = ColDeptCode
                    .Text = AcName1
                Else
                    .Row = .ActiveRow
                    .Col = ColDeptDesc
                    .Text = xDeptDesc
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDeptCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColDeptCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColDeptCode)
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
            If mActiveCol = ColRatio Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColRatio
                If SprdMain.MaxRows = SprdMain.ActiveRow Then
                    MainClass.AddBlankSprdRow(SprdMain, ColDeptCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
                SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
            If mActiveCol = ColDeptDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptDesc, 0))
        End If
        eventArgs.KeyCode = 9999
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub

        With SprdMain
            .Row = .ActiveRow
            Select Case eventArgs.Col
                Case ColDeptCode
                    eventArgs.col = ColDeptCode
                    If Trim(.Text) = "" Then Exit Sub
                    If MainClass.ValidateWithMasterTable(.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                        .Col = ColDeptDesc
                        .Text = MasterNo
                        If DuplicateDept() = False Then
                            FormatSprdMain(-1)
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDeptCode)
                    End If
                Case ColRatio
                    .Col = ColRatio
                    If Val(.Text) > 100 Then
                        MsgInformation("Department Ratio Cann't be Greater Than 100")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColRatio)
                        Exit Sub
                    End If
            End Select
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Function DuplicateDept() As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckDeptCode As String
        Dim mDeptCode As String

        DuplicateDept = False

        With SprdMain
            .Row = .ActiveRow
            .Col = ColDeptCode
            mCheckDeptCode = Trim(UCase(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColDeptCode
                mDeptCode = Trim(UCase(.Text))

                If (mDeptCode = mCheckDeptCode And mCheckDeptCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateDept = True
                    MsgInformation("Duplicate Dept : " & mCheckDeptCode)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDeptCode)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = eventArgs.row
        SprdView.Col = 1
        txtMachineNo.Text = SprdView.Text
        SprdView.Col = 2
        txtDate.Text = SprdView.Text
        txtDate_Validating(txtDate, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Public Sub frmDeptCompressUtility_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Department Wise Compressor Utility"

        SqlStr = " Select * From MAN_DEPT_UTILITY_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsUtility, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmDeptCompressUtility_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Width = VB6.TwipsToPixelsX(8460)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmDeptCompressUtility_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsUtility.Close()
        RsUtility = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String

        Clear1()

        If Not RsUtility.EOF Then
            IsShowing = True

            txtDate.Text = IIf(IsDbNull(RsUtility.Fields("WEF_DATE").Value), "", RsUtility.Fields("WEF_DATE").Value)
            txtMachineNo.Text = Trim(IIf(IsDbNull(RsUtility.Fields("MACHINE_NO").Value), "", RsUtility.Fields("MACHINE_NO").Value))

            lblMachineNo.Text = ""
            If ADDMode = True Then
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' AND STATUS='O' "
            Else
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' "
            End If

            If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                lblMachineNo.text = MasterNo
            End If

            Call ShowDetail1((txtMachineNo.Text), (txtDate.Text))

            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtMachineNo.Enabled = False
        txtDate.Enabled = False
        cmdSearchMachine.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsUtility, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowDetail1(ByRef pMachineNo As String, ByRef pDate As String)

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim I As Integer

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM MAN_DEPT_UTILITY_MST " & vbCrLf & " WHERE MACHINE_NO='" & MainClass.AllowSingleQuote(pMachineNo) & "'" & vbCrLf & " AND WEF_DATE='" & VB6.Format(pDate, "DD-MMM-YYYY") & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColDeptCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))

                MainClass.ValidateWithMasterTable(SprdMain.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                SprdMain.Col = ColDeptDesc
                SprdMain.Text = MasterNo

                SprdMain.Col = ColRatio
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DEPT_RATIO").Value), "", .Fields("DEPT_RATIO").Value))

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
        Dim mDeptCode As String
        Dim mReading As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mDate = txtDate.Text

        PubDBCn.Execute("DELETE FROM MAN_DEPT_UTILITY_MST WHERE WEF_DATE=TO_DATE('" & vb6.Format(mDate, "DD-MMM-YYYY") & "' AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' AND COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value)


        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDeptCode
                mDeptCode = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColRatio
                mReading = Val(.Text)

                SqlStr = ""
                If mDeptCode <> "" Then
                    SqlStr = " INSERT INTO MAN_DEPT_UTILITY_MST ( " & vbCrLf & " COMPANY_CODE, MACHINE_NO, WEF_DATE, DEPT_CODE, DEPT_RATIO, ADDUSER, ADDDATE ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "',  '" & VB6.Format(mDate, "DD-MMM-YYYY") & "'," & vbCrLf & " '" & mDeptCode & "'," & mReading & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
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
        Dim CntRow As Integer
        Dim mRatio As Double
        Dim mTotRatio As Double

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
            Exit Function
        End If

        If MODIFYMode = True And RsUtility.EOF = True Then Exit Function
        mTotRatio = 0

        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColRatio
                mRatio = Val(.Text)
                If mRatio > 100 Then
                    MsgInformation("Department Ratio Cann't be Greater Than 100")
                    FieldsVarification = False
                    Exit Function
                End If

                mTotRatio = mTotRatio + mRatio
                If mTotRatio > 100 Then
                    MsgInformation("Total Ratio Cann't be Greater Than 100")
                    FieldsVarification = False
                    Exit Function
                End If
            Next
        End With

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT MACHINE_NO, WEF_DATE, DEPT_CODE, DEPT_RATIO " & vbCrLf & " FROM MAN_DEPT_UTILITY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY WEF_DATE, MACHINE_NO, DEPT_CODE"
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Dept Wise Power Opening Meter Reading"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PowerOpen.rpt"

        SqlStr = " SELECT MAN_DEPT_UTILITY_MST.*, PAY_DEPT_MST.DEPT_DESC " & vbCrLf & " FROM MAN_DEPT_UTILITY_MST, PAY_DEPT_MST " & vbCrLf & " WHERE MAN_DEPT_UTILITY_MST.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE " & vbCrLf & " AND MAN_DEPT_UTILITY_MST.DEPT_CODE=PAY_DEPT_MST.DEPT_CODE " & vbCrLf & " AND MAN_DEPT_UTILITY_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MAN_DEPT_UTILITY_MST.WEF_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

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
        Dim xMachineNo As String
        Dim xDate As Date
        Dim mDate As Date
        Dim SqlStr As String

        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        End If
        mDate = CDate(VB6.Format(txtDate.Text, "DD/MM/YYYY"))

        If MODIFYMode = True And RsUtility.BOF = False Then
            xMachineNo = RsUtility.Fields("MACHINE_NO").Value
            xDate = RsUtility.Fields("WEF_DATE").Value
        End If

        SqlStr = "SELECT * FROM MAN_DEPT_UTILITY_MST " _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'" & vbCrLf _
                    & " AND WEF_DATE=TO_DATE('" & vb6.Format(mDate, "DD-MMM-YYYY") & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsUtility, ADODB.LockTypeEnum.adLockReadOnly)
        If RsUtility.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Date. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_DEPT_UTILITY_MST " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(xMachineNo) & "'" & vbCrLf & " AND WEF_DATE='" & VB6.Format(xDate, "DD-MMM-YYYY") & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsUtility, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtDate.Maxlength = RsUtility.Fields("WEF_DATE").DefinedSize - 6
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 3)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
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

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.DoubleClick
        Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValMacERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub

        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' AND STATUS='O' "
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' "
        End If
        If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Machine Does Not Exist In Master.")
            Cancel = True
            Exit Sub
        Else
            lblMachineNo.text = MasterNo
        End If

        Call txtDate_Validating(txtDate, New System.ComponentModel.CancelEventArgs(False))
        GoTo EventExitSub
ValMacERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchMachine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachine.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' AND STATUS='O' "
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtMachineNo.Text = AcName1
            lblMachineNo.text = AcName
            If txtMachineNo.Enabled = True Then txtMachineNo.Focus()
        End If
    End Sub
End Class
