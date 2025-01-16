Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmFARegister
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    '''Private PvtDBCn As ADODB.Connection					

    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColFYear As Short = 2
    Private Const ColBookType As Short = 3
    Private Const ColBookSubType As Short = 4
    Private Const ColCompanyName As Short = 5
    Private Const ColTrnName As Short = 6
    Private Const ColRefNo As Short = 7
    Private Const ColVNo As Short = 8
    Private Const ColVDate As Short = 9
    Private Const ColMRRNo As Short = 10
    Private Const ColMRRDate As Short = 11
    Private Const ColBillNo As Short = 12
    Private Const ColBillDate As Short = 13
    Private Const ColItemDesc As Short = 14
    Private Const ColPartyName As Short = 15
    Private Const ColBillAmount As Short = 16
    Private Const ColCustomDuty As Short = 17
    Private Const ColInstCharges As Short = 18
    Private Const ColModvatAmount As Short = 19
    Private Const ColSTRefundAmount As Short = 20
    Private Const ColGSTRefundAmount As Short = 21
    Private Const ColTotalCost As Short = 22
    Private Const ColSaleAmount As Short = 23
    Private Const ColSaleDate As Short = 24
    Private Const ColSaleAmount1 As Short = 25
    Private Const ColSaleDate1 As Short = 26
    Private Const ColSaleAmount2 As Short = 27
    Private Const ColSaleDate2 As Short = 28
    Private Const ColSaleAmount3 As Short = 29
    Private Const ColSaleDate3 As Short = 30
    Private Const ColSaleAmount4 As Short = 31
    Private Const ColSaleDate4 As Short = 32
    Private Const ColSaleAmount5 As Short = 33
    Private Const ColSaleDate5 As Short = 34
    Private Const ColItemType As Short = 35
    Private Const ColAssetType As Short = 36
    Private Const ColMKEY As Short = 37

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
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

    Private Sub chkAllDepr_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDepr.CheckStateChanged
        Call PrintStatus(False)
        If chkAllDepr.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDeprMode.Enabled = False
            cmdsearchDepr.Enabled = False
        Else
            txtDeprMode.Enabled = True
            cmdsearchDepr.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonFA(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonFA(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonFA(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim CntLst As Integer
        Dim mInvoiceType As String

        Report1.Reset()
        mTitle = "Fixed Assets Register (As Required Under The Companies Act, 1956)"
        '    For CntLst = 0 To lstInvoiceType.ListCount - 1					
        '        If lstInvoiceType.Selected(CntLst) = True Then					
        '            mInvoiceType = lstInvoiceType.List(CntLst)					
        '            Exit For					
        '        End If					
        '    Next					

        mSubTitle = mInvoiceType & " (" & Year(RsCompany.Fields("Start_Date").Value) & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY") & ")"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\FixedAssetsReg.RPT"

        SqlStr = MakeSQL()

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
        SearchAccounts()
    End Sub

    Private Sub cmdsearchDepr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchDepr.Click
        SearchDepr()
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
    Private Sub frmFARegister_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Fixed Assets Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmFARegister_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        Call FillInvoiceType()
        optType(2).Checked = True
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        txtDeprMode.Enabled = False
        cmdsearchDepr.Enabled = False

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmFARegister_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmFARegister_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub lstInvoiceType_Click()
        Call PrintStatus(False)
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
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

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        'Dim SqlStr As String					
        'Dim xVDate As String					
        'Dim xMkey As String					
        'Dim xVNo As String					
        'Dim xBookType As String					
        'Dim xBookSubType As String					
        'Dim pIndex As Long					
        'Dim xVTYPE As String					
        '					
        '   SprdMain.Row = SprdMain.ActiveRow					
        '					
        '    SprdMain.Col = ColVDate					
        '    xVDate = Me.SprdMain.Text					
        '					
        '    SprdMain.Col = ColMkey					
        '    xMkey = Me.SprdMain.Text					
        '					
        '    SprdMain.Col = ColVNo					
        '    xVNo = Me.SprdMain.Text					
        '					
        '    SprdMain.Col = ColBookType					
        '    xBookType = Me.SprdMain.Text					
        '					
        '    SprdMain.Col = ColBookSubType					
        '    xBookSubType = Me.SprdMain.Text					
        '					
        '    If xBookType = "B" Or xBookType = "F" Or xBookType = "C" Or xBookType = "J" Or xBookType = "R" Or xBookType = "E" Then					
        ''            xVTYPE = Mid(xVNo, 1, Len(xVNo) - 5)					
        ''            xVNo = Right(xVNo, 5)					
        '        SqlStr = "COMPANY_CODE=" & RsCompany!Company_Code & "" & vbCrLf _					
        ''                & " AND FYEAR=" & RsCompany!FYEAR & "" & vbCrLf _					
        ''                & " AND MKEY='" & xMkey & "'" & vbCrLf _					
        ''                & " AND BOOKTYPE='" & xBookType & "'" & vbCrLf _					
        ''                & " AND BOOKSUBTYPE='" & xBookSubType & "'" & vbCrLf _					
        ''                & " AND VDATE='" & vb6.Format(xVDate, "DD-MMM-YYYY") & "'"					
        '					
        '        If MainClass.ValidateWithMasterTable(xVNo, "VNO", "VTYPE", "FIN_POSTED_TRN", PubDBCn, MasterNo, , SqlStr) = True Then					
        '            xVTYPE = MasterNo					
        '            xVNo = Mid(xVNo, Len(xVTYPE) + 1)					
        '        Else					
        '            Exit Sub					
        '        End If					
        '    End If					
        '					
        '    Call ShowTrn(xMkey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType)					

    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True					
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub txtDateFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='F'"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr					
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  ,  , SqlStr)
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
        Dim SqlStr As String

        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='F'"

        'UPGRADE_WARNING: Untranslated statement in txtAccount_Validate. Please check source code.					
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
            .set_RowHeight(0, RowHeight * 1.5)
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

            .Col = ColFYear
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 6)
            .ColHidden = IIf(optShow(3).Checked = True, False, True)

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookType, 9)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBookSubType, 9)
            .ColHidden = True

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyName, 15)
            .ColHidden = IIf(optShow(2).Checked = True, True, False)

            .Col = ColTrnName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTrnName, 15)

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRDate, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMRRNo, 7)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVNo, 7)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 4000
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 15)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .ColsFrozen = IIf(optShow(0).Checked = True, ColBillNo, ColTrnName)

            For cntCol = ColBillAmount To ColTotalCost
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 10)
            Next

            .Col = ColSaleAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColSaleAmount, 10)

            .Col = ColSaleAmount1
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColSaleAmount1, 10)

            .Col = ColSaleAmount2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColSaleAmount2, 10)

            .Col = ColSaleAmount3
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColSaleAmount3, 10)

            .Col = ColSaleAmount4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColSaleAmount4, 10)

            .Col = ColSaleAmount5
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColSaleAmount5, 10)

            .Col = ColSaleDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSaleDate, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColSaleDate1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSaleDate1, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColSaleDate2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSaleDate2, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColSaleDate3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSaleDate3, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColSaleDate4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSaleDate4, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColSaleDate5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSaleDate5, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColItemType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemType, 12)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColAssetType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAssetType, 12)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With

        Call FillHeading()
    End Sub
    Private Function Show1() As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************					
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mGroupCode As String
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mAccountCode As String
        Dim mAllCheck As Boolean
        Dim mCompanyName As String
        Dim mCompanyCode As String


        If optShow(0).Checked = True Then
            SqlStr = " SELECT  '', TRN.FYEAR, TRN.BOOKTYPE, '1',  GEN.COMPANY_NAME, INVMST.NAME,TO_CHAR(TRN.AUTO_KEY_ASSET) AS AUTO_KEY_ASSET," & vbCrLf _
                & " TRN.PV_NO, TO_CHAR(TRN.PV_DATE,'DD/MM/YYYY'),  " & vbCrLf _
                & " TRN.MRR_NO, TRN.MRR_DATE, " & vbCrLf _
                & " TRN.BILL_NO, TRN.BILL_DATE, " & vbCrLf _
                & " TRN.ITEM_DESC, TRN.SUPP_CUST_NAME," & vbCrLf _
                & " TRN.TOTAL_COST-DN_CR_AMOUNT, TRN.CD_AMOUNT, TRN.OTH_AMOUNT, MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT, SALETAX_REFUND, " & vbCrLf _
                & " TRN.CGST_CLAIMAMOUNT + TRN.SGST_CLAIMAMOUNT + TRN.IGST_CLAIMAMOUNT," & vbCrLf _
                & " TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+TRN.CGST_CLAIMAMOUNT + TRN.SGST_CLAIMAMOUNT + TRN.IGST_CLAIMAMOUNT), " & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST ELSE 0 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN SALE_BILL_DATE END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE1>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE1<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST1 ELSE 0 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE1>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE1<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN SALE_BILL_DATE1 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE2>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE2<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST2 ELSE 0 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE2>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE2<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN SALE_BILL_DATE2 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE3>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE3<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST3 ELSE 0 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE3>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE3<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN SALE_BILL_DATE3 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE4>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE4<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST4 ELSE 0 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE4>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE4<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN SALE_BILL_DATE4 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE5>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE5<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST5 ELSE 0 END," & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE5>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE5<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN SALE_BILL_DATE5 END," & vbCrLf _
                & " TRN.ITEM_TYPE, TRN.AST_TYPE," & vbCrLf _
                & " TRN.AUTO_KEY_ASSET "
        Else
            If optShow(1).Checked = True Then
                SqlStr = " SELECT  '', '','', '1', GEN.COMPANY_NAME,"
            ElseIf optShow(2).Checked = True Then
                SqlStr = " SELECT  '', '','', '1', '',"
            Else
                SqlStr = " SELECT  '', TRN.FYEAR,'', '1', GEN.COMPANY_NAME,"
            End If

            SqlStr = SqlStr & vbCrLf _
                & " INVMST.NAME, ''," & vbCrLf _
                & " '', '',  " & vbCrLf _
                & " '', '', " & vbCrLf _
                & " '', '', " & vbCrLf _
                & " '', ''," & vbCrLf _
                & " SUM(TRN.TOTAL_COST-DN_CR_AMOUNT), SUM(TRN.CD_AMOUNT), SUM(TRN.OTH_AMOUNT), SUM(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT), SUM(SALETAX_REFUND), SUM(TRN.CGST_CLAIMAMOUNT + TRN.SGST_CLAIMAMOUNT + TRN.IGST_CLAIMAMOUNT)," & vbCrLf _
                & " SUM(TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+TRN.CGST_CLAIMAMOUNT + TRN.SGST_CLAIMAMOUNT + TRN.IGST_CLAIMAMOUNT)), " & vbCrLf _
                & " SUM(CASE WHEN SALE_BILL_DATE>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST ELSE 0 END+" & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE1>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE1<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST1 ELSE 0 END+" & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE2>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE2<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST2 ELSE 0 END+" & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE3>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE3<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST3 ELSE 0 END+" & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE4>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE4<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST4 ELSE 0 END+" & vbCrLf _
                & " CASE WHEN SALE_BILL_DATE5>= TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE5<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN ORIGINAL_COST5 ELSE 0 END)," & vbCrLf _
                & " '','','','','','',''," & vbCrLf _
                & " '', ''," & vbCrLf _
                & " '' "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM AST_ASSET_TRN TRN, FIN_INVTYPE_MST INVMST, GEN_COMPANY_MST GEN"

        ''''WHERE CLAUSE...					
        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND TRN.GROUP_CODE=INVMST.CODE"

        If cboCompany.SelectedIndex > 0 Then
            mCompanyName = Trim(cboCompany.Text)
            '        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then					
            '           mCompanyCode = IIf(IsNull(MasterNo), "", MasterNo)					
            '        End If					
            SqlStr = SqlStr & vbCrLf & " AND GEN.COMPANY_NAME='" & MainClass.AllowSingleQuote(mCompanyName) & "'"
        End If

        '   mAllCheck = True					
        '    For CntLst = 0 To lstInvoiceType.ListCount - 1					
        '        If lstInvoiceType.Selected(CntLst) = True Then					
        '            mInvoiceType = lstInvoiceType.List(CntLst)					
        '             If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = True Then					
        '               mTrnCode = IIf(IsNull(MasterNo), "", MasterNo)					
        '            End If					
        '            mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)					
        '        Else					
        '            mAllCheck = False					
        '        End If					
        '    Next					
        '					
        '    If mAllCheck = False Then					
        '        If mTrnTypeStr <> "" Then					
        '            mTrnTypeStr = "(" & mTrnTypeStr & ")"					
        '            SqlStr = SqlStr & vbCrLf & " AND GROUP_CODE IN " & mTrnTypeStr & ""					
        '        End If					
        '    End If					

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If

            'If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
            '    mGroupCode = IIf(IsDBNull(MasterNo), "", MasterNo)
            'End If
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ACCOUNTPOSTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If optOption(1).Checked = True Then
            '        SqlStr = SqlStr & vbCrLf & "AND ORIGINAL_COST+ORIGINAL_COST1+ORIGINAL_COST2+ORIGINAL_COST3=0"					
        ElseIf optOption(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND ORIGINAL_COST+ORIGINAL_COST1+ORIGINAL_COST2+ORIGINAL_COST3<>0"
        End If

        SqlStr = SqlStr & vbCrLf & "AND CANCELLED='N'"

        If optOption(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND (SALE_BILL_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " OR (SALE_BILL_DATE1 BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            SqlStr = SqlStr & vbCrLf & " OR (SALE_BILL_DATE2 BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            SqlStr = SqlStr & vbCrLf & " OR (SALE_BILL_DATE3 BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            SqlStr = SqlStr & vbCrLf & " OR (SALE_BILL_DATE4 BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            SqlStr = SqlStr & vbCrLf & " OR (SALE_BILL_DATE5 BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"
        Else
            SqlStr = SqlStr & vbCrLf & " AND PV_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY GEN.COMPANY_NAME,INVMST.NAME"
        ElseIf optShow(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY INVMST.NAME"
        ElseIf optShow(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " GROUP BY GEN.COMPANY_NAME,INVMST.NAME,TRN.FYEAR"
        End If

        If optShow(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY INVMST.NAME"
        ElseIf optShow(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.FYEAR,GEN.COMPANY_NAME,INVMST.NAME"
        ElseIf optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY GEN.COMPANY_NAME,INVMST.NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY GEN.COMPANY_NAME,INVMST.NAME,TRN.FYEAR,TRN.PV_DATE,TRN.PV_NO"
        End If

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function


    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim mTrnTypeSelect As Boolean
        Dim CntLst As Integer
        Dim mInvoiceType As String

        'UPGRADE_WARNING: Untranslated statement in FieldsVerification. Please check source code.					
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus					
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus					

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        '    mTrnTypeSelect = False					
        '    For CntLst = 0 To lstInvoiceType.ListCount - 1					
        '        If lstInvoiceType.Selected(CntLst) = True Then					
        '            mInvoiceType = lstInvoiceType.List(CntLst)					
        '             If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = True Then					
        '               mTrnTypeSelect = True					
        '               Exit For					
        '            End If					
        '        End If					
        '    Next					
        '					
        '    If mTrnTypeSelect = False Then					
        '        MsgInformation "Nothing to show"					
        '        FieldsVerification = False					
        '        Exit Function					
        '    End If					

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()
        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mBillAmount As Double
        Dim mCustomDuty As Double
        Dim mOtherCharges As Double
        Dim mModvatAmount As Double
        Dim mTotalCost As Double
        Dim mBookSubType As String
        Dim mSTRefund As Double
        Dim mGSTRefund As Double
        Dim mSaleAmount As Double
        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim mSaleAmount5 As Double
        If optShow(0).Checked = True Then
            Call CalcSprdSubTotal()
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColBookSubType
                mBookSubType = Trim(.Text)

                If mBookSubType = "1" Then
                    .Col = ColBillAmount
                    mBillAmount = mBillAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColCustomDuty
                    mCustomDuty = mCustomDuty + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColInstCharges
                    mOtherCharges = mOtherCharges + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColModvatAmount
                    mModvatAmount = mModvatAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSTRefundAmount
                    mSTRefund = mSTRefund + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColGSTRefundAmount
                    mGSTRefund = mGSTRefund + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColTotalCost
                    mTotalCost = mTotalCost + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSaleAmount
                    mSaleAmount = mSaleAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSaleAmount1
                    mSaleAmount1 = mSaleAmount1 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSaleAmount2
                    mSaleAmount2 = mSaleAmount2 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSaleAmount3
                    mSaleAmount3 = mSaleAmount3 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSaleAmount4
                    mSaleAmount4 = mSaleAmount4 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSaleAmount5
                    mSaleAmount5 = mSaleAmount5 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                End If
            Next

            '        Call MainClass.AddBlankfpSprdRow(SprdMain, ColRefNo)					
            .MaxRows = .MaxRows + 1
            .Row = .MaxRows
            .Action = SS_ACTION_INSERT_ROW

            .Col = ColPartyName
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

            .Row = .MaxRows

            .Col = ColBillAmount
            .Text = VB6.Format(mBillAmount, "0.00")

            .Col = ColCustomDuty
            .Text = VB6.Format(mCustomDuty, "0.00")

            .Col = ColInstCharges
            .Text = VB6.Format(mOtherCharges, "0.00")

            .Col = ColTotalCost
            .Text = VB6.Format(mTotalCost, "0.00")

            .Col = ColModvatAmount
            .Text = VB6.Format(mModvatAmount, "0.00")

            .Col = ColSTRefundAmount
            .Text = VB6.Format(mSTRefund, "0.00")

            .Col = ColGSTRefundAmount
            .Text = VB6.Format(mGSTRefund, "0.00")

            .Col = ColSaleAmount
            .Text = VB6.Format(mSaleAmount, "0.00")

            .Col = ColSaleAmount1
            .Text = VB6.Format(mSaleAmount1, "0.00")

            .Col = ColSaleAmount2
            .Text = VB6.Format(mSaleAmount2, "0.00")

            .Col = ColSaleAmount3
            .Text = VB6.Format(mSaleAmount3, "0.00")

            .Col = ColSaleAmount4
            .Text = VB6.Format(mSaleAmount4, "0.00")

            .Col = ColSaleAmount5
            .Text = VB6.Format(mSaleAmount5, "0.00")

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CalcSprdSubTotal()
        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mBillAmount As Double
        Dim mCustomDuty As Double
        Dim mOtherCharges As Double
        Dim mModvatAmount As Double
        Dim mTotalCost As Double
        Dim mTRNType As String
        Dim mCurrentType As String
        Dim mFromRow As Integer
        Dim mToRow As Integer
        Dim mLastRow As Boolean

        mTRNType = ""
        cntRow = 1
        mFromRow = 1
        mLastRow = False
        With SprdMain
            '        For cntRow = 1 To .MaxRows					
            Do While cntRow <= .MaxRows
                .Row = cntRow

                .Col = ColTrnName
                mCurrentType = Trim(.Text)
                If (mTRNType <> mCurrentType And cntRow > 1) Or cntRow = .MaxRows Then

                    If cntRow = .MaxRows Then
                        mLastRow = True
                    End If

                    mToRow = cntRow - IIf(mLastRow = True, 0, 1)

                    .MaxRows = .MaxRows + 1
                    .Row = IIf(mLastRow = True, .MaxRows, cntRow)
                    .Action = SS_ACTION_INSERT_ROW

                    .Col = ColPartyName
                    .Row = IIf(mLastRow = True, .MaxRows, cntRow)
                    .Text = "SUB TOTAL :"
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Row = IIf(mLastRow = True, .MaxRows, cntRow)
                    .Row2 = IIf(mLastRow = True, .MaxRows, cntRow)
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80					
                    .BlockMode = False

                    Call CalcRowTotal(SprdMain, ColBillAmount, mFromRow, ColBillAmount, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColBillAmount)
                    Call CalcRowTotal(SprdMain, ColCustomDuty, mFromRow, ColCustomDuty, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColCustomDuty)
                    Call CalcRowTotal(SprdMain, ColInstCharges, mFromRow, ColInstCharges, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColInstCharges)
                    Call CalcRowTotal(SprdMain, ColModvatAmount, mFromRow, ColModvatAmount, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColModvatAmount)
                    Call CalcRowTotal(SprdMain, ColSTRefundAmount, mFromRow, ColSTRefundAmount, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColSTRefundAmount)
                    Call CalcRowTotal(SprdMain, ColGSTRefundAmount, mFromRow, ColGSTRefundAmount, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColGSTRefundAmount)
                    Call CalcRowTotal(SprdMain, ColTotalCost, mFromRow, ColTotalCost, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColTotalCost)

                    Call CalcRowTotal(SprdMain, ColSaleAmount, mFromRow, ColSaleAmount, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColSaleAmount)
                    Call CalcRowTotal(SprdMain, ColSaleAmount1, mFromRow, ColSaleAmount1, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColSaleAmount1)
                    Call CalcRowTotal(SprdMain, ColSaleAmount2, mFromRow, ColSaleAmount2, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColSaleAmount2)
                    Call CalcRowTotal(SprdMain, ColSaleAmount3, mFromRow, ColSaleAmount3, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColSaleAmount3)

                    Call CalcRowTotal(SprdMain, ColSaleAmount4, mFromRow, ColSaleAmount4, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColSaleAmount4)
                    Call CalcRowTotal(SprdMain, ColSaleAmount5, mFromRow, ColSaleAmount5, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColSaleAmount5)

                    mFromRow = cntRow + 1
                    If mLastRow = True Then Exit Do
                End If
                mTRNType = mCurrentType
                cntRow = cntRow + 1
            Loop

            '        Next					

            '    With SprdMain					
            '        For cntRow = 1 To .MaxRows					
            '            .Row = cntRow					
            '					
            '            .Col = ColBillAmount					
            '            mBillAmount = mBillAmount + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))					
            '					
            '            .Col = ColCustomDuty					
            '            mCustomDuty = mCustomDuty + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))					
            '					
            '            .Col = ColInstCharges					
            '            mOtherCharges = mOtherCharges + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))					
            '					
            '            .Col = ColModvatAmount					
            '            mModvatAmount = mModvatAmount + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))					
            '					
            '            .Col = ColTotalCost					
            '            mTotalCost = mTotalCost + Val(CDbl(IIf(IsNumeric(.Text), .Text, 0)))					
            '        Next					
            '					
            '        Call MainClass.AddBlankfpSprdRow(SprdMain, ColRefNo)					
            '        .Col = ColPartyName					
            '        .Row = .MaxRows					
            '        .Text = "SUB TOTAL :"					
            '        .FontBold = True					
            '					
            '        .Row = .MaxRows					
            '        .Row2 = .MaxRows					
            '        .Col = 1					
            '        .col2 = .MaxCols					
            '        .BlockMode = True					
            '        .BackColor = &H8000000F     ''&H80FF80					
            '        .BlockMode = False					
            '					
            '        .Row = .MaxRows					
            '					
            '        .Col = ColBillAmount					
            '        .Text = Format(mBillAmount, "0.00")					
            '					
            '        .Col = ColCustomDuty					
            '        .Text = Format(mCustomDuty, "0.00")					
            '					
            '        .Col = ColInstCharges					
            '        .Text = Format(mOtherCharges, "0.00")					
            '					
            '        .Col = ColTotalCost					
            '        .Text = Format(mTotalCost, "0.00")					
            '					
            '        .Col = ColModvatAmount					
            '        .Text = Format(mModvatAmount, "0.00")					

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillInvoiceType()
        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer

        '    lstInvoiceType.Clear					
        '    SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST " & vbCrLf _					
        ''        & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _					
        ''        & " AND SUPP_CUST_TYPE='F' ORDER BY SUPP_CUST_NAME"					
        '					
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RS, adLockReadOnly					
        '					
        '    CntLst = 0					
        '    If RS.EOF = False Then					
        '        Do While RS.EOF = False					
        '            lstInvoiceType.AddItem RS!SUPP_CUST_NAME					
        '            lstInvoiceType.Selected(CntLst) = True					
        '            RS.MoveNext					
        '            CntLst = CntLst + 1					
        '        Loop					
        '    End If					
        '					
        '    lstInvoiceType.ListIndex = 0					

        cboCompany.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboCompany.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboCompany.Items.Add(RS.Fields("Company_Name").Value)
                RS.MoveNext()
            Loop
        End If

        '    cboCompany.ListIndex = 1					
        cboCompany.Text = RsCompany.Fields("Company_Name").Value
        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'UPGRADE_WARNING: Untranslated statement in txtdateFrom_Validate. Please check source code.					
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then					
        '        txtDateFrom.SetFocus					
        '        Cancel = True					
        '        Exit Sub					
        '    End If					
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'UPGRADE_WARNING: Untranslated statement in txtdateTo_Validate. Please check source code.					
        '    If FYChk(CDate(txtDateTo.Text)) = False Then					
        '        txtDateTo.SetFocus					
        '        Cancel = True					
        '        Exit Sub					
        '    End If					
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillHeading()
        On Error GoTo ErrPart
        Dim mDate As String

        With SprdMain

            .Row = 0



            .Col = ColLocked
            .Text = "Locked"

            .Col = ColFYear
            .Text = "FYear"

            .Col = ColBookType
            .Text = "Book Type"

            .Col = ColBookSubType
            .Text = "Book Sub Type"

            .Col = ColTrnName
            .Text = "Assets Category"

            .Col = ColRefNo
            .Text = "Ref No"

            .Col = ColVNo
            .Text = "VR. No."

            .Col = ColVDate
            .Text = "VR. Date"

            .Col = ColMRRNo
            .Text = "MRR No."

            .Col = ColMRRDate
            .Text = "MRR Date"

            .Col = ColBillNo
            .Text = "Bill No."

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColItemDesc
            .Text = "Brief Description of Asset & Identification No., If Any"

            .Col = ColPartyName
            .Text = "Supplier's Name"

            .Col = ColBillAmount
            .Text = "Invoice Value"

            .Col = ColCustomDuty
            .Text = "Custom Duty, Insurance & Freight"

            .Col = ColInstCharges
            .Text = "Other Charges etc."

            .Col = ColModvatAmount
            .Text = "Modvat Amount"

            .Col = ColSTRefundAmount
            .Text = "Sales Tax Refund Amount"

            .Col = ColGSTRefundAmount
            .Text = "GST Refund Amount"

            .Col = ColTotalCost
            .Text = "Total Cost"

            .Col = ColSaleAmount
            .Text = "Sale Amount 1"

            .Col = ColSaleDate
            .Text = "Sale Date 1"

            .Col = ColSaleAmount1
            .Text = "Sale Amount 2"

            .Col = ColSaleDate1
            .Text = "Sale Date 2"

            .Col = ColSaleAmount2
            .Text = "Sale Amount 3"

            .Col = ColSaleDate2
            .Text = "Sale Date 3"

            .Col = ColSaleAmount3
            .Text = "Sale Amount 4"

            .Col = ColSaleDate3
            .Text = "Sale Date 4"

            .Col = ColSaleAmount4
            .Text = "Sale Amount 5"

            .Col = ColSaleDate4
            .Text = "Sale Date 5"

            .Col = ColSaleAmount5
            .Text = "Sale Amount 6"

            .Col = ColSaleDate5
            .Text = "Sale Date 6"

            .Col = ColItemType
            .Text = "Item Type"

            .Col = ColAssetType
            .Text = "Assets Type"

            .Col = ColMKEY
            .Text = "MKey"

        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub
    Private Sub txtDeprMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeprMode.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDeprMode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeprMode.DoubleClick
        SearchDepr()
    End Sub

    Private Sub SearchDepr()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        ''MainClass.SearchMaster txtDeprMode, "AST_DEPRECIATION_MODE_MST", "NAME", SqlStr					
        MainClass.SearchGridMaster(txtDeprMode.Text, "AST_DEPRECIATION_MODE_MST", "MODE_DESC", "MODE_CODE", "MODE_TYPE",  , SqlStr)
        If AcName <> "" Then
            txtDeprMode.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDeprMode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeprMode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeprMode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDeprMode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeprMode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchDepr()
    End Sub

    Private Sub txtDeprMode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeprMode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String

        If txtDeprMode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'UPGRADE_WARNING: Untranslated statement in txtDeprMode_Validate. Please check source code.					
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
