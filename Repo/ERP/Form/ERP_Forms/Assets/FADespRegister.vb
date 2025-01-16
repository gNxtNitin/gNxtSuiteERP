Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmFADespRegister
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
    Private Const ColDays As Short = 14
    Private Const ColPurchaseYear As Short = 15
    Private Const ColDeprec1 As Short = 16
    Private Const ColCumulativeDeprec As Short = 17
    Private Const ColSaleAmount As Short = 18
    Private Const ColSaleDate As Short = 19
    Private Const ColTotalDeprecClaim As Short = 20
    Private Const ColPhyDate As Short = 21
    Private Const ColPhyWhom As Short = 22
    Private Const ColGrossBlock As Short = 23
    Private Const ColNetBlock As Short = 24
    Private Const ColItemType As Short = 25
    Private Const ColAssetType As Short = 26

    Private Const ColMKEY As Short = 27
    Dim pAssetOpeningDate As String
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
        'If chkAllDepr.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    txtDeprMode.Enabled = False
        '    cmdsearchDepr.Enabled = False
        'Else
        '    txtDeprMode.Enabled = True
        '    cmdsearchDepr.Enabled = True
        'End If
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
    Private Sub frmFADespRegister_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Fixed Assets Depreciation Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmFADespRegister_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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

        chkAllDepr.Enabled = False
        chkAllDepr.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtDeprMode.Enabled = True
        cmdsearchDepr.Enabled = True

        chkRefNo.CheckState = System.Windows.Forms.CheckState.Checked
        txtRefNo.Enabled = False
        cmdRefNo.Enabled = False



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            pAssetOpeningDate = "31/03/2023"
        Else
            pAssetOpeningDate = "31/03/2023"
        End If
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmFADespRegister_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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
    Private Sub frmFADespRegister_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
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

            For cntCol = ColGrossBlock To ColNetBlock
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
        Dim mPurchaseDate As String
        Dim mDays As Integer
        Dim mDays1 As Integer
        Dim mDays2 As Integer
        Dim mDays3 As Integer
        Dim mDays4 As Integer
        Dim mDeprRate As Double
        Dim mNewDeprRate As Double
        Dim mCompanyCode As Integer
        Dim mCompanyName As String
        Dim pPurchaseYear As Integer
        Dim pTRNType As Double
        Dim pTrnName As String
        Dim pPurchaseAmount As Double
        Dim pSaleAmount As Double
        Dim mDepAmount As Double
        Dim mCummDepAmount As Double
        Dim mFYStartDate As String
        Dim mDepreMode As String
        Dim pCurrentYearDep As Boolean
        Dim mRefNo As Double
        Dim pSaleDesp As Double
        Dim pSaleDate As String
        Dim pIsSale As Boolean
        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim mSaleAmount5 As Double
        Dim mSaleAmount6 As Double
        Dim mSaleDate1 As String
        Dim mSaleDate2 As String
        Dim mSaleDate3 As String
        Dim mSaleDate4 As String
        Dim mSaleDate5 As String
        Dim mSaleDate6 As String
        Dim mGrossBlock As String
        Dim mTempGrossBlock As String
        Dim mNetBlock As String
        Dim mTillDateSaleAmount As String
        Dim pOPGrossBlock As Double
        Dim pDays As Integer
        Dim pNormalDesc As Double
        Dim pABSPurchaseAmount As Double
        Dim pABSGrossAmount As Double

        Dim cntDate As Date
        Dim mAddDays As Integer
        Dim mCalcDepAsOn As String
        Dim mActAddDays As Integer
        Dim mCummDepr As Boolean
        Dim pOPCummDesp As Double
        Dim pOPNetGrossBlock As Double
        Dim pOPNetGrossBlockABS As Double
        Dim pNetGrossBlock As Double
        Dim RsTemp As ADODB.Recordset
        Dim xSaleDateStr As String
        Dim mSqlStr As String

        Dim mPurchaseLife As Double
        Dim mUsefullLife As Double
        Dim mBalUsefullLife As Double
        Dim mNewCummlativeDesp As Double
        Dim xTempOPGrossBlock As Double
        Dim mActualPurchaseAmount As Double
        Dim mNetSaleEffect As Double
        Dim mSelvageValue As Double
        Dim mOPSelvageValue As Double

        UpdateTempTable = False

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
            pIsSale = False
            mDays1 = 0
            mDays2 = 0
            mDays3 = 0
            mDays4 = 0
            mDays = 0
            mSaleAmount1 = 0
            mSaleAmount2 = 0
            mSaleAmount3 = 0
            mSaleAmount4 = 0
            mSaleAmount5 = 0
            mSaleAmount6 = 0

            mSaleDate1 = ""
            mSaleDate2 = ""
            mSaleDate3 = ""
            mSaleDate4 = ""
            mSaleDate5 = ""
            mSaleDate6 = ""

            pDays = 0
            pNormalDesc = 0
            pOPCummDesp = 0
            mCummDepr = False
            pOPNetGrossBlockABS = 0
            pOPNetGrossBlock = 0
            pNetGrossBlock = 0


            pSaleAmount = 0
            pSaleDesp = 0
            pSaleDate = ""
            mCummDepAmount = 0
            mCalcDepAsOn = CStr(0)

            mCompanyName = RsTemp.Fields("COMPANY_NAME").Value
            mCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
            mFYStartDate = VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY")
            mRefNo = RsTemp.Fields("AUTO_KEY_ASSET").Value
            If optDate(1).Checked = True Then
                mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PV_DATE").Value), "", RsTemp.Fields("PV_DATE").Value), "DD/MM/YYYY")
            Else
                mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUT_DATE").Value), "", RsTemp.Fields("PUT_DATE").Value), "DD/MM/YYYY")
            End If

            pPurchaseYear = IIf(IsDBNull(RsTemp.Fields("PUR_YEAR").Value), "0", RsTemp.Fields("PUR_YEAR").Value)
            pTrnName = IIf(IsDBNull(RsTemp.Fields("TRNNAME").Value), "", RsTemp.Fields("TRNNAME").Value)
            pTRNType = IIf(IsDBNull(RsTemp.Fields("TRNCODE").Value), "", RsTemp.Fields("TRNCODE").Value)
            pPurchaseAmount = IIf(IsDBNull(RsTemp.Fields("TOTAL_COST").Value), 0, RsTemp.Fields("TOTAL_COST").Value) ''GetPurchaseAmount(mRefNo, mCompanyCode)	

            mSelvageValue = CDbl(VB6.Format(pPurchaseAmount * 5 / 100, "0"))

            pPurchaseAmount = CDbl(VB6.Format(pPurchaseAmount, "0"))
            pABSPurchaseAmount = System.Math.Abs(pPurchaseAmount)
            mActualPurchaseAmount = CDbl(VB6.Format(pPurchaseAmount, "0"))

            pOPGrossBlock = GetGrossBlock(mRefNo, mCompanyCode, mFYStartDate, pPurchaseAmount)
            pOPGrossBlock = CDbl(VB6.Format(pOPGrossBlock, "0"))
            pABSGrossAmount = System.Math.Abs(pOPGrossBlock)

            mTillDateSaleAmount = CStr(CheckSaleAmount(mRefNo, mCompanyCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mFYStartDate)))))
            If pPurchaseAmount = CDbl(mTillDateSaleAmount) Then
                GoTo NextRec
            End If

            If optShowType(2).Checked = True Then
                If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
                    If CalcSaleAmount(mRefNo, mCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6, "O") = False Then GoTo LedgError
                    If CalcDepreciationAmount(mRefNo, mCompanyCode, mPurchaseDate, pPurchaseYear, pTRNType, pPurchaseAmount, mDepreMode, pCurrentYearDep, pSaleAmount, pSaleDesp, pSaleDate, mCummDepAmount, mCalcDepAsOn, pOPCummDesp, "O", 0) = False Then GoTo LedgError

                    xTempOPGrossBlock = 0
                    If pOPGrossBlock < 0 Then
                        If CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0")) > 0 Then
                            xTempOPGrossBlock = CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0"))
                        End If
                    Else
                        If CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0")) > 0 Then
                            xTempOPGrossBlock = CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0"))
                        End If
                    End If
                End If
            End If

            If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
                If CalcSaleAmount(mRefNo, mCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6, "O") = False Then GoTo LedgError
                If CalcDepreciationAmount(mRefNo, mCompanyCode, mPurchaseDate, pPurchaseYear, pTRNType, pPurchaseAmount, mDepreMode, pCurrentYearDep, pSaleAmount, pSaleDesp, pSaleDate, mCummDepAmount, mCalcDepAsOn, pOPCummDesp, "O", xTempOPGrossBlock) = False Then GoTo LedgError
            End If

            ''01-aug-2013	
            '        If pPurchaseAmount < 0 Then	
            '            If Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0") > 0 Then	
            '                pOPNetGrossBlock = 0	
            '            Else	
            '                pOPNetGrossBlock = Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0")	
            '            End If	
            '        Else	
            '            If Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0") <= 0 Then	
            '                pOPNetGrossBlock = 0	
            '            Else	
            '                pOPNetGrossBlock = Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0")	
            '            End If	
            '        End If	

            If pOPGrossBlock < 0 Then
                mActualPurchaseAmount = mActualPurchaseAmount - mCummDepAmount
                If CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0")) > 0 Then
                    pOPNetGrossBlock = 0
                Else
                    pOPNetGrossBlock = CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0"))
                End If
            Else
                If CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0")) <= 0 Then
                    pOPNetGrossBlock = 0
                Else
                    pOPNetGrossBlock = CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0"))
                End If
            End If

            mDays1 = 0
            mDays2 = 0
            mDays3 = 0
            mDays4 = 0
            mDays = 0
            mSaleAmount1 = 0
            mSaleAmount2 = 0
            mSaleAmount3 = 0
            mSaleAmount4 = 0
            mSaleAmount5 = 0
            mSaleAmount6 = 0

            mSaleDate1 = ""
            mSaleDate2 = ""
            mSaleDate3 = ""
            mSaleDate4 = ""
            mSaleDate5 = ""
            mSaleDate6 = ""

            pSaleAmount = 0
            pSaleDesp = 0
            pSaleDate = ""
            mCummDepAmount = 0
            mCalcDepAsOn = CStr(0)
            pOPCummDesp = 0

            ''For the Year	
            If CalcSaleAmount(mRefNo, mCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6, "") = False Then GoTo LedgError
            If CalcDepreciationAmount(mRefNo, mCompanyCode, mPurchaseDate, pPurchaseYear, pTRNType, pPurchaseAmount, mDepreMode, pCurrentYearDep, pSaleAmount, pSaleDesp, pSaleDate, mCummDepAmount, mCalcDepAsOn, pOPCummDesp, "", pOPGrossBlock) = False Then GoTo LedgError
            mOPSelvageValue = 0
            If optShowType(2).Checked = True Then
                ''14-06-2015	
                If pOPNetGrossBlock <= 0 Then
                    pOPNetGrossBlock = IIf(pOPNetGrossBlock < 0, 0, pOPNetGrossBlock)
                Else
                    pOPNetGrossBlock = pOPNetGrossBlock '- mSelvageValue  ''01072015	
                End If

                '            If pOPNetGrossBlock < 0 Then ''01072015	
                '                mOPSelvageValue = mSelvageValue	
                '            End If	


                pOPGrossBlock = pOPNetGrossBlock
                pPurchaseAmount = pOPNetGrossBlock ''IIf(IsNull(RsTemp!TOTAL_COST), 0, RsTemp!TOTAL_COST)	
                pPurchaseAmount = CDbl(VB6.Format(pPurchaseAmount, "0"))
                pABSPurchaseAmount = System.Math.Abs(pPurchaseAmount)
            End If

            '        .Col = ColCumulativeDeprec	
            If optShowType(2).Checked = True Then

            Else
                If System.Math.Abs(pOPGrossBlock) < System.Math.Abs(mCummDepAmount) Then

                    mCummDepAmount = pOPGrossBlock
                    If System.Math.Abs(pSaleDesp) > System.Math.Abs(pOPGrossBlock) Then
                        pSaleDesp = pOPGrossBlock
                    End If
                End If
            End If

            '        .Text = Format(mCummDepAmount, "0")	


            If CDate(mFYStartDate) > CDate(mPurchaseDate) Then
                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mFYStartDate), CDate(mDeprecAsOn)) + 1 ''mDeprecAsOn	
                mDays = mDays - GetLeapYear(mFYStartDate, mDeprecAsOn) ' mDeprecAsOn)	
            Else
                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mDeprecAsOn)) + 1 ''mDeprecAsOn	
                mDays = mDays - GetLeapYear(mPurchaseDate, mDeprecAsOn) ' mDeprecAsOn)	
            End If

            mDays = mDays - mAddDays

            If pOPGrossBlock = 0 Or pOPNetGrossBlock = 0 Then
                mDays = 0
            End If


            mActAddDays = mDays

            mDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, IIf(optShowType(0).Checked Or optShowType(2).Checked, "O", "N"))

            '        .Col = ColDeprec1	
            If optShowType(2).Checked = True Then

                If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
                    mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
                    mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
                    mBalUsefullLife = mUsefullLife - mPurchaseLife
                    If mBalUsefullLife <= 0 Then
                        mDepAmount = pOPGrossBlock '0	
                    Else
                        If mBalUsefullLife >= 365 Then
                            mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) * mDays / mBalUsefullLife
                        Else
                            mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) ''* mDays / mBalUsefullLife	
                        End If
                    End If
                    mNewCummlativeDesp = mCummDepAmount + mDepAmount
                Else
                    mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(txtDepreciationDate.Text)) + 1
                    mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
                    mBalUsefullLife = mUsefullLife ''- mPurchaseLife	
                    If mBalUsefullLife <= 0 Then
                        mDepAmount = pOPGrossBlock '0	
                    Else
                        If mBalUsefullLife >= 365 Then
                            mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) * mDays / mBalUsefullLife
                        Else
                            mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) ''* mDays / mBalUsefullLife	
                        End If
                    End If
                    mNewCummlativeDesp = mDepAmount

                    '                mDepAmount = mCummDepAmount	
                    '                mNewCummlativeDesp = mCummDepAmount	
                    '                mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")	
                    '                mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) * mNewDeprRate * 0.01 * mDays / 365	
                    '                mNewCummlativeDesp = mCummDepAmount + mDepAmount	
                End If
            Else
                mDepAmount = (pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) * mDeprRate * 0.01 * mDays / 365
            End If
            '            mDepAmount = (pOPGrossBlock - pSaleAmount) * mDeprRate * 0.01 * mDays / 365	

            If mSaleAmount1 <> 0 Then
                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate1)) + 1
                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate1)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate1)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate1)
                End If
                If optShowType(2).Checked = True Then
                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
                        mBalUsefullLife = mUsefullLife - mPurchaseLife
                        If mBalUsefullLife <= 0 Then
                            mDepAmount = 0
                        Else
                            mDepAmount = mDepAmount + (mSaleAmount1) * mDays / mBalUsefullLife
                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
                        End If
                    Else
                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
                        mDepAmount = mDepAmount + (mSaleAmount1 * mNewDeprRate * 0.01 * mDays / 365)
                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
                    End If
                Else
                    mDepAmount = mDepAmount + (mSaleAmount1 * mDeprRate * 0.01 * mDays / 365)
                End If
                If pOPGrossBlock - mSaleAmount1 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If mSaleAmount2 <> 0 Then
                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate2)) + 1
                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate2)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate2)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate2)
                End If
                If optShowType(2).Checked = True Then
                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
                        mBalUsefullLife = mUsefullLife - mPurchaseLife
                        If mBalUsefullLife <= 0 Then
                            mDepAmount = 0
                        Else
                            mDepAmount = mDepAmount + (mSaleAmount2) * mDays / mBalUsefullLife
                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
                        End If
                    Else
                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
                        mDepAmount = mDepAmount + (mSaleAmount2 * mNewDeprRate * 0.01 * mDays / 365)
                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
                    End If
                Else
                    mDepAmount = mDepAmount + (mSaleAmount2 * mDeprRate * 0.01 * mDays / 365)
                End If
                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If mSaleAmount3 <> 0 Then
                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate3)) + 1
                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate3)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate3)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate3)
                End If
                If optShowType(2).Checked = True Then
                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
                        mBalUsefullLife = mUsefullLife - mPurchaseLife
                        If mBalUsefullLife <= 0 Then
                            mDepAmount = 0
                        Else
                            mDepAmount = mDepAmount + (mSaleAmount3) * mDays / mBalUsefullLife
                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
                        End If
                    Else
                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
                        mDepAmount = mDepAmount + (mSaleAmount3 * mNewDeprRate * 0.01 * mDays / 365)
                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
                    End If
                Else
                    mDepAmount = mDepAmount + (mSaleAmount3 * mDeprRate * 0.01 * mDays / 365)
                End If
                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If mSaleAmount4 <> 0 Then
                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate4)) + 1
                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate4)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate4)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate4)
                End If
                If optShowType(2).Checked = True Then
                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
                        mBalUsefullLife = mUsefullLife - mPurchaseLife
                        If mBalUsefullLife <= 0 Then
                            mDepAmount = 0
                        Else
                            mDepAmount = mDepAmount + (mSaleAmount4) * mDays / mBalUsefullLife
                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
                        End If
                    Else
                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
                        mDepAmount = mDepAmount + (mSaleAmount4 * mNewDeprRate * 0.01 * mDays / 365)
                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
                    End If
                Else
                    mDepAmount = mDepAmount + (mSaleAmount4 * mDeprRate * 0.01 * mDays / 365)
                End If
                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If mSaleAmount5 <> 0 Then
                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate5)) + 1
                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate5)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate5)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate5)
                End If
                If optShowType(2).Checked = True Then
                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
                        mBalUsefullLife = mUsefullLife - mPurchaseLife
                        If mBalUsefullLife <= 0 Then
                            mDepAmount = 0
                        Else
                            mDepAmount = mDepAmount + (mSaleAmount5) * mDays / mBalUsefullLife
                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
                        End If
                    Else
                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
                        mDepAmount = mDepAmount + (mSaleAmount5 * mNewDeprRate * 0.01 * mDays / 365)
                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
                    End If
                Else
                    mDepAmount = mDepAmount + (mSaleAmount5 * mDeprRate * 0.01 * mDays / 365)
                End If
                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If mSaleAmount6 <> 0 Then
                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate6)) + 1
                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate6)
                Else
                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate6)) + 1
                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate6)
                End If
                If optShowType(2).Checked = True Then
                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
                        mBalUsefullLife = mUsefullLife - mPurchaseLife
                        If mBalUsefullLife <= 0 Then
                            mDepAmount = 0
                        Else
                            mDepAmount = mDepAmount + (mSaleAmount6) * mDays / mBalUsefullLife
                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
                        End If
                    Else
                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
                        mDepAmount = mDepAmount + (mSaleAmount6 * mNewDeprRate * 0.01 * mDays / 365)
                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
                    End If
                Else
                    mDepAmount = mDepAmount + (mSaleAmount6 * mDeprRate * 0.01 * mDays / 365)
                End If
                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6 = 0 Then
                    mActAddDays = mDays
                End If
            End If

            If System.Math.Abs(mDepAmount) > System.Math.Abs(pOPNetGrossBlock) Then
                mDepAmount = pOPNetGrossBlock
            End If

            mDepAmount = CDbl(VB6.Format(mDepAmount, "0"))
            '	
            '        .Col = ColDays	
            mActAddDays = CInt(VB6.Format(mActAddDays, "0"))
            '	
            '        .Col = ColSaleAmount	
            '        .Text = Format((mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4), "0")	
            '	
            '        .Col = ColSaleDate	

            xSaleDateStr = mSaleDate1 & IIf(mSaleDate2 = "", "", "," & mSaleDate2) & IIf(mSaleDate3 = "", "", "," & mSaleDate3) & IIf(mSaleDate4 = "", "", "," & mSaleDate4) & IIf(mSaleDate5 = "", "", "," & mSaleDate5) & IIf(mSaleDate6 = "", "", "," & mSaleDate6)
            '        .Text = mSaleDate1 & IIf(mSaleDate2 = "", "", "," & mSaleDate2) & IIf(mSaleDate3 = "", "", "," & mSaleDate3) & IIf(mSaleDate4 = "", "", "," & mSaleDate4)	
            '	
            '        .Col = ColTotalDeprecClaim	
            '        .Text = Format(pSaleDesp, "0")	
            '	
            '        .Col = ColGrossBlock	
            If optShowType(2).Checked = True Then
                mGrossBlock = CStr(GetGrossBlock(mRefNo, mCompanyCode, mDeprecAsOn, pPurchaseAmount))
            Else
                mGrossBlock = CStr(GetGrossBlock(mRefNo, mCompanyCode, mDeprecAsOn, pPurchaseAmount))
            End If
            mGrossBlock = VB6.Format(mGrossBlock, CStr(0))
            '            If mGrossBlock <= 100 Then	
            '                mGrossBlock = 0	
            '            End If	
            '        .Text = Format(mGrossBlock, "0")	
            '	
            '	
            '        .Col = ColNetBlock	
            If optShowType(2).Checked = True Then
                mCummDepAmount = CDbl(VB6.Format(mNewCummlativeDesp, "0"))

                If (mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6) = 0 Then
                    mNetSaleEffect = mCummDepAmount
                Else
                    mNetSaleEffect = 0 '' (mCummDepAmount)  '' - mDepAmount)	
                End If

                mTempGrossBlock = IIf(CDbl(mGrossBlock) <= 0, 0, IIf(CDbl(mGrossBlock) < mActualPurchaseAmount, mGrossBlock, mActualPurchaseAmount))
                If pPurchaseAmount < 0 Then
                    If CDbl(VB6.Format(mActualPurchaseAmount - mNetSaleEffect, "0")) > 0 Then
                        pNetGrossBlock = 0
                    Else
                        pNetGrossBlock = CDbl(VB6.Format(mActualPurchaseAmount - mNetSaleEffect, "0"))
                    End If
                Else
                    If CDbl(VB6.Format(mActualPurchaseAmount - mNetSaleEffect, "0")) <= 0 Then
                        pNetGrossBlock = 0
                    Else
                        pNetGrossBlock = CDbl(VB6.Format(mActualPurchaseAmount - mNetSaleEffect, "0"))
                    End If
                End If

                If pPurchaseAmount - (mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6) <= 0 Then
                    pNetGrossBlock = 0
                Else
                    pNetGrossBlock = pNetGrossBlock - CDbl(VB6.Format(mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6, "0"))
                End If
                If pOPGrossBlock = 0 Then pNetGrossBlock = 0
                pNetGrossBlock = pNetGrossBlock + mOPSelvageValue
            Else
                mTempGrossBlock = mGrossBlock

                If pPurchaseAmount < 0 Then
                    If CDbl(VB6.Format(CDbl(mTempGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0")) > 0 Then
                        pNetGrossBlock = 0
                    Else
                        pNetGrossBlock = CDbl(VB6.Format(CDbl(mTempGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0"))
                    End If
                Else
                    If CDbl(VB6.Format(CDbl(mTempGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0")) <= 0 Then
                        pNetGrossBlock = 0
                    Else
                        pNetGrossBlock = CDbl(VB6.Format(CDbl(mTempGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0"))
                    End If
                End If
            End If



            mSqlStr = "UPDATE TEMP_AST_DESP_TRN SET " & vbCrLf & " DAYS=" & mActAddDays & "," & vbCrLf & " CURRENT_DESP=" & mDepAmount & "," & vbCrLf & " CUMULATIVE_DESP=" & mCummDepAmount & "," & vbCrLf & " SALE_AMOUNT=" & VB6.Format(mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6, "0") & "," & vbCrLf & " SALE_DATE='" & xSaleDateStr & "'," & vbCrLf & " SALE_DESP=" & pSaleDesp & "," & vbCrLf & " GROSS_BLOCK=" & mGrossBlock & "," & vbCrLf & " NET_BLOCK=" & pNetGrossBlock & "," & vbCrLf & " OP_GROSS_BLOCK=" & pOPGrossBlock & ""

            mSqlStr = mSqlStr & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & mRefNo & ""

            PubDBCn.Execute(mSqlStr)

NextRec:

            pSaleAmount = 0
            pSaleDesp = 0
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
            & " ITEM_TYPE, ASSET_TYPE,OP_GROSS_BLOCK)"


        SqlStr = " SELECT  '" & MainClass.AllowSingleQuote(PubUserID) & "', TRN.COMPANY_CODE, GEN.COMPANY_NAME," & vbCrLf & " TRN.FYEAR, TRN.BOOKTYPE, '1'," & vbCrLf & " INVMST.CODE, INVMST.NAME, TRN.SUPP_CUST_NAME, TRN.ITEM_DESC, " & vbCrLf & " TRN.AUTO_KEY_ASSET, TRN.LOCATION, TRN.PV_DATE, TRN.PUT_DATE," & vbCrLf & " TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT), 0, TRN.FYEAR," & vbCrLf & " 0, 0, 0, " & vbCrLf & " '', 0, ''," & vbCrLf & " '', 0, 0, " & vbCrLf & " TRN.ITEM_TYPE, TRN.AST_TYPE,0"

        SqlStr = SqlStr & vbCrLf & " FROM AST_ASSET_TRN TRN, FIN_INVTYPE_MST INVMST, GEN_COMPANY_MST GEN"

        ''''WHERE CLAUSE...	
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND TRN.GROUP_CODE=INVMST.CODE"

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

            'If MainClass.ValidateWithMasterTable(mAccountCode, "ACCOUNTPOSTCODE", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mGroupCode = IIf(IsDBNull(MasterNo), "", MasterNo)
            'End If
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ACCOUNTPOSTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
        End If

        If optOption(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ABS(GETASSETSALEVALUE(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,TO_DATE('" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + " & vbCrLf & " GETASSETSALEVALUE1(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE2(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE3(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE4(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE5(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') ) " & vbCrLf & " - ABS(TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT+DN_CR_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT))<>0"
        ElseIf optOption(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ABS(GETASSETSALEVALUE(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,TO_DATE('" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) + " & vbCrLf & " GETASSETSALEVALUE1(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE2(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE3(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE4(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') + " & vbCrLf & " GETASSETSALEVALUE5(TRN.COMPANY_CODE,TRN.AUTO_KEY_ASSET,'" & VB6.Format(lblFYStartDate.Text, "DD-MMM-YYYY") & "') ) " & vbCrLf & " = ABS(TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT+DN_CR_AMOUNT-(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT))"
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



        '    If optShow(1).Value = True Then	
        '        SqlStr = SqlStr & vbCrLf & " GROUP BY GEN.COMPANY_NAME,INVMST.NAME"	
        '    ElseIf optShow(2).Value = True Then	
        '        SqlStr = SqlStr & vbCrLf & " GROUP BY INVMST.NAME"	
        '    ElseIf optShow(3).Value = True Then	
        '        SqlStr = SqlStr & vbCrLf & " GROUP BY GEN.COMPANY_NAME,INVMST.NAME,TRN.FYEAR"	
        '    End If	
        '	
        '    If optShow(2).Value = True Then	
        '        SqlStr = SqlStr & vbCrLf & " ORDER BY INVMST.NAME"	
        '    ElseIf optShow(3).Value = True Then	
        '        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.FYEAR,GEN.COMPANY_NAME,INVMST.NAME"	
        '    ElseIf optShow(1).Value = True Then	
        '        SqlStr = SqlStr & vbCrLf & " ORDER BY GEN.COMPANY_NAME,INVMST.NAME"	
        '    Else	
        If optDate(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY GEN.COMPANY_NAME,INVMST.NAME,TRN.FYEAR,TRN.PV_DATE,TRN.PV_NO"
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY GEN.COMPANY_NAME,INVMST.NAME,TRN.FYEAR,TRN.PUT_DATE,TRN.PV_NO"
        End If
        '    End If	

        mSqlStr = mSqlStr & vbCrLf & SqlStr
        PubDBCn.Execute(mSqlStr)

        ''If UpdateTempTable() = False Then GoTo LedgError
        If UpdateTempTableNew() = False Then GoTo LedgError

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
            If MainClass.LastDay(Month(cntDate), Year(cntDate)) = 29 Then
                GetLeapYear = GetLeapYear + 1
            End If
            cntDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, cntDate)
        Loop
        Exit Function
LedgError:
        '    Resume	
        GetLeapYear = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CalcDepreciationAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef pPurchaseDate As String, ByRef pPurchaseYear As Integer, ByRef pTRNType As Double, ByRef pPurchaseAmount As Double, ByRef pModCode As String, ByRef pCurrentYearDep As Boolean, ByRef pSaleAmount As Double, ByRef pSaleDesp As Double, ByRef pSaleDate As String, ByRef mTotalDepAmount As Double, ByRef mCalcDepAsOn As String, ByRef mOPCummDesp As Double, ByRef pIsOpening As String, ByRef pOPGrossBlock As Double) As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDays As Integer
        Dim mDepRate As Double
        Dim mNewDepRate As Double
        Dim mDepAmount As Double
        'Dim mTotalDepAmount As Double	
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mAsOnDate As String
        Dim mABSPurchaseAmount As Double

        Dim pCurrentYear As Integer
        Dim pCheckCurrentYear As Integer
        Dim mDescpCalcOn As Double
        Dim mSaleValue As Double
        Dim mSaleDesp As Double
        Dim mcntType As Integer
        Dim mSaleDate As String
        Dim mSaleDate1 As String
        Dim mSaleDate2 As String
        Dim mSaleDate3 As String
        Dim mSaleDate4 As String
        Dim mSaleDate5 As String
        Dim mSaleDate6 As String

        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim mSaleAmount5 As Double
        Dim mSaleAmount6 As Double

        Dim pSaleOPDesp As Double
        Dim cntDate As Integer
        Dim mAddDays As Integer
        Dim mYearDay As Integer

        Dim mDays1 As Integer
        Dim mDays2 As Integer
        Dim mDays3 As Integer
        Dim mDays4 As Integer
        Dim mDays5 As Integer
        Dim mDays6 As Integer
        Dim mLastFYEndDate As String
        Dim mTable As String

        Dim mSaleAsOn As String
        Dim mNewDate As String


        Dim mPurchaseLife As Double
        Dim mUsefullLife As Double
        Dim mBalUsefullLife As Double

        mTotalDepAmount = 0
        mOPCummDesp = 0
        mNewDepRate = 0
        mNewDate = pAssetOpeningDate

        If optShowType(2).Checked = True Then

            SqlStr = "SELECT FYEAR, DEPR_RATE " & vbCrLf & " FROM AST_DEPRECIATION_NEW_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND GROUP_CODE=" & pTRNType & ""

            If optShowType(0).Checked = True Or optShowType(2).Checked Then
                SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & lblCurrentFyear.Text & ""
            End If

            SqlStr = SqlStr & vbCrLf & " AND MODE_CODE='" & pModCode & "'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY FYEAR"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mNewDepRate = IIf(IsDBNull(RsTemp.Fields("DEPR_RATE").Value), 0, RsTemp.Fields("DEPR_RATE").Value)
            End If

        End If

        If optShowType(0).Checked = True Or optShowType(2).Checked = True Then
            mTable = "AST_DEPRECIATION_NEW_MST" '"AST_DEPRECIATION_MST"
        Else
            mTable = "AST_DEPRECIATION_NEW_MST"
        End If

        SqlStr = "SELECT FYEAR, DEPR_RATE " & vbCrLf & " FROM " & mTable & " " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND GROUP_CODE=" & pTRNType & ""

        If optShowType(0).Checked = True Or optShowType(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & lblCurrentFyear.Text & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND MODE_CODE='" & pModCode & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY FYEAR"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mStartDate = pPurchaseDate
        If CDate(pPurchaseDate) <= CDate(lblFYStartDate.Text) Then
            mStartDate = lblFYStartDate.Text
        Else
            mStartDate = pPurchaseDate
        End If
        If pIsOpening = "O" Then
            If optShowType(2).Checked Then
                mAsOnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY"))))
                If CDate(mAsOnDate) > CDate(mNewDate) Then
                    mAsOnDate = mNewDate
                End If
                mCalcDepAsOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY"))))
                If CDate(mCalcDepAsOn) > CDate(mNewDate) Then
                    mCalcDepAsOn = mNewDate
                End If


            Else
                mAsOnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY"))))
                mCalcDepAsOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY"))))
            End If
            mSaleAsOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY"))))
        Else
            If optShowType(2).Checked Then
                If CDate(pPurchaseDate) > CDate(mNewDate) Then
                    mAsOnDate = VB6.Format(txtDepreciationDate.Text, "DD/MM/YYYY")
                    mCalcDepAsOn = VB6.Format(txtDepreciationDate.Text, "DD/MM/YYYY")
                Else
                    mAsOnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY"))))
                    If CDate(mAsOnDate) > CDate(mNewDate) Then
                        mAsOnDate = mNewDate
                    End If

                    mCalcDepAsOn = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY"))))
                    If CDate(mCalcDepAsOn) > CDate(mNewDate) Then
                        mCalcDepAsOn = mNewDate
                    End If
                End If
            Else
                mAsOnDate = VB6.Format(txtDepreciationDate.Text, "DD/MM/YYYY")
                mCalcDepAsOn = VB6.Format(txtDepreciationDate.Text, "DD/MM/YYYY")
            End If
            mSaleAsOn = VB6.Format(txtDepreciationDate.Text, "DD/MM/YYYY")
        End If
        mABSPurchaseAmount = System.Math.Abs(pPurchaseAmount)
        pCurrentYearDep = False
        mDescpCalcOn = mABSPurchaseAmount
        If RsTemp.EOF = False Then
            mDepRate = IIf(IsDBNull(RsTemp.Fields("DEPR_RATE").Value), 0, RsTemp.Fields("DEPR_RATE").Value)
            mSaleAmount1 = 0
            mSaleAmount2 = 0
            mSaleAmount3 = 0
            mSaleAmount4 = 0
            mSaleAmount5 = 0
            mSaleAmount6 = 0

            mSaleDate1 = ""
            mSaleDate2 = ""
            mSaleDate3 = ""
            mSaleDate4 = ""
            mSaleDate5 = ""
            mSaleDate6 = ""

            pSaleDesp = 0
            mDepAmount = 0
            If CheckSaleAmount(pRefNo, pCompanyCode, mSaleAsOn) = 0 Then
                mAddDays = 0
                mAddDays = GetLeapYear(mStartDate, mAsOnDate)
                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mAsOnDate)) + 1 - mAddDays
                mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))


                If mDepAmount > mDescpCalcOn Then
                    mDepAmount = mDescpCalcOn
                End If
                mTotalDepAmount = mTotalDepAmount + mDepAmount

            Else
                ''mStartDate  ''31/07/2013	
                '            If CalcSaleAmount(pRefNo, pCompanyCode, mStartDate, mAsOnDate, mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, pIsOpening) = False Then GoTo LedgError	
                If CalcSaleAmount(pRefNo, pCompanyCode, IIf(pIsOpening = "O", lblFYStartDate.Text, mStartDate), mSaleAsOn, mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6, pIsOpening) = False Then GoTo LedgError

                mSaleAmount1 = System.Math.Abs(mSaleAmount1)
                mSaleAmount2 = System.Math.Abs(mSaleAmount2)
                mSaleAmount3 = System.Math.Abs(mSaleAmount3)
                mSaleAmount4 = System.Math.Abs(mSaleAmount4)
                mSaleAmount5 = System.Math.Abs(mSaleAmount5)
                mSaleAmount6 = System.Math.Abs(mSaleAmount6)

                If mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6 <> 0 Then
                    If mSaleDate1 <> "" Then
                        mAddDays = 0


                        If optShowType(2).Checked = True Then ''26-06-2015 And CDate(mSaleDate1) > CDate(mNewDate)	
                            mAddDays = GetLeapYear(mStartDate, mNewDate)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mNewDate)) + 1 - mAddDays
                            mDays1 = mDays

                            mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                            pSaleDesp = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))

                            mAddDays = GetLeapYear(mNewDate, mSaleDate1)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mNewDate), CDate(mSaleDate1)) - mAddDays
                            mDays1 = mDays


                            mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mNewDate)) + 1
                            mUsefullLife = GetUsefullLife(pCompanyCode, pTRNType)
                            mBalUsefullLife = mUsefullLife - mPurchaseLife
                            '	
                            '                        mDepAmount = mDepAmount + Format(((mDescpCalcOn) * mDays / mBalUsefullLife), "0") ''-mDepAmount	
                            mTotalDepAmount = mTotalDepAmount + mDepAmount



                            If CDate(mSaleDate1) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate1) <= CDate(lblFYEndDate.Text) Then
                                pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount1 * mDays / mBalUsefullLife, "0")) '' * mNewDepRate * mDays * 0.01 / 365, "0")	
                                pSaleAmount = pSaleAmount + mSaleAmount1
                            Else
                                mAddDays = GetLeapYear(mStartDate, mSaleDate1)
                                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate1)) + 1 - mAddDays
                                mDays1 = mDays
                                pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount1 * mDepRate * mDays * 0.01 / 365, "0"))
                            End If
                            mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate1)))
                            mDescpCalcOn = mDescpCalcOn - mSaleAmount1
                            mCalcDepAsOn = mSaleDate1

                        Else
                            mAddDays = GetLeapYear(mStartDate, mSaleDate1)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate1)) + 1 - mAddDays
                            mDays1 = mDays
                            mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))


                            mTotalDepAmount = mTotalDepAmount + mDepAmount
                            mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate1)))
                            mDescpCalcOn = mDescpCalcOn - mSaleAmount1

                            If CDate(mSaleDate1) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate1) <= CDate(lblFYEndDate.Text) Then
                                pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount1 * mDepRate * mDays * 0.01 / 365, "0"))
                                pSaleAmount = pSaleAmount + mSaleAmount1
                            Else
                                pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount1 * mDepRate * mDays * 0.01 / 365, "0"))
                            End If
                            mCalcDepAsOn = mSaleDate1
                        End If
                    End If
                    If mSaleDate2 <> "" Then
                        If optShowType(2).Checked Then
                            mAddDays = GetLeapYear(mStartDate, mNewDate)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mNewDate)) + 1 - mAddDays
                            mDays1 = mDays

                            mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                            pSaleDesp = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))

                            mAddDays = GetLeapYear(mNewDate, mSaleDate2)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mNewDate), CDate(mSaleDate2)) - mAddDays
                            mDays2 = mDays


                            mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mNewDate)) + 1
                            mUsefullLife = GetUsefullLife(pCompanyCode, pTRNType)
                            mBalUsefullLife = mUsefullLife - mPurchaseLife
                            '	
                            '                        mDepAmount = mDepAmount + Format(((mDescpCalcOn) * mDays / mBalUsefullLife), "0") ''-mDepAmount	
                            mTotalDepAmount = mTotalDepAmount + mDepAmount



                            If CDate(mSaleDate2) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate2) <= CDate(lblFYEndDate.Text) Then
                                pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount2 * mDays / mBalUsefullLife, "0")) '' * mNewDepRate * mDays * 0.01 / 365, "0")	
                                pSaleAmount = pSaleAmount + mSaleAmount2
                            Else
                                mAddDays = GetLeapYear(mStartDate, mSaleDate2)
                                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate2)) + 1 - mAddDays
                                mDays2 = mDays
                                pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount2 * mDepRate * mDays * 0.01 / 365, "0"))
                            End If
                            mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate2)))
                            mDescpCalcOn = mDescpCalcOn - mSaleAmount2
                            mCalcDepAsOn = mSaleDate2
                        Else
                            mAddDays = 0
                            mAddDays = GetLeapYear(mStartDate, mSaleDate2)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate2)) + 1 - mAddDays
                            mDays2 = mDays
                            mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                            mTotalDepAmount = mTotalDepAmount + mDepAmount
                            mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate2)))
                            mDescpCalcOn = mDescpCalcOn - mSaleAmount2
                            If CDate(mSaleDate2) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate2) <= CDate(lblFYEndDate.Text) Then
                                pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount2 * mDepRate * (mDays + mDays1) * 0.01 / 365, "0")) ''+ mDays1	
                                pSaleAmount = pSaleAmount + mSaleAmount2
                            Else
                                pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount2 * mDepRate * (mDays + mDays1) * 0.01 / 365, "0")) ''mDays ''+ mDays1	
                            End If
                            mCalcDepAsOn = mSaleDate2
                        End If
                    End If
                    If mSaleDate3 <> "" Then
                        If optShowType(2).Checked Then
                            mAddDays = GetLeapYear(mStartDate, mNewDate)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mNewDate)) + 1 - mAddDays
                            mDays1 = mDays

                            mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                            pSaleDesp = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))

                            mAddDays = GetLeapYear(mNewDate, mSaleDate3)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mNewDate), CDate(mSaleDate3)) - mAddDays
                            mDays3 = mDays


                            mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mNewDate)) + 1
                            mUsefullLife = GetUsefullLife(pCompanyCode, pTRNType)
                            mBalUsefullLife = mUsefullLife - mPurchaseLife
                            '	
                            '                        mDepAmount = mDepAmount + Format(((mDescpCalcOn) * mDays / mBalUsefullLife), "0") ''-mDepAmount	
                            mTotalDepAmount = mTotalDepAmount + mDepAmount



                            If CDate(mSaleDate3) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate3) <= CDate(lblFYEndDate.Text) Then
                                pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount3 * mDays / mBalUsefullLife, "0")) '' * mNewDepRate * mDays * 0.01 / 365, "0")	
                                pSaleAmount = pSaleAmount + mSaleAmount3
                            Else
                                mAddDays = GetLeapYear(mStartDate, mSaleDate3)
                                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate3)) + 1 - mAddDays
                                mDays3 = mDays
                                pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount3 * mDepRate * mDays * 0.01 / 365, "0"))
                            End If
                            mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate3)))
                            mDescpCalcOn = mDescpCalcOn - mSaleAmount3
                            mCalcDepAsOn = mSaleDate3
                        Else
                            mAddDays = 0
                            mAddDays = GetLeapYear(mStartDate, mSaleDate3)
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate3)) + 1 - mAddDays
                            mDays3 = mDays
                            mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * (mDays) * 0.01 / 365, "0"))
                            mTotalDepAmount = mTotalDepAmount + mDepAmount
                            mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate3))) '''Nitin 13/07/2012    'mSaleDate3 start from next sale date	
                            mDescpCalcOn = mDescpCalcOn - mSaleAmount3
                            If CDate(mSaleDate3) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate3) <= CDate(lblFYEndDate.Text) Then
                                pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount3 * mDepRate * (mDays + mDays1 + mDays2) * 0.01 / 365, "0")) ''+ mDays1 + mDays2	
                                pSaleAmount = pSaleAmount + mSaleAmount3
                            Else
                                pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount3 * mDepRate * (mDays + mDays1 + mDays2) * 0.01 / 365, "0")) ' mDays ''+ mDays1 + mDays2	
                            End If
                            mCalcDepAsOn = mSaleDate3
                        End If
                    End If
                    If mSaleDate4 <> "" Then
                        mAddDays = 0
                        mAddDays = GetLeapYear(mStartDate, mSaleDate4)
                        mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate4)) + 1 - mAddDays
                        mDays4 = mDays
                        mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                        mTotalDepAmount = mTotalDepAmount + mDepAmount
                        mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate4))) '''Nitin 13/07/2012   'mSaleDate4 start from next sale date	
                        mDescpCalcOn = mDescpCalcOn - mSaleAmount4
                        If CDate(mSaleDate4) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate4) <= CDate(lblFYEndDate.Text) Then
                            pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount4 * mDepRate * (mDays + mDays1 + mDays2 + mDays3) * 0.01 / 365, "0")) ''+ mDays1 + mDays2 + mDays3	
                            pSaleAmount = pSaleAmount + mSaleAmount4
                        Else
                            pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount4 * mDepRate * (mDays + mDays1 + mDays2 + mDays3) * 0.01 / 365, "0")) 'mDays '' + mDays1 + mDays2 + mDays3	
                        End If
                        mCalcDepAsOn = mSaleDate4
                    End If

                    If mSaleDate5 <> "" Then
                        mAddDays = 0
                        mAddDays = GetLeapYear(mStartDate, mSaleDate5)
                        mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate5)) + 1 - mAddDays
                        mDays5 = mDays
                        mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                        mTotalDepAmount = mTotalDepAmount + mDepAmount
                        mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate5))) '''Nitin 13/07/2012   'mSaleDate4 start from next sale date	
                        mDescpCalcOn = mDescpCalcOn - mSaleAmount5
                        If CDate(mSaleDate5) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate5) <= CDate(lblFYEndDate.Text) Then
                            pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount5 * mDepRate * (mDays + mDays1 + mDays2 + mDays3 + mDays4) * 0.01 / 365, "0")) ''+ mDays1 + mDays2 + mDays3	
                            pSaleAmount = pSaleAmount + mSaleAmount5
                        Else
                            pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount5 * mDepRate * (mDays + mDays1 + mDays2 + mDays3 + mDays4) * 0.01 / 365, "0")) 'mDays '' + mDays1 + mDays2 + mDays3	
                        End If
                        mCalcDepAsOn = mSaleDate5
                    End If

                    If mSaleDate6 <> "" Then
                        mAddDays = 0
                        mAddDays = GetLeapYear(mStartDate, mSaleDate6)
                        mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mSaleDate6)) + 1 - mAddDays
                        mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))
                        mTotalDepAmount = mTotalDepAmount + mDepAmount
                        mStartDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSaleDate6))) '''Nitin 13/07/2012   'mSaleDate4 start from next sale date	
                        mDescpCalcOn = mDescpCalcOn - mSaleAmount6
                        If CDate(mSaleDate6) >= CDate(lblFYStartDate.Text) And CDate(mSaleDate6) <= CDate(lblFYEndDate.Text) Then
                            pSaleDesp = pSaleDesp + CDbl(VB6.Format(mSaleAmount6 * mDepRate * (mDays + mDays1 + mDays2 + mDays3 + mDays4 + mDays5) * 0.01 / 365, "0")) ''+ mDays1 + mDays2 + mDays3	
                            pSaleAmount = pSaleAmount + mSaleAmount6
                        Else
                            pSaleOPDesp = pSaleOPDesp + CDbl(VB6.Format(mSaleAmount6 * mDepRate * (mDays + mDays1 + mDays2 + mDays3 + mDays4 + mDays5) * 0.01 / 365, "0")) 'mDays '' + mDays1 + mDays2 + mDays3	
                        End If
                        mCalcDepAsOn = mSaleDate6
                    End If

                End If

                If optShowType(2).Checked = True Then

                Else
                    mAddDays = 0
                    '            mStartDate = IIf(CVDate(mStartDate) > CVDate(mAsOnDate), mAsOnDate, mStartDate)	
                    mAddDays = GetLeapYear(mStartDate, mAsOnDate)
                    If CDate(pPurchaseDate) = CDate(mStartDate) Then
                        mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mAsOnDate)) + 1 - mAddDays
                    Else
                        If GetCurrentFYNo(PubDBCn, mAsOnDate) <= 2009 Then
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mAsOnDate)) + 1 - mAddDays ''23/07/2010 by Nitin Error in Cumm. Desp. If Sale	
                        Else
                            mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartDate), CDate(mAsOnDate)) + 1 - mAddDays '''12/07/07  by nitin calc in Days..	
                        End If
                    End If

                    mDepAmount = CDbl(VB6.Format(mDescpCalcOn * mDepRate * mDays * 0.01 / 365, "0"))

                    mTotalDepAmount = mTotalDepAmount + mDepAmount - pSaleOPDesp
                End If
            End If
        End If

        '    If mDepRate = 100 Then	
        '        mAddDays = 0	
        '        mLastFYEndDate = DateAdd("d", -1, lblFYStartDate.Caption)	
        '        mAddDays = GetLeapYear(pPurchaseDate, mLastFYEndDate)	
        '        mDays = DateDiff("d", pPurchaseDate, mLastFYEndDate) + 1 - mAddDays	
        '        mOPCummDesp = Format(mABSPurchaseAmount * mDepRate * mDays * 0.01 / 365, "0")	
        '	
        '        If mOPCummDesp < 1 Then	
        '            mOPCummDesp = 0	
        '        ElseIf mOPCummDesp > mABSPurchaseAmount Then	
        '            mOPCummDesp = mABSPurchaseAmount	
        '        End If	
        '    End If	

        If pPurchaseAmount < 1 Then
            mTotalDepAmount = mTotalDepAmount * -1
            pSaleDesp = pSaleDesp * -1
        End If

        If System.Math.Abs(pSaleDesp) > System.Math.Abs(mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6) Then
            pSaleDesp = mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6
        End If
        CalcDepreciationAmount = True
        Exit Function
LedgError:
        '    Resume	
        CalcDepreciationAmount = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function






    Private Function CheckSaleAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mStartDate As String) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf _
            & " FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf _
            & " AND SALE_BILL_DATE <=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value)
            '        mSaleAmount1 = IIf(IsNull(RsTemp!ORIGINAL_COST), 0, RsTemp!ORIGINAL_COST)	
            '        mSaleDate1 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE), 0, RsTemp!SALE_BILL_DATE), "DD/MM/YYYY")	
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf _
            & " FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf _
            & " AND SALE_BILL_DATE1 <=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value)
            '        mSaleAmount2 = IIf(IsNull(RsTemp!ORIGINAL_COST1), 0, RsTemp!ORIGINAL_COST1)	
            '        mSaleDate2 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE1), 0, RsTemp!SALE_BILL_DATE1), "DD/MM/YYYY")	
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_BILL_DATE2" & vbCrLf _
            & " FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf & " AND SALE_BILL_DATE2 <=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value)
            '        mSaleAmount3 = IIf(IsNull(RsTemp!ORIGINAL_COST2), 0, RsTemp!ORIGINAL_COST2)	
            '        mSaleDate3 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE2), 0, RsTemp!SALE_BILL_DATE2), "DD/MM/YYYY")	
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_BILL_DATE3" & vbCrLf _
            & " FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf _
            & " AND SALE_BILL_DATE3 <=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value)
            '        mSaleAmount4 = IIf(IsNull(RsTemp!ORIGINAL_COST3), 0, RsTemp!ORIGINAL_COST3)	
            '        mSaleDate4 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE3), 0, RsTemp!SALE_BILL_DATE3), "DD/MM/YYYY")	
        End If

        SqlStr = "SELECT ORIGINAL_COST4,SALE_BILL_DATE4" & vbCrLf _
            & " FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf _
            & " AND SALE_BILL_DATE4 <=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value)
            '        mSaleAmount4 = IIf(IsNull(RsTemp!ORIGINAL_COST3), 0, RsTemp!ORIGINAL_COST3)	
            '        mSaleDate4 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE3), 0, RsTemp!SALE_BILL_DATE3), "DD/MM/YYYY")	
        End If

        SqlStr = "SELECT ORIGINAL_COST5,SALE_BILL_DATE5" & vbCrLf _
            & " FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf _
            & " AND SALE_BILL_DATE5 <=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckSaleAmount = CheckSaleAmount + IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value)
            '        mSaleAmount4 = IIf(IsNull(RsTemp!ORIGINAL_COST3), 0, RsTemp!ORIGINAL_COST3)	
            '        mSaleDate4 = Format(IIf(IsNull(RsTemp!SALE_BILL_DATE3), 0, RsTemp!SALE_BILL_DATE3), "DD/MM/YYYY")	
        End If

        Exit Function
LedgError:
        CheckSaleAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CalcSaleAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer, ByRef mStartDate As String, ByRef mEndDate As String, ByRef mSaleAmount1 As Double, ByRef mSaleAmount2 As Double, ByRef mSaleAmount3 As Double, ByRef mSaleAmount4 As Double, ByRef mSaleAmount5 As Double, ByRef mSaleAmount6 As Double, ByRef mSaleDate1 As String, ByRef mSaleDate2 As String, ByRef mSaleDate3 As String, ByRef mSaleDate4 As String, ByRef mSaleDate5 As String, ByRef mSaleDate6 As String, ByRef pIsOpening As String) As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset


        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        If pIsOpening = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE <TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = IIf(IsNull(RsTemp!ORIGINAL_COST), 0, RsTemp!ORIGINAL_COST)	
            mSaleAmount1 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value), CStr(0)))
            mSaleDate1 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE").Value), 0, RsTemp.Fields("SALE_BILL_DATE").Value), "DD/MM/YYYY")
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        If pIsOpening = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE1 <TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE1 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE1 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = CheckSaleAmount + IIf(IsNull(RsTemp!ORIGINAL_COST1), 0, RsTemp!ORIGINAL_COST1)	
            mSaleAmount2 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST1").Value), 0, RsTemp.Fields("ORIGINAL_COST1").Value), CStr(0)))
            mSaleDate2 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE1").Value), 0, RsTemp.Fields("SALE_BILL_DATE1").Value), "DD/MM/YYYY")
        End If

        SqlStr = "SELECT ORIGINAL_COST2,SALE_BILL_DATE2" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        If pIsOpening = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE2 <TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE2 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE2 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = CheckSaleAmount + IIf(IsNull(RsTemp!ORIGINAL_COST2), 0, RsTemp!ORIGINAL_COST2)	
            mSaleAmount3 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST2").Value), 0, RsTemp.Fields("ORIGINAL_COST2").Value), CStr(0)))
            mSaleDate3 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE2").Value), 0, RsTemp.Fields("SALE_BILL_DATE2").Value), "DD/MM/YYYY")
        End If

        SqlStr = "SELECT ORIGINAL_COST3,SALE_BILL_DATE3" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        If pIsOpening = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE3 <TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE3 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE3 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = CheckSaleAmount + IIf(IsNull(RsTemp!ORIGINAL_COST3), 0, RsTemp!ORIGINAL_COST3)	
            mSaleAmount4 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST3").Value), 0, RsTemp.Fields("ORIGINAL_COST3").Value), CStr(0)))
            mSaleDate4 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE3").Value), 0, RsTemp.Fields("SALE_BILL_DATE3").Value), "DD/MM/YYYY")
        End If

        SqlStr = "SELECT ORIGINAL_COST4,SALE_BILL_DATE4" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        If pIsOpening = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE4 <TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE4 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE4 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = CheckSaleAmount + IIf(IsNull(RsTemp!ORIGINAL_COST3), 0, RsTemp!ORIGINAL_COST3)	
            mSaleAmount5 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST4").Value), 0, RsTemp.Fields("ORIGINAL_COST4").Value), CStr(0)))
            mSaleDate5 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE4").Value), 0, RsTemp.Fields("SALE_BILL_DATE4").Value), "DD/MM/YYYY")
        End If

        SqlStr = "SELECT ORIGINAL_COST5,SALE_BILL_DATE5" & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        If pIsOpening = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE5 <TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND SALE_BILL_DATE5 >=TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SALE_BILL_DATE5 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            '        CheckSaleAmount = CheckSaleAmount + IIf(IsNull(RsTemp!ORIGINAL_COST3), 0, RsTemp!ORIGINAL_COST3)	
            mSaleAmount6 = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST5").Value), 0, RsTemp.Fields("ORIGINAL_COST5").Value), CStr(0)))
            mSaleDate6 = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SALE_BILL_DATE5").Value), 0, RsTemp.Fields("SALE_BILL_DATE5").Value), "DD/MM/YYYY")
        End If

        CalcSaleAmount = True
        Exit Function
LedgError:
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

        SqlStr = "SELECT ORIGINAL_COST,SALE_BILL_DATE" & vbCrLf _
            & " FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf _
            & " AND SALE_BILL_DATE <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetGrossBlock = GetGrossBlock - IIf(IsDBNull(RsTemp.Fields("ORIGINAL_COST").Value), 0, RsTemp.Fields("ORIGINAL_COST").Value)
        End If

        SqlStr = "SELECT ORIGINAL_COST1,SALE_BILL_DATE1" & vbCrLf _
            & " FROM AST_ASSET_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND AUTO_KEY_ASSET=" & pRefNo & "" & vbCrLf _
            & " AND SALE_BILL_DATE1 <=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf _
            & " AND CANCELLED='N'"

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

    Private Function GetPurchaseAmount(ByRef pRefNo As Double, ByRef pCompanyCode As Integer) As Double
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mPurchaseAmount As Double

        GetPurchaseAmount = 0

        SqlStr = " SELECT  TOTAL_COST-DN_CR_AMOUNT+CD_AMOUNT+OTH_AMOUNT + DN_CR_AMOUNT -(MODVAT_AMOUNT+CESS_AMOUNT+SHEC_AMOUNT+AED_AMOUNT+SALETAX_REFUND+CGST_CLAIMAMOUNT+SGST_CLAIMAMOUNT+IGST_CLAIMAMOUNT) AS PURCHASE_COST " & vbCrLf & " FROM AST_ASSET_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & pRefNo & ""

        SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPurchaseAmount = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value)
        End If

        Exit Function
LedgError:
        GetPurchaseAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetDepreciationRate(ByRef pCompanyCode As Integer, ByRef pTRNType As Double, ByRef pModCode As String, ByRef pDeprType As String) As Double
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

        If pDeprType = "O" Then
            mTable = "AST_DEPRECIATION_NEW_MST" '"AST_DEPRECIATION_MST"
        Else
            mTable = "AST_DEPRECIATION_NEW_MST"
        End If

        SqlStr = "SELECT FYEAR, DEPR_RATE " & vbCrLf _
            & " FROM " & mTable & " " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND GROUP_CODE=" & pTRNType & ""


        If pDeprType = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & pCurrentYear & ""
        End If

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
        Dim mAsOnDate As String
        Dim pCurrentYear As Integer
        Dim mTable As String

        If CDate(txtDepreciationDate.Text) < CDate("01/04/2003") Then
            If Month(CDate(txtDepreciationDate.Text)) = 1 Or Month(CDate(txtDepreciationDate.Text)) = 2 Or Month(CDate(txtDepreciationDate.Text)) = 3 Then
                pCurrentYear = Year(CDate(txtDepreciationDate.Text)) - 1
            Else
                pCurrentYear = Year(CDate(txtDepreciationDate.Text))
            End If
        Else
            pCurrentYear = GetCurrentFYNo(PubDBCn, VB6.Format(txtDepreciationDate.Text))
        End If

        SqlStr = "SELECT ASSETS_LIFE_DAYS " & vbCrLf & " FROM AST_DEPRECIATION_NEW_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND GROUP_CODE=" & pTRNType & ""


        SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & pCurrentYear & ""


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
        Dim mOPGrossField As String

        If optShowType(2).Checked = True Then
            mOPGrossField = "OP_GROSS_BLOCK" ''"NET_BLOCK"	
        Else
            mOPGrossField = "OP_GROSS_BLOCK"
        End If

        SqlStr = ""
        If optShow(0).Checked = True And chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = " SELECT  '', '', '', '1', COMPANY_NAME, " & vbCrLf _
                & " TRNNAME, 'OPENING', '', '', '', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'),TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'), " & vbCrLf _
                & " SUM(" & mOPGrossField & "), 0, '', SUM(CURRENT_DESP), SUM(CUMULATIVE_DESP), " & vbCrLf _
                & " SUM(SALE_AMOUNT), '', SUM(SALE_DESP), TO_DATE(''), '', " & vbCrLf _
                & " SUM(GROSS_BLOCK), SUM(NET_BLOCK), '', '' "

            SqlStr = SqlStr & vbCrLf & " FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' "

            If optDate(1).Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND PV_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND PUT_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')"
            End If
            SqlStr = SqlStr & vbCrLf & " GROUP BY TRNNAME, COMPANY_NAME"
            '        SqlStr = SqlStr & vbCrLf & " ORDER BY 6,5,11,9"	
            SqlStr = SqlStr & vbCrLf & " UNION ALL "

        End If

        If optShow(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " SELECT  '', FYEAR, BOOKTYPE, BOOKSUBTYPE, COMPANY_NAME, " & vbCrLf _
                & " TRNNAME, SUPP_CUST_NAME, ITEM_DESC, TO_CHAR(AUTO_KEY_ASSET), LOCATION, " & vbCrLf _
                & " PV_DATE, PUT_DATE, " & mOPGrossField & ", DAYS, PUR_YEAR, CURRENT_DESP, CUMULATIVE_DESP, " & vbCrLf _
                & " SALE_AMOUNT, SALE_DATE, SALE_DESP, PHY_DATE, PHY_WHOM, " & vbCrLf _
                & " GROSS_BLOCK, NET_BLOCK, ITEM_TYPE, ASSET_TYPE "
        Else
            SqlStr = " SELECT  '', '', '', '1', '', " & vbCrLf _
                & " TRNNAME, '', '', '', '', " & vbCrLf _
                & " '', '',SUM(" & mOPGrossField & "), 0, '', SUM(CURRENT_DESP), SUM(CUMULATIVE_DESP), " & vbCrLf _
                & " SUM(SALE_AMOUNT), '', SUM(SALE_DESP), '', '', " & vbCrLf _
                & " SUM(GROSS_BLOCK), SUM(NET_BLOCK), '', '' "
        End If

        SqlStr = SqlStr & vbCrLf & " FROM TEMP_AST_DESP_TRN WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' "

        If optShow(0).Checked = True Then
            If chkOpening.CheckState = System.Windows.Forms.CheckState.Checked Then
                If optDate(1).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND PV_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND PUT_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')"
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

        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.Focus()
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

        If Trim(txtDeprMode.Text) = "" Then
            MsgInformation("Invaild Mode.")
            txtDeprMode.Focus()
            FieldsVerification = False
            Exit Function
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
                    Call CalcRowTotal(SprdMain, ColDeprec1, mFromRow, ColDeprec1, mToRow, cntRow, ColDeprec1)
                    Call CalcRowTotal(SprdMain, ColCumulativeDeprec, mFromRow, ColCumulativeDeprec, mToRow, cntRow, ColCumulativeDeprec)


                    Call CalcRowTotal(SprdMain, ColSaleAmount, mFromRow, ColSaleAmount, mToRow, cntRow, ColSaleAmount)
                    Call CalcRowTotal(SprdMain, ColTotalDeprecClaim, mFromRow, ColTotalDeprecClaim, mToRow, cntRow, ColTotalDeprecClaim)

                    Call CalcRowTotal(SprdMain, ColGrossBlock, mFromRow, ColGrossBlock, mToRow, cntRow, ColGrossBlock)
                    Call CalcRowTotal(SprdMain, ColNetBlock, mFromRow, ColNetBlock, mToRow, cntRow, ColNetBlock)


                    '                Call CalcRowTotal(SprdMain, ColTotalCost, mFromRow, ColTotalCost, mToRow, IIf(mLastRow = True, .MaxRows, cntRow), ColTotalCost)	
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
            Call CalcRowTotal(SprdMain, ColDeprec1, mFromRow, ColDeprec1, mToRow, .MaxRows, ColDeprec1)
            Call CalcRowTotal(SprdMain, ColCumulativeDeprec, mFromRow, ColCumulativeDeprec, mToRow, .MaxRows, ColCumulativeDeprec)


            Call CalcRowTotal(SprdMain, ColSaleAmount, mFromRow, ColSaleAmount, mToRow, .MaxRows, ColSaleAmount)
            Call CalcRowTotal(SprdMain, ColTotalDeprecClaim, mFromRow, ColTotalDeprecClaim, mToRow, .MaxRows, ColTotalDeprecClaim)

            Call CalcRowTotal(SprdMain, ColGrossBlock, mFromRow, ColGrossBlock, mToRow, .MaxRows, ColGrossBlock)
            Call CalcRowTotal(SprdMain, ColNetBlock, mFromRow, ColNetBlock, mToRow, .MaxRows, ColNetBlock)

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
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CDate(txtDateFrom.Text)) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            Exit Sub
        End If

        If FYChk(CDate(txtDateTo.Text)) = False Then
            txtDateTo.Focus()
            Cancel = True
            Exit Sub
        End If
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


        If MainClass.ValidateWithMasterTable(txtDeprMode.Text, "MODE_CODE", "MODE_CODE", "AST_DEPRECIATION_MODE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtDeprMode.Text = UCase(Trim(txtDeprMode.Text))
        Else
            MsgInformation("No Such Depreciation Mode.")
            Cancel = True
        End If

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
        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        End If

        If mCompanyCode = "" Then
            SqlStr = ""
        Else
            SqlStr = "COMPANY_CODE=" & mCompanyCode & ""
            SqlStr = SqlStr & vbCrLf & " AND CANCELLED='N'"
        End If

        If MainClass.ValidateWithMasterTable(txtRefNo.Text, "AUTO_KEY_ASSET", "AUTO_KEY_ASSET", "AST_ASSET_TRN", PubDBCn, MasterNo, , SqlStr) = True Then
            txtRefNo.Text = Val(txtRefNo.Text)
        Else
            MsgInformation("No Such Ref No.")
            Cancel = True
        End If

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
        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        End If

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
    Private Function UpdateTempTableNew() As Boolean
        On Error GoTo LedgError

        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mDepreMode As String
        Dim mDeprecAsOn As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCompanyName As String
        Dim mCompanyCode As Long
        Dim mFYStartDate As String
        Dim mRefNo As Double
        Dim mPurchaseDate As String
        Dim mPrevCumulativeDespBal As Double
        Dim mCurrDesp As Double
        Dim mCurrCumulativeDespBal As Double
        Dim mFYear As Long
        Dim mSqlStr As String

        Dim pSaleDesp As Double
        Dim pSaleDate As String
        Dim pIsSale As Boolean
        Dim mSaleAmount1 As Double
        Dim mSaleAmount2 As Double
        Dim mSaleAmount3 As Double
        Dim mSaleAmount4 As Double
        Dim mSaleAmount5 As Double
        Dim mSaleAmount6 As Double
        Dim mSaleDate1 As String
        Dim mSaleDate2 As String
        Dim mSaleDate3 As String
        Dim mSaleDate4 As String
        Dim mSaleDate5 As String
        Dim mSaleDate6 As String
        Dim mGrossBlock As String
        'Dim mTempGrossBlock As String
        Dim mNetBlock As String
        'Dim mTillDateSaleAmount As String
        Dim pOPGrossBlock As Double
        Dim mAddDays As Integer
        'Dim pNormalDesc As Double
        'Dim pABSPurchaseAmount As Double
        'Dim pABSGrossAmount As Double

        'Dim cntDate As Date
        'Dim mAddDays As Integer
        'Dim mCalcDepAsOn As String
        'Dim mActAddDays As Integer
        'Dim mCummDepr As Boolean
        'Dim pOPCummDesp As Double
        'Dim pOPNetGrossBlock As Double
        'Dim pOPNetGrossBlockABS As Double
        Dim pNetGrossBlock As Double
        'Dim RsTemp As ADODB.Recordset
        Dim xSaleDateStr As String
        'Dim mSqlStr As String

        'Dim mPurchaseLife As Double
        'Dim mUsefullLife As Double
        'Dim mBalUsefullLife As Double
        'Dim mNewCummlativeDesp As Double
        'Dim xTempOPGrossBlock As Double
        'Dim mActualPurchaseAmount As Double
        'Dim mNetSaleEffect As Double
        'Dim mSelvageValue As Double
        'Dim mOPSelvageValue As Double
        Dim pTrnName As String
        Dim pTRNType As String
        Dim mDeprRate As Double

        UpdateTempTableNew = False




        mFYear = RsCompany.Fields("FYEAR").Value

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

            mCompanyName = RsTemp.Fields("COMPANY_NAME").Value
            mCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
            mFYStartDate = VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY")
            mRefNo = RsTemp.Fields("AUTO_KEY_ASSET").Value
            pTrnName = IIf(IsDBNull(RsTemp.Fields("TRNNAME").Value), "", RsTemp.Fields("TRNNAME").Value)
            pTRNType = IIf(IsDBNull(RsTemp.Fields("TRNCODE").Value), "", RsTemp.Fields("TRNCODE").Value)

            mDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")

            If optDate(1).Checked = True Then
                mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PV_DATE").Value), "", RsTemp.Fields("PV_DATE").Value), "DD/MM/YYYY")
            Else
                mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUT_DATE").Value), "", RsTemp.Fields("PUT_DATE").Value), "DD/MM/YYYY")
            End If

            'mGrossBlock = GetOpeningAssets(mFYear, mCompanyCode, mRefNo, "GROSS_BLOCK")
            If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
                pOPGrossBlock = GetOpeningAssets(mFYear, mCompanyCode, mRefNo, "OP_GROSS_BLOCK")

                mAddDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mFYStartDate), CDate(mDeprecAsOn)) + 1 ''mDeprecAsOn	
                mAddDays = mAddDays - GetLeapYear(mFYStartDate, mDeprecAsOn) ' mDeprecAsOn)	
            Else
                pOPGrossBlock = GetPurchaseAmount(mCompanyCode, mRefNo)
            End If



            mPrevCumulativeDespBal = GetOpeningAssets(mFYear, mCompanyCode, mRefNo, "CUMULATIVE_DESP")

            mCurrDesp = pOPGrossBlock * mDeprRate * 0.01 * mAddDays / 365

            ''   mDepAmount = (pOPGrossBlock - pSaleAmount) * mDeprRate * 0.01 * mDays / 365	


            mCurrCumulativeDespBal = mPrevCumulativeDespBal + mCurrDesp

            mGrossBlock = pOPGrossBlock - mCurrCumulativeDespBal
            pNetGrossBlock = pOPGrossBlock - mCurrCumulativeDespBal

            mSqlStr = "UPDATE TEMP_AST_DESP_TRN SET " & vbCrLf _
                & " DAYS=" & mAddDays & "," & vbCrLf _
                & " CURRENT_DESP=" & mCurrDesp & "," & vbCrLf _
                & " CUMULATIVE_DESP=" & mCurrCumulativeDespBal & "," & vbCrLf _
                & " SALE_AMOUNT=" & VB6.Format(mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6, "0") & "," & vbCrLf _
                & " SALE_DATE='" & xSaleDateStr & "'," & vbCrLf _
                & " SALE_DESP=" & pSaleDesp & "," & vbCrLf _
                & " NET_BLOCK=" & pNetGrossBlock & "," & vbCrLf _
                & " GROSS_BLOCK=" & mGrossBlock & ", OP_GROSS_BLOCK=" & pOPGrossBlock & ""

            mSqlStr = mSqlStr & vbCrLf _
                & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                & " AND AUTO_KEY_ASSET=" & mRefNo & ""

            PubDBCn.Execute(mSqlStr)

            RsTemp.MoveNext()
        Loop
        '            pIsSale = False
        '            mDays1 = 0
        '            mDays2 = 0
        '            mDays3 = 0
        '            mDays4 = 0
        '            mDays = 0
        '            mSaleAmount1 = 0
        '            mSaleAmount2 = 0
        '            mSaleAmount3 = 0
        '            mSaleAmount4 = 0
        '            mSaleAmount5 = 0
        '            mSaleAmount6 = 0

        '            mSaleDate1 = ""
        '            mSaleDate2 = ""
        '            mSaleDate3 = ""
        '            mSaleDate4 = ""
        '            mSaleDate5 = ""
        '            mSaleDate6 = ""

        '            pDays = 0
        '            pNormalDesc = 0
        '            pOPCummDesp = 0
        '            mCummDepr = False
        '            pOPNetGrossBlockABS = 0
        '            pOPNetGrossBlock = 0
        '            pNetGrossBlock = 0


        '            pSaleAmount = 0
        '            pSaleDesp = 0
        '            pSaleDate = ""
        '            mCummDepAmount = 0
        '            mCalcDepAsOn = CStr(0)

        '            mCompanyName = RsTemp.Fields("COMPANY_NAME").Value
        '            mCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
        '            mFYStartDate = VB6.Format(lblFYStartDate.Text, "DD/MM/YYYY")
        '            mRefNo = RsTemp.Fields("AUTO_KEY_ASSET").Value
        '            If optDate(1).Checked = True Then
        '                mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PV_DATE").Value), "", RsTemp.Fields("PV_DATE").Value), "DD/MM/YYYY")
        '            Else
        '                mPurchaseDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PUT_DATE").Value), "", RsTemp.Fields("PUT_DATE").Value), "DD/MM/YYYY")
        '            End If

        '            pPurchaseYear = IIf(IsDBNull(RsTemp.Fields("PUR_YEAR").Value), "0", RsTemp.Fields("PUR_YEAR").Value)
        '            pTrnName = IIf(IsDBNull(RsTemp.Fields("TRNNAME").Value), "", RsTemp.Fields("TRNNAME").Value)
        '            pTRNType = IIf(IsDBNull(RsTemp.Fields("TRNCODE").Value), "", RsTemp.Fields("TRNCODE").Value)
        '            pPurchaseAmount = IIf(IsDBNull(RsTemp.Fields("TOTAL_COST").Value), 0, RsTemp.Fields("TOTAL_COST").Value) ''GetPurchaseAmount(mRefNo, mCompanyCode)	

        '            mSelvageValue = CDbl(VB6.Format(pPurchaseAmount * 5 / 100, "0"))

        '            pPurchaseAmount = CDbl(VB6.Format(pPurchaseAmount, "0"))
        '            pABSPurchaseAmount = System.Math.Abs(pPurchaseAmount)
        '            mActualPurchaseAmount = CDbl(VB6.Format(pPurchaseAmount, "0"))

        '            pOPGrossBlock = GetGrossBlock(mRefNo, mCompanyCode, mFYStartDate, pPurchaseAmount)
        '            pOPGrossBlock = CDbl(VB6.Format(pOPGrossBlock, "0"))
        '            pABSGrossAmount = System.Math.Abs(pOPGrossBlock)

        '            mTillDateSaleAmount = CStr(CheckSaleAmount(mRefNo, mCompanyCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(mFYStartDate)))))
        '            If pPurchaseAmount = CDbl(mTillDateSaleAmount) Then
        '                GoTo NextRec
        '            End If

        '            If optShowType(2).Checked = True Then
        '                If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
        '                    If CalcSaleAmount(mRefNo, mCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6, "O") = False Then GoTo LedgError
        '                    If CalcDepreciationAmount(mRefNo, mCompanyCode, mPurchaseDate, pPurchaseYear, pTRNType, pPurchaseAmount, mDepreMode, pCurrentYearDep, pSaleAmount, pSaleDesp, pSaleDate, mCummDepAmount, mCalcDepAsOn, pOPCummDesp, "O", 0) = False Then GoTo LedgError

        '                    xTempOPGrossBlock = 0
        '                    If pOPGrossBlock < 0 Then
        '                        If CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0")) > 0 Then
        '                            xTempOPGrossBlock = CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0"))
        '                        End If
        '                    Else
        '                        If CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0")) > 0 Then
        '                            xTempOPGrossBlock = CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0"))
        '                        End If
        '                    End If
        '                End If
        '            End If

        '            If CDate(mPurchaseDate) < CDate(mFYStartDate) Then
        '                If CalcSaleAmount(mRefNo, mCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6, "O") = False Then GoTo LedgError
        '                If CalcDepreciationAmount(mRefNo, mCompanyCode, mPurchaseDate, pPurchaseYear, pTRNType, pPurchaseAmount, mDepreMode, pCurrentYearDep, pSaleAmount, pSaleDesp, pSaleDate, mCummDepAmount, mCalcDepAsOn, pOPCummDesp, "O", xTempOPGrossBlock) = False Then GoTo LedgError
        '            End If

        '            ''01-aug-2013	
        '            '        If pPurchaseAmount < 0 Then	
        '            '            If Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0") > 0 Then	
        '            '                pOPNetGrossBlock = 0	
        '            '            Else	
        '            '                pOPNetGrossBlock = Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0")	
        '            '            End If	
        '            '        Else	
        '            '            If Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0") <= 0 Then	
        '            '                pOPNetGrossBlock = 0	
        '            '            Else	
        '            '                pOPNetGrossBlock = Format(pPurchaseAmount + pSaleDesp - mCummDepAmount, "0")	
        '            '            End If	
        '            '        End If	

        '            If pOPGrossBlock < 0 Then
        '                mActualPurchaseAmount = mActualPurchaseAmount - mCummDepAmount
        '                If CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0")) > 0 Then
        '                    pOPNetGrossBlock = 0
        '                Else
        '                    pOPNetGrossBlock = CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0"))
        '                End If
        '            Else
        '                If CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0")) <= 0 Then
        '                    pOPNetGrossBlock = 0
        '                Else
        '                    pOPNetGrossBlock = CDbl(VB6.Format(pOPGrossBlock + pSaleDesp - mCummDepAmount, "0"))
        '                End If
        '            End If

        '            mDays1 = 0
        '            mDays2 = 0
        '            mDays3 = 0
        '            mDays4 = 0
        '            mDays = 0
        '            mSaleAmount1 = 0
        '            mSaleAmount2 = 0
        '            mSaleAmount3 = 0
        '            mSaleAmount4 = 0
        '            mSaleAmount5 = 0
        '            mSaleAmount6 = 0

        '            mSaleDate1 = ""
        '            mSaleDate2 = ""
        '            mSaleDate3 = ""
        '            mSaleDate4 = ""
        '            mSaleDate5 = ""
        '            mSaleDate6 = ""

        '            pSaleAmount = 0
        '            pSaleDesp = 0
        '            pSaleDate = ""
        '            mCummDepAmount = 0
        '            mCalcDepAsOn = CStr(0)
        '            pOPCummDesp = 0

        '            ''For the Year	
        '            If CalcSaleAmount(mRefNo, mCompanyCode, (lblFYStartDate.Text), (lblFYEndDate.Text), mSaleAmount1, mSaleAmount2, mSaleAmount3, mSaleAmount4, mSaleAmount5, mSaleAmount6, mSaleDate1, mSaleDate2, mSaleDate3, mSaleDate4, mSaleDate5, mSaleDate6, "") = False Then GoTo LedgError
        '            If CalcDepreciationAmount(mRefNo, mCompanyCode, mPurchaseDate, pPurchaseYear, pTRNType, pPurchaseAmount, mDepreMode, pCurrentYearDep, pSaleAmount, pSaleDesp, pSaleDate, mCummDepAmount, mCalcDepAsOn, pOPCummDesp, "", pOPGrossBlock) = False Then GoTo LedgError
        '            mOPSelvageValue = 0
        '            If optShowType(2).Checked = True Then
        '                ''14-06-2015	
        '                If pOPNetGrossBlock <= 0 Then
        '                    pOPNetGrossBlock = IIf(pOPNetGrossBlock < 0, 0, pOPNetGrossBlock)
        '                Else
        '                    pOPNetGrossBlock = pOPNetGrossBlock '- mSelvageValue  ''01072015	
        '                End If

        '                '            If pOPNetGrossBlock < 0 Then ''01072015	
        '                '                mOPSelvageValue = mSelvageValue	
        '                '            End If	


        '                pOPGrossBlock = pOPNetGrossBlock
        '                pPurchaseAmount = pOPNetGrossBlock ''IIf(IsNull(RsTemp!TOTAL_COST), 0, RsTemp!TOTAL_COST)	
        '                pPurchaseAmount = CDbl(VB6.Format(pPurchaseAmount, "0"))
        '                pABSPurchaseAmount = System.Math.Abs(pPurchaseAmount)
        '            End If

        '            '        .Col = ColCumulativeDeprec	
        '            If optShowType(2).Checked = True Then

        '            Else
        '                If System.Math.Abs(pOPGrossBlock) < System.Math.Abs(mCummDepAmount) Then

        '                    mCummDepAmount = pOPGrossBlock
        '                    If System.Math.Abs(pSaleDesp) > System.Math.Abs(pOPGrossBlock) Then
        '                        pSaleDesp = pOPGrossBlock
        '                    End If
        '                End If
        '            End If

        '            '        .Text = Format(mCummDepAmount, "0")	


        '            If CDate(mFYStartDate) > CDate(mPurchaseDate) Then
        '                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mFYStartDate), CDate(mDeprecAsOn)) + 1 ''mDeprecAsOn	
        '                mDays = mDays - GetLeapYear(mFYStartDate, mDeprecAsOn) ' mDeprecAsOn)	
        '            Else
        '                mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mDeprecAsOn)) + 1 ''mDeprecAsOn	
        '                mDays = mDays - GetLeapYear(mPurchaseDate, mDeprecAsOn) ' mDeprecAsOn)	
        '            End If

        '            mDays = mDays - mAddDays

        '            If pOPGrossBlock = 0 Or pOPNetGrossBlock = 0 Then
        '                mDays = 0
        '            End If


        '            mActAddDays = mDays

        '            mDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, IIf(optShowType(0).Checked Or optShowType(2).Checked, "O", "N"))

        '            '        .Col = ColDeprec1	
        '            If optShowType(2).Checked = True Then

        '                If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
        '                    mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
        '                    mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
        '                    mBalUsefullLife = mUsefullLife - mPurchaseLife
        '                    If mBalUsefullLife <= 0 Then
        '                        mDepAmount = pOPGrossBlock '0	
        '                    Else
        '                        If mBalUsefullLife >= 365 Then
        '                            mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) * mDays / mBalUsefullLife
        '                        Else
        '                            mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) ''* mDays / mBalUsefullLife	
        '                        End If
        '                    End If
        '                    mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                Else
        '                    mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(txtDepreciationDate.Text)) + 1
        '                    mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
        '                    mBalUsefullLife = mUsefullLife ''- mPurchaseLife	
        '                    If mBalUsefullLife <= 0 Then
        '                        mDepAmount = pOPGrossBlock '0	
        '                    Else
        '                        If mBalUsefullLife >= 365 Then
        '                            mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) * mDays / mBalUsefullLife
        '                        Else
        '                            mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) ''* mDays / mBalUsefullLife	
        '                        End If
        '                    End If
        '                    mNewCummlativeDesp = mDepAmount

        '                    '                mDepAmount = mCummDepAmount	
        '                    '                mNewCummlativeDesp = mCummDepAmount	
        '                    '                mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")	
        '                    '                mDepAmount = (pOPNetGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) * mNewDeprRate * 0.01 * mDays / 365	
        '                    '                mNewCummlativeDesp = mCummDepAmount + mDepAmount	
        '                End If
        '            Else
        '                mDepAmount = (pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6) * mDeprRate * 0.01 * mDays / 365
        '            End If
        '            '            mDepAmount = (pOPGrossBlock - pSaleAmount) * mDeprRate * 0.01 * mDays / 365	

        '            If mSaleAmount1 <> 0 Then
        '                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate1)) + 1
        '                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate1)
        '                Else
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate1)) + 1
        '                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate1)
        '                End If
        '                If optShowType(2).Checked = True Then
        '                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
        '                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
        '                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
        '                        mBalUsefullLife = mUsefullLife - mPurchaseLife
        '                        If mBalUsefullLife <= 0 Then
        '                            mDepAmount = 0
        '                        Else
        '                            mDepAmount = mDepAmount + (mSaleAmount1) * mDays / mBalUsefullLife
        '                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                        End If
        '                    Else
        '                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
        '                        mDepAmount = mDepAmount + (mSaleAmount1 * mNewDeprRate * 0.01 * mDays / 365)
        '                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                    End If
        '                Else
        '                    mDepAmount = mDepAmount + (mSaleAmount1 * mDeprRate * 0.01 * mDays / 365)
        '                End If
        '                If pOPGrossBlock - mSaleAmount1 = 0 Then
        '                    mActAddDays = mDays
        '                End If
        '            End If

        '            If mSaleAmount2 <> 0 Then
        '                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate2)) + 1
        '                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate2)
        '                Else
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate2)) + 1
        '                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate2)
        '                End If
        '                If optShowType(2).Checked = True Then
        '                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
        '                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
        '                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
        '                        mBalUsefullLife = mUsefullLife - mPurchaseLife
        '                        If mBalUsefullLife <= 0 Then
        '                            mDepAmount = 0
        '                        Else
        '                            mDepAmount = mDepAmount + (mSaleAmount2) * mDays / mBalUsefullLife
        '                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                        End If
        '                    Else
        '                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
        '                        mDepAmount = mDepAmount + (mSaleAmount2 * mNewDeprRate * 0.01 * mDays / 365)
        '                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                    End If
        '                Else
        '                    mDepAmount = mDepAmount + (mSaleAmount2 * mDeprRate * 0.01 * mDays / 365)
        '                End If
        '                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 = 0 Then
        '                    mActAddDays = mDays
        '                End If
        '            End If

        '            If mSaleAmount3 <> 0 Then
        '                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate3)) + 1
        '                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate3)
        '                Else
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate3)) + 1
        '                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate3)
        '                End If
        '                If optShowType(2).Checked = True Then
        '                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
        '                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
        '                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
        '                        mBalUsefullLife = mUsefullLife - mPurchaseLife
        '                        If mBalUsefullLife <= 0 Then
        '                            mDepAmount = 0
        '                        Else
        '                            mDepAmount = mDepAmount + (mSaleAmount3) * mDays / mBalUsefullLife
        '                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                        End If
        '                    Else
        '                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
        '                        mDepAmount = mDepAmount + (mSaleAmount3 * mNewDeprRate * 0.01 * mDays / 365)
        '                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                    End If
        '                Else
        '                    mDepAmount = mDepAmount + (mSaleAmount3 * mDeprRate * 0.01 * mDays / 365)
        '                End If
        '                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 = 0 Then
        '                    mActAddDays = mDays
        '                End If
        '            End If

        '            If mSaleAmount4 <> 0 Then
        '                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate4)) + 1
        '                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate4)
        '                Else
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate4)) + 1
        '                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate4)
        '                End If
        '                If optShowType(2).Checked = True Then
        '                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
        '                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
        '                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
        '                        mBalUsefullLife = mUsefullLife - mPurchaseLife
        '                        If mBalUsefullLife <= 0 Then
        '                            mDepAmount = 0
        '                        Else
        '                            mDepAmount = mDepAmount + (mSaleAmount4) * mDays / mBalUsefullLife
        '                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                        End If
        '                    Else
        '                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
        '                        mDepAmount = mDepAmount + (mSaleAmount4 * mNewDeprRate * 0.01 * mDays / 365)
        '                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                    End If
        '                Else
        '                    mDepAmount = mDepAmount + (mSaleAmount4 * mDeprRate * 0.01 * mDays / 365)
        '                End If
        '                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 = 0 Then
        '                    mActAddDays = mDays
        '                End If
        '            End If

        '            If mSaleAmount5 <> 0 Then
        '                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate5)) + 1
        '                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate5)
        '                Else
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate5)) + 1
        '                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate5)
        '                End If
        '                If optShowType(2).Checked = True Then
        '                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
        '                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
        '                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
        '                        mBalUsefullLife = mUsefullLife - mPurchaseLife
        '                        If mBalUsefullLife <= 0 Then
        '                            mDepAmount = 0
        '                        Else
        '                            mDepAmount = mDepAmount + (mSaleAmount5) * mDays / mBalUsefullLife
        '                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                        End If
        '                    Else
        '                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
        '                        mDepAmount = mDepAmount + (mSaleAmount5 * mNewDeprRate * 0.01 * mDays / 365)
        '                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                    End If
        '                Else
        '                    mDepAmount = mDepAmount + (mSaleAmount5 * mDeprRate * 0.01 * mDays / 365)
        '                End If
        '                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 = 0 Then
        '                    mActAddDays = mDays
        '                End If
        '            End If

        '            If mSaleAmount6 <> 0 Then
        '                If CDate(mPurchaseDate) < CDate(lblFYStartDate.Text) Then
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(lblFYStartDate.Text), CDate(mSaleDate6)) + 1
        '                    mDays = mDays - GetLeapYear((lblFYStartDate.Text), mSaleDate6)
        '                Else
        '                    mDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(mSaleDate6)) + 1
        '                    mDays = mDays - GetLeapYear(mPurchaseDate, mSaleDate6)
        '                End If
        '                If optShowType(2).Checked = True Then
        '                    If CDate(mPurchaseDate) <= CDate(pAssetOpeningDate) Then
        '                        mPurchaseLife = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mPurchaseDate), CDate(pAssetOpeningDate)) + 1
        '                        mUsefullLife = GetUsefullLife(mCompanyCode, pTRNType)
        '                        mBalUsefullLife = mUsefullLife - mPurchaseLife
        '                        If mBalUsefullLife <= 0 Then
        '                            mDepAmount = 0
        '                        Else
        '                            mDepAmount = mDepAmount + (mSaleAmount6) * mDays / mBalUsefullLife
        '                            mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                        End If
        '                    Else
        '                        mNewDeprRate = GetDepreciationRate(mCompanyCode, pTRNType, mDepreMode, "N")
        '                        mDepAmount = mDepAmount + (mSaleAmount6 * mNewDeprRate * 0.01 * mDays / 365)
        '                        mNewCummlativeDesp = mCummDepAmount + mDepAmount
        '                    End If
        '                Else
        '                    mDepAmount = mDepAmount + (mSaleAmount6 * mDeprRate * 0.01 * mDays / 365)
        '                End If
        '                If pOPGrossBlock - mSaleAmount1 - mSaleAmount2 - mSaleAmount3 - mSaleAmount4 - mSaleAmount5 - mSaleAmount6 = 0 Then
        '                    mActAddDays = mDays
        '                End If
        '            End If

        '            If System.Math.Abs(mDepAmount) > System.Math.Abs(pOPNetGrossBlock) Then
        '                mDepAmount = pOPNetGrossBlock
        '            End If

        '            mDepAmount = CDbl(VB6.Format(mDepAmount, "0"))
        '            '	
        '            '        .Col = ColDays	
        '            mActAddDays = CInt(VB6.Format(mActAddDays, "0"))
        '            '	
        '            '        .Col = ColSaleAmount	
        '            '        .Text = Format((mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4), "0")	
        '            '	
        '            '        .Col = ColSaleDate	

        '            xSaleDateStr = mSaleDate1 & IIf(mSaleDate2 = "", "", "," & mSaleDate2) & IIf(mSaleDate3 = "", "", "," & mSaleDate3) & IIf(mSaleDate4 = "", "", "," & mSaleDate4) & IIf(mSaleDate5 = "", "", "," & mSaleDate5) & IIf(mSaleDate6 = "", "", "," & mSaleDate6)
        '            '        .Text = mSaleDate1 & IIf(mSaleDate2 = "", "", "," & mSaleDate2) & IIf(mSaleDate3 = "", "", "," & mSaleDate3) & IIf(mSaleDate4 = "", "", "," & mSaleDate4)	
        '            '	
        '            '        .Col = ColTotalDeprecClaim	
        '            '        .Text = Format(pSaleDesp, "0")	
        '            '	
        '            '        .Col = ColGrossBlock	
        '            If optShowType(2).Checked = True Then
        '                mGrossBlock = CStr(GetGrossBlock(mRefNo, mCompanyCode, mDeprecAsOn, pPurchaseAmount))
        '            Else
        '                mGrossBlock = CStr(GetGrossBlock(mRefNo, mCompanyCode, mDeprecAsOn, pPurchaseAmount))
        '            End If
        '            mGrossBlock = VB6.Format(mGrossBlock, CStr(0))
        '            '            If mGrossBlock <= 100 Then	
        '            '                mGrossBlock = 0	
        '            '            End If	
        '            '        .Text = Format(mGrossBlock, "0")	
        '            '	
        '            '	
        '            '        .Col = ColNetBlock	
        '            If optShowType(2).Checked = True Then
        '                mCummDepAmount = CDbl(VB6.Format(mNewCummlativeDesp, "0"))

        '                If (mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6) = 0 Then
        '                    mNetSaleEffect = mCummDepAmount
        '                Else
        '                    mNetSaleEffect = 0 '' (mCummDepAmount)  '' - mDepAmount)	
        '                End If

        '                mTempGrossBlock = IIf(CDbl(mGrossBlock) <= 0, 0, IIf(CDbl(mGrossBlock) < mActualPurchaseAmount, mGrossBlock, mActualPurchaseAmount))
        '                If pPurchaseAmount < 0 Then
        '                    If CDbl(VB6.Format(mActualPurchaseAmount - mNetSaleEffect, "0")) > 0 Then
        '                        pNetGrossBlock = 0
        '                    Else
        '                        pNetGrossBlock = CDbl(VB6.Format(mActualPurchaseAmount - mNetSaleEffect, "0"))
        '                    End If
        '                Else
        '                    If CDbl(VB6.Format(mActualPurchaseAmount - mNetSaleEffect, "0")) <= 0 Then
        '                        pNetGrossBlock = 0
        '                    Else
        '                        pNetGrossBlock = CDbl(VB6.Format(mActualPurchaseAmount - mNetSaleEffect, "0"))
        '                    End If
        '                End If

        '                If pPurchaseAmount - (mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6) <= 0 Then
        '                    pNetGrossBlock = 0
        '                Else
        '                    pNetGrossBlock = pNetGrossBlock - CDbl(VB6.Format(mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6, "0"))
        '                End If
        '                If pOPGrossBlock = 0 Then pNetGrossBlock = 0
        '                pNetGrossBlock = pNetGrossBlock + mOPSelvageValue
        '            Else
        '                mTempGrossBlock = mGrossBlock

        '                If pPurchaseAmount < 0 Then
        '                    If CDbl(VB6.Format(CDbl(mTempGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0")) > 0 Then
        '                        pNetGrossBlock = 0
        '                    Else
        '                        pNetGrossBlock = CDbl(VB6.Format(CDbl(mTempGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0"))
        '                    End If
        '                Else
        '                    If CDbl(VB6.Format(CDbl(mTempGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0")) <= 0 Then
        '                        pNetGrossBlock = 0
        '                    Else
        '                        pNetGrossBlock = CDbl(VB6.Format(CDbl(mTempGrossBlock) + IIf(mDays = 0, 0, pSaleDesp) - mCummDepAmount, "0"))
        '                    End If
        '                End If
        '            End If



        '            mSqlStr = "UPDATE TEMP_AST_DESP_TRN SET " & vbCrLf & " DAYS=" & mActAddDays & "," & vbCrLf & " CURRENT_DESP=" & mDepAmount & "," & vbCrLf & " CUMULATIVE_DESP=" & mCummDepAmount & "," & vbCrLf & " SALE_AMOUNT=" & VB6.Format(mSaleAmount1 + mSaleAmount2 + mSaleAmount3 + mSaleAmount4 + mSaleAmount5 + mSaleAmount6, "0") & "," & vbCrLf & " SALE_DATE='" & xSaleDateStr & "'," & vbCrLf & " SALE_DESP=" & pSaleDesp & "," & vbCrLf & " GROSS_BLOCK=" & mGrossBlock & "," & vbCrLf & " NET_BLOCK=" & pNetGrossBlock & "," & vbCrLf & " OP_GROSS_BLOCK=" & pOPGrossBlock & ""

        '            mSqlStr = mSqlStr & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND AUTO_KEY_ASSET=" & mRefNo & ""

        '            PubDBCn.Execute(mSqlStr)

        'NextRec:

        '            pSaleAmount = 0
        '            pSaleDesp = 0
        '            cntRow = cntRow + 1
        '            lblCount.Text = CStr(cntRow)
        '            System.Windows.Forms.Application.DoEvents()
        '            RsTemp.MoveNext()
        '        Loop
        '''********************************	
        UpdateTempTableNew = True

        Exit Function
LedgError:
        '    Resume	
        UpdateTempTableNew = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
