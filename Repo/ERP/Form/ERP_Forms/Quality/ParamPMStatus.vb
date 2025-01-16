Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamPMStatus
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColCategory As Short = 1
    Private Const ColCheckPoint As Short = 2
    Private Const ColDate1 As Short = 3

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboPMOnCondition_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPMOnCondition.SelectedIndexChanged
        If cboPMOnCondition.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboPMOnCondition.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboPMOnCondition.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboPMOnCondition.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboPMOnCondition.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
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

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPMStatus(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtMachineNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnPMStatus(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnPMStatus(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Check Points & Their Status during Preventive Maintenance"

        If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCheckType.Text) <> "" Then
            mSubTitle = mSubTitle & " [CHECK TYPE : " & Trim(txtCheckType.Text) & "]"
        End If

        If cboPMOnCondition.Text = "Between" Then
            mSubTitle = mSubTitle & " [PM Done Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboPMOnCondition.Text = "After" Then
            mSubTitle = mSubTitle & " [PM Done After  " & txtDate1.Text & " ]"
        End If
        If cboPMOnCondition.Text = "Before" Then
            mSubTitle = mSubTitle & " [PM Done Before  " & txtDate1.Text & " ]"
        End If
        If cboPMOnCondition.Text = "On Date" Then
            mSubTitle = mSubTitle & " [PM Done On  " & txtDate1.Text & " ]"
        End If

        If FillPrintDummyData(SprdMain, 0, SprdMain.MaxRows - 1, 1, SprdMain.MaxCols) = False Then GoTo ReportErr

        SqlStr = FetchRecordForReport()

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\PMStatus.rpt"

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
                '            If FieldNum = prmStartGridCol Then
                '                SetData = "FIELD" & FieldCnt
                '                GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                '            Else
                SetData = SetData & ", " & "FIELD" & FieldCnt
                If RowNum = 0 Then
                    If FieldCnt <= 6 Then
                        GetData = GetData & ",''"
                    Else
                        GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(Mid(GridName.Text, 12, 10)) & "'"
                    End If
                ElseIf RowNum >= prmEndGridRow - 3 Then
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                Else
                    If FieldCnt <= 6 Then
                        GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                    Else
                        GetData = GetData & ", " & "'" & IIf(GridName.Value = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'"
                    End If
                End If
                '            End If
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
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Sub frmParamPMStatus_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Check Points & Their Status during Preventive Maintenance"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamPMStatus_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        cboPMOnCondition.Items.Clear()
        cboPMOnCondition.Items.Add("None")
        cboPMOnCondition.Items.Add("Between")
        cboPMOnCondition.Items.Add("After")
        cboPMOnCondition.Items.Add("Before")
        cboPMOnCondition.Items.Add("On Date")
        cboPMOnCondition.SelectedIndex = 0
    End Sub

    Private Sub frmParamPMStatus_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mAutoKey As Integer
        If eventArgs.col <= ColCheckPoint Then Exit Sub
        SprdMain.Row = SprdMain.MaxRows
        SprdMain.Col = eventArgs.col
        mAutoKey = Val(SprdMain.Text)
        frmPMStatus.MdiParent = Me.MdiParent
        frmPMStatus.frmPMStatus_Activated(Nothing, New System.EventArgs())
        frmPMStatus.Show()
        If frmPMStatus.CmdAdd.Enabled = True Then frmPMStatus.cmdAdd_Click(Nothing, New System.EventArgs())
        frmPMStatus.txtSlipNo.Text = CStr(mAutoKey)
        frmPMStatus.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim mRsDate As ADODB.Recordset
        Dim mRsCheckPoint As ADODB.Recordset
        Dim mRsStatus As ADODB.Recordset
        Dim mRow As Integer
        Dim mCol As Integer

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL

        SqlStr1 = " SELECT DISTINCT AUTO_KEY_PM, PM_DATE, CHECK_TYPE, " & vbCrLf & " REMARKS,DONE_BY,APP_BY " & vbCrLf & " FROM ( " & SqlStr & " ) ORDER BY PM_DATE"
        MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsDate, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsDate.EOF = True Then
            Show1 = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        FormatSprdMain(-1, mRsDate)

        SqlStr2 = " SELECT DISTINCT SERIAL_NO, CATEGORY, CHECK_POINT " & vbCrLf & " FROM ( " & SqlStr & " ) ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr2, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheckPoint, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            mRow = 1
            Do While Not mRsCheckPoint.EOF
                .Row = mRow

                .Col = ColCategory
                .Text = Trim(IIf(IsDbNull(mRsCheckPoint.Fields("CATEGORY").Value), "", mRsCheckPoint.Fields("CATEGORY").Value))

                .Col = ColCheckPoint
                .Text = Trim(IIf(IsDbNull(mRsCheckPoint.Fields("CHECK_POINT").Value), "", mRsCheckPoint.Fields("CHECK_POINT").Value))

                mCol = ColCheckPoint
                mRsDate.MoveFirst()
                Do While Not mRsDate.EOF
                    mCol = mCol + 1

                    SqlStr3 = " SELECT SERIAL_NO, CATEGORY, CHECK_POINT, " & vbCrLf & " PM_DATE, STATUS " & vbCrLf & " FROM ( " & SqlStr & " ) " & vbCrLf & " WHERE SERIAL_NO=" & mRsCheckPoint.Fields("SERIAL_NO").Value & " AND CATEGORY='" & mRsCheckPoint.Fields("CATEGORY").Value & "' " & vbCrLf & " AND CHECK_POINT='" & mRsCheckPoint.Fields("CHECK_POINT").Value & "' " & vbCrLf & " AND PM_DATE=TO_DATE('" & VB6.Format(mRsDate.Fields("PM_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND CHECK_TYPE='" & mRsDate.Fields("CHECK_TYPE").Value & "' "
                    MainClass.UOpenRecordSet(SqlStr3, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsStatus, ADODB.LockTypeEnum.adLockReadOnly)

                    .Col = mCol
                    If mRsStatus.EOF = False Then
                        .Value = IIf(mRsStatus.Fields("Status").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    End If

                    mRsDate.MoveNext()
                Loop

                mRsCheckPoint.MoveNext()
                mRow = mRow + 1
                .MaxRows = mRow
            Loop

            .Row = mRow

            .Col = ColCategory
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "CHECK TYPE"

            mCol = ColCheckPoint
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("CHECK_TYPE").Value), "", mRsDate.Fields("CHECK_TYPE").Value))
                mRsDate.MoveNext()
            Loop

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            .Col = ColCategory
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "REMARKS"

            mCol = ColCheckPoint
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("REMARKS").Value), "", mRsDate.Fields("REMARKS").Value))
                mRsDate.MoveNext()
            Loop

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            .Col = ColCategory
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "DONE BY"

            mCol = ColCheckPoint
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("DONE_BY").Value), "", mRsDate.Fields("DONE_BY").Value))
                mRsDate.MoveNext()
            Loop

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            .Col = ColCategory
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "APPROVED BY"

            mCol = ColCheckPoint
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("APP_BY").Value), "", mRsDate.Fields("APP_BY").Value))
                mRsDate.MoveNext()
            Loop

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            mCol = ColCheckPoint
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("AUTO_KEY_PM").Value), "", mRsDate.Fields("AUTO_KEY_PM").Value))
                mRsDate.MoveNext()
            Loop
            .RowHidden = True

            .Row = .MaxRows - 4
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
            .BlockMode = False

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
        End With

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub FormatSprdMain(ByRef Arow As Integer, ByRef mRsDate As ADODB.Recordset)

        Dim mRow As Integer
        Dim mCol As Integer

        With SprdMain
            mCol = ColCheckPoint
            .MaxCols = mCol
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCheckPoint
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .MaxCols = mCol
                .Col = mCol

                .Row = 0
                .Text = "Status    (" & VB6.Format(mRsDate.Fields("PM_DATE").Value, "DD/MM/YYYY") & ")"

                .Row = -1
                .CellType = SS_CELL_TYPE_CHECKBOX
                .TypeEditLen = 255
                .TypeHAlign = SS_CELL_H_ALIGN_CENTER
                .TypeEditMultiLine = False
                .set_ColWidth(mCol, 9)

                mRsDate.MoveNext()
            Loop

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, mCol)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        MakeSQL = " SELECT A.AUTO_KEY_PM, PM_DATE, CHECK_TYPE, A.REMARKS, DONE_BY, APP_BY, " & vbCrLf & " SERIAL_NO, CATEGORY, CHECK_POINT, STATUS " & vbCrLf & " FROM MAN_MACHINE_PM_HDR A,MAN_MACHINE_PM_DET B" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.AUTO_KEY_PM=B.AUTO_KEY_PM "

        If Trim(txtMachineNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        End If

        If chkAllCheckType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCheckType.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckType.Text) & "'"
        End If

        If cboPMOnCondition.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PM_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboPMOnCondition.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PM_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboPMOnCondition.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PM_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboPMOnCondition.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND PM_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY PM_DATE "

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
        If cboPMOnCondition.Text = "Between" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
            If Not IsDate(txtDate2.Text) Then
                MsgBox("Date2 is Blank.")
                txtDate2.Focus()
                Exit Function
            End If
        End If
        If cboPMOnCondition.Text = "After" Or cboPMOnCondition.Text = "Before" Or cboPMOnCondition.Text = "On Date" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
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
            SqlStr = " SELECT AUTO_KEY_CP " & vbCrLf _
                            & " FROM MAN_MACHINE_CP_HDR " & vbCrLf _
                            & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                            & " AND MACHINE_DESC ='" & MainClass.AllowSingleQuote(lblDescription.text) & "' " & vbCrLf _
                            & " AND MACHINE_SPEC ='" & MainClass.AllowSingleQuote(lblSpec.text) & "' "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If mRsTemp.EOF Then
                MsgBox("Check Points not defined.")
                Cancel = True
            End If
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
End Class
