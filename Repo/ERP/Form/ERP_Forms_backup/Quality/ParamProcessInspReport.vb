Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamProcessInspReport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String

    'Private PvtDBCn As ADODB.Connection

    Private Const ColDate As Short = 1
    Private Const ColInspType As Short = 2
    Private Const ColProdCode As Short = 3
    Private Const ColProdDesc As Short = 4
    Private Const ColOperation As Short = 5
    Private Const ColParameter As Short = 6
    Private Const ColSpec As Short = 7
    Private Const ColInsp As Short = 8
    Private Const ColObservation As Short = 9

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAllOpr_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllOpr.CheckStateChanged
        Call PrintStatus(False)
        If chkAllOpr.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtOperation.Enabled = False
            cmdSearchOpr.Enabled = False
        Else
            txtOperation.Enabled = True
            cmdSearchOpr.Enabled = True
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
        mTitle = "Process Inspection Report"
        If chkAllPartName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Part Name : " & txtPartName.Text & " ]"
        End If
        If chkAllOpr.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtOperation.Text) <> "" Then
            mSubTitle = mSubTitle & " [Source : " & txtOperation.Text & " ]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProcessInspReport.rpt"

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
    Private Sub cmdSearchOpr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchOpr.Click
        Dim SqlStr As String
        Dim mPartNo As String

        If Trim(txtPartName.Text) = "" Then
            MsgInformation("Please select Part No First.")
            Exit Sub
        End If

        mPartNo = ""
        If MainClass.ValidateWithMasterTable(Trim(txtPartName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartNo = Trim(MasterNo)
        End If

        SqlStr = OperationQuery(Trim(mPartNo), "", Trim(txtOperation.Text), "", Trim(txtDateAsOn.Text), "TRN.OPR_CODE", "OPR_DESC", "TRN.DEPT_CODE")
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtOperation.Text = AcName1
            '        lblOperation.text = AcName1
            If txtOperation.Enabled = True Then txtOperation.Focus()
        End If
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

    Private Sub frmParamProcessInspReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Text = "Process Inspection Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamProcessInspReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        txtDateAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkAllPartName.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllPartName_CheckStateChanged(chkAllPartName, New System.EventArgs())
        chkAllOpr.CheckState = System.Windows.Forms.CheckState.Checked
        chkAllOpr_CheckStateChanged(chkAllOpr, New System.EventArgs())
        OptOrderBy(0).Checked = True
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamProcessInspReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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

    Private Sub frmParamProcessInspReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        Dim mRefNo As Double

        If SprdMain.ActiveRow <= 0 Then Exit Sub
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColInspType
        mRefNo = Val(SprdMain.Text)
        If mRefNo <> 0 Then
            frmRecpInspection.MdiParent = Me.MdiParent
            frmRecpInspection.frmRecpInspection_Activated(Nothing, New System.EventArgs())
            frmRecpInspection.Show()
            frmRecpInspection.txtSlipNo.Text = CStr(mRefNo)
            frmRecpInspection.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub txtDateAsOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateAsOn.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDateAsOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateAsOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateAsOn.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateAsOn.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOperation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOperation.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtOperation_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOperation.DoubleClick
        Call cmdSearchOpr_Click(cmdSearchOpr, New System.EventArgs())
    End Sub

    Private Sub txtOperation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOperation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOperation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOperation_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOperation.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchOpr_Click(cmdSearchOpr, New System.EventArgs())
    End Sub

    Private Sub txtOperation_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOperation.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        Dim mPartNo As String

        mPartNo = ""
        If MainClass.ValidateWithMasterTable(Trim(txtPartName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartNo = Trim(MasterNo)
        End If

        If Trim(txtOperation.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT DISTINCT A.OPR_CODE, B.OPR_DESC " & vbCrLf _
                    & " FROM PRD_OPR_TRN A, PRD_OPR_MST B " & vbCrLf _
                    & " WHERE B.OPR_CODE = A.OPR_CODE " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.OPR_CODE)) = LTRIM(RTRIM(A.OPR_CODE)) " & vbCrLf _
                    & " AND B.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.OPR_DESC)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtOperation.Text))) & "'" & vbCrLf _
                    & " AND LTRIM(RTRIM(A.PRODUCT_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(mPartNo))) & "'" & vbCrLf _
                    & " ORDER BY B.OPR_DESC "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then
                MsgBox("Not a valid Operation.")
                Cancel = True
            End If
        End With
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
            .MaxCols = ColObservation
            .set_RowHeight(0, RowHeight * 1)
            .set_ColWidth(0, 5)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColInspType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColProdCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColProdDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColOperation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColParameter
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColSpec
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColInsp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True

            .Col = ColObservation
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

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        MakeSQL = " SELECT IH.INSP_DATE, CASE WHEN IH.INSP_SLOT='1' THEN '1st INSPECTION' ELSE '2nd INSPECTION' END AS INSP_SLOT, IH.ITEM_CODE, " & vbCrLf & " INVMST.ITEM_SHORT_DESC, IH.OPR_CODE, ID.PARAM_DESC, " & vbCrLf & " ID.SPECIFICATION, INSP_MTH, OBSERVATION" & vbCrLf & " FROM QAL_PROCESS_HDR IH, QAL_PROCESS_DET ID, INV_ITEM_MST INVMST, PRD_OPR_MST OMST" & vbCrLf & " WHERE IH.AUTO_KEY_PROCESS=ID.AUTO_KEY_PROCESS " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=OMST.COMPANY_CODE " & vbCrLf & " AND IH.OPR_CODE=OMST.OPR_CODE " ''& vbCrLf |            & " AND SUBSTR(IH.AUTO_KEY_PROCESS,LENGTH(IH.AUTO_KEY_PROCESS)-5,4)=" & RsCompany.fields("FYEAR").value & " "

        If chkAllPartName.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtPartName.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(txtPartName.Text) & "' "
        End If
        If chkAllOpr.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtOperation.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND OMST.OPR_DESC='" & MainClass.AllowSingleQuote(txtOperation.Text) & "' "
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.INSP_DATE=TO_DATE('" & VB6.Format(txtDateAsOn.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.INSP_DATE, IH.INSP_SLOT"

        If OptOrderBy(0).Checked = True Then 'Part No
            MakeSQL = MakeSQL & vbCrLf & ", ID.SERIAL_NO"
        ElseIf OptOrderBy(1).Checked = True Then  'Part Name
            MakeSQL = MakeSQL & vbCrLf & ",INVMST.ITEM_SHORT_DESC"
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

        If chkAllOpr.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtOperation.Text) = "" Then
            MsgBox("Please Select Source")
            txtOperation.Focus()
            Exit Function
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
End Class
