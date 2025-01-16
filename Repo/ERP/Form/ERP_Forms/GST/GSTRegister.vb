Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGSTRegister
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As Integer
    Private Const ColLocked As Short = 1
    Private Const ColSNO As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColVNo As Short = 4
    Private Const ColBillNo As Short = 5
    Private Const ColBillDate As Short = 6
    Private Const ColPartyName As Short = 7
    Private Const ColTaxableAmount As Short = 8
    Private Const ColTaxAmount As Short = 9
    Private Const ColCGSTAmount As Short = 10
    Private Const ColSGSTAmount As Short = 11
    Private Const ColIGSTAmount As Short = 12
    Private Const ColCessAmount As Short = 13
    Private Const ColBillAmount As Short = 14
    Private Const ColAccountPost As Short = 15
    Private Const ColCategory As Short = 16
    Private Const ColMKEY As Short = 17

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
            cmdSearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub

    Private Sub ChkCapital_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkCapital.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkInput_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInput.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mInvoiceTypeStr As String
        Dim mSelected As Boolean

        Report1.Reset()
        mTitle = "GST Credit Register"

        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MODVATREG.RPT"

        '    SqlStr = MakeSQLPurchase
        '
        '    SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLLCOpen
        '
        '    SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLLCDisc


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
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
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
    Private Sub frmGSTRegister_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "GST Credit Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmGSTRegister_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdSearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmGSTRegister_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmGSTRegister_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xRefundNo As Double
        Dim xMkey As String = ""
        Dim xBookType As String = ""
        Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColSNO
        xRefundNo = Val(Me.SprdMain.Text)

        SprdMain.Col = ColMKEY
        xMKey = Me.SprdMain.Text

        'Call ShowModvatTrn(xMKey, xRefundNo)

    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
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
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

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
            .set_RowHeight(0, RowHeight * 1.25)
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
            .set_ColWidth(ColSNO, 5)

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 8)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 6)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 8)
            .ColsFrozen = ColBillNo

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 22)


            For cntCol = ColTaxableAmount To ColBillAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
            Next

            .Col = ColAccountPost
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAccountPost, 22)

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCategory, 10)


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
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQLPurchase

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLService

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLSuppPurchase

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLLCOpen

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLLCDisc

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLRCClaim

        SqlStr = SqlStr & vbCrLf & " ORDER BY 2,3,4"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQLSuppPurchase() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAllInv As Boolean

        ''SELECT CLAUSE...



        MakeSQLSuppPurchase = " SELECT '', GST_CLAIM_NO, IH.GST_CLAIM_DATE, IH.VNO AS VNO,  " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(IH.TOTTAXABLEAMOUNT)) AS TOTTAXABLEAMOUNT, TO_CHAR(SUM(IH.TOTCGST_REFUNDAMT + IH.TOTSGST_REFUNDAMT + IH.TOTIGST_REFUNDAMT)) AS TOTGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(SUM(IH.TOTCGST_REFUNDAMT)) AS TOTCGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTSGST_REFUNDAMT)) As TOTSGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTIGST_REFUNDAMT)) AS TOTIGST_REFUNDAMT, " & vbCrLf & " 0 AS CESS,TO_CHAR(SUM(IH.NETVALUE)) As NETVALUE, INVTYPE.NAME, DECODE(INVTYPE.ISFIXASSETS,'N','I','C') AS CATGEORY,  IH.MKEY "


        ''FROM CLAUSE...
        MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_INVTYPE_MST INVTYPE, FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & " AND IH.COMPANY_CODE=INVTYPE.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVTYPE.CODE"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & "AND INVTYPE.ISFIXASSETS='N'"
        End If

        If ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & "AND INVTYPE.ISFIXASSETS='Y'"
        End If

        If ChkService.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & "AND 1=2"
        End If

        If chkRCM.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & "AND 1=2"
        End If

        MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & "AND CANCELLED='N' AND GST_CLAIM='Y'"


        MakeSQLSuppPurchase = MakeSQLSuppPurchase & " AND IH.GST_CLAIM_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & " GROUP BY GST_CLAIM_NO,IH.GST_CLAIM_DATE, IH.VNO,  IH.BILLNO, IH.INVOICE_DATE, SUPP_CUST_NAME,  IH.MKEY ,INVTYPE.NAME, DECODE(INVTYPE.ISFIXASSETS,'N','I','C') "
        ''ORDER CLAUSE...

        '    MakeSQLSuppPurchase = MakeSQLSuppPurchase & vbCrLf & " ORDER BY GST_CLAIM_NEW_NO,IH.GST_CLAIM_NEW_DATE,IH.VNO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLPurchase() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAllInv As Boolean

        ''SELECT CLAUSE...



        MakeSQLPurchase = " SELECT '', GST_CLAIM_NEW_NO, IH.GST_CLAIM_NEW_DATE, IH.VNO AS VNO,  " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(IH.TOTTAXABLEAMOUNT)) AS TOTTAXABLEAMOUNT, TO_CHAR(SUM(IH.TOTCGST_REFUNDAMT + IH.TOTSGST_REFUNDAMT + IH.TOTIGST_REFUNDAMT)) AS TOTGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(SUM(IH.TOTCGST_REFUNDAMT)) AS TOTCGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTSGST_REFUNDAMT)) As TOTSGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTIGST_REFUNDAMT)) AS TOTIGST_REFUNDAMT, " & vbCrLf & " 0 AS CESS,TO_CHAR(SUM(IH.NETVALUE)) As NETVALUE, INVTYPE.NAME, " & vbCrLf & " CASE WHEN IH.PURCHASE_TYPE='G' AND INVTYPE.ISFIXASSETS='N' THEN 'I' " & vbCrLf & " WHEN IH.PURCHASE_TYPE='G' AND INVTYPE.ISFIXASSETS='Y' THEN 'C' " & vbCrLf & " WHEN IH.PURCHASE_TYPE IN ('R','J') THEN 'S' END AS CATEGORY, " & vbCrLf & " IH.MKEY "


        ''FROM CLAUSE...
        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_INVTYPE_MST INVTYPE, FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.COMPANY_CODE=INVTYPE.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVTYPE.CODE"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND IH.PURCHASE_TYPE='G' AND INVTYPE.ISFIXASSETS='N'"
        End If

        If ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND IH.PURCHASE_TYPE='G' AND INVTYPE.ISFIXASSETS='Y'"
        End If

        '    If chkInput.Value = vbChecked Then
        '        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND IH.PURCHASE_TYPE='G' AND ISCAPITAL='N'"
        '    End If
        '
        '    If ChkCapital.Value = vbChecked Then
        '        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND IH.PURCHASE_TYPE='G' AND IH.ISCAPITAL='Y'"
        '    End If

        If ChkService.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND IH.PURCHASE_TYPE IN ('R','J')"
        End If

        If chkRCM.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND 1=2"
        End If

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND CANCELLED='N' AND GST_CLAIM='Y'"


        MakeSQLPurchase = MakeSQLPurchase & " AND IH.GST_CLAIM_NEW_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY GST_CLAIM_NEW_NO,IH.GST_CLAIM_NEW_DATE, IH.VNO,  IH.BILLNO, IH.INVOICE_DATE, SUPP_CUST_NAME,  IH.MKEY,INVTYPE.NAME, "
        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " CASE WHEN IH.PURCHASE_TYPE='G' AND INVTYPE.ISFIXASSETS='N' THEN 'I' WHEN IH.PURCHASE_TYPE='G' AND INVTYPE.ISFIXASSETS='Y' THEN 'C' WHEN IH.PURCHASE_TYPE IN ('R','J') THEN 'S' END"

        ''ORDER CLAUSE...

        '    MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " ORDER BY GST_CLAIM_NEW_NO,IH.GST_CLAIM_NEW_DATE,IH.VNO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLService() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAllInv As Boolean

        ''SELECT CLAUSE...



        MakeSQLService = " SELECT '', GST_CLAIM_NEW_NO, IH.GST_CLAIM_NEW_DATE, IH.VNO AS VNO,  " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(IH.TOTTAXABLEAMOUNT)) AS TOTTAXABLEAMOUNT, TO_CHAR(SUM(IH.TOTCGST_REFUNDAMT + IH.TOTSGST_REFUNDAMT + IH.TOTIGST_REFUNDAMT)) AS TOTGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(SUM(IH.TOTCGST_REFUNDAMT)) AS TOTCGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTSGST_REFUNDAMT)) As TOTSGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTIGST_REFUNDAMT)) AS TOTIGST_REFUNDAMT, " & vbCrLf & " 0 AS CESS,TO_CHAR(SUM(IH.NETVALUE)) As NETVALUE,'SERVICE' AS NAME, 'S' AS CATGEORY,  IH.MKEY "


        ''FROM CLAUSE...
        MakeSQLService = MakeSQLService & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLService = MakeSQLService & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQLService = MakeSQLService & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLService = MakeSQLService & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Or ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Or chkRCM.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLService = MakeSQLService & vbCrLf & " AND 1=2"
        End If

        If ChkService.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLService = MakeSQLService & vbCrLf & "AND IH.PURCHASE_TYPE IN ('W','S')"
        End If


        MakeSQLService = MakeSQLService & vbCrLf & "AND CANCELLED='N' AND GST_CLAIM='Y'"


        MakeSQLService = MakeSQLService & " AND IH.GST_CLAIM_NEW_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLService = MakeSQLService & vbCrLf & " GROUP BY GST_CLAIM_NEW_NO,IH.GST_CLAIM_NEW_DATE, IH.VNO,  IH.BILLNO, IH.INVOICE_DATE, SUPP_CUST_NAME,  IH.MKEY "
        ''ORDER CLAUSE...

        '    MakeSQLService = MakeSQLService & vbCrLf & " ORDER BY GST_CLAIM_NEW_NO,IH.GST_CLAIM_NEW_DATE,IH.VNO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLRCClaim() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAllInv As Boolean

        ''SELECT CLAUSE...



        MakeSQLRCClaim = " SELECT '', IH.GST_CLAIM_RC_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_RC_DATE AS GST_CLAIM_NEW_DATE, IH.BILLNO AS VNO,  " & vbCrLf & " IH.BILLNO, IH.INVOICE_DATE, SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(IH.TOTTAXABLEAMOUNT)) AS TOTTAXABLEAMOUNT, TO_CHAR(SUM(IH.TOTCGST_RC_REFUNDAMT + IH.TOTSGST_RC_REFUNDAMT + IH.TOTIGST_RC_REFUNDAMT)) AS TOTGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(SUM(IH.TOTCGST_RC_REFUNDAMT)) AS TOTCGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTSGST_RC_REFUNDAMT)) As TOTSGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTIGST_RC_REFUNDAMT)) AS TOTIGST_REFUNDAMT, " & vbCrLf & " 0 AS CESS,TO_CHAR(SUM(IH.NETVALUE)) As NETVALUE,'RCM' AS NAME, 'R' AS CATGEORY, IH.MKEY "

        ''FROM CLAUSE...
        MakeSQLRCClaim = MakeSQLRCClaim & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLRCClaim = MakeSQLRCClaim & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQLRCClaim = MakeSQLRCClaim & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLRCClaim = MakeSQLRCClaim & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Or ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Or ChkService.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLRCClaim = MakeSQLRCClaim & vbCrLf & "AND 1=2"
        End If

        MakeSQLRCClaim = MakeSQLRCClaim & vbCrLf & "AND CANCELLED='N' AND GST_RC_CLAIM   ='Y'"


        MakeSQLRCClaim = MakeSQLRCClaim & " AND IH.GST_CLAIM_RC_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLRCClaim = MakeSQLRCClaim & vbCrLf & " GROUP BY GST_CLAIM_RC_NO,IH.GST_CLAIM_RC_DATE, IH.BILLNO, IH.INVOICE_DATE, SUPP_CUST_NAME,  IH.MKEY "
        ''ORDER CLAUSE...

        '   MakeSQLRCClaim =MakeSQLRCClaim & vbCrLf & " ORDER BY GST_CLAIM_NEW_NO,IH.GST_CLAIM_NEW_DATE,IH.VNO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLLCOpen() As String

        On Error GoTo ERR1

        ''SELECT CLAUSE...



        MakeSQLLCOpen = " SELECT '', IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_DATE AS GST_CLAIM_NEW_DATE, IH.VNO AS VNO,  " & vbCrLf & " IH.REF_NO AS BILLNO, IH.REF_DATE AS INVOICE_DATE, SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(IH.ITEMVALUE)) AS TOTTAXABLEAMOUNT, TO_CHAR(SUM(IH.TOTCGST_CREDITAMT + IH.TOTSGST_CREDITAMT + IH.TOTIGST_CREDITAMT)) AS TOTGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(SUM(IH.TOTCGST_CREDITAMT)) AS TOTCGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTSGST_CREDITAMT)) AS TOTSGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTIGST_CREDITAMT)) AS TOTIGST_REFUNDAMT, " & vbCrLf & " 0 AS CESS,TO_CHAR(SUM(IH.NETVALUE)) As NETVALUE, 'SERVICE' AS NAME, 'S' AS CATGEORY, IH.MKEY "


        ''FROM CLAUSE...
        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " FROM FIN_LCOPEN_HDR IH, FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.BANK_CODE=CMST.SUPP_CUST_CODE"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Or ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Or chkRCM.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND 1=2"
        End If

        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND GST_CLAIM='Y'"


        MakeSQLLCOpen = MakeSQLLCOpen & " AND IH.GST_CLAIM_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " GROUP BY GST_CLAIM_NO,IH.GST_CLAIM_DATE, IH.VNO,  IH.REF_NO, IH.REF_DATE, SUPP_CUST_NAME,  IH.MKEY "
        ''ORDER CLAUSE...

        '    MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " ORDER BY GST_CLAIM_NEW_NO,IH.GST_CLAIM_NEW_DATE,IH.VNO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLLCDisc() As String

        On Error GoTo ERR1

        ''SELECT CLAUSE...



        MakeSQLLCDisc = " SELECT '', IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_DATE AS GST_CLAIM_NEW_DATE, IH.VNO AS VNO,  " & vbCrLf & " IH.REF_NO AS BILLNO, IH.REF_DATE AS INVOICE_DATE, SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(SUM(IH.ITEMVALUE)) AS TOTTAXABLEAMOUNT, TO_CHAR(SUM(IH.TOTCGST_CREDITAMT + IH.TOTSGST_CREDITAMT + IH.TOTIGST_CREDITAMT)) AS TOTGST_REFUNDAMT, " & vbCrLf & " TO_CHAR(SUM(IH.TOTCGST_CREDITAMT)) AS TOTCGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTSGST_CREDITAMT)) AS TOTSGST_REFUNDAMT, TO_CHAR(SUM(IH.TOTIGST_CREDITAMT)) AS TOTIGST_REFUNDAMT, " & vbCrLf & " 0 AS CESS,TO_CHAR(SUM(IH.NETVALUE)) As NETVALUE,'SERVICE' AS NAME, 'S' AS CATGEORY, IH.MKEY "


        ''FROM CLAUSE...
        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " FROM FIN_LCDISC_HDR IH, FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.BANK_CODE=CMST.SUPP_CUST_CODE"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Or ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Or chkRCM.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND 1=2"
        End If

        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND GST_CLAIM='Y'"


        MakeSQLLCDisc = MakeSQLLCDisc & " AND IH.GST_CLAIM_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " GROUP BY GST_CLAIM_NO,IH.GST_CLAIM_DATE, IH.VNO,  IH.REF_NO, IH.REF_DATE, SUPP_CUST_NAME,  IH.MKEY "
        ''ORDER CLAUSE...

        '    MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " ORDER BY GST_CLAIM_NEW_NO,IH.GST_CLAIM_NEW_DATE,IH.VNO"

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

        Dim mTaxableAmount As Double
        Dim mTaxAmount As Double
        Dim mRefundAmount As Double
        Dim mBillAmount As Double
        Dim mCessAmount As Double
        Dim mADEAmount As Double
        Dim mSHCESSAmount As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColTaxableAmount
                mTaxableAmount = mTaxableAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTaxAmount
                mTaxAmount = mTaxAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCGSTAmount
                mRefundAmount = mRefundAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSGSTAmount
                mADEAmount = mADEAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColIGSTAmount
                mCessAmount = mCessAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCessAmount
                mSHCESSAmount = mSHCESSAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColBillAmount
                mBillAmount = mBillAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColBillNo)
            .Col = ColPartyName
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows

            .Col = ColTaxableAmount
            .Text = VB6.Format(mTaxableAmount, "0.00")

            .Col = ColTaxAmount
            .Text = VB6.Format(mTaxAmount, "0.00")

            .Col = ColCGSTAmount
            .Text = VB6.Format(mRefundAmount, "0.00")

            .Col = ColSGSTAmount
            .Text = VB6.Format(mADEAmount, "0.00")

            .Col = ColIGSTAmount
            .Text = VB6.Format(mCessAmount, "0.00")

            .Col = ColCessAmount
            .Text = VB6.Format(mSHCESSAmount, "0.00")

            .Col = ColBillAmount
            .Text = VB6.Format(mBillAmount, "0.00")
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
