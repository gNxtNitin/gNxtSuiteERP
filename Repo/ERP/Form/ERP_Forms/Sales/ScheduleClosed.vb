Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmScheduleClosed
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Private Const RowHeight As Short = 15

    Private Const ColDSNo As Short = 1
    Private Const ColDSDate As Short = 2
    Private Const ColPartyName As Short = 3
    Private Const ColClosedStatus As Short = 4


    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""

        Dim mDSNo As Double
        Dim mUpdateCount As Integer
        Dim mSchdDate As String
        Dim mClosedStatus As String

        If Not IsDate(txtSchdDate.Text) Then
            MsgBox("Invalid Schedule Date")
            txtSchdDate.Focus()
            Exit Sub
        End If

        mSchdDate = UCase(VB6.Format(txtSchdDate.Text, "MMM-YYYY"))

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColDSNo
                mDSNo = Val(.Text)

                .Col = ColClosedStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    mClosedStatus = "Y" ''Closed all DS
                    '            Else
                    '                mClosedStatus = "N"     ''Opened all DS
                    '            End If

                    SqlStr = "UPDATE PUR_DELV_SCHLD_HDR " & vbCrLf & " SET SCHLD_STATUS='" & mClosedStatus & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_DELV=" & mDSNo & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SCHLD_DATE,'MON-YYYY')='" & mSchdDate & "'"

                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1
                End If

            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Delivery Schedule Closed.", MsgBoxStyle.Information)
        Call ShowStatus(True)
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()

        FormatSprdMain()

        Call ShowStatus(False)
    End Sub
    Private Sub ShowStatus(ByRef pButtonStatus As Object)
        cmdShow.Enabled = pButtonStatus
        CmdSave.Enabled = Not pButtonStatus
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmScheduleClosed_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmScheduleClosed_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        txtSchdDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = MakeSQL

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub

    Private Function MakeSQL() As String
        On Error GoTo ErrPart
        Dim mSchdDate As String

        mSchdDate = UCase(VB6.Format(txtSchdDate.Text, "MMM-YYYY"))
        MakeSQL = "SELECT IH.AUTO_KEY_DELV, IH.DELV_SCHLD_DATE,  " & vbCrLf & " CMST.SUPP_CUST_NAME,CASE WHEN SCHLD_STATUS='Y' THEN '1' ELSE '0' END AS ClosedStatus " & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH,FIN_SUPP_CUST_MST CMST" & vbCrLf & " WHERE IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.Company_Code=CMST.Company_Code " & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'MON-YYYY')='" & mSchdDate & "'"


        MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.DELV_SCHLD_DATE,IH.AUTO_KEY_DELV"

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColClosedStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColDSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDSNo, 9)

            .Col = ColDSDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColDSDate, 9)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 33)

            .Col = ColClosedStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColClosedStatus, 8)
            '    .Value = vbUnchecked

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColPartyName)
            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColDSNo
            .Text = "DS No."

            .Col = ColDSDate
            .Text = "DS Date"

            .Col = ColPartyName
            .Text = "Supplier Name"

            .Col = ColClosedStatus
            .Text = "Closed Status"
        End With
    End Sub
    Private Sub frmScheduleClosed_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            Call ShowStatus(False)
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColClosedStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub
    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub txtSchdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSchdDate.TextChanged
        Call ShowStatus(True)
    End Sub

    Private Sub txtSchdDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSchdDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtSchdDate.Text = "" Then GoTo EventExitSub
        If Not IsDate(txtSchdDate.Text) Then
            MsgBox("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        ElseIf FYChk((txtSchdDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If

        txtSchdDate.Text = "01/" & VB6.Format(txtSchdDate.Text, "MM/YYYY")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
