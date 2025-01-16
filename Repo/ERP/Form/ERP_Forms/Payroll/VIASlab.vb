Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmVIASlab
    Inherits System.Windows.Forms.Form
    Dim RSVIA As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean

    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Dim xWEF As String
    Private Sub Clear1()

        txtWEF.Text = ""
        txt80CSlab.Text = ""
        txtWEF.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RSVIA, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If txtWEF.Enabled = True Then txtWEF.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RSVIA.EOF = False Then RSVIA.MoveFirst()
            Show1()
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1 = False Then GoTo DelErrPart
            Clear1()
        End If

        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RSVIA, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = " SELECT DISTINCT TO_CHAR(WEF_DATE,'DD/MM/YYYY') AS WEF, SLAB_80C " & vbCrLf & " FROM PAY_VIA_SLAB_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 16)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RSVIA, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub frmVIASlab_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmVIASlab_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FormActive = True Then Exit Sub
        SqlStr = "Select * From PAY_VIA_SLAB_MST Where 1<>1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSVIA, ADODB.LockTypeEnum.adLockOptimistic)
        AssignGrid(False)

        txtWEF.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmVIASlab_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(3390)
        Me.Width = VB6.TwipsToPixelsX(6315)
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmVIASlab_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RSVIA = Nothing
        'Set PvtDBCn = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        If RSVIA.EOF = False Then
            With RSVIA
                txtWEF.Text = VB6.Format(.Fields("WEF_DATE").Value, "DD/MM/YYYY")
                txt80CSlab.Text = VB6.Format(IIf(IsDbNull(.Fields("SLAB_80C").Value), 0, .Fields("SLAB_80C").Value), "0.00")
                txtWEF.Enabled = False
            End With
        End If

        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RSVIA, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If Update1 = True Then
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
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
        Dim cntRow As Integer
        Dim mMax As Double
        Dim mMin As Double
        Dim mTax As Double
        Dim mSurcharge As Double
        Dim mLTAAmt As Double
        Dim mBaseOn As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = " DELETE FROM PAY_VIA_SLAB_MST WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)


        SqlStr = " INSERT INTO PAY_VIA_SLAB_MST " & vbCrLf & " (COMPANY_CODE , WEF_DATE, SLAB_80C, " & vbCrLf & " ADDUSER, ADDDATE )  VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txt80CSlab.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RSVIA.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RSVIA.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        SqlStr = " DELETE FROM PAY_VIA_SLAB_MST WHERE " & vbCrLf & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " WEF_DATE = TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RSVIA.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RSVIA.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtWEF.Text = SprdView.Text
        txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txt80CSlab_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txt80CSlab.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txt80CSlab_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txt80CSlab.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart

        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invalid Date")
            txtWEF.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        If MODIFYMode = True And RSVIA.EOF = False Then xWEF = RSVIA.Fields("WEF_DATE").Value
        SqlStr = ""

        SqlStr = " SELECT *  FROM PAY_VIA_SLAB_MST WHERE " & vbCrLf & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " WEF_DATE =TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSVIA, ADODB.LockTypeEnum.adLockOptimistic)

        If RSVIA.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_VIA_SLAB_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND WEF_DATE=TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSVIA, ADODB.LockTypeEnum.adLockOptimistic)
            End If
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
