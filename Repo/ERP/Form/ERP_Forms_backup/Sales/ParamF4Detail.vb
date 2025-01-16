Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamF4Detail
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 12
    ''Private PvtDBCn As ADODB.Connection

    Dim mPartyC4 As String
    Private Const ColLocked As Short = 1
    Private Const ColPartyC4No As Short = 2
    Private Const ColPartyC4Date As Short = 3
    Private Const ColPartyName As Short = 4
    Private Const ColMTRLCode As Short = 5
    Private Const ColMtrlName As Short = 6
    Private Const ColRecd As Short = 7
    Private Const ColIssued As Short = 8
    Private Const ColBillNo As Short = 9
    Private Const ColBillDate As Short = 10
    Private Const ColItemName As Short = 11
    Private Const ColBillQty As Short = 12
    Private Const ColMKEY As Short = 13

    Private Const mPageWidth As Short = 132
    Private Const Tab1 As Short = 0
    Private Const Tab2 As Short = 8
    Private Const Tab3 As Short = 29
    Private Const Tab4 As Short = 60 ''75
    Private Const Tab5 As Short = 75 ''95
    Private Const Tab6 As Short = 90 ''115
    Private Const Tab7 As Short = 125 ''135

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
            TxtC4No.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtC4No.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub

    Private Sub chkPaintAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPaintAll.CheckStateChanged
        Call PrintStatus(False)
        If chkPaintAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPaint.Enabled = False
            cmdPaintSearch.Enabled = False
        Else
            txtPaint.Enabled = True
            cmdPaintSearch.Enabled = True
        End If
    End Sub
    Private Sub chkParty_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkParty.CheckStateChanged
        Call PrintStatus(False)
        If chkParty.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPartyName.Enabled = False
            cmdPartySearch.Enabled = False
        Else
            txtPartyName.Enabled = True
            cmdPartySearch.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdPaintSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPaintSearch.Click
        SearchPaint()
    End Sub
    Private Sub cmdPartySearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPartySearch.Click
        SearchParty()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ReportonC4(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonC4(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonC4(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()
        mTitle = "C4 Details Received"
        mSubTitle = "As On Date : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") 'DEEPAK AS ON DATE

        If optShow(0).Checked = True Then
            If InsertSql = False Then GoTo ReportErr

            SqlStr = MakeSQL

            If lblBookType.Text = "P" Then
                mSubTitle = ""
            End If

            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\C4Detail.RPT"
        Else
            If InsertPrintDummy = False Then GoTo ReportErr

            'Select Record for print...

            SqlStr = ""

            SqlStr = FetchRecordForReport(SqlStr)
            If optShow(1).Checked = True Then
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\C4IW_SummITEM.RPT"
            Else
                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\C4OutwardSumm.RPT"
            End If

        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchC4()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        If optShow(0).Checked = True Then
            CalcSprdTotal()
        End If

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamF4Detail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "C4 Details Received"

        If lblBookType.Text = "P" Then
            lblMaterial.Text = "Paint :"
        Else
            lblMaterial.Text = "Material :"
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamF4Detail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtPaint.Enabled = False
        cmdPaintSearch.Enabled = False

        chkPaintAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtC4No.Enabled = False
        cmdSearch.Enabled = False

        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtAsOn.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        cboShow.Items.Clear()
        cboShow.Items.Add("BOTH")
        cboShow.Items.Add("COMPLETE")
        cboShow.Items.Add("PENDING")
        cboShow.SelectedIndex = 0

        chkParty.CheckState = System.Windows.Forms.CheckState.Checked
        txtPartyName.Enabled = False
        cmdPartySearch.Enabled = False

        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamF4Detail_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamF4Detail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub optOrderBy_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optOrderBy.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optOrderBy.GetIndex(eventSender)
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
        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        'Dim xBookType As String
        'Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColBillDate
        xVDate = VB6.Format(Me.SprdMain.Text, "DD/MM/YYYY")

        SprdMain.Col = ColBillNo
        xVNo = Me.SprdMain.Text

        If CDate(xVDate) < CDate(VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")) Or CDate(xVDate) > CDate(VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")) Then
            MsgInformation("You Can Open Only Current Year Data.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(xVNo, "BILLNO", "MKEY", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INVOICE_DATE=TO_DATE('" & VB6.Format(xVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = True Then
            xMKey = MasterNo
            Call ShowTrnFromF4(xMKey, xVDate, "", xVNo, "S", "", Me)
        End If



    End Sub

    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtC4No_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtC4No.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtC4No_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtC4No.DoubleClick
        SearchC4()
    End Sub
    Private Sub SearchC4()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtC4No.Text, "DSP_PAINT57F4_HDR", "PARTY_F4NO", "PARTY_F4DATE", , , SqlStr)
        If AcName <> "" Then
            TxtC4No.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchParty()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtPartyName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchPaint()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtPaint.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            txtPaint.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtC4No_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtC4No.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtC4No.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtC4No_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtC4No.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchC4()
    End Sub
    Private Sub TxtC4No_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtC4No.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtC4No.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((TxtC4No.Text), "PARTY_F4NO", "PARTY_F4NO", "DSP_PAINT57F4_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtC4No.Text = UCase(Trim(TxtC4No.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such C4.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        'Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 2)
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

            .Col = ColPartyC4No
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyC4No, 9)
            '        If optShow(0).Value = True Then
            '            .ColHidden = False
            '        ElseIf optOrderBy(1).Value = True Then
            '            .ColHidden = True
            '        Else
            '            .ColHidden = False
            '        End If

            .Col = ColPartyC4Date
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyC4Date, 8)
            '        If optShow(0).Value = True Then
            '            .ColHidden = False
            '        ElseIf optOrderBy(1).Value = True Then
            '            .ColHidden = True
            '        Else
            '            .ColHidden = False
            '        End If

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 25)

            .Col = ColMTRLCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMTRLCode, 6)

            .Col = ColMtrlName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMtrlName, 20)

            .Col = ColRecd
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColRecd, 8)

            .Col = ColIssued
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColIssued, 8)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 8)
            If optShow(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 8)
            If optShow(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If


            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 25)
            If optShow(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColBillQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBillQty, 8)
            .ColHidden = False


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
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            .Row = 0
            .Col = ColBillQty
            .Text = IIf(optShow(0).Checked = True, "Bill Qty", "Balance")
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""

        Show1 = False
        '    Screen.MousePointer = vbHourglass

        If optShow(0).Checked = True Then
            If InsertSql = False Then GoTo LedgError

            SqlStr = MakeSQL
        Else
            SqlStr = MakeSQLSumm
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        '    Screen.MousePointer = vbDefault

        Exit Function
LedgError:
        Show1 = False
        '    Screen.MousePointer = vbDefault
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mPaintName As String
        ''SELECT CLAUSE...


        '& " DECODE(ITEM_IO,'I','',NVL(GETSaleInvoiceNo(TRN.COMPANY_CODE,TRN.FYEAR,TRN.BILL_NO,TRN.BILL_DATE),TRN.BILL_NO)), " & vbCrLf _
        '& " DECODE(ITEM_IO,'I','',NVL(GETSaleInvoiceDate(TRN.COMPANY_CODE,TRN.FYEAR,TRN.BILL_NO,TRN.BILL_DATE),TRN.BILL_DATE)), " & vbCrLf _
        '
        MakeSQL = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " PARTY_F4NO, PARTY_F4DATE, SUPP_CUST_NAME,  " & vbCrLf & " ITEM_CODE, ITEM_DESC, " & vbCrLf & " TO_CHAR(RECDQTY,'9999999.9999') AS Received,    " & vbCrLf & " TO_CHAR(ISSUEDQTY,'9999999.9999') AS Issued, "

        '    If lblBookType.text = "P" Then
        MakeSQL = MakeSQL & vbCrLf & " DECODE(ITEM_IO,'I','',BILL_NO), " & vbCrLf & " DECODE(ITEM_IO,'I','',BILL_DATE), "
        '    Else
        '        MakeSQL = MakeSQL & vbCrLf _
        ''                & " DECODE(ITEM_IO,'I','',NVL(GETSaleInvoiceNo(COMPANY_CODE,FYEAR,BILL_NO,BILL_DATE),BILL_NO)), " & vbCrLf _
        ''                & " DECODE(ITEM_IO,'I','',NVL(GETSaleInvoiceDate(COMPANY_CODE,FYEAR,BILL_NO,BILL_DATE),BILL_DATE)), "
        '    End If

        MakeSQL = MakeSQL & vbCrLf & " SUB_ITEM_DESC,    " & vbCrLf & " TO_CHAR(BILL_QTY,'9999999.99'),  MKEY "

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM Temp_F4Detail "

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " UserId='" & MainClass.AllowSingleQuote(PubUserID) & "'"

        ''ORDER CLAUSE...
        If Trim(txtBillNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND BILL_NO='" & Trim(txtBillNo.Text) & "' "
        End If

        If optOrderBy(0).Checked Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY PARTY_F4NO,PARTY_F4DATE,ITEM_CODE,ITEM_IO,BILL_DATE,BILL_NO"
        ElseIf optOrderBy(1).Checked Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY PARTY_F4NO,PARTY_F4DATE,ITEM_CODE,ITEM_IO,BILL_DATE,BILL_NO"
        ElseIf optOrderBy(2).Checked Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY SUPP_CUST_NAME,PARTY_F4DATE,PARTY_F4NO,ITEM_CODE,ITEM_IO,BILL_DATE,BILL_NO"
        ElseIf optOrderBy(3).Checked Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY SUPP_CUST_NAME, ITEM_DESC, PARTY_F4DATE, PARTY_F4NO, ITEM_IO, BILL_DATE, BILL_NO"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLSumm() As String

        On Error GoTo ERR1
        Dim mPaintName As String
        Dim mPartyCode As String

        ''SELECT CLAUSE...
        '    If optOrderBy(1).Value = True Then
        '        MakeSQLSumm = " SELECT '','','',"
        '    Else
        MakeSQLSumm = " SELECT '',PARTY_F4NO,PARTY_F4DATE,"
        '    End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " CMST.SUPP_CUST_NAME, TRN.ITEM_CODE,A.ITEM_SHORT_DESC, " & vbCrLf & " TO_CHAR(SUM(DECODE(ITEM_IO,'I',ITEM_QTY,0)),'9999999.9999') AS Received,    " & vbCrLf & " TO_CHAR(SUM(DECODE(ITEM_IO,'O',ITEM_QTY,0)),'9999999.9999') AS Issued, " & vbCrLf & " '','','',TO_CHAR(SUM(DECODE(ITEM_IO,'O',-1,1)*ITEM_QTY),'9999999.9999') "

        ''FROM CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " FROM DSP_PAINT57F4_TRN TRN, INV_ITEM_MST A, FIN_SUPP_CUST_MST CMST "

        ''WHERE CLAUSE...
        MakeSQLSumm = MakeSQLSumm & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND TRN.COMPANY_CODE=A.COMPANY_CODE" & vbCrLf & " AND TRN.ITEM_CODE=A.ITEM_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND TRN.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "



        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.PARTY_F4NO='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        If chkPaintAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPaint.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPaintName = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.ITEM_CODE='" & MainClass.AllowSingleQuote(mPaintName) & "'"
            End If
        End If

        If chkParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If

        End If

        If optDate(0).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.PARTY_F4DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.PARTY_F4DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.BILL_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "

        If optStatus(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.SEND_STATUS ='Y' "
        ElseIf optStatus(2).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.SEND_STATUS ='N' "
        End If

        If chkScrap.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.ISSCRAP='Y'"
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND TRN.ISSCRAP='N'"
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',-1,1)*ITEM_QTY)=0"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',-1,1)*ITEM_QTY)<>0"
        End If

        ''GROUP BY CLAUSE...
        '    If optOrderBy(1).Value = True Then
        '        MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY TRN.ITEM_CODE,A.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"
        '    Else
        MakeSQLSumm = MakeSQLSumm & vbCrLf & "GROUP BY PARTY_F4NO,PARTY_F4DATE,TRN.ITEM_CODE,A.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"
        '    End If


        ''ORDER CLAUSE...
        If optOrderBy(0).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY PARTY_F4NO,PARTY_F4DATE,A.ITEM_SHORT_DESC"
        ElseIf optOrderBy(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,A.ITEM_SHORT_DESC"
        ElseIf optOrderBy(2).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME,PARTY_F4NO,PARTY_F4DATE,A.ITEM_SHORT_DESC"
        ElseIf optOrderBy(3).Checked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME,A.ITEM_SHORT_DESC,PARTY_F4NO,PARTY_F4DATE"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function


    Private Function F4Query() As String

        On Error GoTo ERR1
        Dim mPaintName As String
        Dim mPartyCode As String

        ''SELECT CLAUSE...

        F4Query = " SELECT PARTY_F4NO "

        ''FROM CLAUSE...
        F4Query = F4Query & vbCrLf & " FROM DSP_PAINT57F4_TRN"

        ''WHERE CLAUSE...
        F4Query = F4Query & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            F4Query = F4Query & vbCrLf & "AND PARTY_F4NO='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If


        If chkPaintAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPaint.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPaintName = MasterNo
                F4Query = F4Query & vbCrLf & "AND ITEM_CODE='" & MainClass.AllowSingleQuote(mPaintName) & "'"
            End If
        End If

        If chkParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = MasterNo
                F4Query = F4Query & vbCrLf & "AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If

        End If

        If optDate(0).Checked = True Then
            F4Query = F4Query & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
            F4Query = F4Query & vbCrLf & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        Else
            F4Query = F4Query & vbCrLf & " AND PARTY_F4DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
            F4Query = F4Query & vbCrLf & " AND PARTY_F4DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        End If

        F4Query = F4Query & vbCrLf & " AND BILL_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "

        If cboShow.SelectedIndex = 1 Then
            F4Query = F4Query & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',-1,1)*ITEM_QTY)=0"
        ElseIf cboShow.SelectedIndex = 2 Then
            F4Query = F4Query & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',-1,1)*ITEM_QTY)<>0"
        End If


        ''GROUP BY CLAUSE...
        F4Query = F4Query & vbCrLf & "GROUP BY PARTY_F4NO"


        ''ORDER CLAUSE...
        '    F4Query = F4Query & vbCrLf & "ORDER BY PARTY_F4NO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function InsertPrintDummy() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mLocked As String
        Dim mPartyC4No As String
        Dim mPartyC4Date As String
        Dim mMTRLCode As String
        Dim mMtrlName As String
        Dim mRecd As String
        Dim mIssued As String
        Dim mBillNo As String = ""
        Dim mBillDate As String = ""
        Dim mItemName As String = ""
        Dim mBalQty As String
        Dim mPartyName As String

        'Dim PvtDBCn As ADODB.Connection

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColPartyC4No
                mPartyC4No = .Text

                .Col = ColPartyC4Date
                mPartyC4Date = .Text

                .Col = ColPartyName
                mPartyName = Replace(.Text, "'", "''")

                .Col = ColMTRLCode
                mMTRLCode = .Text

                .Col = ColMtrlName
                mMtrlName = Replace(.Text, "'", "''")

                .Col = ColRecd
                mRecd = .Text

                .Col = ColIssued
                mIssued = .Text

                .Col = ColBillQty
                mBalQty = .Text

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow, " & vbCrLf & " Field1,Field2,Field3,Field4,Field5," & vbCrLf & " Field6,Field7,Field8,Field9,Field10,Field11) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mPartyC4No & "', " & vbCrLf & " '" & mPartyC4Date & "', " & vbCrLf & " '" & mMTRLCode & "', " & vbCrLf & " '" & mMtrlName & "', " & vbCrLf & " '" & mRecd & "', " & vbCrLf & " '" & mIssued & "', " & vbCrLf & " '" & mBillNo & "', " & vbCrLf & " '" & mBillDate & "','" & mItemName & "','" & mBalQty & "','" & mPartyName & "') "

                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        InsertPrintDummy = False
        PubDBCn.RollbackTrans()
    End Function
    Private Function InsertSql() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mPaintName As String
        Dim mPartyCode As String

        ''SELECT CLAUSE...
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_F4Detail NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        SqlStr = "INSERT INTO Temp_F4Detail (" & vbCrLf & " USERID, COMPANY_CODE, FYEAR, PARTY_F4NO, PARTY_F4DATE, " & vbCrLf & " ITEM_CODE, ITEM_DESC, " & vbCrLf & " RECDQTY , ISSUEDQTY, " & vbCrLf & " BILL_NO, BILL_DATE,  SUB_ITEM_DESC, " & vbCrLf & " BILL_QTY, MKEY, SUPP_CUST_CODE,SUPP_CUST_NAME,ITEM_IO, ORDER_FIELD) "

        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', TRN.COMPANY_CODE, TRN.FYEAR, " & vbCrLf & " PARTY_F4NO,PARTY_F4DATE, " & vbCrLf & " TRN.ITEM_CODE, A.ITEM_SHORT_DESC || ' (' || A.CUSTOMER_PART_NO || ')', " & vbCrLf & " DECODE(ITEM_IO,'I',TRN.ITEM_QTY,0) AS Received,    " & vbCrLf & " DECODE(ITEM_IO,'O',TRN.ITEM_QTY,0) AS Issued, "

        If lblBookType.Text = "P" Then
            SqlStr = SqlStr & vbCrLf & " DECODE(ITEM_IO,'I','',BILL_NO), " & vbCrLf & " DECODE(ITEM_IO,'I','',BILL_DATE), "
        Else
            SqlStr = SqlStr & vbCrLf & " DECODE(ITEM_IO,'I','',NVL(GETSaleInvoiceNo(TRN.COMPANY_CODE,TRN.FYEAR,TRN.BILL_NO,TRN.BILL_DATE),TRN.BILL_NO)), " & vbCrLf & " DECODE(ITEM_IO,'I','',NVL(GETSaleInvoiceDate(TRN.COMPANY_CODE,TRN.FYEAR,TRN.BILL_NO,TRN.BILL_DATE),TRN.BILL_DATE)), "
        End If

        '            & " DECODE(ITEM_IO,'I','',TRN.BILL_NO), " & vbCrLf _
        ''            & " DECODE(ITEM_IO,'I','',TRN.BILL_DATE), " & vbCrLf _
        '
        SqlStr = SqlStr & vbCrLf & " GETItemName(TRN.COMPANY_CODE,TRN.SUB_ITEM_CODE),    " & vbCrLf & " BILL_QTY,  TRN.MKEY ," & vbCrLf & " TRN.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, ITEM_IO, "

        If optOrderBy(0).Checked Or optOrderBy(1).Checked Then
            SqlStr = SqlStr & vbCrLf & " PARTY_F4NO"
        ElseIf optOrderBy(2).Checked Then
            SqlStr = SqlStr & vbCrLf & " SUPP_CUST_NAME"
        ElseIf optOrderBy(3).Checked Then
            SqlStr = SqlStr & vbCrLf & " A.ITEM_SHORT_DESC"
        End If


        '& " DECODE(ITEM_IO,'I','',NVL(GETSaleInvoiceNo(TRN.COMPANY_CODE,TRN.FYEAR,TRN.BILL_NO,TRN.BILL_DATE),TRN.BILL_NO)), " & vbCrLf _
        '& " DECODE(ITEM_IO,'I','',NVL(GETSaleInvoiceDate(TRN.COMPANY_CODE,TRN.FYEAR,TRN.BILL_NO,TRN.BILL_DATE),TRN.BILL_DATE)), " & vbCrLf _
        '
        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM DSP_PAINT57F4_TRN TRN, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST A"


        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.BOOKTYPE='" & lblBookType.Text & "'"

        SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND TRN.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf & " AND TRN.ITEM_CODE=A.ITEM_CODE "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND TRN.PARTY_F4NO='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"

        Else
            If cboShow.SelectedIndex <> 0 Then
                SqlStr = SqlStr & vbCrLf & "AND TRN.PARTY_F4NO IN (" & F4Query & ")"
            End If
        End If


        If chkParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
            End If

        End If

        If chkPaintAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPaint.Text, "Item_Short_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPaintName = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND TRN.ITEM_CODE='" & MainClass.AllowSingleQuote(mPaintName) & "'"
            End If

        End If

        If optDate(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
            SqlStr = SqlStr & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.PARTY_F4DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
            SqlStr = SqlStr & vbCrLf & " AND TRN.PARTY_F4DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.BILL_DATE<=TO_DATE('" & VB6.Format(txtAsOn.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY') "

        If optStatus(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.SEND_STATUS ='Y' "
        ElseIf optStatus(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.SEND_STATUS ='N'"
        End If

        If chkScrap.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND TRN.ISSCRAP='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN.ISSCRAP='N'"
        End If

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        InsertSql = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        PubDBCn.RollbackTrans()
        InsertSql = False
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtC4No.Text) = "" Then
                MsgInformation("Invaild C4.")
                TxtC4No.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtC4No.Text), "PARTY_F4NO", "PARTY_F4NO", "DSP_PAINT57F4_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyC4 = MasterNo
            Else
                MsgInformation("Invaild C4")
                TxtC4No.Focus()
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
        Dim StartRow As Integer
        Dim EndRow As Integer
        Dim mIssued As Double

        Dim mPreviousItemCode As String = ""

        Dim mPartyC4 As String
        Dim mItemCode As String
        Dim mCheckCode As String


        Dim mSubRecdTotal As Double
        Dim mSubIssueTotal As Double
        Dim I As Integer

        Call MainClass.AddBlankfpSprdRow(SprdMain, ColPartyC4No)
        cntRow = 1
        StartRow = 1
        With SprdMain
            Do While cntRow <= .MaxRows
                .Row = cntRow
                If optOrderBy(0).Checked = True Or optOrderBy(2).Checked = True Then
                    .Col = ColPartyC4No
                Else
                    .Col = ColPartyName
                End If

                mPartyC4 = Trim(.Text)

                .Col = ColMTRLCode
                mItemCode = Trim(.Text)

                mCheckCode = mPartyC4 & mItemCode

                If mPreviousItemCode <> mCheckCode And cntRow <> 1 Then
                    .MaxRows = .MaxRows + 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow


                    EndRow = cntRow
                    .Row = cntRow
                    .Col = ColPartyC4No
                    .Font = VB6.FontChangeBold(.Font, True)
                    .Text = "TOTAL"

                    '                Call CalcRowTotal(SprdMain, ColRecd, StartRow, ColRecd, EndRow - 1, EndRow, ColRecd)
                    '                Call CalcRowTotal(SprdMain, ColIssued, StartRow, ColIssued, EndRow - 1, EndRow, ColIssued)

                    mSubRecdTotal = 0
                    mSubIssueTotal = 0

                    For I = StartRow To EndRow - 1
                        .Row = I
                        .Col = ColRecd
                        mSubRecdTotal = mSubRecdTotal + Val(IIf(IsNumeric(.Text) = True, .Text, 0))

                        .Col = ColIssued
                        mSubIssueTotal = mSubIssueTotal + Val(IIf(IsNumeric(.Text) = True, .Text, 0))
                    Next

                    .Row = EndRow
                    .Col = ColRecd
                    .Text = VB6.Format(mSubRecdTotal, "0.0000")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Col = ColIssued
                    .Text = VB6.Format(mSubIssueTotal, "0.0000")
                    .Font = VB6.FontChangeBold(.Font, True)

                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)
                    .BlockMode = False

                    cntRow = cntRow + 1
                    .Row = cntRow
                    StartRow = cntRow
                End If

                If optOrderBy(0).Checked = True Or optOrderBy(2).Checked = True Then
                    .Col = ColPartyC4No
                Else
                    .Col = ColPartyName
                End If

                mPartyC4 = Trim(.Text)

                .Col = ColMTRLCode
                mItemCode = Trim(.Text)
                mPreviousItemCode = mPartyC4 & mItemCode


                cntRow = cntRow + 1
            Loop
            .Row = .MaxRows
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub




    Private Sub txtPaint_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaint.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtPaint_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaint.DoubleClick
        SearchPaint()
    End Sub


    Private Sub txtPaint_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaint.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPaint.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPaint_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPaint.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchPaint()
    End Sub


    Private Sub txtPaint_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaint.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If Trim(txtPaint.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtPaint.Text), "ITEM_SHORT_DESC", "ITEm_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            '        txtPaint.Text = UCase(Trim(MasterNo))
        Else
            MsgInformation("Invalid Item Code.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        SearchParty()
    End Sub


    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchParty()
    End Sub


    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtPartyName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable(txtPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtPartyName.Text = UCase(Trim(txtPartyName.Text))
        Else
            MsgInformation("Invalid Party Name")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
