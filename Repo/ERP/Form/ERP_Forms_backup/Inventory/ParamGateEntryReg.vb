Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Data.OleDb
'Imports Infragistics.Win.UltraWinTabControl
Friend Class frmParamGateEntryReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColGateNo As Short = 2
    Private Const ColGateDate As Short = 3
    Private Const ColMRRNo As Short = 4
    Private Const ColMRRDate As Short = 5
    Private Const colSupplier As Short = 6
    Private Const colShipSupplier As Short = 7
    Private Const ColBillNo As Short = 8
    Private Const ColBillDate As Short = 9
    Private Const ColItemCode As Short = 10
    Private Const ColItemDesc As Short = 11
    Private Const ColUnit As Short = 12
    Private Const ColQty As Short = 13
    Private Const ColRate As Short = 14
    Private Const ColAmount As Short = 15

    Private Const ColThickness As Short = 16
    Private Const ColColor As Short = 17



    Private Const ColPONo As Short = 18
    Private Const ColPODate As Short = 19
    Private Const ColOldGateEntryNo As Short = 20
    Private Const ColCompanyName As Short = 21
    Private Const ColRefType As Short = 22

    Private Const ColMKEY As Short = 23

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

    Private Sub cboRefType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRefType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub


    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub chkAllSupp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSupp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSupplier.Enabled = False
            cmdsearchSupp.Enabled = False
        Else
            txtSupplier.Enabled = True
            cmdsearchSupp.Enabled = True
        End If
    End Sub


    Private Sub chkMRRMade_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMRRMade.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkTime_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTime.CheckStateChanged
        Call PrintStatus(False)
        If chkTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTMFrom.Enabled = False
            txtTMTo.Enabled = False
        Else
            txtTMFrom.Enabled = True
            txtTMTo.Enabled = True
        End If
        txtTMFrom.Text = GetServerTime()
        txtTMTo.Text = GetServerTime()
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONMRR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONMRR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONMRR(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()

        mTitle = "Gate Entry Register" & IIf(chkMRRMade.CheckState = System.Windows.Forms.CheckState.Checked, " (Pending List) ", "")

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GateEntryReg.rpt"

        SqlStr = MakeSQL("S")
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub

    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        'FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Private Sub frmParamGateEntryReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Gate Entry Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamGateEntryReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False


        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        cboRefType.Items.Clear()
        cboRefType.Items.Add("ALL")
        cboRefType.Items.Add("Purchase Order")
        cboRefType.Items.Add("Job Work Order")
        cboRefType.Items.Add("Invoice-Sale Return")
        cboRefType.Items.Add("Free of Cost")
        cboRefType.Items.Add("Returnable Gate Pass")
        cboRefType.Items.Add("Cash Purchase")
        cboRefType.Items.Add("1 : Job Work Return")
        cboRefType.Items.Add("2 : Sale Return Under Warranty")

        cboRefType.SelectedIndex = 0

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

        cboCompany.Enabled = True

        oledbCnn = New OleDbConnection(StrConn)

        SqlStr = "Select COMPANY_NAME, COMPANY_CODE " & vbCrLf _
            & " FROM GEN_COMPANY_MST"

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE IN (SELECT COMPANY_CODE FROM GEN_COMPANYRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') "

        SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN (SELECT COMPANY_CODE FROM FIN_RIGHTS_MST WHERE USERID='" & PubUserID & "' AND MENUHEAD='" & myMenu & "') "


        SqlStr = SqlStr & vbCrLf & "ORDER BY COMPANY_NAME "


        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboCompany.DataSource = ds
        cboCompany.DataMember = ""
        Dim c As UltraGridColumn = Me.cboCompany.DisplayLayout.Bands(0).Columns.Add()
        With c
            .Key = "Selected"
            .Header.Caption = String.Empty
            .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
            .DataType = GetType(Boolean)
            .DataType = GetType(Boolean)
            .Header.VisiblePosition = 0
        End With
        cboCompany.CheckedListSettings.CheckStateMember = "Selected"
        cboCompany.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        ' Set up the control to use a custom list delimiter 
        cboCompany.CheckedListSettings.ListSeparator = " , "
        ' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        cboCompany.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        cboCompany.DisplayMember = "COMPANY_NAME"
        cboCompany.ValueMember = "COMPANY_CODE"

        cboCompany.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Company Name"
        cboCompany.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Company Code"


        cboCompany.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboCompany.DisplayLayout.Bands(0).Columns(1).Width = 100


        cboCompany.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown



        oledbAdapter.Dispose()
        oledbCnn.Close()

        If Show1("L") = False Then GoTo BSLError     ''CreateGridHeader("L")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamGateEntryReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamGateEntryReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
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

    '    Dim cntSearchRow As Integer
    '    Dim mSearchKey As String
    '    Dim mCol As Integer

    '    mCol = SprdMain.ActiveCol
    '    If eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
    '        cntSearchRow = 1
    '        mSearchKey = ""
    '        mSearchKey = InputBox("Enter Search String :", "Search", mSearchKey)
    '        If mSearchKey <> "" Then
    '            MainClass.SearchIntoGrid(SprdMain, mCol, mSearchKey, cntSearchRow)
    '            cntSearchRow = cntSearchRow + 1
    '        End If
    '        SprdMain.Focus()
    '    End If
    'End Sub
    'Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    Dim SqlStr As String = ""
    '    Dim RsTemp As ADODB.Recordset = Nothing
    '    Dim xGateNo As Double
    '    'Dim xQCStatus As String
    '    Dim xGateDate As String

    '    SprdMain.Row = SprdMain.ActiveRow

    '    SprdMain.Col = ColGateNo
    '    xGateNo = Val(SprdMain.Text)

    '    SprdMain.Col = ColGateDate
    '    xGateDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

    '    If FYChk(CStr(CDate(xGateDate))) = False Then
    '        Exit Sub
    '    End If


    '    '    SqlStr = "SELECT * from INV_GATE_HDR WHERE AUTO_KEY_MRR=" & xMRRNo & ""
    '    '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly

    '    '    If RsTemp.EOF = False Then
    '    FrmGateEntry.MdiParent = Me.MdiParent
    '    FrmGateEntry.Show()

    '    FrmGateEntry.FrmGateEntry_Activated(Nothing, New System.EventArgs())

    '    FrmGateEntry.txtMRRNo.Text = CStr(xGateNo)
    '    FrmGateEntry.TxtMRRNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

    '    '    End If
    'End Sub


    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            txtCategory.Text = UCase(Trim(txtCategory.Text))
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
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    'Private Sub FormatSprdMain(ByRef Arow As Integer)

    '    Dim cntCol As Integer
    '    With SprdMain
    '        .MaxCols = ColMKEY
    '        .set_RowHeight(0, RowHeight * 1.2)
    '        .set_ColWidth(0, 4.5)

    '        .set_RowHeight(-1, RowHeight)
    '        .Row = -1

    '        .Col = ColLocked
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColLocked, 15)
    '        .ColHidden = True

    '        .Col = ColMRRNo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColMRRNo, 9)

    '        .Col = ColMRRDate
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColMRRDate, 9)

    '        .Col = colSupplier
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(colSupplier, 20)

    '        .Col = colShipSupplier
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(colShipSupplier, 20)

    '        .Col = ColItemCode
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColItemCode, 8)

    '        .Col = ColItemDesc
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColItemDesc, 25)

    '        .Col = ColUnit
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColUnit, 4)

    '        For cntCol = ColQty To ColAmount
    '            .Col = cntCol
    '            .CellType = SS_CELL_TYPE_FLOAT
    '            .TypeFloatDecimalPlaces = 3
    '            .TypeFloatMin = CDbl("-99999999999")
    '            .TypeFloatMax = CDbl("99999999999")
    '            .TypeFloatMoney = False
    '            .TypeFloatSeparator = False
    '            .TypeFloatDecimalChar = Asc(".")
    '            .TypeFloatSepChar = Asc(",")
    '            .set_ColWidth(cntCol, 9)
    '        Next


    '        .Col = ColRate
    '        .ColHidden = True '' IIf(cboRefType.ListIndex = 5, False, True)

    '        .Col = ColAmount
    '        .ColHidden = IIf(cboRefType.SelectedIndex = 5, False, True)

    '        .Col = ColBillNo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColBillNo, 9)

    '        .Col = ColBillDate
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColBillDate, 9)

    '        .Col = ColPONo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColPONo, 12)
    '        .ColHidden = True

    '        .Col = ColPODate
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColPODate, 9)
    '        .ColHidden = True

    '        '        .Col = ColST38No
    '        '        .CellType = SS_CELL_TYPE_EDIT
    '        '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        '        .TypeEditLen = 255
    '        '        .TypeEditMultiLine = True
    '        '        .ColWidth(ColST38No) = 10
    '        '        .ColHidden = True

    '        .Col = ColOldGateEntryNo
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColOldGateEntryNo, 12)
    '        .ColHidden = False

    '        .Col = ColMKEY
    '        .CellType = SS_CELL_TYPE_EDIT
    '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
    '        .TypeEditLen = 255
    '        .TypeEditMultiLine = True
    '        .set_ColWidth(ColMKEY, 8)
    '        .ColHidden = True


    '        MainClass.SetSpreadColor(SprdMain, -1)
    '        MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
    '        '        SprdMain.OperationMode = OperationModeSingle
    '        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
    '        SprdMain.DAutoCellTypes = True
    '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '        SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
    '    End With
    'End Sub
    Private Function Show1(pType As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL(pType)
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

    Private Function MakeSQL(mType As String) As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mDivision As Double

        ''SELECT CLAUSE...


        MakeSQL = " SELECT ''," & vbCrLf _
            & " IGH.AUTO_KEY_GATE," & vbCrLf _
            & " TO_CHAR(IGH.GATE_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IGH.MRR_NO," & vbCrLf _
            & " TO_CHAR(IGH.MRRDATE,'DD/MM/YYYY')," & vbCrLf _
            & " SCMST.SUPP_CUST_NAME," & vbCrLf _
            & " CMST.SUPP_CUST_NAME," & vbCrLf _
            & " IGH.BILL_NO , IGH.BILL_DATE, " & vbCrLf _
            & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " IGD.ITEM_UOM, IGD.BILL_QTY, " & vbCrLf _
            & " IGD.ITEM_RATE, IGD.BILL_QTY*IGD.ITEM_RATE," & vbCrLf _
            & " INVMST.MAT_THICHNESS, INVMST.ITEM_COLOR, IGD.REF_AUTO_KEY_NO,IGD.REF_DATE, IGH.OLD_ERP_NO," & vbCrLf _
            & " CC.COMPANY_NAME,"

        MakeSQL = MakeSQL & vbCrLf _
            & " CASE WHEN IGH.REF_TYPE='P' THEN 'Purchase Order' " & vbCrLf _
            & " WHEN IGH.REF_TYPE='J' THEN 'Job Work Order' " & vbCrLf _
            & " WHEN IGH.REF_TYPE='I' THEN 'Invoice-Sale Return'" & vbCrLf _
            & " WHEN IGH.REF_TYPE='F' THEN 'Free of Cost'" & vbCrLf _
            & " WHEN IGH.REF_TYPE='R' THEN 'Returnable Gate Pass'" & vbCrLf _
            & " WHEN IGH.REF_TYPE='C' THEN 'Cash Purchase'" & vbCrLf _
            & " WHEN IGH.REF_TYPE='1' THEN 'Job Work Rejection'" & vbCrLf _
            & " WHEN IGH.REF_TYPE='2' THEN 'Sale Return Under Warranty'" & vbCrLf _
            & " WHEN IGH.REF_TYPE='3' THEN 'Sale Return RM/BOP'" & vbCrLf _
            & " ELSE 'Inter Unit Purchase' END REF_TYPE,"

        MakeSQL = MakeSQL & vbCrLf & " IGH.AUTO_KEY_GATE"

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM INV_GATEENTRY_HDR IGH, INV_GATEENTRY_DET IGD," & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST SCMST, INV_ITEM_MST INVMST,GEN_COMPANY_MST CC"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE CC.COMPANY_CODE=IGH.COMPANY_CODE" & vbCrLf _
            & " And IGH.AUTO_KEY_GATE=IGD.AUTO_KEY_GATE" & vbCrLf _
            & " And IGH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " And IGH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " And IGH.COMPANY_CODE=SCMST.COMPANY_CODE" & vbCrLf & " And DECODE(SHIPPED_TO_SAMEPARTY,'Y',IGH.SUPP_CUST_CODE,IGH.SHIPPED_TO_PARTY_CODE)=SCMST.SUPP_CUST_CODE " & vbCrLf & " AND IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE "

        Dim mCompanyCode As String

        If cboCompany.Text.Trim <> "" Then
            For Each r As UltraGridRow In cboCompany.CheckedRows
                If mCompanyCode <> "" Then
                    mCompanyCode += "," & "" & r.Cells("COMPANY_CODE").Value.ToString() & ""
                Else
                    mCompanyCode += "" & r.Cells("COMPANY_CODE").Value.ToString() & ""
                End If
            Next
        End If

        If mCompanyCode = "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.COMPANY_CODE IN (" & mCompanyCode & ")"
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If chkMRRMade.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.MRR_MADE='N' "
        End If


        If cboRefType.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "' "
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        '
        '    MakeSQL = MakeSQL & vbCrLf _
        ''            & " AND IGH.GATE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        ''            & " AND IGH.GATE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        If chkTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & " AND IGH.GATE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.GATE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND TO_CHAR(IGH.GATE_DATE,'YYYYMMDD')||TO_CHAR(IGH.ADDDATE,'HH24MI')>=TO_CHAR('" & VB6.Format(txtDateFrom.Text, "YYYYMMDD") & "') || '" & VB6.Format(txtTMFrom.Text, "HHMM") & "'" & vbCrLf & " AND TO_CHAR(IGH.GATE_DATE,'YYYYMMDD')||TO_CHAR(IGH.ADDDATE,'HH24MI')<=TO_CHAR('" & VB6.Format(txtDateTo.Text, "YYYYMMDD") & "') || '" & VB6.Format(txtTMTo.Text, "HHMM") & "'"
        End If

        If mType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & " AND 1=2"
        End If

        If OptOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IGH.GATE_DATE, IGH.AUTO_KEY_GATE, IGD.SERIAL_NO"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,INVMST.ITEM_SHORT_DESC,IGH.GATE_DATE,IGH.AUTO_KEY_GATE"
        End If

        '''End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSupplier.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
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
        '    If FYChk(CDate(txtDateTo.Text)) = False Then
        '        txtDateTo.SetFocus
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    'Private Sub FillPOCombo()
    'On Error GoTo FillErr2
    'Dim SqlStr As String = ""
    'Dim RS As ADODB.Recordset=Nothing
    '
    ''    cboPurType.Clear
    ''    cboPurType.AddItem "ALL"
    ''    cboPurType.AddItem "Purchase Order"
    ''    cboPurType.AddItem "Work Order"
    ''    cboPurType.AddItem "Job Order"
    ''    cboPurType.ListIndex = 0
    ''
    ''    cboOrderType.Clear
    ''    cboOrderType.AddItem "ALL"
    ''    cboOrderType.AddItem "Close"
    ''    cboOrderType.AddItem "Open"
    ''    cboOrderType.ListIndex = 0
    '
    '
    ''    Exit Sub
    'FillErr2:
    '    MsgBox err.Description
    'End Sub

    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        lblAcCode.Text = ""
        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub
    Private Sub txtEmpName_Change()
        Call PrintStatus(False)
    End Sub

    Private Sub txtTMFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTMFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtTMTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
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

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGateNo - 1).Header.Caption = "Gate Entry No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGateDate - 1).Header.Caption = "Gate Entry Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRRNo - 1).Header.Caption = "MRR No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRRDate - 1).Header.Caption = "MRR Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(colSupplier - 1).Header.Caption = "Supplier(Billed)"
            UltraGrid1.DisplayLayout.Bands(0).Columns(colShipSupplier - 1).Header.Caption = "Supplier(Shiped)"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Header.Caption = "Bill Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Header.Caption = "Item Description"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnit - 1).Header.Caption = "Item UOM"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Header.Caption = "Bill Quantity"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).Header.Caption = "Rate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).Header.Caption = "Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Header.Caption = "Thickness"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Header.Caption = "Color"



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1).Header.Caption = "PO No"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPODate - 1).Header.Caption = "PO Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOldGateEntryNo - 1).Header.Caption = "Old ERP Entry no"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCompanyName - 1).Header.Caption = "Company Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefType - 1).Header.Caption = "Ref Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "MKey"


            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).Style = UltraWinGrid.ColumnStyle.Double


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).CellAppearance.TextHAlign = HAlign.Right

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGateNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGateDate - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRRNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMRRDate - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(colSupplier - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(colShipSupplier - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBillDate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnit - 1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Width = 90


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAmount - 1).Width = 100

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Hidden = False
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Hidden = False
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Width = 80
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Width = 80
            Else
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColThickness - 1).Hidden = True
                UltraGrid1.DisplayLayout.Bands(0).Columns(ColColor - 1).Hidden = True
            End If


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPODate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOldGateEntryNo - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCompanyName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefType - 1).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Width = 90

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        UltraDataSource2.Rows.Clear()
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
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn

        Dim xGateNo As Double
        Dim xGateDate As String



        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)

        xGateNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColGateNo - 1))
        xGateDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColGateDate - 1))

        If xGateDate = "" Then Exit Sub

        If CDate(VB6.Format(xGateDate, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")) Then
            MsgInformation("Cann't open Last Year Voucher")
            Exit Sub
        End If


        FrmGateEntry.MdiParent = Me.MdiParent
        FrmGateEntry.Show()

        FrmGateEntry.FrmGateEntry_Activated(Nothing, New System.EventArgs())

        FrmGateEntry.txtMRRNo.Text = CStr(xGateNo)
        FrmGateEntry.TxtMRRNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

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

    Private Sub UltraGrid1_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles UltraGrid1.InitializeLayout

        ''Allowing Summaries in the UltraGrid 
        e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True
        '' Setting the Sum Summary for the desired column

        e.Layout.Bands(0).Summaries.Add("ColQty", SummaryType.Sum, e.Layout.Bands(0).Columns(ColQty - 1))
        e.Layout.Bands(0).Summaries.Add("ColAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColAmount - 1))


        ''Set the display format to be just the number 
        e.Layout.Bands(0).Summaries("ColQty").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColAmount").DisplayFormat = "{0:###0.00}"


        ''Hide the SummaryFooterCaption row 
        e.Layout.Bands(0).Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False

        'band.SummaryFooterCaption = "Subtotal:"

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.BackColor = Color.LightSteelBlue

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black
        '     / Here, I want to add grand total

        e.Layout.Bands(0).Summaries("ColQty").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColAmount").Appearance.TextHAlign = HAlign.Right

        'Disable grid default highlight

        'UltraGrid1.DisplayLayout.Override.ResetActiveRowAppearance()

        'UltraGrid1.DisplayLayout.Override.ResetActiveCellAppearance()

        'UltraGrid1.DisplayLayout.Override.ActiveAppearancesEnabled = DefaultableBoolean.False

        e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy
    End Sub
End Class
