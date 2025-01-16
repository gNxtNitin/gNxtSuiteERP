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
Friend Class frmParamSaleGRReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Dim cntSearchRow As Integer

    Private Const ColLocked As Short = 1
    Private Const ColChallanDate As Short = 2
    Private Const ColChallanNo As Short = 3
    Private Const ColBillDate As Short = 4
    Private Const ColBillNo As Short = 5
    Private Const ColPartyCode As Short = 6
    Private Const ColPartyName As Short = 7
    Private Const ColVendorCode As Short = 8
    Private Const ColBillAmount As Short = 9
    Private Const ColSaleAmount As Short = 10
    Private Const ColGST As Short = 11
    Private Const ColGRNo As Short = 12
    Private Const ColGRDate As Short = 13
    Private Const ColVehicleNo As Short = 14
    Private Const ColTransporter As Short = 15
    Private Const ColTransporterBillNo As Short = 16
    Private Const ColTransporterBillDate As Short = 17

    Private Const ColGRNNo As Short = 18
    Private Const ColGRNDate As Short = 19
    Private Const ColRecdQty As Short = 20
    Private Const ColAccteptedQty As Short = 21
    Private Const ColRejectedQty As Short = 22
    Private Const ColShotageQty As Short = 23
    Private Const ColRemarks As Short = 24


    Private Const ColBillNoPrefix As Short = 25
    Private Const ColBillNoSeq As Short = 26
    Private Const ColAddUser As Short = 27
    Private Const ColAddDate As Short = 28
    Private Const ColModUser As Short = 29
    Private Const ColModDate As Short = 30
    Private Const ColMKEY As Short = 31


    Private Const TabBillDate As Short = 0
    Private Const TabBillNo As Short = 12
    Private Const TabName As Short = 22
    Private Const TabBillAmount As Short = 81
    Private Const TabItemValue As Short = 96
    Private Const TabEDClaimed As Short = 111
    Private Const TabCST As Short = 126
    Private Const TabHGST As Short = 141
    Private Const TabGRNo As Short = 156
    Private Const TabGRDATE As Short = 171
    Private Const TabVehicleNo As Short = 186
    Private Const TabTransporter As Short = 201
    Private Const TabTransporterBillNo As Short = 216
    Private Const TabTransporterBillDate As Short = 230



    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboInvoiceType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvoiceType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboInvoiceType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboInvoiceType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If cboInvoiceType.Text = "ALL" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((cboInvoiceType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblTrnType.Text = MasterNo
        Else
            lblTrnType.Text = CStr(-1)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Call SaleReport("V")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Call SaleReport("P")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleGRReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "GR Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleGRReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        lblTrnType.Text = CStr(-1)
        MainClass.FillCombo(cboInvoiceType, "FIN_INVTYPE_MST", "NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'")

        cboInvoiceType.SelectedIndex = 0
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        cboStatus.Items.Clear()
        cboStatus.Items.Add("All")
        cboStatus.Items.Add("Pending")
        cboStatus.Items.Add("Complete")
        cboStatus.SelectedIndex = 0

        TxtAccount.Enabled = False
        cmdSearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Call Show1("L")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub frmParamSaleGRReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamSaleGRReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub



    'Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent)
    '    SprdMain.Row = -1
    '    SprdMain.Col = eventArgs.col
    '    SprdMain.DAutoCellTypes = True
    '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '    SprdMain.TypeEditLen = 1000
    'End Sub

    'Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent)

    '    Dim mSearchKey As String
    '    Dim mCol As Integer

    '    mCol = SprdMain.ActiveCol
    '    If eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
    '        cntSearchRow = 1
    '        mSearchKey = ""
    '        mSearchKey = InputBox("Enter Search String :", "Search", mSearchKey)
    '        MainClass.SearchIntoGrid(SprdMain, mCol, mSearchKey, cntSearchRow)
    '        cntSearchRow = cntSearchRow + 1
    '        SprdMain.Focus()
    '    End If
    'End Sub

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

        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        'Dim cntCol As Integer
        'With SprdMain
        '    .MaxCols = ColMKEY
        '    .set_RowHeight(0, RowHeight * 1.25)
        '    .set_ColWidth(0, 4.5)

        '    .set_RowHeight(-1, RowHeight)
        '    .Row = -1

        '    .Col = ColLocked
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColLocked, 15)
        '    .ColHidden = True

        '    .Col = ColChallanDate
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColChallanDate, 9)

        '    .Col = ColChallanNo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColChallanNo, 9)

        '    .Col = ColBillDate
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColBillDate, 9)

        '    .Col = ColBillNo
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColBillNo, 9)


        '    .Col = ColPartyCode
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColPartyCode, 6)

        '    .Col = ColPartyName
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColPartyName, 15)
        '    .ColsFrozen = ColPartyName

        '    For cntCol = ColBillAmount To ColHGST
        '        .Col = cntCol
        '        .CellType = SS_CELL_TYPE_FLOAT
        '        .TypeFloatDecimalPlaces = 2
        '        .TypeFloatMin = CDbl("-99999999999")
        '        .TypeFloatMax = CDbl("99999999999")
        '        .TypeFloatMoney = False
        '        .TypeFloatSeparator = False
        '        .TypeFloatDecimalChar = Asc(".")
        '        .TypeFloatSepChar = Asc(",")
        '        .set_ColWidth(cntCol, 12)

        '        If optType(0).Checked = True Then
        '            .ColHidden = False
        '        Else
        '            .ColHidden = True
        '        End If
        '    Next

        '    .Col = ColGRNo
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColGRNo, 10)

        '    .Col = ColGRDate
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColGRDate, 10)

        '    .Col = ColVehicleNo
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColVehicleNo, 10)

        '    .Col = ColTransporter
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColTransporter, 15)

        '    .Col = ColTransporterBillNo
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColTransporterBillNo, 10)

        '    .Col = ColTransporterBillDate
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColTransporterBillDate, 10)

        '    .Col = ColMKEY
        '    .CellType = SS_CELL_TYPE_EDIT
        '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
        '    .TypeEditLen = 255
        '    .TypeEditMultiLine = True
        '    .set_ColWidth(ColMKEY, 8)
        '    .ColHidden = True


        '    MainClass.SetSpreadColor(SprdMain, -1)
        '    MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
        '    SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        '    SprdMain.DAutoCellTypes = True
        '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        '    SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        'End With
    End Sub
    Private Function Show1(pType As String) As Boolean

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

        If optType(0).Checked = True Then
            SqlStr = MakeSQL(pType)
        Else
            SqlStr = MakeSQLGP(pType)
        End If

        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        FillUltraGrid(SqlStr)
        '********************************
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

        'UltraGrid1.DataSource.Rows.Clear()
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
            MsgBox("Can not open connection ! ")
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

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Caption = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanDate - 1).Header.Caption = "Challan Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanNo - 1).Header.Caption = "Challan No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Header.Caption = "Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyCode - 1).Header.Caption = "Party Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Header.Caption = "Party Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVendorCode - 1).Header.Caption = "Vendor Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1).Header.Caption = "Bill Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1).Header.Caption = "Sale Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGST - 1).Header.Caption = "GST"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNo - 1).Header.Caption = "GR No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRDate - 1).Header.Caption = "GR Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVehicleNo - 1).Header.Caption = "Vehicle No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTransporter - 1).Header.Caption = "Transporter"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTransporterBillNo - 1).Header.Caption = "Transporter Bill No"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNNo - 1).Header.Caption = "Customer GRN No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNDate - 1).Header.Caption = "Customer GRN Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRecdQty - 1).Header.Caption = "GRN Received Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAccteptedQty - 1).Header.Caption = "GRN Acctepted Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRejectedQty - 1).Header.Caption = "GRN Rejected Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShotageQty - 1).Header.Caption = "GRN Shortage Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRemarks - 1).Header.Caption = "GRN Remarks"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTransporterBillDate - 1).Header.Caption = "Transporter Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNoPrefix - 1).Header.Caption = "Bill No Prefix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNoSeq - 1).Header.Caption = "Bill No Seq"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Header.Caption = "Add User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Header.Caption = "Add Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Header.Caption = "Mod User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Header.Caption = "Mod Date"



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "MKey"



            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGST - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRecdQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAccteptedQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRejectedQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShotageQty - 1).Style = UltraWinGrid.ColumnStyle.Double

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGST - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRecdQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAccteptedQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRejectedQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShotageQty - 1).CellAppearance.TextHAlign = HAlign.Right




            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNoPrefix - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNoSeq - 1).Hidden = True

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanDate - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColChallanNo - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyCode - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVendorCode - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGST - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRDate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVehicleNo - 1).Width = 150

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTransporter - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTransporterBillNo - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTransporterBillDate - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNNo - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGRNDate - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRecdQty - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAccteptedQty - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRejectedQty - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColShotageQty - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRemarks - 1).Width = 90


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNoPrefix - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNoSeq - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Width = 90

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Function MakeSQL(pType As String) As String

        On Error GoTo ERR1

        ''SELECT CLAUSE...

        MakeSQL = " Select '', IH.DCDATE, IH.AUTO_KEY_DESP, " & vbCrLf _
            & " IH.INVOICE_DATE, IH.BILLNO,  " & vbCrLf _
            & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, IH.VENDOR_CODE," & vbCrLf _
            & " IH.NETVALUE, IH.ITEMVALUE," & vbCrLf _
            & " TO_CHAR(NETCGST_AMOUNT+NETSGST_AMOUNT+NETIGST_AMOUNT)," & vbCrLf _
            & " GRNO,GRDATE," & vbCrLf _
            & " VEHICLENO,CARRIERS," & vbCrLf _
            & " TRANSPORTERBILLNO,TRANSPORTERBILLDATE," & vbCrLf _
            & " GRNNO, GRNDATE, GRN_RECD_QTY, GRN_ACCEPTED_QTY, GEN_REJ_QTY, " & vbCrLf _
            & " GRN_SHORTAGE_QTY, GRN_REMARKS, " & vbCrLf _
            & " BILLNOPREFIX, BILLNOSEQ, IH.ADDUSER, IH.ADDDATE, IH.MODUSER, IH.MODDATE, IH.MKEY "

        '''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVMST.CODE"

        If cboInvoiceType.Text = "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IDENTIFICATION<>'J'"
        Else
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TRNTYPE=" & Val(lblTrnType.Text) & ""
        End If

        If optEntryType(0).Checked = True Then
            If cboStatus.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & "AND (IH.GRNNO IS NULL OR IH.GRNNO='')"
            ElseIf cboStatus.SelectedIndex = 2 Then
                MakeSQL = MakeSQL & vbCrLf & "AND (IH.GRNNO IS NOT NULL)"
            End If
        Else
            If cboStatus.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & "AND (IH.GRNO IS NULL OR IH.GNNO='')"
            ElseIf cboStatus.SelectedIndex = 2 Then
                MakeSQL = MakeSQL & vbCrLf & "AND (IH.GRNO <>'')"
            End If
        End If
        'If Trim(txtGRNo.Text) <> "" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.GRNO='" & MainClass.AllowSingleQuote(txtGRNo.Text) & "'"
        'End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If
            MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If pType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & "AND 1=2"
        End If

        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.BILLNO, IH.INVOICE_DATE"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLGP(pType As String) As String

        On Error GoTo ERR1

        ''SELECT CLAUSE...

        MakeSQLGP = " SELECT '', IH.GATEPASS_DATE, IH.GATEPASS_NO, " & vbCrLf _
            & " IH.GATEPASS_DATE, IH.AUTO_KEY_PASSNO,  " & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, '', " & vbCrLf _
            & " ''," & vbCrLf & " ''," & vbCrLf _
            & " GRNO,GRDATE," & vbCrLf _
            & " VEHICLE_NO,CARRIERS," & vbCrLf _
            & " TRANSPORTERBILLNO,TRANSPORTERBILLDATE," & vbCrLf _
            & " '',IH.AUTO_KEY_PASSNO,IH.AUTO_KEY_PASSNO "

        ''FROM CLAUSE...
        MakeSQLGP = MakeSQLGP & vbCrLf & " FROM INV_GATEPASS_HDR IH, FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...
        MakeSQLGP = MakeSQLGP & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PASSNO,LENGTH(IH.AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        If optEntryType(0).Checked = True Then
            'If cboStatus.SelectedIndex = 1 Then
            '    MakeSQLGP = MakeSQLGP() & vbCrLf & "AND (IH.GRNNO IS NULL OR IH.GRNNO='')"
            'ElseIf cboStatus.SelectedIndex = 2 Then
            '    MakeSQLGP = MakeSQLGP() & vbCrLf & "AND (IH.GRNNO <>'')"
            'End If
        Else
            If cboStatus.SelectedIndex = 1 Then
                MakeSQLGP = MakeSQLGP & vbCrLf & "AND (IH.GRNO IS NULL OR IH.GNNO='')"
            ElseIf cboStatus.SelectedIndex = 2 Then
                MakeSQLGP = MakeSQLGP & vbCrLf & "AND (IH.GRNO <>'')"
            End If
        End If

        'If Trim(txtGRNo.Text) <> "" Then
        '    MakeSQLGP = MakeSQLGP & vbCrLf & "AND IH.GRNO='" & MainClass.AllowSingleQuote(txtGRNo.Text) & "'"
        'End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If
            MakeSQLGP = MakeSQLGP & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        MakeSQLGP = MakeSQLGP & vbCrLf & " AND IH.GATEPASS_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.GATEPASS_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If pType = "L" Then
            MakeSQLGP = MakeSQLGP & vbCrLf & "AND 1=2"
        End If

        ''ORDER CLAUSE...

        MakeSQLGP = MakeSQLGP & vbCrLf & "ORDER BY IH.AUTO_KEY_PASSNO, IH.GATEPASS_DATE"

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

        Dim mBillAmount As Double
        Dim mSaleAmount As Double
        Dim mGST As Double

        Dim mFreight As Double
        Dim mDiscount As Double
        Dim mMSC As Double
        Dim mOthCharges As Double

        Dim mRow As UltraGridRow


        With UltraGrid1
            For cntRow = 0 To UltraGrid1.Rows.Count - 1
                mRow = Me.UltraGrid1.Rows(cntRow)
                mBillAmount = mBillAmount + mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillAmount - 1))
                mSaleAmount = mSaleAmount + mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColSaleAmount - 1))
                mGST = mGST + mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColGST - 1))
            Next

            'Dim newrow1 As UltraDataRow
            'newrow1 = Me.UltraDataSource1.Rows(UltraDataSource1.Rows.Count - 1)
            'newrow1.SetCellValue(0, True)

            'Call MainClass.AddBlankfpSprdRow(SprdMain, ColBillNo)
            '.Col = ColPartyName
            '.Row = .MaxRows
            '.Text = "GRAND TOTAL :"
            '.Font = VB6.FontChangeBold(.Font, True)

            '.Row = .MaxRows
            '.Row2 = .MaxRows
            '.Col = 1
            '.Col2 = .MaxCols
            '.BlockMode = True
            '.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            '.BlockMode = False

            '.Row = .MaxRows

            '.Col = ColBillAmount
            '.Text = VB6.Format(mBillAmount, "0.00")

            '.Col = ColSaleAmount
            '.Text = VB6.Format(mSaleAmount, "0.00")

            '.Col = ColBED
            '.Text = VB6.Format(mBED, "0.00")

            '.Col = ColCST
            '.Text = VB6.Format(mCST, "0.00")

            '.Col = ColHGST
            '.Text = VB6.Format(mHGST, "0.00")

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick


        Dim xVDate As String
        Dim xMkey As String = ""
        Dim xVNoSeq As String
        Dim xVNoPrefix As String
        Dim xBookType As String = ""
        Dim xBookSubType As String

        'Dim mDNNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.Rows.Count < 1 Then Exit Sub

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        xVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1))
        xMkey = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))
        xVNoSeq = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNoSeq - 1))
        xVNoPrefix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNoPrefix - 1))

        If optType(0).Checked = True Then
            Call ShowGRForm(xMkey, xVDate, "", xVNoSeq, xVNoPrefix, "S", "", "S")
        Else
            Call ShowGRForm(xMkey, xVDate, "", xVNoSeq, xVNoPrefix, "", "", "G")
        End If


    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        'Dim xVDate As String
        'Dim xMkey As String = ""
        'Dim xVNo As String
        'Dim xBookType As String = ""
        'Dim xBookSubType As String


        'SprdMain.Row = SprdMain.ActiveRow

        'SprdMain.Col = ColBillDate
        'xVDate = Me.SprdMain.Text

        'SprdMain.Col = ColMKEY
        'xMkey = Me.SprdMain.Text

        'SprdMain.Col = ColBillNo
        'xVNo = Me.SprdMain.Text

        'If optType(0).Checked = True Then
        '    Call ShowGRForm(xMkey, xVDate, "", xVNo, "S", "", "S")
        'Else
        '    Call ShowGRForm(xMkey, xVDate, "", xVNo, "", "", "G")
        'End If
    End Sub
    Private Sub ShowGRForm(ByRef MymKey As String, ByRef MyDate As String, ByRef MyVType As String, ByRef MyVnoSeq As String, ByRef MyVnoPrefix As String, MyBookType As String, ByRef MyBookSubType As String, ByRef mType As String)

        Dim FormLoaded As Boolean


        If optEntryType(0).Checked = True Then
            If optType(0).Checked = True Then
                FrmGRNUpdate.lblMKey.Text = MymKey
                FrmGRNUpdate.txtBillNoPrefix.Text = MyVnoPrefix
                FrmGRNUpdate.txtBillNo.Text = MyVnoSeq
                FrmGRNUpdate.lblType.Text = mType

                FrmGRNUpdate.ShowDialog()

                FrmGRNUpdate.FrmGRNUpdate_Activated(Nothing, New System.EventArgs())
            End If
        Else
            FrmGRUpdate.lblMKey.Text = MymKey
            FrmGRUpdate.txtBillNoPrefix.Text = MyVnoPrefix
            FrmGRUpdate.txtBillNo.Text = MyVnoSeq
            FrmGRUpdate.txtBillNoPrefixTo.Text = MyVnoPrefix
            FrmGRUpdate.txtBillNoTo.Text = MyVnoSeq
            FrmGRUpdate.lblType.Text = mType
            FrmGRUpdate.ShowDialog()

            FrmGRUpdate.FrmGRUpdate_Activated(Nothing, New System.EventArgs())
        End If


        FormLoaded = True
    End Sub
    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent)
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            'SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
    End Sub

    Private Sub TxtGRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
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
End Class
