Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamPOHistory
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Dim cntSearchRow As Integer

    Private Const ColLocked As Short = 1
    Private Const ColPONo As Short = 2
    Private Const ColPODate As Short = 3
    Private Const ColPartyCode As Short = 4
    Private Const ColPartyName As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemDsc As Short = 7
    Private Const ColPartNo As Short = 8
    Private Const ColUOM As Short = 9

    Dim ColMKEY As Integer

    Dim mClickProcess As Boolean

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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
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
        mTitle = "Sales Register"
        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SALESREG.RPT"

        SqlStr = MakeSQL("P")

        'If MainClass.FillPrintDummyDataFromSprd(SprdMain, 0, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ReportErr
        'SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

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
        If FieldsVerification() = False Then Exit Sub

        'MainClass.ClearGrid(SprdMain, RowHeight)
        FillHeading("S")
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPOHistory_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillPOCombo()
        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        cboPurType.Items.Clear()
        cboPurType.Items.Add("ALL")
        cboPurType.Items.Add("Purchase Order")
        cboPurType.Items.Add("Work Order")
        cboPurType.Items.Add("Job Order")
        cboPurType.SelectedIndex = 0

        cboOrderType.Items.Clear()
        cboOrderType.Items.Add("ALL")
        cboOrderType.Items.Add("Close")
        cboOrderType.Items.Add("Open")
        cboOrderType.SelectedIndex = 0

        cboStatus.Items.Clear()
        cboStatus.Items.Add("BOTH")
        cboStatus.Items.Add("Approval")
        cboStatus.Items.Add("Non Approval")
        cboStatus.SelectedIndex = 0

        'cboShow.Items.Clear()
        'cboShow.Items.Add("ALL")
        'cboShow.Items.Add("Only Diff.")
        'cboShow.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmParamPOHistory_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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



        '    MainClass.FillCombo cboInvoiceType, "FIN_INVTYPE_MST", "NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'"

        '    cboInvoiceType.ListIndex = 0
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearchItem.Enabled = False

        Call FillPOCombo()
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FillHeading("L")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillHeading(ByRef pType As String)

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim SqlStr As String = ""
        Dim mRecordCount As Integer
        Dim mAmendNo As String
        Dim mWEF As String

        MainClass.ClearGrid(SprdMain, RowHeight)
        MainClass.ClearGrid(SprdHeading, RowHeight)

        With SprdMain
            .MaxCols = ColUOM
            mRecordCount = 0

            SqlStr = FillHeadingQry(pType)

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            mRecordCount = 0
            If RsTemp.EOF = False Then
                mRecordCount = 1
                Do While Not RsTemp.EOF

                    SprdHeading.Row = mRecordCount
                    SprdHeading.Col = 1
                    SprdHeading.Text = RsTemp.Fields("AMEND_NO").Value ''& "-" & VB6.Format(RsTemp.Fields("PO_WEF_DATE").Value, "DD/MM/YYYY")
                    SprdHeading.Col = 2
                    SprdHeading.Text = VB6.Format(RsTemp.Fields("PO_WEF_DATE").Value, "DD/MM/YYYY")
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        mRecordCount = mRecordCount + 1
                        SprdHeading.MaxRows = SprdHeading.MaxRows + 1
                    End If

                Loop
            End If

            .Row = 0
            ColMKEY = .MaxCols + mRecordCount + 1
            .MaxCols = ColMKEY

            If mRecordCount > 0 Then
                For cntCol = 1 To mRecordCount
                    SprdHeading.Row = cntCol
                    SprdHeading.Col = 1
                    mWEF = VB6.Format(Trim(SprdHeading.Text), "00")

                    SprdHeading.Col = 2
                    mWEF = mWEF & "-" & VB6.Format(SprdHeading.Text, "DD/MM/YYYY")

                    .Row = 0
                    .Col = ColUOM + cntCol
                    .Text = mWEF
                Next
            End If

            .Col = ColMKEY
            .Text = "Mkey"

            FormatSprdMain(-1)
        End With
    End Sub

    Private Function FillHeadingQry(ByRef pType As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = " Select DISTINCT AMEND_NO, ID.PO_WEF_DATE " & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, " & vbCrLf _
                    & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " And IH.MKEY=ID.MKEY" & vbCrLf _
                    & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                    & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
                    & " And IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                    & " And ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " And ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If cboPurType.Text <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.PUR_TYPE='" & VB.Left(cboPurType.Text, 1) & "'"
        End If

        If cboOrderType.Text <> "ALL" Then
            SqlStr = SqlStr & vbCrLf & "AND IH.ORDER_TYPE='" & VB.Left(cboOrderType.Text, 1) & "'"
        End If

        If pType = "L" Then
            SqlStr = SqlStr & vbCrLf & " AND 1=2"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY AMEND_NO, ID.PO_WEF_DATE"

        FillHeadingQry = SqlStr
        Exit Function
ErrPart:
        FillHeadingQry = ""
    End Function
    Private Sub frmParamPOHistory_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPOHistory_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
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

        SqlStr = " SELECT VEHICLENO, CARRIERS " & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR ='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND MKEY='" & mMKey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mStr1 = IIf(IsDBNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)
            mStr2 = IIf(IsDBNull(RsTemp.Fields("CARRIERS").Value), "", RsTemp.Fields("CARRIERS").Value)
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
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
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


            For cntCol = ColUOM + 1 To ColMKEY - 1
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                '            .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
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
        Dim SqlStr As String = ""
        Dim cntCol As Integer
        Dim cntRow As Integer
        Dim mFieldTitle As String
        Dim mMKey As String
        Dim mValue As Double
        'Dim mTotValue As Double
        Dim mCancelled As String

        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mGetFieldName As String
        Dim mGetFieldValue As Double

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL("")
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        FormatSprdMain(-1)

        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetExpenseAmount(ByRef pFieldTitle As String, ByRef pMKey As String, ByRef pCancelled As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetExpenseAmount = 0

        If pCancelled = "Y" Then
            Exit Function
        End If


        SqlStr = "SELECT EXP.AMOUNT " & vbCrLf & " FROM FIN_INVOICE_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf & " WHERE EXP.MKEY='" & pMKey & "'" & vbCrLf & " AND IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=IMST.CODE" & vbCrLf & " AND IMST.NAME='" & MainClass.AllowSingleQuote(pFieldTitle) & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetExpenseAmount = IIf(IsDBNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value)
        End If
        Exit Function
LedgError:
        GetExpenseAmount = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQL(pType As String) As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String = ""
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean
        ''SELECT CLAUSE...
        Dim cntCol As Integer
        Dim mStr As String
        Dim mDivision As Double
        Dim mHeadRow As Integer
        Dim mAmendNo As Double
        Dim mFieldName As String
        Dim mFieldValue As String
        Dim mWEF As String
        mHeadRow = 1


        mFieldName = ""
        mFieldValue = ""
        mStr = ""
        mHeadRow = 1
        'If pType = "" Then
        For cntCol = ColUOM + 1 To ColMKEY - 1
            SprdHeading.Row = mHeadRow
            SprdHeading.Col = 1
            mAmendNo = Val(SprdHeading.Text)

            SprdHeading.Col = 2
            mWEF = VB6.Format(SprdHeading.Text, "DD/MMM/YYYY")

            mFieldName = "FIELD" & mHeadRow
            mStr = mStr & IIf(mStr = "", "", ",")

            mStr = mStr & vbCrLf _
                    & "MAX((SELECT TO_CHAR((100-ITEM_DIS_PER)*ITEM_PRICE/100) AS " & "FIELD" & mHeadRow & " " & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR A, PUR_PURCHASE_DET B" & vbCrLf _
                    & " WHERE A.MKEY=B.MKEY AND A.MKEY=IH.MKEY AND B.ITEM_CODE=ID.ITEM_CODE" & vbCrLf _
                    & " AND A.AMEND_NO=" & mAmendNo & " " & vbCrLf _
                    & " AND B.PO_WEF_DATE=TO_DATE('" & VB6.Format(mWEF, "DD/MMM/YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " )) AS " & "FIELD" & mHeadRow & ""

            mHeadRow = mHeadRow + 1
        Next

        mStr = mStr & IIf(mStr = "", "", ",")

        MakeSQL = " SELECT DISTINCT ''," & vbCrLf _
            & " IH.AUTO_KEY_PO," & vbCrLf _
            & " TO_CHAR(IH.PUR_ORD_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IH.SUPP_CUST_CODE," & vbCrLf _
            & " CMST.SUPP_CUST_NAME," & vbCrLf _
            & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO," & vbCrLf _
            & " ID.ITEM_UOM, "

        MakeSQL = MakeSQL & mStr

        MakeSQL = MakeSQL & vbCrLf & " IH.AUTO_KEY_PO "

        ''''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID," & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "

        MakeSQL = MakeSQL & vbCrLf _
            & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If cboPurType.Text <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PUR_TYPE='" & VB.Left(cboPurType.Text, 1) & "'"
        End If

        If cboOrderType.Text <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ORDER_TYPE='" & VB.Left(cboOrderType.Text, 1) & "'"
        End If

        'If cboShow.Text <> "ALL" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND (100-ITEM_DIS_PER)*ITEM_PRICE/100<>GETLASTPORATE(IH.COMPANY_CODE, IH.AUTO_KEY_PO,AMEND_NO,ID.ITEM_CODE)"
        'End If

        'If chkShowOnlyAmend.CheckState = System.Windows.Forms.CheckState.Checked Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.AMEND_NO<>0"
        'End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PO_STATUS='Y'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PO_STATUS='N'"
        End If

        '    MakeSQL = MakeSQL & vbCrLf & "AND  IH.PO_CLOSED='N'"

        ''''GROUP BY CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " GROUP BY IH.AUTO_KEY_PO," & vbCrLf _
            & " TO_CHAR(IH.PUR_ORD_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " IH.SUPP_CUST_CODE," & vbCrLf _
            & " CMST.SUPP_CUST_NAME," & vbCrLf _
            & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO," & vbCrLf _
            & " ID.ITEM_UOM, IH.AUTO_KEY_PO "


        ''''ORDER CLAUSE...
        If optPONo.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.AUTO_KEY_PO,IH.SUPP_CUST_CODE, TO_CHAR(IH.PUR_ORD_DATE,'DD/MM/YYYY')"
        ElseIf optItemDesc.Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IH.AUTO_KEY_PO, TO_CHAR(IH.PUR_ORD_DATE,'DD/MM/YYYY')"
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

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
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
    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdsearchItem.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearchItem.Enabled = True
        End If
    End Sub
    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub


    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
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
        Dim SqlStr As String

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


    Private Sub cmdsearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchItem.Click
        SearchItem()
    End Sub

End Class
