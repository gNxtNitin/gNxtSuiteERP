Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmVoltmeterCal
    Inherits System.Windows.Forms.Form
    Dim RsVoltmeterCalibHdr As ADODB.Recordset
    Dim RsVoltmeterCalibDet As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Private Const ConRowHeight As Short = 14

    Private Const ColParamDesc As Short = 1
    Private Const ColReadingStep As Short = 2
    Private Const ColPerError As Short = 3
    Private Const ColObservation As Short = 4

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
            If RsVoltmeterCalibHdr.EOF = False Then RsVoltmeterCalibHdr.MoveFirst()
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
        If Not RsVoltmeterCalibHdr.EOF Then
            If PubSuperUser = "U" Then
                If RsVoltmeterCalibHdr.Fields("APPROVED_BY").Value <> "" Then MsgBox("Number been approved, So cann't be Deleted ") : Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_VOLTMETER_CALIB_HDR", (txtSlipNo.Text), RsVoltmeterCalibHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_VOLTMETER_CALIB_DET WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_VOLTMETER_CALIB_HDR WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsVoltmeterCalibHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsVoltmeterCalibHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            If PubSuperUser = "U" Then
                If RsVoltmeterCalibHdr.Fields("APPROVED_BY").Value <> "" Then MsgBox("Number been approved, So cann't be Modified ") : Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsVoltmeterCalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            Call MakeEnableDesableField(True)
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
        If IsRecordExist = True Then Exit Sub
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

    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_CALIB " & vbCrLf & " From QAL_VOLTMETER_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND CALIB_DATE =TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND DOCNO = " & Val(txtDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_CALIB").Value)
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
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim mRsTemp As ADODB.Recordset

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
            SqlStr = " INSERT INTO QAL_VOLTMETER_CALIB_HDR " & vbCrLf _
                            & " (AUTO_KEY_CALIB,COMPANY_CODE,CALIB_DATE,DOCNO," & vbCrLf _
                            & " REMARKS,MASTER_INST,CALIB_BY,APPROVED_BY, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(txtDocNo.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtMasterInst.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCalibBy.Text) & "','" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_VOLTMETER_CALIB_HDR SET " & vbCrLf _
                    & " AUTO_KEY_CALIB=" & mSlipNo & ", " & vbCrLf _
                    & " CALIB_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " DOCNO=" & Val(txtDocNo.Text) & ", " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " MASTER_INST='" & MainClass.AllowSingleQuote(txtMasterInst.Text) & "', " & vbCrLf _
                    & " CALIB_BY='" & MainClass.AllowSingleQuote(txtCalibBy.Text) & "', " & vbCrLf _
                    & " APPROVED_BY='" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_CALIB =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart

        SqlStr = ""
        SqlStr = " SELECT AUTO_KEY_CALIB " & vbCrLf & " FROM QAL_VOLTMETER_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " " & vbCrLf & " AND CALIB_DATE=" & vbCrLf & " (SELECT MAX(CALIB_DATE) " & vbCrLf & " FROM QAL_VOLTMETER_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsTemp.EOF Then
            If mRsTemp.Fields("AUTO_KEY_CALIB").Value = Val(lblMkey.Text) Then
                SqlStr = ""
                SqlStr = " UPDATE QAL_VOLTMETER_MST SET " & vbCrLf & " LAST_CALI_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " CALI_DUE_DATE=TO_DATE('" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Month, Val(lblFrequency.Text), CDate(txtDate.Text)), "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & Val(txtDocNo.Text) & ""

                PubDBCn.Execute(SqlStr)
            End If
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsVoltmeterCalibHdr.Requery()
        RsVoltmeterCalibDet.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_CALIB)  " & vbCrLf & " FROM QAL_VOLTMETER_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mParamDesc As String
        Dim mReadingStep As String
        Dim mPerError As String
        Dim mObservation As String

        PubDBCn.Execute("DELETE FROM QAL_VOLTMETER_CALIB_DET WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParamDesc
                mParamDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColReadingStep
                mReadingStep = MainClass.AllowSingleQuote(.Text)

                .Col = ColPerError
                mPerError = MainClass.AllowSingleQuote(.Text)

                .Col = ColObservation
                mObservation = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mParamDesc <> "" Then
                    SqlStr = " INSERT INTO  QAL_VOLTMETER_CALIB_DET ( " & vbCrLf & " AUTO_KEY_CALIB,SERIAL_NO,PARAM_DESC,READING_STEP,PER_ERROR,OBSERVATION ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mParamDesc & "', " & vbCrLf & " '" & mReadingStep & "','" & mPerError & "','" & mObservation & "') "
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

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Call SearchEmp(txtAppBy, lblAppBy)
    End Sub

    Private Sub cmdSearchCalibBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCalibBy.Click
        Call SearchEmp(txtCalibBy, lblCalibBy)
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

    Private Sub cmdSearchDocNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDocNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STATUS='A' " & vbCrLf
        If MainClass.SearchGridMaster("", "QAL_VOLTMETER_MST", "DOCNO", "DESCRIPTION", "E_NO", "RANGE", SqlStr) = True Then
            txtDocNo.Text = AcName
            If txtDocNo.Enabled = True Then txtDocNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_VOLTMETER_CALIB_HDR", "AUTO_KEY_CALIB", "CALIB_DATE", "DOCNO", "", SqlStr) = True Then
            txtSlipNo.Text = AcName
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsVoltmeterCalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub frmVoltmeterCal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Process Instruments Calibration"

        SqlStr = "Select * From QAL_VOLTMETER_CALIB_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeterCalibHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_VOLTMETER_CALIB_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeterCalibDet, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_CALIB AS SLIP_NUMBER,TO_CHAR(CALIB_DATE,'DD/MM/YYYY') AS CALIB_DATE, " & vbCrLf & " DOCNO,REMARKS,MASTER_INST,CALIB_BY,APPROVED_BY " & vbCrLf & " FROM QAL_VOLTMETER_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY AUTO_KEY_CALIB "
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmVoltmeterCal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmVoltmeterCal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(8715)
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
        txtDocNo.Text = ""
        lblDescription.Text = ""
        lblENo.Text = ""
        lblMake.Text = ""
        lblRange.Text = ""
        lblFrequency.Text = ""
        txtMasterInst.Text = ""
        txtRemarks.Text = ""
        txtCalibBy.Text = ""
        lblCalibBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsVoltmeterCalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsVoltmeterCalibDet.Fields("PARAM_DESC").DefinedSize

            .Col = ColReadingStep
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsVoltmeterCalibDet.Fields("READING_STEP").DefinedSize

            .Col = ColPerError
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsVoltmeterCalibDet.Fields("PER_ERROR").DefinedSize

            .Col = ColObservation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsVoltmeterCalibDet.Fields("OBSERVATION").DefinedSize

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParamDesc, ColPerError)
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
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 5)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 3)
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

        txtSlipNo.Maxlength = RsVoltmeterCalibHdr.Fields("AUTO_KEY_CALIB").Precision
        txtDate.Maxlength = RsVoltmeterCalibHdr.Fields("CALIB_DATE").DefinedSize - 6
        txtDocNo.Maxlength = RsVoltmeterCalibHdr.Fields("DOCNO").Precision
        txtMasterInst.Maxlength = RsVoltmeterCalibHdr.Fields("MASTER_INST").DefinedSize
        txtRemarks.Maxlength = RsVoltmeterCalibHdr.Fields("REMARKS").DefinedSize
        txtCalibBy.Maxlength = RsVoltmeterCalibHdr.Fields("CALIB_BY").DefinedSize
        txtAppBy.Maxlength = RsVoltmeterCalibHdr.Fields("APPROVED_BY").DefinedSize

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

        If MODIFYMode = True And RsVoltmeterCalibHdr.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDocNo.Text) = "" Then
            MsgInformation("Doc No. empty, So unable to save.")
            txtDocNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCalibBy.Text) = "" Then
            MsgInformation("Calibrated By is empty, So unable to save.")
            txtCalibBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmVoltmeterCal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        RsVoltmeterCalibHdr.Close()
        RsVoltmeterCalibHdr = Nothing
        RsVoltmeterCalibDet.Close()
        RsVoltmeterCalibDet = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim SqlStr As String
        '    If Col = 0 And Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
        '        MainClass.DeleteSprdRow SprdMain, Row, ColParamDesc
        '        MainClass.SaveStatus Me, ADDMode, MODIFYMode
        '    End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
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
        If Trim(pTextBox.Text) = "" Then pLable.Text = "" : Exit Function
        pTextBox.Text = VB6.Format(pTextBox.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If
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

    Private Sub txtAppBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAppBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtAppBy, lblAppBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMasterInst_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMasterInst.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDocNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtDocNo.Text) = "" Then GoTo EventExitSub

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_VOLTMETER_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STATUS='A' " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsTemp.EOF Then
            lblDescription.Text = IIf(IsDbNull(mRsTemp.Fields("Description").Value), "", mRsTemp.Fields("Description").Value)
            lblENo.Text = IIf(IsDbNull(mRsTemp.Fields("E_NO").Value), "", mRsTemp.Fields("E_NO").Value)
            lblMake.Text = IIf(IsDbNull(mRsTemp.Fields("MAKE").Value), "", mRsTemp.Fields("MAKE").Value)
            lblRange.Text = IIf(IsDbNull(mRsTemp.Fields("Range").Value), "", mRsTemp.Fields("Range").Value)
            lblFrequency.Text = IIf(IsDbNull(mRsTemp.Fields("FREQUENCY").Value), "", mRsTemp.Fields("FREQUENCY").Value)
            FillPerErrors()
        Else
            MsgBox("Not a valid Doc No.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCalibBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCalibBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCalibBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCalibBy.DoubleClick
        Call cmdSearchCalibBy_Click(cmdSearchCalibBy, New System.EventArgs())
    End Sub

    Private Sub txtCalibBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCalibBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCalibBy_Click(cmdSearchCalibBy, New System.EventArgs())
    End Sub

    Private Sub txtCalibBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCalibBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtCalibBy, lblCalibBy) = False Then Cancel = True
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

        If Not RsVoltmeterCalibHdr.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsVoltmeterCalibHdr.Fields("AUTO_KEY_CALIB").Value), "", RsVoltmeterCalibHdr.Fields("AUTO_KEY_CALIB").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsVoltmeterCalibHdr.Fields("AUTO_KEY_CALIB").Value), "", RsVoltmeterCalibHdr.Fields("AUTO_KEY_CALIB").Value)
            txtDate.Text = IIf(IsDbNull(RsVoltmeterCalibHdr.Fields("CALIB_DATE").Value), "", RsVoltmeterCalibHdr.Fields("CALIB_DATE").Value)
            txtDocNo.Text = IIf(IsDbNull(RsVoltmeterCalibHdr.Fields("DOCNO").Value), "", RsVoltmeterCalibHdr.Fields("DOCNO").Value)
            txtDocNo_Validating(txtDocNo, New System.ComponentModel.CancelEventArgs(False))
            txtMasterInst.Text = IIf(IsDbNull(RsVoltmeterCalibHdr.Fields("MASTER_INST").Value), "", RsVoltmeterCalibHdr.Fields("MASTER_INST").Value)
            txtRemarks.Text = IIf(IsDbNull(RsVoltmeterCalibHdr.Fields("REMARKS").Value), "", RsVoltmeterCalibHdr.Fields("REMARKS").Value)
            txtCalibBy.Text = IIf(IsDbNull(RsVoltmeterCalibHdr.Fields("CALIB_BY").Value), "", RsVoltmeterCalibHdr.Fields("CALIB_BY").Value)
            txtCalibBy_Validating(txtCalibBy, New System.ComponentModel.CancelEventArgs(False))
            txtAppBy.Text = IIf(IsDbNull(RsVoltmeterCalibHdr.Fields("APPROVED_BY").Value), "", RsVoltmeterCalibHdr.Fields("APPROVED_BY").Value)
            txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsVoltmeterCalibHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub FillPerErrors()

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtDocNo.Text) = "" Then Exit Sub
        SqlStr = "SELECT SERIAL_NO,PARAM_DESC,READING_STEP,PER_ERROR " & vbCrLf & " From QAL_VOLTMETER_CALIB_PE " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & Val(txtDocNo.Text) & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParamDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColReadingStep
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("READING_STEP").Value), "", .Fields("READING_STEP").Value))

                SprdMain.Col = ColPerError
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PER_ERROR").Value), "", .Fields("PER_ERROR").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mPerError As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_VOLTMETER_CALIB_DET " & vbCrLf & " WHERE AUTO_KEY_CALIB=" & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeterCalibDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsVoltmeterCalibDet
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParamDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColReadingStep
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("READING_STEP").Value), "", .Fields("READING_STEP").Value))

                SprdMain.Col = ColPerError
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PER_ERROR").Value), "", .Fields("PER_ERROR").Value))

                SprdMain.Col = ColObservation
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERVATION").Value), "", .Fields("OBSERVATION").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParamDesc, ColPerError)
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

    Public Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub
        If Len(Trim(txtSlipNo.Text)) < 6 Then
            txtSlipNo.Text = Trim(txtSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsVoltmeterCalibHdr.BOF = False Then xMKey = RsVoltmeterCalibHdr.Fields("AUTO_KEY_CALIB").Value

        SqlStr = "SELECT * FROM QAL_VOLTMETER_CALIB_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_CALIB=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeterCalibHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsVoltmeterCalibHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_VOLTMETER_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_CALIB=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVoltmeterCalibHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        '    txtDate.Enabled = mMode
        '    txtDocNo.Enabled = mMode
        '    cmdSearchDocNo.Enabled = mMode
        '    txtMasterInst.Enabled = mMode
        '    txtRemarks.Enabled = mMode
        '    txtCalibBy.Enabled = mMode
        '    cmdSearchCalibBy.Enabled = mMode
        '    txtAppBy.Enabled = mMode
        '    cmdSearchAppBy.Enabled = mMode
    End Sub

    Private Sub ReportOnVoltmeterCalib(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "CALIBRATION CERTIFICATE" ''VOLTMETER/AMMETER

        SqlStr = " SELECT QAL_VOLTMETER_CALIB_HDR.*,QAL_VOLTMETER_CALIB_DET.*,QAL_VOLTMETER_MST.*, " & vbCrLf & " CALIB.EMP_NAME,APP.EMP_NAME " & vbCrLf & " FROM QAL_VOLTMETER_CALIB_HDR,QAL_VOLTMETER_CALIB_DET ,QAL_VOLTMETER_MST, " & vbCrLf & " PAY_EMPLOYEE_MST CALIB, PAY_EMPLOYEE_MST APP " & vbCrLf & " WHERE QAL_VOLTMETER_CALIB_HDR.AUTO_KEY_CALIB=QAL_VOLTMETER_CALIB_DET.AUTO_KEY_CALIB(+) " & vbCrLf & " AND QAL_VOLTMETER_CALIB_HDR.COMPANY_CODE=QAL_VOLTMETER_MST.COMPANY_CODE " & vbCrLf & " AND QAL_VOLTMETER_CALIB_HDR.DOCNO=QAL_VOLTMETER_MST.DOCNO " & vbCrLf & " AND QAL_VOLTMETER_CALIB_HDR.COMPANY_CODE=CALIB.COMPANY_CODE(+) " & vbCrLf & " AND QAL_VOLTMETER_CALIB_HDR.CALIB_BY=CALIB.EMP_CODE(+) " & vbCrLf & " AND QAL_VOLTMETER_CALIB_HDR.COMPANY_CODE=APP.COMPANY_CODE(+) " & vbCrLf & " AND QAL_VOLTMETER_CALIB_HDR.APPROVED_BY=APP.EMP_CODE(+) " & vbCrLf & " AND QAL_VOLTMETER_CALIB_HDR.AUTO_KEY_CALIB=" & Val(txtSlipNo.Text) & ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\VoltmeterCal.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnVoltmeterCalib(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnVoltmeterCalib(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
