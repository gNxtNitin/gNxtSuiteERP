Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamGSTDebitReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Private PvtDBCn As ADODB.Connection

    Dim mAccountCode As Integer
    Private Const ColItemDesc As Short = 1
    Private Const ColPartyName As Short = 2
    Private Const ColOpBal As Short = 3
    Private Const ColProd As Short = 4
    Private Const ColClearance As Short = 5
    Private Const ColReProcess As Short = 6
    Private Const ColClBal As Short = 7
    Private Const ColRate As Short = 8
    Private Const ColAmount As Short = 9
    Private Const ColCGSTAmount As Short = 10
    Private Const ColSGSTAmount As Short = 11
    Private Const ColIGSTAmount As Short = 12
    Private Const ColTotCD As Short = 13
    Private Const ColTotCDCess As Short = 14


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboInvoiceType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvoiceType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboInvoiceType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboInvoiceType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If cboInvoiceType.Text = "ALL" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((cboInvoiceType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblTrnType.Text = MasterNo
        Else
            lblTrnType.Text = CStr(-1)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub chkTariff_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTariff.CheckStateChanged
        Call PrintStatus(False)
        If chkTariff.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTariff.Enabled = False
            cmdTariff.Enabled = False
        Else
            txtTariff.Enabled = True
            cmdTariff.Enabled = True
        End If
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        PubDBCn.Errors.Clear()


        'If TxtName.Text = "" Then Exit Sub

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertPrintDummy()


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "RT12 DETAIL FOR THE MONTH OF " & VB6.Format(txtDateTo.Text, "MMMM, YYYY")
        mTitle = mTitle & " UPTO  " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        mSubTitle = Trim(txtTariff.Text)

        mRPTName = "RT12Detail.Rpt"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        ''Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, , , "Y")
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub InsertPrintDummy()


        On Error GoTo ERR1
        Dim mItemDesc As String
        Dim mPartyName As String
        Dim mQty1 As String
        Dim mValue1 As String
        Dim mExcise1 As String
        Dim mQty2 As String
        Dim mValue2 As String
        Dim mExcise2 As String
        Dim mOPBal As String
        Dim mProd As String
        Dim mClearance As String
        Dim mCLBal As String
        Dim mRate As String
        Dim mAmount As String
        Dim mTotExcise As String
        Dim mTotCess As String
        Dim mTotSHECess As String
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mTotCD As Double
        Dim mTotCDCess As Double
        Dim mReprocessing As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColPartyName
                mPartyName = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColOpBal
                mOPBal = Trim(.Text)

                .Col = ColProd
                mProd = Trim(.Text)

                .Col = ColClearance
                mClearance = Trim(.Text)

                .Col = ColReProcess
                mReprocessing = Trim(.Text)

                .Col = ColClBal
                mCLBal = Trim(.Text)

                .Col = ColRate
                mRate = Trim(.Text)

                .Col = ColAmount
                mAmount = Trim(.Text)

                .Col = ColCGSTAmount
                mTotExcise = Trim(.Text)

                .Col = ColSGSTAmount
                mTotCess = Trim(.Text)

                .Col = ColIGSTAmount
                mTotSHECess = Trim(.Text)

                .Col = ColTotCD
                mTotCD = CDbl(Trim(.Text))

                .Col = ColTotCDCess
                mTotCDCess = CDbl(Trim(.Text))

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow," & vbCrLf & " Field1,Field2,Field3,Field4,Field5, " & vbCrLf & " Field6,Field7,Field8,Field9,Field10 ," & vbCrLf & " Field11,Field12,Field13,Field14,Field15,Field16,Field17,Field18,Field19,field20 " & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mItemDesc & "', " & vbCrLf & " '" & mPartyName & "', " & vbCrLf & " '" & mQty1 & "', " & vbCrLf & " '" & mValue1 & "', " & vbCrLf & " '" & mExcise1 & "', " & vbCrLf & " '" & mQty2 & "', " & vbCrLf & " '" & mValue2 & "', " & vbCrLf & " '" & mExcise2 & "', " & vbCrLf & " '" & mOPBal & "', " & vbCrLf & " '" & mProd & "', " & vbCrLf & " '" & mClearance & "', " & vbCrLf & " '" & mCLBal & "', " & vbCrLf & " '" & mRate & "', " & vbCrLf & " '" & mAmount & "', " & vbCrLf & " '" & mTotExcise & "', '" & mTotCess & "'," & vbCrLf & " '" & mTotSHECess & "','" & mTotCD & "','" & mTotCDCess & "','" & mReprocessing & "') "

                PubDBCn.Execute(SqlStr)
            Next

        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call PrintStatus(False)
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
        ErrorMsg(Err.Description, Err.Number & "-Main", MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdTariff_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTariff.Click
        Call PrintStatus(False)
        SearchTariff()
    End Sub

    Private Sub frmParamGSTDebitReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Monthly Despatch Summary (GST)"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamGSTDebitReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim mLastDate As String
        Dim mStartDate As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn


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
        MainClass.FillCombo(cboInvoiceType, "FIN_INVTYPE_MST", "NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") ', "BranchCode=" & RsCompany.fields("CBRANCHCODE & ""

        cboInvoiceType.SelectedIndex = 0
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        chkTariff.CheckState = System.Windows.Forms.CheckState.Checked

        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        TxtItemName.Enabled = False
        cmdSearchItem.Enabled = False

        txtTariff.Enabled = False
        cmdTariff.Enabled = False

        Call PrintStatus(True)
        '' txtDateFrom = Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")

        mStartDate = "01/" & Month(RunDate) & "/" & Year(RunDate)
        mLastDate = MainClass.LastDay(Month(RunDate), Year(RunDate)) & "/" & Month(RunDate) & "/" & Year(RunDate)

        txtDateFrom.Text = VB6.Format(mStartDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(mLastDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamGSTDebitReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamGSTDebitReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.hide()
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

    Private Sub SearchTariff()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster TxtAccount, "FIN_TARRIF_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(txtTariff.Text, "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr)
        If AcName <> "" Then
            txtTariff.Text = AcName
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
            .MaxCols = ColTotCDCess
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 15)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)
            .ColsFrozen = ColPartyName

            For cntCol = ColOpBal To ColTotCDCess
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
            Next

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            '        SprdMain.DAutoCellTypes = True
            '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim mCATEGORY_CODE As String
        '    mCATEGORY_CODE = "('008','015','017')"

        mCATEGORY_CODE = GetER1CategoryCode

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_RT12 NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr1 = "INSERT INTO TEMP_RT12 ( " & vbCrLf & " UserId,ITEM_DESC, PARTYNAME, " & vbCrLf & " QNTY1, VALUE1, ED1, " & vbCrLf & " QNTY2, VALUE2, ED2, " & vbCrLf & " OPBAL, PRODUCTION, DESPATCHED, RE_PROCESS_QTY, " & vbCrLf & " CLOSINGBAL, Rate, Amount, TOTALED,TOTALCESS, TOTALSHECESS, TOTALCD, TOTALCD_CESS) " & vbCrLf
        SqlStr = SqlStr1 & vbCrLf & OPQryForInsert(mCATEGORY_CODE)
        PubDBCn.Execute(SqlStr)

        SqlStr = SqlStr1 & vbCrLf & ProdQryForInsert(mCATEGORY_CODE, True)
        PubDBCn.Execute(SqlStr)

        SqlStr = SqlStr1 & vbCrLf & DespatchQryForInsert(True)
        PubDBCn.Execute(SqlStr)

        SqlStr = SqlStr1 & vbCrLf & ProdQryForInsert(mCATEGORY_CODE, False)
        PubDBCn.Execute(SqlStr)

        SqlStr = SqlStr1 & vbCrLf & DespatchQryForInsert(False)
        PubDBCn.Execute(SqlStr)

        SqlStr = SqlStr1 & vbCrLf & FGScrapQryForInsert(True)
        PubDBCn.Execute(SqlStr)

        SqlStr = SqlStr1 & vbCrLf & FGScrapQryForInsert(False)
        PubDBCn.Execute(SqlStr)

        SqlStr = SqlStr1 & vbCrLf & ReProcessQryForInsert(True)
        PubDBCn.Execute(SqlStr)

        SqlStr = SqlStr1 & vbCrLf & ReProcessQryForInsert(False)
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        ''TO_CHAR(SUM(Rate))
        SqlStr = ""
        SqlStr = "SELECT " & vbCrLf & " ITEM_DESC, MAX(PARTYNAME), " & vbCrLf & " TO_CHAR(SUM(OPBAL)), TO_CHAR(SUM(PRODUCTION)), TO_CHAR(SUM(DESPATCHED)), TO_CHAR(SUM(RE_PROCESS_QTY))," & vbCrLf & " TO_CHAR(SUM(CLOSINGBAL)), TO_CHAR(MAX(Rate)), TO_CHAR(SUM(Amount)), TO_CHAR(SUM(TOTALED)), " & vbCrLf & " TO_CHAR(SUM(TOTALCESS)), TO_CHAR(SUM(TOTALSHECESS)),TO_CHAR(SUM(TOTALCD)),TO_CHAR(SUM(TOTALCD_CESS)) " & vbCrLf & " FROM TEMP_RT12 " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        If chkHide.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " HAVING SUM(OPBAL)+SUM(PRODUCTION)+SUM(DESPATCHED)+SUM(RE_PROCESS_QTY)+SUM(CLOSINGBAL)<>0"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_DESC"
        SqlStr = SqlStr & vbCrLf & " ORDER BY ITEM_DESC"
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")


        ''& " TO_CHAR(SUM(QNTY1)), TO_CHAR(SUM(VALUE1)), TO_CHAR(SUM(ED1)), " & vbCrLf _
        '& " TO_CHAR(SUM(QNTY2)), TO_CHAR(SUM(VALUE2)), TO_CHAR(SUM(ED2)), " & vbCrLf _
        '

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        '    Resume
        Show1 = False
        ErrorMsg(Err.Description, Err.Number & "Show Err", MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Function

    Private Function DespatchQryForInsert(ByRef IsOpening As Boolean) As String

        On Error GoTo ERR1
        Dim mSqlStr As String

        ''SELECT CLAUSE...
        If IsOpening = True Then
            mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ID.ITEM_CODE || '-' || IMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_NAME," & vbCrLf & " 0,0, 0,0,0,0," & vbCrLf & " SUM(ITEM_QTY)*-1 As OpBal, " & vbCrLf & " 0, " & vbCrLf & " 0 AS Clearance, 0 as RE_PROCESS," & vbCrLf & " 0 AS ClBal," & vbCrLf & " ITEM_RATE, " & vbCrLf & " 0 As AMOUNT,  " & vbCrLf & " 0 AS TotExcise,0 AS CESS, 0 AS SHECESS,0,0 "

        Else
            mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ID.ITEM_CODE || '-' || IMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_NAME," & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, 0, 0, " & vbCrLf & " 0 As OpBal, " & vbCrLf & " 0 AS Prod, " & vbCrLf & " SUM(ITEM_QTY) AS Clearance, 0 as RE_PROCESS," & vbCrLf & " 0 AS ClBal," & vbCrLf & " ITEM_RATE, " & vbCrLf & " SUM(ITEM_AMT) As AMOUNT,  " & vbCrLf & " SUM(NETCGST_AMOUNT),SUM(NETSGST_AMOUNT),SUM(NETIGST_AMOUNT)," & vbCrLf & " SUM(DECODE(ITEMVALUE,0,0,ITEM_AMT * TOT_CUSTOMDUTY/ITEMVALUE)), " & vbCrLf & " SUM(DECODE(ITEMVALUE,0,0,ITEM_AMT * TOT_CD_CESS/ITEMVALUE)) "
        End If

        ''    & " SUM(ITEM_AMT), " & vbCrLf _
        '& " SUM(ITEM_ED), " & vbCrLf _
        '
        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST, FIN_INVTYPE_MST INVMST "

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVMST.CODE" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf & " AND CANCELLED='N' AND REJECTION='N' AND AGTD3='N'"

        '    mSqlStr = mSqlStr & vbCrLf & "AND IH.TRNTYPE IN (1000002,1000008,1000012,1000102)"

        mSqlStr = mSqlStr & vbCrLf & " AND INVMST.ISSALECOMP='Y' "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If
            mSqlStr = mSqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        If chkTariff.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & " AND IMST.ITEM_SHORT_DESC = '" & MainClass.AllowSingleQuote(Trim(TxtItemName.Text)) & "'"
        End If

        If cboInvoiceType.Text <> "ALL" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.TRNTYPE=" & Val(lblTrnType.Text) & ""
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 5 Then
            mSqlStr = mSqlStr & vbCrLf & "AND IH.TOTEDAMOUNT>=0 "
        Else
            mSqlStr = mSqlStr & vbCrLf & "AND DECODE(CMST.WITHIN_COUNTRY,'Y',DECODE(AGTCT3,'Y',1,DECODE(TYPE_OF_SUPPLIER,'100% EOU',1,DECODE(AGTCT1,'Y',1,(IH.NETCGST_AMOUNT+IH.NETSGST_AMOUNT+IH.NETIGST_AMOUNT)))),1)>0 "

            '        mSqlStr = mSqlStr & vbCrLf & "AND DECODE(CMST.WITHIN_COUNTRY,'Y',DECODE(AGTCT3,'Y',1,DECODE(TYPE_OF_SUPPLIER,'100% EOU',1,DECODE(AGTCT1,'Y',1,IH.TOTEDAMOUNT))),1)>0 "
        End If

        If IsOpening = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.INVOICE_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''GROUP CLAUSE...


        mSqlStr = mSqlStr & vbCrLf & "GROUP BY ID.ITEM_CODE || '-' || IMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_NAME,ITEM_RATE"

        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.ITEM_CODE || '-' || IMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_NAME"

        DespatchQryForInsert = mSqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function ReProcessQryForInsert(ByRef IsOpening As Boolean) As String

        On Error GoTo ERR1
        Dim mSqlStr As String

        ''SELECT CLAUSE...
        If IsOpening = True Then
            mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " IH.PRODUCT_CODE || '-' || INVMST.ITEM_SHORT_DESC, ''," & vbCrLf & " 0,0, 0,0,0,0," & vbCrLf & " SUM(PROD_QTY)*-1 As OpBal, " & vbCrLf & " 0, " & vbCrLf & " 0 AS Clearance, 0 as RE_PROCESS," & vbCrLf & " 0 AS ClBal," & vbCrLf & " 0, " & vbCrLf & " 0 As AMOUNT,  " & vbCrLf & " 0 AS TotExcise,0 AS CESS, 0 AS SHECESS,0,0 "

        Else
            mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " IH.PRODUCT_CODE || '-' || INVMST.ITEM_SHORT_DESC,''," & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, 0, 0, " & vbCrLf & " 0 As OpBal, " & vbCrLf & " 0 AS Prod, " & vbCrLf & " 0 AS Clearance, SUM(PROD_QTY) as RE_PROCESS," & vbCrLf & " 0 AS ClBal," & vbCrLf & " 0, " & vbCrLf & " 0 As AMOUNT,  " & vbCrLf & " 0 AS TotExcise,0,0," & vbCrLf & " 0, " & vbCrLf & " 0 "
        End If

        ''    & " SUM(ITEM_AMT), " & vbCrLf _
        '& " SUM(ITEM_ED), " & vbCrLf _
        '
        ''FROM CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & " FROM PRD_FGBREAKUP_HDR IH, INV_ITEM_MST INVMST "

        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_REF,LENGTH(IH.AUTO_KEY_REF)-5,4)='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.BOOKTYPE='F'"

        '    If chkAll.Value = vbUnchecked Then
        '        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            lblAcCode.Caption = MasterNo
        '        Else
        '            lblAcCode.Caption = "-1"
        '        End If
        '        mSqlStr = mSqlStr & vbCrLf & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        '    End If

        If chkTariff.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & " AND INVMST.TARIFF_CODE='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC = '" & MainClass.AllowSingleQuote(Trim(TxtItemName.Text)) & "'"
        End If

        '    If cboInvoiceType.Text <> "ALL" Then
        '        mSqlStr = mSqlStr & vbCrLf & " AND IH.TRNTYPE=" & Val(lblTrnType.Caption) & ""
        '    End If

        '    If RsCompany.Fields("COMPANY_CODE").Value = 5 Then
        '        mSqlStr = mSqlStr & vbCrLf & "AND IH.TOTEDAMOUNT>=0 "
        '    Else
        '        mSqlStr = mSqlStr & vbCrLf & "AND DECODE(CMST.WITHIN_COUNTRY,'Y',DECODE(AGTCT3,'Y',1,DECODE(TYPE_OF_SUPPLIER,'100% EOU',1,IH.TOTEDAMOUNT)),1)>0 "
        '    End If

        If IsOpening = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.REF_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''GROUP CLAUSE...


        mSqlStr = mSqlStr & vbCrLf & "GROUP BY IH.PRODUCT_CODE || '-' || INVMST.ITEM_SHORT_DESC"

        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.PRODUCT_CODE || '-' || INVMST.ITEM_SHORT_DESC"

        ReProcessQryForInsert = mSqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FGScrapQryForInsert(ByRef IsOpening As Boolean) As String

        On Error GoTo ERR1
        Dim mSqlStr As String

        ''SELECT CLAUSE...
        If IsOpening = True Then
            mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ID.ITEM_CODE || '-' || INVMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_NAME," & vbCrLf & " 0,0, 0,0,0,0," & vbCrLf & " SUM(RTN_QTY)*-1 As OpBal, " & vbCrLf & " 0, " & vbCrLf & " 0 AS Clearance, 0 as RE_PROCESS," & vbCrLf & " 0 AS ClBal," & vbCrLf & " 0, " & vbCrLf & " 0 As AMOUNT,  " & vbCrLf & " 0 AS TotExcise,0 AS CESS, 0 AS SHECESS,0,0 "

        Else
            mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " ID.ITEM_CODE || '-' || INVMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_NAME," & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, 0, 0, " & vbCrLf & " 0 As OpBal, " & vbCrLf & " 0 AS Prod, " & vbCrLf & " SUM(RTN_QTY) AS Clearance, 0 as RE_PROCESS," & vbCrLf & " 0 AS ClBal," & vbCrLf & " 0, " & vbCrLf & " 0 As AMOUNT,  " & vbCrLf & " 0 AS TotExcise,0,0," & vbCrLf & " 0, " & vbCrLf & " 0 "
        End If

        ''    & " SUM(ITEM_AMT), " & vbCrLf _
        '& " SUM(ITEM_ED), " & vbCrLf _
        '
        ''FROM CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & " FROM INV_SRN_HDR IH, INV_SRN_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST "

        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_SRN,LENGTH(IH.AUTO_KEY_SRN)-5,4)='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND ID.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.STATUS='Y' AND IH.BOOKTYPE='S' AND IH.BOOKSUBTYPE='F' AND ID.FROM_STOCK_TYPE='FG'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If
            mSqlStr = mSqlStr & vbCrLf & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        If chkTariff.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & " AND INVMST.TARIFF_CODE='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC = '" & MainClass.AllowSingleQuote(Trim(TxtItemName.Text)) & "'"
        End If

        '    If cboInvoiceType.Text <> "ALL" Then
        '        mSqlStr = mSqlStr & vbCrLf & " AND IH.TRNTYPE=" & Val(lblTrnType.Caption) & ""
        '    End If

        '    If RsCompany.Fields("COMPANY_CODE").Value = 5 Then
        '        mSqlStr = mSqlStr & vbCrLf & "AND IH.TOTEDAMOUNT>=0 "
        '    Else
        '        mSqlStr = mSqlStr & vbCrLf & "AND DECODE(CMST.WITHIN_COUNTRY,'Y',DECODE(AGTCT3,'Y',1,DECODE(TYPE_OF_SUPPLIER,'100% EOU',1,IH.TOTEDAMOUNT)),1)>0 "
        '    End If

        If IsOpening = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.SRN_DATE<TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.SRN_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND IH.SRN_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''GROUP CLAUSE...


        mSqlStr = mSqlStr & vbCrLf & "GROUP BY ID.ITEM_CODE || '-' || INVMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_NAME"

        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.ITEM_CODE || '-' || INVMST.ITEM_SHORT_DESC, CMST.SUPP_CUST_NAME"

        FGScrapQryForInsert = mSqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function OPQryForInsert(ByRef pCATEGORY_CODE As String) As String

        On Error GoTo ERR1
        Dim mSqlStr As String
        Dim mTHField As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " RM.ITEM_CODE || '-' || IM.ITEM_SHORT_DESC, ''," & vbCrLf & " 0 , 0,0,0,0,0," & vbCrLf & " SUM(ITEM_OP) AS OPQty,0,0,0,0,0,0,0,0,0 ,0,0" & vbCrLf & " FROM FIN_RGOP_MST RM, INV_ITEM_MST IM"

        If RsCompany.Fields("FYEAR").Value <= 2004 Then
            mSqlStr = mSqlStr & vbCrLf & ", FIN_TARRIF_MST TH"
        End If

        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " RM.COMPANY_CODE=IM.Company_Code" & vbCrLf & " AND RM.ITEM_CODE=IM.ITEM_CODE" & vbCrLf & " AND RM.COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND RM.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "


        If RsCompany.Fields("FYEAR").Value <= 2004 Then
            mSqlStr = mSqlStr & vbCrLf & " AND IM.COMPANY_CODE= TH.COMPANY_CODE" & vbCrLf & " AND IM.TARIFF_CODE= TH.TARRIF_CODE"

            If chkTariff.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mSqlStr = mSqlStr & vbCrLf & " AND TH.MAIN_TARRIF_CODE='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
            End If
        Else
            If chkTariff.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                mSqlStr = mSqlStr & vbCrLf & " AND IM.TARIFF_CODE='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
            End If
        End If

        '    If chkTariff.Value = vbUnchecked Then
        ''        mSqlStr = mSqlStr & vbCrLf & "AND IM.TARIFF_CODE='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
        '        mTHField = IIf(RsCompany.Fields("FYEAR").Value <= 2004, "TH.MAIN_TARRIF_CODE", "TH.TARRIF_CODE")
        '        mSqlStr = mSqlStr & vbCrLf & "AND " & mTHField & "='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
        '    End If

        If Trim(pCATEGORY_CODE) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & "AND IM.CATEGORY_CODE IN " & pCATEGORY_CODE & ""
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & "AND IM.ITEM_SHORT_DESC = '" & MainClass.AllowSingleQuote(Trim(TxtItemName.Text)) & "'"
        End If

        mSqlStr = mSqlStr & vbCrLf & "HAVING SUM(ITEM_OP)<>0"

        ''GROUP CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "GROUP BY RM.ITEM_CODE || '-' || IM.ITEM_SHORT_DESC"

        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY RM.ITEM_CODE || '-' || IM.ITEM_SHORT_DESC"

        ''

        OPQryForInsert = mSqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function ProdQryForInsert(ByRef pCATEGORY_CODE As String, ByRef IsOpening As Boolean) As String

        On Error GoTo ERR1
        Dim mSqlStr As String
        ''SELECT CLAUSE...

        mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', " & vbCrLf & " IM.ITEM_CODE || '-' || IM.ITEM_SHORT_DESC, ''," & vbCrLf & " 0 , 0,0,0,0,0,"

        If IsOpening = True Then
            mSqlStr = mSqlStr & vbCrLf & " SUM(ITEM_QTY) As Prod,0,0, "
        Else
            mSqlStr = mSqlStr & vbCrLf & " 0, SUM(ITEM_QTY) As Prod,0, "
        End If

        mSqlStr = mSqlStr & vbCrLf & " 0 AS RE_PROCESS, 0 AS ClBal," & vbCrLf & " 0 , " & vbCrLf & " 0 As AMOUNT,  " & vbCrLf & " 0 AS TotExcise,0 AS CESS, 0 As SHECESS, 0, 0 " & vbCrLf & " FROM FIN_RGDAILYMANU_HDR RM, INV_ITEM_MST IM " & vbCrLf & " WHERE " & vbCrLf & " RM.COMPANY_CODE=IM.Company_Code" & vbCrLf & " AND RM.ITEM_CODE=IM.ITEM_CODE" & vbCrLf & " AND RM.COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND RM.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "


        If chkTariff.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & "AND RM.TARIFF_CODE='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
        End If


        If Trim(pCATEGORY_CODE) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & "AND IM.CATEGORY_CODE IN " & pCATEGORY_CODE & ""
        End If

        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSqlStr = mSqlStr & vbCrLf & "AND IM.ITEM_SHORT_DESC = '" & MainClass.AllowSingleQuote(Trim(TxtItemName.Text)) & "'"
        End If

        mSqlStr = mSqlStr & vbCrLf & "AND UPDATEFLAG='Y'"

        If IsOpening = True Then
            mSqlStr = mSqlStr & vbCrLf & " AND RM.MDATE < TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND RM.MDATE BETWEEN TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        mSqlStr = mSqlStr & vbCrLf & "HAVING SUM(ITEM_QTY)>0"

        ''GROUP CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "GROUP BY IM.ITEM_CODE || '-' || IM.ITEM_SHORT_DESC"

        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IM.ITEM_CODE || '-' || IM.ITEM_SHORT_DESC"

        ProdQryForInsert = mSqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        ''If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        ''If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
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


        Dim mTotOpBal As Double
        Dim mTotProd As Double
        Dim mTotClearance As Double
        Dim mTotClBal As Double
        Dim mRate As Double
        Dim mTotAmount As Double
        Dim mTotExcise As Double

        Dim mReProcess As Double
        Dim mTotReProcess As Double

        Dim mClearance As Double
        Dim mOPBal As Double
        Dim mProd As Double
        Dim mCLBal As Double
        Dim mExcise As Double
        Dim mAmount As Double
        Dim mTotCess As Double
        Dim mCess As Double

        Dim mTotSHECess As Double
        Dim mSHECess As Double
        Dim mTotCDCess As Double
        Dim mCDCess As Double
        Dim mTotCD As Double
        Dim mCD As Double
        Dim mTotRate As Double

        Dim mItemCode As String
        Dim mPurchaseCost As Double
        Dim mLandedcost As Double
        Dim mItemUOM As String
        Dim mItemDesc As String
        Dim mLen As Integer

        With SprdMain
            For cntRow = 1 To .MaxRows
                mClearance = 0
                mOPBal = 0
                mProd = 0
                mCLBal = 0
                mExcise = 0
                mAmount = 0
                mItemCode = ""
                mItemUOM = ""

                .Row = cntRow

                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                mLen = InStr(1, mItemDesc, "-")
                If mLen > 0 Then
                    mItemCode = Trim(Mid(mItemDesc, 1, mLen - 1))
                End If

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemUOM = MasterNo
                End If

                .Col = ColOpBal
                mOPBal = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotOpBal = mTotOpBal + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColProd
                mProd = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotProd = mTotProd + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColClearance
                mClearance = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotClearance = mTotClearance + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColReProcess
                mReProcess = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotReProcess = mTotReProcess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColClBal
                .Text = CStr(mOPBal + mProd - mClearance - mReProcess)
                mTotClBal = mTotClBal + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColRate
                '            If GetLatestItemCostFromPO(pItemCode, xPurchaseCost, xLandedCost, pRefDate, xStockType, pPartyCode, pItemUOM, mFactor) = False Then GoTo UpdateStockTRNErr


                mRate = GetLatestItemCostFromMRR(mItemCode, mItemUOM, 1, VB6.Format(txtDateTo.Text, "DD/MM/YYYY"), "S", "FG")
                .Text = VB6.Format(mRate, "0.000")
                mTotRate = mTotRate + mRate

                .Col = ColAmount
                mAmount = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotAmount = mTotAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCGSTAmount
                mExcise = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotExcise = mTotExcise + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSGSTAmount
                mCess = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotCess = mTotCess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColIGSTAmount
                mSHECess = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotSHECess = mTotSHECess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTotCD
                mCD = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotCD = mTotCD + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColTotCDCess
                mCDCess = Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))
                mTotCDCess = mTotCDCess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColItemDesc)
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

            .Col = ColOpBal
            .Text = VB6.Format(mTotOpBal, "0.00")

            .Col = ColProd
            .Text = VB6.Format(mTotProd, "0.00")

            .Col = ColClearance
            .Text = VB6.Format(mTotClearance, "0.00")

            .Col = ColReProcess
            .Text = VB6.Format(mTotReProcess, "0.00")

            .Col = ColClBal
            .Text = VB6.Format(mTotClBal, "0.00")

            .Col = ColRate
            .Text = VB6.Format(mTotRate, "0.00")

            .Col = ColAmount
            .Text = VB6.Format(mTotAmount, "0.00")

            .Col = ColCGSTAmount
            .Text = VB6.Format(mTotExcise, "0.00")

            .Col = ColSGSTAmount
            .Text = VB6.Format(mTotCess, "0.00")

            .Col = ColIGSTAmount
            .Text = VB6.Format(mTotSHECess, "0.00")

            .Col = ColTotCD
            .Text = VB6.Format(mTotCD, "0.00")

            .Col = ColTotCDCess
            .Text = VB6.Format(mTotCDCess, "0.00")

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number & " - Calc Total", MsgBoxStyle.Critical)
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

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchItem.Click
        SearchItem()
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
    Private Sub chkAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllItem.CheckStateChanged
        Call PrintStatus(False)
        If chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdSearchItem.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdSearchItem.Enabled = True
        End If
    End Sub

    Private Sub txtTariff_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtTariff_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.DoubleClick
        SearchTariff()
    End Sub


    Private Sub txtTariff_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTariff.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTariff.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTariff_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTariff.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    End Sub


    Private Sub txtTariff_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTariff.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtTariff.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.ValidateWithMasterTable((txtTariff.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Tariff in the Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
