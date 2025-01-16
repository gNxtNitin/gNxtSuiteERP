Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGaugeFixInsp
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColParamDesc As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColSpecPlus As Short = 3
    Private Const ColSpecMinus As Short = 4
    Private Const ColWearLimit As Short = 5
    Private Const ColInspMth As Short = 6
    Private Const ColDate1 As Short = 7

    Dim xColorOrig As String

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
        If Trim(txtTypeNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGFInspHis(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtTypeNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGFInspHis(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnGFInspHis(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Gauge Fixture Inspection (Calibration) History"

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

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GaugeFixInspHis.rpt"

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
            FieldCnt = 8
            SetData = "FIELD1,FIELD2,FIELD3,FIELD4,FIELD5,FIELD6,FIELD7"
            GetData = "'" & MainClass.AllowSingleQuote(txtTypeNo.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblDescription.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblCustomer.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblLocation.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblModel.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblFrequency.Text) & "'" & vbCrLf & ",'" & MainClass.AllowSingleQuote(lblDrgNo.Text) & "'"

            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                '            If FieldNum = prmStartGridCol Then
                '                SetData = "FIELD" & FieldCnt
                '                GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                '            Else
                SetData = SetData & ", " & "FIELD" & FieldCnt
                If RowNum = 0 Then
                    If FieldCnt <= 13 Then
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

    Private Sub cmdSearchTypeNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchTypeNo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "SELECT DISTINCT B.TYPENO, A.DOCNO " & vbCrLf & " FROM QAL_GAUGE_CALIB_STD A, QAL_GAUGEFIX_MST B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND LTRIM(RTRIM(A.DOCNO)) = LTRIM(RTRIM(B.DOCNO)) " & vbCrLf & " AND A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY B.TYPENO "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtTypeNo.Text = AcName
            lblDocNo.text = AcName1
            If txtTypeNo.Enabled = True Then txtTypeNo.Focus()
        End If
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
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

    Public Sub frmParamGaugeFixInsp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Gauge Fixture Inspection (Calibration) History"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamGaugeFixInsp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        xColorOrig = System.Drawing.ColorTranslator.ToOle(SprdMain.ForeColor).ToString

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

    Private Sub frmParamGaugeFixInsp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        If eventArgs.col <= ColInspMth Then Exit Sub
        SprdMain.Row = SprdMain.MaxRows
        SprdMain.Col = eventArgs.col
        mAutoKey = Val(SprdMain.Text)
        frmGaugeFixInsp.MdiParent = Me.MdiParent
        frmGaugeFixInsp.frmGaugeFixInsp_Activated(Nothing, New System.EventArgs())
        frmGaugeFixInsp.Show()
        frmGaugeFixInsp.txtSlipNo.Text = CStr(mAutoKey)
        frmGaugeFixInsp.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        frmGaugeFixInsp.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
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

        SqlStr1 = "SELECT DISTINCT AUTO_KEY_CALIB,CALIB_DATE,REMARKS,INSPECTED_BY,APPROVED_BY FROM ( " & SqlStr & " ) ORDER BY CALIB_DATE"
        MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsDate, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsDate.EOF = True Then
            Show1 = True
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        FormatSprdMain(-1, mRsDate)

        SqlStr2 = " SELECT DISTINCT SERIAL_NO,PARAM_DESC,SPECIFICATION,SPEC_PLUS,SPEC_MINUS,WEAR_LIMIT,INSP_MTH" & vbCrLf & " FROM ( " & SqlStr & " ) ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr2, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsParam, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            mRow = 1
            Do While Not mRsParam.EOF
                .Row = mRow

                .Col = ColParamDesc
                .Text = Trim(IIf(IsDbNull(mRsParam.Fields("PARAM_DESC").Value), "", mRsParam.Fields("PARAM_DESC").Value))

                .Col = ColInspMth
                .Text = Trim(IIf(IsDbNull(mRsParam.Fields("INSP_MTH").Value), "", mRsParam.Fields("INSP_MTH").Value))
                mInspMth = .Text

                If mInspMth <> "VISUAL" Then
                    .Col = ColSpecification
                    .Text = Trim(IIf(IsDbNull(mRsParam.Fields("SPECIFICATION").Value), "", mRsParam.Fields("SPECIFICATION").Value))

                    .Col = ColSpecPlus
                    .Text = Trim(IIf(IsDbNull(mRsParam.Fields("SPEC_PLUS").Value), "", mRsParam.Fields("SPEC_PLUS").Value))

                    .Col = ColSpecMinus
                    .Text = Trim(IIf(IsDbNull(mRsParam.Fields("SPEC_MINUS").Value), "", mRsParam.Fields("SPEC_MINUS").Value))

                    .Col = ColWearLimit
                    .Text = Trim(IIf(IsDbNull(mRsParam.Fields("WEAR_LIMIT").Value), "", mRsParam.Fields("WEAR_LIMIT").Value))
                End If

                mCol = ColInspMth
                mRsDate.MoveFirst()
                Do While Not mRsDate.EOF
                    mCol = mCol + 1

                    SqlStr3 = " SELECT SERIAL_NO,PARAM_DESC,SPECIFICATION,SPEC_PLUS,SPEC_MINUS,WEAR_LIMIT,INSP_MTH," & vbCrLf & " CALIB_DATE,OBSERVATION,BEFORE_OBSERVATION " & vbCrLf & " FROM ( " & SqlStr & " ) " & vbCrLf & " WHERE SERIAL_NO=" & mRsParam.Fields("SERIAL_NO").Value & " AND PARAM_DESC='" & MainClass.AllowSingleQuote(mRsParam.Fields("PARAM_DESC")) & "' " & vbCrLf & " AND SPECIFICATION=" & mRsParam.Fields("SPECIFICATION").Value & " AND SPEC_PLUS=" & mRsParam.Fields("SPEC_PLUS").Value & " " & vbCrLf & " AND SPEC_MINUS=" & mRsParam.Fields("SPEC_MINUS").Value & " AND WEAR_LIMIT=" & mRsParam.Fields("WEAR_LIMIT").Value & " " & vbCrLf & " AND INSP_MTH='" & MainClass.AllowSingleQuote(IIf(IsDBNull(mRsParam.Fields("INSP_MTH").Value), "", mRsParam.Fields("INSP_MTH").Value)) & "' " & vbCrLf & " AND CALIB_DATE=TO_DATE('" & VB6.Format(mRsDate.Fields("CALIB_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                    MainClass.UOpenRecordSet(SqlStr3, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsObs, ADODB.LockTypeEnum.adLockReadOnly)

                    .Col = mCol
                    If mRsObs.EOF = False Then
                        .Text = Trim(IIf(IsDbNull(mRsObs.Fields("OBSERVATION").Value), "", mRsObs.Fields("OBSERVATION").Value))
                        If mInspMth = "VISUAL" Then
                            .CellType = SS_CELL_TYPE_EDIT
                            .TypeEditLen = 255
                            .TypeEditMultiLine = True
                            If Val(.Text) = 0 Then
                                .Text = "  OK  "
                            Else
                                .Text = "NOT OK"
                            End If
                        Else
                            Call SetObsCol(.Row, .Col)
                        End If
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
            .Text = "REMARKS"

            mCol = ColInspMth
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
            .Text = "INSPECTED BY"

            mCol = ColInspMth
            mRsDate.MoveFirst()
            Do While Not mRsDate.EOF
                mCol = mCol + 1
                .Col = mCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .Text = Trim(IIf(IsDbNull(mRsDate.Fields("INSPECTED_BY").Value), "", mRsDate.Fields("INSPECTED_BY").Value))
                mRsDate.MoveNext()
            Loop

            mRow = mRow + 1
            .MaxRows = mRow
            .Row = mRow

            .Col = ColParamDesc
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Text = "APPROVED BY"

            mCol = ColInspMth
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

            mCol = ColInspMth
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

            .Row = .MaxRows - 3
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
        '    Resume
    End Function

    Private Sub SetObsCol(ByRef Row As Integer, ByRef Col As Integer)
        Dim xParamDesc As String
        Dim xSpecification As Double
        Dim xSpecPlus As Double
        Dim xSpecMinus As Double
        Dim xWearLimit As Double
        Dim xObservation As Double
        Dim xMinSpec As Double
        Dim xMaxspec As Double
        Dim xMinWear As Double
        Dim xMaxWear As Double
        Dim xMinBlack As Double
        Dim xMaxBlack As Double
        Dim xWearBlack As Double
        Dim xColorBlue As String
        Dim xColorRed As String
        Dim a As Double
        Dim B As Double

        xColorBlue = CStr(&HFF0000)
        xColorRed = CStr(&HFF)

        With SprdMain
            .Col = ColParamDesc
            xParamDesc = Trim(.Text)
            .Col = ColSpecification
            .Col = ColSpecification
            xSpecification = Val(.Text)
            .Col = ColSpecPlus
            xSpecPlus = Val(.Text)
            .Col = ColSpecMinus
            xSpecMinus = Val(.Text)
            .Col = ColWearLimit
            xWearLimit = Val(.Text)
            .Col = Col
            xObservation = Val(.Text)
            If InStr(1, xParamDesc, "ANGLE") > 0 Then
                a = CDbl(VB6.Format(xSpecification, CStr(0)))
                B = xSpecification - a
                xSpecification = a + B * 10 / 6
                a = CDbl(VB6.Format(xSpecPlus, CStr(0)))
                B = xSpecPlus - a
                xSpecPlus = a + B * 10 / 6
                a = CDbl(VB6.Format(xSpecMinus, CStr(0)))
                B = xSpecMinus - a
                xSpecMinus = a + B * 10 / 6
                a = CDbl(VB6.Format(xWearLimit, CStr(0)))
                B = xWearLimit - a
                xWearLimit = a + B * 10 / 6
                a = CDbl(VB6.Format(xObservation, CStr(0)))
                B = xObservation - a
                xObservation = a + B * 10 / 6
            End If
            xMinSpec = xSpecification + xSpecMinus
            xMaxspec = xSpecification + xSpecPlus

            If xObservation = xSpecification Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorOrig))
            Else
                If (xObservation >= xMinSpec And xObservation <= xMaxspec) Then
                    .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorOrig))
                Else
                    If xWearLimit = 0 Then
                        xMinWear = xMinSpec
                        xMaxWear = xMaxspec
                    ElseIf (xWearLimit >= xMinSpec And xWearLimit <= xMaxspec) Then
                        xMinWear = xMinSpec
                        xMaxWear = xMaxspec
                    ElseIf (xWearLimit < xMinSpec) Then
                        xMinWear = xWearLimit
                        xMaxWear = xMaxspec
                    ElseIf (xWearLimit > xMinSpec) Then
                        xMinWear = xMinSpec
                        xMaxWear = xWearLimit
                    End If
                    If (xObservation >= xMinWear And xObservation <= xMaxWear) Then
                        xWearBlack = xWearLimit + ((xSpecification - xWearLimit) * 0.2)
                        If xSpecification <= xWearBlack Then
                            xMinBlack = xSpecification
                            xMaxBlack = xWearBlack
                        ElseIf xSpecification > xWearBlack Then
                            xMinBlack = xWearBlack
                            xMaxBlack = xSpecification
                        End If
                        If (xObservation >= xMinBlack And xObservation <= xMaxBlack) Then
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorOrig))
                        Else
                            .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorBlue))
                        End If
                    Else
                        .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(xColorRed))
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer, ByRef mRsDate As ADODB.Recordset)

        Dim mRow As Integer
        Dim mCol As Integer

        With SprdMain
            mCol = ColInspMth
            .MaxCols = mCol
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeEditMultiLine = True

            .Col = ColSpecPlus
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeEditMultiLine = True

            .Col = ColSpecMinus
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeEditMultiLine = True

            .Col = ColWearLimit
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeEditMultiLine = True

            .Col = ColInspMth
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
                '            .CellType = SS_CELL_TYPE_FLOAT
                '            .TypeEditLen = 255
                '            .TypeFloatDecimalChar = Asc(".")
                '            .TypeFloatDecimalPlaces = 3
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

        MakeSQL = " SELECT A.AUTO_KEY_CALIB,CALIB_DATE,REMARKS,INSPECTED_BY,APPROVED_BY, " & vbCrLf & " SERIAL_NO, PARAM_DESC,SPECIFICATION,SPEC_PLUS,SPEC_MINUS,WEAR_LIMIT,INSP_MTH,OBSERVATION,BEFORE_OBSERVATION" & vbCrLf & " FROM QAL_GAUGE_CALIB_HDR A,QAL_GAUGE_CALIB_DET B" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.AUTO_KEY_CALIB=B.AUTO_KEY_CALIB "

        '            & " AND SUBSTR(A.AUTO_KEY_CALIB,LENGTH(A.AUTO_KEY_CALIB)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
        '
        If Trim(lblDocNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DOCNO=" & Val(lblDocNo.Text) & ""
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
        If Trim(txtTypeNo.Text) = "" Then
            MsgBox("Please Select Type No.")
            txtTypeNo.Focus()
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

    Private Sub txtTypeNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTypeNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtTypeNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTypeNo.DoubleClick
        Call cmdSearchTypeNo_Click(cmdSearchTypeNo, New System.EventArgs())
    End Sub

    Private Sub txtTypeNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTypeNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTypeNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTypeNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTypeNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchTypeNo_Click(cmdSearchTypeNo, New System.EventArgs())
    End Sub

    Public Sub txtTypeNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTypeNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtTypeNo.Text) = "" Then GoTo EventExitSub

        SqlStr = "SELECT DISTINCT B.TYPENO, A.DOCNO " & vbCrLf _
                    & " FROM QAL_GAUGE_CALIB_STD A, QAL_GAUGEFIX_MST B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                    & " AND LTRIM(RTRIM(A.DOCNO)) = LTRIM(RTRIM(B.DOCNO)) " & vbCrLf _
                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.TYPENO)) ='" & MainClass.AllowSingleQuote(txtTypeNo.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblDocNo.Text = IIf(IsDbNull(mRsTemp.Fields("DOCNO").Value), "", .Fields("DOCNO").Value)
                ShowGauge()
            Else
                MsgBox("Not a valid Type No.")
                lblDocNo.Text = ""
                Cancel = True
            End If
        End With
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub ShowGauge()

        On Error GoTo ShowErrPart
        Dim RsGaugeFix As ADODB.Recordset
        Dim SqlStr As String

        If Trim(lblDocNo.Text) = "" Then Exit Sub
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_GAUGEFIX_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO=" & Val(lblDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeFix, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsGaugeFix.EOF Then
            txtTypeNo.Text = IIf(IsDbNull(RsGaugeFix.Fields("TypeNo").Value), "", RsGaugeFix.Fields("TypeNo").Value)
            lblDescription.Text = IIf(IsDbNull(RsGaugeFix.Fields("Description").Value), "", RsGaugeFix.Fields("Description").Value)
            lblCustomer.Text = IIf(IsDbNull(RsGaugeFix.Fields("Customer").Value), "", RsGaugeFix.Fields("Customer").Value)
            lblLocation.Text = IIf(IsDbNull(RsGaugeFix.Fields("Location").Value), "", RsGaugeFix.Fields("Location").Value)
            lblModel.Text = IIf(IsDbNull(RsGaugeFix.Fields("MODEL").Value), "", RsGaugeFix.Fields("MODEL").Value)
            lblDrgNo.Text = IIf(IsDbNull(RsGaugeFix.Fields("DrgNo").Value), "", RsGaugeFix.Fields("DrgNo").Value)
            lblFrequency.Text = IIf(IsDbNull(RsGaugeFix.Fields("ValFrequency").Value), "", RsGaugeFix.Fields("ValFrequency").Value)
        Else
            MsgBox("Doc No Does Not Exist", MsgBoxStyle.Information)
        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
