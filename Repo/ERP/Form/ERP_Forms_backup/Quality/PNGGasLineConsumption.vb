Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPNGGasLineConsumption
    Inherits System.Windows.Forms.Form
    Dim RsHSEB As ADODB.Recordset
    Dim RsHSEBDet As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12
    Private Const ColDeptCode As Short = 1
    Private Const ColDeptName As Short = 2
    Private Const ColConsumptionQty As Short = 3

    Private Sub cboMeterNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMeterNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        GetMeterDetail()
    End Sub

    Private Sub cboMeterNo_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMeterNo.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        GetMeterDetail()
    End Sub


    Private Sub cboMeterNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboMeterNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        GetMeterDetail()
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsHSEB.EOF = False Then RsHSEB.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsHSEB.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_PNG_TRN", (txtNumber.Text), RsHSEB) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_PNG_DEPT_TRN WHERE AUTO_KEY_PNG=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM MAN_PNG_TRN WHERE AUTO_KEY_PNG=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsHSEBDet.Requery()
                RsHSEB.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsHSEBDet.Requery()
        RsHSEB.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsHSEB, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim mMeterCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(cboMeterNo.Text), "METER_NAME", "METER_CODE", "MAN_PNG_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMeterCode = Trim(MasterNo)
        End If

        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO MAN_PNG_TRN " & vbCrLf _
                            & " (AUTO_KEY_PNG, COMPANY_CODE, FYEAR, " & vbCrLf _
                            & " READING_DATE, READING_TIME, TODAY_READING, " & vbCrLf _
                            & " REMARKS, EMP_CODE, " & vbCrLf _
                            & " METER_CODE, DIV_CODE, TOT_CONSUMPTION, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & "," & RsCompany.fields("FYEAR").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & txtTime.Text & "', 'HH24:MI'), " & vbCrLf _
                            & " " & Val(txtCurrReading.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                            & " " & mMeterCode & "," & Val(txtDivCode.Text) & ", " & Val(txtConsumption.Text) & "," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_PNG_TRN SET " & vbCrLf _
                    & " AUTO_KEY_PNG=" & mSlipNo & "," & vbCrLf _
                    & " READING_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " READING_TIME=TO_DATE('" & txtTime.Text & "', 'HH24:MI'), " & vbCrLf _
                    & " TODAY_READING=" & Val(txtCurrReading.Text) & ", TOT_CONSUMPTION=" & Val(txtConsumption.Text) & "," & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                    & " METER_CODE=" & mMeterCode & ", " & vbCrLf _
                    & " DIV_CODE=" & Val(txtDivCode.Text) & ", " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE AUTO_KEY_PNG =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail() = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsHSEBDet.Requery()
        RsHSEB.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_PNG)  " & vbCrLf & " FROM MAN_PNG_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchEmpCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmpCode.Click
        Call SearchEmp(txtEmpCode, lblEmpCode)
    End Sub

    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PNG,LENGTH(AUTO_KEY_PNG)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "MAN_PNG_TRN", "AUTO_KEY_PNG", "READING_DATE", "READING_TIME", "KWH", SqlStr) = True Then
            txtNumber.Text = AcName
            Call txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            Call AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsHSEB, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPNGGasLineConsumption_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "PNG Reading Recording"

        SqlStr = "Select * From MAN_PNG_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSEB, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From MAN_PNG_DEPT_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSEBDet, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT MMST.METER_NAME," & vbCrLf & " TRN.AUTO_KEY_PNG AS REF_NUM, TO_CHAR(TRN.READING_DATE,'DD/MM/YYYY') AS READING_DATE, " & vbCrLf & " TO_CHAR(TRN.READING_TIME,'HH24:MI') AS READING_TIME, TRN.TODAY_READING, TRN.REMARKS, TRN.EMP_CODE " & vbCrLf & " FROM MAN_PNG_TRN TRN, MAN_PNG_METER_MST MMST" & vbCrLf & " WHERE TRN.COMPANY_CODE = MMST.COMPANY_CODE " & vbCrLf & " AND TRN.METER_CODE = MMST.METER_CODE" & vbCrLf & " AND TRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY MMST.METER_NAME, TRN.READING_DATE,TO_CHAR(TRN.READING_TIME,'HH24:MI'), TRN.AUTO_KEY_PNG"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPNGGasLineConsumption_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPNGGasLineConsumption_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(6000)
        Me.Width = VB6.TwipsToPixelsX(9285)

        cboMeterNo.Items.Clear()

        SqlStr = "SELECT METER_NAME FROM MAN_PNG_METER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY METER_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboMeterNo.Items.Add(RS.Fields("METER_NAME").Value)
                RS.MoveNext()
            Loop
        End If

        cboMeterNo.SelectedIndex = -1

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtDate.Text = VB6.Format(GetServerDate, "DD/MM/YYYY")
        txtTime.Text = VB6.Format(GetServerTime, "HH:MM")
        txtCurrReading.Text = ""
        txtConsumption.Text = ""
        txtLastReading.Text = ""

        txtRemarks.Text = ""
        txtEmpCode.Text = ""
        lblEmpCode.Text = ""

        cboMeterNo.SelectedIndex = -1
        cboMeterNo.Enabled = True
        txtDivCode.Text = ""

        txtDivCode.Enabled = False

        FormatSprdMain(-1)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsHSEB, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 2)
            .set_ColWidth(4, 500 * 2)
            .set_ColWidth(5, 500 * 2)
            .set_ColWidth(6, 500 * 2)
            .set_ColWidth(7, 500 * 2)
            '        .ColWidth(8) = 500 * 2
            '        .ColWidth(9) = 500 * 4
            '        .ColWidth(10) = 500 * 4
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsHSEB.Fields("AUTO_KEY_PNG").Precision
        txtDate.Maxlength = RsHSEB.Fields("READING_DATE").DefinedSize - 6
        txtTime.Maxlength = RsHSEB.Fields("READING_TIME").DefinedSize - 11

        txtConsumption.Maxlength = RsHSEB.Fields("TODAY_READING").Precision
        txtCurrReading.Maxlength = RsHSEB.Fields("TODAY_READING").Precision
        txtLastReading.Maxlength = RsHSEB.Fields("TODAY_READING").Precision
        txtRemarks.Maxlength = RsHSEB.Fields("REMARKS").DefinedSize
        txtEmpCode.Maxlength = RsHSEB.Fields("EMP_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mMeterCode As Double
        Dim mDeptConsumptionQty As Double
        Dim CntRow As Integer


        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsHSEB.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Reading Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtTime.Text) = "" Then
            MsgInformation("Reading Time is empty, So unable to save.")
            txtTime.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCurrReading.Text) = "" Then
            MsgInformation("Current Reading is empty, So unable to save.")
            txtCurrReading.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Sign Emp is empty, So unable to save.")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboMeterNo.Text) = "" Then
            MsgBox("Meter No is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboMeterNo.Enabled = True Then cboMeterNo.Focus()
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboMeterNo.Text), "METER_NAME", "METER_CODE", "MAN_PNG_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMeterCode = Trim(MasterNo)
            GetMeterDetail()
        Else
            MsgInformation("Please select Meter No So unable to save.")
            If cboMeterNo.Enabled = True Then cboMeterNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtDivCode.Text) = 0 Then
            MsgInformation("Division Cann't be Blank, So unable to save.")
            FieldsVarification = False
            Exit Function
        End If


        If CheckValidDate() = False Then
            FieldsVarification = False
            Exit Function
        End If

        '    If Val(txtKVAMDI.Text) = 0 Then
        '        MsgInformation "KVA (MDI) Cann't be Blank, So unable to save."
        '        txtKVAMDI.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '    If Val(txtKVA8.Text) = 0 Then
        '        MsgInformation "KVA8 Cann't be Blank, So unable to save."
        '        txtKVA8.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        Call LastReading()

        If Trim(txtCurrReading.Text) <> "" Then
            txtConsumption.Text = CStr(Val(txtCurrReading.Text) - Val(txtLastReading.Text))
        End If

        mDeptConsumptionQty = 0

        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColDeptCode

                If Trim(.Text) <> "" Then
                    .Col = ColConsumptionQty
                    mDeptConsumptionQty = mDeptConsumptionQty + Val(.Text)
                End If
            Next
        End With

        If Val(CStr(mDeptConsumptionQty)) <> Val(txtConsumption.Text) Then
            MsgInformation("Net Consumption is not Match with Dept Wise Consumption.")
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmPNGGasLineConsumption_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsHSEB.Close()
        RsHSEB = Nothing

        RsHSEBDet.Close()
        RsHSEBDet = Nothing

        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptCode

                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColDeptCode
                    .Text = Trim(AcName)

                    .Col = ColDeptName
                    .Text = Trim(AcName1)
                End If

                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDeptCode, .ActiveRow, ColDeptCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptName Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptName

                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColDeptCode
                    .Text = Trim(AcName1)

                    .Col = ColDeptName
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDeptCode, .ActiveRow, ColDeptCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColDeptCode)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptName, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xDeptCode As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColDeptCode
        xDeptCode = Trim(SprdMain.Text)
        If xDeptCode = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColDeptCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColDeptCode
                xDeptCode = Trim(SprdMain.Text)

                If xDeptCode = "" Then Exit Sub
                If CheckDuplicateDept(xDeptCode) = False Then
                    If MainClass.ValidateWithMasterTable(xDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgBox("Department Code Does Not Exist In Master.")
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDeptCode)
                        Exit Sub
                    Else
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColDeptName
                        SprdMain.Text = MasterNo

                        MainClass.AddBlankSprdRow(SprdMain, ColDeptCode, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))

                    End If
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim i As Integer
        Dim mDeptCode As String
        Dim mQty As Double



        PubDBCn.Execute("DELETE FROM MAN_PNG_DEPT_TRN WHERE AUTO_KEY_PNG=" & Val(lblMkey.Text) & "")

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColConsumptionQty
                mQty = Val(.Text)

                SqlStr = ""

                If mQty > 0 And mDeptCode <> "" Then

                    SqlStr = " INSERT INTO  MAN_PNG_DEPT_TRN ( " & vbCrLf & " COMPANY_CODE, AUTO_KEY_PNG, " & vbCrLf & " SERIAL_NO, DEPT_CODE, TOT_DEPT_CONSUMPTION) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & i & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mDeptCode) & "', " & mQty & ") "
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

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim i As Integer
        Dim mDeptCode As String
        Dim mDeptName As String
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM MAN_PNG_DEPT_TRN " & vbCrLf & " WHERE AUTO_KEY_PNG=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSEBDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsHSEBDet
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColDeptCode
                mDeptCode = Trim(IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))
                SprdMain.Text = mDeptCode

                SprdMain.Col = ColDeptName
                MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mDeptName = MasterNo
                SprdMain.Text = mDeptName

                SprdMain.Col = ColConsumptionQty
                SprdMain.Text = CStr(Val(Trim(IIf(IsDbNull(.Fields("TOT_DEPT_CONSUMPTION").Value), "", .Fields("TOT_DEPT_CONSUMPTION").Value))))

                .MoveNext()
                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function CheckDuplicateDept(ByRef mDeptCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If mDeptCode = "" Then CheckDuplicateDept = False : Exit Function
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColDeptCode
                If UCase(Trim(.Text)) = UCase(Trim(mDeptCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateDept = True
                        MsgInformation("Duplicate Dept Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDeptCode)
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
        Dim SqlStr As String

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsHSEBDet.Fields("DEPT_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColDeptName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColConsumptionQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC


            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDeptName, ColDeptName)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Function ValidateEMP(ByRef pTextBox As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label) As Boolean
        On Error GoTo ValEMP
        Dim SqlStr As String
        ValidateEMP = True
        If Trim(pTextBox.Text) = "" Then Exit Function
        pTextBox.Text = VB6.Format(pTextBox.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pLable.text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function

    Private Sub txtConsumption_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtConsumption.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtConsumption_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtConsumption.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDivCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCurrReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCurrReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCurrReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCurrReading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCurrReading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCurrReading.Text) = "" Then GoTo EventExitSub

        If Val(txtCurrReading.Text) <= Val(txtLastReading.Text) Then
            MsgInformation("Current Reading should be greater than Last Reading")
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtCurrReading.Text) <> "" Then
            txtConsumption.Text = CStr(Val(txtCurrReading.Text) - Val(txtLastReading.Text))
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtLastReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLastReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub LastReading()

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mOpenDate As String
        Dim mOpenReading As Double
        Dim mMaxReading As Double
        Dim mMeterCode As Double

        If MainClass.ValidateWithMasterTable(cboMeterNo.Text, "METER_NAME", "METER_CODE", "MAN_PNG_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMeterCode = Val(MasterNo)
        End If


        If Trim(txtDate.Text) = "" Or Trim(txtTime.Text) = "" Then Exit Sub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        SqlStr = " SELECT OPEN_DATE,OPEN_READING FROM MAN_PNG_OPEN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND METER_CODE=" & Val(CStr(mMeterCode)) & "" & vbCrLf & " AND OPEN_DATE= (" & vbCrLf & " SELECT MAX(OPEN_DATE) FROM MAN_PNG_OPEN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND OPEN_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND METER_CODE=" & Val(CStr(mMeterCode)) & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mOpenDate = IIf(IsDbNull(RsTemp.Fields("OPEN_DATE").Value), "", RsTemp.Fields("OPEN_DATE").Value)
            mOpenReading = IIf(IsDbNull(RsTemp.Fields("OPEN_READING").Value), 0, RsTemp.Fields("OPEN_READING").Value)

            ''26-aug-2009 'Dharampal KayJay Auto ..
            '        txtKVAMDI.Text = IIf(isdbnull(RsTemp!KVA_MDI), "", RsTemp!KVA_MDI)
            '        txtKVA8.Text = IIf(isdbnull(RsTemp!KVA8), "", RsTemp!KVA8)
        End If
        SqlStr = " SELECT MAX(TODAY_READING) AS TODAY_READING FROM MAN_PNG_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND METER_CODE=" & Val(CStr(mMeterCode)) & ""
        If Trim(mOpenDate) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND READING_DATE>=TO_DATE('" & VB6.Format(mOpenDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If
        If Trim(txtDate.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND READING_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If
        If Val(lblMkey.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PNG<>" & Val(txtNumber.Text)
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mMaxReading = IIf(IsDbNull(RsTemp.Fields("TODAY_READING").Value), 0, RsTemp.Fields("TODAY_READING").Value)
        End If
        txtLastReading.Text = IIf(mMaxReading > mOpenReading, mMaxReading, mOpenReading)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub GetMeterDetail()

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(cboMeterNo.Text) = "" Then Exit Sub

        SqlStr = " SELECT * FROM MAN_PNG_METER_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND METER_NAME='" & Trim(cboMeterNo.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtDivCode.Text = IIf(IsDbNull(RsTemp.Fields("DIV_CODE").Value), "", RsTemp.Fields("DIV_CODE").Value)
        Else
            txtDivCode.Text = ""
        End If

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            Call LastReading()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mMeterCode As Double
        Dim mMeterDesc As String

        If Not RsHSEB.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsHSEB.Fields("AUTO_KEY_PNG").Value), "", RsHSEB.Fields("AUTO_KEY_PNG").Value)
            txtNumber.Text = IIf(IsDbNull(RsHSEB.Fields("AUTO_KEY_PNG").Value), "", RsHSEB.Fields("AUTO_KEY_PNG").Value)
            txtDate.Text = IIf(IsDbNull(RsHSEB.Fields("READING_DATE").Value), "", RsHSEB.Fields("READING_DATE").Value)
            txtTime.Text = IIf(IsDbNull(RsHSEB.Fields("READING_TIME").Value), "", VB6.Format(RsHSEB.Fields("READING_TIME").Value, "HH:MM"))
            txtCurrReading.Text = IIf(IsDbNull(RsHSEB.Fields("TODAY_READING").Value), "", RsHSEB.Fields("TODAY_READING").Value)
            txtConsumption.Text = IIf(IsDbNull(RsHSEB.Fields("TOT_CONSUMPTION").Value), "", RsHSEB.Fields("TOT_CONSUMPTION").Value)
            txtRemarks.Text = IIf(IsDbNull(RsHSEB.Fields("REMARKS").Value), "", RsHSEB.Fields("REMARKS").Value)
            txtEmpCode.Text = IIf(IsDbNull(RsHSEB.Fields("EMP_CODE").Value), "", RsHSEB.Fields("EMP_CODE").Value)
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))

            mMeterCode = IIf(IsDbNull(RsHSEB.Fields("METER_CODE").Value), -1, RsHSEB.Fields("METER_CODE").Value)

            If MainClass.ValidateWithMasterTable(mMeterCode, "METER_CODE", "METER_NAME", "MAN_PNG_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mMeterDesc = Trim(MasterNo)
                cboMeterNo.Text = mMeterDesc
            End If
            txtDivCode.Text = IIf(IsDbNull(RsHSEB.Fields("DIV_CODE").Value), "", RsHSEB.Fields("DIV_CODE").Value)
            cboMeterNo.Enabled = False

            Call LastReading()

            Call ShowDetail1()
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsHSEB, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub

    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub

        If Len(Trim(txtNumber.Text)) < 6 Then
            txtNumber.Text = Trim(txtNumber.Text) & RsCompany.Fields("FYEAR").Value & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsHSEB.BOF = False Then xMKey = RsHSEB.Fields("AUTO_KEY_PNG").Value

        SqlStr = "SELECT * FROM MAN_PNG_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PNG,LENGTH(AUTO_KEY_PNG)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PNG=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSEB, ADODB.LockTypeEnum.adLockReadOnly)
        If RsHSEB.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_PNG_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PNG,LENGTH(AUTO_KEY_PNG)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PNG=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSEB, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtTime.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtEmpCode.Enabled = mMode
        cmdSearchEmpCode.Enabled = mMode
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ReportOnConsump(ByRef Mode As Crystal.DestinationConstants)

    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnConsump(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnConsump(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        Call cmdSearchEmpCode_Click(cmdSearchEmpCode, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEmpCode_Click(cmdSearchEmpCode, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtEmpCode, lblEmpCode) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTime.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If CheckTimeFormat(txtTime) = False Then Cancel = True : GoTo EventExitSub
        txtTime.Text = VB6.Format(txtTime.Text, "HH:MM")
        Call LastReading()

        GoTo EventExitSub
ERR1:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function CheckTimeFormat(ByRef pTextTime As System.Windows.Forms.TextBox) As Boolean
        On Error GoTo ERR1
        CheckTimeFormat = True
        If InStr(1, pTextTime.Text, ":", CompareMethod.Text) <= 0 Then
            MsgBox("Time should be in format of HH24:MI with numeric value")
            CheckTimeFormat = False
        ElseIf InStr(1, pTextTime.Text, ":", CompareMethod.Text) > 0 Then
            If Not IsNumeric(VB.Left(pTextTime.Text, InStr(1, pTextTime.Text, ":", CompareMethod.Text) - 1)) = True Or Not IsNumeric(Mid(pTextTime.Text, InStr(1, pTextTime.Text, ":", CompareMethod.Text) + 1)) = True Then
                MsgBox("Time should be in format of HH24:MI with numeric value")
                CheckTimeFormat = False
            ElseIf Val(VB.Left(pTextTime.Text, InStr(1, pTextTime.Text, ":", CompareMethod.Text) - 1)) > 23 Then
                MsgBox("HH cann't be greater than 23")
                CheckTimeFormat = False
            ElseIf Val(Mid(pTextTime.Text, InStr(1, pTextTime.Text, ":", CompareMethod.Text) + 1)) > 59 Then
                MsgBox("MM cann't be greater than 59")
                CheckTimeFormat = False
            End If
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Function CheckValidDate() As Object

        On Error GoTo CheckERR
        Dim SqlStr As String
        Dim mMeterCode As Double

        Dim mCurrentDate As String

        Dim RsTemp As ADODB.Recordset

        CheckValidDate = True

        If MainClass.ValidateWithMasterTable(Trim(cboMeterNo.Text), "METER_NAME", "METER_CODE", "MAN_PNG_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMeterCode = Trim(MasterNo)
        End If


        SqlStr = "SELECT MAX(READING_DATE)" & vbCrLf & " FROM MAN_PNG_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND METER_CODE=" & mMeterCode & "" & vbCrLf & " AND DIV_CODE=" & Val(txtDivCode.Text) & "" & vbCrLf & " AND READING_DATE>=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If Val(txtNumber.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PNG<> " & Val(txtNumber.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mCurrentDate = ""
        If RsTemp.EOF = False Then
            mCurrentDate = IIf(IsDbNull(RsTemp.Fields(0).Value), "", RsTemp.Fields(0).Value)
        End If

        If mCurrentDate <> "" Then
            MsgInformation("Back Entry cann't be allow.")
            CheckValidDate = False
        End If

        Exit Function
CheckERR:
        CheckValidDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
