Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBonusCeiling
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection
    Dim RsCeil As ADODB.Recordset

    Dim SqlStr As String = ""
    Dim FormActive As Boolean

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchMaster((txtWEF.Text), "PAY_PFESICeiling_MST", "WEF", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & ConBonus & "") = True Then
            txtWEF.Text = AcName
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtWEF.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsCeil.EOF = False Then RsCeil.MoveFirst()
            Show1()
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCeil, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'ShowReport crptToPrinter
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtWEF.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsCeil.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsCeil.EOF = 0 Then
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
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
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
    Private Sub cboBonusRound_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboBonusRound.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmBonusCeiling_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormActive = True Then Exit Sub
        ShowCurrentCeilling()
    End Sub
    Private Sub frmBonusCeiling_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmBonusCeiling_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Height = VB6.TwipsToPixelsY(4170)
        Me.Width = VB6.TwipsToPixelsX(7080)
        Me.Left = 0
        Me.Top = 0

        FillRoundOff(cboBonusRound)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        Dim mCode As Integer
        Dim mCeiling As Double
        Dim mRate As Double
        Dim mEPF As Double
        Dim mPFund As Double
        Dim mRounOff As String
        Dim cntRow As Short
        Dim mEmplerCont As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_PFESICeiling_MST WHERE " & vbCrLf & " WEF = TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') AND " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & ConBonus & ""

        PubDBCn.Execute(SqlStr)

        mCode = ConBonus
        mCeiling = CDbl(txtBonusCeiling.Text)
        mRate = Val(txtBonusRate.Text)
        mEPF = 0
        mPFund = 0
        mRounOff = cboBonusRound.Text

        SqlStr = "INSERT INTO PAY_PFESICeiling_MST (COMPANY_CODE , WEF, CODE," & vbCrLf & " CEILING , Rate, EPF, PFUND, ROUNDOFF, " & vbCrLf & " EMPER_CONT, " & vbCrLf & " AddUser, AddDate ) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ",TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & vbCrLf & " " & mCode & ", " & mCeiling & "," & mRate & ", " & vbCrLf & " " & mEPF & ", " & mPFund & ",'" & mRounOff & "', " & vbCrLf & " 'C'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Update1 = True

        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function

    Private Sub Show1()

        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mCode As Integer

        If RsCeil.EOF = False Then
            Do While Not RsCeil.EOF
                txtWEF.Text = RsCeil.Fields("WEF").Value
                txtBonusCeiling.Text = MainClass.FormatRupees(RsCeil.Fields("ceiling"))
                txtBonusRate.Text = MainClass.FormatRupees(RsCeil.Fields("Rate"))


                RsCeil.MoveNext()
            Loop
        End If
        ADDMode = False
        MODIFYMode = False
        If RsCeil.BOF = False Then RsCeil.MoveFirst()
        MainClass.ButtonStatus(Me, XRIGHT, RsCeil, ADDMode, MODIFYMode, CmdAdd, CmdModify, cmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        FieldsVarification = True
        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Please enter the Vaild Date.")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Not IsNumeric(txtBonusCeiling.Text) Then
            MsgInformation("Please enter the Vaild PF Ceiling.")
            txtBonusCeiling.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Not IsNumeric(txtBonusRate.Text) Then
            MsgInformation("Please enter the Vaild PF Rate.")
            txtBonusRate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboBonusRound.Text) = "" Then
            MsgInformation("This Feild Cann't be empty.")
            cboBonusRound.Focus()
            FieldsVarification = False
            Exit Function
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
    End Function
    Private Sub FillRoundOff(ByRef mCboName As System.Windows.Forms.ComboBox)
        With mCboName
            .Items.Add("0")
            .Items.Add("0.0")
            .Items.Add("0.00")
            .SelectedIndex = 0
        End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow

        SqlStr = " SELECT * from PAY_PFESICeiling_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & ConBonus & " AND WEF=TO_DATE('" & VB6.Format(SprdView.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeil, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeil.EOF = False Then
            Show1()
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtBonusCeiling_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusCeiling.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBonusCeiling_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusCeiling.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBonusRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBonusRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mDate As String
        Dim SqlStr As String = ""
        If txtWEF.Text = "" Then GoTo EventExitSub
        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtWEF.Text = VB6.Format(txtWEF.Text, "dd/mm/yyyy")
        mDate = txtWEF.Text
        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_PFESICeiling_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & ConBonus & "" & vbCrLf & " AND  WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeil, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeil.EOF = False Then Show1()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub ShowCurrentCeilling()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_PFESICeiling_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & ConBonus & " AND  " & vbCrLf & " WEF=(SELECT MAX(WEF) " & vbCrLf & " FROM PAY_PFESICeiling_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & ConBonus & " AND  " & vbCrLf & " WEF<=TO_DATE('" & VB6.Format(txtWEF.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeil, ADODB.LockTypeEnum.adLockOptimistic)
        Show1()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub Clear1()

        txtWEF.Text = ""
        txtBonusCeiling.Text = "6500.00"
        txtBonusRate.Text = "8.33"
        MainClass.ButtonStatus(Me, XRIGHT, RsCeil, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsCeil, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = "Select TO_CHAR(WEF,'DD/MM/YYYY') AS WEF, Ceiling,Rate from PAY_PFESICeiling_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & ConBonus & " Order by wef"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 8)
            .set_ColWidth(3, 8)
            .set_ColWidth(4, 8)
            .set_ColWidth(5, 8)
            .set_ColWidth(6, 8)
            .set_ColWidth(7, 8)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_PFESICeiling_MST", (txtWEF.Text), RsCeil) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_PFESICeiling_MST", "WEF", VB6.Format(txtWEF.Text, "DD-MMM-YYYY")) = False Then GoTo DeleteErr

        SqlStr = "Delete from PAY_PFESICeiling_MST where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE=" & ConBonus & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsCeil.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        MsgBox(Err.Description)
        '     Resume
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsCeil.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If

    End Function
End Class