Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamDNRateDiff
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    '''Private PvtDBCn As ADODB.Connection
    Dim mAccountCode As String

    Private Const ColMKEY As Short = 1
    Private Const ColLocked As Short = 2
    Private Const ColDNNo As Short = 3
    Private Const ColDNDate As Short = 4
    Private Const ColDNType As Short = 5
    Private Const ColBillNo As Short = 6
    Private Const ColBillDate As Short = 7
    Private Const ColPartyName As Short = 8
    Private Const ColItemCode As Short = 9
    Private Const ColItemDesc As Short = 10
    Private Const ColQty As Short = 11
    Private Const ColRate As Short = 12
    Private Const ColBillAmount As Short = 13
    Private Const ColGSTApp As Short = 14
    Private Const ColGSTAmount As Short = 15
    Private Const ColAccountPostingHead As Short = 16
    Private Const ColPONo As Short = 17
    Private Const ColPORate As Short = 18
    Private Const ColBillRate As Short = 19
    Private Const ColRateDiff As Short = 20
    Private Const ColCompanyCode As Short = 21
    Private Const ColCompanyName As Short = 22

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
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportonShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSubTitle1 As String
        Dim mHeading As String
        Dim mHeading1 As String
        Dim mHeading2 As String

        PubDBCn.Errors.Clear()


        'If TxtName.Text = "" Then Exit Sub

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        If InsertPrintDummy = False Then GoTo ReportErr

        Report1.Reset()
        If optType(0).Checked = True Then
            mTitle = "Debit Note Check List"
        Else
            mTitle = "Credit Note Check List"
        End If

        If optApproval(0).Checked = True Then
            mSubTitle = IIf(cboType.Text = "ALL", "", cboType.Text & " - ") & "Approved List"
        ElseIf optApproval(1).Checked = True Then
            mSubTitle = IIf(cboType.Text = "ALL", "", cboType.Text & " - ") & "Not Approved List"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\DnCnCheckList.RPT"

        '''''Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Function InsertPrintDummy() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mDNNo As String
        Dim mDNDate As String
        Dim mPartyName As String
        Dim mItemDesc As String
        Dim mPONo As String
        Dim mPORate As String
        Dim mBillRate As String
        Dim mRateDiff As String
        Dim mBillNo As String
        Dim mQty As String
        Dim mBillDate As String


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColDNNo
                mDNNo = .Text

                .Col = ColDNDate
                mDNDate = .Text

                .Col = ColBillNo
                mBillNo = Replace(.Text, "'", "''")

                .Col = ColBillDate
                mBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColPartyName
                mPartyName = Replace(.Text, "'", "''")

                .Col = ColItemDesc
                mItemDesc = Replace(.Text, "'", "''")

                .Col = ColQty
                mQty = .Text

                .Col = ColPONo
                mPONo = .Text

                .Col = ColPORate
                mPORate = .Text

                .Col = ColBillRate
                mBillRate = .Text

                .Col = ColRateDiff
                mRateDiff = .Text

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow, " & vbCrLf & " Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,Field10, Field11) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & mDNNo & "', " & vbCrLf & " '" & Trim(mDNDate) & "', " & vbCrLf & " '" & Trim(mPartyName) & "', " & vbCrLf & " '" & Trim(mItemDesc) & "', " & vbCrLf & " '" & Trim(mPONo) & "', " & vbCrLf & " '" & Trim(mPORate) & "', " & vbCrLf & " '" & Trim(mBillRate) & "', " & vbCrLf & " '" & Trim(mRateDiff) & "', " & vbCrLf & " '" & Trim(mBillNo) & "'," & vbCrLf & " '" & Trim(mQty) & "', '" & VB6.Format(mBillDate, "DD/MM/YYYY") & "') "

                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        PubDBCn.RollbackTrans()
        InsertPrintDummy = False
        MsgInformation(Err.Description)
    End Function
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
        '    CalcSprdTotal
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDNRateDiff_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Debit/Credit Note Check List"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDNRateDiff_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        cboType.Items.Clear()
        cboType.Items.Add("ALL")
        cboType.Items.Add("PO RATE DIFF.")
        cboType.Items.Add("SHORTAGE")
        cboType.Items.Add("REJECTION")
        cboType.Items.Add("DISCOUNT")
        cboType.Items.Add("OTHERS")
        cboType.Items.Add("AMEND. RATE DIFF")
        cboType.Items.Add("VOLUME DISCOUNT")

        cboType.SelectedIndex = 0

        Call FillInvoiceType()

        lblTrnType.Text = CStr(-1)
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False
        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Call frmParamDNRateDiff_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamDNRateDiff_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamDNRateDiff_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub optApproval_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optApproval.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optApproval.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
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

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim xVDate As String
        Dim xMKey As String
        Dim xVNo As String
        Dim xBookType As String
        Dim xBookSubType As String
        Dim pIndex As Integer
        Dim xVTYPE As String
        Dim xCompanyCode As Long

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColMKEY
        xMKey = Me.SprdMain.Text

        SprdMain.Col = ColCompanyCode
        xCompanyCode = Val(SprdMain.Text)

        If RsCompany.Fields("COMPANY_CODE").Value <> xCompanyCode Then
            Exit Sub
        End If

        SqlStr = "SELECT * FROM FIN_DNCN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & xMKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            xVDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")

            '************************************************************************************
            '        xVNo = Mid(IIf(IsNull(RsTemp!VNO), "", RsTemp!VNO), 3) 'MRAKED DEEPAK 30/04/2004 , BEOUSE PROB WHEN VNO LIKE 'DNW01132', SO WRITTEN NEXT STMT
            If IsDbNull(RsTemp.Fields("VNO").Value) Then
                xVNo = ""
            Else
                If RsCompany.Fields("FYEAR").Value >= 2020 Then
                    xVNo = Mid(RsTemp.Fields("VNO").Value, Len(RsTemp.Fields("VNO").Value) - 7)
                Else
                    xVNo = Mid(RsTemp.Fields("VNO").Value, Len(RsTemp.Fields("VNO").Value) - 4)
                End If
            End If
            '************************************************************************************

            xBookType = IIf(IsDbNull(RsTemp.Fields("BookType").Value), "", RsTemp.Fields("BookType").Value)
            xBookSubType = IIf(IsDbNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)
            xVTYPE = IIf(IsDbNull(RsTemp.Fields("VTYPE").Value), "", RsTemp.Fields("VTYPE").Value)

            Call ShowTrn(xMKey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType, Me)
        End If
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S', 'C')"
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN('S', 'C')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            '        lblAcCode.text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            '        lblAcCode.text = ""
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
            .MaxCols = ColCompanyName
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 15)
            .ColHidden = True



            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyCode, 15)
            .ColHidden = True

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyName, 15)
            .ColHidden = False

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColDNNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDNNo, 15)

            .Col = ColDNDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDNDate, 10)

            .Col = ColDNType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDNType, 10)

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
            '        .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 9)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 15)

            '.Col = ColQty
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 2
            '.TypeFloatMin = CDbl("-99999999999")
            '.TypeFloatMax = CDbl("99999999999")
            '.TypeFloatMoney = False
            '.TypeFloatSeparator = False
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatSepChar = Asc(",")
            '.set_ColWidth(ColQty, 9.5)

            '.Col = ColRate
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 2
            '.TypeFloatMin = CDbl("-99999999999")
            '.TypeFloatMax = CDbl("99999999999")
            '.TypeFloatMoney = False
            '.TypeFloatSeparator = False
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatSepChar = Asc(",")
            '.set_ColWidth(ColRate, 9.5)

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPONo, 8)

            For cntCol = ColPORate To ColRateDiff
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9.5)
            Next


            .Col = ColAccountPostingHead
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAccountPostingHead, 15)

            For cntCol = ColQty To ColBillAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9.5)
            Next

            .Col = ColGSTApp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColGSTApp, 15)

            For cntCol = ColGSTAmount To ColGSTAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 9.5)
            Next

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
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

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        CalcSprdTotal()

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
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        ''''SELECT CLAUSE...

        MakeSQL = " SELECT IH.mKey,'', IH.VNO, IH.VDATE, " & vbCrLf _
            & " CASE WHEN IH.DNCNTYPE='P' THEN 'PO RATE DIFF.'" & vbCrLf _
            & " WHEN IH.DNCNTYPE='S' THEN 'SHORTAGE'" & vbCrLf _
            & " WHEN IH.DNCNTYPE='R' THEN 'REJECTION'" & vbCrLf _
            & " WHEN IH.DNCNTYPE='D' THEN 'DISCOUNT'" & vbCrLf _
            & " WHEN IH.DNCNTYPE='O' THEN 'OTHERS'" & vbCrLf _
            & " WHEN IH.DNCNTYPE='A' THEN 'AMEND. RATE DIFF'" & vbCrLf _
            & " WHEN IH.DNCNTYPE='V' THEN 'VOLUME DISCOUNT'" & vbCrLf _
            & " ELSE ''" & vbCrLf _
            & " END AS PO_TYPE," & vbCrLf _
            & " ID.SUPP_REF_NO, SUPP_REF_DATE, " & vbCrLf _
            & " CMST.SUPP_CUST_NAME,ID.ITEM_CODE,ID.ITEM_DESC,ID.ITEM_QTY,  ID.ITEM_RATE, ITEM_AMT, " & vbCrLf _
            & " DECODE(IH.ISGSTREFUND,'G','GST APPLICABLE','WITHOUT GST') ISGSTREFUND, (CGST_AMOUNT + SGST_AMOUNT + IGST_AMOUNT) AS GST_AMOUNT," & vbCrLf _
            & " AMST.SUPP_CUST_NAME," & vbCrLf _
            & " ID.REF_PO_NO, " & vbCrLf _
            & " TO_CHAR(GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.VDATE,DECODE(LENGTH(TRIM(TRANSLATE(ID.REF_PO_NO, ' +-.0123456789',' '))),NULL,ID.REF_PO_NO,-1),ID.ITEM_CODE)) As PORATE, " & vbCrLf _
            & " TO_CHAR(GETPurchaseVPrice(" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ",ID.PURVDATE,ID.PURVNO,ID.ITEM_CODE)) As PURRATE,ID.ITEM_RATE AS ITEM_RATE1,IH.COMPANY_CODE,GEN.COMPANY_SHORTNAME"

        '

        '' & " TO_CHAR(GetITEMPRICE_NEW(IH.FYEAR, " & RsCompany.Fields("FYEAR").Value & ", IH.VDATE,DECODE(SUBSTR(ID.REF_PO_NO,1,1),'S',-1,ID.REF_PO_NO),ID.ITEM_CODE)) As PORATE, " & vbCrLf _

        'GetITEMPRICE_NEW(1,1,TO_DATE('" & vb6.Format(mBillDate, "DD-MMM-YYYY") & "')," & Val(mPONo) & ",'" & mItemCode & "')

        ''''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST AMST, GEN_COMPANY_MST GEN"

        '& " FIN_PURCHASE_HDR PH, FIN_PURCHASE_DET PD, " & vbCrLf _
        '''''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE IH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY(+)" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE  AND IH.COMPANY_CODE=AMST.COMPANY_CODE "  ''& vbCrLf |            & " AND PH.MKEY=PD.MKEY" & vbCrLf |            & " AND IH.PURVNO=PH.VNO(+)" & vbCrLf |            & " AND IH.PURVDATE=PH.VDate(+) AND ID.ITEM_CODE=PD.ITEM_CODE(+) "


        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & ", " & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQL = MakeSQL & vbCrLf & " And GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then

            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                If optType(0).Checked = True Then
                    MakeSQL = MakeSQL & vbCrLf & "AND IH.DEBITACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
                Else
                    MakeSQL = MakeSQL & vbCrLf & "AND IH.CREDITACCOUNTCODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
                End If
            End If
        End If

        If optType(0).Checked = True Then
            MakeSQL = MakeSQL & " AND IH.DEBITACCOUNTCODE =CMST.SUPP_CUST_CODE "
            MakeSQL = MakeSQL & vbCrLf & "AND IH.BOOKTYPE='" & VB.Left(ConDebitNote, 1) & "' AND IH.BOOKSUBTYPE='" & VB.Right(ConDebitNote, 1) & "'"
        Else
            MakeSQL = MakeSQL & " AND IH.CREDITACCOUNTCODE=CMST.SUPP_CUST_CODE "
            MakeSQL = MakeSQL & vbCrLf & "AND IH.BOOKTYPE='" & VB.Left(ConCreditNote, 1) & "' AND IH.BOOKSUBTYPE='" & VB.Right(ConCreditNote, 1) & "'"
        End If

        If optType(0).Checked = True Then
            MakeSQL = MakeSQL & " AND IH.CREDITACCOUNTCODE =AMST.SUPP_CUST_CODE "
        Else
            MakeSQL = MakeSQL & " AND IH.DEBITACCOUNTCODE=AMST.SUPP_CUST_CODE "
        End If

        If cboType.Text <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.DNCNTYPE='" & UCase(VB.Left(cboType.Text, 1)) & "'"
        End If

        If optApproval(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.APPROVED='Y'"
        ElseIf optApproval(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.APPROVED='N'"
        End If

        MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='N'"

        MakeSQL = MakeSQL & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        ''''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.VNO, IH.VDATE"

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

        If Trim(cboType.Text) = "" Then
            MsgInformation("Debit Note/Credit Note Type is Blank.")
            TxtAccount.Focus()
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


        Dim mQty As Double = 0
        Dim mAmount As Double = 0
        Dim mGSTAmount As Double = 0

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                '            .Col = 4
                '            mItemAmount = mItemAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColQty
                mQty = mQty + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColBillAmount
                mAmount = mAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColGSTAmount
                mGSTAmount = mGSTAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColPartyName)
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
            .Font = VB6.FontChangeBold(.Font, True)
            .BlockMode = False

            .Row = .MaxRows

            .Col = ColQty
            .Text = VB6.Format(mQty, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColBillAmount
            .Text = VB6.Format(mAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColGSTAmount
            .Text = VB6.Format(mGSTAmount, "0.00")
            .Font = VB6.FontChangeBold(.Font, True)

        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
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
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_NAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_NAME").Value = RsCompany.Fields("COMPANY_NAME").Value, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
End Class
