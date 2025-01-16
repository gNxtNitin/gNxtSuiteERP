Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamLoadingReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColSlipNo As Short = 2
    Private Const ColSlipDate As Short = 3

    Private Const ColInDate As Short = 4
    Private Const ColVehicleNo As Short = 5
    Private Const ColTransporter As Short = 6
    Private Const colRemarks As Short = 7
    Private Const ColTotQty As Short = 8
    Private Const ColTotPack As Short = 9
    Private Const ColVehicleType As Short = 10
    Private Const ColRefType As Short = 11
    Private Const ColRefNo As Short = 12
    Private Const ColRefDate As Short = 13
    Private Const colSupplier As Short = 14
    Private Const ColItemCode As Short = 15
    Private Const ColItemDesc As Short = 16
    Private Const ColUnit As Short = 17
    Private Const ColQty As Short = 18
    Private Const ColNoOfPkt As Short = 19
    Private Const ColItemWt As Short = 20
    Private Const ColTotalWt As Short = 21
    Private Const ColPackType As Short = 22
    'Private Const ColPackRecd As Short = 27
    'Private Const ColPackStd As Short = 28

    Private Const ColTripAmount As Short = 23
    Private Const ColOthAmount As Short = 24
    Private Const ColTollAmount As Short = 25
    Private Const ColNetAmount As Short = 26
    'Private Const ColPoint As Short = 33

    'Private Const ColStandardRate As Short = 34
    'Private Const ColStandardAmount As Short = 35
    Private Const ColFreightType As Short = 27
    Private Const ColADDUser As Short = 28
    Private Const ColADDDate As Short = 29
    Private Const ColMODUser As Short = 30
    Private Const ColMODDate As Short = 31

    Private Const ColMKEY As Short = 32

    Dim mClickProcess As Boolean

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
            TxtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearch.Enabled = True
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



    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONDNR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONDNR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONDNR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()
        mTitle = IIf(lblBookType.Text = "L", "Loading Register", "UnLoading Register")
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        If optShow(0).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\LoadingReg.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\LoadingRegSummary.rpt"
        End If

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr

        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

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

    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)

        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamLoadingReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = IIf(lblBookType.Text = "L", "Loading Register", "UnLoading Register")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamLoadingReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Long

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

        chkVehicleAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtVehicleNo.Enabled = False
        cmdSearchVehicle.Enabled = False

        chkTransportAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtTransporter.Enabled = False
        cmdSearchTransport.Enabled = False


        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
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
    Private Sub frmParamLoadingReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamLoadingReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim xMkey As String



        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColMKEY
        xMkey = SprdMain.Text

        frmLoadingSlip.MdiParent = Me.MdiParent
        frmLoadingSlip.LblMKey.Text = xMkey
        frmLoadingSlip.lblBookType.Text = lblBookType.Text
        frmLoadingSlip.Show()
        frmLoadingSlip.frmLoadingSlip_Activated(Nothing, New System.EventArgs())
        frmLoadingSlip.txtSlipNo.Text = xMkey
        frmLoadingSlip.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
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
            .ColHidden = True

            For cntCol = ColSlipNo To colRemarks
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 9)
                .ColHidden = False
            Next

            For cntCol = ColVehicleType To ColUnit
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 9)
                .ColHidden = False
            Next

            For cntCol = ColTotQty To ColTotPack
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
                .ColHidden = False
            Next

            For cntCol = ColQty To ColTotalWt
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
                .ColHidden = False
            Next

            .Col = ColPackType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPackType, 9)
            .ColHidden = False

            '.Col = ColPackStd
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 3
            '.TypeFloatMin = CDbl("-99999999999")
            '.TypeFloatMax = CDbl("99999999999")
            '.TypeFloatMoney = False
            '.TypeFloatSeparator = False
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatSepChar = Asc(",")
            '.set_ColWidth(ColPackStd, 9)

            '.Col = ColPackRecd
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 3
            '.TypeFloatMin = CDbl("-99999999999")
            '.TypeFloatMax = CDbl("99999999999")
            '.TypeFloatMoney = False
            '.TypeFloatSeparator = False
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatSepChar = Asc(",")
            '.set_ColWidth(ColPackRecd, 9)

            For cntCol = ColTripAmount To ColNetAmount  '' ColStandardAmount
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
                .ColHidden = False
            Next

            For cntCol = ColFreightType To ColMODDate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 8)
                .ColHidden = False
            Next

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            For cntCol = ColRefType To ColRefType
                .Col = cntCol
                .ColHidden = IIf(optShow(0).Checked = True, False, True)
            Next

            .Col = ColRefNo
            .ColHidden = False

            For cntCol = ColRefDate To ColPackType      ''ColPackStd
                .Col = cntCol
                .ColHidden = IIf(OptShow(0).Checked = True, False, True)
            Next

            For cntCol = ColQty To ColTotalWt
                .Col = cntCol
                .ColHidden = False
            Next


            If OptShow(2).Checked Then
                For cntCol = ColLocked To ColVehicleNo
                    .Col = cntCol
                    .ColHidden = True
                Next


                .Col = colRemarks
                .ColHidden = True

                For cntCol = ColVehicleType To ColUnit
                    .Col = cntCol
                    .ColHidden = True
                Next

                .Col = ColPackType
                .ColHidden = True

                For cntCol = ColFreightType To ColMKEY
                    .Col = cntCol
                    .ColHidden = True
                Next
            ElseIf OptShow(1).Checked Then
                For cntCol = ColRefType To ColItemWt
                    .Col = cntCol
                    .ColHidden = True
                Next

            ElseIf OptShow(0).Checked Then
                For cntCol = ColTripAmount To ColNetAmount
                    .Col = cntCol
                    .ColHidden = True
                Next

                For cntCol = ColTripAmount To ColNetAmount
                    .Col = cntCol
                    .ColHidden = True
                Next

                For cntCol = ColTotQty To ColVehicleType
                    .Col = cntCol
                    .ColHidden = True
                Next

                .Col = colSupplier
                .ColHidden = False

            End If




            .set_ColWidth(ColTransporter, 20)
            .set_ColWidth(colSupplier, 20)
            .set_ColWidth(ColItemDesc, 20)

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
        Dim mRefNo As Double
        Dim mCustName As String = ""
        Dim mPoint As Integer
        Dim cntRow As Integer
        Dim mProductCode As String = ""
        Dim xInvType As String
        Dim xInvNo As Double
        Dim mStdAmount As Double
        Dim mStdRate As Double
        Dim mPackQty As Double
        Dim mBillNo As String = ""

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        FormatSprdMain(-1)
        'If optShow(0).Checked = True Then
        '    If ChkStandardTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
        '        With SprdMain
        '            For cntRow = 1 To .MaxRows
        '                .Row = cntRow
        '                .Col = ColSlipNo
        '                mRefNo = Val(.Text)

        '                .Col = ColRefType
        '                xInvType = Trim(.Text)

        '                .Col = ColRefNo
        '                xInvNo = Val(.Text)

        '                .Col = colSupplier
        '                mCustName = Trim(.Text)

        '                .Col = ColItemCode
        '                mProductCode = Trim(.Text)

        '                .Col = ColQty
        '                mPackQty = Val(.Text)

        '                '.Col = ColStandardRate
        '                'mStdRate = GetStandardAmount(mRefNo, mProductCode, mCustName, xInvType, xInvNo, "D")
        '                '.Text = CStr(Val(CStr(mStdRate)))

        '                '.Col = ColStandardAmount
        '                'mStdAmount = CDbl(VB6.Format(mPackQty * mStdRate, "0.00"))
        '                '.Text = CStr(Val(CStr(mStdAmount)))

        '            Next
        '        End With
        '    End If
        'Else
        '    With SprdMain
        '        For cntRow = 1 To .MaxRows
        '            .Row = cntRow
        '            .Col = ColSlipNo
        '            mRefNo = Val(.Text)

        '            If GetVehicleData(mRefNo, mCustName, mBillNo, mPoint) = False Then GoTo LedgError

        '            .Row = cntRow

        '            .Col = ColRefNo
        '            .Text = Trim(mBillNo)

        '            .Col = colSupplier
        '            .Text = Trim(mCustName)

        '            '.Col = ColPoint
        '            '.Text = Trim(CStr(mPoint))

        '            'If ChkStandardTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
        '            '    .Col = ColStandardAmount
        '            '    mStdAmount = GetStandardAmount(mRefNo, "", "", "", -1, "S")
        '            '    .Text = CStr(Val(CStr(mStdAmount)))
        '            'End If
        '        Next
        '    End With
        'End If

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mTotValue As Double

        Call MainClass.AddBlankfpSprdRow(SprdMain, ColTotQty)
        With SprdMain
            .Col = ColTransporter
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False


            For cntCol = ColQty To ColTotalWt
                mTotValue = 0
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = cntCol
                    mTotValue = mTotValue + Val(.Text)
                Next
                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mTotValue, "0.00")
            Next

            For cntCol = ColTripAmount To ColNetAmount
                mTotValue = 0
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = cntCol
                    mTotValue = mTotValue + Val(.Text)
                Next
                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mTotValue, "0.00")
            Next

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mDivision As Double
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        ''SELECT CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQL = " SELECT ''," & vbCrLf _
                & " TO_CHAR(IH.AUTO_KEY_LOAD) AS AUTO_KEY_LOAD,TO_CHAR(IH.SLIP_DATE,'DD/MM/YYYY HH24:MI')," & vbCrLf _
                & " TO_CHAR(IH.IN_DATE_TIME,'DD/MM/YYYY HH24:MI'),IH.VEHICLE_NO,IH.TRANSPORTER_NAME," & vbCrLf _
                & " IH.REMARKS,IH.TOT_QTY," & vbCrLf _
                & " IH.TOT_PACK,IH.VEHICLE_TYPE,ID.REF_TYPE," & vbCrLf _
                & " ID.REF_NO,ID.REF_DATE,ID.SUPP_CUST_NAME," & vbCrLf _
                & " ID.ITEM_CODE,ID.ITEM_SHORT_DESC,ID.ITEM_UOM," & vbCrLf _
                & " ID.PACKED_QTY,ID.NO_OF_PACKETS, INVMST.ITEM_WEIGHT,INVMST.ITEM_WEIGHT*ID.PACKED_QTY*.001," & vbCrLf _
                & " ID.PACK_TYPE," & vbCrLf _
                & " TRIP_AMOUNT, OTH_AMOUNT, TOLL_AMOUNT, NET_AMOUNT,DECODE(IH.FREIGHT_TYPE,'R','REGULAR','PREMIUM'), IH.ADDUSER,IH.ADDDATE,IH.MODUSER,IH.MODDATE," & vbCrLf _
                & " IH.AUTO_KEY_LOAD"

        ElseIf optShow(1).Checked = True Then
            MakeSQL = " SELECT DISTINCT ''," & vbCrLf _
                & " TO_CHAR(IH.AUTO_KEY_LOAD) AS AUTO_KEY_LOAD,TO_CHAR(IH.SLIP_DATE,'DD/MM/YYYY HH24:MI')," & vbCrLf _
                & " TO_CHAR(IH.IN_DATE_TIME,'DD/MM/YYYY HH24:MI'),IH.VEHICLE_NO,IH.TRANSPORTER_NAME," & vbCrLf _
                & " IH.REMARKS,IH.TOT_QTY," & vbCrLf _
                & " IH.TOT_PACK,IH.VEHICLE_TYPE,''," & vbCrLf _
                & " '','',''," & vbCrLf _
                & " '','',''," & vbCrLf _
                & " '','',0,NET_WT,''," & vbCrLf _
                & " IH.TRIP_AMOUNT, IH.OTH_AMOUNT, IH.TOLL_AMOUNT, IH.NET_AMOUNT, DECODE(IH.FREIGHT_TYPE,'R','REGULAR','PREMIUM'),IH.ADDUSER,IH.ADDDATE,IH.MODUSER,IH.MODDATE," & vbCrLf _
                & " IH.AUTO_KEY_LOAD"
        Else
            MakeSQL = " SELECT '',''," & vbCrLf _
               & " ''," & vbCrLf _
               & " '', '' ,IH.TRANSPORTER_NAME," & vbCrLf _
               & " '',SUM(IH.TOT_QTY) AS TOT_QTY," & vbCrLf _
               & " SUM(IH.TOT_PACK) AS TOT_PACK, '' VEHICLE_TYPE,''," & vbCrLf _
               & " '','',''," & vbCrLf _
               & " '','',''," & vbCrLf _
               & " SUM(IH.TOT_QTY),SUM(IH.TOT_PACK),0,SUM(NET_WT),''," & vbCrLf _
               & " SUM(IH.TRIP_AMOUNT), SUM(IH.OTH_AMOUNT), SUM(IH.TOLL_AMOUNT), SUM(IH.NET_AMOUNT), '','','','',''," & vbCrLf _
               & " ''"
        End If
        ''FROM CLAUSE...

        If OptShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & " FROM DSP_LOADING_HDR IH, DSP_LOADING_DET ID, INV_ITEM_MST INVMST"
        Else
            MakeSQL = MakeSQL & vbCrLf _
                & " FROM DSP_LOADING_HDR IH"
        End If


        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE 1=1"

        '& vbCrLf _
        '    & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

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
            MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If OptShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & " AND IH.AUTO_KEY_LOAD=ID.AUTO_KEY_LOAD AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"
        End If

        '            & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
        ''            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
        ''            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
        ''            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQL = MakeSQL & vbCrLf & "AND IH.BOOKTYPE='" & MainClass.AllowSingleQuote(lblBookType.Text) & "'"

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND ID.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If chkVehicleAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.VEHICLE_NO='" & MainClass.AllowSingleQuote(txtVehicleNo.Text) & "'"
        End If

        If chkTransportAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then    ''If Trim(txtTransporter.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TRANSPORTER_NAME ='" & MainClass.AllowSingleQuote(txtTransporter.Text) & "'"
        End If

        If optFreightType(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.FREIGHT_TYPE='R'"
        ElseIf optFreightType(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.FREIGHT_TYPE='P'"
        End If

        If OptVT(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.IS_TP_VEHICLE='N'"
        ElseIf OptVT(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.IS_TP_VEHICLE='Y'"
        End If

        If OptShowAck(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.IS_ACK_RECEIPT='Y'"
        ElseIf OptShowAck(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.IS_ACK_RECEIPT='N'"
        End If

        If chkWOCollection.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.WO_COLLECTION='Y'"
        End If

        If chkInComplete.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOT_PENDING_BILLS<>0"
        End If

        If chkPendingInTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND (IH.IN_DATE_TIME IS NULL OR IH.IN_DATE_TIME='')"
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If chkAck.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            MakeSQL = MakeSQL & vbCrLf _
                & " AND TO_CHAR(IH.SLIP_DATE,'YYYYMMDD')>='" & VB6.Format(txtDateFrom.Text, "YYYYMMDD") & "'" & vbCrLf _
                & " AND TO_CHAR(IH.SLIP_DATE,'YYYYMMDD')<='" & VB6.Format(txtDateTo.Text, "YYYYMMDD") & "'"
        Else
            MakeSQL = MakeSQL & vbCrLf _
                & " AND TO_CHAR(IH.ACK_RECEIPTDATE,'YYYYMMDD')>='" & VB6.Format(txtDateFrom.Text, "YYYYMMDD") & "'" & vbCrLf _
                & " AND TO_CHAR(IH.ACK_RECEIPTDATE,'YYYYMMDD')<='" & VB6.Format(txtDateTo.Text, "YYYYMMDD") & "'"

            '& " AND TO_DATE(IH.ACK_RECEIPTDATE,'DD-MON-YYYY')>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            '    & " AND TO_DATE(IH.ACK_RECEIPTDATE,'DD-MON-YYYY')<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        End If

        'GROUP BY

        If optShow(1).Checked = True Then
            'MakeSQL = MakeSQL & vbCrLf & "GROUP BY IH.AUTO_KEY_LOAD," & vbCrLf _
            '    & " TO_CHAR(IH.SLIP_DATE,'DD/MM/YYYY HH24:MI'), " & vbCrLf _
            '    & " TO_CHAR(IH.IN_DATE_TIME,'DD/MM/YYYY HH24:MI'),IH.VEHICLE_NO,IH.TRANSPORTER_NAME," & vbCrLf _
            '    & " IH.REMARKS,IH.TOT_QTY," & vbCrLf _
            '    & " IH.TOT_PACK,IH.VEHICLE_TYPE," & vbCrLf _
            '    & " IH.AUTO_KEY_LOAD,IH.TRIP_AMOUNT, IH.OTH_AMOUNT, IH.TOLL_AMOUNT, IH.NET_AMOUNT,DECODE(IH.FREIGHT_TYPE,'R','REGULAR','PREMIUM'),IH.ADDUSER,IH.ADDDATE,IH.MODUSER,IH.MODDATE"

        ElseIf OptShow(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY IH.TRANSPORTER_NAME"
        End If

        'ORDER CLAUSE...

        If OptShow(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.TRANSPORTER_NAME"
        Else
            If OptOrderBy(0).Checked = True Then
                MakeSQL = MakeSQL & vbCrLf & "ORDER BY TO_CHAR(IH.SLIP_DATE,'DD/MM/YYYY HH24:MI'), IH.AUTO_KEY_LOAD"
            ElseIf OptOrderBy(1).Checked = True Then
                If OptShow(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & "ORDER BY ID.ITEM_SHORT_DESC"
                Else
                    MakeSQL = MakeSQL & vbCrLf & "ORDER BY TO_CHAR(IH.SLIP_DATE,'DD/MM/YYYY HH24:MI'), IH.AUTO_KEY_LOAD"
                End If
            End If
        End If


        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function GetVehicleData(ByRef mRefNo As Double, ByRef mCustName As String, ByRef mBillNo As String, ByRef mPoint As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mCheckCustName As String = ""
        Dim mCheckCustNameStr As String = ""

        Dim mCheckBillNo As String
        Dim mCheckBillNoStr As String = ""

        ''SELECT CLAUSE...

        mPoint = 1
        mCustName = ""
        mBillNo = ""
        GetVehicleData = False
        SqlStr = " SELECT DISTINCT ID.SUPP_CUST_NAME, DECODE(REF_TYPE,'R','','S') || SUBSTR(REF_NO,1,LENGTH(REF_NO)-6) AS REF_NO " & vbCrLf & " FROM DSP_LOADING_HDR IH, DSP_LOADING_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_LOAD=ID.AUTO_KEY_LOAD " & vbCrLf & " AND IH.AUTO_KEY_LOAD=" & Val(CStr(mRefNo)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCheckCustNameStr = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
            mCheckBillNoStr = IIf(IsDbNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)

            Do While RsTemp.EOF = False
                mCheckCustName = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                If InStr(1, mCheckCustNameStr, mCheckCustName) = 0 Then
                    mCheckCustNameStr = mCheckCustNameStr & ", " & mCheckCustName
                    mPoint = mPoint + 1
                End If

                mCheckBillNo = IIf(IsDbNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)
                If InStr(1, mCheckBillNoStr, mCheckBillNo) = 0 Then
                    mCheckBillNoStr = mCheckBillNoStr & ", " & mCheckBillNo
                End If

                RsTemp.MoveNext()
            Loop
        End If

        mCustName = mCheckCustNameStr
        mBillNo = mCheckBillNoStr
        GetVehicleData = True
        Exit Function
ERR1:
        GetVehicleData = False
        MsgInformation(Err.Description)
    End Function
    Private Function GetStandardAmount(ByRef mLoadingNo As Double, ByRef mProductCode As String, ByRef mCustName As String, ByRef xRefType As String, ByRef xRefNo As Double, ByRef mShowType As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mRefType As String
        Dim mRefNo As String
        Dim mCustCode As String = ""
        Dim mRefDate As String
        Dim RsTrans As ADODB.Recordset = Nothing
        Dim mQty As Double
        Dim mStdRate As Double

        ''SELECT CLAUSE...

        GetStandardAmount = 0

        SqlStr = " SELECT DISTINCT ID.REF_TYPE, ID.REF_NO, ID.REF_DATE, ID.ITEM_CODE, ID.ITEM_UOM , ID.PACKED_QTY " & vbCrLf & " FROM DSP_LOADING_HDR IH, DSP_LOADING_DET ID " & vbCrLf & " WHERE IH.AUTO_KEY_LOAD=ID.AUTO_KEY_LOAD" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_LOAD=" & Val(CStr(mLoadingNo)) & "" & vbCrLf & " AND IS_TP_VEHICLE='N'"

        If Trim(xRefType) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.REF_TYPE='" & MainClass.AllowSingleQuote(xRefType) & "'"
        End If

        If Val(CStr(xRefNo)) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND ID.REF_NO=" & Val(CStr(xRefNo)) & ""
        End If

        If Trim(mProductCode) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"
        End If

        If Trim(mCustName) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mCustName) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mRefType = IIf(IsDbNull(RsTemp.Fields("REF_TYPE").Value), "", RsTemp.Fields("REF_TYPE").Value)
                mRefNo = IIf(IsDbNull(RsTemp.Fields("REF_NO").Value), -1, RsTemp.Fields("REF_NO").Value)
                mRefDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value), "DD-MMM-YYYY")
                If mShowType = "S" Then
                    mQty = IIf(IsDbNull(RsTemp.Fields("PACKED_QTY").Value), 0, RsTemp.Fields("PACKED_QTY").Value)
                Else
                    mQty = 1
                End If
                mStdRate = 0
                If mRefType = "R" Then
                    If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_PASSNO", "SUPP_CUST_CODE", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCustCode = MasterNo
                    End If
                ElseIf mRefType = "I" Then
                    If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_INVOICE", "SUPP_CUST_CODE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCustCode = MasterNo
                    End If
                Else
                    If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_MRR", "SUPP_CUST_CODE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCustCode = MasterNo
                    End If
                End If

                SqlStr = " SELECT TRANSPORT_RATE FROM PRD_CUST_TRANS_RATE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "' " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' "

                SqlStr = SqlStr & vbCrLf & " AND WEF_DATE = (" & vbCrLf & " SELECT MAX(WEF_DATE) AS WEF " & vbCrLf & " FROM PRD_CUST_TRANS_RATE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCustCode) & "' " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND WEF_DATE<=TO_DATE('" & VB6.Format(mRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTrans, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTrans.EOF = False Then
                    mStdRate = IIf(IsDbNull(RsTrans.Fields("TRANSPORT_RATE").Value), 0, RsTrans.Fields("TRANSPORT_RATE").Value)
                End If

                GetStandardAmount = GetStandardAmount + (mQty * mStdRate)
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ERR1:
        ''Resume
        GetStandardAmount = 0
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

        If chkVehicleAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtVehicleNo.Text) = "" Then
                MsgInformation("Invaild Vehicle No")
                txtVehicleNo.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkTransportAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtTransporter.Text) = "" Then
                MsgInformation("Invaild Transporter No")
                txtTransporter.Focus()
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
    Private Sub txtVehicleNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtVehicleNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.DoubleClick
        SearchVehicle()
    End Sub
    Private Sub txtVehicleNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVehicleNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicleNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicle()
    End Sub
    Private Sub txtVehicleNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicleNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtVehicleNo.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtVehicleNo.Text), "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtVehicleNo.Text = MasterNo
        Else
            MsgInformation("No Such Item in Vehicle Master")
            Cancel = True
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchVehicle_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchVehicle.Click
        SearchVehicle()
    End Sub

    Private Sub chkVehicleAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkVehicleAll.CheckStateChanged
        Call PrintStatus(False)
        If chkVehicleAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtVehicleNo.Enabled = False
            cmdSearchVehicle.Enabled = False
        Else
            txtVehicleNo.Enabled = True
            cmdSearchVehicle.Enabled = True
        End If
    End Sub

    Private Sub SearchVehicle()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtVehicleNo.Text, "FIN_VEHICLE_MST", "NAME", "TRANSPORTER_NAME", , , SqlStr)
        If AcName <> "" Then
            txtVehicleNo.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchTransport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchTransport.Click
        SearchTransport()
    End Sub

    Private Sub chkTransportAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTransportAll.CheckStateChanged
        Call PrintStatus(False)
        If chkTransportAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTransporter.Enabled = False
            cmdSearchTransport.Enabled = False
        Else
            txtTransporter.Enabled = True
            cmdSearchTransport.Enabled = True
        End If
    End Sub

    Private Sub SearchTransport()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtTransporter.Text, "FIN_TRANSPORTER_MST ", "TRANSPORTER_NAME", "TRANSPORTER_ID", , , SqlStr)
        If AcName <> "" Then
            txtTransporter.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub



End Class
