Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGST
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Private PvtDBCn As ADODB.Connection

    Dim mAccountCode As Integer
    Private Const ColLocked As Short = 1
    Private Const ColSNO As Short = 2
    Private Const ColSDate As Short = 3
    Private Const Col3A As Short = 4
    Private Const Col3B As Short = 5
    Private Const Col3C As Short = 6
    Private Const Col4A As Short = 7
    Private Const Col4B As Short = 8
    Private Const Col4C As Short = 9
    Private Const Col4D As Short = 10
    Private Const Col5A As Short = 11
    Private Const Col5B As Short = 12
    Private Const Col5C As Short = 13
    Private Const Col6A As Short = 14
    Private Const Col6B As Short = 15
    Private Const Col6C As Short = 16
    Private Const Col7A As Short = 17
    Private Const Col7B As Short = 18
    Private Const Col7C As Short = 19
    Private Const Col8 As Short = 20
    Private Const ColMKEY As Short = 21

    Dim mClickProcess As Boolean
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
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        cboShowType.Items.Clear()
        cboShowType.Items.Add("Other than Reverse Charge")
        cboShowType.Items.Add("Reverse Charge")
        cboShowType.SelectedIndex = 0

        Call FillInvoiceType()

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamGST_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        '    SprdMain.Row = -1
        '    SprdMain.Col = Col
        '    SprdMain.DAutoCellTypes = True
        '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        '    SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            MsgInformation("No Such Account in Account Master")
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
            .set_RowHeight(0, RowHeight * 2.5)
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


            .Col = ColSNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 6)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColSDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)

            For cntCol = Col3A To Col3C
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(.Col, 12)
            Next

            .Col = Col4A
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 16)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = Col4B
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 16)
            .ColHidden = False
            '        .ColHidden = IIf(optShow(0).Value = True, False, True)

            .Col = Col4C
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 16)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = Col4D
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 16)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            For cntCol = Col5A To Col7C
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(.Col, 12)
            Next

            .Col = Col8
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 12)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 12)
            .ColHidden = True

            Call FillHeading()

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL("N")
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL(ByRef pIsOpening As String) As String
        On Error GoTo ERR1
        Dim mCompanyGSTNo As String
        Dim mFieldName As String

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        ''SELECT CLAUSE...

        mCompanyGSTNo = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & ", " & mCompanyCode)
                End If
            Next
        End If


        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
        End If


        If pIsOpening = "Y" Then
            MakeSQL = " SELECT  VLOCK, SNO, VDATE, " & vbCrLf _
                & " OPCGST, OPSGST, OPIGST,  " & vbCrLf _
                & " VNO, REF_NO, REF_DATE, GST_RGN_NO,   " & vbCrLf _
                & " SUM(CREDITCGST) AS CREDITCGST, SUM(CREDITSGST) AS CREDITSGST, SUM(CREDITIGST) AS CREDITIGST,  " & vbCrLf _
                & " SUM(DEBITCGST) AS DEBITCGST, SUM(DEBITSGST) AS DEBITSGST, SUM(DEBITIGST) AS DEBITIGST,  " & vbCrLf _
                & " CLCGST, CLSGST, CLIGST,  " & vbCrLf _
                & " GOOD_SERVICE, MKEY " & vbCrLf _
                & " FROM ("
        Else
            If optShow(0).Checked = True Then
                MakeSQL = " SELECT  VLOCK, SNO, VDATE, " & vbCrLf _
                    & " OPCGST, OPSGST, OPIGST,  " & vbCrLf _
                    & " VNO, REF_NO, REF_DATE, GST_RGN_NO,   " & vbCrLf _
                    & " CREDITCGST, CREDITSGST, CREDITIGST,  " & vbCrLf _
                    & " DEBITCGST, DEBITSGST, DEBITIGST,  " & vbCrLf _
                    & " CLCGST, CLSGST, CLIGST,  " & vbCrLf _
                    & " GOOD_SERVICE, MKEY " & vbCrLf _
                    & " FROM ("
            Else
                MakeSQL = " SELECT  VLOCK, SNO, VDATE, " & vbCrLf _
                    & " OPCGST, OPSGST, OPIGST,  " & vbCrLf _
                    & " VNO, REF_NO, REF_DATE, GST_RGN_NO,   " & vbCrLf _
                    & " SUM(CREDITCGST) AS CREDITCGST, SUM(CREDITSGST) AS CREDITSGST, SUM(CREDITIGST) AS CREDITIGST,  " & vbCrLf _
                    & " SUM(DEBITCGST) AS DEBITCGST, SUM(DEBITSGST) AS DEBITSGST, SUM(DEBITIGST) AS DEBITIGST,  " & vbCrLf _
                    & " CLCGST, CLSGST, CLIGST,  " & vbCrLf _
                    & " GOOD_SERVICE, MKEY " & vbCrLf _
                    & " FROM ("
            End If
        End If

        If pIsOpening = "Y" Then
            MakeSQL = MakeSQL & vbCrLf _
                & " SELECT '0' AS VLOCK, " & vbCrLf _
                & " '' AS SNO, " & vbCrLf _
                & " '' AS VDATE, " & vbCrLf _
                & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf _
                & " '' AS VNO," & vbCrLf _
                & " '' AS REF_NO,  '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf _
                & " 0 as CREDITCGST," & vbCrLf _
                & " 0 as CREDITSGST," & vbCrLf _
                & " 0 as CREDITIGST," & vbCrLf _
                & " 0 as DEBITCGST," & vbCrLf _
                & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"
        Else
            If optShow(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '0' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(SYSDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO," & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " 0 as CREDITCGST," & vbCrLf & " 0 as CREDITSGST," & vbCrLf & " 0 as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"
            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT '0' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO," & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " 0 as CREDITCGST," & vbCrLf & " 0 as CREDITSGST," & vbCrLf & " 0 as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"
            End If
        End If

        MakeSQL = MakeSQL & vbCrLf & " FROM DUAL WHERE 1=2 "

        ''Invoice..

        If chkSale.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO," & vbCrLf & " '' AS REF_NO, '' AS REF_DATE,'' AS GST_RGN_NO," & vbCrLf & " 0 as CREDITCGST," & vbCrLf & " 0 as CREDITSGST," & vbCrLf & " 0 as CREDITIGST," & vbCrLf & " SUM(b.NETCGST_AMOUNT) as DEBITCGST," & vbCrLf & " SUM(b.NETSGST_AMOUNT) as DEBITSGST," & vbCrLf & " SUM(b.NETIGST_AMOUNT) as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '01' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " B.BILLNO AS VNO, " & vbCrLf & " DECODE(B.BILLNO,'-','',b.BILLNO) As REF_NO, TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " 0 as CREDITCGST," & vbCrLf & " 0 as CREDITSGST," & vbCrLf & " 0 as CREDITIGST," & vbCrLf & " b.NETCGST_AMOUNT as DEBITCGST," & vbCrLf & " b.NETSGST_AMOUNT as DEBITSGST," & vbCrLf & " b.NETIGST_AMOUNT as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " CASE WHEN INVOICESEQTYPE IN ('4','8') THEN 'SERVICE' ELSE 'GOODS' END AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '01' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(b.INVOICE_DATE,'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'SALE' AS REF_NO, '' AS REF_DATE,  '' AS GST_RGN_NO," & vbCrLf & " 0 as CREDITCGST," & vbCrLf & " 0 as CREDITSGST," & vbCrLf & " 0 as CREDITIGST," & vbCrLf & " SUM(b.NETCGST_AMOUNT) as DEBITCGST," & vbCrLf & " SUM(b.NETSGST_AMOUNT) as DEBITSGST," & vbCrLf & " SUM(b.NETIGST_AMOUNT) as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"

                End If
            End If


            MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE B.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
                & " AND B.SUPP_CUST_CODE=A.SUPP_CUST_CODE AND B.CANCELLED='N'"

            If mCompanyCodeStr <> "" Then
                ''mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If cboShowType.SelectedIndex = 0 Then
                MakeSQL = MakeSQL & vbCrLf & " AND INVOICESEQTYPE IN (0,1,2,3,4,5,6,9)"
                MakeSQL = MakeSQL & vbCrLf & " AND A.GST_RGN_NO<>'" & mCompanyGSTNo & "'"
            ElseIf cboShowType.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & " AND INVOICESEQTYPE IN (7,8)"
            End If


            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND B.INVOICE_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND B.INVOICE_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND B.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND B.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(b.INVOICE_DATE,'YYYY-MM')"
            End If
        End If

        ''Supplementary..
        If chkSaleDN.CheckState = System.Windows.Forms.CheckState.Checked And cboShowType.SelectedIndex = 0 Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'SALE RETURN' AS REF_NO, '' AS REF_DATE,'' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_AMOUNT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_AMOUNT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_AMOUNT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '02' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(b.VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " B.VNO AS VNO,  " & vbCrLf & " DECODE(B.BILLNO,'-','',b.BILLNO) AS REF_NO, TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " b.TOTCGST_AMOUNT as CREDITCGST," & vbCrLf & " b.TOTSGST_AMOUNT as CREDITSGST," & vbCrLf & " b.TOTIGST_AMOUNT as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " 'GOODS' AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '02' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(b.VDATE,'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'SALE RETURN' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_AMOUNT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_AMOUNT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_AMOUNT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"

                End If
            End If

            MakeSQL = MakeSQL & vbCrLf & " FROM FIN_PURCHASE_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE" & vbCrLf _
                & " AND B.SUPP_CUST_CODE=A.SUPP_CUST_CODE AND GST_RGN_NO<>'" & mCompanyGSTNo & "' "

            If mCompanyCodeStr <> "" Then
                ''mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            MakeSQL = MakeSQL & vbCrLf & " AND B.CANCELLED='N' AND B.PURCHASESEQTYPE=2"

            MakeSQL = MakeSQL & vbCrLf & " AND (TOTCGST_AMOUNT+TOTSGST_AMOUNT+TOTIGST_AMOUNT)>0 "

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND B.VDATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND B.VDATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND B.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND B.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(b.VDATE,'YYYY-MM')"
            End If

            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'SALE DEBIT / CREDIT NOTE' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_AMOUNT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_AMOUNT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_AMOUNT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '02' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(b.VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " VNO AS VNO, " & vbCrLf & " DECODE(B.BILLNO,'-','',b.BILLNO) AS REF_NO, TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " b.TOTCGST_AMOUNT as CREDITCGST," & vbCrLf & " b.TOTSGST_AMOUNT as CREDITSGST," & vbCrLf & " b.TOTIGST_AMOUNT as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " 'GOODS' AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '02' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(b.VDATE,'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'SALE DEBIT / CREDIT NOTE' AS REF_NO,'' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_AMOUNT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_AMOUNT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_AMOUNT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"

                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND GST_APP='Y' AND B.CANCELLED='N'" & vbCrLf _
                & " AND B.SUPP_CUST_CODE=A.SUPP_CUST_CODE AND GST_RGN_NO<>'" & mCompanyGSTNo & "' "

            If mCompanyCodeStr <> "" Then
                ''  mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND B.VDATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND B.VDATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND B.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND B.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(b.VDATE,'YYYY-MM')"
            End If

        End If

        ''Purchase ..
        If chkPurchase.CheckState = System.Windows.Forms.CheckState.Checked And cboShowType.SelectedIndex = 0 Then
            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                mFieldName = "b.GST_CLAIM_NEW_DATE"
            Else
                mFieldName = "b.VDATE"
            End If
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"
            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO,'' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_REFUNDAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_REFUNDAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_REFUNDAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '03' AS VLOCK, " & vbCrLf & " TO_CHAR(GST_CLAIM_NEW_NO) AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " VNO AS VNO,  " & vbCrLf & " DECODE(B.BILLNO,'-','',b.BILLNO) AS REF_NO, TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " b.TOTCGST_REFUNDAMT as CREDITCGST," & vbCrLf & " b.TOTSGST_REFUNDAMT as CREDITSGST," & vbCrLf & " b.TOTIGST_REFUNDAMT as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " DECODE(PURCHASE_TYPE,'G','GOODS','SERVICE') AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '03' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'PURCHASE' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_REFUNDAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_REFUNDAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_REFUNDAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"

                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_PURCHASE_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND PURCHASE_TYPE IN ('G','R','J') AND GST_CLAIM='Y'" & vbCrLf _
                & " AND B.SUPP_CUST_CODE=A.SUPP_CUST_CODE AND GST_RGN_NO<>'" & mCompanyGSTNo & "' "

            If mCompanyCodeStr <> "" Then
                ''mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            MakeSQL = MakeSQL & vbCrLf & " AND B.CANCELLED='N' AND B.PURCHASESEQTYPE<>2"

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(" & mFieldName & ",'YYYY-MM')"
            End If

            ''Purchase Service ..
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "


            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_REFUNDAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_REFUNDAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_REFUNDAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '04' AS VLOCK, " & vbCrLf & " TO_CHAR(GST_CLAIM_NEW_NO) AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " VNO AS VNO, " & vbCrLf & " DECODE(B.BILLNO,'-','',b.BILLNO) AS REF_NO, TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " b.TOTCGST_REFUNDAMT as CREDITCGST," & vbCrLf & " b.TOTSGST_REFUNDAMT as CREDITSGST," & vbCrLf & " b.TOTIGST_REFUNDAMT as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " DECODE(PURCHASE_TYPE,'G','GOODS','SERVICE') AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '04' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'PURCHASE' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_REFUNDAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_REFUNDAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_REFUNDAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"

                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_PURCHASE_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND PURCHASE_TYPE IN ('W','S') AND GST_CLAIM='Y' AND B.CANCELLED='N'" & vbCrLf _
                & " AND B.SUPP_CUST_CODE=A.SUPP_CUST_CODE AND GST_RGN_NO<>'" & mCompanyGSTNo & "' "

            If mCompanyCodeStr <> "" Then
                '' mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND " & mFieldName & "<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(" & mFieldName & ",'YYYY-MM')"
            End If

        End If

        ''Purchase Supplementary ..

        If chkPurSupp.CheckState = System.Windows.Forms.CheckState.Checked And cboShowType.SelectedIndex = 0 Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "

            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                mFieldName = "b.GST_CLAIM_DATE"
            Else
                mFieldName = "b.VDATE"
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO,'' as REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_REFUNDAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_REFUNDAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_REFUNDAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '05' AS VLOCK, " & vbCrLf & " TO_CHAR(GST_CLAIM_NO) AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " VNO AS VNO, " & vbCrLf & " DECODE(B.BILLNO,'-','',b.BILLNO) AS REF_NO, TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " b.TOTCGST_REFUNDAMT as CREDITCGST," & vbCrLf & " b.TOTSGST_REFUNDAMT as CREDITSGST," & vbCrLf & " b.TOTIGST_REFUNDAMT as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " 'GOODS' AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '05' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'PURCHASE Supplementary' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_REFUNDAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_REFUNDAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_REFUNDAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"

                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_SUPP_PURCHASE_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND GST_CLAIM='Y'" & vbCrLf _
                & " AND B.SUPP_CUST_CODE=A.SUPP_CUST_CODE AND GST_RGN_NO<>'" & mCompanyGSTNo & "' AND B.CANCELLED='N'"

            If mCompanyCodeStr <> "" Then
                '' mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND " & mFieldName & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND " & mFieldName & "<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(" & mFieldName & ",'YYYY-MM')"
            End If
        End If

        ''Debit / Credit Note ..

        If chkDNCN.CheckState = System.Windows.Forms.CheckState.Checked And cboShowType.SelectedIndex = 0 Then
            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                mFieldName = "b.PARTY_DNCN_RECDDATE"
            Else
                mFieldName = "b.VDATE"
            End If

            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO,'' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.CGST_REFUNDAMOUNT)) as CREDITCGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.SGST_REFUNDAMOUNT)) as CREDITSGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.IGST_REFUNDAMOUNT)) as CREDITIGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.CGST_REFUNDAMOUNT,0)) as DEBITCGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.SGST_REFUNDAMOUNT,0)) as DEBITSGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.IGST_REFUNDAMOUNT,0)) as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '06' AS VLOCK, " & vbCrLf & " TO_CHAR(GST_NO) AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " VNO AS VNO, " & vbCrLf & " DECODE(B.BILLNO,'-','',b.BILLNO) AS REF_NO, TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.CGST_REFUNDAMOUNT) as CREDITCGST," & vbCrLf & " DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.SGST_REFUNDAMOUNT) as CREDITSGST," & vbCrLf & " DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.IGST_REFUNDAMOUNT) as CREDITIGST," & vbCrLf & " DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.CGST_REFUNDAMOUNT,0) as DEBITCGST," & vbCrLf & " DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.SGST_REFUNDAMOUNT,0) as DEBITSGST," & vbCrLf & " DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.IGST_REFUNDAMOUNT,0) as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " 'GOODS' AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '06' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'PURCHASE DEBIT /CREDIT NOTE' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.CGST_REFUNDAMOUNT)) as CREDITCGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.SGST_REFUNDAMOUNT)) as CREDITSGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",0,b.IGST_REFUNDAMOUNT)) as CREDITIGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.CGST_REFUNDAMOUNT,0)) as DEBITCGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.SGST_REFUNDAMOUNT,0)) as DEBITSGST," & vbCrLf & " SUM(DECODE(BOOKCODE," & ConDebitNoteBookCode & ",b.IGST_REFUNDAMOUNT,0)) as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"
                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_DNCN_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND ISGSTREFUND='G' AND B.CANCELLED='N' AND B.APPROVED='Y'" & vbCrLf _
                & " AND DECODE(BOOKCODE," & ConDebitNoteBookCode & ",B.DEBITACCOUNTCODE,B.CREDITACCOUNTCODE)=A.SUPP_CUST_CODE AND GST_RGN_NO<>'" & mCompanyGSTNo & "' "

            If mCompanyCodeStr <> "" Then
                ''  mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(" & mFieldName & ",'YYYY-MM')"
            End If
        End If

        ''LC Opening..

        If chkLCOpening.CheckState = System.Windows.Forms.CheckState.Checked And cboShowType.SelectedIndex = 0 Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                mFieldName = "b.GST_CLAIM_DATE"
            Else
                mFieldName = "b.VDATE"
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_CREDITAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_CREDITAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_CREDITAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '07' AS VLOCK, " & vbCrLf & " TO_CHAR(GST_CLAIM_NO) AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " VNO AS VNO, " & vbCrLf & " DECODE(B.LC_NO,'-','',b.LC_NO) AS REF_NO,   TO_CHAR(b.LC_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " b.TOTCGST_CREDITAMT as CREDITCGST," & vbCrLf & " b.TOTSGST_CREDITAMT as CREDITSGST," & vbCrLf & " b.TOTIGST_CREDITAMT as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " 'GOODS' AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '07' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'LC OPENING' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_CREDITAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_CREDITAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_CREDITAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"
                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_LCOPEN_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND GST_CLAIM='Y'" & vbCrLf _
                & " AND B.BANK_CODE=A.SUPP_CUST_CODE AND GST_RGN_NO<>'" & mCompanyGSTNo & "' "

            If mCompanyCodeStr <> "" Then
                '' mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(" & mFieldName & ",'YYYY-MM')"
            End If
        End If

        ''LC Discounting..

        If chkLCDiscount.CheckState = System.Windows.Forms.CheckState.Checked And cboShowType.SelectedIndex = 0 Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                mFieldName = "b.GST_CLAIM_DATE"
            Else
                mFieldName = "b.VDATE"
            End If
            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_CREDITAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_CREDITAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_CREDITAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '08' AS VLOCK, " & vbCrLf & " TO_CHAR(GST_CLAIM_NO) AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " VNO AS VNO, " & vbCrLf & " DECODE(B.LC_NO,'-','',b.LC_NO) AS REF_NO, TO_CHAR(b.LC_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " b.TOTCGST_CREDITAMT as CREDITCGST," & vbCrLf & " b.TOTSGST_CREDITAMT as CREDITSGST," & vbCrLf & " b.TOTIGST_CREDITAMT as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " 'GOODS' AS GOOD_SERVICE,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '08' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'LC DISCOUNTING' AS REF_NO,'' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_CREDITAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_CREDITAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_CREDITAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE, '' AS MKEY"

                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_LCDISC_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND GST_CLAIM='Y'" & vbCrLf _
                & " AND B.BANK_CODE=A.SUPP_CUST_CODE AND GST_RGN_NO<>'" & mCompanyGSTNo & "' "

            If mCompanyCodeStr <> "" Then
                '' mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(" & mFieldName & ",'YYYY-MM')"
            End If
        End If

        ''Reverse Charge..

        If cboShowType.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "
            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                mFieldName = "b.GST_CLAIM_RC_DATE"
            Else
                mFieldName = "b.INVOICE_DATE"
            End If
            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_RC_REFUNDAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_RC_REFUNDAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_RC_REFUNDAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '09' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " VNO AS VNO, " & vbCrLf & " DECODE(B.BILLNO,'-','',b.BILLNO) AS REF_NO, TO_CHAR(b.INVOICE_DATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO," & vbCrLf & " b.TOTCGST_RC_REFUNDAMT as CREDITCGST," & vbCrLf & " b.TOTSGST_RC_REFUNDAMT as CREDITSGST," & vbCrLf & " b.TOTIGST_RC_REFUNDAMT as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " CASE WHEN INVOICESEQTYPE='8' THEN 'SERVICE' ELSE 'GOODS' END AS GOOD_SERVICE ,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '09' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(" & mFieldName & ",'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'REVERSE CHARGE CREDIT' AS REF_NO,'' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(b.TOTCGST_RC_REFUNDAMT) as CREDITCGST," & vbCrLf & " SUM(b.TOTSGST_RC_REFUNDAMT) as CREDITSGST," & vbCrLf & " SUM(b.TOTIGST_RC_REFUNDAMT) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE , '' AS MKEY"

                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_INVOICE_HDR B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND GST_RC_CLAIM='Y' AND B.CANCELLED='N'" & vbCrLf _
                & " AND B.SUPP_CUST_CODE=A.SUPP_CUST_CODE " ''AND GST_RGN_NO<>'" & mCompanyGSTNo & "'

            If mCompanyCodeStr <> "" Then
                '' mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND " & mFieldName & ">=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND " & mFieldName & "<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(" & mFieldName & ",'YYYY-MM')"
            End If
        End If

        ''Transfer Voucher..

        If chkTransfer.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO,"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '10' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(b.REFDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " TO_CHAR(B.REFNO) AS VNO, " & vbCrLf & " DECODE(B.REFNO,'-','',b.REFNO) AS REF_NO,  TO_CHAR(b.REFDATE,'DD/MM/YYYY') AS REF_DATE, GST_RGN_NO,"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '10' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(b.REFDATE,'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'GST TRANSFER VOUCHER' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO,"

                End If
            End If

            If pIsOpening = "Y" Then
                If cboShowType.SelectedIndex = 0 Then
                    MakeSQL = MakeSQL & vbCrLf & " SUM(DECODE(CGST_DC,'C',b.TOTCGST_AMOUNT,0)) as CREDITCGST," & vbCrLf & " SUM(DECODE(SGST_DC,'C',b.TOTSGST_AMOUNT,0)) as CREDITSGST," & vbCrLf & " SUM(DECODE(IGST_DC,'C',b.TOTIGST_AMOUNT,0)) as CREDITIGST," & vbCrLf & " SUM(DECODE(CGST_DC,'D',b.TOTCGST_AMOUNT,0)) as DEBITCGST," & vbCrLf & " SUM(DECODE(SGST_DC,'D',b.TOTSGST_AMOUNT,0)) as DEBITSGST," & vbCrLf & " SUM(DECODE(IGST_DC,'D',b.TOTIGST_AMOUNT,0)) as DEBITIGST,"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SUM(DECODE(RCCGST_DC,'C',b.TOTRCCGST_AMOUNT,0)) as CREDITCGST," & vbCrLf & " SUM(DECODE(RCSGST_DC,'C',b.TOTRCSGST_AMOUNT,0)) as CREDITSGST," & vbCrLf & " SUM(DECODE(RCIGST_DC,'C',b.TOTRCIGST_AMOUNT,0)) as CREDITIGST," & vbCrLf & " SUM(DECODE(RCCGST_DC,'D',b.TOTRCCGST_AMOUNT,0)) as DEBITCGST," & vbCrLf & " SUM(DECODE(RCSGST_DC,'D',b.TOTRCSGST_AMOUNT,0)) as DEBITSGST," & vbCrLf & " SUM(DECODE(RCIGST_DC,'D',b.TOTRCIGST_AMOUNT,0)) as DEBITIGST,"

                End If
            Else
                If optShow(0).Checked = True Then
                    If cboShowType.SelectedIndex = 0 Then
                        MakeSQL = MakeSQL & vbCrLf & " DECODE(CGST_DC,'C',b.TOTCGST_AMOUNT,0) as CREDITCGST," & vbCrLf & " DECODE(SGST_DC,'C',b.TOTSGST_AMOUNT,0) as CREDITSGST," & vbCrLf & " DECODE(IGST_DC,'C',b.TOTIGST_AMOUNT,0) as CREDITIGST," & vbCrLf & " DECODE(CGST_DC,'D',b.TOTCGST_AMOUNT,0) as DEBITCGST," & vbCrLf & " DECODE(SGST_DC,'D',b.TOTSGST_AMOUNT,0) as DEBITSGST," & vbCrLf & " DECODE(IGST_DC,'D',b.TOTIGST_AMOUNT,0) as DEBITIGST,"
                    Else
                        MakeSQL = MakeSQL & vbCrLf & " DECODE(RCCGST_DC,'C',b.TOTRCCGST_AMOUNT,0) as CREDITCGST," & vbCrLf & " DECODE(RCSGST_DC,'C',b.TOTRCSGST_AMOUNT,0) as CREDITSGST," & vbCrLf & " DECODE(RCIGST_DC,'C',b.TOTRCIGST_AMOUNT,0) as CREDITIGST," & vbCrLf & " DECODE(RCCGST_DC,'D',b.TOTRCCGST_AMOUNT,0) as DEBITCGST," & vbCrLf & " DECODE(RCSGST_DC,'D',b.TOTRCSGST_AMOUNT,0) as DEBITSGST," & vbCrLf & " DECODE(RCIGST_DC,'D',b.TOTRCIGST_AMOUNT,0) as DEBITIGST,"

                    End If
                Else
                    If cboShowType.SelectedIndex = 0 Then
                        MakeSQL = MakeSQL & vbCrLf & " SUM(DECODE(CGST_DC,'C',b.TOTCGST_AMOUNT,0)) as CREDITCGST," & vbCrLf & " SUM(DECODE(SGST_DC,'C',b.TOTSGST_AMOUNT,0)) as CREDITSGST," & vbCrLf & " SUM(DECODE(IGST_DC,'C',b.TOTIGST_AMOUNT,0)) as CREDITIGST," & vbCrLf & " SUM(DECODE(CGST_DC,'D',b.TOTCGST_AMOUNT,0)) as DEBITCGST," & vbCrLf & " SUM(DECODE(SGST_DC,'D',b.TOTSGST_AMOUNT,0)) as DEBITSGST," & vbCrLf & " SUM(DECODE(IGST_DC,'D',b.TOTIGST_AMOUNT,0)) as DEBITIGST,"
                    Else
                        MakeSQL = MakeSQL & vbCrLf & " SUM(DECODE(RCCGST_DC,'C',b.TOTRCCGST_AMOUNT,0)) as CREDITCGST," & vbCrLf & " SUM(DECODE(RCSGST_DC,'C',b.TOTRCSGST_AMOUNT,0)) as CREDITSGST," & vbCrLf & " SUM(DECODE(RCIGST_DC,'C',b.TOTRCIGST_AMOUNT,0)) as CREDITIGST," & vbCrLf & " SUM(DECODE(RCCGST_DC,'D',b.TOTRCCGST_AMOUNT,0)) as DEBITCGST," & vbCrLf & " SUM(DECODE(RCSGST_DC,'D',b.TOTRCSGST_AMOUNT,0)) as DEBITSGST," & vbCrLf & " SUM(DECODE(RCIGST_DC,'D',b.TOTRCIGST_AMOUNT,0)) as DEBITIGST,"

                    End If
                End If
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST, '' AS GOOD_SERVICE ,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " 'TRANSFER' AS GOOD_SERVICE ,B.Mkey AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE , '' AS MKEY"
                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_GSTTRANSFER_TRN B, FIN_SUPP_CUST_MST A" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " B.COMPANY_CODE=A.COMPANY_CODE AND CANCELLED='N' " & vbCrLf _
                & " AND B.SUPP_CUST_CODE=A.SUPP_CUST_CODE "

            If mCompanyCodeStr <> "" Then
                '' mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And B.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                MakeSQL = MakeSQL & vbCrLf & "AND ISFINALPOST='Y' AND B.CANCELLED='N'"
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND B.REFDATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND B.REFDATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND B.REFDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND B.REFDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(b.REFDATE,'YYYY-MM')"
            End If

        End If

        ''Challan..

        If chkChallan.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL "

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " '' AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " '' AS REF_NO, '' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(PAID_FROM_CGST) as CREDITCGST," & vbCrLf & " SUM(PAID_FROM_SGST) as CREDITSGST," & vbCrLf & " SUM(PAID_FROM_IGST) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE,'' AS MKEY"
            Else
                If optShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '11' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(IH.REF_DATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " DECODE(IH.CHALLANNO,'-','',IH.CHALLANNO) AS REF_NO, TO_CHAR(IH.CHALLANDATE,'DD/MM/YYYY') AS REF_DATE, '" & mCompanyGSTNo & "' AS GST_RGN_NO," & vbCrLf & " PAID_FROM_CGST as CREDITCGST," & vbCrLf & " PAID_FROM_SGST as CREDITSGST," & vbCrLf & " PAID_FROM_IGST as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " 'TRANSFER' AS GOOD_SERVICE ,TO_CHAR(IH.REF_NO) AS MKEY"
                Else
                    MakeSQL = MakeSQL & vbCrLf & " SELECT '11' AS VLOCK, " & vbCrLf & " '' AS SNO, " & vbCrLf & " TO_CHAR(IH.REF_DATE,'YYYY-MM') AS VDATE, " & vbCrLf & " '' AS OPCGST, '' AS OPSGST, '' AS OPIGST, " & vbCrLf & " '' AS VNO, " & vbCrLf & " 'CHALLAN DEPOSIT' AS REF_NO,'' AS REF_DATE, '' AS GST_RGN_NO," & vbCrLf & " SUM(PAID_FROM_CGST) as CREDITCGST," & vbCrLf & " SUM(PAID_FROM_SGST) as CREDITSGST," & vbCrLf & " SUM(PAID_FROM_IGST) as CREDITIGST," & vbCrLf & " 0 as DEBITCGST," & vbCrLf & " 0 as DEBITSGST," & vbCrLf & " 0 as DEBITIGST," & vbCrLf & " '' As CLCGST,'' As CLSGST,'' As CLIGST," & vbCrLf & " '' AS GOOD_SERVICE ,'' AS MKEY"
                End If
            End If

            MakeSQL = MakeSQL & vbCrLf _
                & " FROM FIN_GSTCHALLAN_HDR IH, FIN_GSTCHALLAN_DET ID" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR=ID.FYEAR" & vbCrLf _
                & " AND IH.REF_NO=ID.REF_NO"

            If mCompanyCodeStr <> "" Then
                '' mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " And IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
            End If

            If cboShowType.SelectedIndex = 0 Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.IS_RC='N'"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND IH.IS_RC='Y'"
            End If

            If pIsOpening = "Y" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If optShow(1).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY TO_CHAR(IH.REF_DATE,'YYYY-MM')"
            End If
        End If

        If pIsOpening = "Y" Then
            MakeSQL = MakeSQL & vbCrLf & " ) GROUP BY  VLOCK, SNO, VDATE, " & vbCrLf & " OPCGST, OPSGST, OPIGST,  " & vbCrLf & " VNO, REF_NO, REF_DATE, GST_RGN_NO,   " & vbCrLf & " CLCGST, CLSGST, CLIGST,  " & vbCrLf & " GOOD_SERVICE, MKEY "
        Else
            If optShow(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & ") ORDER BY 3,1 "
            Else
                MakeSQL = MakeSQL & vbCrLf & ") GROUP BY VDATE,VLOCK ,SNO,OPCGST, OPSGST, OPIGST,VNO, REF_NO, REF_DATE, GST_RGN_NO,CLCGST, CLSGST, CLIGST, GOOD_SERVICE, MKEY "
                MakeSQL = MakeSQL & vbCrLf & " ORDER BY VDATE,VLOCK "
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
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
        Dim mOPCGST As Double
        Dim mCGSTDr As Double
        Dim mCGSTCr As Double
        Dim mCLCGST As Double

        Dim mOPSGST As Double
        Dim mSGSTDr As Double
        Dim mSGSTCr As Double
        Dim mCLSGST As Double

        Dim mOPIGST As Double
        Dim mIGSTDr As Double
        Dim mIGSTCr As Double
        Dim mCLIGST As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        mOPCGST = GetGSTDutyOPBal("C", (txtDateFrom.Text), IIf(cboShowType.SelectedIndex = 0, "N", "Y"))
        mOPSGST = GetGSTDutyOPBal("S", (txtDateFrom.Text), IIf(cboShowType.SelectedIndex = 0, "N", "Y"))
        mOPIGST = GetGSTDutyOPBal("I", (txtDateFrom.Text), IIf(cboShowType.SelectedIndex = 0, "N", "Y"))

        SqlStr = MakeSQL("Y")
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mOPCGST = mOPCGST + IIf(IsDbNull(RsTemp.Fields("CREDITCGST").Value), 0, RsTemp.Fields("CREDITCGST").Value) - IIf(IsDbNull(RsTemp.Fields("DEBITCGST").Value), 0, RsTemp.Fields("DEBITCGST").Value)
            mOPSGST = mOPSGST + IIf(IsDbNull(RsTemp.Fields("CREDITSGST").Value), 0, RsTemp.Fields("CREDITSGST").Value) - IIf(IsDbNull(RsTemp.Fields("DEBITSGST").Value), 0, RsTemp.Fields("DEBITSGST").Value)
            mOPIGST = mOPIGST + IIf(IsDbNull(RsTemp.Fields("CREDITIGST").Value), 0, RsTemp.Fields("CREDITIGST").Value) - IIf(IsDbNull(RsTemp.Fields("DEBITIGST").Value), 0, RsTemp.Fields("DEBITIGST").Value)
        End If

        With SprdMain
            .Row = 1
            .Col = Col3A
            .Text = VB6.Format(mOPCGST, "0.00")
            .Col = Col3B
            .Text = VB6.Format(mOPSGST, "0.00")
            .Col = Col3C
            .Text = VB6.Format(mOPIGST, "0.00")

            mCLCGST = 0
            mCLSGST = 0
            mCLIGST = 0
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                If cntRow = 1 Then
                    .Col = Col3A
                    mCLCGST = mCLCGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = Col3B
                    mCLSGST = mCLSGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = Col3C
                    mCLIGST = mCLIGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                Else
                    .Col = Col3A
                    .Text = VB6.Format(mCLCGST, "0.00")

                    .Col = Col3B
                    .Text = VB6.Format(mCLSGST, "0.00")

                    .Col = Col3C
                    .Text = VB6.Format(mCLIGST, "0.00")
                End If


                .Col = Col5A
                mCLCGST = mCLCGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = Col6A
                mCLCGST = mCLCGST - Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = Col7A
                .Text = VB6.Format(mCLCGST, "0.00")



                .Col = Col5B
                mCLSGST = mCLSGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = Col6B
                mCLSGST = mCLSGST - Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = Col7B
                .Text = VB6.Format(mCLSGST, "0.00")



                .Col = Col5C
                mCLIGST = mCLIGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = Col6C
                mCLIGST = mCLIGST - Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = Col7C
                .Text = VB6.Format(mCLIGST, "0.00")

            Next

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FillHeading()

        With SprdMain
            .Row = 0

            .Col = ColLocked
            .Text = "Locked"

            .Col = ColSNO
            .Text = "Sl. No."

            .Col = ColSDate
            .Text = "Date"

            .Col = Col3A
            .Text = "Op. Bal. of CGST "

            .Col = Col3B
            .Text = "Op. Bal. of SGST"

            .Col = Col3C
            .Text = "Op. Bal. of IGST"

            .Col = Col4A
            .Text = "V No."

            .Col = Col4B
            .Text = "Ref No."

            .Col = Col4C
            .Text = "Ref Date"

            .Col = Col4D
            .Text = "GSTN No"

            .Col = Col5A
            .Text = "CREDIT CGST AMOUNT"

            .Col = Col5B
            .Text = "CREDIT SGST AMOUNT"

            .Col = Col5C
            .Text = "CREDIT IGST AMOUNT"

            .Col = Col6A
            .Text = "DEBIT CGST AMOUNT"

            .Col = Col6B
            .Text = "DEBIT SGST AMOUNT"

            .Col = Col6C
            .Text = "DEBIT IGST AMOUNT"

            .Col = Col7A
            .Text = "Cl. Bal. of CGST "

            .Col = Col7B
            .Text = "Cl. Bal. of SGST "

            .Col = Col7C
            .Text = "Cl. Bal. of IGST "

            .Col = Col8
            .Text = "Remarks"

            .Col = ColMKEY
            .Text = CStr(ColMKEY)

        End With

    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Function GetOpBED(ByRef mFieldType As String) As Double
        'On Error GoTo ErrPart
        'Dim mOpening As Double
        'Dim mSql As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        '
        '    mSql = "SELECT * FROM FIN_RG23IIOPAMT_MST " & vbCrLf _
        ''        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''        & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        '    MainClass.UOpenRecordSet mSql, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        If mFieldType = "B" Then
        '            If optType(0).Value = True Then
        '                mOpening = IIf(IsNull(RsTemp!RG23IIA), 0, RsTemp!RG23IIA)
        '            Else
        '                mOpening = IIf(IsNull(RsTemp!RG23IIC), 0, RsTemp!RG23IIC)
        '            End If
        '        ElseIf mFieldType = "A" Then
        '            If optType(0).Value = True Then
        '                mOpening = IIf(IsNull(RsTemp!ADD_DUTY), 0, RsTemp!ADD_DUTY)
        '            Else
        '                mOpening = IIf(IsNull(RsTemp!ADD_DUTY_C), 0, RsTemp!ADD_DUTY_C)
        '            End If
        '        ElseIf mFieldType = "C" Then
        '            If optType(0).Value = True Then
        '                mOpening = IIf(IsNull(RsTemp!RG23IIA_CESS), 0, RsTemp!RG23IIA_CESS)
        '            Else
        '                mOpening = IIf(IsNull(RsTemp!RG23IIC_CESS), 0, RsTemp!RG23IIC_CESS)
        '            End If
        '        ElseIf mFieldType = "H" Then
        '            If optType(0).Value = True Then
        '                mOpening = IIf(IsNull(RsTemp!RG23IIA_SHECESS), 0, RsTemp!RG23IIA_SHECESS)
        '            Else
        '                mOpening = IIf(IsNull(RsTemp!RG23IIC_SHECESS), 0, RsTemp!RG23IIC_SHECESS)
        '            End If
        '        End If
        '    Else
        '        mOpening = 0
        '    End If
        '
        '    If mFieldType = "B" Then
        '        mSql = "SELECT SUM(B.MODVATAMOUNT) AS MODVATAMOUNT "
        '    ElseIf mFieldType = "A" Then
        '        mSql = "SELECT SUM(B.ADEMODVATAMOUNT) AS MODVATAMOUNT "
        '    ElseIf mFieldType = "C" Then
        '        mSql = "SELECT SUM(B.CESSAMOUNT) AS MODVATAMOUNT "
        '    ElseIf mFieldType = "H" Then
        '        mSql = "SELECT SUM(B.SHECMODVATAMOUNT) AS MODVATAMOUNT "
        '    End If
        '    ''FROM CLAUSE...
        '    mSql = mSql & vbCrLf & " FROM FIN_SUPP_CUST_MST A, FIN_PURCHASE_HDR B"
        '
        '    '& " AND B.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf
        '
        '    ''WHERE CLAUSE...
        '    mSql = mSql & vbCrLf & " WHERE " & vbCrLf _
        ''            & " B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND B.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
        ''            & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
        ''            & " AND ISMODVAT='Y'"
        '
        '    mSql = mSql & vbCrLf _
        ''            & " AND B.MODVATDATE>=TO_DATE('" & VB6.Format(RsCompany!Start_Date, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " AND B.MODVATDATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')"
        '
        '    If chkAll.Value = vbUnchecked Then
        '        mSql = mSql & vbCrLf & "AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        '    End If
        '
        '    If optType(0).Value = True Then
        '         mSql = mSql & vbCrLf & "AND B.ISCAPITAL='N'"
        '    Else
        '         mSql = mSql & vbCrLf & "AND B.ISCAPITAL='Y'"
        '    End If
        '
        '    mSql = mSql & vbCrLf & "AND B.ISPLA='N'"
        '
        '    MainClass.UOpenRecordSet mSql, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        mOpening = mOpening + IIf(IsNull(RsTemp!MODVATAMOUNT), 0, RsTemp!MODVATAMOUNT)
        '    End If
        '    GetOpBED = mOpening
        Exit Function
ErrPart:
        GetOpBED = 0
    End Function
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        PubDBCn.Errors.Clear()


        'If TxtName.Text = "" Then Exit Sub

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertPrintDummy()


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "FORM GST Debit / Credit Register"
        mSubTitle = "ENTRY BOOK OF DUTY CREDIT"

        mRPTName = "ParamGST.Rpt"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        ''Resume
    End Sub
    Private Sub InsertPrintDummy()

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mColLocked As String
        'Dim mColSNO As String
        'Dim mColSDate As String
        'Dim mCol3A As String
        'Dim mCol3B As String
        'Dim mCol3C As String
        'Dim mCol4A As String
        'Dim mCol4B As String
        'Dim mCol5 As String
        'Dim mCol6A As String
        'Dim mCol6B As String
        'Dim mCol6C As String
        'Dim mCol6D As String
        'Dim mCol7A As String
        'Dim mCol7B As String
        'Dim mCol8A As String
        'Dim mCol8B As String
        'Dim mCol8C As String
        'Dim mCol9A As String
        'Dim mCol9B As String
        'Dim mCol9C As String
        'Dim mCol10 As String
        '
        'Dim mCol3AA As String
        'Dim mCol8AA As String
        'Dim mCol9AA As String
        '
        '    PubDBCn.Errors.Clear
        '
        '    PubDBCn.BeginTrans
        '
        '    SqlStr = ""
        '    With SprdMain
        '        For cntRow = 1 To .MaxRows - 1
        '            .Row = cntRow
        '
        '            .Col = ColSNO
        '            mColSNO = Trim(.Text)
        '            .Col = ColSDate
        '            mColSDate = Trim(.Text)
        '            .Col = Col3A
        '            mCol3A = Trim(.Text)
        '            .Col = Col3B
        '            mCol3B = Trim(.Text)
        '            .Col = Col3C
        '            mCol3C = Trim(.Text)
        '
        '            .Col = Col4A
        '            mCol4A = Trim(.Text)
        '            .Col = Col4B
        '            mCol4B = MainClass.AllowSingleQuote(Trim(.Text))
        '            .Col = Col5
        '            mCol5 = MainClass.AllowSingleQuote(Trim(.Text))
        '
        '            .Col = Col6A
        '            mCol6A = Trim(.Text)
        '            .Col = Col6B
        '            mCol6B = Trim(.Text)
        '            .Col = Col6C
        '            mCol6C = Trim(.Text)
        '            .Col = Col6D
        '            mCol6D = Trim(.Text)
        '
        '            .Col = Col7A
        '            mCol7A = Trim(.Text)
        '            .Col = Col7B
        '            mCol7B = Trim(.Text)
        '            .Col = Col8A
        '            mCol8A = Trim(.Text)
        '            .Col = Col8B
        '            mCol8B = Trim(.Text)
        '            .Col = Col8C
        '            mCol8C = Trim(.Text)
        '            .Col = Col9A
        '            mCol9A = Trim(.Text)
        '            .Col = Col9B
        '            mCol9B = Trim(.Text)
        '            .Col = Col9C
        '            mCol9C = Trim(.Text)
        '            .Col = Col10
        '            mCol10 = Trim(.Text)
        '
        '             .Col = Col3AA
        '            mCol3AA = Trim(.Text)
        '
        '             .Col = Col8AA
        '            mCol8AA = Trim(.Text)
        '
        '             .Col = Col9AA
        '            mCol9AA = Trim(.Text)
        '
        '
        '            SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow," & vbCrLf _
        ''                & " Field1,Field2,Field3,Field4,Field5, " & vbCrLf _
        ''                & " Field6,Field7,Field8,Field9,Field10 ," & vbCrLf _
        ''                & " Field11,Field12,Field13,Field14,Field15, " & vbCrLf _
        ''                & " Field16,Field17,Field18,Field19,Field20, " & vbCrLf _
        ''                & " Field21,Field22,Field23,Field24 " & vbCrLf _
        ''                & " ) Values ("
        '
        '            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''                & " " & cntRow & ", " & vbCrLf _
        ''                & " '" & mColSNO & "', " & vbCrLf _
        ''                & " '" & mColSDate & "', " & vbCrLf _
        ''                & " '" & mCol3A & "', " & vbCrLf _
        ''                & " '" & mCol3B & "', " & vbCrLf _
        ''                & " '" & mCol3C & "', " & vbCrLf _
        ''                & " '" & mCol4A & "', " & vbCrLf _
        ''                & " '" & mCol4B & "', " & vbCrLf _
        ''                & " '" & mCol5 & "', " & vbCrLf _
        ''                & " '" & mCol6A & "', " & vbCrLf _
        ''                & " '" & mCol6B & "', " & vbCrLf _
        ''                & " '" & mCol6C & "', " & vbCrLf _
        ''                & " '" & mCol6D & "', " & vbCrLf _
        ''                & " '" & mCol7A & "', " & vbCrLf _
        ''                & " '" & mCol7B & "', " & vbCrLf _
        ''                & " '" & mCol8A & "', " & vbCrLf _
        ''                & " '" & mCol8B & "', " & vbCrLf _
        ''                & " '" & mCol8C & "', " & vbCrLf _
        ''                & " '" & mCol9A & "', " & vbCrLf _
        ''                & " '" & mCol9B & "', " & vbCrLf _
        ''                & " '" & mCol9C & "', " & vbCrLf _
        ''                & " '" & MainClass.AllowSingleQuote(mCol10) & "','" & mCol3AA & "','" & mCol8AA & "','" & mCol9AA & "') "
        '
        '            PubDBCn.Execute SqlStr
        '        Next
        '
        '    End With
        '    PubDBCn.CommitTrans
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mMonth As String
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, , , "Y")

        If VB6.Format(txtDateTo.Text, "MMMM, YYYY") = VB6.Format(txtDateFrom.Text, "MMMM, YYYY") Then
            mMonth = "Month : " & VB6.Format(txtDateTo.Text, "MMMM, YYYY")
        Else
            mMonth = "Month : FROM " & VB6.Format(txtDateFrom.Text, "MMMM, YYYY") & " To " & VB6.Format(txtDateTo.Text, "MMMM, YYYY")
        End If

        MainClass.AssignCRptFormulas(Report1, "MonthTitle=""" & mMonth & """")

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_NAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_NAME").Value = RsCompany.Fields("COMPANY_NAME").Value, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
End Class
