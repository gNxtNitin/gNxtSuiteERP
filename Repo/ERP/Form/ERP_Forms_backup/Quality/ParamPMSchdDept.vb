Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamPMSchdDept
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColMachineNo As Short = 1
    Private Const ColMachineDesc As Short = 2
    Private Const ColCheckType As Short = 3
    Private Const ColPlanActual As Short = 4
    Private Const ColJan1 As Short = 5
    Private Const ColJan2 As Short = 6
    Private Const ColFeb1 As Short = 7
    Private Const ColFeb2 As Short = 8
    Private Const ColMar1 As Short = 9
    Private Const ColMar2 As Short = 10
    Private Const ColApr1 As Short = 11
    Private Const ColApr2 As Short = 12
    Private Const ColMay1 As Short = 13
    Private Const ColMay2 As Short = 14
    Private Const ColJun1 As Short = 15
    Private Const ColJun2 As Short = 16
    Private Const ColJul1 As Short = 17
    Private Const ColJul2 As Short = 18
    Private Const ColAug1 As Short = 19
    Private Const ColAug2 As Short = 20
    Private Const ColSep1 As Short = 21
    Private Const ColSep2 As Short = 22
    Private Const ColOct1 As Short = 23
    Private Const ColOct2 As Short = 24
    Private Const ColNov1 As Short = 25
    Private Const ColNov2 As Short = 26
    Private Const ColDec1 As Short = 27
    Private Const ColDec2 As Short = 28
    Private Const ColRemarks As Short = 29

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
        If Trim(txtDeptCode.Text) = "" Then
            If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked Then MsgInformation("Please select the Dept")
            chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Checked
            txtDeptCode.Focus()
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
        If Trim(txtDeptCode.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPMSchd(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtDeptCode.Text) = "" Then Exit Sub
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
        mTitle = "Department Wise Preventive Maintenance Schedule"

        mSubTitle = mSubTitle & " [ YEAR : " & cboYear.Text & " ]"
        If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCheckType.Text) <> "" Then
            mSubTitle = mSubTitle & " [ CHECK TYPE : " & Trim(txtCheckType.Text) & " ]"
        End If

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr

        SqlStr = FetchRecordForReport()

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\PMSchdHisDept.rpt"

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
            FieldCnt = 3
            SetData = "FIELD1,FIELD2"
            GetData = "'" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblDescription.Text) & "'"

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
        If Trim(txtDeptCode.Text) = "" Then
            MsgInformation("Please select the Dept")
            txtDeptCode.Focus()
            Exit Sub
        End If
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND MACHINE_NO IN (" & vbCrLf _
                    & " SELECT MACHINE_NO FROM MAN_MACHINE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'" & vbCrLf _
                    & " ) "

        If MainClass.SearchGridMasterBySQL2(txtCheckType.Text, SqlStr) = True Then
            txtCheckType.Text = AcName
        End If
        If txtCheckType.Enabled = True Then txtCheckType.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        Dim SqlStr As String
        SqlStr = " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "", "", SqlStr) = True Then
            txtDeptCode.Text = AcName1
            lblDescription.text = AcName
            If txtDeptCode.Enabled = True Then txtDeptCode.Focus()
        End If
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        ClearScreen()
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ClearScreen()

        Dim mCol As Short
        MainClass.ClearGrid(SprdMain, RowHeight)
        With SprdMain
            .MaxRows = 2

            .Row = 1
            .Col = ColPlanActual
            .Text = "Plan"
            .Row2 = 1
            .Col = ColMachineNo
            .Col2 = ColRemarks
            .BlockMode = True
            '        .BackColorStyle = BackColorStyleUnderGrid
            .BackColor = System.Drawing.ColorTranslator.FromOle(ClrWhite)
            .BlockMode = False

            .Row = 2
            .Col = ColPlanActual
            .Text = "Actual"
            .Row2 = 2
            .Col = ColMachineNo
            .Col2 = ColRemarks
            .BlockMode = True
            '        .BackColorStyle = BackColorStyleUnderGrid
            .BackColor = System.Drawing.ColorTranslator.FromOle(ClrGrey)
            .BlockMode = False
        End With
    End Sub

    Public Sub frmParamPMSchdDept_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Me.Text = "Department Wise Preventive Maintenance Schedule"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        cboYear.Text = CStr(Year(RunDate))
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamPMSchdDept_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

    Private Sub frmParamPMSchdDept_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntRow As Integer
        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColRemarks
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMachineNo, 8)

            .Col = ColMachineDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMachineDesc, 25)

            .Col = ColCheckType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCheckType, 9)

            .Col = ColPlanActual
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPlanActual, 5)

            For cntCol = ColJan1 To ColDec2
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .TypeEditLen = 255
                .set_ColWidth(cntCol, 4)
            Next

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 15)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&H80000012)
            .BackColorStyle = FPSpreadADO.BackColorStyleConstants.BackColorStyleUnderGrid
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mRsSchd As ADODB.Recordset
        Dim mMachineNo As String
        Dim mCheckType As String
        Dim I As Short
        Dim mCol As Short

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsSchd, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsSchd.EOF = True Then
            Show1 = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgInformation("No Schedule is available for this Department in this Year.")
            Exit Function
        End If

        I = 1
        With SprdMain
            Do While Not mRsSchd.EOF
                mMachineNo = mRsSchd.Fields("MACHINE_NO").Value

                .MaxRows = I + 1

                .Row = I
                .Row2 = I
                .Col = ColMachineNo
                .Col2 = ColRemarks
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(ClrWhite)
                .BlockMode = False

                .Row = I + 1
                .Row2 = I + 1
                .Col = ColMachineNo
                .Col2 = ColRemarks
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(ClrGrey)
                .BlockMode = False

                .Row = I

                .Col = ColMachineNo
                .Text = Trim(IIf(IsDbNull(mRsSchd.Fields("MACHINE_NO").Value), "", mRsSchd.Fields("MACHINE_NO").Value))

                .Col = ColMachineDesc
                .Text = Trim(IIf(IsDbNull(mRsSchd.Fields("MACHINE_DESC").Value), "", mRsSchd.Fields("MACHINE_DESC").Value))

                Do While (mMachineNo = mRsSchd.Fields("MACHINE_NO").Value)
                    mCheckType = mRsSchd.Fields("CHECK_TYPE").Value

                    .MaxRows = I + 1
                    .Row = I

                    .Col = ColCheckType
                    .Text = Trim(IIf(IsDbNull(mRsSchd.Fields("CHECK_TYPE").Value), "", mRsSchd.Fields("CHECK_TYPE").Value))

                    Do While (mMachineNo = mRsSchd.Fields("MACHINE_NO").Value) And (mCheckType = mRsSchd.Fields("CHECK_TYPE").Value)
                        .Row = I

                        .Col = ColPlanActual
                        .Text = "Plan"

                        .Col = IIf(VB.Day(mRsSchd.Fields("PM_DUE").Value) <= 15, (mRsSchd.Fields("SCHD_MONTH").Value * 2) + 3, (mRsSchd.Fields("SCHD_MONTH").Value * 2) + 4)
                        .Text = "Y"
                        .BackColor = System.Drawing.ColorTranslator.FromOle(ClrYellow)

                        .Row = I + 1

                        .Col = ColPlanActual
                        .Text = "Actual"

                        If IsDbNull(mRsSchd.Fields("PM_DONE").Value) Then
                            .Col = IIf(VB.Day(mRsSchd.Fields("PM_DUE").Value) <= 15, (mRsSchd.Fields("SCHD_MONTH").Value * 2) + 3, (mRsSchd.Fields("SCHD_MONTH").Value * 2) + 4)
                            .Text = Trim(IIf(IsDbNull(mRsSchd.Fields("NOT_ACH_REASON").Value), "", mRsSchd.Fields("NOT_ACH_REASON").Value))
                            .BackColor = System.Drawing.ColorTranslator.FromOle(ClrRed)

                            .Col = ColRemarks
                            .Text = Trim(.Text) & IIf(IsDbNull(mRsSchd.Fields("NEXT_DUE").Value), "", " Next Due:" & mRsSchd.Fields("NEXT_DUE").Value)
                        Else
                            .Col = IIf(VB.Day(mRsSchd.Fields("PM_DONE").Value) <= 15, (Month(mRsSchd.Fields("PM_DONE").Value) * 2) + 3, (Month(mRsSchd.Fields("PM_DONE").Value) * 2) + 4)
                            .Text = "Y"
                            .BackColor = System.Drawing.ColorTranslator.FromOle(ClrGreen)
                        End If

                        mRsSchd.MoveNext()
                        If mRsSchd.EOF Then
                            Exit Do
                        End If
                    Loop
                    If mRsSchd.EOF Then
                        Exit Do
                    End If
                    I = I + 2
                Loop
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

        MakeSQL = " SELECT MAN_MACHINE_SCHD_DET.MACHINE_NO, MACHINE_DESC, SCHD_MONTH, CHECK_TYPE, " & vbCrLf & " PM_DUE, PM_DONE, NOT_ACH_REASON, NEXT_DUE " & vbCrLf & " FROM MAN_MACHINE_SCHD_HDR, MAN_MACHINE_SCHD_DET, MAN_MACHINE_MST " & vbCrLf & " WHERE MAN_MACHINE_SCHD_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.AUTO_KEY_SCHD=MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD " & vbCrLf & " AND SUBSTR(MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD,LENGTH(MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD)-1,2)=MAN_MACHINE_MST.COMPANY_CODE " & vbCrLf & " AND MAN_MACHINE_SCHD_DET.MACHINE_NO=MAN_MACHINE_MST.MACHINE_NO "

        If Trim(cboYear.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND SCHD_YEAR=" & Val(cboYear.Text) & " "
        End If

        If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCheckType.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND MAN_MACHINE_SCHD_DET.MACHINE_NO IN ( " & vbCrLf & " SELECT MACHINE_NO FROM MAN_MACHINE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        If Trim(txtDeptCode.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ) "

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY MAN_MACHINE_SCHD_DET.MACHINE_NO,CHECK_TYPE,SCHD_MONTH "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If Trim(txtDeptCode.Text) = "" Then
            MsgBox("Please Select Dept.")
            txtDeptCode.Focus()
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
        If Trim(txtDeptCode.Text) = "" Then
            MsgBox("Please Select Dept")
            txtDeptCode.Focus()
            GoTo EventExitSub
        End If
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "' " & vbCrLf _
                    & " AND MACHINE_NO IN (" & vbCrLf _
                    & " SELECT MACHINE_NO FROM MAN_MACHINE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'" & vbCrLf _
                    & " ) "

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

    Private Sub txtDeptCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDeptCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeptCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeptCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDeptCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeptCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Public Sub txtDeptCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeptCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtDeptCode.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.ValidateWithMasterTable(txtDeptCode.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblDescription.text = MasterNo
        Else
            MsgBox("Not a valid Dept.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmParamPMSchdDept_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        'FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'SSTab1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
