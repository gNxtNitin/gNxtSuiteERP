Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMiscGateEntryReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20


    Private Const ColLocked As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const ColGateNo As Short = 4
    Private Const ColGateDate As Short = 5
    Private Const colSupplierCode As Short = 6
    Private Const colSupplier As Short = 7
    Private Const ColBillNo As Short = 8
    Private Const ColBillDate As Short = 9
    Private Const ColVehicle As Short = 10
    Private Const ColTransporter As Short = 11
    Private Const ColItemCode As Short = 12
    Private Const ColItemDesc As Short = 13
    Private Const ColItemPart As Short = 14
    Private Const ColUnit As Short = 15
    Private Const ColLocCode As Short = 16
    Private Const ColQty As Short = 17
    Private Const ColRecdQty As Short = 18
    Private Const ColRate As Short = 19
    Private Const ColAmount As Short = 20
    Private Const ColMaterialOut As Short = 21
    Private Const ColMaterialOutDate As Short = 22
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
        txtTMFrom.Text = GetServerTime
        txtTMTo.Text = GetServerTime
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


        Report1.Reset()

        mTitle = "Gate Entry Register" & IIf(chkMRRMade.CheckState = System.Windows.Forms.CheckState.Checked, " (Pending List) ", "")

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\GateEntryReg.rpt"

        SqlStr = MakeSQL
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

    Private Sub frmParamMiscGateEntryReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Gate Entry Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamMiscGateEntryReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

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

        '    chkAll.Value = vbChecked
        '    TxtItemName.Enabled = False
        '    cmdsearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False




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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamMiscGateEntryReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamMiscGateEntryReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent

        Dim cntSearchRow As Integer
        Dim mSearchKey As String
        Dim mCol As Integer

        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            cntSearchRow = 1
            mSearchKey = ""
            mSearchKey = InputBox("Enter Search String :", "Search", mSearchKey)
            If mSearchKey <> "" Then
                MainClass.SearchIntoGrid(SprdMain, mCol, mSearchKey, cntSearchRow)
                cntSearchRow = cntSearchRow + 1
            End If
            SprdMain.Focus()
        End If
    End Sub
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xGateNo As Double
        'Dim xQCStatus As String
        Dim xGateDate As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColGateNo
        xGateNo = Val(SprdMain.Text)

        SprdMain.Col = ColGateDate
        xGateDate = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

        If FYChk(CStr(CDate(xGateDate))) = False Then
            Exit Sub
        End If


        '    SqlStr = "SELECT * from INV_GATE_HDR WHERE AUTO_KEY_MRR=" & xMRRNo & ""
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly

        '    If RsTemp.EOF = False Then
        FrmGateEntry.MdiParent = Me.MdiParent
        FrmGateEntry.Show()

        FrmGateEntry.FrmGateEntry_Activated(Nothing, New System.EventArgs())

        FrmGateEntry.txtMRRNo.Text = CStr(xGateNo)
        FrmGateEntry.TxtMRRNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

        '    End If
    End Sub


    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
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

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 9)

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 9)

            .Col = ColGateNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColGateNo, 9)

            .Col = ColGateDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColGateDate, 9)

            .Col = colSupplierCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplierCode, 8)

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, 20)

            .Col = ColVehicle
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVehicle, 12)

            .Col = ColTransporter
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTransporter, 12)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 10)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 25)

            .Col = ColItemPart
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemPart, 15)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            .Col = ColLocCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocCode, 7)

            For cntCol = ColQty To ColAmount
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

            .Col = ColMaterialOut
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMaterialOut, 9)

            .Col = ColMaterialOutDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMaterialOutDate, 9)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 9)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 9)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            '        SprdMain.OperationMode = OperationModeSingle
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
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

        SqlStr = MakeSQL
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
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mDivision As Double

        ''SELECT CLAUSE...


        MakeSQL = " SELECT ''," & vbCrLf _
            & " IGH.AUTO_KEY_NO, TO_CHAR(IGH.REF_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IGH.AUTO_KEY_GATE, TO_CHAR(IGH.GATE_DATE,'DD/MM/YYYY'), " & vbCrLf _
            & " CMST.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME," & vbCrLf _
            & " IGH.BILL_NO , IGH.BILL_DATE, " & vbCrLf _
            & " IGH.VEHICLE , IGH.TRANSPORT_MODE, " & vbCrLf _
            & " IGD.ITEM_CODE, IGD.ITEM_DESC, IMST.CUSTOMER_PART_NO," & vbCrLf _
            & " IGD.ITEM_UOM, IGD.LOC_CODE, TO_CHAR(IGD.BILL_QTY), TO_CHAR(IGD.RECEIVED_QTY), " & vbCrLf _
            & " TO_CHAR(IGD.ITEM_RATE),  TO_CHAR(IGD.ITEM_AMOUNT), DECODE(IS_OUT,'Y','YES','NO') AS IS_OUT, OUT_DATE, IGH.AUTO_KEY_NO"


        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM INV_MISC_GATE_HDR IGH, INV_MISC_GATE_DET IGD," & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IGH.AUTO_KEY_NO=IGD.AUTO_KEY_NO" & vbCrLf _
            & " AND IGH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IGH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IGH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
            & " AND IGD.ITEM_CODE=IMST.ITEM_CODE "

        '    If cboDivision.Text <> "ALL" Then
        '        If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '            mDivision = MasterNo
        '            MakeSQL = MakeSQL & vbCrLf & "AND IGH.DIV_CODE=" & mDivision & ""
        '        End If
        '    End If


        If chkMRRMade.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.GATE_ENTRY_MADE='N' "
        End If

        If optPending.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.IS_OUT='N' "
        ElseIf optOut.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.IS_OUT='Y' "
        End If

        If optOnlyReversed.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IGH.IS_REVERSED='Y' "
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IGH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        '    If chkTime.Value = vbChecked Then
        MakeSQL = MakeSQL & vbCrLf & " AND IGH.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        '    Else
        '        MakeSQL = MakeSQL & vbCrLf _
        ''                & " AND TO_CHAR(IGH.GATE_DATE,'YYYYMMDD')||TO_CHAR(IGH.ADDDATE,'HH24MI')>=TO_CHAR('" & VB6.Format(txtDateFrom.Text, "YYYYMMDD") & "') || '" & VB6.Format(txtTMFrom.Text, "HHMM") & "'" & vbCrLf _
        ''                & " AND TO_CHAR(IGH.GATE_DATE,'YYYYMMDD')||TO_CHAR(IGH.ADDDATE,'HH24MI')<=TO_CHAR('" & VB6.Format(txtDateTo.Text, "YYYYMMDD") & "') || '" & VB6.Format(txtTMTo.Text, "HHMM") & "'"
        '    End If

        'If OptOrderBy(0).Checked = True Then
        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IGH.REF_DATE, IGH.AUTO_KEY_NO, IGD.SERIAL_NO"
        'ElseIf OptOrderBy(1).Checked = True Then
        '    MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,INVMST.ITEM_SHORT_DESC,IGH.REF_DATE,IGH.AUTO_KEY_NO"
        'End If

        'End If
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
    Private Sub txtTMFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTMFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtTMTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTMTo.TextChanged
        Call PrintStatus(False)
    End Sub
End Class
