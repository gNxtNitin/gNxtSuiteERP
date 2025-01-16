Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamVendorHistory
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColMRRNo As Short = 2
    Private Const ColMRRDate As Short = 3
    Private Const colSupplier As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColItemDesc As Short = 6
    Private Const ColReceived As Short = 7
    Private Const ColAccepted As Short = 8
    Private Const ColUnderDev As Short = 9
    Private Const ColSeggregated As Short = 10
    Private Const ColReworked As Short = 11
    Private Const ColRejected As Short = 12
    Private Const ColPPM As Short = 13
    Private Const ColPPM_SE As Short = 14
    Private Const ColPPM_RW As Short = 15
    Private Const ColPPM_RJ As Short = 16
    Private Const ColRemarks As Short = 17
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mClickProcess As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
        cmdGraph.Enabled = pPrintEnable
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
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdSearch.Enabled = True
        End If
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


    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdGraph_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGraph.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonGraph(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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
        mTitle = "Vendor History Card"
        mSubTitle = VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " - " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")


        If optShow(3).Checked = True Then
            SqlStr = MakeSQLOverAll
        Else
            SqlStr = MakeSQL
        End If

        If optShow(0).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\VendorHistCard.rpt"
        ElseIf optShow(1).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\VendorHistCardItem.rpt"
        ElseIf optShow(2).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\VendorHistCardSupp.rpt"
        ElseIf optShow(3).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\VendorHistCardOverAll.rpt"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportonGraph(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        SqlStr = " SELECT * FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"


        If optGroup(0).Checked = True Then
            If optGraphShow(0).Checked = True Then
                SqlStr = SqlStr & " AND FIELD15<>0"
            ElseIf optGraphShow(1).Checked = True Then
                SqlStr = SqlStr & " AND FIELD16<>0"
            ElseIf optGraphShow(2).Checked = True Then
                SqlStr = SqlStr & " AND FIELD14<>0"
            ElseIf optGraphShow(3).Checked = True Then
                SqlStr = SqlStr & " AND FIELD13<>0"
            End If
        End If

        SqlStr = SqlStr & " ORDER BY SUBROW"


        If optGraphShow(0).Checked = True Then
            mTitle = "PPM REWORK"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\PPM_Graph_RW.rpt"
            '        Report1.g
        ElseIf optGraphShow(1).Checked = True Then
            mTitle = "PPM REJECTION"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\PPM_Graph_RJ.rpt"
        ElseIf optGraphShow(2).Checked = True Then
            mTitle = "PPM SEGREGATION"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\PPM_Graph_SG.rpt"
        ElseIf optGraphShow(3).Checked = True Then
            mTitle = "PPM OVERALL"
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\PPM_Graph_OA.rpt"
        End If

        mTitle = mTitle & "( FROM : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " TO : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & " )"

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " (Supplier Name : " & txtSupplier.Text & ")"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " (Item Name : " & TxtItemName.Text & ")"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, myMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
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
    Private Sub frmParamVendorHistory_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Vendor History Card"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamVendorHistory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamVendorHistory_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdSearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False
        Call FillItemType()

        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamVendorHistory_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamVendorHistory_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub




    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)
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

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDateFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColRemarks
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)


            If optGroup(0).Checked = True Then
                .ColHidden = True
            ElseIf optGroup(1).Checked = True Then
                .ColHidden = False
            ElseIf optGroup(2).Checked = True Then
                .ColHidden = False
            ElseIf optGroup(3).Checked = True Then
                .ColHidden = False
            End If

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRNo, 9)
            If optShow(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRDate, 9)
            If optShow(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, 20)
            .ColHidden = IIf(optShow(3).Checked = True, True, False)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)
            .ColHidden = IIf(optShow(2).Checked = True Or optShow(3).Checked = True, True, False)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 25)
            .ColHidden = IIf(optShow(2).Checked = True, True, False)

            For cntCol = ColReceived To ColPPM
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
            Next

            For cntCol = ColPPM_SE To ColPPM_RJ
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
                .ColHidden = False ''IIf(optShow(3).Value = True, False, True)
            Next

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 15)
            .ColHidden = True

            '        If optShow(3).Value = True Then
            '            .Row = 0
            '            .Col = ColPPM_SE
            '            .Text = "PPM (Seggregated)"
            '
            '            .Col = ColPPM_RW
            '            .Text = "PPM (Reworked)"
            '
            '            .Col = ColPPM_RJ
            '            .Text = "PPM (Rejection)"
            '        End If

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


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If optShow(0).Checked = True Then
            SqlStr = MakeSQL
        ElseIf optShow(3).Checked = True Then
            SqlStr = MakeSQLOverAll
        Else
            SqlStr = MakeSQLSumm
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mItemTypeCode As String = ""

        mTrnTypeStr = ""
        MakeSQL = ""
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mItemTypeCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", "'" & mItemTypeCode & "'", mTrnTypeStr & "," & "'" & mItemTypeCode & "'")
            End If
        Next

        ''SELECT CLAUSE...
        ''+GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE)

        If optGroup(0).Checked = True Then
            MakeSQL = " SELECT CMST.SUPP_CUST_NAME,"
        ElseIf optGroup(1).Checked = True Then
            MakeSQL = " SELECT 'WK' || TO_CHAR(IGH.MRR_DATE,'WW'),"
        ElseIf optGroup(2).Checked = True Then
            MakeSQL = " SELECT TO_CHAR(IGH.MRR_DATE,'MM MONTH'),"
        ElseIf optGroup(3).Checked = True Then
            MakeSQL = " SELECT 'QTR' || TO_CHAR(IGH.MRR_DATE,'Q'),"
        End If

        ''Not Group
        MakeSQL = MakeSQL & vbCrLf & " IGH.AUTO_KEY_MRR," & vbCrLf & " TO_CHAR(IGH.MRR_DATE,'DD/MM/YYYY')," & vbCrLf & " CMST.SUPP_CUST_NAME," & vbCrLf & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf & " TO_CHAR(IGD.RECEIVED_QTY), TO_CHAR(IGD.APPROVED_QTY+GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO)), " & vbCrLf & " TO_CHAR(IGD.LOT_ACCEPT_DEV), TO_CHAR(IGD.LOT_ACC_SEG), " & vbCrLf & " TO_CHAR(IGD.LOT_ACC_RWK), TO_CHAR(IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO)), " & vbCrLf & " CASE WHEN IGD.RECEIVED_QTY<>0 THEN TO_CHAR((LOT_ACC_SEG+LOT_ACC_RWK+IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))*100*10000/IGD.RECEIVED_QTY) END AS PPM, " & vbCrLf & " '0.00','0.00','0.00',''"

        'GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE)
        ''Change 10-03-2008 '' Only Rejection PPM
        '& " CASE WHEN IGD.RECEIVED_QTY<>0 THEN TO_CHAR((LOT_ACC_SEG+LOT_ACC_RWK+IGD.REJECTED_QTY)*100*10000/IGD.RECEIVED_QTY) END AS PPM, "
        ''IGD.REJECTED_QTY changed by umesh dt 10-08-2005
        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM INV_GATE_HDR IGH, INV_GATE_DET IGD," & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_MRR=IGD.AUTO_KEY_MRR" & vbCrLf & " AND IGH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IGH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE"

        If mTrnTypeStr <> "" Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " AND INVMST.CATEGORY_CODE IN " & mTrnTypeStr & ""
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IGH.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If

        End If


        'ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,INVMST.ITEM_SHORT_DESC,IGH.AUTO_KEY_MRR, IGH.MRR_DATE"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function MakeSQLSumm() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mItemTypeCode As String = ""
        MakeSQLSumm = ""
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

        ''SELECT CLAUSE...

        If optGroup(0).Checked = True Then
            MakeSQLSumm = " SELECT CMST.SUPP_CUST_NAME,"
        ElseIf optGroup(1).Checked = True Then
            MakeSQLSumm = " SELECT 'WK' || TO_CHAR(IGH.MRR_DATE,'WW'),"
        ElseIf optGroup(2).Checked = True Then
            MakeSQLSumm = " SELECT TO_CHAR(IGH.MRR_DATE,'MM MONTH'),"
        ElseIf optGroup(3).Checked = True Then
            MakeSQLSumm = " SELECT 'QTR' || TO_CHAR(IGH.MRR_DATE,'Q'),"
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " ''," & vbCrLf & " ''," & vbCrLf & " CMST.SUPP_CUST_NAME,"


        If optShow(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC,"
        ElseIf optShow(2).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " '', '',"
        End If

        ''+GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE)

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " TO_CHAR(SUM(IGD.RECEIVED_QTY)), TO_CHAR(SUM(IGD.APPROVED_QTY+GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))), " & vbCrLf & " TO_CHAR(SUM(IGD.LOT_ACCEPT_DEV)), TO_CHAR(SUM(IGD.LOT_ACC_SEG)), " & vbCrLf & " TO_CHAR(SUM(IGD.LOT_ACC_RWK)), TO_CHAR(SUM(IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))), " & vbCrLf & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(LOT_ACC_SEG+LOT_ACC_RWK+IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.LOT_ACC_SEG)*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.LOT_ACC_RWK)*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf & " ''"
        '
        '& " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(LOT_ACC_SEG+LOT_ACC_RWK+IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE))*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf _
        ''            & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.LOT_ACC_SEG)*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf _
        ''            & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.LOT_ACC_RWK)*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf _
        ''            & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE))*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf _
        ''            & " ''"
        '

        'GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE)

        ''LOT_ACC_SEG+LOT_ACC_RWK+IGD.REJECTED_QTY ''Only Rejection after Reoffer '10-03-2008
        'IGD.REJECTED_QTY ' Change by umesh dt.10-08-2005

        ''FROM CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " FROM INV_GATE_HDR IGH, INV_GATE_DET IGD," & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_MRR=IGD.AUTO_KEY_MRR" & vbCrLf & " AND IGH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IGH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE "

        If mTrnTypeStr <> "" Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND INVMST.CATEGORY_CODE IN " & mTrnTypeStr & ""
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If

        End If

        'GROUP BY CLAUSE ...

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " GROUP BY " & vbCrLf & " CMST.SUPP_CUST_NAME"

        If optShow(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " ,IGD.ITEM_CODE, INVMST.ITEM_SHORT_DESC"
        End If

        If optGroup(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & " , 'WK' || TO_CHAR(IGH.MRR_DATE,'WW')"
        ElseIf optGroup(2).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & " , TO_CHAR(IGH.MRR_DATE,'MM MONTH'),TO_CHAR(IGH.MRR_DATE,'YYYYMM')"
        ElseIf optGroup(3).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & " , TO_CHAR(IGH.MRR_DATE,'Q')"
        End If

        'ORDER CLAUSE...
        If optShow(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,INVMST.ITEM_SHORT_DESC"
        ElseIf optShow(2).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME"
        ElseIf optShow(3).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " ORDER BY INVMST.ITEM_SHORT_DESC"
        End If

        If optGroup(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & " , 'WK' || TO_CHAR(IGH.MRR_DATE,'WW')"
        ElseIf optGroup(2).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & " ,TO_CHAR(IGH.MRR_DATE,'YYYYMM')"
        ElseIf optGroup(3).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & " ,TO_CHAR(IGH.MRR_DATE,'Q')"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLOverAll() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mItemTypeCode As String = ""

        mTrnTypeStr = ""
        MakeSQLOverAll = ""
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mItemTypeCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", "'" & mItemTypeCode & "'", mTrnTypeStr & "," & "'" & mItemTypeCode & "'")
            End If
        Next

        ''SELECT CLAUSE...

        If optGroup(0).Checked = True Then
            MakeSQLOverAll = " SELECT GMST.GEN_DESC,"
        ElseIf optGroup(1).Checked = True Then
            MakeSQLOverAll = " SELECT 'WK' || TO_CHAR(IGH.MRR_DATE,'WW'),"
        ElseIf optGroup(2).Checked = True Then
            MakeSQLOverAll = " SELECT TO_CHAR(IGH.MRR_DATE,'MM MONTH'),"
        ElseIf optGroup(3).Checked = True Then
            MakeSQLOverAll = " SELECT 'QTR' || TO_CHAR(IGH.MRR_DATE,'Q'),"
        End If

        MakeSQLOverAll = MakeSQLOverAll & vbCrLf & " ''," & vbCrLf & " ''," & vbCrLf & " '','',GMST.GEN_DESC,"


        MakeSQLOverAll = MakeSQLOverAll & vbCrLf & " TO_CHAR(SUM(IGD.RECEIVED_QTY)), TO_CHAR(SUM(IGD.APPROVED_QTY+GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))), " & vbCrLf & " TO_CHAR(SUM(IGD.LOT_ACCEPT_DEV)), TO_CHAR(SUM(IGD.LOT_ACC_SEG)), " & vbCrLf & " TO_CHAR(SUM(IGD.LOT_ACC_RWK)), TO_CHAR(SUM(IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))), " & vbCrLf & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(LOT_ACC_SEG+LOT_ACC_RWK+IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.LOT_ACC_SEG)*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.LOT_ACC_RWK)*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf & " CASE WHEN SUM(IGD.RECEIVED_QTY)<>0 THEN TO_CHAR(SUM(IGD.REJECTED_QTY-GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE,IGD.REF_AUTO_KEY_NO))*100*10000/SUM(IGD.RECEIVED_QTY)) END AS PPM, " & vbCrLf & " ''"

        ''+GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE)
        ''+GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE)

        ''GETREOFFERQTY_NEW (IGH.COMPANY_CODE, IGH.AUTO_KEY_MRR, IGH.MRR_DATE, IGH.SUPP_CUST_CODE, IGD.ITEM_CODE)
        ''(LOT_ACC_SEG+LOT_ACC_RWK+IGD.REJECTED_QTY) ''Change only Rejection PPM (Include Reoffer)

        ''FROM CLAUSE...
        MakeSQLOverAll = MakeSQLOverAll & vbCrLf & " FROM INV_GATE_HDR IGH, INV_GATE_DET IGD," & vbCrLf & " INV_ITEM_MST INVMST, INV_GENERAL_MST GMST"

        ''WHERE CLAUSE...
        MakeSQLOverAll = MakeSQLOverAll & vbCrLf & " WHERE " & vbCrLf & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IGH.AUTO_KEY_MRR=IGD.AUTO_KEY_MRR" & vbCrLf & " AND IGD.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IGD.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE " & vbCrLf & " AND GMST.GEN_TYPE='C'"

        If mTrnTypeStr <> "" Then
            mTrnTypeStr = "(" & mTrnTypeStr & ")"
            MakeSQLOverAll = MakeSQLOverAll & vbCrLf & " AND INVMST.CATEGORY_CODE IN " & mTrnTypeStr & ""
        End If

        MakeSQLOverAll = MakeSQLOverAll & vbCrLf & " AND IGH.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQLOverAll = MakeSQLOverAll & vbCrLf & "AND IGH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLOverAll = MakeSQLOverAll & vbCrLf & "AND IGD.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If

        End If

        'GROUP BY CLAUSE ...
        MakeSQLOverAll = MakeSQLOverAll & vbCrLf & " GROUP BY GMST.GEN_DESC"

        If optGroup(1).Checked = True Then
            MakeSQLOverAll = MakeSQLOverAll & " , 'WK' || TO_CHAR(IGH.MRR_DATE,'WW')"
        ElseIf optGroup(2).Checked = True Then
            MakeSQLOverAll = MakeSQLOverAll & " , TO_CHAR(IGH.MRR_DATE,'MM MONTH'), TO_CHAR(IGH.MRR_DATE,'YYYYMM')"
        ElseIf optGroup(3).Checked = True Then
            MakeSQLOverAll = MakeSQLOverAll & " , TO_CHAR(IGH.MRR_DATE,'Q')"
        End If

        'ORDER CLAUSE...
        MakeSQLOverAll = MakeSQLOverAll & vbCrLf & " ORDER BY GMST.GEN_DESC"
        If optGroup(1).Checked = True Then
            MakeSQLOverAll = MakeSQLOverAll & " , 'WK' || TO_CHAR(IGH.MRR_DATE,'WW')"
        ElseIf optGroup(2).Checked = True Then
            MakeSQLOverAll = MakeSQLOverAll & " ,TO_CHAR(IGH.MRR_DATE,'YYYYMM')"
        ElseIf optGroup(3).Checked = True Then
            MakeSQLOverAll = MakeSQLOverAll & " ,TO_CHAR(IGH.MRR_DATE,'Q')"
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
End Class
