Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamIMTECal
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColAutoKey As Short = 1
    Private Const ColCalOn As Short = 2
    Private Const ColGoSize As Short = 3
    Private Const ColNoGoSize As Short = 4
    Private Const ColAmbTemp As Short = 5
    Private Const ColHumidity As Short = 6
    Private Const ColSoakingTime As Short = 7
    Private Const ColCalibProc As Short = 8
    Private Const ColVisualInsp As Short = 9
    Private Const ColZeroError As Short = 10
    Private Const ColUncertainty As Short = 11
    Private Const ColCalibOK As Short = 12
    Private Const ColRemarks As Short = 13
    Private Const ColInspectedBy As Short = 14
    Private Const ColApprovedBy As Short = 15

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim RsIMTE As ADODB.Recordset

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
        ReportOnIMTECalHis(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtDocNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIMTECalHis(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnIMTECalHis(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "IMTE Calibration History Card (Attributes)"
        '    If Trim(txtDocNo.Text) <> "" Then
        '        mSubTitle = mSubTitle & " [TypeNo : " & txtDocNo.Text & " ]"
        '    End If
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

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IMTECalHis.rpt"

        SqlStr = MakeSQL1
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdSearchDocNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDocNo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE='ATTRIBUTE' "
        If MainClass.SearchGridMaster("", "QAL_IMTE_MST", "DocNo", "Description", "E_NO", "L_C", SqlStr) = True Then
            txtDocNo.Text = AcName
        End If
        txtDocNo.Focus()
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
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Sub frmParamIMTECal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "IMTE Calibration History Card (Attributes)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamIMTECal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

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

    Private Sub frmParamIMTECal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Dim mAutoKey As Long
        'Dim Response As Integer
        '
        '    If Trim(txtDocNo.Text) = "" Then
        '        MsgBox "Please Select Gauge No."
        '        txtDocNo.SetFocus
        '        Exit Sub
        '    End If
        '    With SprdMain
        '        If eventArgs.Col = 0 And eventArgs.Row > 0 Then
        '            .Row = Row
        '            .Col = ColAutoKey
        '            If .Row = 0 Then Exit Sub
        '            Response = MsgQuestion("Click 'Yes' for Insert  And 'No' for Delete. ")
        '            If Response = vbYes Then
        '                frmGaugeFixCal.lblNew = 1
        '
        '                frmGaugeFixCal.lblDocNo = lblDocNo.text
        '                frmGaugeFixCal.lblGaugeNo = txtDocNo.Text
        '                frmGaugeFixCal.lblGaugeDesc = lblGaugeDesc.text
        '                frmGaugeFixCal.lblCustomer = lblCustomer.text
        '                frmGaugeFixCal.lblModel = lblModel.text
        '                frmGaugeFixCal.lblDepartment = lblLocation.text
        '                frmGaugeFixCal.lblFrequency = lblFrequency.text
        '                frmGaugeFixCal.lblGoSize = lblGoSize.text
        '                frmGaugeFixCal.lblNoGoSize = lblNoGoSize.text
        '                frmGaugeFixCal.lblWearSize = lblWearSize.text
        '                frmGaugeFixCal.lblBasicSize = lblBasicSize.text
        '
        '                frmGaugeFixCal.Show 0
        '            Else
        '                If Trim(.Text) = "" Then
        '                    MsgBox "No Row to Delete", vbInformation
        '                Else
        '                    Response = MsgQuestion("Are you sure to Delete this Row ? ")
        '                    If Response = vbYes Then
        '                        mAutoKey = Val(.Text)
        '                        If delCal(mAutoKey) = False Then MsgBox "Cann't Delete", vbInformation
        '                        cmdShow_Click
        '                    End If
        '                End If
        '            End If
        '        End If
        '    End With
        '
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mAutoKey As String
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColAutoKey
        mAutoKey = Trim(SprdMain.Text)
        frmIMTEInsp.MdiParent = Me.MdiParent
        frmIMTEInsp.frmIMTEInsp_Activated(Nothing, New System.EventArgs())
        frmIMTEInsp.Show()
        frmIMTEInsp.txtSlipNo.Text = mAutoKey
        frmIMTEInsp.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Function delCal(ByRef mAKey As Integer) As Boolean
        'On Error GoTo ErrPart
        'Dim SqlStr As String
        '
        '    PubDBCn.Errors.Clear
        '    PubDBCn.BeginTrans
        '
        '    SqlStr = ""
        '    SqlStr = " DELETE FROM QAL_IMTE_CALIB_TRN " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''            & " AND AUTO_KEY_CALIB =" & mAKey & ""
        '
        '    PubDBCn.Execute (SqlStr)
        '
        '    delCal = True
        '    PubDBCn.CommitTrans
        '    Exit Function
        'ErrPart:
        '    delCal = False
        '    PubDBCn.RollbackTrans
        '    'RsCalibCertGauge.Requery
        '    MsgBox err.Description
        '    'Resume
    End Function

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColApprovedBy
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColAutoKey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColCalOn
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColGoSize
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColNoGoSize
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColsFrozen = ColNoGoSize

            .Col = ColAmbTemp
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeEditMultiLine = False

            .Col = ColHumidity
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeEditMultiLine = False

            .Col = ColSoakingTime
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeEditMultiLine = False

            .Col = ColCalibProc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColVisualInsp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColZeroError
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColUncertainty
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCalibOK
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColInspectedBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColApprovedBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim SqlStr As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1

        MakeSQL = " SELECT AUTO_KEY_CALIB,CALIB_DATE,ACTUAL_GOSIZE,ACTUAL_NOGOSIZE,  " & vbCrLf & " AMB_TEMP,HUMIDITY,SOAKING_TIME, " & vbCrLf & " CALIB_PROC,VISUAL_INSP,ZERO_ERROR,UNCERTAINTY,DECODE(CALIB_OK,'Y','YES','N','NO') CALIB_OK, " & vbCrLf & " REMARKS,INSPECTED_BY,APPROVED_BY " & vbCrLf & " FROM QAL_IMTE_CALIB_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        '            & " AND SUBSTR(AUTO_KEY_CALIB,LENGTH(AUTO_KEY_CALIB)-5,4)=" & RsCompany.fields("FYEAR").value & " "

        If Trim(txtDocNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " "
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

    Private Function MakeSQL1() As String
        On Error GoTo ERR1

        MakeSQL1 = " SELECT IH.*,ID.* " & vbCrLf & " FROM QAL_IMTE_MST IH,QAL_IMTE_CALIB_HDR ID " & vbCrLf & " WHERE IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.DOCNO=ID.DOCNO " & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        '            & " AND SUBSTR(ID.AUTO_KEY_CALIB,LENGTH(ID.AUTO_KEY_CALIB)-5,4)=" & RsCompany.fields("FYEAR").value & " "

        If Trim(txtDocNo.Text) <> "" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ID.DOCNO=" & Val(txtDocNo.Text) & " "
        End If

        If cboCalOnCondition.Text = "Between" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ID.CALIB_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboCalOnCondition.Text = "After" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ID.CALIB_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCalOnCondition.Text = "Before" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ID.CALIB_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCalOnCondition.Text = "On Date" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ID.CALIB_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL1 = MakeSQL1 & vbCrLf & " ORDER BY CALIB_DATE "

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

        If Trim(txtDocNo.Text) = "" Then GoTo EventExitSub
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_IMTE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE='ATTRIBUTE' " & vbCrLf & " AND DOCNO=" & Val(txtDocNo.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIMTE, ADODB.LockTypeEnum.adLockReadOnly)
        If RsIMTE.EOF = False Then
            ShowIMTE()
        Else
            MsgBox("Such Number Does Not Exist", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub ShowIMTE()
        On Error GoTo ShowErrPart
        If Not RsIMTE.EOF Then
            lblDescription.Text = IIf(IsDbNull(RsIMTE.Fields("Description").Value), "", RsIMTE.Fields("Description").Value)
            lblENo.Text = IIf(IsDbNull(RsIMTE.Fields("E_NO").Value), "", RsIMTE.Fields("E_NO").Value)
            lblLC.Text = IIf(IsDbNull(RsIMTE.Fields("L_C").Value), "", RsIMTE.Fields("L_C").Value)
            lblMakersNo.Text = IIf(IsDbNull(RsIMTE.Fields("Markers_No").Value), "", RsIMTE.Fields("Markers_No").Value)
            lblMake.Text = IIf(IsDbNull(RsIMTE.Fields("Make_Name").Value), "", RsIMTE.Fields("Make_Name").Value)
            lblLocation.Text = IIf(IsDbNull(RsIMTE.Fields("Location").Value), "", RsIMTE.Fields("Location").Value)
            lblFrequency.Text = IIf(IsDbNull(RsIMTE.Fields("ValFrequency").Value), "", RsIMTE.Fields("ValFrequency").Value)
            lblGoSize.Text = IIf(IsDbNull(RsIMTE.Fields("GOSIZE").Value), "", RsIMTE.Fields("GOSIZE").Value)
            lblNoGoSize.Text = IIf(IsDbNull(RsIMTE.Fields("NOGOSIZE").Value), "", RsIMTE.Fields("NOGOSIZE").Value)
            lblBasicSize.Text = IIf(IsDbNull(RsIMTE.Fields("BASICSIZE").Value), "", RsIMTE.Fields("BASICSIZE").Value)
            lblWearSize.Text = IIf(IsDbNull(RsIMTE.Fields("WearSize").Value), "", RsIMTE.Fields("WearSize").Value)
        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
End Class
