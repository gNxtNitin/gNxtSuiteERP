Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEmpTrfRecd
    Inherits System.Windows.Forms.Form
    Dim RsEmpTrf As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim Shw As Boolean
    Dim xFromEmpCode As String
    Dim xFromCCode As Integer
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsEmpTrf, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        cboUnitFrom.SelectedIndex = 0
        cboUnitTo.SelectedIndex = 0
        txtFromEmpCode.Text = ""
        txtFromEmpName.Text = ""
        txtToEmpCode.Text = ""
        txtToEmpName.Text = ""

        txtLeaveDate.Text = ""
        txtJoiningDate.Text = ""
        cboUnitFrom.Enabled = True
        txtFromEmpCode.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsEmpTrf, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsEmpTrf, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If cboUnitFrom.Enabled = True Then cboUnitFrom.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsEmpTrf.EOF = False Then RsEmpTrf.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If cboUnitFrom.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If txtFromEmpCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub

        If Not RsEmpTrf.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsEmpTrf.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub frmEmpTrfRecd_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmEmpTrfRecd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        Dim mFromCompanyCode As Integer
        Dim mFromEmpCode As String


        SqlStr = ""
        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = 1
        mFromCompanyCode = Val(SprdView.Text)

        SprdView.Col = 2
        mFromEmpCode = UCase(Trim(SprdView.Text))

        SqlStr = " SELECT * from PAY_EMP_TRF_MST" & vbCrLf & " WHERE FROM_COMPANY_CODE=" & mFromCompanyCode & "" & vbCrLf & " AND FROM_EMP_CODE='" & MainClass.AllowSingleQuote(UCase(mFromEmpCode)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpTrf, ADODB.LockTypeEnum.adLockReadOnly)

        If RsEmpTrf.EOF = False Then
            Show1()
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub frmEmpTrfRecd_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsEmpTrf = Nothing
        ''Me = Nothing
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mToCompanyCode As Integer

        Shw = True
        If Not RsEmpTrf.EOF Then

            xFromCCode = IIf(IsDbNull(RsEmpTrf.Fields("FROM_COMPANY_CODE").Value), "", RsEmpTrf.Fields("FROM_COMPANY_CODE").Value)
            If MainClass.ValidateWithMasterTable(xFromCCode, "COMPANY_CODE", "COMPANY_NAME", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
                cboUnitFrom.Text = Trim(UCase(MasterNo))
            End If

            xFromEmpCode = IIf(IsDbNull(RsEmpTrf.Fields("FROM_EMP_CODE").Value), "", RsEmpTrf.Fields("FROM_EMP_CODE").Value)
            txtFromEmpCode.Text = IIf(IsDbNull(RsEmpTrf.Fields("FROM_EMP_CODE").Value), "", RsEmpTrf.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtFromEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xFromCCode & "") = True Then
                txtFromEmpName.Text = Trim(UCase(MasterNo))
            End If

            If MainClass.ValidateWithMasterTable((txtFromEmpCode.Text), "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xFromCCode & "") = True Then
                txtLeaveDate.Text = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            mToCompanyCode = IIf(IsDbNull(RsEmpTrf.Fields("TO_COMPANY_CODE").Value), "", RsEmpTrf.Fields("TO_COMPANY_CODE").Value)
            If MainClass.ValidateWithMasterTable(mToCompanyCode, "COMPANY_CODE", "COMPANY_NAME", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
                cboUnitTo.Text = Trim(UCase(MasterNo))
            End If

            txtToEmpCode.Text = IIf(IsDbNull(RsEmpTrf.Fields("TO_EMP_CODE").Value), "", RsEmpTrf.Fields("TO_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtToEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mToCompanyCode & "") = True Then
                txtToEmpName.Text = Trim(UCase(MasterNo))
            End If

            If MainClass.ValidateWithMasterTable((txtToEmpCode.Text), "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mToCompanyCode & "") = True Then
                txtJoiningDate.Text = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If


            cboUnitFrom.Enabled = False
            txtFromEmpCode.Enabled = False
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsEmpTrf, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo DataErr
        Dim SqlStr As String = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification = False Then GoTo DataErr
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtFromEmpCode_Validating(txtFromEmpCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

DataErr:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
        MsgInformation(" Unable to Save")
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mFromCompanyCode As Integer
        Dim mToCompanyCode As Integer
        Dim mFromEmpCode As String
        Dim mToEmpCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""

        If MainClass.ValidateWithMasterTable((cboUnitFrom.Text), "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
            mFromCompanyCode = MasterNo
        Else
            MsgInformation("Invalid From Company Name")
            Update1 = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((cboUnitTo.Text), "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
            mToCompanyCode = MasterNo
        Else
            MsgInformation("Invalid To Company Name")
            Update1 = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtFromEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromCompanyCode & "") = True Then
            mFromEmpCode = MasterNo
        Else
            MsgInformation("Invalid From Employee Code")
            Update1 = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtToEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mToCompanyCode & "") = True Then
            mToEmpCode = MasterNo
        Else
            MsgInformation("Invalid To Employee Code")
            Update1 = False
            Exit Function
        End If


        If ADDMode = True Then
            SqlStr = " INSERT INTO PAY_EMP_TRF_MST ( " & vbCrLf & " FROM_COMPANY_CODE, FROM_EMP_CODE, " & vbCrLf & " TO_COMPANY_CODE, TO_EMP_CODE, " & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & mFromCompanyCode & ", '" & MainClass.AllowSingleQuote(mFromEmpCode) & "', " & vbCrLf & " " & mToCompanyCode & ", '" & MainClass.AllowSingleQuote(mToEmpCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '','')"
        Else
            SqlStr = " UPDATE PAY_EMP_TRF_MST SET " & vbCrLf & " FROM_COMPANY_CODE=" & mFromCompanyCode & ", " & vbCrLf & " FROM_EMP_CODE='" & MainClass.AllowSingleQuote(mFromEmpCode) & "', " & vbCrLf & " TO_COMPANY_CODE=" & Val(CStr(mToCompanyCode)) & ", " & vbCrLf & " TO_EMP_CODE='" & MainClass.AllowSingleQuote(mToEmpCode) & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE FROM_COMPANY_CODE= " & xFromCCode & "" & vbCrLf & " AND FROM_EMP_CODE='" & MainClass.AllowSingleQuote(xFromEmpCode) & "'"
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        RsEmpTrf.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub frmEmpTrfRecd_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From PAY_EMP_TRF_MST Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpTrf, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmEmpTrfRecd_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(3930)
        Me.Width = VB6.TwipsToPixelsX(7080)

        Call FillCombo()


        cboUnitFrom.SelectedIndex = 0
        cboUnitTo.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Public Sub FillCombo()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsCbo As ADODB.Recordset

        SqlStr = "Select COMPANY_NAME From GEN_COMPANY_MST ORDER BY COMPANY_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCbo, ADODB.LockTypeEnum.adLockReadOnly)

        cboUnitFrom.Items.Clear()
        cboUnitTo.Items.Clear()
        If RsCbo.EOF = False Then
            Do While RsCbo.EOF = False
                cboUnitFrom.Items.Add(RsCbo.Fields("Company_Name").Value)
                cboUnitTo.Items.Add(RsCbo.Fields("Company_Name").Value)
                RsCbo.MoveNext()
            Loop
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo FieldsVarificationErr
        Dim SqlStr As String = ""

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsEmpTrf.EOF = True Then Exit Function


        If UCase(Trim(cboUnitFrom.Text)) = UCase(Trim(cboUnitTo.Text)) Then
            MsgInformation("Both Unit are Same.")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtLeaveDate.Text) = "" Then
            MsgInformation("Emp Not Leave From Company")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtJoiningDate.Text) = "" Then
            MsgInformation("Emp Not Join To Company")
            FieldsVarification = False
            Exit Function
        End If

        If CDate(txtJoiningDate.Text) < CDate(txtLeaveDate.Text) Then
            MsgInformation("Joing Date is Less Than Leave Date")
            FieldsVarification = False
            Exit Function
        End If

        FieldsVarification = True
        Exit Function
FieldsVarificationErr:
        FieldsVarification = False
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtFromEmpCode.Maxlength = MainClass.SetMaxLength("EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn)
        txtToEmpCode.Maxlength = MainClass.SetMaxLength("EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = "SELECT FROM_COMPANY_CODE,FROM_EMP_CODE,TO_COMPANY_CODE,TO_EMP_CODE " & vbCrLf & " FROM PAY_EMP_TRF_MST ORDER BY FROM_COMPANY_CODE,FROM_EMP_CODE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

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
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean

        On Error GoTo DeleteErr
        SqlStr = ""

        If Trim(xFromEmpCode) = "" Then Delete1 = False : Exit Function
        If Val(CStr(xFromCCode)) = -1 Then Delete1 = False : Exit Function

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_EMP_TRF_MST", (txtFromEmpCode.Text), RsEmpTrf) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_EMP_TRF_MST", "FROM_EMP_CODE", xFromEmpCode) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM PAY_EMP_TRF_MST " & vbCrLf & " WHERE FROM_COMPANY_CODE=" & xFromCCode & "" & vbCrLf & " AND FROM_EMP_CODE='" & MainClass.AllowSingleQuote(xFromEmpCode) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsEmpTrf.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsEmpTrf.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "Department Listing"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\EmpTrf.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtFromEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFromEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFromEmpCode.DoubleClick
        SearchFromEmpCode()
    End Sub

    Private Sub txtFromEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFromEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFromEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFromEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFromEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            SearchFromEmpCode()
        End If
    End Sub

    Private Sub txtFromEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFromEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mFromCompanyCode As Integer
        Dim mFromEmpCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(cboUnitFrom.Text) = "" Then GoTo EventExitSub
        If Trim(txtFromEmpCode.Text) = "" Then GoTo EventExitSub

        txtFromEmpCode.Text = VB6.Format(Val(txtFromEmpCode.Text), "000000")

        If MainClass.ValidateWithMasterTable((cboUnitFrom.Text), "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
            mFromCompanyCode = MasterNo
        Else
            MsgInformation("Invalid From Company Name")
            Cancel = False
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((txtFromEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromCompanyCode & "") = True Then
            mFromEmpCode = MasterNo
        Else
            MsgInformation("Invalid From Employee Code")
            Cancel = True
            GoTo EventExitSub
        End If

        If MODIFYMode = True And RsEmpTrf.EOF = False Then
            xFromCCode = RsEmpTrf.Fields("FROM_COMPANY_CODE").Value
            xFromEmpCode = RsEmpTrf.Fields("FROM_EMP_CODE").Value
        End If

        SqlStr = ""
        SqlStr = " SELECT * from  PAY_EMP_TRF_MST " & vbCrLf & " WHERE FROM_COMPANY_CODE=" & mFromCompanyCode & "" & vbCrLf & " AND FROM_EMP_CODE='" & MainClass.AllowSingleQuote(Trim(mFromEmpCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpTrf, ADODB.LockTypeEnum.adLockReadOnly)
        If RsEmpTrf.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else

            SqlStr = ""
            SqlStr = " SELECT EMP_NAME,EMP_LEAVE_DATE from  PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & mFromCompanyCode & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(Trim(mFromEmpCode)) & "' "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


            If RsTemp.EOF = False Then
                txtFromEmpName.Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
                txtLeaveDate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")
            End If

            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from  PAY_EMP_TRF_MST " & vbCrLf & " WHERE FROM_COMPANY_CODE=" & xFromCCode & " AND FROM_EMP_CODE='" & xFromEmpCode & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpTrf, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtToEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToEmpCode.DoubleClick
        SearchToEmpCode()
    End Sub

    Private Sub txtToEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtToEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtToEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            SearchToEmpCode()
        End If
    End Sub
    Private Sub SearchFromEmpCode()
        Dim mFromCompanyCode As Integer


        If MainClass.ValidateWithMasterTable((cboUnitFrom.Text), "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
            mFromCompanyCode = MasterNo
        End If

        SqlStr = "COMPANY_CODE=" & mFromCompanyCode & ""

        If MainClass.SearchGridMaster((txtFromEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtFromEmpCode.Text = AcName1
            txtFromEmpCode_Validating(txtFromEmpCode, New System.ComponentModel.CancelEventArgs(False))
            If txtFromEmpCode.Enabled = True Then txtFromEmpCode.Focus()
        End If

        Exit Sub

    End Sub
    Private Sub SearchToEmpCode()
        Dim mToCompanyCode As Integer


        If MainClass.ValidateWithMasterTable((cboUnitTo.Text), "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
            mToCompanyCode = MasterNo
        End If

        SqlStr = "COMPANY_CODE=" & mToCompanyCode & ""

        If MainClass.SearchGridMaster((txtToEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtToEmpCode.Text = AcName1
            txtToEmpCode_Validating(txtToEmpCode, New System.ComponentModel.CancelEventArgs(False))
            If txtToEmpCode.Enabled = True Then txtToEmpCode.Focus()
        End If

        Exit Sub

    End Sub
    Private Sub txtToEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mToCompanyCode As Integer
        Dim mToEmpCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(cboUnitTo.Text) = "" Then GoTo EventExitSub
        If Trim(txtToEmpCode.Text) = "" Then GoTo EventExitSub
        txtToEmpCode.Text = VB6.Format(Val(txtToEmpCode.Text), "000000")

        If MainClass.ValidateWithMasterTable((cboUnitTo.Text), "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
            mToCompanyCode = MasterNo
        Else
            MsgInformation("Invalid To Company Name")
            Cancel = False
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((txtToEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mToCompanyCode & "") = True Then
            mToEmpCode = MasterNo
        Else
            MsgInformation("Invalid From Employee Code")
            Cancel = True
            GoTo EventExitSub
        End If

        SqlStr = ""
        SqlStr = " SELECT EMP_NAME,EMP_DOJ from  PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & mToCompanyCode & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(Trim(mToEmpCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            txtToEmpName.Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
            txtJoiningDate.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
