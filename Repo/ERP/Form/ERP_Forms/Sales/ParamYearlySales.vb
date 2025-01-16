Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmParamYearlySalesReport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    ''''Private PvtDBCn As ADODB.Connection				


    Dim mAccountCode As Integer
    Private Const ColCustomerName As Short = 1
    Private Const ColCustomerLoc As Short = 2
    Private Const ColCustomerVC As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemName As Short = 5
    Private Const ColPartNo As Short = 6
    Private Const ColTariff As Short = 7
    Private Const ColType As Short = 8
    Private Const ColApr As Short = 9
    Private Const ColMay As Short = 10
    Private Const ColJun As Short = 11
    Private Const ColJul As Short = 12
    Private Const ColAug As Short = 13
    Private Const ColSep As Short = 14
    Private Const ColOct As Short = 15
    Private Const ColNov As Short = 16
    Private Const ColDec As Short = 17
    Private Const ColJan As Short = 18
    Private Const ColFeb As Short = 19
    Private Const ColMar As Short = 20
    Private Const ColTotal As Short = 21
    Private Const ColRate As Short = 22
    Private Const ColPrevRate As Short = 23

    Dim mClickProcess As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
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
    'Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    Call PrintStatus(False)
    '    If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
    '        txtItemName.Enabled = False
    '        cmdsearchItem.Enabled = False
    '    Else
    '        txtItemName.Enabled = True
    '        cmdsearchItem.Enabled = True
    '    End If
    'End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        MainClass.AssignCRptFormulas(Report1, "SHOWWISE=""" & IIf(optWise(0).Checked = True, "A", IIf(optWise(1).Checked = True, "Q", "G")) & """")


        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSubTitle1 As String

        Report1.Reset()


        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows - 1, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")


        mTitle = "Yearly Sales Reports" & IIf(optWise(0).Checked = True, " ( Value Wise ) ", IIf(optWise(1).Checked = True, " ( Qty Wise ) ", " ( GST Amount Wise ) "))

        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text

        mSubTitle1 = IIf(Trim(txtTariffHeading.Text) = "", "", "Tariff : " & txtTariffHeading.Text)
        mSubTitle = mSubTitle & IIf(mSubTitle1 = "", "", " (" & mSubTitle1 & ")")

        If optDetail.Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\YearlySalesReport.RPT"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\YearlySalesReportSumm.RPT"
        End If
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume				
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM TEMP_SALE_vs_PROD " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If optOrderBy(0).Checked = True Then
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY ITEM_DESC, TYPE"
        Else
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY ITEM_CODE, TYPE"
        End If

        FetchRecordForReport = mSqlStr

    End Function

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'SearchItem()
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
    Private Sub frmParamYearlySalesReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Yearly Sales Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamYearlySalesReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '''Set PvtDBCn = New ADODB.Connection				
        '''PvtDBCn.Open StrConn				

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        cboAccount.Enabled = True
        cboItem.Enabled = True
        'txtItemName.Enabled = False
        'cmdsearchItem.Enabled = False

        cboInterUnit.Items.Clear()
        cboInterUnit.Items.Add("ALL")
        cboInterUnit.Items.Add("NO")
        cboInterUnit.Items.Add("YES")
        cboInterUnit.SelectedIndex = 0

        Call FillInvoiceType()

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamYearlySalesReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamYearlySalesReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub


    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
        '    lstInvoiceType.ToolTipText = lstInvoiceType.Text				
    End Sub
    Private Sub lstInvoiceType_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lstInvoiceType.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        ToolTip1.SetToolTip(lstInvoiceType, lstInvoiceType.Text)
    End Sub

    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer
        Dim pCompanyCode As Long
        Dim mRights As String


        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim oledbAdapter1 As OleDbDataAdapter

        oledbCnn = New OleDbConnection(StrConn)

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT DISTINCT NAME FROM FIN_INVTYPE_MST " & vbCrLf _
            & " WHERE CATEGORY='S'"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            SqlStr = SqlStr & vbCrLf & " AND CODE IN (SELECT DISTINCT ACCOUNT_POSTING_CODE FROM FIN_INVOICE_DET)"
        Else
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("Name").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME, COMPANY_CODE FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                pCompanyCode = RS.Fields("COMPANY_CODE").Value
                mRights = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn, pCompanyCode)
                If mRights <> "" Then
                    lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                    lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                    CntLst = CntLst + 1
                End If
                RS.MoveNext()
            Loop
        End If

        lstCompanyName.SelectedIndex = 0


        SqlStr = "Select DISTINCT SUPP_CUST_NAME, SUPP_CUST_CODE, SUPP_CUST_ADDR,  SUPP_CUST_CITY, SUPP_CUST_STATE " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') ORDER BY SUPP_CUST_NAME"

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboAccount.DataSource = ds
        cboAccount.DataMember = ""
        cboAccount.ValueMember = "SUPP_CUST_CODE"
        cboAccount.DisplayMember = "SUPP_CUST_NAME"

        'Dim c As UltraGridColumn = Me.cboAccount.DisplayLayout.Bands(0).Columns.Add()
        'With c
        '    .Key = "Selected"
        '    .Header.Caption = String.Empty
        '    .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
        '    .DataType = GetType(Boolean)
        '    .DataType = GetType(Boolean)
        '    .Header.VisiblePosition = 0
        'End With
        'cboAccount.CheckedListSettings.CheckStateMember = "Selected"
        'cboAccount.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        '' Set up the control to use a custom list delimiter 
        'cboAccount.CheckedListSettings.ListSeparator = " , "
        '' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        'cboAccount.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item

        cboAccount.Appearance.FontData.SizeInPoints = 8.5

        cboAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        cboAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
        cboAccount.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
        cboAccount.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
        cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"
        ''cboAccount.DisplayLayout.Bands(0).Columns(4).Header.Caption = "GST No"

        cboAccount.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboAccount.DisplayLayout.Bands(0).Columns(1).Width = 100
        cboAccount.DisplayLayout.Bands(0).Columns(2).Width = 350
        cboAccount.DisplayLayout.Bands(0).Columns(3).Width = 100
        cboAccount.DisplayLayout.Bands(0).Columns(4).Width = 100

        cboAccount.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        cboAccount.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        'cboCompany.Rows(0).Selected = True


        oledbAdapter.Dispose()

        SqlStr = "Select DISTINCT ITEM_SHORT_DESC, ITEM_CODE, CUSTOMER_PART_NO " & vbCrLf _
            & " FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE IN (SELECT DISTINCT ITEM_CODE FROM FIN_INVOICE_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"

        SqlStr = SqlStr & vbCrLf & "ORDER BY ITEM_SHORT_DESC"

        oledbAdapter1 = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter1.Fill(ds1)

        ' Set the data source and data member to bind the grid.
        cboItem.DataSource = ds1
        cboItem.DataMember = ""
        cboItem.ValueMember = "ITEM_CODE"
        cboItem.DisplayMember = "ITEM_SHORT_DESC"

        'Dim c1 As UltraGridColumn = Me.cboItem.DisplayLayout.Bands(0).Columns.Add()
        'With c1
        '    .Key = "Selected"
        '    .Header.Caption = String.Empty
        '    .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
        '    .DataType = GetType(Boolean)
        '    .DataType = GetType(Boolean)
        '    .Header.VisiblePosition = 0
        'End With
        'cboItem.CheckedListSettings.CheckStateMember = "Selected"
        'cboItem.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        '' Set up the control to use a custom list delimiter 
        'cboItem.CheckedListSettings.ListSeparator = " , "
        '' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        'cboItem.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item


        cboItem.Appearance.FontData.SizeInPoints = 8.5

        cboItem.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Item Name"
        cboItem.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Item Code"
        cboItem.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Item Part no"


        cboItem.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboItem.DisplayLayout.Bands(0).Columns(1).Width = 100
        cboItem.DisplayLayout.Bands(0).Columns(2).Width = 100


        cboItem.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        cboItem.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        'cboCompany.Rows(0).Selected = True


        oledbAdapter1.Dispose()
        oledbCnn.Close()

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
    'Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
    '    SprdMain.Row = -1
    '    SprdMain.Col = Col
    '    SprdMain.DAutoCellTypes = True
    '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
    '    SprdMain.TypeEditLen = 1000
    'End Sub
    Private Sub txtDateFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    '    Private Sub SearchItem()

    '        On Error GoTo ERR1
    '        Dim SqlStr As String

    '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
    '        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr				
    '        MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  ,  , SqlStr)
    '        If AcName <> "" Then
    '            txtItemName.Text = AcName
    '            lblItemCode.Text = AcName1
    '        End If
    '        Exit Sub
    'ERR1:
    '        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    '    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColPrevRate
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 6)
            .ColHidden = IIf(optDetail.Checked = True, False, True)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 15)
            .ColsFrozen = ColItemName
            .ColHidden = IIf(optDetail.Checked = True, False, True)

            .Col = ColTariff
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTariff, 10)
            .ColHidden = IIf(optDetail.Checked = True, False, True)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartNo, 10)
            .ColHidden = IIf(optDetail.Checked = True, False, True)

            .Col = ColType
            .ColHidden = True

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerName, 25)

            .Col = ColCustomerLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerLoc, 15)
            .ColHidden = IIf(optSumm.Checked = True, True, False)

            .Col = ColCustomerVC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerVC, 15)

            For cntCol = ColApr To ColPrevRate
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

            .Col = ColRate
            .ColHidden = True   'IIf(optDetail.Checked = True, False, True)

            .Col = ColPrevRate
            .ColHidden = True   'IIf(optDetail.Checked = True, False, True)

            .Col = ColItemCode
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColItemName
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            '        .Col = ColTariff				
            '        .ColMerge = MergeAlways				


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            .Row = 0
            .Col = ColItemCode
            .Text = "Item Code"
            .Col = ColItemName
            .Text = "Item Name"
            .Col = ColTariff
            .Text = "Tariff"
            .Col = ColType
            .Text = "Type"
            .Col = ColApr
            .Text = "April"
            .Col = ColMay
            .Text = "May"
            .Col = ColJun
            .Text = "June"
            .Col = ColJul
            .Text = "July"
            .Col = ColAug
            .Text = "August"
            .Col = ColSep
            .Text = "September"
            .Col = ColOct
            .Text = "October"
            .Col = ColNov
            .Text = "November"
            .Col = ColDec
            .Text = "December"
            .Col = ColJan
            .Text = "January"
            .Col = ColFeb
            .Text = "February"
            .Col = ColMar
            .Text = "March"

            .Col = ColTotal
            .Text = IIf(optWise(0).Checked = True, "Total Amount", IIf(optWise(1).Checked = True, "Total Qty", "Total GST Amount"))

            .Col = ColRate
            .Text = "Current Rate"

            .Col = ColPrevRate
            .Text = "Previous Rate"

            .Col = ColCustomerName
            .Text = "Customer Name"

            .Col = ColCustomerLoc
            .Text = "Customer Location"

            .Col = ColCustomerVC
            .Text = "Customer Vendor Code"

            .Col = ColPartNo
            .Text = "Part No"

        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If InsertIntoTemp() = False Then GoTo LedgError

        SqlStr = ""

        If optDetail.Checked = True Then
            SqlStr = "SELECT " & vbCrLf _
                & " SUPP_CUST_NAME, BILL_TO_LOC_ID, VENDOR_CODE, ITEM_CODE, ITEM_DESC, PART_NO, TARIFF_CODE," & vbCrLf _
                & " DECODE(TYPE,'S','SALE','PRODUCTION') AS STYPE, " & vbCrLf _
                & " TO_CHAR(SUM(APR_QTY)) AS APR," & vbCrLf _
                & " TO_CHAR(SUM(MAY_QTY)) AS MAY," & vbCrLf _
                & " TO_CHAR(SUM(JUN_QTY)) AS JUN," & vbCrLf & " TO_CHAR(SUM(JUL_QTY)) AS JUL," & vbCrLf & " TO_CHAR(SUM(AUG_QTY)) AS AUG," & vbCrLf & " TO_CHAR(SUM(SEP_QTY)) AS SEP," & vbCrLf & " TO_CHAR(SUM(OCT_QTY)) AS OCT," & vbCrLf & " TO_CHAR(SUM(NOV_QTY)) AS NOV," & vbCrLf & " TO_CHAR(SUM(DEC_QTY)) AS DEC," & vbCrLf & " TO_CHAR(SUM(JAN_QTY)) AS JAN," & vbCrLf & " TO_CHAR(SUM(FEB_QTY)) AS FEB," & vbCrLf & " TO_CHAR(SUM(MAR_QTY)) AS MAR," & vbCrLf & " TO_CHAR(SUM(TOTQTY)) AS TOTQTY, " & vbCrLf & " ITEM_RATE, ITEM_RATE"
        Else

            If optSummLocWise.Checked = True Then
                SqlStr = "SELECT " & vbCrLf _
                    & " SUPP_CUST_NAME, BILL_TO_LOC_ID,VENDOR_CODE, '','', '','',"
            Else
                SqlStr = "SELECT " & vbCrLf _
                    & " SUPP_CUST_NAME, '',VENDOR_CODE, '', '','','',"
            End If

            SqlStr = SqlStr & vbCrLf _
                & " DECODE(TYPE,'S','SALE','PRODUCTION') AS STYPE, " & vbCrLf _
                & " TO_CHAR(SUM(APR_QTY)) AS APR," & vbCrLf _
                & " TO_CHAR(SUM(MAY_QTY)) AS MAY," & vbCrLf _
                & " TO_CHAR(SUM(JUN_QTY)) AS JUN," & vbCrLf _
                & " TO_CHAR(SUM(JUL_QTY)) AS JUL," & vbCrLf _
                & " TO_CHAR(SUM(AUG_QTY)) AS AUG," & vbCrLf _
                & " TO_CHAR(SUM(SEP_QTY)) AS SEP," & vbCrLf _
                & " TO_CHAR(SUM(OCT_QTY)) AS OCT," & vbCrLf _
                & " TO_CHAR(SUM(NOV_QTY)) AS NOV," & vbCrLf _
                & " TO_CHAR(SUM(DEC_QTY)) AS DEC," & vbCrLf & " TO_CHAR(SUM(JAN_QTY)) AS JAN," & vbCrLf _
                & " TO_CHAR(SUM(FEB_QTY)) AS FEB," & vbCrLf & " TO_CHAR(SUM(MAR_QTY)) AS MAR," & vbCrLf & " TO_CHAR(SUM(TOTQTY)) AS TOTQTY, " & vbCrLf & " 0, 0"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM TEMP_SALE_vs_PROD " & vbCrLf _
            & " WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"


        If optDetail.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_CODE, ITEM_DESC, TYPE,TARIFF_CODE,ITEM_RATE,PART_NO,SUPP_CUST_NAME,BILL_TO_LOC_ID, VENDOR_CODE"

            If optOrderBy(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & "ORDER BY SUPP_CUST_NAME, ITEM_DESC, TYPE"
            Else
                SqlStr = SqlStr & vbCrLf & "ORDER BY SUPP_CUST_NAME, ITEM_CODE, TYPE"
            End If
        Else
            If optSummLocWise.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " GROUP BY SUPP_CUST_NAME,BILL_TO_LOC_ID,TYPE,VENDOR_CODE"
            Else
                SqlStr = SqlStr & vbCrLf & " GROUP BY SUPP_CUST_NAME,TYPE,VENDOR_CODE"
            End If


            If optOrderBy(0).Checked = True Then
                SqlStr = SqlStr & vbCrLf & "ORDER BY SUPP_CUST_NAME"
            Else
                SqlStr = SqlStr & vbCrLf & "ORDER BY SUPP_CUST_NAME"
            End If
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************				
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Function

    Private Function InsertIntoTemp() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim SqlStr1 As String

        InsertIntoTemp = False

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_SALE_vs_PROD NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr1 = "INSERT INTO TEMP_SALE_vs_PROD ( " & vbCrLf _
            & " UserId, COMPANY_CODE, " & vbCrLf _
            & " ITEM_CODE, ITEM_DESC, ITEM_UOM, " & vbCrLf _
            & " TARIFF_CODE,TYPE, " & vbCrLf _
            & " APR_QTY, MAY_QTY, JUN_QTY, " & vbCrLf _
            & " JUL_QTY, AUG_QTY, SEP_QTY, " & vbCrLf _
            & " OCT_QTY, NOV_QTY, DEC_QTY, " & vbCrLf _
            & " JAN_QTY, FEB_QTY, MAR_QTY, TOTQTY,ITEM_RATE,SUPP_CUST_NAME,BILL_TO_LOC_ID,PART_NO,VENDOR_CODE) "


        If optSale.Checked = True Then
            SqlStr = SqlStr1 & vbCrLf & MakeSQL_S("S")
        Else
            SqlStr = SqlStr1 & vbCrLf & MakeSQL_S("D")
        End If

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()

        '''********************************				
        InsertIntoTemp = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
ErrPart:
        InsertIntoTemp = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Function
    Private Function MakeSQL_S(ByRef pType As String) As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String
        ''''SELECT CLAUSE...				


        Dim mAccountCodeStr As String = ""
        Dim mItemCodeStr As String = ""

        Dim pHeaderTable As String
        Dim pDetailTable As String

        Dim pQtyField As String
        Dim pAmountField As String
        Dim pVCField As String

        If pType = "S" Then
            pQtyField = "ID.ITEM_QTY"
            pAmountField = "ID.ITEM_AMT"
            pVCField = "IH.VENDOR_CODE"
        Else
            pQtyField = "ID.QTY"
            pAmountField = "ID.AMOUNT"
            pVCField = "''"
        End If


        If optWise(0).Checked = True Then
            MakeSQL_S = " SELECT " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf _
                & " 1 AS COMPANY_CODE, " & vbCrLf _
                & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.HSNCODE,'S', " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='APR' THEN " & pAmountField & " ELSE 0 END)) AS APR," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAY' THEN " & pAmountField & " ELSE 0 END)) AS MAY," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUN' THEN " & pAmountField & " ELSE 0 END)) AS JUN," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUL' THEN " & pAmountField & " ELSE 0 END)) AS JUL," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='AUG' THEN " & pAmountField & " ELSE 0 END)) AS AUG," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='SEP' THEN " & pAmountField & " ELSE 0 END)) AS SEP," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='OCT' THEN " & pAmountField & " ELSE 0 END)) AS OCT," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='NOV' THEN " & pAmountField & " ELSE 0 END)) AS NOV," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='DEC' THEN " & pAmountField & " ELSE 0 END)) AS DEC," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JAN' THEN " & pAmountField & " ELSE 0 END)) AS JAN," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='FEB' THEN " & pAmountField & " ELSE 0 END)) AS FEB," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAR' THEN " & pAmountField & " ELSE 0 END)) AS MAR," & vbCrLf _
                & " TO_CHAR(SUM(" & pAmountField & ")) AS ITEM_AMT, " & vbCrLf _
                & " 0 AS ITEM_RATE," & vbCrLf _
                & " CMST.SUPP_CUST_NAME,IH.BILL_TO_LOC_ID,INVMST.CUSTOMER_PART_NO "
        ElseIf optWise(1).Checked = True Then
            MakeSQL_S = " SELECT " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf _
                & " 1 AS COMPANY_CODE, " & vbCrLf _
                & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.HSNCODE,'S', " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='APR' THEN " & pQtyField & " ELSE 0 END)) AS APR," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAY' THEN " & pQtyField & " ELSE 0 END)) AS MAY," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUN' THEN " & pQtyField & " ELSE 0 END)) AS JUN," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUL' THEN " & pQtyField & " ELSE 0 END)) AS JUL," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='AUG' THEN " & pQtyField & " ELSE 0 END)) AS AUG," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='SEP' THEN " & pQtyField & " ELSE 0 END)) AS SEP," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='OCT' THEN " & pQtyField & " ELSE 0 END)) AS OCT," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='NOV' THEN " & pQtyField & " ELSE 0 END)) AS NOV," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='DEC' THEN " & pQtyField & " ELSE 0 END)) AS DEC," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JAN' THEN " & pQtyField & " ELSE 0 END)) AS JAN," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='FEB' THEN " & pQtyField & " ELSE 0 END)) AS FEB," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAR' THEN " & pQtyField & " ELSE 0 END)) AS MAR," & vbCrLf _
                & " TO_CHAR(SUM(" & pQtyField & ")) AS TOTQTY, " & vbCrLf _
                & " 0 AS ITEM_RATE," & vbCrLf _
                & " CMST.SUPP_CUST_NAME,IH.BILL_TO_LOC_ID,INVMST.CUSTOMER_PART_NO "
        Else
            MakeSQL_S = " SELECT " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf _
                & " 1 AS COMPANY_CODE, " & vbCrLf _
                & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.HSNCODE,'S', " & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='APR' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS APR," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAY' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS MAY," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUN' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS JUN," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JUL' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS JUL," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='AUG' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS AUG," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='SEP' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS SEP," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='OCT' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS OCT," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='NOV' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS NOV," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='DEC' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS DEC," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='JAN' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS JAN," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='FEB' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS FEB," & vbCrLf _
                & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.INVOICE_DATE,'MON')='MAR' THEN ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT ELSE 0 END)) AS MAR," & vbCrLf _
                & " TO_CHAR(SUM(ID.CGST_AMOUNT + ID.SGST_AMOUNT + ID.IGST_AMOUNT)) AS TOTGST, " & vbCrLf _
                & " 0 AS ITEM_RATE," & vbCrLf _
                & " CMST.SUPP_CUST_NAME,IH.BILL_TO_LOC_ID,INVMST.CUSTOMER_PART_NO "
        End If

        ''''FROM CLAUSE...		
        MakeSQL_S = MakeSQL_S & vbCrLf & " , " & pVCField & ""



        If pType = "S" Then
            pHeaderTable = "FIN_INVOICE_HDR"
            pDetailTable = "FIN_INVOICE_DET"
        Else
            pHeaderTable = "FIN_SUPP_SALE_HDR"
            pDetailTable = "FIN_SUPP_SALE_DET"
        End If

        MakeSQL_S = MakeSQL_S & vbCrLf _
            & " FROM " & pHeaderTable & " IH, " & pDetailTable & " ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST,FIN_INVTYPE_MST ITYPE "

        ''''WHERE CLAUSE...				
        MakeSQL_S = MakeSQL_S & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE = CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE = CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE = INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE = INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE = ITYPE.COMPANY_CODE" & vbCrLf _
            & " AND IH.TRNTYPE = ITYPE.CODE"

        If pType = "S" Then
            MakeSQL_S = MakeSQL_S & vbCrLf _
                & " AND IH.BILLNO NOT IN (" & vbCrLf _
                & " SELECT BILLNO FROM FIN_SUPP_SALE_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf _
                & " AND FYEAR=IH.FYEAR" & vbCrLf _
                & " AND SUPP_CUST_CODE=IH.SUPP_CUST_CODE" & vbCrLf _
                & " AND CANCELLED='N' AND ISFINALPOST='Y'" & vbCrLf _
                & " AND REASON = '6'" & vbCrLf _
                & " )"
        Else
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND REASON <> '6'"
        End If

        If Trim(txtTariffHeading.Text) <> "" Then
            MakeSQL_S = MakeSQL_S & vbCrLf & "AND ID.HSNCODE='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
        End If

        If pType = "S" Then
            MakeSQL_S = MakeSQL_S & vbCrLf & "AND IH.AGTD3='N'"
            MakeSQL_S = MakeSQL_S & vbCrLf & "AND IH.FOC='N'"
            MakeSQL_S = MakeSQL_S & vbCrLf & "AND IH.REJECTION='N'"
        End If

        MakeSQL_S = MakeSQL_S & vbCrLf & "AND IH.CANCELLED='N'"

        '    MakeSQL_S = MakeSQL_S & vbCrLf & "AND DECODE(CMST.WITHIN_COUNTRY,'Y',DECODE(AGTCT3,'Y',1,IH.TOTEDAMOUNT),1)>0 "				

        '    MakeSQL_S = MakeSQL_S & vbCrLf & "AND ITYPE.ISSALECOMP='Y'"				


        mShowAll = True
        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst				
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mShowAll = False
            End If
        Next

        If mShowAll = False Then
            If mTrnTypeStr <> "" Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
            End If
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
            MakeSQL_S = MakeSQL_S & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If cboInterUnit.SelectedIndex = 1 Then
            MakeSQL_S = MakeSQL_S & vbCrLf & "AND CMST.INTER_UNIT='N'"
        ElseIf cboInterUnit.SelectedIndex = 2 Then
            MakeSQL_S = MakeSQL_S & vbCrLf & "AND CMST.INTER_UNIT='Y'"
        End If

        If Trim(cboAccount.Text) <> "" Then
            MakeSQL_S = MakeSQL_S & vbCrLf _
                & "AND CMST.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(cboAccount.Text) & "'"
        End If

        If Trim(cboItem.Text) <> "" Then
            MakeSQL_S = MakeSQL_S & vbCrLf _
                & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(cboItem.Text) & "'"
        End If

        MakeSQL_S = MakeSQL_S & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        ''''GROUP BY CLAUSE...				
        MakeSQL_S = MakeSQL_S & vbCrLf _
            & " GROUP BY ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.ITEM_UOM ,INVMST.CUSTOMER_PART_NO, CMST.SUPP_CUST_NAME, BILL_TO_LOC_ID, ID.HSNCODE"


        If pType = "S" Then
            MakeSQL_S = MakeSQL_S & vbCrLf & " , " & pVCField & ""
        End If

        ''''ORDER BY CLAUSE...				

        If optOrderBy(0).Checked = True Then
            MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC"
        Else
            MakeSQL_S = MakeSQL_S & vbCrLf & "ORDER BY ID.ITEM_CODE"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mApr_S As Double
        Dim mMay_S As Double
        Dim mJun_S As Double
        Dim mJul_S As Double
        Dim mAug_S As Double
        Dim mSep_S As Double
        Dim mOct_S As Double
        Dim mNov_S As Double
        Dim mDec_S As Double
        Dim mJan_S As Double
        Dim mFeb_S As Double
        Dim mMar_S As Double
        Dim mTotal_S As Double

        Dim mApr_P As Double
        Dim mMay_P As Double
        Dim mJun_P As Double
        Dim mJul_P As Double
        Dim mAug_P As Double
        Dim mSep_P As Double
        Dim mOct_P As Double
        Dim mNov_P As Double
        Dim mDec_P As Double
        Dim mJan_P As Double
        Dim mFeb_P As Double
        Dim mMar_P As Double
        Dim mTotal_P As Double
        Dim mItemCode As String
        Dim mCustomerCode As String
        Dim mCurrentRate As Double
        Dim mPreviousRate As Double

        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim pTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean

        mShowAll = True
        mTrnTypeStr = ""
        For CntLst = 0 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst				
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mShowAll = False
            End If
        Next

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColCustomerName
                mCustomerCode = Trim(.Text)


                'If optDetail.Checked = True Then
                '    pTrnTypeStr = mTrnTypeStr
                '    mCurrentRate = GETRate(mItemCode, mCustomerCode, (txtDateTo.Text), "C", mShowAll, pTrnTypeStr)
                '    pTrnTypeStr = mTrnTypeStr
                '    mPreviousRate = GETRate(mItemCode, mCustomerCode, (txtDateFrom.Text), "P", mShowAll, pTrnTypeStr)
                'Else
                '    mCurrentRate = 0
                '    mPreviousRate = 0
                'End If

                '.Col = ColRate
                '.Text = VB6.Format(mCurrentRate, "0.00")

                '.Col = ColPrevRate
                '.Text = VB6.Format(mPreviousRate, "0.00")

                .Col = ColType

                .Col = ColApr
                mApr_S = mApr_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColMay
                mMay_S = mMay_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColJun
                mJun_S = mJun_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColJul
                mJul_S = mJul_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColAug
                mAug_S = mAug_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSep
                mSep_S = mSep_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColOct
                mOct_S = mOct_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColNov
                mNov_S = mNov_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColDec
                mDec_S = mDec_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColJan
                mJan_S = mJan_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColFeb
                mFeb_S = mFeb_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColMar
                mMar_S = mMar_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTotal
                mTotal_S = mTotal_S + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

            Next

            '''Sale Total				
            Call MainClass.AddBlankfpSprdRow(SprdMain, ColType)
            .Col = ColType
            .Row = .MaxRows
            .Text = "SALE TOTAL:"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80				
            .BlockMode = False

            .Row = .MaxRows

            .Col = ColApr
            .Text = VB6.Format(mApr_S, "0.00")

            .Col = ColMay
            .Text = VB6.Format(mMay_S, "0.00")

            .Col = ColJun
            .Text = VB6.Format(mJun_S, "0.00")

            .Col = ColJul
            .Text = VB6.Format(mJul_S, "0.00")

            .Col = ColAug
            .Text = VB6.Format(mAug_S, "0.00")

            .Col = ColSep
            .Text = VB6.Format(mSep_S, "0.00")

            .Col = ColOct
            .Text = VB6.Format(mOct_S, "0.00")

            .Col = ColNov
            .Text = VB6.Format(mNov_S, "0.00")

            .Col = ColDec
            .Text = VB6.Format(mDec_S, "0.00")

            .Col = ColJan
            .Text = VB6.Format(mJan_S, "0.00")

            .Col = ColFeb
            .Text = VB6.Format(mFeb_S, "0.00")

            .Col = ColMar
            .Text = VB6.Format(mMar_S, "0.00")

            .Col = ColTotal
            .Text = VB6.Format(mTotal_S, "0.00")


        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub
    Private Function GETRate(ByRef pItemCode As String, ByRef pCustomerName As String, ByRef pDate As String, ByRef pRateType As String, ByRef mShowAll As Boolean, ByRef mTrnTypeStr As String) As Object

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pCustomerCode As String



        pCustomerCode = ""

        If MainClass.ValidateWithMasterTable(pCustomerName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pCustomerCode = MasterNo
        End If

        If pRateType = "P" Then
            SqlStr = "SELECT MAX(ITEM_RATE) AS ITEM_RATE" & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR IN (" & RsCompany.Fields("FYEAR").Value & "," & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

            SqlStr = SqlStr & vbCrLf & "AND IH.AGTD3='N' AND IH.FOC='N' AND IH.REJECTION='N' AND IH.CANCELLED='N'" '' AND ITYPE.ISSALECOMP='Y'				

            If mShowAll = False Then
                If mTrnTypeStr <> "" Then
                    '                mTrnTypeStr = "(" & mTrnTypeStr & ")"				
                    SqlStr = SqlStr & vbCrLf & " AND IH.TRNTYPE IN " & "(" & mTrnTypeStr & ")"
                End If
            End If

            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE = (" & vbCrLf _
                & " SELECT MAX(INVOICE_DATE) " & vbCrLf _
                & " FROM FIN_INVOICE_HDR SH, FIN_INVOICE_DET SD " & vbCrLf _
                & " WHERE SH.MKEY=ID.MKEY " & vbCrLf _
                & " AND SH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SH.FYEAR IN (" & RsCompany.Fields("FYEAR").Value & "," & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf _
                & " AND SH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf _
                & " AND SD.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND SH.INVOICE_DATE <=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & "AND SH.AGTD3='N' AND SH.FOC='N' AND SH.REJECTION='N' AND SH.CANCELLED='N'" ' AND ITYPE.ISSALECOMP='Y'				

            If mShowAll = False Then
                If mTrnTypeStr <> "" Then
                    '                mTrnTypeStr = "(" & mTrnTypeStr & ")"				
                    SqlStr = SqlStr & vbCrLf & " AND SH.TRNTYPE IN " & "(" & mTrnTypeStr & ")"
                End If
            End If
            SqlStr = SqlStr & vbCrLf & ")"
        Else
            SqlStr = " SELECT (NVL(ITEM_PRICE,0)) AS ITEM_RATE " & vbCrLf _
                & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf _
                & " AND ID.ITEM_CODE='" & pItemCode & "' AND SO_APPROVED='Y'" & vbCrLf _
                & " AND IH.AMEND_WEF_FROM= ( " & vbCrLf & " SELECT MAX(SH.AMEND_WEF_FROM) " & vbCrLf _
                & " FROM DSP_SALEORDER_HDR SH, DSP_SALEORDER_DET SD" & vbCrLf _
                & " WHERE SH.MKEY=SD.MKEY" & vbCrLf _
                & " AND SH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pCustomerCode) & "'" & vbCrLf _
                & " AND SD.ITEM_CODE='" & pItemCode & "' AND SO_APPROVED='Y'" & vbCrLf _
                & " AND SH.AMEND_WEF_FROM<=TO_DATE('" & VB6.Format(pDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GETRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
        End If

        Exit Function
ErrPart:
        GETRate = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function


    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'SearchItem()
    End Sub


    'Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
    '    Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

    '    KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
    '    eventArgs.KeyChar = Chr(KeyAscii)
    '    If KeyAscii = 0 Then
    '        eventArgs.Handled = True
    '    End If
    'End Sub


    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs)
        'Dim KeyCode As Short = eventArgs.KeyCode
        'Dim Shift As Short = eventArgs.KeyData \ &H10000
        'If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub


    '    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs)
    '        Dim Cancel As Boolean = eventArgs.Cancel
    '        On Error GoTo ERR1
    '        Dim SqlStr As String

    '        lblItemCode.Text = ""
    '        If txtItemName.Text = "" Then GoTo EventExitSub

    '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

    '        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
    '            lblItemCode.Text = MasterNo
    '            txtItemName.Text = UCase(Trim(txtItemName.Text))
    '        Else
    '            lblItemCode.Text = ""
    '            MsgInformation("No Such Item in Item Master")
    '            Cancel = True
    '        End If
    '        GoTo EventExitSub
    'ERR1:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    'EventExitSub:
    '        eventArgs.Cancel = Cancel
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
        Dim SqlStr As String

        If Trim(txtTariffHeading.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtTariffHeading.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo,  , SqlStr) = False Then
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
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtTariffHeading.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC",  ,  , SqlStr) = True Then
            txtTariffHeading.Text = AcName
            '        txtTariff_Validate False				
            If txtTariffHeading.Enabled = True Then txtTariffHeading.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cboItem_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles cboItem.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboItem.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub
    Private Sub cboAccount_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles cboAccount.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboAccount.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub cboAccount_TextChanged(sender As Object, e As EventArgs) Handles cboAccount.TextChanged, cboItem.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboItem_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles cboItem.InitializeLayout, cboAccount.InitializeLayout
        e.Layout.Override.AllowRowFiltering = DefaultableBoolean.True
        e.Layout.Override.FilterUIType = FilterUIType.FilterRow
        e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.ExternalSortSingle
        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle
    End Sub
End Class
