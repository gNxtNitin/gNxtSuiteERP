Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMachineMst
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColMachineNo As Short = 1
    Private Const ColItemCode As Short = 2
    Private Const ColMachineDesc As Short = 3
    Private Const ColSpec As Short = 4
    Private Const ColCapacity As Short = 5
    Private Const ColMake As Short = 6
    Private Const ColDept As Short = 7
    Private Const ColLocation As Short = 8
    Private Const ColInstDate As Short = 9
    Private Const ColKeyMachine As Short = 10
    Private Const ColMachineBD As Short = 11
    Private Const ColOperation As Short = 12
    Private Const ColPiecesHr As Short = 13
    Private Const ColWorkingHrs As Short = 14
    Private Const ColUnitsHr As Short = 15
    Private Const ColCheckType As Short = 16
    Private Const ColLastPM As Short = 17
    Private Const ColDuePM As Short = 18
    Private Const ColPM As Short = 19
    Private Const ColHistory As Short = 20

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboLastPMDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLastPMDate.SelectedIndexChanged
        If cboLastPMDate.Text = "None" Then
            txtDate1.Visible = False
            lblDate1.Visible = False
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboLastPMDate.Text = "Between" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = True
            lblDate2.Visible = True
        ElseIf cboLastPMDate.Text = "After" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboLastPMDate.Text = "Before" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        ElseIf cboLastPMDate.Text = "On Date" Then
            txtDate1.Visible = True
            lblDate1.Visible = True
            txtDate2.Visible = False
            lblDate2.Visible = False
        End If
    End Sub

    Private Sub cboDuePMDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDuePMDate.SelectedIndexChanged
        If cboDuePMDate.Text = "None" Then
            txtDate3.Visible = False
            lblDate3.Visible = False
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboDuePMDate.Text = "Between" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = True
            lblDate4.Visible = True
        ElseIf cboDuePMDate.Text = "After" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboDuePMDate.Text = "Before" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        ElseIf cboDuePMDate.Text = "On Date" Then
            txtDate3.Visible = True
            lblDate3.Visible = True
            txtDate4.Visible = False
            lblDate4.Visible = False
        End If
    End Sub

    Private Sub chkAllMachineDesc_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllMachineDesc.CheckStateChanged
        Call PrintStatus(False)
        If chkAllMachineDesc.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMachineDesc.Enabled = False
            cmdSearchMachineDesc.Enabled = False
        Else
            txtMachineDesc.Enabled = True
            cmdSearchMachineDesc.Enabled = True
        End If
    End Sub

    Private Sub chkAllMake_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllMake.CheckStateChanged
        Call PrintStatus(False)
        If chkAllMake.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMake.Enabled = False
            cmdSearchMake.Enabled = False
        Else
            txtMake.Enabled = True
            cmdSearchMake.Enabled = True
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

    Private Sub chkAllMachineNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllMachineNo.CheckStateChanged
        Call PrintStatus(False)
        If chkAllMachineNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtMachineNo.Enabled = False
            cmdSearchMachineNo.Enabled = False
        Else
            txtMachineNo.Enabled = True
            cmdSearchMachineNo.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMachineMstList(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnMachineMstList(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnMachineMstList(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Machine Master List"
        If chkAllMachineNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachineNo.Text) <> "" Then
            mSubTitle = mSubTitle & " [Machine No : " & txtMachineNo.Text & " ]"
        End If
        If chkAllMachineDesc.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachineDesc.Text) <> "" Then
            mSubTitle = mSubTitle & " [Machine Desc : " & txtMachineDesc.Text & " ]"
        End If
        If chkAllMake.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMake.Text) <> "" Then
            mSubTitle = mSubTitle & " [Make : " & txtMake.Text & " ]"
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) <> "" Then
            mSubTitle = mSubTitle & " [Location : " & txtLocation.Text & " ]"
        End If
        If cboLastPMDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [Calib Due Between  " & txtDate1.Text & " And " & txtDate2.Text & " ]"
        End If
        If cboLastPMDate.Text = "After" Then
            mSubTitle = mSubTitle & " [Calib Due After  " & txtDate1.Text & " ]"
        End If
        If cboLastPMDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Calib Due Before  " & txtDate1.Text & " ]"
        End If
        If cboLastPMDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Calib Due On  " & txtDate1.Text & " ]"
        End If
        If cboDuePMDate.Text = "Between" Then
            mSubTitle = mSubTitle & " [ Last Calib Between  " & txtDate3.Text & " And " & txtDate4.Text & " ]"
        End If
        If cboDuePMDate.Text = "After" Then
            mSubTitle = mSubTitle & " [Last Calib After  " & txtDate3.Text & " ]"
        End If
        If cboDuePMDate.Text = "Before" Then
            mSubTitle = mSubTitle & " [Last Calib Before  " & txtDate3.Text & " ]"
        End If
        If cboDuePMDate.Text = "On Date" Then
            mSubTitle = mSubTitle & " [Last Calib On  " & txtDate3.Text & " ]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MachList.rpt"

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

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        'On Error GoTo ErrPart
        'Dim SqlStr As String
        'Dim CntRow As Long
        'Dim mMachineNo As Double
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
        '            .Col = ColMachineNo
        '            mMachineNo = Trim(.Text)
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
        '                SqlStr = " UPDATE MAN_MACHINE_MST SET " & vbCrLf _
        ''                            & " LCDATE=TO_DATE('" & vb6.Format(mLCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        ''                            & " CDATE=TO_DATE('" & vb6.Format(mCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        ''                            & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''                            & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        ''                            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''                            & " AND DOCNO =" & mMachineNo & ""
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

    Private Sub cmdSearchMachineNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineNo.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = " (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf


        If MainClass.SearchGridMaster(txtMachineNo.Text, "MAN_MACHINE_MST", "MACHINE_NO", "MACHINE_DESC", "MAKE", "LOCATION", SqlStr) = True Then
            txtMachineNo.Text = AcName
        End If
        txtMachineNo.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMachineDesc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineDesc.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        ''SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = " (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf

        If MainClass.SearchGridMaster(txtMachineDesc.Text, "MAN_MACHINE_MST", "MACHINE_DESC", , , , SqlStr) = True Then
            txtMachineDesc.Text = AcName
        End If
        txtMachineDesc.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMake_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMake.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'SqlStr = " (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf

        If MainClass.SearchGridMaster(txtMake.Text, "MAN_MACHINE_MST", "MAKE", , , , SqlStr) = True Then
            txtMake.Text = AcName
        End If
        txtMake.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchLocation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchLocation.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'SqlStr = " (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf

        If MainClass.SearchGridMaster(txtLocation.Text, "MAN_MACHINE_MST", "LOCATION", , , , SqlStr) = True Then
            txtLocation.Text = AcName
        End If
        txtLocation.Focus()
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

    Private Sub frmParamMachineMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Machine Master List"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMachineMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAllMachineNo.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllMachineNo_CheckStateChanged(chkAllMachineNo, New System.EventArgs())
        chkAllMachineDesc.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllMachineDesc_CheckStateChanged(chkAllMachineDesc, New System.EventArgs())
        chkAllMake.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllMake_CheckStateChanged(chkAllMake, New System.EventArgs())
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
        cboLastPMDate.Items.Clear()
        cboLastPMDate.Items.Add("None")
        cboLastPMDate.Items.Add("Between")
        cboLastPMDate.Items.Add("After")
        cboLastPMDate.Items.Add("Before")
        cboLastPMDate.Items.Add("On Date")
        cboLastPMDate.SelectedIndex = 0

        cboDuePMDate.Items.Clear()
        cboDuePMDate.Items.Add("None")
        cboDuePMDate.Items.Add("Between")
        cboDuePMDate.Items.Add("After")
        cboDuePMDate.Items.Add("Before")
        cboDuePMDate.Items.Add("On Date")
        cboDuePMDate.SelectedIndex = 0

        cboKey.Items.Clear()
        cboKey.Items.Add("Yes")
        cboKey.Items.Add("No")
        cboKey.Items.Add("Both")
        cboKey.SelectedIndex = 2

        cboBreakDown.Items.Clear()
        cboBreakDown.Items.Add("Yes")
        cboBreakDown.Items.Add("No")
        cboBreakDown.Items.Add("Both")
        cboBreakDown.SelectedIndex = 2

        cboStatus.Items.Clear()
        cboStatus.Items.Add("OPEN/ACTIVE")
        cboStatus.Items.Add("TRANSFER SALE")
        cboStatus.Items.Add("SCRAP SALE")
        cboStatus.Items.Add("CLOSE/INACTIVE")
        cboStatus.Items.Add("ALL")
        cboStatus.SelectedIndex = 0

        cboMaintType.Items.Clear()
        cboMaintType.Items.Add("Preventive")
        cboMaintType.Items.Add("Preductive")
        cboMaintType.Items.Add("Hour Basis")
        cboMaintType.Items.Add("All")
        cboMaintType.SelectedIndex = 3
    End Sub

    Private Sub frmParamMachineMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        Dim mMachineNo As String
        Dim mMachineDesc As String
        Dim mCheckType As String
        Dim mSpec As String
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset

        With SprdMain
            .Row = .ActiveRow
            If .ActiveCol = ColPM Then
                .Col = ColMachineNo
                mMachineNo = Trim(.Text)

                .Col = ColMachineDesc
                mMachineDesc = Trim(.Text)

                .Col = ColSpec
                mSpec = Trim(.Text)

                .Col = ColCheckType
                mCheckType = Trim(.Text)

                SqlStr = " SELECT AUTO_KEY_CP " & vbCrLf _
                                & " FROM MAN_MACHINE_CP_HDR " & vbCrLf _
                                & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                                & " AND MACHINE_DESC ='" & MainClass.AllowSingleQuote(mMachineDesc) & "' " & vbCrLf _
                                & " AND MACHINE_SPEC ='" & MainClass.AllowSingleQuote(mSpec) & "' " & vbCrLf _
                                & " AND CHECK_TYPE ='" & MainClass.AllowSingleQuote(mCheckType) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If mRsTemp.EOF Then
                    MsgBox("Preventive Maintenance Check Points not defined.")
                Else
                    If CheckMachinePMSchd(mMachineNo, mCheckType, RunDate) = True Then
                        frmPMStatus.MdiParent = Me.MdiParent
                        frmPMStatus.frmPMStatus_Activated(Nothing, New System.EventArgs())
                        frmPMStatus.Show()
                        frmPMStatus.txtMachineNo.Text = mMachineNo
                        frmPMStatus.txtMachineNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                    End If
                End If
            ElseIf .ActiveCol = ColHistory Then
                .Col = ColMachineNo
                mMachineNo = Trim(.Text)
                frmParamMachineHis.MdiParent = Me.MdiParent
                frmParamMachineHis.frmParamMachineHis_Activated(Nothing, New System.EventArgs())
                frmParamMachineHis.Show()
                frmParamMachineHis.txtMachineNo.Text = mMachineNo
                frmParamMachineHis.txtMachineNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
                frmParamMachineHis.cmdShow_Click(Nothing, New System.EventArgs())
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
        Dim mMachineNo As String
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColMachineNo
        mMachineNo = Trim(SprdMain.Text)
        frmMachineMaster.MdiParent = Me.MdiParent
        frmMachineMaster.Show()
        frmMachineMaster.frmMachineMaster_Activated(Nothing, New System.EventArgs())
        frmMachineMaster.txtMachineNo.Text = mMachineNo
        frmMachineMaster.txtMachineNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub
        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = " (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf

        If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_NO", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Machine No", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineDesc.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtMachineDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineDesc.DoubleClick
        Call cmdSearchMachineDesc_Click(cmdSearchMachineDesc, New System.EventArgs())
    End Sub

    Private Sub txtMachineDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachineDesc_Click(cmdSearchMachineDesc, New System.EventArgs())
    End Sub

    Private Sub txtMachineDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachineDesc.Text) = "" Then GoTo EventExitSub
        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = " (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf

        If MainClass.ValidateWithMasterTable(txtMachineDesc.Text, "MACHINE_DESC", "MACHINE_NO", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Machine Desc", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtMake_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.DoubleClick
        Call cmdSearchMake_Click(cmdSearchMake, New System.EventArgs())
    End Sub

    Private Sub txtMake_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMake.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMake.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMake_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMake.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMake_Click(cmdSearchMake, New System.EventArgs())
    End Sub

    Private Sub txtMake_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMake.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMake.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'SqlStr = " (COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf

        If MainClass.ValidateWithMasterTable(txtMake.Text, "Make", "Make", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Make", vbInformation)
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
        If MainClass.ValidateWithMasterTable(txtLocation.Text, "LOCATION", "LOCATION", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Location", vbInformation)
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

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColMachineDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .ColsFrozen = ColMachineDesc

            .Col = ColSpec
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColCapacity
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColMake
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColInstDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColKeyMachine
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColMachineBD
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColOperation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColPiecesHr
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeEditMultiLine = False

            .Col = ColWorkingHrs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeEditMultiLine = False

            .Col = ColUnitsHr
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .TypeEditLen = 255
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeEditMultiLine = False

            .Col = ColCheckType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColLastPM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColDuePM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = False

            .Col = ColPM
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "PM"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColPM, 5)

            .Col = ColHistory
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "History"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColHistory, 6)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColDuePM)
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

        MakeSQL = " SELECT MAN_MACHINE_MST.MACHINE_NO,MAN_MACHINE_MST.MACHINE_ITEM_CODE,MACHINE_DESC,MACHINE_SPEC, " & vbCrLf _
            & " CAPACITY,MAKE,DEPT_CODE,LOCATION,MACHINE_INST_DATE,KEY_MACHINE, " & vbCrLf _
            & " MACHINE_UB,OPR_CODE,NO_OF_PIECES,NO_OF_WORKHRS,NO_OF_UNITS, " & vbCrLf _
            & " CHECK_TYPE,LAST_PM,DUE_PM,'','' " & vbCrLf _
            & " FROM MAN_MACHINE_MST,MAN_MACHINE_MAINT_TRN " & vbCrLf _
            & " WHERE MAN_MACHINE_MST.TRANSFER_UNIT_CODE=MAN_MACHINE_MAINT_TRN.COMPANY_CODE (+) " & vbCrLf _
            & " AND MAN_MACHINE_MST.MACHINE_NO=MAN_MACHINE_MAINT_TRN.MACHINE_NO (+) "

        '& vbCrLf _
        '    & " AND MAN_MACHINE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQL = MakeSQL & vbCrLf _
            & " AND (MAN_MACHINE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " OR MAN_MACHINE_MST.TRANSFER_UNIT_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")" & vbCrLf


        If chkAllMachineNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachineNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAN_MACHINE_MST.MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        End If
        If chkAllMachineDesc.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachineDesc.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "'"
        End If
        If chkAllMake.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMake.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAKE='" & MainClass.AllowSingleQuote(txtMake.Text) & "'"
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "'"
        End If

        If cboLastPMDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LAST_PM BETWEEN TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboLastPMDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LAST_PM>TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboLastPMDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LAST_PM<TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboLastPMDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND LAST_PM=TO_DATE('" & VB6.Format(txtDate1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboDuePMDate.Text = "Between" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DUE_PM BETWEEN TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDate4.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        ElseIf cboDuePMDate.Text = "After" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DUE_PM>TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDuePMDate.Text = "Before" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DUE_PM<TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf cboDuePMDate.Text = "On Date" Then
            MakeSQL = MakeSQL & vbCrLf & " AND DUE_PM=TO_DATE('" & VB6.Format(txtDate3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If cboKey.Text = "Yes" Then
            MakeSQL = MakeSQL & vbCrLf & " AND KEY_MACHINE='Y'"
        ElseIf cboKey.Text = "No" Then
            MakeSQL = MakeSQL & vbCrLf & " AND KEY_MACHINE='N'"
        End If

        If cboBreakDown.Text = "Yes" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MACHINE_UB='Y'"
        ElseIf cboBreakDown.Text = "No" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MACHINE_UB='N'"
        End If

        If cboStatus.Text <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & " AND STATUS='" & VB.Left(cboStatus.Text, 1) & "'"
        End If

        If cboMaintType.Text = "Preventive" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_TYPE='P'"
        ElseIf cboMaintType.Text = "Preductive" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_TYPE='D'"
        ElseIf cboMaintType.Text = "Hour Basis" Then
            MakeSQL = MakeSQL & vbCrLf & " AND MAINT_TYPE='H'"
        End If

        If OptOrderBy(0).Checked = True Then 'Machine No
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY MAN_MACHINE_MST.MACHINE_NO"
        ElseIf OptOrderBy(1).Checked = True Then  'Machine Desc
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY MACHINE_DESC"
        ElseIf OptOrderBy(2).Checked = True Then  'Last PM Date
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY LAST_PM"
        ElseIf OptOrderBy(3).Checked = True Then  'PM Due Date
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY DUE_PM"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllMachineNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachineNo.Text) = "" Then
            MsgBox("Please Select Machine No")
            txtMachineNo.Focus()
            Exit Function
        End If
        If chkAllMachineDesc.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMachineDesc.Text) = "" Then
            MsgBox("Please Select Machine Desc")
            txtMachineDesc.Focus()
            Exit Function
        End If
        If chkAllMake.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtMake.Text) = "" Then
            MsgBox("Please Select Make")
            txtMake.Focus()
            Exit Function
        End If
        If chkAllLocation.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtLocation.Text) = "" Then
            MsgBox("Please Select Location")
            txtLocation.Focus()
            Exit Function
        End If
        If cboLastPMDate.Text = "Between" Then
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
        If cboLastPMDate.Text = "After" Or cboLastPMDate.Text = "Before" Or cboLastPMDate.Text = "On Date" Then
            If Not IsDate(txtDate1.Text) Then
                MsgBox("Date1 is Blank.")
                txtDate1.Focus()
                Exit Function
            End If
        End If
        If cboDuePMDate.Text = "Between" Then
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
        If cboDuePMDate.Text = "After" Or cboDuePMDate.Text = "Before" Or cboDuePMDate.Text = "On Date" Then
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

    Private Sub frmParamMachineMst_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        'SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        'fraGridView.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'FraTrans.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)

        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
