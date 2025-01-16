Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGaugeFixMst
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColDocNo As Short = 1
    Private Const ColModel As Short = 2
    Private Const ColDescription As Short = 3
    Private Const ColCustomer As Short = 4
    Private Const ColType As Short = 5
    Private Const ColTypeNo As Short = 6
    Private Const ColComponent As Short = 7
    Private Const ColDrawingNo As Short = 8
    Private Const ColVDoneOn As Short = 9
    Private Const ColLocation As Short = 10
    Private Const ColValFrequency As Short = 11
    Private Const ColVDueOn As Short = 12
    Private Const ColIssueDate As Short = 13
    Private Const ColQtyChecked As Short = 14
    Private Const ColRemarks As Short = 15
    Private Const ColCalibration As Short = 16
    Private Const ColGoSize As Short = 17
    Private Const ColNoGoSize As Short = 18
    Private Const ColWearSize As Short = 19
    Private Const ColCompSize As Short = 20
    Private Const ColHistory As Short = 21

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboCDDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCDDate.SelectedIndexChanged
        If cboCDDate.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCDDate.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboCDDate.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCDDate.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboCDDate.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub cboLCDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLCDate.SelectedIndexChanged
        If cboLCDate.Text = "None" Then
            txtDate3.Visible = False
            lblDate3.Visible = False
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboLCDate.Text = "Between" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = True
            lblDate4.Visible = True
        ElseIf cboLCDate.Text = "After" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboLCDate.Text = "Before" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboLCDate.Text = "On Date" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        End If
    End Sub

    Private Sub chkAllCustomer_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCustomer.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCustomer.Enabled = False
            CmdSearchCustomer.Enabled = False
        Else
            txtCustomer.Enabled = True
            CmdSearchCustomer.Enabled = True
        End If
    End Sub

    Private Sub chkAllDRG_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDRG.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDRG.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDrawingNo.Enabled = False
            cmdSearchDRGNo.Enabled = False
        Else
            txtDrawingNo.Enabled = True
            cmdSearchDRGNo.Enabled = True
        End If
    End Sub
    Private Sub chkAllLocation_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllLocation.CheckStateChanged
        Call PrintStatus(False)
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtLocation.Enabled = False
            cmdSearchLocation.Enabled = False
        Else
            txtLocation.Enabled = True
            cmdSearchLocation.Enabled = True
        End If
    End Sub

    Private Sub chkAllModel_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllModel.CheckStateChanged
        Call PrintStatus(False)
        If chkAllModel.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtModel.Enabled = False
            cmdSearchModel.Enabled = False
        Else
            txtModel.Enabled = True
            cmdSearchModel.Enabled = True
        End If
    End Sub

    Private Sub chkAllTypeNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllTypeNo.CheckStateChanged
        Call PrintStatus(False)
        If chkAllTypeNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTypeNo.Enabled = False
            cmdSearchTypeNo.Enabled = False
        Else
            txtTypeNo.Enabled = True
            cmdSearchTypeNo.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGFMstList(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnGFMstList(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnGFMstList(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Gauge Fixture Master List"
        If chkAllModel.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtModel.Text) <> "" Then
            mSubTitle = mSubTitle & " [Model : " & txtModel.Text & " ]"
        End If
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomer.Text) <> "" Then
            mSubTitle = mSubTitle & " [Customer : " & txtCustomer.Text & " ]"
        End If
        If chkAllTypeNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtTypeNo.Text) <> "" Then
            mSubTitle = mSubTitle & " [TypeNo : " & txtTypeNo.Text & " ]"
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) <> "" Then
            mSubTitle = mSubTitle & " [Location : " & txtLocation.Text & " ]"
        End If

        If chkAllType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtType.Text) <> "" Then
            mSubTitle = mSubTitle & " [Type : " & txtType.Text & " ]"
        End If

        If chkAllComponent.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtComponent.Text) <> "" Then
            mSubTitle = mSubTitle & " [Component : " & txtComponent.Text & " ]"
        End If

        If chkAllDRG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDrawingNo.Text) <> "" Then
            mSubTitle = mSubTitle & " [Model : " & txtDrawingNo.Text & " ]"
        End If

        If cboCDDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [V. Due Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboCDDate.Text = "After" Then
            mSubTitle = mSubTitle & " [V. Due After  " & txtDate1.Text & " ]"
        End If
        If cboCDDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [V. Due Before  " & txtDate1.Text & " ]"
        End If
        If cboCDDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [V. Due On  " & txtDate1.Text & " ]"
        End If

        If cboLCDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [Last Calib Between  " & txtDate3.Text & " And " & txtDate4.Text & " ]"
        End If
        If cboLCDate.Text = "After" Then
            mSubTitle = mSubTitle & " [Last Calib After  " & txtDate3.Text & " ]"
        End If
        If cboLCDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Last Calib Before  " & txtDate3.Text & " ]"
        End If
        If cboLCDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Last Calib On  " & txtDate3.Text & " ]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GaugeFixMstList.rpt"

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

    'Private Sub CmdSave_Click()
    'On Error GoTo ErrPart
    'Dim SqlStr As String
    'Dim CntRow As Long
    'Dim mDocNo As Double
    'Dim mVDoneOn As String
    'Dim mVDueOn As String
    'Dim mFrequency As Double
    '
    '    PubDBCn.Errors.Clear
    '    PubDBCn.BeginTrans
    '
    '    With SprdMain
    '        For CntRow = 1 To .MaxRows
    '            .Row = CntRow
    '            .Col = ColDocNo
    '            mDocNo = Trim(.Text)
    '
    '            .Col = ColCompletionDate
    '            mVDoneOn = Format(.Text, "DD/MM/YYYY")
    '
    '            .Col = ColValFrequency
    '            mFrequency = Val(.Text)
    '
    '            If IsDate(mVDoneOn) Then
    ''                If MainClass.ValidateWithMasterTable(mDocNo, "DOCNO", "VALFREQUENCY", "QAL_GAUGEFIX_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
    ''                    mFrequency = MasterNo
    ''                End If
    '
    '                mVDueOn = DateAdd("d", (Val(mFrequency) * 30), mVDoneOn)
    '
    '                SqlStr = " UPDATE QAL_GAUGEFIX_MST SET " & vbCrLf _
    ''                            & " VDoneOn=TO_DATE('" & vb6.Format(mVDoneOn, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
    ''                            & " VDueOn=TO_DATE('" & vb6.Format(mVDueOn, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
    ''                            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
    ''                            & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
    ''                            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
    ''                            & " AND DOCNO =" & mDocNo & ""
    '
    '                PubDBCn.Execute SqlStr
    '             End If
    '        Next
    '    End With
    '
    '    PubDBCn.CommitTrans
    ''    CmdSave.Enabled = False
    '    Call cmdShow_Click
    'Exit Sub
    'ErrPart:
    '    ErrorMsg err.Number, err.Description, vbCritical
    '    PubDBCn.RollbackTrans
    'End Sub

    Private Sub CmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCustomer.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "Customer", "", "", "", SqlStr) = True Then
            txtCustomer.Text = AcName
        End If
        txtCustomer.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchDRGNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDRGNo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "DRGNO", "Customer", "TypeNo", "Description", SqlStr) = True Then
            txtDrawingNo.Text = AcName
        End If
        txtDrawingNo.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdSearchLocation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchLocation.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "Location", "", "", "", SqlStr) = True Then
            txtLocation.Text = AcName
        End If
        txtLocation.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchModel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchModel.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "Model", "Customer", "TypeNo", "Description", SqlStr) = True Then
            txtModel.Text = AcName
        End If
        txtModel.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchTypeNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchTypeNo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "TypeNo", "Description", "Customer", "Model", SqlStr) = True Then
            txtTypeNo.Text = AcName
        End If
        txtTypeNo.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

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
    Private Sub frmParamGaugeFixMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Gauge Fixture Master List"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        CmdSave.Visible = False
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGaugeFixMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAllModel.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllModel_CheckStateChanged(chkAllModel, New System.EventArgs())
        chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllCustomer_CheckStateChanged(chkAllCustomer, New System.EventArgs())
        chkAllTypeNo.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllTypeNo_CheckStateChanged(chkAllTypeNo, New System.EventArgs())
        chkAllLocation.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllLocation_CheckStateChanged(chkAllLocation, New System.EventArgs())
        chkAllDRG.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllDRG_CheckStateChanged(chkAllDRG, New System.EventArgs())
        OptOrderBy(0).Checked = True
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillCbo()
        cboCDDate.Items.Clear()
        cboCDDate.Items.Add("None")
        cboCDDate.Items.Add("Between")
        cboCDDate.Items.Add("After")
        cboCDDate.Items.Add("Before")
        cboCDDate.Items.Add("On Date")
        cboCDDate.SelectedIndex = 0

        cboLCDate.Items.Clear()
        cboLCDate.Items.Add("None")
        cboLCDate.Items.Add("Between")
        cboLCDate.Items.Add("After")
        cboLCDate.Items.Add("Before")
        cboLCDate.Items.Add("On Date")
        cboLCDate.SelectedIndex = 0

        cboStatus.Items.Clear()
        cboStatus.Items.Add("Active")
        cboStatus.Items.Add("Inactive")
        cboStatus.Items.Add("Both")
        cboStatus.SelectedIndex = 0

    End Sub

    Private Sub frmParamGaugeFixMst_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 180, mReFormWidth - 30, mReFormWidth), 11592.4, 760)

        '    Frame1.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamGaugeFixMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        Dim mTypeNo As String
        Dim mDocNo As Double
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset

        With SprdMain
            .Row = .ActiveRow
            If .ActiveCol = ColCalibration Then
                .Col = ColType
                If Trim(.Text) = "GAUGE" Or Trim(.Text) = "PGG" Then
                    .Col = ColTypeNo
                    mTypeNo = Trim(.Text)
                    frmGaugeFixCal.MdiParent = Me.MdiParent
                    frmGaugeFixCal.frmGaugeFixCal_Activated(Nothing, New System.EventArgs())
                    frmGaugeFixCal.Show()
                    frmGaugeFixCal.txtGaugeNo.Text = mTypeNo
                    frmGaugeFixCal.txtGaugeNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                Else
                    .Col = ColDocNo
                    mDocNo = Val(.Text)

                    SqlStr = "SELECT DISTINCT DOCNO " & vbCrLf & " FROM QAL_GAUGE_CALIB_STD " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & mDocNo & ""

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If mRsTemp.EOF Then
                        MsgBox("Calibration Standards not defined.")
                    Else
                        .Col = ColTypeNo
                        mTypeNo = Trim(.Text)
                        frmGaugeFixInsp.MdiParent = Me.MdiParent
                        frmGaugeFixInsp.frmGaugeFixInsp_Activated(Nothing, New System.EventArgs())
                        frmGaugeFixInsp.Show()
                        frmGaugeFixInsp.txtTypeNo.Text = mTypeNo
                        frmGaugeFixInsp.txtTypeNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                    End If
                End If
            ElseIf .ActiveCol = ColHistory Then
                .Col = ColType
                If Trim(.Text) = "GAUGE" Or Trim(.Text) = "PGG" Then
                    .Col = ColTypeNo
                    mTypeNo = Trim(.Text)
                    frmParamGaugeFixCal.MdiParent = Me.MdiParent
                    frmParamGaugeFixCal.frmParamGaugeFixCal_Activated(Nothing, New System.EventArgs())
                    frmParamGaugeFixCal.Show()
                    frmParamGaugeFixCal.txtTypeNo.Text = mTypeNo
                    frmParamGaugeFixCal.txtTypeNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                    frmParamGaugeFixCal.cmdShow_Click(Nothing, New System.EventArgs())
                Else
                    .Col = ColDocNo
                    mDocNo = Val(.Text)

                    SqlStr = "SELECT DISTINCT DOCNO " & vbCrLf & " FROM QAL_GAUGE_CALIB_STD " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DOCNO =" & mDocNo & ""

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If mRsTemp.EOF Then
                        MsgBox("Calibration Standards not defined.")
                    Else
                        .Col = ColTypeNo
                        mTypeNo = Trim(.Text)
                        frmParamGaugeFixInsp.MdiParent = Me.MdiParent
                        frmParamGaugeFixInsp.frmParamGaugeFixInsp_Activated(Nothing, New System.EventArgs())
                        frmParamGaugeFixInsp.Show()
                        frmParamGaugeFixInsp.txtTypeNo.Text = mTypeNo
                        frmParamGaugeFixInsp.txtTypeNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                        frmParamGaugeFixInsp.cmdShow_Click(Nothing, New System.EventArgs())
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        '    SprdMain.DAutoCellTypes = True
        '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mDocNo As String
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColDocNo
        mDocNo = Trim(SprdMain.Text)
        frmGaugeFixMst.MdiParent = Me.MdiParent
        frmGaugeFixMst.frmGaugeFixMst_Activated(Nothing, New System.EventArgs())
        frmGaugeFixMst.Show()
        frmGaugeFixMst.txtNumber.Text = mDocNo
        frmGaugeFixMst.txtNumber_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub txtComponent_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComponent.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtComponent_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComponent.DoubleClick
        Call cmdSearchComponent_Click(cmdSearchComponent, New System.EventArgs())
    End Sub

    Private Sub txtComponent_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtComponent.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtComponent.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtComponent_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtComponent.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchComponent_Click(cmdSearchComponent, New System.EventArgs())
    End Sub

    Private Sub txtComponent_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtComponent.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtComponent.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtComponent.Text, "COMPONENT_DESC", "DocNo", "QAL_GAUGEFIX_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Component")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub chkAllComponent_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllComponent.CheckStateChanged
        Call PrintStatus(False)
        If chkAllComponent.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtComponent.Enabled = False
            cmdSearchComponent.Enabled = False
        Else
            txtComponent.Enabled = True
            cmdSearchComponent.Enabled = True
        End If
    End Sub

    Private Sub cmdSearchComponent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchComponent.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "COMPONENT_DESC", "", "", "", SqlStr) = True Then
            txtComponent.Text = AcName
        End If
        txtComponent.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "Customer", "DocNo", "QAL_GAUGEFIX_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Customer")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDrawingNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDrawingNo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDrawingNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDrawingNo.DoubleClick
        Call cmdSearchDRGNo_Click(cmdSearchDRGNo, New System.EventArgs())
    End Sub
    Private Sub txtDrawingNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDrawingNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDrawingNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDrawingNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDrawingNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDRGNo_Click(cmdSearchDRGNo, New System.EventArgs())
    End Sub
    Private Sub txtDrawingNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDrawingNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtDrawingNo.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDrawingNo.Text, "DRGNO", "DOCNO", "QAL_GAUGEFIX_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Model")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtLocation_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.DoubleClick
        Call cmdSearchLocation_Click(cmdSearchLocation, New System.EventArgs())
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLocation_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLocation.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchLocation_Click(cmdSearchLocation, New System.EventArgs())
    End Sub

    Private Sub txtLocation_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLocation.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtLocation.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtLocation.Text, "Location", "DocNo", "QAL_GAUGEFIX_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Location")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtModel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtModel_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.DoubleClick
        Call cmdSearchModel_Click(cmdSearchModel, New System.EventArgs())
    End Sub
    Private Sub txtModel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtModel.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtModel_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtModel.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchModel_Click(cmdSearchModel, New System.EventArgs())
    End Sub
    Private Sub txtModel_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtModel.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtModel.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtModel.Text, "Model", "DocNo", "QAL_GAUGEFIX_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Model")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColHistory
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColDocNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCustomer
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .ColsFrozen = ColModel

            .Col = ColDocNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColTypeNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColComponent
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDrawingNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColVDoneOn
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColValFrequency
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColVDueOn
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColIssueDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColQtyChecked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False
            .set_ColWidth(ColRemarks, 20)
            .Col = ColCalibration
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "Calibration"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColCalibration, 8)

            .Col = ColGoSize
            .ColHidden = True

            .Col = ColNoGoSize
            .ColHidden = True

            .Col = ColWearSize
            .ColHidden = True

            .Col = ColCompSize
            .ColHidden = True

            .Col = ColHistory
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "History"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColCalibration, 8)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColRemarks)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle
            '        SprdMain.DAutoCellTypes = True
            '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
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

        MakeSQL = " SELECT DocNo,Model,Description,Customer,Type,TypeNo, COMPONENT_DESC, DRGNO," & vbCrLf & " VDoneOn,Location,ValFrequency,VDueOn,IssueDate,CHECK_QTY,REMARKS,'', " & vbCrLf & " ReqGoSize,ReqNoGoSize,WearSize,CompSize,'' " & vbCrLf & " FROM QAL_GAUGEFIX_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkAllModel.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtModel.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND Model='" & MainClass.AllowSingleQuote(txtModel.Text) & "'"
        End If
        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomer.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND Customer='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        End If
        If chkAllTypeNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtTypeNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TypeNo='" & MainClass.AllowSingleQuote(txtTypeNo.Text) & "'"
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND Location='" & MainClass.AllowSingleQuote(txtLocation.Text) & "'"
        End If

        If chkAllType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtType.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND TYPE='" & MainClass.AllowSingleQuote(txtType.Text) & "'"
        End If

        If chkAllComponent.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtComponent.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND COMPONENT_DESC='" & MainClass.AllowSingleQuote(txtComponent.Text) & "'"
        End If

        If chkAllDRG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDrawingNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DRGNO='" & MainClass.AllowSingleQuote(txtDrawingNo.Text) & "'"
        End If


        If cboCDDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND VDueOn BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboCDDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND VDueOn>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCDDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND VDueOn<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCDDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND VDueOn=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboLCDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND VDoneOn BETWEEN '" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "' AND '" & VB6.Format(txtDate4.Text, "DD-MMM-YYYY") & "' "
        ElseIf cboLCDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND VDoneOn>TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboLCDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND VDoneOn<TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboLCDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND VDoneOn=TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboStatus.Text = "Active" Then
            MakeSQL = MakeSQL & vbCrLf & " AND STATUS='O'"
        ElseIf cboStatus.Text = "Inactive" Then
            MakeSQL = MakeSQL & vbCrLf & " AND STATUS='C'"
        End If

        If OptOrderBy(0).Checked = True Then 'VDueOn
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY VDueOn"
        ElseIf OptOrderBy(1).Checked = True Then  'Model
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY Model"
        ElseIf OptOrderBy(2).Checked = True Then  'Customer
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY Customer"
        ElseIf OptOrderBy(3).Checked = True Then  'Location
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY Location"
        ElseIf OptOrderBy(4).Checked = True Then  'DocNo
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY DocNo"
        ElseIf OptOrderBy(5).Checked = True Then  'Type
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY TYPE"
        ElseIf OptOrderBy(6).Checked = True Then  'Component
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY COMPONENT_DESC"
        ElseIf OptOrderBy(7).Checked = True Then  'Component
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY DRGNO"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllModel.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtModel.Text) = "" Then
            MsgBox("Please Select Model")
            txtModel.Focus()
            Exit Function
        End If

        If chkAllDRG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDrawingNo.Text) = "" Then
            MsgBox("Please Select Drawing No")
            txtDrawingNo.Focus()
            Exit Function
        End If

        If chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtCustomer.Text) = "" Then
            MsgBox("Please Select Customer")
            txtCustomer.Focus()
            Exit Function
        End If
        If chkAllTypeNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtTypeNo.Text) = "" Then
            MsgBox("Please Select Type No.")
            txtTypeNo.Focus()
            Exit Function
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) = "" Then
            MsgBox("Please Select Location.")
            txtLocation.Focus()
            Exit Function
        End If

        If chkAllType.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtType.Text) = "" Then
            MsgBox("Please Select Type.")
            txtType.Focus()
            Exit Function
        End If

        If chkAllComponent.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtComponent.Text) = "" Then
            MsgBox("Please Select Component.")
            txtComponent.Focus()
            Exit Function
        End If

        If cboCDDate.Text = "Between" Then
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
        If cboCDDate.Text = "After" Or cboCDDate.Text = "Before" Or cboCDDate.Text = "On Date" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
        End If
        If cboLCDate.Text = "Between" Then
            If Not IsDate(txtDate3.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate3.Focus()
                Exit Function
            End If
            If Not IsDate(txtDate4.Text) Then
                MsgBox("Date2 is Blank.")
                txtDate4.Focus()
                Exit Function
            End If
        End If
        If cboLCDate.Text = "After" Or cboLCDate.Text = "Before" Or cboLCDate.Text = "On Date" Then
            If Not IsDate(txtDate3.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate3.Focus()
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtType.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtType.DoubleClick
        Call cmdSearchType_Click(cmdSearchType, New System.EventArgs())
    End Sub

    Private Sub txtType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchType_Click(cmdSearchType, New System.EventArgs())
    End Sub

    Private Sub txtType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtType.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtType.Text, "TYPE", "DocNo", "QAL_GAUGEFIX_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Type")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub chkAllType_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllType.CheckStateChanged
        Call PrintStatus(False)
        If chkAllType.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtType.Enabled = False
            cmdSearchType.Enabled = False
        Else
            txtType.Enabled = True
            cmdSearchType.Enabled = True
        End If
    End Sub

    Private Sub cmdSearchType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchType.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "QAL_GAUGEFIX_MST", "Type", "", "", "", SqlStr) = True Then
            txtType.Text = AcName
        End If
        txtType.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub


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

    Private Sub txtTypeNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTypeNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtTypeNo.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtTypeNo.Text, "TypeNo", "DocNo", "QAL_GAUGEFIX_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid TypeNo")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
