Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmSalesReportTally
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    ''''Private PvtDBCn As ADODB.Connection				


    Dim mAccountCode As Integer

    Private Const ColInvNo As Short = 1
    Private Const ColInvDate As Short = 2
    Private Const ColCustomerCode As Short = 3
    Private Const ColCustomerName As Short = 4
    Private Const ColBillOfSupply As Short = 5
    Private Const ColGSTNo As Short = 6
    Private Const ColTax3 As Short = 7
    Private Const ColTax5 As Short = 8
    Private Const ColTax12 As Short = 9
    Private Const ColTax18 As Short = 10
    Private Const ColTax28 As Short = 11
    Private Const ColCompanyName As Short = 12

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

        MainClass.AssignCRptFormulas(Report1, "SHOWWISE=""" & IIf(optWise(0).Checked = True, "A", "Q") & """")


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

        mTitle = "Yearly Sales Reports" & IIf(optWise(0).Checked = True, " ( Value Wise ) ", " ( Qty Wise ) ")

        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text


        mSubTitle = mSubTitle & IIf(mSubTitle1 = "", "", " (" & mSubTitle1 & ")")


        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SalesReportTally.RPT"

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
    Private Sub frmSalesReportTally_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Yearly Sales Report"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmSalesReportTally_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
    Private Sub frmSalesReportTally_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmSalesReportTally_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
            lstInvoiceType.Items.Add("ALL")
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
                ElseIf pCompanyCode = RsCompany.Fields("COMPANY_CODE").Value Then
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
            .MaxCols = ColCompanyName
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColInvNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInvNo, 12)

            .Col = ColInvDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInvDate, 12)


            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerCode, 12)

            .Col = ColCustomerName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCustomerName, 25)

            .Col = ColBillOfSupply
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillOfSupply, 15)

            .Col = ColGSTNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColGSTNo, 15)


            For cntCol = ColTax3 To ColTax28
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

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)


        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = ""



        ''''SELECT CLAUSE...				


        Dim mAccountCodeStr As String = ""
        Dim mItemCodeStr As String = ""


        SqlStr = " SELECT " & vbCrLf _
                & " IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_STATE, CMST.GST_RGN_NO, " & vbCrLf

        ''CGST_PER, 

        SqlStr = SqlStr & vbCrLf _
                & " SUM(CASE WHEN ID.CGST_PER + ID.SGST_PER + ID.IGST_PER = 3 THEN ID.GSTABLE_AMT ELSE 0 END) AS TAX_3," & vbCrLf _
                & " SUM(CASE WHEN ID.CGST_PER + ID.SGST_PER + ID.IGST_PER = 5 THEN ID.GSTABLE_AMT ELSE 0 END) AS TAX_5," & vbCrLf _
                & " SUM(CASE WHEN ID.CGST_PER + ID.SGST_PER + ID.IGST_PER = 12 THEN ID.GSTABLE_AMT ELSE 0 END) AS TAX_12," & vbCrLf _
                & " SUM(CASE WHEN ID.CGST_PER + ID.SGST_PER + ID.IGST_PER = 18 THEN ID.GSTABLE_AMT ELSE 0 END) AS TAX_18," & vbCrLf _
                & " SUM(CASE WHEN ID.CGST_PER + ID.SGST_PER + ID.IGST_PER = 28 THEN ID.GSTABLE_AMT ELSE 0 END) AS TAX_28, CC.COMPANY_SHORTNAME"

        ''''FROM CLAUSE...				
        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_SUPP_CUST_BUSINESS_MST CMST,FIN_INVTYPE_MST ITYPE, GEN_COMPANY_MST CC "

        ''''WHERE CLAUSE...				
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY AND CC.COMPANY_CODE = IH.COMPANY_CODE" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE = CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE = CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID = CMST.LOCATION_ID" & vbCrLf _
            & " AND IH.COMPANY_CODE = ITYPE.COMPANY_CODE" & vbCrLf _
            & " AND IH.TRNTYPE = ITYPE.CODE"

        SqlStr = SqlStr & vbCrLf & "AND IH.AGTD3='N'"
        SqlStr = SqlStr & vbCrLf & "AND IH.FOC='N'"
        SqlStr = SqlStr & vbCrLf & "AND IH.REJECTION='N'"
        SqlStr = SqlStr & vbCrLf & "AND IH.CANCELLED='N'"


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
                SqlStr = SqlStr & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
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
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If cboInterUnit.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.INTER_UNIT='N'"
        ElseIf cboInterUnit.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.INTER_UNIT='Y'"
        End If

        If Trim(cboAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & "AND CMST.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(cboAccount.Text) & "'"
        End If


        SqlStr = SqlStr & vbCrLf _
            & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.BILLNO, IH.INVOICE_DATE, CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_STATE, CMST.GST_RGN_NO,CC.COMPANY_SHORTNAME " & vbCrLf

        ''''ORDER BY CLAUSE...				

        SqlStr = SqlStr & vbCrLf & "ORDER BY CC.COMPANY_SHORTNAME, IH.BILLNO"
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

    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mTax3 As Double
        Dim mTax5 As Double
        Dim mTax12 As Double
        Dim mTax18 As Double
        Dim mTax28 As Double


        Dim mCustomerCode As String

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

                .Col = ColCustomerName
                mCustomerCode = Trim(.Text)


                .Col = ColTax3
                mTax3 = mTax3 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTax5
                mTax5 = mTax5 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTax12
                mTax12 = mTax12 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTax18
                mTax18 = mTax18 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTax28
                mTax28 = mTax28 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))


            Next

            '''Sale Total				
            Call MainClass.AddBlankfpSprdRow(SprdMain, ColCustomerName)
            .Col = ColCustomerName
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

            .Col = ColTax3
            .Text = VB6.Format(mTax3, "0.00")

            .Col = ColTax5
            .Text = VB6.Format(mTax5, "0.00")

            .Col = ColTax12
            .Text = VB6.Format(mTax12, "0.00")

            .Col = ColTax18
            .Text = VB6.Format(mTax18, "0.00")

            .Col = ColTax28
            .Text = VB6.Format(mTax28, "0.00")


        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub

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

    Private Sub cboAccount_KeyPress(sender As Object, EventArgs As KeyPressEventArgs) Handles cboAccount.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboAccount.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub cboAccount_TextChanged(sender As Object, e As EventArgs) Handles cboAccount.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboAccount_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles cboAccount.InitializeLayout
        e.Layout.Override.AllowRowFiltering = DefaultableBoolean.True
        e.Layout.Override.FilterUIType = FilterUIType.FilterRow
        e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.ExternalSortSingle
        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle
    End Sub
End Class
