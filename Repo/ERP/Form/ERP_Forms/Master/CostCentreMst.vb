Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCostCentreMst
   Inherits System.Windows.Forms.Form
   Dim RsCCMain As ADODB.Recordset
   Dim RsCCDetail As ADODB.Recordset
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   'Private PvtDBCn As ADODB.Connection

   Dim xCode As String
   Dim FormActive As Boolean
   Dim Shw As Boolean
   Dim MasterNo As Object
    Dim SqlStr As String = ""

    Private Const ConRowHeight As Short = 14

    Private Const ColUpdate As Short = 1
    Private Const ColDept As Short = 2
    Private Const ColDeptDesc As Short = 3

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
        MainClass.ButtonStatus(Me, XRIGHT, RsCCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtCode.Text = ""
        txtDesc.Text = ""
        ChkClickAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtCode.Enabled = True

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        Call AutoCompleteSearch("FIN_CCENTER_HDR", "CC_CODE", "", txtCode)
        Call AutoCompleteSearch("FIN_CCENTER_HDR", "CC_DESC", "", txtDesc)

        MainClass.ButtonStatus(Me, XRIGHT, RsCCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub ChkClickAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkClickAll.CheckStateChanged
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mChecked As String

        mChecked = IIf(ChkClickAll.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColUpdate
                .Value = IIf(mChecked = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            Next
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
            txtCode.Enabled = False
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
            txtCode.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "FIN_CCENTER_HDR", (txtCode.Text), RsCCMain, "CC_CODE") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_CCENTER_HDR", "CC_CODE", (txtCode.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM FIN_CCENTER_DET " & vbCrLf _
                  & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                  & " AND CC_CODE='" & MainClass.AllowSingleQuote(UCase((txtCode.Text))) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM FIN_CCENTER_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND CC_CODE='" & MainClass.AllowSingleQuote(UCase((txtCode.Text))) & "'"


        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsCCMain.Requery() ''.Refresh	
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''	
        RsCCMain.Requery() ''.Refresh	
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsCCMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                If Delete1() = False Then GoTo DelErrPart
                If RsCCMain.EOF = True Then
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
    Private Sub frmCostCentreMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmCostCentreMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim SqlStr As String = ""

        '    SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""	
        '    If eventArgs.Row = 0 And eventArgs.Col = ColDept Then	
        '        With SprdMain	
        '            .Row = .ActiveRow	
        '            .Col = ColDept	
        '	
        '            If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", "", "", SqlStr) = True Then	
        '                .Row = .ActiveRow	
        '	
        '                .Col = ColDept	
        '                .Text = Trim(AcName)	
        '	
        '                .Col = ColDeptDesc	
        '                .Text = Trim(AcName1)	
        '            End If	
        '            Call SprdMain_LeaveCell(ColDept, .ActiveRow, ColDeptDesc, .ActiveRow, False)	
        '        End With	
        '    End If	
        '	
        '    If eventArgs.Row = 0 And eventArgs.Col = ColDeptDesc Then	
        '        With SprdMain	
        '            .Row = .ActiveRow	
        '            .Col = ColDeptDesc	
        '            If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "", "", SqlStr) = True Then	
        '                .Row = .ActiveRow	
        '	
        '                .Col = ColDept	
        '                .Text = Trim(AcName1)	
        '	
        '                .Col = ColDeptDesc	
        '                .Text = Trim(AcName)	
        '            End If	
        '            Call SprdMain_LeaveCell(ColDept, .ActiveRow, ColDeptDesc, .ActiveRow, False)	
        '        End With	
        '    End If	

        '    If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then	
        '        MainClass.DeleteSprdRow SprdMain, Row, ColDept	
        '        MainClass.SaveStatus Me, ADDMode, MODIFYMode	
        '    End If	
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        Dim mRow As Short

        mCol = SprdMain.ActiveCol
        mRow = SprdMain.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDept Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDept, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptDesc, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.Tab And mCol = ColDept Then
            Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDept, mRow, mCol + 1, mRow + 1, True))
        End If
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xDept As String
        If eventArgs.newRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColDept
        xDept = Trim(SprdMain.Text)
        If xDept = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColDept
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColDept
                xDept = Trim(SprdMain.Text)
                If xDept = "" Then Exit Sub
                If CheckDept() = True Then
                    If CheckDuplicateDept(xDept) = False Then
                        MainClass.AddBlankSprdRow(SprdMain, ColDept, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDept() As Boolean

        On Error GoTo CheckERR
        With SprdMain
            .Row = .ActiveRow
            .Col = ColDept
            If MainClass.ValidateWithMasterTable(.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then
                .Row = .ActiveRow
                .Col = ColDeptDesc
                .Text = CStr(MasterNo)
                CheckDept = True
            Else
                .Col = ColDeptDesc
                .Text = ""
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDept)
            End If
        End With
        Exit Function
CheckERR:
        MsgBox(Err.Description)
    End Function

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Function CheckDuplicateDept(ByRef pDept As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If pDept = "" Then CheckDuplicateDept = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColDept
                If UCase(Trim(.Text)) = UCase(Trim(pDept)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateDept = True
                        MsgInformation("Duplicate Deptt")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDept)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""


        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColUpdate
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8.5)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsCCDetail.Fields("DEPT_CODE").DefinedSize
            .TypeEditMultiLine = True

            .Col = ColDeptDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDept, ColDeptDesc)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtCode.Text = Trim(SprdView.Text)
        txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        txtCode.Text = VB6.Format(txtCode.Text, "000")

        If MODIFYMode = True And RsCCMain.EOF = False Then xCode = RsCCMain.Fields("CC_CODE").Value

        SqlStr = "SELECT * FROM FIN_CCENTER_HDR " & vbCrLf _
                  & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                  & " AND CC_CODE='" & MainClass.AllowSingleQuote(UCase((Trim(txtCode.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCCMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCCMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = True Then
                Call FillDeptInGrid()
            End If

            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Cost Center Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM FIN_CCENTER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CC_CODE='" & xCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCCMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If

        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmCostCentreMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From FIN_CCENTER_HDR Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCCMain, ADODB.LockTypeEnum.adLockReadOnly)
        MainClass.UOpenRecordSet("Select * From FIN_CCENTER_DET Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCCDetail, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCostCentreMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

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

        '     cboOverHeads.Clear	
        '     cboOverHeads.AddItem "ADMINISTRATION OVERHEAD"	
        '     cboOverHeads.AddItem "MANUFACTURING OVERHEAD"	
        '     cboOverHeads.AddItem "SELLING OVERHEAD"	
        '     cboOverHeads.ListIndex = 0	
        '	
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCostCentreMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsCCMain = Nothing
        RsCCMain.Close()
        RsCCDetail = Nothing
        RsCCDetail.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mDeptCode As String

        If Not RsCCMain.EOF Then
            Clear1()
            txtCode.Text = IIf(IsDBNull(RsCCMain.Fields("CC_CODE").Value), "", RsCCMain.Fields("CC_CODE").Value)
            txtDesc.Text = IIf(IsDBNull(RsCCMain.Fields("CC_DESC").Value), "", RsCCMain.Fields("CC_DESC").Value)

            xCode = RsCCMain.Fields("CC_CODE").Value

            Call ShowDetail1()
        End If
        txtCode.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsCCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
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
            SqlStr = "INSERT INTO FIN_CCENTER_HDR (" & vbCrLf _
                    & " COMPANY_CODE, CC_CODE, CC_DESC, " & vbCrLf _
                    & " ADDUSER, ADDDATE, MODUSER, MODDATE " & vbCrLf _
                    & " ) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), '', ''" & vbCrLf _
                    & " )"
        Else
            SqlStr = " UPDATE FIN_CCENTER_HDR  SET " & vbCrLf _
                    & " CC_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND CC_CODE= '" & xCode & "'"
        End If

        'If ADDMode = True Then	
        '	'        mCode = MainClass.AutoGenRowNo("FIN_TARRIF_MST", "Code", PubDBCn)
        'Else	
        '	Sqlstr = " UPDATE FIN_CCENTER_HDR  SET " & vbCrLf & " CC_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CC_CODE= '" & xCode & "'"
        'End If	


UpdatePart:
        PubDBCn.Execute(SqlStr)

        If UpdateDetail() = False Then GoTo UpdateError
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''	
        RsCCMain.Requery() ''.Refresh	
        RsCCDetail.Requery()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtCode.MaxLength = RsCCMain.Fields("CC_CODE").DefinedSize
        txtDesc.MaxLength = RsCCMain.Fields("CC_DESC").DefinedSize ''	
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtCode.Text) = "" Then
            MsgInformation("Cost Centre Code is empty. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDesc.Text) = "" Then
            MsgInformation("Cost Centre Description is empty. Cannot Save")
            txtDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsCCMain.EOF = True Then Exit Function

        If MainClass.ValidDataInGrid(SprdMain, ColDept, "S", "Please Check Deptt.") = False Then FieldsVarification = False : Exit Function


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 3)
            .set_ColWidth(1, 6)
            .set_ColWidth(2, 25)
            .set_ColWidth(3, 25)
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
        Dim SqlStr As String = ""
        mTitle = ""
        Report1.Reset()
        mTitle = "Cost Center Master"

        SqlStr = MakeSQL()

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\CostCMaster.rpt"
        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function MakeSQL() As String
        On Error GoTo ERR1

        MakeSQL = " SELECT CMST.CC_CODE AS CODE, CMST.CC_DESC , " & vbCrLf & " DEPT.DEPT_DESC " & vbCrLf & " FROM FIN_CCENTER_HDR CMST, FIN_CCENTER_DET CDTL, PAY_DEPT_MST DEPT" & vbCrLf & " WHERE CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CMST.COMPANY_CODE=CDTL.COMPANY_CODE(+)" & vbCrLf & " AND CMST.CC_CODE=CDTL.CC_CODE(+)" & vbCrLf & " AND CDTL.COMPANY_CODE=DEPT.COMPANY_CODE(+)" & vbCrLf & " AND CDTL.DEPT_CODE=DEPT.DEPT_CODE(+)"

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY CMST.CC_DESC, DEPT.DEPT_DESC "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub txtDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesc.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mDeptt As String
        Dim mUpdate As String

        SqlStr = ""
        SqlStr = " SELECT ID.DEPT_CODE AS CCDEPT, " & vbCrLf _
                 & " DEPT.DEPT_CODE, DEPT.DEPT_DESC " & vbCrLf _
                 & " FROM PAY_DEPT_MST DEPT, FIN_CCENTER_DET ID " & vbCrLf _
                 & " WHERE DEPT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                 & " AND DEPT.COMPANY_CODE=ID.COMPANY_CODE(+)" & vbCrLf _
                 & " AND DEPT.DEPT_CODE=ID.DEPT_CODE(+)" & vbCrLf _
                 & " AND ID.CC_CODE(+)='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                 & " ORDER BY DEPT.DEPT_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCCDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsCCDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I
                FormatSprdMain(I)

                SprdMain.Col = ColUpdate
                mUpdate = Trim(IIf(IsDBNull(.Fields("CCDEPT").Value), "", .Fields("CCDEPT").Value))
                SprdMain.Value = IIf(mUpdate = "", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                SprdMain.Col = ColDept
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))
                mDeptt = SprdMain.Text

                SprdMain.Col = ColDeptDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("DEPT_DESC").Value), "", .Fields("DEPT_DESC").Value))
                '            If MainClass.ValidateWithMasterTable(mDeptt, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then	
                '                SprdMain.Text = MasterNo	
                '            Else	
                '                SprdMain.Text = ""	
                '            End If	

                .MoveNext()
                If .EOF = False Then
                    I = I + 1
                    SprdMain.MaxRows = I
                End If
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mDept As String
        Dim mMinQty As Double
        Dim mMaxQty As Double
        Dim mUpdate As String

        PubDBCn.Execute("DELETE FROM FIN_CCENTER_DET " & vbCrLf _
                  & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                  & " AND CC_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                SprdMain.Col = ColDept
                mDept = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColUpdate
                mUpdate = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                SqlStr = ""
                If Trim(mDept) <> "" And mUpdate = "Y" Then
                    SqlStr = " INSERT INTO  FIN_CCENTER_DET ( " & vbCrLf _
                       & " COMPANY_CODE, CC_CODE, SERIAL_NO, DEPT_CODE) " & vbCrLf _
                       & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "'," & vbCrLf _
                       & " " & I & ",'" & mDept & "') "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub FillDeptInGrid()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Sqlstr = ""
        Sqlstr = " SELECT  " & vbCrLf _
           & " DEPT.DEPT_CODE, DEPT.DEPT_DESC " & vbCrLf _
           & " FROM PAY_DEPT_MST DEPT " & vbCrLf _
           & " WHERE DEPT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " ORDER BY DEPT.DEPT_DESC"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I
                FormatSprdMain(I)

                SprdMain.Col = ColUpdate
                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColDept
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))

                SprdMain.Col = ColDeptDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DEPT_DESC").Value), "", .Fields("DEPT_DESC").Value))

                .MoveNext()
                If .EOF = False Then
                    I = I + 1
                    SprdMain.MaxRows = I
                End If
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
