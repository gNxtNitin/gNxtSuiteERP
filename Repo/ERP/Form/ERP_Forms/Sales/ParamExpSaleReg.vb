Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamExpSaleReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Dim cntSearchRow As Integer

    Private Const ColLocked As Short = 1
    Private Const ColBillDate As Short = 2
    Private Const ColBillNo As Short = 3
    Private Const ColPartyCode As Short = 4
    Private Const ColPartyName As Short = 5
    Private Const ColExpBillNo As Short = 6
    Private Const ColExpBillDate As Short = 7
    Private Const ColQty As Short = 8
    Private Const ColFOB As Short = 9
    Private Const ColFreight As Short = 10
    Private Const ColCIF As Short = 11
    Private Const ColNetValue As Short = 12
    Private Const ColCurrency As Short = 13
    Private Const ColExchangeRate As Short = 14
    Private Const ColExchangeValue As Short = 15
    Private Const ColDutyForgone As Short = 16
    Private Const colRemarks As Short = 17
    Private Const ColPlace As Short = 18
    Private Const ColDescription As Short = 19
    Private Const ColContainerNo As Short = 20
    Private Const ColItemType As Short = 21
    Private Const ColShippingBillNo As Short = 22
    Private Const ColShippingBillDate As Short = 23
    Private Const ColAR1No As Short = 24
    Private Const ColAR1Date As Short = 25

    Private Const ColItemCode As Short = 26
    Private Const ColPartNo As Short = 27
    Private Const ColUOM As Short = 28
    Private Const ColItemDesc As Short = 29
    Private Const ColItemQty As Short = 30
    Private Const ColItemRate As Short = 31
    Private Const ColItemAmount As Short = 32
    Private Const ColCGST As Short = 33
    Private Const ColSGST As Short = 34
    Private Const ColIGST As Short = 35


    Private Const ColMKEY As Short = 36

    Dim mClickProcess As Boolean

    'Private Const ColTotValue = 8
    'Private Const ColNetValue = 9
    'Private Const ColExchangeRate = 10
    'Private Const ColExchangeValue = 11
    'Private Const ColMKEY = 12

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboAgtD3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCancelled_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboFOC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLocation.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRejection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejection.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSale(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSale(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForSale(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Export Sales Register"
        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SALESREGEXP.RPT"

        SqlStr = MakeSQL
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
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
    Private Sub frmParamExpSaleReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Export Sale Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamExpSaleReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        cboAgtD3.Items.Clear()
        cboFOC.Items.Clear()
        cboRejection.Items.Clear()
        cboCancelled.Items.Clear()
        cboCT3.Items.Clear()
        cboLocation.Items.Clear()

        cboAgtD3.Items.Add("BOTH")
        cboAgtD3.Items.Add("YES")
        cboAgtD3.Items.Add("NO")

        cboCT3.Items.Add("BOTH")
        cboCT3.Items.Add("YES")
        cboCT3.Items.Add("NO")

        cboFOC.Items.Add("BOTH")
        cboFOC.Items.Add("YES")
        cboFOC.Items.Add("NO")

        cboRejection.Items.Add("BOTH")
        cboRejection.Items.Add("YES")
        cboRejection.Items.Add("NO")

        cboCancelled.Items.Add("BOTH")
        cboCancelled.Items.Add("YES")
        cboCancelled.Items.Add("NO")

        cboAgtD3.SelectedIndex = 0
        cboCT3.SelectedIndex = 0
        cboFOC.SelectedIndex = 0
        cboRejection.SelectedIndex = 0
        cboCancelled.SelectedIndex = 2

        Call FillInvoiceType()

        '    MainClass.FillCombo cboInvoiceType, "FIN_INVTYPE_MST", "NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'"


        '    cboInvoiceType.ListIndex = 0
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamExpSaleReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamExpSaleReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
        '    lstInvoiceType.ToolTipText = lstInvoiceType.Text
    End Sub

    Private Sub lstInvoiceType_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lstInvoiceType.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        ToolTip1.SetToolTip(lstInvoiceType, lstInvoiceType.Text)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim mMKey As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStr1 As String
        Dim mStr2 As String
        Dim mStr As String
        'Dim cntSearchRow As Long
        'Dim mSearchKey As String
        '
        '    cntSearchRow = 1
        '    If eventArgs.row = 0 And eventArgs.col = ColBillNo Then
        '        mSearchKey = ""
        '        mSearchKey = InputBox("Enter Bill No :", "Search", mSearchKey)
        '        MainClass.SearchIntoGrid SprdMain, ColBillNo, mSearchKey, cntSearchRow
        '        cntSearchRow = cntSearchRow + 1
        '        SprdMain.SetFocus
        '    End If

        If eventArgs.row = 0 Then Exit Sub

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColMKEY
        mMKey = SprdMain.Text

        SqlStr = " SELECT VEHICLENO, CARRIERS " & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & mMKey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mStr1 = IIf(IsDbNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)
            mStr2 = IIf(IsDbNull(RsTemp.Fields("CARRIERS").Value), "", RsTemp.Fields("CARRIERS").Value)
            mStr1 = IIf(mStr1 = "", "", "Vehicle No : " & mStr1)
            mStr2 = IIf(mStr2 = "", "", "Carriers : " & mStr2)
            mStr = mStr1 & IIf(mStr2 = "", "", IIf(mStr1 = "", "", ",") & mStr2)

            ToolTip1.SetToolTip(SprdMain, mStr)
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent


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

    Private Sub SprdMain_RightClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles SprdMain.RightClick
        'Dim SqlStr As String=""=""
        'Dim mMkey As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mStr1 As String
        'Dim mStr2 As String
        'Dim mStr As String
        '
        '    SprdMain.Row = Row
        '    SprdMain.Col = ColMKEY
        '    mMkey = SprdMain.Text
        '
        '    SqlStr = " SELECT VEHICLENO, CARRIERS " & vbCrLf _
        ''            & " FROM FIN_INVOICE_HDR " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND FYEAR =" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        ''            & " AND MKEY='" & mMkey & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        mStr1 = IIf(IsNull(RsTemp!VEHICLENO), "", RsTemp!VEHICLENO)
        '        mStr2 = IIf(IsNull(RsTemp!CARRIERS), "", RsTemp!CARRIERS)
        '        mStr1 = IIf(mStr1 = "", "", "Vehicle No : " & mStr1)
        '        mStr2 = IIf(mStr2 = "", "", "Carriers : " & mStr2)
        '        mStr = mStr1 & IIf(mStr2 = "", "", "," & mStr2)
        '
        '        SprdMain.ToolTipText = mStr
        '    End If
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
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
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.25)
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

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 9)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 9)

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyCode, 6)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)
            .ColsFrozen = ColPartyName

            .Col = ColExpBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColExpBillDate, 9)

            .Col = ColExpBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColExpBillNo, 9)

            For cntCol = ColQty To ColNetValue
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                '            .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
            Next

            .Col = ColCurrency
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCurrency, 5)

            For cntCol = ColExchangeRate To ColDutyForgone
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                '            .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9)
            Next

            For cntCol = colRemarks To ColAR1Date
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(cntCol, 12)
            Next

            For cntCol = ColItemCode To ColItemDesc
                .Col = cntCol
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .ColHidden = IIf(optSummary.Checked = True, True, False)
                .set_ColWidth(ColItemCode, 9)
                .set_ColWidth(ColPartNo, 12)
                .set_ColWidth(ColUOM, 5)
                .set_ColWidth(ColItemDesc, 30)
            Next

            For cntCol = ColItemQty To ColIGST
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .ColHidden = IIf(optSummary.Checked = True, True, False)
                .set_ColWidth(cntCol, 9)
            Next


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            '        SprdMain.OperationMode = OperationModeNormal
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mMKey As String
        Dim mDutyForgone As Double


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColMKEY
                mMKey = Trim(.Text)
                mDutyForgone = 0

                SqlStr = "SELECT SUM(AMOUNT) AS AMOUNT FROM FIN_INVOICE_EXP WHERE MKEY='" & mMKey & "' AND DUTYFORGONE='Y'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    mDutyForgone = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                End If

                .Row = cntRow
                .Col = ColDutyForgone
                .Text = VB6.Format(mDutyForgone, "0.00")

            Next
        End With
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean
        ''SELECT CLAUSE...


        MakeSQL = " SELECT '', " & vbCrLf _
            & " IH.INVOICE_DATE, IH.BILLNO,  " & vbCrLf _
            & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " EXPBILLNO, EXPINV_DATE, " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTQTY))," & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE-ABS(IH.TOT_EXPORTEXP)))," & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,ABS(IH.TOT_EXPORTEXP)))," & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE))," & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETVALUE))," & vbCrLf _
            & " (SELECT CURR_DESC FROM FIN_EXPINV_HDR WHERE COMPANY_CODE=IH.COMPANY_CODE AND AUTO_KEY_PACK=IH.OUR_AUTO_KEY_SO) CURRENCYNAME," & vbCrLf _
            & " TO_CHAR(EXCHANGE_RATE), " & vbCrLf _
            & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTEXCHANGEVALUE))," & vbCrLf _
            & " '0.00'," & vbCrLf _
            & " DECODE(IH.AGTD3,'Y','AGST D3',DECODE(IH.NETVALUE,0,'FOC',''))," & vbCrLf _
            & " CMST.SUPP_CUST_CITY," & vbCrLf _
            & " IH.REMARKS," & vbCrLf _
            & " IH.VEHICLENO || ' ' || IH.CARRIERS, ITEMDESC,SHIPPING_NO ,SHIPPING_DATE ,ARE1_NO, ARE1_DATE, "

        ''SELECT CURR_DESC FROM FIN_EXPINV_HDR WHERE AUTO_KEY_PACK=OUR_AUTO_KEY_SO
        ''CMST.CURRENCYNAME

        If optSummary.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " '', '', '', '', 0, 0, 0, 0, 0, 0,"
        Else
            MakeSQL = MakeSQL & vbCrLf & " ID.ITEM_CODE, ID.CUSTOMER_PART_NO, ID.ITEM_UOM, IMST.ITEM_SHORT_DESC, ID.ITEM_QTY, ID.ITEM_RATE, ID.ITEM_AMT, ID.CGST_AMOUNT, ID.SGST_AMOUNT, ID.IGST_AMOUNT,"
        End If

        MakeSQL = MakeSQL & vbCrLf & " IH.MKEY"


        ''

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST INVMST"

        If optDetail.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " , FIN_INVOICE_DET ID, INV_ITEM_MST IMST"
        End If

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " And IH.TRNTYPE=INVMST.CODE"

        If optDetail.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf _
                & " And IH.MKEY=ID.MKEY" & vbCrLf _
                & " And ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                & " And ID.ITEM_CODE=IMST.ITEM_CODE"
        End If

        MakeSQL = MakeSQL & vbCrLf _
            & " And IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If

            MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        '    If cboInvoiceType.Text = "ALL" Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IDENTIFICATION<>'J'"
        '    Else
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.TRNTYPE=" & Val(lblTrnType.text) & ""
        '    End If

        mShowAll = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
                MakeSQL = MakeSQL & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
            End If
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            '        MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTD3='" & vb.Left(cboAgtD3.Text, 1) & "'"
            If cboAgtD3.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE ='S'"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE <>'S'"
            End If
        End If

        If cboCT3.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT3='" & VB.Left(cboCT3.Text, 1) & "'"
        End If

        If cboFOC.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.FOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboRejection.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboRejection.Text, 1) & "'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"

        If cboLocation.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.DESP_LOCATION='" & MainClass.AllowSingleQuote(cboLocation.Text) & "'"
        End If


        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.EXPINV_DATE,IH.EXPBILLNO "

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

        Dim mQty As Double
        Dim mFOB As Double
        Dim mFreight As Double
        Dim mCIF As Double
        Dim mNETVALUE As Double
        Dim mExchangeValue As Double
        Dim mDutyForgone As Double


        '.Col =  ColQty
        '.Col =  ColFOB
        '.Col =  ColFreight
        '.Col =  ColCIF
        '.Col =  ColNetValue
        '.Col =  ColExchangeValue
        '.Col =  ColDutyForgone

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColQty
                mQty = mQty + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColFOB
                mFOB = mFOB + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColFreight
                mFreight = mFreight + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCIF
                mCIF = mCIF + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColNetValue
                mNETVALUE = mNETVALUE + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColExchangeValue
                mExchangeValue = mExchangeValue + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColDutyForgone
                mDutyForgone = mDutyForgone + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColBillNo)
            .Col = ColPartyName
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows

            .Col = ColQty
            .Text = VB6.Format(mQty, "0.00")

            .Col = ColFOB
            .Text = VB6.Format(mFOB, "0.00")

            .Col = ColFreight
            .Text = VB6.Format(mFreight, "0.00")

            .Col = ColCIF
            .Text = VB6.Format(mCIF, "0.00")

            .Col = ColNetValue
            .Text = VB6.Format(mNETVALUE, "0.00")

            .Col = ColExchangeValue
            .Text = VB6.Format(mExchangeValue, "0.00")

            .Col = ColDutyForgone
            .Text = VB6.Format(mDutyForgone, "0.00")


        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub


    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMkey As String = ""
        Dim xVNo As String
        Dim xBookType As String = ""
        Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColBillDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColMKEY
        xMkey = Me.SprdMain.Text

        SprdMain.Col = ColBillNo
        xVNo = Me.SprdMain.Text

        Call ShowTrn(xMkey, xVDate, "", xVNo, "S", "", Me)

    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
    End Sub




    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' ORDER BY NAME"
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

        SqlStr = "SELECT DISTINCT DESP_LOCATION FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY DESP_LOCATION"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboLocation.Items.Clear()
        cboLocation.Items.Add("All")

        Do While RS.EOF = False
            cboLocation.Items.Add(IIf(IsDbNull(RS.Fields("DESP_LOCATION").Value), "", RS.Fields("DESP_LOCATION").Value))
            RS.MoveNext()
        Loop

        cboLocation.SelectedIndex = 0
        Exit Sub
FillErr2:
        MsgBox(Err.Description)
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

End Class
