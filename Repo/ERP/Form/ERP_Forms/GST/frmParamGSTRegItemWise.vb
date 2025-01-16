Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGSTRegItemWise
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Private PvtDBCn As ADODB.Connection

    Dim mAccountCode As Integer

    Private Const ColLocked As Short = 1
    Private Const ColGSTNo As Short = 2
    Private Const ColGSTDate As Short = 3
    Private Const ColVNo As Short = 4
    Private Const colSupplier As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemDesc As Short = 7
    Private Const ColHSNCode As Short = 8
    Private Const ColUOM As Short = 9
    Private Const ColQty As Short = 10
    Private Const ColItemAmount As Short = 11
    Private Const CGSTAmount As Short = 12
    Private Const SGSTAmount As Short = 13
    Private Const IGSTAmount As Short = 14
    Private Const ColMKEY As Short = 15


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cboSupplierType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSupplierType.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboSupplierType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSupplierType.SelectedIndexChanged
        Call PrintStatus(False)
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
    Private Sub frmParamGSTRegItemWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTRegItemWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False



        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        cboSupplierType.Items.Clear()
        cboSupplierType.Items.Add("ALL")
        cboSupplierType.Items.Add("INTER STATE")
        cboSupplierType.Items.Add("INTRA STATE")
        '    cboSupplierType.AddItem "Ist & IInd Stage Dealer/TRADERS/Others"
        cboSupplierType.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamGSTRegItemWise_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11410.1, 751)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGSTRegItemWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

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

            .Col = ColGSTNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColGSTNo, 6)
            .ColHidden = False

            .Col = ColGSTDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .set_ColWidth(ColGSTDate, 8)

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColHSNCode, 8)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 6)
            .ColHidden = False

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, IIf(optShow(1).Checked = True, 35, 15))
            .ColsFrozen = colSupplier


            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 9)
            .ColHidden = IIf(optShow(1).Checked = True, True, False)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 20)
            .ColHidden = IIf(optShow(1).Checked = True, True, False)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUOM, 5)

            For cntCol = ColQty To IGSTAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
            Next

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
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQLPurchase

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLLCOpen
        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLLCDisc

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & MakeSQLRCM

        SqlStr = SqlStr & vbCrLf & "ORDER BY 2,3,4"


        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQLPurchase() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mAccountCode As String
        Dim mCatCode As String = ""

        ''SELECT CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQLPurchase = " SELECT '', IH.GST_CLAIM_NEW_NO, IH.GST_CLAIM_NEW_DATE, IH.VNO AS VNO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " DECODE(ID.ITEM_CODE,'-1','',ID.ITEM_CODE) AS ITEM_CODE, ID.ITEM_DESC, ID.HSNCODE, ID.ITEM_UOM, " & vbCrLf & " TO_CHAR(ID.ITEM_QTY) AS ITEM_QTY, ID.GSTABLE_AMT, ID.CGST_AMOUNT , ID.SGST_AMOUNT , ID.IGST_AMOUNT "
        Else
            MakeSQLPurchase = " SELECT '',  IH.GST_CLAIM_NEW_NO, IH.GST_CLAIM_NEW_DATE, IH.VNO AS VNO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " '', '', '', '', " & vbCrLf & " TO_CHAR(SUM(ID.ITEM_QTY)) AS ITEM_QTY, TO_CHAR(SUM(ID.GSTABLE_AMT)) AS GSTABLE_AMT, " & vbCrLf & " TO_CHAR(SUM(ID.CGST_AMOUNT)) As CGST_AMOUNT , TO_CHAR(SUM(ID.SGST_AMOUNT)) AS SGST_AMOUNT, TO_CHAR(SUM(ID.IGST_AMOUNT)) AS IGST_AMOUNT"
        End If
        ''FROM CLAUSE...
        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_INVTYPE_MST INVTYPE, FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf
        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.COMPANY_CODE=INVTYPE.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVTYPE.CODE"

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND CANCELLED='N' AND GST_CLAIM='Y'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND (IH.PURCHASE_TYPE='G' OR ID.GOODS_SERVICE='G') AND INVTYPE.ISFIXASSETS='N'" ''AND ISCAPITAL='N'"
        End If

        If ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND (IH.PURCHASE_TYPE='G' OR ID.GOODS_SERVICE='G') AND INVTYPE.ISFIXASSETS='Y'" ''AND ISCAPITAL='Y'"
        End If

        If ChkService.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND (IH.PURCHASE_TYPE IN ('R','J') OR (ID.GOODS_SERVICE='S' AND IH.PURCHASE_TYPE IN ('W','S')))"
        End If

        If chkRCM.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND 1=2"
        End If

        If Trim(txtTariffHeading.Text) <> "" Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "AND ID.HSNCODE='" & MainClass.AllowSingleQuote(Trim(txtTariffHeading.Text)) & "'"
        End If

        MakeSQLPurchase = MakeSQLPurchase & " AND IH.GST_CLAIM_NEW_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If optShow(1).Checked = True Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & "GROUP BY IH.GST_CLAIM_NEW_NO,CMST.SUPP_CUST_NAME, IH.VNO,IH.GST_CLAIM_NEW_DATE"
        End If

        'ORDER CLAUSE...



        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLLCOpen() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mAccountCode As String
        Dim mCatCode As String = ""

        ''SELECT CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQLLCOpen = " SELECT '', IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_DATE AS GST_CLAIM_NEW_DATE, IH.VNO AS VNO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " '' AS ITEM_CODE, '' AS ITEM_DESC, ID.HSN_CODE AS HSNCODE, '' AS ITEM_UOM, " & vbCrLf & " 'O' AS ITEM_QTY, ID.AMOUNT AS GSTABLE_AMT, ID.CGST_AMOUNT , ID.SGST_AMOUNT , ID.IGST_AMOUNT "
        Else
            MakeSQLLCOpen = " SELECT '',  IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_DATE AS GST_CLAIM_NEW_DATE, IH.VNO AS VNO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " '', '', '', '', " & vbCrLf & " '0' AS ITEM_QTY, TO_CHAR(SUM(ID.AMOUNT)) AS GSTABLE_AMT, " & vbCrLf & " TO_CHAR(SUM(ID.CGST_AMOUNT)) As CGST_AMOUNT , TO_CHAR(SUM(ID.SGST_AMOUNT)) AS SGST_AMOUNT, TO_CHAR(SUM(ID.IGST_AMOUNT)) AS IGST_AMOUNT"
        End If


        ''FROM CLAUSE...
        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " FROM FIN_LCOPEN_HDR IH, FIN_LCOPEN_DET ID,  FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf
        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.BANK_CODE=CMST.SUPP_CUST_CODE"

        MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND GST_CLAIM='Y'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If


        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Or ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Or chkRCM.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND 1=2"
        End If

        If Trim(txtTariffHeading.Text) <> "" Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "AND ID.HSN_CODE='" & MainClass.AllowSingleQuote(Trim(txtTariffHeading.Text)) & "'"
        End If

        MakeSQLLCOpen = MakeSQLLCOpen & " AND IH.GST_CLAIM_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If optShow(1).Checked = True Then
            MakeSQLLCOpen = MakeSQLLCOpen & vbCrLf & "GROUP BY IH.GST_CLAIM_DATE,CMST.SUPP_CUST_NAME, IH.VNO,IH.GST_CLAIM_NO"
        End If

        'ORDER CLAUSE...



        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLLCDisc() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mAccountCode As String
        Dim mCatCode As String = ""

        ''SELECT CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQLLCDisc = " SELECT '', IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_DATE AS GST_CLAIM_NEW_DATE, IH.VNO AS VNO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " '' AS ITEM_CODE, '' AS ITEM_DESC, ID.HSN_CODE AS HSNCODE, '' AS ITEM_UOM, " & vbCrLf & " 'O' AS ITEM_QTY, ID.AMOUNT AS GSTABLE_AMT, ID.CGST_AMOUNT , ID.SGST_AMOUNT , ID.IGST_AMOUNT "
        Else
            MakeSQLLCDisc = " SELECT '',  IH.GST_CLAIM_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_DATE AS GST_CLAIM_NEW_DATE, IH.VNO AS VNO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " '', '', '', '', " & vbCrLf & " '0' AS ITEM_QTY, TO_CHAR(SUM(ID.AMOUNT)) AS GSTABLE_AMT, " & vbCrLf & " TO_CHAR(SUM(ID.CGST_AMOUNT)) As CGST_AMOUNT , TO_CHAR(SUM(ID.SGST_AMOUNT)) AS SGST_AMOUNT, TO_CHAR(SUM(ID.IGST_AMOUNT)) AS IGST_AMOUNT"
        End If


        ''FROM CLAUSE...
        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " FROM FIN_LCDISC_HDR IH, FIN_LCDISC_DET ID,  FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf
        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.BANK_CODE=CMST.SUPP_CUST_CODE"

        MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND GST_CLAIM='Y'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If


        If chkInput.CheckState = System.Windows.Forms.CheckState.Checked Or ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Or chkRCM.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND 1=2"
        End If

        If Trim(txtTariffHeading.Text) <> "" Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "AND ID.HSN_CODE='" & MainClass.AllowSingleQuote(Trim(txtTariffHeading.Text)) & "'"
        End If

        MakeSQLLCDisc = MakeSQLLCDisc & " AND IH.GST_CLAIM_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If optShow(1).Checked = True Then
            MakeSQLLCDisc = MakeSQLLCDisc & vbCrLf & "GROUP BY IH.GST_CLAIM_DATE,CMST.SUPP_CUST_NAME, IH.VNO,IH.GST_CLAIM_NO"
        End If

        'ORDER CLAUSE...



        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLRCM() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mAccountCode As String
        Dim mCatCode As String = ""

        ''SELECT CLAUSE...

        '
        '
        '

        If optShow(0).Checked = True Then
            MakeSQLRCM = " SELECT '', IH.GST_CLAIM_RC_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_RC_DATE AS GST_CLAIM_NEW_DATE, IH.BILLNO AS VNO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " DECODE(ID.ITEM_CODE,'-1','',ID.ITEM_CODE), ID.ITEM_DESC, ID.HSNCODE, ID.ITEM_UOM, " & vbCrLf & " TO_CHAR(ID.ITEM_QTY) AS ITEM_QTY, ID.GSTABLE_AMT, ID.CGST_AMOUNT , ID.SGST_AMOUNT , ID.IGST_AMOUNT "
        Else
            MakeSQLRCM = " SELECT '', IH.GST_CLAIM_RC_NO AS GST_CLAIM_NEW_NO, IH.GST_CLAIM_RC_DATE AS GST_CLAIM_NEW_DATE, IH.BILLNO AS VNO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " '', '', '', '', " & vbCrLf & " TO_CHAR(SUM(ID.ITEM_QTY)) AS ITEM_QTY, TO_CHAR(SUM(ID.GSTABLE_AMT)), TO_CHAR(SUM(ID.CGST_AMOUNT)) , TO_CHAR(SUM(ID.SGST_AMOUNT)) , TO_CHAR(SUM(ID.IGST_AMOUNT)) "
        End If
        ''FROM CLAUSE...
        MakeSQLRCM = MakeSQLRCM & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID,  FIN_SUPP_CUST_MST CMST"


        ''WHERE CLAUSE...
        MakeSQLRCM = MakeSQLRCM & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf
        MakeSQLRCM = MakeSQLRCM & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        MakeSQLRCM = MakeSQLRCM & vbCrLf & "AND CANCELLED='N' AND GST_RC_CLAIM='Y'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLRCM = MakeSQLRCM & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If


        If ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked Or chkInput.CheckState = System.Windows.Forms.CheckState.Checked Or ChkService.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLRCM = MakeSQLRCM & vbCrLf & "AND 1=2"
        End If

        If Trim(txtTariffHeading.Text) <> "" Then
            MakeSQLRCM = MakeSQLRCM & vbCrLf & "AND ID.HSNCODE='" & MainClass.AllowSingleQuote(Trim(txtTariffHeading.Text)) & "'"
        End If

        MakeSQLRCM = MakeSQLRCM & " AND IH.GST_CLAIM_RC_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If optShow(1).Checked = True Then
            MakeSQLRCM = MakeSQLRCM & vbCrLf & "GROUP BY IH.GST_CLAIM_RC_NO,CMST.SUPP_CUST_NAME, IH.BILLNO,IH.GST_CLAIM_RC_DATE"
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

        Dim mSno As String
        Dim mQty As Double
        Dim mItemAmount As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow


                .Col = ColGSTNo
                mSno = Trim(.Text)

                .Col = ColQty
                mQty = mQty + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColItemAmount
                mItemAmount = mItemAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = CGSTAmount
                mCGSTAmount = mCGSTAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = SGSTAmount
                mSGSTAmount = mSGSTAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = IGSTAmount
                mIGSTAmount = mIGSTAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, IIf(optShow(1).Checked = True, colSupplier, ColGSTNo))

            .Col = colSupplier
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

            .Col = ColQty
            .Text = VB6.Format(mQty, "0.00")

            .Col = ColItemAmount
            .Text = VB6.Format(mItemAmount, "0.00")

            .Col = CGSTAmount
            .Text = VB6.Format(mCGSTAmount, "0.00")

            .Col = SGSTAmount
            .Text = VB6.Format(mSGSTAmount, "0.00")

            .Col = IGSTAmount
            .Text = VB6.Format(mIGSTAmount, "0.00")

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        PubDBCn.Errors.Clear()

        SqlStr = ""

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "")


        mTitle = "GST Credit Register (Item Wise)"

        '    If optType(0).Value = True Then
        '        mSubTitle = "INPUT - "
        '    Else
        '        mSubTitle = "CAPITAL - "
        '    End If

        mSubTitle = mSubTitle & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        mRPTName = IIf(optShow(0).Checked = True, "ModvatRegItemWise.Rpt", "ModvatRegItemWiseSumm.Rpt")

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

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mShowAll As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        '    mShowAll = IIf(chkList.Value = vbChecked, "Y", "N")

        MainClass.AssignCRptFormulas(Report1, "ShowAll=""" & mShowAll & """")

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub txtTariffHeading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariffHeading.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtTariffHeading_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariffHeading.DoubleClick
        SearchTariff()
    End Sub

    Private Sub txtTariffHeading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTariffHeading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTariffHeading_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTariffHeading.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    End Sub

    Private Sub txtTariffHeading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTariffHeading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtTariffHeading.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtTariffHeading.Text), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Tariff.", "", MsgBoxStyle.Critical)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchTariff()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtTariffHeading.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr) = True Then
            txtTariffHeading.Text = AcName
            '        txtTariff_Validate False
            If txtTariffHeading.Enabled = True Then txtTariffHeading.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
