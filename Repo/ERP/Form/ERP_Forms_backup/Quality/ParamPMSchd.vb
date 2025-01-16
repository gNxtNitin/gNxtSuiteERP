Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamPMSchd
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColMonth As Short = 1
    Private Const ColResponsibility As Short = 2
    Private Const ColPlanActual As Short = 3
    Private Const ColDay1 As Short = 4
    Private Const ColDay2 As Short = 5
    Private Const ColDay3 As Short = 6
    Private Const ColDay4 As Short = 7
    Private Const ColDay5 As Short = 8
    Private Const ColDay6 As Short = 9
    Private Const ColDay7 As Short = 10
    Private Const ColDay8 As Short = 11
    Private Const ColDay9 As Short = 12
    Private Const ColDay10 As Short = 13
    Private Const ColDay11 As Short = 14
    Private Const ColDay12 As Short = 15
    Private Const ColDay13 As Short = 16
    Private Const ColDay14 As Short = 17
    Private Const ColDay15 As Short = 18
    Private Const ColDay16 As Short = 19
    Private Const ColDay17 As Short = 20
    Private Const ColDay18 As Short = 21
    Private Const ColDay19 As Short = 22
    Private Const ColDay20 As Short = 23
    Private Const ColDay21 As Short = 24
    Private Const ColDay22 As Short = 25
    Private Const ColDay23 As Short = 26
    Private Const ColDay24 As Short = 27
    Private Const ColDay25 As Short = 28
    Private Const ColDay26 As Short = 29
    Private Const ColDay27 As Short = 30
    Private Const ColDay28 As Short = 31
    Private Const ColDay29 As Short = 32
    Private Const ColDay30 As Short = 33
    Private Const ColDay31 As Short = 34
    Private Const ColRemarks As Short = 35

    Private Const ClrYellow As Integer = &H80FFFF
    Private Const ClrGreen As Integer = &H80FF80
    Private Const ClrRed As Integer = &H8080FF
    Private Const ClrWhite As Integer = &H80000005
    Private Const ClrGrey As Integer = &HE0E0E0

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAllCheckType_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCheckType.CheckStateChanged
        If Trim(txtMachineNo.Text) = "" Then
            If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked Then MsgInformation("Please select the Machine")
            chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Checked
            txtMachineNo.Focus()
            Exit Sub
        End If
        If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCheckType.Enabled = False
            cmdSearchCheckType.Enabled = False
        Else
            txtCheckType.Enabled = True
            cmdSearchCheckType.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPMSchd(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPMSchd(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnPMSchd(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Preventive Maintenance Schedule for Machines"

        mSubTitle = mSubTitle & "[ YEAR : " & cboYear.Text & " ]"
        If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCheckType.Text) <> "" Then
            mSubTitle = mSubTitle & " [ CHECK TYPE : " & Trim(txtCheckType.Text) & " ]"
        End If

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr

        SqlStr = FetchRecordForReport()

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\PMSchdHis.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Function FillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer) As Boolean

        ''' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 5
            SetData = "FIELD1,FIELD2,FIELD3,FIELD4"
            GetData = "'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblDescription.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblSpec.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblLocation.Text) & "'"

            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                SetData = SetData & ", " & "FIELD" & FieldCnt
                If (RowNum Mod 2) = 0 And (FieldNum = 1 Or FieldNum = 2) Then
                    GetData = GetData & ", " & "''"
                Else
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
NextRec:
        Next
        PubDBCn.CommitTrans()
        FillPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyData = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function FetchRecordForReport() As String

        Dim mSqlStr As String

        mSqlStr = " SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdSearchCheckType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCheckType.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        If Trim(txtMachineNo.Text) = "" Then
            MsgInformation("Please select the Machine")
            txtMachineNo.Focus()
            Exit Sub
        End If
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "

        If MainClass.SearchGridMasterBySQL2(txtCheckType.Text, SqlStr) = True Then
            txtCheckType.Text = AcName
        End If
        If txtCheckType.Enabled = True Then txtCheckType.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMachineNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineNo.Click
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", "", "", SqlStr) = True Then
            txtMachineNo.Text = AcName1
            lblDescription.text = AcName
            If txtMachineNo.Enabled = True Then txtMachineNo.Focus()
        End If
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        ClearScreen()
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ClearScreen()
        Dim mRow As Short
        Dim mCol As Short
        For mRow = 1 To 24
            For mCol = 2 To 35
                If mCol <> 3 Then
                    SprdMain.Row = mRow
                    SprdMain.Col = mCol
                    SprdMain.Text = ""
                    SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(IIf((mRow Mod 2) = 0, ClrGrey, ClrWhite))
                End If
            Next
        Next
    End Sub

    Public Sub frmParamPMSchd_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Me.Text = "Preventive Maintenance Schedule for Machines"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        cboYear.Text = CStr(Year(RunDate))
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamPMSchd_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        SprdMain.Row = 1
        SprdMain.Col = 1
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(11565)

        Call FillCbo()

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillCbo()
        Dim I As Short
        cboYear.Items.Clear()
        For I = 1970 To 2200
            cboYear.Items.Add(CStr(I))
        Next
    End Sub

    Private Sub frmParamPMSchd_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mRsSchd As ADODB.Recordset

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsSchd, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsSchd.EOF = True Then
            Show1 = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgInformation("No Schedule is available for this Machine in this Year.")
            Exit Function
        End If

        With SprdMain
            Do While Not mRsSchd.EOF
                .Row = (mRsSchd.Fields("SCHD_MONTH").Value * 2) - 1

                .Col = ColResponsibility
                .Text = Trim(IIf(IsDbNull(mRsSchd.Fields("RESPONSIBILITY").Value), "", mRsSchd.Fields("RESPONSIBILITY").Value))

                .Col = VB.Day(mRsSchd.Fields("PM_DUE").Value) + 3
                .Text = "Y"
                .BackColor = System.Drawing.ColorTranslator.FromOle(ClrYellow)

                .Row = .Row + 1

                .Col = ColResponsibility
                .Text = Trim(IIf(IsDbNull(mRsSchd.Fields("RESPONSIBILITY").Value), "", mRsSchd.Fields("RESPONSIBILITY").Value))

                If IsDbNull(mRsSchd.Fields("PM_DONE").Value) Then
                    .Col = VB.Day(mRsSchd.Fields("PM_DUE").Value) + 3
                    .Text = Trim(IIf(IsDbNull(mRsSchd.Fields("NOT_ACH_REASON").Value), "", mRsSchd.Fields("NOT_ACH_REASON").Value))
                    .BackColor = System.Drawing.ColorTranslator.FromOle(ClrRed)

                    .Col = ColRemarks
                    .Text = Trim(IIf(IsDbNull(mRsSchd.Fields("NEXT_DUE").Value), "", "Next Due:" & mRsSchd.Fields("NEXT_DUE").Value))
                Else
                    .Col = VB.Day(mRsSchd.Fields("PM_DONE").Value) + 3
                    .Text = "Y"
                    .BackColor = System.Drawing.ColorTranslator.FromOle(ClrGreen)
                End If

                mRsSchd.MoveNext()
            Loop
        End With

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        MakeSQL = " SELECT SCHD_MONTH, CHECK_TYPE, RESPONSIBILITY, PM_DUE, " & vbCrLf & " PM_DONE, NOT_ACH_REASON, NEXT_DUE " & vbCrLf & " FROM MAN_MACHINE_SCHD_HDR, MAN_MACHINE_SCHD_DET " & vbCrLf & " WHERE MAN_MACHINE_SCHD_HDR.AUTO_KEY_SCHD=MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtMachineNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        End If

        If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCheckType.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "'"
        End If

        If Trim(cboYear.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND SCHD_YEAR=" & Val(cboYear.Text) & " "
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY SCHD_MONTH "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If Trim(txtMachineNo.Text) = "" Then
            MsgBox("Please Select Machine No.")
            txtMachineNo.Focus()
            Exit Function
        End If
        If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCheckType.Text) = "" Then
            MsgBox("Please Select Check Type")
            txtCheckType.Focus()
            Exit Function
        End If
        If Trim(cboYear.Text) = "" Then
            MsgBox("Please Select Year")
            cboYear.Focus()
            Exit Function
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtCheckType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtCheckType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckType.DoubleClick
        Call cmdSearchCheckType_Click(cmdSearchCheckType, New System.EventArgs())
    End Sub

    Private Sub txtCheckType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCheckType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCheckType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCheckType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCheckType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCheckType_Click(cmdSearchCheckType, New System.EventArgs())
    End Sub

    Private Sub txtCheckType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCheckType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(txtCheckType.Text) = "" Then GoTo EventExitSub
        If Trim(txtMachineNo.Text) = "" Then
            MsgBox("Please Select Machine No.")
            txtMachineNo.Focus()
            GoTo EventExitSub
        End If
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' " & vbCrLf _
                    & " AND MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF Then
            MsgBox("Not a valid Check Type", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtMachineNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.DoubleClick
        Call cmdSearchMachineNo_Click(cmdSearchMachineNo, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachineNo_Click(cmdSearchMachineNo, New System.EventArgs())
    End Sub

    Public Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' "
        If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            ShowMachine()
        Else
            MsgBox("Not a valid Machine No.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub ShowMachine()

        On Error GoTo ShowErrPart
        Dim RsMachineMst As ADODB.Recordset
        Dim SqlStr As String

        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                    & " FROM MAN_MACHINE_MST" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineMst, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsMachineMst.EOF Then
            lblDescription.Text = IIf(IsDbNull(RsMachineMst.Fields("MACHINE_DESC").Value), "", RsMachineMst.Fields("MACHINE_DESC").Value)
            lblSpec.Text = IIf(IsDbNull(RsMachineMst.Fields("MACHINE_SPEC").Value), "", RsMachineMst.Fields("MACHINE_SPEC").Value)
            lblLocation.Text = IIf(IsDbNull(RsMachineMst.Fields("Location").Value), "", RsMachineMst.Fields("Location").Value)
        Else
            MsgBox("Machine No Does Not Exist", MsgBoxStyle.Information)
        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub frmParamPMSchd_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
