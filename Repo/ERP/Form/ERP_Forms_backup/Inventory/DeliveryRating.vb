Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDeliveryRating
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    ''Private PvtDBCn As ADODB.Connection

    Private Const ColSupplierCode As Short = 1
    Private Const colSupplier As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColPlanQty1 As Short = 5
    Private Const ColActualQty1 As Short = 6
    Private Const ColQtyRating1 As Short = 7
    Private Const ColLineRating1 As Short = 8
    Private Const ColPlanQty2 As Short = 9
    Private Const ColActualQty2 As Short = 10
    Private Const ColQtyRating2 As Short = 11
    Private Const ColLineRating2 As Short = 12
    Private Const ColPlanQty3 As Short = 13
    Private Const ColActualQty3 As Short = 14
    Private Const ColQtyRating3 As Short = 15
    Private Const ColLineRating3 As Short = 16
    Private Const ColPlanQty4 As Short = 17
    Private Const ColActualQty4 As Short = 18
    Private Const ColQtyRating4 As Short = 19
    Private Const ColLineRating4 As Short = 20
    Private Const ColPlanQty As Short = 21
    Private Const ColActualQty As Short = 22
    Private Const ColQtyRating As Short = 23
    Private Const ColLineRating As Short = 24
    Private Const ColOverAllDRRating As Short = 25
    Private Const ColQRating As Short = 26
    Private Const ColPDIR As Short = 27
    Private Const ColQF As Short = 28
    Private Const ColReworkBy As Short = 29
    Private Const ColRepeated As Short = 30
    Private Const ColResones As Short = 31
    Private Const ColSRating As Short = 32
    Private Const ColOverAllRating As Short = 33

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim pmyMenu As String

    Dim mClickProcess As Boolean
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
        Me.Dispose()
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
        Dim mRPTName As String = ""

        Report1.Reset()

        mSubTitle = "For the Month : " & VB6.Format(lblNewDate.Text, "MMMM , YYYY")

        If FillPrintDummyData(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols) = False Then GoTo ReportErr


        If lblBookType.Text = "DR" Then
            mRPTName = "VendorDR.rpt"
            mTitle = "Vendor Delivery Rating "
        ElseIf lblBookType.Text = "QR" Then
            mRPTName = "VendorQR.rpt"
            mTitle = "Vendor Quality Rating "
        ElseIf lblBookType.Text = "SR" Then
            mRPTName = "VendorSR.rpt"
            mTitle = "Vendor Service Rating "
        Else

            frmPrintRating.ShowDialog()

            If G_PrintLedg = False Then
                Exit Sub
            End If

            SqlStr = FetchRecordForReport(SqlStr)

            mTitle = "Vendor Rating "
            mSubTitle = ""
            If frmPrintRating.OptSelected(0).Checked = True Then
                mRPTName = "VendorRating.rpt"
            Else
                If frmPrintRating.OptSelected(1).Checked = True Then
                    mRPTName = "VendorRatingTabular.rpt"
                ElseIf frmPrintRating.OptSelected(2).Checked = True Then
                    mRPTName = "VendorRatingTabular1.rpt"
                ElseIf frmPrintRating.OptSelected(3).Checked = True Then
                    mRPTName = "VendorRatingTabular2.rpt"
                ElseIf frmPrintRating.OptSelected(4).Checked = True Then
                    mRPTName = "VendorRatingTabular3.rpt"
                ElseIf frmPrintRating.OptSelected(5).Checked = True Then
                    mRPTName = "VendorRatingTabular4.rpt"
                End If
                mSubTitle = "For The Month : " & VB6.Format(lblNewDate.Text, "MMMM-YYYY")
            End If
        End If

        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        frmPrintRating.Close()
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
            '        GridName.Col = ColActualQty
            '        If Val(GridName.Text) <> 0 Then
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " " & GetData & ") "
            PubDBCn.Execute(SqlStr)
            '        End If
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

        mSqlStr = "SELECT * " & " FROM TEMP_PRINTDUMMYDATA PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " "


        If frmPrintRating.OptSelected(2).Checked = True Then
            mSqlStr = mSqlStr & "ORDER BY FIELD1,FIELD3"
        Else
            mSqlStr = mSqlStr & "ORDER BY SUBROW"
        End If

        '    If frmPrintRating.OptSelected(1).Value = True Then
        '        mSqlStr = mSqlStr & ", FIELD2"
        '    ElseIf frmPrintRating.OptSelected(2).Value = True Then
        '        mSqlStr = mSqlStr & ", FIELD33"
        '    ElseIf frmPrintRating.OptSelected(3).Value = True Then
        '        mSqlStr = mSqlStr & ", FIELD25"
        '    ElseIf frmPrintRating.OptSelected(4).Value = True Then
        '        mSqlStr = mSqlStr & ", FIELD2"
        '    ElseIf frmPrintRating.OptSelected(5).Value = True Then
        '        mSqlStr = mSqlStr & ", FIELD2"
        '    End If

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mMonth As String

        mMonth = VB6.Format(lblNewDate.Text, "MMMM-YYYY")
        Report1.SQLQuery = mSqlStr
        If frmPrintRating.OptSelected(0).Checked = True Then
            SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Else
            SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, pmyMenu)
        End If
        MainClass.AssignCRptFormulas(Report1, "ForMonth=""" & mMonth & """")
        '    Report1.GroupSortFields(2) = "-" & "FIELD3"
        Report1.WindowShowGroupTree = False
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
    Private Sub frmDeliveryRating_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        If lblBookType.Text = "DR" Then
            Me.Text = "Vendor Delivery Rating "
        ElseIf lblBookType.Text = "QR" Then
            Me.Text = "Vendor Quality Rating "
        ElseIf lblBookType.Text = "SR" Then
            Me.Text = "Vendor ServiceRespones Rating "
        Else
            Me.Text = "Vendor Over-All Rating "
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmDeliveryRating_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        cboType.Items.Clear()
        cboType.Items.Add("Both")
        cboType.Items.Add("Supplier")
        cboType.Items.Add("Customer")
        cboType.SelectedIndex = 0

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        cmdsearch.Enabled = False
        TxtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        FillItemType()
        Call PrintStatus(True)

        txtMonth.Enabled = True
        lblNewDate.Text = CStr(RunDate)
        txtMonth.Text = MonthName(Month(RunDate)) & ", " & Year(RunDate)
        FormatSprdMain(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
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
    Private Sub frmDeliveryRating_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmDeliveryRating_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
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

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColOverAllRating
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColSupplierCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .set_ColWidth(ColSupplierCode, 15)
            .ColHidden = True

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, 20)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .set_ColWidth(ColItemCode, 8)
            If lblBookType.Text = "SR" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 25)
            If lblBookType.Text = "SR" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            For cntCol = ColPlanQty1 To ColOverAllDRRating
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
                If lblBookType.Text = "DR" Or lblBookType.Text = "OR" Then
                    .ColHidden = False
                Else
                    .ColHidden = True
                End If
            Next

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
            If lblBookType.Text = "QR" Or lblBookType.Text = "OR" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            For cntCol = ColPDIR To ColSRating
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
                If lblBookType.Text = "SR" Or lblBookType.Text = "OR" Then
                    .ColHidden = False
                Else
                    .ColHidden = True
                End If
            Next

            .Col = ColOverAllRating
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColOverAllRating, 9)
            If lblBookType.Text = "OR" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' OperationModeSingle
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
        Dim mItemCode As String = ""
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

                SprdMain.Col = ColSupplierCode
                mSuppCode = IIf(IsDbNull(RSDR.Fields("SUPP_CUST_CODE").Value), "", RSDR.Fields("SUPP_CUST_CODE").Value)
                SprdMain.Text = mSuppCode

                SprdMain.Col = colSupplier
                mSuppName = IIf(IsDbNull(RSDR.Fields("SUPP_CUST_NAME").Value), "", RSDR.Fields("SUPP_CUST_NAME").Value)
                SprdMain.Text = mSuppName

                If lblBookType.Text <> "SR" Then
                    mDSNo = IIf(IsDbNull(RSDR.Fields("AUTO_KEY_DELV").Value), "", RSDR.Fields("AUTO_KEY_DELV").Value)
                    mPONo = IIf(IsDbNull(RSDR.Fields("AUTO_KEY_PO").Value), "", RSDR.Fields("AUTO_KEY_PO").Value)

                    SprdMain.Col = ColItemCode
                    mItemCode = Trim(IIf(IsDbNull(RSDR.Fields("ITEM_CODE").Value), "", RSDR.Fields("ITEM_CODE").Value))
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColItemDesc
                    mItemName = IIf(IsDbNull(RSDR.Fields("ITEM_SHORT_DESC").Value), "", RSDR.Fields("ITEM_SHORT_DESC").Value)
                    SprdMain.Text = mItemName

                    SprdMain.Col = ColPlanQty
                    mPlanQty = IIf(IsDbNull(RSDR.Fields("TOTAL_QTY").Value), "", RSDR.Fields("TOTAL_QTY").Value)
                    SprdMain.Text = CStr(mPlanQty)
                End If

                If lblBookType.Text = "DR" Or lblBookType.Text = "OR" Then
                    Call CalcDRRating(CntRow, mDSNo, mItemCode, mOverAllDRRating, mSuppCode)
                End If

                If lblBookType.Text = "QR" Or lblBookType.Text = "OR" Then
                    Call CalcQRRating(CntRow, mPONo, mItemCode, mOverAllQRRating, mSuppCode)
                End If

                If lblBookType.Text = "SR" Or lblBookType.Text = "OR" Then
                    '                If mNextSuppCode <> mSuppCode Then
                    Call CalcSRRating(CntRow, mSuppCode, mItemCode, mOverAllSRRating)
                    '                End If
                End If

                SprdMain.Row = CntRow
                SprdMain.Col = ColOverAllRating
                If lblBookType.Text = "DR" Then
                    mOverAllRating = mOverAllDRRating
                ElseIf lblBookType.Text = "QR" Then
                    mOverAllRating = mOverAllQRRating
                ElseIf lblBookType.Text = "SR" Then
                    mOverAllRating = mOverAllSRRating
                Else
                    If mOverAllDRRating = 0 And mOverAllQRRating = 0 Then mOverAllSRRating = 0
                    mOverAllRating = (0.3 * mOverAllDRRating) + (0.5 * mOverAllQRRating) + (0.2 * mOverAllSRRating)
                End If
                mOverAllRating = System.Math.Round(mOverAllRating, 0)
                SprdMain.Text = CStr(mOverAllRating)

                RSDR.MoveNext()
                If RSDR.EOF = False Then
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    mNextSuppCode = IIf(IsDbNull(RSDR.Fields("SUPP_CUST_CODE").Value), "", RSDR.Fields("SUPP_CUST_CODE").Value)
                End If
                CntRow = CntRow + 1
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

        Dim mDivisionCode As Double

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

        If lblBookType.Text = "SR" Then
            MakeSQL = " SELECT DISTINCT IH.AUTO_KEY_PO, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME "
        Else
            MakeSQL = " SELECT IH.AUTO_KEY_PO, " & vbCrLf & " IH.SUPP_CUST_CODE, ID.*,INVMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"
        End If

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DELV_SCHLD_DET ID, PUR_PURCHASE_HDR PH," & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=PH.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_PO=PH.AUTO_KEY_PO" & vbCrLf & " AND IH.PO_AMEND_NO=PH.AMEND_NO" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ID.TOTAL_QTY<>0"

        If cboType.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND CMST.SUPP_CUST_TYPE= '" & UCase(VB.Left(cboType.Text, 1)) & "'"
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        '    MakeSQL = MakeSQL & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mItemTypeCode) & "'"


        If mTrnTypeStr <> "" Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.CATEGORY_CODE IN " & mTrnTypeStr & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            MakeSQL = MakeSQL & vbCrLf & " AND PH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.SCHLD_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.SCHLD_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        ''ORDER CLAUSE...
        If lblBookType.Text = "SR" Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME"
        Else
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,INVMST.ITEM_SHORT_DESC"
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





    Private Sub CalcDRRating(ByRef mRow As Integer, ByRef pDSNo As Double, ByRef pItemCode As String, ByRef pOverAllDRRating As Double, ByRef mSuppCode As String)

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""


        Dim mPlanQty As Double
        Dim mActualQty As Double
        Dim mSerialDay As Integer

        Dim mWeekEndPlanQty1 As Double
        Dim mWeekEndActualQty1 As Double
        Dim mDlvPer1 As Double
        Dim mQtyPer1 As Double
        Dim mLinearityPoint1 As Double
        Dim mLinearityPer1 As Double
        Dim mPlanDays1 As Double

        Dim mWeekEndPlanQty2 As Double
        Dim mWeekEndActualQty2 As Double
        Dim mDlvPer2 As Double
        Dim mQtyPer2 As Double
        Dim mLinearityPoint2 As Double
        Dim mLinearityPer2 As Double
        Dim mPlanDays2 As Double

        Dim mWeekEndPlanQty3 As Double
        Dim mWeekEndActualQty3 As Double
        Dim mDlvPer3 As Double
        Dim mQtyPer3 As Double
        Dim mLinearityPoint3 As Double
        Dim mLinearityPer3 As Double
        Dim mPlanDays3 As Double

        Dim mWeekEndPlanQty4 As Double
        Dim mWeekEndActualQty4 As Double
        Dim mDlvPer4 As Double
        Dim mQtyPer4 As Double
        Dim mLinearityPoint4 As Double
        Dim mLinearityPer4 As Double
        Dim mPlanDays4 As Double

        Dim mWeekEndPlanQty As Double
        Dim mWeekEndActualQty As Double
        Dim mDlvPer As Double
        Dim mQtyPer As Double
        Dim mLinearityPoint As Double
        Dim mLinearityPer As Double
        Dim mPlanDays As Double

        Dim mLinerCnt1 As Double
        Dim mLinerCnt2 As Double
        Dim mLinerCnt3 As Double
        Dim mLinerCnt4 As Double
        Dim mPremiumFreight As Double
        Dim mStartDate As String
        Dim mEndDate As String

        mWeekEndPlanQty1 = 0
        mWeekEndActualQty1 = 0
        mDlvPer1 = 0
        mQtyPer1 = 0
        mLinearityPoint1 = 0
        mLinearityPer1 = 0
        mPlanDays1 = 0

        mWeekEndPlanQty2 = 0
        mWeekEndActualQty2 = 0
        mDlvPer2 = 0
        mQtyPer2 = 0
        mLinearityPoint2 = 0
        mLinearityPer2 = 0
        mPlanDays2 = 0


        mWeekEndPlanQty3 = 0
        mWeekEndActualQty3 = 0
        mDlvPer3 = 0
        mQtyPer3 = 0
        mLinearityPoint3 = 0
        mLinearityPer3 = 0
        mPlanDays3 = 0


        mWeekEndPlanQty4 = 0
        mWeekEndActualQty4 = 0
        mDlvPer4 = 0
        mQtyPer4 = 0
        mLinearityPoint4 = 0
        mLinearityPer4 = 0
        mPlanDays4 = 0

        mWeekEndPlanQty = 0
        mWeekEndActualQty = 0
        mDlvPer = 0
        mQtyPer = 0
        mLinearityPoint = 0
        mLinearityPer = 0
        mPlanDays = 0



        mStartDate = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mEndDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        mPremiumFreight = GetPremiumFreight(mSuppCode, pItemCode, mStartDate, mEndDate)

        SqlStr = " SELECT * " & vbCrLf & " FROM PUR_DAILY_SCHLD_DET DS " & vbCrLf & " WHERE DS.AUTO_KEY_DELV=" & pDSNo & "" & vbCrLf & " AND DS.ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND TO_CHAR(DS.SCHLD_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"


        '    If mWeek = 1 Then
        '        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(DS.SERIAL_DATE,'DD')>='14'" & vbCrLf _
        ''                & " AND TO_CHAR(DS.SERIAL_DATE,'DD')<='22' "
        '
        '    ElseIf mWeek = 2 Then
        '
        '    ElseIf mWeek = 3 Then
        '
        '    ElseIf mWeek = 4 Then
        '
        '    End If

        SqlStr = SqlStr & vbCrLf & "ORDER BY SERIAL_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)



        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mSerialDay = CInt(VB6.Format(IIf(IsDbNull(RsTemp.Fields("SERIAL_DATE").Value), "", RsTemp.Fields("SERIAL_DATE").Value), "DD"))
                mPlanQty = IIf(IsDbNull(RsTemp.Fields("PLANNED_QTY").Value), 0, RsTemp.Fields("PLANNED_QTY").Value)
                '            mActualQty = IIf(IsNull(RsTemp!ACTUAL_QTY), 0, RsTemp!ACTUAL_QTY)
                mActualQty = GetActualQty(pDSNo, IIf(IsDbNull(RsTemp.Fields("SERIAL_DATE").Value), "", RsTemp.Fields("SERIAL_DATE").Value), pItemCode)

                If mSerialDay <= 7 Then
                    mWeekEndPlanQty1 = mWeekEndPlanQty1 + mPlanQty
                    mWeekEndActualQty1 = mWeekEndActualQty1 + mActualQty

                    If mPlanQty = 0 Then
                        mDlvPer1 = 0
                    Else
                        mLinerCnt1 = 1
                        mPlanDays1 = mPlanDays1 + 1
                        mDlvPer1 = mActualQty / mPlanQty * 100
                    End If

                    If mDlvPer1 >= 90 And mDlvPer1 <= 110 Then
                        mLinearityPoint1 = mLinearityPoint1 + 1
                    End If

                    '                If mActualQty > 0 And mPlanQty = 0 Then
                    '                    mLinearityPoint1 = mLinearityPoint1 - 1
                    '                End If

                ElseIf mSerialDay > 7 And mSerialDay <= 14 Then
                    mWeekEndPlanQty2 = mWeekEndPlanQty2 + mPlanQty
                    mWeekEndActualQty2 = mWeekEndActualQty2 + mActualQty

                    If mPlanQty = 0 Then
                        mDlvPer2 = 0
                    Else
                        mLinerCnt2 = 1
                        mPlanDays2 = mPlanDays2 + 1
                        mDlvPer2 = mActualQty / mPlanQty * 100
                    End If

                    If mDlvPer2 >= 90 And mDlvPer2 <= 110 Then
                        mLinearityPoint2 = mLinearityPoint2 + 1
                    End If

                    '                If mActualQty > 0 And mPlanQty = 0 Then
                    '                    mLinearityPoint2 = mLinearityPoint2 - 1
                    '                End If

                ElseIf mSerialDay > 14 And mSerialDay <= 22 Then
                    mWeekEndPlanQty3 = mWeekEndPlanQty3 + mPlanQty
                    mWeekEndActualQty3 = mWeekEndActualQty3 + mActualQty

                    If mPlanQty = 0 Then
                        mDlvPer3 = 0
                    Else
                        mLinerCnt3 = 1
                        mPlanDays3 = mPlanDays3 + 1
                        mDlvPer3 = mActualQty / mPlanQty * 100
                    End If

                    If mDlvPer3 >= 90 And mDlvPer3 <= 110 Then
                        mLinearityPoint3 = mLinearityPoint3 + 1
                    End If

                    '                If mActualQty > 0 And mPlanQty = 0 Then
                    '                    mLinearityPoint3 = mLinearityPoint3 - 1
                    '                End If

                ElseIf mSerialDay > 22 Then
                    mWeekEndPlanQty4 = mWeekEndPlanQty4 + mPlanQty
                    mWeekEndActualQty4 = mWeekEndActualQty4 + mActualQty

                    If mPlanQty = 0 Then
                        mDlvPer4 = 0
                    Else
                        mLinerCnt4 = 1
                        mPlanDays4 = mPlanDays4 + 1
                        mDlvPer4 = mActualQty / mPlanQty * 100
                    End If

                    If mDlvPer4 >= 90 And mDlvPer4 <= 110 Then
                        mLinearityPoint4 = mLinearityPoint4 + 1
                    End If

                    '                If mActualQty > 0 And mPlanQty = 0 Then
                    '                    mLinearityPoint4 = mLinearityPoint4 - 1
                    '                End If
                End If

                mWeekEndPlanQty = mWeekEndPlanQty + mPlanQty
                mWeekEndActualQty = mWeekEndActualQty + mActualQty

                If mPlanQty = 0 Then
                    mDlvPer = 0
                Else
                    mPlanDays = mPlanDays + 1
                    mDlvPer = mActualQty / mPlanQty * 100
                End If

                If mDlvPer >= 90 And mDlvPer <= 110 Then
                    mLinearityPoint = mLinearityPoint + 1
                End If

                '            If mActualQty > 0 And mPlanQty = 0 Then
                '                mLinearityPoint = mLinearityPoint - 1
                '            End If

                mPlanQty = 0
                mActualQty = 0
                RsTemp.MoveNext()
            Loop
        End If
        ''18/02/2018

        '    If mWeekEndPlanQty1 <> 0 Then
        '        mQtyPer1 = (mWeekEndActualQty1 / mWeekEndPlanQty1 * 100)
        '        mQtyPer1 = IIf(mQtyPer1 > 100, 100, mQtyPer1)
        '    End If
        '
        '    If mPlanDays1 <> 0 Then
        '        mLinearityPer1 = (mLinearityPoint1 * 100 / mPlanDays1)
        '    End If
        '
        '    If mWeekEndPlanQty2 <> 0 Then
        '        mQtyPer2 = (mWeekEndActualQty2 / mWeekEndPlanQty2 * 100)
        '        mQtyPer2 = IIf(mQtyPer2 > 100, 100, mQtyPer2)
        '    End If
        '
        '    If mPlanDays2 <> 0 Then
        '        mLinearityPer2 = (mLinearityPoint2 * 100 / mPlanDays2)
        '    End If
        '
        '    If mWeekEndPlanQty3 <> 0 Then
        '        mQtyPer3 = (mWeekEndActualQty3 / mWeekEndPlanQty3 * 100)
        '        mQtyPer3 = IIf(mQtyPer3 > 100, 100, mQtyPer3)
        '    End If
        '
        '    If mPlanDays3 <> 0 Then
        '        mLinearityPer3 = (mLinearityPoint3 * 100 / mPlanDays3)
        '    End If
        '
        '    If mWeekEndPlanQty4 <> 0 Then
        '        mQtyPer4 = (mWeekEndActualQty4 / mWeekEndPlanQty4 * 100)
        '        mQtyPer4 = IIf(mQtyPer4 > 100, 100, mQtyPer4)
        '    End If
        '
        '    If mPlanDays4 <> 0 Then
        '        mLinearityPer4 = (mLinearityPoint4 * 100 / mPlanDays4)
        '    End If
        '
        '    If mWeekEndPlanQty <> 0 Then
        '        mQtyPer = (mWeekEndActualQty / mWeekEndPlanQty * 100)   ''(mQtyPer1 + mQtyPer2 + mQtyPer3 + mQtyPer4) / 4  ''
        '        mQtyPer = IIf(mQtyPer > 100, 100, mQtyPer)
        '    End If
        '
        '    If mPlanDays <> 0 Then
        '        mLinearityPer = (mLinearityPer1 + mLinearityPer2 + mLinearityPer3 + mLinearityPer4) / (mLinerCnt1 + mLinerCnt2 + mLinerCnt3 + mLinerCnt4)  ''(mLinearityPoint * 100 / mPlanDays)
        '    End If



        mLinearityPer1 = 0
        mLinearityPer2 = 0
        mLinearityPer3 = 0
        mLinearityPer4 = 0

        mLinerCnt1 = 0
        mLinerCnt2 = 0
        mLinerCnt3 = 0
        mLinerCnt4 = 0

        If mWeekEndPlanQty1 = 0 And mWeekEndActualQty1 = 0 Then
            mLinerCnt1 = 0
            '    ElseIf mWeekEndPlanQty1 = 0 And mWeekEndActualQty1 <> 0 Then

        Else
            mLinerCnt1 = 1
            If mWeekEndPlanQty1 = 0 Then
                mQtyPer1 = 0
            Else
                mQtyPer1 = (mWeekEndActualQty1 / mWeekEndPlanQty1 * 100)
            End If

            If mQtyPer1 >= 90 And mQtyPer1 <= 110 Then
                mLinearityPer1 = 1
            End If
        End If

        If mWeekEndPlanQty2 = 0 And mWeekEndActualQty2 = 0 Then
            mLinerCnt2 = 0
            '    ElseIf mWeekEndPlanQty2 = 0 And mWeekEndActualQty2 <> 0 Then

        Else
            mLinerCnt2 = 1
            If mWeekEndPlanQty2 = 0 Then
                mQtyPer2 = 0
            Else
                mQtyPer2 = (mWeekEndActualQty2 / mWeekEndPlanQty2 * 100)
            End If

            If mQtyPer2 >= 90 And mQtyPer2 <= 110 Then
                mLinearityPer2 = 1
            End If
        End If

        If mWeekEndPlanQty3 = 0 And mWeekEndActualQty3 = 0 Then
            mLinerCnt3 = 0
            '    ElseIf mWeekEndPlanQty3 = 0 And mWeekEndActualQty3 <> 0 Then

        Else
            mLinerCnt3 = 1
            If mWeekEndPlanQty3 = 0 Then
                mQtyPer3 = 0
            Else
                mQtyPer3 = (mWeekEndActualQty3 / mWeekEndPlanQty3 * 100)
            End If
            If mQtyPer3 >= 90 And mQtyPer3 <= 110 Then
                mLinearityPer3 = 1
            End If
        End If

        If mWeekEndPlanQty4 = 0 And mWeekEndActualQty4 = 0 Then
            mLinerCnt4 = 0
            '    ElseIf mWeekEndPlanQty4 = 0 And mWeekEndActualQty4 <> 0 Then

        Else
            mLinerCnt4 = 1
            If mWeekEndPlanQty4 = 0 Then
                mQtyPer4 = 0
            Else
                mQtyPer4 = (mWeekEndActualQty4 / mWeekEndPlanQty4 * 100)
            End If
            If mQtyPer4 >= 90 And mQtyPer4 <= 110 Then
                mLinearityPer4 = 1
            End If
        End If


        If mWeekEndPlanQty <> 0 Then
            mQtyPer = (mWeekEndActualQty / mWeekEndPlanQty * 100) ''(mQtyPer1 + mQtyPer2 + mQtyPer3 + mQtyPer4) / 4  ''
            '        mQtyPer = IIf(mQtyPer > 100, 100, mQtyPer)
        End If

        If mPlanDays <> 0 Then
            mLinearityPer = (mLinearityPer1 + mLinearityPer2 + mLinearityPer3 + mLinearityPer4) / (mLinerCnt1 + mLinerCnt2 + mLinerCnt3 + mLinerCnt4) ''(mLinearityPoint * 100 / mPlanDays)
        End If

        With SprdMain

            .Row = mRow

            .Col = ColPlanQty1
            .Text = CStr(mWeekEndPlanQty1)

            .Col = ColActualQty1
            .Text = CStr(mWeekEndActualQty1)

            .Col = ColQtyRating1
            .Text = CStr(mQtyPer1)

            .Col = ColLineRating1
            .Text = CStr(mLinearityPer1)

            .Col = ColPlanQty2
            .Text = CStr(mWeekEndPlanQty2)

            .Col = ColActualQty2
            .Text = CStr(mWeekEndActualQty2)

            .Col = ColQtyRating2
            .Text = CStr(mQtyPer2)

            .Col = ColLineRating2
            .Text = CStr(mLinearityPer2)

            .Col = ColPlanQty3
            .Text = CStr(mWeekEndPlanQty3)

            .Col = ColActualQty3
            .Text = CStr(mWeekEndActualQty3)

            .Col = ColQtyRating3
            .Text = CStr(mQtyPer3)

            .Col = ColLineRating3
            .Text = CStr(mLinearityPer3)

            .Col = ColPlanQty4
            .Text = CStr(mWeekEndPlanQty4)

            .Col = ColActualQty4
            .Text = CStr(mWeekEndActualQty4)

            .Col = ColQtyRating4
            .Text = CStr(mQtyPer4)

            .Col = ColLineRating4
            .Text = CStr(mLinearityPer4)

            .Col = ColPlanQty
            .Text = CStr(mWeekEndPlanQty)

            .Col = ColActualQty
            .Text = CStr(mWeekEndActualQty)

            .Col = ColQtyRating
            .Text = CStr(mQtyPer)

            .Col = ColLineRating
            .Text = CStr(mLinearityPer)

            .Col = ColOverAllDRRating
            pOverAllDRRating = (0.6 * mQtyPer) + (0.3 * mLinearityPer * 100) + (0.1 * IIf(mQtyPer + (mLinearityPer * 100) <= 0, 0, mPremiumFreight))
            pOverAllDRRating = System.Math.Round(pOverAllDRRating, 0)
            .Text = CStr(pOverAllDRRating)

        End With
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub CalcQRRating(ByRef mRow As Integer, ByRef pPONO As Double, ByRef pItemCode As String, ByRef pOverAllQRRating As Double, ByRef pSupplierCode As String)

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mStartDate As String
        Dim mEndDate As String
        Dim a As Double
        Dim B As Double
        Dim c As Double
        Dim D As Double
        Dim e As Double
        Dim N As Double

        Dim F As Double
        Dim G As Double
        Dim H As Double

        Dim mDivisionCode As Double
        Dim mLineRej As Double

        mStartDate = " 01/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mEndDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        pOverAllQRRating = 0

        mLineRej = 0


        SqlStr = " SELECT SUM(RTN_QTY) RTN_QTY " & vbCrLf & " FROM INV_SRN_HDR IH, INV_SRN_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN" & vbCrLf & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "' AND FROM_STOCK_TYPE='ST' AND TO_STOCK_TYPE='RJ' AND IH.STATUS='Y'" & vbCrLf _
                & " AND IH.SRN_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.SRN_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(RTN_QTY)>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mLineRej = IIf(IsDBNull(RsTemp.Fields("RTN_QTY").Value), 0, RsTemp.Fields("RTN_QTY").Value)
        End If

        SqlStr = " SELECT SUM(LOT_ACCEPT) LOT_ACCEPT, " & vbCrLf & " SUM(LOT_ACCEPT_DEV) LOT_ACCEPT_DEV, SUM(LOT_ACC_SEG) LOT_ACC_SEG, " & vbCrLf & " SUM(LOT_ACC_RWK) LOT_ACC_RWK, SUM(REJECTED_QTY) REJECTED_QTY, " & vbCrLf & " SUM(RECEIVED_QTY) RECEIVED_QTY " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND ID.REF_AUTO_KEY_NO=" & pPONO & "" & vbCrLf & " AND ID.ITEM_CODE='" & pItemCode & "'" & vbCrLf _
            & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


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
            e = System.Math.Abs(IIf(IsDbNull(RsTemp.Fields("REJECTED_QTY").Value), 0, RsTemp.Fields("REJECTED_QTY").Value))
            N = IIf(IsDbNull(RsTemp.Fields("RECEIVED_QTY").Value), 0, RsTemp.Fields("RECEIVED_QTY").Value)
            a = a - B - c - D - mLineRej

            F = GetSaleReturn(pSupplierCode, pItemCode, mStartDate, mEndDate)
            G = GetCustomerLineStop(pSupplierCode, pItemCode, mStartDate, mEndDate)
            H = 0 '' Warranty Return Due to BOP
        End If

        'A Stands for 100% straight accepted nos.
        'B Stands for conditionally accepted or accepted on deviation nos.
        'C Stands for segregation nos.
        'D Stands for segregation and subsequent rework nos.
        'E Stands for rejected nos.

        'F Stands for customer complaint/QFR received due to BOP issue (?2 nos. then 2% reduction, >2 then 5% deduction in QR)
        'G Stands for Customer Line Loss (10% deduction in QR)
        'H Stands for Warranty parts related BOP issue (2% reduction in QR)
        'N Stands for total quantity received.
        '
        'For an example:
        '03 lots of 1000 nos. each were received from the supplier and one lot was 100% rejected, 01 lot was segregated and 01 lot was straight accepted & 3 customer complaint received for BOP Issue in a month
        '
        'The Quantity rating for that item =
        '
        'QR = [(1000*100%)+(0*75%) + (1000*50%)+{0*(-50%)}+(1000*0)] * 100 - (5 + 0 + 0)
        '3000
        '= (1500*100/ 3000) -5%
        '= 50%-5%
        '=45%
        '

        'QR = [(A*100%)+(B*75%)+(C*50%)+{D*(-50%)}+(E*0) -(F+G+H)
        '                                       N

        With SprdMain
            .Row = mRow
            .Col = ColQRating
            If N = 0 Then
                pOverAllQRRating = 0
            Else
                pOverAllQRRating = (((a * 1) + (B * 0.75) + (c * 0.5) + (D * (-0.5)) + (e * 0)) / N) - (F + G + H)
                pOverAllQRRating = pOverAllQRRating * 100
            End If

            pOverAllQRRating = System.Math.Round(pOverAllQRRating, 0)
            .Text = CStr(pOverAllQRRating)

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetSaleReturn(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mStartDate As String, ByRef mEndDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSaleReturn As Double

        Dim mDivisionCode As Double


        GetSaleReturn = 0
        mSaleReturn = 0

        SqlStr = "SELECT COUNT(DISTINCT IH.AUTO_KEY_REF) AS AUTO_KEY_REF" & vbCrLf & " FROM PRD_SALERETURN_HDR IH, PRD_SALERETURN_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.BOP_SUPP_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.BOP_ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IH.MRR_DATE >= TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.MRR_DATE <= TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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

        ''19-02-2018
        If mSaleReturn > 2 Then
            GetSaleReturn = 0.05
        ElseIf mSaleReturn > 0 Then
            GetSaleReturn = 0.01
        End If
        ''Change By Haridwar Mail dt. 16-04-2018
        '    If mSaleReturn > 0 Then
        '        GetSaleReturn = 0.1
        '    End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetPremiumFreight(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mStartDate As String, ByRef mEndDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPremiumFreight As Double

        Dim mDivisionCode As Double


        GetPremiumFreight = 0
        mPremiumFreight = 0

        SqlStr = "SELECT COUNT(DISTINCT IH.AUTO_KEY_MRR) AS AUTO_KEY_REF" & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND PREMIUM_FRIGHT = 'Y'" & vbCrLf _
            & " AND IH.MRR_DATE >= TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.MRR_DATE <= TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '    If cboDivision.ListIndex > 0 Then
        '        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mDivisionCode = Trim(MasterNo)
        '        End If
        '        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(mDivisionCode) & ""
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPremiumFreight = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_REF").Value), 0, RsTemp.Fields("AUTO_KEY_REF").Value)
        End If

        If mPremiumFreight = 0 Then
            GetPremiumFreight = 100
        ElseIf mPremiumFreight = 1 Then
            GetPremiumFreight = 25
        End If
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
        mTimeLoss = 0

        SqlStr = "SELECT SUM(TIME_LOSS_MIN) AS TIME_LOSS_MIN" & vbCrLf & " FROM INV_CUSTOMER_LINE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND REF_DATE >= TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE <= TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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

        '18/02/2018
        '    If mTimeLoss > 30 Then
        '        GetCustomerLineStop = 0.05
        '    ElseIf mTimeLoss > 0 Then
        '        GetCustomerLineStop = 0.01
        '    End If
        '    If mTimeLoss > 0 Then
        '        GetCustomerLineStop = 0.05
        '    End If

        ' Change from Haridwar  Mail 16-04-2018
        If mTimeLoss > 0 Then
            GetCustomerLineStop = 0.1
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CalcSRRating(ByRef mRow As Integer, ByRef pSuppCode As String, ByRef pItemCode As String, ByRef pOverAllSRRating As Double)

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mPDIRStatus As Double
        Dim mQFRStatus As Double
        Dim mTotLotRecd As Double
        Dim mTotPDIRRecd As Double

        Dim mTotQFR As Double
        Dim mTotQFRRecd As Double

        Dim mTotRework As Double
        Dim mTotReworkByHema As Double
        Dim mReworkStatus As Double

        Dim mTotRepeated As Double
        Dim mRepeated As Double
        Dim mRepeatedStatus As Double

        Dim mResponeStatus As Double
        Dim mDivisionCode As Double

        mStartDate = " 01/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mEndDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        pOverAllSRRating = 0

        ''& " AND ID.ITEM_CODE='" & pItemCode & "'"

        SqlStr = " SELECT COUNT(ID.AUTO_KEY_MRR) TOT_LOT, " & vbCrLf & " PDIR_FLAG " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf _
            & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        SqlStr = SqlStr & vbCrLf & "GROUP BY PDIR_FLAG"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mTotLotRecd = 0

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF

                mTotLotRecd = mTotLotRecd + IIf(IsDbNull(RsTemp.Fields("TOT_LOT").Value), 0, RsTemp.Fields("TOT_LOT").Value)
                If RsTemp.Fields("PDIR_FLAG").Value = "Y" Then
                    mTotPDIRRecd = IIf(IsDbNull(RsTemp.Fields("TOT_LOT").Value), 0, RsTemp.Fields("TOT_LOT").Value)
                End If
                RsTemp.MoveNext()
            Loop
        End If

        If mTotLotRecd = 0 Then
            mPDIRStatus = 0
        Else
            mPDIRStatus = mTotPDIRRecd / mTotLotRecd
        End If

        SqlStr = " SELECT  " & vbCrLf & " COUNT(AUTO_KEY_FLASH) TOT_QFR " & vbCrLf & " FROM QAL_FLASH_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf _
            & " AND FLASH_RPT_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND FLASH_RPT_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mTotLotRecd = 0
        If RsTemp.EOF = False Then
            mTotQFR = IIf(IsDbNull(RsTemp.Fields("TOT_QFR").Value), 0, RsTemp.Fields("TOT_QFR").Value)
        End If

        SqlStr = " SELECT  " & vbCrLf & " COUNT(AUTO_KEY_FLASH) TOT_QFRRecd " & vbCrLf & " FROM QAL_FLASH_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf _
            & " AND FLASH_RPT_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND FLASH_RPT_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REPLY_RECV='Y'" & vbCrLf & " AND FLASH_RPT_DATE-REPLY_RECV_DATE<=5"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mTotQFRRecd = IIf(IsDbNull(RsTemp.Fields("TOT_QFRRecd").Value), 0, RsTemp.Fields("TOT_QFRRecd").Value)
        End If

        If mTotQFR = 0 Then
            mQFRStatus = 1
        Else
            mQFRStatus = mTotQFRRecd / mTotQFR
        End If


        SqlStr = " SELECT  " & vbCrLf & " COUNT(AUTO_KEY_FLASH) TOT_QFR,REWORK_BY " & vbCrLf & " FROM QAL_FLASH_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf _
            & " AND FLASH_RPT_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND FLASH_RPT_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND REWORK_FLAG='Y'" & vbCrLf & " GROUP BY REWORK_BY "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mTotRework = 0
        mTotReworkByHema = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mTotRework = mTotRework + IIf(IsDbNull(RsTemp.Fields("TOT_QFR").Value), 0, RsTemp.Fields("TOT_QFR").Value)
                If RsTemp.Fields("REWORK_BY").Value = "C" Then
                    mTotReworkByHema = IIf(IsDbNull(RsTemp.Fields("TOT_QFR").Value), 0, RsTemp.Fields("TOT_QFR").Value)
                End If
                RsTemp.MoveNext()
            Loop
        End If


        If mTotRework = 0 Then
            mReworkStatus = 1
        Else
            mReworkStatus = mTotReworkByHema / mTotRework
        End If

        SqlStr = " SELECT  " & vbCrLf & " COUNT(AUTO_KEY_FLASH) TOT_QFR,PROBLEM_STATUS " & vbCrLf & " FROM QAL_FLASH_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf _
            & " AND FLASH_RPT_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND FLASH_RPT_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " GROUP BY PROBLEM_STATUS "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mTotRepeated = 0
        mRepeated = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mTotRepeated = mTotRepeated + IIf(IsDbNull(RsTemp.Fields("TOT_QFR").Value), 0, RsTemp.Fields("TOT_QFR").Value)
                If RsTemp.Fields("PROBLEM_STATUS").Value = "R" Then
                    mRepeated = IIf(IsDbNull(RsTemp.Fields("TOT_QFR").Value), 0, RsTemp.Fields("TOT_QFR").Value)
                End If
                RsTemp.MoveNext()
            Loop
        End If


        If mTotRepeated = 0 Then
            mRepeatedStatus = 1
        Else
            mRepeatedStatus = mRepeated / mTotRepeated
        End If

        SqlStr = " SELECT  " & vbCrLf & " RES_POINT " & vbCrLf & " FROM PUR_SUPP_CUST_RES" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCode & "'" & vbCrLf _
            & " AND RES_DATE>=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND RES_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mResponeStatus = 0
        If RsTemp.EOF = False Then
            mResponeStatus = IIf(IsDbNull(RsTemp.Fields("RES_POINT").Value), 0, RsTemp.Fields("RES_POINT").Value)
        End If

        pOverAllSRRating = (mPDIRStatus * 10) + (mQFRStatus * 50) + (mReworkStatus * 10) + (mRepeatedStatus * 20) + (mResponeStatus)

        With SprdMain
            .Row = mRow
            .Col = ColPDIR
            .Text = CStr(mPDIRStatus * 10)

            .Col = ColQF
            .Text = CStr(mQFRStatus * 50)

            .Col = ColReworkBy
            .Text = CStr(mReworkStatus * 10)

            .Col = ColRepeated
            .Text = CStr(mRepeatedStatus * 20)

            .Col = ColResones
            .Text = CStr(mResponeStatus)

            .Col = ColSRating
            pOverAllSRRating = System.Math.Round(pOverAllSRRating, 0)
            .Text = CStr(pOverAllSRRating)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function GetActualQty(ByRef pDSNo As Double, ByRef mSerialDay As String, ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPONo As Double
        Dim mSuppCode As String = ""

        Dim mDivisionCode As Double


        GetActualQty = 0

        SqlStr = "SELECT SUPP_CUST_CODE, AUTO_KEY_PO FROM PUR_DELV_SCHLD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_DELV=" & pDSNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPONo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_PO").Value), "-1", RsTemp.Fields("AUTO_KEY_PO").Value)
            mSuppCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
        End If

        SqlStr = "SELECT SUM(RECEIVED_QTY) AS RECEIVED_QTY" & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
            & " AND IH.MRR_DATE=TO_DATE('" & VB6.Format(mSerialDay, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'" & vbCrLf _
            & " AND ID.REF_AUTO_KEY_NO=" & mPONo & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetActualQty = IIf(IsDbNull(RsTemp.Fields("RECEIVED_QTY").Value), 0, RsTemp.Fields("RECEIVED_QTY").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged
        lblNewDate.Text = txtMonth.Text
    End Sub
End Class
