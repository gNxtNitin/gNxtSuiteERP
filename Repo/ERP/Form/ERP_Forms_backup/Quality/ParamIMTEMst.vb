Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamIMTEMst
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColDocNo As Short = 1
    Private Const ColItemCode As Short = 2
    Private Const ColDescription As Short = 3
    Private Const ColENo As Short = 4
    Private Const ColType As Short = 5
    Private Const ColMaster As Short = 6
    Private Const ColMarkNo As Short = 7
    Private Const ColLC As Short = 8
    Private Const ColMake As Short = 9
    Private Const ColRange As Short = 10
    Private Const ColLocation As Short = 11
    Private Const ColValFrequency As Short = 12
    Private Const ColLCDate As Short = 13
    Private Const ColCaliFacil As Short = 14
    Private Const ColIssueDate As Short = 15
    Private Const ColCDDate As Short = 16
    Private Const ColIssue As Short = 17
    Private Const ColCalibration As Short = 18
    Private Const ColHistory As Short = 19

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        cmdPreview.Enabled = pPrintEnable
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

    Private Sub chkAllENo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllENo.CheckStateChanged
        Call PrintStatus(False)
        If chkAllENo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtENo.Enabled = False
            cmdSearchENo.Enabled = False
        Else
            txtENo.Enabled = True
            cmdSearchENo.Enabled = True
        End If
    End Sub

    Private Sub chkAllIssueTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllIssueTo.CheckStateChanged
        Call PrintStatus(False)
        If chkAllIssueTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtIssueTo.Enabled = False
            cmdSearchIssueTo.Enabled = False
        Else
            txtIssueTo.Enabled = True
            cmdSearchIssueTo.Enabled = True
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

    Private Sub chkAllEName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEName.CheckStateChanged
        Call PrintStatus(False)
        If chkAllEName.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEName.Enabled = False
            cmdSearchEName.Enabled = False
        Else
            txtEName.Enabled = True
            cmdSearchEName.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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
        mTitle = "IMTE Master List"
        If chkAllEName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtEName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Description : " & txtEName.Text & " ]"
        End If
        If chkAllIssueTo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtIssueTo.Text) <> "" Then
            mSubTitle = mSubTitle & " [ISSUETO : " & txtIssueTo.Text & " ]"
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) <> "" Then
            mSubTitle = mSubTitle & " [Location : " & txtLocation.Text & " ]"
        End If
        If cboCDDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [Calib Due Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboCDDate.Text = "After" Then
            mSubTitle = mSubTitle & " [Calib Due After  " & txtDate1.Text & " ]"
        End If
        If cboCDDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Calib Due Before  " & txtDate1.Text & " ]"
        End If
        If cboCDDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Calib Due On  " & txtDate1.Text & " ]"
        End If
        If cboLCDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [ Last Calib Between  " & txtDate3.Text & " And " & txtDate4.Text & " ]"
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

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IMTEReg.rpt"

        '    SqlStr = MakeSQL
        SqlStr = ""

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, ColDocNo, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr


        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
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

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        'On Error GoTo ErrPart
        'Dim SqlStr As String
        'Dim CntRow As Long
        'Dim mDocNo As Double
        'Dim mLCDate As String
        'Dim mCDate As String
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
        '            mLCDate = Format(.Text, "DD/MM/YYYY")
        '
        '            .Col = ColValFrequency
        '            mFrequency = Val(.Text)
        '
        '            If IsDate(mLCDate) Then
        '
        '                mCDate = DateAdd("d", (Val(mFrequency) * 30), mLCDate)
        '
        '                SqlStr = " UPDATE QAL_IMTE_MST SET " & vbCrLf _
        ''                            & " LCDATE=TO_DATE('" & vb6.Format(mLCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        ''                            & " CDATE=TO_DATE('" & vb6.Format(mCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
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
    End Sub

    Private Sub cmdSearchENo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchENo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtENo.Text, "QAL_IMTE_MST", "E_NO", , , , SqlStr) = True Then
            txtENo.Text = AcName
        End If
        txtENo.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchIssueTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchIssueTo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtIssueTo.Text, "QAL_IMTE_MST", "ISSUETO", , , , SqlStr) = True Then
            txtIssueTo.Text = AcName
        End If
        txtIssueTo.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchLocation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchLocation.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtLocation.Text, "QAL_IMTE_MST", "Location", , , , SqlStr) = True Then
            txtLocation.Text = AcName
        End If
        txtLocation.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchEName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEName.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtEName.Text, "QAL_IMTE_MST", "Description", , , , SqlStr) = True Then
            txtEName.Text = AcName
        End If
        txtEName.Focus()
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
    Private Sub frmParamIMTEMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "IMTE Master List"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamIMTEMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAllEName.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllEName_CheckStateChanged(chkAllEName, New System.EventArgs())
        chkAllENo.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllENo_CheckStateChanged(chkAllENo, New System.EventArgs())
        chkAllIssueTo.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllIssueTo_CheckStateChanged(chkAllIssueTo, New System.EventArgs())
        chkAllLocation.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllLocation_CheckStateChanged(chkAllLocation, New System.EventArgs())
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

        cboCaliFacil.Items.Clear()
        cboCaliFacil.Items.Add("INSIDE")
        cboCaliFacil.Items.Add("OUTSIDE")
        cboCaliFacil.Items.Add("CUSTOMER")
        cboCaliFacil.Items.Add("ALL")
        cboCaliFacil.SelectedIndex = 3

        cboType.Items.Clear()
        cboType.Items.Add("ATTRIBUTE")
        cboType.Items.Add("VARIABLE")
        cboType.Items.Add("BOTH")
        cboType.SelectedIndex = 2

        cboMaster.Items.Clear()
        cboMaster.Items.Add("MASTER")
        cboMaster.Items.Add("NON-MASTER")
        cboMaster.Items.Add("BOTH")
        cboMaster.SelectedIndex = 2
    End Sub

    Private Sub frmParamIMTEMst_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame5.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11592.4, 763)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamIMTEMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        Dim mDescription As String
        Dim mLC As String
        Dim mDocNo As Double
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset

        With SprdMain
            .Row = .ActiveRow
            If .ActiveCol = ColCalibration Then
                .Col = ColType
                If Trim(.Text) = "ATTRIBUTE" Then
                    .Col = ColDocNo
                    mDocNo = Val(.Text)
                    frmIMTEInsp.MdiParent = Me.MdiParent
                    frmIMTEInsp.frmIMTEInsp_Activated(Nothing, New System.EventArgs())
                    frmIMTEInsp.Show()
                    frmIMTEInsp.txtDocNo.Text = CStr(mDocNo)
                    frmIMTEInsp.txtDocNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                Else
                    .Col = ColDescription
                    mDescription = Trim(.Text)

                    .Col = ColLC
                    mLC = Trim(.Text)

                    SqlStr = "SELECT AUTO_KEY_PE " & vbCrLf _
                                        & " FROM QAL_IMTE_PE_HDR " & vbCrLf _
                                        & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                                        & " AND DESCRIPTION ='" & MainClass.AllowSingleQuote(mDescription) & "' " & vbCrLf _
                                        & " AND L_C ='" & MainClass.AllowSingleQuote(mLC) & "' "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If mRsTemp.EOF Then
                        MsgBox("Permissible Errors not defined.")
                    Else
                        .Col = ColDocNo
                        mDocNo = Val(.Text)
                        frmIMTEInsp.MdiParent = Me.MdiParent
                        frmIMTEInsp.frmIMTEInsp_Activated(Nothing, New System.EventArgs())
                        frmIMTEInsp.Show()
                        frmIMTEInsp.txtDocNo.Text = CStr(mDocNo)
                        frmIMTEInsp.txtDocNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                    End If
                End If
            ElseIf .ActiveCol = ColHistory Then
                .Col = ColType
                If Trim(.Text) = "ATTRIBUTE" Then
                    .Col = ColDocNo
                    mDocNo = Val(.Text)
                    frmParamIMTECal.MdiParent = Me.MdiParent
                    frmParamIMTECal.frmParamIMTECal_Activated(Nothing, New System.EventArgs())
                    frmParamIMTECal.Show()
                    frmParamIMTECal.txtDocNo.Text = CStr(mDocNo)
                    frmParamIMTECal.txtDocNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                    frmParamIMTECal.cmdShow_Click(Nothing, New System.EventArgs())
                Else
                    .Col = ColDescription
                    mDescription = Trim(.Text)

                    .Col = ColLC
                    mLC = Trim(.Text)

                    SqlStr = "SELECT AUTO_KEY_PE " & vbCrLf & " FROM QAL_IMTE_PE_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DESCRIPTION ='" & MainClass.AllowSingleQuote(mDescription) & "' " & vbCrLf & " AND L_C ='" & MainClass.AllowSingleQuote(mLC) & "' "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If mRsTemp.EOF Then
                        MsgBox("Permissible Errors not defined.")
                    Else
                        .Col = ColDocNo
                        mDocNo = Val(.Text)
                        frmParamIMTEInsp.MdiParent = Me.MdiParent
                        frmParamIMTEInsp.frmParamIMTEInsp_Activated(Nothing, New System.EventArgs())
                        frmParamIMTEInsp.Show()
                        frmParamIMTEInsp.txtDocNo.Text = CStr(mDocNo)
                        frmParamIMTEInsp.txtDocNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                        frmParamIMTEInsp.cmdShow_Click(Nothing, New System.EventArgs())
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mDocNo As String
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColDocNo
        mDocNo = Trim(SprdMain.Text)
        frmIMTEMst.MdiParent = Me.MdiParent
        frmIMTEMst.frmIMTEMst_Activated(Nothing, New System.EventArgs())
        frmIMTEMst.Show()
        frmIMTEMst.txtNumber.Text = mDocNo
        frmIMTEMst.txtNumber_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub txtENO_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtENo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtENo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtENo.DoubleClick
        Call cmdSearchENo_Click(cmdSearchENo, New System.EventArgs())
    End Sub

    Private Sub txtENo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtENo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtENo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtENo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtENo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchENo_Click(cmdSearchENo, New System.EventArgs())
    End Sub

    Private Sub txtENo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtENo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtENo.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtENo.Text, "E_NO", "DocNo", "QAL_IMTE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid E. No.", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIssueTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtIssueTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssueTo.DoubleClick
        Call cmdSearchIssueTo_Click(cmdSearchIssueTo, New System.EventArgs())
    End Sub

    Private Sub txtIssueTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIssueTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIssueTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIssueTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtIssueTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchIssueTo_Click(cmdSearchIssueTo, New System.EventArgs())
    End Sub

    Private Sub txtIssueTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIssueTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtIssueTo.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtIssueTo.Text, "ISSUETO", "DocNo", "QAL_IMTE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Employee", vbInformation)
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
        If MainClass.ValidateWithMasterTable(txtLocation.Text, "Location", "DocNo", "QAL_IMTE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Location", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtEName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEName.DoubleClick
        Call cmdSearchEName_Click(cmdSearchEName, New System.EventArgs())
    End Sub

    Private Sub txtEName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEName_Click(cmdSearchEName, New System.EventArgs())
    End Sub

    Private Sub txtEName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtEName.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtEName.Text, "Description", "DocNo", "QAL_IMTE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Description", vbInformation)
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

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColENo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .ColsFrozen = ColENo

            .Col = ColType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColMaster
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColMarkNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColLC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColMake
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

            .Col = ColRange
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColIssueDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColCDDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeDateSeparator = Asc("/")
            .set_ColWidth(2, 10)

            .Col = ColCaliFacil
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCDDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColIssue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCalibration
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "Calibration"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColCalibration, 8)

            .Col = ColHistory
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "History"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColHistory, 8)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColIssue)
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

        MakeSQL = " SELECT IH.DOCNO, IH.ITEMCODE, IH.Description, " & vbCrLf & " IH.E_NO, IH.TYPE, IH.MASTER_INST, IH.MARKERS_NO, IH.L_C, " & vbCrLf & " IH.MAKE_NAME, IH.RANGE, IH.LOCATION, " & vbCrLf & " IH.VALFREQUENCY, IH.LCDATE, IH.CALIFACIL, " & vbCrLf & " GetIMTEIssueDate(IH.COMPANY_CODE,IH.DOCNO),IH.CDATE, GetIMTEIssueTo(IH.COMPANY_CODE,IH.DOCNO),'' ,''" & vbCrLf & " FROM QAL_IMTE_MST IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkAllEName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtEName.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.Description='" & MainClass.AllowSingleQuote(txtEName.Text) & "'"
        End If
        If chkAllENo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtENo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.E_NO='" & MainClass.AllowSingleQuote(txtENo.Text) & "'"
        End If
        If chkAllIssueTo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtIssueTo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND GetIMTEIssueTo(IH.COMPANY_CODE,IH.DOCNO)='" & MainClass.AllowSingleQuote(txtIssueTo.Text) & "'"
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.Location='" & MainClass.AllowSingleQuote(txtLocation.Text) & "'"
        End If

        If cboCDDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.CDATE BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboCDDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.CDATE>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCDDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.CDATE<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboCDDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.CDATE=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboLCDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.LCDATE BETWEEN TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate4.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboLCDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.LCDATE>TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboLCDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.LCDATE<TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboLCDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.LCDATE=TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboStatus.Text = "Active" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.STATUS='O'"
        ElseIf cboStatus.Text = "Inactive" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.STATUS='C'"
        End If

        If cboCaliFacil.SelectedIndex <> 3 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.CALIFACIL='" & Trim(cboCaliFacil.Text) & "'"
        End If

        If cboType.SelectedIndex <> 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.TYPE='" & Trim(cboType.Text) & "'"
        End If

        If cboMaster.Text = "MASTER" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.MASTER_INST='Y'"
        ElseIf cboMaster.Text = "NON-MASTER" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.MASTER_INST='N'"
        End If

        If OptOrderBy(0).Checked = True Then 'CDATE
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.CDATE"
        ElseIf OptOrderBy(1).Checked = True Then  'Description
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY GetIMTEIssueTo(IH.COMPANY_CODE,IH.DOCNO)"
        ElseIf OptOrderBy(2).Checked = True Then  'ISSUETO
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.DESCRIPTION"
        ElseIf OptOrderBy(3).Checked = True Then  'Location
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.Location"
        ElseIf OptOrderBy(4).Checked = True Then  'L C Date
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.LCDATE"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function GetIssueTo(ByRef mDocNo As Double) As String

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetIssueTo = ""
        SqlStr = " SELECT DOCNO, ISSUE_TO " & vbCrLf & " FROM QAL_IMTE_ISS_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DOCNO=" & mDocNo & ""

        SqlStr = SqlStr & vbCrLf & " AND (ID.RECD_DATE IS NULL OR ID.RECD_DATE ='')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetIssueTo = IIf(IsDbNull(RsTemp.Fields("ISSUE_TO").Value), "", RsTemp.Fields("ISSUE_TO").Value)
        End If

        Exit Function
ERR1:
        GetIssueTo = ""
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllEName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtEName.Text) = "" Then
            MsgBox("Please Select Description")
            txtEName.Focus()
            Exit Function
        End If

        If chkAllIssueTo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtIssueTo.Text) = "" Then
            MsgBox("Please Select ISSUE TO")
            txtIssueTo.Focus()
            Exit Function
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) = "" Then
            MsgBox("Please Select Location.")
            txtLocation.Focus()
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
End Class
