Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmParamIWPurchaseRegGST
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColVNo As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColBillNo As Short = 4
    Private Const ColBillDate As Short = 5
    Private Const ColPartyName As Short = 6
    Private Const ColPartyGSTNo As Short = 7
    Private Const ColDebiName As Short = 8
    Private Const ColItemCode As Short = 9
    Private Const ColItemDesc As Short = 10
    Private Const ColItemUOM As Short = 11
    Private Const ColItemQty As Short = 12
    Private Const ColItemRate As Short = 13
    Private Const ColItemAmount As Short = 14
    Private Const ColCGSTAmount As Short = 15
    Private Const ColSGSTAmount As Short = 16
    Private Const ColIGSTAmount As Short = 17
    Private Const ColOtherAmount As Short = 18
    Private Const ColNetAmount As Short = 19

    Private Const ColCancelled As Short = 20
    Private Const ColMKEY As Short = 21

    'Dim ColMKEY As Integer
    'Dim ColCancelled As Integer

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mClickProcess As Boolean = False
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboAgtD3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboAgtD3_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCancelled_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCancelled_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCountry_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCountry.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboFOC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboFOC_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboGST_Click()
        Call PrintStatus(False)
    End Sub

    Private Sub cboRC_Change()
        Call PrintStatus(False)
    End Sub

    Private Sub cboGS_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGS.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboPurchaseType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurchaseType.TextChanged
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

    Private Sub cboGST_Change()
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call SaleReport("V")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call SaleReport("P")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub

        CreateGridHeader()
        If Show1("") = False Then GoTo ErrPart

        'Me.UltraGrid1.DisplayLayout.Override.FixedRowIndicator = FixedRowIndicator.None
        'Me.UltraGrid1.Rows(0).Fixed = True

        'Me.UltraGrid1.Rows(UltraGrid1.Rows.Count - 1).Fixed = True

        UltraGrid1.Focus()
        Call PrintStatus(True)

        'FillHeading()
        ''MainClass.ClearGrid(SprdMain, RowHeight)

        'If Show1 = False Then GoTo ErrPart
        'Call PrintStatus(True)
        'CalcSprdTotal()
        '''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        ''    FormatSprdMain -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamIWPurchaseRegGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Purchase Register (Item Wise)"

        If Show1("S") = False Then GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamIWPurchaseRegGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim Sqlstr As String
        Dim RS As ADODB.Recordset

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

        lblTrnType.Text = CStr(-1)
        cboAgtD3.Items.Clear()
        cboCancelled.Items.Clear()
        cboFOC.Items.Clear()
        cboGSTStatus.Items.Clear()
        cboGS.Items.Clear()
        cboPurchaseType.Items.Clear()

        cboAgtD3.Items.Add("BOTH")
        cboAgtD3.Items.Add("YES")
        cboAgtD3.Items.Add("NO")

        cboCancelled.Items.Add("BOTH")
        cboCancelled.Items.Add("YES")
        cboCancelled.Items.Add("NO")

        cboFOC.Items.Add("BOTH")
        cboFOC.Items.Add("YES")
        cboFOC.Items.Add("NO")

        cboGSTStatus.Items.Add("All")
        cboGSTStatus.Items.Add("GST Refund")
        cboGSTStatus.Items.Add("Reverse Charge")
        cboGSTStatus.Items.Add("Exempt")
        cboGSTStatus.Items.Add("Non-GST")
        cboGSTStatus.Items.Add("Ineligible")
        cboGSTStatus.Items.Add("1. GST Refund (W/o Reverse Charge)")
        cboGSTStatus.Items.Add("2. GST Refund (With Reverse Charge)")

        cboGS.Items.Add("BOTH")
        cboGS.Items.Add("GOODS")
        cboGS.Items.Add("SERVICES")

        cboCountry.Items.Add("ALL")
        cboCountry.Items.Add("WITHIN COUNTRY")
        cboCountry.Items.Add("OUTSIDE COUNTRY")

        cboPurchaseType.Items.Add("ALL")
        cboPurchaseType.Items.Add("1. GST Goods Order)")
        cboPurchaseType.Items.Add("2. Sale Return Agt Debit Note")
        cboPurchaseType.Items.Add("3. GST Ship)")
        cboPurchaseType.Items.Add("4. GST-Jobwork Order")
        cboPurchaseType.Items.Add("5. GST-Repair")
        cboPurchaseType.Items.Add("6. GST Work Order")
        cboPurchaseType.Items.Add("7. Contract / Service / Other Bill Entry - GST")
        cboPurchaseType.Items.Add("8. Sale Return Agt Invoice")

        cboAgtD3.SelectedIndex = 0
        cboCancelled.SelectedIndex = 0
        cboFOC.SelectedIndex = 0
        cboGSTStatus.SelectedIndex = 0
        cboGS.SelectedIndex = 0
        cboCountry.SelectedIndex = 0
        cboPurchaseType.SelectedIndex = 0

        cboDivision.Items.Clear()

        Sqlstr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        Call FillInvoiceType()
        'FillHeading()

        optType(2).Checked = True
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtMRRDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtMRRDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        Call frmParamIWPurchaseRegGST_Activated(eventSender, eventArgs)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamIWPurchaseRegGST_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamIWPurchaseRegGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String

        Dim xCompanyCode As Long

        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub

        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)

        xVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1))
        xMKey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))
        xVNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1))

        Call ShowTrn(xMKey, xVDate, "", xVNo, "P", "", Me)
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
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim Sqlstr As String

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , Sqlstr)
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
        Dim Sqlstr As String

        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , Sqlstr) = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    '    Private Sub FormatSprdMain(ByRef Arow As Integer)

    '        Dim cntCol As Integer
    '        With SprdMain
    '            .MaxCols = ColMKEY
    '            .set_RowHeight(0, RowHeight * 1.25)
    '            .set_ColWidth(0, 4.5)

    '            .set_RowHeight(-1, RowHeight)
    '            .Row = -1

    '            .Col = ColLocked
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColLocked, 10)
    '            .ColHidden = False

    '            .Col = ColVDate
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColVDate, 9)

    '            .Col = ColVNo
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColVNo, 9)

    '            .Col = ColBillDate
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColBillDate, 9)

    '            .Col = ColBillNo
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColBillNo, 9)


    '            '        If OptSumDet(0).Value = True Then
    '            '            .ColHidden = False
    '            '        Else
    '            '            .ColHidden = True
    '            '        End If

    '            .Col = ColPartyName
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColPartyName, 15)
    '            '        If OptSumDet(0).Value = True Then
    '            '            .ColHidden = False
    '            '            .ColsFrozen = ColAcctName
    '            '        Else
    '            '            .ColHidden = True
    '            '        End If
    '            .ColsFrozen = ColPartyName

    '            .Col = ColPartyGSTNo
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColPartyGSTNo, 10)
    '            For cntCol = ColItemQty To ColMKEY - 2
    '                .Col = cntCol
    '                .CellType = SS_CELL_TYPE_FLOAT
    '                .TypeFloatDecimalPlaces = 2
    '                .TypeFloatMin = CDbl("-99999999999")
    '                .TypeFloatMax = CDbl("99999999999")
    '                .TypeFloatMoney = False
    '                .TypeFloatSeparator = False
    '                .TypeFloatDecimalChar = Asc(".")
    '                '            .TypeFloatSepChar = Asc(",")
    '                .set_ColWidth(cntCol, 12)
    '            Next

    '            .Col = ColBillItemValue
    '            .ColHidden = True

    '            .Col = ColNetAmount
    '            .ColHidden = True

    '            .Col = ColCancelled
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColCancelled, 8)
    '            .ColHidden = True

    '            .Col = ColMKEY
    '            .CellType = SS_CELL_TYPE_EDIT
    '            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '            .TypeEditLen = 255
    '            .TypeEditMultiLine = True
    '            .set_ColWidth(ColMKEY, 8)
    '            .ColHidden = True


    '            MainClass.SetSpreadColor(SprdMain, -1)
    '            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
    '            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle
    '            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
    '            SprdMain.DAutoCellTypes = True
    '            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
    '        End With
    '    End Sub
    '    Private Sub FillHeading()

    '        Dim RsTemp As ADODB.Recordset
    '        Dim cntCol As Integer
    '        Dim Sqlstr As String
    '        Dim mRecordCount As Integer

    '        MainClass.ClearGrid(SprdMain)

    '        With SprdMain
    '            .MaxCols = ColNetAmount
    '            mRecordCount = 0
    '            .Row = 0
    '            '        .RowHeight(0) = ConRowHeight * 2
    '            '
    '            '        .RowHeight(-1) = ConRowHeight * 1.5

    '            If FormActive = False Then
    '                Sqlstr = " SELECT NAME FROM FIN_INTERFACE_MST " & vbCrLf _
    '                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    '                    & " AND TYPE IN ('B','P')" & vbCrLf & " AND STATUS='O' AND GST_ENABLED='Y'"

    '                Sqlstr = Sqlstr & vbCrLf & " ORDER BY PRINTSEQUENCE"
    '            Else
    '                Sqlstr = FillHeadingQry()
    '            End If

    '            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

    '            If RsTemp.EOF = False Then
    '                Do While Not RsTemp.EOF
    '                    mRecordCount = mRecordCount + 1
    '                    RsTemp.MoveNext()
    '                Loop
    '                RsTemp.MoveFirst()
    '            End If

    '            If RsTemp.EOF = False Then
    '                ColCancelled = .MaxCols + mRecordCount + 1
    '                ColMKEY = ColCancelled + 1
    '                .MaxCols = ColMKEY

    '                cntCol = 1
    '                Do While Not RsTemp.EOF
    '                    .Col = ColNetAmount + cntCol
    '                    .Text = RsTemp.Fields("Name").Value
    '                    cntCol = cntCol + 1
    '                    RsTemp.MoveNext()
    '                Loop
    '            Else
    '                ColCancelled = .MaxCols + 1
    '                ColMKEY = ColCancelled + 1
    '                .MaxCols = ColMKEY
    '            End If

    '            .Col = ColMKEY
    '            .Text = "Mkey"

    '            .Col = ColCancelled
    '            .Text = "Cancelled"

    '            FormatSprdMain(-1)
    '        End With
    '    End Sub

    '    Private Function FillHeadingQry() As String

    '        On Error GoTo ErrPart
    '        Dim Sqlstr As String
    '        Dim mTrnCode As Integer
    '        Dim mTrnTypeStr As String
    '        Dim CntLst As Integer
    '        Dim mInvoiceType As String
    '        Dim mShowAll As Boolean

    '        Dim mCompanyName As String
    '        Dim mCompanyCode As String
    '        Dim mCompanyCodeStr As String

    '        Sqlstr = " SELECT DISTINCT IMST.NAME ,IMST.PRINTSEQUENCE " & vbCrLf _
    '            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_EXP EXP, FIN_SUPP_CUST_MST CMST, FIN_INTERFACE_MST IMST, GEN_COMPANY_MST CC" & vbCrLf _
    '            & " WHERE IH.COMPANY_CODE=CC.COMPANY_CODE" & vbCrLf _
    '            & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf _
    '            & " AND IH.MKEY=EXP.MKEY" & vbCrLf _
    '            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
    '            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
    '            & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
    '            & " AND EXP.EXPCODE=IMST.CODE"
    '        '
    '        '    SqlStr = SqlStr & vbCrLf _
    '        ''            & " AND IH.VDATE>=TO_DATE('" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
    '        ''            & " AND IH.VDATE<=TO_DATE('" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"


    '        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                lblAcCode.Text = MasterNo
    '            Else
    '                lblAcCode.Text = "-1"
    '            End If

    '            Sqlstr = Sqlstr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
    '        End If

    '        If optType(0).Checked = True Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND CMST.WITHIN_STATE='Y'"
    '        ElseIf optType(1).Checked = True Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND CMST.WITHIN_STATE='N'"
    '        End If

    '        If lstCompanyName.GetItemChecked(0) = True Then
    '            mCompanyCodeStr = ""
    '        Else
    '            For CntLst = 1 To lstCompanyName.Items.Count - 1
    '                If lstCompanyName.GetItemChecked(CntLst) = True Then
    '                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
    '                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
    '                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
    '                    End If
    '                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
    '                End If
    '            Next
    '        End If

    '        If mCompanyCodeStr <> "" Then
    '            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
    '            Sqlstr = Sqlstr & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
    '        End If

    '        mShowAll = True
    '        For CntLst = 1 To lstInvoiceType.Items.Count - 1
    '            If lstInvoiceType.GetItemChecked(CntLst) = True Then
    '                '            lstInvoiceType.ListIndex = CntLst
    '                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
    '                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
    '                End If
    '                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
    '            Else
    '                mShowAll = False
    '            End If
    '        Next

    '        If mShowAll = False Then
    '            If mTrnTypeStr <> "" Then
    '                mTrnTypeStr = "(" & mTrnTypeStr & ")"
    '                Sqlstr = Sqlstr & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
    '            End If
    '        End If

    '        If cboAgtD3.SelectedIndex > 0 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboAgtD3.Text, 1) & "'"
    '        End If

    '        If cboPurchaseType.SelectedIndex > 0 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND IH.PURCHASESEQTYPE='" & VB.Left(cboPurchaseType.Text, 1) & "'"
    '        End If

    '        If cboCountry.SelectedIndex = 1 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
    '        ElseIf cboCountry.SelectedIndex = 2 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
    '        End If

    '        If cboGSTStatus.SelectedIndex > 0 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND IH.ISGSTAPPLICABLE='" & VB.Left(cboGSTStatus.Text, 1) & "'"
    '        End If

    '        If cboGS.SelectedIndex = 1 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND IH.BOOKSUBTYPE<>'W'"
    '        ElseIf cboGS.SelectedIndex = 2 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND IH.BOOKSUBTYPE='W'"
    '        End If

    '        If cboCancelled.SelectedIndex > 0 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
    '        End If

    '        If cboFOC.SelectedIndex > 0 Then
    '            Sqlstr = Sqlstr & vbCrLf & "AND IH.ISFOC='" & VB.Left(cboFOC.Text, 1) & "'"
    '        End If

    '        'If Trim(txtTariffHeading.Text) <> "" Then
    '        '    Sqlstr = Sqlstr & vbCrLf & "AND IH.TARIFFHEADING=" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & ""
    '        'End If

    '        Sqlstr = Sqlstr & vbCrLf & "AND IH.ISFINALPOST='Y'" '' AND IH.VNO<>'-1'"

    '        Sqlstr = Sqlstr & vbCrLf & " AND IH.MRRDATE BETWEEN TO_DATE('" & VB6.Format(txtMRRDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtMRRDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

    '        Sqlstr = Sqlstr & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


    '        Sqlstr = Sqlstr & vbCrLf & "AND EXP.AMOUNT<>0"

    '        Sqlstr = Sqlstr & vbCrLf & " ORDER BY IMST.PRINTSEQUENCE"

    '        FillHeadingQry = Sqlstr
    '        Exit Function
    'ErrPart:
    '        FillHeadingQry = ""
    '    End Function

    Private Function Show1(pType As String) As Boolean

        On Error GoTo LedgError
        Dim cntCol As Integer
        Dim cntRow As Integer
        Dim mFieldTitle As String
        Dim mMKey As String
        Dim mValue As Double
        'Dim mTotValue As Double
        Dim mCancelled As String
        Dim Sqlstr As String

        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim mGetFieldName As String
        Dim mGetFieldValue As Double

        Dim mGetItemValue As Double
        Dim mItemValue As Double
        Dim mBillItemValue As Double
        Dim mNetItemValue As Double

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If optShowReg(0).Checked = True Then
            Sqlstr = MakeSQL(pType)



            'MainClass.AssignDataInSprd8(Sqlstr, SprdMain, StrConn, "Y")
            'FormatSprdMain(-1)

            'With SprdMain
            '    For cntRow = 1 To .MaxRows

            '        .Row = cntRow
            '        .Col = ColMKEY
            '        mMKey = Trim(.Text)

            '        If mMKey = "" Then GoTo NextRow

            '        .Col = ColCancelled
            '        mCancelled = Trim(.Text)

            '        .Col = ColItemAmount
            '        mNetItemValue = CDbl(VB6.Format(.Text, "0.00"))

            '        If VB.Left(mCancelled, 1) = "N" Then

            '            pSqlStr = "SELECT EXP.AMOUNT * DECODE(ADD_DED,'D',-1,1) AS AMOUNT, IMST.NAME " & vbCrLf _
            '                & " FROM FIN_PURCHASE_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf _
            '                & " WHERE EXP.MKEY='" & mMKey & "'" & vbCrLf _
            '                & " AND EXP.EXPCODE=IMST.CODE"  '' & vbCrLf 
            '            '' & " And EXP.EXPCODE=IMST.CODE" ''& vbCrLf |                            & " And IMST.NAME='" & MainClass.AllowSingleQuote(pFieldTitle) & "'"

            '            MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            '            Do While RsTemp.EOF = False
            '                mGetFieldName = IIf(IsDBNull(RsTemp.Fields("Name").Value), "", RsTemp.Fields("Name").Value)
            '                mGetFieldValue = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)

            '                .Row = cntRow
            '                .Col = ColItemAmount
            '                mItemValue = CDbl(VB6.Format(.Text, "0.00"))

            '                .Col = ColBillItemValue
            '                mBillItemValue = CDbl(VB6.Format(.Text, "0.00"))

            '                mGetItemValue = CDbl(VB6.Format(mGetFieldValue * mItemValue / mBillItemValue, "0.00"))

            '                For cntCol = ColNetAmount + 1 To ColMKEY - 2
            '                    .Row = 0
            '                    .Col = cntCol
            '                    mFieldTitle = Trim(.Text)

            '                    If UCase(Trim(mFieldTitle)) = UCase(Trim(mGetFieldName)) Then
            '                        .Row = cntRow
            '                        .Col = cntCol
            '                        .Text = VB6.Format(mGetItemValue, "0.00") ''Format(mGetFieldValue, "0.00")
            '                        mNetItemValue = mNetItemValue + CDbl(VB6.Format(mGetItemValue, "0.00"))
            '                        Exit For
            '                    End If
            '                Next
            '                RsTemp.MoveNext()
            '            Loop

            '            .Row = cntRow
            '            .Col = ColNetAmount
            '            .Text = VB6.Format(mNetItemValue, "0.00")

            '        End If
            '    Next
            'End With
        Else
            Sqlstr = MakeSQLSupp(pType)
            'MainClass.AssignDataInSprd8(Sqlstr, SprdMain, StrConn, "Y")
            'FormatSprdMain(-1)

            'With SprdMain
            '    For cntRow = 1 To .MaxRows

            '        .Row = cntRow
            '        .Col = ColMKEY
            '        mMKey = Trim(.Text)

            '        If mMKey = "" Then GoTo NextRow

            '        .Col = ColItemAmount
            '        mNetItemValue = CDbl(VB6.Format(.Text, "0.00"))

            '        .Col = ColCancelled
            '        mCancelled = Trim(.Text)

            '        If VB.Left(mCancelled, 1) = "N" Then

            '            pSqlStr = "SELECT EXP.AMOUNT, IMST.NAME " & vbCrLf _
            '                & " FROM FIN_SUPP_PURCHASE_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf _
            '                & " WHERE EXP.MKEY='" & mMKey & "'" & vbCrLf _
            '                & " AND EXP.EXPCODE=IMST.CODE"

            '            MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            '            Do While RsTemp.EOF = False
            '                mGetFieldName = IIf(IsDBNull(RsTemp.Fields("Name").Value), "", RsTemp.Fields("Name").Value)
            '                mGetFieldValue = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)

            '                .Row = cntRow
            '                .Col = ColItemAmount
            '                mItemValue = CDbl(VB6.Format(.Text, "0.00"))

            '                .Col = ColBillItemValue
            '                mBillItemValue = CDbl(VB6.Format(.Text, "0.00"))

            '                mGetItemValue = CDbl(VB6.Format(mGetFieldValue * mItemValue / mBillItemValue, "0.00"))

            '                For cntCol = ColNetAmount + 1 To ColMKEY - 2
            '                    .Row = 0
            '                    .Col = cntCol
            '                    mFieldTitle = Trim(.Text)

            '                    If UCase(Trim(mFieldTitle)) = UCase(Trim(mGetFieldName)) Then
            '                        .Row = cntRow
            '                        .Col = cntCol
            '                        '                                .Text = Format(mGetFieldValue, "0.00")
            '                        .Text = VB6.Format(mGetItemValue, "0.00") ''Format(mGetFieldValue, "0.00")
            '                        mNetItemValue = mNetItemValue + CDbl(VB6.Format(mGetItemValue, "0.00"))
            '                        Exit For
            '                    End If
            '                Next
            '                RsTemp.MoveNext()
            '            Loop
            '        End If
            '    Next
            'End With
        End If

        Call FillUltraGrid(Sqlstr)

NextRow:

        '''********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        UltraDataSource1.Rows.Clear()
        Me.UltraGrid1.DataSource = Nothing
        oledbCnn = New OleDbConnection(StrConn)
        Try

            ClearGroupFromUltraGrid(UltraGrid1)
            ClearFilterFromUltraGrid(UltraGrid1)
            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(pMakeSql, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            Me.UltraGrid1.DataSource = ds
            Me.UltraGrid1.DataMember = ""

            CreateGridHeader()


            oledbAdapter.Dispose()
            oledbCnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function GetExpenseAmount(ByRef pFieldTitle As String, ByRef pMKey As String, ByRef pCancelled As String) As Double

        On Error GoTo LedgError
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset

        GetExpenseAmount = 0

        If pCancelled = "Y" Then
            Exit Function
        End If


        Sqlstr = "SELECT EXP.AMOUNT " & vbCrLf _
            & " FROM FIN_PURCHASE_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf _
            & " WHERE EXP.MKEY='" & pMKey & "'" & vbCrLf _
            & " AND EXP.EXPCODE=IMST.CODE" & vbCrLf & " AND IMST.NAME='" & MainClass.AllowSingleQuote(pFieldTitle) & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetExpenseAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If
        Exit Function
LedgError:
        GetExpenseAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQL(ByRef pType As String) As String

        On Error GoTo ERR1
        Dim mTrnCode As String
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean
        Dim cntCol As Integer
        Dim mStr As String
        Dim mDivisionCode As Double
        Dim mGSTStatus As String

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        'For cntCol = ColNetAmount + 1 To ColMKEY - 2
        '    mStr = mStr & "0, "
        'Next

        ''''SELECT CLAUSE...

        ''TO_CHAR(IH.ITEMVALUE + NVL(IH.TOTEXPAMT,0)-NVL(IH.TOTEDAMOUNT,0) - NVL(IH.TOTSERVICEAMOUNT,0) -NVL(IH.TOTEDUAMOUNT,0)-NVL(IH.TOTSTAMT,0) - NVL(IH.SHECAMOUNT,0))


        MakeSQL = " SELECT CC.COMPANY_SHORTNAME, IH.VNO, IH.VDATE, " & vbCrLf _
            & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf _
            & " CMST.SUPP_CUST_NAME, CMST.GST_RGN_NO, " & vbCrLf _
            & " DMST.SUPP_CUST_NAME, " & vbCrLf _
            & " ID.ITEM_CODE, ID.ITEM_DESC, ID.ITEM_UOM, " & vbCrLf _
            & " ID.ITEM_QTY, ID.ITEM_RATE, ID.ITEM_AMT, "

        'MakeSQL = MakeSQL & vbCrLf & "0,0,0,0,0,"

        'MakeSQL = MakeSQL & vbCrLf _
        '    & " TO_CHAR(DECODE(NVL(IH.ITEMVALUE,0),0,0,IH.TOTCGST_REFUNDAMT*ID.ITEM_AMT/IH.ITEMVALUE),'0.00')," & vbCrLf _
        '    & " TO_CHAR(DECODE(NVL(IH.ITEMVALUE,0),0,0,IH.TOTSGST_REFUNDAMT*ID.ITEM_AMT/IH.ITEMVALUE),'0.00')," & vbCrLf _
        '    & " TO_CHAR(DECODE(NVL(IH.ITEMVALUE,0),0,0,IH.TOTIGST_REFUNDAMT*ID.ITEM_AMT/IH.ITEMVALUE),'0.00')," & vbCrLf _
        '    & " TO_CHAR(DECODE(NVL(IH.ITEMVALUE,0),0,0,IH.TOTEXPAMT*ID.ITEM_AMT/IH.ITEMVALUE),'0.00'), " & vbCrLf _
        '    & " TO_CHAR(DECODE(NVL(IH.ITEMVALUE,0),0,0,IH.NETVALUE*ID.ITEM_AMT/IH.ITEMVALUE),'0.00'),"

        MakeSQL = MakeSQL & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ROUND(IH.TOTCGST_REFUNDAMT*ID.ITEM_AMT/IH.ITEMVALUE,2) END," & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ROUND(IH.TOTSGST_REFUNDAMT*ID.ITEM_AMT/IH.ITEMVALUE,2) END," & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ROUND(IH.TOTIGST_REFUNDAMT*ID.ITEM_AMT/IH.ITEMVALUE,2) END," & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ROUND(IH.TOTEXPAMT*ID.ITEM_AMT/IH.ITEMVALUE,2) END, " & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE ROUND(IH.NETVALUE*ID.ITEM_AMT/IH.ITEMVALUE,2) END,"

        MakeSQL = MakeSQL & vbCrLf & " IH.CANCELLED ,IH.MKEY"

        ''''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST DMST, GEN_COMPANY_MST CC"


        ''''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE IH.COMPANY_CODE=CC.COMPANY_CODE AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=DMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND ID.PUR_ACCOUNT_CODE=DMST.SUPP_CUST_CODE(+)"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If optType(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_STATE='N'"
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQL = MakeSQL & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        mAllTrnType = True

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & ",'" & mTrnCode & "'")
            Else
                mAllTrnType = False
            End If
        Next

        If mTrnTypeStr <> "" And mAllTrnType = False Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND ID.PUR_ACCOUNT_CODE IN " & mTrnTypeStr & ""
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboAgtD3.Text, 1) & "'"
        End If

        If cboPurchaseType.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PURCHASESEQTYPE='" & VB.Left(cboPurchaseType.Text, 1) & "'"
        End If

        If cboCountry.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        ElseIf cboCountry.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        End If

        If cboGSTStatus.SelectedIndex > 0 Then
            mGSTStatus = VB.Left(cboGSTStatus.Text, 1)
            mGSTStatus = IIf(mGSTStatus = "1" Or mGSTStatus = "2", "G", mGSTStatus)

            MakeSQL = MakeSQL & vbCrLf & "AND IH.ISGSTAPPLICABLE='" & mGSTStatus & "'"
        End If

        If cboGS.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.BOOKSUBTYPE<>'W'"
        ElseIf cboGS.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.BOOKSUBTYPE='W'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ISFOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING=" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & ""
        'End If

        MakeSQL = MakeSQL & vbCrLf & "AND IH.ISFINALPOST='Y' AND IH.PURCHASE_TYPE NOT IN ('S','W')" '' AND IH.VNO<>'-1'"

        If pType = "S" Then
            MakeSQL = MakeSQL & vbCrLf & " AND 1=2"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.MRRDATE BETWEEN TO_DATE('" & VB6.Format(txtMRRDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtMRRDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQL = MakeSQL & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        '''''ORDER CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.VDATE,IH.VNO"
        ElseIf optShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.VNO, IH.VDATE"
        ElseIf optShow(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,IH.VNO, IH.VDATE"
        ElseIf optShow(3).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY DMST.SUPP_CUST_NAME,IH.VNO, IH.VDATE"
        End If


        '''''ORDER CLAUSE...

        'If optShow(0).Checked = True Then
        '    Sqlstr = Sqlstr & vbCrLf & "ORDER BY 3,2"
        'ElseIf optShow(1).Checked = True Then
        '    Sqlstr = Sqlstr & vbCrLf & "ORDER BY 2,3"
        'ElseIf optShow(2).Checked = True Then
        '    Sqlstr = Sqlstr & vbCrLf & "ORDER BY 6,2,3"
        'ElseIf optShow(3).Checked = True Then
        '    Sqlstr = Sqlstr & vbCrLf & "ORDER BY 7,2,3"
        'End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function


    Private Function MakeSQLWO() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean
        Dim cntCol As Integer
        Dim mStr As String
        Dim mDivisionCode As Double


        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        'For cntCol = ColNetAmount + 1 To ColMKEY - 2
        '    mStr = mStr & "0, "
        'Next

        ''''SELECT CLAUSE...

        ''TO_CHAR(IH.ITEMVALUE + NVL(IH.TOTEXPAMT,0)-NVL(IH.TOTEDAMOUNT,0) - NVL(IH.TOTSERVICEAMOUNT,0) -NVL(IH.TOTEDUAMOUNT,0)-NVL(IH.TOTSTAMT,0) - NVL(IH.SHECAMOUNT,0))


        MakeSQLWO = " SELECT CC.COMPANY_SHORTNAME, IH.VNO, IH.VDATE, " & vbCrLf _
            & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf _
            & " CMST.SUPP_CUST_NAME, CMST.GST_RGN_NO," & vbCrLf _
            & " DMST.SUPP_CUST_NAME, " & vbCrLf _
            & " IH.NETVALUE, " & vbCrLf _
            & " SUM(ID.ITEM_AMT), SUM(DECODE(GST_CREDITAPP,'Y',DECODE(GST_RCAPP,'N',DECODE(IH.ISGSTAPPLICABLE,'G',ID.CGST_AMOUNT,0),0),0)), SUM(DECODE(GST_CREDITAPP,'Y',DECODE(GST_RCAPP,'N',DECODE(IH.ISGSTAPPLICABLE,'G',ID.SGST_AMOUNT,0),0),0)),SUM(DECODE(GST_CREDITAPP,'Y',DECODE(GST_RCAPP,'N',DECODE(IH.ISGSTAPPLICABLE,'G',ID.IGST_AMOUNT,0),0),0)), " & vbCrLf _
            & " 0, " & vbCrLf _
            & mStr

        MakeSQLWO = MakeSQLWO & vbCrLf & " IH.CANCELLED ,IH.MKEY"

        ''''FROM CLAUSE...
        MakeSQLWO = MakeSQLWO & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST DMST, GEN_COMPANY_MST CC"

        ''''WHERE CLAUSE...
        MakeSQLWO = MakeSQLWO & vbCrLf & " WHERE IH.COMPANY_CODE=CC.COMPANY_CODE AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=DMST.COMPANY_CODE(+)" & vbCrLf & " AND ID.PUR_ACCOUNT_CODE=DMST.SUPP_CUST_CODE(+)"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If optType(0).Checked = True Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND CMST.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND CMST.WITHIN_STATE='N'"
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQLWO = MakeSQLWO & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQLWO = MakeSQLWO & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        mAllTrnType = True

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mAllTrnType = False
            End If
        Next




        If mTrnTypeStr <> "" And mAllTrnType = False Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQLWO = MakeSQLWO & vbCrLf & " AND IH.ITEM_TRNTYPE IN " & mTrnTypeStr & ""
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboAgtD3.Text, 1) & "'"
        End If

        If cboPurchaseType.SelectedIndex > 0 Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND IH.PURCHASESEQTYPE='" & VB.Left(cboPurchaseType.Text, 1) & "'"
        End If

        If cboCountry.SelectedIndex = 1 Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        ElseIf cboCountry.SelectedIndex = 2 Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        End If

        If VB.Left(cboGSTStatus.Text, 1) = "G" Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND  ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N'" '(IH.ISGSTAPPLICABLE='G' OR
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "N" Or VB.Left(cboGSTStatus.Text, 1) = "C" Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND IH.ISGSTAPPLICABLE='" & VB.Left(cboGSTStatus.Text, 1) & "'"
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "R" Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND (ID.GST_RCAPP='Y' OR IH.ISGSTAPPLICABLE IN ('R'))"
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "E" Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND ID.GST_EXEMPTED='Y'"
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "I" Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND ID.GST_CREDITAPP='N' AND ID.GST_EXEMPTED='N' AND IH.ISGSTAPPLICABLE NOT IN ('N')"
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "1" Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND  ID.GST_CREDITAPP='Y' AND ID.GST_RCAPP='N' AND IH.ISGSTAPPLICABLE NOT IN ('R')"
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "2" Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND  ID.GST_CREDITAPP='Y' AND (ID.GST_RCAPP='Y' OR IH.ISGSTAPPLICABLE IN ('R'))"
        End If

        If cboGS.SelectedIndex = 1 Then
            MakeSQLWO = MakeSQLWO & vbCrLf & " AND IH.BOOKSUBTYPE<>'W'"
        ElseIf cboGS.SelectedIndex = 2 Then
            MakeSQLWO = MakeSQLWO & vbCrLf & " AND IH.BOOKSUBTYPE='W'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "AND IH.ISFOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    MakeSQLWO = MakeSQLWO & vbCrLf & "AND IH.TARIFFHEADING=" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & ""
        'End If

        MakeSQLWO = MakeSQLWO & vbCrLf & "AND IH.ISFINALPOST='Y' AND NVL(IH.PURCHASE_TYPE,'G') IN ('W','S')" '' AND IH.VNO<>'-1'"

        MakeSQLWO = MakeSQLWO & vbCrLf & " AND IH.MRRDATE BETWEEN TO_DATE('" & VB6.Format(txtMRRDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtMRRDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MakeSQLWO = MakeSQLWO & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        ''''GROUP BY
        MakeSQLWO = MakeSQLWO & vbCrLf & "GROUP BY CC.COMPANY_SHORTNAME,IH.VNO, IH.VDATE, IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_NAME,CMST.GST_RGN_NO, DMST.SUPP_CUST_NAME, IH.NETVALUE, IH.CANCELLED ,IH.MKEY "

        ''''ORDER CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "ORDER BY IH.VDATE,IH.VNO,CC.COMPANY_SHORTNAME"
        ElseIf optShow(1).Checked = True Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "ORDER BY IH.VNO, IH.VDATE,CC.COMPANY_SHORTNAME"
        ElseIf optShow(2).Checked = True Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,IH.VNO, IH.VDATE,CC.COMPANY_SHORTNAME"
        ElseIf optShow(3).Checked = True Then
            MakeSQLWO = MakeSQLWO & vbCrLf & "ORDER BY DMST.SUPP_CUST_NAME,IH.VNO, IH.VDATE,CC.COMPANY_SHORTNAME"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLSupp(ByRef pType As String) As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAllTrnType As Boolean
        Dim cntCol As Integer
        Dim mStr As String
        Dim mDivisionCode As Double

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        'For cntCol = ColNetAmount + 1 To ColMKEY - 2
        '    mStr = mStr & "0, "
        'Next

        ''''SELECT CLAUSE...

        ''TO_CHAR(IH.ITEMVALUE + NVL(IH.TOTEXPAMT,0)-NVL(IH.TOTEDAMOUNT,0) - NVL(IH.TOTSERVICEAMOUNT,0) -NVL(IH.TOTEDUAMOUNT,0)-NVL(IH.TOTSTAMT,0) - NVL(IH.SHECAMOUNT,0))


        MakeSQLSupp = " SELECT CC.COMPANY_SHORTNAME, IH.VNO, IH.VDATE, " & vbCrLf _
            & " IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf _
            & " CMST.SUPP_CUST_NAME,CMST.GST_RGN_NO, " & vbCrLf _
            & " DMST.SUPP_CUST_NAME, " & vbCrLf _
            & " ID.ITEM_CODE, ID.ITEM_DESC, ID.ITEM_UOM, " & vbCrLf _
            & " ID.QTY, ID.RATE, ID.AMOUNT, " & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE IH.TOTCGST_REFUNDAMT*ID.AMOUNT/IH.ITEMVALUE END," & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE IH.TOTSGST_REFUNDAMT*ID.AMOUNT/IH.ITEMVALUE END," & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE IH.TOTIGST_REFUNDAMT*ID.AMOUNT/IH.ITEMVALUE END," & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE IH.TOTEXPAMT*ID.ITEM_AMT/IH.ITEMVALUE END," & vbCrLf _
            & " CASE WHEN IH.ITEMVALUE=0 THEN 0 ELSE IH.NETVALUE*ID.ITEM_AMT/IH.ITEMVALUE END,"


        MakeSQLSupp = MakeSQLSupp & vbCrLf & " IH.CANCELLED ,IH.MKEY"

        ''''FROM CLAUSE...
        MakeSQLSupp = MakeSQLSupp & vbCrLf _
            & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_SUPP_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST DMST, GEN_COMPANY_MST CC"

        ''''WHERE CLAUSE...
        MakeSQLSupp = MakeSQLSupp & vbCrLf _
            & " WHERE IH.COMPANY_CODE=CC.COMPANY_CODE AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=DMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND ID.PUR_ACCOUNT_CODE=DMST.SUPP_CUST_CODE(+)"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If optType(0).Checked = True Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND CMST.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND CMST.WITHIN_STATE='N'"
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND CC.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        mAllTrnType = True

        'For CntLst = 1 To lstInvoiceType.Items.Count - 1
        '    If lstInvoiceType.GetItemChecked(CntLst) = True Then
        '        mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
        '        If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        '        End If
        '        mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
        '    Else
        '        mAllTrnType = False
        '    End If
        'Next

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & ",'" & mTrnCode & "'")
            Else
                mAllTrnType = False
            End If
        Next

        If mTrnTypeStr <> "" And mAllTrnType = False Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND ID.PUR_ACCOUNT_CODE IN " & mTrnTypeStr & ""
        End If

        '    If cboPurchaseType.ListIndex > 0 Then
        '        Sqlstr = Sqlstr & vbCrLf & "AND IH.PURCHASESEQTYPE='" & vb.Left(cboPurchaseType.Text, 1) & "'"
        '    End If

        '    If cboAgtD3.ListIndex > 0 Then
        '        MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.REJECTION='" & vb.Left(cboAgtD3.Text, 1) & "'"
        '    End If

        If cboCountry.SelectedIndex = 1 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        ElseIf cboCountry.SelectedIndex = 2 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        End If

        If cboGSTStatus.SelectedIndex > 0 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.ISGSTAPPLICABLE='" & VB.Left(cboGSTStatus.Text, 1) & "'"
        End If

        '    If cboGS.ListIndex = 1 Then
        '        MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND IH.BOOKSUBTYPE<>'W'"
        '    ElseIf cboGS.ListIndex = 2 Then
        '        MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND IH.BOOKSUBTYPE='W'"
        '    End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.ISFOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.TARIFFHEADING=" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & ""
        'End If

        MakeSQLSupp = MakeSQLSupp & vbCrLf & "AND IH.ISFINALPOST='Y'" '' AND IH.VNO<>'-1'"
        '
        '    MakeSQLSupp = MakeSQLSupp & vbCrLf _
        ''            & " AND IH.MRRDATE BETWEEN TO_DATE('" & vb6.Format(txtMRRDateFrom.Text, "DD-MMM-YYYY") & "') AND TO_DATE('" & vb6.Format(txtMRRDateTo.Text, "DD-MMM-YYYY") & "')"

        MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND IH.VDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If pType = "S" Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & " AND 1=2"
        End If

        ''''ORDER CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY IH.VDATE,IH.VNO, CC.COMPANY_SHORTNAME"
        ElseIf optShow(1).Checked = True Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY IH.VNO, IH.VDATE, CC.COMPANY_SHORTNAME"
        ElseIf optShow(2).Checked = True Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,IH.VNO, IH.VDATE,CC.COMPANY_SHORTNAME"
        ElseIf optShow(3).Checked = True Then
            MakeSQLSupp = MakeSQLSupp & vbCrLf & "ORDER BY DMST.SUPP_CUST_NAME,IH.VNO, IH.VDATE,CC.COMPANY_SHORTNAME"
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

        If MainClass.ChkIsdateF(txtMRRDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtMRRDateFrom.Text)) = False Then txtMRRDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtMRRDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtMRRDateTo.Text)) = False Then txtMRRDateTo.SetFocus

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
    '    Private Sub CalcSprdTotal()

    '        On Error GoTo ErrPart
    '        Dim cntRow As Integer
    '        Dim cntCol As Integer
    '        Dim mTotValue As Double

    '        Call MainClass.AddBlankfpSprdRow(SprdMain, ColPartyName)
    '        FormatSprdMain(-1)

    '        With SprdMain
    '            .Col = ColPartyName
    '            .Row = .MaxRows
    '            .Text = "GRAND TOTAL :"
    '            .Font = VB6.FontChangeBold(.Font, True)

    '            .Row = .MaxRows
    '            .Row2 = .MaxRows
    '            .Col = 1
    '            .Col2 = .MaxCols
    '            .BlockMode = True
    '            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
    '            .BlockMode = False


    '            For cntCol = ColItemAmount To ColMKEY - 2
    '                mTotValue = 0
    '                For cntRow = 1 To .MaxRows - 1
    '                    .Row = cntRow
    '                    .Col = cntCol
    '                    mTotValue = mTotValue + Val(.Text)
    '                Next
    '                .Row = .MaxRows
    '                .Col = cntCol
    '                .Text = VB6.Format(mTotValue, "0.00")
    '            Next
    '        End With
    '        Exit Sub
    'ErrPart:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Sub

    Private Sub txtMRRDateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtMRRDateFrom) = False Then
            txtMRRDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtMRRDateFrom.Text)) = False Then
        '        txtMRRDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtMRRDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtMRRDateTo) = False Then
            txtMRRDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtMRRDateTo.Text)) = False Then
        '        txtMRRDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim Sqlstr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        Sqlstr = "SELECT DISTINCT B.SUPP_CUST_NAME FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.ACCOUNTPOSTCODE=B.SUPP_CUST_CODE" & vbCrLf _
            & " AND A.CATEGORY='P' ORDER BY SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            lstInvoiceType.SetItemChecked(0, True)
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        lstCompanyName.Items.Clear()
        Sqlstr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(RsCompany.Fields("COMPANY_SHORTNAME").Value = RS.Fields("COMPANY_SHORTNAME").Value, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub


    '    Private Sub txtTariffHeading_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '        SearchTariff()
    '    End Sub

    '    Private Sub txtTariffHeading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
    '        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

    '        KeyAscii = MainClass.SetNumericField(KeyAscii)
    '        eventArgs.KeyChar = Chr(KeyAscii)
    '        If KeyAscii = 0 Then
    '            eventArgs.Handled = True
    '        End If
    '    End Sub

    '    Private Sub txtTariffHeading_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs)
    '        Dim KeyCode As Short = eventArgs.KeyCode
    '        Dim Shift As Short = eventArgs.KeyData \ &H10000
    '        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    '    End Sub

    '    Private Sub txtTariffHeading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
    '        Dim Cancel As Boolean = eventArgs.Cancel
    '        On Error GoTo ERR1
    '        Dim Sqlstr As String

    '        If Trim(txtTariffHeading.Text) = "" Then GoTo EventExitSub

    '        Sqlstr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

    '        If MainClass.ValidateWithMasterTable((txtTariffHeading.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo, , Sqlstr) = False Then
    '            ErrorMsg("Please Enter Vaild Tariff.", "", MsgBoxStyle.Critical)
    '            Cancel = True
    '        End If
    '        GoTo EventExitSub
    'ERR1:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    'EventExitSub:
    '        eventArgs.Cancel = Cancel
    '    End Sub

    '    Private Sub SearchTariff()
    '        On Error GoTo ErrPart
    '        Dim Sqlstr As String
    '        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

    '        If MainClass.SearchGridMaster((txtTariffHeading.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , Sqlstr) = True Then
    '            txtTariffHeading.Text = AcName
    '            '        txtTariff_Validate False
    '            If txtTariffHeading.Enabled = True Then txtTariffHeading.Focus()
    '        End If
    '        Exit Sub
    'ErrPart:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Sub
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

    Private Sub lstInvoiceType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstInvoiceType.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstInvoiceType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstInvoiceType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header

            UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True
            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "")

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Caption = "Company Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Header.Caption = "VNo"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1).Header.Caption = "VDate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Header.Caption = "Bill Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Header.Caption = "Party Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyGSTNo - 1).Header.Caption = "GST No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDebiName - 1).Header.Caption = "Debit A / c Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Header.Caption = "Item Desc"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemUOM - 1).Header.Caption = "Item UOM"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemQty - 1).Header.Caption = "Item Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemRate - 1).Header.Caption = "Item Rate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemAmount - 1).Header.Caption = "Item Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCGSTAmount - 1).Header.Caption = "CGST Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSGSTAmount - 1).Header.Caption = "SGST Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIGSTAmount - 1).Header.Caption = "IGST Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOtherAmount - 1).Header.Caption = "Other Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNetAmount - 1).Header.Caption = "Net Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCancelled - 1).Header.Caption = "Cancelled"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "MKey"




            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
                Me.UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellMultiLine = DefaultableBoolean.True
                'UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.FixedHeaderIndicator = FixedHeaderIndicator.Button ''FixedHeaderIndicator.None
                'UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Fixed = False     ''True
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).SortIndicator = SortIndicator.Ascending
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemRate - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCGSTAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSGSTAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIGSTAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOtherAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNetAmount - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemRate - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCGSTAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSGSTAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIGSTAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOtherAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNetAmount - 1).CellAppearance.TextHAlign = HAlign.Right


            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True
            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookType - 1).Hidden = True
            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColBookSubType - 1).Hidden = True


            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyGSTNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDebiName - 1).Width = 250

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemUOM - 1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemQty - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemRate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemAmount - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCGSTAmount - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSGSTAmount - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIGSTAmount - 1).Width = 80


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOtherAmount - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNetAmount - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCancelled - 1).Width = 60

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Width = 100


            Me.UltraGrid1.DisplayLayout.Override.DefaultRowHeight = 30
            Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle   ''HeaderClickAction.Select
            Me.UltraGrid1.DisplayLayout.Override.SelectTypeCol = SelectType.None
            Me.UltraGrid1.DisplayLayout.Override.SelectedAppearancesEnabled = DefaultableBoolean.False
            'Me.UltraGrid1.DisplayLayout.Override.SelectTypeCell = SelectType.ExtendedAutoDrag
            'Me.UltraGrid1.DisplayLayout.Override.SelectTypeRow = SelectType.ExtendedAutoDrag
            Me.UltraGrid1.DisplayLayout.Override.RowSizingAutoMaxLines = True

            Me.UltraGrid1.DisplayLayout.Override.AllowMultiCellOperations = AllowMultiCellOperation.CopyWithHeaders

            Me.UltraGrid1.DisplayLayout.Override.CellClickAction = CellClickAction.CellSelect
            Me.UltraGrid1.DisplayLayout.Override.FixedRowStyle = FixedRowStyle.Top

            'Me.UltraGrid1.DisplayLayout.Override.FixedRowStyle = FixedRowStyle.Bottom

            'Me.UltraGrid1.DisplayLayout.Override.GroupByColumnsHidden = Infragistics.Win.DefaultableBoolean.True

            'Me.UltraGrid1.DisplayLayout.UseFixedHeaders = True
            'Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Header.Fixed = True

            'Me.UltraGrid1.DisplayLayout.Override.FixedRowIndicator = FixedRowIndicator.Button
            'Me.UltraGrid1.Rows(0).AllowFixing = Infragistics.Win.DefaultableBoolean.False

            'Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVDate - 1).Header.Fixed = True
            'Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColVNo - 1).Header.Fixed = True
            ''Me.UltraGrid1.DisplayLayout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow
            ''Me.UltraGrid1.DisplayLayout.Override.FixedCellAppearance.BackColor = Color.LightYellow
            'Me.UltraGrid1.DisplayLayout.Override.FixedCellSeparatorColor = Color.DarkBlue


            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default


            '' Format the Running Total column as currency.
            'UltraGrid1.Columns(ColBalance - 1).DefaultCellStyle.Format = "c"
            '' Set the ValueType of the Running Total column to Decimal.
            'Me.UltraGrid1.Columns(ColBalance - 1).ValueType = GetType(System.Decimal)



        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub UltraGrid1_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles UltraGrid1.InitializeLayout
        Dim mCnt As Long

        ' Turn on all of the Cut, Copy, and Paste functionality. 
        e.Layout.Override.AllowMultiCellOperations = AllowMultiCellOperation.CopyWithHeaders

        ' In order to cut or copy, the user needs to select cells or rows. 
        ' So set CellClickAction so that clicking on a cell selects that cell
        ' instead of going into edit mode.
        e.Layout.Override.CellClickAction = CellClickAction.CellSelect


        ''Allowing Summaries in the UltraGrid 
        e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True
        '' Setting the Sum Summary for the desired column

        e.Layout.Bands(0).Summaries.Add("ColItemQty", SummaryType.Sum, e.Layout.Bands(0).Columns(ColItemQty - 1))
        'e.Layout.Bands(0).Summaries.Add("ColItemRate", SummaryType.Sum, e.Layout.Bands(0).Columns(ColItemRate - 1))

        e.Layout.Bands(0).Summaries.Add("ColItemAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColItemAmount - 1))
        e.Layout.Bands(0).Summaries.Add("ColCGSTAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColCGSTAmount - 1))
        e.Layout.Bands(0).Summaries.Add("ColSGSTAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColSGSTAmount - 1))
        e.Layout.Bands(0).Summaries.Add("ColIGSTAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColIGSTAmount - 1))
        e.Layout.Bands(0).Summaries.Add("ColOtherAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColOtherAmount - 1))
        e.Layout.Bands(0).Summaries.Add("ColNetAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColNetAmount - 1))



        ''Set the display format to be just the number 
        e.Layout.Bands(0).Summaries("ColItemQty").DisplayFormat = "{0:###0.00}"
        'e.Layout.Bands(0).Summaries("ColItemRate").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColItemAmount").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColCGSTAmount").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColSGSTAmount").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColIGSTAmount").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColOtherAmount").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColNetAmount").DisplayFormat = "{0:###0.00}"

        ''Hide the SummaryFooterCaption row 
        e.Layout.Bands(0).Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False

        'e.Layout.Bands(0).SummaryFooterCaption = "TOTAL :"
        Me.UltraGrid1.DisplayLayout.Bands(0).SummaryFooterCaption = "TOTAL :"

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.BackColor = Color.LightSteelBlue

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black
        '     / Here, I want to add grand total

        e.Layout.Bands(0).Summaries("ColItemQty").Appearance.TextHAlign = HAlign.Right
        'e.Layout.Bands(0).Summaries("ColItemRate").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColItemAmount").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColCGSTAmount").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColSGSTAmount").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColIGSTAmount").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColOtherAmount").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColNetAmount").Appearance.TextHAlign = HAlign.Right




        'Disable grid default highlight

        UltraGrid1.DisplayLayout.Override.ResetActiveRowAppearance()
        UltraGrid1.DisplayLayout.Override.ResetActiveCellAppearance()
        UltraGrid1.DisplayLayout.Override.ActiveAppearancesEnabled = DefaultableBoolean.True

        e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy

    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try


            'If m_blnChangeInData = True Then
            '    MessageFromResFile(7284, MessageType.Information)
            '    GridSetFocus(UltraGrid1.ActiveRow.Tag.ToString, UltraGrid1.ActiveCell.Column.Index)
            '    Exit Sub
            'End If

            ''Please provide a location where you whould like to export the data to
            'MessageFromResFile(7304, MessageType.Information, GetLabelDes("7305"))

            Try
                SaveFileDialog1.FileName = Me.Text
            Catch
                SaveFileDialog1.FileName = "File1"
            End Try

            Dim strAction As String = ""
            Try
                strAction = SaveFileDialog1.ShowDialog()
            Catch
                SaveFileDialog1.FileName = "File1"
                strAction = SaveFileDialog1.ShowDialog()
            End Try

            If strAction = "1" Then
                ExportToExcel(SaveFileDialog1.FileName)
            End If
            'Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal strFileName As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   To export the report to excel/csv/text file
        'Comments       :   This function will be called from clickinvoked and enterpressed events
        '                   THIS FUNCTION HAS TO BE OVERRIDED IN THE DERIVED FORM   
        '----------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        Dim start As DateTime
        'Dim timespan As TimeSpan
        start = DateTime.Now
        Try
            Me.UltraGridExcelExporter1.FileLimitBehaviour = ExcelExport.FileLimitBehaviour.TruncateData
            Me.UltraGridExcelExporter1.ExportAsync(Me.UltraGrid1, strFileName & ".xls")
            ' timespan = DateTime.Now.Subtract(start)
            'Exported To File : 
            '  MessageFromResFile(7292, MessageType.Information, strFileName)
        Catch
            'Specified Path Does Not Exist,Invalid File Name
            ErrorMsg(Err.Description, Err.Number)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub UltraGrid1_KeyDown(sender As Object, e As KeyEventArgs) Handles UltraGrid1.KeyDown
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.Enter Then Call UltraGrid1_DoubleClick(sender, e)
    End Sub
End Class
