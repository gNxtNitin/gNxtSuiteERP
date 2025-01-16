Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmFADespRegisterUseFull
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
    Private Const ColPartyName As Short = 7
    Private Const ColItemDesc As Short = 8
    Private Const ColRefNo As Short = 9
    Private Const ColLocation As Short = 10
    Private Const ColVDate As Short = 11
    Private Const ColPutDate As Short = 12
    Private Const ColTotalCost As Short = 13
    Private Const ColWDV As Short = 14
    Private Const ColDays As Short = 15
    Private Const ColPurchaseYear As Short = 16
    Private Const ColDeprec1 As Short = 17
    Private Const ColCumulativeDeprec As Short = 18
    Private Const ColSaleAmount As Short = 19
    Private Const ColSaleDate As Short = 20
    Private Const ColTotalDeprecClaim As Short = 21
    Private Const ColGV_WrittenOff = 22
    Private Const ColDesp_WrittenOff = 23
    Private Const ColScrapValue = 24

    Private Const ColPhyDate = 25
    Private Const ColPhyWhom = 26
    Private Const ColGrossBlock = 27
    Private Const ColNetBlock = 28
    Private Const ColSelvageAmount = 29
    Private Const ColItemType = 30
    Private Const ColAssetType = 31
    Private Const ColMKEY = 32

    Private Const ConsStartDate As String = "01/04/2014"

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

    Private Sub chkRefNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRefNo.CheckStateChanged
        Call PrintStatus(False)
        If chkRefNo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtRefNo.Enabled = False
            cmdRefNo.Enabled = False
        Else
            txtRefNo.Enabled = True
            cmdRefNo.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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

    Private Sub cmdRefNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefNo.Click
        SearchRefNo()
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdsearchDepr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchDepr.Click
        SearchDepr()
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If InsertDataInTempFile() = False Then GoTo ErrPart
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4					
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmFADespRegisterUseFull_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Fixed Assets Depreciation Register - Usefull"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmFADespRegisterUseFull_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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

        chkRefNo.CheckState = System.Windows.Forms.CheckState.Checked
        txtRefNo.Enabled = False
        cmdRefNo.Enabled = False

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmFADespRegisterUseFull_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmFADespRegisterUseFull_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
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
            .ColHidden = True ' IIf(optShow(1).Value = True, False, True)					

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
            .ColHidden = IIf(optShow(1).Checked = True, True, False)

            .Col = ColTrnName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColTrnName, 15)

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

            .ColsFrozen = IIf(optShow(0).Checked = True, ColRefNo, ColTrnName)

            .Col = ColRefNo
            '        .CellType = SS_CELL_TYPE_EDIT					
            '        .TypeHAlign = SS_CELL_H_ALIGN_LEFT					
            '        .TypeEditLen = 255					
            '        .TypeEditMultiLine = True					
            '        .ColWidth(ColRefNo) = 9					
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColVDate, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPutDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPutDate, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocation, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColWDV
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColWDV, 10)

            .Col = ColTotalCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColTotalCost, 10)

            .Col = ColDays
            .TypeNumberMin = CDbl("-99999999999")
            .TypeNumberMax = CDbl("99999999999")
            .TypeNumberSeparator = CStr(False)
            .TypeNumberDecPlaces = 0
            .CellType = SS_CELL_TYPE_INTEGER

            .set_ColWidth(ColDays, 6)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPurchaseYear
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMin = CDbl("-99999999999")
            .TypeNumberMax = CDbl("99999999999")
            .TypeNumberSeparator = CStr(False)
            .TypeNumberDecPlaces = 0
            .set_ColWidth(ColPurchaseYear, 6)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            For cntCol = ColDeprec1 To ColSaleAmount
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

            For cntCol = ColTotalDeprecClaim To ColTotalDeprecClaim
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

            For cntCol = ColGrossBlock To ColSelvageAmount
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

            .Col = ColSaleDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSaleDate, 9)
            .ColHidden = IIf(optShow(0).Checked = True, False, True)

            .Col = ColPhyDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPhyDate, 9)
            '        .ColHidden = IIf(optShow(0).Value = True, False, True)					
            .ColHidden = True

            .Col = ColPhyWhom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPhyWhom, 9)
            '        .ColHidden = IIf(optShow(0).Value = True, False, True)					
            .ColHidden = True

            .Col = ColItemType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemType, 12)
            '        .ColHidden = IIf(optShow(0).Value = True, False, True)					
            .ColHidden = True

            .Col = ColAssetType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAssetType, 12)
            '        .ColHidden = IIf(optShow(0).Value = True, False, True)					
            .ColHidden = True

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

    End Sub
    Private Function UpdateTempTable() As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mDeprecAsOn As String
        Dim mCompanyCode As Integer
        Dim mDepAmount As Double
        Dim mOPCummDepAmount2013 As Double
        Dim mCummDepAmount As Double
        Dim mDepreMode As String
        Dim mRefNo As Double
        Dim pSaleDesp As Double
        Dim mSaleAmount As Double
        Dim mSaleDate As String
        Dim mGrossBlock As String
        Dim pOPGrossBlock As Double
        Dim pOPNetBlock As Double
        Dim pDays As Integer
        Dim mAddDays As Integer
        Dim mActAddDays As Integer
        Dim pNetGrossBlock As Double
        Dim RsTemp As ADODB.Recordset
        Dim mSqlStr As String
        Dim xLastYearSaleDesp As Double
        Dim mSalvageAmount As Double
        Dim GetSalvageAmountOn As Double
        Dim mPutDate As String
        Dim mPurchaseAmount As Double
        Dim mCalcDays As Integer

        UpdateTempTable = False

        mCalcDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(txtDepreciationDate.Text)) + 1
        mCalcDays = IIf(mCalcDays > 365, 365 + IIf(RsCompany.Fields("COMPANY_CODE").Value = 27, 1, 0), mCalcDays)



        If chkAllDepr.CheckState = System.Windows.Forms.CheckState.Checked Then
            mDepreMode = "SLM1"
        Else
            mDepreMode = Trim(txtDeprMode.Text)
        End If

        SqlStr = "SELECT * FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mDeprecAsOn = VB6.Format(txtDepreciationDate.Text, "DD/MM/YYYY")
        cntRow = 0
        lblCount.Text = CStr(cntRow)
        System.Windows.Forms.Application.DoEvents()
        Do While RsTemp.EOF = False
            '        GoTo NextRec					
            GetSalvageAmountOn = 0
            mRefNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_ASSET").Value), 0, RsTemp.Fields("AUTO_KEY_ASSET").Value)
            mCompanyCode = IIf(IsDBNull(RsTemp.Fields("COMPANY_CODE").Value), 0, RsTemp.Fields("COMPANY_CODE").Value)

            mPutDate = ""
            If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_ASSET", "PUT_DATE", "AST_ASSET_TRN", PubDBCn, MasterNo,  , "COMPANY_CODE=" & mCompanyCode & "") = True Then
                mPutDate = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If

            mActAddDays = GetDays(mRefNo, mCompanyCode)

            pOPGrossBlock = 0
            mSaleDate = ""
            pSaleDesp = 0
            xLastYearSaleDesp = 0
            mGrossBlock = CStr(0)
            mPurchaseAmount = 0

            pOPNetBlock = GetOpeningNetBlock(mRefNo, mCompanyCode, GetSalvageAmountOn, pOPGrossBlock, mPurchaseAmount)
            mSaleAmount = GetSaleAmount(mRefNo, mCompanyCode, pOPNetBlock, mSaleDate, pSaleDesp, xLastYearSaleDesp, GetSalvageAmountOn)
            GetSalvageAmountOn = GetSalvageAmountOn - CDbl(VB6.Format(xLastYearSaleDesp, CStr(0)))
            mSalvageAmount = GetSalvageAmountOn * 0     '' 0.05 Sandeep 20/03/2024

            If pOPNetBlock <= 0 Then
                mDepAmount = 0
                mSalvageAmount = 0
            ElseIf mSalvageAmount > pOPNetBlock Then
                mDepAmount = 0
                mSalvageAmount = pOPNetBlock
            Else
                mDepAmount = GetDespAmount(mRefNo, mCompanyCode, pOPNetBlock)
                If pOPNetBlock = mSaleAmount Then
                    mSalvageAmount = 0
                Else
                    If mPutDate <> "" Then
                        If mActAddDays < mCalcDays And CDate(mPutDate) < CDate(lblFYStartDate.Text) Then
                            If (pOPNetBlock - mSaleAmount) < mSalvageAmount Then
                                mDepAmount = IIf(mDepAmount > mSalvageAmount, mDepAmount - mSalvageAmount, mDepAmount)
                            End If
                            '                        ''08/08/2016					
                            '                        If mDepAmount > mSalvageAmount Then					
                            '                            mDepAmount = mDepAmount - mSalvageAmount					
                            '                        End If					
                        End If
                    End If
                End If
            End If

            mOPCummDepAmount2013 = GetOPCummulativeDesp(mRefNo, mCompanyCode, "Y") ''+ mDepAmount					

            mCummDepAmount = GetOPCummulativeDesp(mRefNo, mCompanyCode, "N") ''+ mDepAmount					

            ''08/08/2016					
            '        If CDate(lblFYEndDate.Caption) > CDate("31/03/2015") Then					
            If mDepAmount <> 0 Then
                If mCummDepAmount + mDepAmount <> 0 Then ''22/08/2016					
                    If mPurchaseAmount < mCummDepAmount Then
                        mCummDepAmount = mPurchaseAmount
                        mDepAmount = 0
                    ElseIf mPurchaseAmount < mCummDepAmount + mDepAmount Then
                        mDepAmount = mPurchaseAmount - mCummDepAmount
                    End If
                    '        End If					
                End If
            End If

            mCummDepAmount = mCummDepAmount + mDepAmount

            '        mCummDepAmount = IIf(mCummDepAmount < 0, 0, mCummDepAmount) ''19/08/2016					
            '        If CDate(lblFYEndDate.Caption) > CDate("31/03/2015") Then					
            If mCummDepAmount < pSaleDesp And pSaleDesp <> 0 Then
                If mCummDepAmount >= 0 Then ''17/08/2016					
                    pSaleDesp = mCummDepAmount
                End If
            End If
            '        End If					

            mGrossBlock = CStr(pOPNetBlock - mSaleAmount)
            If pOPNetBlock = 0 Or CDbl(mGrossBlock) = 0 Then
                pNetGrossBlock = 0
            Else
                pNetGrossBlock = CDbl(mGrossBlock) - (mCummDepAmount - mOPCummDepAmount2013) + (pSaleDesp - xLastYearSaleDesp) ''08/06/2016 mGrossBlock - mDepAmount + (pSaleDesp - xLastYearSaleDesp)					
            End If
            pNetGrossBlock = System.Math.Round(pNetGrossBlock)

            If mDepAmount <> 0 Then
                If pNetGrossBlock < mSalvageAmount And pNetGrossBlock > 0 Then
                    mDepAmount = mDepAmount - (mSalvageAmount - pNetGrossBlock)
                    mCummDepAmount = mCummDepAmount - (mSalvageAmount - pNetGrossBlock)
                    pNetGrossBlock = mSalvageAmount
                ElseIf pNetGrossBlock < mSalvageAmount And CDbl(mGrossBlock) > 0 And CDate(lblFYEndDate.Text) > CDate("01/04/2015") Then  ''02/08/2016					
                    If mDepAmount - (mSalvageAmount - pNetGrossBlock) > 0 Then
                        mDepAmount = mDepAmount - (mSalvageAmount - pNetGrossBlock)
                        mCummDepAmount = mCummDepAmount - (mSalvageAmount - pNetGrossBlock)
                        pNetGrossBlock = mSalvageAmount
                    End If
                End If
            End If

            ''Rounding Off					
            mDepAmount = System.Math.Round(mDepAmount, 0)
            mCummDepAmount = System.Math.Round(mCummDepAmount, 0)
            pSaleDesp = System.Math.Round(pSaleDesp, 0)
            mGrossBlock = CStr(System.Math.Round(CDbl(mGrossBlock), 0))
            pNetGrossBlock = System.Math.Round(pNetGrossBlock, 0)
            pOPGrossBlock = System.Math.Round(pOPGrossBlock, 0)
            pOPNetBlock = System.Math.Round(pOPNetBlock, 0)
            mSalvageAmount = System.Math.Round(mSalvageAmount, 0)


            mSqlStr = "UPDATE TEMP_AST_DESP_TRN SET " & vbCrLf & " DAYS=" & mActAddDays & "," & vbCrLf & " CURRENT_DESP=" & mDepAmount & "," & vbCrLf & " CUMULATIVE_DESP=" & mCummDepAmount & "," & vbCrLf & " SALE_AMOUNT=" & VB6.Format(mSaleAmount, "0") & "," & vbCrLf & " SALE_DATE='" & mSaleDate & "'," & vbCrLf & " SALE_DESP=" & pSaleDesp & "," & vbCrLf & " GROSS_BLOCK=" & mGrossBlock & "," & vbCrLf & " NET_BLOCK=" & pNetGrossBlock & "," & vbCrLf & " OP_GROSS_BLOCK=" & pOPGrossBlock & ", " & vbCrLf & " OP_WDV=" & pOPNetBlock & ", " & vbCrLf & " SALVAGE_AMOUNT=" & mSalvageAmount & ""

            mSqlStr = mSqlStr & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & mRefNo & ""

            PubDBCn.Execute(mSqlStr)

NextRec:

            '        If cntRow = 4600 Then					
            '            MsgBox cntRow					
            '        End If					
            cntRow = cntRow + 1
            lblCount.Text = CStr(cntRow)
            System.Windows.Forms.Application.DoEvents()
            RsTemp.MoveNext()
        Loop
        '''********************************					
        UpdateTempTable = True

        Exit Function
LedgError:
        '    Resume					
        UpdateTempTable = False
        ErrorMsg(Err.Description & " - " & mRefNo, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetDays(ByRef pRefNo As Double, ByRef pCompanyCode As Integer) As Double
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mUsefullLife As Double
        Dim mAstType As Double
        Dim mBalanceLife As Double
        Dim mPurchaseDate As String
        Dim mBalAmount As Double
        Dim mPurchaseLife As Double
        Dim mStartDate As String
        Dim mEndDate As String


        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim mSaleAmount5 As Double
        Dim mSaleAmount6 As Double
        Dim mActSaleAmount1 As Double
        Dim mActSaleAmount2 As Double
        Dim mActSaleAmount3 As Double
        Dim mActSaleAmount4 As Double
        Dim mActSaleAmount5 As Double
        Dim mActSaleAmount6 As Double

        Dim mSaleDate1 As String
        Dim mSaleDate2 As String
        Dim mSaleDate3 As String
        Dim mSaleDate4 As String
        Dim mSaleDate5 As String
        Dim mSaleDate6 As String
        Dim mCalcDays As Integer

        mCalcDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(txtDepreciationDate.Text)) + 1
        mCalcDays = IIf(mCalcDays > 365, 365 + IIf(RsCompany.Fields("COMPANY_CODE").Value = 27, 1, 0), mCalcDays)


        SqlStr = "SELECT GROUP_CODE, PUT_DATE, (TOTAL_COST-DN_CR_AMOUNT+OTH_AMOUNT+DN_CR_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT)) -" & vbCrLf & " (GETASSETSALEVALUE(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE1(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE2(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE3(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE4(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE5(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') ) " & vbCrLf & " AS Bal_Amount" & vbCrLf & " FROM AST_ASSET_TRN TRN" & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mAstType = IIf(IsDBNull(RsTemp.Fields("GROUP_CODE").Value), 0, RsTemp.Fields("GROUP_CODE").Value)
            mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUT_DATE").Value), 0, RsTemp.Fields("PUT_DATE").Value), "DD/MM/YYYY")

            If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(lblFYStartDate.Text))
            Else
                mPurchaseLife = 0
            End If

            mBalAmount = IIf(IsDBNull(RsTemp.Fields("Bal_Amount").Value), 0, RsTemp.Fields("Bal_Amount").Value)
            If mBalAmount <= 0 Then
                GetDays = 0
            Else
                mUsefullLife = GetUsefullLife(pCompanyCode, mAstType)
                GetDays = mUsefullLife - mPurchaseLife
            End If
        End If


        If CheckSaleAmount(pRefNo, pCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text)) = 0 Then

            If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                GetDays = IIf(GetDays < 0, 0, GetDays)
                GetDays = IIf(GetDays >= mCalcDays, mCalcDays, GetDays)
            Else
                GetDays = IIf(GetDays < 0, 0, GetDays)
                GetDays = IIf(GetDays >= mCalcDays, mCalcDays - DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mPurchaseDate)), GetDays)
            End If

        Else
            If CalcSaleAmount(pRefNo, pCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mActSaleAmount1, mActSaleAmount2, mActSaleAmount3, mActSaleAmount4, mActSaleAmount5, mActSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6) = False Then GoTo LedgError

            If mSaleAmount1 <> 0 Then
                GetDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate1)) + 1
            End If
            If mSaleAmount2 <> 0 Then
                GetDays = GetDays + DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mSaleDate1), CDate(mSaleDate2)) + 1
            End If
            If mSaleAmount3 <> 0 Then
                GetDays = GetDays + DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mSaleDate2), CDate(mSaleDate3)) + 1
            End If
            If mSaleAmount4 <> 0 Then
                GetDays = GetDays + DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mSaleDate3), CDate(mSaleDate4)) + 1
            End If
            If mSaleAmount5 <> 0 Then
                GetDays = GetDays + DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mSaleDate4), CDate(mSaleDate5)) + 1
            End If
        End If


        Exit Function
LedgError:
        'Resume					
        GetDays = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetDespAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef pOPGrossBlock As Double) As Double
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mUsefullLife As Double
        Dim mAstType As Double
        Dim mBalanceLife As Double
        Dim mPurchaseDate As String
        Dim mBalAmount As Double
        Dim mPurchaseLife As Double
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mDays As Integer
        Dim mBalUsefullLife As Double


        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim mSaleAmount5 As Double
        Dim mSaleAmount6 As Double
        Dim mActSaleAmount1 As Double
        Dim mActSaleAmount2 As Double
        Dim mActSaleAmount3 As Double
        Dim mActSaleAmount4 As Double
        Dim mActSaleAmount5 As Double
        Dim mActSaleAmount6 As Double

        Dim mSaleDate1 As String
        Dim mSaleDate2 As String
        Dim mSaleDate3 As String
        Dim mSaleDate4 As String
        Dim mSaleDate5 As String
        Dim mSaleDate6 As String

        Dim mNetDays As Integer
        Dim xStartDate As String
        Dim mCalcDays As Integer

        '+DN_CR_AMOUNT					

        SqlStr = "SELECT GROUP_CODE, PUT_DATE, (TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT)) - " & vbCrLf & " (GETASSETSALEVALUE(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE1(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE2(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE3(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE4(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE5(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') ) " & vbCrLf & " AS Bal_Amount" & vbCrLf & " FROM AST_ASSET_TRN TRN" & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mAstType = IIf(IsDBNull(RsTemp.Fields("GROUP_CODE").Value), 0, RsTemp.Fields("GROUP_CODE").Value)
            mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUT_DATE").Value), 0, RsTemp.Fields("PUT_DATE").Value), "DD/MM/YYYY")

            If CDate(mPurchaseDate) < CDate(ConsStartDate) Then ''08/06/2016  If CDate(mPurchaseDate) < CDate(lblFYStartDate.Caption) Then					
                mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(ConsStartDate)) ''08/06/2016   DateDiff("d", mPurchaseDate, lblFYStartDate.Caption)					
            Else
                mPurchaseLife = 0
            End If

            mBalAmount = IIf(IsDBNull(RsTemp.Fields("Bal_Amount").Value), 0, RsTemp.Fields("Bal_Amount").Value)
            If mBalAmount <= 0 Then
                mBalUsefullLife = 0
            Else
                mUsefullLife = GetUsefullLife(pCompanyCode, mAstType)
                mBalUsefullLife = mUsefullLife - mPurchaseLife
            End If
        End If

        ''17/06/2016					
        If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then ''CDate(mPurchaseDate) < CDate(ConsStartDate) Then					
            xStartDate = CStr(CDate(lblFYStartDate.Text)) 'ConsStartDate					
            '    ElseIf CDate(mPurchaseDate) < CDate(lblFYStartDate.Caption) Then					
            '        xStartDate = CDate(lblFYStartDate.Caption)					
        Else
            xStartDate = mPurchaseDate
        End If

        mBalUsefullLife = IIf(mBalUsefullLife < 0, 0, mBalUsefullLife)

        If mBalUsefullLife <= 0 Then
            GetDespAmount = 0
            Exit Function
        End If

        mCalcDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(txtDepreciationDate.Text)) + 1
        mCalcDays = IIf(mCalcDays > 365, 365 + IIf(RsCompany.Fields("COMPANY_CODE").Value = 27, 1, 0), mCalcDays)

        If CheckSaleAmount(pRefNo, pCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text)) = 0 Then
            If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                mDays = IIf(mBalUsefullLife < 0, 0, mBalUsefullLife)
                mDays = IIf(mBalUsefullLife >= mCalcDays, mCalcDays, mBalUsefullLife)
            Else
                mDays = IIf(mBalUsefullLife < 0, 0, mBalUsefullLife)
                mDays = IIf(mBalUsefullLife >= mCalcDays, mCalcDays - DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mPurchaseDate)), mBalUsefullLife)
            End If

            If mBalUsefullLife <= mDays Then
                GetDespAmount = pOPGrossBlock
            Else
                GetDespAmount = (pOPGrossBlock) * mDays / mBalUsefullLife
            End If
        Else
            If CalcSaleAmount(pRefNo, pCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mActSaleAmount1, mActSaleAmount2, mActSaleAmount3, mActSaleAmount4, mActSaleAmount5, mActSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6) = False Then GoTo LedgError

            If mSaleAmount1 <> 0 Then
                If mBalUsefullLife < DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate1)) + 1 Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate1)) + 1 - (DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate1)) - mBalUsefullLife + 1)
                    mDays = IIf(mDays < 0, 0, mDays)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate1)) + 1 ''06/08/2016					
                End If

                '            mDays = DateDiff("d", xStartDate, mSaleDate1) + 1  ''06/08/2016					
                '            mNetDays = mDays					
                If mBalUsefullLife <= mDays Then
                    GetDespAmount = pOPGrossBlock
                Else
                    GetDespAmount = (mSaleAmount1) * mDays / mBalUsefullLife
                End If
            End If

            If mSaleAmount2 <> 0 Then
                If mBalUsefullLife < DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate2)) + 1 Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate2)) + 1 - (DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate2)) - mBalUsefullLife + 1)
                    mDays = IIf(mDays < 0, 0, mDays)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate2)) + 1
                End If
                '            mNetDays = mNetDays + mDays					
                If mBalUsefullLife <= mDays Then
                    GetDespAmount = pOPGrossBlock
                Else
                    GetDespAmount = GetDespAmount + (mSaleAmount2 * mDays / mBalUsefullLife)
                End If
            End If

            If mSaleAmount3 <> 0 Then
                If mBalUsefullLife < DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate3)) + 1 Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate3)) + 1 - (DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate3)) - mBalUsefullLife + 1)
                    mDays = IIf(mDays < 0, 0, mDays)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate3)) ''+ 1					
                End If

                '            mNetDays = mNetDays + mDays					
                If mBalUsefullLife <= mDays Then
                    GetDespAmount = pOPGrossBlock
                Else
                    GetDespAmount = GetDespAmount + (mSaleAmount3 * mDays / mBalUsefullLife)
                End If
            End If

            If mSaleAmount4 <> 0 Then
                If mBalUsefullLife < DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate4)) + 1 Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate4)) + 1 - (DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate4)) - mBalUsefullLife + 1)
                    mDays = IIf(mDays < 0, 0, mDays)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate4)) ''+ 1					
                End If
                '            mNetDays = mNetDays + mDays					
                If mBalUsefullLife <= mDays Then
                    GetDespAmount = pOPGrossBlock
                Else
                    GetDespAmount = GetDespAmount + (mSaleAmount4 * mDays / mBalUsefullLife)
                End If
            End If

            If mSaleAmount5 <> 0 Then
                If mBalUsefullLife < DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate5)) + 1 Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate5)) + 1 - (DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(ConsStartDate), CDate(mSaleDate5)) - mBalUsefullLife + 1)
                    mDays = IIf(mDays < 0, 0, mDays)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(mSaleDate5)) ''+ 1					
                End If

                '            mNetDays = mNetDays + mDays					
                If mBalUsefullLife <= mDays Then
                    GetDespAmount = pOPGrossBlock
                Else
                    GetDespAmount = GetDespAmount + (mSaleAmount5 * mDays / mBalUsefullLife)
                End If
            End If

        End If

        If pOPGrossBlock - (mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5) > 0 And (mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5) > 0 Then
            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(txtDepreciationDate.Text)) + 1
            If mBalUsefullLife <= mDays Then
                GetDespAmount = pOPGrossBlock
            Else
                GetDespAmount = GetDespAmount + ((pOPGrossBlock - (mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5)) * mDays / mBalUsefullLife)
            End If
        End If

        GetDespAmount = CDbl(VB6.Format(GetDespAmount, "0"))
        Exit Function
LedgError:
        GetDespAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetSaleAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef pOPGrossBlock As Double, ByRef mSaleDate As String, ByRef pSaleDesp As Double, ByRef xLastYearSaleDesp As Double, ByRef GetSalvageAmountOn As Double) As Double
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mUsefullLife As Double
        Dim mAstType As Double
        Dim mBalanceLife As Double
        Dim mPurchaseDate As String
        Dim mBalAmount As Double
        Dim mPurchaseLife As Double
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mDays As Integer
        Dim mBalUsefullLife As Double

        Dim mPurchaseAmount As Double
        Dim pActSaleAmount As Double

        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim mSaleAmount5 As Double
        Dim mSaleAmount6 As Double

        Dim mActSaleAmount1 As Double
        Dim mActSaleAmount2 As Double
        Dim mActSaleAmount3 As Double
        Dim mActSaleAmount4 As Double
        Dim mActSaleAmount5 As Double
        Dim mActSaleAmount6 As Double

        Dim mSaleDate1 As String
        Dim mSaleDate2 As String
        Dim mSaleDate3 As String
        Dim mSaleDate4 As String
        Dim mSaleDate5 As String
        Dim mSaleDate6 As String
        Dim mDepreMode As String

        Dim mDepRate As Double

        If chkAllDepr.CheckState = System.Windows.Forms.CheckState.Checked Then
            mDepreMode = "SLM1"
        Else
            mDepreMode = Trim(txtDeprMode.Text)
        End If


        GetSaleAmount = 0
        pSaleDesp = 0
        mSaleDate = ""
        If CalcSaleAmount(pRefNo, pCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mActSaleAmount1, mActSaleAmount2, mActSaleAmount3, mActSaleAmount4, mActSaleAmount5, mActSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6) = False Then GoTo LedgError

        GetSaleAmount = mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6
        pActSaleAmount = mActSaleAmount1 + mActSaleAmount2 + mActSaleAmount3 + mActSaleAmount4 + mActSaleAmount5 + mActSaleAmount6

        If GetSaleAmount = 0 And pActSaleAmount > 0 Then
            pSaleDesp = GetOPCummulativeDesp(pRefNo, pCompanyCode, "N")
            Exit Function
        End If

        If GetSaleAmount = 0 Then Exit Function

        If GetSaleAmount > 0 Then
            mSaleDate = mSaleDate1
            mSaleDate = mSaleDate & IIf(mSaleDate2 <> "", ", " & mSaleDate2, "")
            mSaleDate = mSaleDate & IIf(mSaleDate3 <> "", ", " & mSaleDate3, "")
            mSaleDate = mSaleDate & IIf(mSaleDate4 <> "", ", " & mSaleDate4, "")
            mSaleDate = mSaleDate & IIf(mSaleDate5 <> "", ", " & mSaleDate5, "")
            mSaleDate = mSaleDate & IIf(mSaleDate6 <> "", ", " & mSaleDate6, "")
        End If

        ''-DN_CR_AMOUNT					

        SqlStr = "SELECT GROUP_CODE, PUT_DATE,TOTAL_COST+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT)-DN_CR_AMOUNT PURCHASE_AMOUNT" & vbCrLf & " FROM AST_ASSET_TRN TRN" & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mAstType = IIf(IsDBNull(RsTemp.Fields("GROUP_CODE").Value), 0, RsTemp.Fields("GROUP_CODE").Value)
            mPurchaseAmount = IIf(IsDBNull(RsTemp.Fields("PURCHASE_AMOUNT").Value), 0, RsTemp.Fields("PURCHASE_AMOUNT").Value)
            mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUT_DATE").Value), 0, RsTemp.Fields("PUT_DATE").Value), "DD/MM/YYYY")
            mDepRate = GetDepreciationRate(pCompanyCode, mAstType, mDepreMode)
            xLastYearSaleDesp = 0

            If GetSaleAmount > 0 Then
                pSaleDesp = GetSaleDesp(mAstType, pCompanyCode, pOPGrossBlock, mDepRate, mPurchaseAmount, mPurchaseDate, mSaleAmount1, mSaleDate1, mSaleAmount2, mSaleDate2, mSaleAmount3, mSaleDate3, mSaleAmount4, mSaleDate4, mSaleAmount5, mSaleDate5, xLastYearSaleDesp, pRefNo, GetSalvageAmountOn)
            End If

            '        If mSaleAmount2 > 0 Then					
            '            pSaleDesp = pSaleDesp + GetSaleDesp(mAstType, pCompanyCode, mDepRate, mPurchaseAmount, mPurchaseDate, mSaleAmount2, mSaleDate2)					
            '        End If					
            '					
        End If

        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetPreviousSaleAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer) As Double
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDateFrom As String

        mDateFrom = ConsStartDate

        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE <'" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousSaleAmount = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value), "0.00"))
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE1 <'" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousSaleAmount = GetPreviousSaleAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value), "0.00"))
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_BILL_DATE2" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE2 <'" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousSaleAmount = GetPreviousSaleAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value), "0.00"))
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_BILL_DATE3" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE3  <'" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousSaleAmount = GetPreviousSaleAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value), "0.00"))
        End If

        SqlStr = "SELECT ORIGINAL_COST4,SALE_BILL_DATE4" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE4  <'" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousSaleAmount = GetPreviousSaleAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value), "0.00"))
        End If

        SqlStr = "SELECT ORIGINAL_COST5,SALE_BILL_DATE5" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE5 <'" & VB6.Format(mDateFrom, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousSaleAmount = GetPreviousSaleAmount + CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value), "0.00"))
        End If

        Exit Function
LedgError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetSaleDesp(ByRef mAstType As Double, ByRef pCompanyCode As Integer, ByRef pOPGrossBlock As Double, ByRef mDeprRate As Double, ByRef mPurchaseAmount As Double, ByRef mPurchaseDate As String, ByRef xSaleAmount1 As Double, ByRef xSaleDate1 As String, ByRef xSaleAmount2 As Double, ByRef xSaleDate2 As String, ByRef xSaleAmount3 As Double, ByRef xSaleDate3 As String, ByRef xSaleAmount4 As Double, ByRef xSaleDate4 As String, ByRef xSaleAmount5 As Double, ByRef xSaleDate5 As String, ByRef xLastYearSaleDesp As Double, ByRef pRefNo As Double, ByRef GetSalvageAmountOn As Double) As Double
        On Error GoTo LedgError

        Dim xSaleDesp As Double
        Dim mDays As Double
        Dim mPurchaseLife As Double
        Dim mBalanceLife As Double
        Dim mUsefullLife As Double
        Dim mBalUsefullLife As Double

        Dim xStartDate As String
        Dim mPreviousSale As Double

        Dim SqlStr1 As String
        Dim RsTemp As ADODB.Recordset
        Dim mWDV As Double
        Dim mCalculatedValue As Double
        Dim mTable As String
        Dim mCalcDays As Integer

        GetSaleDesp = 0
        xSaleDesp = 0
        mPreviousSale = GetPreviousSaleAmount(pRefNo, pCompanyCode) ''Before Sale 2014					

        mCalcDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(txtDepreciationDate.Text)) + 1
        mCalcDays = IIf(mCalcDays > 365, 365 + IIf(RsCompany.Fields("COMPANY_CODE").Value = 27, 1, 0), mCalcDays)

        If CDate(mPurchaseDate) < CDate(ConsStartDate) Then

            '            If CDate(ConsStartDate) = CDate(lblFYStartDate.Caption) Then					
            '                mTable = "AST_DESP_TRN_TILL2013" ''& (Format(CDate(lblFYStartDate.Caption), "YYYY") - 1)					
            '            Else					
            '                mTable = "AST_DESP_TRN_TILL" & (Format(CDate(lblFYStartDate.Caption), "YYYY") - 1)					
            '            End If					
            '					
            '            SqlStr1 = "SELECT NET_BLOCK AS NET_BLOCK " & vbCrLf _					
            ''                & " FROM " & mTable & " TRN" & vbCrLf _					
            ''                & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _					
            ''                & " AND AUTO_KEY_ASSET=" & pRefNo & ""					
            '            MainClass.UOpenRecordSet SqlStr1, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly					
            '					
            '            If RsTemp.EOF = False Then					
            '                mWDV = IIf(IsNull(RsTemp!NET_BLOCK), 0, RsTemp!NET_BLOCK)					
            '            End If					
            mWDV = pOPGrossBlock
            If mWDV = 0 Then
                mCalculatedValue = 0
            Else
                mCalculatedValue = (mPurchaseAmount - mPreviousSale) / mWDV * (xSaleAmount1 + xSaleAmount2 + xSaleAmount3 + xSaleAmount4 + xSaleAmount5)
            End If
            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(ConsStartDate))

            mDays = mDays - GetLeapYear(mPurchaseDate, xSaleDate1) ''lblFYStartDate.Caption					

            ''11/07/2016					
            '            xSaleDesp = ((mPurchaseAmount - mPreviousSale) * mDeprRate * 0.01 * mDays / 365)					
            '            xSaleDesp = IIf(xSaleDesp > (mPurchaseAmount - mPreviousSale), (mPurchaseAmount - mPreviousSale), xSaleDesp)					

            ''11/07/2016					
            xSaleDesp = (mCalculatedValue * mDeprRate * 0.01 * mDays / mCalcDays)
            xSaleDesp = IIf(xSaleDesp > (mCalculatedValue), (mCalculatedValue), xSaleDesp)

            xLastYearSaleDesp = xSaleDesp

            mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(ConsStartDate))
            mUsefullLife = GetUsefullLife(pCompanyCode, mAstType)
            mBalUsefullLife = mUsefullLife - mPurchaseLife

            mBalUsefullLife = IIf(mBalUsefullLife < 0, 0, mBalUsefullLife)
            xStartDate = ConsStartDate
        Else
            mPurchaseLife = 0
            mUsefullLife = GetUsefullLife(pCompanyCode, mAstType)
            mBalUsefullLife = mUsefullLife - mPurchaseLife
            mBalUsefullLife = IIf(mBalUsefullLife < 0, 0, mBalUsefullLife)
            xStartDate = mPurchaseDate
        End If

        If xSaleAmount1 > 0 And mBalUsefullLife <> 0 Then 'And Round(GetSalvageAmountOn, 0) <> Round(xLastYearSaleDesp, 0)					
            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(xSaleDate1)) + 1
            mDays = IIf(mDays > mBalUsefullLife, mBalUsefullLife, mDays)
            xSaleDesp = xSaleDesp + ((xSaleAmount1) * mDays / mBalUsefullLife)
        End If

        If xSaleAmount2 > 0 And mBalUsefullLife <> 0 Then
            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(xSaleDate2)) + 1
            mDays = IIf(mDays > mBalUsefullLife, mBalUsefullLife, mDays)
            xSaleDesp = xSaleDesp + ((xSaleAmount2) * mDays / mBalUsefullLife)
        End If

        If xSaleAmount3 > 0 And mBalUsefullLife <> 0 Then
            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(xSaleDate3)) + 1
            mDays = IIf(mDays > mBalUsefullLife, mBalUsefullLife, mDays)
            xSaleDesp = xSaleDesp + ((xSaleAmount3) * mDays / mBalUsefullLife)
        End If

        If xSaleAmount4 > 0 And mBalUsefullLife <> 0 Then
            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(xSaleDate4)) + 1
            mDays = IIf(mDays > mBalUsefullLife, mBalUsefullLife, mDays)
            xSaleDesp = xSaleDesp + ((xSaleAmount4) * mDays / mBalUsefullLife)
        End If

        If xSaleAmount5 > 0 And mBalUsefullLife <> 0 Then
            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(xStartDate), CDate(xSaleDate5)) + 1
            mDays = IIf(mDays > mBalUsefullLife, mBalUsefullLife, mDays)
            xSaleDesp = xSaleDesp + ((xSaleAmount5) * mDays / mBalUsefullLife)
        End If

        GetSaleDesp = xSaleDesp

        Exit Function
LedgError:
        '    Resume					
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetOpeningNetBlock(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mSalvageAmountOn As Double, ByRef pOPGrossBlock As Double, ByRef mPurchaseAmount As Double) As Double
        On Error GoTo LedgError
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        'Dim mPurchaseAmount As Double					
        Dim mPurchaseDate As String

        Dim SqlStr1 As String
        Dim RsTempTrn As ADODB.Recordset

        Dim mSaleAmount As Double
        Dim mSaleDate As String

        Dim mTable As String
        Dim mField1 As String
        Dim mField2 As String
        Dim pOPGrossBlock2013 As Double

        GetOpeningNetBlock = 0

        ''TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT + DN_CR_AMOUNT -(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND)					

        SqlStr = "SELECT GROUP_CODE, PUT_DATE, DN_CR_AMOUNT," & vbCrLf & " TOTAL_COST+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT) AS PURCHASE_COST, " & vbCrLf & " ORIGINAL_COST,SALE_BILL_DATE, " & vbCrLf & " ORIGINAL_COST1,SALE_BILL_DATE1, " & vbCrLf & " ORIGINAL_COST2,SALE_BILL_DATE2, " & vbCrLf & " ORIGINAL_COST3,SALE_BILL_DATE3, " & vbCrLf & " ORIGINAL_COST4,SALE_BILL_DATE4, " & vbCrLf & " ORIGINAL_COST5,SALE_BILL_DATE5 " & vbCrLf & " FROM AST_ASSET_TRN TRN" & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPurchaseAmount = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value) - IIf(IsDBNull(RsTemp.Fields("DN_CR_AMOUNT").Value), 0, RsTemp.Fields("DN_CR_AMOUNT").Value)
            mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUT_DATE").Value), "", RsTemp.Fields("PUT_DATE").Value), "DD/MM/YYYY")
            mSalvageAmountOn = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value) - IIf(IsDBNull(RsTemp.Fields("DN_CR_AMOUNT").Value), 0, RsTemp.Fields("DN_CR_AMOUNT").Value)
            '        mSalvageAmountOn = mSalvageAmountOn - GetOPCummulativeDesp(pRefNo, pCompanyCode)					
            mSaleDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE").Value), "", RsTemp.Fields("SALE_BILL_DATE").Value), "DD/MM/YYYY")
            mSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value)
            If mSaleAmount > 0 Then
                If CDate(mSaleDate) <= CDate(lblFYEndDate.Text) Then
                    mSalvageAmountOn = mSalvageAmountOn - mSaleAmount
                End If
            End If

            mSaleDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE1").Value), "", RsTemp.Fields("SALE_BILL_DATE1").Value), "DD/MM/YYYY")
            mSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value)
            If mSaleAmount > 0 Then
                If CDate(mSaleDate) <= CDate(lblFYEndDate.Text) Then
                    mSalvageAmountOn = mSalvageAmountOn - mSaleAmount
                End If
            End If

            mSaleDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE2").Value), "", RsTemp.Fields("SALE_BILL_DATE2").Value), "DD/MM/YYYY")
            mSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value)
            If mSaleAmount > 0 Then
                If CDate(mSaleDate) <= CDate(lblFYEndDate.Text) Then
                    mSalvageAmountOn = mSalvageAmountOn - mSaleAmount
                End If
            End If

            mSaleDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE3").Value), "", RsTemp.Fields("SALE_BILL_DATE3").Value), "DD/MM/YYYY")
            mSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value)
            If mSaleAmount > 0 Then
                If CDate(mSaleDate) <= CDate(lblFYEndDate.Text) Then
                    mSalvageAmountOn = mSalvageAmountOn - mSaleAmount
                End If
            End If

            mSaleDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE4").Value), "", RsTemp.Fields("SALE_BILL_DATE4").Value), "DD/MM/YYYY")
            mSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value)
            If mSaleAmount > 0 Then
                If CDate(mSaleDate) <= CDate(lblFYEndDate.Text) Then
                    mSalvageAmountOn = mSalvageAmountOn - mSaleAmount
                End If
            End If

            mSaleDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE5").Value), "", RsTemp.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")
            mSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value)
            If mSaleAmount > 0 Then
                If CDate(mSaleDate) <= CDate(lblFYEndDate.Text) Then
                    mSalvageAmountOn = mSalvageAmountOn - mSaleAmount
                End If
            End If
        End If

        If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then ''CDate(ConsStartDate) Then					

            SqlStr1 = "SELECT GROSS_BLOCK AS GROSS_BLOCK " & vbCrLf & " FROM AST_DESP_TRN_TILL2013 TRN" & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""
            MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempTrn, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTempTrn.EOF = False Then
                pOPGrossBlock = IIf(IsDBNull(RsTempTrn.Fields("GROSS_BLOCK").Value), 0, RsTempTrn.Fields("GROSS_BLOCK").Value)
            End If


            If CDate(ConsStartDate) = CDate(lblFYStartDate.Text) Then
                mTable = "AST_DESP_TRN_TILL2013" ''& (Format(CDate(lblFYStartDate.Caption), "YYYY") - 1)					
                mField1 = "NET_BLOCK"
                mField2 = "GROSS_BLOCK"

            Else
                mTable = "AST_DESP_TRN_TILL" & (CDbl(VB6.Format(CDate(lblFYStartDate.Text), "YYYY")) - 1)
                mField1 = "OP_GROSS_BLOCK"
                mField2 = "OP_GROSS_BLOCK"
            End If

            SqlStr1 = "SELECT " & mField1 & " AS NET_BLOCK, " & mField2 & " AS GROSS_BLOCK " & vbCrLf & " FROM " & mTable & " TRN" & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""
            MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempTrn, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTempTrn.EOF = False Then
                GetOpeningNetBlock = IIf(IsDBNull(RsTempTrn.Fields("NET_BLOCK").Value), 0, RsTempTrn.Fields("NET_BLOCK").Value)
                '            pOPGrossBlock = IIf(IsNull(RsTempTrn!GROSS_BLOCK), 0, RsTempTrn!GROSS_BLOCK)					
            End If

            '        If CDate(mPurchaseDate) < CDate(ConsStartDate) Then					
            '            pOPGrossBlock = pOPGrossBlock2013					
            '        End If					
        Else
            GetOpeningNetBlock = mPurchaseAmount
        End If

        Exit Function
LedgError:
        '    Resume					
        GetOpeningNetBlock = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetOPCummulativeDesp(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef pIsOpening As String) As Double
        On Error GoTo LedgError

        Dim SqlStr1 As String
        Dim RsTempTrn As ADODB.Recordset
        Dim mTable As String

        GetOPCummulativeDesp = 0

        '    mTable = ConsStartDate ' CDate(lblFYStartDate.Caption)					

        If pIsOpening = "Y" Then
            mTable = "AST_DESP_TRN_TILL2013"
        Else
            mTable = "AST_DESP_TRN_TILL" & (CDbl(VB6.Format(CDate(lblFYStartDate.Text), "YYYY")) - 1)
        End If

        SqlStr1 = "SELECT (NVL(CUMULATIVE_DESP,0) - NVL(SALE_DESP,0)) AS CUMULATIVE_DESP " & vbCrLf & " FROM " & mTable & " TRN" & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""
        MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempTrn, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTempTrn.EOF = False Then
            GetOPCummulativeDesp = IIf(IsDBNull(RsTempTrn.Fields("CUMULATIVE_DESP").Value), 0, RsTempTrn.Fields("CUMULATIVE_DESP").Value)
        End If

        Exit Function
LedgError:
        GetOPCummulativeDesp = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function InsertDataInTempFile() As Boolean
        On Error GoTo LedgError
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
        Dim mSqlStr As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        mSqlStr = "INSERT INTO TEMP_AST_DESP_TRN (" & vbCrLf _
            & " USERID, COMPANY_CODE, COMPANY_NAME, " & vbCrLf _
            & " FYEAR, BOOKTYPE, BOOKSUBTYPE, " & vbCrLf _
            & " TRNCODE, TRNNAME, SUPP_CUST_NAME, ITEM_DESC, " & vbCrLf _
            & " AUTO_KEY_ASSET, LOCATION, PV_DATE, PUT_DATE," & vbCrLf _
            & " TOTAL_COST, DAYS, PUR_YEAR, " & vbCrLf _
            & " CURRENT_DESP, CUMULATIVE_DESP, SALE_AMOUNT, " & vbCrLf _
            & " SALE_DATE, SALE_DESP, PHY_DATE, " & vbCrLf _
            & " PHY_WHOM, GROSS_BLOCK, NET_BLOCK, " & vbCrLf _
            & " ITEM_TYPE, ASSET_TYPE,OP_GROSS_BLOCK,OP_WDV)"


        SqlStr = " SELECT  '" & MainClass.AllowSingleQuote(PubUserID) & "', TRN.COMPANY_CODE, GEN.COMPANY_NAME," & vbCrLf _
            & " TRN.FYEAR, TRN.BOOKTYPE, '1'," & vbCrLf _
            & " INVMST.CODE, INVMST.NAME, TRN.SUPP_CUST_NAME, TRN.ITEM_DESC, " & vbCrLf _
            & " TRN.AUTO_KEY_ASSET, TRN.LOCATION, TRN.PV_DATE, TRN.PUT_DATE," & vbCrLf _
            & " TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT), 0, TRN.FYEAR," & vbCrLf _
            & " 0, 0, 0, " & vbCrLf _
            & " '', 0, ''," & vbCrLf _
            & " '', 0, 0, " & vbCrLf _
            & " TRN.ITEM_TYPE, TRN.AST_TYPE,0,0"

        SqlStr = SqlStr & vbCrLf & " FROM AST_ASSET_TRN TRN, FIN_INVTYPE_MST INVMST, GEN_COMPANY_MST GEN"

        ''''WHERE CLAUSE...					
        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND TRN.GROUP_CODE=INVMST.CODE"

        If cboCompany.SelectedIndex > 0 Then
            mCompanyName = Trim(cboCompany.Text)
            SqlStr = SqlStr & vbCrLf & " AND GEN.COMPANY_NAME='" & MainClass.AllowSingleQuote(mCompanyName) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If

            'If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mGroupCode = IIf(IsDBNull(MasterNo), "", MasterNo)
            'End If
            'SqlStr = SqlStr & vbCrLf & "AND GROUP_CODE='" & MainClass.AllowSingleQuote(mGroupCode) & "'"
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ACCOUNTPOSTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If optOption(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ABS(GETASSETSALEVALUE(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE1(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE2(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE3(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE4(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE5(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') ) " & vbCrLf & " - ABS(TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT+DN_CR_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT))<>0"
        ElseIf optOption(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ABS(GETASSETSALEVALUE(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE1(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE2(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE3(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE4(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE5(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') ) " & vbCrLf & " = ABS(TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT+DN_CR_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT))"
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.CANCELLED='N'"

        If optDate(1).Checked = True Then
            If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf & " AND PV_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND PV_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        Else
            If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf & " AND PUT_DATE <= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND PUT_DATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If
        If chkRefNo.CheckState = System.Windows.Forms.CheckState.Unchecked And Val(txtRefNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.AUTO_KEY_ASSET=" & Val(txtRefNo.Text) & ""
        End If

        If optDate(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY GEN.COMPANY_NAME,INVMST.NAME,TRN.FYEAR,TRN.PV_DATE,TRN.PV_NO"
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY GEN.COMPANY_NAME,INVMST.NAME,TRN.FYEAR,TRN.PUT_DATE,TRN.PV_NO"
        End If

        mSqlStr = mSqlStr & vbCrLf & SqlStr
        PubDBCn.Execute(mSqlStr)

        If UpdateTempTable() = False Then GoTo LedgError

        PubDBCn.CommitTrans()
        InsertDataInTempFile = True
        Exit Function
LedgError:
        '    Resume					
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        InsertDataInTempFile = False
        PubDBCn.RollbackTrans()

    End Function

    Private Function Show1() As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim cntRow As Integer


        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")



        Show1 = True


        Exit Function
LedgError:
        '    Resume					
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetLeapYear(ByRef pStartDate As String, ByRef pEndDate As String) As Integer
        On Error GoTo LedgError

        Dim cntDate As Date
        Dim mAddDays As Integer

        GetLeapYear = 0
        pStartDate = CStr(CDate(pStartDate))
        pEndDate = CStr(CDate(pEndDate))
        cntDate = CDate(pStartDate)
        Do While cntDate <= CDate(pEndDate)
            'UPGRADE_WARNING: Untranslated statement in GetLeapYear. Please check source code.					
            cntDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, cntDate)
        Loop
        Exit Function
LedgError:
        '    Resume					
        GetLeapYear = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function





    Private Function CheckSaleAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mStartDate As String, ByRef mEndDate As String) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALE_BILL_DATE <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE1 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALE_BILL_DATE1 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_BILL_DATE2" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE2 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALE_BILL_DATE2 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_BILL_DATE3" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE3 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALE_BILL_DATE3 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST4,SALE_BILL_DATE4" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE4 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALE_BILL_DATE4 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST5,SALE_BILL_DATE5" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE5 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALE_BILL_DATE5 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value)
        End If

        Exit Function
LedgError:
        CheckSaleAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CalcSaleAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mStartDate As String, ByRef mEndDate As String, ByRef mSaleAmount1 As Double, ByRef mSaleAmount2 As Double, ByRef mSaleAmount3 As Double, ByRef mSaleAmount4 As Double, ByRef mSaleAmount5 As Double, ByRef mSaleAmount6 As Double, ByRef mActSaleAmount1 As Double, ByRef mActSaleAmount2 As Double, ByRef mActSaleAmount3 As Double, ByRef mActSaleAmount4 As Double, ByRef mActSaleAmount5 As Double, ByRef mActSaleAmount6 As Double, ByRef mSaleDate1 As String, ByRef mSaleDate2 As String, ByRef mSaleDate3 As String, ByRef mSaleDate4 As String, ByRef mSaleDate5 As String, ByRef mSaleDate6 As String) As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mFirstSale As Boolean
        Dim mSaleCount As Integer


        SqlStr = "SELECT ORIGINAL_COST,SALE_AMOUNT,SALE_BILL_DATE" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mSaleCount = 0
        If RsTemp.EOF = False Then
            mSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value), "0.00"))
            mActSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT").Value), 0, RsTemp.Fields("SALE_AMOUNT").Value), "0.00"))
            mSaleDate1 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE").Value), "", RsTemp.Fields("SALE_BILL_DATE").Value), "DD/MM/YYYY")
            mSaleCount = mSaleCount + 1
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_AMOUNT1,SALE_BILL_DATE1" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE1 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE1 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If mSaleCount = 0 Then
                mSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value), "0.00"))
                mActSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT1").Value), 0, RsTemp.Fields("SALE_AMOUNT1").Value), "0.00"))
                mSaleDate1 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE1").Value), "", RsTemp.Fields("SALE_BILL_DATE1").Value), "DD/MM/YYYY")
            Else
                mSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value), "0.00"))
                mActSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT1").Value), 0, RsTemp.Fields("SALE_AMOUNT1").Value), "0.00"))
                mSaleDate2 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE1").Value), "", RsTemp.Fields("SALE_BILL_DATE1").Value), "DD/MM/YYYY")
            End If
            mSaleCount = mSaleCount + 1
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_AMOUNT2,SALE_BILL_DATE2" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE2 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE2 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If mSaleCount = 0 Then
                mSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value), "0.00"))
                mActSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT2").Value), 0, RsTemp.Fields("SALE_AMOUNT2").Value), "0.00"))
                mSaleDate1 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE2").Value), "", RsTemp.Fields("SALE_BILL_DATE2").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 1 Then
                mSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value), "0.00"))
                mActSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT2").Value), 0, RsTemp.Fields("SALE_AMOUNT2").Value), "0.00"))
                mSaleDate2 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE2").Value), "", RsTemp.Fields("SALE_BILL_DATE2").Value), "DD/MM/YYYY")
            Else
                mSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value), "0.00"))
                mActSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT2").Value), 0, RsTemp.Fields("SALE_AMOUNT2").Value), "0.00"))
                mSaleDate3 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE2").Value), "", RsTemp.Fields("SALE_BILL_DATE2").Value), "DD/MM/YYYY")
            End If
            mSaleCount = mSaleCount + 1
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_AMOUNT3,SALE_BILL_DATE3" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE3 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE3 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If mSaleCount = 0 Then
                mSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value), "0.00"))
                mActSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT3").Value), 0, RsTemp.Fields("SALE_AMOUNT3").Value), "0.00"))
                mSaleDate1 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE3").Value), "", RsTemp.Fields("SALE_BILL_DATE3").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 1 Then
                mSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value), "0.00"))
                mActSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT3").Value), 0, RsTemp.Fields("SALE_AMOUNT3").Value), "0.00"))
                mSaleDate2 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE3").Value), "", RsTemp.Fields("SALE_BILL_DATE3").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 2 Then
                mSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value), "0.00"))
                mActSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT3").Value), 0, RsTemp.Fields("SALE_AMOUNT3").Value), "0.00"))
                mSaleDate3 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE3").Value), "", RsTemp.Fields("SALE_BILL_DATE3").Value), "DD/MM/YYYY")
            Else
                mSaleAmount4 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value), "0.00"))
                mActSaleAmount4 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT3").Value), 0, RsTemp.Fields("SALE_AMOUNT3").Value), "0.00"))
                mSaleDate4 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE3").Value), "", RsTemp.Fields("SALE_BILL_DATE3").Value), "DD/MM/YYYY")
            End If
            mSaleCount = mSaleCount + 1
        End If

        SqlStr = "SELECT ORIGINAL_COST4,SALE_AMOUNT4,SALE_BILL_DATE4" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE4 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE4 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If mSaleCount = 0 Then
                mSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value), "0.00"))
                mActSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT4").Value), 0, RsTemp.Fields("SALE_AMOUNT4").Value), "0.00"))
                mSaleDate1 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE4").Value), "", RsTemp.Fields("SALE_BILL_DATE4").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 1 Then
                mSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value), "0.00"))
                mActSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT4").Value), 0, RsTemp.Fields("SALE_AMOUNT4").Value), "0.00"))
                mSaleDate2 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE4").Value), "", RsTemp.Fields("SALE_BILL_DATE4").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 2 Then
                mSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value), "0.00"))
                mActSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT4").Value), 0, RsTemp.Fields("SALE_AMOUNT4").Value), "0.00"))
                mSaleDate3 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE4").Value), "", RsTemp.Fields("SALE_BILL_DATE4").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 3 Then
                mSaleAmount4 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value), "0.00"))
                mActSaleAmount4 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT4").Value), 0, RsTemp.Fields("SALE_AMOUNT4").Value), "0.00"))
                mSaleDate4 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE4").Value), "", RsTemp.Fields("SALE_BILL_DATE4").Value), "DD/MM/YYYY")
            Else
                mSaleAmount5 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value), "0.00"))
                mActSaleAmount5 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT4").Value), 0, RsTemp.Fields("SALE_AMOUNT4").Value), "0.00"))
                mSaleDate5 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE4").Value), "", RsTemp.Fields("SALE_BILL_DATE4").Value), "DD/MM/YYYY")
            End If
            mSaleCount = mSaleCount + 1
        End If

        SqlStr = "SELECT ORIGINAL_COST5,SALE_AMOUNT5,SALE_BILL_DATE5" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE5 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE5 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If mSaleCount = 0 Then
                mSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value), "0.00"))
                mActSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT5").Value), 0, RsTemp.Fields("SALE_AMOUNT5").Value), "0.00"))
                mSaleDate1 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE5").Value), "", RsTemp.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 1 Then
                mSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value), "0.00"))
                mActSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT5").Value), 0, RsTemp.Fields("SALE_AMOUNT5").Value), "0.00"))
                mSaleDate2 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE5").Value), "", RsTemp.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 2 Then
                mSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value), "0.00"))
                mActSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT5").Value), 0, RsTemp.Fields("SALE_AMOUNT5").Value), "0.00"))
                mSaleDate3 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE5").Value), "", RsTemp.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 3 Then
                mSaleAmount4 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value), "0.00"))
                mActSaleAmount4 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT5").Value), 0, RsTemp.Fields("SALE_AMOUNT5").Value), "0.00"))
                mSaleDate4 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE5").Value), "", RsTemp.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")
            ElseIf mSaleCount = 4 Then
                mSaleAmount5 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value), "0.00"))
                mActSaleAmount5 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT5").Value), 0, RsTemp.Fields("SALE_AMOUNT5").Value), "0.00"))
                mSaleDate5 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE5").Value), "", RsTemp.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")
            Else
                mSaleAmount6 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value), "0.00"))
                mActSaleAmount6 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_AMOUNT5").Value), 0, RsTemp.Fields("SALE_AMOUNT5").Value), "0.00"))
                mSaleDate6 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE5").Value), "", RsTemp.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")
            End If

            mSaleCount = mSaleCount + 1
        End If

        CalcSaleAmount = True
        Exit Function
LedgError:
        '    Resume					
        CalcSaleAmount = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetGrossBlock(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mEndDate As String, ByRef xPurchaseAmount As Double) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mPurchaseAmount As Double

        GetGrossBlock = 0

        '    SqlStr = " SELECT  TOTAL_COST+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND) AS PURCHASE_COST " & vbCrLf _					
        ''            & " FROM AST_ASSET_TRN " & vbCrLf _					
        ''            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _					
        ''            & " AND AUTO_KEY_ASSET=" & pRefNo & ""					
        '					
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly					
        '					
        '    If RsTemp.EOF = False Then					
        '        GetGrossBlock = IIf(IsNull(RsTemp!PURCHASE_COST), 0, RsTemp!PURCHASE_COST)					
        '        mPurchaseAmount = IIf(IsNull(RsTemp!PURCHASE_COST), 0, RsTemp!PURCHASE_COST)					
        '    End If					

        GetGrossBlock = xPurchaseAmount
        mPurchaseAmount = xPurchaseAmount

        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE1 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_BILL_DATE2" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE2 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_BILL_DATE3" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE3 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST4,SALE_BILL_DATE4" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE4 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST5,SALE_BILL_DATE5" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE5 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value)
        End If

        '    If mPurchaseAmount > 0 Then					
        '        If GetGrossBlock < 0 Then					
        '            GetGrossBlock = 0					
        '        End If					
        '    End If					
        Exit Function
LedgError:
        GetGrossBlock = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function



    Private Function GetDepreciationRate(ByRef pCompanyCode As Integer, ByRef pTRNType As Double, ByRef pModCode As String) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mAsOnDate As String
        Dim pCurrentYear As Integer
        Dim mTable As String

        GetDepreciationRate = 0
        If CDate(txtDepreciationDate.Text) < CDate("01/04/2003") Then
            If Month(CDate(txtDepreciationDate.Text)) = 1 Or Month(CDate(txtDepreciationDate.Text)) = 2 Or Month(CDate(txtDepreciationDate.Text)) = 3 Then
                pCurrentYear = Year(CDate(txtDepreciationDate.Text)) - 1
            Else
                pCurrentYear = Year(CDate(txtDepreciationDate.Text))
            End If
        Else
            pCurrentYear = GetCurrentFYNo(PubDBCn, VB6.Format(txtDepreciationDate.Text))
        End If


        SqlStr = "SELECT FYEAR, DEPR_RATE " & vbCrLf & " FROM AST_DEPRECIATION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND GROUP_CODE=" & pTRNType & ""

        SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & pCurrentYear & ""

        SqlStr = SqlStr & vbCrLf & " AND MODE_CODE='" & pModCode & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY FYEAR"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDepreciationRate = IIf(IsDBNull(RsTemp.Fields("DEPR_RATE").Value), 0, RsTemp.Fields("DEPR_RATE").Value)
        End If


        Exit Function
LedgError:
        GetDepreciationRate = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetUsefullLife(ByRef pCompanyCode As Integer, ByRef pTRNType As Double) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDespMode As String
        Dim mYear As Integer

        SqlStr = ""
        mYear = GetCurrentFYNo(PubDBCn, (lblFYStartDate.Text))

        If chkAllDepr.CheckState = System.Windows.Forms.CheckState.Checked Then
            mDespMode = "SLM1"
        Else
            mDespMode = Trim(txtDeprMode.Text)
        End If

        SqlStr = "SELECT ASSETS_LIFE_DAYS " & vbCrLf & " FROM AST_DEPRECIATION_NEW_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & " AND MODE_CODE='" & mDespMode & "'" & vbCrLf & " AND GROUP_CODE=" & pTRNType & " AND FYEAR = " & mYear & " " ' (SELECT MAX(FYEAR) FROM AST_DEPRECIATION_NEW_MST WHERE COMPANY_CODE=" & pCompanyCode & " AND MODE_CODE='" & mDespMode & "' AND GROUP_CODE=" & pTRNType & ")"					

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetUsefullLife = IIf(IsDBNull(RsTemp.Fields("ASSETS_LIFE_DAYS").Value), 0, RsTemp.Fields("ASSETS_LIFE_DAYS").Value)
        End If


        Exit Function
LedgError:
        GetUsefullLife = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String


        SqlStr = ""
        If optShow(0).Checked = True And chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = " SELECT  '', '', '', '1', COMPANY_NAME, " & vbCrLf & " TRNNAME, 'OPENING', '', '', '', " & vbCrLf & " TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SUM(OP_GROSS_BLOCK),SUM(OP_WDV), 0, '', SUM(CURRENT_DESP), SUM(CUMULATIVE_DESP), " & vbCrLf _
                & " SUM(SALE_AMOUNT), '', SUM(SALE_DESP), 0,0,0,TO_DATE(''), '', " & vbCrLf _
                & " SUM(GROSS_BLOCK), SUM(NET_BLOCK),SUM(SALVAGE_AMOUNT), '', '',''"

            SqlStr = SqlStr & vbCrLf & " FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' "
            If optDate(1).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND PV_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND PUT_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRNNAME, COMPANY_NAME"
            '        SqlStr = SqlStr & vbCrLf & " ORDER BY 6,5,11,9"					
            SqlStr = SqlStr & vbCrLf & " UNION ALL "

        End If

        If optShow(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " SELECT  '', FYEAR, BOOKTYPE, BOOKSUBTYPE, COMPANY_NAME, " & vbCrLf & " TRNNAME, SUPP_CUST_NAME, REPLACE(REPLACE(REPLACE(REPLACE(ITEM_DESC, CHR(10),' '),CHR(9),' '),CHR(11), ' '),CHR(13),' ') AS ITEM_DESC, TO_CHAR(AUTO_KEY_ASSET), LOCATION, " & vbCrLf & " PV_DATE, PUT_DATE, OP_GROSS_BLOCK,OP_WDV, DAYS, PUR_YEAR, CURRENT_DESP, CUMULATIVE_DESP, " & vbCrLf & " SALE_AMOUNT, SALE_DATE, SALE_DESP, 0,0,0, PHY_DATE, PHY_WHOM, " & vbCrLf _
                & " GROSS_BLOCK, NET_BLOCK, SALVAGE_AMOUNT, ITEM_TYPE, ASSET_TYPE,TO_CHAR(AUTO_KEY_ASSET)"
        Else
            SqlStr = " SELECT  '', '', '', '1', '', " & vbCrLf & " TRNNAME, '', '', '', '', " & vbCrLf & " '', '',SUM(OP_GROSS_BLOCK),SUM(OP_WDV), 0, '', SUM(CURRENT_DESP), SUM(CUMULATIVE_DESP), " & vbCrLf & " SUM(SALE_AMOUNT), '', SUM(SALE_DESP),  0,0,0,'', '', " & vbCrLf _
                & " SUM(GROSS_BLOCK), SUM(NET_BLOCK),SUM(SALVAGE_AMOUNT), '', '','' "
        End If


        SqlStr = SqlStr & vbCrLf & " FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' "

        If optShow(0).Checked = True Then
            If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
                If optDate(1).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND PV_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND PUT_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If
                SqlStr = SqlStr & vbCrLf & " ORDER BY 6,5,11,9"
            Else
                If optDate(1).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " ORDER BY TRNNAME, COMPANY_NAME, PV_DATE,AUTO_KEY_ASSET"
                Else
                    SqlStr = SqlStr & vbCrLf & " ORDER BY TRNNAME, COMPANY_NAME, PUT_DATE,AUTO_KEY_ASSET"
                End If
            End If
        Else
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRNNAME"
            SqlStr = SqlStr & vbCrLf & " ORDER BY TRNNAME"
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
        If MainClass.ChkIsdateF(txtDepreciationDate) = False Then Exit Function

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

        If CDate(txtDepreciationDate.Text) <= CDate("30/06/1988") Then
            If Month(CDate(txtDepreciationDate.Text)) = 1 Or Month(CDate(txtDepreciationDate.Text)) = 2 Or Month(CDate(txtDepreciationDate.Text)) = 3 Or Month(CDate(txtDepreciationDate.Text)) = 4 Or Month(CDate(txtDepreciationDate.Text)) = 5 Or Month(CDate(txtDepreciationDate.Text)) = 6 Then
                lblCurrentFyear.Text = CStr(Year(CDate(txtDepreciationDate.Text)) - 1)
            Else
                lblCurrentFyear.Text = CStr(Year(CDate(txtDepreciationDate.Text)))
            End If
            lblFYStartDate.Text = VB6.Format("01/07/" & lblCurrentFyear.Text, "DD/MM/YYYY")
            lblFYEndDate.Text = VB6.Format("30/06/" & (CDbl(lblCurrentFyear.Text) + 1), "DD/MM/YYYY")
        ElseIf CDate(txtDepreciationDate.Text) <= CDate("31/03/1989") Then
            If Month(CDate(txtDepreciationDate.Text)) = 1 Or Month(CDate(txtDepreciationDate.Text)) = 2 Or Month(CDate(txtDepreciationDate.Text)) = 3 Then
                lblCurrentFyear.Text = CStr(Year(CDate(txtDepreciationDate.Text)) - 1)
            Else
                lblCurrentFyear.Text = CStr(Year(CDate(txtDepreciationDate.Text)))
            End If
            lblFYStartDate.Text = VB6.Format("01/07/" & lblCurrentFyear.Text, "DD/MM/YYYY")
            lblFYEndDate.Text = VB6.Format("31/03/" & (CDbl(lblCurrentFyear.Text) + 1), "DD/MM/YYYY")
        ElseIf CDate(txtDepreciationDate.Text) < CDate("01/04/2003") Then
            If Month(CDate(txtDepreciationDate.Text)) = 1 Or Month(CDate(txtDepreciationDate.Text)) = 2 Or Month(CDate(txtDepreciationDate.Text)) = 3 Then
                lblCurrentFyear.Text = CStr(Year(CDate(txtDepreciationDate.Text)) - 1)
            Else
                lblCurrentFyear.Text = CStr(Year(CDate(txtDepreciationDate.Text)))
            End If
            lblFYStartDate.Text = VB6.Format("01/04/" & lblCurrentFyear.Text, "DD/MM/YYYY")
            lblFYEndDate.Text = VB6.Format("31/03/" & (CDbl(lblCurrentFyear.Text) + 1), "DD/MM/YYYY")
        Else
            lblCurrentFyear.Text = CStr(GetCurrentFYNo(PubDBCn, VB6.Format(txtDepreciationDate.Text, "DD/MM/YYYY")))
            lblFYStartDate.Text = VB6.Format("01/04/" & lblCurrentFyear.Text, "DD/MM/YYYY")
            lblFYEndDate.Text = VB6.Format("31/03/" & (CDbl(lblCurrentFyear.Text) + 1), "DD/MM/YYYY")
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()
        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mTotalCost As Double
        Dim mDeprec1 As Double
        Dim mDeprec2 As Double
        Dim mDeprec3 As Double
        Dim mBookSubType As String
        Dim mTotalDeprec As Double
        Dim mSaleAmount As Double
        Dim mTotalDeprecClaim As Double
        Dim mCumulativeDeprec As Double
        Dim mGrossBlock As Double
        Dim mNetBlock As Double
        Dim mSelvageAmount As Double
        Dim mWDV As Double

        If optShow(0).Checked = True Then
            Call CalcSprdSubTotal()
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColBookSubType
                mBookSubType = Trim(.Text)

                If mBookSubType = "1" Then
                    .Col = ColTotalCost
                    mTotalCost = mTotalCost + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColWDV
                    mWDV = mWDV + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColDeprec1
                    mDeprec1 = mDeprec1 + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColCumulativeDeprec
                    mCumulativeDeprec = mCumulativeDeprec + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSaleAmount
                    mSaleAmount = mSaleAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColTotalDeprecClaim
                    mTotalDeprecClaim = mTotalDeprecClaim + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColGrossBlock
                    mGrossBlock = mGrossBlock + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColNetBlock
                    mNetBlock = mNetBlock + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                    .Col = ColSelvageAmount
                    mSelvageAmount = mSelvageAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
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

            .Col = ColTotalCost
            .Text = VB6.Format(mTotalCost, "0.00")

            .Col = ColWDV
            .Text = VB6.Format(mWDV, "0.00")

            .Col = ColDeprec1
            .Text = VB6.Format(mDeprec1, "0.00")

            .Col = ColCumulativeDeprec
            .Text = VB6.Format(mCumulativeDeprec, "0.00")

            .Col = ColSaleAmount
            .Text = VB6.Format(mSaleAmount, "0.00")

            .Col = ColTotalDeprecClaim
            .Text = VB6.Format(mTotalDeprecClaim, "0.00")

            .Col = ColGrossBlock
            .Text = VB6.Format(mGrossBlock, "0.00")

            .Col = ColNetBlock
            .Text = VB6.Format(mNetBlock, "0.00")

            .Col = ColSelvageAmount
            .Text = VB6.Format(mSelvageAmount, "0.00")
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
                If (mTRNType <> mCurrentType And cntRow > 1) Then ''Or cntRow = .MaxRows					

                    '                If cntRow = .MaxRows Then					
                    '                    mLastRow = True					
                    '                End If					

                    mToRow = cntRow - 1 ''IIf(mLastRow = True, 0, 1)					

                    .MaxRows = .MaxRows + 1
                    .Row = cntRow '' IIf(mLastRow = True, .MaxRows, cntRow)					
                    .Action = SS_ACTION_INSERT_ROW

                    .Col = ColPartyName
                    .Row = cntRow ''IIf(mLastRow = True, .MaxRows, cntRow)					
                    .Text = "SUB TOTAL :"
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColTrnName
                    .Text = mTRNType
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Row = cntRow ''IIf(mLastRow = True, .MaxRows, cntRow)					
                    .Row2 = cntRow ''IIf(mLastRow = True, .MaxRows, cntRow)					
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80					
                    .BlockMode = False

                    Call CalcRowTotal(SprdMain, ColTotalCost, mFromRow, ColTotalCost, mToRow, cntRow, ColTotalCost)
                    Call CalcRowTotal(SprdMain, ColWDV, mFromRow, ColWDV, mToRow, cntRow, ColWDV)
                    Call CalcRowTotal(SprdMain, ColDeprec1, mFromRow, ColDeprec1, mToRow, cntRow, ColDeprec1)
                    Call CalcRowTotal(SprdMain, ColCumulativeDeprec, mFromRow, ColCumulativeDeprec, mToRow, cntRow, ColCumulativeDeprec)


                    Call CalcRowTotal(SprdMain, ColSaleAmount, mFromRow, ColSaleAmount, mToRow, cntRow, ColSaleAmount)
                    Call CalcRowTotal(SprdMain, ColTotalDeprecClaim, mFromRow, ColTotalDeprecClaim, mToRow, cntRow, ColTotalDeprecClaim)

                    Call CalcRowTotal(SprdMain, ColGrossBlock, mFromRow, ColGrossBlock, mToRow, cntRow, ColGrossBlock)
                    Call CalcRowTotal(SprdMain, ColNetBlock, mFromRow, ColNetBlock, mToRow, cntRow, ColNetBlock)
                    Call CalcRowTotal(SprdMain, ColSelvageAmount, mFromRow, ColSelvageAmount, mToRow, cntRow, ColSelvageAmount)


                    '                Call CalcRowTotal(SprdMain, ColWDV, mFromRow, ColWDV, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColWDV)					
                    '                Call CalcRowTotal(SprdMain, ColDeprec1, mFromRow, ColDeprec1, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColDeprec1)					
                    '                Call CalcRowTotal(SprdMain, ColCumulativeDeprec, mFromRow, ColCumulativeDeprec, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColCumulativeDeprec)					
                    '					
                    '					
                    '                Call CalcRowTotal(SprdMain, ColSaleAmount, mFromRow, ColSaleAmount, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColSaleAmount)					
                    '                Call CalcRowTotal(SprdMain, ColTotalDeprecClaim, mFromRow, ColTotalDeprecClaim, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColTotalDeprecClaim)					
                    '					
                    '                Call CalcRowTotal(SprdMain, ColGrossBlock, mFromRow, ColGrossBlock, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColGrossBlock)					
                    '                Call CalcRowTotal(SprdMain, ColNetBlock, mFromRow, ColNetBlock, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColNetBlock)					

                    mFromRow = cntRow + 1
                    '                If mLastRow = True Then Exit Do					
                End If
                mTRNType = mCurrentType
                cntRow = cntRow + 1
            Loop

            mToRow = .MaxRows
            .MaxRows = .MaxRows + 1
            .Row = .MaxRows
            .Action = SS_ACTION_INSERT_ROW

            .Col = ColPartyName
            .Row = .MaxRows
            .Text = "SUB TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColTrnName
            .Text = mTRNType
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80					
            .BlockMode = False

            Call CalcRowTotal(SprdMain, ColTotalCost, mFromRow, ColTotalCost, mToRow, .MaxRows, ColTotalCost)
            Call CalcRowTotal(SprdMain, ColWDV, mFromRow, ColWDV, mToRow, .MaxRows, ColWDV)
            Call CalcRowTotal(SprdMain, ColDeprec1, mFromRow, ColDeprec1, mToRow, .MaxRows, ColDeprec1)
            Call CalcRowTotal(SprdMain, ColCumulativeDeprec, mFromRow, ColCumulativeDeprec, mToRow, .MaxRows, ColCumulativeDeprec)


            Call CalcRowTotal(SprdMain, ColSaleAmount, mFromRow, ColSaleAmount, mToRow, .MaxRows, ColSaleAmount)
            Call CalcRowTotal(SprdMain, ColTotalDeprecClaim, mFromRow, ColTotalDeprecClaim, mToRow, .MaxRows, ColTotalDeprecClaim)

            Call CalcRowTotal(SprdMain, ColGrossBlock, mFromRow, ColGrossBlock, mToRow, .MaxRows, ColGrossBlock)
            Call CalcRowTotal(SprdMain, ColNetBlock, mFromRow, ColNetBlock, mToRow, .MaxRows, ColNetBlock)
            Call CalcRowTotal(SprdMain, ColSelvageAmount, mFromRow, ColSelvageAmount, mToRow, .MaxRows, ColSelvageAmount)

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
            '            .Col = ColWDV					
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
            '        .Col = ColWDV					
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


        cboCompany.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboCompany.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboCompany.Items.Add(RS.Fields("COMPANY_NAME").Value)
                RS.MoveNext()
            Loop
        End If

        '    cboCompany.ListIndex = 1					
        cboCompany.Text = RsCompany.Fields("COMPANY_NAME").Value
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

    Private Sub txtDepreciationDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepreciationDate.TextChanged
        Call PrintStatus(False)
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
        MainClass.SearchGridMaster(txtDeprMode.Text, "AST_DEPRECIATION_MODE_MST", "MODE_CODE", "MODE_DESC", "MODE_TYPE",  , SqlStr)
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

    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtRefNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.DoubleClick
        SearchRefNo()
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRefNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchRefNo()
    End Sub

    Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String

        If Val(txtRefNo.Text) = 0 Then GoTo EventExitSub

        mCompanyName = Trim(cboCompany.Text)
        'UPGRADE_WARNING: Untranslated statement in txtRefNo_Validate. Please check source code.					

        If mCompanyCode = "" Then
            SqlStr = ""
        Else
            SqlStr = "COMPANY_CODE=" & mCompanyCode & ""
            SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        End If

        'UPGRADE_WARNING: Untranslated statement in txtRefNo_Validate. Please check source code.					
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchRefNo()
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String


        mCompanyName = Trim(cboCompany.Text)
        'UPGRADE_WARNING: Untranslated statement in SearchRefNo. Please check source code.					

        If mCompanyCode = "" Then
            SqlStr = ""
        Else
            SqlStr = "COMPANY_CODE=" & mCompanyCode & ""
            SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        End If

        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr					
        MainClass.SearchGridMaster(txtRefNo.Text, "AST_ASSET_TRN", "AUTO_KEY_ASSET", "SUPP_CUST_NAME",  ,  , SqlStr)
        If AcName <> "" Then
            txtRefNo.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class
