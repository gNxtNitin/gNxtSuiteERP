Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmFuelConsumption
    Inherits System.Windows.Forms.Form
    Dim RsFuelCons As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

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
            If RsFuelCons.EOF = False Then RsFuelCons.MoveFirst()
            Clear1()
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
        If Not RsFuelCons.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_FUELCONSUMP_TRN", (txtNumber.Text), RsFuelCons) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_FUELCONSUMP_TRN WHERE AUTO_KEY_FUEL=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsFuelCons.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsFuelCons.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsFuelCons, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            SqlStr = " INSERT INTO MAN_FUELCONSUMP_TRN " & vbCrLf _
                            & " (AUTO_KEY_FUEL, COMPANY_CODE, FYEAR, " & vbCrLf _
                            & " DOC_DATE, MACHINE_NO, FUEL_TYPE, " & vbCrLf _
                            & " FUEL_CONS_ON, FUEL_CONS, HOUR_METER_READING, " & vbCrLf _
                            & " UNIT_METER_READING, NET_HOURS, NET_UNITS, " & vbCrLf _
                            & " TOT_FUEL_CONSUMED, FUEL_RATE, TOT_AMOUNT, " & vbCrLf _
                            & " REMARKS, EMP_CODE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                            & " '" & VB.Left(txtFuelType.Text, 1) & "','" & VB.Left(txtFuelConsOn.Text, 1) & "', " & vbCrLf _
                            & " " & Val(txtFuelCons.Text) & "," & Val(txtHourReading.Text) & ", " & vbCrLf _
                            & " " & Val(txtUnitReading.Text) & "," & Val(txtNetHours.Text) & ", " & vbCrLf _
                            & " " & Val(txtNetUnits.Text) & "," & Val(txtTotFuelConsumed.Text) & ", " & vbCrLf _
                            & " " & Val(txtFuelRate.Text) & "," & Val(txtTotAmount.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_FUELCONSUMP_TRN SET " & vbCrLf _
                    & " AUTO_KEY_FUEL=" & mSlipNo & "," & vbCrLf _
                    & " DOC_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'," & vbCrLf _
                    & " FUEL_TYPE='" & VB.Left(txtFuelType.Text, 1) & "'," & vbCrLf _
                    & " FUEL_CONS_ON='" & VB.Left(txtFuelConsOn.Text, 1) & "', " & vbCrLf _
                    & " FUEL_CONS=" & Val(txtFuelCons.Text) & ", " & vbCrLf _
                    & " HOUR_METER_READING=" & Val(txtHourReading.Text) & "," & vbCrLf _
                    & " UNIT_METER_READING=" & Val(txtUnitReading.Text) & ", " & vbCrLf _
                    & " NET_HOURS=" & Val(txtNetHours.Text) & ", " & vbCrLf _
                    & " NET_UNITS=" & Val(txtNetUnits.Text) & ", " & vbCrLf _
                    & " TOT_FUEL_CONSUMED=" & Val(txtTotFuelConsumed.Text) & "," & vbCrLf _
                    & " FUEL_RATE=" & Val(txtFuelRate.Text) & ", " & vbCrLf _
                    & " TOT_AMOUNT=" & Val(txtTotAmount.Text) & ", " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE AUTO_KEY_FUEL =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsFuelCons.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_FUEL)  " & vbCrLf & " FROM MAN_FUELCONSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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

    Private Sub cmdSearchMachine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachine.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' AND STATUS='O' "
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtMachineNo.Text = AcName1
            lblMachine.text = AcName
            If txtMachineNo.Enabled = True Then txtMachineNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "MAN_FUELCONSUMP_TRN", "AUTO_KEY_FUEL", "DOC_DATE", "MACHINE_NO", "", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsFuelCons, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmFuelConsumption_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Machines' Fuel Consumption"

        SqlStr = "Select * From MAN_FUELCONSUMP_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFuelCons, ADODB.LockTypeEnum.adLockReadOnly)

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
        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_FUEL AS REF_NUM, TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOC_DATE, " & vbCrLf & " MACHINE_NO, DECODE(FUEL_TYPE,'E','Electricity','D','Diesel') AS FUEL_TYPE, " & vbCrLf & " DECODE(FUEL_CONS_ON,'H','Hour Basis','U','Unit Basis') AS FUEL_CONS_ON, FUEL_CONS, " & vbCrLf & " HOUR_METER_READING, UNIT_METER_READING, NET_HOURS, NET_UNITS, " & vbCrLf & " TOT_FUEL_CONSUMED, FUEL_RATE, TOT_AMOUNT, REMARKS, EMP_CODE " & vbCrLf & " FROM MAN_FUELCONSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_FUEL"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmFuelConsumption_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmFuelConsumption_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(5490)
        Me.Width = VB6.TwipsToPixelsX(9765)
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
        txtMachineNo.Text = ""
        lblMachine.Text = ""
        txtFuelType.Text = ""
        txtFuelConsOn.Text = ""
        txtFuelCons.Text = ""
        txtLastHourReading.Text = ""
        txtHourReading.Text = ""
        txtLastUnitReading.Text = ""
        txtUnitReading.Text = ""
        txtNetHours.Text = ""
        txtNetUnits.Text = ""
        txtTotFuelConsumed.Text = ""
        txtFuelRate.Text = ""
        txtTotAmount.Text = ""
        txtRemarks.Text = ""
        txtEmpCode.Text = ""
        lblEmpCode.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsFuelCons, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .set_ColWidth(8, 500 * 2)
            .set_ColWidth(9, 500 * 2)
            .set_ColWidth(10, 500 * 2)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsFuelCons.Fields("AUTO_KEY_FUEL").Precision
        txtDate.Maxlength = RsFuelCons.Fields("DOC_DATE").DefinedSize - 6
        txtMachineNo.Maxlength = RsFuelCons.Fields("MACHINE_NO").DefinedSize
        txtFuelType.Maxlength = 255
        txtFuelConsOn.Maxlength = 255
        txtFuelCons.Maxlength = RsFuelCons.Fields("FUEL_CONS").Precision
        txtLastHourReading.Maxlength = 255
        txtHourReading.Maxlength = RsFuelCons.Fields("HOUR_METER_READING").Precision
        txtLastUnitReading.Maxlength = 255
        txtUnitReading.Maxlength = RsFuelCons.Fields("UNIT_METER_READING").Precision
        txtNetHours.Maxlength = RsFuelCons.Fields("NET_HOURS").Precision
        txtNetUnits.Maxlength = RsFuelCons.Fields("NET_UNITS").Precision
        txtTotFuelConsumed.Maxlength = RsFuelCons.Fields("TOT_FUEL_CONSUMED").Precision
        txtFuelRate.Maxlength = RsFuelCons.Fields("FUEL_RATE").Precision
        txtTotAmount.Maxlength = RsFuelCons.Fields("TOT_AMOUNT").Precision
        txtRemarks.Maxlength = RsFuelCons.Fields("REMARKS").DefinedSize
        txtEmpCode.Maxlength = RsFuelCons.Fields("EMP_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsFuelCons.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMachineNo.Text) = "" Then
            MsgInformation("Machine No is empty, So unable to save.")
            txtMachineNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If VB.Left(txtFuelConsOn.Text, 1) = "H" Then
            If Trim(txtHourReading.Text) = "" Then
                MsgInformation("Current Hour Meter Reading is empty, So unable to save.")
                txtHourReading.Focus()
                FieldsVarification = False
                Exit Function
            End If
        ElseIf VB.Left(txtFuelConsOn.Text, 1) = "U" Then
            If Trim(txtUnitReading.Text) = "" Then
                MsgInformation("Current Unit Meter Reading is empty, So unable to save.")
                txtUnitReading.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Sign Emp is empty, So unable to save.")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmFuelConsumption_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsFuelCons.Close()
        RsFuelCons = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
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

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

        If MODIFYMode = True And RsFuelCons.BOF = False Then xMKey = RsFuelCons.Fields("AUTO_KEY_FUEL").Value

        SqlStr = "SELECT * FROM MAN_FUELCONSUMP_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_FUEL=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFuelCons, ADODB.LockTypeEnum.adLockReadOnly)
        If RsFuelCons.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_FUELCONSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_FUEL=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFuelCons, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            Call FuelRate()
            Call LastReading()
            Call CalcTot()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
        Dim RsMachineMst As ADODB.Recordset

        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                    & " FROM MAN_MACHINE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf _
                    & " AND MACHINE_UB='N' AND STATUS='O' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineMst, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsMachineMst.EOF Then
            lblMachine.Text = IIf(IsDbNull(RsMachineMst.Fields("MACHINE_DESC").Value), "", RsMachineMst.Fields("MACHINE_DESC").Value)
            If Not IsDbNull(RsMachineMst.Fields("FUEL_TYPE").Value) Then
                txtFuelType.Text = IIf(RsMachineMst.Fields("FUEL_TYPE").Value = "E", "Electricity", "Diesel")
            End If
            If Not IsDbNull(RsMachineMst.Fields("FUEL_CONS_ON").Value) Then
                txtFuelConsOn.Text = IIf(RsMachineMst.Fields("FUEL_CONS_ON").Value = "H", "Hour Basis", "Unit Basis")
            End If
            txtFuelCons.Text = IIf(IsDbNull(RsMachineMst.Fields("FUEL_CONS").Value), 0, VB6.Format(RsMachineMst.Fields("FUEL_CONS").Value, "0.00"))

            '        If Left(txtFuelConsOn.Text, 1) = "H" Then
            '            txtHourReading.Enabled = True
            '            txtUnitReading.Enabled = False
            '            txtUnitReading.Text = ""
            '            txtNetUnits.Text = ""
            '        ElseIf Left(txtFuelConsOn.Text, 1) = "U" Then
            '            txtUnitReading.Enabled = True
            '            txtHourReading.Enabled = False
            '            txtHourReading.Text = ""
            '            txtNetHours.Text = ""
            '        End If

            Call FuelRate()
            Call LastReading()
            Call CalcTot()
        Else
            MsgBox("Machine Does Not Exist In Master.")
            Cancel = True
        End If
        GoTo EventExitSub
ValMacERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtFuelType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFuelType.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFuelType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFuelType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFuelType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFuelConsOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFuelConsOn.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFuelConsOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFuelConsOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFuelConsOn.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFuelCons_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFuelCons.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFuelCons_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFuelCons.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLastHourReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLastHourReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLastHourReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLastHourReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHourReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHourReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHourReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHourReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHourReading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHourReading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtHourReading.Text) = "" Then GoTo EventExitSub

        If Val(txtHourReading.Text) <= Val(txtLastHourReading.Text) Then
            MsgInformation("Current Hour Meter Reading should be greater than Last Hour Meter Reading")
            Cancel = True
        Else
            txtHourReading.Text = VB6.Format(txtHourReading.Text, "0.00")
            Call CalcTot()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLastUnitReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLastUnitReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLastUnitReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLastUnitReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUnitReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnitReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUnitReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnitReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUnitReading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUnitReading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtUnitReading.Text) = "" Then GoTo EventExitSub

        If Val(txtUnitReading.Text) <= Val(txtLastUnitReading.Text) Then
            MsgInformation("Current Unit Meter Reading should be greater than Last Unit Meter Reading")
            Cancel = True
        Else
            txtUnitReading.Text = VB6.Format(txtUnitReading.Text, "0.00")
            Call CalcTot()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNetHours_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetHours.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetHours_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetHours.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNetUnits_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetUnits.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetUnits_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetUnits.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotFuelConsumed_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotFuelConsumed.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotFuelConsumed_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotFuelConsumed.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFuelRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFuelRate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFuelRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFuelRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFuelRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFuelRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtFuelRate.Text) = "" Then GoTo EventExitSub
        txtFuelRate.Text = VB6.Format(txtFuelRate.Text, "0.00")
        Call CalcTot()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotAmount.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

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

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    Private Sub FuelRate()

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " SELECT ELECTRICITY_RATE,DIESEL_RATE FROM MAN_FUELRATE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOC_DATE= (" & vbCrLf & " SELECT MAX(DOC_DATE) FROM MAN_FUELRATE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If Trim(txtDate.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND DOC_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If
        SqlStr = SqlStr & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If VB.Left(txtFuelType.Text, 1) = "E" Then
                txtFuelRate.Text = IIf(IsDbNull(RsTemp.Fields("ELECTRICITY_RATE").Value), "0.00", VB6.Format(RsTemp.Fields("ELECTRICITY_RATE").Value, "0.00"))
            ElseIf VB.Left(txtFuelType.Text, 1) = "D" Then
                txtFuelRate.Text = IIf(IsDbNull(RsTemp.Fields("DIESEL_RATE").Value), "0.00", VB6.Format(RsTemp.Fields("DIESEL_RATE").Value, "0.00"))
            End If
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub LastReading()

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mOpenDate As String
        Dim mOpenHourReading As Double
        Dim mOpenUnitReading As Double
        Dim mMaxHourReading As Double
        Dim mMaxUnitReading As Double

        If Trim(txtMachineNo.Text) = "" Then Exit Sub

        SqlStr = " SELECT OPEN_DATE,OPEN_READING,OPEN_UNIT_READING FROM MAN_GENREC_OPEN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf _
                    & " AND OPEN_DATE= (" & vbCrLf _
                    & " SELECT MAX(OPEN_DATE) FROM MAN_GENREC_OPEN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
        If Trim(txtDate.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND OPEN_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If
        SqlStr = SqlStr & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mOpenDate = IIf(IsDbNull(RsTemp.Fields("OPEN_DATE").Value), "", RsTemp.Fields("OPEN_DATE").Value)
            mOpenHourReading = IIf(IsDbNull(RsTemp.Fields("OPEN_READING").Value), "", RsTemp.Fields("OPEN_READING").Value)
            mOpenUnitReading = IIf(IsDbNull(RsTemp.Fields("OPEN_UNIT_READING").Value), "", RsTemp.Fields("OPEN_UNIT_READING").Value)
        End If

        SqlStr = " SELECT MAX(HOUR_METER_READING) AS HOUR_METER_READING, " & vbCrLf & " MAX(UNIT_METER_READING) AS UNIT_METER_READING " & vbCrLf & " FROM MAN_FUELCONSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
        If Trim(mOpenDate) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND DOC_DATE>=TO_DATE('" & VB6.Format(mOpenDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If
        If Trim(txtDate.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND DOC_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If
        If Val(lblMkey.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_FUEL<>" & Val(txtNumber.Text)
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mMaxHourReading = IIf(IsDbNull(RsTemp.Fields("HOUR_METER_READING").Value), 0, RsTemp.Fields("HOUR_METER_READING").Value)
            mMaxUnitReading = IIf(IsDbNull(RsTemp.Fields("UNIT_METER_READING").Value), 0, RsTemp.Fields("UNIT_METER_READING").Value)
        End If
        txtLastHourReading.Text = IIf(mMaxHourReading > mOpenHourReading, mMaxHourReading, mOpenHourReading)
        txtLastHourReading.Text = VB6.Format(txtLastHourReading.Text, "0.00")
        txtLastUnitReading.Text = IIf(mMaxUnitReading > mOpenUnitReading, mMaxUnitReading, mOpenUnitReading)
        txtLastUnitReading.Text = VB6.Format(txtLastUnitReading.Text, "0.00")
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub CalcTot()
        On Error GoTo ERR1

        If Trim(txtFuelConsOn.Text) = "" Then Exit Sub

        If Trim(txtHourReading.Text) <> "" Then
            txtNetHours.Text = VB6.Format(Val(txtHourReading.Text) - Val(txtLastHourReading.Text), "0.00")
        End If
        If Trim(txtUnitReading.Text) <> "" Then
            txtNetUnits.Text = VB6.Format(Val(txtUnitReading.Text) - Val(txtLastUnitReading.Text), "0.00")
        End If
        If VB.Left(txtFuelConsOn.Text, 1) = "H" Then
            txtTotFuelConsumed.Text = VB6.Format(Val(txtNetHours.Text) * Val(txtFuelCons.Text), "0.00")
        ElseIf VB.Left(txtFuelConsOn.Text, 1) = "U" Then
            txtTotFuelConsumed.Text = VB6.Format(Val(txtNetUnits.Text) * Val(txtFuelCons.Text), "0.00")
        End If
        txtTotAmount.Text = VB6.Format(Val(txtTotFuelConsumed.Text) * Val(txtFuelRate.Text), "0.00")
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String

        If Not RsFuelCons.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsFuelCons.Fields("AUTO_KEY_FUEL").Value), "", RsFuelCons.Fields("AUTO_KEY_FUEL").Value)
            txtNumber.Text = IIf(IsDbNull(RsFuelCons.Fields("AUTO_KEY_FUEL").Value), "", RsFuelCons.Fields("AUTO_KEY_FUEL").Value)
            txtDate.Text = IIf(IsDbNull(RsFuelCons.Fields("DOC_DATE").Value), "", RsFuelCons.Fields("DOC_DATE").Value)
            txtMachineNo.Text = IIf(IsDbNull(RsFuelCons.Fields("MACHINE_NO").Value), "", RsFuelCons.Fields("MACHINE_NO").Value)
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' AND STATUS='O' "
            If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                lblMachine.text = MasterNo
            End If
            If Not IsDbNull(RsFuelCons.Fields("FUEL_TYPE").Value) Then
                txtFuelType.Text = IIf(RsFuelCons.Fields("FUEL_TYPE").Value = "E", "Electricity", "Diesel")
            End If
            If Not IsDbNull(RsFuelCons.Fields("FUEL_CONS_ON").Value) Then
                txtFuelConsOn.Text = IIf(RsFuelCons.Fields("FUEL_CONS_ON").Value = "H", "Hour Basis", "Unit Basis")
            End If
            txtFuelCons.Text = IIf(IsDbNull(RsFuelCons.Fields("FUEL_CONS").Value), "", VB6.Format(RsFuelCons.Fields("FUEL_CONS").Value, "0.00"))
            txtHourReading.Text = IIf(IsDbNull(RsFuelCons.Fields("HOUR_METER_READING").Value), "", VB6.Format(RsFuelCons.Fields("HOUR_METER_READING").Value, "0.00"))
            txtUnitReading.Text = IIf(IsDbNull(RsFuelCons.Fields("UNIT_METER_READING").Value), "", VB6.Format(RsFuelCons.Fields("UNIT_METER_READING").Value, "0.00"))
            txtNetHours.Text = IIf(IsDbNull(RsFuelCons.Fields("NET_HOURS").Value), "", VB6.Format(RsFuelCons.Fields("NET_HOURS").Value, "0.00"))
            txtNetUnits.Text = IIf(IsDbNull(RsFuelCons.Fields("NET_UNITS").Value), "", VB6.Format(RsFuelCons.Fields("NET_UNITS").Value, "0.00"))
            txtTotFuelConsumed.Text = IIf(IsDbNull(RsFuelCons.Fields("TOT_FUEL_CONSUMED").Value), "", VB6.Format(RsFuelCons.Fields("TOT_FUEL_CONSUMED").Value, "0.00"))
            txtFuelRate.Text = IIf(IsDbNull(RsFuelCons.Fields("FUEL_RATE").Value), "", VB6.Format(RsFuelCons.Fields("FUEL_RATE").Value, "0.00"))
            txtTotAmount.Text = IIf(IsDbNull(RsFuelCons.Fields("TOT_AMOUNT").Value), "", VB6.Format(RsFuelCons.Fields("TOT_AMOUNT").Value, "0.00"))
            txtRemarks.Text = IIf(IsDbNull(RsFuelCons.Fields("REMARKS").Value), "", RsFuelCons.Fields("REMARKS").Value)
            txtEmpCode.Text = IIf(IsDbNull(RsFuelCons.Fields("EMP_CODE").Value), "", RsFuelCons.Fields("EMP_CODE").Value)
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            Call LastReading()
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsFuelCons, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
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
End Class
