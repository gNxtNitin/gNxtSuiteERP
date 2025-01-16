Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmIMTEPE
    Inherits System.Windows.Forms.Form
    Dim RsIMTEPEHdr As ADODB.Recordset
    Dim RsIMTEPEDet As ADODB.Recordset
    Dim RsIMTEPEStd As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Dim xMenuID As String

    Private Const ColParamDesc As Short = 1
    Private Const ColMinRange As Short = 2
    Private Const ColMaxRange As Short = 3
    Private Const ColPerError As Short = 4

    Private Const ColStepParamDesc As Short = 1
    Private Const ColStepReadingStep As Short = 2
    Private Const ColStepPerError As Short = 3

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
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTEPEHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtDescription.Text = ""
        txtLC.Text = ""
        txtCalibProc.Text = ""
        txtReadingStepDec.Text = ""
        txtObservationDec.Text = ""
        Call MakeEnableDeField(True)
        MainClass.ClearGrid(SprdPE, ConRowHeight)
        MainClass.ClearGrid(SprdStep, ConRowHeight)
        FormatSprdPE(-1)
        FormatSprdStep(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTEPEHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        txtDescription.Enabled = mMode
        cmdSearchDescription.Enabled = mMode
        txtLC.Enabled = mMode
        cmdSearchLC.Enabled = mMode
    End Sub

    Private Sub FormatSprdPE(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdPE
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIMTEPEDet.Fields("PARAM_DESC").DefinedSize

            .Col = ColMinRange
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.9999")
            .TypeFloatMin = CDbl("-99999.9999")
            .TypeEditLen = RsIMTEPEDet.Fields("MIN_RANGE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 4


            .Col = ColMaxRange
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.9999")
            .TypeFloatMin = CDbl("-99999.9999")
            .TypeEditLen = RsIMTEPEDet.Fields("MAX_RANGE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 4

            .Col = ColPerError
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.99999")
            .TypeFloatMin = CDbl("-99999.99999")
            .TypeEditLen = RsIMTEPEDet.Fields("PER_ERROR").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 5

            MainClass.SetSpreadColor(SprdPE, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub FormatSprdStep(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdStep
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColStepParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIMTEPEStd.Fields("PARAM_DESC").DefinedSize

            .Col = ColStepReadingStep
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.9999")
            .TypeFloatMin = CDbl("-99999.9999")
            .TypeEditLen = RsIMTEPEStd.Fields("READING_STEP").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 4

            .Col = ColStepPerError
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999.99999")
            .TypeFloatMin = CDbl("-99999.99999")
            .TypeEditLen = RsIMTEPEStd.Fields("PER_ERROR").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 5

            MainClass.SetSpreadColor(SprdStep, Arow)
            MainClass.ProtectCell(SprdStep, 1, .MaxRows, ColStepPerError, ColStepPerError)
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
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsIMTEPEHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            MakeEnableDeField((True))
            SprdPE.Enabled = True
            SprdStep.Enabled = True
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

    Private Sub cmdSearchDescription_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDescription.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE='VARIABLE' "
        If MainClass.SearchGridMaster(txtDescription.Text, "QAL_IMTE_MST", "Description", , , , SqlStr) = True Then
            txtDescription.Text = AcName
        End If
        txtDescription.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchLC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchLC.Click

        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE='VARIABLE' "
        If Trim(txtDescription.Text) <> "" Then
            SqlStr = SqlStr & " AND DESCRIPTION='" & MainClass.AllowSingleQuote(txtDescription.Text) & "' "
        End If
        If MainClass.SearchGridMaster(txtLC.Text, "QAL_IMTE_MST", "L_C", , , , SqlStr) = True Then
            txtLC.Text = AcName
        End If
        txtLC.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.SearchGridMaster("", "QAL_IMTE_PE_HDR", "Auto_Key_PE", "Description", "L_C", "", SqlStr) = True Then
            txtNumber.Text = AcName
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
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
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            SprdPE.Enabled = True
            SprdStep.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsIMTEPEHdr.EOF = False Then RsIMTEPEHdr.MoveFirst()
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

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsIMTEPEHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_IMTE_PE_HDR", (txtNumber.Text), RsIMTEPEHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_IMTE_PE_DET WHERE AUTO_KEY_PE=" & Val(lblMkey.Text) & " ")
                PubDBCn.Execute("DELETE FROM QAL_IMTE_PE_STD WHERE AUTO_KEY_PE=" & Val(lblMkey.Text) & " ")
                PubDBCn.Execute("DELETE FROM QAL_IMTE_PE_HDR WHERE AUTO_KEY_PE=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                PubDBCn.CommitTrans()
                RsIMTEPEHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsIMTEPEHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmIMTEPE_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmIMTEPE_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdPE_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPE.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdPE_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPE.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdPE, eventArgs.Row, ColParamDesc)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdPE_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPE.LeaveCell

        On Error GoTo ErrPart
        Dim xParamDesc As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdPE.Row = SprdPE.ActiveRow

        SprdPE.Col = ColParamDesc
        xParamDesc = Trim(SprdPE.Text)
        If xParamDesc = "" Then Exit Sub
        MainClass.AddBlankSprdRow(SprdPE, ColParamDesc, ConRowHeight)
        If eventArgs.NewRow = SprdPE.MaxRows And eventArgs.NewCol = ColParamDesc Then
            SprdPE.Col = ColParamDesc
            SprdPE.Text = xParamDesc
        End If
        FormatSprdPE((SprdPE.MaxRows))

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdPE_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdPE.Leave
        With SprdPE
            SprdPE_LeaveCell(SprdPE, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdStep_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdStep.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdStep_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdStep.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdStep, eventArgs.Row, ColStepParamDesc)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdStep_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdStep.LeaveCell

        On Error GoTo ErrPart
        Dim xStepParamDesc As String
        Dim xStepReadingStep As Double
        Dim xParamDesc As String
        Dim xMinRange As Double
        Dim xMaxRange As Double
        Dim xPerError As Double
        Dim I As Integer

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdStep.Row = SprdStep.ActiveRow

        SprdStep.Col = ColStepParamDesc
        xStepParamDesc = Trim(SprdStep.Text)
        If xStepParamDesc = "" Then Exit Sub
        If eventArgs.col = ColStepReadingStep Then
            SprdStep.Col = ColStepReadingStep
            If Trim(SprdStep.Text) <> "" Then
                xStepReadingStep = Val(SprdStep.Text)

                With SprdPE
                    For I = 1 To .MaxRows
                        .Row = I

                        .Col = ColParamDesc
                        xParamDesc = Trim(.Text)

                        .Col = ColMinRange
                        xMinRange = Val(.Text)

                        .Col = ColMaxRange
                        xMaxRange = Val(.Text)

                        If (xStepParamDesc = xParamDesc And xStepReadingStep >= xMinRange And xStepReadingStep <= xMaxRange) Then
                            .Col = ColPerError
                            xPerError = Val(.Text)
                            SprdStep.Col = ColStepPerError
                            SprdStep.Text = CStr(xPerError)
                            Exit For
                        End If
                    Next
                End With
            End If
        End If
        MainClass.AddBlankSprdRow(SprdStep, ColStepParamDesc, ConRowHeight)
        If eventArgs.NewRow = SprdStep.MaxRows And (eventArgs.NewCol = ColStepParamDesc Or eventArgs.NewCol = ColStepReadingStep) Then
            SprdStep.Col = ColStepParamDesc
            SprdStep.Text = xStepParamDesc
        End If
        FormatSprdStep((SprdStep.MaxRows))

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdStep_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdStep.Leave
        With SprdStep
            SprdStep_LeaveCell(SprdStep, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Public Sub frmIMTEPE_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "IMTE Permissible Errors"

        SqlStr = " Select * From QAL_IMTE_PE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTEPEHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = " Select * From QAL_IMTE_PE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTEPEDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = " Select * From QAL_IMTE_PE_STD Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTEPEStd, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmIMTEPE_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Height = VB6.TwipsToPixelsY(8580)
        'Me.Width = VB6.TwipsToPixelsX(8460)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmIMTEPE_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsIMTEPEHdr.Close()
        RsIMTEPEHdr = Nothing
        RsIMTEPEDet.Close()
        RsIMTEPEDet = Nothing
        RsIMTEPEStd.Close()
        RsIMTEPEStd = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsIMTEPEHdr.EOF Then
            IsShowing = True

            lblMkey.Text = IIf(IsDbNull(RsIMTEPEHdr.Fields("AUTO_KEY_PE").Value), "", RsIMTEPEHdr.Fields("AUTO_KEY_PE").Value)
            txtNumber.Text = IIf(IsDbNull(RsIMTEPEHdr.Fields("AUTO_KEY_PE").Value), "", RsIMTEPEHdr.Fields("AUTO_KEY_PE").Value)
            txtDescription.Text = IIf(IsDbNull(RsIMTEPEHdr.Fields("Description").Value), "", RsIMTEPEHdr.Fields("Description").Value)
            txtLC.Text = IIf(IsDbNull(RsIMTEPEHdr.Fields("L_C").Value), "", RsIMTEPEHdr.Fields("L_C").Value)
            txtCalibProc.Text = IIf(IsDbNull(RsIMTEPEHdr.Fields("CALIB_PROC").Value), "", RsIMTEPEHdr.Fields("CALIB_PROC").Value)
            txtReadingStepDec.Text = IIf(IsDbNull(RsIMTEPEHdr.Fields("READING_STEP_DEC").Value), "", RsIMTEPEHdr.Fields("READING_STEP_DEC").Value)
            txtObservationDec.Text = IIf(IsDbNull(RsIMTEPEHdr.Fields("OBSERVATION_DEC").Value), "", RsIMTEPEHdr.Fields("OBSERVATION_DEC").Value)

            Call ShowPE()
            Call ShowStep()
            Call MakeEnableDeField(False)
            IsShowing = False
        End If
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        ADDMode = False
        MODIFYMode = False
        SprdPE.Enabled = False
        SprdStep.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsIMTEPEHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowPE()

        On Error GoTo ERR1
        Dim I As Integer

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_PE_DET " & vbCrLf & " WHERE AUTO_KEY_PE=" & Val(lblMkey.Text) & " " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTEPEDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsIMTEPEDet
            If .EOF = True Then Exit Sub
            FormatSprdPE(-1)
            I = 1
            Do While Not .EOF
                SprdPE.Row = I

                SprdPE.Col = ColParamDesc
                SprdPE.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdPE.Col = ColMinRange
                SprdPE.Text = Trim(IIf(IsDbNull(.Fields("Min_Range").Value), "", .Fields("Min_Range").Value))

                SprdPE.Col = ColMaxRange
                SprdPE.Text = Trim(IIf(IsDbNull(.Fields("Max_Range").Value), "", .Fields("Max_Range").Value))

                SprdPE.Col = ColPerError
                SprdPE.Text = Trim(IIf(IsDbNull(.Fields("PER_ERROR").Value), "", .Fields("PER_ERROR").Value))

                .MoveNext()
                I = I + 1
                SprdPE.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShowStep()

        On Error GoTo ERR1
        Dim I As Integer

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_PE_STD " & vbCrLf & " WHERE AUTO_KEY_PE=" & Val(lblMkey.Text) & " " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTEPEStd, ADODB.LockTypeEnum.adLockReadOnly)
        With RsIMTEPEStd
            If .EOF = True Then Exit Sub
            FormatSprdStep(-1)
            I = 1
            Do While Not .EOF
                SprdStep.Row = I

                SprdStep.Col = ColStepParamDesc
                SprdStep.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdStep.Col = ColStepReadingStep
                SprdStep.Text = Trim(IIf(IsDbNull(.Fields("READING_STEP").Value), "", .Fields("READING_STEP").Value))

                SprdStep.Col = ColPerError
                SprdStep.Text = Trim(IIf(IsDbNull(.Fields("PER_ERROR").Value), "", .Fields("PER_ERROR").Value))

                .MoveNext()
                I = I + 1
                SprdStep.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        If IsRecordExist = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
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

    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_PE " & vbCrLf _
                    & " From QAL_IMTE_PE_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND DESCRIPTION ='" & MainClass.AllowSingleQuote(txtDescription.Text) & "' " & vbCrLf _
                    & " AND L_C = '" & MainClass.AllowSingleQuote(txtLC.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_PE").Value)
                IsRecordExist = True
            End If
        End With
        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = " SELECT Max(AUTO_KEY_PE)  " & vbCrLf & " FROM QAL_IMTE_PE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    '                mAutoGen = Mid(.Fields(0), 1, Len(.Fields(0)) - 6)
                    mAutoGen = .Fields(0).Value + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = mAutoGen
        '    AutoGenKeyNo = mAutoGen & vb6.Format(RsCompany.fields("FYEAR").value, "0000") & vb6.Format(RsCompany.fields("COMPANY_CODE").value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_IMTE_PE_HDR " & vbCrLf _
                            & " (COMPANY_CODE,AUTO_KEY_PE,DESCRIPTION,L_C,CALIB_PROC, " & vbCrLf _
                            & " READING_STEP_DEC,OBSERVATION_DEC, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & RsCompany.fields("COMPANY_CODE").value & "," & mSlipNo & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDescription.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtLC.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCalibProc.Text) & "', " & vbCrLf _
                            & " " & Val(txtReadingStepDec.Text) & ", " & vbCrLf _
                            & " " & Val(txtObservationDec.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_IMTE_PE_HDR SET " & vbCrLf & " Auto_Key_PE=" & mSlipNo & "," & vbCrLf & " Description='" & MainClass.AllowSingleQuote(txtDescription.Text) & "', " & vbCrLf & " L_C='" & MainClass.AllowSingleQuote(txtLC.Text) & "', " & vbCrLf & " CALIB_PROC='" & MainClass.AllowSingleQuote(txtCalibProc.Text) & "', " & vbCrLf & " READING_STEP_DEC=" & Val(txtReadingStepDec.Text) & ", " & vbCrLf & " OBSERVATION_DEC=" & Val(txtObservationDec.Text) & ", " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND Auto_Key_PE =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdatePE = False Then GoTo ErrPart
        If UpdateStep = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsIMTEPEHdr.Requery()
        RsIMTEPEDet.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function UpdatePE() As Boolean

        On Error GoTo UpdatePEERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mParamDesc As String
        Dim mMinRange As Double
        Dim mMaxRange As Double
        Dim mPerError As Double

        PubDBCn.Execute("DELETE FROM QAL_IMTE_PE_DET WHERE AUTO_KEY_PE=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value)

        With SprdPE
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParamDesc
                mParamDesc = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColMinRange
                mMinRange = Val(.Text)

                .Col = ColMaxRange
                mMaxRange = Val(.Text)

                .Col = ColPerError
                mPerError = Val(.Text)

                SqlStr = ""
                If mParamDesc <> "" Then
                    SqlStr = " INSERT INTO  QAL_IMTE_PE_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_PE,SERIAL_NO,PARAM_DESC, " & vbCrLf & " MIN_RANGE,MAX_RANGE,PER_ERROR ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & ",'" & mParamDesc & "'," & vbCrLf & " " & mMinRange & "," & mMaxRange & "," & mPerError & ")"
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdatePE = True
        Exit Function
UpdatePEERR:
        UpdatePE = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateStep() As Boolean

        On Error GoTo UpdateStepERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mParamDesc As String
        Dim mReadingStep As Double
        Dim mPerError As Double

        PubDBCn.Execute("DELETE FROM QAL_IMTE_PE_STD WHERE AUTO_KEY_PE=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value)

        With SprdStep
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColStepParamDesc
                mParamDesc = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColStepReadingStep
                mReadingStep = Val(.Text)

                .Col = ColStepPerError
                mPerError = Val(.Text)

                SqlStr = ""
                If mParamDesc <> "" Then
                    SqlStr = " INSERT INTO  QAL_IMTE_PE_STD ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_PE,SERIAL_NO,PARAM_DESC, " & vbCrLf & " READING_STEP,PER_ERROR ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & ",'" & mParamDesc & "'," & vbCrLf & " " & mReadingStep & "," & mPerError & ")"
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateStep = True
        Exit Function
UpdateStepERR:
        UpdateStep = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtDescription.Text) = "" Then
            MsgInformation("Description is empty, So unable to Save")
            txtDescription.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtLC.Text) = "" Then
            MsgInformation("L.C. is empty, So unable to Save")
            txtLC.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtReadingStepDec.Text) = "" Then
            MsgInformation("Reading Step Decimals is empty, So unable to Save")
            txtReadingStepDec.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtObservationDec.Text) = "" Then
            MsgInformation("Observation Decimals is empty, So unable to Save")
            txtObservationDec.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdPE, ColParamDesc, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdStep, ColStepParamDesc, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsIMTEPEHdr.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT Auto_Key_PE as Slip_No,Description,L_C,CALIB_PROC " & vbCrLf & " FROM QAL_IMTE_PE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY Auto_Key_PE"
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()

    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "IMTE Permissible Errors"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\IMTEPE.rpt"

        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_PE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND Auto_Key_PE=" & Val(lblMkey.Text) & " "

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtCalibProc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCalibProc.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCalibProc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCalibProc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCalibProc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDescription_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.DoubleClick
        Call cmdSearchDescription_Click(cmdSearchDescription, New System.EventArgs())
    End Sub

    Private Sub txtDescription_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDescription.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDescription.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDescription_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDescription.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDescription_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDescription_Click(cmdSearchDescription, New System.EventArgs())
    End Sub

    Private Sub txtDescription_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDescription.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtDescription.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE='VARIABLE' "
        If MainClass.ValidateWithMasterTable(txtDescription.Text, "Description", "DocNo", "QAL_IMTE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Description", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLC_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLC.DoubleClick
        Call cmdSearchLC_Click(cmdSearchLC, New System.EventArgs())
    End Sub

    Private Sub txtLC_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLC.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLC.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLC.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLC_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLC.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchLC_Click(cmdSearchLC, New System.EventArgs())
    End Sub

    Private Sub txtLC_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLC.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtLC.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE='VARIABLE' "
        If Trim(txtDescription.Text) <> "" Then
            SqlStr = SqlStr & " AND DESCRIPTION='" & MainClass.AllowSingleQuote(txtDescription.Text) & "' "
        End If
        If MainClass.ValidateWithMasterTable(txtLC.Text, "L_C", "DocNo", "QAL_IMTE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid L. C.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Public Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsIMTEPEHdr.BOF = False Then xMKey = RsIMTEPEHdr.Fields("AUTO_KEY_PE").Value

        SqlStr = "SELECT * FROM QAL_IMTE_PE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_PE=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTEPEHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsIMTEPEHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_IMTE_PE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_PE=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTEPEHdr, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtNumber.Maxlength = RsIMTEPEHdr.Fields("Auto_Key_PE").DefinedSize
        txtDescription.Maxlength = RsIMTEPEHdr.Fields("Description").DefinedSize
        txtLC.Maxlength = RsIMTEPEHdr.Fields("L_C").DefinedSize
        txtCalibProc.Maxlength = RsIMTEPEHdr.Fields("CALIB_PROC").DefinedSize
        txtReadingStepDec.Maxlength = RsIMTEPEHdr.Fields("READING_STEP_DEC").DefinedSize
        txtObservationDec.Maxlength = RsIMTEPEHdr.Fields("OBSERVATION_DEC").DefinedSize
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 5)
            .set_ColWidth(3, 500 * 3)
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

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNumber.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtObservationDec_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtObservationDec.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtObservationDec_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtObservationDec.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtObservationDec_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtObservationDec.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtObservationDec.Text) = "" Then GoTo EventExitSub

        If Val(txtObservationDec.Text) < 1 Or Val(txtObservationDec.Text) > 5 Then
            MsgInformation("Observation Decimals should be between 1 to 5")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReadingStepDec_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReadingStepDec.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReadingStepDec_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReadingStepDec.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReadingStepDec_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReadingStepDec.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtReadingStepDec.Text) = "" Then GoTo EventExitSub

        If Val(txtReadingStepDec.Text) < 1 Or Val(txtReadingStepDec.Text) > 4 Then
            MsgInformation("Reading Step Decimals should be between 1 to 4")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
