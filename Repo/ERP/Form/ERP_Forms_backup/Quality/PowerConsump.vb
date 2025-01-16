Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPowerConsump
    Inherits System.Windows.Forms.Form
    Dim RsPower As ADODB.Recordset
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
            If RsPower.EOF = False Then RsPower.MoveFirst()
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
        If Not RsPower.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_POWERCOSUMP_TRN", (txtNumber.Text), RsPower) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_POWERCOSUMP_TRN WHERE AUTO_KEY_POWER=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsPower.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsPower.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPower, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            SqlStr = " INSERT INTO MAN_POWERCOSUMP_TRN " & vbCrLf _
                            & " (AUTO_KEY_POWER,COMPANY_CODE,FYEAR," & vbCrLf _
                            & " DOC_DATE,TOT_UNIT,UNIT_RATE," & vbCrLf _
                            & " TOT_UNIT_COST,WORK_HOUR,REMARKS," & vbCrLf _
                            & " SIGN_EMP_CODE,DEPT_CODE,METER_READING, METER_FACTOR," & vbCrLf _
                            & " ADDUSER,ADDDATE,MODUSER,MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(txtTotUnit.Text) & "," & Val(txtRate.Text) & ", " & vbCrLf _
                            & " " & Val(lblTotalCost.Text) & "," & Val(txtHour.Text) & ",'" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "','" & MainClass.AllowSingleQuote(txtDept.Text) & "'," & Val(txtMeterReading.Text) & ", " & Val(txtFactor.Text) & "," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_POWERCOSUMP_TRN SET " & vbCrLf _
                    & " AUTO_KEY_POWER=" & mSlipNo & "," & vbCrLf _
                    & " DOC_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TOT_UNIT=" & Val(txtTotUnit.Text) & "," & vbCrLf _
                    & " UNIT_RATE=" & Val(txtRate.Text) & "," & vbCrLf _
                    & " WORK_HOUR=" & Val(txtHour.Text) & "," & vbCrLf _
                    & " TOT_UNIT_COST=" & Val(lblTotalCost.text) & ", " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " SIGN_EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                    & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                    & " METER_READING=" & Val(txtMeterReading.Text) & ", " & vbCrLf _
                    & " METER_FACTOR=" & Val(txtFactor.Text) & ", " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE AUTO_KEY_POWER =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsPower.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_POWER)  " & vbCrLf & " FROM MAN_POWERCOSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDept.Text = AcName
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_POWER,LENGTH(AUTO_KEY_POWER)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "MAN_POWERCOSUMP_TRN", "AUTO_KEY_POWER", "DOC_DATE", "DEPT_CODE", "METER_READING", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsPower, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPowerConsump_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Dept Wise Power Consumption"

        SqlStr = "Select * From MAN_POWERCOSUMP_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPower, ADODB.LockTypeEnum.adLockReadOnly)


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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_POWER AS REF_NUM,TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOCDATE, " & vbCrLf & " DEPT_CODE AS DEPT,TOT_UNIT AS UNITS,UNIT_RATE AS RATE,TOT_UNIT_COST AS COST  " & vbCrLf & " FROM MAN_POWERCOSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_POWER"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPowerConsump_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPowerConsump_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(3810)
        Me.Width = VB6.TwipsToPixelsX(9285)
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
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtTotUnit.Text = ""
        txtRate.Text = ""
        lblTotalCost.Text = ""
        txtRemarks.Text = ""
        txtEmpCode.Text = ""
        lblEmpCode.Text = ""
        txtDept.Text = ""
        lblDept.Text = ""
        txtHour.Text = ""
        txtLastReading.Text = ""
        txtMeterReading.Text = ""
        txtFactor.Text = IIf(RsCompany.Fields("COMPANY_CODE").Value = 1, 15, 1)
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsPower, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        txtNumber.Maxlength = RsPower.Fields("AUTO_KEY_POWER").Precision
        txtDate.Maxlength = RsPower.Fields("DOC_DATE").DefinedSize - 6
        txtTotUnit.Maxlength = RsPower.Fields("TOT_UNIT").Precision - RsPower.Fields("TOT_UNIT").NumericScale
        txtRate.Maxlength = RsPower.Fields("UNIT_RATE").Precision - RsPower.Fields("UNIT_RATE").NumericScale
        txtHour.Maxlength = RsPower.Fields("WORK_HOUR").Precision - RsPower.Fields("WORK_HOUR").NumericScale
        txtRemarks.Maxlength = RsPower.Fields("REMARKS").DefinedSize
        txtEmpCode.Maxlength = RsPower.Fields("SIGN_EMP_CODE").DefinedSize
        txtDept.Maxlength = RsPower.Fields("DEPT_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
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
        If MODIFYMode = True And RsPower.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDept.Text) = "" Then
            MsgInformation("Dept is empty, So unable to save.")
            txtDept.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMeterReading.Text) = "" Then
            MsgInformation("Current Meter Reading is empty, So unable to save.")
            txtMeterReading.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtFactor.Text) <= 0 Then
            MsgInformation("Factor is empty, So unable to save.")
            txtFactor.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(lblTotalCost.Text) <= 0 Then
            MsgBox("Can not be saved for zero consumption")
            FieldsVarification = False
            Exit Function
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
        'Resume
    End Function

    Private Sub CalcTot()

        Dim mRate As Double
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        mSqlStr = "SELECT GETELECTRICRATE(" & RsCompany.Fields("COMPANY_CODE").Value & ",TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS ERATE FROM DUAL"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            txtRate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ERATE").Value), 0, RsTemp.Fields("ERATE").Value), "0.00")
        Else
            txtRate.Text = CStr(0)
        End If
        lblTotalCost.Text = VB6.Format(Val(txtTotUnit.Text) * Val(txtRate.Text), "#0.00") '''* Val(txtHour.Text), "#0.00")
    End Sub

    Private Sub frmPowerConsump_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsPower.Close()
        RsPower = Nothing
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
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

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFactor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFactor.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFactor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFactor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFactor_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFactor.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtMeterReading.Text) = "" Then GoTo EventExitSub

        txtMeterReading.Text = VB6.Format(txtMeterReading.Text, "0.000")
        txtTotUnit.Text = VB6.Format((Val(txtMeterReading.Text) - Val(txtLastReading.Text)) * Val(txtFactor.Text), "0.000")
        Call CalcTot()

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtHour_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHour.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHour_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHour.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHour_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHour.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Val(txtHour.Text) > 24 Then
            MsgInformation("Hour Cann't be Greater than 24.")
            Cancel = False
            GoTo EventExitSub
        End If
        Call CalcTot()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMeterReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMeterReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMeterReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMeterReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMeterReading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMeterReading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtMeterReading.Text) = "" Then GoTo EventExitSub

        If Val(txtMeterReading.Text) <= Val(txtLastReading.Text) Then
            MsgInformation("Current Meter Reading should be greater than Last Meter Reading")
            Cancel = True
        Else
            txtMeterReading.Text = VB6.Format(txtMeterReading.Text, "0.000")
            txtTotUnit.Text = VB6.Format((Val(txtMeterReading.Text) - Val(txtLastReading.Text)) * Val(txtFactor.Text), "0.000")
            Call CalcTot()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValEMP
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mOpenDate As String
        Dim mOpenReading As Double
        Dim mMaxReading As Double

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Dept Does Not Exist In Master.")
            Cancel = True
        Else
            lblDept.Text = MasterNo

            SqlStr = " SELECT OPEN_DATE,UNIT_RATE,OPEN_READING FROM MAN_POWER_OPEN " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' " & vbCrLf _
                            & " AND OPEN_DATE= (" & vbCrLf _
                            & " SELECT MAX(OPEN_DATE) FROM MAN_POWER_OPEN " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' "
            If Trim(txtDate.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND OPEN_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
            SqlStr = SqlStr & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mOpenDate = IIf(IsDbNull(RsTemp.Fields("OPEN_DATE").Value), "", RsTemp.Fields("OPEN_DATE").Value)
                mOpenReading = IIf(IsDbNull(RsTemp.Fields("OPEN_READING").Value), "", RsTemp.Fields("OPEN_READING").Value)
                '            txtRate.Text = IIf(isdbnull(RsTemp!UNIT_RATE), "", Format(RsTemp!UNIT_RATE, "0.00"))
            End If
            SqlStr = " SELECT MAX(METER_READING) AS METER_READING FROM MAN_POWERCOSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' "
            If Trim(mOpenDate) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND DOC_DATE>=TO_DATE('" & VB6.Format(mOpenDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
            If Trim(txtDate.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND DOC_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
            If Val(lblMkey.Text) <> 0 Then
                SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_POWER<>" & Val(txtNumber.Text)
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mMaxReading = IIf(IsDbNull(RsTemp.Fields("METER_READING").Value), 0, RsTemp.Fields("METER_READING").Value)
            End If
            txtLastReading.Text = IIf(mMaxReading > mOpenReading, mMaxReading, mOpenReading)
            txtLastReading.Text = VB6.Format(txtLastReading.Text, "0.000")
            If txtMeterReading.Enabled Then txtMeterReading.Focus()
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTot()
        eventArgs.Cancel = Cancel
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
            GoTo EventExitSub
        End If
        If CDate(txtDate.Text) > CDate(PubCurrDate) Then
            MsgBox("Date Cann't be Greater than Current Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsPower.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsPower.Fields("AUTO_KEY_POWER").Value), "", RsPower.Fields("AUTO_KEY_POWER").Value)
            txtNumber.Text = IIf(IsDbNull(RsPower.Fields("AUTO_KEY_POWER").Value), "", RsPower.Fields("AUTO_KEY_POWER").Value)
            txtDate.Text = IIf(IsDbNull(RsPower.Fields("DOC_DATE").Value), "", RsPower.Fields("DOC_DATE").Value)
            txtTotUnit.Text = IIf(IsDBNull(RsPower.Fields("TOT_UNIT").Value), "", VB6.Format(RsPower.Fields("TOT_UNIT").Value, "0.000"))
            txtHour.Text = IIf(IsDbNull(RsPower.Fields("WORK_HOUR").Value), "", RsPower.Fields("WORK_HOUR").Value)
            txtRate.Text = IIf(IsDbNull(RsPower.Fields("UNIT_RATE").Value), "", VB6.Format(RsPower.Fields("UNIT_RATE").Value, "0.00"))
            lblTotalCost.Text = IIf(IsDbNull(RsPower.Fields("TOT_UNIT_COST").Value), "", VB6.Format(RsPower.Fields("TOT_UNIT_COST").Value, "0.00"))
            txtRemarks.Text = IIf(IsDbNull(RsPower.Fields("REMARKS").Value), "", RsPower.Fields("REMARKS").Value)
            txtEmpCode.Text = IIf(IsDbNull(RsPower.Fields("SIGN_EMP_CODE").Value), "", RsPower.Fields("SIGN_EMP_CODE").Value)
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            txtMeterReading.Text = IIf(IsDBNull(RsPower.Fields("METER_READING").Value), "", VB6.Format(RsPower.Fields("METER_READING").Value, "0.000"))
            txtFactor.Text = IIf(IsDbNull(RsPower.Fields("METER_FACTOR").Value), "", VB6.Format(RsPower.Fields("METER_FACTOR").Value, "0.00"))

            txtDept.Text = IIf(IsDbNull(RsPower.Fields("DEPT_CODE").Value), "", RsPower.Fields("DEPT_CODE").Value)
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsPower, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        If MODIFYMode = True And RsPower.BOF = False Then xMKey = RsPower.Fields("AUTO_KEY_POWER").Value

        SqlStr = "SELECT * FROM MAN_POWERCOSUMP_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_POWER,LENGTH(AUTO_KEY_POWER)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_POWER=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPower, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPower.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_POWERCOSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_POWER,LENGTH(AUTO_KEY_POWER)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_POWER=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPower, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtTotUnit.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtRate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtHour.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtEmpCode.Enabled = mMode
        cmdSearchEmpCode.Enabled = mMode
        txtDept.Enabled = True ''IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        cmdSearchDept.Enabled = True ''IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
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

    Private Sub txtTotUnit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotUnit.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotUnit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotUnit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotUnit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotUnit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtTotUnit.Text) = "" Then GoTo EventExitSub

        If Val(txtTotUnit.Text) <= 0 Then
            MsgInformation("Total Unit should be greater than Zero")
            Cancel = True
        Else
            txtTotUnit.Text = VB6.Format(txtTotUnit.Text, "0.000")
            If Val(txtFactor.Text) = 0 Then
                txtMeterReading.Text = VB6.Format(Val(txtLastReading.Text) + (Val(txtTotUnit.Text)), "0.000")
            Else
                txtMeterReading.Text = VB6.Format(Val(txtLastReading.Text) + (Val(txtTotUnit.Text) / Val(txtFactor.Text)), "0.000")
            End If
            Call CalcTot()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
