Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamInspStdLst
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColStage As Short = 1
    Private Const ColDocNo As Short = 2
    Private Const ColDateOrig As Short = 3
    Private Const ColRevNo As Short = 4
    Private Const ColRevDate As Short = 5
    Private Const ColPartNo As Short = 6
    Private Const ColPartName As Short = 7
    Private Const ColModel As Short = 8
    Private Const ColSource As Short = 9
    Private Const ColStdNo As Short = 10

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAllSource_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSource.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSource.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSource.Enabled = False
            cmdSearchSource.Enabled = False
        Else
            txtSource.Enabled = True
            cmdSearchSource.Enabled = True
        End If
    End Sub

    Private Sub chkAllPartName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllPartName.CheckStateChanged
        Call PrintStatus(False)
        If chkAllPartName.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPartName.Enabled = False
            cmdSearchPartName.Enabled = False
        Else
            txtPartName.Enabled = True
            cmdSearchPartName.Enabled = True
        End If
    End Sub

    Private Sub chkAllStage_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllStage.CheckStateChanged
        Call PrintStatus(False)
        If chkAllStage.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboStage.Enabled = False
        Else
            cboStage.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnInspStdLst(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnInspStdLst(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnInspStdLst(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Inspection Standard List"
        If chkAllPartName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Part Name : " & txtPartName.Text & " ]"
        End If
        If chkAllSource.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtSource.Text) <> "" Then
            mSubTitle = mSubTitle & " [Source : " & txtSource.Text & " ]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\InspStdLst.rpt"

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

    Private Sub cmdSearchSource_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSource.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = " SELECT DISTINCT FIN_SUPP_CUST_MST.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_DET,FIN_SUPP_CUST_MST,QAL_INSPECTION_STD_HDR " & vbCrLf & " WHERE FIN_SUPP_CUST_DET.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_DET.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE " & vbCrLf & " AND FIN_SUPP_CUST_DET.COMPANY_CODE=QAL_INSPECTION_STD_HDR.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_DET.ITEM_CODE=QAL_INSPECTION_STD_HDR.ITEM_CODE " & vbCrLf & " AND FIN_SUPP_CUST_DET.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMasterBySQL2(txtSource.Text, SqlStr) = True Then
            txtSource.Text = AcName
        End If
        txtSource.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchPartName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPartName.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = " SELECT DISTINCT INV_ITEM_MST.ITEM_SHORT_DESC " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR,INV_ITEM_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE "
        If MainClass.SearchGridMasterBySQL2(txtPartName.Text, SqlStr) = True Then
            txtPartName.Text = AcName
        End If
        txtPartName.Focus()
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

    Private Sub frmParamInspStdLst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Inspection Standard List"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamInspStdLst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        Call FillCombo()

        chkAllPartName.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllPartName_CheckStateChanged(chkAllPartName, New System.EventArgs())
        chkAllSource.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllSource_CheckStateChanged(chkAllSource, New System.EventArgs())
        OptOrderBy(0).Checked = True
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamInspStdLst_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth), 11592.4, 763)
        '    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamInspStdLst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub FillCombo()
        cboStage.Items.Clear()
        cboStage.Items.Add("Receipt Inspection")
        cboStage.Items.Add("Final Inspection")
        cboStage.Items.Add("Layout Inspection")
        cboStage.Items.Add("Doc Audit Inspection")
        cboStage.Items.Add("Preventive Maintenance")
        cboStage.Items.Add("Predictive Maintenance")
        cboStage.Items.Add("Electro Plating Inspection")
        cboStage.Items.Add("Painted / Powder Coated Inspection")
        cboStage.Items.Add("Gauge / Fixture Inspection")
        cboStage.Items.Add("Initial Sample Parts")
        cboStage.SelectedIndex = 0
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim mStdNo As Double

        If SprdMain.ActiveRow <= 0 Then Exit Sub
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColStdNo
        mStdNo = Val(SprdMain.Text)
        If mStdNo <> 0 Then
            frmInspectionStnd.MdiParent = Me.MdiParent
            frmInspectionStnd.frmInspectionStnd_Activated(Nothing, New System.EventArgs())
            frmInspectionStnd.Show()
            frmInspectionStnd.txtStandardNo.Text = CStr(mStdNo)
            frmInspectionStnd.txtStandardNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub txtSource_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSource.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSource_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSource.DoubleClick
        Call cmdSearchSource_Click(cmdSearchSource, New System.EventArgs())
    End Sub

    Private Sub txtSource_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSource.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSource.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSource_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSource.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSource_Click(cmdSearchSource, New System.EventArgs())
    End Sub

    Private Sub txtSource_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSource.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtSource.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT FIN_SUPP_CUST_MST.SUPP_CUST_NAME " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_DET,FIN_SUPP_CUST_MST,QAL_INSPECTION_STD_HDR " & vbCrLf _
                    & " WHERE FIN_SUPP_CUST_DET.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf _
                    & " AND FIN_SUPP_CUST_DET.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE " & vbCrLf _
                    & " AND FIN_SUPP_CUST_DET.COMPANY_CODE=QAL_INSPECTION_STD_HDR.COMPANY_CODE " & vbCrLf _
                    & " AND FIN_SUPP_CUST_DET.ITEM_CODE=QAL_INSPECTION_STD_HDR.ITEM_CODE " & vbCrLf _
                    & " AND FIN_SUPP_CUST_DET.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND FIN_SUPP_CUST_MST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSource.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsTemp.EOF Then
            MsgBox("Not a valid Source")
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtPartName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartName.DoubleClick
        Call cmdSearchPartName_Click(cmdSearchPartName, New System.EventArgs())
    End Sub

    Private Sub txtPartName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPartName_Click(cmdSearchPartName, New System.EventArgs())
    End Sub

    Private Sub txtPartName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtPartName.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT INV_ITEM_MST.ITEM_SHORT_DESC " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR,INV_ITEM_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND INV_ITEM_MST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(txtPartName.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsTemp.EOF Then
            MsgBox("Not a valid Part Name")
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
            .MaxCols = ColStdNo
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 5)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColStage
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDocNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColDateOrig
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRevNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColRevDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColPartName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColSource
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColStdNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
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

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mStage As String

        If cboStage.Text = "Receipt Inspection" Then
            mStage = "R"
        ElseIf cboStage.Text = "Final Inspection" Then
            mStage = "F"
        ElseIf cboStage.Text = "Layout Inspection" Then
            mStage = "L"
        ElseIf cboStage.Text = "Doc Audit Inspection" Then
            mStage = "D"
        ElseIf cboStage.Text = "Preventive Maintenance" Then
            mStage = "M"
        ElseIf cboStage.Text = "Predictive Maintenance" Then
            mStage = "C"
        ElseIf cboStage.Text = "Electro Plating Inspection" Then
            mStage = "E"
        ElseIf cboStage.Text = "Painted / Powder Coated Inspection" Then
            mStage = "A"
        ElseIf cboStage.Text = "Gauge / Fixture Inspection" Then
            mStage = "G"
        ElseIf cboStage.Text = "Initial Sample Parts" Then
            mStage = "I"
        End If

        MakeSQL = " SELECT INSP_TYPE,DOC_NO,DOC_DATE,REV_NO,REV_DATE, " & vbCrLf & " QAL_INSPECTION_STD_HDR.ITEM_CODE,INV_ITEM_MST.ITEM_SHORT_DESC, " & vbCrLf & " INV_ITEM_MST.ITEM_MODEL,FIN_SUPP_CUST_MST.SUPP_CUST_NAME,AUTO_KEY_STD " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR,FIN_SUPP_CUST_MST,FIN_SUPP_CUST_DET,INV_ITEM_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_HDR.COMPANY_CODE=FIN_SUPP_CUST_DET.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE=FIN_SUPP_CUST_DET.ITEM_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_DET.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_DET.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If chkAllStage.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(cboStage.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND INSP_TYPE='" & mStage & "' "
        End If

        If chkAllPartName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartName.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND INV_ITEM_MST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(txtPartName.Text) & "' "
        End If
        If chkAllSource.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtSource.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSource.Text) & "' "
        End If

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY INSP_TYPE "

        If OptOrderBy(0).Checked = True Then 'Part No
            MakeSQL = MakeSQL & vbCrLf & ",QAL_INSPECTION_STD_HDR.ITEM_CODE"
        ElseIf OptOrderBy(1).Checked = True Then  'Part Name
            MakeSQL = MakeSQL & vbCrLf & ",INV_ITEM_MST.ITEM_SHORT_DESC"
        ElseIf OptOrderBy(2).Checked = True Then  'Source
            MakeSQL = MakeSQL & vbCrLf & ",FIN_SUPP_CUST_MST.SUPP_CUST_NAME"
        ElseIf OptOrderBy(3).Checked = True Then  'Std. No.
            MakeSQL = MakeSQL & vbCrLf & ",AUTO_KEY_STD"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAllPartName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartName.Text) = "" Then
            MsgBox("Please Select Part Name")
            txtPartName.Focus()
            Exit Function
        End If

        If chkAllSource.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtSource.Text) = "" Then
            MsgBox("Please Select Source")
            txtSource.Focus()
            Exit Function
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
End Class
