Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGaugeFixCal
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColAutoKey As Short = 1
    Private Const ColGoSize As Short = 2
    Private Const ColNoGoSize As Short = 3
    Private Const ColCalOn As Short = 4
    Private Const ColCalDue As Short = 5
    Private Const ColPartName As Short = 6
    Private Const ColRemarks As Short = 7

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim RsGaugeFix As ADODB.Recordset

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
        ReportOnGFCalHis(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        If Trim(txtTypeNo.Text) = "" Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGFCalHis(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnGFCalHis(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Gauge Fixture Calibration Card"
        '    If Trim(txtTypeNo.Text) <> "" Then
        '        mSubTitle = mSubTitle & " [TypeNo : " & txtTypeNo.Text & " ]"
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

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GaugeFixCalHis.rpt"

        SqlStr = MakeSQL
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

    Private Sub cmdSearchTypeNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchTypeNo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (TYPE='GAUGE' OR TYPE='PGG') "
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "TypeNo", "Description", "Customer", "Model", SqlStr) = True Then
            txtTypeNo.Text = AcName
        End If
        txtTypeNo.Focus()
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
    Public Sub frmParamGaugeFixCal_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Gauge Fixture Calibration History Card"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGaugeFixCal_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

    Private Sub frmParamGaugeFixCal_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Dim mAutoKey As Long
        'Dim Response As Integer
        '
        '    If Trim(txtTypeNo.Text) = "" Then
        '        MsgBox "Please Select Gauge No."
        '        txtTypeNo.SetFocus
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
        '                frmGaugeFixCal.lblGaugeNo = txtTypeNo.Text
        '                frmGaugeFixCal.lblGaugeDesc = lblGaugeDesc.text
        '                frmGaugeFixCal.lblCustomer = lblCustomer.text
        '                frmGaugeFixCal.lblModel = lblModel.text
        '                frmGaugeFixCal.lblDepartment = lblLocation.text
        '                frmGaugeFixCal.lblFrequency = lblFrequency.text
        '                frmGaugeFixCal.lblGoSize = lblGoSize.text
        '                frmGaugeFixCal.lblNoGoSize = lblNoGoSize.text
        '                frmGaugeFixCal.lblWearSize = lblWearSize.text
        '                frmGaugeFixCal.lblCompSize = lblCompSize.text
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
        frmGaugeFixCal.MdiParent = Me.MdiParent
        frmGaugeFixCal.frmGaugeFixCal_Activated(Nothing, New System.EventArgs())
        frmGaugeFixCal.Show()
        frmGaugeFixCal.txtNumber.Text = mAutoKey
        frmGaugeFixCal.txtNumber_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Function delCal(ByRef mAKey As Integer) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        SqlStr = " DELETE FROM QAL_GAUGE_CALIB_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND AUTO_KEY_CALIB =" & mAKey & ""

        PubDBCn.Execute(SqlStr)

        delCal = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        delCal = False
        PubDBCn.RollbackTrans()
        'RsCalibCertGauge.Requery
        MsgBox(Err.Description)
        'Resume
    End Function

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColRemarks
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColAutoKey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

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

            .Col = ColCalOn
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCalDue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColPartName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColRemarks)
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

        MakeSQL = " SELECT TRN.AUTO_KEY_CALIB, TRN.ACTUAL_GOSIZE, TRN.ACTUAL_NOGOSIZE,  " & vbCrLf & " TRN.CALIB_DATE, ADD_MONTHS(TRN.CALIB_DATE, VALFREQUENCY), TRN.PARTNAME, TRN.CALIB_REMARKS " & vbCrLf & " FROM QAL_GAUGE_CALIB_TRN TRN, QAL_GAUGEFIX_MST MST" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE AND TRN.GAUGE_NO=MST.TYPENO "

        If Trim(txtTypeNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.Gauge_No='" & MainClass.AllowSingleQuote(txtTypeNo.Text) & "'"
        End If

        If cboCalOnCondition.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.CALIB_DATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboCalOnCondition.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.CALIB_DATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCalOnCondition.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.CALIB_DATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCalOnCondition.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.CALIB_DATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY TRN.CALIB_DATE "

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQL1() As String

        On Error GoTo ERR1

        MakeSQL1 = " SELECT IH.TypeNo,IH.Description,IH.Customer,IH.Location,IH.Model, " & vbCrLf & " IH.ValFrequency,IH.ReqGoSize,IH.ReqNoGoSize,IH.WearSize,IH.CompSize, " & vbCrLf & " ID.ACTUAL_GOSIZE,ID.ACTUAL_NOGOSIZE, " & vbCrLf & " ID.CALIB_DATE,ID.CALIBDUE_DATE,ID.PARTNAME,ID.CALIB_REMARKS " & vbCrLf & " FROM QAL_GAUGEFIX_MST IH,QAL_GAUGE_CALIB_TRN ID " & vbCrLf & " WHERE IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.FYEAR=ID.FYEAR " & vbCrLf & " AND IH.TYPENO=ID.GAUGE_NO " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If Trim(txtTypeNo.Text) <> "" Then
            MakeSQL1 = MakeSQL1 & vbCrLf & " AND ID.Gauge_No='" & MainClass.AllowSingleQuote(txtTypeNo.Text) & "'"
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
        If Trim(txtTypeNo.Text) = "" Then
            MsgBox("Please Select Gauge No.")
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

        If Trim(txtTypeNo.Text) = "" Then GoTo EventExitSub
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                    & " FROM QAL_GAUGEFIX_MST" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND (TYPE='GAUGE' OR TYPE='PGG') " & vbCrLf _
                    & " AND UPPER(TRIM(TypeNo))='" & MainClass.AllowSingleQuote(UCase(txtTypeNo.Text)) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGaugeFix, ADODB.LockTypeEnum.adLockReadOnly)
        If RsGaugeFix.EOF = False Then
            ShowGauge()
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

    Private Sub ShowGauge()
        On Error GoTo ShowErrPart
        If Not RsGaugeFix.EOF Then
            lblDocNo.Text = IIf(IsDbNull(RsGaugeFix.Fields("DOCNO").Value), "", RsGaugeFix.Fields("DOCNO").Value)
            lblGaugeDesc.Text = IIf(IsDbNull(RsGaugeFix.Fields("Description").Value), "", RsGaugeFix.Fields("Description").Value)
            lblCustomer.Text = IIf(IsDbNull(RsGaugeFix.Fields("CUSTOMER").Value), "", RsGaugeFix.Fields("CUSTOMER").Value)
            lblLocation.Text = IIf(IsDbNull(RsGaugeFix.Fields("LOCATION").Value), "", RsGaugeFix.Fields("LOCATION").Value)
            lblModel.Text = IIf(IsDbNull(RsGaugeFix.Fields("MODEL").Value), "", RsGaugeFix.Fields("MODEL").Value)
            lblFrequency.Text = IIf(IsDbNull(RsGaugeFix.Fields("VALFREQUENCY").Value), "", RsGaugeFix.Fields("VALFREQUENCY").Value)
            lblGoSize.Text = IIf(IsDbNull(RsGaugeFix.Fields("REQGOSIZE").Value), "", RsGaugeFix.Fields("REQGOSIZE").Value)
            lblNoGoSize.Text = IIf(IsDbNull(RsGaugeFix.Fields("REQNOGOSIZE").Value), "", RsGaugeFix.Fields("REQNOGOSIZE").Value)
            lblWearSize.Text = IIf(IsDbNull(RsGaugeFix.Fields("WEARSIZE").Value), "", RsGaugeFix.Fields("WEARSIZE").Value)
            lblCompSize.Text = IIf(IsDbNull(RsGaugeFix.Fields("COMPSIZE").Value), "", RsGaugeFix.Fields("COMPSIZE").Value)
        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
End Class