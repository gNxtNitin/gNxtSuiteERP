Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPredictiveChkSheet
    Inherits System.Windows.Forms.Form
    Dim RsPredictiveChkMain As ADODB.Recordset
    Dim RsPredictiveChkDetail As ADODB.Recordset
    Dim RsPredictiveItemDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColParameter As Short = 1
    Private Const ColObservation As Short = 2

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColStockQty As Short = 3
    Private Const ColUom As Short = 4
    Private Const ColQty As Short = 5
    Private Const ColRate As Short = 6
    Private Const ColAmount As Short = 7
    Private Const ColSavedItemCode As Short = 8
    Private Const ColSavedQty As Short = 9


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsPredictiveChkMain.EOF = False Then RsPredictiveChkMain.MoveFirst()
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

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsPredictiveChkMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_PREDICTIVE_HDR", (txtSlipNo.Text), RsPredictiveChkMain) = False Then GoTo DelErrPart
                If DeleteStockTRN(PubDBCn, ConStockRefType_PDM, (lblMkey.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_PREDICTIVE_DET WHERE AUTO_KEY_PRED=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM MAN_PREDICTIVE_HDR WHERE AUTO_KEY_PRED=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsPredictiveChkMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsPredictiveChkMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPredictiveChkMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
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
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mActionTaken As Byte
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If




        mActionTaken = cboAction.SelectedIndex

        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)

        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO MAN_PREDICTIVE_HDR " & vbCrLf _
                            & " (AUTO_KEY_PRED,COMPANY_CODE," & vbCrLf _
                            & " ENTRY_DATE,MACHINE_NO,AUTO_KEY_STD," & vbCrLf _
                            & " TEAM_MEMBERS,SIGN_EMP_CODE,ACTION_TAKEN, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE,DIV_CODE,DEPT_CODE,COST_CENTER_CODE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                            & " " & Val(txtInspectionStd.Text) & ",'" & MainClass.AllowSingleQuote(txtTeamMembers.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtSignCode.Text) & "'," & mActionTaken & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ",'" & MainClass.AllowSingleQuote(txtFromDept.Text) & "','" & MainClass.AllowSingleQuote((txtCost.Text)) & "')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_PREDICTIVE_HDR SET " & vbCrLf _
                    & " AUTO_KEY_PRED=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                    & " ENTRY_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                    & " AUTO_KEY_STD=" & Val(txtInspectionStd.Text) & ", COST_CENTER_CODE ='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'," & vbCrLf _
                    & " TEAM_MEMBERS='" & MainClass.AllowSingleQuote(txtTeamMembers.Text) & "', " & vbCrLf _
                    & " SIGN_EMP_CODE='" & MainClass.AllowSingleQuote(txtSignCode.Text) & "', " & vbCrLf _
                    & " ACTION_TAKEN=" & mActionTaken & ", DEPT_CODE ='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', DIV_CODE = " & mDivisionCode & "," & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_PRED =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail() = False Then GoTo ErrPart
        If UpdateItemDetail(mDivisionCode) = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsPredictiveChkMain.Requery()
        RsPredictiveChkDetail.Requery()
        RsPredictiveItemDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_PRED)  " & vbCrLf & " FROM MAN_PREDICTIVE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PRED,LENGTH(AUTO_KEY_PRED)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mParameter As String
        Dim mObservation As String

        PubDBCn.Execute("DELETE FROM MAN_PREDICTIVE_DET WHERE AUTO_KEY_PRED=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParameter
                mParameter = MainClass.AllowSingleQuote(.Text)

                .Col = ColObservation
                mObservation = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mParameter <> "" Then
                    SqlStr = " INSERT INTO  MAN_PREDICTIVE_DET ( " & vbCrLf & " AUTO_KEY_PRED,SERIAL_NO,PARAMETER,OBSERVATION) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mParameter & "', " & vbCrLf & " '" & mObservation & "') "
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

    Private Sub cmdSearchMacNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMacNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N'"
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", "MACHINE_ITEM_CODE", , SqlStr) = True Then
            txtMachineNo.Text = AcName1
            lblMachineNo.text = AcName
            If txtMachineNo.Enabled = True Then txtMachineNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchSignCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSignCode.Click
        Call SearchEmp(txtSignCode, lblSignCode)
    End Sub
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
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PRED,LENGTH(AUTO_KEY_PRED)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "MAN_PREDICTIVE_HDR", "AUTO_KEY_PRED", "MACHINE_NO", , , SqlStr) = True Then
            txtSlipNo.Text = AcName
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsPredictiveChkMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPredictiveChkSheet_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Predictive Maintenance Check Sheet"

        SqlStr = "Select * From MAN_PREDICTIVE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPredictiveChkMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From MAN_PREDICTIVE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPredictiveChkDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From MAN_PRED_ITEM_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPredictiveItemDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_PRED AS SLIP_NUMBER,TO_CHAR(ENTRY_DATE,'DD/MM/YYYY') AS ENTRY_DATE, " & vbCrLf & " MACHINE_NO,AUTO_KEY_STD,TEAM_MEMBERS,SIGN_EMP_CODE,  " & vbCrLf & " (CASE WHEN ACTION_TAKEN=0 THEN 'OK' WHEN ACTION_TAKEN=1 THEN 'Not OK/Action Needed'  " & vbCrLf & " WHEN ACTION_TAKEN=2 THEN 'Not Applicable' ELSE '' END) AS  ACTION_TAKEN " & vbCrLf & " FROM MAN_PREDICTIVE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PRED,LENGTH(AUTO_KEY_PRED)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_PRED"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPredictiveChkSheet_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPredictiveChkSheet_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(11370)

        cboAction.Items.Insert(0, "OK")
        cboAction.Items.Insert(1, "Not OK/Action Needed")
        cboAction.Items.Insert(2, "Not Applicable")
        cboAction.Items.Insert(3, "")

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
        txtSlipNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtMachineNo.Text = ""
        lblMachineNo.Text = ""
        txtInspectionStd.Text = ""
        cboAction.SelectedIndex = 3
        txtTeamMembers.Text = ""
        txtSignCode.Text = ""
        lblSignCode.Text = ""
        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True
        txtFromDept.Text = ""
        lblFromDept.Text = ""
        txtCost.Text = ""
        txtCost.Enabled = True
        cmdSearchCC.Enabled = True

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ClearGrid(SprdMainItem, ConRowHeight)
        FormatSprdMainItem(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsPredictiveChkMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String


        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColParameter
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPredictiveChkDetail.Fields("PARAMETER").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 45)

            .Col = ColObservation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPredictiveChkDetail.Fields("OBSERVATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 45)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColParameter, ColParameter)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 5)
            .set_ColWidth(6, 500 * 4)
            .set_ColWidth(7, 500 * 5)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsPredictiveChkMain.Fields("AUTO_KEY_PRED").Precision
        txtDate.Maxlength = RsPredictiveChkMain.Fields("ENTRY_DATE").DefinedSize - 6
        txtMachineNo.Maxlength = RsPredictiveChkMain.Fields("MACHINE_NO").DefinedSize
        txtInspectionStd.Maxlength = RsPredictiveChkMain.Fields("AUTO_KEY_STD").Precision
        txtTeamMembers.Maxlength = RsPredictiveChkMain.Fields("TEAM_MEMBERS").DefinedSize
        txtSignCode.MaxLength = RsPredictiveChkMain.Fields("SIGN_EMP_CODE").DefinedSize
        txtCost.MaxLength = RsPredictiveChkMain.Fields("COST_CENTER_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mDivisionCode As Double
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPredictiveChkMain.EOF = True Then Exit Function

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If Trim(txtFromDept.Text) = "" Then
            MsgInformation("Dept. is empty, So unable to save.")
            txtFromDept.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCost.Text) = "" Then
            MsgBox("Cost Center Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtCost.Enabled Then txtCost.Focus()
            Exit Function
        Else
            SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
                & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
                & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote(txtCost.Text) & "'" & vbCrLf _
                & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtFromDept.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtFromDept.Text))
                FieldsVarification = False
                If txtCost.Enabled Then txtCost.Focus()
                Exit Function
            End If
        End If


        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        Else
            MsgInformation("Please select Division, So unable to save.")
            If cboDivision.Enabled = True Then cboDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtMachineNo.Text) = "" Then
            MsgInformation("Machine No. is empty, So unable to save.")
            txtMachineNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtInspectionStd.Text) = "" Then
            MsgInformation("Inspection Std. is empty, So unable to save.")
            txtInspectionStd.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSignCode.Text) = "" Then
            MsgInformation("Singatory Code is empty, So unable to save.")
            txtSignCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColParameter, "S", "Please Check Parameter.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColObservation, "S", "Please Check Observation.") = False Then FieldsVarification = False


        If SprdMainItem.Row > 1 Then
            If MainClass.ValidDataInGrid(SprdMainItem, ColItemCode, "S", "Please Check Item Consumed Detail.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMainItem, ColQty, "N", "Please Check Item Consumed Detail.") = False Then FieldsVarification = False : Exit Function


            If CheckStockQty(SprdMainItem, ColStockQty, ColQty, ColItemCode, -1, True) = False Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmPredictiveChkSheet_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsPredictiveChkMain.Close()
        RsPredictiveChkMain = Nothing
        RsPredictiveChkDetail.Close()
        RsPredictiveChkDetail = Nothing

        RsPredictiveItemDetail.Close()
        RsPredictiveItemDetail = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColParameter)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xParameter As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColParameter
        xParameter = Trim(SprdMain.Text)
        If xParameter = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColParameter
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColParameter
                xParameter = Trim(SprdMain.Text)
                If xParameter = "" Then Exit Sub
                MainClass.AddBlankSprdRow(SprdMain, ColParameter, ConRowHeight)
                FormatSprdMain((SprdMain.MaxRows))
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtSlipNo.Text = SprdView.Text
        txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
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
    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.DoubleClick
        Call cmdSearchMacNo_Click(cmdSearchMacNo, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMacNo_Click(cmdSearchMacNo, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValMacERR
        Dim SqlStr As String
        Dim mRsQualityInspec As ADODB.Recordset

        ''AND QAL_INSPECTION_STD_DET.DETAIL_TYPE NOT IN ('A','E') 

        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT QAL_INSPECTION_STD_HDR.AUTO_KEY_STD, " & vbCrLf _
                    & " QAL_INSPECTION_STD_DET.PARAM_DESC " & vbCrLf _
                    & " FROM QAL_INSPECTION_STD_HDR ,QAL_INSPECTION_STD_DET,MAN_MACHINE_MST  " & vbCrLf _
                    & " WHERE QAL_INSPECTION_STD_HDR.AUTO_KEY_STD=QAL_INSPECTION_STD_DET.AUTO_KEY_STD " & vbCrLf _
                    & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE= MAN_MACHINE_MST.MACHINE_ITEM_CODE " & vbCrLf _
                    & " AND QAL_INSPECTION_STD_HDR.INSP_TYPE = 'C' " & vbCrLf _
                    & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MAN_MACHINE_MST.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MAN_MACHINE_MST.MACHINE_NO = '" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf _
                    & " ORDER BY QAL_INSPECTION_STD_DET.SERIAL_NO "

        'SqlStr = " SELECT QAL_INSPECTION_STD_HDR.AUTO_KEY_STD, " & vbCrLf _
        '            & " QAL_INSPECTION_STD_DET.PARAM_DESC " & vbCrLf _
        '            & " FROM QAL_INSPECTION_STD_HDR ,QAL_INSPECTION_STD_DET  " & vbCrLf _
        '            & " WHERE QAL_INSPECTION_STD_HDR.AUTO_KEY_STD=QAL_INSPECTION_STD_DET.AUTO_KEY_STD " & vbCrLf _
        '            & " AND QAL_INSPECTION_STD_HDR.INSP_TYPE = 'C' " & vbCrLf _
        '            & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '            & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE = '" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf _
        '            & " ORDER BY QAL_INSPECTION_STD_DET.SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsQualityInspec, ADODB.LockTypeEnum.adLockReadOnly)
        If Not mRsQualityInspec.EOF Then
            txtInspectionStd.Text = IIf(IsDbNull(mRsQualityInspec.Fields("AUTO_KEY_STD").Value), "", mRsQualityInspec.Fields("AUTO_KEY_STD").Value)
            Call FillQualityInspecDetail(mRsQualityInspec)
        Else
            txtInspectionStd.Text = ""
            MsgBox("This Machine No. not found in Quality Inspection Standard.")
            Cancel = True
        End If
        GoTo EventExitSub
ValMacERR:
        MsgBox(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillQualityInspecDetail(ByRef pRsQualityInspec As ADODB.Recordset)
        Dim I As Integer
        With pRsQualityInspec
            If .EOF = True Then Exit Sub
            MainClass.ClearGrid(SprdView)
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
    End Sub
    Private Sub txtSignCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSignCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSignCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSignCode.DoubleClick
        Call cmdSearchSignCode_Click(cmdSearchSignCode, New System.EventArgs())
    End Sub

    Private Sub txtSignCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSignCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSignCode_Click(cmdSearchSignCode, New System.EventArgs())
    End Sub

    Private Sub txtSignCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSignCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtSignCode, lblSignCode) = False Then Cancel = True
        eventArgs.Cancel = Cancel
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
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mDivisionCode As Long
        Dim mDivisionDesc As String

        If Not RsPredictiveChkMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsPredictiveChkMain.Fields("AUTO_KEY_PRED").Value), "", RsPredictiveChkMain.Fields("AUTO_KEY_PRED").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsPredictiveChkMain.Fields("AUTO_KEY_PRED").Value), "", RsPredictiveChkMain.Fields("AUTO_KEY_PRED").Value)
            txtDate.Text = IIf(IsDbNull(RsPredictiveChkMain.Fields("ENTRY_DATE").Value), "", RsPredictiveChkMain.Fields("ENTRY_DATE").Value)
            txtMachineNo.Text = IIf(IsDbNull(RsPredictiveChkMain.Fields("MACHINE_NO").Value), "", RsPredictiveChkMain.Fields("MACHINE_NO").Value)
            txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False))
            txtInspectionStd.Text = IIf(IsDbNull(RsPredictiveChkMain.Fields("AUTO_KEY_STD").Value), "", RsPredictiveChkMain.Fields("AUTO_KEY_STD").Value)
            txtTeamMembers.Text = IIf(IsDbNull(RsPredictiveChkMain.Fields("TEAM_MEMBERS").Value), "", RsPredictiveChkMain.Fields("TEAM_MEMBERS").Value)
            txtSignCode.Text = IIf(IsDbNull(RsPredictiveChkMain.Fields("SIGN_EMP_CODE").Value), "", RsPredictiveChkMain.Fields("SIGN_EMP_CODE").Value)
            txtSignCode_Validating(txtSignCode, New System.ComponentModel.CancelEventArgs(False))
            cboAction.SelectedIndex = IIf(IsDBNull(RsPredictiveChkMain.Fields("ACTION_TAKEN").Value), -1, RsPredictiveChkMain.Fields("ACTION_TAKEN").Value)

            mDivisionCode = IIf(IsDBNull(RsPredictiveChkMain.Fields("DIV_CODE").Value), -1, RsPredictiveChkMain.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionDesc = Trim(MasterNo)
                cboDivision.Text = mDivisionDesc
            End If
            cboDivision.Enabled = False

            txtFromDept.Text = IIf(IsDBNull(RsPredictiveChkMain.Fields("DEPT_CODE").Value), "", RsPredictiveChkMain.Fields("DEPT_CODE").Value)
            TxtFromDept_Validating(txtFromDept, New System.ComponentModel.CancelEventArgs(False))

            txtCost.Text = IIf(IsDBNull(RsPredictiveChkMain.Fields("COST_CENTER_CODE").Value), "", RsPredictiveChkMain.Fields("COST_CENTER_CODE").Value)


            Call ShowDetail1()
            Call ShowDetailItem1(mDivisionCode)
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsPredictiveChkMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM MAN_PREDICTIVE_DET " & vbCrLf & " WHERE AUTO_KEY_PRED=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPredictiveChkDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPredictiveChkDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("Parameter").Value), "", .Fields("Parameter").Value))

                SprdMain.Col = ColObservation
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERVATION").Value), "", .Fields("OBSERVATION").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSlipNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.DoubleClick
        Call cmdSearchSlipNo_Click(cmdSearchSlipNo, New System.EventArgs())
    End Sub

    Private Sub txtSlipNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSlipNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSlipNo_Click(cmdSearchSlipNo, New System.EventArgs())
    End Sub
    Private Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsPredictiveChkMain.BOF = False Then xMKey = RsPredictiveChkMain.Fields("AUTO_KEY_PRED").Value

        SqlStr = "SELECT * FROM MAN_PREDICTIVE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PRED,LENGTH(AUTO_KEY_PRED)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PRED=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPredictiveChkMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPredictiveChkMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_PREDICTIVE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PRED,LENGTH(AUTO_KEY_PRED)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PRED=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPredictiveChkMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtMachineNo.Enabled = mMode
        cmdSearchMacNo.Enabled = mMode
        txtInspectionStd.Enabled = False
        cboAction.Enabled = mMode
        txtSignCode.Enabled = mMode
        cmdSearchSignCode.Enabled = mMode
        txtFromDept.Enabled = mMode
        cmdSearchFromDept.Enabled = mMode
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
    Private Sub ReportOnPredictiveSheet(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPredictiveSheet(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPredictiveSheet(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub ShowDetailItem1(ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemName As String
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM MAN_PRED_ITEM_DET " & vbCrLf _
            & " WHERE AUTO_KEY_SLIP=" & Val(lblMkey.Text) & "" & vbCrLf _
            & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPredictiveItemDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPredictiveItemDetail
            If .EOF = True Then Exit Sub
            FormatSprdMainItem(-1)
            i = 1
            Do While Not .EOF
                SprdMainItem.Row = i

                SprdMainItem.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMainItem.Text = mItemCode

                SprdMainItem.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemName = MasterNo
                SprdMainItem.Text = mItemName

                SprdMainItem.Col = ColStockQty
                SprdMainItem.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)), "MNT", "ST", "", ConSH, mDivisionCode, ConStockRefType_PDM, CDbl(txtSlipNo.Text))) '''+ GetSavedQty(pItemCode)

                SprdMainItem.Col = ColUom
                SprdMainItem.Text = Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMainItem.Col = ColQty
                SprdMainItem.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value))))

                SprdMainItem.Col = ColRate
                SprdMainItem.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value))))

                SprdMainItem.Col = ColAmount
                SprdMainItem.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("ITEM_AMOUNT").Value), "", .Fields("ITEM_AMOUNT").Value))))

                SprdMainItem.Col = ColSavedItemCode
                SprdMainItem.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMainItem.Col = ColSavedQty
                SprdMainItem.Text = CStr(Val(Trim(IIf(IsDBNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value))))

                .MoveNext()
                i = i + 1
                SprdMainItem.MaxRows = i
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function UpdateItemDetail(ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim i As Integer
        Dim mItemCode As String
        Dim mUOM As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mCompDate As String

        If DeleteStockTRN(PubDBCn, ConStockRefType_PDM, (lblMkey.Text)) = False Then GoTo UpdateDetailERR
        PubDBCn.Execute("DELETE FROM MAN_PRED_ITEM_DET WHERE AUTO_KEY_SLIP=" & Val(lblMkey.Text) & "")

        With SprdMainItem
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

                SqlStr = ""

                If mQty > 0 Then
                    SqlStr = " INSERT INTO  MAN_PRED_ITEM_DET ( " & vbCrLf _
                        & " COMPANY_CODE, AUTO_KEY_SLIP, SERIAL_NO, " & vbCrLf _
                        & " ITEM_CODE, ITEM_UOM, STOCK_TYPE, ITEM_QTY, ITEM_RATE, ITEM_AMOUNT ) " & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & i & "," & vbCrLf _
                        & " '" & mItemCode & "','" & mUOM & "', " & vbCrLf _
                        & " 'ST'," & mQty & "," & mRate & "," & mAmount & ") "

                    PubDBCn.Execute(SqlStr)

                    If UpdateStockTRN(PubDBCn, ConStockRefType_PDM, CStr(Val(lblMkey.Text)), i, (txtDate.Text), (txtDate.Text), "ST", mItemCode, mUOM, CStr(-1), mQty, 0, "O", 0, 0, "", "", (txtFromDept.Text), "MNT", "", "N", " From : Sub Store To : " & txtFromDept.Text & "-" & ConStockRefType_PDM, "-1", ConSH, mDivisionCode, "", "") = False Then GoTo UpdateDetailERR
                End If
            Next
        End With
        UpdateItemDetail = True
        Exit Function
UpdateDetailERR:
        UpdateItemDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub FormatSprdMainItem(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdMainItem
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPredictiveItemDetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsPredictiveItemDetail.Fields("ITEM_UOM").DefinedSize
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColSavedItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPredictiveItemDetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .ColHidden = True

            .Col = ColSavedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True

            MainClass.ProtectCell(SprdMainItem, 1, SprdMainItem.MaxRows, ColItemName, ColUom)
            MainClass.ProtectCell(SprdMainItem, 1, SprdMainItem.MaxRows, ColRate, ColAmount)
            MainClass.SetSpreadColor(SprdMainItem, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub


    Private Sub SprdMainItem_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMainItem.ClickEvent

        Dim SqlStr As String

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMainItem
                .Row = .ActiveRow
                .Col = ColItemCode
                '            If RsCompany.fields("COMPANY_CODE").value = 12 Then
                SqlStr = GetStockItemQry(.Text, "Y", VB6.Format(txtDate.Text, "DD/MM/YYYY"), ConSH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "1") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                End If
                '            Else
                '                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                '                    .Row = .ActiveRow
                '
                '                    .Col = ColItemCode
                '                    .Text = Trim(AcName)
                '
                '                    .Col = ColItemName
                '                    .Text = Trim(AcName1)
                '                End If
                '            End If
                Call SprdMainItem_LeaveCell(SprdMainItem, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName Then
            With SprdMainItem
                .Row = .ActiveRow
                .Col = ColItemName
                '            If RsCompany.fields("COMPANY_CODE").value = 12 Then
                SqlStr = GetStockItemQry(.Text, "N", VB6.Format(txtDate.Text, "DD/MM/YYYY"), ConSH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "2") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemName
                    .Text = Trim(AcName)
                End If
                '            Else
                '                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                '                    .Row = .ActiveRow
                '
                '                    .Col = ColItemCode
                '                    .Text = Trim(AcName1)
                '
                '                    .Col = ColItemName
                '                    .Text = Trim(AcName)
                '                End If
                '            End If
                Call SprdMainItem_LeaveCell(SprdMainItem, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMainItem, eventArgs.row, ColItemCode)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMainItem_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMainItem.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMainItem.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMainItem_ClickEvent(SprdMainItem, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMainItem_ClickEvent(SprdMainItem, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        SprdMainItem.Refresh()
    End Sub

    Private Sub SprdMainItem_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainItem.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String
        Dim mDivisionCode As Double

        If eventArgs.newRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SprdMainItem.Row = SprdMainItem.ActiveRow
        SprdMainItem.Col = ColItemCode
        xICode = Trim(SprdMainItem.Text)
        If xICode = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColItemCode
                SprdMainItem.Row = SprdMainItem.ActiveRow

                SprdMainItem.Col = ColItemCode
                xICode = Trim(SprdMainItem.Text)
                If xICode = "" Then Exit Sub
                If CheckDuplicateItem(xICode) = False Then
                    If FillGridRow(xICode, mDivisionCode) = False Then Exit Sub
                    Call CalcAmount()
                    MainClass.AddBlankSprdRow(SprdMainItem, ColItemCode, ConRowHeight)
                    FormatSprdMainItem((SprdMainItem.MaxRows))
                End If
            Case ColQty
                If CheckQty() = True Then
                    Call CalcAmount()
                    MainClass.AddBlankSprdRow(SprdMainItem, ColItemCode, ConRowHeight)
                    FormatSprdMainItem((SprdMainItem.MaxRows))
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CalcAmount()
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double

        With SprdMainItem
            .Row = .ActiveRow

            .Col = ColQty
            mQty = Val(.Text)

            .Col = ColRate
            mRate = Val(.Text)

            .Col = ColAmount
            .Text = CStr(mQty * mRate)
        End With

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        With SprdMainItem
            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMainItem, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillGridRow(ByRef pItemCode As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mUnit As String
        Dim mStockQty As Double

        If pItemCode = "" Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Please Select Slip Date.")
            If txtDate.Enabled = True Then txtDate.Focus()
            FillGridRow = True
            Exit Function
        End If

        If Trim(txtFromDept.Text) = "" Then
            MsgInformation("Please Select Dept Code.")
            If txtFromDept.Enabled = True Then txtFromDept.Focus()
            FillGridRow = True
            Exit Function
        End If

        SqlStr = ""
        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf _
                    & " FROM INV_ITEM_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMainItem.Row = SprdMainItem.ActiveRow
            With RsMisc

                SprdMainItem.Col = ColItemCode
                SprdMainItem.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                mItemCode = Trim(SprdMainItem.Text)

                SprdMainItem.Col = ColItemName
                SprdMainItem.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMainItem.Col = ColUom
                SprdMainItem.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                mUnit = Trim(SprdMainItem.Text)

                mStockQty = GetBalanceStockQty(pItemCode, (txtDate.Text), mUnit, "MNT", "ST", "", ConSH, mDivisionCode, ConStockRefType_PDM, Val(txtSlipNo.Text)) '''+ GetSavedQty(pItemCode)
                SprdMainItem.Col = ColStockQty
                SprdMainItem.Text = CStr(mStockQty)

                SprdMainItem.Col = ColRate
                SprdMainItem.Text = CStr(GetLatestItemCostFromMRR(mItemCode, mUnit, 1, VB6.Format(IIf((txtDate.Text = "" Or txtDate.Text = "/  /"), RunDate, txtDate.Text), "DD/MM/YYYY"), "L"))
            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMainItem, SprdMainItem.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        '    Resume
        FillGridRow = False
        MsgBox(Err.Description)
    End Function

    Private Function GetSavedQty(ByRef pItemCode As String) As Double
        On Error GoTo GetERR
        Dim mSavedItemCode As String
        Dim mSavedQty As Double

        With SprdMainItem
            .Row = .ActiveRow

            .Col = ColSavedItemCode
            mSavedItemCode = .Text

            .Col = ColSavedQty
            mSavedQty = Val(.Text)

            If UCase(Trim(pItemCode)) = UCase(Trim(mSavedItemCode)) Then
                GetSavedQty = mSavedQty
            Else
                GetSavedQty = 0
            End If
        End With
        Exit Function
GetERR:
        GetSavedQty = 0
        MsgBox(Err.Description)
    End Function

    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If mItemCode = "" Then CheckDuplicateItem = False : Exit Function
        With SprdMainItem
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColItemCode
                If UCase(Trim(.Text)) = UCase(Trim(mItemCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMainItem, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMainItem_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMainItem.Leave
        'With SprdMainItem
        '    SprdMainItem_LeaveCell(SprdMainItem, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        Dim cntRow As Double
        Dim mItemCode As String
        Dim mUnit As String
        Dim mDivisionCode As Double

        With SprdMainItem
            For cntRow = 1 To .MaxRows
                .Col = ColItemCode
                mItemCode = Trim(SprdMainItem.Text)

                .Col = ColUom
                mUnit = Trim(.Text)

                .Col = ColStockQty
                .Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mUnit, "MNT", "ST", "", ConSH, mDivisionCode, ConStockRefType_PDM, Val(txtSlipNo.Text))) '''+ GetSavedQty(pItemCode)
            Next
        End With


        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdSearchFromDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFromDept.Click
        Call SearchDept(txtFromDept, lblFromDept)
    End Sub

    Private Sub SearchDept(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.Text = AcName
            If pTextBax.Enabled = True Then pTextBax.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub TxtFromDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDept.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtFromDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromDept.DoubleClick
        Call cmdSearchFromDept_Click(cmdSearchFromDept, New System.EventArgs())
    End Sub

    Private Sub TxtFromDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFromDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFromDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtFromDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFromDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchFromDept_Click(cmdSearchFromDept, New System.EventArgs())
    End Sub

    Private Sub TxtFromDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateDept(txtFromDept, lblFromDept) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub
    Private Function ValidateDept(ByRef pTextBox As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label) As Boolean
        On Error GoTo ValERR
        Dim SqlStr As String
        ValidateDept = True
        If Trim(pTextBox.Text) = "" Then Exit Function
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Deparment Does Not Exist In Master.")
            ValidateDept = False
        Else
            pLable.Text = MasterNo
        End If
        Exit Function
ValERR:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        Call txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtFromDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtFromDept.Focus()
            Exit Sub
        End If
        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
            & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtFromDept.Text)) & "'"

        '    If MainClass.SearchGridMaster(txtCost.Text, "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2((txtCost.Text), SqlStr) = True Then
            txtCost.Text = AcName
            txtCost_Validating(txtCost, New System.ComponentModel.CancelEventArgs(False))
            If txtCost.Enabled = True Then txtCost.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCost.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCost_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCost.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub txtCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Trim(txtCost.Text) = "" Then GoTo EventExitSub
        txtCost.Text = VB6.Format(txtCost.Text, "000")
        If Trim(txtFromDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtFromDept.Focus()
            GoTo EventExitSub
        End If

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf _
            & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf _
            & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtFromDept.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            'lblCostctr.Text = IIf(IsDBNull(RsTemp.Fields("CC_DESC").Value), "", RsTemp.Fields("CC_DESC").Value)
        Else
            MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtFromDept.Text))
            Cancel = True
        End If

        '    If MainClass.ValidateWithMasterTable(txtCost.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '        lblCostctr.text = MasterNo
        '    Else
        '        MsgInformation "Invalid CostC Code"
        '        Cancel = True
        '    End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

End Class
