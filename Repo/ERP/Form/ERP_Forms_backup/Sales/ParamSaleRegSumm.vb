Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSaleRegSumm
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Private Const ColLocked As Short = 1
    Private Const ColGroupField As Short = 2
    Private Const ColBillAmount As Short = 3
    Private Const ColSaleAmount As Short = 4
    Private Const ColCGST As Short = 5
    Private Const ColSGST As Short = 6
    Private Const ColIGST As Short = 7
    Private Const ColBED As Short = 8
    Private Const ColST As Short = 9
    Private Const ColCess As Short = 10
    Private Const ColSHCess As Short = 11
    Private Const ColTaxableAmount As Short = 12
    Private Const ColTaxPer As Short = 13
    Private Const ColHGST As Short = 14
    Private Const ColCST As Short = 15
    Private Const ColMSC As Short = 16
    Private Const ColOthCharges As Short = 17
    Private Const ColMKEY As Short = 18


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
    Private Sub cboExport_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExport.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboFOC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboRejection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejection.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub
    Private Sub chkAllInv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllInv.CheckStateChanged
        Call PrintStatus(False)
        If chkAllInv.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtINVType.Enabled = False
            cmdsearchInv.Enabled = False
        Else
            txtINVType.Enabled = True
            cmdsearchInv.Enabled = True
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
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Sales Register"
        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SALESREGSUMM.RPT"

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
    Private Sub cmdsearchInv_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchInv.Click
        SearchINVType()
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
    Private Sub frmParamSaleRegSumm_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = IIf(lblBookType.Text = "P", "Purchase", "Sales") & " Register (Summary)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleRegSumm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        cboAgtD3.Items.Clear()
        cboFOC.Items.Clear()
        cboRejection.Items.Clear()
        cboCancelled.Items.Clear()
        cboCT3.Items.Clear()
        cboShow.Items.Clear()
        cboGroup.Items.Clear()

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

        cboExport.Items.Add("BOTH")
        cboExport.Items.Add("YES")
        cboExport.Items.Add("NO")

        cboShow.Items.Add("All")
        cboShow.Items.Add("Only Excise")
        cboShow.Items.Add("Only Service Tax")
        cboShow.Items.Add("Only Cess")
        cboShow.Items.Add("Only W/o Excise")
        cboShow.Items.Add("Only W/o Service Tax")
        cboShow.Items.Add("Only W/o Cess")

        cboGroup.Items.Add("INVOICE TYPE")
        cboGroup.Items.Add("PARTY NAME")
        cboGroup.Items.Add("TARIFF HEADING")

        cboAgtD3.SelectedIndex = 0
        cboCT3.SelectedIndex = 0
        cboFOC.SelectedIndex = 0
        cboRejection.SelectedIndex = 0
        cboCancelled.SelectedIndex = 0
        cboExport.SelectedIndex = 0
        cboShow.SelectedIndex = 0
        cboGroup.SelectedIndex = 0

        optType(2).Checked = True

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdSearch.Enabled = False

        chkAllInv.CheckState = System.Windows.Forms.CheckState.Checked
        txtINVType.Enabled = False
        cmdsearchInv.Enabled = False

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamSaleRegSumm_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamSaleRegSumm_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        Dim mMKey As String
        Dim RsTemp As ADODB.Recordset
        Dim mStr1 As String
        Dim mStr2 As String
        Dim mStr As String

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
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

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
        Dim SqlStr As String


        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
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

            .Col = ColGroupField
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColGroupField, 25)
            .ColsFrozen = ColGroupField


            For cntCol = ColBillAmount To ColOthCharges
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .set_ColWidth(cntCol, 10)
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
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "S" Then
            SqlStr = MakeSQL
        Else
            SqlStr = MakeSQL ''MakeSQLPurchase
        End If

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
        Dim mPartyCode As String
        Dim mTrnCode As Integer
        Dim mTable As String
        Dim mSHEField As String

        Dim mCGSTField As String
        Dim mSGSTField As String
        Dim mIGSTField As String

        ''''SELECT CLAUSE...

        ''

        MakeSQL = " SELECT '', "

        mTable = IIf(lblBookType.Text = "S", "FIN_INVOICE_HDR", "FIN_PURCHASE_HDR")
        mSHEField = IIf(lblBookType.Text = "S", "IH.TOTSHECAMOUNT", "IH.SHECAMOUNT")

        mCGSTField = IIf(lblBookType.Text = "S", "IH.NETCGST_AMOUNT", "IH.TOTCGST_AMOUNT")
        mSGSTField = IIf(lblBookType.Text = "S", "IH.NETSGST_AMOUNT", "IH.TOTSGST_AMOUNT")
        mIGSTField = IIf(lblBookType.Text = "S", "IH.NETIGST_AMOUNT", "IH.TOTIGST_AMOUNT")

        If cboGroup.SelectedIndex = 0 Then
            MakeSQL = MakeSQL & vbCrLf & " INVMST.NAME, "
        ElseIf cboGroup.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " CMST.SUPP_CUST_NAME,"
        ElseIf cboGroup.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " IH.TARIFFHEADING,"
        End If

        '

        MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.NETVALUE))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE " & mCGSTField & " END))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE " & mSGSTField & " END))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,CASE WHEN CMST.GST_RGN_NO='" & RsCompany.Fields("COMPANY_GST_RGN_NO").Value & "' THEN 0 ELSE " & mIGSTField & " END))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOTEDAMOUNT)))  , " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOTSERVICEAMOUNT)))  , " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOTEDUAMOUNT))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0," & mSHEField & "))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOTTAXABLEAMOUNT))), " & vbCrLf & " TO_CHAR(IH.STPERCENT), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,DECODE(WITHIN_STATE,'Y',IH.TOTSTAMT,0))))," & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,DECODE(WITHIN_STATE,'N',IH.TOTSTAMT,0))))," & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,TOTMSCAMOUNT)))," & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,TO_CHAR(NVL(TOTDISCAMOUNT,0)+NVL(TOTFREIGHT,0)+NVL(TOTSURCHARGEAMT,0)+NVL(TOTCHARGES,0)+NVL(TOTRO,0)))))," & vbCrLf & " '' "

        ''''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM " & mTable & " IH, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST INVMST"

        ''''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVMST.CODE"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = MasterNo
            Else
                mPartyCode = "-1"
            End If

            MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
        End If

        If chkAllInv.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtINVType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & lblBookType.Text & "'") = True Then
                mTrnCode = MasterNo
            Else
                mTrnCode = CInt("-1")
            End If

            MakeSQL = MakeSQL & vbCrLf & " AND IH.TRNTYPE = " & mTrnCode & ""
        End If

        If optType(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_STATE='N'"
        End If

        If lblBookType.Text = "S" Then
            If cboAgtD3.SelectedIndex > 0 Then
                MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTD3='" & VB.Left(cboAgtD3.Text, 1) & "'"
            End If

            If cboCT3.SelectedIndex > 0 Then
                MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT3='" & VB.Left(cboCT3.Text, 1) & "'"
            End If

            If cboFOC.SelectedIndex > 0 Then
                MakeSQL = MakeSQL & vbCrLf & "AND IH.FOC='" & VB.Left(cboFOC.Text, 1) & "'"
            End If
        Else
            If cboFOC.SelectedIndex > 0 Then
                MakeSQL = MakeSQL & vbCrLf & "AND IH.ISFOC='" & VB.Left(cboFOC.Text, 1) & "'"
            End If

            MakeSQL = MakeSQL & vbCrLf & "AND IH.ISFINALPOST='Y'"
        End If

        If cboRejection.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboRejection.Text, 1) & "'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        If cboExport.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        ElseIf cboExport.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTSERVICEAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDUAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 5 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTSERVICEAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 6 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDUAMOUNT=0"
        End If

        If lblBookType.Text = "S" Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        '''GROUP BY

        If cboGroup.SelectedIndex = 0 Then
            MakeSQL = MakeSQL & vbCrLf & " GROUP BY INVMST.NAME, "
        ElseIf cboGroup.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " GROUP BY CMST.SUPP_CUST_NAME,"
        ElseIf cboGroup.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " GROUP BY IH.TARIFFHEADING,"
        End If

        MakeSQL = MakeSQL & " STPERCENT"
        ''''ORDER CLAUSE...

        If cboGroup.SelectedIndex = 0 Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY INVMST.NAME, "
        ElseIf cboGroup.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME,"
        ElseIf cboGroup.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY IH.TARIFFHEADING,"
        End If
        MakeSQL = MakeSQL & " STPERCENT"

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

        Dim mBillAmount As Double
        Dim mSaleAmount As Double
        Dim mBED As Double
        Dim mCess As Double
        Dim mCST As Double
        Dim mHGST As Double
        Dim mServiceTax As Double
        Dim mTaxableAmount As Double
        Dim mMSC As Double
        Dim mOthCharges As Double
        Dim mSHCess As Double
        Dim mCGST As Double
        Dim mSGST As Double
        Dim mIGST As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColBillAmount
                mBillAmount = mBillAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSaleAmount
                mSaleAmount = mSaleAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCGST
                mCGST = mCGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSGST
                mSGST = mSGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColIGST
                mIGST = mIGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))


                .Col = ColBED
                mBED = mBED + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCess
                mCess = mCess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSHCess
                mSHCess = mSHCess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCST
                mCST = mCST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColHGST
                mHGST = mHGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColMSC
                mMSC = mMSC + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColST
                mServiceTax = mServiceTax + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTaxableAmount
                mTaxableAmount = mTaxableAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColOthCharges
                mOthCharges = mOthCharges + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColGroupField)
            .Col = ColGroupField
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

            .Col = ColBillAmount
            .Text = VB6.Format(mBillAmount, "0.00")

            .Col = ColSaleAmount
            .Text = VB6.Format(mSaleAmount, "0.00")

            .Col = ColCGST
            .Text = VB6.Format(mCGST, "0.00")

            .Col = ColSGST
            .Text = VB6.Format(mSGST, "0.00")

            .Col = ColIGST
            .Text = VB6.Format(mIGST, "0.00")


            .Col = ColBED
            .Text = VB6.Format(mBED, "0.00")

            .Col = ColCess
            .Text = VB6.Format(mCess, "0.00")

            .Col = ColSHCess
            .Text = VB6.Format(mSHCess, "0.00")

            .Col = ColCST
            .Text = VB6.Format(mCST, "0.00")

            .Col = ColHGST
            .Text = VB6.Format(mHGST, "0.00")

            .Col = ColMSC
            .Text = VB6.Format(mMSC, "0.00")

            .Col = ColST
            .Text = VB6.Format(mServiceTax, "0.00")

            .Col = ColTaxableAmount
            .Text = VB6.Format(mTaxableAmount, "0.00")

            .Col = ColOthCharges
            .Text = VB6.Format(mOthCharges, "0.00")

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub SearchINVType()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & lblBookType.Text & "'"

        If MainClass.SearchGridMaster((txtINVType.Text), "FIN_INVTYPE_MST", "CODE", "NAME", , , SqlStr) = True Then
            txtINVType.Text = AcName
            txtINVType_Validating(txtINVType, New System.ComponentModel.CancelEventArgs(False))
            If txtINVType.Enabled = True Then txtINVType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtINVType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtINVType.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtINVType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtINVType.DoubleClick
        SearchINVType()
    End Sub
    Private Sub txtINVType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtINVType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtINVType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtINVType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtINVType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchINVType()
    End Sub
    Private Sub txtINVType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtINVType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String


        If txtINVType.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='" & lblBookType.Text & "'"

        If MainClass.ValidateWithMasterTable((txtINVType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtINVType.Text = UCase(Trim(txtINVType.Text))
        Else
            MsgInformation("No Such Invoice Type in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
