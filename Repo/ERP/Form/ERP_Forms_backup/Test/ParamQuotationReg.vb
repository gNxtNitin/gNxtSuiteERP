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
Friend Class frmParamQuotationReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColIndentNo As Short = 2
    Private Const colIndentDate As Short = 3
    Private Const colQuotationNo As Short = 4
    Private Const colQuotaionDate As Short = 5
    Private Const ColDeptCode As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemDesc As Short = 8
    Private Const ColUnit As Short = 9
    Private Const ColSupplierCode As Short = 10
    Private Const ColSupplierName As Short = 11


    Private Const ColPrice As Short = 12
    Private Const ColDiscount As Short = 13
    Private Const ColDeliveryTime As Short = 14
    Private Const ColCredibility As Short = 15
    Private Const ColItemRemarks As Short = 16
    Private Const ColRemarks As Short = 17

    '    IH.AUTO_KEY_QUOT,
    'IH.QUOTATION_DATE,
    'IH.SUPP_CUST_CODE,
    'IH.QUOTATION_STATUS,
    'IH.REMARKS,
    'ID.AUTO_KEY_INDENT,
    'ID.ITEM_PRICE,
    'ID.DISCOUNT,
    'ID.DELIVERY_TIME,
    'ID.CREDIBILITY,
    'ID.REMARKS,
    'ID.QUOTATION_APP,
    'ID.ITEM_CODE,
    'ID.COMPANY_CODE,


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboHODApp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboHODApp.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboPriority_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPriority.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboSendBack_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If ChkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdSearch.Enabled = False
        Else
            txtItemName.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonIndent(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonIndent(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonIndent(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()
        mTitle = "Indent Register"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IndentReg.RPT"

        SqlStr = MakeSQL("S")
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim Printer As New Printer

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        If chkAllName.CheckState = False Then
            MainClass.AssignCRptFormulas(Report1, "IndentedByName=""" & txtEmpName.Text & """")
        End If
        Report1.WindowShowGroupTree = False

        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then
        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt

        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            Report1.PrinterSelect()
        '            Exit For
        '        End If
        '    Next prt
        'End If

        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        'FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamQuotationReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Indent Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamQuotationReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        cmdSearch.Enabled = False
        cmdSearchName.Enabled = False

        Call PrintStatus(True)
        Call FillIndentCombo()
        Call Show1("L")
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamQuotationReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width - 5)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 250, mReFormWidth - 250, mReFormWidth))


        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth)) '' VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamQuotationReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim xIndentNo As Double
        Dim mm As New Form

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)


        xIndentNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(colQuotationNo - 1))



        'If cboHODApp.SelectedIndex = 2 Then
        '    XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "mnuIndentHODApp", PubDBCn)
        '    If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
        '        Exit Sub
        '    End If
        'Else
        '    XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "mnuIndentEntry", PubDBCn)
        '    If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
        '        Exit Sub
        '    End If
        'End If

        FrmQuotation.MdiParent = Me.ParentForm

        FrmQuotation.Show()
        'If cboHODApp.SelectedIndex = 2 Then
        '    FrmIndentEntry.lblBookType.Text = "IH"
        'Else
        '    If cboStatus.SelectedIndex = 2 Then
        '        FrmIndentEntry.lblBookType.Text = "IA"
        '    Else
        '        FrmIndentEntry.lblBookType.Text = "II"
        '    End If
        'End If

        FrmQuotation.FrmQuotation_Activated(Nothing, New System.EventArgs())

        FrmQuotation.txtQuotationNo.Text = xIndentNo
        FrmQuotation.txtQuotationNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

        'FrmIndentEntry.Show()

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
            txtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
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
        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            txtItemName.Text = UCase(Trim(txtItemName.Text))
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

    Private Function Show1(pShowType As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL(pShowType)
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIndentNo - 1).Header.Caption = "Indent No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(colIndentDate - 1).Header.Caption = "Indent Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(colQuotationNo - 1).Header.Caption = "Quotation No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(colQuotaionDate - 1).Header.Caption = "Quotation Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDeptCode - 1).Header.Caption = "Dept Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Header.Caption = "Item Description"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnit - 1).Header.Caption = "UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierCode - 1).Header.Caption = "Supplier Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierName - 1).Header.Caption = "Supplier Name"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPrice - 1).Header.Caption = "Price"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDiscount - 1).Header.Caption = "Discount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDeliveryTime - 1).Header.Caption = "Delivery Time"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCredibility - 1).Header.Caption = "Payment Terms"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemRemarks - 1).Header.Caption = "Item Remarks"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRemarks - 1).Header.Caption = "Remarks"

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPrice - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDiscount - 1).CellAppearance.TextHAlign = HAlign.Right

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True

            '' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIndentNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(colIndentDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(colQuotationNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(colQuotaionDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDeptCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnit - 1).Width = 60

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSupplierName - 1).Width = 250


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPrice - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDiscount - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDeliveryTime - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCredibility - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemRemarks - 1).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRemarks - 1).Width = 200

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Function MakeSQL(ByVal pShowType As String) As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mEmployee As String
        Dim mDivision As Double

        ''SELECT CLAUSE...

        MakeSQL = " SELECT ''," & vbCrLf _
            & " ID.AUTO_KEY_INDENT," & vbCrLf _
            & " TO_CHAR(IIH.INDENT_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IH.AUTO_KEY_QUOT," & vbCrLf _
            & " TO_CHAR(IH.QUOTATION_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IIH.DEPT_CODE," & vbCrLf _
            & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " INVMST.PURCHASE_UOM, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, ID.ITEM_PRICE, " & vbCrLf _
            & " ID.DISCOUNT, " & vbCrLf _
            & " ID.DELIVERY_TIME, ID.CREDIBILITY, ID.REMARKS ITEMREMARKS, IH.REMARKS "


        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM PUR_QUOTATION_HDR IH, PUR_QUOTATION_DET ID, " & vbCrLf _
            & " PUR_INDENT_HDR IIH, INV_ITEM_MST INVMST, FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MakeSQL = MakeSQL & vbCrLf _
            & " And IH.AUTO_KEY_QUOT=ID.AUTO_KEY_QUOT" & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " And ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQL = MakeSQL & vbCrLf _
            & " And ID.AUTO_KEY_INDENT=IIH.AUTO_KEY_INDENT"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "And ID.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If

        If chkAllName.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtEmpName.Text, "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmployee = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IIH.IND_EMP_CODE='" & MainClass.AllowSingleQuote(mEmployee) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IIH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IIH.DIV_CODE=" & mDivision & ""
            End If
        End If

        'If cboPriority.Text <> "ALL" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND ID.PRIORITY_LEVEL='" & VB.Left(cboPriority.Text, 1) & "'"
        'End If


        If cboHODApp.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.QUOTATION_STATUS='" & Mid(cboHODApp.Text, 1, 1) & "'"
        End If

        If cboStatus.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND NVL(ID.QUOTATION_APP,'N')='" & Mid(cboStatus.Text, 1, 1) & "'"
        End If


        'MakeSQL = MakeSQL & vbCrLf & "AND IH.APPROVAL_STATUS='Y'"

        MakeSQL = MakeSQL & vbCrLf & " AND IH.QUOTATION_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.QUOTATION_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If pShowType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & "AND 1=2"
        End If

        ''ORDER CLAUSE...
        If OptOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IIH.AUTO_KEY_INDENT, IIH.INDENT_DATE,IH.AUTO_KEY_QUOT"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IIH.DEPT_CODE,IIH.AUTO_KEY_INDENT, IIH.INDENT_DATE,IH.AUTO_KEY_QUOT"
        ElseIf OptOrderBy(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IIH.AUTO_KEY_INDENT, IIH.INDENT_DATE,IH.AUTO_KEY_QUOT"
        End If
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
        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
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
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillIndentCombo()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDept.Items.Clear()

        SqlStr = "SELECT DEPT_DESC FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDept.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDept.Items.Add(RS.Fields("DEPT_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDept.SelectedIndex = 0

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

        cboPriority.Items.Clear()
        cboPriority.Items.Add("ALL")
        cboPriority.Items.Add("Regular")
        cboPriority.Items.Add("Urgent")
        cboPriority.Items.Add("Most Urgent")
        cboPriority.SelectedIndex = 0

        cboHODApp.Items.Clear()
        cboHODApp.Items.Add("BOTH")
        cboHODApp.Items.Add("YES")
        cboHODApp.Items.Add("NO")
        cboHODApp.SelectedIndex = 0

        CboStatus.Items.Clear()
        CboStatus.Items.Add("BOTH")
        cboStatus.Items.Add("YES")
        cboStatus.Items.Add("NO")
        cboStatus.SelectedIndex = 0

        'cboSendBack.Items.Clear()
        'cboSendBack.Items.Add("ALL")
        'cboSendBack.Items.Add("Yes")
        'cboSendBack.Items.Add("No")
        'cboSendBack.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.DoubleClick
        SearchEmpName()
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEmpName()
    End Sub

    Private Sub txtEmpName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtEmpName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtEmpName.Text), "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtEmpName.Text = UCase(Trim(txtEmpName.Text))
        Else
            MsgInformation("No Such Emp in Master.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchName.Click
        SearchEmpName()
    End Sub
    Private Sub SearchEmpName()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtEmpName.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr)
        If AcName <> "" Then
            txtEmpName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub chkAllName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllName.CheckStateChanged
        Call PrintStatus(False)
        If chkAllName.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpName.Enabled = False
            cmdSearchName.Enabled = False
        Else
            txtEmpName.Enabled = True
            cmdSearchName.Enabled = True
        End If
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
