Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frm5W2H
    Inherits System.Windows.Forms.Form
    Dim RsWHMain As ADODB.Recordset
    Dim RsWHDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Dim xMyMenu As String

    Private Const ConRowHeight As Short = 14

    Private Const ColWhat As Short = 1
    Private Const ColWhy As Short = 2
    Private Const ColWhere As Short = 3
    Private Const ColHowMany As Short = 4
    Private Const ColHow As Short = 5
    Private Const ColWho As Short = 6
    Private Const ColWhen As Short = 7

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
            If RsWHMain.EOF = False Then RsWHMain.MoveFirst()
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
        If Not RsWHMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_W52H_HDR", (txtSlipNo.Text), RsWHMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_W52H_DET WHERE AUTO_KEY_5W2H=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_W52H_HDR WHERE AUTO_KEY_5W2H=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsWHMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsWHMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsWHMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            SqlStr = " INSERT INTO QAL_W52H_HDR " & vbCrLf _
                            & " (AUTO_KEY_5W2H,COMPANY_CODE," & vbCrLf _
                            & " WITH_DESC,SUBJECT_DESC,HELDAT_DESC,HELDON_DATE," & vbCrLf _
                            & " REC_EMP_CODE,PRESENT_HEIL1,PRESENT_HEIL2, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtWith.Text) & "','" & MainClass.AllowSingleQuote(txtSubject.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtHeldAt.Text) & "',TO_DATE('" & VB6.Format(txtHeldOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPresentHEIL1.Text) & "','" & MainClass.AllowSingleQuote(txtPresentHEIL2.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_W52H_HDR SET " & vbCrLf _
                            & " AUTO_KEY_5W2H=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                            & " WITH_DESC='" & MainClass.AllowSingleQuote(txtWith.Text) & "', " & vbCrLf _
                            & " SUBJECT_DESC='" & MainClass.AllowSingleQuote(txtSubject.Text) & "', " & vbCrLf _
                            & " HELDAT_DESC='" & MainClass.AllowSingleQuote(txtHeldAt.Text) & "', " & vbCrLf _
                            & " HELDON_DATE=TO_DATE('" & VB6.Format(txtHeldOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " REC_EMP_CODE='" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf _
                            & " PRESENT_HEIL1='" & MainClass.AllowSingleQuote(txtPresentHEIL1.Text) & "', " & vbCrLf _
                            & " PRESENT_HEIL2='" & MainClass.AllowSingleQuote(txtPresentHEIL2.Text) & "', " & vbCrLf _
                            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                            & " AND AUTO_KEY_5W2H =" & Val(lblMkey.Text) & ""
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
        RsWHMain.Requery()
        RsWHDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_5W2H)  " & vbCrLf & " FROM QAL_W52H_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_5W2H,LENGTH(AUTO_KEY_5W2H)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mWhat As String
        Dim mWhy As String
        Dim mWhere As String
        Dim mHowMany As String
        Dim mHow As String
        Dim mWho As String
        Dim mWhen As String

        PubDBCn.Execute("DELETE FROM QAL_W52H_DET WHERE AUTO_KEY_5W2H=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColWhat
                mWhat = MainClass.AllowSingleQuote(.Text)

                .Col = ColWhy
                mWhy = MainClass.AllowSingleQuote(.Text)

                .Col = ColWhere
                mWhere = MainClass.AllowSingleQuote(.Text)

                .Col = ColHowMany
                mHowMany = MainClass.AllowSingleQuote(.Text)

                .Col = ColHow
                mHow = MainClass.AllowSingleQuote(.Text)

                .Col = ColWho
                mWho = MainClass.AllowSingleQuote(.Text)

                .Col = ColWhen
                mWhen = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mWhat <> "" Then
                    SqlStr = " INSERT INTO  QAL_W52H_DET ( " & vbCrLf & " AUTO_KEY_5W2H,SERIAL_NO,WHAT_DESC,WHY_DESC, " & vbCrLf & " WHERE_DESC,HOWMANY_DESC,HOW_DESC,WHO_DESC,WHEN_DESC) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mWhat & "','" & mWhy & "', " & vbCrLf & " '" & mWhere & "','" & mHowMany & "','" & mHow & "','" & mWho & "','" & mWhen & "') "
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
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_5W2H,LENGTH(AUTO_KEY_5W2H)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_W52H_HDR", "AUTO_KEY_5W2H", "WITH_DESC", , , SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsWHMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frm5W2H_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "5W2H"

        SqlStr = "Select * From QAL_W52H_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsWHMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_W52H_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsWHDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_5W2H AS SLIP_NUMBER,WITH_DESC,SUBJECT_DESC, " & vbCrLf & " HELDAT_DESC,TO_CHAR(HELDON_DATE,'DD/MM/YYYY') AS HELD_ON, " & vbCrLf & " REC_EMP_CODE,PRESENT_HEIL1,PRESENT_HEIL2 " & vbCrLf & " FROM QAL_W52H_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_5W2H,LENGTH(AUTO_KEY_5W2H)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_5W2H"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frm5W2H_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frm5W2H_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        xMyMenu = myMenu
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(11370)
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
        txtWith.Text = ""
        txtSubject.Text = ""
        txtHeldAt.Text = ""
        txtHeldOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        TxtPreparedBy.Text = ""
        lblPreparedBy.Text = ""
        txtPresentHEIL1.Text = ""
        txtPresentHEIL2.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsWHMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColWhat
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsWHDetail.Fields("WHAT_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColWhy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsWHDetail.Fields("WHY_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColWhere
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsWHDetail.Fields("WHERE_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColHowMany
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsWHDetail.Fields("HOWMANY_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColHow
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsWHDetail.Fields("HOW_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColWho
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsWHDetail.Fields("WHO_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColWhen
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsWHDetail.Fields("WHEN_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

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
            .set_ColWidth(2, 500 * 4)
            .set_ColWidth(3, 500 * 4)
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 5)
            .set_ColWidth(7, 500 * 5)
            .set_ColWidth(8, 500 * 5)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsWHMain.Fields("AUTO_KEY_5W2H").Precision
        txtWith.Maxlength = RsWHMain.Fields("WITH_DESC").DefinedSize
        txtSubject.Maxlength = RsWHMain.Fields("SUBJECT_DESC").DefinedSize
        txtHeldAt.Maxlength = RsWHMain.Fields("HELDAT_DESC").DefinedSize
        txtHeldOn.Maxlength = RsWHMain.Fields("HELDON_DATE").DefinedSize - 6
        TxtPreparedBy.Maxlength = RsWHMain.Fields("REC_EMP_CODE").DefinedSize
        txtPresentHEIL1.Maxlength = RsWHMain.Fields("PRESENT_HEIL1").DefinedSize
        txtPresentHEIL2.Maxlength = RsWHMain.Fields("PRESENT_HEIL2").DefinedSize
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
        If MODIFYMode = True And RsWHMain.EOF = True Then Exit Function

        If Trim(txtWith.Text) = "" Then
            MsgInformation("With is empty, So unable to save.")
            txtWith.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSubject.Text) = "" Then
            MsgInformation("Subject is empty, So unable to save.")
            txtSubject.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtHeldAt.Text) = "" Then
            MsgInformation("Held At is empty, So unable to save.")
            txtHeldOn.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtHeldOn.Text) = "" Then
            MsgInformation("Held On is empty, So unable to save.")
            txtHeldOn.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtPreparedBy.Text) = "" Then
            MsgInformation("Recorded By is empty, So unable to save.")
            TxtPreparedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPresentHEIL1.Text) = "" Then
            MsgInformation("Present HEIL1 is empty, So unable to save.")
            txtPresentHEIL1.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPresentHEIL2.Text) = "" Then
            MsgInformation("Present HEIL2 is empty, So unable to save.")
            txtPresentHEIL2.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColWhat, "S", "Please Check What") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColWhy, "S", "Please Check Why") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColWhere, "S", "Please Check Where") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColHowMany, "S", "Please Check How Many") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColHow, "S", "Please Check How") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColWho, "S", "Please Check Who") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColWhen, "S", "Please Check When") = False Then FieldsVarification = False : Exit Function


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frm5W2H_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsWHMain.Close()
        RsWHMain = Nothing
        RsWHDetail.Close()
        RsWHDetail = Nothing
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
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColWhat)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xWhat As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColWhat
        xWhat = Trim(SprdMain.Text)
        If xWhat = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColWhat
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColWhat
                xWhat = Trim(SprdMain.Text)
                If xWhat = "" Then Exit Sub
                MainClass.AddBlankSprdRow(SprdMain, ColWhat, ConRowHeight)
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

    Private Sub txtHeldAt_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHeldAt.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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
    Private Sub txtHeldOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHeldOn.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHeldOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHeldOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtHeldOn.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtHeldOn.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPresentHEIL1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPresentHEIL1.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPresentHEIL2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPresentHEIL2.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsWHMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsWHMain.Fields("AUTO_KEY_5W2H").Value), "", RsWHMain.Fields("AUTO_KEY_5W2H").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsWHMain.Fields("AUTO_KEY_5W2H").Value), "", RsWHMain.Fields("AUTO_KEY_5W2H").Value)
            txtWith.Text = IIf(IsDbNull(RsWHMain.Fields("WITH_DESC").Value), "", RsWHMain.Fields("WITH_DESC").Value)
            txtSubject.Text = IIf(IsDbNull(RsWHMain.Fields("SUBJECT_DESC").Value), "", RsWHMain.Fields("SUBJECT_DESC").Value)
            txtHeldAt.Text = IIf(IsDbNull(RsWHMain.Fields("HELDAT_DESC").Value), "", RsWHMain.Fields("HELDAT_DESC").Value)
            txtHeldOn.Text = IIf(IsDbNull(RsWHMain.Fields("HELDON_DATE").Value), "", RsWHMain.Fields("HELDON_DATE").Value)
            TxtPreparedBy.Text = IIf(IsDbNull(RsWHMain.Fields("REC_EMP_CODE").Value), "", RsWHMain.Fields("REC_EMP_CODE").Value)
            TxtPreparedBy_Validating(TxtPreparedBy, New System.ComponentModel.CancelEventArgs(False))
            txtPresentHEIL1.Text = IIf(IsDbNull(RsWHMain.Fields("PRESENT_HEIL1").Value), "", RsWHMain.Fields("PRESENT_HEIL1").Value)
            txtPresentHEIL2.Text = IIf(IsDbNull(RsWHMain.Fields("PRESENT_HEIL2").Value), "", RsWHMain.Fields("PRESENT_HEIL2").Value)
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsWHMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_W52H_DET " & vbCrLf & " WHERE AUTO_KEY_5W2H=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsWHDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsWHDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColWhat
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("WHAT_DESC").Value), "", .Fields("WHAT_DESC").Value))

                SprdMain.Col = ColWhy
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("WHY_DESC").Value), "", .Fields("WHY_DESC").Value))

                SprdMain.Col = ColWhere
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("WHERE_DESC").Value), "", .Fields("WHERE_DESC").Value))

                SprdMain.Col = ColHowMany
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("HOWMANY_DESC").Value), "", .Fields("HOWMANY_DESC").Value))

                SprdMain.Col = ColHow
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("HOW_DESC").Value), "", .Fields("HOW_DESC").Value))

                SprdMain.Col = ColWho
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("WHO_DESC").Value), "", .Fields("WHO_DESC").Value))

                SprdMain.Col = ColWhen
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("WHEN_DESC").Value), "", .Fields("WHEN_DESC").Value))

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

        If Len(Trim(txtSlipNo.Text)) < 6 Then
            txtSlipNo.Text = txtSlipNo.Text & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsWHMain.BOF = False Then xMKey = RsWHMain.Fields("AUTO_KEY_5W2H").Value

        SqlStr = "SELECT * FROM QAL_W52H_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_5W2H,LENGTH(AUTO_KEY_5W2H)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_5W2H=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsWHMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsWHMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_W52H_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_5W2H,LENGTH(AUTO_KEY_5W2H)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_5W2H=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsWHMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtHeldOn.Enabled = mMode
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
    Private Sub ReportOn5W2H(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        MainClass.ClearCrptFormulas(Report1)

        mTitle = "5W2H"

        SqlStr = " SELECT QAL_W52H_HDR.*,QAL_W52H_DET.*, PAY_EMPLOYEE_MST.EMP_NAME " & vbCrLf & " FROM QAL_W52H_HDR, QAL_W52H_DET, PAY_EMPLOYEE_MST " & vbCrLf & " WHERE QAL_W52H_HDR.AUTO_KEY_5W2H=QAL_W52H_DET.AUTO_KEY_5W2H(+) " & vbCrLf & " AND QAL_W52H_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE(+) " & vbCrLf & " AND QAL_W52H_HDR.REC_EMP_CODE=PAY_EMPLOYEE_MST.EMP_CODE(+) " & vbCrLf & " AND QAL_W52H_HDR.AUTO_KEY_5W2H=" & Val(txtSlipNo.Text) & ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\5W2H.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOn5W2H(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOn5W2H(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtSubject_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubject.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWith_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWith.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
End Class
