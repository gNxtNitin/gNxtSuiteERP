Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProcQualRecord
    Inherits System.Windows.Forms.Form
    Dim RsProcQualRecordMain As ADODB.Recordset
    Dim RsProcQualRecordDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColProcessType As Short = 1
    Private Const ColParamDesc As Short = 2
    Private Const ColSpecMin As Short = 3
    Private Const ColSpecMax As Short = 4
    Private Const ColTrialMinSet As Short = 5
    Private Const ColTrialMinObs As Short = 6
    Private Const ColTrialMinRemark As Short = 7
    Private Const ColTrialMaxSet As Short = 8
    Private Const ColTrialMaxObs As Short = 9
    Private Const ColTrialMaxRemark As Short = 10


    Dim xMenuID As String
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
            If RsProcQualRecordMain.EOF = False Then RsProcQualRecordMain.MoveFirst()
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
        If Not RsProcQualRecordMain.EOF Then
            If RsProcQualRecordMain.Fields("APPR_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be deleted.") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_PROCTEST_HDR", (txtSlipNo.Text), RsProcQualRecordMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_PROCTEST_DET WHERE AUTO_KEY_PROCTEST=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_PROCTEST_HDR WHERE AUTO_KEY_PROCTEST=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsProcQualRecordMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsProcQualRecordMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            If RsProcQualRecordMain.Fields("APPR_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be modified") : Exit Sub
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProcQualRecordMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            SqlStr = " INSERT INTO QAL_PROCTEST_HDR " & vbCrLf _
                            & " (AUTO_KEY_PROCTEST,COMPANY_CODE," & vbCrLf _
                            & " PROCTEST_DATE,PART_NAME,MACHINE_DESC,PROCESS_NAME," & vbCrLf _
                            & " MIN_TRAIL_DATE,MAX_TRAIL_DATE,MIN_TRAIL_OPR,MAX_TRAIL_OPR,REMARKS, " & vbCrLf _
                            & " APPR_DATE,NOTE_DETAILS,APPR_EMP_CODE,QC_EMP_CODE,PRD_SUP_EMP_CODE, " & vbCrLf _
                            & " MGR_EMP_CODE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtNameTypeProd.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtNameEquip.Text) & "','" & MainClass.AllowSingleQuote(txtNameProcess.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtMinTrialDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtMaxTrialDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMinTrialOp.Text) & "','" & MainClass.AllowSingleQuote(txtMaxTrialOp.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(cboFinalRemarks.Text) & "',TO_DATE('" & vb6.Format(txtApprovedDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtNote.Text) & "','" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtQCInspector.Text) & "','" & MainClass.AllowSingleQuote(txtProdSupervisor.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtManager.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_PROCTEST_HDR SET " & vbCrLf _
                    & " AUTO_KEY_PROCTEST=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " PROCTEST_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),PART_NAME='" & MainClass.AllowSingleQuote(txtNameTypeProd.Text) & "', " & vbCrLf _
                    & " MACHINE_DESC='" & MainClass.AllowSingleQuote(txtNameEquip.Text) & "',PROCESS_NAME='" & MainClass.AllowSingleQuote(txtNameProcess.Text) & "', " & vbCrLf _
                    & " MIN_TRAIL_DATE=TO_DATE('" & vb6.Format(txtMinTrialDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),MAX_TRAIL_DATE=TO_DATE('" & vb6.Format(txtMaxTrialDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MIN_TRAIL_OPR='" & MainClass.AllowSingleQuote(txtMinTrialOp.Text) & "',MAX_TRAIL_OPR='" & MainClass.AllowSingleQuote(txtMaxTrialOp.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(cboFinalRemarks.Text) & "', " & vbCrLf _
                    & " APPR_DATE=TO_DATE('" & vb6.Format(txtApprovedDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NOTE_DETAILS='" & MainClass.AllowSingleQuote(txtNote.Text) & "', " & vbCrLf _
                    & " APPR_EMP_CODE='" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "',QC_EMP_CODE='" & MainClass.AllowSingleQuote(txtQCInspector.Text) & "', " & vbCrLf _
                    & " PRD_SUP_EMP_CODE='" & MainClass.AllowSingleQuote(txtProdSupervisor.Text) & "', " & vbCrLf _
                    & " MGR_EMP_CODE='" & MainClass.AllowSingleQuote(txtManager.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_PROCTEST =" & Val(lblMkey.Text) & ""
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
        RsProcQualRecordMain.Requery()
        RsProcQualRecordDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_PROCTEST)  " & vbCrLf & " FROM QAL_PROCTEST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCTEST,LENGTH(AUTO_KEY_PROCTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mProcessType As String
        Dim mParamDesc As String
        Dim mSpecMin As String
        Dim mSpecMax As String
        Dim mTrialMinSet As String
        Dim mTrialMinObs As String
        Dim mTrialMinRemark As String
        Dim mTrialMaxSet As String
        Dim mTrialMaxObs As String
        Dim mTrialMaxRemark As String


        PubDBCn.Execute("DELETE FROM QAL_PROCTEST_DET WHERE AUTO_KEY_PROCTEST=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColProcessType
                mProcessType = IIf(.Text = "Product", "R", "P")

                .Col = ColParamDesc
                mParamDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecMin
                mSpecMin = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecMax
                mSpecMax = MainClass.AllowSingleQuote(.Text)

                .Col = ColTrialMinSet
                mTrialMinSet = MainClass.AllowSingleQuote(.Text)

                .Col = ColTrialMinObs
                mTrialMinObs = MainClass.AllowSingleQuote(.Text)

                .Col = ColTrialMinRemark
                mTrialMinRemark = MainClass.AllowSingleQuote(.Text)

                .Col = ColTrialMaxSet
                mTrialMaxSet = MainClass.AllowSingleQuote(.Text)

                .Col = ColTrialMaxObs
                mTrialMaxObs = MainClass.AllowSingleQuote(.Text)

                .Col = ColTrialMaxRemark
                mTrialMaxRemark = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mProcessType <> "" And mParamDesc <> "" Then
                    SqlStr = " INSERT INTO  QAL_PROCTEST_DET ( " & vbCrLf & " AUTO_KEY_PROCTEST,SERIAL_NO,PROCESS_TYPE,PARA_DESC,SPEC_MIN,SPEC_MAX, " & vbCrLf & " TRAIL_MIN_SET,TRAIL_MIN_OBS,TRAIL_MIN_REMARKS,TRAIL_MAX_SET,TRAIL_MAX_OBS,TRAIL_MAX_REMARKS ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mProcessType & "','" & mParamDesc & "', " & vbCrLf & " '" & mSpecMin & "','" & mSpecMax & "','" & mTrialMinSet & "','" & mTrialMinObs & "','" & mTrialMinRemark & "', " & vbCrLf & " '" & mTrialMaxSet & "','" & mTrialMaxObs & "','" & mTrialMaxRemark & "') "
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


    Private Sub cmdSearchApprBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchApprBy.Click
        Call SearchEmp(txtApprovedBy, lblApprovedBy)
    End Sub

    Private Sub cmdSearchMan_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMan.Click
        Call SearchEmp(txtManager, lblManager)
    End Sub

    Private Sub cmdSearchProdSuper_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProdSuper.Click
        Call SearchEmp(txtProdSupervisor, lblProdSupervisor)
    End Sub


    Private Sub cmdSearchQCInsp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchQCInsp.Click
        Call SearchEmp(txtQCInspector, lblQCInspector)
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
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCTEST,LENGTH(AUTO_KEY_PROCTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_PROCTEST_HDR", "AUTO_KEY_PROCTEST", "PROCTEST_DATE", "PART_NAME", "MACHINE_DESC", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsProcQualRecordMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmProcQualRecord_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Process Qualification Record"

        SqlStr = "Select * From QAL_PROCTEST_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcQualRecordMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_PROCTEST_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcQualRecordDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_PROCTEST AS SLIP_NUMBER,TO_CHAR(PROCTEST_DATE,'DD/MM/YYYY') AS PROCTEST_DATE, " & vbCrLf & " PART_NAME,MACHINE_DESC,PROCESS_NAME,REMARKS,  " & vbCrLf & " APPR_EMP_CODE,QC_EMP_CODE,PRD_SUP_EMP_CODE,MGR_EMP_CODE " & vbCrLf & " FROM QAL_PROCTEST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCTEST,LENGTH(AUTO_KEY_PROCTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_PROCTEST"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmProcQualRecord_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProcQualRecord_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMenuID = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(10755)
        cboFinalRemarks.Items.Add("Process Qualified")
        cboFinalRemarks.Items.Add("Not Qualified")
        cboFinalRemarks.Items.Add("Further Trial Required")
        cboFinalRemarks.Items.Add(" ")
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
        txtNameTypeProd.Text = ""
        txtNameEquip.Text = ""
        txtNameProcess.Text = ""
        txtMinTrialDate.Text = ""
        txtMaxTrialDate.Text = ""
        txtMinTrialOp.Text = ""
        txtMaxTrialOp.Text = ""
        txtApprovedDate.Text = ""
        cboFinalRemarks.SelectedIndex = 1
        txtNote.Text = ""
        txtApprovedBy.Text = ""
        lblApprovedBy.Text = ""
        txtQCInspector.Text = ""
        lblQCInspector.Text = ""
        txtProdSupervisor.Text = ""
        lblProdSupervisor.Text = ""
        txtManager.Text = ""
        lblManager.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsProcQualRecordMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColProcessType
            .CellType = SS_CELL_TYPE_COMBOBOX
            .TypeComboBoxList = "Process" & Chr(9) & "Product"
            .TypeComboBoxCurSel = 0

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("PARA_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSpecMin
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("SPEC_MIN").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSpecMax
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("SPEC_MAX").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColTrialMinSet
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("TRAIL_MIN_SET").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColTrialMinObs
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("TRAIL_MIN_OBS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColTrialMinRemark
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("TRAIL_MIN_REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColTrialMaxSet
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("TRAIL_MAX_SET").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColTrialMaxObs
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("TRAIL_MAX_OBS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColTrialMaxRemark
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsProcQualRecordDetail.Fields("TRAIL_MAX_REMARKS").DefinedSize
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
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 5)
            .set_ColWidth(4, 500 * 5)
            .set_ColWidth(5, 500 * 5)
            .set_ColWidth(6, 500 * 5)
            .set_ColWidth(7, 500 * 4)
            .set_ColWidth(8, 500 * 4)
            .set_ColWidth(9, 500 * 4)
            .set_ColWidth(10, 500 * 4)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsProcQualRecordMain.Fields("AUTO_KEY_PROCTEST").Precision
        txtDate.Maxlength = RsProcQualRecordMain.Fields("PROCTEST_DATE").DefinedSize - 6
        txtNameTypeProd.Maxlength = RsProcQualRecordMain.Fields("PART_NAME").DefinedSize
        txtNameEquip.Maxlength = RsProcQualRecordMain.Fields("MACHINE_DESC").DefinedSize
        txtNameProcess.Maxlength = RsProcQualRecordMain.Fields("PROCESS_NAME").DefinedSize
        txtMinTrialDate.Maxlength = RsProcQualRecordMain.Fields("MIN_TRAIL_DATE").DefinedSize - 6
        txtMaxTrialDate.Maxlength = RsProcQualRecordMain.Fields("MAX_TRAIL_DATE").DefinedSize - 6
        txtMinTrialOp.Maxlength = RsProcQualRecordMain.Fields("MIN_TRAIL_OPR").DefinedSize
        txtMaxTrialOp.Maxlength = RsProcQualRecordMain.Fields("MAX_TRAIL_OPR").DefinedSize
        txtApprovedDate.Maxlength = RsProcQualRecordMain.Fields("APPR_DATE").DefinedSize - 6
        txtNote.Maxlength = RsProcQualRecordMain.Fields("NOTE_DETAILS").DefinedSize
        txtApprovedBy.Maxlength = RsProcQualRecordMain.Fields("APPR_EMP_CODE").DefinedSize
        txtQCInspector.Maxlength = RsProcQualRecordMain.Fields("QC_EMP_CODE").DefinedSize
        txtProdSupervisor.Maxlength = RsProcQualRecordMain.Fields("PRD_SUP_EMP_CODE").DefinedSize
        txtManager.Maxlength = RsProcQualRecordMain.Fields("MGR_EMP_CODE").DefinedSize
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
        If MODIFYMode = True And RsProcQualRecordMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtNameTypeProd.Text) = "" Then
            MsgInformation("Part Name is empty, So unable to save.")
            txtNameTypeProd.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtNameEquip.Text) = "" Then
            MsgInformation("Machine Description is empty, So unable to save.")
            txtNameEquip.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtNameProcess.Text) = "" Then
            MsgInformation("Process Name is empty, So unable to save.")
            txtNameProcess.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColProcessType, "S", "Please Check Process Type.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColParamDesc, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Sub frmProcQualRecord_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsProcQualRecordMain.Close()
        RsProcQualRecordMain = Nothing
        RsProcQualRecordDetail.Close()
        RsProcQualRecordDetail = Nothing
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
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColParamDesc)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xParamDesc As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColParamDesc
        xParamDesc = Trim(SprdMain.Text)
        If xParamDesc = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColParamDesc
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColParamDesc
                xParamDesc = Trim(SprdMain.Text)
                If xParamDesc = "" Then Exit Sub
                MainClass.AddBlankSprdRow(SprdMain, ColParamDesc, ConRowHeight)
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


    Private Sub txtApprovedDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtApprovedDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtApprovedDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtApprovedDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtApprovedDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtManager_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtManager.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtManager_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtManager.DoubleClick
        Call cmdSearchMan_Click(cmdSearchMan, New System.EventArgs())
    End Sub

    Private Sub txtManager_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtManager.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMan_Click(cmdSearchMan, New System.EventArgs())
    End Sub

    Private Sub txtManager_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtManager.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtManager, lblManager) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMaxTrialOp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaxTrialOp.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMinTrialDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMinTrialDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMinTrialDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMinTrialDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtMinTrialDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtMinTrialDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtApprovedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtApprovedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.DoubleClick
        Call cmdSearchApprBy_Click(cmdSearchApprBy, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtApprovedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchApprBy_Click(cmdSearchApprBy, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtApprovedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtApprovedBy, lblApprovedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtMinTrialOp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMinTrialOp.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNameEquip_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNameEquip.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNameProcess_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNameProcess.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNameTypeProd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNameTypeProd.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNote_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNote.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProdSupervisor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdSupervisor.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProdSupervisor_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdSupervisor.DoubleClick
        Call cmdSearchProdSuper_Click(cmdSearchProdSuper, New System.EventArgs())
    End Sub

    Private Sub txtProdSupervisor_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProdSupervisor.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProdSuper_Click(cmdSearchProdSuper, New System.EventArgs())
    End Sub

    Private Sub txtProdSupervisor_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProdSupervisor.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtProdSupervisor, lblProdSupervisor) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMaxTrialDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaxTrialDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMaxTrialDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMaxTrialDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtMaxTrialDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtMaxTrialDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtQCInspector_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQCInspector.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQCInspector_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQCInspector.DoubleClick
        Call cmdSearchQCInsp_Click(cmdSearchQCInsp, New System.EventArgs())
    End Sub

    Private Sub txtQCInspector_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtQCInspector.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchQCInsp_Click(cmdSearchQCInsp, New System.EventArgs())
    End Sub

    Private Sub txtQCInspector_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtQCInspector.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtQCInspector, lblQCInspector) = False Then Cancel = True
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

        If Not RsProcQualRecordMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("AUTO_KEY_PROCTEST").Value), "", RsProcQualRecordMain.Fields("AUTO_KEY_PROCTEST").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("AUTO_KEY_PROCTEST").Value), "", RsProcQualRecordMain.Fields("AUTO_KEY_PROCTEST").Value)
            txtDate.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("PROCTEST_DATE").Value), "", RsProcQualRecordMain.Fields("PROCTEST_DATE").Value)
            txtNameTypeProd.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("PART_NAME").Value), "", RsProcQualRecordMain.Fields("PART_NAME").Value)
            txtNameEquip.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("MACHINE_DESC").Value), "", RsProcQualRecordMain.Fields("MACHINE_DESC").Value)
            txtNameProcess.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("PROCESS_NAME").Value), "", RsProcQualRecordMain.Fields("PROCESS_NAME").Value)
            txtMinTrialDate.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("MIN_TRAIL_DATE").Value), "", RsProcQualRecordMain.Fields("MIN_TRAIL_DATE").Value)
            txtMaxTrialDate.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("MAX_TRAIL_DATE").Value), "", RsProcQualRecordMain.Fields("MAX_TRAIL_DATE").Value)
            txtMinTrialOp.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("MIN_TRAIL_OPR").Value), "", RsProcQualRecordMain.Fields("MIN_TRAIL_OPR").Value)
            txtMaxTrialOp.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("MAX_TRAIL_OPR").Value), "", RsProcQualRecordMain.Fields("MAX_TRAIL_OPR").Value)
            cboFinalRemarks.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("REMARKS").Value), " ", RsProcQualRecordMain.Fields("REMARKS").Value)
            txtApprovedDate.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("APPR_DATE").Value), "", RsProcQualRecordMain.Fields("APPR_DATE").Value)
            txtNote.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("NOTE_DETAILS").Value), "", RsProcQualRecordMain.Fields("NOTE_DETAILS").Value)
            txtApprovedBy.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("APPR_EMP_CODE").Value), "", RsProcQualRecordMain.Fields("APPR_EMP_CODE").Value)
            txtApprovedBy_Validating(txtApprovedBy, New System.ComponentModel.CancelEventArgs(False))
            txtQCInspector.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("QC_EMP_CODE").Value), "", RsProcQualRecordMain.Fields("QC_EMP_CODE").Value)
            txtQCInspector_Validating(txtQCInspector, New System.ComponentModel.CancelEventArgs(False))
            txtProdSupervisor.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("PRD_SUP_EMP_CODE").Value), "", RsProcQualRecordMain.Fields("PRD_SUP_EMP_CODE").Value)
            txtProdSupervisor_Validating(txtProdSupervisor, New System.ComponentModel.CancelEventArgs(False))
            txtManager.Text = IIf(IsDbNull(RsProcQualRecordMain.Fields("MGR_EMP_CODE").Value), "", RsProcQualRecordMain.Fields("MGR_EMP_CODE").Value)
            txtManager_Validating(txtManager, New System.ComponentModel.CancelEventArgs(False))
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsProcQualRecordMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_PROCTEST_DET " & vbCrLf & " WHERE AUTO_KEY_PROCTEST=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcQualRecordDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsProcQualRecordDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColProcessType
                SprdMain.Text = IIf(IsDbNull(Trim(.Fields("PROCESS_TYPE").Value)) Or (Trim(.Fields("PROCESS_TYPE").Value)) = "P", "Process", "Product")

                SprdMain.Col = ColParamDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARA_DESC").Value), "", .Fields("PARA_DESC").Value))

                SprdMain.Col = ColSpecMin
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPEC_MIN").Value), "", .Fields("SPEC_MIN").Value))

                SprdMain.Col = ColSpecMax
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPEC_MAX").Value), "", .Fields("SPEC_MAX").Value))

                SprdMain.Col = ColTrialMinSet
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("TRAIL_MIN_SET").Value), "", .Fields("TRAIL_MIN_SET").Value))

                SprdMain.Col = ColTrialMinObs
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("TRAIL_MIN_OBS").Value), "", .Fields("TRAIL_MIN_OBS").Value))

                SprdMain.Col = ColTrialMinRemark
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("TRAIL_MIN_REMARKS").Value), "", .Fields("TRAIL_MIN_REMARKS").Value))

                SprdMain.Col = ColTrialMaxSet
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("TRAIL_MAX_SET").Value), "", .Fields("TRAIL_MAX_SET").Value))

                SprdMain.Col = ColTrialMaxObs
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("TRAIL_MAX_OBS").Value), "", .Fields("TRAIL_MAX_OBS").Value))

                SprdMain.Col = ColTrialMaxRemark
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("TRAIL_MAX_REMARKS").Value), "", .Fields("TRAIL_MAX_REMARKS").Value))

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
            txtSlipNo.Text = Trim(txtSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsProcQualRecordMain.BOF = False Then xMKey = RsProcQualRecordMain.Fields("AUTO_KEY_PROCTEST").Value

        SqlStr = "SELECT * FROM QAL_PROCTEST_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCTEST,LENGTH(AUTO_KEY_PROCTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PROCTEST=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcQualRecordMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsProcQualRecordMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_PROCTEST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PROCTEST,LENGTH(AUTO_KEY_PROCTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PROCTEST=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcQualRecordMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        cboFinalRemarks.Enabled = mMode
        txtApprovedBy.Enabled = mMode
        cmdSearchApprBy.Enabled = mMode
        txtQCInspector.Enabled = mMode
        cmdSearchQCInsp.Enabled = mMode
        txtProdSupervisor.Enabled = mMode
        cmdSearchProdSuper.Enabled = mMode
        txtManager.Enabled = mMode
        cmdSearchMan.Enabled = mMode
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMenuID)


        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnProcQualReco(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        MainClass.ClearCrptFormulas(Report1)

        mTitle = "Process Qualification Record"

        mSubTitle = " "

        SqlStr = " SELECT QAL_PROCTEST_HDR.*, QAL_PROCTEST_DET.*, " & vbCrLf & " APPR.EMP_NAME, QC.EMP_NAME, PRD_SUP.EMP_NAME, MGR.EMP_NAME " & vbCrLf & " FROM QAL_PROCTEST_HDR, QAL_PROCTEST_DET,  " & vbCrLf & " PAY_EMPLOYEE_MST APPR, PAY_EMPLOYEE_MST QC, PAY_EMPLOYEE_MST PRD_SUP, PAY_EMPLOYEE_MST MGR " & vbCrLf & " WHERE QAL_PROCTEST_HDR.AUTO_KEY_PROCTEST=QAL_PROCTEST_DET.AUTO_KEY_PROCTEST " & vbCrLf & " AND QAL_PROCTEST_HDR.COMPANY_CODE=APPR.COMPANY_CODE (+) " & vbCrLf & " AND QAL_PROCTEST_HDR.APPR_EMP_CODE=APPR.EMP_CODE (+) " & vbCrLf & " AND QAL_PROCTEST_HDR.COMPANY_CODE=QC.COMPANY_CODE (+) " & vbCrLf & " AND QAL_PROCTEST_HDR.QC_EMP_CODE=QC.EMP_CODE (+) " & vbCrLf & " AND QAL_PROCTEST_HDR.COMPANY_CODE=PRD_SUP.COMPANY_CODE (+) " & vbCrLf & " AND QAL_PROCTEST_HDR.PRD_SUP_EMP_CODE=PRD_SUP.EMP_CODE (+) " & vbCrLf & " AND QAL_PROCTEST_HDR.COMPANY_CODE=MGR.COMPANY_CODE (+) " & vbCrLf & " AND QAL_PROCTEST_HDR.MGR_EMP_CODE=MGR.EMP_CODE (+) " & vbCrLf & " AND QAL_PROCTEST_HDR.AUTO_KEY_PROCTEST=" & Val(lblMkey.Text) & " ORDER BY PROCESS_TYPE, SERIAL_NO "

        ShowReport(SqlStr, Mode, mTitle, mSubTitle, "ProcQualReco.rpt")



        Exit Sub
ERR1:
        MsgInformation(Err.Description)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProcQualReco(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProcQualReco(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
