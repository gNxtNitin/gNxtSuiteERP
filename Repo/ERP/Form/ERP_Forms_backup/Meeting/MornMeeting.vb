Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMornMeeting
    Inherits System.Windows.Forms.Form
    Dim RsMeeting As ADODB.Recordset
    'Dim PvtDBCn As ADODB.Connection						

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String
        SqlStr = ""
        SqlStr = "SELECT TO_CHAR(AUTO_KEY_NO,'000000') AS REFNO, TO_CHAR(RAISEDDATE,'DD/MM/YYYY') AS RAISEDDATE, " & vbCrLf _
            & " RAISEDBY_USERNAME, EXPECTEDBY_USERNAME, REMARKS, " & vbCrLf _
            & " TO_CHAR(CURRENTDATE,'DD/MM/YYYY') AS CURRENTDATE, DECODE(STATUS,'O','OPEN','CLOSED') AS STATUS " & vbCrLf _
            & " FROM PRD_MORNMEET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf _
            & " ORDER BY AUTO_KEY_NO "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 16)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .set_ColWidth(6, 12)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mExpectDate1 As String
        Dim mExpectDate2 As String
        Dim mExpectDate3 As String
        Dim mExpectDate4 As String
        Dim mExpectDate5 As String


        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsMeeting.EOF = True Then Exit Function

        '    If chkIsStatus.Value = vbChecked And MODIFYMode = True Then						
        '        MsgBox "Status Closed. Cann't be Modify", vbInformation						
        '        FieldsVarification = False						
        '        Exit Function						
        '    End If						


        If Trim(txtRaisedDate.Text) = "" Or Trim(txtRaisedDate.Text) = "__/__/____" Then
            MsgBox("Raised Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtRaisedDate.Enabled = True Then txtRaisedDate.Focus()
            Exit Function
        End If

        If Trim(txtRaisedBy.Text) = "" Then
            MsgBox("Raised By is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtRaisedBy.Enabled = True Then txtRaisedBy.Focus()
            Exit Function
        End If

        If Trim(txtExpectedBy.Text) = "" Then
            MsgBox("Expected By is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtExpectedBy.Enabled = True Then txtExpectedBy.Focus()
            Exit Function
        End If

        If Trim(txtExpectedDept.Text) = "" Then
            MsgBox("Expected Dept is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtExpectedDept.Enabled = True Then txtExpectedDept.Focus()
            Exit Function
        End If

        'If CDate(txtRaisedDate.Text) >= CDate("01/12/2008") Then
        '    If Trim(txtPointType.Text) = "" Then
        '        MsgBox("Point Type is Blank", MsgBoxStyle.Information)
        '        FieldsVarification = False
        '        If txtPointType.Enabled = True Then txtPointType.Focus()
        '        Exit Function
        '    End If
        'End If

        'If Trim(TxtRemarks.Text) = "" Then
        '    MsgBox("Remarks is Blank", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    TxtRemarks.Focus()
        '    Exit Function
        'End If

        '  If Trim(txtExpectedDate1.Text) = "" Or Trim(txtExpectedDate1.Text) = "__/__/____" Then
        If Not IsDate(txtExpectedDate1.Text) Then
            MsgBox("First Expected Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtExpectedDate1.Enabled = True Then txtExpectedDate1.Focus()
            Exit Function
        End If

        If CDate(txtRaisedDate.Text) > CDate(txtExpectedDate1.Text) Then
            MsgBox("Expected Date Cann't be Less Than Raised Date", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtExpectedDate1.Enabled = True Then txtExpectedDate1.Focus()
            Exit Function
        End If


        mExpectDate1 = IIf(IsDate(txtExpectedDate1), txtExpectedDate1, "") '' IIf(txtExpectedDate1.Text = "__/__/____", "", txtExpectedDate1.Text)
        mExpectDate2 = IIf(IsDate(txtExpectedDate2), txtExpectedDate2, "") '' IIf(txtExpectedDate2.Text = "__/__/____", "", txtExpectedDate2.Text)
        mExpectDate3 = IIf(IsDate(txtExpectedDate3), txtExpectedDate3, "") ''IIf(txtExpectedDate3.Text = "__/__/____", "", txtExpectedDate3.Text)
        mExpectDate4 = IIf(IsDate(txtExpectedDate4), txtExpectedDate4, "") '' IIf(txtExpectedDate4.Text = "__/__/____", "", txtExpectedDate4.Text)
        mExpectDate5 = IIf(IsDate(txtExpectedDate5), txtExpectedDate5, "") ''IIf(txtExpectedDate5.Text = "__/__/____", "", txtExpectedDate5.Text)

        If mExpectDate2 = "" Then
            If Trim(mExpectDate3) <> "" Then
                MsgBox("First Select Expected Date #2", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate2.Enabled = True Then txtExpectedDate2.Focus()
                Exit Function
            End If

            If Trim(mExpectDate4) <> "" Then
                MsgBox("First Select Expected Date #2", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate2.Enabled = True Then txtExpectedDate2.Focus()
                Exit Function
            End If

            If Trim(mExpectDate5) <> "" Then
                MsgBox("First Select Expected Date #2", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate2.Enabled = True Then txtExpectedDate2.Focus()
                Exit Function
            End If
        Else
            If CDate(txtExpectedDate1.Text) >= CDate(txtExpectedDate2.Text) Then
                MsgBox("Expect Date #1 Cann't be Greater Than Expected Date #2", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate2.Enabled = True Then txtExpectedDate2.Focus()
                Exit Function
            End If
        End If

        If mExpectDate3 = "" Then
            If Trim(mExpectDate4) <> "" Then
                MsgBox("First Select Expected Date #3", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate3.Enabled = True Then txtExpectedDate3.Focus()
                Exit Function
            End If

            If Trim(mExpectDate5) <> "" Then
                MsgBox("First Select Expected Date #3", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate3.Enabled = True Then txtExpectedDate3.Focus()
                Exit Function
            End If
        Else
            If CDate(txtExpectedDate2.Text) >= CDate(txtExpectedDate3.Text) Then
                MsgBox("Expect Date #2 Cann't be Greater Than Expected Date #3", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate3.Enabled = True Then txtExpectedDate3.Focus()
                Exit Function
            End If
        End If

        If mExpectDate4 = "" Then
            If Trim(mExpectDate5) <> "" Then
                MsgBox("First Select Expected Date #4", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate4.Enabled = True Then txtExpectedDate4.Focus()
                Exit Function
            End If
        Else
            If CDate(txtExpectedDate3.Text) >= CDate(txtExpectedDate4.Text) Then
                MsgBox("Expect Date #3 Cann't be Greater Than Expected Date #4", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate4.Enabled = True Then txtExpectedDate4.Focus()
                Exit Function
            End If
        End If

        If mExpectDate5 <> "" Then
            If CDate(txtExpectedDate4.Text) >= CDate(txtExpectedDate5.Text) Then
                MsgBox("Expect Date #4 Cann't be Greater Than Expected Date #5", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtExpectedDate5.Enabled = True Then txtExpectedDate5.Focus()
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub chkAllDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDept.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkIsStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkIsStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        '    Resume						
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If Trim(txtNumber.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsMeeting.EOF Then
            If RsMeeting.Fields("Status").Value = "C" Then MsgBox("Status has been closed , So cann't be deleted") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_MORNMEET_TRN", (txtNumber.Text), RsMeeting) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_MORNMEET_TRN", "AUTO_KEY_NO", (txtNumber.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PRD_MORNMEET_TRN WHERE AUTO_KEY_NO=" & Val(txtNumber.Text) & "")
                PubDBCn.CommitTrans()
                RsMeeting.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsMeeting.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub


    Private Sub cmdSearchExpectedDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchExpectedDept.Click
        On Error GoTo SrchERR
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtExpectedDept.Text = AcName1
            lblExpectedDept.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            If RsMeeting.Fields("Status").Value = "C" Then MsgBox("Status has been closed , So cann't be modified") : Exit Sub
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMeeting, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Morning Meeting Mintues " & IIf(lblBookType.Text = "E", "(Export)", "")

        SqlStr = "SELECT IH.* " & vbCrLf & " FROM PRD_MORNMEET_TRN IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND IH.AUTO_KEY_NO=" & Val(txtNumber.Text) & ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ParamMornMeet.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
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
        If Err.Description = "" Then Exit Sub
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchExpectedBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchExpectedBy.Click
        Call SearchEmp(txtExpectedBy, lblExpectedBy)
    End Sub
    Private Sub SearchEmp(ByRef ptxt As System.Windows.Forms.TextBox, ByRef pLbl As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND STATUS='O'"

        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "EMP_NAME", "USER_ID", , , SqlStr) = True Then
            ptxt.Text = AcName1
            pLbl.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchPT_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPT.Click
        On Error GoTo SrchERR
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PRD_MORN_PT_MST", "NAME", "CODE", , , SqlStr) = True Then
            txtPointType.Text = AcName1
            lblPointType.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchRaisedBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRaisedBy.Click
        Call SearchEmp(txtRaisedBy, lblRaisedBy)
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub frmMornMeeting_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String
        If FormActive = True Then Exit Sub

        Me.Text = "Morning Meeting Minutes " & IIf(lblBookType.Text = "E", "(Export)", "")

        SqlStr = ""
        SqlStr = "Select * from PRD_MORNMEET_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMeeting, ADODB.LockTypeEnum.adLockReadOnly)


        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SetTextLengths()

        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmMornMeeting_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmMornMeeting_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmMornMeeting_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim mDocNo As String
        Dim mDateOrg As String
        Dim mRevNo As String
        Dim mDateRev As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection						
        'PvtDBCn.Open StrConn						

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(6540)
        Me.Width = VB6.TwipsToPixelsX(8865)

        txtRaisedDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsMeeting

            txtNumber.MaxLength = .Fields("AUTO_KEY_NO").Precision
            txtRaisedDate.MaxLength = .Fields("RAISEDDATE").DefinedSize - 6
            txtRaisedBy.MaxLength = .Fields("RAISEDBY_USERID").DefinedSize
            txtExpectedBy.MaxLength = .Fields("EXPECTEDBY_USERID").DefinedSize
            txtExpectedDept.MaxLength = .Fields("EXPECTDEPT").DefinedSize
            TxtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
            txtPointType.MaxLength = MainClass.SetMaxLength("CODE", "PRD_MORN_PT_MST", PubDBCn)
            txtNarration.MaxLength = .Fields("NARRATION").DefinedSize
            txtExpectedDate1.MaxLength = .Fields("EXPECTED_DATE1").DefinedSize - 6
            txtExpectedDate2.MaxLength = .Fields("EXPECTED_DATE2").DefinedSize - 6
            txtExpectedDate3.MaxLength = .Fields("EXPECTED_DATE3").DefinedSize - 6
            txtExpectedDate4.MaxLength = .Fields("EXPECTED_DATE4").DefinedSize - 6
            txtExpectedDate5.MaxLength = .Fields("EXPECTED_DATE5").DefinedSize - 6

        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume						
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        chkIsStatus.Enabled = IIf(chkIsStatus.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        txtRaisedDate.Enabled = mMode

        If chkIsStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtExpectedDate1.Enabled = False
            txtExpectedDate2.Enabled = False
            txtExpectedDate3.Enabled = False
            txtExpectedDate4.Enabled = False
            txtExpectedDate5.Enabled = False
        Else
            txtExpectedDate1.Enabled = IIf(IsDate(txtExpectedDate1.Text), False, True) '' IIf(txtExpectedDate2.Text = "__/__/____", True, False)
            txtExpectedDate2.Enabled = IIf(IsDate(txtExpectedDate2.Text), False, True) '' IIf(txtExpectedDate3.Text = "__/__/____", True, False)
            txtExpectedDate3.Enabled = IIf(IsDate(txtExpectedDate3.Text), False, True) ''IIf(txtExpectedDate4.Text = "__/__/____", True, False)
            txtExpectedDate4.Enabled = IIf(IsDate(txtExpectedDate4.Text), False, True) ''IIf(txtExpectedDate5.Text = "__/__/____", True, False)

            If Not IsDate(txtExpectedDate1.Text) Then '' txtExpectedDate1.Text = "__/__/____" Then
                txtExpectedDate2.Enabled = False
                txtExpectedDate3.Enabled = False
                txtExpectedDate4.Enabled = False
                txtExpectedDate5.Enabled = False
            End If

            If Not IsDate(txtExpectedDate2.Text) Then ''If txtExpectedDate2.Text = "__/__/____" Then
                txtExpectedDate3.Enabled = False
                txtExpectedDate4.Enabled = False
                txtExpectedDate5.Enabled = False
            End If

            If Not IsDate(txtExpectedDate3.Text) Then '' If txtExpectedDate3.Text = "__/__/____" Then
                txtExpectedDate4.Enabled = False
                txtExpectedDate5.Enabled = False
            End If

            If Not IsDate(txtExpectedDate4.Text) Then '' If txtExpectedDate4.Text = "__/__/____" Then
                txtExpectedDate5.Enabled = False
            Else
                txtExpectedDate5.Enabled = True
            End If

        End If


    End Sub
    Private Sub frmMornMeeting_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsMeeting.Close()
        'PvtDBCn.Close						
        RsMeeting = Nothing
        'Set PvtDBCn = Nothing						
    End Sub
    Private Sub Clear1()


        txtNumber.Text = ""
        txtRaisedDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtRaisedBy.Text = ""
        txtExpectedBy.Text = ""
        txtExpectedDept.Text = ""
        TxtRemarks.Text = ""
        txtNarration.Text = ""
        lblPointType.Text = ""
        txtPointType.Text = ""

        txtExpectedDate1.Text = "__/__/____"
        txtExpectedDate2.Text = "__/__/____"
        txtExpectedDate3.Text = "__/__/____"
        txtExpectedDate4.Text = "__/__/____"
        txtExpectedDate5.Text = "__/__/____"

        lblRaisedBy.Text = ""
        lblExpectedBy.Text = ""
        lblExpectedDept.Text = ""
        chkIsStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtExpectedBy.Enabled = True
        cmdSearchExpectedBy.Enabled = True

        txtRaisedBy.Enabled = True
        cmdSearchRaisedBy.Enabled = True

        txtExpectedDept.Enabled = True
        cmdSearchExpectedDept.Enabled = True
        Call MakeEnableDesableField(True)

        MainClass.ButtonStatus(Me, XRIGHT, RsMeeting, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        With RsMeeting
            If Not .EOF Then
                txtNumber.Text = IIf(IsDBNull(.Fields("AUTO_KEY_NO").Value), "", .Fields("AUTO_KEY_NO").Value)
                txtNumber.Text = VB6.Format(txtNumber.Text, "00000")

                txtRaisedDate.Text = IIf(IsDBNull(.Fields("RAISEDDATE").Value), "__/__/____", .Fields("RAISEDDATE").Value)

                '            txtRaisedBy.Text = IIf(IsNull(.Fields("RAISEDBY")), "", .Fields("RAISEDBY"))						
                '            If MainClass.ValidateWithMasterTable(txtRaisedBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then						
                '                lblRaisedBy.Caption = MasterNo						
                '            Else						
                '                lblRaisedBy.Caption = ""						
                '            End If						
                '						
                '            txtExpectedBy.Text = IIf(IsNull(.Fields("EXPECTEDBY")), "", .Fields("EXPECTEDBY"))						
                '            If MainClass.ValidateWithMasterTable(txtExpectedBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then						
                '                lblExpectedBy.Caption = MasterNo						
                '            Else						
                '                lblExpectedBy.Caption = ""						
                '            End If						


                txtRaisedBy.Text = IIf(IsDBNull(.Fields("RAISEDBY_USERID").Value), "", .Fields("RAISEDBY_USERID").Value)
                lblRaisedBy.Text = IIf(IsDBNull(.Fields("RAISEDBY_USERNAME").Value), "", .Fields("RAISEDBY_USERNAME").Value)

                txtExpectedBy.Text = IIf(IsDBNull(.Fields("EXPECTEDBY_USERID").Value), "", .Fields("EXPECTEDBY_USERID").Value)
                lblExpectedBy.Text = IIf(IsDBNull(.Fields("EXPECTEDBY_USERNAME").Value), "", .Fields("EXPECTEDBY_USERNAME").Value)

                txtExpectedDept.Text = IIf(IsDBNull(.Fields("EXPECTDEPT").Value), "", .Fields("EXPECTDEPT").Value)
                If MainClass.ValidateWithMasterTable(txtExpectedDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblExpectedDept.Text = MasterNo
                Else
                    lblExpectedDept.Text = ""
                End If




                TxtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtExpectedDate1.Text = IIf(IsDBNull(.Fields("EXPECTED_DATE1").Value), "__/__/____", .Fields("EXPECTED_DATE1").Value)
                txtExpectedDate2.Text = IIf(IsDBNull(.Fields("EXPECTED_DATE2").Value), "__/__/____", .Fields("EXPECTED_DATE2").Value)
                txtExpectedDate3.Text = IIf(IsDBNull(.Fields("EXPECTED_DATE3").Value), "__/__/____", .Fields("EXPECTED_DATE3").Value)
                txtExpectedDate4.Text = IIf(IsDBNull(.Fields("EXPECTED_DATE4").Value), "__/__/____", .Fields("EXPECTED_DATE4").Value)
                txtExpectedDate5.Text = IIf(IsDBNull(.Fields("EXPECTED_DATE5").Value), "__/__/____", .Fields("EXPECTED_DATE5").Value)
                chkIsStatus.CheckState = IIf(.Fields("STATUS").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkAllDept.CheckState = IIf(.Fields("ALL_DEPT").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                lblPointType.Text = IIf(IsDBNull(.Fields("POINT_TYPE").Value), "", .Fields("POINT_TYPE").Value)

                If MainClass.ValidateWithMasterTable(lblPointType.Text, "NAME", "CODE", "PRD_MORN_PT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtPointType.Text = MasterNo
                Else
                    txtPointType.Text = ""
                End If

                txtNarration.Text = IIf(IsDBNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)


                txtExpectedBy.Enabled = True ''false						
                cmdSearchExpectedBy.Enabled = True ''False						

                txtRaisedBy.Enabled = True ''False						
                cmdSearchRaisedBy.Enabled = True ''False						

                txtExpectedDept.Enabled = True ''False						
                cmdSearchExpectedDept.Enabled = True '' False						

                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsMeeting, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        txtNumber.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub


    Private Function AutoGenNumber() As String

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_NO)  " & vbCrLf _
            & " FROM PRD_MORNMEET_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mAutoGen = .Fields(0).Value
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenNumber = VB6.Format(mAutoGen, "00000")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mRefNumber As String
        Dim mStatus As String
        Dim mCurrentDate As String
        Dim mExpectDate1 As String
        Dim mExpectDate2 As String
        Dim mExpectDate3 As String
        Dim mExpectDate4 As String
        Dim mExpectDate5 As String
        Dim mAllDept As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mRefNumber = Trim(txtNumber.Text)
        If Trim(txtNumber.Text) = "" Then
            mRefNumber = AutoGenNumber()
        End If

        txtNumber.Text = mRefNumber

        mStatus = IIf(chkIsStatus.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O")
        mAllDept = IIf(chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mExpectDate1 = IIf(IsDate(txtExpectedDate1.Text), txtExpectedDate1.Text, "") '' IIf(txtExpectedDate1.Text = "__/__/____", "", txtExpectedDate1.Text)
        mExpectDate2 = IIf(IsDate(txtExpectedDate2.Text), txtExpectedDate2.Text, "") ''IIf(txtExpectedDate2.Text = "__/__/____", "", txtExpectedDate2.Text)
        mExpectDate3 = IIf(IsDate(txtExpectedDate3.Text), txtExpectedDate3.Text, "") '' IIf(txtExpectedDate3.Text = "__/__/____", "", txtExpectedDate3.Text)
        mExpectDate4 = IIf(IsDate(txtExpectedDate4.Text), txtExpectedDate4.Text, "") '' IIf(txtExpectedDate4.Text = "__/__/____", "", txtExpectedDate4.Text)
        mExpectDate5 = IIf(IsDate(txtExpectedDate5.Text), txtExpectedDate5.Text, "") '' IIf(txtExpectedDate5.Text = "__/__/____", "", txtExpectedDate5.Text)


        If Not IsDate(txtExpectedDate5.Text) Then 'txtExpectedDate5.Text = "__/__/____" Then
            If Not IsDate(txtExpectedDate4.Text) Then ' txtExpectedDate4.Text = "__/__/____" Then
                If Not IsDate(txtExpectedDate3.Text) Then 'txtExpectedDate3.Text = "__/__/____" Then
                    If Not IsDate(txtExpectedDate2.Text) Then 'txtExpectedDate2.Text = "__/__/____" Then
                        mCurrentDate = txtExpectedDate1.Text
                    Else
                        mCurrentDate = txtExpectedDate2.Text
                    End If
                Else
                    mCurrentDate = txtExpectedDate3.Text
                End If
            Else
                mCurrentDate = txtExpectedDate4.Text
            End If
        Else
            mCurrentDate = txtExpectedDate5.Text
        End If

        SqlStr = ""
        If ADDMode = True Then

            SqlStr = " INSERT INTO PRD_MORNMEET_TRN (" & vbCrLf _
                & " COMPANY_CODE, AUTO_KEY_NO, " & vbCrLf _
                & " RAISEDDATE, RAISEDBY_USERID, RAISEDBY_USERNAME, " & vbCrLf _
                & " EXPECTEDBY_USERID, EXPECTEDBY_USERNAME, EXPECTDEPT," & vbCrLf _
                & " REMARKS, EXPECTED_DATE1, " & vbCrLf _
                & " EXPECTED_DATE2, EXPECTED_DATE3, " & vbCrLf _
                & " EXPECTED_DATE4, EXPECTED_DATE5, " & vbCrLf _
                & " CURRENTDATE, STATUS, ALL_DEPT, BOOKTYPE, " & vbCrLf _
                & " ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE, POINT_TYPE, NARRATION" & vbCrLf _
                & " ) VALUES ("

            SqlStr = SqlStr & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(txtNumber.Text) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtRaisedDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtRaisedBy.Text) & "', '" & MainClass.AllowSingleQuote(lblRaisedBy.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtExpectedBy.Text) & "', '" & MainClass.AllowSingleQuote(lblExpectedBy.Text) & "', '" & MainClass.AllowSingleQuote(txtExpectedDept.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "', TO_DATE('" & VB6.Format(mExpectDate1, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mExpectDate2, "DD/MMM/YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(mExpectDate3, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mExpectDate4, "DD/MMM/YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(mExpectDate5, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mCurrentDate, "DD/MMM/YYYY") & "','DD-MON-YYYY'), '" & mStatus & "', '" & mAllDept & "', '" & lblBookType.Text & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '', '', '" & MainClass.AllowSingleQuote(lblPointType.Text) & "','" & MainClass.AllowSingleQuote(txtNarration.Text) & "' )"


        ElseIf MODIFYMode = True Then
            SqlStr = ""

            SqlStr = " UPDATE PRD_MORNMEET_TRN SET " & vbCrLf _
               & " RAISEDDATE=TO_DATE('" & VB6.Format(txtRaisedDate.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
               & " RAISEDBY_USERID='" & MainClass.AllowSingleQuote(txtRaisedBy.Text) & "', " & vbCrLf _
               & " RAISEDBY_USERNAME='" & MainClass.AllowSingleQuote(lblRaisedBy.Text) & "', " & vbCrLf _
               & " EXPECTEDBY_USERID='" & MainClass.AllowSingleQuote(txtExpectedBy.Text) & "', " & vbCrLf _
               & " EXPECTEDBY_USERNAME='" & MainClass.AllowSingleQuote(lblExpectedBy.Text) & "', " & vbCrLf _
               & " EXPECTDEPT='" & MainClass.AllowSingleQuote(txtExpectedDept.Text) & "', " & vbCrLf _
               & " REMARKS='" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "', " & vbCrLf _
               & " EXPECTED_DATE1=TO_DATE('" & VB6.Format(mExpectDate1, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
               & " EXPECTED_DATE2=TO_DATE('" & VB6.Format(mExpectDate2, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
               & " EXPECTED_DATE3=TO_DATE('" & VB6.Format(mExpectDate3, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
               & " EXPECTED_DATE4=TO_DATE('" & VB6.Format(mExpectDate4, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
               & " EXPECTED_DATE5=TO_DATE('" & VB6.Format(mExpectDate5, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
               & " CURRENTDATE=TO_DATE('" & VB6.Format(mCurrentDate, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
               & " STATUS ='" & mStatus & "', " & vbCrLf _
               & " BOOKTYPE='" & lblBookType.Text & "', " & vbCrLf _
               & " ALL_DEPT ='" & mAllDept & "', " & vbCrLf _
               & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
               & " MODDATE=TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
               & " POINT_TYPE='" & MainClass.AllowSingleQuote(lblPointType.Text) & "'," & vbCrLf _
               & " NARRATION='" & MainClass.AllowSingleQuote(txtNarration.Text) & "'" & vbCrLf _
               & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
               & " AND AUTO_KEY_NO=" & Val(txtNumber.Text) & " "
        End If
        PubDBCn.Execute(SqlStr)
        txtNumber.Text = mRefNumber
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsMeeting.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh						
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsMeeting, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtNumber.Text = SprdView.Text

        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtExpectedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExpectedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedBy.DoubleClick
        Call cmdSearchExpectedBy_Click(cmdSearchExpectedBy, New System.EventArgs())
    End Sub

    Private Sub txtExpectedBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExpectedBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExpectedBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExpectedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtExpectedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchExpectedBy_Click(cmdSearchExpectedBy, New System.EventArgs())
    End Sub

    Private Sub txtExpectedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpectedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtExpectedBy, lblExpectedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtExpectedDate1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedDate1.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExpectedDate1_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpectedDate1.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'If Trim(txtExpectedDate1.Text) = "" Or Trim(txtExpectedDate1.Text) = "__/__/____" Then GoTo EventExitSub
        If Trim(txtExpectedDate1.Text) = "" Or Trim(txtExpectedDate1.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtExpectedDate1.Text) Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtExpectedDate2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedDate2.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtExpectedDate2_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpectedDate2.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtExpectedDate2.Text) = "" Or Trim(txtExpectedDate2.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtExpectedDate2.Text) Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtExpectedDate3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedDate3.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtExpectedDate3_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpectedDate3.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtExpectedDate3.Text) = "" Or Trim(txtExpectedDate3.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtExpectedDate3.Text) Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtExpectedDate4_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedDate4.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtExpectedDate4_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpectedDate4.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtExpectedDate4.Text) = "" Or Trim(txtExpectedDate4.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtExpectedDate4.Text) Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtExpectedDate5_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedDate5.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtExpectedDate5_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpectedDate5.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtExpectedDate5.Text) = "" Or Trim(txtExpectedDate5.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtExpectedDate5.Text) Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtExpectedDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedDept.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExpectedDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExpectedDept.DoubleClick
        Call cmdSearchExpectedDept_Click(cmdSearchExpectedDept, New System.EventArgs())
    End Sub


    Private Sub txtExpectedDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExpectedDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExpectedDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtExpectedDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtExpectedDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchExpectedDept_Click(cmdSearchExpectedDept, New System.EventArgs())
    End Sub

    Private Sub txtExpectedDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtExpectedDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtExpectedDept.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtExpectedDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Dept Does Not Exist In Master.")
            Cancel = True
        Else
            lblExpectedDept.Text = MasterNo
        End If

        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNarration_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNarration.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNarration_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNarration.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNarration.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPointType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPointType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPointType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPointType.DoubleClick
        Call cmdSearchPT_Click(cmdSearchPT, New System.EventArgs())
    End Sub


    Private Sub txtPointType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPointType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPointType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPointType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPointType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPT_Click(cmdSearchPT, New System.EventArgs())
    End Sub

    Private Sub txtPointType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPointType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtPointType.Text) = "" Then
            lblPointType.Text = ""
            GoTo EventExitSub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtPointType.Text, "CODE", "NAME", "PRD_MORN_PT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Name does not Exist In Master.")
            Cancel = True
        Else
            lblPointType.Text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRaisedBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRaisedBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRaisedBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRaisedDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRaisedDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRaisedDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRaisedDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRaisedDate.Text) = "" Or Trim(txtRaisedDate.Text) = "__/__/____" Then GoTo EventExitSub
        If Not IsDate(txtRaisedDate.Text) Then
            MsgBox("Not a valid date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Public Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If ShowRecord() = False Then Cancel = True
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRaisedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRaisedBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRaisedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRaisedBy.DoubleClick
        Call cmdSearchRaisedBy_Click(cmdSearchRaisedBy, New System.EventArgs())
    End Sub

    Private Sub txtRaisedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRaisedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchRaisedBy_Click(cmdSearchRaisedBy, New System.EventArgs())
    End Sub

    Private Sub txtRaisedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRaisedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtRaisedBy, lblRaisedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub
    Private Function ValidateEMP(ByRef ptxt As System.Windows.Forms.TextBox, ByRef pLbl As System.Windows.Forms.Label) As Boolean
        On Error GoTo ValEMP
        Dim SqlStr As String
        ValidateEMP = True
        If Trim(ptxt.Text) = "" Then Exit Function
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND STATUS='O'"

        If MainClass.ValidateWithMasterTable(ptxt.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pLbl.Text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim xMkey As Double

        ShowRecord = True
        If Trim(txtNumber.Text) = "" Then Exit Function

        If MODIFYMode = True And RsMeeting.EOF = False Then xMkey = RsMeeting.Fields("AUTO_KEY_NO").Value

        SqlStr = " SELECT * FROM PRD_MORNMEET_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND AUTO_KEY_NO=" & Val(txtNumber.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMeeting, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMeeting.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such No. Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then

                SqlStr = "SELECT * FROM PRD_MORNMEET_TRN " & vbCrLf & " WHERE AUTO_KEY_NO=" & Val(CStr(xMkey)) & " " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMeeting, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
End Class
