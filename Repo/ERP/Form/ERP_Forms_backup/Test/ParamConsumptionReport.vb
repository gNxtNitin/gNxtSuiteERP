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

Friend Class frmConsumptionReport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Private PvtDBCn As ADODB.Connection				

    Dim mAccountCode As Integer
    Private Const ColLocked As Short = 1
    Private Const ColCatgeory As Short = 2
    Private Const ColCatgeoryDesc As Short = 3
    Private Const ColAccountPostingHead As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColItemName As Short = 6
    Private Const ColItemUOM As Short = 7
    Private Const ColOpeningQty As Short = 8
    Private Const ColOpeningValue As Short = 9
    Private Const ColPurchaseQty As Short = 10
    Private Const ColPurchaseValue As Short = 11
    Private Const ColPurchaseSuppQty As Short = 12
    Private Const ColPurchaseSuppValue As Short = 13
    Private Const ColPurchaseDebitQty As Short = 14
    Private Const ColPurchaseDebitValue As Short = 15

    Private Const ColPurchaseCreditQty As Short = 16
    Private Const ColPurchaseCreditValue As Short = 17

    Private Const ColSaleQty As Short = 18
    Private Const ColSaleValue As Short = 19

    Private Const ColSaleDebitQty As Short = 20
    Private Const ColSaleDebitValue As Short = 21

    Private Const ColSaleCreditQty As Short = 22
    Private Const ColSaleCreditValue As Short = 23

    Private Const ColJVValue As Short = 24

    Private Const ColClosingQty As Short = 25
    Private Const ColClosingValue As Short = 26

    Private Const ColMkey As Short = 27

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
        Me.Hide()
        Me.Dispose()
        Me.Close()
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
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4				
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmConsumptionReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmConsumptionReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        Call FillInvoiceType()
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub FillInvoiceType()
        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer
        Dim ds As New DataSet
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter

        lstInvoiceType.Items.Clear()
        'SqlStr = "SELECT DISTINCT CMST.SUPP_CUST_NAME " & vbCrLf _
        '    & " FROM FIN_INVTYPE_MST A, FIN_SUPP_CUST_MST CMST" & vbCrLf _
        '    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND A.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
        '    & " AND A.ACCOUNTPOSTCODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        '    & " AND CATEGORY IN ('S','P') ORDER BY CMST.SUPP_CUST_NAME"

        SqlStr = "SELECT DISTINCT A.GEN_DESC " & vbCrLf _
            & " FROM INV_GENERAL_MST A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'" & vbCrLf _
            & " ORDER BY A.GEN_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstInvoiceType.SetItemChecked(CntLst, False) '' True				
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0


        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If


        lstAccountMapping.Items.Clear()
        SqlStr = "SELECT DISTINCT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST A, INV_GENERAL_MST B" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.SUPP_CUST_CODE=B.ACCT_CONSUM_CODE" & vbCrLf _
            & " AND GEN_TYPE='C' ORDER BY SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstAccountMapping.Items.Add("ALL")
            lstAccountMapping.SetItemChecked(CntLst, True)
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstAccountMapping.Items.Add(RS.Fields("SUPP_CUST_NAME").Value)
                lstAccountMapping.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        oledbCnn = New OleDbConnection(StrConn)


        SqlStr = " Select 'STORE' AS WARE_HOUSE , 'WH' AS STOCK_ID  FROM DUAL " & vbCrLf _
            & " UNION ALL" & vbCrLf _
            & " Select 'PRODUCTION' AS WARE_HOUSE, 'PH' AS STOCK_ID FROM DUAL" & vbCrLf _
            & " UNION ALL" & vbCrLf _
            & " Select 'SUB STORE' AS WARE_HOUSE, 'SH' AS STOCK_ID FROM DUAL"

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboExportItem.DataSource = ds
        cboExportItem.DataMember = ""
        Dim c As UltraGridColumn = Me.cboExportItem.DisplayLayout.Bands(0).Columns.Add()
        With c
            .Key = "Selected"
            .Header.Caption = String.Empty
            .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
            .DataType = GetType(Boolean)
            .DataType = GetType(Boolean)
            .Header.VisiblePosition = 0
        End With
        cboExportItem.CheckedListSettings.CheckStateMember = "Selected"
        cboExportItem.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        ' Set up the control to use a custom list delimiter 
        cboExportItem.CheckedListSettings.ListSeparator = " , "
        ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        cboExportItem.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        cboExportItem.DisplayMember = "WARE_HOUSE"
        cboExportItem.ValueMember = "STOCK_ID"

        cboExportItem.DisplayLayout.Bands(0).Columns(0).Header.Caption = "WareHouse"
        cboExportItem.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Stock ID"

        cboExportItem.DisplayLayout.Bands(0).Columns(0).Width = 100
        cboExportItem.DisplayLayout.Bands(0).Columns(1).Width = 50

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            cboExportItem.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown
        Else
            cboExportItem.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown
            'cboExportItem.CheckedRows
        End If


        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmConsumptionReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmConsumptionReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()

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

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr				
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
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
        Dim SqlStr As String


        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
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
            .MaxCols = ColMkey
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

            .Col = ColCatgeory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColCatgeory, 10)
            .ColHidden = True

            .Col = ColCatgeoryDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColCatgeoryDesc, 20)

            .Col = ColAccountPostingHead
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_RowHeight(Arow, RowHeight)
            .set_ColWidth(ColAccountPostingHead, 20)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 15)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            For cntCol = ColOpeningQty To ColClosingValue
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(.Col, 9)
            Next

            .Col = ColMkey
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

    Private Function MakeSQL() As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mCond As String
        Dim mItemCode As String
        Dim mHavingClause As Boolean
        Dim mTableName As String
        Dim mToDate As String

        Dim mDivision As Double

        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""

        Dim mCompanyCodeStr As String = ""
        Dim mStockTypeStr As String = ""
        Dim mStockType As String = ""


        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        Dim mWareHouse As String


        If cboExportItem.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboExportItem.CheckedRows
                If mWareHouse <> "" Then
                    mWareHouse += "," & "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
                Else
                    mWareHouse += "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
                End If
            Next
        End If


        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',  INV.COMPANY_CODE, " & vbCrLf _
            & " NVL(ITEM.CATEGORY_CODE,'-'), NVL(GMST.GEN_DESC,'-'), '' SUBCATEGORY_CODE, '' SUBCATEGORY_DESC,  INV.ITEM_CODE, " & vbCrLf _
            & " NVL(ITEM.ITEM_SHORT_DESC,'-'), NVL(ITEM.ISSUE_UOM,'-') AS ITEM_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR, ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY, "

        'SqlStr = SqlStr & vbCrLf _
        '    & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
        '    & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, "

        SqlStr = SqlStr & vbCrLf _
            & " NVL((SELECT SUM(A.ITEM_QTY * DECODE(A.ITEM_IO,'I',1,-1)) " & vbCrLf _
            & " FROM INV_STOCK_REC_TRN A, INV_ITEM_MST B, INV_GENERAL_MST C" & vbCrLf _
            & " WHERE A.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
            & " AND A.FYEAR=INV.FYEAR" & vbCrLf _
            & " AND A.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ITEM_CODE=B.ITEM_CODE" & vbCrLf _
            & " AND B.COMPANY_CODE=C.COMPANY_CODE" & vbCrLf _
            & " AND B.CATEGORY_CODE=C.GEN_CODE AND C.GEN_TYPE='C'"

        If mWareHouse = "" Then
            SqlStr = SqlStr & vbCrLf & " AND A.STOCK_ID = 'WH'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND A.STOCK_ID IN (" & mWareHouse & ")"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND STATUS='O' AND C.ACCT_CONSUM_CODE=INV.ACCOUNTCODE" & vbCrLf _
            & " AND A.E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ),0) AS Opening, "



        'SqlStr = SqlStr & vbCrLf _
        '    & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE,SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS OpeningValue, "

        SqlStr = SqlStr & vbCrLf & " 0 AS OpeningValue, "

        SqlStr = SqlStr & vbCrLf & " 0 AS MRRQty, "


        SqlStr = SqlStr & vbCrLf & " 0 AS MRRValue, "


        SqlStr = SqlStr & vbCrLf & " 0 AS Receipt, "

        SqlStr = SqlStr & vbCrLf & " 0 as ReceiptValue,"

        SqlStr = SqlStr & vbCrLf & " 0 AS Issue, " ''AND STOCK_TYPE IN ('ST','CS','FG')

        SqlStr = SqlStr & vbCrLf & " 0 as ISSUEValue,"

        'SqlStr = SqlStr & vbCrLf _
        '    & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Closing, " & vbCrLf _
        '    & " 0 AS Rejection, " & vbCrLf _
        '    & " 0 AS UnderQC, " & vbCrLf _
        '    & " 0  as DEPT_QTY, " & vbCrLf _
        '    & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))  as TotClosing, " & vbCrLf _
        '    & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, 1) as Rate, " & vbCrLf _
        '    & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Value,"

        'SqlStr = SqlStr & vbCrLf _
        '    & " NVL((SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) " & vbCrLf _
        '    & " FROM INV_STOCK_REC_TRN " & vbCrLf _
        '    & " WHERE COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
        '    & " AND FYEAR=INV.FYEAR" & vbCrLf _
        '    & " AND ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
        '    & " AND STOCK_ID IN ('WH','PH','SH') AND STATUS='O'" & vbCrLf _
        '    & " AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '    & " ),0) AS Closing, "

        SqlStr = SqlStr & vbCrLf _
            & " 0 AS Closing,0 AS Rejection, " & vbCrLf _
            & " 0 AS UnderQC, " & vbCrLf _
            & " 0  as DEPT_QTY, "

        'SqlStr = SqlStr & vbCrLf _
        '    & " NVL((SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) " & vbCrLf _
        '    & " FROM INV_STOCK_REC_TRN " & vbCrLf _
        '    & " WHERE COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
        '    & " AND FYEAR=INV.FYEAR" & vbCrLf _
        '    & " AND ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
        '    & " AND STOCK_ID IN ('WH','PH','SH') AND STATUS='O'" & vbCrLf _
        '    & " AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '    & " ),0) AS TotClosing, "

        SqlStr = SqlStr & vbCrLf _
            & " NVL((SELECT SUM(A.ITEM_QTY * DECODE(A.ITEM_IO,'I',1,-1)) " & vbCrLf _
            & " FROM INV_STOCK_REC_TRN A, INV_ITEM_MST B, INV_GENERAL_MST C" & vbCrLf _
            & " WHERE A.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
            & " AND A.FYEAR=INV.FYEAR" & vbCrLf _
            & " AND A.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ITEM_CODE=B.ITEM_CODE" & vbCrLf _
            & " AND B.COMPANY_CODE=C.COMPANY_CODE" & vbCrLf _
            & " AND B.CATEGORY_CODE=C.GEN_CODE AND C.GEN_TYPE='C'"

        If mWareHouse = "" Then
            SqlStr = SqlStr & vbCrLf & " AND A.STOCK_ID = 'WH'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND A.STOCK_ID IN (" & mWareHouse & ")"
        End If


        SqlStr = SqlStr & vbCrLf _
            & " AND STATUS='O' AND C.ACCT_CONSUM_CODE=INV.ACCOUNTCODE" & vbCrLf _
            & " AND A.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ),0) AS TotClosing, "

        SqlStr = SqlStr & vbCrLf _
            & " 0 as Rate, " & vbCrLf _
            & " 0 as Value,"


        SqlStr = SqlStr & vbCrLf _
            & " 0 as ConQty, " & vbCrLf _
            & " 0 as ConValue,"

        SqlStr = SqlStr & vbCrLf _
            & " 0 as OtherQty, " & vbCrLf _
            & " 0 as OtherValue,"

        SqlStr = SqlStr & vbCrLf _
            & " 0 as ASSETS_VALUE," & vbCrLf _
            & " '' , ACCT.SUPP_CUST_NAME AS LEDGER_HEAD" & vbCrLf _
            & " , '' AS LEDGER_AMT , 0 AS CLOSING_NOS,"

        SqlStr = SqlStr & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='P' THEN INV.ITEM_QTY ELSE 0 END) PUR_QTY," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='P' THEN INV.ITEM_AMOUNT ELSE 0 END) PUR_VALUE," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='U' THEN INV.ITEM_QTY ELSE 0 END) PUR_SUPP_QTY," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='U' THEN INV.ITEM_AMOUNT ELSE 0 END) PUR_SUPP_VALUE," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='E' THEN INV.ITEM_QTY ELSE 0 END) PUR_DN_QTY," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='E' THEN INV.ITEM_AMOUNT ELSE 0 END) PUR_DN_VALUE," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='R' THEN INV.ITEM_QTY ELSE 0 END) PUR_CN_QTY," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='R' THEN INV.ITEM_AMOUNT ELSE 0 END) PUR_CN_VALUE," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='S' THEN INV.ITEM_QTY ELSE 0 END) SALE_QTY," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='S' THEN INV.ITEM_AMOUNT ELSE 0 END) SALE_VALUE," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='SD' OR INV.BOOKTYPE='M' THEN INV.ITEM_QTY ELSE 0 END) SALE_DN_QTY," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='SD' OR INV.BOOKTYPE='M' THEN INV.ITEM_AMOUNT ELSE 0 END) SALE_DN_VALUE," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='L' THEN INV.ITEM_QTY ELSE 0 END) SALE_CN_QTY," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='L' THEN INV.ITEM_AMOUNT ELSE 0 END) SALE_CN_VALUE," & vbCrLf _
                & " SUM(CASE WHEN INV.BOOKTYPE='J' THEN INV.ITEM_AMOUNT ELSE 0 END) JV_VALUE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM vwITEMCONSUMPTION INV, " & vbCrLf _
            & " INV_ITEM_MST ITEM, INV_GENERAL_MST GMST, FIN_SUPP_CUST_MST ACCT "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " WHERE INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE(+)" & vbCrLf _
            & " AND INV.ITEM_CODE=ITEM.ITEM_CODE(+)"

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=GMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE(+) "

        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ACCT.COMPANY_CODE(+)" & vbCrLf _
            & " AND INV.ACCOUNTCODE=ACCT.SUPP_CUST_CODE(+) "


        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If


        If lstInvoiceType.GetItemChecked(0) = True Then
            mRMCatCodeStr = ""
        Else
            For CntLst = 1 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    mMaterialType = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
                End If
            Next
        End If

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If


        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mAccountCodeStr As String

        If lstAccountMapping.GetItemChecked(0) = True Then
            mAccountCodeStr = ""
        Else
            For CntLst = 1 To lstAccountMapping.Items.Count - 1
                If lstAccountMapping.GetItemChecked(CntLst) = True Then
                    mAccountName = VB6.GetItemString(lstAccountMapping, CntLst)
                    If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", "'" & mAccountCode & "'", mAccountCodeStr & "," & "'" & mAccountCode & "'")
                End If
            Next
        End If

        If mAccountCodeStr <> "" Then
            mAccountCodeStr = "(" & mAccountCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND INV.ACCOUNTCODE IN " & mAccountCodeStr & ""
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
            SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        '' SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            SqlStr = SqlStr & vbCrLf & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If


        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
            & " INV.COMPANY_CODE, INV.FYEAR, ITEM.CATEGORY_CODE, GMST.GEN_DESC, INV.ACCOUNTCODE, ACCT.SUPP_CUST_NAME, INV.ITEM_CODE, " & vbCrLf _
            & " ITEM.ITEM_SHORT_DESC, ITEM.ISSUE_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR, ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY "

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf _
            & "  INV.COMPANY_CODE, ITEM.CATEGORY_CODE, INV.ITEM_CODE "


        MakeSQL = SqlStr
        Exit Function
InsertErr:
        MakeSQL = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function MakeSQLOTH() As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mCond As String
        Dim mItemCode As String
        Dim mHavingClause As Boolean
        Dim mTableName As String
        Dim mToDate As String

        Dim mDivision As Double

        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""

        Dim mCompanyCodeStr As String = ""
        Dim mStockTypeStr As String = ""
        Dim mStockType As String = ""


        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable

        Dim mWareHouse As String


        If cboExportItem.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboExportItem.CheckedRows
                If mWareHouse <> "" Then
                    mWareHouse += "," & "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
                Else
                    mWareHouse += "'" & r.Cells("STOCK_ID").Value.ToString() & "'"
                End If
            Next
        End If

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',  ITEM.COMPANY_CODE, " & vbCrLf _
            & " NVL(ITEM.CATEGORY_CODE,'-'), NVL(GMST.GEN_DESC,'-'), '' SUBCATEGORY_CODE, '' SUBCATEGORY_DESC,  ITEM.ITEM_CODE, " & vbCrLf _
            & " NVL(ITEM.ITEM_SHORT_DESC,'-'), NVL(ITEM.ISSUE_UOM,'-') AS ITEM_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR, ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY, "

        SqlStr = SqlStr & vbCrLf _
            & " NVL((SELECT SUM(A.ITEM_QTY * DECODE(A.ITEM_IO,'I',1,-1)) " & vbCrLf _
            & " FROM INV_STOCK_REC_TRN A, INV_ITEM_MST B, INV_GENERAL_MST C" & vbCrLf _
            & " WHERE A.COMPANY_CODE = ITEM.COMPANY_CODE" & vbCrLf _
            & " AND A.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND A.ITEM_CODE=ITEM.ITEM_CODE" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ITEM_CODE=B.ITEM_CODE" & vbCrLf _
            & " AND B.COMPANY_CODE=C.COMPANY_CODE" & vbCrLf _
            & " AND B.CATEGORY_CODE=C.GEN_CODE AND C.GEN_TYPE='C'"

        If mWareHouse = "" Then
            SqlStr = SqlStr & vbCrLf & " AND A.STOCK_ID = 'WH'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND A.STOCK_ID IN (" & mWareHouse & ")"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND STATUS='O' " & vbCrLf _
            & " AND A.E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ),0) AS Opening, "

        SqlStr = SqlStr & vbCrLf & " 0 AS OpeningValue, "

        SqlStr = SqlStr & vbCrLf & " 0 AS MRRQty, "


        SqlStr = SqlStr & vbCrLf & " 0 AS MRRValue, "


        SqlStr = SqlStr & vbCrLf & " 0 AS Receipt, "

        SqlStr = SqlStr & vbCrLf & " 0 as ReceiptValue,"

        SqlStr = SqlStr & vbCrLf & " 0 AS Issue, " ''AND STOCK_TYPE IN ('ST','CS','FG')

        SqlStr = SqlStr & vbCrLf & " 0 as ISSUEValue,"

        'SqlStr = SqlStr & vbCrLf _
        '    & " NVL((SELECT SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1)) " & vbCrLf _
        '    & " FROM INV_STOCK_REC_TRN " & vbCrLf _
        '    & " WHERE COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
        '    & " AND FYEAR=INV.FYEAR" & vbCrLf _
        '    & " AND ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
        '    & " AND STOCK_ID IN ('WH','PH','SH') AND STATUS='O'" & vbCrLf _
        '    & " AND E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        '    & " ),0) AS Closing, "

        SqlStr = SqlStr & vbCrLf _
            & " 0 AS Closing, 0 AS Rejection, " & vbCrLf _
            & " 0 AS UnderQC, " & vbCrLf _
            & " 0  as DEPT_QTY, "

        SqlStr = SqlStr & vbCrLf _
            & " NVL((SELECT SUM(A.ITEM_QTY * DECODE(A.ITEM_IO,'I',1,-1)) " & vbCrLf _
            & " FROM INV_STOCK_REC_TRN A, INV_ITEM_MST B, INV_GENERAL_MST C" & vbCrLf _
            & " WHERE A.COMPANY_CODE = ITEM.COMPANY_CODE" & vbCrLf _
            & " AND A.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND A.ITEM_CODE=ITEM.ITEM_CODE" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.ITEM_CODE=B.ITEM_CODE" & vbCrLf _
            & " AND B.COMPANY_CODE=C.COMPANY_CODE" & vbCrLf _
            & " AND B.CATEGORY_CODE=C.GEN_CODE AND C.GEN_TYPE='C'"

        If mWareHouse = "" Then
            SqlStr = SqlStr & vbCrLf & " AND A.STOCK_ID = 'WH'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND A.STOCK_ID IN (" & mWareHouse & ")"
        End If

        ''AND A.STOCK_ID IN ('WH','PH','SH')

        SqlStr = SqlStr & vbCrLf _
            & " AND STATUS='O' " & vbCrLf _
            & " AND A.E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ),0) AS TotClosing, "

        SqlStr = SqlStr & vbCrLf _
            & " 0 as Rate, " & vbCrLf _
            & " 0 as Value,"


        SqlStr = SqlStr & vbCrLf _
            & " 0 as ConQty, " & vbCrLf _
            & " 0 as ConValue,"

        SqlStr = SqlStr & vbCrLf _
            & " 0 as OtherQty, " & vbCrLf _
            & " 0 as OtherValue,"

        SqlStr = SqlStr & vbCrLf _
            & " 0 as ASSETS_VALUE," & vbCrLf _
            & " '' , ACCT.SUPP_CUST_NAME AS LEDGER_HEAD" & vbCrLf _
            & " , '' AS LEDGER_AMT , 0 AS CLOSING_NOS,"

        SqlStr = SqlStr & vbCrLf _
                & " 0 PUR_QTY," & vbCrLf _
                & " 0 PUR_VALUE," & vbCrLf _
                & " 0 PUR_SUPP_QTY," & vbCrLf _
                & " 0 PUR_SUPP_VALUE," & vbCrLf _
                & " 0 PUR_DN_QTY," & vbCrLf _
                & " 0 PUR_DN_VALUE," & vbCrLf _
                & " 0 PUR_CN_QTY," & vbCrLf _
                & " 0 PUR_CN_VALUE," & vbCrLf _
                & " 0 SALE_QTY," & vbCrLf _
                & " 0 SALE_VALUE," & vbCrLf _
                & " 0 SALE_DN_QTY," & vbCrLf _
                & " 0 SALE_DN_VALUE," & vbCrLf _
                & " 0 SALE_CN_QTY," & vbCrLf _
                & " 0 SALE_CN_VALUE," & vbCrLf _
                & " 0 JV_VALUE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_ITEM_MST ITEM, INV_GENERAL_MST GMST, FIN_SUPP_CUST_MST ACCT "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " WHERE ITEM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " AND GMST.COMPANY_CODE=ACCT.COMPANY_CODE(+)" & vbCrLf _
            & " AND GMST.ACCT_CONSUM_CODE=ACCT.SUPP_CUST_CODE(+) "


        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_CODE='" & mItemCode & "'"
            End If
        End If


        If lstInvoiceType.GetItemChecked(0) = True Then
            mRMCatCodeStr = ""
        Else
            For CntLst = 1 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    mMaterialType = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
                End If
            Next
        End If

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If


        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mAccountCodeStr As String

        If lstAccountMapping.GetItemChecked(0) = True Then
            mAccountCodeStr = ""
        Else
            For CntLst = 1 To lstAccountMapping.Items.Count - 1
                If lstAccountMapping.GetItemChecked(CntLst) = True Then
                    mAccountName = VB6.GetItemString(lstAccountMapping, CntLst)
                    If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", "'" & mAccountCode & "'", mAccountCodeStr & "," & "'" & mAccountCode & "'")
                End If
            Next
        End If

        If mAccountCodeStr <> "" Then
            mAccountCodeStr = "(" & mAccountCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND GMST.ACCT_CONSUM_CODE IN " & mAccountCodeStr & ""
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
            SqlStr = SqlStr & vbCrLf & " AND ITEM.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_CODE NOT IN (" & vbCrLf _
            & "  SELECT DISTINCT ITEM_CODE FROM  TEMP_STOCKREG WHERE USERID ='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "')"

        SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_CODE IN (" & vbCrLf _
            & "  SELECT DISTINCT ITEM_CODE FROM  INV_STOCK_REC_TRN " & vbCrLf _
            & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & " And STATUS='O'"

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        SqlStr = SqlStr & vbCrLf _
            & " )"

        '' SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"

        'SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
        '    & " INV.COMPANY_CODE, INV.FYEAR, ITEM.CATEGORY_CODE, GMST.GEN_DESC, INV.ACCOUNTCODE, ACCT.SUPP_CUST_NAME, INV.ITEM_CODE, " & vbCrLf _
        '    & " ITEM.ITEM_SHORT_DESC, ITEM.ISSUE_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR, ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY "

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf _
            & "  ITEM.COMPANY_CODE, ITEM.CATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQLOTH = SqlStr
        Exit Function
InsertErr:
        MakeSQLOTH = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function MakeSQLOLD27042024() As String

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim mCategoryCode As String = ""
        Dim mSubCategoryCode As String
        Dim mCond As String
        Dim mItemCode As String
        Dim mHavingClause As Boolean
        Dim mTableName As String
        Dim mToDate As String
        Dim mDeptFunction As String
        Dim mDivision As Double

        Dim mRMCatCode As String = ""
        Dim mRMCatCodeStr As String = ""
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""

        Dim mCompanyCodeStr As String = ""
        Dim mStockTypeStr As String = ""
        Dim mStockType As String = ""


        mHavingClause = False
        mToDate = VB6.Format(txtDateTo.Text, "DD-MMM-YYYY")

        mTableName = ConInventoryTable


        mDeptFunction = "GETDEPTSTOCK"

        Dim mWareHouse As String

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "',  INV.COMPANY_CODE, " & vbCrLf _
            & " ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE, SC.SUBCATEGORY_DESC, GMST.ACCT_CONSUM_CODE, ACCT.SUPP_CUST_NAME, ITEM.ITEM_CODE, " & vbCrLf _
            & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR, ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY, "

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS Opening, "

        SqlStr = SqlStr & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE,SUM(CASE WHEN E_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  THEN ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) ELSE 0 END)) AS OpeningValue, "



        SqlStr = SqlStr & vbCrLf & " 0 AS MRRQty, "


        SqlStr = SqlStr & vbCrLf & " 0 AS MRRValue, "


        SqlStr = SqlStr & vbCrLf & " 0 AS Receipt, "

        SqlStr = SqlStr & vbCrLf & " 0 as ReceiptValue,"

        SqlStr = SqlStr & vbCrLf & " 0 AS Issue, " ''AND STOCK_TYPE IN ('ST','CS','FG')

        SqlStr = SqlStr & vbCrLf & " 0 as ISSUEValue,"

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Closing, " & vbCrLf _
            & " 0 AS Rejection, " & vbCrLf _
            & " 0 AS UnderQC, " & vbCrLf _
            & " 0  as DEPT_QTY, " & vbCrLf _
            & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))  as TotClosing, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, 1) as Rate, " & vbCrLf _
            & " GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE),  INV.COMPANY_CODE, SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1) * CASE WHEN E_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN 1 ELSE 0 END )) as Value,"

        SqlStr = SqlStr & vbCrLf _
            & " 0 as ConQty, " & vbCrLf _
            & " 0 as ConValue,"

        SqlStr = SqlStr & vbCrLf _
            & " 0 as OtherQty, " & vbCrLf _
            & " 0 as OtherValue,"

        SqlStr = SqlStr & vbCrLf _
            & " 0 as ASSETS_VALUE," & vbCrLf _
            & " '' , '' AS LEDGER_HEAD" & vbCrLf _
            & " , '' AS LEDGER_AMT , 0 AS CLOSING_NOS,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_QTY) " & vbCrLf _
                & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS PUR_QTY, "


        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_AMT) " & vbCrLf _
                & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS PUR_VALUE,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.QTY) " & vbCrLf _
                & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_SUPP_PURCHASE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS PUR_SUPP_QTY,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.AMOUNT) " & vbCrLf _
                & " FROM FIN_SUPP_PURCHASE_HDR IH, FIN_SUPP_PURCHASE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS PUR_SUPP_VALUE,"


        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_QTY) " & vbCrLf _
                & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.BOOKCODE = " & ConDebitNoteBookCode & " AND IH.CANCELLED='N' AND IH.APPROVED='Y'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS PUR_DN_QTY,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_AMT) " & vbCrLf _
                & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.BOOKCODE = " & ConDebitNoteBookCode & " AND IH.CANCELLED='N' AND IH.APPROVED='Y'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS PUR_DN_VALUE,"


        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_QTY) " & vbCrLf _
                & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.BOOKCODE = " & ConCreditNoteBookCode & " AND IH.CANCELLED='N' AND IH.APPROVED='Y'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS PUR_CN_QTY,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_QTY) " & vbCrLf _
                & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.BOOKCODE = " & ConCreditNoteBookCode & " AND IH.CANCELLED='N' AND IH.APPROVED='Y'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS PUR_CN_VALUE,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_QTY) " & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N' " & vbCrLf _
                & " AND IH.INVOICESEQTYPE IN (1,2,3,5,6) AND IH.REF_DESP_TYPE NOT IN ('Q','L')" & vbCrLf _
                & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS  SALE_QTY,"



        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_AMT) " & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N'" & vbCrLf _
                & " AND IH.INVOICESEQTYPE IN (1,2,3,5,6) AND IH.REF_DESP_TYPE NOT IN ('Q','L')" & vbCrLf _
                & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS  SALE_VALUE,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_QTY) " & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N' " & vbCrLf _
                & " AND IH.INVOICESEQTYPE IN (9) AND IH.REF_DESP_TYPE NOT IN ('Q','L')" & vbCrLf _
                & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS SALE_DN_QTY,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.ITEM_AMT) " & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N'" & vbCrLf _
                & " AND IH.INVOICESEQTYPE IN (9) AND IH.REF_DESP_TYPE NOT IN ('Q','L')" & vbCrLf _
                & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS SALE_DN_VALUE,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.QTY) " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N' AND GOODS_SERVICE='G' AND IH.ISFINALPOST='Y'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS SALE_CN_QTY,"

        SqlStr = SqlStr & vbCrLf _
                & " (SELECT SUM(ID.AMOUNT) " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR IH, FIN_SUPP_SALE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE = INV.COMPANY_CODE" & vbCrLf _
                & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND ID.ITEM_CODE = ITEM.ITEM_CODE AND IH.CANCELLED='N' AND GOODS_SERVICE='G' AND IH.ISFINALPOST='Y'" & vbCrLf _
                & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & "  ) AS SALE_CN_VALUE"


        SqlStr = SqlStr & vbCrLf _
            & " FROM " & mTableName & " INV, " & vbCrLf _
            & " INV_ITEM_MST ITEM, FIN_SUPP_CUST_MST ACM, INV_GENERAL_MST GMST, FIN_SUPP_CUST_MST ACCT, INV_SUBCATEGORY_MST SC "


        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf _
            & " WHERE INV.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND INV.STOCK_ID IN ('WH','PH','SH')"

        '

        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf _
            & " AND INV.ITEM_CODE=ITEM.ITEM_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND INV.COMPANY_CODE=ACM.COMPANY_CODE(+)" & vbCrLf _
            & " AND INV.PARTYCODE=ACM.SUPP_CUST_CODE(+) "

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=GMST.GEN_CODE "

        SqlStr = SqlStr & vbCrLf _
            & " AND GMST.COMPANY_CODE=ACCT.COMPANY_CODE(+)" & vbCrLf _
            & " AND GMST.ACCT_CONSUM_CODE=ACCT.SUPP_CUST_CODE(+) "

        SqlStr = SqlStr & vbCrLf _
            & " AND ITEM.COMPANY_CODE=SC.COMPANY_CODE" & vbCrLf _
            & " AND ITEM.CATEGORY_CODE=SC.CATEGORY_CODE " & vbCrLf _
            & " AND ITEM.SUBCATEGORY_CODE=SC.SUBCATEGORY_CODE "



        If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = Trim(MasterNo)
                SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
            End If
        End If


        If lstInvoiceType.GetItemChecked(0) = True Then
            mRMCatCodeStr = ""
        Else
            For CntLst = 1 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    mMaterialType = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                        mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
                End If
            Next
        End If

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND ITEM.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If


        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mAccountCodeStr As String

        If lstAccountMapping.GetItemChecked(0) = True Then
            mAccountCodeStr = ""
        Else
            For CntLst = 1 To lstAccountMapping.Items.Count - 1
                If lstAccountMapping.GetItemChecked(CntLst) = True Then
                    mAccountName = VB6.GetItemString(lstAccountMapping, CntLst)
                    If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", "'" & mAccountCode & "'", mAccountCodeStr & "," & "'" & mAccountCode & "'")
                End If
            Next
        End If

        If mAccountCodeStr <> "" Then
            mAccountCodeStr = "(" & mAccountCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND GMST.ACCT_CONSUM_CODE IN " & mAccountCodeStr & ""
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
            SqlStr = SqlStr & vbCrLf & " AND INV.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        'If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mItemCode = Trim(MasterNo)
        '        SqlStr = SqlStr & vbCrLf & " AND INV.ITEM_CODE='" & mItemCode & "'"
        '    End If
        'End If


        ''AND DEPT_CODE_TO='STR'
        'If cboShow.SelectedIndex = 0 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='A' "
        'ElseIf cboShow.SelectedIndex = 1 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_STATUS='I' "
        'End If

        'If Val(txtLocation.Text) <> 0 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.ITEM_LOCATION ='" & txtLocation.Text & "'"
        'End If

        'If cboExportItem.SelectedIndex >= 1 Then
        '    SqlStr = SqlStr & vbCrLf & " AND ITEM.IS_EXPORT_ITEM = '" & VB.Left(cboExportItem.Text, 1) & "'"
        'End If

        'If chkViewAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        SqlStr = SqlStr & vbCrLf & " AND INV.STATUS='O'"
        'End If

        If IsDate(txtDateTo.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        'If chkOption.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))" & cboCond.Text & Val(txtCondQty.Text) & ""
        '    mHavingClause = True
        'Else
        '    If lblBookType.Text = "B" Then
        '        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<ITEM.MINIMUM_QTY"
        '        mHavingClause = True
        '    ElseIf lblBookType.Text = "A" Then
        '        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>ITEM.MAXIMUM_QTY"
        '        mHavingClause = True
        '    End If
        'End If

        'If chkZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    If mHavingClause = False Then
        '        SqlStr = SqlStr & vbCrLf & " HAVING "
        '        mHavingClause = True
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND "
        '    End If

        '    SqlStr = SqlStr & vbCrLf & " SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))<>0"
        'End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY " & vbCrLf _
            & "  INV.COMPANY_CODE, ITEM.CATEGORY_CODE, GMST.GEN_DESC, SC.SUBCATEGORY_CODE,SC.SUBCATEGORY_DESC, ITEM.ITEM_CODE, " & vbCrLf _
            & " ITEM.ITEM_SHORT_DESC, INV.ITEM_UOM, ITEM.MAT_THICHNESS, ITEM.ITEM_COLOR, ITEM.MINIMUM_QTY, ITEM.MAXIMUM_QTY,PRD_TYPE,NVL(ITEM.ITEM_CODE,ITEM.ITEM_CODE) "

        'If chkShowLedgerBalance.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    SqlStr = SqlStr & vbCrLf _
        '        & " , NVL(ACCMST.SUPP_CUST_NAME,''), GETITEMLEDGERAMOUNT(INV.COMPANY_CODE, INV.FYEAR,ITEM.ITEM_CODE, NVL(GMST.ACCT_CONSUM_CODE,''), TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        'End If

        'If lblBookType.Text = "B" Then
        '    SqlStr = SqlStr & vbCrLf & " , ITEM.MINIMUM_QTY"
        'ElseIf lblBookType.Text = "A" Then
        '    SqlStr = SqlStr & vbCrLf & " , ITEM.MAXIMUM_QTY"
        'End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY " & vbCrLf & "  INV.COMPANY_CODE, ITEM.CATEGORY_CODE, SC.SUBCATEGORY_CODE, ITEM.ITEM_CODE "


        MakeSQLOLD27042024 = SqlStr
        Exit Function
InsertErr:
        MakeSQLOLD27042024 = ""
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function Show1() As Boolean

        On Error GoTo InsertErr
        Dim SqlStr As String = ""
        Dim SqlStrOth As String = ""

        Dim mSqlStr As String = ""
        Dim pSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        pSqlStr = MakeSQL()
        SqlStrOth = MakeSQLOTH()

        'If optType(1).Checked = True Then
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSqlStr = "DELETE FROM TEMP_STOCKREG WHERE USERID ='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(mSqlStr)



        SqlStr = " INSERT INTO TEMP_STOCKREG ( USERID, COMPANY_CODE, GROUP_NAME, CATEGORY_NAME,SUBCATEGORY_CODE,SUBCATEGORY_NAME, " & vbCrLf _
                    & " ITEM_CODE,ITEM_NAME,ITEM_UOM, MAT_THICHNESS, ITEM_COLOR, MINIMUM_QTY, MAXIMUM_QTY, " & vbCrLf _
                    & " OPENING,OPENING_VALUE, MRR_QTY, MRR_VALUE, RECEIPT,RECEIPT_VALUE ,ISSUE,ISSUE_VALUE,CLOSING," & vbCrLf _
                    & " REJ_QTY,UNDERQC_QTY,DEPT_QTY,TOTAL_QTY," & vbCrLf _
                    & " RATE,VALUE, CONSUMPTION_QTY, CONSUMPTION_VALUE,OTHER_ISSUE_QTY, OTHER_ISSUE_VALUE, " & vbCrLf _
                    & " ASSETS_VALUE, LAST_TRANS_DATE,LEDGER_HEAD,LEDGER_AMT,CLOSING_NOS, " & vbCrLf _
                    & " PUR_QTY, PUR_VALUE, " & vbCrLf _
                    & " PUR_SUPP_QTY, PUR_SUPP_VALUE, " & vbCrLf _
                    & " PUR_DN_QTY, PUR_DN_VALUE," & vbCrLf _
                    & " PUR_CN_QTY, PUR_CN_VALUE, " & vbCrLf _
                    & " SALE_QTY, SALE_VALUE, " & vbCrLf _
                    & " SALE_DN_QTY, SALE_DN_VALUE, SALE_CN_QTY, SALE_CN_VALUE, JV_VALUE" & vbCrLf _
                    & " )" & vbCrLf _
                    & pSqlStr

        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_STOCKREG ( USERID, COMPANY_CODE, GROUP_NAME, CATEGORY_NAME,SUBCATEGORY_CODE,SUBCATEGORY_NAME, " & vbCrLf _
                    & " ITEM_CODE,ITEM_NAME,ITEM_UOM, MAT_THICHNESS, ITEM_COLOR, MINIMUM_QTY, MAXIMUM_QTY, " & vbCrLf _
                    & " OPENING,OPENING_VALUE, MRR_QTY, MRR_VALUE, RECEIPT,RECEIPT_VALUE ,ISSUE,ISSUE_VALUE,CLOSING," & vbCrLf _
                    & " REJ_QTY,UNDERQC_QTY,DEPT_QTY,TOTAL_QTY," & vbCrLf _
                    & " RATE,VALUE, CONSUMPTION_QTY, CONSUMPTION_VALUE,OTHER_ISSUE_QTY, OTHER_ISSUE_VALUE, " & vbCrLf _
                    & " ASSETS_VALUE, LAST_TRANS_DATE,LEDGER_HEAD,LEDGER_AMT,CLOSING_NOS, " & vbCrLf _
                    & " PUR_QTY, PUR_VALUE, " & vbCrLf _
                    & " PUR_SUPP_QTY, PUR_SUPP_VALUE, " & vbCrLf _
                    & " PUR_DN_QTY, PUR_DN_VALUE," & vbCrLf _
                    & " PUR_CN_QTY, PUR_CN_VALUE, " & vbCrLf _
                    & " SALE_QTY, SALE_VALUE, " & vbCrLf _
                    & " SALE_DN_QTY, SALE_DN_VALUE, SALE_CN_QTY, SALE_CN_VALUE, JV_VALUE" & vbCrLf _
                    & " )" & vbCrLf _
                    & SqlStrOth

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()

        SqlStr = " SELECT '', GROUP_NAME, CATEGORY_NAME, LEDGER_HEAD," & vbCrLf _
                & " ITEM_CODE,ITEM_NAME,ITEM_UOM," & vbCrLf _
                & " SUM(OPENING), "


        'SUM(OPENING_VALUE),

        SqlStr = SqlStr & vbCrLf _
            & " SUM(GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM_CODE,'-'),  COMPANY_CODE,OPENING)) AS OpeningValue, "

        SqlStr = SqlStr & vbCrLf _
                & "SUM(PUR_QTY), SUM(PUR_VALUE),"

        SqlStr = SqlStr & vbCrLf _
                & " SUM(PUR_SUPP_QTY), SUM(PUR_SUPP_VALUE), " & vbCrLf _
                & " SUM(PUR_DN_QTY), SUM(PUR_DN_VALUE), " & vbCrLf _
                & " SUM(PUR_CN_QTY), SUM(PUR_CN_VALUE), " & vbCrLf _
                & " SUM(SALE_QTY), SUM(SALE_VALUE), " & vbCrLf _
                & " SUM(SALE_DN_QTY), SUM(SALE_DN_VALUE), " & vbCrLf _
                & " SUM(SALE_CN_QTY), SUM(SALE_CN_VALUE), SUM(JV_VALUE),"

        SqlStr = SqlStr & vbCrLf _
                & " SUM(TOTAL_QTY),"

        'SqlStr = SqlStr & vbCrLf _
        '        & " SUM(VALUE), "

        SqlStr = SqlStr & vbCrLf _
            & " SUM(GETFIFOITEMRATE(TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NVL(ITEM_CODE,'-'),  COMPANY_CODE,TOTAL_QTY)) AS Value, "


        SqlStr = SqlStr & vbCrLf & "'' FROM TEMP_STOCKREG A " & vbCrLf _
                & " WHERE USERID ='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
                & " GROUP BY GROUP_NAME, CATEGORY_NAME,LEDGER_HEAD,ITEM_CODE,ITEM_NAME,ITEM_UOM"

        SqlStr = SqlStr & vbCrLf & "ORDER BY GROUP_NAME, CATEGORY_NAME,ITEM_CODE,LEDGER_HEAD"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Show1 = True
        Exit Function
InsertErr:
        Show1 = False
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description)
        ''Resume
    End Function
    '    Private Function Show1() As Boolean
    '        On Error GoTo LedgError
    '        Dim SqlStr As String

    '        Show1 = False
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

    '        'SqlStr = MakeSQL()
    '        SqlStr = MakeSQL()
    '        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

    '        '''********************************				
    '        Show1 = True
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    '        Exit Function
    'LedgError:
    '        Show1 = False
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '    End Function

    Private Function MakeSQLOld() As String
        On Error GoTo ERR1


        Dim CntLst As Integer
        Dim mAccountName As String
        Dim mAccountCode As String
        Dim mAccountCodeStr As String
        Dim mShowAll As Boolean
        Dim mChkType As Boolean
        Dim mCompanyGSTNo As String

        mShowAll = True
        mAccountCodeStr = ""

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)

        mChkType = lstInvoiceType.GetItemChecked(0)

        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = Not mChkType Then
                mShowAll = False
                Exit For
            End If
        Next

        If mShowAll = False Then
            For CntLst = 0 To lstInvoiceType.Items.Count - 1
                If lstInvoiceType.GetItemChecked(CntLst) = True Then
                    mAccountName = VB6.GetItemString(lstInvoiceType, CntLst)
                    If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mAccountCode = "'" & IIf(IsDBNull(MasterNo), "", MasterNo) & "'"
                    End If
                    mAccountCodeStr = IIf(mAccountCodeStr = "", mAccountCode, mAccountCodeStr & "," & mAccountCode)
                End If
            Next
        End If

        If optShow(0).Checked = True Then
            MakeSQLOld = " SELECT  VLOCK, VNO, TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf _
                & " SUPP_CUST_CODE, SUPP_CUST_NAME, ACCOUNT_CODE, ACCOUNT_NAME,  " & vbCrLf _
                & " ITEM_CODE, ITEM_SHORT_DESC, CUSTOMER_PART_NO, QTY, RATE,   " & vbCrLf _
                & " ITEM_AMOUNT, GSTABLE_AMT, GST_REFUND_AMOUNT, CGST_AMOUNT, SGST_AMOUNT,  IGST_AMOUNT, OTHERS_AMOUNT, DRCR, " & vbCrLf _
                & " REMARKS, BOOKCODE, BOOKTYPE,  " & vbCrLf _
                & " VTYPE, MKEY " & vbCrLf _
                & " FROM ("
        Else
            MakeSQLOld = " SELECT  VLOCK, VNO, TO_CHAR(VDATE,'DD/MM/YYYY') AS VDATE, " & vbCrLf _
                & " SUPP_CUST_CODE, SUPP_CUST_NAME, ACCOUNT_CODE, ACCOUNT_NAME,  " & vbCrLf _
                & " '' AS ITEM_CODE, '' AS ITEM_SHORT_DESC, '' As CUSTOMER_PART_NO, SUM(QTY) AS QTY, 0 AS RATE,   " & vbCrLf _
                & " SUM(ITEM_AMOUNT) AS ITEM_AMOUNT, SUM(GSTABLE_AMT) AS GSTABLE_AMT, SUM(GST_REFUND_AMOUNT) AS GST_REFUND_AMOUNT, SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT, SUM(OTHERS_AMOUNT) AS OTHERS_AMOUNT, " & vbCrLf & " DRCR, '' AS REMARKS, BOOKCODE, BOOKTYPE,  " & vbCrLf _
                & " VTYPE, MKEY " & vbCrLf _
                & " FROM ("
        End If

        MakeSQLOld = MakeSQLOld & vbCrLf & " SELECT '0' AS VLOCK, " & vbCrLf _
            & " '' AS VNO, NULL AS VDATE, " & vbCrLf _
            & " '' AS SUPP_CUST_CODE, '' AS SUPP_CUST_NAME, '' AS ACCOUNT_CODE, '' AS ACCOUNT_NAME,  " & vbCrLf _
            & " '' AS ITEM_CODE, '' AS ITEM_SHORT_DESC,'' AS CUSTOMER_PART_NO, 0 AS QTY, 0 AS RATE,   " & vbCrLf _
            & " 0 AS ITEM_AMOUNT, 0 AS GSTABLE_AMT, 0 AS GST_REFUND_AMOUNT, 0 AS CGST_AMOUNT, 0 AS SGST_AMOUNT,  0 AS IGST_AMOUNT, 0 AS OTHERS_AMOUNT, " & vbCrLf & " '' AS DRCR, '' AS REMARKS, -1 AS BOOKCODE, '' AS BOOKTYPE,  " & vbCrLf & " '' AS VTYPE, '' AS MKEY "

        MakeSQLOld = MakeSQLOld & vbCrLf & " FROM DUAL WHERE 1=2 "

        'If chkSale.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    MakeSQLOld = MakeSQLOld & vbCrLf & " UNION ALL "
        '    MakeSQLOld = MakeSQLOld & vbCrLf & MakeSQLOldSale(mShowAll, mAccountCodeStr, "01", "SALE", "DR", mCompanyGSTNo)
        'End If

        'If chkSaleDN.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    MakeSQLOld = MakeSQLOld & vbCrLf & " UNION ALL "
        '    MakeSQLOld = MakeSQLOld & vbCrLf & MakeSQLOldSaleDN(mShowAll, mAccountCodeStr, "02", "SALE DN", "CR", mCompanyGSTNo)
        'End If

        'If chkPurchase.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    MakeSQLOld = MakeSQLOld & vbCrLf & " UNION ALL "
        '    MakeSQLOld = MakeSQLOld & vbCrLf & MakeSQLOldPur(mShowAll, mAccountCodeStr, "03", "PURCHASE", "CR", mCompanyGSTNo)
        'End If

        'If chkPurSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    MakeSQLOld = MakeSQLOld & vbCrLf & " UNION ALL "
        '    MakeSQLOld = MakeSQLOld & vbCrLf & MakeSQLOldPurSupp(mShowAll, mAccountCodeStr, "04", "PURCHASE SUPP", "CR", mCompanyGSTNo)
        'End If

        'If chkDNCN.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    MakeSQLOld = MakeSQLOld & vbCrLf & " UNION ALL "
        '    MakeSQLOld = MakeSQLOld & vbCrLf & MakeSQLOldPurDN(mShowAll, mAccountCodeStr, "05", "PURCHASE DEBIT", "DR", mCompanyGSTNo)

        '    MakeSQLOld = MakeSQLOld & vbCrLf & " UNION ALL "
        '    MakeSQLOld = MakeSQLOld & vbCrLf & MakeSQLOldPurDN(mShowAll, mAccountCodeStr, "06", "PURCHASE CREDIT", "CR", mCompanyGSTNo)
        'End If

        'If chkJournal.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    MakeSQLOld = MakeSQLOld & vbCrLf & " UNION ALL "
        '    MakeSQLOld = MakeSQLOld & vbCrLf & MakeSQLOldJournal(mShowAll, mAccountCodeStr, "07", "JOURNAL", "CR", mCompanyGSTNo)

        '    MakeSQLOld = MakeSQLOld & vbCrLf & " UNION ALL "
        '    MakeSQLOld = MakeSQLOld & vbCrLf & MakeSQLOldJournalDetails(mShowAll, mAccountCodeStr, "07", "JOURNAL", "CR", mCompanyGSTNo)
        'End If

        'If optShow(0).Checked = True Then
        '    MakeSQLOld = MakeSQLOld & vbCrLf & ") ORDER BY 3,2 "
        'Else
        '    MakeSQLOld = MakeSQLOld & vbCrLf & ") GROUP BY VLOCK, VNO, VDATE,SUPP_CUST_CODE, SUPP_CUST_NAME, ACCOUNT_CODE, ACCOUNT_NAME, DRCR, BOOKCODE, BOOKTYPE,VTYPE, MKEY"
        '    MakeSQLOld = MakeSQLOld & vbCrLf & " ORDER BY VDATE,VLOCK "
        'End If

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
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
        Dim CntRow As Integer

        Dim mOpeningQty As Double
        Dim mOpeningValue As Double
        Dim mPurchaseQty As Double
        Dim mPurchaseValue As Double
        Dim mPurchaseSuppQty As Double
        Dim mPurchaseSuppValue As Double
        Dim mPurchaseDebitQty As Double
        Dim mPurchaseDebitValue As Double
        Dim mPurchaseCreditQty As Double
        Dim mPurchaseCreditValue As Double
        Dim mSaleQty As Double
        Dim mSaleValue As Double
        Dim mSaleDebitQty As Double
        Dim mSaleDebitValue As Double
        Dim mSaleCreditQty As Double
        Dim mSaleCreditValue As Double
        Dim mClosingQty As Double
        Dim mClosingValue As Double
        Dim mJVValue As Double


        With SprdMain


            mOpeningQty = 0
            mOpeningValue = 0
            mPurchaseQty = 0
            mPurchaseValue = 0
            mPurchaseSuppQty = 0
            mPurchaseSuppValue = 0
            mPurchaseDebitQty = 0
            mPurchaseDebitValue = 0
            mPurchaseCreditQty = 0
            mPurchaseCreditValue = 0
            mSaleQty = 0
            mSaleValue = 0
            mSaleDebitQty = 0
            mSaleDebitValue = 0
            mSaleCreditQty = 0
            mSaleCreditValue = 0
            mClosingQty = 0
            mClosingValue = 0
            mJVValue = 0


            For CntRow = 1 To .MaxRows
                .Row = CntRow



                .Col = ColOpeningQty
                mOpeningQty = mOpeningQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColOpeningValue
                mOpeningValue = mOpeningValue + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPurchaseQty
                mPurchaseQty = mPurchaseQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPurchaseValue
                mPurchaseValue = mPurchaseValue + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPurchaseSuppQty
                mPurchaseSuppQty = mPurchaseSuppQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPurchaseSuppValue
                mPurchaseSuppValue = mPurchaseSuppValue + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPurchaseDebitQty
                mPurchaseDebitQty = mPurchaseDebitQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPurchaseDebitValue
                mPurchaseDebitValue = mPurchaseDebitValue + Val(IIf(IsNumeric(.Text), .Text, 0))


                .Col = ColPurchaseCreditQty
                mPurchaseCreditQty = mPurchaseCreditQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColPurchaseCreditValue
                mPurchaseCreditValue = mPurchaseCreditValue + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColSaleQty
                mSaleQty = mSaleQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColSaleValue
                mSaleValue = mSaleValue + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColSaleDebitQty
                mSaleDebitQty = mSaleDebitQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColSaleDebitValue
                mSaleDebitValue = mSaleDebitValue + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColSaleCreditQty
                mSaleCreditQty = mSaleCreditQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColSaleCreditValue
                mSaleCreditValue = mSaleCreditValue + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColJVValue
                mJVValue = mJVValue + Val(IIf(IsNumeric(.Text), .Text, 0))


                .Col = ColClosingQty
                mClosingQty = mClosingQty + Val(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColClosingValue
                mClosingValue = mClosingValue + Val(IIf(IsNumeric(.Text), .Text, 0))
            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColItemCode)
            .Row = .MaxRows
            .Col = ColItemName
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColOpeningQty
            .Text = VB6.Format(mOpeningQty, "0.00")

            .Col = ColOpeningValue
            .Text = VB6.Format(mOpeningValue, "0.00")

            .Col = ColPurchaseQty
            .Text = VB6.Format(mPurchaseQty, "0.00")

            .Col = ColPurchaseValue
            .Text = VB6.Format(mPurchaseValue, "0.00")

            .Col = ColPurchaseSuppQty
            .Text = VB6.Format(mPurchaseSuppQty, "0.00")

            .Col = ColPurchaseSuppValue
            .Text = VB6.Format(mPurchaseSuppValue, "0.00")

            .Col = ColPurchaseDebitQty
            .Text = VB6.Format(mPurchaseDebitQty, "0.00")

            .Col = ColPurchaseDebitValue
            .Text = VB6.Format(mPurchaseDebitValue, "0.00")

            .Col = ColPurchaseCreditQty
            .Text = VB6.Format(mPurchaseCreditQty, "0.00")

            .Col = ColPurchaseCreditValue
            .Text = VB6.Format(mPurchaseCreditValue, "0.00")

            .Col = ColSaleQty
            .Text = VB6.Format(mSaleQty, "0.00")

            .Col = ColSaleValue
            .Text = VB6.Format(mSaleValue, "0.00")

            .Col = ColSaleDebitQty
            .Text = VB6.Format(mSaleDebitQty, "0.00")

            .Col = ColSaleDebitValue
            .Text = VB6.Format(mSaleDebitValue, "0.00")

            .Col = ColSaleCreditQty
            .Text = VB6.Format(mSaleCreditQty, "0.00")

            .Col = ColSaleCreditValue
            .Text = VB6.Format(mSaleCreditValue, "0.00")

            .Col = ColJVValue
            .Text = VB6.Format(mJVValue, "0.00")

            .Col = ColClosingQty
            .Text = VB6.Format(mClosingQty, "0.00")

            .Col = ColClosingValue
            .Text = VB6.Format(mClosingValue, "0.00")

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80				
            .Font = VB6.FontChangeBold(.Font, True)
            .BlockMode = False





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

            .Col = ColCatgeory
            .Text = "Category"

            .Col = ColCatgeoryDesc
            .Text = "Category Desc"

            .Col = ColAccountPostingHead
            .Text = "Account Posting Head"

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Name"

            .Col = ColItemUOM
            .Text = "UOM"

            .Col = ColOpeningQty
            .Text = "Opening Qty"

            .Col = ColOpeningValue
            .Text = "Opening Value"

            .Col = ColPurchaseQty
            .Text = "Purchase Qty"

            .Col = ColPurchaseValue
            .Text = "Purchase Value"

            .Col = ColPurchaseSuppQty
            .Text = "Purchase Supp Qty"

            .Col = ColPurchaseSuppValue
            .Text = "Purchase Supp Value"

            .Col = ColPurchaseDebitQty
            .Text = "Purchase Debit Qty"

            .Col = ColPurchaseDebitValue
            .Text = "Purchase Debit Value"


            .Col = ColPurchaseCreditQty
            .Text = "Purchase Credit Qty"

            .Col = ColPurchaseCreditValue
            .Text = "Purchase Credit Value"


            .Col = ColSaleQty
            .Text = "Sale Qty"

            .Col = ColSaleValue
            .Text = "Sale Value"


            .Col = ColSaleDebitQty
            .Text = "Sale Debit Qty"

            .Col = ColSaleDebitValue
            .Text = "Sale Debit Value"


            .Col = ColSaleCreditQty
            .Text = "Sale Credit Qty"

            .Col = ColSaleCreditValue
            .Text = "Sale Credit Value"

            .Col = ColJVValue
            .Text = "JV Value"

            .Col = ColClosingQty
            .Text = "Closing Qty"

            .Col = ColClosingValue
            .Text = "Closing Value"

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



    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        PubDBCn.Errors.Clear()


        'If TxtName.Text = "" Then Exit Sub				

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertPrintDummy()


        '''''Select Record for print...				

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Consumption Report Register"
        mSubTitle = ""

        mRPTName = "ConsumptionReport.Rpt"

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
        Dim SqlStr As String
        Dim CntRow As Integer
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
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle,  ,  , "Y")

        If VB6.Format(txtDateTo.Text, "MMMM, YYYY") = VB6.Format(txtDateFrom.Text, "MMMM, YYYY") Then
            mMonth = "Month : " & VB6.Format(txtDateTo.Text, "MMMM, YYYY")
        Else
            mMonth = "Month : FROM " & VB6.Format(txtDateFrom.Text, "MMMM, YYYY") & " To " & VB6.Format(txtDateTo.Text, "MMMM, YYYY")
        End If

        MainClass.AssignCRptFormulas(Report1, "MonthTitle=""" & mMonth & """")

        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call cmdItemDesc_Click(cmdItemDesc, New System.EventArgs())
    End Sub

    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdItemDesc_Click(cmdItemDesc, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtItemName.Text) = "" Then
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Item Name.", , MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub chkItemAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemAll.CheckStateChanged
        txtItemName.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdItemDesc.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub


    Private Sub cmdItemDesc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdItemDesc.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtItemName.Text), "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(False))
            txtItemName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub lstAccountMapping_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstAccountMapping.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstAccountMapping.GetItemChecked(0) = True Then
                    For I = 1 To lstAccountMapping.Items.Count - 1
                        lstAccountMapping.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstAccountMapping.Items.Count - 1
                        lstAccountMapping.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstAccountMapping.GetItemChecked(e.Index - 1) = False Then
                    lstAccountMapping.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
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
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstMaterialType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstMaterialType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboExportItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.TextChanged
        PrintStatus(False)
    End Sub


End Class
