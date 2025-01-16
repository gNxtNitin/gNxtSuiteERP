Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmVoltmeterMst
    Inherits System.Windows.Forms.Form
    Dim RsVoltmeter As ADODB.Recordset
    Dim RsVoltmeterCaibPE As ADODB.Recordset

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
    Private Const ColReadingStep As Short = 2
    Private Const ColPerError As Short = 3

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
        MainClass.ButtonStatus(Me, XRIGHT, RsVoltmeter, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        lblMkey.Text = ""
        txtDocNo.Text = ""
        txtDescription.Text = ""
        txtENo.Text = ""
        txtMakersNo.Text = ""
        txtMake.Text = ""
        txtRange.Text = ""
        txtLC.Text = ""
        txtLocation.Text = ""
        txtShuntRatio.Text = ""
        txtDeptCode.Text = ""
        lblDeptDesc.Text = ""
        txtFrequency.Text = ""
        cboCalibSource.SelectedIndex = 0
        txtLastCalibDate.Text = ""
        txtCalibDueDate.Text = ""
        optStatus(0).Checked = True
        Call MakeEnableDeField(True)
        MainClass.ClearGrid(SprdPE, ConRowHeight)
        FormatSprdPE(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsVoltmeter, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
    End Sub

    Private Sub FormatSprdPE(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdPE
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsVoltmeterCaibPE.Fields("PARAM_DESC").DefinedSize

            .Col = ColReadingStep
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsVoltmeterCaibPE.Fields("READING_STEP").DefinedSize

            .Col = ColPerError
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsVoltmeterCaibPE.Fields("PER_ERROR").DefinedSize

            MainClass.SetSpreadColor(SprdPE, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Function CheckDate(ByRef pTxtDate As System.Windows.Forms.TextBox) As Boolean
        CheckDate = True
        If Trim(pTxtDate.Text) = "" Then Exit Function
        If Not IsDate(pTxtDate.Text) Then
            MsgBox("Not a Valid Date")
            CheckDate = False
        Else
            pTxtDate.Text = VB6.Format(pTxtDate.Text, "DD/MM/YYYY")
        End If
    End Function

    Private Sub cboCalibSource_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCalibSource.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtDocNo.Enabled = False
            cmdSearchDocNo.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsVoltmeter, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdPE.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CalcCalibDueDate()
        If Trim(txtLastCalibDate.Text) = "" Then txtCalibDueDate.Text = "" : Exit Sub
        txtCalibDueDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Val(txtFrequency.Text), CDate(txtLastCalibDate.Text)))
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdSearchDeptCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDeptCode.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "", "", SqlStr) = True Then
            txtDeptCode.Text = AcName1
            lblDeptDesc.text = AcName
        End If
    End Sub

    Private Sub cmdSearchDocNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDocNo.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "QAL_VOLTMETER_MST", "DOCNO", "DESCRIPTION", "E_NO", "RANGE", SqlStr) = True Then
            txtDocNo.Text = AcName
            txtDocNo_Validating(txtDocNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
            txtDocNo.Enabled = False
            cmdSearchDocNo.Enabled = False
            SprdPE.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsVoltmeter.EOF = False Then RsVoltmeter.MoveFirst()
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

        If txtDocNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsVoltmeter.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_VOLTMETER_MST", (txtDocNo.Text), RsVoltmeter, "", "D") = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_VOLTMETER_CALIB_PE WHERE DOCNO=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                PubDBCn.Execute("DELETE FROM QAL_VOLTMETER_MST WHERE DOCNO=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")
                PubDBCn.CommitTrans()
                RsVoltmeter.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsVoltmeter.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmVoltmeterMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmVoltmeterMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub optStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtDocNo.Text = SprdView.Text
        txtDocNo_Validating(txtDocNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Public Sub frmVoltmeterMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Process Instruments Calibration Master"

        SqlStr = " Select * From QAL_VOLTMETER_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeter, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = " Select * From QAL_VOLTMETER_CALIB_PE Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeterCaibPE, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmVoltmeterMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7635)
        Me.Width = VB6.TwipsToPixelsX(8820)
        Call FillCboCalibSource()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub FillCboCalibSource()
        cboCalibSource.Items.Clear()
        cboCalibSource.Items.Add("INSIDE")
        cboCalibSource.Items.Add("OUTSIDE")
        cboCalibSource.SelectedIndex = 0
    End Sub

    Private Sub frmVoltmeterMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsVoltmeter.Close()
        RsVoltmeter = Nothing
        RsVoltmeterCaibPE.Close()
        RsVoltmeterCaibPE = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        If Not RsVoltmeter.EOF Then
            IsShowing = True

            lblMkey.Text = IIf(IsDbNull(RsVoltmeter.Fields("DOCNO").Value), "", RsVoltmeter.Fields("DOCNO").Value)
            txtDocNo.Text = IIf(IsDbNull(RsVoltmeter.Fields("DOCNO").Value), "", RsVoltmeter.Fields("DOCNO").Value)
            txtDescription.Text = IIf(IsDbNull(RsVoltmeter.Fields("Description").Value), "", RsVoltmeter.Fields("Description").Value)
            txtENo.Text = IIf(IsDbNull(RsVoltmeter.Fields("E_NO").Value), "", RsVoltmeter.Fields("E_NO").Value)
            txtMakersNo.Text = IIf(IsDbNull(RsVoltmeter.Fields("MAKERS_NO").Value), "", RsVoltmeter.Fields("MAKERS_NO").Value)
            txtMake.Text = IIf(IsDbNull(RsVoltmeter.Fields("MAKE").Value), "", RsVoltmeter.Fields("MAKE").Value)
            txtRange.Text = IIf(IsDbNull(RsVoltmeter.Fields("Range").Value), "", RsVoltmeter.Fields("Range").Value)
            txtLC.Text = IIf(IsDbNull(RsVoltmeter.Fields("L_C").Value), "", RsVoltmeter.Fields("L_C").Value)
            txtLocation.Text = IIf(IsDbNull(RsVoltmeter.Fields("Location").Value), "", RsVoltmeter.Fields("Location").Value)
            txtShuntRatio.Text = IIf(IsDbNull(RsVoltmeter.Fields("SHUNT_RATIO").Value), "", RsVoltmeter.Fields("SHUNT_RATIO").Value)
            txtDeptCode.Text = IIf(IsDbNull(RsVoltmeter.Fields("DEPT_CODE").Value), "", RsVoltmeter.Fields("DEPT_CODE").Value)
            txtDeptCode_Validating(txtDeptCode, New System.ComponentModel.CancelEventArgs(False))
            txtFrequency.Text = IIf(IsDbNull(RsVoltmeter.Fields("FREQUENCY").Value), "", RsVoltmeter.Fields("FREQUENCY").Value)
            If RsVoltmeter.Fields("CALI_SOURCE").Value = "I" Then
                cboCalibSource.Text = "INSIDE"
            Else
                cboCalibSource.Text = "OUTSIDE"
            End If
            txtLastCalibDate.Text = IIf(IsDbNull(RsVoltmeter.Fields("LAST_CALI_DATE").Value), "", RsVoltmeter.Fields("LAST_CALI_DATE").Value)
            txtCalibDueDate.Text = IIf(IsDbNull(RsVoltmeter.Fields("CALI_DUE_DATE").Value), "", RsVoltmeter.Fields("CALI_DUE_DATE").Value)
            If RsVoltmeter.Fields("Status").Value = "A" Then
                optStatus(0).Checked = True
            Else
                optStatus(1).Checked = True
            End If

            Call ShowStd()
            Call MakeEnableDeField(False)
            IsShowing = False
        End If
        txtDocNo.Enabled = True
        cmdSearchDocNo.Enabled = True
        ADDMode = False
        MODIFYMode = False
        SprdPE.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsVoltmeter, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowStd()

        On Error GoTo ERR1
        Dim I As Integer

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_VOLTMETER_CALIB_PE " & vbCrLf & " WHERE DOCNO=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeterCaibPE, ADODB.LockTypeEnum.adLockReadOnly)
        With RsVoltmeterCaibPE
            If .EOF = True Then Exit Sub
            FormatSprdPE(-1)
            I = 1
            Do While Not .EOF
                SprdPE.Row = I

                SprdPE.Col = ColParamDesc
                SprdPE.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdPE.Col = ColReadingStep
                SprdPE.Text = Trim(IIf(IsDbNull(.Fields("READING_STEP").Value), "", .Fields("READING_STEP").Value))

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

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtDocNo_Validating(txtDocNo, New System.ComponentModel.CancelEventArgs(False))
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

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = " SELECT Max(DOCNO)  " & vbCrLf & " FROM QAL_VOLTMETER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mAutoGen = .Fields(0).Value
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = mAutoGen
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mDocNo As Double
        Dim mCalibSource As String
        Dim mStatus As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mDocNo = Val(txtDocNo.Text)
        If Val(txtDocNo.Text) = 0 Then
            mDocNo = AutoGenKeyNo()
        End If
        txtDocNo.Text = CStr(mDocNo)
        mCalibSource = IIf(cboCalibSource.Text = "OUTSIDE", "O", "I")
        mStatus = IIf(optStatus(0).Checked = True, "A", "I")

        If ADDMode = True Then
            lblMkey.Text = CStr(mDocNo)
            SqlStr = " INSERT INTO QAL_VOLTMETER_MST " & vbCrLf _
                            & " (COMPANY_CODE, DOCNO, Description, E_NO, " & vbCrLf _
                            & " MAKERS_NO, MAKE, RANGE, L_C, LOCATION, SHUNT_RATIO, " & vbCrLf _
                            & " DEPT_CODE, FREQUENCY, CALI_SOURCE, LAST_CALI_DATE, " & vbCrLf _
                            & " CALI_DUE_DATE, Status, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & mDocNo & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDescription.Text) & "','" & MainClass.AllowSingleQuote(txtENo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMakersNo.Text) & "','" & MainClass.AllowSingleQuote(txtMake.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRange.Text) & "','" & MainClass.AllowSingleQuote(txtLC.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtLocation.Text) & "','" & MainClass.AllowSingleQuote(txtShuntRatio.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'," & Val(txtFrequency.Text) & ", " & vbCrLf _
                            & " '" & mCalibSource & "',TO_DATE('" & vb6.Format(txtLastCalibDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtCalibDueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mStatus & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_VOLTMETER_MST SET " & vbCrLf & " DOCNO=" & mDocNo & "," & vbCrLf & " Description='" & MainClass.AllowSingleQuote(txtDescription.Text) & "', " & vbCrLf & " E_NO='" & MainClass.AllowSingleQuote(txtENo.Text) & "', " & vbCrLf & " MAKERS_NO='" & MainClass.AllowSingleQuote(txtMakersNo.Text) & "', " & vbCrLf & " MAKE='" & MainClass.AllowSingleQuote(txtMake.Text) & "'," & vbCrLf & " RANGE='" & MainClass.AllowSingleQuote(txtRange.Text) & "'," & vbCrLf & " L_C='" & MainClass.AllowSingleQuote(txtLC.Text) & "'," & vbCrLf & " LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "'," & vbCrLf & " SHUNT_RATIO='" & MainClass.AllowSingleQuote(txtShuntRatio.Text) & "'," & vbCrLf & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'," & vbCrLf & " FREQUENCY=" & Val(txtFrequency.Text) & "," & vbCrLf & " CALI_SOURCE='" & mCalibSource & "'," & vbCrLf & " LAST_CALI_DATE=TO_DATE('" & VB6.Format(txtLastCalibDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " CALI_DUE_DATE=TO_DATE('" & VB6.Format(txtCalibDueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " Status='" & mStatus & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateStd = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtDocNo.Text = CStr(mDocNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsVoltmeter.Requery()
        RsVoltmeterCaibPE.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function UpdateStd() As Boolean

        On Error GoTo UpdateStdERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mParamDesc As String
        Dim mReadingStep As String
        Dim mPerError As String

        PubDBCn.Execute("DELETE FROM QAL_VOLTMETER_CALIB_PE WHERE DOCNO=" & Val(lblMkey.Text) & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ")

        With SprdPE
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParamDesc
                mParamDesc = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColReadingStep
                mReadingStep = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColPerError
                mPerError = MainClass.AllowSingleQuote(Trim(.Text))

                SqlStr = ""
                If mParamDesc <> "" Then
                    SqlStr = " INSERT INTO  QAL_VOLTMETER_CALIB_PE ( " & vbCrLf & " COMPANY_CODE, DOCNO, SERIAL_NO, PARAM_DESC, READING_STEP, PER_ERROR ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & "," & vbCrLf & " '" & mParamDesc & "','" & mReadingStep & "','" & mPerError & "')"
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateStd = True
        Exit Function
UpdateStdERR:
        UpdateStd = False
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

        If Trim(cboCalibSource.Text) = "" Then
            MsgInformation("Calib Source is empty, So unable to Save")
            cboCalibSource.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtFrequency.Text) = "" Then
            MsgInformation("Frequency is empty, So unable to Save")
            txtFrequency.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtLastCalibDate.Text) = "" Then
            MsgInformation("Last Calib Date is empty, So unable to Save")
            txtLastCalibDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdPE, ColParamDesc, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsVoltmeter.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT DOCNO, Description, E_NO, MAKERS_NO, " & vbCrLf & " MAKE, RANGE, L_C, DEPT_CODE, " & vbCrLf & " FREQUENCY, DECODE(CALI_SOURCE,'I','INSIDE','OUTSIDE') AS CALI_SOURCE, " & vbCrLf & " TO_CHAR(LAST_CALI_DATE,'DD/MM/YYYY') AS LAST_CALI_DATE, TO_CHAR(CALI_DUE_DATE,'DD/MM/YYYY') AS CALI_DUE_DATE  " & vbCrLf & " FROM QAL_VOLTMETER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY DOCNO"
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
    End Sub

    Private Sub txtDeptCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeptCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.DoubleClick
        Call CmdSearchDeptCode_Click(CmdSearchDeptCode, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeptCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeptCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDeptCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeptCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchDeptCode_Click(CmdSearchDeptCode, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeptCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDeptCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDeptCode.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Deparment Does Not Exist In Master.")
            Cancel = True
        Else
            lblDeptDesc.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub txtENO_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtENo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtENo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtENo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtENo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFrequency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrequency.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFrequency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFrequency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFrequency_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrequency.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcCalibDueDate()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLastCalibDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLastCalibDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLastCalibDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLastCalibDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtLastCalibDate) = False Then Cancel = True : GoTo EventExitSub
        Call CalcCalibDueDate()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDocNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDocNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDocNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDocNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.DoubleClick
        Call cmdSearchDocNo_Click(cmdSearchDocNo, New System.EventArgs())
    End Sub

    Private Sub txtDocNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDocNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDocNo_Click(cmdSearchDocNo, New System.EventArgs())
    End Sub

    Public Sub txtDocNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDocNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        If Trim(txtDocNo.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsVoltmeter.EOF = False Then xMKey = RsVoltmeter.Fields("DOCNO").Value
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_VOLTMETER_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeter, ADODB.LockTypeEnum.adLockReadOnly)
        If RsVoltmeter.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xMKey = RsVoltmeter.Fields("DOCNO").Value
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Number Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM QAL_VOLTMETER_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO=" & xMKey & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeter, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtDocNo.Maxlength = RsVoltmeter.Fields("DOCNO").Precision
        txtDescription.Maxlength = RsVoltmeter.Fields("Description").DefinedSize
        txtENo.Maxlength = RsVoltmeter.Fields("E_NO").DefinedSize
        txtMakersNo.Maxlength = RsVoltmeter.Fields("MAKERS_NO").DefinedSize
        txtMake.Maxlength = RsVoltmeter.Fields("MAKE").DefinedSize
        txtRange.Maxlength = RsVoltmeter.Fields("RANGE").DefinedSize
        txtLC.Maxlength = RsVoltmeter.Fields("L_C").DefinedSize
        txtLocation.Maxlength = RsVoltmeter.Fields("LOCATION").DefinedSize
        txtShuntRatio.Maxlength = RsVoltmeter.Fields("SHUNT_RATIO").DefinedSize
        txtDeptCode.Maxlength = RsVoltmeter.Fields("DEPT_CODE").DefinedSize
        txtFrequency.Maxlength = RsVoltmeter.Fields("FREQUENCY").Precision
        txtLastCalibDate.Maxlength = RsVoltmeter.Fields("LAST_CALI_DATE").DefinedSize - 6
        txtCalibDueDate.Maxlength = RsVoltmeter.Fields("CALI_DUE_DATE").DefinedSize - 6
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

    Private Sub txtLC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLC.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLC_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLC.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLC.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMake_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMake.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMake.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMakersNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMakersNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMakersNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMakersNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMakersNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRange_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRange.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRange.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtShuntRatio_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShuntRatio.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShuntRatio_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShuntRatio.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShuntRatio.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
