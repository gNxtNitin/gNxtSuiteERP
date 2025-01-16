Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmActionPlan
    Inherits System.Windows.Forms.Form
    Dim RsActionPlan As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsActionPlan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtAreaDesc.Text = ""
        txtObserStatus.Text = ""
        txtActionsReq.Text = ""
        txtResponsibility.Text = ""
        txtTargetDate.Text = ""
        txtComplDate.Text = ""
        Call MakeEnableDeField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsActionPlan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        txtTargetDate.Enabled = mMode
        txtComplDate.Enabled = mMode
    End Sub
    Private Function CheckDate(ByRef pTxtDate As System.Windows.Forms.TextBox) As Boolean
        CheckDate = True
        If Trim(pTxtDate.Text) = "" Then Exit Function
        If Not IsDate(pTxtDate.Text) Then
            MsgBox("Not a Valid Date")
            CheckDate = False
        Else
            pTxtDate.Text = VB6.Format(pTxtDate.Text, "DD/MM/YYYY")
        End If

    End Function
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsActionPlan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ACTIONPLAN,LENGTH(AUTO_KEY_ACTIONPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster("", "QAL_ACTIONPLAN_TRN", "AUTO_KEY_ACTIONPLAN", "AREA_DESC", , , SqlStr) = True Then
            txtNumber.Text = AcName
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Call ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsActionPlan.EOF = False Then RsActionPlan.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        ''Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsActionPlan.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_ACTIONPLAN_TRN", (txtNumber.Text), RsActionPlan) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_ACTIONPLAN_TRN WHERE AUTO_KEY_ACTIONPLAN=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsActionPlan.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsActionPlan.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmActionPlan_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmActionPlan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub frmActionPlan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Action Plan"
        SqlStr = " Select * From QAL_ACTIONPLAN_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsActionPlan, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetTextLengths()
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume
    End Sub
    Private Sub frmActionPlan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(3855)
        Me.Width = VB6.TwipsToPixelsX(8295)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmActionPlan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsActionPlan.Close()
        RsActionPlan = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsActionPlan.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsActionPlan.Fields("AUTO_KEY_ACTIONPLAN").Value), "", RsActionPlan.Fields("AUTO_KEY_ACTIONPLAN").Value)
            txtNumber.Text = IIf(IsDbNull(RsActionPlan.Fields("AUTO_KEY_ACTIONPLAN").Value), "", RsActionPlan.Fields("AUTO_KEY_ACTIONPLAN").Value)
            txtAreaDesc.Text = IIf(IsDbNull(RsActionPlan.Fields("AREA_DESC").Value), "", RsActionPlan.Fields("AREA_DESC").Value)
            txtObserStatus.Text = IIf(IsDbNull(RsActionPlan.Fields("OBS_STATUS").Value), "", RsActionPlan.Fields("OBS_STATUS").Value)
            txtActionsReq.Text = IIf(IsDbNull(RsActionPlan.Fields("ACTION_REQ").Value), "", RsActionPlan.Fields("ACTION_REQ").Value)
            txtResponsibility.Text = IIf(IsDbNull(RsActionPlan.Fields("RESPONSIBILITY").Value), "", RsActionPlan.Fields("RESPONSIBILITY").Value)
            txtTargetDate.Text = IIf(IsDbNull(RsActionPlan.Fields("TARGET_DATE").Value), "", RsActionPlan.Fields("TARGET_DATE").Value)
            txtComplDate.Text = IIf(IsDbNull(RsActionPlan.Fields("COMPLETION_DATE").Value), "", RsActionPlan.Fields("COMPLETION_DATE").Value)
            Call MakeEnableDeField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsActionPlan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ACTIONPLAN)  " & vbCrLf & " FROM QAL_ACTIONPLAN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ACTIONPLAN,LENGTH(AUTO_KEY_ACTIONPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
            SqlStr = " INSERT INTO QAL_ACTIONPLAN_TRN " & vbCrLf _
                            & " (COMPANY_CODE,AUTO_KEY_ACTIONPLAN,AREA_DESC," & vbCrLf _
                            & " OBS_STATUS,ACTION_REQ,RESPONSIBILITY," & vbCrLf _
                            & " TARGET_DATE,COMPLETION_DATE, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & mSlipNo & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtAreaDesc.Text) & "','" & MainClass.AllowSingleQuote(txtObserStatus.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtActionsReq.Text) & "','" & MainClass.AllowSingleQuote(txtResponsibility.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtTargetDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtComplDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_ACTIONPLAN_TRN SET " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " AUTO_KEY_ACTIONPLAN=" & mSlipNo & ", " & vbCrLf _
                    & " AREA_DESC='" & MainClass.AllowSingleQuote(txtAreaDesc.Text) & "'," & vbCrLf _
                    & " OBS_STATUS='" & MainClass.AllowSingleQuote(txtObserStatus.Text) & "', " & vbCrLf _
                    & " ACTION_REQ='" & MainClass.AllowSingleQuote(txtActionsReq.Text) & "', " & vbCrLf _
                    & " RESPONSIBILITY='" & MainClass.AllowSingleQuote(txtResponsibility.Text) & "', " & vbCrLf _
                    & " TARGET_DATE=TO_DATE('" & VB6.Format(txtTargetDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " COMPLETION_DATE=TO_DATE('" & VB6.Format(txtComplDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_ACTIONPLAN =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsActionPlan.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtAreaDesc.Text) = "" Then
            MsgInformation("Area Description is empty, So unable to Save")
            txtAreaDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtObserStatus.Text) = "" Then
            MsgInformation("Observation / Status is empty, So unable to Save")
            txtObserStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtActionsReq.Text) = "" Then
            MsgInformation("Actions Required is empty, So unable to Save")
            txtActionsReq.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtResponsibility.Text) = "" Then
            MsgInformation("Responsibility is empty, So unable to Save")
            txtResponsibility.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtTargetDate.Text) = "" Then
            MsgInformation("Target Date is empty, So unable to Save")
            txtTargetDate.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsActionPlan.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT AUTO_KEY_ACTIONPLAN,AREA_DESC,OBS_STATUS, " & vbCrLf & " ACTION_REQ,RESPONSIBILITY,TO_CHAR(TARGET_DATE,'DD/MM/YYYY') AS TARGET_DATE, " & vbCrLf & " TO_CHAR(COMPLETION_DATE,'DD/MM/YYYY') AS COMPLETION_DATE " & vbCrLf & " FROM QAL_ACTIONPLAN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ACTIONPLAN,LENGTH(AUTO_KEY_ACTIONPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_ACTIONPLAN"
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()

    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Action Plan"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ActionPlan.rpt"

        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_ACTIONPLAN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_ACTIONPLAN=" & Val(lblMkey.Text) & " "

        Report1.SQLQuery = SqlStr
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtActionsReq_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionsReq.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAreaDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAreaDesc.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtObserStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtObserStatus.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtResponsibility_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtResponsibility.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTargetDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTargetDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTargetDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTargetDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtTargetDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtComplDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComplDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtComplDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtComplDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtComplDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsActionPlan.EOF = False Then xMKey = RsActionPlan.Fields("AUTO_KEY_ACTIONPLAN").Value
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_ACTIONPLAN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ACTIONPLAN,LENGTH(AUTO_KEY_ACTIONPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_ACTIONPLAN=" & Val(txtNumber.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsActionPlan, ADODB.LockTypeEnum.adLockReadOnly)
        If RsActionPlan.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xMKey = RsActionPlan.Fields("AUTO_KEY_ACTIONPLAN").Value
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Number Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM QAL_ACTIONPLAN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ACTIONPLAN,LENGTH(AUTO_KEY_ACTIONPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_ACTIONPLAN=" & xMKey & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsActionPlan, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtAreaDesc.Maxlength = RsActionPlan.Fields("AREA_DESC").DefinedSize
        txtObserStatus.Maxlength = RsActionPlan.Fields("OBS_STATUS").DefinedSize
        txtActionsReq.Maxlength = RsActionPlan.Fields("ACTION_REQ").DefinedSize
        txtResponsibility.Maxlength = RsActionPlan.Fields("RESPONSIBILITY").DefinedSize
        txtTargetDate.Maxlength = RsActionPlan.Fields("TARGET_DATE").DefinedSize - 6
        txtComplDate.Maxlength = RsActionPlan.Fields("COMPLETION_DATE").DefinedSize - 6
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 3)
            .set_ColWidth(2, 500 * 4)
            .set_ColWidth(3, 500 * 4)
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 4)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNumber.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
