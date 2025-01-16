Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamVoltmeterCal
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColParamDesc As Short = 1
    Private Const ColReadingStep As Short = 2
    Private Const ColPerError As Short = 3
    Private Const ColDate1 As Short = 4

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboCalonCondition_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCalOnCondition.SelectedIndexChanged
        If cboCalOnCondition.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCalOnCondition.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboCalOnCondition.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCalOnCondition.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCalOnCondition.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        If Trim(txtDocNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnVoltmeterCal(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtDocNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnVoltmeterCal(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnVoltmeterCal(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Process Instruments Calibration History"

        If cboCalOnCondition.Text = "Between" Then
            mSubTitle = mSubTitle & " [Calibrated Done Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboCalOnCondition.Text = "After" Then
            mSubTitle = mSubTitle & " [Calibrated Done After  " & txtDate1.Text & " ]"
        End If
        If cboCalOnCondition.Text = "Before" Then
            mSubTitle = mSubTitle & " [Calibrated Done Before  " & txtDate1.Text & " ]"
        End If
        If cboCalOnCondition.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Calibrated Done On  " & txtDate1.Text & " ]"
        End If

        If FillPrintDummyData(SprdMain, 0, SprdMain.MaxRows - 1, 1, SprdMain.MaxCols) = False Then GoTo ReportErr

        SqlStr = FetchRecordForReport()

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\VoltmeterCalHis.rpt"

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
            FieldCnt = 12
            SetData = "FIELD1,FIELD2,FIELD3,FIELD4,FIELD5,FIELD6,FIELD7,FIELD8,FIELD9,FIELD10,FIELD11"
            GetData = "'" & MainClass.AllowSingleQuote(txtDocNo.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblDescription.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblENo.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblMake.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblRange.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblLC.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblFrequency.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblShuntRatio.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblLocation.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblDept.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblCalibDueDate.Text) & "'"

            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                '            If FieldNum = prmStartGridCol Then
                '                SetData = "FIELD" & FieldCnt
                '                GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                '            Else
                SetData = SetData & ", " & "FIELD" & FieldCnt
                If RowNum = 0 Then
                    If FieldCnt <= 14 Then
                        GetData = GetData & ",''"
                    Else
                        GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(Mid(GridName.Text, 13, 12)) & "'"
                    End If
                Else
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
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

    Private Sub cmdSearchDocNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDocNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STATUS='A' "
        If MainClass.SearchGridMaster("", "QAL_VOLTMETER_MST", "DOCNO", "DESCRIPTION", "E_NO", "RANGE", SqlStr) = True Then
            txtDocNo.Text = AcName
            If txtDocNo.Enabled = True Then txtDocNo.Focus()
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

    Public Sub frmParamVoltmeterCal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Process Instruments Calibration History"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamVoltmeterCal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7245)
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
        cboCalOnCondition.Items.Clear()
        cboCalOnCondition.Items.Add("None")
        cboCalOnCondition.Items.Add("Between")
        cboCalOnCondition.Items.Add("After")
        cboCalOnCondition.Items.Add("Before")
        cboCalOnCondition.Items.Add("On Date")
        cboCalOnCondition.SelectedIndex = 0
    End Sub

    Private Sub frmParamVoltmeterCal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        If eventArgs.col <= ColPerError Then Exit Sub
        SprdMain.Row = SprdMain.MaxRows
        SprdMain.Col = eventArgs.col
        mAutoKey = Val(SprdMain.Text)
        frmVoltmeterCal.MdiParent = Me.MdiParent
        frmVoltmeterCal.frmVoltmeterCal_Activated(Nothing, New System.EventArgs())
        frmVoltmeterCal.Show()
        frmVoltmeterCal.txtSlipNo.Text = CStr(mAutoKey)
        frmVoltmeterCal.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim mRsDate As ADODB.Recordset
        Dim mRsParam As ADODB.Recordset
        Dim mRsObs As ADODB.Recordset
        Dim mRow As Integer
        Dim mCol As Integer
        Dim mInspMth As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL

        SqlStr1 = "SELECT DISTINCT AUTO_KEY_CALIB,CALIB_DATE,MASTER_INST,REMARKS,CALIB_BY,APPROVED_BY FROM ( " & SqlStr & " ) ORDER BY CALIB_DATE"
        MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsDate, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsDate.EOF = True Then
            Show1 = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        FormatSprdMain(-1, mRsDate)

        SqlStr2 = " SELECT DISTINCT SERIAL_NO,PARAM_DESC,READING_STEP,PER_ERROR" & vbCrLf & " FROM ( " & SqlStr & " ) ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr2, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsParam, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            mRow = 1
            Do While Not mRsParam.EOF
                .Row = mRow

                .Col = ColParamDesc
                .Text = Trim(IIf(IsDbNull(mRsParam.Fields("PARAM_DESC").Value), "", mRsParam.Fields("PARAM_DESC").Value))

                .Col = ColReadingStep
                .Text = Trim(IIf(IsDbNull(mRsParam.Fields("READING_STEP").Value), "", mRsParam.Fields("READING_STEP").Value))

                .Col = ColPerError
                .Text = Trim(IIf(IsDbNull(mRsParam.Fields("PER_ERROR").Value), "", mRsParam.Fields("PER_ERROR").Value))

                mCol = ColPerError
                mRsDate.MoveFirst()
                Do While Not mRsDate.EOF
                    mCol = mCol + 1

                    SqlStr3 = " SELECT SERIAL_NO,PARAM_DESC,READING_STEP,PER_ERROR," & vbCrLf & " CALIB_DATE,OBSERVATION " & vbCrLf & " FROM ( " & SqlStr & " ) " & vbCrLf & " WHERE SERIAL_NO=" & mRsParam.Fields("SERIAL_NO").Value & " AND PARAM_DESC='" & mRsParam.Fields("PARAM_DESC").Value & "' " & vbCrLf & " AND READING_STEP='" & mRsParam.Fields("READING_STEP").Value & "' AND PER_ERROR='" & mRsParam.Fields("PER_ERROR").Value & "' " & vbCrLf & " AND CALIB_DATE=TO_DATE('" & VB6.Format(mRsDate.Fields("CALIB_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                    MainClass.UOpenRecordSet(SqlStr3, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsObs, ADODB.LockTypeEnum.adLockReadOnly)

                    .Col = mCol
                    If mRsObs.EOF = False Then
                        .Text = Trim(IIf(IsDbNull(mRsObs.Fields("OBSERVATION").Value), "", mRsObs.Fields("OBSERVATION").Value))
                    End If

                    mRsDate.MoveNext()
                Loop

                mRsParam.MoveNext()
                mRow = mRow + 1
                .MaxRows = mRow
            Loop

            .Row = mRow

            .Col = ColParamDesc
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "MASTER INSTRUMENT"

            mCol = ColPerError
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("MASTER_INST").Value), "", mRsDate.Fields("MASTER_INST").Value))
                mRsDate.MoveNext()
            Loop

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            .Col = ColParamDesc
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "REMARKS"

            mCol = ColPerError
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

            .Col = ColParamDesc
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "CALIBRATED BY"

            mCol = ColPerError
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("CALIB_BY").Value), "", mRsDate.Fields("CALIB_BY").Value))
                mRsDate.MoveNext()
            Loop

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            .Col = ColParamDesc
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "APPROVED BY"

            mCol = ColPerError
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("APPROVED_BY").Value), "", mRsDate.Fields("APPROVED_BY").Value))
                mRsDate.MoveNext()
            Loop

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            mCol = ColPerError
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("AUTO_KEY_CALIB").Value), "", mRsDate.Fields("AUTO_KEY_CALIB").Value))
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
            mCol = ColPerError
            .MaxCols = mCol
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColReadingStep
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColPerError
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .MaxCols = mCol
                .Col = mCol

                .Row = 0
                .Text = "Observation (" & VB6.Format(mRsDate.Fields("CALIB_DATE").Value, "DD/MM/YYYY") & ")"

                .Row = -1
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
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

        MakeSQL = " SELECT A.AUTO_KEY_CALIB,CALIB_DATE,MASTER_INST,REMARKS,CALIB_BY,APPROVED_BY, " & vbCrLf & " SERIAL_NO,PARAM_DESC,READING_STEP,PER_ERROR,OBSERVATION " & vbCrLf & " FROM QAL_VOLTMETER_CALIB_HDR A,QAL_VOLTMETER_CALIB_DET B" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.AUTO_KEY_CALIB=B.AUTO_KEY_CALIB "

        If Trim(txtDocNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & ""
        End If

        If cboCalOnCondition.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALIB_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboCalOnCondition.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALIB_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCalOnCondition.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALIB_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCalOnCondition.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND CALIB_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY CALIB_DATE "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If Trim(txtDocNo.Text) = "" Then
            MsgBox("Please Select Doc No.")
            txtDocNo.Focus()
            Exit Function
        End If
        If cboCalOnCondition.Text = "Between" Then
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
        If cboCalOnCondition.Text = "After" Or cboCalOnCondition.Text = "Before" Or cboCalOnCondition.Text = "On Date" Then
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

    Private Sub txtDocNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.TextChanged
        Call PrintStatus(False)
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

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtDocNo.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_VOLTMETER_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STATUS='A' " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRsTemp.EOF Then
            lblDescription.Text = IIf(IsDbNull(mRsTemp.Fields("Description").Value), "", mRsTemp.Fields("Description").Value)
            lblENo.Text = IIf(IsDbNull(mRsTemp.Fields("E_NO").Value), "", mRsTemp.Fields("E_NO").Value)
            lblMake.Text = IIf(IsDbNull(mRsTemp.Fields("MAKE").Value), "", mRsTemp.Fields("MAKE").Value)
            lblRange.Text = IIf(IsDbNull(mRsTemp.Fields("Range").Value), "", mRsTemp.Fields("Range").Value)
            lblLC.Text = IIf(IsDbNull(mRsTemp.Fields("L_C").Value), "", mRsTemp.Fields("L_C").Value)
            lblFrequency.Text = IIf(IsDbNull(mRsTemp.Fields("FREQUENCY").Value), "", mRsTemp.Fields("FREQUENCY").Value)
            lblShuntRatio.Text = IIf(IsDbNull(mRsTemp.Fields("SHUNT_RATIO").Value), "", mRsTemp.Fields("SHUNT_RATIO").Value)
            lblLocation.Text = IIf(IsDbNull(mRsTemp.Fields("Location").Value), "", mRsTemp.Fields("Location").Value)
            lblDept.Text = IIf(IsDbNull(mRsTemp.Fields("DEPT_CODE").Value), "", mRsTemp.Fields("DEPT_CODE").Value)
            lblCalibDueDate.Text = IIf(IsDbNull(mRsTemp.Fields("CALI_DUE_DATE").Value), "", mRsTemp.Fields("CALI_DUE_DATE").Value)
        Else
            MsgBox("Not a valid Doc No.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
