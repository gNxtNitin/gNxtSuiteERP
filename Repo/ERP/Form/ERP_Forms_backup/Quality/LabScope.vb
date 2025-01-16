Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLabScope
    Inherits System.Windows.Forms.Form
    Dim RsLabScopeMain As ADODB.Recordset
    Dim RsLabScopeDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColCatOfTest As Short = 1
    Private Const ColInstrumentCode As Short = 2
    Private Const ColInstrumentDesc As Short = 3
    Private Const ColTemp As Short = 4
    Private Const ColHumidity As Short = 5
    Private Const ColMethodOfTest As Short = 6
    Private Const ColRR As Short = 7
    Private Const ColCalibration As Short = 8


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
            If RsLabScopeMain.EOF = False Then RsLabScopeMain.MoveFirst()
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
        If Not RsLabScopeMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_LABSCOPE_HDR", (txtSlipNo.Text), RsLabScopeMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_LABSCOPE_DET WHERE AUTO_KEY_LAB=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_LABSCOPE_HDR WHERE AUTO_KEY_LAB=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsLabScopeMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsLabScopeMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsLabScopeMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_LABSCOPE_HDR " & vbCrLf _
                            & " (AUTO_KEY_LAB,COMPANY_CODE," & vbCrLf _
                            & " ENTRY_DATE,LAB_DEPT_CODE,PRE_EMP_CODE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDepartment.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_LABSCOPE_HDR SET " & vbCrLf _
                    & " AUTO_KEY_LAB=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " ENTRY_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " LAB_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDepartment.Text) & "', " & vbCrLf _
                    & " PRE_EMP_CODE='" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_LAB =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsLabScopeMain.Requery()
        RsLabScopeDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_LAB)  " & vbCrLf & " FROM QAL_LABSCOPE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAB,LENGTH(AUTO_KEY_LAB)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mCatOfTest As String
        Dim mInstrumentCode As String
        Dim mInstrumentDesc As String
        Dim mTemp As String
        Dim mHumidity As String
        Dim mMethodOfTest As String
        Dim mRR As String
        Dim mCalibration As String

        PubDBCn.Execute("DELETE FROM QAL_LABSCOPE_DET WHERE AUTO_KEY_LAB=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColCatOfTest
                mCatOfTest = MainClass.AllowSingleQuote(.Text)

                .Col = ColInstrumentCode
                mInstrumentCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColInstrumentDesc
                mInstrumentDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColTemp
                mTemp = MainClass.AllowSingleQuote(.Text)

                .Col = ColHumidity
                mHumidity = MainClass.AllowSingleQuote(.Text)

                .Col = ColMethodOfTest
                mMethodOfTest = MainClass.AllowSingleQuote(.Text)

                .Col = ColRR
                mRR = MainClass.AllowSingleQuote(.Text)

                .Col = ColCalibration
                mCalibration = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mCatOfTest <> "" And mInstrumentCode <> "" Then
                    SqlStr = " INSERT INTO  QAL_LABSCOPE_DET ( " & vbCrLf & " AUTO_KEY_LAB,SERIAL_NO,LAB_TESTS,INSTRUMENT_NO, " & vbCrLf & " INSTRUMENT_DESC,ENV_TEMP,ENV_HUMIDITY,TEST_CALIB_METHOD, " & vbCrLf & " COMP_RR,COMP_CALIB) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mCatOfTest & "','" & mInstrumentCode & "', " & vbCrLf & " '" & mInstrumentDesc & "','" & mTemp & "','" & mHumidity & "','" & mMethodOfTest & "', " & vbCrLf & " '" & mRR & "','" & mCalibration & "') "
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
    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDepartment.Text = AcName1
            lblDepartment.text = AcName
            If txtDepartment.Enabled = True Then txtDepartment.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchPrepBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrepBy.Click
        Call SearchEmp(TxtPreparedBy, lblPreparedBy)
    End Sub
    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.text = AcName
            If pTextBax.Enabled = True Then pTextBax.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAB,LENGTH(AUTO_KEY_LAB)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_LABSCOPE_HDR", "AUTO_KEY_LAB", "ENTRY_DATE", "LAB_DEPT_CODE", "PRE_EMP_CODE", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsLabScopeMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmLabScope_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Laboratory Scope"

        SqlStr = "Select * From QAL_LABSCOPE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLabScopeMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_LABSCOPE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLabScopeDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_LAB AS SLIP_NUMBER,TO_CHAR(ENTRY_DATE,'DD/MM/YYYY') AS ENTRY_DATE, " & vbCrLf & " LAB_DEPT_CODE,PRE_EMP_CODE  " & vbCrLf & " FROM QAL_LABSCOPE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAB,LENGTH(AUTO_KEY_LAB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_LAB"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmLabScope_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmLabScope_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(6225)
        'Me.Width = VB6.TwipsToPixelsX(10755)
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
        txtDepartment.Text = ""
        lblDepartment.Text = ""
        TxtPreparedBy.Text = ""
        lblPreparedBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsLabScopeMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColCatOfTest
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLabScopeDetail.Fields("LAB_TESTS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColInstrumentCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLabScopeDetail.Fields("INSTRUMENT_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColInstrumentDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLabScopeDetail.Fields("INSTRUMENT_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColTemp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLabScopeDetail.Fields("ENV_TEMP").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColHumidity
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLabScopeDetail.Fields("ENV_HUMIDITY").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColMethodOfTest
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLabScopeDetail.Fields("TEST_CALIB_METHOD").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColRR
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLabScopeDetail.Fields("COMP_RR").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColCalibration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsLabScopeDetail.Fields("COMP_CALIB").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColInstrumentDesc, ColInstrumentDesc)
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
            .set_ColWidth(3, 500 * 2)
            .set_ColWidth(4, 500 * 2)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsLabScopeMain.Fields("AUTO_KEY_LAB").Precision
        txtDate.Maxlength = RsLabScopeMain.Fields("ENTRY_DATE").DefinedSize - 6
        txtDepartment.Maxlength = RsLabScopeMain.Fields("LAB_DEPT_CODE").DefinedSize
        TxtPreparedBy.Maxlength = RsLabScopeMain.Fields("PRE_EMP_CODE").DefinedSize
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
        If MODIFYMode = True And RsLabScopeMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDepartment.Text) = "" Then
            MsgInformation("Department is empty, So unable to save.")
            txtDepartment.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(TxtPreparedBy.Text) = "" Then
            MsgInformation("Prepared By is empty, So unable to save.")
            TxtPreparedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColCatOfTest, "S", "Please Check Category Of Tests.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColInstrumentCode, "S", "Please Check Instrument.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColTemp, "S", "Please Check Temperature.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColHumidity, "S", "Please Check Humidity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColMethodOfTest, "S", "Please Check Method of test.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColCalibration, "S", "Please Check Calibration.") = False Then FieldsVarification = False : Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Sub frmLabScope_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsLabScopeMain.Close()
        RsLabScopeMain = Nothing
        RsLabScopeDetail.Close()
        RsLabScopeDetail = Nothing
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

        If eventArgs.Row = 0 And eventArgs.Col = ColInstrumentCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColInstrumentCode
                If MainClass.SearchGridMaster(.Text, "QAL_INSTRUMENT_MST", "INSTRUMENT_NO", "COMP_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColInstrumentCode
                    .Text = Trim(AcName)

                    .Col = ColInstrumentDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColInstrumentCode, .ActiveRow, ColInstrumentCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColInstrumentDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColInstrumentDesc
                If MainClass.SearchGridMaster("", "QAL_INSTRUMENT_MST", "COMP_DESC", "INSTRUMENT_NO", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColInstrumentCode
                    .Text = Trim(AcName1)

                    .Col = ColInstrumentDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColInstrumentCode, .ActiveRow, ColInstrumentCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColCatOfTest)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColInstrumentCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColInstrumentCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColInstrumentDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColInstrumentDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xCatOfTest As String
        Dim xInstrumentCode As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColCatOfTest
        xCatOfTest = Trim(SprdMain.Text)
        If xCatOfTest = "" Then Exit Sub

        SprdMain.Col = ColInstrumentCode
        xInstrumentCode = Trim(SprdMain.Text)
        If xInstrumentCode = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColInstrumentCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColInstrumentCode
                xInstrumentCode = Trim(SprdMain.Text)
                If xInstrumentCode = "" Then Exit Sub
                If FillGridRow(xInstrumentCode) = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColInstrumentCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function FillGridRow(ByRef pInstrumentCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String

        If pInstrumentCode = "" Then Exit Function
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(pInstrumentCode, "INSTRUMENT_NO", "COMP_DESC", "QAL_INSTRUMENT_MST", PubDBCn, MasterNo) = True Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColInstrumentDesc
            SprdMain.Text = IIf(IsDbNull(MasterNo), "", MasterNo)
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColInstrumentCode)
            FillGridRow = False
        End If
        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
        Resume
    End Function

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


    Private Sub txtDepartment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepartment.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDepartment_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepartment.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDepartment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDepartment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDepartment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDepartment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDepartment.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDepartment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDepartment.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValERR
        Dim SqlStr As String
        If Trim(txtDepartment.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDepartment.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Department Does Not Exist In Master.")
            Cancel = True
        Else
            lblDepartment.text = MasterNo
        End If
        GoTo EventExitSub
ValERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtPreparedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreparedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtPreparedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreparedBy.DoubleClick
        Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub TxtPreparedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPreparedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub TxtPreparedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPreparedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(TxtPreparedBy, lblPreparedBy) = False Then Cancel = True
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

        If Not RsLabScopeMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsLabScopeMain.Fields("AUTO_KEY_LAB").Value), "", RsLabScopeMain.Fields("AUTO_KEY_LAB").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsLabScopeMain.Fields("AUTO_KEY_LAB").Value), "", RsLabScopeMain.Fields("AUTO_KEY_LAB").Value)
            txtDate.Text = IIf(IsDbNull(RsLabScopeMain.Fields("ENTRY_DATE").Value), "", RsLabScopeMain.Fields("ENTRY_DATE").Value)
            txtDepartment.Text = IIf(IsDbNull(RsLabScopeMain.Fields("LAB_DEPT_CODE").Value), "", RsLabScopeMain.Fields("LAB_DEPT_CODE").Value)
            txtDepartment_Validating(txtDepartment, New System.ComponentModel.CancelEventArgs(False))
            TxtPreparedBy.Text = IIf(IsDbNull(RsLabScopeMain.Fields("PRE_EMP_CODE").Value), "", RsLabScopeMain.Fields("PRE_EMP_CODE").Value)
            TxtPreparedBy_Validating(TxtPreparedBy, New System.ComponentModel.CancelEventArgs(False))
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsLabScopeMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_LABSCOPE_DET " & vbCrLf & " WHERE AUTO_KEY_LAB=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLabScopeDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsLabScopeDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColCatOfTest
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("LAB_TESTS").Value), "", .Fields("LAB_TESTS").Value))

                SprdMain.Col = ColInstrumentCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSTRUMENT_NO").Value), "", .Fields("INSTRUMENT_NO").Value))

                SprdMain.Col = ColInstrumentDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSTRUMENT_DESC").Value), "", .Fields("INSTRUMENT_DESC").Value))

                SprdMain.Col = ColTemp
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ENV_TEMP").Value), "", .Fields("ENV_TEMP").Value))

                SprdMain.Col = ColHumidity
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ENV_HUMIDITY").Value), "", .Fields("ENV_HUMIDITY").Value))

                SprdMain.Col = ColMethodOfTest
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("TEST_CALIB_METHOD").Value), "", .Fields("TEST_CALIB_METHOD").Value))

                SprdMain.Col = ColRR
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("COMP_RR").Value), "", .Fields("COMP_RR").Value))

                SprdMain.Col = ColCalibration
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("COMP_CALIB").Value), "", .Fields("COMP_CALIB").Value))

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

        If MODIFYMode = True And RsLabScopeMain.BOF = False Then xMKey = RsLabScopeMain.Fields("AUTO_KEY_LAB").Value

        SqlStr = "SELECT * FROM QAL_LABSCOPE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAB,LENGTH(AUTO_KEY_LAB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_LAB=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLabScopeMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsLabScopeMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_LABSCOPE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LAB,LENGTH(AUTO_KEY_LAB)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_LAB=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLabScopeMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtDepartment.Enabled = mMode
        cmdSearchDept.Enabled = mMode
        TxtPreparedBy.Enabled = mMode
        cmdSearchPrepBy.Enabled = mMode
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
    Private Sub ReportOnLabScope(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnLabScope(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnLabScope(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
