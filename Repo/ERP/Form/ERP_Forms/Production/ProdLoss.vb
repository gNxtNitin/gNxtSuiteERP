Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProdLoss
    Inherits System.Windows.Forms.Form
    Dim RsProdLoss As ADODB.Recordset

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Sub cboReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReason.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
            If RsProdLoss.EOF = False Then RsProdLoss.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsProdLoss.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_PROD_LOSS_TRN", (txtNumber.Text), RsProdLoss) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_PROD_LOSS_TRN", "AUTO_KEY_NO", (txtNumber.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_PROD_LOSS_TRN WHERE AUTO_KEY_NO=" & Val(lblMKey.Text) & "")
                PubDBCn.CommitTrans()
                RsProdLoss.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsProdLoss.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProdLoss, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        '    If IsRecordExist = True Then Exit Sub
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

    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_NO " & vbCrLf _
            & " From PRD_PROD_LOSS_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
            & " AND REF_DATE =TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND EMP_CODE ='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_NO").Value)
                IsRecordExist = True
            End If
        End With
        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
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
            lblMKey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO PRD_PROD_LOSS_TRN " & vbCrLf _
                & " (AUTO_KEY_NO, COMPANY_CODE," & vbCrLf _
                & " REF_DATE, EMP_CODE, TIME_FROM, TIME_TO, TOTAL_TIME," & vbCrLf _
                & " REASON, REMARKS, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE " & vbCrLf _
                & " ) VALUES ( " & vbCrLf _
                & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                & " TO_DATE('" & IIf(txtTimeFrom.Text = "__:__", "", txtTimeFrom.Text) & "', 'HH24:MI')," & vbCrLf _
                & " TO_DATE('" & IIf(txtTimeTo.Text = "__:__", "", txtTimeTo.Text) & "', 'HH24:MI')," & vbCrLf _
                & " " & Val(txtTotalTime.Text) & ",'" & VB.Left(cboReason.Text, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE PRD_PROD_LOSS_TRN SET " & vbCrLf _
                & " AUTO_KEY_NO=" & mSlipNo & "," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                & " TIME_FROM=TO_DATE('" & IIf(txtTimeFrom.Text = "__:__", "", txtTimeFrom.Text) & "', 'HH24:MI')," & vbCrLf _
                & " TIME_TO=TO_DATE('" & IIf(txtTimeTo.Text = "__:__", "", txtTimeTo.Text) & "', 'HH24:MI')," & vbCrLf _
                & " TOTAL_TIME=" & Val(txtTotalTime.Text) & "," & vbCrLf _
                & " REASON='" & VB.Left(cboReason.Text, 1) & "'," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE AUTO_KEY_NO =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsProdLoss.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = " SELECT Max(AUTO_KEY_NO) " & vbCrLf & " FROM PRD_PROD_LOSS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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

    Private Sub SearchEMP(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_TYPE='W'"

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", "EMP_FNAME", , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.Text = AcName
        End If
        txtEmpCode.Focus()
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchEmpCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmpCode.Click
        Call SearchEMP(txtEmpCode, lblEmpCode)
    End Sub

    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "PRD_PROD_LOSS_TRN", "AUTO_KEY_NO", "REF_DATE", "EMP_CODE", , SqlStr) = True Then
            txtNumber.Text = AcName
            'Call txtNumber_Validate(False)
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
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
        MainClass.ButtonStatus(Me, XRIGHT, RsProdLoss, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmProdLoss_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Worker's Production Loss Entry"

        SqlStr = "Select * From PRD_PROD_LOSS_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdLoss, ADODB.LockTypeEnum.adLockReadOnly)

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
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_NO AS REF_NUM,TO_CHAR(REF_DATE,'DD/MM/YYYY') AS REFDATE, " & vbCrLf & " EMP_CODE, TO_CHAR(TIME_FROM,'HH24:MI') AS TIME_FROM, TO_CHAR(TIME_TO,'HH24:MI') AS TIME_TO, " & vbCrLf & " TO_CHAR(TOTAL_TIME) AS TOTAL_TIME, " & vbCrLf & " DECODE(REASON,'1','M/C B/D','2','DIE B/D','3','DIE CHANGE','4','OTHERS') AS REASON, REMARKS " & vbCrLf & " FROM PRD_PROD_LOSS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_NO"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmProdLoss_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProdLoss_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(4140)
        Me.Width = VB6.TwipsToPixelsX(9285)

        cboReason.Items.Clear()
        cboReason.Items.Add("1.M/C Breakdown")
        cboReason.Items.Add("2.Die Breakdown")
        cboReason.Items.Add("3.Die Change")
        cboReason.Items.Add("4.Others")
        cboReason.Items.Add("5.Material Shortage")

        cboReason.SelectedIndex = 0

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

        lblMKey.Text = ""
        txtNumber.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtEmpCode.Text = ""
        lblEmpCode.Text = ""
        txtTimeFrom.Text = "__:__"
        txtTimeTo.Text = "__:__"
        txtTotalTime.Text = ""
        cboReason.SelectedIndex = 0
        txtRemarks.Text = ""
        If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            txtTimeFrom.Enabled = False
            txtTimeTo.Enabled = False
            txtTotalTime.Enabled = True
        Else
            txtTimeFrom.Enabled = True
            txtTimeTo.Enabled = True
            txtTotalTime.Enabled = False
        End If
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsProdLoss, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsProdLoss.Fields("AUTO_KEY_NO").Precision
        txtDate.Maxlength = RsProdLoss.Fields("REF_DATE").DefinedSize - 6
        txtEmpCode.Maxlength = RsProdLoss.Fields("EMP_CODE").DefinedSize
        txtTimeFrom.MaxLength = RsProdLoss.Fields("TIME_FROM").DefinedSize - 11
        txtTimeTo.MaxLength = RsProdLoss.Fields("TIME_TO").DefinedSize - 11
        txtTotalTime.Maxlength = RsProdLoss.Fields("TOTAL_TIME").Precision
        txtRemarks.Maxlength = RsProdLoss.Fields("REMARKS").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mHour As Integer
        Dim mMIN As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsProdLoss.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Emp Code is empty, So unable to save.")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(Replace(txtTimeFrom.Text, ":", ".")) = 0 Then
            MsgInformation("Time From is empty, So unable to save.")
            txtTimeFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(Replace(txtTimeTo.Text, ":", ".")) = 0 Then
            MsgInformation("Time To is empty, So unable to save.")
            txtTimeTo.Focus()
            FieldsVarification = False
            Exit Function
        End If



        If Val(txtTotalTime.Text) = 0 Then
            MsgInformation("Total Time To is empty, So unable to save.")
            FieldsVarification = False
            Exit Function
        End If

        mHour = Int(CDbl(txtTotalTime.Text))
        mMIN = (Val(txtTotalTime.Text) - mHour) * 100

        If Val(CStr(mHour)) > 23 Then
            MsgInformation("Please select hour less than or equal to 24, So unable to save.")
            FieldsVarification = False
            Exit Function
        End If

        If mMIN > 59 Then
            MsgInformation("Invalid Time Please enter Time in HH:MM Format, So unable to save.")
            FieldsVarification = False
            Exit Function
        End If

        SqlStr = "SELECT AUTO_KEY_NO FROM  PRD_PROD_LOSS_TRN WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "' AND REF_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If Val(txtNumber.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_NO<>" & Val(txtNumber.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If MsgQuestion("Employee GD is already Exists for such date ( Ref No : " & RsTemp.Fields("AUTO_KEY_NO").Value & ") , want to continue.. ? ") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        End If



        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmProdLoss_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsProdLoss.Close()
        RsProdLoss = Nothing
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
        Dim SqlStr As String = ""
        ValidateEMP = True
        If Trim(pTextBox.Text) = "" Then Exit Function
        pTextBox.Text = VB6.Format(pTextBox.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_TYPE='W' AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
            Exit Function
        Else
            pLable.text = MasterNo
        End If

        If FillEmpINTimeOut(Trim(txtEmpCode.Text), VB6.Format(txtDate.Text, "DD/MM/YYYY")) = False Then GoTo ValEMP

        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function

    Private Function FillEmpINTimeOut(ByRef mCode As String, ByRef mDate As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        FillEmpINTimeOut = False

        If Trim(mCode) = "" Or Trim(mDate) = "" Then Exit Function


        SqlStr = "SELECT * FROM PAY_DALIY_ATTN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtINTime.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "hh:mm")
            txtOUTTime.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "hh:mm")
        End If


        FillEmpINTimeOut = True

        Exit Function


ERR1:
        FillEmpINTimeOut = False
        MsgInformation(Err.Description)
    End Function

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        End If
        If FillEmpINTimeOut(Trim(txtEmpCode.Text), VB6.Format(txtDate.Text, "DD/MM/YYYY")) = False Then GoTo ErrPart
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsProdLoss.EOF Then
            lblMKey.Text = IIf(IsDbNull(RsProdLoss.Fields("AUTO_KEY_NO").Value), "", RsProdLoss.Fields("AUTO_KEY_NO").Value)
            txtNumber.Text = IIf(IsDbNull(RsProdLoss.Fields("AUTO_KEY_NO").Value), "", RsProdLoss.Fields("AUTO_KEY_NO").Value)
            txtDate.Text = IIf(IsDbNull(RsProdLoss.Fields("REF_DATE").Value), "", RsProdLoss.Fields("REF_DATE").Value)
            txtEmpCode.Text = IIf(IsDbNull(RsProdLoss.Fields("EMP_CODE").Value), "", RsProdLoss.Fields("EMP_CODE").Value)
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            txtTimeFrom.Text = VB6.Format(IIf(IsDbNull(RsProdLoss.Fields("TIME_FROM").Value), "__:__", RsProdLoss.Fields("TIME_FROM").Value), "HH:MM")
            txtTimeTo.Text = VB6.Format(IIf(IsDbNull(RsProdLoss.Fields("TIME_TO").Value), "__:__", RsProdLoss.Fields("TIME_TO").Value), "HH:MM")
            txtTotalTime.Text = IIf(IsDbNull(RsProdLoss.Fields("TOTAL_TIME").Value), "", VB6.Format(RsProdLoss.Fields("TOTAL_TIME").Value, "0.00"))
            If RsProdLoss.Fields("REASON").Value = "1" Then
                cboReason.Text = "1.M/C Breakdown"
            ElseIf RsProdLoss.Fields("REASON").Value = "2" Then
                cboReason.Text = "2.Die Breakdown"
            ElseIf RsProdLoss.Fields("REASON").Value = "3" Then
                cboReason.Text = "3.Die Change"
            ElseIf RsProdLoss.Fields("REASON").Value = "4" Then
                cboReason.Text = "4.Others"
            ElseIf RsProdLoss.Fields("REASON").Value = "5" Then
                cboReason.Text = "5.Material Shortage"
            End If
            txtRemarks.Text = IIf(IsDbNull(RsProdLoss.Fields("REMARKS").Value), "", RsProdLoss.Fields("REMARKS").Value)
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsProdLoss, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Public Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String = ""

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub

        If Len(txtNumber.Text) < 6 Then txtNumber.Text = Trim(txtNumber.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")

        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsProdLoss.BOF = False Then xMkey = RsProdLoss.Fields("AUTO_KEY_NO").Value

        SqlStr = "SELECT * FROM PRD_PROD_LOSS_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdLoss, ADODB.LockTypeEnum.adLockReadOnly)
        If RsProdLoss.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_PROD_LOSS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_NO,LENGTH(AUTO_KEY_NO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_NO=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdLoss, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
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

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

    Private Sub txtTimeFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTimeFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTimeFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTimeFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtTimeFrom.Text) = "" Or Trim(txtTimeFrom.Text) = "__:__" Then GoTo EventExitSub
        If CheckTimeFormat(txtTimeFrom) = False Then Cancel = True : GoTo EventExitSub
        txtTimeFrom.Text = VB6.Format(txtTimeFrom.Text, "HH:MM")
        If CheckTime(txtTimeFrom) = False Then Cancel = True : GoTo EventExitSub
        Call CalcTot()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTimeTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTimeTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTimeTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTimeTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtTimeTo.Text) = "" Or Trim(txtTimeTo.Text) = "__:__" Then GoTo EventExitSub
        If CheckTimeFormat(txtTimeTo) = False Then Cancel = True : GoTo EventExitSub
        txtTimeTo.Text = VB6.Format(txtTimeTo.Text, "HH:MM")
        If CheckTime(txtTimeTo) = False Then Cancel = True : GoTo EventExitSub
        Call CalcTot()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function CheckTimeFormat(ByRef pTextTime As System.Windows.Forms.MaskedTextBox) As Boolean
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

    Private Function CheckTime(ByRef pTextTime As System.Windows.Forms.MaskedTextBox) As Boolean
        CheckTime = True
        If Val(Replace(txtTimeFrom.Text, ":", ".")) > 0 And Val(Replace(txtTimeTo.Text, ":", ".")) > 0 Then
            If Val(Replace(txtTimeFrom.Text, ":", ".")) >= Val(Replace(txtTimeTo.Text, ":", ".")) Then
                MsgBox("Time From cann't be greater than Time To")
                CheckTime = False
                Exit Function
            End If
        End If
    End Function

    Private Sub CalcTot()
        On Error GoTo ERR1
        Dim mStartTime As String
        Dim mStartDateTime As String
        Dim mEndTime As String
        Dim mEndDateTime As String
        Dim mTotHour As Double
        Dim mTotMin As Double
        Dim mTotTime As Double
        Dim mTotalBDTime As Double
        Dim mHour As Integer
        Dim mMIN As Integer

        If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            mTotalBDTime = Val(txtTotalTime.Text)
            mHour = Int(CDbl(txtTotalTime.Text))
            mMIN = (mTotalBDTime - mHour) * 100

            mStartDateTime = Trim(txtDate.Text) & " " & "09:00"
            mEndDateTime = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, mHour, CDate(mStartDateTime)))
            mEndDateTime = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, mMIN, CDate(mEndDateTime)))

            txtTimeFrom.Text = VB6.Format(mStartDateTime, "HH:MM")
            txtTimeTo.Text = VB6.Format(mEndDateTime, "HH:MM")
        Else
            mStartTime = Trim(txtTimeFrom.Text)
            mEndTime = Trim(txtTimeTo.Text)

            If mStartTime = "" Or mStartTime = "__:__" Then Exit Sub
            If mEndTime = "" Or mEndTime = "__:__" Then Exit Sub

            mStartDateTime = Trim(txtDate.Text) & " " & mStartTime
            mEndDateTime = Trim(txtDate.Text) & " " & mEndTime
        End If

        mTotHour = Int(DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mStartDateTime), CDate(mEndDateTime)) / 60)
        mTotMin = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mStartDateTime), CDate(mEndDateTime)) Mod 60

        mTotMin = CDbl(VB6.Format(mTotMin / 100, "0.00"))
        mTotTime = mTotHour + mTotMin

        txtTotalTime.Text = VB6.Format(mTotTime, "0.00")

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtTotalTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalTime.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotalTime_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalTime.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotalTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotalTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mHour As Integer
        Dim mMIN As Integer

        If Val(txtTotalTime.Text) <= 0 Then
            txtTimeFrom.Text = "__:__"
            txtTimeTo.Text = "__:__"
            txtTotalTime.Text = ""
            GoTo EventExitSub
        End If

        mHour = Int(CDbl(txtTotalTime.Text))
        mMIN = (Val(txtTotalTime.Text) - mHour) * 100

        If PubSuperUser = "Y" Then
            If Val(CStr(mHour)) > 23 Then
                MsgInformation("Please select hour less than or equal to 24.")
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            If Val(CStr(mHour)) > 12 Then
                MsgInformation("Please select hour less than or equal to 12. Contact your System administrator.")
                Cancel = True
                GoTo EventExitSub
            End If
        End If

        If mMIN > 59 Then
            MsgInformation("Invalid Time Please enter Time in HH:MM Format.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtTotalTime.Text = VB6.Format(txtTotalTime.Text, "0.00")
        Call CalcTot()
        GoTo EventExitSub
ErrPart:

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
