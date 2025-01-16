Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCO2Consump
    Inherits System.Windows.Forms.Form
    Dim RsConsump As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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
            If RsConsump.EOF = False Then RsConsump.MoveFirst()
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
        If Not RsConsump.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_CO2COSUMP_TRN", (txtNumber.Text), RsConsump) = False Then GoTo DelErrPart
                If DeleteStockTRN(PubDBCn, ConStockRefType_CON, (txtNumber.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_CO2COSUMP_TRN WHERE AUTO_KEY_CONSUMP=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsConsump.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsConsump.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsConsump, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        Dim mUOM As String
        Dim mDivisionCode As Double


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        ''Update Stock .....
        If DeleteStockTRN(PubDBCn, ConStockRefType_CON, (txtNumber.Text)) = False Then GoTo ErrPart

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO MAN_CO2COSUMP_TRN " & vbCrLf _
                            & " (AUTO_KEY_CONSUMP,COMPANY_CODE,FYEAR," & vbCrLf _
                            & " DOC_DATE,ITEM_CODE_LIQ,CARBON_KG,ITEM_CODE_CYL,CYLINDER_NO,EACH_CYLINDER_KG," & vbCrLf _
                            & " TOT_CYLINDER_KG,REMARKS,SIGN_EMP_CODE,DEPT_CODE_FROM, DEPT_CODE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE,DIV_CODE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtItemCodeLiq.Text) & "'," & Val(txtCarbonKg.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtItemCodeCyl.Text) & "'," & Val(txtCylinderNo.Text) & "," & Val(txtEachCylinderKg.Text) & "," & vbCrLf _
                            & " " & Val(lblTotCylinderKg.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "','" & MainClass.AllowSingleQuote(txtDeptFrom.Text) & "','" & MainClass.AllowSingleQuote(txtDept.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ")"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_CO2COSUMP_TRN SET " & vbCrLf _
                    & " AUTO_KEY_CONSUMP=" & mSlipNo & "," & vbCrLf _
                    & " DOC_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " ITEM_CODE_LIQ='" & MainClass.AllowSingleQuote(txtItemCodeLiq.Text) & "'," & vbCrLf _
                    & " CARBON_KG=" & Val(txtCarbonKg.Text) & "," & vbCrLf _
                    & " ITEM_CODE_CYL='" & MainClass.AllowSingleQuote(txtItemCodeCyl.Text) & "'," & vbCrLf _
                    & " CYLINDER_NO=" & Val(txtCylinderNo.Text) & ", " & vbCrLf _
                    & " EACH_CYLINDER_KG=" & Val(txtEachCylinderKg.Text) & "," & vbCrLf _
                    & " TOT_CYLINDER_KG=" & Val(lblTotCylinderKg.Text) & ", " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " SIGN_EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                    & " DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(txtDeptFrom.Text) & "', " & vbCrLf _
                    & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                    & " WHERE AUTO_KEY_CONSUMP =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If Trim(txtItemCodeLiq.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtItemCodeLiq.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                mUOM = MasterNo
            End If


            If UpdateStockTRN(PubDBCn, ConStockRefType_CON, (txtNumber.Text), 1, (txtDate.Text), (txtDate.Text), "ST", Trim(txtItemCodeLiq.Text), mUOM, CStr(-1), Val(txtCarbonKg.Text), 0, "O", 0, 0, "", "", (txtDept.Text), (txtDeptFrom.Text), "", "N", "To : " & lblDept.Text, "-1", ConSH, mDivisionCode, "", "") = False Then GoTo ErrPart
        End If

        If Trim(txtItemCodeCyl.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtItemCodeCyl.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                mUOM = MasterNo
            End If

            If UpdateStockTRN(PubDBCn, ConStockRefType_CON, (txtNumber.Text), 2, (txtDate.Text), (txtDate.Text), "ST", (txtItemCodeCyl.Text), mUOM, CStr(-1), Val(txtCylinderNo.Text), 0, "O", 0, 0, "", "", (txtDept.Text), (txtDeptFrom.Text), "", "N", "To : " & lblDept.Text, "-1", ConSH, mDivisionCode, "", "") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsConsump.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_CONSUMP)  " & vbCrLf & " FROM MAN_CO2COSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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
            lblDept.text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchEmpCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmpCode.Click
        Call SearchEmp(txtEmpCode, lblEmpCode)
    End Sub

    Private Sub cmdSearchICodeCyl_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchICodeCyl.Click
        Call SearchCOItem(txtItemCodeCyl, lblItemCodeCyl)
    End Sub

    Private Sub cmdSearchICodeLiq_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchICodeLiq.Click
        Call SearchCOItem(txtItemCodeLiq, lblItemCodeLiq)
    End Sub
    Private Sub SearchCOItem(ByRef pTxt As System.Windows.Forms.TextBox, ByRef plbl As System.Windows.Forms.Label)
        On Error GoTo SERR
        Dim SqlStr As String
        SqlStr = "SELECT I.ITEM_CODE,I.ITEM_SHORT_DESC, ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST I " & vbCrLf & " WHERE I.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CLASSIFICATION='2'" & vbCrLf & " ORDER BY I.ITEM_SHORT_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            pTxt.Text = AcName
            plbl.text = AcName1
            pTxt.Focus()
        End If
        Exit Sub
SERR:
        MsgBox(Err.Description)
    End Sub
    Private Function ValidateCOItem(ByRef pTxt As System.Windows.Forms.TextBox, ByRef plbl As System.Windows.Forms.Label) As Boolean

        On Error GoTo SERR
        Dim SqlStr As String
        Dim mRs As ADODB.Recordset
        ValidateCOItem = True
        If Trim(pTxt.Text) = "" Then Exit Function
        SqlStr = "SELECT I.ITEM_CODE,I.ITEM_SHORT_DESC " & vbCrLf _
                    & " FROM INV_ITEM_MST I" & vbCrLf _
                    & " WHERE I.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_CLASSIFICATION='2' " & vbCrLf _
                    & " AND I.ITEM_CODE='" & MainClass.AllowSingleQuote(pTxt.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
        With mRs
            If Not .EOF Then
                plbl.Text = IIf(IsDbNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
            Else
                ValidateCOItem = False
            End If
        End With
        Exit Function
SERR:
        ValidateCOItem = False
        MsgBox(Err.Description)
    End Function

    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CONSUMP,LENGTH(AUTO_KEY_CONSUMP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "MAN_CO2COSUMP_TRN", "AUTO_KEY_CONSUMP", "MACHINE_NO", , , SqlStr) = True Then
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
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsConsump, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCO2Consump_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Dept Wise CO2 Consumption"

        SqlStr = "Select * From MAN_CO2COSUMP_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsump, ADODB.LockTypeEnum.adLockReadOnly)


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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_CONSUMP AS DOC_NUMBER,TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOC_DATE, " & vbCrLf & " ITEM_CODE_LIQ,CARBON_KG,ITEM_CODE_CYL,CYLINDER_NO,EACH_CYLINDER_KG,  " & vbCrLf & " TOT_CYLINDER_KG,REMARKS,SIGN_EMP_CODE " & vbCrLf & " FROM MAN_CO2COSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_CONSUMP"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmCO2Consump_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmCO2Consump_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(5580)
        Me.Width = VB6.TwipsToPixelsX(9285)

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

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
        txtItemCodeLiq.Text = ""
        lblItemCodeLiq.Text = ""
        txtCarbonKg.Text = ""
        txtItemCodeCyl.Text = ""
        lblItemCodeCyl.Text = ""
        txtCylinderNo.Text = ""
        txtEachCylinderKg.Text = ""
        lblTotCylinderKg.Text = ""
        txtRemarks.Text = ""
        txtEmpCode.Text = ""
        lblEmpCode.Text = ""
        txtDept.Text = ""
        lblDept.Text = ""

        txtDeptFrom.Text = ""
        lblDeptFrom.Text = ""

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True
        '    txtDate.Enabled = False
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsConsump, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .set_ColWidth(1, 500 * 3)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 4)
            .set_ColWidth(10, 500 * 4)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsConsump.Fields("AUTO_KEY_CONSUMP").Precision
        txtDate.Maxlength = RsConsump.Fields("DOC_DATE").DefinedSize - 6
        txtItemCodeLiq.Maxlength = RsConsump.Fields("ITEM_CODE_LIQ").DefinedSize
        txtCarbonKg.Maxlength = RsConsump.Fields("CARBON_KG").Precision - RsConsump.Fields("CARBON_KG").NumericScale
        txtItemCodeCyl.Maxlength = RsConsump.Fields("ITEM_CODE_CYL").DefinedSize
        txtCylinderNo.Maxlength = RsConsump.Fields("CYLINDER_NO").Precision - RsConsump.Fields("CYLINDER_NO").NumericScale
        txtEachCylinderKg.Maxlength = RsConsump.Fields("EACH_CYLINDER_KG").Precision - RsConsump.Fields("EACH_CYLINDER_KG").NumericScale
        txtRemarks.Maxlength = RsConsump.Fields("REMARKS").DefinedSize
        txtEmpCode.Maxlength = RsConsump.Fields("SIGN_EMP_CODE").DefinedSize
        txtDept.Maxlength = RsConsump.Fields("DEPT_CODE").DefinedSize
        txtDeptFrom.Maxlength = RsConsump.Fields("DEPT_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer

        Dim mDivisionCode As Double
        Dim mCheckLastEntryDate As String
        Dim mStockQty As Double
        Dim mItemUOM As String

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsConsump.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDeptFrom.Text) = "" Then
            MsgInformation("From Dept is empty, So unable to save.")
            txtDeptFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Dept is empty, So unable to save.")
            txtDept.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtItemCodeLiq.Text) = "" And Trim(txtItemCodeCyl.Text) = "" Then
            MsgBox("Both Item Code Is Empty, So unable to save.")
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtCarbonKg.Text) <= 0 And Val(lblTotCylinderKg.Text) <= 0 Then
            MsgBox("Can not be saved for zero consumption")
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtCarbonKg.Text) > 0 Then
            If Trim(txtItemCodeLiq.Text) = "" Then
                MsgBox("Item Code For Liquid Is Empty,So Can not be saved.")
                FieldsVarification = False
                If txtItemCodeLiq.Enabled = True Then txtItemCodeLiq.Focus()
                Exit Function
            End If
        End If
        If Val(lblTotCylinderKg.Text) > 0 Then
            If Trim(txtItemCodeCyl.Text) = "" Then
                MsgBox("Item Code For Cylinder Is Empty,So Can not be saved.")
                FieldsVarification = False
                If txtItemCodeCyl.Enabled = True Then txtItemCodeCyl.Focus()
                Exit Function
            End If
        End If
        If Trim(txtItemCodeLiq.Text) = Trim(txtItemCodeCyl.Text) Then
            MsgBox("Both Item Code Is Same, So unable to save.")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Sign Emp is empty, So unable to save.")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Division Does Not Exist In Master", vbInformation)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            mDivisionCode = Val(MasterNo)
        End If



        If PubSuperUser <> "S" Then
            mCheckLastEntryDate = GetLastEntryDate
            If mCheckLastEntryDate <> "" Then
                If CDate(txtDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If MainClass.ValidateWithMasterTable(txtItemCodeLiq.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mItemUOM = MasterNo
        End If

        mStockQty = GetBalanceStockQty(Trim(txtItemCodeLiq.Text), (txtDate.Text), mItemUOM, Trim(txtDeptFrom.Text), "ST", "", ConSH, mDivisionCode, ConStockRefType_CON, Val(txtNumber.Text))

        If mStockQty < Val(txtCarbonKg.Text) Then
            MsgBox("You have Only Balance Stock is " & mStockQty & " " & mItemUOM & " For Item Code " & Trim(txtItemCodeLiq.Text), MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtItemCodeCyl.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mItemUOM = MasterNo
        End If

        mStockQty = GetBalanceStockQty(Trim(txtItemCodeCyl.Text), (txtDate.Text), mItemUOM, Trim(txtDeptFrom.Text), "ST", "", ConSH, mDivisionCode, ConStockRefType_CON, Val(txtNumber.Text))

        If mStockQty < Val(txtCylinderNo.Text) Then
            MsgBox("You have Only Balance Stock is " & mStockQty & " " & mItemUOM & " For Item Code " & Trim(txtItemCodeCyl.Text), MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = ""

        SqlStr = "SELECT Max(DOC_DATE) AS  REF_DATE " & vbCrLf & " FROM MAN_CO2COSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        ''& vbCrLf _
        ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function

    Private Sub CalcTotCylinderKg()
        lblTotCylinderKg.Text = VB6.Format(Val(txtCylinderNo.Text) * Val(txtEachCylinderKg.Text), "#0.00")
    End Sub
    Private Sub frmCO2Consump_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsConsump.Close()
        RsConsump = Nothing
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


    Private Sub txtCarbonKg_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarbonKg.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCarbonKg_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarbonKg.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCylinderNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCylinderNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCylinderNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCylinderNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCylinderNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCylinderNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTotCylinderKg()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Dept Does Not Exist In Master.")
            Cancel = True
        Else
            lblDept.text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDeptFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptFrom.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDeptFrom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptFrom.DoubleClick
        Call cmdSearchDeptFrom_Click(cmdSearchDeptFrom, New System.EventArgs())
    End Sub
    Private Sub txtDeptFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeptFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeptFrom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDeptFrom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeptFrom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDeptFrom_Click(cmdSearchDeptFrom, New System.EventArgs())
    End Sub

    Private Sub txtDeptFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeptFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtDeptFrom.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.ValidateWithMasterTable(txtDeptFrom.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Dept Does Not Exist In Master.")
            Cancel = True
        Else
            lblDeptFrom.text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchDeptFrom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDeptFrom.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDeptFrom.Text = AcName1
            lblDeptFrom.text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtEachCylinderKg_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEachCylinderKg.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEachCylinderKg_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEachCylinderKg.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEachCylinderKg_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEachCylinderKg.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTotCylinderKg()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCodeCyl_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCodeCyl.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCodeCyl_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCodeCyl.DoubleClick
        Call cmdSearchICodeCyl_Click(cmdSearchICodeCyl, New System.EventArgs())
    End Sub

    Private Sub txtItemCodeCyl_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCodeCyl.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCodeCyl.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCodeCyl_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCodeCyl.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchICodeCyl_Click(cmdSearchICodeCyl, New System.EventArgs())
    End Sub

    Private Sub txtItemCodeCyl_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCodeCyl.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateCOItem(txtItemCodeCyl, lblItemCodeCyl) = False Then
            MsgBox("Not A Valid CO2 Gas Item")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCodeLiq_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCodeLiq.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCodeLiq_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCodeLiq.DoubleClick
        Call cmdSearchICodeLiq_Click(cmdSearchICodeLiq, New System.EventArgs())
    End Sub

    Private Sub txtItemCodeLiq_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCodeLiq.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCodeLiq.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCodeLiq_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCodeLiq.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchICodeLiq_Click(cmdSearchICodeLiq, New System.EventArgs())
    End Sub

    Private Sub txtItemCodeLiq_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCodeLiq.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateCOItem(txtItemCodeLiq, lblItemCodeLiq) = False Then
            MsgBox("Not A Valid CO2 Gas Item")
            Cancel = True
        End If
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
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        If Not RsConsump.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsConsump.Fields("AUTO_KEY_CONSUMP").Value), "", RsConsump.Fields("AUTO_KEY_CONSUMP").Value)
            txtNumber.Text = IIf(IsDbNull(RsConsump.Fields("AUTO_KEY_CONSUMP").Value), "", RsConsump.Fields("AUTO_KEY_CONSUMP").Value)
            txtDate.Text = IIf(IsDbNull(RsConsump.Fields("DOC_DATE").Value), "", RsConsump.Fields("DOC_DATE").Value)
            txtItemCodeLiq.Text = IIf(IsDbNull(RsConsump.Fields("ITEM_CODE_LIQ").Value), "", RsConsump.Fields("ITEM_CODE_LIQ").Value)
            txtItemCodeLiq_Validating(txtItemCodeLiq, New System.ComponentModel.CancelEventArgs(False))
            txtCarbonKg.Text = IIf(IsDbNull(RsConsump.Fields("CARBON_KG").Value), "", RsConsump.Fields("CARBON_KG").Value)
            txtItemCodeCyl.Text = IIf(IsDbNull(RsConsump.Fields("ITEM_CODE_CYL").Value), "", RsConsump.Fields("ITEM_CODE_CYL").Value)
            txtItemCodeCyl_Validating(txtItemCodeCyl, New System.ComponentModel.CancelEventArgs(False))
            txtCylinderNo.Text = IIf(IsDbNull(RsConsump.Fields("CYLINDER_NO").Value), "", RsConsump.Fields("CYLINDER_NO").Value)
            txtEachCylinderKg.Text = IIf(IsDbNull(RsConsump.Fields("EACH_CYLINDER_KG").Value), "", RsConsump.Fields("EACH_CYLINDER_KG").Value)
            lblTotCylinderKg.Text = IIf(IsDbNull(RsConsump.Fields("TOT_CYLINDER_KG").Value), "", RsConsump.Fields("TOT_CYLINDER_KG").Value)
            txtRemarks.Text = IIf(IsDbNull(RsConsump.Fields("REMARKS").Value), "", RsConsump.Fields("REMARKS").Value)
            txtEmpCode.Text = IIf(IsDbNull(RsConsump.Fields("SIGN_EMP_CODE").Value), "", RsConsump.Fields("SIGN_EMP_CODE").Value)
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            txtDeptFrom.Text = IIf(IsDbNull(RsConsump.Fields("DEPT_CODE_FROM").Value), "", RsConsump.Fields("DEPT_CODE_FROM").Value)
            txtDeptFrom_Validating(txtDeptFrom, New System.ComponentModel.CancelEventArgs(False))

            txtDept.Text = IIf(IsDbNull(RsConsump.Fields("DEPT_CODE").Value), "", RsConsump.Fields("DEPT_CODE").Value)
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))

            mDivisionCode = IIf(IsDbNull(RsConsump.Fields("DIV_CODE").Value), -1, RsConsump.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionDesc = Trim(MasterNo)
                cboDivision.Text = mDivisionDesc
            End If
            cboDivision.Enabled = False

            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsConsump, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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


        '    If Trim(txtNumber.Text) = "" Then Exit Sub
        '    mSlipNo = Val(txtNumber.Text)
        '
        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub

        If Len(Trim(txtNumber.Text)) < 6 Then
            txtNumber.Text = txtNumber.Text & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsConsump.BOF = False Then xMKey = RsConsump.Fields("AUTO_KEY_CONSUMP").Value

        SqlStr = "SELECT * FROM MAN_CO2COSUMP_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CONSUMP,LENGTH(AUTO_KEY_CONSUMP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CONSUMP=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsump, ADODB.LockTypeEnum.adLockReadOnly)
        If RsConsump.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_CO2COSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CONSUMP,LENGTH(AUTO_KEY_CONSUMP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CONSUMP=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsump, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = mMode
        txtItemCodeLiq.Enabled = mMode
        txtCarbonKg.Enabled = mMode
        txtItemCodeCyl.Enabled = mMode
        txtCylinderNo.Enabled = mMode
        txtEachCylinderKg.Enabled = mMode
        txtEmpCode.Enabled = mMode
        cmdSearchEmpCode.Enabled = mMode
        txtDept.Enabled = mMode
        cmdSearchDept.Enabled = mMode
        txtDeptFrom.Enabled = mMode
        cmdSearchDeptFrom.Enabled = mMode
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

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
