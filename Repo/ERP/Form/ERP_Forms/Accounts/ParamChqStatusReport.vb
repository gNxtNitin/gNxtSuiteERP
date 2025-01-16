Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamChqStatusReport
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection					
    Dim mAccountCode As Integer
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColVNo As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColChqNo As Short = 4
    Private Const ColChqDate As Short = 5
    Private Const ColPartyName As Short = 6
    Private Const ColChqAmount As Short = 7
    Private Const ColChqDC As Short = 8
    Private Const ColHOSendDate As Short = 9
    Private Const ColHORecdDate As Short = 10
    Private Const ColIssueDate As Short = 11
    Private Const ColDepositDate As Short = 12
    Private Const ColClearDate As Short = 13
    Private Const ColCompanyCode As Short = 14

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForReport(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim mMkey As String
        Dim mVNo As String
        Dim mVDate As String
        Dim mChqNo As String
        Dim mChqDate As String
        Dim mPartyName As String
        Dim mChqAmount As String
        Dim mHOSendDate As String
        Dim mHORecdDate As String
        Dim mIssueDate As String
        Dim mDepositDate As String
        Dim mClearDate As String
        Dim cntRow As Integer
        Dim mUnitFrom As Integer
        Dim mUnitTo As Integer
        Dim cntUnit As Integer
        Dim mDC As String
        Dim mTotalAmount As Double

        Dim mCompanyCode As Integer
        Dim mSql As String
        Dim RsCC As ADODB.Recordset
        Dim mTableName As String

        Dim xCompanyCode As Integer

        If FieldsVerification() = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        MainClass.ClearGrid(SprdMain, RowHeight)

        FormatSprdMain()

        SqlStr = MakeSQL()

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1
        mTotalAmount = 0

        If Not RsTemp.EOF Then
            With SprdMain
                Do While Not RsTemp.EOF
                    mMkey = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                    mVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                    mVDate = IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
                    mChqNo = IIf(IsDBNull(RsTemp.Fields("CHEQUENO").Value), "", RsTemp.Fields("CHEQUENO").Value)
                    mChqDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CHQDATE").Value), "", RsTemp.Fields("CHQDATE").Value), "DD/MM/YYYY")
                    mPartyName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mChqAmount = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value), "0.00")
                    mDC = IIf(IsDBNull(RsTemp.Fields("DC").Value), "", RsTemp.Fields("DC").Value)

                    mTotalAmount = mTotalAmount + (mChqAmount * IIf(mDC = "D", 1, -1))

                    mHOSendDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SEND_HO_DATE").Value), "", RsTemp.Fields("SEND_HO_DATE").Value), "DD/MM/YYYY")
                    mHORecdDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("RECD_HO_DATE").Value), "", RsTemp.Fields("RECD_HO_DATE").Value), "DD/MM/YYYY")
                    mIssueDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CHQ_ISSUE_DATE").Value), "", RsTemp.Fields("CHQ_ISSUE_DATE").Value), "DD/MM/YYYY")
                    mDepositDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CHQ_DEPOSIT_DATE").Value), "", RsTemp.Fields("CHQ_DEPOSIT_DATE").Value), "DD/MM/YYYY")
                    mClearDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("CLEARDATE").Value), "", RsTemp.Fields("CLEARDATE").Value), "DD/MM/YYYY")

                    xCompanyCode = IIf(IsDBNull(RsTemp.Fields("CompanyCode").Value), -1, RsTemp.Fields("CompanyCode").Value)

                    .Row = cntRow
                    .Col = ColMKEY
                    .Text = mMkey

                    .Col = ColVNo
                    .Text = mVNo

                    .Col = ColVDate
                    .Text = mVDate

                    .Col = ColChqNo
                    .Text = mChqNo

                    .Col = ColChqDate
                    .Text = mChqDate

                    .Col = ColPartyName
                    .Text = mPartyName

                    .Col = ColChqAmount
                    .Text = mChqAmount

                    .Col = ColChqDC
                    .Text = mDC & "R"

                    .Col = ColHOSendDate
                    .Text = mHOSendDate

                    .Col = ColHORecdDate
                    .Text = mHORecdDate

                    .Col = ColIssueDate
                    .Text = mIssueDate

                    .Col = ColDepositDate
                    .Text = mDepositDate

                    .Col = ColClearDate
                    .Text = mClearDate

                    .Col = ColCompanyCode
                    .Text = CStr(xCompanyCode)



                    cntRow = cntRow + 1
                    .MaxRows = cntRow

NextRec:
                    RsTemp.MoveNext()
                Loop

                .Row = cntRow
                .Col = ColPartyName
                .Text = "Total"
                .FontBold = True

                .Col = ColChqAmount
                .Text = Math.Abs(mTotalAmount)
                .FontBold = True

                .Col = ColChqDC
                .Text = IIf(mTotalAmount >= 0, "DR", "CR")
                .FontBold = True

            End With
        End If




        FormatSprdMain()
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume					
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged
        'Call ShowStatus(True)
    End Sub

    Private Sub txtBankName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.DoubleClick
        SearchBankName()
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtBankName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBankName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchBankName()
    End Sub

    Private Sub txtBankName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        Dim RsACM As ADODB.Recordset

        On Error GoTo ERR1

        If txtBankName.Text = "" Then GoTo EventExitSub

        SqlStr = "SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(txtBankName.Text)) & "'" & vbCrLf & " AND (SUPP_CUST_TYPE='2')"

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockOptimistic)

        If Not RsACM.EOF = False Then
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchBankName()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE='2')"

        MainClass.SearchMaster(txtBankName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_Name", SqlStr)

        If AcName <> "" Then
            txtBankName.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Public Function MakeSQL() As String
        On Error GoTo ErrPart
        Dim xAccountCode As String

        If MainClass.ValidateWithMasterTable(txtBankName.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAccountCode = MasterNo
        Else
            xAccountCode = "-1"
        End If


        MakeSQL = "SELECT DISTINCT IH.MKEY, IH.VNO, IH.VDATE, ID.CHEQUENO, ID.CHQDATE,  " & vbCrLf _
            & " CMST.SUPP_CUST_NAME, AMOUNT, DC," & vbCrLf _
            & " SEND_HO_DATE, RECD_HO_DATE, CHQ_ISSUE_DATE, CHQ_DEPOSIT_DATE, CLEARDATE, " & vbCrLf _
            & " IH.COMPANY_CODE AS COMPANYCODE" & vbCrLf _
            & " FROM FIN_VOUCHER_HDR IH, FIN_VOUCHER_DET ID, FIN_SUPP_CUST_MST CMST, GEN_COMPANY_MST CCMST" & vbCrLf _
            & " WHERE IH.Company_Code=CCMST.Company_Code" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.Company_Code=CMST.Company_Code " & vbCrLf _
            & " AND ID.ACCOUNTCODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.BOOKTYPE IN ('" & ConBankBook & "')" & vbCrLf _
            & " AND IH.CANCELLED='N'" '','" & ConPDCBook & "'					

        ''AND IH.BOOKSUBTYPE ='P' 

        If cboUnit.SelectedIndex = 0 Then

        Else
            MakeSQL = MakeSQL & vbCrLf & " AND CCMST.COMPANY_NAME ='" & cboUnit.Text & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.BOOKCODE='" & MainClass.AllowSingleQuote(xAccountCode) & "'"



        If optStatus(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND (RECD_HO_DATE='' OR RECD_HO_DATE IS NULL)"
        ElseIf optStatus(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND (CHQ_ISSUE_DATE='' OR CHQ_ISSUE_DATE IS NULL)"
        ElseIf optStatus(3).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND (CHQ_DEPOSIT_DATE='' OR CHQ_DEPOSIT_DATE IS NULL)"
        ElseIf optStatus(4).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND (CLEARDATE='' OR CLEARDATE IS NULL)"
        ElseIf optStatus(5).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND (CLEARDATE<>'' OR CLEARDATE IS NOT NULL)"
        ElseIf optStatus(6).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND (CLEARDATE='' OR CLEARDATE IS NULL OR  CLEARDATE>TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & " AND IS_REVERSAL_MADE='N' AND IS_REVERSAL_VOUCHER='N'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & " AND (IS_REVERSAL_MADE='Y' OR IS_REVERSAL_VOUCHER='Y')"
        End If

        '    MakeSql = MakeSql & vbCrLf & " AND (ID.CHEQUENO<>'' OR ID.CHEQUENO IS NOT NULL)"					

        'If optStatus(6).Checked = True Then

        'Else
        MakeSQL = MakeSQL & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "D-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        & " AND VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        'End If


        MakeSQL = MakeSQL & vbCrLf & " ORDER BY CLEARDATE,  ID.CHQDATE,  ID.CHEQUENO, IH.VDATE, IH.VNO,CMST.SUPP_CUST_NAME"

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume					
    End Function

    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim mDRNo As String

        If MainClass.ValidateWithMasterTable(Trim(txtBankName.Text), "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = False Then
            FieldsVerification = False
            Exit Function
        End If


        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then txtDateTo.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmParamChqStatusReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamChqStatusReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''''Set PvtDBCn = New ADODB.Connection					
        ''''PvtDBCn.Open StrConn					
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        'TxtAccount.Enabled = False
        'cmdsearch.Enabled = False
        'chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        txtBankName.Enabled = True
        cmdsearchBank.Enabled = True

        Call FillComboBox()

        Call PrintStatus(True)
        txtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        txtDateTo.Text = CStr(RunDate)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamChqStatusReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdMain()
        Dim I As Integer

        With SprdMain
            .MaxCols = ColCompanyCode
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColVNo, 15)

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColVDate, 10)

            .Col = ColChqNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColChqNo, 12)

            .Col = ColChqDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColChqDate, 10)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 40)
            .ColsFrozen = ColPartyName

            .Col = ColChqAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColChqAmount, 15)

            .Col = ColChqDC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChqDC, 4)

            For I = ColHOSendDate To ColDepositDate
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(I, 8)
                .ColHidden = True
            Next

            For I = ColClearDate To ColClearDate
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(I, 10)
            Next

            For I = ColCompanyCode To ColCompanyCode
                .Col = I
                .CellType = SS_CELL_TYPE_EDIT
                .TypeHAlign = SS_CELL_H_ALIGN_LEFT
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
                .TypeEditLen = 255
                .TypeEditMultiLine = True
                .set_ColWidth(I, 10)
                .ColHidden = False '' True					
            Next

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000					
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColVNo
            .Text = "Voucher No."

            .Col = ColVDate
            .Text = "Voucher Date"

            .Col = ColChqNo
            .Text = "Cheque No."

            .Col = ColChqDate
            .Text = "Cheque Date"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColChqAmount
            .Text = "Cheque Amount"

            .Col = ColChqDC
            .Text = "Cheque DC"

            .Col = ColHOSendDate
            .Text = "Send Date TO HO"

            .Col = ColHORecdDate
            .Text = "Recd Date From HO"

            .Col = ColIssueDate
            .Text = "Issue Date to Party"

            .Col = ColDepositDate
            .Text = "Cheque Deposit Date"

            .Col = ColClearDate
            .Text = "Clear Date"

            .Col = ColCompanyCode
            .Text = "Company Code"

        End With
    End Sub
    Private Sub frmParamChqStatusReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForReport(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForReport(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        SqlStr = ""


        '''''Select Record for print...					

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = MainClass.FetchFromTempData(SqlStr, "")

        mTitle = "Cheque Status Register"

        If optStatus(1).Checked = True Then
            mTitle = mTitle & " (Pending For Received From HO)"
        ElseIf optStatus(2).Checked = True Then
            mTitle = mTitle & " (Pending For Issue to Supplier)"
        ElseIf optStatus(3).Checked = True Then
            mTitle = mTitle & " (Pending For Deposit)"
        ElseIf optStatus(4).Checked = True Then
            mTitle = mTitle & " (Pending For Clearing)"
        ElseIf optStatus(5).Checked = True Then
            mTitle = mTitle & " (Pending For Clearing)"
        ElseIf optStatus(6).Checked = True Then
            mTitle = mTitle & " (Pending For Clearing As on Date)"
        End If

        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")


        mReportFileName = "ChqStatusReg.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume					
    End Sub


    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies					
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
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
    Private Sub FillComboBox()
        On Error GoTo ErrPart

        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        'ALL_USERS
        cboUnit.Items.Clear()
        SqlStr = "SELECT COMPANY_NAME FROM GEN_COMPANY_MST WHERE STATUS='O' ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboUnit.Items.Add("ALL")
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboUnit.Items.Add(RS.Fields("COMPANY_NAME").Value)
                RS.MoveNext()
            Loop
        End If

        cboUnit.Text = RsCompany.Fields("COMPANY_NAME").Value


        cboShow.Items.Clear()
        cboShow.Items.Add("ALL")
        cboShow.Items.Add("Without Reversal Voucher")
        cboShow.Items.Add("Only Reversal Voucher")
        cboShow.SelectedIndex = 0


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdsearchBank_Click(sender As Object, e As EventArgs) Handles cmdsearchBank.Click
        SearchBankName()
    End Sub
End Class
