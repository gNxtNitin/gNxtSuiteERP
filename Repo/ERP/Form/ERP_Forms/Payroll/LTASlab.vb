Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLTASlab
    Inherits System.Windows.Forms.Form
    Dim RsLTA As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean

    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Dim xWEF As String

    Private Const ColMin As Short = 1
    Private Const ColMax As Short = 2
    Private Const ColLTAAmount As Short = 3
    Private Sub Clear1()

        txtWEF.Text = ""
        txtLTAAmount.Text = ""
        txtLTAPer.Text = ""
        txtWLTAPer.Text = ""
        optWorkerSlab(0).Checked = True
        txtWEF.Enabled = True

        MainClass.ClearGrid(SprdMain)
        MainClass.ButtonStatus(Me, XRIGHT, RsLTA, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            If RsLTA.EOF = False Then RsLTA.MoveFirst()
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
            MainClass.ButtonStatus(Me, XRIGHT, RsLTA, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = " SELECT DISTINCT TO_CHAR(WEF_DATE,'DD/MM/YYYY') AS WEF " & vbCrLf & " FROM PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

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
        MainClass.ButtonStatus(Me, XRIGHT, RsLTA, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub frmLTASlab_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub optWorkerSlab_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optWorkerSlab.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optWorkerSlab.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            txtWLTAPer.Enabled = IIf(optWorkerSlab(1).Checked = True, True, False)
            txtLTAAmount.Enabled = IIf(optWorkerSlab(0).Checked = True, True, False)

        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdMain.ClickEvent
        sprdMain.Row = eventArgs.row
        If eventArgs.Col = 0 Then
            If MsgQuestion("Are sure to delete the row? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                SprdMain.Action = SS_ACTION_DELETE_ROW
                SprdMain.MaxRows = SprdMain.MaxRows - 1
            End If
        End If
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mMaxAmount As Double
        Dim mMinAmount As Double

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdMain.Row = eventArgs.row
        If eventArgs.col = ColMax Or eventArgs.col = ColMin Then
            If eventArgs.row > 1 Then
                sprdMain.Row = eventArgs.row - 1

                sprdMain.Col = ColMin
                mMinAmount = Val(sprdMain.Text)

                sprdMain.Col = ColMax
                mMaxAmount = Val(sprdMain.Text)

                sprdMain.Row = eventArgs.row
                sprdMain.Col = ColMin
                If Val(sprdMain.Text) <= mMaxAmount And Val(sprdMain.Text) <> 0 Then
                    MsgInformation("Please enter the vaild Value.")
                    MainClass.SetFocusToCell(sprdMain, eventArgs.row, ColMin)
                    Exit Sub
                End If
            End If

            If CheckMinMaxLimit(eventArgs.row, eventArgs.col) = True Then
                MainClass.AddBlankSprdRow(sprdMain, ColMin)
            End If
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub frmLTASlab_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FormActive = True Then Exit Sub
        SqlStr = "Select * From PAY_LTA_MST Where 1<>1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTA, ADODB.LockTypeEnum.adLockOptimistic)
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
    Private Sub frmLTASlab_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(6000)
        Me.Width = VB6.TwipsToPixelsX(6315)
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmLTASlab_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsLTA = Nothing
        'Set PvtDBCn = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        If RsLTA.EOF = False Then
            With RsLTA
                txtWEF.Text = VB6.Format(.Fields("WEF_DATE").Value, "DD/MM/YYYY")
                txtLTAAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("LTA_WORK_AMT").Value), 0, .Fields("LTA_WORK_AMT").Value), "0.00")
                txtLTAPer.Text = VB6.Format(IIf(IsDbNull(.Fields("LTA_PER").Value), 0, .Fields("LTA_PER").Value), "0.00")
                txtWLTAPer.Text = VB6.Format(IIf(IsDbNull(.Fields("LTA_WORK_PER").Value), 0, .Fields("LTA_WORK_PER").Value), "0.00")

                If .Fields("LTA_WORK_BASE_ON").Value = "A" Then
                    optWorkerSlab(0).Checked = True
                Else
                    optWorkerSlab(1).Checked = True
                End If

                cntRow = 1
                Do While Not RsLTA.EOF
                    SprdMain.Row = cntRow

                    SprdMain.Col = ColMin
                    SprdMain.Text = CStr(.Fields("MINLIMIT").Value)

                    SprdMain.Col = ColMax
                    SprdMain.Text = CStr(.Fields("MAXLIMIT").Value)

                    SprdMain.Col = ColLTAAmount
                    SprdMain.Text = CStr(.Fields("LTAAMT").Value)

                    cntRow = cntRow + 1
                    RsLTA.MoveNext()
                    SprdMain.MaxRows = cntRow
                Loop
            End With
            RsLTA.MoveFirst()
        End If

        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsLTA, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

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

        mBaseOn = IIf(optWorkerSlab(0).Checked = True, "A", "P")


        SqlStr = " DELETE FROM PAY_LTA_MST WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColMin
                mMin = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColMax
                mMax = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColLTAAmount
                mLTAAmt = IIf(IsNumeric(.Text), .Text, 0)


                SqlStr = " INSERT INTO PAY_LTA_MST " & vbCrLf & " (COMPANY_CODE , WEF_DATE, SUBROWNO, " & vbCrLf & " MINLIMIT , MAXLIMIT, LTAAMT,  " & vbCrLf & " LTA_WORK_AMT, LTA_PER," & vbCrLf & " LTA_WORK_PER, LTA_WORK_BASE_ON, " & vbCrLf & " ADDUSER, ADDDATE )  VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & cntRow & ", " & vbCrLf & " " & mMin & "," & mMax & ", " & vbCrLf & " " & mLTAAmt & ", " & vbCrLf & " " & Val(txtLTAAmount.Text) & ", " & Val(txtLTAPer.Text) & "," & vbCrLf & " " & Val(txtWLTAPer.Text) & ", '" & mBaseOn & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        RsLTA.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsLTA.Requery()
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
        SqlStr = " DELETE FROM PAY_LTA_MST WHERE " & vbCrLf & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " WEF_DATE = TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsLTA.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsLTA.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 1.5)
            .Row = mRow
            .MaxCols = ColLTAAmount
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColMin
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.99")
            .TypeFloatMin = CDbl("-99999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMin, 16)


            .Col = ColMax
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.99")
            .TypeFloatMin = CDbl("-99999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMin, 16)

            .Col = ColLTAAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColLTAAmount, 10)

        End With
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMin, ColLTAAmount)
        MainClass.SetSpreadColor(SprdMain, mRow)
        '    MainClass.ButtonStatus Me, XRIGHT, RsLTA, ADDMode, MODIFYMode, True
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function CheckMinMaxLimit(ByRef mRow As Integer, ByRef mCol As Integer) As Boolean
        CheckMinMaxLimit = False
        With SprdMain
            .Row = mRow
            .Col = ColMin
            If Val(.Text) > 0 Then
                .Col = ColMax
                If Val(.Text) > 0 Then
                    CheckMinMaxLimit = True
                End If
            End If
        End With
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
    Private Sub txtLTAAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLTAAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtLTAAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLTAAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtLTAPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLTAPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLTAPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLTAPer.KeyPress
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

        If MODIFYMode = True And RsLTA.EOF = False Then xWEF = RsLTA.Fields("WEF_DATE").Value
        SqlStr = ""

        SqlStr = " SELECT *  FROM PAY_LTA_MST WHERE " & vbCrLf & " Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " WEF_DATE = TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ORDER BY SUBROWNO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTA, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLTA.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_LTA_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND WEF_DATE=TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTA, ADODB.LockTypeEnum.adLockOptimistic)
            End If
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtWLTAPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWLTAPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWLTAPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWLTAPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
