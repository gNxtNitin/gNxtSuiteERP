Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamReworkDetail
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 12
    ''Private PvtDBCn As ADODB.Connection

    Dim mPartyC4 As String
    Private Const ColLocked As Short = 1
    Private Const COLSBNo As Short = 2
    Private Const COLSBDate As Short = 3
    Private Const ColMTRLCode As Short = 4
    Private Const ColMtrlName As Short = 5
    Private Const ColRate As Short = 6
    Private Const ColRecd As Short = 7
    Private Const ColRectified As Short = 8
    Private Const ColScrap As Short = 9
    Private Const ColBalQty As Short = 10
    Private Const ColAmount As Short = 11
    Private Const ColRefNo As Short = 12
    Private Const ColRefDate As Short = 13
    Private Const ColMKEY As Short = 14


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtSBNo.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtSBNo.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub
    Private Sub chkPaintAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPaintAll.CheckStateChanged
        Call PrintStatus(False)
        If chkPaintAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPaint.Enabled = False
            cmdPaintSearch.Enabled = False
        Else
            txtPaint.Enabled = True
            cmdPaintSearch.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdPaintSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPaintSearch.Click
        SearchPaint()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ReportonC4(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonC4(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonC4(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String

        Exit Sub
        Report1.Reset()
        mTitle = "Rework Details Report"
        mSubTitle = "As On Date : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") 'DEEPAK AS ON DATE

        If InsertPrintDummy = False Then GoTo ReportErr

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)
        If optShow(1).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ReworkDetail.RPT"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ReworkSumm.RPT"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchC4()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        If optShow(0).Checked = True Then
            CalcSprdTotal()
        End If

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamReworkDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Rework Details Report"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamReworkDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtPaint.Enabled = False
        cmdPaintSearch.Enabled = False

        chkPaintAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtSBNo.Enabled = False
        cmdSearch.Enabled = False

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        cboShow.Items.Clear()
        cboShow.Items.Add("BOTH")
        cboShow.Items.Add("COMPLETE")
        cboShow.Items.Add("PENDING")
        cboShow.SelectedIndex = 0

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamReworkDetail_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamReworkDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub optOrderBy_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optOrderBy.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optOrderBy.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub TxtSBNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSBNo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtSBNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSBNo.DoubleClick
        SearchC4()
    End Sub
    Private Sub SearchC4()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '    MainClass.SearchGridMaster TxtSBNo, "DSP_PAINT57F4_HDR", "PARTY_F4NO", "PARTY_F4DATE", , , SqlStr
        '    If AcName <> "" Then
        '        TxtSBNo.Text = AcName
        '    End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchPaint()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtPaint.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtPaint.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtSBNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtSBNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtSBNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtSBNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtSBNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchC4()
    End Sub
    Private Sub TxtSBNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtSBNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtSBNo.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_TYPE='" & ConStockRefType_RWK & "'"

        If MainClass.ValidateWithMasterTable(TxtSBNo.Text, "AUTO_KEY_SBRWK", "SB_DATE", "PRD_REWORK_TRN", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtSBNo.Text = UCase(Trim(TxtSBNo.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Slip No.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = COLSBNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(COLSBNo, 9)
            '        If optShow(0).Value = True Then
            '            .ColHidden = False
            '        ElseIf optOrderBy(1).Value = True Then
            '            .ColHidden = True
            '        Else
            '            .ColHidden = False
            '        End If

            .Col = COLSBDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(COLSBDate, 8)
            '        If optShow(0).Value = True Then
            '            .ColHidden = False
            '        ElseIf optOrderBy(1).Value = True Then
            '            .ColHidden = True
            '        Else
            '            .ColHidden = False
            '        End If


            .Col = ColMTRLCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMTRLCode, 6)

            .Col = ColMtrlName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMtrlName, 20)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRate, 8)

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBalQty, 8)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 8)


            .Col = ColRecd
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRecd, 8)

            .Col = ColRectified
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRectified, 8)

            .Col = ColScrap
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColScrap, 8)


            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 8)
            If optShow(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 8)
            If optShow(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            '        .Row = 0
            '        .Col = ColBillQty
            '        .Text = IIf(optShow(0).Value = True, "Bill Qty", "Balance")
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""

        Show1 = False
        '    Screen.MousePointer = vbHourglass


        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        '    Screen.MousePointer = vbDefault

        Exit Function
LedgError:
        Show1 = False
        '    Screen.MousePointer = vbDefault
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mPaintName As String
        Dim mPartyCode As String

        ''SELECT CLAUSE...

        MakeSQL = " SELECT '',AUTO_KEY_SBRWK, SB_DATE,"

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " TRN.ITEM_CODE,INVMST.ITEM_SHORT_DESC, MAX(ITEM_RATE) AS ITEM_RATE, " & vbCrLf & " SUM(CASE WHEN REF_TYPE='" & ConStockRefType_RWK & "' AND ITEM_IO='I' THEN ITEM_QTY ELSE 0 END) AS IN_QTY," & vbCrLf & " SUM(CASE WHEN REF_TYPE='" & ConStockRefType_REWORK & "' AND STOCK_TYPE='WR' AND ITEM_IO='O' THEN ITEM_QTY ELSE 0 END) AS DONE_QTY," & vbCrLf & " SUM(CASE WHEN REF_TYPE='" & ConStockRefType_WRBREAKUP & "' AND STOCK_TYPE='WR' AND ITEM_IO='O' THEN ITEM_QTY ELSE 0 END) AS SCRAP_QTY," & vbCrLf & " 0 AS BALANCEQTY, " & vbCrLf & " 0 AS BALANCEAmount, " & vbCrLf & " AUTO_KEY_REF || '-' || REF_TYPE, REF_DATE, '' "
        Else
            MakeSQL = MakeSQL & vbCrLf & " TRN.ITEM_CODE,INVMST.ITEM_SHORT_DESC, MAX(ITEM_RATE) AS ITEM_RATE, " & vbCrLf & " SUM(CASE WHEN REF_TYPE='" & ConStockRefType_RWK & "' AND ITEM_IO='I' THEN ITEM_QTY ELSE 0 END) AS IN_QTY," & vbCrLf & " SUM(CASE WHEN REF_TYPE='" & ConStockRefType_REWORK & "'  AND STOCK_TYPE='WR' AND ITEM_IO='O' THEN ITEM_QTY ELSE 0 END) AS DONE_QTY," & vbCrLf & " SUM(CASE WHEN REF_TYPE='" & ConStockRefType_WRBREAKUP & "'  AND STOCK_TYPE='WR' AND ITEM_IO='O' THEN ITEM_QTY ELSE 0 END) AS SCRAP_QTY," & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) AS BALANCEQTY, " & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) * MAX(ITEM_RATE) AS BALANCEAmount, " & vbCrLf & " '' AS AUTO_KEY_REF, '' AS REF_DATE, '' "
        End If



        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM PRD_REWORK_TRN TRN, INV_ITEM_MST INVMST "

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND TRN.ITEM_CODE=INVMST.ITEM_CODE AND TRN.STOCK_TYPE<>'RS'"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtSBNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & "AND TRN.AUTO_KEY_SBRWK='" & MainClass.AllowSingleQuote(TxtSBNo.Text) & "'"
        End If

        If chkPaintAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPaint.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPaintName = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND TRN.ITEM_CODE='" & MainClass.AllowSingleQuote(mPaintName) & "'"
            End If
        End If


        If optDate(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.SB_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
            MakeSQL = MakeSQL & vbCrLf & " AND TRN.SB_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND DECODE(ITEM_IO,'O', TRN.COMPLETION_DATE,TO_DATE('" & VB6.Format(txtAsOn.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'))<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',-1,1)*ITEM_QTY)=0"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',-1,1)*ITEM_QTY)<>0"
        End If

        MakeSQL = MakeSQL & vbCrLf & "GROUP BY AUTO_KEY_SBRWK,SB_DATE,TRN.ITEM_CODE,INVMST.ITEM_SHORT_DESC "

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ,AUTO_KEY_REF || '-' || REF_TYPE, REF_DATE" ',ITEM_RATE
        End If

        ''ORDER CLAUSE...
        If optOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY AUTO_KEY_SBRWK,SB_DATE,INVMST.ITEM_SHORT_DESC"
        ElseIf optOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY CINVMST.ITEM_SHORT_DESC,AUTO_KEY_SBRWK,SB_DATE"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function InsertPrintDummy() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mLocked As String
        Dim mPartyC4No As String
        Dim mPartyC4Date As String
        Dim mMTRLCode As String
        Dim mMtrlName As String
        Dim mRecd As String
        Dim mIssued As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mItemName As String
        Dim mBalQty As String
        Dim mPartyName As String

        'Dim PvtDBCn As ADODB.Connection

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = COLSBNo
                mPartyC4No = .Text

                .Col = COLSBDate
                mPartyC4Date = .Text

                .Col = ColMTRLCode
                mMTRLCode = .Text

                .Col = ColMtrlName
                mMtrlName = Replace(.Text, "'", "''")

                .Col = ColRecd
                mRecd = .Text

                .Col = ColRectified
                mIssued = .Text

                .Col = ColBalQty
                mBalQty = .Text

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow, " & vbCrLf & " Field1,Field2,Field3,Field4,Field5," & vbCrLf & " Field6,Field7,Field8,Field9,Field10,Field11) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mPartyC4No & "', " & vbCrLf & " '" & mPartyC4Date & "', " & vbCrLf & " '" & mMTRLCode & "', " & vbCrLf & " '" & mMtrlName & "', " & vbCrLf & " '" & mRecd & "', " & vbCrLf & " '" & mIssued & "', " & vbCrLf & " '" & mBillNo & "', " & vbCrLf & " '" & mBillDate & "','" & mItemName & "','" & mBalQty & "','" & mPartyName & "') "

                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        InsertPrintDummy = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtSBNo.Text) = "" Then
                MsgInformation("Invaild SB Slip No.")
                TxtSBNo.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim StartRow As Integer
        Dim EndRow As Integer
        Dim mIssued As Double

        Dim mPreviousItemCode As String

        Dim mPartyC4 As String
        Dim mItemCode As String
        Dim mCheckCode As String


        Dim mSubRecdTotal As Double
        Dim mSubIssueTotal As Double
        Dim mSubRectifyTotal As Double
        Dim mSubScrapTotal As Double
        Dim mRate As Double
        Dim mBalAmount As Double

        Dim i As Integer

        Call MainClass.AddBlankfpSprdRow(SprdMain, COLSBNo)
        cntRow = 1
        StartRow = 1
        With SprdMain
            Do While cntRow <= .MaxRows
                .Row = cntRow

                .Col = COLSBNo


                mPartyC4 = Trim(.Text)

                .Col = ColMTRLCode
                mItemCode = Trim(.Text)

                mCheckCode = mPartyC4 & mItemCode

                mRate = 0
                mBalAmount = 0


                If mPreviousItemCode <> mCheckCode And cntRow <> 1 Then
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow


                    EndRow = cntRow
                    .Row = cntRow
                    .Col = COLSBNo
                    .Font = VB6.FontChangeBold(.Font, True)
                    .Text = "TOTAL"

                    '                Call CalcRowTotal(SprdMain, ColRecd, StartRow, ColRecd, EndRow - 1, EndRow, ColRecd)
                    '                Call CalcRowTotal(SprdMain, ColDespatch, StartRow, ColDespatch, EndRow - 1, EndRow, ColDespatch)

                    mSubRecdTotal = 0
                    mSubIssueTotal = 0
                    mSubRectifyTotal = 0
                    mSubScrapTotal = 0


                    For i = StartRow To EndRow - 1
                        .Row = i
                        .Col = ColRate
                        mRate = Val(.Text)

                        .Col = ColRecd
                        mSubRecdTotal = mSubRecdTotal + Val(IIf(IsNumeric(.Text) = True, .Text, 0))

                        .Col = ColRectified
                        mSubRectifyTotal = mSubRectifyTotal + Val(IIf(IsNumeric(.Text) = True, .Text, 0))

                        .Col = ColScrap
                        mSubScrapTotal = mSubScrapTotal + Val(IIf(IsNumeric(.Text) = True, .Text, 0))

                        '                    .Col = ColDespatch
                        '                    mSubIssueTotal = mSubIssueTotal + Val(IIf(IsNumeric(.Text) = True, .Text, 0))
                    Next

                    .Row = EndRow
                    .Col = ColRecd
                    .Text = VB6.Format(mSubRecdTotal, "0.0000")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColRectified
                    .Text = VB6.Format(mSubRectifyTotal, "0.0000")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColScrap
                    .Text = VB6.Format(mSubScrapTotal, "0.0000")
                    .Font = VB6.FontChangeBold(.Font, True)

                    '                .Col = ColDespatch
                    '                .Text = Format(mSubIssueTotal, "0.0000")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColBalQty
                    .Text = VB6.Format(mSubRecdTotal - mSubScrapTotal - mSubRectifyTotal, "0.0000")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColAmount
                    .Text = VB6.Format((mSubRecdTotal - mSubScrapTotal - mSubRectifyTotal) * mRate, "0.0000")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)
                    .BlockMode = False

                    cntRow = cntRow + 1
                    .Row = cntRow
                    StartRow = cntRow
                End If

                '            If optOrderBy(0).Value = True Or optOrderBy(2).Value = True Then
                .Col = COLSBNo
                '            Else
                '                .Col = ColPartyName
                '            End If

                mPartyC4 = Trim(.Text)

                .Col = ColMTRLCode
                mItemCode = Trim(.Text)
                mPreviousItemCode = mPartyC4 & mItemCode


                cntRow = cntRow + 1
            Loop
            .Row = .MaxRows
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtPaint_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaint.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtPaint_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaint.DoubleClick
        SearchPaint()
    End Sub


    Private Sub txtPaint_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaint.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPaint.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPaint_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPaint.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchPaint()
    End Sub


    Private Sub txtPaint_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaint.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtPaint.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable(txtPaint.Text, "ITEM_SHORT_DESC", "ITEm_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            '        txtPaint.Text = UCase(Trim(MasterNo))
        Else
            MsgInformation("Invalid Item Code.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
