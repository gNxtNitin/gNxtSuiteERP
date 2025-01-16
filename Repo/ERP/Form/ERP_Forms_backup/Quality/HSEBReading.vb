Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmHSEBReading
    Inherits System.Windows.Forms.Form
    Dim RsHSEB As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

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
                If InsertIntoDelAudit(PubDBCn, "MAN_HSEB_TRN", (txtNumber.Text), RsHSEB) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_HSEB_TRN WHERE AUTO_KEY_HSEB=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsHSEB.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
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
        Call CalcPF()
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

        If MainClass.ValidateWithMasterTable(Trim(cboMeterNo.Text), "METER_NAME", "METER_CODE", "MAN_HSBC_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
            SqlStr = " INSERT INTO MAN_HSEB_TRN " & vbCrLf _
                            & " (AUTO_KEY_HSEB, COMPANY_CODE, FYEAR, " & vbCrLf _
                            & " READING_DATE, READING_TIME, KWH, KWH_DAY, " & vbCrLf _
                            & " KVARHG, KVRHD, KVAH, KVAH_DAY, " & vbCrLf _
                            & " KVA_MDI, KVA8, PF, PF_DAY, " & vbCrLf _
                            & " REMARKS, EMP_CODE, " & vbCrLf _
                            & " METER_CODE, MULTI_FACTOR, DIV_CODE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & "," & RsCompany.fields("FYEAR").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & txtTime.Text & "', 'HH24:MI'), " & vbCrLf _
                            & " " & Val(txtKWH.Text) & "," & Val(txtKWHDay.Text) & ", " & vbCrLf _
                            & " " & Val(txtKVARHG.Text) & "," & Val(txtKVRHD.Text) & ", " & vbCrLf _
                            & " " & Val(txtKVAH.Text) & "," & Val(txtKVAHDay.Text) & ", " & vbCrLf _
                            & " " & Val(txtKVAMDI.Text) & "," & Val(txtKVA8.Text) & ", " & vbCrLf _
                            & " " & Val(txtPF.Text) & "," & Val(txtPFDay.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                            & " " & mMeterCode & ", " & Val(txtMultiFactor.Text) & ", " & Val(txtDivCode.Text) & "," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_HSEB_TRN SET " & vbCrLf _
                    & " AUTO_KEY_HSEB=" & mSlipNo & "," & vbCrLf _
                    & " READING_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " READING_TIME=TO_DATE('" & txtTime.Text & "', 'HH24:MI'), " & vbCrLf _
                    & " KWH=" & Val(txtKWH.Text) & "," & vbCrLf _
                    & " KWH_DAY=" & Val(txtKWHDay.Text) & "," & vbCrLf _
                    & " KVARHG=" & Val(txtKVARHG.Text) & "," & vbCrLf _
                    & " KVRHD=" & Val(txtKVRHD.Text) & ", " & vbCrLf _
                    & " KVAH=" & Val(txtKVAH.Text) & ", " & vbCrLf _
                    & " KVAH_DAY=" & Val(txtKVAHDay.Text) & ", " & vbCrLf _
                    & " KVA_MDI=" & Val(txtKVAMDI.Text) & ", " & vbCrLf _
                    & " KVA8=" & Val(txtKVA8.Text) & ", " & vbCrLf _
                    & " PF=" & Val(txtPF.Text) & ", " & vbCrLf _
                    & " PF_DAY=" & Val(txtPFDay.Text) & ", " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                    & " METER_CODE=" & mMeterCode & ", " & vbCrLf _
                    & " MULTI_FACTOR=" & Val(txtMultiFactor.Text) & ", " & vbCrLf _
                    & " DIV_CODE=" & Val(txtDivCode.Text) & ", " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE AUTO_KEY_HSEB =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
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
        SqlStr = "SELECT Max(AUTO_KEY_HSEB)  " & vbCrLf & " FROM MAN_HSEB_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""      ''  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "EMP_NAME", "USER_ID", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.Text = AcName
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_HSEB,LENGTH(AUTO_KEY_HSEB)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "MAN_HSEB_TRN", "AUTO_KEY_HSEB", "READING_DATE", "READING_TIME", "KWH", SqlStr) = True Then
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

    Private Sub frmHSEBReading_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "HSEB Reading Recording"

        SqlStr = "Select * From MAN_HSEB_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSEB, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT MMST.METER_NAME," & vbCrLf & " TRN.AUTO_KEY_HSEB AS REF_NUM, TO_CHAR(TRN.READING_DATE,'DD/MM/YYYY') AS READING_DATE, " & vbCrLf & " TO_CHAR(TRN.READING_TIME,'HH24:MI') AS READING_TIME, TRN.KWH, TRN.KWH_DAY, TRN.KVARHG, TRN.KVRHD, " & vbCrLf & " TRN.KVAH, TRN.KVAH_DAY, TRN.KVA_MDI, TRN.KVA8, TRN.PF, TRN.PF_DAY, TRN.REMARKS, TRN.EMP_CODE " & vbCrLf & " FROM MAN_HSEB_TRN TRN, MAN_HSBC_METER_MST MMST" & vbCrLf & " WHERE TRN.COMPANY_CODE = MMST.COMPANY_CODE " & vbCrLf & " AND TRN.METER_CODE = MMST.METER_CODE" & vbCrLf & " AND TRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY MMST.METER_NAME, TRN.READING_DATE,TO_CHAR(TRN.READING_TIME,'HH24:MI'), TRN.AUTO_KEY_HSEB"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmHSEBReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmHSEBReading_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(5640)
        Me.Width = VB6.TwipsToPixelsX(9285)

        cboMeterNo.Items.Clear()

        SqlStr = "SELECT METER_NAME FROM MAN_HSBC_METER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY METER_NAME"
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
        txtLastKWHReading.Text = ""
        txtKWH.Text = ""
        txtKWHDay.Text = ""
        txtKVARHG.Text = ""
        txtKVRHD.Text = ""
        txtLastKVAHReading.Text = ""
        txtKVAH.Text = ""
        txtKVAHDay.Text = ""
        txtKVAMDI.Text = ""
        txtKVA8.Text = ""
        txtPF.Text = ""
        txtPFDay.Text = ""
        txtRemarks.Text = ""
        txtEmpCode.Text = ""
        lblEmpCode.Text = ""

        cboMeterNo.SelectedIndex = -1
        cboMeterNo.Enabled = True
        txtDivCode.Text = ""
        txtMultiFactor.Text = ""
        txtDivCode.Enabled = False
        txtMultiFactor.Enabled = False
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

        txtNumber.Maxlength = RsHSEB.Fields("AUTO_KEY_HSEB").Precision
        txtDate.Maxlength = RsHSEB.Fields("READING_DATE").DefinedSize - 6
        txtTime.Maxlength = RsHSEB.Fields("READING_TIME").DefinedSize - 11
        txtLastKWHReading.Maxlength = RsHSEB.Fields("KWH").Precision
        txtKWH.Maxlength = RsHSEB.Fields("KWH").Precision
        txtKWHDay.Maxlength = RsHSEB.Fields("KWH_DAY").Precision
        txtKVARHG.Maxlength = RsHSEB.Fields("KVARHG").Precision
        txtKVRHD.Maxlength = RsHSEB.Fields("KVRHD").Precision
        txtLastKVAHReading.Maxlength = RsHSEB.Fields("KVAH").Precision
        txtKVAH.Maxlength = RsHSEB.Fields("KVAH").Precision
        txtKVAHDay.Maxlength = RsHSEB.Fields("KVAH_DAY").Precision
        txtKVAMDI.Maxlength = RsHSEB.Fields("KVA_MDI").Precision
        txtKVA8.Maxlength = RsHSEB.Fields("KVA8").Precision
        txtPF.Maxlength = RsHSEB.Fields("PF").Precision
        txtPFDay.Maxlength = RsHSEB.Fields("PF_DAY").Precision
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
        If Trim(txtKWH.Text) = "" Then
            MsgInformation("KWH is empty, So unable to save.")
            txtKWH.Focus()
            FieldsVarification = False
            Exit Function
        End If
        '    If Trim(txtKVAH.Text) = "" Then
        '        MsgInformation "KVAH is empty, So unable to save."
        '        txtKVAH.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
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

        If MainClass.ValidateWithMasterTable(Trim(cboMeterNo.Text), "METER_NAME", "METER_CODE", "MAN_HSBC_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        If Val(txtMultiFactor.Text) <= 0 Then
            MsgInformation("Multi factor Cann't be Blank, So unable to save.")
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
        Call CalcPF()

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmHSEBReading_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsHSEB.Close()
        RsHSEB = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
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
        'pTextBox.Text = VB6.Format(pTextBox.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""  ''  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pLable.text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function

    Private Sub txtDivCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtKVA8_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKVA8.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtKVA8_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtKVA8.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtKVAHDay_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKVAHDay.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtKVAMDI_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKVAMDI.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtKVAMDI_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtKVAMDI.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtKVARHG_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKVARHG.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtKVARHG_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtKVARHG.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtKVRHD_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKVRHD.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtKVRHD_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtKVRHD.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtKWH_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKWH.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtKWH_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtKWH.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtKWH_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtKWH.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtKWH.Text) = "" Then GoTo EventExitSub

        If Val(txtKWH.Text) <= Val(txtLastKWHReading.Text) Then
            MsgInformation("Current KWH Reading should be greater than Last KWH Reading")
            Cancel = True
        Else
            Call CalcPF()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtKVAH_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKVAH.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtKVAH_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtKVAH.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtKVAH_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtKVAH.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtKVAH.Text) = "" Then GoTo EventExitSub

        If Val(txtKVAH.Text) < Val(txtLastKVAHReading.Text) Then
            MsgInformation("Current KVAH Reading should be greater than Last KVAH Reading")
            Cancel = True
        Else
            Call CalcPF()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtKWHDay_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKWHDay.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLastKWHReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLastKWHReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMultiFactor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMultiFactor.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPF.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPF_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPF.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPF.Text) = "" Then GoTo EventExitSub
        txtPF.Text = VB6.Format(txtPF.Text, "0.0000")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPFDay_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPFDay.TextChanged

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
        Dim mOpenKWHReading As Double
        Dim mOpenKVAHReading As Double
        Dim mMaxKWHReading As Double
        Dim mMaxKVAHReading As Double
        Dim mMeterCode As Double

        If MainClass.ValidateWithMasterTable(cboMeterNo.Text, "METER_NAME", "METER_CODE", "MAN_HSBC_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMeterCode = Val(MasterNo)
        End If

        If Trim(txtDate.Text) = "" Or Trim(txtTime.Text) = "" Then Exit Sub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        SqlStr = " SELECT OPEN_DATE,OPEN_KWH_READING,OPEN_KVAH_READING,KVA_MDI,KVA8 FROM MAN_HSEB_OPEN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND METER_CODE=" & Val(CStr(mMeterCode)) & "" & vbCrLf & " AND OPEN_DATE= (" & vbCrLf & " SELECT MAX(OPEN_DATE) FROM MAN_HSEB_OPEN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND OPEN_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND METER_CODE=" & Val(CStr(mMeterCode)) & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mOpenDate = IIf(IsDbNull(RsTemp.Fields("OPEN_DATE").Value), "", RsTemp.Fields("OPEN_DATE").Value)
            mOpenKWHReading = IIf(IsDbNull(RsTemp.Fields("OPEN_KWH_READING").Value), 0, RsTemp.Fields("OPEN_KWH_READING").Value)
            mOpenKVAHReading = IIf(IsDbNull(RsTemp.Fields("OPEN_KVAH_READING").Value), 0, RsTemp.Fields("OPEN_KVAH_READING").Value)

            ''26-aug-2009 'Dharampal KayJay Auto ..
            '        txtKVAMDI.Text = IIf(isdbnull(RsTemp!KVA_MDI), "", RsTemp!KVA_MDI)
            '        txtKVA8.Text = IIf(isdbnull(RsTemp!KVA8), "", RsTemp!KVA8)
        End If
        SqlStr = " SELECT MAX(KWH) AS KWH, MAX(KVAH) AS KVAH FROM MAN_HSEB_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND METER_CODE=" & Val(CStr(mMeterCode)) & ""
        If Trim(mOpenDate) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND READING_DATE>=TO_DATE('" & VB6.Format(mOpenDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If
        If Trim(txtDate.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND READING_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If
        If Val(lblMkey.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_HSEB<>" & Val(txtNumber.Text)
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mMaxKWHReading = IIf(IsDbNull(RsTemp.Fields("KWH").Value), 0, RsTemp.Fields("KWH").Value)
            mMaxKVAHReading = IIf(IsDbNull(RsTemp.Fields("KVAH").Value), 0, RsTemp.Fields("KVAH").Value)
        End If
        txtLastKWHReading.Text = IIf(mMaxKWHReading > mOpenKWHReading, mMaxKWHReading, mOpenKWHReading)
        txtLastKVAHReading.Text = IIf(mMaxKVAHReading > mOpenKVAHReading, mMaxKVAHReading, mOpenKVAHReading)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub GetMeterDetail()

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(cboMeterNo.Text) = "" Then Exit Sub

        SqlStr = " SELECT * FROM MAN_HSBC_METER_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND METER_NAME='" & Trim(cboMeterNo.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtDivCode.Text = IIf(IsDbNull(RsTemp.Fields("DIV_CODE").Value), "", RsTemp.Fields("DIV_CODE").Value)
            txtMultiFactor.Text = IIf(IsDbNull(RsTemp.Fields("MULTI_FACTOR").Value), "", RsTemp.Fields("MULTI_FACTOR").Value)
        Else
            txtDivCode.Text = ""
            txtMultiFactor.Text = ""
        End If

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcPF()
        On Error GoTo ERR1
        If Trim(txtKWH.Text) <> "" Then
            txtKWHDay.Text = CStr((Val(txtKWH.Text) - Val(txtLastKWHReading.Text)) * Val(txtMultiFactor.Text))
        End If
        If Trim(txtKVAH.Text) <> "" Then
            txtKVAHDay.Text = CStr((Val(txtKVAH.Text) - Val(txtLastKVAHReading.Text)) * Val(txtMultiFactor.Text))
        End If
        If Val(txtKWHDay.Text) = 0 Or Val(txtKVAHDay.Text) = 0 Then
            txtPFDay.Text = CStr(0)
        Else
            txtPFDay.Text = VB6.Format(Val(txtKWHDay.Text) / Val(txtKVAHDay.Text), "0.0000000000")
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
            Call CalcPF()
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
            lblMkey.Text = IIf(IsDbNull(RsHSEB.Fields("AUTO_KEY_HSEB").Value), "", RsHSEB.Fields("AUTO_KEY_HSEB").Value)
            txtNumber.Text = IIf(IsDbNull(RsHSEB.Fields("AUTO_KEY_HSEB").Value), "", RsHSEB.Fields("AUTO_KEY_HSEB").Value)
            txtDate.Text = IIf(IsDbNull(RsHSEB.Fields("READING_DATE").Value), "", RsHSEB.Fields("READING_DATE").Value)
            txtTime.Text = IIf(IsDbNull(RsHSEB.Fields("READING_TIME").Value), "", VB6.Format(RsHSEB.Fields("READING_TIME").Value, "HH:MM"))
            txtKWH.Text = IIf(IsDbNull(RsHSEB.Fields("KWH").Value), "", RsHSEB.Fields("KWH").Value)
            txtKWHDay.Text = IIf(IsDbNull(RsHSEB.Fields("KWH_DAY").Value), "", RsHSEB.Fields("KWH_DAY").Value)
            txtKVARHG.Text = IIf(IsDbNull(RsHSEB.Fields("KVARHG").Value), "", RsHSEB.Fields("KVARHG").Value)
            txtKVRHD.Text = IIf(IsDbNull(RsHSEB.Fields("KVRHD").Value), "", RsHSEB.Fields("KVRHD").Value)
            txtKVAH.Text = IIf(IsDbNull(RsHSEB.Fields("KVAH").Value), "", RsHSEB.Fields("KVAH").Value)
            txtKVAHDay.Text = IIf(IsDbNull(RsHSEB.Fields("KVAH_DAY").Value), "", RsHSEB.Fields("KVAH_DAY").Value)
            txtKVAMDI.Text = IIf(IsDbNull(RsHSEB.Fields("KVA_MDI").Value), "", RsHSEB.Fields("KVA_MDI").Value)
            txtKVA8.Text = IIf(IsDbNull(RsHSEB.Fields("KVA8").Value), "", RsHSEB.Fields("KVA8").Value)
            txtPF.Text = IIf(IsDBNull(RsHSEB.Fields("PF").Value), "", VB6.Format(RsHSEB.Fields("PF").Value, "0.0000"))
            txtPFDay.Text = IIf(IsDbNull(RsHSEB.Fields("PF_DAY").Value), "", VB6.Format(RsHSEB.Fields("PF_DAY").Value, "0.0000000000"))
            txtRemarks.Text = IIf(IsDbNull(RsHSEB.Fields("REMARKS").Value), "", RsHSEB.Fields("REMARKS").Value)
            txtEmpCode.Text = IIf(IsDbNull(RsHSEB.Fields("EMP_CODE").Value), "", RsHSEB.Fields("EMP_CODE").Value)
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))

            mMeterCode = IIf(IsDbNull(RsHSEB.Fields("METER_CODE").Value), -1, RsHSEB.Fields("METER_CODE").Value)

            If MainClass.ValidateWithMasterTable(mMeterCode, "METER_CODE", "METER_NAME", "MAN_HSBC_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mMeterDesc = Trim(MasterNo)
                cboMeterNo.Text = mMeterDesc
            End If
            txtDivCode.Text = IIf(IsDbNull(RsHSEB.Fields("DIV_CODE").Value), "", RsHSEB.Fields("DIV_CODE").Value)
            txtMultiFactor.Text = IIf(IsDbNull(RsHSEB.Fields("MULTI_FACTOR").Value), "", RsHSEB.Fields("MULTI_FACTOR").Value)

            cboMeterNo.Enabled = False

            Call LastReading()
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

        If MODIFYMode = True And RsHSEB.BOF = False Then xMKey = RsHSEB.Fields("AUTO_KEY_HSEB").Value

        SqlStr = "SELECT * FROM MAN_HSEB_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_HSEB,LENGTH(AUTO_KEY_HSEB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_HSEB=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSEB, ADODB.LockTypeEnum.adLockReadOnly)
        If RsHSEB.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_HSEB_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_HSEB,LENGTH(AUTO_KEY_HSEB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_HSEB=" & Val(CStr(xMKey)) & " "
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
        Call CalcPF()
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
End Class
