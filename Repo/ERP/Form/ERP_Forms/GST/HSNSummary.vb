Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmHSNSummary
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As Integer

    Private Const ColVNo As Short = 1
    Private Const ColVDate As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemName As Short = 4
    Private Const ColUOM As Short = 5
    Private Const ColBillNo As Short = 6
    Private Const ColBillDate As Short = 7
    Private Const ColPartyCode As Short = 8
    Private Const ColPartyName As Short = 9
    Private Const ColHSNCode As Short = 10
    Private Const ColQuantity As Short = 11
    Private Const ColRate As Short = 12
    Private Const ColAmount As Short = 13
    Private Const ColTaxableAmount As Short = 14
    Private Const ColTaxRate As Short = 15
    Private Const ColCGST As Short = 16
    Private Const ColSGST As Short = 17
    Private Const ColIGST As Short = 18



    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
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
    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdSearchItem.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdSearchItem.Enabled = True
        End If
    End Sub






    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        '    Report1.WindowShowPrintBtn = IIf(PubGridLockUser = "Y", False, True) '' IIf(PubSuperUser = "S", True, False)
        '    Report1.WindowShowPrintSetupBtn = IIf(PubGridLockUser = "Y", False, True) ''IIf(PubSuperUser = "S", True, False)
        '    Report1.WindowShowExportBtn = IIf(PubGridLockUser = "Y", False, True)
        Report1.Action = 1
    End Sub

    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSubTitle1 As String

        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mSelected As Boolean

        '    Report1.Reset
        '
        '    mTitle = "Item Despatches"
        '
        '    mSelected = True
        '    For CntLst = 0 To lstInvoiceType.ListCount - 1
        '        If lstInvoiceType.Selected(CntLst) = True Then
        '            mInvoiceType = lstInvoiceType.List(CntLst)
        ''            mSubTitle = IIf(mSubTitle = "", mInvoiceType, mSubTitle & "/" & mInvoiceType)
        '        Else
        '            mSelected = False
        '        End If
        '    Next
        '    If mSelected = True Then
        '        mSubTitle = ""
        '    Else
        '        mSubTitle = " (" & mSubTitle & ")"
        '    End If
        '
        '    mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text & mSubTitle
        '
        '    If cboAgtD3.ListIndex = 1 Then
        '        mSubTitle1 = "AGT D3"
        '    End If
        '
        '    If cboFOC.ListIndex = 1 Then
        '        mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "FOC", "/FOC")
        '    End If
        '
        '    If cboRejection.ListIndex = 1 Then
        '        mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Rejection", "/Rejetion")
        '    End If
        '
        '    If cboCancelled.ListIndex = 1 Then
        '        mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Cancelled", "/Cancelled")
        '    End If
        '
        '    If cboCT3.ListIndex = 1 Then
        '        mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "CT3", "/CT3")
        '    End If
        '
        '    If cboCT1.ListIndex = 1 Then
        '        mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "CT1", "/CT1")
        '    End If
        '
        '    If cboExport.ListIndex = 1 Then
        '        mSubTitle1 = mSubTitle1 & IIf(mSubTitle1 = "", "Export", "/Export")
        '    End If
        '
        '    mSubTitle = mSubTitle & IIf(mSubTitle1 = "", "", " (" & mSubTitle1 & ")")
        '
        '    mSubTitle = Mid(mSubTitle, 1, 254)
        '
        '
        '
        '    If chkStock.Value = vbUnchecked Then
        '        If optType(0).Value = True Then
        '            If optOrderBy(0).Value = True Then
        '                Report1.ReportFileName = App.path & "\Reports\ItemDespatch.RPT"
        '            Else
        '                Report1.ReportFileName = App.path & "\Reports\IDBillWise.RPT"
        '            End If
        '        Else
        '            If chkMonthWise.Value = vbUnchecked Then
        '                Report1.ReportFileName = App.path & "\Reports\ItemDespatchSumm.RPT"
        '            Else
        '                Report1.ReportFileName = App.path & "\Reports\ItemDespMonthSumm.RPT"
        '            End If
        '        End If
        '        SqlStr = MakeSQL
        '    Else
        '        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr
        '
        '        SqlStr = ""
        '        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        '        Report1.ReportFileName = App.path & "\Reports\ItemDespatchWithStock.RPT"
        '    End If
        '
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchItem.Click
        SearchItem()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmHSNSummary_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "HSN Summary"
        Call FillInvoiceType()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmHSNSummary_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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



        '    cboInvoiceType.Clear
        '    cboInvoiceType.AddItem "Sale"
        '    cboInvoiceType.AddItem "Purchase"
        '    cboInvoiceType.ListIndex = 0

        '    Call FillInvoiceType

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        TxtItemName.Enabled = False
        cmdSearchItem.Enabled = False


        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmHSNSummary_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmHSNSummary_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub lstInvoiceType_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lstInvoiceType.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        ToolTip1.SetToolTip(lstInvoiceType, lstInvoiceType.Text)
    End Sub


    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
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
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
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
            .MaxCols = ColIGST
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 8)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 8)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 6)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, IIf(optType(0).Checked = True, 20, 29))
            .ColsFrozen = ColItemName
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 8)
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyCode, 6)
            .ColHidden = True

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, IIf(optType(0).Checked = True, 25, 32))
            If optType(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If


            For cntCol = ColQuantity To ColQuantity
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
            Next

            For cntCol = ColRate To ColRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
                If optType(0).Checked = True Then
                    .ColHidden = False
                Else
                    .ColHidden = True
                End If
            Next


            For cntCol = ColAmount To ColIGST
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
            Next

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColHSNCode, 12)




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
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mStock As Double
        Dim mUOM As String
        Dim RS As ADODB.Recordset = Nothing
        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblInvoiceType.Text = "S" Then
            SqlStr = MakeSQL
        ElseIf lblInvoiceType.Text = "P" Then
            SqlStr = MakeSQLPurchase
        End If

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        If lblInvoiceType.Text = "S" Then
            SqlStr = MakeSQLSummary
        ElseIf lblInvoiceType.Text = "P" Then
            SqlStr = MakeSQLPurchaseSummary
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        SprdMain.MaxRows = SprdMain.MaxRows + 1
        SprdMain.Row = SprdMain.MaxRows
        SprdMain.Row2 = SprdMain.MaxRows
        SprdMain.Col = 1
        SprdMain.col2 = SprdMain.MaxCols
        SprdMain.BlockMode = True
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
        SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, False)
        SprdMain.BlockMode = False

        cntRow = SprdMain.MaxRows

        With SprdMain
            If RS.EOF = False Then
                .Row = cntRow
                '            .Col = ColVNo
                '            .Text = IIf(IsNull(RS!VNO), "", RS!VNO)
                '
                '            .Col = ColVDate
                '            .Text = Format(IIf(IsNull(RS!VDate), "", RS!VDate), "DD/MM/YYYY")
                '
                '            .Col = ColItemCode
                '            .Text = IIf(IsNull(RS!ITEM_CODE), "", RS!ITEM_CODE)
                '
                '            .Col = ColItemName
                '            .Text = IIf(IsNull(RS!ITEM_NAME), "", RS!ITEM_NAME)
                '
                '            .Col = ColUOM
                '            .Text = IIf(IsNull(RS!UOM), "", RS!UOM)
                '
                '            .Col = ColBillNo
                '            .Text = IIf(IsNull(RS!BILL_NO), "", RS!BILL_NO)
                '
                '            .Col = ColBillDate
                '            .Text = Format(IIf(IsNull(RS!BILLDATE), "", RS!BILLDATE), "DD/MM/YYYY")
                '
                '            .Col = ColPartyCode
                '            .Text = IIf(IsNull(RS!PARTYCODE), "", RS!PARTYCODE)
                '
                '            .Col = ColPartyName
                '            .Text = IIf(IsNull(RS!PARTYNAME), "", RS!PARTYNAME)
                '
                '            .Col = ColHSNCode
                '            .Text = IIf(IsNull(RS!HSNCODE), "", RS!HSNCODE)

                .Col = ColQuantity
                .Text = VB6.Format(IIf(IsDbNull(RS.Fields("quantity").Value), 0, RS.Fields("quantity").Value), "0.00")

                '            .Col = ColRate
                '            .Text = Format(IIf(IsNull(RS!Rate), 0, RS!Rate), "0.00")

                .Col = ColAmount
                .Text = VB6.Format(IIf(IsDbNull(RS.Fields("Amount").Value), 0, RS.Fields("Amount").Value), "0.00")

                .Col = ColTaxableAmount
                .Text = VB6.Format(IIf(IsDbNull(RS.Fields("taxableAmount").Value), 0, RS.Fields("taxableAmount").Value), "0.00")

                .Col = ColTaxRate
                .Text = VB6.Format(IIf(IsDbNull(RS.Fields("TAX_RATE").Value), 0, RS.Fields("TAX_RATE").Value), "0.00")

                .Col = ColCGST
                .Text = VB6.Format(IIf(IsDbNull(RS.Fields("CGST").Value), 0, RS.Fields("CGST").Value), "0.00")

                .Col = ColSGST
                .Text = VB6.Format(IIf(IsDbNull(RS.Fields("SGST").Value), 0, RS.Fields("SGST").Value), "0.00")

                .Col = ColIGST
                .Text = VB6.Format(IIf(IsDbNull(RS.Fields("IGST").Value), 0, RS.Fields("IGST").Value), "0.00")
                '
                '            RS.MoveNext
                '            If RS.EOF = False Then
                '                .MaxRows = .MaxRows + 1
                '                cntRow = cntRow + 1
                '            End If
            End If
        End With




        '    Call CalcRowTotal(SprdMain, ColQuantity, 1, ColQuantity, SprdMain.MaxRows - 1, SprdMain.MaxRows, ColQuantity)
        '    Call CalcRowTotal(SprdMain, ColAmount, 1, ColAmount, SprdMain.MaxRows - 1, SprdMain.MaxRows, ColAmount)
        '    Call CalcRowTotal(SprdMain, ColTaxableAmount, 1, ColTaxableAmount, SprdMain.MaxRows - 1, SprdMain.MaxRows, ColTaxableAmount)
        '    Call CalcRowTotal(SprdMain, ColCGST, 1, ColCGST, SprdMain.MaxRows - 1, SprdMain.MaxRows, ColCGST)
        '    Call CalcRowTotal(SprdMain, ColSGST, 1, ColSGST, SprdMain.MaxRows - 1, SprdMain.MaxRows, ColSGST)
        '    Call CalcRowTotal(SprdMain, ColIGST, 1, ColIGST, SprdMain.MaxRows - 1, SprdMain.MaxRows, ColIGST)

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQLOld() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mAccountCode As String
        Dim mItemCode As String
        Dim mShowAll As Boolean
        Dim mDivision As Double


        ''SELECT CLAUSE...


        If optType(0).Checked = True Then
            MakeSQLOld = MakeSQLOld & vbCrLf & " SELECT IH.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC, IH.VNO,IH.VDATE,  " & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, IH.HSNCODE, " & vbCrLf & " SUM(IH.ITEM_QTY) AS ITEM_QTY, '', " & vbCrLf & " SUM(ITEM_AMT) AS AMOUNT, SUM(GSTABLE_AMT) AS GSTABLE_AMT, " & vbCrLf & " SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT "
        Else

            MakeSQLOld = " SELECT '', '', " & vbCrLf & " '', ''," & vbCrLf & " '','', IH.HSNCODE, " & vbCrLf & " SUM(IH.ITEM_QTY) AS ITEM_QTY, '', " & vbCrLf & " SUM(ITEM_AMT) AS AMOUNT, SUM(GSTABLE_AMT) AS GSTABLE_AMT, " & vbCrLf & " SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT "



        End If

        ''FROM CLAUSE...
        MakeSQLOld = MakeSQLOld & vbCrLf & " FROM FIN_GST_POST_TRN IH, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST ITEMMST"

        ''WHERE CLAUSE...
        MakeSQLOld = MakeSQLOld & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQLOld = MakeSQLOld & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'"

        MakeSQLOld = MakeSQLOld & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=ITEMMST.COMPANY_CODE(+)" & vbCrLf & " AND IH.ITEM_CODE=ITEMMST.ITEM_CODE(+)"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLOld = MakeSQLOld & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLOld = MakeSQLOld & vbCrLf & "AND TRIM(ITEMMST.ITEM_SHORT_DESC)='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQLOld = MakeSQLOld & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If


        If lblInvoiceType.Text = "S" Then
            MakeSQLOld = MakeSQLOld & vbCrLf & "AND IH.GST_DC='D'"
            MakeSQLOld = MakeSQLOld & vbCrLf & " AND BOOKCODE IN (-2,-7,-13,-16,-17,-21)"
        Else
            MakeSQLOld = MakeSQLOld & vbCrLf & "AND IH.GST_DC='C'"
            MakeSQLOld = MakeSQLOld & vbCrLf & " AND BOOKCODE NOT IN (-2,-7,-13,-16,-17,-21)"
        End If


        '    MakeSQLOld = MakeSQLOld & vbCrLf & "AND IH.CANCELLED='N'"


        MakeSQLOld = MakeSQLOld & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '' GROUP BY CLAUSE
        If optType(0).Checked = True Then
            MakeSQLOld = MakeSQLOld & vbCrLf & " GROUP BY IH.ITEM_CODE , ITEMMST.ITEM_SHORT_DESC, IH.VNO,IH.VDATE,  " & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, IH.HSNCODE "
        Else
            MakeSQLOld = MakeSQLOld & vbCrLf & " GROUP BY IH.HSNCODE"
        End If

        ''ORDER BY CLAUSE...

        If optType(0).Checked = True Then
            MakeSQLOld = MakeSQLOld & vbCrLf & "ORDER BY IH.VNO,IH.VDATE, HSNCODE "
        Else
            MakeSQLOld = MakeSQLOld & vbCrLf & "ORDER BY HSNCODE"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQL() As String

        On Error GoTo CreateErr
        Dim pDateFrom As String
        Dim pDateTo As String
        Dim pCompanyCode As Integer


        pDateFrom = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        pDateTo = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        pCompanyCode = RsCompany.Fields("COMPANY_CODE").Value

        MakeSQL = " SELECT VNO, VDATE, ITEM_CODE, ITEM_NAME, UOM, " & vbCrLf & " BILL_NO, BILLDATE,  " & vbCrLf & " PARTYCODE, PARTYNAME,  HSNCODE, " & vbCrLf & " SUM(QUANTITY), " & vbCrLf & " RATE,  " & vbCrLf & " SUM(AMOUNT), " & vbCrLf & " SUM(TAXABLEAMOUNT), " & vbCrLf & " TAX_RATE, " & vbCrLf & " SUM(CGST),  " & vbCrLf & " SUM(SGST),  " & vbCrLf & " SUM(IGST) FROM (  "

        MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME, '' AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, '' AS HSNCODE, " & vbCrLf & " 0 AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " 0 AS AMOUNT,  " & vbCrLf & " 0 AS TAXABLEAMOUNT, " & vbCrLf & " 0 AS TAX_RATE, " & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " 0 AS IGST FROM DUAL IH "


        If lstInvoiceType.GetItemChecked(0) = True Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT  INVOICE_NUMBER, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, BUYER_NAME AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If

            MakeSQL = MakeSQL & vbCrLf & " FROM VWGSTR1_B2B IH   " & vbCrLf _
                & " WHERE IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),BUYER_NAME,ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(1) = True Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT  INVOICE_NUMBER AS VNO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') AS VDATE, '' AS ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, BUYER_NAME AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If
            MakeSQL = MakeSQL & vbCrLf & " FROM VWGSTR1_B2C IH   " & vbCrLf & " WHERE IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),BUYER_NAME,ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(2) = True Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT  '' AS VNO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME,'' AS UOM, " & vbCrLf & " '' AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, BUYER_NAME AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " 1 AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME, '' AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " 1 AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If

            MakeSQL = MakeSQL & vbCrLf & " FROM VWGSTR1_B2CS IH   " & vbCrLf & " WHERE IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY BUYER_NAME,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.HSN_SAC_CODE, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(8) = True Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT  INVOICE_NUMBER AS VNO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VNO, '' AS VDATE,  '' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If

            MakeSQL = MakeSQL & vbCrLf & " FROM VWGSTR1_EXPORT IH   " & vbCrLf & " WHERE IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY INVOICE_NUMBER , TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), ITEM_CODE,ITEM_DESCRIPTION, UNIT_OF_MEASUREMENT,IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If

            '        MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

        End If

        If lstInvoiceType.GetItemChecked(3) = True Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT VNO, TO_CHAR(VDATE,'DD/MM/YYYY') As VDATE,  ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT as UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, BUYER_NAME AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'C',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'C',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'C',-1,1)) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS IGST  "

            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'C',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'C',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'C',-1,1)) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS IGST  "
            End If

            MakeSQL = MakeSQL & vbCrLf & " FROM VWGSTR1_DNCN_REG IH   " & vbCrLf & " WHERE IH.NOTE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.NOTE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY VNO,TO_CHAR(VDATE,'DD/MM/YYYY'),ITEM_CODE,BUYER_NAME,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT, IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(4) = True Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT VNO, TO_CHAR(VDATE,'DD/MM/YYYY') As  VDATE,  ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, BUYER_NAME AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'C',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'C',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'C',-1,1)) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS IGST  "
            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VNO, '' AS VDATE,  '' AS ITEM_CODE, '' AS ITEM_NAME,UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'C',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'C',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'C',-1,1)) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS IGST  "
            End If

            MakeSQL = MakeSQL & vbCrLf & " FROM VWGSTR1_DNCN_UNREG IH   " & vbCrLf _
                & " WHERE IH.NOTE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.NOTE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY  VNO, TO_CHAR(VDATE,'DD/MM/YYYY'), INVOICE_NUMBER, ITEM_CODE,BUYER_NAME,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT, IH.HSN_SAC_CODE, IH.RATE"
            Else
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If

            '        MakeSQL = MakeSQL & vbCrLf & " UNION ALL"
        End If

        If lstInvoiceType.GetItemChecked(5) = True Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VNO, '' AS VDATE,  '' AS ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME,UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " ADVANCE_RECEIPT_NUMBER AS BILL_NO, TO_CHAR(ADVANCE_RECEIPT_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, BUYER_NAME AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VNO, '' AS VDATE,  '' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If


            MakeSQL = MakeSQL & vbCrLf & " FROM VWGSTR1_ADVANCE IH   " & vbCrLf & " WHERE IH.ADVANCE_RECEIPT_NUMBER>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.ADVANCE_RECEIPT_NUMBER<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY ADVANCE_RECEIPT_NUMBER , BUYER_NAME, ITEM_DESCRIPTION,TO_CHAR(ADVANCE_RECEIPT_DATE,'DD/MM/YYYY'),UNIT_OF_MEASUREMENT, IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(7) = True Then
            MakeSQL = MakeSQL & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VNO, '' AS VDATE,  '' AS ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME,UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " ADVANCE_RECEIPT_NUMBER AS BILL_NO, TO_CHAR(ADVANCE_RECEIPT_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, BUYER_NAME AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY* -1) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* -1 ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* -1) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT* -1) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT* -1) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT* -1) AS IGST  "
            Else
                MakeSQL = MakeSQL & vbCrLf & " SELECT '' AS VNO, '' AS VDATE,  '' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY* -1) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* -1 ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* -1) AS TAXABLEAMOUNT, " & vbCrLf & " TO_NUMBER(IH.RATE) AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT* -1) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT* -1) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT* -1) AS IGST  "
            End If

            MakeSQL = MakeSQL & vbCrLf & " FROM VWGSTR1_TAXPAID IH   " & vbCrLf & " WHERE IH.ADVANCE_RECEIPT_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.ADVANCE_RECEIPT_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY ADVANCE_RECEIPT_NUMBER ,TO_CHAR(ADVANCE_RECEIPT_DATE,'DD/MM/YYYY'),BUYER_NAME,ITEM_DESCRIPTION, UNIT_OF_MEASUREMENT,IH.HSN_SAC_CODE, IH.RATE"
            Else
                MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If


        MakeSQL = MakeSQL & vbCrLf & " ) GROUP BY VNO, VDATE, ITEM_CODE, ITEM_NAME,UOM," & vbCrLf & " BILL_NO, BILLDATE, PARTYCODE, PARTYNAME,  HSNCODE,  " & vbCrLf & " RATE, TAX_RATE ORDER BY HSNCODE"


        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        '    Resume
    End Function
    Private Function MakeSQLSummary() As String

        On Error GoTo CreateErr
        Dim pDateFrom As String
        Dim pDateTo As String
        Dim pCompanyCode As Integer


        pDateFrom = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        pDateTo = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        pCompanyCode = RsCompany.Fields("COMPANY_CODE").Value

        MakeSQLSummary = " SELECT " & vbCrLf & " SUM(QUANTITY) As QUANTITY, " & vbCrLf & " SUM(AMOUNT) AS AMOUNT, " & vbCrLf & " SUM(TAXABLEAMOUNT) AS TAXABLEAMOUNT,SUM(TAX_RATE) As TAX_RATE, " & vbCrLf & " SUM(CGST) AS CGST,  " & vbCrLf & " SUM(SGST) As SGST,  " & vbCrLf & " SUM(IGST) As IGST FROM (  "

        MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " 0 AS QUANTITY, " & vbCrLf & " 0 AS AMOUNT,  " & vbCrLf & " 0 AS TAXABLEAMOUNT, 0 AS TAX_RATE, " & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " 0 AS IGST FROM DUAL IH "


        If lstInvoiceType.GetItemChecked(0) = True Then
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT  " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If

            MakeSQLSummary = MakeSQLSummary & vbCrLf & " FROM VWGSTR1_B2B IH   " & vbCrLf _
                & " WHERE IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),BUYER_NAME,ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,IH.HSN_SAC_CODE "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(1) = True Then
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " FROM VWGSTR1_B2C IH   " & vbCrLf _
                & " WHERE IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),BUYER_NAME,ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,IH.HSN_SAC_CODE"
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(2) = True Then
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " 1 AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " 1 AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT,0 AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If

            MakeSQLSummary = MakeSQLSummary & vbCrLf & " FROM VWGSTR1_B2CS IH   " & vbCrLf _
                & " WHERE IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY BUYER_NAME,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),IH.HSN_SAC_CODE "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(8) = True Then
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.ItemOther_value) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If

            MakeSQLSummary = MakeSQLSummary & vbCrLf & " FROM VWGSTR1_EXPORT IH   " & vbCrLf _
                & " WHERE IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY INVOICE_NUMBER , TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), ITEM_CODE,ITEM_DESCRIPTION, UNIT_OF_MEASUREMENT,IH.HSN_SAC_CODE "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If

            '        MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

        End If

        If lstInvoiceType.GetItemChecked(3) = True Then
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'C',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'C',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'C',-1,1)) AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS IGST  "

            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'C',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'C',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'C',-1,1)) AS TAXABLEAMOUNT, 0 AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS IGST  "
            End If

            MakeSQLSummary = MakeSQLSummary & vbCrLf & " FROM VWGSTR1_DNCN_REG IH   " & vbCrLf _
                & " WHERE IH.NOTE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.NOTE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY VNO,TO_CHAR(VDATE,'DD/MM/YYYY'),ITEM_CODE,BUYER_NAME,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT, IH.HSN_SAC_CODE "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(4) = True Then
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'C',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'C',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'C',-1,1)) AS TAXABLEAMOUNT,0 AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS IGST  "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'C',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'C',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'C',-1,1)) AS TAXABLEAMOUNT,0 AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'C',-1,1)) AS IGST  "
            End If

            MakeSQLSummary = MakeSQLSummary & vbCrLf & " FROM VWGSTR1_DNCN_UNREG IH   " & vbCrLf _
                & " WHERE IH.NOTE_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.NOTE_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY  VNO, TO_CHAR(VDATE,'DD/MM/YYYY'), INVOICE_NUMBER, ITEM_CODE,BUYER_NAME,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'),ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT, IH.HSN_SAC_CODE "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If

            '        MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"
        End If

        If lstInvoiceType.GetItemChecked(5) = True Then
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT,0 AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT,0 AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "
            End If


            MakeSQLSummary = MakeSQLSummary & vbCrLf & " FROM VWGSTR1_ADVANCE IH   " & vbCrLf _
                & " WHERE IH.ADVANCE_RECEIPT_NUMBER>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.ADVANCE_RECEIPT_NUMBER<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY ADVANCE_RECEIPT_NUMBER , BUYER_NAME, ITEM_DESCRIPTION,TO_CHAR(ADVANCE_RECEIPT_DATE,'DD/MM/YYYY'),UNIT_OF_MEASUREMENT, IH.HSN_SAC_CODE "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(7) = True Then
            MakeSQLSummary = MakeSQLSummary & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY* -1) AS QUANTITY, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* -1 ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* -1) AS TAXABLEAMOUNT,0 AS TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT* -1) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT* -1) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT* -1) AS IGST  "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " SELECT " & vbCrLf & " SUM(IH.QUANTITY* -1) AS QUANTITY, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* -1 ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* -1) AS TAXABLEAMOUNT,0 AS TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT* -1) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT* -1) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT* -1) AS IGST  "
            End If

            MakeSQLSummary = MakeSQLSummary & vbCrLf & " FROM VWGSTR1_TAXPAID IH   " & vbCrLf _
                & " WHERE IH.ADVANCE_RECEIPT_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.ADVANCE_RECEIPT_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY ADVANCE_RECEIPT_NUMBER ,TO_CHAR(ADVANCE_RECEIPT_DATE,'DD/MM/YYYY'),BUYER_NAME,ITEM_DESCRIPTION, UNIT_OF_MEASUREMENT,IH.HSN_SAC_CODE "
            Else
                MakeSQLSummary = MakeSQLSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If


        MakeSQLSummary = MakeSQLSummary & vbCrLf & " ) "

        'GROUP BY VNO, VDATE, ITEM_CODE, ITEM_NAME,UOM," & vbCrLf _
        ''            & " BILL_NO, BILLDATE, PARTYCODE, PARTYNAME,  HSNCODE,  " & vbCrLf _
        ''            & " RATE ORDER BY HSNCODE"


        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        '    Resume
    End Function

    Private Function MakeSQLPurchase() As String

        On Error GoTo CreateErr
        Dim pDateFrom As String
        Dim pDateTo As String
        Dim pCompanyCode As Integer


        pDateFrom = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        pDateTo = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        pCompanyCode = RsCompany.Fields("COMPANY_CODE").Value

        MakeSQLPurchase = " SELECT VNO, VDATE, ITEM_CODE, ITEM_NAME, UOM, " & vbCrLf & " BILL_NO, BILLDATE,  " & vbCrLf & " PARTYCODE, PARTYNAME,  HSNCODE, " & vbCrLf & " SUM(QUANTITY) AS QUANTITY, " & vbCrLf & " RATE,  " & vbCrLf & " SUM(AMOUNT) AS AMOUNT, " & vbCrLf & " SUM(TAXABLEAMOUNT) As TAXABLEAMOUNT, TAX_RATE, " & vbCrLf & " SUM(CGST) As CGST,  " & vbCrLf & " SUM(SGST) As SGST,  " & vbCrLf & " SUM(IGST) IGST FROM (  "

        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME, '' AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, '' AS HSNCODE, " & vbCrLf & " 0 AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " 0 AS AMOUNT,  " & vbCrLf & " 0 AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " 0 AS IGST FROM DUAL IH "

        If lstInvoiceType.GetItemChecked(0) = True Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"

            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME,"

            End If
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAXABLE_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAXABLE_VALUE) AS TAXABLEAMOUNT, TO_NUMBER(IH.RATE) As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "


            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_B2B IH   " & vbCrLf _
                & " WHERE IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(VDATE,'DD/MM/YYYY'), SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE, UNIT_OF_MEASUREMENT, IH.RATE" '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(1) = True Then
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME,UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME,UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME,"
            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAXABLE_VALUE +  IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAXABLE_VALUE) AS TAXABLEAMOUNT, TO_NUMBER(IH.RATE) As TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_B2B_UR IH   " & vbCrLf & " WHERE IH.DOC_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.DOC_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(DOC_DATE,'DD/MM/YYYY'), SUPPLIER_NAME,ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(4) = True Then ''GSTR2_IMP_GOODS
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " BILL_ENTRY_NUMBER AS BILL_NO, TO_CHAR(BILL_ENTRY_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME,UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, "
            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " 1 AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.IGST_AMOUNT ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, TO_NUMBER(IH.RATE) As TAX_RATE, " & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "


            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_IMP_GOODS IH   " & vbCrLf & " WHERE IH.DOC_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.DOC_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If
            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(DOC_DATE,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,BILL_ENTRY_NUMBER,TO_CHAR(BILL_ENTRY_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(5) = True Then ''GSTR2_IMP_SERVICE
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE,'' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME,"
            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, TO_NUMBER(IH.RATE) As TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_IMP_SERVICE IH   " & vbCrLf _
                & " WHERE IH.DOC_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.DOC_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(DOC_DATE,'DD/MM/YYYY'), SUPPLIER_NAME,ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(6) = True Then ''GSTR2_ITC_REVERSAL
            '        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"
            '
            '        MakeSQLPurchase = MakeSQLPurchase & vbCrLf _
            ''                & " SELECT  '' AS ITEM_CODE, '' AS ITEM_NAME, " & vbCrLf _
            ''                & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf _
            ''                & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf _
            ''                & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf _
            ''                & " 0 AS RATE, " & vbCrLf _
            ''                & " SUM(IH.CGST_AMOUNT +  IH.SGST_AMOUNT + IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf _
            ''                & " 0 AS TAXABLEAMOUNT, " & vbCrLf _
            ''                & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf _
            ''                & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf _
            ''                & " SUM(IH.IGST_AMOUNT) AS IGST  "
            '
            '        MakeSQLPurchase = MakeSQLPurchase & vbCrLf _
            ''                & " FROM VWGSTR2_ITC_REVERSAL IH   " & vbCrLf _
            ''                & " WHERE IH.INVOICE_DATE>='" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "' " & vbCrLf _
            ''                & " AND IH.INVOICE_DATE<='" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "' " & vbCrLf _
            ''                & " AND IH.COMPANY_CODE=" & pCompanyCode & ""
            '
            '        If Trim(txtHSNCode.Text) <> "" Then
            '            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            '        End If
            '
            '        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE " '',IH.UNIT_OF_MEASUREMENT"

        End If

        If lstInvoiceType.GetItemChecked(7) = True Then ''GSTR2_NIL_RATED
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, TO_CHAR('','DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE,'' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME,"
            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.CGST_AMOUNT + IH.SGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, TO_NUMBER(IH.RATE) As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_NIL_RATED IH   " & vbCrLf _
                & " WHERE IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(VDATE,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT, IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(8) = True Then ''GSTR2_TAX_LIABILITY
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME,UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, "

            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.ADVANCE_AMOUNT +  IH.CGST_AMOUNT + IH.SGST_AMOUNT+ IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.ADVANCE_AMOUNT) AS TAXABLEAMOUNT, TO_NUMBER(IH.RATE) As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_TAX_LIABILITY IH   " & vbCrLf _
                & " WHERE IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(VDATE,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(9) = True Then ''GSTR2_TAX_PAID
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM, " & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE,'' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, "
            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM(IH.ADVANCE_AMOUNT +  IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.ADVANCE_AMOUNT) AS TAXABLEAMOUNT, TO_NUMBER(IH.RATE) As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_TAX_PAID IH   " & vbCrLf _
                & " WHERE IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(VDATE,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(2) = True Then ''GSTR2_CRDR_REG
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(FINANCIAL_PERIOD,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT '' AS VNO, '' AS VDATE, '' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, "
            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'D',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'D',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'D',-1,1)) AS TAXABLEAMOUNT,TO_NUMBER(IH.RATE) As TAX_RATE, " & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'D',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'D',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'D',-1,1)) AS IGST  "

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_CRDR_REG IH   " & vbCrLf _
                & " WHERE IH.FINANCIAL_PERIOD>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.FINANCIAL_PERIOD<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If
            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(FINANCIAL_PERIOD,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(3) = True Then ''GSTR2_CRDR_UNREG
            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " UNION ALL"

            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  V_NO AS VNO, TO_CHAR(FINANCIAL_PERIOD,'DD/MM/YYYY') AS VDATE, ITEM_CODE, ITEM_DESCRIPTION AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " INVOICE_NUMBER AS BILL_NO, TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, SUPPLIER_NAME AS PARTYNAME,"
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " SELECT  '' AS VNO, '' AS VDATE,'' AS ITEM_CODE, '' AS ITEM_NAME, UNIT_OF_MEASUREMENT AS UOM," & vbCrLf & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf & " '' AS PARTYCODE, '' AS PARTYNAME, "
            End If

            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf & " SUM(IH.QUANTITY * DECODE(NTTY,'D',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " 0 AS RATE, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NTTY,'D',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NTTY,'D',-1,1)) AS TAXABLEAMOUNT, TO_NUMBER(IH.RATE) As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NTTY,'D',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NTTY,'D',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NTTY,'D',-1,1)) AS IGST  "


            MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " FROM VWGSTR2_CRDR_UNREG IH   " & vbCrLf & " WHERE IH.FINANCIAL_PERIOD>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.FINANCIAL_PERIOD<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If
            If optType(0).Checked = True Then
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY V_NO, TO_CHAR(FINANCIAL_PERIOD,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE, IH.RATE "
            Else
                MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT, IH.RATE " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If


        MakeSQLPurchase = MakeSQLPurchase & vbCrLf & " ) GROUP BY VNO, VDATE, ITEM_CODE, ITEM_NAME, UOM, " & vbCrLf & " BILL_NO, BILLDATE, PARTYCODE, PARTYNAME,  HSNCODE,  " & vbCrLf & " RATE,TAX_RATE ORDER BY HSNCODE"


        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        '    Resume
    End Function

    Private Function MakeSQLPurchaseSummary() As String

        On Error GoTo CreateErr
        Dim pDateFrom As String
        Dim pDateTo As String
        Dim pCompanyCode As Integer


        pDateFrom = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY")
        pDateTo = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        pCompanyCode = RsCompany.Fields("COMPANY_CODE").Value

        MakeSQLPurchaseSummary = " SELECT " & vbCrLf & " SUM(QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(AMOUNT) AS AMOUNT, " & vbCrLf & " SUM(TAXABLEAMOUNT) As TAXABLEAMOUNT, SUM(TAX_RATE) AS TAX_RATE, " & vbCrLf & " SUM(CGST) As CGST,  " & vbCrLf & " SUM(SGST) As SGST,  " & vbCrLf & " SUM(IGST) IGST FROM (  "

        MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT " & vbCrLf & " 0 AS QUANTITY, " & vbCrLf & " 0 AS AMOUNT,  " & vbCrLf & " 0 AS TAXABLEAMOUNT, 0 AS TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " 0 AS IGST FROM DUAL IH "

        If lstInvoiceType.GetItemChecked(0) = True Then
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAXABLE_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAXABLE_VALUE) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "


            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_B2B IH   " & vbCrLf & " WHERE IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(VDATE,'DD/MM/YYYY'), SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE, UNIT_OF_MEASUREMENT" '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(1) = True Then
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAXABLE_VALUE +  IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAXABLE_VALUE) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_B2B_UR IH   " & vbCrLf & " WHERE IH.DOC_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.DOC_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(DOC_DATE,'DD/MM/YYYY'), SUPPLIER_NAME,ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(4) = True Then ''GSTR2_IMP_GOODS
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT 1 AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE + IH.IGST_AMOUNT ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "


            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_IMP_GOODS IH   " & vbCrLf & " WHERE IH.DOC_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.DOC_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If
            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(DOC_DATE,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,BILL_ENTRY_NUMBER,TO_CHAR(BILL_ENTRY_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(5) = True Then ''GSTR2_IMP_SERVICE
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"


            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " 0 AS CGST,  " & vbCrLf & " 0 AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_IMP_SERVICE IH   " & vbCrLf _
                & " WHERE IH.DOC_DATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.DOC_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(DOC_DATE,'DD/MM/YYYY'), SUPPLIER_NAME,ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(6) = True Then ''GSTR2_ITC_REVERSAL
            '        MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"
            '
            '        MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf _
            ''                & " SELECT  '' AS ITEM_CODE, '' AS ITEM_NAME, " & vbCrLf _
            ''                & " '' AS BILL_NO, '' As BILLDATE, " & vbCrLf _
            ''                & " '' AS PARTYCODE, '' AS PARTYNAME, IH.HSN_SAC_CODE AS HSNCODE, " & vbCrLf _
            ''                & " SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf _
            ''                & " 0 AS RATE, " & vbCrLf _
            ''                & " SUM(IH.CGST_AMOUNT +  IH.SGST_AMOUNT + IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf _
            ''                & " 0 AS TAXABLEAMOUNT, " & vbCrLf _
            ''                & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf _
            ''                & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf _
            ''                & " SUM(IH.IGST_AMOUNT) AS IGST  "
            '
            '        MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf _
            ''                & " FROM VWGSTR2_ITC_REVERSAL IH   " & vbCrLf _
            ''                & " WHERE IH.INVOICE_DATE>='" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "' " & vbCrLf _
            ''                & " AND IH.INVOICE_DATE<='" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "' " & vbCrLf _
            ''                & " AND IH.COMPANY_CODE=" & pCompanyCode & ""
            '
            '        If Trim(txtHSNCode.Text) <> "" Then
            '            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            '        End If
            '
            '        MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE " '',IH.UNIT_OF_MEASUREMENT"

        End If

        If lstInvoiceType.GetItemChecked(7) = True Then ''GSTR2_NIL_RATED
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.TAX_VALUE +  IH.IGST_AMOUNT + IH.CGST_AMOUNT + IH.SGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_NIL_RATED IH   " & vbCrLf _
                & " WHERE IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(VDATE,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT, IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(8) = True Then ''GSTR2_TAX_LIABILITY
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.ADVANCE_AMOUNT +  IH.CGST_AMOUNT + IH.SGST_AMOUNT+ IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.ADVANCE_AMOUNT) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_TAX_LIABILITY IH   " & vbCrLf _
                & " WHERE IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(VDATE,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(9) = True Then ''GSTR2_TAX_PAID
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT SUM(IH.QUANTITY) AS QUANTITY, " & vbCrLf & " SUM(IH.ADVANCE_AMOUNT +  IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT) AS AMOUNT,  " & vbCrLf & " SUM(IH.ADVANCE_AMOUNT) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT) AS IGST  "

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_TAX_PAID IH   " & vbCrLf _
                & " WHERE IH.VDATE>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If

            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(VDATE,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(2) = True Then ''GSTR2_CRDR_REG
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT SUM(IH.QUANTITY * DECODE(NOTE_TYPE,'D',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NOTE_TYPE,'D',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NOTE_TYPE,'D',-1,1)) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NOTE_TYPE,'D',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NOTE_TYPE,'D',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NOTE_TYPE,'D',-1,1)) AS IGST  "

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_CRDR_REG IH   " & vbCrLf _
                & " WHERE IH.FINANCIAL_PERIOD>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.FINANCIAL_PERIOD<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If
            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(FINANCIAL_PERIOD,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If

        If lstInvoiceType.GetItemChecked(3) = True Then ''GSTR2_CRDR_UNREG
            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " UNION ALL"

            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " SELECT SUM(IH.QUANTITY * DECODE(NTTY,'D',-1,1) * DECODE(SUBSTR(REASON ,1,2),'07',0,1)) AS QUANTITY, " & vbCrLf & " SUM((IH.TAX_VALUE + IH.CGST_AMOUNT + IH.SGST_AMOUNT + IH.IGST_AMOUNT)* DECODE(NTTY,'D',-1,1) ) AS AMOUNT,  " & vbCrLf & " SUM(IH.TAX_VALUE* DECODE(NTTY,'D',-1,1)) AS TAXABLEAMOUNT, 0 As TAX_RATE," & vbCrLf & " SUM(IH.CGST_AMOUNT * DECODE(NTTY,'D',-1,1)) AS CGST,  " & vbCrLf & " SUM(IH.SGST_AMOUNT * DECODE(NTTY,'D',-1,1)) AS SGST,  " & vbCrLf & " SUM(IH.IGST_AMOUNT * DECODE(NTTY,'D',-1,1)) AS IGST  "


            MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " FROM VWGSTR2_CRDR_UNREG IH   " & vbCrLf _
                & " WHERE IH.FINANCIAL_PERIOD>=TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.FINANCIAL_PERIOD<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.COMPANY_CODE=" & pCompanyCode & ""

            If Trim(txtHSNCode.Text) <> "" Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " AND IH.HSN_SAC_CODE='" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "'"
            End If
            If optType(0).Checked = True Then
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY V_NO, TO_CHAR(FINANCIAL_PERIOD,'DD/MM/YYYY'),SUPPLIER_NAME, ITEM_CODE, ITEM_DESCRIPTION,UNIT_OF_MEASUREMENT,INVOICE_NUMBER,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY'), IH.HSN_SAC_CODE "
            Else
                MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " GROUP BY IH.HSN_SAC_CODE,UNIT_OF_MEASUREMENT " '',IH.UNIT_OF_MEASUREMENT"
            End If
        End If


        MakeSQLPurchaseSummary = MakeSQLPurchaseSummary & vbCrLf & " )"

        '            GROUP BY VNO, VDATE, ITEM_CODE, ITEM_NAME, UOM, " & vbCrLf _
        ''            & " BILL_NO, BILLDATE, PARTYCODE, PARTYNAME,  HSNCODE,  " & vbCrLf _
        ''            & " RATE ORDER BY HSNCODE"


        Exit Function
CreateErr:
        '    Resume
        MsgInformation(Err.Description)
        '    Resume
    End Function
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        lstInvoiceType.Items.Clear()
        If lblInvoiceType.Text = "S" Then
            lstInvoiceType.Items.Add("GSTR1_B2B")
            lstInvoiceType.SetItemChecked(0, True)

            lstInvoiceType.Items.Add("GSTR1_B2C")
            lstInvoiceType.SetItemChecked(1, True)

            lstInvoiceType.Items.Add("GSTR1_B2CS")
            lstInvoiceType.SetItemChecked(2, True)

            lstInvoiceType.Items.Add("GSTR1_DNCN_REG")
            lstInvoiceType.SetItemChecked(3, True)

            lstInvoiceType.Items.Add("GSTR1_DNCN_UNREG")
            lstInvoiceType.SetItemChecked(4, True)

            lstInvoiceType.Items.Add("GSTR1_ADVANCE")
            lstInvoiceType.SetItemChecked(5, True)

            lstInvoiceType.Items.Add("GSTR1_NILRATE")
            lstInvoiceType.SetItemChecked(6, True)

            lstInvoiceType.Items.Add("GSTR1_TAXPAID")
            lstInvoiceType.SetItemChecked(7, True)

            lstInvoiceType.Items.Add("GSTR1_EXPORT")
            lstInvoiceType.SetItemChecked(8, True)
        Else
            lstInvoiceType.Items.Add("GSTR2_B2B")
            lstInvoiceType.SetItemChecked(0, True)

            lstInvoiceType.Items.Add("GSTR2_B2B_UR")
            lstInvoiceType.SetItemChecked(1, True)

            lstInvoiceType.Items.Add("GSTR2_CRDR_REG")
            lstInvoiceType.SetItemChecked(2, True)

            lstInvoiceType.Items.Add("GSTR2_CRDR_UNREG")
            lstInvoiceType.SetItemChecked(3, True)

            lstInvoiceType.Items.Add("GSTR2_IMP_GOODS")
            lstInvoiceType.SetItemChecked(4, True)

            lstInvoiceType.Items.Add("GSTR2_IMP_SERVICE")
            lstInvoiceType.SetItemChecked(5, True)

            lstInvoiceType.Items.Add("GSTR2_ITC_REVERSAL")
            lstInvoiceType.SetItemChecked(6, True)

            lstInvoiceType.Items.Add("GSTR2_NIL_RATED")
            lstInvoiceType.SetItemChecked(7, True)

            lstInvoiceType.Items.Add("GSTR2_TAX_LIABILITY")
            lstInvoiceType.SetItemChecked(8, True)

            lstInvoiceType.Items.Add("GSTR2_TAX_PAID")
            lstInvoiceType.SetItemChecked(9, True)
        End If

        lstInvoiceType.SelectedIndex = 0


        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub


    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
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

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        SearchItem()
    End Sub


    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub


    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
            MsgInformation("No Such Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then
        '        txtDateFrom.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
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
End Class
