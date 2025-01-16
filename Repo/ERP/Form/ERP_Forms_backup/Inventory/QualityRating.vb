Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmQualityRating
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    ''Private PvtDBCn As ADODB.Connection

    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColQRating As Short = 3

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mClickProcess As Boolean
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


    Private Sub FillItemType()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = " SELECT GEN_DESC from INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' ORDER BY GEN_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstInvoiceType.SetItemChecked(CntLst, IIf(CntLst = 0, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0


        CboItemClass.Items.Clear()
        CboItemClass.Items.Add("ALL")
        CboItemClass.Items.Add("A Class")
        CboItemClass.Items.Add("B Class")
        CboItemClass.Items.Add("C Class")
        CboItemClass.Items.Add("DOL Class")
        CboItemClass.SelectedIndex = 0


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

        Exit Sub
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub CboItemClass_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboItemClass.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub CboItemClass_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboItemClass.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllSupp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSupp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtSupplier.Enabled = False
            cmdsearchSupp.Enabled = False
        Else
            TxtSupplier.Enabled = True
            cmdsearchSupp.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdGraph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGraph.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONQR(Crystal.DestinationConstants.crptToWindow, "Y")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONQR(Crystal.DestinationConstants.crptToWindow, "N")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONQR(Crystal.DestinationConstants.crptToPrinter, "N")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONQR(ByRef Mode As Crystal.DestinationConstants, ByRef GraphMode As String)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim mRPTName As String = ""

        Report1.Reset()

        mSubTitle = "For the Month : " & VB6.Format(lblNewDate.Text, "MMMM , YYYY")

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr

        SqlStr = FetchRecordForReport(SqlStr)

        If GraphMode = "N" Then
            mRPTName = "VendorQR_Summ.rpt"
        Else
            mRPTName = "VendorQR_Graph.rpt"
        End If


        mTitle = "Vendor Quality Rating "
        mTitle = mTitle & IIf(lblBookType.Text = "S", "", " - Item wise")


        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            GridName.Row = RowNum
            GridName.Col = ColQRating
            If Val(GridName.Text) <> 0 Then
                For FieldNum = prmStartGridCol To prmEndGridCol
                    GridName.Col = FieldNum
                    If FieldNum = prmStartGridCol Then
                        SetData = "FIELD" & FieldCnt
                        GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                    Else
                        SetData = SetData & ", " & "FIELD" & FieldCnt
                        GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                    End If
                    FieldCnt = FieldCnt + 1
                Next



                SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
                PubDBCn.Execute(SqlStr)
            End If
NextRec:
        Next
        PubDBCn.CommitTrans()
        FillPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyData = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mOrderBy As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        If lblBookType.Text = "S" Then
            mOrderBy = "Supplier Code & Name"
        Else
            mOrderBy = "Item Code & Name"
        End If
        MainClass.AssignCRptFormulas(Report1, "OrderBy=""" & mOrderBy & """")
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub

    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
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
    Private Sub frmQualityRating_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Me.Text = "Vendor Quality Rating - Summarised"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmQualityRating_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        cboType.Items.Clear()
        cboType.Items.Add("Both")
        cboType.Items.Add("Supplier")
        cboType.Items.Add("Customer")
        cboType.SelectedIndex = 0


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        cmdsearch.Enabled = False
        TxtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        FillItemType()

        Call PrintStatus(True)

        txtMonth.Enabled = False
        lblNewDate.Text = CStr(RunDate)
        txtMonth.Text = MonthName(Month(RunDate)) & ", " & Year(RunDate)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmQualityRating_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmQualityRating_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Close()
    End Sub





    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub



    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
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
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtSupplier.Text = AcName
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

        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemName.Text = UCase(Trim(txtItemName.Text))
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer = 0

        With SprdMain
            .MaxCols = ColQRating
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .set_ColWidth(ColCode, 15)
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 35)

            .Col = ColQRating
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColQRating, 9)

            If lblBookType.Text = "S" Then
                .Row = 0
                .Col = ColCode
                .Text = "Supplier Code"

                .Col = ColName
                .Text = "Supplier Name"
            Else
                .Row = 0
                .Col = ColCode
                .Text = "Item Code"

                .Col = ColName
                .Text = "Item Name"
            End If

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RSDR As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim mSuppCode As String
        Dim mSuppName As String = ""
        Dim mItemCode As String
        Dim mItemName As String
        Dim mPONo As Double

        Dim mPlanQty As Double
        Dim mDSNo As Double
        Dim mOverAllDRRating As Double
        Dim mOverAllQRRating As Double
        Dim mOverAllSRRating As Double
        Dim mOverAllRating As Double
        Dim mNextSuppCode As String


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSDR, ADODB.LockTypeEnum.adLockReadOnly)

        If RSDR.EOF = False Then
            CntRow = 1
            Do While Not RSDR.EOF
                SprdMain.Row = CntRow

                If lblBookType.Text = "S" Then
                    mSuppCode = IIf(IsDbNull(RSDR.Fields("SUPP_CUST_CODE").Value), "", RSDR.Fields("SUPP_CUST_CODE").Value)
                    mSuppName = IIf(IsDbNull(RSDR.Fields("SUPP_CUST_NAME").Value), "", RSDR.Fields("SUPP_CUST_NAME").Value)

                    If CalcQRRating(CntRow, mSuppCode, "", mOverAllQRRating) = False Then
                        RSDR.MoveNext()
                        GoTo NextRec
                    End If

                    SprdMain.Row = CntRow

                    SprdMain.Col = ColCode
                    mSuppCode = IIf(IsDbNull(RSDR.Fields("SUPP_CUST_CODE").Value), "", RSDR.Fields("SUPP_CUST_CODE").Value)
                    SprdMain.Text = mSuppCode

                    SprdMain.Col = ColName
                    mSuppName = IIf(IsDbNull(RSDR.Fields("SUPP_CUST_NAME").Value), "", RSDR.Fields("SUPP_CUST_NAME").Value)
                    SprdMain.Text = mSuppName
                Else
                    mItemCode = IIf(IsDbNull(RSDR.Fields("ITEM_CODE").Value), "", RSDR.Fields("ITEM_CODE").Value)
                    mItemName = IIf(IsDbNull(RSDR.Fields("ITEM_SHORT_DESC").Value), "", RSDR.Fields("ITEM_SHORT_DESC").Value)

                    If CalcQRRating(CntRow, "", mItemCode, mOverAllQRRating) = False Then
                        RSDR.MoveNext()
                        GoTo NextRec
                    End If

                    SprdMain.Row = CntRow

                    SprdMain.Col = ColCode
                    mItemCode = IIf(IsDbNull(RSDR.Fields("ITEM_CODE").Value), "", RSDR.Fields("ITEM_CODE").Value)
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColName
                    mItemName = IIf(IsDbNull(RSDR.Fields("ITEM_SHORT_DESC").Value), "", RSDR.Fields("ITEM_SHORT_DESC").Value)
                    SprdMain.Text = mItemName

                End If
                SprdMain.Col = ColQRating
                SprdMain.Text = CStr(mOverAllQRRating)

                RSDR.MoveNext()
                If RSDR.EOF = False Then
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    '                mNextSuppCode = IIf(IsNull(RSDR!SUPP_CUST_CODE), "", RSDR!SUPP_CUST_CODE)
                End If
                CntRow = CntRow + 1
NextRec:
            Loop
        End If
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        'Resume
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mSupplier As String
        Dim mItemCode As String
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mItemTypeCode As String = ""

        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String

        mStartDate = " 01/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mEndDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        '    If MainClass.ValidateWithMasterTable(cboItemType.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND GEN_TYPE='C'") = True Then
        '        mItemTypeCode = MasterNo
        '    Else
        '        mItemTypeCode = "-1"
        '    End If

        mTrnTypeStr = ""

        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mItemTypeCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", "'" & mItemTypeCode & "'", mTrnTypeStr & "," & "'" & mItemTypeCode & "'")
            End If
        Next

        ''PUR_DAILY_SCHLD_DET ID,

        If lblBookType.Text = "S" Then
            MakeSQL = " SELECT DISTINCT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME "
        Else
            MakeSQL = " SELECT DISTINCT ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC"
        End If

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_SUPP_CUST_HDR IH, " & vbCrLf & " FIN_SUPP_CUST_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=ID.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If cboType.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND CMST.SUPP_CUST_TYPE= '" & UCase(VB.Left(cboType.Text, 1)) & "'"
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        '    If chkAll.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '            mItemCode = MasterNo
        '            MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
        '        End If
        '    End If
        '
        '    MakeSQL = MakeSQL & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mItemTypeCode) & "'"

        If mTrnTypeStr <> "" Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.CATEGORY_CODE IN " & mTrnTypeStr & ""
        End If

        If CboItemClass.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.ITEM_CLASS = '" & VB.Left(CboItemClass.Text, 1) & "'"
        End If

        ''ORDER CLAUSE...

        If lblBookType.Text = "S" Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME"
        Else
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(lblNewDate) = False Then Exit Function
        If FYChk(CStr(CDate(lblNewDate.Text))) = False Then Exit Function
        '    If MainClass.ChkIsdateF(lblNewDate) = False Then Exit Function
        If FYChk(CStr(CDate(lblNewDate.Text))) = False Then Exit Function
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtSupplier.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                TxtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Supplier Name")
                TxtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtSupplier.Text)
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

    'Private Sub UpDMonth_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.DownClick
    '    SetNewDate(-1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    '    Call PrintStatus(False)
    'End Sub
    Sub SetNewDate(ByRef prmSpinDirection As Short)
        lblNewDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, prmSpinDirection, CDate(lblNewDate.Text)))
    End Sub
    'Private Sub UpDMonth_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.UpClick
    '    SetNewDate(1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    '    Call PrintStatus(False)
    'End Sub







    Private Function CalcQRRating(ByRef mRow As Integer, ByRef pSuppCustCode As String, ByRef xItemCode As String, ByRef pOverAllQRRating As Double) As Boolean

        On Error GoTo ErrPart
        Dim RsSRNTemp As ADODB.Recordset = Nothing
        Dim RsTempMain As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pItemCode As String
        Dim SqlStr As String = ""
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mCountItem As Integer
        Dim a As Double
        Dim B As Double
        Dim c As Double
        Dim D As Double
        Dim e As Double
        Dim N As Double


        Dim F As Double
        Dim G As Double

        Dim mTotalQRRating As Double
        Dim mQRRating As Double
        Dim pPONO As Double
        Dim mDivisionCode As Double
        Dim mLineRej As Double
        Dim xSuppCustCode As String
        Dim xCheckSuppCustCode As String = ""

        CalcQRRating = False
        mStartDate = " 01/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mEndDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked And TxtSupplier.Text <> "" Then
            If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xCheckSuppCustCode = MasterNo
            End If
        End If

        mCountItem = 0

        pOverAllQRRating = 0


        '

        SqlStr = " SELECT DISTINCT AUTO_KEY_PO, ID.ITEM_CODE, IH.SUPP_CUST_CODE " & vbCrLf & " FROM " & vbCrLf & " PUR_DELV_SCHLD_HDR IH, PUR_DELV_SCHLD_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV "

        If pSuppCustCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCustCode & "'"
        End If

        If xCheckSuppCustCode <> "" Then
            SqlStr = SqlStr & " AND SUPP_CUST_CODE='" & xCheckSuppCustCode & "'"
        End If

        If xItemCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & xItemCode & "'"
        End If


        '    If cboDivision.ListIndex > 0 Then
        '        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mDivisionCode = Trim(MasterNo)
        '        End If
        '        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(mDivisionCode) & ""
        '    End If

        SqlStr = SqlStr & vbCrLf & " AND ID.TOTAL_QTY>0 AND IH.SCHLD_DATE BETWEEN TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTempMain.EOF = False Then
            Do While Not RsTempMain.EOF
                pItemCode = Trim(IIf(IsDbNull(RsTempMain.Fields("ITEM_CODE").Value), "-1", RsTempMain.Fields("ITEM_CODE").Value))
                pPONO = IIf(IsDbNull(RsTempMain.Fields("AUTO_KEY_PO").Value), "-1", RsTempMain.Fields("AUTO_KEY_PO").Value)
                xSuppCustCode = IIf(IsDbNull(RsTempMain.Fields("SUPP_CUST_CODE").Value), "", RsTempMain.Fields("SUPP_CUST_CODE").Value)
                mLineRej = 0



                SqlStr = " SELECT SUM(RTN_QTY) RTN_QTY " & vbCrLf & " FROM INV_SRN_HDR IH, INV_SRN_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN" & vbCrLf & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSuppCustCode) & "'" & vbCrLf & " AND FROM_STOCK_TYPE='ST' AND TO_STOCK_TYPE='RJ' AND IH.STATUS='Y'" & vbCrLf & " AND IH.SRN_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.SRN_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                If pItemCode <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
                End If

                If cboDivision.SelectedIndex > 0 Then
                    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionCode = CDbl(Trim(MasterNo))
                    End If
                    SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
                End If

                SqlStr = SqlStr & vbCrLf & " HAVING SUM(RTN_QTY)>0"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSRNTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsSRNTemp.EOF = False Then
                    mLineRej = IIf(IsDBNull(RsSRNTemp.Fields("RTN_QTY").Value), 0, RsSRNTemp.Fields("RTN_QTY").Value)
                End If

                SqlStr = " SELECT SUM(LOT_ACCEPT) LOT_ACCEPT, " & vbCrLf & " SUM(LOT_ACCEPT_DEV) LOT_ACCEPT_DEV, SUM(LOT_ACC_SEG) LOT_ACC_SEG, " & vbCrLf & " SUM(LOT_ACC_RWK) LOT_ACC_RWK, SUM(REJECTED_QTY) REJECTED_QTY, " & vbCrLf & " SUM(RECEIVED_QTY) RECEIVED_QTY " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND ID.REF_AUTO_KEY_NO=" & pPONO & "" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                '            If xItemCode <> "" Then
                '                SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'"
                '            End If
                '
                If cboDivision.SelectedIndex > 0 Then
                    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDivisionCode = CDbl(Trim(MasterNo))
                    End If
                    SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    a = IIf(IsDbNull(RsTemp.Fields("LOT_ACCEPT").Value), 0, RsTemp.Fields("LOT_ACCEPT").Value)
                    B = IIf(IsDbNull(RsTemp.Fields("LOT_ACCEPT_DEV").Value), 0, RsTemp.Fields("LOT_ACCEPT_DEV").Value)
                    c = IIf(IsDbNull(RsTemp.Fields("LOT_ACC_SEG").Value), 0, RsTemp.Fields("LOT_ACC_SEG").Value)
                    D = IIf(IsDbNull(RsTemp.Fields("LOT_ACC_RWK").Value), 0, RsTemp.Fields("LOT_ACC_RWK").Value)
                    e = IIf(IsDbNull(RsTemp.Fields("REJECTED_QTY").Value), 0, RsTemp.Fields("REJECTED_QTY").Value)
                    N = IIf(IsDbNull(RsTemp.Fields("RECEIVED_QTY").Value), 0, RsTemp.Fields("RECEIVED_QTY").Value)
                    a = a - B - c - D - mLineRej

                    F = GetCustomerLineStop(pSuppCustCode, pItemCode, mStartDate, mEndDate)
                    G = GetSaleReturn(pSuppCustCode, pItemCode, mStartDate, mEndDate)
                End If


                If N = 0 Then
                    pOverAllQRRating = 0
                Else
                    mQRRating = (((a * 1) + (B * 0.75) + (c * 0.5) + (D * (-0.5)) + (e * 0)) / N) - (F + G)
                    mTotalQRRating = mTotalQRRating + mQRRating
                    mCountItem = mCountItem + 1
                End If
                RsTempMain.MoveNext()
            Loop
            CalcQRRating = True
        End If

        If mCountItem = 0 Then
            pOverAllQRRating = 0
        Else
            pOverAllQRRating = (mTotalQRRating * 100 / mCountItem)
        End If

        pOverAllQRRating = System.Math.Round(pOverAllQRRating, 0)
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetCustomerLineStop(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mStartDate As String, ByRef mEndDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTimeLoss As Double

        Dim mDivisionCode As Double


        GetCustomerLineStop = 0

        SqlStr = "SELECT SUM(TIME_LOSS_MIN) AS TIME_LOSS_MIN" & vbCrLf & " FROM INV_CUSTOMER_LINE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_DATE >= TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE <= TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If pSupplierCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'"
        End If

        If pItemCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        End If


        '    If cboDivision.ListIndex > 0 Then
        '        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mDivisionCode = Trim(MasterNo)
        '        End If
        '        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(mDivisionCode) & ""
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mTimeLoss = IIf(IsDbNull(RsTemp.Fields("TIME_LOSS_MIN").Value), 0, RsTemp.Fields("TIME_LOSS_MIN").Value)
        End If

        If mTimeLoss > 30 Then
            GetCustomerLineStop = 0.05
        ElseIf mTimeLoss > 0 Then
            GetCustomerLineStop = 0.01
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetSaleReturn(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mStartDate As String, ByRef mEndDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSaleReturn As Double

        Dim mDivisionCode As Double


        GetSaleReturn = 0
        mSaleReturn = 0

        SqlStr = "SELECT COUNT(DISTINCT IH.AUTO_KEY_REF) AS AUTO_KEY_REF" & vbCrLf & " FROM PRD_SALERETURN_HDR IH, PRD_SALERETURN_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MRR_DATE >= TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.MRR_DATE <= TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If pSupplierCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.BOP_SUPP_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'"
        End If

        If pItemCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.BOP_ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        End If

        '    If cboDivision.ListIndex > 0 Then
        '        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mDivisionCode = Trim(MasterNo)
        '        End If
        '        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(mDivisionCode) & ""
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mSaleReturn = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_REF").Value), 0, RsTemp.Fields("AUTO_KEY_REF").Value)
        End If

        If mSaleReturn > 2 Then
            GetSaleReturn = 0.05
        ElseIf mSaleReturn > 0 Then
            GetSaleReturn = 0.01
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
