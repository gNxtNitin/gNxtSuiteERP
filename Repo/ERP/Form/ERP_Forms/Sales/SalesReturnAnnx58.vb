Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSalesReturnAnnx58
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    'Private PvtDBCn As ADODB.Connection

    Dim mAccountCode As String
    Private Const Col1 As Short = 1
    Private Const col2 As Short = 2
    Private Const Col3 As Short = 3
    Private Const Col4 As Short = 4
    Private Const Col5 As Short = 5
    Private Const Col6 As Short = 6
    Private Const Col7 As Short = 7
    Private Const Col8 As Short = 8
    Private Const Col9 As Short = 9
    Private Const Col10 As Short = 10
    Private Const Col11 As Short = 11
    Private Const Col12 As Short = 12
    Private Const Col13 As Short = 13
    Private Const Col14 As Short = 14
    Private Const Col15 As Short = 15
    Private Const Col16 As Short = 16

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mBackColor As Object
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
    Private Sub txtTariff_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtTariff_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.DoubleClick
        SearchTariff()
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

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub InsertPrintDummy()


        On Error GoTo ERR1
        Dim m1 As String
        Dim m2 As String
        Dim m3 As String
        Dim m4 As String
        Dim m5 As String
        Dim m6 As String
        Dim m7 As String
        Dim m8 As String
        Dim m9 As String
        Dim m10 As String
        Dim m11 As String
        Dim m12 As String
        Dim m13 As String
        Dim m14 As String
        Dim m15 As String
        Dim m16 As String
        Dim m17 As String
        Dim SqlStr As String = ""
        Dim cntRow As Integer


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = Col1
                m1 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = col2
                m2 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = Col3
                m3 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = Col4
                m4 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = Col5
                m5 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = Col6
                m6 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = Col7
                m7 = Trim(.Text)
                .Col = Col8
                m8 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = Col9
                m9 = Trim(.Text)
                .Col = Col10
                m10 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = Col11
                m11 = MainClass.AllowSingleQuote(Trim(.Text))
                .Col = Col12
                m12 = Trim(.Text)
                .Col = Col13
                m13 = Trim(.Text)
                .Col = Col14
                m14 = Trim(.Text)
                .Col = Col15
                m15 = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = Col16
                m16 = MainClass.AllowSingleQuote(Trim(.Text))


                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow," & vbCrLf & " Field1,Field2,Field3,Field4,Field5, " & vbCrLf & " Field6,Field7,Field8,Field9,Field10 ," & vbCrLf & " Field11,Field12,Field13,Field14,Field15,Field16  " & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & m1 & "', " & vbCrLf & " '" & m2 & "', " & vbCrLf & " '" & m3 & "', " & vbCrLf & " '" & m4 & "', " & vbCrLf & " '" & m5 & "', " & vbCrLf & " '" & m6 & "', " & vbCrLf & " '" & m7 & "', " & vbCrLf & " '" & m8 & "', " & vbCrLf & " '" & m9 & "', " & vbCrLf & " '" & m10 & "', " & vbCrLf & " '" & m11 & "', " & vbCrLf & " '" & m12 & "', " & vbCrLf & " '" & m13 & "', " & vbCrLf & " '" & m14 & "', " & vbCrLf & " '" & m15 & "','" & m16 & "' ) "

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

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
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

        mTitle = "ACCOUNT OF DUTY-PAID GOODS RECEIVED FOR REPROCESSING AND REPAIRS"
        mSubTitle = "( Rule 97,97 - A,173-H,173-L )"


        mRPTName = "EDForm5.Rpt"

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
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        MainClass.AssignCRptFormulas(Report1, "mECCNo=""" & RsCompany.Fields("ECC_NO").Value & """")
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub


    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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

    Private Sub cmdTariff_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTariff.Click
        Call PrintStatus(False)
        SearchTariff()
    End Sub

    Private Sub frmSalesReturnAnnx58_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmSalesReturnAnnx58_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
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
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)
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
    Private Sub frmSalesReturnAnnx58_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmSalesReturnAnnx58_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
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
            .MaxCols = Col16
            .set_RowHeight(0, RowHeight * 1.75)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = Col1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col1, 8)

            .Col = col2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(col2, 8)

            .Col = Col3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col3, 15)

            .Col = Col4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col4, 8)

            .Col = Col5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col5, 20)


            .Col = Col6
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(Col6, 8)

            .Col = Col7
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col7, 20)

            .Col = Col8
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col8, 12)

            .Col = Col9
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col9, 8)

            .Col = Col10
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col10, 8)

            .Col = Col11
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col1, 8)

            .Col = Col12
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(Col12, 10)

            .Col = Col13
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(Col13, 10)

            .Col = Col14
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col14, 8)

            .Col = Col15
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(Col15, 10)

            .Col = Col16
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(Col16, 8)

            '        .Col = Col1
            '        .ColMerge = MergeAlways
            '        .Col = Col2
            '        .ColMerge = MergeAlways
            '        .Col = Col3
            '        .ColMerge = MergeAlways
            '        .Col = Col4
            '        .ColMerge = MergeAlways
            ''        .Col = Col5
            ''        .ColMerge = MergeAlways
            '        .Col = Col12
            '        .ColMerge = MergeAlways

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
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
        Dim mMRRNo As String
        Dim mItemCode As String
        Dim mMRRDate As String

        Dim mNextMRRNo As String
        Dim mNextItemCode As String

        Dim mNextCheck As String
        Dim mCurrentCheck As String
        Dim mSupplierName As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mItemDesc As String
        Dim mPartNo As String

        Dim mMKey As String
        Dim mPrevMkey As String


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        ''MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        mBackColor = &HC0FFFF
        If RsTemp.EOF = False Then
            With SprdMain

                Do While RsTemp.EOF = False
                    .MaxRows = cntRow
                    .Row = cntRow

                    mMKey = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)

                    .Col = Col1
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("MODVATNO").Value), "", RsTemp.Fields("MODVATNO").Value))

                    .Col = col2
                    .Text = IIf(IsDbNull(RsTemp.Fields("VNO").Value) Or RsTemp.Fields("VNO").Value = "-1", "", RsTemp.Fields("VNO").Value)

                    .Col = Col3
                    .Text = RsCompany.Fields("Company_Name").Value

                    .Col = Col4
                    '                If mMkey <> mPrevMkey Then
                    .Text = "" 'Val(IIf(IsNull(RsTemp!ITEMVALUE), 0, RsTemp!ITEMVALUE))
                    '                End If

                    .Col = Col5
                    .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)
                    mItemDesc = IIf(IsDbNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)

                    .Col = Col6
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)))

                    .Col = Col7
                    .Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mSupplierName = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)

                    .Col = Col8
                    .Text = "Rectification"

                    .Col = Col9
                    .Text = ""

                    .Col = Col10
                    .Text = ""

                    .Col = Col11
                    .Text = ""

                    .Col = Col12
                    If mMKey <> mPrevMkey Then
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("MODVATAMOUNT").Value), "", RsTemp.Fields("MODVATAMOUNT").Value), "0.00")
                        '                    mBackColor = IIf(mBackColor = &H8000000F, &H80FF80, &H8000000F)
                        mBackColor = IIf(mBackColor = &HC0FFFF, &HFFFFC0, &HC0FFFF)
                        '                    &HC0FFFF, vbBlack, &HFFFFC0, vbBlack

                        .Row = cntRow
                        .Row2 = cntRow
                        .Col = 1
                        .col2 = .MaxCols
                        .BlockMode = True
                        .BackColor = System.Drawing.ColorTranslator.FromOle(mBackColor) ''&H8000000F     ''&H80FF80
                        .BlockMode = False

                    Else
                        .Row = cntRow
                        .Row2 = cntRow
                        .Col = 1
                        .col2 = .MaxCols
                        .BlockMode = True
                        .BackColor = System.Drawing.ColorTranslator.FromOle(mBackColor)
                        .BlockMode = False
                    End If

                    mMRRNo = Str(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "-1", RsTemp.Fields("AUTO_KEY_MRR").Value))
                    mMRRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("MRRDATE").Value), "", RsTemp.Fields("MRRDATE").Value), "DD/MM/YYYY")
                    mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    mCurrentCheck = mMRRNo & mItemCode

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        mNextMRRNo = Str(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "-1", RsTemp.Fields("AUTO_KEY_MRR").Value))
                        mNextItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                        mNextCheck = mNextMRRNo & mNextItemCode
                    Else
                        mNextCheck = ""
                    End If

                    If mCurrentCheck <> mNextCheck Then
                        Call GetQtyAfterRecovered(Val(mMRRNo), mMRRDate, mItemCode, mSupplierName, mBillNo, mBillDate, mItemDesc, mPartNo, cntRow)
                    End If

                    '                RsTemp.MoveNext
                    mPrevMkey = mMKey
                    cntRow = cntRow + 1
                Loop
            End With
        End If
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1

        ''SELECT CLAUSE...

        '    MakeSQL = " SELECT AUTO_KEY_MRR,MRRDATE, CMST.SUPP_CUST_NAME,IH.BILLNO,IH.INVOICE_DATE,ID.ITEM_CODE,ID.ITEM_DESC, " & vbCrLf _
        ''            & " ID.CUSTOMER_PART_NO, SUM((ID.ITEM_QTY-ID.SHORTAGE_QTY)) AS ITEM_QTY,AVG(ID.ITEM_RATE) AS ITEM_RATE"

        MakeSQL = " SELECT AUTO_KEY_MRR,MRRDATE,IH.MKEY, IH.MODVATDATE,IH.MODVATNO, IH.VNO, ID.ITEM_CODE,ID.ITEM_DESC," & vbCrLf & " SUM((ID.ITEM_QTY-ID.SHORTAGE_QTY)) AS ITEM_QTY, CMST.SUPP_CUST_NAME, SUM(IH.MODVATAMOUNT) AS MODVATAMOUNT"

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST"

        ''& " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        MakeSQL = MakeSQL & vbCrLf & " AND IH.MODVATDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf & " AND IH.MODVATDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If

            MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        End If

        If chkTariff.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariff.Text) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND DECODE(CMST.WITHIN_COUNTRY,'Y',ISMODVAT,'Y')='Y' AND REJECTION='Y'"

        ''GROUP CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & "GROUP BY AUTO_KEY_MRR,MRRDATE,IH.MKEY,IH.MODVATDATE,IH.MODVATNO, IH.VNO, ID.ITEM_CODE,ID.ITEM_DESC,  CMST.SUPP_CUST_NAME"
        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.MODVATDATE,IH.MODVATNO,ID.ITEM_CODE, CMST.SUPP_CUST_NAME "

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

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = Col6
                mQty = mQty + CDbl(IIf(IsNumeric(.Text), .Text, 0))
            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, Col1)
            .Col = Col5
            .Row = .MaxRows
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows

            .Col = Col6
            .Text = VB6.Format(mQty, "0.00")

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub GetQtyAfterRecovered(ByRef pMRRNo As Double, ByRef pMRRDate As String, ByRef pITEM_CODE As String, ByRef mSupplierName As String, ByRef mBillNo As String, ByRef mBillDate As String, ByRef mItemDesc As String, ByRef mPartNo As String, ByRef cntRow As Integer)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        SqlStr = "SELECT IH.DESP_DATE, INV.BILLNO, SUM(ID.PACKED_QTY) AS PACKED_QTY, SUM(IID.ITEM_ED) AS ITEM_ED" & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID, FIN_INVOICE_HDR INV, FIN_INVOICE_DET IID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP " & vbCrLf & " AND IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.AUTO_KEY_DESP=INV.AUTO_KEY_DESP" & vbCrLf & " AND INV.MKEY=IID.MKEY AND IID.ITEM_CODE=ID.ITEM_CODE AND IID.SUBROWNO=ID.SERIAL_NO" & vbCrLf & " AND ID.MRR_REF_NO=" & Val(CStr(pMRRNo)) & "" & vbCrLf & " AND TRIM(ID.ITEM_CODE)='" & pITEM_CODE & "'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.DESP_DATE, INV.BILLNO "
        ''& " AND MRR_REF_DATE=TO_DATE('" & vb6.Format(pMRRDATE, "DD-MMM-YYYY") & "')" & vbCrLf _
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            With SprdMain
                Do While Not RS.EOF
                    .Row = cntRow

                    '                .Col = Col1
                    '                .Text = pMRRDate
                    '
                    '                .Col = Col2
                    '                .Text = mSupplierName
                    '
                    '                .Col = Col3
                    '                .Text = mBillNo
                    '
                    '                .Col = Col4
                    '                .Text = mBillDate
                    '
                    '                .Col = Col5
                    '                .Text = mItemDesc
                    '
                    '                .Col = Col6
                    '                .Text = mPartNo
                    '
                    .Col = Col13
                    .Text = CStr(Val(IIf(IsDbNull(RS.Fields("ITEM_ED").Value), "", RS.Fields("ITEM_ED").Value)))

                    .Col = Col14
                    .Text = IIf(IsDbNull(RS.Fields("BILLNO").Value), "", RS.Fields("BILLNO").Value)

                    .Col = Col15
                    .Text = CStr(Val(IIf(IsDbNull(RS.Fields("PACKED_QTY").Value), "", RS.Fields("PACKED_QTY").Value)))

                    RS.MoveNext()
                    If RS.EOF = False Then
                        .MaxRows = .MaxRows + 1
                        cntRow = cntRow + 1
                        .Row = cntRow
                        .Row2 = cntRow
                        .Col = 1
                        .col2 = .MaxCols
                        .BlockMode = True
                        .BackColor = System.Drawing.ColorTranslator.FromOle(mBackColor)
                        .BlockMode = False
                    End If
                Loop
            End With
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
End Class
