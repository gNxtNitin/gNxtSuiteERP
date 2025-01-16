Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPayMovementSlip
    Inherits System.Windows.Forms.Form
    Dim RsEmpMove As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim xRefNo As Double
    Dim mIsAuthorisedUser As Boolean
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsEmpMove, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        txtRefNo.Text = ""
        '    txtRefDate.Text = Format(RunDate, "DD/MM/YYYY")
        '    txtRefDateTo.Text = Format(RunDate, "DD/MM/YYYY")
        txtRefDateTo.Enabled = True
        txtEmpCode.Text = ""
        TxtEmpName.Text = ""
        txtDept.Text = ""
        txtPlace.Text = ""
        txtFrom.Text = "__:__"
        txtTo.Text = "__:__"
        txtTotalHrs.Text = "__:__"
        txtAthCode.Text = ""
        txtDistance.Text = ""
        'txtOTHr.Text = ""
        txtOTThisMonth.Text = ""

        cboVisitedFrom.SelectedIndex = -1
        cboVehicle.SelectedIndex = -1
        chkHRApproval.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboFHalf.SelectedIndex = -1
        cboSHalf.SelectedIndex = -1

        chkAgtOT.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtOTHr.Text = ""
        txtOTHr.Enabled = False
        'If lblMovementType.Text = "A" Then
        '    optMoveType(0).Enabled = False
        '    optMoveType(1).Enabled = False
        '    optMoveType(2).Enabled = True
        '    optMoveType(2).Checked = True
        'Else
        optMoveType(0).Enabled = True
        optMoveType(1).Enabled = True
        optMoveType(2).Enabled = True
        optMoveType(0).Checked = True
        'End If

        If lblBookType.Text = "U" Then
            chkHRApproval.Enabled = False
        Else
            txtRefDateTo.Enabled = False
            txtEmpCode.Enabled = False
            TxtEmpName.Enabled = False
            txtDept.Enabled = False
            txtPlace.Enabled = False
            txtFrom.Enabled = False
            txtTo.Enabled = False
            txtTotalHrs.Enabled = False
            txtAthCode.Enabled = False
            optType.Enabled = False

            cboVisitedFrom.Enabled = False
            cboVehicle.Enabled = False
            chkHRApproval.Enabled = True
            cmdAthSearch.Enabled = False
            cmdSearch.Enabled = False
            txtDistance.Enabled = False
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsEmpMove, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVehicle.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVehicle_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVehicle.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVisitedFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVisitedFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboVisitedFrom_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboVisitedFrom.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAgainstLeave_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAgainstLeave.CheckStateChanged

        On Error GoTo ErrPart
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        If optMoveType(1).Checked = True And chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ShowLeave = False Then GoTo ErrPart
        End If
        Exit Sub
ErrPart:

    End Sub

    Private Function ShowLeave() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFH As Double
        Dim mSH As Double

        ShowLeave = False
        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & Trim(txtEmpCode.Text) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mFH = IIf(IsDbNull(RsTemp.Fields("FIRSTHALF").Value), -1, RsTemp.Fields("FIRSTHALF").Value)
            mSH = IIf(IsDbNull(RsTemp.Fields("SECONDHALF").Value), -1, RsTemp.Fields("SECONDHALF").Value)

            If mFH = 10 Then

            Else
                cboFHalf.SelectedIndex = mFH
            End If
            If mFH = 10 Then

            Else
                cboSHalf.SelectedIndex = mSH
            End If

        End If
        ShowLeave = True
        Exit Function
ErrPart:
        ShowLeave = False
    End Function
    Private Sub chkHRApproval_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHRApproval.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAthSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAthSearch.Click
        Dim SqlStr As String = ""
        Dim mDOJ As String
        Dim mDOL As String

        mDOJ = VB6.Format(txtRefDate.Text, "DD/MM/YYYY") ''MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
        mDOL = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE=1" & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE<>'" & PubUserEMPCode & "'"
        End If

        If MainClass.SearchGridMaster((txtAthCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtAthCode.Text = AcName1
            txtAthCode_Validating(txtAthCode, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsEmpMove, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function FillEmpINTimeOut() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCode As String
        Dim mDate As String

        FillEmpINTimeOut = False

        mCode = VB6.Format(txtEmpCode.Text, "000000")
        mDate = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")

        If Trim(mCode) = "" Or Trim(mDate) = "" Then Exit Function

        If ADDMode = True Then
            SqlStr = "SELECT * FROM PAY_DALIY_ATTN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                txtFrom.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "hh:mm")
                txtTo.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "hh:mm")
            End If
        End If

        FillEmpINTimeOut = True

        Exit Function


ERR1:
        FillEmpINTimeOut = False
        MsgInformation(Err.Description)
    End Function

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim SqlStr As String = ""
        Dim mDOJ As String
        Dim mDOL As String

        mDOJ = VB6.Format(txtRefDate.Text, "DD/MM/YYYY") ''MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
        mDOL = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")

        SqlStr = "SELECT EMP_NAME, EMP_CODE, DEPT_DESC, WORKING_HOURS"

        SqlStr = SqlStr & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_DEPT_MST DEPT" & vbCrLf


        SqlStr = SqlStr & vbCrLf _
            & " WHERE EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP.EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP.EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP.EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " AND EMP.COMPANY_CODE=DEPT.COMPANY_CODE AND EMP.EMP_DEPT_CODE=DEPT.DEPT_CODE"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP.EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        'If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", "EMP_DEPT_CODE", , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2(txtEmpCode.Text, SqlStr) = True Then
            txtEmpCode.Text = AcName1
            TxtEmpName.Text = AcName
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            txtRefNo.Enabled = False
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsEmpMove.EOF = False Then RsEmpMove.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        '    If txtTDSName.Text = "" Then MsgExclamation "Nothing to delete": Exit Sub

        If chkHRApproval.CheckState = System.Windows.Forms.CheckState.Checked And chkHRApproval.Enabled = False Then
            MsgInformation("Slip already Approved. Cann't be Deleted")
            Exit Sub
        End If

        If lblBookType.Text = "H" Then
            MsgInformation("Cann't be Deleted")
            Exit Sub
        End If

        If Not RsEmpMove.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsEmpMove.EOF = True Then
                    Clear1()
                Else
                    Clear1()
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub

    Private Sub frmPayMovementSlip_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmPayMovementSlip_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub optMoveType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optMoveType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optMoveType.GetIndex(eventSender)

            On Error GoTo ErrPart
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)

            If Index = 0 Then  ''optMoveType(0).Checked = True
                Label11.Visible = True
                cboVisitedFrom.Visible = True
                Label12.Visible = True
                cboVehicle.Visible = True
                Label13.Visible = True
                txtDistance.Visible = True


                Label5.Text = "Place to visit :"

                Frame1.Visible = False
            Else
                Label11.Visible = False
                cboVisitedFrom.Visible = False
                Label12.Visible = False
                cboVehicle.Visible = False
                Label13.Visible = False
                txtDistance.Visible = False


                Label5.Text = "Reason :"
                Frame1.Visible = True

                If Index = 1 And chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Checked Then ''optMoveType(1).Checked = True
                    If ShowLeave() = False Then GoTo ErrPart
                End If
            End If



            If optMoveType(2).Checked = True Then
                If FillEmpINTimeOut() = False Then GoTo ErrPart
            End If
            Exit Sub
ErrPart:

        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = 1
        txtRefNo.Text = VB6.Format(SprdView.Text, "000000")

        txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Public Sub frmPayMovementSlip_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub


        SqlStr = "SELECT * FROM PAY_MOVEMENT_TRN WHERE  1<>1"

        'If lblBookType.Text = "U" Then
        '    Me.Text = "Movement Slip"
        'Else
        '    Me.Text = "Movement Slip HR Approval"
        'End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpMove, ADODB.LockTypeEnum.adLockReadOnly)
        Clear1()

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()

        Show1()

        If RsEmpMove.EOF = True Then
            If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        End If
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmPayMovementSlip_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)

        If InStr(1, XRIGHT, "S") > 0 Then
            mIsAuthorisedUser = True
        End If

        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(5475)
        Me.Width = VB6.TwipsToPixelsX(8355)

        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtRefDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        cboVisitedFrom.Items.Clear()
        cboVehicle.Items.Clear()

        cboVisitedFrom.Items.Add("1. N/A")
        cboVisitedFrom.Items.Add("2. Office")
        cboVisitedFrom.Items.Add("3. Home")
        cboVisitedFrom.Items.Add("4. Others")
        cboVisitedFrom.SelectedIndex = -1

        cboVehicle.Items.Add("1. N/A")
        cboVehicle.Items.Add("2. Two Wheeler")
        cboVehicle.Items.Add("3. Four Wheeler")
        cboVehicle.Items.Add("4. Self Paid Cab")
        cboVehicle.Items.Add("5. Company Paid Cab")
        cboVehicle.Items.Add("6. Office Cab")
        cboVehicle.Items.Add("7. Others")

        cboVehicle.SelectedIndex = -1

        cboFHalf.Items.Add("")
        cboFHalf.Items.Add("0 -APPROVED LEAVE")
        cboFHalf.Items.Add("1 -CASUAL")
        cboFHalf.Items.Add("2 -EARN")
        cboFHalf.Items.Add("3 -SICK")
        cboFHalf.Items.Add("4 -MATERNITY")
        cboFHalf.Items.Add("5 -CPLEARN")
        cboFHalf.Items.Add("6 -UNAPPROVED LEAVE")
        cboFHalf.Items.Add("7 -CPLAVAIL")
        cboFHalf.Items.Add("8 -SUNDAY")
        cboFHalf.Items.Add("9 -HOLIDAY")
        '    cboFHalf.AddItem "10 -PRESENT"
        cboFHalf.SelectedIndex = -1

        cboSHalf.Items.Add("")
        cboSHalf.Items.Add("0 -APPROVED LEAVE")
        cboSHalf.Items.Add("1 -CASUAL")
        cboSHalf.Items.Add("2 -EARN")
        cboSHalf.Items.Add("3 -SICK")
        cboSHalf.Items.Add("4 -MATERNITY")
        cboSHalf.Items.Add("5 -CPLEARN")
        cboSHalf.Items.Add("6 -UNAPPROVED LEAVE")
        cboSHalf.Items.Add("7 -CPLAVAIL")
        cboSHalf.Items.Add("8 -SUNDAY")
        cboSHalf.Items.Add("9 -HOLIDAY")
        '    cboSHalf.AddItem "10 -PRESENT"
        cboSHalf.SelectedIndex = -1

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPayMovementSlip_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsEmpMove = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mMoveType As String
        Dim mVisitFrom As Integer
        Dim mVehicle As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFH As Integer
        Dim mSH As Integer

        If Not RsEmpMove.EOF Then

            txtRefNo.Text = IIf(IsDbNull(RsEmpMove.Fields("AUTO_KEY_NO").Value), "", RsEmpMove.Fields("AUTO_KEY_NO").Value)
            txtRefNo.Text = VB6.Format(txtRefNo.Text, "00000")

            txtRefDate.Text = IIf(IsDbNull(RsEmpMove.Fields("REF_DATE").Value), "", RsEmpMove.Fields("REF_DATE").Value)
            txtRefDateTo.Text = IIf(IsDbNull(RsEmpMove.Fields("REF_DATE").Value), "", RsEmpMove.Fields("REF_DATE").Value)

            txtEmpCode.Text = IIf(IsDbNull(RsEmpMove.Fields("EMP_CODE").Value), "", RsEmpMove.Fields("EMP_CODE").Value)
            mEmpCode = IIf(IsDbNull(RsEmpMove.Fields("EMP_CODE").Value), "", RsEmpMove.Fields("EMP_CODE").Value)
            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                TxtEmpName.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtDept.Text = MasterNo
            End If
            txtDistance.Text = IIf(IsDbNull(RsEmpMove.Fields("VISIT_DISTANCE").Value), "", RsEmpMove.Fields("VISIT_DISTANCE").Value)


            txtPlace.Text = IIf(IsDbNull(RsEmpMove.Fields("PLACE_VISIT").Value), "", RsEmpMove.Fields("PLACE_VISIT").Value)
            txtFrom.Text = VB6.Format(IIf(IsDbNull(RsEmpMove.Fields("TIME_FROM").Value), "", RsEmpMove.Fields("TIME_FROM").Value), "HH:MM")
            txtTo.Text = VB6.Format(IIf(IsDbNull(RsEmpMove.Fields("TIME_TO").Value), "", RsEmpMove.Fields("TIME_TO").Value), "HH:MM")
            txtTotalHrs.Text = VB6.Format(IIf(IsDbNull(RsEmpMove.Fields("TOTAL_HRS").Value), "", RsEmpMove.Fields("TOTAL_HRS").Value), "HH:MM")
            txtAthCode.Text = IIf(IsDbNull(RsEmpMove.Fields("ATH_CODE").Value), "", RsEmpMove.Fields("ATH_CODE").Value)
            mMoveType = IIf(IsDbNull(RsEmpMove.Fields("MOVE_TYPE").Value), "", RsEmpMove.Fields("MOVE_TYPE").Value)

            mVisitFrom = IIf(IsDbNull(RsEmpMove.Fields("VISIT_FROM").Value), 0, RsEmpMove.Fields("VISIT_FROM").Value)
            mVehicle = IIf(IsDbNull(RsEmpMove.Fields("VEHICLE_MODE").Value), 0, RsEmpMove.Fields("VEHICLE_MODE").Value)

            cboVisitedFrom.SelectedIndex = mVisitFrom - 1
            cboVehicle.SelectedIndex = mVehicle - 1

            chkHRApproval.CheckState = IIf(RsEmpMove.Fields("HR_APPROVAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkHRApproval.Enabled = IIf(RsEmpMove.Fields("HR_APPROVAL").Value = "Y", False, True)

            chkAgainstLeave.CheckState = IIf(RsEmpMove.Fields("AGT_LEAVE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAgtOT.CheckState = IIf(IIf(IsDBNull(RsEmpMove.Fields("AGT_OT").Value), "N", RsEmpMove.Fields("AGT_OT").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            txtOTHr.Text = IIf(IsDBNull(RsEmpMove.Fields("OT_HOURS").Value), 0, RsEmpMove.Fields("OT_HOURS").Value)

            If chkAgtOT.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtOTHr.Enabled = False
                txtOTThisMonth.Text = GetTillOTHours(Trim(txtEmpCode.Text), txtRefDate.Text)
            Else
                txtOTHr.Enabled = True
                txtOTThisMonth.Text = ""
            End If

            If mMoveType = "O" Then
                optMoveType(0).Checked = True
            ElseIf mMoveType = "P" Then
                optMoveType(1).Checked = True
            Else
                optMoveType(2).Checked = True
            End If
            '        optMoveType(0).Value = IIf(mMoveType = "O", True, False)
            '        optMoveType(1).Value = IIf(mMoveType = "P", True, False)
            txtRefDateTo.Enabled = False
        End If


        If optMoveType(1).Checked = True And chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ShowLeave = False Then GoTo ShowErrPart
        End If
        ADDMode = False
        MODIFYMode = False
        txtRefNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsEmpMove, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
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
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""
        Dim mRefNo As Double
        Dim mMoveType As String
        Dim mRefDate As String
        Dim mDays As Integer
        Dim mCntDay As Integer
        Dim mFromTime As String
        Dim mToTime As String
        Dim mVisitFrom As Integer
        Dim mVehicle As Integer
        Dim mHRApproval As String

        Dim mFHalf As Integer
        Dim mSHalf As Integer
        Dim mAgtLeave As String
        Dim mAgtOT As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If optMoveType(0).Checked = True Then
            mVisitFrom = CInt(VB.Left(cboVisitedFrom.Text, 1))
            mVehicle = CInt(VB.Left(cboVehicle.Text, 1))
        Else
            mVisitFrom = 1
            mVehicle = 1
        End If

        mHRApproval = IIf(chkHRApproval.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If optMoveType(0).Checked = True Then
            mMoveType = "O"
        ElseIf optMoveType(1).Checked = True Then
            mMoveType = "P"
        Else
            mMoveType = "M"
        End If

        If Val(txtRefNo.Text) = 0 Then
            mRefNo = MaxRefNo
            txtRefNo.Text = VB6.Format(mRefNo, "00000")
        Else
            mRefNo = CDbl(VB6.Format(txtRefNo.Text, "00000"))
        End If

        mAgtLeave = IIf(chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAgtOT = IIf(chkAgtOT.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If ADDMode = True Then
            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtRefDate.Text), CDate(txtRefDateTo.Text))
            For mCntDay = 0 To mDays
                mRefDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, mCntDay, CDate(txtRefDate.Text)))
                mRefNo = mRefNo + IIf(mCntDay = 0, 0, 1)

                mFromTime = VB6.Format(mRefDate & " " & txtFrom.Text, "DD/MM/YYYY HH:MM")

                If CDate(txtFrom.Text) <= CDate(txtTo.Text) Then
                    mToTime = VB6.Format(mRefDate & " " & txtTo.Text, "DD/MM/YYYY HH:MM")
                Else
                    mToTime = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mRefDate)) & " " & txtTo.Text, "DD/MM/YYYY HH:MM")
                End If

                SqlStr = " INSERT INTO PAY_MOVEMENT_TRN ( " & vbCrLf _
                    & " COMPANY_CODE, AUTO_KEY_NO, " & vbCrLf _
                    & " REF_DATE, EMP_CODE, " & vbCrLf _
                    & " PLACE_VISIT, TIME_FROM, " & vbCrLf _
                    & " TIME_TO, TOTAL_HRS, MOVE_TYPE," & vbCrLf _
                    & " ATH_CODE, VISIT_FROM, VEHICLE_MODE, HR_APPROVAL, VISIT_DISTANCE, " & vbCrLf _
                    & " ADDUSER, ADDDATE, MODUSER, MODDATE, AGT_LEAVE, AGT_OT, OT_HOURS ) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mRefNo)) & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & Trim(txtEmpCode.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote((txtPlace.Text)) & "', TO_DATE('" & VB6.Format(mFromTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mToTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & VB6.Format(txtTotalHrs.Text, "HH:MM") & "','HH24:MI')," & vbCrLf _
                    & " '" & mMoveType & "', '" & Trim(txtAthCode.Text) & "', " & mVisitFrom & ", " & mVehicle & ",'" & mHRApproval & "'," & Val(txtDistance.Text) & "," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '','','" & mAgtLeave & "','" & mAgtOT & "'," & Val(txtOTHr.Text) & ")"

                PubDBCn.Execute(SqlStr)

                If mMoveType = "M" Then
                    If UpdateDailyAttnTrn(Trim(txtEmpCode.Text), (txtRefDate.Text), CDate(mFromTime), CDate(mToTime)) = False Then GoTo UpdateError
                End If

                If mMoveType = "O" Or mMoveType = "M" Then
                    If UpdateLeave(Trim(txtEmpCode.Text), mRefDate) = False Then GoTo UpdateError
                End If
            Next
        Else

            mFromTime = VB6.Format(txtRefDate.Text & " " & txtFrom.Text, "DD/MM/YYYY HH:MM")

            If CDate(txtFrom.Text) <= CDate(txtTo.Text) Then
                mToTime = VB6.Format(txtRefDate.Text & " " & txtTo.Text, "DD/MM/YYYY HH:MM")
            Else
                mToTime = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(txtRefDate.Text)) & " " & txtTo.Text, "DD/MM/YYYY HH:MM")
            End If

            '        mToTime = Format(txtRefDate.Text & " " & txtTo.Text, "DD/MM/YYYY HH:MM")

            SqlStr = " UPDATE PAY_MOVEMENT_TRN SET AUTO_KEY_NO=" & Val(CStr(mRefNo)) & "," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EMP_CODE='" & Trim(txtEmpCode.Text) & "', " & vbCrLf _
                & " PLACE_VISIT='" & MainClass.AllowSingleQuote((txtPlace.Text)) & "', " & vbCrLf _
                & " TIME_FROM=TO_DATE('" & VB6.Format(mFromTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " TIME_TO=TO_DATE('" & VB6.Format(mToTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " TOTAL_HRS=TO_DATE('" & VB6.Format(txtTotalHrs.Text, "HH:MM") & "','HH24:MI'), " & vbCrLf _
                & " MOVE_TYPE='" & Trim(mMoveType) & "', " & vbCrLf & " ATH_CODE='" & Trim(txtAthCode.Text) & "', " & vbCrLf _
                & " VISIT_FROM=" & Val(CStr(mVisitFrom)) & ", " & vbCrLf _
                & " VEHICLE_MODE=" & Val(CStr(mVehicle)) & ", HR_APPROVAL='" & mHRApproval & "', VISIT_DISTANCE=" & Val(txtDistance.Text) & "," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " AGT_LEAVE='" & mAgtLeave & "', AGT_OT='" & mAgtOT & "', OT_HOURS=" & Val(txtOTHr.Text) & "" & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_NO=" & Val(CStr(mRefNo)) & ""

            PubDBCn.Execute(SqlStr)

            If mMoveType = "M" Then
                If UpdateDailyAttnTrn(Trim(txtEmpCode.Text), (txtRefDate.Text), CDate(mFromTime), CDate(mToTime)) = False Then GoTo UpdateError
            End If

            If mMoveType = "O" Or mMoveType = "M" Then
                If UpdateLeave(Trim(txtEmpCode.Text), (txtRefDate.Text)) = False Then GoTo UpdateError
            End If

        End If

        If chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Checked And mMoveType = "P" Then

            mFHalf = IIf(cboFHalf.Text = "", -1, Val(VB.Left(cboFHalf.Text, 2)))
            mSHalf = IIf(cboSHalf.Text = "", -1, Val(VB.Left(cboSHalf.Text, 2)))

            If mFHalf <> -1 Or mSHalf <> -1 Then
                If CheckAttnData(Trim(txtEmpCode.Text), (txtRefDate.Text)) = False Then
                    SqlStr = "INSERT INTO PAY_ATTN_MST (" & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE," & vbCrLf & " ATTN_DATE, FIRSTHALF, SECONDHALF, AGT_LATE," & vbCrLf & " ADDUSER, ADDDATE) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(txtRefDate.Text)) & ", " & vbCrLf & " '" & Trim(txtEmpCode.Text) & "', TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & "  " & mFHalf & ", " & mSHalf & ", 'N'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                Else
                    SqlStr = "UPDATE PAY_ATTN_MST SET " & vbCrLf & " FIRSTHALF=" & mFHalf & ", " & vbCrLf & " SECONDHALF=" & mSHalf & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & txtEmpCode.Text & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If
                PubDBCn.Execute(SqlStr)
            End If

        End If

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        '    MsgBox err.Description + " Error No.: " + Str(err.Number)
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
        PubDBCn.Errors.Clear()
        RsEmpMove.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateDailyAttnTrn(ByRef mCode As String, ByRef mDate As String, ByRef mInTime As Date, ByRef mOutTime As Date) As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mGSalary As Double
        Dim SqlStr As String = ""
        Dim mTOTHours As Date
        Dim mWorksHours As Date
        Dim mOTHours As Date

        Dim mTOTHoursValue As Double
        Dim mWorksHoursValue As Double
        Dim mOTHoursValue As Double

        If CDate(mInTime) <= CDate(mOutTime) Then
            mOutTime = CDate(VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(mOutTime), Minute(mOutTime), 0), "DD/MM/YYYY HH:MM"))
        Else
            mOutTime = CDate(VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mDate)))) & " " & TimeSerial(Hour(mOutTime), Minute(mOutTime), 0), "DD/MM/YYYY HH:MM"))
        End If

        mInTime = CDate(VB6.Format(DateSerial(Year(CDate(mDate)), Month(CDate(mDate)), VB.Day(CDate(mDate))) & " " & TimeSerial(Hour(mInTime), Minute(mInTime), 0), "DD/MM/YYYY HH:MM"))


        'CalcTotatHours cntRow, mDate

        Call CalcTotatHours(mCode, mInTime, mOutTime, mDate, mTOTHours, mWorksHours, mOTHours)

        mTOTHours = CDate(VB6.Format(mTOTHours, "hh:mm"))
        mWorksHours = CDate(VB6.Format(mWorksHours, "hh:mm"))
        mOTHours = CDate(VB6.Format(mOTHours, "hh:mm"))

        mTOTHoursValue = Val(VB.Left(CStr(mTOTHours), 2)) + (CDbl(VB.Right(CStr(mTOTHours), 2)) / 60)

        mWorksHoursValue = Val(VB.Left(CStr(mWorksHours), 2)) + (CDbl(VB.Right(CStr(mWorksHours), 2)) / 60)
        mOTHoursValue = Val(VB.Left(CStr(mOTHours), 2)) + (CDbl(VB.Right(CStr(mOTHours), 2)) / 60)

        If mCode <> "" Then
            SqlStr = " DELETE FROM PAY_DALIY_ATTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & mCode & "'"

            PubDBCn.Execute(SqlStr)
            '                If Val(mTOTHours) <> 0 Then
            SqlStr = " INSERT INTO PAY_DALIY_ATTN_TRN ( " & vbCrLf & " COMPANY_CODE, PAYYEAR, " & vbCrLf & " EMP_CODE, ATTN_DATE, " & vbCrLf & " IN_TIME, OUT_TIME, TOT_HOURS," & vbCrLf & " WORKS_HOURS, OT_HOURS," & vbCrLf & " ADDUSER, ADDDATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(mDate)) & ", " & vbCrLf & " '" & mCode & "', TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TO_DATE('" & VB6.Format(mInTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & VB6.Format(mOutTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & mTOTHoursValue & ", " & vbCrLf & " " & mWorksHoursValue & ", " & mOTHoursValue & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

            PubDBCn.Execute(SqlStr)
            '                End If
        End If

        UpdateDailyAttnTrn = True
        Exit Function
UpdateError:
        UpdateDailyAttnTrn = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        '    Resume
        PubDBCn.Errors.Clear()
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function CalcTotatHours(ByRef mCode As String, ByRef mInDateTime As Date, ByRef mOutDateTime As Date, ByRef mDate As String, ByRef mTotDateTime As Date, ByRef mWorkHours As Date, ByRef mOTHours As Date) As Object
        On Error GoTo ERR1
        'Dim mInDateTime As Date
        'Dim mOutDateTime As Date

        Dim mBalHours As Date
        Dim mHour As Short
        Dim mMin As Short
        Dim mShiftInTime As Date
        Dim mShiftOutTime As Date
        Dim mMarginsMinute As Double
        Dim mSundayOTHours As Date
        Dim mISHoliday As Boolean
        Dim mHolidayType As String

        mShiftInTime = CDate(GetShiftTime(mCode, VB6.Format(mDate, "DD/MM/YYYY"), mMarginsMinute, "I", "E"))
        mShiftOutTime = CDate(GetShiftTime(mCode, VB6.Format(mDate, "DD/MM/YYYY"), mMarginsMinute, "O", "E"))


        If GetTotatHours(mInDateTime, mOutDateTime, mInDateTime, mOutDateTime, mTotDateTime, mWorkHours, mOTHours, mSundayOTHours, mShiftInTime, mShiftOutTime, mDate, mCode) = False Then GoTo ERR1

CalcPart:

        mHolidayType = ""
        mISHoliday = GetIsHolidays(VB6.Format(mDate, "DD/MM/YYYY"), mHolidayType, mCode, "", "N")

        If mISHoliday = False Then
            mOTHours = mOTHours
        Else
            mOTHours = System.Date.FromOADate(mWorkHours.ToOADate + mOTHours.ToOADate)
        End If

        If mISHoliday = False Then
            mWorkHours = mWorkHours
        Else
            mWorkHours = System.Date.FromOADate(0)
        End If




        Exit Function
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateLeave(ByRef mCode As String, ByRef xDate As String) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""

        Dim pFHalf As String
        Dim pSHalf As String
        Dim mEmpShiftBreak As String

        Dim pFHalfPresent As Integer
        Dim pSHalfPresent As Integer

        Dim mShiftInTime As String
        Dim mShiftOutTime As String

        Dim mIsRoundClock As Boolean
        Dim mIsPrevRoundClock As Boolean

        Dim mInTime As String
        Dim mOutTime As String
        Dim mFirstIsO As Boolean
        Dim mSecondIsOD As Boolean

        mShiftInTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 5, "I", "E")
        mShiftOutTime = GetShiftTime(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), 5, "O", "E")
        mIsRoundClock = GetRoundClock(mCode, VB6.Format(xDate, "DD-MMM-YYYY"), "E")
        mIsPrevRoundClock = GetRoundClock(mCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(xDate, "DD-MMM-YYYY")))), "E")

        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 4, CDate(mShiftInTime)), "DD/MM/YYYY HH:MM")))
        mEmpShiftBreak = CStr(CDate(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Minute, 30, CDate(mEmpShiftBreak)), "DD/MM/YYYY HH:MM")))
        mFirstIsO = False
        mSecondIsOD = False
        If CheckEmpTime(mCode, xDate, mInTime, mOutTime, IIf(mIsRoundClock = True, "Y", "N"), mFirstIsO, mSecondIsOD, mEmpShiftBreak) = False Then GoTo UpdateError

        If CDate(VB6.Format(mInTime, "HH:MM")) <= CDate(mShiftInTime) And CDate(VB6.Format(mOutTime, "HH:MM")) >= CDate(mEmpShiftBreak) Then
            pFHalf = "P"
        End If

        If CDate(VB6.Format(mInTime, "HH:MM")) <= CDate(mEmpShiftBreak) And CDate(VB6.Format(mOutTime, "HH:MM")) >= CDate(mShiftOutTime) Then
            pSHalf = "P"
        End If


        If pFHalf = "P" Or pSHalf = "P" Then
            If UpdateEmpPresent(mCode, xDate, pFHalf, pSHalf, PubDBCn) = False Then GoTo UpdateError
        End If

        UpdateLeave = True
        Exit Function
UpdateError:
        UpdateLeave = False
    End Function
    Private Function CheckEmpTime(ByRef mEmpCode As String, ByRef mMonthDate As String, ByRef mEmpInTime As String, ByRef mEmpOutTime As String, ByRef mIsRound As String, ByRef mFirstIsOD As Boolean, ByRef mSecondIsOD As Boolean, ByRef mEmpShiftBreak As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEMPODOut As String
        Dim mEmpODIn As String
        Dim mIsODLocal1 As Boolean
        Dim mIsODLocal2 As Boolean

        mEmpInTime = ""
        mEmpOutTime = ""

        mIsODLocal1 = False
        mIsODLocal2 = False
        mFirstIsOD = False
        mSecondIsOD = False

        SqlStr = " SELECT IN_TIME, OUT_TIME " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mEmpInTime = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
            mEmpOutTime = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:MM")

            mEmpInTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpInTime)), Minute(CDate(mEmpInTime)), 0), "DD/MM/YYYY HH:MM")

            If mIsRound = "Y" Then
                mEmpOutTime = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
            Else
                mEmpOutTime = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
            End If
        Else
            mEmpInTime = "00:00"
            mEmpOutTime = "00:00"
        End If
        mEMPODOut = "00:00"
        mEmpODIn = "00:00"

        SqlStr = " SELECT MIN(TO_CHAR(TIME_FROM,'DD-MON-YYYY HH24:MI')) AS TIME_FROM " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("TIME_FROM").Value) = False Then
                mIsODLocal1 = True
                mEMPODOut = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_FROM").Value), "00:00", RsTemp.Fields("TIME_FROM").Value), "HH:MM")
                mEMPODOut = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEMPODOut)), Minute(CDate(mEMPODOut)), 0), "DD/MM/YYYY HH:MM")
            End If
        End If

        If mIsRound = "Y" Then
            SqlStr = " SELECT MIN(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & UCase(VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)), "DD-MMM-YYYY")) & "','DD-MON-YYYY')"

            '        SqlStr = SqlStr & vbCrLf & " AND TO_DATE(TIME_TO,'DD-MON-YYYY HH24:MI')<='" & VB6.Format(DateAdd("h", 8, mEmpInTime), "DD-MMM-YYYY hh:MM") & "'"

            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(TIME_TO,'YYYYMMDDHH24MI')<=TO_CHAR('" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Hour, 8, CDate(mEmpInTime)), "YYYYMMDDhhMM") & "')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDbNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mMonthDate)))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        Else
            SqlStr = " SELECT MAX(TO_CHAR(TIME_TO,'DD-MON-YYYY HH24:MI')) AS TIME_TO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE IN ('O','M')" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then
                SqlStr = SqlStr & vbCrLf & " AND HR_APPROVAL='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                If IsDbNull(RsTemp.Fields("TIME_TO").Value) = False Then
                    mIsODLocal2 = True
                    mEmpODIn = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TIME_TO").Value), "00:00", RsTemp.Fields("TIME_TO").Value), "HH:MM")
                    mEmpODIn = VB6.Format(DateSerial(Year(CDate(mMonthDate)), Month(CDate(mMonthDate)), VB.Day(CDate(mMonthDate))) & " " & TimeSerial(Hour(CDate(mEmpODIn)), Minute(CDate(mEmpODIn)), 0), "DD/MM/YYYY HH:MM")
                End If
            End If
        End If

        If VB6.Format(mEmpInTime, "HH:MM") = "00:00" And VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
            If mIsODLocal1 = True Then
                If VB6.Format(mEMPODOut, "HH:MM") <= VB6.Format(mEmpShiftBreak, "HH:MM") And VB6.Format(mEmpODIn, "HH:MM") <= VB6.Format(mEmpShiftBreak, "HH:MM") Then
                    mFirstIsOD = True
                    mEmpInTime = mEMPODOut
                Else
                    If VB6.Format(mEMPODOut, "HH:MM") <= VB6.Format(mEmpShiftBreak, "HH:MM") Then
                        mFirstIsOD = True
                        mEmpInTime = mEMPODOut
                    Else
                        mFirstIsOD = False
                    End If
                End If

                If VB6.Format(mEmpODIn, "HH:MM") > VB6.Format(mEmpShiftBreak, "HH:MM") Then
                    mSecondIsOD = True
                    mEmpOutTime = mEmpODIn
                Else
                    mSecondIsOD = False
                End If
            Else
                mFirstIsOD = False
            End If
        Else
            If VB6.Format(mEmpInTime, "HH:MM") = "00:00" Then
                mEmpInTime = mEMPODOut
                mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
                mEmpInTime = IIf(mIsODLocal1 = True, mEMPODOut, mEmpInTime)
            Else
                If VB6.Format(mEMPODOut, "HH:MM") <> "00:00" Then
                    If CDate(mEMPODOut) < CDate(mEmpInTime) Then
                        mEmpInTime = mEMPODOut
                        mFirstIsOD = True
                    End If
                End If
            End If

            If VB6.Format(mEmpOutTime, "HH:MM") = "00:00" Then
                mEmpOutTime = mEmpODIn
                mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
                mEmpOutTime = IIf(mIsODLocal2 = True, mEmpODIn, mEmpOutTime)
            Else
                If VB6.Format(mEmpODIn, "HH:MM") <> "00:00" Then
                    If CDate(mEmpODIn) > CDate(mEmpOutTime) Then
                        mEmpOutTime = mEmpODIn
                        mSecondIsOD = True
                    End If
                End If
            End If
        End If

        '    If Format(mEmpInTime, "HH:MM") = "00:00" Then
        ''        mEmpInTime = mEMPODOut
        '        mFirstIsOD = IIf(mIsODLocal1 = True, True, False)
        '    End If
        '
        '    If Format(mEmpOutTime, "HH:MM") = "00:00" Then
        ''        mEmpOutTime = mEmpODIn
        '        mSecondIsOD = IIf(mIsODLocal2 = True, True, False)
        '    End If
        '
        '    If Format(mEMPODOut, "HH:MM") <> "00:00" Then
        '        If CVDate(mEMPODOut) < CVDate(mEmpInTime) Then
        ''            mEmpInTime = mEMPODOut
        '            mFirstIsOD = True
        '        End If
        '    End If
        '
        '    If Format(mEmpODIn, "HH:MM") <> "00:00" Then
        '        If CVDate(mEmpODIn) > CVDate(mEmpOutTime) Then
        ''            mEmpOutTime = mEmpODIn
        '            mSecondIsOD = True
        '        End If
        '    End If

        CheckEmpTime = True
        Exit Function
ErrPart:
        '    Resume
        CheckEmpTime = False

    End Function
    Private Function GetTotalShortLeave(ByRef mEmpCode As String, ByRef mMonthDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetTotalShortLeave = 0
        SqlStr = " SELECT COUNT(AUTO_KEY_NO) AS AUTO_KEY_NO " & vbCrLf _
            & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
            & " AND MOVE_TYPE ='P' AND AGT_LEAVE='N' AND AGT_OT='N'" & vbCrLf _
            & " AND TO_CHAR(REF_DATE,'YYYYMM')='" & (VB6.Format(mMonthDate, "YYYYMM")) & "'"

        If Val(txtRefNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_NO<> " & Val(txtRefNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("AUTO_KEY_NO").Value) = False Then
                GetTotalShortLeave = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_NO").Value), 0, RsTemp.Fields("AUTO_KEY_NO").Value)
            End If
        End If

        Exit Function
ErrPart:
        GetTotalShortLeave = 0

    End Function

    Private Function GetMonthManualEntryAllow(ByRef mEmpCode As String, ByRef mMonthDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetMonthManualEntryAllow = 0
        SqlStr = " SELECT COUNT(AUTO_KEY_NO) AS AUTO_KEY_NO " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND MOVE_TYPE ='M'" & vbCrLf & " AND TO_CHAR(REF_DATE,'YYYYMM')='" & (VB6.Format(mMonthDate, "YYYYMM")) & "'"

        If Val(txtRefNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_NO<> " & Val(txtRefNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("AUTO_KEY_NO").Value) = False Then
                GetMonthManualEntryAllow = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_NO").Value), 0, RsTemp.Fields("AUTO_KEY_NO").Value)
            End If
        End If

        Exit Function
ErrPart:
        GetMonthManualEntryAllow = 0

    End Function
    Private Function GetTotalShortLeaveTime(ByRef mEmpCode As String, ByRef mMonthDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mHTime As Double
        Dim mMTime As Double

        GetTotalShortLeaveTime = 0
        SqlStr = " SELECT TOTAL_HRS " & vbCrLf _
            & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
            & " AND MOVE_TYPE ='P' AND AGT_LEAVE='N' AND AGT_OT='N'" & vbCrLf _
            & " AND TO_CHAR(REF_DATE,'YYYYMM')='" & (VB6.Format(mMonthDate, "YYYYMM")) & "'"

        If Val(txtRefNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_NO<> " & Val(txtRefNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mHTime = ((Hour(CDate(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TOTAL_HRS").Value), "00:00", RsTemp.Fields("TOTAL_HRS").Value), "HH:MM"))) * 60))
                mMTime = Minute(CDate(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TOTAL_HRS").Value), "00:00", RsTemp.Fields("TOTAL_HRS").Value), "HH:MM")))


                If mMTime > 5 And mMTime <= 30 Then
                    mMTime = 30
                ElseIf mMTime > 30 Then
                    mMTime = 60
                End If

                GetTotalShortLeaveTime = GetTotalShortLeaveTime + (mHTime + mMTime)
                RsTemp.MoveNext()
            Loop
        End If

        'SqlStr = " SELECT SUM(OT_HOURS) AS OT_HOURS" & vbCrLf _
        '    & " FROM PAY_MOVEMENT_TRN " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
        '    & " AND MOVE_TYPE ='P' AND AGT_OT='Y'" & vbCrLf _
        '    & " AND TO_CHAR(REF_DATE,'YYYYMM')='" & (VB6.Format(mMonthDate, "YYYYMM")) & "'"

        'If Val(txtRefNo.Text) > 0 Then
        '    SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_NO<> " & Val(txtRefNo.Text) & ""
        'End If

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        'If RsTemp.EOF = False Then
        '    GetTotalShortLeaveTime = GetTotalShortLeaveTime + IIf(IsDBNull(RsTemp.Fields("OT_HOURS").Value), 0, RsTemp.Fields("OT_HOURS").Value)
        'End If
        Exit Function
ErrPart:
        GetTotalShortLeaveTime = 0

    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim mRefNo As String
        Dim mShortLeaveTime As Double
        Dim mShortLeaveDays As Double
        Dim mActualMin As Double
        Dim mActualMinTaken As Double
        Dim mActualDays As Double
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim mMannualEntryAllow As String
        Dim mMonthManualEntry As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If

        If txtEmpCode.Text = "" Then
            MsgInformation("Please Entered Emp Code.")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "PUNCH_OPT", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUNCH_OPT='S'") = True Then
            MsgInformation("Employee Attendance is Stopped.")
            FieldsVarification = False
            Exit Function
        End If

        If txtAthCode.Text = "" Then
            MsgInformation("Please Athorised Emp Code.")
            txtAthCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If CurrModuleName = mInventoryModule Then
            If VB6.Format(txtEmpCode.Text, "000000") <> VB6.Format(PubUserEMPCode, "000000") Then
                MsgInformation("You are not a Valid User for Such ID.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        '    If Val(txtRefNo.Text) = 0 Then
        '        MsgInformation "Please Entered Ref No."
        '        If txtRefNo.Enabled = True Then txtRefNo.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If txtRefDate.Text = "" Then
            MsgInformation("Please Entered Ref Date.")
            txtRefDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If txtRefDateTo.Text = "" Then
            MsgInformation("Please Entered Ref Date.")
            txtRefDateTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If optMoveType(1).Checked = True Then
            If CDate(txtRefDate.Text) <> CDate(txtRefDateTo.Text) Then
                MsgInformation("Ref Date From should be Equal Than To Date.")
                txtRefDateTo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If CDate(txtRefDate.Text) > CDate(txtRefDateTo.Text) Then
                MsgInformation("Ref Date From Cann't be Greater Than To Date.")
                txtRefDateTo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If optMoveType(0).Checked = True Then
            If Trim(cboVisitedFrom.Text) = "" Then
                MsgInformation("Please Select Visited From.")
                FieldsVarification = False
                Exit Function
            End If

            If Trim(cboVehicle.Text) = "" Then
                MsgInformation("Please Select Vehicle Type.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If chkHRApproval.CheckState = System.Windows.Forms.CheckState.Checked And chkHRApproval.Enabled = False Then
            MsgInformation("Slip already Approved. Cann't be Modify")
            FieldsVarification = False
            Exit Function
        End If

        If CheckAlreadyMove(mRefNo) = True Then
            MsgInformation("Employee Already Out as such Time. Ref No is : " & mRefNo)
            FieldsVarification = False
            Exit Function
        End If

        If optMoveType(1).Checked = True Then
            If chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Checked And chkAgtOT.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Please Select either Agt Leave or OT.")
                FieldsVarification = False
                Exit Function
            End If
            If chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If Not IsDate(txtTo.Text) Or Not IsDate(txtFrom.Text) Then
                    MsgInformation("Please enter the vaild Time.")
                    FieldsVarification = False
                    Exit Function
                End If
                mShortLeaveTime = IIf(IsDBNull(RsCompany.Fields("SHORT_LEAVE").Value), 0, RsCompany.Fields("SHORT_LEAVE").Value)
                mShortLeaveDays = IIf(IsDBNull(RsCompany.Fields("SHORT_LEAVE_DAYS").Value), 0, RsCompany.Fields("SHORT_LEAVE_DAYS").Value)

                mActualMin = (VB.Left(txtTotalHrs.Text, 2) * 60) + VB.Right(txtTotalHrs.Text, 2)     '' (Hour(CDate(txtTo.Text)) * 60 + Minute(CDate(txtTo.Text))) - (Hour(CDate(txtFrom.Text)) * 60 + Minute(CDate(txtFrom.Text)))

                If chkAgtOT.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mActualMin = mActualMin - Val(txtOTHr.Text)
                    If mActualMin > mShortLeaveTime Then
                        MsgInformation("Employee Cann't be taken More than " & mShortLeaveTime & " Min in one time.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    mActualMinTaken = GetTotalShortLeaveTime(Trim(txtEmpCode.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY"))
                    If mActualMinTaken + mActualMin > mShortLeaveTime Then
                        MsgInformation("Employee Already Taken " & mActualMinTaken & " Min, Against Short Leave " & mShortLeaveTime & " Mintue.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    mActualDays = GetTotalShortLeave(Trim(txtEmpCode.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY"))
                    If mActualDays >= mShortLeaveDays Then
                        MsgInformation("Employee Already Taken " & mActualDays & " Days, Against Short Leave " & mShortLeaveDays & " Days.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    If Val(txtOTHr.Text) >= Val(txtOTThisMonth.Text) Then
                        MsgInformation("OT Hours cann't be Greater than Actual Month OT Hours.")
                        FieldsVarification = False
                        Exit Function
                    End If

                Else
                    If mActualMin > mShortLeaveTime Then
                        MsgInformation("Employee Cann't be taken More than " & mShortLeaveTime & " Min in one time.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    mActualMinTaken = GetTotalShortLeaveTime(Trim(txtEmpCode.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY"))
                    If mActualMinTaken + mActualMin > mShortLeaveTime Then
                        MsgInformation("Employee Already Taken " & mActualMinTaken & " Min, Against Short Leave " & mShortLeaveTime & " Mintue.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    mActualDays = GetTotalShortLeave(Trim(txtEmpCode.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY"))
                    If mActualDays >= mShortLeaveDays Then
                        MsgInformation("Employee Already Taken " & mActualDays & " Days, Against Short Leave " & mShortLeaveDays & " Days.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    If Val(txtOTHr.Text) > 0 Then
                        MsgInformation("You Cann't be Select The OT Hours.")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If



            If chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Checked Then
                mFHalf = IIf(cboFHalf.Text = "", -1, Val(VB.Left(cboFHalf.Text, 2)))
                mSHalf = IIf(cboSHalf.Text = "", -1, Val(VB.Left(cboSHalf.Text, 2)))

                If mFHalf = -1 And mSHalf = -1 Then
                    MsgInformation("Please Select Leave Against Short Leave.")
                    FieldsVarification = False
                    Exit Function
                End If

                If mFHalf = 5 Or mFHalf = 7 Or mFHalf = 8 Or mFHalf = 9 Then
                    MsgInformation("Please Select Valid Leave. CPL Earn / CPL Avail / Holiday / Sunday Cann't be Select.")
                    FieldsVarification = False
                    Exit Function
                End If

                If mSHalf = 5 Or mSHalf = 7 Or mSHalf = 8 Or mSHalf = 9 Then
                    MsgInformation("Please Select Valid Leave. CPL Earn / CPL Avail / Holiday / Sunday Cann't be Select.")
                    FieldsVarification = False
                    Exit Function
                End If


            End If
        ElseIf optMoveType(0).Checked = True Then
            chkAgainstLeave.CheckState = System.Windows.Forms.CheckState.Unchecked
            cboFHalf.SelectedIndex = -1
            cboSHalf.SelectedIndex = -1
            chkAgtOT.CheckState = System.Windows.Forms.CheckState.Unchecked
            txtOTHr.Text = 0
        End If

        If optMoveType(2).Checked = True Then
            mMannualEntryAllow = "N"
            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "PUNCH_OPT", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUNCH_OPT='M'") = True Then
                mMannualEntryAllow = "Y"
            End If

            If PubSuperUser = "S" Or PubSuperUser = "A" Or mIsAuthorisedUser = True Then
            Else
                If mMannualEntryAllow = "N" Then
                    mMonthManualEntry = GetMonthManualEntryAllow((txtEmpCode.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY"))
                    If mMonthManualEntry > 3 Then
                        MsgInformation("You have no rights to Enter Manual Entry more than 3 times. Please contact Administrator")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
        End If


        If MODIFYMode = True And RsEmpMove.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtRefNo.Maxlength = RsEmpMove.Fields("AUTO_KEY_NO").Precision
        txtRefDate.MaxLength = 10
        txtRefDateTo.MaxLength = 10
        txtEmpCode.Maxlength = RsEmpMove.Fields("EMP_CODE").DefinedSize
        TxtEmpName.Maxlength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)
        txtDept.Maxlength = MainClass.SetMaxLength("EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn)
        txtPlace.Maxlength = RsEmpMove.Fields("PLACE_VISIT").DefinedSize
        txtDistance.Maxlength = RsEmpMove.Fields("VISIT_DISTANCE").Precision
        txtFrom.MaxLength = 5
        txtTo.MaxLength = 5
        txtTotalHrs.MaxLength = 5
        txtAthCode.Maxlength = RsEmpMove.Fields("ATH_CODE").DefinedSize

        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '' Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = " SELECT TRN.AUTO_KEY_NO, TRN.REF_DATE, TRN.EMP_CODE, EMP.EMP_NAME, TO_CHAR(TOTAL_HRS,'HH24:MI') AS TOTAL_HRS, " & vbCrLf _
            & " DECODE(MOVE_TYPE,'O','OFFICIAL',DECODE(MOVE_TYPE,'M','MANUAL','PERSONAL')) AS MOVE_TYPE, ATH_CODE" & vbCrLf _
            & " FROM PAY_MOVEMENT_TRN TRN, PAY_EMPLOYEE_MST EMP " & vbCrLf _
            & " WHERE TRN.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.COMPANY_CODE = EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE = EMP.EMP_CODE"

        'If lblMovementType.Text = "A" Then
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.MOVE_TYPE ='M'"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.MOVE_TYPE IN ('P','O')"
        'End If

        'If CurrModuleName = mInventoryModule Then
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.EMP_CODE ='" & VB6.Format(PubUserEMPCode, "000000") & "'"
        'End If

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.REF_DATE, TRN.EMP_CODE"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 6)
            .set_ColWidth(2, 8)
            .set_ColWidth(3, 8)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 8)
            .set_ColWidth(6, 8)
            .set_ColWidth(7, 8)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Dim SqlStr As String = ""

        SqlStr = ""
        '     If IsFieldExist = True Then Delete1 = False: Exit Function

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "PAY_MOVEMENT_TRN", (txtRefNo.Text), RsEmpMove, "", "D") = False Then GoTo DeleteErr

        If InsertIntoDeleteTrn(PubDBCn, "PAY_MOVEMENT_TRN", "AUTO_KEY_NO", (txtRefNo.Text)) = False Then GoTo DeleteErr

        If optMoveType(2).Checked = True Then
            SqlStr = " DELETE FROM PAY_DALIY_ATTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ATTN_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpCode.Text) & "'"

            PubDBCn.Execute(SqlStr)
        End If

        SqlStr = " DELETE " & vbCrLf & " FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_NO=" & Val(txtRefNo.Text) & ""

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsEmpMove.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsEmpMove.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String

        Report1.Reset()
        mTitle = "EMPLOYEE MOVEMENT SLIP"
        'mSubTitle = "From : " & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY") & " TO : " & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")


        SqlStr = " SELECT TRN.AUTO_KEY_NO, TRN.REF_DATE, TRN.EMP_CODE, EMP.EMP_NAME, TO_CHAR(TOTAL_HRS,'HH24:MI') AS TOTAL_HRS, " & vbCrLf _
            & " DECODE(MOVE_TYPE,'O','OFFICIAL',DECODE(MOVE_TYPE,'M','MANUAL','PERSONAL')) AS MOVE_TYPE, ATH_CODE" & vbCrLf _
            & " FROM PAY_MOVEMENT_TRN TRN, PAY_EMPLOYEE_MST EMP " & vbCrLf _
            & " WHERE TRN.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.COMPANY_CODE = EMP.COMPANY_CODE" & vbCrLf _
            & " AND TRN.EMP_CODE = EMP.EMP_CODE"

        SqlStr = SqlStr & vbCrLf & " AND TRN.AUTO_KEY_NO ='" & Trim(txtRefNo.Text) & "'"


        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.REF_DATE, TRN.EMP_CODE"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PAYMOVEMENT.rpt"
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle)

        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtAthCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAthCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAthCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAthCode.DoubleClick
        Call cmdAthSearch_Click(cmdAthSearch, New System.EventArgs())
    End Sub

    Private Sub txtAthCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAthCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAthCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAthCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAthCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdAthSearch_Click(cmdAthSearch, New System.EventArgs())
    End Sub

    Private Sub txtAthCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAthCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mDOJ As String
        Dim mDOL As String

        If Trim(txtAthCode.Text) = "" Then GoTo EventExitSub

        txtAthCode.Text = VB6.Format(txtAthCode.Text, "000000")
        mDOJ = VB6.Format(txtRefDate.Text, "DD/MM/YYYY") ''MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
        mDOL = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")

        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtAthCode.Text)) & "' " & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        If RS.EOF = False Then
            txtAthCode.Text = RS.Fields("EMP_CODE").Value
        Else
            MsgBox("This Employee Code does not exsits in Master.", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub

ERR1:

        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDistance_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDistance.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDistance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDistance.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mDOJ As String
        Dim mDOL As String

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub

        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        mDOJ = VB6.Format(txtRefDate.Text, "DD/MM/YYYY") ''MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
        mDOL = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")

        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "' " & vbCrLf _
            & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND (EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & PubUserEMPCode & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        If RS.EOF = False Then
            txtEmpCode.Text = RS.Fields("EMP_CODE").Value
            TxtEmpName.Text = IIf(IsDbNull(RS.Fields("EMP_NAME").Value), "", RS.Fields("EMP_NAME").Value)
            txtDept.Text = IIf(IsDbNull(RS.Fields("EMP_DEPT_CODE").Value), "", RS.Fields("EMP_DEPT_CODE").Value)
        Else
            MsgBox("This Employee Code does not exsits in Master.", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If

        If optMoveType(2).Checked = True Then
            If FillEmpINTimeOut() = False Then GoTo ERR1
        End If

        GoTo EventExitSub


ERR1:

        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtEmpName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtFrom.Text) = "" Or Trim(txtFrom.Text) = "__:__" Then GoTo EventExitSub
        If Not IsDate(txtFrom.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            txtFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If


        Call CalcTotalHrs()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub CalcTotalHrs()
        On Error GoTo ErrPart
        Dim mTotalHrs As String
        Dim mMin1 As Integer
        Dim mMin2 As Integer
        Dim mOTHours As Integer

        If Trim(txtFrom.Text) = "" Or Trim(txtFrom.Text) = "__:__" Or Trim(txtTo.Text) = "" Or Trim(txtTo.Text) = "__:__" Then Exit Sub

        If Not IsDate(txtTo.Text) Or Not IsDate(txtFrom.Text) Then Exit Sub

        mMin1 = Hour(CDate(txtFrom.Text)) * 60 + Minute(CDate(txtFrom.Text))
        mMin2 = Hour(CDate(txtTo.Text)) * 60 + Minute(CDate(txtTo.Text))

        If mMin1 = 0 Or mMin2 = 0 Then Exit Sub

        If CDate(txtFrom.Text) <= CDate(txtTo.Text) Then
            mTotalHrs = VB6.Format(Int((mMin2 - mMin1) / 60), "00") & ":" & VB6.Format((mMin2 - mMin1) Mod 60, "00")
            mOTHours = (mMin2 - mMin1)
        Else
            mMin2 = mMin2 + (24 * 60)
            mTotalHrs = VB6.Format(Int((mMin2 - mMin1) / 60), "00") & ":" & VB6.Format((mMin2 - mMin1) Mod 60, "00")
            mOTHours = (mMin2 - mMin1)
        End If

        txtTotalHrs.Text = VB6.Format(mTotalHrs, "HH:MM")

        If chkAgtOT.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtOTHr.Text = mOTHours
        Else
            txtOTHr.Text = ""
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtPlace_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlace.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlace_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPlace.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPlace.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If Trim(txtRefDate.Text) = "" Or Trim(txtRefDate.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtRefDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If ADDMode = True Then
            txtRefDateTo.Text = txtRefDate.Text
        End If

        If Year(CDate(txtRefDate.Text)) <> CDbl(PubPAYYEAR) Then
            MsgBox("Invalid Current Calender Year Date", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If optMoveType(2).Checked = True Then
            If FillEmpINTimeOut() = False Then GoTo ERR1
        End If
        GoTo EventExitSub
ERR1:

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDateTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefDateTo.Text) = "" Or Trim(txtRefDateTo.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtRefDateTo.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If Year(CDate(txtRefDateTo.Text)) <> CDbl(PubPAYYEAR) Then
            MsgBox("Invalid Current Calender Year Date", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub
        txtRefNo.Text = VB6.Format(txtRefNo.Text, "00000")
        If MODIFYMode = True And RsEmpMove.EOF = False Then xRefNo = RsEmpMove.Fields("AUTO_KEY_NO").Value

        SqlStr = ""
        SqlStr = "Select A.* from  PAY_MOVEMENT_TRN A, PAY_EMPLOYEE_MST B " & vbCrLf _
            & " Where A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And A.AUTO_KEY_NO=" & Val(txtRefNo.Text) & "" & vbCrLf _
            & " AND  A.COMPANY_CODE= B.COMPANY_CODE AND  A.EMP_CODE=B.EMP_CODE"

        If PubUserLevel = 1 Then

        ElseIf PubUserLevel = 2 Then
            SqlStr = SqlStr & vbCrLf & " And (A.EMP_DEPT_CODE IN (SELECT DEPT_CODE FROM GEN_DEPTRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') OR EMP_CODE = '" & PubUserEMPCode & "')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND A.EMP_CODE='" & PubUserEMPCode & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpMove, ADODB.LockTypeEnum.adLockReadOnly)
        If RsEmpMove.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                MainClass.UOpenRecordSet("Select * From PAY_MOVEMENT_TRN Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_NO=" & xRefNo & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpMove, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtTo.Text) = "" Or Trim(txtTo.Text) = "__:__" Then GoTo EventExitSub

        If Not IsDate(txtTo.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
        End If
        Call CalcTotalHrs()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotalHrs_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalHrs.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotalHrs_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotalHrs.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtTotalHrs.Text) = "" Or Trim(txtTotalHrs.Text) = "__:__" Then GoTo EventExitSub

        If Not IsDate(txtTotalHrs.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function MaxRefNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMaxRef As Double

        SqlStr = "SELECT MAX(AUTO_KEY_NO) AS AUTO_KEY_NO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MaxRefNo = 1
        Else
            mMaxRef = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_NO").Value), 0, RsTemp.Fields("AUTO_KEY_NO").Value)
            MaxRefNo = mMaxRef + 1
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Function


    Private Function CheckAlreadyMove(ByRef mRefNo As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFromTime As String
        Dim mToTime As String

        CheckAlreadyMove = False
        mRefNo = ""
        SqlStr = "SELECT AUTO_KEY_NO,TIME_FROM, TIME_TO FROM PAY_MOVEMENT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If optMoveType(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND MOVE_TYPE = 'M'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND MOVE_TYPE IN ('O', 'P')"
        End If

        If Val(txtRefNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_NO<>" & Val(txtRefNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mFromTime = VB6.Format(RsTemp.Fields("TIME_FROM").Value, "hh:mm")
                mToTime = VB6.Format(RsTemp.Fields("TIME_TO").Value, "hh:mm")
                If (CDate(txtFrom.Text) <= CDate(mFromTime) And CDate(txtTo.Text) <= CDate(mFromTime)) Or (CDate(txtFrom.Text) >= CDate(mToTime) And CDate(txtTo.Text) >= CDate(mToTime)) Then
                    CheckAlreadyMove = False
                Else
                    mRefNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_NO").Value), "", RsTemp.Fields("AUTO_KEY_NO").Value)
                    CheckAlreadyMove = True
                    Exit Function
                End If
                '            SqlStr = "SELECT TIME_FROM, TIME_TO FROM PAY_MOVEMENT_TRN " & vbCrLf _
                ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                ''            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'" & vbCrLf _
                ''            & " AND REF_DATE='" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "'"

                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        CheckAlreadyMove = False
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Function

    Private Sub txtOTHr_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles txtOTHr.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub chkAgtOT_CheckedChanged(sender As Object, e As EventArgs) Handles chkAgtOT.CheckedChanged
        On Error GoTo ErrPart
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)

        If optMoveType(1).Checked = True Then
            If chkAgtOT.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtOTHr.Enabled = True
                If Val(txtOTHr.Text) = 0 Then
                    CalcTotalHrs()
                End If
                txtOTThisMonth.Text = GetTillOTHours(Trim(txtEmpCode.Text), txtRefDate.Text)
            Else
                txtOTHr.Text = 0
                txtOTHr.Enabled = False
            End If
        Else
            txtOTHr.Text = 0
        End If
        Exit Sub
ErrPart:
    End Sub
End Class
